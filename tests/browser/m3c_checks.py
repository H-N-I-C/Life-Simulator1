"""M3c independent evidence manifest (Kimi D, QA).

Run by tools/m3c_evidence.py. Same step contract and honesty rules as
tests/browser/m3_journeys.py (PASS / FAIL / PENDING / SKIP): ladders are
climbed by REAL PLAY only — training via train_skill commands, credentials
via real education_enroll/study/age_up, jobs via careers_apply -> stored
offer -> accept, promotions via careers_promote. The only env-level hook
allowed is the legacy M2 save construction (strip_to_legacy_m2), which is
the documented migration delta, plus bounded forced fixtures for rare
engine branches (none needed for ladders). No production keys guessed:
pension is state.work.pension; finance rows are the ledger projection.
"""

import json

from tests.browser.m3_journeys import (  # noqa: F401 (re-exported helpers)
    _click_tab, _education_programs, _error_code, _typed_reason,
    _resolve_funding, drain_pendings,
)

LADDERS = {
    "electrical": ["app_electrician", "electrician",
                   "senior_electrician", "site_supervisor"],
    "office": ["office_assistant", "coordinator",
               "operations_specialist", "operations_manager"],
    "healthcare": ["care_assistant", "qualified_care_worker",
                   "nurse", "senior_nurse"],
}
QUAL_PROGRAM = {"electrical_trade_cert": "app_electrical",
                "care_diploma_cert": "care_diploma",
                "nursing_degree": "nursing_bachelors"}


# ---------------------------------------------------------------------------
# Shared real-play helpers
# ---------------------------------------------------------------------------

def _drain(ctx, bound=10):
    """Drain pending chains, preferring free enabled options; among those,
    prefer decline/keep/No-style options (least likely to change jobs or
    spend money), then the fewest published effects."""
    import re as _re
    for _ in range(bound):
        st = ctx.state()
        d = st.get("pending_decision")
        if not d:
            return True, ""
        opts = [o for o in d.get("options", [])
                if o.get("enabled", True) and not o.get("disabled")]
        if not opts:
            return False, "pending %s has no enabled options" % d.get("kind")
        free = [o for o in opts if not o.get("known_costs")]
        pool = free or opts
        calm = [o for o in pool if _re.search(
            r"decline|keep|stay|not now|no\b|skip|pass",
            str(o.get("label", "")) + " " + str(o.get("id", "")),
            _re.I)]
        pool = calm or pool
        pick = min(pool, key=lambda o: len(o.get("known_effects") or []))
        r = ctx.api("resolve_decision",
                    {"decision_id": d["id"], "option_id": pick["id"],
                     "inputs": {}})
        if not r.get("game_id"):
            return False, "resolve %s failed: %s" % (d.get("kind"),
                                                     r.get("_body"))
    return not ctx.state().get("pending_decision"), "drain bound hit"


def _age(ctx):
    ok, err = _drain(ctx)
    if not ok:
        return False, "pending chain: " + err
    r = ctx.api("age_up", {})
    if not r.get("game_id"):
        return False, "age_up refused: %s" % r.get("_body")
    return True, ""


def _skills_projection(ctx):
    return ctx.state().get("skills") or {}


def _train_all_you_can(ctx, plan):
    """Real training toward {skill_id: target}: rotate across all unmet
    skills, spending this year's uses (option + evening course). Returns
    (uses_trained, last_refusal_reason)."""
    trained = 0
    last = ""
    for _ in range(8):
        st = ctx.state()
        sk = st.get("skills") or {}
        values = sk.get("values") or {}
        uses = sk.get("uses_this_year") or {}
        training = {t.get("target_id") or t.get("id"): t
                    for t in sk.get("training") or []}
        unmet = [s for s, t in plan.items()
                 if (values.get(s) or 0) < t]
        if not unmet:
            break
        progressed = False
        for skill in unmet:
            opt = "train_" + skill
            spec = training.get(opt)
            if spec and spec.get("enabled", True) and \
                    uses.get(opt, 0) < 2:
                r = ctx.api("train_skill", {"target_id": opt})
                if not r.get("_status"):
                    trained += 1
                    progressed = True
                    break
                last = _typed_reason(r) or str(r.get("_body"))
        if progressed:
            continue
        eve = training.get("train_evening_course")
        if eve and eve.get("enabled", True) and \
                uses.get("train_evening_course", 0) < 1:
            r = ctx.api("train_skill",
                        {"target_id": "train_evening_course",
                         "skill": unmet[0]})
            if not r.get("_status"):
                trained += 1
                continue
            last = _typed_reason(r) or str(r.get("_body"))
        break
    return trained, last


def _take_any_earnable_job(ctx):
    """Widen to any opening with earnable (qualification-free) gates: free
    training options work even at $0, and an off-ladder job funds gated
    ladder training (real play). Returns (role_id, err)."""
    all_openings = (ctx.state().get("careers") or {}).get("openings") or []
    for o in all_openings:
        s2, q2 = _unmet(o.get("role_id"), o)
        if q2:
            continue
        p2 = {sid: req for sid, val, req in s2}
        tr2, _ref = _train_all_you_can(ctx, p2)
        if tr2 == 0 and p2:
            continue
        ok, _e = _apply_and_accept(ctx, o.get("role_id"))
        if ok:
            return o.get("role_id"), ""
    return None, "no earnable opening anywhere"


def _opening(ctx, role_id):
    openings = (ctx.state().get("careers") or {}).get("openings") or []
    return next((o for o in openings if o.get("role_id") == role_id), None)


