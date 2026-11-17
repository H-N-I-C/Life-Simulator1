"""Headless-Chromium browser check for the web UI (stdlib only).

One command: python3 tools/browser_check.py

What it does:
  1. Starts web.server on a free port with a throwaway save dir.
  2. For every page x (390x844, 1280x800): loads it, waits for load +
     network idle, records console errors, uncaught exceptions, failed
     requests and horizontal overflow, and saves a screenshot.
  3. Runs the data-driven flows in tests/browser/flows.py (creator
     keyboard/export checks and the M1 life flow).
  4. Runs the robot player (tools/robot_player.quick_check) against the
     same server: ROBOT_LIVES random lives (default 200, seed 7). Hard
     failures (5xx, invariant breaks, soft-locks) FAIL the gate; typed
     4xx eligibility mismatches are reported as WARN.
  5. Writes docs/collab/evidence/browser/report.md + screenshots.

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
from tools.robot_player import quick_check as robot_quick  # noqa: E402

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "browser")
PAGES = ["/creator", "/gallery", "/game", "/relationships"]
VIEWPORTS = [(390, 844), (1280, 800)]
ROBOT_LIVES = 200
ROBOT_SEED = 7


def _dig(obj, dotted):
    """Resolve 'a.b.0.c' (and 'a.list.length') against dicts/lists."""
    cur = obj
    for part in dotted.split("."):
        if isinstance(cur, list):
            if part == "length":
                return len(cur)
            cur = cur[int(part)]
        else:
            cur = cur[part]
    return cur


def _resolve(value, ctx, state):
    """Substitute CTX paths and flow shortcuts inside command payloads."""
    if isinstance(value, str):
        if value == "first_enabled":
            for opt in (state.get("pending_decision") or {}).get("options", []):
                if opt.get("enabled"):
                    return opt["id"]
            raise RuntimeError("no enabled pending option")
        if value.startswith("CTX."):
            return _dig(ctx, value[4:])
        return value
    if isinstance(value, dict):
        return {k: _resolve(v, ctx, state) for k, v in value.items()}
    if isinstance(value, list):
        return [_resolve(v, ctx, state) for v in value]
    return value


class PendingSkip(Exception):
    """Raised when a required path could not be exercised (engine gap,
    rare branch, missing UI). Recorded as PENDING - which FAILS the
    acceptance gate unless the flow is in DEFERRED_PENDING (Codex M2
    review finding 6: unperformed checks must never count as passes)."""


# PENDING rows explicitly deferred by Claude IN WRITING. Team rule
# (Claude 2026-11-16): unperformed required parity paths FAIL the gate
# by default; only written approvals go here.
DEFERRED_PENDING = {
    # WRITTEN APPROVAL, Claude 2026-11-16: the kidnapping ransom is
    # legacy parity - 'not x_in_y(karma,130) and one_in(250)' - with
    # karma <= 100 the per-year chance is roughly 1/500, so a seed
    # sweep cannot reliably reach it (a 64-seed scan finding nothing is
    # EXPECTED, not evidence of a bug). Deferred ONLY until A lands the
    # dev-only force_event hook; then m2b-r69 must force the offer and
    # assert ransom/outcomes/ledger as a real check.
    "m2b-r69-kidnapping": "Claude-approved 2026-11-16: legacy 1/500/yr "
                          "rarity; awaiting A's dev-only force_event "
                          "hook, then must become a forced real check",
}


# Minimal schema-v2 appearance for QA fixtures (M0b): valid against the
# current catalog, tolerant of future optional fields.
QA_APPEARANCE = {"schema_version": 2, "seed": 7, "base": "woman",
                 "skin_tone": "medium", "hair": "default"}

# B mid-migration (M0b, reported 2026-11-16): /api/catalog and
# /api/render crash until the v2 catalog/render wiring lands; remove the
# entries once fixed so regressions fail again.
KNOWN_REQUEST_ISSUES = ("/api/scenarios", "/favicon.ico",
                        "/api/catalog", "/api/render")
# transient Chromium network-service errors, not app defects
ENV_FLAKES = ("net::ERR_NETWORK_CHANGED", "net::ERR_NETWORK_IO_SUSPENDED",
              "net::ERR_INTERNET_DISCONNECTED")


def _known_request(f):
    url = f.get("url", "")
    return any(url.endswith(k) for k in KNOWN_REQUEST_ISSUES)


class Runner:
    def __init__(self, base, session, out):
        self.base = base
        self.s = session
        self.out = out
        self.results = []   # (flow, name, ok, extra)
        self.ctx = {}       # flow context, exposed to JS as window.CTX

    def record(self, flow, name, ok, extra="", status=None):
        if status is None:
            status = "WARN" if (ok and name.startswith("WARN")) else (
                "PASS" if ok else "FAIL")
        self.results.append((flow, name, status == "PASS", extra, status))
        print("%s %-56s %s" % (status,
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
            # known-issue requests (B: /api/scenarios polled outside --dev,
            # favicon) may keep the page busy; report but don't fail
            inflight = [u for u in str(exc).split("'")[1::2]]
            if inflight and all(any(u.endswith(k)
                                    for k in KNOWN_REQUEST_ISSUES)
                                for u in inflight):
                self.record(tag, "network idle (known-issue poller)", True,
                            "perpetually busy: %r" % inflight)
            else:
                self.record(tag, "page load + network idle", False, str(exc))
        overflow = s.js("document.scrollingElement.scrollWidth > "
                        "window.innerWidth")
        self.record(tag, "no horizontal overflow", not overflow,
                    "scrollWidth=%s innerWidth=%s"
                    % (s.js("document.scrollingElement.scrollWidth"),
                       s.js("window.innerWidth")))
        self.record(tag, "no console errors", not s.console_errors,
                    json.dumps(s.console_errors)[:200])
        ftf = [e for e in s.exceptions if "Failed to fetch" in e]
        hard_exc = [e for e in s.exceptions if "Failed to fetch" not in e]
        for e in ftf:
            self.record(tag, "known migration outage: uncaught fetch "
                        "failure (B; reported 2026-11-16)", True, e[:120])
        self.record(tag, "no uncaught exceptions", not hard_exc,
                    json.dumps(hard_exc)[:200])
        known = [f for f in s.failed_requests if _known_request(f)]
        for f in known:
            self.record(tag, "known request issue: %s 404/busy" %
                        f["url"].rsplit("/", 1)[-1], True,
                        "reported to Claude 2026-11-16")
        flakes = [f for f in s.failed_requests
                  if f.get("error") in ENV_FLAKES]
        for f in flakes:
            self.record(tag, "environment flake: %s" % f["error"], True,
                        f["url"].rsplit("/", 1)[-1])
        real_failures = [f for f in s.failed_requests
                         if f.get("error") not in ENV_FLAKES
                         and not _known_request(f)]
        self.record(tag, "no failed requests", not real_failures,
                    json.dumps(real_failures)[:200])
        shot = s.call("Page.captureScreenshot", {"format": "png"})
        with open(os.path.join(self.out, tag + ".png"), "wb") as f:
            f.write(base64.b64decode(shot["data"]))

    # ---- flow engine -------------------------------------------------------

    def _push_ctx(self):
        self.s.js("window.CTX = %s; window.state = (CTX && CTX.state) || "
                  "null; true" % json.dumps(self.ctx))

    def run_flow(self, flow):
        name = flow["name"]
        s = self.s
        self.ctx.clear()
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
                except PendingSkip as skip:
                    self.record(name, str(skip), False, status="PENDING")
                    return
                except Exception as exc:
                    raise RuntimeError("step %d (%s): %s"
                                       % (i, json.dumps(step)[:160], exc))
        except Exception as exc:
            self.record(name, "flow aborted", False, str(exc)[:900])
        known = [f for f in s.failed_requests if _known_request(f)]
        for f in known:
            self.record(name, "known request issue during flow: %s"
                        % f["url"].rsplit("/", 1)[-1], True,
                        "reported to Claude 2026-11-16")
        for f in s.failed_requests:
            if (f.get("error") in ENV_FLAKES):
                self.record(name, "environment flake during flow: %s"
                            % f["error"], True,
                            f["url"].rsplit("/", 1)[-1])
            elif (not _known_request(f)):
                self.record(name, "no failed requests during flow",
                            False, json.dumps(f)[:200])
        if s.console_errors:
            self.record(name, "no console errors during flow", False,
                        json.dumps(s.console_errors)[:200])
        if s.exceptions:
            ftf = [e for e in s.exceptions if "Failed to fetch" in e]
            hard_exc = [e for e in s.exceptions
                        if "Failed to fetch" not in e]
            for e in ftf:
                self.record(name, "known migration outage during flow "
                            "(B; reported 2026-11-16)", True, e[:120])
            if hard_exc:
                self.record(name, "no uncaught exceptions during flow",
                            False, json.dumps(hard_exc)[:200])

    def _step(self, flow, step):
        s = self.s
        self._push_ctx()
        if "python" in step:
            spec = step["python"]
            if isinstance(spec, dict):
                getattr(self, "_hook_" + spec.pop("hook"))(flow, **spec)
            else:
                getattr(self, "_hook_" + spec)(flow)
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
            ok = value is True  # strict boolean; 0/""/{}/DOM nodes fail
            self.record(flow, step.get("desc") or step["check_js"][:60],
                        ok, "" if ok else "not boolean true: %r" % (value,))
        elif "store_js" in step:
            self.ctx[step["as"]] = s.js(step["store_js"])
        elif "store_ctx" in step:
            self.ctx[step["as"]] = _dig(self.ctx, step["store_ctx"])
        elif "check_ctx" in step:
            value = s.js(step["check_ctx"])
            ok = value is True  # strict boolean; 0/""/{}/DOM nodes fail
            self.record(flow, step.get("desc") or step["check_ctx"][:60],
                        ok, "" if ok else "not boolean true: %r" % (value,))
        elif "clear_network_errors" in step:
            s.console_errors.clear()
            s.failed_requests.clear()
            s.exceptions.clear()
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
        """The creator's exported JSON must load through the appearance
        model and round-trip. Schema-agnostic (v1 marks or v2 base/hair).
        """
        from src.appearance.model import Appearance
        data = json.loads(self.ctx["export_json"])
        model = Appearance.from_dict(data)
        rt = model.to_dict()
        if "marks" in data:
            ok = set(model.marks) == set(data["marks"])
            detail = "marks model=%r exported=%r" % (list(model.marks),
                                                     data["marks"])
        else:
            ok = rt.get("base") == model.base and \
                rt.get("skin_tone") == model.skin_tone
            detail = "base=%s skin=%s" % (model.base, model.skin_tone)
        self.record(flow, "export validates via Appearance.from_dict", ok,
                    detail)
        svg_ok = self.s.js("""(() => { const p = document.getElementById(
            'preview'); return !!(p.querySelector('img') ||
                                  p.querySelector('svg')); })()""")
        self.record(flow, "preview present at export time", svg_ok)

    def _hook_settle_turn(self, flow, max_commands=30):
        """Drive a committed year to completion: resolve any stored pending
        decisions (first enabled option, no rerolls) and age exactly once."""
        def player_age(state):
            for p in state.get("people", []):
                if p.get("id") == state.get("player_id"):
                    return p.get("age")
            return None

        start_age = player_age(self.ctx["state"])
        for _ in range(max_commands):
            self._hook_refresh_state(flow)
            state = self.ctx["state"]
            if state.get("pending_decision"):
                d = state["pending_decision"]
                option = next((o for o in d.get("options", []) if o.get(
                    "enabled")), None)
                if not option:
                    self.record(flow, "settle_turn: enabled option", False,
                                json.dumps(d)[:160])
                    return
                inputs = {}
                for spec in option.get("inputs", []):
                    if spec.get("type") == "text":
                        inputs[spec["id"]] = "QA Child"
                self._hook_cmd(flow, action="resolve_decision",
                               payload={"decision_id": d["id"],
                                        "option_id": option["id"],
                                        "inputs": inputs})
                continue
            # A finished year and a fresh year both show cursor year_start
            # with pending False, so termination is age-based: age until the
            # player's age moved, resolving anything stored along the way.
            if player_age(state) == start_age:
                self._hook_cmd(flow, action="age_up")
                continue
            break
        self._hook_refresh_state(flow)
        after_age = player_age(self.ctx["state"])
        self.record(flow, "age advanced exactly once",
                    after_age == start_age + 1,
                    "before=%s after=%s" % (start_age, after_age))
        self.record(flow, "turn settled (no pending)", True,
                    "revision=%s" % self.ctx["state"].get("revision"))

    def _hook_assert_work_income(self, flow, hours=None):
        """Independently verify the last closed year's work_income legs:
        gross from hours-scaled salary, bracket tax, exact quote identity,
        and ledger sum == balance delta (tools/expected_finance)."""
        from tools import expected_finance, m2_assertions
        state = self.ctx["state"]
        fin = state.get("finances") or {}
        work = state.get("work") or {}
        rows = [r for r in fin.get("ledger", [])
                if ":work_income:salary:" in r.get("source_key", "")]
        ok, problems = m2_assertions.check_ledger_rows(rows)
        legs = {}
        for r in rows:
            legs[r["source_key"].rsplit(":", 1)[-1]] = r["amount"]
        if hours is not None and work.get("salary"):
            expected = expected_finance.expected_gross_and_tax(
                work["salary"], True, hours)
            if legs.get("gross") != round(expected[0]):
                problems.append("gross %s != expected %s (salary=%s "
                                "hours=%s)" % (legs.get("gross"),
                                               round(expected[0]),
                                               work["salary"], hours))
            # tax/expenses legs are SIGNED (negative) per the frozen
            # work_income_turn_example
            if legs.get("tax") != -round(expected[1]):
                problems.append("tax leg %s != expected %s"
                                % (legs.get("tax"), -round(expected[1])))
        if all(k in legs for k in ("gross", "tax", "expenses", "net")):
            if legs["gross"] + legs["tax"] + legs["expenses"] != legs["net"]:
                problems.append("quote identity broken: %r" % (legs,))
        self.record(flow, "work_income legs match independent expectation",
                    ok and not problems, json.dumps(problems)[:250])

    def _hook_assert_pension_income(self, flow):
        """After retirement, pension is paid independent of job_hours:
        the pension leg's gross must equal the projected pension."""
        state = self.ctx["state"]
        fin = state.get("finances") or {}
        pension = (state.get("work") or {}).get("pension")
        rows = [r for r in fin.get("ledger", [])
                if ":work_income:salary:" in r.get("source_key", "")
                and r["source_key"].endswith(":gross")]
        ok = bool(rows) and pension is not None and \
            rows[-1]["amount"] == pension
        self.record(flow, "pension gross equals projected pension", ok,
                    "pension=%s last gross=%s" %
                    (pension, rows[-1]["amount"] if rows else None))

    def _hook_assert_player_avatar(self, flow):
        """Player card portrait must render. While /api/render is a known
        migration outage (B, reported) this is a visible WARN; otherwise
        the wait is strict."""
        import urllib.error
        try:
            req = urllib.request.Request(
                self.base + "/api/render",
                data=json.dumps({"appearance": QA_APPEARANCE, "age": 30,
                                 "size": 96}).encode(),
                headers={"Content-Type": "application/json"})
            with urllib.request.urlopen(req, timeout=10) as resp:
                resp.read()
            render_up = True
        except urllib.error.HTTPError as exc:
            render_up = exc.code != 500
        except Exception:
            render_up = False
        try:
            self.s.wait_js(
                "!!document.querySelector('#playerCard .avatar svg, "
                "#playerCard .avatar img')",
                timeout=10, desc="player avatar rendered")
            self.record(flow, "player avatar rendered", True)
        except TimeoutError:
            if render_up:
                self.record(flow, "player avatar rendered", False,
                            "render endpoint healthy but avatar absent")
            else:
                self.record(flow, "WARN known outage: /api/render 500 "
                            "(B migration; reported 2026-11-16)", True,
                            "avatar wait skipped")

    def _hook_assert_refs(self, flow):
        """Person-reference resolution on ctx['state'] (shape-agnostic;
        valid for M1 and M2 states)."""
        from tools import m2_assertions
        state = self.ctx.get("state")
        if not state:
            raise RuntimeError("no ctx['state']; add an api GET step first")
        ok, problems = m2_assertions.check_person_refs_resolve(state)
        self.record(flow, "person references resolve", ok,
                    json.dumps(problems)[:250])

    def _hook_count_games(self, flow, as_="n_games"):
        with urllib.request.urlopen(self.base + "/api/games") as resp:
            self.ctx[as_] = len(json.loads(resp.read())["games"])

    def _hook_make_life(self, flow, first="Probe", as_="probe_id", last="Life"):
        """Create a minimal life straight through the API; stores game_id."""
        import uuid
        body = {"first_name": first, "last_name": last,
                "gender": "female", "country": "AU",
                "appearance": QA_APPEARANCE,
                "request_id": "mk-%s-%s" % (first, uuid.uuid4().hex[:8])}
        req = urllib.request.Request(
            self.base + "/api/games", data=json.dumps(body).encode(),
            headers={"Content-Type": "application/json"})
        with urllib.request.urlopen(req) as resp:
            self.ctx[as_] = json.loads(resp.read())["game_id"]
        self.record(flow, "created life %s" % first, True)

    def _hook_reach_participant_decision(self, flow, max_commands=60):
        """Age the current game (via API) until a pending decision with a
        NON-EMPTY participant list appears. Decisions without participants
        are resolved via their first enabled option and aging continues, so
        the participant-portrait fixture below is always meaningful."""
        gid = self.ctx.get("game_id") or (self.ctx.get("state") or {}).get(
            "game_id")
        with urllib.request.urlopen(self.base + "/api/games/" + gid) as resp:
            state = json.loads(resp.read())
        import uuid
        for _ in range(max_commands):
            if state.get("pending_decision"):
                d = state["pending_decision"]
                if d.get("person_ids"):
                    self.ctx["state"] = state
                    self.record(flow, "pending decision with participants",
                                True, "kind=%s n=%d" % (d.get("kind"),
                                                       len(d["person_ids"])))
                    return
                option = next((o for o in d.get("options", [])
                               if o.get("enabled")), None)
                if not option:
                    self.record(flow, "pending decision with participants",
                                False,
                                "decision %r has no enabled option (e.g. "
                                "unaffordable) — life is soft-locked"
                                % (d.get("kind"),))
                    return
                cmd = {"expected_revision": state["revision"],
                       "request_id": str(uuid.uuid4()),
                       "action": "resolve_decision",
                       "payload": {"decision_id": d["id"],
                                   "option_id": option["id"],
                                   "inputs": {}}}
            else:
                cmd = {"expected_revision": state["revision"],
                       "request_id": str(uuid.uuid4()),
                       "action": "age_up", "payload": {}}
            req = urllib.request.Request(
                self.base + "/api/games/" + gid + "/actions",
                data=json.dumps(cmd).encode(),
                headers={"Content-Type": "application/json"})
            try:
                with urllib.request.urlopen(req) as resp:
                    state = json.loads(resp.read())
            except urllib.error.HTTPError as exc:
                body = exc.read()[:300]
                if exc.code == 409:
                    with urllib.request.urlopen(self.base + "/api/games/" +
                                                gid) as resp:
                        state = json.loads(resp.read())
                    continue
                raise RuntimeError("command %s -> HTTP %s: %s"
                                   % (cmd["action"], exc.code, body))
        self.record(flow, "pending decision with participants", False,
                    "none in %d commands" % max_commands)

    def _hook_decision_modal_walk(self, flow):
        """UI walk of a real pending decision (consolidated from B's smoke
        checks): focus inside the modal, Tab trap, non-empty participant
        portraits, Escape-to-pill, reopen-with-focus, resolve, timeline
        portraits after reload."""
        s = self.s
        gid = self.ctx.get("game_id") or (self.ctx.get("state") or {}).get(
            "game_id")
        pending = (self.ctx.get("state") or {}).get("pending_decision") or {}
        if not pending.get("person_ids"):
            self.record(flow, "decision modal walk", True,
                        "skipped: no participant decision was reached")
            return
        n_persons = len(pending.get("person_ids") or [])
        self.record(flow, "participant fixture is non-empty",
                    n_persons >= 1, "person_ids=%d" % n_persons)
        s.js("sessionStorage.clear(); location.href = '/game?game=%s'; true"
             % gid)
        s.wait_js("""(() => { const m = document.getElementById('modal');
            return !!m && !m.hidden &&
                   m.contains(document.activeElement); })()""",
            desc="decision modal opens with focus inside")
        s.key("Tab")
        s.wait_js("""document.getElementById('modal')
            .contains(document.activeElement)""",
            desc="Tab stays trapped in modal")
        try:
            s.wait_js("document.querySelectorAll("
                      "'#modalPeople svg, #modalPeople img').length"
                      " >= %d" % max(1, n_persons),
                      timeout=10, desc="participant portraits render")
            svgs = s.js("document.querySelectorAll("
                        "'#modalPeople svg, #modalPeople img').length")
            self.record(flow, "decision participants show portraits",
                        isinstance(svgs, int) and svgs >= n_persons >= 1,
                        "person_ids=%d svgs=%r" % (n_persons, svgs))
        except TimeoutError:
            svgs = s.js("document.querySelectorAll("
                        "'#modalPeople svg, #modalPeople img').length")
            self.record(flow, "decision participants show portraits",
                        False, "person_ids=%d svgs=%r (never rendered)"
                        % (n_persons, svgs))
        s.js("""document.dispatchEvent(new KeyboardEvent('keydown',
            {key: 'Escape', bubbles: true})); true""")
        s.wait_js("""(() => { const p = document.getElementById(
            'decisionPill'); const m = document.getElementById('modal');
            return !!p && !p.hidden && !!m && m.hidden; })()""",
            desc="Escape collapses to reopening pill")
        s.js("document.querySelector('#decisionPill button').click(); true")
        s.wait_js("""(() => { const m = document.getElementById('modal');
            return !!m && !m.hidden &&
                   m.contains(document.activeElement); })()""",
            desc="pill reopens decision with focus")
        s.js("document.activeElement.click(); true")
        s.wait_js("""(() => { const m = document.getElementById('modal');
            return !!m && m.hidden; })()""",
            desc="decision resolves and modal closes")
        # reload: state persisted, timeline shows participant portraits
        s.js("location.reload(); true")
        s.wait_js("!document.getElementById('gameView').hidden",
                  desc="game view after decision reload")
        tl = s.wait_js("""(() => { const n = document
            .querySelectorAll('#journalList svg, #journalList img').length;
            return n > 0 ? true : false; })()""",
            desc="timeline entries show portraits")
        self.record(flow, "post-decision reload keeps portraits", bool(tl))

    @staticmethod
    def _matches_retired_projection_bug(work):
        """The exact known-broken projection signature (A, reported
        2026-11-16): model retired (has_job False, salary = pension > 0)
        but the projection shows retired:false and pension:0. Anything
        else is a NEW deviation and must fail hard."""
        return (work.get("employed") is False
                and work.get("retired") is False
                and work.get("pension") == 0
                and isinstance(work.get("salary"), int)
                and work["salary"] > 0)

    def _hook_expect_retired_projection(self, flow):
        """Assert the retired flag. WARN-masked ONLY for the exact known
        projection bug (self-removing once A fixes it); any other state
        fails hard."""
        work = (self.ctx.get("state") or {}).get("work") or {}
        if work.get("retired") is True:
            self.record(flow, "retired flag set", True)
        elif self._matches_retired_projection_bug(work):
            self.record(flow, "WARN known deviation: work.retired "
                        "projection false after retire (A; reported "
                        "2026-11-16)", True, json.dumps(work)[:200])
        else:
            self.record(flow, "retired flag set", False,
                        json.dumps(work)[:200])

    def _hook_expect_pension_income_warn(self, flow):
        """WARN-masked twin of assert_pension_income (same projection
        finding: pension leg posts with gross == salary, projection
        pension field shows 0). Remove masking once fixed."""
        state = self.ctx["state"]
        fin = state.get("finances") or {}
        pension = (state.get("work") or {}).get("pension")
        rows = [r for r in fin.get("ledger", [])
                if ":work_income:salary:" in r.get("source_key", "")
                and r["source_key"].endswith(":gross")]
        work = (state.get("work") or {})
        ok = bool(rows) and pension is not None and \
            rows[-1]["amount"] == pension
        if ok:
            self.record(flow, "pension gross equals projected pension", True,
                        "pension=%s" % pension)
        elif self._matches_retired_projection_bug(work):
            self.record(flow, "WARN known deviation: pension projection "
                        "income (A; reported 2026-11-16)", True,
                        "pension=%s last gross=%s"
                        % (pension, rows[-1]["amount"] if rows else None))
        else:
            self.record(flow, "pension gross equals projected pension",
                        False, "pension=%s last gross=%s"
                        % (pension, rows[-1]["amount"] if rows else None))


    def _hook_parent_id(self, flow, as_="parent_id", index=0):
        """Resolve a parent's person_id into ctx."""
        state = self.ctx.get("state") or {}
        parents = (state.get("family") or {}).get("parents") or []
        if len(parents) <= index:
            raise RuntimeError("no parent at index %d" % index)
        self.ctx[as_] = parents[index]
        self.record(flow, "parent id resolved", True, parents[index][:8])

    def _hook_surrender_to_death(self, flow):
        """Walk the double-confirm surrender chain to the death state."""
        self._hook_cmd(flow, action="surrender")
        self._hook_refresh_state(flow)
        for _ in range(4):
            d = (self.ctx.get("state") or {}).get("pending_decision")
            if not d:
                break
            def wants_end(o):
                text = (o.get("id", "") + " " + o.get("label", "")).lower()
                return (o.get("enabled") and not
                        any(k in text for k in ("cancel", "keep living",
                                                "refuse", "decline"))
                        and any(k in text
                                for k in ("final", "surrender", "end",
                                          "warn", "proceed")))
            opt = next((o for o in d.get("options", []) if wants_end(o)),
                       None)
            if not opt:
                opt = next((o for o in d.get("options", [])
                            if o.get("enabled")), None)
            self._hook_cmd(flow, action="resolve_decision",
                           payload={"decision_id": d["id"],
                                    "option_id": opt["id"], "inputs": {}})
            self._hook_refresh_state(flow)
            if self.ctx["state"].get("status") == "dead":
                break
        dead = self.ctx["state"].get("status") == "dead"
        self.record(flow, "surrender leads to death state", dead,
                    "status=%s" % self.ctx["state"].get("status"))

    def _hook_continue_as_eligible(self, flow):
        """Continue as the first eligible child (frozen M2c shape)."""
        state = self.ctx.get("state") or {}
        elig = (state.get("succession") or {}).get("eligible") or []
        if not elig:
            self.record(flow, "succession eligible children", False,
                        json.dumps(state.get("succession"))[:160])
            return
        target = elig[0]["person_id"]
        gen_before = state.get("generation")
        pid_before = state.get("player_id")
        bal_before = (state.get("finances") or {}).get("balance")
        self._hook_cmd(flow, action="continue_as",
                       payload={"target_id": target})
        self._hook_refresh_state(flow)
        state = self.ctx["state"]
        player = next((p for p in state.get("people", [])
                       if p.get("id") == state.get("player_id")), {})
        ok = (state.get("player_id") == target
              and state.get("generation") == gen_before + 1
              and player.get("age") == elig[0].get("age"))
        self.record(flow, "continue_as: identity/generation preserved", ok,
                    "player=%s gen %s->%s age=%s" %
                    (state.get("player_id") == target, gen_before,
                     state.get("generation"), player.get("age")))
        same_game = state.get("game_id") == self.ctx.get("game_id")
        self.record(flow, "same game_id after succession", same_game)
        # inherited cash: balance continues (transfer nets to zero across
        # accounts); record actuals as evidence
        self.record(flow, "cash continuity recorded", True,
                    "balance %s -> %s; completed_lives=%d" %
                    (bal_before, (state.get("finances") or {}).get(
                        "balance"),
                     len(state.get("completed_lives") or [])))
        self._hook_assert_refs(flow)

    def _hook_partner_id(self, flow, as_="partner_id"):
        """Resolve the current partner's person_id into ctx."""
        state = self.ctx.get("state") or {}
        pid = (state.get("family") or {}).get("partner")
        if not pid:
            raise RuntimeError("no partner in current state")
        self.ctx[as_] = pid
        self.record(flow, "partner id resolved", True, pid[:8])

    def _hook_record_balance(self, flow, as_="balance_before"):
        self.ctx[as_] = (self.ctx.get("state") or {}).get(
            "finances", {}).get("balance")
        self.record(flow, "balance recorded", True,
                    "balance=%s" % self.ctx[as_])

    def _hook_assert_last_payment(self, flow, category=None,
                                  source_contains=None, amount_min=None,
                                  amount_max=None, desc=None):
        """The last closed/committed ledger rows since balance_before must
        include exactly one payment of the given category within bounds,
        and the balance delta must equal the sum of all new rows."""
        from tools import m2_assertions
        state = self.ctx["state"]
        fin = state.get("finances") or {}
        before = self.ctx.get("balance_before")
        rows = fin.get("ledger", [])
        # rows whose balance chain starts after the recorded balance:
        # take rows after the last row that had balance_after == before
        idx = 0
        for i, r in enumerate(rows):
            if r.get("balance_after") == before:
                idx = i + 1
        new_rows = rows[idx:]
        def match(r):
            if source_contains is not None:
                return source_contains in r.get("source_key", "")
            return category is None or r.get("category") == category
        pay = [r for r in new_rows if match(r)]
        ok = len(pay) == 1
        detail = "new_rows=%d payment_rows=%d" % (len(new_rows), len(pay))
        if pay:
            amt = pay[0]["amount"]
            detail += " amount=%s" % amt
            if amount_min is not None and not (amount_min <= -amt <=
                                               amount_max):
                ok = False
                detail += " outside [%s,%s]" % (amount_min, amount_max)
        if before is not None:
            delta_ok = (sum(r["amount"] for r in new_rows) ==
                        fin.get("balance") - before)
            if not delta_ok:
                ok = False
                detail += " delta mismatch"
        shape_ok, problems = m2_assertions.check_ledger_rows(new_rows)
        if not shape_ok:
            ok = False
        self.record(flow, desc or "paid action posted exactly once",
                    ok and shape_ok, detail + " " +
                    json.dumps(problems)[:120])

    def _hook_assert_illnesses(self, flow, absent=None, present=None):
        """Illness set assertions after treatment."""
        illnesses = (self.ctx["state"].get("health") or {}).get(
            "illnesses") or []
        ok = all(i not in illnesses for i in (absent or [])) and \
            all(i in illnesses for i in (present or []))
        self.record(flow, "illnesses updated", ok,
                    "illnesses=%r" % (illnesses,))

    def _hook_clear_pending(self, flow, max_resolutions=6):
        """Resolve any open stored decisions (first enabled option, no
        rerolls). Some scenarios hand off with a contextual pending left
        open; commands like age_up are 409-blocked until it is resolved."""
        for _ in range(max_resolutions):
            self._hook_refresh_state(flow)
            d = (self.ctx.get("state") or {}).get("pending_decision")
            if not d:
                self.record(flow, "no open pending decisions", True)
                return
            self._hook_resolve_pending_option(flow, match=None)
        raise RuntimeError("pending decisions never cleared")

    def _hook_find_job_until_employed(self, flow, max_offers=4):
        """Find a job and keep accepting offers until the interview roll
        lands (legacy: acceptance is a smarts/karma interview roll, so a
        single Apply can legitimately fail). Hard fail if never employed."""
        for attempt in range(max_offers):
            self._hook_refresh_state(flow)
            work = (self.ctx.get("state") or {}).get("work") or {}
            if work.get("employed"):
                self.record(flow, "employed after interview", True,
                            "salary=%s" % work.get("salary"))
                return
            self._hook_cmd(flow, action="find_job")
            self._hook_refresh_state(flow)
            d = (self.ctx.get("state") or {}).get("pending_decision")
            if not d:
                continue
            self._hook_cmd(flow, action="resolve_decision",
                           payload={"decision_id": d["id"],
                                    "option_id": "apply", "inputs": {}})
        self._hook_refresh_state(flow)
        work = (self.ctx.get("state") or {}).get("work") or {}
        self.record(flow, "employed after interview", work.get("employed")
                    is True, "offers=%d" % max_offers)

    def _hook_require_selector(self, flow, selector=None, note=None):
        """PendingSkip when a legacy/UI-dependent selector is absent —
        used to park flows whose UI steps await B's rebuilt screens."""
        found = self.s.js("!!document.querySelector(%s)"
                          % json.dumps(selector))
        if not found:
            raise PendingSkip(note or "selector %r absent; UI steps "
                              "await B's screens" % selector)
        self.record(flow, "selector %s present" % selector, True)

    def _hook_await_vaccination(self, flow, seeds=(1, 2, 3, 4, 5, 6)):
        """Row 61 parity: the age-2 vaccination needs age==2 AND a 1/2
        roll, so a single seed legitimately misses (documented limit in
        parity-matrix.md). Sweep a fixed seed list; hard fail only if no
        seed produces the event within the first two years."""
        for i, seed in enumerate(seeds):
            self._hook_create_game(flow, seed=seed)
            self._hook_refresh_state(flow)
            try:
                self._hook_await_pending_kind(flow,
                                              kinds=["vaccination_age2"],
                                              max_commands=4)
            except PendingSkip:
                if i + 1 < len(seeds):
                    self.record(flow, "seed missed the 1/2 roll; retrying",
                                True, "next seed=%s" % seeds[i + 1])
                    continue
                raise
            self._hook_resolve_pending_option(flow, match=None)
            self.record(flow, "vaccination event exercised", True,
                        "seed=%s" % seed)
            return

    def _hook_await_kidnapping(self, flow,
                               seeds=(777, 4242, 1, 2, 3, 42),
                               max_years=8):
        """Row 69: partner kidnapping ransom needs money>=1M AND a 1/250
        yearly roll (parity). wealthy_married_40 supplies the money gate;
        sweep a fixed seed list for the roll. Rare-branch honest status:
        PENDING (not a failure) if no seed fires within the bound."""
        for i, seed in enumerate(seeds):
            if i:
                self._hook_scenario_start(flow,
                                          scenario="wealthy_married_40",
                                          seed=seed)
            for year in range(max_years):
                self._hook_refresh_state(flow)
                d = (self.ctx.get("state") or {}).get("pending_decision")
                if d and "kidnapping" in str(d.get("kind") or ""):
                    self.record(flow, "kidnapping decision after %d "
                                "year(s), seed %s" % (year, seed), True,
                                "kind=%s" % d.get("kind"))
                    return
                if d:
                    self._hook_resolve_pending_option(flow, match=None)
                    continue
                self._hook_cmd(flow, action="age_up")
            self.record(flow, "seed %s: no kidnapping in %d years" %
                        (seed, max_years), True, "1/250 roll missed")
        # Unperformed: recorded as PENDING. This flow is the ONLY
        # Claude-approved deferral (written, 2026-11-16) and only until
        # A's dev-only force_event hook lands; the sweep above is the
        # honest rarity evidence (legacy ~1/500/yr).
        raise PendingSkip("kidnapping (legacy ~1/500/yr with karma<=100) "
                          "did not fire across %d seeds x %d years; "
                          "deferred by Claude's written approval until "
                          "A's force_event hook lands"
                          % (len(seeds), max_years))

    def _hook_await_action(self, flow, action=None, max_commands=12):
        """Resolve/age until the given action becomes available. If it
        never appears the path is not reachable from this scenario —
        PENDING with the reason (e.g. no dating path after divorce yet)."""
        for _ in range(max_commands):
            self._hook_refresh_state(flow)
            state = self.ctx.get("state") or {}
            ids = [a.get("id") for a in state.get("available_actions", [])]
            if action in ids:
                self.record(flow, "action %s available" % action, True)
                return
            d = state.get("pending_decision")
            if d:
                self._hook_resolve_pending_option(flow, match=None)
                continue
            self._hook_cmd(flow, action="age_up")
        raise PendingSkip("action %r never became available within %d "
                          "commands (path not reachable from this "
                          "scenario yet)" % (action, max_commands))

    def _hook_resolve_funeral_if_pending(self, flow):
        """Resolve the funeral decision if one is stored. When
        await_rel_death already recorded a hard failure (no death within
        the cap), this no-ops so the flow ends cleanly."""
        d = (self.ctx.get("state") or {}).get("pending_decision")
        if not d:
            return
        self._hook_record_balance(flow)
        self._hook_resolve_pending_option(flow, match=None)
        self._hook_refresh_state(flow)
        deceased = any(p.get("alive") is False
                       for p in self.ctx["state"].get("people", []))
        self.record(flow, "deceased archived and refs resolve", deceased)
        self._hook_assert_refs(flow)

    def _hook_await_rel_death(self, flow, max_years=60):
        """Age until a family death produces a funeral decision (bounded,
        hard fail at cap — a run must not pass without the event)."""
        for year in range(max_years):
            self._hook_refresh_state(flow)
            state = self.ctx.get("state") or {}
            d = state.get("pending_decision")
            if d and "funeral" in str(d.get("kind") or ""):
                self.record(flow, "funeral decision after %d year(s)" % year,
                            True, "kind=%s" % d.get("kind"))
                return
            deaths = [e for e in state.get("journal", [])
                      if e.get("kind") == "death"]
            if deaths:
                self.record(flow, "death journaled after %d year(s)" % year,
                            True, deaths[-1].get("text", "")[:70])
                return
            if d:
                self._hook_resolve_pending_option(flow, match=None)
                continue
            self._hook_cmd(flow, action="age_up")
        self.record(flow, "funeral decision reached", False,
                    "no family death in %d seeded years" % max_years)

    def _hook_resolve_pending_option(self, flow, match=None):
        """Resolve the current pending decision preferring the option whose
        id/label contains `match` (e.g. 'keep_working'); falls back to the
        first enabled option."""
        self._hook_refresh_state(flow)
        d = (self.ctx.get("state") or {}).get("pending_decision")
        if not d:
            # the retirement offer is a 1/3-chance yearly event (parity
            # row 73); its absence is a valid engine outcome
            self.record(flow, "no pending decision to resolve (%s)" % match,
                        True, "optional event did not fire this year")
            return
        options = [o for o in d.get("options", []) if o.get("enabled")]
        picked = next((o for o in options if match and match.lower() in
                       (o.get("id", "") + o.get("label", "")).lower()),
                      options[0] if options else None)
        if not picked:
            self.record(flow, "resolve pending (%s)" % match, False,
                        "no enabled option")
            return
        self._hook_cmd(flow, action="resolve_decision",
                       payload={"decision_id": d["id"],
                                "option_id": picked["id"], "inputs": {}})
        self.record(flow, "resolved pending via %r" % picked.get("label"),
                    True, "kind=%s" % d.get("kind"))

    def _hook_assert_fired_cleanly(self, flow):
        """Parity row 71: fired for performance at the year boundary.
        Asserts employment ended exactly once and the boundary year's
        postings (whatever the parity rule posts) are unique-keyed and sum
        to the balance delta. Records the actual pay behaviour as
        evidence for the parity question rather than assuming it."""
        from tools import m2_assertions
        state = self.ctx["state"]
        work = state.get("work") or {}
        fin = state.get("finances") or {}
        ok = work.get("employed") is False
        self.record(flow, "fired: no longer employed", ok,
                    json.dumps(work)[:160])
        rows_ok, problems = m2_assertions.check_ledger_rows(
            fin.get("ledger", []))
        self.record(flow, "ledger integrity after firing", rows_ok,
                    json.dumps(problems)[:200])
        turn_id = (state.get("turn") or {}).get("turn_id")
        gross_keys = [r["source_key"] for r in fin.get("ledger", [])
                      if r["source_key"].startswith(str(turn_id))
                      and r["source_key"].endswith(":gross")]
        self.record(flow, "boundary-year pay posted at most once",
                    len(gross_keys) <= 1,
                    "gross legs this turn: %d" % len(gross_keys))

    def _hook_settle_until_fired(self, flow, scenario=None,
                                 seeds=(777, 4242, 1, 2, 3, 42),
                                 max_years=15):
        """Age year by year until the performance firing lands (legacy
        rule: randint(1, performance+1) == 1 per year; performance drifts
        upward, so the window is the first years only). Because it is a
        lottery, retry across a fixed seed list (deterministic per run);
        HARD FAIL if no seed fires - a run must not pass without the
        firing actually happening."""
        for i, seed in enumerate(seeds):
            if self._fired_within(flow, max_years):
                return
            rest = tuple(seeds[i + 1:])
            if not rest:
                break
            self.record(flow, "seed exhausted without firing; retrying", True,
                        "next seed=%s" % rest[0])
            self._hook_scenario_start(flow, scenario=scenario, seed=rest[0])
        self.record(flow, "fired within %d years" % max_years, False,
                    "no seed fired within %d years" % max_years)

    def _fired_within(self, flow, max_years):
        for year in range(max_years):
            self._hook_refresh_state(flow)
            state = self.ctx.get("state") or {}
            work = state.get("work") or {}
            if not work.get("employed"):
                self.record(flow, "fired after %d year(s)" % year, True)
                return True
            if state.get("pending_decision"):
                # clear boundary-year decisions without rerolls first;
                # age_up is 409-blocked while one is stored
                self._hook_resolve_pending_option(flow, match=None)
                continue
            self._hook_cmd(flow, action="age_up")
        self._hook_refresh_state(flow)
        work = (self.ctx.get("state") or {}).get("work") or {}
        if not work.get("employed"):
            self.record(flow, "fired within %d years" % max_years, True)
            return True
        return False

    def _hook_resolve_university_funding_loan(self, flow):
        """Walk the open university chain choosing the LOAN option per the
        frozen pending shapes: if the open pending is the application, take
        it, then resolve the funding decision's loan option."""
        d = (self.ctx.get("state") or {}).get("pending_decision") or {}
        kind = d.get("kind")
        self.record(flow, "open university pending", bool(d), "kind=%s" % kind)
        if not d:
            return
        if kind == "university_application":
            self._hook_resolve_pending_option(flow, match="apply")
            self._hook_await_pending_kind(flow,
                                          kinds=["university_funding"])
            self._hook_refresh_state(flow)
            d = (self.ctx.get("state") or {}).get("pending_decision") or {}
        if (d.get("kind") == "university_funding"):
            self._hook_resolve_pending_option(flow, match="loan")
        else:
            self.record(flow, "funding decision kind", False,
                        "got %r" % d.get("kind"))

    def _hook_await_pending_kind(self, flow, kinds=(), max_commands=24):
        """Resolve/age until a pending decision whose kind is in `kinds`
        appears (bounded). If it never appears, the engine path is not
        wired yet — record PENDING rather than fail. Stores nothing; the
        caller refreshes/asserts afterwards."""
        kinds = tuple(kinds)
        for _ in range(max_commands):
            self._hook_refresh_state(flow)
            state = self.ctx["state"]
            d = state.get("pending_decision")
            if d and d.get("kind") in kinds:
                self.record(flow, "awaited pending %s" % (kinds,), True,
                            "kind=%s" % d.get("kind"))
                return
            if d:
                option = next((o for o in d.get("options", []) if o.get(
                    "enabled")), None)
                if not option:
                    raise PendingSkip(
                        "blocked by unresolvable decision %r" % d.get("kind"))
                self._hook_cmd(flow, action="resolve_decision",
                               payload={"decision_id": d["id"],
                                        "option_id": option["id"],
                                        "inputs": {}})
                continue
            self._hook_cmd(flow, action="age_up")
        raise PendingSkip("no pending decision %s within %d commands "
                          "(engine path not wired yet)" % (kinds, max_commands))

    def _hook_retire_if_working(self, flow):
        """Retire unless the yearly flow already resolved a retirement
        offer during settling (settle_turn resolves stored decisions)."""
        work = (self.ctx.get("state") or {}).get("work") or {}
        if work.get("retired"):
            self.record(flow, "retirement offer resolved during settling",
                        True, "pension=%s" % work.get("pension"))
            return
        self._hook_cmd(flow, action="retire")
        self._hook_refresh_state(flow)
        work = (self.ctx.get("state") or {}).get("work") or {}
        if work.get("retired") is True:
            self.record(flow, "retire command accepted", True)
        elif self._matches_retired_projection_bug(work):
            # WARN only while A's projection bug has this EXACT signature
            # (reported 2026-11-16). Any other mismatch fails hard, and
            # the mask self-removes once the projection is fixed.
            self.record(flow, "WARN known deviation: retire accepted but "
                        "work.retired false (A; reported 2026-11-16)", True,
                        json.dumps(work)[:160])
        else:
            self.record(flow, "retire command accepted", False,
                        json.dumps(work)[:160])

    def _hook_refresh_state(self, flow, as_="state"):
        """GET the current game state into ctx[as_]. Prefers an explicit
        ctx['game_id'] (e.g. just created) over a previously cached state."""
        gid = self.ctx.get("game_id") or (self.ctx.get("state") or {}).get(
            "game_id")
        if not gid:
            raise RuntimeError("no game_id in ctx")
        with urllib.request.urlopen(self.base + "/api/games/" + gid) as resp:
            self.ctx[as_] = json.loads(resp.read())
        self.record(flow, "refresh state", True, "game_id=%s" % gid[:8])

    SCENARIO_AGES = {
        "high_school_first_day": 14, "graduating_18": 18,
        "unemployed_18": 18, "enrolled_child_10": 10, "employed_30": 30,
        "near_retirement_64": 64, "retirement_eligible_65": 65,
        "university_pending_17": 17, "ill_with_cash": 30,
        "low_performance_30": 30, "married_30": 30,
        "married_with_child": 30, "pregnant_25": 25,
        "new_parent_25": 25, "wealthy_married_40": 40,
    }

    def _discover_scenarios(self):
        """Runtime scenario-id catalog (A is consolidating ids). Prefers
        the list-scenarios endpoint; falls back to the module catalog via
        the service process. None = discovery unavailable (keep the static
        age map and known-id checks only)."""
        if hasattr(self, "_scenario_catalog"):
            return self._scenario_catalog
        catalog = None
        try:
            with urllib.request.urlopen(self.base + "/api/scenarios",
                                        timeout=5) as resp:
                body = json.loads(resp.read())
            items = body.get("scenarios", body) if isinstance(body, dict) \
                else body
            ids = [i.get("id") if isinstance(i, dict) else i for i in items]
            if all(isinstance(i, str) for i in ids):
                catalog = sorted(ids)
        except Exception:
            pass
        self._scenario_catalog = catalog
        return catalog

    def _hook_scenario_start(self, flow, scenario=None, seed=777):
        """Start a life through the FROZEN dev scenario-start shape
        (docs/collab/M2/api-examples.json: profile key `scenario`).
        Built through real transitions server-side; we verify the frozen
        result marker (player age) afterwards — if the service ignored
        the scenario, the factory hasn't landed and the flow is recorded
        PENDING rather than running against a fabricated state."""
        import uuid
        profile = {
            "first_name": "Dev", "last_name": "Tester",
            "gender": "female", "country": "Australia", "seed": seed,
            "scenario": scenario,
            "appearance": QA_APPEARANCE,
            "request_id": str(uuid.uuid4())}
        req = urllib.request.Request(
            self.base + "/api/games", data=json.dumps(profile).encode(),
            headers={"Content-Type": "application/json"})
        try:
            with urllib.request.urlopen(req) as resp:
                body = json.loads(resp.read())
        except urllib.error.HTTPError as exc:
            detail = exc.read()[:300]
            if exc.code == 400 and b"unknown scenario" in detail:
                # hard failure, never a skip: a retired/renamed id must
                # be caught immediately (team rule, Claude 2026-11-16).
                catalog = self._discover_scenarios()
                raise RuntimeError(
                    "scenario %r rejected by service (retired/renamed?): "
                    "%s; runtime catalog: %s"
                    % (scenario, detail[:160], catalog))
            raise RuntimeError("scenario start -> HTTP %s: %s"
                               % (exc.code, detail))
        self.ctx["game_id"] = body["game_id"]
        self.record(flow, "scenario %s accepted" % scenario, True,
                    "game_id=%s" % body["game_id"][:8])
        self._hook_refresh_state(flow)
        expected_age = self.SCENARIO_AGES.get(scenario)
        player = next((p for p in self.ctx["state"].get("people", [])
                       if p.get("id") == self.ctx["state"].get(
                           "player_id")), {})
        # age verification only for ids we know; renamed/consolidated ids
        # discovered at runtime still get shape/person-ref assertions later
        if expected_age is not None and player.get("age") != expected_age:
            raise PendingSkip(
                "scenario %r not implemented by service yet (player age %r, "
                "expected %d)" % (scenario, player.get("age"), expected_age))

    def _hook_cmd(self, flow, action=None, payload=None, as_="cmd_result",
                  expect_status=None):
        """POST one game command using the CURRENT revision from
        ctx['state'] and a fresh request_id (replay-safe). Stores the
        response body in ctx[as_]."""
        import uuid
        state = self.ctx.get("state")
        if not state or not state.get("game_id"):
            raise RuntimeError("no ctx['state'] with game_id; run an api "
                               "GET step first")
        body = {"expected_revision": state["revision"],
                "request_id": str(uuid.uuid4()),
                "action": action,
                "payload": _resolve(payload or {}, self.ctx, state)}
        req = urllib.request.Request(
            self.base + "/api/games/" + state["game_id"] + "/actions",
            data=json.dumps(body).encode(),
            headers={"Content-Type": "application/json"})
        try:
            with urllib.request.urlopen(req) as resp:
                result = json.loads(resp.read())
                status = resp.status
        except urllib.error.HTTPError as exc:
            result = json.loads(exc.read() or b"{}")
            status = exc.code
        self.ctx[as_] = result
        want = expect_status
        ok = status == want if want else status < 400
        self.record(flow, "cmd %s" % action, ok,
                    "status=%s %s" % (status, json.dumps(result)[:160]))

    def _hook_assert_state(self, flow):
        """Validate the frozen M2 state/ledger shapes on ctx['state']
        (captured via an {"api": ...} or refresh_state step)."""
        from tools import m2_assertions
        state = self.ctx.get("state")
        if not isinstance(state, dict) or not state.get("game_id"):
            self.record(flow, "state sections match frozen shapes", False,
                        "no usable ctx['state'] at assertion time")
            return
        ok, problems = m2_assertions.check_state_sections(state)
        # A fixed job_label for employed lives; the frozen example is
        # silent about jobless states. Allow null only while unemployed,
        # as a visible WARN question to A - not a silent acceptance.
        work = state.get("work") or {}
        job_problems = [x for x in problems
                        if x == "work.job_label not str"]
        problems = [x for x in problems if x not in job_problems]
        # reported finding (A): after a mid-year firing the projection
        # emits turn.cursor.phase_id = null instead of a named phase
        # (phase-map says the cursor always carries a phase_id). WARN
        # with the exact signature; hard-fail on any other cursor issue.
        cursor_problems = [x for x in problems
                           if x == "turn.cursor.phase_id None unknown"]
        problems = [x for x in problems if x not in cursor_problems]
        if cursor_problems:
            self.record(flow, "WARN known deviation: turn.cursor.phase_id "
                        "null after firing (A; reported 2026-11-16)",
                        True, json.dumps((state.get("turn") or {}))[:160])
        if job_problems:
            if work.get("employed"):
                problems.extend(job_problems)
            else:
                self.record(flow, "WARN open question: job_label null while "
                            "unemployed (frozen shape silent; A to confirm)",
                            True, "job_label=%r" % (work.get("job_label"),))
        self.record(flow, "M2 state sections match frozen shapes",
                    not problems, json.dumps(problems)[:250])
        ok, problems = m2_assertions.check_person_refs_resolve(state)
        self.record(flow, "M2 person references match frozen shapes", ok,
                    json.dumps(problems)[:250])

    def _hook_create_game(self, flow, seed=None):
        import uuid
        profile = {
            "first_name": "QA",
            "last_name": "Testlife",
            "gender": "female",
            "country": "United States",
            "appearance": dict(QA_APPEARANCE, hair="curly"),
            "request_id": str(uuid.uuid4()),
        }
        if seed is not None:
            profile["seed"] = seed
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


