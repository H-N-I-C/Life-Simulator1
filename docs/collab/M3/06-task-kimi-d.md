# M3 task proposal — D owns content tooling and independent integrated QA

Await Claude assignment and frozen M3 schemas. Own `tools/**`, `tests/browser/**`, QA tests under `tests/content/**` and `docs/collab/evidence/M3/**` after Claude approves that manifest. Do not implement other agents' features or alter production data to conceal failures.

## Traps before work

A schema-valid career graph can still be unreachable or unaffordable. A content counter can hide identical reskins. Testing exact values by calling the same helper as production proves little. Fixed-seed scenarios need valid education/debt/assignment state, not a bare age edit. Avatar job contexts may have no asset in a particular age/skin/headwear combination. Don't write test flows against draft event/feature shapes or mask errors from schema drift.

## Deliverables

1. One content validation command for careers/employers/programs/skills and **the frozen M2 event schema**. Check unique/stable IDs, referenced roles/programs/skills/assets, acyclic feasible promotion paths (or explicit valid loop rules), wage/cost bounds, free decision exits and supported predicate/effect vocabulary. Add tiny intentionally invalid fixtures with clear failures. No Python/eval conditions in authored content.
2. Reachability/cost report: all12 roles/4 programs/4 skills have at least one executable path; each prerequisite can be satisfied in a normal lifespan and without requiring a role that itself requires the resulting credential. Identify dead-end prerequisites, duplicate events and impossible promotion thresholds. Numeric validation alone cannot certify narrative quality; include human-readable summaries for Claude.
3. Extend canonical browser runner/scenario manifest for M3a then M3b: free skill path, apprenticeship, diploma/degree, named job, promotion, changing employer, rejection/dismissal, retirement and old-save resume. Include $0 and failed-course starts. Scenario factories belong to feature owners via A's registry; use real actions to prove progression after setup.
4. Independently audit one employed/training/study/apprenticeship year: integer cash delta equals ledger sum; tuition financing/principal credited once, debt serviced once, one wage source. Verify command retry/pending restart and progress/qualification awards once. Compare to explicit expected fixtures, not a call back into production's income calculator.
5. Visual/context evidence at40px/creator size: active supported job look, missing job-look fallback, job loss restoring personal look, headscarf/badges/skin variants, elder employee and temporary graduation context. Use the pinned M0b manifest/license/credit behavior; don't fetch unpinned extra artwork.
6. Phone/desktop keyboard/focus/overflow and structured-person references on all new feature screens; record real console/network failures. Keep waits bounded and assertion results boolean. Consolidate supported smoke flows rather than adding another runner.

Run focused tooling/browser tests as each feature lands. Publish `kimi-d-report.md` with actual coverage, counts/timings, failures and evidence; each issue names owner and reproducible journey. End with the full ticked 00-SELF-CHECK, with exceptions honest; update status and hc send t7. Codex does one consolidated review per complete testable stage and at most one fix re-review; full-suite review is Codex's job.
