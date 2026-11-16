"""M2 state/ledger assertions for browser QA, coded against the FROZEN
shapes in docs/collab/M2/api-examples.json (A-published). Pure functions
over plain dicts — they run on API responses captured by browser_check
flows (via the {"api": ...} step) and never call the implementation.

Self-test (uses only the frozen examples as fixtures):
    python3 tools/m2_assertions.py
"""

import json
import os
import re
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from tools.expected_finance import check_cash_delta, check_ledger_identity  # noqa: E402

API_EXAMPLES = os.path.join(ROOT, "docs", "collab", "M2", "api-examples.json")

PHASE_ORDER = [
    "year_start", "relations_age", "player_stats", "mortality",
    "work_income", "debt_service", "education", "family_events",
    "illness", "annual_events", "decision_offer", "year_close",
]

SOURCE_KEY_RE = re.compile(
    r"^t_[A-Za-z0-9_]+:[a-z_]+:[A-Za-z0-9:_-]+:.+:[a-z_]+$")

EDU_STAGES = ("none", "elementary", "middle", "high", "university")


def _is_int(v):
    return isinstance(v, int) and not isinstance(v, bool)


def check_state_sections(state):
    """Validate the frozen M2 state section shapes (additive sections).

    Returns (ok, problems[]). `state` is the game state envelope;
    sections may be absent (M1-era states), which is fine — only
    MALFORMED present sections are flagged.
    """
    problems = []

    edu = state.get("education")
    if edu is not None:
        if edu.get("stage") not in EDU_STAGES:
            problems.append("education.stage %r not in %s"
                            % (edu.get("stage"), EDU_STAGES))
        for k in ("enrolled", "dropped_out", "graduated_high_school"):
            if not isinstance(edu.get(k), bool):
                problems.append("education.%s not bool" % k)
        for k in ("grades", "university_years_remaining"):
            if not _is_int(edu.get(k)):
                problems.append("education.%s not int" % k)
        loan = edu.get("loan")
        if not isinstance(loan, dict) or not _is_int(loan.get("principal")) \
                or not _is_int(loan.get("remaining")) \
                or not isinstance(loan.get("has_loan"), bool):
            problems.append("education.loan malformed: %r" % (loan,))

    work = state.get("work")
    if work is not None:
        for k in ("employed", "retired", "can_retire"):
            if not isinstance(work.get(k), bool):
                problems.append("work.%s not bool" % k)
        for k in ("hours", "salary", "effective_annual_gross", "pension",
                  "performance", "stress", "years_worked"):
            if not _is_int(work.get(k)):
                problems.append("work.%s not int" % k)
        if not isinstance(work.get("job_label"), str):
            problems.append("work.job_label not str")

    fin = state.get("finances")
    if fin is not None:
        if not _is_int(fin.get("balance")):
            problems.append("finances.balance not int")
        for d in fin.get("debts", []):
            if not isinstance(d, dict) or d.get("kind") not in (
                    "student_loan",) or not _is_int(d.get("remaining")):
                problems.append("finances.debts row malformed: %r" % (d,))
        est = fin.get("annual_estimate")
        if est is not None:
            for k in ("gross", "tax", "expenses", "net"):
                if not _is_int(est.get(k)):
                    problems.append("annual_estimate.%s not int" % k)
            if all(_is_int(est.get(k)) for k in
                   ("gross", "tax", "expenses", "net")):
                if est["gross"] - est["tax"] - est["expenses"] != est["net"]:
                    problems.append(
                        "annual_estimate identity broken: %r" % (est,))
        ok, ledger_problems = check_ledger_rows(fin.get("ledger", []))
        problems.extend("finances.ledger: " + p for p in ledger_problems)

    fam = state.get("family")
    if fam is not None:
        for k in ("parents", "children", "siblings"):
            ids = fam.get(k)
            if not isinstance(ids, list) or \
                    not all(isinstance(i, str) for i in ids):
                problems.append("family.%s not list[str]" % k)
        if fam.get("partner") is not None and \
                not isinstance(fam.get("partner"), str):
            problems.append("family.partner not person_id/null")

    turn = state.get("turn")
    if turn is not None:
        cursor = turn.get("cursor") or {}
        if cursor.get("phase_id") not in PHASE_ORDER:
            problems.append("turn.cursor.phase_id %r unknown"
                            % (cursor.get("phase_id"),))
        if not _is_int(cursor.get("item_cursor", 0)):
            problems.append("turn.cursor.item_cursor not int")
        if not isinstance(turn.get("pending"), bool):
            problems.append("turn.pending not bool")
    return (not problems), problems


def check_ledger_rows(rows):
    """Frozen ledger-row shape + source_key uniqueness + running balances."""
    problems = []
    seen_keys = {}
    bal = None
    for i, r in enumerate(rows):
        for k in ("id", "source_key", "turn_id", "person_id",
                  "category", "description"):
            if not isinstance(r.get(k), str):
                problems.append("row %d: %s not str" % (i, k))
        for k in ("age", "amount", "balance_after"):
            if not _is_int(r.get(k)):
                problems.append("row %d: %s not int" % (i, k))
        key = r.get("source_key", "")
        m = SOURCE_KEY_RE.match(key)
        if not m:
            problems.append("row %d: source_key %r off the frozen "
                            "{turn}:{phase}:{occurrence}:{person}:{leg} shape"
                            % (i, key))
        if key in seen_keys:
            problems.append("row %d: duplicate source_key %r (first row %d)"
                            % (i, key, seen_keys[key]))
        seen_keys[key] = i
    ok, ident = check_ledger_identity(rows, rows[0]["balance_after"] -
                                      rows[0]["amount"]
                                      if rows and _is_int(rows[0].get(
                                          "balance_after")) else 0)
    problems.extend(ident)
    return (not problems), problems


