"""Training actions: eligibility, the single gain applier, the command,
annual job/study contributions and action specs.

All money flows through the frozen ledger boundary with a persisted
occurrence key; all gains go through apply_training exactly once per
persisted occurrence. Eligibility is checked BEFORE any spend.
"""

import math

from src.game.features.skills import data, state
from src.game.service import GameError


def _round_stochastic(rng, value):
    low = math.floor(value)
    high = math.ceil(value)
    if value < 0:
        return low if rng.random() < high - value else high
    return high if rng.random() < value - low else low

# Per-skill first-person journal variants. One is picked per gain through
# the COSMETIC stream (never the simulation stream) with a no-repeat
# window persisted in env["skill_line_recents"], so training the same
# skill year after year does not reprint one fixed line.
SKILL_LINE_RECENTS_KEY = "skill_line_recents"
SKILL_LINE_WINDOW = 30

_SKILL_LINES = {
    "practical": [
        "I spent my free hours fixing and making things. My hands "
        "are learning faster than my patience.",
        "I practised the practical stuff until my fingers knew the "
        "steps before my brain did.",
        "Another week of tinkering, measuring and nearly swearing. "
        "The results, though, are starting to show.",
        "I fixed something that used to beat me and stood back to "
        "admire my own handiwork far too long.",
    ],
    "organization": [
        "I planned something properly for once and it actually "
        "worked. I could get used to this order business.",
        "I made a list, kept to the list, and finished early. "
        "Nobody was more surprised than me.",
        "My desk, my diary and my life all got a small tidy this "
        "year. The diary is the only one staying tidy.",
        "I rehearsed the routine until the routine ran itself. "
        "There is real joy in a well-kept calendar.",
    ],
    "communication": [
        "I stood up and talked in front of people and only "
        "forgot my point twice. Progress.",
        "I practised saying the difficult sentence out loud until "
        "it stopped being difficult.",
        "A conversation went exactly where I meant it to go. I am "
        "as shocked as anyone.",
        "I told a story at a gathering and people actually leaned "
        "in. Leaning in! From people!",
    ],
    "care": [
        "I spent my time looking after people who needed it. Turns "
        "out I am rather good at noticing what hurts.",
        "I practised the gentle arts of listening and helping, and "
        "someone said it made their week.",
        "I looked after someone properly this year and felt the "
        "quiet pride of it for days.",
        "Patience, checking in, small kindnesses: my care skills "
        "grew the way the best things grow, quietly.",
    ],
}

_SKILL_LINE_FALLBACK = "I practised %s until it started to stick."


def _skill_line(env, skill_id):
    """Cosmetic variant, avoiding recent text until its pool is exhausted.
    Renew the oldest picks from this skill on exhaustion; other skills'
    history stays intact. The bounded envelope history is undo-safe."""
    lines = _SKILL_LINES.get(skill_id) or [_SKILL_LINE_FALLBACK % skill_id]
    from src.appearance import integration
    rng = integration._APP_RNG  # cosmetic stream; mounted per command
    recents = env.setdefault(SKILL_LINE_RECENTS_KEY, [])
    recent = set(recents)
    eligible = [t for t in lines if t not in recent]
    while not eligible:
        oldest = next(i for i, text in enumerate(recents) if text in lines)
        recents.pop(oldest)
        eligible = [t for t in lines if t not in recents]
    text = rng.choice(eligible)
    recents.append(text)
    del recents[:-SKILL_LINE_WINDOW]
    return text


def _invalid(msg):
    return GameError("invalid_input", msg, 400)


def _not_allowed(msg):
    return GameError("not_allowed", msg, 409)


