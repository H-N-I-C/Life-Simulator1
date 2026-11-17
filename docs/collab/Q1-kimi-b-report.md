# Q1 B1 report — front-end resilience (Kimi B, 2026-11-17)

Scope: docs/collab/Q1-quality-sprint.md task B1. Files owned and changed:
`web/static/common.js`, `web/static/game.js`, `web/static/creator.js`,
`web/static/game.html`, `web/static/creator.html`, `web/static/gallery.html`,
`web/static/relationships.html`, `web/static/style.css`;
tests: `tests/test_web_b1.py`, `tests/web_b1_dom.mjs`. Nothing outside
`web/**` + my test files was touched.

## What landed

1. **Global error boundary** (`common.js`, installed on import by every
   page): `window.onerror` + `unhandledrejection` →
   `reportClientError()` → fire-and-forget `POST /api/client-error`
   (fetch with `keepalive`, sendBeacon fallback), plus
   `showCrashCard()`: a modal "Something went wrong. Your save is safe."
   card with **Retry** (calls a per-page registered retry handler;
   default `location.reload()`) and **Reload**. Reporting is deduped
   (5 s per identical message), capped at 20/session, and swallows
   endpoint failures. **Report shape is A's frozen shape**
   (docs/collab/q1-a1-report.md): `{kind, message, stack?, url?,
   game_id?}` — we map type→kind, page→url, and fold
   source/lineno/colno into the stack string. No extra fields.

2. **Offline state**: sticky offline banner on every page
   (`online`/`offline` events), and `api()` in game.js throws a friendly
   "You are offline — reconnect, then retry. Your save is safe." typed
   error (which the existing pending-op pill then handles as an
   uncertain result → safe retry).

3. **Loading / empty / error states for every screen**:
   - game: `#pageLoading` spinner on initial load and game fetch;
     resume list keeps its "Loading…" + gains a Retry error card;
     `showFatalLoadError()` full-view card (Retry / All lives) when the
     current game can't be loaded (404 still returns to the list);
     timeline empty states ("Nothing has happened yet…" / "A quiet life
     so far."); family empty state ("No family or relationships yet.").
   - creator: "Loading…" placeholder in options; catalog failure shows
     the crash card with Retry = reload (module halts cleanly, no
     unhandled rejection).
   - gallery: per-section "Loading…", error + Retry, empty handling.
   - relationships: loading / error-with-retry / empty states.

4. **Duplicate-submit protection on every action button**:
   - action modal Confirm disables itself before dispatch;
   - decision modal freezes all option buttons on click (plus the
     existing global `busy` gate);
   - legacy "Continue as a child" buttons all disable on click.
   All gameplay commands already funnel through the retry-stable
   operation machinery, so a double click can never apply twice.

5. **Crash fix found while wiring B1**: `continueAs()` called an
   undefined helper `attempt(...)` — continuing as a child would have
   hard-crashed (ReferenceError). It now goes through
   `startOperation("continue_as", {target_id})`, the same persisted,
   replay-safe path as every other command. Verified `continue_as` is a
   real service action (`src/game/service.py:1180`) and returns typed
   4xx errors (e.g. `game_over` for a living player, never a 500).

## Contract with Kimi A (via Claude)

**Resolved — A's frozen shape adopted** (docs/collab/q1-a1-report.md):
`POST /api/client-error`, JSON `{kind, message, stack?, url?, game_id?}`
(all strings; message required). The client maps type→kind, page→url and
folds source/lineno/colno into `stack`; it sends no other fields. Pinned
by `test_client_error_bad_shape_is_typed_400`.

## End-to-end proof

`test_client_error_lands_in_server_log` boots the real server with a temp
`log_path`, POSTs the exact payload common.js builds, asserts `{"ok":
true}` and that `logs/server.log` contains the `client-error` entry with
kind/url/game_id/message.

## Tests

- `tests/web_b1_dom.mjs` (node, real `common.js` with stubbed browser
  globals): 9 behaviour tests — frozen-shape report, stack folding,
  crash card content and buttons, dedupe, 20-report cap, fetch-failure
  tolerance, game_id attachment, offline banner show/hide,
  Retry-vs-Reload behaviour.
- `tests/test_web_b1.py`: 9 tests over the real HTTP layer — pages and
  assets served with the wiring, marker tests for guards/states,
  `continue_as` typed-error test, client-error 400-shape pin, the
  server-log end-to-end test, and a subprocess wrapper running the DOM
  suite.
- Focused suites: `tests.test_web_b1` (9 OK, also under PYTHONHASHSEED
  0/1/42), `tests.test_web_m1` + `tests.test_render` OK.
  `tests.test_web_m2` currently has 6 errors: scenario `married_30`
  creation returns A1's new typed 500 `{error:"internal", crash_id}` —
  caused by Kimi A's in-flight A1 edits to `src/game/service.py` /
  `src/game/events.py` (dirty in the working tree), NOT by B1. Flagged
  to Claude; A's crash-safety itself works (crash id returned, no
  traceback leak).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Tests use real objects at real boundaries: DOM tests exercise the
  actual `common.js` module; HTTP tests run the real server + service.
- [x] Each fix has a failing-without-it test (see above).
- [x] Randomness: no RNG use added.
- [x] Inputs validated: no new input surfaces; offline/typed errors only.
- [x] Money/ledger: untouched. Identity: untouched.
- [x] Save/load: untouched.
- [x] Shown costs/eligibility: untouched.
- [x] Age/state gates: untouched (server-side already).
- [x] No soft-locks: every new error state offers Retry/Reload/All-lives;
  decision pill flow unchanged.

Hygiene
- [x] Only owned files edited (`web/**`, `tests/test_web_*`, plus the new
  node test); status row updated.
- [x] No probability/balance changes.
- [x] Focused tests pass; new suite run under 3 PYTHONHASHSEED values.
- [x] UI: buttons are native (keyboard OK), Retry gets initial focus,
  no new fixed widths (phone-safe), contrast dark-on-white/~12:1,
  avatar rendering untouched (every person still gets one).
- [x] Rare paths forced (error handlers invoked directly; offline via
  navigator stub), no random-roll reliance; earlier fixes grepped intact
  (`trapModalFocus`, pending-op machinery still present).
