"""Dev scenario starts: real-transition setup, determinism, typed errors."""

import tempfile
import unittest

from src.game.service import GameError, GameService


def make_service():
    return GameService(tempfile.mkdtemp(prefix="lifesim-scen-test-"))


PROFILE = {"first_name": "Dev", "last_name": "Tester", "gender": "female",
           "country": "Australia", "seed": 777}


class ScenarioTests(unittest.TestCase):
    def _player(self, st):
        return next(p for p in st["people"] if p["id"] == st["player_id"])

    def test_high_school_first_day(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="high_school_first_day"),
                          "r1")
        self.assertEqual(self._player(st)["age"], 14)
        self.assertEqual(st["education"]["stage"], "high")
        self.assertTrue(st["education"]["enrolled"])
        self.assertIsNotNone(st["education"]["grades"])
        # ordinary game afterwards: can age up
        st = svc.command(st["game_id"], st["revision"], "a1", "age_up", {})
        self.assertEqual(self._player(st)["age"], 15)

    def test_graduating_18(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="graduating_18"), "r1")
        self.assertEqual(self._player(st)["age"], 18)
        self.assertEqual(st["education"]["stage"], "none")
        self.assertFalse(st["education"]["enrolled"])

    def test_employed_30(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="employed_30"), "r1")
        self.assertEqual(self._player(st)["age"], 30)
        self.assertTrue(st["work"]["employed"])
        self.assertGreater(st["work"]["salary"], 0)
        self.assertGreater(st["work"]["years_worked"], 0)
        # income history posted through the ledger
        self.assertTrue(st["finances"]["ledger"])

    def test_near_retirement_64(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="near_retirement_64"),
                          "r1")
        self.assertEqual(self._player(st)["age"], 64)
        self.assertTrue(st["work"]["employed"])
        self.assertGreaterEqual(st["work"]["years_worked"], 10)
        st = svc.command(st["game_id"], st["revision"], "a1", "age_up", {})
        self.assertTrue(st["work"]["can_retire"])

    def test_same_seed_same_scenario_state(self):
        svc = make_service()
        s1 = svc.new_game(dict(PROFILE, scenario="employed_30"), "r1")
        s2 = svc.new_game(dict(PROFILE, scenario="employed_30"), "r2")
        self.assertEqual(s1["stats"], s2["stats"])
        self.assertEqual(s1["work"], s2["work"])
        self.assertEqual(
            [{k: v for k, v in p.items() if k != "id"}
             for p in s1["people"]],
            [{k: v for k, v in p.items() if k != "id"}
             for p in s2["people"]])

    def test_scenario_creation_replay_returns_scenario_state(self):
        svc = make_service()
        profile = dict(PROFILE, scenario="graduating_18")
        s1 = svc.new_game(profile, "r1")
        s2 = svc.new_game(profile, "r1")
        self.assertEqual(s2["game_id"], s1["game_id"])
        self.assertEqual(self._player(s2)["age"], 18)
        svc2 = GameService(svc.save_dir)
        s3 = svc2.new_game(profile, "r1")
        self.assertEqual(self._player(s3)["age"], 18)

    def test_unknown_scenario_typed_error(self):
        svc = make_service()
        with self.assertRaises(GameError) as ctx:
            svc.new_game(dict(PROFILE, scenario="time_traveler"), "r1")
        self.assertEqual(ctx.exception.code, "invalid_input")
        self.assertEqual(ctx.exception.status, 400)
        self.assertEqual(svc.list_games()["games"], [])


if __name__ == "__main__":
    unittest.main()



