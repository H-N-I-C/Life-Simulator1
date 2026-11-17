"""Weddings & marriage core (M2.5, Kimi A): ceremonies on union edges,
prenups, vow renewals, anniversaries, selected-union divorce.

All charges/settlements post ONCE through the frozen M2 ledger; the
always-available free configuration (courthouse ceremony, no frills) is
the legal no-cost exit. Fictional, versioned price catalog.
"""

from src.game.features.family_graph.graph import GraphError, assert_adult_romantic


def _guard_union(env, service, union):
    people = service._all_people(env)
    a, b = union["person_ids"]
    assert_adult_romantic(people.get(a), people.get(b))

# ------------------------------------------------------------- catalog
# Fictional venues/outfits/honeymoons; base prices are frozen
# placeholder calibration (game balance, not real-world costs).

VENUES = {
    "courthouse": {"label": "Courthouse ceremony", "base": 0,
                   "per_guest": 0},
    "garden":     {"label": "Botanic garden", "base": 800,
                   "per_guest": 15},
    "hall":       {"label": "Community hall", "base": 1500,
                   "per_guest": 25},
    "grand":      {"label": "Grand hotel ballroom", "base": 6000,
                   "per_guest": 60},
}

OUTFITS = {
    "hand_me_down": {"label": "Hand-me-down finery", "price": 0},
    "hired":        {"label": "Hired suit or dress", "price": 300},
    "tailored":     {"label": "Tailor-made", "price": 2500},
}

HONEYMOONS = {
    "none":   {"label": "No honeymoon", "price": 0},
    "road":   {"label": "Road trip", "price": 400},
    "island": {"label": "Island week", "price": 3500},
}

ANNIVERSARY_MILESTONES = (1, 5, 10, 15, 20, 25, 30, 40, 50)

CHILD_SUPPORT_PER_CHILD = 2000  # one-time posting + obligation record


def new_weddings_state():
    return {"anniversaries": {}, "renewals": {}}


def normalize_weddings(env):
    state = env.get("weddings")
    if not isinstance(state, dict):
        env["weddings"] = new_weddings_state()
        return True
    dirty = False
    for key, default in (("anniversaries", {}), ("renewals", {})):
        if not isinstance(state.get(key), type(default)):
            state[key] = default
            dirty = True
    return dirty


def wedding_cost(venue_id, guests, outfit_id, honeymoon_id):
    """Sum of all parts; each part posts as its own single ledger
    occurrence, so the free configuration genuinely costs 0."""
    venue = VENUES.get(venue_id)
    if venue is None:
        raise GraphError("invalid_input", "unknown venue %r" % venue_id)
    if outfit_id not in OUTFITS:
        raise GraphError("invalid_input", "unknown outfit %r" % outfit_id)
    if honeymoon_id not in HONEYMOONS:
        raise GraphError("invalid_input",
                         "unknown honeymoon %r" % honeymoon_id)
    if not isinstance(guests, int) or isinstance(guests, bool) \
            or not 0 <= guests <= 500:
        raise GraphError("invalid_input",
                         "guests must be an integer in [0, 500]")
    parts = []
    venue_cost = venue["base"] + venue["per_guest"] * guests
    parts.append(("venue", venue_cost))
    parts.append(("outfit", OUTFITS[outfit_id]["price"]))
    parts.append(("honeymoon", HONEYMOONS[honeymoon_id]["price"]))
    return parts


