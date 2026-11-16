"""Headless gameplay rules extracted from the legacy terminal menus.

M2 rule boundary: Kimi C implements, Kimi A (src/game) consumes; A owns the
phase map and the ledger entry schema (docs/collab/M2/), this module conforms
to them. No terminal I/O: no print, input, yes_no, display_event or
clear_screen. Terminal menus stay as presentation wrappers.

`context` is duck-typed: a Player or any object with a `.player` attribute.
Functions never import src.game (no cycles) and never save.

RuleResult is a plain dict:
    {"events": [{"kind", "text", "person_ids", "changes"}],
     "money_entries": [{phase, occurrence_id, leg, account_owner_id,
                        category, amount, description}],
     "pending_decision": None | {...},
     "continuation": None | {"phase_id": str, "item_cursor": int},
     "finished_turn": bool}

Money: rule functions NEVER mutate money; every cash movement is a ledger
entry request in A's schema and A's finance.Ledger posts it once.

Identity: events/postings use the canonical `person.person_id` (A's
authority), never save-slot `ID` and never fabricated test IDs.

Randomness: all draws go through the passed `rng` (random.Random-shaped),
which A's per-game adapter locks and persists. Choice outcomes (job
interview, university acceptance/funding, retirement) are sampled when the
offer is created and stored in the pending decision's private "sampled"
block; resolve_choice applies only stored data and never rolls.
"""

import math

from src.lifesim_lib.lifesim_lib import Gender, calculate_tax, clamp
from src.people.classes.player import (
    annual_income_quote,
    sample_expense_retention,
)


# --------------------------------------------------------------------- result

def _result(events=None, money_entries=None, pending_decision=None,
            continuation=None, finished_turn=False):
    return {
        "events": events or [],
        "money_entries": money_entries or [],
        "pending_decision": pending_decision,
        "continuation": continuation,
        "finished_turn": finished_turn,
    }


def _pid(person):
    return person.person_id


def _event(kind, text, person=None, changes=None):
    return {
        "kind": kind,
        "text": text,
        "person_ids": [_pid(person)] if person is not None else [],
        "changes": changes or {},
    }


def _money(phase, occurrence_id, leg, person, category, amount,
           description):
    """One ledger posting request in A's schema (src/game/finance.py);
    A owns source keys, posting and balances."""
    return {
        "phase": phase,
        "occurrence_id": occurrence_id,
        "leg": leg,
        "account_owner_id": _pid(person),
        "category": category,
        "amount": int(amount),
        "description": description,
    }


def _income_legs(phase, person, quote):
    """Gross/tax/expenses legs; net == gross - tax - expenses holds by
    construction (residual allocated to expenses in annual_income_quote)."""
    return [
        _money(phase, "salary", "gross", person, "income", quote["gross"],
               "Annual gross pay"),
        _money(phase, "salary", "tax", person, "tax", -quote["tax"],
               "Income tax"),
        _money(phase, "salary", "expenses", person, "expenses",
               -quote["expenses"], "Living expenses"),
    ]


def _player(context):
    """Duck-typed context: a Player or an object exposing .player."""
    return getattr(context, "player", context)


def _check(cond, code, message):
    if not cond:
        raise RuleError(code, message)


class RuleError(Exception):
    """Typed rule failure; maps to a 4xx API error. No mutation on raise."""

    def __init__(self, code, message):
        super().__init__(message)
        self.code = code
        self.message = message


# Randomness wrappers: every draw goes through the caller-supplied rng so
# A's per-game RNG adapter owns the whole stream.
def _one_in(rng, x):
    return x <= 1 or rng.randint(1, x) == 1


def _x_in_y(rng, x, y):
    return rng.uniform(0, y) < x


def _randexpo(rng, lo, avg):
    return lo + rng.expovariate(1 / (avg - lo))


def _round_stochastic(rng, value):
    low = math.floor(value)
    high = math.ceil(value)
    if value < 0:
        return low if rng.random() < high - value else high
    return high if rng.random() < value - low else low


# ==================================================================== ACTIONS
# Source: src/menus/main.py School menu and Job branches. Legacy numbers are
# preserved exactly unless a deviation is called out in a comment.


# ---- School (parity matrix rows 15-17)

