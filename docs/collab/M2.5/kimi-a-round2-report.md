# Kimi A — M2.5 fix round 2 report (2026-11-17, updated 2026-11-18)

## Q-round additions (round 7, 2026-11-18)

**Q1 — ONE shared engine quote for ceremony legal consequences
(IMPLEMENTED; contract published to B first).** The round-6 evidence
showed the web preview re-sampling independently (preview 1135 vs posted
1923) and first-line-only display (preview $500, posted $500+$700). Now:
- `weddings.core.quote_ceremony_fines(env, union_id) -> dict` — samples
  variable ranges ONCE, stores the quote on the plan
  (`env["weddings"]["quotes"][union_id]`, persisted), returns ALL fine
  lines. A stored quote is reused while the law is unchanged (same
  jurisdiction `law_version` + same union recognition status); a law edit
  reprojects and the next call re-samples deliberately — never a silent
  redraw. `normalize_weddings` backfills the `quotes` envelope on old
  saves.
- `GameService.wedding_quote(game_id, union_id)` — locked, `_GameRNG`-
  wrapped, persists the plan; returned shape (published in the contracts
  README Open items for B): `{"union_id", "status", "jurisdiction_id",
  "law_version", "policy_revision", "lines": [{occurrence_id, category,
  amount, leg, description}], "total"}`.
- `plan_wedding` posts EXACTLY the stored quote lines (preview total ==
  posted total, always) through the existing per-union+consequence
  occurrence keys; parts + quote total pass the affordability gate; an
  unknown law category stays a typed `invalid_input`.
- Tests (all fail without their fix): `test_q1_variable_range...`
  ([500,2000] sampled once, stored, never redrawn, posted exactly),
  `test_q1_multiple_fines_all_lines_posted` ($500+$700 both lines),
  `test_q1_quote_affordability_and_replay` (typed rejection with no
  posting; stored quote posted on retry; replay adds nothing),
  `test_q1_law_edit_requotes_deliberately` (law_version bump → fresh
  quote under the new law). Round2+weddings suites green under
  PYTHONHASHSEED 0/1/42.

## L-round additions (round 6, same day)

**L1 — the forbidden-union ceremony law fine is forecast but never posted
(FIXED).** `plan_wedding` now resolves the union's jurisdiction
(save-local overrides merged over the catalog, frozen
`resolve_jurisdiction`) and, for a `unrecognized`/`forbidden` union,
posts each `ceremony_of_unrecognized_union` / `civil_fine` consequence
ONCE through the ledger: the amount is sampled from the law's
`randint` range on the gameplay stream (`wedding_command` now runs
under `_GameRNG`, like every other mutation), the occurrence key is
`lawfine:<union_id>:<consequence index>` (per union + consequence, so
replays/retries never double-charge), the category is the law's
`ledger_category`, and the total (parts + fines) passes the existing
affordability gate. `"legal"` added to `finance.CATEGORIES` per the
frozen graph-contract note (A owns finance.py); an unknown category in
law data is a typed `invalid_input`, never a 500. Test:
`test_l1_forbidden_ceremony_law_fine_posted_once` — the exact
evaluation-round-5 reproduction (editor law edit → courtship_offer →
union_proposal with forbidden projection → free courthouse ceremony):
$500 posts exactly once, the ledger row is category `legal` on the
player, the same-request replay returns the stored response with no
second charge, and a new ceremony command is rejected (already held).
Fails without the posting (proven), passes with; round2+weddings suites
green under PYTHONHASHSEED 0/1/42.

## W-round additions (round-4 review, same day)

**W2 — a lost pregnancy's draw reused by the next pregnancy (FIXED).**
The coordinator now snapshots pregnancy flags around the family_events
phase; a pregnancy that ends there WITHOUT a newborn (miscarriage, both
carrier branches) retires the recorded draw through the new
`parenting.core.terminate_active_conception` — the outcome becomes
`consumed` + `terminated: "miscarriage"`, keeps its original
`pregnancy_id` for the audit trail, and the player's
`_active_conception_key` binding is cleared. The recorder only reuses
unconsumed, unterminated draws, so the next pregnancy records a NEW draw
with its own immutable identity. Test:
`test_w2_miscarriage_retires_draw_next_pregnancy_is_new` — real keep →
forced miscarriage (fertility 0, saved RNG seed 2, journal confirms) →
second keep: two records, the old terminated with `pregnancy_id=d1`,
the new unconsumed with `pregnancy_id=d5`. Fails without the
coordinator retirement (proven).

