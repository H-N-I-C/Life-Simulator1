# Kimi B feel-pass report (2026-11-20) — F1, F2 (web), F3, F7, F8 + C-mount

All items implemented in the web shell (`web/static/v2/shell.js`,
`home.js`, `theme.css`, `web/static/game.js`, `game.html`,
`web/static/adapter.js`). Codex's Back fixes are untouched (Back button,
priority order, history restore — all Back tests still green).

## F1 — full-screen in-game, no site nav
- `game.js render()` hides `.topbar` while a life is running; `showResume()`
  and `renderLegacy()` show it again. `theme.css` needed
  `.topbar[hidden] { display: none !important; }` because style.css's
  `.topbar { display: flex }` beat the hidden attribute.
- A running life is now full-bleed: `main:has(> #gameView:not([hidden]))`
  drops the 480px website column while playing.

## F2 — no internal IDs / phase text (web side)
- `turnCard` rendered "This year (t_000003)" + "Current phase: year_close".
  It now renders player-facing text only ("A choice is needed…" /
  "Resolving the year…"). No ids, no phase names, no dev panel fabricated.
- contracts.md §Shell pins the rule; A owns the server-side string scan.

## F3 — one big Age button in the bottom bar
- The bar is now **Occupation · Assets · AGE · Relationships · Activities**:
  four tabs (`v2-tab`) with ONE big round `+ AGE` button (`.v2-agebtn`,
  68px green circle) centred between Assets and Relationships, popping up
  out of the bar. No duplicate "+", no floating mid-page button.
- Age eligibility is still the server's advertised `age_up` spec; when
  blocked, the precise server reason shows as a one-line note
  (`.v2-agenote`) directly above the bar. Home is reached via the header
  "Life" link (unchanged contract).

## F7 — stats pinned above the bar
- New fixed `.v2-dock` above the tab bar: Happy/Health/Smarts/Looks as a
  4-up compact strip (colour + numeric %, colour never the only signal).
  Always visible while alive; hides honestly when the projection has no
  stats. The old Home stats sheet and floating Age bar were removed from
  `home.js` (Home module now only owns the ended-life note).

## F8 — tap feedback / animations, reduced motion
- Press states: tabs and options scale to .92–.97 with 120–140ms ease; the
  Age button presses in (translateY + scale, 120ms) and greys while busy.
- Stat bars animate to new values (180ms width transition) and the cell
  flashes green/red for ~0.5s on change; the % label is the primary signal.
- `@media (prefers-reduced-motion: reduce)` collapses every transition and
  animation to instant state changes.

## C's event module — mounted
- `game.js openDecisionModal()` dynamically imports `./v2/event.js`; when
  `mountEventModal` is present it is used (F5/F6: no pre-choice spoilers,
  centered card, other-person avatar, outcome popup from the server's real
  deltas). Otherwise the Stage-2 choice sheet renders — verified both ways.
- Wired per C's contract: `onResolve` returns a promise of the §Outcome
  (new `last_decision_outcome` field, exposed by `adapter.js` as
  `state.lastDecisionOutcome`); `onClose()` closes the modal.
- One real integration bug fixed on my side: the authoritative re-render
  after a resolve sees "no pending decision" and was closing the modal
  before C's popup could show. `v2EventOutcomeOpen` guards `render()`'s
  close while the popup lives.
- `event.css` is loaded from `game.html`; `theme.css` converts the box from
  bottom-sheet to CENTERED modal card when it hosts a `.v2e-card`
  (`.modal-box.v2-sheet:has(.v2e-card)`), as C's fallback comment expects.

## Tests updated / added
- `tests/web_v2_shell_dom.mjs`: new bar shape (4 tabs + central AGE, order
  asserted), stats dock, Age gating/busy/reason coverage (moved from home).
- `tests/web_v2_home_dom.mjs`: rewritten for the slimmed Home module.
- Browser tests updated for the new selectors/order:
  `test_web_v2_back.py`, `test_web_v2_home_lifecycle.py`,
  `test_web_v2_shell_browser.py` (`.v2h-age` → `.v2-agebtn`,
  `.v2h-stat-value` → `.v2-stat-value`, tab indexes, tab count 5 → 4; also
  fixed a stale-index chained-Back section and a load race).
