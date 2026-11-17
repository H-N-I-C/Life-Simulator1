"""M2.5 editor route tests (Kimi B): the three HTTP routes in
web/server.py over the real GameService — policy-state payload, preview
dry-run (save untouched), apply with replay/revision semantics, rename
forwarding, typed validation errors.

Run: python3 -m unittest tests.test_web_m25
"""

import json
import os
import shutil
import sys
import tempfile
import threading
import unittest
import urllib.error
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import web.server as server  # noqa: E402


def make_profile(**overrides):
    profile = {
        "first_name": "Alex", "last_name": "Rivers", "gender": "Male",
        "country": "Australia",
        "appearance": {"schema_version": 1, "seed": 7, "skin_tone": "default"},
    }
    profile.update(overrides)
    return profile


class EditorRouteTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-m25-test-")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()
        status, body = cls.req_static(
            "POST", "/api/games",
            dict(make_profile(), request_id="m25-create-1"))
        assert status == 201, body
        cls.game_id = json.loads(body)["game_id"]
        cls.state = json.loads(cls.req_static(
            "GET", "/api/games/" + cls.game_id)[1])
        cls.player_id = cls.state["player_id"]

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
            "http://127.0.0.1:%d%s" % (EditorRouteTests.port, path),
            data=data, method=method, headers=headers)
        try:
            with urllib.request.urlopen(request) as res:
                return res.status, res.read().decode("utf-8")
        except urllib.error.HTTPError as e:
            return e.code, e.read().decode("utf-8")

    def req(self, method, path, body=None):
        return self.req_static(method, path, body)

    def current_revision(self):
        _, body = self.req("GET", "/api/games/" + self.game_id)
        return json.loads(body)["revision"]

    def apply(self, commands, request_id):
        return self.req(
            "POST", "/api/games/%s/editor/apply" % self.game_id,
            {"commands": commands, "request_id": request_id,
             "expected_revision": self.current_revision()})

    # ---- policy-state ----

    def test_policy_state_payload(self):
        status, body = self.req(
            "GET", "/api/games/%s/policy-state" % self.game_id)
        self.assertEqual(status, 200, body)
        data = json.loads(body)
        self.assertIn("profiles", data)
        self.assertIn(self.player_id, data["profiles"])
        self.assertEqual(data["jurisdiction_id"], "jur_metropolis_charter")
        self.assertIsInstance(data["policy_revision"], int)
        faiths = {f["id"] for f in data["catalogs"]["faiths"]}
        self.assertIn("faith_tidebound", faiths)
        jurisdictions = {j["id"] for j in data["catalogs"]["jurisdictions"]}
        self.assertIn("jur_riverlands", jurisdictions)
        roster = {p["id"]: p["name"] for p in data["people"]}
        self.assertIn(self.player_id, roster)

    def test_policy_state_unknown_game_is_typed(self):
        status, body = self.req("GET", "/api/games/nope/policy-state")
        self.assertNotEqual(status, 500)
        self.assertEqual(json.loads(body)["error"]["code"], "not_found")

    # ---- preview ----

    def test_preview_valid_does_not_touch_save(self):
        commands = [{"name": "edit_person_attributes", "scope": "person",
                     "target_id": self.player_id,
                     "patch": {"monogamy_preference": 0.13}}]
        before = json.loads(self.req(
            "GET", "/api/games/%s/policy-state" % self.game_id)[1])
        status, body = self.req(
            "POST", "/api/games/%s/editor/preview" % self.game_id,
            {"commands": commands})
        self.assertEqual(status, 200, body)
        data = json.loads(body)
        self.assertTrue(data["valid"])
        self.assertEqual(data["affected_people"], [self.player_id])
        self.assertEqual(data["policy_revision_delta"], 0)
        # save untouched
        _, after = self.req("GET", "/api/games/%s/policy-state"
                            % self.game_id)
        self.assertEqual(json.loads(after)["profiles"][self.player_id]
                         ["monogamy_preference"],
                         before["profiles"][self.player_id]
                         ["monogamy_preference"])

    def test_preview_invalid_is_typed_and_lists_errors(self):
        commands = [{"name": "edit_person_attributes", "scope": "person",
                     "target_id": self.player_id,
                     "patch": {"age": 25}}]
        status, body = self.req(
            "POST", "/api/games/%s/editor/preview" % self.game_id,
            {"commands": commands})
        self.assertEqual(status, 200, body)
        data = json.loads(body)
        self.assertFalse(data["valid"])
        self.assertEqual(data["errors"][0]["code"], "protected_field")

    def test_preview_non_list_commands_is_400(self):
        status, body = self.req(
            "POST", "/api/games/%s/editor/preview" % self.game_id,
            {"commands": "nope"})
        self.assertEqual(status, 400)
        self.assertEqual(json.loads(body)["error"]["code"], "invalid_input")

    # ---- apply ----

    def test_apply_updates_audits_and_replays(self):
        commands = [{"name": "edit_person_attributes", "scope": "person",
                     "target_id": self.player_id,
                     "patch": {"openness": 0.9, "jealousy": 0.2}}]
        status, body = self.apply(commands, "m25-apply-1")
        self.assertEqual(status, 200, body)
        data = json.loads(body)
        # attribute-only edits do not bump the policy revision
        before = json.loads(self.req(
            "GET", "/api/games/%s/policy-state" % self.game_id)[1])
        self.assertEqual(data["policy_revision"],
                         before["policy_revision"])
        self.assertEqual(data["audit"][0]["after"]["openness"], 0.9)
        self.assertEqual(data["audit"][0]["after"]["openness"], 0.9)
        # replay: identical request_id + payload returns the stored
        # response byte-identically, without reapplying
        status2, body2 = self.apply(commands, "m25-apply-1")
        self.assertEqual(status2, 200)
        self.assertEqual(body2, body)

    def test_apply_stale_revision_is_409(self):
        commands = [{"name": "edit_person_attributes", "scope": "person",
                     "target_id": self.player_id, "patch": {"openness": 1}}]
        status, body = self.req(
            "POST", "/api/games/%s/editor/apply" % self.game_id,
            {"commands": commands, "request_id": "m25-apply-2",
             "expected_revision": self.current_revision() + 99})
        self.assertEqual(status, 409)
        self.assertEqual(json.loads(body)["error"]["code"],
                         "stale_revision")

    def test_apply_rename_forwards_to_person_record(self):
        player = next(p for p in self.state["people"]
                      if p["id"] == self.player_id)
        new_first = "Riley"
        if player["name"].split(" ")[0] == new_first:
            new_first = "Jordan"
        commands = [{"name": "rename_person", "scope": "person",
                     "target_id": self.player_id,
                     "patch": {"first_name": new_first}}]
        status, body = self.apply(commands, "m25-rename-1")
        self.assertEqual(status, 200, body)
        state = json.loads(self.req(
            "GET", "/api/games/" + self.game_id)[1])
        renamed = next(p for p in state["people"]
                       if p["id"] == self.player_id)
        self.assertEqual(renamed["name"].split(" ")[0], new_first)
        # identity preserved
        self.assertEqual(renamed["id"], self.player_id)

    def test_apply_jurisdiction_bumps_policy_revision(self):
        commands = [{"name": "edit_jurisdiction", "scope": "jurisdiction",
                     "target_id": "jur_metropolis_charter",
                     "patch": {"recognition": {"plural_civil":
                                               "forbidden"}}}]
        before = json.loads(self.req(
            "GET", "/api/games/%s/policy-state" % self.game_id)[1])
        status, body = self.apply(commands, "m25-jur-1")
        self.assertEqual(status, 200, body)
        self.assertEqual(json.loads(body)["policy_revision"],
                         before["policy_revision"] + 1)
        _, after = self.req("GET", "/api/games/%s/policy-state"
                            % self.game_id)
        self.assertEqual(json.loads(after)["policy_revision"], 1)
        self.assertEqual(json.loads(after)["overrides"]["jurisdictions"]
                         ["jur_metropolis_charter"]["recognition"]
                         ["plural_civil"], "forbidden")


    # ---- family tree (A's service; B's route) ----

    def test_family_tree_route_passthrough(self):
        status, body = self.req(
            "GET", "/api/games/%s/family-tree" % self.game_id)
        self.assertEqual(status, 200, body)
        data = json.loads(body)
        self.assertIn("tree", data)
        self.assertIn("appearances", data)
        nodes = data["tree"].get("nodes", [])
        self.assertTrue(any(n["person_id"] == self.player_id
                            for n in nodes))

    def test_family_tree_focus_and_unknown(self):
        status, body = self.req(
            "GET", "/api/games/%s/family-tree?focus=%s"
            % (self.game_id, self.player_id))
        self.assertEqual(status, 200, body)
        self.assertEqual(json.loads(body)["tree"]["focus_id"],
                         self.player_id)
        status, body = self.req(
            "GET", "/api/games/%s/family-tree?focus=nobody" % self.game_id)
        self.assertEqual(status, 404)
        self.assertEqual(json.loads(body)["error"]["code"], "not_found")

    def test_game_page_wires_family_tree_button(self):
        status, body = self.req("GET", "/game")
        self.assertEqual(status, 200)
        self.assertIn('id="familyTreeToggle"', body)
        status, js = self.req("GET", "/static/game.js")
        self.assertEqual(status, 200)
        self.assertIn("family_graph/family_tree.js", js)
        self.assertIn("mountFamilyTree", js)


if __name__ == "__main__":
    unittest.main()
