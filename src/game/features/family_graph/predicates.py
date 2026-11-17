"""Authoritative role-selection predicates for C's v3 registry
(events.PREDICATES), backed by A's read-only graph queries. Importing
this module REPLACES C's duck-typed defaults with graph-backed versions
(A registers shared engine hooks per the spec). All predicates are
deterministic (sorted iteration) and enforce the fixed rule where the
role semantics demand adults.
"""

from src.game import events as event_mod
from src.game.features.family_graph import graph as graph_mod


def _ctx(context):
    env = getattr(context, "_env", None)
    service = getattr(context, "_service", None)
    if env is None or service is None:
        raise event_mod.EventValidationError(
            "predicate context lacks the envelope/service")
    return env, service


def _people(context):
    env, service = _ctx(context)
    people = {p.person_id: p for p in service._walk_people(env["player"])}
    for person in env.get("archive", []):
        if hasattr(person, "person_id") \
                and person.person_id not in people:
            people[person.person_id] = people.get(person.person_id, person)
    for pid in env.get("dead_ids", []):
        if pid in people:
            people[pid].alive = False
    return env, people


def _adult(person):
    return getattr(person, "age", 0) >= 18 \
        and getattr(person, "alive", True)


def _active_relationship_between(graph, a_id, b_id):
    for rel in graph["relationships"]:
        if rel["status"] == "active" \
                and set(rel["person_ids"]) == {a_id, b_id}:
            return rel
    return None


def _adult_sorted(people):
    return sorted((pid for pid, p in people.items() if _adult(p)))


def _non_related(graph, a_id, b_id):
    return graph_mod.graph_relatedness(graph, a_id, b_id)["category"] \
        in ("unrelated", "unknown")


def _non_stranger(graph, a_id, b_id):
    return _active_relationship_between(graph, a_id, b_id) is not None \
        or any(b_id in u["person_ids"] and u["status"] == "active"
                for u in graph["unions"])


def adult_non_related_stranger(actor_id, context):
    """Adult, living, biologically/socially unrelated, no existing
    relationship or union with the actor — a genuine stranger."""
    env, people = _people(context)
    graph = env["graph"]
    for pid in _adult_sorted(people):
        if pid == actor_id:
            continue
        if not _non_related(graph, actor_id, pid):
            continue
        if _non_stranger(graph, actor_id, pid):
            continue
        return pid
    return None


def adult_acquaintance(actor_id, context):
    """Any adult, living, not already in a relationship/union with the
    actor (relatives allowed — the event's conditions own the rest)."""
    env, people = _people(context)
    graph = env["graph"]
    for pid in _adult_sorted(people):
        if pid == actor_id:
            continue
        if _non_stranger(graph, actor_id, pid):
            continue
        return pid
    return None


def adult_existing_lover(actor_id, context):
    env, people = _people(context)
    graph = env["graph"]
    for rel in sorted(graph["relationships"],
                      key=lambda r: r["relationship_id"]):
        if rel["status"] != "active":
            continue
        if actor_id not in rel["person_ids"]:
            continue
        other = rel["person_ids"][0] if rel["person_ids"][1] == actor_id \
            else rel["person_ids"][1]
        if other in people and _adult(people[other]):
            return other
    return None


def adult_secret_lover_of_actor(actor_id, context):
    env, people = _people(context)
    graph = env["graph"]
    for rel in sorted(graph["relationships"],
                      key=lambda r: r["relationship_id"]):
        if rel["status"] != "active" or rel["visibility"] != "secret":
            continue
        if actor_id not in rel["person_ids"]:
            continue
        other = rel["person_ids"][0] if rel["person_ids"][1] == actor_id \
            else rel["person_ids"][1]
        if other in people and _adult(people[other]):
            return other
    return None


def adult_union_partner_of_actor(actor_id, context):
    """The SELECTED-edge-safe partner query: returns the first active
    union partner. C's offer binds the returned id ONCE; it never
    re-picks at resolve."""
    env, people = _people(context)
    graph = env["graph"]
    for union in sorted(graph["unions"], key=lambda u: u["union_id"]):
        if union["status"] != "active" or actor_id not in union["person_ids"]:
            continue
        other = union["person_ids"][0] if union["person_ids"][1] == actor_id \
            else union["person_ids"][1]
        if other in people and _adult(people[other]):
            return other
    return None


# v2 'engaged' maps onto an active union in the M2.5 graph (there is no
# separate engagement edge); engagements are unions in waiting.
adult_engaged_to_actor = adult_union_partner_of_actor


def adult_same_household(actor_id, context):
    env, people = _people(context)
    graph = env["graph"]
    for household in sorted(graph["households"],
                            key=lambda h: h.get("household_id", "")):
        if actor_id not in household.get("resident_ids", []):
            continue
        for pid in sorted(household.get("resident_ids", [])):
            if pid != actor_id and pid in people and _adult(people[pid]):
                return pid
    return None


def adult_partner_or_lover_of_actor(actor_id, context):
    """Active union partner if one exists, otherwise an active
    relationship partner — the generic 'my partner' role."""
    pid = adult_union_partner_of_actor(actor_id, context)
    if pid is not None:
        return pid
    return adult_existing_lover(actor_id, context)


def adult_ex_lover_of_actor(actor_id, context):
    """An ENDED relationship partner (the ex who resurfaces)."""
    env, people = _people(context)
    graph = env["graph"]
    for rel in sorted(graph["relationships"],
                      key=lambda r: r["relationship_id"]):
        if rel["status"] != "ended" or actor_id not in rel["person_ids"]:
            continue
        other = rel["person_ids"][0] if rel["person_ids"][1] == actor_id \
            else rel["person_ids"][1]
        if other in people and _adult(people[other]):
            return other
    return None


# Registering replaces C's duck-typed defaults (registry semantics:
# re-registering replaces).
event_mod.register_predicate("adult_non_related_stranger",
                             adult_non_related_stranger)
event_mod.register_predicate("adult_acquaintance", adult_acquaintance)
event_mod.register_predicate("adult_existing_lover", adult_existing_lover)
event_mod.register_predicate("adult_secret_lover_of_actor",
                             adult_secret_lover_of_actor)
event_mod.register_predicate("adult_union_partner_of_actor",
                             adult_union_partner_of_actor)
event_mod.register_predicate("adult_engaged_to_actor",
                             adult_engaged_to_actor)
event_mod.register_predicate("adult_same_household", adult_same_household)
event_mod.register_predicate("adult_partner_or_lover_of_actor",
                             adult_partner_or_lover_of_actor)
event_mod.register_predicate("adult_ex_lover_of_actor",
                             adult_ex_lover_of_actor)
