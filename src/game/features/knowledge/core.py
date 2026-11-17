"""Knowledge core (M2.5, Kimi A implements C's frozen
reputation-knowledge.md): engine-true FACTS vs observer KNOWLEDGE
records, and scoped reputation stores.

Separation of truth and knowledge:
- facts exist regardless of who knows them (immutable once created;
  `ended_turn` is set by forgiveness/end transitions, never erased);
- knowledge records say WHO learned WHAT, WHEN, from WHERE, how
  confidently. A global/engine-true fact is never NPC knowledge by
  itself. The reserved observer id "player" is the PLAYER observer.
"""

import uuid

KNOWLEDGE_SCHEMA_VERSION = 1

FACT_KINDS = ("affair", "secret_union", "discovery", "disclosure",
              "forgiveness", "breakup", "conversion")

KNOWLEDGE_SOURCES = ("witnessed", "told", "discovered", "rumor")

PLAYER_OBSERVER = "player"


def _err(code, message, status=400):
    from src.game.features.family_graph.graph import GraphError
    raise GraphError(code, message, status)


def new_knowledge_state():
    return {"schema_version": KNOWLEDGE_SCHEMA_VERSION,
            "facts": [], "records": [], "reputation": {},
            "reputation_posted": []}


def normalize_knowledge(env):
    state = env.get("knowledge")
    if not isinstance(state, dict):
        env["knowledge"] = new_knowledge_state()
        return True
    dirty = False
    defaults = (("facts", []), ("records", []), ("reputation", {}),
                ("reputation_posted", []))
    for key, default in defaults:
        if not isinstance(state.get(key), type(default)):
            state[key] = default
            dirty = True
    if state.get("schema_version") != KNOWLEDGE_SCHEMA_VERSION:
        state["schema_version"] = KNOWLEDGE_SCHEMA_VERSION
        dirty = True
    return dirty


# -------------------------------------------------------------------- facts

def mint_fact(env, kind, subject_ids, turn, occurrence_id=None,
              union_id=None, relationship_id=None):
    """Create an immutable engine-true fact. Returns fact_id."""
    if kind not in FACT_KINDS:
        _err("invalid_input", "unknown fact kind %r" % kind)
    if not isinstance(subject_ids, list) or len(subject_ids) < 1 \
            or not all(isinstance(s, str) for s in subject_ids):
        _err("invalid_input", "subject_ids must be a list of person ids")
    normalize_knowledge(env)
    fact = {
        "fact_id": "fact-%s" % uuid.uuid4().hex[:12],
        "kind": kind,
        "subject_ids": list(subject_ids),
        "union_id": union_id,
        "relationship_id": relationship_id,
        "occurrence_id": occurrence_id,
        "created_turn": turn,
        "ended_turn": None,
    }
    env["knowledge"]["facts"].append(fact)
    return fact["fact_id"]


def get_fact(env, fact_id):
    normalize_knowledge(env)
    for fact in env["knowledge"]["facts"]:
        if fact["fact_id"] == fact_id:
            return fact
    _err("not_found", "unknown fact %r" % fact_id, 404)


# ----------------------------------------------------------------- knowledge

def _norm_observer(env, observer_id):
    """The player is recorded under the reserved id 'player' (frozen
    §1), whatever their person_id."""
    player = env.get("player")
    if player is not None and observer_id == getattr(player, "person_id",
                                                     None):
        return PLAYER_OBSERVER
    return observer_id


def learn(env, observer_id, fact_id, turn, source, confidence=1.0):
    """Append a knowledge record (one per (observer, fact): re-learning
    raises confidence and keeps the earliest learned_turn)."""
    observer_id = _norm_observer(env, observer_id)
    if source not in KNOWLEDGE_SOURCES:
        _err("invalid_input", "unknown knowledge source %r" % source)
    if not isinstance(confidence, (int, float)) \
            or isinstance(confidence, bool) or not 0 <= confidence <= 1:
        _err("invalid_input", "confidence must be in [0, 1]")
    get_fact(env, fact_id)  # dangling fact ids reject
    normalize_knowledge(env)
    records = env["knowledge"]["records"]
    for rec in records:
        if rec["observer_id"] == observer_id \
                and rec["fact_id"] == fact_id:
            rec["confidence"] = max(rec["confidence"], confidence)
            rec["learned_turn"] = min(rec["learned_turn"], turn)
            return rec
    rec = {"observer_id": observer_id, "fact_id": fact_id,
           "learned_turn": turn, "source": source,
           "confidence": float(confidence)}
    records.append(rec)
    return rec


