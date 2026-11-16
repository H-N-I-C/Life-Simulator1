# Team & file ownership (read before every task)

| Agent | Pane | Role | Owns (only edit these) |
|-------|------|------|-------------------------|
| Claude | t7 | Lead: plans, assigns, reviews, talks to user | docs/collab/ plans and tasks |
| Codex | t13 | Planner + reviewer; runs the full suite on every review | review files only, no code |
| Kimi A | t11 | Engine and persistence | src/game/**, src/appearance/** (except render.py), src/people/classes/person.py, src/lifesim_lib/safe_save.py, tests/test_game_*.py, tests/test_appearance.py |
| Kimi B | t12 | Web UI | web/**, play-web.sh, src/appearance/render.py, tests/test_web_*.py, tests/test_render.py |
| Kimi C | t15 | Legacy gameplay rules and bug fixes (prepares M2) | assigned FUNCTIONS in src/people/classes/player.py, child.py, partner.py, parent.py, sibling.py, src/menus/**, src/lifesim_lib/lifesim_lib.py, tests/test_rules_*.py |
| Kimi D | t14 | QA tooling, browser tests, visual evidence | tools/**, tests/browser/**, docs/collab/evidence/** |

Collision rules
1. Edit only files (or functions, for shared files) you own in your current task. If you need a change elsewhere, `hc send t7` with the exact change and why. Claude routes it to the owner.
2. Shared files are split by function: in player.py/child.py, Kimi A owns identity/appearance code (`__init__` identity fields, finalize_appearance, set_parent, convert_child_to_player identity parts). Kimi C owns gameplay functions (age_up money/job/school logic, random_events, divorce, lose_job, get_job, calc_pension, retirement). If unsure, ask first.
3. Never reformat or reorder files you don't own. Keep diffs minimal and focused.
4. Before editing a shared file, re-read it fresh, because another agent may have changed it.
5. Interfaces between agents are written down in docs/collab/M<n>/ specs. No silent signature changes.

Quality rules (from the user): correct logic first, with clear names, small functions and real tests that would fail on the bug. No placeholder features. When a design choice is unclear, ask Claude instead of guessing.

Decision (Claude, 2026-11-16): Kimi A owns the yearly phase map and the ledger/entry schema (src/game/). Kimi C's rule functions conform to them. Kimi C proposes changes, and Kimi A decides; if they disagree, ask Claude. There is exactly ONE phase map: docs/collab/M2/phase-map.md, edited by Kimi A only.
