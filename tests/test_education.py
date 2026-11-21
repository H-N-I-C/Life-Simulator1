"""M3 §Education tests (Kimi B, 2026-11-20).

Contract: docs/collab/M3/02-contracts.md §Education. Real GameService
saves + real domain objects; the feature engine is exercised exactly the
way A's dispatcher integration calls it (apply_action -> post money via
the one poster -> journal via rule_adapter; annual_hook after the legacy
education phase). CommandPathIntegrationTests exercise the real
GameService command path end to end. 3 seeds where sampling matters.

Run: TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_education
"""

import os
import random
import sys
import tempfile
import unittest
from unittest.mock import patch

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402
from src.game import rule_adapter  # noqa: E402
from src.game.features.education import core as education  # noqa: E402

SEEDS = [7, 4242, 90210]

DATA_DIR = os.path.join(os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(DATA_DIR, exist_ok=True)


def make_service():
    return GameService(tempfile.mkdtemp(dir=DATA_DIR))


def new_adult_game(svc, seed, request="c", age=18, smarts=None):
    st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                       "gender": "female", "country": "Australia",
                       "seed": seed}, request)
    env = svc._load(st["game_id"])
    env["player"].age = age
    if smarts is not None:
        env["player"].smarts = smarts
    svc._save(env)
    return svc.get_state(st["game_id"])


def load(svc, gid):
    return svc._load(gid)


def dispatch(svc, env, action, payload, rng=None):
    """Mirror of A's pending _cmd_education_action: feature apply_action
    -> one-ledger-poster money entries -> journal -> pending."""
    result = education.apply_action(env, env["player"], action, payload,
                                    rng or random.Random(1), svc)
    turn_id = env["turn"].get("turn_id", f"t_{env['revision'] + 1}")
    rule_adapter.post_money_entries(
        env, svc, turn_id, env["player"].age,
        result.get("money_entries"))
    rule_adapter.journal_events(env, result.get("events"))
    if "pending_decision" in result:
        env["pending"] = result["pending_decision"]
        if env["pending"] is not None:
            env["pending"]["id"] = f"d_{env['next_decision_num']}"
            env["next_decision_num"] += 1
    return result


def run_year(svc, env, seed=7):
    """Mirror of A's pending coordinator seam: legacy education phase
    journaled (not run here — unit scope), then the education hook,
    money posted + journaled through the single boundary. Each call is
    a fresh turn id (the real coordinator assigns one per age_up)."""
    turn = env.setdefault("turn", {})
    n = turn.get("_test_counter", 0) + 1
    turn["_test_counter"] = n
    turn["turn_id"] = f"t_test_{n:03d}"
    rng = seed if hasattr(seed, "random") else random.Random(seed)
    result = education.annual_hook(env, svc, rng)
    rule_adapter.post_money_entries(
        env, svc, env["turn"]["turn_id"], env["player"].age,
        result.get("money_entries"))
    rule_adapter.journal_events(env, result.get("events"))
    return result


def finish_secondary(env):
    """Force secondary completion: not in school (grades cleared),
    not dropped out, high-school graduate age."""
    p = env["player"]
    p.age = max(p.age, 18)
    p.grades = None
    p.dropped_out = False


def grant_qualification(env, qualification_id):
    st = env.setdefault("education_state", {})
    qs = st.setdefault("qualifications", [])
    if qualification_id not in {q["qualification_id"] for q in qs}:
        qs.append({"qualification_id": qualification_id,
                   "program_id": qualification_id, "program_version": 1,
                   "awarded_turn": "test"})


