"""M3 service integration tests (Kimi A, 2026-11-21).

Contract: docs/collab/M3/02-contracts.md — §Education integration
requests 1-9 (B) and §Skills requests 1/3/4 (C), wired through the real
GameService command boundary (whitelist + dispatch + resolve routes +
annual hooks + migration registry). Real service objects only; forced
state via env edits (no random-roll reliance); 3 hash seeds for the
sampling paths.

Run: TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_game_m3_integration
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402
from src.game.features.careers import core as careers  # noqa: E402
from src.game.features.skills import state as skills_state  # noqa: E402

SEEDS = [7, 4242, 90210]

DATA_DIR = os.path.join(os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(DATA_DIR, exist_ok=True)


def make_service():
    return GameService(tempfile.mkdtemp(dir=DATA_DIR))


def new_graduate_game(svc, seed, request="c", age=18, smarts=None,
                      money=50000):
    """Fresh game with the player force-settled as a secondary-school
    graduate with cash (env edits, no RNG)."""
    st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                       "gender": "female", "country": "Australia",
                       "seed": seed}, request)
    env = svc._load(st["game_id"])
    p = env["player"]
    p.age = age
    p.grades = None
    p.dropped_out = False
    p.money = money
    p.performance = 90  # keep any M2 dismissal roll from firing
    if smarts is not None:
        p.smarts = smarts
    svc._save(env)
    return svc.get_state(st["game_id"])


def grant_skills(svc, gid, **skills):
    env = svc._load(gid)
    from src.game.features.skills import state as skills_state
    values = skills_state.values(env, env["player"].person_id)
    values.update(skills)
    svc._save(env)


def drain_pending(svc, st, tag):
    """Resolve any interrupting pendings with the first enabled option
    (real M2 age_up flow can offer events; tests pin feature behavior,
    not the event deck)."""
    n = 0
    while st.get("pending_decision"):
        d = st["pending_decision"]
        opt = next(o for o in d["options"]
                   if o.get("enabled", True))
        st = svc.command(st["game_id"], st["revision"],
                         f"{tag}-d{n}", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": opt["id"]})
        n += 1
    return st


class EducationDispatchTests(unittest.TestCase):
    def test_enroll_unknown_program_is_typed_400(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "e1",
                        "education_enroll", {"program_id": "bogus"})
        self.assertEqual(cm.exception.code, "invalid_input")
        self.assertEqual(cm.exception.status, 400)

    def test_apprenticeship_enrolls_directly_and_creates_joint_assignment(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"app{seed}")
            self.assertIsNone(st["pending_decision"])
            st = svc.command(st["game_id"], st["revision"], f"app{seed}-e",
                             "education_enroll", {"program_id":
                                                  "app_electrical"})
            # tuition-free: no funding pending, immediate enrollment
            self.assertIsNone(st["pending_decision"])
            view = st["education_programs"]
            self.assertEqual(view["enrollment"]["program_id"],
                             "app_electrical")
            self.assertEqual(view["enrollment"]["status"], "active")
            # A's bridge: the joint paid apprenticeship assignment
            asn = st["careers"]["assignment"]
            self.assertIsNotNone(asn)
            self.assertEqual(asn["role_id"], "app_electrician")
            self.assertEqual(asn["employer_id"], "voltworks")
            roles, _ = careers.data()
            self.assertEqual(asn["salary"], roles["app_electrician"]
                             ["pay_min"])
            self.assertTrue(st["work"]["employed"])

    def test_funding_pending_resolve_self_and_legacy_keys_preserved(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"fd{seed}")
            st = svc.command(st["game_id"], st["revision"], f"fd{seed}-e",
                             "education_enroll", {"program_id":
                                                  "care_diploma"})
            d = st["pending_decision"]
            self.assertIsNotNone(d)
            self.assertEqual(d["kind"], "education_funding")
            self.assertEqual(d["display_category"], "education")
            self.assertIn("self", {o["id"] for o in d["options"]})
            # legacy M2 education view untouched on state["education"]
            self.assertIn("stage", st["education"])
            self.assertNotIn("programs", st["education"])
            self.assertFalse(st["education"]["enrolled"])
            # feature view is additive under education_programs
            self.assertIn("programs", st["education_programs"])
            st = svc.command(st["game_id"], st["revision"], f"fd{seed}-r",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id": "self"})
            self.assertIsNone(st["pending_decision"])
            self.assertEqual(
                st["education_programs"]["enrollment"]["program_id"],
                "care_diploma")
            self.assertEqual(
                st["education_programs"]["enrollment"]["funding"], "self")
            # no money moves at enroll/resolve time (bills post per year)
            self.assertEqual(st["money"], 50000)

    def test_funding_decline_is_free(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        st = svc.command(st["game_id"], st["revision"], "d1",
                         "education_enroll",
                         {"program_id": "business_bachelors"})
        d = st["pending_decision"]
        st = svc.command(st["game_id"], st["revision"], "d2",
                         "resolve_decision",
                         {"decision_id": d["id"], "option_id": "decline"})
        self.assertIsNone(st["pending_decision"])
        self.assertIsNone(st["education_programs"]["enrollment"])
        self.assertEqual(st["money"], 50000)

    def test_student_loan_advances_principal_once_no_cash_legs(self):
        svc = make_service()
        st = new_graduate_game(svc, 7, smarts=50)
        st = svc.command(st["game_id"], st["revision"], "l1",
                         "education_enroll",
                         {"program_id": "business_bachelors"})
        d = st["pending_decision"]
        self.assertIn("student_loan", {o["id"] for o in d["options"]})
        st = svc.command(st["game_id"], st["revision"], "l2",
                         "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": "student_loan"})
        self.assertEqual(st["money"], 50000)  # principal is a liability
        env = svc._load(st["game_id"])
        total = 9000 * 4
        self.assertEqual(env["player"].student_loan, total)
        self.assertTrue(env["player"].chose_student_loan)

    def test_leave_program_closes_apprenticeship(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        st = svc.command(st["game_id"], st["revision"], "lv1",
                         "education_enroll",
                         {"program_id": "app_electrical"})
        self.assertIsNotNone(st["careers"]["assignment"])
        st = svc.command(st["game_id"], st["revision"], "lv2",
                         "education_leave", {})
        self.assertIsNone(st["education_programs"]["enrollment"])
        self.assertEqual(st["education_programs"]["history"][0]["status"], "left")
        self.assertIsNone(st["careers"]["assignment"])
        self.assertFalse(st["work"]["employed"])
        self.assertEqual(st["careers"]["history"][0]["role_id"],
                         "app_electrician")

    def test_enroll_while_employed_is_refused_with_precise_reason(self):
        svc = make_service()
        st = new_graduate_game(svc, 7, request="job")
        # take the apprenticeship first (creates the joint job)
        st = svc.command(st["game_id"], st["revision"], "j1",
                         "education_enroll",
                         {"program_id": "app_electrical"})
        # a second enrollment is blocked (already enrolled) ...
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "j2",
                        "education_enroll", {"program_id": "care_diploma"})
        self.assertEqual(cm.exception.code, "not_allowed")
        self.assertIn("already enrolled", str(cm.exception))
        # ... and leaving the program also closes the joint assignment,
        # so a fresh non-apprentice enrollment is legal again
        st = svc.command(st["game_id"], st["revision"], "j3",
                         "education_leave", {})
        self.assertIsNone(st["careers"]["assignment"])
        # the job gate DOES apply while employed: recreate a job through
        # A's own bridge and try to enroll in a degree program
        env = svc._load(st["game_id"])
        careers.create_apprenticeship_assignment(
            env, env["player"], "app_electrician", "voltworks")
        svc._save(env)
        st = svc.get_state(st["game_id"])
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "j4",
                        "education_enroll", {"program_id": "care_diploma"})
        self.assertEqual(cm.exception.code, "not_allowed")
        self.assertIn("resign", str(cm.exception))

    def test_replay_of_enroll_is_byte_identical(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        st = svc.command(st["game_id"], st["revision"], "rp1",
                         "education_enroll", {"program_id": "care_diploma"})
        again = svc.command(st["game_id"], st["revision"], "rp1",
                            "education_enroll",
                            {"program_id": "care_diploma"})
        self.assertEqual(again, st)
        # and a retry after reload (fresh service read) too
        st2 = svc.command(st["game_id"], again["revision"], "rp1",
                          "education_enroll",
                          {"program_id": "care_diploma"})
        self.assertEqual(st2, st)


class TrainSkillDispatchTests(unittest.TestCase):
    def test_free_training_raises_skill_and_journals(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"tr{seed}")
            env = svc._load(st["game_id"])
            pid = env["player"].person_id
            before = skills_state.values(env, pid)["practical"]
            st = svc.command(st["game_id"], st["revision"], f"tr{seed}-1",
                             "train_skill", {"target_id": "train_practical"})
            env = svc._load(st["game_id"])
            after = skills_state.values(env, pid)["practical"]
            self.assertGreater(after, before)
            self.assertIn("skill", {j["kind"] for j in st["journal"]})
            # free option posts no money leg
            self.assertFalse([e for e in st["finances"]["ledger"]
                              if ":skills:" in e["source_key"]])
            # advertised spec shares the same enabled/reason source
            # (R4 dialect: one train_skill action per option target)
            spec = next(a for a in st["available_actions"]
                        if a["id"] == "train_skill"
                        and a.get("target_id") == "train_practical")
            self.assertTrue(spec["enabled"])

    def test_paid_training_posts_one_education_leg(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"tp{seed}")
            st = svc.command(st["game_id"], st["revision"], f"tp{seed}-1",
                             "train_skill",
                             {"target_id": "train_communication"})
            legs = [e for e in st["finances"]["ledger"]
                    if ":skills:" in e["source_key"]]
            self.assertEqual(len(legs), 1)
            self.assertEqual(legs[0]["category"], "education")
            self.assertEqual(legs[0]["amount"], -25)
            self.assertEqual(st["money"], 50000 - 25)
            # the occurrence key carries person/option/year/n
            self.assertIn("train_communication", legs[0]["source_key"])

    def test_train_unknown_option_400_and_limit_409(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "t1", "train_skill",
                        {"target_id": "bogus"})
        self.assertEqual(cm.exception.code, "invalid_input")
        # annual limit is 2 for train_practical; the third is refused
        # before any spend with the precise reason
        st = svc.command(st["game_id"], st["revision"], "t2", "train_skill",
                         {"target_id": "train_practical"})
        st = svc.command(st["game_id"], st["revision"], "t3", "train_skill",
                         {"target_id": "train_practical"})
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "t4", "train_skill",
                        {"target_id": "train_practical"})
        self.assertEqual(cm.exception.code, "not_allowed")
        self.assertEqual(cm.exception.status, 409)
        self.assertIn("2 times", str(cm.exception))

    def test_choose_skill_option_requires_skill(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "c1", "train_skill",
                        {"target_id": "train_evening_course"})
        self.assertEqual(cm.exception.code, "invalid_input")
        self.assertIn("skill", str(cm.exception))
        st = svc.command(st["game_id"], st["revision"], "c2", "train_skill",
                         {"target_id": "train_evening_course",
                          "skill": "care"})
        env = svc._load(st["game_id"])
        pid = env["player"].person_id
        self.assertGreaterEqual(skills_state.values(env, pid)["care"], 5)


class AnnualHookTests(unittest.TestCase):
    def test_tuition_leg_posts_once_and_study_guarantees_progress(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"yr{seed}")
            st = svc.command(st["game_id"], st["revision"], f"yr{seed}-e",
                             "education_enroll",
                             {"program_id": "care_diploma"})
            d = st["pending_decision"]
            st = svc.command(st["game_id"], st["revision"], f"yr{seed}-r",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id": "self"})
            st = svc.command(st["game_id"], st["revision"], f"yr{seed}-s",
                             "education_study", {})
            st = svc.command(st["game_id"], st["revision"], f"yr{seed}-a",
                             "age_up", {})
            legs = [e for e in st["finances"]["ledger"]
                    if ":edu:" in e["source_key"]]
            self.assertEqual(len(legs), 1, legs)
            self.assertEqual(legs[0]["amount"], -3500)
            self.assertEqual(legs[0]["category"], "education")
            self.assertEqual(st["education_programs"]["enrollment"]["progress"], 1)
            # second year bills the NEXT occurrence key, exactly once
            st = drain_pending(svc, st, f"yr{seed}")
            st = svc.command(st["game_id"], st["revision"], f"yr{seed}-s2",
                             "education_study", {})
            st = svc.command(st["game_id"], st["revision"], f"yr{seed}-a2",
                             "age_up", {})
            legs = [e for e in st["finances"]["ledger"]
                    if ":edu:" in e["source_key"]]
            self.assertEqual(len(legs), 2, legs)
            self.assertEqual(
                {n for n in ("edu:e_1:year:0", "edu:e_1:year:1")
                 if any((":" + n + ":") in l["source_key"] for l in legs)},
                {"edu:e_1:year:0", "edu:e_1:year:1"})

    def test_apprentice_year_wages_tenure_and_fee_leg(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"ap{seed}")
            st = svc.command(st["game_id"], st["revision"], f"ap{seed}-e",
                             "education_enroll",
                             {"program_id": "app_electrical"})
            st = svc.command(st["game_id"], st["revision"], f"ap{seed}-a",
                             "age_up", {})
            asn = st["careers"]["assignment"]
            self.assertEqual(asn["role_tenure"], 1)
            # exactly one wage payer: M2 payroll legs from the adapter
            env = svc._load(st["game_id"])
            self.assertTrue(env["player"].has_job)
            # the materials fee bills cash; tuition is 0
            fees = [e for e in st["finances"]["ledger"]
                    if ":edu:" in e["source_key"]]
            self.assertEqual(len(fees), 1)
            self.assertTrue(fees[0]["source_key"].endswith(":fee"))
            self.assertEqual(fees[0]["amount"], -400)
            # enrollment progressed a year (studied or 85% pass)
            self.assertGreaterEqual(
                st["education_programs"]["enrollment"]["progress"], 0)

    def test_qualification_awarded_at_completion_qualifies_next_turn(self):
        svc = make_service()
        st = new_graduate_game(svc, 7, age=17)
        st = svc.command(st["game_id"], st["revision"], "n1",
                         "education_enroll",
                         {"program_id": "care_diploma"})
        d = st["pending_decision"]
        st = svc.command(st["game_id"], st["revision"], "n2",
                         "resolve_decision",
                         {"decision_id": d["id"], "option_id": "self"})
        for n in range(2):
            st = drain_pending(svc, st, f"nq{n}")
            st = svc.command(st["game_id"], st["revision"], f"n3-{n}",
                             "education_study", {})
            st = svc.command(st["game_id"], st["revision"], f"n4-{n}",
                             "age_up", {})
        self.assertIsNone(st["education_programs"]["enrollment"])
        quals = {q["qualification_id"]
                 for q in st["education_programs"]["qualifications"]}
        self.assertIn("care_diploma_cert", quals)
        # frozen ordering: the credential awarded LAST turn is readable by
        # the hiring gates THIS turn (the remaining care-worker gates are
        # skill/experience, not the credential)
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "qualified_care_worker")
        cert = next(q for q in opening["requirements"]["qualifications"]
                    if q["id"] == "care_diploma_cert")
        self.assertTrue(cert["met"])
        self.assertNotIn("care_diploma_cert", opening["reason"] or "")
        self.assertIn("care skill", opening["reason"] or "")




class CommitmentGateTests(unittest.TestCase):
    """Amendment 2 (R3): ONE shared commitment decision, careers side."""

    def test_enrollment_blocks_careers_apply_with_shared_reason(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"cg{seed}")
            st = svc.command(st["game_id"], st["revision"], f"cg{seed}-e",
                             "education_enroll",
                             {"program_id": "care_diploma"})
            d = st["pending_decision"]
            st = svc.command(st["game_id"], st["revision"], f"cg{seed}-r",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id": "self"})
            # dispatch: refused BEFORE sampling (no offer appears)
            with self.assertRaises(GameError) as cm:
                svc.command(st["game_id"], st["revision"], f"cg{seed}-j",
                            "careers_apply",
                            {"role_id": "office_assistant",
                             "employer_id": "meridian"})
            self.assertEqual(cm.exception.code, "not_allowed")
            self.assertIn("program", str(cm.exception))
            env = svc._load(st["game_id"])
            self.assertEqual(env["careers"]["offers"], {})
            # advertised == dispatched (same enabled/reason source)
            st = svc.get_state(st["game_id"])
            spec = next(a for a in st["available_actions"]
                        if a["id"] == "careers_apply")
            self.assertFalse(spec["enabled"])
            self.assertIn("program", spec["reason"])

    def test_legacy_generic_job_can_transition_to_named_career(self):
        # R3: legacy employment must NOT block careers_apply — the
        # accepted offer overwrites legacy pay through the one adapter
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"lg{seed}")
            grant_skills(svc, st["game_id"], communication=50)
            env = svc._load(st["game_id"])
            env["player"].has_job = True
            env["player"].salary = 50000
            svc._save(env)
            st = svc.get_state(st["game_id"])
            st = svc.command(st["game_id"], st["revision"], f"lg{seed}-a",
                             "careers_apply",
                             {"role_id": "office_assistant",
                              "employer_id": "meridian"})
            self.assertEqual(st["pending_decision"]["kind"], "career_offer")

    def test_resign_and_leave_keep_free_alternatives_legal(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        st = svc.command(st["game_id"], st["revision"], "f1",
                         "education_enroll",
                         {"program_id": "app_electrical"})
        st = svc.command(st["game_id"], st["revision"], "f2",
                         "education_leave", {})
        spec = next(a for a in st["available_actions"]
                    if a["id"] == "careers_apply")
        self.assertTrue(spec["enabled"], spec["reason"])
        st = svc.command(st["game_id"], st["revision"], "f3",
                         "age_up", {})
        self.assertEqual(st["status"], "alive")


class TypedIdTests(unittest.TestCase):
    """R5: ID types validated before dictionary lookup — typed 4xx with
    no revision/RNG/attempt mutation, no catch-all."""

    def _assert_typed(self, svc, st, payload, tag):
        revision = st["revision"]
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], revision, tag, "careers_apply",
                        payload)
        self.assertEqual(cm.exception.code, "invalid_input")
        self.assertEqual(cm.exception.status, 400)
        st2 = svc.get_state(st["game_id"])
        self.assertEqual(st2["revision"], revision)

    def test_list_object_bool_ids_are_typed_400(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"t{seed}")
            self._assert_typed(svc, st, {"role_id": [], "employer_id":
                                         "meridian"}, f"t{seed}-1")
            self._assert_typed(svc, st, {"role_id": "office_assistant",
                                         "employer_id": {"x": 1}},
                               f"t{seed}-2")
            self._assert_typed(svc, st, {"role_id": True, "employer_id":
                                         "meridian"}, f"t{seed}-3")
            self._assert_typed(svc, st, {"role_id": "", "employer_id":
                                         "meridian"}, f"t{seed}-4")

    def test_failed_attempts_do_not_consume_the_yearly_cap(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        grant_skills(svc, st["game_id"], communication=50)
        st = svc.get_state(st["game_id"])
        for i, payload in enumerate(
                ({"role_id": [], "employer_id": "meridian"},
                 {"role_id": None, "employer_id": "meridian"})):
            with self.assertRaises(GameError):
                svc.command(st["game_id"], st["revision"], f"x{i}",
                            "careers_apply", payload)
        # both real attempts of the year are still available
        for i in range(2):
            st = svc.command(st["game_id"], st["revision"], f"ok{i}",
                             "careers_apply",
                             {"role_id": "office_assistant",
                              "employer_id": "meridian"})
            d = st["pending_decision"]
            st = svc.command(st["game_id"], st["revision"], f"ok{i}d",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id":
                              "decline"})
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], st["revision"], "over",
                        "careers_apply",
                        {"role_id": "office_assistant",
                         "employer_id": "meridian"})
        self.assertEqual(cm.exception.code, "not_allowed")
        self.assertIn("twice", str(cm.exception))

    def test_promote_validates_id_type_too(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        st = svc.command(st["game_id"], st["revision"], "p1",
                         "education_enroll",
                         {"program_id": "app_electrical"})
        env = svc._load(st["game_id"])
        env["player"].performance = 90
        svc._save(env)
        revision = svc.get_state(st["game_id"])["revision"]
        with self.assertRaises(GameError) as cm:
            svc.command(st["game_id"], revision, "p2", "careers_promote",
                        {"role_id": ["electrician"]})
        self.assertEqual(cm.exception.code, "invalid_input")
        self.assertEqual(svc.get_state(st["game_id"])["revision"],
                         revision)


class PersonScopedQualificationsTests(unittest.TestCase):
    """R2 (careers side): earned qualifications are person-scoped."""

    def test_other_persons_qualification_never_satisfies_the_gate(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"q{seed}")
            env = svc._load(st["game_id"])
            edu = env.setdefault("education_state", {})
            edu["qualifications"] = [{
                "qualification_id": "nursing_degree",
                "program_id": "nursing_bachelors", "program_version": 1,
                "awarded_turn": "t_x", "person_id": "someone_else"}]
            svc._save(env)
            st = svc.get_state(st["game_id"])
            opening = next(o for o in st["careers"]["openings"]
                           if o["role_id"] == "nurse")
            cert = next(q for q in
                        opening["requirements"]["qualifications"]
                        if q["id"] == "nursing_degree")
            self.assertFalse(cert["met"])
            # ... and the same record owned by the player satisfies it
            env = svc._load(st["game_id"])
            env["education_state"]["qualifications"][0][
                "person_id"] = env["player"].person_id
            svc._save(env)
            st = svc.get_state(st["game_id"])
            opening = next(o for o in st["careers"]["openings"]
                           if o["role_id"] == "nurse")
            cert = next(q for q in
                        opening["requirements"]["qualifications"]
                        if q["id"] == "nursing_degree")
            self.assertTrue(cert["met"])

    def test_legacy_ownerless_records_stay_readable_until_migration(self):
        # pre-R2 saves have ownerless records; B's migration stamps them.
        # Until then they must not silently stop satisfying gates.
        svc = make_service()
        st = new_graduate_game(svc, 7)
        env = svc._load(st["game_id"])
        env["education_state"]["qualifications"] = [{
            "qualification_id": "nursing_degree",
            "program_id": "nursing_bachelors", "program_version": 1,
            "awarded_turn": "t_legacy"}]
        svc._save(env)
        st = svc.get_state(st["game_id"])
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "nurse")
        cert = next(q for q in opening["requirements"]["qualifications"]
                    if q["id"] == "nursing_degree")
        self.assertTrue(cert["met"])





class M3bCareersCoreTests(unittest.TestCase):
    """M3b: dismissal/resignation/retirement through M2, legacy job-loss
    reconciliation, salary history + pension across reload, employer
    change, and human-readable requirement reasons."""

    def _employ(self, svc, st, tag, role="office_assistant",
                employer="meridian"):
        st = svc.command(st["game_id"], st["revision"], f"{tag}-a",
                         "careers_apply", {"role_id": role,
                                           "employer_id": employer})
        d = st["pending_decision"]
        return svc.command(st["game_id"], st["revision"], f"{tag}-r",
                           "resolve_decision",
                           {"decision_id": d["id"], "option_id": "accept"})

    def test_quit_job_reconciles_named_assignment_immediately(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"q{seed}")
            grant_skills(svc, st["game_id"], communication=50)
            st = svc.get_state(st["game_id"])
            st = self._employ(svc, st, f"q{seed}")
            self.assertIsNotNone(st["careers"]["assignment"])
            st = svc.command(st["game_id"], st["revision"], f"q{seed}-quit",
                             "quit_job", {})
            self.assertFalse(st["work"]["employed"])
            self.assertIsNone(st["careers"]["assignment"])
            self.assertEqual(st["careers"]["history"][0]["reason"],
                             "dismissed")
            # recovery: applying again is legal right away
            spec = next(a for a in st["available_actions"]
                        if a["id"] == "careers_apply")
            self.assertTrue(spec["enabled"], spec["reason"])

    def test_retirement_closes_assignment_pension_survives_reload(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"rt{seed}")
            grant_skills(svc, st["game_id"], communication=50)
            st = svc.get_state(st["game_id"])
            st = self._employ(svc, st, f"rt{seed}")
            env = svc._load(st["game_id"])
            p = env["player"]
            p.age = 66
            p.years_worked = 12
            p.salary_years = [p.salary] * 12
            svc._save(env)
            st = svc.get_state(st["game_id"])
            st = svc.command(st["game_id"], st["revision"],
                             f"rt{seed}-ret", "retire", {})
            self.assertIsNone(st["careers"]["assignment"])
            self.assertEqual(st["careers"]["history"][0]["reason"],
                             "retired")
            pension = st["work"]["pension"]
            self.assertGreater(pension, 0)
            # the pension basis survives a REAL reload (fresh service)
            reloaded = GameService(svc.save_dir)
            st2 = reloaded.get_state(st["game_id"])
            self.assertEqual(st2["work"]["pension"], pension)
            self.assertEqual(st2["careers"]["history"][0]["reason"],
                             "retired")
            self.assertIsNone(st2["careers"]["assignment"])

    def test_forced_legacy_job_loss_reconciles_at_year_boundary(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"f{seed}")
            grant_skills(svc, st["game_id"], communication=50)
            st = svc.get_state(st["game_id"])
            st = self._employ(svc, st, f"f{seed}")
            tenure_before = st["careers"]["assignment"]["role_tenure"]
            # simulate the legacy payroll authority ending the job
            # (dismissal/firing paths all funnel through lose_job)
            env = svc._load(st["game_id"])
            env["player"].lose_job()
            svc._save(env)
            st = svc.command(st["game_id"], st["revision"], f"f{seed}-age",
                             "age_up", {})
            self.assertIsNone(st["careers"]["assignment"])
            hist = st["careers"]["history"][0]
            self.assertEqual(hist["reason"], "dismissed")
            self.assertEqual(hist["role_tenure"], tenure_before,
                             "tenure must not accrue after the job ended")

    def test_yearly_retirement_offer_reconciles_on_accept(self):
        # Seed 90210 deterministically offers retirement on this year;
        # exercise the stored decision route, not the direct retire action.
        svc = make_service()
        st = new_graduate_game(svc, 90210)
        gid = st["game_id"]
        grant_skills(svc, gid, communication=50)
        st = self._employ(svc, svc.get_state(gid), "offer-retire")
        env = svc._load(gid)
        person = env["player"]
        person.age = 66
        person.years_worked = 12
        person.salary_years = [person.salary] * 12
        person.performance = 90
        svc._save(env)
        st = svc.command(gid, st["revision"], "offer-retire-year",
                         "age_up", {})
        for i in range(16):
            decision = st.get("pending_decision")
            self.assertIsNotNone(decision, "expected the retirement offer")
            option = ("retire" if decision["kind"] == "retirement_offer"
                      else next(o["id"] for o in decision["options"]
                                if o.get("enabled", True)))
            st = svc.command(gid, st["revision"], f"offer-retire-{i}",
                             "resolve_decision",
                             {"decision_id": decision["id"],
                              "option_id": option, "inputs": {}})
            if decision["kind"] == "retirement_offer":
                break
        else:
            self.fail("retirement offer did not appear within bound")
        self.assertTrue(st["work"]["retired"])
        self.assertIsNone(st["careers"]["assignment"])
        self.assertEqual(st["careers"]["history"][-1]["reason"], "retired")
        reloaded = GameService(svc.save_dir).get_state(gid)
        self.assertIsNone(reloaded["careers"]["assignment"])
        self.assertEqual(reloaded["work"]["pension"], st["work"]["pension"])

    def test_employer_change_resign_apply_accept(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"ec{seed}")
            # joint apprenticeship at voltworks, then qualify as
            # electrician and move to brightspark
            st = svc.command(st["game_id"], st["revision"], f"ec{seed}-e",
                             "education_enroll",
                             {"program_id": "app_electrical"})
            grant_skills(svc, st["game_id"], practical=40)
            st = svc.get_state(st["game_id"])
            self.assertEqual(st["careers"]["assignment"]["employer_id"],
                             "voltworks")
            # finish the apprenticeship (completion keeps the joint
            # assignment active as the promotion gate), then change
            # employer: resign from voltworks, join brightspark
            for n in range(4):
                st = drain_pending(svc, st, f"ec{seed}-{n}")
                st = svc.command(st["game_id"], st["revision"],
                                 f"ec{seed}-s{n}", "education_study", {})
                st = svc.command(st["game_id"], st["revision"],
                                 f"ec{seed}-y{n}", "age_up", {})
            st = drain_pending(svc, st, f"ec{seed}-fin")
            self.assertIsNone(st["education_programs"]["enrollment"])
            quals = {q["qualification_id"]
                     for q in st["education_programs"]["qualifications"]}
            self.assertIn("electrical_trade_cert", quals)
            self.assertEqual(st["careers"]["assignment"]["employer_id"],
                             "voltworks")
            st = svc.command(st["game_id"], st["revision"], f"ec{seed}-r1",
                             "careers_resign", {})
            st = svc.command(st["game_id"], st["revision"], f"ec{seed}-a",
                             "careers_apply",
                             {"role_id": "electrician",
                              "employer_id": "brightspark"})
            d = st["pending_decision"]
            st = svc.command(st["game_id"], st["revision"], f"ec{seed}-acc",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id":
                              "accept"})
            asn = st["careers"]["assignment"]
            self.assertEqual(asn["role_id"], "electrician")
            self.assertEqual(asn["employer_id"], "brightspark")
            reasons = [h["reason"] for h in st["careers"]["history"]]
            self.assertIn("left", reasons)
            env = svc._load(st["game_id"])
            self.assertTrue(env["player"].has_job)
            self.assertEqual(env["player"].salary, asn["salary"])


    def test_fresh_hire_survives_first_age_up_without_shortcuts(self):
        # Codex defect: the adapter left performance/stress at the
        # cleared 0/0, so M2's performance firing (<15) fired on the
        # FIRST age_up. Real new life, real accept, no stat pinning.
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"fh{seed}")
            grant_skills(svc, st["game_id"], communication=50)
            st = svc.get_state(st["game_id"])
            st = self._employ(svc, st, f"fh{seed}")
            env = svc._load(st["game_id"])
            self.assertEqual(env["player"].performance, 50)
            self.assertEqual(env["player"].stress, 45)
            self.assertEqual(env["player"].years_worked, 0)
            svc._save(env)
            st = svc.command(st["game_id"], st["revision"], f"fh{seed}-y",
                             "age_up", {})
            self.assertTrue(st["work"]["employed"],
                            "fresh hire must not be fired on year one")
            fired = [e for e in st["journal"]
                     if "fired" in e["text"].lower()]
            self.assertFalse(fired)
            self.assertIsNotNone(st["careers"]["assignment"])
            self.assertEqual(st["careers"]["assignment"]["role_tenure"],
                             1)
            env = svc._load(st["game_id"])
            self.assertEqual(len(env["player"].salary_years), 1)
            # job practice (C's hook) could read the active role
            self.assertTrue(env["player"].has_job)

    def test_apprentice_start_gets_the_same_baseline(self):
        # the apprenticeship bridge runs through the same adapter
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"ap{seed}")
            st = svc.command(st["game_id"], st["revision"], f"ap{seed}-e",
                             "education_enroll",
                             {"program_id": "app_electrical"})
            env = svc._load(st["game_id"])
            self.assertEqual(env["player"].performance, 50)
            self.assertEqual(env["player"].stress, 45)

    def test_promotion_keeps_ongoing_job_state(self):
        # Amendment 3: ongoing employment must NOT be re-baselined
        for seed in (7,):
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"pr{seed}")
            # apprentice -> complete -> promote to electrician
            st = svc.command(st["game_id"], st["revision"], f"pr{seed}-e",
                             "education_enroll",
                             {"program_id": "app_electrical"})
            grant_skills(svc, st["game_id"], practical=40)
            for n in range(4):
                st = drain_pending(svc, st, f"pr{seed}-{n}")
                st = svc.command(st["game_id"], st["revision"],
                                 f"pr{seed}-s{n}", "education_study", {})
                st = svc.command(st["game_id"], st["revision"],
                                 f"pr{seed}-y{n}", "age_up", {})
            st = drain_pending(svc, st, f"pr{seed}-fin")
            env = svc._load(st["game_id"])
            env["player"].performance = 73
            env["player"].stress = 31
            env["player"].years_worked = 4
            env["player"].salary_years = [20000, 21000, 22000, 23000]
            svc._save(env)
            st = svc.get_state(st["game_id"])
            st = svc.command(st["game_id"], st["revision"], f"pr{seed}-p",
                             "careers_promote", {"role_id": "electrician"})
            d = st["pending_decision"]
            self.assertEqual(d["kind"], "career_offer")
            st = svc.command(st["game_id"], st["revision"], f"pr{seed}-a",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id":
                              "accept"})
            env = svc._load(st["game_id"])
            self.assertEqual(env["player"].performance, 73)
            self.assertEqual(env["player"].stress, 31)
            self.assertEqual(env["player"].years_worked, 4)
            self.assertEqual(env["player"].salary_years,
                             [20000, 21000, 22000, 23000])

    def test_requirement_reason_uses_labels_not_raw_ids(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        st = svc.get_state(st["game_id"])
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "operations_specialist")
        self.assertFalse(opening["eligible"])
        self.assertIn("Bachelor of Business", opening["reason"])
        self.assertIn(" or ", opening["reason"])
        self.assertNotIn("business_degree", opening["reason"])
        self.assertNotIn("or Coordinator", opening["reason"].replace(
            " or Coordinator", ""))
        # the qualification view carries human labels too
        nurse = next(o for o in st["careers"]["openings"]
                     if o["role_id"] == "nurse")
        cert = next(q for q in nurse["requirements"]["qualifications"]
                    if q["id"] == "nursing_degree")
        self.assertEqual(cert["label"], "Bachelor of Nursing")
        self.assertIn("Bachelor of Nursing", nurse["reason"])




def _wrk_stu_ids():
    from src.game import events as event_mod
    loaded = event_mod.load_event_files()
    return [e["id"] for e in loaded
            if e.get("schema_version") == 3
            and e["id"].startswith(("wrk_", "stu_"))]


def _seed_wrkstu_seen(svc, gid, keep):
    """Seen-cadence forcing through the REAL envelope field: every
    wrk_/stu_ record except `keep` is marked already offered, so the
    yearly weighted draw has exactly one eligible candidate."""
    env = svc._load(gid)
    env["wrk_stu_seen"] = [i for i in _wrk_stu_ids() if i not in keep]
    svc._save(env)


def _drain_until_wrkstu(svc, st, tag, bound=15):
    """Age (resolving unrelated pendings with their first enabled free
    option) until a wrk_/stu_ pending is offered, or assert failure."""
    for i in range(bound):
        st = svc.command(st["game_id"], st["revision"], f"{tag}-y{i}",
                         "age_up", {})
        d = st["pending_decision"]
        if d is None:
            continue
        if d.get("kind", "").startswith(("wrk_", "stu_")):
            return st, d
        st = drain_pending(svc, st, f"{tag}-d{i}")
    raise AssertionError("no wrk_/stu_ event offered within bound")


class WorkplaceStudyRouteTests(unittest.TestCase):
    """Amendment 4 (M3b B1): the wrk_/stu_ yearly route through the
    existing v3 pipeline — forced real age_up, reload, single posting,
    undo/redo, closed gates."""

    def _apprentice(self, svc, seed):
        st = new_graduate_game(svc, seed, request=f"w{seed}")
        st = svc.command(st["game_id"], st["revision"], f"w{seed}-e",
                         "education_enroll",
                         {"program_id": "app_electrical"})
        self.assertIsNone(st["pending_decision"])
        return st

    def test_wrk_and_stu_offer_and_resolve_in_one_year(self):
        for seed in SEEDS:
            svc = make_service()
            st = self._apprentice(svc, seed)
            _seed_wrkstu_seen(svc, st["game_id"],
                              keep=("wrk_skill_refresh",
                                    "stu_group_project"))
            st = svc.get_state(st["game_id"])
            env = svc._load(st["game_id"])
            pid = env["player"].person_id
            from src.game.features.skills import state as skills_state
            smarts_before = skills_state.values(env, pid) and env[
                "player"].smarts
            happy_before = env["player"].happiness
            st, d = _drain_until_wrkstu(svc, st, f"w{seed}")
            self.assertEqual(d["kind"], "wrk_skill_refresh")
            # resolve with the stat-training option; effects apply once
            opt = next(o for o in d["options"]
                       if o["id"].endswith("_attend"))
            st = svc.command(st["game_id"], st["revision"], f"w{seed}-r1",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opt["id"]})
            # the _keep_phase resume serves the study family next
            d2 = st["pending_decision"]
            self.assertIsNotNone(d2, "study family must follow in-year")
            self.assertEqual(d2["kind"], "stu_group_project")
            opt2 = next(o for o in d2["options"]
                        if o.get("enabled", True))
            st = svc.command(st["game_id"], st["revision"], f"w{seed}-r2",
                             "resolve_decision",
                             {"decision_id": d2["id"],
                              "option_id": opt2["id"]})
            env = svc._load(st["game_id"])
            # life-cadence keeps earlier (seeded) ids; the two offers
            # land at the tail, wrk first
            self.assertEqual(env["wrk_stu_seen"][-2:],
                             ["wrk_skill_refresh", "stu_group_project"])
            # the wrk choice applied EXACTLY once, with the authored
            # deltas recorded on its own journal entry (M2 yearly drift
            # also moves stats, so the event entry is the precise pin)
            self.assertGreater(env["player"].smarts, smarts_before)
            outcomes = [e for e in env["journal"]
                        if "refresher workshop" in e["text"]]
            self.assertEqual(len(outcomes), 1, outcomes)
            self.assertEqual(outcomes[0]["changes"],
                             {"smarts": 3, "happiness": 2, "stress": 1})
            # single posting: no duplicate ledger keys, no wrk/stu money
            keys = [e["source_key"] for e in env["ledger"]]
            self.assertEqual(len(keys), len(set(keys)))
            self.assertFalse([e for e in env["ledger"]
                              if ":wrk_" in e["source_key"]
                              or ":stu_" in e["source_key"]])

    def test_pending_survives_reload_and_resolves_once(self):
        for seed in SEEDS:
            svc = make_service()
            st = self._apprentice(svc, seed)
            _seed_wrkstu_seen(svc, st["game_id"],
                              keep=("wrk_skill_refresh",))
            st = svc.get_state(st["game_id"])
            st, d = _drain_until_wrkstu(svc, st, f"rl{seed}")
            self.assertEqual(d["kind"], "wrk_skill_refresh")
            # reload through a FRESH service before resolving
            reloaded = GameService(svc.save_dir)
            st2 = reloaded.get_state(st["game_id"])
            self.assertEqual(st2["pending_decision"]["kind"],
                             "wrk_skill_refresh")
            opt = next(o for o in st2["pending_decision"]["options"]
                       if o["id"].endswith("_attend"))
            payload = {"decision_id": st2["pending_decision"]["id"],
                       "option_id": opt["id"]}
            st2 = reloaded.command(st["game_id"], st2["revision"],
                                   f"rl{seed}-r", "resolve_decision",
                                   payload)
            env = reloaded._load(st["game_id"])
            outcomes = [e for e in env["journal"]
                        if "refresher workshop" in e["text"]]
            self.assertEqual(len(outcomes), 1)
            # retry is byte-identical (single posting under replay)
            again = reloaded.command(st["game_id"], st2["revision"],
                                     f"rl{seed}-r", "resolve_decision",
                                     payload)
            self.assertEqual(again, st2)

    def test_undo_rolls_back_and_redo_replays_identically(self):
        for seed in SEEDS:
            svc = make_service()
            st = self._apprentice(svc, seed)
            _seed_wrkstu_seen(svc, st["game_id"],
                              keep=("wrk_skill_refresh",
                                    "stu_group_project"))
            st = svc.get_state(st["game_id"])
            st, d = _drain_until_wrkstu(svc, st, f"u{seed}")
            opt = next(o for o in d["options"]
                       if o["id"].endswith("_attend"))
            st = svc.command(st["game_id"], st["revision"], f"u{seed}-r1",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opt["id"]})
            d2 = st["pending_decision"]
            opt2 = next(o for o in d2["options"]
                        if o.get("enabled", True))
            st = svc.command(st["game_id"], st["revision"], f"u{seed}-r2",
                             "resolve_decision",
                             {"decision_id": d2["id"],
                              "option_id": opt2["id"]})
            texts_before = [(e["kind"], e["text"]) for e in
                            svc._load(st["game_id"])["journal"]]
            seeded = [i for i in _wrk_stu_ids()
                      if i not in ("wrk_skill_refresh",
                                   "stu_group_project")]
            st = svc.command(st["game_id"], st["revision"], f"u{seed}-undo",
                             "undo_year", {})
            env = svc._load(st["game_id"])
            # undo restores the pre-age snapshot: the year's two offers
            # roll back, the seeded window remains
            self.assertEqual(env.get("wrk_stu_seen"), seeded)
            # redo: restored RNG => the SAME offers in the SAME order
            st, d = _drain_until_wrkstu(svc, st, f"u{seed}re")
            self.assertEqual(d["kind"], "wrk_skill_refresh")

    def test_gates_stay_closed_without_the_active_source(self):
        # unemployed + not enrolled: neither family may ever be offered
        svc = make_service()
        st = new_graduate_game(svc, 7, request="gate")
        st = svc.get_state(st["game_id"])
        for i in range(3):
            st = drain_pending(svc, st, f"gate-{i}")
            st = svc.command(st["game_id"], st["revision"], f"g-{i}",
                             "age_up", {})
            if st["status"] != "alive":
                break
            self.assertFalse(
                (st["pending_decision"] or {}).get("kind", "")
                .startswith(("wrk_", "stu_")))
        env = svc._load(st["game_id"])
        seen = env.get("wrk_stu_seen") or []
        self.assertFalse([i for i in seen if i.startswith(("wrk_", "stu_"))])

    def test_wrong_role_record_stays_closed(self):
        # office worker: the electrician-only wrk_wrong_cable must not
        # be offered even as the only unseen record
        svc = make_service()
        st = new_graduate_game(svc, 7, request="role")
        grant_skills(svc, st["game_id"], communication=50)
        st = svc.get_state(st["game_id"])
        st = svc.command(st["game_id"], st["revision"], "role-a",
                         "careers_apply",
                         {"role_id": "office_assistant",
                          "employer_id": "meridian"})
        d = st["pending_decision"]
        st = svc.command(st["game_id"], st["revision"], "role-r",
                         "resolve_decision",
                         {"decision_id": d["id"], "option_id": "accept"})
        _seed_wrkstu_seen(svc, st["game_id"], keep=("wrk_wrong_cable",))
        st = svc.get_state(st["game_id"])
        for i in range(3):
            st = drain_pending(svc, st, f"role-{i}")
            st = svc.command(st["game_id"], st["revision"], f"ro-{i}",
                             "age_up", {})
            if st["status"] != "alive":
                break
            self.assertFalse(
                (st["pending_decision"] or {}).get("kind", "")
                .startswith(("wrk_", "stu_")))
        env = svc._load(st["game_id"])
        seen = env.get("wrk_stu_seen") or []
        self.assertNotIn("wrk_wrong_cable", seen)




class M3cLadderAndDriftTests(unittest.TestCase):
    """Amendment 5 (M3c): dual routes on every ladder, paired openings,
    and the parent-drift line cap."""

    def _opening(self, st, role_id):
        return next(o for o in st["careers"]["openings"]
                    if o["role_id"] == role_id)

    def test_trade_ladder_has_degree_and_non_degree_routes(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"dg{seed}")
            grant_skills(svc, st["game_id"], practical=40)
            env = svc._load(st["game_id"])
            env["education_state"]["qualifications"] = [{
                "qualification_id": "business_degree",
                "program_id": "business_bachelors", "program_version": 1,
                "awarded_turn": "t_x"}]
            svc._save(env)
            st = svc.get_state(st["game_id"])
            opening = self._opening(st, "electrician")
            self.assertTrue(opening["eligible"], opening["reason"])
            # dispatch agrees (advertised == dispatched): apply works
            # with NO trade certificate and NO apprenticeship history
            st = svc.command(st["game_id"], st["revision"], f"dg{seed}-a",
                             "careers_apply",
                             {"opening": "electrician@voltworks"})
            self.assertEqual(st["pending_decision"]["kind"],
                             "career_offer")

    def test_electrician_routes_line_advertises_both(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        grant_skills(svc, st["game_id"], practical=40)
        st = svc.get_state(st["game_id"])
        opening = self._opening(st, "electrician")
        self.assertFalse(opening["eligible"])
        self.assertIn("Electrical trade certificate", opening["reason"])
        self.assertIn("Bachelor of Business", opening["reason"])
        self.assertIn(" or ", opening["reason"])

    def test_healthcare_ladder_non_degree_route_to_nurse(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"nd{seed}")
            grant_skills(svc, st["game_id"], care=45, communication=30)
            env = svc._load(st["game_id"])
            env["player"].age = 26
            env["education_state"]["qualifications"] = [{
                "qualification_id": "care_diploma_cert",
                "program_id": "care_diploma", "program_version": 1,
                "awarded_turn": "t_x"}]
            pid = env["player"].person_id
            env["careers"]["assignments"][pid] = {
                "assignment_id": "ca_9", "role_id":
                "qualified_care_worker", "employer_id": "bluegum",
                "salary": 45000, "hours": 38, "accepted_turn": "t_x",
                "accepted_age": 26, "role_tenure": 8, "status": "active"}
            env["player"].performance = 70
            svc._save(env)
            st = svc.get_state(st["game_id"])
            # NO nursing degree — the diploma + 8-years route qualifies
            opening = self._opening(st, "nurse")
            self.assertTrue(opening["eligible"], opening["reason"])
            st = svc.command(st["game_id"], st["revision"], f"nd{seed}-p",
                             "careers_promote", {"role_id": "nurse"})
            self.assertEqual(st["pending_decision"]["kind"],
                             "career_offer")
            d = st["pending_decision"]
            st = svc.command(st["game_id"], st["revision"], f"nd{seed}-pd",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": "decline"})
            # the same person with only 7 years does NOT qualify, and
            # the advertised reason names the experience route
            env = svc._load(st["game_id"])
            env["careers"]["assignments"][pid]["role_tenure"] = 7
            svc._save(env)
            st = svc.get_state(st["game_id"])
            opening = self._opening(st, "nurse")
            self.assertFalse(opening["eligible"])
            self.assertIn("8 years as a Qualified care worker",
                          opening["reason"])
            with self.assertRaises(GameError) as cm:
                svc.command(st["game_id"], st["revision"], f"nd{seed}-p2",
                            "careers_promote", {"role_id": "nurse"})
            self.assertEqual(cm.exception.code, "not_allowed")

    def test_office_ladder_degree_route_reaches_the_top(self):
        # data sanity: both office routes existed; pin that the degree
        # route alone (no coordinator history) qualifies
        svc = make_service()
        st = new_graduate_game(svc, 7)
        grant_skills(svc, st["game_id"], organization=45)
        env = svc._load(st["game_id"])
        env["education_state"]["qualifications"] = [{
            "qualification_id": "business_degree",
            "program_id": "business_bachelors", "program_version": 1,
            "awarded_turn": "t_x"}]
        svc._save(env)
        st = svc.get_state(st["game_id"])
        opening = self._opening(st, "operations_specialist")
        self.assertTrue(opening["eligible"], opening["reason"])

    def test_parent_drift_lines_capped_to_one_per_year(self):
        # both parents drift every year; only the STRONGEST change may
        # be journaled, at most one line per year
        for seed in SEEDS:
            svc = make_service()
            st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                               "gender": "female", "country": "Australia",
                               "seed": seed}, f"dc{seed}")
            gid = st["game_id"]
            env = svc._load(gid)
            env["player"].age = 30
            svc._save(env)
            st = svc.get_state(gid)
            drifted = []
            for i in range(3):
                st = drain_pending(svc, st, f"dc{seed}-{i}")
                st = svc.command(gid, st["revision"], f"dc{seed}-y{i}",
                                 "age_up", {})
                env = svc._load(gid)
                drifted.append(len(
                    [e for e in env["journal"]
                     if e.get("age") == env["player"].age
                     and "relationship" in e.get("changes", {})]))
                if st["status"] != "alive":
                    break
            for count in drifted:
                self.assertLessEqual(count, 1, drifted)
            self.assertGreaterEqual(sum(drifted), 1,
                                    "expected at least one drift line")




class TenurePolicyTests(unittest.TestCase):
    """Amendment 6 C1 (Claude's frozen policy): experience = total
    recorded years across ALL stints in the role; unknown tenure = 0;
    transitions never add years."""

    def _hire(self, svc, st, tag, role, employer):
        st = svc.command(st["game_id"], st["revision"], f"{tag}-a",
                         "careers_apply",
                         {"opening": f"{role}@{employer}"})
        d = st["pending_decision"]
        return svc.command(st["game_id"], st["revision"], f"{tag}-r",
                           "resolve_decision",
                           {"decision_id": d["id"], "option_id": "accept"})

    def test_resigning_a_below_threshold_role_does_not_qualify(self):
        # Codex repro: care assistant -> qualified care worker, one
        # aged year, resign -> the PAST tenure (1) must NOT satisfy the
        # nurse experience route
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"t{seed}")
            grant_skills(svc, st["game_id"], care=45, communication=30)
            env = svc._load(st["game_id"])
            env["education_state"]["qualifications"] = [{
                "qualification_id": "care_diploma_cert",
                "program_id": "care_diploma", "program_version": 1,
                "awarded_turn": "t_x"}]
            svc._save(env)
            st = svc.get_state(st["game_id"])
            st = self._hire(svc, st, f"t{seed}", "care_assistant",
                            "bluegum")
            st = svc.command(st["game_id"], st["revision"],
                             f"t{seed}-x", "careers_resign", {})
            st = self._hire(svc, st, f"t{seed}2", "qualified_care_worker",
                            "bluegum")
            st = drain_pending(svc, st, f"t{seed}3")
            st = svc.command(st["game_id"], st["revision"],
                             f"t{seed}-y", "age_up", {})
            st = drain_pending(svc, st, f"t{seed}4")
            self.assertEqual(
                st["careers"]["assignment"]["role_tenure"], 1)
            st = svc.command(st["game_id"], st["revision"],
                             f"t{seed}-q", "careers_resign", {})
            opening = next(o for o in st["careers"]["openings"]
                           if o["role_id"] == "nurse")
            self.assertFalse(opening["eligible"])
            self.assertIn("8 years as a Qualified care worker",
                          opening["reason"])
            with self.assertRaises(GameError) as cm:
                svc.command(st["game_id"], st["revision"], f"t{seed}-n",
                            "careers_apply",
                            {"opening": "nurse@bluegum"})
            self.assertEqual(cm.exception.code, "not_allowed")

    def test_experience_totals_across_all_stints(self):
        # two stints (2 + 6 recorded years) satisfy the 8-year route
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"tot{seed}")
            grant_skills(svc, st["game_id"], care=45, communication=30)
            env = svc._load(st["game_id"])
            p = env["player"]
            p.age = 30
            env["education_state"]["qualifications"] = [{
                "qualification_id": "care_diploma_cert",
                "program_id": "care_diploma", "program_version": 1,
                "awarded_turn": "t_x"}]
            pid = p.person_id
            env["careers"]["assignments"][pid] = {
                "assignment_id": "ca_1", "role_id":
                "qualified_care_worker", "employer_id": "bluegum",
                "salary": 45000, "hours": 38, "accepted_turn": "t_a",
                "accepted_age": 22, "role_tenure": 2, "status": "active"}
            env["careers"]["past_assignments"][pid] = [{
                "assignment_id": "ca_0", "role_id":
                "qualified_care_worker", "employer_id": "staria",
                "salary": 41000, "hours": 38, "accepted_turn": "t_b",
                "accepted_age": 20, "role_tenure": 6, "status": "left",
                "to_turn": "t_c"}]
            svc._save(env)
            st = svc.get_state(st["game_id"])
            opening = next(o for o in st["careers"]["openings"]
                           if o["role_id"] == "nurse")
            self.assertTrue(opening["eligible"], opening["reason"])
            alt = opening["requirements"]["routes"][1]
            self.assertEqual(alt["roles"][0]["recorded_years"], 8)
            self.assertTrue(alt["met"])

    def test_unknown_legacy_tenure_counts_as_zero(self):
        svc = make_service()
        st = new_graduate_game(svc, 7, request="leg")
        grant_skills(svc, st["game_id"], care=45, communication=30)
        env = svc._load(st["game_id"])
        p = env["player"]
        p.age = 40
        env["education_state"]["qualifications"] = [{
            "qualification_id": "care_diploma_cert",
            "program_id": "care_diploma", "program_version": 1,
            "awarded_turn": "t_x"}]
        pid = p.person_id
        # legacy past record WITHOUT any recorded tenure
        env["careers"]["past_assignments"][pid] = [{
            "assignment_id": "ca_old", "role_id":
            "qualified_care_worker", "employer_id": "staria",
            "salary": 40000, "hours": 38, "accepted_turn": "t_old",
            "status": "left", "to_turn": "t_old2"}]
        svc._save(env)
        st = svc.get_state(st["game_id"])
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "nurse")
        self.assertFalse(opening["eligible"])
        alt = opening["requirements"]["routes"][1]
        self.assertEqual(alt["roles"][0]["recorded_years"], 0)
        self.assertFalse(alt["met"])


class PromotionEmployerAvailabilityTests(unittest.TestCase):
    """Amendment 6 C2: promotion respects employer role_ids; an
    unavailable internal step names the employer-change path."""

    def _senior_electrician(self, svc, seed, employer):
        st = new_graduate_game(svc, seed, request=f"c2{seed}")
        # site_supervisor's shared skills (practical 60/org 50)
        grant_skills(svc, st["game_id"], practical=60, organization=50)
        env = svc._load(st["game_id"])
        env["education_state"]["qualifications"] = [{
            "qualification_id": "electrical_trade_cert",
            "program_id": "app_electrical", "program_version": 1,
            "awarded_turn": "t_x"}]
        pid = env["player"].person_id
        env["careers"]["past_assignments"][pid] = [
            {"assignment_id": "ca_1", "role_id": "app_electrician",
             "employer_id": "voltworks", "salary": 28000, "hours": 38,
             "accepted_turn": "t_a", "accepted_age": 18,
             "role_tenure": 2, "status": "left", "to_turn": "t_b"},
            {"assignment_id": "ca_2", "role_id": "electrician",
             "employer_id": "voltworks", "salary": 55000, "hours": 38,
             "accepted_turn": "t_b", "accepted_age": 20,
             "role_tenure": 2, "status": "left", "to_turn": "t_c"}]
        env["careers"]["assignments"][pid] = {
            "assignment_id": "ca_3", "role_id": "senior_electrician",
            "employer_id": employer, "salary": 70000, "hours": 38,
            "accepted_turn": "t_c", "accepted_age": 22,
            "role_tenure": 3, "status": "active"}
        env["player"].performance = 90
        svc._save(env)
        return svc.get_state(st["game_id"])

    def test_unavailable_internal_promotion_names_employer_change(self):
        for seed in SEEDS:
            svc = make_service()
            st = self._senior_electrician(svc, seed, "voltworks")
            spec = next(a for a in st["available_actions"]
                        if a["id"] == "careers_promote")
            # site_supervisor is not offered at voltworks: the spec
            # disables promotion with the employer-change reason
            self.assertFalse(spec["enabled"])
            self.assertIn("change employer", spec["reason"])
            self.assertNotIn("site_supervisor", spec["inputs"][0]
                             ["choices"])
            self.assertEqual(st["careers"]["assignment"]["promotions_to"], [])
            self.assertEqual(st["careers"]["assignment"]["promotion_reason"],
                             spec["reason"])
            with self.assertRaises(GameError) as cm:
                svc.command(st["game_id"], st["revision"],
                            f"c2{seed}-p", "careers_promote",
                            {"role_id": "site_supervisor"})
            self.assertEqual(cm.exception.code, "not_allowed")
            self.assertIn("no internal promotion", str(cm.exception))
            self.assertIn("change employer", str(cm.exception))

    def test_available_internal_promotion_still_works(self):
        for seed in SEEDS:
            svc = make_service()
            st = self._senior_electrician(svc, seed, "brightspark")
            spec = next(a for a in st["available_actions"]
                        if a["id"] == "careers_promote")
            self.assertIn("site_supervisor", spec["inputs"][0]["choices"])
            self.assertTrue(spec["enabled"], spec["reason"])
            st = svc.command(st["game_id"], st["revision"],
                             f"c2{seed}-p", "careers_promote",
                             {"role_id": "site_supervisor"})
            self.assertEqual(st["pending_decision"]["kind"],
                             "career_offer")
            env = svc._load(st["game_id"])
            offer = next(o for o in env["careers"]["offers"].values()
                         if o["status"] == "open")
            self.assertEqual(offer["employer_id"], "brightspark")


class PublishedRoutesTests(unittest.TestCase):
    """Amendment 6 C3: openings publish named routes with per-route
    requirements/tenure/met plus shared age/skills."""

    def test_electrician_publishes_both_routes_and_shared(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_graduate_game(svc, seed, request=f"c3{seed}")
            grant_skills(svc, st["game_id"], practical=40)
            st = svc.get_state(st["game_id"])
            opening = next(o for o in st["careers"]["openings"]
                           if o["role_id"] == "electrician")
            req = opening["requirements"]
            routes = req["routes"]
            self.assertEqual(len(routes), 2)
            self.assertIn("Electrical trade certificate",
                          routes[0]["label"])
            self.assertFalse(routes[0]["met"])
            self.assertEqual(routes[1]["label"],
                             "a Bachelor of Business")
            self.assertFalse(routes[1]["met"])
            self.assertTrue(all(not r["met"] for r in routes))
            self.assertFalse(opening["eligible"])
            # shared requirements: age + skills, rendered once
            self.assertEqual(opening["reason_kind"], "requirements")
            self.assertEqual(req["roles"][0]["label"], "Electrical apprentice")
            self.assertEqual(req["shared"]["min_age"], 18)
            self.assertEqual(
                [x["id"] for x in req["shared"]["skills"]],
                ["practical"])
            # presence-only roles publish no recorded_years
            self.assertIsNone(routes[0]["roles"][0]["recorded_years"])
            # the alternative route admits -> its met flag flips
            env = svc._load(st["game_id"])
            env["education_state"]["qualifications"] = [{
                "qualification_id": "business_degree",
                "program_id": "business_bachelors", "program_version": 1,
                "awarded_turn": "t_x"}]
            svc._save(env)
            st = svc.get_state(st["game_id"])
            opening = next(o for o in st["careers"]["openings"]
                           if o["role_id"] == "electrician")
            routes = opening["requirements"]["routes"]
            self.assertFalse(routes[0]["met"])
            self.assertTrue(routes[1]["met"])
            self.assertTrue(opening["eligible"])

    def test_nurse_route_publishes_tenure_met(self):
        svc = make_service()
        st = new_graduate_game(svc, 7, request="c3n")
        grant_skills(svc, st["game_id"], care=45, communication=30)
        env = svc._load(st["game_id"])
        env["player"].age = 30
        env["education_state"]["qualifications"] = [{
            "qualification_id": "care_diploma_cert",
            "program_id": "care_diploma", "program_version": 1,
            "awarded_turn": "t_x"}]
        pid = env["player"].person_id
        env["careers"]["assignments"][pid] = {
            "assignment_id": "ca_9", "role_id":
            "qualified_care_worker", "employer_id": "bluegum",
            "salary": 45000, "hours": 38, "accepted_turn": "t_x",
            "accepted_age": 22, "role_tenure": 8, "status": "active"}
        svc._save(env)
        st = svc.get_state(st["game_id"])
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "nurse")
        routes = opening["requirements"]["routes"]
        self.assertEqual(len(routes), 2)
        alt = routes[1]
        self.assertEqual(alt["label"],
                         "8 years as a Qualified care worker")
        self.assertEqual(alt["roles"][0]["min_tenure"], 8)
        self.assertEqual(alt["roles"][0]["recorded_years"], 8)
        self.assertTrue(alt["met"])
        self.assertTrue(opening["eligible"])


class MigrationTests(unittest.TestCase):
    def test_legacy_save_without_m3_keys_loads_and_backfills(self):
        svc = make_service()
        st = new_graduate_game(svc, 7)
        gid = st["game_id"]
        env = svc._load(gid)
        del env["education_state"]
        del env["skills"]
        del env["careers"]
        svc._save(env)
        st = svc.get_state(gid)
        self.assertIn("programs", st["education_programs"])
        self.assertEqual(st["education_programs"]["qualifications"], [])
        self.assertIn("stage", st["education"])
        # skills backfilled with deterministic per-person defaults
        env = svc._load(gid)
        pid = env["player"].person_id
        values = skills_state.values(env, pid)
        self.assertEqual(set(values),
                         {"practical", "organization", "communication",
                          "care"})
        for v in values.values():
            self.assertTrue(5 <= v <= 10)
        # stable across reloads (no per-load reroll)
        st2 = svc.get_state(gid)
        env2 = svc._load(gid)
        self.assertEqual(values, skills_state.values(env2, pid))


if __name__ == "__main__":
    unittest.main()