**W1 — two inheritances collide and crash year advancement (A half
DONE + C fix verified).** C landed the deceased-id occurrence id
(`"inheritance:{_pid(relation)}"`, rule_actions.py:2650) — the real
double-death probe now returns 200 with two decedent-keyed, conserved
inheritance rows. A added the strictness-preserving safety net:
`LedgerConflict` inside command execution maps to a typed 409
`ledger_conflict` (save byte-unchanged) instead of an unhandled 500 —
proven by restoring the colliding key in a scratch edit (409, unchanged
save). Test: `test_w1_two_inheritances_same_turn_are_typed_never_500`
accepts the healthy 200 (asserting the inheritance rows) and the typed
4xx branch. An Open item asks C to audit other per-decedent mortality
money entries for the same class.

## N-round additions (evaluation after E-round, same day)

**N1 — conception recording wrong for both player-gender branches (CODEX's
fix, preserved by A per the round-4 split).** Codex edited
`service._resolve_rule_pending` (pregnancy-presence condition covering
the male-player branch), `parenting.core.record_legacy_conception`
(Gender-enum mapping) and added `tests/test_game_m25_n1.py` (real keep
replay/reload/birth ordered roles, overruled-abort and termination
recording no conception). A's N2 recorder rework kept both narrow fixes
intact; A's earlier N1 tests in test_game_m25_conception remain as
ordered-role guards.

**N2 — already-consumed children steal the next pregnancy's draw (FIXED).**
- `find_conception_outcome` now STOPS when the child already consumed an
  outcome (any `consumed_child_id` == this child → None) — a repeated
  `consume_conception_outcome` for child A can no longer take child B's
  pending draw.
- Immutable identity over pair/insertion order: `record_legacy_conception`
  stores the pregnancy pending id in the outcome and binds the storage
  key on the player (`_active_conception_key`, pickled with the player);
  `apply_birth_conception` consumes THAT key when valid for the child's
  bound parents, then clears it. Reusing a pending draw rebinds it to
  the current pregnancy instead of silently returning.
- Test: `test_n2_reconsume_never_steals_next_pregnancy_draw` — real keep
  → real birth → second keep → repeated consumption for the first child
  returns None and the second draw stays unconsumed and childless. Fails
  without the stop-guard (proven).

**N3 — graph divorce leaves the legacy view married (FIXED).**
`sync_legacy_partner` now treats UNIONS as the married authority: when
the current partner has no active union but a surviving courtship/
committed edge, the partner is kept but DOWNGRADED to dating (status 0)
— the married presentation dies with the union, so a fresh GameService
shows `partner_status != 2` and never advertises the legacy Divorce
action. No active edge of any kind still clears the partner entirely.
Test: `test_n3_graph_divorce_downgrades_legacy_married_view` — real
`married_30` → public `wedding_command(divorce_union)` → fresh service:
no married status, no Divorce action, union divorced. Fails without the
downgrade branch (proven).

Verification: 19 conception+partner-sync tests OK under PYTHONHASHSEED
0/1/42; `py_compile`/`git diff --check` clean; full discovery suite run
after the changes.

## E-round additions (Codex evaluation, same day)

**Appearance flake — `test_all_person_subclasses_get_appearances` (FIXED).**
Intermittent full-suite failure reproduced analytically: `inherit_appearance`
could produce TWO novel physical traits (an in-between `skin_tone` plus the
10% hair-drift mutation) in ~3.9% of draws, violating the design invariant
the test asserts (a biological child differs from BOTH parents in at most
one physical trait). Whether the failing draw occurs depends on the shared
`_APP_RNG` stream state, hence suite-order flakiness (it passed in two
full-suite runs here, and Codex saw it fail). Fix in
`src/appearance/generate.py`: the hair mutation is skipped when the skin
tone already landed in-between the parents' tones — one novel-trait budget
per child. Verified 0/20000 violations across parent combos while mutation
still fires (~4.6% of non-novel-skin draws); `test_appearance` 51 tests OK
under PYTHONHASHSEED 0/1/42.

