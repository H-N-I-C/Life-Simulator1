"""M3 education/qualifications (B-owned, M3/02-contracts.md §Education).

Programs/enrollment/tuition/debt/completion. Commands ride the existing
validated action/replay/transaction boundary (A applies the dispatcher
integration requests); money posts only through the one ledger poster;
legacy school/university progression (uv_years) is never touched here.
Draws consume only the mounted command stream.
"""

import json
import os

DATA_PATH = os.path.join(os.path.dirname(__file__), "..", "..", "content",
                         "education", "programs.json")

ACTION_IDS = ("education_enroll", "education_study", "education_leave")

_STATE_VERSION = 1
# frozen pass/fail policy (§Education): studying guarantees the year;
# skipping study fails on rng < 0.15
FAIL_CHANCE_UNSUDIED = 0.15
# frozen admission gate for scholarship funding (pre-sampled at enroll)
SCHOLARSHIP_MIN_SMARTS = 70

PROGRAMS = {}


def load_data():
    global PROGRAMS
    with open(os.path.abspath(DATA_PATH), encoding="utf-8") as f:
        data = json.load(f)
    if data.get("schema_version") != 1:
        raise ValueError("education data schema_version must be 1")
    programs = {p["id"]: p for p in data["programs"]}
    labels = [p["credential_id"] for p in programs.values()]
    if len(set(labels)) != len(labels):
        raise ValueError("duplicate credential_id in education data")
    PROGRAMS = programs
    return data


def data():
    if not PROGRAMS:
        load_data()
    return PROGRAMS


def empty_state():
    return {"schema_version": _STATE_VERSION, "next_num": 1,
            "enrollments": {}, "qualifications": []}


def ensure(env):
    st = env.get("education_state")
    if not isinstance(st, dict) or st.get("schema_version") != _STATE_VERSION:
        st = empty_state()
        env["education_state"] = st
    st.setdefault("enrollments", {})
    st.setdefault("qualifications", [])
    _migrate_ownership(st, env)
    return st


def _migrate_ownership(st, env):
    """R2 (Codex consolidated review, 2026-11-21): qualification records
    are person-scoped. Pre-fix records carry no owner; in M3a only the
    current player could ever earn one, so ownerless records are
    attributed to env['player'] exactly once and persist with the save
    (the envelope is rewritten on the next save; reloads are then
    stable). Archives are preserved — records are never dropped."""
    owner = getattr(env.get("player"), "person_id", None)
    if not owner:
        return
    for q in st.get("qualifications", []):
        if isinstance(q, dict) and not q.get("person_id"):
            q["person_id"] = owner


# ------------------------------------------------------------- legacy adapter

def apply_funding_adapter(env, person, loan_principal=0):
    """The ONLY feature path to the legacy student-loan scalars: loan
    funding advances the total principal ONCE at enrollment; M2
    debt_service repays it as today."""
    if loan_principal:
        person.student_loan = getattr(person, "student_loan", 0) \
            + int(loan_principal)
        person.chose_student_loan = True


# ------------------------------------------------------------- A bridges

def _careers_bridge(name):
    try:
        from src.game.features.careers import core as careers_core
    except Exception:
        return None
    return getattr(careers_core, name, None)


def active_assignment_present(env, person_id):  # noqa: D103 - R3 shim
    fn = _careers_bridge("active_assignment")
    if fn is None:
        return False
    return fn(env, person_id) is not None


# ------------------------------------------------------------- eligibility

def earned_qualification_ids(env, person_id):
    """Person-scoped earned credentials (R2): a qualification belongs to
    the person who earned it; successors keep their own records."""
    st = ensure(env)
    return {q["qualification_id"] for q in st["qualifications"]
            if isinstance(q, dict) and q.get("qualification_id")
            and q.get("person_id") == person_id}


def secondary_complete(person):
    return not person.dropped_out and not person.is_in_school()


def active_enrollment(env, person_id):
    st = ensure(env)
    for e in st["enrollments"].values():
        if e["person_id"] == person_id and e["status"] == "active":
            return e
    return None


