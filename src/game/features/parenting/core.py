"""Parenting core (M2.5, Kimi A): styles, personalities, milestones.

Envelope block env["parenting"]:
    {"styles": {child_id: style_id},
     "personalities": {child_id: {"energy": 0-100, "compliance": 0-100,
                                  "hobby": id}},
     "milestones": {child_id: {milestone_id: {"turn": int, "record": ...}}},
     "seen": ["<event_id>:<child_id>", ...]}
Children are non-romantic dependents; nothing here attaches romance to a
minor. Personality/style generation is deterministic per (game, child)
and NEVER drawn from the gameplay RNG stream.
"""

import hashlib
import random

STYLES = ("balanced", "warm_firm", "permissive", "hands_off")
DEFAULT_STYLE = "balanced"

PERSONALITY_HOBBIES = ("music", "sport", "books", "art", "games",
                       "nature", "building")


def new_parenting_state():
    return {"styles": {}, "personalities": {}, "milestones": {},
            "seen": []}


def normalize_parenting(env):
    """Backfill the envelope block. Returns True when changed."""
    state = env.get("parenting")
    if not isinstance(state, dict):
        env["parenting"] = new_parenting_state()
        return True
    dirty = False
    for key, default in (("styles", {}), ("personalities", {}),
                         ("milestones", {}), ("seen", [])):
        if not isinstance(state.get(key), type(default)):
            state[key] = default
            dirty = True
    return dirty


def _child_rng(env, child_id):
    digest = hashlib.sha256(
        ("parenting:%s:%s" % (env["game_id"], child_id)).encode()
    ).hexdigest()
    return random.Random(int(digest[:16], 16))


def person_children(env, service, person_id):
    """Dependent children of a person (deduped across unions/households;
    A's family_graph query)."""
    from src.game.features.family_graph import dependent_children
    return dependent_children(env["graph"], person_id)


def ensure_child_record(env, service, child_id):
    """Default style + deterministic personality for a dependent child.
    Idempotent; safe for any child id."""
    normalize_parenting(env)
    state = env["parenting"]
    dirty = False
    if child_id not in state["styles"]:
        state["styles"][child_id] = DEFAULT_STYLE
        dirty = True
    if child_id not in state["personalities"]:
        rng = _child_rng(env, child_id)
        state["personalities"][child_id] = {
            "energy": rng.randint(0, 100),
            "compliance": rng.randint(0, 100),
            "hobby": rng.choice(PERSONALITY_HOBBIES),
        }
        dirty = True
    return dirty


def backfill_all(env, service):
    """Ensure every dependent child of the player has style/personality."""
    dirty = normalize_parenting(env)
    player_id = env["player"].person_id
    for child_id in person_children(env, service, player_id):
        if ensure_child_record(env, service, child_id):
            dirty = True
    return dirty


def set_style(env, child_id, style_id):
    if style_id not in STYLES:
        from src.game.features.family_graph.graph import GraphError
        raise GraphError("invalid_input",
                         "unknown parenting style %r" % style_id)
    normalize_parenting(env)
    env["parenting"]["styles"][child_id] = style_id


def style_of(env, child_id):
    normalize_parenting(env)
    return env["parenting"]["styles"].get(child_id, DEFAULT_STYLE)


def record_milestone(env, child_id, milestone_id, turn, record=None):
    normalize_parenting(env)
    child = env["parenting"]["milestones"].setdefault(child_id, {})
    if milestone_id in child:
        return False  # milestones are once-per-child
    child[milestone_id] = {"turn": turn, "record": record}
    return True


def milestones_of(env, child_id):
    normalize_parenting(env)
    return dict(env["parenting"]["milestones"].get(child_id, {}))


def has_milestone(env, child_id, milestone_id):
    return milestone_id in env["parenting"]["milestones"].get(child_id, {})


def mark_seen(env, event_id, child_id):
    normalize_parenting(env)
    key = "%s:%s" % (event_id, child_id)
    if key in env["parenting"]["seen"]:
        return False
    env["parenting"]["seen"].append(key)
    return True


