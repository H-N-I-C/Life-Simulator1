import builtins
import os
import pickle
import random
import subprocess
import sys
import tempfile
import unittest

from src.game.service import GameService, GameError, ENVELOPE_SCHEMA_VERSION


PROFILE = {
    "first_name": "Ada",
    "last_name": "Nightingale",
    "gender": "female",
    "country": "Australia",
    "seed": 12345,
    "appearance": {
        "schema_version": 1,
        "seed": 7,
        "skin_tone": "skin_04",
        "face_shape": "oval",
        "eye_shape": "almond",
        "eye_color": "brown_dark",
        "brows": "soft",
        "nose": "button",
        "mouth": "smile",
        "hair_texture": "curly",
        "hair_color": "black",
        "marks": ["freckles"],
        "hair_style": "afro_medium",
        "facial_hair": "none",
        "accessories": {"glasses": "none", "headwear": "none",
                        "earrings": "studs"},
        "outfit_style": "tee",
        "outfit_color": "blue",
    },
}


def make_service():
    return GameService(tempfile.mkdtemp(prefix="lifesim-web-test-"))


def strip_ids(state):
    """Person IDs are uuid identity, not simulation output."""
    return [{k: v for k, v in p.items() if k != "id"}
            for p in state["people"]]


def resolve_pending(svc, game_id, revision, request_id, state):
    """Resolve a pending decision, skipping options that are gated
    (e.g. unaffordable), mirroring a player choosing an available option.
    University applications are DECLINED so employed/test lifetimes stay
    out of university (scenario policy)."""
    d = state["pending_decision"]
    options = list(d["options"])
    if d.get("kind") == "university_application":
        options.sort(key=lambda o: 0 if any(
            k in o["id"].lower() for k in ("decline", "no", "not", "skip"))
            else 1)
    last_err = None
    for opt in options:
        try:
            return svc.command(game_id, revision, request_id,
                               "resolve_decision",
                               {"decision_id": d["id"],
                                "option_id": opt["id"]})
        except GameError as e:
            if e.code != "not_allowed":
                raise
            last_err = e
    raise last_err


def age_up_once(svc, game_id, request_id):
    """Age exactly one year, draining any pending chain produced along
    the way (university applications declined so test lifetimes stay
    employable)."""
    st = svc.get_state(game_id)
    n = 0
    while st["pending_decision"]:
        st = resolve_pending(svc, game_id, st["revision"],
                             f"{request_id}-d{n}", st)
        n += 1
        if n > 10:
            raise AssertionError("pending chain did not drain")
    st = svc.command(game_id, st["revision"], request_id, "age_up", {})
    while st["pending_decision"]:
        st = resolve_pending(svc, game_id, st["revision"],
                             f"{request_id}-d{n}", st)
        n += 1
        if n > 10:
            raise AssertionError("pending chain did not drain")
    return st


def _session_resolve(sess, st, rid):
    from src.game.service import GameError
    d = st["pending_decision"]
    options = sorted(d["options"],
                     key=lambda o: 0 if o.get("enabled", True) else 1)
    for opt in options:
        try:
            return sess.command(f"{rid}-{opt['id']}", "resolve_decision",
                                {"decision_id": d["id"],
                                 "option_id": opt["id"]})
        except GameError as e:
            if e.code != "not_allowed":
                raise
    raise AssertionError("no resolvable option")


def age_until(svc, game_id, cap=130, stop_at_decision=False):
    """Age the game; returns (state, saw_decision). Pending decisions are
    resolved with an enabled option so aging can continue. Uses an
    in-memory session for speed and saves once at the end."""
    from src.game.scenarios import Session
    saw = False
    sess = Session(svc, game_id)
    for i in range(cap):
        st = sess.state()
        if st["status"] == "dead":
            sess.save()
            return st, saw
        if st["pending_decision"]:
            saw = True
            if stop_at_decision:
                sess.save()
                return st, saw
            st = _session_resolve(sess, st, f"dec-{i}")
            if st["status"] == "dead":
                sess.save()
                return st, saw
            continue
        st = sess.command(f"age-{i}", "age_up")
        if st["status"] == "dead":
            sess.save()
            return st, saw
    sess.save()
    return sess.state(), saw


class ClosedStdinImportTests(unittest.TestCase):
    def test_game_imports_never_prompt(self):
        code = (
            "import sys; sys.path.insert(0, '.')\n"
            "import src.game\n"
            "import src.people.classes.player\n"
            "import src.menus.main\n"
            "svc = src.game.GameService.__init__\n"
        )
        result = subprocess.run(
            [sys.executable, "-c", code],
            stdin=subprocess.DEVNULL, capture_output=True, text=True,
            timeout=60)
        self.assertEqual(result.returncode, 0, result.stderr)
        self.assertEqual(result.stdout, "")
        self.assertEqual(result.stderr, "")


