# Task — Kimi D: QA tooling, browser tests and visual evidence

Read: docs/collab/00-PROCESS.md, 00-TEAM.md. You own only tools/**, tests/browser/**, docs/collab/evidence/**. Do not edit web/, src/ or other tests; report problems you find to Claude (`hc send t7`) with steps to reproduce.

Available: /usr/bin/chromium (headless, supports --remote-debugging-port and DevTools protocol), rsvg-convert, Python stdlib. Playwright is NOT installed. Ask Claude before adding any dependency (a venv-local pip install for test-only tooling is likely fine; ask first).

Deliverables:
1. `tools/avatar_sheet.py`: generates labelled PNG contact sheets from src/appearance/render.py + catalog. Sheets: skin tones × age stages, hair styles × each headwear, each face feature category, outfits, accessories, and 32 px / 128 px versions. Output to docs/collab/evidence/avatars/. Must be one command and deterministic, so every reviewer uses the same evidence.
2. `tools/browser_check.py` + `tests/browser/`: a headless-Chromium smoke runner. Start the web server on a free port; load each page at 390×844 and 1280×800; record console errors, failed requests and horizontal overflow; take screenshots; run scripted flows (keyboard: tab through the creator, select options, check focus stays put; export the appearance JSON and validate it with src/appearance/model.Appearance.from_dict). Then the M1 flow once Kimi B's game pages exist: create → be born → age up → resolve a decision → reload → state persists. One command: `python3 tools/browser_check.py`, exits non-zero on failure and writes a short markdown report plus screenshots to docs/collab/evidence/browser/.
3. Run both now against the current state and send Claude the findings (bugs go to their owners through Claude).

Quality: robust waits (no fixed sleeps where an event or condition can be awaited), clear failure messages, and it must work for every future milestone (data-driven list of flows). Report: docs/collab/M1/kimi-d-report.md, then `hc send t7`.
