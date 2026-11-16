# Kimi D report — M1 QA tooling, browser tests, visual evidence

Date: 2026-11-16. Owner files: `tools/**`, `tests/browser/**`,
`docs/collab/evidence/**` (nothing outside these was edited).

## Deliverables

### 1. `tools/avatar_sheet.py` — avatar contact sheets

One command, deterministic (no randomness/time input), renders every catalog
option through `src/appearance/render.py`, lays avatars out in labelled SVG
grids and rasterises with `rsvg-convert`. Output:
`docs/collab/evidence/avatars/` — 28 PNGs (14 sheets × 32 px and 128 px):

- `matrix` — 12 skin tones × ages 1/8/16/30/70 (60 cells)
- `hair-headwear` — all 22 hair styles under each of 7 headwear items (154)
- one sheet per feature category: face_shape, eye_shape, eye_color, brows,
  nose, mouth, hair_texture, hair_color, facial_hair, marks
- `outfits` — 8 outfit styles × 12 colours
- `accessories` — 6 glasses × 5 earrings

Spot-checked `matrix_128px` (clean age progression, distinct skin tones) and
`hair-headwear_32px` (every hat sits correctly on every style, labels legible).

### 2. `tools/browser_check.py` + `tests/browser/` — headless smoke runner

- `tools/cdp_client.py`: stdlib-only CDP client (raw RFC 6455 websocket,
  session management, event collection of console errors / uncaught
  exceptions / failed requests, condition polling, network-idle waits that
  pump events — no fixed sleeps).
- `tools/browser_check.py`: starts `web.server` on a free port with a
  throwaway save dir; loads `/creator`, `/gallery`, `/game`, `/relationships`
  at 390×844 and 1280×800; records console errors, failed requests and
  horizontal overflow; screenshots every page; runs the flows; writes
  `docs/collab/evidence/browser/report.md` + screenshots. Exits non-zero on
  any failure.
- `tests/browser/flows.py`: **data-driven flow list** — new milestone flows
  are appended here without runner changes. Current flows:
  - `creator-keyboard`: swatch render, focus retention across the async
    preview round-trip, `aria-pressed`, ArrowRight tab traversal,
    multi-select marks, export JSON validated in Python via
    `Appearance.from_dict`, rapid age-slider sweep keeps a valid SVG.
  - `m1-life-flow`: create life via API → born → age up → pending decision
    resolved in the modal → age ≥ 2 → reload → age/name persist → player
    avatar rendered (validated against the modal-based UI Kimi B shipped;
    note the game page gained `turnCard`/modal/screens while this task ran,
    so the hook targets the current markup).

### 3. Results against current state

- `python3 tools/avatar_sheet.py`: 28/28 sheets written.
- `python3 tools/browser_check.py`: **50/50 checks passed**, exit 0
  (verified green three runs in a row).
- `python3 -m unittest discover -s tests`: 164 tests, the only
  failures/errors are in Kimi C's in-progress rules tests
  (`test_rules_menus`, `test_rules_income`, `test_rules_m2a`) — none touch
  files I own.

## Findings reported to Claude

1. **favicon 404** (Kimi B, `web/`): every page requests `/favicon.ico` and
   gets a 404. Filtered to a note in the browser check so the tool stays
   green; pages should serve an icon.
2. Suite state: `test_rules_*` failures above belong to Kimi C's milestone-2
   work in flight.

## Usage for reviewers

```
python3 tools/avatar_sheet.py     # regenerates docs/collab/evidence/avatars/
python3 tools/browser_check.py    # full smoke; report + screenshots in
                                  # docs/collab/evidence/browser/
```
