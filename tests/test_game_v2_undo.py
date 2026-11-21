"""v2 §Undo tests (Kimi A, 2026-11-20 — BACKLOG 'Undo last year').

Contract: docs/collab/redesign-v2/contracts.md §Undo — one level of undo,
full-envelope snapshot/restore (ledger, graph, knowledge, pendings, RNG),
replay-safe, no re-roll (anti-bypass / 18+ parity).

Forced runs over 3 seeds. Run:
    TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_game_v2_undo
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

# surfaces that must be byte-identical after undo (undo availability
# itself is expected to flip, so can_undo/available_actions are checked
# separately with the undo_year spec normalized)
SURFACES = ("stats", "money", "people", "journal", "journal_feed",
            "pending_decision", "education", "work", "health", "family",
            "finances", "status")


def normalize_actions(actions):
    return [{**a, "enabled": None, "reason": None} if a["id"] == "undo_year"
            else a for a in actions]


def make_service():
    return GameService(tempfile.mkdtemp(
        dir=os.path.expanduser("~/.lifesim-tmp")))


def new_game(svc, seed):
    return svc.new_game(
        {"first_name": "Ada", "last_name": "Lee", "gender": "female",
         "country": "Australia", "seed": seed}, f"u-{seed}")


def age_up_auto(svc, gid, rid):
    st = svc.get_state(gid)
    if st["pending_decision"]:
        d = st["pending_decision"]
        opts = sorted(d["options"],
                      key=lambda o: 0 if o.get("enabled", True) else 1)
        for o in opts:
            try:
                return svc.command(gid, st["revision"], rid,
                                   "resolve_decision",
                                   {"decision_id": d["id"],
                                    "option_id": o["id"]})
            except GameError as e:
                if e.code != "not_allowed":
                    raise
        raise AssertionError("no resolvable option")
    return svc.command(gid, st["revision"], rid, "age_up", {})


def pick_surface(st):
    out = {k: st[k] for k in SURFACES if k in st}
    out["available_actions"] = list(st["available_actions"])
    out["revision"] = st["revision"]
    return out


def settle_clean_year(svc, gid, seed, prefix):
    """Advance until an age_up starts with NO pending open and completes
    WITHOUT interruption. Returns (before, after) where `before` is the
    settled state == the undo snapshot of the completed year (nothing
    mutated between capture and the age_up, no resolves during it).
    Request ids are prefixed uniquely by the caller — reusing a
    request_id would replay a stored response instead of executing."""
    st = svc.get_state(gid)
    guard = 0
    while guard < 120:
        while st["pending_decision"] and guard < 120:
            d = st["pending_decision"]
            opts = sorted(d["options"],
                          key=lambda o: 0 if o.get("enabled", True) else 1)
            for o in opts:
                try:
                    st = svc.command(gid, st["revision"],
                                     f"{prefix}-c{guard}", "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": o["id"]})
                    break
                except GameError as e:
                    if e.code != "not_allowed":
                        raise
            guard += 1
            if st["status"] == "dead":
                return None, st
        if st["status"] == "dead":
            return None, st
        before = pick_surface(svc.get_state(gid))
        st = svc.command(gid, st["revision"], f"{prefix}-a{guard}",
                         "age_up", {})
        guard += 1
        if st["status"] == "dead":
            return None, st
        if st["pending_decision"] is None:
            return before, st
        # interrupted year: loop — the drain at the top resolves it and
        # the next age_up takes a fresh snapshot (undo here would replay
        # the identical interruption forever: the restored RNG
        # deterministically reproduces the same year)
    raise AssertionError("could not settle a clean year")


class CanUndoLifecycleTests(unittest.TestCase):
    def test_can_undo_and_advertised_spec(self):
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        self.assertEqual(st["can_undo"],
                         {"enabled": False,
                          "reason": "there is no year to undo"})
        spec = next(a for a in st["available_actions"]
                    if a["id"] == "undo_year")
        self.assertFalse(spec["enabled"])
        self.assertEqual(spec["reason"], st["can_undo"]["reason"])
        # undo with nothing -> typed conflict
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "u0", "undo_year", {})
        self.assertEqual(ctx.exception.code, "not_allowed")
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        self.assertEqual(st["can_undo"], {"enabled": True, "reason": None})
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        self.assertEqual(st["can_undo"],
                         {"enabled": False,
                          "reason": "only one year can be undone"})
        # one level only: second undo refused
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "u2", "undo_year", {})
        self.assertEqual(ctx.exception.code, "not_allowed")
        # a fresh age re-arms undo
        st = svc.command(gid, st["revision"], "a2", "age_up", {})
        self.assertTrue(st["can_undo"]["enabled"])


class FullRestoreTests(unittest.TestCase):
    def test_undo_restores_all_surfaces(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed)
            gid = st["game_id"]
            before, st = None, st
            for n in range(6):
                before, st = settle_clean_year(svc, gid, seed, f"pre{seed}-{n}")
                if st["status"] == "dead":
                    self.skipTest(f"seed {seed}: died early")
            st = svc.command(gid, st["revision"], f"undo-{seed}",
                             "undo_year", {})
            after = pick_surface(st)
            for key in SURFACES:
                self.assertEqual(
                    before[key], after[key],
                    f"seed {seed}: undo did not restore {key}")
            # actions identical except the undo_year availability itself
            self.assertEqual(
                normalize_actions(before["available_actions"]),
                normalize_actions(st["available_actions"]))
            self.assertEqual(st["can_undo"],
                             {"enabled": False,
                              "reason": "only one year can be undone"})
            # revision stays MONOTONIC across restoration (Codex undo
            # review): before R -> year R+1 -> undo response R+2, never
            # back to R+1
            self.assertEqual(st["revision"], before["revision"] + 2)


class DeterminismTests(unittest.TestCase):
    def test_redone_year_replays_identically(self):
        # anti-bypass: restoring the RNG stream means the redone year is
        # byte-identical — undo is never a re-roll (3+ gating parity)
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed)
            gid = st["game_id"]
            for i in range(5):
                st = age_up_auto(svc, gid, f"pre{seed}-{i}")
                if st["status"] == "dead":
                    self.skipTest(f"seed {seed}: died early")
            # the age-5 year ends with the guaranteed new_friend offer:
            # resolve it (first enabled option), then take one raw
            # age_up so the target year is uninterrupted by resolves
            while st["pending_decision"]:
                d = st["pending_decision"]
                opt = next(o for o in d["options"]
                           if o.get("enabled", True))
                st = svc.command(gid, st["revision"],
                                 f"pre{seed}-nf", "resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": opt["id"]})
            st = svc.command(gid, st["revision"], f"yr-{seed}", "age_up",
                             {})
            if st["status"] == "dead":
                self.skipTest(f"seed {seed}: died in the target year")
            age = st["people"][0]["age"]
            year_lines = [(e["text"], tuple(sorted(e["changes"].items())))
                          for e in st["journal"] if e["age"] == age]
            st = svc.command(gid, st["revision"], f"un-{seed}", "undo_year",
                             {})
            st = svc.command(gid, st["revision"], f"re-{seed}", "age_up", {})
            self.assertEqual(st["people"][0]["age"], age)
            redo_lines = [(e["text"], tuple(sorted(e["changes"].items())))
                          for e in st["journal"] if e["age"] == age]
            self.assertEqual(year_lines, redo_lines,
                             f"seed {seed}: redone year diverged — undo "
                             f"became a re-roll")


class ReplaySafetyTests(unittest.TestCase):
    def test_undo_retry_returns_stored_response(self):
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        # retry of the undo request_id: byte-identical stored response
        st2 = svc.command(gid, st["revision"] - 1, "u1", "undo_year", {})
        self.assertEqual(st, st2)

    def test_undone_age_request_reexecutes_identically(self):
        # the undone year's request_id left the ledger with the rollback:
        # replaying it re-executes the identical year (desired redo path)
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        first = [(e["text"], e["age"]) for e in st["journal"]]
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        # same request_id "a1", revision is back to the pre-year value
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        second = [(e["text"], e["age"]) for e in st["journal"]]
        self.assertEqual(first, second)
        self.assertTrue(st["can_undo"]["enabled"])

    def test_stale_revision_on_undo(self):
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, 9999, "u1", "undo_year", {})
        self.assertEqual(ctx.exception.code, "stale_revision")


class RuleActionRollbackTests(unittest.TestCase):
    def test_spend_time_undone_fully(self):
        svc = make_service()
        st = new_game(svc, 4242)
        gid = st["game_id"]
        for n in range(6):
            before, st = settle_clean_year(svc, gid, 4242, f"pre-{n}")
            if st["status"] == "dead":
                self.skipTest("died early")
        # a mid-year action belongs to the NEW year: undoing rolls back to
        # the pre-year snapshot, wiping the action with the boundary
        target = next(p for p in st["people"]
                      if p["role"] == "mother")
        spec = next((a for a in st["available_actions"]
                     if a["id"] == "spend_time"
                     and a.get("target_id") == target["id"]), None)
        if spec is None or not spec["enabled"]:
            self.skipTest("spend_time with mother unavailable this year")
        st = svc.command(gid, st["revision"], "sp1", "spend_time",
                         {"target_id": target["id"]})
        changed = st["journal"][-1]
        self.assertTrue(changed["changes"] or changed["text"])
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        # the action's mutations are gone; surfaces match the pre-year
        mother_before = next(p for p in before["people"]
                             if p["id"] == target["id"])
        mother = next(p for p in st["people"] if p["id"] == target["id"])
        self.assertEqual(mother.get("relationship"),
                         mother_before.get("relationship"))
        self.assertEqual(st["stats"], before["stats"])
        self.assertEqual(st["journal"], before["journal"])
        # availability matches the pre-year advertisement for that target
        spec_before = next(
            (a for a in before["available_actions"]
             if a["id"] == "spend_time"
             and a.get("target_id") == target["id"]), None)
        spec_after = next(
            (a for a in st["available_actions"]
             if a["id"] == "spend_time"
             and a.get("target_id") == target["id"]), None)
        if spec_before is not None and spec_after is not None:
            self.assertEqual(spec_after["enabled"], spec_before["enabled"])


class PendingOpenUndoTests(unittest.TestCase):
    def test_undo_while_pending_open(self):
        svc = make_service()
        st = new_game(svc, 90210)
        gid = st["game_id"]
        # settle first so the capture is mid-life, then raw-age until an
        # offer appears, capturing the state BEFORE the offering age_up
        for i in range(4):
            st = age_up_auto(svc, gid, f"pre{i}")
            if st["status"] == "dead":
                self.skipTest("died early")
        guard = 0
        while guard < 60:
            while st["pending_decision"]:
                d = st["pending_decision"]
                o = next(x for x in d["options"] if x.get("enabled", True))
                st = svc.command(gid, st["revision"], f"d{guard}",
                                 "resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": o["id"]})
            before = pick_surface(svc.get_state(gid))
            st = svc.command(gid, st["revision"], f"a{guard}", "age_up", {})
            guard += 1
            if st["status"] == "dead":
                self.skipTest("died before any pending")
            if st["pending_decision"] is not None:
                break
        else:
            self.skipTest("no pending arrived")
        self.assertIsNotNone(st["pending_decision"])
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        self.assertIsNone(st["pending_decision"])
        # the pending belonged to the undone year: state matches pre-age
        for key in ("stats", "money", "people", "journal"):
            self.assertEqual(before[key], st[key])


_EVOKED = []


def _evil_undo_payload():
    _EVOKED.append(True)


class _UndoPayload:
    def __reduce__(self):
        return (_evil_undo_payload, ())


class UndoCorrectionsTests(unittest.TestCase):
    """Codex undo-engine review (2026-11-20) corrections, frozen in
    contracts.md §Undo: monotonic revision, untouched ambient RNG,
    restricted snapshot loading, retry-vs-redo distinction."""

    def test_revision_monotonic_and_stale_clients_rejected(self):
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        self.assertEqual(st["revision"], 1)
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        self.assertEqual(st["revision"], 2)
        post_age_rev = st["revision"]
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        self.assertEqual(st["revision"], 3, "undo must not rewind revision")
        # a stale client holding the post-age revision 2 must go stale,
        # never mutate the restored timeline
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, post_age_rev, "late-1", "age_up", {})
        self.assertEqual(ctx.exception.code, "stale_revision")
        # also with an old request_id from the undone timeline + its
        # original expected revision: a delayed RETRY, not a redo
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, 1, "a1", "age_up", {})
        self.assertEqual(ctx.exception.code, "stale_revision")

    def test_revision_monotonic_across_multi_command_year(self):
        # a year with several commands (age + resolves) must never rewind
        svc = make_service()
        st = new_game(svc, 4242)
        gid = st["game_id"]
        guard = 0
        while guard < 60:
            before_rev = st["revision"]
            st = svc.command(gid, st["revision"], f"a{guard}", "age_up", {})
            guard += 1
            if st["status"] == "dead" or st["pending_decision"] is None:
                continue
            # interrupted: resolve at least once, then undo the year
            while st["pending_decision"]:
                d = st["pending_decision"]
                o = next(x for x in d["options"] if x.get("enabled", True))
                st = svc.command(gid, st["revision"], f"r{guard}",
                                 "resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": o["id"]})
            post_year_rev = st["revision"]
            self.assertGreater(post_year_rev, before_rev)
            st = svc.command(gid, st["revision"], "u1", "undo_year", {})
            self.assertEqual(st["revision"], post_year_rev + 1)
            # every revision in the undone window is now stale
            for stale_rev in range(before_rev + 1, post_year_rev + 1):
                with self.assertRaises(GameError) as ctx:
                    svc.command(gid, stale_rev, f"s{stale_rev}", "age_up",
                                {})
                self.assertEqual(ctx.exception.code, "stale_revision")
            return
        self.skipTest("no interrupted year arrived")

    def test_ambient_rng_untouched_by_undo(self):
        import random as _random
        import src.game.service as service_mod
        import src.appearance.integration as integration
        svc = make_service()
        st = new_game(svc, 90210)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        before_global = _random.getstate()
        before_cosmetic = service_mod._COSMETIC_RNG.getstate()
        before_app_rng = integration._APP_RNG
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        self.assertEqual(st["revision"], 3)
        self.assertEqual(_random.getstate(), before_global,
                         "undo changed the caller's global random state")
        self.assertEqual(service_mod._COSMETIC_RNG.getstate(),
                         before_cosmetic,
                         "undo changed the cosmetic RNG state")
        self.assertIs(integration._APP_RNG, before_app_rng,
                      "undo rebound the appearance RNG")

    def test_snapshot_deserialized_via_restricted_loader(self):
        # a nested snapshot carrying a disallowed global must be rejected
        # by SaveUnpickler WITHOUT invoking it
        import pickle
        _EVOKED.clear()
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        # tamper: replace the stored snapshot with a malicious pickle
        env = svc._load(gid)
        env["_undo_snapshot"] = pickle.dumps(_UndoPayload())
        svc._save(env)
        with self.assertRaises(GameError):
            svc.command(gid, env["revision"], "u1", "undo_year", {})
        self.assertFalse(_EVOKED, "disallowed global was invoked")
        # the game itself is untouched and still playable
        st = svc.get_state(gid)
        self.assertEqual(st["revision"], env["revision"])
        self.assertEqual(st["people"][0]["age"], 1)

    def test_redo_with_current_revision_replays_identically(self):
        # explicit redo: same request_id but the CURRENT revision is a
        # new operation, deterministic via the restored streams
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        first = [(e["text"], e["age"]) for e in st["journal"]]
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        st = svc.command(gid, st["revision"], "a1", "age_up", {})
        second = [(e["text"], e["age"]) for e in st["journal"]]
        self.assertEqual(first, second)
        self.assertEqual(st["revision"], 4)


    def test_death_undo_restores_alive_ledger_and_age(self):
        # forced: die (surrender double-confirm) -> undo -> alive again at
        # the previous age with the ledger/journal fully restored
        from src.menus import rule_actions
        if "surrender" not in getattr(rule_actions, "_SELF_ACTIONS", {}):
            self.skipTest("surrender action unavailable")
        svc = make_service()
        st = svc.new_game({"first_name": "Mara", "last_name": "Voss",
                           "gender": "female", "country": "Australia",
                           "seed": 61}, "death-u")
        gid = st["game_id"]
        # settle clean years until the surrender age gate passes
        for n in range(40):
            if st["status"] == "dead":
                self.skipTest("died before surrender age")
            if st["people"][0]["age"] >= 18 and "surrender" in {
                    a["id"] for a in st["available_actions"]}:
                break
            before, st = settle_clean_year(svc, gid, 61, f"du-pre-{n}")
        else:
            self.skipTest("surrender never became available")
        # capture the pre-year surfaces — settle returned `before`, which
        # is exactly the snapshot undo restores (the surrender resolves
        # inside the same year boundary). Keep the pre-surrender envelope
        # to compare the raw ledger against the snapshot later.
        import io
        from src.lifesim_lib.safe_save import load_save
        age_before = before["people"][0]["age"]
        money_before = before["money"]
        journal_before = [(e["kind"], e["text"]) for e in before["journal"]]
        finances_before = before["finances"]
        env_pre = svc._load(gid)
        snap_ledger = load_save(io.BytesIO(env_pre["_undo_snapshot"]))[
            "ledger"]
        # die via the two-step surrender
        st = svc.command(gid, st["revision"], "s1", "surrender", {})
        while st["status"] != "dead":
            d = st["pending_decision"]
            opt = next((o for o in d["options"]
                        if o["id"] in ("ask_final", "surrender")),
                       d["options"][-1])
            st = svc.command(gid, st["revision"], f"s{d['id']}",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id": opt["id"]})
        self.assertEqual(st["status"], "dead")
        self.assertEqual([a["id"] for a in st["available_actions"]],
                         ["undo_year"])
        # undo the year of death
        st = svc.command(gid, st["revision"], "u1", "undo_year", {})
        self.assertEqual(st["status"], "alive")
        self.assertEqual(st["people"][0]["age"], age_before)
        self.assertEqual(st["money"], money_before)
        self.assertEqual(st["finances"], finances_before)
        self.assertEqual([(e["kind"], e["text"]) for e in st["journal"]],
                         journal_before)
        # the RAW ledger is restored entry-for-entry against the snapshot
        env = svc._load(gid)
        self.assertEqual(env.get("ledger", []), snap_ledger,
                         "ledger not restored by undo")
        # one undo only: no further undo, and no death entry anymore
        self.assertEqual(st["can_undo"],
                         {"enabled": False,
                          "reason": "only one year can be undone"})
        self.assertNotIn("death", [e["kind"] for e in st["journal"]])


if __name__ == "__main__":
    unittest.main()