def _unmet(role_id, opening):
    req = (opening or {}).get("requirements") or {}
    skills = [(s.get("id"), s.get("value") or 0, s.get("required"))
              for s in req.get("skills") or [] if not s.get("met")]
    quals = [q.get("id") for q in req.get("qualifications") or []
             if not q.get("met")]
    return skills, quals


def _apply_and_accept(ctx, role_id):
    """Apply for role_id and accept the stored offer. Returns (ok, err).
    Every accepted assignment is asserted against the frozen employer-role
    pair list (C2 evidence at the moment of creation)."""
    ok, err = _drain(ctx)
    if not ok:
        return False, "pending chain: " + err
    o = _opening(ctx, role_id)
    if not o:
        return False, "no opening for %s" % role_id
    if not o.get("eligible"):
        return False, "opening %s not eligible: %s" % (
            role_id, o.get("reason"))
    if (o.get("employer_id"), role_id) not in VALID_PAIRS:
        return False, "projection offers invalid pair %s@%s" % (
            role_id, o.get("employer_id"))
    r = ctx.api("careers_apply",
                {"role_id": role_id, "employer_id": o.get("employer_id")})
    if r.get("_status"):
        return False, "careers_apply %s: %s %s" % (
            role_id, _error_code(r), _typed_reason(r)[:120])
    st = ctx.state()
    d = st.get("pending_decision") or {}
    if d.get("kind") != "career_offer":
        return False, "no career_offer after apply for %s" % role_id
    r = ctx.api("resolve_decision",
                {"decision_id": d["id"], "option_id": "accept",
                 "inputs": {}})
    if not r.get("game_id"):
        return False, "accept failed: %s" % r.get("_body")
    bad = _invalid_assignment(ctx)
    if bad:
        return False, "accepted INVALID pair %s@%s (not in frozen employer " \
                      "role_ids)" % bad
    return True, ""


# --- C2: the frozen employer-role pair authority (18 valid pairs) --------
import os as _os

_DATA_PATH = _os.path.join(_os.path.dirname(
    _os.path.dirname(_os.path.dirname(_os.path.abspath(__file__)))),
    "src", "game", "content", "careers", "careers.json")
with open(_DATA_PATH, encoding="utf-8") as _f:
    _CAREERS_DATA = json.load(_f)
VALID_PAIRS = {(e["id"], rid)
               for e in _CAREERS_DATA["employers"]
               for rid in e["role_ids"]}
if len(VALID_PAIRS) != 18:
    raise AssertionError("frozen pair list promises 18 pairs, data has %d"
                         % len(VALID_PAIRS))


def _invalid_assignment(ctx):
    """(employer_id, role_id) of the active assignment if it violates the
    frozen pair list, else None."""
    a = (ctx.state().get("careers") or {}).get("assignment") or {}
    if not a:
        return None
    pair = (a.get("employer_id"), a.get("role_id"))
    return None if pair in VALID_PAIRS else pair


def _complete_program(ctx, program_id):
    """Real study: enroll -> resolve funding -> study+age through the whole
    program. Returns (ok, err, credential_id)."""
    ok, err = _drain(ctx)
    if not ok:
        return False, "pending chain: " + err, None
    r = ctx.api("education_enroll", {"program_id": program_id})
    if r.get("_status"):
        return False, "education_enroll %s: %s %s" % (
            program_id, _error_code(r), _typed_reason(r)[:140]), None
    ok, det = _resolve_funding(ctx)
    if not ok and not _education_programs(ctx.state()).get("enrollment"):
        _drain(ctx)
        if not _education_programs(ctx.state()).get("enrollment"):
            return False, "enroll produced no enrollment (%s)" % det, None
    st = ctx.state()
    enr = _education_programs(st).get("enrollment") or {}
    if enr.get("program_id") != program_id:
        return False, "enrolled in %s, expected %s" % (
            enr.get("program_id"), program_id), None
    duration = enr.get("duration_years") or 1
    credential = None
    prog = next((p for p in _education_programs(st).get("programs") or []
                 if p.get("program_id") == program_id), {})
    credential = prog.get("credential_id")
    quals0 = {q.get("qualification_id") for q in
              _education_programs(st).get("qualifications") or []}
    for year in range(duration):
        ok, err = _drain(ctx)
        if not ok:
            return False, err, None
        enr = _education_programs(ctx.state()).get("enrollment")
        if not enr:  # completed early (unexpected) or left
            break
        r = ctx.api("education_study", {})
        if not r.get("game_id"):
            return False, "education_study refused: %s" % r.get("_body"), None
        ok, err = _age(ctx)
        if not ok:
            return False, err, None
    quals1 = {q.get("qualification_id") for q in
              _education_programs(ctx.state()).get("qualifications") or []}
    if credential and credential not in quals1 - quals0:
        return False, "credential %s not awarded after %d years" % (
            credential, duration), None
    return True, "", credential


def _resign(ctx):
    ok, err = _drain(ctx)
    if not ok:
        return False, "pending chain: " + err
    r = ctx.api("careers_resign", {})
    return bool(r.get("game_id")), r.get("_body")


def _work_harder(ctx):
    r = ctx.api("work_harder", {})
    return bool(r.get("game_id")), r.get("_body")


# ---------------------------------------------------------------------------
# Ladder engine (real play only)
# ---------------------------------------------------------------------------

