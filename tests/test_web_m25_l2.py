"""Q2 route tests: the wedding planner consumes A's stored ceremony-fine
quote — no web-side forecasting, every fine line shown, preview total ==
posted total.

A's published contract (contracts/README.md, "A → B Q1 ceremony-fine
quote", 2026-11-18): `GameService.wedding_quote(game_id, union_id)`
samples variable ranges ONCE, stores the quote on the wedding plan, never
redraws while the law is unchanged, returns ALL fine lines
`{"union_id", "status", ..., "lines": [{occurrence_id, category, amount,
leg, description}], "total"}`, and `plan_wedding` posts EXACTLY the stored
quote lines. The web route is POST (sampling + persistence is a mutation).

Covered here with forced law configs through the real editor→service→HTTP
path: a nonconstant range, multiple simultaneous fines, stored-once
stability, and money reconciliation (preview total == posted total).

Run: python3 -m unittest tests.test_web_m25_l2
"""

import json
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

import web.server as server  # noqa: E402


def make_profile(**overrides):
    profile = {
        "first_name": "Sam", "last_name": "Lee", "gender": "Male",
        "country": "Australia",
        "appearance": {"schema_version": 1, "seed": 7, "skin_tone": "default"},
    }
    profile.update(overrides)
    return profile


class Q2WeddingQuoteTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(prefix="lifesim-q2-")
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

    def req(self, method, path, body=None):
        data = None
        headers = {}
        if body is not None:
            data = json.dumps(body).encode("utf-8")
            headers["Content-Type"] = "application/json"
        request = urllib.request.Request(
            "http://127.0.0.1:%d%s" % (self.port, path), data=data,
            method=method, headers=headers)
        try:
            with urllib.request.urlopen(request) as res:
                return res.status, res.read().decode("utf-8")
        except urllib.error.HTTPError as e:
            return e.code, e.read().decode("utf-8")

    # -- fixture ------------------------------------------------------

    def _union_under_law(self, request_id, consequences):
        """married_30 game whose policy jurisdiction carries the given
        ceremony-fine consequences (forbidden recognition); the union is
        re-formed under that jurisdiction so the law applies to it."""
        status, body = self.req(
            "POST", "/api/games",
            dict(make_profile(scenario="married_30"),
                 request_id=request_id + "-create"))
        self.assertEqual(status, 201, body)
        gid = json.loads(body)["game_id"]
        env = self.httpd.service._load(gid)
        jur = env["policy"]["jurisdiction_id"]
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        self.httpd.service.editor_apply(
            gid, state["revision"], request_id + "-law",
            [{"name": "edit_jurisdiction", "scope": "jurisdiction",
              "target_id": jur,
              "patch": {
                  "recognition": {"monogamous_civil": "forbidden",
                                  "plural_civil": "forbidden"},
                  "consequences": consequences}}])
        union = self._view_union(gid)
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        svc = self.httpd.service
        svc.graph_command(
            gid, state["revision"], request_id + "-end", "end_union",
            {"union_id": union["union_id"], "how": "divorce",
             "turn": state["revision"]})
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        svc.graph_command(
            gid, state["revision"], request_id + "-form", "form_union",
            {"a_id": state["player_id"], "b_id": union["partner"]["id"],
             "kind": "marriage",
             "recognition": {"status": "forbidden",
                             "jurisdiction_id": jur,
                             "law_version": 1, "basis": "ceremony"},
             "turn": state["revision"]})
        return gid, self._view_union(gid)["union_id"]

    def _view_union(self, gid):
        _, body = self.req(
            "GET", "/api/games/%s/relationships-view" % gid)
        unions = json.loads(body)["unions"]
        return next(u for u in unions if u["status"] == "active")

    def _quote(self, gid, union_id):
        status, body = self.req(
            "POST", "/api/games/%s/wedding-quote" % gid,
            {"union_id": union_id})
        self.assertEqual(status, 200, body)
        return json.loads(body)

    def _plan_free_wedding(self, gid, union_id, request_id):
        state = json.loads(self.req("GET", "/api/games/" + gid)[1])
        status, body = self.req(
            "POST", "/api/games/%s/wedding" % gid,
            {"op": "plan_wedding",
             "args": {"union_id": union_id, "venue_id": "courthouse",
                      "guests": 0, "outfit_id": "hand_me_down",
                      "honeymoon_id": "none"},
             "request_id": request_id,
             "expected_revision": state["revision"]})
        self.assertEqual(status, 200, body)
        return json.loads(body)

    # -- the contract --------------------------------------------------

    def test_nonconstant_range_quotes_once_and_posts_the_quote(self):
        gid, uid = self._union_under_law(
            "q2-range",
            [{"trigger": "ceremony_of_unrecognized_union",
              "effect": "civil_fine",
              "ledger_category": "legal",
              "max_per_occurrence_key": "union_id",
              "amount": {"randint": [200, 800]}}])
        quote = self._quote(gid, uid)
        self.assertEqual(quote["union_id"], uid)
        self.assertEqual(len(quote["lines"]), 1)
        line = quote["lines"][0]
        self.assertEqual(line["occurrence_id"], "lawfine:%s:0" % uid)
        self.assertEqual(line["category"], "legal")
        self.assertTrue(200 <= line["amount"] <= 800)
        self.assertEqual(quote["total"], line["amount"])
        # stored, never redrawn: a second quote returns the SAME draw
        self.assertEqual(self._quote(gid, uid), quote)

        before = json.loads(self.req("GET", "/api/games/" + gid)[1])
        env = self.httpd.service._load(gid)
        ledger_before = len(env["ledger"])
        self._plan_free_wedding(gid, uid, "q2-range-wed")
        after = json.loads(self.req("GET", "/api/games/" + gid)[1])
        env = self.httpd.service._load(gid)
        new_rows = env["ledger"][ledger_before:]
        # preview total == posted total: exactly the quote lines, no more,
        # no less (free ceremony adds no wedding-category charge)
        self.assertEqual(len(new_rows), len(quote["lines"]))
        for row, line in zip(new_rows, quote["lines"]):
            self.assertIn(line["occurrence_id"], row["source_key"])
            self.assertEqual(-row["amount"], line["amount"])
            self.assertEqual(row["category"], line["category"])
        self.assertEqual(sorted((r["category"], -r["amount"])
                                for r in new_rows),
                         sorted((l["category"], l["amount"])
                                for l in quote["lines"]))
        self.assertEqual(after["money"],
                         before["money"] - quote["total"])

    def test_multiple_fines_all_lines_previewed_and_posted(self):
        gid, uid = self._union_under_law(
            "q2-multi",
            [{"trigger": "ceremony_of_unrecognized_union",
              "effect": "civil_fine",
              "ledger_category": "legal",
              "max_per_occurrence_key": "union_id",
              "amount": {"randint": [300, 300]}},
             {"trigger": "ceremony_of_unrecognized_union",
              "effect": "civil_fine",
              "ledger_category": "other",
              "max_per_occurrence_key": "union_id",
              "amount": {"randint": [150, 150]}}])
        quote = self._quote(gid, uid)
        # every fine line is shown, in law order, with its own occurrence
        self.assertEqual(len(quote["lines"]), 2)
        self.assertEqual([l["occurrence_id"] for l in quote["lines"]],
                         ["lawfine:%s:0" % uid, "lawfine:%s:1" % uid])
        self.assertEqual([l["category"] for l in quote["lines"]],
                         ["legal", "other"])
        self.assertEqual(quote["total"],
                         sum(l["amount"] for l in quote["lines"]))
        self.assertEqual(quote["total"], 450)

        before = json.loads(self.req("GET", "/api/games/" + gid)[1])
        env = self.httpd.service._load(gid)
        ledger_before = len(env["ledger"])
        self._plan_free_wedding(gid, uid, "q2-multi-wed")
        after = json.loads(self.req("GET", "/api/games/" + gid)[1])
        env = self.httpd.service._load(gid)
        new_rows = env["ledger"][ledger_before:]
        self.assertEqual(len(new_rows), 2)
        self.assertEqual(sorted((r["category"], -r["amount"])
                                for r in new_rows),
                         [("legal", 300), ("other", 150)])
        self.assertEqual(after["money"],
                         before["money"] - quote["total"])

    def test_recognized_union_quotes_empty_and_view_has_no_forecast(self):
        # Q2 regression guard: the view must not carry any web-side
        # ceremony_fine forecast anymore (no hashing in the web layer)
        status, body = self.req(
            "POST", "/api/games",
            dict(make_profile(scenario="married_30"),
                 request_id="q2-ok-create"))
        self.assertEqual(status, 201, body)
        gid = json.loads(body)["game_id"]
        union = self._view_union(gid)
        self.assertNotIn("ceremony_fine", union)
        quote = self._quote(gid, union["union_id"])
        self.assertEqual(quote["lines"], [])
        self.assertEqual(quote["total"], 0)

    def test_quote_route_is_typed(self):
        status, body = self.req(
            "POST", "/api/games/nope/wedding-quote", {"union_id": "x"})
        self.assertNotEqual(status, 500)
        self.assertEqual(json.loads(body)["error"]["code"], "not_found")
        status, body = self.req(
            "POST", "/api/games/nope/wedding-quote", {})
        self.assertEqual(status, 400, body)


if __name__ == "__main__":
    unittest.main()
