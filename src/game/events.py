"""Event data-file engine (loader for the M2c content pipeline; C-owned
per 00-TEAM.md 2026-11-16, schema v2.1).

Loads event definitions from src/game/events/*.json (bundled fit-proof)
and src/game/content/events/*.json (C content) per the frozen
event-schema.json (v2.1: auto events, sign, branches field_refs,
sampled_at_offer, chance_n), validates the structural invariants
(including the soft-lock rule for non-auto events), evaluates conditions
against the transaction-local context, and instantiates pending decisions
with the same shape the service resolver consumes. All randomness is
sampled once at offer creation; resolve never rerolls.
"""

import json
import os
import random

EVENTS_DIR = os.path.join(os.path.dirname(__file__), "events")
CONTENT_DIR = os.path.join(os.path.dirname(__file__), "content", "events")

# Declared legacy order for independent annual checks (finding-round #5):
# the order the gates appear in Player.age_up/random_events. chance_n
# events are evaluated in THIS order, separately from the flavor pool.
LEGACY_CHANCE_ORDER = [
    "lightning_strike",
    "partner_breakup_offer",
    "partner_divorce_offer",
    "partner_kidnapping",
    "dating_offer",
    "thanatophobia_worry",
    "vaccination_age2",
]

STAT_KEYS = ("happiness", "health", "smarts", "looks", "karma", "stress")
CONDITION_KINDS = {
    "alive", "has_illness", "employed", "in_school", "min_stat",
    "min_money", "flag", "no_partner", "partner_status",
    "min_age_sampled", "max_age_sampled", "partner_relationship_below",
    "karma_gate",
}


class EventValidationError(Exception):
    """An event file violates the schema invariants."""


def validate_event(ev):
    """Structural invariants per event-schema.json v2.1."""
    for key in ("id", "title", "text"):
        if key not in ev:
            raise EventValidationError(f"event missing {key!r}")
    if not isinstance(ev["id"], str) or not ev["id"]:
        raise EventValidationError("event id must be a non-empty string")
    if not isinstance(ev.get("weight", 1), int) or ev.get("weight", 1) < 1:
        raise EventValidationError(f"{ev['id']}: weight must be int >= 1")
    chance_n = ev.get("chance_n")
    if chance_n is not None             and (not isinstance(chance_n, int) or chance_n < 1):
        raise EventValidationError(f"{ev['id']}: chance_n must be int >= 1")
    if ev.get("auto"):
        # v2.1 automatic event: no choices; effects (possibly outcomes)
        # apply immediately at offer time and are journaled.
        if "effects" not in ev and "outcomes" not in ev:
            raise EventValidationError(
                f"{ev['id']}: auto event needs effects or outcomes")
        if ev.get("choices"):
            raise EventValidationError(
                f"{ev['id']}: auto event must not carry choices")
    else:
        choices = ev.get("choices")
        if not isinstance(choices, list) or len(choices) < 2:
            raise EventValidationError(
                f"{ev['id']}: at least 2 choices required")
        free = 0
        for choice in choices:
            for key in ("id", "label"):
                if key not in choice:
                    raise EventValidationError(
                        f"{ev['id']}:{choice.get('id')}: missing {key!r}")
            if "branches" not in choice and "outcomes" not in choice:
                for key in ("effects", "outcome_text"):
                    if key not in choice:
                        raise EventValidationError(
                            f"{ev['id']}:{choice.get('id')}: missing "
                            f"{key!r}")
            cost = choice.get("cost", {})
            if cost.get("money", 0) == 0 and "money_sampled" not in cost:
                free += 1
        if free < 1:
            # SOFT-LOCK RULE: every decision keeps an always-available option
            raise EventValidationError(
                f"{ev['id']}: no free option (soft-lock rule)")
    cond = ev.get("conditions", {})
    for clause in cond.get("requires", []):
        if clause.get("kind") not in CONDITION_KINDS:
            raise EventValidationError(
                f"{ev['id']}: unknown condition kind "
                f"{clause.get('kind')!r}")

    def _check_weights(outcomes, where):
        # _pick_outcome does rng.randrange(sum(weights)): any weight < 1
        # (or a non-int) can zero the total and crash the sampler.
        for o in outcomes:
            w = o.get("weight", 1)
            if not isinstance(w, int) or isinstance(w, bool) or w < 1:
                raise EventValidationError(
                    f"{ev['id']}:{where}: outcome weight must be int >= 1")

    for where, outcomes in (("auto", ev.get("outcomes") or []),):
        _check_weights(outcomes, where)
    if not ev.get("auto"):
        for c in ev.get("choices") or []:
            _check_weights(c.get("outcomes") or [], c.get("id", "?"))
            br = c.get("branches")
            if br:
                for side in ("then", "else"):
                    _check_weights(br.get(side, {}).get("outcomes") or [],
                                   f"{c.get('id', '?')}.{side}")


def load_event_files(directory=None):
    """Load and validate every event file. Returns [EventDef].

    Default loads BOTH the bundled fit-proof dir (src/game/events) and the
    content dir (src/game/content/events). Pass an explicit directory to
    load only that one (tests). v2 (schema_version 2, default) and v3
    (schema_version 3, M2.5) files coexist; each is validated against its
    own schema and keeps its dialect marker.
    """
    if directory is not None:
        directories = [directory]
    else:
        directories = [EVENTS_DIR, CONTENT_DIR]
    events = []
    for directory in directories:
        if not os.path.isdir(directory):
            continue
        for name in sorted(os.listdir(directory)):
            if not name.endswith(".json"):
                continue
            with open(os.path.join(directory, name),
                      encoding="utf-8") as f:
                doc = json.load(f)
            if not isinstance(doc, dict) \
                    or not isinstance(doc.get("events"), list):
                raise EventValidationError(f"{name}: bad event file shape")
            if not isinstance(doc.get("schema_version", 2), int):
                raise EventValidationError(
                    f"{name}: schema_version must be an int")
            for ev in doc["events"]:
                if doc.get("schema_version", 2) == 3:
                    validate_event3(ev)
                    ev = dict(ev)
                    ev["schema_version"] = 3
                else:
                    validate_event(ev)
                events.append(ev)
    ids = [ev["id"] for ev in events]
    if len(ids) != len(set(ids)):
        raise EventValidationError("duplicate event ids")
    return events


# ==================================================================== v3
# Event schema v3 (M2.5, frozen contracts/event-schema-v3.md): explicit
# role bindings, bounded all/any/not conditions, interaction kinds with
# the immutable romantic adult guard, typed domain effects, audience
# projection. C owns predicates/bindings/typed effects/audience.

