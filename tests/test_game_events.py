"""Coordinator-level event-pipeline integration (loader internals are
Kimi C's — C's own suites cover them). These tests pin A's call sites:
decision_offer serves event DATA FILES end-to-end, and automatic events
never block the turn."""

import tempfile
import unittest

from src.game.service import GameService


def make_service():
    return GameService(tempfile.mkdtemp(prefix="lifesim-ev-test-"))


PROFILE = {"first_name": "Ada", "last_name": "Lee", "gender": "female",
           "country": "Australia", "seed": 31}


class EventPipelineIntegrationTests(unittest.TestCase):
    def _drain(self, svc, gid, st, rid):
        """Resolve (and keep resolving carried) pendings until none."""
        n = 0
        while st["pending_decision"]:
            d = st["pending_decision"]
            options = sorted(
                d["options"],
                key=lambda o: 0 if o.get("enabled", True) else 1)
            for opt in options:
                try:
                    st = svc.command(gid, st["revision"], f"{rid}-{n}",
                                     "resolve_decision",
                                     {"decision_id": d["id"],
                                      "option_id": opt["id"]})
                    break
                except Exception as e:
                    from src.game.service import GameError
                    if (not isinstance(e, GameError)
                            or e.code != "not_allowed"):
                        raise
            else:
                raise AssertionError("no resolvable option")
            n += 1
            if n > 10:
                raise AssertionError("pending chain did not drain")
        return st

    def test_decision_offer_serves_data_file_events_end_to_end(self):
        from src.game import events as event_mod
        offered = {e["id"] for e in event_mod.load_event_files()}
        self.assertGreaterEqual(len(offered), 6)
        svc = make_service()
        st = svc.new_game(dict(PROFILE), "r1")
        gid = st["game_id"]
        saw = False
        for i in range(40):
            st = svc.get_state(gid)
            d = st["pending_decision"]
            if d and d.get("event_id") in offered:
                saw = True
                st = self._drain(svc, gid, st, f"r{i}")
                break
            if d:
                st = self._drain(svc, gid, st, f"r{i}")
            st = svc.command(gid, st["revision"], f"a{i}", "age_up", {})
            if st["status"] == "dead":
                break
        self.assertTrue(saw, "data-file event never offered in 40 years")

    def test_auto_events_do_not_block_the_turn(self):
        # auto events (lightning, thanatophobia) apply immediately: the
        # age_up response must never present them as an open pending
        svc = make_service()
        st = svc.new_game(
            dict(PROFILE, seed=11, appearance={
                "schema_version": 1, "seed": 5, "skin_tone": "skin_04"}),
            "r1")
        gid = st["game_id"]
        blocked = False
        for i in range(60):
            st = svc.get_state(gid)
            d = st["pending_decision"]
            if d and d.get("auto"):
                blocked = True
                break
            if d:
                st = self._drain(svc, gid, st, f"r{i}")
            st = svc.command(gid, st["revision"], f"a{i}", "age_up", {})
            if st["status"] == "dead":
                break
        self.assertFalse(blocked, "an auto event blocked the turn")


if __name__ == "__main__":
    unittest.main()


