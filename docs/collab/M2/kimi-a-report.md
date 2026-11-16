# M2 Kimi A report — orchestration, ledger, identity, saves

## Stage status

M2a service wiring landed. Phase map v4 + rule-examples.json +
api-examples.json are FROZEN (A-owned per 00-TEAM.md), with Claude's
approved orderings written in (end-of-year accounting with deaths processed
after income in family_events; player mortality before decision_offer).

## Delivered this stage

- `src/game/context.py` — RuleContext (frozen contract fields: player,
  people_by_id by canonical person_id, annual_flags, cursor, done_markers,
  versioned data, person-creation hook).
- `src/game/rule_adapter.py` — RuleResult adapters: money_entries through
  the single Ledger authority; events to journal with per-person
  relationship deltas; continuation/pending folded into the turn cursor
  (explicit None clears pending).
- `src/game/coordinator.py` — yearly turn coordinator running the frozen
  12-phase order from the persisted cursor. C-served phases (work_income,
  education) dispatch to src/menus/rule_actions.advance_phase; A-owned
  phases run built-in minimal implementations preserving M1 behavior
  (year_start snapshot, relations drift, stat drift, family_events relation
  deaths + archive, player mortality, M1 contextual decision_offer,
  year_close). Loop-guarded; pending interrupts persist cursor + done
  markers so resolve resumes without rerunning phases.
- `src/game/service.py` — `_cmd_age_up` now runs the coordinator; command
  dispatch accepts C's action ids (ACTION_IDS); kind-tagged rule pendings
  resolve through rule_actions.resolve_choice (stored pre-sampled outcomes),
  M1 templates keep the service path; new state sections: education, work,
  health, family, finances (balance/debts/ledger from the Ledger
  authority), turn cursor, generation, completed_lives; enriched
  available_actions (category/inputs/known_costs/limits/enabled/reason)
  from C's action_specs.
- Validator: kind-tagged rule pendings validate a lighter shape
  (person_ids optional, normalized); M1 templates still require
  known_effects/_effects/_outcome; `turn` optional with default.
- `src/game/finance.py` — single posting authority (from earlier stage):
  idempotent keys {turn}:{phase}:{occurrence}:{account}:{leg}, conflict on
  changed details, unit rollback, per-account balances, debt separation.

## Cross-agent notes

- Added `ACTION_IDS` to C's rule_actions.py (additive constant used by the
  dispatcher) — flagged to C.
- C's rule pendings carry `kind`/`sampled` (server-side, never projected);
  service projects kind + options with enabled/reason/inputs/known_effects.

## Validation (focused, per process)

- tests/test_game_m2a.py (new, 6): school journey incl. study limit and
  drop-out refusal branch; find_job pending → resolve → employed; employed
  year posts income legs once (retry cannot double-post); adjust_hours
  validation (400) + success; rule pending survives restart.
- tests/test_game_context.py (6), tests/test_game_finance.py (9),
  tests/test_game_service.py (37), tests/test_appearance.py (39),
  C's test_rules_* (67), B's test_web_m1 (26): all green.
- Deterministic seeds, replay/rollback protections and M1 migration tests
  remain green through the coordinator path.

## Limitations / next

- debt_service, illness, annual_events are no-op phases until C's M2b/M2c
  extractions; family_events currently handles relation deaths only
  (pregnancy/births/children M2b; inheritance cash postings M2c).
- annual_estimate projection is intentionally None until the quote API is
  wired for previews (never RNG-sampled at projection time).
- Succession (M2c): C returns transfer instructions; A owns graph
  conversion — `convert_child_to_player` already preserves canonical
  person_ids.

## Dev scenario starts (spec addendum)

- Shape FROZEN in api-examples.json (`scenario_start_example`): optional
  profile key `scenario` on POST /api/games; ids `high_school_first_day`,
  `graduating_18`, `employed_30`, `near_retirement_64`; unknown id → 400
  `invalid_input`. B's dev panel (play-web.sh --dev) posts these ids.
  "30, married with a child" is deferred until M2b (partner/pregnancy
  transitions not extracted yet).
- `src/game/scenarios.py` — `drive_scenario` creates the game normally,
  then drives it through the PUBLIC command flow (age_up / find_job /
  resolve_decision) with the game's own seeded RNG until the scenario's
  conditions hold. Real transitions only — no bare field edits. A scenario
  game is an ordinary game afterwards; creation replay returns the
  scenario state (not age 0); a failed setup rolls back game + ledger so a
  retry starts clean.
- Service validation of the scenario id happens before anything is
  created; the driver runs inside the service lock.

Focused tests (tests/test_game_scenarios.py, 7): all four scenarios reach
their valid states and stay playable; same seed → identical scenario state;
creation replay (incl. after restart) returns the scenario state; unknown
scenario → typed 400 with no game left behind.
game suites: 64 OK; appearance 39 OK.

## Scenario factory v2 (D manifest coverage)

