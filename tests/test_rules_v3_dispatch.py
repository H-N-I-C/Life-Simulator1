"""M2.5 fix round 2, R7 — ONE v3 dispatch/condition path.

`evaluate_v3_conditions` evaluates the frozen all/any/not tree with
policy/graph/knowledge leaves against the real envelope;
`EventEngine.sample_v3` is the single dispatch gate (seen-cadence + age
bounds + conditions + bindability + one weighted draw);
`rule_actions.romance_events_phase` offers one rom_* occurrence per year
through that path on a real persisted GameService, resolved through the
real command boundary. The coordinator wiring itself is A's two-line
hook (contracts README Open items) — these tests drive the phase through
`advance_phase`, the exact call the coordinator makes for RULE_PHASES.
"""

import json
import random
import tempfile
import unittest

import src.menus.rule_actions as ra
from src.game import events as event_mod
from src.game.context import RuleContext
from src.game.service import GameService
from src.people.classes.partner import Partner
from src.lifesim_lib.lifesim_lib import Gender

import src.game.features.family_graph.predicates  # noqa: F401 (registry)


def make_event(eid, conditions=None, predicate="adult_non_related_stranger",
               weight=1, optional=False):
    role = {"kind": "person", "select": "predicate",
            "predicate": predicate}
    if optional:
        role["optional"] = True
    return {
        "id": eid,
        "title": "T", "text": "T {person:target}",
        "interaction": {"kind": "date", "romantic": True},
        "conditions": conditions or {"min_age": 18},
        "roles": {"actor": {"kind": "person", "select": "player"},
                  "target": role},
        "choices": [{"id": "a", "label": "A", "cost": {}, "effects": {},
                     "outcome_text": "I did a."},
                    {"id": "b", "label": "B", "cost": {}, "effects": {},
                     "outcome_text": "I did b."}],
        "weight": weight,
        "schema_version": 3,
    }


class ServiceFixture(unittest.TestCase):
    """Real persisted GameService with an adult player, a partner and a
    stranger; graph backfilled from legacy state."""

    def new(self, age=30):
        self.tmp = tempfile.TemporaryDirectory(prefix="m25-r7-")
        self.addCleanup(self.tmp.cleanup)
        self.svc = GameService(self.tmp.name)
        st = self.svc.new_game({"first_name": "Ada", "last_name": "Lee",
                                "gender": "female", "country": "Australia",
                                "seed": 4242}, "create")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        player = env["player"]
        player.age = age
        player.money = 5000
        partner = Partner(29, Gender.Male, 60, 60, 100, 0)
        player.partner = partner
        player.relations.append(partner)
        stranger = Partner(31, Gender.Female, 60, 60, 100, 0)
        player.relations.append(stranger)
        self.svc._ensure_ids(env)
        env["balances"][player.person_id] = player.money
        from src.game.features.family_graph import graph as graph_mod
        if age >= 18:
            graph_mod.start_relationship(
                env["graph"], player, partner, "courtship",
                {"satisfaction": 50, "trust": 50, "chemistry": 50}, turn=1)
        self.svc._save(env)
        self.pid = player.person_id
        self.partner_id = partner.person_id
        self.stranger_id = stranger.person_id
        return env

    def ctx(self, env):
        return RuleContext(env, self.svc)

    def command(self, action, payload=None, req=None):
        state = self.svc.get_state(self.gid)
        return self.svc.command(self.gid, state["revision"],
                                req or f"r-{state['revision']}",
                                action, payload or {})


