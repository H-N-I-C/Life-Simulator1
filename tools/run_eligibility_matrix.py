#!/usr/bin/env python3
"""Run the M2.5 eligibility-matrix fixtures (tests/content/eligibility_matrix.json).

Draft until B's evaluator and A's family_graph are implemented (see
docs/collab/M2.5/contracts/validation-protocol.md §2). Behavior:

1. ALWAYS: validate fixture well-formedness, the fixed 18+ rule
   (independent of any evaluator), and fixture reason codes against B's
   published reason_code_vocabulary (policy-fixtures.json). Shape errors
   exit 2.
2. If B's evaluator module is importable (evaluator_entry in
   policy-fixtures.json): also execute B's own fixtures cross-contract
   (deterministic hard/score/breakdown parts per B's _status note).
3. Full engine evaluation of MY matrix cases requires A's in-memory graph
   (ages live in chronology, not in the fixture). Until A publishes the
   graph fixture builder, those cases print SKIP — never a faked pass.

The hook lookup is dynamic on purpose: this file must run UNMODIFIED
against the frozen evaluator.
"""

import importlib
import json
import os
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
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


def reason_vocabulary():
    data = load_contract("policy-fixtures.json") or {}
    vocab = data.get("reason_code_vocabulary") or {}
    return set(vocab.get("soft") or []) | set(vocab.get("hard") or [])


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
    matrix case's expectations, including the UI projection classes."""
    problems = []
    expect = case["expect"]
    hard = result.get("hard") or {}
    gate = "both_pass" if hard.get("ok") else "blocked"
    if gate != expect["adult_gate"]:
        problems.append("adult_gate: expected %s, evaluator hard.ok=%s "
                        "(code %s)" % (expect["adult_gate"],
                                       hard.get("ok"), hard.get("code")))
    expected_hard = set(expect.get("reason_codes") or []) & {
        "not_adult", "dead", "self_target", "unknown_person",
        "duplicate_member", "graph_error"}
    if gate == "blocked" and expected_hard and \
            hard.get("code") not in expected_hard:
        problems.append("hard.code: expected one of %s, got %r"
                        % (sorted(expected_hard), hard.get("code")))
    refusal = result.get("refusal") or {}
    if gate == "both_pass" and \
            bool(refusal.get("possible")) != expect["refusal_possible"]:
        problems.append("refusal_possible: expected %s, got %s"
                        % (expect["refusal_possible"],
                           refusal.get("possible")))
    reasons = {r.get("code") for r in result.get("reasons") or []
               if isinstance(r, dict)}
    expected_soft = set(expect.get("reason_codes") or []) - {
        "not_adult", "dead", "self_target", "unknown_person",
        "duplicate_member", "graph_error"}
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


def run_b_fixtures(evaluate):
    """Execute B's own fixtures cross-contract (deterministic parts only;
    the accept/refuse draw is C's and is NOT asserted, per B's _status)."""
    data = load_contract("policy-fixtures.json") or {}
    fixtures = data.get("fixtures") or []
    failures = 0
    for fx in fixtures:
        name = fx.get("name", "?")
        inp = fx.get("input") or {}
        exp = fx.get("expected") or {}
        try:
            result = evaluate(
                inp.get("interaction_kind"),
                (inp.get("initiator") or {}).get("person_id",
                                                 inp.get("initiator")),
                (inp.get("target") or {}).get("person_id",
                                              inp.get("target")),
                inp.get("context") or {})
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
    evaluate = None
    if hook is not None:
        try:
            module = importlib.import_module(hook[0])
            evaluate = getattr(module, hook[1])
        except (ImportError, AttributeError) as e:
            print("NOTE: evaluator hook published but not importable yet "
                  "(%s); matrix engine evaluation stays SKIPPED." % e)

    failures = 0
    if evaluate is not None:
        failures += run_b_fixtures(evaluate)
        print("NOTE: full matrix engine evaluation requires A's "
              "family_graph chronology (ages live in the graph, not in "
              "fixtures); matrix cases SKIP until A publishes the graph "
              "fixture builder. Fixed-rule self-assertions above already "
              "ran.")
    else:
        for case in cases:
            print("SKIP %s: evaluator not importable yet" % case["id"])
    print("%d cases: shape OK, fixed-rule assertions OK%s."
          % (len(cases),
             ", %d b-fixture failure(s)" % failures if evaluate else
             "; engine evaluation SKIPPED, not faked"))
    return 1 if failures else 0


if __name__ == "__main__":
    sys.exit(main())
