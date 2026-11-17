"""Family graph core (M2.5, Kimi A). Frozen contract:
docs/collab/M2.5/contracts/graph-contract.md + graph-examples.json.

Edges are plain dicts persisted in env["graph"]. All mutations validate
the frozen invariants and raise typed GraphError codes; callers (service
adapter, B/C effect targets, editor) convert or propagate them.
"""

import uuid

GRAPH_SCHEMA_VERSION = 1

ADULT_AGE = 18

RELATIONSHIP_KINDS = ("courtship", "lover", "committed", "consort")
UNION_KINDS = ("marriage", "civil_partnership", "handfasting",
               "consort_union")
UNION_STATUSES = ("active", "divorced", "dissolved", "widowed")
RECOGNITION_STATUSES = ("recognized", "civil_only", "unrecognized",
                        "forbidden")
RECOGNITION_BASIS = ("ceremony", "registration", "common_law")
PARENT_ROLES = ("biological", "adoptive", "step", "guardian", "social")
END_UNION_HOW = ("divorce", "dissolve", "widow")

INTERACTION_KINDS = (
    "flirt", "date", "propose_courtship", "propose_union",
    "propose_plural_union", "convert_request", "repair_attempt",
    "parenting_request",  # nonromantic; romantic flag governs the guard
)
ROMANTIC_KINDS = INTERACTION_KINDS[:-1]

OFFER_STATUSES = ("pending", "accepted", "declined", "expired",
                  "superseded")

RELATEDNESS_CATEGORIES = (
    "self", "parent_child", "grandparent_grandchild", "sibling",
    "aunt_uncle_niece_nephew", "first_cousin", "kin_other", "unrelated",
    "unknown",
)
COEFFICIENT_BANDS = ("0", "<0.015", "0.015-0.0625", "0.125", "0.25",
                     "unknown")


class GraphError(Exception):
    """Typed graph failure. status mirrors the HTTP contract:
    400 data error / 404 unknown id / 409 refused or invariant."""

    def __init__(self, code, message, status=400):
        super().__init__(message)
        self.code = code
        self.message = message
        self.status = status


def _err(code, message, status=400):
    raise GraphError(code, message, status)


def _not_found(what="unknown id"):
    _err("not_found", what, 404)


def _conflict(code, message):
    _err(code, message, 409)


def _new_id(prefix):
    return "%s-%s" % (prefix, uuid.uuid4().hex[:12])


def _bounded_int(value, name, lo=0, hi=100):
    if not isinstance(value, int) or isinstance(value, bool) \
            or not lo <= value <= hi:
        _err("invalid_input", "%s must be an integer in [%d, %d]"
             % (name, lo, hi))
    return value


# ---------------------------------------------------------------- envelope

def new_graph():
    return {
        "schema_version": GRAPH_SCHEMA_VERSION,
        "relationships": [],
        "unions": [],
        "parentage": [],
        "households": [],
        "offers": [],
        "primary_relationship_id": None,
        "conception_outcomes": {},
    }


def validate_graph(graph):
    """Structural check for envelope validation (deep person checks stay
    in the transition functions; this must be cheap and exception-free on
    envelope load)."""
    if not isinstance(graph, dict):
        return False
    if graph.get("schema_version") != GRAPH_SCHEMA_VERSION:
        return False
    for key in ("relationships", "unions", "parentage", "households",
                "offers"):
        if not isinstance(graph.get(key), list):
            return False
    if not isinstance(graph.get("primary_relationship_id"),
                      (str, type(None))) \
            or not isinstance(graph.get("conception_outcomes"), dict):
        return False
    return True


def normalize_envelope(env):
    """Backfill the frozen M2.5 fields onto any save (old saves included).
    Returns True when the envelope changed (caller persists once)."""
    dirty = False
    if not validate_graph(env.get("graph")):
        env["graph"] = new_graph()
        dirty = True
    if not isinstance(env.get("editor_audit"), list):
        env["editor_audit"] = []
        dirty = True
    return dirty


