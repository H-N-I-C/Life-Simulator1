"""Social projection (A-owned, additive, pure read): _state["social"]
per B's frozen shape in 02-contracts.md, plus the person_ref `social`
delta on the existing people projection. Guards like careers: a
projection failure degrades to honest empties, never a state error.
"""

GROUP_ORDER = ("family", "friends", "classmates", "coworkers")


def _person_ref(person, social_delta=None):
    ref = {"id": person.person_id, "name": person.name,
           "age": person.age, "alive": True}
    if social_delta is not None:
        ref["social"] = social_delta
    return ref


def person_social(env, person):
    """The M4 person_ref delta; None for people with no social record
    (B: kind null = plain circle member; missing person = no delta,
    never fabricated)."""
    from src.game.features.social import state as social
    st = env.get("social_state")
    if not isinstance(st, dict):
        return None
    rec = st.get("people", {}).get(person.person_id)
    if rec is None:
        return None
    from src.game.features.social import circles as circles_mod
    circles = []
    # CURRENT membership only: a former classmate no longer claims the
    # classmates circle (they are a faded contact in the Friends group)
    if person.person_id in circles_mod.group_member_ids(env,
                                                        "classmates"):
        circles.append("classmates")
    if person.person_id in circles_mod.group_member_ids(env,
                                                        "coworkers"):
        circles.append("coworkers")
    if rec["kind"] in ("friend", "best_friend", "rival", "bully"):
        circles.append("friends")
    for club_id in st.get("circles", {}).get("clubs", {}):
        if person.person_id in st["circles"]["clubs"][club_id].get(
                "member_ids", []):
            circles.append(f"club:{club_id}")
    memory = [m["text"] for m in rec.get("memory", [])
              if m.get("player_known")][-3:]
    return {"kind": rec["kind"], "closeness": rec["closeness"],
            "circles": circles, "memory": memory,
            "status": rec.get("status")}


def _find_person_obj(env, service, person_id):
    try:
        return service._find_person(env, person_id)
    except Exception:
        return None


def social_view(env, service):
    """B's frozen shape: groups (frozen order), invites (newest first),
    popularity {score, band, label, delta_last_year}, clubs ([] until
    C's clubs.json lands — honest empties)."""
    from src.game.features.social import circles as circles_mod
    from src.game.features.social import popularity as popularity_mod
    from src.game.features.social import state as social
    player = env["player"]
    st = social.ensure(env)
    groups = []
    for gid in GROUP_ORDER:
        people = []
        if gid == "family":
            for person in service._walk_people(player):
                if person is player:
                    continue
                role = service._role(person)
                if role in ("mother", "father", "partner", "child",
                            "sibling"):
                    delta = person_social(env, person)
                    people.append(_person_ref(person, delta))
        else:
            for pid in circles_mod.group_member_ids(env, gid):
                person = _find_person_obj(env, service, pid)
                if person is None:
                    continue
                people.append(_person_ref(person, person_social(env,
                                                                person)))
        groups.append({"group_id": gid,
                       "label": gid.capitalize(), "people": people})
    invites = []
    open_invite_id = None
    pending_env = env.get("pending")
    if isinstance(pending_env, dict) \
            and pending_env.get("kind") == "social_invite":
        open_invite_id = pending_env.get("invite_id")
    for invite in sorted(st["invites"].values(),
                         key=lambda i: i["offered_turn"], reverse=True):
        host = _find_person_obj(env, service, invite.get("host_id"))
        invites.append({
            "invite_id": invite["invite_id"], "kind": invite["kind"],
            "title": invite.get("title", invite["kind"]),
            "host": _person_ref(host) if host is not None else None,
            "venue_label": invite.get("venue_label", ""),
            "adults_only": bool(invite.get("adults_only")),
            "status": invite["status"],
            "decision_id": pending_env.get("id")
            if invite["invite_id"] == open_invite_id else None})
    pop = st["popularity"]
    band = popularity_mod.band_for(pop["score"])
    clubs_out = []
    try:
        from src.game.content import social as social_content
        from src.menus import rule_actions
        clubs_data = social_content.load_clubs().get("clubs", [])
        specs = rule_actions.club_specs(env, player)
        joined = st.get("circles", {}).get("clubs", {})
        for club, spec in zip(clubs_data, specs):
            members = (joined.get(club["id"]) or {}).get("member_ids",
                                                         [])
            member = player.person_id in members
            clubs_out.append({
                "club_id": club["id"], "label": club["label"],
                "category": club.get("category", "hobby"),
                "member": member,
                "role": "member" if member else None,
                "meets_label": club.get("meets_label", ""),
                "cost_per_year": int(club.get("cost_per_year") or 0),
                "adults_only": bool(club.get("adults_only", False)),
                "actions": [spec] if spec else []})
    except Exception:
        clubs_out = []
    return {"schema_version": 1, "groups": groups, "invites": invites,
            "popularity": {"score": pop["score"], "band": band["id"],
                           "label": band["label"],
                           "delta_last_year": pop["score"]
                           - pop.get("last_score", pop["score"])},
            "clubs": clubs_out}
