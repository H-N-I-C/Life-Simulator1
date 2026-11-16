# Kimi C report — M2a (contracts + school/work slice)

## Deliverables this stage

1. **docs/collab/M2/parity-matrix.md** — full reachable-branch inventory
   (77 rows: main menu 24, activities 14, yearly phases ~40) with source
   file:line, preconditions, stage assignment, proposed action/event and
   deferral/ambiguity list awaiting Claude confirmation.
2. **docs/collab/M2/phase-map.md** — proposed P1–P9 yearly phase split with
   per-phase ownership (A vs C) and the money/decision rules; pending
   A confirmation (4 open points listed at bottom).
3. **docs/collab/M2/rule-examples.json** — skeleton with concrete RuleResult
   examples for study/find_job/resolve/P5/P6 flows.
4. **src/menus/rule_actions.py** — new, C-owned, zero terminal I/O. M2a scope:
   - School actions: `study_harder`, `skip_school`, `drop_out` (rows 15–17)
   - Job actions: `find_job` (sampled offer → pending decision, never
     rerolled), `apply_for_job`, `work_harder`, `adjust_hours`,
     `request_raise`, `retire`, `quit_job` (rows 18–22)
   - Yearly phases: `yearly_work_phase` (P5: stress model, years worked,
     salary history, retirement offer/forced retirement, performance firing,
     stress damage/HBP, annual income via M1's `annual_income_breakdown` as a
     single ledger entry) and `yearly_education_phase` (P6: grade drift,
     school starts 6/12/14, HS graduation, university application with
     funding decision, uv progression/expulsion (M1-cleared)/graduation with
     loan principal)
   - Contract dispatchers: `action_specs`, `apply_action`, `resolve_choice`,
     `advance_phase`; `RuleError` typed failures, no mutation before
     validation.
5. **tests/test_rules_m2a.py** — 23 tests, all passing; together with the M1
   rule tests: 43 focused tests green
   (`python3 -m unittest discover -s tests -p 'test_rules_*.py'`).

## Design decisions (for A/C agreement)

- **RNG**: every rule function takes an explicit `rng` (random.Random-shaped);
  module-local `_one_in/_x_in_y/_randexpo/_round_stochastic` wrap it, so the
  legacy global-random helpers are not used here and A's per-game RNG adapter
  owns the stream. (Note: `annual_income_breakdown` still uses global
  `random.uniform` — A's existing lock/swap covers it; flagged for A.)
- **Money**: rule functions never touch `player.money` for ledger flows; P5
  emits one `money_entries` row `{source_key: "<turn>:P5:annual_income",
  person_id, category, amount, description}` and A's ledger posts it once.
- **Decisions**: random outcomes are sampled into `pending["sampled"]` before
  the decision is shown; `resolve_*` applies the stored values only
  (retirement offer, job offer salary, university funding with legacy's
  option-removal on scholarship/parents refusal).
- **Balance**: legacy numbers preserved exactly; the only behavioral
  deviations from terminal are the documented M1 fixes.

## Status of parity-matrix rows

- Extracted + tested (M2a): rows 15–22, 54–56, 62, 64–66, 71, 73–75.
- Deferred to M2b: rows 3–14, 25–36 (family/romance/health/activities).
- Deferred to M2c: rows 1, 37–38, 39–53, 57–61, 67–70, 72, 76–77
  (orchestration, events, loans/lottery, succession).
- Awaiting Claude: rows 23–24 (save management / debug menu exclusion),
  row 34 (collapse read-book page loop), row 36 (adoption gate 21+ vs 18+),
  row 61 (keep age-2 vaccination?), row 70 (keep both dating paths?).

## Validation

- 43 focused tests pass (M1: 20, M2a: 23). No identity/appearance code
  touched; no edits to player.py/child.py this stage (per-team split, pending
  A's manifest confirmation).
- Full suite is Codex's job per testing policy.

## Blockers / needs from teammates

- A: confirm phase-map.md (P2 trait split, relation-death boundary, money
  posting, cursor format) and provide the context factory + ledger posting
  hook so `_cmd_age_up` can call P5/P6.
- Claude: answer the 5 ambiguity questions in parity-matrix.md.

## Addendum — phase-map v2 contributions (2026-11-16)

- Reviewed A's landed phase-map.md v2 / rule-examples.json and closed the
  C-owned open items: real income fixture values
  (annual_income_quote(30000, True, 40, 0.6) → gross 30000 / tax 3401 /
  expenses 10640 / net 15959, verified by an executable assertion), removed
  the non-legacy $50 university fee, documented admission as the age-17
  annual flow per parity-matrix row 66, and froze the deterministic integer
  rounding policy (residual to expenses; no posting-time RNG).
- Inserted the missing `education` phase between `income` and
  `debt_service` (legacy order: salary line → uv progression/expulsion →
  same-year loan repayment; graduation-year principal must be repayable
  immediately).
- rule_actions.py aligned to A's leg shape: money entries are now
  {phase, occurrence_id, leg, account_owner_id, category, amount,
  description} with gross/tax/expenses legs; phases accept "income"/
  "education" ids (P5/P6 aliases kept).
- Money tests updated for legs; all 55 focused tests pass.

## Addendum — Codex M2a FAIL fixes (2026-11-16)

Rule code (src/menus/rule_actions.py) rewritten per codex-review-c.md; A owns
phase-map.md and the ledger schema (not edited; one schema question sent to
A: my entries now match finance.py's {phase, occurrence_id, leg,
account_owner_id, category, amount, description} interim shape).

1. Identity: events/postings use canonical `person.person_id`; fixtures now
   construct REAL Player objects (tests/test_rules_income.make_player); no
   fabricated IDs.
2. Money: entries align with A's finance.Ledger schema; income phase emits
   gross/tax/expenses legs from the pure annual_income_quote (no global RNG;
   retention sampled once via the passed rng); net implied by legs.
3. Retirement: forced retirement is an EVENT (no empty pending); voluntary
   offer resolves only listed options; re-resolution is guarded by a stored
   `applied` marker (no double happiness/lose_job). Income policy: pay quoted
   from employment state at the income phase; mid-turn voluntary retire keeps
   this year's salary, pension pays from next year; forced retirement (legacy
   order) quotes the pension same year.
