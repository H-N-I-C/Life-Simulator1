# Feel-pass evidence (Kimi D, QA — 2026-11-20)

Screenshot pairs for the v2 "feel pass" (docs/collab/redesign-v2/feel-pass.md).
Every screen at both phone widths (390x844 and the 320x568 floor):

- `before-*` — pre-redesign commit `595fd9ab` (M2.5 accepted), captured via
  a git worktree of that commit.
- `after-*` — current tree (interim; B/C are mid-build). Re-run
  `python3 tools/feel_screens.py --label after` once Kimi B and Kimi C land
  F1–F8, then refresh this set.

Harness: `tools/feel_screens.py` (generation-adaptive — drives the legacy
`#tabbar` screens or the v2 shell `.v2-tabbar`/More menu, whichever exists).
2026-11-20 nav fix (Codex review item): every More-route capture restarts
from a fresh active-life page (`fresh_game_page`), waits for a route marker
element (`#resumeView` / `#editorPanel` / `#familyTreeCard` / `#m25Card`),
scrolls it into view, and SKIPS — writing no misleadingly-named file — when
the marker never appears. All-lives gets the same marker check.

Naming: `<label>-<screen>-<width>x<height>.png`.

Stress evidence (Codex F11/F13 open items — narrow 320x844 geometry, long
content, keyboard): `feel/stress/`, harness `tools/feel_stress.py`.

| File | What it certifies |
|---|---|
| narrow-home-320x844.png | fresh-life home at Codex's narrow 320x844 |
| narrow-event-modal-320x844.png | centered opaque choice card at 320x844 (paid option shows "Costs $50") |
| narrow-person-320x844.png | Person overlay at 320x844 |
| long-home-390x844.png / long-home-320x844.png | life aged 30→55 (25 years, deaths/funerals in journal), both widths |
| long-person-390x844.png / long-person-320x844.png | Person overlay (deceased parent) on the aged life |
| keyboard-creator-name-320x844.png | real CDP key events typed into focused /creator #name at 320: field kept focus, page scrolled it into view, value updated, zero console errors/exceptions/failed requests |

Notes:
- `long-person-320x844` and `narrow-person-320x844` are byte-identical on
  purpose: both lives are `married_with_child` and the first person chip is
  the same mother at the same width.
- Headless Chromium has no OS software keyboard; the keyboard evidence uses
  real `Input.dispatchKeyEvent` trusted key events on a focused control —
  focus handling, input acceptance and layout stability — not an OS IME.
- `after-lives-*` and `after-game-empty-*` are identical because `/` with
  no save and `/game` with no save render the same "Your lives" page.
- SKIPPED (reported, not faked): an in-game text-input keyboard case —
  the `pregnant_25` scenario produced **no `baby_naming` decision in 25
  age-ups** (seed 99; the pregnancy resolves without a naming prompt).
  Engine-owner observation, see feel-stress output.
- The stress harness opens a fresh browser tab per life on purpose: the v2
  shell restores overlays/lives from its history state, and reloading one
  shared tab resurrected older lives (back/forward cache) — verified
  symptom before the fix.

## Screen index

| Screen | before | after (interim) |
|---|---|---|
| Lives list (`/`) | before-lives | after-lives |
| Creator | before-creator | after-creator |
| Gallery | before-gallery | after-gallery |
| Game, no save (`/game`) | before-game-empty | after-game-empty |
| Relationships (`/relationships`) | before-relationships | after-relationships |
| Home / life timeline | before-home-life | after-home-life |
| Legacy tabs: summary, education, work, relationships, activities, finances | before-tab-* | — (v2 nav: Occupation/Assets/Relationships/Activities) |
| v2 tabs: occupation, assets, relationships, activities | — | after-tab-* |
| Character editor | before-editor | after-more-character-editor |
| Family tree | before-family-tree | after-more-family-tree |
| Dating & weddings (m25) | before-m25-dating / -wedding-planner / -what-i-know | after-more-dating-&-weddings |
| Decision modal + reopen pill | before-decision-modal / -decision-pill | after-decision-modal / -decision-pill |
| All-lives view | before-all-lives | after-all-lives (legacy Back) + after-more-all-lives |
| Death → legacy/continuation | before-legacy-continuation | after-legacy-continuation |

Note: in the interim after-set the dating_app pending decision did not
surface as a legacy modal in the v2 shell (C's event module is under
construction); `after-decision-*` shots show whatever the page presented.

## F9 — default avatars: findings (report to Claude; fix is outside QA scope)

The "both parents, identical purple hooded look" in audit/3-home.png is
`web/static/avatars/headscarf_default.png` (Fluent "person with headscarf",
purple). Root causes, measured by simulating `random_appearance` + render
resolution over 200 adults per gender:

1. **`src/appearance/catalog.py:39-42` — `HEADWEAR` has only `none` and
   `headscarf`.** `random_appearance` picks uniformly
   (`src/appearance/generate.py:15-16,32`), so **~50% of all adults get a
   headscarf** — the exact purple asset seen. The richer headwear list
   (cap/beanie/sun_hat/…) exists only in `V1_CATEGORIES` (catalog.py:212),
   not in v2.
2. **`src/appearance/catalog.py:34-37` — `FACIAL_HAIR` is only `none`/`beard`,
   so ~50% of adults roll "beard", including women** (rendered as
   `beard_person_*` — observed in simulation: female adults resolving to
   `beard_person_default` etc.).
3. **Parents can collide**: appearances are rolled independently per parent
   (`src/appearance/integration.py:65-92`, called from
   `src/people/classes/person.py:27`), with no same-household de-dup; with
   ~50% headscarf odds plus skin/hair from small pools, identical-looking
   parents are common. A new-game spot check reproduced two headscarf
   parents in one family.

Suggested fix locations (for Claude/engine owner — all outside `tools/`):
bias accessory rolls toward "none" in `src/appearance/generate.py`
(e.g. ~10-15% headwear instead of 50%), gate `facial_hair` on
`base == "man"`/gender, and add a cheap differ check when generating
parents of the same child. Guardrails in `age_appropriate`
(generate.py:139-152) only strip accessories for babies/children, so
adults are unaffected today.

## Robot 500-life run: status

No 500-life robot run has a completed summary. What exists in
`docs/collab/evidence/robot/`:

- `run-pre-fix/` — seed 1, 2000-life default run, pre-fix: failure artifacts
  only, reaching life 1999 (one short of the finish; **no summary file was
  written**).
- Top level — seed 1 artifacts reach life ~722 (multiple restarts,
  2026-11-17/18) and seed 7 artifacts reach life 199 (the standard 200-life
  gate). Again: **failure JSONs only, no run summary**.
- No robot process is running now (checked `ps`). The tool writes its
  summary only at the end of a complete run
  (`tools/robot_player.py:711-719`); every 500+ life run so far stopped
  (or was cut off) before that point.

If the team wants a definitive 500-life summary, it needs a fresh
`python3 tools/robot_player.py --lives 500` run to completion.
