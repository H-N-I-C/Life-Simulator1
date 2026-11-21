# Codex final feel-pass review — 2026-11-20

**Accepted for the requested feel-pass scope after implementation, browser review, corrections and a second review.** This supersedes the changes-requested verdict in `codex-feel-evaluation.md`. No commit or deployment performed.

Reviewed `8604e37138e61a645e6c980e15e9fdbac9c49434` plus the integrated working-tree changes. The exact production file hashes are in [source-manifest.json](../evidence/redesign-v2/feel/codex-final-review/source-manifest.json).

## Changes accepted

- **F1–F3/F7:** full-screen live game, player-facing turn text, one central Age button, pinned numeric stats. Corrected both the bar-height mismatch and the Age circle overlapping stat tracks/reasons. Short `Happy` label and a separately reserved age chip fit at 320px.
- **F4/F12:** chronological per-year feed uses A's public journal projection; the viewport owns scrolling, the bottom follows new entries, and scrolling up preserves reading position. Fixed initial Home visibility before measuring, history entries without saved positions, per-life initialization, and capturing reading intent before the pending-choice notice changes feed height. Removed temporary instrumentation and repeated frame-based scroll forcing.
- **F5/F6:** prices survive server → adapter → modal, including authored label/amount costs and derived money costs; unaffordable reasons remain visible and forecasts stay hidden. Main portrait excludes the player while participant links remain available. Outcome money/stat/per-person relationship rows use actual server deltas. Private outcomes are filtered at capture and serialization. Fixed missing Person stylesheet, hidden/collapsed modal CSS, portrait badge positioning, Person title overlap and long-text wrapping.
- **F8:** corrected the white Age/header text contrast by darkening their gradients (core gradient stops exceed 4.5:1); persistent stat fills now produce real width transitions; the age chip ticks and respects reduced motion. Captured `transitionrun` events confirm width animation; reduced-motion browser check reports `0s` transition and zero stat animations.
- **F9:** default generated accessories are age/base appropriate, cosmetic RNG remains separate, and deliberate existing edits are preserved. The fresh seed-7 mother no longer has a beard. A headscarf on a woman in an older-life fixture is not itself a failure or a reason to rewrite an existing appearance.
- **F10/F11/F14:** duplicate taps cannot resubmit, outcomes gate Age/background interaction, chained decisions progress without another year, collapse preserves pending choices, focus stays inside the active modal, and safe retries recover the matching committed outcome without double application. Death/continuation and retry/Back regressions are included in the passing affected suites.
- **F13:** long names, large balances, long unbroken narratives and many options tested at 320px. Opened editor/tree/dating panels now scroll into view; real saved panel positions restore on Back. Fixed an additional editor loading race: person controls are withheld until policy catalogs arrive, and stale fetches cannot overwrite the current editor context.

## Delegation and review cycle

A implemented default-avatar/cost changes, then corrected authored cost preservation, empty-list consumers and outcome privacy after review. B implemented the feed/stat changes; Codex corrected final reload/follow behavior. C independently exercised deterministic modal, focus, chain and retry journeys. D repaired misleading route screenshots and added stress evidence. Codex implemented the smaller integration/UI fixes and independently reviewed the finished browser build.

Coordination correction: too many follow-ups were queued during the work. D's redundant queued prompts were discarded, its extra task stopped, and D is idle. Completed QA artifacts were retained. No further agent prompts were added after the user's correction.

## Verification

- Complete stable-source suite: **832 passed, 9 skipped, 117 subtests passed; one editor loading-race failure found.** That failure was fixed locally.
- After the editor fix, the affected browser suites (`test_web_m25_browser`, `test_web_v2_shell_browser`, `test_web_v2_event_lifecycle`, `test_web_v2_back`, `test_web_v2_home_lifecycle`): **32 passed**.
- The editor suite was then strengthened to hold the real policy fetch at the I/O boundary, prove no person controls are exposed early, release the request and complete the real preview/apply journey: **2 passed**.
- Event DOM checks: **19 passed**. Shell checks include persistent bars, age tick and reduced motion. Python/JS syntax and `git diff --check` pass.
- Earlier test attempts were contaminated by temporary `RTDBG` logging while a worker still edited the shared source; they are recorded in scratch logs and are not counted as passing final checks.

