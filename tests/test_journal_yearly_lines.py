"""Yearly journal flavour lines (C content, redesign-v2 F4): pool sanity,
age-band fit, condition selection, no-repeat guarantee, determinism, and
the env wrapper's recents persistence.

Run: python3 -m unittest tests.test_journal_yearly_lines
"""

import os
import random
import re
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

os.environ.setdefault("TMPDIR", os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(os.environ["TMPDIR"], exist_ok=True)

from src.game.content.journal import yearly_lines as yl  # noqa: E402
from src.lifesim_lib.lifesim_lib import Gender  # noqa: E402

BAND_MIN = {"baby": 15, "toddler": 20, "child": 35, "teen": 25,
            "adult": 25, "elder": 20}


class FakeRel:
    def __init__(self, cls, relationship, gender=None):
        self.__class__ = type(cls, (), {})
        self.relationship = relationship
        self.gender = gender
        self.alive = True


class FakePlayer:
    def __init__(self, age=10, happiness=70, health=70, smarts=70,
                 looks=70):
        self.age = age
        self.happiness = happiness
        self.health = health
        self.smarts = smarts
        self.looks = looks
        self.relations = []
        self.children = []
        self.partner = None
        self.illnesses = []
        self.dropped_out = False


CONTEXT = {"education": {"stage": "elementary", "enrolled": True,
                         "grades": 70, "dropped_out": False},
           "work": {"employed": False, "retired": False}}


class PoolSanityTests(unittest.TestCase):
    def test_at_least_150_lines(self):
        self.assertGreaterEqual(len(yl.LINES), 150)

    def test_ids_unique(self):
        ids = [l["id"] for l in yl.LINES]
        self.assertEqual(len(ids), len(set(ids)), "duplicate line ids")

    def test_all_bands_covered_with_minimums(self):
        counts = {}
        for l in yl.LINES:
            counts[l["band"]] = counts.get(l["band"], 0) + 1
        for band, minimum in BAND_MIN.items():
            self.assertGreaterEqual(counts.get(band, 0), minimum, band)

    def test_schema_and_text_quality(self):
        for l in yl.LINES:
            self.assertIn(l["band"], yl.BANDS, l["id"])
            self.assertIsInstance(l["text"], str)
            text = l["text"]
            self.assertTrue(8 <= len(text) <= 220, (l["id"], len(text)))
            self.assertNotIn("{", text, l["id"])
            self.assertNotIn("}", text, l["id"])
            self.assertIsNone(re.search(r"\s{2,}", text), l["id"])
            when = l.get("when")
            if when is not None:
                self.assertIsInstance(when, dict, l["id"])
                for group in ("all", "any"):
                    for cond in when.get(group) or []:
                        self.assertTrue(
                            any(k in cond for k in
                                ("stat", "flag", "school", "role_relation")),
                            (l["id"], cond))

    def test_stats_referenced_exist(self):
        for l in yl.LINES:
            for group in ("all", "any"):
                for cond in (l.get("when") or {}).get(group) or []:
                    if "stat" in cond:
                        self.assertIn(cond["stat"],
                                      ("happiness", "health", "smarts",
                                       "looks"), l["id"])


class AgeBandFitTests(unittest.TestCase):
    def test_band_boundaries(self):
        cases = [(0, "baby"), (1, "baby"), (2, "toddler"), (4, "toddler"),
                 (5, "child"), (12, "child"), (13, "teen"), (17, "teen"),
                 (18, "adult"), (64, "adult"), (65, "elder"), (99, "elder")]
        for age, band in cases:
            self.assertEqual(yl.band_for_age(age), band, age)

    def test_picks_only_from_the_right_band(self):
        rng = random.Random(1234)
        for age in range(0, 100):
            player = FakePlayer(age=age)
            recent = []
            for _ in range(6):
                picked = yl.pick_year_line(player, CONTEXT, recent, rng)
                self.assertIsNotNone(picked, age)
                line_id, text = picked
                band = yl.band_for_age(age)
                matches = [l for l in yl.LINES if l["id"] == line_id]
                self.assertEqual(matches[0]["band"], band, (age, line_id))
                self.assertTrue(text.strip())
                recent.append(line_id)


class SelectionTests(unittest.TestCase):
    def test_conditions_gate_selection(self):
        # smarts-gated line only for smart players
        smart = FakePlayer(age=10, smarts=85)
        dumb = FakePlayer(age=10, smarts=20)
        rng = random.Random(7)
        hits = set()
        for i in range(60):
            picked = yl.pick_year_line(smart, CONTEXT, [], rng)
            hits.add(picked[0])
        gated = {l["id"] for l in yl.LINES
                 if l["band"] == "child"
                 if any(c.get("stat") == "smarts" and c.get("min", 0) >= 72
                        for c in (l.get("when") or {}).get("all") or [])}
        self.assertTrue(gated & hits, "high-smarts lines should be pickable")
        low_hits = set()
        for i in range(60):
            low_hits.add(yl.pick_year_line(dumb, CONTEXT, [], rng)[0])
        self.assertFalse(gated & low_hits,
                         "gated lines must not leak for low smarts")

    def test_school_condition(self):
        teen = FakePlayer(age=15)
        ctx_work = {"education": {"stage": "none", "enrolled": False},
                    "work": {"employed": False}}
        rng = random.Random(11)
        hits = {yl.pick_year_line(teen, ctx_work, [], rng)[0]
                for _ in range(80)}
        high_only = {l["id"] for l in yl.LINES
                     if l["band"] == "teen"
                     and any("school" in c
                             for c in (l.get("when") or {}).get("all") or [])}
        self.assertFalse(high_only & hits,
                         "school-gated lines must not fire when not enrolled")

    def test_unemployed_flag(self):
        adult = FakePlayer(age=30)
        ctx = {"education": {"stage": "none"},
               "work": {"employed": True}}
        rng = random.Random(5)
        hits = {yl.pick_year_line(adult, ctx, [], rng)[0] for _ in range(120)}
        unemp_only = {l["id"] for l in yl.LINES
                      if l["band"] == "adult"
                      and any(c.get("flag") == "unemployed"
                              for c in (l.get("when") or {}).get("all") or [])}
        self.assertFalse(unemp_only & hits)

    def test_deterministic_for_same_seed(self):
        p = FakePlayer(age=8)
        r1 = yl.pick_year_line(p, CONTEXT, [], random.Random(99))
        r2 = yl.pick_year_line(p, CONTEXT, [], random.Random(99))
        self.assertEqual(r1, r2)

    def test_no_rng_no_line(self):
        self.assertIsNone(yl.pick_year_line(FakePlayer(age=8), CONTEXT,
                                            [], None))


class NoRepeatTests(unittest.TestCase):
    def test_no_repeat_until_pool_exhausted(self):
        # Draw a band until the selector runs dry: no id may repeat, and
        # the pool must last roughly as long as the band is deep.
        rng = random.Random(2024)
        for age, minimum in ((3, 15), (9, 25), (15, 20), (30, 18),
                             (70, 15)):
            recent = []
            seen = []
            for _ in range(60):
                picked = yl.pick_year_line(FakePlayer(age=age), CONTEXT,
                                           recent, rng)
                if picked is None:
                    break
                seen.append(picked[0])
                recent.append(picked[0])
            self.assertGreaterEqual(len(seen), minimum, age)
            self.assertEqual(len(seen), len(set(seen)),
                             "repeat at age %d: %r" % (age, seen))

    def test_consecutive_years_never_repeat(self):
        rng = random.Random(31337)
        recent = []
        prev = None
        for age in range(2, 60):
            picked = yl.pick_year_line(FakePlayer(age=age), CONTEXT,
                                       recent, rng)
            if picked is None:
                continue          # band pool legitimately drained
            self.assertNotEqual(picked[0], prev,
                                "consecutive repeat: %s" % picked[0])
            prev = picked[0]
            recent.append(picked[0])


class EnvWrapperTests(unittest.TestCase):
    def _env(self, age=10):
        return {"player": FakePlayer(age=age),
                "journal_line_recents": []}

    def test_wrapper_persists_recents_and_returns_text(self):
        rng = random.Random(2)
        env = self._env()
        texts = []
        for _ in range(10):
            text = yl.year_journal_line(env, CONTEXT, rng)
            self.assertIsInstance(text, str)
            texts.append(text)
        self.assertEqual(len(set(texts)), len(texts), "no repeated texts")
        self.assertEqual(len(env["journal_line_recents"]), 10)
        self.assertLessEqual(len(env["journal_line_recents"]),
                             yl.RECENT_WINDOW)

    def test_wrapper_caps_recents_window(self):
        rng = random.Random(3)
        env = self._env(age=30)
        for _ in range(80):
            yl.year_journal_line(env, CONTEXT, rng)
        self.assertLessEqual(len(env["journal_line_recents"]),
                             yl.RECENT_WINDOW)

    def test_distinct_bands_never_share_recents_conflicts(self):
        rng = random.Random(4)
        env = self._env(age=6)
        yl.year_journal_line(env, CONTEXT, rng)   # child pick
        env["player"].age = 70                     # elder pick
        elder = yl.year_journal_line(env, CONTEXT, rng)
        self.assertIsInstance(elder, str)


if __name__ == "__main__":
    unittest.main()
