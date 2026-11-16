"""Parity-oracle tests for legacy annual events.

Two layers:
1. EXACT branch tests (pinned RNG): deterministic outcomes and effect
   ranges checked exactly against tests/fixtures/annual_events_oracle.json.
2. STATISTICAL rate tests (seeded, many trials): occurrence probabilities
   and branch chances checked against the oracle's exact values with
   binomial tolerance bands, usable later against the M2c data-file
   implementation by pointing the same helpers at it.

Helpers are implementation-agnostic: `measure_rate(run_trial, trials,
seed0)` returns an observed rate; `assert_rate` compares to the oracle.
"""

import json
import math
import os
import random
import unittest
from unittest import mock

SLOW = os.environ.get("LIFESIM_SLOW_TESTS") == "1"


def ntrials(default, slow):
    """Smallest N that holds by default (wide bands); large-N versions run
    only behind LIFESIM_SLOW_TESTS=1 to keep the unit suite under 60s."""
    return slow if SLOW else default

import src.people.classes.player as player_mod
from src.lifesim_lib.lifesim_lib import Gender
from src.people.classes.partner import Partner
from tests.test_rules_income import make_player

FIXTURE = os.path.join(os.path.dirname(__file__), "fixtures",
                       "annual_events_oracle.json")
ORACLE = json.load(open(FIXTURE))["events"]
TOL = json.load(open(FIXTURE))["tolerances"]


def tolerance(p, trials):
    return max(4 * math.sqrt(p * (1 - p) / trials), TOL["min_abs"])


def measure_rate(run_trial, trials=None, seed0=1000):
    if trials is None:
        trials = ntrials(200, 2000)
    hits = 0
    for t in range(trials):
        if run_trial(seed0 + t):
            hits += 1
    return hits / trials


def assert_rate(testcase, observed, p, trials):
    testcase.assertAlmostEqual(observed, p, delta=tolerance(p, trials),
                               msg=f"rate {observed:.4f} vs expected {p} "
                                   f"over {trials} trials")


def run_legacy_events(p, patches, seed):
    """Run one legacy random_events with global random seeded and the
    usual I/O pinned; `patches` are extra mock.patch entries."""
    random.seed(seed)
    base = [
        mock.patch.object(player_mod, "choice_input", return_value=1),
        mock.patch("builtins.input", return_value=""),
        mock.patch.object(player_mod, "display_event",
                          side_effect=lambda *a, **k: None),
        mock.patch("builtins.print", side_effect=lambda *a, **k: None),
        mock.patch.object(player_mod, "clear_screen",
                          side_effect=lambda *a, **k: None),
    ]
    for patcher in patches:
        base.append(patcher)
    stack = mock.ExitStack()
    try:
        for patcher in base:
            stack.enter_context(patcher)
        p.random_events()
    finally:
        stack.close()


class OracleStructureTest(unittest.TestCase):
    def test_fixture_covers_all_annual_events(self):
        expected = {"lightning", "thanatophobia_worry", "vaccination",
                    "partner_breakup_offer", "partner_divorce_offer",
                    "partner_kidnapping", "dating_offer"}
        self.assertEqual(set(ORACLE), expected)
        for event in ORACLE.values():
            self.assertIn("source", event)
            self.assertIn("occurrence", event)