**E4 — `try_for_baby` → keep records no conception (FIXED).**
`parenting/core.py` gains `record_legacy_conception(env, player, rng)`:
a continuing legacy pregnancy rolls ONCE, keyed
`legacy:<mother>:<father>#n`, storing the bound bio parents; an existing
unconsumed draw for the pair is reused (never rerolled); ineligible
pairs (no partner, underage, same person) return None; adoption never
routes here. Production seam (A-owned, per Claude's note): the
`_RESOLVER_KINDS` dispatch seam in `resolve_choice` was MOVED to
`service._resolve_rule_pending` after `resolve_choice` returns
(kind `pregnancy` + `is_pregnant`), inside `_GameRNG` — C's
`rule_actions.py` carries no A hook, and Codex's 18+ entry guard stays
first in `resolve_pregnancy` before any effect or RNG.
Test: `test_try_for_baby_keep_records_conception_consumed_at_birth` —
real try_for_baby command → keep resolve → exactly one pair-keyed
outcome with the correct bio parents, no second draw, and a real
`age_up` birth consuming it with `consumed_child_id` set.

**E5 — same-parent conceptions swapped between children (FIXED).**
`find_conception_outcome` is now two-pass: (1) the child's OWN pre-bound
outcome (service `child:request` keys) always wins; (2) pair-keyed
outcomes (legacy/v3, recorded before the child existed) are consumed
first-come-first-served in insertion order, and any key prefixed by a
DIFFERENT child's id is skipped. Tests:
`test_sibling_conceptions_never_swap` (two same-parent pre-bound draws,
each child resolves its own) and
`test_two_legacy_pregnancies_births_consume_distinct_draws` (two keeps →
two draws, no reroll while a draw is pending, each birth consumes its
own — birth #1 through the full yearly production path; the second
through the same applier the coordinator calls, because the miscarriage
draw makes a second full year non-deterministic).
Failing-without-fix verified for E4 and E5 (regressions fail, then
restored).

**E3 — married_30 scenario partner never migrated (FIXED).**
Root cause: scenario starts walk the player up from childhood across
loads; the FIRST load (partner absent) stamped `migration_version`, so
the later-arriving married partner never migrated — no graph union,
Weddings empty, the spouse selectable as a dating-app stranger. Fix in
`service._read_save`: when the marker is already set but the player has
a MARRIED adult partner with no graph union edge, rerun `migrate_v2`
(idempotent merge; a post-divorce game has partner None via the legacy
sync, so the A1 "no divorce retrigger" guarantee holds).
Test: `test_scenario_married_partner_migrates_to_union` — real
`married_30` game: exactly one active marriage union, a relationship
edge, the dating stranger predicate never returns the spouse, and a
second load adds nothing. Failing-without-fix verified (union absent
with the branch disabled).

Also repaired the R5 projection test
(`test_union_without_legacy_partner_gains_projection`) to assert the
real load-path behavior (`_read_save` repairs and persists a wiped
legacy presentation) instead of the contradictory private-read timing.

(Original R8/R4/R5/R9 content below.)

Scope from `M2.5-fix-round-2.md`: R8 (conception→birth→health), R9
(secrecy integration), R4 A half (pending supersession for personal
edits), R5 A half (graph/legacy partner sync), plus A's suite hygiene.
Branch `dev`. Much of the R8/R4/R5 implementation landed before the
quota restart; this session verified, completed and regression-hardened
each item. Files touched here: `src/game/features/knowledge/effects.py`,
`tests/test_game_m25_round2.py`, `tests/test_game_m25_secrecy.py`,
`tests/test_game_m25_partner_sync.py`.

## R8 — one immutable conception identity, consumed at birth (VERIFIED + tests green)

Already integrated in the tree; verified end to end:
- `GameService.record_conception` binds ONE conception per child:
  an existing bound outcome wins BEFORE any RNG use (retry/new-request of
  the same parents returns the stored draw; a different parent pair is a
  typed `graph_error` conflict, never a reroll). Biological parentage
  edges are recorded at conception; both parents re-validated 18+.
- `parenting.core.find_conception_outcome` resolves the stored outcome by
  the child's bound biological parents (graph edges first, legacy attrs
  fallback), whatever key shape stored it; never redraws.
- `parenting.core.apply_birth_conception` is called in the SAME
  transaction as the birth (`coordinator.py` family_events phase
  snapshots children before/after; a newborn consumes its recorded
  outcome immediately). The bounded fictional health consequence
  (`related_parent_health_risk`) applies at most once (`consumed` flag +
  `consumed_child_id`); adoption returns None and never rolls.
- `consume_conception_outcome` service seam kept for other birth paths.
- Tests: `tests/test_game_m25_conception.py` (6) — once-only, idempotent
  retry (RNG state equality), changed-parent conflict, adoption
  exclusion, reload stability. Codex's reported issues (missing `Child`
  import, child-bound unknown person) are resolved — suite green.

## R4 A half — `_editor_supersede` cancels the actual stored decision (VERIFIED + forced regressions)

- `service._editor_supersede` now supersedes pending GRAPH offers across
  all ROMANTIC_KINDS (not just four) AND the actual `env.pending` when
  `_pending_superseded` matches: romantic interaction kind (incl. legacy
  `date_offer` → `date`) touching an affected person (bindings, stored
  partner/target ids). The cancelled offer resolves to an explicit,
  journal-visible "the moment has passed" outcome; attempts/cooldowns/
  pre-effects are preserved elsewhere; supersession never resamples and
  never invents consent; historical consent records are untouched.
