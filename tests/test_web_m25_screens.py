"""M2.5 finish (Kimi B) route tests: B1 revision chain, B2 relationships
view + wedding planner/divorce routes — all over the real HTTP server and
GameService (real ledger balances, replay semantics, typed errors).

Run: python3 -m unittest tests.test_web_m25_screens
"""

import json
import shutil
import tempfile
import threading
import unittest
import urllib.error
import urllib.request
import os
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import web.server as server  # noqa: E402


def make_profile(**overrides):
    profile = {
        "first_name": "Sam", "last_name": "Lee", "gender": "Male",
        "country": "Australia",
        "appearance": {"schema_version": 1, "seed": 7, "skin_tone": "default"},
    }
    profile.update(overrides)
    return profile


class M25ScreensRouteTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-m25-screens-")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()
        status, body = cls.req_static(
            "POST", "/api/games",
            dict(make_profile(scenario="married_30"),
                 request_id="m25s-create-1"))
        assert status == 201, body
        cls.game_id = json.loads(body)["game_id"]

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)

    @staticmethod
    def req_static(method, path, body=None):
        data = None
        headers = {}
        if body is not None:
            data = json.dumps(body).encode("utf-8")
            headers["Content-Type"] = "application/json"
        request = urllib.request.Request(
            "http://127.0.0.1:%d%s" % (M25ScreensRouteTests.port, path),
            data=data, method=method, headers=headers)
        try:
            with urllib.request.urlopen(request) as res:
                return res.status, res.read().decode("utf-8")
        except urllib.error.HTTPError as e:
            return e.code, e.read().decode("utf-8")

    def req(self, method, path, body=None):
        return self.req_static(method, path, body)

    def state(self):
        _, body = self.req("GET", "/api/games/" + self.game_id)
        return json.loads(body)

    # ---- B1: the fixed-UI revision chain -----------------------------

    def test_two_consecutive_applies_using_returned_revision_succeed(self):
        """The B1 fix: the UI sends the CURRENT revision (returned by the
        previous apply) instead of the snapshot from page load. Two edits
        back to back must both commit; a genuinely stale revision still
        409s."""
        rev = self.state()["revision"]
        st1, b1 = self.req(
            "POST", "/api/games/%s/editor/apply" % self.game_id,
            {"commands": [{"name": "edit_person_attributes",
                           "scope": "person",
                           "target_id": self.state()["player_id"],
                           "patch": {"openness": 0.61}}],
             "request_id": "b1-edit-1", "expected_revision": rev})
        self.assertEqual(st1, 200, b1)
        out1 = json.loads(b1)
        self.assertEqual(out1["revision"], rev + 1)
        st2, b2 = self.req(
            "POST", "/api/games/%s/editor/apply" % self.game_id,
            {"commands": [{"name": "edit_person_attributes",
                           "scope": "person",
                           "target_id": self.state()["player_id"],
                           "patch": {"openness": 0.72}}],
             "request_id": "b1-edit-2",
             "expected_revision": out1["revision"]})
        self.assertEqual(st2, 200, b2)
        out2 = json.loads(b2)
        self.assertEqual(out2["revision"], rev + 2)
        # replay of the same request_id returns the stored response
        st3, b3 = self.req(
            "POST", "/api/games/%s/editor/apply" % self.game_id,
            {"commands": [{"name": "edit_person_attributes",
                           "scope": "person",
                           "target_id": self.state()["player_id"],
                           "patch": {"openness": 0.72}}],
             "request_id": "b1-edit-2",
             "expected_revision": out1["revision"]})
        self.assertEqual(st3, 200, b3)
        self.assertEqual(json.loads(b3), out2)
        # a genuinely stale revision still rejects with 409
        st4, b4 = self.req(
            "POST", "/api/games/%s/editor/apply" % self.game_id,
            {"commands": [{"name": "edit_person_attributes",
                           "scope": "person",
                           "target_id": self.state()["player_id"],
                           "patch": {"openness": 0.11}}],
             "request_id": "b1-edit-3", "expected_revision": rev})
        self.assertEqual(st4, 409, b4)
        self.assertEqual(json.loads(b4)["error"]["code"], "stale_revision")
        # the rejected edit never landed
        _, pb = self.req("GET", "/api/games/%s/policy-state" % self.game_id)
        profile = json.loads(pb)["profiles"][self.state()["player_id"]]
        self.assertAlmostEqual(profile["openness"], 0.72, places=5)

    # ---- B2: relationships view ---------------------------------------

    def test_relationships_view_shape(self):
        status, body = self.req(
            "GET", "/api/games/%s/relationships-view" % self.game_id)
        self.assertEqual(status, 200, body)
        data = json.loads(body)
        self.assertIsInstance(data["unions"], list)
        for u in data["unions"]:
            for key in ("union_id", "partner", "status", "recognition",
                        "ceremony_held", "prenup_amount",
                        "shared_children"):
                self.assertIn(key, u)
        self.assertIsInstance(data["facts"], list)
        self.assertIsInstance(data["reputation"], list)
        self.assertIn("venues", data["catalog"])
        self.assertIn("courthouse", data["catalog"]["venues"])
        self.assertEqual(data["catalog"]["venues"]["courthouse"]["base"], 0)
        self.assertIn("child_support_per_child", data["catalog"])
        self.assertIsInstance(data["money"], int)

    def test_relationships_view_unknown_game_is_typed(self):
        status, body = self.req("GET", "/api/games/nope/relationships-view")
        self.assertNotEqual(status, 500)
        self.assertEqual(json.loads(body)["error"]["code"], "not_found")

    # ---- B2: wedding routes --------------------------------------------

    def _union_with_partner(self, prenup=2000):
        """The married_30 scenario's partner→graph sync already minted
        exactly one active union with the partner, so never stack a
        duplicate form_union on top of it; when a test needs a different
        prenup, renegotiate by ending that synced union and re-forming.
        The union is the canonical partner record — the legacy people
        list's ``relationship`` field is a score, not a partner flag."""
        svc = self.httpd.service

        def unions_view():
            _, body = self.req(
                "GET", "/api/games/%s/relationships-view" % self.game_id)
            return json.loads(body)["unions"]

        unions = unions_view()
        self.assertTrue(unions)
        union = next((u for u in unions if u["status"] == "active"), None)
        if union is not None and union["prenup_amount"] == prenup:
            return union
        pid = self.state()["player_id"]
        partner_id = (union or unions[-1])["partner"]["id"]
        if union is not None:
            state = self.state()
            svc.graph_command(
                self.game_id, state["revision"],
                "wedding-test-end-%s" % prenup, "end_union",
                {"union_id": union["union_id"], "how": "divorce",
                 "turn": state["revision"]})
        state = self.state()
        svc.graph_command(
            self.game_id, state["revision"],
            "wedding-test-union-%s" % prenup, "form_union",
            {"a_id": pid, "b_id": partner_id, "kind": "marriage",
             "recognition": {"status": "recognized",
                             "basis": "ceremony",
                             "jurisdiction_id": "jur_metropolis_charter",
                             "law_version": 0},
             "prenup": {"asset": "cash", "amount": prenup},
             "turn": state["revision"]})
        unions = unions_view()
        return next(u for u in unions if u["status"] == "active")

    def test_r5_view_exposes_relationships_and_offers(self):
        union = self._union_with_partner(prenup=0)
        uid = union["union_id"]
        pid = self.state()["player_id"]
        partner_id = union["partner"]["id"]
        svc = self.httpd.service
        # the partner→graph sync also minted a committed relationship
        # edge; retire it so the courtship below is the one active edge
        _, body = self.req(
            "GET", "/api/games/%s/relationships-view" % self.game_id)
        rel = next(r for r in json.loads(body)["relationships"]
                   if r["other"]["id"] == partner_id)
        state = self.state()
        svc.graph_command(
            self.game_id, state["revision"], "r5-rel-end",
            "update_relationship",
            {"relationship_id": rel["relationship_id"],
             "patch": {"end": True}, "turn": state["revision"]})
        state = self.state()
        svc.graph_command(
            self.game_id, state["revision"], "r5-rel", "start_relationship",
            {"a_id": pid, "b_id": partner_id, "kind": "courtship",
             "sampled": {"satisfaction": 55, "trust": 45, "chemistry": 62},
             "turn": state["revision"]})
        state = self.state()
        svc.graph_command(
            self.game_id, state["revision"], "r5-offer", "record_offer",
            {"initiator_id": pid, "target_id": partner_id,
             "interaction_kind": "propose_union", "turn": 3,
             "expiry_turn": 7, "policy_revision": 0})
        _, body = self.req(
            "GET", "/api/games/%s/relationships-view" % self.game_id)
        data = json.loads(body)
        rels = data["relationships"]
        self.assertEqual(len(rels), 1)
        self.assertEqual(rels[0]["kind"], "courtship")
        self.assertEqual(rels[0]["other"]["id"], partner_id)
        self.assertEqual(rels[0]["chemistry"], 62)
        offers = data["offers"]
        self.assertEqual(len(offers), 1)
        self.assertEqual(offers[0]["interaction_kind"], "propose_union")
        self.assertEqual(offers[0]["direction"], "outgoing")
        self.assertEqual(offers[0]["other"]["id"], partner_id)
        self.assertEqual(offers[0]["expiry_turn"], 7)

    def test_wedding_route_validation_is_typed(self):
        state = self.state()
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % self.game_id,
            {"op": "bogus", "args": {}, "request_id": "wv-1",
             "expected_revision": state["revision"]})
        self.assertEqual(status, 400, body)
        self.assertEqual(json.loads(body)["error"]["code"], "invalid_input")
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % self.game_id,
            {"op": "renew_vows", "args": {}, "request_id": "wv-2"})
        self.assertEqual(status, 400, body)
        self.assertEqual(json.loads(body)["error"]["code"], "invalid_input")
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % self.game_id,
            {"op": "renew_vows", "request_id": "wv-3",
             "expected_revision": state["revision"]})
        self.assertEqual(status, 400, body)

    def test_plan_free_wedding_then_divorce_conserves_money(self):
        union = self._union_with_partner(prenup=2000)
        uid = union["union_id"]
        self.assertFalse(union["ceremony_held"])
        self.assertEqual(union["prenup_amount"], 2000)

        before = self.state()
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % self.game_id,
            {"op": "plan_wedding",
             "args": {"union_id": uid, "venue_id": "courthouse",
                      "guests": 0, "outfit_id": "hand_me_down",
                      "honeymoon_id": "none"},
             "request_id": "wed-free-1",
             "expected_revision": before["revision"]})
        self.assertEqual(status, 200, body)
        self.assertTrue(json.loads(body)["result"]["ok"])
        after_plan = self.state()
        # the free configuration genuinely costs nothing
        self.assertEqual(after_plan["money"], before["money"])
        _, view = self.req(
            "GET", "/api/games/%s/relationships-view" % self.game_id)
        mine = next(u for u in json.loads(view)["unions"]
                    if u["union_id"] == uid)
        self.assertTrue(mine["ceremony_held"])

        status, body = self.req(
            "POST", "/api/games/%s/wedding" % self.game_id,
            {"op": "divorce_union", "args": {"union_id": uid},
             "request_id": "wed-div-1",
             "expected_revision": after_plan["revision"]})
        self.assertEqual(status, 200, body)
        final = self.state()
        # payer debited exactly the prenup settlement, once
        self.assertEqual(final["money"], after_plan["money"] - 2000)
        _, view = self.req(
            "GET", "/api/games/%s/relationships-view" % self.game_id)
        mine = next(u for u in json.loads(view)["unions"]
                    if u["union_id"] == uid)
        self.assertEqual(mine["status"], "divorced")
        # replaying the same divorce request returns the stored response
        status, body2 = self.req(
            "POST", "/api/games/%s/wedding" % self.game_id,
            {"op": "divorce_union", "args": {"union_id": uid},
             "request_id": "wed-div-1",
             "expected_revision": after_plan["revision"]})
        self.assertEqual(status, 200, body2)
        self.assertEqual(json.loads(body2)["result"]["ok"], True)
        self.assertEqual(self.state()["money"], final["money"])

    def test_unaffordable_wedding_is_typed_not_allowed(self):
        # Pin scenario generation, then establish the actual $0 budget
        # through the ledger if future content adds a gift/inheritance.
        # Paid configurations must reject; the free configuration stays legal.
        status, body = self.req(
            "POST", "/api/games",
            dict(make_profile(seed=777, scenario="unemployed_18"),
                 request_id="m25s-create-poor"))
        self.assertEqual(status, 201, body)
        gid = json.loads(body)["game_id"]
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        pid = state["player_id"]
        from src.game.finance import Ledger
        if state["money"] != 0:
            # canonical $0: one ledger posting so the balance and
            # player.money agree (never a direct attribute poke)
            env = self.httpd.service._load(gid)
            ledger = Ledger(env, resolver=self.httpd.service
                            ._make_resolver(env))
            turn_id = env["turn"].get("turn_id", "t_setup")
            ledger.post(turn_id, env["player"].age, {
                "phase": "test_setup", "occurrence_id": "zero-for-poor",
                "account_owner_id": pid, "leg": "main", "category": "other",
                "amount": -env["player"].money,
                "description": "Test setup: zero the wedding budget"})
            self.httpd.service._save(env)
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        self.assertEqual(state["money"], 0)
        self.assertEqual(state["finances"]["balance"], 0)
        initial_ledger = state["finances"]["ledger"]
        env = self.httpd.service._load(gid)
        self.assertEqual(Ledger(env, resolver=self.httpd.service
                                ._make_resolver(env)).balance(pid), 0)
        adult = next(p for p in state["people"]
                     if p["id"] != pid and (p.get("age") or 0) >= 18)
        self.httpd.service.graph_command(
            gid, state["revision"], "poor-union", "form_union",
            {"a_id": pid, "b_id": adult["id"], "kind": "marriage",
             "recognition": {"status": "forbidden", "basis": "ceremony",
                             "jurisdiction_id": "jur_metropolis_charter",
                             "law_version": 0}, "turn": 1})
        _, view = self.req("GET", "/api/games/%s/relationships-view" % gid)
        union = json.loads(view)["unions"][0]
        self.assertEqual(union["recognition"], "forbidden")
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        self.assertEqual(state["money"], 0)  # still $0 before the attempt
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % gid,
            {"op": "plan_wedding",
             "args": {"union_id": union["union_id"], "venue_id": "garden",
                      "guests": 10, "outfit_id": "hired",
                      "honeymoon_id": "road"},
             "request_id": "wed-rich-1",
             "expected_revision": state["revision"]})
        self.assertEqual(status, 409, body)
        self.assertEqual(json.loads(body)["error"]["code"], "not_allowed")
        # no money moved, no ceremony held
        rejected = json.loads(self.req("GET", "/api/games/" + gid)[1])
        self.assertEqual(rejected["money"], 0)
        self.assertEqual(rejected["finances"]["balance"], 0)
        self.assertEqual(rejected["finances"]["ledger"], initial_ledger)
        _, view = self.req("GET", "/api/games/%s/relationships-view" % gid)
        mine = next(u for u in json.loads(view)["unions"]
                    if u["union_id"] == union["union_id"])
        self.assertFalse(mine["ceremony_held"])
        # the free configuration remains the legal no-cost exit at $0
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % gid,
            {"op": "plan_wedding",
             "args": {"union_id": union["union_id"], "venue_id": "courthouse",
                      "guests": 0, "outfit_id": "hand_me_down",
                      "honeymoon_id": "none"},
             "request_id": "wed-free-poor",
             "expected_revision": state["revision"]})
        self.assertEqual(status, 200, body)
        self.assertTrue(json.loads(body)["result"]["ok"])
        free = json.loads(self.req("GET", "/api/games/" + gid)[1])
        self.assertEqual(free["money"], 0)
        self.assertEqual(free["finances"]["balance"], 0)
        self.assertEqual(free["finances"]["ledger"], initial_ledger)


if __name__ == "__main__":
    unittest.main()
