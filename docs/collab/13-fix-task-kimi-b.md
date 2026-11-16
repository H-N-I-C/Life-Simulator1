# Fix round 1 — Kimi B (M0) — Codex review 12 + Claude review

Pause M1 work, do this first, then resume M1.

## A. Fix all 8 findings in 12-codex-review-b.md
Blockers 1–4 (afro hides the face, marks exported as a string, controls that change nothing, keyboard focus lost) and 5–8 (preview race/errors, gallery coverage, contrast/long names, input bounds + static path containment). Also remove the old fallback/stub aging path in render.py (render must use Kimi A's aging only).

## B. Claude's visual review (from docs/collab/codex-review-evidence/contact.png) — quality bar is "polished mobile game", not "works"
1. Faces all look the same. Make features visibly different at 128 px: eye shapes, brows, noses, mouths and face shapes must each be recognisable, side by side.
2. Proportions: the head is too small and the torso too big. Aim for a head-and-shoulders portrait, with the head taking about 55–60% of the frame height (adult). Babies/children: bigger head relative to the body, rounder face.
3. Age styling at render time: babies (0–2) show only short/wispy hair of their hair colour whatever the stored style, and no accessories. Children get no facial hair.
4. Hats must sit on the head (follow its curve, slightly wider than the skull) and never cover the eyes. The "bun beanie" and "afro" examples fail.
5. Skin tones: adjacent tones must be clearly distinguishable, and the range must run from very light to very dark (check skin_06 vs skin_12, which look alike).
6. Hair: give it volume and a highlight/shade layer, and show texture (straight/wavy/curly/coily) visibly on compatible styles.
7. Add a soft circular background and a subtle outline/shadow so avatars read well on white cards and at 32 px.
8. Outfits: each of the 8 styles visibly different (collar/neckline shapes), not just a colour change.

## Evidence required in your report
- A contact sheet PNG (rsvg-convert or a browser screenshot) at docs/collab/M0-evidence/ showing: every skin tone × 5 ages; every hair style with and without each headwear; every eye/nose/mouth/brow option; every outfit; and 32 px versions.
- Browser test notes for keyboard use and marks export round-trip.
- Full test suite output.

Update docs/collab/kimi-b-report.md ("Fix round 1"), then `hc send t7`.
