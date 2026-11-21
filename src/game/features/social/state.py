"""M4 social engine (A-owned, docs/collab/M4/02-contracts-A.md).

State envelope, single typed appliers (the ONLY social-state mutators),
accessors for C's v3 condition leaves, and popularity. Content never
mutates social state directly; everything measurable is person_id keyed.
"""

KINDS = ("acquaintance", "friend", "best_friend", "rival", "bully")
STATUSES = ("active", "faded", "estranged")
INVITE_KINDS = ("party", "study_group", "after_work", "team_practice")
CLUB_MEMBER_CAP = 15
POPULARITY_HISTORY_CAP = 20
MEMORY_CAP = 20


def empty_state():
    return {"schema_version": 1, "next_num": 1,
            "people": {}, "best_friend": None,
            "popularity": {"score": 0, "phase": "child", "last_score": 0,
                           "bonus": 0, "history": []},
            "invites": {}, "arcs": {}}


def ensure(env):
    st = env.get("social_state")
    if not isinstance(st, dict) or st.get("schema_version") != 1:
        st = empty_state()
        env["social_state"] = st
    st.setdefault("people", {})
    st.setdefault("best_friend", None)
    pop = st.setdefault("popularity", {"score": 0, "phase": "child",
                                       "last_score": 0, "history": []})
    pop.setdefault("history", [])
    st.setdefault("invites", {})
    st.setdefault("arcs", {})
    return st


def record(env, person_id, create=False):
    st = ensure(env)
    rec = st["people"].get(person_id)
    if rec is None and create:
        # plain circle members default to acquaintance so the UI never
        # receives a null kind (review fix)
        rec = {"kind": "acquaintance", "closeness": 0, "status": "active",
               "since_turn": env.get("turn", {}).get("turn_id")
                            or f"rev{env['revision']}",
               "source": {"type": "event", "ref": None},
               "met_turn": env.get("turn", {}).get("turn_id")
                           or f"rev{env['revision']}",
               "contact_turn": None, "memory": []}
        st["people"][person_id] = rec
    return rec


def _turn(env):
    return env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"


def apply_kind(env, person, target_id, kind):
    """Single kind-transition applier. Typed errors; exclusivity of
    best_friend enforced here; journaled by callers/events. kind=None
    clears the edge back to plain circle member."""
    if kind is not None and kind not in KINDS:
        raise ValueError(f"unknown social kind: {kind!r}")
    st = ensure(env)
    rec = record(env, target_id, create=True)
    if kind == "best_friend":
        old = st.get("best_friend")
        if old and old != target_id and old in st["people"]:
            st["people"][old]["kind"] = "friend"
        st["best_friend"] = target_id
    elif st.get("best_friend") == target_id:
        st["best_friend"] = None
    rec["kind"] = kind
    if rec["status"] == "estranged" and kind in ("friend", "best_friend"):
        rec["status"] = "active"
    return rec


def apply_closeness(env, person, target_id, delta, why=None,
                    journal=None, mark_contact=True):
    """Single closeness applier: clamped 0-100, actual-delta journal,
    contact timestamp (drift input)."""
    if not isinstance(delta, int) or isinstance(delta, bool):
        raise ValueError("closeness delta must be an integer")
    rec = record(env, target_id, create=True)
    before = rec["closeness"]
    rec["closeness"] = max(0, min(100, before + delta))
    actual = rec["closeness"] - before
    if mark_contact and delta > 0:
        rec["contact_turn"] = _turn(env)
    if journal is not None and actual:
        svc, kind_text = journal
        svc._journal(env, "relationship",
                     f"{kind_text} (we're at {rec['closeness']}/100).",
                     [person.person_id, target_id],
                     {"relationship": actual})
    return actual


def apply_popularity_delta(env, person, delta, why=None):
    if not isinstance(delta, int) or isinstance(delta, bool):
        raise ValueError("popularity delta must be an integer")
    delta = max(-20, min(20, delta))
    pop = ensure(env)["popularity"]
    pop["score"] = max(0, min(100, pop["score"] + delta))
    # the bonus accumulator keeps event-applied deltas across yearly
    # recomputes (formula + bonus), bounded like a single call
    pop["bonus"] = max(-30, min(30, pop.get("bonus", 0) + delta))
    return delta


def popularity_score(env):
    return ensure(env)["popularity"]["score"]


def record_memory(env, person, target_id, text, sentiment=0,
                  player_known=True):
    """Memory lines ride knowledge facts (audience = the friend); the
    bounded envelope index keeps the renderable text + privacy flag."""
    if not isinstance(text, str) or not text:
        raise ValueError("memory text must be a non-empty string")
    from src.game.features.knowledge import core as knowledge
    fact_id = knowledge.mint_fact(env, "social_memory",
                                  [person.person_id, target_id],
                                  _turn(env))
    knowledge.learn(env, target_id, fact_id, _turn(env), "witnessed")
    rec = record(env, target_id, create=True)
    rec["memory"].append({"fact_id": fact_id, "text": text,
                          "turn": _turn(env), "sentiment": sentiment,
                          "player_known": bool(player_known)})
    del rec["memory"][:-MEMORY_CAP]
    return fact_id


