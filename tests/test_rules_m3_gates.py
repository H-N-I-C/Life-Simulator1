"""M3 workplace/study event gates (Kimi C, 2026-11-21).

The new v3 condition leaves active_assignment / active_enrollment gate
m3_work.json / m3_study.json through the SHARED v3 dispatch path
(sample_v3 + evaluate_v3_conditions) — never a parallel loader. Real
GameService envelopes: real careers apply->accept and real
education_enroll->funding resolve create the active sources.

Run: python3 -m unittest tests.test_rules_m3_gates
"""

import json
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

from src.game import events  # noqa: E402
from src.game.context import RuleContext  # noqa: E402
from src.game.service import GameService  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}


def content_events(name):
    path = os.path.join(ROOT, "src", "game", "content", "events",
                        "%s.json" % name)
    with open(path, encoding="utf-8") as f:
        return json.load(f)["events"]


class GateTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m3-gates-")
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

    def ctx(self, env):
        return RuleContext(env, self.svc)

    def cond_ok(self, ev, env):
        ctx = self.ctx(env)
        pid = env["player"].person_id
        return events.evaluate_v3_conditions(
            ev.get("conditions") or {}, ctx, {"actor": pid}, pid)

    def test_work_events_shut_without_assignment(self):
        env = self.env()
        for ev in content_events("m3_work"):
            self.assertFalse(self.cond_ok(ev, env), ev["id"])

    def test_study_events_shut_without_enrollment(self):
        env = self.env()
        for ev in content_events("m3_study"):
            self.assertFalse(self.cond_ok(ev, env), ev["id"])

    def _take_office_job(self):
        from src.game.features.skills import state as skills_state
        env = self.env()
        skills_state.values(env, env["player"].person_id).update(
            {"communication": 50})
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        while st.get("pending_decision"):
            d = st["pending_decision"]
            st = self.svc.command(self.gid, st["revision"], "g-pre",
                                  "resolve_decision",
                                  {"decision_id": d["id"],
                                   "option_id": d["options"][0]["id"],
                                   "inputs": {}})
        st = self.svc.command(self.gid, st["revision"], "g-apply",
                              "careers_apply",
                              {"role_id": "office_assistant",
                               "employer_id": "meridian"})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "career_offer")
        st = self.svc.command(self.gid, st["revision"], "g-accept",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": "accept", "inputs": {}})
        self.assertIsNone(st.get("pending_decision"))
        return self.env()

    def test_work_events_open_with_assignment_and_respect_ladder(self):
        env = self._take_office_job()
        opened, closed = [], []
        for ev in content_events("m3_work"):
            (opened if self.cond_ok(ev, env) else closed).append(ev["id"])
        self.assertTrue(opened)
        # office-ladder events open for an office assistant...
        self.assertIn("wrk_paperwork_avalanche", opened)
        self.assertIn("wrk_grumpy_client", opened)
        # ...general events too...
        self.assertIn("wrk_overtime_weekend", opened)
        # ...but trades-ladder events stay shut for the wrong ladder
        self.assertIn("wrk_wrong_cable", closed)
        self.assertIn("wrk_safety_inspection", closed)

    def _enroll(self, program_id):
        st = self.svc.get_state(self.gid)
        while st.get("pending_decision"):
            d = st["pending_decision"]
            st = self.svc.command(self.gid, st["revision"], "s-pre",
                                  "resolve_decision",
                                  {"decision_id": d["id"],
                                   "option_id": d["options"][0]["id"],
                                   "inputs": {}})
        env = self.env()
        env["player"].money = 20000
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "s-enroll",
                              "education_enroll",
                              {"program_id": program_id})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "education_funding")
        st = self.svc.command(self.gid, st["revision"], "s-fund",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": "self", "inputs": {}})
        return self.env()

    def test_study_events_open_with_enrollment_and_respect_program(self):
        env = self._enroll("care_diploma")
        opened, closed = [], []
        for ev in content_events("m3_study"):
            (opened if self.cond_ok(ev, env) else closed).append(ev["id"])
        self.assertIn("stu_group_project", opened)
        self.assertIn("stu_practical_exam", opened)
        self.assertIn("stu_placement_review", opened)
        # business-only and apprenticeship-only events stay shut
        self.assertIn("stu_class_debate", closed)
        self.assertIn("stu_lab_spill", closed)

    def test_shared_dispatch_path_draws_only_when_gated_open(self):
        # the production single v3 dispatch path, not a side loader
        loaded = []
        for name in ("m3_work", "m3_study"):
            for ev in content_events(name):
                ev = dict(ev)
                ev["schema_version"] = 3  # stamped by load_event_files
                loaded.append(ev)
        engine = events.EventEngine(loaded)
        rng = random.Random(3)
        env = self.env()
        ctx = self.ctx(env)
        pid = env["player"].person_id
        ev, _ = engine.sample_v3(rng, 20, set(), ctx, prefixes=("wrk_",),
                                 actor_id=pid)
        self.assertIsNone(ev, "no assignment -> no work event")
        ev, _ = engine.sample_v3(rng, 20, set(), ctx, prefixes=("stu_",),
                                 actor_id=pid)
        self.assertIsNone(ev, "no enrollment -> no study event")
        env = self._take_office_job()
        ctx = self.ctx(env)
        ev, _ = engine.sample_v3(rng, 20, set(), ctx, prefixes=("wrk_",),
                                 actor_id=pid)
        self.assertIsNotNone(ev)
        self.assertTrue(ev["id"].startswith("wrk_"))
        # and the drawn event passes its own gate in this envelope
        self.assertTrue(self.cond_ok(ev, env))


if __name__ == "__main__":
    unittest.main()