def is_seen(env, event_id, child_id):
    normalize_parenting(env)
    return "%s:%s" % (event_id, child_id) in env["parenting"]["seen"]


def child_style_modifier(env, child_id):
    """Soft rate modifier by parenting style (frozen calibration
    placeholder, fictional balance): warm_firm lowers teen-trouble
    rates, permissive/hands_off raise request rates. Never a gate."""
    return {
        "balanced": {"trouble": 1.0, "requests": 1.0},
        "warm_firm": {"trouble": 0.8, "requests": 0.9},
        "permissive": {"trouble": 1.2, "requests": 1.3},
        "hands_off": {"trouble": 1.4, "requests": 1.1},
    }[style_of(env, child_id)]


# ---------------------------------------------------- conception -> birth

# Bounded FICTIONAL health consequence of the ratified related-parent
# risk band; applied once at birth, never lethal, adoption never uses it.
# (Magnitude is game balance, flagged for Claude's ratification in the
# round-2 report; the risk bands themselves are frozen.)
RELATED_PARENT_HEALTH_DELTA = -5
RELATED_PARENT_HEALTH_FLOOR = 5


def conception_parents_of(env, child):
    """The child's bound biological parents as person ids: graph
    parentage edges first (authoritative), mother/father attrs as the
    legacy fallback. Adoption/social links are never included."""
    child_id = getattr(child, "person_id", child)
    parents = [e["parent_id"] for e in env["graph"]["parentage"]
               if e["child_id"] == child_id and e["role"] == "biological"]
    if not parents:
        for attr in ("mother", "father"):
            par = getattr(child, attr, None)
            pid = getattr(par, "person_id", None)
            if pid and pid not in parents:
                parents.append(pid)
    return parents


def find_conception_outcome(env, child):
    """The ONE recorded conception outcome bound to this child's
    biological parents, however it was stored (service child:request
    keys or v3 occurrence/pair keys). Returns (key, outcome) or
    (None, None). Never redraws.

    E5: a child's OWN pre-bound outcome (a service key prefixed with
    the child id) always wins — two children of the same parents must
    never consume each other's draw. Pair-keyed outcomes (recorded
    before the child existed, e.g. legacy/v3 conceptions) are consumed
    first-come-first-served in insertion order, one per birth.

    N2: a child that already consumed an outcome NEVER consumes again —
    a repeated lookup stops instead of stealing a later pregnancy's
    draw."""
    outcomes = env["graph"].get("conception_outcomes") or {}
    child_id = getattr(child, "person_id", child)
    if any(v.get("consumed_child_id") == child_id
           for v in outcomes.values()):
        return None, None
    parents = set(conception_parents_of(env, child))
    if not parents:
        return None, None
    own_prefix = "%s:" % child_id
    for key, outcome in outcomes.items():
        if outcome.get("consumed") or not key.startswith(own_prefix):
            continue
        bound = {outcome.get("bio_mother_id"), outcome.get("bio_father_id")}
        if bound == parents:
            return key, outcome
    other_children = {e["child_id"] for e in env["graph"]["parentage"]
                      if e["child_id"] != child_id}
    for key, outcome in outcomes.items():
        if outcome.get("consumed") or key.startswith(own_prefix):
            continue
        if key.split(":", 1)[0] in other_children:
            continue  # another child's pre-bound draw, never this one's
        bound = {outcome.get("bio_mother_id"), outcome.get("bio_father_id")}
        if bound == parents:
            return key, outcome
    return None, None


