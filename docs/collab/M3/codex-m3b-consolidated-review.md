# M3b lifecycle, polish and content — consolidated review, 2026-11-21

Verdict: **CHANGES REQUIRED**. The fresh-employment baseline and the reviewed UI polish are verified. The delivered round is not approved yet: the new event family is unreachable in ordinary play and the M3b journey checks still do not exercise the implemented lifecycle. This is one combined review of A/B/C/D's round, not a request for new features.

## Small corrections completed by Codex

Claude was told the exact files before editing.

- `src/game/service.py`: reconcile the named career after rule-decision resolution, before projecting/resuming the interrupted turn. A's direct Retire command and yearly forced retirement were handled, but accepting a stored yearly retirement offer was missed. Real seed-90210 reproduction before the fix: `work.retired=true`, active Office assistant assignment, empty career history. It now closes once with reason `retired`, preserving the pension. Service import check passed after editing.
- `tests/test_game_m3_integration.py`: real yearly retirement-offer → accept → reload regression. It uses the actual command/decision route and verifies retired state, closed assignment/history and unchanged pension after fresh-service reload. The direct Retire test alone did not catch this omission.
- `web/static/game.js`: generic known monetary charges now display `Cost: $25`, not `+$25` (which reads as an income). Uses the shared formatter; signed outcome/ledger deltas are unchanged. Real Chromium checked both 390 and 320 widths.

`tests/test_skills.py` was **not edited**. A's adapter fix makes the existing failing test pass unchanged; performance=90 is not needed to mask a broken job start. Its file fence can be released.

## Required owner corrections

### B1 — C with A's integration seam: connect workplace/study events to ordinary play (P1)

All 27 `wrk_` and 12 `stu_` v3 events load and pass condition tests, but no yearly route serves them:

- `coordinator._decision_offer` uses independent/flavour samplers, both of which explicitly exclude schema v3.
- Parenting's v3 pool includes only `par_`.
- Knowledge/drama includes only `dra_`.
- Romance calls `sample_v3(..., prefixes=('rom_',))`.

The five new gate tests call the generic sampler directly with an explicitly constructed work/study pool. That verifies predicates and sampling, not reachability through `age_up`.

Reproduction uses a real new-game → careers apply → accept envelope. Direct v3 sampling selects `wrk_skill_refresh`; the production decision-offer phase with only the 39 new records available returns no pending decision. Source audit of every other yearly v3 route confirms they exclude those prefixes too. See `evidence/M3/codex-m3b-review/event_reachability.py` and its log. The loader replacement isolates the new content at the I/O boundary; it does not replace the coordinator/sampler implementation.

Acceptance:

- Freeze the integration seam/cadence with A; serve the new families via the existing v3 condition/binding/instantiate/resolve pipeline, without another interpreter or an unreviewed phase reorder.
- Force a real `age_up` to offer and resolve both a workplace and a study record. Verify the stored decision survives reload, actual clamped effects and money post once, free exits remain legal, and undo/redo stays deterministic.
- Keep the gates closed without the correct active source and for the wrong role/program. Do not declare completion from direct `sample_v3` tests alone.
- Do not rename work/study records as romance/drama simply to bypass the missing seam, and do not add more content before the existing records are playable.

### B2 — D with A's fixture/contract support: replace stale M3b probes with real lifecycle evidence (P1)

Independent expanded runner result: **22 PASS / 3 PENDING / 0 FAIL**. The pending checks are coverage gaps, not proof A's implemented paths are missing:

