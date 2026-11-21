"""Feel-pass stress evidence (Kimi D, QA): narrow width, long content,
software-keyboard behavior.

Complements tools/feel_screens.py (which covers every screen at 390x844 and
320x568) with the cases Codex's evaluation flags as not certified
(docs/collab/redesign-v2/codex-feel-evaluation.md F11/F13):

  1. narrow-320x844: fresh-life home, event modal, Person overlay at
     320x844 (Codex's narrow geometry) in addition to the 568-height floor.
  2. long-content: a life aged into old age (long journal, many people),
     home + Person overlay at 390x844 and 320x844.
  3. keyboard: real CDP key events (Input.dispatchKeyEvent, trusted
     keypresses) typed into a FOCUSED control:
       - /creator #name (plain text field)
       - an in-game decision text input (baby naming via the pregnant_25
         scenario), focused inside the event modal
     Headless Chromium has no OS software keyboard; this verifies focus
     handling, input acceptance, layout stability while typing, and that
     no console errors/exceptions occur — it is not a photo of an OS IME.

Writes feel/stress/*.png (prefix: narrow-, long-, keyboard-) and prints a
summary. Exit non-zero on harness errors only.

    python3 tools/feel_stress.py [--evidence DIR]
"""

import base64
import json
import os
import sys
import tempfile
import shutil
import threading
import time
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

# Harness scratch must live under /home in a PRIVATE dir (chromium
# ProcessSingleton refuses a world-readable parent like /home itself).
TMP_HOME = os.path.expanduser("~/.lifesim-tmp")  # RULE: never /tmp
os.makedirs(TMP_HOME, exist_ok=True)
os.environ["TMPDIR"] = TMP_HOME

import web.server as server  # noqa: E402
from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
from tools.browser_check import Runner, QA_APPEARANCE  # noqa: E402

NARROW = (320, 844)
WIDE = (390, 844)

notes = []


def note(msg):
    notes.append(msg)
    print(msg)


def set_vp(s, wh):
    s.call("Emulation.setDeviceMetricsOverride",
           {"width": wh[0], "height": wh[1], "deviceScaleFactor": 1,
            "mobile": True})


def shot(s, evidence, name):
    data = s.call("Page.captureScreenshot", {"format": "png"})
    path = os.path.join(evidence, name + ".png")
    with open(path, "wb") as f:
        f.write(base64.b64decode(data["data"]))
    return path


def settle(s, ms=700):
    s.js("new Promise(r => setTimeout(r, %d))" % ms)


def start_life(base, scenario, seed=777):
    profile = {
        "first_name": "Dev", "last_name": "Tester", "gender": "female",
        "country": "Australia", "seed": seed,
        "appearance": QA_APPEARANCE,
        # request_id is an idempotence key: must be unique per life AND
        # per process (a replayed id can resurrect another run's game_id
        # against a fresh save dir -> 404s)
        "request_id": "feel-%s-%s-%d" % (
            scenario or "fresh", seed, os.getpid())}
    if scenario:
        profile["scenario"] = scenario
    req = urllib.request.Request(
        base + "/api/games", data=json.dumps(profile).encode(),
        headers={"Content-Type": "application/json"})
    return json.loads(urllib.request.urlopen(req).read())["game_id"]


def state_of(base, gid):
    with urllib.request.urlopen(base + "/api/games/" + gid) as resp:
        return json.loads(resp.read())


_cmd_seq = [0]


def cmd(base, gid, rev, action, payload):
    _cmd_seq[0] += 1
    req = urllib.request.Request(
        base + "/api/games/" + gid + "/actions",
        data=json.dumps({"expected_revision": rev,
                         # request_id is an idempotence key: unique per call
                         "request_id": "feel-stress-%d" % _cmd_seq[0],
                         "action": action, "payload": payload}).encode(),
        headers={"Content-Type": "application/json"})
    try:
        return json.loads(urllib.request.urlopen(req).read())
    except urllib.error.HTTPError as e:
        return {"_status": e.code, "_body": e.read()[:200]}


def resolve_pending(base, gid, st):
    d = st.get("pending_decision")
    if not d:
        return st
    opts = [o for o in d.get("options", []) if not o.get("disabled")] \
        or d.get("options", [])
    if not opts:
        return st
    r = cmd(base, gid, st["revision"], "resolve_decision",
            {"decision_id": d["id"], "option_id": opts[0]["id"],
             "inputs": {}})
    if r.get("game_id"):
        return state_of(base, gid)
    return st


