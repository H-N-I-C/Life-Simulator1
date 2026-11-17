"""R9 forced regressions: secrecy integration. A secret relationship edge
stays hidden from the player's tree until a fact-backed disclosure; an
`affair` fact requires a REAL exclusive-agreement breach — consensual
open/plural arrangements are never affairs. Real GameService + resolver
paths."""
import json
import random
import tempfile
import unittest

from src.game.service import GameError, GameService


PROFILE = {"first_name": "Ada", "last_name": "Lee",
           "gender": "female", "country": "Australia", "seed": 99}


REVEAL_EVENT = {
    "id": "m25_test_caught_out",
    "title": "Seen at the cafe",
    "text": "Someone saw them together.",
    "interaction": {"kind": "date", "romantic": True},
    "roles": {
        "actor": {"kind": "person", "select": "player"},
        "the_partner": {"kind": "person", "select": "predicate",
                        "predicate": "adult_non_related_stranger"},
        "the_stranger": {"kind": "person", "select": "predicate",
                         "predicate": "adult_non_related_stranger"},
    },
    "choices": [
        {"id": "reveal", "label": "Confront them", "cost": {},
         "effects": {"reveal_fact": {
             "kind": "affair",
             "about": ["the_partner", "the_stranger"],
             "to": ["actor"]}},
         "outcome_text": "It all came out."},
        {"id": "walk", "label": "Say nothing", "cost": {},
         "effects": {}, "outcome_text": "I said nothing."},
    ],
    "audience": {"scope": "player_known",
                 "participants": ["actor", "the_partner", "the_stranger"]},
}