V3_INTERACTION_KINDS = {
    "flirt", "date", "propose_courtship", "propose_union",
    "propose_plural_union", "convert_request", "repair_attempt",
    "parenting_request",  # nonromantic
}
V3_ROLE_KINDS = {"person", "relationship", "union", "household"}
V3_DOMAIN_EFFECTS = {
    "offer", "respond", "end_relationship", "end_union", "reveal_fact",
    "reputation", "chemistry", "convert_profile", "set_personal_tenet",
    "reproject_recognition", "milestone", "sample_conception",
}
V3_CONDITION_KINDS = CONDITION_KINDS | {
    "relationship_state", "union_state", "expectation",
    "recognition_status", "tenet_stance", "faith_id", "jurisdiction_id",
    "attraction_band", "openness_band", "monogamy_band", "known_fact",
    "reputation_at_least", "kinship", "milestone", "bound",
}
V3_AUDIENCE_SCOPES = {"public", "player_known", "private"}
V3_SELECTS = {"player", "predicate"}

# Role selection predicate registry (C-owned allowlist; A's read-only
# graph queries and B's policy reads back these once their domains land —
# re-registering replaces the duck-typed defaults below).
PREDICATES = {}


def register_predicate(name, fn):
    """Bind a role-selection predicate (name -> fn(actor_id, context))."""
    PREDICATES[name] = fn


def _v3_person_id(context):
    pid = getattr(context, "player_id", None)
    if pid is None:
        player = getattr(context, "player", None) or getattr(context, "_player", None)
        pid = getattr(player, "person_id", None)
    return pid


def _v3_people(context):
    by_id = getattr(context, "people_by_id", None)
    if by_id is not None:
        return by_id
    people = {}
    player = getattr(context, "player", None) or getattr(context, "_player", None)
    if player is not None:
        stack, seen = [player], set()
        while stack:
            person = stack.pop()
            if id(person) in seen or person is None:
                continue
            seen.add(id(person))
            if hasattr(person, "person_id"):
                people[person.person_id] = person
            stack.extend(getattr(person, "relations", None) or [])
            for attr in ("mother", "father", "partner"):
                stack.append(getattr(person, attr, None))
    return people


def guard_event_adults(event, context, bindings=None):
    from src.game.features.family_graph.graph import assert_interaction_adults
    interaction = event.get("interaction")
    if event.get("id") == "dating_offer":
        interaction = {"kind": "date"}
    people = _v3_people(context)
    ids = [_v3_person_id(context)] + [pid for role, pid in (bindings or {}).items()
                                     if role != "witness"]
    participants = [people[pid] for pid in ids if pid in people]
    assert_interaction_adults(interaction, participants)


def _v3_predicate_default_adult(allow_partner=False, require=None):
    def fn(actor_id, context):
        people = _v3_people(context)
        for pid, person in people.items():
            if pid == actor_id:
                continue
            if getattr(person, "age", 0) < 18:
                continue
            if getattr(person, "health", 1) <= 0:
                continue
            if not allow_partner and getattr(person, "partner", None) \
                    is getattr(context, "player", None):
                continue
            if require and not require(person, context):
                continue
            return pid
        return None
    return fn


# Duck-typed default predicates for the v3 loader core; the authoritative
# versions (A graph queries / B policy bands) re-register at integration.
register_predicate("adult_non_related_stranger",
                   _v3_predicate_default_adult())
register_predicate("adult_existing_lover", _v3_predicate_default_adult())
register_predicate("adult_engaged_to_actor", _v3_predicate_default_adult())
register_predicate("adult_acquaintance", _v3_predicate_default_adult())
register_predicate("adult_same_household", _v3_predicate_default_adult())
register_predicate("adult_secret_lover_of_actor",
                   _v3_predicate_default_adult())
register_predicate("adult_union_partner_of_actor",
                   _v3_predicate_default_adult())
register_predicate("dependent_child_age_13_17",
                   lambda actor_id, context: next(
                       (pid for pid, p in _v3_people(context).items()
                        if 13 <= getattr(p, "age", 0) <= 17), None))
register_predicate("active_union_between_actor_and_spouse_a",
                   lambda actor_id, context: None)  # union role: A's query


def _v3_role_names(ev):
    roles = ev.get("roles") or {}
    if not isinstance(roles, dict):
        raise EventValidationError(f"{ev['id']}: roles must be an object")
    person_roles = {name for name, cfg in roles.items()
                    if isinstance(cfg, dict)
                    and cfg.get("kind", "person") == "person"}
    return roles, person_roles


def _v3_money_negative(money):
    if isinstance(money, int):
        return money < 0
    if isinstance(money, dict):
        return money.get("sign", 1) in (-1, "negative")
    return False


def _v3_check_role_ref(ev, person_roles, ref, where):
    if ref not in person_roles:
        raise EventValidationError(
            f"{ev['id']}:{where}: references role {ref!r} which is not a "
            f"declared person role")


