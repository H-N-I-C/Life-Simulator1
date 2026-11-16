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

## Update 2026-11-16 (M2a flows authored against frozen shapes)

- `tests/browser/flows.py` gains four M2a flows keyed to the manifest:
  `m2a-s1-school-limits` (study once/year, typed 409 on repeat, no
  mutation on rejection), `m2a-s2-university-loan` (loan recorded at
  acceptance, balance sane after year), `m2a-s3-job-hours-income-ledger`
  (stored job offer not rerolled, hours 55 → independent gross/tax check
  + quote identity + ledger audit), `m2a-s4-retirement-pension`
  (can_retire gates, pension gross == projected pension, hours-independent).
- Runner machinery: parametrized python hooks (`{"python": {"hook": ...,
  ...}}`), `check_ctx`/`store_ctx` steps (assert/dig the frozen-shaped
  state), `cmd`/`refresh_state` hooks (replay-safe commands at current
  revision), `settle_turn` (drives a committed year, resolves stored
  decisions without rerolls, asserts age +1 exactly once),
  `assert_work_income`/`assert_pension_income` (independent expectations
  via tools/expected_finance.py), and `PendingSkip` so scenario-dependent
  flows record PENDING instead of failing or being faked.
- Phase-map v4 landed (mortality before work_income, `work_income` name):
  m2_assertions PHASE_ORDER updated; self-test still OK.
- Honest status: all four flows currently record PENDING — they execute
  the moment A's dev scenario factory lands; no bare-age/injected-balance
  shortcuts used.
- Checks: browser_check 53/53 exit 0 (49 executed + 4 PENDING rows);
  m2_assertions self-test OK; avatar_sheet 30/30.

## Update 2026-11-16 (consolidated-review item 5 + B check consolidation)

- Strict booleans: wait_js/check_js/check_ctx now REQUIRE real booleans —
  numeric 0, "" and {} (e.g. a DOM node serialized over CDP) fail loudly
  instead of passing on identity. Non-boolean waits raise TypeError with
  the offending value; all flow expressions updated (DOM lookups wrapped
  in `!!`).
- M1 flow extended through a REAL decision: `reach_participant_decision`
  ages via API until a pending decision with a NON-EMPTY person list
  (participant-less decisions are resolved and aging continues, so the
  fixture is meaningful by construction), then `decision_modal_walk`
  asserts in the UI: focus inside modal, Tab trap, participant portraits
  (polls for async render), Escape→pill, pill reopen with focus, resolve
  closes modal, post-reload timeline portraits. Verified green (3 runs).
- New `m1-resilience` flow folds in B's browser_smoke regressions:
  simulated committed-but-lost creation (retry → exactly one life,
  operation cleared after success), deep-link vs Resume (Resume wins,
  URL cleaned, refresh keeps selection), invalid deep link → life list,
  resume-list portraits. B's smoke file itself is untouched (B-owned).
- Checks: browser_check 75/75, 3 consecutive runs; avatar_sheet 30/30;
  m2_assertions + expected_finance OK.

### Findings routed to Claude

1. **Soft-lock: unaffordable decision with $0.** A naturally aged
   zero-balance life reached a $500 choice whose options were all
   disabled; age_up is 409-blocked by the pending decision and resolve is
   409-blocked by funds — no legal command remains. Reproduce: create a
   life via API, age with resolve-first-enabled until the paid choice.
   Owner: likely A (offer only affordable options / a decline path) or C
   (legacy parity — check what the terminal does when broke).
2. **Live M1 state deviates from frozen M2 shapes:** `education.grades`
   is not an int and `work.job_label` is absent on current projections
   (seen by m2_assertions on a live state). Flag for A — either the
   frozen example or the projection needs to converge before M2a
   assertions run for real. My M1 flow asserts person-refs only; frozen
   shape assertions live in the M2a flows.

