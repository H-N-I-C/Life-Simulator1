"""Redesign v2 "feel pass" screenshot harness (Kimi D, QA).

Records every player-facing screen at BOTH phone widths (390x844 and the
320x568 floor) into docs/collab/evidence/redesign-v2/feel/, prefixed
before- (pre-redesign, commit 595fd9ab) or after- (current tree), so the
redesign can be compared screen by screen:

    # "before" set: run inside a worktree of the pre-redesign commit,
    # writing back into the main tree's evidence dir:
    git worktree add .review-tmp/feel-before 595fd9ab
    cp tools/feel_screens.py tools/cdp_client.py \
       .review-tmp/feel-before/tools/
    (cd .review-tmp/feel-before && \
       python3 tools/feel_screens.py --label before \
         --evidence /path/to/main/docs/collab/evidence/redesign-v2/feel)

    # "after" set: from the main tree
    python3 tools/feel_screens.py --label after

Generation-adaptive: drives the legacy #tabbar screens when present and
the v2 shell (.v2-tabbar / More menu) when present, capturing whichever
exists. Screens that cannot be reached are logged as SKIPPED (the v2 shell
is under active construction) — the harness never gates.

Exit code is non-zero only on harness errors (no chromium, server down).
"""

import argparse
import base64
import json
import os
import sys
import tempfile
import shutil
import threading
import time

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

# Harness scratch (chrome profile/socket dirs) must live under /home —
# chromium needs a PRIVATE dir for its ProcessSingleton socket, so not /home
# itself (that aborts launch) but a dedicated per-user tmp dir.
TMP_HOME = os.path.expanduser("~/.lifesim-tmp")  # RULE: never /tmp
os.makedirs(TMP_HOME, exist_ok=True)
os.environ["TMPDIR"] = TMP_HOME

import web.server as server  # noqa: E402
from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
from tools.browser_check import Runner  # noqa: E402

VIEWPORTS = [(390, 844), (320, 568)]
DEFAULT_EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence",
                                "redesign-v2", "feel")

skipped = []

# Feature modules that follow the §Shell mount API
# (mountX(root, hooks, data) -> {update, unmount}); B wires them into the
# shell — until a module is wired, the harness mounts it in a detached
# container with the real projection slice and reports wired=false.
# Values: (path, export name, JS expression for the module's data slice,
# JS expression that evaluates truthy once the shell wires it).
SHELL_API_MODULES = {
    "careers": ("/static/features/careers/careers.js", "mountCareers",
                "(window.__m3state || {})[%s]" % json.dumps("careers"),
                "(window.__m3state || {})[%s] != null"
                % json.dumps("careers")),
    "skills": ("/static/features/skills/skills.js", "mountSkills",
               "(window.__m3state || {})[%s]" % json.dumps("skills"),
               "(window.__m3state || {})[%s] != null"
               % json.dumps("skills")),
    # M4 (docs/collab/M4/02-contracts.md §UI): social mounts on
    # state.social; clubs mounts on the state.social.clubs slice.
    "social": ("/static/features/social/social.js", "mountSocial",
               "((window.__m3state || {}).social) || null",
               "(window.__m3state || {}).social != null"),
    "clubs": ("/static/features/social/clubs.js", "mountClubs",
              "((window.__m3state || {}).social || {}).clubs || null",
              "((window.__m3state || {}).social || {}).clubs != null"),
}
# Modules mounted by the shell through their own flows (route-covered);
# here we only prove they still parse/import cleanly.
IMPORT_ONLY_MODULES = {
    "family_tree": "/static/features/family_graph/family_tree.js",
    "editor": "/static/features/relationship_policy/editor.js",
    "m25_screens": "/static/features/relationship_policy/m25_screens.js",
}


