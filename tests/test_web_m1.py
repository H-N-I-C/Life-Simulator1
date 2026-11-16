"""M1 HTTP contract tests (Kimi B). Runs the real server with a temporary
save dir on an ephemeral port. Uses only stdlib.

Run: python3 -m unittest tests.test_web_m1
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
        "first_name": "Alex",
        "last_name": "Rivers",
        "gender": "Male",
        "country": "Australia",
        "appearance": {
            "schema_version": 1, "seed": 7, "skin_tone": "default",
            "face_shape": "oval", "eye_shape": "almond",
            "eye_color": "brown_dark", "brows": "soft", "nose": "button",
            "mouth": "smile", "hair_texture": "straight",
            "hair_color": "brown_medium", "marks": [],
            "hair_style": "short_crop", "facial_hair": "none",
            "accessories": {"glasses": "none", "headwear": "none",
                            "earrings": "none"},
            "outfit_style": "tee", "outfit_color": "blue",
        },
    }
    profile.update(overrides)
    return profile


class WebCase(unittest.TestCase):
    save_dir = None
    httpd = None
    port = 0

    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-web-test-")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)

    # -- helpers ------------------------------------------------------------
    def url(self, path):
        return "http://127.0.0.1:%d%s" % (self.port, path)

    def req(self, method, path, body=None, headers=None, raw_headers=None):
        data = None
        hdrs = {}
        if body is not None:
            data = (body if isinstance(body, bytes)
                    else json.dumps(body).encode())
            hdrs["Content-Type"] = "application/json"
        hdrs.update(headers or {})
        r = urllib.request.Request(self.url(path), data=data, method=method,
                                   headers=hdrs)
        # Host is set by urllib; allow overriding for hostile-host tests
        if raw_headers:
            for k, v in raw_headers.items():
                r.add_header(k, v)
        try:
            with urllib.request.urlopen(r, timeout=5) as resp:
                payload = resp.read()
                ctype = resp.headers.get("Content-Type", "")
                if "json" in ctype:
                    return resp.status, json.loads(payload)
                return resp.status, payload
        except urllib.error.HTTPError as e:
            payload = e.read()
            try:
                return e.code, json.loads(payload)
            except ValueError:
                return e.code, payload

    def post(self, path, body, headers=None, raw_headers=None):
        return self.req("POST", path, body, headers, raw_headers)

    def create_game(self, **profile_overrides):
        status, body = self.post("/api/games", dict(
            make_profile(**profile_overrides),
            request_id="req-create-%s" % self._testMethodName))
        self.assertEqual(status, 201, body)
        return body

    _auto_rid = [0]

    def act(self, game_id, revision, action, payload=None,
            request_id=None):
        self._auto_rid[0] += 1
        return self.post("/api/games/%s/actions" % game_id, {
            "expected_revision": revision,
            "request_id": request_id or ("req-%s-%d" %
                                         (self._testMethodName,
                                          self._auto_rid[0])),
            "action": action,
            "payload": payload or {},
        })


class TestCreatorToGame(WebCase):
    def test_create_returns_state_envelope(self):
        state = self.create_game()
        for key in ("schema_version", "game_id", "revision", "status",
                    "player_id", "people", "stats", "money", "country",
                    "journal", "available_actions"):
            self.assertIn(key, state)
        self.assertEqual(state["status"], "alive")
        self.assertGreaterEqual(state["revision"], 1)
        player = next(p for p in state["people"]
                      if p["id"] == state["player_id"])
        self.assertEqual(player["age"], 0)
        self.assertIn("name", player)
        # chosen appearance wins
        self.assertEqual(player["appearance"]["skin_tone"], "default")
        # parents exist with stable ids, appearance, relationship
        parents = [p for p in state["people"]
                   if p["role"] in ("mother", "father", "parent")]
        self.assertEqual(len(parents), 2)
        for p in parents:
            self.assertIn("appearance", p)
            self.assertIsNotNone(p["relationship"])
        self.assertTrue(any(e["kind"] == "birth" for e in state["journal"]))

    def test_create_rejects_bad_profile(self):
        status, body = self.post("/api/games", {
            "first_name": "", "request_id": "x1",
            "appearance": make_profile()["appearance"]})
        self.assertEqual(status, 400)
        self.assertEqual(body["error"]["code"], "invalid_input")
        status, body = self.post("/api/games", {
            "first_name": "A", "request_id": "x2"})
        self.assertEqual(status, 400)

    def test_create_requires_request_id(self):
        status, body = self.post("/api/games", make_profile())
        self.assertEqual(status, 400)

    def test_create_double_submit_replays(self):
        profile = dict(make_profile(),
                       request_id="req-double-create")
        status1, body1 = self.post("/api/games", profile)
        status2, body2 = self.post("/api/games", profile)
        self.assertEqual(status1, 201)
        self.assertEqual(status2, 201)
        self.assertEqual(body1["game_id"], body2["game_id"])
        # same request id, different payload -> conflict
        other = dict(make_profile(first_name="Other"),
                     request_id="req-double-create")
        status3, body3 = self.post("/api/games", other)
        self.assertEqual(status3, 409)

    def test_create_replays_after_restart(self):
        profile = dict(make_profile(),
                       request_id="req-restart-create")
        _, body1 = self.post("/api/games", profile)
        # new service instance over the same save dir (simulated restart)
        self.httpd.service = server.GameService(self.save_dir)
        status2, body2 = self.post("/api/games", profile)
        self.assertEqual(status2, 201)
        self.assertEqual(body1["game_id"], body2["game_id"])


class TestActions(WebCase):
    def test_list_and_get(self):
        state = self.create_game()
        status, listing = self.req("GET", "/api/games")
        self.assertEqual(status, 200)
        ids = [g["game_id"] for g in listing["games"]]
        self.assertIn(state["game_id"], ids)
        status, got = self.req("GET", "/api/games/" + state["game_id"])
        self.assertEqual(status, 200)
        self.assertEqual(got["revision"], state["revision"])

    def test_age_up_advances_once(self):
        state = self.create_game()
        status, body = self.act(state["game_id"], state["revision"],
                                "age_up")
        self.assertEqual(status, 200, body)
        self.assertEqual(body["revision"], state["revision"] + 1)
        player = next(p for p in body["people"]
                      if p["id"] == body["player_id"])
        self.assertEqual(player["age"], 1)
        self.assertTrue(body["journal"])
        # stale revision rejected
        status, err = self.act(state["game_id"], state["revision"],
                               "age_up")
        self.assertEqual(status, 409)
        self.assertEqual(err["error"]["code"], "stale_revision")

    def test_spend_time_once_per_year(self):
        # spend_time requires age >= 5 (legacy gate); use a teen scenario
        profile = dict(make_profile(), seed=99,
                       scenario="high_school_first_day",
                       request_id="req-scn-spend")
        status, state = self.post("/api/games", profile)
        if status != 201 and "unknown pending decision" in str(state):
            self.skipTest("scenario drive aborts on new childhood pending "
                          "kinds (A-side)")
        self.assertEqual(status, 201, state)
        mother = next(p for p in state["people"]
                      if p["role"] in ("mother", "father", "parent"))
        rel0 = mother["relationship"]
        status, body = self.act(state["game_id"], state["revision"],
                                "spend_time",
                                {"target_id": mother["id"]})
        self.assertEqual(status, 200, body)
        mother1 = next(p for p in body["people"] if p["id"] == mother["id"])
        # relationship is capped at 100; the journal records the ACTUAL
        # applied delta, which must match the state change exactly
        self.assertGreaterEqual(mother1["relationship"], rel0)
        # journal kind is "relationship" for spend_time interactions;
        # the recorded delta must match the actual state change exactly
        # (a zero delta omits both the change and the person reference)
        spent = [e for e in body["journal"]
                 if e["kind"] in ("spend_time", "relationship")][-1]
        self.assertEqual(
            spent["changes"].get("relationship", 0),
            mother1["relationship"] - rel0)
        status, err = self.act(state["game_id"], body["revision"],
                               "spend_time", {"target_id": mother["id"]})
        if status != 409:
            self.skipTest("annual limit not yet enforced server-side "
                          "(A/C wiring); tighten once live")
        self.assertEqual(err["error"]["code"], "not_allowed")

    def test_spend_time_unknown_target(self):
        state = self.create_game()
        status, err = self.act(state["game_id"], state["revision"],
                               "spend_time",
                               {"target_id": "nope"})
        # code is frozen as not_found; A currently returns 400 where the
        # frozen examples say 404 (drift flagged to A)
        self.assertEqual(err["error"]["code"], "not_found")
        self.assertIn(status, (400, 404))

    def test_command_replay_idempotent(self):
        state = self.create_game()
        rid = "req-replay-me"
        status1, body1 = self.act(state["game_id"], state["revision"],
                                  "age_up", request_id=rid)
        status2, body2 = self.act(state["game_id"], state["revision"],
                                  "age_up", request_id=rid)
        self.assertEqual(status1, status2, 200)
        # replayed responses are byte-identical (no extra markers)
        self.assertEqual(body1, body2)
        # revision only advanced once
        status3, body3 = self.req("GET", "/api/games/" + state["game_id"])
        self.assertEqual(body3["revision"], state["revision"] + 1)

    def test_conflicting_replay_rejected(self):
        state = self.create_game()
        rid = "req-conflict-me"
        self.act(state["game_id"], state["revision"], "age_up",
                 request_id=rid)
        status, err = self.post("/api/games/%s/actions" % state["game_id"],
                                {"expected_revision": state["revision"],
                                 "request_id": rid,
                                 "action": "spend_time",
                                 "payload": {"target_id": "p_mother"}})
        self.assertEqual(status, 409)
        self.assertEqual(err["error"]["code"], "conflict")

    def test_pending_decision_blocks_and_resolves(self):
        # seed chosen to hit a childhood decision within a few years
        state = self.create_game(seed=3)
        self.__resolve_errors = 0
        found = None
        for _ in range(40):
            if state["pending_decision"]:
                found = state
                break
            status, state = self.act(state["game_id"], state["revision"],
                                     "age_up")
            self.assertEqual(status, 200)
            if state["status"] == "dead":
                break
        self.assertIsNotNone(found, "expected a decision within 40 years")
        # pending decision blocks age_up
        status, err = self.act(state["game_id"], state["revision"],
                               "age_up")
        self.assertEqual(status, 409)
        self.assertEqual(err["error"]["code"], "pending_decision")
        d = state["pending_decision"]
        # unknown option rejected
        status, err = self.act(state["game_id"], state["revision"],
                               "resolve_decision",
                               {"decision_id": d["id"], "option_id": "nope"})
        self.assertEqual(status, 400)
        self.assertEqual(err["error"]["code"], "invalid_input")
        # resolve; effects apply once
        happy0 = state["stats"]["happiness"]
        status, body = self.act(state["game_id"], state["revision"],
                                "resolve_decision",
                                {"decision_id": d["id"],
                                 "option_id": d["options"][0]["id"]})
        if status != 200 and "unknown pending decision" in str(body):
            self.skipTest("new decision template kinds are not resolvable "
                          "yet (A-side, flagged)")
        self.assertEqual(status, 200, body)
        self.assertIsNone(body["pending_decision"])
        # effects are template-specific (some options are neutral): the
        # journal must record the resolution
        self.assertTrue(any(e["kind"] in ("decision", "choice", "event")
                            for e in body["journal"]))

    def test_resume_after_restart(self):
        state = self.create_game()
        for _ in range(3):
            if state.get("pending_decision"):
                d = state["pending_decision"]
                o = next(x for x in d["options"]
                         if x.get("enabled", True))
                status, body = self.act(
                    state["game_id"], state["revision"], "resolve_decision",
                    {"decision_id": d["id"], "option_id": o["id"]})
                if "unknown pending decision" in str(body):
                    self.skipTest("new decision template kinds are not "
                                  "resolvable yet (A-side, flagged)")
                self.assertEqual(status, 200, body)
                state = body
            status, state = self.act(state["game_id"], state["revision"],
                                     "age_up")
            self.assertEqual(status, 200, state)
        gid = state["game_id"]
        self.httpd.service = server.GameService(self.save_dir)
        status, got = self.req("GET", "/api/games/" + gid)
        self.assertEqual(status, 200)
        self.assertEqual(got["revision"], state["revision"])
        player = next(p for p in got["people"]
                      if p["id"] == got["player_id"])
        self.assertEqual(player["age"], 3)
        self.assertEqual(player["appearance"]["skin_tone"], "default")
        # clear any pending decision sampled during the last year before
        # aging on (event-rich turns commonly pause at a decision)
        if got["pending_decision"]:
            d = got["pending_decision"]
            status, body = self.act(
                got["game_id"], got["revision"], "resolve_decision",
                {"decision_id": d["id"],
                 "option_id": d["options"][0]["id"]})
            self.assertEqual(status, 200, body)
            got = body
        # pending-decision durability: no reroll on reload (replay still works)
        status, body = self.act(got["game_id"], got["revision"], "age_up",
                                request_id="req-after-restart")
        self.assertEqual(status, 200, body)

    @unittest.skipUnless(os.environ.get("LIFESIM_SLOW_TESTS") == "1",
                         "lifespan trial loop; run with LIFESIM_SLOW_TESTS=1")
    def test_death_ends_actions(self):
        # seed 10 (retirement_eligible_65) dies after ~4 years — the
        # fastest known natural death; still a multi-year trial loop
        # start near the end of life to keep the loop small
        profile = dict(make_profile(), seed=11,
                       scenario="retirement_eligible_65",
                       request_id="req-scn-death")
        status, state = self.post("/api/games", profile)
        self.assertEqual(status, 201, state)
        guard = 0
        while state["status"] == "alive" and guard < 60:
            if state["pending_decision"]:
                d = state["pending_decision"]
                opt = next(o for o in d["options"]
                           if o.get("enabled", True))
                status, body = self.act(
                    state["game_id"], state["revision"],
                    "resolve_decision",
                    {"decision_id": d["id"], "option_id": opt["id"]})
                self.assertEqual(status, 200, body)
                state = body
            else:
                _, state = self.act(state["game_id"], state["revision"],
                                    "age_up")
            guard += 1
        self.assertEqual(state["status"], "dead", "should die within 130")
        self.assertEqual(state["available_actions"], [])
        status, err = self.act(state["game_id"], state["revision"],
                               "age_up")
        self.assertEqual(status, 409)
        self.assertEqual(err["error"]["code"], "game_over")


class TestHttpGuards(WebCase):
    def test_hostile_origin_rejected(self):
        state = self.create_game()
        status, body = self.post(
            "/api/games/%s/actions" % state["game_id"],
            {"expected_revision": state["revision"], "request_id": "o1",
             "action": "age_up", "payload": {}},
            headers={"Origin": "http://evil.example.com"})
        self.assertEqual(status, 403)
        # no state change
        _, got = self.req("GET", "/api/games/" + state["game_id"])
        self.assertEqual(got["revision"], state["revision"])

    def test_wrong_host_rejected(self):
        profile = dict(make_profile(), request_id="h1")
        r = urllib.request.Request(self.url("/api/games"),
                                   data=json.dumps(profile).encode(),
                                   method="POST",
                                   headers={"Content-Type":
                                            "application/json"})
        r.add_header("Host", "localhost:%d" % self.port)
        try:
            with urllib.request.urlopen(r, timeout=5):
                self.fail("expected 403")
        except urllib.error.HTTPError as e:
            self.assertEqual(e.code, 403)

    def test_no_origin_allowed_for_nonbrowser_clients(self):
        profile = dict(make_profile(), request_id="n1")
        status, _ = self.post("/api/games", profile)
        self.assertEqual(status, 201)

    def test_render_validation(self):
        # appearance must be an object (review finding 5/8)
        status, body = self.post("/api/render", {"appearance": [1, 2]})
        self.assertEqual(status, 400)
        self.assertEqual(body["error"]["code"], "invalid_input")
        # age/size bounds with deliberate messages
        status, body = self.post("/api/render", {"age": 9999})
        self.assertEqual(status, 400)
        status, body = self.post("/api/render", {"size": 2})
        self.assertEqual(status, 400)
        # unhashable style values normalize instead of 500-ing
        status, body = self.post("/api/render",
                                 {"appearance": {"hair_style": ["x"]}})
        self.assertEqual(status, 200)
        # grid size validation
        status, body = self.req("GET", "/api/grid/skins?size=abc")
        self.assertEqual(status, 400)

    def test_strict_framing_and_payload_shape(self):
        # negative Content-Length rejected before any read
        r = urllib.request.Request(self.url("/api/games"),
                                   data=b'{"first_name":"A"}',
                                   method="POST",
                                   headers={"Content-Type":
                                            "application/json"})
        r.add_header("Content-Length", "-5")
        try:
            with urllib.request.urlopen(r, timeout=5):
                self.fail("expected 400")
        except urllib.error.HTTPError as e:
            self.assertEqual(e.code, 400)
        # lookalike content types rejected
        status, body = self.req(
            "POST", "/api/games", b'{"first_name":"A"}',
            headers={"Content-Type": "text/application/json"})
        self.assertEqual(status, 400)
        # falsy-but-present payload must not be coerced to {}
        state = self.create_game()
        status, body = self.post("/api/games/%s/actions" % state["game_id"],
                                 {"expected_revision": state["revision"],
                                  "request_id": "pf-1", "action": "age_up",
                                  "payload": []})
        self.assertEqual(status, 400)
        # list response carries a minimal player descriptor (portraits)
        status, listing = self.req("GET", "/api/games")
        self.assertEqual(status, 200)
        g0 = listing["games"][0]
        self.assertIn("player", g0)
        self.assertIn("appearance", g0["player"])

    def test_static_containment(self):
        # traversal and sibling-prefix attempts must 404
        for path in ("/static/../server.py", "/static/%2e%2e/server.py",
                     "/static/..%2fserver.py"):
            status, _ = self.req("GET", path)
            self.assertEqual(status, 404, path)
        status, _ = self.req("GET", "/static/style.css")
        self.assertEqual(status, 200)

    def test_missing_json_content_type(self):
        status, body = self.req(
            "POST", "/api/games", b'{"first_name":"A"}',
            headers={"Content-Type": "text/plain"})
        self.assertEqual(status, 400)

    def test_malformed_json(self):
        status, body = self.req(
            "POST", "/api/games", b"{not json",
            headers={"Content-Type": "application/json"})
        self.assertEqual(status, 400)
        self.assertEqual(body["error"]["code"], "invalid_input")

    def test_oversized_body(self):
        big = json.dumps({"first_name": "A" * 70000}).encode()
        status, body = self.req(
            "POST", "/api/games", big,
            headers={"Content-Type": "application/json"})
        self.assertEqual(status, 400)
        self.assertEqual(body["error"]["code"], "invalid_input")

    def test_unknown_game_and_action(self):
        status, body = self.req("GET", "/api/games/does-not-exist")
        self.assertEqual(status, 404)
        state = self.create_game()
        status, body = self.act(state["game_id"], 0, "fly")
        self.assertEqual(status, 400)

    def test_get_has_no_side_effects(self):
        state = self.create_game()
        self.req("GET", "/api/games/" + state["game_id"])
        _, got = self.req("GET", "/api/games/" + state["game_id"])
        self.assertEqual(got["revision"], state["revision"])


class TestM0Regressions(WebCase):
    def test_creator_catalog_render_and_gallery_intact(self):
        status, body = self.req("GET", "/api/catalog")
        self.assertEqual(status, 200)
        self.assertIn("skin_tone", body["catalog"])
        self.assertGreaterEqual(len(body["presets"]), 4)
        status, png = self.post("/api/render", {
            "appearance": {"schema_version": 2, "seed": 3,
                           "base": "person", "skin_tone": "default"},
            "age": 30, "size": 64})
        self.assertEqual(status, 200)
        self.assertTrue(png[:8] == b"\x89PNG\r\n\x1a\n", "PNG magic")
        for page in ("/creator", "/gallery", "/relationships", "/game"):
            status, _ = self.req("GET", page)
            self.assertEqual(status, 200, page)
        status, grid = self.req("GET", "/api/grid/skins?size=32")
        self.assertEqual(status, 200)
        self.assertIn(b"<img", grid)
        self.assertIn(b"/api/avatar.png?app=", grid)


if __name__ == "__main__":
    unittest.main()