def check_person_refs_resolve(state):
    """Every person reference in state/journal/ledger resolves to a
    persisted person id (frozen identity invariant)."""
    problems = []
    people = state.get("people") or state.get("peopleById") or []
    if isinstance(people, dict):
        known = set(people)
    else:
        known = {p.get("id") for p in people if isinstance(p, dict)}
    player_id = state.get("player_id")
    if player_id and player_id not in known:
        problems.append("player_id %r not in people" % player_id)

    def ref(where, pid):
        if pid is not None and pid not in known:
            problems.append("%s references unknown person %r"
                            % (where, pid))

    fam = state.get("family") or {}
    for i, pid in enumerate(fam.get("parents", [])):
        ref("family.parents[%d]" % i, pid)
    for i, pid in enumerate(fam.get("children", [])):
        ref("family.children[%d]" % i, pid)
    for i, pid in enumerate(fam.get("siblings", [])):
        ref("family.siblings[%d]" % i, pid)
    ref("family.partner", fam.get("partner"))
    for i, e in enumerate((state.get("journal") or [])):
        for pid in e.get("person_ids", []):
            ref("journal[%d]" % i, pid)
    for i, r in enumerate((state.get("finances") or {}).get("ledger", [])):
        ref("ledger[%d]" % i, r.get("person_id"))
    return (not problems), problems


def check_transfer_legs(legs):
    """Succession/inheritance: grouped legs must net to zero (no fabricated
    or dropped money) per the frozen succession_example."""
    problems = []
    if not legs:
        return False, ["no transfer legs"]
    groups = {}
    for i, leg in enumerate(legs):
        if not _is_int(leg.get("amount")):
            problems.append("leg %d: amount not int" % i)
            continue
        groups.setdefault(leg.get("group_id"), []).append(leg["amount"])
        if not leg.get("source_key"):
            problems.append("leg %d: missing source_key" % i)
    for gid, amounts in groups.items():
        if sum(amounts) != 0:
            problems.append("group %r nets to %s, expected 0"
                            % (gid, sum(amounts)))
    return (not problems), problems


def _self_test():
    with open(API_EXAMPLES) as f:
        ex = json.load(f)
    failures = []

    def expect(ok, problems, label):
        if not ok:
            failures.append("%s: %s" % (label, problems))

    sections = json.loads(json.dumps(ex["new_state_sections"]))
    # the example's stage value is a placeholder enum list; use a real one
    sections["education"]["stage"] = "high"
    ok, problems = check_state_sections(sections)
    expect(ok, problems, "new_state_sections")
    ok, problems = check_state_sections(
        dict(sections,
             finances=dict(sections["finances"],
                           annual_estimate={"gross": 10, "tax": 1,
                                            "expenses": 2, "net": 9})))
    expect(not ok, problems, "broken estimate must fail")

    # abbreviated legs in the example lack category/description/person;
    # fill with real values, keep the frozen source_keys and amounts
    rows = [dict(r, id="le_%d" % i, turn_id="t_000043", age=31,
                 person_id="p", description="x", balance_after=None)
            for i, r in enumerate(ex["work_income_turn_example"]
                                  ["ledger_legs"])]
    for r in rows:
        r["category"] = r["source_key"].rsplit(":", 1)[-1]
    bal = 0
    for r in rows:
        bal += r["amount"]
        r["balance_after"] = bal
    ok, problems = check_ledger_rows(rows)
    expect(ok, problems, "work_income_turn legs")
    ok, delta = check_cash_delta(rows, 0, rows[-1]["balance_after"])
    expect(ok, [], "cash delta == sum(legs)")

    bad = [dict(rows[0], source_key="garbage")]
    ok, problems = check_ledger_rows(bad)
    expect(not ok, problems, "bad source_key must fail")

    state = {"people": [{"id": "p1"}], "player_id": "p1",
             "family": {"parents": ["p1", "ghost"], "partner": None,
                        "children": [], "siblings": []},
             "journal": [{"person_ids": ["p1", "ghost2"]}]}
    ok, problems = check_person_refs_resolve(state)
    expect(not ok and len(problems) == 2, problems,
           "unresolved refs must fail twice")

    # M2c succession example no longer carries raw transfer legs (it
    # shows the continue_as request/response shapes); exercise the
    # transfer-leg checker with an equivalent balanced pair instead.
    demo_legs = [
        {"source_key": "t_000100:succession:inheritance:c1:transfer",
         "account_owner_id": "c1", "category": "inheritance",
         "amount": 125000, "group_id": "g1"},
        {"source_key": "t_000100:succession:estate:p1:transfer",
         "account_owner_id": "p1", "category": "transfer",
         "amount": -125000, "group_id": "g1"}]
    ok, problems = check_transfer_legs(demo_legs)
    expect(ok, problems, "succession transfer legs")
    broken = [dict(l, amount=l["amount"] + 1) for l in demo_legs]
    ok, problems = check_transfer_legs(broken)
    expect(not ok, problems, "unbalanced transfer must fail")

    if failures:
        print("SELF-TEST FAILURES:")
        for f_ in failures:
            print(" -", f_)
        return 1
    print("m2_assertions self-test OK (frozen api-examples fixtures)")
    return 0


if __name__ == "__main__":
    sys.exit(_self_test())
