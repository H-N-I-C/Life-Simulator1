"""Bullying arcs: A owns arc STATE and its advance; C owns the stage
table (src/game/content/social/bullying.json). The frozen default
table (02-contracts-C.md example) applies until C's file lands; a
present C file overrides it. The `bullying_response` domain effect is
applied at resolve through `apply_response` (one outcome table, two
entry points: event options and C's proactive rule actions).
"""

import json
import os

DEFAULT_TABLE = {
    "stages": ["targeted", "escalated", "resolving", "ended"],
    "responses": {
        "tell_someone": {"to_stage": "resolving", "karma": 1,
                         "fact": "sought_help"},
        "stand_up": {"to_stage": "resolving", "health_min": 40,
                     "fail_to_stage": "escalated"},
        "avoid": {"to_stage": None, "stress": -2},
        "befriend": {"to_stage": "ended", "edge_to": "acquaintance"}}}

_TABLE = None


def table():
    global _TABLE
    if _TABLE is None:
        path = os.path.join(os.path.dirname(__file__), "..", "..",
                            "content", "social", "bullying.json")
        try:
            with open(os.path.abspath(path), encoding="utf-8") as f:
                data = json.load(f)
            if data.get("schema_version") == 1 and data.get("responses"):
                _TABLE = data
            else:
                _TABLE = DEFAULT_TABLE
        except (OSError, ValueError):
            _TABLE = DEFAULT_TABLE
    return _TABLE


def start_arc(env, actor_id, bully_id, stage="targeted"):
    from src.game.features.social import state as social
    return social.set_arc(env, actor_id, bully_id, stage,
                          target_id=actor_id)


def start_bullying_arc(env, service, person):
    """Assign a bully from the current school circle and open a
    targeted arc (the entry C's school arc content uses). The pick is
    seed-stable (hash of creation fingerprint + counter), zero rng
    draws; journaled. Returns the bully person_id, or None when there
    is no school circle to draw from."""
    from src.game.features.social import circles as circles_mod
    from src.game.features.social import state as social
    candidates = [pid for pid in
                  circles_mod.group_member_ids(env, "classmates")
                  if social.ensure(env)["people"].get(pid, {})
                  .get("status", "active") == "active"]
    if not candidates:
        return None
    st = social.ensure(env)
    import hashlib
    basis = env.get("creation", {}).get("fingerprint", "")
    digest = hashlib.sha256(
        f"{basis}:bully:{st['next_num']}".encode("utf-8")).digest()
    bully_id = candidates[int.from_bytes(digest[:4], "big")
                          % len(candidates)]
    start_arc(env, person.person_id, bully_id)
    service._journal(env, "social",
                     f"{_bully_name(env, bully_id)} has started picking "
                     f"on me.", [person.person_id, bully_id], {})
    return bully_id


def _bully_name(env, bully_id):
    for person in env.get("social_people", []):
        if person.person_id == bully_id:
            return person.name
    return "Someone at school"


def register_domain_effects():
    """Idempotent: register the `bullying_response` v3 domain effect so
    C's event options and C's proactive actions share A's one outcome
    table (Amendment 2). Called from A-owned seams once rule_actions is
    importable."""
    from src.menus import rule_actions
    if "bullying_response" in rule_actions._DOMAIN_EFFECTS:
        return

    def _bullying_response(context, player, bindings, value, rng,
                           occurrence_id=None):
        env = getattr(context, "_env", None)
        if not isinstance(env, dict):
            return [], []
        target = (bindings or {}).get("bully")
        if target is None:
            st = ensure_st = env.get("social_state") or {}
            for pid, arc in (st.get("arcs") or {}).items():
                if arc.get("target_id") == player.person_id \
                        and arc.get("stage") in ("targeted",
                                                 "escalated"):
                    target = pid
                    break
        if target is None:
            raise rule_actions.RuleError(
                "not_available", "no active bullying arc to respond to")
        service = getattr(context, "_service", None)
        result = apply_response(env, service, player, value, target)
        return result.get("events") or [], result.get("money_entries") \
            or []

    rule_actions.register_domain_effect("bullying_response",
                                        _bullying_response)


def apply_response(env, service, person, response, bully_id,
                   outcome_text=None):
    """Apply one frozen table row. Deterministic rules only (no rng):
    tell_someone always works; stand_up checks the health floor and
    escalates on failure. Journals the ACTUAL applied deltas."""
    from src.game.features.social import state as social
    row = table()["responses"].get(response)
    if row is None:
        raise ValueError(f"unknown bullying response: {response!r}")
    st = social.ensure(env)
    arc = st["arcs"].get(bully_id)
    changes = {}
    events = []
    if response == "stand_up":
        ok = person.health >= (row.get("health_min") or 0)
        stage = row["to_stage"] if ok else row.get("fail_to_stage")
        if not ok:
            person.change_health(-5)
            changes["health"] = -5
        social.set_arc(env, person.person_id, bully_id, stage)
    elif response == "avoid":
        if row.get("stress"):
            before = person.stress
            person.change_stress(row["stress"])
            changes["stress"] = person.stress - before
        # no stage advance
    else:
        social.set_arc(env, person.person_id, bully_id,
                       row.get("to_stage"))
    if row.get("karma"):
        person.change_karma(row["karma"])
        changes["karma"] = row["karma"]
    if response == "befriend":
        social.apply_kind(env, person, bully_id,
                          row.get("edge_to") or "acquaintance")
        rec = social.record(env, bully_id, create=True)
        social.apply_closeness(env, person, bully_id, 10)
        st["arcs"].pop(bully_id, None)
    if row.get("fact"):
        social.record_memory(env, person, bully_id,
                             f"When it got bad, I {response.replace('_', ' ')}.",
                             sentiment=1)
    events.append({"kind": "social",
                   "person_ids": [person.person_id, bully_id],
                   "text": outcome_text or f"I {response.replace('_', ' ')}.",
                   "changes": changes})
    return {"events": events, "money_entries": []}