def check_eligibility(env, person, program):
    """Server-side eligibility (§Education projection): advertised ==
    dispatched. Returns (requirements_view, eligible, reason)."""
    req_view = {"min_age": None, "secondary_met": None, "min_smarts": None,
                "qualifications": []}
    failures = []
    if program["min_age"] and person.age < program["min_age"]:
        req_view["min_age"] = program["min_age"]
        failures.append(
            f"you must be at least {program['min_age']} to enroll")
    if program.get("requires_secondary"):
        met = secondary_complete(person)
        req_view["secondary_met"] = met
        if not met:
            failures.append(
                "you need to finish secondary school first"
                if not person.dropped_out
                else "you dropped out of secondary school")
    admission = program.get("admission") or {}
    min_smarts = admission.get("min_smarts")
    if min_smarts and person.smarts < min_smarts:
        req_view["min_smarts"] = min_smarts
        failures.append(
            f"this program asks for smarts {min_smarts} or higher")
    earned = earned_qualification_ids(env, person.person_id)
    for qid in admission.get("qualifications", []):
        met = qid in earned
        req_view["qualifications"].append(
            {"id": qid,
             "label": _credential_label(qid, program), "met": met})
        if not met:
            failures.append(f"you need the {_credential_label(qid, program)}")
    return req_view, not failures, ("; ".join(failures) if failures else None)


def _credential_label(credential_id, _program):
    for p in data().values():
        if p["credential_id"] == credential_id:
            return p["credential_label"]
    return credential_id


def _blocked_reason(env, person, program):
    """Commitment/coexistence gates that are NOT program requirements
    (so they don't show as program 'requirements' but still refuse
    dispatch with the precise reason).

    R3 (Amendment 2, codex consolidated review): the ONE shared
    commitment decision feeds both advertised reasons and dispatch —
    job/study/apprenticeship conflicts (named assignment, legacy job,
    legacy university, secondary school, active enrollment) all come
    from commitments.refusal, never from feature-local rules. The only
    education-specific gate left here is the apprenticeship stage gate
    (A's bridge availability — a stage gate, never a dummy wage)."""
    from src.game.features import commitments
    want = (commitments.APPRENTICESHIP
            if program.get("apprentice_role") is not None
            else commitments.STUDY)
    blocked = commitments.refusal(env, person, want)
    if blocked:
        return blocked
    if program.get("apprentice_role") is not None and _careers_bridge(
            "create_apprenticeship_assignment") is None:
        return "apprenticeship placements are not open yet"
    return None


# ------------------------------------------------------------- commands

def _event(kind, text, person, changes=None):
    return {"kind": kind, "text": text,
            "person_ids": [person.person_id], "changes": changes or {}}


def _money(phase, occurrence_id, leg, person, category, amount, desc):
    return {"phase": phase, "occurrence_id": occurrence_id, "leg": leg,
            "account_owner_id": person.person_id, "category": category,
            "amount": int(amount), "description": desc}


def cmd_enroll(env, person, payload, rng, service):
    programs = data()
    program_id = (payload or {}).get("program_id")
    # R5 (Codex review): typed validation BEFORE any lookup/draw/mutation —
    # missing, list/object/bool or unknown ids are a plain 400, never a
    # programming TypeError, and leave no revision/RNG/attempt side effects.
    if not isinstance(program_id, str) or not program_id:
        raise ValueError("program_id must be a non-empty string")
    program = programs.get(program_id)
    if program is None:
        raise ValueError(f"unknown program: {program_id}")
    blocked = _blocked_reason(env, person, program)
    if blocked:
        raise PermissionError(blocked)
    _, eligible, reason = check_eligibility(env, person, program)
    if not eligible:
        raise PermissionError(reason)

    tuition = program["tuition_per_year"] * program["duration_years"]
    funding_choices = []
    sampled = {"spent": []}
    if tuition > 0:
        funding_choices.append({"id": "self", "label": "Pay each year"})
        funding_choices.append({"id": "student_loan",
                                "label": "Student loan (borrow the full "
                                         "tuition)"})
        if getattr(person, "parents", None):
            parents_accept = rng.random() < 0.75
            sampled["parents_accept"] = parents_accept
            funding_choices.append({"id": "parents",
                                    "label": "Ask your parents to pay"})
        if person.smarts >= SCHOLARSHIP_MIN_SMARTS:
            scholarship_accept = rng.random() < 0.5
            sampled["scholarship_accept"] = scholarship_accept
            funding_choices.append({"id": "scholarship",
                                    "label": "Apply for a scholarship"})
        funding_choices.append({"id": "decline", "label": "Not now"})

    if not funding_choices:
        # tuition-free program (apprenticeship): enroll directly
        return _enroll(env, person, program, "self", rng)

    pending = {"id": None, "kind": "education_funding",
               "event_id": f"education_funding:{program_id}",
               "title": f"Enroll: {program['label']}",
               "text": (f"{program['label']} costs "
                        f"${program['tuition_per_year']:,} a year for "
                        f"{program['duration_years']} years. How would you "
                        f"like to pay?"),
               "person_ids": [person.person_id],
               "_program_id": program_id,
               "sampled": sampled,
               "options": [{**o, "known_costs": [], "known_effects": [],
                            "enabled": True, "reason": None}
                           for o in funding_choices]}
    return {"events": [], "money_entries": [],
            "pending_decision": pending}


