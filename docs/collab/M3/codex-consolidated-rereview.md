# M3 correction re-review — Codex, 2026-11-21

Verdict: **APPROVED for the delivered M3a skills/education stage; ready for Claude's final evaluation.** This is the single combined re-review of all four owners' R1–R8 corrections from `codex-consolidated-review.md`. Approval does not cover the whole M3 milestone or the deferred scope below.

## Findings disposition

| Finding | Re-review result |
|---|---|
| R1 annual skill gains | Active assignment/program bridges now select the declared skill; annual synthetic options reach the shared applier. Real-service job/study, bounded gain, reload and once-per-year tests pass. |
| R2 education ownership | Qualifications carry owners; migration stamps ownerless records; career queries filter owners; annual education ignores previous lives' enrollments. The real `continue_as` test completes the parent's diploma, leaves nursing enrollment open, and proves the successor receives neither its tuition nor its credential. |
| R3 commitments | Both features consume the shared refusal function and the approved Amendment 2 table. Named/legacy employment blocks study/apprenticeship without overwriting pay. Codex also corrected the remaining generic Enroll action-spec mismatch; see below. M2 university/school-to-job exceptions are explicitly approved in the table. |
| R4 manual training | Advertised actions use `train_skill` + `target_id`; the feature offers a real inline skill chooser. Actual Chromium at 320: select Care, value 9→18, usage 0→1, exactly one $60 training ledger debit, no horizontal document overflow. |
| R5 invalid inputs | Typed ID matrices and real command tests pass before mutation, including education, career application/promotion and apprenticeship bridge. |
| R6 QA | Correct JSON import, frozen education key/tab, bounded pending drain, targeted UI controls, exact rejection checks, real completion/reload/award and single-payroll assertions. Codex additionally made the normal Skills journey prove an actual opening becomes eligible, rather than hiding this check in a skipped fallback. Legacy-save browser migration remains explicitly pending. |
| R7 relationship drift | Real role pools and per-person recents are wired. Codex fixed the remaining age-band alias repeat and added a failing-before regression. Existing selector, cosmetic RNG and undo behavior are preserved. |
| R8 determinism | Same-seed UUID-normalized life test passes unchanged across 0/1/42. New defaults use a persisted name/gender seed; existing saved values are preserved and person records remain keyed by immutable ID. See the variety limitation below. |

## Small corrections completed by Codex during re-review

Claude was notified of each exact file scope before editing.

- `src/game/coordinator.py`: exclude all age-band aliases of recently selected relationship text, not just that band's IDs. Actual failing-before reproduction: cosmetic seed 0, ages 4→5 repeated “I called Mum just to chat more often, and it made us closer.” The original shared selector is unchanged.
- `tests/test_relationship_lines.py`: seeded age-band transition regression using real Player/Parent objects and a real isolated cosmetic RNG. Nine tests pass.
- `tests/browser/m3_journeys.py`: prefer `target_id` over action ID when identifying training options. The always-executed J3 training step now requires an opening to change from ineligible to eligible while crossing its published practical requirement. The normal apprenticeship bridge path had skipped the older fallback that purported to test this.
- `src/game/features/education/core.py`: derive generic Enroll action availability/reason from the same program requirements and commitment gates as dispatch. The program cards already did so, but the generic action remained enabled for a legacy-employed person.
- `tests/test_education.py`: extend the existing legacy-job parity test to assert the generic action disables with the resign reason, then re-enables after leaving employment.

## Validation

- Final focused re-review: `test_education`, `test_game_m3_integration`, `test_relationship_lines`, `test_skills`, `test_rules_hashseed`: **82 passed**. Includes the unchanged three-hash-seed normalized-life regression.
- Additional `test_education` + `test_relationship_lines` checks under `PYTHONHASHSEED=1` and `42`: **32 passed** each.
- Feature DOM checks: Skills (8 checks), Education and Careers all passed.
- Independent revised browser journeys, including the strengthened J3 assertion: **17 PASS / 1 PENDING / 0 FAIL**. Pending is the unimplemented legacy-save migration journey, not a pass.
- Independent paid-course Chromium check at 320: **PASS**. Its initial probe incorrectly expected net cash to change only by tuition; this scenario also resumes payroll in the unfinished turn. The corrected probe asserts the distinct, single training ledger debit rather than treating unrelated payroll as a duplicate training payment.
- Full repository suite: **975 passed, 9 skipped, 117 subtests passed in 564.83 seconds**. It began before the small final Codex edits; their affected paths are independently covered by the final focused/DOM/browser checks above. No full-suite failures remain.
- `git diff --check` passes. No commits.

## Keep and defer

- Keep the corrected ledger/payroll boundary, stored sampling, command/replay validation, immutable identity keys, existing persisted skills, additive `education_programs`, role pool/selector separation and cosmetic RNG. No rewrite is warranted.
- The persisted skill seed is name/gender based: unrelated people with the same name/gender start with the same default skill vector, even in different games. This closes the seeded-reproducibility bug and does not merge their records or later training. Treat independent default-talent variety as a future design refinement using a stable per-person generation seed; do not reroll existing lives or weaken determinism tests.
- M3b dismissal/retirement synchronization, complete ladder/employer progression, and the legacy-save browser journey remain unapproved scope. A promotion rejection check and resignation are not evidence of successful promotion or dismissal recovery. Keep these limitations explicit for the next scoped stage.
- Do not expand this correction round with balance changes, new jobs/art, broad UI redesign, or a ban on repeated factual journal outcomes.