def study_harder(player, rng):
    """main.py "Study harder": grades/smarts gain, once per year."""
    _check(player.is_in_school(), "not_allowed", "you are not in school")
    _check(not player.studied, "not_allowed", "you already studied this year")
    before_grades, before_smarts = player.grades, player.smarts
    player.change_grades(rng.randint(5, 7 + (100 - player.grades) // 5))
    player.change_smarts(rng.randint(0, 2) + player.has_trait("NERD"))
    if _x_in_y(rng, player.smarts, 2000):
        player.learn_trait("NERD")
    player.studied = True
    return _result(events=[_event(
        "education", "You began studying harder", player,
        {"grades": player.grades - before_grades,
         "smarts": player.smarts - before_smarts})])


def skip_school(player, rng):
    """main.py "Skip school": core losses apply every time (legacy), the
    positive fun reward only the first time; 1/5 caught (never in uv)."""
    _check(player.is_in_school(), "not_allowed", "you are not in school")
    before_smarts, before_grades = player.smarts, player.grades
    player.change_smarts(-2)
    player.change_grades(-rng.randint(4, 8))
    events = [_event("education", "You skipped school.", player,
                     {"smarts": player.smarts - before_smarts,
                      "grades": player.grades - before_grades})]
    if player.uv_years == 0 and _one_in(rng, 5):
        before_happy = player.happiness
        player.change_happiness(-rng.randint(15, 25))
        player.change_karma(-rng.randint(1, 6))
        events.append(_event(
            "education",
            "You were caught skipping school! You were sent to the "
            "principal's office and got detention.",
            player, {"happiness": player.happiness - before_happy}))
    elif not player.skipped_school:
        before_happy = player.happiness
        player.change_happiness(rng.randint(3, 7))
        player.change_karma(-rng.randint(1, 6))
        events.append(_event("education", "You had a good time.",
                             player,
                             {"happiness": player.happiness - before_happy}))
    player.skipped_school = True
    return _result(events=events)


def drop_out(player, rng):
    """main.py "Drop out": eligibility, parents' refusal branch.

    Student-loan policy (Claude 2026-11-16): tuition financed but not yet
    materialized as debt is not retroactively charged; the stale funding
    flag clears when no principal exists; existing debt survives."""
    _check(player.is_in_school(), "not_allowed", "you are not in school")
    can_drop_out = player.smarts < rng.randint(8, 12) + rng.randint(0, 13)
    can_drop_out = can_drop_out and not player.tried_to_drop_out
    if (player.age >= 18 or player.uv_years > 0
            or (player.age >= rng.randint(15, 16) and can_drop_out)):
        player.dropped_out = True
        player.grades = None
        if player.uv_years > 0:
            player.uv_years = 0
            if player.student_loan == 0:
                player.chose_student_loan = False
        return _result(events=[_event(
            "education", "You dropped out of school.", player)])
    player.tried_to_drop_out = True
    return _result(events=[_event(
        "education", "Your parents won't let you drop out of school.",
        player)])


# ---- Jobs (parity matrix rows 18-22)

def sample_job_offer(player, rng):
    """main.py "Find a Job": sample the offer up front so the decision can
    be stored without rerolling."""
    return _round_stochastic(rng, _randexpo(rng, 30000, 65000))


def sample_job_interview(player, salary, rng):
    """main.py interview roll, sampled at OFFER time and stored privately;
    resolve applies only this result."""
    m = 100 + _round_stochastic(rng, (salary - 40000) / 300)
    mod = 50 - player.smarts
    if rng.randint(1, 3) == 1:
        mod += round((50 - player.karma) / 2)
    roll = rng.randint(1, m)
    if mod > 0:
        roll += rng.randint(0, mod)
    elif mod < 0:
        roll -= rng.randint(0, abs(mod))
    success = roll <= 100
    return {"success": success,
            "happiness_delta": 4 if success else -rng.randint(1, 4)}


def work_harder(player, rng):
    """main.py "Work Harder": performance gain, once per year."""
    _check(player.has_job, "not_allowed", "you don't have a job")
    _check(not player.worked_harder, "not_allowed",
           "you already worked harder this year")
    before_perf, before_stress = player.performance, player.stress
    player.change_performance(rng.randint(1, 10))
    player.change_stress(4)
    if player.has_trait("LAZY"):
        player.change_stress(6)
    player.worked_harder = True
    return _result(events=[_event(
        "work", "You worked harder.", player,
        {"performance": player.performance - before_perf,
         "stress": player.stress - before_stress})])


def adjust_hours(player, hours):
    """main.py "Adjust Hours": 38-70, stress reacts to the change."""
    _check(player.has_job, "not_allowed", "you don't have a job")
    _check(isinstance(hours, int) and not isinstance(hours, bool),
           "invalid_input", "hours must be an integer")
    _check(38 <= hours <= 70, "invalid_input",
           "hours must be between 38 and 70")
    before_stress = player.stress
    player.update_hours(hours)
    return _result(events=[_event(
        "work", f"Your work hours are now {hours}.", player,
        {"stress": player.stress - before_stress})])


def request_raise(player, rng):
    """main.py "Request a Raise": gate, approval %, fired-for-asking branch."""
    _check(player.has_job, "not_allowed", "you don't have a job")
    _check(not player.asked_for_raise, "not_allowed",
           "you already asked for a raise this year")
    eligible = (player.age - player.last_raise >= 10
                and player.performance >= rng.randint(40, 120))
    if eligible:
        perc = round(rng.randint(20, 85) / 10, 2)
        before_salary = player.salary
        player.salary += round(player.salary * perc / 100)
        player.times_asked_since_last_raise = 0
        player.last_raise = player.age
        player.asked_for_raise = True
        return _result(events=[_event(
            "work", f"Your request for a raise has been approved. "
                    f"You got a raise of {perc}%.", player,
            {"salary": player.salary - before_salary})])
    events = [_event("work", "Your request for a raise has been rejected.",
                     player)]
    fired = (player.times_asked_since_last_raise >= 2
             and rng.randint(1, 9) == 1)
    if fired:
        player.lose_job()
        before_happy = player.happiness
        player.change_happiness(-rng.randint(25, 35))
        events.append(_event(
            "work", "Your boss fired you for asking for a raise.", player,
            {"happiness": player.happiness - before_happy}))
    player.times_asked_since_last_raise += 1
    player.asked_for_raise = True
    return _result(events=events)


def can_retire(player):
    return player.years_worked >= 10 and player.age >= 65


def retire(player, rng):
    """main.py "Retire": pension from calc_pension, job state cleared.
    Per the phase-map income policy, the pension starts paying NEXT turn;
    this turn's pay was already quoted from the employment state."""
    _check(player.has_job, "not_allowed", "you don't have a job")
    _check(can_retire(player), "not_allowed",
           "you are not eligible to retire")
    pension = player.calc_pension()
    player.lose_job()
    player.salary = pension
    before_happy = player.happiness
    player.change_happiness(rng.randint(25, 50))
    return _result(events=[_event(
        "work",
        f"You retired and are now receiving a yearly pension of "
        f"${pension:,}.", player,
        {"happiness": player.happiness - before_happy})])


def quit_job(player):
    """main.py "Quit Job"."""
    _check(player.has_job, "not_allowed", "you don't have a job")
    player.lose_job()
    return _result(events=[_event("work", "You quit your job.", player)])


# =============================================================== YEARLY PHASES

def yearly_work_phase(player, turn_id, rng, year_start_happiness=None):
    """income phase (parity rows 47, 54, 55, 71, 72, 73, 74, 75).

    Subrules run in legacy age_up/random_events order:
    stress +-4, stress drift, salary history + happiness-swing performance,
    retirement offer / forced retirement, performance firing, stress damage,
    then the annual pay quote (employment state as of this phase; a
    mid-turn retiree's pension starts NEXT turn).
    """
    events = []
    pending = None
    if player.has_job:
        # age_up opening stress jitter
        player.change_stress(rng.randint(-4, 4))
        # Stress drift model
        base = 60 - player.happiness * 0.3 + (player.job_hours - 40)
        base += 4 * math.sqrt(sum(1 for c in player.children if c.age < 18))
        base = clamp(base, 0, 100)
        if player.has_trait("MEDITATOR"):
            base *= 2 / 3
        diff = base - player.stress
        if diff > 0:
            player.change_stress(
                rng.randint(0, _round_stochastic(rng, diff / 6)))
        elif diff < 0:
            factor = player.happiness / 25 + 0.5
            if player.has_trait("MEDITATOR"):
                factor += 0.5
            player.change_stress(
                -rng.randint(0, _round_stochastic(rng, abs(diff) / 9 * factor)))
        # Salary history; happiness-swing stress/performance
        player.salary_years.append(player.salary)
        if year_start_happiness is not None:
            happy_change = player.happiness - year_start_happiness
            if happy_change > 0:
                diff2 = max(happy_change - 5, 0) / 2
            else:
                diff2 = min(happy_change + 5, 0)
            player.change_stress(-_round_stochastic(rng, diff2 / 4))
        player.change_performance(
            rng.randint(-4, 4)
            + _round_stochastic(rng, (50 - player.stress) / 20))
        # Retirement offer (sampled now; the ANSWER is a stored decision)
        if can_retire(player) and _one_in(rng, 3):
            pension = player.calc_pension()
            if player.age >= 70 and _one_in(rng, 3):
                # Forced retirement is an EVENT, not a choice.
                _do_retire(player, pension, rng, events)
            else:
                pending = {
                    "kind": "retirement_offer",
                    "title": "Retirement",
                    "text": f"You are eligible to retire and begin "
                            f"receiving a yearly pension of ${pension:,}.",
                    "options": [{"id": "retire", "label": "Retire"},
                                {"id": "keep_working",
                                 "label": "Keep working"}],
                    "sampled": {"pension": pension},
                }
        elif player.performance < 15 \
                and rng.randint(1, player.performance + 1) == 1:
            player.lose_job()
            before_happy = player.happiness
            player.change_happiness(-rng.randint(20, 35))
            events.append(_event(
                "work", "You have been fired from your job.\nReason: "
                        "Performance", player,
                {"happiness": player.happiness - before_happy}))
        # Stress damage / high blood pressure
        if player.has_job and player.stress > 65:
            player.change_happiness(-rng.randint(
                0, _round_stochastic(rng, (player.stress - 65) / 5)))
            critical = player.stress > 85
            if critical:
                # Rule policy (documented fix): legacy ADDED health here
                # (positive change under critical stress); damage is applied.
                player.change_health(
                    -_round_stochastic(rng, (player.stress - 80) / 4))
            if critical and "High Blood Pressure" not in player.illnesses \
                    and _one_in(rng, 7):
                player.add_illness("High Blood Pressure")
                player.change_health(-rng.randint(4, 8))
                events.append(_event(
                    "health", "You are suffering from high blood pressure.",
                    player))
    if player.has_job:
        player.years_worked += 1  # legacy random_events increments here
    money = []
    if player.salary > 0:
        retention = sample_expense_retention(rng)
        quote = annual_income_quote(player.salary, player.has_job,
                                    player.job_hours, retention)
        money.extend(_income_legs("income", player, quote))
    return _result(events=events, money_entries=money,
                   pending_decision=pending,
                   continuation={"phase_id": "education", "item_cursor": 0})


def resolve_retirement_choice(player, pending, option_id, rng):
    """Resolve a stored retirement offer; applies stored pension only."""
    _check(option_id in [o["id"] for o in pending.get("options", [])],
           "invalid_input", "unknown option")
    events = []
    sampled = pending.get("sampled", {})
    if sampled.get("applied"):
        return _result(events=events,
                       continuation={"phase_id": "education",
                                     "item_cursor": 0})
    if option_id == "retire":
        _do_retire(player, sampled["pension"], rng, events)
        sampled["applied"] = True
    return _result(events=events,
                   continuation={"phase_id": "education", "item_cursor": 0})


def _do_retire(player, pension, rng, events):
    player.lose_job()
    player.salary = pension
    before_happy = player.happiness
    player.change_happiness(rng.randint(25, 50))
    events.append(_event(
        "work", f"You retired and are now receiving a yearly pension of "
                f"${pension:,}.", player,
        {"happiness": player.happiness - before_happy}))


def yearly_education_phase(player, turn_id, rng):
    """education phase (rows 56, 62, 64, 65, 66) in legacy order:
    uv progression/expulsion/graduation, grade drift, school transitions,
    then the age-17 graduation + university application (a stored decision
    with pre-sampled acceptance and per-option funding outcomes)."""
    events = []
    pending = None
    if player.uv_years > 0:
        player.uv_years -= 1
        if player.uv_years == 0:
            player.grades = None
            player.change_happiness(rng.randint(14, 20))
            player.change_smarts(rng.randint(10, 15))
            events.append(_event("education",
                                 "You graduated from university.", player))
            if player.chose_student_loan:
                player.student_loan = rng.randint(20000, 40000)
                events.append(_event(
                    "education",
                    "You now have to start paying back your student loan",
                    player))
        elif player.grades is not None \
                and player.grades < rng.randint(10, 45):
            player.uv_years = 0
            player.grades = None
            # Student-loan policy: no retroactive tuition debt.
            if player.student_loan == 0:
                player.chose_student_loan = False
            player.change_happiness(-rng.randint(30, 50))
            events.append(_event(
                "education",
                "You were expelled from university after earning bad grades.",
                player))
    if player.is_in_school():
        player.change_grades(rng.randint(-3, 3))
        base = round(10 * math.sqrt(player.smarts))
        if player.grades < base - 2:
            player.change_grades(rng.randint(1, 3))
        elif player.grades > base + 2:
            player.change_grades(-rng.randint(1, 3))
        delta = (player.happiness - 50) / 10
        if delta > 0:
            delta /= 2
        player.change_grades(_round_stochastic(rng, delta))
    if player.age == 6:
        player.change_smarts(rng.randint(1, 2)
                             + 2 * player.has_trait("GENIUS"))
        player.calc_grades(rng.randint(4, 8))
        events.append(_event("education", "You are starting elementary school",
                             player))
    if player.age == 12:
        player.change_smarts(rng.randint(1, 3)
                             + 3 * player.has_trait("GENIUS"))
        player.calc_grades(rng.randint(0, 8))
        events.append(_event("education", "You are starting middle school",
                             player))
    if player.age == 14:
        player.change_smarts(rng.randint(1, 4)
                             + 4 * player.has_trait("GENIUS"))
        player.calc_grades(rng.randint(-8, 8))
        events.append(_event("education", "You are starting high school",
                             player))
    if player.age == 17 and not player.dropped_out:
        player.grades = None
        player.change_happiness(rng.randint(15, 20))
        player.change_smarts(rng.randint(6, 10))
        events.append(_event("education",
                             "You graduated from high school.", player))
        if player.smarts >= rng.randint(30, 44):
            pending = _sample_university_application(player, rng)
        else:
            events.append(_event(
                "education", "Your application to university was rejected.",
                player))
    return _result(events=events, pending_decision=pending,
                   continuation={"phase_id": "debt_service", "item_cursor": 0})


def _sample_university_application(player, rng):
    """Age-17 flow, legacy main.py:1095-1160. Acceptance and every funding
    option's outcome are sampled NOW; resolve applies stored data only."""
    funding_options = [{"id": "scholarship", "label": "Scholarship"},
                       {"id": "student_loan", "label": "Student Loan"}]
    if player.parents:
        funding_options.append({"id": "parents",
                                "label": "Ask parents to pay"})
    outcomes, effects = {}, {}
    outcomes["scholarship"] = (
        player.smarts >= rng.randint(rng.randint(75, 85), 100))
    effects["scholarship"] = (
        rng.randint(10, 15) + 10 * player.has_trait("CHEERFUL")
        if outcomes["scholarship"] else -rng.randint(7, 9))
    outcomes["student_loan"] = True
    effects["student_loan"] = 0
    if player.parents:
        avg = sum(p.generosity for p in player.parents.values()) \
            / len(player.parents)
        outcomes["parents"] = _x_in_y(rng, (avg / 100) ** 4, 1.0)
        effects["parents"] = (
            rng.randint(7, 9) + 7 * player.has_trait("CHEERFUL")
            if outcomes["parents"] else -rng.randint(7, 9))
    return {
        "kind": "university_application",
        "title": "University",
        "text": "Would you like to apply to university?",
        "options": [{"id": "apply", "label": "Yes"},
                    {"id": "decline", "label": "No"}],
        "sampled": {
            "accepted": True,
            "funding_options": funding_options,
            "funding_outcomes": outcomes,
            "funding_effects": effects,
            "spent": [],
        },
    }


def resolve_university_application(player, pending, option_id, rng):
    """Apply/decline for the age-17 application. Accepting an accepted
    application yields the funding decision (pre-sampled outcomes)."""
    _check(option_id in [o["id"] for o in pending.get("options", [])],
           "invalid_input", "unknown option")
    events = []
    if option_id == "decline":
        return _result(events=events,
                       continuation={"phase_id": "debt_service",
                                     "item_cursor": 0})
    funding_pending = {
        "kind": "university_funding",
        "title": "University",
        "text": ("Your application to university was accepted! How would "
                 "you like to pay for your college tuition?"),
        "options": pending["sampled"]["funding_options"],
        "sampled": pending["sampled"],
    }
    return _result(events=events, pending_decision=funding_pending,
                   continuation={"phase_id": "debt_service",
                                 "item_cursor": 0})


def resolve_university_funding(player, pending, option_id, rng):
    """Resolve a stored funding choice using its pre-sampled outcome.
    Refusal removes that option from the re-offered decision (legacy's
    re-prompt loop); only currently listed options are accepted."""
    available = [o["id"] for o in pending.get("options", [])]
    _check(option_id in available, "invalid_input",
           "unknown or no-longer-available option")
    sampled = pending["sampled"]
    outcome = sampled["funding_outcomes"][option_id]
    happy_delta = sampled["funding_effects"][option_id]
    events = []
    if happy_delta:
        before_happy = player.happiness
        player.change_happiness(happy_delta)
        events.append(_event("education", "", player,
                             {"happiness": player.happiness - before_happy}))
    if outcome:
        _enroll_university(player, rng, events)
        return _result(events=events,
                       continuation={"phase_id": "debt_service",
                                     "item_cursor": 0})
    events.append(_event(
        "education",
        {"scholarship": "Your scholarship application was rejected.",
         "parents": "Your parents refused to pay for your university "
                    "tuition."}.get(option_id, "Your request was refused."),
        player))
    spent = list(sampled.get("spent", [])) + [option_id]
    sampled["spent"] = spent
    reoffer = dict(pending)
    reoffer["options"] = [o for o in pending["options"]
                          if o["id"] not in spent]
    return _result(events=events, pending_decision=reoffer,
                   continuation={"phase_id": "debt_service",
                                 "item_cursor": 0})


def _enroll_university(player, rng, events):
    player.uv_years = 4
    player.calc_grades(rng.randint(-8, 10))
    events.append(_event("education", "You are now enrolled in university.",
                         player))


# ================================================================== DISPATCH

def _school_action_specs(player):
    return [
        {"id": "study_harder", "category": "education",
         "label": "Study harder", "enabled": not player.studied,
         "reason": None if not player.studied else "already studied this year",
         "limits": ["once per year"]},
        {"id": "skip_school", "category": "education",
         "label": "Skip school", "enabled": True,
         "reason": None, "limits": []},
        {"id": "drop_out", "category": "education", "label": "Drop out",
         "enabled": True, "reason": None, "limits": []},
    ]


def _work_action_specs(player):
    specs = []
    if player.age >= 18 and not player.has_job \
            and not player.is_in_school():
        # Legacy main menu: School and Job branches are mutually exclusive,
        # so university students never see job actions.
        specs.append({"id": "find_job", "category": "work",
                      "label": "Find a job", "enabled": True,
                      "reason": None, "limits": []})
    if player.has_job:
        specs.append({"id": "work_harder", "category": "work",
                      "label": "Work harder", "enabled": not player.worked_harder,
                      "reason": None if not player.worked_harder
                      else "already worked harder this year",
                      "limits": ["once per year"]})
        specs.append({"id": "adjust_hours", "category": "work",
                      "label": "Adjust hours",
                      "inputs": [{"id": "hours", "type": "number",
                                  "label": "Hours per week",
                                  "min": 38, "max": 70, "required": True,
                                  "integer": True}],
                      "enabled": True, "reason": None, "limits": []})
        specs.append({"id": "request_raise", "category": "work",
                      "label": "Request a raise",
                      "enabled": not player.asked_for_raise,
                      "reason": None if not player.asked_for_raise
                      else "already asked this year",
                      "limits": ["once per year"]})
        retiring = can_retire(player)
        specs.append({"id": "retire" if retiring else "quit_job",
                      "category": "work",
                      "label": "Retire" if retiring else "Quit job",
                      "enabled": True, "reason": None, "limits": []})
    return specs


def action_specs(context):
    """Allowed actions for the player right now; one shared eligibility
    predicate drives both this projection and the dispatcher. Pure read."""
    p = _player(context)
    if p.is_in_school():
        return _school_action_specs(p)
    return _work_action_specs(p)


_ACTIONS = {
    "study_harder": lambda p, payload, rng: study_harder(p, rng),
    "skip_school": lambda p, payload, rng: skip_school(p, rng),
    "drop_out": lambda p, payload, rng: drop_out(p, rng),
    "work_harder": lambda p, payload, rng: work_harder(p, rng),
    "adjust_hours": lambda p, payload, rng: adjust_hours(
        p, _require_int(payload, "hours", 38, 70)),
    "request_raise": lambda p, payload, rng: request_raise(p, rng),
    "retire": lambda p, payload, rng: retire(p, rng),
    "quit_job": lambda p, payload, rng: quit_job(p),
}


def _require_int(payload, key, lo, hi):
    value = payload.get(key)
    _check(isinstance(value, (int, float)) and not isinstance(value, bool),
           "invalid_input", f"{key} must be a number")
    _check(float(value).is_integer(), "invalid_input",
           f"{key} must be an integer")
    value = int(value)
    _check(lo <= value <= hi, "invalid_input",
           f"{key} must be between {lo} and {hi}")
    return value


def apply_action(context, action_id, payload, rng):
    """Validate and apply one menu action; returns a RuleResult.

    find_job is two-step: the offer (salary + interview outcome) is sampled
    NOW and stored in the pending decision; resolve_choice applies it.
    """
    p = _player(context)
    _check(isinstance(payload, dict), "invalid_input",
           "payload must be an object")
    if action_id == "find_job":
        _check(p.age >= 18, "not_allowed", "you are too young to work")
        _check(not p.has_job, "not_allowed", "you already have a job")
        _check(not p.is_in_school(), "not_allowed",
               "you cannot work while enrolled in school")
        salary = sample_job_offer(p, rng)
        interview = sample_job_interview(p, salary, rng)
        return _result(pending_decision={
            "kind": "job_offer",
            "title": "Job offer",
            "text": f"You found a job with a salary of ${salary:,}. "
                    f"Would you like to apply?",
            "options": [{"id": "apply", "label": "Apply"},
                        {"id": "walk_away", "label": "Walk away"}],
            "sampled": {"salary": salary, **interview},
        })
    fn = _ACTIONS.get(action_id)
    _check(fn is not None, "invalid_input", f"unknown action: {action_id!r}")
    return fn(p, payload, rng)


def resolve_choice(context, pending, option_id, inputs, rng):
    """Resolve a stored pending decision. Only options currently listed in
    the pending decision are accepted; resolution applies the pre-sampled
    outcome and never rolls against ambient RNG (except outcomes legacy
    genuinely defers, which are sampled at offer creation)."""
    p = _player(context)
    _check(isinstance(pending, dict), "invalid_input", "invalid pending")
    _check(isinstance(option_id, str), "invalid_input",
           "option_id must be a string")
    kind = pending.get("kind")
    if kind == "job_offer":
        options = [o["id"] for o in pending.get("options", [])]
        _check(option_id in options, "invalid_input", "unknown option")
        if option_id == "walk_away":
            return _result(events=[_event("work", "You walked away.", p)])
        sampled = pending["sampled"]
        before_happy = p.happiness
        p.change_happiness(sampled["happiness_delta"])
        changes = {"happiness": p.happiness - before_happy}
        if sampled["success"]:
            p.get_job(sampled["salary"])
            return _result(events=[_event(
                "work", "You got the job!", p, changes)])
        return _result(events=[_event(
            "work", "You didn't get an interview.", p, changes)])
    if kind == "retirement_offer":
        return resolve_retirement_choice(p, pending, option_id, rng)
    if kind == "university_application":
        return resolve_university_application(p, pending, option_id, rng)
    if kind == "university_funding":
        return resolve_university_funding(p, pending, option_id, rng)
    raise RuleError("invalid_input", f"unknown pending decision: {kind!r}")


_PHASES = {"P5": yearly_work_phase, "P6": yearly_education_phase,
           "income": yearly_work_phase, "education": yearly_education_phase}


def advance_phase(context, phase_id, rng, turn_id="turn",
                  year_start_happiness=None):
    p = _player(context)
    fn = _PHASES.get(phase_id)
    _check(fn is not None, "invalid_input", f"unknown phase: {phase_id!r}")
    if fn is yearly_work_phase:
        return fn(p, turn_id, rng,
                  year_start_happiness=year_start_happiness)
    return fn(p, turn_id, rng)