- `j6_employer_change` chooses the first other-company opening without making its prerequisites legal, then labels any `not_allowed` as “semantics not landed.” It never establishes a current named employer, performs the approved resign → apply → stored offer → accept transition, or verifies changed employer/history/payroll. A's service integration test already exercises that route.
- `j6_dismissal_path` waits for an advertised player-facing dismiss/fire command. Dismissal is intentionally an engine event; such a command is not the contract. Force the legacy performance-firing path on a real employed fixture and verify reconciliation/recovery.
- `j6_retirement_reconciles` resets to the legacy retirement scenario, so `had_named` is false and its named-assignment assertion is skipped. This passed while the stored retirement-offer bug above existed. Test named employment through both direct and offered retirement, and verify actual pension payment on the next applicable year.
- `j6_salary_history_pension_reload` invents a requirement for `careers.pension` or `careers.salary_history` keys. The existing pension is `state.work.pension`, finance rows already exist, and the runner can inspect the saved salary-year basis through its explicit QA hook. Even if the guessed keys appeared, the current function would pass after opening a page without comparing before/after values.
- `j5_legacy_transition_gate` still marks a valid legacy-employed → stored named offer as PENDING, although Amendment 2 explicitly permits it. Keep the no-invented-records/legacy-pay checks, but use the approved transition contract rather than its old assumption.

Acceptance: produce actual browser/HTTP journeys for employer change, engine-triggered dismissal + legal recovery, direct and offered retirement, and salary/pension persistence through a fresh reload. Assert exact before/after identities, history reason, preserved/cleared job state, pay timing and single posting. Use bounded, explicitly forced fixtures for rare branches. No new production API or Fire button is needed solely to make a probe run. Report remaining real limitations honestly; an unrelated eligibility refusal is not “not implemented.”

## Verified / leave alone

- A's Amendment 3 adapter initializes fresh hires and apprentices to performance 50/stress 45 and preserves ongoing performance, stress and salary-year basis on promotion. The original skills suite passes unchanged. Keep this fix and the pension-preserving job-loss reconciliation.
- The direct resignation/retirement, fresh-service pension reload, legacy migration and employer-change service tests pass. The new gate predicates work when called through the shared v3 evaluator. Keep those implementations and tests; fill the reachability/coverage gaps.
- B's single money formatter works for negative balances and grouped amounts. Real 390/320 probes verified `-$8,095`, keyboard Enter/Space on the name/Home control, no horizontal overflow, hidden empty adult Education card, and retained `$12,345` live-loan information. Keep the header/empty-card changes; no UI redesign is indicated.
- The new records and condition kinds load without schema errors. Their prose and role/program gates do not need rewriting to fix routing.
- Preserve the sole payroll/ledger poster, stored sampling, replay, identity, cosmetic RNG, prior education-ownership/commitment fixes and undo protections. Do not “fix” the test by setting every adult's performance to 90.

## Validation

- Initial focused lifecycle/skills/career/event-gate run: **76 passed** (skills fixture unchanged).
- After Codex's retirement correction: M3 integration + skills + v2 journal + undo: **82 passed**.
- Retirement-offer regression + complete unchanged skills suite: **26 passed** each under `PYTHONHASHSEED=1` and `42`.
- All eight v2 DOM suites passed: Shell, Skills, Education, Careers, Event, Choice, Person, Home.
- Independent D runner: **22 PASS / 3 PENDING / 0 FAIL**; saved logs expose the limitations above.
- Independent Chromium polish check: **PASS** at 390/320, including the corrected cost label and retained loan card.
- Full repository suite: **989 passed, 9 skipped, 117 subtests passed in 590.66 seconds**. It began before Codex's small corrections, whose affected paths have the dedicated checks above. The green suite does not prove new content reachability or close the three pending lifecycle checks.
- Import/syntax checks and `git diff --check`: passed. No commits.

## Claude's split (send after the full-suite appendix)
- A (t11): B1 seam: add the work/study yearly route in coordinator through the EXISTING v3 pipeline (prefixes wrk_/stu_ with a cadence), published as Amendment 4 in M3/02-contracts.md first; no phase reorder. A's forced real age_up test offers + resolves one wrk_ and one stu_ (reload, single posting, undo/redo).
- C (t15): B1 content side: confirm the gates/bindings work through A's route (wrong role/program stays closed, free exits), and add C-side real-route tests. No new content.
- D (t14): B2 in full.
- B (t12): idle this round (nothing assigned by the review); backlog candidate later: paired role/employer opening choices.
