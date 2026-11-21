# Codex feel-pass evaluation — 2026-11-20

**Verdict: changes requested. Do not hand this build to the user as the completed feel pass.** The full-screen shell and centered choices are a clear improvement, but the journal dynamic is still the old implementation and avatar generation remains unresolved.

Reviewed HEAD `8604e37138e61a645e6c980e15e9fdbac9c49434` plus the agents' uncommitted implementation and the small Codex fixes below. Compared the Home/Choice/Person design sources with real Chromium captures at **390×844 and 320×844**; additional broad route/death captures use 390×844 and 320×568. No deployment or commit performed.

Evidence: `../evidence/redesign-v2/feel/` contains agents' earlier shots; independent final evidence is **`docs/collab/evidence/redesign-v2/feel/codex/`**, with `results.json` holding geometry, displayed text, actual server outcome and browser errors. Reproduction harness: `.review-tmp/evaluate_feel.py` (isolated saves, seed 7, real service/actions). Main flow: fresh life → rapid double Age tap → age to 7 event → resolve → Continue → Relationships → Person; reopen each More route from a fresh active-life page.

## Required corrections, via Claude to owners

1. **High — F4/F12: journal is still newest-first and the page grows instead of providing the intended scrolling feed (B, using A's contract).** `game.js:416–488` ignores `journal_feed`, removes year-only lines, collapses quiet years and reverses the cards. Browser order is Age 7, 6, quiet 5, 4…0. At 390px after aging, `.v2-scroll` grows to roughly 1016px and its clientHeight equals scrollHeight, with scrollTop 0: the document owns scrolling, so the existing history helpers that save `.v2-scroll.scrollTop` cannot capture ordinary document scrolling. Implement a viewport-constrained inner feed, one header per year, oldest → newest, following the bottom only when appropriate. Preserve older-reading position and Back restoration. The feel pass supersedes the old newest-first contract; update that contract/comment/tests too.

2. **High — F5: paid-choice costs are lost (A/B/C coordinated contract).** Source-confirmed: pending option projection in `service.py` emits known_effects but no known_costs, `adapter.js:120–130` drops costs, and event.js deliberately hides knownEffects while reading knownCosts. Consequently an affordable paid option can lose its only price indication. Publish explicit public costs, preserve them through normalization, and format structured money costs rather than `String(object)`. Verify real affordable, unaffordable and free options in the browser. The ordinary seed-7 event exercised here has no price; this finding comes from the integration path, not a paid-choice screenshot.

3. **High — F9: default avatars still fail (engine/appearance owner, through Claude).** Fresh seed-7 mother Beitris, age 27, visibly has a beard at both widths; see `person-320.png` and `person-390.png`. D correctly reported the generation problem but did not implement a fix. Resolve generation/default compatibility and household variety; preserve deliberate user-edited appearances.

4. **Medium — F5: outcome popup omits per-person relationship deltas (C).** A exposes `relationship_changes`, but event.js renders only selected keys of `outcome.changes`. Add actual per-person relationship rows with safe public names. Verify a relationship-only outcome as well as a mixed stat/money/relationship outcome. Source-confirmed omission; the captured flavor outcome only changes Happiness and Smarts.

5. **Medium — F8: width animation is not implemented by attaching a transition to newly created bars (B).** `shell.js:198–226` empties the stats row and creates every fill directly at its final width on each sync. Keep existing fills and update their widths after initial paint. The promised age-number ticking is also absent from the shell. Recheck the real animations and reduced-motion behavior; press styling alone does not complete F8.

6. **Medium — F10/F11: finish the outcome lifecycle (B/C).** In both browser runs Age is enabled in the underlying UI while the outcome is open (`ageEnabledDuringOutcome: true`). The overlay does intercept ordinary pointer taps; this is not evidence that a trusted pointer tap advanced behind it. Explicitly gate background Age/actions until Continue. Source also replaces an event module immediately when resolution creates a new pending ID, so chained-choice outcome presentation needs a real browser regression. Do not claim that case passed from the single flavor event.

## Small confirmed fixes completed by Codex

- Loaded missing `v2/person.css` and put theme.css after module styles. Before the fix Person was a raw text/button layout; final screenshots show the intended blue header, portrait card, relationship bar and action tiles.
- Passed playerId into the event module and excluded the player only when selecting the main portrait. Retained event person chips and their navigation. Player-only events now omit the main portrait; other people remain eligible.
- Fixed bottom bar height accounting so its actual height matches the stats dock offset, including short-view/safe-area sizing. At 390px the bar previously measured 80px while the dock assumed 72px, covering the stat tracks. Final bar is 72px and dock ends at its top.

## Acceptance status

| Checks | Result |
|---|---|
| F1/F2 | Pass in sampled live screens: no site header, internal turn IDs or phase names. |
| F3/F7 | Pass after local dock fix: one centered Age button, four tabs, pinned numeric stats; no horizontal overflow in the fresh-life/event/person samples. |
| F4/F12 | Fail: journal order, per-year treatment, feed sizing and scrolling. |
| F5 | Partial: pre-choice stat spoilers hidden; actual +3 Happiness/+2 Smarts match the server outcome. Costs and relationship rows incomplete. |
| F6 | Centered opaque card passes; player-only main portrait fixed. Other-person identity handling still needs a targeted NPC event acceptance case. |
| F8 | Partial: press/busy/flash CSS exists; bar-width implementation and age tick incomplete. Reduced-motion is source-reviewed, not a completed interactive certification. |
| F9 | Fail: fresh default mother visibly bearded. |
| F10 | Rapid double Age tap advances exactly once at both widths. Outcome background gate and chained decisions remain open. |
| F11/F13 | Sampled event and Person fit at both widths. More routes reopen correctly when tested from a fresh active life. Long content, software keyboard, text zoom and focus continuity are not certified by these captures. |
| F14 | Death screenshot has archive/All lives/child continuation controls, no normal Age button. Existing retry/stale-state lifecycle tests pass; this is not a full new death/continuation journey certification. |

## Validation and evidence quality

- Initial targeted regression run: **20 passed** (journal engine, event, Home lifecycle, Back).
- Post-fix shell/event/Back run: **21 passed, 1 skipped, 1 failed**. The failure caught my first avatar fix filtering all player person links. Corrected it to filter only the main portrait, preserving the links.
- Reran the affected real-browser person-chip test plus event suite after that correction: **4 passed**. The skipped test in the broader run is not counted as passing.
- Independent browser seed-7 runs at both widths: no console errors, uncaught exceptions or failed network requests; actual double-tap age = 1.
- Broad `tools/feel_screens.py` refresh wrote 36 screenshots. **Its More-route sequence is unreliable**: it opens All lives before attempting the other routes; the resulting `codex-final-more-family-tree-*` image shows the lives list and an error rather than a tree. Do not interpret filenames or exit 0 as reachability passes. Independent `route-family-tree-*`, `route-character-editor-*` and `route-dating-&-weddings-*` captures restart from the active game and are the relevant evidence. This should be corrected by D before using the harness as an acceptance gate.
- The browser run uses a child life for the independently reopened routes, so the dating view correctly reports the adult eligibility restriction. Full adult wedding/privacy/quote parity remains outside this feel sample.

Small fixes are ready for review. Larger corrections above should return through Claude to the assigned owners, followed by another real-browser pass.
