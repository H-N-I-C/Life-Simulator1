# Standing process (M0 → M12) — applies to every agent

Lead: Claude (t7). Planner/reviewer: Codex (t13). Implementers: Kimi A (t11), Kimi B (t12).
Roadmap: 09-roadmap.md (+ detailed specs in 07-roadmap-codex.md). User approved running all milestones continuously.

Per milestone:
1. Codex drafts the milestone spec + Kimi A / Kimi B task files (docs/collab/M<n>/), split by file ownership to avoid conflicts. Claude approves or edits.
2. Kimis implement, write M<n>/kimi-<a|b>-report.md, then `hc send t7 "<done + report path>"`.
3. Codex reviews → M<n>/codex-review*.md (PASS/FAIL/NEEDS-FIX with file:line), then `hc send t7`.
4. Claude reviews, sends fix tasks. Loop until Codex AND Claude pass.
5. Claude tells the user what's ready to test and how. Feedback from the user becomes fix tasks.
While Kimis implement milestone N, Codex drafts N+1 so there's no idle time.

Rules for everyone:
- ALWAYS `hc send t7 "..."` when you finish a task or are blocked. Never wait silently.
- Don't commit or push. Don't edit files you don't own in the current task.
- Every feature must be playable in the browser via ./play-web.sh, with tests.
- Quality bar: polished, content-rich, original (BitLife-like mechanics, not BitLife's art or text).

Testing policy (user, 2026-11-16): Kimi agents run only focused tests for what they changed and spend their time writing high-quality, correct code and tests. Codex runs the FULL suite on every review (it is cheap, ~11 s). Claude and Codex are responsible for evaluating logic and correctness and helping the Kimis get it right.

Review cadence (user, 2026-11-16, to save Codex tokens): Codex does NOT review individual tasks or fix rounds. Codex does (1) one planning pass per milestone (spec, pitfalls, task split), and (2) one consolidated review when a milestone (or a user-testable stage such as M2a) is complete, with all agents' work at once, plus at most one re-review of the fixes. Between those, Claude does lightweight checks of reports and focused tests and routes fixes. Kimis tell Claude when done; Claude decides when a milestone is ready for Codex.

Speed-up rules (user-approved, 2026-11-16)
1. Contract first: each milestone/stage begins with frozen shared data shapes (API/state/ledger/event examples). No parallel coding on a shape that isn't frozen. Changes go through the shape's owner and Claude.
2. Self-check: every report ends with a ticked 00-SELF-CHECK.md.
3. Status board: keep your row in 00-STATUS.md current.
4. From M3: feature ownership — one Kimi owns a whole feature end to end (rules + service + UI + tests) on the stable M2 engine; D does QA plus content tooling.
5. Content as data: events/careers are written in data files using a schema (defined in M2c), so content can be added without engine edits.
6. Scenario starts: a dev-only "start as…" panel (valid states, not bare age edits) so the user can test features in minutes (A service + B UI, M2a).
7. Git: once M1 passes, work moves to per-agent branches with small commits; Claude merges after checks.

HALT AFTER M2 (user, 2026-11-16): once M2 is finalised (Codex review plus any fix round passed), ALL agents stop. No M3 or M2.5 work, drafts included, until the user has tested M2 and Claude has committed. Until then, work only on M2 fixes and polish.
