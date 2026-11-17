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

Decision update (Claude, 2026-11-16): the EVENT SCHEMA (docs/collab/M2/event-schema.json) is now owned by Kimi C, who also owns the event data files. Kimi A reviews it for loader, ledger and coordinator fit, and owns the loader. Claude freezes it. (This replaces the earlier ruling: A has already handed the draft over, and C has the most complete picture.)

Rebalance (Claude, 2026-11-16): Kimi A is the bottleneck. M0b ENGINE work (appearance schema v2, M0b-examples.json, v1→v2 migration, generation, inheritance and their tests; files src/appearance/model.py, catalog.py, generate.py, aging.py, integration.py, tests/test_appearance.py) moves from A to Kimi C. The surrender rule (double-confirm decision) is C's as a rule action. A keeps: the service, coordinator, save code, the DELETE game endpoint in the service, and the remaining M2a fixes.

Rebalance (Claude, 2026-11-16): src/game/events.py (the event loader) and src/game/content/** move from A to Kimi C, who owns the schema and content. A keeps the coordinator call site (decision_offer/annual_events phases calling the loader's public functions).

Rebalance M2.5 (Claude, 2026-11-17): C's task step 5 (PARENTING: styles, kids' personalities and milestones, requests, teen troubles, holidays, grandchildren) moves from C to Kimi A, because A has finished its graph work and C's list is the longest. A builds it in a new feature module src/game/features/parenting/** using C's v3 loader and content format; C keeps dating, drama, weddings and the general content. A doesn't edit C's files; anything needed from events.py/rule_actions.py goes to C as a request.

Rebalance M2.5 #2 (Claude, 2026-11-17): WEDDINGS & MARRIAGE (C's step 4: guests, venue, outfit, honeymoon, cash prenups, anniversaries, vows, selected-union breakup/divorce) moves to Kimi A in src/game/features/weddings/**, using C's v3 loader. C keeps dating (current), drama/secrets/reputation and general content. A sends C requests for anything needed in C's files.

Rebalance M2.5 #3 (Claude, 2026-11-17): DRAMA/SECRETS/REPUTATION (C's step 3: jealousy and expectations, affairs, observer knowledge, discovery, scoped reputation, forgiveness; consensual plural unions aren't automatically cheating) moves to Kimi A in src/game/features/knowledge/**, implementing C's frozen reputation-knowledge.md. C keeps dating (current), the loader/schema and the general content, and reviews A's knowledge implementation against its contract.

Rebalance M2.5 #4: CANCELLED (B had already built the dating screen; B keeps it, and C reviews it).
