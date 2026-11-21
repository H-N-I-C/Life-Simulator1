"""M3 careers/employers (A-owned, contracts.md §Careers).

Commands ride the existing validated action/replay/transaction boundary;
M2 legacy payroll scalars remain the single wage authority via the
adapter. Draws consume only the mounted command stream.
"""

import json
import os

DATA_PATH = os.path.join(os.path.dirname(__file__), "..", "..", "content",
                         "careers", "careers.json")

ACTION_IDS = ("careers_apply", "careers_resign", "careers_promote")

# frozen attempt caps (§Careers): per person per aged year
MAX_APPLY_ATTEMPTS = 2
MAX_PROMOTE_ATTEMPTS = 2

_STATE_VERSION = 1
ROLES = {}
EMPLOYERS = {}
_BY_EMPLOYER = {}


def load_data():
    global ROLES, EMPLOYERS, _BY_EMPLOYER
    with open(os.path.abspath(DATA_PATH), encoding="utf-8") as f:
        data = json.load(f)
    if data.get("schema_version") != 1:
        raise ValueError("careers data schema_version must be 1")
    roles = {r["id"]: r for r in data["roles"]}
    employers = {e["id"]: e for e in data["employers"]}
    for e in employers.values():
        for rid in e["role_ids"]:
            if rid not in roles:
                raise ValueError(f"employer {e['id']} references unknown role {rid}")
    for r in roles.values():
        for target in r.get("promotions_to", []):
            if target not in roles:
                raise ValueError(f"role {r['id']} promotes to unknown {target}")
        for alt in r.get("prerequisites", {}).get("alternatives", []):
            for rid in alt.get("roles", []):
                if rid not in roles:
                    raise ValueError(f"role {r['id']} alternative references {rid}")
    ROLES, EMPLOYERS = roles, employers
    _BY_EMPLOYER = {eid: [ROLES[r] for r in e["role_ids"]]
                    for eid, e in employers.items()}
    return data


def data():
    if not ROLES:
        load_data()
    return ROLES, EMPLOYERS


def empty_state():
    return {"schema_version": _STATE_VERSION, "next_num": 1,
            "assignments": {}, "offers": {}, "attempts": {},
            "past_assignments": {}}


def ensure(env):
    st = env.get("careers")
    if not isinstance(st, dict) or st.get("schema_version") != _STATE_VERSION:
        st = empty_state()
        env["careers"] = st
    st.setdefault("assignments", {})
    st.setdefault("offers", {})
    st.setdefault("attempts", {})
    st.setdefault("past_assignments", {})
    return st


def active_assignment(env, person_id):
    st = ensure(env)
    a = st["assignments"].get(person_id)
    if a and a.get("status") == "active":
        return a
    return None


# ------------------------------------------------------------- B/C bridges

def earned_qualifications(env, person_id=None):
    """B bridge: durable qualification records. Person-scoped per the
    R2 contract: records carrying an owner (person_id) belong to that
    person only and never leak to a successor; ownerless records are
    pre-R2 legacy globals and stay readable until B's migration stamps
    them (absent envelope -> empty, never fabricated)."""
    edu = env.get("education_state") or {}
    out = set()
    for q in edu.get("qualifications", []):
        if not isinstance(q, dict) or not q.get("qualification_id"):
            continue
        owner = q.get("person_id")
        if person_id is not None and owner not in (None, person_id):
            continue
        out.add(q["qualification_id"])
    return out


def skill_values(env, person_id):
    """C bridge (frozen §Skills): deterministic per-person values via
    skills.state.values — backfilled defaults, no rng on reads."""
    from src.game.features.skills import state as skills_state
    return skills_state.values(env, person_id)


def practiced_skill(role_id):
    """trains_skill declaration (frozen §Skills): the skill an active
    role practices yearly, from role data; None if undeclared."""
    roles, _ = data()
    return roles.get(role_id, {}).get("practices_skill")


def prior_roles(st, person_id):
    """Role history: active + all past assignment role ids."""
    roles = set()
    a = st["assignments"].get(person_id)
    if a:
        roles.add(a["role_id"])
    for past in st.get("past_assignments", {}).get(person_id, []):
        roles.add(past["role_id"])
    return roles