- New forced regressions (`tests/test_game_m25_round2.py`):
  - `test_personal_edit_supersedes_real_dating_pending` — a REAL
    `dating_app` pending through HTTP, then an editor apply on the
    TARGET's openness → pending cancelled, journal line, revision bump,
    and `age_up` still works (no soft-lock).
  - `test_edit_of_unrelated_person_keeps_dating_pending` — an edit on a
    person outside the offer leaves the pending AND its stored
    refusal/score draw byte-identical.
- **Failing-without-fix verified**: disabling the pending-cancellation
  branch makes the first regression fail (pending survives); restored
  and re-green.

## R5 A half — legacy single-partner view derives from the graph (VERIFIED + test repair)

- `transitions.sync_legacy_partner` (idempotent, presentation-only):
  graph divorce/dissolve/widow of the ONLY active edge clears
  `player.partner`; selected divorce among plural unions re-points the
  legacy view at a remaining active union's partner; a pure-legacy
  partner with no graph edges is left untouched (legacy authority until
  an edge exists); a player with no legacy partner gains the married
  projection when an active union exists. Called at load (`_read_save`,
  persists once when it changes) and after graph/wedding commands.
- Repaired `test_union_without_legacy_partner_gains_projection`: the
  test asserted a private raw read shows the wiped value, but `_read_save`
  IS the load path and repairs + persists the projection — the test now
  asserts that real behavior (wipe → read → projection restored with
  correct partner id and status 2). All 5 partner-sync tests green.

## R9 — secrecy integration (COMPLETED this session)

Already in tree: `get_family_tree` passes
`knowledge.known_secret_relationship_ids(env)` (fact-backed observer
knowledge, not world truth) into `graph_tree`; `reveal_fact` dedupes per
occurrence and applies reputation with provenance.

Completed here:
- `knowledge/effects.py::_reveal_fact` — the `affair` reveal path now
  establishes the fact through `drama.detect_affair` (the
  agreement-aware production seam: mints only on a real
  exclusive-agreement breach, returns None for consensual open/plural),
  bound to the stored occurrence id; `reveal_fact` then reuses that fact
  (one immutable fact per occurrence — never two mints). The typed
  `not_allowed` gate for breach-free occurrences is unchanged.
- Tests (`tests/test_game_m25_secrecy.py`): the disclosure-through-HTTP
  test now asserts EXACTLY ONE affair fact after the real resolve; new
  `test_replayed_reveal_never_mints_a_second_fact` retries the identical
  command through the replay ledger and asserts the fact count stays 1.
  Secret edge hidden → disclosed → visible across reload, consensual
  plural never an affair, unknown fact never in the tree: all green.

## Verification

- A suites: round2 (incl. 2 new) + secrecy (5) + partner_sync (5) +
  conception (6) + codex_fixes (17) + family_graph core/service +
  weddings + knowledge = **127 tests OK**.
- PYTHONHASHSEED 0/1/42 on the four M2.5 round-2 files: OK ×3.
- `py_compile` clean, `git diff --check` clean.
- Full discovery suite: run after these changes (see repo state at
  Codex's re-evaluation; other panes were editing concurrently).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real GameService transactions throughout (real `dating_app` HTTP
  command, real editor applies, real graph commands); no mocks.
- [x] Failing-without-fix: R4 pending-cancellation branch disabled →
  regression fails (shown above, then restored). R9 single-fact and
  R5 projection assertions fail against the pre-integration shapes by
  construction.
- [x] Randomness: no new draws; conception RNG consumption unchanged
  (stored draw wins before RNG use); reveal path adds no sampling.
- [x] Inputs validated: supersession works from the hook's affected-id
  filter; no new caller-facing surface beyond the existing typed seams.
- [x] Money: untouched in these items.
- [x] Identity: person_ids everywhere; pending bindings by id.
- [x] Save/load: supersession, sync and consumed flags persist through
  the envelope save; reload stability asserted in every new test.
- [x] Costs/eligibility: no new costs.
- [x] Age/state gates: conception parents re-checked 18+ at record;
  adult guards on graph transitions unchanged.
- [x] No soft-locks: superseded pending leaves an explicit journal
  outcome and `age_up` proceeds (asserted); replayed reveals answer from
  the ledger.

Hygiene
- [x] Only A-owned files touched; shared files re-read before editing;
  B/C files untouched.
- [x] No probability/balance changes.
- [x] Focused suites green under 3 hash seeds; no new skips.
- [x] UI: no UI changes in this round (B owns the screens).
- [x] Rare paths forced (real dating pending, replay ledger, wiped
  projection, pre-bound secret edge) — no reliance on random rolls.
