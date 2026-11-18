# v2 Stage 1 consolidated evaluation — Codex, 2026-11-18

**Verdict: NEEDS-FIX for Stage 1 navigation; substantial progress, but not ready for user handoff.** The earlier identity/dependency stall is resolved: contracts and A's projection exist, B's shell mounts C's Person module, and D recorded a baseline. Home/Choice redesign is explicitly Stage 2, so its absence is not a new Stage 1 failure.

Full discovery: **776 tests, OK, 9 skipped, 165.995 seconds** (`evidence/stage1-full-suite.log`). Current-tree M2.5 Chromium flows: **298 PASS / 0 FAIL / 0 PENDING / 0 WARN** (`../../M2.5-review-evidence/evaluation-v2-stage1/browser/report.md`). These green checks miss the two directly reproduced navigation failures below.

## Blockers

### S1 — P1 / FAIL — B: leaving the game does not hide its shell

`web/static/v2/theme.css:66` gives `.v2-scope` `display:flex` without an explicit hidden-state override. This overrides the browser's default hidden presentation of `#gameView`.

Forced real Chromium journey: open a real married life → People → Person → Back/secondary navigation → All lives. DOM result: `gameView.hidden === true`, **`gameView.checkVisibility() === true`**, computed display `flex`, and the resume screen also visible. The old game remains rendered beside the lives screen. The same shared hide path is used by legacy/death presentation, so verify that too rather than assuming a property assignment hides a flex container.

Guidance: preserve the semantic hidden contract explicitly in scoped CSS (e.g. `.v2-scope[hidden] { display:none; }`) and check actual visibility, not just `.hidden`. Add real-browser All lives → resume/new life and death → continuation/archive regressions. See `evidence/stage1-browser-review.json` (`all_lives`) and runnable `stage1-browser-review.py`.

### S2 — P1 / FAIL — B: More → Character editor opens an invisible destination

`web/static/game.js:1177` only clicks the existing toggle. `editor.js` mounts its panel under the General/actionList container, which is now inside the Activities section (`game.html:72`). From Home, the panel becomes `hidden:false` but **`checkVisibility() === false`**, because Activities stays hidden. The advertised secondary route therefore appears to do nothing.

Guidance: make secondary routes navigate to/show their owning surface before opening it, or give the editor a dedicated visible host. Opening a route should be idempotent, not toggle an already-open panel closed. Verify visibility from every primary screen, including Person Back/Edit, and retain preview/apply/pending-supersession behavior. Audit Family tree and Dating & weddings through their actual hosts too; a synthetic click on a hidden toggle is not a navigation implementation. Evidence `stage1-browser-review.json` (`editor_from_home`).

## Integration items to complete before full v2 handoff

### S3 — P2 / NEEDS-FIX — B/C: raw Person actions bypass the frozen adapter

`web/static/v2/shell.js:165` returns raw detail JSON straight to `mountPerson`, while A intentionally supplies advertised snake_case action specs and the frozen contract says the client normalizes them. `person.js:114` reads `knownCosts`; raw wire specs use `known_costs`. Thus price metadata will be dropped instead of rendered. Additionally `person.js:121` handles object limits only, while existing actual limits include strings such as “once per year per person”. The real Person screen omitted that limit for an available Spend time action.

Guidance: normalize detail actions exactly once through the existing `normalizeAction` at the shell/adapter boundary; keep A's wire shape unchanged. C should render both string and supported object limits. Test a real advertised spec with a known cost, a string limit and a disabled reason through the HTTP view and browser DOM, not only normalized hand-built fixtures. Enabled/reason already match in both dialects, so this does **not** reopen the original disabled-state bug.

### S4 — P2 / scheduled Stage 3 work — B: Person Edit is exposed before exact-person selection is implemented

`game.js:1181` discards the supplied person ID and merely clicks the general editor toggle. The actual Person Edit probe closes the overlay but does not reveal the editor; depending on its prior state the click may also close the panel. No selected-person match is established. This is marked Stage 3 in source, so it need not block parallel Home/Choice work, but do not call the current Edit control complete. Implement explicit `openEditor(personId)` with visible navigation and exact selection, or visibly mark that control unavailable until wired. Test identical names and repeated opening.

### S5 — P2 / QA and contract hygiene

- The contract introduction still calls §Person provisional although A's later section is frozen. C's status still says it is awaiting A/B contracts that now exist. Update those contradictory statements; no new dependency hold is necessary.
- D's baseline intentionally uses accepted pre-v2 content. It is useful parity inventory, not proof that current v2 More routes are visible. Add the S1/S2 journeys to current-tree checks.
- `contracts.md` claims `#16a34a` on white is approximately 4.7:1 and that 12px bold qualifies as large text. Those accessibility claims need correcting and actual rendered contrast must be measured in the visual pass. No fresh full contrast certification is granted here.