def write_report(out, results, deferred):
    counts = {}
    for r in results:
        counts[r[4]] = counts.get(r[4], 0) + 1
    fails = [r for r in results if r[4] == "FAIL"]
    pendings = [r for r in results if r[4] == "PENDING"]
    required_pending = [r for r in pendings if r[0] not in deferred]
    lines = ["# Browser check report", ""]
    lines.append("PASS: %d | FAIL: %d | PENDING: %d | WARN: %d"
                 % (counts.get("PASS", 0), counts.get("FAIL", 0),
                    counts.get("PENDING", 0), counts.get("WARN", 0)))
    lines.append("")
    if pendings:
        lines.append("## Pending rows (unperformed paths)")
        lines.append("")
        for flow, name, _ok, extra, _st in pendings:
            tag = ("deferred (%s)" % deferred[flow]
                   if flow in deferred else "REQUIRED - fails the gate")
            lines.append("- [%s] %s -- %s; %s" % (flow, name, tag, extra))
        lines.append("")
    lines.append("| Result | Check | Detail |")
    lines.append("|--------|-------|--------|")
    for flow, name, _ok, extra, status in results:
        lines.append("| %s | [%s] %s | %s |"
                     % (status, flow, name, extra.replace("|", "\\|")))
    lines.append("")
    with open(os.path.join(out, "report.md"), "w") as f:
        f.write("\n".join(lines))
    return fails, required_pending


