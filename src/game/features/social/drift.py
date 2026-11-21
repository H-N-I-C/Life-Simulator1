"""Yearly social hook (A-owned; ONE coordinator registry line, frozen
slot after relations_age): shared-time accrual, kind promotion,
drift/fall-out for contacts WITHOUT shared time this year, organic
bullying-arc starts, popularity recompute, invite expiry, and circle
reconciliation.

Frozen balance rules (Amendment 2 clarifications, play-balanced):
- Growth comes from SHARED TIME, not from mere co-membership: each
  year every circle member gets the small daily-contact rapport
  (classmates +2, coworkers +2), and ONE hash-picked member per circle
  gets the year's real shared time on top (+12, contact marked).
  Events/actions that call apply_closeness are shared time too.
- Kinded active contacts: shared time this year -> +1 slow burn; no
  shared time -> -3 drift. Acquaintances with no shared time drift -1.
  Faded contacts: -1 (floor 0, never auto-estranged). Rivals: -2 (no
  auto-demote while an arc is active).
- Kind thresholds: plain member -> acquaintance at 10, acquaintance ->
  friend at 22, friend -> best_friend at 70 (one at a time).
- Organic bullying: while at school (ages 6-15) with classmates and no
  active arc, a deterministic 12%-per-year hash roll starts an arc via
  bullying.start_bullying_arc. Zero rng draws anywhere in this hook.
- Popularity recomputes from stored state; open invites older than the
  current turn expire; circles reconcile for the new year.
"""

import hashlib

NO_CONTACT_DECAY = 3
BEST_FRIEND_FLOOR = 30
FRIEND_FLOOR = 18
RIVAL_DECAY = 2
FADED_DECAY = 1
CIRCLE_RAPPORT = 2        # daily-contact baseline per circle member
PICKED_SHARED_TIME = 14   # the sticky pal's yearly shared time
ROTATING_SHARED_TIME = 10  # the rotating second pick
ACQUAINTANCE_AT = 10
FRIEND_AT = 22
BEST_FRIEND_AT = 70
ARC_LIFE_CHANCE = 30   # percent of SCHOOL LIVES that see bullying (once per life)
ARC_DUE_MIN_AGE = 8
ARC_DUE_MAX_AGE = 13


def _promote(env, service, rec, pid):
    from src.game.features.social import state as social
    st = social.ensure(env)
    player_id = env["player"].person_id
    closeness = rec["closeness"]
    if rec["kind"] is None and closeness >= ACQUAINTANCE_AT:
        rec["kind"] = "acquaintance"
    elif rec["kind"] == "acquaintance" and closeness >= FRIEND_AT:
        social.apply_kind(env, env["player"], pid, "friend")
        service._journal(env, "social",
                         f"{_name(env, pid)} and I became friends this "
                         f"year.", [player_id, pid], {})
    elif rec["kind"] == "friend" and closeness >= BEST_FRIEND_AT \
            and not st.get("best_friend"):
        social.apply_kind(env, env["player"], pid, "best_friend")
        service._journal(env, "social",
                         f"{_name(env, pid)} is my best friend now.",
                         [player_id, pid], {})


def _name(env, pid):
    for person in env.get("social_people", []):
        if person.person_id == pid:
            return person.name
    return "Someone"


def _shared_time_pick(turn, count):
    # deterministic per (turn, circle size); indexes the stable member
    # ORDER (never uuid values) so same-seed twin lives stay identical
    digest = hashlib.sha256(f"shared:{turn}:{count}".encode(
        "utf-8")).digest()
    return int.from_bytes(digest[:4], "big") % count


def arc_due(env):
    """Per-LIFE deterministic bullying verdict (zero rng): ~30% of
    school lives get an arc, due at a fixed hashed age 8-13. Returns
    the due age or None."""
    basis = env.get("creation", {}).get("fingerprint", "")
    digest = hashlib.sha256(f"{basis}:arc-life".encode("utf-8")).digest()
    if digest[0] % 100 >= ARC_LIFE_CHANCE:
        return None
    span = ARC_DUE_MAX_AGE - ARC_DUE_MIN_AGE + 1
    return ARC_DUE_MIN_AGE + digest[1] % span


