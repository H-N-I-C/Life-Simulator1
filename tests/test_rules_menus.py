import io
import os
import random
import unittest
from contextlib import redirect_stdout
from unittest import mock

import src.menus.start as start_mod
import src.menus.activities as activities_mod
from src.lifesim_lib.lifesim_lib import int_input_range_optional
from src.people.classes.sibling import Sibling
from tests.test_rules_income import make_player


class IntInputRangeOptionalTest(unittest.TestCase):
    def test_blank_returns_none(self):
        with mock.patch("builtins.input", return_value=""):
            self.assertIsNone(int_input_range_optional(0, 100))

    def test_invalid_then_blank_returns_none(self):
        with mock.patch("builtins.input", side_effect=["abc", ""]):
            self.assertIsNone(int_input_range_optional(0, 100))

    def test_out_of_range_then_valid(self):
        with mock.patch("builtins.input", side_effect=["101", "50"]):
            self.assertEqual(int_input_range_optional(0, 100), 50)

    def test_valid_value(self):
        with mock.patch("builtins.input", return_value="42"):
            self.assertEqual(int_input_range_optional(0, 100), 42)


class CustomLifeFamilySetupTest(unittest.TestCase):
    def test_custom_life_gets_family_setup(self):
        # Inputs: 2=Custom Life, 1=Male, first name, last name, 1=Randomize traits.
        answers = iter(["2", "1", "Custom", "Person", "1"])
        buf = io.StringIO()
        with mock.patch.object(start_mod, "get_save_files", return_value=[]), \
             mock.patch.object(start_mod, "randint", side_effect=lambda a, b: 1), \
             mock.patch("builtins.input", side_effect=lambda *a: next(answers)), \
             redirect_stdout(buf):
            random.seed(7)
            player = start_mod.start_menu()
        self.assertEqual(player.firstname, "Custom")
        self.assertIn("Mother", player.parents)
        self.assertIn("Father", player.parents)
        # Parents must be announced for custom lives too (was skipped before).
        self.assertIn("Your mother is", buf.getvalue())
        self.assertIn("Your father is", buf.getvalue())
        # With randint pinned to 1 a sibling is generated.
        self.assertTrue(any(isinstance(r, Sibling) for r in player.relations))


class ActivitiesToyMenuTest(unittest.TestCase):
    def test_no_unreachable_toddler_branch(self):
        source = open(
            os.path.join(os.path.dirname(activities_mod.__file__), "activities.py")
        ).read()
        self.assertNotIn("player.age < 3", source)

    def test_play_with_toys_at_age_three(self):
        player = make_player(age=3)
        player.played = False
        buf = io.StringIO()
        # random.choice stays LIVE: the assertion is that the printed line
        # IS one of the toy sayings (present verbatim in activities.py),
        # not which one was picked.
        with mock.patch("builtins.input", return_value="2"), \
             mock.patch.object(random, "uniform", return_value=0.0), \
             redirect_stdout(buf):
            activities_mod.activities_menu(player)
        source = open(os.path.join(
            os.path.dirname(activities_mod.__file__),
            "activities.py")).read()
        lines = [l for l in buf.getvalue().splitlines() if l.strip()]
        saying = lines[-1]  # last output line is the picked saying
        self.assertIn(saying, source)
        self.assertTrue(player.played)


if __name__ == "__main__":
    unittest.main()
