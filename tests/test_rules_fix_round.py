"""Fix-round regressions (M2-consolidated-review findings #2-#5 + minor).
Each test forces the failing path — no reliance on rare random events."""

import json
import random
import unittest

import src.menus.rule_actions as ra
import src.game.events as event_mod
from src.game.events import CONTENT_DIR, EventEngine, GameContext
from src.people.classes.partner import Partner
from src.lifesim_lib.lifesim_lib import Gender
from tests.test_rules_income import make_player


def content_event(event_id):
    for ev in event_mod.load_event_files(CONTENT_DIR):
        if ev["id"] == event_id:
            return ev
    raise KeyError(event_id)


class Ctx:
    """Minimal context honoring the loader protocol."""
    def __init__(self, money=0, stats=None, flags=None, alive=None,
                 partner_status=None, partner_relationship=None,
                 fields=None, role_ids=None, names=None):
        self._money = money
        self._stats = stats or {}
        self._flags = flags or {}
        self._alive = alive or set()
        self._ps = partner_status
        self._pr = partner_relationship
        self._fields = fields or {}
        self._role_ids = role_ids or {}
        self._names = names or {}

    def alive_role(self, role):
        return role in self._alive

    def has_illness(self, illness):
        return False

    def is_employed(self):
        return False

    def is_in_school(self):
        return False

    def stat(self, name):
        return self._stats.get(name, 0)

    def money(self):
        return self._money

    def flag(self, name):
        return bool(self._flags.get(name))

    def partner_status(self):
        return self._ps

    def partner_relationship(self):
        return self._pr

    def field(self, ref):
        return self._fields.get(ref, 0)

    def role_person_ids(self, role):
        return list(self._role_ids.get(role, []))

    def random_person_id(self, parents_only=False, rng=None):
        return None

    def person_name(self, person_id):
        return self._names.get(person_id)


class Finding4PreEffectsTest(unittest.TestCase):
    def test_kidnapping_nothing_keeps_event_level_penalty(self):
        ev = content_event("partner_kidnapping")
        ctx = Ctx(money=2_000_000, stats={"karma": 0}, alive={"partner"})
        pending = EventEngine.instantiate(
            ev, 1, "Test", rng=random.Random(11), context=ctx)
        nothing = next(c for c in pending["options"]
                       if c["id"].endswith("nothing"))
        # #4: pre-effects are sampled once and served separately; options
        # carry choice effects only (both survive — no shallow overlay).
        self.assertIn("happiness", pending["pre_effects"])
        self.assertLess(pending["pre_effects"]["happiness"], 0)
        self.assertIn("karma", nothing["_effects"])
        self.assertLess(nothing["_effects"]["karma"], 0)

    def test_same_stat_pre_and_choice_both_apply(self):
        ev = {
            "id": "sum_test", "title": "T", "text": "x", "weight": 1,
            "conditions": {},
            "effects": {"stats": {"happiness": {"randint": [1, 2],
                                                "sign": -1}}},
            "choices": [
                {"id": "a", "label": "A", "known_effects": [],
                 "cost": {"money": 0},
                 "effects": {"stats": {"happiness": {"randint": [3, 4],
                                                     "sign": -1}}},
                 "outcome_text": "done"},
                {"id": "b", "label": "B", "known_effects": [],
                 "cost": {"money": 0}, "effects": {},
                 "outcome_text": "done"},
            ],
        }
        ctx = Ctx()
        pending = EventEngine.instantiate(
            ev, 1, "Test", rng=random.Random(5), context=ctx)
        # pre-effect -(1..2) is served separately; choice -(3..4) applies
        self.assertTrue(-2 <= pending["pre_effects"]["happiness"] <= -1)
        self.assertTrue(-4 <= pending["options"][0]["_effects"]["happiness"] <= -3)

    def test_auto_event_keeps_all_stats_and_text(self):
        ev = content_event("thanatophobia_worry")
        bundle = EventEngine.instantiate_auto(
            ev, random.Random(3),
            Ctx(flags={"trait:THANATOPHOBIA"}, stats={"karma": 0}))
        for key in ("happiness", "stress", "health"):
            self.assertIn(key, bundle["_effects"])
        self.assertTrue(bundle["_outcome"].strip())


class Finding5IndependentChancesTest(unittest.TestCase):
    def test_independent_events_both_fire_apart_from_flavor(self):
        a = {"id": "chance_a", "title": "A", "text": "x", "weight": 1,
             "chance_n": 2, "conditions": {},
             "choices": [{"id": "a", "label": "A", "known_effects": [],
                          "cost": {"money": 0}, "effects": {},
                          "outcome_text": "a"},
                         {"id": "b", "label": "B", "known_effects": [],
                          "cost": {"money": 0}, "effects": {},
                          "outcome_text": "b"}]}
        b = dict(a, id="chance_b")
        flavor = {"id": "flavor_x", "title": "F", "text": "x", "weight": 1,
                  "conditions": {},
                  "choices": [{"id": "a", "label": "A", "known_effects": [],
                               "cost": {"money": 0}, "effects": {},
                               "outcome_text": "a"},
                              {"id": "b", "label": "B", "known_effects": [],
                               "cost": {"money": 0}, "effects": {},
                               "outcome_text": "b"}]}
        engine = EventEngine([a, b, flavor])
        ctx = Ctx()
        # force every 1-in-2 gate to pass
        class ForceRng(random.Random):
            def randrange(self, n):
                return 0
        for seed in range(50):
            hits = engine.sample_independent(
                ForceRng(seed), 30, set(), ctx)
            self.assertEqual([e["id"] for e in hits],
                             ["chance_a", "chance_b"], seed)
        # flavor pool never contains chance_n events
        for seed in range(200):
            pick = engine.sample(ForceRng(seed), 30, set(), ctx)
            if pick is not None:
                self.assertNotIn("chance_n", pick, pick["id"])


