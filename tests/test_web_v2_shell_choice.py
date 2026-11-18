"""v2 Choice tests (Kimi B): node DOM behaviour of web/static/v2/choice.js
plus static-serving checks. Run: python3 -m unittest tests.test_web_v2_shell_choice
"""

import os
import subprocess
import sys
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)


class V2ChoiceDomCase(unittest.TestCase):
    def test_dom_behaviour(self):
        res = subprocess.run(
            ["node", os.path.join(ROOT, "tests", "web_v2_choice_dom.mjs")],
            capture_output=True, text=True)
        self.assertEqual(res.returncode, 0,
                         "DOM tests failed:\n" + res.stdout + res.stderr)


class V2ChoiceServedCase(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        import shutil
        import tempfile
        import threading
        import web.server as server
        cls.save_dir = tempfile.mkdtemp(
            prefix="lifesim-v2c-test-",
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

    def test_choice_js_and_inputs_js_served(self):
        for name, marker in (("choice.js", b"export function mountChoiceSheet"),
                             ("inputs.js", b"export function buildInputRow")):
            status, ctype, body = self._get("/static/v2/" + name)
            self.assertEqual(status, 200, name)
            self.assertIn("javascript", ctype)
            self.assertIn(marker, body)

    def test_game_html_sheet_modal(self):
        _, _, body = self._get("/game")
        self.assertIn(b'modal-box v2-sheet', body)
        self.assertIn(b'id="modalCancel"', body)
        # legacy skeleton ids removed (content is rendered dynamically)
        self.assertNotIn(b'id="modalOptions"', body)


if __name__ == "__main__":
    unittest.main()
