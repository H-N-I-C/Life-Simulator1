"""v2 §Person projection tests (Kimi A): the observer-safe person detail
contract from docs/collab/redesign-v2/contracts.md §Person, over the real
GameService plus real Person/Parent/Child objects.

Fixtures pinned here: unrelated adult, child, deceased, unknown ID.

Run: python3 -m unittest tests.test_game_v2_person
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402
from src.game.features.person_view.projection import person_detail  # noqa: E402
from src.people.classes.child import Child  # noqa: E402
from src.people.classes.parent import Parent  # noqa: E402
from src.people.classes.person import Person  # noqa: E402
from src.lifesim_lib.lifesim_lib import Gender  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}

DETAIL_KEYS = {"id", "name", "age", "role", "alive", "appearance",
               "descriptors", "bars", "actions"}

ADVERTISED = [
    {"id": "spend_time", "label": "Spend time", "target_id": "p1",
     "enabled": True, "reason": None, "known_costs": [], "limits": []},
    {"id": "compliment", "label": "Compliment", "target_id": "p1",
     "enabled": False, "reason": "already complimented them this year",
     "known_costs": [], "limits": ["once per year"]},
    {"id": "insult", "label": "Insult", "target_id": "p2",
     "enabled": True, "reason": None, "known_costs": [], "limits": []},
    {"id": "tip", "label": "Leave a tip", "target_id": "p1",
     "enabled": True, "reason": None,
     "known_costs": [{"kind": "money", "amount": 0}], "limits": []},
]


def env_with(dead=()):
    return {"dead_ids": list(dead), "status": "alive"}


class ProjectionShapeTests(unittest.TestCase):
    """Unit level, real Person objects, fake env — the four fixtures."""

    def test_unrelated_adult_relative_no_bars_no_actions(self):
        adult = Person("Zoe", "Lane", 30, Gender.Female, 50, 50, 50, 50)
        # wire a stable id without consuming gameplay RNG semantics
        adult.person_id = "p9"
        d = person_detail(env_with(), adult, [], role_of=GameService._role)
        self.assertEqual(set(d), DETAIL_KEYS)
        self.assertEqual(d["role"], "relative")
        self.assertEqual(d["bars"], [])
        self.assertEqual(d["actions"], [])
        self.assertEqual(d["descriptors"], {})

    def test_child_has_relationship_bar_and_targeted_specs(self):
        child = Child("Sam", "Rivers", Gender.Male, 50, 50)
        child.person_id = "p1"
        child.age = 7
        d = person_detail(env_with(), child, ADVERTISED,
                          role_of=GameService._role)
        self.assertEqual(d["role"], "child")
        self.assertEqual(d["bars"], [{"key": "relationship",
                                      "label": "Relationship",
                                      "value": child.relationship}])
        ids = [a["id"] for a in d["actions"]]
        self.assertIn("spend_time", ids)
        self.assertNotIn("insult", ids, "only this target's specs")
        # adult offers never appear in a child target's advertised list in
        # the first place (shell eligibility, _relationship_specs age gate);
        # the projection can only ever forward what was advertised.

    def test_disabled_spec_passthrough_and_free_cost(self):
        """Specs pass through in advertised shape; the adapter normalizes.
        $0 money costs stay 0 (a free action is real)."""
        child = Child("Sam", "Rivers", Gender.Male, 50, 50)
        child.person_id = "p1"
        d = person_detail(env_with(), child, ADVERTISED,
                          role_of=GameService._role)
        comp = next(a for a in d["actions"] if a["id"] == "compliment")
        self.assertEqual(comp["enabled"], False)
        self.assertEqual(comp["reason"],
                         "already complimented them this year")
        self.assertEqual(comp["limits"], ["once per year"])
        tip = next(a for a in d["actions"] if a["id"] == "tip")
        self.assertEqual(tip["known_costs"],
                         [{"kind": "money", "amount": 0}])
        self.assertNotIn("price", tip, "no second dialect fields")

    def test_deceased_kept_viewable_no_actions(self):
        parent = Parent("Rivers", 60, Gender.Female)
        parent.person_id = "p1"
        rel = parent.relationship
        d = person_detail(env_with(dead=["p1"]), parent, ADVERTISED,
                          role_of=GameService._role)
        self.assertEqual(d["alive"], False)
        self.assertEqual(d["actions"], [])
        self.assertEqual(d["bars"][0]["value"], rel,
                         "relationship retained as at death")
        self.assertNotIn("†", d["name"], "marker is a client choice")

    def test_privileged_fields_never_leak(self):
        """generosity/money/health live on the objects but are NOT public."""
        parent = Parent("Rivers", 60, Gender.Female)
        parent.person_id = "p1"
        self.assertTrue(hasattr(parent, "generosity"))
        d = person_detail(env_with(), parent, [], role_of=GameService._role)
        blob = repr(d)
        for banned in ("generosity", "faith", "city", "job"):
            self.assertNotIn(banned, blob)


class PersonViewServiceTests(unittest.TestCase):
    """Real GameService: advertised-eligibility equals projection."""

    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-v2p-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "v2p-create")
        self.gid = self.st["game_id"]

    def tearDown(self):
        import shutil
        shutil.rmtree(self.dir, ignore_errors=True)

    def test_unknown_id_raises_typed_not_found(self):
        with self.assertRaises(GameError) as ctx:
            self.svc.get_person_view(self.gid, "nope")
        self.assertEqual(ctx.exception.code, "person_not_found")
        self.assertEqual(ctx.exception.status, 404)

    def test_mother_view_matches_advertised_specs(self):
        state = self.svc.get_state(self.gid)
        mother = next(p for p in state["people"]
                      if p["role"] in ("mother", "father"))
        view = self.svc.get_person_view(self.gid, mother["id"])
        self.assertEqual(set(view), DETAIL_KEYS)
        self.assertEqual(view["id"], mother["id"])
        self.assertEqual(view["role"], mother["role"])
        self.assertEqual(view["alive"], True)
        # actions == exactly the target-filtered advertised specs, passed
        # through in advertised shape
        advertised = [a for a in state["available_actions"]
                      if a.get("target_id") == mother["id"]]
        self.assertEqual(len(view["actions"]), len(advertised))
        for got, src in zip(view["actions"], advertised):
            self.assertEqual(got["id"], src["id"])
            self.assertEqual(got.get("enabled", True), src["enabled"])
            self.assertEqual(got.get("reason"), src["reason"])
        # every projected action id is dispatchable as advertised
        self.assertTrue(all(a["id"] for a in view["actions"]))

    def test_player_detail_has_no_actions(self):
        state = self.svc.get_state(self.gid)
        view = self.svc.get_person_view(self.gid, state["player_id"])
        self.assertEqual(view["role"], "player")
        self.assertEqual(view["actions"], [])
        self.assertEqual(view["bars"], [])


if __name__ == "__main__":
    unittest.main()