def validate_event3(ev):
    """Structural invariants for schema v3 (frozen event-schema-v3.md)."""
    for key in ("id", "title", "text"):
        if key not in ev:
            raise EventValidationError(f"event missing {key!r}")
    if not isinstance(ev["id"], str) or not ev["id"]:
        raise EventValidationError("event id must be a non-empty string")
    # The fixed-rule guard runs before role checks so a romantic event
    # targeting minors fails with its stated reason.
    interaction = ev.get("interaction")
    if interaction is not None:
        if not isinstance(interaction, dict) \
                or interaction.get("kind") not in V3_INTERACTION_KINDS:
            raise EventValidationError(
                f"{ev['id']}: interaction.kind must be one of "
                f"{sorted(V3_INTERACTION_KINDS)}")
        if not isinstance(interaction.get("romantic"), bool):
            raise EventValidationError(
                f"{ev['id']}: interaction.romantic must be a bool")
        # FIXED RULE: romantic content can never make a minor eligible.
        # Only an EXPLICIT min_age below 18 fails validation; an absent
        # min_age is fine (the engine guard enforces 18+ at offer and
        # resolve regardless, outside content).
        cond0 = ev.get("conditions") or {}
        if interaction["romantic"] \
                and "min_age" in cond0 and cond0["min_age"] < 18:
            raise EventValidationError(
                f"{ev['id']}: romantic interaction with conditions.min_age "
                f"< 18 (fixed rule: content cannot make a minor eligible)")
    roles, person_roles = _v3_role_names(ev)
    for name, cfg in roles.items():
        if not isinstance(cfg, dict):
            raise EventValidationError(f"{ev['id']}: role {name!r} bad shape")
        if cfg.get("kind", "person") not in V3_ROLE_KINDS:
            raise EventValidationError(
                f"{ev['id']}: role {name!r} kind must be one of "
                f"{sorted(V3_ROLE_KINDS)}")
        select = cfg.get("select", "player" if name == "actor" else None)
        if select not in V3_SELECTS:
            raise EventValidationError(
                f"{ev['id']}: role {name!r} select must be one of "
                f"{sorted(V3_SELECTS)}")
        if select == "predicate":
            pred = cfg.get("predicate")
            if not isinstance(pred, str) or pred not in PREDICATES:
                raise EventValidationError(
                    f"{ev['id']}: role {name!r} unknown predicate {pred!r}")
    import re as _re
    for token in _re.findall(r"\{person:(\w+)\}", ev["text"]):
        _v3_check_role_ref(ev, person_roles, token, "text")

    def _check_cond(node, where):
        if not isinstance(node, dict):
            raise EventValidationError(f"{ev['id']}:{where}: bad condition")
        if "all" in node or "any" in node:
            branch = node.get("all") or node.get("any")
            if not isinstance(branch, list) or not branch:
                raise EventValidationError(
                    f"{ev['id']}:{where}: all/any needs a non-empty list")
            for i, sub in enumerate(branch):
                _check_cond(sub, f"{where}[{i}]")
        elif "not" in node:
            _check_cond(node["not"], f"{where}.not")
        else:
            kind = node.get("kind")
            if kind not in V3_CONDITION_KINDS:
                raise EventValidationError(
                    f"{ev['id']}:{where}: unknown condition kind {kind!r}")
            for ref_key in ("observer", "person", "a", "b"):
                ref = node.get(ref_key)
                if isinstance(ref, str):
                    _v3_check_role_ref(ev, person_roles, ref,
                                       f"{where}.{ref_key}")

    cond = ev.get("conditions") or {}
    for clause in cond.get("requires", []):
        _check_cond(clause, "conditions.requires")
    for key in ("all", "any", "not"):
        if key in cond:
            _check_cond({key: cond[key]} if key != "not" else
                        {"not": cond[key]}, f"conditions.{key}")

    if ev.get("auto"):
        raise EventValidationError(
            f"{ev['id']}: v3 auto events are not part of the v3 core yet")
    choices = ev.get("choices")
    if not isinstance(choices, list) or len(choices) < 2:
        raise EventValidationError(
            f"{ev['id']}: at least 2 choices required")
    free = 0
    for c in choices:
        for key in ("id", "label"):
            if key not in c:
                raise EventValidationError(
                    f"{ev['id']}:{c.get('id')}: missing {key!r}")
        if "branches" not in c and "outcomes" not in c:
            for key in ("effects", "outcome_text"):
                if key not in c:
                    raise EventValidationError(
                        f"{ev['id']}:{c.get('id')}: missing {key!r}")
        effects = c.get("effects")
        if effects is not None:
            if not isinstance(effects, dict):
                raise EventValidationError(
                    f"{ev['id']}:{c.get('id')}: effects must be an object")
            _v3_validate_effects(ev, person_roles, effects,
                                 f"choice:{c.get('id')}")
            cost = c.get("cost", {})
            if cost.get("money", 0) == 0 and "money_sampled" not in cost:
                free += 1
            if isinstance(cost.get("money"), int) and cost["money"] > 0 \
                    and isinstance(effects.get("money"), (int, dict)) \
                    and _v3_money_negative(effects["money"]):
                raise EventValidationError(
                    f"{ev['id']}:{c.get('id')}: cost gate and effects.money "
                    f"must not both charge (money posts exactly once)")
    if free < 1:
        raise EventValidationError(
            f"{ev['id']}: no free option (soft-lock rule)")

    audience = ev.get("audience")
    if audience is not None:
        if audience.get("scope") not in V3_AUDIENCE_SCOPES:
            raise EventValidationError(
                f"{ev['id']}: audience.scope must be one of "
                f"{sorted(V3_AUDIENCE_SCOPES)}")
        for p in audience.get("participants", []):
            _v3_check_role_ref(ev, person_roles, p, "audience.participants")

    if not isinstance(ev.get("weight", 1), int) or ev.get("weight", 1) < 1:
        raise EventValidationError(f"{ev['id']}: weight must be int >= 1")


def _v3_validate_effects(ev, person_roles, effects, where):
    # v2 structural keys (frozen v2.1 schema) stay legal inside v3 effects;
    # they ride the option's _structural block like v2 records.
    structural_keys = ("lose_partner", "divorce", "gain_partner",
                       "partner_death", "terminal", "add_illness",
                       "remove_illness", "learn_trait", "lose_trait",
                       "set_flag")
    allowed = (set(STAT_KEYS) | {"stats", "money", "relationship"}
               | set(structural_keys) | V3_DOMAIN_EFFECTS)
    # Which effect keys carry role references, and whether they must be
    # person roles (True) or any declared role (False).
    role_fields = {
        "end_relationship": {"between": True},
        "end_union": {"union_role": False},
        "reveal_fact": {"about": True, "to": True},
        "reputation": {"person": True},
        "chemistry": {"between": True},
        "convert_profile": {"person": True},
        "set_personal_tenet": {"person": True},
        "milestone": {"child": True},
        "sample_conception": {"mother": True, "father": True},
        "offer": {"to": True},
        "respond": {"offer_role": False},
    }
    all_roles = set((ev.get("roles") or {}))
    for key, value in effects.items():
        if key not in allowed:
            raise EventValidationError(
                f"{ev['id']}:{where}: unknown effect {key!r}")
        for field, person_only in role_fields.get(key, {}).items():
            ref = (value or {}).get(field) if isinstance(value, dict) \
                else None
            if ref is None:
                continue
            refs = ref if isinstance(ref, list) else [ref]
            for r in refs:
                if r not in all_roles or (person_only
                                          and r not in person_roles):
                    want = "person role" if person_only else "declared role"
                    raise EventValidationError(
                        f"{ev['id']}:{where}: effect {key!r}.{field} must "
                        f"reference a {want}, got {r!r}")


def _v3_render_context(context, bindings):
    """Wrapper so v2 text/effect resolution helpers resolve {person:role}
    through the v3 BINDINGS (concrete ids bound once at offer creation),
    never through ambient v2 role queries (which would re-pick)."""
    class _Bound:
        def role_person_ids(self, role):
            bound = bindings.get(role)
            return [bound] if bound is not None else []

        def random_person_id(self, parents_only=False, rng=None):
            return None

        def person_name(self, person_id):
            return context.person_name(person_id)

        def money(self):
            return context.money()

        def field(self, ref):
            return 0

    return _Bound()