def _close_assignment(st, person_id, asn, status, to_turn):
    """Move a closed assignment into the single history store."""
    closed = dict(asn)
    closed["status"] = status
    closed["to_turn"] = to_turn
    st["past_assignments"].setdefault(person_id, []).append(closed)


# ------------------------------------------------------------- requirements

def _credential_label(credential_id):
    """Human label for a B education credential id (B's data is the
    source); falls back to the raw id only when unknown."""
    try:
        from src.game.features.education import core as education_core
        for p in education_core.data().values():
            if p["credential_id"] == credential_id:
                return p["credential_label"]
    except Exception:
        pass
    return credential_id


def check_requirements(env, person, role):
    """Server-side eligibility (§Careers projection): returns
    (requirements_view, eligible, reason). Advertised == dispatched."""
    roles, employers = data()
    req = role.get("prerequisites", {})
    quals = earned_qualifications(env, person.person_id)
    skills = skill_values(env, person.person_id)
    st = ensure(env)
    mine = prior_roles(st, person.person_id)
    current = active_assignment(env, person.person_id)

    view = {"min_age": None, "qualifications": [], "skills": [], "roles": []}
    failures = []

    min_age = req.get("min_age")
    if min_age:
        view["min_age"] = min_age
        if person.age < min_age:
            failures.append(f"this role requires age {min_age} or older")

    alternatives = req.get("alternatives") or []

    def _recorded_years(rid):
        """C1 frozen policy: total recorded years across ALL stints in
        this role (current role_tenure + every past assignment's
        recorded role_tenure for the role). Unknown/missing tenure
        counts as 0; transitions never add years."""
        total = 0
        if current is not None and current["role_id"] == rid:
            total += current.get("role_tenure", 0)
        for past in st.get("past_assignments", {}).get(
                person.person_id, []):
            if past.get("role_id") == rid:
                total += past.get("role_tenure") or 0
        return total

    def _role_met(rid, min_tenure):
        if min_tenure is None:
            return rid in mine
        return _recorded_years(rid) >= min_tenure

    def _route_ok(route_quals, route_roles, min_tenure):
        if any(q not in quals for q in route_quals):
            return False
        return all(_role_met(r, min_tenure) for r in route_roles)

    # Route semantics (Amendment 5): the default route is {main quals +
    # main roles}; every `alternatives` entry is an ADDITIONAL route.
    # The gate passes when ANY route passes; skills/min_age apply on all
    # routes. When alternatives exist, a completely empty default route
    # is not offered (the alts ARE the routes — how
    # operations_specialist was authored).
    main_quals = req.get("qualifications", [])
    main_roles = req.get("roles", [])
    routes = []
    if main_quals or main_roles or not alternatives:
        routes.append((main_quals, main_roles, None, None))
    for alt in alternatives:
        routes.append((alt.get("qualifications", []),
                       alt.get("roles", []),
                       alt.get("min_role_tenure"), alt.get("label")))
    routes_ok = any(_route_ok(q, r, t) for q, r, t, _ in routes)

    def _route_label(rq, rr, rl):
        if rl:
            return rl
        bits = [_credential_label(q) for q in rq]
        bits += [("experience as "
                  + (roles[r]["label"] if r in roles else r)) for r in rr]
        return " + ".join(bits)

    def _route_view(rq, rr, rt, rl):
        return {
            "label": _route_label(rq, rr, rl),
            "met": _route_ok(rq, rr, rt),
            "qualifications": [{"id": q, "label": _credential_label(q),
                                "met": q in quals} for q in rq],
            "roles": [{"id": r,
                       "label": roles[r]["label"] if r in roles else r,
                       "min_tenure": rt,
                       "recorded_years": (_recorded_years(r)
                                          if rt is not None else None),
                       "met": _role_met(r, rt)} for r in rr],
        }

    # C3: publish the named routes + the shared age/skills so B can
    # render "route A OR route B" with shared requirements once
    view["routes"] = [_route_view(q, r, t, l) for q, r, t, l in routes]
    view["shared"] = {"min_age": view["min_age"],
                      "skills": view["skills"]}

    if not alternatives:
        for qid in main_quals:
            met = qid in quals
            view["qualifications"].append({"id": qid, "met": met,
                                           "label": _credential_label(qid)})
            if not met:
                failures.append(
                    f"you need the {_credential_label(qid)}")
        for rid in main_roles:
            label = roles[rid]["label"] if rid in roles else rid
            met = rid in mine
            view["roles"].append({"id": rid, "label": label, "met": met})
            if not met:
                failures.append(f"you need experience as {label}")
    else:
        # alternatives present: the routes line replaces the individual
        # qual/role failure lines; per-item `met` still reflects whether
        # the item itself is satisfied on any route description
        for qid in main_quals:
            view["qualifications"].append({
                "id": qid, "met": qid in quals,
                "label": _credential_label(qid)})
        for rid in main_roles:
            view["roles"].append({"id": rid,
                                  "label": roles[rid]["label"]
                                  if rid in roles else rid,
                                  "met": _role_met(rid, None)})
        if not routes_ok:
            # Requirement text uses human labels, never raw ids, with
            # proper spacing ("Bachelor of Business or 8 years as a
            # Qualified care worker" — not "business_degreeor ...")
            parts = [_route_label(rq, rr, rl) for rq, rr, rt, rl in routes]
            failures.append("you need " + " or ".join(parts))

    # frozen §Skills query: requirements_met is C's read-only gate; the
    # same values feed the projection view (advertised == dispatched)
    from src.game.features.skills import projection as skills_projection
    skill_reqs = [{"skill": sid, "min": needed}
                  for sid, needed in (req.get("skills") or {}).items()]
    met_result = skills_projection.requirements_met(skills, skill_reqs)
    missing_skills = {m["skill"]: m for m in met_result["missing"]}
    for sid, needed in (req.get("skills") or {}).items():
        value = skills.get(sid)
        met = sid not in missing_skills
        view["skills"].append({"id": sid, "value": value if isinstance(
            value, int) else None, "required": needed, "met": met})
        if not met:
            have = "unknown" if not isinstance(value, int) else value
            failures.append(f"{sid} skill {have}/{needed}")

    return view, not failures, ("; ".join(failures) if failures else None)