- All suites green: shell_dom (18), home_dom (4), event_dom (14);
  final pytest sweep (shell_browser, back, home_lifecycle, shell_home,
  hidden, event, choice): 36 passed, 1 pre-existing A-side skip, 0 failed.

## Evidence (before/after at 390 and 320)
- Before: `docs/collab/redesign-v2/audit/*.png` (Claude's 390×844 audit).
- After: `docs/collab/redesign-v2/evidence/feel/`
  - `b-home-390.png` / `b-home-320.png` — full-screen, no site nav, stats
    dock, new bar.
  - `b-aged-390.png` / `b-aged-320.png` — after age-ups.
  - `b-people-390.png` / `b-people-320.png` — Relationships tab.
  - `b-event-modal-390.png` / `b-event-outcome-390.png` — C's centered
    event card + outcome popup, no spoilers, mother's avatar.
- Harness: `tools/feel_pass_shots.py` (server + headless Chromium, throws
  away its save dir under `/home/user/tmp` — TMPDIR must be on /home, the
  machine's /tmp tmpfs hits its quota).

## Notes / not mine
- Environment: `/tmp` is a 7.8G tmpfs that hits ENOSPC (disk quota) — run
  everything with `TMPDIR=/home/user/tmp`.
- F4 (journal feed), A-side F2 (server string scan) and the
  `last_decision_outcome` projection are Kimi A's (reported in
  `kimi-a-feel-report.md`); F9 is Kimi D's.


---

# Round 2 — F4/F12 feed + F8 completion (2026-11-20, after codex-feel-evaluation.md)

Scope honoured: only `renderTimeline` (game.js), `theme.css`, `shell.js`
(+ test files and comments). No edits to adapter cost plumbing, the event
relationship-delta rendering, or the modal/outcome lifecycle + focus.

## F4/F12 — viewport-filling chronological feed (game.js renderTimeline)
- Blocks come from A's `journal_feed` projection (`state.raw.journal_feed`,
  §Journal v2: one block per year, server-authored header, every line of
  the age); the flat `journal` regroups per age as the fallback for older
  projections.
- Rendered oldest → newest with one header per year; quiet-year collapsing
  removed (every lived year gets its own card, incl. the first-person
  "I turned 6." line); person chips for OTHER people only, unchanged.
- The app shell is viewport-constrained (`.v2-scope { height: 100dvh;
  overflow: hidden }`, `.v2-scroll { min-height: 0 }`) so the scroll
  region OWNS the overflow — Codex measured the old page growing to
  ~1016px with scrollTop stuck at 0. Now `.v2-scroll.scrollTop` is real,
  so the existing history save/restore helpers work for feed reading
  position too.
- Follow-the-bottom: after a render the feed auto-scrolls to the newest
  year only if the reader was within 48px of the bottom (or on first
  paint); otherwise the previous scrollTop is preserved. Back restoration
  flows through the same scroller.

## F8 — stat persistence + age tick (shell.js)
- `renderStats` keeps cells/fills alive across syncs (`statCells` map):
  existing fills get `width` updated after paint with the `.animate`
  class, so the CSS transition actually runs (Codex §5: rebuilding the row
  put every bar at its final width with no animation). Flash: class
  removed/force-reflow/re-added on repeat changes. Removed stats disappear;
  new stats snap in.
- Age tick: header now carries an "Age N" chip (`v2-header-age`) that
  counts up/down from the previously rendered age over ~450ms
  (`renderAgeTick`); `prefers-reduced-motion` (or no rAF in test hosts)
  sets it instantly. Timer cleared on unmount.

## Tests
- `tests/web_v2_shell_dom.mjs`: +2 F8 tests (fill element identity across
  syncs + animate/flash classes; header age chip; async tick 27→30 through
  intermediates with rAF stubbed). 20/20 pass.