# ------------------------------------------------------------------ people

def is_adult(person):
    return getattr(person, "age", 0) >= ADULT_AGE


def assert_adult_romantic(person_a, person_b):
    """The fixed rule: both participants naturally 18+ for every romantic/
    sexual/marriage interaction. Non-overridable: chronology only, checked
    at creation AND at resolution."""
    for person in (person_a, person_b):
        if person is None:
            _not_found("unknown person")
        if not is_adult(person):
            _conflict("not_adult",
                      "both participants must be 18 or older")


def assert_interaction_adults(interaction, people):
    """Content flags cannot downgrade an intrinsically adult interaction."""
    interaction = interaction or {}
    adult_kinds = set(INTERACTION_KINDS) - {"parenting_request"}
    if interaction.get("romantic") or interaction.get("kind") in adult_kinds:
        for person in people:
            assert_adult_romantic(person, person)


def _people_ids(*persons):
    return [getattr(p, "person_id", None) for p in persons]


def _pair_key(person_ids):
    return tuple(sorted(person_ids))


# ------------------------------------------------------------- validation

def _check_pair(person_a, person_b):
    a, b = _people_ids(person_a, person_b)
    if not a or not b:
        _err("invalid_input", "person ids are required")
    if a == b:
        _err("graph_error", "self-edges are not allowed")
    return a, b


def _find_edge(edges, edge_id):
    for edge in edges:
        if edge.get("relationship_id") == edge_id \
                or edge.get("union_id") == edge_id:
            return edge
    _not_found("unknown edge")


def _active_union_between(graph, a, b):
    for union in graph["unions"]:
        if union["status"] == "active" \
                and _pair_key(union["person_ids"]) == _pair_key((a, b)):
            return union
    return None


def _would_create_cycle(graph, child_id, parent_id):
    """True if adding (child<-parent) biological edge closes a cycle:
    the child is already an ANCESTOR of the parent (walking existing
    biological edges). Multi-path redundancy within a DAG stays legal."""
    stack, seen = [parent_id], set()
    while stack:
        cur = stack.pop()
        if cur == child_id:
            return True
        if cur in seen:
            continue
        seen.add(cur)
        stack.extend(_bio_parents_of(graph, cur))
    return False


# -------------------------------------------------------------- transitions

def start_relationship(graph, person_a, person_b, kind, sampled,
                       turn, visibility="public",
                       exclusivity="exclusive"):
    """Create an active relationship edge. Adult guard applies (romantic
    domain). sampled = frozen {satisfaction, trust, chemistry} already
    rolled by the caller; the graph stores, never rolls."""
    assert_adult_romantic(person_a, person_b)
    if kind not in RELATIONSHIP_KINDS:
        _err("invalid_input", "unknown relationship kind")
    a, b = _check_pair(person_a, person_b)
    for edge in graph["relationships"]:
        if edge["status"] == "active" \
                and _pair_key(edge["person_ids"]) == _pair_key((a, b)):
            _conflict("graph_error", "an active relationship already "
                                     "exists between these people")
    for field in ("satisfaction", "trust", "chemistry"):
        _bounded_int(sampled.get(field), "sampled." + field)
    if visibility not in ("public", "secret"):
        _err("invalid_input", "visibility must be public or secret")
    if exclusivity not in ("exclusive", "open", "negotiated"):
        _err("invalid_input", "unknown exclusivity agreement")
    edge = {
        "relationship_id": _new_id("rel"),
        "person_ids": [a, b],
        "kind": kind,
        "satisfaction": sampled["satisfaction"],
        "trust": sampled["trust"],
        "chemistry": sampled["chemistry"],
        "exclusivity": {"agreement": exclusivity},
        "visibility": visibility,
        "start_turn": turn,
        "end_turn": None,
        "status": "active",
    }
    graph["relationships"].append(edge)
    return edge["relationship_id"]


