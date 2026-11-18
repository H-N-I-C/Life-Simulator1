# v2 Stage 2 consolidated evaluation — Codex, 2026-11-18

**Latest outcome: Stage 2 PASS after H1 correction — 799 tests OK (9 skipped), browser 316/316.** The original findings below are retained; the fix and final re-evaluation are recorded at the end.

**Initial verdict: NEEDS-FIX (superseded by the H1 re-evaluation below).** Home/Choice are integrated and the accepted M2.5/visibility flows remain green, but the main Age control gets stuck after its first successful command. This is a core repeat-play defect, not a Stage 3 omission. Exact-person Edit and the final parity/visual pass remain scheduled separately.

**Full suite: 793 tests, OK, 9 skipped, 214.976 seconds.** Evidence `evidence/stage2/full-suite.log`. Green discovery does not include the extra failing repeat-Age reductions documented below.

## H1 — P1 / FAIL — B: central Age remains disabled after successful ageing

`web/static/v2/home.js:101` waits for the shell command and clears `ctx.busy` in `finally`, but never renders after clearing it. The shell renders the fresh authoritative state **before** returning/resolving the transport promise (`game.js::sendOperation`), so Home's update still sees `ctx.busy === true` and leaves the button disabled. The later flag change does not update the DOM.

Forced real-browser reproduction with a new seed-42 life:

1. Open Home; central Age is enabled.
2. Click Age and wait for the real `/actions` response and render/settlement.
3. Read the authoritative persisted state and rendered button.

Result: player **age 1**, **no pending decision**, server still advertises enabled `age_up`, but Home button **disabled:true**, note **“Ageing…”**. No console errors. Evidence and runnable cleanup-aware reduction: `evidence/stage2/age-repeat.json`, `age-repeat.log`, `age_repeat.py`. The General action remains a workaround; this is not a claim that the entire save is irrecoverably locked.

**Correct design:** once the local operation settles, clear busy and render from `hooks.getState()` (guard an unmounted/stale handle), or make busy entirely shell-owned and render on the shell's final operation lifecycle. Do not optimistically enable unconditionally: an actual pending decision, death, rejection, or uncertain-outcome/retry state must still use its authoritative gate.

**Required regression:** two consecutive successful central-Age clicks through the real browser/service with no intervening navigation/reload. After each command, compare the actual button to the current advertised action; force both a no-pending year and a pending-decision year. Also check settlement on rejection/uncertain result. A component test must model the actual order: shell updates Home with fresh state *before* its returned promise resolves.

## Test blind spots to correct with H1

- `tests/test_web_v2_shell_browser.py:138` clicks Age only once, checks the first age/revision increment, and never asserts the button is reusable. It therefore passes despite the repeat-play failure.
- `tests/web_v2_home_dom.mjs:157` resolves the fake operation, then explicitly calls `h.update(state())` **after settlement**, reversing production order. The review's `age_settlement.mjs` forces the production order against the actual Home module and fails with `disabled:true`.
- The DOM harness `test(name, fn)` does not await async test callbacks before counting/reporting passes. Unhandled rejections may eventually fail Node, but “N passed” is premature. Await each test (or use a real async test runner) and provide a negative async assertion to prove the harness correctly fails. Keep real-service/browser tests for lifecycle assertions.
- The live paid/free choice test accepts missing/any nonempty effect labels instead of forcing both cases. The DOM test does render authored “Free”, but the integration test should force real paid and always-available free/decline branches and check exact labels/eligibility; don't claim those branches were both exercised merely because naming succeeded.

## Passing work

- **A / display metadata:** additive `display_category` is a pure mapping of public pending kind with a null fallback, passed through the adapter; no private evaluator score, fake Agreement percentage or new gameplay state. Existing Home data remains authoritative.
- **B / Home:** real stats/numeric labels; advertised age action; newest-first display without mutating server journal. The operation lifecycle failure above is the blocker, not the data source.
- **B / Choice:** reusable input builder, option-specific inputs, disabled reasons and authored known effects, variable option count, safe text rendering, current-decision rerender for chained pendings and collapse/reopen support are present. Naming and death/continuation browser tests are included in discovery. No unbacked certainty/probability hints introduced.
- **C / Person:** frozen normalized-action handling and string limits remain; exact-person Edit intent passes immutable IDs. The shell/editor selection wiring is still Stage 3 and is not accepted as complete by this review.
- **D / current browser parity:** independently run, unmodified current-tree M2.5 plus visibility flows: **316 PASS / 0 FAIL / 0 PENDING / 0 WARN**. Evidence `evidence/stage2/browser/report.md`. The previous combined-flow game-selection issue is no longer failing this run. This does not override the extra repeat-Age failure outside those flows.

## Execution notes

All authoritative runs use workspace temporary directories and explicitly isolated Chromium profiles, with review-owned process groups reaped and profile directories removed. The initial review harness used too long a workspace temp prefix; Chromium aborted with `Socket path too long`. That run was stopped, preserved as `full-suite-long-path.log`, and rerun with short `r-*` temp roots. The startup failure is a **review harness issue**, not a game failure or a reason to count skipped browser work as passing. Both actual-browser Age and parity probes ran successfully after correcting it: Age confirmed the product defect; parity assertions passed. Final suite cleanup records ten Chromium launches and `temp_root_removed:true`; the Age/parity runs record their own successful cleanup.

No production code changes made during this evaluation. Claude should assign the H1 fix/regression and the associated test-harness correction before Stage 2 acceptance; Stage 3 work need not be confused with this defect.

