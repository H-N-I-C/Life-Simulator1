"""M2a integration tests: C's rule boundary wired through GameService —
school/work actions, rule-owned pendings, ledger-posted income."""

import os
import tempfile
import unittest

from src.game.service import GameError, GameService
from src.game.scenarios import Session


PROFILE = {
    "first_name": "Ada", "last_name": "Lee", "gender": "female",
    "country": "Australia", "seed": 4242,
}


def make_service():
    return GameService(tempfile.mkdtemp(prefix="lifesim-m2a-test-"))


def age_up_auto(svc, gid, rid):
    """Age one year, auto-resolving any pending with an enabled option.
    University applications are declined (test lifetimes stay employable)."""
    st = svc.get_state(gid)
    if st["pending_decision"]:
        d = st["pending_decision"]
        options = list(d["options"])
        if d.get("kind") == "university_application":
            options.sort(key=lambda o: 0 if any(
                k in o["id"].lower()
                for k in ("decline", "no", "not", "skip")) else 1)
        # try enabled (affordable) options first — every decision keeps
        # at least one always-available option (soft-lock rule)
        options.sort(key=lambda o: 0 if o.get("enabled", True) else 1)
        for opt in options:
            try:
                return svc.command(gid, st["revision"], rid,
                                   "resolve_decision",
                                   {"decision_id": d["id"],
                                    "option_id": opt["id"]})
            except GameError as e:
                if e.code != "not_allowed":
                    raise
        raise AssertionError("no resolvable option")
    return svc.command(gid, st["revision"], rid, "age_up", {})


def employ(svc, gid, st, base="job"):
    """Find a job via the real offer flow, retrying interview failures."""
    for attempt in range(12):
        if st["work"]["employed"]:
            return st
        st = age_up_auto(svc, gid, f"{base}-age-{attempt}")
        if st["work"]["employed"]:
            return st
        if "find_job" not in {a["id"] for a in st["available_actions"]}:
            continue
        st = svc.command(gid, st["revision"], f"{base}-{attempt}",
                         "find_job", {})
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], f"{base}-r-{attempt}",
                         "resolve_decision",
                         {"decision_id": d["id"], "option_id": "apply"})
    if not st["work"]["employed"]:
        raise AssertionError("could not find a job")
    return st


def _sess_clear(sess):
    from src.game.service import GameError
    n = 0
    while sess.state()["pending_decision"]:
        d = sess.state()["pending_decision"]
        opts = sorted(d["options"],
                      key=lambda o: 0 if o.get("enabled", True) else 1)
        for o in opts:
            try:
                sess.command(f"clr-{n}-{o['id']}", "resolve_decision",
                             {"decision_id": d["id"], "option_id": o["id"]})
                break
            except GameError as e:
                if e.code != "not_allowed":
                    raise
        else:
            raise AssertionError("stuck")
        n += 1


def age_to(svc, gid, target, start=0):
    st = svc.get_state(gid)
    guard = 0
    while st["people"][0]["age"] < target and guard < 200:
        st = age_up_auto(svc, gid, f"age-{start+guard}")
        guard += 1
        if st["status"] == "dead":
            raise AssertionError("died before target age")
    if st["pending_decision"]:
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], f"clear-{guard}",
                         "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][0]["id"]})
    return st


class SchoolJourneyTests(unittest.TestCase):
    def test_school_actions_and_study_limit(self):
        svc = make_service()
        st = svc.new_game(PROFILE, "c1")
        gid = st["game_id"]
        st = age_to(svc, gid, 8)
        actions = {a["id"]: a for a in st["available_actions"]}
        self.assertIn("study_harder", actions)
        self.assertEqual(actions["study_harder"]["category"], "education")
        st = svc.command(gid, st["revision"], "s1", "study_harder", {})
        entry = st["journal"][-1]
        self.assertEqual(entry["kind"], "education")
        actions = {a["id"]: a for a in st["available_actions"]}
        self.assertFalse(actions["study_harder"]["enabled"])
        self.assertIn("already studied", actions["study_harder"]["reason"])
        # annual limit: dispatch rejects too
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "s2", "study_harder", {})
        self.assertEqual(ctx.exception.status, 409)

    def test_skip_school_and_dropout(self):
        svc = make_service()
        st = svc.new_game(PROFILE, "c1")
        gid = st["game_id"]
        st = age_to(svc, gid, 14)
        st = svc.command(gid, st["revision"], "sk1", "skip_school", {})
        self.assertEqual(st["journal"][-1]["kind"], "education")
        # parity: at 14 parents may refuse; either branch is a real outcome
        st = svc.command(gid, st["revision"], "d1", "drop_out", {})
        text = st["journal"][-1]["text"]
        if "won't let you" in text:
            self.assertFalse(st["education"]["dropped_out"])
        else:
            self.assertTrue(st["education"]["dropped_out"])


class WorkJourneyTests(unittest.TestCase):
    def test_find_job_pending_resolve_and_employed_state(self):
        svc = make_service()
        st = svc.new_game(PROFILE, "c1")
        gid = st["game_id"]
        st = age_to(svc, gid, 19)
        actions = {a["id"]: a for a in st["available_actions"]}
        self.assertIn("find_job", actions)
        st = svc.command(gid, st["revision"], "j1", "find_job", {})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "job_offer")
        self.assertTrue(d["options"])
        # resolving applies the pre-sampled outcome
        st = svc.command(gid, st["revision"], "j2", "resolve_decision",
                         {"decision_id": d["id"], "option_id": "apply"})
        self.assertIsNone(st["pending_decision"])
        self.assertIn("work", [e["kind"] for e in st["journal"][-2:]])
        st2 = svc.get_state(gid)
        self.assertEqual(st2["work"], st["work"])  # durable

    def test_employed_year_posts_income_once(self):
        svc = make_service()
        st = svc.new_game(PROFILE, "c1")
        gid = st["game_id"]
        st = age_to(svc, gid, 19)
        st = employ(svc, gid, st, base="income")
        balance0 = st["money"]
        st = svc.command(gid, st["revision"], "y1", "age_up", {})
        income_rows = [r for r in st["finances"]["ledger"]
                       if r["turn_id"] == "y1-leg-source" or
                       r["category"] in ("income", "tax", "expenses")
                       and r["age"] == st["people"][0]["age"]]
        self.assertTrue(income_rows, "expected posted income legs")
        gross = sum(r["amount"] for r in income_rows
                    if r["category"] == "income")
        net = sum(r["amount"] for r in income_rows)
        self.assertNotEqual(st["money"], balance0)
        if gross:
            self.assertEqual(st["money"] - balance0, net)
        # retry of the same age_up cannot double-post
        rep = svc.command(gid, 9999, "y1", "age_up", {})
        self.assertEqual(rep["revision"], st["revision"])

    def test_adjust_hours_validation(self):
        svc = make_service()
        st = svc.new_game(PROFILE, "c1")
        gid = st["game_id"]
        st = age_to(svc, gid, 19)
        st = employ(svc, gid, st, base="hours")
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "h1", "adjust_hours",
                        {"hours": 10})
        self.assertEqual(ctx.exception.code, "invalid_input")
        st = svc.command(gid, st["revision"], "h2", "adjust_hours",
                         {"hours": 50})
        self.assertEqual(st["work"]["hours"], 50)

    def test_rule_pending_survives_restart(self):
        svc = make_service()
        st = svc.new_game(PROFILE, "c1")
        gid = st["game_id"]
        st = age_to(svc, gid, 19)
        st = svc.command(gid, st["revision"], "j1", "find_job", {})
        pending = st["pending_decision"]
        svc2 = GameService(svc.save_dir)
        st2 = svc2.get_state(gid)
        self.assertEqual(st2["pending_decision"]["id"], pending["id"])
        st2 = svc2.command(gid, st2["revision"], "j2", "resolve_decision",
                           {"decision_id": pending["id"],
                            "option_id": "apply"})
        self.assertIsNone(st2["pending_decision"])