def _enroll(env, person, program, funding, rng):
    st = ensure(env)
    enrollment = {
        "enrollment_id": f"e_{st['next_num']}",
        "person_id": person.person_id,
        "program_id": program["id"],
        "program_version": program.get("schema_version_data", 1),
        "started_turn": env.get("turn", {}).get("turn_id")
                        or f"rev{env['revision']}",
        "progress": 0, "status": "active", "funding": funding,
        "tuition_per_year": program["tuition_per_year"],
        "fee_per_year": program["fee_per_year"],
        "studied_this_year": False, "attempts": 0, "last_turn": None}
    st["next_num"] += 1
    st["enrollments"][enrollment["enrollment_id"]] = enrollment
    events = [_event("education",
                     f"I enrolled in the {program['label']} program.",
                     person)]
    apprentice = program.get("apprentice_role")
    if apprentice:
        create = _careers_bridge("create_apprenticeship_assignment")
        create(env, person, apprentice["role_id"], apprentice["employer_id"])
        events.append(_event(
            "work",
            f"I started my apprenticeship at the same time.", person))
    return {"events": events, "money_entries": [],
            "pending_decision": None, "_enrollment": enrollment}


def resolve_funding(env, person, pending, option_id, rng):
    """Resolve an education_funding pending inside _cmd_resolve."""
    program = data().get(pending.get("_program_id"))
    if program is None:
        raise ValueError("unknown program in funding decision")
    available = [o["id"] for o in pending.get("options", [])]
    if option_id not in available:
        raise PermissionError("unknown or no-longer-available option")
    sampled = pending.get("sampled") or {}
    if option_id == "decline":
        return {"events": [_event(
            "education",
            f"I decided not to enroll in the {program['label']} "
            f"program for now.", person)],
            "money_entries": [], "pending_decision": None}
    if option_id == "parents" and not sampled.get("parents_accept", False):
        return _refuse_funding(env, person, pending, program, option_id,
                               "My parents couldn't cover the tuition.")
    if option_id == "scholarship" \
            and not sampled.get("scholarship_accept", False):
        return _refuse_funding(env, person, pending, program, option_id,
                               "My scholarship application was rejected.")
    result = _enroll(env, person, program, option_id, rng)
    if option_id == "student_loan":
        apply_funding_adapter(
            env, person,
            program["tuition_per_year"] * program["duration_years"])
        result["events"].append(_event(
            "finances",
            "I took out a student loan covering the whole program.", person))
    return result


def _refuse_funding(env, person, pending, program, option_id, text):
    spent = list((pending.get("sampled") or {}).get("spent", []))
    spent.append(option_id)
    pending["sampled"]["spent"] = spent
    events = [_event("education", text, person)]
    remaining = [o for o in pending["options"] if o["id"] not in spent]
    if len(remaining) <= 1:  # only decline left
        events.append(_event(
            "education",
            f"Without funding I couldn't enroll in the "
            f"{program['label']} program.", person))
        return {"events": events, "money_entries": [],
                "pending_decision": None}
    reoffer = dict(pending)
    reoffer["options"] = remaining
    return {"events": events, "money_entries": [],
            "pending_decision": reoffer}