class CreationTests(unittest.TestCase):
    def test_new_game_state_and_manual_appearance(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        self.assertTrue(state["created"])
        self.assertEqual(state["revision"], 1)
        self.assertEqual(state["status"], "alive")
        player = next(p for p in state["people"] if p["role"] == "player")
        self.assertEqual(state["player_id"], player["id"])
        self.assertTrue(state["player_id"])
        roles = [p["role"] for p in state["people"]]
        self.assertEqual(roles, ["player", "mother", "father"])
        player = state["people"][0]
        # manual appearance wins over the constructor-generated one; the
        # v1 skin id migrates to the v2 catalog tone, seed preserved
        self.assertEqual(player["appearance"]["skin_tone"], "light")
        self.assertEqual(player["appearance"]["seed"], 7)
        self.assertEqual(state["journal"][0]["kind"], "birth")
        actions = {a["id"] for a in state["available_actions"]}
        self.assertEqual(actions,
                         {"age_up", "spend_time", "surrender"})
        self.assertEqual(state["country"], "Australia")

    def test_profile_validation(self):
        svc = make_service()
        bad_profiles = [
            dict(PROFILE, first_name=""),
            dict(PROFILE, first_name="x" * 41),
            dict(PROFILE, last_name=None),
            dict(PROFILE, gender="other"),
            dict(PROFILE, country=123),
            dict(PROFILE, seed="abc"),
            dict(PROFILE, appearance={"skin_tone": "skin_999"}),
            dict(PROFILE, appearance={"facial_hair": "wizard_beard"}),
        ]
        for i, profile in enumerate(bad_profiles):
            with self.assertRaises(GameError, msg=str(profile)) as ctx:
                svc.new_game(profile, f"req-bad-{i}")
            self.assertEqual(ctx.exception.status, 400)
            self.assertEqual(ctx.exception.code, "invalid_input")
        # fields belonging to categories the current catalog does not
        # have are ignored (forward-compatible schemas) — accepted
        st = svc.new_game(
            dict(PROFILE, appearance={"marks": ["nope"],
                                      "accessories": {"glasses": "xray"}}),
            "req-forward-compat")
        self.assertEqual(st["revision"], 1)

    def test_deterministic_seed_reproduces_life(self):
        svc = make_service()
        s1 = svc.new_game(PROFILE, "r1")
        s2 = svc.new_game(PROFILE, "r2")
        self.assertNotEqual(s1["game_id"], s2["game_id"])
        for i in range(12):
            s1 = age_up_once(svc, s1["game_id"], f"a{i}")
            s2 = age_up_once(svc, s2["game_id"], f"a{i}")
        self.assertEqual(s1["stats"], s2["stats"])
        self.assertEqual(strip_ids(s1), strip_ids(s2))

    def test_global_random_untouched_by_commands(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "r1")
        random.seed(999)
        before = random.getstate()
        svc.command(state["game_id"], 1, "r2", "age_up", {})
        svc.command(state["game_id"], 2, "r3", "age_up", {})
        self.assertEqual(random.getstate(), before)


class InteractionTests(unittest.TestCase):
    def _new(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        mother = next(p for p in state["people"] if p["role"] == "mother")
        return svc, state["game_id"], mother["id"]

    def test_spend_time_once_per_year_and_effects(self):
        svc, gid, mid = self._new()
        # legacy gate: spend time requires the player to be at least 5
        st = svc.get_state(gid)
        for i in range(6):
            st = age_up_once(svc, gid, f"g{i}")
        st = svc.command(gid, st["revision"], "st1", "spend_time",
                         {"target_id": mid})
        entry = st["journal"][-1]
        self.assertEqual(entry["kind"], "relationship")
        self.assertIn("happiness", entry["changes"])
        mother = next(p for p in st["people"] if p["id"] == mid)
        self.assertGreaterEqual(mother["relationship"], 90)
        # annual limit: repeat is a typed 409 (C's enforcement)
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "st2", "spend_time",
                        {"target_id": mid})
        self.assertEqual(ctx.exception.code, "not_allowed")
        self.assertEqual(ctx.exception.status, 409)
        # next year the annual flag resets and the action is offered again
        st = age_up_once(svc, gid, "age1")
        actions = {(a["id"], a.get("target_id"))
                   for a in st["available_actions"]}
        self.assertIn(("spend_time", mid), actions)

    def test_spend_time_invalid_targets(self):
        svc, gid, mid = self._new()
        st = svc.get_state(gid)
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "x1", "spend_time",
                        {"target_id": "p_99"})
        self.assertEqual(ctx.exception.code, "not_found")
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "x2", "spend_time",
                        {"target_id": st["player_id"]})
        # the player is not one of their own relations
        self.assertEqual(ctx.exception.code, "not_allowed")