class SecrecyIntegrationTests(unittest.TestCase):
    def setUp(self):
        from src.appearance.integration import ensure_person_id
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        self.tmp = tempfile.mkdtemp(prefix="m25-r9-")
        self.svc = GameService(self.tmp)
        self.st = self.svc.new_game(dict(PROFILE), "r9-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        partner = Parent("Jordan", 32, Gender.Male)
        stranger = Parent("Riley", 33, Gender.Male)
        for p in (partner, stranger):
            ensure_person_id(p)
            env["player"].relations.append(p)
        self.svc._save(env)
        env = self.svc._load(self.gid)
        self.pid = env["player"].person_id
        self.partner_id = partner.person_id
        self.stranger_id = stranger.person_id
        # marriage (exclusive by default) + a SECRET relationship edge
        st = self.svc.get_state(self.gid)
        rec = {"status": "recognized", "jurisdiction_id": "jur-r9",
               "law_version": 1, "basis": "ceremony"}
        self.svc.graph_command(
            self.gid, st["revision"], "r9-union", "form_union",
            {"a_id": self.pid, "b_id": self.partner_id, "kind": "marriage",
             "recognition": rec, "turn": 0})
        env = self.svc._load(self.gid)
        from src.game.features.family_graph import transitions
        transitions.start_relationship_op(
            env, self.partner_id, self.stranger_id, "lover",
            {"satisfaction": 40, "trust": 30, "chemistry": 60}, 0,
            visibility="secret", exclusivity="exclusive")
        self.svc._save(env)

    # ----------------------------------------------------------- helpers
    def _secret_edges_in_tree(self):
        tree = self.svc.get_family_tree(self.gid)["tree"]
        return [e for e in tree["edges"]
                if e["kind"] == "relationship" and "secret" in e["flags"]]

    def _resolve_reveal(self):
        from src.game.events import EventEngine, GameContext
        from src.game.features.parenting.engine import _V3Context
        env = self.svc._load(self.gid)
        ctx = _V3Context(GameContext(env, self.svc))
        pending = EventEngine.instantiate3(
            REVEAL_EVENT, 11, random.Random(5), ctx,
            prebound={"the_partner": self.partner_id,
                      "the_stranger": self.stranger_id})
        pending["id"] = "d_r9_reveal"
        env["pending"] = pending
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        return self.svc.command(
            self.gid, st["revision"], "r9-req", "resolve_decision",
            {"decision_id": "d_r9_reveal",
             "option_id": next(o["id"] for o in pending["options"]
                               if o["id"].endswith("reveal"))})

    # -------------------------------------------------------------- tests
    def test_secret_edge_hidden_until_disclosure_then_visible(self):
        self.assertEqual(self._secret_edges_in_tree(), [])
        st = self._resolve_reveal()
        self.assertIsNotNone(st)
        edges = self._secret_edges_in_tree()
        self.assertEqual(len(edges), 1)
        self.assertEqual({edges[0]["from_id"], edges[0]["to_id"]},
                         {self.partner_id, self.stranger_id})
        # reload: the projection stays stable
        self.assertEqual(len(self._secret_edges_in_tree()), 1)
        # and the player holds a knowledge record for the affair fact
        env = self.svc._load(self.gid)
        self.assertTrue(any(f["kind"] == "affair"
                            for f in env["knowledge"]["facts"]))
        # production path mints exactly ONE fact for the occurrence
        # (detect_affair establishes it; reveal_fact reuses it)
        self.assertEqual(len([f for f in env["knowledge"]["facts"]
                              if f["kind"] == "affair"]), 1)

    def test_replayed_reveal_never_mints_a_second_fact(self):
        st = self._resolve_reveal()
        self.assertIsNotNone(st)
        # the exact retry (same request id, same payload) is answered by
        # the replay ledger; a second affair fact never appears
        again = self._resolve_reveal()
        self.assertIsNotNone(again)
        env = self.svc._load(self.gid)
        self.assertEqual(len([f for f in env["knowledge"]["facts"]
                              if f["kind"] == "affair"]), 1)

    def test_consensual_plural_is_not_an_affair(self):
        # renegotiate the marriage as OPEN: the same occurrence breaches
        # nothing, so no affair fact may be minted
        env = self.svc._load(self.gid)
        union = env["graph"]["unions"][0]
        self.assertEqual(union["exclusivity"]["agreement"], "exclusive")
        union["exclusivity"] = {"agreement": "open"}
        self.svc._save(env)
        with self.assertRaises(GameError) as ctx:
            self._resolve_reveal()
        self.assertEqual(ctx.exception.status, 409)
        env = self.svc._load(self.gid)
        self.assertFalse([f for f in env["knowledge"]["facts"]
                          if f["kind"] == "affair"])
        # consensual plural: the edge stays hidden too (no discovery fact)
        self.assertEqual(self._secret_edges_in_tree(), [])

    def test_open_secret_relationship_is_not_a_breach(self):
        # both relationships non-exclusive -> detect_affair says None
        from src.game.features.knowledge import drama
        env = self.svc._load(self.gid)
        env["graph"]["unions"][0]["exclusivity"] = {"agreement": "open"}
        for rel in env["graph"]["relationships"]:
            rel["exclusivity"] = {"agreement": "negotiated"}
        self.svc._save(env)
        env = self.svc._load(self.gid)
        self.assertFalse(drama.is_affair_breach(env, self.partner_id,
                                                self.stranger_id))
        self.assertIsNone(drama.detect_affair(env, self.partner_id,
                                              self.stranger_id, 1))

    def test_known_secret_ids_match_facts_not_world_truth(self):
        # an affair fact that NOBODY told the player about stays hidden:
        # engine truth alone never projects into the player's tree
        from src.game.features.knowledge import drama, core as knowledge
        env = self.svc._load(self.gid)
        fact_id = drama.detect_affair(env, self.partner_id,
                                      self.stranger_id, 1,
                                      occurrence_id="occ-r9")
        self.svc._save(env)
        self.assertIsNotNone(fact_id)
        self.assertEqual(self._secret_edges_in_tree(), [])
        # after the player learns it, the edge renders
        env = self.svc._load(self.gid)
        knowledge.learn(env, knowledge.PLAYER_OBSERVER, fact_id, 2, "told")
        self.svc._save(env)
        self.assertEqual(len(self._secret_edges_in_tree()), 1)


if __name__ == "__main__":
    unittest.main()
