import random
import unittest
from unittest import mock

from src.people.classes.player import Player
from src.lifesim_lib.lifesim_lib import calculate_tax
import src.people.classes.player as player_mod


def make_player(**overrides):
    """A REAL constructed Player (canonical person_id, real stats)."""
    p = Player("Testfirst", "Testlast", player_mod.Gender.Male)
    p.age = 40
    p.happiness = 50
    p.health = 50
    p.smarts = 50
    p.looks = 50
    p.karma = 50
    p.money = 0
    p.salary = 0
    p.has_job = False
    p.job_hours = 0
    p.years_worked = 0
    p.uv_years = 0
    p.grades = None
    p.student_loan = 0
    p.chose_student_loan = False
    p.illnesses = []
    p.traits = {}
    p.partner = None
    p.relations = []
    p.children = []
    p.is_pregnant = False
    p.dropped_out = True
    for k, v in overrides.items():
        setattr(p, k, v)
    return p


def run_random_events(p, one_in=lambda x: False, randint=lambda a, b: 1,
                      uniform=0.6):
    """Drive Player.random_events with randomness pinned and input stubbed."""
    with mock.patch.object(player_mod, "one_in", side_effect=one_in), \
         mock.patch.object(player_mod, "randint", side_effect=randint), \
         mock.patch.object(random, "uniform", return_value=uniform), \
         mock.patch.object(player_mod, "round_stochastic",
                           side_effect=lambda v: v), \
         mock.patch.object(player_mod, "choice_input", return_value=1), \
         mock.patch("builtins.input", return_value=""):
        p.random_events()


class AnnualIncomeBreakdownTest(unittest.TestCase):
    def breakdown(self, **overrides):
        p = make_player(**overrides)
        with mock.patch.object(random, "uniform", return_value=0.5):
            return p.annual_income_breakdown()

    def test_full_time_worker_gross_equals_salary(self):
        gross, tax, expenses, net = self.breakdown(salary=40000, has_job=True, job_hours=40)
        self.assertEqual(gross, 40000)
        self.assertEqual(tax, calculate_tax(40000))
        self.assertAlmostEqual(net, (40000 - tax) * 0.5)

    def test_overtime_scales_gross_and_tax(self):
        # 50 hours = 1.25x gross; tax and net must be computed from that gross.
        gross, tax, expenses, net = self.breakdown(salary=40000, has_job=True, job_hours=50)
        self.assertEqual(gross, 50000)
        self.assertEqual(tax, calculate_tax(50000))
        self.assertAlmostEqual(net + expenses, gross - tax)

    def test_pension_not_zeroed_by_job_hours(self):
        # After retirement lose_job() sets job_hours = 0, but the pension in
        # salary must still be paid in full.
        gross, tax, expenses, net = self.breakdown(salary=30000, has_job=False, job_hours=0)
        self.assertEqual(gross, 30000)
        self.assertEqual(tax, calculate_tax(30000))
        self.assertGreater(net, 0)


class RandomEventsIncomeTest(unittest.TestCase):
    def test_pension_paid_every_year_after_retirement(self):
        p = make_player(age=66, salary=40000, has_job=False, job_hours=0)
        run_random_events(p, uniform=0.6)
        tax = calculate_tax(40000)
        self.assertAlmostEqual(p.money, (40000 - tax) * 0.6)

    def test_overtime_income_uses_adjusted_gross(self):
        p = make_player(age=40, salary=40000, has_job=True, job_hours=50)
        run_random_events(p, uniform=0.6)
        tax = calculate_tax(50000)
        self.assertAlmostEqual(p.money, (50000 - tax) * 0.6)


if __name__ == "__main__":
    unittest.main()


class AnnualIncomeQuoteTest(unittest.TestCase):
    """M2 frozen quote contract: deterministic, integer, leg identity."""
    @staticmethod
    def annual_income_quote(*a):
        from src.people.classes.player import annual_income_quote
        return annual_income_quote(*a)

    def test_identity_gross_minus_tax_minus_expenses_equals_net(self):
        for hours in (38, 40, 50, 70):
            for retention in (0.4, 0.6, 0.8):
                q = self.annual_income_quote(40000, True, hours, retention)
                self.assertEqual(
                    q["gross"] - q["tax"] - q["expenses"], q["net"])
                self.assertTrue(all(isinstance(v, int) for v in q.values()))

    def test_overtime_gross_and_tax(self):
        q = self.annual_income_quote(40000, True, 50, 0.6)
        self.assertEqual(q["gross"], 50000)
        self.assertEqual(q["tax"], calculate_tax(50000))

    def test_pension_independent_of_hours(self):
        for hours in (0, 40):
            q = self.annual_income_quote(30000, False, hours, 0.6)
            self.assertEqual(q["gross"], 30000)
            self.assertEqual(q["tax"], calculate_tax(30000))
            self.assertGreater(q["net"], 0)

    def test_zero_salary(self):
        self.assertEqual(self.annual_income_quote(0, True, 40, 0.6),
                         {"gross": 0, "tax": 0, "expenses": 0, "net": 0})

    def test_deterministic_no_sampling(self):
        # Two calls with the same factor must agree exactly (no RNG inside).
        a = self.annual_income_quote(45000, True, 45, 0.55)
        b = self.annual_income_quote(45000, True, 45, 0.55)
        self.assertEqual(a, b)

    def test_sampler_range(self):
        rng = random.Random(123)
        from src.people.classes.player import sample_expense_retention
        for _ in range(200):
            r = sample_expense_retention(rng)
            self.assertTrue(0.4 <= r <= 0.8)


class RetirementPensionFlowTest(unittest.TestCase):
    """Codex M1 review: retirement followed by actual annual payments."""

    def test_retire_then_two_pension_years(self):
        import src.menus.rule_actions as ra
        p = make_player(age=66, salary=50000, has_job=True, job_hours=40,
                        years_worked=20, stress=30, performance=60,
                        children=[], illnesses=[], salary_years=[50000] * 20)
        pension = p.calc_pension()
        ra.retire(p, random.Random(42))
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, pension)
        for year in ("t2", "t3"):
            with mock.patch.object(ra, "sample_expense_retention",
                                   return_value=0.6):
                r = ra.yearly_work_phase(p, year, random.Random(1))
            legs = {m["leg"]: m for m in r["money_entries"]}
            tax = calculate_tax(pension)
            self.assertEqual(legs["gross"]["amount"], pension)
            self.assertEqual(-legs["tax"]["amount"], tax)
            self.assertEqual(legs["gross"]["amount"]
                             + legs["tax"]["amount"]
                             + legs["expenses"]["amount"],
                             round((pension - tax) * 0.6))