class AutoEventForcedTests(unittest.TestCase):
    """Failing-before guarantee for the auto-event branch: without it,
    _decision_offer raises EventValidationError (the production 500)."""

    AUTO_EVENT = {
        "id": "probe_auto", "title": "Probe", "text": "A flash.",
        "category": "adult", "weight": 1, "auto": True,
        "conditions": {"min_age": 0, "max_age": 200},
        "outcomes": [
            {"weight": 1, "effects": {"stats": {"happiness": 1}},
             "outcome_text": "You felt a spark, {name}."},
        ],
    }

    def test_auto_event_applies_without_pending(self):
        import random as _random
        from src.game import coordinator, events as event_mod
        from src.game.service import GameService
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game(dict(PROFILE), "r1")
        env = svc._load(st["game_id"])
        player = env["player"]
        happy0 = player.happiness
        journal0 = len(env["journal"])

        real_load = event_mod.load_event_files
        event_mod.load_event_files = lambda *a, **k: [self.AUTO_EVENT]
        try:
            result = coordinator._decision_offer(env, None, svc,
                                                 _random.Random(3))
        finally:
            event_mod.load_event_files = real_load
        # applied immediately: no pending, journaled, effect landed
        self.assertIsNone(result["pending_decision"])
        self.assertFalse(result.get("finished_turn"))
        self.assertEqual(len(env["journal"]), journal0 + 1)
        entry = env["journal"][-1]
        self.assertIn(player.firstname, entry["text"])
        self.assertGreaterEqual(player.happiness, happy0)
        # and the turn continues to year_close
        self.assertEqual(result["continuation"]["phase_id"], "year_close")

    def test_terminal_auto_event_performs_death(self):
        import random as _random
        from src.game import coordinator, events as event_mod
        from src.game.service import GameService
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game(dict(PROFILE), "r1")
        env = svc._load(st["game_id"])
        terminal = dict(self.AUTO_EVENT, id="probe_auto_terminal",
                        outcomes=[
                            {"weight": 1, "terminal": True,
                             "effects": {},
                             "outcome_text": "Gone, {name}."}])
        real_load = event_mod.load_event_files
        event_mod.load_event_files = lambda *a, **k: [terminal]
        try:
            result = coordinator._decision_offer(env, None, svc,
                                                 _random.Random(3))
        finally:
            event_mod.load_event_files = real_load
        self.assertTrue(result["finished_turn"])
        self.assertEqual(env["status"], "dead")
        self.assertIn(env["player"].person_id, env["dead_ids"])
        self.assertEqual(result["continuation"]["phase_id"], "year_close")


class AutoEffectsCompletenessTests(unittest.TestCase):
    """Codex #4 (A half): auto effects use the complete applier — stress,
    karma and money post, journal shows ACTUAL deltas, and empty outcome
    text falls back to the event text."""

    def _force_auto(self, event):
        import random as _random
        from src.game import coordinator, events as event_mod
        from src.game.service import GameService
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game(dict(PROFILE), "r1")
        env = svc._load(st["game_id"])
        player = env["player"]
        # M2 rules gate stress changes to employed players: give the
        # probe a job so the stress delta is a real applied change
        player.has_job = True
        player.stress = 20
        svc._save(env)
        env = svc._load(st["game_id"])
        player = env["player"]
        stress0, karma0, happy0 = player.stress, player.karma, \
            player.happiness
        real_load = event_mod.load_event_files
        event_mod.load_event_files = lambda *a, **k: [event]
        try:
            coordinator._decision_offer(env, None, svc, _random.Random(3))
        finally:
            event_mod.load_event_files = real_load
        return env, player, (stress0, karma0, happy0)

    def test_auto_effects_include_stress_karma_and_actual_deltas(self):
        event = {
            "id": "probe_stress", "title": "Worry spike", "text": "",
            "category": "adult", "weight": 1, "auto": True,
            "conditions": {"min_age": 0, "max_age": 200},
            "outcomes": [
                {"weight": 1,
                 "effects": {"stats": {"happiness": -4, "stress": 7,
                                       "karma": -1}},
                 "outcome_text": ""}
            ],
        }
        env, player, (stress0, _, _) = self._force_auto(event)
        entry = env["journal"][-1]
        # empty outcome text falls back to the event text/title
        self.assertTrue(entry["text"].strip())
        # actual deltas recorded, including stress and karma
        self.assertEqual(entry["changes"].get("happiness"), -4)
        self.assertEqual(entry["changes"].get("stress"), 7)
        self.assertEqual(entry["changes"].get("karma"), -1)
        self.assertEqual(player.stress, stress0 + 7)  # applied

    def test_auto_money_posts_through_ledger(self):
        event = {
            "id": "probe_cash", "title": "Refund", "text": "A refund.",
            "category": "adult", "weight": 1, "auto": True,
            "conditions": {"min_age": 0, "max_age": 200},
            "outcomes": [
                {"weight": 1, "effects": {"money": 250},
                 "outcome_text": "You got $250, {name}."}
            ],
        }
        env, player, _ = self._force_auto(event)
        self.assertEqual(player.money, 250)
        rows = [r for r in env["ledger"]
                if "probe_cash" in r["source_key"]]
        self.assertEqual(len(rows), 1)
        self.assertEqual(rows[0]["amount"], 250)
        self.assertEqual(rows[0]["balance_after"], 250)


