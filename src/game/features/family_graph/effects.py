"""v3 domain-effect appliers for A's graph offer lifecycle (frozen
event-schema-v3.md §4): `offer` records a graph offer from the occurrence
and resolves it from the STORED sampled response (never caller-supplied
consent); `respond` resolves a previously recorded offer bound by role.
Accepted courtship/flirt/date offers create the stranger -> partner
relationship edge; accepted union proposals form the union; a refusal is
a legal stored outcome (attempts preserved, nothing invented).

Importing this module performs the registrations (service.py imports it
at load), like the parenting/knowledge applier modules.
"""

from src.menus import rule_actions
from src.game.features.family_graph import transitions
from src.game.features.family_graph.graph import (
    GraphError, INTERACTION_KINDS, ROMANTIC_KINDS)

_COURTSHIP_KINDS = ("date", "flirt", "propose_courtship", "repair_attempt")
_UNION_KINDS = ("propose_union", "propose_plural_union")
_OFFER_EXPIRY_TURNS = 4


def _env(context):
    return context._env


def _turn(env):
    return env.get("revision", 0)


def _policy_revision(env):
    return int((env.get("policy") or {}).get("policy_revision", 0))


def _default_recognition(env):
    policy = env.get("policy") or {}
    return {"status": "recognized",
            "jurisdiction_id": policy.get("jurisdiction_id",
                                          "jur-neutral-default"),
            "law_version": _policy_revision(env),
            "basis": "ceremony"}


def _active_edge(env, a_id, b_id):
    pair = {a_id, b_id}
    for rel in env["graph"]["relationships"]:
        if rel["status"] == "active" and set(rel["person_ids"]) == pair:
            return rel
    return None


def _active_union(env, a_id, b_id):
    pair = {a_id, b_id}
    for union in env["graph"]["unions"]:
        if union["status"] == "active" and set(union["person_ids"]) == pair:
            return union
    return None


def _apply_accepted(env, offer, journal):
    """An accepted offer becomes graph state: a courtship-kind offer
    creates the relationship edge (stranger -> partner); a union proposal
    forms the union. Idempotent per edge: a replay never duplicates."""
    a_id, b_id = offer["initiator_id"], offer["target_id"]
    kind = offer["interaction_kind"]
    turn = _turn(env)
    if kind in _COURTSHIP_KINDS and _active_edge(env, a_id, b_id) is None:
        journal.extend(transitions.start_relationship_op(
            env, a_id, b_id, "courtship",
            {"satisfaction": 55, "trust": 45, "chemistry": 55},
            turn)["journal"])
    if kind in _UNION_KINDS and _active_union(env, a_id, b_id) is None:
        recognition = offer.get("sampled_outcomes", {}).get(
            "projected_recognition") or _default_recognition(env)
        journal.extend(transitions.form_union_op(
            env, a_id, b_id, "marriage", recognition, turn,
            policy_revision=_policy_revision(env))["journal"])
    return journal


def _offer(context, player, bindings, value, rng, occurrence_id=None):
    env = _env(context)
    interaction = value.get("interaction")
    if interaction not in INTERACTION_KINDS:
        raise rule_actions.RuleError(
            "invalid_input", "unknown interaction kind %r" % interaction)
    role = value.get("to") or "target"
    other_id = bindings.get(role)
    if not other_id:
        raise rule_actions.RuleError(
            "invalid_input", "role %r is not bound" % role)
    if value.get("as") == "npc":
        initiator_id, target_id = other_id, player.person_id
    else:
        initiator_id, target_id = player.person_id, other_id
    # Consent comes from the response stored at offer creation (C's
    # single seeded draw), never from the resolver path. An occurrence
    # without a stored response records a pending offer instead (the
    # target's answer arrives later via `respond`).
    pending = env.get("pending") or {}
    sampled = dict(pending.get("sampled") or {})
    turn = _turn(env)
    recorded = transitions.record_offer_op(
        env, initiator_id, target_id, interaction, turn,
        turn + _OFFER_EXPIRY_TURNS, _policy_revision(env),
        romantic=interaction in ROMANTIC_KINDS,
        bindings=dict(bindings), sampled_outcomes=sampled)
    offer_id = recorded["ids"]["offer_id"]
    journal = []
    if "accepted" in sampled:
        accepted = bool(sampled["accepted"])
        resolved = transitions.resolve_offer_op(env, offer_id, accepted,
                                                turn)
        journal.extend(resolved["journal"])
        if accepted:
            offer = next(o for o in env["graph"]["offers"]
                         if o["offer_id"] == offer_id)
            _apply_accepted(env, offer, journal)
    return journal, []


def _respond(context, player, bindings, value, rng, occurrence_id=None):
    env = _env(context)
    role = value.get("offer_role") or "offer"
    offer_id = bindings.get(role)
    if not offer_id:
        raise rule_actions.RuleError(
            "invalid_input", "role %r is not bound" % role)
    if not isinstance(value.get("accept"), bool):
        raise rule_actions.RuleError(
            "invalid_input", "respond.accept must be a boolean")
    turn = _turn(env)
    resolved = transitions.resolve_offer_op(env, offer_id, value["accept"],
                                            turn)
    journal = list(resolved["journal"])
    if value["accept"]:
        offer = next(o for o in env["graph"]["offers"]
                     if o["offer_id"] == offer_id)
        _apply_accepted(env, offer, journal)
    return journal, []


rule_actions.register_domain_effect("offer", _offer)
rule_actions.register_domain_effect("respond", _respond)
