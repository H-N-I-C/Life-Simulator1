# M3b B1/B2 correction re-review — Codex, 2026-11-21

Verdict: **APPROVED for the delivered B1/B2 correction round; ready for Claude's final evaluation.** This is the single combined re-review of A's yearly work/study seam, C's independent route tests, and D's lifecycle journeys requested in `codex-m3b-consolidated-review.md`. It does not assert that every future M3c content/balance goal is complete.

## B1 — production reachability corrected

Amendment 4 defines the new `workplace_events` slot after education and before debt service. Existing phases keep their relative order. The route uses the existing v3 binder, condition evaluator, instantiator and shared resolver. Active named employment gates `wrk_`; active enrollment gates `stu_`; an apprentice can receive both families in one year through saved phase re-entry. Record history, family markers, pending state and RNG all live inside the existing save/undo/replay boundary.

A's forced real-command tests verify both families, source/role rejection, actual outcomes, reload, retry and undo. C's separately delivered `tests/test_rules_m3_route.py` verifies the route independently, including multi-year ladder/program gating and cash outcomes. No sampler substitute or new interpreter is needed.

Independent actual Chromium at 320 pixels also passes: real `age_up` → workplace card → click Attend → actual outcome → Continue → study card → click choice → actual outcome. Both families persist in the same year. Screenshots and the runnable probe are in `evidence/M3/codex-m3b-rereview/`.

## B2 — lifecycle evidence corrected

Independent revised journeys: **27 PASS / 0 PENDING / 0 FAIL**, including the final strengthened recovery assertion.

- Legacy save: strip additive M3 fields, resume without inventing employers/credentials, retain single payroll, then accept a valid legacy-to-named stored offer under Amendment 2.
- Employer change: real resign → closed assignment/history → eligible other-employer application → stored offer → acceptance → new employer and synchronized payroll terms.
- Dismissal: force the actual legacy low-performance firing branch, verify closed named assignment and `dismissed` history, then accept a real recovery offer and prove employment and legal aging resume.
- Retirement: named employment through direct retirement and a real yearly retirement offer. Both close the assignment with `retired`, preserve the pension and post exactly one pension-income leg in the next applicable year.
- Reload: compare existing `work.pension`, retirement state, career history/assignment and ledger row identities before/after a fresh page load. No invented pension projection or player-facing Fire command.
- Scenario creation now uses a unique request ID per start; retirement setup no longer accidentally reuses an earlier life through creation replay.

## Small fixes completed by Codex

Claude was notified of the exact file scopes before edits.

1. `tools/m2_assertions.py`: synchronize the independent accepted-phase list with the frozen order, including workplace and the existing M2.5 phases. Preserve the pure validator. Its frozen-fixture self-test passes; explicit canonical parity and a pending `workplace_events` cursor check pass.
2. `src/game/service.py` + `tests/test_game_m2a.py`: clear only `wrk_stu_seen` after successful succession. The list is once per **life**, not once per save. The real surrender/continue-as identity/cash regression was extended with a prior-life seen list; it failed before the fix and passes afterward. Existing journal/archive, finances and identity behavior are preserved. Service import check passed.
3. `tests/browser/m3_journeys.py`: require a dismissal recovery application to succeed, produce a stored offer and be accepted into actual employment. Previously errors other than “already have a job” could be ignored. Absence of a reachable recovery opening now fails; a legal age-up alone no longer stands in for recovery. The strengthened full journey still passes 27/27.
4. `tests/test_rules_m3_route.py`: force the cash-choice record through the real persisted seen-history field. The original four-year money test sampled storm shutdown, promotion whisper, shift swap and broken lift, all with `money=None`, so its money assertion never executed. The final test must offer overtime and assert exactly one `$120` ledger entry. No production/content changes were needed.

## Validation

- Integration + real succession + unchanged normalized-life seed regression: **43 passed** after the per-life fix.
- C's final route + gate tests: **12 passed**. C's test file arrived after full-suite collection, so it was independently run rather than assumed included.
- Final C route tests under `PYTHONHASHSEED=0`, `1`, `42`: **7 passed** each, including the forced money choice.
- Alternate-seed route + succession check: **8 passed**.
- Independent strengthened browser/HTTP lifecycle journeys: **27 PASS**, no pending/skipped/failed steps.
- Actual work/study modal browser sequence at 320: **PASS**, no horizontal document overflow.
- QA assertion self-test, phase-order parity, pending cursor acceptance, service import and `git diff --check`: passed.
- Full repository suite: **995 passed, 9 skipped, 117 subtests passed in 622.14 seconds**. It began before the small Codex corrections and C's new test file; their affected paths are covered by the targeted and browser checks above. No full-suite failures remain.
- No commits. `tests/test_skills.py` remains unchanged.

## Keep and defer

Keep the shared v3 pipeline, explicit cadence, sole money poster, stored offers, pending/replay/undo behavior, immutable identities, verified employment baseline, retirement fixes and approved UI polish. No new content, APIs, dismissal button, salary authority or broad refactor is required for B1/B2.

The covered lifecycle journeys use explicit QA state fixtures for rare branches and prerequisite setup; they do not claim a long unassisted playthrough of every ladder. Future M3c balance/content/reachability expansion remains separate from accepting these delivered corrections. Preserve existing saved values and the previously documented default-talent variety limitation.