class V3ConditionTests(ServiceFixture):
    def _ctx_env(self):
        env = self.new()
        context = event_mod.GameContext(env, self.svc)
        people = context.people_by_id
        return env, context, people

    def test_all_any_not_tree(self):
        env, context, people = self._ctx_env()
        bindings = {"actor": self.pid, "target": self.stranger_id}
        base = {"min_age": 18}
        self.assertTrue(event_mod.evaluate_v3_conditions(
            dict(base, **{"all": [{"kind": "bound", "role": "target"},
                                  {"not": {"kind": "kinship", "role_a":
                                           "actor", "role_b": "target",
                                           "category": "parent_child"}}]}),
            context, bindings, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"any": [{"kind": "bound", "role": "missing"}]}, context,
            bindings, self.pid))
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"not": {"kind": "bound", "role": "missing"}}, context,
            bindings, self.pid))

    def test_min_age_gate(self):
        env, context, people = self._ctx_env()
        bindings = {"actor": self.pid}
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"min_age": 30}, context, bindings, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"min_age": 31}, context, bindings, self.pid))

    def test_relationship_and_union_state(self):
        env, context, people = self._ctx_env()
        graph = env["graph"]
        rel = next(r for r in graph["relationships"]
                   if set(r["person_ids"]) == {self.pid, self.partner_id})
        b = {"actor": self.pid, "partner": self.partner_id}
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "relationship_state", "role_a": "actor",
             "role_b": "partner", "chemistry": 0}, context, b, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "relationship_state", "role_a": "actor",
             "role_b": "partner", "chemistry": 101}, context, b, self.pid))
        rel["status"] = "ended"
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "relationship_state", "role_a": "actor",
             "role_b": "partner"}, context, b, self.pid))
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "expectation", "role_a": "actor", "role_b": "partner",
             "value": "exclusive"}, context, b, self.pid))
        rel["exclusivity"] = {"agreement": "open"}
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "expectation", "role_a": "actor", "role_b": "partner",
             "value": ["open", "negotiated"]}, context, b, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "expectation", "role_a": "actor", "role_b": "partner",
             "value": "exclusive"}, context, b, self.pid))
        # no union yet between actor and stranger
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "union_state", "role_a": "actor", "role_b": "target"},
            context, {"actor": self.pid, "target": self.stranger_id},
            self.pid))

    def test_kinship_and_recognition(self):
        env, context, people = self._ctx_env()
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "kinship", "role_a": "actor", "role_b": "target",
             "categories": ["unrelated", "unknown"]}, context,
            {"actor": self.pid, "target": self.stranger_id}, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "kinship", "role_a": "actor", "role_b": "target",
             "category": "parent_child"}, context,
            {"actor": self.pid, "target": self.stranger_id}, self.pid))
        union = {"union_id": "uni-x",
                 "person_ids": [self.pid, self.partner_id],
                 "status": "active", "kind": "marriage",
                 "recognition": {"status": "recognized"}}
        env["graph"]["unions"].append(union)
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "recognition_status", "role": "the_union",
             "status": ["recognized"]}, context,
            {"actor": self.pid, "the_union": "uni-x"}, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "recognition_status", "role": "the_union",
             "status": ["forbidden"]}, context,
            {"actor": self.pid, "the_union": "uni-x"}, self.pid))

    def test_policy_leaves(self):
        env, context, people = self._ctx_env()
        env["policy"].setdefault("profiles", {})[self.stranger_id] = {
            "faith_id": "faith_sun", "openness": 0.9,
            "monogamy_preference": 0.2, "attraction_to_initiator": 0.1}
        b = {"actor": self.pid, "target": self.stranger_id}
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "faith_id", "role": "target", "value": "faith_sun"},
            context, b, self.pid))
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "openness_band", "role": "target", "band": "high"},
            context, b, self.pid))
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "attraction_band", "role": "target", "band": "low"},
            context, b, self.pid))
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "monogamy_band", "role": "target",
             "band": ["low", "mid"]}, context, b, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "openness_band", "role": "target", "band": "low"},
            context, b, self.pid))
        env["policy"]["jurisdiction_id"] = "jur_test"
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "jurisdiction_id", "value": "jur_test"},
            context, b, self.pid))
        # tenet stance via B's published predicate fn
        self.assertIn(event_mod._v3_node(
            {"kind": "tenet_stance", "role": "target",
             "tenet_id": "conversion_out", "stance": ["favoured",
                                                     "neutral",
                                                     "disfavoured"]},
            context, b, people, self.pid), (True,))

    def test_knowledge_leaves(self):
        from src.game.features.knowledge import core as knowledge
        env, context, people = self._ctx_env()
        knowledge.normalize_knowledge(env)
        b = {"actor": self.pid, "target": self.stranger_id}
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "known_fact", "observer": "actor",
             "fact_kind": "affair"}, context, b, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "reputation_at_least", "role": "target",
             "observer": "actor", "value": 1}, context, b, self.pid))
        # mint a real fact + knowledge + reputation, then both leaves flip
        from src.game.features.knowledge import drama as knowledge_drama
        knowledge_drama.reveal_fact(
            env, self.svc, "affair", [self.pid, self.partner_id],
            [self.stranger_id], 1, "occ-1")
        fact_id = next(iter(knowledge.known_facts(env, self.stranger_id)))
        knowledge_drama.disclose_to(env, fact_id, self.stranger_id,
                                    [self.pid], 2)
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "known_fact", "observer": "actor",
             "fact_kind": "affair"}, context, b, self.pid))
        # reputation is fact-backed (reputation_as_known), not the raw
        # store: assert around the ACTUAL known value
        env["knowledge"].setdefault("reputation", {})[
            self.stranger_id] = {"global": -5}
        known = knowledge.reputation_as_known(env, self.stranger_id,
                                              "global", self.pid)
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "reputation_at_least", "role": "target",
             "observer": "actor", "value": known}, context, b, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "reputation_at_least", "role": "target",
             "observer": "actor", "value": known + 1}, context, b,
            self.pid))

    def test_milestone_leaf(self):
        env, context, people = self._ctx_env()
        env.setdefault("parenting", {}).setdefault("milestones", {})[
            self.stranger_id] = {"first_steps": {"turn": 1}}
        b = {"actor": self.pid, "child": self.stranger_id}
        self.assertTrue(event_mod.evaluate_v3_conditions(
            {"kind": "milestone", "role": "child", "id": "first_steps"},
            context, b, self.pid))
        self.assertFalse(event_mod.evaluate_v3_conditions(
            {"kind": "milestone", "role": "child", "id": "graduation"},
            context, b, self.pid))

    def test_unknown_leaf_is_a_typed_error(self):
        env, context, people = self._ctx_env()
        with self.assertRaises(event_mod.EventValidationError):
            event_mod.evaluate_v3_conditions(
                {"kind": "nonsense"}, context, {"actor": self.pid},
                self.pid)


