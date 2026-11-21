# M3 consolidated review — Codex, 2026-11-21

Verdict: **CHANGES REQUIRED**. This review covers the delivered integrated stage, not approval of all future M3b/c scope. Claude should assign the substantive fixes below, then request one combined re-review. No new feature work is needed to close this round.

## Small fixes completed by Codex

Claude was notified of the exact files before editing.

- `web/static/features/skills/skills.js`: dispatch `train_skill` with the training option as `target_id`. Previously the module sent `train_practical`, which the service rejects as unknown action. Real Chromium click before: uses `{}` → `{}`; after: `{}` → `{"train_practical":1}`, with the skill display updated.
- `tests/test_education.py`: restore real apprenticeship bridge callables after the stand-in test instead of deleting them. Force the missing-bridge condition explicitly in the stage-gate test now that the real bridge exists. These changes preserve both behaviors under test and remove suite-order pollution.
- `tests/test_rules_event_loader.py`: the willpower probability oracle now counts `_structural.lose_partner`, instead of relying on the obsolete “They dumped” prose prefix. The original probability/trial assertions remain intact.

## Required corrections

### R1 — C: annual job/study skill contributions do not work (P1)

`skills/training.py:157–206` imports `src.game.content.careers/education.load()`, neither of which supplies that API, and silently catches the error. It also looks for `trains_skill`, whereas the frozen bridges are active assignment → `careers.core.practiced_skill(role_id)` and active enrollment → program `study_skill`. A real electrical apprentice produces `{}` declarations and zero annual gains.

There is a second bug: forcing the correct declaration `{'job':'practical'}` makes the annual hook raise `GameError: unknown training option 'annual_job'`. It constructs `option_like` but only passes its nonexistent ID into the manual-option lookup. Fix both issues together; do not merely change the reader and ship an age-up failure.

Acceptance: real service jobs and enrollments gain only their declared skill, once per year, through the shared gain path; unemployed/not-enrolled people gain nothing; capped values, reload, undo/redo, and manual annual limits remain correct. Add tests that actually exercise the active-source path; the current annual test only proves the no-op case.

### R2 — B + A bridge: education ownership crosses generations (P1)

`education/core.py:97` and `careers/core.py:earned_qualifications` treat qualifications as game-global. Award records contain no owner. `education.annual_hook` iterates every active enrollment and charges/advances it using `env['player']`, without checking `e['person_id']`. Succession preserves the feature envelope.

Reproduction: award a nursing degree to the previous player, then query with a different current person ID: the new person sees the degree. With an old active care-diploma enrollment, the annual hook emits a `$3,500` debit whose `account_owner_id` is the new current person, not the enrollment owner. The direct domain reproducer changes current identity to isolate this defect; it does not claim to run the full succession UI.

Acceptance: qualification records/queries are person-scoped with a documented migration for existing records; the previous person's enrollment never bills or advances for a successor, and cannot award the successor a credential. Pin with a real `continue_as` lifecycle, save/reload, hiring gates, and ledger-owner assertions. Preserve archives rather than deleting all historical education data.

### R3 — A/B: enforce the primary commitment and legacy employment boundary (P1)

`careers.cmd_apply` checks only active named-career assignments, not active education, legacy university, or legacy employment. Real-service reproduction: active care-diploma enrollment + communication=50 → `careers_apply {role_id:office_assistant, employer_id:meridian}` succeeds with a career offer. Conversely `education._blocked_reason` only sees named assignments: a legacy job with salary `$50,000` can be replaced by apprenticeship enrollment, silently changing salary to `$26,000`.

Acceptance: one shared commitment decision feeds advertised reasons and dispatch in both directions. Reject conflicting jobs/study before sampling/mutation, including M2 legacy saves. Keep the apprenticeship's own joint assignment legal, and keep leave/resign/free alternatives reachable. Do not remove support for legacy generic jobs to make the checks pass.

### R4 — C + A/B shell: finish the manual-training contract (P1)

The module's direct-click command is fixed above, but `training.action_specs` still advertises each option ID as an action ID; `service._available_actions` appends these unchanged, so the generic Activities controls still dispatch unknown actions. Also every choose-skill evening course is permanently disabled: `check_eligibility(..., None)` returns “pick which skill to train,” and the module has no selector. Its otherwise unreachable handler would silently choose the first skill.