def openings_view(env, person):
    """All employer-role openings with server-computed eligibility."""
    roles, employers = data()
    out = []
    for eid, employer in employers.items():
        for role in _BY_EMPLOYER[eid]:
            req, eligible, reason = check_requirements(env, person, role)
            out.append({
                "role_id": role["id"], "role_label": role["label"],
                "path": role["path"], "level": role["level"],
                "employer_id": eid, "employer_name": employer["name"],
                "pay_min": role["pay_min"], "pay_max": role["pay_max"],
                "base_hours": role["base_hours"],
                "promotions_to": list(role.get("promotions_to", [])),
                "requirements": req,
                "eligible": eligible,
                "reason": reason,
                "reason_kind": "requirements" if reason else None})
    return out


# ------------------------------------------------------------- adapter

def apply_adapter(env, person):
    """One wage payer: sync legacy M2 payroll scalars from the active
    assignment (or clear them). Feature code never edits the scalars
    except through here.

    M3b: a FRESH employment start (not-employed -> employed, both
    ordinary offers and apprenticeship bridge starts) also initializes
    the M2 job-start baseline with Player.get_job parity — performance
    50 / stress 45 / fresh job-start fields. Without this a new hire
    kept the cleared 0/0 state and was fired on the first age_up.
    Ongoing employment (promotion, employer-change accept while still
    legacy-employed) keeps its performance/stress/years — pension
    salary_years are never reset mid-job. With no active assignment, a
    positive salary on an already job-less person is M2's retirement
    path (lose_job then salary = pension) — the pension keeps paying
    and must survive this sync and reloads."""
    a = active_assignment(env, person.person_id)
    if a is not None:
        was_employed = person.has_job
        person.has_job = True
        person.salary = a["salary"]
        person.job_hours = a["hours"]
        if not was_employed:
            # fresh-employment baseline (Player.get_job parity)
            person.performance = 50
            person.stress = 45
            person.years_worked = 0
            person.last_raise = person.age
            person.times_asked_since_last_raise = 0
            person.salary_years = []
    else:
        was_employed = person.has_job
        person.has_job = False
        person.job_hours = 0
        if was_employed:
            person.salary = 0
        # else: salary > 0 on a job-less person is the M2 pension; keep it
    return a


# ------------------------------------------------------------- A bridges