if __name__ == "__main__":
    unittest.main()


class SoftLockAndShapeTests(unittest.TestCase):
    def test_every_template_has_a_free_option(self):
        from src.game import decisions
        decisions.validate_templates()  # raises if any template lacks one
        for t in decisions.TEMPLATES:
            self.assertTrue(decisions.has_free_option(t), t["id"])

    def test_zero_dollar_life_always_has_a_legal_move(self):
        # D's soft-lock: a $0 life facing a $500 decision must keep an
        # always-available option in the projection and at resolve time
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=99), "r1")
        gid = st["game_id"]
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        loan = next(t for t in __import__(
            "src.game.decisions", fromlist=["TEMPLATES"]).TEMPLATES
            if t["id"] == "old_friend_loan")
        env["player"].money = 0
        pending = __import__(
            "src.game.decisions", fromlist=["instantiate"]).instantiate(
            loan, 1, env["player"].firstname, [])
        env["pending"] = pending
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = svc.get_state(gid)
        d = st["pending_decision"]
        enabled = [o for o in d["options"] if o["enabled"]]
        self.assertTrue(enabled, "soft-lock: no enabled option at $0")
        self.assertFalse(next(o for o in d["options"]
                              if o["id"].endswith("o1"))["enabled"])
        # the free option resolves legally
        st = svc.command(gid, st["revision"], "sl1", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": enabled[0]["id"]})
        self.assertIsNone(st["pending_decision"])

    def test_live_state_matches_frozen_shapes(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=99), "r1")
        # education: grades is always an int
        self.assertIsInstance(st["education"]["grades"], int)
        for key in ("stage", "enrolled", "grades", "dropped_out",
                    "university_years_remaining",
                    "graduated_high_school", "loan"):
            self.assertIn(key, st["education"])
        # work: job_label always present
        self.assertIn("job_label", st["work"])
        for key in ("employed", "retired", "hours", "salary",
                    "effective_annual_gross", "pension", "performance",
                    "stress", "years_worked", "can_retire"):
            self.assertIn(key, st["work"])
        # finances: ledger rows carry the frozen ledger shape
        for key in ("balance", "debts", "annual_estimate",
                    "last_closed_turn", "ledger"):
            self.assertIn(key, st["finances"])
        st = svc.command(st["game_id"], st["revision"], "a1", "age_up", {})
        for row in st["finances"]["ledger"]:
            for key in ("id", "source_key", "turn_id", "age", "person_id",
                        "category", "amount", "balance_after",
                        "description"):
                self.assertIn(key, row)
        self.assertIsInstance(st["education"]["grades"], int)


class AnnualEstimateTests(unittest.TestCase):
    def test_employed_estimate_is_deterministic_quote(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="employed_30"), "r1")
        est = st["finances"]["annual_estimate"]
        self.assertIsInstance(est, dict)
        for key in ("gross", "tax", "expenses", "net"):
            self.assertIsInstance(est[key], int)
        self.assertEqual(est["gross"] - est["tax"] - est["expenses"],
                         est["net"])
        self.assertEqual(est["gross"], st["work"]["salary"])
        # deterministic: reading state twice gives the identical estimate
        st2 = svc.get_state(st["game_id"])
        self.assertEqual(st2["finances"]["annual_estimate"], est)

    def test_estimate_never_consumes_gameplay_rng(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="employed_30"), "r1")
        seq = svc.get_state(st["game_id"])
        before = seq["stats"]
        for _ in range(5):
            svc.get_state(st["game_id"])
            seq2 = svc.get_state(st["game_id"])
        self.assertEqual(seq2["stats"], before)
        self.assertEqual(seq2["finances"]["annual_estimate"],
                         seq["finances"]["annual_estimate"])

    def test_unemployed_estimate_is_zero(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="unemployed_18"), "r1")
        self.assertEqual(st["finances"]["annual_estimate"],
                         {"gross": 0, "tax": 0, "expenses": 0, "net": 0})


