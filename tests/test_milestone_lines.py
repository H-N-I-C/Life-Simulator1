"""Milestone journal line variants (Kimi C, BACKLOG polish).

The legacy milestone strings in rule_actions.py were flat and
inconsistent; they now draw 2-4 original first-person variants through
C's frozen pick_year_line. These tests pin: pool shape, 2-4 variants
per situation with the fact kept, band coverage, selector integration,
no-repeat on the player-persisted window, cosmetic-stream purity, and
REAL age_up milestones (ages 6/12/14/17) journaling from the right
pools with no second-person text.

Run: python3 -m unittest tests.test_milestone_lines
"""

import os
import random
import shutil
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

os.environ.setdefault("TMPDIR", os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(os.environ["TMPDIR"], exist_ok=True)

from src.game.content.journal import milestone_lines  # noqa: E402
from src.game.content.journal.yearly_lines import BANDS, pick_year_line  # noqa: E402
from src.game.service import GameService  # noqa: E402
from src.menus import rule_actions  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}

# the fact each situation must keep (substring of every variant, lower)
FACT_KEYWORDS = {
    "start_elementary": ["elementary school"],
    "start_middle": ["middle school"],
    "start_high": ["high school"],
    "graduate_high": ["high school"],  # completion implied by the
    # milestone itself ("finished/graduated" both keep the fact)
    "enroll_university": ["universit"],
    "graduate_university": ["graduated", "universit"],
    "expelled_university": ["expelled", "universit"],
    "loan_repayment": ["loan"],
    "university_rejected": ["universit", "reject"],
    "scholarship_rejected": ["scholarship", "reject"],
    "parents_refused": ["refus"],
    "funding_refused": ["refus"],
}


class PoolTests(unittest.TestCase):
    def test_shape_and_two_to_four_variants(self):
        for name in milestone_lines.situations():
            texts = {l["text"] for l in milestone_lines.lines_for(name)}
            self.assertGreaterEqual(len(texts), 2, name)
            self.assertLessEqual(len(texts), 4, name)
            for line in milestone_lines.lines_for(name):
                self.assertIn(line["band"], BANDS)
                self.assertTrue(line["text"].strip())
                self.assertTrue(line["text"].endswith((".", "!", '."')),
                                "period missing: %r" % line["text"])

    def test_fact_kept_in_every_variant(self):
        for name, keywords in FACT_KEYWORDS.items():
            for text in {l["text"]
                         for l in milestone_lines.lines_for(name)}:
                low = text.lower()
                for kw in keywords:
                    self.assertIn(kw, low,
                                  "%s lost its fact %r: %r"
                                  % (name, kw, text))

    def test_first_person_no_second_person(self):
        for name in milestone_lines.situations():
            for text in {l["text"]
                         for l in milestone_lines.lines_for(name)}:
                low = text.lower()
                self.assertFalse(low.startswith("your "),
                                 "%s: %r" % (name, text))
                self.assertTrue(
                    any(m in low for m in ("i ", " i ", "my ", " me ")),
                    "not first person: %r" % text)

    def test_twelve_situations(self):
        self.assertEqual(len(milestone_lines.situations()), 12)


class SelectorAndHelperTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-milestone-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-c")
        self.gid = st["game_id"]

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def _player(self):
        env = self.svc._load(self.gid)
        env["player"].age = 20
        self.svc._save(env)
        return self.svc._load(self.gid)["player"]

    def test_helper_variants_and_window(self):
        player = self._player()
        seen = set()
        for _ in range(4):
            text = rule_actions._milestone_line(player, "start_middle")
            self.assertIn(text, {l["text"] for l in
                                 milestone_lines.lines_for("start_middle")})
            seen.add(text)
        self.assertEqual(len(seen), 4, "four draws, four variants")
        # window persisted on the player object (envelope-riding)
        self.assertTrue(getattr(player, "_milestone_line_recents", []))

    def test_helper_never_touches_gameplay_stream(self):
        player = self._player()
        before = random.getstate()
        rule_actions._milestone_line(player, "enroll_university")
        self.assertEqual(before, random.getstate())

    def test_pick_year_line_pool_integration(self):
        rng = random.Random(4)
        pool = milestone_lines.lines_for("reconciled") \
            if "reconciled" in milestone_lines.situations() \
            else milestone_lines.lines_for("graduate_high")

        class P:
            age = 30
        recent = []
        for _ in range(3):
            picked = pick_year_line(P(), None, recent, rng, pool=pool)
            self.assertIsNotNone(picked)
            recent.append(picked[0])


class RealAgeUpTests(unittest.TestCase):
    def test_school_milestones_journal_from_pools(self):
        d = tempfile.mkdtemp(prefix="lifesim-msl-ageup-")
        svc = GameService(d)
        st = svc.new_game(dict(PROFILE), "ageup")
        gid = st["game_id"]
        pool_texts = {
            6: {l["text"] for l in
                milestone_lines.lines_for("start_elementary")},
            12: {l["text"] for l in
                 milestone_lines.lines_for("start_middle")},
            14: {l["text"] for l in
                 milestone_lines.lines_for("start_high")},
            17: {l["text"] for l in
                 milestone_lines.lines_for("graduate_high")},
        }
        seen = {6: False, 12: False, 14: False, 17: False}
        age = 0
        req = 0
        try:
            for _ in range(18):
                st = svc.get_state(gid)
                while st.get("pending_decision"):
                    pend = st["pending_decision"]
                    options = [o for o in pend["options"]
                               if o.get("enabled", True)
                               and not o.get("known_costs")]
                    choice = options[0] if options else pend["options"][-1]
                    st = svc.command(
                        gid, st["revision"], "r%d" % req,
                        "resolve_decision",
                        {"decision_id": pend["id"],
                         "option_id": choice["id"], "inputs": {}})
                    req += 1
                st = svc.command(gid, st["revision"], "a%d" % _,
                                 "age_up", {})
                age += 1
                env = svc._load(gid)
                for entry in env["journal"]:
                    if entry.get("kind") != "education":
                        continue
                    text = entry["text"]
                    self.assertFalse(text.lower().startswith("your "),
                                     text)
                    if age in pool_texts and text in pool_texts[age]:
                        seen[age] = True
        finally:
            shutil.rmtree(d, ignore_errors=True)
        self.assertTrue(seen[6], "elementary milestone not journaled "
                                 "from the pool")
        self.assertTrue(seen[12])
        self.assertTrue(seen[14])
        self.assertTrue(seen[17])


if __name__ == "__main__":
    unittest.main()