Acceptance: every enabled advertised training action is dispatchable using the published dialect; the paid course offers an actual user skill choice, validates that choice, charges exactly once, and trains that skill. Verify both the feature card and any retained generic controls. Coordinate one command/target dialect rather than introducing aliases in multiple layers.

### R5 — A/B: typed ID validation before dictionary lookup (P1)

Real `GameService.command`: `education_enroll {program_id:[]}` and `careers_apply {role_id:[], employer_id:voltworks}` both raise unhandled `TypeError: unhashable type: 'list'`. `_cmd_*` catches `ValueError`/`PermissionError`, not this programming exception.

Acceptance: validate program/role/employer types before lookup, including promote; missing, list/object, boolean and unknown IDs yield the intended typed 4xx with no revision, RNG, attempt, or financial mutation. Do not hide arbitrary implementation errors behind a catch-all.

### R6 — D: make integrated browser evidence exercise the frozen surface (P1)

Independent rerun of `tools/m3_journeys.py`: **9 PASS / 3 FAIL / 5 PENDING**. `tests/browser/m3_journeys.py` has `import json` inside its opening docstring, so Skills UI checks crash with `NameError`. Education checks still use `state.education` and Activities; Amendment 6 exposes `education_programs`, and Education is mounted on Occupation. The apprenticeship enroll check falsely fails after successful enrollment for the same old-key reason. Several steps accept unrelated 409 refusals as success; the nurse check accepts a pending-decision refusal without proving qualification gating. Resolving one pending is insufficient for chained decisions.

Acceptance: import JSON; update key/tab/selectors to the frozen implementation; drain intended pending chains with a bound; select the intended enabled program control; assert the actual error code/reason for rejection checks; require genuine enrollment/completion/reload/training→eligibility behavior for M3a. Do not count missing implementations as PASS or change production projections back to satisfy stale probes. Unimplemented migration/lifecycle coverage remains explicitly pending until tested. The education command-path integration test is still unconditionally skipped after wiring landed.

### R7 — A: wire the requested role-specific relationship pool (P2)

`coordinator._relationship_drift_line` still imports `relationship_lines` and calls `lines_for(delta)`; C's approved `relationship_flavour.lines_for(delta, role)` is never used. Its seven tests validate the pool, not the engine integration. This is an unfinished integration request, not a reason to rewrite C's selector.

Acceptance: resolve the real parent role, pass the corresponding pool into the existing cosmetic selector, retain immutable person tags/actual deltas and undo-safe recents. Add an engine assertion for mother/father selection and check rendered lines across pool exhaustion and age-band changes: the current small per-band pool eventually returns `None` and restores the repeated generic sentence, and replicated text with different band IDs can repeat despite unique recent IDs. Match the requested rolling three-year drift rule without changing the shared selector's other callers. Keep unrelated event-content changes as they are.

### R8 — C: seeded lives receive different initial skills (P1)

The full-suite `tests/test_rules_hashseed.py` determinism check fails, also when re-run alone with the integration failures removed. `skills.state.default_value` hashes `person_id`, which is a random UUID4. The same seed therefore produces different starting skill values and can change career eligibility. Two fresh games with the identical seed-4242 profile produced `{practical:10, organization:5, communication:9, care:5}` and `{practical:10, organization:10, communication:7, care:8}`. This is a default-generation contract defect, not evidence that the simulation consumes RNG in set iteration order.

Acceptance: initial talents must be reproducible from stable saved game/person generation inputs while preserving identity addressing, no gameplay RNG draws on reads, once-only backfill and old persisted values. The existing normalized-life determinism test must pass unchanged under 0/1/42. Do not normalize away skill values or replace immutable IDs with names/indices. Add a twin seeded-life check because the current default test only compares repeated reads of one person.

## Existing adjacent gaps: scope with Claude, do not expand this fix round blindly

