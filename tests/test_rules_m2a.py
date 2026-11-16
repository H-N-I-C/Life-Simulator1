import random
import unittest
from unittest import mock

import src.menus.rule_actions as ra
from src.lifesim_lib.lifesim_lib import calculate_tax
from tests.test_rules_income import make_player


def student(**kw):
    kw.setdefault("age", 10)
    kw.setdefault("grades", 60)
    p = make_player(**kw)
    p.studied = False
    p.skipped_school = False
    p.tried_to_drop_out = False
    return p


def worker(**kw):
    defaults = dict(age=40, salary=40000, has_job=True, job_hours=40,
                    years_worked=5, stress=40, performance=60,
                    last_raise=30, times_asked_since_last_raise=0,
                    asked_for_raise=False, worked_harder=False,
                    salary_years=[40000] * 5)
    defaults.update(kw)
    p = make_player(**defaults)
    return p


class SchoolActionTest(unittest.TestCase):
    def test_study_harder_once_per_year(self):
        p = student()
        r = ra.study_harder(p, random.Random(1))
        self.assertTrue(p.studied)
        self.assertGreater(p.grades, 60)
        with self.assertRaises(ra.RuleError):
            ra.study_harder(p, random.Random(1))

    def test_skip_school_lowers_stats(self):
        p = student()
        s0, g0 = p.smarts, p.grades
        ra.skip_school(p, random.Random(2))
        self.assertLess(p.smarts, s0)
        self.assertLess(p.grades, g0)
        self.assertTrue(p.skipped_school)

    def test_drop_out_refusal_and_success(self):
        young = student(age=10, smarts=100, dropped_out=False)
        r = ra.drop_out(young, random.Random(3))
        self.assertFalse(young.dropped_out)
        self.assertTrue(young.tried_to_drop_out)
        self.assertIn("won't let you", r["events"][0]["text"])
        adult = student(age=18, dropped_out=False)
        ra.drop_out(adult, random.Random(3))
        self.assertTrue(adult.dropped_out)
        self.assertIsNone(adult.grades)

    def test_school_actions_require_enrollment(self):
        p = make_player(age=25, grades=None)
        for fn in (ra.study_harder, ra.skip_school, ra.drop_out):
            with self.assertRaises(ra.RuleError):
                fn(p, random.Random(0))


class JobActionTest(unittest.TestCase):
    def test_work_harder_once_per_year(self):
        p = worker()
        ra.work_harder(p, random.Random(4))
        self.assertTrue(p.worked_harder)
        self.assertGreater(p.performance, 60)
        with self.assertRaises(ra.RuleError):
            ra.work_harder(p, random.Random(4))

    def test_adjust_hours_validates_and_applies(self):
        p = worker()
        with self.assertRaises(ra.RuleError):
            ra.adjust_hours(p, 20)
        with self.assertRaises(ra.RuleError):
            ra.adjust_hours(p, 90)
        ra.adjust_hours(p, 50)
        self.assertEqual(p.job_hours, 50)
        self.assertGreater(p.stress, 40)

    def test_request_raise_approval(self):
        p = worker(age=45, performance=100)
        rng = mock.Mock()
        rng.randint = lambda a, b: {40: 40, 20: 50}.get(a, 0)
        r = ra.request_raise(p, rng)
        self.assertGreater(p.salary, 40000)
        self.assertEqual(p.last_raise, 45)
        self.assertIn("approved", r["events"][0]["text"])

    def test_request_raise_rejection_sets_flag(self):
        p = worker(performance=0)
        rng = mock.Mock()
        rng.randint = lambda a, b: 120 if a == 40 else 0
        r = ra.request_raise(p, rng)
        self.assertIn("rejected", r["events"][0]["text"])
        self.assertTrue(p.asked_for_raise)
        with self.assertRaises(ra.RuleError):
            ra.request_raise(p, random.Random(6))

    def test_retire_pays_pension(self):
        p = worker(age=66, years_worked=20, salary_years=[40000] * 20)
        r = ra.retire(p, random.Random(7))
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, p.calc_pension() if p.has_job else p.salary)
        self.assertGreater(p.salary, 0)

    def test_retire_requires_eligibility(self):
        p = worker(age=40, years_worked=5)
        with self.assertRaises(ra.RuleError):
            ra.retire(p, random.Random(7))

    def test_quit_job(self):
        p = worker()
        ra.quit_job(p)
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, 0)


class FindJobFlowTest(unittest.TestCase):
    def test_offer_sampled_then_decision(self):
        p = make_player(age=25, smarts=100, karma=100)
        with mock.patch.object(ra, "_randexpo", return_value=45000), \
             mock.patch.object(ra, "_round_stochastic", side_effect=lambda rng, v: round(v)):
            r = ra.apply_action(p, "find_job", {}, random.Random(0))
        pending = r["pending_decision"]
        self.assertEqual(pending["sampled"]["salary"], 45000)
        # resolving uses the stored salary; a hired player gets exactly it
        r2 = ra.resolve_choice(p, pending, "apply", random.Random(11))
        if p.has_job:
            self.assertEqual(p.salary, 45000)
        else:
            self.assertIn("interview", r2["events"][0]["text"])
        self.assertIsNone(r2["pending_decision"])

    def test_walk_away(self):
        p = make_player(age=25)
        pending = {"kind": "job_offer", "sampled": {"salary": 45000},
                   "options": []}
        r = ra.resolve_choice(p, pending, "walk_away", random.Random(0))
        self.assertFalse(p.has_job)


