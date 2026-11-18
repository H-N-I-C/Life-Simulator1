"""§Person detail projection (redesign-v2, contracts.md §Person).

Observer-safe additive view of one person for the v2 Person card. Pure
read: never mutates env, never re-derives eligibility, never reads the
editor's privileged policy catalog. Safe data only — no routing, HTML or
rule/balance changes (codex-review.md §4, owner A).
"""

from src.people.classes.relationship import Relationship

__all__ = ["person_detail"]

# Reserved observer-public descriptor keys (contracts.md §Person). v1 has no
# approved public view for any of them, so none are ever emitted.
DESCRIPTOR_KEYS = ("job", "faith", "city", "country")


def _appearance_dict(person):
    appearance = getattr(person, "appearance", None)
    if appearance is None:
        return None
    return appearance.to_dict() if hasattr(appearance, "to_dict") \
        else None


def _bars(person):
    """Public observer-safe stats. v1: the relationship value, and only for
    Relation objects — the same value people[] already exposes."""
    if not isinstance(person, Relationship):
        return []
    return [{"key": "relationship", "label": "Relationship",
             "value": person.relationship}]


def _actions_for(person_id, advertised_specs, alive):
    """Target-filtered advertised specs, passed through in advertised shape
    (frozen §Person): the client normalizes via the frozen adapter
    normalizeAction — no second dialect. The advertised list is the exact
    list _state serves (pending/death gating already applied), so keeping
    target_id matches preserves spec/dispatch parity by construction.
    Deceased targets accept nothing."""
    if not alive:
        return []
    out = []
    for spec in advertised_specs:
        if spec.get("target_id") != person_id:
            continue
        item = dict(spec)
        item.setdefault("inputs", [])
        item.setdefault("known_costs", [])
        item.setdefault("limits", [])
        item.setdefault("enabled", True)
        item.setdefault("reason", None)
        out.append(item)
    return out


def person_detail(env, person, advertised_specs, role_of):
    """Build the §Person detail dict for one resolved Person object.
    `advertised_specs` is GameService._advertised_actions(env, player);
    `role_of` is GameService._role. Unknown ids are rejected by the caller
    (typed not_found) before this runs."""
    person_id = person.person_id
    alive = person_id not in env.get("dead_ids", [])
    return {
        "id": person_id,
        "name": person.name,
        "age": person.age,
        "role": role_of(person),
        "alive": alive,
        "appearance": _appearance_dict(person),
        "descriptors": {},
        "bars": _bars(person),
        "actions": _actions_for(person_id, advertised_specs, alive),
    }
