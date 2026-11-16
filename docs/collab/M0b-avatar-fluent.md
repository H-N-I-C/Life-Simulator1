# M0b — Switch avatars to Fluent 3D emoji (user-approved 2026-11-16)

Look reference: canvas option F (https://claude.ai/artifact/FC5Y1cxgk9PTEnQ5QnmR15). Source: github.com/microsoft/fluentui-emoji, MIT licence. Keep the LICENSE text in web/static/avatars/LICENSE-fluentui-emoji.txt and credit it on an About/credits line.

## Contract (freeze first; Kimi A owns it, publish to docs/collab/M0b-examples.json before B builds)
Appearance schema v2, with a migration from v1 (keep `seed` and `person_id` links):
- `base`: person | woman | man (independent of the gameplay gender; default derived from gender)
- `skin_tone`: default | light | medium-light | medium | medium-dark | dark (map the 12 v1 tones by luminance into these 6)
- `hair`: default | curly | red | white | bald (inherited: hair type/colour from parents; `white` is also applied automatically by ageing, see below)
- `facial_hair`: none | beard (beard uses the "man beard"/"person beard" assets; adults only)
- `headwear`: none | headscarf (uses the "with headscarf" assets)
- `badges`: up to 2 of glasses, earrings, cap, crown, flower (small Fluent object emoji shown as a corner badge on the portrait; not drawn on the face)
- `background`: one of 10 soft colours (circle behind the portrait)
- `look_override` (optional, for life events): e.g. student, health worker, police, chef, farmer, mechanic, scientist, artist, judge, pilot, graduate, bride/groom. Set by game state (job, wedding) when an asset exists, else the regular look.
Age stage is derived at render time: baby 0–2, child 3–12 (boy/girl/child), teen/adult 13–59, greying 60–69 (hair→white when hair ≠ bald), elder 70+ (old man/old woman/older person).

## Asset pipeline (Kimi D)
`tools/fetch_fluent_avatars.py`: downloads ONLY the needed 3D PNGs (base × stage × skin × hair/beard/headscarf × look variants) from the pinned commit, resizes to 256 px (keep the alpha channel), writes `web/static/avatars/<key>.png` plus `manifest.json` (key → file, plus the fallback chain when a combo doesn't exist). Pin the commit hash. Report the total size (target < 15 MB). Missing combos fall back gracefully (e.g. curly + elder → elder).

## Rendering and UI (Kimi B)
- Replace the SVG face drawing with `avatar_image_key(appearance, age, context)` resolving through the manifest; the person chip shows the image in a circle on the background colour, with badges in the corner. Keep `render_avatar_svg` only if still referenced, otherwise delete it plus its tests.
- Creator: tabs Look (base), Skin, Hair, Extras (beard/headscarf), Badges, Background; age preview slider; Random; presets. Same accessibility rules as before.
- Gallery: every skin × hair × stage, every look_override, badges, 40 px list size.

## Engine (Kimi A)
Schema v2 + v1→v2 migration, generation (age-appropriate: no beards, badges or headscarves on babies), inheritance (skin tone: one parent's or the in-between tone; hair type from either parent; never inherit badges/headwear), tests. Kimi C: nothing, unless look_override needs job ids (A asks C).

Order: A publishes contract examples → D fetches assets in parallel → B builds. Priority: after the current M2a blocker (S2 funding), before more M2b UI.

## Status (2026-11-16, engine: Kimi C)

- docs/collab/M0b-examples.json: schema v2 contract published to B (t12) and D (t14).
- Engine landed in src/appearance (model/catalog/generate/aging/integration):
  v2 model + tolerant loader, v1->v2 migration (12-tone pairwise skin map,
  hair derivation, beard/badge/headscarf mapping, seed-derived background),
  base refinement from gender for MIGRATED v1 data only (established v2
  'person' bases never mutate on load), generation guardrails (babies never
  get badges/beards/headscarves; badge cap 2), inheritance (skin: parent's
  or in-between tone; hair from either parent; badges/headwear never
  inherited), aging stages per contract (greying onset 60-64 seed-derived,
  render-time white hair, stored hair unchanged).
- tests/test_appearance.py: 51 tests (24 new v2 + 27 ported from the v1
  suite: determinism, RNG isolation, fixtures, save migration/round-trip,
  identity/finalization invariants, hashseed stability) — green.

### A-side changes required by the v2 switch (sent to Claude)

1. tests/test_game_service.py:125-136 asserts v1 projection keys. Diff:
   - `self.assertEqual(player["appearance"]["hair_style"], "afro_medium")`
   -> `self.assertEqual(player["appearance"]["schema_version"], 2)` and
   add `self.assertEqual(player["appearance"]["seed"], 7)` (seed preserved
   through migration; v1 afro_medium has no v2 equivalent key).
2. Service appearance validation now accepts v1 AND v2 IDs (compat layer in
   catalog.is_valid_id); v2 creator profiles should validate against the v2
   categories (badges list etc.) when B's creator lands — coordinate with B.
3. State projection appearance is now a v2 dict (to_dict passthrough).
