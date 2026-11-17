"""v3 domain-effect appliers for the knowledge/reputation domain
(frozen event-schema-v3.md §4): `reveal_fact` and `reputation`.
Registered into C's resolve registry when this module is imported
(service.py imports it at load).

applier(context, player, bindings, value, rng) -> (events, money_entries)
"""

import json

from src.menus import rule_actions

from src.game.features.knowledge import core as knowledge
from src.game.features.knowledge import drama


def _bindings_pids(bindings, roles):
    out = []
    for role in roles:
        pid = bindings.get(role)
        if pid is None:
            raise rule_actions.RuleError(
                "invalid_input", "role %r is not bound" % role)
        out.append(pid)
    return out


def _reveal_fact(context, player, bindings, value, rng, occurrence_id=None):
    env = context._env
    kind = value.get("kind")
    if kind not in ("affair", "secret_union", "conversion", "breakup"):
        raise rule_actions.RuleError(
            "invalid_input", "unknown reveal kind %r" % kind)
    about = _bindings_pids(bindings, value.get("about") or [])
    to = _bindings_pids(bindings, (value.get("to") or ["actor"]))
    turn = env.get("revision", 0)
    occurrence_id = occurrence_id or "reveal:%s:%s" % (
        context.turn_id, json.dumps([kind, sorted(about), sorted(to)]))
    if kind == "affair":
        # An `affair` label must be backed by an ACTUAL agreement breach
        # (frozen §4): consensual open/plural arrangements are never
        # affairs, and content cannot mint a cheating fact without one.
        # The fact is established through detect_affair — the same
        # agreement-aware production seam the drama engine uses — bound
        # to THIS occurrence; reveal_fact below then reuses it (one
        # immutable fact per occurrence, never two mints).
        if len(about) != 2 \
                or not drama.is_affair_breach(env, about[0], about[1]):
            raise rule_actions.RuleError(
                "not_allowed",
                "this occurrence breaches no exclusive agreement")
        drama.detect_affair(env, about[0], about[1], turn,
                            occurrence_id=occurrence_id)
    reputation = []
    for spec in value.get("reputation") or []:
        pid = bindings.get(spec.get("person"))
        if pid is None:
            raise rule_actions.RuleError(
                "invalid_input", "reputation person role not bound")
        reputation.append({"person_id": pid,
                           "scope": spec.get("scope", "global"),
                           "delta": spec.get("delta", 0)})
    fact_id = drama.reveal_fact(
        env, context._service, kind, about, to, turn, occurrence_id,
        reputation=reputation)
    return [{"kind": "event", "text": "Something hidden came to light.",
             "person_ids": about,
             "audience": "public", "fact_id": fact_id}], []


def _reputation(context, player, bindings, value, rng, occurrence_id=None):
    env = context._env
    person = bindings.get(value.get("person"))
    if person is None:
        raise rule_actions.RuleError(
            "invalid_input", "person role is not bound")
    scope = value.get("scope", "global")
    key = "v3rep:%s:%s:%s" % (occurrence_id or context.turn_id, person, scope)
    knowledge.normalize_knowledge(env)
    if key in env["knowledge"]["reputation_posted"]:
        return [], []
    delta = value.get("delta", 0)
    if isinstance(delta, dict):
        lo, hi = delta.get("randint", [0, 0])
        delta = rng.randint(int(lo), int(hi))
    fact_id = knowledge.mint_fact(env, "discovery", [person],
                                  env.get("revision", 0), occurrence_id=key)
    knowledge.get_fact(env, fact_id)["reputation_changes"] = [
        {"person_id": person, "scope": scope, "delta": int(delta)}]
    knowledge.learn(env, player.person_id, fact_id, env.get("revision", 0),
                    "witnessed")
    knowledge.reputation_delta(env, person, scope, int(delta),
                               occurrence_key=key)
    return [], []


rule_actions.register_domain_effect("reveal_fact", _reveal_fact)
rule_actions.register_domain_effect("reputation", _reputation)