def close_overlays(s):
    """The v2 shell restores overlay/person state on reload; close any
    restored overlay so a 'home' screenshot is really the home screen."""
    for _ in range(6):
        if not person_overlay_open(s) and not event_modal_visible(s):
            return True
        s.js("(() => { const b = document.querySelector('.v2-back');"
             " if (b && !b.hidden) { b.click(); return; }"
             " document.dispatchEvent(new KeyboardEvent('keydown',"
             "{key: 'Escape', bubbles: true})); })()")
        time.sleep(0.6)
    return not person_overlay_open(s) and not event_modal_visible(s)


def open_game_page(conn, runner, gid, wh=WIDE):
    """Open the game in a FRESH tab. The v2 shell pushes history state and
    restores overlays/lives from it on reload; sharing one tab across
    lives resurrects older lives via the back/forward cache. A new target
    per call has empty history and no restoration surprises."""
    s = conn.new_session()
    runner.s = s
    set_vp(s, wh)
    # sessionStorage is denied on about:blank; land on the origin first.
    s.call("Page.navigate", {"url": runner.base + "/game"})
    s.wait_js("document.readyState === 'complete'", timeout=20)
    s.js("sessionStorage.setItem('game_id', %s); true" % json.dumps(gid))
    s.call("Page.navigate", {"url": runner.base + "/game"})
    s.wait_js("!!document.querySelector('.v2-tabbar') || "
              "!!document.getElementById('tabbar') || "
              "!!document.getElementById('gameView')",
              timeout=20, desc="game page mounted")
    settle(s, 1200)
    return s


def event_modal_visible(s):
    return s.js(
        "(() => { const m = document.getElementById('modal');"
        " if (m && !m.hidden) return true;"
        " const sh = document.querySelector('.modal-box');"
        " return !!sh && sh.offsetParent !== null; })()")


def person_overlay_open(s):
    return s.js(
        "(() => { const h = document.querySelector('.v2-person-host');"
        " if (h && !h.hidden && h.children.length) return true;"
        " const p = document.getElementById('personView');"
        " return !!p && !p.hidden; })()")