## Passes and evidence

- **A projection PASS:** observer-safe detail based on existing exposed basics/relationship, empty unapproved descriptors, advertised target actions, typed unknown-person error. Uses the same extracted advertised-action helper as state. Privacy/projection tests cover unrelated adult, child, deceased and unknown IDs; no privileged job/faith/generosity/NPC stat data added.
- **Original disabled-action finding PASS:** the preserved real GameService once-used Spend time spec, normalized through the actual adapter, now renders disabled with its precise reason. Current 11 DOM checks and this extra compatibility assertion pass. Evidence `stage1-disabled-regression.{mjs,log}`. The old historical harness used obsolete fixtures; it was not mistaken for a current failure.
- **Real Person action PASS:** roster opens the clicked parent's projection, Spend time submits through the shell, a new authoritative revision is adopted, that tile becomes disabled with the used-year reason, other eligible tiles remain enabled, no console errors. No horizontal overflow at 390px in the probe.
- **Promise amendment PASS by source/full-suite check:** `sendAction`/`resolveDecision` return the operation chain, busy rejects typed, successful resolution returns normalized state, and fire-and-forget internal callers catch. This resolves the earlier void-return integration concern; targeted delayed-network and uncertain-retry UI coverage should remain part of the final parity pass.
- **D baseline PASS as baseline:** existing feature inventory and evidence are present. Full redesign visual/parity certification remains Stage 4, not this baseline.

## Recommended next allocation

B fixes S1/S2 and normalizes Person actions at its boundary; C fixes string-limit display and aligns status with the frozen contract. B can then proceed with the already-planned Home/Choice stage under Claude's go. A has no newly identified projection blocker. D adds actual visibility assertions for lives/editor routes, then verifies the remaining baseline routes against the current shell. Preserve the accepted engine and current pricing/secrecy/adult gates.

No production code edited during this evaluation. Initial attempts to write logs/scripts to `/tmp` hit quota; the authoritative completed suite and probes use workspace temporary directories and workspace evidence.

## Self-check

- [x] Branch `dev` confirmed; actual frozen contracts and integration reviewed.
- [x] Real service/HTTP and Chromium journeys used; rare used-year state forced through an actual command.
- [x] Full suite run: 776 OK, 9 skips; accepted M2.5 browser paths 298/298.
- [x] Passing component checks separated from confirmed actual-visibility failures; no inference of a server eligibility exploit.
- [x] Identity, observer privacy, authoritative actions and retry/promise boundaries checked; no new rules/balance/money changes.
- [x] Only Codex report/evidence/status edited; no ownership overlap or implementation changes.
- [x] Stage 2/3 planned omissions distinguished from Stage 1 route regressions.
- [x] Concrete owner guidance recorded; lead notified and status updated.

## S1 scoped correction — Codex (2026-11-18)

**S1 fixed and verified.** Assigned by Claude after this review. Added only the scoped semantic-hidden override in `web/static/v2/theme.css`:

```css
.v2-scope[hidden],
.v2-scope [hidden] {
  display: none !important;
}
```

The root selector fixes the flex-layout collision; the descendant selector preserves hidden semantics for v2 children. Other pages/scopes are unaffected.

New real Chromium regression: `tests/test_web_v2_hidden.py` opens a real saved game, uses More → All lives, asserts the game has no actual rendered visibility and computed display is `none`, asserts the lives view is visible, then resumes and verifies game visibility/flex layout return. It checks both directions rather than only reading `.hidden`.

- Before the fix: forced failure with `{hidden:true, visible:true, display:"flex", resume:true}`. Evidence `evidence/s1-before.log`.
- After: browser regression passes under PYTHONHASHSEED 0/1/42 (`s1-hash*.log`).
- Related Person/shell/browser checks: **11 tests OK**, 10.914s (`evidence/s1-focused-final.log`). An earlier combined run caught an in-flight C string-limit test discrepancy; the unchanged focused set was rerun after it cleared. That intermediate failure is preserved, not counted as green.
- Full integrated re-evaluation and full-suite run remain reserved for B/C/D's completed reports. This small-fix check is not the milestone re-review.

Self-check:
- [x] Branch `dev`; shared stylesheet re-read before editing; only assigned CSS rule, new regression and own reporting/status changed.
- [x] Real server/save/Chromium; regression demonstrably fails without the fix and passes with it.
- [x] Actual visibility and return/resume behavior asserted; no mocked CSS or property-only assertion.
- [x] Three hash seeds and final related checks green; diff whitespace check clean.
- [x] No new data shape, gameplay/RNG/money/age-gate change; no claim that other review findings are closed.
- [x] Status/lead updated; standing by for B/C/D before integrated re-evaluation.

## Re-review — integrated Stage 1 fixes (2026-11-18)