Self-check delta: item "failing-without-fix" — the strict-boolean change
was verified by the previously-passing `{}`-truthy expression now failing
TypeError in a direct probe; new decision/resilience assertions fail when
run against the pre-extension flow file (steps absent). Flakiness: the two
racy checks (resume-list and modal-portrait rendering) were caught failing
intermittently and fixed with condition waits; 3 consecutive clean runs.

## Update 2026-11-16 (scenario-start shape frozen; flows wired)

- `scenario_start` hook implements the frozen profile-key shape from
  api-examples.json (4 scenario ids; married-with-child deferred to M2b).
  Flows updated: m2a-s1 `high_school_first_day`, m2a-s2/m2a-s3
  `graduating_18`, m2a-s4 `near_retirement_64`.
- Auto-detection: after the scenario POST the hook refreshes state and
  verifies the frozen result marker (player age 14/18/18/64). While the
  service silently ignores `scenario` (current: age 0), the flow records
  PENDING; the day A's implementation lands, the same run goes live with
  no D-side changes. No bare-age/injected-balance shortcuts anywhere.
- Runner fixes along the way: `refresh_state` prefers an explicit new
  `game_id` over a cached state (was re-reading the previous flow's game),
  and the flow context is cleared between flows.
- Checks: browser_check 83/83, 2 consecutive runs (75 executed + 8
  scenario PENDING rows); avatar_sheet 30/30; m2_assertions +
  expected_finance OK.

Self-check delta: frozen-shape discipline held — coded only against the
published `scenario_start_example`; unknown-scenario 400 path is treated
as PENDING too. Failing-before: refresh_state precedence bug was caught by
the scenario age check itself (stale game read) and now covered by the
PENDING message showing the real age.

## Update 2026-11-16 (M2a S1–S4 executed against live scenario factory)

B fixed the adapter (`scenario` now reaches the service); A's scenario
factory (src/game/scenarios.py) works through real transitions. Executed
results via `python3 tools/browser_check.py`:

- **S1 school limits — COVERED, green.** Age 14 enrolled; study changed
  grades 90→96; immediate repeat = typed 409 with revision unchanged
  (no mutation); settle advances age exactly once.
- **S3 job→hours→income ledger — COVERED, green.** Stored offer resolved
  without reroll; hours 55 → gross leg == round(salary×55/40) (42868),
  tax leg == independent bracket calc (signed), gross+tax+expenses==net,
  Σlegs == Δbalance, ledger rows match frozen shape.
- **S4 retirement — COVERED with WARN mask.** keep_working → retire
  action: model correct (salary becomes pension 21824 = 31177×0.7,
  has_job False, years 0) but the work projection shows retired:false /
  pension:0 → **finding for A** (repro: scenario near_retirement_64,
  age_up, resolve offer keep_working, action retire; WARN rows reference
  it and will be restored to strict once fixed).
- **S2 university — PENDING (engine).** `graduating_18` ages 24 commands
  with no `university_application`/`university_funding` pending; the
  frozen `apply_university` action is "unknown action" in the live
  engine. Flow flips live when A/C wire the P6 path.
- Runner fixes this round: `window.state` injected alongside CTX (flow
  expressions reference bare `state`); settle_turn termination is
  age-based (finished year and fresh year share cursor year_start);
  signed tax/expenses leg expectations; known-issue filtering for B's
  `/api/scenarios` 404-outside-dev poller (kept visible as WARN rows).
- Checks: browser_check **202/202, 2 consecutive runs**; avatar_sheet
  30/30; m2_assertions + expected_finance OK.

### New findings routed to Claude

1. **A — work projection for retired players**: `state.work` shows
   `retired:false, pension:0, job_label:null` after a correct retirement
   (model: has_job False, salary = pension). file: service `_state`.
2. **A — education.grades is None** (not int) on `graduating_18`
   (`graduated_high_school:true, stage:none`); frozen example says int.
3. **B — dev panel polls `/api/scenarios` outside `--dev`** → 404s on
   every page load and network never idles. Gate the fetch on the dev
   flag (or serve `[]` in normal mode).
4. Earlier: unaffordable-decision soft-lock (A, decisions.py
   `validate_templates` KeyError '_effects' crash seen while
   scenario-driving with some seeds).

Self-check delta: WARN masks are explicit, dated and tied to reported
findings, with strict assertions preserved in-code for post-fix restore;
no probability/balance or non-owned files touched (B's adapter fix was
verified, not edited).

## Update 2026-11-16 (M2a re-pointed at A's per-scenario ids; S6 added)

- Flows now use the recommended scenario ids + seeds: S1 `enrolled_child_10`,
  S2 `university_pending_17` (seed 4242; open decision verified at start,
  application resolves "Yes"), S3 `unemployed_18`, S4 `retirement_eligible_65`
  (can_retire true immediately; no boundary year needed), S6 NEW
  `low_performance_30` (documented dev-only perf adjustment — no forcing
  hook needed, thank you A).
- S6 learning: legacy firing for performance <15 is PROBABILISTIC
  (player.py:679 `randint(1, performance+1)==1`, ~1/11 at perf 10), so the
  flow ages year-by-year until the firing lands (max 30 seeded years;
  PendingSkip if it never lands — performance can drift up, which is
  parity-correct). Result: **fired cleanly; ledger integrity holds;
  boundary-year pay posted at most once.**
- Runner: scenario_start takes `seed`; ENV_FLAKES filter for transient
  Chromium network errors (ERR_NETWORK_CHANGED etc. — recorded, not
  failures); settle_until_fired hook.
- S2 remains PENDING on the engine: `university_application` resolves but
  no `university_funding` pending follows within 24 commands (P6 funding
  path not wired). All other M2a rows COVERED.
- Checks: browser_check **222/222, 3 consecutive runs**; avatar_sheet
  30/30; m2_assertions + expected_finance OK.

Self-check delta: the probabilistic-firing correction came from reading
the legacy source (player.py:679) after my deterministic assumption
failed in the dry run — the S6 assertion now matches the actual rule and
records observed behaviour as evidence rather than assuming it.

## Update 2026-11-16 (runtime scenario-id discovery)

- `scenario_start` no longer trusts hard-coded ids as the source of truth:
  on an "unknown scenario" 400 it consults a runtime catalog (list-scenarios
  endpoint when live; parsed defensively as list[str] / list[{id}] /
  {scenarios:[...]}) and reports PENDING with the available ids if the id
  was retired/renamed in A's consolidation. Age verification only applies
  to known ids; runtime-discovered ids still get the full state/ledger/
  person-ref assertions downstream. Static SCENARIO_AGES map remains a
  fallback until the endpoint lands (currently 404 — expected).
- Checks: browser_check 222/222; avatar_sheet 30/30; m2_assertions OK.

## Update 2026-11-16 (A's fixes verified; masks removed)

- Unmasked `education.grades` / `work.job_label` after A's fix: grades now
  always int; job_label verified str on employed lives across S1/S3/S4/S6.
- Nuance found while unmasking: `job_label` is still null on JOBLESS states
  (child 10, retired, fired). The frozen example is silent about
  jobless states, so the checker allows null only while unemployed and
  records a visible WARN question for A (employed + null = hard failure).
- Soft-lock fix acknowledged (A: structural free option + affordability
  gating + re-enable guarantee, regression-tested). My settle/resolution
  hooks already assert an enabled option exists for every pending
  decision they touch, so any regression there fails my runs loudly.
- Scenario ids used by flows (enrolled_child_10, university_pending_17,
  unemployed_18, retirement_eligible_65, low_performance_30) are all in
  A's canonical 10 — no rewiring needed.
- Still WARN-masked, awaiting A: retired-player projection
  (work.retired=false / pension=0 after a correct model retirement).
- Checks: browser_check **217/217, 2 consecutive runs** (jobless WARN
  rows visible); avatar_sheet 30/30; m2_assertions + expected_finance OK.

## Update 2026-11-16 (Claude's test-quality points)

1. **S4 determinism confirmed:** the flow uses `retirement_eligible_65` +
   the voluntary `retire` action (can_retire asserted first); no run can
   pass without actually retiring and asserting the pension. The optional
   offer-acceptance path was already removed in the previous round.
2. **WARN masks are now self-removing:** they apply ONLY to the exact
   known-broken projection signature (employed:false + retired:false +
   pension:0 + salary>0 — model correctly retired, projection wrong; A,
   reported 2026-11-16). Any other mismatch fails hard, so the mask
   disappears automatically when A's fix lands and guards against new
   regressions in the meantime.
- Checks: browser_check 217/217 then 219/219 (S6's probabilistic firing
  year count varies by engine RNG state — both runs fully green);
  avatar_sheet 30/30; m2_assertions + expected_finance OK.

## Update 2026-11-16 (S6 hard cap + ENV_FLAKES trimmed, per Claude)

- S6 `settle_until_fired`: cap is now 15 years and NEVER being fired is a
  HARD failure (no silent skip) — a run must not pass without the firing
  actually happening, same determinism bar as S4. Observed: fires after 4
  seeded years on current engine (consistent across runs).
- ENV_FLAKES trimmed to genuine network-change errors only
  (ERR_NETWORK_CHANGED / ERR_NETWORK_IO_SUSPENDED /
  ERR_INTERNET_DISCONNECTED); ERR_ABORTED removed (navigation-cancel
  class, already handled separately at the CDP event layer).
- Checks: browser_check 217/217, 2 consecutive runs; avatar_sheet 30/30;
  m2_assertions + expected_finance OK.

## Update 2026-11-16 (M0b asset pipeline + v2 migration findings)

### M0b Fluent asset pipeline (tools/fetch_fluent_avatars.py) — DONE
- Pinned commit 1ffb34c752ecf5d402f04cfb4b392c77f57c54bc; 227 assets
  (baby/child/boy/girl × skins, 3 adult bases × 5 hairs × 6 skins, 3
  elders, man/person beard, headscarf, 12 look assets, 5 badge objects),
  all resized to 256px RGBA, alpha kept.
- **Total: 8.9 MB (target <15 MB)** including manifest.json (87.5 KB) and
  the MIT LICENSE at web/static/avatars/LICENSE-fluentui-emoji.txt.
- manifest.json: assets + 587 resolved combos with ordered fallback
  chains (verified: looks resolve to look assets, graduate falls back to
  plain adult per contract, curly+elder→elder, skin fallback→default).
- Fetcher self-check: every look combo starts with its look asset;
  re-runs are idempotent (0 re-fetched).
- avatar_sheet.py: Fluent sheets are now the default mode (matrix, hair,
  looks, extras at 128px); legacy SVG sheets behind --svg, auto-skipped
  under catalog v2 (obsolete).

### v2-migration findings (routed to Claude)
- S4 UNMASKED and green: retired projection fixed (retired:true,
  pension gross == 54218 strict PASS).
- S3: `unemployed_18` scenario regression — player now lands enrolled in
  UNIVERSITY (3y remaining, grades 89); find_job 409 "cannot work while
  enrolled". Violates the frozen scenario spec ("no job, clean actionable
  state"). A to fix.
- S2: funding decision now appears and resolves ("Student Loan") but
  education.loan.has_loan stays false — funding resolution doesn't record
  the loan (A/C).
- B: /api/catalog returns ERR_EMPTY_RESPONSE and /api/render HTTP 500
  mid-migration — creator/gallery/avatar pages all fail; every flow's
  network check is polluted by /api/catalog.
- QA fixtures migrated to schema-v2 appearances (minimal: base +
  skin_tone + hair); validate_export is now schema-agnostic.

## Update 2026-11-16 (dev-mode server + hard scenario-id failures)

- Runner starts the web server with `dev_mode = True` (scenario starts
  are dev-only per web/server.py; matches play-web.sh --dev).
- Unknown/retired scenario id is now a HARD failure (RuntimeError naming
  the runtime catalog), never PendingSkip — renames are caught
  immediately per team rule.
- Runner masks for B's known migration outage (dated, removal-noted):
  /api/catalog + /api/render request failures, uncaught "Failed to
  fetch" exceptions, and the m1 avatar render wait (probes /api/render;
  WARN while 500, strict otherwise).
