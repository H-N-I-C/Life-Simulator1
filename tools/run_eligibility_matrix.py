#!/usr/bin/env python3
"""Run the M2.5 eligibility-matrix fixtures (tests/content/eligibility_matrix.json).

Post-freeze behavior (2026-11-17, D1): the 18+/kinship/law matrix RUNS —
no SKIP — against B's frozen evaluator (policy-fixtures.json
evaluator_entry) and A's REAL family_graph:

1. ALWAYS: validate fixture well-formedness, the fixed 18+ rule
   (independent of any evaluator), and fixture reason codes against B's
   published reason_code_vocabulary (policy-fixtures.json). Shape errors
   exit 2.
2. Execute B's own fixtures cross-contract (deterministic hard/score/
   breakdown parts per B's _status note) with views built from each
   fixture's context, mirroring B's tests/test_relationship_policy.py.
3. Execute every matrix case against the real evaluator with:
   - a REAL A graph (src/game/features/family_graph/graph.py): kinship
     relations are encoded as actual parentage/union edges so
     relatedness_category comes from graph_relatedness — the A<->B
     integration, not a stubbed category;
   - a StaticPolicyView over the real catalog, with the case's law/tenets
     mapped to jurisdiction/faith overrides (prohibited_unrecognized ->
     unrecognized recognition + ceremony consequence, per the frozen
     protocol: consequences, never eligibility).

Profile-field mapping (inline fixture -> evaluator profile):
- target.attraction min|max -> attraction_to_initiator 0.0|1.0 (else 0.5)
- target.openness min|max     -> openness 0.0|1.0 (p_refuse input only)
- monogamy_preference 0.5 (culture_meridian defaults); prior_partners is
  history, not an active union: existing_commitments must come from the
  case's unions, none of which the fixtures declare, so it contributes 0.

The evaluator hook lookup stays dynamic: this file runs unmodified
against the frozen evaluator; a published module path (foo/bar.py) is
resolved to an importable dotted name.
"""

import importlib
import json
import os
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

MATRIX = os.path.join(ROOT, "tests", "content", "eligibility_matrix.json")
CONTRACTS = os.path.join(ROOT, "docs", "collab", "M2.5", "contracts")

GENDERS = ("male", "female", "unknown")  # "unknown" only for
# dangling-ID graph-error cases; the evaluator must reject those
RELATIONS = {"unrelated", "biological_sibling", "half_sibling", "step",
             "adoptive", "in_law", "first_cousin", "kin_other",
             "parent_child", "grandparent_grandchild",
             "self", "missing"}
LAWS = {"neutral", "permissive", "prohibited_unrecognized"}
TENETS = {"neutral", "permissive", "hostile"}
GATES = {"both_pass", "blocked"}
HARD_CODES = {"not_adult", "dead", "self_target", "unknown_person",
              "duplicate_member", "graph_error"}

MATRIX_JURISDICTION = "jur_matrix"
MATRIX_FAITH = "faith_matrix"
RELEVANT_TENETS = ("plural_unions", "interfaith_unions",
                   "same_gender_unions", "large_age_gap")
UNION_TYPES = ("monogamous_civil", "plural_civil", "religious_only",
               "customary", "private")
BASE_JURISDICTION = "jur_metropolis_charter"  # all recognized, no
# consequences (the catalog's neutral-law preset)


def load_contract(name):
    path = os.path.join(CONTRACTS, name)
    if not os.path.isfile(path):
        return None
    try:
        with open(path, encoding="utf-8") as f:
            return json.load(f)
    except (OSError, ValueError):
        return None


def evaluator_hook():
    data = load_contract("policy-fixtures.json")
    if not data:
        return None
    hook = data.get("evaluator_entry")
    if isinstance(hook, dict) and hook.get("module") \
            and (hook.get("function") or hook.get("callable")):
        return hook["module"], hook.get("function") or hook.get("callable")
    return None


def import_hook(hook):
    """Resolve the published hook to a callable. A published MODULE may
    be a repo-relative .py path (frozen contract style) or a dotted
    name; both must reach the same real evaluator."""
    module_name, function = hook
    candidates = [module_name]
    if module_name.endswith(".py"):
        inner = module_name[:-3].replace("/", ".").replace(os.sep, ".")
        candidates = [inner.replace("..", "."), module_name]
    for name in candidates:
        try:
            module = importlib.import_module(name)
        except ImportError:
            continue
        return getattr(module, function)
    return None