def open_person_and_shot(s, evidence, name):
    """Click the first person chip; only write the screenshot once the
    Person overlay is actually mounted (never write a misleadingly-named
    duplicate of the underlying screen)."""
    clicked = s.js(
        "(() => { const c = document.querySelector('.v2-person-link');"
        " if (!c) return false; c.click(); return true; })()")
    if not clicked:
        note("SKIP %s: no .v2-person-link chip" % name)
        return False
    ok = False
    for _ in range(16):
        if person_overlay_open(s):
            ok = True
            break
        time.sleep(0.5)
    if not ok:
        note("SKIP %s: person overlay did not open after chip click" % name)
        return False
    settle(s, 600)
    shot(s, evidence, name)
    return True



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
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    evidence = os.path.join(ROOT, "docs", "collab", "evidence",
                            "redesign-v2", "feel", "stress")
    if len(sys.argv) > 2 and sys.argv[1] == "--evidence":
        evidence = sys.argv[2]
    os.makedirs(evidence, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-stress-", dir=TMP_HOME)
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
        base = runner.base
        v2 = None

        def errors_so_far():
            return {
                "console": list(s.console_errors),
                "exceptions": list(s.exceptions),
                "failed": list(s.failed_requests),
            }

        # ================= 1+3b: fresh life, narrow + keyboard ==========
        gid = start_life(base, "married_with_child")
        st = state_of(base, gid)
        s = open_game_page(conn, runner, gid)
        v2 = bool(s.js("!!document.querySelector('.v2-tabbar')"))
        note("UI generation: %s" % ("v2 shell" if v2 else "legacy"))

        # 1. narrow 320x844 on home
        set_vp(s, NARROW)
        settle(s, 500)
        shot(s, evidence, "narrow-home-320x844")

        # event modal: age until a pending decision appears (bounded)
        for _ in range(12):
            st = state_of(base, gid)
            if st.get("pending_decision"):
                break
            r = cmd(base, gid, st["revision"], "age_up", {})
            if not r.get("game_id"):
                break
        st = state_of(base, gid)
        if st.get("pending_decision"):
            s = open_game_page(conn, runner, gid, NARROW)
            ok = False
            for _ in range(20):
                if event_modal_visible(s):
                    ok = True
                    break
                time.sleep(0.5)
            if ok:
                shot(s, evidence, "narrow-event-modal-320x844")
            else:
                note("SKIP narrow-event-modal: pending decision did not "
                     "surface a modal in the page")
            # resolve it so later sections are not blocked
            st = resolve_pending(base, gid, state_of(base, gid))
        else:
            note("SKIP narrow-event-modal: no pending decision after "
                 "12 age-ups")

        # Person overlay at 320x844 (fresh page)
        s = open_game_page(conn, runner, gid, NARROW)
        open_person_and_shot(s, evidence, "narrow-person-320x844")

        # 3b. keyboard: creator page #name (plain field)
        s.console_errors.clear(); s.exceptions.clear()
        set_vp(s, NARROW)
        s.call("Page.navigate", {"url": base + "/creator"})
        s.wait_js("document.readyState === 'complete'", timeout=20)
        settle(s, 800)
        s.js("document.getElementById('name').focus(); true")
        focused = s.js("document.activeElement === "
                       "document.getElementById('name')")
        for ch in "Maya":
            s.call("Input.dispatchKeyEvent",
                   {"type": "keyDown", "key": ch, "text": ch})
            s.call("Input.dispatchKeyEvent",
                   {"type": "keyUp", "key": ch})
        settle(s, 300)
        val = s.js("document.getElementById('name').value")
        shot(s, evidence, "keyboard-creator-name-320x844")
        note("keyboard creator: focused=%s value_after_MAYA=%r errors=%s"
             % (focused, val, errors_so_far()))

        # ================= 2: long content ==============================
        gid2 = start_life(base, "married_with_child", seed=4242)
        st = state_of(base, gid2)
        target = st["people"] and next(
            p for p in st["people"]
            if p["id"] == st["player_id"])["age"] + 35
        aged = 0
        for i in range(60):
            st = state_of(base, gid2)
            player = next(p for p in st["people"]
                          if p["id"] == st["player_id"])
            if player["age"] >= target or st.get("status") == "dead":
                break
            st = resolve_pending(base, gid2, st)
            r = cmd(base, gid2, st["revision"], "age_up", {})
            if r.get("game_id"):
                aged += 1
        st = state_of(base, gid2)
        player = next(p for p in st["people"]
                      if p["id"] == st["player_id"])
        note("long-content: aged %d years, player age %s, status %s"
             % (aged, player["age"], st.get("status")))
        for wh, tag in ((WIDE, "390x844"), (NARROW, "320x844")):
            s = open_game_page(conn, runner, gid2, wh)
            close_overlays(s)
            shot(s, evidence, "long-home-%s" % tag)
            open_person_and_shot(s, evidence, "long-person-%s" % tag)

        # ============ 3c: in-game decision text input ===================
        gid3 = start_life(base, "pregnant_25", seed=99)
        got_input = False
        for i in range(25):
            st = state_of(base, gid3)
            d = st.get("pending_decision")
            if d and d.get("kind") == "baby_naming":
                got_input = True
                break
            st = resolve_pending(base, gid3, st)
            r = cmd(base, gid3, st["revision"], "age_up", {})
            if not r.get("game_id"):
                break
        if got_input:
            s = open_game_page(conn, runner, gid3, WIDE)
            ok = False
            for _ in range(20):
                if event_modal_visible(s):
                    ok = True
                    break
                time.sleep(0.5)
            if ok:
                s.console_errors.clear(); s.exceptions.clear()
                has = s.js("!!document.querySelector('.v2e-opt-inputs input,"
                           " #modal input, .modal-box input, input')")
                s.js("(() => { const i = document.querySelector("
                     "'.v2e-opt-inputs input') || document.querySelector("
                     "'.modal-box input') || document.querySelector("
                     "'#modal input'); if (i) i.focus(); return !!i; })()")
                for ch in "Robin":
                    s.call("Input.dispatchKeyEvent",
                           {"type": "keyDown", "key": ch, "text": ch})
                    s.call("Input.dispatchKeyEvent",
                           {"type": "keyUp", "key": ch})
                settle(s, 300)
                val = s.js("(() => { const i = document.activeElement;"
                           " return i && i.value !== undefined ? i.value :"
                           " null; })()")
                shot(s, evidence, "keyboard-baby-naming-input-390x844")
                note("keyboard baby-naming: input_present=%s "
                     "active_value=%r errors=%s"
                     % (has, val, errors_so_far()))
            else:
                note("SKIP keyboard baby-naming: modal never surfaced")
        else:
            note("SKIP keyboard baby-naming: no baby_naming decision in "
                 "25 age-ups of pregnant_25 seed 99 (reported to engine "
                 "owner as a scenario observation)")

        print("stress evidence in %s" % evidence)
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
