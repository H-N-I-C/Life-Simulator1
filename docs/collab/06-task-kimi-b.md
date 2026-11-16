# Task — Kimi B: avatar renderer, creator, gallery, person chip (web)

Read first: docs/collab/03-plan.md, 04-appearance-contract.md (frozen contract). Visual reference: mockup "A · Character creator" and "D · Relationships" at https://claude.ai/artifact/FC5Y1cxgk9PTEnQ5QnmR15 (if you can't open it: light #f3f5fa background, blue #2f6fed header, green #22b55a primary button, Nunito font, rounded white cards, big circular avatar preview).
Your files: `src/appearance/render.py`, `web/`, `play-web.sh`, render/web tests. Do NOT edit `src/people/`, save code, or Kimi A's appearance files. Kimi A will message you when catalog/model/fixtures exist — start with the web scaffold and renderer structure meanwhile; use a stub for `aged_view` until Kimi A's `aging.py` lands, then switch and delete the stub.

## Deliverables
1. `render_avatar_svg(appearance_dict, age, size)`: layered SVG (back hair → face → features → marks → front hair → facial hair → glasses → earrings → headwear → outfit), fixed viewBox, deterministic, no scripts/external refs. Must look GOOD — high quality, cohesive, friendly flat-illustration style; readable at 32 px; distinct proportions per age stage (baby/child/teen/adult/elder). Headwear must layer correctly over every hair style.
2. `./play-web.sh` → starts a local server on 127.0.0.1, prints the URL. Python stdlib only unless reviewers approve otherwise.
3. `/creator`: large live preview; tabs (Body, Face, Hair, Style, Accessories); labelled swatches; randomize all + randomize this category; 4+ presets; reset; age preview slider (baby→elder); name/gender/country fields; keyboard accessible; phone-width layout. Save produces a valid appearance JSON (for now: download/print it; game wiring is the next milestone).
4. `/gallery`: every skin tone, every age stage, hair×headwear overlaps, glasses×earrings, at 32 px and 128 px.
5. Shared "person chip" (avatar + name + role, optional relationship bar) used on a demo relationships page built from fixtures.

## Rules
- Don't commit. When done, write `docs/collab/kimi-b-report.md`: what you built, files, how to run, screenshots if you can, anything unfinished. Then stop and wait for review.
