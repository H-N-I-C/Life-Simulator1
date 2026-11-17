"""M2.5 weddings & marriage tests (Kimi A): plan/renew/divorce through
the real GameService with replay semantics, ledger single-posting,
selected-union divorce among plural spouses, prenup settlements,
anniversaries at year_start.

Run: python3 -m unittest tests.test_weddings
"""

import os
import sys
import tempfile
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.service import GameError, GameService  # noqa: E402
from src.people.classes.child import Child
from src.lifesim_lib.lifesim_lib import Gender


PROFILE = {"first_name": "Alex", "last_name": "Rivers",
           "gender": "Male", "country": "Australia", "seed": 31}


class WeddingCase(unittest.TestCase):
    def setUp(self):
        self.svc = GameService(tempfile.mkdtemp(prefix="lifesim-wed-"))
        self.st = self.svc.new_game(dict(PROFILE), "wed-create")
        self.gid = self.st["game_id"]
        env = self.svc._load(self.gid)
        env["player"].age = 30
        self.svc._save(env)
        st = self.svc.get_state(self.gid)
        self.adults = {p["role"]: p["id"] for p in st["people"]
                       if p.get("role") in ("mother", "father")}
        self.pid = st["player_id"]

    def _union(self, req, other, prenup=None):
        st = self.svc.get_state(self.gid)
        args = {"a_id": self.pid, "b_id": other, "kind": "marriage",
                "recognition": {"status": "recognized",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "registration"},
                "turn": 0}
        if prenup:
            args["prenup"] = prenup
        res = self.svc.graph_command(self.gid, st["revision"], req,
                                     "form_union", args)
        return res["result"]["ids"]["union_id"]

    def _give(self, amount):
        env = self.svc._load(self.gid)
        env["balances"][self.pid] = amount
        env["player"].money = amount
        self.svc._save(env)

    def _money(self):
        return self.svc._load(self.gid)["player"].money


class PlanWeddingTests(WeddingCase):
    def test_courthouse_wedding_is_free(self):
        uni = self._union("u1", self.adults["mother"])
        self._give(0)
        st = self.svc.get_state(self.gid)
        res = self.svc.wedding_command(
            self.gid, st["revision"], "w1", "plan_wedding",
            {"union_id": uni, "venue_id": "courthouse", "guests": 2,
             "outfit_id": "hand_me_down", "honeymoon_id": "none"})
        self.assertTrue(res["result"]["ok"])
        self.assertEqual(self._money(), 0)
        env = self.svc._load(self.gid)
        union = env["graph"]["unions"][0]
        self.assertTrue(union["ceremony"]["held"])
        self.assertEqual(union["ceremony"]["venue_id"], "courthouse")
        self.assertEqual(union["recognition"]["status"], "recognized")
        self.assertEqual(env["journal"][-1]["kind"], "wedding")

    def test_grand_wedding_charges_each_part_once(self):
        uni = self._union("u2", self.adults["mother"])
        self._give(20000)
        st = self.svc.get_state(self.gid)
        res = self.svc.wedding_command(
            self.gid, st["revision"], "w2", "plan_wedding",
            {"union_id": uni, "venue_id": "grand", "guests": 100,
             "outfit_id": "tailored", "honeymoon_id": "island"})
        self.assertTrue(res["result"]["ok"])
        # 6000 + 60*100 + 2500 + 3500 = 18000
        self.assertEqual(self._money(), 20000 - 18000)
        env = self.svc._load(self.gid)
        venue = [r for r in env["ledger"]
                 if "Wedding venue" in r["description"]]
        self.assertEqual(len(venue), 1)
        self.assertEqual(venue[0]["amount"], -12000)
        self.assertEqual({r["description"].split(" (")[0]
                          .replace("Wedding ", "") for r in
                          env["ledger"]},
                         {"venue", "outfit", "honeymoon"})
        # replay does not double-charge
        retry = self.svc.wedding_command(
            self.gid, st["revision"], "w2", "plan_wedding",
            {"union_id": uni, "venue_id": "grand", "guests": 100,
             "outfit_id": "tailored", "honeymoon_id": "island"})
        self.assertEqual(retry, res)
        self.assertEqual(self._money(), 20000 - 18000)

    def test_unaffordable_wedding_refused(self):
        uni = self._union("u3", self.adults["mother"])
        self._give(100)
        st = self.svc.get_state(self.gid)
        with self.assertRaises(GameError) as ctx:
            self.svc.wedding_command(
                self.gid, st["revision"], "w3", "plan_wedding",
                {"union_id": uni, "venue_id": "grand", "guests": 100,
                 "outfit_id": "tailored", "honeymoon_id": "island"})
        self.assertEqual(ctx.exception.code, "not_allowed")
        env = self.svc._load(self.gid)
        self.assertFalse(env["graph"]["unions"][0]["ceremony"]["held"])
        self.assertEqual(self._money(), 100)  # no partial charge

    def test_second_ceremony_for_same_union_refused(self):
        uni = self._union("u4", self.adults["mother"])
        self._give(0)
        st = self.svc.get_state(self.gid)
        self.svc.wedding_command(
            self.gid, st["revision"], "w4", "plan_wedding",
            {"union_id": uni, "venue_id": "courthouse", "guests": 0,
             "outfit_id": "hand_me_down", "honeymoon_id": "none"})
        with self.assertRaises(GameError) as ctx:
            self.svc.wedding_command(
                self.gid, self.svc.get_state(self.gid)["revision"], "w5",
                "plan_wedding",
                {"union_id": uni, "venue_id": "courthouse", "guests": 0,
                 "outfit_id": "hand_me_down", "honeymoon_id": "none"})
        self.assertEqual(ctx.exception.status, 409)


