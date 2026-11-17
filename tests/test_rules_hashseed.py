"""Q1 hash-seed determinism regression (Kimi C).

D's robot player reported that PYTHONHASHSEED changes game outcomes. This
test drives one seeded life through the REAL GameService under three
different PYTHONHASHSEED values in subprocesses and asserts the
uuid-normalized state is byte-identical every turn — proving no set/dict
hash-order iteration feeds the RNG stream.

Run just the driver (used by the test's subprocesses):
    python tests/test_rules_hashseed.py --driver [turns]

Set SKIP_HASHSEED=1 to skip in quick runs (each of the 3 subprocesses
drives ~12 service turns).
"""
import hashlib
import json
import os
import re
import subprocess
import sys
import tempfile
import unittest

UUID_SUB = re.compile(
    r"[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}"
    r"|^g_[0-9a-f]+$")

PROFILE = {
    "first_name": "Ada", "last_name": "Lee", "gender": "female",
    "country": "Australia", "seed": 4242,
}

# A deterministic spread of actions covering relationships, offers with
# sampled outcomes, paid gates and yearly phases.
ACTIONS = ("gym", "meditate", "read_book", "lottery", "spend_time",
           "conversation", "compliment", "find_partner", "try_for_baby",
           "propose", "doctor", "witch_doctor", "find_job",
           "work_harder", "request_raise")


def clean(o):
    """Normalize random ids (uuid4/game ids appear in references and
    ledger source keys); simulation outcomes must never depend on them."""
    if isinstance(o, dict):
        return {k: clean(v) for k, v in o.items()}
    if isinstance(o, list):
        return [clean(v) for v in o]
    if isinstance(o, str):
        return UUID_SUB.sub("UUID", o)
    return o


def auto_resolve(svc, gid, st, tag):
    n = 0
    while st["pending_decision"]:
        d = st["pending_decision"]
        options = [o for o in d["options"] if o.get("enabled", True)]
        if not options:
            options = d["options"][-1:]
        choice = options[0]
        inputs = {}
        for inp in choice.get("inputs", []):
            if inp.get("type") == "numbers":
                inputs[inp["id"]] = ([[1, 2, 3, 4]] * inp.get("lines", 1)
                                     if inp.get("lines") else [1, 2, 3, 4])
            else:
                inputs[inp["id"]] = "Robin"
        st = svc.command(gid, st["revision"], f"{tag}-d{n}",
                         "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": choice["id"], "inputs": inputs})
        n += 1
        if n > 25:
            raise AssertionError("pending chain did not drain")
    return st


def run_life(turns):
    """Drive one seeded life; print one normalized digest line per turn."""
    from src.game.service import GameService
    svc = GameService(tempfile.mkdtemp(prefix="hashseed-test-"))
    st = svc.new_game(dict(PROFILE), "c1")
    gid = st["game_id"]
    turn = 0
    while st.get("status", "alive") == "alive" and turn < turns:
        try:
            st = auto_resolve(svc, gid, st, f"t{turn}")
            for action in ACTIONS:
                st = auto_resolve(svc, gid, st, f"t{turn}")
                acts = {a["id"]: a for a in st.get("available_actions", [])}
                if action in acts and acts[action].get("enabled", True):
                    payload = {}
                    if acts[action].get("target_id"):
                        payload["target_id"] = acts[action]["target_id"]
                    try:
                        st = svc.command(gid, st["revision"],
                                         f"t{turn}-{action}", action,
                                         payload)
                    except Exception:
                        pass
                    st = auto_resolve(svc, gid, st, f"t{turn}{action}")
            st = svc.command(gid, st["revision"], f"t{turn}", "age_up", {})
            st = auto_resolve(svc, gid, st, f"t{turn}up")
        except Exception:
            break
        blob = json.dumps(clean(svc.get_state(gid)), sort_keys=True,
                          default=str)
        print(f"{turn} {hashlib.sha256(blob.encode()).hexdigest()}")
        turn += 1


class HashSeedDeterminismTest(unittest.TestCase):
    TURNS = int(os.environ.get("HASHSEED_TURNS", "12"))

    @unittest.skipIf(os.environ.get("SKIP_HASHSEED"),
                     "hash-seed subprocess test skipped (SKIP_HASHSEED set)")
    def test_same_seed_identical_life_under_three_hash_seeds(self):
        runs = {}
        for hs in ("0", "1", "42"):
            env = dict(os.environ, PYTHONHASHSEED=hs)
            proc = subprocess.run(
                [sys.executable, __file__, "--driver", str(self.TURNS)],
                capture_output=True, text=True, env=env, timeout=600)
            self.assertEqual(proc.returncode, 0,
                             f"driver failed under PYTHONHASHSEED={hs}:\n"
                             f"{proc.stderr[-2000:]}")
            lines = [l for l in proc.stdout.splitlines() if l.strip()]
            self.assertGreaterEqual(len(lines), min(self.TURNS, 8),
                                    f"life ended early under hash seed "
                                    f"{hs}: {len(lines)} turns")
            runs[hs] = lines
        base = runs["0"]
        for hs in ("1", "42"):
            self.assertEqual(len(runs[hs]), len(base),
                             f"life length differs under PYTHONHASHSEED="
                             f"{hs}: {len(runs[hs])} vs {len(base)} turns")
            self.assertEqual(runs[hs], base,
                             f"PYTHONHASHSEED={hs} diverges from seed 0 — "
                             f"set/dict iteration feeds the RNG:\n"
                             f"seed0: {base}\n"
                             f"seed{hs}: {runs[hs]}")


if __name__ == "__main__":
    if len(sys.argv) > 1 and sys.argv[1] == "--driver":
        sys.path.insert(0, os.path.dirname(os.path.dirname(
            os.path.abspath(__file__))))
        run_life(int(sys.argv[2]) if len(sys.argv) > 2 else 12)
    else:
        unittest.main()
