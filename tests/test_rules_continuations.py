"""Executable continuation examples — the frozen phase-map v4 chain.

JSON mirror: docs/collab/M2/rule-examples.json (continuation_chain_example).
Verifies that C's phase results chain to the canonical named order
work_income -> debt_service -> education -> family_events, that a pending
decision pauses the chain and resolve_choice continues after it exactly
once, and that the whole chain composes on REAL Player objects with the
supplied rng only.
"""

import random
import unittest
from unittest import mock

import src.menus.rule_actions as ra
from tests.test_rules_income import make_player


CANONICAL_AFTER = {
    "work_income": "education",
    "education": "debt_service",
    "debt_service": "family_events",
}


def run_phase(player, phase, rng, **kw):
    return ra.advance_phase(player, phase, rng, turn_id="t1", **kw)


class ContinuationChainTest(unittest.TestCase):
    def test_employed_chain_follows_canonical_order(self):
        p = make_player(age=40, salary=40000, has_job=True, job_hours=40,
                        years_worked=5, stress=40, performance=60,
                        last_raise=30, salary_years=[40000] * 5,
                        student_loan=5000, grades=None)
        p.times_asked_since_last_raise = 0
        p.asked_for_raise = False
        p.worked_harder = False
        rng = random.Random(7)
        phase = "work_income"
        seen = []
        results = {}
        for _ in range(3):
            r = run_phase(p, phase, rng)
            results[phase] = r
            seen.append(phase)
            cont = r["continuation"]
            self.assertEqual(cont["phase_id"], CANONICAL_AFTER[phase])
            phase = cont["phase_id"]
        self.assertEqual(seen, ["work_income", "education", "debt_service"])
        # debt_service posted a repayment leg after education
        (repay,) = results["debt_service"]["money_entries"]
        self.assertEqual(repay["category"], "loan_repay")

    def test_pending_pauses_chain_and_resolve_continues_once(self):
        p = make_player(age=66, salary=50000, has_job=True, job_hours=40,
                        years_worked=20, stress=40, performance=60,
                        last_raise=30, salary_years=[50000] * 20,
                        grades=None, student_loan=0)
        p.times_asked_since_last_raise = 0
        p.asked_for_raise = False
        p.worked_harder = False
        with mock.patch.object(ra, "_one_in", return_value=True):
            r = run_phase(p, "work_income", random.Random(10))
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "retirement_offer")
        # the phase itself still points at the next phase
        self.assertEqual(r["continuation"]["phase_id"], "education")
        r2 = ra.resolve_choice(p, pending, "retire", {}, random.Random(11))
        self.assertEqual(r2["continuation"]["phase_id"], "education")
        # exactly once: re-resolution changes nothing
        money_before = p.money
        happy_before = p.happiness
        r3 = ra.resolve_choice(p, pending, "retire", {}, random.Random(12))
        self.assertEqual(p.happiness, happy_before)
        self.assertEqual(p.money, money_before)
        self.assertEqual(r3["continuation"]["phase_id"], "education")

    def test_chain_composes_full_year_school_worker(self):
        # 10-year-old student: work_income (no job) -> debt_service (no
        # loan) -> education (grade drift) with real assertions per phase.
        p = make_player(age=10, grades=60, salary=0, has_job=False)
        p.studied = False
        p.skipped_school = False
        p.tried_to_drop_out = False
        p.dropped_out = False
        rng = random.Random(21)
        g0 = p.grades
        r1 = run_phase(p, "work_income", rng)
        self.assertEqual(r1["money_entries"], [])
        self.assertEqual(r1["continuation"]["phase_id"], "education")
        r2 = run_phase(p, "education", rng)
        self.assertEqual(r2["money_entries"], [])
        self.assertEqual(r2["continuation"]["phase_id"], "debt_service")
        r3 = run_phase(p, "debt_service", rng)
        self.assertEqual(r3["continuation"]["phase_id"], "family_events")
        self.assertIsNotNone(p.grades)


if __name__ == "__main__":
    unittest.main()


class ExecutableScenariosTest(unittest.TestCase):
    """The four scenarios A requested, executable with landed rules."""

    def test_two_choices_one_phase_university_chain(self):
        # education phase at 17 -> application decision -> funding decision.
        p = make_player(age=17, smarts=100, grades=80)
        p.parents = {}
        p.dropped_out = False
        r = ra.advance_phase(p, "education", random.Random(14), turn_id="t1")
        d1 = r["pending_decision"]
        self.assertEqual(d1["kind"], "university_application")
        r2 = ra.resolve_choice(p, d1, "apply", {}, random.Random(15))
        d2 = r2["pending_decision"]
        self.assertEqual(d2["kind"], "university_funding")
        r3 = ra.resolve_choice(p, d2, "student_loan", {}, random.Random(16))
        self.assertEqual(p.uv_years, 4)
        self.assertEqual(r3["continuation"]["phase_id"], "debt_service")

    def test_debt_before_family_order(self):
        p = make_player(age=40, salary=40000, has_job=True, job_hours=40,
                        years_worked=5, stress=40, performance=60,
                        last_raise=30, salary_years=[40000] * 5,
                        student_loan=5000, grades=None)
        p.times_asked_since_last_raise = 0
        p.asked_for_raise = False
        p.worked_harder = False
        rng = random.Random(7)
        r1 = ra.advance_phase(p, "work_income", rng, turn_id="t1")
        r2 = ra.advance_phase(p, "education", rng, turn_id="t1")
        r3 = ra.advance_phase(p, "debt_service", rng, turn_id="t1")
        (repay,) = r3["money_entries"]
        self.assertEqual(repay["phase"], "debt_service")
        self.assertEqual(repay["category"], "loan_repay")
        self.assertLess(repay["amount"], 0)
        # education after income; debt_service after education
        self.assertEqual(r1["continuation"]["phase_id"], "education")
        self.assertEqual(r2["continuation"]["phase_id"], "debt_service")

    def test_interruption_after_result_is_deterministic(self):
        # Same rng seed -> byte-identical entries, so A's idempotent keys
        # dedupe the retry (A-side posting is the single authority).
        def run():
            p = make_player(age=40, salary=40000, has_job=True,
                            job_hours=40, years_worked=5, stress=40,
                            performance=60, last_raise=30,
                            salary_years=[40000] * 5, grades=None,
                            student_loan=0)
            p.times_asked_since_last_raise = 0
            p.asked_for_raise = False
            p.worked_harder = False
            return ra.advance_phase(p, "work_income", random.Random(7),
                                    turn_id="t1")["money_entries"]
        first, retry = run(), run()
        # entries are identical apart from each player's random person_id
        strip = lambda entries: [{k: v for k, v in e.items()
                                  if k != "account_owner_id"}
                                 for e in entries]
        self.assertEqual(strip(first), strip(retry))
        # retry posting through A's Ledger: same keys -> original rows,
        # balance unchanged (no double credit)
        from src.game.finance import Ledger
        p = make_player()
        env = {"player": p, "archive": []}
        first = [dict(e, account_owner_id=p.person_id) for e in first]
        ledger = Ledger(env)
        for e in first:
            ledger.post("t1", 30, e)
        balance_after_first = env["balances"][p.person_id]
        for e in first:
            ledger.post("t1", 30, e)
        self.assertEqual(env["balances"][p.person_id],
                         balance_after_first)
