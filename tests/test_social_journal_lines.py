"""Tests for the M4 social journal flavour pools (Kimi C).

Pins: pool shape for pick_year_line's pool= argument, >=60 distinct
original texts across the nine drift situations, band coverage,
{name}-placeholder formatting, no-repeat through the frozen selector,
and the fact-audit basics (no unrecorded milestone claims).

Run: python3 -m unittest tests.test_social_journal_lines
"""

import random
import re
import unittest

from src.game.content.journal import social_lines
from src.game.content.journal.yearly_lines import BANDS, pick_year_line


class PoolShapeTests(unittest.TestCase):
    def test_nine_situations_covered(self):
        self.assertEqual(set(social_lines.situations()), {
            "friend_made", "friendship_grew", "best_friend",
            "drifting_apart", "falling_out", "reconciled", "rival",
            "reunion", "coworker_friend"})

    def test_shape_and_bands(self):
        for name in social_lines.situations():
            pool = social_lines.lines_for(name)
            self.assertGreaterEqual(len(pool), 24)  # texts x 4 bands
            # (child..elder — no social graph at 0-4)
            for line in pool:
                self.assertIn("id", line)
                self.assertIn(line["band"], BANDS)
                self.assertTrue(line["text"].strip())
                self.assertIn("{name}", line["text"])
                line["text"].format(name="Sam")  # format-clean

    def test_at_least_sixty_distinct_original_texts(self):
        texts = set()
        for name in social_lines.situations():
            for line in social_lines.lines_for(name):
                texts.add(line["text"])
        self.assertGreaterEqual(len(texts), 60, len(texts))

    def test_unknown_situation_raises(self):
        with self.assertRaises(KeyError):
            social_lines.lines_for("ghost")

    def test_no_false_claim_patterns(self):
        forbidden = (r"got married", r"\bpromoted\b", r"gave birth",
                     r"\bpregnant\b", r"\$\d")
        for name in social_lines.situations():
            for line in social_lines.lines_for(name):
                low = line["text"].lower()
                for pat in forbidden:
                    self.assertIsNone(
                        re.search(pat, low),
                        "%s: %r" % (name, line["text"]))


class SelectorIntegrationTests(unittest.TestCase):
    def _player(self, age):
        class P:
            pass
        p = P()
        p.age = age
        return p

    def test_pick_from_every_situation_pool(self):
        rng = random.Random(9)
        for name in social_lines.situations():
            pool = social_lines.lines_for(name)
            recent = []
            for _ in range(3):
                picked = pick_year_line(self._player(30), None, recent,
                                        rng, pool=pool)
                self.assertIsNotNone(picked)
                line_id, text = picked
                self.assertTrue(line_id.startswith("socl_%s_" % name))
                text.format(name="Sam")
                recent.append(line_id)

    def test_no_repeat_within_pool_window(self):
        rng = random.Random(13)
        pool = social_lines.lines_for("friend_made")
        recent, seen = [], set()
        for _ in range(5):
            picked = pick_year_line(self._player(40), None, recent, rng,
                                    pool=pool)
            self.assertIsNotNone(picked)
            self.assertNotIn(picked[0], seen)
            seen.add(picked[0])
            recent.append(picked[0])


if __name__ == "__main__":
    unittest.main()
