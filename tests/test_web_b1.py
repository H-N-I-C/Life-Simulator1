"""Q1 B1 web resilience tests (Kimi B): the front-end assets are served with
the error-boundary wiring, load/empty/offline states exist on every page,
duplicate-submit guards are present in game.js, and the continue_as command
(A) is reachable through the same retry-stable action endpoint the UI now
uses (the previous client path called an undefined helper and would have
crashed).

Run: python3 -m unittest tests.test_web_b1
"""

import json
import os
import shutil
import subprocess
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


class WebCase(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-b1-test-")
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

    def url(self, path):
        return "http://127.0.0.1:%d%s" % (self.port, path)

    def req(self, method, path, body=None, headers=None):
        data = None
        hdrs = {}
        if body is not None:
            data = json.dumps(body).encode("utf-8")
            hdrs["Content-Type"] = "application/json"
        if headers:
            hdrs.update(headers)
        request = urllib.request.Request(self.url(path), data=data,
                                         method=method, headers=hdrs)
        try:
            with urllib.request.urlopen(request) as res:
                return res.status, res.read().decode("utf-8")
        except urllib.error.HTTPError as e:
            return e.code, e.read().decode("utf-8")

    def static(self, name):
        status, body = self.req("GET", "/static/" + name)
        self.assertEqual(status, 200, name)
        return body

    # ---- pages and assets are served with the B1 wiring ----

    def test_pages_served(self):
        for page in ("/game", "/creator", "/gallery", "/relationships"):
            status, _ = self.req("GET", page)
            self.assertEqual(status, 200, page)

    def test_error_boundary_wired_in_common_js(self):
        js = self.static("common.js")
        self.assertIn("/api/client-error", js)
        self.assertIn("unhandledrejection", js)
        self.assertIn("installErrorBoundary", js)
        self.assertIn("offlineBanner", js)
        self.assertIn("Your save is safe", js)

    def test_game_js_guards_and_states(self):
        js = self.static("game.js")
        # duplicate-submit guards
        self.assertIn("x.disabled = true", js)
        self.assertIn("submit.disabled = true", js)
        # empty states
        self.assertIn("Nothing has happened yet", js)
        self.assertIn("No family or relationships yet", js)
        # fatal load error view + retry handler
        self.assertIn("showFatalLoadError", js)
        self.assertIn("setErrorRetryHandler", js)
        # continue_as goes through the retry-stable operation path
        self.assertIn('startOperation("continue_as"', js)
        self.assertNotIn("attempt(", js.replace("attempt(\n", ""))

    def test_html_states(self):
        game = self.static("game.html")
        self.assertIn('id="pageLoading"', game)
        creator = self.static("creator.html")
        self.assertIn("Loading…", creator)
        relationships = self.static("relationships.html")
        self.assertIn("Loading…", relationships)
        self.assertIn("load-error", relationships)
        gallery = self.static("gallery.html")
        self.assertIn("Loading…", gallery)
        self.assertIn("common.js", gallery)

    def test_css_resilience_styles(self):
        css = self.static("style.css")
        for marker in (".crash-card", ".offline-banner", ".load-error"):
            self.assertIn(marker, css)

    # ---- continue_as via the standard action endpoint ----

    def test_continue_as_unknown_target_is_typed_error(self):
        status, body = self.req("POST", "/api/games",
                                dict(make_profile(), request_id="r-b1-new"))
        self.assertEqual(status, 201, body)
        game_id = json.loads(body)["game_id"]
        status, body = self.req(
            "POST", "/api/games/%s/actions" % game_id,
            {"expected_revision": 1, "request_id": "r-b1-1",
             "action": "continue_as", "payload": {"target_id": "nobody"}})
        self.assertNotEqual(status, 500, body)  # never a raw server fault
        err = json.loads(body)["error"]
        self.assertIn(err["code"],
                      ("invalid_input", "not_found", "invalid_target",
                       "ineligible", "game_over", "conflict"))

    def test_client_error_bad_shape_is_typed_400(self):
        # Frozen shape (q1-a1-report.md): message required string; the
        # client never sends extra fields.
        status, body = self.req("POST", "/api/client-error", {})
        self.assertEqual(status, 400, body)
        self.assertEqual(json.loads(body)["error"]["code"], "invalid_input")
        status, body = self.req(
            "POST", "/api/client-error",
            {"message": "m", "stack": {"not": "a string"}})
        self.assertEqual(status, 400, body)

    # ---- end-to-end: a real client report lands in logs/server.log ----

    def test_client_error_lands_in_server_log(self):
        log_dir = tempfile.mkdtemp(prefix="lifesim-b1-log-")
        log_path = os.path.join(log_dir, "server.log")
        httpd = server.make_server(0, save_dir=self.save_dir,
                                   log_path=log_path)
        thread = threading.Thread(target=httpd.serve_forever, daemon=True)
        thread.start()
        try:
            port = httpd.server_port
            # Exactly what web/static/common.js sends for an uncaught
            # error on the game page (frozen A1 shape).
            payload = {
                "kind": "error",
                "message": "e2e boundary probe",
                "stack": "at /static/game.js:3:7\nError: e2e boundary probe",
                "url": "/game",
                "game_id": "g_e2e",
            }
            data = json.dumps(payload).encode("utf-8")
            request = urllib.request.Request(
                "http://127.0.0.1:%d/api/client-error" % port,
                data=data, method="POST",
                headers={"Content-Type": "application/json"})
            with urllib.request.urlopen(request) as res:
                self.assertEqual(res.status, 200)
                self.assertEqual(json.loads(res.read()), {"ok": True})
            # The rotating handler may buffer; flush by reaching the
            # logger directly.
            for handler in server._LOGGER.handlers:
                handler.flush()
            with open(log_path, encoding="utf-8") as f:
                log = f.read()
            self.assertIn("client-error", log)
            self.assertIn("kind=error", log)
            self.assertIn("url=/game", log)
            self.assertIn("game_id=g_e2e", log)
            self.assertIn("e2e boundary probe", log)
        finally:
            httpd.shutdown()
            httpd.server_close()
            shutil.rmtree(log_dir, ignore_errors=True)

    # ---- DOM behaviour tests (node) ----

    def test_dom_behaviour(self):
        proc = subprocess.run(
            ["node", os.path.join(ROOT, "tests", "web_b1_dom.mjs")],
            capture_output=True, text=True, cwd=ROOT)
        self.assertEqual(proc.returncode, 0,
                         proc.stdout + "\n" + proc.stderr)
        self.assertIn("9 DOM behaviour tests done", proc.stdout)


if __name__ == "__main__":
    unittest.main()
