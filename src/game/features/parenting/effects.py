"""v3 domain-effect appliers for A-owned domains (graph transitions +
parenting), registered into C's resolve registry (frozen
event-schema-v3.md §4: applier(context, player, bindings, value, rng) ->
(events, money_entries)). B's policy appliers register in B's domain; C's
knowledge appliers with C's layer.

Importing this module performs the registrations (service.py imports it
at load).
"""

from src.game import events as event_mod
from src.menus import rule_actions

from src.game.features.family_graph import transitions
from src.game.features.parenting import core


# ---------------------------------------------------------------- helpers

def _env(context):
    return context._env


def _pid(bindings, role):
    pid = bindings.get(role)
    if pid is None:
        raise rule_actions.RuleError(
            "invalid_input", "role %r is not bound" % role)
    return pid


def _int_value(value, rng, name):
    """DeltaValue: int or {"randint": [lo, hi]} (drawn once here)."""
    if isinstance(value, int) and not isinstance(value, bool):
        return value
    if isinstance(value, dict) and isinstance(value.get("randint"), list):
        lo, hi = value["randint"]
        return rng.randint(int(lo), int(hi))
    raise rule_actions.RuleError(
        "invalid_input", "%s must be an int or {randint: [...]}" % name)


def _turn(env):
    return env.get("revision", 0)


# ------------------------------------------------------------------ appliers

def _milestone(context, player, bindings, value, rng):
    env = _env(context)
    child_id = _pid(bindings, value.get("child"))
    core.record_milestone(env, child_id, str(value.get("id")),
                          _turn(env), record=value.get("record"))
    return [], []


def _sample_conception(context, player, bindings, value, rng,
                       occurrence_id=None):
    from src.game.features.family_graph import sample_conception
    env = _env(context)
    mother = _pid(bindings, value.get("mother"))
    father = _pid(bindings, value.get("father"))
    # One persisted outcome per conception, identified by the stored
    # occurrence id (a replayed occurrence reuses its outcome; a new
    # occurrence for the same pair is a new conception). The stored key
    # is checked BEFORE any draw, so a replayed occurrence never
    # re-consumes the gameplay RNG.
    key = "occurrence:%s" % occurrence_id if occurrence_id \
        else "%s+%s" % (mother, father)
    outcomes = env["graph"].setdefault("conception_outcomes", {})
    if key in outcomes:
        return [], []
    rule_actions._guard_romance(context.people_by_id.get(mother),
                                context.people_by_id.get(father))
    outcome = sample_conception(env["graph"], mother, father, rng,
                                persons_by_id=context.people_by_id)
    outcome["bio_mother_id"] = mother
    outcome["bio_father_id"] = father
    outcomes[key] = outcome
    return [], []


def _find_active_relationship(graph, a_id, b_id):
    for rel in graph["relationships"]:
        if rel["status"] == "active" \
                and set(rel["person_ids"]) == {a_id, b_id}:
            return rel
    return None


def _end_relationship(context, player, bindings, value, rng):
    env = _env(context)
    a_id = _pid(bindings, value["between"][0])
    b_id = _pid(bindings, value["between"][1])
    rel = _find_active_relationship(env["graph"], a_id, b_id)
    if rel is None:
        raise rule_actions.RuleError(
            "not_found", "no active relationship between the bound pair")
    result = transitions.update_relationship_op(
        env, rel["relationship_id"], {"end": True}, _turn(env))
    return result["journal"], result["ledger_requests"]


def _end_union(context, player, bindings, value, rng):
    env = _env(context)
    union_id = bindings.get(value.get("union_role"))
    if union_id is None:
        raise rule_actions.RuleError(
            "invalid_input", "union role is not bound")
    how = value.get("how", "divorce")
    result = transitions.end_union_op(env, union_id, how, _turn(env))
    return result["journal"], result["ledger_requests"]


def _chemistry(context, player, bindings, value, rng):
    env = _env(context)
    stored = (env.get("pending") or {}).get("sampled") or {}
    if stored.get("accepted") is False:
        # a stored refusal cancels the date's spark: no edge, no delta
        return [], []
    a_id = _pid(bindings, value["between"][0])
    b_id = _pid(bindings, value["between"][1])
    rel = _find_active_relationship(env["graph"], a_id, b_id)
    if rel is None:
        # A chemistry delta is not consent. Only an accepted offer may
        # create the edge; established relationships may change normally.
        return [], []
    delta = _int_value(value.get("delta", 0), rng, "delta")
    patch = {"chemistry": max(0, min(100, rel["chemistry"] + delta))}
    transitions.update_relationship_op(env, rel["relationship_id"], patch,
                                       _turn(env))
    return [], []


rule_actions.register_domain_effect("milestone", _milestone)
rule_actions.register_domain_effect("sample_conception", _sample_conception)
rule_actions.register_domain_effect("end_relationship", _end_relationship)
rule_actions.register_domain_effect("end_union", _end_union)
rule_actions.register_domain_effect("chemistry", _chemistry)


# ------------------------------------------------- child role predicates
# C's registry (register_predicate); these deliberately bind MINORS for
# child roles — par_* events are interaction.romantic: false, so the
# fixed 18+ guard never applies to them. Children stay non-romantic
# dependents.

def _context_people(context):
    people = {p.person_id: p for p in
              context._service._walk_people(context._env["player"])}
    for person in context._env.get("archive", []):
        if hasattr(person, "person_id") \
                and person.person_id not in people:
            people[person.person_id] = person
    return people


def _child_of_actor(min_age=0, max_age=17):
    def fn(actor_id, context):
        env = context._env
        people = _context_people(context)
        core.backfill_all(env, context._service)
        for child_id in core.person_children(env, context._service,
                                             actor_id):
            person = people.get(child_id)
            if person is None:
                continue
            if not getattr(person, "alive", True):
                continue
            if min_age <= getattr(person, "age", 0) <= max_age:
                return child_id
        return None
    return fn


event_mod.register_predicate("dependent_child_of_actor",
                             _child_of_actor(0, 17))
event_mod.register_predicate("toddler_child_of_actor",
                             _child_of_actor(1, 3))
event_mod.register_predicate("school_child_of_actor",
                             _child_of_actor(5, 12))
event_mod.register_predicate("teen_child_of_actor",
                             _child_of_actor(13, 17))
event_mod.register_predicate("adult_child_of_actor",
                             _child_of_actor(18, 120))


def _grandchild_of_actor(actor_id, context):
    """A person whose parent is one of the actor's children (graph
    parentage, any role). Grandchildren are family, never romantic
    targets: par_* events bind them under interaction.romantic false."""
    env = context._env
    people = _context_people(context)
    from src.game.features.family_graph import dependent_children
    children = set(dependent_children(env["graph"], actor_id))
    for pid, person in sorted(people.items()):
        if pid in children or pid == actor_id:
            continue
        if getattr(person, "age", 0) > 17:
            continue
        parents = {e["parent_id"] for e in env["graph"]["parentage"]
                   if e["child_id"] == pid}
        if parents & children:
            return pid
    return None


event_mod.register_predicate("grandchild_of_actor", _grandchild_of_actor)
