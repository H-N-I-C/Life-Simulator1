"""v2 Home tests (Kimi B): node DOM behaviour of web/static/v2/home.js plus
static-serving checks. Run: python3 -m unittest tests.test_web_v2_shell_home
"""

import os
import subprocess
import sys
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)


class V2HomeDomCase(unittest.TestCase):
    def test_dom_behaviour(self):
        res = subprocess.run(
            ["node", os.path.join(ROOT, "tests", "web_v2_home_dom.mjs")],
            capture_output=True, text=True)
        self.assertEqual(res.returncode, 0,
                         "DOM tests failed:\n" + res.stdout + res.stderr)

    def test_async_harness_reports_rejections_as_failures(self):
        result = subprocess.run(
            ["node", os.path.join(ROOT, "tests", "web_v2_home_dom.mjs"),
             "--async-failure-probe"], capture_output=True, text=True)
        self.assertEqual(result.returncode, 1)
        self.assertIn("FAIL - awaited async failure probe", result.stderr)
        self.assertNotIn("ok - awaited async failure probe", result.stdout)



class V2HomeServedCase(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        import shutil
        import tempfile
        import threading
        import web.server as server
        cls.save_dir = tempfile.mkdtemp(
            prefix="lifesim-v2h-test-",
            dir=os.path.join(ROOT, "game_saves"))
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()
        cls._shutil = shutil

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        cls._shutil.rmtree(cls.save_dir, ignore_errors=True)

    def _get(self, path):
        import urllib.request
        with urllib.request.urlopen(
                "http://127.0.0.1:%d%s" % (self.port, path)) as r:
            return r.status, r.headers.get("Content-Type", ""), r.read()

    def test_home_js_served(self):
        status, ctype, body = self._get("/static/v2/home.js")
        self.assertEqual(status, 200)
        self.assertIn("javascript", ctype)
        self.assertIn(b"export function mountHome", body)

    def test_game_html_hosts_home_mount(self):
        _, _, body = self._get("/game")
        self.assertIn(b'id="v2HomeMount"', body)


if __name__ == "__main__":
    unittest.main()
