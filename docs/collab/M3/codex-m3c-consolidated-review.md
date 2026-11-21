# M3c consolidated review — Codex, 2026-11-21

Verdict: **CHANGES REQUIRED**. Real-play progression, the Occupation layout and most journal changes are working, but the new alternative routes expose a historical-tenure bypass and promotions can create role/employer combinations excluded by the employer data. Alternative routes also need an honest requirement projection/UI. This is one combined review of A/B/C/D's M3c round.

## Required owner corrections

### C1 — A: minimum experience disappears after leaving the role (P1)

`careers.core.check_requirements._role_met` checks `min_role_tenure` only when the required role is CURRENT. For a past role, mere presence returns True. The comment that historical tenure is unavailable is incorrect: `_close_assignment` copies `role_tenure` into the history record.

Real-command reproduction (skills and care-diploma prerequisite are fixture setup, not experience grants): accept/resign Care assistant → accept Qualified care worker → age one real year and resolve its decisions. Current tenure is **1**. Nurse is correctly disabled because no nursing degree and the alternative requires **8** years. Resign: Nurse immediately becomes eligible; `careers_apply {opening:'nurse@bluegum'}` produces a stored career offer without another aged year or degree. Same pattern affects the office experience alternative.

Acceptance: read recorded tenure for both current and past assignments; freeze whether multiple stints accumulate or one qualifying stint is required, then enforce that policy consistently. Resignation, dismissal, employer change or taking another role must not manufacture experience. Pin below-threshold current→past transitions and a genuinely qualifying past role; missing/unknown legacy tenure cannot silently count as the minimum. Keep the OR-route semantics, shared age/skill checks, person identity, posting and stored-offer boundaries.

Reproducer and exact output: `evidence/M3/codex-m3c-review/repro_tenure.py` / `tenure.log`.

### C2 — A + D: promotions bypass the employer-role pairing authority (P1)

`cmd_promote` carries the current employer into the target offer but never checks whether the employer offers that role. The new paired-opening authority is derived from employer `role_ids` and explicitly promises 18 valid pairs.

An independent run of D's actual electrical ladder reaches **`site_supervisor@voltworks`**, which is not one of those pairs: Voltworks has apprentice/electrician/senior electrician; Site supervisor is at Brightspark. This is an actual command-produced assignment, not an injected fixture. The headline ladder test passes because it checks the top role alone. The same omission exists for the office/healthcare top steps.

Acceptance: make assignment/offer creation consistent with the frozen employer availability data. If a promotion is unavailable at the current employer, show the real reason and use the existing approved employer-change path rather than creating an unsupported role there. If internal-only positions are intended, freeze and publish that distinction explicitly instead of contradicting the “valid pair” list. D must assert employer-role validity throughout every ladder and adapt real play to move employers when necessary; reaching a role ID alone is insufficient. Do not fabricate history or widen every company's role list merely to preserve the existing passing trace.

Probe: `evidence/M3/codex-m3c-review/repro_employer.py` / `employer.log` (expected FAIL on the delivered implementation).

### C3 — A + B: show alternative routes as alternatives (P2)

The server returns only the MAIN qualification/role items in `requirements`; it publishes no alternative-route description or route-met status. The labelled “or” line exists only as a refusal reason, so it disappears when an alternative succeeds. The frontend renders each remaining main item as an ordinary requirement and ignores the provided credential label.

Example: an electrician opening with a business degree and sufficient practical skill returns `eligible:true, reason:null`, but its displayed requirements are “Electrical trade certificate: not met” and “apprentice experience: not met.” The degree route that actually admitted the player is absent. The eight-year healthcare and office degree routes have the same presentation gap. A player sees an enabled Apply next to apparently missing mandatory requirements.

Acceptance: publish server-computed named routes, their individual requirements/tenure and met status; show “route A OR route B,” with age/skills clearly shared. Use the published credential/role labels. Preserve backend authority; do not reimplement route eligibility in JavaScript or simply hide missing requirements. Test a passing default route, passing alternative and neither-route case in both projection and DOM/actual screen.

## Small corrections completed by Codex

Claude was notified of the exact file scopes before editing.

