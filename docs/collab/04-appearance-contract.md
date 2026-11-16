# Appearance contract v1 (frozen — changes need Claude + Codex agreement)

Both Kimi agents build against this. Plan: 03-plan.md.

## Data shape (JSON form; Python `Appearance` dataclass mirrors it, `to_dict()` / `from_dict()`)
```json
{
  "schema_version": 1,
  "seed": 123456789,
  "skin_tone": "skin_04",
  "face_shape": "oval",
  "eye_shape": "almond",
  "eye_color": "brown_dark",
  "brows": "soft",
  "nose": "button",
  "mouth": "smile",
  "hair_texture": "curly",
  "hair_color": "black",
  "marks": ["freckles"],
  "hair_style": "afro_medium",
  "facial_hair": "none",
  "accessories": {"glasses": "round", "headwear": "none", "earrings": "studs"},
  "outfit_style": "tee",
  "outfit_color": "blue"
}
```
- Physical (inherited): skin_tone, face_shape, eye_shape, eye_color, brows, nose, mouth, hair_texture, hair_color, marks.
- Styling (never inherited): hair_style, facial_hair, accessories.*, outfit_style, outfit_color.
- Every value is a stable string ID from `src/appearance/catalog.py`. Every category includes a safe default; unknown IDs → default (never crash).
- Catalog minimums: 10 skin tones, 5 face shapes, 6 eye shapes, 8 eye colours, 5 brows, 6 noses, 6 mouths, 4 hair textures, 12 hair colours (incl. grey/white), 18 hair styles (incl. bald, buzz, bun, braids, locs, ponytail, afro, long straight, wavy bob), 6 facial hair (incl. none), marks: freckles, mole, beauty_mark, scar_brow, birthmark; glasses 5 + none, headwear 6 + none (cap, beanie, headscarf, sun hat, headband, beret), earrings 4 + none; outfit styles 8; outfit colours 12.
- Age is NOT stored in Appearance; rendering takes age separately. Age stages: baby 0–2, child 3–12, teen 13–17, adult 18–49, elder 50+. Grey/wrinkle/recession progression derived from `seed` + age, deterministic.

## Python API (Kimi A)
- `src/appearance/model.py`: `Appearance` dataclass, `SCHEMA_VERSION`, `to_dict`, `from_dict` (tolerant).
- `src/appearance/catalog.py`: category → ordered list of `{id, label, ...}` (colour hex for colour categories). Labels user-facing.
- `src/appearance/generate.py`: `random_appearance(rng, age, gender=None)`, `inherit_appearance(rng, mother, father)` (either may be None), `age_appropriate(appearance, age)` for accessory rules. Uses its own `random.Random`, never the global `random`.
- `src/appearance/aging.py`: `aged_view(appearance, age) -> dict` (effective hair colour, wrinkles level 0–3, recession level 0–3, stage name).
- Integration: every Person gets `.appearance`; `ensure_appearance(person)`; post-load migration in the save loader (keep safe_save allowlist protections).

## Rendering API (Kimi B)
- `src/appearance/render.py`: `render_avatar_svg(appearance_dict, age, size=128) -> str`. Pure; calls `aged_view` from Kimi A (until it lands, use a local stub with the same signature — delete it after). Fixed viewBox 0 0 200 200, no scripts, no external refs, deterministic.
- Fixtures: `tests/fixtures/appearances/*.json` (Kimi A creates at least 12 diverse ones first; Kimi B may add more).

## Web app (Kimi B)
- `web/` folder; start with `./play-web.sh` → local server on 127.0.0.1 only (stdlib `http.server` or one small dependency agreed in review), prints the URL.
- Pages: `/creator` (character creator), `/gallery` (every skin tone × age stage × accessory overlaps × 32 px / 128 px), plus a shared "person chip" component (avatar + name + role).
