"""M3 journey manifest (Kimi D, QA) — the spec's playable journeys as data.

Run by tools/m3_journeys.py. Each step function receives a JourneyCtx and
returns (status, detail) where status is PASS, FAIL, PENDING or SKIP:

  PENDING(owner, why)  — the step's contract surface does not exist yet
       (unknown action id, missing projection key, missing scenario).
       This is the DEFAULT honest state for A/B/C items whose schemas are
       not FROZEN/implemented. It never counts as a pass.
  FAIL                 — the contract surface EXISTS and behavior is wrong.
  SKIP                 — not applicable for this journey/run.

Honesty rules (Codex consolidated review R6, 2026-11-21):
- probes read the FROZEN surface: state["education_programs"] (Amendment 6)
  for the M3 education feature view; the Occupation (job) screen for the
  Education/Careers mounts; Activities for Skills.
- rejection checks assert the ACTUAL error code and reason: a PASS requires
  code "not_allowed" plus an eligibility-dimension keyword, never an
  unrelated 409 (e.g. a pending_decision block) and never any refusal.
- pendings are drained with a BOUND before probes; chained decisions from
  one age_up are resolved one by one.
- M3a steps require genuine behavior: real enrollment/funding resolve,
  real per-year progress to completion, reload persistence, and a real
  training -> eligibility change. Missing implementations stay PENDING
  with the owner named; they are never counted as PASS.
"""

import json

# Words that identify a genuine eligibility refusal (server-computed
# gate), as opposed to an unrelated conflict or a stale pending.
GATE_WORDS = ("skill", "qualification", "age", "certificate", "diploma",
              "degree", "secondary", "require", "prerequisite", "eligible",
              "enrolled", "enrollment", "assignment", "job", "career",
              "performance", "years")


def _error_body(r):
    body = r.get("_body")
    if isinstance(body, bytes):
        body = body.decode("utf-8", "replace")
    try:
        return json.loads(body).get("error", {})
    except Exception:
        return {}


def _error_code(r):
    return _error_body(r).get("code") or ""


def _typed_reason(r):
    return _error_body(r).get("message") or (r.get("_body") if not
                                             isinstance(r.get("_body"),
                                                        bytes) else "")


def _is_eligibility_refusal(r):
    """True only for a genuine server-computed eligibility gate."""
    return (r.get("_status") == 409
            and _error_code(r) == "not_allowed"
            and any(w in _typed_reason(r).lower() for w in GATE_WORDS))


def drain_pendings(ctx, bound=8):
    """Resolve chained pending decisions one by one, with a bound.
    Returns (ok, detail)."""
    resolved = 0
    for _ in range(bound):
        st = ctx.state()
        if not st.get("pending_decision"):
            return True, "drained (%d resolved)" % resolved
        d = st["pending_decision"]
        opts = [o for o in d.get("options", []) if not o.get("disabled")] \
            or d.get("options", [])
        if not opts:
            return False, "pending with no options"
        pick = next((o for o in opts if not o.get("known_costs")), opts[0])
        r = ctx.api("resolve_decision",
                    {"decision_id": d["id"], "option_id": pick["id"],
                     "inputs": {}})
        if r.get("game_id"):
            resolved += 1
            continue
        return False, "resolve %s (event %s, option %s costs %s) failed: " \
                      "%s" % (d.get("kind"), d.get("event_id"),
                              pick.get("id"), pick.get("known_costs"),
                              r.get("_body"))
    st = ctx.state()
    if st.get("pending_decision"):
        return False, "pending chain exceeded bound %d" % bound
    return True, "drained (%d resolved)" % resolved


def _resolve_funding(ctx):
    """Resolve an education_funding pending, preferring student_loan."""
    st = ctx.state()
    d = st.get("pending_decision") or {}
    if d.get("kind") != "education_funding":
        return False, "no education_funding pending (kind=%s)" % d.get("kind")
    enabled = [o for o in d.get("options", []) if not o.get("disabled")]
    if not enabled:
        return False, "education_funding pending with no enabled options"
    pick = next((o for o in enabled if o.get("id") == "student_loan"),
                enabled[0])
    r = ctx.api("resolve_decision",
                {"decision_id": d["id"], "option_id": pick["id"],
                 "inputs": {}})
    if r.get("game_id"):
        return True, "funding=%s" % pick["id"]
    return False, "resolve failed: %s" % r.get("_body")


def _require_drained(ctx):
    ok, detail = drain_pendings(ctx)
    return None if ok else ("FAIL", "pending chain: " + detail)


# ---------------------------------------------------------------------------
# Baseline steps (M2 surface)
# ---------------------------------------------------------------------------

def step_finish_school(ctx):
    """Baseline: graduate secondary school with a real life (M2 surface)."""
    st = ctx.state()
    age = ctx.player_age(st)
    if age >= 18:
        return "PASS", "already graduated (age %s)" % age
    for _ in range(18 - age):
        st = ctx.state()
        if st.get("pending_decision"):
            ok, detail = ctx.resolve_pending_any()
            if not ok:
                return "FAIL", "could not resolve pending: " + detail
            continue
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up rejected: %s" % r.get("_body")
    st = ctx.state()
    return ("PASS", "graduated at %s" % ctx.player_age(st)) \
        if ctx.player_age(st) >= 18 else \
        ("FAIL", "still %s after aging" % ctx.player_age(st))


def step_open_work_tab(ctx):
    """Baseline: Work/Occupation screen reachable at both widths; the M3
    careers module host is present on the frozen Occupation screen."""
    out = []
    for w, tag in ((390, "390"), (320, "320")):
        s = ctx.open_page(w)
        ok = s.js(
            "(() => { const b = [...document.querySelectorAll("
            "'.v2-tabbar button')].find(x => /occupation|work/i"
            ".test(x.textContent)); if (!b) return false; b.click();"
            " return true; })()")
        if not ok:
            return "FAIL", "no Occupation/Work tab at %s" % tag
        ctx.settle(s, 800)
        ctx.shot(s, "journey-%s-work-tab-%s" % (ctx.journey_id, tag))
        out.append(tag)
    return "PASS", "work tab at %s" % "/".join(out)


def _click_tab(s, name):
    return s.js(
        "(() => { const b = [...document.querySelectorAll("
        "'.v2-tabbar button')].find(x => /%s/i.test(x.textContent));"
        " if (!b) return false; b.click(); return true; })()" % name)


def _education_programs(st):
    return st.get("education_programs") or {}


# ---------------------------------------------------------------------------
# J1 — Electrical trade (spec 01: Apprentice → Electrician → Senior
# electrician → Site supervisor; nondegree route stays viable)
# ---------------------------------------------------------------------------

def j1_enroll_apprenticeship(ctx):
    """Enroll in the paid electrical apprenticeship program."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    r = ctx.api("education_enroll", {"program_id": "app_electrical"})

    def on_ok():
        st = ctx.state()
        edu = _education_programs(st)
        pending = st.get("pending_decision") or {}
        if edu.get("enrollment") or \
                pending.get("kind") == "education_funding":
            return "PASS", "enroll flow started (kind=%s)" % pending.get(
                "kind")
        return "FAIL", "education_enroll accepted but no enrollment or " \
                       "education_funding pending under " \
                       "state.education_programs"

    if r.get("_status") == 409 and _error_code(r) == "not_allowed":
        reason = _typed_reason(r)
        if "placements" in reason.lower():
            return "PENDING", "A: apprenticeship bridge not open yet " \
                              "(typed stage gate: %s)" % reason[:100]
        return "FAIL", "eligible graduate refused: %s" % reason[:150]
    return ctx.pending_or_run(r, owner="B",
                              want="education_enroll command + programs "
                                   "projection",
                              on_ok=on_ok)


def _required_skill(st, role_id, skill_id):
    """The server's published skill requirement for a role, or None."""
    openings = (st.get("careers") or {}).get("openings") or []
    role = next((o for o in openings if o.get("role_id") == role_id), None)
    if not role:
        return None
    for req in (role.get("requirements") or {}).get("skills") or []:
        if req.get("id") == skill_id:
            return req.get("required")
    return None


