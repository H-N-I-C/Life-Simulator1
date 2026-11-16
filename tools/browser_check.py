"""Headless-Chromium browser check for the web UI (stdlib only).

One command: python3 tools/browser_check.py

What it does:
  1. Starts web.server on a free port with a throwaway save dir.
  2. For every page x (390x844, 1280x800): loads it, waits for load +
     network idle, records console errors, uncaught exceptions, failed
     requests and horizontal overflow, and saves a screenshot.
  3. Runs the data-driven flows in tests/browser/flows.py (creator
     keyboard/export checks and the M1 life flow).
  4. Writes docs/collab/evidence/browser/report.md + screenshots.

Exit code is non-zero if any check failed.
"""

import base64
import json
import os
import sys
import tempfile
import threading
import urllib.error
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import web.server as server  # noqa: E402
from tools.cdp_client import CHROMIUM, launch_chrome  # noqa: E402
from tests.browser.flows import FLOWS  # noqa: E402

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "browser")
PAGES = ["/creator", "/gallery", "/game", "/relationships"]
VIEWPORTS = [(390, 844), (1280, 800)]


class PendingSkip(Exception):
    """Raised by a hook when a flow's prerequisite has not landed yet
    (e.g. A's dev scenario factory). The flow is recorded as PENDING and
    its remaining steps are skipped — not a failure."""