class WorkPhaseTest(unittest.TestCase):
    def test_employed_year_posts_income_once(self):
        p = worker()
        with mock.patch.object(ra, "sample_expense_retention", return_value=0.6):
            r = ra.yearly_work_phase(p, "t1", random.Random(8))
        self.assertEqual(p.years_worked, 6)
        self.assertEqual(len(p.salary_years), 6)
        tax = calculate_tax(40000)
        legs = {m["leg"]: m for m in r["money_entries"]}
        self.assertEqual(len(legs), 3)
        self.assertEqual(legs["gross"]["amount"], 40000)
        self.assertEqual(-legs["tax"]["amount"], tax)
        self.assertAlmostEqual(legs["gross"]["amount"]
                               + legs["tax"]["amount"]
                               + legs["expenses"]["amount"],
                               round((40000 - tax) * 0.6))
        for m in r["money_entries"]:
            self.assertEqual(m["phase"], "income")
            self.assertEqual(m["account_owner_id"], p.ID)

    def test_unemployed_pension_year_posts_income(self):
        p = make_player(age=70, salary=20000, has_job=False, job_hours=0,
                        years_worked=0, salary_years=[], children=[],
                        stress=0, performance=0, illnesses=[])
        with mock.patch.object(ra, "sample_expense_retention", return_value=0.6):
            r = ra.yearly_work_phase(p, "t1", random.Random(9))
        tax = calculate_tax(20000)
        legs = {m["leg"]: m for m in r["money_entries"]}
        self.assertEqual(legs["gross"]["amount"], 20000)
        self.assertAlmostEqual(legs["gross"]["amount"]
                               + legs["tax"]["amount"]
                               + legs["expenses"]["amount"],
                               round((20000 - tax) * 0.6))

    def test_retirement_offer_is_sampled_not_rerolled(self):
        p = worker(age=66, years_worked=20, salary_years=[50000] * 20)
        with mock.patch.object(ra, "_one_in", return_value=True):
            r = ra.yearly_work_phase(p, "t1", random.Random(10))
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "retirement_offer")
        pension = pending["sampled"]["pension"]
        r2 = ra.resolve_retirement_choice(p, pending, "retire",
                                          random.Random(12))
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, pension)

    def test_forced_retirement_has_no_choice(self):
        p = worker(age=71, years_worked=30, salary_years=[50000] * 30)
        with mock.patch.object(ra, "_one_in", return_value=True):
            r = ra.yearly_work_phase(p, "t1", random.Random(10))
        self.assertEqual(r["pending_decision"]["sampled"]["forced"], True)
        self.assertFalse(p.has_job)  # applied immediately
        self.assertEqual(p.salary, r["pending_decision"]["sampled"]["pension"])


class EducationPhaseTest(unittest.TestCase):
    def test_school_starts_at_six(self):
        p = student(age=6)
        r = ra.yearly_education_phase(p, "t1", random.Random(13))
        self.assertIsNotNone(p.grades)
        self.assertEqual(r["events"][0]["text"],
                         "You are starting elementary school")

    def test_graduation_at_17_with_funding_decision(self):
        p = student(age=17, smarts=100, dropped_out=False)
        p.parents = {}
        r = ra.yearly_education_phase(p, "t1", random.Random(14))
        self.assertIsNone(p.grades)
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "university_funding")
        self.assertEqual([o["id"] for o in pending["options"]],
                         ["scholarship", "student_loan"])
        r2 = ra.resolve_university_funding(p, pending, "student_loan",
                                           random.Random(15))
        self.assertEqual(p.uv_years, 4)
        self.assertTrue(p.chose_student_loan)
        self.assertIsNone(r2["pending_decision"])

    def test_funding_refusal_reoffers_without_spent_option(self):
        p = student(age=17, smarts=0, dropped_out=False)
        p.parents = {}
        pending = {"kind": "university_funding",
                   "options": [{"id": "scholarship"}, {"id": "student_loan"}],
                   "spent": []}
        r = ra.resolve_university_funding(p, pending, "scholarship",
                                          random.Random(16))
        self.assertIsNotNone(r["pending_decision"])
        self.assertEqual([o["id"] for o in r["pending_decision"]["options"]],
                         ["student_loan"])
        self.assertFalse(p.is_in_school() and p.uv_years > 0)

    def test_expulsion_clears_enrollment(self):
        p = student(age=20, grades=0, uv_years=2, chose_student_loan=False)
        ra.yearly_education_phase(p, "t1", random.Random(17))
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)

    def test_graduation_sets_loan_principal(self):
        p = student(age=22, grades=50, uv_years=1, chose_student_loan=True)
        ra.yearly_education_phase(p, "t1", random.Random(18))
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)
        self.assertGreater(p.student_loan, 0)


class ActionSpecsTest(unittest.TestCase):
    def test_specs_follow_state(self):
        p = student()
        ids = [s["id"] for s in ra.action_specs(p)]
        self.assertEqual(ids, ["study_harder", "skip_school", "drop_out"])
        w = worker()
        ids = [s["id"] for s in ra.action_specs(w)]
        self.assertIn("work_harder", ids)
        self.assertIn("adjust_hours", ids)
        self.assertIn("request_raise", ids)
        self.assertIn("quit_job", ids)
        old = worker(age=66, years_worked=20)
        ids = [s["id"] for s in ra.action_specs(old)]
        self.assertIn("retire", ids)


if __name__ == "__main__":
    unittest.main()