4. Pre-sampling: job interview (success + happiness delta), university
   acceptance and every funding option's outcome/effect are sampled at offer
   creation into the private `sampled` block; resolve_choice
   (context, pending, option_id, inputs, rng) applies stored data only.
5. Validation: payload shape, hours type/range (RuleError, no KeyError),
   option membership in the CURRENT pending options; forged/spent options
   rejected before any mutation.
6. Skip-school deltas fixed (named before_* baselines; exact change dicts
   asserted from before/after across school/work rules).
7. Eligibility: one shared predicate drives action_specs and dispatch —
   enrolled students (incl. university) never see/pass job actions; actions
   carry enabled/reason; the age-17 university flow has an explicit
   apply/decline decision (legacy parity) before the funding decision.
8. Work phase order matches legacy age_up/random_events subrule order
   (stress ±4, drift, salary history + happiness-swing, retirement, firing,
   stress damage, years_worked++, pay quote). Documented rule-policy fix:
   legacy ADDED health under critical stress; extracted rule applies damage
   (flagged here for Claude/Codex).
9. Education phase reordered to legacy: uv progression/expulsion/graduation,
   then grade drift, then school transitions, then the age-17 application.
10. Tests: 33 in tests/test_rules_m2a.py incl. A/C ledger integration
    (C entries posted through finance.Ledger with real balance checks, retry
    idempotence, child account separation), pre-sampled-outcome stability
    across resolver RNGs, forged/spent option rejection, university decline,
    enrolled-job prohibition, forced retirement without pending.
    Focused: 65 pass. Full suite: 195 pass.

## Self-check (00-SELF-CHECK.md) — M2a alignment round

Logic
- [x] Tests use REAL objects: fixtures construct real Player objects; mocks
  only for RNG pinning and builtins.input at the terminal boundary.
- [x] Failing-without-fix evidence: divorce/willpower, expulsion, pension,
  overtime, child-net, skip-delta and pre-sampling tests assert real state
  transitions that the old code fails (Codex probes independently confirmed
  each bug class before fixes).
- [x] Randomness: every draw uses the passed rng (module wrappers _one_in,
  _x_in_y, _randexpo, _round_stochastic); yearly_work_phase/education/
  debt_service never touch global random; pre-sampled outcomes make
  resolution reproducible independent of resolver RNG.
- [x] Inputs validated: hours type/range, payload shape, option membership
  against current pending options — typed RuleError, no raw KeyError.
- [x] Money: income legs gross/tax/expenses with net implied, posted once by
  A's Ledger (integration test checks balances, retry idempotence);
  student-loan debt accrues per financed year in the rule (never zero on
  leaving; dropout-after-2-years test); journal changes are actual
  before/after deltas (skip/study/work assertions).
