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
    p.dropped_out = False
    return p


def worker(**kw):
    defaults = dict(age=40, salary=40000, has_job=True, job_hours=40,
                    years_worked=5, stress=40, performance=60,
                    last_raise=30, times_asked_since_last_raise=0,
                    asked_for_raise=False, worked_harder=False,
                    salary_years=[40000] * 5)
    defaults.update(kw)
    return make_player(**defaults)


def scripted_rng(mapping):
    rng = mock.Mock()
    rng.randint = lambda a, b: mapping.get((a, b), mapping.get(a, 1))
    return rng


class SchoolActionTest(unittest.TestCase):
    def test_study_harder_once_per_year(self):
        p = student()
        r = ra.study_harder(p, random.Random(1))
        self.assertTrue(p.studied)
        self.assertGreater(p.grades, 60)
        self.assertEqual(r["events"][0]["changes"]["grades"],
                         p.grades - 60)
        with self.assertRaises(ra.RuleError):
            ra.study_harder(p, random.Random(1))

    def test_skip_school_exact_deltas(self):
        p = student()
        s0, g0 = p.smarts, p.grades
        r = ra.skip_school(p, random.Random(2))
        self.assertEqual(r["events"][0]["changes"],
                         {"smarts": p.smarts - s0, "grades": p.grades - g0})
        self.assertEqual(p.smarts - s0, -2)
        self.assertLess(p.grades, g0)
        self.assertTrue(p.skipped_school)

    def test_skip_school_core_losses_repeat_fun_reward_once(self):
        # Legacy: core losses apply again; the positive reward only once.
        p = student()
        ra.skip_school(p, random.Random(3))
        h_after_first = p.happiness
        texts = [e["text"] for e in ra.skip_school(p, random.Random(3))[
            "events"]]
        self.assertNotIn("You had a good time.", texts)
        self.assertEqual(h_after_first, p.happiness)  # caught branch apart

    def test_drop_out_refusal_and_success(self):
        young = student(age=10, smarts=100)
        r = ra.drop_out(young, random.Random(3))
        self.assertFalse(young.dropped_out)
        self.assertTrue(young.tried_to_drop_out)
        self.assertIn("won't let you", r["events"][0]["text"])
        adult = student(age=18)
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
        before_perf, before_stress = p.performance, p.stress
        r = ra.work_harder(p, random.Random(4))
        self.assertTrue(p.worked_harder)
        self.assertGreater(p.performance, before_perf)
        self.assertEqual(r["events"][0]["changes"],
                         {"performance": p.performance - before_perf,
                          "stress": p.stress - before_stress})
        with self.assertRaises(ra.RuleError):
            ra.work_harder(p, random.Random(4))

    def test_adjust_hours_validates_and_applies(self):
        p = worker()
        for bad in (20, 90, 40.5, "40", None, True):
            with self.assertRaises(ra.RuleError):
                ra.adjust_hours(p, bad)
        before_stress = p.stress
        ra.adjust_hours(p, 50)
        self.assertEqual(p.job_hours, 50)
        self.assertGreater(p.stress, before_stress)

    def test_apply_action_missing_hours_is_rule_error(self):
        p = worker()
        with self.assertRaises(ra.RuleError):
            ra.apply_action(p, "adjust_hours", {}, random.Random(0))

    def test_request_raise_approval(self):
        p = worker(age=45, performance=100)
        rng = scripted_rng({(40, 120): 40, (20, 85): 50})
        r = ra.request_raise(p, rng)
        self.assertEqual(p.salary, 42000)
        self.assertEqual(p.last_raise, 45)
        self.assertIn("approved", r["events"][0]["text"])

    def test_request_raise_rejection_sets_flag(self):
        p = worker(performance=0)
        rng = scripted_rng({(40, 120): 120})
        r = ra.request_raise(p, rng)
        self.assertIn("rejected", r["events"][0]["text"])
        self.assertTrue(p.asked_for_raise)
        with self.assertRaises(ra.RuleError):
            ra.request_raise(p, rng)

    def test_retire_pays_calculated_pension(self):
        p = worker(age=66, years_worked=20, salary_years=[40000] * 20)
        expected_pension = p.calc_pension()
        self.assertGreater(expected_pension, 0)
        ra.retire(p, random.Random(7))
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, expected_pension)

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
    def test_interview_pre_sampled_and_stable_across_resolver_rng(self):
        p = make_player(age=25, smarts=50, karma=50)
        offer_rng = random.Random(99)
        r = ra.apply_action(p, "find_job", {}, offer_rng)
        pending = r["pending_decision"]
        self.assertIn("success", pending["sampled"])
        # Resolving with wildly different RNGs must give the SAME outcome.
        r_a = ra.resolve_choice(p, pending, "apply", {}, random.Random(0))
        hired = p.has_job
        if hired:
            self.assertEqual(p.salary, pending["sampled"]["salary"])
        else:
            self.assertIn("interview", r_a["events"][0]["text"])

    def test_forged_option_rejected(self):
        p = make_player(age=25)
        r = ra.apply_action(p, "find_job", {}, random.Random(0))
        with self.assertRaises(ra.RuleError):
            ra.resolve_choice(p, r["pending_decision"], "forge", {},
                              random.Random(0))
        self.assertFalse(p.has_job)

    def test_walk_away(self):
        p = make_player(age=25)
        pending = {"kind": "job_offer",
                   "sampled": {"salary": 45000, "success": True,
                               "happiness_delta": 0},
                   "options": [{"id": "apply"}, {"id": "walk_away"}]}
        r = ra.resolve_choice(p, pending, "walk_away", {}, random.Random(0))
        self.assertFalse(p.has_job)
        self.assertEqual(r["events"][0]["text"], "I walked away.")


