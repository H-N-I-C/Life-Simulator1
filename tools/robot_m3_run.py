#!/usr/bin/env python3
"""M3 robot run (Kimi D, QA): fresh 200-life run on the current tree with
undo_year in the action mix.

What it adds on top of tools/robot_player.py (the shared robot is NOT
modified; this subclasses it):

  * Undo mix: when a life is alive, has no pending decision and the
    server's can_undo spec is enabled, with probability --undo-bias the
    robot runs a full cycle instead of a random action:
        age_up -> capture the new year's journal lines
        undo_year -> assert age back + revision MONOTONIC (+1)
        age_up again -> assert the redone year's journal lines are
                        IDENTICAL (age, kind, text) — undo is never a
                        re-roll (contracts.md §Undo).
  * Journal repetition invariant (checked on EVERY state): no identical
    journal TEXT within a 3-year age window (feel-pass flavour variety).
  * Existing robot invariants kept: money == ledger sum (conservation),
    revision monotonic, soft-lock detection, minor 18+ romantic probes
    (typed 4xx only), stale-revision/unknown-action probes (409/400, never
    5xx).

Failures are saved as evidence JSON (root-cause + owner named in the
summary) to docs/collab/evidence/robot/. TMPDIR=~/.lifesim-tmp per QA rule.

    python3 tools/robot_m3_run.py --lives 200 --seed 41
"""

import argparse
import json
import os
import sys
import threading
import time

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

TMP_HOME = os.path.expanduser("~/.lifesim-tmp")  # RULE: never /tmp
os.makedirs(TMP_HOME, exist_ok=True)
os.environ["TMPDIR"] = TMP_HOME

import tools.robot_player as rp  # noqa: E402


def journal_lines(state):
    return [(e.get("age"), e.get("kind"), e.get("text"))
            for e in state.get("journal") or []]


class RepetitionLedger:
    """Detects identical journal text within a 3-year age window.

    States are re-checked every command, so pairs are deduped by
    (text, earlier_age, later_age)."""

    def __init__(self, window=3):
        self.window = window
        self.violations = []
        self._recorded = set()

    def check(self, state):
        seen = {}
        for age, kind, text in journal_lines(state):
            if text is None:
                continue
            prev = seen.get(text)
            if prev is not None and age is not None and \
                    age - prev[0] <= self.window:
                key = (text, prev[0], age)
                if key not in self._recorded:
                    self._recorded.add(key)
                    self.violations.append(
                        {"text": text[:120], "kind": kind,
                         "ages": [prev[0], age],
                         "game_id": state.get("game_id")})
            if prev is None or age is None or age > prev[0]:
                seen[text] = (age, kind)


class M3Robot(rp.Robot):
    def __init__(self, *args, undo_bias=0.15, **kw):
        super().__init__(*args, **kw)
        self.undo_bias = undo_bias
        self.repetition = RepetitionLedger(window=3)
        self.stats["undo_cycles"] = 0
        self.stats["redo_mismatches"] = 0

    # ---- undo cycle ---------------------------------------------------
    def _undo_cycle(self, game_id, state, rng):
        revision = state["revision"]
        age_before = self._player_age(state)
        lines_before = journal_lines(state)

        st1 = self._command(game_id, revision, "age_up", {}, rng)
        self.stats["commands"] += 1
        self.stats["age_ups"] += 1
        if st1 is None:  # unexpected; _command raised already
            return state
        new_lines = journal_lines(st1)[len(lines_before):]

        can_undo = st1.get("can_undo") or {}
        if not can_undo.get("enabled"):
            return st1  # nothing to undo (should not happen after age_up)
        st2 = self._command(game_id, st1["revision"], "undo_year", {}, rng)
        self.stats["commands"] += 1
        if st2 is None:
            return st1
        self.stats["undo_cycles"] += 1
        if self._player_age(st2) != age_before or \
                st2["revision"] != st1["revision"] + 1:
            raise rp.Failure(
                "invariant",
                "undo violated §Undo: age %s->%s (want %s), rev %s->%s "
                "(want +1)" % (age_before, self._player_age(st2),
                              age_before, st1["revision"], st2["revision"]),
                st2)

        st3 = self._command(game_id, st2["revision"], "age_up", {}, rng)
        self.stats["commands"] += 1
        self.stats["age_ups"] += 1
        if st3 is None:
            return st2
        redone = journal_lines(st3)[len(lines_before):]
        if redone != new_lines:
            self.stats["redo_mismatches"] += 1
            raise rp.Failure(
                "invariant",
                "redo after undo did not replay identically: %s vs %s"
                % (new_lines[-3:], redone[-3:]), st3)
        return st3

    # ---- action mix ----------------------------------------------------
    def _take_action(self, game_id, state, rng):
        """Returns (state, revision, strict). strict=False means the step
        was an undo cycle whose revision deltas were already validated
        inside the cycle (age +1, undo +1, redo +1)."""
        pending = state.get("pending_decision")
        can_undo = (state.get("can_undo") or {}).get("enabled")
        if not pending and can_undo and rng.random() < self.undo_bias:
            st = self._undo_cycle(game_id, state, rng)
            return st, (st or state)["revision"], False
        new_state, revision = super()._take_action(game_id, state, rng)
        return new_state, revision, True

    # ---- play loop with the repetition invariant ------------------------
    def play_life(self, life_index):
        rng = self._life_rng(life_index)
        start = self._new_game(rng, life_index)
        game_id = start["game_id"]
        inv = rp.Invariant(start)
        if inv.problems:
            raise rp.Failure("invariant", "; ".join(inv.problems), start)
        state = start
        revision = state["revision"]
        strict = True
        self.repetition.check(state)

        for step in range(rp.MAX_COMMANDS_PER_LIFE):
            if step % 7 == 0:
                state = self.client.get("/api/games/%s" % game_id)
                revision = state["revision"]
                self.stats["reloads"] += 1

            strict = True  # resolve_pending / reload paths move rev by 1
            prev_revision = revision
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
                return state
            else:
                state, new_rev, strict = self._take_action(game_id, state,
                                                           rng)

            if state is not None:
                # prev_revision is the PRE-step revision for every branch
                # (resolve/reload reassign `revision` themselves).
                inv = rp.Invariant(state, prev_revision) if strict \
                    else rp.Invariant(state)
                if inv.problems:
                    raise rp.Failure("invariant", "; ".join(inv.problems),
                                     state)
                self.repetition.check(state)
                revision = state["revision"]
        raise rp.Failure(
            "soft_lock", "life %s exceeded %d commands without ending"
            % (game_id, rp.MAX_COMMANDS_PER_LIFE), state)


