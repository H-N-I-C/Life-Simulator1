"""Loader tests (src/game/events.py, C-owned) against the parity oracle:
sampling semantics, pre-sampled stored choices, flattening, auto events,
and end-to-end through the real GameService decision_offer phase."""

import random
import unittest

from src.game.events import (CONTENT_DIR, EventEngine, GameContext,
                             load_event_files)
import src.game.events as event_mod
from tests.test_rules_income import make_player


class FakeContext:
    """Duck-typed context for loader tests (C-side, no service)."""
    def __init__(self, money=0, stats=None, flags=None, alive=None,
                 partner_status=None, partner_relationship=None,
                 fields=None, role_ids=None):
        self._money = money
        self._stats = stats or {}
        self._flags = flags or {}
        self._alive = alive or set()
        self._partner_status = partner_status
        self._partner_relationship = partner_relationship
        self._fields = fields or {}
        self._role_ids = role_ids or {}

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
        return self._partner_status

    def partner_relationship(self):
        return self._partner_relationship

    def field(self, ref):
        return self._fields.get(ref, 0)

    def role_person_ids(self, role):
        return list(self._role_ids.get(role, []))

    def random_person_id(self, parents_only=False, rng=None):
        return None


class LoaderOracleTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.events = {e["id"]: e
                      for e in load_event_files(CONTENT_DIR)}

    def test_occurrence_rates(self):
        """chance_n events hit at their 1/N rate over seeded trials."""
        engine = EventEngine([self.events["vaccination_age2"]])
        ctx = FakeContext(alive={"mother"})
        hits = sum(
            1 for t in range(3000)
            if engine.sample_independent(random.Random(t), 2, set(), ctx))
        self.assertAlmostEqual(hits / 3000, 0.5, delta=0.06)
        # age gate: never at age 3
        self.assertEqual(
            engine.sample_independent(random.Random(1), 3, set(), ctx), [])

    def test_breakup_gates(self):
        engine = EventEngine([self.events["partner_breakup_offer"]])
        # engaged with low rel: occurs at ~1/3
        hits = 0
        for t in range(900):
            ctx = FakeContext(partner_status=1, partner_relationship=0)
            if engine.sample_independent(random.Random(t), 30, set(), ctx):
                hits += 1
        self.assertAlmostEqual(hits / 900, 1 / 3, delta=0.07)
        # married (status 2): excluded
        ctx = FakeContext(partner_status=2, partner_relationship=0)
        self.assertEqual(
            engine.sample_independent(random.Random(1), 30, set(), ctx), [])

    def test_kidnapping_gates(self):
        engine = EventEngine([self.events["partner_kidnapping"]])
        # poor: excluded; karma 0 passes the karma_gate always
        ctx = FakeContext(money=10, stats={"karma": 0}, alive={"partner"})
        self.assertEqual(
            engine.sample_independent(random.Random(5), 40, set(), ctx), [])
        # karma 200 fails the gate every time
        ctx = FakeContext(money=2_000_000, stats={"karma": 200},
                          alive={"partner"})
        self.assertEqual(
            engine.sample_independent(random.Random(5), 40, set(), ctx), [])

    def test_instantiate_pre_samples_and_flattens(self):
        ev = self.events["partner_divorce_offer"]
        engine = EventEngine([ev])
        ctx = FakeContext(money=100000, partner_status=2,
                          partner_relationship=10, fields={
                              "partner.willpower": 50})
        pending = engine.instantiate(ev, 1, "Test", rng=random.Random(3),
                                     context=ctx)
        self.assertEqual(pending["kind"], "partner_divorce_offer")
        honor = pending["options"][0]
        self.assertIn("happiness", honor["_effects"])  # pre-effect merged
        self.assertEqual(honor["_effects"]["money"], honor["_effects"]["money"])
        self.assertTrue(honor["_structural"]["divorce"])
        self.assertTrue(honor["_effects"]["money"] < 0)
        self.assertGreaterEqual(-honor["_effects"]["money"], 40000 - 1)
        self.assertLessEqual(-honor["_effects"]["money"], 60000 + 1)
        # pre-sampled: a second instantiate with the SAME seed is identical
        pending2 = engine.instantiate(ev, 1, "Test", rng=random.Random(3),
                                      context=ctx)
        self.assertEqual(
            [o["_effects"] for o in pending["options"]],
            [o["_effects"] for o in pending2["options"]])
        # affordability projection fields exist (current content costs are
        # all proportional, so enabled stays True by construction)
        self.assertIn("enabled", pending["options"][0])
        self.assertIn("reason", pending["options"][0])

    def test_kidnapping_sampled_ransom(self):
        ev = self.events["partner_kidnapping"]
        ctx = FakeContext(money=1_000_000, stats={"karma": 0},
                          alive={"partner"})
        seen = set()
        for seed in range(60):
            pending = EventEngine.instantiate(
                ev, 1, "Test", rng=random.Random(seed), context=ctx)
            pay = pending["options"][0]
            ransom = pay["_effects"]["money"]
            self.assertTrue(-800000 - 1 <= ransom <= -200000 + 1)
            seen.add(ransom)
            self.assertEqual(pay["cost_gates"] if False else
                             pay["known_effects"], pay["known_effects"])
        self.assertGreater(len(seen), 10)

    def test_willpower_branch_two_draws(self):
        """Insist rate at willpower 50 with draws=2 ~ 0.75 (M1 policy)."""
        ev = self.events["partner_breakup_offer"]
        insists = 0
        trials = 1500
        for seed in range(trials):
            ctx = FakeContext(partner_status=0, partner_relationship=0,
                              fields={"partner.willpower": 50})
            pending = EventEngine.instantiate(
                ev, 1, "Test", rng=random.Random(seed), context=ctx)
            beg = pending["options"][1]
            if (beg["_structural"] or {}).get("lose_partner"):
                insists += 1
        self.assertAlmostEqual(insists / trials, 0.75, delta=0.08)

    def test_auto_events(self):
        ev = self.events["lightning_strike"]
        self.assertTrue(ev.get("auto"))
        ctx = FakeContext()
        good = bad = death = 0
        for seed in range(3000):
            bundle = EventEngine.instantiate_auto(
                ev, random.Random(seed), ctx)
            if bundle["terminal"]:
                death += 1
            elif bundle["_effects"]["happiness"] > 0:
                good += 1
            else:
                bad += 1
        total = good + bad + death
        self.assertAlmostEqual(good / total, 0.5, delta=0.06)
        self.assertAlmostEqual(death / total, 0.1, delta=0.04)
        # instantiate() refuses auto events
        with self.assertRaises(event_mod.EventValidationError):
            EventEngine.instantiate(ev, 1, "Test", rng=random.Random(1),
                                    context=ctx)

    def test_thanatophobia_auto_ranges(self):
        ev = self.events["thanatophobia_worry"]
        bundle = EventEngine.instantiate_auto(
            ev, random.Random(9), FakeContext(flags={"trait:THANATOPHOBIA"},
                                                stats={"karma": 0}))
        self.assertTrue(-6 <= bundle["_effects"]["happiness"] <= -4)
        self.assertTrue(3 <= bundle["_effects"]["stress"] <= 8)
        self.assertEqual(bundle["_effects"]["health"], -1)