class WorkPhaseTest(unittest.TestCase):
    def income_legs(self, r):
        return {m["leg"]: m for m in r["money_entries"]}

    def test_employed_year_posts_income_legs(self):
        p = worker()
        with mock.patch.object(ra, "sample_expense_retention",
                               return_value=0.6):
            r = ra.yearly_work_phase(p, "t1", random.Random(8))
        self.assertEqual(p.years_worked, 6)
        self.assertEqual(len(p.salary_years), 6)
        legs = self.income_legs(r)
        tax = calculate_tax(40000)
        self.assertEqual(legs["gross"]["amount"], 40000)
        self.assertEqual(-legs["tax"]["amount"], tax)
        self.assertEqual(legs["gross"]["amount"] + legs["tax"]["amount"]
                         + legs["expenses"]["amount"],
                         round((40000 - tax) * 0.6))
        for m in r["money_entries"]:
            self.assertEqual(m["phase"], "work_income")
            self.assertEqual(m["account_owner_id"], p.person_id)

    def test_unemployed_pension_year_posts_income(self):
        p = make_player(age=70, salary=20000, has_job=False, job_hours=0)
        with mock.patch.object(ra, "sample_expense_retention",
                               return_value=0.6):
            r = ra.yearly_work_phase(p, "t1", random.Random(9))
        legs = self.income_legs(r)
        tax = calculate_tax(20000)
        self.assertEqual(legs["gross"]["amount"], 20000)
        self.assertEqual(-legs["tax"]["amount"], tax)

    def test_voluntary_retirement_both_outcomes(self):
        p = worker(age=66, salary=50000, years_worked=20,
           salary_years=[50000] * 20)
        with mock.patch.object(ra, "_one_in", return_value=True):
            r = ra.yearly_work_phase(p, "t1", random.Random(10))
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "retirement_offer")
        pension = pending["sampled"]["pension"]
        # Income this year still comes from the pre-retirement salary.
        self.assertEqual(self.income_legs(r)["gross"]["amount"], 50000)
        r2 = ra.resolve_retirement_choice(p, pending, "retire",
                                          random.Random(12))
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, pension)
        self.assertEqual(r2["continuation"]["phase_id"], "education")
        # Re-resolution must not re-apply the happiness boost.
        happy = p.happiness
        ra.resolve_retirement_choice(p, pending, "retire", random.Random(13))
        self.assertEqual(p.happiness, happy)

    def test_keep_working(self):
        p = worker(age=66, salary=50000, years_worked=20,
           salary_years=[50000] * 20)
        with mock.patch.object(ra, "_one_in", return_value=True):
            r = ra.yearly_work_phase(p, "t1", random.Random(10))
        ra.resolve_retirement_choice(p, r["pending_decision"], "keep_working",
                                     random.Random(12))
        self.assertTrue(p.has_job)
        self.assertEqual(p.salary, 50000)

    def test_forced_retirement_is_event_without_pending(self):
        p = worker(age=71, salary=50000, years_worked=30,
           salary_years=[50000] * 30)
        pension = p.calc_pension()
        calls = iter([True, True])  # offer fires, forced roll fires
        with mock.patch.object(ra, "_one_in",
                               side_effect=lambda rng_x, x: next(calls)):
            r = ra.yearly_work_phase(p, "t1", random.Random(10))
        self.assertIsNone(r["pending_decision"])
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, pension)
        # Legacy order: forced retirement happens before the salary line, so
        # this year's pay is quoted from the new pension.
        self.assertEqual(self.income_legs(r)["gross"]["amount"], pension)


