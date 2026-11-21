"""M3c independent evidence runner (Kimi D, QA).

    python3 tools/m3c_evidence.py

Plays tests/browser/m3c_checks.py against the real service + headless
Chromium at 390x844 and 320x844: contrasting starts, all three ladders
climbed by real play, an M2 legacy-university save migration, a visual
pass, and a per-year money-conservation audit. Honest PASS/FAIL/PENDING/
SKIP per step; FAIL gates the exit code.

Evidence: docs/collab/evidence/M3/m3c.{md,json} + m3c-*.png.
QA rules: TMPDIR=~/.lifesim-tmp, one chromium launch, profile + save dir
deleted after the run.
"""

import base64
import json
import os
import shutil
import sys
import tempfile
import threading
import time
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

TMP_HOME = os.path.expanduser("~/.lifesim-tmp")  # RULE: never /tmp
os.makedirs(TMP_HOME, exist_ok=True)
os.environ["TMPDIR"] = TMP_HOME

import web.server as server  # noqa: E402
from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
from tools.browser_check import QA_APPEARANCE  # noqa: E402
from tools.m3_journeys import JourneyCtx, RUNNER  # noqa: E402
from tests.browser.m3c_checks import CHECKS  # noqa: E402

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "M3")


def main():
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-m3c-", dir=TMP_HOME)
    profile_dir = None
    httpd = server.make_server(0, save_dir=save_dir)
    httpd.dev_mode = True
    RUNNER["httpd"] = httpd
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    base = "http://127.0.0.1:%d" % httpd.server_port
    before = set(os.listdir(TMP_HOME))
    chrome = None
    rows = []
    try:
        profile_dir = tempfile.mkdtemp(prefix="chrome-profile-", dir=TMP_HOME)
        chrome, conn = launch_chrome(profile_dir)
        seed = 7717
        for j in CHECKS:
            ctx = JourneyCtx(base, conn, EVIDENCE, j["id"])
            ctx.start(j.get("scenario"), seed)
            seed += 1
            print("== %s: %s (%s)" % (j["id"], j["title"],
                                      ctx.game_id[:9]))
            for sid, title, fn in j["steps"]:
                try:
                    status, detail = fn(ctx)
                except Exception as exc:  # harness/step crash: honest FAIL
                    status, detail = "FAIL", "step crashed: %r" % exc
                rows.append({"check": j["id"], "step": sid,
                             "title": title, "status": status,
                             "detail": detail})
                print("  %-7s %s — %s" % (status, sid, detail[:160]))
        counts = {}
        for r in rows:
            counts[r["status"]] = counts.get(r["status"], 0) + 1
        with open(os.path.join(EVIDENCE, "m3c.json"), "w") as f:
            json.dump({"counts": counts, "rows": rows}, f, indent=1)
        with open(os.path.join(EVIDENCE, "m3c.md"), "w") as f:
            f.write("# M3c independent evidence (Kimi D QA, %s)\n\n"
                    % time.strftime("%Y-%m-%d"))
            f.write("Harness: tools/m3c_evidence.py + "
                    "tests/browser/m3c_checks.py — real service + headless "
                    "Chromium at 390x844 and 320x844. Ladders are climbed "
                    "by real play (train_skill / education_enroll+study / "
                    "careers_apply+accept / careers_promote); the only "
                    "env-level hook is the documented pre-M3 save delta.\n")
            cur = None
            for r in rows:
                if r["check"] != cur:
                    cur = r["check"]
                    f.write("\n## %s\n" % cur)
                f.write("- **%s** `%s` — %s\n"
                        % (r["status"], r["step"],
                           (r["detail"] or r["title"])[:240]))
            f.write("\nCounts: %s\n" % json.dumps(counts))
        print("counts: %s" % json.dumps(counts))
        print("evidence: %s" % EVIDENCE)
        return 1 if any(r["status"] == "FAIL" for r in rows) else 0
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
        if profile_dir:
            shutil.rmtree(profile_dir, ignore_errors=True)
        shutil.rmtree(save_dir, ignore_errors=True)
        for name in os.listdir(TMP_HOME):
            if name.startswith("org.chromium.") and name not in before:
                shutil.rmtree(os.path.join(TMP_HOME, name),
                              ignore_errors=True)
        httpd.shutdown()


if __name__ == "__main__":
    sys.exit(main())