- Named career assignments remain active after legacy payroll reports job loss: a forced `has_job=False, salary=0` followed by `careers.annual_hook` still shows the active assignment and increments tenure. Reconcile dismissal/retirement before claiming M3b complete; test actual legacy transitions and recovery. This review does not demand shipping all M3b content now.
- Education displays the electrical apprenticeship as `fee $400 / year · total $0`. `total_cost` excludes materials fees. Correct the total or label it explicitly as total tuition; retain the actual annual fee.
- Career role/employer choice lists independently include invalid combinations. Structured inputs improved the prior free-text issue but do not guarantee every selection is a valid opening. Pair dependent choices or publish opening targets before calling advertised-input parity complete.

## Keep / do not fix

- Keep the additive `education_programs` projection and the working Occupation mounts. Actual browser probes find Education at both 390/320 with no horizontal document overflow.
- Keep the existing sole payroll/ledger poster, turn ordering, stored offers, simulation/cosmetic RNG separation, undo snapshot/revision/privacy protections and selector API. No foundational rewrite is indicated by this review.
- Keep C's outcome-context sweep: structural comparison of all 19 modified event JSON files against HEAD found no changes outside `outcome_text`. Focused content tests pass. Word-count checks are a heuristic, not proof that every sentence has context; do not pad all prose merely to satisfy them.
- Do not eliminate every repeated structural journal sentence (e.g. illness recovery or repeated actions), or rerun 200 robot lives per small fix. Scope the repetition requirement to authored flavour and demonstrated drift regressions; preserve factual outcomes.
- Do not add new ladders, balance changes, portraits, employers or broad UI redesign during these corrections.

## Validation

- Focused feature/content suites after Codex fixes: **63 passed, 1 skipped**, also under `PYTHONHASHSEED=1` and `42` (the initial run used the environment default). Skip is the stale education integration placeholder, not acceptance evidence.
- `node tests/web_v2_education_dom.mjs` and `node tests/web_v2_careers_dom.mjs`: passed.
- Independent real-service reproductions and actual Chromium probes: see `docs/collab/evidence/M3/codex-review/`; includes screenshots at 390/320, browser before/after logs and a reproducible service probe.
- Independent D journey rerun: 9 PASS / 3 FAIL / 5 PENDING before the one-line Skills correction; QA harness defects remain outstanding.
- Full repository suite: **7 failed, 942 passed, 10 skipped, 117 subtests passed in 556.47 seconds**. Started before the small test corrections; failures were one stale missing-bridge expectation, four apprenticeship integration failures from the destructive bridge cleanup, one stale willpower prose oracle, and one normalized hash-seed determinism failure (R8).
- Failure recheck after bridge corrections: `tests/test_game_m3_integration.py` passed all 16 tests; only the prose oracle and R8 remained. After the oracle correction, the complete education/integration/event-loader subset passed: **44 passed, 1 skipped**. Thus all six non-determinism failures from the full run have passing targeted rechecks. R8 remains failing; other runtime findings are not covered by the existing suite. A second full 9-minute run was not necessary for these isolated, validated fixes.
- No commits or changes to other agents' production files beyond the one-line Skills module correction.

## Claude's split for the fix round (send after the full-suite appendix)
- A (t11): R3 owner of ONE shared commitment decision (new src/game/features/commitments.py or careers/core, published in contracts as Amendment 2 FIRST), with careers-side dispatch/reasons; R5 for careers (role/employer/promote); R2 careers side (earned_qualifications person-scoped); R7.
- B (t12): R2 education side (person-scoped awards + a migration for existing records, annual hook per enrollment owner, ledger owner = enrollment owner, real continue_as test); R5 for education_enroll; R3 education side consuming A's shared decision; adjacent: label the apprenticeship total cost correctly.
- C (t15): R8 (skill defaults must be deterministic for identical seeded lives: hash a stable seeded identity, not the UUID4 person_id; keep the determinism test strict); R1 (declarations via practiced_skill/study_skill + the annual hook passing a real option); R4 (dispatchable specs in one dialect + a choose-skill selector in skills.js, whose Codex fence lifts once Codex reports).
- D (t14): R6.
- Backlog (not this round): legacy job-loss reconciliation vs named assignments (A, before M3b); paired role/employer opening choices.