class DBlockerRegressionTests(unittest.TestCase):
    """D's M2a live-run blockers."""

    def _clear(self, svc, gid, st, prefer=None):
        while st["pending_decision"]:
            d = st["pending_decision"]
            opts = list(d["options"])
            if prefer:
                opts.sort(key=lambda o: 0 if prefer in o["id"] else 1)
            st = svc.command(gid, st["revision"], f"clr-{d['id']}",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opts[0]["id"]})
        return st

    def test_retired_projection_after_real_retirement(self):
        # D's flow: near_retirement_64 -> age_up -> keep_working -> retire.
        # Seeded lives vary: accept the offer path or the forced path, and
        # skip seeds where the life ends first.
        for seed in (777, 4242, 2, 3, 7, 21):
            svc = make_service()
            try:
                st = svc.new_game(
                    dict(PROFILE, seed=seed,
                         scenario="retirement_eligible_65"), "r1")
            except GameError:
                continue
            gid = st["game_id"]
            st = self._clear(svc, gid, svc.get_state(gid))
            retired = None
            sess = Session(svc, gid)
            for attempt in range(25):
                cand = sess.state()
                if cand["work"]["retired"]:
                    sess.save()
                    retired = cand
                    break
                d = cand["pending_decision"]
                if d and any("retire" in o["id"] for o in d["options"]):
                    st = sess.command(
                        "b1", "resolve_decision",
                        {"decision_id": d["id"],
                         "option_id": next(
                             o["id"] for o in d["options"]
                             if "retire" in o["id"])})
                    sess.save()
                    retired = st
                    break
                if d:
                    _sess_clear(sess)
                st = sess.command(f"a2-{attempt}", "age_up")
                if st["status"] == "dead":
                    break
            if retired is None or not retired["work"]["retired"]:
                continue  # try the next seed
            w = retired["work"]
            self.assertEqual(w["pension"], w["salary"])
            self.assertGreater(w["pension"], 0)
            self.assertFalse(w["employed"])
            self.assertFalse(w["can_retire"])
            st2 = GameService(svc.save_dir).get_state(gid)
            self.assertTrue(st2["work"]["retired"])
            self.assertEqual(st2["work"]["pension"], w["pension"])
            return
        self.fail("no seed produced a retirement within the bounds")

    def test_resolve_carried_university_funding_pending(self):
        # S2: application resolve returns the funding offer; it persists
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="university_pending_17"),
            "r2")
        gid = st["game_id"]
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "university_application")
        st = svc.command(gid, st["revision"], "c1", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][0]["id"]})
        d2 = st["pending_decision"]
        self.assertIsNotNone(d2)
        self.assertEqual(d2["kind"], "university_funding")
        # durable across restart
        st2 = GameService(svc.save_dir).get_state(gid)
        self.assertEqual(st2["pending_decision"]["id"], d2["id"])

    def test_unemployed_18_not_enrolled_and_can_find_job(self):
        # D regression: the scenario must land graduated and unemployed,
        # never university-enrolled
        svc = make_service()
        for scenario in ("unemployed_18", "graduating_18"):
            st = svc.new_game(
                dict(PROFILE, seed=4242, scenario=scenario), f"sc-{scenario}")
            self.assertFalse(st["education"]["enrolled"],
                             f"{scenario} enrolled")
            self.assertEqual(st["education"]["stage"], "none")
            actions = {a["id"] for a in st["available_actions"]}
            self.assertIn("find_job", actions)

    def test_loan_accrues_per_financed_year_no_graduation_lump(self):
        # APPROVED POLICY #3: debt accrues EACH financed year
        # (5000-10000); there is no graduation lump sum in the web path.
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="university_pending_17"),
            "r3")
        gid = st["game_id"]
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "x", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][0]["id"]})
        d2 = st["pending_decision"]
        self.assertEqual(d2["kind"], "university_funding")
        for _ in range(6):
            if st["education"]["enrolled"]:
                break
            d3 = st["pending_decision"]
            loan_opt = next((o for o in d3["options"]
                             if "loan" in o["id"]), d3["options"][0])
            st = svc.command(gid, st["revision"], "z", "resolve_decision",
                             {"decision_id": d3["id"],
                              "option_id": loan_opt["id"]})
        self.assertTrue(st["education"]["enrolled"])
        self.assertEqual(st["education"]["loan"]["funded_by"], "loan")
        self.assertFalse(st["education"]["loan"]["has_loan"])  # pre-accrual
        # age through the university years; per financed year the balance
        # grows by ONE accrual (5000-10000) minus that year's repayment
        prev_remaining = 0
        accruals = []
        for i in range(8):
            st = age_up_auto(svc, gid, f"a{i}")
            in_uv = st["education"]["stage"] == "university"
            remaining = svc.get_state(gid)["education"]["loan"]["remaining"]
            repaid = sum(-r["amount"] for r in
                         svc.get_state(gid)["finances"]["ledger"]
                         if r["category"] == "loan_repay")
            if in_uv:
                accruals.append(remaining - prev_remaining +
                                (repaid - getattr(self, "_repaid", 0)))
                self._repaid = repaid
            prev_remaining = remaining
            if svc.get_state(gid)["education"]["stage"] != "university":
                break
        self.assertGreaterEqual(len(accruals), 2)
        for accrual in accruals:
            self.assertTrue(
                5000 <= accrual <= 10000,
                f"accrual {accrual} outside per-year policy (lump sum?)")
        final = svc.get_state(gid)["education"]["loan"]
        repaid = sum(-r["amount"] for r in
                     svc.get_state(gid)["finances"]["ledger"]
                     if r["category"] == "loan_repay")
        self.assertEqual(final["remaining"], sum(accruals) - repaid)


if __name__ == "__main__":
    unittest.main()


class SoftLockAndShapeTests(unittest.TestCase):
    def test_every_template_has_a_free_option(self):
        from src.game import decisions
        decisions.validate_templates()  # raises if any template lacks one
        for t in decisions.TEMPLATES:
            self.assertTrue(decisions.has_free_option(t), t["id"])

    def test_zero_dollar_life_always_has_a_legal_move(self):
        # D's soft-lock: a $0 life facing a $500 decision must keep an
        # always-available option in the projection and at resolve time
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=99), "r1")
        gid = st["game_id"]
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        loan = next(t for t in __import__(
            "src.game.decisions", fromlist=["TEMPLATES"]).TEMPLATES
            if t["id"] == "old_friend_loan")
        env["player"].money = 0
        pending = __import__(
            "src.game.decisions", fromlist=["instantiate"]).instantiate(
            loan, 1, env["player"].firstname, [])
        env["pending"] = pending
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = svc.get_state(gid)
        d = st["pending_decision"]
        enabled = [o for o in d["options"] if o["enabled"]]
        self.assertTrue(enabled, "soft-lock: no enabled option at $0")
        self.assertFalse(next(o for o in d["options"]
                              if o["id"].endswith("o1"))["enabled"])
        # the free option resolves legally
        st = svc.command(gid, st["revision"], "sl1", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": enabled[0]["id"]})
        self.assertIsNone(st["pending_decision"])

    def test_live_state_matches_frozen_shapes(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=99), "r1")
        # education: grades is always an int
        self.assertIsInstance(st["education"]["grades"], int)
        for key in ("stage", "enrolled", "grades", "dropped_out",
                    "university_years_remaining",
                    "graduated_high_school", "loan"):
            self.assertIn(key, st["education"])
        # work: job_label always present
        self.assertIn("job_label", st["work"])
        for key in ("employed", "retired", "hours", "salary",
                    "effective_annual_gross", "pension", "performance",
                    "stress", "years_worked", "can_retire"):
            self.assertIn(key, st["work"])
        # finances: ledger rows carry the frozen ledger shape
        for key in ("balance", "debts", "annual_estimate",
                    "last_closed_turn", "ledger"):
            self.assertIn(key, st["finances"])
        st = svc.command(st["game_id"], st["revision"], "a1", "age_up", {})
        for row in st["finances"]["ledger"]:
            for key in ("id", "source_key", "turn_id", "age", "person_id",
                        "category", "amount", "balance_after",
                        "description"):
                self.assertIn(key, row)
        self.assertIsInstance(st["education"]["grades"], int)


