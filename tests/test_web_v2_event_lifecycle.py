"""v2 Event modal lifecycle regressions (review round, read-only on
production code): drives the REAL service + REAL page in headless Chromium
over CDP against the latest Codex modal/safe-retry integration.

Deterministic fixtures (no RNG-stream assumptions beyond the pinned seeds):

  * seed-7 profile (TOP-LEVEL seed, no appearance dict — birth-time
    cosmetic draws otherwise shift the stream): the first decision card
    opens at age 5 (A's M4 guaranteed childhood new_friend seam so the
    flavor pool can no longer starve it; the friend NPC is minted at
    resolve time, so the pending itself is player-only). After the F6
    player-filter the card must show NO main avatar; avatars render as
    <img> from /api/render.
  * forced university chain (tests/test_web_v2_home_lifecycle.py H1
    fixture): env player.age=17, smarts=grades=100,
    rng_state=random.Random(1).getstate(), turn cursor at education.
    UI Age -> university_application; accept -> outcome popup; Continue ->
    chained university_funding; age unchanged throughout.

Coverage:
  F5 no stat-effect spoilers on the card (real costs/reasons stay visible);
  outcome popup deltas == server last_decision_outcome verbatim;
  Escape cannot dismiss the outcome (Age gated, chrome inert);
  Continue closes and re-opens the chained next decision;
  Escape collapse -> decision pill -> Show/"Decide later" idempotent;
  Tab focus trapped, initial focus on first enabled option;
  safe retry after transport failure AND after a LOST-SUCCESS resolve
  (request commits server-side, response dropped): the retried op replays
  the same request_id and openResolvedOutcome surfaces the committed
  outcome popup exactly once.

Run: python3 -m unittest tests.test_web_v2_event_lifecycle
Needs chromium (tools.cdp_client.CHROMIUM); skipped otherwise.
Production files are exercised read-only; this file is the only addition.
"""

import json
import os
import random
import re
import shutil
import sys
import tempfile
import threading
import unittest
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

# Keep every temp artifact (chrome profile, save dirs) inside the repo's
# scratch volume — /tmp is quota-limited on this machine.
os.environ["TMPDIR"] = os.path.expanduser("~/.lifesim-tmp")
os.makedirs(os.environ["TMPDIR"], exist_ok=True)

import web.server as server  # noqa: E402

try:
    from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
except Exception:  # pragma: no cover - environment without chromium
    CHROMIUM = None

SPOILER_RE = re.compile(
    r"(happiness|health|smarts|looks|relationship).{0,4}"
    r"\(\s*-?\d+\s*-\s*-?\d+\s*\)", re.I)

# TOP-LEVEL seed, no appearance: this is the pinned harness profile.
PROFILE = {"first_name": "Feel", "last_name": "Review", "gender": "female",
           "country": "Australia", "seed": 7}