def _climb(ctx, ladder, bound=45, stop_at=None):
    """Climb the ladder. Passes when the active role reaches stop_at (or
    the top role when stop_at is None). Returns (status, detail)."""
    top = stop_at or ladder[-1]
    start_age = ctx.player_age(ctx.state())
    pending_role = None  # role to apply for once free (after study)
    trace = []

    def _note(*parts):
        trace.append(" ".join(str(p) for p in parts))
        del trace[:-6]

    for year in range(bound):
        ok, err = _drain(ctx)
        if not ok:
            return "FAIL", "year %d: %s" % (year, err)
        st = ctx.state()
        careers = st.get("careers") or {}
        edu = _education_programs(st)
        a = careers.get("assignment") or {}
        enr = edu.get("enrollment")
        if a.get("status") == "active" and a.get("role_id") == top:
            hist = [(h.get("role_id"), h.get("reason"))
                    for h in careers.get("history") or []]
            return "PASS", "role %s reached in %d years, history %s, " \
                           "money %s" % (top,
                                         ctx.player_age(st) - start_age, hist,
                                         st.get("money"))
        if enr:
            _note("y%d study %s" % (year, enr.get("program_id")))
            # study year: guaranteed pass via education_study, train for
            # the role we aim at next
            plan = {}
            if pending_role:
                o = _opening(ctx, pending_role)
                skills, _q = _unmet(pending_role, o)
                plan = {sid: req for sid, val, req in skills}
            _train_all_you_can(ctx, plan)
            r = ctx.api("education_study", {})
            if not r.get("game_id"):
                return "FAIL", "year %d: study refused: %s" % (
                    year, r.get("_body"))
            ok, err = _age(ctx)
            if not ok:
                return "FAIL", "year %d: %s" % (year, err)
            continue
        if a.get("status") == "active":
            if a["role_id"] not in ladder:
                # funding job (taken to afford gated training): train toward
                # the pending goal, then resign back into the ladder
                goal = pending_role or ladder[0]
                o = _opening(ctx, goal)
                skills, quals = _unmet(goal, o)
                _note("y%d funding job %s for %s" % (year, a["role_id"],
                                                     goal))
                if quals:
                    ok, body = _resign(ctx)
                    if not ok:
                        return "FAIL", "year %d: resign from funding job " \
                                       "failed: %s" % (year, body)
                    continue
                plan = {sid: req for sid, val, req in skills}
                _train_all_you_can(ctx, plan)
                o = _opening(ctx, goal)
                if o and o.get("eligible"):
                    ok, body = _resign(ctx)
                    if not ok:
                        return "FAIL", "year %d: resign to re-enter ladder " \
                                       "failed: %s" % (year, body)
                    continue
                _work_harder(ctx)
                ok, err = _age(ctx)
                if not ok:
                    return "FAIL", "year %d: %s" % (year, err)
                continue
            i = ladder.index(a["role_id"]) \
                if a["role_id"] in ladder else -1
            if i < 0 or i + 1 >= len(ladder):
                return "FAIL", "year %d: assignment %s off-ladder" % (
                    year, a["role_id"])
            target = ladder[i + 1]
            if (a.get("employer_id"), target) not in VALID_PAIRS:
                # C2: the target role is not offered by the current
                # employer. Meet the non-employer gates FIRST (stay
                # employed and paid), then take the approved
                # employer-change path (resign -> apply at a valid
                # employer) instead of creating an unsupported pair.
                o = _opening(ctx, target)
                skills, quals = _unmet(target, o)
                if quals:
                    ok, body = _resign(ctx)
                    if not ok:
                        return "FAIL", "year %d: resign to study failed: " \
                                       "%s" % (year, body)
                    program = QUAL_PROGRAM.get(quals[0])
                    if not program:
                        return "FAIL", "year %d: no program earns %s" % (
                            year, quals[0])
                    ok, err, _cred = _complete_program(ctx, program)
                    if not ok:
                        return "FAIL", "year %d: study %s: %s" % (
                            year, program, err)
                    pending_role = target
                    continue
                if skills:
                    plan = {sid: req for sid, val, req in skills}
                    trained, _ref = _train_all_you_can(ctx, plan)
                    if trained == 0:
                        _work_harder(ctx)
                    ok, err = _age(ctx)
                    if not ok:
                        return "FAIL", "year %d: %s" % (year, err)
                    continue
                _note("y%d %s not offered by %s; changing employer for %s"
                      % (year, target, a.get("employer_id"), target))
                ok, body = _resign(ctx)
                if not ok:
                    return "FAIL", "year %d: resign for employer change " \
                                   "failed: %s" % (year, body)
                ok, err = _apply_and_accept(ctx, target)
                if not ok:
                    return "FAIL", "year %d: apply %s at valid employer " \
                                   "failed: %s" % (year, target, err)
                _note("y%d moved employer ->%s" % (year, target))
                continue
            r = ctx.api("careers_promote", {"role_id": target})
            if r.get("game_id"):
                st = ctx.state()
                d = st.get("pending_decision") or {}
                if d.get("kind") != "career_offer":
                    return "FAIL", "year %d: promote accepted but no offer" \
                                   % year
                r = ctx.api("resolve_decision",
                            {"decision_id": d["id"], "option_id": "accept",
                             "inputs": {}})
                if not r.get("game_id"):
                    return "FAIL", "year %d: promotion accept failed: %s" % (
                        year, r.get("_body"))
                bad = _invalid_assignment(ctx)
                if bad:
                    return "FAIL", "year %d: promotion created INVALID " \
                                   "pair %s@%s" % (year, bad[1], bad[0])
                _note("y%d promoted->%s" % (year, target))
                continue
            reason = _typed_reason(r)
            if r.get("_status") == 409 and "employer" in reason.lower() and \
                    any(w in reason.lower()
                        for w in ("not available", "does not offer",
                                  "offers", "change employer")):
                # A's C2 refusal: follow the advertised employer-change path
                _note("y%d promote refused (availability): %s" % (year,
                                                                  reason))
                ok, body = _resign(ctx)
                if not ok:
                    return "FAIL", "year %d: resign after availability " \
                                   "refusal failed: %s" % (year, body)
                ok, err = _apply_and_accept(ctx, target)
                if not ok:
                    return "FAIL", "year %d: employer-change apply %s " \
                                   "failed: %s" % (year, target, err)
                continue
            o = _opening(ctx, target)
            skills, quals = _unmet(target, o)
            if r.get("_status") != 409:
                return "FAIL", "year %d: promote %s: %s %s" % (
                    year, target, _error_code(r), reason[:140])
            _note("y%d promote %s refused:" % (year, target), reason[:90])
            if quals:
                ok, body = _resign(ctx)
                if not ok:
                    return "FAIL", "year %d: resign to study failed: %s" % (
                        year, body)
                program = QUAL_PROGRAM.get(quals[0])
                if not program:
                    return "FAIL", "year %d: no program earns %s" % (
                        year, quals[0])
                ok, err, _cred = _complete_program(ctx, program)
                if not ok:
                    return "FAIL", "year %d: study %s: %s" % (
                        year, program, err)
                pending_role = target
                continue
            if skills:
                # train toward the published gates, then let tenure accrue
                plan = {sid: req for sid, val, req in skills}
                trained, refusal = _train_all_you_can(ctx, plan)
                if trained == 0 and "afford" in refusal.lower():
                    return "FAIL", "year %d: employed but cannot afford " \
                                   "training toward %s: %s" % (
                                       year, target, refusal[:120])
                _work_harder(ctx)
                ok, err = _age(ctx)
                if not ok:
                    return "FAIL", "year %d: %s" % (year, err)
                continue
            # tenure/performance gate: work a year (work_harder lifts perf)
            _work_harder(ctx)
            ok, err = _age(ctx)
            if not ok:
                return "FAIL", "year %d: %s" % (year, err)
            continue
        # no assignment: enter or re-enter the ladder
        role = pending_role or ladder[0]
        ok, err = _apply_and_accept(ctx, role)
        if ok:
            _note("y%d applied->%s" % (year, role))
            pending_role = None
            continue
        o = _opening(ctx, role)
        skills, quals = _unmet(role, o)
        _note("y%d apply %s refused:" % (year, role), err[:90])
        if quals:
            program = QUAL_PROGRAM.get(quals[0])
            if not program:
                return "FAIL", "year %d: no program earns %s" % (
                    year, quals[0])
            ok, err2, _cred = _complete_program(ctx, program)
            if not ok:
                return "FAIL", "year %d: apply %s: %s; study %s: %s" % (
                    year, role, err, program, err2)
            pending_role = role
            continue
        plan = {sid: req for sid, val, req in skills}
        trained, refusal = _train_all_you_can(ctx, plan)
        if trained == 0:
            took, why = _take_any_earnable_job(ctx)
            if took:
                _note("y%d took %s to fund the ladder" % (year, took))
            else:
                return "FAIL", "year %d: cannot train (%s) and no " \
                               "earnable job anywhere (broke)" % (
                                   year, refusal[:120])
        ok, err2 = _age(ctx)
        if not ok:
            return "FAIL", "year %d: %s" % (year, err2)
    return "FAIL", "ladder not completed in %d steps (trace: %s)" % (
        bound, " | ".join(trace))