class V3DispatchPathTests(ServiceFixture):
    def test_seen_cadence_and_weighted_draw(self):
        env = self.new()
        context = event_mod.GameContext(env, self.svc)
        engine = event_mod.EventEngine([
            make_event("rom_a", weight=1),
            make_event("rom_b", weight=3),
            make_event("dra_x", weight=100),
            make_event("rom_child_only", conditions={"min_age": 1}),
        ])
        # prefix filter: only rom_* considered
        for seed in range(5):
            rng = random.Random(seed)
            ev, b = engine.sample_v3(rng, 30, [], context, prefixes=("rom_",),
                                     actor_id=self.pid)
            self.assertIsNotNone(ev)
            self.assertTrue(ev["id"].startswith("rom_"))
            self.assertEqual(b["actor"], self.pid)
            self.assertIsNotNone(b["target"])
        # seen cadence: seen events are never drawn again
        ev, _ = engine.sample_v3(
            random.Random(1), 30,
            ["rom_a", "rom_b", "rom_child_only"], context,
            prefixes=("rom_",), actor_id=self.pid)
        self.assertIsNone(ev)

    def test_conditions_gate_the_draw(self):
        env = self.new()
        context = event_mod.GameContext(env, self.svc)
        # partner-only event cannot bind its partner role for the
        # stranger predicate? partner predicate binds; a kinship-banned
        # event against a related person must drop out
        related = make_event("rom_related_gate")
        related["conditions"] = {"min_age": 18, "all": [
            {"kind": "kinship", "role_a": "actor", "role_b": "target",
             "category": "parent_child"}]}
        engine = event_mod.EventEngine([related])
        ev, _ = engine.sample_v3(random.Random(1), 30, [], context,
                                 prefixes=("rom_",), actor_id=self.pid)
        self.assertIsNone(ev)  # stranger is unrelated → gate closes

    def test_age_gate(self):
        env = self.new()
        context = event_mod.GameContext(env, self.svc)
        engine = event_mod.EventEngine([make_event("rom_a")])
        ev, _ = engine.sample_v3(random.Random(1), 17, [], context,
                                 prefixes=("rom_",), actor_id=self.pid)
        self.assertIsNone(ev)


