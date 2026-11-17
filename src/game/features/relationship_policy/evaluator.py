"""Bounded acceptance evaluator (frozen policy-evaluator.md).

Scores acceptance for the 7 romantic interaction kinds. Two jobs only:
hard validity (never overridable) and a bounded, explainable soft score.
It never bans a legal adult consensual attempt and never manufactures
consent — refusal stays possible at any score (R3 ratified contract:
C draws ONCE per offer with `accept = u < acceptance.p_accept`;
`refusal.p_refuse` is display-only, `1 - p_accept`).

Deterministic: no RNG here. All person/edge reads go through the
injected read-only graph/policy views (A's queries at integration time),
so this module has no engine imports.
"""

from .catalog import PolicyError, resolve_jurisdiction, resolve_culture, \
    tenet_stance

ROMANTIC_KINDS = ("flirt", "date", "propose_courtship", "propose_union",
                  "propose_plural_union", "convert_request",
                  "repair_attempt")

KINSHIP_CONTRIBUTION = {
    "parent_child": -10,
    "grandparent_grandchild": -10,
    "sibling": -10,
    "aunt_uncle_niece_nephew": -8,
    "first_cousin": -6,
    "kin_other": -4,
    "unknown": -2,
    "unrelated": 0,
}

AGE_GAP_CONTRIBUTION = ((0, 9, 0), (10, 19, -5), (20, 29, -8),
                        (30, 150, -10))

LAW_RISK = {"recognized": 0, "civil_only": -4, "unrecognized": -12,
            "forbidden": -15}

STANCE_SOLO = {"favoured": 6, "neutral": 0, "disfavoured": -6}
STANCE_PERSONAL = {"favoured": 8, "neutral": 0, "disfavoured": -8}
STANCE_HALF = {"favoured": 3, "neutral": 0, "disfavoured": -3}
SOCIAL_STANCE = {"favoured": 4, "neutral": 0, "disfavoured": -4}

REASON_THRESHOLD = 3  # |contribution| >= 3 to surface a reason


def _clamp(value, lo, hi):
    return max(lo, min(hi, value))


def _reason(code, value, reasons):
    """Record a user-facing reason when the contribution is big enough
    (frozen reason disclosure rule)."""
    if abs(value) >= REASON_THRESHOLD:
        reasons.append({"code": code, "contribution": value,
                        "label_key": "reason.%s" % code})


class GraphView:
    """Read-only graph surface the evaluator needs (A's §5 queries at
    integration; tests inject a fake). Methods raise PolicyError with a
    stable code on dangling ids."""

    def person(self, person_id):  # pragma: no cover - interface
        raise NotImplementedError

    def relatedness_category(self, a_id, b_id):  # pragma: no cover
        raise NotImplementedError

    def relationship_scores(self, a_id, b_id):  # pragma: no cover
        raise NotImplementedError

    def active_union_count(self, person_id):  # pragma: no cover
        raise NotImplementedError


class PolicyView:
    """Read-only policy surface (B's own records; the service adapter
    fills these). `profile()` returns the person's frozen
    attraction_profile plus optional 'gender'; `catalog`/`overrides`
    are the loaded catalog and the save-local override block."""

    catalog = None
    overrides = None

    def profile(self, person_id):  # pragma: no cover - interface
        raise NotImplementedError

    def jurisdiction_id(self):  # pragma: no cover
        raise NotImplementedError


class StaticPolicyView(PolicyView):
    """Simple dict-backed view for tests and the service adapter."""

    def __init__(self, catalog, profiles, jurisdiction_id,
                 overrides=None):
        self.catalog = catalog
        self.overrides = overrides or {}
        self._profiles = profiles
        self._jurisdiction_id = jurisdiction_id

    def profile(self, person_id):
        try:
            return self._profiles[person_id]
        except KeyError:
            raise PolicyError("unknown_person",
                              "no policy profile for %r" % person_id)

    def jurisdiction_id(self):
        return self._jurisdiction_id


def _hard_fail(code):
    return {"hard": {"ok": False, "code": code}, "score": None,
            "score_breakdown": None, "reasons": [],
            "acceptance": {"possible": False},
            "refusal": {"possible": False},
            "projected_recognition": None,
            "projected_consequences": [], "policy_revision": 0}


def _proposed_union_type(interaction_kind):
    if interaction_kind == "propose_plural_union":
        return "plural_civil"
    return "monogamous_civil"


def _faiths_differ(i_profile, t_profile):
    return i_profile.get("faith_id") != t_profile.get("faith_id")


def _gender(graph, policy, person, person_id):
    return person.get("gender") or policy.profile(person_id).get("gender")


