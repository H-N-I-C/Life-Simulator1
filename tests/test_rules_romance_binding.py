"""Romance binding guard (Kimi C): auto-served romance-tagged v3 events
must never bind close kin or a minor to a predicate-bound role.

Kinship is a modifier for choices the PLAYER makes (the explicit
dating/proposal actions, the editor); romance-tagged events the game
serves on its own (rom_ first dates, flirts, anniversaries, ...) bind
neither family (parents, children, siblings, grandparents,
grandchildren, aunts/uncles, nieces/nephews, first cousins — over ALL
parentage roles, biological/adoptive/step/guardian/social) nor minors.
The guard lives in the shared v3 binding filter (events.v3_bindable and
the matching branch of EventEngine.instantiate3), so every served and
instantiated romance-tagged event is covered, including dra_/soc_
content should any of it ever declare a romantic interaction.

Run: python3 -m unittest tests.test_rules_romance_binding
"""

import os
import random
import shutil
import sys
import tempfile
import unittest
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

os.environ.setdefault("TMPDIR", os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(os.environ["TMPDIR"], exist_ok=True)

from src.game import events as event_mod  # noqa: E402
from src.game.events import (  # noqa: E402
    EventEngine, GameContext, PREDICATES, v3_bindable)
from src.game.features.family_graph import new_graph  # noqa: E402
from src.game.service import GameService  # noqa: E402

ROMANTIC_PREDICATES = (
    "adult_non_related_stranger", "adult_acquaintance",
    "adult_existing_lover", "adult_ex_lover_of_actor",
    "adult_partner_or_lover_of_actor")


def _rom_event(event_id):
    for ev in EventEngine(event_mod.load_event_files()).events:
        if ev["id"] == event_id:
            return ev
    raise AssertionError(f"{event_id} not in loaded content")


class _P:
    def __init__(self, pid, age):
        self.person_id = pid
        self.age = age
        self.health = 1
        self.partner = None


class _Ctx:
    """Envelope-backed duck context: only _env + people_by_id are read."""

    def __init__(self, graph, people):
        self._env = {"graph": graph}
        self.people_by_id = people


def _graph(*parentage_edges):
    g = new_graph()
    for parent_id, child_id, role in parentage_edges:
        g["parentage"].append({"child_id": child_id,
                               "parent_id": parent_id, "role": role,
                               "source": "birth", "confidence": "assumed"})
    return g


def _stranger(age=25):
    from src.people.classes.person import Person
    p = Person("Sam", "Outsider", age, "Male", 50, 100, 50, 50)
    p.partner = None
    return p


class KinGuardMatrixTests(unittest.TestCase):
    """Every close-kin relation and minors are rejected at bind time;
    unrelated adults still bind."""

    def setUp(self):
        self._saved = PREDICATES.get("adult_acquaintance")
        self.ev = _rom_event("rom_first_date_walk")
        self.addCleanup(self._restore)

    def _restore(self):
        PREDICATES["adult_acquaintance"] = self._saved

    def _probe(self, candidate_id, candidate_age, graph):
        people = {"p": _P("p", 25), candidate_id: _P(candidate_id,
                                                     candidate_age)}
        PREDICATES["adult_acquaintance"] = lambda a, c: candidate_id
        return v3_bindable(self.ev, "p", _Ctx(graph, people))

    def _assert_rejected(self, candidate_id, graph):
        self.assertIsNone(
            self._probe(candidate_id, 40, graph),
            f"{candidate_id} must never be bound as a date")

    def _assert_allowed(self, candidate_id, graph):
        bindings = self._probe(candidate_id, 30, graph)
        self.assertIsNotNone(bindings)
        self.assertEqual(bindings["crush"], candidate_id)

    def test_mother_and_father_rejected(self):
        for role in ("biological", "adoptive", "step", "guardian",
                     "social"):
            self._assert_rejected("mum",
                                  _graph(("mum", "p", role)))

    def test_child_rejected(self):
        self._assert_rejected("kid", _graph(("p", "kid", "biological")))

    def test_sibling_rejected(self):
        self._assert_rejected(
            "sib", _graph(("mum", "p", "biological"),
                          ("mum", "sib", "biological")))

    def test_grandparent_grandchild_rejected(self):
        self._assert_rejected(
            "gran", _graph(("gran", "mum", "biological"),
                           ("mum", "p", "biological")))

    def test_aunt_uncle_niece_nephew_rejected(self):
        self._assert_rejected(
            "aunt", _graph(("gran", "mum", "biological"),
                           ("gran", "aunt", "biological"),
                           ("mum", "p", "biological")))

    def test_first_cousin_rejected(self):
        self._assert_rejected(
            "cuz", _graph(("gran", "mum", "biological"),
                          ("mum", "p", "biological"),
                          ("gran", "unc", "biological"),
                          ("unc", "cuz", "biological")))

    def test_unrelated_adult_still_bound(self):
        self._assert_allowed("sam", _graph())

    def test_minor_rejected_even_unrelated(self):
        self.assertIsNone(self._probe("teen", 17, _graph()))

    def test_non_romantic_event_not_affected(self):
        # kin may be bound in non-romantic content (family arcs)
        import src.game.content  # noqa: F401
        ev = _rom_event("rom_first_date_walk")
        ev = dict(ev)
        ev["interaction"] = {"kind": "parenting_request"}
        people = {"p": _P("p", 25), "mum": _P("mum", 46)}
        PREDICATES["adult_acquaintance"] = lambda a, c: "mum"
        bindings = v3_bindable(ev, "p", _Ctx(_graph(("mum", "p",
                                                     "biological")),
                                             people))
        self.assertIsNotNone(bindings)
        self.assertEqual(bindings["crush"], "mum")


class RealRouteRomanceBindingTests(unittest.TestCase):
    """Through the real envelope: with only kin in reach no rom_ event is
    served at all; an unrelated adult is served and bound."""

    PROFILE = {"first_name": "Alex", "last_name": "Rivers",
               "gender": "Male", "country": "Australia", "seed": 11}

    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-rombind-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(self.PROFILE), self.id() + "-c")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 20
        self.svc._save(env)
        self._saved = {name: PREDICATES.get(name)
                       for name in ROMANTIC_PREDICATES}
        self.addCleanup(self._restore_predicates)

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def _restore_predicates(self):
        for name, fn in self._saved.items():
            PREDICATES[name] = fn

    def env(self):
        return self.svc._load(self.gid)

    def _kin_ids(self, env):
        pid = env["player"].person_id
        return {e["parent_id"] for e in env["graph"]["parentage"]
                if e["child_id"] == pid}

    def _engine(self):
        return EventEngine(event_mod.load_event_files())

    def _sample(self, env, seed):
        ctx = GameContext(env, self.svc)
        return self._engine().sample_v3(
            random.Random(seed), env["player"].age, [], ctx,
            prefixes=("rom_",), actor_id=env["player"].person_id)

    def _patch_all_romantic_predicates(self, pid):
        for name in ROMANTIC_PREDICATES:
            PREDICATES[name] = (lambda a, c, pid=pid: pid)

    def test_only_kin_in_reach_no_romance_served(self):
        env = self.env()
        mother_id = next(iter(self._kin_ids(env)))
        self._patch_all_romantic_predicates(mother_id)
        for seed in range(25):
            ev, bindings = self._sample(env, seed)
            self.assertIsNone(ev, "a rom_ event was served with only a "
                                   "parent reachable as the date")
            self.assertIsNone(bindings)

    def test_mother_never_bound_through_real_route(self):
        # The exact reported bug: the predicate would happily return the
        # player's own mother. Even mixed with other candidates she must
        # never appear in any served bindings.
        env = self.env()
        pid = env["player"].person_id
        mother_id = next(iter(self._kin_ids(env)))
        stranger = _stranger()
        env["archive"].append(stranger)
        saved = PREDICATES.get("adult_acquaintance")
        try:
            PREDICATES["adult_acquaintance"] = lambda a, c: mother_id
            ctx = GameContext(env, self.svc)
            seen = []
            for _ in range(60):
                ev, bindings = self._engine().sample_v3(
                    random.Random(3), 20, seen, ctx, prefixes=("rom_",),
                    actor_id=pid)
                if ev is None:
                    break
                seen.append(ev["id"])
                person_roles = {b for r, b in bindings.items()
                                if b is not None and r != "actor"}
                self.assertNotIn(
                    mother_id, person_roles,
                    f"{ev['id']} bound the player's mother as the date")
        finally:
            PREDICATES["adult_acquaintance"] = saved

    def test_unrelated_adult_still_served_and_bound(self):
        env = self.env()
        stranger = _stranger()
        env["archive"].append(stranger)
        self._patch_all_romantic_predicates(stranger.person_id)
        ev, bindings = self._sample(env, 7)
        self.assertIsNotNone(ev, "an unrelated adult is a legal date")
        bound = {b for r, b in bindings.items()
                 if b is not None and r != "actor"}
        self.assertIn(stranger.person_id, bound)
        self.assertNotIn(env["player"].person_id, bound)
        # and instantiate3 with these prebindings raises nothing
        ctx = GameContext(env, self.svc)
        pending = EventEngine.instantiate3(ev, 1, random.Random(1), ctx,
                                           prebound=bindings)
        self.assertIn("options", pending)


if __name__ == "__main__":
    unittest.main()
