"""Relationship-drift journal line tests (Kimi A, M3 amendment 5).

D robot finding: the yearly 'relationship improved/worsened' line
repeated every year. Now the drift entry is drawn through C's frozen
pick_year_line machinery (passed pool) with a dedicated no-repeat recents
window, cosmetic RNG only, undo-safe via the snapshot.

Run: TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_relationship_lines
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402

os.makedirs(os.path.expanduser("~/.lifesim-tmp"), exist_ok=True)


def make_service():
    return GameService(tempfile.mkdtemp(
        dir=os.path.expanduser("~/.lifesim-tmp")))


def new_game(svc, seed):
    return svc.new_game({"first_name": "Ada", "last_name": "Lee",
                         "gender": "female", "country": "Australia",
                         "seed": seed}, f"rl-{seed}")


def age_years(svc, gid, st, n, prefix):
    for i in range(n):
        guard = 0
        while st["pending_decision"] and guard < 20:
            d = st["pending_decision"]
            o = next(x for x in d["options"] if x.get("enabled", True))
            st = svc.command(gid, st["revision"], f"{prefix}-{i}-{guard}",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id": o["id"]})
            guard += 1
        if st["status"] == "dead":
            return st
        st = svc.command(gid, st["revision"], f"{prefix}-a{i}", "age_up", {})
    return st


class RelationshipLineTests(unittest.TestCase):
    def test_lines_vary_within_the_no_repeat_window(self):
        # under 18 parents drift +1 every year: across 10 years the
        # entries must NOT all read the same (the old bug)
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        st = age_years(svc, gid, st, 10, "v")
        if st["status"] == "dead":
            self.skipTest("died early")
        lines = [e["text"] for e in st["journal"]
                 if e["kind"] == "relationship"]
        # (a parent may die mid-run; the lines that DO appear must vary —
        # the old code emitted the identical sentence every year)
        self.assertGreaterEqual(len(lines), 4,
                                "expected yearly parent-drift lines")
        distinct = set(lines)
        self.assertGreaterEqual(len(distinct), 2,
                                f"drift lines repeat: {lines}")
        # every line carries the actual delta and a person tag
        for e in [e for e in st["journal"] if e["kind"] == "relationship"]:
            self.assertIn("relationship", e["changes"])
            self.assertTrue(e["person_ids"])

    def test_recents_window_persists_and_rolls_back_with_undo(self):
        svc = make_service()
        st = new_game(svc, 4242)
        gid = st["game_id"]
        st = age_years(svc, gid, st, 4, "u")
        if st["status"] == "dead":
            self.skipTest("died early")
        env = svc._load(gid)
        before = {k: list(v) for k, v in (
            env.get("relationship_line_recents") or {}).items()}
        st = age_years(svc, gid, st, 1, "u2")
        env = svc._load(gid)
        recents = env.get("relationship_line_recents") or {}
        # R7 shape: per-relation rolling windows {person_id: [last <= 3]}
        self.assertIsInstance(recents, dict)
        ids = [i for v in recents.values() for i in v]
        self.assertTrue(ids)
        self.assertEqual(len(ids), len(set(ids)))
        for v in recents.values():
            self.assertLessEqual(len(v), 3)
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        env = svc._load(gid)
        after = {k: list(v) for k, v in (
            env.get("relationship_line_recents") or {}).items()}
        # undo restores exactly the pre-age-up window with the snapshot
        self.assertEqual(after, before)

    def test_twin_games_stay_deterministic(self):
        # cosmetic-stream draws must not perturb the simulation stream
        texts = []
        for twin in (0, 1):
            svc = make_service()
            st = new_game(svc, 90210)
            gid = st["game_id"]
            st = age_years(svc, gid, st, 8, f"t{twin}")
            if st["status"] == "dead":
                self.skipTest("died early")
            texts.append([(e["kind"], e["text"]) for e in st["journal"]])
        self.assertEqual(texts[0], texts[1])


class RolePoolEngineTests(unittest.TestCase):
    """R7: C's relationship_flavour.lines_for(delta, role) wired into the
    drift hook — real role resolution, per-relation rolling three-pick
    window, generic-sentence fallback only on pool exhaustion."""

    def _relation(self, cls, gender=None):
        from src.lifesim_lib.lifesim_lib import Gender
        if cls == "parent":
            from src.people.classes.parent import Parent
            g = Gender.Male if gender == "male" else Gender.Female
            rel = Parent("Lee", 55, g)
        elif cls == "partner":
            from src.people.classes.partner import Partner
            g = Gender.Male if gender == "male" else Gender.Female
            rel = Partner(30, g, 50, 50, 80, "dating")
        elif cls == "sibling":
            from src.people.classes.sibling import Sibling
            g = Gender.Male if gender == "male" else Gender.Female
            rel = Sibling("Lee", 25, g, 50, 50)
        elif cls == "child":
            from src.people.classes.child import Child
            g = Gender.Male if gender == "male" else Gender.Female
            rel = Child("Sam", "Lee", g, 50, 50)
        else:
            raise AssertionError(cls)
        if not getattr(rel, "person_id", None):
            rel.person_id = f"pid_{cls}_{gender}"
        return rel

    def _player(self, age=30):
        from src.people.classes.player import Player
        p = Player("Ada", "Lee", "female")
        p.age = age
        return p

    def test_mother_and_father_roles_resolve(self):
        from src.game import coordinator
        self.assertEqual(coordinator._relation_role(
            self._relation("parent", "female")), "mother")
        self.assertEqual(coordinator._relation_role(
            self._relation("parent", "male")), "father")
        self.assertEqual(coordinator._relation_role(
            self._relation("partner")), "partner")
        self.assertEqual(coordinator._relation_role(
            self._relation("sibling")), "sibling")
        self.assertEqual(coordinator._relation_role(
            self._relation("child")), "child")
        # unknown relation classes fall back to the generic set, never None
        class Weird:
            person_id = "w"
            firstname = "X"
        self.assertEqual(coordinator._relation_role(Weird()), "sibling")

    def test_engine_draws_mother_lines_with_rolling_window(self):
        from src.game import coordinator
        env = {}
        player = self._player(30)
        mother = self._relation("parent", "female")
        texts = []
        for _ in range(6):
            texts.append(coordinator._relationship_drift_line(
                env, player, mother, +1))
        # mother-improved lines address Mum directly; the generic
        # sentence must NOT appear while the band pool has eligible lines
        for t in texts:
            self.assertIn("Mum", t)
        recents = env["relationship_line_recents"][mother.person_id]
        # rolling three-year window: at most the last 3 picks remembered
        self.assertLessEqual(len(recents), 3)
        # within one band the role pool (6 lines) outlasts the window:
        # the first pick rolled off and may lawfully recur
        self.assertGreaterEqual(len(set(texts)), 3)

    def test_exhaustion_restores_the_plain_sentence(self):
        from src.game import coordinator
        from src.game.content.journal import relationship_flavour
        from src.game.content.journal.yearly_lines import band_for_age
        env = {}
        player = self._player(30)
        mother = self._relation("parent", "female")
        band = band_for_age(30)
        pool = [l for l in relationship_flavour.lines_for(+1, "mother")
                if l["band"] == band]
        self.assertTrue(pool)
        env["relationship_line_recents"] = {
            mother.person_id: [l["id"] for l in pool]}
        line = coordinator._relationship_drift_line(env, player, mother, +1)
        self.assertEqual(
            line, f"My relationship with {mother.firstname} improved.")
        # no new pick was recorded on the fallback path
        self.assertEqual(env["relationship_line_recents"]
                         [mother.person_id],
                         [l["id"] for l in pool])

    def test_age_band_change_keeps_recent_sentences_excluded(self):
        import random
        from unittest.mock import patch
        from src.game import coordinator
        from src.appearance import integration
        env = {}
        player = self._player(4)
        mother = self._relation("parent", "female")
        with patch.object(integration, "_APP_RNG", random.Random(0)):
            previous = coordinator._relationship_drift_line(
                env, player, mother, +1)
            player.age = 5
            current = coordinator._relationship_drift_line(
                env, player, mother, +1)
        self.assertNotEqual(previous, current)

    def test_worsened_direction_uses_father_pool(self):
        from src.game import coordinator
        env = {}
        player = self._player(40)
        father = self._relation("parent", "male")
        line = coordinator._relationship_drift_line(env, player, father, -1)
        self.assertIn("Dad", line)

    def test_legacy_list_recents_shape_is_migrated_on_read(self):
        from src.game import coordinator
        env = {"relationship_line_recents": ["old_shape_id"]}
        player = self._player(30)
        mother = self._relation("parent", "female")
        line = coordinator._relationship_drift_line(env, player, mother, +1)
        self.assertIn("Mum", line)
        self.assertIsInstance(env["relationship_line_recents"], dict)
        self.assertNotIn("old_shape_id", env["relationship_line_recents"]
                         [mother.person_id])


if __name__ == "__main__":
    unittest.main()