class AnnualEstimateTests(unittest.TestCase):
    def test_employed_estimate_is_deterministic_quote(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="employed_30"), "r1")
        est = st["finances"]["annual_estimate"]
        self.assertIsInstance(est, dict)
        for key in ("gross", "tax", "expenses", "net"):
            self.assertIsInstance(est[key], int)
        self.assertEqual(est["gross"] - est["tax"] - est["expenses"],
                         est["net"])
        self.assertEqual(est["gross"], st["work"]["salary"])
        # deterministic: reading state twice gives the identical estimate
        st2 = svc.get_state(st["game_id"])
        self.assertEqual(st2["finances"]["annual_estimate"], est)

    def test_estimate_never_consumes_gameplay_rng(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="employed_30"), "r1")
        seq = svc.get_state(st["game_id"])
        before = seq["stats"]
        for _ in range(5):
            svc.get_state(st["game_id"])
            seq2 = svc.get_state(st["game_id"])
        self.assertEqual(seq2["stats"], before)
        self.assertEqual(seq2["finances"]["annual_estimate"],
                         seq["finances"]["annual_estimate"])

    def test_unemployed_estimate_is_zero(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, scenario="unemployed_18"), "r1")
        self.assertEqual(st["finances"]["annual_estimate"],
                         {"gross": 0, "tax": 0, "expenses": 0, "net": 0})


class DBlockerRegressionTests(unittest.TestCase):
    """D's M2a live-run blockers."""

    def _clear(self, svc, gid, st, prefer=None):
        while st["pending_decision"]:
            d = st["pending_decision"]
            opts = list(d["options"])
            if prefer:
                opts.sort(key=lambda o: 0 if prefer in o["id"] else 1)
            st = svc.command(gid, st["revision"], f"clr-{d['id']}",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opts[0]["id"]})
        return st

    def test_resolve_carried_university_funding_pending(self):
        # S2: application resolve returns the funding offer; it persists
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="university_pending_17"),
            "r2")
        gid = st["game_id"]
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "university_application")
        st = svc.command(gid, st["revision"], "c1", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][0]["id"]})
        d2 = st["pending_decision"]
        self.assertIsNotNone(d2)
        self.assertEqual(d2["kind"], "university_funding")
        # durable across restart
        st2 = GameService(svc.save_dir).get_state(gid)
        self.assertEqual(st2["pending_decision"]["id"], d2["id"])

    def test_unemployed_18_not_enrolled_and_can_find_job(self):
        # D regression: the scenario must land graduated and unemployed,
        # never university-enrolled
        svc = make_service()
        for scenario in ("unemployed_18", "graduating_18"):
            st = svc.new_game(
                dict(PROFILE, seed=4242, scenario=scenario), f"sc-{scenario}")
            self.assertFalse(st["education"]["enrolled"],
                             f"{scenario} enrolled")
            self.assertEqual(st["education"]["stage"], "none")
            actions = {a["id"] for a in st["available_actions"]}
            self.assertIn("find_job", actions)

    def test_loan_accrues_per_financed_year_no_graduation_lump(self):
        # APPROVED POLICY #3: debt accrues EACH financed year
        # (5000-10000); there is no graduation lump sum in the web path.
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="university_pending_17"),
            "r3")
        gid = st["game_id"]
        st = self._fund_via_loan(svc, gid, st)
        self.assertTrue(st["education"]["enrolled"])
        loan = st["education"]["loan"]
        self.assertEqual(loan["funded_by"], "loan")
        self.assertFalse(loan["has_loan"])  # no accrual before year 1 ends
        # per financed year: balance grows by ONE accrual (5000-10000)
        # minus that year's repayment (debt_service runs the same year)
        prev_remaining = 0
        prev_repaid = 0
        accruals = []
        for i in range(8):
            st = age_up_auto(svc, gid, f"a{i}")
            remaining = st["education"]["loan"]["remaining"]
            repaid = sum(-r["amount"] for r in st["finances"]["ledger"]
                         if r["category"] == "loan_repay")
            if st["education"]["stage"] == "university" or accruals:
                accruals.append(remaining - prev_remaining
                                + (repaid - prev_repaid))
            prev_remaining = remaining
            prev_repaid = repaid
            if st["education"]["stage"] != "university" and accruals:
                break
        financed = [a for a in accruals if a != 0]
        self.assertGreaterEqual(len(financed), 2)
        for accrual in financed:
            self.assertTrue(
                5000 <= accrual <= 10000,
                f"accrual {accrual} outside per-year policy (lump sum?)")
        final = svc.get_state(gid)["education"]["loan"]
        repaid = sum(-r["amount"] for r in svc.get_state(gid)[
            "finances"]["ledger"] if r["category"] == "loan_repay")
        self.assertEqual(final["remaining"], sum(financed) - repaid)

    @staticmethod
    def _fund_via_loan(svc, gid, st):
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "x", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][0]["id"]})
        for _ in range(6):
            if st["education"]["enrolled"]:
                return st
            d3 = st["pending_decision"]
            loan_opt = next((o for o in d3["options"]
                             if "loan" in o["id"]), d3["options"][0])
            st = svc.command(gid, st["revision"], "z", "resolve_decision",
                             {"decision_id": d3["id"],
                              "option_id": loan_opt["id"]})
        raise AssertionError("never enrolled")

    def test_delete_game_shape(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE), "r1")
        gid = st["game_id"]
        result = svc.delete_game(gid)
        self.assertEqual(result, {"deleted": True, "game_id": gid})
        self.assertEqual(svc.list_games()["games"], [])
        with self.assertRaises(GameError) as ctx:
            svc.get_state(gid)
        self.assertEqual(ctx.exception.code, "not_found")
        self.assertEqual(ctx.exception.status, 404)
        with self.assertRaises(GameError) as ctx:
            svc.delete_game(gid)
        self.assertEqual(ctx.exception.status, 404)
        # other games untouched
        st2 = svc.new_game(dict(PROFILE, seed=9), "r2")
        svc.delete_game(gid + "x") if False else None
        self.assertEqual(len(svc.list_games()["games"]), 1)
        self.assertEqual(svc.list_games()["games"][0]["game_id"],
                         st2["game_id"])

    def test_rule_not_found_maps_404(self):
        # B blocker: rule-side unknown targets return the frozen 404
        svc = make_service()
        st = svc.new_game(dict(PROFILE), "r1")
        gid = st["game_id"]
        st = svc.get_state(gid)
        for i in range(6):
            st = age_up_auto(svc, gid, f"g{i}")
        if st["pending_decision"]:
            st = age_up_auto(svc, gid, "g-extra")
        self.assertIsNone(st["pending_decision"])
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "n1", "conversation",
                        {"target_id": "no-such-person"})
        self.assertEqual(ctx.exception.code, "not_found")
        self.assertEqual(ctx.exception.status, 404)


