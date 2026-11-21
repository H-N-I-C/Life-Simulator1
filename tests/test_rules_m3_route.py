"""M3b B1 real-route tests (Kimi C, 2026-11-21).

C-side verification of the wrk_/stu_ content through A's landed seam
(Amendment 4, docs/collab/M3/02-contracts.md): the new `workplace_events`
yearly phase in coordinator. REAL GameService only: real age_up commands,
real resolve_decision, fresh-service reload, real undo_year. Proves:

- an active named assignment makes the route offer + resolve a wrk_ via
  the production command boundary (outcome journaled, seen-cadence kept);
- a wrong ladder / wrong program stays closed THROUGH THE ROUTE (every
  offered record still passes the content condition tree for the player's
  actual role/program);
- no active source -> the families are silently skipped;
- a working student (apprenticeship = one of each source) can be served
  BOTH families in one aged year;
- every offered option is free and a money-carrying choice posts exactly
  one ledger leg at resolve;
- the offered pending survives a fresh-service reload and resolves once;
- undo rolls the seen-cadence back: the same record is offered again.

Run: python3 -m unittest tests.test_rules_m3_route
"""

import json
import os
import shutil
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

os.environ.setdefault("TMPDIR", os.path.expanduser("~/.lifesim-tmp"))
os.makedirs(os.environ["TMPDIR"], exist_ok=True)

from src.game import events  # noqa: E402
from src.game.context import RuleContext  # noqa: E402
from src.game.service import GameService  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}
OFFICE_LADDER = ("office_assistant", "coordinator", "operations_specialist",
                 "operations_manager")


def content_events(name):
    with open(os.path.join(ROOT, "src", "game", "content", "events",
                           "%s.json" % name), encoding="utf-8") as f:
        return json.load(f)["events"]


def option_by_id(ev, option_id):
    # v3 pendings namespace option ids as f"d_{seq}_{choice_id}"
    for c in ev["choices"]:
        if c["id"] == option_id or option_id.endswith("_" + c["id"]):
            return c
    raise AssertionError("%s: no option %r" % (ev["id"], option_id))


class RouteTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m3-route-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-create")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 20
        self.svc._save(env)

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def env(self):
        return self.svc._load(self.gid)

    # ----------------------------------------------------------- helpers

    def _pick_choice(self, d):
        options = d["options"]
        for o in options:
            if o.get("enabled", True) is False:
                continue
            if not o.get("known_costs"):
                return o
        return options[-1]

    def _inputs_for(self, choice):
        inputs = {}
        for inp in choice.get("inputs") or []:
            if inp.get("type") == "numbers":
                inputs[inp["id"]] = ([[1, 2, 3, 4]] * inp.get("lines", 1)
                                     if inp.get("lines") else [1, 2, 3, 4])
            else:
                inputs[inp["id"]] = "Robin"
        return inputs

    def _take_job(self, role, employer):
        from src.game.features.skills import state as skills_state
        env = self.env()
        skills_state.values(env, env["player"].person_id).update(
            {"communication": 50, "practical": 50, "care": 50})
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        st = self._drain(st, "pre")
        st = self.svc.command(self.gid, st["revision"], "r-apply",
                              "careers_apply",
                              {"role_id": role, "employer_id": employer})
        d = st["pending_decision"]
        self.assertEqual(d["kind"], "career_offer")
        st = self.svc.command(self.gid, st["revision"], "r-accept",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": "accept", "inputs": {}})
        self.assertIsNone(st.get("pending_decision"))
        env = self.env()
        env["player"].performance = 100  # belt + braces vs M2 firing rule
        self.svc._save(env)

    def _enroll(self, program_id):
        st = self.svc.get_state(self.gid)
        st = self._drain(st, "pre")
        env = self.env()
        env["player"].money = 30000
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "r-enroll",
                              "education_enroll",
                              {"program_id": program_id})
        d = st["pending_decision"]
        if d is not None:
            self.assertEqual(d["kind"], "education_funding")
            st = self.svc.command(self.gid, st["revision"], "r-fund",
                                  "resolve_decision",
                                  {"decision_id": d["id"],
                                   "option_id": "self", "inputs": {}})
            self.assertIsNone(st.get("pending_decision"))
        return self.env()

    def _drain(self, st, tag):
        """Resolve every pending until the year/turn completes."""
        n = 0
        while st.get("pending_decision"):
            d = st["pending_decision"]
            choice = self._pick_choice(d)
            st = self.svc.command(
                self.gid, st["revision"], "%s-d%d" % (tag, n),
                "resolve_decision",
                {"decision_id": d["id"], "option_id": choice["id"],
                 "inputs": self._inputs_for(choice)})
            n += 1
            self.assertLess(n, 40, "pending chain did not drain")
        return st

    def age_year(self, tag):
        """Age one full year; returns (offers, resolved).

        offers: [(family_prefix, event_id, pending)] for every wrk_/stu_
        occurrence the route served this year; resolved maps
        event_id -> (option_id, ledger_legs_before).
        """
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "%s-age" % tag,
                              "age_up", {})
        offers, resolved = [], {}
        n = 0
        while st.get("pending_decision"):
            d = st["pending_decision"]
            eid = d.get("event_id") or ""
            choice = self._pick_choice(d)
            if eid.startswith(("wrk_", "stu_")):
                offers.append((eid[:3], eid, d))
                env = self.env()
                resolved[eid] = (
                    choice["id"],
                    len(env["ledger"]))
                for o in d["options"]:
                    self.assertFalse(
                        o.get("known_costs"),
                        "%s:%s must be a free exit (got %r)"
                        % (eid, o["id"], o.get("known_costs")))
            st = self.svc.command(
                self.gid, st["revision"], "%s-d%d" % (tag, n),
                "resolve_decision",
                {"decision_id": d["id"], "option_id": choice["id"],
                 "inputs": self._inputs_for(choice)})
            n += 1
            self.assertLess(n, 40, "pending chain did not drain")
        return offers, resolved

    def eligible_ids(self, name, env):
        ctx = RuleContext(env, self.svc)
        pid = env["player"].person_id
        out = set()
        for ev in content_events(name):
            if events.evaluate_v3_conditions(ev.get("conditions") or {},
                                             ctx, {"actor": pid}, pid):
                out.add(ev["id"])
        return out

    # ------------------------------------------------------------- tests

    def test_work_route_offers_and_resolves_a_wrk_record(self):
        self._take_job("office_assistant", "meridian")
        offers, resolved = self.age_year("y1")
        wrk = [o for o in offers if o[0] == "wrk"]
        self.assertTrue(wrk, "an employed player must be served a wrk_ "
                             "record through the yearly route")
        eid = wrk[0][1]
        ev = next(e for e in content_events("m3_work") if e["id"] == eid)
        option_id, _ = resolved[eid]
        expected = option_by_id(ev, option_id)["outcome_text"]
        texts = [j["text"] for j in self.env()["journal"]]
        self.assertIn(expected, texts,
                      "the resolved outcome text must be journaled")
        self.assertIn(eid, self.env().get("wrk_stu_seen", []))

    def test_wrong_ladder_stays_closed_through_the_route(self):
        self._take_job("office_assistant", "meridian")
        seen = []
        for year in range(4):
            offers, _ = self.age_year("y%d" % year)
            seen.extend(o[1] for o in offers if o[0] == "wrk")
        self.assertTrue(seen)
        for eid in seen:
            self.assertIn(eid, self.eligible_ids("m3_work", self.env()),
                          "%s offered to an office worker — the route "
                          "ignored the ladder gate" % eid)
        # the trades-only records must never appear for an office worker
        self.assertNotIn("wrk_wrong_cable", seen)
        self.assertNotIn("wrk_safety_inspection", seen)
        # cadence: each record at most once per life
        self.assertEqual(len(seen), len(set(seen)), seen)

    def test_study_gates_hold_through_the_route(self):
        # unenrolled + unemployed: the families are silently skipped
        for year in range(3):
            offers, _ = self.age_year("s%d" % year)
            self.assertFalse([o for o in offers if o[0] == "stu"],
                             "stu_ offered without an active enrollment")
        # enrolled in the care diploma (2-year program): stu_ served,
        # program gates hold for the whole enrollment
        self._enroll("care_diploma")
        eligible = self.eligible_ids("m3_study", self.env())
        self.assertTrue(eligible)
        seen = []
        for year in range(2):
            offers, _ = self.age_year("e%d" % year)
            seen.extend(o[1] for o in offers if o[0] == "stu")
        self.assertTrue(seen, "an enrolled player must be served stu_ "
                              "records")
        for eid in seen:
            self.assertIn(eid, eligible,
                          "%s offered against the wrong program gate" % eid)
        self.assertNotIn("stu_class_debate", seen)   # business_bachelors
        self.assertNotIn("stu_lab_spill", seen)      # app_electrical only
        self.assertEqual(len(seen), len(set(seen)), seen)

    def test_working_student_sees_both_families_in_one_year(self):
        # the paid apprenticeship holds one of each active source
        env = self._enroll("app_electrical")
        from src.game.features.careers import core as careers_core
        self.assertIsNotNone(careers_core.active_assignment(
            env, env["player"].person_id))
        offers, _ = self.age_year("w1")
        families = {o[0] for o in offers}
        self.assertIn("wrk", families,
                      "apprentice must be served a wrk_ record")
        self.assertIn("stu", families,
                      "apprentice must be served a stu_ record")

    def test_money_choice_posts_exactly_one_leg(self):
        self._take_job("office_assistant", "meridian")
        # Force the money branch through persisted cadence, not a mocked
        # sampler. An ordinary four-year sample can contain no cash choice.
        eid = "wrk_overtime_weekend"
        env = self.env()
        env["wrk_stu_seen"] = [
            e["id"] for e in events.load_event_files()
            if e["id"].startswith(("wrk_", "stu_")) and e["id"] != eid]
        self.svc._save(env)
        offers, resolved = self.age_year("money")
        self.assertIn(eid, [o[1] for o in offers])
        ev = next(e for e in content_events("m3_work") if e["id"] == eid)
        option_id, legs_before = resolved[eid]
        money = option_by_id(ev, option_id)["effects"]["money"]
        self.assertEqual(money, 120)
        new = [l for l in self.env()["ledger"][legs_before:]
               if eid in str(l.get("source_key", ""))]
        self.assertEqual(len(new), 1, new)
        self.assertEqual(new[0]["amount"], money)

    def test_offered_pending_survives_reload_and_resolves_once(self):
        self._take_job("office_assistant", "meridian")
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "rl-age",
                              "age_up", {})
        while st.get("pending_decision"):
            d = st["pending_decision"]
            eid = d.get("event_id") or ""
            if eid.startswith("wrk_"):
                break
            choice = self._pick_choice(d)
            st = self.svc.command(self.gid, st["revision"], "rl-skip",
                                  "resolve_decision",
                                  {"decision_id": d["id"],
                                   "option_id": choice["id"],
                                   "inputs": self._inputs_for(choice)})
        self.assertTrue(st.get("pending_decision"),
                        "expected a wrk_ offer this year")
        d = st["pending_decision"]
        eid = d["event_id"]
        legs_before = len(self.env()["ledger"])
        # fresh service on the same save dir: the stored offer is there
        svc2 = GameService(self.dir)
        st2 = svc2.get_state(self.gid)
        self.assertIsNotNone(st2.get("pending_decision"))
        self.assertEqual(st2["pending_decision"]["id"], d["id"])
        env2 = svc2._load(self.gid)
        choice = self._pick_choice(d)
        st2 = svc2.command(self.gid, st2["revision"], "rl-resolve",
                           "resolve_decision",
                           {"decision_id": d["id"],
                            "option_id": choice["id"], "inputs": {}})
        env2 = svc2._load(self.gid)
        ev = next(e for e in content_events("m3_work") if e["id"] == eid)
        expected = option_by_id(ev, choice["id"])["outcome_text"]
        texts = [j["text"] for j in env2["journal"]]
        self.assertIn(expected, texts)
        new = [l for l in env2["ledger"][legs_before:]
               if eid in str(l.get("source_key", ""))]
        self.assertLessEqual(len(new), 1, "resolve must post at most once")

    def test_undo_rolls_seen_back_and_reoffers_the_same_record(self):
        self._take_job("office_assistant", "meridian")
        offers, _ = self.age_year("u1")
        wrk = [o for o in offers if o[0] == "wrk"]
        self.assertTrue(wrk)
        first_id = wrk[0][1]
        st = self.svc.get_state(self.gid)
        self.assertTrue(st.get("can_undo", {}).get("enabled"))
        st = self.svc.command(self.gid, st["revision"], "u-undo",
                              "undo_year", {})
        # after undo the player is back at the earlier age and the
        # seen-cadence rolled back with the snapshot: re-aging offers the
        # SAME record again (Amendment 4: undo/replay-safe cadence)
        offers2, _ = self.age_year("u2")
        wrk2 = [o for o in offers2 if o[0] == "wrk"]
        self.assertTrue(wrk2, "re-aged year must serve a wrk_ record again")
        self.assertEqual(wrk2[0][1], first_id,
                         "undo must roll the seen-cadence back so the "
                         "same record is re-offered")


if __name__ == "__main__":
    unittest.main()