class Runner:
    def __init__(self, base, session, out):
        self.base = base
        self.s = session
        self.out = out
        self.results = []   # (flow, name, ok, extra)
        self.ctx = {}       # flow context, exposed to JS as window.CTX

    def record(self, flow, name, ok, extra=""):
        self.results.append((flow, name, bool(ok), extra))
        print("%s %-56s %s" % ("PASS" if ok else "FAIL",
                               "[%s] %s" % (flow, name), extra))

    # ---- page-level checks -------------------------------------------------

    def check_page(self, path, width, height):
        tag = "%s_%dx%d" % (path.strip("/").replace("/", "-") or "index",
                            width, height)
        s = self.s
        s.console_errors.clear()
        s.exceptions.clear()
        s.failed_requests.clear()
        s._load_fired = False
        s._inflight.clear()
        s.call("Emulation.setDeviceMetricsOverride",
               {"width": width, "height": height, "deviceScaleFactor": 1,
                "mobile": width < 500})
        s.call("Page.navigate", {"url": self.base + path})
        try:
            s.wait_idle(timeout=15)
        except TimeoutError as exc:
            self.record(tag, "page load + network idle", False, str(exc))
        overflow = s.js("document.scrollingElement.scrollWidth > "
                        "window.innerWidth")
        self.record(tag, "no horizontal overflow", not overflow,
                    "scrollWidth=%s innerWidth=%s"
                    % (s.js("document.scrollingElement.scrollWidth"),
                       s.js("window.innerWidth")))
        self.record(tag, "no console errors", not s.console_errors,
                    json.dumps(s.console_errors)[:200])
        self.record(tag, "no uncaught exceptions", not s.exceptions,
                    json.dumps(s.exceptions)[:200])
        real_failures = [f for f in s.failed_requests
                         if f.get("error") != "net::ERR_ABORTED"
                         and not f.get("url", "").endswith("favicon.ico")]
        favicon_404 = any(f.get("url", "").endswith("favicon.ico")
                          for f in s.failed_requests)
        if favicon_404:
            self.record(tag, "favicon 404 (known issue, reported)", True,
                        "GET /favicon.ico -> 404")
        self.record(tag, "no failed requests", not real_failures,
                    json.dumps(real_failures)[:200])
        shot = s.call("Page.captureScreenshot", {"format": "png"})
        with open(os.path.join(self.out, tag + ".png"), "wb") as f:
            f.write(base64.b64decode(shot["data"]))

    # ---- flow engine -------------------------------------------------------

    def _push_ctx(self):
        self.s.js("window.CTX = %s; true" % json.dumps(self.ctx))

    def run_flow(self, flow):
        name = flow["name"]
        s = self.s
        s.console_errors.clear()
        s.exceptions.clear()
        s.failed_requests.clear()
        s._load_fired = False
        s._inflight.clear()
        s.call("Page.navigate", {"url": self.base + flow["start"]})
        try:
            s.wait_idle(timeout=15)
        except TimeoutError:
            pass  # long-polling or deferred loads; steps wait explicitly
        if flow.get("setup_js"):
            s.js(flow["setup_js"])
        try:
            for i, step in enumerate(flow["steps"]):
                try:
                    self._step(name, step)
                except Exception as exc:
                    raise RuntimeError("step %d (%s): %s"
                                       % (i, json.dumps(step)[:160], exc))
        except Exception as exc:
            self.record(name, "flow aborted", False, str(exc)[:900])
        for f in s.failed_requests:
            if (f.get("error") != "net::ERR_ABORTED"
                    and not f.get("url", "").endswith("favicon.ico")):
                self.record(name, "no failed requests during flow",
                            False, json.dumps(f)[:200])
        if s.console_errors:
            self.record(name, "no console errors during flow", False,
                        json.dumps(s.console_errors)[:200])
        if s.exceptions:
            self.record(name, "no uncaught exceptions during flow", False,
                        json.dumps(s.exceptions)[:200])

    def _step(self, flow, step):
        s = self.s
        self._push_ctx()
        if "python" in step:
            getattr(self, "_hook_" + step["python"])(flow)
        elif "wait_js" in step:
            try:
                s.wait_js(step["wait_js"], desc=step.get("desc"))
            except TimeoutError:
                diag = s.js("JSON.stringify({toast: (document.getElementById("
                            "'toast')||{}).textContent, actions: (document"
                            ".getElementById('actionList')||{}).innerText})")
                raise TimeoutError("%s | page state: %s"
                                   % (step.get("desc"), diag))
            self.record(flow, step.get("desc") or step["wait_js"][:60],
                        True)
        elif "check_js" in step:
            value = s.js(step["check_js"])
            ok = value is not None and value is not False
            self.record(flow, step.get("desc") or step["check_js"][:60],
                        ok, "" if ok else "check was falsy")
        elif "store_js" in step:
            self.ctx[step["as"]] = s.js(step["store_js"])
        elif "api" in step:
            spec = step["api"]
            url = self.base + spec["path"]
            data = None
            headers = {}
            if spec.get("body") is not None:
                data = json.dumps(spec["body"]).encode()
                headers["Content-Type"] = "application/json"
            req = urllib.request.Request(url, data=data, method=spec["method"],
                                         headers=headers)
            try:
                with urllib.request.urlopen(req) as resp:
                    body = json.loads(resp.read())
                    status = resp.status
            except urllib.error.HTTPError as exc:
                body = json.loads(exc.read() or b"{}")
                status = exc.code
            self.ctx[spec["as"]] = body
            want = spec.get("expect_status")
            ok = status == want if want else status < 400
            self.record(flow, "api %s %s" % (spec["method"], spec["path"]),
                        ok, "status=%s" % status)
        elif "key" in step:
            s.key(step["key"])
        elif "shot" in step:
            shot = s.call("Page.captureScreenshot", {"format": "png"})
            with open(os.path.join(self.out, step["shot"] + ".png"),
                      "wb") as f:
                f.write(base64.b64decode(shot["data"]))
        elif "js" in step:
            s.js(step["js"])

    # ---- python hooks (referenced from flows.py) ---------------------------

    def _hook_validate_export(self, flow):
        from src.appearance.model import Appearance
        data = json.loads(self.ctx["export_json"])
        model = Appearance.from_dict(data)
        exported_marks = set(data.get("marks", []))
        ok = set(model.marks) == exported_marks
        self.record(flow, "export validates via Appearance.from_dict", ok,
                    "model marks=%r exported=%r" % (list(model.marks),
                                                    list(exported_marks)))
        svg_ok = self.s.js("""(() => { const s = document.querySelector(
            '#preview svg'); return !!s; })()""")
        self.record(flow, "preview svg present at export time", svg_ok)

    def _hook_assert_state(self, flow):
        """Validate the frozen M2 state/ledger shapes on ctx['state']
        (captured via an {"api": {"path": "/api/games/<id>", "as":
        "state"}} step)."""
        from tools import m2_assertions
        state = self.ctx.get("state")
        if not state:
            raise RuntimeError("no ctx['state']; add an api GET step first")
        for fn, label in (
                (m2_assertions.check_state_sections, "state sections"),
                (m2_assertions.check_person_refs_resolve,
                 "person references")):
            ok, problems = fn(state)
            self.record(flow, "M2 %s match frozen shapes" % label, ok,
                        json.dumps(problems)[:250])

    def _hook_create_game(self, flow):
        import uuid
        profile = {
            "first_name": "QA",
            "last_name": "Testlife",
            "gender": "female",
            "country": "United States",
            "appearance": {
                "schema_version": 1, "seed": 7, "skin_tone": "skin_05",
                "face_shape": "round", "eye_shape": "almond",
                "eye_color": "green", "brows": "soft", "nose": "button",
                "mouth": "smile", "hair_texture": "wavy",
                "hair_color": "brown_medium", "hair_style": "bob",
                "facial_hair": "none", "marks": ["freckles"],
                "accessories": {"glasses": "none", "headwear": "none",
                                "earrings": "none"},
                "outfit_style": "hoodie", "outfit_color": "teal",
            },
            "request_id": str(uuid.uuid4()),
        }
        req = urllib.request.Request(
            self.base + "/api/games",
            data=json.dumps(profile).encode(),
            headers={"Content-Type": "application/json"})
        with urllib.request.urlopen(req) as resp:
            body = json.loads(resp.read())
        self.ctx["game_id"] = body["game_id"]
        self.record(flow, "life created via API", bool(body["game_id"]),
                    "game_id=%s" % body["game_id"][:8])

    def _hook_age_until_two(self, flow):
        s = self.s
        SETTLED = """(() => {
            const m = document.getElementById('modal');
            if (m && !m.hidden) return true;
            const btns = document.querySelectorAll('#actionList button');
            return btns.length === 0 ||
                   [...btns].some(b => !b.disabled);
        })()"""
        for _ in range(12):
            age = s.js("""(() => { const el = document.querySelector(
                '#playerCard .role');
                if (!el) return null;
                const m = el.textContent.match(/Age (\\d+)/);
                return m ? +m[1] : -1; })()""")
            if age is None:
                # page mid-render; wait for the summary card to come back
                s.wait_js("!!document.querySelector('#playerCard .role')",
                          desc="player summary card present")
                continue
            if age >= 2:
                self.record(flow, "reached age >= 2", True, "age=%s" % age)
                return
            modal_open = s.js(
                "(() => { const m = document.getElementById('modal'); "
                "return m && !m.hidden; })()")
            if modal_open:
                clicked = s.js("""(() => { const b = document.querySelector(
                    '#modalOptions button:not([disabled])');
                    if (b) { b.click(); return true; } return false; })()""")
                if not clicked:
                    self.record(flow, "decision has enabled options", False)
                    return
                s.wait_js(SETTLED, timeout=20, desc="decision settles")
                self.record(flow, "decision resolved", True)
                continue
            clicked = s.js("""(() => { const b = [...document
                .querySelectorAll('#actionList button')]
                .find(b => b.textContent.toLowerCase()
                    .includes('age')); if (b) { b.click(); return true; }
                return false; })()""")
            if not clicked:
                self.record(flow, "age-up action available", False)
                return
            s.wait_js(SETTLED, timeout=20, desc="year resolves")
        self.record(flow, "reached age >= 2", False, "gave up waiting")


