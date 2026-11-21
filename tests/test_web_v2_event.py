"""v2 Event modal tests (Kimi C, F5/F6): DOM behaviour of
web/static/v2/event.js via node (tests/web_v2_event_dom.mjs), plus a smoke
check that the module and stylesheet are syntactically valid ES/CSS and
served by the web server.

Run: python3 -m unittest tests.test_web_v2_event
"""

import os
import subprocess
import sys
import threading
import unittest
import shutil
import tempfile
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)


class V2EventDomCase(unittest.TestCase):
    def test_dom_behaviour(self):
        res = subprocess.run(
            ["node", os.path.join(ROOT, "tests", "web_v2_event_dom.mjs")],
            capture_output=True, text=True)
        self.assertEqual(res.returncode, 0,
                         "DOM tests failed:\n" + res.stdout + res.stderr)


class V2EventServedCase(unittest.TestCase):
    """The static files exist and are served with correct content types."""

    @classmethod
    def setUpClass(cls):
        import web.server as server
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-v2e-test-")
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

    def test_event_js_served(self):
        status, ctype, body = self._get("/static/v2/event.js")
        self.assertEqual(status, 200)
        self.assertIn("javascript", ctype)
        self.assertIn(b"export function mountEventModal", body)

    def test_event_css_served(self):
        status, ctype, body = self._get("/static/v2/event.css")
        self.assertEqual(status, 200)
        self.assertIn("css", ctype)
        self.assertIn(b"v2e-card", body)


if __name__ == "__main__":
    unittest.main()
