"""M2.5 Phase 2 step 1 — v3 loader core (Kimi C).

Real-path tests against the FROZEN contracts (docs/collab/M2.5/contracts/):
- validate_event3 rejects the fit-proof invalid records for their stated
  reasons (minor-target romantic event, undeclared role, $0 no-exit);
- the valid fit-proof events validate and instantiate into the FROZEN
  M2 pending_decision shape plus additive v3 fields;
- bindings are concrete at instantiate and stored in the pending; a stale
  binding resolves to the safe explicit cancellation ('The moment has
  passed.'), never a crash, never a re-pick;
- domain effects resolve through the registry, refuse typed (with NO
  partial application) when the domain API is not registered yet;
- v2 files and v3 files coexist in one loader pass; v3 events never
  enter the v2 flavor/independent sampling pools.
"""

import json
import os
import random
import unittest

import src.menus.rule_actions as ra
from src.game.events import (CONTENT_DIR, EventEngine, EventValidationError,
                             load_event_files, validate_event3)
from tests.test_rules_income import make_player

FIT_PROOF = os.path.join(os.path.dirname(os.path.dirname(
    os.path.abspath(__file__))), "docs", "collab", "M2.5", "contracts",
    "v3-fit-proof.json")


def fit_proof():
    with open(FIT_PROOF, encoding="utf-8") as f:
        return json.load(f)


class _FakeService:
    """The walk surface A's predicates need from the service."""

    def __init__(self, player):
        self._root = player

    def _walk_people(self, player):
        out, stack, seen = [], [player], set()
        while stack:
            person = stack.pop()
            if person is None or id(person) in seen:
                continue
            seen.add(id(person))
            if hasattr(person, "person_id"):
                out.append(person)
            stack.extend(getattr(person, "relations", None) or [])
            for attr in ("mother", "father", "partner"):
                stack.append(getattr(person, attr, None))
        return out


class V3Context:
    """Duck-typed context for loader tests (C-side, no service): the
    loader surface (person_name/money/policy_revision), the rule-side
    people_by_id for resolve, and the envelope/service surface A's
    registered graph predicates consume."""

    def __init__(self, player, others=(), money=0, revision=0):
        self.player = player
        self.player_id = player.person_id
        self.people_by_id = {player.person_id: player}
        for person in list(getattr(player, "relations", None) or []):
            self.people_by_id[person.person_id] = person
        for person in others:
            self.people_by_id[person.person_id] = person
        self._money = money
        self.policy_revision = revision
        self._env = {
            "player": player, "archive": [], "dead_ids": [],
            "graph": {"relationships": [], "unions": [], "parentage": [],
                      "households": []},
        }
        self._service = _FakeService(player)

    def add_relationship(self, a, b, visibility="public", status="active"):
        edge = {"relationship_id": f"rel-{a[:8]}-{b[:8]}",
                "person_ids": [a, b], "status": status,
                "visibility": visibility}
        self._env["graph"]["relationships"].append(edge)
        return edge

    def add_union(self, a, b, status="active"):
        edge = {"union_id": f"uni-{a[:8]}-{b[:8]}",
                "person_ids": [a, b], "status": status}
        self._env["graph"]["unions"].append(edge)
        return edge

    def person_name(self, person_id):
        person = self.people_by_id.get(person_id)
        return person.name if person is not None else None

    def money(self):
        return self._money