def cmd_study(env, person, payload, rng, service):
    enrollment = active_enrollment(env, person.person_id)
    if enrollment is None:
        raise PermissionError("you are not enrolled in a program")
    if enrollment["studied_this_year"]:
        raise PermissionError("you already studied hard this year")
    enrollment["studied_this_year"] = True
    return {"events": [_event(
        "education", "I put in extra study hours this year.", person)],
        "money_entries": [], "pending_decision": None}


def cmd_leave(env, person, payload, rng, service):
    enrollment = active_enrollment(env, person.person_id)
    if enrollment is None:
        raise PermissionError("you are not enrolled in a program")
    st = ensure(env)
    enrollment["status"] = "left"
    program = data()[enrollment["program_id"]]
    events = [_event(
        "education",
        f"I left the {program['label']} program.", person)]
    if program.get("apprentice_role"):
        close = _careers_bridge("close_apprenticeship")
        if close is not None:
            close(env, person, "left_program")
        events.append(_event("work", "My apprenticeship ended too.", person))
    return {"events": events, "money_entries": [],
            "pending_decision": None}


def apply_action(env, person, action, payload, rng, service):
    if action == "education_enroll":
        return cmd_enroll(env, person, payload, rng, service)
    if action == "education_study":
        return cmd_study(env, person, payload, rng, service)
    if action == "education_leave":
        return cmd_leave(env, person, payload, rng, service)
    raise ValueError(f"unknown education action {action}")


def action_specs(env, person):
    """Advertised education specs (single source with dispatch)."""
    enrollment = active_enrollment(env, person.person_id)
    enroll_reasons = [
        _blocked_reason(env, person, program)
        or check_eligibility(env, person, program)[2]
        for program in data().values()]
    can_enroll = any(reason is None for reason in enroll_reasons)
    specs = [{
        "id": "education_enroll", "label": "Enroll in a program",
        "category": "education",
        "inputs": [{"id": "program_id", "type": "text", "required": True}],
        "known_costs": [], "limits": [],
        "enabled": can_enroll,
        "reason": None if can_enroll else
        next(iter(enroll_reasons), "no programs are available")}]
    specs.append({
        "id": "education_study", "label": "Study hard this year",
        "category": "education", "inputs": [], "known_costs": [],
        "limits": ["once per year"],
        "enabled": enrollment is not None
        and not enrollment["studied_this_year"],
        "reason": None if enrollment is not None
        else "you are not enrolled in a program"})
    specs.append({
        "id": "education_leave", "label": "Leave the program",
        "category": "education", "inputs": [], "known_costs": [],
        "limits": [],
        "enabled": enrollment is not None,
        "reason": None if enrollment is not None
        else "you are not enrolled in a program"})
    return specs


# ------------------------------------------------------------- annual hook

