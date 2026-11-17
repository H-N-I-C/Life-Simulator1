# Q1 A1 report — server-side crash safety (Kimi A, 2026-11-17)

Scope: `web/server.py`, `src/game/service.py`, new `tests/test_game_crash_safety.py`.
Note: `web/server.py` is normally B's file, but Q1 assigns server crash safety
(and the `/api/client-error` endpoint, which B1 explicitly waits on A for) to A1.

## What changed

**Crash logging + typed 500 (web/server.py)**
- Rotating log at `logs/server.log` (1 MB × 3 backups, stdlib
  `RotatingFileHandler`); wired in `make_server(port, save_dir, log_path)` so
  tests use a temp log.
- Every unhandled (non-`GameError`) exception in GET and POST routes is caught
  by `_report_crash`: generates a 12-hex crash id, logs traceback +
  request summary (method, path, request_id when parsed, client ip), and
  replies `500 {"error": "internal", "crash_id": "..."}`. Tracebacks never
  reach the client. `do_GET` also got a top-level wrapper so faults outside
  route handlers (e.g. in header writing) are logged too.
- `GameError` responses (deliberate validation) are unchanged.

**Save integrity (src/game/service.py)**
- `_save` was already atomic (tmp + `os.replace`); it now first moves the
  previous good save to `game_saves/<id>.pickle.bak`. `command()` already
  only saves after success, so a mid-command exception never touches the save.
- `_load` splits into `_read_save(path)` + fallback: if the primary is missing
  or corrupt and a `.bak` exists, it loads the backup, records a one-shot
  notice ("recovered your game from the last-good backup…"), re-persists the
  primary from the backup, and serves normally. `get_state`/`command` attach
  the notice to the next response only. Both corrupt → typed 500
  ("save data is corrupt") as before.
- `delete_game` also removes the `.bak`.

**Endpoints**
- `GET /api/health` → `{"status": "ok", "service": "src.game.service"}`.
- `POST /api/client-error` (for B1; shape frozen here since B asked for it):
  body `{kind, message, stack?, url?, game_id?}` — `message` required string,
  rest optional strings; logged to server.log as `client-error …`;
  returns `{"ok": true}`. Bad shape → 400 `invalid_input`.

## Tests — tests/test_game_crash_safety.py (11 tests, all OK)

- Backup created on each save holding the previous revision.
- Forced `RuntimeError` mid-`age_up` (mocked `coordinator.run_turn`, a true
  I/O-adjacent boundary): save revision unchanged on disk, subsequent real
  command succeeds — at service level AND over HTTP (HTTP version also
  asserts 500 body `{error:"internal", crash_id}` and that the log contains
  the crash id, `RuntimeError`, and the request id).
- Corrupt primary → recovers with notice, primary restored, notice one-shot.
  Missing primary → same. Both corrupt → typed 500.
- `delete_game` removes both files.
- `/api/health`, `/api/client-error` (happy path + 400).

**Fails without the fix:** `git stash` of the two source files → suite fails
(2 failures + 3 errors); restored → 11/11 OK.

**Verification:** new suite green under PYTHONHASHSEED 0/42/12345; adjacent
suites `test_game_service`, `test_web_m1`, `test_web_m2` — 95 tests OK
(10 pre-existing skips). flake8 not installed here; style matched by hand.

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real GameService + real HTTP server in tests; the only mock is the
      injected mid-command fault (`coordinator.run_turn`), a true boundary.
- [x] Each fix has a failing-without-fix test (verified via `git stash`: 5 fail).
- [x] No RNG changes; no new randomness.
- [x] Inputs validated: `/api/client-error` bad shape → typed 400; corrupt
      saves → typed GameError 500, never raw exceptions.
- [x] No money/ledger/identity changes (not touched).
- [x] Save/load: `.bak` files are plain copies of existing envelopes; old
      saves load unchanged (fallback reuses the existing read+migrate path).
- [x] No gates/costs/actions changed; no soft-lock surface added.

Hygiene
- [x] Only my files + the A1-assigned `web/server.py`; `web/server.py` and
      `service.py` were re-read fresh before editing.
- [x] No probability/balance changes.
- [x] Focused tests pass; new suite run under 3 PYTHONHASHSEED values.
- [x] n/a UI (A task).
- [x] Rare path forced (injected exception, deliberate file corruption),
      never left to chance.
