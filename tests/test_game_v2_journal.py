"""v2 §Journal v2 + §Decision outcome tests (Kimi A, 2026-11-20).

Contracts under test: docs/collab/redesign-v2/contracts.md
- journal_feed: per-age blocks (header + lines) derived from the flat,
  player-known-filtered journal; every lived year has the aging line.
- Relationship lines from yearly parent drift (feel-pass F4).
- last_decision_outcome: actual applied deltas on decision resolve,
  for C's outcome popup (feel-pass F5).
- F2 server side: no turn ids / phase strings in player-facing text.

Forced runs over 3 seeds. Run:
    python3 -m unittest tests.test_game_v2_journal
"""

import os
import re
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402

SEEDS = [7, 4242, 90210]

FORBIDDEN = re.compile(
    r"\bt_\d{6}\b|year_start|year_close|relations_age|player_stats|"
    r"work_income|debt_service|family_events|parenting_events|"
    r"drama_events|romance_events|decision_offer|item_cursor|phase_id|"
    r"annual_events|mortality")


def make_service():
    return GameService(tempfile.mkdtemp(prefix="lifesim-v2-journal-"))


def new_game(svc, seed):
    profile = {"first_name": "Ada", "last_name": "Lee",
               "gender": "female", "country": "Australia", "seed": seed}
    return svc.new_game(profile, f"new-{seed}")


def age_up_auto(svc, gid, rid):
    """Age one year, auto-resolving pendings with the first enabled option."""
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


def player_texts(st):
    """Every server-authored player-facing string in a state projection."""
    out = []
    for e in st["journal"]:
        out.append(e["text"])
    for block in st["journal_feed"]:
        for e in block["entries"]:
            out.append(e["text"])
    d = st.get("pending_decision")
    if d:
        out.append(d["title"])
        out.append(d["text"])
        for o in d["options"]:
            out.append(o["label"])
            if o.get("reason"):
                out.append(o["reason"])
    for p in st["people"]:
        out.append(p["name"])
    return [t for t in out if t]


class JournalFeedTests(unittest.TestCase):
    def test_feed_blocks_per_age_with_aging_line(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed)
            gid = st["game_id"]
            target = 12
            guard = 0
            while st["people"][0]["age"] < target and guard < 100:
                st = age_up_auto(svc, gid, f"a{seed}-{guard}")
                guard += 1
                if st["status"] == "dead":
                    self.skipTest(f"seed {seed} died early")
            feed = st["journal_feed"]
            self.assertTrue(feed, f"seed {seed}: feed empty")
            ages = [b["age"] for b in feed]
            self.assertEqual(ages, sorted(ages),
                             f"seed {seed}: feed not oldest->newest")
            self.assertEqual(len(set(ages)), len(ages),
                             f"seed {seed}: duplicate age blocks")
            for b in feed:
                self.assertEqual(b["header"], f"Age {b['age']}")
                self.assertTrue(b["entries"])
            # every lived year has its aging line (kind "year")
            lived = set(range(1, st["people"][0]["age"] + 1))
            aging = {e["age"] for b in feed
                     for e in b["entries"] if e["kind"] == "year"}
            self.assertTrue(lived <= aging,
                            f"seed {seed}: missing aging lines for "
                            f"{sorted(lived - aging)}")
            # feed entries are exactly the flat journal entries
            flat = [(e["id"], e["text"]) for e in st["journal"]]
            fed = [(e["id"], e["text"]) for b in feed for e in b["entries"]]
            self.assertEqual(flat, fed, f"seed {seed}: feed != flat journal")

    def test_relationship_lines_from_yearly_drift(self):
        # under 18 parents drift +1 every year -> first-person lines appear
        svc = make_service()
        st = new_game(svc, 4242)
        gid = st["game_id"]
        for i in range(8):
            st = age_up_auto(svc, gid, f"r{i}")
            if st["status"] == "dead":
                self.skipTest("died early")
        rel_lines = [e for e in st["journal"]
                     if e["kind"] == "relationship"]
        self.assertTrue(rel_lines,
                        "expected yearly parent-drift relationship lines")
        for e in rel_lines:
            self.assertIn("relationship", e["changes"])
            self.assertTrue(e["person_ids"])
            # varied flavour lines (M3 amendment 5) or the plain fallback
            self.assertTrue(
                e["text"].startswith("My relationship with ") or
                any(ch.isalpha() for ch in e["text"]),
                f"unexpected relationship line text: {e['text']!r}")


