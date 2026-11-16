"""Adapters from C's RuleResult dicts to A's ledger/journal/cursor.

Stable A-side boundary: works with any rule result shaped per the frozen
rule-examples.json — events become journal entries with ACTUAL deltas,
money_entries post through the single Ledger authority, pending/continuation
update the turn cursor. No rule formulas live here.
"""

from src.game.finance import Ledger


def post_money_entries(env, service, turn_id, age, money_entries, ledger=None):
    """Post rule-requested money entries through the one authority."""
    if not money_entries:
        return []
    ledger = ledger or Ledger(env, resolver=service._make_resolver(env))
    return ledger.post_many(turn_id, age, money_entries)


def journal_events(env, events, person_ids_prefix=()):
    """Append rule events to the journal. Relationship/stat changes recorded
    by rules are trusted as actuals (rules measure before/after per the
    contract); entries carry per-person relationship deltas in
    event['relationship_changes'] as {person_id: delta}."""
    for event in events or []:
        seq = env["next_journal_num"]
        env["next_journal_num"] += 1
        entry = {
            "id": f"j_{seq}",
            "age": env["player"].age,
            "kind": event.get("kind", "event"),
            "text": event.get("text", ""),
            "person_ids": list(event.get("person_ids", ())),
            "changes": dict(event.get("changes", {})),
        }
        rel = event.get("relationship_changes") or {}
        if rel:
            entry["relationship_changes"] = dict(rel)
        env["journal"].append(entry)


def apply_cursor_update(env, rule_result):
    """Fold a rule result's continuation/pending into the turn cursor."""
    turn = env.setdefault("turn", {})
    turn.setdefault("turn_id", f"t_{env['revision'] + 1:06d}")
    turn.setdefault("cursor", {"phase_id": "year_start", "item_cursor": 0})
    turn.setdefault("done", {})
    continuation = rule_result.get("continuation")
    if isinstance(continuation, dict) and continuation.get("phase_id"):
        turn["cursor"] = {
            "phase_id": continuation["phase_id"],
            "item_cursor": continuation.get("item_cursor", 0),
        }
        if continuation.get("event_id") is not None:
            turn["cursor"]["event_id"] = continuation["event_id"]
        if continuation.get("step") is not None:
            turn["cursor"]["step"] = continuation["step"]
    pending = rule_result.get("pending_decision")
    if pending is not None:
        env["pending"] = pending
    return rule_result.get("finished_turn", False)