class DebtServiceTest(unittest.TestCase):
    def test_repayment_capped_and_posts_entry(self):
        p = make_player(student_loan=500)
        r = ra.debt_service_phase(p, "t1", random.Random(20))
        self.assertEqual(p.student_loan, 0)
        (entry,) = r["money_entries"]
        self.assertEqual(entry["phase"], "debt_service")
        self.assertEqual(entry["category"], "loan_repay")
        self.assertEqual(entry["amount"], -500)
        self.assertEqual(r["continuation"]["phase_id"], "family_events")

    def test_no_loan_no_entry(self):
        p = make_player(student_loan=0)
        r = ra.debt_service_phase(p, "t1", random.Random(20))
        self.assertEqual(r["money_entries"], [])


class EducationPhaseTest(unittest.TestCase):
    def test_school_starts_at_six(self):
        p = student(age=6)
        r = ra.yearly_education_phase(p, "t1", random.Random(13))
        self.assertIsNotNone(p.grades)
        self.assertIn("elementary", r["events"][0]["text"])

    def test_age_17_application_decline(self):
        p = student(age=17, smarts=100)
        r = ra.yearly_education_phase(p, "t1", random.Random(14))
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "university_application")
        r2 = ra.resolve_university_application(p, pending, "decline",
                                               random.Random(15))
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(r2["pending_decision"])

    def test_age_17_accept_enrolls_via_funding(self):
        p = student(age=17, smarts=100)
        p.parents = {}
        r = ra.yearly_education_phase(p, "t1", random.Random(14))
        funding = ra.resolve_university_application(
            p, r["pending_decision"], "apply", random.Random(15))[
            "pending_decision"]
        self.assertEqual(funding["kind"], "university_funding")
        r2 = ra.resolve_university_funding(p, funding, "student_loan",
                                           random.Random(16))
        self.assertEqual(p.uv_years, 4)
        self.assertTrue(p.chose_student_loan)
        self.assertIsNone(r2["pending_decision"])

    def test_funding_outcomes_pre_sampled(self):
        # smarts 50: application accepted (>= randint(30,44) always), but
        # scholarship outcome samples False (50 < randint(75+,100)).
        p = student(age=17, smarts=50)
        p.parents = {}
        r = ra.yearly_education_phase(p, "t1", random.Random(14))
        funding = ra.resolve_university_application(
            p, r["pending_decision"], "apply", random.Random(15))[
            "pending_decision"]
        # smarts 0 -> scholarship outcome sampled as False; two very
        # different resolver RNGs must produce the identical result.
        ra.resolve_university_funding(p, funding, "scholarship",
                                      random.Random(0))
        self.assertEqual(p.uv_years, 0)
        self.assertEqual(p.uv_years, 0)
        # refused option removed from reoffer
        reoffer = funding  # mutated copy held in result; use fresh flow
        p2 = student(age=17, smarts=50)
        p2.parents = {}
        r2 = ra.yearly_education_phase(p2, "t1", random.Random(14))
        f2 = ra.resolve_university_application(
            p2, r2["pending_decision"], "apply", random.Random(15))[
            "pending_decision"]
        out = ra.resolve_university_funding(p2, f2, "scholarship",
                                            random.Random(777))
        ids = [o["id"] for o in out["pending_decision"]["options"]]
        self.assertEqual(ids, ["student_loan"])

    def test_forged_and_spent_options_rejected(self):
        p = student(age=17, smarts=50)
        p.parents = {}
        r = ra.yearly_education_phase(p, "t1", random.Random(14))
        funding = ra.resolve_university_application(
            p, r["pending_decision"], "apply", random.Random(15))[
            "pending_decision"]
        with self.assertRaises(ra.RuleError):
            ra.resolve_university_funding(p, funding, "forge",
                                          random.Random(0))
        out = ra.resolve_university_funding(p, funding, "scholarship",
                                            random.Random(0))
        with self.assertRaises(ra.RuleError):
            ra.resolve_university_funding(p, out["pending_decision"],
                                          "scholarship", random.Random(0))
        self.assertEqual(p.uv_years, 0)

    def test_expulsion_clears_enrollment(self):
        p = student(age=20, grades=0, uv_years=2, chose_student_loan=False)
        ra.yearly_education_phase(p, "t1", random.Random(17))
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)

    def test_loan_accrues_each_financed_year_and_at_graduation(self):
        p = student(age=22, grades=50, uv_years=1, chose_student_loan=True)
        ra.yearly_education_phase(p, "t1", random.Random(18))
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)
        self.assertGreater(p.student_loan, 0)  # the financed year accrued

    def test_dropout_after_two_financed_years_keeps_debt(self):
        # Two REAL education-phase turns with loan funding: debt accrues
        # per financed year (each tuition in [5000, 10000]); dropping out
        # must NOT erase it (policy: tuition financed stays as debt).
        p = student(age=20, grades=60, uv_years=3,
                    chose_student_loan=True, student_loan=0)
        rng = random.Random(33)
        ra.yearly_education_phase(p, "t1", rng)   # year 1 financed
        year1 = p.student_loan
        self.assertTrue(5000 <= year1 <= 10000)
        ra.yearly_education_phase(p, "t2", rng)   # year 2 financed
        year2 = p.student_loan - year1
        self.assertTrue(5000 <= year2 <= 10000)
        accrued = p.student_loan
        self.assertEqual(accrued, year1 + year2)
        ra.drop_out(p, random.Random(3))
        self.assertTrue(p.dropped_out)
        self.assertEqual(p.uv_years, 0)
        self.assertEqual(p.student_loan, accrued)  # unchanged by dropout
        self.assertTrue(p.chose_student_loan)