class ExactBranchTest(unittest.TestCase):
    """Deterministic branches pinned exactly."""

    def test_lightning_good_branch_maxes_stats(self):
        p = make_player(age=30, happiness=50, health=50, smarts=50,
                        looks=50)
        forced = lambda x: x == 5000 or x == 2  # occur + good branch
        with mock.patch.object(player_mod, "one_in", side_effect=forced), \
             mock.patch.object(player_mod, "press_enter",
                               side_effect=lambda *a: None):
            p.random_events()
        self.assertEqual((p.happiness, p.health, p.smarts, p.looks),
                         (100, 100, 100, 100))

    def test_vaccination_bite_effects_in_oracle_range(self):
        spec = ORACLE["vaccination"]["choices"][2]
        h_lo, h_hi = spec["effects"]["happiness"]["randint"]
        r_lo, r_hi = spec["effects"]["mother_relationship"]["randint"]
        seen = set()
        for seed in range(200):
            p = make_player(age=2)
            mother = p.parents["Mother"]
            rel0 = mother.relationship
            h0 = p.happiness
            random.seed(seed)
            with mock.patch.object(player_mod, "one_in",
                                   side_effect=lambda x: x == 2), \
                 mock.patch.object(player_mod, "choice_input",
                                   return_value=3), \
                 mock.patch("builtins.print",
                            side_effect=lambda *a, **k: None):
                p.random_events()
            seen.add((h0 - p.happiness, rel0 - mother.relationship))
            self.assertTrue(h_lo <= h0 - p.happiness <= h_hi)
            self.assertTrue(r_lo <= rel0 - mother.relationship <= r_hi)
        self.assertGreaterEqual(len(seen), (h_hi - h_lo + 1) // 2)

    def test_divorce_honor_settlement_within_money_pct(self):
        lo, hi = ORACLE["partner_divorce_offer"]["choices"][0][
            "effects"]["divorce_settlement"]["money_pct"]
        p = make_player(age=50, money=100000)
        rel = Partner(45, Gender.Female, 50, 50, 10, 2)
        p.partner = rel
        p.relations.append(rel)
        forced = lambda x: x == 6  # divorce offer fires
        with mock.patch.object(player_mod, "one_in", side_effect=forced), \
             mock.patch.object(player_mod, "choice_input",
                               return_value=1), \
             mock.patch.object(player_mod, "display_event",
                               side_effect=lambda *a, **k: None), \
             mock.patch("builtins.print", side_effect=lambda *a, **k: None), \
             mock.patch("builtins.input", return_value=""):
            p.random_events()
        self.assertIsNone(p.partner)
        paid = 100000 - p.money
        self.assertTrue(lo * 100000 - 1 <= paid <= hi * 100000 + 1)
        self.assertEqual(round(paid), paid)

    def test_kidnapping_ransom_within_money_pct_when_paying(self):
        lo, hi = ORACLE["partner_kidnapping"]["sampled_at_offer"][
            "ransom"]["money_pct"]
        p = make_player(age=40, money=2000000, karma=0)
        rel = Partner(40, Gender.Female, 50, 50, 90, 2)
        p.partner = rel
        p.relations.append(rel)
        forced = lambda x: x == 250
        with mock.patch.object(player_mod, "one_in", side_effect=forced), \
             mock.patch.object(player_mod, "x_in_y",
                               side_effect=lambda x, y: False), \
             mock.patch.object(player_mod, "choice_input",
                               return_value=1), \
             mock.patch.object(player_mod, "display_event",
                               side_effect=lambda *a, **k: None), \
             mock.patch("builtins.print", side_effect=lambda *a, **k: None), \
             mock.patch("builtins.input", return_value=""):
            p.random_events()
        paid = 2000000 - p.money
        self.assertTrue(lo * 2000000 - 1 <= paid <= hi * 2000000 + 1)


class StatisticalRateTest(unittest.TestCase):
    """Occurrence/branch rates vs oracle exact values (seeded trials)."""

    def _occurrence(self, builder, marker, gate_patch, trials=600):
        def trial(seed):
            p = builder()
            seen = []
            with mock.patch.object(player_mod, "display_event",
                                   side_effect=lambda m, *a, **k:
                                   seen.append(str(m))):
                run_legacy_events(p, [gate_patch()], seed)
            return any(marker in m for m in seen)
        rate = measure_rate(trial, trials)
        return rate

    def test_lightning_good_bad_5050(self):
        p_spec = ORACLE["lightning"]["branches"]
        good = 0
        trials = ntrials(120, 4000)

        def trial(seed):
            p = make_player(age=30)

            def forced(x):
                if x == 5000:
                    return True  # lightning occurs
                if x == 2:
                    return random.randint(1, 2) == 1  # real good/bad draw
                return False
            with mock.patch.object(player_mod, "one_in",
                                   side_effect=forced), \
                 mock.patch.object(player_mod, "press_enter",
                                   side_effect=lambda *a: None), \
                 mock.patch.object(player_mod.Player, "display_stats",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch.object(player_mod, "clear_screen",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch.object(player_mod.Player, "die",
                                   side_effect=lambda *a, **k: None):
                random.seed(seed)
                p.random_events()
            return p.happiness == 100
        rate = measure_rate(trial, trials)
        assert_rate(self, rate, p_spec[0]["chance"], trials)

    def test_vaccination_occurrence_half(self):
        expected = 1 / ORACLE["vaccination"]["occurrence"]["n"]
        trials = ntrials(80, 6000)

        def trial(seed):
            p = make_player(age=2)
            seen = []
            with mock.patch.object(player_mod, "choice_input",
                                   return_value=1), \
                 mock.patch.object(player_mod, "press_enter",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch.object(player_mod.Player, "die",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch.object(player_mod, "display_event",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch("builtins.print",
                            side_effect=lambda m, *a, **k: seen.append(
                                str(m))):
                random.seed(seed)
                p.random_events()
            return any("vaccinated" in m or "doctor" in m for m in seen)
        assert_rate(self, measure_rate(trial, trials), expected, trials)

    def test_breakup_occurrence_third_gated(self):
        expected = 1 / ORACLE["partner_breakup_offer"]["occurrence"]["n"]
        trials = ntrials(100, 8000)

        def trial(seed):
            p = make_player(age=30, partner=None)
            rel = Partner(30, Gender.Female, 50, 50, 0, 0)
            p.partner = rel
            p.relations.append(rel)
            seen = []
            with mock.patch.object(player_mod, "choice_input",
                                   return_value=1), \
                 mock.patch.object(player_mod, "press_enter",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch.object(player_mod.Player, "die",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch.object(player_mod, "display_event",
                                   side_effect=lambda *a, **k: None), \
                 mock.patch("builtins.print",
                            side_effect=lambda m, *a, **k: seen.append(
                                str(m))):
                random.seed(seed)
                p.random_events()
            return any("break up" in m for m in seen)
        assert_rate(self, measure_rate(trial, trials), expected, trials)

    def test_two_draw_willpower_insist_rate(self):
        """Two independent draws, short-circuit (M1 policy): at p=0.5 the
        insist probability is 1-(1-0.5)^2 = 0.75."""
        rng = random.Random(7)
        trials = ntrials(600, 20000)
        insists = 0
        for _ in range(trials):
            d1 = rng.uniform(0, 100) < 50
            d2 = False if d1 else rng.uniform(0, 100) < 50
            if d1 or d2:
                insists += 1
        assert_rate(self, insists / trials, 0.75, trials)


if __name__ == "__main__":
    unittest.main()
