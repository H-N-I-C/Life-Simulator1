"""M4 social content + gates (Kimi C).

Data validity (clubs.json/bullying.json per the frozen loaders), the v3
schema additions (six condition leaves + min_stat + the social_invite
record fields) through the REAL loader and the shared v3 evaluator, the
gate matrix for all six soc_ families against fixture envelopes in the
frozen env["social_state"] shape, the advertised club/person specs
(B's projection dialect), and the typed command boundary for the six
C-owned actions. Full mutation paths (A's appliers) live in
tests/test_rules_social_actions.py and activate when A's engine lands.

Run: python3 -m unittest tests.test_social_content
"""

import json
import os
import random
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
from src.game.content import social as social_content  # noqa: E402
from src.game.context import RuleContext  # noqa: E402
from src.game.service import GameError, GameService  # noqa: E402
from src.menus import rule_actions  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}

FILES = ("m4_school", "m4_bullying", "m4_work", "m4_adult", "m4_invites")


def content_events(name):
    with open(os.path.join(ROOT, "src", "game", "content", "events",
                           "%s.json" % name), encoding="utf-8") as f:
        return json.load(f)["events"]


def all_soc_events():
    out = []
    for name in FILES:
        out.extend(content_events(name))
    return out


def fixture_social(**kwargs):
    """The frozen env['social_state'] shape (A owns the real one)."""
    st = {"schema_version": 1, "next_num": 1,
          "circles": {"school": {"member_ids": []},
                      "work": {}, "clubs": {}},
          "people": {}, "best_friend": None,
          "popularity": {"score": 0, "phase": "adult", "history": []},
          "invites": {}}
    st.update(kwargs)
    return st


class DataValidityTests(unittest.TestCase):
    def test_clubs_data_valid_and_card_fields(self):
        raw = social_content.load_clubs(reload=True)
        self.assertGreaterEqual(len(raw["clubs"]), 8)
        for club in raw["clubs"]:
            for key in ("id", "label", "category", "meets_label"):
                self.assertTrue(isinstance(club.get(key), str) and club[key])
            self.assertIsInstance(club["cost_per_year"], int)
            self.assertGreaterEqual(club["cost_per_year"], 0)
            self.assertIsInstance(club["adults_only"], bool)

    def test_club_lookup_unknown_is_none(self):
        self.assertIsNone(social_content.club("nope"))

    def test_bullying_table_frozen(self):
        raw = social_content.load_bullying(reload=True)
        self.assertEqual(raw["stages"],
                         ["targeted", "escalated", "resolving", "ended"])
        self.assertEqual(set(raw["responses"]),
                         {"tell_someone", "stand_up", "avoid", "befriend"})
        # tell_someone always works: no rng gate, no failure branch
        self.assertNotIn("fail_to_stage", raw["responses"]["tell_someone"])
        self.assertNotIn("health_min", raw["responses"]["tell_someone"])

    def test_all_soc_events_load_and_validate(self):
        evs = all_soc_events()
        self.assertGreaterEqual(len(evs), 30)
        for ev in evs:
            events.validate_event3(ev)
        ids = [e["id"] for e in evs]
        self.assertEqual(len(ids), len(set(ids)))

    def test_invite_records_carry_venue_fields(self):
        invites = [e for e in all_soc_events()
                   if e["interaction"]["kind"] == "social_invite"]
        self.assertGreaterEqual(len(invites), 5)
        for ev in invites:
            self.assertTrue(ev.get("venue_label"))
            self.assertIsInstance(ev.get("adults_only"), bool)
            if ev["adults_only"]:
                self.assertGreaterEqual(ev["conditions"].get("min_age", 0),
                                        18)
        # a missing venue fails load loudly
        bad = json.loads(json.dumps(invites[0]))
        del bad["venue_label"]
        with self.assertRaises(events.EventValidationError):
            events.validate_event3(bad)

    def test_every_event_has_a_free_option(self):
        for ev in all_soc_events():
            free = [c for c in ev["choices"]
                    if not (c.get("cost") or {}).get("money")]
            self.assertTrue(free, ev["id"])


class GateMatrixTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m4-gates-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-c")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 20
        self.svc._save(env)

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def env(self):
        return self.svc._load(self.gid)

    def env_with(self, social_state, age=None):
        env = self.svc._load(self.gid)
        if age is not None:
            env["player"].age = age
        if social_state is not None:
            env["social_state"] = social_state
        self.svc._save(env)
        return self.svc._load(self.gid)

    def cond_ok(self, ev, env, extra_bindings=None):
        ctx = RuleContext(env, self.svc)
        pid = env["player"].person_id
        bindings = {"actor": pid}
        bindings.update(extra_bindings or {})
        return events.evaluate_v3_conditions(
            ev.get("conditions") or {}, ctx, bindings, pid)

    def _ids(self, name):
        return {e["id"] for e in content_events(name)}

    def test_school_family_needs_the_classmate_circle(self):
        empty = self.env_with(fixture_social(), age=12)
        me = self.env()["player"].person_id
        full = self.env_with(fixture_social(
            circles={"school": {"member_ids": [me, "p1"]}, "work": {},
                     "clubs": {}},
            people={"p1": {"kind": "friend", "closeness": 70}}), age=12)
        for ev in content_events("m4_school"):
            cond = ev.get("conditions") or {}
            if not (cond.get("min_age", 0) <= 12
                    <= cond.get("max_age", 200)):
                continue  # age band excludes 12; family gating below
            self.assertFalse(self.cond_ok(ev, empty), ev["id"])
            self.assertTrue(self.cond_ok(ev, full), ev["id"])

    def test_bullying_arc_gates(self):
        targeted = self.env_with(fixture_social(people={
            "p9": {"kind": "bully", "closeness": 5}}), age=12)
        ended = self.env_with(fixture_social(people={
            "p9": {"kind": "bully", "closeness": 5, "arc": "ended"}}),
            age=12)
        target_evs = [e for e in content_events("m4_bullying")
                      if e["id"].startswith("soc_bul_")]
        resolving = self.env_with(fixture_social(people={
            "p9": {"kind": "bully", "closeness": 5, "arc": "resolving"}}),
            age=12)
        for ev in target_evs:
            if ev["id"] == "soc_bul_cooldown":
                self.assertFalse(self.cond_ok(ev, targeted,
                                              {"bully": "p9"}))
                self.assertTrue(self.cond_ok(ev, resolving,
                                             {"bully": "p9"}))
            else:
                self.assertTrue(self.cond_ok(ev, targeted,
                                             {"bully": "p9"}), ev["id"])
            self.assertFalse(self.cond_ok(ev, ended,
                                          {"bully": "p9"}), ev["id"])

    def test_popularity_and_club_and_friend_gates(self):
        low = self.env_with(fixture_social())
        high = self.env_with(fixture_social(
            popularity={"score": 65, "phase": "adult", "history": []}))
        high["player"].money = 5000
        work_ev = next(e for e in content_events("m4_work")
                       if e["id"] == "soc_wrk_lunch_invite")
        self.assertFalse(self.cond_ok(work_ev, low))   # no assignment
        # needs the M3 active_assignment: give a real named assignment
        from src.game.features.skills import state as skills_state
        skills_state.values(high["player"].__dict__ and high,
                            high["player"].person_id)
        from src.game.features.careers import core as careers_core
        careers_core.ensure(high)["assignments"][
            high["player"].person_id] = {
            "assignment_id": "ca_x", "role_id": "office_assistant",
            "employer_id": "meridian", "status": "active"}
        self.assertTrue(self.cond_ok(work_ev, high))

        adult_ev = next(e for e in content_events("m4_adult")
                        if e["id"] == "soc_adt_call_friend")
        self.assertFalse(self.cond_ok(adult_ev, low))
        friend_env = self.env_with(fixture_social(people={
            "p2": {"kind": "friend", "closeness": 60}}))
        self.assertTrue(self.cond_ok(adult_ev, friend_env,
                                     {"friend": "p2"}))

        club_ev = next(e for e in content_events("m4_invites")
                       if e["id"] == "soc_inv_book_club")
        self.assertFalse(self.cond_ok(club_ev, low))
        club_env = self.env_with(fixture_social(
            circles={"school": {"member_ids": []}, "work": {},
                     "clubs": {"book_club": {"member_ids":
                                             [self.svc._load(self.gid)
                                              ["player"].person_id]}}}))
        self.assertTrue(self.cond_ok(club_ev, club_env))

    def test_predicates_bind_from_the_envelope(self):
        env = self.env_with(fixture_social(
            circles={"school": {"member_ids": ["p1", "p2"]}, "work": {},
                     "clubs": {}},
            people={"p1": {"kind": "friend", "closeness": 80},
                    "p2": {"kind": "bully", "closeness": 5}}))
        ctx = RuleContext(env, self.svc)
        pid = env["player"].person_id
        self.assertEqual(
            events.PREDICATES["social_classmate_of_actor"](pid, ctx), "p1")
        self.assertEqual(
            events.PREDICATES["social_friend_of_actor"](pid, ctx), "p1")
        self.assertEqual(
            events.PREDICATES["school_bully_of_actor"](pid, ctx), "p2")
        self.assertIsNone(
            events.PREDICATES["social_coworker_of_actor"](pid, ctx))

    def test_shared_dispatch_draws_only_when_gated_open(self):
        loaded = [dict(e, schema_version=3) for e in all_soc_events()]
        engine = events.EventEngine(loaded)
        rng = random.Random(5)
        env = self.env_with(fixture_social())
        ctx = RuleContext(env, self.svc)
        pid = env["player"].person_id
        ev, _ = engine.sample_v3(rng, 20, set(), ctx, prefixes=("soc_",),
                                 actor_id=pid)
        self.assertIsNone(ev, "no social state -> nothing served")
        me2 = self.env()["player"].person_id
        env2 = self.env_with(fixture_social(
            circles={"school": {"member_ids": [me2, "p1"]}, "work": {},
                     "clubs": {}},
            people={"p1": {"kind": "friend", "closeness": 70}}), age=12)
        ctx2 = RuleContext(env2, self.svc)
        drawn = set()
        for _ in range(6):
            ev, _ = engine.sample_v3(rng, 12, drawn, ctx2,
                                     prefixes=("soc_sch_",), actor_id=pid)
            if ev is None:
                break
            drawn.add(ev["id"])
        self.assertTrue(drawn, "school circle open -> school events served")


class SpecAndCommandTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m4-cmd-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-c")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 20
        self.svc._save(env)

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def env(self):
        return self.svc._load(self.gid)

    def _social_env(self, **kwargs):
        env = self.env()
        env["social_state"] = fixture_social(**kwargs)
        self.svc._save(env)
        return self.env()

    def test_action_ids_admitted(self):
        for aid in ("club_join", "club_leave", "social_spend_time",
                    "social_befriend", "social_stand_up",
                    "social_tell_someone"):
            self.assertIn(aid, rule_actions.ACTION_IDS)
            self.assertIn(aid, rule_actions._SOCIAL_SELF_ACTIONS)

    def test_club_specs_match_b_card_dialect(self):
        env = self.svc._load(self.gid)
        env["player"].age = 12
        env["player"].grades = 70  # enrolled: school-locus clubs apply
        env["social_state"] = fixture_social()
        self.svc._save(env)
        env = self.env()
        specs = rule_actions.club_specs(env, env["player"])
        self.assertEqual(len(specs), len(social_content.load_clubs()
                                         ["clubs"]))
        by_id = {s["club_id"]: s for s in specs}
        join = by_id["chess_club"]
        self.assertEqual(join["id"], "club_join")
        self.assertEqual(join["known_costs"], [])
        self.assertTrue(join["enabled"], join["reason"])
        adults = by_id["harbour_run_club"]
        self.assertTrue(adults["known_costs"])
        self.assertFalse(adults["enabled"])
        self.assertIn("18", adults["reason"])
        # a member sees leave instead
        pid = env["player"].person_id
        env2 = self._social_env(circles={
            "school": {"member_ids": []}, "work": {},
            "clubs": {"chess_club": {"member_ids": [pid]}}})
        by_id2 = {s["club_id"]: s for s in
                  rule_actions.club_specs(env2, env2["player"])}
        self.assertEqual(by_id2["chess_club"]["id"], "club_leave")

    def test_person_specs_gated_single_source(self):
        pid = self.env()["player"].person_id
        env = self._social_env(people={
            "p1": {"kind": "friend", "closeness": 70},
            "p2": {"kind": "bully", "closeness": 5},
            "p3": {"kind": None, "closeness": 20}})
        specs = rule_actions._social_specs(RuleContext(env, self.svc),
                                           env["player"])
        ids = {(s["id"], s.get("target_id")) for s in specs}
        self.assertIn(("social_spend_time", "p1"), ids)
        self.assertIn(("social_stand_up", "p2"), ids)
        self.assertIn(("social_tell_someone", None), ids)
        self.assertIn(("social_befriend", "p3"), ids)
        # a faded contact is not advertised
        env2 = self._social_env(people={
            "p1": {"kind": "friend", "closeness": 70,
                   "status": "faded"}})
        specs2 = rule_actions._social_specs(RuleContext(env2, self.svc),
                                            env2["player"])
        self.assertFalse([s for s in specs2
                          if s.get("target_id") == "p1"])

    def test_advertised_actions_per_kind(self):
        """t7 review: a classmate/acquaintance must see Spend time too,
        not only Befriend. Lists the advertised person-overlay action
        ids per social kind."""
        from src.game.features.social import state as social_state

        def advertised(**rec):
            env = self.env()
            social_state.ensure(env)
            env["social_state"]["people"] = {}
            full = social_state.record(env, "p1", create=True)
            full.update(rec)
            self.svc._save(env)
            env = self.env()
            specs = rule_actions._social_specs(
                RuleContext(env, self.svc), env["player"])
            return {(s["id"], s["enabled"]) for s in specs
                    if s.get("target_id") == "p1"}

        none_ads = advertised(kind=None, closeness=5)
        self.assertIn(("social_befriend", True), none_ads)
        self.assertFalse([a for a in none_ads if a[0] ==
                          "social_spend_time"],
                         "fresh classmate: befriend first")

        acq_ads = advertised(kind="acquaintance", closeness=30)
        self.assertIn(("social_befriend", True), acq_ads)
        self.assertIn(("social_spend_time", True), acq_ads,
                      "acquaintance must be offered Spend time: %r"
                      % acq_ads)

        friend_ads = advertised(kind="friend", closeness=60)
        self.assertIn(("social_spend_time", True), friend_ads)
        self.assertFalse([a for a in friend_ads
                          if a[0] == "social_befriend"],
                         "friends are past befriending")

        bf_ads = advertised(kind="best_friend", closeness=90)
        self.assertIn(("social_spend_time", True), bf_ads)

        bully_ads = advertised(kind="bully", closeness=5)
        self.assertIn(("social_stand_up", True), bully_ads)
        self.assertFalse([a for a in bully_ads if a[0] ==
                          "social_spend_time"])

        faded_ads = advertised(kind="friend", closeness=40,
                               status="faded")
        self.assertEqual(faded_ads, set())

    def test_typed_command_validation_real_service(self):
        st = self.svc.get_state(self.gid)
        for payload in ({}, {"club_id": 5}, {"club_id": []},
                        {"club_id": "nope"}):
            with self.assertRaises(GameError) as ctx:
                self.svc.command(self.gid, st["revision"], "v-%s" % len(str(payload)),
                                 "club_join", payload)
            self.assertEqual(ctx.exception.status, 400, payload)
        # no social circles in this save yet -> honest typed refusal
        with self.assertRaises(GameError) as ctx:
            self.svc.command(self.gid, st["revision"], "v-ok",
                             "club_join", {"club_id": "chess_club"})
        self.assertEqual(ctx.exception.status, 409)
        # person actions: missing/unknown target typed 400/404
        with self.assertRaises(GameError) as ctx:
            self.svc.command(self.gid, st["revision"], "v-t0",
                             "social_spend_time", {})
        self.assertEqual(ctx.exception.status, 400)
        with self.assertRaises(GameError) as ctx:
            self.svc.command(self.gid, st["revision"], "v-t1",
                             "social_spend_time",
                             {"target_id": "p-unknown"})
        self.assertIn(ctx.exception.status, (404, 409))


if __name__ == "__main__":
    unittest.main()
