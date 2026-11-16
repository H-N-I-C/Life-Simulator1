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