def knows(env, observer_id, fact_id, confidence_at_least=0.0):
    """Does this observer know this fact (at confidence)? The ONLY way
    an NPC can react to a secret."""
    observer_id = _norm_observer(env, observer_id)
    normalize_knowledge(env)
    for rec in env["knowledge"]["records"]:
        if rec["observer_id"] == observer_id \
                and rec["fact_id"] == fact_id \
                and rec["confidence"] >= confidence_at_least:
            return True
    return False


def known_facts(env, observer_id):
    observer_id = _norm_observer(env, observer_id)
    normalize_knowledge(env)
    return [rec["fact_id"] for rec in env["knowledge"]["records"]
            if rec["observer_id"] == observer_id]


# -------------------------------------------------------------- reputation

def reputation_get(env, person_id, scope_id):
    normalize_knowledge(env)
    return env["knowledge"]["reputation"].get(person_id, {}).get(
        scope_id, 0)


def reputation_delta(env, person_id, scope_id, delta, occurrence_key):
    """Post a scoped reputation delta ONCE per occurrence_key
    (frozen §4: dedupe per (fact, observer, jurisdiction, occurrence) —
    re-witnessing never re-fines)."""
    normalize_knowledge(env)
    state = env["knowledge"]
    if occurrence_key in state["reputation_posted"]:
        return False
    value = state["reputation"].setdefault(person_id, {}).get(scope_id, 0)
    value = max(-100, min(100, value + int(delta)))
    state["reputation"][person_id][scope_id] = value
    state["reputation_posted"].append(occurrence_key)
    return True


def reputation_changes_known(env, observer_id):
    """Fact-backed changes this observer learned; world totals are private.

    Old facts without provenance contribute nothing. Learning a fact twice
    does not duplicate its effects, and hidden scopes leave no UI row.
    """
    known = set(known_facts(env, observer_id))
    return [change for fact in env["knowledge"]["facts"]
            if fact["fact_id"] in known
            for change in fact.get("reputation_changes", [])]


def reputation_as_known(env, person_id, scope_id, observer_id):
    """Project only this observer's fact-backed changes in the scope."""
    value = sum(change["delta"] for change in reputation_changes_known(
        env, observer_id) if change.get("person_id") == person_id
        and change.get("scope", "global") == scope_id)
    return max(-100, min(100, value))


# ------------------------------------------------------- journal visibility

def journal_entry_visible(env, entry):
    """Server-side player_known filter (frozen §2, Claude decision 3):
    applied where entries are SERIALIZED, so a private occurrence never
    reaches the client even by UI bug. Entries may carry an `audience`
    tag: 'public' (default), 'player_known' (requires a player knowledge
    record for the tagged fact), or 'private' (never serialized)."""
    audience = entry.get("audience", "public")
    if isinstance(audience, dict):
        audience = audience.get("scope", "public")
    if audience == "private":
        return False
    if audience == "player_known":
        fact_id = entry.get("fact_id")
        if not fact_id:
            return False
        return knows(env, PLAYER_OBSERVER, fact_id)
    return True


def pending_visible(env, pending):
    """Pending payloads are subject to the same server-side audience gate."""
    if not pending:
        return False
    audience = pending.get("audience") or {}
    scope = audience.get("scope", "public") if isinstance(audience, dict) else audience
    if scope == "private":
        return False
    if scope == "player_known":
        if pending.get("fact_id"):
            return knows(env, PLAYER_OBSERVER, pending["fact_id"])
        bindings = pending.get("bindings") or {}
        participants = audience.get("participants", [])
        return any(bindings.get(role, role) == env["player"].person_id
                   for role in participants)
    return scope == "public"


# ------------------------------------------------------------- projections

def known_secret_relationship_ids(env, observer_id=PLAYER_OBSERVER):
    """Relationship edges the observer legitimately KNOWS despite their
    `secret` visibility: a fact naming the edge (relationship_id) or its
    subject pair, with a knowledge record for this observer. Undiscovered
    secret edges stay hidden from every projection (frozen §2)."""
    normalize_knowledge(env)
    graph = env.get("graph") or {}
    known_pairs = set()
    known_edge_ids = set()
    for fact in env["knowledge"]["facts"]:
        if not knows(env, observer_id, fact["fact_id"]):
            continue
        subjects = fact.get("subject_ids") or []
        if len(subjects) == 2:
            known_pairs.add(frozenset(subjects))
        if fact.get("relationship_id"):
            known_edge_ids.add(fact["relationship_id"])
    out = set()
    for rel in graph.get("relationships", []):
        if rel.get("visibility") != "secret":
            continue
        if rel["relationship_id"] in known_edge_ids \
                or frozenset(rel["person_ids"]) in known_pairs:
            out.add(rel["relationship_id"])
    return out
