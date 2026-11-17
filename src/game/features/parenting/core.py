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
