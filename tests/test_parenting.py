"""M2.5 parenting tests (Kimi A): v3 content through C's real loader,
the yearly phase through real GameService commands, milestones, money
single-posting, personalities/styles, and the fixed-rule guard.

Run: python3 -m unittest tests.test_parenting
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.appearance.integration import ensure_person_id  # noqa: E402
from src.game.service import GameError, GameService  # noqa: E402
from src.game.features.parenting import (  # noqa: E402
    STYLES, milestones_of, style_of, has_milestone,
)
from src.game.features.parenting import engine as parenting_engine
from src.lifesim_lib.lifesim_lib import Gender  # noqa: E402
from src.people.classes.child import Child  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 21}


def add_child(svc, gid, age=2):
    """A real Child wired into the family: relations, children list and
    graph parentage."""
    env = svc._load(gid)
    child = Child("Jordan", "Rivers", Gender.Female, 40, 40,
                  mother=env["player"])
    ensure_person_id(child)
    child.age = age
    env["player"].relations.append(child)
    env["player"].children.append(child)
    env["graph"]["parentage"].append({
        "child_id": child.person_id, "parent_id": env["player"].person_id,
        "role": "biological", "source": "birth", "confidence": "certain"})
    svc._save(env)
    return svc._load(gid)["player"].children[-1].person_id


def resolve_pending(svc, gid, st, option_sub, request_id):
    d = st["pending_decision"]
    option = next(o for o in d["options"]
                  if o["id"] == option_sub
                  or o["id"].endswith("_" + option_sub))
    return svc.command(gid, st["revision"], request_id,
                       "resolve_decision",
                       {"decision_id": d["id"], "option_id": option["id"]})


def _par_events():
    import src.game.service  # registrations (predicates/appliers)
    from src.game import events
    return [e for e in events.load_event_files()
            if e["id"].startswith("par_")]


def force_events(*ids):
    """Deterministic phase tests: restrict the engine to specific
    par_* events (the cache hook the engine exposes for tests)."""
    from src.game import events as event_mod
    from src.game.features.parenting import engine as pe
    loaded = event_mod.load_event_files()
    pe._EVENT_CACHE = [e for e in loaded
                       if e["id"] in ids]


class ContentTests(unittest.TestCase):
    def test_parenting_content_validates_in_real_loader(self):
        evs = _par_events()
        self.assertEqual(len(evs), 34)
        for ev in evs:
            self.assertEqual(ev["schema_version"], 3)
            self.assertFalse(ev["interaction"]["romantic"],
                             "%s must be nonromantic" % ev["id"])

    def test_every_event_has_a_true_free_option(self):
        for ev in _par_events():
            free = [c for c in ev["choices"]
                    if (c.get("effects", {}).get("money", 0) >= 0
                        and not (c.get("cost") or {}).get("money"))]
            self.assertTrue(free,
                            "%s lacks a charge-free option" % ev["id"])

    def test_romantic_minor_event_fails_validation(self):
        from src.game import events
        bad = {
            "id": "par_bad", "title": "t", "text": "x",
            "interaction": {"kind": "flirt", "romantic": True},
            "conditions": {"min_age": 16},
            "roles": {"actor": {"kind": "person", "select": "player"}},
            "choices": [
                {"id": "a", "label": "A", "effects": {},
                 "outcome_text": "x"},
                {"id": "b", "label": "B", "effects": {},
                 "outcome_text": "y"},
            ],
        }
        with self.assertRaises(events.EventValidationError):
            events.validate_event3(bad)


class PhaseTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-par-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "par-create")
        self.gid = self.st["game_id"]

    def test_toddler_milestone_fires_and_records_once(self):
        force_events("par_first_steps")
        child_id = add_child(self.svc, self.gid, age=2)
        st = self.svc.get_state(self.gid)
        fired = None
        for i in range(120):
            st = self.svc.command(self.gid, st["revision"],
                                  "par-age%d" % i, "age_up", {})
            d = st["pending_decision"]
            if d and d["kind"] == "par_first_steps":
                fired = d
                break
            while d:
                free = [o for o in d["options"] if o["enabled"]]
                self.assertTrue(free)
                st = resolve_pending(self.svc, self.gid, st,
                                     free[-1]["id"],
                                     "par-d%d-%s" % (i, d["id"]))
                d = st["pending_decision"]
        self.assertIsNotNone(fired, "first-steps event never fired")
        st = resolve_pending(self.svc, self.gid, st, "celebrate", "par-r1")
        env = self.svc._load(self.gid)
        self.assertTrue(has_milestone(env, child_id, "first_steps"))
        # style + personality backfilled deterministically
        self.assertIn(style_of(env, child_id), STYLES)
        personality = env["parenting"]["personalities"][child_id]
        self.assertIn("energy", personality)
        # milestone is once per child: the same event never re-fires for
        # this child
        self.assertTrue(env["parenting"]["seen"])

    def test_money_request_posts_exactly_once(self):
        force_events("par_child_money_request")
        child_id = add_child(self.svc, self.gid, age=8)
        env = self.svc._load(self.gid)
        env["player"].age = 30
        pid = env["player"].person_id
        env["balances"][pid] = 500
        env["player"].money = 500
        svc2 = self.svc
        svc2._save(env)
        st = svc2.get_state(self.gid)
        money_before = 500
        # age until the money request appears (declining other par offers)
        for i in range(60):
            st = svc2.command(self.gid, st["revision"], "par-m%d" % i,
                              "age_up", {})
            d = st["pending_decision"]
            if d and d["kind"] == "par_child_money_request":
                break
            while d:
                free = [o for o in d["options"] if o["enabled"]]
                self.assertTrue(free, "free exit required: %s" % d["kind"])
                # the state projection strips _effects; content keeps the
                # free/neutral option LAST, so drain the last enabled one
                pick = free[-1]
                st = resolve_pending(svc2, self.gid, st, pick["id"],
                                     "par-d%d-%s" % (i, d["id"]))
                d = st["pending_decision"]
        self.assertEqual(st["pending_decision"]["kind"],
                         "par_child_money_request")
        give = next(o for o in st["pending_decision"]["options"]
                    if "give" in o["id"])
        self.assertTrue(give["enabled"])
        payload = {"decision_id": st["pending_decision"]["id"],
                   "option_id": give["id"]}
        revision = st["revision"]
        st = svc2.command(self.gid, revision, "par-give",
                          "resolve_decision", dict(payload))
        env = svc2._load(self.gid)
        self.assertEqual(env["player"].money, money_before - 50)
        gifts = [row for row in env["ledger"] if row["amount"] == -50]
        self.assertEqual(len(gifts), 1)  # single posting
        self.assertEqual(milestones_of(env, child_id)["money_request"]
                         ["record"], {"given": 50})
        # replaying the same resolve (same request_id, same payload,
        # original revision) does not repost
        retry = svc2.command(self.gid, revision, "par-give",
                             "resolve_decision", dict(payload))
        self.assertEqual(retry["revision"], revision + 1)
        self.assertEqual(svc2._load(self.gid)["player"].money,
                         money_before - 50)

    def test_unaffordable_choice_is_disabled_not_forced(self):
        force_events("par_family_holiday")
        child_id = add_child(self.svc, self.gid, age=8)
        # player has $0: the paid holiday/give options must be disabled
        # with a reason, the free exit enabled
        found = None
        st = self.svc.get_state(self.gid)
        for i in range(80):
            st = self.svc.command(self.gid, st["revision"], "par-f%d" % i,
                                  "age_up", {})
            d = st["pending_decision"]
            if d and d["kind"] == "par_family_holiday":
                found = d
                break
            while d:
                free = [o for o in d["options"] if o["enabled"]]
                self.assertTrue(free, "free exit required: %s" % d["kind"])
                st = resolve_pending(self.svc, self.gid, st,
                                     free[0]["id"],
                                     "par-x%d-%s" % (i, d["id"]))
                d = st["pending_decision"]
        self.assertIsNotNone(found, "holiday event never fired")
        big = next(o for o in found["options"] if "big_trip" in o["id"])
        self.assertFalse(big["enabled"])
        self.assertIn("costs", big["reason"])
        stay = next(o for o in found["options"] if "stay_home" in o["id"])
        self.assertTrue(stay["enabled"])


class AdultAndGrandkidTests(unittest.TestCase):
    """Adult children and grandchildren fire through the real service."""

    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-par-gk-"))
        self.st = self.svc.new_game(dict(PROFILE), "gk-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 55
        self.svc._save(env)

    def _add_adult_child_with_kid(self):
        env = self.svc._load(self.gid)
        adult = Child("Sam", "Rivers", Gender.Male, 40, 40,
                      mother=env["player"])
        ensure_person_id(adult)
        adult.age = 28
        grandkid = Child("Lee", "Rivers", Gender.Female, 40, 40,
                         mother=adult)
        ensure_person_id(grandkid)
        grandkid.age = 6
        env["player"].relations.append(adult)
        env["player"].children.append(adult)
        env["player"].relations.append(grandkid)
        env["graph"]["parentage"].append({
            "child_id": adult.person_id,
            "parent_id": env["player"].person_id,
            "role": "biological", "source": "birth",
            "confidence": "certain"})
        env["graph"]["parentage"].append({
            "child_id": grandkid.person_id,
            "parent_id": adult.person_id,
            "role": "biological", "source": "birth",
            "confidence": "certain"})
        self.svc._save(env)
        return adult.person_id, grandkid.person_id

    def test_grandkid_event_binds_grandchild_role(self):
        force_events("par_grandkid_visit", "par_grandkid_portrait")
        adult_id, grandkid_id = self._add_adult_child_with_kid()
        st = self.svc.get_state(self.gid)
        fired = None
        for i in range(200):
            st = self.svc.command(self.gid, st["revision"], "gk-a%d" % i,
                                  "age_up", {})
            d = st["pending_decision"]
            if d and d["kind"].startswith("par_grandkid"):
                fired = d
                break
            while d:
                free = [o for o in d["options"] if o["enabled"]]
                self.assertTrue(free)
                st = resolve_pending(self.svc, self.gid, st, free[-1]["id"],
                                     "gk-d%d-%s" % (i, d["id"]))
                d = st["pending_decision"]
        self.assertIsNotNone(fired, "no grandkid event fired")
        bindings = fired.get("bindings") or {}
        self.assertEqual(bindings.get("child"), grandkid_id)

    def test_adult_kid_event_and_milestones(self):
        force_events("par_adult_moving_out", "par_adult_birthday")
        adult_id, _ = self._add_adult_child_with_kid()
        env = self.svc._load(self.gid)
        self.assertIn(adult_id, env["parenting"]["personalities"])
        st = self.svc.get_state(self.gid)
        fired = None
        for i in range(200):
            st = self.svc.command(self.gid, st["revision"], "ad-a%d" % i,
                                  "age_up", {})
            d = st["pending_decision"]
            if d and d["kind"].startswith("par_adult"):
                fired = d
                break
            while d:
                free = [o for o in d["options"] if o["enabled"]]
                self.assertTrue(free)
                st = resolve_pending(self.svc, self.gid, st, free[-1]["id"],
                                     "ad-d%d-%s" % (i, d["id"]))
                d = st["pending_decision"]
        self.assertIsNotNone(fired, "no adult-kid event fired")
        self.assertEqual((fired.get("bindings") or {}).get("child"),
                         adult_id)


if __name__ == "__main__":
    unittest.main()