def reason_vocabulary():
    data = load_contract("policy-fixtures.json") or {}
    vocab = data.get("reason_code_vocabulary") or {}
    return set(vocab.get("soft") or []) | set(vocab.get("hard") or [])


# ---------------------------------------------------------------------
# view construction

class MatrixGraphView:
    """evaluator.GraphView backed by a REAL A graph (graph_relatedness
    for kinship) plus plain person dicts. `missing_ids` are registered
    nowhere, so person() raises PolicyError('unknown_person') for them —
    the dangling-id graph-error cases."""

    def __init__(self, graph, persons):
        self._graph = graph
        self._persons = persons

    def person(self, person_id):
        try:
            return self._persons[person_id]
        except KeyError:
            from src.game.features.relationship_policy.catalog import \
                PolicyError
            raise PolicyError("unknown_person",
                              "unknown person %r" % (person_id,))

    def relatedness_category(self, a_id, b_id):
        from src.game.features.family_graph import graph as graph_mod
        self.person(a_id)
        self.person(b_id)
        return graph_mod.graph_relatedness(self._graph, a_id, b_id)[
            "category"]

    def relationship_scores(self, a_id, b_id):
        # mirror ServiceGraphView: a pair with no ACTIVE relationship
        # reads 0 chemistry/trust
        self.person(a_id)
        self.person(b_id)
        for rel in self._graph["relationships"]:
            if set(rel["person_ids"]) == {a_id, b_id} \
                    and rel["status"] == "active":
                return {"chemistry": rel["chemistry"],
                        "trust": rel["trust"],
                        "satisfaction": rel["satisfaction"]}
        return {"chemistry": 0, "trust": 0, "satisfaction": 0}

    def active_union_count(self, person_id):
        self.person(person_id)
        return sum(1 for u in self._graph["unions"]
                   if person_id in u["person_ids"]
                   and u["status"] == "active")


class DictGraph:
    """Dict-backed GraphView for B's own fixtures, whose context names
    kinship/ages directly (mirrors B's tests/test_relationship_policy)."""

    def __init__(self, persons, relatedness="unrelated", scores=None,
                 target_unions=0):
        self._persons = persons
        self._relatedness = relatedness
        self._scores = scores or {}
        self._target_unions = target_unions

    def person(self, person_id):
        try:
            return self._persons[person_id]
        except KeyError:
            from src.game.features.relationship_policy.catalog import \
                PolicyError
            raise PolicyError("unknown_person", person_id)

    def relatedness_category(self, a_id, b_id):
        return self._relatedness

    def relationship_scores(self, a_id, b_id):
        return self._scores

    def active_union_count(self, person_id):
        return self._target_unions


def load_evaluator_modules():
    from src.game.features.relationship_policy import catalog, evaluator
    from src.game.features.family_graph import graph as graph_mod
    return catalog, evaluator, graph_mod


def _person_record(pid, age, gender):
    return {"person_id": pid, "name": pid, "age": age,
            "is_adult": age >= 18, "living": True, "gender": gender}


def _profile(target, faith_id):
    def opt(key):
        value = target.get(key)
        return 0.0 if value == "min" else 1.0 if value == "max" else None
    attraction = opt("attraction")
    openness = opt("openness")
    profile = {"attraction_to_initiator": 0.5 if attraction is None
               else attraction,
               "openness": 0.5 if openness is None else openness,
               "monogamy_preference": 0.5,
               "culture_id": "culture_meridian",
               "faith_id": faith_id}
    return profile


def _policy_overrides(law, tenets):
    overrides = {}
    if law == "prohibited_unrecognized":
        overrides["jurisdictions"] = {
            MATRIX_JURISDICTION: {
                "law_version": 1,
                "recognition": {t: "unrecognized" for t in UNION_TYPES},
                "consequences": [{
                    "trigger": "ceremony_of_unrecognized_union",
                    "effect": "civil_fine",
                    "ledger_category": "legal",
                    "amount": {"randint": [500, 2000]},
                }],
            }}
        jurisdiction = MATRIX_JURISDICTION
    else:
        jurisdiction = BASE_JURISDICTION
    if tenets in ("permissive", "hostile"):
        stance = "favoured" if tenets == "permissive" else "disfavoured"
        overrides["faiths"] = {
            MATRIX_FAITH: {"tenets": [
                {"id": tenet_id, "stance": stance}
                for tenet_id in RELEVANT_TENETS]}}
        faith_id = MATRIX_FAITH
    else:
        faith_id = "faith_unaffiliated"
    return jurisdiction, faith_id, overrides