def summarize_failures(failures):
    """Group evidence by failure kind for root-cause + owner naming."""
    by_kind = {}
    for life_index, kind, path in failures:
        by_kind.setdefault(kind, []).append((life_index, path))
    return by_kind


OWNER_HINTS = {
    "http_5xx": "A (engine/service) — server fault; root-cause from the "
                "evidence JSON's command_log tail",
    "invariant": "A (engine) unless the detail names projection/ledger, "
                 "then A (ledger/projection); journal text dups are A "
                 "(journal engine) or C (flavor content)",
    "soft_lock": "A (engine) — a state with no legal actions",
    "unexpected_4xx": "owner depends on the action id in the detail "
                      "(careers A / education B / skills C / engine A)",
    "wrong_4xx": "owner depends on the probed action (see detail)",
}


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--lives", type=int, default=200)
    ap.add_argument("--seed", type=int, default=41)
    ap.add_argument("--undo-bias", type=float, default=0.15)
    ap.add_argument("--url", default=None)
    args = ap.parse_args()

    import web.server as web_server
    server = None
    if args.url:
        base = args.url
    else:
        import tempfile
        save_dir = tempfile.mkdtemp(prefix="robot_m3_saves-", dir=TMP_HOME)
        server = web_server.make_server(port=0, save_dir=save_dir)
        server.dev_mode = True
        threading.Thread(target=server.serve_forever, daemon=True).start()
        base = "http://127.0.0.1:%d" % server.server_port
    client = rp.Client(base)
    last_names = rp._load_names()
    robot = M3Robot(client, args.seed, last_names,
                    age_up_bias=0.45, undo_bias=args.undo_bias)

    hard, findings = [], []
    t0 = time.time()
    for life_index in range(args.lives):
        robot.action_log = []
        try:
            robot.play_life(life_index)
        except rp.Failure as f:
            path = rp._save_evidence(args.seed, life_index,
                                     robot.action_log, f, f.state)
            (hard if f.kind in ("http_5xx", "invariant", "soft_lock")
             else findings).append((life_index, f.kind, path))
            print("LIFE %d: %s -> %s" % (life_index, f.kind, path))
        except rp.ApiError as e:
            f = rp.Failure("http_5xx", "HTTP %s: %s" % (e.status, e.body))
            path = rp._save_evidence(args.seed, life_index,
                                     robot.action_log, f, None)
            hard.append((life_index, f.kind, path))
            print("LIFE %d: %s -> %s" % (life_index, f.kind, path))
        if (life_index + 1) % 25 == 0:
            print("... %d/%d lives, %ds" % (life_index + 1, args.lives,
                                            time.time() - t0))
    elapsed = time.time() - t0

    # ---------------- repetition report --------------------------------
    reps = robot.repetition.violations
    rep_kinds = {}
    for v in reps:
        rep_kinds[v["kind"]] = rep_kinds.get(v["kind"], 0) + 1

    counts = {"hard_failures": len(hard), "findings": len(findings),
              "journal_repetition_violations": len(reps),
              "undo_cycles": robot.stats.get("undo_cycles", 0),
              "redo_mismatches": robot.stats.get("redo_mismatches", 0)}
    summary = {
        "run": "tools/robot_m3_run.py", "seed": args.seed,
        "lives": args.lives, "undo_bias": args.undo_bias,
        "elapsed_s": round(elapsed, 1), "counts": counts,
        "stats": robot.stats,
        "journal_repetition_by_kind": rep_kinds,
        "hard_failures": [
            {"life": li, "kind": k,
             "evidence": os.path.relpath(p, rp.EVIDENCE_DIR),
             "owner_hint": OWNER_HINTS.get(k, "TBD — root-cause from "
                                              "evidence")}
            for li, k, p in hard],
        "findings": [
            {"life": li, "kind": k,
             "evidence": os.path.relpath(p, rp.EVIDENCE_DIR),
             "owner_hint": OWNER_HINTS.get(k, "TBD")}
            for li, k, p in findings],
        "journal_repetition_samples": reps[:20],
        "checks": {
            "5xx": "PASS (0)" if not any(k == "http_5xx" for _, k, _ in hard)
                   else "FAIL (%d)" % sum(1 for _, k, _ in hard
                                          if k == "http_5xx"),
            "invariants_money_revision": "PASS (0 invariant failures)"
                if not any(k == "invariant" for _, k, _ in hard)
                else "FAIL (%d)" % sum(1 for _, k, _ in hard
                                       if k == "invariant"),
            "soft_locks": "PASS (0)" if not any(k == "soft_lock"
                                                for _, k, _ in hard)
                          else "FAIL (%d)" % sum(1 for _, k, _ in hard
                                                 if k == "soft_lock"),
            "undo_redo_identical": "PASS (%d cycles)" % counts["undo_cycles"]
                if counts["redo_mismatches"] == 0 else
                "FAIL (%d mismatches)" % counts["redo_mismatches"],
            "18plus_minor_probes": "PASS (%d probes, all typed 4xx)"
                % robot.stats.get("minor_romantic_probes", 0)
                if not any(k in ("unexpected_4xx", "wrong_4xx")
                           for _, k, _ in findings + hard)
                else "see findings (unexpected status on a probe)",
            "money_conservation": "checked every state (money == ledger "
                                  "sum) by the robot Invariant",
            "journal_no_repeat_within_3y": "PASS (0 violations)" if not reps
                else "FAIL (%d violations by kind %s)"
                     % (len(reps), rep_kinds),
        },
    }
    os.makedirs(rp.EVIDENCE_DIR, exist_ok=True)
    out_json = os.path.join(rp.EVIDENCE_DIR,
                            "m3-undo-mix-seed%s-summary.json" % args.seed)
    with open(out_json, "w", encoding="utf-8") as f:
        json.dump(summary, f, indent=1, default=str)
    out_md = os.path.join(rp.EVIDENCE_DIR,
                          "m3-undo-mix-seed%s-summary.md" % args.seed)
    with open(out_md, "w", encoding="utf-8") as f:
        f.write("# M3 robot run with undo mix (Kimi D QA) — seed %d, "
                "%d lives, %.0fs\n\n" % (args.seed, args.lives, elapsed))
        f.write("Command: `python3 tools/robot_m3_run.py --lives %d "
                "--seed %d --undo-bias %s` (tools/robot_player.py "
                "subclassed, shared robot unmodified; TMPDIR=~/.lifesim-tmp)\n\n"
                % (args.lives, args.seed, args.undo_bias))
        f.write("## Checks\n\n")
        for k, v in summary["checks"].items():
            f.write("- **%s**: %s\n" % (k, v))
        f.write("\n## Counts\n\n```json\n%s\n```\n"
                % json.dumps(counts, indent=1))
        f.write("\n## Robot stats\n\n```json\n%s\n```\n"
                % json.dumps(robot.stats, indent=1, default=str))
        if hard:
            f.write("\n## Hard failures (root-cause + owner)\n\n")
            for li, k, p in hard:
                f.write("- life %d `%s` — %s\n  evidence: %s\n"
                        % (li, k, OWNER_HINTS.get(k, "TBD"),
                           os.path.relpath(p, rp.EVIDENCE_DIR)))
        if findings:
            f.write("\n## Findings (typed 4xx mismatches; reported, "
                    "not gated)\n\n")
            for li, k, p in findings:
                f.write("- life %d `%s` — %s\n  evidence: %s\n"
                        % (li, k, OWNER_HINTS.get(k, "TBD"),
                           os.path.relpath(p, rp.EVIDENCE_DIR)))
        if reps:
            f.write("\n## Journal repetition samples (first 20)\n\n")
            for v in reps[:20]:
                f.write("- %s ages %s: %r\n"
                        % (v["kind"], v["ages"], v["text"]))
    print("summary: %s" % out_md)
    print("checks: %s" % json.dumps(summary["checks"], indent=1))
    if server is not None:
        server.shutdown()
        server.server_close()
        import shutil
        shutil.rmtree(server.service.save_dir, ignore_errors=True)
    return 1 if (hard or reps) else 0


if __name__ == "__main__":
    sys.exit(main())
