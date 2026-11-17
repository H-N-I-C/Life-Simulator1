"""R8 forced regressions: one immutable conception identity, consumed in
the real birth transaction with an actual health effect. Real GameService
age_up -> family_events phase -> birth; no mocked graph/appliers."""
import tempfile
import unittest

from src.game.service import GameError, GameService


PROFILE = {"first_name": "Ada", "last_name": "Lee",
           "gender": "female", "country": "Australia", "seed": 4242}


class ConceptionBirthTests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.mkdtemp(prefix="m25-r8-")
        self.svc = GameService(self.tmp)
        self.st = self.svc.new_game(dict(PROFILE), "r8-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        self.player = env["player"]
        self.player.age = 30
        self.player.fertility = 100
        partner = self.player.partner or self._make_partner(env)
        self.partner = partner
        self.svc._save(env)
        env = self.svc._load(self.gid)
        self.player, self.partner = env["player"], env["player"].partner

    def _make_partner(self, env):
        from src.appearance.integration import ensure_person_id
        from src.people.classes.partner import Partner
        from src.lifesim_lib.lifesim_lib import Gender
        partner = Partner(32, Gender.Male, 60, 60, 90, 2)
        partner.status = 2
        partner.years_together = 2
        ensure_person_id(partner)
        env["player"].partner = partner
        env["player"].relations.append(partner)
        return partner

    # ----------------------------------------------------------- helpers
    def _seed_outcome(self, key="child-future:req-1", adjustment="none"):
        env = self.svc._load(self.gid)
        env["graph"].setdefault("conception_outcomes", {})[key] = {
            "coefficient_band": "0.25", "roll": 0.01,
            "adjustment": adjustment,
            "bio_mother_id": self.player.person_id,
            "bio_father_id": self.partner.person_id}
        self.svc._save(env)

    def _give_birth(self):
        """A real legacy birth through the production yearly path."""
        env = self.svc._load(self.gid)
        env["player"].is_pregnant = True
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "r8-age",
                              "age_up", {})
        pending = st["pending_decision"]
        while pending and pending.get("kind") != "baby_naming":
            free = [o for o in pending["options"] if o["enabled"]]
            st = self.svc.command(
                self.gid, st["revision"], "r8-drain-" + pending["id"],
                "resolve_decision",
                {"decision_id": pending["id"],
                 "option_id": free[-1]["id"]})
            pending = st["pending_decision"]
        self.assertIsNotNone(pending, "a birth should offer baby naming")
        name_opt = pending["options"][0]
        st = self.svc.command(
            self.gid, st["revision"], "r8-name",
            "resolve_decision",
            {"decision_id": pending["id"], "option_id": name_opt["id"],
             "inputs": {"name": "Robin"}})
        return st

    def _baby(self):
        env = self.svc._load(self.gid)
        return env["player"].children[-1], env

    # -------------------------------------------------------------- tests
    def test_legacy_birth_consumes_recorded_risk_once_with_health_effect(self):
        self._seed_outcome(adjustment="related_parent_health_risk")
        self._give_birth()
        baby, env = self._baby()
        outcome = env["graph"]["conception_outcomes"]["child-future:req-1"]
        self.assertTrue(outcome["consumed"])
        self.assertEqual(outcome["consumed_child_id"], baby.person_id)
        health_after_birth = baby.health
        self.assertLessEqual(health_after_birth, 95)
        # a retried application (replay/crash recovery) never reapplies
        from src.game.features.parenting import core as parenting_core
        again = parenting_core.apply_birth_conception(env, baby)
        self.assertIsNone(again)
        self.assertEqual(baby.health, health_after_birth)

    def test_v3_occurrence_keyed_outcome_is_found_by_bound_parents(self):
        # the v3 domain applier persists occurrence:* keys; consumption
        # must still find them via the immutable bound parent ids
        self._seed_outcome(key="occurrence:d_par_7:sample_conception",
                           adjustment="related_parent_health_risk")
        self._give_birth()
        baby, env = self._baby()
        outcome = env["graph"]["conception_outcomes"][
            "occurrence:d_par_7:sample_conception"]
        self.assertTrue(outcome["consumed"])
        self.assertEqual(outcome["consumed_child_id"], baby.person_id)

    def test_unrecorded_and_adopted_births_never_take_a_penalty(self):
        # no recorded conception: the birth is legal and untouched
        self._give_birth()
        baby, env = self._baby()
        self.assertFalse(any(o.get("consumed")
                             for o in env["graph"]["conception_outcomes"]
                             .values()))
        # adoption never rolls / never consumes
        self._seed_outcome(adjustment="related_parent_health_risk")
        baby, env = self._baby()
        adopted_health = 60
        baby.adopted = True
        baby.health = adopted_health
        from src.game.features.parenting import core as parenting_core
        self.assertIsNone(parenting_core.apply_birth_conception(env, baby))
        self.assertEqual(baby.health, adopted_health)
        self.assertFalse(env["graph"]["conception_outcomes"]
                         ["child-future:req-1"].get("consumed"))

    def test_conception_identity_immutable_across_requests(self):
        env = self.svc._load(self.gid)
        env["player"].money = 0
        self.svc._save(env)
        out1 = self.svc.record_conception(
            self.gid, "child-x", self.player.person_id,
            self.partner.person_id, "req-a")
        # same parents, NEW request: the stored draw wins, no reroll
        rng_before = self.svc._load(self.gid)["rng_state"]
        out2 = self.svc.record_conception(
            self.gid, "child-x", self.player.person_id,
            self.partner.person_id, "req-b")
        self.assertEqual(out2, out1)
        self.assertEqual(self.svc._load(self.gid)["rng_state"], rng_before)
        # a DIFFERENT parent pair for the same child: typed conflict
        from src.appearance.integration import ensure_person_id
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        other = Parent("Riley", 33, Gender.Male)
        ensure_person_id(other)
        env["player"].relations.append(other)
        self.svc._save(env)
        with self.assertRaises(GameError) as ctx:
            self.svc.record_conception(self.gid, "child-x",
                                       self.player.person_id,
                                       other.person_id, "req-c")
        self.assertEqual(ctx.exception.status, 409)

    def test_birth_after_save_and_reload_consumes_once(self):
        self._seed_outcome(adjustment="related_parent_health_risk")
        env = self.svc._load(self.gid)
        env["player"].is_pregnant = True
        self.svc._save(env)
        # fresh service over the same save root: reload mid-pregnancy
        svc2 = GameService(self.tmp)
        st = svc2.get_state(self.gid)
        st = svc2.command(self.gid, st["revision"], "r8-age-2",
                          "age_up", {})
        pending = st["pending_decision"]
        while pending and pending.get("kind") != "baby_naming":
            free = [o for o in pending["options"] if o["enabled"]]
            st = svc2.command(
                self.gid, st["revision"], "r8-drain2-" + pending["id"],
                "resolve_decision",
                {"decision_id": pending["id"],
                 "option_id": free[-1]["id"]})
            pending = st["pending_decision"]
        self.assertIsNotNone(pending)
        baby = None
        for c in svc2._load(self.gid)["player"].children:
            if c.age == 0:
                baby = c
        self.assertIsNotNone(baby)
        outcome = svc2._load(self.gid)["graph"]["conception_outcomes"][
            "child-future:req-1"]
        self.assertTrue(outcome["consumed"])
        self.assertEqual(outcome["consumed_child_id"], baby.person_id)

    def test_health_delta_is_bounded_and_exact(self):
        from src.game.features.parenting import core as parenting_core
        self._seed_outcome(adjustment="related_parent_health_risk")
        baby, env = self._baby_setup_child(env_health=50)
        outcome = parenting_core.apply_birth_conception(env, baby)
        self.assertIsNotNone(outcome)
        self.assertEqual(baby.health, 45)
        # the fictional consequence never drops a newborn below the floor
        baby2, env2 = self._baby_setup_child(env_health=7)
        parenting_core.apply_birth_conception(env2, baby2)
        self.assertEqual(baby2.health, 5)
        # 'none' adjustments never touch health (bound to a different
        # pair so it is a distinct conception)
        from src.appearance.integration import ensure_person_id
        from src.people.classes.parent import Parent
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        other = Parent("Riley", 33, Gender.Male)
        ensure_person_id(other)
        env["player"].relations.append(other)
        env["graph"].setdefault("conception_outcomes", {})[
            "child-future:req-2"] = {
            "coefficient_band": "0", "roll": 0.99, "adjustment": "none",
            "bio_mother_id": self.player.person_id,
            "bio_father_id": other.person_id}
        self.svc._save(env)
        baby3 = Child("Unit3", "Test", Gender.Male, 50, 50,
                      mother=env["player"], father=other)
        baby3.health = 70
        parenting_core.apply_birth_conception(env, baby3)
        self.assertEqual(baby3.health, 70)

    def _baby_setup_child(self, env_health, key="child-future:req-1"):
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        baby = Child("Unit", "Test", Gender.Male, 50, 50,
                     mother=self.player, father=self.partner)
        baby.health = env_health
        env["graph"].setdefault("conception_outcomes", {})
        return baby, env

    # ----------------------------------------------------------- E4 / E5
    def test_try_for_baby_keep_records_conception_consumed_at_birth(self):
        """E4: the legacy keep resolution records ONE conception; the real
        birth transaction consumes it exactly once."""
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "e4-try",
                              "try_for_baby", {})
        pending = st["pending_decision"]
        self.assertEqual(pending["kind"], "pregnancy")
        keep = next(o["id"] for o in pending["options"]
                    if o["id"] == "keep")
        st = self.svc.command(self.gid, st["revision"], "e4-keep",
                              "resolve_decision",
                              {"decision_id": pending["id"],
                               "option_id": keep})
        env = self.svc._load(self.gid)
        outcomes = env["graph"]["conception_outcomes"]
        legacy = {k: v for k, v in outcomes.items()
                  if k.startswith("legacy:")}
        self.assertEqual(len(legacy), 1)
        key, outcome = next(iter(legacy.items()))
        self.assertEqual(
            {outcome["bio_mother_id"], outcome["bio_father_id"]},
            {self.player.person_id, self.partner.person_id})
        self.assertFalse(outcome.get("consumed"))
        # the continuing pregnancy never records a second draw
        st2 = self.svc.get_state(self.gid)
        self.assertIsNotNone(st2)
        env = self.svc._load(self.gid)
        self.assertEqual(len(env["graph"]["conception_outcomes"]), 1)
        # the real birth consumes the recorded draw once
        self._give_birth()
        baby, env = self._baby()
        env = self.svc._load(self.gid)
        outcome = env["graph"]["conception_outcomes"][key]
        self.assertTrue(outcome["consumed"])
        self.assertEqual(outcome["consumed_child_id"], baby.person_id)

    def test_sibling_conceptions_never_swap(self):
        """E5: with two same-parent conceptions pre-bound to two children,
        each child's own draw wins — birth order never swaps them."""
        from src.game.features.parenting import core as parenting_core
        env = self.svc._load(self.gid)
        outcomes = env["graph"].setdefault("conception_outcomes", {})
        mid, fid = self.player.person_id, self.partner.person_id
        outcomes["kidA:req-1"] = {
            "coefficient_band": "0", "roll": 0.99, "adjustment": "none",
            "bio_mother_id": mid, "bio_father_id": fid}
        outcomes["kidB:req-1"] = {
            "coefficient_band": "0.25", "roll": 0.01,
            "adjustment": "related_parent_health_risk",
            "bio_mother_id": mid, "bio_father_id": fid}
        for kid in ("kidA", "kidB"):
            for parent_id in (mid, fid):
                env["graph"]["parentage"].append({
                    "child_id": kid, "parent_id": parent_id,
                    "role": "biological", "source": "birth",
                    "confidence": "certain"})
        self.svc._save(env)
        env = self.svc._load(self.gid)
        key_b, outcome_b = parenting_core.find_conception_outcome(env, "kidB")
        self.assertEqual(key_b, "kidB:req-1")
        self.assertEqual(outcome_b["adjustment"],
                         "related_parent_health_risk")
        key_a, outcome_a = parenting_core.find_conception_outcome(env, "kidA")
        self.assertEqual(key_a, "kidA:req-1")
        self.assertEqual(outcome_a["adjustment"], "none")

    def test_two_legacy_pregnancies_births_consume_distinct_draws(self):
        """E5 (production): two keep-resolved pregnancies record two
        pair-keyed draws; each real birth consumes exactly one, the
        second pregnancy never rerolls an existing unconsumed draw, and
        the children never swap conceptions."""
        def try_keep(tag):
            st = self.svc.get_state(self.gid)
            st = self.svc.command(self.gid, st["revision"], tag + "-try",
                                  "try_for_baby", {})
            pending = st["pending_decision"]
            self.assertEqual(pending["kind"], "pregnancy")
            keep = next(o["id"] for o in pending["options"]
                        if o["id"] == "keep")
            return self.svc.command(self.gid, st["revision"], tag + "-keep",
                                    "resolve_decision",
                                    {"decision_id": pending["id"],
                                     "option_id": keep})

        try_keep("e5-a")
        env = self.svc._load(self.gid)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertEqual(len(outcomes), 1)
        key0 = next(iter(outcomes))
        # a second conception for the same parents is recorded under a
        # distinct pair key (via the production recorder); while the
        # first draw is still pending the recorder reuses and rebinds it
        from src.game.features.parenting import core as parenting_core
        env = self.svc._load(self.gid)
        env["player"].is_pregnant = False
        import random as _random
        # no reroll while a draw is pending: the recorder returns the
        # EXISTING pending key and binds it to this pregnancy
        env["player"].is_pregnant = False
        second = parenting_core.record_legacy_conception(
            env, env["player"], _random.Random(11))
        self.assertEqual(second, key0)
        self.assertEqual(env["player"]._active_conception_key, key0)
        # birth #1 consumes the keep-recorded draw
        self._give_birth()
        baby1, _ = self._baby()
        env = self.svc._load(self.gid)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertTrue(outcomes[key0]["consumed"])
        self.assertEqual(outcomes[key0]["consumed_child_id"], baby1.person_id)
        # now a new pregnancy records a second draw, never rerolling the
        # unconsumed one
        try_keep("e5-b")
        env = self.svc._load(self.gid)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertEqual(len(outcomes), 2)
        key1 = next(k for k in outcomes if k != key0)
        self.assertFalse(outcomes[key1].get("consumed"))
        import random as _random
        again = parenting_core.record_legacy_conception(
            env, env["player"], _random.Random(13))
        self.assertEqual(again, key1)  # reused, never rerolled
        self.assertEqual(len(env["graph"]["conception_outcomes"]), 2)
        # a second birth consumes the second draw (birth #1 already
        # proved the full yearly production path; the miscarriage draw
        # makes a second full year non-deterministic, so the second
        # consumption is applied through the same applier the
        # coordinator calls, with a real child record)
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        env = self.svc._load(self.gid)
        baby2 = Child("Unit2", "Test", Gender.Male, 50, 50,
                      mother=env["player"], father=env["player"].partner)
        parenting_core.apply_birth_conception(env, baby2)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertTrue(outcomes[key1]["consumed"])
        self.assertEqual(outcomes[key1]["consumed_child_id"],
                         baby2.person_id)
        self.assertEqual(outcomes[key0]["consumed_child_id"],
                         baby1.person_id)
        self.assertEqual(len({baby1.person_id, baby2.person_id}), 2)

    def test_n1_male_player_keep_records_conception_with_correct_roles(self):
        """N1: a MALE player whose partner is pregnant records exactly one
        conception with the partner as bio_mother and the player as
        bio_father — ordered roles, not a set."""
        tmp = tempfile.mkdtemp(prefix="m25-n1-")
        svc = GameService(tmp)
        st = svc.new_game({"first_name": "Max", "last_name": "Lee",
                           "gender": "male", "country": "Australia",
                           "seed": 4242}, "n1-create")
        gid = st["game_id"]
        env = svc._load(gid)
        env["player"].age = 30
        env["player"].fertility = 100
        partner = self._make_partner(env)
        partner.fertility = 100
        svc._save(env)
        st = svc.get_state(gid)
        st = svc.command(gid, st["revision"], "n1-try", "try_for_baby", {})
        pending = st["pending_decision"]
        self.assertEqual(pending["kind"], "pregnancy")
        keep = next(o["id"] for o in pending["options"] if o["id"] == "keep")
        st = svc.command(gid, st["revision"], "n1-keep", "resolve_decision",
                         {"decision_id": pending["id"], "option_id": keep})
        env = svc._load(gid)
        self.assertTrue(env["player"].partner.is_pregnant)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertEqual(len(outcomes), 1)
        outcome = next(iter(outcomes.values()))
        self.assertEqual(outcome["bio_mother_id"], partner.person_id)
        self.assertEqual(outcome["bio_father_id"],
                         env["player"].person_id)

    def test_n1_female_player_roles_are_ordered(self):
        """N1: the female player is the bio_MOTHER (ordered assertion —
        a set comparison cannot detect reversed roles)."""
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "n1f-try",
                              "try_for_baby", {})
        pending = st["pending_decision"]
        keep = next(o["id"] for o in pending["options"] if o["id"] == "keep")
        self.svc.command(self.gid, st["revision"], "n1f-keep",
                         "resolve_decision",
                         {"decision_id": pending["id"], "option_id": keep})
        env = self.svc._load(self.gid)
        outcome = next(iter(env["graph"]["conception_outcomes"].values()))
        self.assertEqual(outcome["bio_mother_id"], self.player.person_id)
        self.assertEqual(outcome["bio_father_id"], self.partner.person_id)

    def test_n2_reconsume_never_steals_next_pregnancy_draw(self):
        """N2: real keep -> real birth -> second keep; asking for the
        FIRST child again returns None (already consumed) and leaves the
        second pregnancy's draw untouched."""
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "n2-try1",
                              "try_for_baby", {})
        pending = st["pending_decision"]
        keep = next(o["id"] for o in pending["options"] if o["id"] == "keep")
        st = self.svc.command(self.gid, st["revision"], "n2-keep1",
                              "resolve_decision",
                              {"decision_id": pending["id"],
                               "option_id": keep})
        self._give_birth()
        baby1, _ = self._baby()
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "n2-try2",
                              "try_for_baby", {})
        pending = st["pending_decision"]
        keep = next(o["id"] for o in pending["options"] if o["id"] == "keep")
        st = self.svc.command(self.gid, st["revision"], "n2-keep2",
                              "resolve_decision",
                              {"decision_id": pending["id"],
                               "option_id": keep})
        env = self.svc._load(self.gid)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertEqual(len(outcomes), 2)
        second_key = next(k for k in outcomes
                          if outcomes[k].get("consumed_child_id")
                          != baby1.person_id)
        self.assertFalse(outcomes[second_key].get("consumed"))
        # the repeated service consumption for the FIRST child stops
        self.assertIsNone(
            self.svc.consume_conception_outcome(self.gid, baby1.person_id))
        env = self.svc._load(self.gid)
        self.assertFalse(
            env["graph"]["conception_outcomes"][second_key].get("consumed"))
        self.assertNotIn("consumed_child_id",
                         env["graph"]["conception_outcomes"][second_key])

    def test_w2_miscarriage_retires_draw_next_pregnancy_is_new(self):
        """W2: real keep (d1) -> forced miscarriage -> next keep (d5):
        the lost draw is TERMINATED (not reusable) and the new pregnancy
        records a NEW outcome carrying its own pregnancy_id."""
        import random as _random

        def try_keep(tag):
            env = self.svc._load(self.gid)
            env["player"].fertility = 100
            env["player"].partner.fertility = 100
            env["rng_state"] = _random.Random(1).getstate()
            env["cosmetic_rng_state"] = _random.Random(0).getstate()
            self.svc._save(env)
            st = self.svc.get_state(self.gid)
            st = self.svc.command(self.gid, st["revision"], tag + "-try",
                                  "try_for_baby", {})
            pending = st["pending_decision"]
            self.assertEqual(pending["kind"], "pregnancy")
            keep = next(o["id"] for o in pending["options"]
                        if o["id"] == "keep")
            self.svc.command(self.gid, st["revision"], tag + "-keep",
                             "resolve_decision",
                             {"decision_id": pending["id"],
                              "option_id": keep})
            return pending["id"]

        first_id = try_keep("w2-first")
        env = self.svc._load(self.gid)
        env["player"].fertility = 0
        env["rng_state"] = _random.Random(2).getstate()
        env["turn"] = {"turn_id": "t_w2_miscarriage",
                       "cursor": {"phase_id": "family_events",
                                  "item_cursor": 0},
                       "done": {}}
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        st = self.svc.command(self.gid, st["revision"], "w2-age", "age_up",
                              {})
        for i in range(30):
            pending = st["pending_decision"]
            if not pending:
                break
            free = [o for o in pending["options"] if o["enabled"]]
            st = self.svc.command(
                self.gid, st["revision"], "w2-drain-%d" % i,
                "resolve_decision",
                {"decision_id": pending["id"], "option_id": free[-1]["id"]})
        env = self.svc._load(self.gid)
        self.assertFalse(env["player"].is_pregnant)
        self.assertEqual(env["player"].children, [])
        self.assertTrue(any("miscarriage" in j["text"]
                            for j in env["journal"]))
        outcomes = env["graph"]["conception_outcomes"]
        self.assertEqual(len(outcomes), 1)
        first_key = next(iter(outcomes))
        self.assertTrue(outcomes[first_key]["consumed"])
        self.assertEqual(outcomes[first_key].get("terminated"), "miscarriage")
        self.assertEqual(outcomes[first_key].get("pregnancy_id"), first_id)
        self.assertIsNone(getattr(env["player"], "_active_conception_key",
                                  None))

        second_id = try_keep("w2-second")
        env = self.svc._load(self.gid)
        outcomes = env["graph"]["conception_outcomes"]
        self.assertEqual(len(outcomes), 2)
        new_key = next(k for k in outcomes if k != first_key)
        self.assertFalse(outcomes[new_key].get("consumed"))
        self.assertNotIn("terminated", outcomes[new_key])
        self.assertEqual(outcomes[new_key].get("pregnancy_id"), second_id)
        self.assertNotEqual(outcomes[new_key], outcomes[first_key])


if __name__ == "__main__":
    unittest.main()