# Kinship encodings on a real A graph. Each builder may add helper
# persons (shared parents etc.); every helper is an adult. Graph
# transitions read person AGES off objects (chronology guard), so
# helpers are attribute-stand-ins, never bare id strings.
class _FixturePerson:
    def __init__(self, pid, age):
        self.person_id = pid
        self.age = age
        self.alive = True


def _bio(graph_mod, graph, child, parent):
    graph_mod.add_parentage(graph, child, parent, "biological",
                            source="matrix_fixture")


def _encode_relation(graph_mod, graph, actor_id, target_id, relation,
                     persons, counter):
    """Add real edges encoding `relation` of target relative to actor.
    Social-only links (step/adoptive/in_law) deliberately produce no
    biological relatedness — that is the frozen semantics under test."""

    def helper(age=55):
        # Chronology: every helper must be plausibly older than the
        # actor/target adults (26-45 in the fixtures) and younger than
        # their own parents (see first_cousin below).
        counter[0] += 1
        pid = "helper_%d" % counter[0]
        persons[pid] = _person_record(pid, age, "male")
        return _FixturePerson(pid, age)

    if relation in ("unrelated", "missing", "self"):
        return
    if relation == "biological_sibling":
        p1, p2 = helper(), helper()
        _bio(graph_mod, graph, actor_id, p1)
        _bio(graph_mod, graph, actor_id, p2)
        _bio(graph_mod, graph, target_id, p1)
        _bio(graph_mod, graph, target_id, p2)
    elif relation == "half_sibling":
        p1 = helper()
        _bio(graph_mod, graph, actor_id, p1)
        _bio(graph_mod, graph, target_id, p1)
    elif relation == "step":
        graph_mod.add_parentage(graph, actor_id, target_id, "step",
                                source="matrix_fixture")
    elif relation == "adoptive":
        graph_mod.add_parentage(graph, actor_id, target_id, "adoptive",
                                source="matrix_fixture")
    elif relation == "in_law":
        # both married to the same third party: a social link only; A's
        # biological relatedness must report 'unrelated' (contribution 0)
        third = helper()
        for pid in (actor_id, target_id):
            graph_mod.form_union(
                graph, _FixturePerson(pid, persons[pid]["age"]), third,
                "marriage",
                {"status": "recognized",
                 "jurisdiction_id": BASE_JURISDICTION,
                 "law_version": 0, "basis": "ceremony"}, turn=1)
    elif relation == "first_cousin":
        # grandparents (80) -> parents (55, siblings) -> actor/target;
        # a strict chronology: no helper is the same age as its child.
        gp1, gp2 = helper(80), helper(80)
        pa, pt = helper(55), helper(55)
        _bio(graph_mod, graph, pa, gp1)
        _bio(graph_mod, graph, pa, gp2)
        _bio(graph_mod, graph, pt, gp1)
        _bio(graph_mod, graph, pt, gp2)
        _bio(graph_mod, graph, actor_id, pa)
        _bio(graph_mod, graph, target_id, pt)
    elif relation in ("parent_child", "grandparent_grandchild"):
        _bio(graph_mod, graph, actor_id, target_id)
        if relation == "grandparent_grandchild":
            # middle generation sits between actor and the target
            # grandparent's fixture age
            t_age = persons[target_id]["age"]
            middle = helper(max(19, min(60, t_age - 25)))
            graph_mod.add_parentage(graph, actor_id, middle, "biological",
                                    source="matrix_fixture",
                                    confidence="assumed")
            graph_mod.add_parentage(graph, middle, target_id, "biological",
                                    source="matrix_fixture",
                                    confidence="assumed")
    else:  # pragma: no cover - validate_case rejects unknown relations
        raise ValueError("unhandled relation %r" % relation)