def plan_wedding(env, service, union_id, venue_id, guests, outfit_id,
                 honeymoon_id, turn):
    """Charge once per part, then mark the union's ceremony held. The
    union stays exactly as it was (recognition, prenup untouched)."""
    from src.game.features.family_graph import transitions
    from src.game.finance import Ledger
    graph = env["graph"]
    union = next((u for u in graph["unions"] if u["union_id"] == union_id),
                 None)
    if union is None:
        raise GraphError("not_found", "unknown union", 404)
    if union["status"] != "active":
        raise GraphError("graph_error", "union is not active", 409)
    if union["ceremony"].get("held"):
        raise GraphError("graph_error", "this union already had its "
                                       "ceremony", 409)
    _guard_union(env, service, union)
    parts = wedding_cost(venue_id, guests, outfit_id, honeymoon_id)
    player = env["player"]
    total = sum(amount for _, amount in parts)
    if total > 0 and player.money < total:
        raise GraphError(
            "not_allowed",
            f"the wedding costs ${total:,} and you have ${player.money:,}",
            409)
    ledger = Ledger(env, resolver=service._make_resolver(env))
    turn_id = env["turn"].get("turn_id", f"t_{env['revision'] + 1:06d}")
    money_entries = []
    for item, amount in parts:
        if amount > 0:
            money_entries.append({
                "phase": "weddings", "occurrence_id": f"wed:{union_id}",
                "leg": item, "account_owner_id": player.person_id,
                "category": "wedding", "amount": -amount,
                "description": "Wedding %s (%s)" % (item, union_id),
                "group_id": f"wed:{union_id}:{turn}"})
    ledger.post_many(turn_id, player.age, money_entries)
    # age (not revision) is the year clock: commands within one year
    # (resolves etc.) must not advance the anniversary calendar
    union["ceremony"] = {"held": True, "turn": turn,
                         "age": player.age,
                         "venue_id": venue_id, "guests": guests,
                         "outfit_id": outfit_id,
                         "honeymoon_id": honeymoon_id}
    people = transitions._people(env)
    names = [people.get(pid).name if people.get(pid) else pid
             for pid in union["person_ids"]]
    return {"ok": True, "ids": {"union_id": union_id},
            "journal": [{"kind": "wedding",
                         "text": "%s and %s celebrated their wedding at "
                                 "the %s." % (names[0], names[1],
                                              VENUES[venue_id]["label"]
                                              .lower()),
                         "person_ids": list(union["person_ids"])}],
            "ledger_requests": []}


def renew_vows(env, service, union_id, turn, tier="backyard"):
    """Vow renewal for the SELECTED union. Free tier always available."""
    from src.game.features.family_graph import transitions
    tiers = {"backyard": 0, "dinner": 400, "extravaganza": 4000}
    if tier not in tiers:
        raise GraphError("invalid_input", "unknown renewal tier")
    graph = env["graph"]
    union = next((u for u in graph["unions"] if u["union_id"] == union_id),
                 None)
    if union is None:
        raise GraphError("not_found", "unknown union", 404)
    if union["status"] != "active":
        raise GraphError("graph_error", "union is not active", 409)
    cost = tiers[tier]
    _guard_union(env, service, union)
    player = env["player"]
    money_entries = []
    if cost:
        if player.money < cost:
            raise GraphError(
                "not_allowed",
                f"the renewal costs ${cost:,} and you have "
                f"${player.money:,}", 409)
        money_entries.append({
            "phase": "weddings", "occurrence_id": f"vows:{union_id}",
            "leg": "main", "account_owner_id": player.person_id,
            "category": "wedding", "amount": -cost,
            "description": "Vow renewal (%s)" % union_id})
    if money_entries:
        from src.game.finance import Ledger
        ledger = Ledger(env, resolver=service._make_resolver(env))
        turn_id = env["turn"].get("turn_id", f"t_{env['revision'] + 1:06d}")
        ledger.post_many(turn_id, player.age, money_entries)
    state = env["weddings"]
    state["renewals"][union_id] = state["renewals"].get(union_id, 0) + 1
    player.change_happiness(3)
    return {"ok": True, "ids": {"union_id": union_id},
            "journal": [{"kind": "wedding",
                         "text": "Vows renewed, promises made fresh.",
                         "person_ids": list(union["person_ids"])}],
            "ledger_requests": []}


