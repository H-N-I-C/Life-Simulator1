# Kimi D report — M2 QA (stage M2a prep)

Date: 2026-11-16. Own files only: `tools/**`, `tests/browser/**`,
`docs/collab/evidence/**`. No source or unit-test edits.

## Read first

docs/collab/M2/01-spec.md (incl. "Traps to resolve before coding"),
02-contract.md, parity-matrix.md, phase-map.md, rule-examples.json,
C's `src/menus/rule_actions.py` + kimi-c-report.md, B's kimi-b-report.md.

## What landed now

1. **Scenario manifest** — `docs/collab/evidence/M2/scenario-manifest.md`.
   20+ scenarios keyed to parity-matrix rows across M2a/b/c: each names a
   valid domain-state setup, the browser actions, state/ledger assertions
   with independently computed expectations, evidence paths and an honest
   status (`covered` only when actually executed). Rare branches (kidnapping
   1/250, lightning 1/5000, cancer 1/2000, age-2 vaccination) are listed as
   uncovered-needing-factory, not claimed from random aging.
2. **Independent accounting calculators** — `tools/expected_finance.py`.
   Re-implements bracket tax, hours-scaled gross/pension split, the integer
   quote identity (gross−tax−expenses==net, residual→expenses), pension
   (top-5 mean × min(years,35) × 2%) and ledger running-balance/cash-delta
   checks — without calling C's helpers, so assertions can't rubber-stamp
   the implementation. Self-check passes (`python3 tools/expected_finance.py`).
3. **Runner machinery for M2 flows** — `tools/browser_check.py` gained an
   `{"api": {...}}` step kind (raw API calls with status checks, response
   stored in flow context for JS/Python cross-assertions); flows stay
   data-driven in `tests/browser/flows.py`.
4. **Visual regression base** — `tools/avatar_sheet.py` now also emits a
   `lifecycle` sheet (ages 0→100: newborn, baby, child, teen, adult,
   greying, elder with recession/wrinkles). 30 sheets regenerated; lifecycle
   and matrix sheets eyeballed clean.

## Current verification

- `python3 tools/expected_finance.py` → self-check OK.
- `python3 tools/avatar_sheet.py` → 30 sheets written.
- `python3 tools/browser_check.py` → 50/50 passed, exit 0 (M1 flows +
  page checks still green after the M2 edits).

## Blockers / requests (routed to Claude → A)

Executable M2a browser scenarios (M2a-S1..S6) need:
1. **A's `api-examples.json`** (state sections `education`/`work`/`finances`,
   action schemas, ledger rows) — B is blocked on the same artifact.
2. **A test-only scenario factory** in the service (e.g. a documented
   test-only command or fixture loader) to start lives in valid states
   (18-yo graduate, 65-yo worker with salary history, illness/cash presets)
   without debug UI in normal play or fabricated saves. Manifest setups are
   written assuming one exists; I'll convert rows to `covered` as it lands.

## Observations (not my files to fix)

- Full unittest suite currently has errors in C's `test_rules_m2a` /
  `test_rules_income` and one flaky `test_rules_menus` toy assertion —
  consistent with C's M2a extraction still in flight. Not touched per
  ownership.
- favicon 404 finding from M1 still open (B).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Tests use REAL objects — n/a for this slice (QA tooling only); browser
  flows drive the real server/UI, never mocks of the engine.
- [x] Failing-without-fix check — n/a (new tooling, no bug fix); the avatar
  layout fix was verified by regenerated output (906→780px, band gone).
- [x] Randomness — avatar_sheet is fully deterministic (no RNG/time input);
      browser flows use condition/event waits, never fixed sleeps as logic.
- [x] Inputs validated — runner treats HTTP >=400 and JS exceptions as
  failures with typed context, not silent skips.
- [x] Money — expected_finance.py independently recomputes ledger
  identities (gross−tax−expenses==net, running balances, cash delta) instead
  of calling the implementation helper.
- [x] Identity — assertions key on person_id (manifest invariants); no
  name/seed-based references in my tooling.
- [x] Save/load — browser runs use a temporary save dir + free port; user
  saves can never be altered by QA.
- [x] Shown costs vs applied — asserted in manifest scenarios (fees posted
  once, limits enforced server-side); execution awaits A's frozen shapes.
- [x] Age/state gates — manifest requires server-side rejection checks, not
  UI-hidden actions.

Hygiene
- [x] Only owned files edited (tools/**, tests/browser/**,
  docs/collab/evidence/**; plus my own row in 00-STATUS.md per new rule).
- [x] No probability/balance changes (no source edits at all).
- [x] Focused checks pass: browser_check 50/50 (3 consecutive runs),
  avatar_sheet 30/30, expected_finance self-check OK; no flaky tests owned
  by me. (3-hashseed rule n/a — no new unit tests in this slice.)
- [x] UI rules are B's items; my page-level checks enforce keyboard/overflow/
      console-error evidence per viewport.

Standing rule acknowledged: no browser flows will be coded against shared
data shapes until A freezes them in api-examples.json; manifest assertion
expressions will be re-validated at that point.

## Update 2026-11-16 (api-examples.json frozen)

- Built `tools/m2_assertions.py` against the frozen shapes: state-section
  validation (education/work/finances/turn shapes + annual_estimate
  identity), ledger-row shape + source_key uniqueness + running balances,
  person-reference resolution, and succession transfer-leg netting.
  Self-tested using the frozen api-examples.json as fixtures — including
  its `40000 - 5800 - 12000 == 22200` identity and the balanced
  inheritance legs (`python3 tools/m2_assertions.py` → OK).
- Runner gained a `{"python": "assert_state"}` hook so M2 flows can assert
  the frozen shapes on any captured state; M2a flow definitions will be
  added (and manifest rows flipped to `covered`) once A lands the dev
  scenario starts — setup is the only missing piece, shapes are not.
- Checks: browser_check 49/49 exit 0; m2_assertions self-test OK;
  avatar_sheet 30/30.

Self-check delta (00-SELF-CHECK.md): all items remain as ticked above;
the new assertion layer is pure-over-frozen-shapes (no implementation
calls), and its negative paths (broken estimate, bad source_key,
unresolved refs, unbalanced transfer) are asserted to fail in the
self-test — the failing-before check for the checkers themselves.