def _case_ages(case):
    """Ages of participants actually present in the graph. A 'missing'
    target's placeholder age (0) is not a real minor — the case asserts
    the dangling-id error instead."""
    ages = [case["actor"]["age"]]
    if case["target"].get("relation") != "missing":
        ages.append(case["target"]["age"])
    return ages


def build_matrix_case(catalog, evaluator, graph_mod, case):
    """One matrix case -> (evaluate kwargs). Real graph, real catalog,
    per-case policy overrides."""
    actor = case["actor"]
    target = case["target"]
    kind = case.get("interaction_kind") or "date"
    law = (case.get("policy") or {}).get("law", "neutral")
    tenets = (case.get("policy") or {}).get("tenets", "neutral")

    graph = graph_mod.new_graph()
    persons = {}
    counter = [0]
    actor_id = "actor"
    target_id = "target"
    persons[actor_id] = _person_record(actor_id, actor["age"],
                                       actor.get("gender", "female"))
    relation = target.get("relation", "unrelated")
    if relation == "self":
        target_id = actor_id  # self-target: same person id
    elif relation != "missing":
        persons[target_id] = _person_record(
            target_id, target["age"], target.get("gender", "male"))
    if relation != "self":
        _encode_relation(graph_mod, graph, actor_id, target_id,
                         relation, persons, counter)

    jurisdiction, faith_id, overrides = _policy_overrides(law, tenets)
    profiles = {actor_id: _profile(actor, faith_id),
                target_id: _profile(target, faith_id)}
    policy = evaluator.StaticPolicyView(catalog.load_catalog(), profiles,
                                        jurisdiction, overrides=overrides)
    graph_view = MatrixGraphView(graph, persons)
    return evaluator.evaluate_offer(kind, actor_id, target_id,
                                    graph_view, policy)


# ---------------------------------------------------------------------

def validate_case(case, seen_ids, vocab):
    errs = []
    cid = case.get("id")
    if not isinstance(cid, str) or not cid:
        errs.append("missing id")
    elif cid in seen_ids:
        errs.append("duplicate id %r" % cid)
    seen_ids.add(cid)
    if case.get("kind") not in ("hard_validity", "soft_score"):
        errs.append("kind must be hard_validity|soft_score")
    for who in ("actor", "target"):
        p = case.get(who)
        if not isinstance(p, dict):
            errs.append("%s must be an object" % who)
            continue
        age = p.get("age")
        if not isinstance(age, int) or isinstance(age, bool) \
                or not 0 <= age <= 130:
            errs.append("%s.age must be an int in 0..130, got %r"
                        % (who, age))
        if p.get("gender") not in GENDERS:
            errs.append("%s.gender must be male|female" % who)
        if p.get("relation", "unrelated") not in RELATIONS:
            errs.append("%s.relation unknown: %r"
                        % (who, p.get("relation")))
        for opt in ("attraction", "openness"):
            if opt in p and p[opt] not in ("min", "max"):
                errs.append("%s.%s must be min|max" % (who, opt))
        if "prior_partners" in p and (
                not isinstance(p["prior_partners"], int)
                or p["prior_partners"] < 0):
            errs.append("%s.prior_partners must be a non-negative int" % who)
    policy = case.get("policy") or {}
    if policy.get("law") not in LAWS:
        errs.append("policy.law unknown: %r" % policy.get("law"))
    if policy.get("tenets") not in TENETS:
        errs.append("policy.tenets unknown: %r" % policy.get("tenets"))
    expect = case.get("expect") or {}
    if expect.get("adult_gate") not in GATES:
        errs.append("expect.adult_gate must be both_pass|blocked")
    for key in ("attemptable", "refusal_possible"):
        if not isinstance(expect.get(key), bool):
            errs.append("expect.%s must be a bool" % key)
    for key in ("reason_codes",):
        rc = expect.get(key)
        if rc is not None and (not isinstance(rc, list)
                               or not all(isinstance(x, str) for x in rc)):
            errs.append("expect.%s must be a list of strings" % key)
        elif vocab and rc:
            unknown = set(rc) - vocab
            if unknown:
                errs.append("expect.%s uses codes outside B's published "
                            "vocabulary: %s" % (key, sorted(unknown)))
    ui = expect.get("ui")
    if ui is not None:
        if not isinstance(ui, dict):
            errs.append("expect.ui must be an object")
        else:
            for key in ("projected_consequences_present",
                        "amount_range_ordered"):
                if key in ui and not isinstance(ui[key], bool):
                    errs.append("expect.ui.%s must be a bool" % key)
            ri = ui.get("reasons_include")
            if ri is not None:
                if not isinstance(ri, list) or \
                        not all(isinstance(x, str) for x in ri):
                    errs.append("expect.ui.reasons_include must be a list "
                                "of strings")
                elif vocab:
                    unknown = set(ri) - vocab
                    if unknown:
                        errs.append("expect.ui.reasons_include uses codes "
                                    "outside B's vocabulary: %s"
                                    % sorted(unknown))
    # fixed-rule sanity, independent of any evaluator:
    minor = any(isinstance(case.get(w, {}).get("age"), int)
                and case[w]["age"] < 18 for w in ("actor", "target"))
    if minor and expect.get("adult_gate") != "blocked":
        errs.append("FIXED RULE: a case with a minor must expect "
                    "adult_gate=blocked")
    if minor and expect.get("attemptable"):
        errs.append("FIXED RULE: a minor case cannot be attemptable")
    if not minor and case.get("kind") == "soft_score" \
            and not expect.get("refusal_possible"):
        errs.append("FIXED RULE: refusal must remain possible for adults")
    if expect.get("reason_codes") and "not_adult" in expect["reason_codes"] \
            and not minor:
        errs.append("not_adult reason without a minor participant")
    return errs