def create_apprenticeship_assignment(env, person, role_id, employer_id):
    """§Education cross-ref (A bridge, frozen contract): B's enrollment
    resolve creates the joint paid apprenticeship assignment directly —
    no offer/pending flow (the program, not an employer offer, places the
    player). B's bridge call carries no rng, so the wage quote is the
    role's published pay_min, snapshotted into the assignment like any
    accepted terms. One wage payer: apply_adapter syncs M2 payroll."""
    roles, employers = data()
    # R5: validate ID types before any lookup or mutation
    for key, value in (("role_id", role_id), ("employer_id", employer_id)):
        if not isinstance(value, str) or isinstance(value, bool) or not value:
            raise ValueError(f"{key} must be a non-empty string")
    role = roles.get(role_id)
    if role is None or employer_id not in employers \
            or role_id not in employers[employer_id]["role_ids"]:
        raise ValueError(f"unknown apprenticeship: {role_id} at {employer_id}")
    st = ensure(env)
    if active_assignment(env, person.person_id):
        raise PermissionError("you already have a job — an apprenticeship "
                              "is a joint commitment, resign first")
    asn = {"assignment_id": f"ca_{st['next_num']}",
           "role_id": role_id, "employer_id": employer_id,
           "salary": role["pay_min"], "hours": role["base_hours"],
           "accepted_turn": _attempt_bucket(env), "accepted_age": person.age,
           "role_tenure": 0, "status": "active", "apprenticeship": True}
    st["next_num"] += 1
    st["assignments"][person.person_id] = asn
    apply_adapter(env, person)
    return asn


def close_apprenticeship(env, person, reason):
    """Close the joint apprenticeship assignment (B's leave path).
    Tolerant no-op when no active assignment exists."""
    st = ensure(env)
    a = active_assignment(env, person.person_id)
    if a is None:
        return None
    _close_assignment(st, person.person_id, a, reason, _attempt_bucket(env))
    del st["assignments"][person.person_id]
    apply_adapter(env, person)
    return a


# ------------------------------------------------------------- attempts

def _attempt_bucket(env):
    return env.get("turn", {}).get("turn_id") or f"rev{env['revision']}"


def _attempts(env, person_id):
    st = ensure(env)
    return st["attempts"].setdefault(_attempt_bucket(env), {}).setdefault(
        person_id, {"apply": 0, "promote": 0})


# ------------------------------------------------------------- commands

def _sample_salary(rng, role):
    return rng.randint(role["pay_min"], role["pay_max"])


def _typed_id(payload, key):
    """R5: IDs are validated before any dictionary lookup — lists,
    objects, booleans and empty values yield the typed 400
    (invalid_input) instead of an unhashable TypeError, with no
    revision/RNG/attempt/financial mutation."""
    value = payload.get(key)
    if not isinstance(value, str) or isinstance(value, bool) or not value:
        raise ValueError(f"{key} must be a non-empty string")
    return value


def _make_pending(offer, roles, employers, firstname):
    return {
        "id": None,  # service assigns d_N
        "kind": "career_offer",
        "event_id": f"career_offer:{offer['offer_id']}",
        "title": f"Job offer: {roles[offer['role_id']]['label']}",
        "text": (f"{employers[offer['employer_id']]['name']} offers "
                 f"{firstname} the {roles[offer['role_id']]['label']} role at "
                 f"${offer['salary']:,} a year."),
        "person_ids": [offer["person_id"]],
        "_offer_id": offer["offer_id"],
        "options": [
            {"id": "accept", "label": "Accept the offer",
             "known_effects": [], "known_costs": [], "enabled": True,
             "reason": None},
            {"id": "decline", "label": "Decline",
             "known_effects": [], "known_costs": [], "enabled": True,
             "reason": None}],
    }


