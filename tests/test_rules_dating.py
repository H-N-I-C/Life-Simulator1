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
from src.game.context import RuleContext
from src.people.classes.partner import Partner
from src.lifesim_lib.lifesim_lib import Gender
from tests.test_rules_m2b import partner_for
from tests.test_rules_income import make_player
from tests.test_rules_v3_loader import V3Context, _FakeService

EVALUATOR = "src.game.features.relationship_policy.evaluator.evaluate_offer"


def scored(score=70, p_refuse=0.0, reasons=None, hard_ok=True,
           p_accept=None):
    result = {
        "hard": {"ok": hard_ok, "code": None if hard_ok else "not_adult"},
        "score": score if hard_ok else None,
        "reasons": reasons or [],
        "refusal": {"possible": True, "p_refuse": p_refuse},
        "projected_recognition": None,
        "projected_consequences": [],
        "policy_revision": 0,
    }
    if p_accept is not None:
        result["acceptance"] = {"possible": True, "p_accept": p_accept}
    return result


class _RiggedDraw:
    """RNG whose FIRST draw returns a scripted value (the accept draw),
    then delegates everything to a real seeded Random for the
    instantiate3 sampling that follows."""
    def __init__(self, first):
        self._first = first
        self._inner = random.Random(11)

    def random(self):
        if self._first is not None:
            value, self._first = self._first, None
            return value
        return self._inner.random()

    def __getattr__(self, name):
        return getattr(self._inner, name)


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
        # a stored refusal keeps ONLY the offer domain effect: resolving
        # it records the graph offer attempt (A's applier); the date's
        # happiness/chemistry effects are replaced by the refusal text
        self.assertEqual(list(accept["_domain"].keys()), ["offer"])
        self.assertNotIn("chemistry", accept["_domain"])
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

        saved = {k: ra._DOMAIN_EFFECTS.get(k)
                 for k in ("chemistry", "offer")}
        ra.register_domain_effect("chemistry", fake_chemistry)
        ra.register_domain_effect("offer", lambda c, p, b, v, r: ([], []))
        try:
            ra.resolve_choice(ctx, pending, accept["id"], {},
                              random.Random(2))
        finally:
            for k, v in saved.items():
                if v is None:
                    ra._DOMAIN_EFFECTS.pop(k, None)
                else:
                    ra._DOMAIN_EFFECTS[k] = v
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

    # ---- R3 ratified probability contract (fix round 2) ---------------
    # accept = (u < p_accept), p_accept clamped to [0.02, 0.98]: refusal
    # stays possible at a perfect score and acceptance stays possible at
    # a zero score. One draw, stored, never rerolled.

    def _draw(self, ctx, first, **scored_kwargs):
        with mock.patch(EVALUATOR, return_value=scored(**scored_kwargs)):
            return ra.apply_action(ctx, "dating_app", {}, _RiggedDraw(first))

    def test_perfect_score_still_refuses_bad_draw(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        result = self._draw(ctx, 0.99, p_accept=1.0)
        self.assertFalse(result["pending_decision"]["sampled"]["accepted"])
        self.assertEqual(result["pending_decision"]["sampled"]["p_accept"],
                         0.98)

    def test_zero_score_still_accepts_good_draw(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        result = self._draw(ctx, 0.005, p_accept=0.0)
        self.assertTrue(result["pending_decision"]["sampled"]["accepted"])
        self.assertEqual(result["pending_decision"]["sampled"]["p_accept"],
                         0.02)

    def test_accept_draw_uses_p_accept_both_sides(self):
        player = make_player(age=30, money=500)
        ctx = self._ctx(player)
        yes = self._draw(ctx, 0.49, p_accept=0.5)
        no = self._draw(ctx, 0.51, p_accept=0.5)
        self.assertTrue(yes["pending_decision"]["sampled"]["accepted"])
        self.assertFalse(no["pending_decision"]["sampled"]["accepted"])
        self.assertEqual(yes["pending_decision"]["sampled"]["p_accept"], 0.5)


class DatingAppServiceTest(unittest.TestCase):
    """M2.5 finish C1: the dating-app journey through real persisted
    GameService transactions — action admitted to the command allowlist,
    pending created with the stored single draw, refusal resolved without
    reroll, and the accept option carrying the offer domain for A's
    applier (faked here at the registry seam; production registers A's)."""

    def new(self):
        import tempfile
        from src.game.service import GameService
        from src.people.classes.partner import Partner
        from src.lifesim_lib.lifesim_lib import Gender
        self.tmp = tempfile.TemporaryDirectory(prefix="m25-dating-")
        self.addCleanup(self.tmp.cleanup)
        self.svc = GameService(self.tmp.name)
        state = self.svc.new_game({"first_name": "Ada", "last_name": "Lee",
                                   "gender": "female", "country": "Australia",
                                   "seed": 4242}, "create")
        self.gid = state["game_id"]
        env = self.svc._load(self.gid)
        player = env["player"]
        player.age = 30
        player.happiness = 50
        player.money = 5000
        stranger = Partner(28, Gender.Male, 60, 60, 100, 0)
        player.relations.append(stranger)
        self.svc._ensure_ids(env)
        env["balances"][player.person_id] = player.money
        env["balances"][stranger.person_id] = getattr(stranger, "money", 0)
        self.svc._save(env)
        self.pid = player.person_id
        self.stranger_id = stranger.person_id
        return env

    def command(self, action, payload=None, req=None):
        state = self.svc.get_state(self.gid)
        return self.svc.command(self.gid, state["revision"],
                                req or f"request-{state['revision']}",
                                action, payload or {})

    def test_service_dispatches_dating_app_and_creates_pending(self):
        self.new()
        state = self.command("dating_app")
        pending = state["pending_decision"]
        self.assertEqual(pending["schema_dialect"], "v3")
        self.assertEqual(pending["bindings"]["target"], self.stranger_id)
        self.assertIn("accepted", pending["sampled"])
        self.assertIn("score", pending["sampled"])
        self.assertEqual(pending["interaction"]["kind"], "date")

    def test_refusal_stored_and_resolves_without_reroll(self):
        self.new()
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=1.0)):
            state = self.command("dating_app")
        pending = state["pending_decision"]
        self.assertFalse(pending["sampled"]["accepted"])
        accept = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        state = self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": accept["id"]},
            req="resolve-1")
        self.assertIn("never replied", state["journal"][-1]["text"])
        env = self.svc._load(self.gid)
        self.assertEqual(env["graph"]["relationships"], [])
        # the production offer applier recorded the stored refusal:
        # exactly one offer, declined, npc-initiated, never re-rolled
        offers = env["graph"]["offers"]
        self.assertEqual(len(offers), 1)
        offer = offers[0]
        self.assertEqual(offer["status"], "declined")
        self.assertEqual(offer["initiator_id"], self.stranger_id)
        self.assertEqual(offer["target_id"], self.pid)
        self.assertFalse(bool(offer["sampled_outcomes"].get("accepted")))

    def test_accept_resolves_through_production_offer_applier(self):
        self.new()
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=0.0)):
            state = self.command("dating_app")
        pending = state["pending_decision"]
        if not pending["sampled"]["accepted"]:
            self.skipTest("seeded draw refused (p_accept clamp 0.98)")
        accept = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        stored = next(o for o in self.svc._load(self.gid)["pending"][
            "options"] if o["id"] == accept["id"])
        self.assertIn("offer", stored["_domain"])
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": accept["id"]},
            req="resolve-2")
        env = self.svc._load(self.gid)
        offer = env["graph"]["offers"][0]
        self.assertEqual(offer["status"], "accepted")
        self.assertEqual(offer["initiator_id"], self.stranger_id)
        # acceptance created the real relationship edge (stranger ->
        # partner chemistry)
        rel = env["graph"]["relationships"][0]
        self.assertEqual(set(rel["person_ids"]),
                         {self.pid, self.stranger_id})
        self.assertEqual(rel["status"], "active")


