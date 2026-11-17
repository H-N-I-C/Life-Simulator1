# Kimi C — Q1-C1 report: rules & content hardening (2026-11-17)

Task: audit every rule_actions entry point and every content event for
unvalidated inputs, missing gates, impossible states and division/None
hazards; add property-style tests; fix what's in my files.

## Bugs found and fixed (all in C-owned files)

1. **CRASH: `NameError` in `_resolve_event_pending`** —
   `src/menus/rule_actions.py`. When a pending with event `pre_effects`
   reached the resolver unapplied (legacy/saved pendings), the code used
   `person_ids` and `targets` BEFORE they were assigned (the old lines
   2505-2523 ran ahead of the assignments at 2531-2541). Any event whose
   pre-effects targeted a relation (e.g. `relationship: random_relation`
   roles) crashed the turn with a raw 500. Fixed by hoisting the
   person/target resolution above the pre-effects block. Regression test:
   `EventContentPropertyTest.test_pre_effects_with_extra_person_ids_resolve`
   (forced, synthetic event; FAILS without the fix — verified via stash).

2. **Negative inheritance from a broke parent** —
   `sample_relation_death`. `avg = 100000 * (money/100)^2` is 0 for a
   parent with $0 while `lo = max(avg*generosity/200, 1) = 1`, so
   `_randexpo` drew from a negative-rate exponential → a NEGATIVE
   "inheritance" ledger entry (or a ZeroDivisionError when avg == lo).
   Now `avg = max(avg, lo + 1)` and the inheritance is clamped ≥ 1.
   Regression test: `InheritanceHazardTest` (parent money=0, generosity
   100, 50 seeds; FAILS without the fix).

3. **Hardened `resolve_adoption_offer`** — `pending["sampled"]["cost"]`
   was a raw KeyError on a malformed/crafted pending; now validated with a
   typed `_check` (amount must be a non-negative number).

4. **Hardened `_resolve_event_pending`** — missing/empty `event_id` now
   raises typed `invalid_input` instead of `KeyError` mid-apply.

5. **Loader validation: outcome weights** —
   `src/game/events.py validate_event` now rejects `weight < 1` /
   non-int weights in auto/choice/branch outcomes. `_pick_outcome` does
   `rng.randrange(sum(weights))`, so a zero total would crash the sampler;
   current content is clean, this closes the content-authoring door.

## Audit results (no change needed)

- Every action in the dispatch tables re-checks its gate server-side
  (school/job/age/affordability): study/skip/drop_out check `is_in_school`,
  work actions check `has_job`, retire checks `can_retire`, wedding/doctor/
  witch doctor/plastic surgery/adoption/lottery all check
  `player.money >= cost` before emitting the ledger entry. Shown costs in
  `action_specs` match the enforced amounts.
- Rules never mutate `player.money` — all cash moves via ledger entries;
  verified by grep and asserted per-call in the property test.
- Content JSON (13 files, 6,741 lines): scripted audit found NO bad
  `money_pct` ranges, NO positive-sign percent-of-money gains (the
  in-debt inversion hazard), NO `randint` lo>hi, NO branch y<=0, NO
  outcome weight hazards. The loader's soft-lock rule (every decision has
  a free option) is enforced at validation time.

## New property-style tests — `tests/test_rules_property.py` (6 tests)

- `PropertyHardeningTest`: 40 seeded random Players (random ages, stats,
  jobs, school, debt, negative money, partners, parents, siblings,
  children) driven through EVERY action id in every dispatch table, EVERY
  phase, with random payloads + garbage targets/inputs, then every
  returned pending resolved per-option plus bogus options. Asserts: only
  typed `RuleError` escapes; stats stay 0..100; money untouched by rules;
  every ledger entry is an int with a unique (phase, occurrence, leg)
  key; events reference canonical `person_id` strings.
- `EventContentPropertyTest`: every content event instantiated and every
  option resolved against random real-player contexts; `sample`/
  `sample_independent` across ages 0..99 with random states; plus the
  forced pre_effects regression.
- `InheritanceHazardTest`: forced broke-parent inheritance over 50 seeds.

## Issues outside my files (for Claude to route)

- **A (service/coordinator)**: `Player.learn_trait` in shared
  `src/people/classes/player.py:170-181` prints trait banners
  ("You have gained the Book Lover trait!") to stdout via
  `print_colored`. Headless rules (study_harder, read_book) call it, so
  web play spams the server log. Ownership of `learn_trait` is ambiguous
  (not in the C/A split list); please route — likely A wraps or C
  suppresses behind a flag.
- **A**: `_resolve_event_pending` can return `deaths` for partner_death
  and a `_funeral_pending`; coordinator should consume the typed death
  exactly once (already documented) — the property test only exercises the
  rule side; A1's mid-command exception tests should cover the
  coordinator side.

## Focused test runs

- `tests/test_rules_property.py`: 6/6 OK.
- All `test_rules_*.py` (189 tests, includes loader/content/oracle/m2a/m2b
  suites): OK under PYTHONHASHSEED 0, 1 and 42.
- Each fix has a regression test that FAILS without the fix (verified by
  `git stash` of the two source files: both error, both pass after pop).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Tests use REAL objects (Player/Partner/Parent/Sibling/Child,
  real EventEngine + real content files); no mocks.