def cmd_apply(env, person, payload, rng, service):
    roles, employers = data()
    # M3c Amendment 5: the advertised `opening` pair takes precedence;
    # the legacy {role_id, employer_id} pair stays accepted (same
    # validation). Types are checked before any lookup or mutation.
    if payload.get("opening") is not None:
        opening = payload.get("opening")
        if not isinstance(opening, str) or isinstance(opening, bool) \
                or opening.count("@") != 1:
            raise ValueError("opening must be a 'role_id@employer_id' "
                             "string")
        role_id, employer_id = opening.split("@")
        role_id = _typed_id({"role_id": role_id}, "role_id")
        employer_id = _typed_id({"employer_id": employer_id},
                                "employer_id")
    else:
        # R5: validate ID types before any lookup or mutation
        role_id = _typed_id(payload, "role_id")
        employer_id = _typed_id(payload, "employer_id")
    role = roles.get(role_id)
    employer = employers.get(employer_id)
    if role is None or employer is None or role_id not in employer["role_ids"]:
        raise ValueError(f"unknown opening: {role_id} at {employer_id}")
    # Amendment 2: ONE shared commitment decision, checked BEFORE
    # attempt counting and salary sampling
    from src.game.features import commitments
    blocked = commitments.refusal(env, person, commitments.JOB)
    if blocked:
        raise PermissionError(blocked)
    st = ensure(env)
    attempts = _attempts(env, person.person_id)
    if attempts["apply"] >= MAX_APPLY_ATTEMPTS:
        raise PermissionError("you can only apply twice per year")
    _, eligible, reason = check_requirements(env, person, role)
    if not eligible:
        raise PermissionError(reason)
    attempts["apply"] += 1
    offer = {"offer_id": f"co_{st['next_num']}", "person_id": person.person_id,
             "role_id": role_id, "employer_id": employer_id,
             "salary": _sample_salary(rng, role), "hours": role["base_hours"],
             "sampled_turn": _attempt_bucket(env), "status": "open"}
    st["next_num"] += 1
    st["offers"][offer["offer_id"]] = offer
    pending = _make_pending(offer, roles, employers, person.firstname)
    return {"events": [], "money_entries": [], "pending_decision": pending}


def cmd_promote(env, person, payload, rng, service):
    roles, employers = data()
    # R5: validate ID type before any lookup or mutation
    role_id = _typed_id(payload, "role_id")
    target = roles.get(role_id)
    current = active_assignment(env, person.person_id)
    if current is None:
        raise PermissionError("you need an active job to seek a promotion")
    if role_id not in roles.get(current["role_id"], {}).get("promotions_to", []):
        raise PermissionError("that role is not the next step from your role")
    attempts = _attempts(env, person.person_id)
    if attempts["promote"] >= MAX_PROMOTE_ATTEMPTS:
        raise PermissionError("you can only seek promotion twice per year")
    # tenure + performance gates (frozen in data)
    role_def = roles[current["role_id"]]
    needed_tenure = role_def.get("promote_after_years")
    if needed_tenure is not None and current.get("role_tenure", 0) < needed_tenure:
        raise PermissionError(
            f"you need {needed_tenure} years in your current role first")
    perf_min = role_def.get("promote_performance_min", 0)
    if person.performance < perf_min:
        raise PermissionError(f"your performance is {person.performance}/100 "
                              f"and this step asks {perf_min}")
    # C2: the employer role_ids are the single pairing authority —
    # internal promotion exists only where the current employer
    # actually offers the target role; otherwise the real reason names
    # the approved employer-change path (resign -> apply -> accept)
    employer = employers.get(current["employer_id"])
    if target["id"] not in (employer or {}).get("role_ids", []):
        raise PermissionError(
            f"{(employer or {}).get('name', current['employer_id'])} has "
            f"no internal promotion to {target['label']} — resign and "
            f"apply there to change employer")
    _, eligible, reason = check_requirements(env, person, target)
    if not eligible:
        raise PermissionError(reason)
    attempts["promote"] += 1
    st = ensure(env)
    offer = {"offer_id": f"co_{st['next_num']}", "person_id": person.person_id,
             "role_id": role_id, "employer_id": current["employer_id"],
             "salary": _sample_salary(rng, target), "hours": target["base_hours"],
             "sampled_turn": _attempt_bucket(env), "status": "open",
             "promotion_from": current["assignment_id"]}
    st["next_num"] += 1
    st["offers"][offer["offer_id"]] = offer
    pending = _make_pending(offer, roles, employers, person.firstname)
    pending["title"] = f"Promotion: {target['label']}"
    return {"events": [], "money_entries": [], "pending_decision": pending}


