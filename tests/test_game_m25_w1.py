"""W1 regression: two parent deaths in one turn must not collide on the
inheritance ledger occurrence key.

Review finding (M2.5-review.md, W1): both inheritances posted with the
bare occurrence "inheritance", so two parents leaving different amounts in
the same turn produced one posting key with different details ->
LedgerConflict -> HTTP 500 from age_up, save unchanged. Equal amounts
silently merged into one row (one inheritance lost). The rule entry now
keys the occurrence per deceased; these tests force the two-death year
through the real HTTP server and assert both postings, replay-idempotence
and no silent merge.

Run: python3 -m unittest tests.test_game_m25_w1
"""

import json
import random
import shutil
import tempfile
import threading
import unittest
import urllib.error
import urllib.request

import os
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.people.classes.parent import Parent  # noqa: E402
from src.game.finance import Ledger  # noqa: E402
import web.server as server  # noqa: E402


SEEDS = (0, 1, 42)


class W1TwoParentInheritanceTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-w1-")
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.port = cls.httpd.server_port
        cls.thread = threading.Thread(target=cls.httpd.serve_forever,
                                      daemon=True)
        cls.thread.start()

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir, ignore_errors=True)

    @staticmethod
    def req(port, method, path, body=None):
        data = None
        headers = {}
        if body is not None:
            data = json.dumps(body).encode("utf-8")
            headers["Content-Type"] = "application/json"
        request = urllib.request.Request(
            "http://127.0.0.1:%d%s" % (port, path), data=data,
            method=method, headers=headers)
        try:
            with urllib.request.urlopen(request) as res:
                return res.status, res.read().decode("utf-8")
        except urllib.error.HTTPError as e:
            return e.code, e.read().decode("utf-8")

    # -- helpers ------------------------------------------------------

    def _make_two_death_year(self, seed, parent_moneys, request_id):
        """One game where both parents die in the mortality phase of a
        single age_up. death_check is forced True for parents only; the
        seed varies the inheritance draws."""
        profile = {"first_name": "Sam", "last_name": "Lee", "gender": "Male",
                   "country": "Australia",
                   "appearance": {"schema_version": 1, "seed": 7,
                                  "skin_tone": "default"},
                   "request_id": request_id + "-create"}
        status, body = self.req(self.port, "POST", "/api/games", profile)
        self.assertEqual(status, 201, body)
        gid = json.loads(body)["game_id"]
        svc = self.httpd.service
        env = svc._load(gid)
        parents = [p for p in env["player"].relations
                   if isinstance(p, Parent)]
        self.assertEqual(len(parents), 2, "fixture needs two parents")
        for i, p in enumerate(parents):
            p.age = 130
            p.health = 100
            p.happiness = 100
            p.money = parent_moneys[i]
            p.generosity = 100  # always leaves inheritance
        env["rng_state"] = random.Random(seed).getstate()
        env["cosmetic_rng_state"] = random.Random(0).getstate()
        env["turn"] = {"turn_id": "t_two_parent_deaths",
                       "cursor": {"phase_id": "mortality",
                                  "item_cursor": 0},
                       "done": {}}
        svc._save(env)
        return gid, [p.person_id for p in parents]

    def _age_up(self, gid, request_id):
        state = json.loads(self.req(self.port, "GET", "/api/games/" + gid)[1])
        # Force the inheritance precondition (the 70% generosity roll in
        # C's rule entry) so BOTH estates always pay out; the amounts
        # stay seeded draws.
        from unittest.mock import patch
        with patch("src.menus.rule_actions._x_in_y", return_value=True):
            return self.req(self.port, "POST",
                            "/api/games/%s/actions" % gid,
                            {"expected_revision": state["revision"],
                             "request_id": request_id, "action": "age_up",
                             "payload": {}})

    def _inheritance_rows(self, gid, pid):
        env = self.httpd.service._load(gid)
        return [r for r in env["ledger"]
                if r["category"] == "inheritance"
                and r["person_id"] == pid], env

    # -- the regression ------------------------------------------------

    def test_two_parent_deaths_post_two_distinct_inheritances(self):
        # Different estates: the old bare occurrence key would collide
        # with DIFFERENT details (the traced 500).
        for seed in SEEDS:
            with self.subTest(seed=seed):
                gid, parent_ids = self._make_two_death_year(
                    seed, (600, 700), "w1-diff-%s" % seed)
                before = json.loads(
                    self.req(self.port, "GET", "/api/games/" + gid)[1])
                status, body = self._age_up(gid, "w1-diff-%s-age" % seed)
                self.assertEqual(status, 200, body)
                state = json.loads(
                    self.req(self.port, "GET", "/api/games/" + gid)[1])
                rows = [r for r in self._inheritance_rows(
                    gid, state["player_id"])[0]]
                self.assertEqual(len(rows), 2, rows)
                self.assertNotEqual(rows[0]["source_key"],
                                    rows[1]["source_key"])
                for parent_id, row in zip(parent_ids, rows):
                    self.assertIn(parent_id, row["source_key"])
                total = sum(r["amount"] for r in rows)
                self.assertEqual(rows[0]["amount"] > 0, True)
                self.assertEqual(rows[1]["amount"] > 0, True)
                self.assertNotEqual(rows[0]["amount"], rows[1]["amount"])
                # both inheritances landed exactly once and the ledger is
                # the single authority: balance and player.money agree
                env = self.httpd.service._load(gid)
                self.assertEqual(state["money"],
                                 before["money"] + total)
                self.assertEqual(env["balances"][state["player_id"]],
                                 env["player"].money)

    def test_equal_estates_do_not_silently_merge(self):
        # Equal estates: the old key would collide with SAME details and
        # silently drop one inheritance. Keys are per deceased, so both
        # rows always land even if the drawn amounts coincide.
        for seed in SEEDS:
            with self.subTest(seed=seed):
                gid, parent_ids = self._make_two_death_year(
                    seed, (500, 500), "w1-same-%s" % seed)
                before = json.loads(
                    self.req(self.port, "GET", "/api/games/" + gid)[1])
                status, body = self._age_up(gid, "w1-same-%s-age" % seed)
                self.assertEqual(status, 200, body)
                state = json.loads(
                    self.req(self.port, "GET", "/api/games/" + gid)[1])
                rows, env = self._inheritance_rows(gid, state["player_id"])
                self.assertEqual(len(rows), 2, rows)
                self.assertNotEqual(rows[0]["source_key"],
                                    rows[1]["source_key"])
                for parent_id, row in zip(parent_ids, rows):
                    self.assertIn(parent_id, row["source_key"])
                self.assertEqual(
                    state["money"] - before["money"],
                    sum(r["amount"] for r in rows))

    def test_replay_and_repost_are_idempotent(self):
        gid, parent_ids = self._make_two_death_year(3, (600, 700),
                                                    "w1-replay")
        before = json.loads(self.req(self.port, "GET",
                                     "/api/games/" + gid)[1])
        status, body = self._age_up(gid, "w1-replay-age")
        self.assertEqual(status, 200, body)
        state = json.loads(self.req(self.port, "GET", "/api/games/" + gid)[1])
        rows_before, env = self._inheritance_rows(gid, state["player_id"])
        money_before = env["player"].money

        # exact replay of the same request_id (with the revision the
        # original ran against) returns the stored response and never
        # re-executes: no second posting, no money delta
        status, again = self.req(
            self.port, "POST", "/api/games/%s/actions" % gid,
            {"expected_revision": before["revision"],
             "request_id": "w1-replay-age", "action": "age_up",
             "payload": {}})
        self.assertEqual(status, 200, again)
        self.assertEqual(json.loads(again), json.loads(body))
        rows_after, env = self._inheritance_rows(gid, state["player_id"])
        self.assertEqual(len(rows_after), len(rows_before))
        self.assertEqual(env["player"].money, money_before)

        # a phase re-run re-posting the SAME sampled entries through the
        # single authority is an exact no-op (same keys, same details)
        ledger = Ledger(env, resolver=self.httpd.service
                        ._make_resolver(env))
        entries = [{"phase": r["source_key"].split(":")[1],
                    "occurrence_id": ":".join(
                        r["source_key"].split(":")[2:-2]),
                    "account_owner_id": r["person_id"],
                    "leg": r["source_key"].split(":")[-1],
                    "category": r["category"], "amount": r["amount"],
                    "description": r["description"]}
                   for r in rows_before]
        ledger.post_many("t_two_parent_deaths", env["player"].age, entries)
        rows_final, env = self._inheritance_rows(gid, state["player_id"])
        self.assertEqual(len(rows_final), len(rows_before))
        self.assertEqual(env["player"].money, money_before)
        self.assertEqual(env["balances"][state["player_id"]],
                         env["player"].money)


if __name__ == "__main__":
    unittest.main()