## Self-check

- [x] Branch `dev`; current reports/contracts and actual Home/Choice/shell sources reviewed.
- [x] Real persisted GameService and Chromium used for the blocking finding; both server eligibility and actual DOM state recorded.
- [x] Separate component ordering reduction forces the cause; existing green tests distinguished from missing repeat-play coverage.
- [x] Accepted M2.5/visibility browser flows independently green at 316/316.
- [x] Public-only display metadata, IDs, authoritative stats/money/actions and pending/retry boundaries reviewed; no gameplay or balance changes introduced.
- [x] Only own report/evidence/status changed; no edits to B/C implementation or tests.
- [x] Full discovery result recorded above with skips/time explicit; initial harness failure preserved.
- [x] Review-owned Chromium profile cleanup verified; source of startup issue explained rather than attributed to the game.
- [x] Stage 2 blocker distinguished from Stage 3 editor and Stage 4 visual work; lead notified.

## H1 fix and Stage 2 re-evaluation — Codex (2026-11-18)

Assigned by Claude: Home busy settlement/unmount guard, corrected async harness and ordering, repeated real Age + pending/rejected/uncertain regressions. No other feature implementation changed. One shared browser-test readiness guard was added after full discovery exposed an actual race (details below).

### Fix

`web/static/v2/home.js` now clears local busy on settlement, then renders **the latest `hooks.getState()`**, rather than assuming the shell will render again. Authoritative pending/death/eligibility gates therefore still apply. A module-private WeakMap tracks the mounted context: both exported and handle unmount paths invalidate it, old handle updates become harmless, and an old promise or old handle unmount cannot mutate a replacement Home mounted on the same root.

The Home DOM harness now registers tests, **awaits each callback**, and only counts/logs a pass after it completes. Its busy regression reproduces production ordering (fresh shell render before transport resolution) and performs two consecutive clicks. Added pending/rejection/uncertain cases plus late-settlement and remount guards. A Python negative-control test deliberately runs a deferred failing assertion and verifies the harness reports FAIL and exits 1; this is an expected test of the harness, not a skipped failure.

### Forced real-browser/service coverage

New `tests/test_web_v2_home_lifecycle.py` uses the real temporary server, persisted GameService and Chromium:

1. Two consecutive central-Age clicks: ages 0 → 1 → 2, distinct request IDs, no intervening navigation or manual re-render, button matches the enabled server action after each settlement.
2. Resume a genuine yearly cursor at age-17 education with deterministic smarts/grades/RNG; Age produces the real university-application pending, with no age action advertised and the button disabled after settlement. No fake decision/response is installed.
3. A second real client spends time with a parent to advance revision; the stale browser Age receives real HTTP 409, does not advance age, reconciles, and returns to the actual eligible action state.
4. Commit a real Age request, discard its first response **only at the browser I/O boundary**, then use the existing safe-retry pill. Both requests have identical payload/request ID; the replay adds no revision and age stays 1. No service/ledger mock hides duplicate execution.

### Verification

- Corrected-order DOM tests **failed before** the implementation change (`evidence/h1/dom-before.log`, four failures). The prior real-browser blocker record remains at `evidence/stage2/age-repeat.json`.
- Final Home harness includes 12 awaited tests; unmount through either API and remount safety covered.
- New browser + Home wrapper tests: **8 tests OK under PYTHONHASHSEED 0/1/42** (`evidence/h1/hash*.log`). Each run includes four real Chromium journeys and the async-harness negative control; all profile roots removed.
- Current-tree M2.5/visibility browser parity is **316/316 PASS** (`evidence/h1/browser/report.md`). **Final full discovery: 799 tests, OK, 9 skipped, 233.148 seconds** (`evidence/h1/full-suite.log`). Final cleanup confirms 15 Chromium launches and the isolated root removed. The first full run (799 tests) caught one browser-helper readiness race: `_launch` waited for shell chrome but not the asynchronously imported Home, so the offline test attempted `null.click`. Preserved as `full-suite-readiness-race.log`. After re-reading the shared test file, only that helper was changed to wait for the actual `.v2h-age` control as well. The previously failing offline test now passes (`offline-readiness.log`); B/Claude were notified to preserve the guard. No further production implementation changed.

### Final verdict

**H1 CLOSED; Stage 2 PASS.** Main Age now supports repeated real play and obeys the authoritative gate after pending, rejected and uncertain outcomes. The corrected async harness and real lost-response replay regression cover the earlier blind spots. B may resume Home work; preserve the shared browser helper's actual-Home readiness guard. No broader v2 user-handoff or Stage 4 visual certification is implied.

### Self-check

- [x] `dev` verified; Home re-read before the exclusive assigned edit; no B/C feature changes outside this fix.
- [x] Real service/browser regressions force success, pending, real stale rejection and committed-response loss; mocks only at the I/O fault boundary.
- [x] Red-before/green-after ordering regression preserved; async negative control verifies the harness really awaits failures.
- [x] Current authoritative action gates retained; no unconditional re-enable or local age/money/RNG change.
- [x] Both unmount APIs and stale/remounted handles guarded and tested.
- [x] Three hash seeds green; syntax and diff whitespace checks clean.
- [x] Full integrated Stage 2 re-evaluation PASS: 799 tests OK / 9 skipped; parity browser 316/316.
- [x] Workspace TMPDIR, short browser socket paths and review-owned Chromium profile/process cleanup used.
- [x] Report/status updated and lead notified; Stage 3 integration and Stage 4 visual acceptance remain outside this H1/Stage 2 verdict.
