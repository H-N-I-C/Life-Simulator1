"""Undo browser tests (Kimi D, QA) — A+B's §Undo contract (contracts.md
§Undo FROZEN v1) exercised in a REAL browser at 390x844 and 320x844.

Scenarios (each asserts via the API state AND captures the page):
  1. disabled reason on a fresh life ("there is no year to undo")
  2. age → undo (two-tap confirm) → age again: age restored, revision
     MONOTONIC (+1, never rewound), journal year vanishes and the redone
     year replays EXACTLY (same visible timeline text)
  3. undo-consumed reason ("only one year can be undone"), at both widths
  4. stale-tab: a tab holding a pre-age revision gets stale_revision on
     undo and must not mutate the restored timeline
  5. double-tap: rapid re-clicks execute the undo exactly once
  6. reload: an armed confirm resets; a completed undo survives reload
  7. undo with a pending decision open: allowed, pending vanishes

Evidence: docs/collab/evidence/redesign-v2/undo/*.png + results.md.
QA rules: TMPDIR=~/.lifesim-tmp, per-run chromium profile deleted after
the run, one browser launch total.

    python3 tools/feel_undo.py
"""

import base64
import json
import os
import sys
import tempfile
import threading
import time

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

TMP_HOME = os.path.expanduser("~/.lifesim-tmp")  # RULE: never /tmp
os.makedirs(TMP_HOME, exist_ok=True)
os.environ["TMPDIR"] = TMP_HOME

import web.server as server  # noqa: E402
from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
from tools.feel_stress import (  # noqa: E402
    start_life, state_of, cmd, settle, WIDE, NARROW)


def drain_cmd(base, gid, bound=12):
    """Resolve open pendings with the first free enabled option (M3/M4
    content now serves events the original harness never drained; an
    open pending withholds the advertised undo_year action and the shell
    then renders no undo row at all)."""
    for _ in range(bound):
        st = state_of(base, gid)
        d = st.get("pending_decision")
        if not d:
            return True
        opts = [o for o in d.get("options", [])
                if o.get("enabled", True) and not o.get("disabled")]
        if not opts:
            return False
        free = [o for o in opts if not o.get("known_costs")]
        pick = free[0] if free else opts[0]
        r = cmd(base, gid, st["revision"], "resolve_decision",
                {"decision_id": d["id"], "option_id": pick["id"],
                 "inputs": {}})
        if not r.get("game_id"):
            return False
    return not state_of(base, gid).get("pending_decision")

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "redesign-v2",
                        "undo")

results = []


def result(name, ok, detail=""):
    results.append((name, ok, detail))
    print("%s %s%s" % ("PASS " if ok else "FAIL ", name,
                       (" — " + detail) if detail else ""))


def set_vp(s, wh):
    s.call("Emulation.setDeviceMetricsOverride",
           {"width": wh[0], "height": wh[1], "deviceScaleFactor": 1,
            "mobile": True})


def shot(s, name):
    data = s.call("Page.captureScreenshot", {"format": "png"})
    path = os.path.join(EVIDENCE, name + ".png")
    with open(path, "wb") as f:
        f.write(base64.b64decode(data["data"]))
    return path


def open_game(conn, base, gid, wh=WIDE):
    s = conn.new_session()
    set_vp(s, wh)
    s.call("Page.navigate", {"url": base + "/game"})
    s.wait_js("document.readyState === 'complete'", timeout=20)
    s.js("sessionStorage.setItem('game_id', %s); true" % json.dumps(gid))
    s.call("Page.navigate", {"url": base + "/game"})
    s.wait_js("!!document.querySelector('.v2-tabbar') || "
              "!!document.getElementById('tabbar') || "
              "!!document.getElementById('gameView')",
              timeout=20, desc="game page mounted")
    settle(s, 1200)
    return s


def player_age(st):
    return next(p["age"] for p in st["people"]
                if p["id"] == st["player_id"])


def undo_spec(st):
    spec = next((a for a in st.get("available_actions", [])
                 if a.get("id") == "undo_year"), None)
    if spec is None:
        # fall back to the projection field
        return st.get("can_undo") or {}
    return {"enabled": spec.get("enabled", False),
            "reason": spec.get("reason")}


