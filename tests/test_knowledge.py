"""M2.5 knowledge/drama/reputation tests (Kimi A) against C's frozen
reputation-knowledge.md + v3-fit-proof.json knowledge_fixtures.

Run: python3 -m unittest tests.test_knowledge
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import src.game.service  # noqa: E402,F401 (registrations)
from src.game.service import GameService  # noqa: E402
from src.game.features.knowledge import (  # noqa: E402
    new_knowledge_state, normalize_knowledge,
    mint_fact, get_fact, learn, knows, known_facts,
    reputation_get, reputation_delta, journal_entry_visible,
    PLAYER_OBSERVER,
)
from src.game.features.knowledge import drama  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 41}


def fresh_env(svc, gid):
    return svc._load(gid)


class FixtureSemanticsTests(unittest.TestCase):
    """The four knowledge_fixtures from v3-fit-proof.json, executable."""

    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-know-"))
        self.st = self.svc.new_game(dict(PROFILE), "know-create")
        self.gid = self.st["game_id"]
        self.env = fresh_env(self.svc, self.gid)

    def test_uninformed_npc_cannot_react(self):
        fact_id = mint_fact(self.env, "affair", ["pid-a", "pid-b"], 10)
        # engine-true fact is NOT NPC knowledge
        self.assertFalse(knows(self.env, "pid-c", fact_id))
        self.assertEqual(known_facts(self.env, "pid-c"), [])

    def test_witness_knowledge_record_created_once(self):
        fact_id = mint_fact(self.env, "affair", ["pid-a", "pid-b"], 10)
        learn(self.env, "pid-w", fact_id, 10, "witnessed", confidence=1.0)
        learn(self.env, "pid-w", fact_id, 12, "witnessed",
              confidence=0.5)  # re-witness: dedupe, keep max confidence
        records = [r for r in self.env["knowledge"]["records"]
                   if r["observer_id"] == "pid-w"]
        self.assertEqual(len(records), 1)
        self.assertEqual(records[0]["confidence"], 1.0)
        self.assertEqual(records[0]["learned_turn"], 10)
        self.assertTrue(knows(self.env, "pid-w", fact_id, 1.0))

    def test_reason_visibility_filtering(self):
        reasons_in = [
            {"code": "reputation", "contribution": -8,
             "label_key": "reason.reputation"},
            {"code": "faith_tenets", "contribution": -6,
             "label_key": "reason.faith_tenets"},
        ]
        out = drama.filter_reasons(
            self.env, reasons_in, tenet_visibility={"faith_tenets":
                                                    "public"})
        # reputation filtered (no known fact), faith kept (public);
        # scoring is untouched by the filter (display-only)
        self.assertEqual([r["code"] for r in out], ["faith_tenets"])
        self.assertEqual(sum(r["contribution"] for r in reasons_in), -14)

    def test_reputation_reason_shown_when_player_knows(self):
        fact_id = mint_fact(self.env, "affair", ["pid-a", "pid-b"], 10)
        reasons = [{"code": "reputation", "contribution": -8,
                    "label_key": "reason.reputation", "fact_id": fact_id}]
        self.assertEqual(drama.filter_reasons(self.env, reasons), [])
        learn(self.env, PLAYER_OBSERVER, fact_id, 11, "discovered")
        kept = drama.filter_reasons(self.env, reasons)
        self.assertEqual(len(kept), 1)

    def test_forgiveness_repairs_not_erases(self):
        env = self.env
        # a real relationship edge to patch
        from src.game.features import family_graph as fg
        people = {p.person_id: p for p in self.svc._walk_people(
            env["player"])}
        adults = [p for p in people.values() if p.age >= 18]
        a, b = adults[0], adults[1]
        rel_id = fg.start_relationship(
            env["graph"], a, b, "committed",
            {"satisfaction": 50, "trust": 20, "chemistry": 40}, turn=0)
        fact_id = mint_fact(env, "affair", [b.person_id, "pid-x"], 10)
        self.assertTrue(drama.forgive(env, self.svc, fact_id,
                                      a.person_id, 14))
        fact = get_fact(env, fact_id)
        self.assertEqual(fact["ended_turn"], 14)      # ended...
        self.assertIn(fact_id, [f["fact_id"]
                                for f in env["knowledge"]["facts"]])
        rel = fg.graph_relationship(env["graph"], rel_id)
        self.assertEqual(rel["trust"], 20 + drama.TRUST_REPAIR_ON_FORGIVENESS)
        learn(env, a.person_id, fact_id, 12, "told")
        self.assertTrue(knows(env, a.person_id, fact_id))  # record kept
        # forgiving again is a no-op
        self.assertFalse(drama.forgive(env, self.svc, fact_id,
                                       a.person_id, 15))


class AffairExpectationTests(unittest.TestCase):
    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-aff-"))
        self.st = self.svc.new_game(dict(PROFILE), "aff-create")
        self.gid = self.st["game_id"]
        env = fresh_env(self.svc, self.gid)
        env["player"].age = 30
        self.svc._save(env)
        self.env = fresh_env(self.svc, self.gid)
        people = {p.person_id: p for p in self.svc._walk_people(
            self.env["player"])}
        adults = [p for p in people.values() if p.age >= 18]
        self.a, self.b, self.c = adults[0], adults[1], adults[2] \
            if len(adults) > 2 else adults[0]

    def _rel(self, x, y, exclusivity="exclusive"):
        from src.game.features import family_graph as fg
        return fg.start_relationship(
            self.env["graph"], x, y, "committed",
            {"satisfaction": 50, "trust": 50, "chemistry": 50}, turn=0,
            exclusivity=exclusivity)

    def test_exclusive_breach_is_an_affair(self):
        self._rel(self.a, self.b, "exclusive")
        fact_id = drama.detect_affair(self.env, self.a.person_id,
                                      self.c.person_id, turn=5)
        self.assertIsNotNone(fact_id)
        fact = get_fact(self.env, fact_id)
        self.assertEqual(fact["kind"], "affair")
        self.assertEqual(set(fact["subject_ids"]),
                         {self.a.person_id, self.c.person_id})

    def test_consensual_plural_is_not_cheating(self):
        # open agreement: no expectation violated
        self._rel(self.a, self.b, "open")
        self.assertIsNone(drama.detect_affair(
            self.env, self.a.person_id, self.c.person_id, turn=5))
        # no relationship at all: nothing to breach
        env2 = fresh_env(self.svc, self.gid)
        people = {p.person_id: p for p in self.svc._walk_people(
            env2["player"])}
        adults = [p for p in people.values() if p.age >= 18]
        self.assertIsNone(drama.detect_affair(
            env2, adults[0].person_id, adults[1].person_id, turn=5))

    def test_reveal_fact_dedupes_reputation(self):
        fact_id = drama.reveal_fact(
            self.env, self.svc, "affair",
            [self.a.person_id, self.b.person_id],
            [self.c.person_id], turn=5, occurrence_id="occ-1",
            reputation=[{"person_id": self.a.person_id,
                         "scope": "global", "delta": -10}])
        self.assertEqual(reputation_get(self.env, self.a.person_id,
                                        "global"), -10)
        # same occurrence key re-applies nothing
        knowledge_state = self.env["knowledge"]
        key = "%s:%s:global:occ-1" % (fact_id, self.a.person_id)
        self.assertFalse(reputation_delta(self.env, self.a.person_id,
                                          "global", -10, key))
        self.assertEqual(reputation_get(self.env, self.a.person_id,
                                        "global"), -10)
        # bounded store
        reputation_delta(self.env, self.a.person_id, "global", -500,
                         "other-occ")
        self.assertEqual(reputation_get(self.env, self.a.person_id,
                                        "global"), -100)

    def test_disclose_adds_authorized_knowledge(self):
        fact_id = mint_fact(self.env, "affair", [self.a.person_id,
                                                 self.b.person_id], 5)
        drama.disclose_to(self.env, fact_id, self.a.person_id,
                          [self.c.person_id], turn=6)
        self.assertTrue(knows(self.env, self.c.person_id, fact_id))
        rec = next(r for r in self.env["knowledge"]["records"]
                   if r["observer_id"] == self.c.person_id)
        self.assertEqual(rec["source"], "told")


class ServerSideFilterTests(unittest.TestCase):
    def test_private_and_player_known_never_serialize(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-kf-"))
        st = svc.new_game(dict(PROFILE), "kf-create")
        gid = st["game_id"]
        env = svc._load(gid)
        env["journal"].append({
            "id": "jx1", "age": 0, "kind": "event", "text": "public one",
            "person_ids": [env["player"].person_id], "changes": {}})
        env["journal"].append({
            "id": "jx2", "age": 0, "kind": "event", "text": "secret one",
            "person_ids": [env["player"].person_id], "changes": {},
            "audience": "private"})
        fact_id = mint_fact(env, "secret_union", ["pid-a", "pid-b"], 0)
        env["journal"].append({
            "id": "jx3", "age": 0, "kind": "event",
            "text": "player-known one",
            "person_ids": [env["player"].person_id], "changes": {},
            "audience": "player_known", "fact_id": fact_id})
        svc._save(env)
        texts = [e["text"] for e in svc.get_state(gid)["journal"]]
        self.assertIn("public one", texts)
        self.assertNotIn("secret one", texts)
        self.assertNotIn("player-known one", texts)  # player doesn't know
        # once the player learns the fact, the entry serializes
        env = svc._load(gid)
        learn(env, PLAYER_OBSERVER, fact_id, 1, "discovered")
        svc._save(env)
        texts = [e["text"] for e in svc.get_state(gid)["journal"]]
        self.assertIn("player-known one", texts)


def _dra_events():
    from src.game import events
    return [e for e in events.load_event_files()
            if e["id"].startswith("dra_")]


class DramaContentTests(unittest.TestCase):
    def test_twenty_drama_events_validate(self):
        evs = _dra_events()
        self.assertEqual(len(evs), 20)

    def test_every_drama_event_has_a_free_option(self):
        for ev in _dra_events():
            free = [c for c in ev["choices"]
                    if (c.get("effects", {}).get("money", 0) >= 0
                        and not (c.get("cost") or {}).get("money"))]
            self.assertTrue(free, "%s lacks a free option" % ev["id"])

    def test_outcome_texts_first_person(self):
        for ev in _dra_events():
            for c in ev["choices"]:
                t = c["outcome_text"]
                self.assertFalse(t.startswith("You "),
                                 "%s/%s: %r" % (ev["id"], c["id"], t))

    def test_adult_binding_predicates_only(self):
        # every non-actor role must bind through a registered adult
        # predicate (18+/living gates in family_graph.predicates)
        from src.game import events as event_mod
        for ev in _dra_events():
            for name, cfg in (ev.get("roles") or {}).items():
                if cfg.get("select", "player") == "player":
                    continue
                self.assertIn(cfg["predicate"], event_mod.PREDICATES,
                              "%s:%s" % (ev["id"], name))

    def test_reveal_fact_effects_reference_declared_roles(self):
        for ev in _dra_events():
            person_roles = {n for n, c in (ev.get("roles") or {}).items()
                            if c.get("kind", "person") == "person"}
            for c in ev["choices"]:
                reveal = (c.get("effects") or {}).get("reveal_fact")
                if not reveal:
                    continue
                for field in ("about", "to"):
                    for role in reveal.get(field, []):
                        self.assertIn(role, person_roles,
                                      "%s/%s.%s" % (ev["id"], c["id"],
                                                    field))


class DramaServiceFlowTests(unittest.TestCase):
    """Real GameService: the drama phase offers occurrences; the caught
    event mints the affair fact, records the partner's knowledge and
    posts reputation once; the consensual check-in is not an affair."""

    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-dra-"))
        self.st = self.svc.new_game(dict(PROFILE), "dra-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        self.svc._save(env)

    def _form_union(self, other_role, req):
        from src.game.features import family_graph as fg
        env = self.svc._load(self.gid)
        people = {p.person_id: p for p in self.svc._walk_people(
            env["player"])}
        pid = env["player"].person_id
        adults = [p for p in people.values() if p.age >= 18
                  and p.person_id != pid]
        st = self.svc.get_state(self.gid)
        args = {"a_id": st["player_id"], "b_id": adults[0].person_id,
                "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "j",
                                "law_version": 1, "basis": "ceremony"},
                "turn": 0}
        return self.svc.graph_command(self.gid, st["revision"], req,
                                      "form_union", args)[
            "result"]["ids"]["union_id"], adults

    def _force(self, *ids):
        from src.game import events as event_mod
        from src.game.features.knowledge import engine as ke
        loaded = event_mod.load_event_files()
        ke._EVENT_CACHE = [e for e in loaded if e["id"] in ids]

    def _age_until(self, kind, max_iter=40):
        st = self.svc.get_state(self.gid)
        for i in range(max_iter):
            st = self.svc.command(self.gid, st["revision"], "dr-a%d" % i,
                                  "age_up", {})
            d = st["pending_decision"]
            if d and d["kind"] == kind:
                return st, d
            while d:
                free = [o for o in d["options"] if o["enabled"]]
                self.assertTrue(free, d["kind"])
                opt = free[-1]
                st = self.svc.command(
                    self.gid, st["revision"], "dr-d%d-%s" % (i, d["id"]),
                    "resolve_decision",
                    {"decision_id": d["id"], "option_id": opt["id"]})
                d = st["pending_decision"]
        self.fail("%s never fired" % kind)

    def _add_stranger(self):
        """An unrelated adult (no parentage edges to anyone)."""
        from src.appearance.integration import ensure_person_id
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        stranger = Parent("Riley", 32, Gender.Female)
        ensure_person_id(stranger)
        stranger.relationship = 40
        env["player"].relations.append(stranger)
        self.svc._save(env)

    def test_caught_late_mints_affair_and_partner_knowledge(self):
        self._form_union("mother", "dr-u1")
        self._add_stranger()
        self._force("dra_caught_late")
        st, d = self._age_until("dra_caught_late")
        bindings = d["bindings"]
        self.assertIn("partner", bindings)
        truth = next(o for o in d["options"] if o["id"].endswith("_truth"))
        st = self.svc.command(self.gid, st["revision"], "dr-tell",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": truth["id"]})
        env = self.svc._load(self.gid)
        facts = env["knowledge"]["facts"]
        affair = [f for f in facts if f["kind"] == "affair"]
        self.assertEqual(len(affair), 1)
        fact = affair[0]
        # partner (witness) and subjects know; player (a subject) knows
        from src.game.features.knowledge import knows, PLAYER_OBSERVER
        self.assertTrue(knows(env, bindings["partner"], fact["fact_id"]))
        self.assertTrue(knows(env, PLAYER_OBSERVER, fact["fact_id"]))
        # reputation posted exactly once for the truth option
        self.assertEqual(
            env["knowledge"]["reputation"][env["player"].person_id]
            ["global"], -6)

    def test_open_checkin_fires_and_is_not_an_affair(self):
        self._form_union("mother", "dr-u2")
        self._force("dra_open_checkin")
        st, d = self._age_until("dra_open_checkin")
        honest = next(o for o in d["options"]
                      if o["id"].endswith("_honest"))
        self.svc.command(self.gid, st["revision"], "dr-honest",
                         "resolve_decision",
                         {"decision_id": d["id"], "option_id": honest["id"]})
        env = self.svc._load(self.gid)
        self.assertEqual([f for f in env["knowledge"]["facts"]
                          if f["kind"] == "affair"], [])
        rels = env["graph"]["relationships"]
        self.assertEqual(
            [r for r in rels if r["kind"] == "affair"], [])

    def test_secret_pressure_go_public_mints_secret_union_fact(self):
        from src.game.features import family_graph as fg
        env = self.svc._load(self.gid)
        people = {p.person_id: p for p in self.svc._walk_people(
            env["player"])}
        pid = env["player"].person_id
        adults = [p for p in people.values() if p.age >= 18
                  and p.person_id != pid]
        rel_id = fg.start_relationship(
            env["graph"], env["player"], adults[0], "lover",
            {"satisfaction": 60, "trust": 50, "chemistry": 70}, turn=0,
            visibility="secret")
        self.svc._save(env)
        self._force("dra_secret_pressure")
        st, d = self._age_until("dra_secret_pressure")
        go_public = next(o for o in d["options"]
                         if o["id"].endswith("_public"))
        self.svc.command(self.gid, st["revision"], "dr-pub",
                         "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": go_public["id"]})
        env = self.svc._load(self.gid)
        facts = [f for f in env["knowledge"]["facts"]
                 if f["kind"] == "secret_union"]
        self.assertEqual(len(facts), 1)
        # the edge itself stays secret until A/C transitions change it;
        # the disclosure fact is the public record of what happened
        rel = fg.graph_relationship(env["graph"], rel_id)
        self.assertEqual(rel["visibility"], "secret")


if __name__ == "__main__":
    unittest.main()
