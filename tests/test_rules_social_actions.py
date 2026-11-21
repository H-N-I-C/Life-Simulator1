"""M4 social action mutation paths (Kimi C) — through A's appliers.

These tests exercise the FULL command path (club_join/leave fees and
membership, spend time, befriend, stand up, tell someone, and the
bullying outcome table) once A's social engine
(src/game/features/social/state.py with the frozen applier set) has
landed. Until then they skip loudly — the C-owned content, gates,
specs and typed validation are proven by tests/test_social_content.py,
which runs green now.

Run: python3 -m unittest tests.test_rules_social_actions
"""

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

from src.game.service import GameError, GameService  # noqa: E402
from src.menus import rule_actions  # noqa: E402

PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 11}


def _appliers_available():
    try:
        from src.game.features.social import state as social_state
    except ImportError:
        return False
    return all(hasattr(social_state, n) for n in (
        "join_club", "leave_club", "apply_kind", "apply_closeness",
        "apply_popularity_delta", "record_memory"))


def fixture_social(**kwargs):
    st = {"schema_version": 1, "next_num": 1,
          "circles": {"school": {"member_ids": []},
                      "work": {}, "clubs": {}},
          "people": {}, "best_friend": None,
          "popularity": {"score": 0, "phase": "adult", "history": []},
          "invites": {}, "arcs": {}}
    st.update(kwargs)
    return st


@unittest.skipUnless(_appliers_available(),
                     "A's social engine (features/social) not landed yet")