def _relevant_tenets(interaction_kind, i_profile, t_profile, i_gender,
                     t_gender, gap_years):
    if interaction_kind in ("flirt", "date", "propose_courtship",
                            "repair_attempt"):
        return []
    relevant = []
    if interaction_kind == "propose_plural_union":
        relevant.append("plural_unions")
    if interaction_kind in ("propose_union", "propose_plural_union"):
        if _faiths_differ(i_profile, t_profile):
            relevant.append("interfaith_unions")
        if i_gender and i_gender == t_gender:
            relevant.append("same_gender_unions")
        if gap_years >= 10:
            relevant.append("large_age_gap")
    return relevant


def evaluate_offer(interaction_kind, initiator_id, target_id, graph,
                   policy, policy_revision=0):
    """Score one offer. `graph`/`policy` are the read-only views above.
    Returns the frozen EvaluatorResult dict (policy-evaluator.md §5).
    The caller (C's offer creation) owns accept/refuse sampling; this
    function is pure."""
    if interaction_kind not in ROMANTIC_KINDS:
        raise PolicyError("invalid_value",
                          "unknown interaction_kind %r" % interaction_kind)
    if initiator_id == target_id:
        return _hard_fail("self_target")

    try:
        initiator = graph.person(initiator_id)
        target = graph.person(target_id)
        t_profile = policy.profile(target_id)
        i_profile = policy.profile(initiator_id)
    except PolicyError as exc:
        return _hard_fail(exc.code)

    if not initiator.get("living", False) or not target.get("living", False):
        return _hard_fail("dead")
    if not initiator.get("is_adult", False) or \
            not target.get("is_adult", False):
        # Mirrors A's centralized guard, which is authoritative; this
        # rejection can never be waived by score, tenet, law or edit.
        return _hard_fail("not_adult")

    reasons = []
    contributions = {}

    # attraction / chemistry / trust
    attraction = float(t_profile.get("attraction_to_initiator", 0.5))
    value = _clamp(round((attraction - 0.5) * 30), -15, 15)
    contributions["attraction"] = value
    _reason("attraction", value, reasons)
    scores = graph.relationship_scores(initiator_id, target_id)
    for key in ("chemistry", "trust"):
        value = _clamp(round((float(scores.get(key, 50)) - 50) / 5),
                       -10, 10)
        contributions[key] = value
        _reason(key, value, reasons)

    # tenets: personal override gives full personal weight AND halves the
    # public tenet; no override gives the public tenet at full weight.
    gap_years = abs(int(initiator.get("age", 0)) - int(target.get("age", 0)))
    i_gender = _gender(graph, policy, initiator, initiator_id)
    t_gender = _gender(graph, policy, target, target_id)
    relevant = _relevant_tenets(interaction_kind, i_profile, t_profile,
                                i_gender, t_gender, gap_years)
    faith_sum = 0
    personal_sum = 0
    t_overrides = t_profile.get("personal_tenet_overrides") or {}
    for tenet_id in relevant:
        if tenet_id in t_overrides:
            personal_sum += STANCE_PERSONAL[t_overrides[tenet_id]]
            public, _ = tenet_stance(policy.catalog, policy.overrides,
                                     t_profile.get("faith_id"), tenet_id,
                                     None)
            faith_sum += STANCE_HALF[public]
        else:
            public, _ = tenet_stance(policy.catalog, policy.overrides,
                                     t_profile.get("faith_id"), tenet_id,
                                     None)
            faith_sum += STANCE_SOLO[public]
    contributions["personal_tenets"] = _clamp(personal_sum, -10, 10)
    _reason("personal_tenets", contributions["personal_tenets"], reasons)
    contributions["faith_tenets"] = _clamp(faith_sum, -10, 10)
    _reason("faith_tenets", contributions["faith_tenets"], reasons)

    # culture (relevant social stances only)
    culture = resolve_culture(policy.catalog, policy.overrides,
                              t_profile.get("culture_id"))
    cmods = culture.get("modifiers", {})
    culture_sum = 0
    if "plural_unions" in relevant:
        culture_sum += SOCIAL_STANCE.get(
            cmods.get("plural_unions_social_stance", "neutral"), 0)
    if "interfaith_unions" in relevant and _faiths_differ(i_profile,
                                                          t_profile):
        culture_sum += SOCIAL_STANCE.get(
            cmods.get("interfaith_social_stance", "neutral"), 0)
    contributions["culture"] = _clamp(culture_sum, -8, 8)
    _reason("culture", contributions["culture"], reasons)

    # projected recognition + law risk (consequences, never eligibility)
    projected = None
    consequences = []
    if interaction_kind in ("propose_union", "propose_plural_union"):
        jurisdiction = resolve_jurisdiction(policy.catalog,
                                            policy.overrides,
                                            policy.jurisdiction_id())
        union_type = _proposed_union_type(interaction_kind)
        status = jurisdiction.get("recognition", {}).get(
            union_type, "recognized")
        projected = {"status": status,
                     "jurisdiction_id": jurisdiction["id"],
                     "law_version": int(jurisdiction.get("law_version", 0)),
                     "basis": "ceremony"}
        if status in ("unrecognized", "forbidden"):
            for cons in jurisdiction.get("consequences", []):
                if cons.get("trigger") == "ceremony_of_unrecognized_union":
                    lo, hi = cons.get("amount", {}).get("randint", [0, 0])
                    consequences.append({
                        "trigger": cons["trigger"],
                        "effect": cons["effect"],
                        "ledger_category": cons.get("ledger_category",
                                                     "legal"),
                        "amount_range": [lo, hi]})
    law_value = LAW_RISK.get(projected["status"], 0) if projected else 0
    contributions["law_risk"] = law_value
    _reason("law_risk", law_value, reasons)

    # reputation (scoped, as known to the target; C filters display)
    rep = 0
    reputation_of = getattr(graph, "reputation_of", None)
    if reputation_of is not None:
        rep = reputation_of(initiator_id, observer_id=target_id)
    value = _clamp(round(rep / 10), -10, 10)
    contributions["reputation"] = value
    _reason("reputation", value, reasons)
    if abs(value) >= REASON_THRESHOLD:
        fact_source = getattr(graph, "reputation_fact_id", None)
        reasons[-1]["fact_id"] = (fact_source(initiator_id, target_id)
                                  if fact_source else None)

    # class: M3 placeholder (frozen bound, no source yet)
    contributions["class"] = 0

    # adult age gap
    gap_contribution = 0
    for lo, hi, value in AGE_GAP_CONTRIBUTION:
        if lo <= gap_years <= hi:
            gap_contribution = value
            break
    contributions["adult_age_gap"] = gap_contribution
    _reason("adult_age_gap", gap_contribution, reasons)

    # existing commitments
    unions = graph.active_union_count(target_id)
    value = -round(8 * float(t_profile.get("monogamy_preference", 0.5))) \
        if unions else 0
    contributions["existing_commitments"] = value
    _reason("existing_commitments", value, reasons)

    # kinship (social acceptance only; A's conception model is separate)
    category = graph.relatedness_category(initiator_id, target_id)
    value = KINSHIP_CONTRIBUTION.get(category, 0)
    contributions["kinship"] = value
    _reason("kinship", value, reasons)

    # convert request
    if interaction_kind == "convert_request":
        stance, source = tenet_stance(
            policy.catalog, policy.overrides, t_profile.get("faith_id"),
            "conversion_out", t_overrides or None)
        value = 0 if stance != "disfavoured" else (
            -3 if source == "personal" else -6)
        contributions["convert_request"] = value
        _reason("convert_request", value, reasons)
    else:
        contributions["convert_request"] = 0

    score = _clamp(50 + sum(contributions.values()), 0, 100)

    # R3 ratified probability contract (Claude, M2.5-fix-round-2.md):
    # ONE sampled-once draw per offer: accept = (u < p_accept). The
    # refusal traits (openness, jealousy) live INSIDE p_accept; p_refuse
    # is derived for display only. p_accept is clamped to [0.02, 0.98]
    # so refusal is always possible and acceptance is never impossible
    # for an eligible adult pair (soft modifiers are never bans).
    openness = float(t_profile.get("openness", 0.5))
    jealousy = float(t_profile.get("jealousy", 0.4))
    trait_drag = (0.4 * (1 - openness) + 0.2 * jealousy) / 2.0
    p_accept = _clamp(round(score / 100.0 - trait_drag, 4), 0.02, 0.98)
    p_refuse = round(1.0 - p_accept, 2)

    return {"hard": {"ok": True, "code": None},
            "score": score,
            "score_breakdown": dict(contributions, base=50),
            "reasons": reasons,
            "acceptance": {"possible": True, "p_accept": p_accept},
            "refusal": {"possible": True, "p_refuse": p_refuse},
            "projected_recognition": projected,
            "projected_consequences": consequences,
            "policy_revision": int(policy_revision)}


# ---- read-only predicates exposed to C's v3 conditions (frozen names) --

def predicate_tenet_stance(catalog, overrides, faith_id, tenet_id,
                           personal_overrides=None):
    stance, _ = tenet_stance(catalog, overrides, faith_id, tenet_id,
                             personal_overrides)
    return stance


def predicate_faith_id(profile):
    return profile.get("faith_id")


def predicate_jurisdiction_id(policy_view):
    return policy_view.jurisdiction_id()


def predicate_recognition_status(union_edge):
    return (union_edge or {}).get("recognition", {}).get("status")


def predicate_attraction_band(profile):
    return _band(float(profile.get("attraction_to_initiator", 0.5)))


def predicate_openness_band(profile):
    return _band(float(profile.get("openness", 0.5)))


def predicate_monogamy_band(profile):
    return _band(float(profile.get("monogamy_preference", 0.5)))


def _band(value):
    if value < 0.34:
        return "low"
    if value < 0.67:
        return "mid"
    return "high"