def update_relationship(graph, relationship_id, patch, turn):
    edge = _find_edge(graph["relationships"], relationship_id)
    for field in ("satisfaction", "trust", "chemistry"):
        if field in patch:
            _bounded_int(patch[field], field)
            edge[field] = patch[field]
    if "exclusivity" in patch:
        if patch["exclusivity"] not in ("exclusive", "open", "negotiated"):
            _err("invalid_input", "unknown exclusivity agreement")
        edge["exclusivity"] = {"agreement": patch["exclusivity"]}
    if "visibility" in patch:
        if patch["visibility"] not in ("public", "secret"):
            _err("invalid_input", "visibility must be public or secret")
        edge["visibility"] = patch["visibility"]
    if patch.get("end"):
        if edge["status"] != "active":
            _conflict("graph_error", "relationship is not active")
        edge["status"] = "ended"
        edge["end_turn"] = turn
    return edge


def end_relationship(graph, relationship_id, turn):
    return update_relationship(graph, relationship_id, {"end": True}, turn)


def form_union(graph, person_a, person_b, kind, recognition, turn,
               prenup=None, ceremony=None, policy_revision=0):
    """Form one pairwise union. Forbidden/unrecognized unions still form
    (attemptable); recognition is a label, never a gate. Adult guard."""
    assert_adult_romantic(person_a, person_b)
    if kind not in UNION_KINDS:
        _err("invalid_input", "unknown union kind")
    a, b = _check_pair(person_a, person_b)
    if _active_union_between(graph, a, b):
        _conflict("graph_error", "an active union already exists between "
                                 "these people")
    rec_status = recognition.get("status")
    if rec_status not in RECOGNITION_STATUSES:
        _err("invalid_input", "unknown recognition status")
    if recognition.get("basis") not in RECOGNITION_BASIS:
        _err("invalid_input", "unknown recognition basis")
    if not isinstance(recognition.get("jurisdiction_id"), str) \
            or not recognition["jurisdiction_id"]:
        _err("invalid_input", "jurisdiction_id is required")
    if not isinstance(recognition.get("law_version"), int) \
            or isinstance(recognition["law_version"], bool):
        _err("invalid_input", "law_version must be an integer")
    if prenup is not None:
        if not isinstance(prenup, dict) or prenup.get("asset") != "cash" \
                or not isinstance(prenup.get("amount"), int) \
                or isinstance(prenup["amount"], bool) or prenup["amount"] < 0:
            _err("invalid_input", "prenup must be a cash amount")
    edge = {
        "union_id": _new_id("uni"),
        "person_ids": [a, b],
        "kind": kind,
        "status": "active",
        "recognition": {
            "status": rec_status,
            "jurisdiction_id": recognition["jurisdiction_id"],
            "law_version": recognition["law_version"],
            "basis": recognition["basis"],
        },
        "ceremony": ceremony or {"held": False, "turn": None,
                                 "venue_id": None},
        "prenup": prenup,
        "start_turn": turn,
        "end_turn": None,
        # stamped at creation (B's policy_revision); additive to the
        # frozen union shape
        "policy_revision": policy_revision,
    }
    graph["unions"].append(edge)
    return edge["union_id"]


def end_union(graph, union_id, how, turn):
    """Terminate ONE selected union. Children, other unions and shared
    history are untouched."""
    if how not in END_UNION_HOW:
        _err("invalid_input", "how must be divorce, dissolve or widow")
    edge = _find_edge(graph["unions"], union_id)
    if edge["status"] != "active":
        _conflict("graph_error", "union is not active")
    edge["status"] = {"divorce": "divorced", "dissolve": "dissolved",
                      "widow": "widowed"}[how]
    edge["end_turn"] = turn
    return edge