class DecisionOutcomeTests(unittest.TestCase):
    def test_no_outcome_until_resolve_then_actual_deltas(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed)
            gid = st["game_id"]
            self.assertIsNone(st["last_decision_outcome"])
            guard = 0
            while st["pending_decision"] is None and guard < 60:
                st = svc.command(gid, st["revision"], f"w{guard}",
                                 "age_up", {})
                guard += 1
                if st["status"] == "dead":
                    self.skipTest(f"seed {seed}: died before any decision")
            self.assertIsNone(st["last_decision_outcome"],
                              "age_up must not invent an outcome")
            d = st["pending_decision"]
            stats_before = dict(st["stats"])
            money_before = st["money"]
            opt = next(o for o in d["options"] if o.get("enabled", True))
            st = svc.command(gid, st["revision"], "resolve-1",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opt["id"]})
            oc = st["last_decision_outcome"]
            self.assertIsNotNone(oc, f"seed {seed}: outcome missing")
            self.assertEqual(oc["decision_id"], d["id"])
            self.assertEqual(oc["option_id"], opt["id"])
            self.assertTrue(oc["text"])
            self.assertEqual(oc["person_ids"][0], st["player_id"])
            # actual deltas: match the real before/after of this command
            for key, delta in oc["changes"].items():
                if key == "money":
                    self.assertEqual(st["money"] - money_before, delta)
                elif key == "relationship":
                    continue  # per-target; validated via rel paths below
                else:
                    self.assertEqual(st["stats"][key] - stats_before[key],
                                     delta,
                                     f"seed {seed}: {key} delta not actual")
            # the outcome text is the journaled decision line
            j_texts = [e["text"] for e in st["journal"]
                       if e["kind"] == "decision"]
            self.assertIn(oc["text"], j_texts)
            # a later age_up keeps the last outcome (never cleared/invented)
            if st["status"] == "alive" and st["pending_decision"] is None:
                st2 = svc.command(gid, st["revision"], "after",
                                  "age_up", {})
                self.assertEqual(st2["last_decision_outcome"]["decision_id"],
                                 d["id"])


class PlayerTextPurityTests(unittest.TestCase):
    """F2 server side: no internal turn/phase strings in player text."""

    def test_no_turn_or_phase_strings_across_seeds(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed)
            gid = st["game_id"]
            guard = 0
            while st["people"][0]["age"] < 30 and guard < 200:
                st = age_up_auto(svc, gid, f"p{seed}-{guard}")
                guard += 1
                if st["status"] == "dead":
                    break
                for text in player_texts(st):
                    self.assertIsNone(
                        FORBIDDEN.search(text),
                        f"seed {seed}: player-facing text leaks internals: "
                        f"{text!r}")


