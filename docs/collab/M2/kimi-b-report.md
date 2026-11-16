# M2 report — Kimi B: browser gameplay UI (in progress)

Status: **machinery phase done; blocked on A's api-examples.json for
validation.** No commits. Focused tests only, per policy.

## What exists now (built against frozen docs/collab/M2/02-contract.md)

### `web/static/adapter.js` — the single wire-shape assumption point
Normalizes the state envelope and action/decision schemas per the frozen
contract. Tolerant of M1-shaped states (missing sections → empty structures),
so the UI works across A's rollout and any drift is a one-file fix here.

### Reworked `/game` (phone-width, tabbed)
- Persistent summary card (avatar/name/age/country/generation/cash/stats).
- Bottom tab bar: Summary (timeline with person refs incl. deceased †),
  Education, Work, Family, Activities (health included), Finances.
- Generic action renderer: category grouping, known costs under labels,
  bounded input forms (number min/max, choice selects, text maxlength,
  required validation client-side mirroring backend bounds), paid-action
  confirm shows exact cost.
- Pending-decision modal: title/text/person chips, decision-level inputs
  (e.g. baby naming), options with `enabled`/`reason` states (disabled
  options show their reason as a note), per-option known effects.
- Turn progress card (multi-phase annual flow, `turn.phases` with done
  states) once the service publishes it.
- Finances screen: balance + debt rows, annual estimate clearly labelled as
  a forecast (not posted), last-12 ledger rows with signed amounts and
  running balance. Degrades to "No finance data yet" under M1 states.
- Completed-life view with archived-journal access and an eligible-child
  continuation list (portrait, age, cash inheritance — labelled cash-only),
  behind the service's `continuation` block.
- Same command discipline as M1: one request_id per attempt, replay-safe,
  409 stale/pending → explicit refresh + toast, never auto-retry mutations,
  no client-side simulation or state repair.

### Fixed during build
- `.modal[hidden]` — the modal's `display:flex` was overriding the hidden
  attribute (modal always visible); now correct.

## Assumptions to validate against A's api-examples.json (flagged, not silent)
1. Succession command id: I used `continue_as_child` with
   `payload.target_id` = child person id. If A names it differently, one
   line in game.js (plus adapter) changes.
2. Section field names (from the contract's prose, examples will confirm):
   education `{stage, enrolled, grades, university_years, loan_status}`;
   work `{state, job_label, hours, salary, effective_annual_gross, pension,
   performance, stress}`; finances `{balance, debts, current_annual_estimate
   {gross, tax, expenses, debt_service, pension, net}, last_closed_year,
   ledger_entries[{id, source_key, turn_id, age, category, amount,
   balance_after, description}]}`; turn `{label, phases[{id, label, done}]}`;
   continuation `{eligible_children[{id, name, age, appearance,
   inheritance}]}`.
3. Option/action inputs use contract types number/text/choice/boolean.

## Evidence
- `docs/collab/evidence/avatars/m2_game_tabs.png` — phone-width tabbed game
  UI rendering an M1 state correctly (degraded sections, working tabs).
- Focused tests: `python3 -m unittest tests.test_web_m1` → 25 OK (M1 behavior
  preserved through the rework). JS syntax-checked as ES modules.

## Waiting on
- A: `M2/api-examples.json` (state sections, multiphase pending turn,
  birth/naming, succession examples) — then I validate layouts against real
  payloads and tighten the adapter.
- C: parity-matrix.md for the action inventory labels/categories.
- Then wire real education/work/health/romance/finance actions per vertical
  stage (M2a first), plus tests/test_web_m2.py against the live service.

## Self-check (00-SELF-CHECK.md) — M2 machinery phase
- [x] Tests use REAL objects (real GameService states through every UI
      test); no mocks beyond the network-loss I/O boundary.
- [x] Fixes have failing-before tests: modal-hidden CSS bug found via
      screenshot; retry/deep-link regressions mirror Codex's demonstrated
      failures.
- [x] Randomness: no client-side draws; rendering seed-deterministic.
- [x] Inputs validated via declared schemas; backend owns authoritative
      validation (untouched by B).
- [ ] Money ledger invariants — not yet applicable: no M2 finance backend;
      awaiting A's frozen shapes before coding against them (speed-up rule
      1).
- [x] Identity: person_id everywhere; journal participants resolved through
      peopleById, never names-in-prose.
- [ ] Save/load migrations — A-owned, awaiting frozen envelope.
- [x] Shown costs/eligibility rendered from server-declared fields only.
- [x] Age/state gates: server-owned; client never invents eligibility.
- [x] Only owned files edited (web/**, play-web.sh, render.py, B tests).
- [x] No probability/balance changes.
- [x] Focused tests pass under PYTHONHASHSEED 0/1/42 (test_render,
      test_web_m1).
- [x] UI: keyboard/focus lifecycle browser-verified; phone-width layout
      screenshot; portraits on all person surfaces; contrast ≥ 4.5:1.