# ------------------------------------------------------------- arcs (bullying)

def arc_stage(env, actor_id, other_id):
    """C accessor: bullying arc stage between the two people, or None."""
    arc = ensure(env)["arcs"].get(other_id)
    if not arc or actor_id not in (arc.get("target_id"),):
        return None
    return arc.get("stage")


def set_arc(env, actor_id, other_id, stage, target_id=None):
    st = ensure(env)
    st["arcs"][other_id] = {"stage": stage, "since_turn": _turn(env),
                            "target_id": target_id or actor_id}
    rec = record(env, other_id, create=True)
    if stage in ("targeted", "escalated"):
        rec["kind"] = "bully"
    elif stage == "ended" and rec["kind"] == "bully":
        rec["kind"] = "acquaintance"
    return st["arcs"][other_id]


# ------------------------------------------------------- clubs (C data gates)

def load_clubs():
    """C-owned clubs data, read through C's content loader (single
    source). Returns {id: club}; {} when the catalog is absent."""
    try:
        from src.game.content import social as social_content
        data = social_content.load_clubs()
    except Exception:
        return {}
    clubs = data.get("clubs") if isinstance(data, dict) else None
    if not clubs:
        return {}
    return {c["id"]: c for c in clubs if isinstance(c, dict)
            and c.get("id")}


def _social_resolver(env):
    def resolve(owner_id):
        candidates = [env.get("player")]
        player = env.get("player")
        if player is not None:
            candidates.extend(getattr(player, "relations", []) or [])
        candidates.extend(env.get("archive", []) or [])
        candidates.extend(env.get("social_people", []) or [])
        for person in candidates:
            if getattr(person, "person_id", None) == owner_id:
                return person
        return None
    return resolve


def join_club(env, person, club_id):
    """The ONLY writer of club membership. Re-validates C's data gates
    BEFORE any mutation/spend (C's command validates too — advertised ==
    dispatched); join fee posts through the one ledger poster with the
    frozen per-year occurrence key; leave is always free."""
    if not isinstance(club_id, str) or not club_id:
        raise ValueError("club_id must be a non-empty string")
    clubs = load_clubs()
    club = clubs.get(club_id)
    if club is None:
        raise PermissionError("that club is not open yet"
                              if not clubs else
                              f"unknown club: {club_id}")
    st = ensure(env)
    circles = st.setdefault("circles", {}).setdefault("clubs", {})
    member_ids = circles.setdefault(club_id, {}).setdefault("member_ids",
                                                            [])
    if person.person_id in member_ids:
        raise PermissionError("you are already a member of that club")
    if club.get("adults_only") and person.age < 18:
        raise PermissionError("you must be 18 or older to join")
    if club.get("min_age") and person.age < club["min_age"]:
        raise PermissionError(
            f"you must be at least {club['min_age']} to join")
    if club.get("max_age") and person.age > club["max_age"]:
        raise PermissionError(
            f"this club is for ages {club['min_age']}-"
            f"{club['max_age']}")
    if len(member_ids) >= club.get("member_cap", CLUB_MEMBER_CAP):
        raise PermissionError("that club is full")
    fee = int(club.get("cost_per_year") or 0)
    if fee and person.money < fee:
        raise PermissionError(f"joining costs ${fee} and you have "
                              f"${person.money}")
    member_ids.append(person.person_id)
    if fee:
        from src.game.finance import Ledger
        turn = env.get("turn", {}).get("turn_id") or \
            f"rev{env['revision']}"
        Ledger(env, resolver=_social_resolver(env)).post(
            turn, person.age, {
                "phase": "social",
                "occurrence_id": f"club:{club_id}:{person.person_id}:"
                                 f"{person.age}",
                "leg": "club_fee", "account_owner_id": person.person_id,
                "category": "other", "amount": -fee,
                "description": f"Joined {club['label']}"})
    apply_popularity_delta(env, person,
                           int(club.get("popularity_delta", 0)))
    return club


def leave_club(env, person, club_id):
    st = ensure(env)
    circles = st.get("circles", {}).get("clubs", {})
    member_ids = (circles.get(club_id) or {}).get("member_ids", [])
    if person.person_id not in member_ids:
        raise PermissionError("you are not a member of that club")
    member_ids.remove(person.person_id)
    if not member_ids:
        circles.pop(club_id, None)
    return True


def in_club(env, actor_id, club_id):
    return club_id in ensure(env).get("circles", {}).get("clubs", {})


# --------------------------------------------- accessors for C's v3 leaves

def edge(env, actor_id, other_id):
    return ensure(env)["people"].get(other_id)


def in_circle_group(env, actor_id, group_id):
    """`in_circle` leaf: >=1 person in the frozen group bucket."""
    from src.game.features.social import circles as circles_mod
    return bool(circles_mod.group_member_ids(env, group_id))
