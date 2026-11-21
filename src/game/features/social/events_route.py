"""M4 soc_ events yearly route (A-owned seam; C authors the content).

Serves C's soc_* v3 records through the EXISTING v3 pipeline
(v3_bindable + evaluate_v3_conditions + instantiate3 + the shared
resolve boundary) — the Amendment-4 pattern. Cadence: at most ONE soc_
occurrence per aged year; each record at most once per life
(env["soc_seen"], pickle/undo/replay-safe); the per-year marker lives
in env["turn"]["soc_served"] and dies with the turn. The route gates on
nothing itself — C's condition trees (school/work/age/arc/circle gates)
decide eligibility, so records are unreachable until their gate is
true. Adults-only venues stay gated by A's resolve applier.
"""

_EVENT_CACHE = None

PREFIX = "soc_"


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
                        and ev["id"].startswith(PREFIX)]
    return _EVENT_CACHE


def advance_social_events_phase(env, ctx, service, rng):
    from src.game import events as event_mod
    from src.game import rule_adapter
    from src.game.features.social import bullying as _bullying_mod
    _bullying_mod.register_domain_effects()

    player = env["player"]
    turn = env["turn"]
    if turn.get("soc_served"):
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "debt_service",
                                 "item_cursor": 0},
                "finished_turn": False}
    seen = env.setdefault("soc_seen", [])
    context = event_mod.GameContext(env, service)
    actor_id = player.person_id
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
        eligible.append((ev, bindings))
    if not eligible:
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "debt_service",
                                 "item_cursor": 0},
                "finished_turn": False}
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
        chosen, seq, rng=rng, context=context, prebound=chosen_bindings)
    seen.append(chosen["id"])
    turn["soc_served"] = True
    # Amendment 2: soc_inv_* records become social_invite stored offers
    # (the inbox): v3 instantiation/resolution stays generic, the
    # pending kind + envelope record make them addressable offers
    if (chosen.get("interaction") or {}).get("kind") == "social_invite":
        from src.game.features.social import invites as invites_mod
        iid = f"inv_{env['social_state']['next_num']}"
        env["social_state"]["next_num"] += 1
        invite_kind = chosen["id"]
        for needle, mapped in (("study_group", "study_group"),
                               ("afterwork", "after_work"),
                               ("after_work", "after_work"),
                               ("team_practice", "team_practice")):
            if needle in chosen["id"]:
                invite_kind = mapped
                break
        else:
            invite_kind = "party"
        invites_mod.register_for_v3(
            env, player, invite_id=iid, kind=invite_kind,
            title=chosen.get("title", chosen["id"]),
            host_id=(chosen_bindings or {}).get("host"),
            venue_label=chosen.get("venue_label", ""),
            adults_only=bool(chosen.get("adults_only", False)),
            circle=invite_kind if invite_kind in ("study_group",
                                                  "team_practice")
            else "friends")
        pending["kind"] = "social_invite"
        pending["invite_id"] = iid
        pending["adults_only"] = bool(chosen.get("adults_only", False))
        pending["venue_label"] = chosen.get("venue_label", "")
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
            "continuation": {"phase_id": "debt_service",
                             "item_cursor": 0},
            "finished_turn": False}