class LedgerAuthorityTests(unittest.TestCase):
    def test_decision_money_posts_through_ledger_once(self):
        # every cash movement goes through the ledger exactly once;
        # person.money and the ledger balance can never diverge
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=99), "r1")
        gid = st["game_id"]
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        loan = next(t for t in __import__(
            "src.game.decisions", fromlist=["TEMPLATES"]).TEMPLATES
            if t["id"] == "old_friend_loan")
        env["player"].money = 2000
        pending = __import__(
            "src.game.decisions", fromlist=["instantiate"]).instantiate(
            loan, 1, env["player"].firstname, [])
        env["pending"] = pending
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = svc.get_state(gid)
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "lend", "resolve_decision",
                         {"decision_id": d["id"], "option_id": "d_1_o1"})
        self.assertEqual(st["money"], 1500)
        rows = [r for r in st["finances"]["ledger"]
                if "d_1_o1" in r["source_key"]]
        self.assertEqual(len(rows), 1)
        self.assertEqual(rows[0]["amount"], -500)
        self.assertEqual(rows[0]["balance_after"], st["money"])
        # identical retry returns the stored response; no second posting
        rep = svc.command(gid, 9999, "lend", "resolve_decision",
                          {"decision_id": "d_1", "option_id": "d_1_o1"})
        self.assertEqual(rep["money"], 1500)
        self.assertEqual(len([r for r in rep["finances"]["ledger"]
                              if "d_1_o1" in r["source_key"]]), 1)


class SurrenderFlowTests(unittest.TestCase):
    def test_surrender_double_confirm_ends_life(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=5), "r1")
        gid = st["game_id"]
        self.assertIn("surrender",
                      {a["id"] for a in st["available_actions"]})
        st = svc.command(gid, st["revision"], "s1", "surrender", {})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "surrender_confirm")
        # continue keeps living
        cont = next(o for o in d["options"] if "continue" in o["id"]
                    or "keep" in o["id"])
        st = svc.command(gid, st["revision"], "s2", "resolve_decision",
                         {"decision_id": d["id"], "option_id": cont["id"]})
        self.assertEqual(st["status"], "alive")
        self.assertIsNone(st["pending_decision"])
        # full flow: confirm -> final warning -> surrender
        st = svc.command(gid, st["revision"], "s3", "surrender", {})
        d = st["pending_decision"]
        final = next(o for o in d["options"]
                     if any(k in o["id"] for k in ("final", "warn", "ask")))
        st = svc.command(gid, st["revision"], "s4", "resolve_decision",
                         {"decision_id": d["id"], "option_id": final["id"]})
        d2 = st["pending_decision"]
        self.assertEqual(d2["kind"], "surrender_final")
        end = next(o for o in d2["options"]
                   if "surrender" in o["id"] or "end" in o["id"])
        st = svc.command(gid, st["revision"], "s5", "resolve_decision",
                         {"decision_id": d2["id"], "option_id": end["id"]})
        self.assertEqual(st["status"], "dead")
        self.assertEqual(st["available_actions"], [])
        self.assertTrue(any(e["kind"] == "death"
                            for e in st["journal"]))
        # durable across restart
        st2 = GameService(svc.save_dir).get_state(gid)
        self.assertEqual(st2["status"], "dead")