def j1_apprentice_assignment(ctx):
    """Paid apprentice assignment at a named employer. Primary path (M3a
    bridge): the apprenticeship enrollment itself must have created the
    joint assignment — exactly one commitment, one wage source. Fallback:
    with no enrollment, train practical (bounded) until the server's
    published gate is met and careers_apply must store a real offer."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    assignment = (st.get("careers") or {}).get("assignment") or {}
    if assignment.get("status") == "active":
        if assignment.get("role_id") != "app_electrician":
            return "FAIL", "expected the joint apprentice assignment, got " \
                           "role %r" % assignment.get("role_id")
        if not assignment.get("employer_id"):
            return "FAIL", "apprentice assignment has no named employer"
        return "PASS", "joint apprentice assignment active at %s (one " \
                       "commitment via the apprenticeship bridge)" % \
                       assignment.get("employer_id")
    # fallback: no enrollment/assignment — train up to the published gate
    need = _required_skill(st, "app_electrician", "practical")
    if need is None:
        return "PENDING", "A: careers projection has no app_electrician " \
                          "opening with a published practical requirement"
    for _year in range(4):
        ok, _ = drain_pendings(ctx)
        if not ok:
            return "FAIL", "pending chain while training"
        st = ctx.state()
        have = (_required_skill_value(st, "practical"))
        if have is None:
            return "PENDING", "C/A: skills projection values not exposed"
        if have >= need:
            r = ctx.api("careers_apply",
                        {"role_id": "app_electrician",
                         "employer_id": "voltworks"})
            st = ctx.state()
            pending = st.get("pending_decision") or {}
            if pending.get("kind") == "career_offer":
                return "PASS", "training unlocked eligibility: stored offer " \
                               "at practical %s/%s" % (have, need)
            if r.get("game_id") and pending.get("kind") != "career_offer":
                return "FAIL", "careers_apply accepted but no career_offer " \
                               "pending"
            if _is_eligibility_refusal(r):
                continue  # keep training; gate still enforced
            return "FAIL", "careers_apply refused for a non-eligibility " \
                           "reason: %s %s" % (_error_code(r),
                                              _typed_reason(r)[:120])
        # below the gate: train practical (max 2 uses/year per frozen data)
        for _ in range(2):
            st = ctx.state()
            spec = next((t for t in (st.get("skills") or {}).get(
                             "training") or []
                         if (t.get("target_id") or t.get("id"))
                         == "train_practical" and t.get("enabled")), None)
            if not spec:
                break
            r = ctx.api("train_skill", {"target_id": "train_practical"})
            if r.get("_status"):
                return "FAIL", "advertised enabled train_practical refused " \
                               "at practical %s/%s: %s %s" % (
                                   have, need, _error_code(r),
                                   _typed_reason(r)[:100])
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up rejected: %s" % r.get("_body")
    return "FAIL", "bounded training (4 years) never reached the published " \
                   "practical gate (%s) — training->eligibility broken" % need


def _required_skill_value(st, skill_id):
    values = (st.get("skills") or {}).get("values") or {}
    return values.get(skill_id)


def j1_apprentice_wage(ctx):
    """Apprentice year pays exactly one wage through the frozen ledger:
    one income leg sourced from work_income:salary in the latest turn,
    and no second wage-source leg."""
    ok, detail = drain_pendings(ctx)
    if not ok:
        return "FAIL", "pending chain: " + detail
    st = ctx.state()
    money0 = st.get("money")
    r = ctx.api("age_up", {})
    if not r.get("game_id"):
        return "FAIL", "age_up rejected: %s" % r.get("_body")
    st1 = ctx.state()
    delta = (st1.get("money") or 0) - (money0 or 0)
    legs = ctx.ledger_legs(st1, categories=("wage", "salary", "income"))
    if legs is None:
        return "PENDING", "A: ledger projection has no per-leg breakdown " \
                          "(needs frozen finance projection contract)"
    last_turn = max((l.get("turn_id") for l in legs), default=None)
    if not last_turn:
        if delta == 0:
            return "PENDING", "A: no wage legs posted for the apprentice " \
                              "year (apprentice wage not implemented yet)"
        return "FAIL", "money changed (%s) but no income leg recorded" % delta
    year_legs = [l for l in legs if l.get("turn_id") == last_turn]
    wage_legs = [l for l in year_legs
                 if "salary" in str(l.get("source_key", ""))]
    other_income = [l for l in year_legs
                    if "salary" not in str(l.get("source_key", ""))]
    if len(wage_legs) != 1:
        return "FAIL", "expected exactly 1 payroll-sourced income leg in " \
                       "%s, got %d (delta %s)" % (
                           last_turn, len(wage_legs), delta)
    if other_income:
        return "FAIL", "unexpected second income source in %s: %s" % (
            last_turn, [l.get("description") for l in other_income])
    return "PASS", "one payroll income leg in %s, delta %s" % (last_turn,
                                                               delta)


def j1_promotion(ctx):
    """Promotion: apprentice -> electrician (published requirements)."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    r = ctx.api("careers_promote", {"role_id": "electrician"})
    if r.get("game_id"):
        st = ctx.state()
        pending = st.get("pending_decision") or {}
        if pending.get("kind") == "career_offer":
            return "PASS", "promotion offer sampled and stored"
        return "FAIL", "careers_promote accepted but no career_offer pending"
    if _is_eligibility_refusal(r):
        return "PASS", "promotion gate enforced: %s" % \
                       _typed_reason(r)[:120]
    return "FAIL", "careers_promote unexpected refusal: %s %s" % (
        _error_code(r), _typed_reason(r)[:120])


