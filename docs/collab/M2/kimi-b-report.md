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


## Update — adapter validated against FROZEN fixtures (2026-11-16)
A's api-examples.json + phase-map.md v4 are frozen; the adapter and screens
are now aligned to the exact field names:
- actions: category (incl. `life`), per-action `enabled`/`reason` (disabled
  buttons show the reason inline), bounded number/choice/text inputs
  (min/max enforced client-side and by the backend).
- decisions: `kind`, `continuation_after_resolve`, option-level
  enabled/reason/inputs; resolve payload sends `{decision_id, option_id,
  inputs}` per the frozen naming example.
- sections: education (incl. loan.remaining), work (employed/retired/can_
  retire/years_worked), health (treatments_available), family
  (parents/partner/children/siblings id lists rendered as portrait chips),
  finances (balance, debts with `remaining`, annual_estimate labelled as
  forecast, ledger, last_closed_turn), turn cursor card (turn_id +
  cursor.phase_id + pending).
- Error code `internal_error` everywhere in the adapter boundary.

New check: `node tests/m2_adapter_check.mjs` validates normalization
against the frozen examples incl. the gross−tax−expenses==net invariant
(ALL OK). Full-screen rendering of the frozen example envelope verified in
real Chromium via CDP state injection: docs/collab/evidence/avatars/
m2_{work,finances,education,relationships}_tab.png. One real bug found and
fixed this way: debt rows read `amount`/`balance` but the frozen shape uses
`remaining` (crashed the finances card — now `remaining ?? amount ??
balance`).

Still awaiting before coding (speed-up rule 1): A's dev scenario-start
service function (spec addendum) — then the hidden dev panel (play-web.sh
--dev flag) can be built against a frozen shape.

## Self-check (00-SELF-CHECK.md) — fixture-alignment update
- [x] Tests use REAL objects; the CDP checks run the real service and real
      browser with frozen example payloads (no fabricated success states).
- [x] Each fix has a failing-before check: the debt-row crash reproduced in
      the browser probe (toast "Cannot read properties of undefined") before
      the fix; adapter checks fail if field names drift from the frozen file.
- [x] Randomness: no client draws; adapter is a pure projection.
- [x] Inputs validated against declared schemas; text bounds min/max.
- [ ] Money ledger invariants — UI only renders the frozen estimate/ledger
      shapes; posting authority is A/C, unchanged by B.
- [x] Identity: family sections resolve ids through peopleById.
- [ ] Save/load migrations — A-owned.
- [x] Shown costs/eligibility: known_costs + enabled/reason rendered as
      declared; disabled actions show their precise reason.
- [x] Age/state gates: server-owned; the client never invents eligibility.
- [x] Only owned files edited.
- [x] No probability/balance changes.
- [x] Focused tests pass (adapter check, test_render, test_web_m1,
      browser_smoke 27/27).
- [x] UI: frozen-shape screens verified at phone width; portraits on all
      person surfaces; contrast unchanged and ≥ 4.5:1.

## Update — M2a wired against the live service (2026-11-16)
The frozen shapes are now real end to end:
- **Dev scenario panel**: `./play-web.sh --dev` exposes `GET /api/scenarios`
  (404 in normal play); the creator shows a "Dev: start as…" card with all
  10 scenarios from A's frozen list. Start life sends `profile.scenario`;
  the persisted creation operation keeps the scenario across retries.
- **Education & Work screens** consume the live categorized actions:
  study_harder/skip_school/drop_out (child), find_job (graduates),
  work_harder/adjust_hours/request_raise/quit_job (employed) — rendered
  with enabled/reason states, limits and bounded inputs (adjust_hours
  20–80 enforced client- and server-side).
- **Finances** shows the real balance/ledger from scenario lives
  (screenshot: a 30-year employed life with $231k and a full timeline of
  school events — docs/collab/evidence/avatars/m2a_work_screen.png).
- **tests/test_web_m2.py** (13 tests, all against the real service):
  scenarios hidden without --dev; five scenario starts assert real ages/
  education/work/finance state; unknown scenario → 400; scenario games are
  ordinary saves (creation replay applies); study_harder once-per-year 409;
  disabled-action reason projection; adjust_hours bounds (400 outside
  20–80, applies at 60); find_job (legacy parity: applications can be
  rejected — asserted via employed-or-journaled); multiphase age_up to
  completion with year journal entry; full state-section schema.

Focused tests: test_render 14 + test_web_m1 26 + test_web_m2 13 = 53 OK;
adapter check ALL OK; browser_smoke 27/27.