Shape re-frozen in api-examples.json with D's requested states:
`enrolled_child_10`, `unemployed_18`, `retirement_eligible_65`,
`university_pending_17` (funding decision left OPEN; typed error naming the
smarts gate if the supplied seed isn't accepted — PROVISIONAL behavior
documented in the examples), `ill_with_cash` (illnesses via the canonical
Player.add_illness API that legacy events use; cash from real employment).
Non-pending scenarios hand off a clean actionable state (contextual
decisions sampled during the final year are resolved; resolving never
ages). PROVISIONAL fields marked: finances.annual_estimate (None until the
quote API wires deterministic previews), health.treatments_available
(M2b).
M2a-S6 (performance<15 boundary) has no legitimate forcing transition —
D should drive from employed_30 until performance drops; noted to D.
Tests: 12 scenario tests green (5 new D-manifest tests incl. restart
persistence for illnesses); game suites 49 OK.

## M2 fix round (from M2-consolidated-review.md)

#1 (lead) interrupted yearly turns: `run_turn` returns a completion flag;
interrupted turns keep their ENTIRE state (turn id, done markers, snapshot,
funeral queue, `interrupted: true`) across saves/reloads; `_cmd_age_up`
resets the turn only on completion; BOTH resolve paths (rule-owned and M1
templates) resume the persisted continuation inside the same command until
the next decision or real year_close (gated on the interrupted marker so
menu actions on completed turns never run a year). Dead `resume_after_resolve`
field removed. Forced tests: university funding chain (same turn id/done
markers across a reload), forced M1 template at decision_offer.

#2 (lead, with C): my half — relations_age walks the frozen snapshot
(relations only) calling C's single `relation_year` primitive; snapshot
exposed as `ctx.turn_snapshot` for C's family_events newborn exclusion.

#4 (with C): my half — `rule_adapter.apply_effects_bundle`, the shared
COMPLETE applier (all stat keys incl stress/karma/performance, money via
the single ledger, relationship, illnesses, flags, terminal) with honest
actual-delta journaling; auto events use it with event-text fallback.
Forced tests: stress/karma/actual deltas, money ledger posting, text
fallback. (C's half: karma pass-through in _resolve_effects + deep-merged
pre-effects — in coordination.)

#5 (with C): my half — decision_offer rolls independent chance_n events
once per year (`independent_rolled` marker) and serves the queue one
occurrence per invocation; flavor pool separate via C's split API.

Minor: finance.post_many rollback now removes only rows inserted by the
failing batch (pre-existing idempotent replays survive) with a
failing-before test.

### Fix-round completion (integrated with C's interfaces)

#2 landed end-to-end: relations_age walks the frozen snapshot with C's
single `relation_year` primitive (no composite age_up — child
schooling/income/social cannot double-run); `ctx.turn_snapshot` exposed;
C's family_events receives `snapshot_ids` for newborn exclusion.
Forced no-double-application spy test: stat_drift fires once per person
per year; every person ages exactly +1.

#3 (A half): rule resolves now consume C's `deaths` instruction list —
single graph transition (archive/remove/mark-dead exactly once, partner
child-refs cleared), idempotent under replay, announcement journaled from
the stored cause (never resampled).

#4 landed end-to-end: C's deep-merged pre-effects + karma-inclusive
_resolve_effects; A's shared complete applier (rule_adapter.
apply_effects_bundle) serves auto AND selected outcomes — all stat keys,
ledger money, relationship, illnesses, flags, terminal — with honest
actual deltas and event-text fallback. Forced tests: stress/karma
deltas, money posting, text fallback, terminal death.

#5 landed end-to-end: decision_offer rolls C's `sample_independent`
(chance_n, legacy order, once per year via `independent_rolled` marker)
and serves the queue one occurrence per invocation; flavor pool separate
via `sample_flavor` — 1/N rates can no longer be diluted.

Scenario factory: retries with a shifted seed when terminal events end
a driven life early (documented dev-factory limit; terminal lightning
now actually kills — the fix working as designed).

Suite: 415 tests, 56.4s (was 74s — lifespan/retirement trial loops moved
to in-memory sessions).

### Final blockers (Codex re-review tail)

#5: decision_offer now drains queued AUTO independent events inside the
phase (legacy order preserved — a leading choice event still interrupts
first), and a choice event whose queue still has occurrences returns
`_keep_phase`: run_turn does NOT mark the phase done and points the
resume cursor back at decision_offer, so every sampled independent
occurrence is offered/applied across resolves without the queue being
reset. Forced real-service test: worry(auto) + kidnapping(choice) both
happen in one committed year; year completes with empty queue.

#4: verified through the real service — pre-effects apply exactly once
at offer (exactly one -31 event row; the year summary legitimately
aggregates the same delta), marker persisted, and a service-restart
reload before resolve cannot re-apply; resolve applies only the choice.
Forced tests in tests/test_game_events.FixRoundServiceLevelTests.

Full suite: 420 tests OK in 53.7s.
