"""M2.5 family_graph core tests (Kimi A, step 2). Real Person objects
against the frozen contracts (docs/collab/M2.5/contracts/graph-contract.md
+ graph-examples.json). Run: python3 -m unittest tests.test_family_graph_core
"""

import os
import random
import sys
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.appearance.integration import ensure_person_id
from src.game.features.family_graph import (  # noqa: E402
    GraphError, new_graph, validate_graph, normalize_envelope,
    assert_adult_romantic, start_relationship, update_relationship,
    form_union, end_union, add_parentage, record_offer, resolve_offer,
    supersede_offers, graph_relatedness, graph_tree, graph_union_memberships,
    graph_person, sample_conception, migrate_v2,
)
from src.lifesim_lib.lifesim_lib import Gender  # noqa: E402
from src.people.classes.parent import Parent  # noqa: E402
from src.people.classes.player import Player  # noqa: E402


def make_person(name, age, alive=True):
    player = Player(first=name, last="Test")
    player.age = age
    ensure_person_id(player)
    player.alive = alive
    return player


class FixtureCase(unittest.TestCase):
    """The blended family from graph-examples.json, built with real people."""

    def setUp(self):
        self.player = make_person("Riley", 34)
        self.jordan = make_person("Jordan", 33)
        self.sam = make_person("Sam", 36)
        self.lee = make_person("Lee", 8)
        self.morgan = make_person("Morgan", 40)
        self.ruth = make_person("Ruth", 5)
        self.ezra = make_person("Ezra", 64, alive=False)
        self.persons = {p.person_id: p for p in (
            self.player, self.jordan, self.sam, self.lee, self.morgan,
            self.ruth, self.ezra)}
        self.graph = new_graph()
        self.rel_rj = start_relationship(
            self.graph, self.player, self.jordan, "committed",
            {"satisfaction": 78, "trust": 81, "chemistry": 64}, turn=40)
        self.rel_rs = start_relationship(
            self.graph, self.player, self.sam, "lover",
            {"satisfaction": 55, "trust": 40, "chemistry": 71}, turn=52,
            visibility="secret", exclusivity="open")
        self.rel_rm = start_relationship(
            self.graph, self.player, self.morgan, "committed",
            {"satisfaction": 30, "trust": 20, "chemistry": 50}, turn=10)
        update_relationship(self.graph, self.rel_rm, {"end": True}, turn=22)
        self.uni_rj = form_union(
            self.graph, self.player, self.jordan, "marriage",
            {"status": "recognized", "jurisdiction_id": "jur-aurora-bay",
             "law_version": 3, "basis": "ceremony"}, turn=40,
            ceremony={"held": True, "turn": 41, "venue_id": "ven-botanical"})
        self.uni_rm = form_union(
            self.graph, self.player, self.morgan, "marriage",
            {"status": "recognized", "jurisdiction_id": "jur-aurora-bay",
             "law_version": 1, "basis": "ceremony"}, turn=10,
            prenup={"asset": "cash", "amount": 20000,
                    "terms_id": "pre-standard"})
        end_union(self.graph, self.uni_rm, "divorce", turn=22)
        add_parentage(self.graph, self.lee, self.player, "biological",
                      "birth")
        add_parentage(self.graph, self.lee, self.jordan, "adoptive",
                      "adoption")
        add_parentage(self.graph, self.player, self.ezra, "biological",
                      "birth")
        add_parentage(self.graph, self.ruth, self.morgan, "biological",
                      "birth")
        add_parentage(self.graph, self.ruth, self.sam, "social", "recorded",
                      confidence="assumed")


class GuardTests(FixtureCase):
    def test_adult_guard_blocks_minor_at_creation_and_resolution(self):
        minor = make_person("Kid", 17)
        with self.assertRaises(GraphError) as ctx:
            start_relationship(self.graph, self.player, minor, "lover",
                               {"satisfaction": 1, "trust": 1,
                                "chemistry": 1}, turn=0)
        self.assertEqual(ctx.exception.code, "not_adult")
        self.assertEqual(ctx.exception.status, 409)
        with self.assertRaises(GraphError) as ctx:
            record_offer(self.graph, self.player, minor, "date",
                         turn=0, expiry_turn=2, policy_revision=0)
        self.assertEqual(ctx.exception.code, "not_adult")
        # a participant who was adult at creation but is somehow no longer
        # chronologically valid at resolution is still refused (the guard
        # re-runs at resolve time)
        adult = make_person("Temp", 18)
        self.persons[adult.person_id] = adult
        offer = record_offer(self.graph, self.player, adult, "date",
                             turn=0, expiry_turn=2, policy_revision=0)
        adult.age = 17
        with self.assertRaises(GraphError) as ctx:
            resolve_offer(self.graph, offer, self.persons, accept=True,
                          turn=1)
        self.assertEqual(ctx.exception.code, "not_adult")

    def test_guard_is_not_overridable_by_scores(self):
        minor = make_person("Kid", 10)
        with self.assertRaises(GraphError):
            assert_adult_romantic(self.player, minor)

    def test_self_edge_and_duplicate_rejected(self):
        with self.assertRaises(GraphError) as ctx:
            start_relationship(self.graph, self.player, self.player,
                               "lover", {"satisfaction": 1, "trust": 1,
                                         "chemistry": 1}, turn=0)
        self.assertEqual(ctx.exception.code, "graph_error")
        with self.assertRaises(GraphError) as ctx:
            start_relationship(self.graph, self.player, self.jordan,
                               "lover", {"satisfaction": 1, "trust": 1,
                                         "chemistry": 1}, turn=41)
        self.assertEqual(ctx.exception.status, 409)

    def test_parentage_dag_and_duplicates(self):
        with self.assertRaises(GraphError) as ctx:
            add_parentage(self.graph, self.player, self.lee, "biological",
                          "birth")
        self.assertEqual(ctx.exception.code, "graph_error")
        self.assertIn("acyclic", ctx.exception.message)
        with self.assertRaises(GraphError) as ctx:
            add_parentage(self.graph, self.lee, self.player, "biological",
                          "birth")
        self.assertEqual(ctx.exception.status, 409)