class V3ValidationTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.bundle = fit_proof()

    def _valid(self, name):
        record = next(e for e in self.bundle["valid_events"]
                      if e["_name"] == name)
        return record["event"]["events"][0]

    def test_all_valid_fit_proof_events_validate(self):
        for record in self.bundle["valid_events"]:
            with self.subTest(record=record["_name"]):
                validate_event3(record["event"]["events"][0])

    def test_invalid_minor_target_romantic_rejected(self):
        ev = next(e for e in self.bundle["invalid_events"]
                  if e["_name"] == "minor_target_romantic_event")["event"]
        with self.assertRaises(EventValidationError) as ctx:
            validate_event3(ev["events"][0])
        self.assertIn("min_age", str(ctx.exception))

    def test_invalid_unknown_role_rejected(self):
        ev = next(e for e in self.bundle["invalid_events"]
                  if e["_name"] == "unknown_role_reference")["event"]
        with self.assertRaises(EventValidationError) as ctx:
            validate_event3(ev["events"][0])
        self.assertIn("ghost", str(ctx.exception))

    def test_invalid_no_free_exit_rejected(self):
        ev = next(e for e in self.bundle["invalid_events"]
                  if e["_name"] == "no_free_exit_soft_lock")["event"]
        with self.assertRaises(EventValidationError) as ctx:
            validate_event3(ev["events"][0])
        self.assertIn("soft-lock", str(ctx.exception))

    def test_romantic_interaction_requires_kind_and_bool(self):
        ev = self._valid("first_date_with_refusal_outcome")
        bad = dict(ev, interaction={"kind": "teen_dating", "romantic": True})
        with self.assertRaises(EventValidationError):
            validate_event3(bad)
        bad2 = dict(ev, interaction={"kind": "date"})
        with self.assertRaises(EventValidationError):
            validate_event3(bad2)

    def test_cost_and_effect_double_charge_rejected(self):
        ev = self._valid("wedding_occurrence")
        choices = json.loads(json.dumps(ev["choices"]))
        # give the free option both a cost gate and a money effect
        choices[3]["cost"] = {"money": 5}
        choices[3]["effects"] = {"money": -5}
        bad = dict(ev, choices=choices)
        with self.assertRaises(EventValidationError) as ctx:
            validate_event3(bad)
        self.assertIn("both charge", str(ctx.exception))

    def test_unknown_condition_leaf_rejected(self):
        ev = self._valid("first_date_with_refusal_outcome")
        bad = json.loads(json.dumps(ev))
        bad["conditions"]["all"].append({"kind": "eval_python"})
        with self.assertRaises(EventValidationError):
            validate_event3(bad)


class V3InstantiateTest(unittest.TestCase):
    def _event(self, name):
        bundle = fit_proof()
        record = next(e for e in bundle["valid_events"]
                      if e["_name"] == name)
        return record["event"]["events"][0]

    def test_pending_shape_frozen_plus_additive_fields(self):
        from tests.test_rules_m2b import partner_for
        player = make_player(age=30, money=500)
        partner_for(player)
        ctx = V3Context(player, money=500, revision=7)
        ev = self._event("first_date_with_refusal_outcome")
        pending = EventEngine.instantiate3(ev, 0, random.Random(5), ctx)
        # frozen M2 pending-decision projection
        for key in ("id", "kind", "event_id", "title", "text", "options"):
            self.assertIn(key, pending)
        for option in pending["options"]:
            for key in ("id", "label", "known_effects", "enabled",
                        "reason", "inputs"):
                self.assertIn(key, option)
        self.assertTrue(any(o.get("enabled", True) for o in pending["options"]))
        # v3 additive fields
        self.assertEqual(pending["schema_dialect"], "v3")
        self.assertEqual(pending["bindings"]["actor"], player.person_id)
        self.assertIsNotNone(pending["bindings"]["target"])
        self.assertEqual(pending["interaction"]["kind"], "date")
        self.assertEqual(pending["policy_revision"], 7)
        self.assertEqual(pending["audience"]["scope"], "player_known")

    def test_instantiation_is_seeded_reproducible(self):
        from tests.test_rules_m2b import partner_for
        player = make_player(age=30, money=500)
        partner_for(player)
        ctx = V3Context(player, money=500)
        ev = self._event("first_date_with_refusal_outcome")
        a = EventEngine.instantiate3(ev, 0, random.Random(9), ctx)
        b = EventEngine.instantiate3(ev, 0, random.Random(9), ctx)
        self.assertEqual(a, b)

    def test_unaffordable_option_marked_disabled_with_reason(self):
        from tests.test_rules_m2b import partner_for
        player = make_player(age=30, money=100)
        partner = partner_for(player, status=1)
        ctx = V3Context(player, money=100)
        ctx.add_union(player.person_id, partner.person_id)
        ev = self._event("wedding_occurrence")
        pending = EventEngine.instantiate3(ev, 0, random.Random(5), ctx)
        by_id = {o["id"].split("_", 2)[2]: o for o in pending["options"]}
        self.assertFalse(by_id["simple"]["enabled"])
        self.assertIn("costs", by_id["simple"]["reason"])
        self.assertTrue(by_id["postpone"]["enabled"])

    def test_required_role_that_does_not_bind_is_typed_error(self):
        player = make_player(age=30, money=500)
        player.relations = []
        ctx = V3Context(player, money=500)
        ev = self._event("first_date_with_refusal_outcome")
        with self.assertRaises(EventValidationError) as raised:
            EventEngine.instantiate3(ev, 0, random.Random(5), ctx)
        self.assertIn("did not bind", str(raised.exception))