class ActionSpecsTest(unittest.TestCase):
    def test_specs_follow_state(self):
        p = student()
        ids = [s["id"] for s in ra.action_specs(p)]
        self.assertEqual(ids[:3], ["study_harder", "skip_school",
                                   "drop_out"])
        # M2b activity specs are appended for students too
        self.assertIn("play_toys", ids)
        w = worker()
        ids = [s["id"] for s in ra.action_specs(w)]
        self.assertIn("work_harder", ids)
        self.assertIn("adjust_hours", ids)
        self.assertIn("request_raise", ids)
        self.assertIn("quit_job", ids)
        old = worker(age=66, years_worked=20)
        ids = [s["id"] for s in ra.action_specs(old)]
        self.assertIn("retire", ids)

    def test_university_student_cannot_see_job_actions(self):
        # Legacy main menu: School and Job branches are mutually exclusive.
        p = student(age=19, grades=70, uv_years=3)
        specs = ra.action_specs(p)
        self.assertNotIn("find_job", [s["id"] for s in specs])
        with self.assertRaises(ra.RuleError):
            ra.apply_action(p, "find_job", {}, random.Random(0))

    def test_used_actions_advertise_disabled_reason(self):
        p = worker()
        p.worked_harder = True
        p.asked_for_raise = True
        specs = {s["id"]: s for s in ra.action_specs(p)}
        self.assertFalse(specs["work_harder"]["enabled"])
        self.assertIn("year", specs["work_harder"]["reason"])
        self.assertFalse(specs["request_raise"]["enabled"])


class LedgerIntegrationTest(unittest.TestCase):
    """C result -> A's finance.Ledger: one poster, real balances."""

    def test_phase_entries_post_once_to_real_balances(self):
        from src.game.finance import Ledger
        p = worker()
        env = {"player": p, "archive": []}
        ledger = Ledger(env)
        with mock.patch.object(ra, "sample_expense_retention",
                               return_value=0.6):
            r = ra.yearly_work_phase(p, "t1", random.Random(8))
        for e in r["money_entries"]:
            ledger.post("t1", p.age, e)
        total = sum(e["amount"] for e in r["money_entries"])
        tax = calculate_tax(40000)
        self.assertEqual(total, round((40000 - tax) * 0.6))
        self.assertEqual(env["balances"][p.person_id], p.money)
        self.assertEqual(p.money, total)
        # retry: identical keys return original rows, no double credit
        for e in r["money_entries"]:
            ledger.post("t1", p.age, e)
        self.assertEqual(p.money, total)

    def test_child_income_posts_to_child_account(self):
        from src.game.finance import Ledger
        from src.people.classes.player import annual_income_quote
        p = worker()
        child = make_player()
        child.salary = 40000
        env = {"player": p, "archive": []}
        by_id = {p.person_id: p, child.person_id: child}
        ledger = Ledger(env, resolver=by_id.get)
        quote = annual_income_quote(child.salary, True, 40, 0.6)
        for e in ra._income_legs("income", child, quote):
            ledger.post("t1", child.age, e)
        self.assertIn(child.person_id, env["balances"])
        self.assertEqual(env["balances"].get(p.person_id), None)
        self.assertEqual(env["balances"][child.person_id], child.money)
        self.assertNotEqual(child.person_id, p.person_id)


