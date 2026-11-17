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
        for key in ("audience", "fact_id"):
            if key in event:
                entry[key] = event[key]
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
    if "pending_decision" in rule_result:
        # explicit None clears any previously stored pending record
        env["pending"] = rule_result["pending_decision"]
    return rule_result.get("finished_turn", False)


def apply_effects_bundle(env, service, player, effects, occurrence,
                         person_ids=None, journal_kind="event",
                         outcome_text="", rng=None, audience="public", fact_id=None):
    """Shared COMPLETE effect applier for automatic and selected outcomes.
    Handles every declared key (stats incl stress/karma/performance,
    money via the single ledger, per-person relationship, illnesses,
    flags) and journals the ACTUAL before/after deltas. `occurrence`
    scopes ledger idempotence, e.g. f"{event_id}:{choice_id}"."""
    from src.game.finance import Ledger
    person_ids = list(person_ids or [])
    stat_keys = ("happiness", "health", "smarts", "looks", "stress",
                 "karma", "performance")
    before = {k: getattr(player, k, 0) for k in stat_keys}
    before_rel = {}
    for pid in person_ids:
        target = service._find_person(env, pid)
        rel = getattr(target, "relationship", None)
        if rel is not None:
            before_rel[pid] = rel
    before_money = player.money

    entries = []
    for key, value in (effects or {}).items():
        if key == "money" and value:
            entries.append({
                "phase": env.get("turn", {}).get("cursor", {}).get(
                    "phase_id", "annual_events"),
                "occurrence_id": occurrence,
                "account_owner_id": player.person_id,
                "category": "other",
                "amount": value,
                "description": outcome_text[:80] or occurrence,
            })
        elif key == "relationship":
            rel = value if isinstance(value, (int, float)) else \
                value.get("delta", 0)
            for pid in person_ids:
                target = service._find_person(env, pid)
                if hasattr(target, "change_relationship"):
                    target.change_relationship(rel)
        elif key in ("add_illness", "remove_illness"):
            name = value if isinstance(value, str) else value.get("illness")
            if key == "add_illness":
                from src.lifesim_lib.lifesim_lib import TranslateMarker
                player.add_illness(TranslateMarker(name))
            else:
                player.remove_illness(name)
        elif key == "set_flag":
            env.setdefault("flags", {})[
                value.get("name")] = value.get("value", True)
        elif key in stat_keys:
            getattr(player, f"change_{key}")(value)
    if entries:
        ledger = Ledger(env, resolver=service._make_resolver(env))
        turn_id = env.get("turn", {}).get("turn_id",
                                          f"t_{env['revision']}")
        ledger.post_many(turn_id, player.age, entries)

    changes = {}
    for k, b in before.items():
        delta = getattr(player, k, 0) - b
        if delta:
            changes[k] = delta
    money_delta = player.money - before_money
    if money_delta:
        changes["money"] = money_delta
    for pid, b in before_rel.items():
        target = service._find_person(env, pid)
        delta = target.relationship - b
        if delta:
            changes[f"relationship:{pid}"] = delta
    if outcome_text:
        journal_events(env, [{
            "kind": journal_kind,
            "text": outcome_text,
            "person_ids": [player.person_id] + person_ids,
            "changes": changes,
            "audience": audience,
            "fact_id": fact_id,
        }])
    return changes