def annual_hook(env, service, rng):
    """Runs inside run_turn immediately AFTER the legacy education phase
    is journaled (A-owned one-line integration, §Education). Per active
    enrollment, once per turn id: bill the year, pass/fail, progress,
    award at completion. Never posts wage legs; tuition posts through
    the command result boundary's poster by the caller."""
    st = ensure(env)
    turn_id = env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"
    events = []
    money = []
    person = env["player"]
    for e in st["enrollments"].values():
        # R2: only the CURRENT player's enrollments bill/advance/award —
        # a previous generation's open enrollment is dormant for a
        # successor (never bills them, never awards them a credential).
        if e["person_id"] != person.person_id:
            continue
        if e["status"] != "active" or e.get("last_turn") == turn_id:
            continue
        e["last_turn"] = turn_id
        program = data()[e["program_id"]]
        progress = e["progress"]
        occurrence = f"edu:{e['enrollment_id']}:year:{progress}"

        # 1. bill the year (self tuition cash leg; fee always cash;
        #    loan principal landed once at enrollment; parents and
        #    scholarship carry no player cash leg — legacy parity)
        if e["funding"] == "self" and e["tuition_per_year"]:
            money.append(_money("education", occurrence, "tuition", person,
                                "education", -e["tuition_per_year"],
                                f"{program['label']} tuition"))
        if e["fee_per_year"]:
            money.append(_money("education", occurrence, "fee", person,
                                "education", -e["fee_per_year"],
                                f"{program['label']} materials fee"))

        # 2/3. pass/fail then progress
        if e["studied_this_year"] or rng.random() >= FAIL_CHANCE_UNSUDIED:
            e["progress"] = progress + 1
            e["attempts"] = 0
            before = person.smarts
            person.change_smarts(rng.randint(1, 3))
            events.append(_event(
                "education",
                f"I completed year {e['progress']} of the "
                f"{program['label']}.", person,
                {"smarts": person.smarts - before}))
            if e["progress"] >= program["duration_years"]:
                e["status"] = "completed"
                st["qualifications"].append({
                    "qualification_id": program["credential_id"],
                    "program_id": program["id"],
                    "program_version": e["program_version"],
                    "person_id": e["person_id"],
                    "awarded_turn": turn_id})
                before = person.smarts
                person.change_smarts(rng.randint(8, 12))
                smarts_delta = person.smarts - before
                before = person.happiness
                person.change_happiness(rng.randint(10, 16))
                events.append(_event(
                    "education",
                    f"I graduated! I now hold the "
                    f"{program['credential_label']}.", person,
                    {"smarts": smarts_delta,
                     "happiness": person.happiness - before}))
        else:
            e["attempts"] += 1
            person.change_happiness(-rng.randint(5, 10))
            events.append(_event(
                "education",
                f"I failed a year of the {program['label']} and have to "
                f"repeat it.", person))
        e["studied_this_year"] = False
    return {"events": events, "money_entries": money,
            "pending_decision": None}


# ------------------------------------------------------------- projection

def education_view(env, person):
    programs = data()
    st = ensure(env)
    enrollment = active_enrollment(env, person.person_id)
    enrollment_view = None
    if enrollment is not None:
        program = programs[enrollment["program_id"]]
        enrollment_view = {
            "enrollment_id": enrollment["enrollment_id"],
            "program_id": enrollment["program_id"],
            "program_label": program["label"],
            "progress": enrollment["progress"],
            "duration_years": program["duration_years"],
            "status": enrollment["status"],
            "funding": enrollment["funding"],
            "tuition_per_year": enrollment["tuition_per_year"],
            "fee_per_year": enrollment["fee_per_year"],
            "studied_this_year": enrollment["studied_this_year"],
            "attempts": enrollment["attempts"]}
    program_views = []
    for program in programs.values():
        req, eligible, reason = check_eligibility(env, person, program)
        blocked = _blocked_reason(env, person, program)
        funding = list(program["funding"])
        if "parents" in funding and not getattr(person, "parents", None):
            funding.remove("parents")
        if "scholarship" in funding \
                and person.smarts < SCHOLARSHIP_MIN_SMARTS:
            funding.remove("scholarship")
        program_views.append({
            "program_id": program["id"], "label": program["label"],
            "credential_id": program["credential_id"],
            "credential_label": program["credential_label"],
            "duration_years": program["duration_years"],
            "tuition_per_year": program["tuition_per_year"],
            "fee_per_year": program["fee_per_year"],
            "total_cost": (program["tuition_per_year"]
                           + program["fee_per_year"])
                          * program["duration_years"],
            "study_skill": program["study_skill"],
            "apprentice_role": program.get("apprentice_role"),
            "requirements": req,
            "funding_options": funding,
            "eligible": eligible and blocked is None,
            "reason": reason or blocked})
    qualifications = [{
        "qualification_id": q["qualification_id"],
        "label": _credential_label(q["qualification_id"], None),
        "program_id": q["program_id"],
        "awarded_turn": q.get("awarded_turn")}
        for q in st["qualifications"]
        if q.get("person_id") == person.person_id]
    history = [{
        "enrollment_id": e["enrollment_id"],
        "program_id": e["program_id"],
        "program_label": programs.get(e["program_id"], {}).get("label"),
        "status": e["status"], "progress": e["progress"],
        "started_turn": e["started_turn"]}
        for e in st["enrollments"].values()
        if e["person_id"] == person.person_id and e["status"] != "active"]
    return {"enrollment": enrollment_view, "programs": program_views,
            "qualifications": qualifications, "history": history}


load_data()
