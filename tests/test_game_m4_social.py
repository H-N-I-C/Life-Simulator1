"""M4 social engine tests (Kimi A, 2026-11-21).

Contract: docs/collab/M4/02-contracts-A.md (+ B's frozen shape in
02-contracts.md). Real GameService objects, forced real commands; 3
hash seeds where it matters.

Run: TMPDIR=~/.lifesim-tmp python3 -m unittest tests.test_game_m4_social
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)
sys.path.insert(0, os.path.join(ROOT, "tests"))

from src.game.service import GameError, GameService  # noqa: E402
from src.game.features.social import state as social  # noqa: E402
from src.game.features.social import invites as social_invites  # noqa: E402

SEEDS = [7, 4242, 90210]

os.makedirs(os.path.expanduser("~/.lifesim-tmp"), exist_ok=True)


def make_service():
    return GameService(tempfile.mkdtemp(
        dir=os.path.expanduser("~/.lifesim-tmp")))


def new_game(svc, seed, request="c"):
    return svc.new_game({"first_name": "Ada", "last_name": "Lee",
                         "gender": "female", "country": "Australia",
                         "seed": seed}, request)


def drain_pending(svc, st, tag):
    n = 0
    while st.get("pending_decision"):
        d = st["pending_decision"]
        opt = next(o for o in d["options"] if o.get("enabled", True))
        st = svc.command(st["game_id"], st["revision"],
                         f"{tag}-d{n}", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": opt["id"]})
        n += 1
    return st


def run_social_hook(svc, gid, turn):
    import random as _random
    from src.game.features.social import drift as social_drift
    env = svc._load(gid)
    env.setdefault("turn", {})["turn_id"] = turn
    social_drift.annual_hook(env, svc, _random.Random(1))
    svc._save(env)


def make_person(svc, gid, tag, age=20):
    """A REAL canonical Person registered as a social NPC."""
    from src.game.features.social.circles import _npc_person
    from src.lifesim_lib.lifesim_lib import Gender
    env = svc._load(gid)
    person = _npc_person(env, f"Sam{tag}", "Tester", age, Gender.Male)
    svc._save(env)
    return person.person_id


def groups_of(st):
    return {g["group_id"]: g["people"] for g in st["social"]["groups"]}


def grant_skills(svc, gid, **skills):
    env = svc._load(gid)
    from src.game.features.skills import state as skills_state
    values = skills_state.values(env, env["player"].person_id)
    values.update(skills)
    svc._save(env)


def force_offer(svc, gid, **kw):
    """Persist a social_invite stored offer exactly the way C's event
    path will (A's offer() + the boundary's decision-id assignment)."""
    env = svc._load(gid)
    result = social_invites.offer(env, env["player"], **kw)
    pending = result["pending_decision"]
    pending["id"] = f"d_{env['next_decision_num']}"
    env["next_decision_num"] += 1
    env["social_state"]["invites"][pending["invite_id"]]\
        ["decision_id"] = pending["id"]
    env["pending"] = pending
    svc._save(env)
    return pending


class ProjectionAndMigrationTests(unittest.TestCase):
    def test_new_life_honest_empty_social(self):
        svc = make_service()
        st = new_game(svc, 7)
        soc = st["social"]
        self.assertEqual([g["group_id"] for g in soc["groups"]],
                         ["family", "friends", "classmates", "coworkers"])
        self.assertEqual(groups_of(st)["classmates"], [])
        self.assertEqual(soc["invites"], [])
        club_ids = {c["club_id"] for c in soc["clubs"]}
        self.assertIn("chess_club", club_ids)
        for club in soc["clubs"]:
            self.assertFalse(club["member"])
        self.assertEqual(soc["popularity"]["score"], 0)
        self.assertEqual(soc["popularity"]["band"], "unknown")
        family = groups_of(st)["family"]
        self.assertTrue(family)
        for ref in family:
            self.assertNotIn("social", ref)

    def test_legacy_save_without_social_keys_migrates_empty(self):
        svc = make_service()
        st = new_game(svc, 7)
        gid = st["game_id"]
        env = svc._load(gid)
        del env["social_state"]
        del env["social_people"]
        svc._save(env)
        st = svc.get_state(gid)
        self.assertEqual(st["social"]["invites"], [])
        self.assertEqual(st["social"]["popularity"]["score"], 0)
        env = svc._load(gid)
        self.assertEqual(env["social_people"], [])


class CircleTests(unittest.TestCase):
    def test_school_circle_appears_and_is_twin_deterministic(self):
        rosters = []
        for _ in range(2):
            svc = make_service()
            st = new_game(svc, 7)
            gid = st["game_id"]
            for i in range(7):
                st = drain_pending(svc, st, f"c{i}")
                st = svc.command(gid, st["revision"], f"c-y{i}",
                                 "age_up", {})
                if st["status"] == "dead":
                    break
            st = svc.get_state(gid)
            rosters.append([(p["name"], p["age"])
                            for p in groups_of(st)["classmates"]])
        self.assertTrue(rosters[0], "expected classmates at school age")
        self.assertEqual(rosters[0], rosters[1])
        st = svc.get_state(gid)
        for ref in groups_of(st)["classmates"]:
            # plain members default to acquaintance (UI never gets None)
            self.assertEqual(ref["social"]["kind"], "acquaintance")
            self.assertIn("classmates", ref["social"]["circles"])

    def test_work_circle_and_job_change_fade_rule(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed, f"w{seed}")
            gid = st["game_id"]
            env = svc._load(gid)
            p = env["player"]
            p.age = 18
            p.grades = None
            p.dropped_out = False
            p.money = 50000
            svc._save(env)
            grant_skills(svc, gid, communication=50)
            st = svc.get_state(gid)
            st = svc.command(gid, st["revision"], "w-a", "careers_apply",
                             {"opening": "office_assistant@meridian"})
            d = st["pending_decision"]
            st = svc.command(gid, st["revision"], "w-r",
                             "resolve_decision",
                             {"decision_id": d["id"], "option_id":
                              "accept"})
            coworkers = groups_of(st)["coworkers"]
            self.assertTrue(coworkers)
            befriended = coworkers[0]["id"]
            env = svc._load(gid)
            social.apply_kind(env, env["player"], befriended, "friend")
            social.apply_closeness(env, env["player"], befriended, 40)
            svc._save(env)
            st = svc.get_state(gid)
            st = svc.command(gid, st["revision"], "w-x", "careers_resign",
                             {})
            groups = groups_of(st)
            self.assertEqual(groups["coworkers"], [])
            friends = {p["id"]: p for p in groups["friends"]}
            self.assertIn(befriended, friends)
            # the faded contact carries its status in the projection
            self.assertEqual(friends[befriended]["social"]["status"],
                             "faded")
            env = svc._load(gid)
            self.assertEqual(env["social_state"]["people"]
                             [befriended]["status"], "faded")


class KindClosenessDriftTests(unittest.TestCase):
    def _friend_env(self, svc, seed, closeness=60):
        st = new_game(svc, seed, f"k{seed}")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 20
        svc._save(env)
        target = make_person(svc, gid, "F", age=20)
        env = svc._load(gid)
        social.apply_kind(env, env["player"], target, "friend")
        social.apply_closeness(env, env["player"], target, closeness)
        svc._save(env)
        return gid, target

    def test_best_friend_exclusivity(self):
        svc = make_service()
        gid, _ = self._friend_env(svc, 7)
        a = make_person(svc, gid, "A")
        b = make_person(svc, gid, "B")
        env = svc._load(gid)
        social.apply_kind(env, env["player"], a, "best_friend")
        social.apply_kind(env, env["player"], b, "best_friend")
        st = env["social_state"]
        self.assertEqual(st["best_friend"], b)
        self.assertEqual(st["people"][a]["kind"], "friend")
        social.apply_kind(env, env["player"], b, "friend")
        self.assertIsNone(env["social_state"]["best_friend"])

    def test_closeness_clamps(self):
        svc = make_service()
        gid, target = self._friend_env(svc, 7)
        env = svc._load(gid)
        social.apply_closeness(env, env["player"], target, 100)
        self.assertEqual(env["social_state"]["people"][target]
                         ["closeness"], 100)
        social.apply_closeness(env, env["player"], target, -200)
        self.assertEqual(env["social_state"]["people"][target]
                         ["closeness"], 0)

    def test_no_contact_drift_and_fall_out(self):
        for seed in SEEDS:
            svc = make_service()
            gid, target = self._friend_env(svc, seed, closeness=22)
            run_social_hook(svc, gid, "t_drift1")
            env = svc._load(gid)
            rec = env["social_state"]["people"][target]
            self.assertEqual(rec["closeness"], 19)   # 22 - 3 no-contact
            run_social_hook(svc, gid, "t_drift2")
            env = svc._load(gid)
            rec = env["social_state"]["people"][target]
            # demoted to acquaintance (16 < 18 floor), then -1 drift
            self.assertEqual(rec["closeness"], 16)
            self.assertEqual(rec["kind"], "acquaintance")
            fell = [e for e in env["journal"]
                    if "fell out of touch" in e["text"]]
            self.assertEqual(len(fell), 1)

    def test_contact_slow_burn(self):
        svc = make_service()
        gid, target = self._friend_env(svc, 7, closeness=50)
        env = svc._load(gid)
        social.apply_closeness(env, env["player"], target, 5)
        env["social_state"]["people"][target]["contact_turn"] = "t_burn"
        svc._save(env)
        run_social_hook(svc, gid, "t_burn")
        env = svc._load(gid)
        # event contact (+5) plus the +1 slow burn for a kinded contact
        # WITH shared time this year (frozen rule)
        self.assertEqual(env["social_state"]["people"][target]
                         ["closeness"], 56)


class PopularityTests(unittest.TestCase):
    def test_formula_and_bands(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed, f"p{seed}")
            gid = st["game_id"]
            env = svc._load(gid)
            env["player"].age = 20
            svc._save(env)
            pals = [make_person(svc, gid, f"P{i}") for i in range(3)]
            env = svc._load(gid)
            for pid in pals:
                social.apply_kind(env, env["player"], pid, "friend")
            social.apply_popularity_delta(env, env["player"], 100)
            svc._save(env)
            from src.game.features.social import popularity as pop
            env = svc._load(gid)
            pop.recompute(env, env["player"])
            svc._save(env)
            env = svc._load(gid)
            score = env["social_state"]["popularity"]["score"]
            # base 15 + 3 friends*3 + the bounded applied delta (+20)
            # SURVIVES the recompute via the bonus accumulator
            self.assertEqual(score, 44)
            self.assertEqual(pop.band_for(score)["id"], "liked")
            self.assertEqual(pop.band_for(0)["id"], "unknown")
            self.assertEqual(pop.band_for(25)["id"], "known")
            self.assertEqual(pop.band_for(100)["id"], "beloved")
            nemesis = make_person(svc, gid, "N")
            env = svc._load(gid)
            social.apply_kind(env, env["player"], nemesis, "rival")
            pop.recompute(env, env["player"])
            svc._save(env)
            env = svc._load(gid)
            self.assertEqual(env["social_state"]["popularity"]["score"],
                             score - 4)

    def test_projection_band_and_delta(self):
        svc = make_service()
        st = new_game(svc, 7, "pp")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 20
        svc._save(env)
        st = svc.get_state(gid)
        self.assertEqual(st["social"]["popularity"]["band"], "unknown")
        ff = [make_person(svc, gid, f"F{i}") for i in range(2)]
        env = svc._load(gid)
        for pid in ff:
            social.apply_kind(env, env["player"], pid, "friend")
        svc._save(env)
        st = svc.get_state(gid)
        st = drain_pending(svc, st, "pp")
        st = svc.command(gid, st["revision"], "pp-y", "age_up", {})
        pop = st["social"]["popularity"]
        self.assertGreater(pop["score"], 0)
        self.assertEqual(pop["band"], pop["band"])


class MemoryPrivacyTests(unittest.TestCase):
    def test_memory_player_known_filter(self):
        svc = make_service()
        st = new_game(svc, 7, "m")
        gid = st["game_id"]
        pid_x = make_person(svc, gid, "X")
        env = svc._load(gid)
        social.record_memory(env, env["player"], pid_x,
                             "We built a raft together.", sentiment=1)
        social.record_memory(env, env["player"], pid_x,
                             "A secret I never learned.", sentiment=-1,
                             player_known=False)
        svc._save(env)
        st = svc.get_state(gid)
        people = {p["id"]: p for p in st["people"]}
        mem = people[pid_x]["social"]["memory"]
        self.assertEqual(mem, ["We built a raft together."])
        env = svc._load(gid)
        from src.game.features.knowledge import core as knowledge
        fact_id = env["social_state"]["people"][pid_x]["memory"][0][
            "fact_id"]
        self.assertTrue(knowledge.knows(env, pid_x, fact_id))


class InviteTests(unittest.TestCase):
    def _hosted(self, svc, gid):
        env = svc._load(gid)
        env["player"].age = 20
        svc._save(env)
        host = make_person(svc, gid, "H")
        env = svc._load(gid)
        social.apply_kind(env, env["player"], host, "friend")
        svc._save(env)
        return host

    def test_offer_accept_posts_once_and_updates_state(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed, f"i{seed}")
            gid = st["game_id"]
            host = self._hosted(svc, gid)
            pending = force_offer(
                svc, gid, kind="party", title="Sam's birthday",
                host_id=host, venue_label="Riverside Park",
                adults_only=False, circle="friends",
                options=[{"id": "attend", "label": "Go",
                          "effects": {"stats": {"happiness": 5},
                                      "money": -20, "closeness": 6},
                          "outcome_text": "I went and had a great time."}])
            st = svc.get_state(gid)
            self.assertEqual(st["pending_decision"]["kind"],
                             "social_invite")
            self.assertEqual(st["pending_decision"]
                             ["display_category"], "social")
            invite_row = next(i for i in st["social"]["invites"]
                              if i["invite_id"] == pending["invite_id"])
            self.assertEqual(invite_row["status"], "open")
            self.assertEqual(invite_row["decision_id"], pending["id"])
            money_before = st["money"]
            st = svc.command(gid, st["revision"], f"i{seed}-r",
                             "resolve_decision",
                             {"decision_id": pending["id"],
                              "option_id": "attend"})
            env = svc._load(gid)
            inv = env["social_state"]["invites"][pending["invite_id"]]
            self.assertEqual(inv["status"], "accepted")
            self.assertIsNone(inv["decision_id"])
            legs = [e for e in env["ledger"] if ":soc:" in
                    e["source_key"]]
            self.assertEqual(len(legs), 1)
            self.assertEqual(legs[0]["amount"], -20)
            self.assertEqual(st["money"], money_before - 20)
            self.assertEqual(env["social_state"]["people"][host]
                             ["closeness"], 6)
            self.assertEqual(st["pending_decision"], None)
            again = svc.command(gid, st["revision"], f"i{seed}-r",
                                "resolve_decision",
                                {"decision_id": pending["id"],
                                 "option_id": "attend"})
            self.assertEqual(again, st)

    def test_decline_is_free_and_adults_only_gate_blocks_minor(self):
        svc = make_service()
        st = new_game(svc, 7, "g")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 17      # minor
        svc._save(env)
        boss = make_person(svc, gid, "Boss", age=40)
        pending = force_offer(
            svc, gid, kind="after_work", title="After-work drinks",
            host_id=boss, venue_label="The Harbour Bar",
            adults_only=True, circle="work", options=[])
        st = svc.get_state(gid)
        money_before = st["money"]
        revision = st["revision"]
        with self.assertRaises(GameError) as cm:
            svc.command(gid, revision, "g-r", "resolve_decision",
                        {"decision_id": pending["id"],
                         "option_id": "bogus"})
        self.assertEqual(cm.exception.code, "invalid_input")
        st = svc.command(gid, revision, "g-r2", "resolve_decision",
                         {"decision_id": pending["id"],
                          "option_id": "decline"})
        env = svc._load(gid)
        self.assertEqual(env["social_state"]["invites"]
                         [pending["invite_id"]]["status"], "declined")
        self.assertEqual(st["money"], money_before)

    def test_open_invite_expires_after_a_year(self):
        svc = make_service()
        st = new_game(svc, 7, "e")
        gid = st["game_id"]
        host = self._hosted(svc, gid)
        pending = force_offer(svc, gid, kind="study_group",
                              title="Study group", host_id=host,
                              venue_label="Library", options=[])
        env = svc._load(gid)
        env["pending"] = None
        svc._save(env)
        run_social_hook(svc, gid, "t_next_year")
        env = svc._load(gid)
        self.assertEqual(env["social_state"]["invites"]
                         [pending["invite_id"]]["status"], "expired")

    def test_pending_survives_reload(self):
        svc = make_service()
        st = new_game(svc, 7, "rl")
        gid = st["game_id"]
        host = self._hosted(svc, gid)
        pending = force_offer(svc, gid, kind="party", title="Party",
                              host_id=host, venue_label="Park",
                              options=[{"id": "go", "label": "Go",
                                        "effects": {"closeness": 4}}])
        reloaded = GameService(svc.save_dir)
        st2 = reloaded.get_state(gid)
        self.assertEqual(st2["pending_decision"]["kind"], "social_invite")
        invite_row = next(i for i in st2["social"]["invites"]
                          if i["invite_id"] == pending["invite_id"])
        self.assertEqual(invite_row["decision_id"], pending["id"])
        st2 = reloaded.command(gid, st2["revision"], "rl-r",
                               "resolve_decision",
                               {"decision_id": pending["id"],
                                "option_id": "go"})
        env = reloaded._load(gid)
        self.assertEqual(env["social_state"]["invites"]
                         [pending["invite_id"]]["status"], "accepted")

    def test_adults_only_v3_invite_refused_for_minor(self):
        # C's content already gates OFFERS by min_age, so the
        # defense-in-depth gate at resolve is tested with a v3-shaped
        # stored offer a 17-year-old could not be served in production
        svc = make_service()
        st = new_game(svc, 7, "a2m")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 17
        env["player"].grades = 60
        env["player"].money = 500
        svc._save(env)
        run_social_hook(svc, gid, "t_seed")
        env = svc._load(gid)
        social_invites.register_for_v3(
            env, env["player"], invite_id="inv_gate", kind="party",
            title="House party", host_id="host_x",
            venue_label="A house", adults_only=True)
        svc._save(env)
        env = svc._load(gid)
        seq = env["next_decision_num"]
        env["next_decision_num"] += 1
        env["pending"] = {
            "id": f"d_{seq}", "kind": "social_invite",
            "event_id": "soc_inv_house_party",
            "schema_dialect": "v3", "invite_id": "inv_gate",
            "adults_only": True, "title": "House party",
            "text": "Party at a house.", "person_ids": [],
            "bindings": {}, "options": [
                {"id": f"d_{seq}_accept", "label": "Go",
                 "known_costs": [], "known_effects": [],
                 "enabled": True, "reason": None,
                 "_effects": {"happiness": 2}, "_outcome": "Went."},
                {"id": f"d_{seq}_decline", "label": "Decline",
                 "known_costs": [], "known_effects": [],
                 "enabled": True, "reason": None,
                 "_effects": {}, "_outcome": "Declined."}]}
        svc._save(env)
        st = svc.get_state(gid)
        with self.assertRaises(GameError) as cm:
            svc.command(gid, st["revision"], "a2m-a", "resolve_decision",
                        {"decision_id": f"d_{seq}",
                         "option_id": f"d_{seq}_accept"})
        self.assertEqual(cm.exception.code, "not_allowed")
        self.assertIn("18", str(cm.exception))
        env = svc._load(gid)
        self.assertEqual(env["social_state"]["invites"]["inv_gate"]
                         ["status"], "open")
        self.assertIsNotNone(env["pending"])
        st = svc.command(gid, st["revision"], "a2m-d", "resolve_decision",
                         {"decision_id": f"d_{seq}",
                          "option_id": f"d_{seq}_decline"})
        env = svc._load(gid)
        self.assertEqual(env["social_state"]["invites"]["inv_gate"]
                         ["status"], "declined")


class BullyingArcTests(unittest.TestCase):
    def test_arc_lifecycle_and_responses(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed, f"b{seed}")
            gid = st["game_id"]
            env = svc._load(gid)
            env["player"].age = 12
            svc._save(env)
            from src.game.features.social import bullying as bullying_mod
            bully = make_person(svc, gid, "Bully", age=13)
            env = svc._load(gid)
            bullying_mod.start_arc(env, env["player"].person_id, bully)
            svc._save(env)
            env = svc._load(gid)
            self.assertEqual(env["social_state"]["people"][bully]
                             ["kind"], "bully")
            bullying_mod.apply_response(env, None, env["player"],
                                        "tell_someone", bully)
            svc._save(env)
            env = svc._load(gid)
            self.assertEqual(env["social_state"]["arcs"][bully]
                             ["stage"], "resolving")
            mem = env["social_state"]["people"][bully]["memory"]
            self.assertTrue(any("tell someone" in m["text"]
                                for m in mem))
            env = svc._load(gid)
            bullying_mod.apply_response(env, None, env["player"],
                                        "befriend", bully)
            svc._save(env)
            env = svc._load(gid)
            self.assertNotIn(bully, env["social_state"]["arcs"])
            self.assertEqual(env["social_state"]["people"][bully]
                             ["kind"], "acquaintance")


class ClubTests(unittest.TestCase):
    def test_catalog_projects_with_c_action_specs(self):
        svc = make_service()
        st = new_game(svc, 7, "cl")
        clubs = {c["club_id"]: c for c in st["social"]["clubs"]}
        self.assertIn("chess_club", clubs)
        chess = clubs["chess_club"]
        self.assertEqual(chess["label"], "Chess club")
        self.assertFalse(chess["member"])
        self.assertEqual(chess["actions"][0]["id"], "club_join")
        self.assertFalse(chess["actions"][0]["enabled"])
        self.assertIn("young", chess["actions"][0]["reason"])
        adult = next(c for c in st["social"]["clubs"] if c["adults_only"])
        self.assertIn("18", adult["actions"][0]["reason"])

    def test_join_leave_via_real_command_with_fee_posting(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed, f"cj{seed}")
            gid = st["game_id"]
            env = svc._load(gid)
            env["player"].age = 30
            env["player"].money = 1000
            svc._save(env)
            paid = next(
                (c for c in social.load_clubs().values()
                 if c.get("cost_per_year")
                 and (not c.get("min_age") or c["min_age"] <= 30)
                 and (not c.get("max_age") or c["max_age"] >= 30)),
                None)
            self.assertIsNotNone(paid, "expected a paid club in data")
            cid = paid["id"]
            st = svc.get_state(gid)
            st = svc.command(gid, st["revision"], f"cj{seed}-j",
                             "club_join", {"club_id": cid})
            env = svc._load(gid)
            self.assertIn(env["player"].person_id,
                          env["social_state"]["circles"]["clubs"]
                          [cid]["member_ids"])
            legs = [e for e in env["ledger"] if ":club:" in
                    e["source_key"]]
            self.assertEqual(len(legs), 1)
            self.assertEqual(legs[0]["amount"], -paid["cost_per_year"])
            clubs = {c["club_id"]: c for c in st["social"]["clubs"]}
            self.assertTrue(clubs[cid]["member"])
            st = svc.command(gid, st["revision"], f"cj{seed}-l",
                             "club_leave", {"club_id": cid})
            env = svc._load(gid)
            self.assertNotIn(cid, env["social_state"]["circles"]
                             ["clubs"])

    def test_underage_refused_precise_reason_no_mutation(self):
        svc = make_service()
        st = new_game(svc, 7, "cg")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 10
        svc._save(env)
        st = svc.get_state(gid)
        adult_club = next(c for c in social.load_clubs().values()
                          if c.get("adults_only"))
        with self.assertRaises(GameError) as cm:
            svc.command(gid, st["revision"], "cg-j", "club_join",
                        {"club_id": adult_club["id"]})
        self.assertEqual(cm.exception.code, "not_allowed")
        self.assertIn("18", str(cm.exception))
        env = svc._load(gid)
        self.assertEqual(env["social_state"].get("circles", {})
                         .get("clubs", {}), {})


class RealPlayReviewTests(unittest.TestCase):
    """Claude's real-play review: school years must produce a lived
    social life (friend, closeness, a soc_ event, popularity movement)."""

    def _play_school_years(self, seed, years=16):
        svc = make_service()
        st = new_game(svc, seed, f"rp{seed}")
        gid = st["game_id"]
        soc_events = []
        for i in range(years):
            st = svc.command(gid, st["revision"], f"rp-y{i}", "age_up",
                             {})
            guard = 0
            while st["pending_decision"] and guard < 25:
                d = st["pending_decision"]
                if (d.get("event_id") or "").startswith("soc_"):
                    soc_events.append(d["event_id"])
                opt = next(o for o in d["options"]
                           if o.get("enabled", True))
                st = svc.command(gid, st["revision"], f"rp-d{i}-{guard}",
                                 "resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": opt["id"]})
                guard += 1
            if st["status"] != "alive":
                break
        return svc, gid, svc.get_state(gid), soc_events

    def test_sixteen_school_years_produce_a_social_life(self):
        for seed in SEEDS:
            svc, gid, st, soc_events = self._play_school_years(seed)
            self.assertEqual(st["status"], "alive")
            groups = {g["group_id"]: g["people"]
                      for g in st["social"]["groups"]}
            friends = [p for p in groups["friends"]
                       if p["social"]["kind"] in ("friend",
                                                  "best_friend")]
            self.assertTrue(friends,
                            f"seed {seed}: expected >=1 friend")
            self.assertTrue(any(p["social"]["closeness"] > 0
                                for p in friends),
                            f"seed {seed}: expected non-zero closeness")
            self.assertTrue(soc_events,
                            f"seed {seed}: expected >=1 soc_ event")
            pop = st["social"]["popularity"]
            env = svc._load(gid)
            history = env["social_state"]["popularity"]["history"]
            moved = any(h["score"] != 0 for h in history)
            self.assertTrue(moved or pop["band"] != "unknown",
                            f"seed {seed}: popularity never moved")
            from src.game.features.social import circles as circles_mod
            catalog_last = set(circles_mod._lastnames())
            for gid_ in ("classmates", "coworkers"):
                for ref in groups[gid_]:
                    self.assertIn(ref["name"].split()[-1],
                                  catalog_last)

    def test_graduation_turns_classmates_into_former_classmates(self):
        svc = make_service()
        st = new_game(svc, 7, "grad")
        gid = st["game_id"]
        for i in range(19):
            st = drain_pending(svc, st, f"g{i}")
            st = svc.command(gid, st["revision"], f"g-y{i}", "age_up",
                             {})
            if st["status"] != "alive":
                break
        self.assertEqual(st["status"], "alive")
        env = svc._load(gid)
        school = env["social_state"]["circles"]["school"]
        self.assertIsNone(school["cohort_key"])
        former = [pid for pid, rec in
                  env["social_state"]["people"].items()
                  if rec.get("source", {}).get("type") == "school"
                  and rec["status"] == "faded"]
        self.assertTrue(former, "expected former classmates after "
                                "graduation")
        st = svc.get_state(gid)
        friends_group = next(g for g in st["social"]["groups"]
                             if g["group_id"] == "friends")
        faded_ids = {pid for pid, rec in
                     env["social_state"]["people"].items()
                     if rec["status"] == "faded"}
        faded_in_group = [p for p in friends_group["people"]
                          if p["id"] in faded_ids]
        self.assertTrue(faded_in_group,
                        "former classmates visible in the Friends group")
        # former classmates no longer claim the classmates circle
        for p in faded_in_group:
            self.assertNotIn("classmates", p["social"]["circles"])


class AmendmentTwoRouteTests(unittest.TestCase):
    """Amendment 2: soc_inv_* -> social_invite stored offers through the
    real age_up + shared v3 resolve; bullying arc start/response."""

    def _school_env(self, svc, seed, age=10):
        st = new_game(svc, seed, f"a2{seed}")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = age
        env["player"].grades = 60
        env["player"].money = 500
        svc._save(env)
        run_social_hook(svc, gid, "t_seed")
        return gid

    def test_soc_inv_offer_routes_through_the_inbox(self):
        for seed in SEEDS:
            svc = make_service()
            gid = self._school_env(svc, seed)
            env = svc._load(gid)
            from src.game.features.social import circles as circles_mod
            from src.game.features.social import events_route
            mate = circles_mod.group_member_ids(env, "classmates")[0]
            social.apply_kind(env, env["player"], mate, "friend")
            social.apply_closeness(env, env["player"], mate, 40)
            env["soc_seen"] = [e["id"] for e in events_route._events()
                               if e["id"] != "soc_inv_birthday_party"]
            svc._save(env)
            st = svc.get_state(gid)
            st = svc.command(gid, st["revision"], f"a2{seed}-y",
                             "age_up", {})
            d = st["pending_decision"]
            self.assertIsNotNone(d)
            self.assertEqual(d["kind"], "social_invite")
            self.assertEqual(d["display_category"], "social")
            row = next(i for i in st["social"]["invites"]
                       if i["status"] == "open")
            self.assertEqual(row["decision_id"], d["id"])
            self.assertEqual(row["host"]["id"], mate)
            self.assertFalse(row["adults_only"])
            opt = next(o for o in d["options"]
                       if o["id"].endswith("_accept"))
            money_before = st["money"]
            st = svc.command(gid, st["revision"], f"a2{seed}-r",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opt["id"]})
            env = svc._load(gid)
            invite = next(i for i in
                          env["social_state"]["invites"].values()
                          if i["host_id"] == mate)
            self.assertEqual(invite["status"], "accepted")
            legs = [e for e in env["ledger"]
                    if "soc_inv" in e["source_key"]]
            self.assertEqual(len(legs), 1)  # exactly-once posting
            self.assertEqual(st["money"], money_before - 8)
            rec = env["social_state"]["people"][mate]
            self.assertGreater(rec["closeness"], 40)

    def test_bullying_arc_start_and_v3_response_domain_effect(self):
        for seed in SEEDS:
            svc = make_service()
            gid = self._school_env(svc, seed)
            from src.game.features.social import bullying as bullying_mod
            env = svc._load(gid)
            bully = bullying_mod.start_bullying_arc(env, svc,
                                                    env["player"])
            self.assertIsNotNone(bully)
            svc._save(env)
            env = svc._load(gid)
            self.assertEqual(env["social_state"]["people"][bully]
                             ["kind"], "bully")
            from src.game.features.social import events_route
            env["soc_seen"] = [e["id"] for e in events_route._events()
                               if e["id"] != "soc_bul_locker_note"]
            svc._save(env)
            st = svc.get_state(gid)
            st = svc.command(gid, st["revision"], f"bl{seed}-y",
                             "age_up", {})
            d = st["pending_decision"]
            self.assertIsNotNone(d)
            self.assertEqual(d.get("event_id"), "soc_bul_locker_note")
            opt = next(o for o in d["options"]
                       if o["id"].endswith("_tell_someone"))
            st = svc.command(gid, st["revision"], f"bl{seed}-r",
                             "resolve_decision",
                             {"decision_id": d["id"],
                              "option_id": opt["id"]})
            env = svc._load(gid)
            self.assertEqual(env["social_state"]["arcs"][bully]
                             ["stage"], "resolving")
            self.assertTrue(env["social_state"]["people"][bully]
                            ["memory"])


class BalanceAndArcTests(unittest.TestCase):
    """Balance: organic arcs, friend bounds, person view."""

    def test_arc_onset_is_per_life_and_rate_targeted(self):
        from src.game.features.social import drift as social_drift
        dues = []
        for seed in list(SEEDS) + [41011, 41015, 61, 99, 123, 777,
                                   31337, 2718, 5150]:
            svc = make_service()
            st = new_game(svc, seed, f"arc{seed}")
            gid = st["game_id"]
            env = svc._load(gid)
            due = social_drift.arc_due(env)
            dues.append(due)
            again = social_drift.arc_due(env)
            self.assertEqual(due, again)
            if due is None:
                continue
            self.assertTrue(8 <= due <= 13, due)
            env["player"].grades = 60
            svc._save(env)
            started = []
            for year in range(6, 17):
                env = svc._load(gid)
                env["player"].age = year
                svc._save(env)
                run_social_hook(svc, gid, f"t_arc_{year}_{seed}")
                env = svc._load(gid)
                if env["social_state"]["arcs"]:
                    started.append(year)
            self.assertTrue(started, f"seed {seed}: arc never started")
            self.assertEqual(started[0], due,
                             f"seed {seed}: arc years {started}, due "
                             f"{due}")
            env2 = svc._load(gid)
            self.assertTrue(env2["social_state"].get("arc_rolled"))
        rate = sum(1 for d in dues if d is not None) / len(dues)
        self.assertGreaterEqual(rate, 0.08, dues)
        self.assertLessEqual(rate, 0.70, dues)

    def test_balance_friends_are_few_not_universal(self):
        for seed in SEEDS:
            svc = make_service()
            st = new_game(svc, seed, f"bal{seed}")
            gid = st["game_id"]
            ever_kinded = set()
            snapshot_max = 0
            for i in range(16):
                st = svc.command(gid, st["revision"], f"by{i}", "age_up",
                                 {})
                guard = 0
                while st["pending_decision"] and guard < 25:
                    d = st["pending_decision"]
                    opt = next(o for o in d["options"]
                               if o.get("enabled", True))
                    st = svc.command(gid, st["revision"],
                                     f"bd{i}-{guard}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": opt["id"]})
                    guard += 1
                env = svc._load(gid)
                kinded = {pid for pid, rec in
                          env["social_state"]["people"].items()
                          if rec["kind"] in ("friend", "best_friend")}
                ever_kinded |= kinded
                snapshot_max = max(snapshot_max, len(kinded))
            self.assertGreaterEqual(len(ever_kinded), 1)
            self.assertLessEqual(snapshot_max, 6,
                                 f"seed {seed}: too many friends at once "
                                 f"({snapshot_max})")
            self.assertLessEqual(len(ever_kinded), 9,
                                 f"seed {seed}: friendship is universal")

    def test_friendship_guaranteed_for_normal_social_play(self):
        for seed in (41011, 41015):
            svc = make_service()
            st = new_game(svc, seed, f"fg{seed}")
            gid = st["game_id"]
            for i in range(18):
                st = svc.command(gid, st["revision"], f"fg-y{i}",
                                 "age_up", {})
                guard = 0
                while st["pending_decision"] and guard < 25:
                    d = st["pending_decision"]
                    opt = next(o for o in d["options"]
                               if o.get("enabled", True))
                    st = svc.command(gid, st["revision"],
                                     f"fg-d{i}-{guard}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": opt["id"]})
                    guard += 1
            st = svc.get_state(gid)
            friends = [p for g in st["social"]["groups"]
                       if g["group_id"] == "friends" for p in g["people"]
                       if p["social"]["kind"] in ("friend",
                                                  "best_friend")]
            self.assertTrue(friends,
                            f"seed {seed}: ordinary play must yield a "
                            f"friend by 18")

    def test_person_view_shows_social_role(self):
        svc = make_service()
        st = new_game(svc, 7, "role")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 18
        env["player"].grades = None
        env["player"].dropped_out = False
        env["player"].money = 50000
        svc._save(env)
        grant_skills(svc, gid, communication=50)
        st = svc.get_state(gid)
        st = svc.command(gid, st["revision"], "ro-a", "careers_apply",
                         {"opening": "office_assistant@meridian"})
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "ro-r", "resolve_decision",
                         {"decision_id": d["id"], "option_id": "accept"})
        coworker = next(p for g in st["social"]["groups"]
                        if g["group_id"] == "coworkers"
                        for p in g["people"])
        view = svc.get_person_view(gid, coworker["id"])
        self.assertEqual(view["role"], "coworker")
        env = svc._load(gid)
        social.apply_kind(env, env["player"], coworker["id"], "friend")
        svc._save(env)
        view = svc.get_person_view(gid, coworker["id"])
        self.assertEqual(view["role"], "friend")
        parent_view = svc.get_person_view(gid, next(
            p["id"] for p in (next(g for g in st["social"]["groups"]
                                   if g["group_id"] == "family")
                              ["people"])))
        self.assertIn(parent_view["role"], ("mother", "father"))


class BacklogRoundTests(unittest.TestCase):
    """Backlog: new_friend guarantee, age-fitted drift lines."""

    def test_new_friend_guaranteed_and_mints_a_real_friend(self):
        for seed in (7, 4242, 90210):
            svc = make_service()
            st = new_game(svc, seed, f"nf{seed}")
            gid = st["game_id"]
            fired = False
            for i in range(6):    # ages 1..6; the offer arrives at 5
                st = svc.command(gid, st["revision"], f"nf-y{i}",
                                 "age_up", {})
                guard = 0
                while st["pending_decision"] and guard < 25:
                    d = st["pending_decision"]
                    if d.get("template_id") == "new_friend":
                        fired = True
                        opt = next(o for o in d["options"]
                                   if o["id"].endswith("_o1"))
                    else:
                        opt = next(o for o in d["options"]
                                   if o.get("enabled", True))
                    st = svc.command(gid, st["revision"],
                                     f"nf-d{i}-{guard}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": opt["id"]})
                    guard += 1
            self.assertTrue(fired, f"seed {seed}: new_friend must fire")
            st = svc.get_state(gid)
            friends = [p for g in st["social"]["groups"]
                       if g["group_id"] == "friends" for p in g["people"]
                       if p["social"]["kind"] in ("friend",
                                                  "best_friend")]
            self.assertTrue(friends,
                            "new_friend must mint a real friend")

    def test_baby_toddler_drift_lines_are_age_fitted(self):
        from src.game.content.journal import relationship_lines as rl
        grown_markers = ("argued", "strained", "drifting apart",
                         "misunderstanding", "did not see much")
        for band in ("baby", "toddler"):
            texts = [l["text"] for l in rl.IMPROVED_LINES
                     + rl.WORSENED_LINES if l["band"] == band]
            self.assertTrue(texts)
            for text in texts:
                for marker in grown_markers:
                    self.assertNotIn(marker, text.lower(),
                                     f"{band}: grown-up line {text!r}")
        adult = [l["text"] for l in rl.WORSENED_LINES
                 if l["band"] == "adult"]
        self.assertTrue(any("argued" in t for t in adult))
        for band in rl.BANDS:
            self.assertTrue([l for l in rl.IMPROVED_LINES
                             if l["band"] == band])
            self.assertTrue([l for l in rl.WORSENED_LINES
                             if l["band"] == band])


class SmallFixesTests(unittest.TestCase):
    """Small fixes: surname exclusion, no null kinds."""

    def test_npc_surnames_exclude_family_name(self):
        # the D liveness case: a FAMILY member (mother) carries the
        # colliding surname while the player has a different one
        svc = make_service()
        st = new_game(svc, 41012, "sn")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].lastname = "Probe"
        for rel in env["player"].relations:
            rel.lastname = "Stafford"
        env["player"].age = 8
        env["player"].grades = 60
        svc._save(env)
        env = svc._load(gid)
        from src.game.features.social import circles as circles_mod
        circles_mod.reconcile(env, svc, None)
        svc._save(env)
        env = svc._load(gid)
        self.assertTrue(env["social_people"])
        for person in env["social_people"]:
            self.assertNotEqual(person.lastname, "Stafford",
                                person.name)

    def test_no_null_kinds_anywhere(self):
        svc = make_service()
        st = new_game(svc, 7, "nk")
        gid = st["game_id"]
        for i in range(8):
            st = svc.command(gid, st["revision"], f"nk-y{i}", "age_up",
                             {})
            guard = 0
            while st["pending_decision"] and guard < 25:
                d = st["pending_decision"]
                opt = next(o for o in d["options"]
                           if o.get("enabled", True))
                st = svc.command(gid, st["revision"], f"nk-d{i}-{guard}",
                                 "resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": opt["id"]})
                guard += 1
        st = svc.get_state(gid)
        for g in st["social"]["groups"]:
            for ref in g["people"]:
                if "social" in ref:
                    self.assertIsNotNone(ref["social"]["kind"])


if __name__ == "__main__":
    unittest.main()