def add_parentage(graph, child, parent, role, source, confidence="certain",
                  turn=None):
    child_id = getattr(child, "person_id", child)
    parent_id = getattr(parent, "person_id", parent)
    if not isinstance(child_id, str) or not isinstance(parent_id, str):
        _err("invalid_input", "child and parent ids are required")
    if child_id == parent_id:
        _err("graph_error", "a person cannot be their own parent")
    if role not in PARENT_ROLES:
        _err("invalid_input", "unknown parentage role")
    if confidence not in ("certain", "assumed"):
        _err("invalid_input", "unknown confidence")
    existing = {(e["child_id"], e["parent_id"], e["role"])
                for e in graph["parentage"]}
    if (child_id, parent_id, role) in existing:
        _conflict("graph_error", "duplicate parentage edge")
    if role == "biological":
        if _would_create_cycle(graph, child_id, parent_id):
            _err("graph_error", "biological ancestry must be acyclic")
        if turn is not None:
            # chronology: a biological parent must be older than the child
            pass
    edge = {"child_id": child_id, "parent_id": parent_id, "role": role,
            "source": source, "confidence": confidence}
    graph["parentage"].append(edge)
    return edge


def household_move(graph, person_id, household_id):
    for household in graph["households"]:
        if household["household_id"] == household_id:
            if person_id not in household["resident_ids"]:
                household["resident_ids"].append(person_id)
            return
    _not_found("unknown household")


def set_guardian(graph, child_id, guardian_ids, household_id=None):
    for household in graph["households"]:
        if household_id is None \
                or household["household_id"] == household_id:
            household.setdefault("guardians", {})[child_id] = list(
                guardian_ids)
            if child_id not in household.setdefault("dependent_ids", []):
                household["dependent_ids"].append(child_id)
            return
    _not_found("unknown household")


def add_support(graph, from_id, to_id, kind, amount, union_id=None,
                until_turn=None):
    if kind not in ("child_support", "spousal_support"):
        _err("invalid_input", "unknown support kind")
    _bounded_int(amount, "amount", 0, 10 ** 9)
    for household in graph["households"]:
        household.setdefault("support_obligations", []).append({
            "from_id": from_id, "to_id": to_id, "kind": kind,
            "amount": amount, "union_id": union_id,
            "until_turn": until_turn})
        return
    _not_found("unknown household")


# ------------------------------------------------------------------ offers

def record_offer(graph, initiator, target, interaction_kind, turn,
                 expiry_turn, policy_revision, romantic=True,
                 relationship_id=None, union_id=None, bindings=None,
                 sampled_outcomes=None):
    if interaction_kind not in INTERACTION_KINDS:
        _err("invalid_input", "unknown interaction kind")
    if romantic or interaction_kind != "parenting_request":
        assert_adult_romantic(initiator, target)
    a, b = _check_pair(initiator, target)
    offer = {
        "offer_id": _new_id("off"),
        "initiator_id": a,
        "target_id": b,
        "interaction_kind": interaction_kind,
        "romantic": bool(romantic or interaction_kind != "parenting_request"),
        "relationship_id": relationship_id,
        "union_id": union_id,
        "creation_turn": turn,
        "expiry_turn": expiry_turn,
        "policy_revision": policy_revision,
        "bindings": bindings or {},
        "sampled_outcomes": sampled_outcomes or {},
        "status": "pending",
        "attempt": {"count": 1, "cooldown_until_turn": 0},
    }
    graph["offers"].append(offer)
    return offer["offer_id"]


def _find_offer(graph, offer_id):
    for offer in graph["offers"]:
        if offer["offer_id"] == offer_id:
            return offer
    _not_found("unknown offer")


def resolve_offer(graph, offer_id, persons_by_id, accept, turn,
                  option_id=None):
    """Resolve a pending offer. Revalidates membership and the adult
    guard (participants may have aged) before any mutation. Returns the
    offer."""
    offer = _find_offer(graph, offer_id)
    if offer["status"] != "pending":
        _conflict("graph_error", "offer is not pending")
    if turn > offer["expiry_turn"]:
        offer["status"] = "expired"
        _conflict("graph_error", "offer has expired")
    if offer["romantic"] or offer["interaction_kind"] != "parenting_request":
        assert_adult_romantic(persons_by_id.get(offer["initiator_id"]),
                              persons_by_id.get(offer["target_id"]))
    offer["status"] = "accepted" if accept else "declined"
    return offer


