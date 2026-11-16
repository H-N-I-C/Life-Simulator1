import tempfile
import unittest

from src.game.context import RuleContext
from src.game.rule_adapter import (
    apply_cursor_update, journal_events, post_money_entries)
from src.game.service import GameService


def make_env():
    svc = GameService(tempfile.mkdtemp(prefix="lifesim-ctx-test-"))
    state = svc.new_game(
        {"first_name": "Ada", "last_name": "Lee", "gender": "female",
         "country": "Australia", "seed": 5}, "r1")
    env = svc._load(state["game_id"])
    return svc, env, state["game_id"]


class RuleContextTests(unittest.TestCase):
    def test_context_fields_match_frozen_contract(self):
        svc, env, gid = make_env()
        ctx = RuleContext(env, svc)
        self.assertIs(ctx.player, env["player"])
        self.assertTrue(ctx.turn_id.startswith("t_"))
        self.assertEqual(ctx.cursor["phase_id"], "year_start")
        self.assertEqual(ctx.data, {"version": 2})
        by_id = ctx.people_by_id
        self.assertIn(env["player"].person_id, by_id)
        for parent in env["player"].parents.values():
            self.assertIn(parent.person_id, by_id)
        # annual flags alias
        self.assertIs(ctx.annual_flags, env["year_actions"])

    def test_done_markers(self):
        svc, env, gid = make_env()
        ctx = RuleContext(env, svc)
        ctx.mark_done("family_events", "divorce:x")
        self.assertTrue(ctx.is_done("family_events", "divorce:x"))
        ctx.mark_done("family_events", "divorce:x")  # idempotent
        self.assertEqual(ctx.done_markers["family_events"], ["divorce:x"])


class RuleAdapterTests(unittest.TestCase):
    def test_post_money_entries_through_ledger(self):
        svc, env, gid = make_env()
        ctx = RuleContext(env, svc)
        rows = post_money_entries(env, svc, ctx.turn_id, 30, [
            {"phase": "work_income", "occurrence_id": "salary",
             "leg": "gross", "account_owner_id": env["player"].person_id,
             "category": "income", "amount": 1000},
            {"phase": "work_income", "occurrence_id": "salary",
             "leg": "tax", "account_owner_id": env["player"].person_id,
             "category": "tax", "amount": -150},
        ])
        self.assertEqual(len(rows), 2)
        self.assertEqual(env["player"].money, 850)

    def test_journal_events_with_relationship_deltas(self):
        svc, env, gid = make_env()
        journal_events(env, [
            {"kind": "conversation",
             "text": "You had a chat.",
             "person_ids": [env["player"].person_id],
             "changes": {"happiness": 3},
             "relationship_changes": {"<mother_id>": 2}},
        ])
        entry = env["journal"][-1]
        self.assertEqual(entry["kind"], "conversation")
        self.assertEqual(entry["changes"], {"happiness": 3})
        self.assertEqual(entry["relationship_changes"], {"<mother_id>": 2})

    def test_apply_cursor_update(self):
        svc, env, gid = make_env()
        finished = apply_cursor_update(env, {
            "continuation": {"phase_id": "debt_service", "item_cursor": 0},
            "pending_decision": {"id": "d_1"},
            "finished_turn": False,
        })
        self.assertFalse(finished)
        self.assertEqual(env["turn"]["cursor"]["phase_id"], "debt_service")
        self.assertEqual(env["pending"], {"id": "d_1"})
        finished = apply_cursor_update(env, {
            "continuation": {"phase_id": "year_close", "item_cursor": 0},
            "pending_decision": None,
            "finished_turn": True,
        })
        self.assertTrue(finished)
        self.assertIsNone(env["pending"])


if __name__ == "__main__":
    unittest.main()