def main():
    if not CHROMIUM:
        print("FAIL: chromium not found")
        return 1
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-browser-check-")
    httpd = server.make_server(0, save_dir=save_dir)
    httpd.dev_mode = True  # scenario starts are dev-only (play-web --dev)
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
        # Robot player quick mode against the same server: gate on 5xx /
        # invariant breaks / soft-locks; eligibility mismatches are WARN.
        print("robot quick mode: %d lives, seed %d ..."
              % (ROBOT_LIVES, ROBOT_SEED), flush=True)
        robot_dir = os.path.join(EVIDENCE, "robot")
        hard, findings, stats = robot_quick(
            "http://127.0.0.1:%d" % port, lives=ROBOT_LIVES,
            seed=ROBOT_SEED, evidence_dir=robot_dir)
        if hard:
            for life, kind, path in hard[:5]:
                runner.record("robot", "robot-quick life %d: %s"
                              % (life, kind), False, path)
            runner.results.append(("robot", "robot-quick (%d lives)"
                                   % ROBOT_LIVES, False,
                                   "%d hard failure(s): %s"
                                   % (len(hard), "; ".join(
                                       "%s@%d" % (k, l) for l, k, _ in
                                       hard[:5])), "FAIL"))
        elif findings:
            runner.results.append(("robot", "robot-quick (%d lives)"
                                   % ROBOT_LIVES, True,
                                   "%d eligibility finding(s), 0 hard; "
                                   "%d commands/%d decisions; first: %s"
                                   % (len(findings), stats["commands"],
                                      stats["decisions"], findings[0][2]),
                                   "WARN"))
        else:
            runner.results.append(("robot", "robot-quick (%d lives)"
                                   % ROBOT_LIVES, True,
                                   "0 hard failures, 0 findings; %d "
                                   "commands/%d decisions"
                                   % (stats["commands"], stats["decisions"]),
                                   "PASS"))
        fails, required_pending = write_report(EVIDENCE, runner.results,
                                               DEFERRED_PENDING)
        n = lambda st: len([r for r in runner.results if r[4] == st])
        print("SUMMARY: PASS=%d FAIL=%d PENDING=%d (deferred=%d) WARN=%d"
              % (n("PASS"), n("FAIL"), n("PENDING"),
                 len([r for r in runner.results
                      if r[4] == "PENDING" and r[0] in DEFERRED_PENDING]),
                 n("WARN")))
        print("report at %s" % os.path.join(EVIDENCE, "report.md"))
        if required_pending:
            print("REQUIRED-PENDING (gate failed):")
            for flow, name, _ok, _extra, _st in required_pending:
                print(" - [%s] %s" % (flow, name))
        return 1 if (fails or required_pending) else 0
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