def supersede_offers(graph, interaction_kinds=None,
                     affected_person_ids=None,
                     affected_jurisdiction=None):
    """Editor hook (B's on_policy_revision_bump): atomically supersede
    matching pending offers, preserving attempts/cooldowns/pre-effects.
    Never resamples. Returns superseded offer ids."""
    superseded = []
    for offer in graph["offers"]:
        if offer["status"] != "pending":
            continue
        if interaction_kinds and offer["interaction_kind"] not in \
                interaction_kinds:
            continue
        if affected_person_ids and not (
                {offer["initiator_id"], offer["target_id"]}
                & set(affected_person_ids)):
            continue
        if affected_jurisdiction and offer.get("jurisdiction_id") \
                not in (None, affected_jurisdiction):
            continue
        offer["status"] = "superseded"
        superseded.append(offer["offer_id"])
    return superseded


# ------------------------------------------------------------------ queries

def graph_person(person):
    return {
        "person_id": person.person_id,
        "name": person.name,
        "age": person.age,
        "is_adult": is_adult(person),
        "living": getattr(person, "alive", True),
    }


def _bio_parents_of(graph, person_id):
    return [e["parent_id"] for e in graph["parentage"]
            if e["child_id"] == person_id and e["role"] == "biological"]


def _bio_children_of(graph, person_id):
    return [e["child_id"] for e in graph["parentage"]
            if e["parent_id"] == person_id and e["role"] == "biological"]


def graph_relatedness(graph, a_id, b_id, persons_by_id=None):
    """Biological relatedness only; documented fictional bands. Unknown
    ancestry reports 'unknown' (explicit lineage:"unknown" marker on a
    person's record, passed via persons_by_id). Adoption/step/social links
    contribute nothing."""
    if a_id == b_id:
        return {"category": "self", "biological_paths": 0,
                "coefficient_band": "0"}

    def ancestors(pid, depth):
        """ancestor id -> (depth, path_count) over biological edges."""
        result = {}
        frontier = [(pid, 0)]
        while frontier:
            cur, d = frontier.pop()
            if d >= depth:
                continue
            for par in _bio_parents_of(graph, cur):
                entry = result.get(par)
                if entry is None or entry[0] > d + 1:
                    result[par] = (d + 1, 1)
                    frontier.append((par, d + 1))
                elif entry[0] == d + 1:
                    result[par] = (d + 1, entry[1] + 1)
        return result

    MAX_DEPTH = 4
    anc_a = ancestors(a_id, MAX_DEPTH)
    anc_b = ancestors(b_id, MAX_DEPTH)

    # direct line: parent/child at ancestor depth 1
    if any(b_id == pid and d == 1 for pid, (d, _) in anc_a.items()) \
            or any(a_id == pid and d == 1
                   for pid, (d, _) in anc_b.items()):
        return {"category": "parent_child", "biological_paths": 1,
                "coefficient_band": "0.25"}
    if any(b_id == pid and d == 2 for pid, (d, _) in anc_a.items()) \
            or any(a_id == pid and d == 2
                   for pid, (d, _) in anc_b.items()):
        return {"category": "grandparent_grandchild",
                "biological_paths": 1, "coefficient_band": "0.125"}
    # shared ancestors -> sibling / avuncular / cousins
    shared = set(anc_a) & set(anc_b)
    if shared:
        nearest = min(anc_a[s][0] + anc_b[s][0] for s in shared)
        if nearest == 2:
            return {"category": "sibling", "biological_paths": 1,
                    "coefficient_band": "0.25"}
        if nearest == 3:
            return {"category": "aunt_uncle_niece_nephew",
                    "biological_paths": 1, "coefficient_band": "0.125"}
        if nearest == 4:
            return {"category": "first_cousin", "biological_paths": 1,
                    "coefficient_band": "0.015-0.0625"}
        return {"category": "kin_other", "biological_paths": 1,
                "coefficient_band": "<0.015"}
    # explicit uncertainty marker wins over an assumed 'unrelated'
    if persons_by_id is not None:
        for pid in (a_id, b_id):
            rec = persons_by_id.get(pid)
            if rec is None:
                return {"category": "unknown", "biological_paths": None,
                        "coefficient_band": "unknown"}
            lineage = rec.get("lineage") if isinstance(rec, dict) else \
                getattr(rec, "lineage", None)
            if lineage == "unknown":
                return {"category": "unknown", "biological_paths": None,
                        "coefficient_band": "unknown"}
    return {"category": "unrelated", "biological_paths": 0,
            "coefficient_band": "0"}


