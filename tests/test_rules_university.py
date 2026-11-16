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
    """Policy: tuition financed but not yet materialized as debt is not
    retroactively charged; the stale funding flag clears; existing debt
    survives dropout/expulsion."""

    def test_expulsion_clears_stale_flag_no_debt(self):
        p = make_player(age=20, grades=0, uv_years=2,
                        chose_student_loan=True, student_loan=0)
        run_random_events(p)
        self.assertEqual(p.uv_years, 0)
        self.assertEqual(p.student_loan, 0)
        self.assertFalse(p.chose_student_loan)

    def test_expulsion_keeps_existing_debt(self):
        # randint is pinned to 1 by run_random_events, so the year's debt
        # service repays exactly $1 of the existing principal.
        p = make_player(age=20, grades=0, uv_years=2,
                        chose_student_loan=True, student_loan=12000)
        run_random_events(p)
        self.assertEqual(p.student_loan, 11999)
        self.assertTrue(p.chose_student_loan)

    def test_graduation_materializes_debt(self):
        p = make_player(age=22, grades=50, uv_years=1,
                        chose_student_loan=True, student_loan=0)
        run_random_events(p, randint=lambda a, b: 25000
                          if (a, b) == (20000, 40000) else 1)
        # principal $25,000 materializes, then $1 legacy debt service
        self.assertEqual(p.student_loan, 24999)
