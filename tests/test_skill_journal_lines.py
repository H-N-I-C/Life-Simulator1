"""Skill-gain journal line variants (Kimi C, M3c).

The single _JOURNAL_LINES entry per skill repeated every year. Now each
skill has 4 original variants picked through the COSMETIC stream with a
persisted no-repeat window (env["skill_line_recents"], pickle/undo-safe
like the yearly-flavour recents). These tests pin: variation, the
no-repeat window, cosmetic-stream purity, and persistence across
save/reload.

Run: python3 -m unittest tests.test_skill_journal_lines
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

from src.game.features.skills import state, training  # noqa: E402
from src.game.service import GameService  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}


class SkillLineTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-skill-lines-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-create")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 20
        self.svc._save(env)

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def env(self):
        return self.svc._load(self.gid)

    def train_repeat(self, env, skill_option, n):
        texts = []
        for _ in range(n):
            # clear the annual cap without touching rng streams
            entry = state.person_state(env, env["player"].person_id,
                                       create=True)
            entry["trained_this_year"].pop(skill_option, None)
            entry["count_this_year"].pop(skill_option, None)
            entry["history"].clear()  # keep headroom/gain path identical
            training.cmd_train(env, {"target_id": skill_option},
                               random.Random(1), self.svc)
            texts.append(env["journal"][-1]["text"])
        return texts

    def test_variants_and_no_repeat_window(self):
        env = self.env()
        texts = self.train_repeat(env, "train_practical", 4)
        self.assertEqual(len(set(texts)), 4, "four gains, four different "
                                             "lines: %r" % texts)
        # the pool has exactly 4 variants; a 5th gain wraps without error
        texts5 = self.train_repeat(env, "train_practical", 1)
        self.assertTrue(texts5[0])

    def test_annual_hook_uses_variants_too(self):
        # the annual hook's gain path is apply_training with a synthetic
        # option — drive exactly that (the hook adds no other journaling)
        env = self.env()
        pid = env["player"].person_id
        rules = training.data.load()["rules"]["job_practice_per_year"]
        option = {"id": "annual_job", "skill": "organization",
                  "progress": rules, "choose_skill": False}
        for year in range(3):
            entry = state.person_state(env, pid, create=True)
            entry["history"].clear()
            training.apply_training(env, env["player"], option,
                                    "organization", random.Random(2),
                                    svc=self.svc)
        recents = env[training.SKILL_LINE_RECENTS_KEY]
        self.assertGreaterEqual(len(recents), 3)

    def test_exhausted_pool_does_not_repeat_the_last_line(self):
        from unittest.mock import patch
        from src.appearance import integration
        env = self.env()
        # Keep another skill's history while renewing the practical pool.
        other = training._SKILL_LINES["care"][0]
        env[training.SKILL_LINE_RECENTS_KEY] = [other]
        with patch.object(integration, "_APP_RNG", random.Random(0)):
            lines = [training._skill_line(env, "practical")
                     for _ in range(20)]
        self.assertEqual(len(set(lines[:4])), 4)
        self.assertTrue(all(a != b for a, b in zip(lines, lines[1:])))
        self.assertIn(other, env[training.SKILL_LINE_RECENTS_KEY])

    def test_draw_is_cosmetic_stream_only(self):
        env = self.env()
        before = random.getstate()
        self.train_repeat(env, "train_organization", 2)
        self.assertEqual(before, random.getstate(),
                         "journal line draws must not touch the gameplay "
                         "stream")

    def test_recents_persist_across_reload(self):
        env = self.env()
        self.train_repeat(env, "train_care", 2)
        seen = list(env[training.SKILL_LINE_RECENTS_KEY])
        self.assertTrue(seen)
        self.svc._save(env)
        env2 = self.env()
        self.assertEqual(env2[training.SKILL_LINE_RECENTS_KEY][:len(seen)],
                         seen)
        # and the window still applies after reload: no immediate repeat
        entry = state.person_state(env2, env2["player"].person_id,
                                   create=True)
        entry["trained_this_year"].pop("train_care", None)
        entry["count_this_year"].pop("train_care", None)
        entry["history"].clear()
        training.cmd_train(env2, {"target_id": "train_care"},
                           random.Random(1), self.svc)
        self.assertNotIn(env2["journal"][-1]["text"], seen)

    def test_every_skill_has_variants(self):
        from src.game.features.skills import data
        for skill in data.skill_ids():
            self.assertGreaterEqual(len(training._SKILL_LINES[skill]), 4)
            self.assertEqual(
                len(set(training._SKILL_LINES[skill])),
                len(training._SKILL_LINES[skill]),
                "%s variants must be unique" % skill)


if __name__ == "__main__":
    unittest.main()