def _ladder_step(ladder_key):
    ladder = LADDERS[ladder_key]

    def fn(ctx):
        return _climb(ctx, ladder)

    return fn


# ---------------------------------------------------------------------------
# J7 — contrasting starts: every start offers legal free actions and a
# clean 390px mount
# ---------------------------------------------------------------------------

CONTRASTING = [
    ("graduating_18", "Age 18, just graduated"),
    ("unemployed_18", "Age 18, graduated, unemployed"),
    ("enrolled_child_10", "Child age 10, enrolled"),
    ("employed_30", "30, employed"),
]


def j7_contrasting_starts(ctx):
    out = []
    for scenario, label in CONTRASTING:
        ctx.start(scenario, 9910 + len(out))
        ok, err = _drain(ctx)
        if not ok:
            return "FAIL", "%s: pending chain: %s" % (scenario, err)
        st = ctx.state()
        actions = st.get("available_actions") or []
        enabled = [a for a in actions if a.get("enabled", True)]
        if not enabled:
            return "FAIL", "%s: no enabled actions (soft-lock)" % scenario
        if not any(not a.get("known_costs") for a in enabled):
            return "FAIL", "%s: no free action available (soft-lock)" % scenario
        if not any(a.get("id") == "age_up" for a in enabled):
            return "FAIL", "%s: age_up not advertised" % scenario
        ok, err = _age(ctx)
        if not ok:
            return "FAIL", "%s cannot age: %s" % (scenario, err)
        s = ctx.open_page(390)
        ctx.settle(s, 600)
        if s.js("document.documentElement.scrollWidth > window.innerWidth"):
            return "FAIL", "%s: horizontal overflow at 390" % scenario
        if s.console_errors or s.exceptions:
            return "FAIL", "%s: console errors: %s %s" % (
                scenario, s.console_errors[:2], s.exceptions[:2])
        ctx.shot(s, "m3c-start-%s-390" % scenario)
        out.append(scenario)
    return "PASS", "starts aged legally with free actions: %s" % \
                   ", ".join(out)