class EventEngine:
    """Condition evaluation + sampling over loaded event defs."""

    def __init__(self, events):
        self.events = list(events)

    @staticmethod
    def _conditions_met(ev, age, used_ids, context, rng):
        cond = ev.get("conditions", {})
        if not (cond.get("min_age", 0) <= age <= cond.get("max_age", 200)):
            return False
        if cond.get("once_per_lifetime", True) and ev["id"] in used_ids:
            return False
        for clause in cond.get("requires", []):
            kind = clause.get("kind")
            if kind == "alive":
                if not context.alive_role(clause.get("role")):
                    return False
            elif kind == "has_illness":
                if not context.has_illness(clause.get("illness")):
                    return False
            elif kind == "employed":
                if not context.is_employed():
                    return False
            elif kind == "in_school":
                if not context.is_in_school():
                    return False
            elif kind == "min_stat":
                if context.stat(clause.get("stat")) < clause.get("value", 0):
                    return False
            elif kind == "min_money":
                if context.money() < clause.get("value", 0):
                    return False
            elif kind == "flag":
                if not context.flag(clause.get("name")):
                    return False
            elif kind == "no_partner":
                if context.alive_role("partner"):
                    return False
            elif kind == "partner_status":
                status = context.partner_status()
                if status is None:
                    return False
                if "value" in clause and status != clause["value"]:
                    return False
                if "max" in clause and status > clause["max"]:
                    return False
            elif kind == "min_age_sampled":
                if age < rng.randint(*clause["randint"]):
                    return False
            elif kind == "max_age_sampled":
                if age >= rng.randint(*clause["randint"]):
                    return False
            elif kind == "partner_relationship_below":
                rel = context.partner_relationship()
                if rel is None or rel >= rng.randint(*clause["randint"]):
                    return False
            elif kind == "karma_gate":
                # legacy: passes when NOT x_in_y(karma, value)
                if rng.uniform(0, clause.get("value", 1)) < context.stat(
                        "karma"):
                    return False
        return True

    # ------------------------------------------------------------- v3 path
    # ONE evaluated dispatch/condition path for v3 (M2.5 fix round 2, R7):
    # the same tree is used by offer creation (this selector), the yearly
    # phases (A's engines and C's romance_events_phase) and resolve-time
    # validity. Leaves follow the frozen table in event-schema-v3.md §2;
    # reads go through A's graph/knowledge queries and B's published
    # policy predicates — this file never mints domain truth.

    def sample_v3(self, rng, age, seen_ids, context, prefixes=None,
                  actor_id=None):
        """The single v3 dispatch path: cadence (seen_ids) + age bounds +
        the evaluated condition tree + role bindability, then ONE
        weighted draw. `prefixes` restricts the pool (e.g. ("rom_",)).
        Returns (event, bindings) or (None, None); bindings are the ids
        probed here so offer creation can prebind the SAME people."""
        actor_id = actor_id or _v3_person_id(context)
        pool = []
        for ev in self.events:
            if ev.get("schema_version") != 3:
                continue
            if prefixes and not any(ev["id"].startswith(p)
                                    for p in prefixes):
                continue
            if ev["id"] in seen_ids:
                continue
            cond = ev.get("conditions") or {}
            if not (cond.get("min_age", 0) <= age <=
                    cond.get("max_age", 200)):
                continue
            bindings = v3_bindable(ev, actor_id, context)
            if bindings is None:
                continue
            if not evaluate_v3_conditions(cond, context, bindings,
                                          actor_id):
                continue
            pool.append((ev, bindings))
        if not pool:
            return None, None
        total = sum(ev.get("weight", 1) for ev, _ in pool)
        roll = rng.randrange(total)
        for ev, bindings in pool:
            roll -= ev.get("weight", 1)
            if roll < 0:
                return ev, bindings
        return pool[-1]

    def roll_independent(self, rng, age, used_ids, context):
        """A's coordinator entry point for finding-round #5 (independent
        chance_n events, handled separately from the flavor pool)."""
        return self.sample_independent(rng, age, used_ids, context)

    def sample_independent(self, rng, age, used_ids, context):
        """Findings-round #5: events WITH chance_n carry their own
        independent per-year 1-in-N gate (legacy parity) and are handled
        SEPARATELY from the flavor pool, in declared (file) order, so two
        successful independent events both happen and flavor content can
        never dilute their rates. Returns the list of passed events
        (possibly empty); each occurrence must be handled exactly once by
        the coordinator with a per-occurrence done marker."""
        out = []
        order = {eid: i for i, eid in enumerate(LEGACY_CHANCE_ORDER)}
        eligible = [ev for ev in self.events if ev.get("chance_n")
                    and ev.get("schema_version") != 3]
        eligible.sort(key=lambda ev: order.get(ev["id"], len(order)))
        for ev in eligible:
            if not self._conditions_met(ev, age, used_ids, context, rng):
                continue
            if rng.randrange(ev["chance_n"]) == 0:
                out.append(ev)
        return out

    def sample_flavor(self, rng, age, used_ids, context):
        """A's coordinator entry point for the weighted flavor pool."""
        return self.sample(rng, age, used_ids, context)

    def sample(self, rng, age, used_ids, context):
        """FLAVOR selection only: weighted pool (with the ~1-in-3
        eligible-years gate, M1 template parity) over events WITHOUT
        chance_n. Independent legacy chances run via sample_independent.
        v3 (M2.5) events are NOT sampled here — they enter through C's
        offer creation (romance/family pipeline), not the flavor pool."""
        pool = [ev for ev in self.events
                if not ev.get("chance_n")
                and ev.get("schema_version") != 3
                and self._conditions_met(ev, age, used_ids, context, rng)]
        if not pool:
            return None
        if rng.randrange(3) != 0:
            return None
        total = sum(ev.get("weight", 1) for ev in pool)
        roll = rng.randrange(total)
        for ev in pool:
            roll -= ev.get("weight", 1)
            if roll < 0:
                return ev
        return pool[-1]

    @staticmethod
    def _sign(value, default=1):
        """v2.1 accepts 1/-1 ints and 'positive'/'negative' strings."""
        if value in ("negative", -1):
            return -1
        if value in ("positive", 1):
            return 1
        return default

    @classmethod
    def _money_value(cls, money, rng, context, sampled=None):
        sampled = sampled or {}
        if isinstance(money, int):
            return money
        if isinstance(money, dict) and "money_pct" in money:
            lo, hi = money["money_pct"]
            sign = cls._sign(money.get("sign", 1))
            return sign * round(context.money() * rng.uniform(lo, hi))
        if isinstance(money, dict) and "sampled" in money:
            sign = cls._sign(money.get("sign", 1))
            return sign * sampled[money["sampled"]]
        return 0

    @classmethod
    def _resolve_effects(cls, effects, rng, context, sampled=None):
        """Flatten the schema Effects form to the service's flat _effects
        and sample ALL randomness ONCE (pre-sampled policy): randint ranges
        (and ordered {"sum": [...]} combinations), money_pct (with sign),
        sampled_at_offer references, and relationship role targets.
        Structural keys pass through for the resolver's _structural split.
        """
        import copy
        sampled = sampled or {}
        flat = {}
        extra_ids = []

        def sample_delta(value):
            if isinstance(value, dict) and "randint" in value:
                lo, hi = value["randint"]
                return cls._sign(value.get("sign", 1)) \
                    * rng.randint(lo, hi)
            if isinstance(value, dict) and "sum" in value:
                return sum(sample_delta(v) for v in value["sum"])
            return value

        stats = effects.get("stats", {})
        for key, value in list(stats.items()):
            flat[key] = sample_delta(value)
        # Tolerant lift: stat keys placed at the top level of an effects
        # map (e.g. "karma": -1 beside "stats") are real effects and must
        # not be dropped — fold them in unless already present.
        for key in ("happiness", "health", "smarts", "looks", "karma",
                    "stress"):
            if key in effects and key not in flat:
                flat[key] = sample_delta(effects[key])
        money = effects.get("money")
        if isinstance(money, dict) and "sum" in money:
            flat["money"] = sum(
                cls._money_value(m, rng, context, sampled)
                for m in money["sum"])
        elif money is not None:
            flat["money"] = cls._money_value(money, rng, context, sampled)
        rel = effects.get("relationship")
        if isinstance(rel, dict):
            delta = rel.get("delta")
            delta = sample_delta(delta)
            role = rel.get("role")
            if role in ("random_parent", "random_relation"):
                pid = context.random_person_id(
                    parents_only=(role == "random_parent"), rng=rng)
                if pid:
                    extra_ids.append(pid)
                    flat["relationship"] = delta
            elif role in ("mother", "father", "partner", "child",
                          "all_parents"):
                pids = context.role_person_ids(role)
                extra_ids.extend(pids)
                flat["relationship"] = delta
            else:
                flat["relationship"] = delta
        return flat, extra_ids

    _STRUCTURAL_KEYS = ("lose_partner", "divorce", "gain_partner",
                        "partner_death", "terminal", "add_illness",
                        "remove_illness", "learn_trait", "lose_trait",
                        "set_flag")

    @classmethod
    def _split_structural(cls, effects):
        """Structural effects are NOT applied by the legacy money/stats
        resolver; they ride in a separate _structural key so the current
        resolver ignores them safely (A applies them via rule actions)."""
        return {k: v for k, v in effects.items() if k in cls._STRUCTURAL_KEYS}

    @staticmethod
    def _sample_offer_values(ev, rng, context):
        """sampled_at_offer block: drawn once, kept private in the pending."""
        sampled = {}
        for name, spec in (ev.get("sampled_at_offer") or {}).items():
            if isinstance(spec, dict) and "money_pct" in spec:
                lo, hi = spec["money_pct"]
                sign = EventEngine._sign(spec.get("sign", 1))
                sampled[name] = sign * round(
                    context.money() * rng.uniform(lo, hi))
            elif isinstance(spec, dict) and "randint" in spec:
                sampled[name] = rng.randint(*spec["randint"])
            else:
                sampled[name] = spec
        return sampled

    @staticmethod
    def _pick_outcome(outcomes, rng):
        total = sum(o.get("weight", 1) for o in outcomes)
        roll = rng.randrange(total)
        for o in outcomes:
            roll -= o.get("weight", 1)
            if roll < 0:
                return o
        return outcomes[-1]

    @classmethod
    def instantiate(cls, ev, seq, player_name, alive_role_ids=None, rng=None,
                    context=None):
        """Build the service pending record (same shape as M1 templates;
        resolver consumes _effects/_outcome). All randomness is sampled
        HERE, once, per the pre-sampled policy. Event-level pre-effects are
        merged into every option (legacy applies them at offer time)."""
        rng = rng or random.Random()
        context = context or _NullContext()
        if ev.get("auto"):
            raise EventValidationError(
                f"{ev['id']}: auto events use instantiate_auto()")
        guard_event_adults(ev, context)
        person_ids = list(alive_role_ids or [])
        text = cls._render_text(ev["text"], player_name, context)
        sampled = cls._sample_offer_values(ev, rng, context)
        pre = ev.get("effects", {})
        pre_flat, pre_extra = ([], [])
        if pre:
            pre_flat, pre_extra = cls._resolve_effects(pre, rng, context,
                                                       sampled)
        options = []
        for c in ev["choices"]:
            # choice-level effects always apply; outcomes/branches overlay
            base = dict(c.get("effects", {}))
            bundle = {"effects": base,
                      "outcome_text": c.get("outcome_text", "")}
            if c.get("outcomes"):
                picked = cls._pick_outcome(c["outcomes"], rng)
                base.update(picked.get("effects", {}))
                bundle = {"effects": base,
                          "outcome_text": picked.get("outcome_text",
                                                      bundle["outcome_text"])}
            if c.get("branches"):
                br = c["branches"]
                sample = br.get("sample", {})
                x = context.field(sample.get("x")) \
                    if isinstance(sample.get("x"), str) \
                    else sample.get("x", 1)
                y = context.field(sample.get("y")) \
                    if isinstance(sample.get("y"), str) \
                    else sample.get("y", 1)
                draws = sample.get("draws", 1)
                hit = False
                for _ in range(draws):
                    if rng.uniform(0, y) < x:
                        hit = True
                        break
                branch = br["then"] if hit else br["else"]
                base.update(branch.get("effects", {}))
                bundle = {"effects": base,
                          "outcome_text": branch.get(
                              "outcome_text", bundle["outcome_text"])}
            # #4: options carry CHOICE effects only; event pre-effects are
            # sampled once at offer creation and served via the pending's
            # pre_effects (applied at offer time by the coordinator).
            fx, extra = cls._resolve_effects(
                bundle.get("effects", {}), rng, context, sampled)
            structural = cls._split_structural(bundle.get("effects", {}))
            # affordability projection: cost gates only (P6); the resolver
            # still enforces it authoritatively.
            enabled, reason = True, None
            if fx.get("money", 0) < 0 and context is not None \
                    and -fx["money"] > context.money():
                enabled = False
                reason = (f"this choice costs ${-fx['money']} and you have "
                          f"${context.money()}")
            options.append({
                "id": f"d_{seq}_{c['id']}",
                "label": c["label"],
                "known_effects": list(c.get("known_effects", [])),
                "enabled": enabled,
                "reason": reason,
                "inputs": c.get("inputs", []),
                "_outcome": bundle["outcome_text"],
                "_effects": fx,
                "_structural": structural or None,
                "_person_ids_extra": extra or None,
            })
        return {
            "id": f"d_{seq}",
            "kind": ev["id"],
            "event_id": ev["id"],
            "title": ev["title"],
            "text": text,
            "person_ids": person_ids,
            "sampled": sampled or None,
            "pre_effects": pre_flat or None,
            "_pre_person_ids_extra": pre_extra or None,
            "options": options,
        }

    @classmethod
    def instantiate3(cls, ev, seq, rng, context, prebound=None):
        """Build a v3 pending record (frozen event-schema-v3.md §6):
        roles are bound ONCE here to concrete ids and stored; resolve
        never rebinds. `prebound` lets an offer-creation path pin a role
        to a specific id it already chose (e.g. the dating app picked the
        target BEFORE scoring — the pending must bind THAT person, never
        re-pick). The projection is the FROZEN M2 pending_decision
        shape (title/text/options with known_effects, enabled+reason,
        free exit) plus additive v3 fields (schema_dialect, bindings,
        interaction, audience, policy_revision, _domain internals)."""
        rng = rng or random.Random()
        actor_id = _v3_person_id(context)
        if actor_id is None:
            raise EventValidationError(
                f"{ev['id']}: context exposes no player person_id")
        roles, _ = _v3_role_names(ev)
        bindings = {}
        person_ids = []
        prebound = prebound or {}
        for name, cfg in roles.items():
            if name in prebound:
                bound = prebound[name]
            else:
                select = cfg.get(
                    "select", "player" if name == "actor" else None)
                if select == "player":
                    bound = actor_id
                else:
                    bound = PREDICATES[cfg["predicate"]](actor_id, context)
                    if bound is None and not cfg.get("optional"):
                        raise EventValidationError(
                            f"{ev['id']}: required role {name!r} "
                            f"did not bind")
            bindings[name] = bound
            if cfg.get("kind", "person") == "person" and bound is not None:
                person_ids.append(bound)
        guard_event_adults(ev, context, {k: v for k, v in bindings.items()
                                       if k in roles and
                                       roles[k].get("kind", "person") == "person"})
        player_name = None
        if context is not None:
            player_name = context.person_name(actor_id)
        bound_ctx = _v3_render_context(context, bindings)
        text = cls._render_text(ev["text"], player_name or "I", bound_ctx)
        sampled = cls._sample_offer_values(ev, rng, bound_ctx)
        pre = ev.get("effects", {})
        pre_flat, pre_extra = ([], [])
        if pre:
            pre_flat, pre_extra = cls._resolve_effects(pre, rng, bound_ctx,
                                                       sampled)
        options = []
        for c in ev["choices"]:
            base = dict(c.get("effects", {}))
            bundle = {"effects": base,
                      "outcome_text": c.get("outcome_text", "")}
            if c.get("outcomes"):
                picked = cls._pick_outcome(c["outcomes"], rng)
                base.update(picked.get("effects", {}))
                bundle = {"effects": base,
                          "outcome_text": picked.get(
                              "outcome_text", bundle["outcome_text"])}
            fx, extra = cls._resolve_effects(
                bundle.get("effects", {}), rng, bound_ctx, sampled)
            domain = {k: v for k, v in bundle.get("effects", {}).items()
                      if k in V3_DOMAIN_EFFECTS}
            structural = cls._split_structural(bundle.get("effects", {}))
            enabled, reason = True, None
            gate = 0
            cost = c.get("cost") or {}
            if isinstance(cost.get("money"), int):
                gate = max(0, cost["money"])
            if fx.get("money", 0) < 0:
                gate = max(gate, -fx["money"])
            if context is not None and gate > 0 and gate > context.money():
                enabled = False
                reason = (f"this choice costs ${gate:,} and you have "
                          f"${context.money():,}")
            options.append({
                "id": f"d_{seq}_{c['id']}",
                "label": c["label"],
                "known_effects": list(c.get("known_effects", [])),
                "enabled": enabled,
                "reason": reason,
                "inputs": c.get("inputs", []),
                "_outcome": bundle["outcome_text"],
                "_effects": fx,
                "_domain": domain or None,
                "_structural": structural or None,
                "_person_ids_extra": extra or None,
            })
        return {
            "id": f"d_{seq}",
            "kind": ev["id"],
            "event_id": ev["id"],
            "title": ev["title"],
            "text": text,
            "person_ids": person_ids,
            "sampled": sampled or None,
            "pre_effects": pre_flat or None,
            "_pre_person_ids_extra": pre_extra or None,
            "options": options,
            # --- v3 additive fields (frozen contract §6) ---
            "schema_dialect": "v3",
            "bindings": bindings,
            "interaction": ev.get("interaction"),
            "audience": ev.get("audience"),
            "fact_id": ev.get("secret_fact"),
            "policy_revision": getattr(context, "policy_revision", None),
        }

    @staticmethod
    def _merge_effects(pre, choice):
        """Deep-merge choice effects over event pre-effects. Per-stat and
        money entries present in BOTH are combined as an ordered {"sum":
        [...]} so every consequence applies (a choice stats map must not
        replace the whole pre-effect map)."""
        merged = dict(pre)
        for key, value in choice.items():
            if key == "stats":
                stats = dict(merged.get("stats", {}))
                for sk, sv in value.items():
                    if sk in stats:
                        stats[sk] = {"sum": [stats[sk], sv]}
                    else:
                        stats[sk] = sv
                merged["stats"] = stats
            elif key == "money" and key in merged:
                merged["money"] = {"sum": [merged["money"], value]}
            elif key == "relationship" and key in merged \
                    and isinstance(merged[key], dict) \
                    and isinstance(value, dict):
                rel = dict(merged[key])
                if "delta" in rel and "delta" in value:
                    rel["delta"] = {"sum": [rel["delta"], value["delta"]]}
                    for k2, v2 in value.items():
                        rel.setdefault(k2, v2)
                    merged["relationship"] = rel
                else:
                    merged[key] = value
            else:
                merged[key] = value
        return merged

    @staticmethod
    def _render_text(template, player_name, context=None):
        """Resolve {name} and {person:role} refs (engine-resolved to
        canonical names via the context, including archived people).
        Never leak schema syntax to the player."""
        out = template.replace("{name}", player_name)
        if context is None:
            return out

        def sub(match):
            role = match.group(1)
            pids = context.role_person_ids(role)
            if pids:
                name = context.person_name(pids[0])
                if name:
                    return name
            return match.group(0)
        import re as _re
        prev = None
        while prev != out:
            prev = out
            out = _re.sub(r"\{person:(\w+)\}", sub, out)
        return out

    @classmethod
    def instantiate_auto(cls, ev, rng, context):
        """v2.1 auto event: no pending record. Samples the event-level
        effects/outcomes once and returns the resolved bundle for the
        coordinator to apply immediately and journal:
        {"event_id", "_effects" (flat), "_outcome", "terminal"}."""
        rng = rng or random.Random()
        context = context or _NullContext()
        if not ev.get("auto"):
            raise EventValidationError(
                f"{ev['id']}: not an auto event")
        guard_event_adults(ev, context)
        bundle = {"effects": dict(ev.get("effects", {})),
                  "outcome_text": ev.get("outcome_text", "")
                  or ev.get("text", "")}
        if ev.get("outcomes"):
            picked = cls._pick_outcome(ev["outcomes"], rng)
            bundle = {"effects": cls._merge_effects(
                          ev.get("effects", {}),
                          picked.get("effects", {})),
                      "outcome_text": picked.get("outcome_text")
                      or ev.get("text", ""),
                      "terminal": picked.get("terminal", False)}
        merged = bundle.get("effects", {})
        fx, extra = cls._resolve_effects(merged, rng, context)
        return {
            "event_id": ev["id"],
            "_effects": fx,
            "_structural": cls._split_structural(merged) or None,
            "_outcome": bundle.get("outcome_text", ""),
            "terminal": bool(bundle.get("terminal")),
            "_person_ids_extra": extra or None,
        }