class Finding3DeathInstructionTest(unittest.TestCase):
    def test_partner_death_returns_typed_instruction(self):
        p = make_player(age=40, money=2_000_000, karma=0)
        rel = Partner(40, Gender.Female, 50, 50, 90, 2)
        p.partner = rel
        p.relations.append(rel)
        ev = content_event("partner_kidnapping")
        ctx = Ctx(money=2_000_000, stats={"karma": 0}, alive={"partner"},
                  role_ids={"partner": [rel.person_id]})
        pending = EventEngine.instantiate(
            ev, 1, "Test", rng=random.Random(11), context=ctx)
        nothing = next(c for c in pending["options"]
                       if c["id"].endswith("nothing"))
        result = ra._resolve_event_pending(
            p, p, {"id": "d_1", "event_id": "partner_kidnapping",
                   "title": "Kidnapping", "text": "x",
                   "person_ids": [p.person_id],
                   "options": [nothing]}, nothing["id"], random.Random(2))
        self.assertEqual(len(result["deaths"]), 1)
        death = result["deaths"][0]
        self.assertEqual(death["person_id"], rel.person_id)
        self.assertEqual(death["cause"], "presumed dead")
        self.assertIn("presumed dead", death["announcement"])
        # funeral decision continues the turn; graph removal is A's step
        self.assertIsNotNone(result["pending_decision"])
        self.assertEqual(result["pending_decision"]["kind"], "funeral")


class Finding2SnapshotFilterTest(unittest.TestCase):
    def test_non_snapshot_children_get_no_year(self):
        p = make_player(age=40)
        c1 = make_player(age=20, salary=40000, has_job=False)
        c2 = make_player(age=18, salary=30000, has_job=False)
        c1.is_in_uv = 0
        c2.is_in_uv = 0
        c1.partner_desire = 0
        c2.partner_desire = 0
        c1.may_divorce = False
        c2.may_divorce = False
        p.children = [c1, c2]
        y1 = c1.years_worked
        y2 = c2.years_worked
        # snapshot includes only c1 (c2 is a "newborn"/non-member)
        ra.family_events_phase(p, "t1", random.Random(7),
                               snapshot_ids={c1.person_id})
        self.assertEqual(c1.years_worked, y1 + 1)   # updated once
        self.assertEqual(c2.years_worked, y2)       # skipped entirely
        # no filter = legacy behavior (both update)
        ra.family_events_phase(p, "t2", random.Random(7))
        self.assertEqual(c1.years_worked, y1 + 2)
        self.assertEqual(c2.years_worked, y2 + 1)


class MinorPersonTokenTest(unittest.TestCase):
    def test_person_role_token_resolves_to_name(self):
        import tempfile
        from src.game.service import GameService
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game({"first_name": "Tok", "last_name": "En",
                           "gender": "male", "country": "Australia",
                           "seed": 3}, "token-test")
        env = svc._load(st["game_id"])
        rel = Partner(35, Gender.Female, 50, 50, 80, 2)
        env["player"].partner = rel
        env["player"].relations.append(rel)
        svc._save(env)
        env = svc._load(st["game_id"])
        ctx = GameContext(env, svc)
        ev = content_event("partner_kidnapping")
        pending = EventEngine.instantiate(
            ev, 1, "Tok", rng=random.Random(4), context=ctx)
        partner = env["player"].partner
        self.assertIsNotNone(partner)
        self.assertIn(partner.name.split()[0], pending["text"])
        self.assertNotIn("{person:", pending["text"])


if __name__ == "__main__":
    unittest.main()


class KarmaPassthroughTest(unittest.TestCase):
    """A's failing-before probe: top-level stat keys (karma) in an effects
    map must reach _effects, not be dropped."""

    def test_top_level_karma_survives(self):
        fx, _ = event_mod.EventEngine._resolve_effects(
            {"stats": {"happiness": -5}, "karma": -1},
            random.Random(1), Ctx())
        self.assertEqual(fx, {"happiness": -5, "karma": -1})

    def test_top_level_stat_keys_lifted(self):
        fx, _ = event_mod.EventEngine._resolve_effects(
            {"stress": {"randint": [2, 4]}, "stats": {"health": -1}},
            random.Random(2), Ctx())
        self.assertIn("stress", fx)
        self.assertTrue(2 <= fx["stress"] <= 4)
        self.assertEqual(fx["health"], -1)