class ProjectionTests(unittest.TestCase):
    def test_programs_shape_and_reachability(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        programs = education.data()
        self.assertEqual(len(programs), 4)
        expected = {"app_electrical", "care_diploma", "business_bachelors",
                    "nursing_bachelors"}
        self.assertEqual(set(programs), expected)
        for p in programs.values():
            self.assertGreaterEqual(p["duration_years"], 2)
            self.assertGreaterEqual(p["tuition_per_year"], 0)
            if p["id"] != "app_electrical":
                self.assertGreater(p["tuition_per_year"], 0)
            # apprentice program declares exactly one wage source
            if p["apprentice_role"]:
                self.assertEqual(p["id"], "app_electrical")
        # free legal alternative: every paid program has loan funding
        for p in programs.values():
            if p["tuition_per_year"] > 0:
                self.assertIn("student_loan", p["funding"])
            self.assertIn("self", p["funding"])

    def test_advertised_matches_dispatch_gates(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=17)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        svc._save(env)
        st = svc.get_state(st["game_id"])
        view = education.education_view(env, env["player"])
        # 17-year-old: all min_age gates met except nursing (18)
        by_id = {p["program_id"]: p for p in view["programs"]}
        self.assertTrue(by_id["care_diploma"]["eligible"])
        self.assertFalse(by_id["nursing_bachelors"]["eligible"])
        self.assertIn("Care diploma", by_id["nursing_bachelors"]["reason"])
        # a 17-year-old still in school hits the age + secondary gates
        env2svc = make_service()
        st2 = new_adult_game(env2svc, 7, age=16)
        e2 = load(env2svc, st2["game_id"])
        e2["player"].age = 17  # still in school (grades set), not finished
        view2 = education.education_view(e2, e2["player"])
        by_id2 = {p["program_id"]: p for p in view2["programs"]}
        self.assertFalse(by_id2["nursing_bachelors"]["eligible"])
        self.assertIn("18", by_id2["nursing_bachelors"]["reason"])
        # projection is a pure read: no state drift
        rev = env["revision"]
        education.education_view(env, env["player"])
        self.assertEqual(env["revision"], rev)


class EnrollGateTests(unittest.TestCase):
    def test_young_and_dropped_out_refused(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=14)
        env = load(svc, st["game_id"])
        with self.assertRaises(PermissionError):
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "care_diploma"},
                                   random.Random(1), svc)
        env["player"].age = 18
        env["player"].grades = None
        env["player"].dropped_out = True
        with self.assertRaises(PermissionError):
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "care_diploma"},
                                   random.Random(1), svc)

    def test_prerequisite_qualification_gate(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=20)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        with self.assertRaises(PermissionError) as ctx:
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "nursing_bachelors"},
                                   random.Random(1), svc)
        self.assertIn("Care diploma", str(ctx.exception))
        grant_qualification(env, "care_diploma_cert")
        result = education.apply_action(
            env, env["player"], "education_enroll",
            {"program_id": "nursing_bachelors"}, random.Random(1), svc)
        self.assertEqual(result["pending_decision"]["kind"],
                         "education_funding")

    def test_admission_smarts_gate(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18, smarts=20)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        with self.assertRaises(PermissionError) as ctx:
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "business_bachelors"},
                                   random.Random(1), svc)
        self.assertIn("smarts 40", str(ctx.exception))

    def test_unknown_program_invalid(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        env = load(svc, st["game_id"])
        with self.assertRaises(ValueError):
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "nope"},
                                   random.Random(1), svc)

    def test_legacy_university_coexistence_blocked(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        env["player"].uv_years = 3
        with self.assertRaises(PermissionError) as ctx:
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "care_diploma"},
                                   random.Random(1), svc)
        self.assertIn("university", str(ctx.exception))
        # legacy uv state untouched by the refusal
        self.assertEqual(env["player"].uv_years, 3)

    def test_apprenticeship_stage_gate_without_bridge(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        # Force an unavailable bridge even after integration has landed.
        from src.game.features.careers import core as careers_core
        with patch.object(careers_core, "create_apprenticeship_assignment",
                          None), self.assertRaises(PermissionError) as ctx:
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "app_electrical"},
                                   random.Random(1), svc)
        self.assertIn("not open yet", str(ctx.exception))


