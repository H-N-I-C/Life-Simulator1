#!/usr/bin/env python3
"""Robot player (Q1 task D1): drives the HTTP API through random lives.

Reproducible by seed. For every life it picks random creation choices,
fires random enabled actions, resolves every pending decision (including
text/number inputs at boundary lengths), ages up to death, sometimes
continues as a child, reloads state mid-turn and sends duplicate submits.
On any 5xx, invariant break or soft-lock it writes a minimal repro
(seed + command log) to docs/collab/evidence/robot/.

Usage:
  python tools/robot_player.py --lives 2000 --seed 1            # quick mode
  python tools/robot_player.py --lives 20000 --seed 1 --overnight
  python tools/robot_player.py --url http://127.0.0.1:8765 ...  # external server
  python tools/robot_player.py --replay docs/collab/evidence/robot/<file>.json

Stdlib only.
"""

import argparse
import json
import os
import random
import shutil
import sys
import tempfile
import threading
import time
import urllib.error
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

EVIDENCE_DIR = os.path.join(ROOT, "docs", "collab", "evidence", "robot")

# A life that never ends (soft-lock suspicion) after this many commands.
MAX_COMMANDS_PER_LIFE = 2500

FIRST_NAMES = {"male": [], "female": []}


def _load_names():
    for gender, fn in (("male", "male_names.txt"),
                       ("female", "female_names.txt")):
        path = os.path.join(ROOT, "assets", fn)
        try:
            with open(path, encoding="utf-8") as f:
                FIRST_NAMES[gender] = [ln.strip() for ln in f
                                       if ln.strip()][:200]
        except OSError:
            FIRST_NAMES[gender] = ["Alex", "Sam", "Robin", "Jo"]
    last_path = os.path.join(ROOT, "assets", "last_names.txt")
    try:
        with open(last_path, encoding="utf-8") as f:
            return [ln.strip() for ln in f if ln.strip()][:200]
    except OSError:
        return ["Citizen"]


class ApiError(Exception):
    def __init__(self, status, body):
        super().__init__("HTTP %s: %s" % (status, body[:400]))
        self.status = status
        self.body = body


class Client:
    """Minimal JSON HTTP client (no cookies needed; loopback only)."""

    def __init__(self, base):
        self.base = base.rstrip("/")

    def get(self, path):
        req = urllib.request.Request(self.base + path)
        return self._send(req)

    def post(self, path, body):
        data = json.dumps(body).encode()
        req = urllib.request.Request(
            self.base + path, data=data, method="POST",
            headers={"Content-Type": "application/json"})
        return self._send(req)

    def _send(self, req):
        try:
            with urllib.request.urlopen(req, timeout=60) as resp:
                raw = resp.read()
        except urllib.error.HTTPError as e:
            raw = e.read()
            raise ApiError(e.code, raw.decode("utf-8", "replace"))
        except urllib.error.URLError as e:
            raise ApiError(0, "connection error: %s" % e)
        if not raw:
            return {}
        return json.loads(raw.decode("utf-8"))