# ---------------------------------------------------------------------------
# J8 — M2 save migration: legacy university in progress
# ---------------------------------------------------------------------------

def j8_legacy_university(ctx):
    """A pre-M3 save mid-university (uv_years>0): resumes with no invented
    M3 records; M3 enrollment is refused while the legacy course runs
    (Amendment 2); after it finishes, enrollment works through the normal
    action."""
    ctx.start("graduating_18", 9920)
    try:
        ctx.strip_to_legacy_m2(salary=0, employ=False, uv_years=2)
    except Exception as exc:
        return "FAIL", "legacy fixture crashed: %r" % exc
    st = ctx.state()
    if _education_programs(st).get("enrollment"):
        return "FAIL", "invented enrollments on legacy save"
    if (_education_programs(st).get("qualifications") or []):
        return "FAIL", "invented qualifications on legacy save"
    r = ctx.api("education_enroll", {"program_id": "care_diploma"})
    if r.get("game_id"):
        return "FAIL", "M3 enrollment accepted during a legacy university " \
                       "course (Amendment 2 gate missing)"
    if not (_error_code(r) == "not_allowed" and "university" in
            _typed_reason(r).lower()):
        return "FAIL", "wrong refusal for legacy university gate: %s %s" % (
            _error_code(r), _typed_reason(r)[:140])
    # age the legacy course to completion (bounded)
    finished = False
    for _ in range(6):
        ok, err = _drain(ctx)
        if not ok:
            return "FAIL", err
        # decline any fresh university application offers (free option)
        st = ctx.state()
        d = st.get("pending_decision") or {}
        if d.get("kind") in ("university_application",
                             "university_funding"):
            keep = next((o for o in d.get("options", [])
                         if "decline" in str(o.get("id")) or
                         "not" in str(o.get("label", "")).lower()), None)
            pick = keep or d.get("options", [None])[0]
            ctx.api("resolve_decision",
                    {"decision_id": d["id"], "option_id": pick["id"],
                     "inputs": {}})
        ok, err = _age(ctx)
        if not ok:
            return "FAIL", err
        # the legacy course is done when the enroll gate lifts
        r = ctx.api("education_enroll", {"program_id": "care_diploma"})
        if not r.get("_status"):
            finished = True
            break
    if not finished:
        return "FAIL", "legacy university course never finished in 6 years"
    ok, det = _resolve_funding(ctx)
    if not ok and not _education_programs(ctx.state()).get("enrollment"):
        return "FAIL", "post-university enrollment failed: " + det
    quals = _education_programs(ctx.state()).get("qualifications") or []
    if quals:
        return "FAIL", "generic university invented a named credential: %s" \
                       % [q.get("qualification_id") for q in quals]
    return "PASS", "legacy uv_years course resumed, finished and gated " \
                   "enrollment; then care_diploma enrolled via normal " \
                   "action, no invented credential"


# ---------------------------------------------------------------------------
# J9 — money conservation: integer cash delta == sum of the year's ledger
# legs, posted once through the one poster
# ---------------------------------------------------------------------------

def j9_money_conservation(ctx):
    ctx.start("employed_30", 9930)
    ok, err = _drain(ctx)
    if not ok:
        return "FAIL", err
    st = ctx.state()
    money0 = st.get("money") or 0
    ok, err = _age(ctx)
    if not ok:
        return "FAIL", err
    st1 = ctx.state()
    money1 = st1.get("money") or 0
    ledger = ((st1.get("finances") or {}).get("ledger")) or []
    if not ledger:
        return "PENDING", "A: ledger projection has no rows"
    last_turn = max(l.get("turn_id") for l in ledger)
    legs = [l for l in ledger if l.get("turn_id") == last_turn]
    ids = [l.get("id") for l in legs]
    if len(ids) != len(set(ids)):
        return "FAIL", "duplicate leg ids in %s (double posting)" % last_turn
    total = sum(int(l.get("amount") or 0) for l in legs)
    delta = money1 - money0
    if delta != total:
        return "FAIL", "money delta %s != ledger sum %s for %s (%d legs)" \
                       % (delta, total, last_turn, len(legs))
    by_cat = {}
    for l in legs:
        by_cat[l.get("category")] = by_cat.get(l.get("category"), 0) + \
            int(l.get("amount") or 0)
    # also audit a study year with loan principal if cheap to add later
    return "PASS", "%s: delta %s == sum of %d legs %s" % (
        last_turn, delta, len(legs), by_cat)


# ---------------------------------------------------------------------------
# J10 — visual pass at 390 + 320: mounts, overflow, console, money format
# ---------------------------------------------------------------------------

SCREENS = [("occupation|work", "Occupation"), ("assets", "Assets"),
           ("relationships|people", "Relationships"),
           ("activities", "Activities")]


