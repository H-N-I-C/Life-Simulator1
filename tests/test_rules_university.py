import unittest

from tests.test_rules_income import make_player, run_random_events


class UniversityExpulsionTest(unittest.TestCase):
    def test_expulsion_clears_university_state(self):
        # Enrolled with 2 years left and failing grades -> expelled.
        p = make_player(age=20, uv_years=2, grades=0)
        run_random_events(p)
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)

    def test_passing_grades_continue_degree(self):
        p = make_player(age=20, uv_years=2, grades=100)
        run_random_events(p)
        self.assertEqual(p.uv_years, 1)
        self.assertIsNotNone(p.grades)  # still enrolled

    def test_graduation_clears_grades(self):
        p = make_player(age=20, uv_years=1, grades=80)
        run_random_events(p)
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)


if __name__ == "__main__":
    unittest.main()


class StudentLoanPolicyTest(unittest.TestCase):
    """Policy (Claude 2026-11-16): tuition financed by a student loan
    becomes debt when financed; each enrolled year accrues a year's
    tuition; leaving without graduating keeps the accrued debt — never
    zero if any year was loan-financed."""

    def test_expulsion_keeps_accrued_debt_never_zero(self):
        # One loan-financed year accrues debt, THEN the bad grades expel.
        p = make_player(age=20, grades=0, uv_years=1,
                        chose_student_loan=True, student_loan=0)
        run_random_events(p, randint=lambda a, b: 5000
                          if (a, b) == (5000, 10000) else 1)
        self.assertEqual(p.uv_years, 0)
        self.assertTrue(p.chose_student_loan)
        # 5000 accrued, then 1 of legacy same-year debt service repaid
        self.assertEqual(p.student_loan, 4999)

    def test_graduation_keeps_accrued_debt(self):
        p = make_player(age=22, grades=50, uv_years=1,
                        chose_student_loan=True, student_loan=0)
        run_random_events(p, randint=lambda a, b: 5000
                          if (a, b) == (5000, 10000) else 1)
        self.assertEqual(p.uv_years, 0)
        self.assertIsNone(p.grades)
        self.assertEqual(p.student_loan, 4999)
