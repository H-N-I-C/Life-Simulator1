"""M3 §Careers tests (Kimi A, 2026-11-20).

Contract: docs/collab/M3/02-contracts.md §Careers. Real GameService,
deterministic rules; 3 seeds where sampling matters.

Run: TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_careers
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

SEEDS = [7, 4242, 90210]

DATA_DIR = os.path.join(os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(DATA_DIR, exist_ok=True)


def make_service():
    return GameService(tempfile.mkdtemp(dir=DATA_DIR))


def new_adult_game(svc, seed, request="c"):
    """Fresh game with the player force-aged to 18 (env edit, no RNG)."""
    st = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                       "gender": "female", "country": "Australia",
                       "seed": seed}, request)
    env = svc._load(st["game_id"])
    env["player"].age = 18
    svc._save(env)
    return svc.get_state(st["game_id"])


def grant(env_svc, gid, qualifications=(), skills=None):
    env = env_svc._load(gid)
    if qualifications:
        edu = env.setdefault("education_state", {})
        qs = edu.setdefault("qualifications", [])
        for q in qualifications:
            if q not in {x["qualification_id"] for x in qs}:
                qs.append({"qualification_id": q,
                           "program_id": q, "awarded_turn": "test"})
    if skills:
        # frozen §Skills shape (C-owned state): backfill via C's accessor
        from src.game.features.skills import state as skills_state
        values = skills_state.values(env, env["player"].person_id)
        values.update(skills)
    env_svc._save(env)


def apply_and_accept(svc, gid, st, role, employer, tag):
    st = svc.command(gid, st["revision"], f"{tag}-a", "careers_apply",
                     {"role_id": role, "employer_id": employer})
    d = st["pending_decision"]
    assert d["kind"] == "career_offer", d.get("kind")
    return svc.command(gid, st["revision"], f"{tag}-r", "resolve_decision",
                       {"decision_id": d["id"], "option_id": "accept"})


class ProjectionTests(unittest.TestCase):
    def test_openings_shape_and_data_reachability(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        view = st["careers"]
        self.assertIsNone(view["assignment"])
        self.assertEqual(view["history"], [])
        openings = view["openings"]
        self.assertEqual(len(openings), 18)
        roles, employers = careers.data()
        self.assertEqual(len(roles), 12)
        self.assertEqual(len(employers), 6)
        for o in openings:
            self.assertIn(o["role_id"], roles)
            self.assertIn(o["employer_id"], employers)
            self.assertLessEqual(o["pay_min"], o["pay_max"])
            self.assertIn("requirements", o)
            self.assertIn("eligible", o)
        # every role is offered somewhere; every promotion target exists
        offered = {o["role_id"] for o in openings}
        self.assertEqual(offered, set(roles))
        for r in roles.values():
            for t in r.get("promotions_to", []):
                self.assertIn(t, roles)
        # three complete ladders (entry -> top)
        for path, top in (("electrical", "site_supervisor"),
                          ("office", "operations_manager"),
                          ("healthcare", "senior_nurse")):
            self.assertIn(top, {r["id"] for r in roles.values()
                                if r["path"] == path})

    def test_inspection_never_samples_or_costs(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        grant(svc, st["game_id"], skills={"communication": 50})
        st = svc.get_state(st["game_id"])
        before = (st["money"], st["revision"],
                  len(st["finances"]["ledger"]))
        for _ in range(5):
            st = svc.get_state(st["game_id"])
        self.assertEqual((st["money"], st["revision"],
                          len(st["finances"]["ledger"])), before)


class ApplyFlowTests(unittest.TestCase):
    def test_prerequisite_rejection_matches_advertised_reason(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        # nurse: needs the nursing degree (B's credential id) + care
        # worker experience
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "nurse")
        self.assertFalse(opening["eligible"])
        self.assertTrue(opening["reason"])
        with self.assertRaises(GameError) as ctx:
            svc.command(st["game_id"], st["revision"], "x1", "careers_apply",
                        {"role_id": "nurse", "employer_id": "staria"})
        self.assertEqual(ctx.exception.code, "not_allowed")
        # dispatch refusal names the same requirement
        self.assertIn("Bachelor of Nursing", str(ctx.exception))

    def test_happy_path_apply_accept_resign_and_adapter(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_adult_game(svc, seed, f"h{seed}")
            gid = st["game_id"]
            grant(svc, gid, skills={"communication": 50})
            st = svc.get_state(gid)
            money_before = st["money"]
            st = svc.command(gid, st["revision"], "a1", "careers_apply",
                             {"role_id": "office_assistant",
                              "employer_id": "meridian"})
            d = st["pending_decision"]
            self.assertEqual(d["kind"], "career_offer")
            self.assertEqual(d["display_category"], "work")
            self.assertEqual(d["options"][0]["known_costs"], [])
            offer = careers.ensure(svc._load(gid))["offers"]["co_1"]
            self.assertEqual(offer["status"], "open")
            self.assertGreaterEqual(offer["salary"], 30000)
            self.assertLessEqual(offer["salary"], 36000)
            # no money moved by applying
            self.assertEqual(st["money"], money_before)
            st = svc.command(gid, st["revision"], "a1r", "resolve_decision",
                             {"decision_id": d["id"], "option_id": "accept"})
            a = st["careers"]["assignment"]
            self.assertEqual(a["role_label"], "Office assistant")
            self.assertEqual(a["employer_name"], "Meridian Office Group")
            # legacy adapter: M2 payroll scalars carry the named terms
            self.assertTrue(st["work"]["employed"])
            self.assertEqual(st["work"]["salary"], a["salary"])
            self.assertEqual(st["work"]["hours"], a["hours"])
            # offer terms snapshotted
            env = svc._load(gid)
            self.assertEqual(careers.ensure(env)["offers"]["co_1"]
                             ["status"], "accepted")
            # resign clears assignment + adapter
            st = svc.command(gid, st["revision"], "q1", "careers_resign", {})
            self.assertIsNone(st["careers"]["assignment"])
            self.assertFalse(st["work"]["employed"])
            self.assertEqual(st["work"]["salary"], 0)
            self.assertTrue(st["careers"]["history"])

    def test_decline_keeps_freedom_and_marks_offer(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        grant(svc, st["game_id"], skills={"care": 20})
        st = svc.get_state(st["game_id"])
        gid = st["game_id"]
        st = svc.command(gid, st["revision"], "a1", "careers_apply",
                         {"role_id": "care_assistant", "employer_id": "bluegum"})
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "a1r", "resolve_decision",
                         {"decision_id": d["id"], "option_id": "decline"})
        self.assertIsNone(st["careers"]["assignment"])
        self.assertFalse(st["work"]["employed"])
        env = svc._load(gid)
        self.assertEqual(careers.ensure(env)["offers"]["co_1"]["status"],
                         "declined")

    def test_attempt_cap_per_year(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        gid = st["game_id"]
        grant(svc, gid, skills={"practical": 60, "communication": 60})
        st = svc.get_state(gid)
        # cap applications at 2: two opens offers, third refused
        st = svc.command(gid, st["revision"], "p1", "careers_apply",
                         {"role_id": "office_assistant", "employer_id": "meridian"})
        d1 = st["pending_decision"]["id"]
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "p1x", "careers_apply",
                        {"role_id": "office_assistant", "employer_id": "cascade"})
        self.assertEqual(ctx.exception.code, "pending_decision")
        st = svc.command(gid, st["revision"], "r1", "resolve_decision",
                         {"decision_id": d1, "option_id": "decline"})
        st = svc.command(gid, st["revision"], "p2", "careers_apply",
                         {"role_id": "office_assistant", "employer_id": "meridian"})
        d2 = st["pending_decision"]["id"]
        st = svc.command(gid, st["revision"], "r2", "resolve_decision",
                         {"decision_id": d2, "option_id": "decline"})
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "p3", "careers_apply",
                        {"role_id": "office_assistant", "employer_id": "meridian"})
        self.assertEqual(ctx.exception.code, "not_allowed")
        self.assertIn("twice", str(ctx.exception))

    def test_one_assignment_at_a_time(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        gid = st["game_id"]
        grant(svc, gid, skills={"communication": 50})
        st = apply_and_accept(svc, gid, svc.get_state(gid),
                              "office_assistant", "meridian", "w")
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "w2", "careers_apply",
                        {"role_id": "coordinator", "employer_id": "cascade"})
        self.assertEqual(ctx.exception.code, "not_allowed")
        self.assertIn("already have a job", str(ctx.exception))


class PromotionAndPayrollTests(unittest.TestCase):
    def _become_assistant(self, seed=7):
        svc = make_service()
        st = new_adult_game(svc, seed, f"pr{seed}")
        gid = st["game_id"]
        grant(svc, gid, skills={"practical": 70, "organization": 60})
        st = apply_and_accept(svc, gid, svc.get_state(gid),
                              "app_electrician", "voltworks", "e")
        # solid performance so promotion gates hit tenure/quals, not perf
        env = svc._load(gid)
        env["player"].performance = 70
        svc._save(env)
        return svc, gid, svc.get_state(gid)

    def test_tenure_accrues_and_promotion_requires_gates(self):
        svc, gid, st = self._become_assistant()
        # promotion before tenure: refused with precise reason
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "g0", "careers_promote",
                        {"role_id": "electrician"})
        self.assertIn("years", str(ctx.exception))
        # two aged years -> role_tenure 2 (annual hook after work_income)
        n = 0
        for i in range(2):
            st = svc.command(gid, st["revision"], f"y{i}", "age_up", {})
            while st["pending_decision"]:
                d = st["pending_decision"]
                o = next(x for x in d["options"] if x.get("enabled", True))
                st = svc.command(gid, st["revision"], f"yr-{n}",
                                 "resolve_decision",
                                 {"decision_id": d["id"], "option_id": o["id"]})
                n += 1
        self.assertEqual(st["careers"]["assignment"]["role_tenure"], 2)
        # still missing the qualification gate
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "g1", "careers_promote",
                        {"role_id": "electrician"})
        self.assertIn("Electrical trade certificate", str(ctx.exception))
        # grant it (B bridge) -> promote -> accept -> payroll moves
        grant(svc, gid, qualifications=["electrical_trade_cert"])  # B id
        st = svc.get_state(gid)
        salary_before = st["careers"]["assignment"]["salary"]
        st = svc.command(gid, st["revision"], "g2", "careers_promote",
                         {"role_id": "electrician"})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "career_offer")
        self.assertTrue(d["title"].startswith("Promotion"))
        st = svc.command(gid, st["revision"], "g2r", "resolve_decision",
                         {"decision_id": d["id"], "option_id": "accept"})
        a = st["careers"]["assignment"]
        self.assertEqual(a["role_label"], "Electrician")
        self.assertGreater(a["salary"], salary_before)
        self.assertTrue(st["work"]["employed"])
        self.assertEqual(st["work"]["salary"], a["salary"])
        # history records the promotion
        self.assertEqual(st["careers"]["history"][-1]["reason"],
                         "promoted")

    def test_payroll_posts_once_from_adapter_next_period(self):
        svc, gid, st = self._become_assistant()
        salary = st["careers"]["assignment"]["salary"]
        # one aged year posts exactly the M2 salary legs (one wage payer);
        # the careers feature adds no legs of its own
        st = svc.command(gid, st["revision"], "y1", "age_up", {})
        n = 0
        while st["pending_decision"]:
            d = st["pending_decision"]
            o = next(x for x in d["options"] if x.get("enabled", True))
            st = svc.command(gid, st["revision"], f"yr-{n}", "resolve_decision",
                             {"decision_id": d["id"], "option_id": o["id"]})
            n += 1
        legs = [e for e in st["finances"]["ledger"]
                if e.get("category") == "income"]
        gross = [e for e in legs if e.get("amount", 0) > 0]
        self.assertEqual(len(gross), 1, "exactly one wage leg per year")
        self.assertGreater(gross[0]["amount"], 0)


class ReplayAndMigrationTests(unittest.TestCase):
    def test_apply_is_replay_safe_and_offer_reloads(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_adult_game(svc, seed, f"rp{seed}")
            gid = st["game_id"]
            grant(svc, gid, skills={"care": 20})
            st = svc.get_state(gid)
            payload = {"role_id": "care_assistant", "employer_id": "bluegum"}
            st1 = svc.command(gid, st["revision"], "a1", "careers_apply",
                              payload)
            st2 = svc.command(gid, st["revision"], "a1", "careers_apply",
                              payload)  # retry: byte-identical stored response
            self.assertEqual(st1, st2)
            # reload: the sampled offer survives (no re-sample on read)
            st3 = svc.get_state(gid)
            env = svc._load(gid)
            offer = careers.ensure(env)["offers"]["co_1"]
            self.assertEqual(offer["status"], "open")
            self.assertEqual(st3["pending_decision"]["id"],
                             st1["pending_decision"]["id"])

    def test_legacy_save_without_careers_state(self):
        svc = make_service()
        st = svc.new_game({"first_name": "Old", "last_name": "Save",
                           "gender": "female", "country": "Australia",
                           "seed": 3}, "leg")
        gid = st["game_id"]
        env = svc._load(gid)
        env.pop("careers", None)
        env["player"].age = 30
        env["player"].has_job = True
        env["player"].salary = 45000
        svc._save(env)
        st = svc.get_state(gid)
        self.assertEqual(st["careers"]["assignment"], None)
        self.assertEqual(len(st["careers"]["openings"]), 18)
        # legacy generic pay untouched by the feature
        self.assertTrue(st["work"]["employed"])
        self.assertEqual(st["work"]["salary"], 45000)
        # re-validation is idempotent
        st = svc.get_state(gid)
        self.assertEqual(len(st["careers"]["openings"]), 18)


if __name__ == "__main__":
    unittest.main()


class RobotFindingFixTests(unittest.TestCase):
    """D robot run (seed 41) findings — fixes pinned here."""

    def test_apply_inputs_are_choice_selects(self):
        # (1) advertised inputs are choice selects with real values, not
        # free text (robot sent ~90 bogus 400s free-typing ids); M3c
        # Amendment 5 pairs them: only VALID role@employer openings are
        # published
        svc = make_service()
        st = new_adult_game(svc, 7)
        apply = next(a for a in st["available_actions"]
                     if a["id"] == "careers_apply")
        self.assertEqual([i["id"] for i in apply["inputs"]], ["opening"])
        opening = apply["inputs"][0]
        self.assertEqual(opening["type"], "choice")
        self.assertTrue(opening["choices"])
        self.assertIn("office_assistant@meridian", opening["choices"])
        # invalid combinations are never advertised
        self.assertNotIn("office_assistant@voltworks", opening["choices"])
        self.assertNotIn("nurse@meridian", opening["choices"])
        # every advertised pair is a real opening (employer offers role)
        roles, employers = careers.data()
        for pair in opening["choices"]:
            rid, eid = pair.split("@")
            self.assertIn(rid, roles)
            self.assertIn(eid, employers)
            self.assertIn(rid, employers[eid]["role_ids"])
        # dispatch accepts the advertised opening string...
        grant(svc, st["game_id"], skills={"communication": 50})
        st = svc.get_state(st["game_id"])
        st = svc.command(st["game_id"], st["revision"], "b0", "careers_apply",
                         {"opening": "office_assistant@meridian"})
        self.assertEqual(st["pending_decision"]["kind"], "career_offer")
        d = st["pending_decision"]
        svc.command(st["game_id"], st["revision"], "b0r", "resolve_decision",
                    {"decision_id": d["id"], "option_id": "decline"})
        # ...and still validates malformed/invalid pairs (typed invalid)
        st = svc.get_state(st["game_id"])
        with self.assertRaises(GameError) as ctx:
            svc.command(st["game_id"], st["revision"], "b1", "careers_apply",
                        {"opening": "nurse@voltworks"})
        self.assertEqual(ctx.exception.code, "invalid_input")
        with self.assertRaises(GameError) as ctx:
            svc.command(st["game_id"], st["revision"], "b2", "careers_apply",
                        {"opening": "a@b@c"})
        self.assertEqual(ctx.exception.code, "invalid_input")
        with self.assertRaises(GameError) as ctx:
            svc.command(st["game_id"], st["revision"], "b3", "careers_apply",
                        {"role_id": "nurse", "employer_id": "voltworks"})
        self.assertEqual(ctx.exception.code, "invalid_input")

    def test_promote_inputs_are_choices_from_assignment(self):
        svc = make_service()
        st = new_adult_game(svc, 7)
        grant(svc, st["game_id"], skills={"practical": 60})
        st = apply_and_accept(svc, st["game_id"], svc.get_state(st["game_id"]),
                              "app_electrician", "voltworks", "pc")
        promote = next(a for a in st["available_actions"]
                       if a["id"] == "careers_promote")
        self.assertEqual(promote["inputs"][0]["choices"], ["electrician"])

    def test_credentials_match_education_program_ids(self):
        # (2) role gates reference B's actual credential ids — the care
        # and nursing roles were gated forever by typos
        svc = make_service()
        st = new_adult_game(svc, 7)
        gid = st["game_id"]
        # qualified_care_worker: WRONG old id refused, B's id accepted
        grant(svc, gid, qualifications=["care_diploma"],
              skills={"care": 40})
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, svc.get_state(gid)["revision"], "w1",
                        "careers_apply",
                        {"role_id": "qualified_care_worker",
                         "employer_id": "bluegum"})
        self.assertIn("Care diploma", str(ctx.exception))
        grant(svc, gid, qualifications=["care_diploma_cert"])
        st = svc.get_state(gid)
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "qualified_care_worker")
        # prior care_assistant experience still required (honest gate)
        self.assertFalse(opening["eligible"])
        self.assertIn("Care assistant", opening["reason"])
        # nurse: B's nursing_degree + experience + age + skills
        grant(svc, gid, qualifications=["nursing_degree"],
              skills={"care": 60, "communication": 40})
        env = svc._load(gid)
        env["player"].age = 25
        # simulate experience by a prior assignment record
        stt = careers.ensure(env)
        stt["past_assignments"][env["player"].person_id] = [{
            "assignment_id": "ca_x", "role_id": "qualified_care_worker",
            "employer_id": "bluegum", "salary": 45000,
            "accepted_turn": "t", "to_turn": "t", "status": "left"}]
        svc._save(env)
        st = svc.get_state(gid)
        opening = next(o for o in st["careers"]["openings"]
                       if o["role_id"] == "nurse" and
                       o["employer_id"] == "staria")
        self.assertTrue(opening["eligible"], opening["reason"])