def graph_edges(graph, person_id):
    return {
        "relationships": [e for e in graph["relationships"]
                          if person_id in e["person_ids"]],
        "unions": [e for e in graph["unions"]
                   if person_id in e["person_ids"]],
        "parentage": [e for e in graph["parentage"]
                      if person_id in (e["child_id"], e["parent_id"])],
        "households": [h for h in graph["households"]
                       if person_id in h.get("resident_ids", [])],
    }


def graph_relationship(graph, relationship_id):
    edge = _find_edge(graph["relationships"], relationship_id)
    return dict(edge)


def graph_union_memberships(graph, person_id):
    return [{"union_id": u["union_id"], "kind": u["kind"],
             "status": u["status"], "recognition": dict(u["recognition"])}
            for u in graph["unions"] if person_id in u["person_ids"]]


def graph_household(graph, person_id):
    for household in graph["households"]:
        if person_id in household.get("resident_ids", []):
            return dict(household)
    return None


def graph_tree(graph, focus_id, persons_by_id, max_generations=3,
               known_relationship_ids=None):
    """Bounded, cycle-safe tree projection matching the frozen fixture in
    graph-examples.json: the focus's ancestors and descendants within
    max_generations (via any parentage role), the other parents of every
    included child (so adoptive/step parents appear), union partners of
    included people (active/dissolved/widowed/divorced flags), secret
    relationship edges only when the viewer knows them, ex partners
    flagged. Portrait resolution stays with the M0b renderer;
    portrait_ref is always None."""
    def any_parents(pid):
        return [e["parent_id"] for e in graph["parentage"]
                if e["child_id"] == pid]

    # 1) generational walk up and down (bounded)
    seen = {focus_id}
    frontier_up, frontier_down = {focus_id}, {focus_id}
    for _ in range(max_generations):
        nxt_up = set()
        for pid in frontier_up:
            nxt_up.update(any_parents(pid))
        nxt_down = {e["child_id"] for e in graph["parentage"]
                    if e["parent_id"] in frontier_down}
        frontier_up, frontier_down = nxt_up - seen, nxt_down - seen
        seen |= nxt_up | nxt_down
    # 2) close over the other parents of every included child (adoptive,
    #    step, guardian links render) — terminates: finite person ids
    changed = True
    while changed:
        changed = False
        for e in graph["parentage"]:
            if e["child_id"] in seen and e["parent_id"] not in seen:
                seen.add(e["parent_id"])
                changed = True
    # 3) union partners of included people (any union status)
    for union in graph["unions"]:
        if any(pid in seen for pid in union["person_ids"]):
            seen.update(union["person_ids"])

    nodes = {}
    for pid in seen:
        person = persons_by_id.get(pid)
        if person is None:
            continue
        flags = []
        if not getattr(person, "alive", True):
            flags.append("deceased")
        nodes[pid] = {
            "person_id": pid,
            "name": person.name,
            "age": person.age,
            "living": getattr(person, "alive", True),
            "portrait_ref": None,
            "flags": flags,
        }

    edges = []
    for e in graph["parentage"]:
        if e["parent_id"] in nodes and e["child_id"] in nodes:
            edges.append({"from_id": e["parent_id"],
                          "to_id": e["child_id"], "kind": "parentage",
                          "flags": [e["role"]]})
    for union in graph["unions"]:
        a, b = union["person_ids"]
        if a in nodes and b in nodes:
            flag = union["status"] if union["status"] != "divorced" \
                else "dissolved"
            edges.append({"from_id": a, "to_id": b, "kind": "union",
                          "flags": [flag]})

    rel_edges = []
    ex_partners = set()
    for rel in graph["relationships"]:
        a, b = rel["person_ids"]
        # None = the default PUBLIC projection: secrets stay hidden.
        # A set = the viewing player's known relationship ids (C's
        # observer-knowledge rules decide what goes in the set).
        known_secret = rel["visibility"] == "secret" \
            and known_relationship_ids is not None \
            and rel["relationship_id"] in known_relationship_ids
        if (a not in nodes or b not in nodes) and not known_secret:
            continue
        if known_secret:
                # a known secret renders (dashed): bring its endpoints in
                for pid in (a, b):
                    if pid not in nodes:
                        person = persons_by_id.get(pid)
                        if person is not None:
                            nodes[pid] = {
                                "person_id": pid,
                                "name": person.name,
                                "age": person.age,
                                "living": getattr(person, "alive", True),
                                "portrait_ref": None,
                                "flags": []}
                rel_edges.append({"from_id": a, "to_id": b,
                                  "kind": "relationship",
                                  "flags": ["secret"]})
        elif rel["status"] == "ended":
            # ex status renders as a NODE flag; the frozen tree projection
            # does not draw a separate relationship edge for ended edges
            ex_partners.add(b if a == focus_id else a)
    edges.extend(rel_edges)

    def bio_parents(pid):
        return [e["parent_id"] for e in graph["parentage"]
                if e["child_id"] == pid and e["role"] == "biological"]

    for pid in ex_partners:
        if pid in nodes and "ex" not in nodes[pid]["flags"]:
            nodes[pid]["flags"].append("ex")
    for pid, node in nodes.items():
        if node["living"] and node["age"] < ADULT_AGE \
                and not bio_parents(pid):
            node["flags"].append("unknown_parent")
        if pid != focus_id and any(
                e["role"] == "adoptive" and e["child_id"] == pid
                for e in graph["parentage"]):
            node["flags"].append("adoptive_parent_edge")
    return {"focus_id": focus_id, "max_generations": max_generations,
            "nodes": list(nodes.values()), "edges": edges}


