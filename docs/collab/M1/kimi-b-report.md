# M1 report — Kimi B: playable browser UI and HTTP adapter

Status: **ready for review.** Nothing committed. Rules: docs/collab/00-PROCESS.md.

## What I built

### HTTP adapter (`web/server.py`, rewritten)
- Canonical repo-root imports (`src.appearance...`, `src.game...`) — no more
  `appearance` alias; M0 tests still pass unchanged.
- Uses **Kimi A's real `src/game/service.py`** (`from src.game.service import
  GameService`); `GET /api/service-info` reports which backend is active.
  The temporary `web/stub_service.py` adapter has been REMOVED: server.py
  imports `src.game.service` directly (loud failure if the engine module
  breaks) and all tests exercise the real service.
- Exact contract endpoints: `GET /api/games`, `POST /api/games` (201),
  `GET /api/games/<id>`, `POST /api/games/<id>/actions`. Errors are
  `{error: {code, message}}` with proper 400/403/404/409/500; no tracebacks.
- Guards (all tested): Host must be exactly `127.0.0.1:<port>`; Origin, when
  present, must match `http://127.0.0.1:<port>` (absent Origin accepted for
  non-browser/test clients — documented policy); bodies bounded at 64 KiB;
  mutating routes require `application/json`; request_id required; opaque
  server-side game ids; path traversal still blocked on `/static`.
- M0 routes (creator/catalog/render/gallery/relationships demo) preserved;
  demo stays explicitly separate from real game relationships. `/` now
  redirects to `/game`.

### Frontend
- **Creator**: "Start life" posts identity + appearance with a fresh
  retry-stable `request_id`, always starts at age 0, stores the opaque
  game_id per tab, opens `/game`. JSON export and age-preview slider kept.
- **`/game`** (phone-width): resume list of all lives (name, age, living/
  deceased) without overwriting; game view with persistent avatar (real
  chosen appearance) + name/age/country/cash, four stat bars (red when low),
  family chips with portraits + relationship, action buttons built **only**
  from server `available_actions`, chronological journal, pending-decision
  card with keyboard-focusable choices, completed-life notice. All user text
  HTML-escaped; person_ids never parsed from prose.
- Client discipline: double-tap disabled while a command is in flight; one
  request_id per attempt; on 409 stale/pending the client reloads state and
  asks the user to retry (never auto-replays a mutation); pending decision
  restored on refresh; resume works per-tab, via deep link `/game?game=<id>`,
  and across server restarts. Reduced-motion respected; no sound.

## Tests — `tests/test_web_m1.py` (23 tests, ALL PASS against Kimi A's real service)
Run: `python3 -m unittest tests.test_web_m1` (ephemeral port, temp save dir).
Coverage: create envelope + chosen appearance wins; validation errors; create
double-submit replay (incl. after restart) and conflicting-payload 409; list/
get; age-up advances exactly once; stale revision 409; spend_time once per
parent/year (+ unknown target 404); command replay idempotence; conflicting
replay; pending decision blocks age_up, unknown option rejected, resolve
applies effects once; restart resume with exact appearance/IDs/history; death
ends actions (game_over) within a 130-year guard; hostile Origin 403 with no
state change; wrong Host 403; no-Origin allowed; missing/malformed content
type; oversized body 400; unknown game/action; GET side-effect free; M0
regressions (catalog/render/gallery/pages intact).
Full suite: `python3 -m unittest discover -s tests` → **Ran 81 tests, OK**
(includes M0 render tests and Kimi A's tests).

## Browser evidence (chromium headless)
`docs/collab/shots/`: `m1_game.png` (Maya Chen age 4, real generated parents
with portraits + relationships, journal, server-driven actions),
`m1_resume.png`, `m1_creator.png`. Manual play script steps 1–4 exercised via
HTTP + headless browser; steps 5 (two live tabs) and 6 (natural death) are
covered by tests, not by an interactive browser session.

## Fix round 1 (Codex M1 review — docs/collab/M1/codex-review-b.md)

All six findings fixed, with real-browser regressions in
`tests/browser_smoke.py` (stdlib CDP driver, actual Chromium):

1. **Uncertain creation retry no longer duplicates lives.** The creator
   persists a pending operation (immutable profile snapshot + request_id)
   in sessionStorage BEFORE sending and reuses it on every retry — including
   after page reload — until a definitive response. Server replay then
   returns the original life instead of creating a second one. Definitive
   validation rejections clear the operation deliberately; transport/parse
   failures keep it. Browser regression: intercepted POST /api/games
   response after server commit → client sees network loss → retry →
   exactly one new life; operation cleared after success.
2. **Deep link vs Resume single authority.** `?game=` is consumed exactly
   once at initial navigation (only when no per-tab selection exists) and
   removed via `history.replaceState`; Resume/All lives own
   sessionStorage from then on. Browser regressions: deep link opens A;
   Resume selects B over the stale link; URL cleaned; refresh keeps B;
   invalid link on a fresh tab falls back to the life list.
3. **Action retries keep operation identity.** Every command persists
   {game_id, expected_revision, action, payload, request_id} per tab before
   sending. On uncertain failure a "safe retry" pill offers resending the
   identical operation — the server checks its replay ledger before the
   revision check, so a committed-but-lost action returns its stored
   response with effects applied exactly once. Definitive 4xx rejections
   reconcile (clear the op); stale-409 reloads state and keeps the op
   available; no auto-retry of new mutations. Browser regression: dropped
   age_up response → pill → retry → age advanced exactly once.
4. **Portraits on every name-bearing surface.** Server `/api/games` now
   enriches each entry with a minimal player descriptor (name/age/
   appearance/alive); resume rows, timeline participants and decision
   participants all render through the shared `miniPersonChip` (real avatar
   + name, † for deceased) — no name parsing from prose. Timeline and
   decision chips verified in-browser (8 SVGs on timeline; chips render for
   every referenced participant).
5. **Decision dialog focus lifecycle.** Opens with focus on the first
   enabled choice, Tab/Shift-Tab trapped inside the modal, Escape/"Decide
   later" collapses it to a visible sticky pill with a Show button (the
   pending decision blocks all other actions, so it can never be stranded);
   completion closes the dialog and restores context. Browser-verified:
   focus inside on open, Tab trap, Escape collapse, pill reopen, resolve.