def j1_dismissal_recovery(ctx):
    """Resignation path exists (dismissal is event-driven, A-internal)."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    r = ctx.api("careers_resign", {})
    if r.get("game_id"):
        st = ctx.state()
        if (st.get("careers") or {}).get("assignment"):
            return "FAIL", "resign accepted but assignment still active"
        return "PASS", "resigned; assignment cleared"
    if r.get("_status") == 409 and _error_code(r) == "not_allowed":
        return "PASS", "typed refusal (no active assignment): %s" % \
                       _typed_reason(r)[:100]
    return "FAIL", "careers_resign unexpected: %s %s" % (
        _error_code(r), _typed_reason(r)[:120])


# ---------------------------------------------------------------------------
# J2 — Healthcare (Care assistant -> Qualified care worker -> Nurse ->
# Senior nurse; diploma/bachelor-gated) + Education UI + completion/reload
# ---------------------------------------------------------------------------

def j2_education_ui(ctx):
    """Education UI on Occupation: frozen programs listed with the server's
    own eligibility reasons, at 390 + 320. Probes use labels/reasons from
    the live education_programs projection, never hardcoded guesses."""
    st = ctx.state()
    programs = _education_programs(st).get("programs") or []
    if not programs:
        return "PENDING", "A/B: education projection (state." \
                          "education_programs.programs) not exposed yet"
    inel = next((p for p in programs if not p.get("eligible") and
                 p.get("reason")), None)
    out = []
    for w, tag in ((390, "390"), (320, "320")):
        s = ctx.open_page(w)
        if not _click_tab(s, "occupation|work"):
            return "FAIL", "no Occupation/Work tab at %s" % tag
        ctx.settle(s, 800)
        mounted = s.js(
            "(() => { const h = document.getElementById('v2EducationMount');"
            " return !!h && !h.hidden && h.children.length > 0; })()")
        label = programs[0].get("label") or programs[0].get("program_id")
        if not mounted:
            if s.js("document.body.textContent.includes(%s)"
                    % json.dumps(label)):
                return "FAIL", "education content rendered outside the " \
                               "frozen #v2EducationMount host at %s" % tag
            return "PENDING", "B: Education UI not mounted on Occupation at " \
                              "%s (#v2EducationMount empty)" % tag
        if not s.js("document.body.textContent.includes(%s)"
                    % json.dumps(label)):
            return "FAIL", "Education UI mounted but frozen program label " \
                           "%r missing at %s" % (label, tag)
        if inel and not s.js(
                "document.body.textContent.toLowerCase().includes(%s)"
                % json.dumps(inel["reason"].lower())):
            return "FAIL", "server reason %r for ineligible program %r " \
                           "not shown at %s" % (
                               inel["reason"], inel.get("program_id"), tag)
        ctx.shot(s, "journey-%s-education-ui-%s" % (ctx.journey_id, tag))
        out.append(tag)
    detail = "programs listed at %s" % "/".join(out)
    if inel:
        detail += "; ineligibility reason visible (%s: %s)" % (
            inel.get("program_id"), inel["reason"])
    return "PASS", detail


def j2_education_ui_enroll(ctx):
    """UI-driven enroll: click the ENABLED Enroll control belonging to the
    intended program row; a real enrollment or education_funding pending
    must follow."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    programs = _education_programs(st).get("programs") or []
    if not programs:
        return "PENDING", "A/B: education projection not exposed yet"
    if _education_programs(st).get("enrollment"):
        return "SKIP", "already enrolled (education_progress covers flow)"
    target = next((p for p in programs
                   if p.get("program_id") == "care_diploma"
                   and p.get("eligible")), None) or \
        next((p for p in programs if p.get("eligible")), None)
    if not target:
        return "SKIP", "no eligible program to enroll via UI this life"
    s = ctx.open_page(390)
    if not _click_tab(s, "occupation|work"):
        return "FAIL", "no Occupation/Work tab"
    ctx.settle(s, 800)
    label = target.get("label") or target.get("program_id")
    if not s.js(
            "(() => { const h = document.getElementById('v2EducationMount');"
            " return !!h && !h.hidden && h.children.length > 0; })()"):
        return "PENDING", "B: Education UI not mounted on Occupation"
    clicked = s.js(
        "(() => { const rows = [...document.querySelectorAll("
        "'.v2ed-program-row')]; const row = rows.find(r => (r.textContent "
        "|| '').includes(%s)); if (!row) return 'no-row';"
        " const b = row.querySelector('button.v2ed-enroll');"
        " if (!b) return 'no-button';"
        " if (b.disabled || b.getAttribute('aria-disabled') === 'true')"
        " return 'disabled'; b.click(); return 'clicked'; })()"
        % json.dumps(label))
    if clicked == "no-row":
        return "PENDING", "B: program row %r not rendered (stale UI?)" % label
    if clicked == "disabled":
        return "FAIL", "UI enroll disabled for projection-advertised " \
                       "eligible program %r (advertised != UI)" % label
    if clicked == "no-button":
        return "FAIL", "program row %r has no enroll control" % label
    for _ in range(20):
        ctx.settle(s, 250)
        st1 = ctx.state()
        d = st1.get("pending_decision") or {}
        e1 = _education_programs(st1)
        if d.get("kind") == "education_funding" or e1.get("enrollment"):
            ctx.shot(s, "journey-%s-education-ui-enroll-390"
                     % ctx.journey_id)
            return "PASS", "UI enroll started (pending=%s, enrollment=%s)" \
                           % (d.get("kind"), bool(e1.get("enrollment")))
    return "FAIL", "clicked UI enroll for %r but no enrollment or " \
                   "education_funding pending appeared" % label


def j2_education_progress(ctx):
    """Genuine completion: study each year and age through the whole
    program; progress must advance yearly, the credential is awarded
    exactly once at completion, and the journal/projection agree."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    edu = _education_programs(st)
    if not edu.get("programs"):
        return "PENDING", "A/B: education projection not exposed yet"
    if not edu.get("enrollment"):
        r = ctx.api("education_enroll", {"program_id": "care_diploma"})
        if r.get("_status"):
            if _is_eligibility_refusal(r):
                return "SKIP", "care_diploma not eligible this life: %s" % \
                               _typed_reason(r)[:100]
            return "FAIL", "education_enroll care_diploma: %s %s" % (
                _error_code(r), _typed_reason(r)[:120])
        ok, detail = _resolve_funding(ctx)
        if not ok:
            drain_pendings(ctx)
            if not _education_programs(ctx.state()).get("enrollment"):
                return "FAIL", "enroll flow produced no enrollment " \
                               "(funding: %s)" % detail
    st = ctx.state()
    edu = _education_programs(st)
    enr = edu.get("enrollment")
    if not enr:
        return "FAIL", "no active enrollment after enroll flow"
    duration = enr.get("duration_years")
    program_id = enr.get("program_id")
    enrollment_id = enr.get("enrollment_id")
    quals0 = [q.get("qualification_id")
              for q in edu.get("qualifications") or []]
    progress0 = enr.get("progress") or 0
    for year in range(duration - progress0):
        bad = _require_drained(ctx)
        if bad:
            return bad
        r = ctx.api("education_study", {})
        if not r.get("game_id"):
            return "FAIL", "education_study refused with active " \
                           "enrollment: %s %s" % (
                               _error_code(r), _typed_reason(r)[:120])
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused mid-program: %s" % r.get("_body")
        st = ctx.state()
        enr = _education_programs(st).get("enrollment") or {}
        expected = progress0 + year + 1
        if enr.get("status") == "active" and \
                (enr.get("progress") or 0) != expected:
            return "FAIL", "progress stuck at %s (expected %s) after " \
                           "year %d" % (enr.get("progress"), expected,
                                        year + 1)
    st = ctx.state()
    edu = _education_programs(st)
    quals1 = [q.get("qualification_id")
              for q in edu.get("qualifications") or []]
    new_quals = [q for q in quals1 if q not in quals0]
    # the enrollment view projects only the ACTIVE enrollment, so a
    # completed program is verified through the immutable qualification
    # (awarded exactly once) plus the history row for this enrollment.
    hist = [h for h in edu.get("history") or []
            if h.get("enrollment_id") == enrollment_id]
    if len(new_quals) != 1:
        return "FAIL", "expected exactly one new qualification, got %s" % (
            new_quals)
    if not hist or hist[-1].get("status") != "completed":
        return "FAIL", "no completed history row for enrollment %s (got %s)" \
                       % (enrollment_id,
                          [h.get("status") for h in hist])
    prog = next((p for p in edu.get("programs") or []
                 if p.get("program_id") == program_id), {})
    want = prog.get("credential_id")
    if want and new_quals[0] != want:
        return "FAIL", "awarded %s, program publishes %s" % (new_quals[0],
                                                             want)
    return "PASS", "completed %s: %s awarded once, history row completed " \
                   "(%s/%s years)" % (
                       program_id, new_quals[0], hist[-1].get("progress"),
                       duration)


def j2_reload_persistence(ctx):
    """Reload: a fresh page load must still show the earned qualification
    (server-persisted education_programs projection through the UI)."""
    st = ctx.state()
    quals = _education_programs(st).get("qualifications") or []
    if not quals:
        return "SKIP", "no qualification earned yet (education_progress " \
                       "covers awarding)"
    label = quals[-1].get("label") or quals[-1].get("qualification_id")
    s = ctx.open_page(390)
    if not _click_tab(s, "occupation|work"):
        return "FAIL", "no Occupation/Work tab"
    ctx.settle(s, 800)
    if not s.js("document.body.textContent.includes(%s)"
                % json.dumps(str(label))):
        return "FAIL", "qualification %r missing from UI after fresh " \
                       "page load (reload persistence broken)" % label
    ctx.shot(s, "journey-%s-education-reload-390" % ctx.journey_id)
    return "PASS", "qualification %r persisted across reload" % label


def j2_nurse_role_gated(ctx):
    """Nurse role requires the qualification (gate enforced server-side
    with the exact missing-credential reason, not a pending conflict)."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    careers = st.get("careers") or {}
    openings = careers.get("openings") or []
    nurse = next((o for o in openings if o.get("role_id") == "nurse"), None)
    if not nurse:
        return "PENDING", "A: careers projection has no openings list yet"
    r = ctx.api("careers_apply",
                {"role_id": "nurse",
                 "employer_id": nurse.get("employer_id")})
    if r.get("game_id"):
        has_degree = any(
            q.get("qualification_id") == "nursing_degree"
            for q in _education_programs(ctx.state()).get("qualifications")
            or [])
        if has_degree:
            st1 = ctx.state()
            if (st1.get("pending_decision") or {}).get("kind") == \
                    "career_offer":
                return "PASS", "nurse apply accepted with nursing_degree " \
                               "(offer pending)"
            return "FAIL", "nurse apply accepted with degree but no " \
                           "career_offer pending"
        return "FAIL", "ungated apply accepted without nursing_degree"
    if r.get("_status") == 409 and _error_code(r) == "not_allowed" \
            and ("nursing_degree" in _typed_reason(r)
                 or "bachelor of nursing" in _typed_reason(r).lower()):
        return "PASS", "qualification gate enforced: %s" % \
                       _typed_reason(r)[:120]
    return "FAIL", "nurse gate wrong: %s %s" % (
        _error_code(r), _typed_reason(r)[:150])