def _smarts_bonus(player, rules):
    bonus = rules.get("smarts_learning_bonus") or {}
    raw = (getattr(player, "smarts", 0) // 10) * bonus.get("per_10_smarts", 0)
    return min(raw, bonus.get("cap", 0))


def check_eligibility(env, player, option, skill_id):
    """Returns (ok, reason). No side effects; affordability included."""
    age = getattr(player, "age", 0)
    req = option.get("requires") or {}
    if "min_age" in req and age < req["min_age"]:
        return False, "you are too young for this (age %d, needs %d)" % (
            age, req["min_age"])
    if option.get("choose_skill"):
        # No preselected skill: the UI must offer the choice; the
        # skill-specific saturation check defers to dispatch with the
        # chosen skill.
        if skill_id and data.skill_def(skill_id) is None:
            return False, "unknown skill %r" % (skill_id,)
    else:
        skill_id = option["skill"]
    uses = state.uses_this_year(env, player.person_id)
    used = uses.get(option["id"], 0)
    if used >= option["annual_limit"]:
        return False, "already used this %d time%s this year" % (
            option["annual_limit"], "s" if option["annual_limit"] > 1 else "")
    values = state.values(env, player.person_id)
    check_skill = skill_id if option.get("choose_skill") else option["skill"]
    if check_skill and values[check_skill] >= 100:
        return False, "already at the maximum"
    cost = (option.get("cost") or {}).get("money", 0)
    if cost and getattr(player, "money", 0) < cost:
        return False, ("this training costs $%d and you have $%d"
                       % (cost, player.money))
    return True, None


def apply_training(env, player, option_or_id, skill_id, rng, occurrence=None,
                   svc=None):
    """THE single gain applier: rolls the bounded gain, clamps to headroom,
    journals the ACTUAL applied delta, records use/history. Used by the
    train command, the annual hook and any event option that trains.
    option_or_id is a training option id, or an explicit option dict
    (the annual hook's synthetic job/study option). Returns
    (skill_id, applied_gain) — gain 0 means 'no headroom', and the
    caller must not have charged for it."""
    if isinstance(option_or_id, dict):
        option = option_or_id
    else:
        option = data.option(option_or_id)
        if option is None:
            raise _invalid("unknown training option %r" % (option_or_id,))
    if option.get("choose_skill"):
        if data.skill_def(skill_id) is None:
            raise _invalid("unknown skill %r" % (skill_id,))
    else:
        skill_id = option["skill"]
    values = state.values(env, player.person_id)
    headroom = 100 - values[skill_id]
    if headroom <= 0:
        return skill_id, 0
    prog = option["progress"]
    gain = rng.randint(prog["min"], prog["max"])
    gain += _round_stochastic(rng, _smarts_bonus(player, data.load()["rules"]))
    gain = max(1, min(gain, headroom))
    year = getattr(player, "age", 0)
    state.add_gain(env, player.person_id, skill_id, gain, option["id"], year)
    if occurrence is None:
        occurrence = "skills:%s:%s:%s:%s" % (player.person_id, option["id"],
                                             year,
                                             state.use_count(env,
                                                             player.person_id,
                                                             option["id"]) + 1)
    state.bump_use(env, player.person_id, option["id"])
    if gain and svc is not None:
        text = _skill_line(env, skill_id)
        svc._journal(env, "skill", text, [player.person_id],
                     {skill_id: gain})
    return skill_id, gain


def cmd_train(env, payload, rng, svc):
    """`train_skill {target_id, skill?}` — validated command body.
    Eligibility (incl. affordability) is proven BEFORE any spend; the
    ledger post uses a per-year occurrence key so replay applies nothing
    twice."""
    player = env["player"]
    option_id = payload.get("target_id") or payload.get("option_id")
    if not isinstance(option_id, str):
        raise _invalid("target_id is required")
    option = data.option(option_id)
    if option is None:
        raise _invalid("unknown training option %r" % (option_id,))
    skill_id = payload.get("skill")
    if skill_id is not None and not isinstance(skill_id, str):
        raise _invalid("skill must be a string")
    if option.get("choose_skill") and not skill_id:
        raise _invalid("skill is required for this training option")
    ok, reason = check_eligibility(env, player, option, skill_id)
    if not ok:
        raise _not_allowed(reason)
    cost = (option.get("cost") or {}).get("money", 0)
    if cost:
        from src.game.finance import Ledger
        ledger = Ledger(env, resolver=svc._make_resolver(env))
        turn_id = env["turn"].get("turn_id", "t_%s" % env["revision"])
        # count BEFORE the gain so occurrence n matches the year's nth use
        n = state.use_count(env, player.person_id, option_id) + 1
        occurrence = "skills:%s:%s:%s:%s" % (player.person_id, option_id,
                                             player.age, n)
        ledger.post(turn_id, player.age, {
            "phase": "training",
            "occurrence_id": occurrence,
            "account_owner_id": player.person_id,
            "category": "education",
            "amount": -cost,
            "description": "Training: %s" % option["label"],
        })
    applied_skill, gain = apply_training(env, player, option_id, skill_id,
                                         rng, svc=svc)
    return {"trained": option_id, "skill": applied_skill, "gain": gain}


def annual_hook(env, rng, svc):
    """Once-per-year bounded contributions from job practice and study.
    Runs after work_income, before education (A's registry). Job
    practice reads A's frozen declaration for the ACTIVE assignment
    (careers.core.practiced_skill); study reads B's program
    declaration (study_skill) for the ACTIVE enrollment. Absent active
    sources are a no-op. Occurrence keys are persisted per year so
    reload/replay cannot double-apply."""
    player = env["player"]
    rules = data.load()["rules"]
    declares = _active_contributions(env, player)
    for source, rule_key in (("job", "job_practice_per_year"),
                             ("study", "study_contribution_per_year")):
        skill_id = declares.get(source)
        if not skill_id or data.skill_def(skill_id) is None:
            continue
        values = state.values(env, player.person_id)
        if values[skill_id] >= 100:
            continue
        # the annual contribution IS a real option passed to the shared
        # applier (id/progress/skill), not a lookup key
        option = {"id": "annual_%s" % source, "skill": skill_id,
                  "progress": rules[rule_key], "choose_skill": False}
        existing = state.person_state(env, player.person_id, create=True)
        already = any(h.get("year") == player.age
                      and h.get("option_id") == option["id"]
                      for h in existing["history"])
        if already:
            continue
        occurrence = "skills:annual:%s:%s:%s" % (source, player.person_id,
                                                 player.age)
        apply_training(env, player, option, skill_id, rng,
                       occurrence=occurrence, svc=svc)


def _active_contributions(env, player):
    """{job: skill_id, study: skill_id} from the player's ACTIVE
    assignment and enrollment, via A/B's frozen declarations.
    Read-only; no active source -> {} (no-op hook)."""
    out = {}
    try:
        from src.game.features.careers import core as careers_core
        asn = careers_core.active_assignment(env, player.person_id)
        if asn:
            skill = careers_core.practiced_skill(asn["role_id"])
            if skill:
                out["job"] = skill
    except Exception:
        pass
    try:
        from src.game.features.education import core as education_core
        enr = education_core.active_enrollment(env, player.person_id)
        if enr:
            program = education_core.data().get(enr["program_id"]) or {}
            skill = program.get("study_skill")
            if skill:
                out["study"] = skill
    except Exception:
        pass
    return out


def action_specs(env):
    """The frozen action-spec dialect for the projection. ONE dispatch
    shape: every option is advertised as action `train_skill` with
    `target_id` = the training option id (the same dialect Codex pinned
    in the module's dispatch) — never per-option action ids."""
    player = env["player"]
    specs = []
    for option in data.training_options():
        skill_id = option["skill"]
        ok, reason = check_eligibility(env, player, option, None)
        cost = (option.get("cost") or {}).get("money", 0)
        labels = []
        if skill_id:
            labels.append("trains %s" % data.skill_def(skill_id)["label"])
        else:
            labels.append("choose a skill")
        if cost:
            labels.append("Costs $%d" % cost)
        specs.append({
            "id": "train_skill",
            "label": option["label"],
            "category": "activities",
            "target_id": option["id"],
            # label is self-sufficient — a duplicated target_label would
            # render twice in the generic controls ("X — X")
            "target_label": (data.skill_def(skill_id)["label"]
                             if skill_id else None),
            "inputs": ([{"id": "skill", "type": "choice", "label": "Skill",
                         "required": True,
                         "choices": [s["id"] for s in data.load()["skills"]]}]
                       if option.get("choose_skill") else []),
            "known_costs": ([{"kind": "money", "amount": cost}] if cost
                            else []),
            "limits": ["%d per year" % option["annual_limit"]],
            "enabled": ok,
            "reason": reason,
            "_labels": labels,
        })
    return specs