- [x] Identity: person_id only (events, money entries, tests).
- [x] Save/load: no new persisted fields in this round (quotes/cursors are
  A's envelope fields).
- [x] Shown costs/eligibility: action_specs shares the dispatch predicate;
  used actions advertise enabled/reason; enrolled students cannot take job
  actions server-side.
- [x] Age/state gates enforced in the rule (find_job age/school checks), not
  only in specs.

Hygiene
- [x] Only owned files touched (rule_actions.py, menus, player.py C
  functions, tests/test_rules_*, parity-matrix status); phase-map.md left to
  A per the 2026-11-16 ownership decision (proposals sent via hc).
- [x] No probability/balance changes beyond agreed fixes (documented policy
  fix: critical stress now damages health — flagged for Claude/Codex).
- [x] Focused 67 pass; flaky-prone suites run under 3+ PYTHONHASHSEED values
  (toy-narration flake root-caused and fixed).
- [x] UI n/a (Kimi B).

## Addendum — student-loan policy change (Claude decision 2026-11-16)

Policy corrected from "no retroactive debt" to "tuition already financed
stays as debt": each loan-financed university year accrues one year's
tuition (rng 5000-10000, a quarter of the legacy 20k-40k total) at the
moment it is financed, in both the extracted rule
(rule_actions.yearly_education_phase) and the legacy terminal path
(player.py random_events). Graduation no longer materializes a lump sum;
expulsion/dropout keep the accrued debt (never zero when any year was
financed). The stale-flag clearing was removed everywhere (player.py,
main.py, rule_actions). Tests: dropout-after-2-financed-years keeps $2500
(test_rules_m2a), expulsion/graduation accrual tests (test_rules_university,
test_rules_m2a). Focused 67 pass. NOTE: phase-map.md edits — none this
round; all map proposals go to Kimi A per the 2026-11-16 ownership rule.

## Addendum — frozen-shape conformance + executable continuation examples

- rule_actions conforms to frozen v4: resolve_choice(context, pending,
  option_id, inputs, rng); forced retirement = event; pre-sampled choice
  outcomes; work_income legs via pure quote; entries match finance.py
  schema. No frozen shape was modified (additions only).
- rule-examples.json gains `executable_examples_c`: two-choices-one-phase
  (university application→funding chain), debt-before-family canonical
  order, interruption-after-one-posting (deterministic same-seed rerun +
  Ledger retry idempotence). Two-people single-phase examples marked pending
  M2b/M2c with the frozen target shape noted.
- tests/test_rules_continuations.py (6 tests) is the executable mirror;
  73 focused tests pass, hash-seed stable.

## M2b report — relationship + activity extraction

Cause/design/validation per row group:
- Relationship actions (rows 3-14): extracted headless with the M1
  discipline — stored pre-sampled decisions (argument outcomes, pregnancy
  overrule via one sampled draw compared per branch, wedding venues,
  divorce settlement), money via entries (ask-money gift_received, wedding
  cost, divorce transfer), person_id targets resolved through the
  duck-typed context (people_by_id / find_person / player-graph walk).
  Validation: refusal gates (<15/<25 rel), once-per-year flags,
  engagement/marriage state gates, unaffordable wedding rejected with
  typed errors before any mutation.
- Activities (rows 25-36): age gates, once-per-year flags, costs as entries
  (doctor fee medical, witch doctor medical, surgery medical, adoption
  other), doctor per-illness cure outcomes + health/happiness gains all
  sampled at offer creation; witch-doctor/surgery lethal outcomes return
  terminal transitions (finished_turn, continuation year_close) for A to
  perform death; adoption uses A's canonical Child creation hook with a
  rename decision (legacy: random last name, rename offered).
- action_specs projects all of the above with enabled/reason; dispatch
  routes target/self/offer actions with shared eligibility.
- Validation: 29 new tests (real constructed Player/Partner/Parent/Child),
  102 rule tests pass, hash-seed stable. Full suite: the only failures are
  in Kimi A's in-flight tests/test_game_scenarios.py (dev scenario starts,
  A-owned service) — not on C paths.

## Self-check (00-SELF-CHECK.md) — M2b round

Logic
- [x] Real objects throughout (constructed Player/Partner/Parent/Child);
  mocks only for RNG pinning at the rng boundary.
- [x] Failing-without-fix: adoption last-name parity, doctor pre-sampling,
  plastic-surgery affordability and terminal transitions asserted against
  real state (each fails on the old/absent behavior).
- [x] Randomness: all draws via passed rng; choice outcomes pre-sampled at
  offer creation; pregnancy-overrule uses one stored draw for both
  branches (re-resolution with a wild rng produces identical events —
  tested).