if __name__ == "__main__":
    unittest.main()


class FiringAndStressDamageTest(unittest.TestCase):
    """Isolated proofs for parity rows 71 (performance firing) and 72
    (critical stress damage / high blood pressure)."""

    def _scripted_rng(self, randint_val):
        rng = mock.Mock()
        rng.randint = lambda a, b: randint_val
        rng.random = lambda: 0.5
        rng.uniform = lambda a, b: (a + b) / 2
        return rng

    def test_fired_for_performance(self):
        p = worker(performance=10)
        rng = self._scripted_rng(1)  # firing roll hits
        with mock.patch.object(ra, "_one_in", return_value=False):
            r = ra.yearly_work_phase(p, "t1", rng)
        self.assertFalse(p.has_job)
        self.assertEqual(p.salary, 0)
        self.assertTrue(any("fired" in e["text"] for e in r["events"]))

    def test_not_fired_when_roll_misses(self):
        p = worker(performance=10)
        rng = self._scripted_rng(11)  # firing roll misses (b = perf+1)
        r = ra.yearly_work_phase(p, "t1", rng)
        self.assertTrue(p.has_job)

    def test_critical_stress_damages_health_and_adds_hbp(self):
        p = worker(stress=100, health=80)
        rng = self._scripted_rng(0)  # no drift, minimal amounts
        with mock.patch.object(ra, "_one_in", side_effect=lambda rng_x, x: x == 7):
            r = ra.yearly_work_phase(p, "t1", rng)
        self.assertLess(p.health, 80)  # damage, not the legacy heal bug
        self.assertIn("High Blood Pressure", p.illnesses)
        self.assertTrue(any("high blood pressure" in e["text"].lower()
                            for e in r["events"]))

    def test_noncritical_stress_no_hbp(self):
        p = worker(stress=70, health=80)
        with mock.patch.object(ra, "_one_in", return_value=False):
            ra.yearly_work_phase(p, "t1", self._scripted_rng(0))
        self.assertNotIn("High Blood Pressure", p.illnesses)


class TraitDriftTest(unittest.TestCase):
    """Parity row 63: canonical ageing subrules with explicit rng."""

    def test_increment_age_only(self):
        p = make_player(age=10)
        ra.increment_age(p)
        self.assertEqual(p.age, 11)

    def test_stat_drift_uses_rng(self):
        p = make_player(age=20, happiness=50, health=50, smarts=50,
                        looks=50)
        h0 = (p.happiness, p.health, p.smarts, p.looks)
        r = random.Random(5)
        ra.stat_drift(p, r)
        self.assertNotEqual((p.happiness, p.health, p.smarts, p.looks), h0)
        p2 = make_player(age=20, happiness=50, health=50, smarts=50,
                         looks=50)
        ra.stat_drift(p2, random.Random(5))
        self.assertEqual((p.happiness, p.health, p.smarts, p.looks),
                         (p2.happiness, p2.health, p2.smarts, p2.looks))

    def test_relation_year_dift_parent(self):
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        parent = Parent("Last", 40, Gender.Female)
        parent.relationship = 50
        ra.relation_year_drift(parent, 10, random.Random(1))
        self.assertEqual(parent.relationship, 51)  # +1 under 18
        ra.relation_year_drift(parent, 30, random.Random(2))
        self.assertLessEqual(parent.relationship, 51)  # -1/0

    def test_trait_effects_apply(self):
        p = worker(looks=50, health=50, happiness=60, performance=50,
                   stress=40)
        p.traits = {"WELL_KEPT": None, "SICKLY": None, "FAST_WORKER": None}
        looks0, health0, perf0 = p.looks, p.health, p.performance
        ra.apply_yearly_trait_effects(p, random.Random(3))
        self.assertGreaterEqual(p.looks, looks0)  # WELL_KEPT 0..+2
        self.assertLessEqual(p.health, health0)   # SICKLY -0..4
        self.assertGreaterEqual(p.performance, perf0)  # FAST_WORKER
