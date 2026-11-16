import random
import unittest
from unittest import mock

import src.people.classes.child as child_mod
from src.people.classes.child import Child
from src.lifesim_lib.lifesim_lib import calculate_tax


def make_child(salary):
    # adopted=True makes finalize_appearance a no-op (identity code path).
    c = Child("Testchild", "Testlast", child_mod.Gender.Male, 50, 50, adopted=True)
    c.age = 20
    c.salary = salary
    c.money = 0
    c.years_worked = 0
    c.is_in_uv = 0
    c.student_loan = 0
    c.partner = None
    c.partner_desire = 0  # never generates a partner
    return c


class ChildIncomeTest(unittest.TestCase):
    def test_child_credits_net_income_not_full_salary(self):
        c = make_child(salary=40000)
        with mock.patch.object(child_mod, "randint", side_effect=lambda a, b: 1), \
             mock.patch.object(random, "uniform", return_value=0.5), \
             mock.patch.object(child_mod, "round_stochastic", side_effect=lambda v: v), \
             mock.patch.object(child_mod, "one_in", side_effect=lambda x: False), \
             mock.patch("builtins.input", return_value=""):
            c.age_up()
        expected_net = (40000 - calculate_tax(40000)) * 0.5
        self.assertAlmostEqual(c.money, expected_net)
        self.assertLess(c.money, 40000)


if __name__ == "__main__":
    unittest.main()
