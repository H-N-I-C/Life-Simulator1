# Avatar & character customization — agreed plan (Claude + Codex), pending user approval

Sources: 01-avatar-brief.md (Claude), 02-codex-response.md (Codex). We agree on all points below.

## Direction
- Target UI: local web app (Python game + HTML/CSS/JS in the browser), matching the approved phone-style mockups. Terminal can't show avatars.
- This milestone builds the avatar system + creator + shared "avatar + name" component. Wiring every game screen into the web UI is the next milestone (requires separating game rules from terminal input/output).

## Features
1. Appearance on every person (player, parents, siblings, partners, children), generated once and kept for life.
   - Physical (inherited): skin tone, face shape, eyes + eye colour, eyebrows, nose, mouth, hair texture/colour, freckles/moles/marks.
   - Styling (editable, never inherited): hair style, facial hair, glasses, headwear, earrings, outfit style + colour.
   - All styling available regardless of gender. Cosmetic only — doesn't change Looks/stats.
2. Children inherit physical features from biological parents with small variation; adopted children get their own.
3. Ageing: baby → child → teen → adult → elder; gradual grey hair, wrinkles, optional hair recession — stable, no flicker.
4. Accessories spread across NPCs naturally (age-appropriate: no hats/jewellery on babies).
5. Creator: large live preview, category tabs, labelled swatches, randomize all / randomize one category, presets, reset/remove accessory.
6. Avatar next to every name via one shared component (lists, headers, popups).
7. Old saves get avatars assigned once on load and keep them.
8. Bug fix bundled: "continue as child" makes you your own sibling and loses relatives' names/looks — fixed.

## Technical contract (frozen before parallel work)
- `src/appearance/`: versioned `Appearance` with stable string IDs (not list indices), seed, catalog, generation, inheritance, ageing. Separate RNG so cosmetics never change gameplay randomness.
- `render_avatar_svg(appearance, age, size)`: pure, deterministic, readable at 24–40 px and detailed at creator size. No external images or scripts.
- Save loader: keep the safety allowlist; add new class; migrate old saves; unknown IDs fall back safely.

## Work split
- Kimi A: `src/appearance/` model/catalog/generation/ageing, Person/Child integration, successor fix, save migration, tests.
- Kimi B: SVG renderer + art, creator web page, shared avatar+name component, a gallery page (skin tones × ages × accessories). Uses agreed fixtures; doesn't touch Person/save files.

## Review
Codex reviews → Claude reviews → report to user. Acceptance: deterministic output, gameplay RNG untouched, save/load + old-save migration, inheritance vs adoption, successor fix, gallery visual check, contrast and keyboard access in creator.

## Out of scope this round
Buying/gifting accessories, plastic surgery changing features, friends/coworkers, other bug fixes from the handoff (pension, overtime, divorce crash, child income) — separate tasks.

## Decision log
- 2026-11-16 (Claude): Skin palette re-graded (IDs kept). Existing test saves render slightly different complexions; acceptable because nothing has been released yet. From the first user-facing release on, catalog colour changes need a schema/version note.