# ---------------------------------------------------------------------------
# J3 — Skills (C): train a skill, bounded annual usage, real eligibility
# ---------------------------------------------------------------------------

def j3_train_skill(ctx):
    bad = _require_drained(ctx)
    if bad:
        return bad
    st0 = ctx.state()
    sk0 = st0.get("skills") or {}
    uses0 = dict(sk0.get("uses_this_year") or {})
    values0 = dict(sk0.get("values") or {})
    r = ctx.api("train_skill", {"target_id": "train_practical"})

    def on_ok():
        st = ctx.state()
        sk1 = st.get("skills") or {}
        uses1 = sk1.get("uses_this_year") or {}
        values1 = sk1.get("values") or {}
        changed = uses1.get("train_practical", 0) > \
            uses0.get("train_practical", 0) or \
            values1.get("practical", 0) > values0.get("practical", 0)
        if changed:
            # This always-run step must prove a gameplay gate changed;
            # the apprenticeship journey's fallback is normally skipped.
            value0 = values0.get("practical", 0)
            value1 = values1.get("practical", 0)
            before_openings = {
                (o.get("role_id"), o.get("employer_id")): o
                for o in (st0.get("careers") or {}).get("openings", [])}
            crossed = []
            for opening in (st.get("careers") or {}).get("openings", []):
                before = before_openings.get((opening.get("role_id"),
                                              opening.get("employer_id")))
                if (not before or before.get("eligible")
                        or not opening.get("eligible")):
                    continue
                for req in (opening.get("requirements") or {}).get(
                        "skills", []):
                    if (req.get("id") == "practical"
                            and value0 < req.get("required", 0) <= value1
                            and req.get("met")):
                        crossed.append(opening.get("role_id"))
            if not crossed:
                return "FAIL", "training changed values but did not unlock " \
                               "a published practical requirement"
            return "PASS", "trained practical: uses %s->%s value %s->%s" % (
                uses0.get("train_practical", 0),
                uses1.get("train_practical", 0),
                values0.get("practical"), values1.get("practical"))
        return "FAIL", "train_skill accepted but no uses/value change in " \
                       "state.skills"

    if r.get("_status") == 409:
        return "FAIL", "advertised training refused for an adult life: " \
                       "%s %s" % (_error_code(r), _typed_reason(r)[:120])
    return ctx.pending_or_run(
        r, owner="C", want="skills surface (train_skill command + "
                           "skills projection)",
        on_ok=on_ok)


def j3_skills_ui(ctx):
    """Skills UI on Activities: C's module mounted (.v2sk-scope), the
    server's advertised training options and values rendered."""
    st = ctx.state()
    sk = st.get("skills") or {}
    training = sk.get("training") or []
    values = sk.get("values") or {}
    if not training or not values:
        return "PENDING", "C/A: skills projection (state.skills.training/" \
                          "values) not exposed yet"
    s = ctx.open_page(390)
    if not _click_tab(s, "activities"):
        return "FAIL", "no Activities tab"
    ctx.settle(s, 800)
    if not s.js("!!document.querySelector('.v2sk-scope')"):
        return "PENDING", "B: skills module not mounted on Activities " \
                          "(no .v2sk-scope in DOM)"
    label = training[0].get("label") or training[0].get("id")
    if not s.js("document.body.textContent.includes(%s)"
                % json.dumps(label)):
        return "FAIL", "skills module mounted but advertised option %r " \
                       "not rendered" % label
    ctx.shot(s, "journey-%s-skills-ui-390" % ctx.journey_id)
    return "PASS", "skills UI mounted; %d values, %d training options " \
                   "advertised" % (len(values), len(training))