**Product verdict: PASS for Stage 1; proceed to Home/Choice Stage 2 under Claude's go.** S1, S2 and S3 are closed on the current tree. This is not final acceptance of the complete v2 redesign. Exact-person Edit selection and the full visual/accessibility pass remain scheduled work. One test-isolation correction for D is documented below; no newly demonstrated product blocker.

### Independently verified fixes

- **S1 PASS:** scoped root/descendant `[hidden]` override keeps the shell genuinely hidden on All lives and restores its flex layout on resume. The preserved Chromium journey now reports `game_hidden:true`, **`game_visible:false`**, `game_display:"none"`, with the lives view visible. The real hide/resume regression remains in discovery.
- **S2 PASS:** More → Character editor selects Activities, explicitly opens the editor, and repeated opening is idempotent. The prior exact failing probe now reports **editor visible**, Activities not hidden. Person Edit also reveals that editor instead of opening an invisible panel; exact-person selection remains Stage 3, not claimed complete.
- **S3 PASS:** shell detail loading normalizes raw action specs using the existing adapter before mounting/updating Person. C renders string limits and composes them with known costs. Real displayed Spend time includes “once per year per person”; submitting it updates the authoritative state and disables that tile with the server reason while other eligible tiles stay usable.
- **No console errors / no horizontal overflow** in the independent 390px Person/navigation probe. Evidence: `evidence/rereview/stage1-browser-review.json` and `targeted.log`.
- **Full suite: 778 tests, OK, 9 skipped, 90.335 seconds.** Evidence: `evidence/rereview/full-suite.log`. The runner changes only browser launch/profile cleanup, not game/service behavior.

### Browser-flow caveat — D minor QA fix, not a product regression

The original combined run retained **314 PASS / 1 FAIL** (`evidence/rereview/browser/report.md`). All M2.5 flows pass; the failure is the final death-screen step of `v2-route-visibility` when run after other lives already exist.

`tools/browser_check.py::_hook_resume_first_life` resumes the first available life but does not update the flow's `CTX.game_id`. The following surrender helper operates on the newly created scenario life in CTX instead. Final navigation supplies `?game=CTX.game_id`, but `game.js::consumeDeepLink` intentionally preserves an existing session selection. The browser therefore remains on the other living life, while the API correctly reports the scenario life dead. D's isolated single-life run masked this mismatch.

**Guidance to D:** select the exact scenario life during resume, or explicitly synchronize the browser session to the surrendered life before loading its death view. Prefer a target-ID-aware resume hook and assert browser/API game identity. Keep the real `checkVisibility()` assertions; do not skip the death step or weaken them. Add the flow after several pre-existing lives to verify isolation.

For diagnosis, the controlled reviewer copy changes only the final navigation setup to set `sessionStorage.game_id` to the same `CTX.game_id` before reloading; all original product assertions remain. Original failure evidence is preserved. **Controlled corrected-context run: 316 PASS / 0 FAIL / 0 PENDING / 0 WARN**, including the death visibility assertion (`evidence/rereview/browser-context-aligned/report.md`). Its cleanup confirms the profile root was removed. This reviewer-only copy does not pretend D's production flow has already been patched.

### Remaining stage work / minor items

- Person Edit still requests the general editor without selecting the target ID; keep the Stage 3 exact-person implementation and identical-name test.
- Contract introduction still contradicts the frozen §Person section. The existing contrast claim is also wrong: `#16a34a` on white is approximately **3.30:1**, not 4.7:1; 12px bold is not large text. Correct the documentation and verify actual foreground contrast in the planned visual pass. No full visual certification granted here.
- C's status still mentions normalization pending although B's boundary implementation now exists. Update stale coordination text; this is no longer a real dependency.

### Workspace and browser cleanup

All review processes used unique temporary roots under `.review-tmp`, passed as `TMPDIR`, and explicit Chromium user-data profiles under those roots. Review-owned Chromium process groups are terminated/reaped and profile roots removed in `finally`; no other agent's processes or profiles were touched. The full-suite cleanup records six browser launches and `temp_root_removed:true`; targeted and browser runs record their own cleanup results. No `/tmp` fallback was used for the authoritative runs.

### Self-check

- [x] Branch `dev`; B/C changes and D current-tree evidence read before verification.
- [x] Real persisted service/HTTP/Chromium paths; no mocks replacing game behavior.
- [x] S1–S3 product failures rechecked through actual visibility and authoritative actions, not properties alone.
- [x] Full discovery green with skip/time counts explicit; browser QA setup failure preserved and explained separately.
- [x] No new gameplay, privacy, financial, age-gate or RNG changes introduced by this review.
- [x] Only own status/review/evidence changed; no production code edits beyond the previously assigned S1 correction.
- [x] Stage 1 acceptance distinguished from Stage 3 Edit and Stage 4 visual work.
- [x] Review-created Chromium profiles/processes cleaned; lead receives the verdict and D's actionable QA correction.
