# Kimi B report — avatar renderer, creator, gallery, person chip (web)

Status: **done, awaiting review.** Nothing committed, per rules.

## What I built

### 1. SVG renderer — `src/appearance/render.py`
- `render_avatar_svg(appearance_dict, age, size=128) -> str`: pure, deterministic,
  fixed viewBox `0 0 200 200`, no scripts/external refs.
- Layer order per contract: back hair → outfit → face → features → marks →
  wrinkles → front hair → facial hair → glasses → earrings → headwear.
- Distinct geometry per age stage (baby/child/teen/adult/elder): head size,
  neck, shoulders, blush on babies/children, wrinkle lines + crow's feet +
  nasolabial folds on elders, age-derived grey hair and recession via
  `aged_view`.
- Bespoke art for all 21 hair styles, 6 facial hair, 6 glasses, 6 headwear
  (layered correctly over every hair style), 4 earring types, 8 outfit styles,
  5 face shapes, 7 eye shapes, 6 brows, 6 noses, 6 mouths, all marks.
- Reads **Kimi A's real catalog** (`catalog.CATEGORIES`) and **Kimi A's
  `aging.aged_view`** (dict→`Appearance` adapter in `render.aged_view`; the
  temporary stub is deleted). A `FALLBACK_CATALOG` (same IDs/hexes) keeps the
  renderer working standalone if `catalog.py` is absent.
- Unknown IDs never crash: `_resolve()` falls back to category defaults;
  real-catalog IDs I didn't write bespoke art for are aliased to the closest
  implemented style (e.g. `short_crop→short`, `curtains→side_part`,
  `bob→wavy_bob`, `suit→blazer`, `dangles→dangly`, `rectangle→square`).
- Rendering bugs found via screenshot review and fixed: outfit-torso path arg
  shift, face ellipse arc (rsvg renders near-full-circle `a` arcs incorrectly
  → two half-arcs), braid/loc strands covering the face, headscarf covering
  the eyes, wrinkle lines floating above the hairline.

### 2. Web app — `web/`, started by `./play-web.sh`
- Stdlib only (`http.server`, ThreadingHTTPServer), binds 127.0.0.1, prints
  URLs. `web/server.py` exposes `/creator`, `/gallery`, `/relationships`,
  `/api/catalog` (catalog + 6 presets + countries), `/api/people`,
  `/api/render` (POST appearance+age+size → SVG), `/api/grid/{skins,ages,
  hair-hats,glasses-earrings}?size=`. Static files in `web/static/`.
- **`/creator`**: 220 px live preview, age slider 0–80 with stage label,
  name/gender/country fields, 5 tabs (Body, Face, Hair, Style, Accessories)
  with labelled colour-dot swatches, randomize-all / randomize-category,
  6 presets, reset, keyboard-accessible (real buttons/selects, focus rings).
  Save = downloads a valid contract-v1 appearance JSON (+ identity block).
- **`/gallery`**: every skin tone ×12, all 5 age stages, hair×headwear
  overlaps (5 hairstyles × 7 headwear), glasses×earrings (6×5), toggle
  32 px / 128 px.