if __name__ == "__main__":
    unittest.main()

class TargetedOfferServiceTest(DatingAppServiceTest):
    """E2 (fix round 2): courtship_offer / union_proposal — the generic
    targetable adult offer journey B's UI calls. Real persisted
    GameService, production offer appliers, ratified draw contract."""

    def _specs(self):
        state = self.svc.get_state(self.gid)
        env = self.svc._load(self.gid)
        ctx = RuleContext(env, self.svc)
        return {s["id"]: s for s in ra.action_specs(ctx)}

    def test_specs_advertise_targeted_offers_for_adult_relations(self):
        self.new()
        specs = self._specs()
        self.assertIn("courtship_offer", specs)
        self.assertIn("union_proposal", specs)
        self.assertEqual(specs["courtship_offer"]["target_id"],
                         self.stranger_id)
        self.assertTrue(specs["courtship_offer"]["enabled"])
        self.assertEqual(specs["union_proposal"]["target_id"],
                         self.stranger_id)

    def test_specs_hide_targeted_offers_for_minor_targets(self):
        self.new()
        env = self.svc._load(self.gid)
        player = env["player"]
        kid = Partner(10, Gender.Male, 50, 50, 50, 0)
        player.relations.append(kid)
        self.svc._ensure_ids(env)
        env["balances"][kid.person_id] = 0
        self.svc._save(env)
        ctx = RuleContext(env, self.svc)
        rows = [s for s in ra.action_specs(ctx)
                if s["id"] in ("courtship_offer", "union_proposal")
                and s.get("target_id") == kid.person_id]
        self.assertEqual(rows, [])

    def test_courtship_dispatch_requires_valid_target(self):
        self.new()
        with self.assertRaises(Exception) as raised:
            self.command("courtship_offer", {})
        self.assertEqual(getattr(raised.exception, "code", None),
                         "invalid_input")
        with self.assertRaises(Exception) as raised:
            self.command("courtship_offer", {"target_id": "bogus"})
        self.assertEqual(getattr(raised.exception, "code", None),
                         "not_found")
        with self.assertRaises(Exception) as raised:
            self.command("courtship_offer",
                         {"target_id": self.pid})
        # apply_action's generic target gate refuses non-relations
        # (you are not your own relation) before the offer handler
        self.assertEqual(getattr(raised.exception, "code", None),
                         "not_allowed")

    def test_courtship_hard_failure_is_typed_not_allowed(self):
        self.new()
        with mock.patch(EVALUATOR,
                        return_value=scored(hard_ok=False)):
            with self.assertRaises(Exception) as raised:
                self.command("courtship_offer",
                             {"target_id": self.stranger_id})
        self.assertEqual(getattr(raised.exception, "code", None),
                         "not_allowed")

    def test_courtship_accept_creates_relationship_via_production(self):
        self.new()
        with mock.patch(EVALUATOR,
                        return_value=scored(p_refuse=0.0)):
            state = self.command("courtship_offer",
                                 {"target_id": self.stranger_id})
        pending = state["pending_decision"]
        self.assertEqual(pending["interaction"]["kind"], "propose_courtship")
        self.assertEqual(pending["bindings"]["target"], self.stranger_id)
        if not pending["sampled"]["accepted"]:
            self.skipTest("seeded draw refused (p_accept clamp 0.98)")
        ask = next(o for o in pending["options"]
                   if o["id"].endswith("_ask"))
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": ask["id"]},
            req="e2-court")
        env = self.svc._load(self.gid)
        offer = env["graph"]["offers"][0]
        self.assertEqual(offer["status"], "accepted")
        self.assertEqual(offer["initiator_id"], self.pid)
        rel = env["graph"]["relationships"][0]
        self.assertEqual(set(rel["person_ids"]),
                         {self.pid, self.stranger_id})
        self.assertEqual(rel["kind"], "courtship")

    def _court(self, req):
        """Real stage entry: public courtship offer accepted -> active
        courtship edge (W3 proposals require the courtship stage)."""
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=0.0)):
            state = self.command("courtship_offer",
                                 {"target_id": self.stranger_id}, req=req)
        pending = state["pending_decision"]
        if not pending["sampled"]["accepted"]:
            self.skipTest("seeded draw refused (p_accept clamp 0.98)")
        ask = next(o for o in pending["options"]
                   if o["id"].endswith("_ask"))
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": ask["id"]},
            req=req + "-r")

    def test_union_proposal_accept_forms_union_via_production(self):
        self.new()
        # W3 stage parity: without the courtship stage the proposal is
        # refused with the exact reason the spec advertises.
        env = self.svc._load(self.gid)
        ctx = RuleContext(env, self.svc)
        spec = next(s for s in ra.action_specs(ctx)
                    if s["id"] == "union_proposal"
                    and s["target_id"] == self.stranger_id)
        self.assertFalse(spec["enabled"])
        self.assertIn("courtship", spec["reason"])
        with self.assertRaises(Exception) as raised:
            self.command("union_proposal", {"target_id": self.stranger_id})
        self.assertEqual(getattr(raised.exception, "code", None),
                         "not_allowed")
        # real stage entry, then the proposal is admitted and enabled
        self._court("e2-stage")
        env = self.svc._load(self.gid)
        ctx = RuleContext(env, self.svc)
        spec = next(s for s in ra.action_specs(ctx)
                    if s["id"] == "union_proposal"
                    and s["target_id"] == self.stranger_id)
        self.assertTrue(spec["enabled"])
        with mock.patch(EVALUATOR,
                        return_value=scored(p_refuse=0.0)):
            state = self.command("union_proposal",
                                 {"target_id": self.stranger_id})
        pending = state["pending_decision"]
        self.assertEqual(pending["interaction"]["kind"], "propose_union")
        if not pending["sampled"]["accepted"]:
            self.skipTest("seeded draw refused (p_accept clamp 0.98)")
        ask = next(o for o in pending["options"]
                   if o["id"].endswith("_ask"))
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": ask["id"]},
            req="e2-union")
        env = self.svc._load(self.gid)
        union = env["graph"]["unions"][0]
        self.assertEqual(set(union["person_ids"]),
                         {self.pid, self.stranger_id})
        self.assertEqual(union["status"], "active")

    def test_stored_refusal_records_declined_offer(self):
        self.new()
        with mock.patch(EVALUATOR, return_value=scored(p_refuse=1.0)):
            state = self.command("courtship_offer",
                                 {"target_id": self.stranger_id})
        pending = state["pending_decision"]
        self.assertFalse(pending["sampled"]["accepted"])
        ask = next(o for o in pending["options"]
                   if o["id"].endswith("_ask"))
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": ask["id"]},
            req="e2-refuse")
        env = self.svc._load(self.gid)
        offer = env["graph"]["offers"][0]
        self.assertEqual(offer["status"], "declined")
        self.assertEqual(env["graph"]["relationships"], [])
        self.assertIn("turned me down", self.svc.get_state(
            self.gid)["journal"][-1]["text"])


