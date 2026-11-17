"""Q1 A1 crash-safety tests (Kimi A): rotating crash log + crash id + typed
500, atomic saves with last-good .bak fallback + notice, /api/health and
/api/client-error endpoints, and forced mid-command exceptions proving the
save stays intact.

Run: python3 -m unittest tests.test_game_crash_safety
"""

import http.client
import json
import os
import shutil
import sys
import tempfile
import threading
import unittest
import urllib.error
import urllib.request
from unittest import mock

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402
import web.server as server  # noqa: E402


def make_profile(**overrides):
    profile = {"first_name": "Alex", "last_name": "Rivers",
               "gender": "Male", "country": "Australia", "seed": 42}
    profile.update(overrides)
    return profile


class ServiceSaveSafetyCase(unittest.TestCase):
    """Real GameService on a temp save dir."""

    def setUp(self):
        self.save_dir = tempfile.mkdtemp(prefix="lifesim-a1-save-")
        self.service = GameService(self.save_dir)

    def tearDown(self):
        shutil.rmtree(self.save_dir, ignore_errors=True)

    def _new_game(self):
        state = self.service.new_game(make_profile(), "req-create-1")
        return state["game_id"]

    # -- backup creation ---------------------------------------------------
    def test_save_creates_last_good_backup(self):
        game_id = self._new_game()
        primary = os.path.join(self.save_dir, game_id + ".pickle")
        backup = primary + ".bak"
        self.assertTrue(os.path.isfile(primary))
        self.assertFalse(os.path.isfile(backup))  # no prior good save yet
        self.service.command(game_id, 1, "req-age-1", "age_up", {})
        self.assertTrue(os.path.isfile(backup))
        env = self.service._read_save(backup)
        self.assertEqual(env["game_id"], game_id)
        self.assertEqual(env["revision"], 1)  # backup = previous good state

    # -- mid-command exception never corrupts the save ----------------------
    def test_mid_command_exception_keeps_save_intact(self):
        game_id = self._new_game()
        before = self.service.get_state(game_id)
        with mock.patch("src.game.coordinator.run_turn",
                        side_effect=RuntimeError("boom")):
            with self.assertRaises(RuntimeError):
                self.service.command(game_id, before["revision"], "req-age-2",
                                     "age_up", {})
        after = self.service.get_state(game_id)
        self.assertEqual(after["revision"], before["revision"])
        # the save on disk still loads and a real command succeeds
        state = self.service.command(game_id, before["revision"], "req-age-3",
                                     "age_up", {})
        self.assertEqual(state["revision"], before["revision"] + 1)

    # -- corrupt primary falls back to the backup ---------------------------
    def test_corrupt_primary_recovers_from_backup_with_notice(self):
        game_id = self._new_game()
        self.service.command(game_id, 1, "req-age-4", "age_up", {})
        primary = os.path.join(self.save_dir, game_id + ".pickle")
        with open(primary, "wb") as f:
            f.write(b"\x80not-a-pickle-at-all")
        state = self.service.get_state(game_id)
        self.assertIn("notice", state)  # the user is told
        self.assertEqual(state["revision"], 1)  # last-good backup state
        # primary was restored from the backup
        restored = self.service.get_state(game_id)
        self.assertNotIn("notice", restored)
        self.assertEqual(restored["revision"], 1)

    def test_missing_primary_recovers_from_backup(self):
        game_id = self._new_game()
        self.service.command(game_id, 1, "req-age-6", "age_up", {})
        primary = os.path.join(self.save_dir, game_id + ".pickle")
        os.unlink(primary)
        state = self.service.get_state(game_id)
        self.assertEqual(state["game_id"], game_id)
        self.assertIn("notice", state)
        self.assertEqual(state["revision"], 1)
        self.assertTrue(os.path.isfile(primary))  # restored

    def test_both_primary_and_backup_corrupt_fails_typed(self):
        game_id = self._new_game()
        self.service.command(game_id, 1, "req-age-7", "age_up", {})
        primary = os.path.join(self.save_dir, game_id + ".pickle")
        for path in (primary, primary + ".bak"):
            with open(path, "wb") as f:
                f.write(b"garbage")
        with self.assertRaises(GameError) as ctx:
            self.service.get_state(game_id)
        self.assertEqual(ctx.exception.status, 500)

    def test_backup_removed_with_game(self):
        game_id = self._new_game()
        self.service.command(game_id, 1, "req-age-5", "age_up", {})
        primary = os.path.join(self.save_dir, game_id + ".pickle")
        self.assertTrue(os.path.isfile(primary + ".bak"))
        self.service.delete_game(game_id)
        self.assertFalse(os.path.isfile(primary))
        self.assertFalse(os.path.isfile(primary + ".bak"))