class SuccessionTests(unittest.TestCase):
    def _dead_with_children(self):
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="married_with_child"), "r1")
        gid = st["game_id"]
        # add a second child for the sibling check
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        env = svc._load(gid)
        player = env["player"]
        kid = Child("Sam", player.lastname, Gender.Male, 55, 60,
                    player.parents.get("Mother"), player)
        kid.age = 14
        kid.appearance.seed = 4242
        player.children.append(kid)
        player.relations.append(kid)
        # record identities before death
        before = {c.person_id: c.appearance.to_dict()
                  for c in player.children}
        before_names = {c.person_id: (c.firstname, c.lastname)
                        for c in player.children}
        money_before = player.money
        svc._save(env)
        # kill the player through a real surrender (no age cap needed)
        st = svc.get_state(gid)
        st = svc.command(gid, st["revision"], "k1", "surrender", {})
        d = st["pending_decision"]
        final = next(o for o in d["options"]
                     if any(k in o["id"] for k in ("final", "warn")))
        st = svc.command(gid, st["revision"], "k2", "resolve_decision",
                         {"decision_id": d["id"], "option_id": final["id"]})
        d2 = st["pending_decision"]
        end = next(o for o in d2["options"]
                   if "surrender" in o["id"] or "end" in o["id"])
        st = svc.command(gid, st["revision"], "k3", "resolve_decision",
                         {"decision_id": d2["id"], "option_id": end["id"]})
        self.assertEqual(st["status"], "dead")
        return svc, gid, before, before_names, money_before

    def test_dead_state_projects_eligible_successors(self):
        svc, gid, before, names, _ = self._dead_with_children()
        st = svc.get_state(gid)
        elig = {e["person_id"] for e in st["succession"]["eligible"]}
        self.assertEqual(elig, set(before))
        self.assertEqual(st["succession"]["generation_next"],
                         st["generation"] + 1)

    def test_continue_as_preserves_identity_and_balances_cash(self):
        svc, gid, before, names, money_before = self._dead_with_children()
        st = svc.get_state(gid)
        target = st["succession"]["eligible"][0]
        rev = st["revision"]
        st = svc.command(gid, rev, "c1", "continue_as",
                         {"target_id": target["person_id"]})
        self.assertEqual(st["status"], "alive")
        self.assertEqual(st["generation"], 2)
        # successor IS the same person
        player = next(p for p in st["people"]
                      if p["id"] == st["player_id"])
        self.assertEqual(player["id"], target["person_id"])
        self.assertEqual(player["appearance"], before[target["person_id"]])
        self.assertEqual(
            (player["name"].split()[0], player["name"].split()[-1]),
            names[target["person_id"]])
        # balanced cash across ALL accounts (share + residual == 0);
        # the deceased's ledger rows live outside the successor's
        # finances projection, so read the authoritative envelope
        env = svc._load(gid)
        legs = [r for r in env["ledger"] if r["turn_id"] == "t_succ_g2"]
        self.assertEqual(sum(r["amount"] for r in legs), 0)
        self.assertEqual(len([r for r in legs if r["category"] ==
                              "estate_close"]), 1)
        share = next(r for r in legs if r["category"] == "transfer")
        self.assertEqual(share["person_id"], target["person_id"])
        # completed life recorded; deceased resolvable through archive
        self.assertEqual(st["completed_lives"][0]["generation"], 1)
        people_ids = {p["id"] for p in st["people"]}
        dead = [p for p in st["people"] if not p["alive"]]
        self.assertIn(st["completed_lives"][0]["person_id"],
                      {p["id"] for p in dead})
        # old journal references still resolve
        for entry in st["journal"]:
            for pid in entry["person_ids"]:
                self.assertIn(pid, people_ids)
        # successor is not their own sibling; sibling keeps identity
        siblings = [p for p in st["people"] if p["role"] == "sibling"]
        self.assertEqual(len(siblings), 1)
        self.assertEqual(siblings[0]["id"],
                         next(i for i in before if i != target["person_id"]))
        self.assertEqual(siblings[0]["appearance"],
                         before[siblings[0]["id"]])
        # the game continues: aging works
        st = svc.command(gid, st["revision"], "n1", "age_up", {})
        self.assertEqual(st["revision"], rev + 2)

    def test_continue_as_errors(self):
        svc, gid, *_ = self._dead_with_children()
        st = svc.get_state(gid)
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "e1", "continue_as",
                        {"target_id": "no-such-child"})
        self.assertEqual(ctx.exception.status, 404)
        # living life cannot continue
        svc2 = make_service()
        st = svc2.new_game(dict(PROFILE, seed=5), "x1")
        with self.assertRaises(GameError) as ctx:
            svc2.command(st["game_id"], st["revision"], "e2", "continue_as",
                         {"target_id": "anyone"})
        self.assertEqual(ctx.exception.status, 409)

    def test_succession_replay_and_restart(self):
        svc, gid, before, names, _ = self._dead_with_children()
        st = svc.get_state(gid)
        target = st["succession"]["eligible"][0]["person_id"]
        st = svc.command(gid, st["revision"], "r1", "continue_as",
                         {"target_id": target})
        # identical retry returns the stored response without reapplying
        rep = svc.command(gid, 9999, "r1", "continue_as",
                          {"target_id": target})
        self.assertEqual(rep["revision"], st["revision"])
        self.assertEqual(rep["generation"], 2)
        svc2 = GameService(svc.save_dir)
        st2 = svc2.get_state(gid)
        self.assertEqual(st2["generation"], 2)
        self.assertEqual(st2["player_id"], target)


class DEngineGapTests(unittest.TestCase):
    def test_elderly_parent_dies_within_bounded_years(self):
        # D gap 1: relation mortality actually fires (seeded bound)
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=5), "r1")
        gid = st["game_id"]
        env = svc._load(gid)
        for p in env["player"].parents.values():
            p.age = 92
        svc._save(env)
        died = []
        for i in range(15):
            st = age_up_auto(svc, gid, f"a{i}")
            env = svc._load(gid)
            alive = {r.person_id for r in env["player"].relations}
            for pid in [p.person_id
                        for p in env["archive"]]:
                if pid not in alive and pid not in died:
                    died.append(pid)
            if len(died) >= 1:
                break
            if st["status"] == "dead":
                break
        self.assertTrue(died, "no relation died within 15 seeded years")
        # death journaled and person resolvable via archive
        st = svc.get_state(gid)
        self.assertTrue(any(e["kind"] == "death" for e in st["journal"]))
        by_id = {p["id"]: p for p in st["people"]}
        self.assertIn(died[0], by_id)
        self.assertFalse(by_id[died[0]]["alive"])

    def test_date_options_refresh_yearly_and_dating_resumes(self):
        # D gap 3: after divorce, dating is offered again in later years
        # (date_options must refresh annually, legacy reset_already_did)
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="married_30"), "r2")
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "d1", "divorce", {})
        offered_again = False
        for i in range(12):
            st = svc.get_state(gid)
            if "find_partner" in {a["id"] for a in
                                  st["available_actions"]}:
                offered_again = True
                break
            st = age_up_auto(svc, gid, f"y{i}")
        self.assertTrue(offered_again,
                         "find_partner never offered again after divorce")
        # date_options refresh directly observable: pin an out-of-band
        # value, age one year, and the legacy reset must redraw 9-11
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["player"].date_options = 99
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = age_up_auto(svc, gid, "chk")
        env = svc._load(gid)
        self.assertTrue(9 <= env["player"].date_options <= 11,
                        env["player"].date_options)

    def test_birth_naming_pending_fires_after_pregnancy(self):
        # D gap 2: a pregnancy at term produces the naming decision in
        # the age_up response (family_events phase interrupt)
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="married_30"), "r3")
        gid = st["game_id"]
        saw_naming = False
        for i in range(10):
            st = svc.get_state(gid)
            if "try_for_baby" in {a["id"] for a in
                                  st["available_actions"]}:
                try:
                    st = svc.command(gid, st["revision"], f"t{i}",
                                     "try_for_baby", {})
                except GameError as e:
                    if e.code not in ("not_allowed", "refused"):
                        raise
            if st["pending_decision"]:
                d = st["pending_decision"]
                if d.get("kind") == "pregnancy":
                    st = svc.command(gid, st["revision"], f"pr{i}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": d["options"][0]["id"]})
            # age WITHOUT draining: the naming pending must surface in
            # the age_up response itself
            st = svc.get_state(gid)
            if not st["pending_decision"]:
                st = svc.command(gid, st["revision"], f"a{i}", "age_up",
                                 {})
            d = st["pending_decision"]
            if d and d.get("kind") == "baby_naming":
                saw_naming = True
                st = svc.command(gid, st["revision"], f"n{i}",
                                 "resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": d["options"][0]["id"],
                                  "inputs": {"name": "Robin"}})
                break
            if d:
                st = age_up_auto(svc, gid, f"dr{i}")
        self.assertTrue(saw_naming, "baby naming never fired")
        st = svc.get_state(gid)
        self.assertTrue(st["family"]["children"])
        name = next(p["name"] for p in st["people"]
                    if p["id"] == st["family"]["children"][0])
        self.assertTrue(name.startswith("Robin"), name)


