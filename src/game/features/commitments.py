"""M3 Amendment 2 (contracts): ONE shared primary-commitment decision.

Codex review R3: careers and education each used to check only their
own envelope, so an active enrollment + careers_apply succeeded, and a
legacy generic job could be silently replaced by an apprenticeship.
This module is the single source both features consult BEFORE any
sampling/mutation; the advertised reasons and the dispatch refusals
come from the same call (advertised == dispatched).

Decision table is frozen in docs/collab/M3/02-contracts.md Amendment 2.
An absent feature envelope reads as "no commitment", never a refusal.
"""

JOB = "job"
STUDY = "study"
APPRENTICESHIP = "apprenticeship"

_WANTS = (JOB, STUDY, APPRENTICESHIP)

_ALREADY_ENROLLED = "you are already enrolled in a program"
_HAVE_JOB_PROMOTE = ("you already have a job — resign or seek a "
                     "promotion first")
_RESIGN_FOR_STUDY = "resign from your job before enrolling"
_APPRENTICESHIP_JOB = ("you already have a job — an apprenticeship is a "
                       "joint commitment, resign first")
_PROGRAM_FIRST = "finish or leave your program before taking a job"
_UV_FIRST = "finish or leave your current university course first"
_SCHOOL_FIRST = "finish secondary school first"


def _active_assignment(env, person_id):
    try:
        from src.game.features.careers import core as careers_core
    except Exception:
        return None
    try:
        return careers_core.active_assignment(env, person_id)
    except Exception:
        return None


def _active_enrollment(env, person_id):
    try:
        from src.game.features.education import core as education_core
    except Exception:
        return None
    try:
        return education_core.active_enrollment(env, person_id)
    except Exception:
        return None


def refusal(env, person, want):
    """The frozen shared commitment gate. Returns the precise reason
    string when `want` is illegal right now, else None. First matching
    row of the Amendment 2 table wins."""
    if want not in _WANTS:
        raise ValueError(f"unknown commitment verb: {want!r}")
    person_id = person.person_id
    enrolled = _active_enrollment(env, person_id)
    assigned = _active_assignment(env, person_id)
    legacy_job = bool(getattr(person, "has_job", False)) and assigned \
        is None

    # 1. active feature enrollment
    if enrolled is not None:
        if want == JOB:
            return _PROGRAM_FIRST
        return _ALREADY_ENROLLED
    # 2. named career assignment
    if assigned is not None:
        if want == JOB:
            return _HAVE_JOB_PROMOTE
        if want == APPRENTICESHIP:
            return _APPRENTICESHIP_JOB
        return _RESIGN_FOR_STUDY
    # 3. legacy generic job
    if legacy_job:
        if want == STUDY:
            return _RESIGN_FOR_STUDY
        if want == APPRENTICESHIP:
            return _APPRENTICESHIP_JOB
        return None  # normal job transition, offer overwrites via adapter
    # 4. legacy university
    if getattr(person, "uv_years", 0) > 0:
        if want == JOB:
            return None  # M2 parity
        return _UV_FIRST
    # 5. secondary school
    try:
        in_school = person.is_in_school()
    except Exception:
        in_school = False
    if in_school:
        if want == JOB:
            return None  # M2 parity
        return _SCHOOL_FIRST
    return None