def j3_skills_ui_train(ctx):
    """UI-driven training: click a free, enabled advertised option; the
    server's uses_this_year / value must actually change."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    sk = st.get("skills") or {}
    training = sk.get("training") or []
    if not training or not sk.get("values"):
        return "PENDING", "C/A: skills projection not exposed yet"
    free = next((t for t in training if t.get("enabled") and
                 not t.get("known_costs")), None)
    if not free:
        return "SKIP", "no enabled free training option advertised this life"
    option_id = free.get("target_id") or free.get("id")
    uses0 = (sk.get("uses_this_year") or {}).get(option_id, 0)
    s = ctx.open_page(390)
    if not _click_tab(s, "activities"):
        return "FAIL", "no Activities tab"
    ctx.settle(s, 800)
    if not s.js("!!document.querySelector('.v2sk-scope')"):
        return "PENDING", "B: skills module not mounted on Activities"
    label = free.get("label") or option_id
    clicked = s.js(
        "(() => { const b = [...document.querySelectorAll("
        "'button.v2sk-train')].find(x => (x.textContent || '').trim()"
        ".startsWith(%s)); if (!b || b.disabled || "
        "b.getAttribute('aria-disabled') === 'true') return false;"
        " b.click(); return true; })()" % json.dumps(label))
    if not clicked:
        return "FAIL", "advertised enabled option %r has no clickable UI " \
                       "control" % label
    for _ in range(20):
        ctx.settle(s, 250)
        sk1 = (ctx.state().get("skills") or {})
        uses1 = (sk1.get("uses_this_year") or {}).get(option_id, 0)
        if uses1 > uses0:
            ctx.shot(s, "journey-%s-skills-ui-train-390" % ctx.journey_id)
            return "PASS", "UI training applied: %s uses %s -> %s" % (
                option_id, uses0, uses1)
    return "FAIL", "clicked UI train for %s but uses_this_year unchanged " \
                   "(%s)" % (option_id, uses0)


# ---------------------------------------------------------------------------
# J4 — $0 start (B): decline funding, stay legal, age freely
# ---------------------------------------------------------------------------

def j4_zero_money_legal(ctx):
    st = ctx.state()
    if (st.get("money") or 0) > 0:
        return "SKIP", "life has money; $0-only assertions not applicable"
    r = ctx.api("age_up", {})
    if r.get("game_id"):
        return "PASS", "aged legally at $0"
    if r.get("_status") == 409:
        return "FAIL", "$0 life cannot age: %s" % r.get("_body")
    return "FAIL", "age_up unexpected: %s" % r.get("_body")


# ---------------------------------------------------------------------------
# J5 — Old-save resume (A migration contract): a real pre-M3 save (M3
# envelope keys stripped — the exact delta the migration contract defines)
# resumes without invented degrees/employers; legacy generic pay is the
# one wage payer until the player transitions through normal actions.
# ---------------------------------------------------------------------------

def j5_strip_and_load(ctx):
    """Convert the journey's save to a pre-M3 legacy save and load it."""
    try:
        ctx.strip_to_legacy_m2()
    except Exception as exc:
        return "FAIL", "legacy strip hook crashed: %r" % exc
    st = ctx.state()
    work = st.get("work") or {}
    if not work.get("employed"):
        return "FAIL", "legacy employment not preserved after resume " \
                       "(state.work: %s)" % json.dumps(work)[:150]
    if (work.get("salary") or 0) <= 0:
        return "FAIL", "legacy salary lost after resume"
    me = next(p for p in st["people"] if p["id"] == st["player_id"])
    return "PASS", "pre-M3 save resumed: age %s, legacy job '%s' at $%s" % (
        me.get("age"), work.get("job_label"), work.get("salary"))


def j5_no_invented_records(ctx):
    """No invented M3 history: no named assignment, no fabricated
    employer/degree records from old pay or university alone."""
    st = ctx.state()
    careers = st.get("careers") or {}
    edu = _education_programs(st)
    if careers.get("assignment"):
        return "FAIL", "invented named assignment for a legacy generic job"
    if careers.get("history"):
        return "FAIL", "invented employer history for a legacy generic job"
    if edu.get("enrollment"):
        return "FAIL", "invented enrollment on a pre-M3 save"
    quals = edu.get("qualifications") or []
    if quals:
        return "FAIL", "invented qualifications on a pre-M3 save: %s" % [
            q.get("qualification_id") for q in quals]
    return "PASS", "no invented assignments/employers/enrollments/degrees"


def j5_legacy_pay_preserved(ctx):
    """Legacy M2 payroll still pays, exactly one wage leg, through the
    frozen ledger."""
    ok, detail = drain_pendings(ctx)
    if not ok:
        return "FAIL", "pending chain: " + detail
    st = ctx.state()
    money0 = st.get("money")
    r = ctx.api("age_up", {})
    if not r.get("game_id"):
        return "FAIL", "age_up rejected on legacy save: %s" % r.get("_body")
    st1 = ctx.state()
    delta = (st1.get("money") or 0) - (money0 or 0)
    legs = ctx.ledger_legs(st1, categories=("wage", "salary", "income"))
    if legs is None:
        return "PENDING", "A: ledger projection has no per-leg breakdown"
    last_turn = max((l.get("turn_id") for l in legs), default=None)
    year_legs = [l for l in legs if l.get("turn_id") == last_turn] \
        if last_turn else []
    wage_legs = [l for l in year_legs
                 if "salary" in str(l.get("source_key", ""))]
    if len(wage_legs) != 1:
        return "FAIL", "legacy year: expected 1 payroll income leg, got %d " \
                       "(delta %s)" % (len(wage_legs), delta)
    return "PASS", "legacy wage paid once (%s, delta %s)" % (last_turn,
                                                             delta)