class Invariant:
    def __init__(self, state, previous_revision=None, replay=False):
        self.problems = []
        self._check_shape(state)
        if previous_revision is not None:
            if replay:
                if state["revision"] < previous_revision:
                    self.problems.append(
                        "revision moved backwards on replay: %s -> %s"
                        % (previous_revision, state["revision"]))
            elif state["revision"] != previous_revision + 1:
                self.problems.append(
                    "revision did not increment by exactly 1: %s -> %s"
                    % (previous_revision, state["revision"]))
        self._check_people(state)
        self._check_money(state)
        self._check_pending(state)

    def _check_shape(self, s):
        for key in ("game_id", "revision", "status", "player_id", "people",
                    "stats", "money", "journal", "available_actions",
                    "pending_decision", "finances"):
            if key not in s:
                self.problems.append("state missing key %r" % key)
        if s.get("status") not in ("alive", "dead"):
            self.problems.append("status %r not in (alive, dead)"
                                 % s.get("status"))
        stats = s.get("stats") or {}
        for k in ("happiness", "health", "smarts", "looks"):
            v = stats.get(k)
            if not isinstance(v, int) or isinstance(v, bool) \
                    or not 0 <= v <= 100:
                self.problems.append("stat %s out of range: %r" % (k, v))

    def _check_people(self, s):
        ids = set()
        for p in s.get("people") or []:
            pid = p.get("id")
            if not isinstance(pid, str) or not pid:
                self.problems.append("person with invalid id: %r" % (pid,))
                continue
            if pid in ids:
                self.problems.append("duplicate person id %r" % pid)
            ids.add(pid)
            age = p.get("age")
            if not isinstance(age, int) or isinstance(age, bool) \
                    or not 0 <= age <= 140:
                self.problems.append("person %s has invalid age %r"
                                     % (pid, age))
        player = next((p for p in s.get("people") or []
                       if p.get("id") == s.get("player_id")), None)
        if player is None:
            self.problems.append("player %r not in people list"
                                 % s.get("player_id"))
        elif player.get("alive") != (s.get("status") == "alive"):
            self.problems.append("player alive flag disagrees with status")

    def _check_money(self, s):
        ledger = ((s.get("finances") or {}).get("ledger")) or []
        total = 0
        for row in ledger:
            amount = row.get("amount")
            if not isinstance(amount, int) or isinstance(amount, bool):
                self.problems.append("ledger row with non-int amount: %r"
                                     % (row,))
                continue
            total += amount
        if total != s.get("money"):
            self.problems.append(
                "money %s != ledger sum %s (player %s)"
                % (s.get("money"), total, s.get("player_id")))

    def _check_pending(self, s):
        pending = s.get("pending_decision")
        if pending is None:
            return
        options = pending.get("options") or []
        if not options:
            self.problems.append("pending decision %r has no options "
                                 "(soft-lock)" % pending.get("id"))
            return
        if not any(o.get("enabled") for o in options):
            self.problems.append(
                "pending decision %r has zero enabled options (soft-lock)"
                % pending.get("id"))


class Failure(Exception):
    """A 5xx or invariant break worth saving as evidence."""

    def __init__(self, kind, detail, state=None):
        super().__init__("%s: %s" % (kind, detail))
        self.kind = kind
        self.detail = detail
        self.state = state


