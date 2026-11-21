"""Per-person skill state: deterministic defaults, backfill, annual reset.

State lives in env["skills"] (additive envelope key, pickled with the
save). Defaults are derived from a stable SEED IDENTITY — the person's
generation inputs (first/last name + gender), never the random UUID4
person_id and never rng — so two identically seeded lives get identical
starting talents, reads stay free and reproducible, and identity
addressing (person_id) is untouched. The backfilled value is persisted
on first write, so a person keeps their talent across reloads and only
ever gets one baseline.
"""

import hashlib

from src.game.features.skills import data

STATE_KEY = "skills"
HISTORY_CAP = 20


def stable_id_hash(seed_key, skill_id):
    digest = hashlib.sha256(("%s:%s" % (seed_key, skill_id))
                            .encode("utf-8")).hexdigest()
    return int(digest[:16], 16)


def default_value(seed_key, skill_def):
    return skill_def["default"] + stable_id_hash(seed_key, skill_def["id"]) % 6


def _person_for(env, person_id):
    """Resolve a person object by id from the reachable graph (player,
    their relations transitively, and the archive). Returns None when
    the id is unreachable (e.g. a pruned record)."""
    player = env.get("player")
    if player is None:
        return None
    stack = [player]
    seen = set()
    while stack:
        person = stack.pop()
        if id(person) in seen:
            continue
        seen.add(id(person))
        if getattr(person, "person_id", None) == person_id:
            return person
        stack.extend(getattr(person, "relations", []) or [])
        partner = getattr(person, "partner", None)
        if partner is not None:
            stack.append(partner)
    for person in env.get("archive", []) or []:
        if getattr(person, "person_id", None) == person_id:
            return person
    return None


def seed_key_for(env, person_id):
    """The stable seed identity for default generation: name + gender
    (reproducible for identical seeded lives). Falls back to the
    person_id ONLY when the person is unreachable from the graph, which
    never happens for the player."""
    person = _person_for(env, person_id)
    if person is None:
        return person_id
    return "%s|%s|%s" % (getattr(person, "firstname", ""),
                         getattr(person, "lastname", ""),
                         getattr(person, "gender", ""))


def state(env):
    """The feature state dict (created empty on first access)."""
    return env.setdefault(STATE_KEY, {"schema_version": 1, "people": {}})


def person_state(env, person_id, create=False):
    st = state(env)
    entry = st["people"].get(person_id)
    if entry is None and create:
        entry = {"values": {}, "trained_this_year": {}, "history": [],
                 "year": None, "count_this_year": {}}
        st["people"][person_id] = entry
    return entry


def values(env, person_id):
    """Read-only values; backfills defaults deterministically (no rng).
    The seed identity is resolved once and persisted with the entry, so
    old saves never reroll and reads stay cheap."""
    entry = person_state(env, person_id, create=True)
    seed = entry.get("seed")
    if seed is None:
        seed = seed_key_for(env, person_id)
        entry["seed"] = seed
    values_ = entry["values"]
    for s in data.load()["skills"]:
        if not isinstance(values_.get(s["id"]), int):
            values_[s["id"]] = default_value(seed, s)
    _roll_year(env, person_id, entry)
    return values_


def uses_this_year(env, person_id):
    entry = person_state(env, person_id, create=True)
    _roll_year(env, person_id, entry)
    return entry["trained_this_year"]


def _roll_year(env, person_id, entry):
    """trained_this_year is keyed to the player's current age, so a fresh
    year clears usage without a separate phase flag."""
    age = getattr(env["player"], "age", None)
    if age is not None and entry.get("year") != age:
        entry["year"] = age
        entry["trained_this_year"] = {}
        entry["count_this_year"] = {}


def add_gain(env, person_id, skill_id, gain, option_id, year):
    entry = person_state(env, person_id, create=True)
    vals = values(env, person_id)
    vals[skill_id] = max(0, min(100, vals[skill_id] + gain))
    entry["history"].append({"year": year, "option_id": option_id,
                             "skill": skill_id, "gain": gain})
    del entry["history"][:-HISTORY_CAP]
    return vals[skill_id]


def bump_use(env, person_id, option_id):
    entry = person_state(env, person_id, create=True)
    _roll_year(env, person_id, entry)
    entry["trained_this_year"][option_id] = \
        entry["trained_this_year"].get(option_id, 0) + 1
    n = entry["count_this_year"].get(option_id, 0) + 1
    entry["count_this_year"][option_id] = n
    return n


def use_count(env, person_id, option_id):
    entry = person_state(env, person_id, create=True)
    _roll_year(env, person_id, entry)
    return entry["count_this_year"].get(option_id, 0)


def backfill(env):
    """One-time baseline for pre-M3 saves: deterministic defaults only,
    no rng, no per-load reroll (values persist after the first write)."""
    player = env["player"]
    values(env, player.person_id)
    for pid in list(env.get("dead_ids", []) or []):
        person_state(env, pid)  # deceased entries stay read-only
    return env[STATE_KEY]
