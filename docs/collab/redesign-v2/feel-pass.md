# v2 "feel pass" (Claude, 2026-11-20). Top priority. The user: "looks nothing like BitLife: the dynamic, UI design, look and feel."
Screenshots (390×844, fresh server): docs/collab/redesign-v2/audit/*.png. Target: a full-screen mobile game that feels fast, punchy and one-handed, like BitLife's DYNAMIC (not its art or text).

## Problems I saw (Claude, playing it myself)
F1 Two headers: the website nav "Life | Game Creator Gallery" sits above the game header. In-game must be full-screen; site nav only on the lives list/creator.
F2 Debug text leaks to players: "This year (t_000003)", "Current phase: year_close". Remove all internal IDs/phase names from player view (dev panel only).
F3 The Age button shows "+ + AGE" (a duplicate plus). It must be ONE big round Age button, centered IN the bottom bar (bar: Occupation · Assets · AGE · Relationships · Activities), always reachable; not floating mid-page.
F4 The journal is the heart: a scrolling feed filling the screen, with an age header per year ("Age 2 years") and several short first-person lines per year, newest at the bottom, auto-scrolled. Now it's a small card with only "I was born…", and nothing appears after aging.
F5 Choices spoil outcomes: "-happiness (25-35) · -mother relationship (6-10)". Hide stat effects before choosing (keep REAL costs: money/time/eligibility reasons). After choosing → an outcome popup (short text + the stat deltas that actually happened), then a tap continues.
F6 The event popup must be a centered modal card (title, avatar of the OTHER person involved, text, option buttons), not a bottom sheet over a dimmed page with the page's text showing through. It shows the player's avatar where the mother should be.
F7 Stats: a compact bar block pinned above the bottom bar (Happy/Health/Smarts/Looks with colour + %), always visible.
F8 Feel: instant feedback on every tap (press state, 120–200 ms transitions, the age-up number ticking, stat bars animating to new values, a brief green/red flash on change); reduced-motion respected.
F9 Avatars: default parents got odd accessories (both parents identical purple hooded look). Accessories must fit age/gender and differ between people.

## Split (one task each, non-overlapping)
- Kimi B (web shell owner): F1, F2 (web side), F3, F7, F8 in shell/home/game.html/theme.css.
- Kimi C (temporary grant: NEW web/static/v2/event.js + v2/event.css + tests/test_web_v2_event*.py): F5 + F6, the event modal and outcome popup as a module; B mounts it. Show effects via the server's real results only.
- Kimi A (engine): F4 server side: every year writes journal entries (an age header + lines from events/aging/school/relationships), exposed in the projection; F2 server side (no phase/turn strings in player text); the outcome result (actual deltas) on decision resolve for C's popup. Publish the shapes in contracts.md first.
- Kimi D (QA): F9 (avatar defaults by age/gender) in the avatar tool/data if in D/tools scope, else report to Claude; plus a screenshot harness (390 and 320, every screen, before/after) at docs/collab/evidence/redesign-v2/feel/.
Codex evaluates in a REAL browser with screenshots when all report. Claude plays it again before the user does.

## Codex additions

These extend the acceptance checks; they are not newly confirmed runtime defects. F1–F9 above take priority. Keep existing A/B/C/D ownership; substantial changes go back through Claude.

- **F10 — One tap, one action (B; A transport contract as needed).** Age and choice taps show immediate busy feedback and cannot submit twice. Rapid double taps must advance exactly one year or resolve one choice. A pending event or outcome must block Age; do not let the next year run behind a popup. If a year contains several decisions, continue through all of them before returning to normal play.
- **F11 — Modal continuity and keyboard (C + B integration).** Event/outcome cards need a scrollable body at 320px and on short screens, with every option reachable, including text-input choices above the software keyboard. Focus starts inside the modal, stays there, and returns sensibly on close. Back/Escape/reload must preserve an unresolved server choice and an obvious way to reopen it; background controls must not activate through the overlay. Outcomes show once per resolved choice, never replay merely because the UI rerendered.
- **F12 — Journal reading behavior (B + A content).** Follow new entries when the reader is already at the bottom; preserve their position when they scroll up to read history. Keep the final line clear of the pinned stats/nav. Verify several consecutive years, multiple entries in one year, a quiet year, and resume/reload: no missing, duplicated, or reordered entries. First-person wording must retain the actual event facts and privacy filtering.
- **F13 — Narrow-screen content and navigation (B; C for modal/person content).** At 390×844 and 320px wide, use long player/NPC names, long narratives, many options and large money values. No horizontal page scroll, overlapping tabs, clipped percentages or unreachable controls; also check text zoom and a short viewport. Preserve the journal/tab scroll position when returning from a person or action screen. Keep a discoverable route to All lives and existing game features after removing the website header.
- **F14 — Failure and end-of-life feedback (B; A authoritative state).** Offline/retry and stale-state recovery must settle visibly without phantom age/stat/money updates or duplicate outcomes. Death must replace the normal Age interaction with the actual supported continuation/new-life route; no active-looking dead-end Age control. Feedback and final stats must remain readable with reduced motion, without relying only on a red/green flash.

### Codex browser review gate

After implementation reports, capture real-browser screenshots at **390×844 and 320×844** under `docs/collab/evidence/redesign-v2/feel/`, recording the reviewed revision and any working-tree changes. Compare Home, Choice and Person against the v2 `.dc.html` mockups and F1–F14, with F1–F9 taking precedence where this feel pass deliberately changes the original mockups (notably chronological journal, centered event card and pinned Age). Capture home before/after aging, event, resolved outcome, person and narrow-screen overflow cases. Record interaction results separately from screenshot appearance; DOM/unit tests alone do not satisfy this gate. Small confirmed fixes are Codex's; larger gaps return to the assigned owner through Claude.
