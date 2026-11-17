"""M2.5 service-adapter tests (Kimi A): policy envelope, profile stamping,
rename forwarding, supersession hook, GraphView backing. Real GameService.

Run: python3 -m unittest tests.test_family_graph_service
"""

import os
import pickle
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402
from src.game.features import family_graph as fg  # noqa: E402
from src.game.features.family_graph.service_view import (  # noqa: E402
    ServiceGraphView, ServicePolicyView,
)

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}


class PolicyEnvelopeTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m25-svc-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "svc-create")
        self.gid = self.st["game_id"]

    def test_policy_state_created_and_stamped(self):
        pol = self.svc.get_policy_state(self.gid)
        self.assertIn("profiles", pol)
        self.assertIn("policy_revision", pol)
        # every initial person has a stamped profile
        people = self.svc.get_state(self.gid)["people"]
        self.assertEqual(set(pol["profiles"]),
                         {p["id"] for p in people})

    def test_old_save_without_policy_migrates(self):
        path = self.svc._path(self.gid)
        env = self.svc._read_save(path)
        del env["policy"]
        with open(path, "wb") as f:
            pickle.dump(env, f)
        pol = self.svc.get_policy_state(self.gid)
        self.assertIn("profiles", pol)
        # persisted: a second load does not re-migrate
        env2 = self.svc._read_save(path)
        self.assertIn("policy", env2)

    def test_stamping_is_deterministic_and_idempotent(self):
        env = self.svc._load(self.gid)
        people = list(self.svc._walk_people(env["player"]))
        first = self.svc.stamp_person_profile(env, people[0])
        second = self.svc.stamp_person_profile(env, people[0])
        self.assertEqual(first, second)


class EditorApplyTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m25-ed-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "ed-create")
        self.gid = self.st["game_id"]

    def test_rename_forwarding_and_identity(self):
        pid = self.st["player_id"]
        res = self.svc.editor_apply(
            self.gid, self.st["revision"], "ed-1",
            [{"name": "rename_person", "scope": "person",
              "target_id": pid, "patch": {"first_name": "Alexis"}}])
        self.assertEqual(res["revision"], self.st["revision"] + 1)
        self.assertEqual(res["policy_revision"], 0)  # not shared policy
        st = self.svc.get_state(self.gid)
        person = next(p for p in st["people"] if p["id"] == pid)
        self.assertTrue(person["name"].startswith("Alexis"))
        self.assertEqual(person["id"], pid)
        env = self.svc._load(self.gid)
        self.assertEqual(len(env["editor_audit"]), 1)
        self.assertEqual(env["editor_audit"][0]["command"], "rename_person")

    def test_retry_is_replay_exact(self):
        pid = self.st["player_id"]
        cmd = [{"name": "rename_person", "scope": "person",
                "target_id": pid, "patch": {"first_name": "Alexis"}}]
        res = self.svc.editor_apply(self.gid, self.st["revision"], "ed-1",
                                    cmd)
        retry = self.svc.editor_apply(self.gid, self.st["revision"], "ed-1",
                                      cmd)
        self.assertEqual(retry, res)
        # nothing applied twice
        env = self.svc._load(self.gid)
        self.assertEqual(len(env["editor_audit"]), 1)

    def test_stale_revision_rejected(self):
        with self.assertRaises(GameError) as ctx:
            self.svc.editor_apply(self.gid, 999, "ed-2",
                                  [{"name": "rename_person",
                                    "scope": "person",
                                    "target_id": self.st["player_id"],
                                    "patch": {"first_name": "Zed"}}])
        self.assertEqual(ctx.exception.code, "stale_revision")

    def test_preview_does_not_mutate(self):
        pid = self.st["player_id"]
        before = self.svc.get_state(self.gid)["revision"]
        self.svc.editor_preview(
            self.gid, [{"name": "rename_person", "scope": "person",
                        "target_id": pid, "patch": {"first_name": "Zed"}}])
        after = self.svc.get_state(self.gid)["revision"]
        self.assertEqual(before, after)
        st = self.svc.get_state(self.gid)
        self.assertTrue(next(p for p in st["people"]
                             if p["id"] == pid)["name"].startswith("Alex"))


