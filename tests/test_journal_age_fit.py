"""Age-fit test for the journal pools (Kimi C, BACKLOG).

Relationship-drift lines must be age-true: a one-year-old cannot have
'helped Mum out around the house'. Pins:

- relationship_flavour: baby/toddler bands carry ONLY age-true
  attachment/sensory lines (no mature-activity vocabulary); child..elder
  bands carry no baby lines; every line formats {name} where present.
- social_lines: no baby/toddler entries at all (no social graph at
  0-4 — honest absence), child..elder intact.
- the frozen selector actually returns a baby-band line at age 1.

Run: python3 -m unittest tests.test_journal_age_fit
"""

import random
import re
import unittest

from src.game.content.journal import relationship_flavour, social_lines
from src.game.content.journal.yearly_lines import BANDS, band_for_age, \
    pick_year_line

BABY_BANDS = ("baby", "toddler")
GROWN_BANDS = ("child", "teen", "adult", "elder")

# grown-up activities a 0-4-year-old cannot truthfully report
MATURE = re.compile(
    r"phone call|rent|budget|salary|\bjob\b|coffee|advice|lecture|"
    r"deadline|homework|exam|shift|dinner table|spreadsheet|"
    r"university|driving|interview", re.I)


class RelationshipFlavourAgeFitTests(unittest.TestCase):
    def test_every_role_covers_baby_and_toddler(self):
        for role in relationship_flavour.ROLES:
            for delta, pool in ((1, relationship_flavour.IMPROVED_LINES),
                                (-1, relationship_flavour.WORSENED_LINES)):
                per_band = {b: 0 for b in BABY_BANDS}
                for line in relationship_flavour.lines_for(delta, role):
                    if line["band"] in per_band:
                        per_band[line["band"]] += 1
                self.assertTrue(all(per_band[b] > 0 for b in BABY_BANDS),
                                "%s delta=%s: %s" % (role, delta, per_band))

    def test_grown_bands_have_no_baby_lines(self):
        for pool in (relationship_flavour.IMPROVED_LINES,
                     relationship_flavour.WORSENED_LINES):
            for line in pool:
                if line["band"] in GROWN_BANDS:
                    self.assertNotIn("baby", line["id"])
                else:
                    self.assertNotIn("imp_", line["id"])
                    self.assertNotIn("wor_", line["id"])

    def test_baby_lines_are_age_true(self):
        for pool in (relationship_flavour.IMPROVED_LINES,
                     relationship_flavour.WORSENED_LINES):
            for line in pool:
                if line["band"] not in BABY_BANDS:
                    continue
                self.assertIsNone(
                    MATURE.search(line["text"]),
                    "%s (%s): mature vocabulary in a baby line: %r"
                    % (line["id"], line["band"], line["text"]))
                line["text"].format(name="Sam")

    def test_baby_lines_have_periods_and_context(self):
        for pool in (relationship_flavour.IMPROVED_LINES,
                     relationship_flavour.WORSENED_LINES):
            for line in pool:
                if line["band"] in BABY_BANDS:
                    self.assertTrue(line["text"].rstrip().endswith("."))


class SocialLinesAgeFitTests(unittest.TestCase):
    def test_no_baby_or_toddler_entries(self):
        for name in social_lines.situations():
            for line in social_lines.lines_for(name):
                self.assertIn(line["band"], GROWN_BANDS,
                              "%s leaked into %s"
                              % (name, line["band"]))

    def test_child_to_elder_coverage_intact(self):
        for name in social_lines.situations():
            per_band = {b: 0 for b in GROWN_BANDS}
            for line in social_lines.lines_for(name):
                per_band[line["band"]] += 1
            self.assertTrue(all(per_band[b] > 0 for b in GROWN_BANDS),
                            "%s: %s" % (name, per_band))


class SelectorAgeFitTests(unittest.TestCase):
    def _player(self, age):
        class P:
            pass
        p = P()
        p.age = age
        return p

    def test_age_one_draws_a_baby_band_line(self):
        self.assertEqual(band_for_age(1), "baby")
        pool = relationship_flavour.lines_for(3, "mother")
        recent = []
        for _ in range(3):
            picked = pick_year_line(self._player(1), None, recent,
                                    random.Random(2), pool=pool)
            self.assertIsNotNone(picked)
            line_id, text = picked
            self.assertIn("_baby_", line_id, line_id)
            self.assertIsNone(MATURE.search(text), text)
            recent.append(line_id)

    def test_age_three_draws_a_toddler_band_line(self):
        self.assertEqual(band_for_age(3), "toddler")
        pool = relationship_flavour.lines_for(-3, "father")
        picked = pick_year_line(self._player(3), None, [],
                                random.Random(5), pool=pool)
        self.assertIsNotNone(picked)
        self.assertIn("_toddler_", picked[0])


if __name__ == "__main__":
    unittest.main()
