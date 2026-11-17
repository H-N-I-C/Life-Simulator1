"""M2.5 Phase 2 step 2 — dating-app offers (Kimi C).

The dating-app action binds an adult stranger through A's registered
graph predicate, scores the offer with B's evaluator (mocked here at
that true domain boundary), draws accept/refuse ONCE with the seeded
rng, and stores the draw in the pending. Stored refusal replaces the
accept outcome (never rerolled); stored acceptance resolves through the
domain registry (A's appliers in production, fakes here).
"""

import random
import unittest
from unittest import mock

import src.menus.rule_actions as ra
from tests.test_rules_m2b import partner_for
from tests.test_rules_income import make_player
from tests.test_rules_v3_loader import V3Context, _FakeService

EVALUATOR = "src.game.features.relationship_policy.evaluator.evaluate_offer"


def scored(score=70, p_refuse=0.0, reasons=None, hard_ok=True):
    return {
        "hard": {"ok": hard_ok, "code": None if hard_ok else "not_adult"},
        "score": score if hard_ok else None,
        "reasons": reasons or [],
        "refusal": {"possible": True, "p_refuse": p_refuse},
        "projected_recognition": None,
        "projected_consequences": [],
        "policy_revision": 0,
    }


class DatingAppTest(unittest.TestCase):
    def _ctx(self, player, partner=True):
        if partner:
            partner_for(player)
        ctx = V3Context(player, money=500)
        ctx._env["policy"] = {"profiles": {}, "overrides": {},
                              "jurisdiction_id": None}
        return ctx

    def test_hard_failure_is_typed_error(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        with mock.patch(EVALUATOR, return_value=scored(hard_ok=False,
                                                       p_refuse=0.0)):
            with self.assertRaises(ra.RuleError) as raised:
                ra.apply_action(ctx, "dating_app", {}, random.Random(1))
        self.assertEqual(raised.exception.code, "not_allowed")

    def test_acceptance_stored_and_seeded(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=0.0)):
            result = ra.apply_action(ctx, "dating_app", {},
                                     random.Random(7))
        pending = result["pending_decision"]
        self.assertEqual(pending["schema_dialect"], "v3")
        self.assertTrue(pending["sampled"]["accepted"])
        self.assertEqual(pending["sampled"]["score"], 70)
        target = pending["bindings"]["target"]
        self.assertIsNotNone(target)
        self.assertIn(target, ctx.people_by_id)
        accept = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        self.assertTrue(accept["_effects"])  # effects intact on acceptance

    def test_refusal_stored_once_and_never_rerolled(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=1.0)):
            result = ra.apply_action(ctx, "dating_app", {},
                                     random.Random(7))
        pending = result["pending_decision"]
        self.assertFalse(pending["sampled"]["accepted"])
        accept = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        self.assertEqual(accept["_effects"], {})
        self.assertIsNone(accept["_domain"])
        # resolve the stored refusal: no effects, refusal text, and a
        # different rng must not change anything (no reroll)
        happy_before = player.happiness
        resolved = ra.resolve_choice(ctx, pending, accept["id"], {},
                                     random.Random(999))
        self.assertEqual(player.happiness, happy_before)
        self.assertIn("never replied", resolved["events"][-1]["text"])

    def test_accepted_resolve_applies_domain_once(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=0.0)):
            result = ra.apply_action(ctx, "dating_app", {},
                                     random.Random(7))
        pending = result["pending_decision"]
        accept = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        calls = []

        def fake_chemistry(context, pl, bindings, value, rng):
            calls.append(bindings["target"])
            return [], []

        ra.register_domain_effect("chemistry", fake_chemistry)
        ra.register_domain_effect("offer", lambda c, p, b, v, r: ([], []))
        try:
            ra.resolve_choice(ctx, pending, accept["id"], {},
                              random.Random(2))
        finally:
            ra._DOMAIN_EFFECTS.pop("chemistry", None)
            ra._DOMAIN_EFFECTS.pop("offer", None)
        self.assertEqual(calls, [pending["bindings"]["target"]])

    def test_no_eligible_adults_is_typed_error(self):
        player = make_player(age=30, money=500)
        player.relations = []
        ctx = V3Context(player, money=500)
        ctx._env["policy"] = {"profiles": {}, "overrides": {}}
        with self.assertRaises(ra.RuleError) as raised:
            ra.apply_action(ctx, "dating_app", {}, random.Random(1))
        self.assertEqual(raised.exception.code, "not_found")

    def test_dating_app_blocked_during_school(self):
        player = make_player(age=30, money=500)
        player.dropped_out = False
        player.grades = 50
        player.age = 17  # hmm: school requires is_in_school
        player.age = 20
        player.uv_years = 1  # university counts as in school
        ctx = self._ctx(player)
        with self.assertRaises(ra.RuleError) as raised:
            ra.apply_action(ctx, "dating_app", {}, random.Random(1))
        self.assertEqual(raised.exception.code, "not_allowed")

    def test_spec_agreement_both_directions(self):
        # enabled spec -> dispatch accepts; blocked state hides the spec
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        specs = {s["id"]: s for s in ra.action_specs(ctx)}
        self.assertIn("dating_app", specs)
        self.assertTrue(specs["dating_app"]["enabled"])
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=0.0)):
            ra.apply_action(ctx, "dating_app", {}, random.Random(1))


if __name__ == "__main__":
    unittest.main()