class SupersessionHookTests(unittest.TestCase):
    def test_policy_apply_supersedes_matching_offers(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-sup-"))
        st = svc.new_game(dict(PROFILE), "sup-create")
        gid = st["game_id"]
        env = svc._load(gid)
        adults = [p for p in svc._walk_people(env["player"]) if p.age >= 18]
        self.assertGreaterEqual(len(adults), 2)
        offer = fg.record_offer(env["graph"], adults[0], adults[1],
                                "propose_union", turn=env["revision"],
                                expiry_turn=env["revision"] + 5,
                                policy_revision=0)
        svc._save(env)
        res = svc.editor_apply(
            gid, svc.get_state(gid)["revision"], "sup-1",
            [{"name": "edit_jurisdiction", "scope": "jurisdiction",
              "target_id": "jur_metropolis_charter", "patch": {}}])
        self.assertEqual(res["policy_revision"], 1)
        env2 = svc._load(gid)
        off = next(o for o in env2["graph"]["offers"]
                   if o["offer_id"] == offer)
        self.assertEqual(off["status"], "superseded")
        self.assertEqual(off["attempt"]["count"], 1)


class FamilyTreeEndpointTests(unittest.TestCase):
    def test_tree_includes_parents_with_appearances(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-tree-"))
        st = svc.new_game(dict(PROFILE), "tree-create")
        out = svc.get_family_tree(st["game_id"])
        tree = out["tree"]
        names = {n["name"] for n in tree["nodes"]}
        self.assertEqual(len(names), 3)  # player + two parents
        self.assertEqual(len(tree["edges"]), 2)
        self.assertTrue(all(e["kind"] == "parentage" for e in tree["edges"]))
        self.assertEqual(set(out["appearances"]),
                         {n["person_id"] for n in tree["nodes"]})

    def test_unknown_focus_rejected(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-tf-"))
        st = svc.new_game(dict(PROFILE), "tf-create")
        with self.assertRaises(GameError) as ctx:
            svc.get_family_tree(st["game_id"], focus_id="nope")
        self.assertEqual(ctx.exception.status, 404)


class GraphViewTests(unittest.TestCase):
    def test_service_graph_view_backs_evaluator_interface(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-gv-"))
        st = svc.new_game(dict(PROFILE), "gv-create")
        env = svc._load(st["game_id"])
        view = ServiceGraphView(env)
        people = list(svc._walk_people(env["player"]))
        person = view.person(people[0].person_id)
        self.assertIn(person["gender"], ("male", "female", ""))
        self.assertIn("is_adult", person)
        self.assertEqual(view.relatedness_category(people[0].person_id,
                                                   people[0].person_id),
                         "self")
        self.assertIsInstance(view.active_union_count(people[0].person_id),
                              int)
        with self.assertRaises(Exception):
            view.person("nope")

    def test_service_policy_view(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-pv-"))
        st = svc.new_game(dict(PROFILE), "pv-create")
        env = svc._load(st["game_id"])
        view = ServicePolicyView(env)
        self.assertEqual(view.jurisdiction_id(),
                         env["policy"]["jurisdiction_id"])
        self.assertIn(st["player_id"], env["policy"]["profiles"])


class V2MigrationIntegrationTests(unittest.TestCase):
    def test_real_v2_family_migrates_on_load(self):
        from src.appearance.integration import ensure_person_id
        from src.lifesim_lib.lifesim_lib import Gender
        from src.people.classes.partner import Partner
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-mig-"))
        st = svc.new_game(dict(PROFILE), "mig-create")
        gid = st["game_id"]
        # simulate a real M2 married family: partner + recorded child
        env = svc._load(gid)
        env["player"].age = 30  # a real married M2 save has an adult player
        partner = Partner(33, Gender.Female, 50, 50, 80, 2)
        ensure_person_id(partner)
        env["player"].partner = partner
        svc._save(env)
        # wipe the M2.5 graph AND the migration marker, as a pre-M2.5
        # save would have
        from src.game.features.family_graph import new_graph
        env = svc._load(gid)
        env["graph"] = new_graph()
        svc._save(env)
        # load: migration runs exactly once
        out = svc.get_family_tree(gid)
        tree = out["tree"]
        union_edges = [e for e in tree["edges"] if e["kind"] == "union"]
        self.assertEqual(len(union_edges), 1)
        self.assertEqual(union_edges[0]["flags"], ["active"])
        env2 = svc._load(gid)
        self.assertEqual(len(env2["graph"]["unions"]), 1)
        # idempotent: a second load does not duplicate
        env3 = svc._read_save(svc._path(gid))
        self.assertEqual(len(env3["graph"]["unions"]), 1)

    def test_migration_preserves_pending_and_cash(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-mp-"))
        st = svc.new_game(dict(PROFILE), "mp-create")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].money = 4242
        env["pending"] = {"id": "d_1", "title": "t", "text": "x",
                          "options": [{"id": "o1", "label": "ok",
                                       "known_effects": [], "_effects": {},
                                       "_outcome": ""}]}
        svc._save(env)
        out = svc.get_state(gid)  # triggers load + backfills
        env2 = svc._load(gid)
        self.assertEqual(env2["player"].money, 4242)
        self.assertEqual(env2["pending"]["id"], "d_1")


class RelatednessAndConceptionTests(unittest.TestCase):
    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-kin-"))
        self.st = self.svc.new_game(dict(PROFILE), "kin-create")
        self.gid = self.st["game_id"]
        people = self.svc.get_state(self.gid)["people"]
        self.roles = {p["role"]: p["id"] for p in people}

    def test_relatedness_real_family(self):
        got = self.svc.get_relatedness(self.gid, self.roles["player"],
                                       self.roles["mother"])
        self.assertEqual(got["category"], "parent_child")
        self.assertEqual(got["coefficient_band"], "0.25")
        got = self.svc.get_relatedness(self.gid, self.roles["mother"],
                                       self.roles["father"])
        self.assertEqual(got["category"], "unrelated")
        with self.assertRaises(GameError) as ctx:
            self.svc.get_relatedness(self.gid, self.roles["player"], "nope")
        self.assertEqual(ctx.exception.status, 404)

    def test_conception_outcome_rolled_once_and_persisted(self):
        env = self.svc._load(self.gid)
        adults = [p for p in self.svc._walk_people(env["player"])
                  if p.age >= 18]
        mother, father = adults[0].person_id, adults[1].person_id
        rng_before = env["rng_state"]
        out1 = self.svc.record_conception(self.gid, "child-future", mother,
                                          father, "conc-1")
        env2 = self.svc._load(self.gid)
        self.assertIn("child-future:conc-1",
                      env2["graph"]["conception_outcomes"])
        # retry: same stored outcome, game RNG not re-consumed
        out2 = self.svc.record_conception(self.gid, "child-future", mother,
                                          father, "conc-1")
        self.assertEqual(out2, out1)
        env3 = self.svc._load(self.gid)
        self.assertEqual(env3["rng_state"], env2["rng_state"])
        self.assertNotEqual(env3["rng_state"], rng_before)

    def test_conception_requires_adult_parents(self):
        env = self.svc._load(self.gid)
        kid = next(p for p in self.svc._walk_people(env["player"])
                   if p.age < 18)
        adult = next(p for p in self.svc._walk_people(env["player"])
                     if p.age >= 18)
        with self.assertRaises(GameError) as ctx:
            self.svc.record_conception(self.gid, "child-x", kid.person_id,
                                       adult.person_id, "conc-2")
        self.assertEqual(ctx.exception.code, "not_adult")

    def test_conception_binds_parentage_and_consumes_once_at_birth(self):
        env = self.svc._load(self.gid)
        adults = [p for p in self.svc._walk_people(env["player"])
                  if p.age >= 18]
        mother, father = adults[0].person_id, adults[1].person_id
        out = self.svc.record_conception(self.gid, "child-bound", mother,
                                         father, "conc-3")
        self.assertEqual(out["bio_mother_id"], mother)
        self.assertEqual(out["bio_father_id"], father)
        env2 = self.svc._load(self.gid)
        edges = {(e["child_id"], e["parent_id"], e["role"], e["confidence"])
                 for e in env2["graph"]["parentage"]}
        self.assertIn(("child-bound", mother, "biological", "certain"),
                      edges)
        self.assertIn(("child-bound", father, "biological", "certain"),
                      edges)
        # consumed exactly once at birth; a retried birth gets nothing
        got = self.svc.consume_conception_outcome(self.gid, "child-bound")
        self.assertEqual(got["adjustment"], out["adjustment"])
        self.assertIsNone(self.svc.consume_conception_outcome(
            self.gid, "child-bound"))
        # a child with no recorded conception (legacy birth/adoption)
        self.assertIsNone(self.svc.consume_conception_outcome(
            self.gid, "child-unrecorded"))

    def test_newborn_after_initial_parents_gets_parentage_on_load(self):
        """The per-record backfill is not all-or-nothing: a child born
        after the first load still gets graph parentage for both recorded
        parents (and only real recorded parents)."""
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        env["player"].age = 30
        mother = next(p for p in self.svc._walk_people(env["player"])
                      if getattr(p, "name", "") and p.age >= 18)
        baby = Child("Newborn", "Test", Gender.Male, 50, 50)
        baby.age = 0
        baby.mother = env["player"]
        baby.father = mother
        env["player"].children.append(baby)
        self.svc._save(env)
        self.svc.get_state(self.gid)  # triggers load + backfill
        env2 = self.svc._load(self.gid)
        parents = {e["parent_id"] for e in env2["graph"]["parentage"]
                   if e["child_id"] == baby.person_id}
        self.assertEqual(parents, {env2["player"].person_id,
                                   mother.person_id})

    def test_relatedness_honours_unknown_lineage_marker(self):
        from src.appearance.integration import ensure_person_id
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        pid = env["player"].person_id
        stranger = Parent("Riley", 32, Gender.Female)
        ensure_person_id(stranger)
        env["player"].relations.append(stranger)
        self.svc._save(env)
        # a Parent-typed acquaintance is NOT a recorded parent
        self.assertEqual(self.svc.get_relatedness(
            self.gid, pid, stranger.person_id)["category"], "unrelated")
        env = self.svc._load(self.gid)
        stranger = next(p for p in env["player"].relations
                        if p.person_id == stranger.person_id)
        stranger.lineage = "unknown"
        self.svc._save(env)
        got = self.svc.get_relatedness(self.gid, pid, stranger.person_id)
        self.assertEqual(got["category"], "unknown")
        self.assertEqual(got["coefficient_band"], "unknown")


class GraphCommandTests(unittest.TestCase):
    """Step 6: typed transitions for B/C via the replay-safe wrapper."""

    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-gc-"))
        self.st = self.svc.new_game(dict(PROFILE), "gc-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        self.adults = {p["role"]: p["id"] for p in st["people"]
                       if p.get("role") in ("mother", "father")}

    def test_form_union_journaled_with_replay(self):
        st = self.svc.get_state(self.gid)
        res = self.svc.graph_command(
            self.gid, st["revision"], "gc-1", "form_union", {
                "a_id": st["player_id"], "b_id": self.adults["mother"],
                "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "registration"},
                "turn": st["revision"]})
        self.assertTrue(res["result"]["ok"])
        self.assertIn("union_id", res["result"]["ids"])
        # journal recorded the ACTUAL transition
        st2 = self.svc.get_state(self.gid)
        self.assertEqual(st2["journal"][-1]["kind"], "union")
        # retry replays without double effects
        retry = self.svc.graph_command(self.gid, st["revision"], "gc-1",
                                       "form_union", {
                "a_id": st["player_id"], "b_id": self.adults["mother"],
                "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "registration"},
                "turn": st["revision"]})
        self.assertEqual(retry, res)
        env = self.svc._load(self.gid)
        self.assertEqual(len(env["graph"]["unions"]), 1)

    def test_end_union_selected_edge(self):
        st = self.svc.get_state(self.gid)
        self.svc.graph_command(
            self.gid, st["revision"], "gc-2", "form_union", {
                "a_id": st["player_id"], "b_id": self.adults["mother"],
                "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "registration"},
                "turn": 0})
        st = self.svc.get_state(self.gid)
        res = self.svc.graph_command(
            self.gid, st["revision"], "gc-3", "end_union",
            {"union_id": res_union_id(self.svc, self.gid), "how": "divorce",
             "turn": st["revision"]})
        self.assertTrue(res["result"]["ok"])
        memberships = self.svc.get_relatedness(self.gid, st["player_id"],
                                               self.adults["mother"])
        self.assertEqual(memberships["category"], "parent_child")

    def test_guard_surfaces_typed_error(self):
        st = self.svc.get_state(self.gid)
        with self.assertRaises(GameError) as ctx:
            self.svc.graph_command(
                self.gid, st["revision"], "gc-4", "form_union", {
                    "a_id": st["player_id"], "b_id": st["player_id"],
                    "kind": "marriage",
                    "recognition": {"status": "recognized",
                                    "jurisdiction_id": "j",
                                    "law_version": 1, "basis": "ceremony"},
                    "turn": 0})
        self.assertEqual(ctx.exception.code, "graph_error")

    def test_unknown_op_rejected(self):
        st = self.svc.get_state(self.gid)
        with self.assertRaises(GameError) as ctx:
            self.svc.graph_command(self.gid, st["revision"], "gc-5",
                                   "demolish", {})
        self.assertEqual(ctx.exception.code, "invalid_input")


def res_union_id(svc, gid):
    env = svc._load(gid)
    return env["graph"]["unions"][0]["union_id"]


class RenameSameNameTests(unittest.TestCase):
    """Graph edges and queries are person_id-based; two people with the
    same display name must stay fully distinguishable."""

    def test_same_name_family_stays_consistent(self):
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-rn-"))
        st = svc.new_game(dict(PROFILE), "rn-create")
        gid = st["game_id"]
        st = svc.get_state(gid)
        roles = {p["role"]: p for p in st["people"]}
        # rename the father to the player's exact name through the real
        # editor path
        res = svc.editor_apply(
            gid, st["revision"], "rn-1",
            [{"name": "rename_person", "scope": "person",
              "target_id": roles["father"]["id"],
              "patch": {"first_name": PROFILE["first_name"],
                        "last_name": PROFILE["last_name"]}}])
        self.assertIn("audit", res)
        st2 = svc.get_state(gid)
        names = [p["name"] for p in st2["people"]]
        self.assertEqual(names.count(PROFILE["first_name"] + " " +
                                     PROFILE["last_name"]), 2)
        ids = [p["id"] for p in st2["people"]]
        self.assertEqual(len(set(ids)), len(ids))
        # kinship is unaffected: still the player's biological father
        rel = svc.get_relatedness(gid, roles["player"]["id"],
                                  roles["father"]["id"])
        self.assertEqual(rel["category"], "parent_child")
        # the tree renders BOTH nodes, distinct ids, father flagged as ex
        # of nobody / parent edge intact
        out = svc.get_family_tree(gid)
        tree_names = [n["name"] for n in out["tree"]["nodes"]]
        self.assertEqual(tree_names.count(PROFILE["first_name"] + " " +
                                          PROFILE["last_name"]), 2)
        parent_edges = [e for e in out["tree"]["edges"]
                        if e["kind"] == "parentage"
                        and e["to_id"] == roles["player"]["id"]]
        self.assertEqual({e["from_id"] for e in parent_edges},
                         {roles["mother"]["id"], roles["father"]["id"]})
        # renaming again hits only the father record
        svc.editor_apply(
            gid, svc.get_state(gid)["revision"], "rn-2",
            [{"name": "rename_person", "scope": "person",
              "target_id": roles["father"]["id"],
              "patch": {"first_name": "Grampa"}}])
        st3 = svc.get_state(gid)
        player = next(p for p in st3["people"]
                      if p["id"] == roles["player"]["id"])
        self.assertTrue(player["name"].startswith(PROFILE["first_name"]))


class DeathAndSuccessionGraphTests(unittest.TestCase):
    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-death-"))
        self.st = self.svc.new_game(dict(PROFILE), "death-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        self.roles = {p["role"]: p["id"] for p in st["people"]}

    def _form(self, rev, rid, other, req):
        return self.svc.graph_command(
            self.gid, rev, req, "form_union", {
                "a_id": rid, "b_id": other, "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "registration"},
                "turn": 0})

    def test_death_widows_unions_ends_relationships_cancels_offers(self):
        st = self.svc.get_state(self.gid)
        mother = self.roles["mother"]
        self._form(st["revision"], st["player_id"], mother, "d-1")
        env = self.svc._load(self.gid)
        people = {p.person_id: p for p in self.svc._walk_people(env["player"])}
        from src.game.features import family_graph as fg
        fg.start_relationship(env["graph"], env["player"], people[mother],
                              "committed",
                              {"satisfaction": 1, "trust": 1,
                               "chemistry": 1}, turn=0)
        fg.record_offer(env["graph"], env["player"], people[mother],
                        "propose_union", turn=0, expiry_turn=9,
                        policy_revision=0)
        self.svc._save(env)
        # the mother dies through the real event-death path
        env = self.svc._load(self.gid)
        self.svc._apply_event_death(env, {"person_id": mother,
                                          "cause": "passed away"})
        self.svc._save(env)
        env2 = self.svc._load(self.gid)
        union = env2["graph"]["unions"][0]
        self.assertEqual(union["status"], "widowed")
        rel = env2["graph"]["relationships"][0]
        self.assertEqual(rel["status"], "ended")
        self.assertEqual(env2["graph"]["offers"][0]["status"],
                         "superseded")
        self.assertEqual(env2["graph"]["offers"][0]["attempt"]["count"], 1)
        # parentage history survives: tree still shows the deceased mother
        out = self.svc.get_family_tree(self.gid)
        node = next(n for n in out["tree"]["nodes"]
                    if n["person_id"] == mother)
        self.assertIn("deceased", node["flags"])
        self.assertIn((mother,), [(e["from_id"],) for e in
                                  out["tree"]["edges"]
                                  if e["kind"] == "parentage"])
        # idempotent: applying the same death again changes nothing
        self.svc._apply_event_death(env2, {"person_id": mother,
                                           "cause": "passed away"})
        env3 = self.svc._load(self.gid)
        self.assertEqual(env3["graph"]["unions"][0]["status"], "widowed")
        self.assertEqual(len(env3["graph"]["unions"]), 1)


class ChildCostDedupeTests(unittest.TestCase):
    def test_shared_child_posts_once(self):
        from src.game.features.family_graph import dependent_children
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-dd-"))
        st = svc.new_game(dict(PROFILE), "dd-create")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 30
        svc._save(env)
        st = svc.get_state(gid)
        roles = {p["role"]: p["id"] for p in st["people"]}
        svc.graph_command(
            gid, st["revision"], "dd-1", "form_union", {
                "a_id": roles["player"], "b_id": roles["mother"],
                "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "registration"},
                "turn": 0})
        svc.graph_command(
            gid, svc.get_state(gid)["revision"], "dd-2", "form_union", {
                "a_id": roles["player"], "b_id": roles["father"],
                "kind": "consort_union",
                "recognition": {"status": "unrecognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "ceremony"},
                "turn": 0})
        # a child shared by both unions, recorded once as player's child
        svc.graph_command(
            gid, svc.get_state(gid)["revision"], "dd-3", "add_parentage",
            {"child_id": roles["player"], "parent_id": roles["mother"],
             "role": "step", "source": "marriage"})
        env = svc._load(gid)
        # both unions list the player; the shared child id must appear once
        deps = dependent_children(env["graph"], roles["player"])
        self.assertEqual(deps.count(roles["player"]), 0)  # sanity: self no
        # one entry per distinct child even when two households + parentage
        env["graph"]["households"].append({
            "household_id": "hh-a", "resident_ids": [roles["player"]],
            "dependent_ids": ["child-1", "child-2"],
            "guardians": {}, "support_obligations": []})
        env["graph"]["households"].append({
            "household_id": "hh-b", "resident_ids": [roles["player"]],
            "dependent_ids": ["child-2", "child-3"],
            "guardians": {}, "support_obligations": []})
        self.assertEqual(dependent_children(env["graph"], roles["player"]),
                         ["child-1", "child-2", "child-3"])


class DatingEvaluatorEditorIntegrationTests(unittest.TestCase):
    """C's dating offers score through B's evaluator over A's graph/policy
    views; an editor change on the save moves the evaluation."""

    def test_editor_change_moves_offer_acceptance(self):
        from src.game.features.relationship_policy.evaluator import (
            evaluate_offer)
        from src.game.features.family_graph.service_view import (
            ServiceGraphView, ServicePolicyView)
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-m25-date-"))
        st = svc.new_game(dict(PROFILE), "date-create")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 30
        svc._save(env)
        st = svc.get_state(gid)
        roles = {p["role"]: p["id"] for p in st["people"]}
        target_id = roles["father"]

        def score():
            env = svc._load(gid)
            return evaluate_offer(
                "propose_union", st["player_id"], target_id,
                ServiceGraphView(env), ServicePolicyView(env))

        before = score()
        self.assertTrue(before["hard"]["ok"])
        # an editor change: convert the target's faith to match the
        # initiator's — the faith contribution to the score must move
        env = svc._load(gid)
        my_faith = env["policy"]["profiles"][st["player_id"]]["faith_id"]
        target_faith = env["policy"]["profiles"][target_id]["faith_id"]
        if target_faith == my_faith:
            # pick any different faith from the catalog
            from src.game.features.relationship_policy.catalog import                 load_catalog
            faiths = [f["id"] for f in load_catalog().get("faiths", [])]
            other = next(f for f in faiths if f != my_faith)
            # first move the target AWAY so the conversion below bites
            svc.editor_apply(
                gid, svc.get_state(gid)["revision"], "date-prep",
                [{"name": "convert_profile", "scope": "person",
                  "target_id": target_id,
                  "patch": {"target_faith_id": other}}])
        # an editor change: the target now personally disfavours
        # interfaith unions (relevant because faiths differ) — the
        # personal-tenets contribution to the offer score must move
        svc.editor_apply(
            gid, svc.get_state(gid)["revision"], "date-edit",
            [{"name": "edit_personal_tenets", "scope": "personal_tenets",
              "target_id": target_id,
              "patch": {"tenet_overrides":
                        {"interfaith_unions": "disfavoured"}}}])
        after = score()
        self.assertNotEqual(before["score"], after["score"],
                            (before["score_breakdown"],
                             after["score_breakdown"]))
        self.assertNotEqual(
            before["score_breakdown"].get("personal_tenets"),
            after["score_breakdown"].get("personal_tenets"),
            "the tenet edit must change the personal-tenets contribution")


if __name__ == "__main__":
    unittest.main()


class DatingOfferApplierTests(unittest.TestCase):
    """A3: the offer/respond domain appliers through the real resolver.
    Accepted -> courtship edge (stranger -> partner); refused -> a stored
    declined outcome with no invented relationship."""

    EVENT = {
        "id": "m25_test_date_offer",
        "title": "A match wants to meet",
        "text": "{person:target} asks {name} out for coffee.",
        "interaction": {"kind": "date", "romantic": True},
        "roles": {
            "actor": {"kind": "person", "select": "player"},
            "target": {"kind": "person", "select": "predicate",
                       "predicate": "adult_non_related_stranger"},
        },
        "choices": [
            {"id": "accept", "label": "Go on the date", "cost": {},
             "effects": {
                 "chemistry": {"between": ["actor", "target"],
                               "delta": {"randint": [3, 8]}},
                 "offer": {"interaction": "propose_courtship",
                           "to": "target", "as": "npc"}},
             "outcome_text": "We had a lovely evening."},
            {"id": "decline", "label": "Not tonight", "cost": {},
             "effects": {}, "outcome_text": "I swiped away."},
        ],
        "audience": {"scope": "player_known",
                     "participants": ["actor", "target"]},
    }

    def setUp(self):
        import random
        from src.appearance.integration import ensure_person_id
        from src.game.events import EventEngine, GameContext
        from src.game.features.parenting.engine import _V3Context
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        self._random = random
        self.dir = tempfile.mkdtemp(prefix="lifesim-m25-offer-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "offer-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        stranger = Parent("Riley", 32, Gender.Female)
        ensure_person_id(stranger)
        stranger.relationship = 40
        env["player"].relations.append(stranger)
        self.target_id = stranger.person_id
        self.svc._save(env)
        # bind the stranger once, as the engine does at offer creation
        env = self.svc._load(self.gid)
        ctx = _V3Context(GameContext(env, self.svc))
        self.pending = EventEngine.instantiate3(
            self.EVENT, 7, random.Random(3), ctx,
            prebound={"target": self.target_id})
        self.pending["id"] = "d_offer_test"
        env["pending"] = self.pending
        self.svc._save(env)

    def _resolve_accept(self):
        st = self.svc.get_state(self.gid)
        return self.svc.command(
            self.gid, st["revision"], "req-offer-1", "resolve_decision",
            {"decision_id": "d_offer_test",
             "option_id": next(o["id"] for o in
                               self.pending["options"]
                               if o["id"].endswith("accept"))})

    def _graph(self):
        return self.svc._load(self.gid)["graph"]

    def test_accepted_offer_creates_courtship_edge_once(self):
        env = self.svc._load(self.gid)
        sampled = dict(env["pending"].get("sampled") or {})
        sampled["accepted"] = True
        env["pending"]["sampled"] = sampled
        self.svc._save(env)
        self._resolve_accept()
        graph = self._graph()
        offers = graph["offers"]
        self.assertEqual(len(offers), 1)
        self.assertEqual(offers[0]["status"], "accepted")
        self.assertEqual(set(offers[0]["person_ids"])
                         if "person_ids" in offers[0] else
                         {offers[0]["initiator_id"],
                          offers[0]["target_id"]},
                         {env["player"].person_id, self.target_id})
        active = [r for r in graph["relationships"]
                  if r["status"] == "active"]
        self.assertEqual(len(active), 1)
        self.assertEqual(set(active[0]["person_ids"]),
                         {env["player"].person_id, self.target_id})
        self.assertEqual(active[0]["kind"], "courtship")
        # replayed occurrence: a second resolve cannot duplicate the edge
        self._resolve_accept()
        graph = self._graph()
        active = [r for r in graph["relationships"]
                  if r["status"] == "active"]
        self.assertEqual(len(active), 1)

    def test_refused_offer_is_a_stored_outcome_without_an_edge(self):
        env = self.svc._load(self.gid)
        sampled = dict(env["pending"].get("sampled") or {})
        sampled["accepted"] = False
        env["pending"]["sampled"] = sampled
        self.svc._save(env)
        self._resolve_accept()
        graph = self._graph()
        offers = graph["offers"]
        self.assertEqual(len(offers), 1)
        self.assertEqual(offers[0]["status"], "declined")
        self.assertEqual(graph["relationships"], [])
        self.assertEqual(graph["unions"], [])
        # the refusal preserved the attempt record
        self.assertEqual(offers[0]["attempt"]["count"], 1)

    def test_offer_without_stored_response_stays_pending(self):
        self._resolve_accept()
        graph = self._graph()
        offers = graph["offers"]
        self.assertEqual(len(offers), 1)
        self.assertEqual(offers[0]["status"], "pending")
        # R2 (Codex): chemistry is not consent — with no stored accepted
        # response nothing may create the relationship; the offer waits
        # for the target's answer via `respond`
        self.assertEqual(graph["relationships"], [])


if __name__ == "__main__":
    unittest.main()


class EditorSupersessionReprojectionTests(unittest.TestCase):
    """A4: a jurisdiction law edit reprojects recognition onto the living
    world's active unions; policy edits supersede relevant stored offers,
    never unrelated pendings."""

    JUR = "jur-editor-test"

    def setUp(self):
        from src.appearance.integration import ensure_person_id
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        self.dir = tempfile.mkdtemp(prefix="lifesim-m25-ed-")
        self.svc = GameService(self.dir)
        self.st = self.svc.new_game(dict(PROFILE), "ed-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        spouse = Parent("Jordan", 32, Gender.Male)
        ensure_person_id(spouse)
        env["player"].relations.append(spouse)
        self.spouse_id = spouse.person_id
        self.svc._save(env)
        env = self.svc._load(self.gid)
        st = self.svc.get_state(self.gid)
        self.svc.graph_command(
            self.gid, st["revision"], "ed-form-union", "form_union",
            {"a_id": st["player_id"], "b_id": self.spouse_id,
             "kind": "marriage",
             "recognition": {"status": "recognized",
                             "jurisdiction_id": self.JUR,
                             "law_version": 1, "basis": "ceremony"},
             "turn": 0})

    def _union(self):
        env = self.svc._load(self.gid)
        return env["graph"]["unions"][0], env

    def _edit_law(self, status):
        st = self.svc.get_state(self.gid)
        return self.svc.editor_apply(
            self.gid, st["revision"], "ed-law-%s" % status,
            [{"name": "edit_jurisdiction", "scope": "jurisdiction",
              "target_id": self.JUR,
              "patch": {"recognition": {"monogamous_civil": status}}}])

    def test_law_edit_reprojects_recognition_on_active_union(self):
        self._edit_law("forbidden")
        union, env = self._union()
        self.assertEqual(union["recognition"]["status"], "forbidden")
        self.assertEqual(union["recognition"]["law_version"],
                         env["policy"]["policy_revision"])
        # the union still exists; nothing was deleted or retro-fined
        self.assertEqual(union["status"], "active")
        self.assertEqual(len(env["graph"]["unions"]), 1)

    def test_unrelated_law_does_not_touch_the_union(self):
        st = self.svc.get_state(self.gid)
        self.svc.editor_apply(
            self.gid, st["revision"], "ed-other-law",
            [{"name": "edit_jurisdiction", "scope": "jurisdiction",
              "target_id": "jur-somewhere-else",
              "patch": {"recognition": {"monogamous_civil": "forbidden"}}}])
        union, _env = self._union()
        self.assertEqual(union["recognition"]["status"], "recognized")

    def test_policy_edit_supersedes_relevant_pending_offer(self):
        # a stored v3 date pending involving the spouse
        env = self.svc._load(self.gid)
        env["pending"] = {
            "id": "d_ed_date", "kind": "v3", "title": "Date",
            "text": "A date offer.", "options": [{"id": "go",
                                                  "label": "Go"}],
            "interaction": {"kind": "date", "romantic": True},
            "bindings": {"actor": env["player"].person_id,
                         "target": self.spouse_id}}
        self.svc._save(env)
        self._edit_law("unrecognized")
        env = self.svc._load(self.gid)
        self.assertIsNone(env["pending"])
        # a non-romantic pending is left alone
        env["pending"] = {"id": "d_ed_par", "kind": "v3",
                          "title": "School",
                          "text": "A parenting request.",
                          "options": [{"id": "ok", "label": "OK"}],
                          "interaction": {"kind": "parenting_request",
                                          "romantic": False},
                          "bindings": {"actor": env["player"].person_id}}
        self.svc._save(env)
        self._edit_law("recognized")
        env = self.svc._load(self.gid)
        self.assertIsNotNone(env["pending"])


if __name__ == "__main__":
    unittest.main()