class DecisionTests(unittest.TestCase):
    def test_early_childhood_decision_flow(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, saw = age_until(svc, gid, cap=30, stop_at_decision=True)
        self.assertTrue(saw, "no decision sampled in childhood")
        self.assertLessEqual(st["people"][0]["age"], 12)
        d = st["pending_decision"]
        self.assertTrue(d["id"].startswith("d_"))
        self.assertGreaterEqual(len(d["options"]), 2)
        for opt in d["options"]:
            # known_effects is a list; empty means the option has no
            # predicted effect (legitimate for calm/no-op choices)
            self.assertIsInstance(opt["known_effects"], list)
        # age was already advanced; pending blocks unrelated actions
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "blk", "age_up", {})
        self.assertEqual(ctx.exception.code, "pending_decision")
        # resolve applies only the option effects
        stats_before = dict(st["stats"])
        money_before = st["money"]
        st2 = svc.command(gid, st["revision"], "dec1", "resolve_decision",
                          {"decision_id": d["id"],
                           "option_id": d["options"][0]["id"]})
        self.assertIsNone(st2["pending_decision"])
        self.assertEqual(st2["journal"][-1]["kind"], "decision")
        self.assertEqual(st2["people"][0]["age"], st["people"][0]["age"])
        # effects are bounded and defined
        for key, delta in st2["journal"][-1]["changes"].items():
            self.assertIn(key, ("happiness", "health", "smarts", "looks",
                                "money", "relationship"))
            self.assertLessEqual(abs(delta), 600)

    def test_resolve_wrong_ids_rejected(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, _ = age_until(svc, gid, cap=30, stop_at_decision=True)
        self.assertTrue(st["pending_decision"])
        d = st["pending_decision"]
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "w1", "resolve_decision",
                        {"decision_id": "d_999",
                         "option_id": d["options"][0]["id"]})
        self.assertEqual(ctx.exception.code, "not_found")
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, st["revision"], "w2", "resolve_decision",
                        {"decision_id": d["id"], "option_id": "d_1_o9"})
        self.assertEqual(ctx.exception.code, "invalid_input")
        self.assertEqual(ctx.exception.status, 400)

    def test_pending_state_survives_restart(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, _ = age_until(svc, gid, cap=30, stop_at_decision=True)
        self.assertTrue(st["pending_decision"])
        svc2 = GameService(svc.save_dir)  # restart
        st2 = svc2.get_state(gid)
        self.assertEqual(st2["pending_decision"], st["pending_decision"])
        self.assertEqual(st2["revision"], st["revision"])


class ReplayTests(unittest.TestCase):
    def test_exactly_once_across_restart(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, _ = age_until(svc, gid, cap=30, stop_at_decision=True)
        d = st["pending_decision"]
        payload = {"decision_id": d["id"], "option_id": d["options"][0]["id"]}
        svc2 = GameService(svc.save_dir)
        st2 = svc2.command(gid, st["revision"], "req-dec-1",
                           "resolve_decision", payload)
        # identical retry returns the original response without reapplying
        rep = svc2.command(gid, 9999, "req-dec-1", "resolve_decision", payload)
        self.assertEqual(rep, st2)
        self.assertEqual(rep["journal"], st2["journal"])
        # different payload with same id is a conflict
        with self.assertRaises(GameError) as ctx:
            svc2.command(gid, 9999, "req-dec-1", "resolve_decision",
                         {"decision_id": d["id"],
                          "option_id": d["options"][-1]["id"]})
        self.assertEqual(ctx.exception.code, "conflict")
        # replay is checked before the revision check (would otherwise
        # have failed with stale_revision above)

    def test_creation_replay(self):
        svc = make_service()
        s1 = svc.new_game(PROFILE, "req-create-001")
        self.assertTrue(s1["created"])
        # after later actions, replaying the creation request returns the
        # ORIGINAL response (revision 1), not the latest state
        st = svc.command(s1["game_id"], 1, "a1", "age_up", {})
        self.assertEqual(st["revision"], 2)
        s2 = svc.new_game(PROFILE, "req-create-001")
        self.assertEqual(s2["game_id"], s1["game_id"])
        self.assertEqual(s2["revision"], 1)
        self.assertEqual(s2["created"], True)
        # still exactly one game on disk
        self.assertEqual(len(svc.list_games()["games"]), 1)
        svc2 = GameService(svc.save_dir)
        s3 = svc2.new_game(PROFILE, "req-create-001")
        self.assertEqual(s3["game_id"], s1["game_id"])
        self.assertEqual(s3["revision"], 1)
        with self.assertRaises(GameError) as ctx:
            svc.new_game(dict(PROFILE, first_name="Bob"), "req-create-001")
        self.assertEqual(ctx.exception.code, "conflict")

    def test_creation_ledger_failure_rolls_back_and_stays_exactly_once(self):
        import unittest.mock as mock
        svc = make_service()
        calls = {"n": 0}
        real = svc._save_creations

        def flaky(ledger):
            calls["n"] += 1
            if calls["n"] == 1:
                # the real _save_creations wraps failures in GameError
                raise GameError(
                    "internal_error",
                    "the operation failed; no changes were applied", 500)
            return real(ledger)

        with mock.patch.object(svc, "_save_creations", flaky):
            with self.assertRaises(GameError) as ctx:
                svc.new_game(PROFILE, "req-create-flaky")
            self.assertEqual(ctx.exception.code, "internal_error")
            self.assertEqual(ctx.exception.status, 500)
        # the failed creation left no game behind
        self.assertEqual(svc.list_games()["games"], [])
        # retry succeeds and creates exactly one life
        s1 = svc.new_game(PROFILE, "req-create-flaky")
        s2 = svc.new_game(PROFILE, "req-create-flaky")
        self.assertEqual(s1["game_id"], s2["game_id"])
        games = svc.list_games()["games"]
        self.assertEqual(len(games), 1)
        # and after a restart the replay still resolves to that one life
        svc2 = GameService(svc.save_dir)
        s3 = svc2.new_game(PROFILE, "req-create-flaky")
        self.assertEqual(s3["game_id"], s1["game_id"])

    def test_creation_ledger_corrupt_fails_closed(self):
        svc = make_service()
        with open(os.path.join(svc.save_dir, "_creations.json"), "w") as f:
            f.write("{corrupt json")
        with self.assertRaises(GameError) as ctx:
            svc.new_game(PROFILE, "req-x")
        self.assertEqual(ctx.exception.code, "internal_error")
        self.assertEqual(ctx.exception.status, 500)

    def test_creation_expiry_via_real_eviction(self):
        # exercise the real transition with a small retention limit: the
        # 6th creation evicts the oldest live entry into a durable tombstone
        svc = GameService(tempfile.mkdtemp(prefix="lifesim-web-test-"),
                          creation_replay_limit=5)
        first = svc.new_game(PROFILE, "req-creation-000")
        for i in range(1, 5):
            svc.new_game(dict(PROFILE, first_name=f"P{i}"),
                         f"req-creation-{i:03d}")
        svc.new_game(dict(PROFILE, first_name="P5"),
                     "req-creation-500")  # triggers eviction of req-000
        for profile, label in (
                (PROFILE, "identical"),
                (dict(PROFILE, first_name="Eve"), "conflicting")):
            with self.assertRaises(GameError) as ctx:
                svc.new_game(profile, "req-creation-000")
            self.assertEqual(ctx.exception.code, "stale_revision", label)
            self.assertEqual(ctx.exception.status, 409)
        # after a restart the tombstone still protects the expired request
        svc2 = GameService(svc.save_dir)
        with self.assertRaises(GameError) as ctx:
            svc2.new_game(PROFILE, "req-creation-000")
        self.assertEqual(ctx.exception.code, "stale_revision")
        # and a live recent creation still replays normally
        live = svc.new_game(dict(PROFILE, first_name="P5"),
                            "req-creation-500")
        replayed = svc2.new_game(dict(PROFILE, first_name="P5"),
                                 "req-creation-500")
        self.assertEqual(replayed["game_id"], live["game_id"])
        self.assertEqual(replayed["revision"], 1)


class RevisionTests(unittest.TestCase):
    def test_stale_revision_and_bad_input(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, 0, "r1", "age_up", {})
        self.assertEqual(ctx.exception.code, "stale_revision")
        self.assertEqual(ctx.exception.status, 409)
        for rev in ("1", True, -1):
            with self.assertRaises(GameError) as ctx:
                svc.command(gid, rev, "r2", "age_up", {})
            self.assertEqual(ctx.exception.code, "invalid_input")
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, 1, "r3", "fly", {})
        self.assertEqual(ctx.exception.code, "invalid_input")
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, 1, "", "age_up", {})
        self.assertEqual(ctx.exception.code, "invalid_input")
        # failed commands leave no trace
        self.assertEqual(svc.get_state(gid)["revision"], 1)


