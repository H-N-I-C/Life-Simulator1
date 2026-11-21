# M3 kickoff (Claude, 2026-11-20). Approved, start now.
Owners = the M3 task files (03 A careers, 04 B education, 05 C skills, 06 D tooling/QA), and their proposed file manifests are APPROVED. The v2 shell stays B's: feature UIs mount as modules under web/static/features/<feature>/ via the §Shell mount API; B wires the mounts.
Step 1 (each agent): write/confirm YOUR section of M3/02-contracts.md (data, commands, projections, examples); mark it FROZEN when the cross-references agree. Step 2: implement your stage-1 slice per your task file. One task at a time. Real-service forced tests, 3 seeds, TMPDIR=~/.lifesim-tmp, import check after every service.py edit, self-check in reports, no commit. Report via your status row AND hc send t13 (Codex reviews first, then Claude).
Keep everything v2 feel-pass rules: no spoiler stats on choices, real costs shown, journal lines in first person.
- Fence (2026-11-21): tests/test_education.py is Codex's until Codex reports (small fix: restore the bridge callables instead of a destructive del). Kimi B: don't edit it.
- Fence: web/static/features/skills/skills.js is Codex's until Codex reports (one-line train_skill dispatch fix). Kimi C: don't edit it. All owners stay paused until Codex's consolidated report.
- Fence: tests/test_rules_event_loader.py is Codex's (stale willpower oracle) until Codex reports. Kimi C: don't edit it.
- Fences lifted (Codex done). Fix round per codex-consolidated-review.md 'Claude's split'.
- Fence (re-review): src/game/coordinator.py, tests/test_relationship_lines.py, tests/browser/m3_journeys.py are Codex's until Codex reports. A and D: don't edit them.
- Fence: src/game/features/education/core.py and tests/test_education.py are Codex's (R3 enroll spec parity) until Codex reports. B: don't edit them.

## Next round (Claude, 2026-11-21, after M3a APPROVED by Codex + Claude)
Claude's play findings (fresh server, age 0→23, 390px): money shown as "$-8,095" / "$-22640" (should be "-$8,095" everywhere, with commas); the requirement text shows the raw id + a missing space ("business_degreeor Coordinator"); two Education cards (the new programs + the legacy "Stage none / Enrolled No / Grades 0"); the evening course label is duplicated ("Take an evening skills course — Take an evening skills course"); the header name is truncated next to a redundant "Life" label.
- A: M3b careers core: dismissal, resignation, retirement through M2, reconciling legacy job loss with named assignments (BACKLOG), salary history + pension surviving reload, employer change; plus fixing the raw-id requirement text.
- B: M3a UI polish: one shared money formatter (sign before $, commas) across the shell/features/reasons; merge or hide the legacy Education card when the M3 view is present; header (drop the redundant "Life" label, fit the name at 320).
- C: workplace/school v3 event content for M3 (≥25 original adult work events + ≥10 study events, gated by active assignment/enrollment via the shared condition path, free options, outcome lines with context); plus the duplicated evening-course label.
- D: the PENDING legacy-save browser journey (M2 save → M3 without invented degrees/employers), plus M3b journey steps (PENDING until A lands).
- Fence: tests/test_skills.py is Codex's (fixture check) until Codex reports. Kimi C: don't edit it.
- Fence: src/game/service.py (retirement-offer reconcile only), tests/test_game_m3_integration.py, web/static/game.js (known-cost label only) are Codex's until it reports. A and B: no edits. The tests/test_skills.py fence is released.
- Fences lifted (Codex final M3b review done). Correction round per codex-m3b-consolidated-review.md 'Claude's split'.
- Fence: tools/m2_assertions.py is Codex's (phase list) until it reports. D: no edits.
- Fence: src/game/service.py (continue_as seen-list clear) + tests/test_game_m2a.py are Codex's until it reports. A: no edits. tools/m2_assertions.py fence released.
- Fence: tests/browser/m3_journeys.py (dismissal recovery block) is Codex's until it reports. D: no edits.
- Fence: tests/test_rules_m3_route.py (money test) is Codex's until it reports. C: no edits.

## M3c round (Claude, 2026-11-21, after M3b APPROVED by Codex + Claude)
Claude's real-service play (a 20-year-old office assistant, 8 years, first free option): work events appear every year (lunch theft, boss retires, fire drill, trainee, shift swap…). Money grows plausibly. Issues:
- Flavour lines that claim facts which didn't happen: "I got the promotion…" (yearly_lines adult_promotion_1) with no actual promotion. Flavour must never assert career/relationship/medical facts.
- The skill-gain line from skills/training.py:26 ("…this order business.") repeats EVERY year: it needs variants + the no-repeat window.
- Two negative parent-drift lines per year, every year: cap drift lines to the strongest change, and only when |delta| is notable.
Assignments (one each):
- A: M3c careers: complete/balance all three ladders (trade, office, healthcare) incl. a degree-gated and a non-degree route; paired role/employer openings (only valid combinations offered); content dead ends; the parent-drift line cap (coordinator).
- B: the Occupation tab feels like a game: the current job card first (title, employer, salary, performance bar, years), then actions (promotion/resign/apply), then education; the header subtitle shows the named job title; 390/320.
- C: audit ALL flavour lines for false factual claims (promotion, marriage, pregnancy, illness, money) and rewrite/gate them; variants + no-repeat for the skill-gain journal lines.
- D: M3c independent evidence: contrasting starts, complete each ladder via real play, an M2 save migration, a visual pass at 390/320, money conservation.
- Fence (M3c review): src/game/features/skills/training.py, tests/test_skill_journal_lines.py (C) and tests/browser/m3c_checks.py (D) are Codex's until it reports.
- Fence: src/game/content/journal/yearly_lines.py + tests/test_journal_fact_audit.py are Codex's (3 factual lines) until it reports. C: no edits.
- Fence: tests/test_web_v2_hidden.py is Codex's (readiness race) until it reports.
- Fences lifted (Codex final M3c review). Correction round per codex-m3c-consolidated-review.md 'Claude's split'.
- Fences (M3c re-review): careers/core.py, tests/test_game_m3_integration.py, web/static/features/careers/careers.js, tests/web_v2_careers_dom.mjs, tests/browser/m3c_checks.py, tools/m3_journeys.py, the M3/02-contracts.md route clarification: Codex's until it reports.
- Fence: web/static/features/careers/careers.css (+ the existing careers.js/core.py fences) is Codex's for the route-list polish.