def check_feature_modules(runner, s, label, evidence):
    """Every features/* module imports, and §Shell-API modules mount +
    unmount, without console errors — at 320. Returns list of findings
    (module, ok, wired, detail)."""
    findings = []
    s.console_errors.clear()
    s.exceptions.clear()
    s.failed_requests.clear()
    s.call("Emulation.setDeviceMetricsOverride",
           {"width": 320, "height": 844, "deviceScaleFactor": 1,
            "mobile": True})
    for name, path in IMPORT_ONLY_MODULES.items():
        ok = s.js("import(%s).then(() => true).catch((e) => String(e))"
                  % json.dumps(path))
        findings.append((name, ok is True, None,
                         "" if ok is True else str(ok)))
    state_js = "window.__m3state || null"
    for name, spec in SHELL_API_MODULES.items():
        path, export, data_expr, wired_expr = spec
        result = s.js(
            "import(%s).then((m) => {\n"
            "  const fn = m[%s];\n"
            "  if (typeof fn !== 'function') return 'no export %s';\n"
            "  const host = document.createElement('div');\n"
            "  host.style.cssText = 'position:absolute;left:-9999px;';\n"
            "  document.body.appendChild(host);\n"
            "  const hooks = { getState: () => ({}), sendAction: () =>\n"
            "    Promise.resolve({}) };\n"
            "  const data = %s;\n"
            "  let handle;\n"
            "  try { handle = fn(host, hooks, data); }\n"
            "  catch (e) { host.remove(); return 'mount threw: ' + e; }\n"
            "  try { if (handle && handle.update) handle.update(data); }\n"
            "  catch (e) { host.remove(); return 'update threw: ' + e; }\n"
            "  try { if (handle && handle.unmount) handle.unmount(); }\n"
            "  catch (e) { host.remove(); return 'unmount threw: ' + e; }\n"
            "  const text = host.textContent || '';\n"
            "  host.remove();\n"
            "  return 'ok:' + text.length;\n"
            "}).catch((e) => String(e))" % (json.dumps(path),
                                             json.dumps(export), export,
                                             data_expr))
        wired = s.js("(() => %s)()" % wired_expr)
        ok = isinstance(result, str) and result.startswith("ok:")
        findings.append((name, ok, wired,
                         (("data-present " if wired else "data-absent ")
                          + result) if ok else str(result)))
    errs = list(s.console_errors) + list(s.exceptions) + \
        [str(x) for x in s.failed_requests]
    w, h = VIEWPORTS[0]
    s.call("Emulation.setDeviceMetricsOverride",
           {"width": w, "height": h, "deviceScaleFactor": 1,
            "mobile": True})
    for name, ok, wired, detail in findings:
        status = "PASS" if ok and not errs else "FAIL"
        print("feature-module %-4s %-5s wired=%s %s%s"
              % (name, status, wired, detail[:60],
                 (" console=" + json.dumps(errs)[:120]) if errs else ""))
    shot(runner, evidence, label, "feature-modules-320")
    return findings, errs


def shot(runner, evidence, label, name):
    for w, h in VIEWPORTS:
        runner.s.call("Emulation.setDeviceMetricsOverride",
                      {"width": w, "height": h, "deviceScaleFactor": 1,
                       "mobile": True})
        data = runner.s.call("Page.captureScreenshot", {"format": "png"})
        fname = "%s-%s-%dx%d.png" % (label, name, w, h)
        with open(os.path.join(evidence, fname), "wb") as f:
            f.write(base64.b64decode(data["data"]))
    w, h = VIEWPORTS[0]
    runner.s.call("Emulation.setDeviceMetricsOverride",
                  {"width": w, "height": h, "deviceScaleFactor": 1,
                   "mobile": True})


def wait(s, expr, desc, timeout=20):
    s.wait_js(expr, timeout=timeout, desc=desc)


def try_js(s, expr):
    try:
        s.js(expr)
        return True
    except Exception:
        return False


def is_v2(s):
    return bool(s.js("!!document.querySelector('.v2-tabbar')"))


def nav_v2_tab(s, label):
    return try_js(
        s,
        "[...document.querySelectorAll('.v2-tabbar button')]"
        ".find(b => b.textContent.includes(%s)).click()" % json.dumps(label))


