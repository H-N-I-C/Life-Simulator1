import tempfile
import unittest

from src.game.finance import Ledger, LedgerConflict
from src.game.service import GameService


def make_env():
    svc = GameService(tempfile.mkdtemp(prefix="lifesim-ledger-test-"))
    state = svc.new_game(
        {"first_name": "Ada", "last_name": "Lee", "gender": "female",
         "country": "Australia", "seed": 5}, "r1")
    env = svc._load(state["game_id"])
    return svc, env


class LedgerTests(unittest.TestCase):
    def test_posting_is_idempotent_per_source_key(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        entry = {"phase": "income", "occurrence_id": "salary",
                 "account_owner_id": pid,
                 "category": "income", "amount": 1000,
                 "description": "gross"}
        r1 = led.post("t1", 30, entry)
        r2 = led.post("t1", 30, dict(entry))
        self.assertIs(r1, r2)
        self.assertEqual(len(env["ledger"]), 1)
        self.assertEqual(led.balance(pid), 1000)
        self.assertEqual(env["player"].money, 1000)

    def test_source_key_scoped_per_turn(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        entry = {"phase": "income", "occurrence_id": "salary",
                 "account_owner_id": pid,
                 "category": "income", "amount": 1000}
        led.post("t1", 30, entry)
        led.post("t2", 31, entry)  # new turn -> new posting
        self.assertEqual(len(env["ledger"]), 2)
        self.assertEqual(led.balance(pid), 2000)

    def test_signed_amounts_and_balance_after(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        led.post("t1", 30, {"phase": "income", "occurrence_id": "salary",
                            "account_owner_id": pid,
                            "category": "income", "amount": 1000})
        row = led.post("t1", 30, {"phase": "income",
                                  "occurrence_id": "salary:tax",
                                  "account_owner_id": pid,
                                  "category": "tax", "amount": -200})
        self.assertEqual(row["balance_after"], 800)
        self.assertEqual(row["amount"], -200)

    def test_post_many_rolls_back_on_failure(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        with self.assertRaises(ValueError):
            led.post_many("t1", 30, [
                {"phase": "income", "occurrence_id": "ok",
                 "account_owner_id": pid,
                 "category": "income", "amount": 5},
                {"phase": "income", "occurrence_id": "bad",
                 "account_owner_id": pid,
                 "category": "not_a_category", "amount": 5},
            ])
        self.assertEqual(env["ledger"], [])
        self.assertEqual(env["player"].money, 0)
        self.assertEqual(led.balance(pid), 0)

    def test_child_account_is_tracked_separately(self):
        svc, env = make_env()
        led = Ledger(env)
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        player = env["player"]
        mother = player.parents["Mother"]
        kid = Child("Kid", "Lee", Gender.Male, 50, 50, mother, player)
        kid.age = 20
        player.children.append(kid)
        player.relations.append(kid)
        led.post("t1", 40, {"phase": "income", "occurrence_id": "child_net",
                            "account_owner_id": kid.person_id,
                            "category": "child_net", "amount": 300})
        self.assertEqual(led.balance(kid.person_id), 300)
        self.assertEqual(kid.money, 300)
        self.assertEqual(led.balance(player.person_id), 0)

    def test_same_occurrence_different_accounts_do_not_collide(self):
        # player salary and child salary in the same turn are distinct
        # postings even though the occurrence label matches
        svc, env = make_env()
        led = Ledger(env)
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender
        player = env["player"]
        kid = Child("Kid", "Lee", Gender.Male, 50, 50,
                    player.parents["Mother"], player)
        kid.age = 20
        player.children.append(kid)
        player.relations.append(kid)
        entry = {"phase": "income", "occurrence_id": "salary",
                 "category": "income", "amount": 1000}
        led.post("t1", 40, dict(entry, account_owner_id=player.person_id))
        led.post("t1", 40, dict(entry, account_owner_id=kid.person_id))
        self.assertEqual(len(env["ledger"]), 2)
        self.assertEqual(led.balance(player.person_id), 1000)
        self.assertEqual(led.balance(kid.person_id), 1000)

    def test_repeated_paid_action_uses_occurrence_ids(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        for i, amount in enumerate((-200, -350)):
            led.post("t1", 30, {
                "phase": "activities", "occurrence_id": f"doctor:{i}",
                "account_owner_id": pid, "category": "medical",
                "amount": amount, "description": "Doctor visit"})
        self.assertEqual(len(env["ledger"]), 2)
        self.assertEqual(led.balance(pid), -550)

    def test_key_reuse_with_different_details_conflicts(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        entry = {"phase": "income", "occurrence_id": "salary",
                 "account_owner_id": pid, "category": "income",
                 "amount": 1000}
        led.post("t1", 30, entry)
        with self.assertRaises(LedgerConflict):
            led.post("t1", 30, dict(entry, amount=1500))
        with self.assertRaises(LedgerConflict):
            led.post("t1", 30, dict(entry, category="lottery"))
        # identical retry still returns the original row
        row = led.post("t1", 30, dict(entry))
        self.assertEqual(row["amount"], 1000)
        self.assertEqual(len(env["ledger"]), 1)

    def test_projection_filters_and_debt(self):
        svc, env = make_env()
        led = Ledger(env)
        pid = env["player"].person_id
        env["player"].student_loan = 15000
        led.post("t1", 30, {"phase": "income", "occurrence_id": "salary",
                            "account_owner_id": pid,
                            "category": "income", "amount": 500})
        led.post("t2", 31, {"phase": "income", "occurrence_id": "salary",
                            "account_owner_id": pid,
                            "category": "income", "amount": 600})
        self.assertEqual(len(led.entries_for(turn_id="t1")), 1)
        self.assertEqual(len(led.entries_for(owner_id=pid)), 2)
        debts = led.debt_projection()
        self.assertEqual(debts[0]["remaining"], 15000)
        self.assertEqual(debts[0]["person_id"], pid)


if __name__ == "__main__":
    unittest.main()
