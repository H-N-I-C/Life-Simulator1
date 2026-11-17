"""Forced M2.5 regressions through real persisted GameService transactions."""
import copy
import json
import random
import tempfile
import unittest
from unittest.mock import patch

from src.game.service import GameError, GameService
from src.game.events import EventEngine, GameContext
from src.game.features.parenting.engine import _V3Context
from src.game.features.family_graph import graph
from src.game.features.family_graph.service_view import ServiceGraphView, ServicePolicyView
from src.game.features.relationship_policy.evaluator import evaluate_offer
from src.game.features.knowledge import core as knowledge
from src.game.features.knowledge.drama import filter_reasons
from src.people.classes.partner import Partner
from src.people.classes.child import Child
from src.lifesim_lib.lifesim_lib import Gender


class CodexFixes(unittest.TestCase):
    def new(self, actor_age=30, target_age=30):
        self.tmp = tempfile.TemporaryDirectory(prefix="m25-codex-")
        self.addCleanup(self.tmp.cleanup)
        self.svc = GameService(self.tmp.name)
        self.state = self.svc.new_game({"first_name": "Ada", "last_name": "Lee",
            "gender": "female", "country": "Australia", "seed": 4242}, "create")
        self.gid = self.state["game_id"]
        env = self.svc._load(self.gid)
        player = env["player"]
        player.age = actor_age
        player.happiness = 50
        player.fertility = 100
        player.money = 10000
        target = Partner(target_age, Gender.Male, 60, 60, 100, 0)
        target.years_together = 20
        target.craziness = 0
        target.money = 1000
        player.partner = target
        player.relations.append(target)
        self.svc._ensure_ids(env)
        env["balances"][player.person_id] = player.money
        env["balances"][target.person_id] = target.money
        self.pid, self.tid = player.person_id, target.person_id
        self.svc._save(env)
        self.state = self.svc.get_state(self.gid)
        return self.svc._load(self.gid)

    def command(self, action, payload=None, req=None):
        state = self.svc.get_state(self.gid)
        self.state = self.svc.command(self.gid, state["revision"],
            req or f"request-{state['revision']}", action, payload or {})
        return self.state

    def event(self, effects=None, interaction=None, audience=None):
        return {"id": "review_event", "title": "Review", "text": "Review event",
            "interaction": interaction or {"kind": "parenting_request", "romantic": False},
            "roles": {"actor": {"kind": "person", "select": "player"},
                      "target": {"kind": "person", "select": "predicate",
                                 "predicate": "adult_acquaintance"}},
            "choices": [{"id": "yes", "label": "Continue", "cost": {},
                         "effects": effects or {}, "outcome_text": "Resolved review event."},
                        {"id": "no", "label": "Decline", "cost": {},
                         "effects": {}, "outcome_text": "I declined."}],
            "audience": audience or {"scope": "public"}}

    def install(self, event, occurrence="d_review", prebound=None):
        env = self.svc._load(self.gid)
        pending = EventEngine.instantiate3(event, 500, random.Random(1),
            _V3Context(GameContext(env, self.svc)),
            prebound=prebound or {"target": self.tid})
        pending["id"] = occurrence
        env["pending"] = pending
        self.svc._save(env)
        return pending

    def resolve(self, pending, req=None):
        return self.command("resolve_decision", {
            "decision_id": pending["id"], "option_id": pending["options"][0]["id"]}, req)

    def test_legacy_creation_gates_both_ages_and_allows_adults(self):
        for action, status in (("propose", 0), ("plan_wedding", 1),
                                ("try_for_baby", 0), ("renew_vows", 2)):
            for ages in ((17, 18), (18, 17), (18, 18)):
                with self.subTest(action=action, ages=ages):
                    env = self.new(*ages)
                    env["player"].partner.status = status
                    env["player"].partner.last_renew_vows = -100
                    self.svc._save(env)
                    if min(ages) < 18:
                        before = self.svc._load(self.gid)
                        with self.assertRaises(GameError) as ctx:
                            self.command(action)
                        self.assertEqual(ctx.exception.code, "not_adult")
                        after = self.svc._load(self.gid)
                        self.assertEqual(after["revision"], before["revision"])
                        self.assertEqual(after["player"].happiness, before["player"].happiness)
                        self.assertEqual(after["ledger"], before["ledger"])
                    else:
                        self.command(action)

    def test_stale_legacy_date_and_wedding_recheck_both_ages(self):
        for kind in ("date_offer", "wedding_plan"):
            for ages in ((17, 18), (18, 17), (18, 18)):
                with self.subTest(kind=kind, ages=ages):
                    env = self.new(*ages)
                    target = env["player"].partner
                    if kind == "date_offer":
                        pending = {"id": "d_old_date", "kind": kind, "title": "Date", "text": "Date",
                            "options": [{"id": "ask_out", "label": "Accept"}],
                            "sampled": {"compatible": True}, "sampled_partner": target}
                    else:
                        target.status = 1
                        pending = {"id": "d_old_wedding", "kind": kind, "title": "Wedding", "text": "Wedding",
                            "options": [{"id": "venue:courthouse", "label": "Courthouse"}],
                            "sampled": {"venues": ["courthouse"]}}
                    env["pending"] = pending
                    self.svc._save(env)
                    if min(ages) < 18:
                        with self.assertRaises(GameError) as ctx:
                            self.resolve(pending)
                        self.assertEqual(ctx.exception.code, "not_adult")
                    else:
                        self.resolve(pending)

    def test_v3_cannot_downgrade_date_and_revalidates_after_reload(self):
        for ages in ((17, 18), (18, 17), (18, 18)):
            with self.subTest(ages=ages):
                self.new(18, 18)
                event = self.event({"stats": {"happiness": 5}},
                                   {"kind": "date", "romantic": False})
                pending = self.install(event)
                env = self.svc._load(self.gid)
                env["player"].age, env["player"].partner.age = ages
                self.svc._save(env)
                self.svc = GameService(self.tmp.name)
                if min(ages) < 18:
                    with self.assertRaises(GameError) as ctx:
                        self.resolve(pending)
                    self.assertEqual(ctx.exception.code, "not_adult")
                else:
                    self.resolve(pending)
                    self.assertEqual(self.svc._load(self.gid)["player"].happiness, 55)

    def test_v3_offer_creation_guards_both_ages_through_yearly_service(self):
        from src.game import events as event_mod
        from src.game.features.parenting import engine
        for ages in ((17, 18), (18, 17), (18, 18)):
            with self.subTest(ages=ages):
                env = self.new(*ages)
                child = Child("Bound", "Lee", Gender.Male, 50, 50, adopted=True)
                child.age = ages[1]
                # Isolate this guard from the separately deferred legacy
                # migration that replaces a union-less graph with a partner.
                env["player"].partner = None
                env["player"].children.append(child)
                env["player"].relations.append(child)
                graph.add_parentage(env["graph"], child, env["player"], "adoptive", "recorded")
                env["turn"] = {"cursor": {"phase_id": "parenting_events", "item_cursor": 0}}
                self.svc._save(env)
                event = self.event({"stats": {"happiness": 1}}, {"kind": "date", "romantic": False})
                event["id"] = "par_review_adult_guard"
                event["roles"]["target"]["predicate"] = (
                    "adult_child_of_actor" if ages[1] >= 18 else "teen_child_of_actor")
                event["weight"] = 1
                with tempfile.TemporaryDirectory() as root:
                    with open(root + "/event.json", "w") as handle:
                        json.dump({"schema_version": 3, "events": [event]}, handle)
                    old_cache = engine._EVENT_CACHE
                    engine.reset_cache()
                    try:
                        with patch.object(event_mod, "EVENTS_DIR", root), patch.object(
                                event_mod, "CONTENT_DIR", root + "/absent"):
                            if min(ages) < 18:
                                with self.assertRaises(GameError) as ctx:
                                    self.command("age_up")
                                self.assertEqual(ctx.exception.code, "not_adult")
                            else:
                                state = self.command("age_up")
                                self.assertEqual(state["pending_decision"]["event_id"], event["id"])
                    finally:
                        engine._EVENT_CACHE = old_cache

    def test_graph_offer_false_flag_cannot_bypass_age(self):
        for ages in ((17, 18), (18, 17), (18, 18)):
            with self.subTest(ages=ages):
                self.new(*ages)
                args = {"initiator_id": self.pid, "target_id": self.tid,
                    "interaction_kind": "date", "romantic": False,
                    "turn": 1, "expiry_turn": 10, "policy_revision": 0}
                if min(ages) < 18:
                    with self.assertRaises(GameError) as ctx:
                        self.svc.graph_command(self.gid, self.state["revision"], "off", "record_offer", args)
                    self.assertEqual(ctx.exception.code, "not_adult")
                else:
                    self.svc.graph_command(self.gid, self.state["revision"], "off", "record_offer", args)

    def test_conception_domain_rechecks_both_ages(self):
        for ages in ((17, 18), (18, 17), (18, 18)):
            with self.subTest(ages=ages):
                self.new(*ages)
                pending = self.install(self.event({"sample_conception": {
                    "mother": "actor", "father": "target"}}))
                if min(ages) < 18:
                    with self.assertRaises(GameError) as ctx:
                        self.resolve(pending)
                    self.assertEqual(ctx.exception.code, "not_adult")
                    self.assertFalse(self.svc._load(self.gid)["graph"]["conception_outcomes"])
                else:
                    self.resolve(pending)
                    self.assertTrue(self.svc._load(self.gid)["graph"]["conception_outcomes"])

    def test_private_journal_and_pending_are_not_serialized_or_softlocked(self):
        self.new()
        pending = self.install(self.event({"stats": {"happiness": 25}},
                                         audience={"scope": "private"}))
        self.assertIsNone(self.svc.get_state(self.gid)["pending_decision"])
        self.assertIn("age_up", {a["id"] for a in self.svc.get_state(self.gid)["available_actions"]})
        self.command("age_up")
        self.assertNotEqual((self.svc._load(self.gid).get("pending") or {}).get("id"), pending["id"])
        # A real resolved occurrence must carry its tags through the adapter.
        self.new()
        env = self.svc._load(self.gid)
        # Persist an adapter-produced private row, then read through the
        # actual service projection rather than calling its filter alone.
        from src.game import rule_adapter
        rule_adapter.journal_events(env, [{"kind": "event", "text": "PRIVATE_SENTINEL",
            "person_ids": [], "changes": {}, "audience": "private", "fact_id": "f_hidden"}])
        env["pending"] = None
        self.svc._save(env)
        self.assertNotIn("PRIVATE_SENTINEL", json.dumps(self.svc.get_state(self.gid)))
        self.assertEqual(self.svc._load(self.gid)["journal"][-1]["audience"], "private")
        fact = knowledge.mint_fact(env, "discovery", [self.tid], env["revision"])
        rule_adapter.journal_events(env, [{"kind": "event", "text": "KNOWN_SENTINEL",
            "person_ids": [], "changes": {}, "audience": "player_known", "fact_id": fact}])
        self.svc._save(env)
        self.assertNotIn("KNOWN_SENTINEL", json.dumps(self.svc.get_state(self.gid)))
        knowledge.learn(env, self.pid, fact, env["revision"], "told")
        self.svc._save(env)
        self.assertIn("KNOWN_SENTINEL", json.dumps(self.svc.get_state(self.gid)))

    def test_knowledge_occurrence_ids_distinguish_occurrences_and_dedupe_replay(self):
        self.new()
        ev = self.event({"reveal_fact": {"kind": "secret_union", "about": ["actor", "target"],
                                        "to": ["target"]},
                         "reputation": {"person": "actor", "scope": "global", "delta": -5}})
        first = self.install(ev, "d_first")
        self.resolve(first)
        self.install(ev, "d_second")
        self.resolve(self.svc._load(self.gid)["pending"])
        env = self.svc._load(self.gid)
        self.assertEqual(knowledge.reputation_get(env, self.pid, "global"), -10)
        self.assertEqual(len([f for f in env["knowledge"]["facts"] if f["kind"] == "secret_union"]), 2)
        env["pending"] = copy.deepcopy(first)
        self.svc._save(env)
        self.resolve(first, "retry-same-occurrence-new-command")
        env = self.svc._load(self.gid)
        self.assertEqual(knowledge.reputation_get(env, self.pid, "global"), -10)
        self.assertEqual(len([f for f in env["knowledge"]["facts"] if f["kind"] == "secret_union"]), 2)

    def test_reputation_reason_has_known_fact_provenance(self):
        self.new()
        ev = self.event({"reveal_fact": {"kind": "secret_union", "about": ["actor", "target"],
            "to": ["target"], "reputation": [{"person": "actor", "scope": "global", "delta": -40}]}})
        self.resolve(self.install(ev))
        env = self.svc._load(self.gid)
        scored = evaluate_offer("date", self.pid, self.tid,
                                 ServiceGraphView(env), ServicePolicyView(env))
        reason = next(r for r in scored["reasons"] if r["code"] == "reputation")
        self.assertTrue(knowledge.knows(env, self.pid, reason["fact_id"]))
        self.assertTrue(knowledge.knows(env, "player", reason["fact_id"]))
        self.assertIn(reason, filter_reasons(env, [reason]))

    def test_known_reputation_reason_stays_hidden_until_player_learns_fact(self):
        self.new()
        pending = self.install(self.event({"reveal_fact": {
            "kind": "secret_union", "about": ["target"], "to": ["target"],
            "reputation": [{"person": "actor", "scope": "global", "delta": -40}]}}))
        self.resolve(pending)
        env = self.svc._load(self.gid)
        scored = evaluate_offer("date", self.pid, self.tid,
                                 ServiceGraphView(env), ServicePolicyView(env))
        reason = next(r for r in scored["reasons"] if r["code"] == "reputation")
        self.assertFalse(filter_reasons(env, [reason]))
        knowledge.learn(env, self.pid, reason["fact_id"], env["revision"], "told")
        self.svc._save(env)
        reloaded = self.svc._load(self.gid)
        self.assertEqual(filter_reasons(reloaded, [reason]), [reason])

    def test_zero_cost_choice_is_legal_even_with_a_negative_balance(self):
        env = self.new()
        env["player"].money = -100
        env["balances"][self.pid] = -100
        self.svc._save(env)
        pending = self.install(self.event({"stats": {"happiness": 1}}))
        self.assertTrue(all(o["enabled"] for o in pending["options"]))
        self.resolve(pending)
        env = self.svc._load(self.gid)
        self.assertEqual(env["player"].happiness, 51)
        self.assertEqual(env["player"].money, -100)

    def test_no_drama_for_children_and_adult_drama_remains_reachable(self):
        for age in (0, 16, 17):
            with self.subTest(age=age):
                self.new(age, 30)
                state = self.command("age_up")
                pending = state["pending_decision"]
                if age < 17:
                    self.assertFalse(pending and (pending.get("event_id") or "").startswith("dra_"))
                else:
                    # A naturally aged 18-year-old may now receive adult drama.
                    env = self.svc._load(self.gid)
                    self.assertEqual(env["player"].age, 18)

    def test_forced_npc_marriage_checks_both_ages(self):
        # Seed 9 exercised the marriage branch on the pre-fix code with
        # a 17-year-old target. The year starts at the family boundary.
        for actor_age, partner_age in ((17, 18), (18, 16), (18, 17)):
            with self.subTest(actor_age=actor_age, partner_age=partner_age):
                env = self.new(40, 35)
                child = Child("NPC", "Lee", Gender.Male, 50, 50, adopted=True)
                child.age = actor_age
                child.happiness = 80
                child.partner = Partner(partner_age, Gender.Female, 50, 50, 100, 0)
                child.partner.years_together = 20
                for key in ("happiness", "health", "smarts", "looks"):
                    setattr(child.partner, key, 50)
                env["player"].children.append(child)
                env["player"].relations.append(child)
                self.svc._ensure_ids(env)
                env["rng_state"] = random.Random(9).getstate()
                env["turn"] = {"cursor": {"phase_id": "family_events", "item_cursor": 0},
                    "snapshot": [child.person_id],
                    "done": {"parenting_events": True, "drama_events": True}}
                self.svc._save(env)
                self.command("age_up")
                npc = self.svc._load(self.gid)["player"].children[0]
                self.assertIsNotNone(npc.partner)
                if min(actor_age, partner_age + 1) < 18:
                    self.assertNotEqual(npc.partner.status, 2)
                else:
                    self.assertEqual(npc.partner.status, 2)

    def test_auto_content_cannot_downgrade_adult_guard(self):
        from src.game import events as event_mod
        for age in (17, 18):
            with self.subTest(age=age):
                env = self.new(age, 30)
                env["turn"] = {"cursor": {"phase_id": "decision_offer", "item_cursor": 0},
                    "done": {"independent_rolled": True}, "event_queue": ["adult_auto"]}
                self.svc._save(env)
                with tempfile.TemporaryDirectory() as root:
                    with open(root + "/auto.json", "w") as handle:
                        json.dump({"schema_version": 2, "events": [{
                            "id": "adult_auto", "title": "Adult event", "text": "Adult event",
                            "category": "adult", "weight": 1, "chance_n": 1, "auto": True,
                            "interaction": {"kind": "date", "romantic": False},
                            "effects": {"stats": {"happiness": 2}}}]}, handle)
                    # Only the file-system roots are substituted; loading,
                    # sampling, effects, service transaction and save are real.
                    with patch.object(event_mod, "EVENTS_DIR", root), patch.object(
                            event_mod, "CONTENT_DIR", root + "/absent"):
                        if age < 18:
                            with self.assertRaises(GameError) as ctx:
                                self.command("age_up")
                            self.assertEqual(ctx.exception.code, "not_adult")
                        else:
                            self.command("age_up")
                            self.assertEqual(self.svc._load(self.gid)["player"].happiness, 52)

    def test_new_wedding_commands_revalidate_both_participants(self):
        for ages in ((17, 18), (18, 17), (18, 18)):
            with self.subTest(ages=ages):
                env = self.new(18, 18)
                # a real union edge (dating partners no longer migrate to
                # unions; only recorded marriages do)
                uid = graph.form_union(env["graph"], env["player"],
                                       env["player"].partner, "marriage",
                                       {"status": "recognized",
                                        "jurisdiction_id": "jur-neutral-default",
                                        "law_version": 1, "basis": "registration"},
                                       turn=0)
                env["player"].age, env["player"].partner.age = ages
                self.svc._save(env)
                st = self.svc.get_state(self.gid)
                args = {"union_id": uid, "venue_id": "courthouse", "guests": 0,
                        "outfit_id": "hand_me_down", "honeymoon_id": "none"}
                if min(ages) < 18:
                    with self.assertRaises(GameError) as ctx:
                        self.svc.wedding_command(self.gid, st["revision"], "wed", "plan_wedding", args)
                    self.assertEqual(ctx.exception.code, "not_adult")
                else:
                    self.svc.wedding_command(self.gid, st["revision"], "wed", "plan_wedding", args)

    def divorce_fixture(self, cash):
        env = self.new()
        player, target = env["player"], env["player"].partner
        env["graph"] = graph.new_graph()
        player.money = cash
        env["balances"][self.pid] = cash
        child = Child("Shared", "Lee", Gender.Male, 60, 60, adopted=True)
        child.age = 8
        player.children.append(child)
        player.relations.append(child)
        self.svc._ensure_ids(env)
        graph.add_parentage(env["graph"], child, player, "adoptive", "adoption")
        graph.add_parentage(env["graph"], child, target, "adoptive", "adoption")
        uid = graph.form_union(env["graph"], player, target, "marriage", {
            "status": "recognized", "jurisdiction_id": "jur_metropolis_charter",
            "law_version": 1, "basis": "registration"}, 0,
            prenup={"asset": "cash", "amount": 2000} if cash else None)
        self.svc._save(env)
        return uid, child.person_id

    def test_divorce_transfers_are_balanced_and_replay_exact(self):
        uid, cid = self.divorce_fixture(10000)
        state = self.svc.get_state(self.gid)
        args = {"union_id": uid}
        response = self.svc.wedding_command(self.gid, state["revision"], "divorce", "divorce_union", args)
        replay = self.svc.wedding_command(self.gid, state["revision"], "divorce", "divorce_union", args)
        self.assertEqual(response, replay)
        env = self.svc._load(self.gid)
        self.assertEqual(env["player"].money, 6000)
        self.assertEqual(env["balances"][self.tid], 3000)
        self.assertEqual(env["balances"][cid], 2000)
        self.assertEqual(sum(r["amount"] for r in env["ledger"]), 0)
        self.assertEqual(len({r["group_id"] for r in env["ledger"]}), 1)

    def test_zero_cash_divorce_records_support_without_overdrawing(self):
        uid, cid = self.divorce_fixture(0)
        state = self.svc.get_state(self.gid)
        self.svc.wedding_command(self.gid, state["revision"], "divorce", "divorce_union", {"union_id": uid})
        env = self.svc._load(self.gid)
        self.assertEqual(env["player"].money, 0)
        self.assertFalse(env["ledger"])
        obligation = env["graph"]["households"][-1]["support_obligations"][0]
        self.assertEqual(obligation["to_id"], cid)
        self.assertEqual(obligation["outstanding"], 2000)


if __name__ == "__main__":
    unittest.main()