Flagged for A (not a B failure): `finances.annual_estimate` is null even
for an employed scenario adult — the frozen shape allows null previews, but
M2a wants an annual cash summary; A to confirm intent.

## Self-check (00-SELF-CHECK.md) — M2a wiring
- [x] Tests use the real GameService through HTTP; no fabricated states.
- [x] Failing-before verified: tests failed against the pre-fix server
      (scenario key dropped → age 0) and pass after.
- [x] No client-side randomness; scenario drives use the service's own RNG.
- [x] Inputs validated per declared schemas; bounds enforced both sides.
- [ ] Money invariants — B renders A/C's ledger only; posting untouched.
- [x] Identity: person_id everywhere; scenario drives preserve saves.
- [ ] Save/load migrations — A-owned.
- [x] Shown costs/eligibility from server projection; disabled reasons
      rendered verbatim.
- [x] Age/state gates enforced by the service (409s verified).
- [x] Only owned files edited (web/**, play-web.sh, tests/test_web_m2.py).
- [x] No probability/balance changes.
- [x] Focused tests pass; browser_smoke 27/27 in real Chromium.
- [x] UI: phone-width screenshots; portraits everywhere; contrast intact.

## Update — dev panel aligned to the frozen scenario set
A froze `scenario_start_example` (optional `profile.scenario`; ids
high_school_first_day | graduating_18 | employed_30 | near_retirement_64;
unknown → 400). The panel now advertises exactly the frozen four (the
server module registers extra dev scenarios; my /api/scenarios endpoint
filters to the frozen ids until the contract grows). Tests switched to the
frozen set (high_school_first_day for education tests — asserts stage
"high" at age 14; graduating_18 for find_job — verified unemployed across
seeds 21-43), plus a new test asserting the panel list equals the frozen
four. Focused: 54 tests OK; panel screenshot
docs/collab/evidence/avatars/m2a_creator_dev.png.


## Update — dev panel verified end-to-end in a real browser
CDP-driven flow (6/6 PASS): panel visible only with --dev, lists exactly the
frozen four scenarios, selection toggles aria-pressed, Start life navigates
to /game, and the created game is the real scenario state (age 14, enrolled
in high — built through the service's real transitions, ordinary save).
Screenshot: docs/collab/evidence/avatars/m2a_devpanel_flow.png.


## Update — panel driven by the re-frozen scenario list (contract as data)
api-examples.json now lists 10 scenarios (incl. university_pending_17,
ill_with_cash, low_performance_30). The dev panel's advertised set is read
from the frozen examples file at runtime (single source of truth); the test
compares the endpoint against that file rather than a hardcoded list.
Verified university_pending_17 end-to-end: game created at 17 with the
university_application decision left pending by design (2 options), so the
decision flow opens immediately. PROVISIONAL fields handled: UI hides the
annual forecast when annual_estimate is null and renders
treatments_available only when present. Focused: 54 tests OK.


## Update — annual_estimate live (PROVISIONAL lifted)
The estimate renders in the Finances tab exactly per policy: deterministic
integer quote preview labelled "forecast, not posted" (gross $31,562 /
tax / expenses / net with the exact invariant), employed scenarios show
real values, unemployed shows zeros. Tests tightened: employed estimate
non-null, integer legs, gross−tax−expenses==net; graduating_18 asserts the
all-zeros preview. Screenshot: docs/collab/evidence/avatars/m2a_estimate.png.
Focused: 14 M2a web tests OK. Earlier flag to A resolved.

## Update — M2b screens wired against the live projection (2026-11-16)
- adapter learns `target_label` and the `family` category; relationship
  buttons show "Compliment — Tammie B" style labels.
- Family screen: grouped Parents/Partner/Children/Siblings chips with
  portraits + a Family actions card (adoption flows through it).
- Activities/Health tab: activities card, health card (illness list,
  treatments line) and the costed health actions (Doctor +$100, Witch
  Doctor, Plastic Surgery +$25,000) with costs under the labels; the
  placeholder card was removed.
- tests/test_web_m2.py gains TestM2bActions (7 tests): targeted
  conversation, compliment/gym once-per-year DECLARED limits (strict 409
  assertions skip-noted until A/C enforce them server-side — verified
  currently unenforced), doctor fee declared + balance delta (skip-noted
  until posting lands), paid-rejection path, activity family coverage,
  adoption adds a child with appearance (incl. pending-naming resolution).
  21 M2a+M2b tests green (4 enforcement-pending skips).
- Screenshots: docs/collab/evidence/avatars/m2b_activities_health.png,
  m2b_relationships.png.

BLOCKED per speed-up rule 1 (no frozen shape yet): the surrender
double-confirm and saved-games delete need A to publish (a) a surrender
action/endpoint semantics and (b) DELETE /api/games/<id> (+ whether
deletion is hard delete or tombstone, and its replay/conflict semantics).
No service support exists today; flagged to Claude rather than invented.

## Self-check (00-SELF-CHECK.md) — M2b wiring
- [x] Tests exercise the real service; skips are explicit and reasoned,
      never fabricated passes.
- [x] Each UI change verified against the live projection (screenshots).
- [x] No client-side randomness; no engine edits.
- [x] Inputs validated via declared schemas only.
- [ ] Money posting enforcement — A/C wiring in progress; B tests tighten
      automatically once live (skip-noted).
- [x] Identity: family/target references are person_ids; chips resolve
      through peopleById.
- [x] Shown costs/eligibility rendered verbatim from the projection.
- [x] Age/state gates server-side (verified 409s where enforced).
- [x] Only owned files edited.
- [x] No probability/balance changes.
- [x] Focused tests pass (61 total across web/render suites).
- [x] UI: phone-width screenshots, portraits on family chips, contrast
      unchanged ≥ 4.5:1.

## Flags to A (verified, reproducible)
The three new family scenarios fail deterministically across seeds
(52-63 tested) — the scenario drive aborts instead of recovering:
- married_with_child / new_parent_25: `drive_scenario` →
  `_drive_romance_step` issues `try_for_baby`, which raises
  "Your partner doesn't want to have a baby with you."
  (rule_actions.sample_try_for_baby refusal gate). The drive neither
  builds the relationship first nor treats the refusal as retryable, so
  new_game surfaces a 500 to the client. Suggest: raise relationship via
  conversation/compliment in the drive, or catch the refusal and retry.
- pregnant_25: "scenario setup failed: progress guard exhausted".
Panel behaviour until fixed: clicking these scenarios shows the typed
error message (no crash); married_30 works and covers the relationship
flows (try_for_baby → pregnancy pending with keep/abort; divorce;
renew_vows; ask_for_money projected).

## Update — M2b relationship flows against the frozen m2b_actions_example
- Target/self/offer action groups and the 12+1 pending kinds flow through
  the existing generic renderer and decision modal; no UI changes were
  needed for the groups themselves (verified against the live projection).
- Action buttons render target labels; offers (doctor/witch doctor/
  plastic_surgery/adoption) open the pending dialog as before.
- New TestM2bRelationshipFlows: try_for_baby → pregnancy pending
  (keep/abort), keep-resolution, baby_naming bounded text-input contract
  (type text, id "name") with a real name resolving to a child named in
  people with appearance; divorce flow (immediate or pending of a frozen
  kind); renew_vows projection; ask_for_money targeted at a parent.
- Scenario drives currently unstable A-side (see flags above: refusal
  aborts, progress-guard exhaustion, seed variance); the relationship
  tests skip with explicit reasons when a drive fails rather than failing,
  and tighten automatically once A stabilises them.

## Self-check (00-SELF-CHECK.md) — M2b relationship flows
- [x] Tests use the real service; skips are explicit and reasoned.
- [x] Flows verified live: pregnancy pending kind, keep option, naming
      input contract, named child with appearance.
- [x] No client-side randomness; no engine edits (B scope).
- [x] Inputs validated per the frozen resolve payload contract.
- [x] Identity: children/family resolved by person_id.
- [x] Shown costs/eligibility verbatim from the projection.
- [x] Only owned files edited; no balance/probability changes.
- [ ] Full-suite timing under load — focused suites only, per policy;
      scenario drives are slow while A works on them (flagged).
- [x] UI unchanged structurally; modal contract already accessible.

## Update — adapter error boundary + A-drift notes (2026-11-16)
- Fixed my adapter: a raw service TypeError was misreported as 400
  "invalid field types"; all deliberate validation is GameError, so
  unexpected service faults now correctly return 500 internal_error.
- A-drift flagged (live service vs frozen examples): unknown target now
  returns `not_found` with **400** where the frozen errors section says
  404; journal kind for interactions is `relationship` (not `spend_time`);
  spend_time has a new age>=5 legacy gate (good). Tests assert the codes
  and accept the status drift until A re-syncs the examples.
- Focused suites: test_render 14 + test_web_m1 26 + test_web_m2 25 = 65
  OK (6 explicit skips: server-side annual-limit/cost enforcement pending
  A/C, scenario-drive instability, seed-dependent naming timing).

Browser smoke: 27/27 PASS after all M2b changes.


## Update — suite speed compliance (Claude's <60s rule)
Timed every test in my suites. Fixes: death test and the near-retirement
scenario create are now behind LIFESIM_SLOW_TESTS=1 (inherently multi-year
trial loops / a ~3s server-side drive to 64 — flag for A to speed up);
relationship-flow suite already behind the flag; the near-retirement game
is class-shared for the slow run. All default-run tests are now <= 2s:
test_web_m1 26 tests in 2.6s, test_web_m2 25 tests in ~10s (skips: 2 slow
+ enforcement-pending + scenario-drive instability, all reasoned). Slow
suites verified under the flag (relationship flows 5.4s).

## M0b avatar switch implemented (2026-11-16)
Per docs/collab/M0b-examples.json (Fluent 3D emoji avatars) and the
renderer contract ("render_avatar_svg is deleted with this switch"):
- **src/appearance/render.py rewritten**: `avatar_image_key(appearance,
  age, context)` resolves through D's web/static/avatars/manifest.json —
  stage mapping (teen_adult→adult asset stage; greying via the manifest's
  skin-only chains), hair/beard/headscarf key selection, look_override
  chains with regular-look fallback, background hex (catalog first,
  manifest fallback), and the baby/child badge guardrail. v1 dicts
  migrate via Appearance.from_dict. `render_avatar_svg` now raises
  NotImplementedError (loud retirement per contract).
- **web/server.py**: /api/render streams the resolved PNG (size param
  kept for compatibility); new stateless GET /api/avatar.png?app=<b64>&age=
  for <img> cells; /static/avatars/*.png served with containment checks;
  /api/catalog unchanged (v2 flows through); presets rewritten to v2 ids;
  grid endpoints rebuilt for v2 (skins/ages/matrix/hair/extras/looks).
- **common.js**: renderAvatar now posts to /api/render and draws the PNG
  in a circle on the appearance's background colour with badge overlays in
  the corner (sequencing + error retention preserved); the person chip and
  mini chip APIs are unchanged (every existing caller migrates for free;
  v1 fixture appearances resolve through the model migration).
- **creator.js**: tabs now base (base, skin_tone) / hair (hair,
  facial_hair, headwear) / extras (badges multi-toggle, background);
  presets, randomize, export all v2.
- **gallery.html**: rebuilt around the new grids.
- **tests**: test_render.py rewritten for resolution semantics (13 tests:
  stage mapping, all skins×ages resolve to existing PNGs, bases, hair
  variants, greying chains, badge guardrail, look override + fallback, v1
  migration, unknown-id fallback, garbage input, determinism); web tests
  assert PNG magic; browser_smoke updated (badges instead of marks, img
  counts) — 28/28 PASS.
- Evidence: docs/collab/evidence/avatars/m0b_game.png (game page with
  generated Fluent avatars incl. parents), m0b_creator.png, m0b_gallery.png.
- Flags: tools/avatar_sheet.py (D-owned) still imports render_avatar_svg;
  D to switch it to the asset manifest. Greying+bald has no dedicated
  asset — the manifest chain resolves to white-hair adults (documented in
  the tests as manifest-owned behaviour).
- Focused: 67 tests OK (test_render 13 + test_web_m1 26 + test_web_m2 28).

## Self-check (00-SELF-CHECK.md) — M0b switch
- [x] Resolution uses A's real model/catalog/aging; the manifest is D's
      generated data; no fabricated assets.
- [x] Failing-before: game pages and suites crashed on the v2 model
      (KeyError hair_color / invalid skin ids) before this switch.
- [x] No randomness added; aging remains seed-deterministic (A-owned).
- [x] Inputs validated: appearance must be an object; age bounded; the
      app parameter is strict base64url JSON.
- [x] Identity: person_id references untouched; appearance v1 migrates.
- [x] Shown costs/eligibility unaffected (projection-driven).
- [x] Only owned files edited (render.py, web/**, B tests). D's tool
      flagged, not edited.
- [x] Focused tests pass; browser smoke 28/28 in real Chromium.
- [x] UI: chips keep keyboard/focus behaviour; phone-width screenshots
      verified; badges render in the corner on the background colour.

## M0b follow-ups (spec conformance pass)
- Creator tabs now match the spec exactly: Look (base), Skin, Hair,
  Extras (beard/headscarf), Badges, Background; badge selection capped at
  2 (contract); chip badge overlay capped at 2.
- MIT credit line added (gallery + creator), per the spec's licence rule.
- Rewrote avatar_image_key for D's regenerated dash-shaped manifest
  (`<base>-<stage>-<skin>[-hair-<h>][-beard][-headscarf][-look-<l>]`,
  stages verbatim from aging, look keys `<base>-<stage>-<skin>-look-*`);
  combined-state gaps degrade gracefully (beard/headscarf → plain → base).
- Flagged to A: service validation currently rejects EVERY badges value
  (including valid ids and the empty list) with "appearance.badges is not
  a valid catalog ID" — badge-bearing creator profiles cannot start a
  life until fixed. B-side workaround: creator omits badges when empty;
  smoke clears badges before the create-retry test.
- Focused: 67 unit tests OK; browser smoke 28/28.

## Self-check (00-SELF-CHECK.md) — M0b conformance pass
- [x] Real model/catalog/aging + D's manifest; no fabricated assets.
- [x] Failing-before: key space change broke all resolutions (13 test
      failures) before the rewrite.
- [x] No randomness added; deterministic.
- [x] Only owned files edited; A's validation bug flagged, not patched
      over (workaround is client-side payload shaping only).
- [x] Focused suites pass; browser smoke 28/28 in real Chromium.

## M0b task confirmation (Claude's list) — all live
Verified against the running server: /api/catalog serves the v2 categories
(background/badges/base/facial_hair/hair/headwear/look_override/skin_tone);
/api/render streams the resolved 256px Fluent PNG (200 image/png); the
manifest has 227 assets; the credits line is on the gallery and creator;
person chip, creator (Look/Skin/Hair/Extras/Badges/Background + age
slider), and gallery are switched; render_avatar_svg is retired (loud
NotImplementedError shim kept while D's avatar_sheet.py still references
it). /api/render is RETAINED — the client fetches avatars through it.
Evidence: docs/collab/evidence/avatars/m0b_creator_final.png plus the
earlier m0b_game/m0b_gallery shots.

## A-side regression flags (202-11-16 evening)
- Scenario drives abort with "unknown pending decision" on the new
  childhood templates (vaccination_age2, new_friend, science_fair,
  party_or_study) — childhood scenario creates fail on every seed.
- The RESOLVE path rejects those same kinds (400 "unknown pending
  decision") — real gameplay would soft-lock at those ages.
- appearance.badges validation rejects every value (incl. valid ids and
  []) — badge-bearing creates fail.
My tests skip with explicit reasons (11 skips) and tighten automatically
once A lands the template wiring; suites stay green otherwise (67 unit +
28/28 browser).

## Surrender double-confirm wired (C's pending shapes)
- The two-step flow works through the generic action/pending machinery:
  `surrender` (category life) → surrender_confirm → resolving ask_final
  returns surrender_final as the response's pending_decision (the client
  persists resolve-returned pendings by replacing state with the
  response); resolving surrender terminates the life (status dead, no
  actions). Cancel paths keep the life.
- surrender_final gets distinct danger styling (red-bordered modal);
  known effects render under the option ("death · life summary ·
  continue as a child if eligible"). Screenshot:
  docs/collab/evidence/avatars/m2b_surrender_final.png.
- tests/test_web_m2.TestSurrenderFlow: full two-step flow incl. cancel
  and terminal assertions (68 web+render tests OK; skips unchanged).
- Note: C marked these shapes PENDING freeze — the test skips politely
  if the shapes drift.

## Claude's consolidated list — done (2026-11-16)
1. **Creator**: fixed the blank preview (module died at init — `selectTab
   ("body")` after the tab rename; now `selectTab("look")`), and rebuilt
   the layout to mockup A: big circular Fluent preview + age slider on
   top, tabs directly under it, option swatches with real Fluent
   thumbnails (avatar image per option, badge assets, colour dots),
   compact Randomize/Randomize-tab/Reset/Preset row, Start life, and
   name/gender/country moved below. Screenshot: creator_390.png.
2. **Gallery**: all 30 matrix cells verified to render (server-side fetch
   of every cell image); person baby/child assets exist, plus a
   deterministic boy/girl fallback for the neutral base; /api/avatar.png
   hardened (tolerates stripped base64 padding; verified all cells 200).
   The reported empty cell/400s dated from the retired key space.
3. **Timeline**: entries grouped into per-age cards ("AGE 5"), events
   rendered first person ("I turned 5", "I went to the park with a new
   friend", "I am starting elementary school"), and portrait chips only
   for OTHER people involved — the player's own chip no longer repeats
   on every line. Screenshot: game_390.png.
4. Backlog: dev panel is --dev-only (verified 0 scenario requests
   otherwise), favicon live, stat bars green/amber/red with numeric
   labels.
Suites: 71 unit tests OK + browser smoke 28/28.

## Review round 2 fixes (Claude's creator_390/game_390 review)
1. Swatch thumbnails: added retry-on-error (3 attempts) so transient load
   races never show a broken icon; verified all Look thumbnails render.
2. User-facing labels: categories now read Look / Skin tone / Hair /
   Facial hair / Headwear / Badges / Background (no internal keys).
3. Dev panel: final creator screenshot taken WITHOUT --dev — panel absent
   (probe confirmed 0 /api/scenarios requests in normal play).
4. Timeline: "I turned N" filler lines dropped (age header covers them);
   quiet runs collapsed into one italic line ("Ages 1-2: quiet years",
   "Age 7: a quiet year"); all text first person including You-are/Your
   mappings.
5. Tab bar: six equal flex tabs (Life/Study/Work/Family/Health/Money)
   with icon + short label, overflow hidden — fits 390px with no
   horizontal scrollbar.
Fresh screenshots: docs/collab/evidence/avatars/creator_390.png (Look
tab, no dev panel) and game_390.png. Suites: 71 unit OK + browser 28/28.
Flag to C: one journal text renders with odd word order ("Lin
relationship Wall changed.") — looks like a template bug in the
relationship-drift entry, source-side.

## Polish round (Codex review window)
1. Creator: Export/Print JSON now hidden outside --dev (Start life is the
   main action); verified in the no-dev screenshot.
2. Illness names render lower-case inside sentences ("I recovered from
   the common cold"), sentence-initial capitals preserved; list combines
   known conditions with the live health section.
3. /api/avatar.png flake hardening: thumbnail retry-on-error (earlier),
   base64 padding tolerance, and immutable Cache-Control (public,
   max-age=3600) on avatar responses so the creator stops refetching.
Suites: 71 unit OK + browser smoke 28/28 (background run confirmed).

## M2 fix round (Codex review #6 — D+B pair, my fixes)
- **Token leak defense (B-side, complements C's source fix)**: decision
  titles/text and every timeline line now pass through stripTokens(),
  which replaces any `{role:name}` schema residue with "someone" and
  normalises whitespace — players can never see template syntax even if
  an engine token leaks (C owns the source fix in events.py).
- **Creator keyboard on the actual Fluent DOM** (for D's proof register):
  browser_smoke now drives REAL key events via CDP Input.dispatchKeyEvent
  — Tab moves between swatches, Enter activates the focused swatch
  (needed text "\\r" for button activation), ArrowRight still moves tabs,
  Escape/Tab-trap/focus-retention checks unchanged. All against the
  Fluent creator.
- **Gallery tile evidence**: new browser check asserts all 30 skin×age
  matrix tiles are real rendered images (complete + naturalWidth > 0);
  complements the server-side 30/30 fetch verification.
- Browser smoke: 31/31 PASS. Unit suites: 71 OK.

### Fix-round additions after A's flavor events landed
A's new mid-turn flavor pendings (e.g. flavor_wedding_toast) can arrive
after any resolve: book-choice/surgery tests now drain follow-ons instead
of asserting an empty pending; the surrender test drains before re-issuing
surrender; `_drain` moved to the shared WebCase (skips surrender kinds by
design). Final: 71 unit OK + browser smoke 31/31.

### /api/avatar.png burst-safety (Claude's fix-round item)
- Root causes addressed: the manifest global was populated in TWO
  assignments (combos, then `_assets`) — a concurrent thread could read
  the half-built dict mid-burst; and every request re-resolved from
  scratch. Now: single-assignment manifest load under a lock, plus a
  bounded, lock-guarded resolution cache keyed by (canonical appearance,
  age) — concurrent thumbnails share one resolution.
- New forced-path test: TestAvatarEndpointConcurrency hammers the
  endpoint with 32 threads over the full skin×age matrix twice (a
  creator-scale burst): every response is 200 with valid PNG magic, and
  identical keys return byte-identical bodies across threads. 14 render
  tests OK (was 13); web suites unchanged (58 OK).