def check_result(case, result):
    """Compare an evaluator result (policy-evaluator.md §5 shape) with a
    matrix case's expectations, including the UI projection classes.

    adult_gate is the AGE gate only (frozen protocol): for adults it is
    both_pass even when a non-age hard error (self_target/unknown_person)
    blocks the attempt — those assert hard.code + attemptable instead.
    """
    problems = []
    expect = case["expect"]
    hard = result.get("hard") or {}
    minor = any(age < 18 for age in _case_ages(case))
    if minor:
        if hard.get("ok") or hard.get("code") != "not_adult":
            problems.append("adult_gate: a minor must hard-fail not_adult, "
                            "got ok=%s code=%s" % (hard.get("ok"),
                                                   hard.get("code")))
    expected_hard = set(expect.get("reason_codes") or []) & HARD_CODES
    if expected_hard:
        if hard.get("code") not in expected_hard:
            problems.append("hard.code: expected one of %s, got %r"
                            % (sorted(expected_hard), hard.get("code")))
    elif not minor and not hard.get("ok"):
        problems.append("hard.ok: expected a pass for adults, got "
                        "code %s" % hard.get("code"))
    if bool(hard.get("ok")) != bool(expect["attemptable"]):
        problems.append("attemptable: expected %s, hard.ok=%s (code %s)"
                        % (expect["attemptable"], hard.get("ok"),
                           hard.get("code")))
    refusal = result.get("refusal") or {}
    if bool(refusal.get("possible")) != expect["refusal_possible"]:
        problems.append("refusal_possible: expected %s, got %s"
                        % (expect["refusal_possible"],
                           refusal.get("possible")))
    reasons = {r.get("code") for r in result.get("reasons") or []
               if isinstance(r, dict)}
    expected_soft = set(expect.get("reason_codes") or []) - HARD_CODES
    if expected_soft - reasons:
        problems.append("reasons missing %s (evaluator: %s)"
                        % (sorted(expected_soft - reasons),
                           sorted(reasons)))
    ui = expect.get("ui") or {}
    if ui.get("projected_consequences_present"):
        cons = result.get("projected_consequences")
        if not cons:
            problems.append("projected_consequences empty but UI expects "
                            "a cost preview")
        elif ui.get("amount_range_ordered"):
            for c in cons:
                rng = c.get("amount_range")
                if not (isinstance(rng, list) and len(rng) == 2
                        and all(isinstance(v, int) for v in rng)
                        and 0 <= rng[0] <= rng[1]):
                    problems.append("bad amount_range %r in %s"
                                    % (rng, c.get("effect")))
    for code in ui.get("reasons_include") or []:
        if code not in reasons:
            problems.append("ui.reasons_include %r not in evaluator "
                            "reasons %s" % (code, sorted(reasons)))
    return problems