def _organic_arc_start(env, service, turn):
    """Start the arc in the due year of a due life (once per life)."""
    from src.game.features.social import bullying as bullying_mod
    from src.game.features.social import circles as circles_mod
    from src.game.features.social import state as social
    player = env["player"]
    st = social.ensure(env)
    if st.get("arc_rolled"):
        return
    due = arc_due(env)
    if due is None or player.age != due:
        return
    st["arc_rolled"] = True
    if any(a.get("stage") in ("targeted", "escalated")
           for a in st["arcs"].values()):
        return
    if not circles_mod.group_member_ids(env, "classmates"):
        return
    bullying_mod.start_bullying_arc(env, service, player)


def annual_hook(env, service, rng):
    from src.game.features.social import circles as circles_mod
    from src.game.features.social import invites as invites_mod
    from src.game.features.social import popularity as popularity_mod
    from src.game.features.social import state as social
    st = social.ensure(env)
    turn = env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"
    player_id = env["player"].person_id

    # 1. shared time: daily-contact rapport for every member; the
    #    cohort's STICKY PAL gets the year's real shared time every
    #    year (guarantees ordinary play yields a friend by 18); a
    #    rotating second pick adds variety (2-4 friends typical)
    basis = env.get("creation", {}).get("fingerprint", "")
    for gid in ("classmates", "coworkers"):
        members = [pid for pid in circles_mod.group_member_ids(env, gid)
                   if st["people"].get(pid, {}).get("status")
                   == "active"]
        if not members:
            continue
        for pid in members:
            rec = st["people"][pid]
            rec["closeness"] = min(100, rec["closeness"]
                                   + CIRCLE_RAPPORT)
        pal_digest = hashlib.sha256(
            f"{basis}:pal:{gid}:{len(members)}".encode("utf-8")
        ).digest()
        pal = members[int.from_bytes(pal_digest[:4], "big")
                      % len(members)]
        for pid, bonus in ((pal, PICKED_SHARED_TIME),
                           (members[_shared_time_pick(turn,
                                                      len(members))],
                            ROTATING_SHARED_TIME)):
            if pid == pal and bonus == ROTATING_SHARED_TIME:
                continue
            rec = st["people"][pid]
            rec["closeness"] = min(100, rec["closeness"] + bonus)
            rec["contact_turn"] = turn
            _promote(env, service, rec, pid)

    # 2. kinded contacts WITHOUT shared time this year drift
    for pid, rec in list(st["people"].items()):
        kind = rec["kind"]
        if kind is None:
            continue
        contacted = rec.get("contact_turn") == turn
        if rec["status"] == "faded":
            rec["closeness"] = max(0, rec["closeness"] - FADED_DECAY)
            continue
        if kind == "acquaintance" and not contacted:
            rec["closeness"] = max(0, rec["closeness"] - 1)
            continue
        if kind in ("friend", "best_friend"):
            if contacted:
                rec["closeness"] = min(100, rec["closeness"] + 1)
            else:
                rec["closeness"] = max(
                    0, rec["closeness"] - NO_CONTACT_DECAY)
                delta = -NO_CONTACT_DECAY
                if kind == "best_friend" and rec["closeness"] \
                        < BEST_FRIEND_FLOOR:
                    rec["kind"] = "friend"
                    if st.get("best_friend") == pid:
                        st["best_friend"] = None
                    service._journal(env, "relationship",
                                     "We drifted apart a little this "
                                     "year.", [player_id, pid],
                                     {"relationship": delta})
                elif kind == "friend" and rec["closeness"] < FRIEND_FLOOR:
                    rec["kind"] = "acquaintance"
                    service._journal(env, "relationship",
                                     "We fell out of touch this year.",
                                     [player_id, pid],
                                     {"relationship": delta})
        elif kind == "rival":
            arc = st["arcs"].get(pid)
            active_arc = arc and arc.get("stage") in ("targeted",
                                                      "escalated")
            if not active_arc:
                rec["closeness"] = max(0, rec["closeness"]
                                       - RIVAL_DECAY)

    # 3. organic bullying-arc onset for school lives (deterministic)
    _organic_arc_start(env, service, turn)

    invites_mod.expire_old(env, turn)
    popularity_mod.recompute(env, env["player"])
    circles_mod.reconcile(env, service, rng)
