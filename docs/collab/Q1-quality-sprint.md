# Quality sprint Q1 (user, 2026-11-17): reliability + polished UI + richer avatars
The user tested M2: the UI didn't look good, avatar choices were too limited, and it crashed. M2.5/M3 stay paused until Q1 is done. One task per agent at a time; report with the self-check list, and keep your status row current.

## Track 1: reliability (agents start now)
- Kimi A (A1): server-side crash safety. Log every unhandled exception with a traceback and request summary to logs/server.log (rotating), plus a crash id; return a typed 500 JSON {error:"internal", crash_id}. Never corrupt a save: write atomically and keep the last-good backup per game (game_saves/<id>.bak); load falls back to the backup with a notice. Add a /api/health endpoint. Tests force exceptions mid-command and prove the save is intact.
- Kimi D (D1): ROBOT PLAYER. tools/robot_player.py drives the HTTP API (and optionally the browser) through thousands of random lives: random creation choices, every enabled action, every pending choice (including text inputs at boundary lengths), age-ups to death, continue-as-child, reload mid-turn, duplicate submits. Seeded and reproducible. On any 5xx, invariant break (money vs ledger, person refs, ages, soft-lock) or console error, it saves a minimal repro (seed + command log) to docs/collab/evidence/robot/ and sends the owner a one-line summary via Claude. Targets: 2,000 lives quick mode (CI), 20,000 overnight mode.
- Kimi C (C1): rules and content hardening. Audit every rule_actions entry point and every content event for unvalidated inputs, missing gates, impossible states (dead/absent people, negative money, empty lists), and division/None hazards. Add property-style tests (random states, every action) that assert no exception and that the invariants hold. Fix what's in your files; send A the coordinator/service issues.
- Kimi B (B1): front-end resilience. A global error boundary: window.onerror/unhandledrejection report to POST /api/client-error (A adds the endpoint; agree the shape with A first) and show a friendly "Something went wrong. Your save is safe" card with Retry/Reload instead of a broken page. Loading, empty and offline states for every screen. Double-click/duplicate-submit protection on every action button.

## Track 2: design (Claude)
Claude designs every main screen in high fidelity in the design canvas and gets the user's approval. Kimi B then implements them exactly (task B2, after B1).

## Track 3: avatars (Claude)
Claude evaluates open-licensed layered avatar libraries (many face, eye, brow, nose, mouth, hair, colour and accessory options) and shows the user samples. The user picks, then an implementation task follows.
