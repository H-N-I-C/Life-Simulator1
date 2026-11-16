import random
import unittest
from unittest import mock

import src.people.classes.player as player_mod
from src.people.classes.player import Player
from src.people.classes.partner import Partner
from tests.test_rules_income import make_player, run_random_events


def make_married_player():
    partner = Partner(35, player_mod.Gender.Female, 50, 50, relationship=0, status=2)
    p = make_player(age=40, partner=partner, relations=[partner], money=100000)
    return p


def run_divorce(p, choice, one_in=lambda x: x == 6, uniform=0.0):
    with mock.patch.object(player_mod, "one_in", side_effect=one_in), \
         mock.patch.object(player_mod, "randint", side_effect=lambda a, b: 1), \
         mock.patch.object(random, "uniform", return_value=uniform), \
         mock.patch.object(player_mod, "choice_input", return_value=choice), \
         mock.patch("builtins.input", return_value=""):
        p.random_events()


def run_divorce_with_rolls(p, choice, rolls):
    """x_in_y driven by an explicit script so draw order/policy is visible."""
    with mock.patch.object(player_mod, "one_in", side_effect=lambda x: x == 6), \
         mock.patch.object(player_mod, "randint", side_effect=lambda a, b: 1), \
         mock.patch.object(random, "uniform", return_value=0.0), \
         mock.patch.object(player_mod, "x_in_y", side_effect=rolls), \
         mock.patch.object(player_mod, "choice_input", return_value=choice), \
         mock.patch("builtins.input", return_value=""):
        p.random_events()


class PartnerDivorceTest(unittest.TestCase):
    def test_honor_divorce_request(self):
        p = make_married_player()
        run_divorce(p, choice=1)
        self.assertIsNone(p.partner)

    def test_beg_and_partner_insists(self):
        # willpower check succeeds (uniform 0.0) -> divorce goes through.
        p = make_married_player()
        run_divorce(p, choice=2)
        self.assertIsNone(p.partner)

    def test_beg_and_partner_stays(self):
        # willpower check fails (huge uniform) -> marriage survives.
        p = make_married_player()
        run_divorce(p, choice=2, uniform=1000.0)
        self.assertIsNotNone(p.partner)
        self.assertIn(p.partner, p.relations)

    def test_two_draw_willpower_second_roll_wins(self):
        # Policy (Claude): keep the legacy two independent draws in order.
        # First draw fails, second succeeds -> partner insists -> divorce.
        p = make_married_player()
        run_divorce_with_rolls(p, choice=2, rolls=[False, True])
        self.assertIsNone(p.partner)

    def test_two_draw_willpower_both_fail_stays(self):
        p = make_married_player()
        run_divorce_with_rolls(p, choice=2, rolls=[False, False])
        self.assertIsNotNone(p.partner)

    def test_insult_branch(self):
        p = make_married_player()
        run_divorce(p, choice=3)
        self.assertIsNone(p.partner)


if __name__ == "__main__":
    unittest.main()