@unittest.skipIf(CHROMIUM is None, "chromium not available")
class EventLifecycleTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-v2e-lifecycle-")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()
        cls.profile_dir = tempfile.mkdtemp(prefix="v2e-chrome-profile-",
                                           dir=os.environ["TMPDIR"])
        cls.proc, cls.conn = launch_chrome(cls.profile_dir)

    @classmethod
    def tearDownClass(cls):
        cls.proc.terminate()
        cls.proc.wait(timeout=5)
        if cls.proc.stderr:
            cls.proc.stderr.close()
        shutil.rmtree(cls.profile_dir, ignore_errors=True)
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)

    # ------------------------------------------------------------- helpers
    def setUp(self):
        self.session = self.conn.new_session("about:blank")
        self.session.call("Runtime.enable")
        self.session.call("Page.enable")
        self.session.call("Log.enable")
        self.session.call("Emulation.setDeviceMetricsOverride",
                          {"width": 390, "height": 844,
                           "deviceScaleFactor": 1, "mobile": True})
        # Unique request_id per test: new_game is idempotent on it, so a
        # shared label would silently reuse one aged/resolved save.
        st = self.httpd.service.new_game(dict(PROFILE), self.id())
        self.game_id = st["game_id"]
        self.base = "http://127.0.0.1:%d" % self.port
        self.session.call(
            "Page.navigate",
            {"url": self.base + "/game?game=" + self.game_id})
        self.session.wait_js("!!document.querySelector('.v2-agebtn')",
                             timeout=15, desc="shell mounted")

    def tearDown(self):
        errs = [e for e in self.session.console_errors
                if "favicon" not in e.get("text", "")]
        self.assertEqual(errs, [], "console errors: %r" % (errs,))

    def api_state(self):
        with urllib.request.urlopen(
                "%s/api/games/%s" % (self.base, self.game_id)) as r:
            return json.loads(r.read())

    def svc_env(self):
        return self.httpd.service._load(self.game_id)  # noqa: SLF001

    def svc_save(self, env):
        self.httpd.service._save(env)  # noqa: SLF001

    def js(self, expr):
        return self.session.js(expr)

    def wait_js(self, expr, timeout=10, desc=None):
        return self.session.wait_js(expr, timeout=timeout, desc=desc)

    def modal_open(self):
        return self.js("!document.getElementById('modal').hidden && " +
                       "!!document.querySelector('.v2e-card')")

    def choice_card_open(self):
        return self.js("!document.getElementById('modal').hidden && " +
                       "!!document.querySelector('.v2e-options')")

    def outcome_open(self):
        return self.js("!document.getElementById('modal').hidden && " +
                       "!!document.querySelector('.v2e-outcome')")

    def player_age(self):
        st = self.api_state()
        return next(p["age"] for p in st["people"]
                    if p["id"] == st["player_id"])

    def click_first_enabled_option(self):
        return self.js(
            "Array.from(document.querySelectorAll('.v2e-option'))" +
            ".find(b => !b.disabled).click(); 'clicked'")

    def continue_outcome(self):
        self.js("document.querySelector('.v2e-continue').click()")
        self.session.wait_idle()

    def reach_seed7_event(self, max_clicks=8):
        """Age until the first decision card opens. Check the modal FIRST
        after each age/wait — never settle/drain before checking (that was
        a harness bug: it consumed the very event under test)."""
        for _ in range(max_clicks):
            if self.outcome_open():            # defensive: never expected
                self.continue_outcome()
                continue
            if self.choice_card_open():
                return self.player_age()
            self.js("document.querySelector('.v2-agebtn').click()")
            self.session.wait_idle()
        self.fail("seed-7 decision card did not open within %d ages"
                  % max_clicks)

    def force_university_offer(self):
        """H1 fixture: resume a real yearly cursor just before the age-17
        education offer; the real rule samples/stores the pending."""
        env = self.svc_env()
        env["player"].age = 17
        env["player"].smarts = env["player"].grades = 100
        env["rng_state"] = random.Random(1).getstate()
        env["turn"] = {"turn_id": "t_h1_education",
                       "cursor": {"phase_id": "education", "item_cursor": 0},
                       "done": {}}
        self.svc_save(env)
        self.js("location.reload(); 'reloading'")
        self.session.wait_js("!!document.querySelector('.v2-agebtn')",
                             timeout=15, desc="shell remounted")

    def open_forced_university(self):
        self.force_university_offer()
        self.js("document.querySelector('.v2-agebtn').click()")
        self.session.wait_idle()
        self.wait_js("!document.getElementById('modal').hidden && " +
                     "!!document.querySelector('.v2e-card')",
                     desc="university decision card")
        st = self.api_state()
        self.assertEqual(st["pending_decision"]["kind"],
                         "university_application")
        self.assertEqual(self.player_age(), 17, "pending blocks the year")
        return st["pending_decision"]

    # --------------------------------------------------------------- tests
    def test_seed7_event_card_f5_f6_solo(self):
        age = self.reach_seed7_event()
        # M4 (A's guaranteed friend seam, coordinator.py): the childhood
        # new_friend offer is served at age 5 so the flavor pool can no
        # longer starve it, and the pending carries the minted friend
        # NPC (person_ids = [npc]) — zero rng draws, seeded parity kept.
        self.assertEqual(age, 5, "seed-7 first decision must open at 5")
        pending = self.api_state()["pending_decision"]
        self.assertIsNotNone(pending)
        self.assertEqual(pending.get("title"), "A new friend")

        # Centered event card semantics.
        self.assertEqual(
            self.js("document.querySelector('.v2e-card')" +
                    ".getAttribute('role')"), "dialog")
        self.assertTrue(self.js(
            "getComputedStyle(document.getElementById('modal')).display" +
            " === 'flex'"), "modal overlay must flex (centered card)")
        card = self.js("document.querySelector('.v2e-card').textContent")
        self.assertTrue(card.strip(), "card has content")

        # F6: the pending names only the PLAYER (the friend is minted at
        # resolve time), so after the player-filter the card must show NO
        # main avatar (avatars render as <img> from /api/render).
        st = self.api_state()
        others = [pid for pid in (pending.get("person_ids") or [])
                  if pid != st["player_id"]]
        self.assertEqual(others, [],
                         "harness premise: first event is player-only")
        self.assertFalse(self.js("!!document.querySelector('.v2e-avatar')"),
                         "no avatar may render for a player-only event")
        self.assertFalse(self.js("!!document.querySelector('.v2e-people')"),
                         "player-only events must not show the player's own chip")

        # F5: no stat-effect spoilers anywhere on the card.
        self.assertIsNone(SPOILER_RE.search(card),
                          "stat-effect spoiler on card: %r" % (card,))
        self.assertTrue(self.js(
            "Array.from(document.querySelectorAll('.v2e-option'))" +
            ".some(b => !b.disabled)"), "at least one enabled option")

    def test_university_chain_outcome_gating(self):
        app = self.open_forced_university()
        # accept: the application resolve carries no deltas/text (the
        # funding offer supersedes it server-side), so the card swaps
        # straight to the chained funding decision — no popup in between.
        self.click_first_enabled_option()
        self.wait_js("!document.getElementById('modal').hidden && " +
                     "!!document.querySelector('.v2e-card')",
                     desc="chained funding card")
        chained = self.api_state()["pending_decision"]
        self.assertIsNotNone(chained, "funding offer must follow apply")
        self.assertEqual(chained["kind"], "university_funding")
        self.assertNotEqual(chained["id"], app["id"])
        # The card swap is async (module reload + mount): wait until the
        # sheet actually serves the new pending id.
        self.wait_js(
            "(document.querySelector('.modal-box')||{})._v2SheetDecisionId" +
            " === " + json.dumps(chained["id"]),
            desc="chained card serves the new pending id")
        self.assertFalse(self.outcome_open(),
                         "no outcome popup between chain links")
        self.assertEqual(self.player_age(), 17)

        # Resolve the funding choice: THIS resolve carries real effects.
        resolved_id = chained["id"]
        money_before = self.api_state()["money"]
        self.click_first_enabled_option()
        self.wait_js("!document.getElementById('modal').hidden && " +
                     "!!document.querySelector('.v2e-outcome')",
                     desc="outcome popup")
        self.session.wait_idle()

        after = self.api_state()
        outcome = after.get("last_decision_outcome")
        self.assertIsNotNone(outcome, "server must publish the outcome")
        self.assertEqual(outcome["decision_id"], resolved_id)

        # Outcome card: heading, outcome text, deltas == server's changes.
        self.assertEqual(self.js(
            "document.querySelector('.v2e-outcome .v2e-title')" +
            ".textContent"), "What happened")
        card_text = self.js(
            "document.querySelector('.v2e-outcome').textContent")
        self.assertIn(outcome["text"].strip(), card_text)
        changes = {k: v for k, v in (outcome.get("changes") or {}).items()
                   if v}
        rows = self.js(
            "Array.from(document.querySelectorAll('.v2e-delta'))" +
            ".map(r => ({label: r.querySelector('.v2e-delta-label')" +
            ".textContent, value: r.querySelector('.v2e-delta-value')" +
            ".textContent, cls: r.querySelector('.v2e-delta-value')" +
            ".className}))")
        rendered = {r["label"]: r for r in rows}
        for key, delta in changes.items():
            label = {"money": "Money", "happiness": "Happiness",
                     "health": "Health", "smarts": "Smarts",
                     "looks": "Looks"}.get(key, "Relationship")
            self.assertIn(label, rendered, "delta row for %s" % key)
            row = rendered[label]
            if key == "money":
                expect = ("+$" if delta > 0 else "-$") + \
                    format(abs(delta), ",")
            else:
                expect = ("+" if delta > 0 else "") + str(delta)
            self.assertEqual(row["value"], expect,
                             "%s delta rendered verbatim" % key)
            self.assertIn("v2e-delta-up" if delta > 0 else "v2e-delta-down",
                          row["cls"])
        self.assertEqual(len(rows), len(changes), "no fabricated rows")
        self.assertEqual(self.api_state()["money"] - money_before,
                         changes.get("money", 0))

        # Escape must NOT dismiss the outcome popup.
        self.session.key("Escape", "Escape", 27)
        self.session.wait_idle()
        self.assertTrue(self.outcome_open(),
                        "Escape must not close the outcome popup")

        # Age gating: chrome inert and aging cannot fire while open.
        self.assertTrue(self.js(
            "!!document.querySelector('.v2-dock, .v2-tabbar') && " +
            "(document.querySelector('.v2-dock') || " +
            " document.querySelector('.v2-tabbar')).inert"))
        self.js("document.querySelector('.v2-agebtn').click()")
        self.session.wait_idle()
        self.assertEqual(self.player_age(), 17,
                         "aging is gated while the outcome is open")

        # Continue closes the popup; the funding resolve finishes the
        # year, so no decision reopens and the player has not aged.
        recorded = dict(outcome)
        self.continue_outcome()
        self.assertFalse(self.outcome_open())
        self.assertTrue(self.js("document.getElementById('modal').hidden"),
                        "no further pending after funding resolve")
        self.assertIsNone(self.api_state()["pending_decision"])
        self.assertEqual(self.player_age(), 17)
        # The recorded outcome survives the close.
        self.assertEqual(self.api_state()["last_decision_outcome"]
                         ["decision_id"], recorded["decision_id"])

    def test_escape_collapse_and_reopen(self):
        self.reach_seed7_event()
        title = self.js("document.querySelector('.v2e-title').textContent")
        # Escape collapses to the pill — never discards.
        self.session.key("Escape", "Escape", 27)
        self.session.wait_idle()
        self.assertTrue(self.js("document.getElementById('modal').hidden"))
        self.assertTrue(self.js(
            "!document.getElementById('decisionPill').hidden"),
            "decision pill is the return path")
        self.assertIsNotNone(self.api_state()["pending_decision"],
                             "collapse must not resolve the decision")
        # Show reopens the same decision.
        self.js("Array.from(document.querySelectorAll('#decisionPill button'))"
                ".find(b => b.textContent === 'Show').click()")
        self.wait_js("!document.getElementById('modal').hidden && " +
                     "!!document.querySelector('.v2e-card')", desc="reopen")
        self.assertEqual(self.js(
            "document.querySelector('.v2e-title').textContent"), title)
        # "Decide later" collapses again; the decision survives.
        self.js("document.querySelector('.v2e-later').click()")
        self.session.wait_idle()
        self.assertTrue(self.js("document.getElementById('modal').hidden"))
        self.assertTrue(self.js(
            "!document.getElementById('decisionPill').hidden"))
        self.assertIsNotNone(self.api_state()["pending_decision"])

    def test_focus_trap_and_initial_focus(self):
        self.reach_seed7_event()
        active_cls = self.js(
            "document.activeElement && " +
            "document.activeElement.className || ''")
        self.assertIn("v2e-option", active_cls,
                      "initial focus on the first enabled option")
        seen = set()
        for _ in range(8):
            self.session.key("Tab", "Tab", 9)
            inside = self.js("document.getElementById('modal')" +
                             ".contains(document.activeElement)")
            self.assertTrue(inside, "Tab escaped the modal")
            seen.add(self.js(
                "document.activeElement.className || " +
                "document.activeElement.tagName"))
        self.assertGreaterEqual(len(seen), 2, "Tab cycles between controls")

    def test_safe_retry_after_transport_failure(self):
        self.reach_seed7_event()
        money_before = self.api_state()["money"]
        # Sabotage transport: the resolve POST never reaches the server.
        self.js("window.__origFetch = window.fetch.bind(window);" +
                "window.fetch = function(){return Promise.reject(" +
                "new Error('sabotaged transport'))}; 'sabotaged'")
        self.click_first_enabled_option()
        self.wait_js("!document.getElementById('pendingPill').hidden",
                     desc="safe-retry pill after transport failure")
        self.session.wait_idle()
        self.assertFalse(self.outcome_open(),
                         "uncertain failure: pill owns the outcome")
        self.assertIsNone(self.api_state().get("last_decision_outcome"),
                         "resolve never committed server-side")
        # Restore transport; the retry applies the op and the outcome
        # popup appears (via the normal resolve flow).
        self.js("window.fetch = window.__origFetch; 'restored'")
        self.js("Array.from(document.querySelectorAll(" +
                "'#pendingPill button')).find(b => /retry/i.test(" +
                "b.textContent)).click()")
        self.wait_js("!document.getElementById('modal').hidden && " +
                     "!!document.querySelector('.v2e-outcome')",
                     desc="outcome after safe retry")
        self.session.wait_idle()
        outcome = self.api_state()["last_decision_outcome"]
        self.assertIsNotNone(outcome)
        card_text = self.js(
            "document.querySelector('.v2e-outcome').textContent")
        self.assertIn(outcome["text"].strip(), card_text)
        delta = (outcome.get("changes") or {}).get("money", 0)
        self.assertEqual(self.api_state()["money"] - money_before, delta)
        self.continue_outcome()
        self.assertFalse(self.outcome_open())

    def test_lost_success_retry_surfaces_outcome_once(self):
        """LOST-SUCCESS: the resolve POST REACHES the server and commits,
        but the response is dropped. The safe retry replays the same
        request_id, the replay ledger returns the ORIGINAL committed
        response, and openResolvedOutcome must surface the outcome popup —
        sendOperation merely rendering state would silently lose it."""
        self.reach_seed7_event()
        money_before = self.api_state()["money"]
        # Fire the real request (server commits) but reject to the caller;
        # drain the body so the single-threaded test server never blocks
        # writing a response nobody reads.
        self.js("window.__origFetch = window.fetch.bind(window);" +
                "window.fetch = function(){ " +
                " window.__origFetch.apply(null, arguments)" +
                "   .then(function(r){return r.text();})" +
                "   .catch(function(){}); " +
                " return Promise.reject(new Error('lost response')); }; " +
                "'sabotaged'")
        self.click_first_enabled_option()
        self.wait_js("!document.getElementById('pendingPill').hidden",
                     desc="safe-retry pill after lost response")
        self.session.wait_idle()
        self.js("window.fetch = window.__origFetch; 'restored'")
        self.js("Array.from(document.querySelectorAll(" +
                "'#pendingPill button')).find(b => /retry/i.test(" +
                "b.textContent)).click()")
        # The committed outcome MUST reach the player via the popup.
        self.wait_js("!document.getElementById('modal').hidden && " +
                     "!!document.querySelector('.v2e-outcome')",
                     timeout=15, desc="outcome popup after lost-success retry")
        self.session.wait_idle()
        outcome = self.api_state()["last_decision_outcome"]
        self.assertIsNotNone(outcome)
        card_text = self.js(
            "document.querySelector('.v2e-outcome').textContent")
        self.assertIn(outcome["text"].strip(), card_text)
        # Exactly-once: money moved by precisely the committed delta.
        delta = (outcome.get("changes") or {}).get("money", 0)
        self.assertEqual(self.api_state()["money"] - money_before, delta)
        self.continue_outcome()
        self.assertFalse(self.outcome_open())


if __name__ == "__main__":
    unittest.main()