def j10_visual_pass(ctx):
    ctx.start("graduating_18", 9940)
    ok, err = _drain(ctx)
    if not ok:
        return "FAIL", err
    problems = []
    for w, tag in ((390, "390"), (320, "320")):
        s = ctx.open_page(w)
        # Home is the name row (M3a shell polish), not a tabbar button
        home = s.js(
            "(() => { const b = document.querySelector('[aria-label^="
            "\"Home\"]'); if (!b) return false; b.click(); return true; })()")
        if not home:
            problems.append("%s: no Home name-row control" % tag)
        else:
            ctx.settle(s, 600)
            ctx.shot(s, "m3c-visual-home-%s" % tag)
        for tab_js, label in SCREENS:
            if not _click_tab(s, tab_js):
                problems.append("%s: no %s tab" % (tag, label))
                continue
            ctx.settle(s, 700)
            if s.js("document.documentElement.scrollWidth > "
                    "window.innerWidth"):
                problems.append("%s/%s: horizontal overflow" % (tag, label))
            if s.console_errors or s.exceptions:
                problems.append("%s/%s: console %s %s" % (
                    tag, label, s.console_errors[:1], s.exceptions[:1]))
            ctx.shot(s, "m3c-visual-%s-%s" % (label.lower(), tag))
        if s.js("/\\$-\\d/.test(document.body.innerText)"):
            problems.append("%s: raw negative money format '$-'" % tag)
    if problems:
        return "FAIL", "; ".join(problems[:6])
    return "PASS", "Home + 4 tabs x 390/320: no overflow, no console " \
                   "errors, money format clean, screenshots captured"


# ---------------------------------------------------------------------------
# J11 — C2 probe: a promotion unavailable at the current employer must be
# refused with the real reason (or, pre-C2, it exposes the invalid pair).
# ---------------------------------------------------------------------------

def j11_promotion_unavailable_reason(ctx):
    """Real-play to operations_specialist@meridian (meridian does NOT
    offer operations_manager — only cascade does), gates met, then probe
    the promotion: accepted offer = C2 defect (FAIL, owner A); typed
    refusal pointing at the employer-change path = PASS."""
    status, detail = _climb(ctx, LADDERS["office"],
                            stop_at="operations_specialist")
    if status == "SKIP":
        return status, detail
    if status != "PASS":
        return "FAIL", "real-play climb to the probe point: " + detail
    st = ctx.state()
    a = (st.get("careers") or {}).get("assignment") or {}
    if a.get("role_id") != "operations_specialist" or \
            a.get("employer_id") != "meridian":
        return "SKIP", "life is at %s@%s, not the meridian probe point" % (
            a.get("role_id"), a.get("employer_id"))
    # meet the remaining non-employer gates so only availability can refuse
    o = _opening(ctx, "operations_manager")
    skills, quals = _unmet("operations_manager", o)
    if quals:
        return "SKIP", "unexpected qualification gate for operations_manager"
    for _ in range(10):
        st = ctx.state()
        o = _opening(ctx, "operations_manager")
        skills, _ = _unmet("operations_manager", o)
        perf = ((st.get("work") or {}).get("performance") or 0)
        if not skills and perf >= 60:
            break
        _train_all_you_can(ctx, {sid: req for sid, val, req in skills})
        _work_harder(ctx)
        ok, err = _age(ctx)
        if not ok:
            return "FAIL", err
    # THE PROBE: operations_manager is not in meridian's role_ids
    ok, err = _drain(ctx)
    if not ok:
        return "FAIL", err
    r = ctx.api("careers_promote", {"role_id": "operations_manager"})
    if r.get("game_id"):
        st = ctx.state()
        d = st.get("pending_decision") or {}
        if d.get("kind") == "career_offer":
            ctx.api("resolve_decision",
                    {"decision_id": d["id"], "option_id": "decline",
                     "inputs": {}})
        return "FAIL", "C2 not enforced: promotion created the invalid " \
                       "pair operations_manager@meridian (frozen list has " \
                       "operations_manager only at cascade) — offer " \
                       "declined, owner A"
    reason = _typed_reason(r)
    if r.get("_status") == 409 and "employer" in reason.lower():
        return "PASS", "unavailable promotion refused with the real " \
                       "reason: %s" % reason[:140]
    return "FAIL", "unexpected promotion probe result: %s %s" % (
        _error_code(r), reason[:140])


# ---------------------------------------------------------------------------
# J12 — C1 alt-route history cases (Claude policy: role experience = total
# recorded years across ALL stints; unknown legacy tenure counts as 0).
# The nurse alternative route requires 8 recorded years as qualified care
# worker instead of the nursing degree.
# ---------------------------------------------------------------------------

def _accumulate_role_years(ctx, role, target_years, plan, bound=30,
                           after_first_year=None):
    """Age until `target_years` have been served in-role (each such year
    adds one recorded tenure year — the quantity C1 sums across stints).
    Engine events that change the job are recovered by re-promotion.
    Returns (counted, err)."""
    counted = 0
    for _ in range(bound):
        ok, err = _drain(ctx)
        if not ok:
            return counted, "pending chain: " + err
        st = ctx.state()
        a = (st.get("careers") or {}).get("assignment") or {}
        if a.get("role_id") == role:
            _train_all_you_can(ctx, plan)
            _work_harder(ctx)
            ok, err = _age(ctx)
            if not ok:
                return counted, err
            counted += 1
            if counted == 1 and after_first_year:
                verdict = after_first_year(ctx)
                if verdict:
                    return counted, verdict
            if counted >= target_years:
                return counted, ""
            continue
        # job changed under us (engine event): restore by promotion,
        # working a year when tenure/performance gate the restore
        for _try in range(4):
            ok, err = _drain(ctx)
            if not ok:
                return counted, "pending chain: " + err
            r = ctx.api("careers_promote", {"role_id": role})
            if r.get("game_id"):
                d = ctx.state().get("pending_decision") or {}
                ctx.api("resolve_decision",
                        {"decision_id": d["id"], "option_id": "accept",
                         "inputs": {}})
                break
            _train_all_you_can(ctx, plan)
            _work_harder(ctx)
            ok, err = _age(ctx)
            if not ok:
                return counted, "restore %s: %s" % (role, err)
        else:
            return counted, "could not restore role %s after an engine " \
                            "event changed the job" % role
    return counted, "bound hit after %d/%d in-role years" % (
        counted, target_years)