def write_report(out, results):
    failures = [r for r in results if not r[2]]
    lines = ["# Browser check report", ""]
    lines.append("Checks: %d, failed: %d" % (len(results), len(failures)))
    lines.append("")
    lines.append("| Result | Check | Detail |")
    lines.append("|--------|-------|--------|")
    for flow, name, ok, extra in results:
        lines.append("| %s | [%s] %s | %s |"
                     % ("PASS" if ok else "FAIL", flow, name,
                        extra.replace("|", "\\|")))
    lines.append("")
    with open(os.path.join(out, "report.md"), "w") as f:
        f.write("\n".join(lines))


def main():
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-browser-check-")
    httpd = server.make_server(0, save_dir=save_dir)
    port = httpd.server_port
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    chrome = None
    try:
        chrome, conn = launch_chrome()
        s = conn.new_session()
        runner = Runner("http://127.0.0.1:%d" % port, s, EVIDENCE)
        for path in PAGES:
            for w, h in VIEWPORTS:
                runner.check_page(path, w, h)
        for flow in FLOWS:
            runner.run_flow(flow)
        write_report(EVIDENCE, runner.results)
        failed = [r for r in runner.results if not r[2]]
        print("SUMMARY: %d/%d passed; report at %s"
              % (len(runner.results) - len(failed), len(runner.results),
                 os.path.join(EVIDENCE, "report.md")))
        return 1 if failed else 0
    finally:
        if chrome:
            chrome.terminate()
            try:
                chrome.wait(timeout=5)
            except Exception:
                chrome.kill()
        httpd.shutdown()
        httpd.server_close()


if __name__ == "__main__":
    sys.exit(main())