def j5_legacy_transition_gate(ctx):
    """Amendment 2 approved transition: a legacy-employed life applies for
    a named role through normal careers_apply; the stored offer is legal.
    On accept the one adapter overwrites legacy pay (same employer state,
    still one wage payer)."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    openings = (st.get("careers") or {}).get("openings") or []
    if not openings:
        return "PENDING", "A: careers projection has no openings list yet"
    target = next((o for o in openings if o.get("eligible")), None)
    if not target:
        candidate = _opening_with_earnable_gates(openings)
        if not candidate:
            return "SKIP", "no named opening with earnable gates this life"
        _force_opening_gates(ctx, candidate)
        st = ctx.state()
        target = next(
            (o for o in (st.get("careers") or {}).get("openings") or []
             if o.get("role_id") == candidate.get("role_id")
             and o.get("employer_id") == candidate.get("employer_id")),
            None)
        if not target or not target.get("eligible"):
            return "FAIL", "legacy->named opening not eligible after the " \
                           "bounded fixture (server reason: %s)" % (
                               target and target.get("reason"))
    r = ctx.api("careers_apply",
                {"role_id": target.get("role_id"),
                 "employer_id": target.get("employer_id")})
    if r.get("_status"):
        if _is_eligibility_refusal(r):
            return "FAIL", "Amendment 2 allows legacy->named transition but " \
                           "apply was refused: %s" % _typed_reason(r)[:120]
        return "FAIL", "careers_apply on legacy save: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    st1 = ctx.state()
    d = st1.get("pending_decision") or {}
    if d.get("kind") != "career_offer":
        return "FAIL", "apply accepted but no career_offer pending"
    accept = next((o for o in d.get("options", []) if o.get("id") == "accept"),
                  None)
    if not accept:
        return "FAIL", "career_offer has no accept option"
    r = ctx.api("resolve_decision",
                {"decision_id": d["id"], "option_id": "accept",
                 "inputs": {}})
    if not r.get("game_id"):
        return "FAIL", "offer accept failed: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    st2 = ctx.state()
    assignment = (st2.get("careers") or {}).get("assignment") or {}
    work = st2.get("work") or {}
    if assignment.get("status") != "active":
        return "FAIL", "accept did not activate the named assignment"
    if assignment.get("role_id") != target.get("role_id"):
        return "FAIL", "assigned role %r != offered %r" % (
            assignment.get("role_id"), target.get("role_id"))
    if (work.get("salary") or 0) != (assignment.get("salary") or -1):
        return "FAIL", "adapter did not sync legacy pay: work.salary %s != " \
                       "assignment salary %s" % (
                           work.get("salary"), assignment.get("salary"))
    if _education_programs(st2).get("enrollment"):
        return "FAIL", "transition created a study commitment (one-" \
                       "commitment broken)"
    return "PASS", "legacy->named transition: %s at %s, pay $%s via the " \
                   "one adapter" % (
                       assignment.get("role_id"),
                       assignment.get("employer_id"), work.get("salary"))


# ---------------------------------------------------------------------------
# J6 — M3b careers lifecycle (A): real employer change, engine-triggered
# dismissal + recovery, direct + offered named retirement with pension paid
# the next year, and salary/pension persistence across a fresh reload.
# Rare branches use bounded, explicitly forced fixtures (qa hook) — no new
# production commands, no guessed projection keys (pension is work.pension).
# ---------------------------------------------------------------------------

def _opening_with_earnable_gates(openings):
    """An opening whose qualification and prior-role gates are already
    met (credentials and role experience are earned, never fabricated)."""
    return next((o for o in openings
                 if all(q.get("met") for q in
                        ((o.get("requirements") or {})
                         .get("qualifications") or []))
                 and all(r.get("met") for r in
                         ((o.get("requirements") or {})
                          .get("roles") or []))), None)


def _force_opening_gates(ctx, opening):
    """Bounded fixture: set the server-advertised skill/age requirements
    for one opening (env-level; exact published gates only)."""
    reqs = opening.get("requirements") or {}

    def mutate(env):
        p = env["player"]
        min_age = reqs.get("min_age")
        if min_age and p.age < min_age:
            p.age = min_age
        people = (env.get("skills") or {}).get("people") or {}
        entry = people.get(p.person_id) or {}
        values = entry.get("values") or {}
        for s in reqs.get("skills") or []:
            if s.get("required") is not None:
                values[s["id"]] = s["required"]
        entry["values"] = values
        people[p.person_id] = entry
        env.setdefault("skills", {})["people"] = people
    ctx.qa_env(mutate)


def _accept_career_offer(ctx):
    """Resolve the current career_offer pending with accept. Returns
    (ok, detail)."""
    st = ctx.state()
    d = st.get("pending_decision") or {}
    if d.get("kind") != "career_offer":
        return False, "no career_offer pending (kind=%s)" % d.get("kind")
    r = ctx.api("resolve_decision",
                {"decision_id": d["id"], "option_id": "accept",
                 "inputs": {}})
    if r.get("game_id"):
        return True, "accepted"
    return False, "accept failed: %s" % r.get("_body")


def _named_employment(ctx):
    """Shared setup: establish a named assignment on the current life via
    the approved apply -> stored offer -> accept route, forcing only the
    advertised skill/age gates when needed. Returns (assignment, error)."""
    ok, det = drain_pendings(ctx)
    if not ok:
        return None, "pending chain: " + det
    st = ctx.state()
    openings = (st.get("careers") or {}).get("openings") or []
    if not openings:
        return None, "PENDING: A: careers projection has no openings list"
    target = next((o for o in openings if o.get("eligible") and
                   _opening_with_earnable_gates([o])), None)
    if not target:
        candidate = _opening_with_earnable_gates(openings)
        if not candidate:
            return None, "SKIP: no opening with earnable gates this life"
        _force_opening_gates(ctx, candidate)
        st = ctx.state()
        target = next((o for o in (st.get("careers") or {}).get("openings")
                       or [] if o.get("role_id") == candidate.get("role_id")
                       and o.get("employer_id") == candidate.get("employer_id")),
                      None)
        if not target or not target.get("eligible"):
            return None, "FAIL: server still advertises ineligible after " \
                         "the forced fixture (advertised != dispatched)"
    r = ctx.api("careers_apply",
                {"role_id": target.get("role_id"),
                 "employer_id": target.get("employer_id")})
    if r.get("_status"):
        if _is_eligibility_refusal(r):
            return None, "FAIL: apply refused after eligibility: %s" % \
                         _typed_reason(r)[:120]
        return None, "FAIL: careers_apply: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    ok, det = _accept_career_offer(ctx)
    if not ok:
        return None, "FAIL: " + det
    st = ctx.state()
    assignment = (st.get("careers") or {}).get("assignment") or {}
    if assignment.get("status") != "active":
        return None, "FAIL: no active assignment after accept"
    return assignment, None


def j6_named_employment(ctx):
    """Setup + proof: named employment via the approved transition."""
    assignment, err = _named_employment(ctx)
    if err:
        status = "PENDING" if err.startswith("PENDING") else \
            ("SKIP" if err.startswith("SKIP") else "FAIL")
        return status, err.split(": ", 1)[-1]
    return "PASS", "named employment: %s at %s, salary $%s" % (
        assignment.get("role_id"), assignment.get("employer_id"),
        assignment.get("salary"))


def j6_employer_change(ctx):
    """Real employer change: resign -> apply -> stored offer -> accept.
    Asserts exact before/after employer identity, the 'left' history row
    for the old employer, and adapter pay sync (one wage payer)."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    old = (st.get("careers") or {}).get("assignment") or {}
    if old.get("status") != "active":
        return "SKIP", "no active named assignment (named_employment " \
                       "covers setup)"
    old_employer, old_role = old.get("employer_id"), old.get("role_id")
    r = ctx.api("careers_resign", {})
    if not r.get("game_id"):
        return "FAIL", "resign refused with active assignment: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    st = ctx.state()
    if (st.get("careers") or {}).get("assignment"):
        return "FAIL", "assignment still active after resign"
    if (st.get("work") or {}).get("employed"):
        return "FAIL", "legacy adapter still employed after resign"
    hist = (st.get("careers") or {}).get("history") or []
    if not any(h.get("employer_id") == old_employer and
               h.get("reason") == "left" for h in hist):
        return "FAIL", "no 'left' history row for employer %s" % old_employer
    openings = (st.get("careers") or {}).get("openings") or []
    target = next((o for o in openings
                   if o.get("employer_id") not in (None, old_employer)
                   and _opening_with_earnable_gates([o])), None)
    if not target:
        return "SKIP", "no other-employer opening with earnable gates"
    _force_opening_gates(ctx, target)
    st = ctx.state()
    target = next(
        (o for o in (st.get("careers") or {}).get("openings") or []
         if o.get("role_id") == target.get("role_id")
         and o.get("employer_id") == target.get("employer_id")), None)
    if not target or not target.get("eligible"):
        return "FAIL", "other-employer opening %s/%s not eligible after " \
                       "the bounded fixture (server reason: %s)" % (
                           target and target.get("role_id"),
                           target and target.get("employer_id"),
                           target and target.get("reason"))
    r = ctx.api("careers_apply",
                {"role_id": target.get("role_id"),
                 "employer_id": target.get("employer_id")})
    if r.get("_status"):
        return "FAIL", "apply at %s refused after resign: %s %s" % (
            target.get("employer_id"), _error_code(r),
            _typed_reason(r)[:120])
    ok, det = _accept_career_offer(ctx)
    if not ok:
        return "FAIL", det
    st = ctx.state()
    new = (st.get("careers") or {}).get("assignment") or {}
    work = st.get("work") or {}
    if new.get("employer_id") in (None, old_employer):
        return "FAIL", "employer unchanged: %s -> %s" % (
            old_employer, new.get("employer_id"))
    if new.get("role_id") == old_role and \
            new.get("salary") == old.get("salary") and \
            old_employer == new.get("employer_id"):
        return "FAIL", "assignment identical after employer change"
    if (work.get("salary") or 0) != (new.get("salary") or -1):
        return "FAIL", "adapter pay not synced to new employer terms: " \
                       "work $%s vs assignment $%s" % (
                           work.get("salary"), new.get("salary"))
    hist = (st.get("careers") or {}).get("history") or []
    reasons = [(h.get("employer_id"), h.get("reason")) for h in hist]
    if (old_employer, "left") not in reasons:
        return "FAIL", "old employer missing from history %s" % (reasons,)
    return "PASS", "employer change: %s (%s) -> %s (%s), pay $%s, " \
                   "history %s" % (
                       old_employer, old_role, new.get("employer_id"),
                       new.get("role_id"), work.get("salary"), reasons)