- `tests/test_web_v2_shell_browser.py`:
  `test_home_stats_age_and_newest_first_timeline` replaced by
  `test_home_stats_age_and_chronological_feed` — asserts chronological
  order, viewport-constrained scroller (scrollHeight > clientHeight after
  several years), bottom-following after age-up, and older-reading-position
  preservation when scrolled up.
- Stale "newest-first" comments updated (`home.js`, `game.js`;
  `TURNED_RE` removed — the aging line now renders as first-person
  content). contracts.md §Journal already specified oldest→newest.

## Checks run
- `node tests/web_v2_shell_dom.mjs` — 20 passed
- `node tests/web_v2_home_dom.mjs` — 4 passed
- `node tests/web_v2_event_dom.mjs` — 18 passed
- `pytest tests/test_web_v2_shell_browser.py tests/test_web_v2_back.py
  tests/test_web_v2_home_lifecycle.py tests/test_web_v2_shell_home.py
  tests/test_web_v2_hidden.py tests/test_web_v2_event.py
  tests/test_game_v2_choice.py` — see status row for the final tally
  (all green at report time).
- Screenshots re-captured with `tools/feel_pass_shots.py` after the change.


## Round 3 — review fixes (2026-11-20): scroll gating, tick speed, 320 header, Happy label

- **Feed follow gating (F4/F12):** `renderTimeline` only follows the bottom
  when Home is the active screen; on other tabs the shared `.v2-scroll`
  position is always preserved. First paint opens at the newest year;
  `timelinePainted` is only consumed by a render with a live scroller
  (an early pre-shell render must not burn the one-time follow).
- **History seed:** the base history entry is seeded WITHOUT `scroll`,
  and `v2Apply` restores scroll only for finite values (mirrors the panel
  branch), so first load/resume opens the feed at the newest year while
  real Back/scroll restoration (captured by v2Push) is unchanged.
- **F8 tick speed:** the age tick now lands inside the 120–200 ms window
  (~180 ms total, `max(30, 180/steps)` per year).
- **F7 label:** the dock displays "Happy" for the `happiness` key
  (`STAT_LABELS` in shell.js — projection key unchanged); verified
  unclipped at 320px (scrollWidth ≤ clientWidth for all four labels).
- **320 header:** the age chip is a separate flex child
  (`.v2-header-namerow`: name `flex: 0 1 auto` with ellipsis, chip
  `flex: 0 0 auto`), so a long name truncates without swallowing the age.
- Tests: `test_home_stats_age_and_newest_first_timeline` replaced by
  `test_home_stats_age_and_chronological_feed` (chronological order,
  viewport-bounded scroller, bottom-follow, older-position preserved);
  shell_dom gained F8 persistence/tick tests and the Happy-label
  assertion. Suites: shell_dom 20, home_dom 4, event_dom 19; browser
  sweep 36 passed + the feed test green individually. Harness note:
  `tools/feel_pass_shots.py` reuses ONE game across both viewports, so its
  320 shots open a resumed life at Age 3/6 — a harness artifact (fix is
  D's to coordinate), not a feed regression; independent fresh-game
  harnesses pass bottom-follow at both widths.
- Deferred to Codex: person.css portrait/role overlap on the Person
  header (mother role text under the portrait).


## Round 4 — resume-load follow fix (2026-11-20, final narrow fix)

- Reproduced on the production path only: a FRESH browser opening a saved
  life with an overflowing feed rendered at the oldest year. Cause: at
  first paint the feed content still fits (person chips render after
  their portrait fetch), so the single `scrollTop = scrollHeight` write
  clamped to 0; content then grew and nothing re-pinned.
- Fix (renderTimeline only): after an authorized follow (Home active,
  reader near bottom / first paint), re-pin to the bottom on each
  requestAnimationFrame for ~0.5 s (30 frames) while content settles.
  The branch is never entered when the reader scrolled up or on other
  screens, so preserved positions are untouched. Verified: fresh browser
  opens the saved life at `scrollTop == scrollHeight - clientHeight`
  (newest year visible) at 320px.
- All temporary instrumentation removed. Shell/tests frozen for
  independent final review.
