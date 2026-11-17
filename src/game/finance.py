"""Single audited money ledger for web games.

The one posting authority: rule code (src/menus/rule_actions.py, owned by
C) requests postings via money_entries and NEVER mutates money directly.

Posting keys are fully qualified and idempotent:
    {turn_id}:{phase}:{occurrence_id}:{account_id}:{leg}
so postings for different accounts, different occurrences of the same
action, or different legs of a transfer never collide. Reusing a key with a
DIFFERENT amount/account/category raises LedgerConflict instead of silently
returning the original entry. All legs of one transfer share a group id but
keep unique posting keys.

Ledger row: {id, source_key, turn_id, age, person_id, category, amount,
balance_after, description, group_id?}. Signed amounts. balance_after is
tracked per account owner (player household vs child-owned accounts).
"""

CATEGORIES = {
    "income", "tax", "expenses", "pension", "overtime", "child_net",
    "debt_borrow", "debt_repay", "loan_borrow", "loan_repay",
    "medical", "activity", "transfer", "inheritance", "lottery",
    "gift_received", "gift_given", "wedding", "education",
    "estate_close", "legal", "other",
}


class LedgerConflict(Exception):
    """A posting key was reused with different details."""


class Ledger:
    def __init__(self, env, resolver=None):
        """resolver(owner_id) -> Person or None; defaults to walking the
        player graph plus the archive."""
        self._env = env
        self._resolver = resolver or self._default_resolver
        env.setdefault("ledger", [])
        env.setdefault("ledger_next_id", 1)
        # balances tracked per account owner id; seeded from player/children
        env.setdefault("balances", {})

    # -- balances -----------------------------------------------------

    def balance(self, owner_id):
        if owner_id not in self._env["balances"]:
            person = self._resolver(owner_id)
            self._env["balances"][owner_id] = getattr(person, "money", 0) if person else 0
        return self._env["balances"][owner_id]

    def _default_resolver(self, owner_id):
        from src.game.service import GameService  # late import; no cycle
        for person in GameService._walk_people(self._env["player"]):
            if person.person_id == owner_id:
                return person
        for person in self._env.get("archive", []):
            if getattr(person, "person_id", None) == owner_id:
                return person
        return None

    def sync_from_people(self):
        """Initialize balances for owners without recorded state (migration).
        Called once when a ledger is introduced to an older save."""
        for person in [self._env["player"]] + [
                c for c in self._env["player"].children]:
            oid = person.person_id
            if oid not in self._env["balances"]:
                self._env["balances"][oid] = person.money

    # -- posting ------------------------------------------------------

    @staticmethod
    def posting_key(turn_id, entry):
        try:
            return "{turn}:{phase}:{occurrence}:{account}:{leg}".format(
                turn=turn_id,
                phase=entry["phase"],
                occurrence=entry["occurrence_id"],
                account=entry["account_owner_id"],
                leg=entry.get("leg", "main"),
            )
        except KeyError as e:
            raise ValueError(f"money entry missing field: {e}")

    def post(self, turn_id, age, entry):
        """Post one money entry idempotently. Returns the ledger row.

        entry: {phase, occurrence_id, account_owner_id, leg?, category,
                amount, description, group_id?}
        Reusing a key with different details raises LedgerConflict.
        """
        source_key = self.posting_key(turn_id, entry)
        for row in self._env["ledger"]:
            if row["source_key"] == source_key:
                if (row["amount"] != entry["amount"]
                        or row["person_id"] != entry["account_owner_id"]
                        or row["category"] != entry["category"]):
                    raise LedgerConflict(
                        f"posting key reused with different details: "
                        f"{source_key}")
                return row  # exactly-once: already posted
        amount = entry["amount"]
        if not isinstance(amount, int) or isinstance(amount, bool):
            raise ValueError("amount must be an integer")
        category = entry["category"]
        if category not in CATEGORIES:
            raise ValueError(f"unknown category: {category}")
        owner = entry["account_owner_id"]
        new_balance = self.balance(owner) + amount
        person = self._resolver(owner)
        if person is not None:
            person.money = new_balance
        self._env["balances"][owner] = new_balance
        seq = self._env["ledger_next_id"]
        self._env["ledger_next_id"] += 1
        row = {
            "id": f"le_{seq}",
            "source_key": source_key,
            "turn_id": turn_id,
            "age": age,
            "person_id": owner,
            "category": category,
            "amount": amount,
            "balance_after": new_balance,
            "description": entry.get("description", ""),
        }
        if entry.get("group_id"):
            row["group_id"] = entry["group_id"]
        self._env["ledger"].append(row)
        return row

    def post_many(self, turn_id, age, entries):
        """Post several entries as a unit; rolls back on failure so a
        failed command cannot leave a half-posted turn."""
        posted = []
        ledger_len = len(self._env["ledger"])
        try:
            for entry in entries:
                row = self.post(turn_id, age, entry)
                # idempotent replays return PRE-EXISTING rows: only rows
                # appended by THIS batch may be rolled back
                if len(self._env["ledger"]) > ledger_len:
                    posted.append(row)
                    ledger_len = len(self._env["ledger"])
        except Exception:
            for row in posted:
                self._env["ledger"].remove(row)
                self._env["balances"][row["person_id"]] -= row["amount"]
                person = self._resolver(row["person_id"])
                if person is not None:
                    person.money = self._env["balances"][row["person_id"]]
                self._env["ledger_next_id"] -= 1
            raise
        return posted

    # -- projection ---------------------------------------------------

    def entries_for(self, owner_id=None, turn_id=None):
        rows = self._env["ledger"]
        if owner_id is not None:
            rows = [r for r in rows if r["person_id"] == owner_id]
        if turn_id is not None:
            rows = [r for r in rows if r["turn_id"] == turn_id]
        return [dict(r) for r in rows]

    def debt_projection(self):
        """Debt balances (student loans etc.) from canonical player/child
        fields; liabilities stay separate from cash."""
        player = self._env["player"]
        debts = []
        if getattr(player, "student_loan", 0):
            debts.append({
                "kind": "student_loan", "person_id": player.person_id,
                "remaining": player.student_loan,
            })
        return debts