class V3ResolveTest(unittest.TestCase):
    def _pending(self, name, seed=5, money=500):
        from tests.test_rules_m2b import partner_for
        bundle = fit_proof()
        record = next(e for e in bundle["valid_events"]
                      if e["_name"] == name)
        player = make_player(age=30, money=money)
        partner_for(player)
        ctx = V3Context(player, money=money)
        pending = EventEngine.instantiate3(record["event"]["events"][0],
                                           0, random.Random(seed), ctx)
        return player, ctx, pending

    def test_decline_option_resolves_without_domain(self):
        player, ctx, pending = self._pending(
            "first_date_with_refusal_outcome")
        option = next(o for o in pending["options"]
                      if o["id"].endswith("_decline"))
        result = ra.resolve_choice(ctx, pending, option["id"], {},
                                   random.Random(1))
        self.assertFalse(result["finished_turn"])
        self.assertEqual(result["money_entries"], [])
        self.assertTrue(result["events"][-1]["text"])

    def test_unregistered_domain_effect_typed_and_no_partial_application(self):
        player, ctx, pending = self._pending(
            "first_date_with_refusal_outcome")
        option = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        self.assertTrue(option["_domain"])  # chemistry + offer (registered)
        happy_before = player.happiness
        domain = dict(option["_domain"])
        option["_domain"] = {**domain, "__unknown_test_effect": {}}
        try:
            with self.assertRaises(ra.RuleError) as raised:
                ra.resolve_choice(ctx, pending, option["id"], {},
                                  random.Random(1))
            self.assertEqual(raised.exception.code, "not_available")
        finally:
            option["_domain"] = domain
        self.assertEqual(player.happiness, happy_before)  # nothing applied
        self.assertEqual(player.money, 500)

    def test_registered_domain_effect_applies_once(self):
        player, ctx, pending = self._pending(
            "first_date_with_refusal_outcome")
        calls = []

        def fake_chemistry(context, pl, bindings, value, rng):
            calls.append(value)
            target = context.people_by_id[bindings["target"]]
            target.change_relationship(10)
            return ([ra._event("decision", "We clicked.", target)], [])

        saved = {k: ra._DOMAIN_EFFECTS.get(k)
                 for k in ("chemistry", "offer")}
        ra.register_domain_effect("chemistry", fake_chemistry)
        option = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        try:
            # an unknown domain effect fails typed BEFORE anything
            # mutates (no partial application), even with real appliers
            # registered for the other effects
            domain = dict(option["_domain"])
            option["_domain"] = {**domain, "__unknown_test_effect": {}}
            with self.assertRaises(ra.RuleError):
                ra.resolve_choice(ctx, pending, option["id"], {},
                                  random.Random(1))
            self.assertEqual(calls, [])
            option["_domain"] = domain
            ra.register_domain_effect(
                "offer", lambda c, p, b, v, r: ([], []))
            result = ra.resolve_choice(ctx, pending, option["id"], {},
                                       random.Random(1))
            self.assertEqual(len(calls), 1)  # applied exactly once
            self.assertTrue(any("clicked" in e["text"]
                                for e in result["events"]))
        finally:
            option["_domain"] = {k: v for k, v in domain.items()}
            for k, v in saved.items():
                if v is None:
                    ra._DOMAIN_EFFECTS.pop(k, None)
                else:
                    ra._DOMAIN_EFFECTS[k] = v

    def test_stale_binding_resolves_to_safe_cancellation(self):
        player, ctx, pending = self._pending(
            "first_date_with_refusal_outcome")
        # Simulate the bound target vanishing from the graph (archived).
        stale_id = pending["bindings"]["target"]
        del ctx.people_by_id[stale_id]
        option = next(o for o in pending["options"]
                      if o["id"].endswith("_accept"))
        result = ra.resolve_choice(ctx, pending, option["id"], {},
                                   random.Random(1))
        self.assertEqual(len(result["events"]), 1)
        text = result["events"][0]["text"]
        # Meaning, not the exact wording: the chance lapsed and why.
        self.assertIn("let the chance slip by", text)
        self.assertIn("no longer possible", text)
        self.assertEqual(result["money_entries"], [])
        self.assertFalse(result["finished_turn"])


    def test_outcome_text_reperson_role_through_bindings(self):
        """Forced regression (A's parenting gap): {person:role} in
        outcome_text crashed with KeyError at resolve; it must render
        through the stored bindings like the offer text."""
        from tests.test_rules_m2b import partner_for
        player = make_player(age=30, money=500)
        target = partner_for(player)
        ev = {
            "id": "m25_outcome_role_test",
            "title": "Goodbye for now",
            "text": "{person:target} waves at {name}.",
            "interaction": {"kind": "date", "romantic": True},
            "roles": {
                "actor": {"kind": "person", "select": "player"},
                "target": {"kind": "person", "select": "predicate",
                           "predicate": "adult_non_related_stranger"},
            },
            "choices": [
                {"id": "part", "label": "Part ways", "cost": {},
                 "effects": {},
                 "outcome_text": "I said goodbye to {person:target}."},
                {"id": "stay", "label": "Stay", "cost": {},
                 "effects": {}, "outcome_text": "We stayed."},
            ],
        }
        ctx = V3Context(player, money=500)
        pending = EventEngine.instantiate3(ev, 0, random.Random(5), ctx)
        option = next(o for o in pending["options"]
                      if o["id"].endswith("_part"))
        result = ra.resolve_choice(ctx, pending, option["id"], {},
                                   random.Random(1))
        decision = result["events"][-1]
        self.assertIn(target.name, decision["text"])
        self.assertNotIn("{person:", decision["text"])