- [x] Each fix has a failing-without-fix test (stash-verified above).
- [x] Randomness: all draws use the passed-in seeded rng; results
  reproducible per seed; no set/dict iteration before RNG use (dispatch
  lists are sorted; property tests pass under 3 hash seeds).
- [x] Inputs validated: bogus action/phase/pending/option/target/input
  shapes give typed RuleError, never KeyError/500 (asserted in tests).
- [x] Money: no rule mutates money; entries are int, unique-keyed; the
  inheritance fix keeps the ledger direction sane.
- [x] Identity: events/deaths/money entries use person_id strings only.
- [x] Save/load: no new persisted fields (pending pre_effects shape
  unchanged; validation only tightens).
- [x] Shown costs match enforced costs (wedding/doctor/witch/surgery/
  adoption/lottery checked in audit).
- [x] Age/state gates enforced server-side in the rule bodies (audited);
  soft-lock loader rule intact (weight validation added next to it).
- [x] Regression tests FORCE the rare path (synthetic pre-effects event;
  money=0 parent over 50 seeds), never a random roll.

Hygiene
- [x] Only C-owned files edited: src/menus/rule_actions.py,
  src/game/events.py, tests/test_rules_property.py, my 00-STATUS.md row.
  (00-PROCESS.md branch guard and web/service changes are other agents'.)
- [x] No probability or balance changes: the inheritance change only
  clamps an impossible negative draw; no rates touched.
- [x] Focused tests pass under 3 PYTHONHASHSEED values (0/1/42).
- [x] UI item: n/a (no UI changes).
- [x] Rare paths forced, not rolled (see above).

---

# Addendum 1 (2026-11-17): robot-player crashes + advertised-vs-accepted agreement

## Crash fix (crash_id 62a309a1e51b, 100% deterministic)
- `resolve_argument` read `fx["happiness"]` but `sample_conversation`'s
  "insult" outcome stores no happiness key -> KeyError -> 500. Now tolerant
  `.get` access (missing key = zero delta). Forced regression test:
  `ResolverKeyAccessTest.test_argument_insult_option_without_happiness_key`
  (verified: 8 error without the fix, 0 with).

## Resolver sweep (same unsafe-key class)
- `resolve_funeral_choice`: `.get` for happiness/karma (same bug shape).
- `resolve_book_choice`: unknown book title -> typed `invalid_input`;
  effect keys read with `.get`.
- `resolve_plastic_surgery`: crafted `surgeon:99`/`surgeon:abc` -> typed
  error (was IndexError/ValueError).
- `resolve_doctor_visit`: cured-branch gains read with `.get`.
- `resolve_university_funding` / `_application`: missing stored options/
  outcomes -> typed errors.
- `resolve_pregnancy`: missing stored outcome -> typed error.
- job_offer / `resolve_retirement_choice` / `resolve_lottery`: missing
  sampled structure -> typed errors.
- `Player.calc_pension` (C-owned): empty `salary_years` crashed
  `statistics.mean` -> returns 0 (found by the property fuzz).

## Advertised-vs-accepted agreement (~690 robot findings)
Rule (Claude): if action_specs says enabled, dispatch must accept, and
vice versa. Fixed mismatches in `src/menus/rule_actions.py`:
- doctor/witch_doctor/plastic_surgery/adoption advertised during school
  while `_OFFER_ACTIONS` refused them -> specs now mirror the school gate.
- Paid actions not gated on affordability -> sample-time gates
  (plastic_surgery $25,000; witch_doctor min fee $50; adoption min
  $10,000) + per-option `enabled`/`reason` in the sampled pendings
  (lottery buy_one/buy_ten, witch take, surgery surgeons, wedding venues,
  adoption, doctor treatments). Resolve keeps its authoritative re-check.
- lottery action stays available while broke (frozen M1 behavior: walk
  away free); only its paid options gate.
- Missing parity gates added: conversation/compliment/insult require
  relation age >= 5 (legacy main.py outer gate); ask_for_money requires
  player age >= 6; read_book requires age >= 12; break_up advertised for
  engaged too (status < 2, legacy); try_for_baby hidden while pregnant.
- gym spec disables on health < 10 (rule gate was invisible in the UI).
- Partner lifecycle actions refused while enrolled in school
  (`_SCHOOL_BLOCKED_ACTIONS`); relationship upkeep stays available per
  the service's M1-frozen projection.
- Fixed an introduced bug where the wedding venue option id lost its
  f-prefix (`venue:{v}` literal, caught by the fuzz).

## New tests
- `SpecDispatchAgreementTest`: 60 seeded random states; direction 1 —
  every enabled spec is accepted by dispatch (soft "refused" gameplay
  outcomes excepted); direction 2 — everything dispatch accepts matches an
  enabled spec. Specs re-read fresh across mutations.
- `PendingOptionAffordabilityTest`: broke players see disabled paid
  options with reasons; a free option always remains (no soft-lock);
  resolve re-checks after money drops.
- `ResolverKeyAccessTest`: 8 forced crash-shape regressions.

## Runs
- test_rules_*: 205 tests OK under PYTHONHASHSEED 0/1/42.
- test_game_* (A's suites): 127 OK (verifies service compatibility).
- Stash check: ResolverKeyAccessTest 8 errors without the fixes, all pass
  with them.