class WebCrashSafetyCase(unittest.TestCase):
    """Real HTTP server on an ephemeral port with temp save + log dirs."""

    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-a1-web-save-")
        cls.log_dir = tempfile.mkdtemp(prefix="lifesim-a1-web-log-")
        cls.log_path = os.path.join(cls.log_dir, "server.log")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir,
                                       log_path=cls.log_path)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)
        shutil.rmtree(cls.log_dir, ignore_errors=True)

    def _post(self, path, body):
        data = json.dumps(body).encode()
        conn = http.client.HTTPConnection("127.0.0.1", self.port)
        conn.request("POST", path, body=data,
                     headers={"Content-Type": "application/json",
                              "Host": "127.0.0.1:%d" % self.port})
        resp = conn.getresponse()
        raw = resp.read()
        conn.close()
        return resp.status, json.loads(raw.decode() or "{}")

    def _get(self, path):
        conn = http.client.HTTPConnection("127.0.0.1", self.port)
        conn.request("GET", path,
                     headers={"Host": "127.0.0.1:%d" % self.port})
        resp = conn.getresponse()
        raw = resp.read()
        conn.close()
        return resp.status, json.loads(raw.decode() or "{}")

    def _create_game(self):
        status, body = self._post("/api/games", {
            "first_name": "Alex", "last_name": "Rivers",
            "gender": "Male", "country": "Australia", "seed": 7,
            "request_id": "req-web-create"})
        self.assertEqual(status, 201, body)
        return body

    def _read_log(self):
        if not os.path.isfile(self.log_path):
            return ""
        with open(self.log_path, encoding="utf-8") as f:
            return f.read()

    # -- endpoints ----------------------------------------------------------
    def test_health_endpoint(self):
        status, body = self._get("/api/health")
        self.assertEqual(status, 200)
        self.assertEqual(body["status"], "ok")

    def test_client_error_endpoint_logs_and_accepts(self):
        status, body = self._post("/api/client-error", {
            "kind": "unhandledrejection", "message": "undefined is not a",
            "url": "/game", "stack": "f()/g()", "game_id": "abc"})
        self.assertEqual(status, 200)
        self.assertEqual(body, {"ok": True})
        self.assertIn("client-error", self._read_log())
        self.assertIn("undefined is not a", self._read_log())

    def test_client_error_rejects_bad_body(self):
        status, body = self._post("/api/client-error", {"no_message": 1})
        self.assertEqual(status, 400)
        self.assertEqual(body["error"]["code"], "invalid_input")

    # -- crash path ----------------------------------------------------------
    def test_forced_exception_returns_typed_500_and_logs_crash(self):
        state = self._create_game()
        game_id = state["game_id"]
        revision = state["revision"]
        with mock.patch("src.game.coordinator.run_turn",
                        side_effect=RuntimeError("web boom")):
            status, body = self._post(
                "/api/games/%s/actions" % game_id, {
                    "request_id": "req-web-age-1",
                    "expected_revision": revision, "action": "age_up",
                    "payload": {}})
        self.assertEqual(status, 500)
        self.assertEqual(body["error"], "internal")
        self.assertTrue(body["crash_id"])
        log = self._read_log()
        self.assertIn(body["crash_id"], log)
        self.assertIn("RuntimeError", log)          # traceback logged
        self.assertIn("web boom", log)
        self.assertIn("req-web-age-1", log)         # request summary
        # the save is intact: state loads and a real command succeeds
        status, after = self._get("/api/games/%s" % game_id)
        self.assertEqual(status, 200)
        self.assertEqual(after["revision"], revision)
        status, ok = self._post("/api/games/%s/actions" % game_id, {
            "request_id": "req-web-age-2",
            "expected_revision": revision, "action": "age_up",
            "payload": {}})
        self.assertEqual(status, 200, ok)
        self.assertEqual(ok["revision"], revision + 1)

    def test_corrupt_save_over_http_recovers_with_notice(self):
        state = self._create_game()
        game_id = state["game_id"]
        status, _ = self._post("/api/games/%s/actions" % game_id, {
            "request_id": "req-web-age-3",
            "expected_revision": state["revision"], "action": "age_up",
            "payload": {}})
        self.assertEqual(status, 200)
        primary = os.path.join(self.save_dir, game_id + ".pickle")
        with open(primary, "wb") as f:
            f.write(b"corrupt!")
        status, body = self._get("/api/games/%s" % game_id)
        self.assertEqual(status, 200)
        self.assertIn("notice", body)
        self.assertEqual(body["revision"], state["revision"])


if __name__ == "__main__":
    unittest.main()
