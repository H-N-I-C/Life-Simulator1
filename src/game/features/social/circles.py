"""Social circles: cohort/classmate + employer/coworker derivation.

Called from A-owned service seams after careers/education transitions
and from the yearly drift hook. NPC classmates/coworkers are REAL
canonical Person objects persisted in env["social_people"] (reached by
the service person walk like everyone else).

NPC traits derive from a stable sha256 of (game_id, envelope counter) —
deterministic per save, replay-safe, and deliberately draw NOTHING from
any rng stream (legacy M2 parity: social must not perturb legacy seeded
expectations; person_id stays the os.urandom-backed identity exception).
Group membership rules are frozen in 02-contracts-A.md: job change ->
coworkers keep contact as `faded` people records when they carry a
kind; unkinded coworkers are forgotten; school cohort change -> same
rule for classmates; `estranged` is ONLY event-driven (never automatic).
"""

import hashlib

SCHOOL_MIN, SCHOOL_SPREAD = 4, 3    # 4-6 classmates
WORK_MIN, WORK_SPREAD = 3, 3        # 3-5 coworkers

_LASTNAMES = None


def _lastnames():
    global _LASTNAMES
    if _LASTNAMES is None:
        import os
        path = os.path.join(os.path.dirname(__file__), "..", "..", "..",
                            "..", "assets", "last_names.txt")
        try:
            with open(os.path.abspath(path), encoding="utf-8") as f:
                _LASTNAMES = [ln.strip() for ln in f if ln.strip()]
        except OSError:
            _LASTNAMES = []
    return _LASTNAMES


def _firstnames(gender):
    import os
    from src.lifesim_lib.lifesim_lib import Gender
    fname = "male_names.txt" if gender == Gender.Male \
        else "female_names.txt"
    path = os.path.join(os.path.dirname(__file__), "..", "..", "..",
                        "..", "assets", fname)
    try:
        with open(os.path.abspath(path), encoding="utf-8") as f:
            return [ln.strip() for ln in f if ln.strip()]
    except OSError:
        return ["Sam"]


def _npc_person(env, firstname, lastname, age, gender):
    from src.appearance.integration import ensure_appearance, \
        ensure_person_id
    from src.people.classes.person import Person
    person = Person(firstname, lastname, age, gender,
                    happiness=60, health=90, smarts=50, looks=50)
    ensure_person_id(person)
    ensure_appearance(person)
    env.setdefault("social_people", []).append(person)
    return person


def _family_lastnames(env):
    """Every living family surname (player, parents, siblings,
    partner, children, and reachable relations) — NPCs must never
    collide with any of them (D liveness: mother 'Stafford' vs a
    classmate 'Wilfred Stafford')."""
    player = env["player"]
    names = set()
    seen = set()

    def visit(person):
        if person is None or id(person) in seen:
            return
        seen.add(id(person))
        lastname = getattr(person, "lastname", None)
        if lastname:
            names.add(lastname)
        for rel in getattr(person, "relations", None) or []:
            visit(rel)
        for attr in ("mother", "father", "partner"):
            visit(getattr(person, attr, None))
        for child in getattr(person, "children", None) or []:
            visit(child)

    visit(player)
    return names


def _derive(env, counter):
    """Stable per-life digest -> deterministic NPC traits. Basis: the
    creation fingerprint (sha256 of the creation profile incl. seed) +
    envelope counter — identical for same-seed twin lives, stable across
    the life, and never draws from any rng stream."""
    from src.lifesim_lib.lifesim_lib import Gender
    basis = env.get("creation", {}).get("fingerprint", "")
    seed = f"{basis}:{counter}".encode("utf-8")
    d = hashlib.sha256(seed).digest()
    gender = Gender.Male if d[0] % 2 == 0 else Gender.Female
    firsts = _firstnames(gender)
    first = firsts[int.from_bytes(d[1:3], "big") % len(firsts)]
    all_lasts = _lastnames()
    # NPCs are not related to the player: NEVER reuse ANY living family
    # surname (D liveness: 'Wilfred Stafford' while the mother's family
    # is Stafford); catalog only otherwise.
    family_names = _family_lastnames(env)
    lasts = [n for n in all_lasts if n not in family_names] \
        or all_lasts
    last = lasts[int.from_bytes(d[4:6], "big") % len(lasts)] \
        if lasts else "Citizen"
    return gender, first, last, d


def _gen_classmates(env, st, player):
    out = []
    count = SCHOOL_MIN + _derive(env, st["next_num"])[3][6] \
        % SCHOOL_SPREAD
    for i in range(count):
        gender, first, last, d = _derive(env, st["next_num"] + i)
        age = max(5, player.age + (d[7] % 3) - 1)
        out.append(_npc_person(env, first, last, age, gender))
    st["next_num"] += SCHOOL_MIN + SCHOOL_SPREAD
    return out


def _gen_coworkers(env, st, player):
    out = []
    count = WORK_MIN + _derive(env, st["next_num"])[3][6] % WORK_SPREAD
    for i in range(count):
        gender, first, last, d = _derive(env, st["next_num"] + i)
        lo = max(18, player.age - 12)
        age = lo + d[7] % max(1, (player.age + 15) - lo + 1)
        out.append(_npc_person(env, first, last, age, gender))
    st["next_num"] += WORK_MIN + WORK_SPREAD
    return out