def _v3_env(context):
    env = getattr(context, "_env", None)
    if env is None:
        raise EventValidationError(
            "v3 conditions need an envelope-backed context")
    return env


def _v3_bound_id(bindings, people, role):
    pid = (bindings or {}).get(role)
    return pid if pid in people else None


def _v3_pair_edge(graph, a_id, b_id, kind):
    for edge in graph.get(kind + "s", []):
        if set(edge.get("person_ids", [])) == {a_id, b_id}:
            return edge
    return None


def _v3_node(node, context, bindings, people, pid):
    if not isinstance(node, dict):
        return False
    if "all" in node:
        return all(_v3_node(n, context, bindings, people, pid)
                   for n in node["all"])
    if "any" in node:
        return any(_v3_node(n, context, bindings, people, pid)
                   for n in node["any"])
    if "not" in node:
        return not _v3_node(node["not"], context, bindings, people, pid)
    kind = node.get("kind")
    env = _v3_env(context)
    graph = env["graph"]

    if kind == "bound":
        return (bindings or {}).get(node.get("role")) is not None

    if kind in ("relationship_state", "union_state", "expectation"):
        a_id = _v3_bound_id(bindings, people, node.get("role_a", "actor"))
        b_id = _v3_bound_id(bindings, people, node.get("role_b"))
        if not a_id or not b_id:
            return False
        if kind == "relationship_state":
            edge = _v3_pair_edge(graph, a_id, b_id, "relationship")
            if not edge or edge.get("status") != "active":
                return False
            if node.get("kinds") and edge.get("kind") not in node["kinds"]:
                return False
            for metric in ("chemistry", "trust", "satisfaction"):
                if metric in node and \
                        float(edge.get(metric, 0)) < node[metric]:
                    return False
            return True
        if kind == "union_state":
            edge = _v3_pair_edge(graph, a_id, b_id, "union")
            if not edge:
                return False
            if node.get("status") and \
                    edge.get("status") not in node["status"]:
                return False
            if node.get("kinds") and edge.get("kind") not in node["kinds"]:
                return False
            return True
        # expectation: the pair's exclusivity agreement; absent agreement
        # defaults to exclusive (affairs = violating an exclusive one;
        # consensual plural is recorded explicitly, never inferred).
        edge = _v3_pair_edge(graph, a_id, b_id, "relationship")
        agreement = ((edge or {}).get("exclusivity") or {}).get(
            "agreement") or (edge or {}).get("expectation") or "exclusive"
        want = node.get("value", node.get("values"))
        if isinstance(want, list):
            return agreement in want
        return agreement == want

    if kind == "recognition_status":
        union_id = (bindings or {}).get(node.get("role", node.get("union_role")))
        for union in graph.get("unions", []):
            if union.get("union_id") == union_id:
                status = (union.get("recognition") or {}).get("status")
                want = node.get("status", node.get("values"))
                return status in want if isinstance(want, list) \
                    else status == want
        return False

    if kind == "kinship":
        from src.game.features.family_graph import graph as graph_mod
        a_id = _v3_bound_id(bindings, people, node.get("role_a", "actor"))
        b_id = _v3_bound_id(bindings, people, node.get("role_b"))
        if not a_id or not b_id:
            return False
        category = graph_mod.graph_relatedness(graph, a_id, b_id, people)[
            "category"]
        want = node.get("categories", node.get("category"))
        return category in want if isinstance(want, list) \
            else category == want

    if kind == "milestone":
        from src.game.features.parenting import core as parenting_core
        child_id = (bindings or {}).get(node.get("role"))
        if child_id is None:
            return False
        milestones = (env.get("parenting", {}).get("milestones", {})
                      .get(child_id, {}))
        return node.get("id") in milestones

    if kind in ("known_fact", "reputation_at_least"):
        from src.game.features.knowledge import core as knowledge
        observer = node.get("observer", "actor")
        observer_id = pid if observer == "actor" else \
            (bindings or {}).get(observer)
        if observer_id is None:
            return False
        # knowledge core normalizes the reserved 'player' observer itself
        if kind == "known_fact":
            for fact_id in knowledge.known_facts(env, observer_id):
                fact = knowledge.get_fact(env, fact_id)
                if node.get("fact_id") and \
                        fact_id != node["fact_id"]:
                    continue
                if node.get("fact_kind") and \
                        fact.get("kind") != node["fact_kind"]:
                    continue
                if not knowledge.knows(
                        env, observer_id, fact_id,
                        confidence_at_least=node.get("confidence", 0.0)):
                    continue
                return True
            return False
        target_id = (bindings or {}).get(node.get("role"))
        if target_id is None:
            return False
        scope = node.get("scope", "global")
        return knowledge.reputation_as_known(
            env, target_id, scope, observer_id) >= node.get("value", 0)

    # B's published policy predicates (frozen names, evaluator.py)
    if kind in ("tenet_stance", "faith_id", "jurisdiction_id",
                "attraction_band", "openness_band", "monogamy_band"):
        from src.game.features.family_graph.service_view import \
            ServicePolicyView
        from src.game.features.relationship_policy import evaluator as \
            policy_eval
        policy = ServicePolicyView(env)
        if kind == "jurisdiction_id":
            current = policy_eval.predicate_jurisdiction_id(policy)
            want = node.get("value", node.get("values"))
            return current in want if isinstance(want, list) \
                else current == want
        target_id = pid if node.get("role", "actor") == "actor" else \
            (bindings or {}).get(node.get("role"))
        if target_id is None:
            return False
        profile = policy.profile(target_id)
        if kind == "tenet_stance":
            stance = policy_eval.predicate_tenet_stance(
                policy.catalog, policy.overrides,
                profile.get("faith_id"), node.get("tenet_id"),
                profile.get("overrides") or None)
            want = node.get("stance", node.get("value"))
            return stance in want if isinstance(want, list) \
                else stance == want
        if kind == "faith_id":
            current = policy_eval.predicate_faith_id(profile)
            want = node.get("value", node.get("values"))
            return current in want if isinstance(want, list) \
                else current == want
        band_fn = {"attraction_band": policy_eval.predicate_attraction_band,
                   "openness_band": policy_eval.predicate_openness_band,
                   "monogamy_band":
                       policy_eval.predicate_monogamy_band}[kind]
        current = band_fn(profile)
        want = node.get("band", node.get("value"))
        return current in want if isinstance(want, list) \
            else current == want

    raise EventValidationError(
        f"unknown v3 condition kind {kind!r}")