def j6_dismissal_forced(ctx):
    """Engine-triggered dismissal + legal recovery. Forced fixture: the
    legacy performance-firing branch (performance < 15) with performance
    pinned to 0 and stress pinned to 100 so the yearly drift cannot lift
    it; fired with certainty within the 8-year bound (worst case 1/3 per
    year). No new dismissal command — the engine path is the contract."""
    bad = _require_drained(ctx)
    if bad:
        return bad
    st = ctx.state()
    if ((st.get("careers") or {}).get("assignment") or {}).get("status") \
            != "active":
        return "SKIP", "no active named assignment (employer_change " \
                       "covers the setup)"
    role = st["careers"]["assignment"]["role_id"]
    employer = st["careers"]["assignment"]["employer_id"]
    fired_year = None
    for year in range(8):
        def mutate(env):
            p = env["player"]
            p.performance = 0
            p.stress = 100
        try:
            ctx.qa_env(mutate)
        except Exception as exc:
            return "FAIL", "QA fixture crashed: %r" % exc
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused during dismissal probe: %s" % \
                           r.get("_body")
        st = ctx.state()
        if not (st.get("work") or {}).get("employed"):
            fired_year = year
            break
        bad = _require_drained(ctx)
        if bad:
            return bad
    if fired_year is None:
        return "FAIL", "performance firing never fired in 8 forced years " \
                       "(engine dismissal broken)"
    ok, det = drain_pendings(ctx)
    if not ok:
        return "FAIL", "pending chain after the firing: " + det
    st = ctx.state()
    careers = st.get("careers") or {}
    if careers.get("assignment"):
        return "FAIL", "assignment still active after the firing"
    hist = careers.get("history") or []
    if not any(h.get("reason") == "dismissed" and
               h.get("employer_id") == employer for h in hist):
        return "FAIL", "no 'dismissed' history row for %s (history: %s)" % (
            employer, [(h.get("employer_id"), h.get("reason"))
                       for h in hist])
    # legal recovery: no stale commitment blocks a fresh application and
    # aging stays legal
    openings = careers.get("openings") or []
    target = _opening_with_earnable_gates(openings)
    if target:
        _force_opening_gates(ctx, target)
        r = ctx.api("careers_apply",
                    {"role_id": target.get("role_id"),
                     "employer_id": target.get("employer_id")})
        if r.get("_status"):
            return "FAIL", "recovery application refused: %s %s" % (
                _error_code(r), _typed_reason(r)[:120])
        ok, det = _accept_career_offer(ctx)
        if not ok:
            return "FAIL", "recovery did not accept a stored offer: " + det
        recovered = ctx.state()
        if (not (recovered.get("careers") or {}).get("assignment")
                or not (recovered.get("work") or {}).get("employed")):
            return "FAIL", "accepted recovery offer did not restore employment"
        # The recovered worker must still be able to age legally.
        ok, det = drain_pendings(ctx)
        if not ok:
            return "FAIL", "pending chain after recovery apply: " + det
    else:
        return "FAIL", "no reachable opening for dismissal recovery"
    r = ctx.api("age_up", {})
    if not r.get("game_id"):
        return "FAIL", "age_up illegal after dismissal: %s" % r.get("_body")
    return "PASS", "engine dismissal closed %s (%s) with a 'dismissed' " \
                   "history row; recovery actions legal" % (role, employer)


def _retirement_setup(ctx):
    """Fresh 65-year-old life with a named assignment (fresh hires at 65
    are legal — min_age gates only). Returns (assignment, error)."""
    ctx.start("retirement_eligible_65", 6543)
    assignment, err = _named_employment(ctx)
    if err:
        status = "PENDING" if err.startswith("PENDING") else \
            ("SKIP" if err.startswith("SKIP") else "FAIL")
        return None, (status, err.split(": ", 1)[-1])
    return assignment, None


def _check_retired(ctx, expect_employer):
    """Shared assertions after any retirement route closes a named
    assignment. Returns (status, detail)."""
    st = ctx.state()
    work = st.get("work") or {}
    careers = st.get("careers") or {}
    if not work.get("retired"):
        return "FAIL", "work.retired not set after retiring"
    if careers.get("assignment"):
        return "FAIL", "named assignment outlived retirement"
    hist = careers.get("history") or []
    if not any(h.get("reason") == "retired" and
               h.get("employer_id") == expect_employer for h in hist):
        return "FAIL", "no 'retired' history row for %s (history: %s)" % (
            expect_employer, [(h.get("employer_id"), h.get("reason"))
                              for h in hist])
    pension = work.get("pension") or 0
    if pension <= 0:
        return "FAIL", "pension not set (work.pension=%s)" % pension
    # pension pays next year: exactly one income leg sourced from payroll
    money0 = st.get("money")
    r = ctx.api("age_up", {})
    if not r.get("game_id"):
        return "FAIL", "age_up refused in retirement: %s" % r.get("_body")
    ok, det = drain_pendings(ctx)
    if not ok:
        return "FAIL", "pending chain in retirement year: " + det
    st1 = ctx.state()
    legs = ctx.ledger_legs(st1, categories=("income",)) or []
    last_turn = max((l.get("turn_id") for l in legs), default=None)
    year_legs = [l for l in legs if l.get("turn_id") == last_turn]
    pension_legs = [l for l in year_legs
                    if "salary" in str(l.get("source_key", ""))]
    if len(pension_legs) != 1:
        return "FAIL", "expected exactly 1 pension income leg in %s, got " \
                       "%d" % (last_turn, len(pension_legs))
    paid = pension_legs[0].get("amount")
    if paid != pension:
        return "FAIL", "pension leg $%s != work.pension $%s" % (
            paid, pension)
    if (st1.get("work") or {}).get("employed"):
        return "FAIL", "employed again after retirement"
    return "PASS", "retired from %s; pension $%s paid once in %s (money " \
                   "delta %s)" % (
                       expect_employer, pension, last_turn,
                       (st1.get("money") or 0) - (money0 or 0))


def j6_retirement_direct(ctx):
    """Direct M2 retire command with a named assignment: assignment closes
    with reason 'retired', pension starts paying the next year."""
    assignment, err = _retirement_setup(ctx)
    if err:
        return err
    employer = assignment.get("employer_id")
    st = ctx.state()
    if not (st.get("work") or {}).get("can_retire"):
        return "FAIL", "65-year-old with 10+ years cannot retire (M2 gate)"
    r = ctx.api("retire", {})
    if not r.get("game_id"):
        return "FAIL", "advertised-path retire refused: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    return _check_retired(ctx, employer)


