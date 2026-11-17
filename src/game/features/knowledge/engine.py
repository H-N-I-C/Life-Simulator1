"""Drama engine phase (M2.5, Kimi A): offers ONE dra_* v3 occurrence per
year (jealousy, suspicion, exes, in-laws, being caught, confessing,
forgiving, gossip, consensual check-ins) through C's frozen v3 pipeline.
Knowledge/fact tagging rides the reveal_fact/reputation domain effects;
the seen-cadence lives in env['knowledge']['seen'].
"""

from src.game.features.knowledge import core as knowledge
from src.game.features.knowledge.core import normalize_knowledge
from src.game.features.parenting.engine import _V3Context, reset_cache as \
    _reset_parenting_cache  # noqa: F401 (shared cache-reset semantics)

_EVENT_CACHE = None


def reset_cache():
    global _EVENT_CACHE
    _EVENT_CACHE = None


def _events():
    global _EVENT_CACHE
    if _EVENT_CACHE is None:
        from src.game import events as event_mod
        loaded = event_mod.load_event_files()
        _EVENT_CACHE = [ev for ev in loaded
                        if ev.get("schema_version") == 3
                        and ev["id"].startswith("dra_")]
    return _EVENT_CACHE


def advance_drama_phase(env, ctx, service, rng):
    """Yearly phase hook (A's coordinator call site). At most one drama
    occurrence per year; unseen occurrences only. Eligibility routes
    through C's single v3 dispatch path (R7/E6): bindability probe plus
    the evaluated condition tree, so e.g. min_age-gated content is never
    served to the wrong age."""
    from src.game import events as event_mod
    from src.game import rule_adapter

    normalize_knowledge(env)
    player = env["player"]
    if player.age < 18:
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "illness", "item_cursor": 0},
                "finished_turn": False}
    actor_id = player.person_id
    turn = env.get("revision", 0)
    context = _V3Context(event_mod.GameContext(env, service))

    seen = env["knowledge"].setdefault("seen", [])
    eligible = []
    for ev in _events():
        if ev["id"] in seen:
            continue
        bindings = event_mod.v3_bindable(ev, actor_id, context)
        if bindings is None:
            continue
        if not event_mod.evaluate_v3_conditions(
                ev.get("conditions") or {}, context, bindings, actor_id):
            continue
        eligible.append(ev)
    if not eligible:
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "romance_events",
                                 "item_cursor": 0},
                "finished_turn": False}

    total = sum(ev.get("weight", 1) for ev in eligible)
    roll = rng.randrange(total)
    chosen = eligible[0]
    for ev in eligible:
        roll -= ev.get("weight", 1)
        if roll < 0:
            chosen = ev
            break

    seq = env["next_decision_num"]
    env["next_decision_num"] += 1
    pending = event_mod.EventEngine.instantiate3(chosen, seq, rng=rng,
                                                 context=context)
    seen.append(chosen["id"])
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
            "continuation": {"phase_id": "romance_events",
                             "item_cursor": 0},
            "finished_turn": False}