class DManifestScenarioTests(unittest.TestCase):
    """Scenarios requested in docs/collab/evidence/M2/scenario-manifest.md."""

    def _player(self, st):
        return next(p for p in st["people"] if p["id"] == st["player_id"])

    def test_enrolled_child_10(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="enrolled_child_10"), "r1")
        self.assertEqual(self._player(st)["age"], 10)
        self.assertTrue(st["education"]["enrolled"])
        self.assertIsNotNone(st["education"]["grades"])
        actions = {a["id"]: a for a in st["available_actions"]}
        self.assertTrue(actions["study_harder"]["enabled"])

    def test_unemployed_18(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="unemployed_18"), "r1")
        self.assertEqual(self._player(st)["age"], 18)
        self.assertFalse(st["work"]["employed"])
        actions = {a["id"]: a for a in st["available_actions"]}
        self.assertIn("find_job", actions)

    def test_retirement_eligible_65(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="retirement_eligible_65"),
                          "r1")
        self.assertEqual(self._player(st)["age"], 65)
        self.assertTrue(st["work"]["employed"])
        self.assertGreaterEqual(st["work"]["years_worked"], 10)
        self.assertTrue(st["work"]["can_retire"])
        self.assertTrue(st["work"]["salary"] > 0)
        # salary history feeds the pension calculation
        self.assertTrue(st["finances"]["ledger"])

    def test_university_pending_17(self):
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="university_pending_17"),
            "r1")
        self.assertEqual(self._player(st)["age"], 17)
        d = st["pending_decision"]
        self.assertIn(d["kind"],
                      ("university_application", "university_funding"))
        # resolving the application carries the funding offer (the S2 fix)
        st2 = svc.command(st["game_id"], st["revision"], "x1",
                          "resolve_decision",
                          {"decision_id": d["id"],
                           "option_id": d["options"][0]["id"]})
        self.assertIsNotNone(st2["pending_decision"])
        self.assertEqual(st2["pending_decision"]["kind"],
                         "university_funding")

    def test_ill_with_cash(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="ill_with_cash"), "r1")
        self.assertEqual(self._player(st)["age"], 30)
        self.assertEqual(sorted(st["health"]["illnesses"]),
                         ["Common Cold", "Flu"])
        self.assertGreater(st["finances"]["balance"], 0)
        # illness persisted across restart
        st2 = GameService(svc.save_dir).get_state(st["game_id"])
        self.assertEqual(sorted(st2["health"]["illnesses"]),
                         ["Common Cold", "Flu"])


class LowPerformanceHookTests(unittest.TestCase):
    def test_low_performance_30_dev_hook(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="low_performance_30"),
                          "r1")
        self.assertGreaterEqual(
            next(p for p in st["people"]
                 if p["id"] == st["player_id"])["age"], 30)
        self.assertTrue(st["work"]["employed"])
        self.assertLess(st["work"]["performance"], 15)
        # still an ordinary, playable game
        rev = st["revision"]
        st = svc.command(st["game_id"], rev, "a1", "age_up", {})
        self.assertEqual(st["revision"], rev + 1)


class RomanceScenarioTests(unittest.TestCase):
    def test_married_30(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="married_30"), "r1")
        self.assertTrue(st["family"]["partner"])
        self.assertEqual(st["family"]["partner_status"], 2)
        self.assertGreaterEqual(
            next(p for p in st["people"]
                 if p["id"] == st["player_id"])["age"], 30)
        self.assertTrue(st["work"]["employed"])

    def test_married_with_child(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="married_with_child"), "r1")
        self.assertEqual(st["family"]["partner_status"], 2)
        self.assertEqual(len(st["family"]["children"]), 1)
        child_id = st["family"]["children"][0]
        child = next(p for p in st["people"] if p["id"] == child_id)
        self.assertTrue(child["name"].startswith("Robin"), child["name"])
        player_age = next(p for p in st["people"]
                          if p["id"] == st["player_id"])["age"]
        self.assertTrue(0 <= child["age"] < player_age)
        self.assertIsNotNone(child["appearance"])
        # canonical identity: child has its own person_id, resolvable
        self.assertIn(child_id, {p["id"] for p in st["people"]})

    def test_pregnant_25(self):
        # yearly turns cannot freeze the in-progress window: the scenario
        # guarantees married + (expecting or newborn)
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="pregnant_25"), "r1")
        self.assertEqual(st["family"]["partner_status"], 2)
        env = svc._load(st["game_id"])
        expecting = env["player"].is_pregnant or (
            env["player"].partner and env["player"].partner.is_pregnant)
        self.assertTrue(expecting or len(st["family"]["children"]) > 0)

    def test_new_parent_25(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="new_parent_25"), "r1")
        self.assertEqual(len(st["family"]["children"]), 1)
        age = next(p for p in st["people"]
                   if p["id"] == st["player_id"])["age"]
        self.assertLessEqual(age, 32)
        child = next(p for p in st["people"]
                     if p["id"] == st["family"]["children"][0])
        self.assertTrue(child["name"].startswith("Robin"), child["name"])


class WealthyScenarioTests(unittest.TestCase):
    def test_wealthy_married_scenario(self):
        # row 69: $1M+ and karma below the legacy kidnapping gate
        # (NOT x_in_y(karma, 130) — lower karma fires more often)
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="wealthy_married_40"), "w1")
        self.assertEqual(st["family"]["partner_status"], 2)
        self.assertGreaterEqual(st["money"], 1_000_000)
        self.assertLess(svc._load(st["game_id"])["player"].karma, 130)
        self.assertGreaterEqual(
            next(p for p in st["people"]
                 if p["id"] == st["player_id"])["age"], 40)
        # still an ordinary game
        rev = st["revision"]
        st = svc.command(st["game_id"], rev, "w2", "age_up", {})
        self.assertEqual(st["revision"], rev + 1)