- Current red rows are all B's live half-landed v2 UI (creator.js
  buildOptions crash, /api/avatar.png 404, render 200-but-not-SVG) plus
  the reported S2 engine defect (loan not recorded). Re-verified after
  B's next landing; no D-side changes pending.
- Fluent asset pipeline remains DONE: 227 assets, 8.9 MB, manifest with
  587 verified fallback combos, LICENSE copied.

## Update 2026-11-16 (manifest rebuilt to the published M0b key space)

- docs/collab/M0b-examples.json landed; tools/fetch_fluent_avatars.py
  `build_combos` now emits exactly the published key shape
  `<base>-<stage>-<skin>[-hair-<h>][-beard][-headscarf][-look-<look>]`
  (stages baby|child|teen_adult|greying|elder; looks use the published
  ids incl. `police`; look assets live under the same key suffix).
- 2,093 combos, every key validated against the published shape by
  regex self-check; greying resolves to white-hair assets (spec: derived
  white hair); elder ignores hair variants with fallback to the elder
  base (spec: curly+elder -> elder); graduate has no asset and falls
  back to the regular look; badges stay as `badge-<name>` (corner
  objects outside the face key space).
- web/static/avatars total: 9.4 MB (8.8 MB assets + manifest + LICENSE).
- Portrait selectors in the runner accept img as well as inline SVG
  (forward-compatible with B's Fluent chips).
- Remaining browser reds are B's half-landed v2 UI (creator.js crash,
  /api/avatar.png 404) — re-verify after B lands.