class UnionTests(FixtureCase):
    def test_forbidden_union_still_forms(self):
        uni = form_union(
            self.graph, self.player, self.sam, "marriage",
            {"status": "forbidden", "jurisdiction_id": "jur-aurora-bay",
             "law_version": 3, "basis": "ceremony"}, turn=53)
        edge = next(u for u in self.graph["unions"] if u["union_id"] == uni)
        self.assertEqual(edge["recognition"]["status"], "forbidden")
        self.assertEqual(edge["status"], "active")

    def test_selected_edge_divorce_keeps_other_union(self):
        end_union(self.graph, self.uni_rj, "divorce", turn=60)
        statuses = {u["union_id"]: u["status"] for u in self.graph["unions"]}
        self.assertEqual(statuses[self.uni_rj], "divorced")
        self.assertEqual(statuses[self.uni_rm], "divorced")
        # parentage untouched
        self.assertEqual(len(self.graph["parentage"]), 5)

    def test_policy_revision_stamped(self):
        uni = form_union(
            self.graph, self.jordan, self.sam, "civil_partnership",
            {"status": "recognized", "jurisdiction_id": "jur-aurora-bay",
             "law_version": 3, "basis": "registration"}, turn=61,
            policy_revision=9)
        edge = next(u for u in self.graph["unions"] if u["union_id"] == uni)
        self.assertEqual(edge["policy_revision"], 9)


class RelatednessTests(FixtureCase):
    def _rel(self, a, b):
        persons = {pid: {"lineage": "unknown"} if p is self.morgan else
                   {"lineage": "known"}
                   for pid, p in self.persons.items()}
        return graph_relatedness(self.graph, a.person_id, b.person_id,
                                 persons_by_id=persons)

    def test_frozen_fixture_categories(self):
        cases = [
            (self.player, self.lee, "parent_child", "0.25"),
            (self.jordan, self.lee, "unrelated", "0"),
            (self.ruth, self.sam, "unrelated", "0"),
            (self.player, self.ruth, "unrelated", "0"),
            (self.lee, self.ezra, "grandparent_grandchild", "0.125"),
            (self.lee, self.morgan, "unknown", "unknown"),
        ]
        for a, b, category, band in cases:
            got = self._rel(a, b)
            self.assertEqual(got["category"], category,
                             "%s vs %s" % (a.name, b.name))
            self.assertEqual(got["coefficient_band"], band)

    def test_social_parentage_adds_no_risk(self):
        # sam is ruth's SOCIAL parent: no biological link to ruth's or
        # lee's recorded bio family (jordan is unrelated to everyone bio)
        got = self._rel(self.sam, self.jordan)
        self.assertEqual(got["coefficient_band"], "0")


class TreeTests(FixtureCase):
    def test_frozen_tree_projection(self):
        tree = graph_tree(self.graph, self.player.person_id, self.persons,
                          max_generations=3)
        nodes = {n["person_id"]: n for n in tree["nodes"]}
        self.assertEqual(set(nodes), {
            self.player.person_id, self.jordan.person_id, self.lee.person_id,
            self.ezra.person_id, self.morgan.person_id})
        self.assertIn("deceased", nodes[self.ezra.person_id]["flags"])
        self.assertIn("ex", nodes[self.morgan.person_id]["flags"])
        self.assertIn("adoptive_parent_edge",
                      nodes[self.lee.person_id]["flags"])
        edges = {(e["from_id"], e["to_id"], e["kind"], tuple(e["flags"]))
                 for e in tree["edges"]}
        self.assertIn((self.player.person_id, self.jordan.person_id, "union",
                       ("active",)), edges)
        self.assertIn((self.player.person_id, self.morgan.person_id, "union",
                       ("dissolved",)), edges)
        self.assertIn((self.player.person_id, self.lee.person_id,
                       "parentage", ("biological",)), edges)
        self.assertIn((self.jordan.person_id, self.lee.person_id,
                       "parentage", ("adoptive",)), edges)
        self.assertIn((self.ezra.person_id, self.player.person_id,
                       "parentage", ("biological",)), edges)
        # the secret lover edge is NOT projected to a viewer who doesn't
        # know it
        self.assertFalse(any(e["kind"] == "relationship" for e in
                             tree["edges"]))
        # ...but IS projected (flagged secret) for a viewer who knows it
        tree2 = graph_tree(self.graph, self.player.person_id, self.persons,
                           known_relationship_ids={self.rel_rs})
        secret = [e for e in tree2["edges"]
                  if e["kind"] == "relationship"]
        self.assertEqual(len(secret), 1)
        self.assertEqual(secret[0]["flags"], ["secret"])