class RenewalAndAnniversaryTests(WeddingCase):
    def test_renewal_free_tier_and_paid_once(self):
        uni = self._union("u5", self.adults["mother"])
        self._give(1000)
        st = self.svc.get_state(self.gid)
        res = self.svc.wedding_command(
            self.gid, st["revision"], "v1", "renew_vows",
            {"union_id": uni, "tier": "backyard"})
        self.assertTrue(res["result"]["ok"])
        self.assertEqual(self._money(), 1000)  # free tier
        st = self.svc.get_state(self.gid)
        res = self.svc.wedding_command(
            self.gid, st["revision"], "v2", "renew_vows",
            {"union_id": uni, "tier": "dinner"})
        self.assertTrue(res["result"]["ok"])
        self.assertEqual(self._money(), 600)
        env = self.svc._load(self.gid)
        self.assertEqual(env["weddings"]["renewals"][uni], 2)

    def test_anniversaries_fire_once_at_year_start(self):
        uni = self._union("u6", self.adults["mother"])
        self._give(0)
        st = self.svc.get_state(self.gid)
        self.svc.wedding_command(
            self.gid, st["revision"], "a0", "plan_wedding",
            {"union_id": uni, "venue_id": "courthouse", "guests": 0,
             "outfit_id": "hand_me_down", "honeymoon_id": "none"})
        # four years pass; year-1 anniversary fires once, then year-5
        kinds = []
        st = self.svc.get_state(self.gid)
        for i in range(4):
            st = self.svc.command(self.gid, st["revision"], "ann%d" % i,
                                  "age_up", {})
            while st["pending_decision"]:
                free = [o for o in st["pending_decision"]["options"]
                        if o["enabled"]]
                d = st["pending_decision"]
                opt = next(o for o in free
                           if o["id"].endswith(free[-1]["id"]))
                st = self.svc.command(
                    self.gid, st["revision"], "ann%d-%s" % (i, d["id"]),
                    "resolve_decision",
                    {"decision_id": d["id"], "option_id": opt["id"]})
        # st["journal"] is the FULL journal: count once, from the end
        anniversaries = [e for e in st["journal"]
                         if e["text"].startswith("Wedding anniversary")]
        # exactly one anniversary journal (year 1); year 5 not reached
        self.assertEqual(len(anniversaries), 1)
        env = self.svc._load(self.gid)
        self.assertEqual(env["weddings"]["anniversaries"][uni], [1])


