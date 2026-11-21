"""Invites: stored offers (pending kind `social_invite`,
display_category "social") through the shared resolve boundary.

C's `soc_inv_*` v3 events author these records in production; A's
`offer()` is the same machinery available until C lands (and the
seam C's events use). Decline is always free; accept is gated by the
unbypassable adults_only 18+ rule even if content forgot; money posts
through the one ledger poster with `soc:<invite_id>` occurrence keys.
"""

DECLINE_ID = "decline"


def offer(env, person, *, kind, title, host_id, text=None,
          venue_label="", adults_only=False, circle="friends",
          options=(), rng=None):
    """Build + persist a social_invite stored offer. `options` are
    C-shaped dicts: {"id", "label", "effects": {"stats": {...},
    "money": int, "closeness": int}, "cost": int|None}. Returns the
    command result boundary dict (pending_decision inside)."""
    from src.game.features.social import state as social
    if kind not in social.INVITE_KINDS:
        raise ValueError(f"unknown invite kind: {kind!r}")
    st = social.ensure(env)
    invite_id = f"inv_{st['next_num']}"
    st["next_num"] += 1
    turn = env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"
    options_out = []
    for opt in options:
        cost = opt.get("cost") or 0
        options_out.append({
            "id": opt["id"], "label": opt["label"],
            "known_costs": ([{"kind": "money", "amount": cost}]
                            if cost else []),
            "known_effects": [], "enabled": True, "reason": None,
            "_effects": dict(opt.get("effects") or {}),
            "_outcome": opt.get("outcome_text", opt["label"])})
    options_out.append({"id": DECLINE_ID, "label": "Decline",
                        "known_costs": [], "known_effects": [],
                        "enabled": True, "reason": None,
                        "_effects": {}, "_outcome": ""})
    st["invites"][invite_id] = {
        "invite_id": invite_id, "kind": kind, "title": title,
        "host_id": host_id, "circle": circle,
        "venue_label": venue_label, "adults_only": bool(adults_only),
        "offered_turn": turn, "decision_id": None, "status": "open"}
    pending = {"id": None, "kind": "social_invite",
               "event_id": f"soc_inv:{invite_id}",
               "title": title,
               "text": text or f"You've been invited: {title}.",
               "person_ids": [person.person_id],
               "invite_id": invite_id, "host_id": host_id,
               "venue_label": venue_label,
               "adults_only": bool(adults_only),
               "options": options_out}
    return {"events": [], "money_entries": [],
            "pending_decision": pending}


def register_for_v3(env, person, *, invite_id, kind, title, host_id,
                    venue_label="", adults_only=False, circle="friends"):
    """Amendment 2: back a v3-authored soc_inv_* pending with the
    invite envelope record (the inbox row). The pending's decision id
    is echoed at projection time from the open pending."""
    from src.game.features.social import state as social
    st = social.ensure(env)
    turn = env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"
    st["invites"][invite_id] = {
        "invite_id": invite_id, "kind": kind, "title": title,
        "host_id": host_id, "circle": circle,
        "venue_label": venue_label, "adults_only": bool(adults_only),
        "offered_turn": turn, "decision_id": None, "status": "open"}
    return st["invites"][invite_id]


def mark_resolved(env, invite_id, accepted):
    st = env.get("social_state")
    if not isinstance(st, dict):
        return
    invite = st.get("invites", {}).get(invite_id)
    if invite is not None and invite.get("status") == "open":
        invite["status"] = "accepted" if accepted else "declined"
        invite["decision_id"] = None


def expire_old(env, current_turn):
    st = env.get("social_state")
    if not isinstance(st, dict):
        return
    for invite in st.get("invites", {}).values():
        if invite.get("status") == "open" \
                and invite.get("offered_turn") != current_turn:
            invite["status"] = "expired"
            invite["decision_id"] = None


def resolve(env, service, person, pending, option_id, rng):
    """The social_invite resolve applier (A-owned), called from the
    service `_cmd_resolve` seam. Returns {"events": [...]} for the
    boundary to journal/post; raises typed PermissionError/ValueError."""
    from src.game import rule_adapter
    from src.game.features.social import state as social
    st = social.ensure(env)
    invite = st["invites"].get(pending.get("invite_id"))
    if invite is None or invite["status"] != "open":
        raise PermissionError("this invitation is no longer open")
    option = next((o for o in pending["options"]
                   if o["id"] == option_id), None)
    if option is None:
        raise ValueError("unknown option")
    turn = env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"
    if option_id == DECLINE_ID:
        invite["status"] = "declined"
        invite["decision_id"] = None
        return {"events": [{
            "kind": "social", "person_ids": [person.person_id],
            "text": f"I declined the invite: {invite['title']}.",
            "changes": {}}]}
    if pending.get("adults_only") and person.age < 18:
        raise PermissionError(
            "you must be 18 or older to attend")
    invite["status"] = "accepted"
    invite["decision_id"] = None
    effects = dict(option.get("_effects") or {})
    stats = effects.pop("stats", None)
    money = effects.pop("money", 0)
    closeness = effects.pop("closeness", 0)
    money_entries = []
    if money:
        money_entries.append({"phase": "social",
                              "occurrence_id":
                              f"soc:{invite['invite_id']}",
                              "leg": "invite", "account_owner_id":
                              person.person_id, "category": "other",
                              "amount": int(money),
                              "description": invite["title"]})
    events = []
    if closeness and invite.get("host_id"):
        actual = social.apply_closeness(env, person, invite["host_id"],
                                        int(closeness))
        if actual:
            events.append({"kind": "relationship",
                           "person_ids": [person.person_id,
                                          invite["host_id"]],
                           "text": f"I went to {invite['title']} — "
                                   f"good times with the host.",
                           "changes": {"relationship": actual}})
    events.append({"kind": "social", "person_ids": [person.person_id],
                   "text": option.get("_outcome")
                           or f"I attended: {invite['title']}.",
                   "changes": {}})
    return {"events": events, "money_entries": money_entries,
            "bundle": stats}