def _c1_early_detection():
    """After ONE recorded qualified-care year: if nurse is already
    eligible, the implemented C1 gate has regressed and must FAIL."""
    def check(ctx):
        on = _opening(ctx, "nurse")
        if on and on.get("eligible"):
            return "FAIL: C1 tenure bypass — nurse " \
                   "eligible with 1 recorded qualified-care year (bypass)"
        return ""
    return check


def _qualified_via_history(ctx, stint1, expect_complete):
    """Real-play build: diploma -> care assistant -> qualified care
    worker (stint1 recorded years) -> resign -> qualified care worker
    again (stint2 recorded years). Returns (status, detail)."""
    ok, err = _drain(ctx)
    if not ok:
        return "FAIL", err
    ok, err, _cred = _complete_program(ctx, "care_diploma")
    if not ok:
        return "FAIL", "care_diploma: " + err
    # enter care work (fund via any earnable job when broke — real play)
    for _ in range(10):
        st = ctx.state()
        a = (st.get("careers") or {}).get("assignment") or {}
        if a.get("role_id"):
            break
        ok, err = _apply_and_accept(ctx, "care_assistant")
        if ok:
            break
        o = _opening(ctx, "care_assistant")
        skills, _q = _unmet("care_assistant", o)
        trained, _ref = _train_all_you_can(
            ctx, {sid: req for sid, val, req in skills})
        if trained == 0:
            took, why = _take_any_earnable_job(ctx)
            if not took:
                return "FAIL", "broke at entry: " + why
        ok, err2 = _age(ctx)
        if not ok:
            return "FAIL", err2
    # if the funding job was off-ladder, resign back into care work
    for _ in range(4):
        st = ctx.state()
        a = (st.get("careers") or {}).get("assignment") or {}
        if not a.get("role_id") or a.get("role_id") == "care_assistant":
            break
        ok, body = _resign(ctx)
        if not ok:
            return "FAIL", "resign from funding job: %s" % body
    for _ in range(8):
        st = ctx.state()
        a = (st.get("careers") or {}).get("assignment") or {}
        if a.get("role_id") == "care_assistant":
            break
        ok, err = _apply_and_accept(ctx, "care_assistant")
        if ok:
            break
        o = _opening(ctx, "care_assistant")
        skills, _q = _unmet("care_assistant", o)
        _train_all_you_can(ctx, {sid: req for sid, val, req in skills})
        ok, err2 = _age(ctx)
        if not ok:
            return "FAIL", err2
    # promote to qualified care worker (tenure 1, perf 50)
    for _ in range(8):
        ok, err = _drain(ctx)
        if not ok:
            return "FAIL", err
        st = ctx.state()
        a = (st.get("careers") or {}).get("assignment") or {}
        if a.get("role_id") == "qualified_care_worker":
            break
        if a.get("role_id") and a.get("role_id") != "care_assistant":
            ok, body = _resign(ctx)
            if not ok:
                return "FAIL", "job changed by an event; resign failed: " \
                               "%s" % body
            ok, err = _apply_and_accept(ctx, "care_assistant")
            if not ok:
                return "FAIL", "job changed by an event; re-apply failed: " \
                               "%s" % err
            continue
        r = ctx.api("careers_promote", {"role_id": "qualified_care_worker"})
        if r.get("game_id"):
            d = ctx.state().get("pending_decision") or {}
            ctx.api("resolve_decision",
                    {"decision_id": d["id"], "option_id": "accept",
                     "inputs": {}})
            continue
        _train_all_you_can(ctx, {"care": 30})
        _work_harder(ctx)
        ok, err = _age(ctx)
        if not ok:
            return "FAIL", err
    else:
        return "FAIL", "never promoted to qualified_care_worker"
    # stint 1: N recorded years, training the shared nurse skills so the
    # only remaining gate is the alternative-route tenure
    plan = {"care": 45, "communication": 30}
    n1, err = _accumulate_role_years(ctx, "qualified_care_worker", stint1,
                                     plan,
                                     after_first_year=_c1_early_detection())
    if err.startswith("PENDING"):
        return "PENDING", err.split(": ", 1)[1]
    if err:
        return "FAIL", "stint 1: " + err
    ok, body = _resign(ctx)
    if not ok:
        return "FAIL", "resign after stint 1 failed: %s" % body
    ok, err = _apply_and_accept(ctx, "qualified_care_worker")
    if not ok:
        return "FAIL", "re-applying qualified_care_worker from history " \
                       "failed: %s" % err
    stint2 = 8 - stint1 if expect_complete else 7 - stint1
    n2, err = _accumulate_role_years(ctx, "qualified_care_worker", stint2,
                                     plan)
    if err:
        return "FAIL", "stint 2: " + err
    st = ctx.state()
    on = _opening(ctx, "nurse")
    quals = {q.get("qualification_id") for q in
             _education_programs(st).get("qualifications") or []}
    if expect_complete:
        if not on or not on.get("eligible"):
            return "FAIL", "%d+%d recorded qualified-care years but " \
                           "nurse not eligible via the alternative route: " \
                           "%s" % (n1, n2, on and on.get("reason"))
        if "nursing_degree" in quals:
            return "FAIL", "nursing_degree present — alternative route " \
                           "not what admitted the player"
        ok, body = _resign(ctx)
        if not ok:
            return "FAIL", "resign before the nurse apply failed: %s" % body
        ok, err = _apply_and_accept(ctx, "nurse")
        if not ok:
            return "FAIL", "nurse apply via alternative route: " + err
        return "PASS", "nurse reached via the 8-year alternative route " \
                       "(%d+%d recorded years across two stints, no " \
                       "degree)" % (n1, n2)
    if on and on.get("eligible"):
        return "FAIL", "C1 bypass: nurse eligible with only %d recorded " \
                       "qualified-care years" % (n1 + n2)
    ok, body = _resign(ctx)
    if not ok:
        return "FAIL", "resign before the below-threshold probe failed: %s" \
                       % body
    ok, err = _drain(ctx)
    if not ok:
        return "FAIL", err
    r = ctx.api("careers_apply",
                {"role_id": "nurse",
                 "employer_id": (on or {}).get("employer_id")})
    if r.get("game_id"):
        d = ctx.state().get("pending_decision") or {}
        if d.get("kind") == "career_offer":
            ctx.api("resolve_decision",
                    {"decision_id": d["id"], "option_id": "decline",
                     "inputs": {}})
        return "FAIL", "ungated nurse apply accepted with %d recorded " \
                       "years" % (n1 + n2)
    reason = _typed_reason(r)
    if r.get("_status") != 409 or _error_code(r) != "not_allowed":
        return "FAIL", "unexpected nurse refusal: %s %s" % (
            _error_code(r), reason[:140])
    if "8 years as a Qualified care worker" not in reason:
        return "FAIL", "refusal does not cite the 8-year alternative " \
                       "requirement: %s" % reason[:140]
    return "PASS", "%d recorded years refused with the exact " \
                   "alternative-route reason: %s" % (n1 + n2,
                                                     reason[:140])