class SelectedDivorceTests(WeddingCase):
    def test_divorce_selected_union_only(self):
        uni_a = self._union("d-a", self.adults["mother"],
                            prenup={"asset": "cash", "amount": 5000,
                                    "terms_id": "pre-standard"})
        uni_b = self._union("d-b", self.adults["father"])
        self._give(10000)
        st = self.svc.get_state(self.gid)
        res = self.svc.wedding_command(
            self.gid, st["revision"], "dv1", "divorce_union",
            {"union_id": uni_a})
        self.assertTrue(res["result"]["ok"])
        self.assertEqual(self._money(), 5000)  # prenup settlement once
        env = self.svc._load(self.gid)
        statuses = {u["union_id"]: u["status"] for u in env["graph"]["unions"]}
        self.assertEqual(statuses[uni_a], "divorced")
        self.assertEqual(statuses[uni_b], "active")  # selected-edge only
        settle = [r for r in env["ledger"]
                  if "settlement" in r["description"] and r["person_id"] == self.pid]
        self.assertEqual(len(settle), 1)
        self.assertEqual(settle[0]["amount"], -5000)
        received = [r for r in env["ledger"] if r["amount"] == 5000
                    and r["person_id"] == self.adults["mother"]]
        self.assertEqual(len(received), 1)
        self.assertEqual(sum(r["amount"] for r in env["ledger"]), 0)
        # retry replays, no second settlement
        retry = self.svc.wedding_command(
            self.gid, st["revision"], "dv1", "divorce_union",
            {"union_id": uni_a})
        self.assertEqual(retry, res)
        self.assertEqual(self._money(), 5000)

    def test_no_prenup_no_settlement_but_children_supported(self):
        # a shared child via graph parentage to both spouses
        uni = self._union("d-c", self.adults["mother"])
        env = self.svc._load(self.gid)
        child = Child("Shared", "Rivers", Gender.Male, 50, 50)
        child.age = 8
        child_id = child.person_id
        env["player"].children.append(child)
        env["player"].relations.append(child)
        env["graph"]["parentage"].append({
            "child_id": child_id, "parent_id": self.pid,
            "role": "biological", "source": "birth",
            "confidence": "certain"})
        env["graph"]["parentage"].append({
            "child_id": child_id, "parent_id": self.adults["mother"],
            "role": "biological", "source": "birth",
            "confidence": "certain"})
        self.svc._save(env)
        self._give(3000)
        st = self.svc.get_state(self.gid)
        self.svc.wedding_command(
            self.gid, st["revision"], "dv2", "divorce_union",
            {"union_id": uni})
        env = self.svc._load(self.gid)
        self.assertEqual(self._money(), 1000)  # only child support
        support = [r for r in env["ledger"]
                   if "Child support (%s)" % child_id in r["description"]
                   and r["person_id"] == self.pid]
        self.assertEqual(len(support), 1)
        self.assertEqual(env["balances"][child_id], 2000)
        self.assertEqual(child.money, 0)  # the pre-transaction object is unchanged
        self.assertEqual(sum(r["amount"] for r in env["ledger"]), 0)
        household = env["graph"]["households"][0]
        self.assertEqual(household["dependent_ids"], [child_id])
        self.assertEqual(household["support_obligations"][0]["kind"],
                         "child_support")
        # parentage untouched
        bio = [(e["child_id"], e["parent_id"]) for e in
               env["graph"]["parentage"] if e["role"] == "biological"]
        self.assertIn((child_id, self.pid), bio)
        self.assertIn((child_id, self.adults["mother"]), bio)

    def test_divorce_never_blocked_by_recognition(self):
        st = self.svc.get_state(self.gid)
        args = {"a_id": self.pid, "b_id": self.adults["mother"],
                "kind": "marriage",
                "recognition": {"status": "forbidden",
                                "jurisdiction_id": "jur-neutral-default",
                                "law_version": 1, "basis": "ceremony"},
                "turn": 0}
        uni = self.svc.graph_command(self.gid, st["revision"], "d-d",
                                     "form_union", args)[
            "result"]["ids"]["union_id"]
        self._give(0)
        st = self.svc.get_state(self.gid)
        res = self.svc.wedding_command(
            self.gid, st["revision"], "dv3", "divorce_union",
            {"union_id": uni})
        self.assertTrue(res["result"]["ok"])
        env = self.svc._load(self.gid)
        self.assertEqual(env["graph"]["unions"][0]["status"], "divorced")


if __name__ == "__main__":
    unittest.main()