class PreEffectsOfferTimeTests(unittest.TestCase):
    """Codex #4: event pre-effects apply ONCE at offer time (real deltas,
    persisted marker); the choice applies separately at resolve; reload
    before resolve cannot double-apply."""

    EVENT = {
        "id": "probe_pre", "title": "Bad news", "text": "Trouble, {name}.",
        "category": "adult", "weight": 1, "chance_n": 1,
        "conditions": {"min_age": 0, "max_age": 200},
        "effects": {"stats": {"happiness": -15}},
        "choices": [
            {"id": "do_nothing", "label": "Do nothing",
             "known_effects": ["-karma"],
             "effects": {"stats": {"karma": -15}},
             "outcome_text": "You did nothing, {name}."},
            {"id": "walk", "label": "Walk away",
             "effects": {}, "outcome_text": "You left."}
        ],
    }

    def _offer(self, svc, gid):
        import random as _random
        from src.game import coordinator, events as event_mod
        env = svc._load(gid)
        happy0 = env["player"].happiness
        karma0 = env["player"].karma
        journal0 = len(env["journal"])
        real_load = event_mod.load_event_files
        event_mod.load_event_files = lambda *a, **k: [self.EVENT]
        try:
            res = coordinator._decision_offer(env, None, svc,
                                              _random.Random(5))
        finally:
            event_mod.load_event_files = real_load
        # apply_cursor_update's contract: run_turn installs the pending
        env["pending"] = res["pending_decision"]
        svc._save(env)
        return env, happy0, karma0, journal0

    def test_pre_effects_apply_once_at_offer_and_survive_reload(self):
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game(dict(PROFILE), "r1")
        gid = st["game_id"]
        env, happy0, karma0, journal0 = self._offer(svc, gid)
        pending = env["pending"]
        self.assertIsNotNone(pending)
        # pre-effects applied at OFFER: happiness dropped once, journaled
        self.assertEqual(env["player"].happiness, max(0, happy0 - 15))
        self.assertTrue(pending.get("pre_applied"))
        pre_entry = env["journal"][-1]
        self.assertEqual(pre_entry["changes"].get("happiness"), -15)
        # resolve the choice: karma applies, happiness NOT double-hit
        st = svc.get_state(gid)
        d = st["pending_decision"]
        st = svc.command(gid, st["revision"], "pr1", "resolve_decision",
                         {"decision_id": d["id"], "option_id":
                          next(o["id"] for o in d["options"]
                               if "do_nothing" in o["id"])})
        env = svc._load(gid)
        self.assertEqual(env["player"].karma, max(0, karma0 - 15))
        self.assertEqual(env["player"].happiness, max(0, happy0 - 15))
        # reload-before-resolve durability was implicitly exercised above
        # (the marker persisted through the command's own save/reload)

    def test_no_pre_effects_no_marker(self):
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game(dict(PROFILE), "r2")
        gid = st["game_id"]
        event = dict(self.EVENT, id="probe_nopre", effects=None)
        real = dict(self.EVENT)
        import random as _random
        from src.game import coordinator, events as event_mod
        env = svc._load(gid)
        real_load = event_mod.load_event_files
        event_mod.load_event_files = lambda *a, **k: [dict(
            self.EVENT, id="probe_nopre", effects=None)]
        try:
            res = coordinator._decision_offer(env, None, svc,
                                              _random.Random(5))
        finally:
            event_mod.load_event_files = real_load
        self.assertIsNotNone(res["pending_decision"])
        self.assertNotIn("pre_applied", res["pending_decision"])


