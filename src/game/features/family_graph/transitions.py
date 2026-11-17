"""Typed graph transitions for B/C (M2.5 frozen contract §7, Kimi A).

Each op validates through family_graph.graph, mutates env["graph"], and
returns ACTUAL-delta results: {ok, ids, journal, ledger_requests}. B/C call
these from inside their own command context and post the returned
journal/ledger through their pipeline; GameService.graph_command wraps the
whole thing with replay/revision/save semantics for direct UI use.
"""

from . import graph as graph_mod
from .graph import GraphError  # re-export: callers map codes to their errors


def _people(env):
    from src.game.service import GameService
    people = {p.person_id: p for p in GameService._walk_people(env["player"])}
    for person in env.get("archive", []):
        if hasattr(person, "person_id") and \
                person.person_id not in people:
            people[person.person_id] = person
    return people


def _resolve(env, person_id):
    person = _people(env).get(person_id)
    if person is None:
        raise GraphError("not_found", "unknown person %r" % person_id, 404)
    return person


def _j(kind, text, person_ids):
    return {"kind": kind, "text": text,
            "person_ids": [p for p in person_ids if p]}


def start_relationship_op(env, a_id, b_id, kind, sampled, turn,
                          visibility="public", exclusivity="exclusive"):
    a, b = _resolve(env, a_id), _resolve(env, b_id)
    rel_id = graph_mod.start_relationship(
        env["graph"], a, b, kind, sampled, turn, visibility=visibility,
        exclusivity=exclusivity)
    return {"ok": True, "ids": {"relationship_id": rel_id},
            "journal": [_j("relationship",
                           f"{a.name} and {b.name} are now {kind}.",
                           [a_id, b_id])],
            "ledger_requests": []}


def update_relationship_op(env, relationship_id, patch, turn):
    edge = graph_mod.update_relationship(env["graph"], relationship_id,
                                         patch, turn)
    journal = []
    if patch.get("end"):
        people = _people(env)
        names = [people.get(pid).name if people.get(pid) else pid
                 for pid in edge["person_ids"]]
        journal.append(_j("relationship",
                          "%s and %s are no longer together." %
                          tuple(names[:2]), edge["person_ids"]))
    return {"ok": True, "ids": {"relationship_id": relationship_id},
            "journal": journal, "ledger_requests": []}


def form_union_op(env, a_id, b_id, kind, recognition, turn, prenup=None,
                  ceremony=None, policy_revision=0):
    a, b = _resolve(env, a_id), _resolve(env, b_id)
    union_id = graph_mod.form_union(env["graph"], a, b, kind, recognition,
                                    turn, prenup=prenup, ceremony=ceremony,
                                    policy_revision=policy_revision)
    return {"ok": True, "ids": {"union_id": union_id},
            "journal": [_j("union", f"{a.name} and {b.name} formed a "
                                    f"{kind}.", [a_id, b_id])],
            "ledger_requests": []}


def end_union_op(env, union_id, how, turn):
    edge = graph_mod.end_union(env["graph"], union_id, how, turn)
    people = _people(env)
    names = [people.get(pid).name if people.get(pid) else pid
             for pid in edge["person_ids"]]
    label = {"divorce": "divorced", "dissolve": "separated",
             "widow": "widowed"}[how]
    return {"ok": True, "ids": {"union_id": union_id},
            "journal": [_j("union", "%s and %s are %s." %
                           (names[0], names[1], label), edge["person_ids"])],
            "ledger_requests": []}


def add_parentage_op(env, child_id, parent_id, role, source,
                     confidence="certain", turn=None):
    child, parent = _resolve(env, child_id), _resolve(env, parent_id)
    graph_mod.add_parentage(env["graph"], child, parent, role, source,
                            confidence=confidence, turn=turn)
    return {"ok": True,
            "ids": {},
            "journal": [_j("parentage",
                           f"{parent.name} is recorded as {child.name}'s "
                           f"{role} parent.", [child_id, parent_id])],
            "ledger_requests": []}


def record_offer_op(env, initiator_id, target_id, interaction_kind, turn,
                    expiry_turn, policy_revision, romantic=True,
                    relationship_id=None, union_id=None, bindings=None,
                    sampled_outcomes=None):
    initiator, target = _resolve(env, initiator_id), _resolve(env, target_id)
    offer_id = graph_mod.record_offer(
        env["graph"], initiator, target, interaction_kind, turn, expiry_turn,
        policy_revision, romantic=romantic, relationship_id=relationship_id,
        union_id=union_id, bindings=bindings,
        sampled_outcomes=sampled_outcomes)
    return {"ok": True, "ids": {"offer_id": offer_id}, "journal": [],
            "ledger_requests": []}


def resolve_offer_op(env, offer_id, accept, turn, option_id=None):
    offer = graph_mod._find_offer(env["graph"], offer_id)
    people = _people(env)
    result = graph_mod.resolve_offer(env["graph"], offer_id, people, accept,
                                     turn, option_id=option_id)
    text = "%s %s %s's offer." % (
        people.get(result["target_id"]).name
        if people.get(result["target_id"]) else result["target_id"],
        "accepted" if accept else "declined",
        people.get(result["initiator_id"]).name
        if people.get(result["initiator_id"]) else result["initiator_id"])
    return {"ok": True, "ids": {"offer_id": offer_id},
            "journal": [_j("relationship", text,
                           [result["initiator_id"],
                            result["target_id"]])],
            "ledger_requests": []}


OPS = {
    "start_relationship": start_relationship_op,
    "update_relationship": update_relationship_op,
    "form_union": form_union_op,
    "end_union": end_union_op,
    "add_parentage": add_parentage_op,
    "record_offer": record_offer_op,
    "resolve_offer": resolve_offer_op,
}
