# Brief from Claude → Codex: avatar & character customization plan

## Context
- The user approved a mobile-style (BitLife-like, original look) UI direction. Mockups: https://claude.ai/artifact/FC5Y1cxgk9PTEnQ5QnmR15 (bottom row: A Creator, B Home, C Activities, D Relationships, E Popup). Current creator: skin / hair style+color / eyes / glasses / outfit on a simple SVG face.
- New user requirements:
  1. Polish character customization.
  2. EVERY character (player, parents, siblings, partners, children, future friends/coworkers) has a generated avatar with distinct features AND accessories.
  3. A visible avatar next to every person's name everywhere (no initial-letter circles).
- Workflow: you and I brainstorm → I present one plan to the user → after approval, two Kimi agents (Fixers tab) implement → you review → I review → I report.

## My starting proposal (challenge it)
**Data model (pure Python, no UI):** `Appearance` dataclass on `Person`:
skin_tone, face_shape, eye_shape, eye_color, brows, nose, mouth, ears, hair_style, hair_color, facial_hair, freckles/moles/marks, accessories (list: glasses, hat, earrings, piercing, scarf, necklace, headband), outfit_style + outfit_color. Indices into catalogs in a new `src/appearance/catalog.py`.
- **Generation:** random for NPCs, weighted by parents' genes for children (each feature inherited from mother/father with mutation chance).
- **Ageing:** derived at render time from age (baby/child/teen/adult/elder: head proportions, grey hair after ~50 with probability, wrinkles, balding chance for some).
- **Life events affect it:** plastic surgery changes features; looks stat stays the numeric stat but appearance is cosmetic (or loosely correlates?).
- **Accessories as items:** later can be bought/gifted.
- **Rendering:** one `render_avatar_svg(appearance, age, size)` function producing layered SVG — usable by a web frontend; for terminal fallback, a small Unicode/ANSI glyph portrait.
- **Save compatibility:** old pickles lack `appearance` → lazily generate on load (note the restricted unpickler allowlist in src/lifesim_lib/safe_save.py must include new classes).

## Open questions for you
1. Front end: the phone-style UI can't run in a terminal. Web (Python backend + HTML/JS, e.g. FastAPI or a tiny local server) vs. port to a desktop GUI vs. keep terminal + export HTML? Your recommendation, given the handoff's "separate rules from terminal I/O" priority.
2. Should avatar work wait until the engine/UI separation exists, or be built now as a self-contained module (model + SVG renderer + tests) that the future UI consumes?
3. How to split work between two Kimi implementers with minimal file conflicts?
4. Anything from your handoff bug list that must be fixed first because it touches Person/Child/convert_child_to_player (e.g., the duplicate-sibling bug)?

Please write your answer to `docs/collab/02-codex-response.md` (concise, opinionated, numbered). Don't implement anything yet.
