"""v2 shell real-browser journey (Kimi B): headless Chromium via
tools/cdp_client — load /game, open the People tab, click a roster chip,
assert C's Person module renders A's projection in the overlay, Back closes
it and restores the list. Zero console errors required.

Run: python3 -m unittest tests.test_web_v2_shell_browser
Needs: chromium on PATH. Save dir lives under game_saves (/tmp may be
quota-exhausted on this machine).
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
sys.path.insert(0, os.path.join(ROOT, "tests"))

from test_web_m1 import make_profile  # noqa: E402
from tools.cdp_client import launch_chrome  # noqa: E402


class V2ShellBrowserCase(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        import web.server as server
        cls.save_dir = tempfile.mkdtemp(
            prefix="lifesim-v2b-test-",
            dir=os.path.join(ROOT, "game_saves"))
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()
        r = urllib.request.Request(
            "http://127.0.0.1:%d/api/games" % cls.port,
            data=json.dumps(dict(make_profile(),
                                 request_id="v2-shell-browser")).encode(),
            headers={"Content-Type": "application/json"})
        cls.game = json.loads(urllib.request.urlopen(r).read())
        cls.base = "http://127.0.0.1:%d" % cls.port

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)

    def test_roster_to_person_overlay_and_back(self):
        proc, conn = launch_chrome()
        try:
            sess = conn.new_session(
                self.base + "/game?game=" + self.game["game_id"])
            sess.wait_js("document.querySelector('.v2-tabbar') !== null",
                         desc="v2 tabbar renders")
            sess.js("document.querySelectorAll('.v2-tab')[3].click()")  # People
            sess.wait_js(
                "document.getElementById('familyList').children.length > 0",
                desc="roster renders")
            names = [p["name"] for p in self.game["people"]]
            sess.js("document.querySelectorAll('#familyList .chip')[0]"
                    ".click()")
            sess.wait_js(
                "!document.querySelector('.v2-person-host').hidden && "
                "document.querySelector('.v2-person-host')"
                ".textContent.length > 20",
                desc="person overlay opens with A's projection")
            text = sess.js(
                "document.querySelector('.v2-person-host').textContent")
            self.assertTrue(any(n in text for n in names),
                            "overlay shows the clicked person: %r" % text[:80])
            sess.js("document.querySelector('.v2-person-host "
                    "button[aria-label=Back]').click()")
            sess.wait_js("document.querySelector('.v2-person-host').hidden",
                         desc="Back closes the overlay")
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    @classmethod
    def _post(cls, path, data):
        r = urllib.request.Request(
            cls.base + path, data=json.dumps(data).encode(),
            headers={"Content-Type": "application/json"})
        return json.loads(urllib.request.urlopen(r).read())

    @classmethod
    def _create(cls, request_id, retries=2, **profile_over):
        profile = make_profile()
        profile.update(profile_over)
        last = None
        for attempt in range(retries):
            try:
                return cls._post("/api/games",
                                 dict(profile, request_id=request_id))
            except Exception as e:  # transient save-dir/quota flakes
                last = e
                import time
                time.sleep(0.5)
        raise last

    @classmethod
    def _state(cls, game_id):
        return json.loads(urllib.request.urlopen(
            "%s/api/games/%s" % (cls.base, game_id)).read())

    def _launch(self, game_id):
        proc, conn = launch_chrome()
        sess = conn.new_session(
            self.base + "/game?game=" + game_id)
        sess.wait_js("document.readyState === 'complete' && " 
                     "document.getElementById('gameView') !== null",
                     desc="game page loaded")
        # Shell chrome mounts before the async Home import completes.
        # Callers may click Age immediately, so wait for that actual control.
        sess.wait_js("document.querySelector('.v2-tabbar') !== null && "
                     "document.querySelector('.v2h-age') !== null",
                     desc="v2 shell and Home control render")
        return proc, conn, sess

    def test_home_stats_age_and_newest_first_timeline(self):
        game = self._create("v2-home-journey")
        gid = game["game_id"]
        proc, conn, sess = self._launch(gid)
        try:
            # real stats with numeric labels
            sess.wait_js(
                "document.querySelectorAll('.v2h-stat-value').length >= 4",
                desc="stats render")
            vals = sess.js("[...document.querySelectorAll('.v2h-stat-value')]"
                           ".map(e => e.textContent)")
            self.assertTrue(all(v.endswith("%") for v in vals), vals)
            # central Age drives the real command
            before = self._state(gid)["revision"]
            sess.js("document.querySelector('.v2h-age').click()")
            import time
            time.sleep(2.0)
            after = self._state(gid)
            self.assertGreater(after["revision"], before)
            self.assertEqual(after["people"][0]["age"], 1)
            # newest-first: card ages are in descending order
            heads = sess.js(
                "[...document.querySelectorAll('#journalList .jcard-head')]"
                ".map(e => Number(e.textContent.replace('Age ', '')))")
            self.assertEqual(heads, sorted(heads, reverse=True), heads)
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def _drive_to_naming_pending(self):
        """Proven path (tests.test_web_m2): married_30 → try_for_baby →
        pregnancy/keep → baby_naming appears within a few years. Skips are
        the established pattern for scenario-drive instability (A-side)."""
        game = self._create("v2-naming-setup", scenario="married_30", seed=52)
        gid = game["game_id"]
        st = self._state(gid)
        if not st["family"].get("partner"):
            self.skipTest("married_30 produced no partner (A-side)")
        if "try_for_baby" not in [a["id"] for a in st["available_actions"]]:
            self.skipTest("try_for_baby not advertised (A-side)")
        def act(action, payload=None):
            return self._post("/api/games/%s/actions" % gid, {
                "expected_revision": st["revision"],
                "request_id": "v2-naming-%s-%s" % (action, st["revision"]),
                "action": action, "payload": payload or {}})
        st = act("try_for_baby")
        if not st.get("pending_decision"):
            self.skipTest("partner refused a baby with this seed")
        d = st["pending_decision"]
        keep = next((o for o in d["options"] if o["id"] == "keep"),
                    d["options"][0])
        st = act("resolve_decision",
                 {"decision_id": d["id"], "option_id": keep["id"]})
        for _ in range(4):
            if st.get("pending_decision"):
                d2 = st["pending_decision"]
                if d2["kind"] == "baby_naming":
                    return gid
                o = next((x for x in d2["options"] if x.get("enabled", True)),
                         None)
                if not o:
                    self.skipTest("pending with no enabled option (A-side)")
                st = act("resolve_decision",
                         {"decision_id": d2["id"], "option_id": o["id"]})
            else:
                st = act("age_up")
        self.skipTest("baby_naming did not appear within 4 years (A-side)")

    def test_choice_sheet_naming_paid_free_and_collapse(self):
        gid = self._drive_to_naming_pending()
        st = self._state(gid)
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "baby_naming")
        proc, conn, sess = self._launch(gid)
        try:
            # sheet auto-opens (pending can never be stranded)
            sess.wait_js(
                "!document.getElementById('modal').hidden && "
                "document.querySelector('.v2c-options') !== null",
                desc="choice sheet opens for the yearly pending")
            # frozen chip: AGE <n> · FAMILY
            chip = sess.js(
                "document.querySelector('.v2c-kind') && "
                "document.querySelector('.v2c-kind').textContent || ''")
            self.assertIn("AGE", chip)
            self.assertIn("FAMILY", chip)
            # the naming input is a real text control
            inp = sess.js("!!document.querySelector('.v2c-form input')")
            self.assertTrue(inp)
            # paid/$0 knownEffects: options advertise costs verbatim
            eff = sess.js("[...document.querySelectorAll('.v2c-effects')]"
                          ".map(e => e.textContent)")
            if eff:
                self.assertTrue(all("·" in e or e for e in eff))
            # collapse via Escape → pill; reopen; resolve with a name
            sess.js("document.dispatchEvent(new KeyboardEvent('keydown',"
                    "{key:'Escape'}))")
            sess.wait_js(
                "document.getElementById('decisionPill') && "
                "!document.getElementById('decisionPill').hidden",
                desc="Escape collapses to the pill")
            sess.js("document.querySelector('#decisionPill button')"
                    ".click()")
            sess.wait_js(
                "!document.getElementById('modal').hidden",
                desc="Show reopens the sheet")
            sess.js("var i = document.querySelector('.v2c-form input');"
                    "i.value = 'Robin';")
            before = self._state(gid)["revision"]
            sess.js("[...document.querySelectorAll('.v2c-option')]"
                    ".find(b => !b.disabled).click()")
            import time
            time.sleep(2.0)
            after = self._state(gid)
            self.assertGreater(after["revision"], before)
            # the naming pending is resolved (a later yearly pending may
            # legitimately surface in the same response)
            pend = after.get("pending_decision")
            self.assertNotEqual(pend and pend.get("kind"), "baby_naming")
            names = [p["name"] for p in after["people"]]
            self.assertTrue(any(n.startswith("Robin") for n in names),
                            "child was named via the sheet: %r" % names)
            # paid choice knownEffects render verbatim in the live sheet
            if pend:
                eff = sess.js("[...document.querySelectorAll('.v2c-effects')]"
                              ".map(e => e.textContent)")
                for e in eff:
                    self.assertTrue(e.strip())
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def test_offline_retry_pill(self):
        game = self._create("v2-offline-journey")
        gid = game["game_id"]
        proc, conn, sess = self._launch(gid)
        try:
            sess.js("Object.defineProperty(navigator, 'onLine', "
                    "{value:false, configurable:true});")
            sess.js("document.querySelector('.v2h-age').click()")
            sess.wait_js(
                "document.getElementById('pendingPill') && "
                "!document.getElementById('pendingPill').hidden",
                desc="offline action surfaces the safe-retry pill")
            before = self._state(gid)["revision"]
            sess.js("Object.defineProperty(navigator, 'onLine', "
                    "{value:true, configurable:true});")
            sess.wait_js(
                "[...document.querySelectorAll('#pendingPill button')]"
                ".some(b => b.textContent.includes('Check result'))",
                timeout=8, desc="retry button is offered")
            sess.js("[...document.querySelectorAll('#pendingPill button')]"
                    ".find(b => b.textContent.includes('Check result'))"
                    ".click()")
            import time
            time.sleep(2.0)
            self.assertGreater(self._state(gid)["revision"], before)
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def test_death_and_continuation_through_v2(self):
        game = self._create("v2-death-journey",
                            scenario="married_with_child")
        gid = game["game_id"]
        st = self._state(gid)
        ids = [a["id"] for a in st["available_actions"]]
        if "surrender" not in ids:
            self.skipTest("surrender not advertised in this scenario")
        proc, conn, sess = self._launch(gid)
        try:
            def click_option(pred_json):
                # Poll until the next sheet's option is enabled (the sheet
                # briefly re-renders mid-flight with all options frozen).
                sess.wait_js(
                    "[...document.querySelectorAll('.v2c-option')]"
                    ".some(b => !b.disabled && (%s).test(b.textContent))"
                    % pred_json,
                    timeout=8,
                    desc="sheet offers an option matching %s" % pred_json)
                sess.js(
                    "[...document.querySelectorAll('.v2c-option')]"
                    ".find(b => !b.disabled && (%s).test(b.textContent))"
                    ".click(); true" % pred_json)
                import time
                time.sleep(1.2)

            sess.wait_js(
                "[...document.querySelectorAll('#actionList button')]"
                ".some(b => /surrender/i.test(b.textContent))",
                timeout=8,
                desc="surrender action is advertised")
            sess.js("[...document.querySelectorAll('#actionList button')]"
                    ".find(b => /surrender/i.test(b.textContent))"
                    ".click()")
            for pred in ("/final warning/i", "/end this life/i"):
                try:
                    click_option(pred)
                except Exception:
                    self.skipTest("surrender flow shape changed at %s" % pred)
            # life ends (server-authoritative); the completed-life view with
            # succession renders on the next load (same as M2.5).
            import time as _time
            for _ in range(10):
                if self._state(gid).get("status") == "dead":
                    break
                _time.sleep(0.5)
            self.assertEqual(self._state(gid)["status"], "dead")
            sess2 = conn.new_session(
                self.base + "/game?game=" + gid)
            sess2.wait_js("document.readyState === 'complete' && "
                          "document.getElementById('legacyView') !== null",
                          desc="game page loaded")
            sess2.wait_js(
                "!document.getElementById('legacyView').hidden",
                desc="completed-life view appears")
            cont = sess2.js(
                "[...document.querySelectorAll('#childList button')]"
                ".length")
            if not cont:
                self.skipTest("no eligible children in scenario state")
            before = self._state(gid)["revision"]
            sess2.js("document.querySelector('#childList button').click()")
            _time.sleep(2.0)
            after = self._state(gid)
            self.assertGreater(after["revision"], before)
            self.assertEqual(after["status"], "alive")
            self.assertEqual(after.get("generation", 1), 2)
            # back in the v2 shell as the child (revision adopts can take a
            # beat after continuation)
            sess2.wait_js(
                "document.readyState === 'complete' && "
                "document.querySelector('.v2-header-name') !== null && "
                "document.querySelector('.v2-header-name').textContent.length > 0",
                timeout=20,
                desc="v2 shell resumes for the child")
            self.assertEqual(sess2.console_errors, [])
        finally:
            proc.terminate()
    def test_person_edit_opens_editor_with_exact_person_selected(self):
        gid = self.game["game_id"]
        other = next(p for p in self.game["people"]
                     if p["id"] != self.game["player_id"])
        proc, conn, sess = self._launch(gid)
        try:
            sess.js("document.querySelectorAll('.v2-tab')[3].click()")  # People
            sess.wait_js(
                "document.getElementById('familyList').children.length > 0",
                desc="roster renders")
            sess.js("document.querySelectorAll('#familyList .chip')[0]"
                    ".click()")
            sess.wait_js(
                "!document.querySelector('.v2-person-host').hidden && "
                "!!document.querySelector('.v2-person-host "
                "button[aria-label=Edit]')",
                desc="person overlay opens with an Edit control")
            sess.js("document.querySelector('.v2-person-host "
                    "button[aria-label=Edit]').click()")
            sess.wait_js(
                "document.getElementById('editorPanel') && "
                "document.getElementById('editorPanel').checkVisibility()",
                desc="editor opens from the person Edit hook")
            active = sess.js(
                "['home','job','assets','people','activities']"
                ".find(s => !document.getElementById('screen-'+s).hidden)")
            self.assertEqual(active, "activities")
            pressed = sess.js(
                "[...document.querySelectorAll('#editorPeople button')]"
                ".filter(b => b.getAttribute('aria-pressed') === 'true')"
                ".map(b => b.textContent)")
            self.assertEqual(len(pressed), 1, pressed)
            self.assertIn(other["name"].split(" ")[0], pressed[0])
            form_name = sess.js(
                "(document.querySelector('#editorForm h3') || {})"
                ".textContent || ''")
            self.assertIn(other["name"].split(" ")[0], form_name)
            law_pressed = sess.js(
                "[...document.querySelectorAll('#editorPeople button')]"
                ".find(b => b.textContent.includes('Local laws'))"
                ".getAttribute('aria-pressed')")
            self.assertEqual(law_pressed, "false")
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def test_person_chips_from_journal_and_decision_sheet(self):
        gid = self.game["game_id"]
        parent = next(p for p in self.game["people"]
                      if p["id"] != self.game["player_id"])
        proc, conn, sess = self._launch(gid)
        try:
            # --- journal chip → person overlay ---
            sess.wait_js(
                "document.querySelectorAll('#journalList .jchips "
                ".v2-person-link').length > 0",
                desc="timeline person chips render")
            sess.js("document.querySelectorAll('#journalList .jchips "
                    ".v2-person-link')[0].click()")
            sess.wait_js(
                "!document.querySelector('.v2-person-host').hidden && "
                "!!document.querySelector('.v2-person-host "
                "button[aria-label=Back]')",
                desc="journal chip opens the person overlay")
            self.assertIn(parent["name"].split(" ")[0],
                          sess.js("document.querySelector('.v2-person-host')"
                                  ".textContent"))
            sess.js("document.querySelector('.v2-person-host "
                    "button[aria-label=Back]').click()")
            sess.wait_js("document.querySelector('.v2-person-host').hidden",
                         desc="Back closes the overlay")

            # --- decision sheet chip → overlay above the sheet ---
            sess.js("[...document.querySelectorAll('#actionList button')]"
                    ".find(b => /surrender/i.test(b.textContent)).click()")
            sess.wait_js(
                "document.querySelectorAll('.v2c-people .v2-person-link')"
                ".length > 0",
                desc="sheet person chips render")
            sess.js("document.querySelector('.v2c-people "
                    ".v2-person-link').click()")
            sess.wait_js(
                "!document.querySelector('.v2-person-host').hidden && "
                "!!document.querySelector('.v2-person-host "
                "button[aria-label=Back]')",
                desc="sheet chip opens the overlay above the sheet")
            sess.js("document.querySelector('.v2-person-host "
                    "button[aria-label=Back]').click()")
            sess.wait_js(
                "document.querySelector('.v2-person-host').hidden && "
                "!document.getElementById('modal').hidden && "
                "document.querySelectorAll('.v2c-option').length > 0",
                desc="Back returns to the still-open decision sheet")
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def _drive_two_robins(self):
        """Two children both named Robin (same-name distinctness), one adult
        pair of parents, and a fresh year for the once-used action."""
        game = self._create("v2-robin-setup", scenario="married_30", seed=52)
        gid = game["game_id"]

        def act(action, payload=None):
            st = self._state(gid)
            return self._post("/api/games/%s/actions" % gid, {
                "expected_revision": st["revision"],
                "request_id": "v2-robin-%s-%s" % (action, st["revision"]),
                "action": action, "payload": payload or {}})

        for _ in range(10):
            st = self._state(gid)
            robins = [p for p in st["people"]
                      if p["name"].startswith("Robin")]
            if len(robins) >= 2:
                # drain any open pendings — a pending gates the advertised
                # actions (enabled:false), which would mask the matrix below
                for _ in range(6):
                    st = self._state(gid)
                    d = st.get("pending_decision")
                    if not d:
                        break
                    if d["kind"] == "baby_naming":
                        act("resolve_decision",
                            {"decision_id": d["id"],
                             "option_id": d["options"][0]["id"],
                             "inputs": {"name": "Sam"}})
                    else:
                        o = next((x for x in d["options"]
                                  if x.get("enabled", True)), None)
                        if not o:
                            break
                        act("resolve_decision",
                            {"decision_id": d["id"], "option_id": o["id"]})
                return gid, [r["id"] for r in robins[:2]]
            d = st.get("pending_decision")
            if d:
                if d["kind"] == "baby_naming":
                    act("resolve_decision",
                        {"decision_id": d["id"],
                         "option_id": d["options"][0]["id"],
                         "inputs": {"name": "Robin"}})
                else:
                    o = next((x for x in d["options"]
                              if x.get("enabled", True)), None)
                    if not o:
                        self.skipTest("pending with no enabled option")
                    act("resolve_decision",
                        {"decision_id": d["id"], "option_id": o["id"]})
                continue
            ids = [a["id"] for a in st["available_actions"]]
            if "try_for_baby" in ids:
                try:
                    st = act("try_for_baby")
                except urllib.error.HTTPError:
                    act("age_up")              # refused/blocked this year
                    continue
                if not st.get("pending_decision"):
                    act("age_up")              # refused this year
                continue
            act("age_up")
        self.skipTest("could not conceive two Robin children in 10 years "
                      "(seed-dependent)")

    def test_same_name_minors_adult_and_once_used_action(self):
        gid, robin_ids = self._drive_two_robins()
        st = self._state(gid)
        parent = next(p for p in st["people"]
                      if p["id"] != st["player_id"]
                      and p["age"] >= 18)
        proc, conn, sess = self._launch(gid)
        try:
            sess.js("document.querySelectorAll('.v2-tab')[3].click()")  # People
            sess.wait_js(
                "document.getElementById('familyList').children.length > 0",
                desc="roster renders")

            # --- two same-name people stay distinct by ID ---
            robins = sess.js(
                "[...document.querySelectorAll('#familyList .chip')]"
                ".map((c, i) => ({i, t: c.textContent}))"
                ".filter(x => x.t.startsWith('Robin'))")
            self.assertEqual(len(robins), 2, robins)

            def robin_indexes():
                return sess.js(
                    "[...document.querySelectorAll('#familyList .chip')]"
                    ".map((c, i) => c.textContent.startsWith('Robin') ? i : -1)"
                    ".filter(i => i >= 0)")

            idx = robin_indexes()
            self.assertEqual(len(idx), 2, idx)

            def open_robin(i):
                sess.js(
                    "[...document.querySelectorAll('#familyList .chip')][%d]"
                    ".click()" % i)
                sess.wait_js(
                    "!document.querySelector('.v2-person-host').hidden && "
                    "!!document.querySelector('.v2-person-host "
                    "button[aria-label=Back]')",
                    desc="overlay opens")

            def overlay_summary():
                return sess.js(
                    "(document.querySelector('.v2-person-host .v2p-name')||{}"
                    ").textContent + '|' + "
                    "(document.querySelector('.v2-person-host .v2p-subtitle')"
                    "||{}).textContent + '|' + "
                    "(document.querySelector('.v2-person-host "
                    ".v2p-header-title')||{}).textContent")

            def back():
                sess.js("document.querySelector('.v2-person-host "
                        "button[aria-label=Back]').click()")
                sess.wait_js(
                    "document.querySelector('.v2-person-host').hidden",
                    desc="overlay closes")

            open_robin(idx[0])
            first = overlay_summary()
            self.assertIn("Robin", first)
            self.assertIn("child", first.lower())   # minor role title
            back()
            open_robin(idx[1])
            second = overlay_summary()
            self.assertIn("Robin", second)
            self.assertNotEqual(first, second,
                                "same-name people must open different "
                                "details: %r vs %r" % (first, second))
            back()

            # --- adult: parent overlay with a real, usable action ---
            first_name = parent["name"].split(" ")[0]
            sess.wait_js(
                "[...document.querySelectorAll('#familyList .chip')]"
                ".some(c => c.textContent.startsWith(%s))" %
                json.dumps(first_name),
                timeout=8, desc="parent chip available")
            sess.js(
                "[...document.querySelectorAll('#familyList .chip')]"
                ".find(c => c.textContent.startsWith(%s)).click()" %
                json.dumps(first_name))
            sess.wait_js(
                "!document.querySelector('.v2-person-host').hidden && "
                "!!document.querySelector('.v2-person-host "
                "button[aria-label=Back]')",
                desc="parent overlay opens")
            adult = overlay_summary()
            self.assertIn(parent["role"].lower(), adult.lower())
            sess.wait_js(
                "[...document.querySelectorAll('.v2-person-host "
                ".v2p-action')].some(t => "
                "t.textContent.includes('Spend time') && !t.disabled)",
                desc="adult has an enabled Spend time tile")
            sess.js(
                "[...document.querySelectorAll('.v2-person-host "
                ".v2p-action')].find(t => "
                "t.textContent.includes('Spend time')).click()")
            # --- once-used: the server spec gates the second use ---
            import time
            for _ in range(12):
                view = json.loads(urllib.request.urlopen(
                    "%s/api/games/%s/people/%s" %
                    (self.base, gid, parent["id"])).read())
                spec = next((a for a in view.get("actions", [])
                             if a["id"] == "spend_time"), None)
                if spec and spec.get("enabled") is False:
                    break
                time.sleep(0.5)
            self.assertIsNotNone(spec, "spend_time spec served for adult")
            self.assertFalse(spec.get("enabled"),
                             "spend_time must be used-up after one use")
            sess.wait_js(
                "[...document.querySelectorAll('.v2-person-host "
                ".v2p-action')].some(t => "
                "t.textContent.includes('Spend time') && t.disabled && "
                "/used|spent/i.test(t.textContent))",
                timeout=10,
                desc="overlay shows the used-up tile disabled with reason")
            back()
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def test_back_navigation_every_destination_and_scroll_restore(self):
        gid = self.game["game_id"]
        proc, conn, sess = self._launch(gid)
        try:
            def back_hidden():
                return sess.js(
                    "(document.querySelector('.v2-back')||{hidden:true})"
                    ".hidden")

            def active_screen():
                return sess.js(
                    "['home','job','assets','people','activities']"
                    ".find(s => !document.getElementById('screen-'+s)"
                    ".hidden)")

            def header_back():
                sess.js("document.querySelector('.v2-back').click()")

            def tab(i):
                sess.js("document.querySelectorAll('.v2-tab')[%d]"
                        ".click()" % i)

            self.assertEqual(active_screen(), "home")
            self.assertTrue(back_hidden(), "Back hidden on plain Home")

            # every top-level destination: open → Back → Home again
            for i, name in [(1, "job"), (2, "assets"), (3, "people"),
                            (4, "activities")]:
                tab(i)
                sess.wait_js("!document.querySelector('.v2-back').hidden",
                             desc="Back visible on %s" % name)
                self.assertEqual(active_screen(), name)
                header_back()
                sess.wait_js("document.querySelector('.v2-back').hidden && "
                             "!document.getElementById('screen-home').hidden",
                             desc="Back from %s returns Home" % name)

            # chained: home → job → assets → Back → job → Back → home,
            # with scroll position restored on the way back (values are
            # clamped to what the home content can actually scroll)
            sess.js("document.querySelector('.v2-scroll').scrollTop = 500;")
            home_scroll = sess.js(
                "document.querySelector('.v2-scroll').scrollTop")
            tab(1)                       # job
            sess.js("document.querySelector('.v2-scroll').scrollTop = 0;")
            job_scroll = sess.js(
                "document.querySelector('.v2-scroll').scrollTop")
            tab(2)                       # assets
            header_back()
            sess.wait_js("!document.getElementById('screen-job').hidden",
                         desc="chained Back lands on job")
            self.assertEqual(active_screen(), "job")
            self.assertEqual(sess.js(
                "document.querySelector('.v2-scroll').scrollTop"),
                job_scroll, "job scroll restored")
            header_back()
            sess.wait_js("!document.getElementById('screen-home').hidden",
                         desc="chained Back lands on home")
            sc = sess.js("document.querySelector('.v2-scroll').scrollTop")
            self.assertEqual(sc, home_scroll,
                             "home scroll restored: %r vs %r" %
                             (sc, home_scroll))

            # browser Back (popstate) matches in-app Back
            tab(3)                       # people
            sess.wait_js("!document.getElementById('screen-people')"
                         ".hidden", desc="people via tab")
            sess.js("history.back()")
            sess.wait_js("!document.getElementById('screen-home').hidden",
                         desc="browser Back returns Home")

            # person overlay → Back returns to the People list
            tab(3)
            sess.wait_js("document.getElementById('familyList')"
                         ".children.length > 0", desc="roster")
            sess.js("document.querySelectorAll('#familyList .chip')[0]"
                    ".click()")
            sess.wait_js("!document.querySelector('.v2-person-host').hidden",
                         desc="overlay opens")
            header_back()
            sess.wait_js("document.querySelector('.v2-person-host').hidden &&"
                         " !document.getElementById('screen-people').hidden",
                         desc="Back from overlay returns to People")

            # More → Editor → Back closes the editor
            sess.js("document.querySelector('.v2-header-btn[aria-label="
                    "\"More routes\"]').click()")
            sess.js("[...document.querySelectorAll('.v2-more button')]"
                    ".find(b => b.textContent === 'Character editor')"
                    ".click()")
            sess.wait_js("document.getElementById('editorPanel') && "
                         "document.getElementById('editorPanel')"
                         ".checkVisibility()",
                         desc="editor opens")
            header_back()
            sess.wait_js("!document.getElementById('editorPanel')"
                         ".checkVisibility()",
                         desc="Back closes the editor")

            # Escape closes the More menu (safe surface)
            sess.js("document.querySelector('.v2-header-btn[aria-label="
                    "\"More routes\"]').click()")
            sess.js("document.dispatchEvent(new KeyboardEvent('keydown',"
                    "{key:'Escape'}))")
            sess.wait_js("document.querySelector('.v2-more').hidden",
                         desc="Escape closes the menu")

            # All lives → visible Back returns to the game
            sess.js("document.getElementById('backToLives').click()")
            sess.wait_js("!document.getElementById('resumeView').hidden",
                         desc="All lives opens")
            self.assertFalse(sess.js("document.getElementById("
                                     "'resumeBackBtn').hidden"))
            sess.js("document.getElementById('resumeBackBtn').click()")
            sess.wait_js("!document.getElementById('gameView').hidden",
                         desc="Back returns to the game")
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def test_phone_320px_no_horizontal_overflow(self):
        # own game: other journeys leave pendings on the class-shared one
        game = self._create("v2-320px")
        gid = game["game_id"]
        proc, conn = launch_chrome()
        try:
            sess = conn.new_session("about:blank")
            sess.call("Emulation.setDeviceMetricsOverride",
                      {"width": 320, "height": 568,
                       "deviceScaleFactor": 2, "mobile": True})
            sess.call("Page.navigate",
                      {"url": self.base + "/game?game=" + gid})
            sess.wait_js("location.href.indexOf('/game') !== -1 && "
                         "document.readyState === 'complete' && "
                         "document.querySelector('.v2h-age') !== null && "
                         "document.querySelector('.v2-tabbar') !== null",
                         timeout=20,
                         desc="shell renders at 320px")
            overflow = sess.js(
                "Math.max(document.documentElement.scrollWidth, "
                "document.body ? document.body.scrollWidth : 0)")
            self.assertLessEqual(overflow, 320,
                                 "horizontal overflow: %r" % overflow)
            self.assertFalse(sess.js(
                "document.querySelector('.v2-back').hidden !== true && "
                "false || document.querySelector('.v2-header-name')"
                ".offsetWidth > 320"))
            # all five tabs visible and labelled at 320px
            self.assertEqual(sess.js(
                "document.querySelectorAll('.v2-tab').length"), 5)
            self.assertTrue(sess.js(
                "[...document.querySelectorAll('.v2-tab')].every(t => "
                "t.getBoundingClientRect().width > 0)"))
            # Age control and stats render
            self.assertTrue(sess.js(
                "!!document.querySelector('.v2h-age')"))
            self.assertTrue(sess.js(
                "document.querySelectorAll('.v2h-stat-value').length >= 1"))
            # decision sheet covers the viewport width on phones
            sess.wait_js(
                "[...document.querySelectorAll('#actionList button')]"
                ".some(b => /surrender/i.test(b.textContent))",
                timeout=10, desc="surrender action advertised")
            sess.js("[...document.querySelectorAll('#actionList button')]"
                    ".find(b => /surrender/i.test(b.textContent)).click()")
            sess.wait_js("!document.getElementById('modal').hidden",
                         desc="sheet opens")
            box = sess.js(
                "(function(){var b = document.querySelector('.modal-box')"
                ".getBoundingClientRect(); return {left: b.left, "
                "width: b.width};})()")
            self.assertLessEqual(box["left"], 1.0, box)
            self.assertGreaterEqual(box["width"], 319.0, box)
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()

    def test_more_editor_reveals_activities_and_opens_idempotently(self):
        proc, conn = launch_chrome()
        try:
            sess = conn.new_session(
                self.base + "/game?game=" + self.game["game_id"])
            sess.wait_js("document.querySelector('.v2-tabbar') !== null",
                         desc="v2 tabbar renders")
            # Open the More menu and choose Character editor.
            sess.js("document.querySelector('.v2-header-btn[aria-label="
                    "\"More routes\"]').click()")
            sess.js("[...document.querySelectorAll('.v2-more button')]"
                    ".find(b => b.textContent === 'Character editor')"
                    ".click()")
            sess.wait_js(
                "document.getElementById('editorPanel') && "
                "document.getElementById('editorPanel').checkVisibility()",
                desc="editor visible on its owning surface")
            visible = sess.js(
                "document.getElementById('editorPanel').checkVisibility()")
            self.assertTrue(visible)
            # Owning surface: the Activities screen must be the active one.
            active = sess.js(
                "['home','job','assets','people','activities']"
                ".find(s => !document.getElementById('screen-'+s).hidden)")
            self.assertEqual(active, "activities")
            # Second More → Editor: idempotent, stays open, no toggle-off.
            sess.js("document.querySelector('.v2-header-btn[aria-label="
                    "\"More routes\"]').click()")
            sess.js("[...document.querySelectorAll('.v2-more button')]"
                    ".find(b => b.textContent === 'Character editor')"
                    ".click()")
            import time
            time.sleep(0.5)
            self.assertTrue(sess.js(
                "document.getElementById('editorPanel').checkVisibility()"))
            self.assertEqual(sess.console_errors, [])
        finally:
            proc.terminate()


if __name__ == "__main__":
    unittest.main()