class BadgesValidationTests(unittest.TestCase):
    def test_v2_badges_accepted_with_rules(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, appearance={
            "skin_tone": "medium", "badges": ["glasses", "cap"]}), "b1")
        self.assertEqual(st["people"][0]["appearance"]["badges"],
                         ["glasses", "cap"])
        with self.assertRaises(GameError) as ctx:
            svc.new_game(dict(PROFILE, appearance={
                "badges": ["glasses", "crown", "flower"]}), "b2")
        self.assertEqual(ctx.exception.code, "invalid_input")
        with self.assertRaises(GameError) as ctx:
            svc.new_game(dict(PROFILE, appearance={
                "badges": ["tiara"]}), "b3")
        self.assertEqual(ctx.exception.code, "invalid_input")


class AutoEventTests(unittest.TestCase):
    def test_auto_event_applies_without_pending(self):
        # auto events (lightning, thanatophobia) never 500 the age_up:
        # they apply immediately and journal, with no pending left open
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=11, appearance={
                "schema_version": 1, "seed": 5, "skin_tone": "skin_04"}),
            "r1")
        gid = st["game_id"]
        for i in range(60):
            st = svc.get_state(gid)
            if st["pending_decision"]:
                from tests.test_game_events import \
                    EventPipelineIntegrationTests as E
                st = E()._drain(svc, gid, st, f"r{i}")
            st = svc.command(gid, st["revision"], f"a{i}", "age_up", {})
            self.assertIsNotNone(st["status"])  # never a 500
            if st["status"] == "dead":
                break


class LotteryAndAgeingTests(unittest.TestCase):
    def test_lottery_flow_with_bounded_inputs(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="unemployed_18"), "r1")
        gid = st["game_id"]
        st = svc.get_state(gid)
        actions = {a["id"] for a in st["available_actions"]}
        self.assertIn("lottery", actions)
        money0 = st["money"]
        st = svc.command(gid, st["revision"], "lo1", "lottery", {})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "lottery")
        # walk away costs nothing
        back = next(o for o in d["options"]
                    if o["id"] in ("back", "walk_away"))
        st = svc.command(gid, st["revision"], "lo2", "resolve_decision",
                         {"decision_id": d["id"], "option_id": back["id"]})
        self.assertEqual(st["money"], money0)
        # buy with bounded number inputs; ticket posts through the ledger
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["player"].money = 100
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = svc.command(gid, st["revision"], "lo3", "lottery", {})
        d = st["pending_decision"]
        one = next(o for o in d["options"] if o["id"] == "buy_one")
        st = svc.command(gid, st["revision"], "lo4", "resolve_decision",
                         {"decision_id": d["id"], "option_id": "buy_one",
                          "inputs": {"numbers": [3, 7, 12, 19]}})
        rows = [r for r in st["finances"]["ledger"]
                if "lottery" in r["source_key"]]
        self.assertTrue(rows)
        self.assertEqual(rows[0]["amount"], -4)
        # invalid inputs rejected typed, nothing applied
        st = svc.command(gid, st["revision"], "lo5", "lottery", {})
        d = st["pending_decision"]
        rev = st["revision"]
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, rev, "lo6", "resolve_decision",
                        {"decision_id": d["id"], "option_id": "buy_one",
                         "inputs": {"numbers": [3, 7, 7, 99]}})
        self.assertEqual(ctx.exception.code, "invalid_input")
        self.assertEqual(svc.get_state(gid)["revision"], rev)

    def test_ageing_subrules_apply_exactly_once(self):
        # no-double-application: each extracted subrule runs exactly once
        # per committed year
        from src.menus import rule_actions
        counts = {"drift": 0, "traits": 0, "rel_drift": 0,
                  "increment": 0}
        real_drift = rule_actions.stat_drift
        real_traits = rule_actions.apply_yearly_trait_effects
        real_inc = rule_actions.increment_age

        def spy_drift(player, rng):
            counts["drift"] += 1
            return real_drift(player, rng)

        def spy_traits(player, rng):
            counts["traits"] += 1
            return real_traits(player, rng)

        def spy_rel(relation, player_age, rng):
            counts["rel_drift"] += 1
            return real_rel(relation, player_age, rng)

        def spy_inc(person):
            counts["increment"] += 1
            return real_inc(person)

        def spy_year(relation, player_age, rng):
            counts["rel_year"] = counts.get("rel_year", 0) + 1
            return real_year(relation, player_age, rng)
        real_year = rule_actions.relation_year
        rule_actions.stat_drift = spy_drift
        rule_actions.apply_yearly_trait_effects = spy_traits
        rule_actions.increment_age = spy_inc
        rule_actions.relation_year = spy_year
        try:
            svc = make_service()
            st = svc.new_game(dict(PROFILE, seed=99), "r1")
            gid = st["game_id"]
            player_id = st["player_id"]
            age0 = st["people"][0]["age"]
            relations0 = len(st["people"]) - 1  # minus the player
            st = svc.command(gid, st["revision"], "y1", "age_up", {})
            # player drift once + one drift per relation year
            self.assertEqual(counts["drift"], 1 + relations0)
            self.assertEqual(counts["traits"], 1)
            self.assertEqual(counts.get("rel_year"), relations0)
            player = next(p for p in st["people"]
                          if p["id"] == player_id)
            self.assertEqual(player["age"], age0 + 1)
            before_ages = {p["id"]: p["age"] for p in st["people"]}
            st2 = svc.command(gid, st["revision"], "y2", "age_up", {})
            for p in st2["people"]:
                self.assertEqual(p["age"], before_ages[p["id"]] + 1,
                                 f"{p['role']} aged twice or not at all")
            self.assertEqual(counts["drift"], 2 + 2 * relations0)
        finally:
            rule_actions.stat_drift = real_drift
            rule_actions.apply_yearly_trait_effects = real_traits
            rule_actions.increment_age = real_inc
            rule_actions.relation_year = real_year


class WealthyScenarioTests(unittest.TestCase):
    def test_wealthy_married_scenario(self):
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="wealthy_married_40"), "w1")
        self.assertEqual(st["family"]["partner_status"], 2)
        self.assertGreaterEqual(st["money"], 1_000_000)
        self.assertGreaterEqual(
            next(p for p in st["people"]
                 if p["id"] == st["player_id"])["age"], 40)
        # still an ordinary game
        rev = st["revision"]
        st = svc.command(st["game_id"], rev, "w2", "age_up", {})
        self.assertEqual(st["revision"], rev + 1)