def record_legacy_conception(env, player, rng, pregnancy_id=None):
    """E4/N1: a legacy pregnancy that continues records its conception
    ONCE, keyed by the biological parent pair, so the birth transaction
    consumes exactly one immutable draw. An existing unconsumed outcome
    for the pair is reused — never rerolled — and bound to THIS
    pregnancy (N2: the birth resolves by immutable identity, never pair
    or insertion order). Adoption never routes here. Returns the
    storage key or None (no partner / ineligible pair)."""
    from src.game.features.family_graph.graph import (
        ADULT_AGE, sample_conception)
    from src.lifesim_lib.lifesim_lib import Gender
    partner = getattr(player, "partner", None)
    if partner is None:
        return None
    if getattr(player, "gender", None) == Gender.Female:
        mother, father = player, partner
    else:
        mother, father = partner, player
    if getattr(mother, "age", 0) < ADULT_AGE \
            or getattr(father, "age", 0) < ADULT_AGE:
        return None
    mid = getattr(mother, "person_id", None)
    fid = getattr(father, "person_id", None)
    if not mid or not fid or mid == fid:
        return None
    outcomes = env["graph"].setdefault("conception_outcomes", {})
    prefix = "legacy:%s:%s" % (mid, fid)
    pending_keys = [k for k in outcomes
                    if k.startswith(prefix) and not outcomes[k].get("consumed")]
    if pending_keys:
        key = pending_keys[0]
        player._active_conception_key = key
        return key
    index = sum(1 for k in outcomes if k.startswith(prefix))
    people = {}
    for person in (mother, father):
        people[getattr(person, "person_id", None)] = person
    outcome = sample_conception(env["graph"], mid, fid, rng,
                                persons_by_id=people)
    outcome["bio_mother_id"] = mid
    outcome["bio_father_id"] = fid
    if pregnancy_id:
        outcome["pregnancy_id"] = pregnancy_id
    key = "%s#%d" % (prefix, index)
    outcomes[key] = outcome
    player._active_conception_key = key
    return key


def terminate_active_conception(env, reason="miscarriage"):
    """W2: a pregnancy that ended WITHOUT a birth (miscarriage) retires
    its recorded draw: the bound outcome becomes terminated (never
    consumable by any child), keeps its original pregnancy_id for the
    audit trail, and the player's active binding is cleared — so the
    NEXT pregnancy records a NEW draw with its own immutable identity,
    never reusing the lost one. Idempotent; returns the outcome or
    None."""
    player = env.get("player")
    if player is None:
        return None
    key = getattr(player, "_active_conception_key", None)
    outcome = None
    outcomes = env["graph"].get("conception_outcomes") or {}
    if key is not None:
        candidate = outcomes.get(key)
        if candidate is not None and not candidate.get("consumed"):
            outcome = candidate
    player._active_conception_key = None
    if outcome is None:
        return None
    outcome["consumed"] = True
    outcome["terminated"] = reason
    return outcome


def apply_birth_conception(env, child, key=None):
    """Consume the recorded conception in the birth transaction: the
    stored draw is applied to actual child health exactly once.
    Returns the outcome dict, or None for adoption / unrecorded
    (legacy) births, which never roll and never take a penalty.

    N2: `key` is the immutable pregnancy->conception binding stashed on
    the player at keep time; when present (and valid for this child's
    bound parents) it wins over any pair-key lookup. A child that
    already consumed an outcome never consumes another."""
    if getattr(child, "adopted", False):
        return None
    outcomes = env["graph"].get("conception_outcomes") or {}
    child_id = getattr(child, "person_id", child)
    if any(record.get("consumed_child_id") == child_id
           for record in outcomes.values()):
        return None
    player = env.get("player")
    if key is None:
        key = getattr(player, "_active_conception_key", None)
    outcome = None
    if key is not None:
        candidate = outcomes.get(key)
        if candidate is not None and not candidate.get("consumed"):
            parents = set(conception_parents_of(env, child))
            if {candidate.get("bio_mother_id"),
                    candidate.get("bio_father_id")} == parents:
                outcome = candidate
    if outcome is None:
        key, outcome = find_conception_outcome(env, child)
        if outcome is None:
            return None
    if outcome.get("adjustment") == "related_parent_health_risk":
        floor = RELATED_PARENT_HEALTH_FLOOR
        delta = max(RELATED_PARENT_HEALTH_DELTA, floor - child.health)
        if delta < 0:
            child.change_health(delta)
    outcome["consumed"] = True
    outcome["consumed_child_id"] = getattr(child, "person_id", child)
    if player is not None \
            and getattr(player, "_active_conception_key", None) == key:
        player._active_conception_key = None
    return outcome