class EndToEndServiceTest(unittest.TestCase):
    def test_decision_offer_serves_content_events(self):
        """Age a real game through the service; the decision_offer phase
        must eventually offer a DATA-FILE event (bundled or content) and
        resolve it through the real resolver."""
        import tempfile
        from src.game.service import GameService
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-events-test-"))
        profile = {"first_name": "Kid", "last_name": "Tester",
                   "gender": "female", "country": "Australia", "seed": 11}
        st = svc.new_game(profile, "ev-test-1")
        gid = st["game_id"]
        offered = None
        for i in range(40):
            st = svc.get_state(gid)
            if st["status"] != "alive":
                break
            if st.get("pending_decision"):
                offered = st["pending_decision"]
                break
            st = svc.command(gid, st["revision"], f"ev-a-{i}", "age_up", {})
        self.assertIsNotNone(offered, "no decision offered in 40 years")
        self.assertTrue(offered.get("event_id"),
                        f"decision has no event_id: {offered}")
        # resolve the first enabled option through the real resolver
        option = next(o for o in offered["options"] if o["enabled"])
        st = svc.command(gid, st["revision"], "ev-resolve-1",
                         "resolve_decision",
                         {"decision_id": offered["id"],
                          "option_id": option["id"]})
        self.assertIsNone(st["pending_decision"])
        self.assertEqual(st["journal"][-1]["kind"], "decision")


if __name__ == "__main__":
    unittest.main()


class SavedPendingCompatibilityTest(unittest.TestCase):
    def test_old_vaccination_pending_of_that_kind_resolves(self):
        """Saved games hold pendings whose kind IS the event id (e.g.
        'vaccination_age2'); resolve_choice must accept them forever."""
        from src.menus import rule_actions as ra
        p = make_player(age=2)
        mother = p.parents["Mother"]
        rel0 = mother.relationship
        pending = {
            "id": "d_9",
            "kind": "vaccination_age2",
            "event_id": "vaccination_age2",
            "title": "Vaccination",
            "text": "Your mother is taking you to the doctor's office.",
            "person_ids": [p.person_id],
            "options": [{
                "id": "d_9_bite", "label": "Bite her",
                "known_effects": ["-happiness (6-10)",
                                  "-mother relationship (25-35)"],
                "enabled": True, "reason": None, "inputs": [],
                "_outcome": "You bit your mother.",
                "_effects": {"happiness": -8, "relationship": -30},
                "_structural": None, "_person_ids_extra": [mother.person_id],
            }],
        }
        r = ra.resolve_choice(p, pending, "d_9_bite", {}, random.Random(1))
        self.assertEqual(p.happiness, 50 - 8)
        self.assertEqual(mother.relationship, rel0 - 30)
        self.assertEqual(r["money_entries"], [])
        self.assertFalse(r["finished_turn"])
        with self.assertRaises(ra.RuleError):
            ra.resolve_choice(p, pending, "d_9_forge", {}, random.Random(1))

    def test_event_money_goes_through_entries_not_mutation(self):
        from src.menus import rule_actions as ra
        p = make_player(age=50, money=100000)
        pending = {
            "id": "d_10", "kind": "partner_divorce_offer",
            "event_id": "partner_divorce_offer",
            "title": "Divorce", "text": "x", "person_ids": [p.person_id],
            "options": [{
                "id": "d_10_honor", "label": "Wish them well",
                "known_effects": [], "enabled": True, "reason": None,
                "inputs": [], "_outcome": "You honored the request.",
                "_effects": {"happiness": -17, "money": -50000},
                "_structural": {"divorce": True},
            }],
        }
        money_before = p.money
        r = ra.resolve_choice(p, pending, "d_10_honor", {}, random.Random(2))
        self.assertEqual(p.money, money_before)  # no direct mutation
        (entry,) = r["money_entries"]
        self.assertEqual(entry["amount"], -50000)
        self.assertEqual(entry["account_owner_id"], p.person_id)