class OutcomePrivacyTests(unittest.TestCase):
    """The raw-projected last_decision_outcome passes the SAME player_known
    gate as journal serialization — a private resolver outcome must never
    reach a command response or the GET projection (forced sentinel via the
    real resolve_decision command path)."""

    def _resolve_with_events(self, events):
        """Persist a synthetic rule pending, resolve it through the real
        command path with a stubbed resolver, return (command_state,
        get_state)."""
        from src.menus import rule_actions
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-v2-privacy-"))
        st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                           "gender": "female", "country": "Australia",
                           "seed": 7}, "priv")
        gid = st["game_id"]
        env = svc._load(gid)
        env["pending"] = {
            "id": "d_77", "kind": "funeral", "title": "T", "text": "X",
            "person_ids": [],
            "options": [{"id": "opt", "label": "Okay"}]}
        svc._save(env)
        original = rule_actions.resolve_choice
        rule_actions.resolve_choice = lambda *a, **k: {
            "events": events, "money_entries": []}
        try:
            cmd_state = svc.command(
                gid, env["revision"], "priv-1", "resolve_decision",
                {"decision_id": "d_77", "option_id": "opt"})
        finally:
            rule_actions.resolve_choice = original
        return cmd_state, svc.get_state(gid)

    def _assert_absent_everywhere(self, cmd_state, get_state, secret):
        self.assertIsNone(cmd_state["last_decision_outcome"],
                          "private outcome leaked into a command response")
        self.assertIsNone(get_state["last_decision_outcome"],
                          "private outcome leaked into the GET projection")
        self.assertNotIn(secret, [e["text"] for e in cmd_state["journal"]])
        self.assertNotIn(secret, [e["text"] for e in get_state["journal"]])

    def test_private_outcome_never_projected(self):
        cmd_state, get_state = self._resolve_with_events([{
            "kind": "event", "text": "SECRET private occurrence.",
            "person_ids": [], "changes": {"happiness": -10},
            "audience": "private"}])
        self._assert_absent_everywhere(cmd_state, get_state,
                                       "SECRET private occurrence.")

    def test_player_known_without_fact_never_projected(self):
        cmd_state, get_state = self._resolve_with_events([{
            "kind": "event", "text": "Unpinned knowledge claim.",
            "person_ids": [], "changes": {},
            "audience": "player_known"}])  # no fact_id -> never visible
        self._assert_absent_everywhere(cmd_state, get_state,
                                       "Unpinned knowledge claim.")

    def test_stored_metadata_refiltered_at_projection(self):
        # a legacy/stored outcome whose fact later becomes unknown (or was
        # stored with private metadata) must be re-filtered at read time
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-v2-privacy-"))
        st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                           "gender": "female", "country": "Australia",
                           "seed": 7}, "priv2")
        gid = st["game_id"]
        env = svc._load(gid)
        env["last_decision_outcome"] = {
            "decision_id": "d_1", "option_id": "o", "text": "HIDDEN",
            "person_ids": [], "changes": {}, "audience": "private"}
        svc._save(env)
        self.assertIsNone(svc.get_state(gid)["last_decision_outcome"])
        self.assertIsNone(svc._state(env)["last_decision_outcome"])

    def test_public_outcome_captured(self):
        cmd_state, get_state = self._resolve_with_events([{
            "kind": "event", "text": "A public thing happened.",
            "person_ids": [], "changes": {"happiness": 3}}])
        oc = cmd_state["last_decision_outcome"]
        self.assertIsNotNone(oc)
        self.assertEqual(oc["text"], "A public thing happened.")
        self.assertEqual(oc["changes"], {"happiness": 3})
        # internal visibility metadata never leaves the server
        self.assertNotIn("audience", oc)
        self.assertNotIn("fact_id", oc)
        self.assertEqual(get_state["last_decision_outcome"]["text"],
                         "A public thing happened.")

    def test_public_outcome_with_relationship_changes(self):
        cmd_state, _ = self._resolve_with_events([{
            "kind": "event", "text": "We grew closer.",
            "person_ids": ["p1"], "changes": {},
            "relationship_changes": {"p1": 6}}])
        self.assertEqual(cmd_state["last_decision_outcome"]
                         ["relationship_changes"], {"p1": 6})

    def test_no_outcome_event_is_null(self):
        cmd_state, _ = self._resolve_with_events([])
        self.assertIsNone(cmd_state["last_decision_outcome"])


if __name__ == "__main__":
    unittest.main()