def sample_conception(graph, bio_mother_id, bio_father_id, rng):
    """Bounded FICTIONAL child-health adjustment, rolled ONCE per
    conception and persisted by the caller in conception_outcomes.
    rng is the caller's seeded RNG. No draw for adoption."""
    rel = graph_relatedness(graph, bio_mother_id, bio_father_id)
    band = rel["coefficient_band"]
    # fictional bands: higher relatedness -> higher (still small and
    # abstract) health-adjustment chance; calibration frozen with Claude.
    risk_by_band = {
        "0": 0.02, "<0.015": 0.03, "0.015-0.0625": 0.08, "0.125": 0.15,
        "0.25": 0.30, "unknown": 0.05,
    }
    roll = rng.random()
    adjustment = "none"
    if band in ("0.25", "0.125") and roll < risk_by_band[band]:
        adjustment = "related_parent_health_risk"
    return {"coefficient_band": band, "roll": roll,
            "adjustment": adjustment}


# -------------------------------------------------------------- migration

def migrate_v2(env):
    """Migrate an M2 single-partner save into the M2.5 graph: exactly one
    union edge from the old partner, biological parentage from RECORDED
    child mother/father (never inferred from the current partner), archive
    ex-partners as ended relationship edges, primary_relationship_id as a
    presentation projection. No invented affairs/spouses/history; person
    ids, cash, archives and pending stay untouched. Returns a fresh graph;
    on any data oddity returns a neutral empty graph (loading must never
    break)."""
    graph = new_graph()
    try:
        player = env["player"]
        partner = getattr(player, "partner", None)
        if partner is not None and getattr(partner, "person_id", None) \
                and getattr(partner, "age", 0) >= ADULT_AGE \
                and getattr(player, "age", 0) >= ADULT_AGE:
            score = int(getattr(partner, "relationship", 50) or 50)
            rel_id = start_relationship(
                graph, player, partner, "committed",
                {"satisfaction": score, "trust": score, "chemistry": 50},
                turn=0)
            graph["primary_relationship_id"] = rel_id
            # legacy marital data is inconsistent (player.marital_status is
            # a derived property); read the partner's raw status and never
            # let marriage-kind detection break migration
            try:
                status = getattr(partner, "status", None)
                married = status == 1 or status == "married"
            except Exception:
                married = False
            form_union(
                graph, player, partner,
                "marriage" if married else "civil_partnership",
                {"status": "recognized",
                 "jurisdiction_id": "jur-neutral-default",
                 "law_version": 1, "basis": "registration"},
                turn=0)
        # recorded biological parentage only
        for child in list(getattr(player, "children", []) or []):
            child_id = getattr(child, "person_id", None)
            if not child_id:
                continue
            for parent_attr in ("mother", "father"):
                parent = getattr(child, parent_attr, None)
                if parent is None:
                    continue
                parent_id = getattr(parent, "person_id", None)
                if not parent_id:
                    continue
                who = parent if hasattr(parent, "age") else \
                    _Record(parent_id)
                cid = child if hasattr(child, "age") else _Record(child_id)
                if any(e["child_id"] == child_id
                       and e["parent_id"] == parent_id
                       and e["role"] == "biological"
                       for e in graph["parentage"]):
                    continue
                add_parentage(graph, cid, who, "biological", "birth")
        # archive: ex partners / deceased relatives become ended edges
        for person in list(env.get("archive", []) or []):
            pid = getattr(person, "person_id", None)
            if not pid or pid == getattr(player, "person_id", None):
                continue
            if getattr(person, "age", 0) >= ADULT_AGE and \
                    getattr(player, "age", 0) >= ADULT_AGE:
                rid = start_relationship(
                    graph, player, person, "committed",
                    {"satisfaction": 0, "trust": 0, "chemistry": 0},
                    turn=0)
                update_relationship(graph, rid, {"end": True}, turn=0)
    except Exception:
        return new_graph()
    return graph


