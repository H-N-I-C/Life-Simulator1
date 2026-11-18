"""v2 shell tests (Kimi B): DOM behaviour of web/static/v2/shell.js via node
(tests/web_v2_shell_dom.mjs), plus static-serving and typed-404 checks for
the frozen §Shell person-detail route.

Run: python3 -m unittest tests.test_web_v2_shell
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


class V2ShellDomCase(unittest.TestCase):
    def test_dom_behaviour(self):
        res = subprocess.run(
            ["node", os.path.join(ROOT, "tests", "web_v2_shell_dom.mjs")],
            capture_output=True, text=True)
        self.assertEqual(res.returncode, 0,
                         "DOM tests failed:\n" + res.stdout + res.stderr)


class V2ShellServedCase(unittest.TestCase):
    """Static files served; person-detail route answers the frozen typed
    404 (A's projection lands in Stage 3)."""

    @classmethod
    def setUpClass(cls):
        import web.server as server
        # NOTE: /tmp may be quota-exhausted on this machine; keep the test
        # save dir inside the project (A hit the same tmpfs issue).
        cls.save_dir = tempfile.mkdtemp(
            prefix="lifesim-v2s-test-",
            dir=os.path.join(ROOT, "game_saves"))
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

    def _get(self, path):
        with urllib.request.urlopen(
                "http://127.0.0.1:%d%s" % (self.port, path)) as r:
            return r.status, r.headers.get("Content-Type", ""), r.read()

    def test_shell_js_served(self):
        status, ctype, body = self._get("/static/v2/shell.js")
        self.assertEqual(status, 200)
        self.assertIn("javascript", ctype)
        self.assertIn(b"export function mountShell", body)

    def test_theme_css_served(self):
        status, ctype, body = self._get("/static/v2/theme.css")
        self.assertEqual(status, 200)
        self.assertIn("text/css", ctype)
        self.assertIn(b"--v2-accent", body)

    def test_game_html_links_theme_and_v2_screens(self):
        _, _, body = self._get("/game")
        html = body.decode("utf-8", "replace")
        self.assertIn('/static/v2/theme.css', html)
        for screen in ("home", "job", "assets", "people", "activities"):
            self.assertIn('id="screen-%s"' % screen, html)
        # legacy tabbar replaced; every legacy panel id preserved
        self.assertNotIn('id="tabbar"', html)
        for panel in ("v2HomeMount", "educationCard", "workCard",
                      "financesCard", "familyList", "healthCard",
                      "actionList", "editorToggle", "familyTreeToggle",
                      "m25Toggle", "backToLives", "journalList"):
            self.assertIn('id="%s"' % panel, html)

    def test_person_detail_unknown_id_typed_404(self):
        url = ("http://127.0.0.1:%d/api/games/no-such-game/people/p1"
               % self.port)
        try:
            with urllib.request.urlopen(url) as r:
                status, body = r.status, r.read()
        except urllib.error.HTTPError as e:
            status, body = e.code, e.read()
        self.assertEqual(status, 404)
        payload = json.loads(body.decode("utf-8"))
        self.assertIn(payload["error"]["code"],
                      ("person_not_found", "not_found"))


if __name__ == "__main__":
    unittest.main()
