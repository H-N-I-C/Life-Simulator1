# Kimi C — M2.5 fix round 2 report (2026-11-17)

Branch `dev`. Items: R3 (probability consumption), R7 (one v3
dispatch/condition path). QUALITY-GUIDE.md followed; forced real-path
tests; 3 hash seeds.

## R3 — accept = (u < p_accept), clamped: DONE

`src/menus/rule_actions.py` (`sample_dating_app`) now consumes B's
published additive field `acceptance.p_accept` (evaluator.py, B's R3
side) per the ratified contract:

- `accepted = rng.random() < p_accept`, ONE draw at offer creation,
  stored in `pending.sampled` (`accepted`, `p_accept`, `score`), never
  redrawn on resolve/reload.
- `p_accept` clamped to [0.02, 0.98]: refusal stays possible at a
  perfect score, acceptance stays possible at a zero score (soft
  modifiers are never bans).
- Tolerant fallback to `1 - refusal.p_refuse` only for pre-contract
  evaluator results (keeps the hard-validity gate and every other
  semantics unchanged).
- Fails-before: with the old `random() >= p_refuse` formula both new
  clamp tests invert (0.99-draw accepts, 0.005-draw refuses) — verified
  by construction against the old formula; the draw-order rigged-RNG
  tests pin both clamp ends and both sides of 0.5.

Tests: `tests/test_rules_dating.py` +4 (both clamp ends with scripted
first-draw RNG, both sides of p_accept, stored p_accept).

## R7 — one v3 dispatch/condition path: DONE (one 2-line A hook pending)

New in `src/game/events.py`:

- `evaluate_v3_conditions(conditions, context, bindings, actor_id)` —
  ONE evaluated tree for dispatch and resolve-time validity:
  `all/any/not` composing the frozen leaves: `relationship_state`,
  `union_state`, `expectation` (exclusivity agreement; absent agreement
  defaults to `exclusive`; consensual plural is recorded, never
  inferred), `recognition_status`, `kinship`, `milestone`, `bound`,
  `known_fact` + `reputation_at_least` (observer-scoped through A's
  knowledge core — facts are engine truth, knowledge belongs to an
  observer), and B's published policy predicates (`tenet_stance`,
  `faith_id`, `jurisdiction_id`, `attraction_band`, `openness_band`,
  `monogamy_band`). Scalar `min_age/max_age` compose with trees.
  Unknown leaves are typed `EventValidationError`, never a 500.
- `v3_bindable(ev, actor_id, context)` — probes every required role
  once (read-only) and returns the bindings so offer creation prebinds
  the SAME people the selector validated.
- `EventEngine.sample_v3(rng, age, seen_ids, context, prefixes)` — the
  single dispatch gate: cadence + age bounds + condition tree +
  bindability + one weighted draw; returns (event, bindings).

New in `src/menus/rule_actions.py`:

- `romance_events_phase(context, rng)` — the C-side yearly phase: at
  most ONE `rom_*` occurrence per year, seen-cadence in
  `env["v3_seen"]` (plain list, pickle-safe), full instantiate3 +
  pre-effects pipeline, children never enter, continuation to illness.
  Registered in `_PHASES`; `advance_phase` dispatches it with the
  envelope-bound context.

Reachability: the phase needs A's TWO-LINE coordinator wiring
(`"romance_events"` in `PHASE_ORDER` after `drama_events` + `RULE_PHASES`)
— requested as contracts-README Open item #5; A may also adopt the same
evaluator in its drama/parenting engines so dra_*/par_* conditions gate
identically. Until A lands the hook the phase is inert in production but
fully proven (below).

Tests: `tests/test_rules_v3_dispatch.py` — 14 tests, all real persisted
GameService: every condition leaf (graph, knowledge incl. a real
minted/disclosed fact, policy bands, milestone, bound, expectation
flips), typed error for unknown leaves, prefix/seen/age/condition gating
of the draw, phase cadence (one occurrence, seen marked, next offer is a
DIFFERENT event), children get nothing, and the phase's pending
resolving through the real `GameService.command` boundary.

## Interop fixes during the round (my tests vs A's landed A3)

A registered the real `offer`/`respond` appliers (family_graph/effects)
mid-round; my round-1 dating/loader tests assumed they were absent.
Fixed properly in MY test files: fakes now save/restore the real
registry entries, unregistered-effect assertions use an injected
`__unknown_test_effect` (still proves typed failure + no partial
application), and the stored-refusal expectation matches the deliberate
new behavior (refusal keeps ONLY the `offer` domain effect so resolving
records the graph attempt; happiness/chemistry stay replaced by the
refusal text — a session change in rule_actions I found in the diff, not
mine, and consistent with A's attempt/cooldown accounting).

## Verification

- My suites (dating, v3_dispatch, v3_loader, event_loader,
  event_content, hashseed): 72 tests OK, PYTHONHASHSEED 0/1/42 OK.
- `git diff --check` clean; compile OK; only C-owned files touched
  (events.py, rule_actions.py, my test files, README Open items).
- Full discovery: 698–705 tests (A/B/D adding tests mid-run), the
  failures observed are ALL in A's in-flight files
  (test_family_graph_service legacy-partner `.status` projections,
  test_weddings, test_knowledge forced-flow, test_game_m25_partner_sync,
  test_game_m25_secrecy) — A's R5/R8/R9 churn, not reproducible against
  a stable tree from my side; re-run after A reports.

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real GameService/Player/Partner envelopes throughout; the only
      mocks are B's evaluate_offer boundary (pre-existing pattern).
- [x] Failing-before: clamp ends pinned by construction against the old
      formula; unknown-effect tests inject a bogus effect name.
- [x] Seeded RNG only; 3 hash seeds reproduce; no iteration-before-draw
      (pool order = loader order, weights stable).
- [x] Typed errors: unknown condition kind and unknown domain effect
      are typed; unknown phase id typed.
- [x] Money: no new postings; pre-effects pipeline reuses the frozen
      single-posting bundle path.
- [x] Identity: person_ids only; bindings probed once and prebound.
- [x] Save/load: v3_seen is a plain list; pending stores its own
      samples; reload keeps cadence (service-level resolve test).
- [x] Age gates: selector enforces min/max age; phase refuses children;
      romantic guard still applies at instantiate3/resolve.
- [x] No soft-locks: all shipped rom_* events keep a free exit
      (content test from round 1 still green).
Hygiene
- [x] Only C-owned files edited; shared files re-read fresh (A moved
      backfill_all and knowledge APIs mid-round — tests use the real
      locations).
- [x] No probability/balance changes beyond the ratified R3 contract.
- [x] Focused suites green x3 hash seeds.
- [n/a] UI items are B's.
- [x] Rare paths forced via injected effects/fixtures, never natural
      rolls.
