"""Tests for the relation-type-specific relationship-drift line pools
(Kimi C, src/game/content/journal/relationship_flavour.py).

The pools must plug into the frozen pick_year_line selector via pool=,
so they carry id/band/text entries across the same six age bands, use a
{name} placeholder (formatted by the caller), and return a usable pool
for every role and direction — including the unknown-role fallback.
"""
import random
import unittest

from src.game.content.journal import relationship_flavour as rf
from src.game.content.journal.yearly_lines import pick_year_line, BANDS


class PoolShapeTests(unittest.TestCase):
    def test_every_line_has_id_band_text(self):
        for pool in (rf.IMPROVED_LINES, rf.WORSENED_LINES):
            self.assertGreaterEqual(len(pool), 20)
            for line in pool:
                self.assertIn("id", line)
                self.assertIn(line["band"], BANDS)
                self.assertTrue(line["text"].strip())

    def test_roles_and_bands_covered(self):
        for role in rf.ROLES:
            for delta, pool in ((5, rf.IMPROVED_LINES), (-5, rf.WORSENED_LINES)):
                per_band = {b: 0 for b in BANDS}
                for line in rf.lines_for(delta, role):
                    per_band[line["band"]] += 1
                self.assertTrue(all(per_band[b] > 0 for b in BANDS),
                                f"{role} delta={delta}: {per_band}")

    def test_direction_split(self):
        imp_ids = {l["id"] for l in rf.lines_for(1, "partner")}
        wor_ids = {l["id"] for l in rf.lines_for(-1, "partner")}
        self.assertFalse(imp_ids & wor_ids)

    def test_unknown_role_falls_back_never_none(self):
        pool = rf.lines_for(3, "goldfish")
        self.assertTrue(pool)
        for band in BANDS:
            self.assertTrue(any(l["band"] == band for l in pool))

    def test_name_placeholder(self):
        # Parent lines address "Mum"/"Dad" directly; every other role
        # template uses the {name} placeholder. All must .format cleanly.
        for pool in (rf.IMPROVED_LINES, rf.WORSENED_LINES):
            for line in pool:
                if "_mother_" not in line["id"] and "_father_" not in line["id"]:
                    self.assertIn("{name}", line["text"])
                line["text"].format(name="Sam")


class SelectorIntegrationTests(unittest.TestCase):
    """Pools must work through C's frozen pick_year_line unchanged."""

    def _player(self, age):
        class P:
            pass
        p = P()
        p.age = age
        return p

    def test_pick_from_each_role_pool(self):
        rng = random.Random(7)
        for role in rf.ROLES:
            for delta in (4, -4):
                pool = rf.lines_for(delta, role)
                recent = []
                for _ in range(5):
                    picked = pick_year_line(self._player(30), None, recent,
                                            rng, pool=pool)
                    self.assertIsNotNone(picked)
                    line_id, text = picked
                    self.assertIn(f"_{role}_", line_id)
                    text.format(name="Sam")
                    recent.append(line_id)

    def test_no_repeat_within_window(self):
        rng = random.Random(11)
        pool = rf.lines_for(2, "partner")
        recent = []
        seen = set()
        for _ in range(6):  # per-band pool has 7; within 60-window no dup
            picked = pick_year_line(self._player(35), None, recent, rng,
                                    pool=pool)
            self.assertIsNotNone(picked)
            self.assertNotIn(picked[0], seen)
            seen.add(picked[0])
            recent.append(picked[0])


if __name__ == "__main__":
    unittest.main()