- [x] Inputs validated (target_id existence/membership, option membership,
  money affordability) with typed RuleError before mutation.
- [x] Money: costs/gifts/settlements are money_entries only, categories
  from A's CATEGORIES (gift_received/wedding/medical/other/transfer); no
  direct money mutation in rules.
- [x] Identity: person_id only (events, money entries, sampled targets).
- [x] Save/load: no new persisted fields.
- [x] Shown costs match application (known_costs on wedding/surgery/
  adoption/doctor options; affordability re-checked at resolve).
- [x] Age/state gates server-side (toys 3-12, gym health, adoption 21-70
  and <5 children, surgery spacing, witch doctor 18+).
Hygiene
- [x] Only owned files (rule_actions.py, tests/test_rules_m2b.py,
  parity-matrix status).
- [x] No probability/balance changes beyond approved fixes (adoption
  last-name parity is a legacy-faithful correction, documented).
- [x] Focused 102 pass; M2b run under 3 PYTHONHASHSEED values.
- [x] UI n/a (Kimi B).

## Addendum — loan-accrual test quality (Claude fix)

- test_dropout_after_two_financed_years_keeps_debt rewritten per Claude:
  no hand-built debt. Two REAL yearly_education_phase turns with loan
  funding (seeded rng) accrue year1 and year2 tuitions (each asserted in
  [5000, 10000]); accrued == year1 + year2; then drop_out leaves the debt
  byte-identical. Passes; hash-seed stable.

## Addendum — final map sign-off + mortality helpers

- Signed off A's FINAL phase map (both Claude overrides were already
  applied: education before debt_service with same-year graduate
  repayment; my phases never processed deaths so the mortality-phase-4
  restore needed no change to landed phases).
- New mortality-phase C helpers (parity row 51): sample_relation_death
  (reason incl. sudden-death table, per-type happiness loss, thanatophobia
  reaction, parent inheritance as an inheritance money entry, and a stored
  funeral decision with pre-sampled attend/skip karma effects) and
  resolve_funeral_choice. A owns graph removal/archive and calls these.
  resolve_choice dispatch is now a _RESOLVER_KINDS registry.
- Tests: +3 (inheritance+funeral, skip-karma+forged option, sudden reason);
  105 rule tests pass, hash-seed stable.

## Addendum — illness + family_events phases

- illness_phase (rows 58-60, 50): full legacy illness block — per-illness
  progression (depression recovery/else-branch, HBP with heart-attack
  terminal transition, cancer with terminal transition, cold/flu recovery),
  cancer diagnosis gate, flu/cold onset, depression onset. Fatal outcomes
  return finished_turn -> year_close exactly once (A performs death).