## Update 2026-11-16 (M2b flows live; engine churn blocker)

- Seven M2b flows authored (API-level; UI steps wait for B's M2b screens):
  conversation, wedding-venue path (via divorce), baby naming with
  60-char long-name rejection (400) + valid name, adoption+rename with
  cost audit, doctor cures ($100/illness, per-illness cure), witch
  doctor fee bounds (50–100), funeral with 30-year bounded death watch.
- New hooks: partner_id, record_balance, assert_last_payment (category
  or source_key match, amount bounds, exactly-once, delta==Σrows),
  assert_illnesses, await_action, await_rel_death (hard cap),
  resolve_funeral_if_pending. `_dig` supports `.length` on lists.
- Results before the churn: S1/S4/S5/S6 covered green; S2 pending-path
  (no dating after divorce); S3 pending-engine (birth naming not firing);
  S7 hard-fails on a real gap — relation mortality never fires (parents
  92/85, 30 seeded years, zero deaths) → finding for A/C.
- m2a-s2 assertion updated per A's financed-year semantics: settle one
  year, then `loan.has_loan && remaining > 0`.
- BLOCKER (A/C mid-edit, repro: any scenario start): HTTP 400 "unknown
  pending decision: 'vaccination_age2'" — a renamed/removed decision
  kind breaks drive_scenario for ALL scenarios. Routed to Claude.
- Also seen: m1 decision-resolve 400 during churn. Re-verify after
  A/C land.
- Checks: before churn 334/342 with only expected reds (B creator
  mid-migration + funeral gap); self-checks (avatar_sheet/m2_assertions/
  expected_finance) OK.

## Update 2026-11-16 (M0b verification + M2b fixes)

- browser_check: 347/354; all M2a/M2b scenario flows green. Fixes this
  round: clear_pending (scenarios hand off with an open contextual
  decision), find_job_until_employed (legacy interview roll can reject;
  bounded retries), pregnancy chain (keep -> birth -> baby_naming with
  60-char name rejected 400 + valid name), children-count baseline moved
  before try_for_baby, ledger source_key leg regex broadened (decision
  effects post `:main` legs).
- Remaining reds: B's creator buildOptions crash (4 rows) + intermittent
  gallery /api/avatar.png 400 in-browser (direct base64url probe works;
  B to check the gallery's app encoding) + m2b-s7 funeral gap (relation
  mortality never fires — A/C finding).
- M0b evidence saved to docs/collab/evidence/m0b/ (index.md + 9
  screenshots @390px). Badge-picker QA marked pending per Claude (A's
  validation rejects badge-bearing creates; routed).
- avatar_sheet (fluent), m2_assertions, expected_finance: OK.

## Update 2026-11-16 (C's resolver fix verified)

- C fixed the event-pending resolver (kind = stable event id by design;
  rule resolver now resolves loader-generated pendings incl. legacy
  'vaccination_age2' compat). Verified through my runner: all M2a/M2b
  scenario flows green again — browser_check **320/327, 2 consecutive
  runs**; remaining reds are exclusively B-owned (creator buildOptions
  crash on every page load; gallery /api/avatar.png intermittent 400
  despite the direct base64url probe returning 200).
- api-examples.json gained m2b_actions_example + delete_game_example
  and succession_example was restructured (continue_as shapes; raw
  transfer legs removed) — m2_assertions self-test fixtures updated to
  the new frozen file; self-check OK.
- Note for M2c: succession is now specified as action `continue_as`
  (succession.eligible + generation_next) — my M2c-S5 flow will target
  that frozen shape when authored.

## Update 2026-11-16 (row 51/61 closed; cursor finding)

- Row 51 verified green: A's relation mortality works — funeral pending
  (kind 'funeral', attend/skip) fires ~year 28 in a seeded married_30
  run; m2b-s7 + m2b-r51 pass with archive/ref-resolution assertions.
  await_rel_death now also matches journal kind 'death' and needs a
  60-iteration cap (flavor decisions consume iterations).
- Row 61 vaccination: fixed-seed sweep (1-6) per C's parity note; green.
- New finding routed to A (WARN-masked with exact signature): after a
  mid-year firing the projection emits `turn.cursor.phase_id = null`
  though the phase map says the cursor always carries a named phase.
  m2_assertions itself stays strict; mask lives in the runner with the
  dated note and self-removes when A fixes the projection.
- Checks: browser_check **590/590, 2 consecutive runs**; avatar sheets,
  m2_assertions, expected_finance OK.

## M2 fix round — finding #6 (D + B): parity acceptance tooling

Implemented in tools/browser_check.py:

1. **Honest four-status accounting** (Codex #6): every check is
   PASS / FAIL / PENDING / WARN. PENDING rows — unperformed required
   paths — are recorded via PendingSkip with `status="PENDING"` and
   NEVER count as passes. WARN rows (known deviations, dated, exact
   signatures) are also not passes. Report shows all four counts plus a
   "Pending rows" section marking each as `deferred (reason)` or
   `REQUIRED - fails the gate`.
2. **Gate semantics:** the run exits 1 if any FAIL exists OR any
   PENDING whose flow is not in DEFERRED_PENDING (explicit
   Claude-approved deferrals only). Currently deferred: m2b-r69-
   kidnapping (1/250 rare branch) and badge-avatar-rendering (A's
   badge validation; now fixed by A — classifier self-removes).
3. **Creator keyboard rebuilt for the Fluent DOM** (was parked):
   skin-tone focus retention + aria-pressed across the async render,
   ArrowRight tab traversal (tab-skin -> tab-hair), badges list
   semantics with the contract cap of 2 (two stick, a third is
   refused), export JSON validated through Appearance.from_dict,
   rapid age-slider sweep keeps a valid preview (img or svg).
4. **Badge-avatar classification:** avatar.png 400s whose decoded
   base64url payload carries badges are recorded as deferred PENDING
   (known A validation gap, per Claude), not masked as passes and not
   counted as product regressions.
5. **Last stable full run before the engine fix-round churn:** PASS=714
   FAIL=0 PENDING=2 (1 deferred) WARN=7, with the gate correctly
   listing required-pending rows (wedding path, baby naming, creator
   load race — race since fixed by waiting for options before the
   focus step).
6. **New B/A finding:** intermittent /api/avatar.png 400 on the creator
   page under the thumbnail burst; every field-value combination
   probes 200 individually (incl. badges now that A fixed validation),
   so the endpoint is not robust under concurrent load — B/A to
   investigate; currently a real FAIL row (not masked).
7. m1-life-flow / m1-resilience repaired for the current DOM
   (img-based chips; #gameView assertions pass again — Codex's two
   browser failures were transient B churn, verified green in the
   714-pass run).

A's in-progress fix-round edits then 500'd all age_up/scenario-start
calls (observed mid-edit); scenario flows will be re-run once A's #1-#5
land. Strict state-shape checks preserved throughout.

### Fix-round re-verification (after A's #1 turn-resume edits landed)

- creator-keyboard: all 11 steps green on the Fluent DOM (fixed my tab
  assumption — the creator opens on Look; the flow switches to Skin
  before the focus test).
- Remaining FAILs are all A-side fix-round fallout, now visible as real
  failures through the new gate (not masked): every married/with_child
  scenario start 500s with "scenario setup failed: progress guard
  exhausted" (drive_scenario's loop stalls against the new
  resume-after-resolve semantics — A must update the factory for fix
  #1); retirement_eligible_65 no longer starts employed/can_retire.
- Intermittent /api/avatar.png 400 under the creator thumbnail burst
  persists (single-request sweep: all 200 incl. badges) — B/A.
- Gate behavior verified: FAILs fail the run; deferred PENDING does not;
  required PENDING lists explicitly.

### Claude's correction applied (no unapproved deferrals)

- DEFERRED_PENDING is now EMPTY (team rule: deferrals only by Claude's
  written approval). Unperformed required paths fail the gate.
- Badge avatars: A's validation fix verified — creator-keyboard now
  asserts a badge-bearing preview actually renders (img naturalWidth>0);
  badge-classifier removed; badge avatar.png 400s are real FAILs again.
- Kidnapping (row 69): seed sweep is a REAL check (hard FAIL when the
  1/250 roll misses all seeds). Sweep + a 64-seed offline scan found NO
  firing seed — root cause identified: src/game/content/events/
  partner_kidnapping.json carries a third, undocumented gate
  `karma_gate: 130`; wealthy_married_40 supplies the $1M but not the
  karma, so the event cannot fire. Finding routed to A/C (lower the
  gate, add karma to the scenario hook, or document). The FAIL row is
  the gate working as intended.
- Last full run with engine recovered: PASS=911 FAIL=1 (kidnapping
  gate) PENDING=0 WARN=7; creator-keyboard 12/12 green.

### Kidnapping deferral — Claude's written approval (final)

Claude clarified the karma gate is legacy parity (`not x_in_y(karma,130)
and one_in(250)`, ~1/500/yr with karma<=100), so a seed sweep is the
wrong tool and a 64-seed miss is expected, not a bug. m2b-r69-kidnapping
is now the ONLY entry in DEFERRED_PENDING, carrying Claude's written
approval and an expiry condition: when A lands the dev-only force_event
hook, the flow must force the offer and assert ransom/options/outcomes/
ledger as a real check. Until then it records PENDING (never a pass).
Final pre-HALT run: PASS=737 FAIL=0 PENDING=1 (the approved deferral)
WARN=7; sheets + self-checks green.