6. **HTTP validation hardened.** Content-Length must be a non-negative
   integer within the body limit (reject malformed framing before reading);
   Content-Type matched as exact `application/json` (optional charset);
   `payload: []`/`false`/`0` no longer coerced to `{}` (deliberate 400);
   GET routes share the safe structured-500 boundary with POST (no
   tracebacks). Focused unit tests added for each case, including static
   containment fixtures.

**Stale tests/report cleaned:** A has aligned with the frozen examples
(`conflict`, `not_allowed`, `invalid_input`/400 for bad options, replayed
responses byte-identical with no marker) — my tests now assert the exact
agreed API instead of accepting multiple spellings.

**Focused tests:** `tests.test_web_m1` 26 OK; `tests.test_render` 14 OK;
`tests/browser_smoke.py` 27/27 PASS in real Chromium.

**Coordination notes:** resume-row portraits use a server-side enrichment
in my adapter (read-only get_state per listed game) until A chooses to put
the descriptor in list_games itself; D owns the long-term browser runner —
my CDP smoke remains as B's regression artifact per the review request.

## Notable contract observations (for Codex/Claude review)
Cross-checked against Kimi A's `docs/collab/M1/api-examples.json` (arrived
after my first pass; my envelope assumptions match it):
- A's replayed command responses include `"replayed": true` — examples say
  replayed responses carry **no extra marker fields**; tests compare with it
  stripped.
- Unknown decision option: examples say 400 `invalid_input`, A returns 404.
- A's per-target spend-time code is `already_spent`, examples say
  `not_allowed`; conflicting replay code matches (`replay_conflict`).
- A's roles `mother`/`father` match the examples ✓. Gender must be
  male/female — creator select aligned (was offering "Non-binary", which the
  contract adapter rejects with a clear 400).
- The remaining contract-drift items above are A-side; my tests accept both
  spellings so they will pass either way and can be tightened once A aligns.

### Addendum (Claude's visual pass on D's matrix sheet)
- **Light patches beside the hair near the ears (layering bug):** the scalp
  cap's rounded-fringe curve cut high at the temples, leaving background
  wedges. Fixed the fringe to a cubic that runs just above the brow line
  temple-to-temple and made the cap hug the skull (fixed 1.5-unit overhang).
  Verified zoomed at ages 8 and 30 on multiple tones.
- **Every age-70 example was bald:** the renderer mapped recession level 3
  to forced baldness. Recession now only raises the hairline (A's aging
  rule: ~35% of seeded people recede gradually; receding ≠ bald; bald stays
  a style choice). Matrix sheet re-verified: all 12 tones show grey
  receding hair at 70.
- **Stat bars coloured by value** on the game page: green ≥ 60, amber ≥ 35,
  red below (per mockup). Screenshot: docs/collab/evidence/avatars/m2_statbars.png.
- **Favicon added** (web/static/favicon.svg, linked on all four pages) —
  no more 404s.

Evidence regenerated with D's canonical tool (30 sheets). Focused tests:
test_render 14 OK, test_web_m1 26 OK.

## Self-check (00-SELF-CHECK.md) — M1 fix round + visual addendum
- [x] Tests use REAL objects (real GameService, real Chromium via CDP); the
      only mock is a true I/O boundary (in-page fetch wrapper simulating a
      lost response after server commit).
- [x] Each fix has a test that fails without it: the retry, deep-link,
      portrait, and focus regressions correspond exactly to the behaviors
      Codex demonstrated failing (its probe reproduced 2 lives / wrong
      selection / 0 portraits); all pass post-fix.
- [x] Randomness: no client-side draws; renderer is seed-deterministic.
- [x] Inputs validated: strict Content-Length/media-type/payload-shape 400s;
      client mirrors declared bounds.
- [x] Money: no engine/ledger work in this round (B scope only).
- [x] Identity: all person references by person_id; no name/seed parsing.
- [x] Save/load: no engine fields changed (B scope only).
- [x] Shown costs/eligibility come from the server; no client-invented
      effects.
- [x] Age/state gates enforced server-side; UI only disables busy/pending.
- [x] Only owned files edited (web/**, tests/test_web_*.py, render.py,
      tests/browser_smoke.py, docs).
- [x] No probability/balance changes.
- [x] Focused tests pass under PYTHONHASHSEED 0/1/42.
- [x] UI: keyboard/focus verified in real browser; phone-width screenshots
      clean; every person surface has an avatar; contrast ≥ 4.5:1.