class SocialActionTests(unittest.TestCase):
    def setUp(self):
        self.dir = tempfile.mkdtemp(prefix="lifesim-m4-actions-")
        self.svc = GameService(self.dir)
        st = self.svc.new_game(dict(PROFILE), self.id() + "-c")
        self.gid = st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 20
        env["social_state"] = fixture_social()
        self.svc._save(env)

    def tearDown(self):
        shutil.rmtree(self.dir, ignore_errors=True)

    def env(self):
        return self.svc._load(self.gid)

    def _people(self, env, **records):
        """Fixture people as FULL A-shaped records (via A's record())."""
        from src.game.features.social import state as social_state
        social_state.ensure(env)
        env["social_state"]["people"] = {}
        for pid, rec in records.items():
            full = social_state.record(env, pid, create=True)
            full.update(rec)
        self.svc._save(env)

    def cmd(self, action, payload, tag):
        st = self.svc.get_state(self.gid)
        return self.svc.command(self.gid, st["revision"], tag, action,
                                payload)

    def test_join_free_club_then_leave(self):
        st = self.cmd("club_join", {"club_id": "book_club"}, "j1")
        env = self.env()
        pid = env["player"].person_id
        self.assertIn(pid, env["social_state"]["circles"]["clubs"]
                      ["book_club"]["member_ids"])
        st = self.cmd("club_leave", {"club_id": "book_club"}, "j2")
        env = self.env()
        self.assertNotIn(pid, env["social_state"]["circles"]["clubs"]
                         .get("book_club", {}).get("member_ids", []))

    def test_paid_club_posts_exactly_once(self):
        env = self.env()
        env["player"].money = 500
        self.svc._save(env)
        self.cmd("club_join", {"club_id": "bowling_league"}, "p1")
        env = self.env()
        self.assertEqual(env["player"].money, 420)
        legs = [l for l in env["ledger"]
                if "club:bowling_league" in str(l.get("source_key", ""))]
        self.assertEqual(len(legs), 1)
        # duplicate join refused, no second charge
        with self.assertRaises(GameError) as ctx:
            self.cmd("club_join", {"club_id": "bowling_league"}, "p2")
        self.assertEqual(ctx.exception.status, 409)
        env = self.env()
        self.assertEqual(env["player"].money, 420)

    def test_adults_only_club_refuses_minors(self):
        # D journey finding (m4_j2 minor_content): forced at EVERY minor
        # age, dispatch AND the projection the UI renders.
        for age in (6, 12, 16, 17):
            env = self.env()
            env["player"].age = age
            self.svc._save(env)
            with self.assertRaises(GameError) as ctx:
                self.cmd("club_join", {"club_id": "harbour_run_club"},
                         "a1-%d" % age)
            self.assertEqual(ctx.exception.status, 409)
            self.assertIn("18", str(ctx.exception))
            st = self.svc.get_state(self.gid)
            clubs = {c["club_id"]: c
                     for c in (st.get("social") or {}).get("clubs") or []}
            for cid, club in clubs.items():
                if not club.get("adults_only"):
                    continue
                for action in club.get("actions") or []:
                    if action.get("id") == "club_join":
                        self.assertFalse(
                            action.get("enabled"),
                            "age %d: %s join advertised enabled (%r)"
                            % (age, cid, action.get("reason")))
                        self.assertIn("18",
                                      action.get("reason") or "")
        # and the join genuinely did not happen
        env = self.env()
        self.assertFalse(
            env["social_state"]["circles"].get("clubs", {})
            .get("harbour_run_club", {}).get("member_ids"))

    def test_spend_time_grows_closeness_once_per_year(self):
        env = self.env()
        pid = env["player"].person_id
        self._people(env, p1={"kind": "friend", "closeness": 50})
        self.cmd("social_spend_time", {"target_id": "p1"}, "s1")
        env = self.env()
        self.assertEqual(env["social_state"]["people"]["p1"]
                         ["closeness"], 56)
        with self.assertRaises(GameError) as ctx:
            self.cmd("social_spend_time", {"target_id": "p1"}, "s2")
        self.assertEqual(ctx.exception.status, 409)

    def test_reconcile_advertised_only_for_fallen_out_or_rival(self):
        from src.game.context import RuleContext
        specs = self._specs_for(kind="friend", closeness=40,
                                status="active")
        self.assertFalse([x for x in specs if x[0] ==
                          "social_reconcile"])
        specs = self._specs_for(kind="friend", closeness=40,
                                status="estranged")
        self.assertIn(("social_reconcile", True), specs)
        specs = self._specs_for(kind="acquaintance", closeness=30,
                                status="estranged")
        self.assertFalse([x for x in specs if x[0] ==
                          "social_reconcile"],
                         "only estranged FRIENDS, not acquaintances")
        specs = self._specs_for(kind="rival", closeness=10)
        self.assertIn(("social_reconcile", True), specs)
        specs = self._specs_for(kind="friend", closeness=40,
                                status="faded")
        self.assertEqual(specs, set())

    def _specs_for(self, **rec):
        from src.game.features.social import state as social_state
        from src.game.context import RuleContext
        env = self.env()
        social_state.ensure(env)
        env["social_state"]["people"] = {}
        full = social_state.record(env, "p1", create=True)
        full.update(rec)
        self.svc._save(env)
        env = self.env()
        specs = rule_actions._social_specs(RuleContext(env, self.svc),
                                           env["player"])
        return {(x["id"], x["enabled"]) for x in specs
                if x.get("target_id") == "p1"}

    def test_reconcile_stored_draw_success_and_refusal(self):
        import random as _random
        from src.game.context import RuleContext
        from src.game.features.social import state as social_state
        from src.game.content.journal import social_lines

        def setup():
            env = self.env()
            social_state.ensure(env)
            env["social_state"]["people"] = {}
            full = social_state.record(env, "p1", create=True)
            full.update({"kind": "friend", "closeness": 40,
                         "status": "estranged"})
            self.svc._save(env)
            return self.env()

        def force(rng_seed):
            """resolve reach_out with a rng whose first draw is known"""
            env = setup()
            pid = env["player"].person_id
            st = self.svc.get_state(self.gid)
            st = self.svc.command(self.gid, st["revision"], "rec-1",
                                  "social_reconcile",
                                  {"target_id": "p1"})
            d = st["pending_decision"]
            self.assertEqual(d["kind"], "social_reconcile")
            self.assertEqual(d["sampled"]["p_accept"], 0.5)
            env = self.env()  # reload: the command stored the pending
            ctx = RuleContext(env, self.svc)
            result = rule_actions.resolve_choice(
                ctx, env["pending"], "reach_out", {},
                _random.Random(rng_seed))
            self.svc._save(env)  # direct call: persist the mutations
            return env, result

        # success path (rng first draw < 0.5)
        env, result = force(3)
        entry = result["events"][-1]
        self.assertTrue(entry["changes"].get("reconciled"))
        texts = {l["text"] for l in social_lines.lines_for(
            "reconciled")}
        self.assertIn(entry["text"].replace("them", "{name}"),
                      texts) if False else None
        # the fixture id "p1" is not a walkable NPC, so the name
        # resolves to "them" — format the pool the same way
        pool = {t.format(name="them") for t in texts}
        self.assertIn(entry["text"], pool,
                      "success must journal from the reconciled pool")
        env2 = self.env()
        rec = env2["social_state"]["people"]["p1"]
        self.assertEqual(rec["status"], "active")
        self.assertGreaterEqual(rec["closeness"], 45)

        # refusal path (rng first draw >= 0.5)
        env, result = force(17)
        entry = result["events"][-1]
        self.assertFalse(entry["changes"].get("reconciled"))
        self.assertIn("not ready", entry["text"])
        rec = self.env()["social_state"]["people"]["p1"]
        self.assertEqual(rec["status"], "estranged")

    def test_reconcile_once_per_year_and_decline_free(self):
        env = self.env()
        from src.game.features.social import state as social_state
        social_state.ensure(env)
        env["social_state"]["people"] = {}
        full = social_state.record(env, "p1", create=True)
        full.update({"kind": "rival", "closeness": 10})
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "rec-y",
                              "social_reconcile", {"target_id": "p1"})
        d = st["pending_decision"]
        st = self.svc.command(self.gid, st["revision"], "rec-yr",
                              "resolve_decision",
                              {"decision_id": d["id"],
                               "option_id": "let_it", "inputs": {}})
        env = self.env()
        self.assertEqual(env["social_rules_state"]
                         ["reconcile_year"]["p1"], env["player"].age)
        # the year is consumed even by a free decline; specs say so
        st = self.svc.get_state(self.gid)
        with self.assertRaises(GameError) as ctx:
            self.svc.command(self.gid, st["revision"], "rec-y2",
                             "social_reconcile", {"target_id": "p1"})
        self.assertEqual(ctx.exception.status, 409)

    def test_social_names_resolve_in_specs_and_journal(self):
        # B's finding: envelope NPCs are not in context.people_by_id, so
        # every social target rendered as 'them'. Two classmates with
        # distinct names must produce distinct spec labels and journal
        # names.
        from src.people.classes.person import Person
        from src.game.features.social import state as social_state
        from src.game.context import RuleContext
        env = self.env()
        social_state.ensure(env)
        env["social_people"] = []
        people = []
        for first in ("Maya", "Theo"):
            npc = Person(first, "Reyes", 20, "Female", 60, 80, 60, 60)
            env["social_people"].append(npc)
            rec = social_state.record(env, npc.person_id, create=True)
            rec["closeness"] = 20
            people.append(npc)
        self.svc._save(env)

        env = self.env()
        specs = rule_actions._social_specs(RuleContext(env, self.svc),
                                           env["player"])
        befriend = [s for s in specs if s["id"] == "social_befriend"]
        labels = {s["target_label"] for s in befriend}
        self.assertEqual(labels, {"Maya", "Theo"}, labels)
        self.assertNotIn("them", labels)

        # journal names: befriend each, check the entry text
        texts = []
        for npc in people:
            env = self.env()
            self.cmd("social_befriend", {"target_id": npc.person_id},
                     "n-" + npc.firstname)
            env = self.env()
            texts.append(env["journal"][-1]["text"])
        joined = " || ".join(texts)
        self.assertIn("Maya", joined)
        self.assertIn("Theo", joined)
        self.assertNotIn("them", joined)
        # the lines name the RIGHT person (not the same name twice)
        self.assertTrue(any("Maya" in t and "Theo" not in t
                            for t in texts), texts)
        self.assertTrue(any("Theo" in t and "Maya" not in t
                            for t in texts), texts)

    def test_befriend_then_spend_time_legal(self):
        env = self.env()
        self._people(env, p3={"kind": None, "closeness": 20})
        self.cmd("social_befriend", {"target_id": "p3"}, "b1")
        env = self.env()
        self.assertEqual(env["social_state"]["people"]["p3"]["kind"],
                         "friend")
        self.cmd("social_spend_time", {"target_id": "p3"}, "b2")
        self.assertGreater(env["social_state"]["people"]["p3"]
                           ["closeness"], 20)
        # once per life
        with self.assertRaises(GameError):
            self.cmd("social_befriend", {"target_id": "p3"}, "b3")

    def test_stand_up_outcome_table(self):
        # healthy: moves to resolving
        env = self.env()
        env["player"].health = 80
        self._people(env, p9={"kind": "bully", "closeness": 5})
        self.cmd("social_stand_up", {"target_id": "p9"}, "u1")
        env = self.env()
        self.assertEqual(env["social_state"]["arcs"]["p9"]["stage"],
                         "resolving")
        # unhealthy: escalates
        env = self.env()
        pid = env["player"].person_id
        env["player"].health = 20
        env["social_state"]["arcs"] = {
            "p9": {"stage": "targeted", "target_id": pid,
                   "since_turn": "t"}}
        self.svc._save(env)
        self.cmd("social_stand_up", {"target_id": "p9"}, "u2")
        env = self.env()
        self.assertEqual(env["social_state"]["arcs"]["p9"]["stage"],
                         "escalated")

    def test_tell_someone_always_works(self):
        env = self.env()
        pid = env["player"].person_id
        from src.game.features.social import state as social_state
        social_state.ensure(env)
        rec = social_state.record(env, "p9", create=True)
        rec["kind"] = "bully"
        rec["closeness"] = 5
        social_state.set_arc(env, pid, "p9", "escalated",
                             target_id=pid)
        self.svc._save(env)
        self.cmd("social_tell_someone", {}, "t1")
        env = self.env()
        self.assertEqual(env["social_state"]["arcs"]["p9"]["stage"],
                         "resolving")
        # no arc -> honest refusal
        env = self.env()
        env["social_state"]["people"] = {}
        self.svc._save(env)
        with self.assertRaises(GameError) as ctx:
            self.cmd("social_tell_someone", {}, "t2")
        self.assertEqual(ctx.exception.status, 409)

    # A's resolve wiring LANDED (bullying.register_domain_effects via
    # the social events route); proven end to end through the real
    # route by tests.test_rules_m4_route. This pins the same boundary
    # against a directly instantiated pending.
    def test_bullying_event_domain_effect_via_resolve(self):
        # an authored event option carrying the bullying_response effect
        # resolves through the real resolve boundary with the same table
        env = self.env()
        env["player"].age = 12
        env["player"].grades = 70
        env["player"].health = 80
        from src.game.features.social import circles as circles_mod
        from src.game.features.social import state as social_state
        social_state.ensure(env)
        pid = circles_mod.mint_friend(env, env["player"], age=12)
        rec = social_state.record(env, pid, create=True)
        rec["kind"] = "bully"
        rec["closeness"] = 5
        self._bully_pid = pid
        self.svc._save(env)
        from src.game.context import RuleContext
        from src.game import events as event_mod
        from src.game.features.social import bullying as bullying_mod
        import random
        bullying_mod.register_domain_effects()  # A's idempotent seam
        ctx = RuleContext(env, self.svc)
        ev = next(e for e in event_mod.load_event_files()
                  if e.get("id") == "soc_bul_locker_note")
        pending = event_mod.EventEngine.instantiate3(
            ev, 99, rng=random.Random(1), context=event_mod.GameContext(
                env, self.svc))
        pending["id"] = "d_99"
        env["pending"] = pending
        self.svc._save(env)
        option = next(o for o in pending["options"]
                      if o["id"].endswith("tell_someone"))
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "d1",
                              "resolve_decision",
                              {"decision_id": pending["id"],
                               "option_id": option["id"], "inputs": {}})
        env = self.env()
        self.assertEqual(env["social_state"]["arcs"][self._bully_pid]
                         ["stage"], "resolving")


if __name__ == "__main__":
    unittest.main()