class V2V3CoexistenceTest(unittest.TestCase):
    def test_v2_content_still_loads_and_v3_files_validate(self):
        events = load_event_files(CONTENT_DIR)  # v2 + landed v3 content
        self.assertTrue(events)
        v2 = [ev for ev in events if ev.get("schema_version", 2) == 2]
        v3 = [ev for ev in events if ev.get("schema_version") == 3]
        self.assertTrue(v2, "v2 content must keep loading")
        self.assertTrue(v3, "landed v3 content must load and validate")

    def test_v3_events_never_enter_v2_sampling_pools(self):
        bundle = fit_proof()
        v3_events = [dict(r["event"]["events"][0], schema_version=3)
                     for r in bundle["valid_events"]]
        engine = EventEngine(v3_events)
        ctx = V3Context(make_player(age=30, money=0), money=0)
        for age in (18, 30, 45):
            self.assertIsNone(engine.sample(random.Random(age), age,
                                            set(), ctx))
            self.assertEqual(engine.sample_independent(
                random.Random(age), age, set(), ctx), [])

    def test_outcome_events_tagged_with_audience_and_fact(self):
        """Wire to A's journal filter convention (knowledge_journal_visible):
        every emitted event carries the pending's audience scope, plus
        fact_id when the occurrence declared a secret_fact. A player_known
        occurrence experienced firsthand (no fact) degrades to public."""
        from tests.test_rules_m2b import partner_for
        player = make_player(age=30, money=500)
        partner_for(player)
        base = {
            "id": "m25_audience_test",
            "title": "Secret date",
            "text": "{person:target} and {name} meet at midnight.",
            "interaction": {"kind": "date", "romantic": True},
            "roles": {
                "actor": {"kind": "person", "select": "player"},
                "target": {"kind": "person", "select": "predicate",
                           "predicate": "adult_non_related_stranger"},
            },
            "choices": [
                {"id": "meet", "label": "Meet them", "cost": {},
                 "effects": {},
                 "outcome_text": "We talked until dawn."},
                {"id": "skip", "label": "Stay home", "cost": {},
                 "effects": {}, "outcome_text": "An early night."},
            ],
        }
        ctx = V3Context(player, money=500)
        # private + secret_fact -> tagged private with fact_id
        ev = dict(base, audience={"scope": "private",
                                  "participants": ["actor", "target"]},
                  secret_fact="fact-secret-1")
        pending = EventEngine.instantiate3(ev, 0, random.Random(5), ctx)
        option = next(o for o in pending["options"]
                      if o["id"].endswith("_meet"))
        result = ra.resolve_choice(ctx, pending, option["id"], {},
                                   random.Random(1))
        for entry in result["events"]:
            self.assertEqual(entry["audience"], "private")
            self.assertEqual(entry["fact_id"], "fact-secret-1")
        # player_known without a fact -> public (firsthand knowledge)
        ev2 = dict(base, audience={"scope": "player_known",
                                   "participants": ["actor", "target"]})
        pending2 = EventEngine.instantiate3(ev2, 1, random.Random(5), ctx)
        option2 = next(o for o in pending2["options"]
                       if o["id"].endswith("_meet"))
        result2 = ra.resolve_choice(ctx, pending2, option2["id"], {},
                                    random.Random(1))
        for entry in result2["events"]:
            self.assertEqual(entry["audience"], "public")
            self.assertNotIn("fact_id", entry)


if __name__ == "__main__":
    unittest.main()
