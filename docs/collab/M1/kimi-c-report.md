# Kimi C report — M1 legacy gameplay-rule bugs

Focused tests: `python3 -m unittest tests.test_rules_income tests.test_rules_child_income tests.test_rules_divorce tests.test_rules_university tests.test_rules_menus` (20 tests, all pass).

## 1. Retirement/pension (bug + fix)

- Cause: retirement sets `self.salary = pension` after `lose_job()`, and `lose_job()` zeroes `job_hours`. Yearly income computed `gross = salary * job_hours / 40 = 0`, `tax = 0`, then credited `(salary - 0) * U(0.4, 0.8)` — an untaxed, randomly-shrunk pension.
- Fix: new helper `Player.annual_income_breakdown()` (`src/people/classes/player.py:676`) returns `(gross, tax, expenses, net)`. When `has_job` is False (retired) gross = the pension, unaffected by `job_hours`; when working, gross = `salary * job_hours / 40`. Tax and net are computed from that gross. `random_events` credits `round_stochastic(net)`.
- Test: `tests/test_rules_income.py` — `test_pension_not_zeroed_by_job_hours`, `test_pension_paid_every_year_after_retirement` (pension = `40000`, expected bank `(40000 - tax(40000)) * 0.6`; old code banked `40000 * 0.6`).

## 2. Overtime (bug + fix)

- Cause: work hours adjusted the taxable amount (`money = salary * hours / 40; tax = calculate_tax(money)`) but the credited income started from the unadjusted `salary`: `income = salary - tax`.
- Fix: same helper — net is `(gross - tax) * U(0.4, 0.8)` with `gross = salary * job_hours / 40`. At the default 40 h this is byte-identical to old behaviour; overtime/undertime now scales pay and tax consistently.
- Test: `test_overtime_scales_gross_and_tax`, `test_overtime_income_uses_adjusted_gross` (50 h on $40k → gross $50k, tax on $50k).

## 3. Child.age_up credited full salary (bug + fix)

- Cause: `src/people/classes/child.py` computed after-tax/expense `income` then did `self.money += round_stochastic(self.salary)`.
- Fix: credit `round_stochastic(income)` (child.py:168).
- Test: `tests/test_rules_child_income.py` — patched `random.uniform→0.5`, `round_stochastic→identity`; expected `money == (40000 - tax(40000)) * 0.5` (< salary; old code credited 40000).

## 4. Partner-initiated divorce (bug + fix)

- Cause: in `Player.random_events` the married branch assigned the response to `choices = choice_input(...)` then tested `choice` (stale/undefined) and referenced `self.parter` (typo) in branch 1 → NameError/AttributeError. Branch 2 was also inverted: partner insisting on divorce did nothing, partner staying called `self.divorce()`.
- Fix (`src/people/classes/player.py:999`): `choice = choice_input(...)`, `self.partner.name`, branch 2 now divorces when the partner insists and keeps the marriage (happiness hit only) when they stay; removed the duplicated `x_in_y(...) or x_in_y(...)`.
- Test: `tests/test_rules_divorce.py` — all four responses (honor / beg-insists / beg-stays / insult) with `choice_input` patched; asserts `partner is None` or still in `relations`.

## 5. University expulsion state (bug + fix)

- Cause: the expulsion branch in `random_events` announced expulsion but left `uv_years > 0` and `grades` set, so the "student" kept attending and later "graduated".
- Fix: on expulsion set `self.uv_years = 0` and `self.grades = None` (player.py:843), mirroring the dropout handling in `src/menus/main.py:527`.
- Test: `tests/test_rules_university.py` — expelled (grades 0), continuing (grades 100), graduating (last year) cases.

## 6. Menu/input fixes (bug + fix)

- `src/menus/start.py`: the Custom Life path returned before the shared family-setup block, so custom lives never got parents announced or a sibling. Now both paths fall through to the same block; `randomize_traits()` is skipped for custom lives (traits already chosen), `after_trait_select()` runs once. `custom_life` flag captured before the reused `choice` variable.
- `src/lifesim_lib/lifesim_lib.py` `int_input_range_optional`: `input()` can never return None, so blank input looped forever; now `""` returns `None` (all existing callers already handle None).
- `src/menus/activities.py`: removed the unreachable `player.age < 3` toy and arts-and-crafts saying lists (menus are gated at age ≥ 3 / ≥ 5), demoting the `elif player.age < 6` to `if`; wrapped the two unwrapped `"You played with your toys."` sayings in `_()`.
- Test: `tests/test_rules_menus.py` — custom-life run through `start_menu` asserts parents announced and a sibling generated; blank/invalid input cases; static check that no `age < 3` branch remains; toy menu playable at age 3.

## Notes for reviewers

- Money logic is centralized in `Player.annual_income_breakdown()` (returns gross/tax/expenses/net) so M2's finance ledger can reuse it.
- No game-balance changes beyond the bug fixes: at 40 h/wk the credited amount is identical to the old formula.
- One trap worth knowing: `random_events` uses `_` as the gettext alias, so tuple-unpacking into `_` inside that function breaks translation (caught by the university test).
- Identity/appearance code paths (`__init__`, `finalize_appearance`, `set_parent`, `convert_child_to_player`) untouched; tests construct players/children via `__new__` or `adopted=True` to avoid them.

## Addendum — Codex M1 review fixes (2026-11-16)

- **Divorce willpower probability:** restored the legacy two independent
  draws `x_in_y(willpower,100) or x_in_y(willpower,100)` (short-circuit
  order preserved) per Claude's decision; only the branch bug stays fixed.
  New tests `test_two_draw_willpower_second_roll_wins` ([False, True] mocked
  rolls → insists → divorce) and `test_two_draw_willpower_both_fail_stays`
  in tests/test_rules_divorce.py distinguish the policy from a single draw.
- **Income helper frozen for M2:** `annual_income_quote(salary, has_job,
  job_hours, retention)` — pure deterministic integer legs with
  `gross - tax - expenses == net` (residual to expenses);
  `sample_expense_retention(rng)` samples the factor once per account/turn.
  Terminal `annual_income_breakdown` keeps legacy float behavior (sampling on
  call) and is not used by the web path. Net is labeled after tax/living
  expenses, BEFORE debt service.
- **Student-loan policy:** tuition financed but not yet materialized as debt
  is not retroactively charged on dropout/expulsion; the stale
  `chose_student_loan` flag clears when no principal exists; existing debt
  survives and keeps being repaid. Implemented in player.py expulsion,
  main.py dropout and rule_actions (drop_out + education phase); tests in
  tests/test_rules_university.py (StudentLoanPolicyTest).
- Retirement → two annual pension payment years test added
  (tests/test_rules_income.py RetirementPensionFlowTest).