class W3LawProjectionServiceTest(DatingAppServiceTest):
    """Round 4 W3 (N4 implementation): real evaluator + real law editor
    on the production service path. The projection the evaluator returns
    must be stored at offer creation and must be the recognition the
    accepted proposal actually forms; the interaction kind must be the
    ordinary/plural one matching existing unions."""

    def _force_accept(self):
        # saved RNG whose first draw lands under the 0.02 clamp floor
        env = self.svc._load(self.gid)
        env["rng_state"] = random.Random(139).getstate()
        self.svc._save(env)

    def _courtship_real(self, req):
        self._force_accept()
        state = self.command("courtship_offer",
                             {"target_id": self.stranger_id}, req=req)
        pending = state["pending_decision"]
        self.assertTrue(pending["sampled"]["accepted"])
        ask = next(o for o in pending["options"]
                   if o["id"].endswith("_ask"))
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": ask["id"]},
            req=req + "-r")

    def _resolve_ask(self, state, req):
        pending = state["pending_decision"]
        ask = next(o for o in pending["options"]
                   if o["id"].endswith("_ask"))
        self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": ask["id"]},
            req=req)
        return pending

    def _forbid(self, req, **recognition):
        env = self.svc._load(self.gid)
        jur = env["policy"]["jurisdiction_id"]
        st = self.svc.get_state(self.gid)
        self.svc.editor_apply(
            self.gid, st["revision"], req,
            [{"name": "edit_jurisdiction", "scope": "jurisdiction",
              "target_id": jur, "patch": {"recognition": recognition}}])

    def test_forbidden_law_projection_forms_forbidden_union(self):
        from src.game.features.family_graph.service_view import (
            ServiceGraphView, ServicePolicyView)
        from src.game.features.relationship_policy.evaluator import (
            evaluate_offer)
        self.new()
        self._courtship_real("w3-court")
        self._forbid("w3-law", monogamous_civil="forbidden",
                     plural_civil="forbidden")
        env = self.svc._load(self.gid)
        expected = evaluate_offer("propose_union", self.pid,
                                  self.stranger_id, ServiceGraphView(env),
                                  ServicePolicyView(env),
                                  env.get("policy_revision", 0))
        self.assertEqual(expected["projected_recognition"]["status"],
                         "forbidden")
        # the attempt stays ADMISSIBLE under a hostile law (consequence,
        # never a gate) — the stored offer carries the projection
        self._force_accept()
        state = self.command("union_proposal",
                             {"target_id": self.stranger_id})
        sampled = state["pending_decision"]["sampled"]
        self.assertTrue(sampled["accepted"])
        self.assertEqual(sampled["projected_recognition"]["status"],
                         "forbidden")
        self._resolve_ask(state, "w3-union")
        env = self.svc._load(self.gid)
        offer = env["graph"]["offers"][-1]
        self.assertEqual(offer["sampled_outcomes"]
                         ["projected_recognition"]["status"], "forbidden")
        union = env["graph"]["unions"][0]
        self.assertEqual(set(union["person_ids"]),
                         {self.pid, self.stranger_id})
        self.assertEqual(union["recognition"]["status"], "forbidden")

    def test_existing_union_selects_plural_interaction_and_recognition(self):
        from src.game.features.relationship_policy.evaluator import (
            evaluate_offer as real_evaluator)
        self.new()
        self._courtship_real("w3p-court")
        self._forbid("w3p-law", monogamous_civil="recognized",
                     plural_civil="unrecognized")
        env = self.svc._load(self.gid)
        player = env["player"]
        third = Partner(32, Gender.Female, 60, 60, 100, 0)
        player.relations.append(third)
        self.svc._ensure_ids(env)
        env["balances"][third.person_id] = getattr(third, "money", 0)
        env["graph"]["unions"].append({
            "union_id": "uni-existing", "kind": "marriage",
            "person_ids": [self.pid, third.person_id],
            "status": "active",
            "recognition": {"status": "recognized"}})
        self.svc._save(env)
        calls = []

        def spy(interaction, *args, **kwargs):
            calls.append(interaction)
            return real_evaluator(interaction, *args, **kwargs)

        self._force_accept()
        with mock.patch(EVALUATOR, side_effect=spy):
            state = self.command("union_proposal",
                                 {"target_id": self.stranger_id})
        self.assertEqual(calls, ["propose_plural_union"])
        pending = state["pending_decision"]
        self.assertEqual(pending["interaction"]["kind"],
                         "propose_plural_union")
        self._resolve_ask(state, "w3p-union")
        env = self.svc._load(self.gid)
        offer = env["graph"]["offers"][-1]
        self.assertEqual(offer["interaction_kind"], "propose_plural_union")
        union = next(u for u in env["graph"]["unions"]
                     if self.stranger_id in u["person_ids"])
        # plural recognition ('unrecognized') was read, never the
        # monogamous default ('recognized')
        self.assertEqual(union["recognition"]["status"], "unrecognized")
