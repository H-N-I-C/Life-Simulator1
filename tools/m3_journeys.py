"""M3 journey runner (Kimi D, QA) — plays tests/browser/m3_journeys.py in a
real browser + real service at 390x844 and 320x844.

    python3 tools/m3_journeys.py

Honesty rules (per task file): a step is PENDING when its contract surface
does not exist yet (unknown action / missing projection), FAIL only when
the surface exists and behavior is wrong, SKIP when not applicable.
PENDING/SKIP never gate the exit code; FAIL does.

Evidence: docs/collab/evidence/M3/journeys.{md,json} + journey-*.png.
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
from tests.browser.m3_journeys import JOURNEYS  # noqa: E402

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "M3")
VIEWS = {"390": (390, 844), "320": (320, 844)}
# Set by main() so JourneyCtx test hooks can reach the in-process service.
RUNNER = {"httpd": None}


class JourneyCtx:
    def __init__(self, base, conn, evidence, journey_id):
        self.base = base
        self.conn = conn
        self.evidence = evidence
        self.journey_id = journey_id
        self.game_id = None
        self._req = 0

    # ---------------- service helpers --------------------------------
    def start(self, scenario, seed):
        self._starts = getattr(self, "_starts", 0) + 1
        profile = {
            "first_name": "Dev", "last_name": "Tester", "gender": "female",
            "country": "Australia", "seed": seed,
            "appearance": QA_APPEARANCE,
            "request_id": "m3j-%s-%d-%d-%d" % (self.journey_id, seed,
                                               os.getpid(), self._starts)}
        if scenario:
            profile["scenario"] = scenario
        req = urllib.request.Request(
            self.base + "/api/games", data=json.dumps(profile).encode(),
            headers={"Content-Type": "application/json"})
        self.game_id = json.loads(urllib.request.urlopen(req).read())[
            "game_id"]
        return self.game_id

    def state(self):
        with urllib.request.urlopen(
                self.base + "/api/games/" + self.game_id) as resp:
            return json.loads(resp.read())

    def api(self, action, payload):
        st = self.state()
        self._req += 1
        req = urllib.request.Request(
            self.base + "/api/games/" + self.game_id + "/actions",
            data=json.dumps({
                "expected_revision": st["revision"],
                "request_id": "m3j-%s-%d" % (self.journey_id, self._req),
                "action": action, "payload": payload}).encode(),
            headers={"Content-Type": "application/json"})
        try:
            return json.loads(urllib.request.urlopen(req).read())
        except urllib.error.HTTPError as e:
            return {"_status": e.code, "_body": e.read()[:400]}

    def resolve_pending_any(self):
        st = self.state()
        d = st.get("pending_decision")
        if not d:
            return True, "no pending"
        opts = [o for o in d.get("options", []) if not o.get("disabled")] \
            or d.get("options", [])
        if not opts:
            return False, "pending with no options"
        r = self.api("resolve_decision",
                     {"decision_id": d["id"], "option_id": opts[0]["id"],
                      "inputs": {}})
        if r.get("game_id"):
            return True, "resolved %s" % d.get("kind")
        return False, "resolve failed: %s" % r.get("_body")

    def strip_to_legacy_m2(self, salary=42000, employ=True, uv_years=0):
        """QA hook (journeys only): turn this game's save into a pre-M3
        save by removing the M3 envelope keys — the exact delta the
        migration contract (02-contracts.md, Migration sections) defines
        for old saves — and give the player legacy generic M2 state: a
        legacy job (has_job/salary, no named employer, no credentials)
        and/or a legacy university course in progress (uv_years). Never
        invents M3 records."""
        svc = RUNNER["httpd"].service
        env = svc._load(self.game_id)
        for key in ("careers", "education_state", "skills"):
            env.pop(key, None)
        p = env["player"]
        if employ:
            p.has_job = True
            p.salary = salary
            p.job_hours = 38
            if not getattr(p, "performance", None):
                p.performance = 60
        else:
            p.has_job = False
            p.salary = 0
        if uv_years:
            p.uv_years = uv_years
        svc._save(env)

    def qa_env(self, mutate):
        """QA fixture hook (journeys only): load the game's env, call
        mutate(env), save. For bounded, explicitly forced fixtures on rare
        engine branches (e.g. the legacy performance-firing dismissal).
        Never invents feature records."""
        svc = RUNNER["httpd"].service
        env = svc._load(self.game_id)
        mutate(env)
        svc._save(env)

    @staticmethod
    def player_age(st):
        return next(p["age"] for p in st["people"]
                    if p["id"] == st["player_id"])

    def ledger_legs(self, st, categories):
        """Per-leg ledger rows matching categories, or None when the
        projection carries no per-leg breakdown (contract pending)."""
        ledger = ((st.get("finances") or {}).get("ledger")) or []
        if not isinstance(ledger, list):
            return None
        return [leg for leg in ledger
                if str(leg.get("category", "")).lower() in categories]

    def pending_or_run(self, r, owner, want, on_ok):
        """PENDING when the contract surface does not exist; else on_ok()."""
        body = (r.get("_body") or b"")
        if isinstance(body, bytes):
            body = body.decode("utf-8", "replace")
        if r.get("_status") == 400 and "unknown action" in body:
            return "PENDING", "%s: %s" % (owner, want)
        if r.get("_status") == 404:
            return "PENDING", "%s: %s (endpoint not found)" % (owner, want)
        return on_ok()

    # ---------------- browser helpers ---------------------------------
    def open_page(self, width):
        wh = VIEWS[str(width)]
        s = self.conn.new_session()
        s.call("Emulation.setDeviceMetricsOverride",
               {"width": wh[0], "height": wh[1], "deviceScaleFactor": 1,
                "mobile": True})
        s.call("Page.navigate", {"url": self.base + "/game"})
        s.wait_js("document.readyState === 'complete'", timeout=20)
        s.js("sessionStorage.setItem('game_id', %s); true"
             % json.dumps(self.game_id))
        s.call("Page.navigate", {"url": self.base + "/game"})
        s.wait_js("!!document.querySelector('.v2-tabbar') && "
                  "document.getElementById('gameView').checkVisibility()",
                  timeout=20, desc="game page mounted")
        settle_ms = 1200
        s.js("new Promise(r => setTimeout(r, %d))" % settle_ms)
        return s

    @staticmethod
    def settle(s, ms):
        s.js("new Promise(r => setTimeout(r, %d))" % ms)

    def shot(self, s, name):
        data = s.call("Page.captureScreenshot", {"format": "png"})
        with open(os.path.join(self.evidence, name + ".png"), "wb") as f:
            f.write(base64.b64decode(data["data"]))

    def console_clean(self, s):
        s.console_errors.clear()
        s.exceptions.clear()
        s.failed_requests.clear()


def main():
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-m3j-", dir=TMP_HOME)
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
        seed = 3311
        for j in JOURNEYS:
            ctx = JourneyCtx(base, conn, EVIDENCE, j["id"])
            ctx.start(j.get("scenario"), seed)
            seed += 1
            print("== %s: %s (%s)" % (j["id"], j["title"], ctx.game_id[:9]))
            for sid, title, fn in j["steps"]:
                try:
                    status, detail = fn(ctx)
                except Exception as exc:  # harness/step crash: honest FAIL
                    status, detail = "FAIL", "step crashed: %r" % exc
                rows.append({"journey": j["id"], "step": sid, "title": title,
                             "status": status, "detail": detail})
                print("  %-7s %s — %s" % (status, sid, detail[:150]))
        counts = {}
        for r in rows:
            counts[r["status"]] = counts.get(r["status"], 0) + 1
        with open(os.path.join(EVIDENCE, "journeys.json"), "w") as f:
            json.dump({"counts": counts, "rows": rows}, f, indent=1)
        with open(os.path.join(EVIDENCE, "journeys.md"), "w") as f:
            f.write("# M3 browser journeys (Kimi D QA, %s)\n\n"
                    % time.strftime("%Y-%m-%d"))
            f.write("Harness: tools/m3_journeys.py + "
                    "tests/browser/m3_journeys.py — real service + headless "
                    "Chromium at 390x844 and 320x844. PENDING = contract "
                    "surface not frozen/implemented (owner named); FAIL = "
                    "surface exists but behavior wrong.\n\n")
            cur = None
            for r in rows:
                if r["journey"] != cur:
                    cur = r["journey"]
                    f.write("\n## %s\n" % cur)
                f.write("- **%s** `%s` — %s\n"
                        % (r["status"], r["step"],
                           (r["detail"] or r["title"])[:220]))
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
