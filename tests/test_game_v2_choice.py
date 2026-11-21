"""v2 §Choice/§Home support tests (Kimi A): the additive observer-safe
`display_category` field on pending_decision (contracts.md §Choice FROZEN v1)
and the §Home guarantee that Home needs no new server fields.

Run: python3 -m unittest tests.test_game_v2_choice
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameService  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}

# The frozen mapping table (contracts.md §Choice) — single source is
# GameService._DISPLAY_CATEGORIES; this mirror pins the contract.
EXPECTED = {
    "baby_naming": "family",
    "date_offer": "dating", "dating_offer": "dating",
    "wedding_plan": "weddings",
    "university_application": "education",
    "university_funding": "education",
    "retirement_offer": "work",
    # v3 flavour events and M1 generic pendings: no chip, never a guess
    "flavor_adulthood": None,
    "some_future_kind": None,
    None: None,
    "": None,
}


class DisplayCategoryMappingTests(unittest.TestCase):
    def test_mapping_matches_contract(self):
        for kind, expected in EXPECTED.items():
            self.assertEqual(GameService._display_category(kind), expected,
                             "kind %r" % (kind,))

    def test_mapping_values_are_from_the_frozen_vocab(self):
        # 'social' added per the M4 contract (social_invite ->
        # display_category "social"), Claude-approved
        vocab = {"family", "dating", "weddings", "education", "work",
                 "social"}
        for kind, cat in GameService._DISPLAY_CATEGORIES.items():
            self.assertIn(cat, vocab, "kind %r" % (kind,))
        # table never invents kinds: every key is a str
        for kind in GameService._DISPLAY_CATEGORIES:
            self.assertIsInstance(kind, str)


class PendingShapeTests(unittest.TestCase):
    """display_category is a pure function of the public kind: force the
    mapping on a live game's envelope and read it back through get_state."""

    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-v2c-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "v2c-create")
        self.gid = self.st["game_id"]

    def tearDown(self):
        import shutil
        shutil.rmtree(self.dir, ignore_errors=True)

    def _force_pending(self, kind):
        env = self.svc._load(self.gid)
        env["pending"] = {
            "id": "d_test", "kind": kind, "title": "T", "text": "X",
            "person_ids": [env["player"].person_id],
            "options": [{"id": "ok", "label": "OK",
                         "known_effects": []}],
        }
        self.svc._save(env)

    def test_pending_carries_display_category(self):
        for kind, expected in (("baby_naming", "family"),
                               ("wedding_plan", "weddings"),
                               ("flavor_adulthood", None),
                               (None, None)):
            self._force_pending(kind)
            state = self.svc.get_state(self.gid)
            pd = state["pending_decision"]
            self.assertIn("display_category", pd, "kind %r" % (kind,))
            self.assertEqual(pd["display_category"], expected,
                             "kind %r" % (kind,))
            # kind itself is unchanged and still public
            self.assertEqual(pd["kind"], kind)

    def test_no_pending_means_no_pending_decision(self):
        state = self.svc.get_state(self.gid)
        self.assertIsNone(state["pending_decision"])


class HomeFieldsTests(unittest.TestCase):
    """§Home: everything Home needs is already in _state — this pins the
    existing fields so nobody 'adds' a parallel dialect."""

    HOME_FIELDS = ("player_id", "people", "stats", "money", "country",
                   "journal", "work", "education", "available_actions")

    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-v2h-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "v2h-create")
        self.gid = self.st["game_id"]

    def tearDown(self):
        import shutil
        shutil.rmtree(self.dir, ignore_errors=True)

    def test_home_fields_present(self):
        state = self.svc.get_state(self.gid)
        for f in self.HOME_FIELDS:
            self.assertIn(f, state)
        player = next(p for p in state["people"]
                      if p["id"] == state["player_id"])
        self.assertIsInstance(player["age"], int)  # "This year" math input
        self.assertIn("job_label", state["work"])  # subtitle source
        for s in ("happiness", "health", "smarts", "looks"):
            self.assertIn(s, state["stats"])
        for e in state["journal"]:
            self.assertIn("age", e)  # age-relative timeline grouping


if __name__ == "__main__":
    unittest.main()