def divorce_union(env, service, union_id, turn):
    """Divorce the SELECTED union among several. Settlement = the cash
    prenup amount (once), or $0 when no prenup exists. Shared dependent
    children gain a support obligation + one-time posting. Other unions,
    parentage and history are untouched."""
    from src.game.features.family_graph import transitions
    from src.game.finance import Ledger
    from src.game.features.family_graph import dependent_children
    graph = env["graph"]
    union = next((u for u in graph["unions"] if u["union_id"] == union_id),
                 None)
    if union is None:
        raise GraphError("not_found", "unknown union", 404)
    if union["status"] != "active":
        raise GraphError("graph_error", "union is not active", 409)
    player = env["player"]
    if player.person_id not in union["person_ids"]:
        raise GraphError("not_allowed", "this is not your union", 409)
    other_id = union["person_ids"][0] \
        if union["person_ids"][1] == player.person_id \
        else union["person_ids"][1]
    ledger = Ledger(env, resolver=service._make_resolver(env))
    turn_id = env["turn"].get("turn_id", f"t_{env['revision'] + 1:06d}")
    entries = []
    prenup = union.get("prenup")
    settlement = int(prenup["amount"]) if prenup else 0
    if settlement > 0:
        if player.money < settlement:
            raise GraphError(
                "not_allowed",
                f"the settlement is ${settlement:,} and you have "
                f"${player.money:,}", 409)
        entries.append({
            "phase": "weddings", "occurrence_id": f"divorce:{union_id}",
            "leg": "settlement", "account_owner_id": player.person_id,
            "category": "wedding", "amount": -settlement,
            "description": "Divorce settlement (prenup, %s)" % union_id})
        entries.append({
            "phase": "weddings", "occurrence_id": f"divorce:{union_id}",
            "leg": "settlement_received", "account_owner_id": other_id,
            "category": "wedding", "amount": settlement,
            "description": "Divorce settlement received (%s)" % union_id})
    shared = [cid for cid in dependent_children(graph, player.person_id)
              if cid in dependent_children(graph, other_id)]
    remaining = player.money - settlement
    outstanding = {}
    for child_id in shared:
        paid = min(max(remaining, 0), CHILD_SUPPORT_PER_CHILD)
        remaining -= paid
        outstanding[child_id] = CHILD_SUPPORT_PER_CHILD - paid
        if paid:
            for owner, leg, amount in ((player.person_id, "paid", -paid),
                                       (child_id, "received", paid)):
                entries.append({
                    "phase": "weddings",
                    "occurrence_id": f"divorce:{union_id}:support:{child_id}",
                    "leg": leg, "account_owner_id": owner,
                    "category": "child_net", "amount": amount,
                    "description": "Child support (%s)" % child_id})
    for entry in entries:
        entry["group_id"] = f"divorce:{union_id}"
    ledger.post_many(turn_id, player.age, entries)
    transitions.end_union_op(env, union_id, "divorce", turn)
    if shared:
        graph["households"].append({
            "household_id": "hh-div-%s" % union_id[-6:],
            "resident_ids": [other_id],
            "dependent_ids": list(shared),
            "guardians": {cid: [other_id] for cid in shared},
            "support_obligations": [{
                "from_id": player.person_id, "to_id": cid,
                "kind": "child_support",
                "amount": CHILD_SUPPORT_PER_CHILD,
                "outstanding": outstanding[cid],
                "union_id": union_id, "until_turn": None} for cid in
                shared]})
    people = transitions._people(env)
    other = people.get(other_id)
    return {"ok": True, "ids": {"union_id": union_id},
            "journal": [{"kind": "wedding",
                         "text": "You and %s divorced%s." % (
                             other.name if other else "your spouse",
                             " (settlement $%s per the prenup)"
                             % f"{settlement:,}" if settlement else ""),
                         "person_ids": list(union["person_ids"])}],
            "ledger_requests": []}


def anniversary_check(env, service):
    """year_start hook (A's phase): one journal per union per milestone
    year (1/5/10/...), small happiness delta, tracked once."""
    graph = env["graph"]
    age = env["player"].age
    state = env["weddings"]
    journals = []
    for union in graph["unions"]:
        ceremony = union.get("ceremony") or {}
        if not ceremony.get("held"):
            continue
        years = age - ceremony.get("age", age)
        if years <= 0:
            continue
        milestone = max((m for m in ANNIVERSARY_MILESTONES if m <= years),
                        default=None)
        if milestone is None:
            continue
        recorded = state["anniversaries"].setdefault(
            union["union_id"], [])
        if milestone in recorded:
            continue
        recorded.append(milestone)
        env["player"].change_happiness(2)
        journals.append({"kind": "wedding",
                         "text": "Wedding anniversary: %d year%s married."
                                 % (milestone, "s" if milestone > 1
                                    else ""),
                         "person_ids": list(union["person_ids"])})
    return journals