- **Person chip** (`web/static/common.js::personChip`): avatar + name + role +
  optional green relationship bar; demo `/relationships` page renders all
  fixtures (Kimi A's 12 + my 12 relationship fixtures) as chips.

### 3. Tests & fixtures
- `tests/test_render.py` (7 tests, all pass): all ages, every catalog value ×
  5 ages, unknown-ID fallback, missing/garbage input, determinism,
  `aged_view` contract, age changes output.
- `tests/fixtures/appearances/`: my 12 relationship fixtures (raw appearance
  shape + `name`/`role`/`age`/`relationship` keys, all IDs validated against
  Kimi A's catalog — passes `FixtureTests`).

## How to run
```
./play-web.sh            # http://127.0.0.1:8765/creator
python3 -m unittest discover -s tests   # render tests
```

## Screenshots
`docs/collab/shots/`: `creator.png`, `gallery.png`, `relationships.png`,
`hair_styles.png` (all 21 styles), `age_stages.png`, `headwear.png`,
`elder_wrinkles.png`, individual accessory PNGs.

## Notes / unfinished
- 5 errors in `tests/test_appearance.py` (Kimi A's integration/save tests) are
  **not mine** — they fail on `from src.people...` imports and the
  translation `_` at import time; my `FixtureTests` and all render tests pass.
- Aliased styles render as a close cousin rather than bespoke art
  (`curtains`, `slick_back`, `bob`, `diamond` face, `deep_set` eyes,
  `hooked` nose, `pout`, `bushy` brows…). Easy to add bespoke art later.
- Save in the creator currently downloads/prints JSON; game wiring is the
  next milestone per plan.
- Headwear colours are fixed palette choices (catalog has no accessory
  colours); happy to parameterise if the contract grows.

---

# Fix round 1 (Codex review 12 + Claude visual review)

## A. Codex findings 1–8 — all addressed
1. **Afro hid the face (blocker)** — rewrote the afro: hair is now a back-layer
   scalloped volume plus a front band that stops above the eyes; face features
   are never covered. Regression test `test_afro_keeps_features_visible`
   verifies draw order for afro with no hat/cap/beanie/headscarf.
2. **Marks exported as string (blocker)** — creator marks are now proper list
   toggle semantics (`marks: ["freckles","mole"]`), None clears, per-mark
   aria-pressed. Browser-verified: export round-trips through
   `Appearance.from_dict` with marks intact (tests/browser_smoke.py).
3. **Inert controls (blocker)** — single `_resolve` (no duplicate); real art
   for `slick_back`, `curtains`, `undercut`, `bob`, `diamond`, `deep_set`,
   `hooked`, `pout`, `bushy` (aliases removed for these); `hair_texture` now
   draws strand/wave/curl/coil patterns on compatible styles; eye tilt
   actually tilts the lash line and outer corner for up/downturned. New
   byte-difference tests assert every texture/eye/hair-style/feature variant
   produces distinct SVG.
4. **Focus lost on click (blocker)** — options DOM is built once per tab;
   selection updates `aria-pressed` in place; focus is never discarded.
   Tabs implement the accessible tab pattern (roving tabindex, arrow/Home/End
   keys). Browser-verified via CDP (focus stays on the swatch; ArrowRight
   moves tab-face → tab-hair).
5. **Preview races / error HTML** — `renderAvatar` sequences requests per
   container (stale responses dropped), checks status, surfaces readable
   errors via title/console while keeping the previous valid portrait.
   Server `/api/render` validates types deliberately (appearance object,
   age 0–120, size 8–512); unhashable values like `hair_style: []` now render
   as the safe default instead of a 500 with a raw Python message.
6. **Gallery coverage** — hair×headwear now iterates the real catalog (all 21
   styles × 7 hats = 147 cells); added full feature surveys (eyes, brows,
   noses, mouths, face shapes, textures, outfits, facial hair), a 12×5
   skin×age matrix, and a correct size label that updates on toggle.
7. **Contrast / long names** — primary green darkened #22b55a → #17813c
   (~5.9:1 on white), muted #8a8f98 → #5f6672 (~5:1); `overflow-wrap:
   anywhere` on person and player names.
8. **Input bounds / containment** — bounded int coercion with deliberate 400s;
   grid `size` validated (no more request termination); static serving uses
   realpath + commonpath containment and rejects symlinks; traversal fixtures
   tested.

Also removed the old fallback/stub aging path: `render.aged_view` now calls
Kimi A's `aging.aged_view` directly and lets integration failures raise.

## B. Claude's visual review (polished-mobile bar)
1. **Distinct features** — face shapes have distinct silhouettes (diamond,
   heart, square jaws…); eyes/brows/noses/mouths each have 6–7 recognisable
   variants at 128 px (survey sheets below).
2. **Proportions** — rebuilt geometry: adult head ≈ 55% of frame height,
   head-and-shoulders portrait; babies/children get bigger, rounder heads
   with tiny bodies.
3. **Age styling** — babies (0–2) render only short wispy hair of their hair
   colour (stored style ignored) and no accessories at all (glasses/earrings/
   hats gated, tested); children get no facial hair (tested).
4. **Hats** — every hat computes the top of the actual hair volume (afro
   radius, bun top) and sits on it; all brims/bands stay above the eyes.
   Bun-beanie and afro-cap verified in the overlap sheet.
5. **Skin tones** — renderer uses catalog hexes verbatim; all 12 render, and
   adjacent tones are distinguishable in the matrix sheet. Note: skin_06 vs
   skin_12 similarity is a catalog (Kimi A) hex issue, not a renderer
   issue — flagged for Claude.
6. **Hair** — volume (inflated caps, back masses), a highlight arc, a darker
   shade edge, and texture strands per hair_texture on compatible styles.
7. **Background** — soft circular backdrop tinted from the outfit colour plus
   a ground shadow and a subtle head outline, so avatars read on white cards
   and at 32 px.
8. **Outfits** — all 8 catalog styles redrawn with distinct necklines/details
   (crew tee, collared shirt + placket, hoodie + strings + pocket, ribbed
   sweater, flared dress, puffed-sleeve blouse with peter-pan collar, suit
   lapels + tie, zip jacket + collar).

## Evidence (docs/collab/M0-evidence/)
- `skins_x_ages.png` — 12 tones × 5 ages matrix
- `hair_styles.png` — all 21 styles; `hair_hats_1/2.png` — styles × cap,
  beanie, sun_hat
- `features_eyes_brows.png`, `features_nose_mouth.png` — feature surveys
- `faces_outfits.png` — face shapes × all 8 outfits
- `facialhair_texture.png` — facial hair × hair textures
- `at_32px.png` — 12 tones at 32 px
- `creator_phone.png`, `gallery_top.png` — browser screenshots
- Browser/keyboard/export checks: `tests/browser_smoke.py` (pure-python CDP
  driver) — 9/9 PASS in real Chromium: focus retention, marks list export +
  Python model round-trip, tab arrow-key traversal, rapid-slider preview
  stability, gallery coverage counts.

## Focused tests run (per policy; full suite reserved for milestone end)
- `python3 -m unittest tests.test_render tests.test_web_m1` → **37+2 = 25 web,
  14 render — all OK**
- `python3 tests/browser_smoke.py` → 9/9 PASS

## Still open / flagged
- skin_06 vs skin_12 tone proximity is in Kimi A's catalog hexes (A-owned).
- `web/stub_service.py` remains as the documented M1 fallback; remove when
  src/game/service.py is final (M1 task).

---

# Re-review 1 fixes (Codex NEEDS-FIX, visual pass 2)

## Braids & locs — continuous strands (finding 1)
Rewrote both from disconnected circles into continuous round-capped strand
silhouettes anchored at the scalp and falling outside the face opening:
braids get interwoven chevron strokes plus a hair tie at the tip; locs are
thicker ropes with subtle two-tone texture. Verified in
`docs/collab/evidence/avatars/hair-headwear_128px.png` (braids/locs rows,
all seven hats, plus 32 px sheet).

## Stronger facial geometry (finding 2)
- **Eyes rebuilt on named inner/outer corner coordinates** per shape — the
  sclera path, lid details and lash line all use the same corners, mirrored
  about the face centre (the old code computed a tilt it never applied).
  Shapes now differ in aperture, width, spacing and tilt: round is large,
  monolid flat with a lid line, hooded with a skin overlap, deep-set with a
  socket shadow, up/downturned genuinely tilted corners.
- **Brows**: per-style weight (1.3–5.0), height offset (arched high, straight
  low) and length; bushy adds strokes. The default hair fringe now ends just
  above the brow line so brows are visible in-game and in evidence.
- **Noses**: ~25% larger with much stronger width/bridge/tip construction —
  wide has flared nostrils and wing lines, hooked a pronounced bridge hook,
  pointed a sharp angle, strong a broad double-line bridge, upturned visible
  nostrils.
- **Mouths**: wider spread between thin/wide, plumper full lips with a
  highlight, pout rendered as puckered lips with a pronounced lower lip.
- Face-shape silhouettes (square/heart/long/diamond) already passed; kept.

## Evidence (finding 4) — generated with Kimi D's canonical tool
`python3 tools/avatar_sheet.py` → 28 sheets in
`docs/collab/evidence/avatars/` (128 px and 32 px): skin×age matrix, all
21 hair styles × all 7 hats, per-feature surveys on a neutral base (face
shape, eye shape, brows, nose, mouth, eye/hair colour, texture, facial
hair, marks), outfits × colours, glasses × earrings. Sheets inspected:
eye-shape/brows/nose/mouth variants are recognisably different people at
128 px and remain distinct silhouettes at 32 px.

## Finding 3 (skin palette)
Remains routed to Kimi A — the renderer reads catalog hexes verbatim, no
private remaps. Flagged again for the review discussion: decide whether
skin_12 is an undertone variant or a lightness step; A tunes, IDs/labels
stay stable.

## Tests
- `python3 -m unittest tests.test_render tests.test_web_m1` → 39 tests OK.
- Codex's full-suite note (test_game_service expiry fixture looping in
  `_scan_creation`) is A-side; B made no engine edits.