class PersistenceTests(unittest.TestCase):
    def test_restart_preserves_people_ids_appearances_journal(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st = svc.command(gid, 1, "a1", "age_up", {})
        svc2 = GameService(svc.save_dir)
        st2 = svc2.get_state(gid)
        self.assertEqual(st2["people"], st["people"])
        self.assertEqual(st2["journal"], st["journal"])
        self.assertEqual(st2["revision"], st["revision"])
        ids = [p["id"] for p in st2["people"]]
        self.assertEqual(len(ids), len(set(ids)))

    def test_list_games(self):
        svc = make_service()
        s1 = svc.new_game(PROFILE, "c1")
        svc.new_game(dict(PROFILE, first_name="Bob", last_name="Hale",
                          seed=777), "c2")
        games = svc.list_games()["games"]
        self.assertEqual(len(games), 2)
        by_id = {g["game_id"]: g for g in games}
        self.assertEqual(by_id[s1["game_id"]]["name"], "Ada Nightingale")
        self.assertIn("revision", by_id[s1["game_id"]])

    def test_unsupported_schema_rejected(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        path = os.path.join(svc.save_dir, state["game_id"] + ".pickle")
        with open(path, "rb") as f:
            env = pickle.load(f)
        env["schema_version"] = 99
        with open(path, "wb") as f:
            pickle.dump(env, f)
        with self.assertRaises(GameError) as ctx:
            svc.get_state(state["game_id"])
        self.assertEqual(ctx.exception.code, "unsupported_schema")
        self.assertEqual(ctx.exception.status, 409)

    def test_corrupt_save_rejected_without_traceback(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        path = os.path.join(svc.save_dir, state["game_id"] + ".pickle")
        with open(path, "wb") as f:
            f.write(b"not a pickle at all")
        with self.assertRaises(GameError) as ctx:
            svc.get_state(state["game_id"])
        self.assertEqual(ctx.exception.code, "internal_error")
        self.assertEqual(ctx.exception.status, 500)
        self.assertNotIn("pickle", ctx.exception.message)

    def test_id_and_symlink_rejected(self):
        svc = make_service()
        for bad in ("../escape", "a/b", "x" * 65, "..", "a b"):
            with self.assertRaises(GameError) as ctx:
                svc.get_state(bad)
            self.assertEqual(ctx.exception.status, 400)
        state = svc.new_game(PROFILE, "req-create-001")
        link = os.path.join(svc.save_dir, "evil.pickle")
        os.symlink(os.path.join(svc.save_dir,
                                state["game_id"] + ".pickle"), link)
        with self.assertRaises(GameError) as ctx:
            svc.get_state("evil")
        self.assertEqual(ctx.exception.code, "not_found")

    def test_failed_write_rolls_back(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        os.chmod(svc.save_dir, 0o500)  # read+execute: writes fail
        try:
            with self.assertRaises(GameError) as ctx:
                svc.command(gid, 1, "a1", "age_up", {})
            self.assertEqual(ctx.exception.code, "internal_error")
            self.assertEqual(ctx.exception.status, 500)
        finally:
            os.chmod(svc.save_dir, 0o700)
        # disk state unchanged; in-memory discarded
        st = svc.get_state(gid)
        self.assertEqual(st["revision"], 1)
        self.assertEqual(st["people"][0]["age"], 0)


class V1IdentityMigrationTests(unittest.TestCase):
    """v1 (web_id p_XX) -> v2 (canonical person_id UUID) migration."""

    def _make_v1_envelope(self):
        import pickle as _pickle
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        path = os.path.join(svc.save_dir, state["game_id"] + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        # rewrite to the real old format: sequential web_id + v1 schema
        people = svc._walk_people(env["player"])
        mapping = {}
        for i, person in enumerate(people, 1):
            old = f"p_{i:02d}"
            mapping[person.person_id] = old
            person.web_id = old
            del person.person_id  # old saves predate person_id
        env["journal"][0]["person_ids"] = [
            mapping[p] for p in env["journal"][0]["person_ids"]]

        def to_web(state_like):
            if not isinstance(state_like, dict):
                return
            state_like["player_id"] = mapping.get(
                state_like.get("player_id"), state_like.get("player_id"))
            for person in state_like.get("people", []):
                person["id"] = mapping.get(person["id"], person["id"])
            for entry in state_like.get("journal", []):
                entry["person_ids"] = [mapping.get(i, i)
                                       for i in entry["person_ids"]]
            for action in state_like.get("available_actions", []):
                if "target_id" in action:
                    action["target_id"] = mapping.get(
                        action["target_id"], action["target_id"])

        to_web(env.get("creation", {}).get("response"))
        for replay_entry in env.get("replay", []):
            to_web(replay_entry.get("response"))
        env["schema_version"] = 1
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        return svc, state["game_id"], path, set(mapping.values())

    def test_v1_references_migrate_and_persist(self):
        import pickle as _pickle
        svc, gid, path, old_ids = self._make_v1_envelope()
        st = svc.get_state(gid)  # triggers migration + one-time persist
        people_ids = {p["id"] for p in st["people"]}
        # new ids are canonical UUIDs, distinct from old web ids
        self.assertTrue(all(i not in old_ids for i in people_ids))
        # journal references resolve
        for entry in st["journal"]:
            for pid in entry["person_ids"]:
                self.assertIn(pid, people_ids)
        # replay/creation response snapshots remapped too
        env = svc._load(gid)
        response = env["creation"]["response"]
        self.assertIn(response["player_id"], people_ids)
        for person in response["people"]:
            self.assertIn(person["id"], people_ids)
        # migration persisted: reload yields identical ids (no regeneration)
        st2 = GameService(svc.save_dir).get_state(gid)
        self.assertEqual({p["id"] for p in st2["people"]}, people_ids)
        # and a later command still works
        st3 = svc.command(gid, st2["revision"], "m1", "age_up", {})
        self.assertEqual(st3["revision"], st2["revision"] + 1)

    def test_v1_pending_person_ids_inside_snapshots_remapped(self):
        import pickle as _pickle
        svc, gid, path, old_ids = self._make_v1_envelope()
        with open(path, "rb") as f:
            env = _pickle.load(f)
        # a replay response snapshot holding a pending decision with old
        # web_id references (the M1 decision-offered state)
        env["replay"] = [{
            "request_id": "r-old",
            "action": "age_up",
            "payload": {},
            "response": {
                "player_id": "p_01",
                "people": [],
                "journal": [],
                "available_actions": [],
                "pending_decision": {"id": "d_1", "title": "t",
                                     "text": "x",
                                     "person_ids": ["p_02"]},
            },
        }]
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = svc.get_state(gid)  # migrates
        env = svc._load(gid)
        remapped = env["replay"][0]["response"]["pending_decision"]
        people_ids = {pp.person_id for pp in svc._walk_people(env["player"])}
        self.assertIn(remapped["person_ids"][0], people_ids)
        self.assertNotIn("p_02", remapped["person_ids"])

    def test_v1_dangling_reference_rejected_typed(self):
        import pickle as _pickle
        svc, gid, path, old_ids = self._make_v1_envelope()
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["journal"][0]["person_ids"].append("p_99")  # unresolvable
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        with self.assertRaises(GameError) as ctx:
            svc.get_state(gid)
        self.assertEqual(ctx.exception.code, "internal_error")
        self.assertEqual(ctx.exception.status, 500)


class LifespanTests(unittest.TestCase):
    def test_full_life_death_and_readonly_history(self):
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, _ = age_until(svc, gid, cap=140)
        self.assertEqual(st["status"], "dead")
        self.assertGreater(st["people"][0]["age"], 49)
        self.assertEqual(st["available_actions"], [])
        self.assertEqual(st["pending_decision"], None)
        kinds = [e["kind"] for e in st["journal"]]
        self.assertIn("birth", kinds)
        self.assertIn("death", kinds)
        # dead life stays readable and rejects new commands
        svc2 = GameService(svc.save_dir)
        st2 = svc2.get_state(gid)
        self.assertEqual(st2["status"], "dead")
        self.assertEqual(len(st2["journal"]), len(st["journal"]))
        with self.assertRaises(GameError) as ctx:
            svc2.command(gid, st2["revision"], "z1", "age_up", {})
        self.assertEqual(ctx.exception.code, "game_over")


class RNGIsolationTests(unittest.TestCase):
    def test_independent_games_no_cross_talk(self):
        svc = make_service()
        s1 = svc.new_game(PROFILE, "g1")
        s2 = svc.new_game(dict(PROFILE, seed=54321), "g2")
        # interleave commands between the two games
        for i in range(15):
            s1 = age_up_once(svc, s1["game_id"], f"g1-{i}")
            s2 = age_up_once(svc, s2["game_id"], f"g2-{i}")
        # replay game 2 fresh in isolation: same seed must match despite
        # the interleaving above
        svc_iso = make_service()
        s3 = svc_iso.new_game(dict(PROFILE, seed=54321), "g3")
        for i in range(15):
            s3 = age_up_once(svc_iso, s3["game_id"], f"g3-{i}")
        self.assertEqual(s2["stats"], s3["stats"])
        self.assertEqual(strip_ids(s2), strip_ids(s3))

class M1FixRound1Tests(unittest.TestCase):
    """Regressions for docs/collab/M1/codex-review-a.md findings 3-8."""

    def test_dead_relations_remain_in_people_with_history(self):
        # Finding 3: journal/pending person_ids must resolve in people,
        # including dead parents (alive: false with appearance)
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, _ = age_until(svc, gid, cap=140)
        self.assertEqual(st["status"], "dead")
        by_id = {p["id"]: p for p in st["people"]}
        self.assertIn(st["player_id"], by_id)
        self.assertFalse(by_id[st["player_id"]]["alive"])
        for entry in st["journal"]:
            for pid in entry["person_ids"]:
                self.assertIn(pid, by_id, (entry["kind"], pid))
                self.assertIsNotNone(by_id[pid]["appearance"])
        # restart: history still fully resolvable
        st2 = GameService(svc.save_dir).get_state(gid)
        by_id2 = {p["id"]: p for p in st2["people"]}
        for entry in st2["journal"]:
            for pid in entry["person_ids"]:
                self.assertIn(pid, by_id2)

    def _find_seed_with_paid_decision(self):
        for seed in range(40):
            svc = make_service()
            profile = dict(PROFILE, seed=seed)
            try:
                profile.pop("appearance")
            except KeyError:
                pass
            state = svc.new_game(profile, f"seek-{seed}")
            gid = state["game_id"]
            for i in range(70):
                st = svc.get_state(gid)
                if st["pending_decision"]:
                    d = st["pending_decision"]
                    if any("money" in " ".join(o["known_effects"])
                           for o in d["options"]):
                        return svc, gid, st
                    resolve_pending(svc, gid, st["revision"], f"s-{i}", st)
                    continue
                st = svc.command(gid, st["revision"], f"a-{i}", "age_up", {})
                if st["status"] == "dead":
                    break
        self.fail("no seed produced a paid decision")

    def test_paid_decision_gated_at_zero_and_journal_honest(self):
        # Finding 4: unaffordable options are rejected; journal records
        # actual applied deltas
        svc, gid, st = self._find_seed_with_paid_decision()
        self.assertEqual(st["money"], 0)
        d = st["pending_decision"]
        # at $0 the paid option is affordability-disabled with a reason
        paid = next(o for o in d["options"]
                    if o.get("enabled") is False
                    and "$" in (o.get("reason") or ""))
        rev = st["revision"]
        with self.assertRaises(GameError) as ctx:
            svc.command(gid, rev, "pay1", "resolve_decision",
                        {"decision_id": d["id"], "option_id": paid["id"]})
        self.assertEqual(ctx.exception.code, "not_allowed")
        self.assertEqual(ctx.exception.status, 409)
        # nothing changed
        st2 = svc.get_state(gid)
        self.assertEqual(st2["revision"], rev)
        self.assertEqual(st2["money"], 0)
        # the free option resolves and the journal matches actual effects
        free = next(o for o in d["options"] if o is not paid)
        st3 = svc.command(gid, rev, "pay2", "resolve_decision",
                          {"decision_id": d["id"], "option_id": free["id"]})
        entry = st3["journal"][-1]
        self.assertEqual(entry["kind"], "decision")
        self.assertNotIn("money", entry["changes"])

    def test_journal_changes_reflect_capped_values(self):
        # a +happiness option on a nearly-maxed stat reports the actual delta
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        st, _ = age_until(svc, gid, cap=30, stop_at_decision=True)
        d = st["pending_decision"]
        boosting = [o for o in d["options"]
                    if "+happiness" in o["known_effects"]]
        if not boosting:
            self.skipTest("decision has no +happiness option")
        # raise happiness near the cap by editing the saved envelope
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["player"].happiness = 96
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st3 = svc.command(gid, st["revision"], "cap1", "resolve_decision",
                          {"decision_id": d["id"],
                           "option_id": boosting[0]["id"]})
        entry = st3["journal"][-1]
        if "happiness" in entry["changes"]:
            self.assertLessEqual(entry["changes"]["happiness"],
                                 100 - 96)
            self.assertEqual(entry["changes"]["happiness"],
                             st3["stats"]["happiness"] - 96)

    def test_same_seed_same_generated_appearance(self):
        # Finding 5: no manual appearance -> seeded games reproduce the
        # generated player appearance exactly
        svc = make_service()
        p1 = dict(PROFILE)
        p1.pop("appearance")
        p2 = dict(p1)
        s1 = svc.new_game(p1, "n1")
        s2 = svc.new_game(p2, "n2")
        player1 = next(p for p in s1["people"] if p["role"] == "player")
        player2 = next(p for p in s2["people"] if p["role"] == "player")
        self.assertEqual(player1["appearance"], player2["appearance"])
        self.assertIsNotNone(player1["appearance"])

    def test_malformed_supported_envelope_is_typed_error(self):
        # Finding 6: a restricted-loadable but structurally invalid envelope
        # must not leak a raw KeyError
        import pickle as _pickle
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        path = os.path.join(svc.save_dir, state["game_id"] + ".pickle")
        with open(path, "wb") as f:
            _pickle.dump({"schema_version": 1}, f)
        with self.assertRaises(GameError) as ctx:
            svc.get_state(state["game_id"])
        self.assertEqual(ctx.exception.code, "internal_error")
        self.assertEqual(ctx.exception.status, 500)

    def test_profile_appearance_version_validated(self):
        bad = dict(PROFILE, appearance=dict(
            PROFILE["appearance"], schema_version=999))
        svc = make_service()
        with self.assertRaises(GameError) as ctx:
            svc.new_game(bad, "v1")
        self.assertEqual(ctx.exception.code, "invalid_input")
        self.assertEqual(ctx.exception.status, 400)

    def test_spend_time_narration_is_age_appropriate(self):
        # legacy parity (C's rule): spend time is impossible under age 5
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        mother = next(p for p in state["people"]
                      if p["role"] == "mother")
        with self.assertRaises(GameError) as ctx:
            svc.command(state["game_id"], 1, "b1", "spend_time",
                        {"target_id": mother["id"]})
        self.assertEqual(ctx.exception.code, "not_allowed")
        self.assertEqual(ctx.exception.status, 409)
        # at age 6+ the rule narrates a real outing
        st = svc.get_state(state["game_id"])
        for i in range(6):
            st = age_up_once(svc, state["game_id"], f"n{i}")
        st = svc.command(state["game_id"], st["revision"], "b2",
                         "spend_time", {"target_id": mother["id"]})
        text = st["journal"][-1]["text"]
        self.assertTrue(text.strip(), "empty spend-time narration")

    def test_spend_time_journals_actual_capped_deltas(self):
        # upper cap: happiness/relationship already maxed -> no false gains
        svc = make_service()
        state = svc.new_game(PROFILE, "req-create-001")
        gid = state["game_id"]
        mother = next(p for p in state["people"] if p["role"] == "mother")
        st = svc.get_state(gid)
        for i in range(6):
            st = age_up_once(svc, gid, f"cap-grow-{i}")
        import pickle as _pickle
        path = os.path.join(svc.save_dir, gid + ".pickle")
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["player"].happiness = 100
        env["player"].relations[0].relationship = 100
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = svc.command(gid, st["revision"], "cap-up", "spend_time",
                         {"target_id": mother["id"]})
        entry = st["journal"][-1]
        self.assertNotIn("happiness", entry["changes"])
        self.assertNotIn("relationship", entry["changes"])
        # lower cap: refusal at zero happiness logs the real (clamped) delta
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["player"].happiness = 0
        for r in env["player"].relations:
            r.relationship = 5  # below the refusal threshold
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st = age_up_once(svc, gid, "cap-next-year")  # resets annual limits
        rel_target = next(p for p in st["people"]
                          if p["role"] == "mother")
        path = os.path.join(svc.save_dir, gid + ".pickle")
        # ensure the refusal branch: relationship below the threshold
        with open(path, "rb") as f:
            env = _pickle.load(f)
        env["player"].happiness = 0
        for r in env["player"].relations:
            r.relationship = 5
        with open(path, "wb") as f:
            _pickle.dump(env, f)
        st2 = svc.command(gid, st["revision"], "cap-down", "spend_time",
                          {"target_id": rel_target["id"]})
        entry = st2["journal"][-1]
        self.assertEqual(entry["text"].split()[2], "refused")
        self.assertNotIn("happiness", entry["changes"])
        self.assertEqual(st2["stats"]["happiness"], 0)

    def test_missing_country_and_malformed_fields_are_typed(self):
        # profile: missing/empty country -> 400
        svc = make_service()
        for bad in (dict(PROFILE, country=""),
                    {k: v for k, v in PROFILE.items() if k != "country"},
                    dict(PROFILE, country=42)):
            with self.assertRaises(GameError) as ctx:
                svc.new_game(bad, "c1")
            self.assertEqual(ctx.exception.code, "invalid_input")
            self.assertEqual(ctx.exception.status, 400)

        # envelope: missing or wrongly-typed fields -> typed 500, and a
        # subsequent valid request still completes
        import pickle as _pickle
        state = svc.new_game(PROFILE, "req-create-001")
        path = os.path.join(svc.save_dir, state["game_id"] + ".pickle")
        with open(path, "rb") as f:
            good = _pickle.load(f)
        mutations = [
            {k: v for k, v in good.items() if k != "country"},
            dict(good, pending={"options": "notalist"}),
            dict(good, pending={"options": []}),  # missing id/title/text
            dict(good, pending=None) if False else dict(good),  # placeholder
            dict(good, year_actions={"spend_time": "nope"}),
            dict(good, rng_state=(1, "garbage", None)),
            dict(good, journal=[{"text": 5}]),
            dict(good, journal=[{"id": "j_1", "age": 1, "kind": "year",
                                 "text": "x", "person_ids": []}]),  # no changes
            dict(good, replay=[{"request_id": "r", "payload": {}}]),  # no action/response
            dict(good, pending={"id": "d_1", "title": "t", "text": "x",
                                "person_ids": [],
                                "options": [{"id": "o1", "label": "l",
                                             "known_effects": []}]}),
        ]
        mutations = [m for i, m in enumerate(mutations) if i != 3]
        for i, env in enumerate(mutations):
            with open(path, "wb") as f:
                _pickle.dump(env, f)
            with self.assertRaises(GameError) as ctx:
                svc.get_state(state["game_id"])
            self.assertEqual(ctx.exception.code, "internal_error",
                             f"mutation {i}")
            self.assertEqual(ctx.exception.status, 500)
        with open(path, "wb") as f:
            _pickle.dump(good, f)
        self.assertEqual(svc.get_state(state["game_id"])["revision"], 1)

    def test_reunion_requires_living_parents(self):
        from src.game import decisions
        used = [t["id"] for t in decisions.TEMPLATES
                if t["id"] != "family_reunion"]
        # with no living parents the reunion is never offered
        for seed in range(60):
            t = decisions.sample(random.Random(seed), 60, used, [])
            if t is not None:
                self.assertNotEqual(t["id"], "family_reunion")
        # with a living parent it can be offered again
        offered = set()
        for seed in range(60):
            t = decisions.sample(random.Random(seed), 60, used, ["p_02"])
            if t is not None:
                offered.add(t["id"])
        self.assertIn("family_reunion", offered)


if __name__ == "__main__":
    unittest.main()
