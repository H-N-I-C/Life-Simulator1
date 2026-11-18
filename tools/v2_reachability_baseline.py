"""v2 redesign Stage 1 (D): reachability baseline of every current M2.5 UI
surface, recorded BEFORE the v2 shell replaces presentation.

Drives the REAL web UI in headless Chromium against a throwaway server with
real scenario-started lives. For every surface Codex's plan (docs/collab/
redesign-v2/codex-review.md §2) says the v2 mockups must not lose, this
records: how it is reached today, a PASS/FAIL reachability check, and a
screenshot. Evidence lands in docs/collab/evidence/redesign-v2/ and is the
Stage 4 parity checklist input.

One command:  python3 tools/v2_reachability_baseline.py
Exit non-zero on any FAIL (mirrors the browser_check gate).
"""

import base64
import json
import os
import sys
import tempfile
import threading

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import web.server as server  # noqa: E402
from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
from tools.browser_check import Runner, write_report  # noqa: E402

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "redesign-v2")
FLOW = "v2-baseline"


def shot(runner, name):
    data = runner.s.call("Page.captureScreenshot", {"format": "png"})
    with open(os.path.join(EVIDENCE, name + ".png"), "wb") as f:
        f.write(base64.b64decode(data["data"]))
    runner.record(FLOW, "screenshot " + name, True)


def check_js(runner, expr, desc):
    value = runner.s.js(expr)
    runner.record(FLOW, desc, value is True,
                  "" if value is True else "not boolean true: %r" % (value,))


def click_tab(runner, name):
    s = runner.s
    s.js("document.querySelector('#tabbar [data-screen=%s]').click(); true"
         % json.dumps(name))
    s.wait_js("(() => { const el = document.getElementById('screen-%s');"
              " return !!el && !el.hidden; })()" % name,
              desc="screen-%s visible" % name)
    if name == "summary":
        check_js(runner,
                 "document.querySelectorAll('#journalList').length === 1",
                 "tab summary: timeline journal present")
    else:
        check_js(runner,
                 "document.querySelectorAll('#screen-%s .actionlist')"
                 ".length >= 1" % name,
                 "tab %s: action list container present" % name)
    shot(runner, "baseline-tab-" + name)


