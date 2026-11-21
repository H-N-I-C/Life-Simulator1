"""v2 §Avatar defaults + §Choice costs tests (Kimi A, 2026-11-20).

Contracts: docs/collab/redesign-v2/contracts.md
- §Avatar defaults (F9 / Codex feel-eval #3): gender-fitting accessories
  (no bearded women, no hooded men), household variety, user edits kept.
- §Choice costs v2 (Codex feel-eval #2): every projected pending option
  carries known_costs (real money charge only, [] when free).

Run: python3 -m unittest tests.test_game_v2_avatar_defaults
"""

import os
import random
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.appearance.generate import random_appearance  # noqa: E402
from src.appearance.integration import ensure_appearance  # noqa: E402
from src.appearance.model import Appearance  # noqa: E402
from src.game.service import GameService  # noqa: E402
from src.lifesim_lib.lifesim_lib import Gender  # noqa: E402


class GenderFittingAccessoryTests(unittest.TestCase):
    def test_women_never_bearded(self):
        for seed in range(200):
            app = random_appearance(random.Random(seed), 27, Gender.Female)
            self.assertEqual(app.facial_hair, "none", f"seed {seed}")
            self.assertNotEqual(app.base, "man")

    def test_men_never_headscarf(self):
        for seed in range(200):
            app = random_appearance(random.Random(seed), 27, Gender.Male)
            self.assertEqual(app.headwear, "none", f"seed {seed}")
            self.assertNotEqual(app.base, "woman")

    def test_unknown_gender_no_accessories(self):
        for seed in range(100):
            app = random_appearance(random.Random(seed), 40, None)
            self.assertEqual(app.facial_hair, "none")
            self.assertEqual(app.headwear, "none")

    def test_children_have_no_accessories(self):
        for seed in range(100):
            for age in (0, 1, 2, 5, 12):
                app = random_appearance(
                    random.Random(seed), age,
                    Gender.Male if seed % 2 else Gender.Female)
                self.assertEqual(app.facial_hair, "none")
                self.assertEqual(app.headwear, "none")
                self.assertEqual(app.badges, [])

    def test_beards_possible_for_adult_men(self):
        beards = sum(
            random_appearance(random.Random(s), 35, Gender.Male).facial_hair
            == "beard" for s in range(400))
        self.assertGreater(beards, 0, "adult men should sometimes have beards")
        self.assertLess(beards, 200, "beard rate should stay modest")

    def test_deterministic_per_seed(self):
        a = random_appearance(random.Random(42), 25, Gender.Female)
        b = random_appearance(random.Random(42), 25, Gender.Female)
        self.assertEqual(a.to_dict(), b.to_dict())


class UserEditsPreservedTests(unittest.TestCase):
    def test_existing_appearance_never_rerolled(self):
        from src.people.classes.person import Person
        person = Person("Beitris", "Test", 27, Gender.Female, 50, 50,
                        50, 50)
        # a deliberate user edit: a bearded woman must survive ensure (the
        # gender rules constrain GENERATION only, never stored data)
        person.appearance = Appearance(seed=9, base="woman",
                                       facial_hair="beard",
                                       headwear="headscarf")
        before = person.appearance.to_dict()
        ensure_appearance(person)
        self.assertEqual(person.appearance.to_dict(), before)

    def test_missing_appearance_gets_gender_fitting_default(self):
        from src.people.classes.person import Person
        person = Person("Mara", "Test", 27, Gender.Female, 50, 50, 50, 50)
        person.appearance = None
        app = ensure_appearance(person)
        self.assertEqual(app.facial_hair, "none")


class HouseholdVarietyTests(unittest.TestCase):
    def test_new_game_parents_differ_and_fit_gender(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-v2-avatar-"))
        for seed in (7, 4242, 90210, 1, 2, 3):
            st = svc.new_game(
                {"first_name": "Kid", "last_name": "Test",
                 "gender": "female", "country": "Australia",
                 "seed": seed}, f"av-{seed}")
            people = {p["role"]: p for p in st["people"]}
            for role, gender in (("mother", Gender.Female),
                                 ("father", Gender.Male)):
                if role not in people:
                    continue
                app = people[role]["appearance"]
                self.assertIsNotNone(app, f"seed {seed}: {role} no avatar")
                self.assertEqual(app["base"],
                                 "woman" if gender == Gender.Female
                                 else "man",
                                 f"seed {seed}: {role} base")
                if gender == Gender.Female:
                    self.assertEqual(app["facial_hair"], "none",
                                     f"seed {seed}: {role} bearded")
                    self.assertIn(app["headwear"], ("none", "headscarf"))
                else:
                    self.assertIn(app["facial_hair"], ("none", "beard"))
                    self.assertEqual(app["headwear"], "none",
                                     f"seed {seed}: {role} headscarf")
            if "mother" in people and "father" in people:
                m = people["mother"]["appearance"]
                f = people["father"]["appearance"]
                self.assertNotEqual(
                    (m["base"], m["skin_tone"], m["hair"]),
                    (f["base"], f["skin_tone"], f["hair"]),
                    f"seed {seed}: parents look identical")


class PendingKnownCostsTests(unittest.TestCase):
    def test_projection_emits_known_costs(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-v2-costs-"))
        st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                           "gender": "female", "country": "Australia",
                           "seed": 7}, "c1")
        env = svc._load(st["game_id"])
        env["pending"] = {
            "id": "d_9", "title": "Offer", "text": "Pay or not.",
            "person_ids": [], "options": [
                {"id": "d_9_free", "label": "No thanks",
                 "known_effects": [], "_effects": {}, "_outcome": "ok"},
                {"id": "d_9_paid", "label": "Buy it",
                 "known_effects": [], "_effects": {"money": -120},
                 "_outcome": "bought"},
                {"id": "d_9_authored", "label": "Grand venue",
                 "known_costs": [{"label": "Grand", "amount": 5000}],
                 "_outcome": "married"},
            ]}
        st = svc._state(env)
        opts = {o["id"]: o for o in st["pending_decision"]["options"]}
        self.assertEqual(opts["d_9_free"]["known_costs"], [])
        self.assertEqual(opts["d_9_paid"]["known_costs"],
                         [{"kind": "money", "amount": 120}])
        # rule-authored costs are preserved verbatim (label+amount dialect)
        self.assertEqual(opts["d_9_authored"]["known_costs"],
                         [{"label": "Grand", "amount": 5000}])

    def test_real_pendings_carry_known_costs_key(self):
        # forced lifetime sweep: whatever pendings the engine serves, every
        # projected option exposes the known_costs list (never missing)
        from src.game.service import GameError
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-v2-costs-"))
        st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                           "gender": "female", "country": "Australia",
                           "seed": 7}, "c2")
        gid = st["game_id"]
        seen = 0
        for i in range(60):
            if st["status"] == "dead":
                break
            if st["pending_decision"]:
                d = st["pending_decision"]
                seen += 1
                for o in d["options"]:
                    self.assertIsInstance(o["known_costs"], list,
                                          f"option {o['id']} lacks "
                                          f"known_costs")
                opt = next(o for o in d["options"]
                           if o.get("enabled", True))
                try:
                    st = svc.command(gid, st["revision"], f"r{i}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": opt["id"]})
                except GameError as e:
                    if e.code != "not_allowed":
                        raise
            else:
                st = svc.command(gid, st["revision"], f"a{i}", "age_up", {})
        self.assertGreater(seen, 0, "expected at least one pending")


if __name__ == "__main__":
    unittest.main()