def j12_alt_route_accumulates(ctx):
    """5 + 3 recorded years across two stints = 8: nurse via alternative."""
    return _qualified_via_history(ctx, 5, expect_complete=True)


def j12_alt_route_below_threshold(ctx):
    """4 + 3 = 7 recorded years: nurse stays gated (no degree)."""
    return _qualified_via_history(ctx, 4, expect_complete=False)




# ---------------------------------------------------------------------------
# Manifest
# ---------------------------------------------------------------------------

CHECKS = [
    {"id": "j7_contrasting_starts",
     "title": "Contrasting starts age legally with free actions (390px)",
     "scenario": None,
     "steps": [
         ("contrasting_starts", "Four scenario starts: legal actions + " \
          "clean mount", j7_contrasting_starts),
     ]},
    {"id": "j8_legacy_university",
     "title": "M2 save migration: legacy university course in progress",
     "scenario": None,
     "steps": [
         ("legacy_university", "uv_years save: no invented records, gate " \
          "lifts after the course", j8_legacy_university),
     ]},
    {"id": "ladder_electrical",
     "title": "Complete the electrical ladder via real play",
     "scenario": "graduating_18",
     "steps": [
         ("climb", "apprentice -> electrician -> senior -> site supervisor",
          _ladder_step("electrical")),
     ]},
    {"id": "ladder_office",
     "title": "Complete the office ladder via real play",
     "scenario": "graduating_18",
     "steps": [
         ("climb", "assistant -> coordinator -> specialist -> manager",
          _ladder_step("office")),
     ]},
    {"id": "ladder_healthcare",
     "title": "Complete the healthcare ladder via real play (diploma + " \
              "degree gated)",
     "scenario": "graduating_18",
     "steps": [
         ("climb", "care assistant -> qualified care worker -> nurse -> " \
          "senior nurse", _ladder_step("healthcare")),
     ]},
    {"id": "j11_promotion_unavailable",
     "title": "C2: promotion unavailable at the current employer is " \
              "refused with the real reason",
     "scenario": "graduating_18",
     "steps": [
         ("unavailable_reason", "operations_manager@meridian probe " \
          "(real-play climb first)", j11_promotion_unavailable_reason),
     ]},
    {"id": "j12_alt_route_accumulates",
     "title": "C1: 5+3 recorded years across two stints -> nurse via the " \
              "alternative route (no degree)",
     "scenario": "graduating_18",
     "steps": [
         ("accumulates", "8 recorded qualified-care years admit the " \
          "player", j12_alt_route_accumulates),
     ]},
    {"id": "j12_alt_route_below_threshold",
     "title": "C1: 4+3 = 7 recorded years stay gated without a degree",
     "scenario": "graduating_18",
     "steps": [
         ("below_threshold", "7 recorded years refuse with the 8-year " \
          "reason", j12_alt_route_below_threshold),
     ]},
    {"id": "j9_money_conservation",
     "title": "Money conservation: cash delta == ledger sum, single posting",
     "scenario": None,
     "steps": [
         ("conservation", "Employed year: integer delta equals signed " \
          "ledger sum", j9_money_conservation),
     ]},
    {"id": "j10_visual_pass",
     "title": "Visual pass at 390/320: screens, overflow, console, format",
     "scenario": None,
     "steps": [
         ("visual_pass", "4 screens at both widths", j10_visual_pass),
     ]},
]