class Robot:
    def __init__(self, client, master_seed, last_names, action_log=None,
                 age_up_bias=0.45, tolerate_codes=()):
        self.client = client
        self.last_names = last_names
        self.age_up_bias = age_up_bias
        self.tolerate_codes = set(tolerate_codes)
        self.catalog = client.get("/api/catalog")
        self.countries = self.catalog.get("countries") or ["Earth"]
        self.master_seed = master_seed
        # Replayed lives replay the SAME per-life stream, so all robot
        # choices are reproducible from (master_seed, life_index).
        self._master = random.Random(master_seed)
        self._life_seeds = [self._master.getrandbits(63)
                            for _ in range(1_000_000)]
        self.action_log = action_log if action_log is not None else []
        self.request_no = 0
        self.stats = {"commands": 0, "decisions": 0, "age_ups": 0,
                      "continuations": 0, "duplicate_submits": 0,
                      "expected_4xx": 0, "reloads": 0}

    # ------------------------------------------------------------- plumbing
    def _life_rng(self, life_index):
        return random.Random(self._life_seeds[life_index])

    def _log(self, kind, **kw):
        self.action_log.append({"n": len(self.action_log), "kind": kind,
                                **kw})

    def _new_game(self, rng, life_index):
        gender = rng.choice(("male", "female"))
        first = rng.choice(FIRST_NAMES[gender])
        last = rng.choice(self.last_names)
        body = {
            "first_name": first[:40],
            "last_name": last[:40],
            "gender": gender,
            "country": rng.choice(self.countries),
            "seed": rng.getrandbits(31),
            "request_id": "robot-%s-%d-create" % (self.master_seed,
                                                  life_index),
        }
        if rng.random() < 0.7:
            body["appearance"] = self._random_appearance(rng)
        # M2.5 boundary characters (D3): every 9th life starts at 17
        # (minor: romantic self-actions must stay refused with a typed
        # 4xx), every 9th+1 at 18 (the first adult year). Fixed seed: the
        # 17 scenario gates on the university smarts roll, so it needs a
        # known-good seed (a setup miss currently surfaces as HTTP 500 —
        # reported separately, do not bake it into the boundary sweep).
        self.boundary_age = None
        if life_index % 9 == 0:
            body["scenario"] = "university_pending_17"
            body["seed"] = 777
            self.boundary_age = 17
        elif life_index % 9 == 1:
            body["scenario"] = "graduating_18"
            body["seed"] = 777
            self.boundary_age = 18
        self._log("create", body=body)
        state = self.client.post("/api/games", body)
        return state

    @staticmethod
    def _player_age(state):
        player = next((p for p in state.get("people") or []
                       if p.get("id") == state.get("player_id")), {})
        return player.get("age")

    def _random_appearance(self, rng):
        cat = self.catalog.get("catalog") or {}

        def pick(category):
            entries = cat.get(category) or []
            return rng.choice([e["id"] for e in entries]) if entries \
                else "none"

        return {
            "schema_version": 2,
            "seed": rng.getrandbits(31),
            "base": pick("base") if cat.get("base") else "person",
            "skin_tone": pick("skin_tone"),
            "hair": pick("hair"),
            "facial_hair": pick("facial_hair"),
            "headwear": pick("headwear"),
            "badges": rng.sample(
                [e["id"] for e in cat.get("badges") or []],
                k=min(len(cat.get("badges") or []), rng.randint(0, 2))),
            "background": pick("background"),
            "look_override": "none",
        }

    def _command(self, game_id, revision, action, payload, rng,
                 request_id=None, expect_status=None):
        self.request_no += 1
        if request_id is None:
            request_id = "robot-%s-r%d" % (self.master_seed, self.request_no)
        body = {"request_id": request_id,
                "expected_revision": revision,
                "action": action,
                "payload": payload}
        self._log("command", game_id=game_id, action=action, payload=payload,
                  request_id=request_id, expected_revision=revision)
        try:
            state = self.client.post("/api/games/%s/actions" % game_id, body)
        except ApiError as e:
            self._log("error", status=e.status, body=e.body[:1000])
            if e.status >= 500:
                raise Failure("http_5xx", "action %r -> HTTP %s: %s"
                              % (action, e.status, e.body[:500]))
            if expect_status is not None and \
                    e.status in (
                        expect_status if isinstance(expect_status, tuple)
                        else (expect_status,)):
                self.stats["expected_4xx"] += 1
                return None
            if expect_status is None:
                # Any typed 4xx from a VALID robot-built request is a bug:
                # the robot only sends what the state's own action/pending
                # specs advertise. Boundary-input probes opt out via
                # expect_status. --tolerate-codes downgrades known classes
                # to findings (the life continues) so deeper bugs surface.
                code = "?"
                try:
                    code = json.loads(e.body)["error"]["code"]
                except Exception:
                    pass
                if code in self.tolerate_codes:
                    self.stats["tolerated_4xx"] = \
                        self.stats.get("tolerated_4xx", 0) + 1
                    return None
                raise Failure("unexpected_4xx",
                              "action %r -> HTTP %s: %s"
                              % (action, e.status, e.body[:500]))
            raise Failure("wrong_4xx", "expected %s got %s for %r: %s"
                          % (expect_status, e.status, action, e.body[:300]))
        return state

    # ------------------------------------------------------------- inputs
    def _fill_inputs(self, inputs, rng, boundary=False):
        values = {}
        for spec in inputs or []:
            iid = spec.get("id")
            itype = spec.get("type")
            if itype == "text":
                if boundary and rng.random() < 0.5:
                    values[iid] = "N" * rng.choice((1, 40))
                else:
                    values[iid] = rng.choice(
                        FIRST_NAMES["male"] or ["Alex"])[:spec.get("max", 40)]
            elif itype == "number":
                lo, hi = spec.get("min", 0), spec.get("max", 100)
                values[iid] = rng.randint(lo, hi)
            elif itype == "numbers":
                lo, hi = spec.get("min", 1), spec.get("max", 20)
                count = spec.get("count", 4)
                lines = spec.get("lines", 1)

                def one_line():
                    return rng.sample(range(lo, hi + 1), count)

                values[iid] = [one_line() for _ in range(lines)] \
                    if lines > 1 else one_line()
            else:
                values[iid] = None
        return values

    # ------------------------------------------------------------- one life
    def play_life(self, life_index):
        rng = self._life_rng(life_index)
        start = self._new_game(rng, life_index)
        game_id = start["game_id"]
        inv = Invariant(start)
        if inv.problems:
            raise Failure("invariant", "; ".join(inv.problems), start)
        if self.boundary_age is not None:
            age = self._player_age(start)
            if age != self.boundary_age:
                raise Failure("invariant",
                              "boundary life %d: player age %r, expected "
                              "%d (scenario ignored?)"
                              % (life_index, age, self.boundary_age), start)
            self.stats["boundary_lives"] = \
                self.stats.get("boundary_lives", 0) + 1
        state = start
        revision = state["revision"]

        for step in range(MAX_COMMANDS_PER_LIFE):
            if step % 7 == 0:
                # reload mid-turn: drop local state, re-read from server
                state = self.client.get("/api/games/%s" % game_id)
                self.stats["reloads"] += 1
                revision = state["revision"]

            pending = state.get("pending_decision")
            if pending:
                state, revision = self._resolve_pending(
                    game_id, state, pending, rng)
                self.stats["decisions"] += 1
            elif state.get("status") == "dead":
                if self._maybe_continue(game_id, state, rng):
                    state = self.client.get("/api/games/%s" % game_id)
                    revision = state["revision"]
                    self.stats["continuations"] += 1
                    continue
                return state  # life over for good
            else:
                state, revision = self._take_action(game_id, state, rng)

            if state is not None:
                inv = Invariant(state, revision - 1)
                if inv.problems:
                    raise Failure("invariant", "; ".join(inv.problems), state)
        raise Failure("soft_lock",
                      "life %s exceeded %d commands without ending"
                      % (game_id, MAX_COMMANDS_PER_LIFE), state)

    def _resolve_pending(self, game_id, state, pending, rng):
        revision = state["revision"]
        options = [o for o in pending["options"] if o.get("enabled")]
        option = rng.choice(options)
        # Boundary probes: occasionally send deliberately invalid inputs;
        # these must produce a typed 4xx, never a 5xx.
        if rng.random() < 0.04:
            bad = dict(option)
            bad_inputs = self._probe_inputs(pending, option, rng)
            result = self._command(
                game_id, revision, "resolve_decision",
                {"decision_id": pending["id"],
                 "option_id": option["id"], "inputs": bad_inputs},
                rng, expect_status=400)
            if result is None:  # typed 400 as required
                return self.client.get("/api/games/%s" % game_id), revision
            # the option had no corruptible inputs: the probe resolved it
            return result, result["revision"]
        payload = {"decision_id": pending["id"], "option_id": option["id"]}
        inputs = option.get("inputs")
        if inputs:
            payload["inputs"] = self._fill_inputs(inputs, rng,
                                                  boundary=True)
        new_state = self._command(game_id, revision, "resolve_decision",
                                  payload, rng)
        if new_state is None:
            new_state = self.client.get("/api/games/%s" % game_id)
        # duplicate submit: same request must replay byte-identically
        if rng.random() < 0.05:
            dup = self._command(game_id, revision, "resolve_decision",
                                payload, rng,
                                request_id="robot-%s-r%d"
                                % (self.master_seed, self.request_no))
            self.stats["duplicate_submits"] += 1
            if dup != new_state:
                raise Failure("replay_mismatch",
                              "duplicate submit returned a different state")
        return new_state, new_state["revision"]

    def _probe_inputs(self, pending, option, rng):
        """Deliberately invalid inputs for boundary testing."""
        probe = {}
        for spec in option.get("inputs") or []:
            itype = spec.get("type")
            if itype == "text":
                probe[spec["id"]] = rng.choice(["", "x" * 5000,
                                                "x" * (spec.get("max", 40)
                                                       + 1)])
            elif itype == "numbers":
                probe[spec["id"]] = [99] * (spec.get("count", 4))
            elif itype == "number":
                probe[spec["id"]] = spec.get("max", 100) + 1
        if not probe:
            # no inputs to corrupt: corrupt the decision reference instead
            return None
        return probe

    def _maybe_continue(self, game_id, state, rng):
        succ = state.get("succession") or {}
        eligible = succ.get("eligible") or []
        if not eligible or rng.random() < 0.3:
            # invalid-continue probe must 404/409, never 5xx
            self._command(game_id, state["revision"], "continue_as",
                          {"target_id": "nobody"}, rng, expect_status=(404, 409))
            return False
        target = rng.choice(eligible)
        new_state = self._command(game_id, state["revision"], "continue_as",
                                  {"target_id": target["person_id"]}, rng)
        inv = Invariant(new_state, state["revision"])
        if inv.problems:
            raise Failure("invariant", "; ".join(inv.problems), new_state)
        return True

    def _take_action(self, game_id, state, rng):
        revision = state["revision"]
        actions = [a for a in state.get("available_actions") or []
                   if a.get("enabled")]
        if not actions:
            raise Failure("soft_lock",
                          "alive with no enabled actions (not even age_up)",
                          state)

        # Stale-revision probe must 409, never 5xx.
        if rng.random() < 0.02 and revision > 0:
            self._command(game_id, revision - 1, "age_up", {}, rng,
                          expect_status=409)

        # Unknown-action probe must 400, never 5xx.
        if rng.random() < 0.01:
            self._command(game_id, revision, "definitely_not_an_action",
                          {}, rng, expect_status=400)

        # M2.5 adult-gate probes (D3): while the player is a minor,
        # romantic self-actions are never advertised and dispatch must
        # refuse them with a typed 4xx — never a silent success, never
        # a 5xx. This is the runtime half of the 17/18 boundary.
        age = self._player_age(state)
        if age is not None and age < 18 and rng.random() < 0.5:
            self._command(game_id, revision, "dating_app", {}, rng,
                          expect_status=(400, 403, 404, 409))
            self.stats["minor_romantic_probes"] = \
                self.stats.get("minor_romantic_probes", 0) + 1
            partner = (state.get("family") or {}).get("partner")
            if partner:
                pid = partner.get("id") if isinstance(partner, dict) \
                    else partner
                for romantic in ("propose", "try_for_baby", "plan_wedding"):
                    self._command(game_id, revision, romantic,
                                  {"target_id": pid}, rng,
                                  expect_status=(400, 403, 404, 409))
                    self.stats["minor_romantic_probes"] = \
                        self.stats.get("minor_romantic_probes", 0) + 1

        if rng.random() < self.age_up_bias:
            action = next(a for a in actions if a["id"] == "age_up")
        else:
            action = rng.choice(actions)
        payload = {}
        if action.get("target_id"):
            payload["target_id"] = action["target_id"]
        if action.get("inputs"):
            payload.update(self._fill_inputs(action["inputs"], rng,
                                             boundary=True))
        new_state = self._command(game_id, revision, action["id"], payload,
                                  rng)
        self.stats["commands"] += 1
        if action["id"] == "age_up":
            self.stats["age_ups"] += 1
        if new_state is None:
            new_state = self.client.get("/api/games/%s" % game_id)
        # duplicate submit on a normal action
        if rng.random() < 0.03:
            self._command(game_id, revision, action["id"], payload, rng,
                          request_id="robot-%s-r%d"
                          % (self.master_seed, self.request_no))
            self.stats["duplicate_submits"] += 1
        return new_state, new_state["revision"]