def evaluate_v3_conditions(conditions, context, bindings=None, actor_id=None):
    """Evaluate one v3 condition tree (frozen event-schema-v3.md §2).
    Scalar bounds (min_age/max_age) compose with all/any/not trees of the
    frozen policy/graph/knowledge leaves. Used by the single dispatch
    path (sample_v3) and available for resolve-time validity checks."""
    conditions = conditions or {}
    bindings = bindings or {}
    people = _v3_people(context)
    pid = actor_id or bindings.get("actor") or _v3_person_id(context)
    person = people.get(pid)
    age = getattr(person, "age", 0)
    if not (conditions.get("min_age", 0) <= age <=
            conditions.get("max_age", 200)):
        return False
    tree = {k: v for k, v in conditions.items() if k not in (
        "min_age", "max_age", "once_per_lifetime", "requires", "chance_n")}
    if not tree:
        return True
    return _v3_node(tree, context, bindings, people, pid)


def v3_bindable(ev, actor_id, context):
    """Probe every required role once against THIS envelope (read-only).
    Returns the bindings dict, or None when a required role cannot bind.
    The returned ids can prebind instantiate3 so the pending names the
    same people the selector validated — never a resolve-time re-pick."""
    roles, _ = _v3_role_names(ev)
    bindings = {}
    for name, cfg in roles.items():
        select = cfg.get("select", "player" if name == "actor" else None)
        if select == "player":
            bound = actor_id
        else:
            fn = PREDICATES.get(cfg.get("predicate"))
            if fn is None:
                return None
            bound = fn(actor_id, context)
            if bound is None and not cfg.get("optional"):
                return None
        bindings[name] = bound
    return bindings


