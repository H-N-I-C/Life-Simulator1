"""Drama: jealousy/expectations, affairs, discovery, disclosure,
forgiveness (M2.5, Kimi A implements C's frozen reputation-knowledge.md
§4/§6). Consensual plural unions are NOT affairs: the `affair` fact kind
is reserved for expectation violations (an `exclusive` agreement
breached). Nothing here creates consent, lowers 18+, or manufactures
NPC knowledge.
"""

from src.game.features.family_graph import transitions
from src.game.features.knowledge import core as knowledge

TRUST_REPAIR_ON_FORGIVENESS = 15  # bounded patch via A's relationship API


# --------------------------------------------------------- expectations

def _active_edges(graph, person_id):
    rels = [r for r in graph["relationships"]
            if person_id in r["person_ids"] and r["status"] == "active"]
    unions = [u for u in graph["unions"]
              if person_id in u["person_ids"] and u["status"] == "active"]
    return rels, unions


def _exclusive_agreements(edges):
    return [e for e in edges
            if (e.get("exclusivity") or {}).get("agreement") == "exclusive"]


def is_affair_breach(env, a_id, b_id):
    """True when a romantic occurrence between a and b violates an
    `exclusive` agreement of either participant with someone else.
    Consensual open/plural arrangements (or no exclusivity promise) are
    NEVER a breach — plural is not cheating."""
    graph = env["graph"]
    rels_a, unions_a = _active_edges(graph, a_id)
    rels_b, unions_b = _active_edges(graph, b_id)
    pair = {a_id, b_id}

    def breach(rels, unions, self_id):
        for rel in _exclusive_agreements(rels):
            other = rel["person_ids"][0] if rel["person_ids"][1] == self_id \
                else rel["person_ids"][1]
            if other not in pair:
                return rel
        for union in _exclusive_agreements(unions):
            other = union["person_ids"][0] \
                if union["person_ids"][1] == self_id \
                else union["person_ids"][1]
            if other not in pair:
                return union
        return None

    return breach(rels_a, unions_a, a_id) is not None \
        or breach(rels_b, unions_b, b_id) is not None


def detect_affair(env, a_id, b_id, turn, occurrence_id=None):
    """Expectation check for a romantic occurrence between a and b.
    Returns a minted `affair` fact id when the occurrence violates an
    `exclusive` agreement of either participant with someone else;
    returns None for consensual plural unions (open/negotiated
    agreements or no exclusivity promise) — plural is not cheating.
    """
    if not is_affair_breach(env, a_id, b_id):
        return None
    return knowledge.mint_fact(
        env, "affair", [a_id, b_id], turn,
        occurrence_id=occurrence_id)


# ----------------------------------------------------- discovery / reveal

def reveal_fact(env, service, kind, about_ids, witness_ids, turn,
                occurrence_id, reputation=None):
    """A persisted discovery/reveal occurrence (frozen §4):
    1. mint the fact, 2. append witness knowledge records (source
    `witnessed`), 3. apply scoped reputation deltas deduped per
    (fact, observer, jurisdiction, occurrence) — re-witnessing the same
    fact never re-fines."""
    knowledge.normalize_knowledge(env)
    existing = next((f for f in env["knowledge"]["facts"]
                     if f.get("occurrence_id") == occurrence_id
                     and f["kind"] == kind), None)
    if existing is not None:
        if existing["subject_ids"] != list(about_ids):
            knowledge._err("invalid_input", "fact occurrence has different subjects")
        fact_id = existing["fact_id"]
    else:
        fact_id = knowledge.mint_fact(
            env, kind, list(about_ids), turn, occurrence_id=occurrence_id)
    # the subjects lived the occurrence: they know it (witnesses get
    # their records below with source `witnessed`)
    for pid in about_ids:
        knowledge.learn(env, pid, fact_id, turn, "witnessed",
                        confidence=1.0)
    for witness_id in witness_ids:
        knowledge.learn(env, witness_id, fact_id, turn, "witnessed",
                        confidence=1.0)
    for spec in reputation or []:
        fact = knowledge.get_fact(env, fact_id)
        changes = fact.setdefault("reputation_changes", [])
        if spec not in changes:
            changes.append(dict(spec))
        knowledge.reputation_delta(
            env, spec["person_id"], spec.get("scope", "global"),
            spec["delta"], occurrence_key="%s:%s:%s:%s" % (
                fact_id, spec["person_id"], spec.get("scope", "global"),
                occurrence_id))
    return fact_id


def disclose_to(env, fact_id, from_id, to_ids, turn):
    """Disclosure occurrence: the `to` observers learn the fact (source
    `told`). A secret edge's authorized-knowledge set is its
    participants plus everyone a disclosure added (frozen §2)."""
    knowledge.get_fact(env, fact_id)
    for pid in to_ids:
        knowledge.learn(env, pid, fact_id, turn, "told", confidence=1.0)


# ----------------------------------------------------- forgiveness / end

def forgive(env, service, fact_id, by_id, turn):
    """Forgiveness repairs trust; it does NOT erase history (frozen §4):
    sets the fact's ended_turn, applies A's bounded relationship trust
    patch between the forgiver and the fact's subjects, and keeps every
    knowledge record (the NPC still knows)."""
    fact = knowledge.get_fact(env, fact_id)
    if fact["ended_turn"] is not None:
        return False
    fact["ended_turn"] = turn
    graph = env["graph"]
    for subject_id in fact["subject_ids"]:
        if subject_id == by_id:
            continue
        rel = None
        for candidate in graph["relationships"]:
            if candidate["status"] == "active" \
                    and set(candidate["person_ids"]) == {by_id, subject_id}:
                rel = candidate
                break
        if rel is not None:
            trust = min(100, rel["trust"] + TRUST_REPAIR_ON_FORGIVENESS)
            transitions.update_relationship_op(
                env, rel["relationship_id"], {"trust": trust}, turn)
    # record the forgiveness occurrence itself as engine-true history
    knowledge.mint_fact(env, "forgiveness", [by_id] +
                        [s for s in fact["subject_ids"] if s != by_id],
                        turn, occurrence_id=fact_id)
    return True


def end_fact(env, fact_id, turn):
    """Typed end transition: closes the fact without the trust repair
    (a breakup of the underlying relationship, e.g.)."""
    fact = knowledge.get_fact(env, fact_id)
    if fact["ended_turn"] is None:
        fact["ended_turn"] = turn
        return True
    return False


# ------------------------------------------------------ reason filtering

def filter_reasons(env, reasons, tenet_visibility=None):
    """Display-only filter for B's reason codes (frozen §3): filtered
    codes are dropped from UI text, NEVER from scoring. `reputation`
    reasons display only when the underlying fact is known to the
    player (reason carries `fact_id` and the player has a knowledge
    record); `tenet_stance`/`faith` reasons display when the stance is
    public or disclosed-known. Numeric contributions stay server-side
    per Claude decision 4 (words only in player-facing UI)."""
    tenet_visibility = tenet_visibility or {}
    out = []
    for reason in reasons:
        code = reason.get("code")
        if code == "reputation":
            fact_id = reason.get("fact_id")
            if not fact_id or not knowledge.knows(
                    env, knowledge.PLAYER_OBSERVER, fact_id):
                continue
        if code in ("tenet_stance", "faith_tenets", "personal_tenets",
                    "faith"):
            visibility = tenet_visibility.get(code)
            if visibility is not None and visibility != "public" \
                    and not reason.get("known_to_player"):
                continue
        out.append(reason)
    return out