def _save_evidence(seed, life_index, log, failure, state,
                   evidence_dir=EVIDENCE_DIR):
    os.makedirs(evidence_dir, exist_ok=True)
    path = os.path.join(
        evidence_dir,
        "robot-seed%s-life%d-%s-%s.json"
        % (seed, life_index, failure.kind,
           time.strftime("%Y%m%d-%H%M%S")))
    data = {
        "robot": "tools/robot_player.py",
        "seed": seed,
        "life_index": life_index,
        "failure": {"kind": failure.kind, "detail": failure.detail},
        "state": state,
        "command_log": log,
        "reproduce": "python tools/robot_player.py --lives %d --seed %s "
                     "--from-life %d --fail-fast" % (life_index + 1, seed,
                                                     life_index),
    }
    with open(path, "w", encoding="utf-8") as f:
        json.dump(data, f, indent=1, default=str)
    return path


def _replay(client, path):
    """Re-send an evidence file's command log verbatim against a fresh
    server; the profile seed makes the server-side RNG deterministic."""
    with open(path, encoding="utf-8") as f:
        data = json.load(f)
    log = data["command_log"]
    failures = []
    for i, entry in enumerate(log):
        if entry["kind"] == "create":
            client.post("/api/games", entry["body"])
            continue
        if entry["kind"] != "command":
            continue
        body = {"request_id": entry["request_id"],
                "expected_revision": entry["expected_revision"],
                "action": entry["action"],
                "payload": entry["payload"]}
        try:
            client.post("/api/games/%s/actions" % entry["game_id"], body)
        except ApiError as e:
            if e.status >= 500:
                failures.append((i, e.status, e.body[:300]))
            # typed 4xx is fine in replay (probe commands)
    print("REPLAY of %s: %d commands, %d 5xx" % (path, len(log), len(failures)))
    for i, status, body in failures:
        print("  cmd %d -> HTTP %s %s" % (i, status, body))
    return 1 if failures else 0


