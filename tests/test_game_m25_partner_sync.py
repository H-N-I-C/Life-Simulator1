"""R5 (A half) forced regressions: the legacy single-partner view derives
from the graph authority. Graph divorce/dissolve must not leave a stale
player.partner; pure-legacy partners (no graph edges) stay untouched.
Real GameService transactions throughout."""
import tempfile
import unittest

from src.game.service import GameService


PROFILE = {"first_name": "Ada", "last_name": "Lee",
           "gender": "female", "country": "Australia", "seed": 7}

REC = {"status": "recognized", "jurisdiction_id": "jur-r5",
       "law_version": 1, "basis": "ceremony"}


class LegacyPartnerSyncTests(unittest.TestCase):
    def setUp(self):
        from src.appearance.integration import ensure_person_id
        from src.people.classes.partner import Partner
        from src.lifesim_lib.lifesim_lib import Gender
        self.tmp = tempfile.mkdtemp(prefix="m25-r5-")
        self.svc = GameService(self.tmp)
        self.st = self.svc.new_game(dict(PROFILE), "r5-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        self.spouses = []
        for name in ("Jordan", "Riley"):
            p = Partner(32, Gender.Male, 60, 60, 90, 2)
            p.firstname = name
            ensure_person_id(p)
            env["player"].relations.append(p)
            self.spouses.append(p)
        self.svc._save(env)
        env = self.svc._load(self.gid)
        self.pid = env["player"].person_id
        self.s1, self.s2 = (p.person_id for p in self.spouses)

    def _form_union(self, other_id, req):
        st = self.svc.get_state(self.gid)
        return self.svc.graph_command(
            self.gid, st["revision"], req, "form_union",
            {"a_id": self.pid, "b_id": other_id, "kind": "marriage",
             "recognition": dict(REC), "turn": 0})["result"]["ids"]["union_id"]

    def _spouse_object(self, env, spouse_id):
        return next(p for p in env["player"].relations
                    if p.person_id == spouse_id)

    def _union_ids(self):
        return [u["union_id"] for u in self.svc._load(self.gid)["graph"]["unions"]]

    # -------------------------------------------------------------- tests
    def test_divorce_of_only_union_clears_legacy_partner(self):
        uid = self._form_union(self.s1, "r5-u1")
        env = self.svc._load(self.gid)
        env["player"].partner = self._spouse_object(env, self.s1)
        env["player"].partner.status = 2
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        self.svc.wedding_command(self.gid, st["revision"], "r5-div",
                                 "divorce_union", {"union_id": uid})
        env = self.svc._load(self.gid)
        self.assertIsNone(env["player"].partner)
        self.assertEqual(env["graph"]["unions"][0]["status"], "divorced")

    def test_selected_divorce_keeps_other_union_and_resyncs_partner(self):
        uid1 = self._form_union(self.s1, "r5-u1")
        self._form_union(self.s2, "r5-u2")
        env = self.svc._load(self.gid)
        env["player"].partner = self._spouse_object(env, self.s1)
        env["player"].partner.status = 2
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        self.svc.wedding_command(self.gid, st["revision"], "r5-div1",
                                 "divorce_union", {"union_id": uid1})
        env = self.svc._load(self.gid)
        # the legacy view follows the graph: spouse 2 is the partner now
        self.assertIsNotNone(env["player"].partner)
        self.assertEqual(env["player"].partner.person_id, self.s2)
        self.assertEqual(env["player"].partner.status, 2)
        # the other union and the people are untouched
        active = [u for u in env["graph"]["unions"]
                  if u["status"] == "active"]
        self.assertEqual(len(active), 1)
        self.assertEqual(set(active[0]["person_ids"]), {self.pid, self.s2})

    def test_graph_divorce_via_command_resyncs_immediately(self):
        uid = self._form_union(self.s1, "r5-u1")
        env = self.svc._load(self.gid)
        env["player"].partner = self._spouse_object(env, self.s1)
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        self.svc.graph_command(self.gid, st["revision"], "r5-end",
                               "end_union",
                               {"union_id": uid, "how": "divorce",
                                "turn": st["revision"]})
        env = self.svc._load(self.gid)
        self.assertIsNone(env["player"].partner)

    def test_pure_legacy_partner_without_graph_edges_is_untouched(self):
        # a legacy-only partner (dating, no graph edge) must NOT be
        # nulled by the graph authority — legacy stays a separate
        # authority until an edge exists
        env = self.svc._load(self.gid)
        partner = self._spouse_object(env, self.s1)
        env["player"].partner = partner
        partner.status = 0
        self.svc._save(env)
        self.svc.get_state(self.gid)  # triggers load-time sync
        env = self.svc._load(self.gid)
        self.assertEqual(env["player"].partner.person_id,
                         partner.person_id)

    def test_scenario_married_partner_migrates_to_union(self):
        # E3: a scenario game whose FIRST load predated the family (the
        # marker was stamped while the player was still a child) must
        # still migrate the married partner: Weddings is populated and
        # the spouse is never a dating-app stranger.
        st = self.svc.new_game(
            dict(PROFILE, scenario="married_30"), "r5-scenario-married")
        gid = st["game_id"]
        env = self.svc._load(gid)
        pid = env["player"].person_id
        spouse_id = env["player"].partner.person_id
        unions = [u for u in env["graph"]["unions"]
                  if pid in u["person_ids"] and u["status"] == "active"]
        self.assertEqual(len(unions), 1, "the marriage must migrate")
        self.assertEqual(unions[0]["kind"], "marriage")
        self.assertTrue(any(pid in r["person_ids"]
                            and spouse_id in r["person_ids"]
                            and r["status"] == "active"
                            for r in env["graph"]["relationships"]))
        # the spouse is not a stranger for the dating predicate
        from src.game import events as event_mod
        from src.game.context import RuleContext
        ctx = RuleContext(env, self.svc)
        found = event_mod.PREDICATES["adult_non_related_stranger"](
            pid, ctx)
        self.assertNotEqual(found, spouse_id)
        # idempotent: a second load changes nothing
        again = self.svc._load(gid)
        self.assertEqual(len([u for u in again["graph"]["unions"]
                              if pid in u["person_ids"]]), 1)

    def test_n3_graph_divorce_downgrades_legacy_married_view(self):
        # N3: real married_30 -> public graph divorce -> a FRESH service
        # must not present a married partner or the legacy Divorce action
        st = self.svc.new_game(
            dict(PROFILE, scenario="married_30"), "r5-scenario-n3")
        gid = st["game_id"]
        env = self.svc._load(gid)
        pid = env["player"].person_id
        union = next(u for u in env["graph"]["unions"]
                     if pid in u["person_ids"] and u["status"] == "active")
        st = self.svc.get_state(gid)
        self.svc.wedding_command(gid, st["revision"], "n3-div",
                                 "divorce_union",
                                 {"union_id": union["union_id"]})
        # fresh GameService (reload from disk), exactly like the probe:
        # no married presentation and no legacy Divorce action; the
        # surviving committed edge keeps them DATING (status 0), not
        # married
        fresh = GameService(self.tmp)
        state = fresh.get_state(gid)
        self.assertNotEqual(state["family"]["partner_status"], 2)
        env = fresh._load(gid)
        if state["family"]["partner"] is not None:
            self.assertEqual(env["player"].partner.status, 0)
        divorce = [a for a in state["available_actions"]
                   if a["id"] == "divorce"]
        self.assertEqual(divorce, [])
        self.assertEqual(env["graph"]["unions"][0]["status"], "divorced")

    def test_union_without_legacy_partner_gains_projection(self):
        # forming the union already projects onto the legacy view at
        # graph_command time
        self._form_union(self.s1, "r5-u1")
        env = self.svc._load(self.gid)
        self.assertIsNotNone(env["player"].partner)
        # and a save wiped back to None is repaired through the real
        # load path (_read_save performs the sync and persists it)
        env["player"].partner = None
        self.svc._save(env)
        raw = self.svc._read_save(self.svc._path(self.gid))
        self.assertIsNotNone(raw["player"].partner)
        self.assertEqual(raw["player"].partner.person_id, self.s1)
        self.assertEqual(raw["player"].partner.status, 2)


if __name__ == "__main__":
    unittest.main()
