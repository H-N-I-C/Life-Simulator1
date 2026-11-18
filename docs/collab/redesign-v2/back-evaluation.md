# Back-button evaluation and small corrections — Codex, 2026-11-18

**Final verdict: PASS after corrections. Full suite: 812 tests OK, 9 skipped (238.803s); browser parity: 316/316.**

**Scope:** user requested evaluation of B's visible/native Back work and direct correction of small issues. Branch `dev`; no engine, ledger or save-format changes. B's baseline visible Back/menu priority and ordinary tab navigation are implemented, but extra real-browser cases exposed navigation-state bugs. Corrected in `web/static/game.js` and `web/static/v2/shell.js`; new regressions in `tests/test_web_v2_back.py`.

## Findings corrected

1. **Forward then Back lost the correct previous destination.** The `popstate` handler decremented a process-local depth on both Back and Forward. Entries now carry a persistent index, and incoming history restores that index. Browser Forward and in-app Back use the same position; the in-app root guard remains.
2. **Reload lost the selected view and usable history depth.** Depth is initialized from the current entry, and initial game rendering reapplies the saved destination. Reloading Job → Money returns to Money; Back returns to Job. Person reload and subsequent native/in-app navigation also restore the same person.
3. **All lives could not return directly to the original Home.** The base history entry omitted the game ID. It now includes the ID/index, and asynchronous restoration waits for the requested life to load before applying its destination/scroll. Reloading All lives retains its visible Back. The entry being left is also repaired when it uses B's older history payload without a game ID/index; the compatibility case failed before that repair and passes at three hash seeds (`existing-history-before.log`, `compat-hash*.log`). Resuming another life explicitly adds that life's Home route; returning through history restores the original identity, not just a similarly named card.
4. **Repeated editor opening added artificial Back steps.** Identical destination requests no longer push duplicate entries. Opening the same visible editor/panel preserves it instead of toggling or unnecessarily rebuilding it. Editor history entries retain an explicitly selected person ID for restoration.
5. **Family tree / Dating & weddings opened under hidden Activities from Home.** All feature openers reveal their owning screen, close other panels, and preserve an already-open requested panel. Restoring any panel dispatches directly without pushing another history entry. Legacy toggle buttons and More routes feed the same navigation intent.
6. **Person's Back diverged from native history.** The shell checked `opts.onOverlayBack`, but game.js supplied `hooks.onOverlayBack`; the fallback pushed another People destination instead of consuming Person. It now uses the actual history hook (with standalone-host fallback). Error-state Back uses the same path. Person Edit closes its local overlay without adding a false intermediate navigation, then opens the exact editor route.

## Evidence and verification

- Initial corrected-syntax browser probe: **5 failures among 6 cases**, preserved in `evidence/back/before.log`. The first probe's selector syntax mistake is separately preserved as `probe-syntax.log`, not attributed to the product.
- After the first corrections, those six browser cases passed. A further Person/native-history regression exposed its hook mismatch (`person-before.log`); corrected and rechecked.
- **Nine real Chromium/service regressions pass at PYTHONHASHSEED 0/1/42** (`hash*.log`): Forward + Back, destination reload, plain Home → All lives → reload → Back, non-Home → All lives → Back, repeated editor open, visible Family tree/Dating routes from Home, Person reload/history parity, switching lives then returning to the correct ID, and browser Back during an actual pending decision.
- Pending preservation asserts the exact stored decision ID and revision remain unchanged by navigation, the decision sheet remains visible, and Age remains disabled. Navigation is not interpreted as consent/resolve/cancel.
- **Current M2.5/visibility parity: 316 PASS / 0 FAIL / 0 PENDING / 0 WARN** (`evidence/back/browser/report.md`).
- Syntax and whitespace checks pass. A first full run passed **811 tests / 9 skipped** before the final existing-tab repair (`full-suite-pre-compat.log`). **Final frozen-code discovery: 812 tests OK, 9 skipped, 238.803 seconds** (`full-suite.log`). Snapshot hashes are unchanged; the run reaped 28 review-owned Chromium launches and removed its profile root.

During review B added and adjusted a separate 320px browser case. One moving-tree full run recorded 812 tests with that case's missing-button error (`full-suite-moving-phone-test.log`); its traceback did not match the file then on disk. B's current version uses its own life instead of the class-shared mutable fixture and passes independently (`phone-current.log`). I did not edit that phone test. Claude then froze shared edits, and `final-snapshot.json` records the relevant source/test hashes for the final full run; snapshot checks showed no changes.

Browser tests use the actual temporary web server and persisted GameService; no mocked history, fabricated decisions or simulated server eligibility. Unique short workspace TMPDIR roots and explicit Chromium profiles avoid the earlier Unix socket-path/tmpfs issues; cleanup records are retained in each run. Other agents' browser processes/profiles and user saves are untouched.

## Design guidance retained

History position belongs to the entry, not a counter that assumes every pop is Back. Each destination must carry sufficient identity to restore itself after a reload or a different life selection. User navigation pushes an entry; history restoration must not push one. Open means idempotent open, while close/Back consumes the prior destination. These changes keep that distinction in the existing shell rather than introducing a new router or changing gameplay commands.

## Self-check

- [x] Shared files re-read; cross-owner fixes explicitly authorised by the user; `dev` branch verified.
- [x] Actual browser/server cases demonstrate before/after behavior; probe mistakes distinguished from product findings.
- [x] Forward/reload and exact life/person IDs covered, not only a single Back click.
- [x] Pending-decision identity/revision/gating preserved; no action or money movement introduced by navigation.
- [x] No gameplay, probability, schema or save migration change.
- [x] Focused browser regressions pass under three hash seeds; parity checks pass.
- [x] Final frozen-code regression suite green: 812 tests OK / 9 skipped; intermediate failures and their disposition retained.
- [x] Review-owned Chromium/profile roots cleaned and evidence retained; lead/status updated.