def quick_check(base, lives=200, seed=7, evidence_dir=None):
    """Small reproducible robot run against a live server (used by
    browser_check and CI). Returns (hard_failures, findings, stats):

    hard_failures: [(life_index, kind, path)] — 5xx, invariant breaks,
    soft-locks; these are gate failures.
    findings: [(life_index, kind, path)] — typed 4xx eligibility
    mismatches (unexpected_4xx/wrong_4xx); reported, not gated.
    """
    if evidence_dir is None:
        evidence_dir = EVIDENCE_DIR
    last_names = _load_names()
    client = Client(base)
    robot = Robot(client, seed, last_names)
    hard, findings = [], []
    for life_index in range(lives):
        robot.action_log = []
        try:
            robot.play_life(life_index)
        except Failure as f:
            path = _save_evidence(seed, life_index, robot.action_log, f,
                                  f.state, evidence_dir)
            if f.kind in ("http_5xx", "invariant", "soft_lock"):
                hard.append((life_index, f.kind, path))
            else:
                findings.append((life_index, f.kind, path))
        except ApiError as e:
            f = Failure("http_5xx", "HTTP %s: %s" % (e.status, e.body))
            path = _save_evidence(seed, life_index, robot.action_log, f,
                                  None, evidence_dir)
            hard.append((life_index, f.kind, path))
    return hard, findings, robot.stats


