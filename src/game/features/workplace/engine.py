"""Workplace/study v3 events phase (M3b B1 seam, Kimi A).

Serves C's wrk_*/stu_ v3 records through the EXISTING v3 pipeline
(Amendment 4 in M3/02-contracts.md): v3_bindable + evaluate_v3_conditions
+ EventEngine.instantiate3 + the shared resolve boundary. No new
interpreter, no phase reorder.

Cadence (frozen): at most one occurrence per family per aged year; each
record at most once per life (env["wrk_stu_seen"]). The route gates only
on the PRESENCE of the active source — wrk_ needs an active named
career assignment, stu_ an active feature enrollment (the paid
apprenticeship holds one of each, so a working student can be served
both families in one year via the existing _keep_phase re-entry).
Role/program gates stay in the content condition trees (C-owned).
"""

_EVENT_CACHE = None

FAMILIES = (("wrk", "wrk_"), ("stu", "stu_"))


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
                        and ev["id"].startswith(("wrk_", "stu_"))]
    return _EVENT_CACHE


def _active_assignment(env, person_id):
    from src.game.features.careers import core as careers_core
    return careers_core.active_assignment(env, person_id)


def _active_enrollment(env, person_id):
    from src.game.features.education import core as education_core
    return education_core.active_enrollment(env, person_id)


def advance_workplace_phase(env, ctx, service, rng):
    """Yearly phase hook (A's coordinator call site, Amendment 4).
    Serves at most one occurrence per family per year, unseen records
    only, through the single v3 dispatch path."""
    from src.game import events as event_mod
    from src.game import rule_adapter

    player = env["player"]
    actor_id = player.person_id
    turn = env["turn"]
    served = turn.setdefault("workplace_served", [])
    seen = env.setdefault("wrk_stu_seen", [])
    done = {"phase_id": "debt_service", "item_cursor": 0}

    sources = {
        "wrk": _active_assignment(env, actor_id) is not None,
        "stu": _active_enrollment(env, actor_id) is not None,
    }
    context = event_mod.GameContext(env, service)

    for family, prefix in FAMILIES:
        if family in served or not sources[family]:
            continue
        eligible = []
        for ev in _events():
            if not ev["id"].startswith(prefix) or ev["id"] in seen:
                continue
            bindings = event_mod.v3_bindable(ev, actor_id, context)
            if bindings is None:
                continue
            if not event_mod.evaluate_v3_conditions(
                    ev.get("conditions") or {}, context, bindings,
                    actor_id):
                continue
            eligible.append((ev, bindings))
        if not eligible:
            served.append(family)  # nothing to serve this year
            continue
        total = sum(ev.get("weight", 1) for ev, _ in eligible)
        roll = rng.randrange(total)
        chosen, chosen_bindings = eligible[0]
        for ev, bindings in eligible:
            roll -= ev.get("weight", 1)
            if roll < 0:
                chosen, chosen_bindings = ev, bindings
                break

        seq = env["next_decision_num"]
        env["next_decision_num"] += 1
        pending = event_mod.EventEngine.instantiate3(
            chosen, seq, rng=rng, context=context,
            prebound=chosen_bindings)
        seen.append(chosen["id"])
        served.append(family)
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
        remaining = any(f not in served and sources[f]
                        for f, _ in FAMILIES)
        return {"events": [], "money_entries": [],
                "pending_decision": pending,
                "continuation": done,
                "finished_turn": False,
                "_keep_phase": remaining}
    return {"events": [], "money_entries": [],
            "pending_decision": None,
            "continuation": done,
            "finished_turn": False}