The full suite was not rerun a third time after the localized editor fix: the affected editor/browser suites above verify that final change. Skips remain skips, not acceptance evidence for their skipped scenarios.

## Independent browser evidence

All final evidence and reproducible harnesses are in **`docs/collab/evidence/redesign-v2/feel/codex-final-review/`**:

- `home-{390,320}.png`, `journal-aged-*`, `journal-reloaded-*`, `event-*`, `outcome-*`, `people-*`, `person-*`, and fresh-life `route-*` captures at **390×844 and 320×844**. `results.json` records geometry, errors, chronological headers and transition events.
- `paid-choice-*`, `paid-outcome-*`, `unaffordable-choice-320.png`: controlled stored-decision fixtures resolved through the real service/ledger. Price $120 is visible; actual outcome is **−$120, +2 Smarts, −4 named relationship**, at both widths. Reload does not replay the outcome.
- `long-short-event-*` and `huge-money-header-320.png`: **320×420**, 10 options and long unbroken text, a 372px scrollable card with 2414px of content, and no horizontal page overflow. `edge-results.json` records the checks.
- `naming-input-320x420.png` and `naming-outcome-320x420.png`: a valid stored naming fixture with a real Child, trusted CDP key events entering `River`, Enter without navigation, and the actual resolver persisting **River Naming**. `input-motion-results.json` confirms the name, reduced motion, no overflow and zero browser errors.

Final main/edge/input runs report no console errors, uncaught exceptions or failed network requests. Rapid double Age advances exactly one year. Reloaded journal scroll positions equal the bottom (400px at 390, 404px at 320 in the sampled save); an older reading position stays at 0 after the next year.

Run the preserved harnesses from the repository root with `TMPDIR=$PWD/.review-tmp/codex-feel-tmp .venv/bin/python docs/collab/evidence/redesign-v2/feel/codex-final-review/run_review.py` (or `run_cost_edges.py`, `run_input_motion.py`). These use isolated throwaway saves, not the user's saves.

## Limits

This verifies the requested UI/feel changes against F1–F14 and the Home/Choice/Person design sources. It is not a new certification of every M2.5 gameplay branch. The naming-input test uses a deterministic saved pending fixture; it does not prove D's natural `pregnant_25` scenario journey. Headless Chromium verifies trusted input and a keyboard-sized viewport, not an actual phone IME. The remaining nine full-suite skips and that hardware limitation are explicitly not counted as passes.

## 00-SELF-CHECK.md

Logic:
- [ ] Real objects/mocks only at I/O: browser/ledger/naming journeys use real objects; A's isolated privacy serialization tests additionally stub the resolver's event result. This is not a claim to test the domain resolver in those privacy tests.
- [ ] Every individual fix tested with an explicit reverted build: failures and before/after browser evidence cover the reported regressions; not every cosmetic correction was independently reverted and retested.
- [x] Seeded/cosmetic RNG separation preserved; no new gameplay random draws.
- [x] Existing typed input validation preserved; naming input reaches the real validated resolver.
- [x] Money/capped deltas verified through the ledger and safe-retry browser tests.
- [x] Actions/relationship rows retain person IDs as identity; names are display labels only.
- [x] Save/reload/retry and existing-save defaults exercised by the relevant suites.
- [x] Paid, unaffordable and free choices verified against actual charges/eligibility.
- [x] Existing server pending/eligibility gates preserved; the extra outcome-display gate is client UI state.
- [ ] New all-template $0 validator sweep: no decision rules were added; used existing regression coverage plus the forced $0 browser fixture, rather than a new exhaustive template audit.

Hygiene:
- [x] User-authorized corrections and agreed agent scopes; shared functions reread before edits, no other work discarded.
- [x] No gameplay balance/probability changes; default cosmetic accessory probabilities changed only for the agreed F9 fix.
- [ ] Final flaky-prone browser checks under three explicit PYTHONHASHSEED values: final affected suites ran on the normal environment; this pass does not claim a fresh three-hash sweep.
- [ ] Entire app AA/physical-phone certification: keyboard, modal focus, narrow/short layouts and core contrast checked; no exhaustive all-screen contrast audit or real phone IME test.
- [x] Rare chain, lost response, private outcome and naming cases forced with stored real fixtures or explicit I/O-boundary faults; earlier fixes rechecked after integration.
