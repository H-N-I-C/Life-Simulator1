"""Parenting engine phase (M2.5, Kimi A). Samples the par_* v3 events
(content authored in C's frozen v3 format, loaded by C's loader) and
offers ONE parenting occurrence per year through EventEngine.instantiate3
(frozen projection, bindings once, free exit preserved by validation).

Children stay non-romantic dependents: every par_* event must declare
interaction.romantic false (the v3 validator rejects a romantic event
with min_age < 18, and these events bind minors by design).
"""

import random

from src.game.features.parenting import core

_EVENT_CACHE = None


def _events():
    """Parenting v3 events via C's loader (cached per process; tests can
    reset _EVENT_CACHE)."""
    global _EVENT_CACHE
    if _EVENT_CACHE is None:
        from src.game import events as event_mod
        loaded = event_mod.load_event_files()
        _EVENT_CACHE = [ev for ev in loaded
                        if ev.get("schema_version") == 3
                        and ev["id"].startswith("par_")]
    return _EVENT_CACHE


def reset_cache():
    global _EVENT_CACHE
    _EVENT_CACHE = None


class _V3Context:
    """GameContext adapted to instantiate3's duck-typed expectations
    (player / player_id / policy_revision); every other attribute
    delegates, so C's predicates and text rendering see the same
    envelope-backed context."""

    def __init__(self, game_context):
        self._gc = game_context
        self.player = game_context._player
        self.player_id = game_context._player.person_id
        self.policy_revision = game_context._env.get(
            "policy", {}).get("policy_revision")

    def __getattr__(self, name):
        return getattr(self._gc, name)


def _bindable(ev, actor_id, context):
    """Light pre-binding: every required role must bind non-null against
    THIS envelope (C's instantiate3 re-binds identically at offer time).
    """
    from src.game import events as event_mod
    roles = ev.get("roles") or {}
    bound_child = None
    for name, cfg in roles.items():
        select = cfg.get("select", "player" if name == "actor" else None)
        if select == "player":
            continue
        pred = cfg.get("predicate")
        pid = event_mod.PREDICATES[pred](actor_id, context)
        if pid is None and not cfg.get("optional"):
            return None
        if name == "child" and pid is not None:
            bound_child = pid
    return bound_child or actor_id


def advance_parenting_phase(env, ctx, service, rng):
    """Yearly phase hook (A's coordinator call site). Returns the phase
    result dict; offers at most one occurrence per year."""
    from src.game import events as event_mod
    from src.game import rule_adapter

    core.backfill_all(env, service)
    player = env["player"]
    actor_id = player.person_id
    turn = env.get("revision", 0)
    context = _V3Context(event_mod.GameContext(env, service))

    eligible = []
    for ev in _events():
        child_id = _bindable(ev, actor_id, context)
        if child_id is None:
            continue
        if core.is_seen(env, ev["id"], child_id):
            continue
        eligible.append((ev, child_id))
    if not eligible:
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "illness",
                                 "item_cursor": 0},
                "finished_turn": False}

    # weighted pick over the eligible occurrences
    total = sum(ev.get("weight", 1) for ev, _ in eligible)
    roll = rng.randrange(total)
    chosen, child_id = eligible[0]
    for ev, cid in eligible:
        roll -= ev.get("weight", 1)
        if roll < 0:
            chosen, child_id = ev, cid
            break

    seq = env["next_decision_num"]
    env["next_decision_num"] += 1
    pending = event_mod.EventEngine.instantiate3(chosen, seq, rng=rng,
                                                 context=context)
    core.mark_seen(env, chosen["id"], child_id)
    if pending.get("pre_effects"):
        rule_adapter.apply_effects_bundle(
            env, service, player, pending["pre_effects"],
            occurrence=f"{chosen['id']}:pre",
            person_ids=pending.get("_pre_person_ids_extra") or [],
            journal_kind="event",
            outcome_text=pending["text"],
                audience=pending.get("audience") or "public",
                fact_id=pending.get("fact_id"))
        pending["pre_applied"] = True
    return {"events": [], "money_entries": [],
            "pending_decision": pending,
            "continuation": {"phase_id": "illness", "item_cursor": 0},
            "finished_turn": False}