class _Record:
    """Minimal person-like handle (id + adult age) for migration records."""

    def __init__(self, person_id):
        self.person_id = person_id
        self.age = ADULT_AGE


def apply_death(graph, person_id, turn):
    """Death graph transition (A owns graph mutation): active
    relationships end, active unions become widowed, the person's pending
    offers are cancelled (attempts preserved). Parentage/history edges
    stay so the tree keeps showing the deceased; other partners' edges
    and completed phases are untouched. Idempotent per call."""
    ended = {"relationships": 0, "unions": 0, "offers": 0}
    for rel in graph["relationships"]:
        if person_id in rel["person_ids"] and rel["status"] == "active":
            rel["status"] = "ended"
            rel["end_turn"] = turn
            ended["relationships"] += 1
    for union in graph["unions"]:
        if person_id in union["person_ids"] and union["status"] == "active":
            union["status"] = "widowed"
            union["end_turn"] = turn
            ended["unions"] += 1
    for offer in graph["offers"]:
        if offer["status"] == "pending" \
                and person_id in (offer["initiator_id"],
                                  offer["target_id"]):
            offer["status"] = "superseded"
            ended["offers"] += 1
    return ended


def dependent_children(graph, person_id):
    """Unique dependent child ids for a person across ALL unions,
    households and parentage roles. Shared children post their aging /
    effects / support ONCE per child, never once per spouse or union
    (frozen §7). Order is stable (first-seen)."""
    ids = []
    for edge in graph["parentage"]:
        if edge["parent_id"] == person_id \
                and edge["child_id"] not in ids:
            ids.append(edge["child_id"])
    for household in graph["households"]:
        if person_id in household.get("resident_ids", []):
            for child_id in household.get("dependent_ids", []):
                if child_id not in ids:
                    ids.append(child_id)
    return ids