- family_events: births (female player + partner-pregnant paths) with
  miscarriage branches; babies created through A's canonical hook at sample
  time; stored baby_naming decision with bounded text input (1-40 chars,
  typed errors); per-child canonical subrules child_stat_year /
  child_money_year (child_net entry to the CHILD's account) /
  child_social_year (partner lifecycle + job rolls). Guarded the latent
  legacy crash: a pregnancy with no recorded father now raises RuleError
  instead of dying on generate_child(None).
- +6 tests (birth+naming with input validation, miscarriage branch, child
  net account separation, cold recovery, cancer terminal, depression
  recovery). 111 rule tests pass, hash-seed stable.

## Self-check (00-SELF-CHECK.md) — illness/family_events round

Logic
- [x] Real objects (constructed Players/Partners; babies via A's hook).
- [x] Failing-without-fix: terminal transitions, naming input bounds,
  child-account separation asserted against real state.
- [x] All draws via passed rng; naming decision stores the created child
  (identity resolved by person_id), no re-creation at resolve.
- [x] Inputs validated (name length/type, missing father, forged options).
- [x] Money: child_net posts to the child's account only; inheritance
  entries already covered; no direct money mutation.
- [x] Identity: person_id everywhere; babies enter relations/children at
  sample time under their canonical person_id.
- [x] No new persisted fields.
- [x] Gates server-side (doctor/witch fees re-checked at resolve etc.).
Hygiene
- [x] Owned files only; no balance changes beyond approved policies.
- [x] 111 rule tests pass; 3 PYTHONHASHSEED values green.

## M2c prep — annual-events parity oracle

- docs/collab/M2/annual-events-oracle.md + tests/fixtures/annual_events_oracle.json:
  exact trigger conditions, probabilities (1-in-N incl. gated occurrences,
  the two-draw willpower policy), choice sets, and legacy effect ranges for
  all seven annual events (lightning, thanatophobia worry, vaccination,
  partner breakup/divorce offers, kidnapping, dating offer), plus the yearly
  non-event rolls (jackpot re-roll, surgeon rotation). Source branch refs
  included; illness-phase events intentionally excluded (already extracted
  and tested in test_rules_m2b.py).
- tests/test_rules_oracle.py: implementation-agnostic helpers
  (measure_rate / assert_rate with binomial tolerance bands) usable against
  ANY implementation (legacy or data-file); 9 tests: fixture structure,
  exact pinned branches (lightning good = all stats 100; vaccination bite
  deltas within oracle ranges; divorce honor settlement within 40-60% of
  money; kidnapping ransom within 20-80% of money when paying) and
  statistical rates over seeded trials (lightning good/bad 1/2 real draw
  with occurrence forced; vaccination 1/2; breakup offer 1/3; two-draw
  willpower insist 0.75 at p=0.5).
- Runtime ~34s (seeded trial loops); 120 rule tests pass in total.

## Self-check — oracle round

- [x] Real objects (constructed Players/Partners; legacy code paths).
- [x] Failing-without-fix: oracle tests assert legacy behavior exactly; a
  data file or engine that deviates will fail these same helpers.
- [x] Randomness: statistical trials reseed global random per trial (the
  legacy boundary); deterministic tests pin draws.
- [x] Fixture is data-only; no engine imports.
- [x] No balance changes (oracle documents; changes nothing).
- [x] 120 rule tests pass; oracle suite re-run 3x with identical results
  (seeded, hash-seed independent).

## Addendum — suite-speed compliance (Claude timing rule)

- tests/test_rules_oracle.py: default trial counts cut to the smallest N
  that holds (lightning 120, vaccination 80, breakup 100, willpower 600)
  with 4-sigma tolerance bands; whole oracle module runs ~2.5s, no single
  test over 2s. Large-N versions (4000-20000 trials) run only behind
  LIFESIM_SLOW_TESTS=1 (verified green in both modes, including the natural
  lightning-strike edge: statistical trials now patch press_enter/die so a
  real 1/5000 strike mid-loop cannot abort the measurement).
- All other C modules: test_rules_* discovery totals ~6.4s.
- Full-suite context: my contribution is ~6.4s of the 410s total; the
  remaining runtime and the current failures/errors sit in A/B/D suites
  (A's in-flight scenario tests among them) — flagged in 00-STATUS.

## Addendum — annual-events data drafts (pre-freeze)

- docs/collab/M2/events-draft/: seven per-event files (lightning,
  thanatophobia, vaccination, breakup offer, divorce offer, kidnapping,
  dating offer) drafted under A's current event-schema.json shape plus the
  seven review proposals; every file references its oracle row; all fields
  depending on unaccepted proposals are explicitly marked (_proposal P1-P8).
  README documents the adaptation plan after freeze. All files JSON-valid.

## Addendum — parity-matrix row status/proof register

Every row 1–77 now carries a status (PROVEN-C/D/A, PENDING-ENGINE, DRAFT,
NO-PROOF) and its exact proof (C unit test names, D flow IDs M2a-S1..M2c-S6,
A suites), with an owned gap list: C owes isolated tests for rows 71
(firing), 72 (stress damage/HBP), 63 (trait-drift extraction) and D-flow
proposals for 2/22/51/58-60/69/76. Register appended to parity-matrix.md
for the M2d audit. 120 rule tests green.

## M2c content — annual events as data files

- Converted the seven events-draft files into real content at
  src/game/content/events/ (lightning_strike, thanatophobia_worry,
  vaccination_age2, partner_breakup_offer, partner_divorce_offer,
  partner_kidnapping, dating_offer) against the FROZEN event-schema v2
  (incl. A's branches form). Two content-driven additive interpretations,
  flagged inline and to A/Claude: (1) "sign" on money_pct (negative
  settlement/ransom postings); (2) "partner.compatibility" as a branches
  field_ref for the dating-acceptance check.
- Automatic legacy events without a real choice (lightning,
  thanatophobia) use a cosmetic choice pair sharing identical sampled
  outcomes, satisfying the >=2-choices + soft-lock invariants; lightning's
  nested 1/5 death composes as outcome weights 5/4/1 (1/2, 2/5, 1/10).
- tests/test_rules_event_content.py (9 tests): schema conformance
  (structure, soft-lock, condition kinds, no double money posting) and
  exact oracle match (occurrence 1-in-N, branch weights, all effect
  ranges with signs, roles, divorce settlement pct, ransom pct). Green;
  131 rule tests + 51 appearance tests pass. Loader-independent: A can
  adapt src/game/events.py to v2 against these files.
- Backlog item verified: player.py random_events uv block already
  implements policy #3 (per-financed-year accrual 5000-10000, no
  graduation lump sum, expulsion keeps debt) — no leftover lump sum;
  covered by StudentLoanPolicyTest.

## Addendum — schema v2.1 auto events + succession helpers

- event-schema.json updated to FROZEN v2.1 (Claude decision): auto:true
  (no choices, applied immediately, journaled, exempt from 2-choice/
  soft-lock), sign on money_pct, partner.compatibility branches field_ref.
  lightning_strike and thanatophobia_worry converted to auto events
  (outcomes at event level; lightning weights 5/4/1). Content tests
  updated; 9 green.
- Succession helpers (parity row 77): eligible_successors(player) and
  succession_instructions(player, child, rng) for A's graph conversion —
  balanced cash transfer (share = max(1, round_stochastic(money/num)) via
  ledger transfer to the successor; residual recorded as estate_close on
  the deceased; share + residual == money exactly), generation+1,
  carried-over job/university state (legacy convert semantics), carried
  partner, sibling list excluding the successor, preserve list
  (person_id/appearance/name). +4 tests on real Players (eligibility,
  balance legs, zero-money, carryover). 135 rule tests green.

## Addendum — loader v2.1 + event pending resolution

- src/game/events.py (C-owned now): full v2.1 support with unchanged public
  signatures (load_event_files/EventEngine/GameContext/instantiate).
  Auto events via new instantiate_auto; conditions incl. sampled gates;
  flattening _resolve_effects with int/string signs, money_pct, sampled
  refs, role resolution; structural effects isolated in _structural.
- rule_actions.resolve_choice now resolves loader event pendings (kind =
  stable event id): flat effects, money via ledger entries, structural
  handling (incl. kidnapping -> funeral chain, divorce, gain_partner),
  terminal -> year_close. Old saved pendings (kind 'vaccination_age2')
  verified resolving (regression test).
- Scenario-start 400 fixed and verified end to end: test_game_scenarios
  17/17, loader suite 11, rule suites 146, appearance 51 — all green.

## Addendum — D engine-gap investigation

- (a) find_partner after divorce: root cause in rule_actions (action never
  projected in action_specs though dispatchable). Fixed; verified through
  GameService (married_30 -> divorce -> find_partner offered -> date_offer
  resolves); regression test FindPartnerAfterDivorceTest.
- (b) pregnancy -> baby naming: NOT reproducible through GameService —
  family_events birth path produces the baby_naming pending with correct
  journal (verified by direct drive). Likely D-side: pregnant_25 arrives
  not pregnant (factory consumed it historically) or try_for_baby refusal
  gates. Design note recorded: an earlier phase's pending delays the birth
  naming by one year (one pending per turn, by design).

## M3 groundwork (contract/data only, DRAFTS — no engine code, M2 untouched)

- docs/collab/M3/drafts/skills-contract.md: full C skills contract proposal —
  state envelope (env['skills'], per-person values/used_this_turn/history,
  successor & legacy-backfill policy), declarative training actions with a
  bounded rule vocabulary (cost/gain/limits/learning_rate), progression curve
  (bounded annual gains, soft-lock free actions, saturation refusals), frozen
  requirement queries for A/B gating (skills never grant promotions or
  credentials), annual contribution hook + ordering trap statement, event
  records on the frozen M2c schema with two proposed typed extensions
  (min_skill condition, add_skill effect), command/projection shapes.
- docs/collab/M3/drafts/skills.json: the four baseline skills (practical,
  organization, communication, care), range 0-100, deterministic defaults.
- docs/collab/M3/drafts/training_actions.json: evening_class ($150, 5-9),
  self_study (free, 1-3), volunteer_shift (free, 16+, care/communication),
  contribution source form for A/B hooks.
- docs/collab/M3/drafts/skills-examples.json: worked examples (state,
  projection, train command + four refusal shapes, skill event with the
  proposed extensions, gating query result).
- Open questions for freeze (in the contract): Q1 envelope section naming,
  Q2 file layout, Q3 event extensions vs domain commands (A decides),
  Q4 diminishing returns (proposed: none at M3a), Q5 learning-rate numbers,
  Q6 legacy backfill baseline.

## Addendum — 30 original flavor events (content)

- src/game/content/events/flavor_{childhood,teen,adulthood,elder}.json:
  8/7/10/5 original events on the frozen v2.1 schema, existing
  condition/effect kinds only (min/max_age, alive, in_school, employed,
  min_money; stats/money/relationship effects with DeltaValue ranges).
  Every event: >=2 choices, >=1 free (soft-lock), honest known_effects,
  once_per_lifetime. No chance_n — they ride the weighted pool's ~1/3
  eligible-years gate, keeping overall frequency at roughly one event per
  three years.
- tests/test_rules_flavor_content.py (6 tests): loader acceptance, exact
  per-file counts, per-event age-window eligibility, known_effects honesty
  for every stat/money delta, text hygiene, and a 3-seed GameService smoke
  run (fires + frequency cap). The smoke test self-probes for A's pending
  auto-event coordinator diff and skips until it lands (it then runs for
  real); it caught the real production crash path (lightning 1/5000 ->
  instantiate raises) and the exact coordinator diff was sent to A.
- All content loads: 43 events total (13 legacy + 30 flavor). Suites:
  166 rule tests green (1 intentional skip pending A's diff).

## Addendum — 25 family & relationship flavor events

- src/game/content/events/flavor_family.json: 25 original events (romance:
  first-date nerves, anniversary dinner, in-law dinner, thermostat accord,
  kitchen dancing, forgotten anniversary; family: parent calls, school play,
  pet campaign, sibling boardgame grudge, road trip, grandparent story hour,
  burnt holiday dinner, reunion potato-salad judging, lost tooth, 3 a.m. baby,
  mom's birthday, inherited lamp, group photo, snowball truce, report card
  conference, teen calling home, the hard question, sibling moving help,
  grandkid sleepover). Adult-only romance: every romance event requires
  min_age >= 18 and every partner effect is gated on an alive partner
  (asserted by test). Existing schema kinds only; every event >=2 choices,
  >=1 free (the loader caught my in-law dinner violating soft-lock — fixed
  with a free 'relentless charm' option); known_effects honesty and text
  hygiene tests extended (counts now 8/7/10/5/25).
- Content total: 68 events (13 legacy + 30 flavor + 25 family). Suites:
  169 rule tests green (1 intentional skip pending A's auto-event diff).

## Addendum — 20 work & school flavor events

- src/game/content/events/flavor_work_school.json: 20 original events.
  School (in_school gate): group-project slacker, exam all-nighter, whiteboard
  meltdown, locker mix-up, ancient library fine, pop quiz, bake-sale wars,
  summer reading list. Work (employed gate): interview tie crisis, office
  birthday cake, promotion rumor, reply-all reckoning, working from home,
  side hustle, the meeting about the meeting, coworker lunch politics, the
  empty desk, weekend workshop, plant-sitting duty, the retirement speech.
  Existing kinds only; soft-lock caught the bake sale (fixed with a free
  volunteer option); honesty/gates tests extended (counts 8/7/10/5/25/20,
  teen->in_school and adult work->employed asserted).
- Content total: 88 events. Suites: 170 rule tests green (1 intentional
  skip — A's auto-event coordinator diff is NOT yet on disk; probed and
  re-reported to A).

## Addendum — journal text quality fixes

- Silent yearly relationship drift no longer journals filler ('Lin Wall
  relationship changed.'): deltas apply quietly; a real sentence is
  journaled only for significant swings (|delta| >= 5), e.g. 'Mum and
  Joe grew closer this year.' / '... drifted apart ...'.
- Illness names are lower-cased with an article inside sentences: 'I
  recovered from the common cold', 'no longer suffering from the common
  cold', 'continue to suffer from the flu' (illness_phase, doctor visit
  cure/continue branches, witch-doctor cure). Regression tests:
  test_rules_m2b.JournalTextQualityTest.

## Addendum — journal voice sweep (first person, relationship-aware)

- Every journal _event line in rule_actions is now first person ('I'm no
  longer suffering from the common cold', 'I got the job!', 'I've been
  diagnosed with cancer', 'I retired and am now receiving...'). UI prompt
  texts (pending 'text' fields) intentionally stay second person.
- Relationship drift lines are role-aware: 'My mother Lin and I grew closer
  this year.' / 'My partner Sam and I drifted apart this year.'
  (_relation_phrase; Parent/Partner/Child/Sibling roles, first name).
- Content: every outcome_text (the journal line) across all 88 events
  rewritten to first person; {name} renders as 'I' in the rule resolver so
  even old saved pendings journal first-person.
- Guard tests (JournalVoiceTest): no journal line starts with 'You ', no
  content outcome_text contains {name}; a rule battery with a uniquely-named
  player ('Zzyzx') asserts no event text contains the player's name, plus a
  role-aware drift assertion ('My partner ... and I drifted apart').
- 174 rule tests, 68 appearance+scenario tests green. Done for M2;
  awaiting HALT.

## M2 fix round (from M2-consolidated-review.md)

- #4 (lead C half, in events.py): choice effects are now DEEP-merged over
  event pre-effects — per-stat and money entries present in both become an
  ordered {"sum": [...]} sampled once, so the kidnapping 'Do nothing' offer
  keeps its event-level happiness penalty AND its karma effect (regression:
  test_rules_fix_round.Finding4PreEffectsTest, incl. same-stat sum bounds).
  instantiate_auto keeps terminal, merges event effects, and falls back to
  the event text when an outcome text is empty (thanatophobia keeps
  happiness+stress+health and a non-empty journal line).
- #5 (C half): chance_n events are evaluated SEPARATELY from the flavor
  pool via EventEngine.roll_independent/sample_independent in declared
  LEGACY_CHANCE_ORDER (lightning, breakup, divorce, kidnapping, dating,
  thanatophobia, vaccination); sample()/sample_flavor is flavor-only with
  the 1/3 pool gate, so independent events both fire and flavor can never
  dilute their rates (regression: Finding5IndependentChancesTest forces
  two 1-in-2 gates to pass and asserts both fire).
- #3 (C lead): event-driven partner death returns a typed instruction
  result['deaths'] = [{'person_id', 'cause': 'presumed dead',
  'announcement'}] consumed by A's single graph transition; the stored
  outcome is authoritative (never resampled); the funeral decision chains
  as the resolve's pending (regression: Finding3DeathInstructionTest).
- #2 (C half): family_events_phase accepts snapshot_ids (frozen turn
  snapshot) and skips non-members, so newborns get no second yearly update
  and each child slice runs once for snapshot members only (regression:
  Finding2SnapshotFilterTest).
- Minor: {person:role} tokens resolve to real names (incl. archived) via
  GameContext.person_name; schema syntax never reaches the player
  (regression: MinorPersonTokenTest).
- Interfaces sent to A (roll_independent/sample_flavor names match its
  coordinator; deaths instruction; snapshot param). 181 rule tests green.
  A's 4 romance-scenario + retirement errors are the factory seed issue:
  terminal lightning now actually kills (previously dropped), so seeded
  drives hit the 1/5000 and the 'life ended early' guard trips — A is
  retuning seeds / making the factory death-tolerant.

## Self-check (00-SELF-CHECK.md) — M2 fix round

- [x] Real objects: GameService/Partner/Player/EventEngine fixtures;
  nothing behavior-mocked (only gate-forcing RNG scripts).
- [x] Each fix has a failing-before regression: each fix-round test fails
  on the pre-fix code (shallow merge dropped pre-effects; combined pool
  returned one winner; no deaths instruction; no snapshot filter).
- [x] Randomness: all draws via passed rng; LEGACY_CHANCE_ORDER documented.
- [x] Typed errors on invalid inputs unchanged.
- [x] Money: kidnapping ransom/pre-effects still single-posting; no new
  mutation paths.
- [x] Identity: deaths instruction carries canonical person_id.
- [x] No new persisted fields.
- [x] Costs/eligibility: unchanged, pre-sampled.
- [x] Gates server-side; no soft-locks (flavor pool keeps a free option).
- [x] Only owned files (events.py now C-owned per 00-TEAM; rule_actions;
  tests). No probability changes beyond the Codex-mandated split.
- [x] 181 rule tests pass; hash-seed stable (seeded per-trial).
- [x] UI n/a.

## M2 fix round — final addendum (A interface match)

- relation_year primitive live (full legacy relation year minus child
  slices); family_events skips non-snapshot children via ctx.turn_snapshot.
- Pre-effects served once at offer creation: pending["pre_effects"] +
  "_pre_person_ids_extra"; A's serve applier sets pending["pre_applied"];
  the rule resolver re-applies only when the marker is absent (no double
  apply). Options carry choice-only effects.
- roll_independent/sample_flavor split with LEGACY_CHANCE_ORDER; chance
  events can never enter the flavor pool.
- karma/stress top-level lift in _resolve_effects (A's probe green).
- Deaths instruction: result["deaths"] = [{person_id, cause, announcement}].
- 183 rule tests + 17 scenario tests green; ready for Codex re-review.