class RomancePhaseTests(ServiceFixture):
    def _run_phase(self, env):
        return ra.advance_phase(self.ctx(env), "romance_events",
                                random.Random(5))

    def test_phase_offers_one_rom_occurrence_and_marks_seen(self):
        env = self.new()
        result = self._run_phase(env)
        pending = result["pending_decision"]
        self.assertIsNotNone(pending)
        self.assertEqual(pending["schema_dialect"], "v3")
        self.assertTrue(pending["event_id"].startswith("rom_"))
        self.assertIn(pending["event_id"], env["v3_seen"])
        # second invocation offers at most one occurrence per year: the
        # first event is spent, another rom_* event may be next year's
        again = self._run_phase(env)
        if again["pending_decision"] is not None:
            self.assertNotEqual(again["pending_decision"]["event_id"],
                                pending["event_id"])
            self.assertIn(again["pending_decision"]["event_id"],
                          env["v3_seen"])

    def test_phase_offers_nothing_for_children(self):
        env = self.new(age=17)
        result = self._run_phase(env)
        self.assertIsNone(result["pending_decision"])
        self.assertEqual(result["continuation"]["phase_id"], "illness")

    def test_phase_pending_resolves_through_real_service(self):
        env = self.new()
        result = self._run_phase(env)
        pending = result["pending_decision"]
        # the coordinator stores a phase's pending into the envelope
        env["pending"] = pending
        pending["id"] = f"d_{env['next_decision_num']}"
        env["next_decision_num"] += 1
        self.svc._save(env)
        option = pending["options"][0]
        state = self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": option["id"]},
            req="r7-resolve")
        self.assertFalse(state.get("pending_decision"))
        self.assertTrue(state["journal"])


if __name__ == "__main__":
    unittest.main()


class E6SharedConditionPathTests(ServiceFixture):
    """E6 (fix round 2): A's drama and parenting engines must route
    eligibility through C's shared v3 condition path — min_age-gated
    content is never served to the wrong age (previously the engines
    probed bindability only and ignored conditions entirely)."""

    def _event(self, eid, min_age, child_role=False):
        roles = {"actor": {"kind": "person", "select": "player"}}
        if child_role:
            roles["child"] = {
                "kind": "person", "select": "predicate",
                "predicate": "dependent_child_of_actor"}
            interaction = {"kind": "parenting_request", "romantic": False}
        else:
            roles["target"] = {
                "kind": "person", "select": "predicate",
                "predicate": "adult_acquaintance"}
            interaction = {"kind": "repair_attempt", "romantic": True}
        return {
            "id": eid, "title": "T", "text": "T",
            "interaction": interaction,
            "conditions": {"min_age": min_age},
            "roles": roles,
            "choices": [{"id": "a", "label": "A", "cost": {}, "effects": {},
                         "outcome_text": "I did a."}],
            "weight": 1, "schema_version": 3,
        }

    def test_drama_engine_conditions_gate_eligibility(self):
        from src.game.features.knowledge import engine as drama
        saved = drama._EVENT_CACHE
        try:
            env = self.new()
            drama._EVENT_CACHE = [self._event("dra_min99", 99)]
            result = drama.advance_drama_phase(env, None, self.svc,
                                               random.Random(1))
            self.assertIsNone(result["pending_decision"])
            drama._EVENT_CACHE = [self._event("dra_ok", 30)]
            result = drama.advance_drama_phase(env, None, self.svc,
                                               random.Random(1))
            self.assertIsNotNone(result["pending_decision"])
            self.assertEqual(result["pending_decision"]["event_id"],
                             "dra_ok")
        finally:
            drama._EVENT_CACHE = saved

    def test_parenting_engine_conditions_gate_eligibility(self):
        from src.game.features.parenting import engine as parenting
        from src.game.features.parenting import core as pcore
        saved = parenting._EVENT_CACHE
        try:
            env = self.new()
            from src.people.classes.child import Child
            from src.game.features.family_graph import graph as graph_mod
            child = Child("Kid", "Lee", Gender.Male, 50, 50)
            env["player"].children.append(child)
            env["player"].relations.append(child)
            self.svc._ensure_ids(env)
            env["balances"][child.person_id] = 0
            graph_mod.add_parentage(env["graph"], child.person_id,
                                    self.pid, "biological", "birth")
            self.svc._save(env)
            pcore.backfill_all(env, self.svc)
            parenting._EVENT_CACHE = [self._event(
                "par_min99", 99, child_role=True)]
            result = parenting.advance_parenting_phase(
                env, None, self.svc, random.Random(1))
            self.assertIsNone(result["pending_decision"])
            parenting._EVENT_CACHE = [self._event(
                "par_ok", 30, child_role=True)]
            result = parenting.advance_parenting_phase(
                env, None, self.svc, random.Random(1))
            self.assertIsNotNone(result["pending_decision"])
            self.assertEqual(result["pending_decision"]["event_id"],
                             "par_ok")
        finally:
            parenting._EVENT_CACHE = saved


if __name__ == "__main__":
    unittest.main()
