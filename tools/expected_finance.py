"""Independent expected-value calculators for M2 finance QA.

These re-implement the documented money formulas (docs/collab/M2/02-contract.md
+ src/people/classes/player.py `_gross_and_tax` / `calc_pension` and
src/lifesim_lib/const.py tax brackets) WITHOUT calling the implementation
helpers, so browser/accounting assertions do not merely re-run the code
under test. When the service returns a quote/ledger projection, tests
compare against these values and fail on mismatch.

Run standalone for a self-check of the quote identity:
    python3 tools/expected_finance.py
"""

# U.S.-style progressive brackets copied from src/lifesim_lib/const.py.
SALARY_TAX_BRACKETS = [
    (9950, 0.10), (40525, 0.12), (86375, 0.22), (164925, 0.24),
    (209425, 0.32), (523600, 0.35),
]
TOP_RATE = 0.37


def calculate_tax(gross):
    """Bracket tax on yearly gross (float, matches legacy helper)."""
    tax = 0.0
    prev = 0
    for bound, perc in SALARY_TAX_BRACKETS:
        if gross <= bound:
            tax += (gross - prev) * perc
            return tax
        tax += (bound - prev) * perc
        prev = bound
    return tax + (gross - prev) * TOP_RATE


def expected_gross_and_tax(salary, has_job, job_hours):
    """Gross yearly pay and tax for an explicit employment state.

    Working accounts earn salary scaled by hours (overtime pays more);
    retired accounts (has_job False) draw their pension, independent of
    job_hours.
    """
    if salary <= 0:
        return 0.0, 0.0
    gross = salary * job_hours / 40.0 if has_job else float(salary)
    return gross, calculate_tax(gross)


def expected_income_quote(salary, has_job, job_hours, retention):
    """Deterministic integer quote for a sampled retention factor.

    `retention` is the fraction of after-tax income kept after living
    expenses (legacy U(0.4, 0.8), sampled once per account/turn by the
    service). Returns ints with the contract identity
    gross - tax - expenses == net (residual goes to expenses).
    """
    gross_f, tax = expected_gross_and_tax(salary, has_job, job_hours)
    if gross_f <= 0:
        return {"gross": 0, "tax": 0, "expenses": 0, "net": 0}
    gross = round(gross_f)
    net = round((gross_f - tax) * retention)
    return {"gross": gross, "tax": round(tax),
            "expenses": gross - round(tax) - net, "net": net}


def expected_pension(salary_years, years_worked):
    """Pension: mean of the top-5 salary years x min(years,35) x 2%."""
    if not salary_years:
        return 0
    top5 = sorted(salary_years, reverse=True)[:5]
    return round(sum(top5) / len(top5) * min(years_worked, 35) * 0.02)


def check_ledger_identity(entries, balance_before):
    """Verify the contract row shape and running balances.

    entries: iterable of {amount, balance_after} in posting order.
    Returns (ok, problems[]) — every row's balance_after must equal
    balance_before + amount, and amounts must be signed ints.
    """
    problems = []
    bal = balance_before
    for i, e in enumerate(entries):
        amount = e.get("amount")
        after = e.get("balance_after")
        if not isinstance(amount, int) or isinstance(amount, bool):
            problems.append("row %d: amount %r not int" % (i, amount))
            continue
        if not isinstance(after, int):
            problems.append("row %d: balance_after %r not int" % (i, after))
            continue
        bal += amount
        if after != bal:
            problems.append("row %d: balance_after %s != expected %s"
                            % (i, after, bal))
            bal = after
    return (not problems), problems


def check_cash_delta(entries, balance_before, balance_after):
    """Sum of signed ledger amounts must equal the balance movement."""
    delta = sum(e["amount"] for e in entries
                if isinstance(e.get("amount"), int))
    return delta == balance_after - balance_before, delta


if __name__ == "__main__":
    # Self-check: quote identity across a salary/hours grid, and a
    # known-value tax spot check (10% bracket edge).
    assert calculate_tax(9950) == 995.0
    assert calculate_tax(10000) == 995.0 + 50 * 0.12
    for salary in (20000, 41500, 90000, 175000, 300000, 550000):
        for hours in (38, 40, 55, 70):
            for retention in (0.4, 0.57, 0.8):
                q = expected_income_quote(salary, True, hours, retention)
                assert q["gross"] - q["tax"] - q["expenses"] == q["net"], q
                assert q["net"] <= q["gross"] - q["tax"]
    # Pension: known value — top5 mean 50000, 20 years -> 20000.
    assert expected_pension([50000] * 10, 20) == 20000
    assert expected_pension([], 30) == 0
    ok, problems = check_ledger_identity(
        [{"amount": 100, "balance_after": 100},
         {"amount": -30, "balance_after": 70}], 0)
    assert ok, problems
    ok, _ = check_ledger_identity(
        [{"amount": 100, "balance_after": 90}], 0)
    assert not ok
    print("expected_finance self-check OK")