class NaturalMortalityTests(unittest.TestCase):
    def test_relation_dies_naturally_before_their_nineties(self):
        # natural life (no age forcing): a parent dies by their late 80s
        from src.game.scenarios import Session
        for seed in (777, 4242, 2):
            svc = make_service()
            st = svc.new_game(dict(PROFILE, seed=seed), f"m{seed}")
            sess = Session(svc, st["game_id"])
            died = False
            for i in range(75):
                st = sess.state()
                if st["status"] == "dead":
                    break
                if st["pending_decision"]:
                    d = st["pending_decision"]
                    opts = sorted(d["options"],
                                  key=lambda o: 0 if o.get(
                                      "enabled", True) else 1)
                    for o in opts:
                        try:
                            sess.command(f"r{i}-{o['id']}",
                                         "resolve_decision",
                                         {"decision_id": d["id"],
                                          "option_id": o["id"]})
                            break
                        except GameError:
                            continue
                    continue
                sess.command(f"a{i}", "age_up")
                st = sess.state()
                if any(e["kind"] == "death" for e in st["journal"]):
                    died = True
                    break
            if died:
                return
        self.fail("no relation death in 3 natural 75-year lives")


class TurnCursorProjectionTests(unittest.TestCase):
    def test_cursor_always_names_a_phase_after_midyear_firing(self):
        # D: after a mid-year firing the projected cursor must be a named
        # phase per the frozen phase map, never null
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=4242,
                               scenario="low_performance_30"), "f1")
        gid = st["game_id"]
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        fired = False
        for i in range(25):
            st = svc.get_state(gid)
            if st["work"]["employed"] is False and st["work"]["salary"] == 0:
                fired = True
                break
            # keep performance in the firing band (the dev hook only pins
            # the starting value; yearly drift moves it)
            with open(path, "rb") as f:
                env = _pickle.load(f)
            if env["player"].performance != 1:
                # performance 1 -> the firing roll is randint(1, 2) == 1
                env["player"].performance = 1
                with open(path, "wb") as f:
                    _pickle.dump(env, f)
            if st["pending_decision"]:
                d = st["pending_decision"]
                opts = sorted(d["options"],
                              key=lambda o: 0 if o.get("enabled", True)
                              else 1)
                for o in opts:
                    try:
                        st = svc.command(gid, st["revision"], f"r{i}",
                                         "resolve_decision",
                                         {"decision_id": d["id"],
                                          "option_id": o["id"]})
                        break
                    except GameError as e:
                        if e.code != "not_allowed":
                            raise
                continue
            st = svc.command(gid, st["revision"], f"a{i}", "age_up", {})
            if st["status"] == "dead":
                break
        self.assertTrue(fired, "player was never fired in 25 years")
        st = svc.get_state(gid)
        cursor = st["turn"]["cursor"]
        self.assertIsNotNone(cursor)
        self.assertIsInstance(cursor["phase_id"], str)
        from src.game.coordinator import PHASE_ORDER
        self.assertIn(cursor["phase_id"], PHASE_ORDER)


class InterruptedTurnResumeTests(unittest.TestCase):
    """Codex finding 1: an interrupted year resumes after resolve with
    the SAME turn id, done markers and snapshot; choices apply once."""

    def test_university_year_resumes_with_same_turn(self):
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=4242, scenario="university_pending_17"),
            "r1")
        gid = st["game_id"]
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "x", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][0]["id"]})
        d2 = st["pending_decision"]
        self.assertEqual(d2["kind"], "university_funding")
        env = svc._load(gid)
        turn_id = env["turn"]["turn_id"]
        done_at_interrupt = dict(env["turn"]["done"])
        rev = st["revision"]
        # resolve the funding choice (first affordable option)
        st = svc.command(gid, rev, "y", "resolve_decision",
                         {"decision_id": d2["id"],
                          "option_id": d2["options"][0]["id"]})
        env = svc._load(gid)
        # SAME turn resumed: id preserved, interruption markers kept
        self.assertEqual(env["turn"].get("turn_id"), turn_id)
        self.assertEqual(env["turn"]["done"], done_at_interrupt)
        # the year ran to completion afterwards (or a new pending exists)
        if env["pending"] is None:
            self.assertEqual(env["turn"].get("cursor"),
                             {"phase_id": "year_start", "item_cursor": 0})
        # reload mid-chain: everything survives a restart
        st = GameService(svc.save_dir).get_state(gid)
        self.assertEqual(st["turn"]["turn_id"], turn_id)

    def test_m1_decision_interrupt_resumes_remaining_phases(self):
        # force an M1 template at decision_offer via a patched engine:
        # resolving it must run the remaining phases in the SAME turn
        from src.game import coordinator, events as event_mod
        import random as _random
        svc = make_service()
        st = svc.new_game(dict(PROFILE, seed=99), "r1")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 30  # eligible for old_friend_loan
        env["turn"] = {"turn_id": "t_forced", "interrupted": True,
                       "cursor":
                       {"phase_id": "decision_offer", "item_cursor": 0},
                       "done": {p: True for p in
                                coordinator.PHASE_ORDER[:10]}}
        loan = next(t for t in __import__(
            "src.game.decisions", fromlist=["TEMPLATES"]).TEMPLATES
            if t["id"] == "old_friend_loan")
        import src.game.decisions as dec
        real = dec.sample
        real_load = event_mod.load_event_files
        dec.sample = lambda rng, age, used, pids: loan
        event_mod.load_event_files = lambda *a, **k: []
        try:
            res = coordinator._decision_offer(env, None, svc,
                                              _random.Random(1))
        finally:
            dec.sample = real
            event_mod.load_event_files = real_load
        # apply_cursor_update's contract: the phase result carries the
        # pending (run_turn installs it in production)
        env["pending"] = res["pending_decision"]
        self.assertIsNotNone(env["pending"])
        journal0 = len(env["journal"])
        env["turn"]["turn_id"] = "t_forced"
        svc._save(env)
        st = svc.get_state(gid)
        d = st["pending_decision"]
        free = next(o for o in d["options"]
                    if not next(c for c in
                                [o]).get("known_effects") or True)
        free = d["options"][-1]  # 'Say no' — the zero-cost choice
        st = svc.command(gid, st["revision"], "rs", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": free["id"]})
        env = svc._load(gid)
        # the resolve RESUMED the interrupted year: either it is still
        # open under the SAME turn id (another decision paused it again)
        # or it ran to completion and only then reset — never discarded
        if env["turn"].get("interrupted"):
            self.assertEqual(env["turn"].get("turn_id"), "t_forced")
            self.assertIn("decision_offer", env["turn"]["done"])
        else:
            self.assertEqual(env["turn"].get("cursor"),
                             {"phase_id": "year_start", "item_cursor": 0})
        self.assertGreaterEqual(len(env["journal"]), journal0)