def open_more(s):
    return try_js(s, "document.querySelector('button[aria-label=\"More "
                     "routes\"]').click()")


def nav_more(s, route_label):
    if not open_more(s):
        return False
    return try_js(
        s,
        "[...document.querySelectorAll('.v2-more button')]"
        ".find(b => b.textContent.includes(%s)).click()"
        % json.dumps(route_label))


def settle(s, ms=600):
    try_js(s, "new Promise(r => setTimeout(r, %d))" % ms)


def fresh_game_page(runner, s):
    """Navigate back to the ACTIVE life from scratch.

    Several routes (All lives, death/archive) replace the game shell, so
    any route attempted after them clicks in the wrong document. Every
    route capture must restart from a fresh active-life page (Codex
    feel-evaluation: "Its More-route sequence is unreliable")."""
    gid = runner.ctx["state"]["game_id"]
    s.js("sessionStorage.setItem('game_id', %s); true" % json.dumps(gid))
    s.call("Page.navigate", {"url": runner.base + "/game"})
    s.wait_js("!!document.querySelector('.v2-tabbar') || "
              "!!document.getElementById('tabbar') || "
              "!!document.getElementById('gameView')",
              timeout=20, desc="active life page mounted")
    settle(s, 800)



def cleanup_chrome_artifacts(proc, profile_dir, save_dir, tmp_home,
                             before):
    """QA rule: delete chromium profiles and scratch after each run.
    Waits for the browser process to die, then removes the profile dir,
    the throwaway save dir, and any org.chromium.* singleton dirs that
    appeared under tmp_home during this run."""
    if proc is not None:
        try:
            proc.terminate()
            proc.wait(timeout=8)
        except Exception:
            pass
        if proc.poll() is None:
            try:
                import signal as _sig
                os.killpg(proc.pid, _sig.SIGKILL)
                proc.wait(timeout=5)
            except Exception:
                pass
    import shutil as _sh
    if profile_dir:
        for _ in range(3):
            _sh.rmtree(profile_dir, ignore_errors=True)
            if not os.path.exists(profile_dir):
                break
            time.sleep(0.5)
    if save_dir:
        _sh.rmtree(save_dir, ignore_errors=True)
    try:
        for name in os.listdir(tmp_home):
            if name.startswith("org.chromium.") and name not in before:
                _sh.rmtree(os.path.join(tmp_home, name), ignore_errors=True)
    except Exception:
        pass


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--label", default="before")
    ap.add_argument("--evidence", default=DEFAULT_EVIDENCE)
    args = ap.parse_args()
    label, evidence = args.label, args.evidence

    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(evidence, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-feel-%s-" % label,
                                dir=os.path.join(ROOT, "game_saves"))
    httpd = server.make_server(0, save_dir=save_dir)
    httpd.dev_mode = True
    port = httpd.server_port
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    chrome = None
    profile_dir = None
    _before = set(os.listdir(TMP_HOME))
    try:
        profile_dir = tempfile.mkdtemp(prefix='chrome-profile-', dir=TMP_HOME)
        chrome, conn = launch_chrome(profile_dir)
        s = conn.new_session()
        runner = Runner("http://127.0.0.1:%d" % port, s, evidence,
                        service=httpd.service)

        # ---- A. standalone pages --------------------------------------
        for path, name in (("/", "lives"), ("/creator", "creator"),
                           ("/gallery", "gallery"), ("/game", "game-empty"),
                           ("/relationships", "relationships")):
            w, h = VIEWPORTS[0]
            s.call("Emulation.setDeviceMetricsOverride",
                   {"width": w, "height": h, "deviceScaleFactor": 1,
                    "mobile": True})
            s.call("Page.navigate", {"url": runner.base + path})
            try:
                s.wait_js("document.readyState === 'complete'", timeout=20,
                          desc="%s loaded" % path)
            except Exception as e:
                print("WARN: %s did not settle: %s" % (path, e))
            settle(s)
            shot(runner, evidence, label, name)

        # ---- B. live life ----------------------------------------------
        runner._hook_phone_viewport("feel")
        runner._hook_scenario_start("feel", scenario="married_with_child")
        runner._hook_refresh_state("feel")
        runner._hook_load_game_in_page("feel")
        settle(s)
        v2 = is_v2(s)
        print("UI generation detected: %s"
              % ("v2 shell" if v2 else "legacy"))
        shot(runner, evidence, label, "home-life")

        # ---- B2. feature modules: import + mount/unmount at 320 ---------
        if v2:
            fresh_game_page(runner, s)
            try:
                import urllib.request as _u
                with _u.urlopen(runner.base + "/api/games/" +
                                runner.ctx["state"]["game_id"]) as _r:
                    _st = json.loads(_r.read())
                s.js("window.__m3state = %s; true"
                     % json.dumps({"careers": _st.get("careers"),
                                   "skills": _st.get("skills")}))
            except Exception as e:
                print("WARN: could not inject projection for module "
                      "checks: %s" % e)
            findings, errs = check_feature_modules(runner, s, label,
                                                   evidence)
            with open(os.path.join(evidence,
                                   "%s-feature-modules.json" % label),
                      "w") as f:
                json.dump({"findings": [
                    {"module": n, "ok": ok, "wired": wired, "detail": d}
                    for n, ok, wired, d in findings],
                    "console_or_network_errors": errs}, f, indent=1)

        if v2:
            for tab in ("Occupation", "Assets", "Relationships",
                        "Activities"):
                if nav_v2_tab(s, tab):
                    settle(s)
                    shot(runner, evidence, label,
                         "tab-" + tab.lower())
                else:
                    skipped.append("tab-" + tab.lower())
            # marker element that must be present (and scrolled to) before
            # the screenshot counts as evidence for the route
            more_routes = (
                ("All lives", "#resumeView:not([hidden])", None),
                ("Character editor", "#editorPanel", "#editorPanel"),
                ("Family tree", "#familyTreeCard", "#familyTreeCard"),
                ("Dating & weddings", "#m25Card", "#m25Card"),
            )
            for route, marker, scroll_sel in more_routes:
                fresh_game_page(runner, s)
                if not nav_more(s, route):
                    skipped.append("more-" +
                                   route.lower().replace(" ", "-"))
                    continue
                ok = False
                try:
                    wait(s, "!!document.querySelector(%s)" %
                         json.dumps(marker), "route %s marker" % route, 12)
                    ok = True
                except Exception:
                    pass
                name = "more-" + route.lower().replace(" ", "-")
                if not ok:
                    # do NOT write a misleadingly-named screenshot of
                    # wherever the page happened to be (Codex review item)
                    skipped.append(name)
                    continue
                settle(s, 800)
                if scroll_sel:
                    try_js(s, "document.querySelector(%s)"
                               ".scrollIntoView({block:'start'})"
                               % json.dumps(scroll_sel))
                    settle(s, 400)
                shot(runner, evidence, label, name)
        else:
            for tab in ("summary", "education", "work", "relationships",
                        "activities", "finances"):
                if try_js(s, "document.querySelector('#tabbar "
                             "[data-screen=%s]').click()"
                          % json.dumps(tab)):
                    try:
                        wait(s, "(() => { const el = document."
                                "getElementById('screen-%s');"
                                " return !!el && !el.hidden; })()" % tab,
                             "screen-%s" % tab, 10)
                    except Exception:
                        pass
                    settle(s)
                    shot(runner, evidence, label, "tab-" + tab)
                else:
                    skipped.append("tab-" + tab)

            if try_js(s, "document.getElementById('editorToggle').click()"):
                settle(s, 1200)
                shot(runner, evidence, label, "editor")
            else:
                skipped.append("editor")
            if try_js(s, "document.getElementById('familyTreeToggle')"
                         ".click()"):
                settle(s, 1200)
                shot(runner, evidence, label, "family-tree")
            else:
                skipped.append("family-tree")
            if try_js(s, "document.getElementById('m25Toggle').click()"):
                settle(s, 1200)
                shot(runner, evidence, label, "m25-dating")
                for data_tab, nm in (("weddings", "m25-wedding-planner"),
                                     ("reputation", "m25-what-i-know")):
                    if try_js(s, "document.querySelector('#m25Card "
                                 "[data-tab=\"%s\"]').click()" % data_tab):
                        settle(s)
                        shot(runner, evidence, label, nm)
                    else:
                        skipped.append(nm)
            else:
                skipped.append("m25")

        # ---- C. decision modal + pill ----------------------------------
        runner._hook_add_adult_stranger("feel")
        runner._hook_cmd("feel", action="dating_app")
        runner._hook_refresh_state("feel")
        pending = (runner.ctx.get("state") or {}).get("pending_decision")
        if pending:
            s.js("sessionStorage.setItem('game_id', %s); true"
                 % json.dumps(runner.ctx["state"]["game_id"]))
            s.call("Page.navigate", {"url": runner.base + "/game"})
            settle(s, 1500)
            modal_sel = ("(() => { const m = document.getElementById('modal');"
                         " if (m && !m.hidden) return true;"
                         " const sh = document.querySelector('.modal-box');"
                         " return !!sh && sh.offsetParent !== null; })()")
            try:
                wait(s, modal_sel, "decision modal opens", 20)
                shot(runner, evidence, label, "decision-modal")
                if try_js(s, "document.dispatchEvent(new KeyboardEvent("
                             "'keydown', {key: 'Escape', bubbles: true}))"):
                    settle(s)
                    shot(runner, evidence, label, "decision-pill")
            except Exception:
                skipped.append("decision-modal")
                shot(runner, evidence, label, "decision-modal-absent")
        else:
            skipped.append("decision-modal (no pending decision)")

        # ---- D. all-lives / death / continuation -----------------------
        fresh_game_page(runner, s)
        if v2:
            ok = nav_more(s, "All lives")
        else:
            ok = try_js(s, "document.getElementById('backToLives').click()")
        try:
            wait(s, "(() => { const v = document.getElementById('resumeView');"
                  " return !!v && !v.hidden; })()", "all-lives view", 12)
        except Exception:
            ok = False
        if ok:
            settle(s, 1000)
            shot(runner, evidence, label, "all-lives")
        else:
            skipped.append("all-lives")

        runner._hook_refresh_state("feel")
        # resolve any pending decision first — surrender 409s on one
        st = runner.ctx.get("state") or {}
        d = st.get("pending_decision")
        if d:
            opts = (d.get("options") or [])
            opt = next((o for o in opts if not o.get("disabled")), None) \
                or (opts[0] if opts else None)
            if opt:
                runner._hook_cmd("feel", action="resolve_decision",
                                 payload={"decision_id": d["id"],
                                          "option_id": opt["id"],
                                          "inputs": {}})
                runner._hook_refresh_state("feel")
        runner._hook_surrender_to_death("feel")
        s.js("sessionStorage.setItem('game_id', %s); true"
             % json.dumps(runner.ctx["state"]["game_id"]))
        s.call("Page.navigate", {"url": runner.base + "/game"})
        settle(s, 2000)
        shot(runner, evidence, label, "legacy-continuation")

        n = len([f for f in os.listdir(evidence)
                 if f.startswith(label + "-")])
        print("feel-screens [%s]: %d files in %s" % (label, n, evidence))
        if skipped:
            print("skipped: %s" % ", ".join(skipped))
        return 0
    finally:
        try:
            if chrome is not None:
                chrome.terminate()
        except Exception:
            pass
        # QA rule: never leave chromium profiles or save dirs behind
        cleanup_chrome_artifacts(chrome, profile_dir, save_dir,
                                 TMP_HOME, _before)
        httpd.shutdown()


if __name__ == "__main__":
    sys.exit(main())