def run_b_variant_fixture(evaluate, fx, personas):
    """A b-fixture with `cases` (input_overrides x expected_hard_code):
    one shared base input, each variant overriding one fact. Mirrors B's
    tests/test_relationship_policy.py::test_hard_validity_rejections."""
    name = fx.get("name", "?")
    base = {"interaction_kind": "propose_union",
            "initiator": "initiator_generic",
            "target": "target_riverlands_faithful",
            "context": {"relationship": {"chemistry": 50, "trust": 50},
                        "existing_unions_target": [],
                        "kinship_category": "none",
                        "age_gap_years": 2,
                        "jurisdiction": BASE_JURISDICTION,
                        "reputation": 0}}
    failures = 0
    for case in fx["cases"]:
        label = json.dumps(case.get("input_overrides"))
        inp = json.loads(json.dumps(base))
        persons, profiles = {}, {}
        i_id = "initiator_generic"
        persons[i_id] = {"person_id": i_id, "age": 30, "is_adult": True,
                         "living": True, "gender": "male"}
        profiles[i_id] = {"attraction_to_initiator": 0.5, "openness": 0.5,
                          "monogamy_preference": 0.5}
        t_id = "target_riverlands_faithful"
        t_age, t_living = 32, True
        for key, value in (case.get("input_overrides") or {}).items():
            if key == "target_age":
                t_age = value
            elif key == "initiator_age":
                persons[i_id]["age"] = value
                persons[i_id]["is_adult"] = value >= 18
            elif key == "target_dead":
                t_living = False
            elif key == "target_id_same_as_initiator":
                t_id = i_id
            elif key == "target_id":
                t_id = value
        if t_id != i_id and t_id in personas:
            persons[t_id] = {"person_id": t_id, "age": t_age,
                             "is_adult": t_age >= 18, "living": t_living,
                             "gender": "female"}
            profiles[t_id] = {"attraction_to_initiator": 0.5,
                              "openness": 0.5, "monogamy_preference": 0.5}
        elif not t_living and t_id in persons:
            persons[t_id]["living"] = False
        graph = DictGraph(persons, relatedness="unrelated",
                          scores=inp["context"]["relationship"],
                          target_unions=0)
        from src.game.features.relationship_policy import catalog \
            as catalog_mod, evaluator as evaluator_mod
        policy = evaluator_mod.StaticPolicyView(
            catalog_mod.load_catalog(), profiles, BASE_JURISDICTION)
        try:
            result = evaluate(inp["interaction_kind"], i_id, t_id, graph,
                              policy)
        except Exception as e:
            failures += 1
            print("FAIL b-fixture/%s[%s]: raised %r" % (name, label, e))
            continue
        want = case.get("expected_hard_code")
        hard = result.get("hard") or {}
        problems = []
        if hard.get("ok") or hard.get("code") != want:
            problems.append("hard expected code %s, got ok=%s code=%s"
                            % (want, hard.get("ok"), hard.get("code")))
        if result.get("score") is not None:
            problems.append("score must be None on hard failure, got %r"
                            % result.get("score"))
        if (result.get("refusal") or {}).get("possible"):
            problems.append("refusal must be impossible on hard failure")
        if problems:
            failures += 1
            for p in problems:
                print("FAIL b-fixture/%s[%s]: %s" % (name, label, p))
        else:
            print("PASS b-fixture/%s[%s]" % (name, label))
    return failures