- `skills/training.py` + `tests/test_skill_journal_lines.py`: the four-variant pool previously fell back to the full pool after exhaustion. All four variants remained in the 30-entry history, so subsequent draws became unrestricted and immediately repeated lines. Renew the oldest picks from the same skill until a variant is available, retaining other skills' history and the cosmetic RNG. Added a 20-pick exhaustion regression with a real isolated RNG. This is a bounded history with pool renewal, not a promise of 30 distinct picks from four texts.
- `tests/browser/m3c_checks.py`: use the actual `train_evening_course` target (the old helper looked for nonexistent `train_study_skills`); inspect singular `education_programs.enrollment` for migration; report actual elapsed player age instead of loop iterations as “years.” These change the evidence's honesty/coverage, not gameplay rules. Final runner still passes 7/7, subject to C1/C2's missing invariants. Updated reported elapsed times are 10/6/11 years for the sampled trade/office/healthcare lives, not the original loop counts.
- `content/journal/yearly_lines.py` + `tests/test_journal_fact_audit.py`: three flavour lines still invented specific tracked actions: asking for a raise, applying for a bigger role, and purchasing a lottery ticket. They now express rehearsal/aspiration/imagination; added phrase pins. Kept IDs, pools, gates and everyday incidental flavour unchanged.
- `tests/test_web_v2_hidden.py`: the full run hit a readiness race at the initial visibility assertion, before navigation. The test waited only for shell markup, which can mount while the game view is still loading. Wait for actual visibility without sleeps, and open the real More routes control instead of clicking the first header button (the hidden Back button). All semantic hidden/resume assertions remain; the isolated original passed, then all 10 hidden/Back checks passed with the correction.

## Verified / leave alone

- Independently reran the D M3c harness: **7 PASS**. Source review confirms ladders use real commands rather than prerequisite/env grants. Preserve that approach; add the missing constraint assertions and alternate-route history cases.
- Real Chromium Occupation probe: paid communication training → named job application/acceptance → two aged years. At **390×844 and 320×568**, the careers hero is first, role/employer and salary reflect state, performance width is the real **63%**, named header is correct, and there is no horizontal document overflow. Keep B's layout/hero/header changes.
- Paired `opening` values are validated before mutation and legacy role/employer payloads remain compatible. Keep the structural pairing improvement; make promotion consistent with it.
- Keep the OR-route repair, active-role lower-bound rejection tests, actual shared age/skill gates, fresh-hire baseline, job-loss/retirement synchronization and the single payroll/ledger boundary. Do not revert to AND-ing all routes together.
- C's retirement flavour gate and core lifecycle-claim rewrites are useful; keep the remaining wording work. Do not interpret a short forbidden-phrase list as a semantic proof of every narrative sentence, or ban ordinary incidental flavour.
- Keep the one-line parent-drift cap and all actual relationship deltas; it reduces clutter without dropping relation updates. Existing cosmetic/no-repeat/undo behavior remains the boundary.
- No new ladders, employers, portraits, balance changes or broad UI redesign is required for these fixes.

## Validation

- Skill lines + fact audit + yearly selector + skills + real year flavour + undo after the pool fix: **70 passed**.
- Pool/undo checks under `PYTHONHASHSEED=1` and `42`: **20 passed** each.
- Final three factual-line edits + journal/selector/year tests: **31 passed**.
- Careers and Shell DOM checks passed.
- Independent final D evidence: **7 PASS**; independent employer-validity probe: **FAIL**, proving why the headline pass is insufficient.
- Actual employed Occupation screen probe: **PASS** at 390×844 / 320×568.
- Full repository suite: **1 failed, 1,016 passed, 9 skipped, 117 subtests passed in 602.61 seconds**. The sole failure was the initial-visibility test race described above; after the small test correction, hidden/Back checks pass **10/10**. No known suite failure remains. The full run began before the small Codex corrections, whose affected paths have targeted/browser verification above; a second full 10-minute run was unnecessary for these isolated changes. C1–C3 remain uncovered functional/presentation findings despite the broad pass count.
- `git diff --check` and imports passed. No commits.

## Claude's split + policy decision (send after the final navigation result)
- Policy (C1, frozen by Claude): role experience = total recorded years across ALL stints in that role (current + past), from stored tenure only. Unknown/missing legacy tenure counts as 0. Resign/dismiss/change never adds years.
- A (t11): C1 per the policy; C2 promotion respects employer availability (an unavailable promotion shows its real reason and points to the employer-change path); C3 server side: publish named routes with per-route requirements/tenure/met + shared age/skills (contract Amendment 5 FIRST, so B can start).
- B (t12): C3 UI: render "Route A OR Route B" from A's projection, shared requirements once, labels not IDs; DOM + real-screen tests for default-pass, alt-pass and neither.
- D (t14): C2 evidence: assert employer-role validity at every ladder step; real play moves employer when needed; alt-route history cases.
- C (t15): no item this round.