class _NullContext:
    """Minimal context for standalone loader use (tests)."""
    def alive_role(self, role):
        return False

    def has_illness(self, illness):
        return False

    def is_employed(self):
        return False

    def is_in_school(self):
        return False

    def stat(self, name):
        return 0

    def money(self):
        return 0

    def flag(self, name):
        return False

    def partner_status(self):
        return None

    def partner_relationship(self):
        return None

    def field(self, ref):
        return 0

    def person_name(self, person_id):
        dead = self._env.get("dead_ids", [])
        for p in self._service._walk_people(self._player):
            if p.person_id == person_id:
                return p.name
        for p in self._env.get("archive", []):
            if getattr(p, "person_id", None) == person_id:
                return p.name
        return None

    def role_person_ids(self, role):
        return []

    def random_person_id(self, parents_only=False, rng=None):
        return None


class GameContext:
    """Condition context over the service envelope (duck-typed for C)."""

    def __init__(self, env, service):
        self._env = env
        self._service = service
        self._player = env["player"]

    @property
    def people_by_id(self):
        by_id = {}
        for person in self._service._walk_people(self._player):
            by_id[person.person_id] = person
        for person in self._env.get("archive", []):
            if person.person_id not in by_id:
                by_id[person.person_id] = person
        return by_id

    def alive_role(self, role):
        from src.lifesim_lib.lifesim_lib import Gender
        dead = self._env.get("dead_ids", [])
        if role in ("mother", "father", "parents"):
            parents = [p for p in self._service._walk_people(self._player)
                       if type(p).__name__ == "Parent"
                       and p.person_id not in dead]
            if role == "parents":
                return bool(parents)
            want = Gender.Female if role == "mother" else Gender.Male
            return any(p.gender == want for p in parents)
        if role == "partner":
            p = self._player.partner
            return p is not None and p.person_id not in dead
        if role == "child":
            return any(c.person_id not in dead
                       for c in self._player.children)
        return False

    def has_illness(self, illness):
        return any(str(i) == illness for i in self._player.illnesses)

    def is_employed(self):
        return self._player.has_job

    def is_in_school(self):
        return self._player.is_in_school() or self._player.uv_years > 0

    def stat(self, name):
        return getattr(self._player, name, 0)

    def money(self):
        return self._player.money

    def flag(self, name):
        return bool(self._env.setdefault("flags", {}).get(name))

    def partner_status(self):
        p = self._player.partner
        return p.status if p is not None else None

    def partner_relationship(self):
        p = self._player.partner
        return p.relationship if p is not None else None

    def person_name(self, person_id):
        dead = self._env.get("dead_ids", [])
        for p in self._service._walk_people(self._player):
            if p.person_id == person_id:
                return p.name
        for p in self._env.get("archive", []):
            if getattr(p, "person_id", None) == person_id:
                return p.name
        return None

    def role_person_ids(self, role):
        dead = self._env.get("dead_ids", [])
        people = self._service._walk_people(self._player)
        if role in ("mother", "father"):
            from src.lifesim_lib.lifesim_lib import Gender
            want = Gender.Female if role == "mother" else Gender.Male
            return [p.person_id for p in people
                    if type(p).__name__ == "Parent"
                    and p.gender == want and p.person_id not in dead]
        if role == "partner":
            p = self._player.partner
            return [p.person_id] if p is not None \
                and p.person_id not in dead else []
        if role == "child":
            return [c.person_id for c in self._player.children
                    if c.person_id not in dead]
        if role == "all_parents":
            return [p.person_id for p in people
                    if type(p).__name__ == "Parent"
                    and p.person_id not in dead]
        return []

    def field(self, ref):
        """field_ref: player.<attr> | partner.<attr> (+ partner.compatibility
        = the one-draw compatibility check, 100 or 0); unknown -> 0."""
        if not isinstance(ref, str) or "." not in ref:
            return 0
        owner, attr = ref.split(".", 1)
        if owner == "partner" and attr == "compatibility":
            partner = self._player.partner
            if partner is None:
                return 0
            return 100 if partner.compatibility_check(self._player) else 0
        obj = self._player if owner == "player" else \
            getattr(self._player, "partner", None) if owner == "partner" \
            else None
        value = getattr(obj, attr, 0) if obj is not None else 0
        try:
            return int(value)
        except (TypeError, ValueError):
            return 0

    def random_person_id(self, parents_only=False, rng=None):
        dead = self._env.get("dead_ids", [])
        people = [p for p in self._service._walk_people(self._player)
                  if p is not self._player and p.person_id not in dead]
        if parents_only:
            people = [p for p in people
                      if type(p).__name__ == "Parent"]
        if not people:
            return None
        if rng is not None:
            return people[rng.randrange(len(people))].person_id
        return people[0].person_id