def run(args):
    last_names = _load_names()
    failures = []
    server = None
    if args.url:
        base = args.url
        client = Client(base)
    else:
        import web.server as web_server
        save_dir = tempfile.mkdtemp(
            prefix="robot_saves_",
            # user quota on /tmp (tmpfs) is shared with the other
            # agents' runs; the repo save dir lives on the root fs
            dir=os.path.join(ROOT, "game_saves"))
        server = web_server.make_server(port=0, save_dir=save_dir)
        server.dev_mode = True
        thread = threading.Thread(target=server.serve_forever,
                                  daemon=True)
        thread.start()
        base = "http://127.0.0.1:%d" % server.server_port
        client = Client(base)

    if args.replay:
        try:
            return _replay(client, args.replay)
        finally:
            if server is not None:
                server.shutdown()
                server.server_close()
                shutil.rmtree(server.service.save_dir, ignore_errors=True)

    lives = range(args.from_life, args.lives)
    robot = Robot(client, args.seed, last_names,
                  age_up_bias=0.97 if args.hammer else 0.45,
                  tolerate_codes=args.tolerate_codes)
    t0 = time.time()
    done = 0
    try:
        for life_index in lives:
            robot.action_log = []
            try:
                robot.play_life(life_index)
            except Failure as f:
                path = _save_evidence(args.seed, life_index,
                                      robot.action_log, f, f.state)
                failures.append((life_index, f.kind, path))
                print("LIFE %d: %s -> %s" % (life_index, f.kind, path))
                if args.fail_fast:
                    break
            except ApiError as e:
                f = Failure("http_5xx", "HTTP %s: %s" % (e.status, e.body))
                path = _save_evidence(args.seed, life_index,
                                      robot.action_log, f, None)
                failures.append((life_index, f.kind, path))
                print("LIFE %d: %s -> %s" % (life_index, f.kind, path))
                if args.fail_fast:
                    break
            done += 1
            if args.max_failures and len(failures) >= args.max_failures:
                break
            if done % 100 == 0:
                rate = done / max(time.time() - t0, 0.1)
                print("... %d lives, %d failures (%.1f lives/s)"
                      % (done, len(failures), rate), flush=True)
    finally:
        if server is not None:
            server.shutdown()
            server.server_close()
            shutil.rmtree(server.service.save_dir, ignore_errors=True)

    elapsed = time.time() - t0
    summary = {
        "lives": done,
        "failures": [{"life": l, "kind": k, "evidence": p}
                     for l, k, p in failures],
        "stats": robot.stats,
        "elapsed_s": round(elapsed, 1),
        "seed": args.seed,
    }
    print(json.dumps(summary, indent=1))
    line = ("ROBOT SUMMARY: %d lives, %d failures, %d commands, %d decisions"
            " in %.0fs (seed %s) — %s"
            % (done, len(failures), robot.stats["commands"],
               robot.stats["decisions"], elapsed, args.seed,
               "CLEAN" if not failures else failures[0][2]))
    print(line)
    return 1 if failures else 0


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--lives", type=int, default=2000)
    ap.add_argument("--seed", type=int, default=1)
    ap.add_argument("--from-life", type=int, default=0)
    ap.add_argument("--url", default=None,
                    help="external server URL (default: spawn in-process)")
    ap.add_argument("--fail-fast", action="store_true")
    ap.add_argument("--max-failures", type=int, default=10)
    ap.add_argument("--overnight", action="store_true",
                    help="label only; use --lives 20000")
    ap.add_argument("--hammer", action="store_true",
                    help="age-up-heavy mode: reproduce 'crashed while aging "
                         "up' (97% age_up, full lifespans, many seeds)")
    ap.add_argument("--tolerate-codes", default=(),
                    help="comma-separated typed 4xx error codes to count as "
                         "findings instead of aborting the life (e.g. "
                         "not_allowed)")
    ap.add_argument("--replay", default=None,
                    help="replay an evidence file's command log verbatim "
                         "against a fresh server")
    args = ap.parse_args()
    if isinstance(args.tolerate_codes, str):
        args.tolerate_codes = tuple(
            c.strip() for c in args.tolerate_codes.split(",") if c.strip())
    sys.exit(run(args))


if __name__ == "__main__":
    main()