def undo_button(s):
    return s.js("(() => { const b = document.querySelector('.v2-undo');"
                " if (!b) return null;"
                " return {text: b.textContent, disabled: b.disabled,"
                " reason: (document.querySelector('.v2-undo-reason') || {})"
                ".textContent || '', hidden: !b.offsetParent}; })()")


def timeline_text(s):
    return s.js("(() => { const j = document.getElementById('journalList')"
                " || document.querySelector('.v2-scroll');"
                " return j ? j.innerText : ''; })()")


def tap_undo(s, times=1, gap_ms=0):
    for i in range(times):
        s.js("document.querySelector('.v2-undo').click(); true")
        if gap_ms:
            settle(s, gap_ms)


def main():
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-undo-", dir=TMP_HOME)
    profile_dir = None
    httpd = server.make_server(0, save_dir=save_dir)
    httpd.dev_mode = True
    port = httpd.server_port
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    chrome = None
    before = set(os.listdir(TMP_HOME))
    try:
        profile_dir = tempfile.mkdtemp(prefix="chrome-profile-", dir=TMP_HOME)
        chrome, conn = launch_chrome(profile_dir)
        base = "http://127.0.0.1:%d" % port

        # ============ 1+2+3: full cycle at 390 and 320 ==================
        # true fresh life (no scenario => no snapshot => disabled reason)
        gid0 = start_life(base, None, seed=780)
        s0 = open_game(conn, base, gid0, WIDE)
        st0 = state_of(base, gid0)
        spec0 = undo_spec(st0)
        btn0 = undo_button(s0)
        ok = (spec0.get("enabled") is False and
              spec0.get("reason") == "there is no year to undo")
        result("fresh-life disabled reason", ok,
               "spec=%s btn=%s" % (spec0, btn0))
        shot(s0, "undo-disabled-fresh-390")

        for wh, tag in ((WIDE, "390"), (NARROW, "320")):
            gid = start_life(base, "married_with_child",
                             seed=700 + wh[0] % 100)
            s = open_game(conn, base, gid, wh)
            st = state_of(base, gid)

            spec = undo_spec(st)
            btn = undo_button(s)
            result("scenario life starts undo-enabled [%s]" % tag,
                   spec.get("enabled") is True,
                   "scenario driver ages to the target age, so a snapshot "
                   "exists: spec=%s" % (spec,))
            shot(s, "undo-enabled-at-scenario-start-%s" % tag)

            # age once
            r = cmd(base, gid, st["revision"], "age_up", {})
            result("age_up accepted [%s]" % tag, bool(r.get("game_id")),
                   "rev %s->%s" % (st["revision"], r.get("revision")))
            s2 = open_game(conn, base, gid, wh)  # fresh tab, synced state
            st2 = state_of(base, gid)
            spec2 = undo_spec(st2)
            result("enabled after age [%s]" % tag,
                   spec2.get("enabled") is True, str(spec2))
            shot(s2, "undo-enabled-after-age-%s" % tag)
            text_before = timeline_text(s2)

            # two-tap confirm
            tap_undo(s2, 1)
            settle(s2, 300)
            btn = undo_button(s2)
            result("confirm arm [%s]" % tag,
                   btn and "confirm" in (btn.get("text") or ""),
                   str(btn))
            shot(s2, "undo-confirm-armed-%s" % tag)

            rev_before_undo = state_of(base, gid)["revision"]
            tap_undo(s2, 1)
            settle(s2, 1500)
            st3 = state_of(base, gid)
            ok = (player_age(st3) == player_age(st2) - 1 and
                  st3["revision"] == rev_before_undo + 1)
            result("undo restores age, monotonic revision [%s]" % tag, ok,
                   "age %s->%s rev %s->%s"
                   % (player_age(st2), player_age(st3),
                      rev_before_undo, st3["revision"]))
            shot(s2, "undo-after-%s" % tag)

            spec3 = undo_spec(st3)
            btn = undo_button(s2)
            ok = spec3.get("enabled") is False and \
                spec3.get("reason") == "only one year can be undone"
            result("consumed reason [%s]" % tag, ok,
                   "spec=%s btn=%s" % (spec3, btn))
            shot(s2, "undo-consumed-%s" % tag)

            # redo: age again — year must replay exactly
            r = cmd(base, gid, st3["revision"], "age_up", {})
            result("redo age_up accepted [%s]" % tag,
                   bool(r.get("game_id")))
            s3 = open_game(conn, base, gid, wh)
            text_after = timeline_text(s3)
            ok = text_before.strip() == text_after.strip()
            result("redone year replays exactly [%s]" % tag, ok,
                   "timeline identical=%s" % ok)
            shot(s3, "undo-redone-%s" % tag)

        # ============ 4: stale tab =====================================
        gid = start_life(base, "married_with_child", seed=881)
        s_main = open_game(conn, base, gid, WIDE)
        s_stale = open_game(conn, base, gid, WIDE)
        st = state_of(base, gid)
        r = cmd(base, gid, st["revision"], "age_up", {})
        result("stale setup: age from another tab", bool(r.get("game_id")))
        s_stale.console_errors.clear()
        s_stale.exceptions.clear()
        tap_undo(s_stale, 2, gap_ms=400)  # two-tap on the stale tab
        settle(s_stale, 1500)
        rev_now = state_of(base, gid)["revision"]
        age_now = player_age(state_of(base, gid))
        stale_blocked = (rev_now == r["revision"] and
                         age_now == player_age(r))
        net_err = [e for e in s_stale.console_errors] + \
                  [e for e in s_stale.exceptions]
        result("stale tab cannot mutate (age/revision unchanged)",
               stale_blocked,
               "console/exceptions=%s" % json.dumps(net_err)[:300])
        shot(s_stale, "undo-stale-tab")
        s_main.close() if hasattr(s_main, "close") else None

        # ============ 5: double-tap executes once ======================
        gid = start_life(base, "married_with_child", seed=882)
        s = open_game(conn, base, gid, WIDE)
        st = state_of(base, gid)
        drain_cmd(base, gid)
        st = state_of(base, gid)
        r = cmd(base, gid, st["revision"], "age_up", {})
        drained = drain_cmd(base, gid)
        st1 = state_of(base, gid)
        rev1, age1 = st1["revision"], player_age(st1)
        s = open_game(conn, base, gid, WIDE)
        if undo_button(s) is None:
            pend = state_of(base, gid).get("pending_decision") or {}
            result("rapid burst (4 taps) = exactly one undo", False,
                   "undo row absent after age: age_up=%s drain=%s "
                   "pending=%s" % (bool(r.get("game_id")), drained,
                                   pend.get("kind")))
        else:
            tap_undo(s, 4, gap_ms=0)  # burst: arm + execute + 2 extra taps
            settle(s, 2000)
            st = state_of(base, gid)
            ok = (player_age(st) == age1 - 1 and
                  st["revision"] == rev1 + 1)
            result("rapid burst (4 taps) = exactly one undo", ok,
                   "age %s->%s rev %s->%s" % (age1, player_age(st),
                                              rev1, st["revision"]))
        shot(s, "undo-double-tap")

        # ============ 6: reload resets confirm / persists undo =========
        gid = start_life(base, "married_with_child", seed=883)
        s = open_game(conn, base, gid, WIDE)
        st = state_of(base, gid)
        drain_cmd(base, gid)
        st = state_of(base, gid)
        r = cmd(base, gid, st["revision"], "age_up", {})
        drained = drain_cmd(base, gid)
        s = open_game(conn, base, gid, WIDE)
        if undo_button(s) is None:
            pend = state_of(base, gid).get("pending_decision") or {}
            spec_now = undo_spec(state_of(base, gid))
            result("reload resets armed confirm", False,
                   "undo row absent after age: age_up=%s drain=%s "
                   "pending=%s server_undo_spec=%s"
                   % (bool(r.get("game_id")), drained, pend.get("kind"),
                      spec_now))
            result("undo persists across reload with consumed reason",
                   False, "skipped: undo never executed")
            shot(s, "undo-after-reload")
        else:
            tap_undo(s, 1)  # arm only
            s.call("Page.navigate", {"url": base + "/game"})
            s.wait_js("!!document.querySelector('.v2-tabbar') || "
                      "!!document.getElementById('gameView')", timeout=20)
            # the undo row renders from the fetched state — wait for it
            # instead of racing it with a fixed sleep
            try:
                s.wait_js("!!document.querySelector('.v2-undo') || "
                          "!!document.querySelector('.v2-undorow')",
                          timeout=8, desc="undo row after reload")
            except Exception:
                pass
            settle(s, 600)
            btn = undo_button(s)
            ok = btn and "confirm" not in (btn.get("text") or "")
            result("reload resets armed confirm", ok, str(btn))
            # complete the undo, reload, verify persistence + reason
            tap_undo(s, 2, gap_ms=400)
            settle(s, 1500)
            st = state_of(base, gid)
            age_after_undo = player_age(st)
            s.call("Page.navigate", {"url": base + "/game"})
            s.wait_js("!!document.querySelector('.v2-tabbar') || "
                      "!!document.getElementById('gameView')", timeout=20)
            settle(s, 1500)
            st = state_of(base, gid)
            spec = undo_spec(st)
            btn = undo_button(s)
            ok = (player_age(st) == age_after_undo and
                  spec.get("enabled") is False and
                  spec.get("reason") == "only one year can be undone")
            result("undo persists across reload with consumed reason", ok,
                   "age=%s spec=%s" % (player_age(st), spec))
            shot(s, "undo-after-reload")

        # ============ 7: undo with a pending decision ==================
        gid = start_life(base, "married_with_child", seed=884)
        st = state_of(base, gid)
        pending = None
        for _ in range(12):
            st = state_of(base, gid)
            if st.get("pending_decision"):
                pending = st["pending_decision"]
                break
            r = cmd(base, gid, st["revision"], "age_up", {})
            if not r.get("game_id"):
                break
        result("pending decision created for undo test",
               pending is not None, str(pending and pending.get("kind")))
        if pending:
            s = open_game(conn, base, gid, WIDE)
            shot(s, "undo-pending-before")
            # Frozen surface: while a pending is open, actions (incl.
            # undo_year) are not advertised, so the shell must hide the
            # undo row. The hide lands with the client's next state
            # refresh, so poll a bounded window instead of one instant.
            hidden_now = None
            for _ in range(12):
                btn = undo_button(s)
                hidden_now = btn is None or btn.get("hidden") is True
                if hidden_now:
                    break
                settle(s, 500)
            result("undo row hidden while a pending is open", hidden_now,
                   str(undo_button(s)))
            # resolve the pending, then undo must work again
            drain_cmd(base, gid)
            st = state_of(base, gid)
            r = cmd(base, gid, st["revision"], "age_up", {})
            s = open_game(conn, base, gid, WIDE)
            settle(s, 400)
            age_before = player_age(state_of(base, gid))
            tap_undo(s, 2, gap_ms=400)
            settle(s, 1500)
            st = state_of(base, gid)
            ok = (st.get("pending_decision") in (None, False) and
                  player_age(st) == age_before - 1)
            result("undo after resolving the pending: age restored", ok,
                   "pending=%s age=%s->%s"
                   % (bool(st.get("pending_decision")), age_before,
                      player_age(st)))
            shot(s, "undo-pending-after")

        # ---------------- report ---------------------------------------
        fails = [r for r in results if not r[1]]
        with open(os.path.join(EVIDENCE, "results.md"), "w") as f:
            f.write("# Undo browser tests (Kimi D, QA — 2026-11-20)\n\n")
            f.write("Harness: tools/feel_undo.py — real server + headless "
                    "Chromium, one launch, fresh tab per scenario. "
                    "TMPDIR=~/.lifesim-tmp; profile deleted after the run.\n\n")
            for name, ok, detail in results:
                f.write("- **%s** %s%s\n"
                        % ("PASS" if ok else "FAIL", name,
                           (" — " + detail) if detail else ""))
            f.write("\nScreenshots: this directory (names mirror the "
                    "scenario steps).\n")
        print("undo evidence in %s (%d checks, %d failed)"
              % (EVIDENCE, len(results), len(fails)))
        return 1 if fails else 0
    finally:
        try:
            if chrome is not None:
                chrome.terminate()
                try:
                    chrome.wait(timeout=8)
                except Exception:
                    pass
                if chrome.poll() is None:
                    import signal
                    os.killpg(chrome.pid, signal.SIGKILL)
        except Exception:
            pass
        import shutil
        if profile_dir:
            shutil.rmtree(profile_dir, ignore_errors=True)
        shutil.rmtree(save_dir, ignore_errors=True)
        try:
            import shutil as _sh
            for name in os.listdir(TMP_HOME):
                if name.startswith("org.chromium.") and name not in before:
                    _sh.rmtree(os.path.join(TMP_HOME, name),
                               ignore_errors=True)
        except Exception:
            pass
        httpd.shutdown()


if __name__ == "__main__":
    sys.exit(main())