class FundingFlowTests(unittest.TestCase):
    def _enroll_diploma(self, svc, seed):
        st = new_adult_game(svc, seed, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        result = dispatch(svc, env, "education_enroll",
                          {"program_id": "care_diploma"},
                          random.Random(seed))
        return env, result["pending_decision"]

    def test_enroll_samples_pending_no_money_moves(self):
        for seed in SEEDS:
            svc = make_service()
            env, pending = self._enroll_diploma(svc, seed)
            self.assertEqual(pending["kind"], "education_funding")
            ids = [o["id"] for o in pending["options"]]
            self.assertIn("self", ids)
            self.assertIn("student_loan", ids)
            self.assertIn("decline", ids)
            # sampling drew nothing from the player's pocket
            self.assertEqual(env["player"].money, env["player"].money)

    def test_self_funding_pays_per_advanced_year(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        money0 = env["player"].money
        result = dispatch(svc, env, "education_enroll",
                          {"program_id": "care_diploma"},
                          random.Random(7))
        enrollment = education.resolve_funding(
            env, env["player"], result["pending_decision"], "self",
            random.Random(7))
        rule_adapter.journal_events(env, enrollment["events"])
        e = education.active_enrollment(env, env["player"].person_id)
        self.assertEqual(e["funding"], "self")
        self.assertEqual(env["player"].money, money0)  # nothing yet
        # year 1 bills tuition once through the poster
        run_year(svc, env)
        self.assertEqual(env["player"].money, money0 - 3500)
        run_year(svc, env)
        self.assertEqual(env["player"].money, money0 - 7000)
        # posting keys distinguish same-turn/year fees
        occurrences = [x["source_key"] for x in env["ledger"]
                       if x.get("category") == "education"]
        self.assertEqual(len(occurrences), len(set(occurrences)))

    def test_loan_funding_advances_principal_once(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        result = dispatch(svc, env, "education_enroll",
                          {"program_id": "care_diploma"},
                          random.Random(7))
        education.resolve_funding(env, env["player"],
                                  result["pending_decision"], "student_loan",
                                  random.Random(7))
        # principal lands ONCE: 2 years x 3500
        self.assertEqual(env["player"].student_loan, 7000)
        self.assertTrue(env["player"].chose_student_loan)
        # no cash legs for loan-funded tuition, only the liability
        run_year(svc, env)
        run_year(svc, env)
        edu_legs = [x for x in env["ledger"] if x.get("category")
                    == "education"]
        self.assertEqual(edu_legs, [])
        self.assertEqual(env["player"].student_loan, 7000)

    def test_decline_and_leave_are_free(self):
        svc = make_service()
        env, pending = self._enroll_diploma(svc, 7)
        result = education.resolve_funding(env, env["player"], pending,
                                           "decline", random.Random(7))
        self.assertIsNone(result["pending_decision"])
        self.assertIsNone(education.active_enrollment(
            env, env["player"].person_id))
        # enroll again and leave mid-course: no refund, no credential
        result = dispatch(svc, env, "education_enroll",
                          {"program_id": "care_diploma"},
                          random.Random(7))
        education.resolve_funding(env, env["player"],
                                  result["pending_decision"], "self",
                                  random.Random(7))
        run_year(svc, env)
        money_before = env["player"].money
        leave = dispatch(svc, env, "education_leave", {})
        self.assertTrue(any("left" in ev["text"] for ev in leave["events"]))
        self.assertIsNone(education.active_enrollment(
            env, env["player"].person_id))
        self.assertEqual(env["player"].money, money_before)  # no refund
        self.assertEqual(env["education_state"]["qualifications"], [])
        # leaving is idempotent-guarded
        with self.assertRaises(PermissionError):
            education.apply_action(env, env["player"], "education_leave",
                                   {}, random.Random(1), svc)


class ProgressionTests(unittest.TestCase):
    def _enroll_self(self, svc, program_id, seed=7, age=18):
        st = new_adult_game(svc, seed, age=age)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        result = dispatch(svc, env, "education_enroll",
                          {"program_id": program_id},
                          random.Random(seed))
        pending = result["pending_decision"]
        if pending is not None:
            education.resolve_funding(env, env["player"], pending, "self",
                                      random.Random(seed))
        return env

    def test_completion_awards_once_and_persists(self):
        for seed in SEEDS:
            svc = make_service()
            env = self._enroll_self(svc, "care_diploma", seed)
            e = education.active_enrollment(env, env["player"].person_id)
            for year in range(2):
                dispatch(svc, env, "education_study", {})
                run_year(svc, env, seed)
            self.assertEqual(e["status"], "completed")
            self.assertEqual(e["progress"], 2)
            quals = env["education_state"]["qualifications"]
            self.assertEqual([q["qualification_id"] for q in quals],
                             ["care_diploma_cert"])
            # re-running the hook for the same turn cannot award twice
            run_year(svc, env, seed)
            self.assertEqual(len(env["education_state"]["qualifications"]),
                             1)
            # save/reload round-trip keeps the durable record
            svc._save(env)
            env2 = svc._load(env["game_id"])
            self.assertEqual(
                env2["education_state"]["qualifications"], quals)
            # careers bridge reads the same records (A's implemented key)
            from src.game.features.careers import core as careers_core
            self.assertIn("care_diploma_cert",
                          careers_core.earned_qualifications(env2))

    def test_study_guarantees_year_and_once_per_year(self):
        svc = make_service()
        env = self._enroll_self(svc, "business_bachelors")
        dispatch(svc, env, "education_study", {})
        with self.assertRaises(PermissionError):
            education.apply_action(env, env["player"], "education_study",
                                   {}, random.Random(1), svc)
        enrollments = env["education_state"]["enrollments"]
        e = next(iter(enrollments.values()))
        for year in range(4):
            run_year(svc, env, seed=999999)  # hostile roll: study overrides
            self.assertEqual(e["progress"], year + 1)
            if year < 3:
                dispatch(svc, env, "education_study", {})
        self.assertEqual(e["status"], "completed")
        self.assertEqual(e["progress"], 4)

    def test_failed_year_repeats_without_double_billing_same_turn(self):
        svc = make_service()
        env = self._enroll_self(svc, "care_diploma")

        class _FailRng:
            def random(self):
                return 0.0  # below FAIL_CHANCE_UNSUDIED -> fail

            def randint(self, a, b):
                return a

        e = education.active_enrollment(env, env["player"].person_id)
        env["player"].money = 50000
        result = run_year(svc, env, seed=_FailRng())
        self.assertIn("failed a year", result["events"][0]["text"])
        self.assertEqual(e["progress"], 0)
        self.assertEqual(e["attempts"], 1)
        # failure bills the repeated year again on the NEXT attempt, not
        # twice in the failed turn (single tuition leg this turn)
        edu_legs = [x for x in env["ledger"] if x.get("category")
                    == "education"]
        self.assertEqual(len(edu_legs), 1)
        # same-turn re-run is a no-op (once-per-turn guard)
        again = education.annual_hook(env, svc, _FailRng())
        self.assertEqual(again["events"], [])
        # repeating with study passes; the repeated year bills again
        dispatch(svc, env, "education_study", {})
        run_year(svc, env, seed=_FailRng())
        self.assertEqual(e["progress"], 1)
        self.assertEqual(e["attempts"], 0)
        edu_legs = [x for x in env["ledger"] if x.get("category")
                    == "education"]
        self.assertEqual(len(edu_legs), 2)
        dispatch(svc, env, "education_study", {})
        run_year(svc, env, seed=_FailRng())
        self.assertEqual(e["status"], "completed")
        self.assertEqual([q["qualification_id"]
                          for q in env["education_state"]["qualifications"]],
                         ["care_diploma_cert"])


class ApprenticeshipBridgeTests(unittest.TestCase):
    def test_joint_assignment_via_a_bridge_monkeypatched(self):
        """Forced joint route: A's real bridge is not landed yet, so the
        exact call contract is pinned with a stand-in (integration with
        A's assignment counted complete only after A lands it)."""
        from src.game.features.careers import core as careers_core

        created = {}

        def fake_create(env, person, role_id, employer_id):
            created["args"] = (role_id, employer_id)
            st = careers_core.ensure(env)
            st["assignments"][person.person_id] = {
                "assignment_id": "ca_test", "role_id": role_id,
                "employer_id": employer_id, "salary": 28000, "hours": 38,
                "accepted_turn": "t_test", "accepted_age": person.age,
                "role_tenure": 0, "status": "active"}
            careers_core.apply_adapter(env, person)

        def fake_close(env, person, reason):
            st = careers_core.ensure(env)
            st["assignments"].pop(person.person_id, None)
            created["closed"] = reason
            careers_core.apply_adapter(env, person)

        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        original_create = careers_core.create_apprenticeship_assignment
        original_close = careers_core.close_apprenticeship
        careers_core.create_apprenticeship_assignment = fake_create
        careers_core.close_apprenticeship = fake_close
        try:
            result = dispatch(svc, env, "education_enroll",
                              {"program_id": "app_electrical"},
                              random.Random(7))
            self.assertIsNone(result["pending_decision"])  # tuition-free
            e = education.active_enrollment(env, env["player"].person_id)
            self.assertEqual(e["funding"], "self")
            self.assertEqual(created["args"],
                             ("app_electrician", "voltworks"))
            # ONE wage: legacy payroll scalars synced by A's adapter only
            self.assertTrue(env["player"].has_job)
            self.assertEqual(env["player"].salary, 28000)
            # fee bills per year, wage never posted by education
            env["player"].money = 100000
            run_year(svc, env)
            self.assertEqual(env["player"].money, 100000 - 400)
            # materials fee occurrence, no wage legs from education
            legs = [x for x in env["ledger"] if x.get("category")
                    == "education"]
            self.assertEqual(len(legs), 1)
            self.assertEqual(legs[0]["amount"], -400)
            # leaving closes the joint assignment too
            dispatch(svc, env, "education_leave", {})
            self.assertEqual(created["closed"], "left_program")
            self.assertFalse(env["player"].has_job)
        finally:
            careers_core.create_apprenticeship_assignment = original_create
            careers_core.close_apprenticeship = original_close

    def test_leave_job_while_studying_and_leave_program(self):
        """Explicit trap tests: career active blocks non-apprentice
        enrollment; enrollment active blocks nothing on the careers side
        except its own one-commitment rule (careers_apply refuses an
        existing job, education_enroll refuses an existing assignment —
        leave either way is always legal)."""
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        # simulate an existing assignment via A's real adapter path
        st_c = careers_state = __import__(
            "src.game.features.careers.core", fromlist=["core"])
        st_c.ensure(env)["assignments"][env["player"].person_id] = {
            "assignment_id": "ca_9", "role_id": "office_assistant",
            "employer_id": "papertrail", "salary": 40000, "hours": 40,
            "accepted_turn": "t_x", "accepted_age": 18,
            "role_tenure": 1, "status": "active"}
        st_c.apply_adapter(env, env["player"])
        with self.assertRaises(PermissionError) as ctx:
            education.apply_action(env, env["player"], "education_enroll",
                                   {"program_id": "care_diploma"},
                                   random.Random(1), svc)
        self.assertIn("resign", str(ctx.exception))
        # leave the job (A's command path), then enrollment is legal
        st_c.cmd_resign(env, env["player"], {}, random.Random(1), svc)
        result = education.apply_action(
            env, env["player"], "education_enroll",
            {"program_id": "care_diploma"}, random.Random(1), svc)
        self.assertIsNotNone(result["pending_decision"])
        # leave program while decision pending is not possible (pending
        # gate is the service's job) — resolve then leave:
        education.resolve_funding(env, env["player"],
                                  result["pending_decision"], "self",
                                  random.Random(1))
        self.assertIsNotNone(education.active_enrollment(
            env, env["player"].person_id))
        dispatch(svc, env, "education_leave", {})
        self.assertIsNone(education.active_enrollment(
            env, env["player"].person_id))


class CommandPathIntegrationTests(unittest.TestCase):
    """Real GameService command path (A's dispatcher integration, M3
    contracts integration requests 1-8): enroll -> funding pending ->
    resolve -> study -> age_up progresses -> undo restores -> redo
    reproduces."""

    def test_command_path_enroll_age_undo(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "e1", "education_enroll",
                         {"program_id": "care_diploma"})
        d = st.get("pending_decision") or {}
        self.assertEqual(d.get("kind"), "education_funding")
        loan = next((o for o in d.get("options", [])
                     if o.get("id") == "student_loan"
                     and not o.get("disabled")), None)
        self.assertIsNotNone(loan, "student_loan option advertised")
        st = svc.command(gid, st["revision"], "e2", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": "student_loan", "inputs": {}})
        enr = (st.get("education_programs") or {}).get("enrollment") or {}
        self.assertEqual(enr.get("program_id"), "care_diploma")
        self.assertEqual(enr.get("status"), "active")
        self.assertEqual(enr.get("progress"), 0)
        st = svc.command(gid, st["revision"], "e3", "education_study", {})
        enr = st["education_programs"]["enrollment"]
        self.assertTrue(enr.get("studied_this_year"))
        st = svc.command(gid, st["revision"], "e4", "age_up", {})
        enr = st["education_programs"]["enrollment"]
        self.assertEqual(enr.get("progress"), 1,
                         "studied year must advance progress once")
        st = svc.command(gid, st["revision"], "e5", "undo_year", {})
        enr = (st.get("education_programs") or {}).get("enrollment") or {}
        self.assertEqual(enr.get("progress"), 0,
                         "undo restores the pre-age enrollment")
        st = svc.command(gid, st["revision"], "e6", "age_up", {})
        enr = st["education_programs"]["enrollment"]
        self.assertEqual(enr.get("progress"), 1,
                         "redo after undo must reproduce the same year")


class SharedCommitmentTests(unittest.TestCase):
    """R3 education side (Amendment 2): job/study conflicts come from
    A's ONE shared commitment decision — including the M2 legacy job
    the old feature-local check missed (review repro: a $50,000 legacy
    job silently replaced by an apprenticeship)."""

    def test_legacy_job_blocks_study_and_apprenticeship(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=25)
        env = load(svc, st["game_id"])
        finish_secondary(env)
        env["player"].has_job = True
        env["player"].salary = 50000
        for program_id in ("care_diploma", "app_electrical"):
            with self.assertRaises(PermissionError) as ctx:
                education.apply_action(env, env["player"], "education_enroll",
                                       {"program_id": program_id},
                                       random.Random(1), svc)
            self.assertIn("resign", str(ctx.exception))
        # projection advertises the same refusal (advertised == dispatch)
        view = education.education_view(env, env["player"])
        by_id = {p["program_id"]: p for p in view["programs"]}
        for program_id in ("care_diploma", "app_electrical"):
            self.assertFalse(by_id[program_id]["eligible"])
            self.assertIn("resign", by_id[program_id]["reason"])
        enroll_spec = next(s for s in education.action_specs(
            env, env["player"]) if s["id"] == "education_enroll")
        self.assertFalse(enroll_spec["enabled"])
        self.assertIn("resign", enroll_spec["reason"])
        # resigning the legacy job makes study legal again (M2 parity)
        env["player"].has_job = False
        env["player"].salary = 0
        view = education.education_view(env, env["player"])
        by_id = {p["program_id"]: p for p in view["programs"]}
        self.assertTrue(by_id["care_diploma"]["eligible"])
        enroll_spec = next(s for s in education.action_specs(
            env, env["player"]) if s["id"] == "education_enroll")
        self.assertTrue(enroll_spec["enabled"])
        self.assertIsNone(enroll_spec["reason"])


class TypedIdValidationTests(unittest.TestCase):
    """R5 (Codex consolidated review): malformed program ids are a typed
    400/ValueError BEFORE any lookup, draw, attempt or money mutation."""

    def test_enroll_rejects_malformed_program_ids(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        gid = st["game_id"]
        env = load(svc, gid)
        finish_secondary(env)
        svc._save(env)
        bad_payloads = [{}, {"program_id": None}, {"program_id": []},
                        {"program_id": {}}, {"program_id": True},
                        {"program_id": 17}, {"program_id": ""},
                        {"program_id": "nope"}]
        for payload in bad_payloads:
            with self.assertRaises(ValueError):
                education.apply_action(env, env["player"], "education_enroll",
                                       payload, random.Random(1), svc)
        # the refusals sampled, enrolled and billed nothing
        self.assertEqual(education.ensure(env)["enrollments"], {})
        self.assertIsNone(education.active_enrollment(
            env, env["player"].person_id))
        # production route: typed 400, no revision advance
        st = svc.get_state(gid)
        rev = st["revision"]
        for bad in ([], {}, True, "nope"):
            with self.assertRaises(GameError) as ctx:
                svc.command(gid, st["revision"], "q1", "education_enroll",
                            {"program_id": bad})
            self.assertEqual(ctx.exception.status, 400)
        self.assertEqual(svc.get_state(gid)["revision"], rev)


class OwnershipTests(unittest.TestCase):
    """R2 (Codex consolidated review): qualifications are person-scoped;
    a previous generation's enrollment never bills/advances/awards for a
    successor; the migration attributes ownerless records once."""

    def test_ownerless_records_migrate_to_current_player(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=20)
        env = load(svc, st["game_id"])
        pid = env["player"].person_id
        # pre-fix record shape (no owner), written around ensure()
        env["education_state"] = {
            "schema_version": 1, "next_num": 1, "enrollments": {},
            "qualifications": [{"qualification_id": "care_diploma_cert",
                                "program_id": "care_diploma",
                                "program_version": 1,
                                "awarded_turn": "t_old"}]}
        ids = education.earned_qualification_ids(env, pid)
        self.assertEqual(ids, {"care_diploma_cert"})
        self.assertEqual(education.earned_qualification_ids(env, "other"),
                         set())
        # migration stamped the owner once (persisted with the save)
        q = env["education_state"]["qualifications"][0]
        self.assertEqual(q["person_id"], pid)
        view = education.education_view(env, env["player"])
        self.assertEqual([x["qualification_id"]
                          for x in view["qualifications"]],
                         ["care_diploma_cert"])

    def test_succession_keeps_education_person_scoped(self):
        """Real continue_as lifecycle on the production command route:
        the parent's completed diploma stays the parent's; the parent's
        open nursing enrollment is dormant for the successor (no
        billing, no progress, no credential leak) across a real age_up."""
        svc = make_service()
        st = svc.new_game(
            {"first_name": "Ada", "last_name": "Lee", "gender": "female",
             "country": "Australia", "seed": 4242,
             "scenario": "married_with_child"}, "r1")
        gid = st["game_id"]
        env = svc._load(gid)
        player = env["player"]
        player.age = 30
        finish_secondary(env)
        # the shared commitment gate (Amendment 2) refuses study while a
        # legacy job is held — the parent quits before enrolling
        player.has_job = False
        player.salary = 0
        parent_id = player.person_id
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        kid = Child("Sam", player.lastname, Gender.Male, 55, 60,
                    player.parents.get("Mother"), player)
        kid.age = 18
        kid.appearance.seed = 4242
        player.children.append(kid)
        player.relations.append(kid)
        svc._save(env)

        st = svc.get_state(gid)

        def cmd(req, action, payload):
            nonlocal st
            st = svc.command(gid, st["revision"], req, action, payload)
            return st

        def drain(req_prefix):
            # bound-drained pending chain so scripted age_ups stay legal
            for i in range(10):
                d = st.get("pending_decision")
                if not d:
                    return
                opt = next(o for o in d["options"]
                           if o.get("enabled", True))
                cmd(f"{req_prefix}{i}", "resolve_decision",
                    {"decision_id": d["id"], "option_id": opt["id"]})

        # complete the care diploma (2 years, self-funded)
        cmd("e1", "education_enroll", {"program_id": "care_diploma"})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "education_funding")
        cmd("e2", "resolve_decision",
            {"decision_id": d["id"], "option_id": "self"})
        cmd("s1", "education_study", {})
        cmd("a1", "age_up", {})
        drain("d1")
        cmd("s2", "education_study", {})
        cmd("a2", "age_up", {})
        drain("d2")
        env = svc._load(gid)
        quals = env["education_state"]["qualifications"]
        self.assertEqual([q["qualification_id"] for q in quals],
                         ["care_diploma_cert"])
        self.assertEqual(quals[0]["person_id"], parent_id)
        # tuition legs belong to the enrollment owner
        edu_legs = [r for r in env["ledger"]
                    if r.get("category") == "education"]
        self.assertTrue(edu_legs)
        self.assertTrue(all(r["person_id"] == parent_id
                            for r in edu_legs))
        # then an OPEN nursing enrollment across succession
        cmd("e3", "education_enroll", {"program_id": "nursing_bachelors"})
        d = st["pending_decision"]
        cmd("e4", "resolve_decision",
            {"decision_id": d["id"], "option_id": "self"})
        cmd("a3", "age_up", {})
        drain("d3")
        env = svc._load(gid)
        nursing = education.active_enrollment(env, parent_id)
        self.assertIsNotNone(nursing)
        self.assertEqual(nursing["progress"], 1)
        # death via the real surrender double-confirm
        st = svc.get_state(gid)
        cmd("k1", "surrender", {})
        d = st["pending_decision"]
        final = next(o for o in d["options"]
                     if any(k in o["id"] for k in ("final", "warn")))
        cmd("k2", "resolve_decision",
            {"decision_id": d["id"], "option_id": final["id"]})
        d2 = st["pending_decision"]
        end = next(o for o in d2["options"]
                   if "surrender" in o["id"] or "end" in o["id"])
        cmd("k3", "resolve_decision",
            {"decision_id": d2["id"], "option_id": end["id"]})
        self.assertEqual(st["status"], "dead")
        target = st["succession"]["eligible"][0]["person_id"]
        cmd("c1", "continue_as", {"target_id": target})
        self.assertEqual(st["status"], "alive")
        self.assertEqual(st["player_id"], target)
        # the successor's real age_up must not touch the parent's course
        cmd("n1", "age_up", {})
        drain("d4")
        env = svc._load(gid)
        nursing2 = env["education_state"]["enrollments"][
            nursing["enrollment_id"]]
        self.assertEqual(nursing2["progress"], 1)
        self.assertEqual(nursing2["status"], "active")
        new_legs = [r for r in env["ledger"]
                    if r.get("category") == "education"
                    and r["person_id"] == target]
        self.assertEqual(new_legs, [])
        # no credential leak; the parent's records stay the parent's
        self.assertEqual(education.earned_qualification_ids(env, target),
                         set())
        self.assertEqual(education.earned_qualification_ids(env, parent_id),
                         {"care_diploma_cert"})
        view = education.education_view(env, env["player"])
        self.assertEqual(view["qualifications"], [])
        self.assertIsNone(view["enrollment"])
        # durable across save/reload
        svc._save(env)
        env2 = svc._load(gid)
        self.assertEqual({q["person_id"]
                          for q in env2["education_state"]["qualifications"]},
                         {parent_id})

    def test_apprenticeship_total_cost_includes_fees(self):
        svc = make_service()
        st = new_adult_game(svc, 7, age=18)
        env = load(svc, st["game_id"])
        view = education.education_view(env, env["player"])
        by_id = {p["program_id"]: p for p in view["programs"]}
        # total = (tuition + fee) * years; the annual fee stays explicit
        self.assertEqual(by_id["app_electrical"]["total_cost"], 1600)
        self.assertEqual(by_id["app_electrical"]["fee_per_year"], 400)
        self.assertEqual(by_id["app_electrical"]["tuition_per_year"], 0)
        self.assertEqual(by_id["care_diploma"]["total_cost"], 7000)


if __name__ == "__main__":
    unittest.main()