def school_cohort_key(env, player):
    """Current cohort: school stage while enrolled, else the active
    program id (B's education envelope, read-only)."""
    if player.uv_years > 0 or getattr(player, "graduated_uv", False):
        return None
    if player.is_in_school():
        if player.age <= 11:
            return "school:elementary"
        if player.age <= 13:
            return "school:middle"
        return "school:high"
    try:
        from src.game.features.education import core as education_core
        enrollment = education_core.active_enrollment(
            env, player.person_id)
        if enrollment is not None:
            return f"program:{enrollment['program_id']}"
    except Exception:
        pass
    return None


def _forget_or_fade(env, pid, rec):
    """Cohort/job-change rule (frozen): leaving a circle turns members
    into FORMER contacts — kinded ones keep their kind, unkinded ones
    become faded acquaintances (kept, never silently forgotten, never
    auto-estranged)."""
    if rec["kind"] is None:
        rec["kind"] = "acquaintance"
    rec["status"] = "faded"


def mint_friend(env, player, *, age=None, source="event",
                ref="new_friend"):
    """Create a REAL friend Person (the legacy new_friend template's
    'Go play' seam, and any event that befriends a named stranger).
    Deterministic (hash-derived), zero rng draws; returns person_id."""
    from src.game.features.social import state as social
    st = social.ensure(env)
    counter = st["next_num"]
    gender, first, last, d = _derive(env, counter)
    npc = _npc_person(env, first, last,
                      age if age is not None else
                      max(4, player.age + (d[7] % 3) - 1), gender)
    st["next_num"] += 1
    rec = social.record(env, npc.person_id, create=True)
    rec["source"] = {"type": source, "ref": ref}
    social.apply_kind(env, player, npc.person_id, "friend")
    social.apply_closeness(env, player, npc.person_id, 50)
    social.record_memory(env, player, npc.person_id,
                         "We met and hit it off right away.", sentiment=1)
    return npc.person_id


def reconcile(env, service, rng=None):
    """Idempotent circle reconciliation; safe inside command contexts
    and at the yearly hook. NEVER generates during projections (the
    service only calls it from command/phase paths)."""
    from src.game.features.social import state as social
    st = social.ensure(env)
    player = env["player"]
    circles = st.setdefault("circles", {"school": {"cohort_key": None,
                                                   "member_ids": []},
                                        "work": {},
                                        "clubs": {}})
    key = school_cohort_key(env, player)
    school = circles.setdefault("school",
                                {"cohort_key": None, "member_ids": []})
    if key != school.get("cohort_key"):
        old_key = school.get("cohort_key")
        # Frozen rule: a STAGE change (elementary->middle->high) is not
        # goodbye — kinded classmates stay ACTIVE friends (they leave
        # the circle and drift naturally); unkinded ones are forgotten.
        # Leaving school entirely (graduation: cohort -> None) turns
        # everyone into FORMER classmates: faded acquaintances.
        leaving = key is None
        for pid in list(school.get("member_ids", [])):
            rec = st["people"].get(pid)
            if rec is None or rec.get("source", {}).get(
                    "type") not in ("school", "program"):
                continue
            if leaving:
                _forget_or_fade(env, pid, rec)
                continue
            # stage change: only REAL bonds survive the move (kinded
            # special kinds or still-close friends); plain classmates
            # the player never bonded with are forgotten
            bonded = rec["kind"] in ("friend", "best_friend", "rival",
                                     "bully") or rec["closeness"] >= 22
            if not bonded:
                st["people"].pop(pid, None)
        school["cohort_key"] = key
        school["member_ids"] = []
        if key is not None:
            for npc in _gen_classmates(env, st, player):
                rec = social.record(env, npc.person_id, create=True)
                rec["source"] = {"type": "school" if key.startswith(
                    "school:") else "program", "ref": key}
                school["member_ids"].append(npc.person_id)
    from src.game.features.careers import core as careers_core
    assignment = careers_core.active_assignment(env, player.person_id)
    current = assignment and assignment["assignment_id"]
    work = circles.setdefault("work", {})
    for aid in list(work):
        if aid != current:
            for pid in list(work[aid].get("member_ids", [])):
                rec = st["people"].get(pid)
                if rec is not None and rec.get("source", {}).get(
                        "type") == "work":
                    _forget_or_fade(env, pid, rec)
            del work[aid]
    if current and current not in work:
        member_ids = []
        for npc in _gen_coworkers(env, st, player):
            rec = social.record(env, npc.person_id, create=True)
            rec["source"] = {"type": "work", "ref": current}
            member_ids.append(npc.person_id)
        work[current] = {"member_ids": member_ids}
    return circles


def group_member_ids(env, group_id):
    """Frozen group buckets for the projection and C's `in_circle`
    leaf. friends = kinded records; classmates/coworkers = current
    circle members; family is resolved by the service walk."""
    from src.game.features.social import state as social
    st = social.ensure(env)
    circles = st.get("circles", {})
    if group_id == "friends":
        # kinded edges count even when faded (frozen keep-contact rule);
        # faded acquaintances are the FORMER classmates/coworkers —
        # visible here, while active plain acquaintances are not
        return [pid for pid, rec in st["people"].items()
                if rec["kind"] in ("friend", "best_friend",
                                   "rival", "bully")
                or (rec["kind"] == "acquaintance"
                    and rec["status"] == "faded")]
    if group_id == "classmates":
        return list(circles.get("school", {}).get("member_ids", []))
    if group_id == "coworkers":
        out = []
        for aid, circle in circles.get("work", {}).items():
            out.extend(circle.get("member_ids", []))
        return out
    return []