def resolve_offer(env, person, pending, option_id):
    """Accept/decline an open career offer (called from _cmd_resolve).
    Applies the assignment + adapter atomically with the resolve."""
    roles, employers = data()
    st = ensure(env)
    offer = st["offers"].get(pending.get("_offer_id"))
    if offer is None or offer["status"] != "open":
        raise PermissionError("this offer is no longer available")
    if option_id == "decline":
        offer["status"] = "declined"
        return {"events": [{
            "kind": "work",
            "text": f"I turned down the {roles[offer['role_id']]['label']} "
                    f"offer at {employers[offer['employer_id']]['name']}.",
            "person_ids": [person.person_id], "changes": {}}]}
    if option_id != "accept":
        raise PermissionError("unknown option")
    offer["status"] = "accepted"
    offer["decision_id"] = pending["id"]
    current = active_assignment(env, person.person_id)
    bucket = _attempt_bucket(env)
    if current is not None:
        _close_assignment(st, person.person_id, current,
                          "promoted" if pending["title"].startswith(
                              "Promotion") else "left", bucket)
    st["assignments"][person.person_id] = {
        "assignment_id": f"ca_{st['next_num']}",
        "role_id": offer["role_id"], "employer_id": offer["employer_id"],
        "salary": offer["salary"], "hours": offer["hours"],
        "accepted_turn": bucket, "accepted_age": person.age,
        "role_tenure": 0, "status": "active"}
    st["next_num"] += 1
    apply_adapter(env, person)
    return {"events": [{
        "kind": "work",
        "text": f"I started as {roles[offer['role_id']]['label']} at "
                f"{employers[offer['employer_id']]['name']}.",
        "person_ids": [person.person_id], "changes": {}}]}


def cmd_resign(env, person, payload, rng, service):
    roles, employers = data()
    a = active_assignment(env, person.person_id)
    if a is None:
        raise PermissionError("you do not have a job to resign from")
    st = ensure(env)
    _close_assignment(st, person.person_id, a, "left",
                      _attempt_bucket(env))
    del st["assignments"][person.person_id]
    apply_adapter(env, person)
    return {"events": [{
        "kind": "work",
        "text": f"I left my job as {roles[a['role_id']]['label']}.",
        "person_ids": [person.person_id], "changes": {}}],
        "money_entries": [], "pending_decision": None}


def dismiss(env, person, reason="dismissed"):
    """§Careers careers_dismiss (internal/event-driven only): close the
    active assignment + adapter clear. Free recovery actions (apply,
    resign-free state, age) remain legal."""
    st = ensure(env)
    a = active_assignment(env, person.person_id)
    if a is None:
        return None
    _close_assignment(st, person.person_id, a, reason,
                      _attempt_bucket(env))
    del st["assignments"][person.person_id]
    apply_adapter(env, person)
    return a


def reconcile_legacy_transition(env, person):
    """M3b: M2 payroll is the employment authority and can end a job
    through paths careers never sees (quit_job, performance firing,
    retirement). Reconcile the named assignment with the legacy state,
    exactly once, idempotently:

    - active assignment + has_job False + salary > 0  -> retired
      (the pension keeps paying through M2's retired-account path)
    - active assignment + has_job False + salary == 0 -> dismissed

    No-op when consistent (no assignment, or still employed). Returns a
    journal event dict when a reconciliation happened, else None."""
    a = active_assignment(env, person.person_id)
    if a is None or person.has_job:
        return None
    reason = "retired" if getattr(person, "salary", 0) > 0 else "dismissed"
    closed = dismiss(env, person, reason)
    if closed is None:
        return None
    roles, _ = data()
    label = roles.get(closed["role_id"], {}).get("label",
                                                 closed["role_id"])
    if reason == "retired":
        text = (f"I retired from my role as {label}; my pension keeps "
                f"paying every year.")
    else:
        text = f"My named position as {label} ended with my employment."
    return {"kind": "work", "text": text,
            "person_ids": [person.person_id], "changes": {}}


def apply_action(env, person, action, payload, rng, service):
    if action == "careers_apply":
        return cmd_apply(env, person, payload, rng, service)
    if action == "careers_promote":
        return cmd_promote(env, person, payload, rng, service)
    if action == "careers_resign":
        return cmd_resign(env, person, payload, rng, service)
    raise ValueError(f"unknown careers action {action}")


