"""M4 social v3 role predicates — A's integration registrations.

C's stubs in src/game/events.py explicitly invite A to re-register
richer versions at integration; these read A's social envelope through
A's accessors and bind roles for C's soc_* events. Plain circle members
(classmates/coworkers WITHOUT a kind) are valid bind candidates —
otherwise nothing can bind before friendship exists (the inert-engine
loop the real-play review found).
"""

from src.game import events as event_mod
from src.game.features.social import state as social


def _pick(actor_id, context, *, kinds=None, circle=None, need_kind=False):
    env = getattr(context, "_env", None)
    if not isinstance(env, dict):
        return None
    from src.game.features.social import circles as circles_mod
    if circle == "school":
        candidates = circles_mod.group_member_ids(env, "classmates")
    elif circle == "work":
        candidates = circles_mod.group_member_ids(env, "coworkers")
    else:
        candidates = [pid for pid, rec in
                      social.ensure(env)["people"].items()
                      if rec.get("status", "active") == "active"]
    best = None
    best_score = -1
    for pid in candidates:
        if pid == actor_id:
            continue
        rec = social.ensure(env)["people"].get(pid) or {}
        if kinds is not None and rec.get("kind") not in kinds:
            continue
        if need_kind and not rec.get("kind"):
            continue
        score = rec.get("closeness") or 0
        if score > best_score:
            best, best_score = pid, score
    return best


def _bully(actor_id, context):
    env = getattr(context, "_env", None)
    if not isinstance(env, dict):
        return None
    st = social.ensure(env)
    for pid, rec in st["people"].items():
        if pid == actor_id or rec.get("kind") != "bully":
            continue
        if rec.get("status", "active") != "active":
            continue
        arc = st["arcs"].get(pid) or {}
        if arc.get("stage") in ("ended", "resolving"):
            continue
        return pid
    return None


def _host(actor_id, context):
    env = getattr(context, "_env", None)
    if not isinstance(env, dict):
        return None
    st = social.ensure(env)
    invites = [i for i in st["invites"].values()
               if i.get("status") == "open" and i.get("host_id")]
    if invites:
        invites.sort(key=lambda i: i.get("offered_turn") or "",
                     reverse=True)
        return invites[0]["host_id"]
    return _pick(actor_id, context, kinds=("friend", "best_friend"))


event_mod.register_predicate(
    "social_classmate_of_actor",
    lambda a, c: _pick(a, c, circle="school"))
event_mod.register_predicate(
    "social_coworker_of_actor",
    lambda a, c: _pick(a, c, circle="work"))
event_mod.register_predicate(
    "social_friend_of_actor",
    lambda a, c: _pick(a, c, kinds=("friend", "best_friend")))
event_mod.register_predicate(
    "social_acquaintance_of_actor",
    lambda a, c: _pick(a, c, need_kind=True))
event_mod.register_predicate("school_bully_of_actor", _bully)
event_mod.register_predicate("social_host_of_actor", _host)
