# v2 redesign kickoff (Claude, 2026-11-18). The user approved this. Start now.
The user hates the current UI; v2 replaces it. Read first: the mockups docs/collab/redesign-v2/{Home,Choice,Person}.dc.html (open them in a browser), Codex's plan docs/collab/redesign-v2/codex-review.md (sections 3–5 are binding), and QUALITY-GUIDE.md.
Ownership = Codex's §4 table. Kimi C gets a TEMPORARY grant for web/static/v2/person.js, v2/person.css and tests/test_web_v2_person*.py only. B is the sole integrator: nobody else edits game.js/style.css/adapter/server.
Rules: branch dev; TokenEconomizer; 1 task at a time; preserve all M2.5 behaviour (retry/revision, privacy, quotes, 18+); no mock-only data (Agreement %, fake jobs/cities, Gift/Holiday/Call) shown as real; phone 320px + keyboard; self-check in every report.

Stage 1 (now):
- A: write the Person projection contract + fixtures (contracts doc docs/collab/redesign-v2/contracts.md §Person), then implement the additive detail view + tests.
- B: write the Navigation/mount + shell-hooks + visual-token contract (contracts.md §Shell/§Tokens), then the v2 shell skeleton keeping all old panels reachable.
- C: once A's fixtures and B's mount API are in contracts.md, build person.js/person.css against them in a test host.
- D: record the reachability baseline of the current UI (every M2.5 surface), evidence under docs/collab/evidence/redesign-v2/.
Stage 2 B: Home + Choice. Stage 3 B integrates C's Person + A's projection. Stage 4 D: parity/visual pass → Codex review → Claude → user.