class FixRoundServiceLevelTests(unittest.TestCase):
    """Codex final blockers, verified through the REAL GameService."""

    WORRY = {
        "id": "probe_worry_auto", "title": "A worry", "text": "Worried.",
        "category": "adult", "weight": 1, "chance_n": 1, "auto": True,
        "conditions": {"min_age": 0, "max_age": 200},
        "outcomes": [
            {"weight": 1, "effects": {"stats": {"happiness": -2}},
             "outcome_text": "A worry passed, {name}."}
        ],
    }
    KIDNAP = {
        "id": "probe_kidnap_choice", "title": "Kidnapping",
        "text": "Your partner has been kidnapped, {name}.",
        "category": "adult", "weight": 1, "chance_n": 1,
        "conditions": {"min_age": 0, "max_age": 200},
        "effects": {"stats": {"happiness": -31}},
        "choices": [
            {"id": "pay", "label": "Pay the ransom",
             "known_effects": ["-money"],
             "effects": {"stats": {"karma": 5}},
             "outcome_text": "You paid, {name}."},
            {"id": "nothing", "label": "Do nothing",
             "known_effects": ["-karma"],
             "effects": {"stats": {"karma": -15}},
             "outcome_text": "You did nothing, {name}."}
        ],
    }

    def _two_event_year(self):
        import random as _random
        from src.game import events as event_mod
        svc = GameService(tempfile.mkdtemp())
        st = svc.new_game(dict(PROFILE), "r1")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 30
        env["player"].happiness = 76
        happy0 = env["player"].happiness
        svc._save(env)
        real_load = event_mod.load_event_files
        # worry (auto) is FIRST in legacy chance order -> drained in-phase;
        # kidnapping (choice) interrupts; BOTH must happen this year
        # stable legacy order: worry (auto) drains in-phase, kidnapping
        # (choice) then interrupts; both happen in the SAME year
        event_mod.load_event_files = lambda *a, **k: [
            dict(self.WORRY), dict(self.KIDNAP)]
        return svc, gid, happy0, real_load, event_mod

    def test_two_independent_events_one_year_real_service(self):
        svc, gid, happy0, real_load, event_mod = self._two_event_year()
        try:
            st = svc.command(gid, 1, "y1", "age_up", {})
        finally:
            event_mod.load_event_files = real_load
        self.assertEqual(st["status"], "alive")
        # the auto worry applied in-phase AND the choice event is offered
        self.assertIsNotNone(st["pending_decision"])
        self.assertEqual(st["pending_decision"].get("event_id"),
                         "probe_kidnap_choice")
        texts = [e["text"] for e in st["journal"]]
        self.assertTrue(any("worry" in t.lower() for t in texts))
        # pre-effect applied at offer: exactly one -31 EVENT row (the
        # year summary legitimately aggregates the same delta; the
        # year's own stat drift is separate and honest)
        env = svc._load(gid)
        pre_rows = [e for e in env["journal"]
                    if e["changes"].get("happiness") == -31
                    and e["kind"] != "year"]
        self.assertEqual(len(pre_rows), 1)
        # resolve: karma applies, happiness NOT double-hit
        d = st["pending_decision"]
        nothing = next(o for o in d["options"]
                       if "nothing" in o["id"])
        karma0 = env["player"].karma
        happy_at_offer = env["player"].happiness
        st = svc.command(gid, st["revision"], "r1", "resolve_decision",
                         {"decision_id": d["id"], "option_id": nothing["id"]})
        env = svc._load(gid)
        self.assertEqual(env["player"].karma, max(0, karma0 - 15))
        # resolving applies the choice only: happiness NOT double-hit
        self.assertEqual(env["player"].happiness, happy_at_offer)
        # the remaining queue (none here) drains and the year completes
        self.assertIsNone(env["pending"])
        self.assertEqual(env["turn"].get("cursor"),
                         {"phase_id": "year_start", "item_cursor": 0})

    def test_pre_effect_once_across_save_reload_real_service(self):
        import pickle as _pickle
        import os as _os
        svc, gid, happy0, real_load, event_mod = self._two_event_year()
        try:
            st = svc.command(gid, 1, "y1", "age_up", {})
        finally:
            event_mod.load_event_files = real_load
        d = st["pending_decision"]
        # RELOAD from disk before resolving (service restart)
        path = _os.path.join(svc.save_dir, gid + ".pickle")
        self.assertTrue(_os.path.isfile(path))
        svc2 = GameService(svc.save_dir)
        st = svc2.get_state(gid)
        self.assertEqual(st["pending_decision"]["id"], d["id"])
        env = svc2._load(gid)
        happy_at_offer = env["player"].happiness
        d = st["pending_decision"]
        st = svc2.command(gid, st["revision"], "r1", "resolve_decision",
                          {"decision_id": d["id"],
                           "option_id": d["options"][0]["id"]})
        env = svc2._load(gid)
        # still exactly one -31 EVENT row after reload + resolve, and
        # resolving changed nothing about happiness (choice had no stat)
        pre_rows = [e for e in env["journal"]
                    if e["changes"].get("happiness") == -31
                    and e["kind"] != "year"]
        self.assertEqual(len(pre_rows), 1)
        self.assertEqual(env["player"].happiness, happy_at_offer)