def main():
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-v2-baseline-",
                                dir=os.path.join(ROOT, "game_saves"))
    httpd = server.make_server(0, save_dir=save_dir)
    httpd.dev_mode = True
    port = httpd.server_port
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    chrome = None
    try:
        chrome, conn = launch_chrome()
        s = conn.new_session()
        runner = Runner("http://127.0.0.1:%d" % port, s, EVIDENCE,
                        service=httpd.service)

        # ---- A. standalone pages at phone width + a 320px floor pass ----
        for path in ("/creator", "/gallery", "/game", "/relationships"):
            runner.check_page(path, 390, 844)
        runner.check_page("/game", 320, 568)

        # ---- B. live life: the six shell screens -------------------------
        runner._hook_phone_viewport(FLOW)
        runner._hook_scenario_start(FLOW, scenario="married_with_child")
        runner._hook_refresh_state(FLOW)
        runner._hook_load_game_in_page(FLOW)
        check_js(runner,
                 "!!document.getElementById('gameView') && "
                 "!document.getElementById('gameView').hidden && "
                 "!!document.getElementById('tabbar') && "
                 "!document.getElementById('tabbar').hidden",
                 "game view + tabbar mounted")
        check_js(runner,
                 "document.querySelectorAll('#playerCard').length === 1 && "
                 "document.querySelectorAll('#journalList').length === 1",
                 "player card + timeline present")
        for tab in ("summary", "education", "work", "relationships",
                    "activities", "finances"):
            click_tab(runner, tab)
        check_js(runner,
                 "document.querySelectorAll('#familyList').length === 1 && "
                 "document.querySelectorAll('#actions-family').length === 1",
                 "family roster + parenting action lists render")

        # ---- C. M2.5 feature cards: editor / family tree / dating -------
        s.js("document.getElementById('editorToggle').click(); true")
        s.wait_js("(() => { const p = document.getElementById('editorPanel');"
                  " return !!p && !p.hidden; })()",
                  timeout=15, desc="character editor panel opens")
        s.wait_js("document.querySelectorAll('#editorPeople button').length "
                  ">= 1", timeout=15,
                  desc="editor people list renders")
        s.js("document.querySelector('#editorPeople button').click(); true")
        s.wait_js("!!document.getElementById('editorForm') && "
                  "document.getElementById('editorForm').children.length > 0",
                  timeout=15, desc="character editor form renders")
        s.js("document.getElementById('editorPanel').scrollIntoView(); true")
        shot(runner, "baseline-editor")
        s.js("[...document.querySelectorAll('#editorPeople button')]"
             ".find(b => b.textContent.includes('Local laws')).click(); true")
        s.wait_js("!!document.getElementById('edLaw')", timeout=15,
                  desc="editor jurisdiction laws form renders")
        check_js(runner, "!!document.getElementById('edLaw')",
                 "editor jurisdiction laws section present")

        s.js("document.getElementById('familyTreeToggle').click(); true")
        s.wait_js("(() => { const c = document.getElementById('familyTreeCard');"
                  " return !!c && !c.hidden && c.innerHTML.length > 20; })()",
                  timeout=15, desc="family tree card mounts")
        s.js("document.getElementById('familyTreeCard').scrollIntoView(); true")
        shot(runner, "baseline-family-tree")

        s.js("document.getElementById('m25Toggle').click(); true")
        s.wait_js("!!document.getElementById('m25Body')",
                  timeout=15, desc="Dating & weddings card mounts")
        check_js(runner,
                 "document.querySelectorAll('#m25Card [data-tab]').length "
                 "=== 3",
                 "m25 exposes Dating / Weddings / What I know tabs")
        s.wait_js("document.getElementById('m25Body').children.length > 0",
                  timeout=15, desc="Dating tab body renders")
        s.js("document.getElementById('m25Card').scrollIntoView(); true")
        shot(runner, "baseline-m25-dating")

        s.js("document.querySelector('#m25Card [data-tab=\"weddings\"]')"
             ".click(); true")
        s.wait_js("document.getElementById('m25Body').innerText.indexOf("
                  "'Plan the wedding') >= 0", timeout=15,
                  desc="wedding planner renders")
        check_js(runner,
                 "document.getElementById('m25Body').innerText.indexOf("
                 "'Part ways') >= 0",
                 "divorce/dissolution controls present on union")
        check_js(runner,
                 "document.getElementById('m25Body').querySelectorAll("
                 "'[data-picker]').length >= 3",
                 "venue/outfit/honeymoon pickers present")
        s.js("document.getElementById('m25Card').scrollIntoView(); true")
        shot(runner, "baseline-m25-wedding-planner")

        s.js("document.querySelector('#m25Card [data-tab=\"reputation\"]')"
             ".click(); true")
        s.wait_js("document.getElementById('m25Body').innerText.indexOf("
                  "'know') >= 0", timeout=15,
                  desc="What I know tab renders")
        s.js("document.getElementById('m25Card').scrollIntoView(); true")
        shot(runner, "baseline-m25-what-i-know")

        # ---- D. decision modal + reopen pill -----------------------------
        runner._hook_add_adult_stranger(FLOW)
        runner._hook_cmd(FLOW, action="dating_app")
        runner._hook_refresh_state(FLOW)
        pending = (runner.ctx.get("state") or {}).get("pending_decision")
        if pending:
            s.js("sessionStorage.setItem('game_id', %s); true"
                 % json.dumps(runner.ctx["state"]["game_id"]))
            s.call("Page.navigate", {"url": runner.base + "/game"})
            s.wait_js("!!document.getElementById('gameView') && "
                      "!document.getElementById('gameView').hidden",
                      timeout=20, desc="game view loads with pending")
            s.wait_js("(() => { const m = document.getElementById('modal');"
                      " return !!m && !m.hidden && "
                      "m.contains(document.activeElement); })()",
                      timeout=20, desc="decision modal opens with focus")
            shot(runner, "baseline-decision-modal")
            s.js("document.dispatchEvent(new KeyboardEvent('keydown',"
                 "{key: 'Escape', bubbles: true})); true")
            s.wait_js("(() => { const p = document.getElementById("
                      "'decisionPill'); const m = document.getElementById("
                      "'modal'); return !!p && !p.hidden && !!m && m.hidden;"
                      " })()", desc="Escape collapses to reopening pill")
            shot(runner, "baseline-decision-pill")
            s.js("document.querySelector('#decisionPill button')"
                 ".click(); true")
            s.wait_js("(() => { const m = document.getElementById('modal');"
                      " return !!m && !m.hidden; })()",
                      desc="pill reopens decision")
            s.js("document.querySelector('#modalOptions button:not("
                 "[disabled])').click(); true")
            s.wait_js("(() => { const m = document.getElementById('modal');"
                      " return !!m && m.hidden; })()",
                      desc="decision resolves and modal closes")
        else:
            runner.record(FLOW, "decision fixture (dating_app pending)",
                          False, "no pending stored after dating_app")

        # ---- E. all-lives view -------------------------------------------
        s.js("document.getElementById('backToLives').click(); true")
        s.wait_js("(() => { const v = document.getElementById('resumeView');"
                  " return !!v && !v.hidden; })()",
                  desc="all-lives view visible")
        check_js(runner,
                 "document.querySelectorAll('#lifeList').length === 1",
                 "life list renders")
        shot(runner, "baseline-all-lives")

        # ---- F. death, completed-life archive + continuation -------------
        runner._hook_refresh_state(FLOW)  # page resolve advanced revision
        runner._hook_surrender_to_death(FLOW)
        s.js("sessionStorage.setItem('game_id', %s); true"
             % json.dumps(runner.ctx["state"]["game_id"]))
        s.call("Page.navigate", {"url": runner.base + "/game"})
        s.wait_js("(() => { const v = document.getElementById('legacyView');"
                  " return !!v && !v.hidden; })()",
                  timeout=20, desc="legacy view shows after death")
        check_js(runner, "!!document.getElementById('continueCard')",
                 "succession continue card present")
        s.js("document.getElementById('legacyView').scrollIntoView(); true")
        shot(runner, "baseline-legacy-continuation")

        # ---- G. 320px floor on the live game view ------------------------
        s.call("Emulation.setDeviceMetricsOverride",
               {"width": 320, "height": 568, "deviceScaleFactor": 1,
                "mobile": True})
        s.js("sessionStorage.setItem('game_id', %s); location.reload(); true"
             % json.dumps(runner.ctx["state"]["game_id"]))
        s.wait_js("(() => { const g = document.getElementById('gameView');"
                  " const l = document.getElementById('legacyView');"
                  " return (g && !g.hidden) || (l && !l.hidden); })()",
                  timeout=20, desc="game view renders at 320px")
        shot(runner, "baseline-game-320")

        fails, required_pending = write_report(EVIDENCE, runner.results, {})
        counts = {}
        for r in runner.results:
            counts[r[4]] = counts.get(r[4], 0) + 1
        print("v2 baseline: PASS=%d FAIL=%d PENDING=%d WARN=%d"
              % (counts.get("PASS", 0), counts.get("FAIL", 0),
                 counts.get("PENDING", 0), counts.get("WARN", 0)))
        return 1 if (fails or required_pending) else 0
    finally:
        try:
            if chrome is not None:
                chrome.terminate()
        except Exception:
            pass
        httpd.shutdown()


if __name__ == "__main__":
    sys.exit(main())