def run_b_fixtures(evaluate, catalog_mod, evaluator_mod):
    """Execute B's own fixtures cross-contract (deterministic parts only;
    the accept/refuse draw is C's and is NOT asserted, per B's _status)."""
    data = load_contract("policy-fixtures.json") or {}
    fixtures = data.get("fixtures") or []
    personas = data.get("personas") or {}
    failures = 0
    for fx in fixtures:
        name = fx.get("name", "?")
        if fx.get("cases"):  # e.g. hard_validity_rejections: variants
            failures += run_b_variant_fixture(evaluate, fx, personas)
            continue
        inp = fx.get("input") or {}
        exp = fx.get("expected") or {}
        ctx = inp.get("context") or {}
        try:
            persons, profiles = {}, {}
            i_ref = inp.get("initiator")
            persona = i_ref if isinstance(i_ref, dict) else personas[i_ref]
            i_id = persona["person_ref"]
            i_age = persona.get("age", 30)
            persons[i_id] = {"person_id": i_id, "age": i_age,
                             "is_adult": i_age >= 18, "living": True,
                             "gender": persona.get("gender")}
            profiles[i_id] = dict(persona["profile"])
            t_ref = inp.get("target")
            try:
                t_persona = t_ref if isinstance(t_ref, dict) \
                    else personas[t_ref]
            except KeyError:
                t_persona = None
            if t_persona is not None:
                t_id = t_persona["person_ref"]
                t_age = t_persona.get("age", 30)
                if "age_gap_years" in ctx:
                    t_age = i_age + int(ctx["age_gap_years"])
                persons[t_id] = {"person_id": t_id, "age": t_age,
                                 "is_adult": t_age >= 18, "living": True,
                                 "gender": t_persona.get("gender")}
                profiles[t_id] = dict(t_persona["profile"])
                t_id_eval = t_id
            else:
                t_id_eval = t_ref  # intentionally unknown id
            kinship = ctx.get("kinship_category") or "unrelated"
            if kinship == "none":
                kinship = "unrelated"
            graph = DictGraph(persons, relatedness=kinship,
                              scores=ctx.get("relationship", {}),
                              target_unions=len(
                                  ctx.get("existing_unions_target", [])))
            policy = evaluator_mod.StaticPolicyView(
                catalog_mod.load_catalog(), profiles,
                ctx.get("jurisdiction", BASE_JURISDICTION))
            result = evaluate(inp.get("interaction_kind"), i_id,
                              t_id_eval, graph, policy)
        except Exception as e:  # evaluator must not raise on fixture input
            failures += 1
            print("FAIL b-fixture/%s: raised %r" % (name, e))
            continue
        problems = []
        hard = result.get("hard") or {}
        if bool(hard.get("ok")) != bool((exp.get("hard") or {}).get("ok")):
            problems.append("hard.ok expected %s got %s"
                            % ((exp.get("hard") or {}).get("ok"),
                               hard.get("ok")))
        if "score" in exp and result.get("score") != exp["score"]:
            problems.append("score expected %s got %s"
                            % (exp["score"], result.get("score")))
        for k, v in (exp.get("score_breakdown") or {}).items():
            if (result.get("score_breakdown") or {}).get(k) != v:
                problems.append("breakdown.%s expected %s got %s"
                                % (k, v, (result.get("score_breakdown")
                                          or {}).get(k)))
        if problems:
            failures += 1
            for p in problems:
                print("FAIL b-fixture/%s: %s" % (name, p))
        else:
            print("PASS b-fixture/%s" % name)
    return failures


def main():
    with open(MATRIX, encoding="utf-8") as f:
        data = json.load(f)
    cases = data.get("cases")
    if not isinstance(cases, list) or not cases:
        print("SHAPE ERROR: matrix needs a non-empty cases list")
        return 2
    vocab = reason_vocabulary()
    seen = set()
    shape_errors = []
    for case in cases:
        for e in validate_case(case, seen, vocab):
            shape_errors.append("%s: %s" % (case.get("id", "?"), e))
    if shape_errors:
        for e in shape_errors:
            print("SHAPE FAIL %s" % e)
        return 2

    hook = evaluator_hook()
    evaluate = import_hook(hook) if hook else None
    if evaluate is None:
        for case in cases:
            print("SKIP %s: evaluator hook %r not importable"
                  % (case["id"], hook))
        print("%d cases: shape OK, fixed-rule assertions OK; engine "
              "evaluation SKIPPED (hook missing), not faked" % len(cases))
        return 1

    catalog_mod, evaluator_mod, graph_mod = load_evaluator_modules()
    failures = run_b_fixtures(evaluate, catalog_mod, evaluator_mod)
    for case in cases:
        try:
            result = build_matrix_case(catalog_mod, evaluator_mod,
                                       graph_mod, case)
        except Exception as e:
            failures += 1
            print("FAIL %s: raised %r" % (case["id"], e))
            continue
        problems = check_result(case, result)
        if problems:
            failures += 1
            for p in problems:
                print("FAIL %s: %s" % (case["id"], p))
        else:
            print("PASS %s" % case["id"])
    print("%d cases: shape OK, fixed-rule assertions OK, engine "
          "evaluation ran against the real evaluator + family_graph; "
          "%d failure(s)" % (len(cases), failures))
    return 1 if failures else 0


if __name__ == "__main__":
    sys.exit(main())
