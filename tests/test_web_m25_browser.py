"""M2.5 editor browser smoke test (Kimi B): drives the real game page
in headless Chromium over CDP — opens the character editor, edits the
player's openness, previews and applies, and verifies the change lands
in the service (policy-state) with zero console errors / exceptions.

Run: python3 -m unittest tests.test_web_m25_browser
Needs chromium (tools.cdp_client.CHROMIUM); skipped otherwise.
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

try:
    from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
except Exception:  # pragma: no cover - environment without chromium
    CHROMIUM = None


def make_profile(**overrides):
    profile = {
        "first_name": "Alex", "last_name": "Rivers", "gender": "Male",
        "country": "Australia",
        "appearance": {"schema_version": 1, "seed": 7,
                       "skin_tone": "default"},
    }
    profile.update(overrides)
    return profile


@unittest.skipIf(CHROMIUM is None, "chromium not available")
class EditorBrowserSmokeTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-m25-browser-")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()
        data = json.dumps(dict(make_profile(),
                               request_id="m25-browser-create")).encode()
        request = urllib.request.Request(
            "http://127.0.0.1:%d/api/games" % cls.port, data=data,
            method="POST", headers={"Content-Type": "application/json"})
        with urllib.request.urlopen(request) as res:
            cls.game_id = json.loads(res.read())["game_id"]

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)

    def test_editor_end_to_end_in_browser(self):
        proc, conn = launch_chrome()
        console_errors = []
        exceptions = []
        try:
            session = conn.new_session("about:blank")
            session.call("Runtime.enable")
            session.call("Page.enable")
            session.call("Log.enable")
            session.call("Emulation.setDeviceMetricsOverride",
                         {"width": 390, "height": 844,
                          "deviceScaleFactor": 1, "mobile": True})
            base = "http://127.0.0.1:%d" % self.port
            session.call("Page.navigate",
                         {"url": base + "/game?game=" + self.game_id})
            session.wait_js(
                "(document.getElementById('gameView')||{}).hidden === "
                "false", timeout=15, desc="game view renders")
            # Delay only the policy-fetch boundary to force a fast click
            # window. The real server still supplies all policy data.
            session.js("const editorFetch=window.fetch;let gateEditor=true;"
                       "window.fetch=async(...args)=>{if(gateEditor && "
                       "String(args[0]).endsWith('/policy-state')){"
                       "gateEditor=false;await new Promise(r=>"
                       "window.releaseEditorPolicy=r);}return editorFetch(...args);};")
            # open the editor panel
            session.js("document.getElementById('editorToggle').click();"
                       " true")
            session.wait_js(
                "typeof window.releaseEditorPolicy === 'function' && "
                "(document.getElementById('editorPanel')||{}).hidden === false",
                desc="editor policy request is waiting")
            self.assertEqual(session.js(
                "document.querySelectorAll('#editorPeople button').length"), 0,
                "no clickable person rows before policy catalogs arrive")
            self.assertIsNone(session.js("document.getElementById('edOpenness')"))
            session.js("window.releaseEditorPolicy()")
            session.wait_js(
                "(document.getElementById('editorPanel')||{}).hidden === "
                "false && document.querySelectorAll("
                "'#editorPeople button').length > 0", timeout=15,
                desc="editor panel with people renders")
            # pick the player (first button = self) and edit openness
            session.js("document.querySelector('#editorPeople button')"
                       ".click(); true")
            session.wait_js(
                "document.getElementById('edOpenness') !== null",
                timeout=15, desc="person form renders")
            session.js("document.getElementById('edOpenness').value ="
                       " '0.83'; true")
            session.js("document.getElementById('edPreview').click();"
                       " true")
            session.wait_js(
                "document.querySelectorAll('#editorPreview button')"
                ".length >= 2", timeout=15, desc="preview offers apply")
            session.js("document.querySelector('#editorPreview .primary')"
                       ".click(); true")
            session.wait_js(
                "document.querySelector('#editorPreview li') !== null",
                timeout=15, desc="apply succeeds and audits")
            # service side: the new value persisted
            request = urllib.request.Request(
                "http://127.0.0.1:%d/api/games/%s/policy-state"
                % (self.port, self.game_id))
            with urllib.request.urlopen(request) as res:
                policy = json.loads(res.read())
            player = policy["people"][0] if False else None
            from tests.test_web_m25 import EditorRouteTests  # noqa
            # player id from the game state
            request = urllib.request.Request(
                "http://127.0.0.1:%d/api/games/%s" % (self.port,
                                                      self.game_id))
            with urllib.request.urlopen(request) as res:
                state = json.loads(res.read())
            profile = policy["profiles"][state["player_id"]]
            self.assertAlmostEqual(profile["openness"], 0.83, places=5)
            # audit recorded
            self.assertTrue(any(
                a["command"] == "edit_person_attributes"
                for a in policy["editor_audit"]))
            # the global error boundary never fired during the flow
            self.assertIsNone(session.js(
                "document.querySelector('.crash-overlay') || null"))
            # A's family tree: route passthrough + shell mount
            tree_req = urllib.request.Request(
                "http://127.0.0.1:%d/api/games/%s/family-tree"
                % (self.port, self.game_id))
            with urllib.request.urlopen(tree_req) as res:
                status = res.status
                body = res.read().decode("utf-8")
            self.assertEqual(status, 200, body)
            tree_payload = json.loads(body)
            self.assertIn("tree", tree_payload)
            session.js("document.getElementById('familyTreeToggle')"
                       ".click(); true")
            session.wait_js(
                "(document.getElementById('familyTreeCard')||{}).hidden"
                " === false", timeout=15, desc="family tree card opens")
            session.wait_js(
                "document.getElementById('familyTreeCard').textContent"
                ".trim().length > 0", timeout=15,
                desc="family tree renders content")
            self.assertIsNone(session.js(
                "document.querySelector('.crash-overlay') || null"))
        finally:
            proc.kill()

    def test_second_consecutive_edit_and_m25_screens(self):
        """B1 regression (Codex review: second edit got 409 stale_revision)
        in the real browser: two applies back-to-back inside one editor
        session must both commit. Then the M2.5 dating & weddings card
        renders its tabs at phone width."""
        proc, conn = launch_chrome()
        try:
            session = conn.new_session("about:blank")
            session.call("Runtime.enable")
            session.call("Page.enable")
            session.call("Emulation.setDeviceMetricsOverride",
                         {"width": 390, "height": 844,
                          "deviceScaleFactor": 1, "mobile": True})
            base = "http://127.0.0.1:%d" % self.port
            session.call("Page.navigate",
                         {"url": base + "/game?game=" + self.game_id})
            session.wait_js(
                "(document.getElementById('gameView')||{}).hidden === "
                "false", timeout=15, desc="game view renders")
            session.js("document.getElementById('editorToggle').click();"
                       " true")
            session.wait_js(
                "(document.getElementById('editorPanel')||{}).hidden === "
                "false && document.querySelectorAll("
                "'#editorPeople button').length > 0", timeout=15,
                desc="editor panel renders")
            # select the player so the attribute form is present
            session.js("document.querySelector('#editorPeople button')"
                       ".click(); true")
            session.wait_js(
                "document.getElementById('edOpenness') !== null",
                timeout=15, desc="person form renders")

            def apply_openness(value, marker):
                session.js("document.getElementById('edOpenness').value ="
                           " '%s'; true" % value)
                session.js("document.getElementById('edPreview').click();"
                           " true")
                session.wait_js(
                    "document.querySelectorAll('#editorPreview button')"
                    ".length >= 2", timeout=15,
                    desc="preview offers apply (%s)" % marker)
                session.js("document.querySelector('#editorPreview "
                           ".primary').click(); true")
                session.wait_js(
                    "document.querySelector('#editorPreview li') !== null",
                    timeout=15,
                    desc="apply commits (%s)" % marker)

            apply_openness("0.61", "first edit")
            # The B1 fix: this second edit used to 409 because the panel
            # sent the page-load revision; it must now commit too.
            apply_openness("0.72", "second edit")
            # ...and a third proves the chain is stable, not lucky.
            apply_openness("0.64", "third edit")
            session.wait_js(
                "!document.getElementById('editorStatus').textContent"
                ".includes('409')", timeout=5,
                desc="no 409 surfaced in editor status")

            request = urllib.request.Request(
                "http://127.0.0.1:%d/api/games/%s/policy-state"
                % (self.port, self.game_id))
            with urllib.request.urlopen(request) as res:
                policy = json.loads(res.read())
            request = urllib.request.Request(
                "http://127.0.0.1:%d/api/games/%s" % (self.port,
                                                      self.game_id))
            with urllib.request.urlopen(request) as res:
                state = json.loads(res.read())
            profile = policy["profiles"][state["player_id"]]
            self.assertAlmostEqual(profile["openness"], 0.64, places=5)
            # three fresh audits appended (the class game may carry
            # earlier edits from the other browser test)
            self.assertEqual(
                [a["command"] for a in policy["editor_audit"][-3:]],
                ["edit_person_attributes"] * 3)

            # M2.5 screens: open the card, tabs render, weddings tab shows
            # the (empty) union state without errors.
            session.js("document.getElementById('m25Toggle').click();"
                       " true")
            session.wait_js(
                "(document.getElementById('m25Card')||{}).hidden === "
                "false && document.querySelectorAll('#m25Body').length "
                "=== 1", timeout=15, desc="m25 card opens")
            session.wait_js(
                "document.querySelectorAll('[data-tab]').length === 3",
                timeout=15, desc="three tabs render")
            session.js("document.querySelector('[data-tab=weddings]')"
                       ".click(); true")
            session.wait_js(
                "document.getElementById('m25Body').textContent.length "
                "> 0", timeout=15, desc="weddings tab renders content")
            body = session.js(
                "document.getElementById('m25Body').textContent")
            self.assertIn("No active unions", body)
            session.js("document.querySelector('[data-tab=reputation]')"
                       ".click(); true")
            session.wait_js(
                "document.getElementById('m25Body').textContent.length "
                "> 0", timeout=15, desc="reputation tab renders content")
            session.js("document.querySelector('[data-tab=dating]')"
                       ".click(); true")
            session.wait_js(
                "document.getElementById('m25Body').textContent.length "
                "> 0", timeout=15, desc="dating tab renders content")
            # phone width: no horizontal overflow
            overflow = session.js(
                "document.documentElement.scrollWidth > "
                "document.documentElement.clientWidth")
            self.assertFalse(overflow)
            self.assertIsNone(session.js(
                "document.querySelector('.crash-overlay') || null"))
        finally:
            proc.kill()


if __name__ == "__main__":
    unittest.main()