def j6_retirement_offered(ctx):
    """Offered retirement: the yearly retirement_offer pending is served,
    stored, and accepting 'retire' closes the named assignment. Forced by
    aging within a bound (offer is a 1-in-3 yearly sample, deterministic
    per save — 12 years per life, two lives); a forced-retirement EVENT at
    70+ is reported honestly and retried."""
    for attempt in range(2):
        assignment, err = _retirement_setup(ctx)
        if err:
            return err
        employer = assignment.get("employer_id")
        accepted = False
        forced = False
        for _year in range(12):
            st = ctx.state()
            d = st.get("pending_decision") or {}
            if d.get("kind") == "retirement_offer":
                r = ctx.api("resolve_decision",
                            {"decision_id": d["id"],
                             "option_id": "retire", "inputs": {}})
                if not r.get("game_id"):
                    return "FAIL", "retirement_offer accept failed: %s" % \
                                   r.get("_body")
                accepted = True
                break
            ok, det = drain_pendings(ctx)
            if not ok:
                return "FAIL", "pending chain while waiting for the " \
                               "retirement offer: " + det
            r = ctx.api("age_up", {})
            if not r.get("game_id"):
                return "FAIL", "age_up refused while waiting for the " \
                               "retirement offer: %s" % r.get("_body")
            st = ctx.state()
            if (st.get("work") or {}).get("retired"):
                forced = True
                break
        if forced:
            continue  # retry: the forced-retirement event pre-empted
        if not accepted:
            continue  # retry: this life's stream served no offer in bound
        status, detail = _check_retired(ctx, employer)
        return status, "offered path: " + detail
    return "FAIL", "retirement_offer never exercised: two lives exhausted " \
                   "their bounds (forced-retirement events or no offer " \
                   "served — offer cadence suspect)"


def j6_salary_pension_reload(ctx):
    """Salary history and pension persist through a fresh reload: compare
    state.work.pension, the assignment/history identity and the pension
    ledger leg before and after a fresh page load + state re-read."""
    st = ctx.state()
    work = st.get("work") or {}
    careers = st.get("careers") or {}
    pension = work.get("pension") or 0
    if not work.get("retired") or pension <= 0:
        return "SKIP", "needs a retired life (retirement steps cover it)"
    before = {
        "pension": pension,
        "retired": work.get("retired"),
        "history": [(h.get("employer_id"), h.get("reason"))
                    for h in careers.get("history") or []],
        "assignment": (careers.get("assignment") or {}).get("assignment_id"),
    }
    legs = ctx.ledger_legs(st, categories=("income",)) or []
    pension_rows = [l.get("id") for l in legs
                    if "salary" in str(l.get("source_key", ""))]
    s = ctx.open_page(390)
    if not _click_tab(s, "occupation|work"):
        return "FAIL", "no Occupation/Work tab after reload"
    ctx.settle(s, 800)
    st1 = ctx.state()
    work1 = st1.get("work") or {}
    careers1 = st1.get("careers") or {}
    after = {
        "pension": work1.get("pension") or 0,
        "retired": work1.get("retired"),
        "history": [(h.get("employer_id"), h.get("reason"))
                    for h in careers1.get("history") or []],
        "assignment": (careers1.get("assignment") or {}).get("assignment_id"),
    }
    if after != before:
        return "FAIL", "state changed across reload: %s != %s" % (after,
                                                                  before)
    legs1 = ctx.ledger_legs(st1, categories=("income",)) or []
    if sorted(l.get("id") for l in legs1) != \
            sorted(l.get("id") for l in legs):
        return "FAIL", "ledger rows lost across reload"
    if not any(i in {l.get("id") for l in legs1} for i in pension_rows):
        return "FAIL", "pension ledger leg missing after reload"
    ctx.shot(s, "journey-%s-pension-reload-390" % ctx.journey_id)
    return "PASS", "pension $%s, history %s and %d ledger rows identical " \
                   "after fresh reload" % (
                       after["pension"], after["history"], len(legs1))


# ---------------------------------------------------------------------------
# Manifest
# ---------------------------------------------------------------------------

JOURNEYS = [
    {"id": "j1_electrical_trade",
     "title": "Electrical trade: apprenticeship → electrician → senior → "
              "supervisor, dismissal + recovery",
     "scenario": "graduating_18",
     "steps": [
         ("finish_school", "Finish secondary school (baseline)", step_finish_school),
         ("work_tab", "Open Occupation/Work at 390+320", step_open_work_tab),
         ("enroll_apprenticeship", "Enroll paid electrical apprenticeship",
          j1_enroll_apprenticeship),
         ("apprentice_assignment", "Training → eligibility → stored apprentice offer",
          j1_apprentice_assignment),
         ("apprentice_wage", "Apprentice year: exactly one ledger wage",
          j1_apprentice_wage),
         ("promotion", "Promotion to electrician", j1_promotion),
         ("dismissal_recovery", "Dismissal and recovery actions",
          j1_dismissal_recovery),
     ]},
    {"id": "j2_healthcare",
     "title": "Healthcare: care diploma → nurse (qualification-gated)",
     "scenario": "graduating_18",
     "steps": [
         ("finish_school", "Finish secondary school (baseline)", step_finish_school),
         ("nurse_gate", "Nurse role gated by nursing_degree (exact reason)",
          j2_nurse_role_gated),
         ("education_ui", "Education UI on Occupation (programs + reasons)",
          j2_education_ui),
         ("education_ui_enroll", "UI-driven enroll starts the real flow",
          j2_education_ui_enroll),
         ("education_progress", "Study + age through to completion (award once)",
          j2_education_progress),
         ("education_reload", "Qualification persists across reload",
          j2_reload_persistence),
     ]},
    {"id": "j3_skills",
     "title": "Skills: train practical, bounded annual usage",
     "scenario": "graduating_18",
     "steps": [
         ("train_skill", "Train a skill with real state change", j3_train_skill),
         ("skills_ui", "Skills UI on Activities (values + options)",
          j3_skills_ui),
         ("skills_ui_train", "UI-driven training changes real state",
          j3_skills_ui_train),
     ]},
    {"id": "j4_zero_money",
     "title": "$0 start: decline funding, stay legal",
     "scenario": None,
     "steps": [
         ("zero_money_legal", "Age legally at $0", j4_zero_money_legal),
     ]},
    {"id": "j5_old_save",
     "title": "Old-save resume: pre-M3 save, no invented degrees/employers",
     "scenario": "employed_30",
     "steps": [
         ("strip_and_load", "Resume a pre-M3 legacy save", j5_strip_and_load),
         ("no_invented_records", "No invented assignments/employers/degrees",
          j5_no_invented_records),
         ("legacy_pay_preserved", "Legacy generic job still pays one wage",
          j5_legacy_pay_preserved),
         ("legacy_transition_gate", "Transition only via the shared " \
          "commitment decision", j5_legacy_transition_gate),
     ]},
    {"id": "j6_m3b_careers",
     "title": "M3b: employer change, dismissal, retirement, salary history",
     "scenario": "employed_30",
     "steps": [
         ("named_employment", "Named employment via the approved " \
          "apply->offer->accept route", j6_named_employment),
         ("employer_change", "Real resign -> apply -> offer -> accept at a " \
          "different employer", j6_employer_change),
         ("dismissal_forced", "Engine-triggered dismissal + legal recovery " \
          "(forced fixture)", j6_dismissal_forced),
         ("retirement_direct", "Direct retire closes the named assignment; " \
          "pension pays next year", j6_retirement_direct),
         ("retirement_offered", "Stored retirement_offer accept closes the " \
          "assignment; pension pays next year", j6_retirement_offered),
         ("salary_pension_reload", "Pension/history/ledger identical " \
          "across a fresh reload", j6_salary_pension_reload),
     ]},
]