def action_specs(env, person):
    """Advertised careers specs (single source with dispatch). Inputs are
    choice selects per the frozen input dialect — clients (and the robot)
    pick real advertised values instead of free-typing ids."""
    roles, employers = data()
    specs = []
    a = active_assignment(env, person.person_id)
    has_job = a is not None
    from src.game.features import commitments
    _apply_block = commitments.refusal(env, person, commitments.JOB)
    # M3c Amendment 5: paired openings — the advertised choice lists
    # only valid role@employer combinations (derived from employer
    # role_ids), so clients/robots can never pick an invalid pair
    openings = sorted(
        f"{rid}@{eid}"
        for eid, employer in employers.items()
        for rid in employer["role_ids"])
    specs.append({"id": "careers_apply",
                  "label": "Apply for a job",
                  "category": "work",
                  "inputs": [{"id": "opening", "type": "choice",
                              "required": True, "choices": openings}],
                  "known_costs": [], "limits": ["2 applications per year"],
                  # Amendment 2: advertised == dispatched — the shared
                  # commitment decision is the single enabled/reason source
                  "enabled": _apply_block is None,
                  "reason": _apply_block})
    if has_job:
        offered = employers.get(a["employer_id"], {}).get("role_ids", [])
        promo_choices = [r for r in roles.get(
            a["role_id"], {}).get("promotions_to", [])
            if r in offered]
        all_steps = list(roles.get(a["role_id"], {}).get("promotions_to",
                                                         []))
    else:
        promo_choices = []
        all_steps = []
    if has_job and all_steps and not promo_choices:
        promo_reason = ("no internal promotion here — change employer "
                        "to advance (resign and apply)")
    elif not has_job:
        promo_reason = "you need an active job"
    elif not all_steps:
        promo_reason = "no promotion path from your current role"
    else:
        promo_reason = None
    specs.append({"id": "careers_promote",
                  "label": "Seek a promotion",
                  "category": "work",
                  "inputs": [{"id": "role_id", "type": "choice",
                              "required": True, "choices": promo_choices}],
                  "known_costs": [], "limits": ["2 attempts per year"],
                  "enabled": has_job and bool(promo_choices),
                  "reason": promo_reason})
    specs.append({"id": "careers_resign",
                  "label": "Resign",
                  "category": "work",
                  "inputs": [],
                  "known_costs": [], "limits": [],
                  "enabled": has_job,
                  "reason": None if has_job else "you do not have a job"})
    return specs


# ------------------------------------------------------------- annual hook

def annual_hook(env, service, rng):
    """Runs inside run_turn right AFTER the work_income phase is
    journaled: M3b reconciliation first (legacy quit/firing/retirement
    ends the named assignment exactly once), then +1 role tenure for the
    assignments that are still active. Payroll legs are posted by M2
    work_income from the adapter scalars — never here."""
    person = env["player"]
    event = reconcile_legacy_transition(env, person)
    if event is not None:
        service._journal(env, event["kind"], event["text"],
                         event["person_ids"], event["changes"])
    st = ensure(env)
    for a in st["assignments"].values():
        if a.get("status") == "active":
            a["role_tenure"] = a.get("role_tenure", 0) + 1


# ------------------------------------------------------------- projection

def careers_view(env, person):
    roles, employers = data()
    st = ensure(env)
    a = active_assignment(env, person.person_id)
    assignment = None
    if a is not None:
        role = roles.get(a["role_id"], {})
        employer = employers.get(a["employer_id"], {})
        promotion = next(spec for spec in action_specs(env, person)
                         if spec["id"] == "careers_promote")
        assignment = {
            "assignment_id": a["assignment_id"],
            "role_id": a["role_id"], "role_label": role.get("label"),
            "employer_id": a["employer_id"],
            "employer_name": employer.get("name"),
            "salary": a["salary"], "hours": a["hours"],
            "role_tenure": a.get("role_tenure", 0),
            "performance": person.performance,
            "status": a["status"],
            "look_override": role.get("look_override"),
            "promotions_to": list(promotion["inputs"][0]["choices"]),
            "promotion_reason": promotion["reason"]}
    history = []
    for past in st["past_assignments"].get(person.person_id, []):
        history.append({
            "role_id": past["role_id"],
            "role_label": roles.get(past["role_id"], {}).get("label"),
            "employer_id": past["employer_id"],
            "employer_name": employers.get(past["employer_id"], {}).get("name"),
            "salary": past["salary"],
            "role_tenure": past.get("role_tenure", 0),
            "from_turn": past.get("accepted_turn"),
            "to_turn": past.get("to_turn"),
            "reason": past.get("status")})
    return {"assignment": assignment,
            "openings": openings_view(env, person),
            "history": history}


load_data()
