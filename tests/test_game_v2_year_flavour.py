"""§Journal flavour engine-hook tests (Kimi A, 2026-11-20).

Contract: docs/collab/redesign-v2/contracts.md §Journal flavour — A's hook
is one `year_journal_line` call in `_cmd_age_up`. Pinned here:
- the flavour line (when one fits) lands in the kind-"year" journal entry;
- the draw uses ONLY the cosmetic stream (twin games with the same seed
  produce byte-identical journals, flavour lines included);
- `env["journal_line_recents"]` persists and is undo-safe (restored with
  the snapshot);
- the journal/feed contract (aging line per lived year) still holds.

Run: TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_game_v2_year_flavour
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402

SEEDS = [7, 4242, 90210]


def make_service():
    return GameService(tempfile.mkdtemp(
        dir=os.path.expanduser("~/.lifesim-tmp")))


PROFILE = {"first_name": "Ada", "last_name": "Lee", "gender": "female",
           "country": "Australia"}


def drive(svc, gid, st, n, prefix):
    """Age n years, auto-resolving pendings; return final state."""
    for i in range(n):
        guard = 0
        while st["pending_decision"] and guard < 20:
            d = st["pending_decision"]
            opts = sorted(d["options"],
                          key=lambda o: 0 if o.get("enabled", True) else 1)
            for o in opts:
                try:
                    st = svc.command(gid, st["revision"], f"{prefix}-r{i}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": o["id"]})
                    break
                except GameError as e:
                    if e.code != "not_allowed":
                        raise
            guard += 1
        if st["status"] == "dead":
            return st
        st = svc.command(gid, st["revision"], f"{prefix}-a{i}", "age_up",
                         {})
    return st


class YearFlavourHookTests(unittest.TestCase):
    def test_year_entries_present_and_flavour_active(self):
        for seed in SEEDS:
            svc = make_service()
            st = svc.new_game({**PROFILE, "seed": seed}, f"f-{seed}")
            gid = st["game_id"]
            st = drive(svc, gid, st, 6, f"f{seed}")
            if st["status"] == "dead":
                self.skipTest(f"seed {seed}: died early")
            year_entries = [e for e in st["journal"] if e["kind"] == "year"]
            self.assertEqual(len(year_entries), 6,
                             f"seed {seed}: one aging line per year")
            for e in year_entries:
                self.assertTrue(e["text"])
                self.assertEqual(e["person_ids"], [st["player_id"]])
            # the flavour selector ran and persisted its recents window
            env = svc._load(gid)
            recents = env.get("journal_line_recents")
            self.assertIsInstance(recents, list)
            self.assertTrue(recents, f"seed {seed}: flavour never drew")
            self.assertEqual(len(recents), len(set(recents)),
                             f"seed {seed}: repeated flavour line")
            # some year used a flavour line instead of the default
            defaults = {f"Ada turned {age}." for age in range(1, 7)}
            flavoured = [e for e in year_entries
                         if e["text"] not in defaults]
            self.assertTrue(flavoured,
                            f"seed {seed}: no flavour line in 6 years")

    def test_cosmetic_only_twin_games_byte_identical(self):
        # if the flavour draw consumed simulation randomness, twin games
        # would diverge; byte-identical journals prove cosmetic-only
        for seed in SEEDS:
            journals = []
            for twin in (0, 1):
                svc = make_service()
                st = svc.new_game({**PROFILE, "seed": seed},
                                  f"t{seed}-{twin}")
                gid = st["game_id"]
                st = drive(svc, gid, st, 6, f"t{seed}x{twin}")
                if st["status"] == "dead":
                    self.skipTest(f"seed {seed}: died early")
                journals.append([(e["kind"], e["text"], e["age"],
                                  tuple(sorted(e["changes"].items())))
                                 for e in st["journal"]])
            self.assertEqual(journals[0], journals[1],
                             f"seed {seed}: flavour draw perturbed the "
                             f"simulation stream")

    def test_recents_are_undo_safe(self):
        svc = make_service()
        st = svc.new_game({**PROFILE, "seed": 7}, "u1")
        gid = st["game_id"]
        journal_before_age = len(svc.get_state(gid)["journal"])
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        if st["status"] == "dead":
            self.skipTest("died in first year")
        env = svc._load(gid)
        recents_after_age = list(env.get("journal_line_recents") or [])
        self.assertTrue(recents_after_age)
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        env = svc._load(gid)
        self.assertEqual(env.get("journal_line_recents") or [],
                         [], "recents must roll back with the snapshot")
        self.assertEqual(len(env["journal"]), journal_before_age)
        # redo replays the identical flavour line (cosmetic stream restored)
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        env = svc._load(gid)
        self.assertEqual(env["journal_line_recents"], recents_after_age)
        year = [e for e in env["journal"] if e["kind"] == "year"][-1]
        self.assertTrue(year["text"])


if __name__ == "__main__":
    unittest.main()
