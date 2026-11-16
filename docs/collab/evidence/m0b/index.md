# M0b evidence — Fluent 3D avatar swap (2026-11-16)

Captured at 390×844 (deviceScaleFactor 2) against a dev-mode server with
the Fluent asset set from `tools/fetch_fluent_avatars.py` (pinned commit
1ffb34c7, 227 assets, 8.8 MB, manifest 2,093 combos).

| File | Shows |
|------|-------|
| `game-family-390.png` | Game summary at phone width: player portrait (Fluent, circle, background tint), stat bars, timeline with per-person avatar chips |
| `game-family-tab-390.png` | Family tab: parents, partner, children — every person with a distinct Fluent portrait + relationship bar |
| `gallery-390.png` | New gallery (Skin tones × age stages); baby and teen cells render |
| `creator-initial.png` | Creator shell + tabs (Look/Skin/Hair/Extras/Badges/Background), age slider, identity form |
| `creator-tab-*.png` | One screenshot per creator tab |

## Known issues visible in this evidence (already routed, not D-owned)

1. **Creator preview/options blank** (`creator-*.png`): `creator.js`
   buildOptions crashes (`Cannot read properties of undefined (reading
   '1')`, creator.js:87) — tab clicks throw, so all creator tab
   screenshots are identical. B to fix.
2. **Gallery "Default · Child" cell empty** while Baby/Teen render —
   child asset key miss or lazy-load gap; B to check against
   `web/static/avatars/manifest.json` (combo `person-child-default` →
   `child_default.png` exists and resolves).
3. **Badge-bearing creates rejected by A's validation** (known, routed
   to A) — badge picker QA marked **pending**, not failing.

## Verified working
- `/api/avatar.png?app=<base64url-json>` returns 256px PNG (200).
- Game screens render Fluent portraits for player, family and timeline.
- Avatar sheets (docs/collab/evidence/avatars/fluent_*.png) regenerated
  from the same manifest.
