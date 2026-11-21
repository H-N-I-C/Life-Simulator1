"""M3 Skills (Kimi C): data validity, deterministic defaults (no RNG on
reads), training costs/deltas/limits, eligibility ordering, saturation,
$0 fallback, annual year roll, seeded reproducibility, reload
persistence, projection shape and requirement queries.

Real GameService games where the flow allows it; the train command body
is invoked directly (A wires the dispatcher hook per contracts.md).

Run: python3 -m unittest tests.test_skills
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

from src.game.features.skills import data, projection, state, training  # noqa: E402
from src.game.service import GameError, GameService  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}


class SkillsTestCase(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-skills-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-create")
        self.gid = st["game_id"]

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def env(self):
        return self.svc._load(self.gid)

    def save(self, env):
        self.svc._save(env)

    def adult_env(self, age=20):
        env = self.env()
        env["player"].age = age
        self.save(env)
        return self.env()

    def player_id(self, env):
        return env["player"].person_id

    def train(self, env, payload, rng_seed=1):
        return training.cmd_train(env, payload, random.Random(rng_seed),
                                  self.svc)


class DataValidityTests(SkillsTestCase):
    def test_content_loads_and_validates(self):
        raw = data.load(reload=True)
        self.assertEqual(raw["schema_version"], 1)
        self.assertEqual(len(raw["skills"]), 4)
        ids = [s["id"] for s in raw["skills"]]
        self.assertEqual(len(ids), len(set(ids)))
        for opt in raw["training_options"]:
            if opt["skill"] is not None:
                self.assertIn(opt["skill"], ids)
        self.assertTrue(any((o.get("cost") or {}).get("money", 0) == 0
                            for o in raw["training_options"]),
                        "a legal $0 option must exist")
        self.assertTrue(any(o.get("choose_skill")
                            for o in raw["training_options"]))

    def test_json_file_is_valid_data(self):
        path = os.path.join(ROOT, "src", "game", "content", "skills",
                            "skills.json")
        with open(path, encoding="utf-8") as f:
            raw = json.load(f)
        self.assertIn("skills", raw)
        self.assertIn("training_options", raw)
        self.assertIn("rules", raw)


class DefaultsAndBackfillTests(SkillsTestCase):
    def test_defaults_deterministic_and_no_rng_on_reads(self):
        env = self.env()
        pid = self.player_id(env)
        before = random.getstate()
        v1 = dict(state.values(env, pid))
        v2 = dict(state.values(env, pid))
        after = random.getstate()
        self.assertEqual(v1, v2)
        self.assertEqual(before, after, "reads must not draw randomness")
        for skill, value in v1.items():
            sdef = data.skill_def(skill)
            self.assertGreaterEqual(value, sdef["default"])
            self.assertLessEqual(value, sdef["default"] + 5)
            self.assertGreaterEqual(value, 0)
            self.assertLessEqual(value, 100)

    def test_twin_seeded_lives_identical_defaults(self):
        # R8 (Codex M3 review): defaults seed from the stable name/gender
        # identity, NOT the random UUID4 person_id — two fresh games with
        # the same seeded profile must start with identical talents even
        # though their person_ids differ.
        seen = []
        for i in range(2):
            d = tempfile.mkdtemp(prefix="lifesim-skills-twin-")
            svc = GameService(d)
            st = svc.new_game(dict(PROFILE), "twin-%d" % i)
            env = svc._load(st["game_id"])
            pid = env["player"].person_id
            seen.append((pid, dict(state.values(env, pid))))
            shutil.rmtree(d, ignore_errors=True)
        self.assertNotEqual(seen[0][0], seen[1][0],
                            "person_ids must differ to prove the point")
        self.assertEqual(seen[0][1], seen[1][1],
                         "identical seeded lives must get identical "
                         "starting skills")

    def test_backfill_once_and_stable(self):
        env = self.env()
        pid = self.player_id(env)
        first = dict(state.backfill(env)["people"][pid]["values"])
        self.assertIn("skills", env)
        second = dict(state.backfill(env)["people"][pid]["values"])
        self.assertEqual(first, second, "backfill must not reroll")
        self.save(env)
        reloaded = dict(state.values(self.env(), pid))
        self.assertEqual(first, reloaded,
                         "backfilled values must persist across reload")


class TrainCommandTests(SkillsTestCase):
    def test_free_training_gain_and_journal(self):
        env = self.adult_env()
        pid = self.player_id(env)
        before = state.values(env, pid)["practical"]
        out = self.train(env, {"target_id": "train_practical"})
        gain = out["gain"]
        self.assertGreaterEqual(gain, 3)
        after = state.values(env, pid)["practical"]
        self.assertEqual(after - before, gain)
        # journal: first-person line with the ACTUAL applied delta
        entry = env["journal"][-1]
        self.assertEqual(entry["kind"], "skill")
        self.assertEqual(entry["changes"], {"practical": gain})
        self.assertTrue(entry["text"].strip())
        self.assertEqual(state.uses_this_year(env, pid)["train_practical"], 1)

    def test_paid_training_exact_cost_once(self):
        env = self.adult_env()
        env["player"].money = 100
        self.save(env)
        env = self.env()
        money_before = env["player"].money
        self.train(env, {"target_id": "train_communication"})
        self.assertEqual(env["player"].money, money_before - 25)
        self.assertEqual(
            sum(1 for e in env["ledger"]
                if e.get("category") == "education"), 1)
        entry = next(e for e in env["ledger"]
                     if e.get("category") == "education")
        self.assertIn("skills:", entry["source_key"])

    def test_annual_limit_blocks_before_any_spend(self):
        env = self.adult_env()
        env["player"].money = 1000
        self.save(env)
        env = self.env()
        pid = self.player_id(env)
        for _ in range(2):
            self.train(env, {"target_id": "train_communication"})
        money = env["player"].money
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_communication"})
        self.assertEqual(ctx.exception.status, 409)
        self.assertIn("year", str(ctx.exception).lower())
        self.assertEqual(env["player"].money, money, "no spend on refusal")
        self.assertEqual(state.uses_this_year(env, pid)["train_communication"], 2)

    def test_saturation_disables_without_charge(self):
        env = self.adult_env()
        pid = self.player_id(env)
        state.values(env, pid)["practical"] = 100
        env["player"].money = 0
        journal_len = len(env["journal"])
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_practical"})
        self.assertIn("maximum", str(ctx.exception).lower())
        self.assertEqual(env["player"].money, 0)
        self.assertEqual(len(env["journal"]), journal_len,
                         "no journal entry for a refused/empty training")

    def test_min_age_gate(self):
        env = self.adult_env(age=10)
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_organization"})
        self.assertEqual(ctx.exception.status, 409)
        self.assertIn("too young", str(ctx.exception).lower())

    def test_affordability_checked_before_spend(self):
        env = self.adult_env()
        env["player"].money = 0
        self.save(env)
        env = self.env()
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_communication"})
        self.assertIn("costs", str(ctx.exception).lower())
        self.assertEqual(env["player"].money, 0)
        # $0 fallback stays legal at $0
        out = self.train(env, {"target_id": "train_practical"})
        self.assertGreater(out["gain"], 0)

    def test_choose_skill_requires_valid_skill(self):
        env = self.adult_env()
        env["player"].money = 500
        self.save(env)
        env = self.env()
        # R4: a missing choice is a typed 400 BEFORE any spend (the UI
        # must supply the chosen skill)
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_evening_course"})
        self.assertEqual(ctx.exception.status, 400)
        self.assertEqual(env["player"].money, 500, "no spend on refusal")
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_evening_course",
                             "skill": "nope"})
        self.assertEqual(ctx.exception.status, 409)
        out = self.train(env, {"target_id": "train_evening_course",
                               "skill": "care"})
        self.assertEqual(out["skill"], "care")
        self.assertGreaterEqual(out["gain"], 5)
        self.assertEqual(env["player"].money, 440, "exactly once")

    def test_choose_skill_saturation_blocks_before_spend(self):
        env = self.adult_env()
        env["player"].money = 500
        pid = self.player_id(env)
        state.values(env, pid)["care"] = 100
        self.save(env)
        env = self.env()
        with self.assertRaises(GameError) as ctx:
            self.train(env, {"target_id": "train_evening_course",
                             "skill": "care"})
        self.assertEqual(ctx.exception.status, 409)
        self.assertIn("maximum", str(ctx.exception).lower())
        self.assertEqual(env["player"].money, 500, "no spend at max")

    def test_invalid_ids_and_types(self):
        env = self.adult_env()
        for payload in ({}, {"target_id": 5},
                        {"target_id": "train_nope"},
                        {"target_id": "train_practical", "skill": 7}):
            with self.assertRaises(GameError) as ctx:
                self.train(env, payload)
            self.assertEqual(ctx.exception.status, 400, payload)

    def test_year_roll_clears_usage(self):
        env = self.adult_env()
        pid = self.player_id(env)
        self.train(env, {"target_id": "train_practical"})
        self.assertEqual(state.uses_this_year(env, pid)["train_practical"], 1)
        env["player"].age = 21
        self.assertEqual(state.uses_this_year(env, pid), {})

    def test_seeded_reproducibility(self):
        gains = []
        for run in range(2):
            d = tempfile.mkdtemp(prefix="lifesim-skills-seed-")
            svc = GameService(d)
            st = svc.new_game(dict(PROFILE), "skills-seed-%d" % run)
            gid = st["game_id"]
            env = svc._load(gid)
            env["player"].age = 20
            svc._save(env)
            env = svc._load(gid)
            seq = []
            for i in range(3):
                state.uses_this_year(env,
                                     env["player"].person_id).clear()
                seq.append(training.cmd_train(
                    env, {"target_id": "train_practical"},
                    random.Random(99), svc)["gain"])
            gains.append(seq)
            shutil.rmtree(d, ignore_errors=True)
        self.assertEqual(gains[0], gains[1])

    def test_global_rng_isolation_on_reads(self):
        env = self.adult_env()
        before = random.getstate()
        projection.project(env)
        state.values(env, self.player_id(env))
        training.action_specs(env)
        self.assertEqual(before, random.getstate(),
                         "projection/specs/reads must not draw randomness")


class AnnualHookTests(SkillsTestCase):
    def test_hook_noops_without_active_sources(self):
        env = self.adult_env()
        pid = self.player_id(env)
        before = dict(state.values(env, pid))
        training.annual_hook(env, random.Random(3), self.svc)
        self.assertEqual(dict(state.values(env, pid)), before)

    def _drain_pending(self, st, tag):
        n = 0
        while st.get("pending_decision"):
            d = st["pending_decision"]
            st = self.svc.command(self.gid, st["revision"],
                                  "%s-d%d" % (tag, n), "resolve_decision",
                                  {"decision_id": d["id"],
                                   "option_id": d["options"][0]["id"],
                                   "inputs": {}})
            n += 1
            self.assertLess(n, 25, "pending chain did not drain")
        return st

    def _grant(self, env, skills):
        pid = self.player_id(env)
        state.values(env, pid).update(skills)
        self.save(env)
        return self.env()

    def _apply_office_job(self):
        """Real service apply -> stored offer -> accept (A's production
        path), giving the player an active office_assistant assignment
        that declares practices_skill=organization."""
        st = self.svc.get_state(self.gid)
        st = self._drain_pending(st, "pre")
        st = self.svc.command(self.gid, st["revision"], "job-a",
                              "careers_apply",
                              {"role_id": "office_assistant",
                               "employer_id": "meridian"})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "career_offer")
        st = self.svc.command(self.gid, st["revision"], "job-r",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": "accept",
                               "inputs": {}})
        self.assertIsNone(st["pending_decision"])
        env = self.env()
        # keep the legacy performance-firing gate shut (rule_actions fires
        # on performance < 15); A's M3b reconciliation then leaves the
        # named assignment active for the whole year
        env["player"].performance = 100
        self.svc._save(env)
        return self.env()

    def test_job_practice_from_active_assignment(self):
        env = self.adult_env(age=20)
        env = self._grant(env, {"communication": 50})
        env = self._apply_office_job()
        pid = self.player_id(env)
        before = dict(state.values(env, pid))
        training.annual_hook(env, random.Random(5), self.svc)
        after = dict(state.values(env, pid))
        entry = env["journal"][-1]
        self.assertEqual(entry["kind"], "skill")
        self.assertEqual(list(entry["changes"]), ["organization"])
        gain = entry["changes"]["organization"]
        rules = data.load()["rules"]["job_practice_per_year"]
        self.assertGreaterEqual(gain, rules["min"])
        self.assertLessEqual(gain, rules["max"] + 4)  # smarts bonus cap
        self.assertEqual(after["organization"] - before["organization"],
                         gain)
        for skill in ("practical", "communication", "care"):
            self.assertEqual(after[skill], before[skill],
                             "only the declared skill may gain")
        # exactly once per year: a second run is a no-op, and reload
        # keeps the applied value
        self.save(env)
        env = self.env()
        training.annual_hook(env, random.Random(6), self.svc)
        self.assertEqual(state.values(env, pid)["organization"],
                         after["organization"])

    def test_job_practice_capped_at_headroom(self):
        env = self.adult_env(age=20)
        env = self._grant(env, {"communication": 50})
        env = self._apply_office_job()
        pid = self.player_id(env)
        state.values(env, pid)["organization"] = 99
        training.annual_hook(env, random.Random(5), self.svc)
        self.assertEqual(state.values(env, pid)["organization"], 100)

    def test_study_contribution_from_active_enrollment(self):
        env = self.adult_env(age=20)
        env["player"].money = 20000
        self.save(env)
        st = self.svc.get_state(self.gid)
        st = self._drain_pending(st, "pre")
        st = self.svc.command(self.gid, st["revision"], "edu-a",
                              "education_enroll",
                              {"program_id": "care_diploma"})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "education_funding")
        st = self.svc.command(self.gid, st["revision"], "edu-r",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": "self", "inputs": {}})
        self.assertIsNone(st["pending_decision"])
        env = self.env()
        pid = self.player_id(env)
        before = dict(state.values(env, pid))
        training.annual_hook(env, random.Random(7), self.svc)
        after = dict(state.values(env, pid))
        entry = env["journal"][-1]
        self.assertEqual(list(entry["changes"]), ["care"])
        self.assertEqual(after["care"] - before["care"],
                         entry["changes"]["care"])
        for skill in ("practical", "organization", "communication"):
            self.assertEqual(after[skill], before[skill])

    def test_annual_contributions_via_real_age_up(self):
        # end-to-end: the coordinator's hook registry applies job
        # practice during a real age_up (after work_income, before
        # education), not just when the hook is called directly
        env = self.adult_env(age=20)
        env = self._grant(env, {"communication": 50})
        env = self._apply_office_job()
        pid = self.player_id(env)
        age = env["player"].age
        before = state.values(env, pid)["organization"]
        st = self.svc.command(self.gid, self.svc.get_state(self.gid)
                              ["revision"], "age1", "age_up", {})
        env = self.env()
        entry = next((e for e in reversed(env["journal"])
                      if e.get("kind") == "skill"), None)
        self.assertIsNotNone(entry, "age_up must apply job practice")
        self.assertEqual(list(entry["changes"]), ["organization"])
        self.assertGreater(state.values(env, pid)["organization"],
                           before)
        self.assertEqual(env["player"].age, age + 1)


class ProjectionAndQueryTests(SkillsTestCase):
    def test_projection_shape(self):
        env = self.adult_env()
        proj = projection.project(env)
        self.assertEqual(set(proj["values"]),
                         {"practical", "organization", "communication",
                          "care"})
        self.assertEqual(len(proj["training"]),
                         len(data.training_options()))
        for spec in proj["training"]:
            for key in ("id", "label", "category", "target_id", "inputs",
                        "known_costs", "limits", "enabled", "reason"):
                self.assertIn(key, spec)
            self.assertNotIn("_labels", spec)
            # R4: ONE dispatchable dialect — action train_skill with the
            # option as target_id, never per-option action ids
            self.assertEqual(spec["id"], "train_skill")
        targets = [s["target_id"] for s in proj["training"]]
        self.assertEqual(len(targets), len(set(targets)))
        paid = next(s for s in proj["training"]
                    if s["target_id"] == "train_communication")
        self.assertEqual(paid["known_costs"],
                         [{"kind": "money", "amount": 25}])
        free = next(s for s in proj["training"]
                    if s["target_id"] == "train_practical")
        self.assertEqual(free["known_costs"], [])
        disabled = next(s for s in proj["training"]
                        if not s["enabled"])
        self.assertTrue(disabled["reason"])

    def test_choose_skill_spec_is_enabled_and_advertises_choice(self):
        # R4: the evening course must be dispatchable — enabled when
        # affordable — with a choice input, not permanently disabled
        env = self.adult_env(age=20)
        env["player"].money = 500
        self.save(env)
        env = self.env()
        proj = projection.project(env)
        course = next(s for s in proj["training"]
                      if s["target_id"] == "train_evening_course")
        self.assertTrue(course["enabled"], course["reason"])
        self.assertIsNone(course["reason"])
        self.assertEqual(course["inputs"][0]["type"], "choice")
        self.assertEqual(len(course["inputs"][0]["choices"]), 4)
        # still disabled when unaffordable (single source with dispatch)
        env = self.adult_env(age=20)
        env["player"].money = 0
        self.save(env)
        proj = projection.project(self.env())
        course = next(s for s in proj["training"]
                      if s["target_id"] == "train_evening_course")
        self.assertFalse(course["enabled"])
        self.assertIn("costs", course["reason"])

    def test_requirements_met_query(self):
        ok = projection.requirements_met({"practical": 40, "care": 100},
                                           [{"skill": "practical", "min": 30},
                                            {"skill": "care", "min": 100}])
        self.assertTrue(ok["ok"])
        self.assertEqual(ok["missing"], [])
        bad = projection.requirements_met({"practical": 20},
                                          [{"skill": "practical", "min": 30},
                                           {"skill": "care", "min": 50},
                                           {"skill": "ghost", "min": 1}])
        self.assertFalse(bad["ok"])
        self.assertEqual(len(bad["missing"]), 3)
        self.assertEqual(bad["missing"][0],
                         {"skill": "practical", "need": 30, "have": 20})
        self.assertIsNone(bad["missing"][2]["have"])
        labels = projection.requirement_labels(
            [{"skill": "communication", "min": 60}])
        self.assertEqual(labels[0]["label"], "Communication")


if __name__ == "__main__":
    unittest.main()