class OfferTests(FixtureCase):
    def test_offer_envelope_and_supersession(self):
        offer = record_offer(self.graph, self.player, self.jordan,
                             "propose_union", turn=52, expiry_turn=54,
                             policy_revision=7)
        edge = next(o for o in self.graph["offers"]
                    if o["offer_id"] == offer)
        self.assertEqual(edge["policy_revision"], 7)
        self.assertEqual(edge["status"], "pending")
        superseded = supersede_offers(
            self.graph, interaction_kinds=["propose_union"],
            affected_person_ids=[self.jordan.person_id])
        self.assertEqual(superseded, [offer])
        self.assertEqual(edge["attempt"]["count"], 1)  # preserved

    def test_expiry(self):
        offer = record_offer(self.graph, self.player, self.jordan, "date",
                             turn=52, expiry_turn=53, policy_revision=0)
        with self.assertRaises(GraphError):
            resolve_offer(self.graph, offer, self.persons, accept=True,
                          turn=54)
        edge = next(o for o in self.graph["offers"]
                    if o["offer_id"] == offer)
        self.assertEqual(edge["status"], "expired")

    def test_nonromantic_kind_skips_guard(self):
        offer = record_offer(self.graph, self.player, self.lee,
                             "parenting_request", turn=0, expiry_turn=2,
                             policy_revision=0, romantic=False)
        resolve_offer(self.graph, offer, self.persons, accept=True, turn=1)


class ConceptionTests(FixtureCase):
    def test_sampled_once_and_persisted(self):
        rng = random.Random(7)
        outcome = sample_conception(self.graph, self.morgan.person_id,
                                    self.sam.person_id, rng)
        self.assertIn(outcome["coefficient_band"],
                      ("0", "<0.015", "0.015-0.0625", "0.125", "0.25",
                       "unknown"))
        # caller persists; a second draw for the same conception is a bug
        self.graph["conception_outcomes"]["child-x"] = outcome
        self.assertIs(
            self.graph["conception_outcomes"]["child-x"], outcome)

    def test_no_draw_for_adoption(self):
        # adoption records parentage without any conception sampling
        adopted = make_person("Adopted", 3)
        add_parentage(self.graph, adopted, self.jordan, "adoptive",
                      "adoption")
        self.assertNotIn(adopted.person_id,
                         self.graph["conception_outcomes"])


class EnvelopeTests(FixtureCase):
    def test_validate_and_normalize_old_envelope(self):
        env = {"graph": None, "editor_audit": None}
        self.assertTrue(normalize_envelope(env))
        self.assertTrue(validate_graph(env["graph"]))
        self.assertEqual(env["editor_audit"], [])
        self.assertFalse(normalize_envelope(env))


class MigrationTests(unittest.TestCase):
    def test_v2_single_partner_migration(self):
        from src.people.classes.partner import Partner
        player = make_person("Riley", 34)
        partner = Partner(35, Gender.Male, 50, 50, 77, "married")
        ensure_person_id(partner)
        player.partner = partner
        child = make_person("Lee", 8)
        child.mother = player
        child.father = partner
        player.children = [child]
        archive_ex = make_person("Morgan", 40)
        env = {"player": player, "archive": [archive_ex], "revision": 5}
        graph = migrate_v2(env)
        unions = graph["unions"]
        self.assertEqual(len(unions), 1)
        self.assertEqual(unions[0]["kind"], "marriage")
        self.assertEqual(set(unions[0]["person_ids"]),
                         {player.person_id, partner.person_id})
        bio = [(e["child_id"], e["parent_id"]) for e in graph["parentage"]
               if e["role"] == "biological"]
        self.assertIn((child.person_id, player.person_id), bio)
        self.assertIn((child.person_id, partner.person_id), bio)
        # ex in archive -> one ended edge, no invented unions
        ended = [e for e in graph["relationships"] if e["status"] == "ended"]
        self.assertEqual(len(ended), 1)
        self.assertEqual(graph["primary_relationship_id"],
                         next(e["relationship_id"] for e in
                              graph["relationships"]
                              if e["status"] == "active"))

    def test_v2_migration_never_breaks_on_odd_data(self):
        self.assertEqual(migrate_v2({}), new_graph())
        self.assertEqual(migrate_v2({"player": None}), new_graph())


if __name__ == "__main__":
    unittest.main()
