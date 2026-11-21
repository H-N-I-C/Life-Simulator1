"""Feel-pass evidence shots (Kimi B, redesign-v2 F1/F2/F3/F7/F8).

Starts a throwaway server, plays a fresh life headlessly and captures the
in-game chrome at 390 and 320 widths:
  b-home-<w>.png    Life screen (full-screen, no site nav, stats dock, bar)
  b-aged-<w>.png    after several age-ups (journal grew, stats animated)
  b-people-<w>.png  Relationships tab
Saves under docs/collab/redesign-v2/evidence/feel/. The "before" shots are
docs/collab/redesign-v2/audit/*.png (same widths, taken 2026-11-20).

Run: python3 tools/feel_pass_shots.py
"""

import base64
import os
import shutil
import sys
import tempfile
import threading

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import web.server as server  # noqa: E402
from tools.cdp_client import launch_chrome  # noqa: E402

OUT = os.path.join(ROOT, "docs", "collab", "redesign-v2", "evidence", "feel")
VIEWPORTS = [(390, 844), (320, 844)]


def shot(page, name):
    data = page.call("Page.captureScreenshot", {"format": "png"})["data"]
    path = os.path.join(OUT, name + ".png")
    with open(path, "wb") as f:
        f.write(base64.b64decode(data))
    print("wrote", path)


def settle_decisions(page, max_n=8):
    """Resolve any pending decision with its first enabled option."""
    for _ in range(max_n):
        open_ = page.js(
            "!document.getElementById('modal').hidden && "
            "!!document.querySelector('.v2c-options, .v2e-card')")
        if not open_:
            return
        page.js("(() => {const next = document.querySelector('.v2e-continue') || "
                "Array.from(document.querySelectorAll('.v2c-option, .v2e-option'))"
                ".find(b => !b.disabled); if (next) next.click();})()")
        page.wait_idle()


def main():
    os.makedirs(OUT, exist_ok=True)
    scratch = os.environ.get("FEEL_TMPDIR", os.path.join(ROOT, ".review-tmp", "codex-feel-tmp"))
    os.makedirs(scratch, exist_ok=True)
    tmp = tempfile.mkdtemp(prefix="feel-shots-", dir=scratch)
    httpd = server.make_server(0, save_dir=tmp)
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    base = "http://127.0.0.1:%d" % httpd.server_port
    proc, conn = launch_chrome()
    try:
        for w, h in VIEWPORTS:
            st = httpd.service.new_game(
                dict(first_name="Feel", last_name="Review", gender="female",
                     country="Australia", seed=7), "feel-shots-%d" % w)
            page = conn.new_session("about:blank")
            page.call("Emulation.setDeviceMetricsOverride",
                      {"width": w, "height": h,
                       "deviceScaleFactor": 2, "mobile": True})
            page.call("Page.navigate",
                      {"url": base + "/game?game=" + st["game_id"]})
            page.wait_js("!!document.querySelector('.v2-agebtn')", timeout=15,
                         desc="shell mounted at %d" % w)
            settle_decisions(page)
            page.wait_idle()
            shot(page, "b-home-%d" % w)
            for _ in range(3):
                page.js("document.querySelector('.v2-agebtn').click()")
                page.wait_idle()
                settle_decisions(page)
            page.wait_idle()
            shot(page, "b-aged-%d" % w)
            page.js("document.querySelector('.v2-tab[data-screen=people]')"
                    ".click()")
            page.wait_js("!document.getElementById('screen-people').hidden")
            page.wait_idle()
            shot(page, "b-people-%d" % w)
            errors = page.console_errors
            if errors:
                print("console errors at %d:" % w, errors)
                return 1
    finally:
        proc.terminate()
        proc.wait(timeout=5)
        if proc.stderr:
            proc.stderr.close()
        httpd.shutdown()
        httpd.server_close()
        shutil.rmtree(tmp, ignore_errors=True)
    return 0


if __name__ == "__main__":
    sys.exit(main())
