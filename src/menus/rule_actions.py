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

import inspect
import math

from src.lifesim_lib.lifesim_lib import Gender, calculate_tax, clamp
from src.people.classes.player import (
    annual_income_quote,
    sample_expense_retention,
)


# --------------------------------------------------------------------- result

def _result(events=None, money_entries=None, pending_decision=None,
            continuation=None, finished_turn=False, deaths=None):
    return {
        "events": events or [],
        "money_entries": money_entries or [],
        "pending_decision": pending_decision,
        "continuation": continuation,
        "finished_turn": finished_turn,
        "deaths": deaths or [],
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
        "education", "I began studying harder", player,
        {"grades": player.grades - before_grades,
         "smarts": player.smarts - before_smarts})])


def skip_school(player, rng):
    """main.py "Skip school": core losses apply every time (legacy), the
    positive fun reward only the first time; 1/5 caught (never in uv)."""
    _check(player.is_in_school(), "not_allowed", "you are not in school")
    before_smarts, before_grades = player.smarts, player.grades
    player.change_smarts(-2)
    player.change_grades(-rng.randint(4, 8))
    events = [_event("education", "I skipped school.", player,
                     {"smarts": player.smarts - before_smarts,
                      "grades": player.grades - before_grades})]
    if player.uv_years == 0 and _one_in(rng, 5):
        before_happy = player.happiness
        player.change_happiness(-rng.randint(15, 25))
        player.change_karma(-rng.randint(1, 6))
        events.append(_event(
            "education",
            "I was caught skipping school! I was sent to the "
            "principal's office and got detention.",
            player, {"happiness": player.happiness - before_happy}))
    elif not player.skipped_school:
        before_happy = player.happiness
        player.change_happiness(rng.randint(3, 7))
        player.change_karma(-rng.randint(1, 6))
        events.append(_event("education", "I had a good time.",
                             player,
                             {"happiness": player.happiness - before_happy}))
    player.skipped_school = True
    return _result(events=events)


def drop_out(player, rng):
    """main.py "Drop out": eligibility, parents' refusal branch.

    Student-loan policy (Claude 2026-11-16): loan-financed years accrue
    debt when financed (see yearly_education_phase); dropping out keeps
    the accrued debt."""
    _check(player.is_in_school(), "not_allowed", "you are not in school")
    can_drop_out = player.smarts < rng.randint(8, 12) + rng.randint(0, 13)
    can_drop_out = can_drop_out and not player.tried_to_drop_out
    if (player.age >= 18 or player.uv_years > 0
            or (player.age >= rng.randint(15, 16) and can_drop_out)):
        player.dropped_out = True
        player.grades = None
        if player.uv_years > 0:
            player.uv_years = 0
        return _result(events=[_event(
            "education", "I dropped out of school.", player)])
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
        "work", "I worked harder.", player,
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
                    f"I got a raise of {perc}%.", player,
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
        f"I retired and am now receiving a yearly pension of "
        f"${pension:,}.", player,
        {"happiness": player.happiness - before_happy})])


def quit_job(player):
    """main.py "Quit Job"."""
    _check(player.has_job, "not_allowed", "you don't have a job")
    player.lose_job()
    return _result(events=[_event("work", "I quit my job.", player)])


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
                "work", "I was fired from my job.\nReason: "
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
                    "health", "I am suffering from high blood pressure.",
                    player))
    if player.has_job:
        player.years_worked += 1  # legacy random_events increments here
    money = []
    if player.salary > 0:
        retention = sample_expense_retention(rng)
        quote = annual_income_quote(player.salary, player.has_job,
                                    player.job_hours, retention)
        money.extend(_income_legs("work_income", player, quote))
    return _result(events=events, money_entries=money,
                   pending_decision=pending,
                   continuation={"phase_id": "education", "item_cursor": 0})


def resolve_retirement_choice(player, pending, option_id, rng):
    """Resolve a stored retirement offer; applies stored pension only."""
    _check(option_id in [o["id"] for o in pending.get("options", [])],
           "invalid_input", "unknown option")
    events = []
    sampled = pending.get("sampled") or {}
    _check("pension" in sampled, "invalid_input", "missing stored pension")
    if sampled.get("applied"):
        return _result(events=events,
                       continuation={"phase_id": "education",
                                     "item_cursor": 0})
    if option_id == "retire":
        _do_retire(player, sampled["pension"], rng, events)
        sampled["applied"] = True
    return _result(events=events,
                   continuation={"phase_id": "education",
                                 "item_cursor": 0})


def _do_retire(player, pension, rng, events):
    player.lose_job()
    player.salary = pension
    before_happy = player.happiness
    player.change_happiness(rng.randint(25, 50))
    events.append(_event(
        "work", f"I retired and am now receiving a yearly pension of "
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
        # Student-loan policy: tuition financed by a loan becomes debt when
        # financed; each enrolled year accrues a year's tuition (legacy
        # total 20k-40k / 4). Leaving without graduating keeps the debt.
        if player.chose_student_loan:
            player.student_loan += rng.randint(5000, 10000)
        if player.uv_years == 0:
            player.grades = None
            player.change_happiness(rng.randint(14, 20))
            player.change_smarts(rng.randint(10, 15))
            events.append(_event("education",
                                 "I graduated from university.", player))
            if player.chose_student_loan:
                events.append(_event(
                    "education",
                    "I now have to start paying back my student loan",
                    player))
        elif player.grades is not None \
                and player.grades < rng.randint(10, 45):
            player.uv_years = 0
            player.grades = None
            player.change_happiness(-rng.randint(30, 50))
            events.append(_event(
                "education",
                "I was expelled from university after earning bad grades.",
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
        events.append(_event("education", "I am starting elementary school",
                             player))
    if player.age == 12:
        player.change_smarts(rng.randint(1, 3)
                             + 3 * player.has_trait("GENIUS"))
        player.calc_grades(rng.randint(0, 8))
        events.append(_event("education", "I am starting middle school",
                             player))
    if player.age == 14:
        player.change_smarts(rng.randint(1, 4)
                             + 4 * player.has_trait("GENIUS"))
        player.calc_grades(rng.randint(-8, 8))
        events.append(_event("education", "I am starting high school",
                             player))
    if player.age == 17 and not player.dropped_out and player.is_in_school():
        # is_in_school guard: identical to legacy order (17-year-olds are
        # enrolled), and makes the phase idempotent under cursor re-entry
        # (the application must not be re-offered after it resolved once).
        player.grades = None
        player.change_happiness(rng.randint(15, 20))
        player.change_smarts(rng.randint(6, 10))
        events.append(_event("education",
                             "I graduated from high school.", player))
        if player.smarts >= rng.randint(30, 44):
            pending = _sample_university_application(player, rng)
        else:
            events.append(_event(
                "education", "Your application to university was rejected.",
                player))
    return _result(events=events, pending_decision=pending,
                   continuation={"phase_id": "debt_service",
                                 "item_cursor": 0})


def debt_service_phase(player, turn_id, rng):
    """debt_service phase: legacy student-loan repayment (player.py
    random_events). Cash moves via money_entries (A posts); the liability
    balance itself is model state and is reduced here exactly once."""
    money = []
    events = []
    if player.student_loan > 0:
        amount = min(rng.randint(1000, 3000) for _ in range(3))
        amount = min(amount, player.student_loan)
        money.append(_money("debt_service", "student_loan", "repay", player,
                            "loan_repay", -amount,
                            "Student loan repayment"))
        player.student_loan -= amount
        if player.student_loan == 0:
            events.append(_event(
                "finances", "You've fully paid off your student loan",
                player))
    return _result(events=events, money_entries=money,
                   continuation={"phase_id": "family_events",
                                 "item_cursor": 0})


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
    sampled = pending.get("sampled") or {}
    _check(isinstance(sampled.get("funding_options"), list),
           "invalid_input", "missing stored funding options")
    funding_pending = {
        "kind": "university_funding",
        "title": "University",
        "text": ("Your application to university was accepted! How would "
                 "you like to pay for your college tuition?"),
        "options": sampled["funding_options"],
        "sampled": sampled,
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
    outcomes = sampled.get("funding_outcomes") or {}
    effects = sampled.get("funding_effects") or {}
    _check(option_id in outcomes and option_id in effects,
           "invalid_input", "no stored outcome for this option")
    outcome = outcomes[option_id]
    happy_delta = effects[option_id]
    events = []
    if happy_delta:
        before_happy = player.happiness
        player.change_happiness(happy_delta)
        events.append(_event("education", "", player,
                             {"happiness": player.happiness - before_happy}))
    if option_id == "student_loan":
        player.chose_student_loan = True
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
    events.append(_event("education", "I am now enrolled in university.",
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


def _dating_target_available(context):
    """True when the dating-app predicate would bind someone right now —
    action_specs and dispatch must agree (no 'enabled but refused')."""
    env = getattr(context, "_env", None)
    if env is None:
        return True  # plain-player projection without an envelope
    from src.game.events import PREDICATES
    try:
        return PREDICATES["adult_non_related_stranger"](
            _player(context).person_id, context) is not None
    except Exception:
        return False


def _activity_specs(p, context=None):
    specs = []
    if 3 <= p.age < 13:
        specs.append({"id": "play_toys", "category": "activities",
                      "label": "Play with your toys",
                      "enabled": not p.played, "reason": None,
                      "limits": ["once per year"]})
    if p.age >= 5:
        specs.append({"id": "arts_and_crafts", "category": "activities",
                      "label": "Arts and Crafts",
                      "enabled": not p.did_arts_and_crafts,
                      "reason": None if not p.did_arts_and_crafts
                      else "already did arts and crafts this year",
                      "limits": ["once per year"]})
    if p.age >= 12:
        specs.extend([
            {"id": "listen_to_music", "category": "activities",
             "label": "Listen to music", "enabled": not p.listened_to_music,
             "reason": None, "limits": ["once per year"]},
            {"id": "meditate", "category": "activities",
             "label": "Meditate", "enabled": not p.meditated,
             "reason": None, "limits": ["once per year"]},
            {"id": "gym", "category": "activities", "label": "Gym",
             "enabled": not p.worked_out and p.health >= 10,
             "reason": "already worked out this year" if p.worked_out
                       else ("you are too sick to work out"
                             if p.health < 10 else None),
             "limits": ["once per year"]},
            {"id": "library", "category": "activities", "label": "Library",
             "enabled": not p.visited_library, "reason": None,
             "limits": ["once per year"]},
            {"id": "read_book", "category": "activities",
             "label": "Read a Book", "enabled": True, "reason": None,
             "limits": []},
        ])
    in_school = p.is_in_school()
    if p.age >= 4:
        can_see_doctor = not in_school
        specs.append({"id": "doctor", "category": "health",
                      "label": "Doctor",
                      "known_costs": ([{"label": "Consultation fee",
                                        "amount": 100}] if p.age >= 18
                                      else []),
                      "enabled": can_see_doctor,
                      "reason": None if can_see_doctor
                                else "not while enrolled in school",
                      "limits": []})
    if p.age >= 18 and not in_school:
        can_pay_witch = p.money >= 50  # minimum fee
        specs.extend([
            {"id": "witch_doctor", "category": "health",
             "label": "Witch Doctor", "enabled": can_pay_witch,
             "reason": None if can_pay_witch else
                       f"the treatment costs at least $50 and you have "
                       f"${p.money:,}",
             "limits": []},
        ])
        surgery_ok = p.age - p.last_plastic_surgery >= 6
        surgery_afford = p.money >= 25000
        surgery_enabled = surgery_ok and surgery_afford
        specs.append({"id": "plastic_surgery", "category": "health",
                      "label": "Plastic Surgery",
                      "enabled": surgery_enabled,
                      "reason": None if surgery_enabled else
                                "wait a few years" if not surgery_ok else
                                f"plastic surgery costs $25,000 and you "
                                f"have ${p.money:,}",
                      "known_costs": [{"label": "Plastic surgery", "amount": 25000}],
                      "limits": []})
    if 21 <= p.age <= 70 and len(p.children) < 5 and not in_school:
        can_adopt = p.money >= 10000  # minimum adoption cost
        specs.append({"id": "adoption", "category": "family",
                      "label": "Adoption", "enabled": can_adopt,
                      "reason": None if can_adopt else
                                f"adoption costs at least $10,000 and you "
                                f"have ${p.money:,}",
                      "limits": []})
    specs.append({"id": "surrender", "category": "life",
                  "label": "Surrender this life", "enabled": True,
                  "reason": None, "limits": []})
    if p.age >= 18:
        specs.append({"id": "lottery", "category": "finances",
                      "label": "Play the lottery",
                      "known_costs": [{"label": "Ticket", "amount": 4}],
                      "enabled": True, "reason": None, "limits": []})
        if not in_school:
            available = _dating_target_available(context) \
                if context is not None else True
            specs.append({"id": "dating_app", "category": "relationships",
                          "label": "Dating app",
                          "enabled": available,
                          "reason": None if available else
                                    "no eligible adults around right now",
                          "limits": []})
    return specs


def _relationship_specs(p):
    specs = []
    for rel in p.relations:
        base = {"target_id": _pid(rel), "target_label": rel.name}
        if rel.age >= 5 and p.age >= 5:
            specs.append({**base, "id": "spend_time",
                          "category": "relationships", "label": "Spend time",
                          "enabled": not rel.spent_time,
                          "reason": None if not rel.spent_time
                                    else "already spent time with them this "
                                         "year",
                          "limits": ["once per year per person"]})
        if p.age >= 3 and rel.age >= 5:
            specs.append({**base, "id": "conversation",
                          "category": "relationships",
                          "label": "Have a conversation", "enabled": True,
                          "reason": None, "limits": []})
        if p.age >= 6 and rel.age >= 5:
            specs.append({**base, "id": "compliment",
                          "category": "relationships", "label": "Compliment",
                          "enabled": not rel.was_complimented,
                          "reason": None if not rel.was_complimented
                                    else "already complimented them this year",
                          "limits": ["once per year"]})
            specs.append({**base, "id": "insult", "category": "relationships",
                          "label": "Insult", "enabled": True,
                          "reason": None, "limits": []})
        from src.people.classes.parent import Parent
        if p.age >= 6 and isinstance(rel, Parent):
            specs.append({**base, "id": "ask_for_money",
                          "category": "relationships",
                          "label": "Ask for money", "enabled": True,
                          "reason": None, "limits": []})
    return specs


def _currently_pregnant(p):
    return bool(p.is_pregnant or (p.partner is not None
                                  and p.partner.is_pregnant))


def action_specs(context):
    """Allowed actions for the player right now; one shared eligibility
    predicate drives both this projection and the dispatcher. Pure read."""
    p = _player(context)
    if p.is_in_school():
        return (_school_action_specs(p) + _relationship_specs(p)
                + _activity_specs(p, context))
    specs = (_work_action_specs(p) + _relationship_specs(p)
             + _activity_specs(p, context))
    if p.age >= 18 and p.marital_status == 0 and p.date_options > 0:
        specs.append({"id": "find_partner", "category": "relationships",
                      "label": "Find a Partner", "enabled": True,
                      "reason": None, "limits": []})
    if p.partner is not None:
        base = {"target_id": _pid(p.partner), "target_label": p.partner.name}
        specs.append({**base, "id": "try_for_baby", "category": "family",
                      "label": "Have a baby",
                      "enabled": not _currently_pregnant(p),
                      "reason": None if not _currently_pregnant(p)
                                else "already pregnant",
                      "limits": []})
        if p.partner.status == 0:
            specs.append({**base, "id": "propose", "category": "relationships",
                          "label": "Propose", "enabled": True,
                          "reason": None, "limits": []})
        if p.partner.status < 2:
            # Legacy main.py offers "Break up" for dating AND engaged.
            specs.append({**base, "id": "break_up",
                          "category": "relationships", "label": "Break up",
                          "enabled": True, "reason": None, "limits": []})
        if p.partner.status == 1:
            specs.append({**base, "id": "plan_wedding",
                          "category": "relationships",
                          "label": "Plan the wedding", "enabled": True,
                          "reason": None, "limits": []})
            specs.append({**base, "id": "call_off_engagement",
                          "category": "relationships",
                          "label": "Call off the engagement",
                          "enabled": True, "reason": None, "limits": []})
        if p.partner.status == 2:
            specs.append({**base, "id": "divorce", "category": "relationships",
                          "label": "Divorce", "enabled": True,
                          "reason": None, "limits": []})
            specs.append({**base, "id": "renew_vows",
                          "category": "relationships", "label": "Renew Vows",
                          "enabled": not p.partner.asked_renew_vows
                          and p.age - p.partner.last_renew_vows >= 10,
                          "reason": None, "limits": []})
    if p.partner is not None and min(p.age, p.partner.age) < 18:
        for spec in specs:
            if spec["id"] in ("try_for_baby", "propose", "plan_wedding", "renew_vows"):
                spec["enabled"] = False
                spec["reason"] = "both participants must be 18 or older"
    return specs


ACTION_IDS = (
    "study_harder", "skip_school", "drop_out", "find_job",
    "work_harder", "adjust_hours", "request_raise", "retire",
    "quit_job",
)

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


def _guard_romance(player, partner=None):
    from src.game.features.family_graph.graph import (
        GraphError, assert_adult_romantic)
    try:
        assert_adult_romantic(player, partner if partner is not None else player)
    except GraphError as exc:
        raise RuleError(exc.code, exc.message) from exc


def _guard_pending_romance(context, player, pending):
    from src.game.features.family_graph.graph import (
        GraphError, assert_interaction_adults)
    people = getattr(context, "people_by_id", {})
    bound = [player] + [people[pid] for role, pid in (pending.get("bindings") or {}).items()
                        if role != "witness" and pid in people]
    try:
        assert_interaction_adults(pending.get("interaction"), bound or [player])
    except GraphError as exc:
        raise RuleError(exc.code, exc.message) from exc
    if any((o.get("_structural") or {}).get("gain_partner")
           for o in pending.get("options", [])):
        _guard_romance(player, pending.get("sampled_partner"))


def apply_action(context, action_id, payload, rng):
    """Validate and apply one menu action; returns a RuleResult.

    find_job is two-step: the offer (salary + interview outcome) is sampled
    NOW and stored in the pending decision; resolve_choice applies it.
    """
    p = _player(context)
    _check(isinstance(payload, dict), "invalid_input",
           "payload must be an object")
    _check(not (p.is_in_school() and action_id in _SCHOOL_BLOCKED_ACTIONS),
           "not_allowed", "not while enrolled in school")
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
    _check(isinstance(payload, dict), "invalid_input",
           "payload must be an object")
    if action_id in _OFFER_ACTIONS:
        _check(not p.is_in_school() or action_id in ("study_harder",
                                                      "skip_school",
                                                      "drop_out"),
               "not_allowed", "not while enrolled in school")
        return _OFFER_ACTIONS[action_id](p, rng)
    if action_id == "find_partner":
        return sample_find_partner(p, rng)
    target = None
    if "target_id" in payload:
        target = _find_person(context, payload["target_id"])
        _require_relation(p, target)
    if action_id in _TARGET_ACTIONS:
        _check(target is not None, "invalid_input",
               "target_id is required")
        return _TARGET_ACTIONS[action_id](p, target, rng)
    if action_id in _PARENT_ACTIONS:
        _check(target is not None, "invalid_input",
               "target_id is required")
        return _PARENT_ACTIONS[action_id](p, target, rng)
    if action_id in _CONTEXT_SELF_ACTIONS:
        return _CONTEXT_SELF_ACTIONS[action_id](context, p, payload, rng)
    if action_id in _SELF_ACTIONS:
        return _SELF_ACTIONS[action_id](p, rng)
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
    if "event_id" in pending or any("_effects" in o
                                    for o in pending.get("options", [])):
        # event-file pending (loader-generated, flat _effects shape);
        # kinds are event ids and are stable forever (e.g. saved games
        # holding kind 'vaccination_age2').
        return _resolve_event_pending(context, p, pending, option_id, rng)
    kind = pending.get("kind")
    resolver = _RESOLVER_KINDS.get(kind)
    if resolver is not None:
        # Resolvers that declare an `inputs` parameter (e.g. baby_naming)
        # receive the bounded client inputs; the rest keep the 4-arg form.
        if "inputs" in inspect.signature(resolver).parameters:
            return resolver(p, pending, option_id, inputs, rng)
        return resolver(p, pending, option_id, rng)
    if kind == "job_offer":
        options = [o["id"] for o in pending.get("options", [])]
        _check(option_id in options, "invalid_input", "unknown option")
        if option_id == "walk_away":
            return _result(events=[_event("work", "I walked away.", p)])
        sampled = pending.get("sampled") or {}
        _check("success" in sampled, "invalid_input", "missing stored outcome")
        before_happy = p.happiness
        p.change_happiness(sampled.get("happiness_delta", 0))
        changes = {"happiness": p.happiness - before_happy}
        if sampled["success"]:
            p.get_job(sampled["salary"])
            return _result(events=[_event(
                "work", "I got the job!", p, changes)])
        return _result(events=[_event(
            "work", "I didn't get an interview.", p, changes)])
    if kind == "retirement_offer":
        return resolve_retirement_choice(p, pending, option_id, rng)
    if kind == "university_application":
        return resolve_university_application(p, pending, option_id, rng)
    if kind == "university_funding":
        return resolve_university_funding(p, pending, option_id, rng)
    if kind == "argument":
        return resolve_argument(p, pending, option_id, rng)
    if kind == "pregnancy":
        return resolve_pregnancy(p, pending, option_id, rng)
    if kind == "wedding_plan":
        return resolve_wedding_plan(p, pending, option_id, rng)
    if kind == "date_offer":
        return resolve_date_offer(p, pending, option_id, rng)
    if kind == "doctor_visit":
        return resolve_doctor_visit(p, pending, option_id, rng)
    if kind == "witch_doctor":
        return resolve_witch_doctor(p, pending, option_id, rng)
    if kind == "book_choice":
        return resolve_book_choice(p, pending, option_id, rng)
    if kind == "plastic_surgery":
        return resolve_plastic_surgery(p, pending, option_id, rng)
    if kind == "adoption_offer":
        return resolve_adoption_offer(p, pending, option_id, rng)
    if kind == "adoption_rename":
        return resolve_adoption_rename(p, pending, option_id, rng)
    raise RuleError("invalid_input", f"unknown pending decision: {kind!r}")


_PHASES = {"P5": yearly_work_phase, "P6": yearly_education_phase,
           "income": yearly_work_phase, "work_income": yearly_work_phase,
           "education": yearly_education_phase,
           "debt_service": debt_service_phase}


def advance_phase(context, phase_id, rng, turn_id="turn",
                  year_start_happiness=None):
    p = _player(context)
    fn = _PHASES.get(phase_id)
    _check(fn is not None, "invalid_input", f"unknown phase: {phase_id!r}")
    if fn is yearly_work_phase:
        return fn(p, turn_id, rng,
                  year_start_happiness=year_start_happiness)
    if fn is family_events_phase:
        snapshot = getattr(context, "turn_snapshot", None)
        return fn(p, turn_id, rng,
                  snapshot_ids=set(snapshot) if snapshot else None)
    return fn(p, turn_id, rng)


# ================================================================== M2b RULES
# Source: src/menus/main.py relationship branches and src/menus/activities.py.
# All choice outcomes are pre-sampled at offer creation; money moves only via
# money_entries; deaths inside rules return a terminal transition
# (finished_turn -> year_close) and A performs the actual death/archive.

from src.lifesim_lib.const import COMPLIMENTS, INSULTS, SPEND_TIME_PLACES

# main.py "Plan the wedding" venue table (legacy prices, C-owned rule data).
WEDDING_VENUES = {
    "golf course": 15300,
    "vineyard": 15300,
    "family member's house": 255,
    "courthouse": 51,
    "restaurant": 5100,
    "drive-thru wedding chapel": 255,
    "country club": 15300,
}


def _find_person(context, person_id):
    """Resolve a canonical person_id through the duck-typed context."""
    _check(isinstance(person_id, str), "invalid_input",
           "target_id must be a string")
    lookup = getattr(context, "people_by_id", None)
    if lookup is not None:
        person = lookup.get(person_id)
    else:
        finder = getattr(context, "find_person", None)
        if finder is not None:
            person = finder(person_id)
        else:
            # Plain-Player context: resolve within the player's graph.
            person = _Ctx(context).people_by_id.get(person_id)
    _check(person is not None, "not_found", "unknown target")
    return person


def _require_relation(player, relation):
    _check(relation in player.relations, "not_allowed",
           "that person is not in your relationships")


def _changes(before, after, keys):
    return {k: after[k] - before[k] for k in keys
            if after[k] != before[k]}


def _stat_snapshot(person, keys):
    return {k: getattr(person, k) for k in keys}


# ---- Row 3: Spend time -------------------------------------------------

def spend_time(player, relation, rng):
    """main.py 'Spend time': refusal below 15 rel; enjoyment formulas.
    Once per year per person (legacy re-try grants nothing; the extracted
    rule refuses explicitly — same policy as compliment)."""
    _require_relation(player, relation)
    _check(not relation.spent_time, "not_allowed",
           "you already spent time with them this year")
    _check(player.age >= 5, "not_allowed", "you are too young")
    _check(relation.age >= 5, "not_allowed",
           "they are too young to spend time with")
    if relation.relationship < 15:
        before = _stat_snapshot(player, ("happiness",))
        player.change_happiness(-4)
        return _result(events=[_event(
            "relationship",
            f"Your {_rel_name(relation)} refused to see you.", player,
            _changes(before, _stat_snapshot(player, ("happiness",)),
                     ("happiness",)),
            )])
    enjoyment1 = max(rng.randint(0, 70), rng.randint(0, 70)) \
        + rng.randint(0, 30)
    if player.has_trait("CHEERFUL"):
        enjoyment1 = max(enjoyment1, rng.randint(0, 100))
    elif player.has_trait("GRUMPY"):
        enjoyment1 = min(enjoyment1, rng.randint(0, 100))
    enjoyment2 = round(rng.triangular(0, 100, relation.relationship))
    from src.people.classes.child import Child
    if isinstance(relation, Child):
        enjoyment1 += _round_stochastic(
            rng, (100 - enjoyment1) * max(0, 18 - relation.age)
            / rng.randint(36, 100))
        enjoyment2 += _round_stochastic(
            rng, (100 - enjoyment2) * max(0, 13 - relation.age)
            / rng.randint(26, 39))
    events = [_event(
        "relationship",
        f"I spent time with my {_rel_name(relation)} "
        f"{rng.choice(SPEND_TIME_PLACES)}.", player)]
    if not relation.spent_time:
        before_p = _stat_snapshot(player, ("happiness",))
        before_r = _stat_snapshot(relation, ("relationship",))
        player.change_happiness(_round_stochastic(rng, enjoyment1 / 12))
        relation.change_relationship(_round_stochastic(rng, enjoyment2 / 12))
        if player.has_trait("CHEERFUL"):
            player.change_happiness(3)
        relation.spent_time = True
        events[0]["changes"] = _changes(
            before_p, _stat_snapshot(player, ("happiness",)), ("happiness",))
        rel_delta = relation.relationship - before_r["relationship"]
        if rel_delta:
            events[0]["changes"]["relationship"] = rel_delta
            events[0]["person_ids"].append(_pid(relation))
    return _result(events=events)


def _rel_name(relation):
    try:
        return relation.name_accusative()
    except Exception:
        return relation.name


# ---- Row 4: Conversation (+ argument sub-choice) -----------------------

def sample_conversation(player, relation, rng):
    """main.py 'Have a conversation': refusal below 25; agreement roll;
    an argument (agreement < 15) yields a stored 3-way decision whose
    outcomes are sampled now."""
    _require_relation(player, relation)
    _check(player.age >= 3, "not_allowed", "you are too young")
    _check(relation.age >= 5, "not_allowed",
           "they are too young to have a conversation with")
    _check(relation.relationship >= 25, "refused",
           "Your %s isn't interested in having a conversation with you."
           % _rel_name(relation))
    agreement = rng.triangular(0, 100, 70)
    agreement += rng.randint(0, max(0, (relation.relationship - 50) // 3))
    from src.people.classes.sibling import Sibling
    if isinstance(relation, Sibling) and _one_in(rng, 2):
        agreement -= rng.randint(0, relation.petulance // 3)
    if player.age < 6:
        v = (6 - player.age) * 8
        agreement += rng.randint(v // 4, v)
    agreement = clamp(round(agreement), rng.randint(0, 10),
                      rng.randint(90, 100))
    result = _result(events=[_event(
        "relationship", "I had a conversation.", player,
        {"agreement": agreement})])
    if not relation.had_conversation:
        before = _stat_snapshot(player, ("happiness",))
        player.change_happiness(8 if player.has_trait("CHEERFUL") else 4)
        relation.change_relationship(_round_stochastic(rng, agreement / 12))
        relation.had_conversation = True
        result["events"][0]["changes"]["happiness"] = (
            player.happiness - before["happiness"])
    if agreement < 15:
        result["pending_decision"] = {
            "kind": "argument",
            "title": "Argument",
            "text": "You and your %s got into an argument. What will you do?"
                    % _rel_name(relation),
            "options": [{"id": "apologize", "label": "Apologize"},
                        {"id": "disagree", "label": "Agree to disagree"},
                        {"id": "insult", "label": "Insult them"}],
            "sampled": {
                "apologize": {"karma": rng.randint(1, 3),
                              "happiness": -rng.randint(2, 4),
                              "relationship": rng.randint(2, 4)},
                "disagree": {"karma": 0, "happiness": 0, "relationship": 0},
                "insult": {"karma": -rng.randint(2, 6),
                           "relationship": -rng.randint(4, 7)},
            },
            "sampled_target_id": _pid(relation),
        }
    return result


def resolve_argument(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    fx = pending["sampled"][option_id]
    # Tolerant access: sample_conversation's "insult" outcome stores no
    # "happiness" key, and legacy/saved pendings may miss any of these —
    # a missing key is a zero delta, never a KeyError -> 500 (crash_id
    # 62a309a1e51b).
    events = []
    if fx.get("karma"):
        player.change_karma(fx["karma"])
    if fx.get("happiness"):
        player.change_happiness(fx["happiness"])
    target_id = pending.get("sampled_target_id")
    if fx.get("relationship") and target_id:
        target = _find_person(_Ctx(player), target_id)
        target.change_relationship(fx["relationship"])
    label = {"apologize": "I apologized.",
             "disagree": "I agreed to disagree.",
             "insult": "I insulted them."}[option_id]
    return _result(events=[_event("relationship", label, player)])


class _Ctx:
    """Minimal context wrapper so _find_person works inside resolves."""
    def __init__(self, player):
        self.player = player
        self.people_by_id = {}
        def walk(p, seen=None):
            if seen is None:
                seen = set()
            if id(p) in seen or p is None:
                return
            seen.add(id(p))
            if hasattr(p, "person_id"):
                self.people_by_id[p.person_id] = p
            for r in getattr(p, "relations", []) or []:
                walk(r, seen)
            for r in (getattr(p, "parents", None) or {}).values():
                walk(r, seen)
            for r in getattr(p, "children", []) or []:
                walk(r, seen)
            for attr in ("mother", "father", "partner"):
                walk(getattr(p, attr, None), seen)
        walk(player)


# ---- Row 5: Ask parents for money --------------------------------------

def ask_for_money(player, parent, rng):
    """main.py 'Ask for money': generosity gate, formula, 3-strike refusal."""
    from src.people.classes.parent import Parent
    _check(isinstance(parent, Parent), "not_allowed",
           "you can only ask your parents for money")
    _require_relation(player, parent)
    _check(player.age >= 6, "not_allowed", "you are too young")
    if parent.asked_for_money >= 3:
        before = _stat_snapshot(parent, ("relationship",))
        parent.change_relationship(-rng.randint(5, 10))
        return _result(events=[_event(
            "relationship",
            f"My {_rel_name(parent)} told me to stop asking for money.",
            parent, _changes(before,
                             _stat_snapshot(parent, ("relationship",)),
                             ("relationship",)))])
    money = []
    events = []
    if parent.asked_for_money == 0 and _x_in_y(rng, parent.generosity, 35):
        amount = 5 ** (parent.money / 22) * (parent.generosity / 100) ** 2 \
            * math.sqrt(player.age)
        amount = max(rng.randint(1, 5),
                     _round_stochastic(rng, amount * rng.uniform(0.6, 1.4)))
        money.append(_money("relationships", "ask_money", "gift", player,
                            "gift_received", amount,
                            f"Your {_rel_name(parent)} gave you money"))
        before = _stat_snapshot(parent, ("relationship",))
        parent.change_relationship(-rng.randint(0, 8))
        events.append(_event(
            "relationship",
            f"My {_rel_name(parent)} gave me ${amount}.", parent,
            _changes(before, _stat_snapshot(parent, ("relationship",)),
                     ("relationship",))))
    else:
        before = _stat_snapshot(parent, ("relationship",))
        parent.change_relationship(-rng.randint(4, 8))
        events.append(_event(
            "relationship",
            f"My {_rel_name(parent)} refused to give me any money.",
            parent, _changes(before,
                             _stat_snapshot(parent, ("relationship",)),
                             ("relationship",))))
    parent.asked_for_money += 1
    return _result(events=events, money_entries=money)


# ---- Row 6: Compliment --------------------------------------------------

def compliment(player, relation, rng):
    """main.py 'Compliment': appreciation rolls, return-compliment event.
    Once per year per person: legacy allows re-trying but grants nothing;
    the extracted rule refuses explicitly so the displayed enabled/reason
    matches dispatch (documented policy note in the M2 report)."""
    _require_relation(player, relation)
    _check(player.age >= 6, "not_allowed", "you are too young")
    _check(relation.age >= 5, "not_allowed",
           "they are too young to compliment")
    _check(not relation.was_complimented, "not_allowed",
           "you already complimented them this year")
    gen = lambda: rng.randint(0, 50) + rng.randint(0, 50)
    appreciation = gen()
    rel = relation.relationship
    if rel >= rng.randint(51, 100):
        appreciation = max(appreciation, gen())
        if rel >= rng.randint(75, 120):
            appreciation = max(appreciation, gen())
    elif rel <= rng.randint(0, 49):
        appreciation = min(appreciation, gen())
        if rel <= rng.randint(0, 25):
            appreciation = min(appreciation, gen())
    events = [_event("relationship",
                     f"I told my {_rel_name(relation)} that "
                     f"{relation.hes_shes()} "
                     f"{rng.choice(COMPLIMENTS)}.", player)]
    if not relation.was_complimented:
        before_p = _stat_snapshot(player, ("happiness", "karma"))
        before_r = _stat_snapshot(relation, ("relationship",))
        player.change_karma(rng.randint(0, 2))
        relation.change_relationship(_round_stochastic(rng, appreciation / 6))
        roll = rng.randint(1, 300)
        cmp_ = _round_stochastic(rng, appreciation * rel / 50)
        if roll <= cmp_:
            player.change_happiness(
                rng.randint(6, 10) - 3 * player.has_trait("GRUMPY"))
            if player.has_trait("CHEERFUL"):
                player.change_happiness(4)
            if roll <= min(15, cmp_ // 2):
                relation.change_relationship(rng.randint(25, 40))
        relation.was_complimented = True
        events[0]["changes"] = _changes(
            before_p, _stat_snapshot(player, ("happiness", "karma")),
            ("happiness", "karma"))
        rel_delta = relation.relationship - before_r["relationship"]
        if rel_delta:
            events[0]["changes"]["relationship"] = rel_delta
            events[0]["person_ids"].append(_pid(relation))
    return _result(events=events)


# ---- Row 7: Insult ------------------------------------------------------

def insult(player, relation, rng):
    """main.py 'Insult': rel/karma loss; counter-insult or attack branch."""
    _require_relation(player, relation)
    _check(player.age >= 6, "not_allowed", "you are too young")
    _check(relation.age >= 5, "not_allowed",
           "they are too young to insult")
    from src.people.classes.sibling import Sibling
    from src.people.classes.partner import Partner
    before_r = _stat_snapshot(relation, ("relationship",))
    relation.change_relationship(-rng.randint(4, 8))
    player.change_karma(-rng.randint(2, 4))
    events = [_event("relationship",
                     f"I called my {_rel_name(relation)} "
                     f"{rng.choice(INSULTS)}.", relation,
                     {"relationship": relation.relationship
                      - before_r["relationship"]})]
    if isinstance(relation, Sibling):
        attack_chance = 30 * (relation.petulance / 100) ** 1.5
    elif isinstance(relation, Partner):
        attack_chance = 45 * (relation.craziness / 100) ** 2
    else:
        attack_chance = 0
    chance = (50 * (relation.petulance / 100) ** 1.5
              if isinstance(relation, Sibling)
              else (100 - relation.relationship) / 4)
    if _x_in_y(rng, attack_chance, 100):
        damage = rng.randint(4, 10)
        before_p = _stat_snapshot(player, ("happiness", "health"))
        player.was_attacked(damage, can_die=False)
        relation.change_relationship(-rng.randint(4, 8))
        events.append(_event(
            "relationship", f"Your {_rel_name(relation)} attacked you!",
            player, _changes(before_p,
                             _stat_snapshot(player, ("happiness", "health")),
                             ("happiness", "health"))))
    elif _x_in_y(rng, chance, 100):
        before_p = _stat_snapshot(player, ("happiness",))
        player.change_happiness(-rng.randint(2, 6))
        events.append(_event(
            "relationship",
            f"Your {_rel_name(relation)} called you {rng.choice(INSULTS)}!",
            player, _changes(before_p,
                             _stat_snapshot(player, ("happiness",)),
                             ("happiness",))))
    return _result(events=events)


# ---- Row 8: Have a baby -------------------------------------------------

def sample_try_for_baby(player, rng):
    """main.py 'Have a baby': gates; fertility draw; a pregnancy yields a
    stored keep/abort decision. The partner-overrule draw u is sampled once
    and both branches compare against it (deterministic per choice)."""
    _guard_romance(player, player.partner)
    partner = player.partner
    _check(partner is not None, "not_allowed", "you have no partner")
    _check(partner in player.relations, "not_allowed",
           "that person is not in your relationships")
    already = partner.is_pregnant if player.gender == Gender.Male \
        else player.is_pregnant
    _check(not already, "not_allowed", "already pregnant")
    _check(partner.relationship >= rng.randint(45, 75),
           "refused", "Your partner doesn't want to have a baby with you.")
    _check(partner.years_together >= rng.randint(1, 2),
           "refused", "Your partner doesn't want to have a baby with you.")
    fertility = partner.fertility if player.gender == Gender.Male \
        else player.fertility
    if not _x_in_y(rng, fertility, 100):
        return _result(events=[_event(
            "family",
            "Your partner failed to get pregnant."
            if player.gender == Gender.Male else "You failed to get pregnant.",
            player)])
    u = rng.uniform(0, 100)
    base = 10 * (partner.craziness / 100) ** 2
    # legacy: partner-female halves the chance when you KEEP; partner-male
    # halves it when you ABORT
    chances = {"keep": base / 2 if partner.gender == Gender.Female else base,
               "abort": base if partner.gender == Gender.Female else base / 2}
    return _result(pending_decision={
        "kind": "pregnancy",
        "title": "Baby",
        "text": ("Your partner is pregnant with your baby!"
                 if player.gender == Gender.Male else
                 "You are pregnant!"),
        "options": [{"id": "keep", "label": "Keep the baby"},
                    {"id": "abort", "label": "Get an abortion"}],
        "sampled": {"overrule_u": u, "overrule_chance": chances},
    })


def resolve_pregnancy(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    sampled = pending["sampled"]
    partner = player.partner
    chances = sampled.get("overrule_chance") or {}
    _check(option_id in chances, "invalid_input",
           "no stored outcome for this option")
    overruled = sampled["overrule_u"] < chances[option_id]
    events = []
    is_pregnant = True
    if option_id == "keep":
        if overruled:
            is_pregnant = False
            before = _stat_snapshot(player, ("happiness",))
            player.change_happiness(-rng.randint(10, 15))
            events.append(_event(
                "family",
                "Your partner got an abortion anyway!"
                if partner.gender == Gender.Female else
                "Your partner forced you to get an abortion!", player,
                _changes(before, _stat_snapshot(player, ("happiness",)),
                         ("happiness",))))
    else:
        if overruled:
            before = _stat_snapshot(player, ("happiness",))
            player.change_happiness(-rng.randint(8, 15))
            events.append(_event(
                "family",
                "Your partner refused to get an abortion!"
                if partner.gender == Gender.Female else
                "Your partner forced you to keep the baby!", player,
                _changes(before, _stat_snapshot(player, ("happiness",)),
                         ("happiness",))))
        else:
            is_pregnant = False
            events.append(_event(
                "family",
                "I made my partner get an abortion!"
                if player.gender == Gender.Male else "I got an abortion.",
                player))
    if is_pregnant:
        if player.gender == Gender.Male:
            partner.is_pregnant = True
        else:
            player.is_pregnant = True
            player._recent_child_father = partner
        events.append(_event("family", "The pregnancy continues.", player))
    return _result(events=events)


# ---- Rows 9-14: partner lifecycle ---------------------------------------

def break_up(player):
    """main.py 'Break up' (confirm is a UI concern)."""
    _check(player.partner is not None, "not_allowed", "you have no partner")
    _check(player.partner.status < 2, "not_allowed",
           "you are married; use divorce")
    name = player.partner.name_accusative()
    player.lose_partner()
    return _result(events=[_event(
        "relationship", f"I broke up with {name}.", player)])


def propose(player, rng):
    """main.py 'Propose': years/craziness, one-shot, relationship gate."""
    _guard_romance(player, player.partner)
    partner = player.partner
    _check(partner is not None, "not_allowed", "you have no partner")
    _check(partner.status == 0, "not_allowed",
           "you are already engaged or married")
    if (partner.years_together >= rng.randint(3, 8 - partner.craziness // 20)
            and not partner.was_proposed_to
            and partner.relationship
            >= rng.randint(50, 60) + rng.randint(0, 40)):
        partner.status = 1
        before = _stat_snapshot(partner, ("relationship",))
        partner.change_relationship(rng.randint(20, 50))
        return _result(events=[_event(
            "relationship",
            f"Your {partner.name_accusative()} accepted your proposal!",
            partner, _changes(before,
                              _stat_snapshot(partner, ("relationship",)),
                              ("relationship",)))])
    if not partner.was_proposed_to:
        before_p = _stat_snapshot(player, ("happiness",))
        before_r = _stat_snapshot(partner, ("relationship",))
        player.change_happiness(-rng.randint(3, 8))
        partner.change_relationship(-rng.randint(4, 9))
        partner.was_proposed_to = True
        return _result(events=[_event(
            "relationship",
            f"Your {partner.name_accusative()} rejected your proposal.",
            partner,
            {**_changes(before_p, _stat_snapshot(player, ("happiness",)),
                        ("happiness",)),
             "relationship": partner.relationship
             - before_r["relationship"]})])
    return _result(events=[_event(
        "relationship",
        f"Your {partner.name_accusative()} rejected your proposal.",
        partner)])


def call_off_engagement(player):
    """main.py 'Call off the engagement'."""
    partner = player.partner
    _check(partner is not None and partner.status == 1, "not_allowed",
           "you are not engaged")
    name = partner.name_accusative()
    partner.status = 0
    before = _stat_snapshot(partner, ("relationship",))
    partner.change_relationship(-15)
    return _result(events=[_event(
        "relationship",
        f"I called off my engagement with {name}.", partner,
        _changes(before, _stat_snapshot(partner, ("relationship",)),
                 ("relationship",)))])


def sample_wedding_plan(player, rng):
    """main.py 'Plan the wedding': 4 sampled venues; per-venue sub-choice
    do/edit/cancel is modeled as options on the stored decision."""
    _guard_romance(player, player.partner)
    partner = player.partner
    _check(partner is not None and partner.status == 1, "not_allowed",
           "you are not engaged")
    venues = rng.sample(list(WEDDING_VENUES), 4)
    return _result(pending_decision={
        "kind": "wedding_plan",
        "title": "Wedding",
        "text": "Choose a location:",
        "options": [{"id": f"venue:{v}",
                     "label": f"{v} (${WEDDING_VENUES[v]:,})",
                     "enabled": player.money >= WEDDING_VENUES[v],
                     "reason": None
                     if player.money >= WEDDING_VENUES[v] else
                     f"this venue costs ${WEDDING_VENUES[v]:,} and you have "
                     f"${player.money:,}",
                     "known_costs": [{"label": v,
                                      "amount": WEDDING_VENUES[v]}]}
                    for v in venues] + [{"id": "cancel", "label": "Cancel"}],
        "sampled": {"venues": venues},
    })


def resolve_wedding_plan(player, pending, option_id, rng):
    _guard_romance(player, player.partner)
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    partner = player.partner
    if option_id == "cancel":
        return _result(events=[_event("relationship", "I cancelled the "
                                      "wedding plans.", player)])
    venue = option_id.split(":", 1)[1]
    price = WEDDING_VENUES[venue]
    _check(player.money >= price, "not_allowed",
           f"this wedding plan costs ${price:,} and you have ${player.money:,}")
    money = [_money("relationships", "wedding", "cost", player, "wedding",
                    -price, f"Married at a {venue}")]
    before_p = _stat_snapshot(player, ("happiness",))
    before_r = _stat_snapshot(partner, ("relationship",))
    player.change_happiness(rng.randint(10, 16))
    partner.change_relationship(rng.randint(30, 50))
    partner.status = 2
    partner.last_renew_vows = player.age
    changes = _changes(before_p, _stat_snapshot(player, ("happiness",)),
                       ("happiness",))
    changes["relationship"] = partner.relationship - before_r["relationship"]
    return _result(events=[_event(
        "relationship",
        f"I married {partner.name} at a {venue}.", partner, changes)],
        money_entries=money)


def divorce_partner(player, rng):
    """main.py 'Divorce': 40-60% settlement via money_entries; the terminal
    Player.divorce keeps the legacy mutation for the terminal path."""
    partner = player.partner
    _check(partner is not None and partner.status == 2, "not_allowed",
           "you are not married")
    amount = round(player.money * rng.uniform(0.4, 0.6))
    money = []
    events = []
    if amount > 0:
        before = _stat_snapshot(player, ("happiness",))
        player.change_happiness(-rng.randint(10, 15))
        money.append(_money("relationships", "divorce", "settlement", player,
                            "transfer", -amount,
                            f"Divorce settlement paid to {partner.name}"))
        events.append(_event(
            "relationship",
            f"The judge ordered you to pay {partner.name} ${amount} to "
            f"settle your divorce.", player,
            _changes(before, _stat_snapshot(player, ("happiness",)),
                     ("happiness",))))
    player.lose_partner()
    events.append(_event("relationship",
                         f"You divorced {partner.name_accusative()}.",
                         player))
    return _result(events=events, money_entries=money)


def renew_vows(player, rng):
    """main.py 'Renew Vows': 10-year spacing, once, relationship gate."""
    _guard_romance(player, player.partner)
    partner = player.partner
    _check(partner is not None and partner.status == 2, "not_allowed",
           "you are not married")
    _check(not partner.asked_renew_vows, "not_allowed",
           "Your partner doesn't want to renew your wedding vows.")
    _check(player.age - partner.last_renew_vows >= 10, "not_allowed",
           "Your partner doesn't want to renew your wedding vows.")
    _check(partner.relationship >= rng.randint(40, 70), "refused",
           "Your partner doesn't want to renew your wedding vows.")
    before_r = _stat_snapshot(partner, ("relationship",))
    before_p = _stat_snapshot(player, ("happiness",))
    partner.change_relationship(rng.randint(15, 20))
    player.change_happiness(rng.randint(10, 16))
    partner.last_renew_vows = player.age
    return _result(events=[_event(
        "relationship",
        f"You and {partner.name_accusative()} renewed your wedding vows.",
        partner,
        {"relationship": partner.relationship - before_r["relationship"],
         "happiness": player.happiness - before_p["happiness"]})])


# ============================================================ M2b ACTIVITIES
# Source: src/menus/activities.py. Costs become money_entries; deaths inside
# a rule return finished_turn with continuation year_close (A performs death).

from src.lifesim_lib.const import MUSIC_ARTISTS, BOOKS
from src.lifesim_lib.lifesim_lib import random_name, random_last_name
from src.people.classes.child import Child


# ---- Row 25: Play with toys --------------------------------------------

def play_toys(player, rng):
    """activities.py 'Play with your toys' (ages 3-12), once per year."""
    _check(3 <= player.age < 13, "not_allowed",
           "toys are for ages 3 to 12")
    _check(not player.played, "not_allowed",
           "you already played with your toys this year")
    if player.is_depressed():
        happy_gain = rng.randint(0, 6)
        if player.has_trait("CHEERFUL"):
            happy_gain += 2
    else:
        happy_gain = rng.randint(5, 10)
        if player.has_trait("CHEERFUL"):
            happy_gain += 5
    events = []
    if not player.played:
        before = _stat_snapshot(player, ("happiness",))
        player.change_happiness(happy_gain)
        player.played = True
        events.append(_event("activity", "I played with my toys.", player,
                             _changes(before, _stat_snapshot(
                                 player, ("happiness",)), ("happiness",))))
    else:
        events.append(_event("activity", "I played with my toys.",
                             player))
    return _result(events=events)


# ---- Row 26: Arts and crafts --------------------------------------------

def arts_and_crafts(player, rng):
    """activities.py 'Arts and Crafts' (age 5+), once per year."""
    _check(player.age >= 5, "not_allowed", "you are too young")
    _check(not player.did_arts_and_crafts, "not_allowed",
           "you already did arts and crafts this year")
    events = []
    if player.age >= rng.randint(5, 10) and _one_in(rng, 10):
        before = _stat_snapshot(player, ("happiness",))
        player.change_happiness(-rng.randint(2, 4))
        events.append(_event(
            "activity", "You couldn't think of anything creative to do.",
            player, _changes(before, _stat_snapshot(player, ("happiness",)),
                             ("happiness",))))
    elif not player.did_arts_and_crafts:
        before = _stat_snapshot(player, ("happiness", "smarts"))
        player.change_happiness(rng.randint(2, 5))
        player.change_smarts(rng.randint(1, 2))
        if player.has_trait("CHEERFUL"):
            player.change_happiness(3)
        if player.has_trait("NERD"):
            player.change_smarts(rng.randint(0, 2))
        events.append(_event("activity", "I did arts and crafts.", player,
                             _changes(before, _stat_snapshot(
                                 player, ("happiness", "smarts")),
                                 ("happiness", "smarts"))))
    else:
        events.append(_event("activity", "I did arts and crafts.", player))
    player.did_arts_and_crafts = True
    return _result(events=events)


# ---- Row 27: Listen to music --------------------------------------------

def listen_to_music(player, rng):
    """activities.py 'Listen to music' (age 12+), once per year."""
    _check(player.age >= 12, "not_allowed", "you are too young")
    _check(not player.listened_to_music, "not_allowed",
           "you already listened to music this year")
    if not player.listened_to_music:
        before = _stat_snapshot(player, ("happiness", "health", "smarts"))
        stress0 = player.stress
        player.change_happiness(
            rng.randint(4, 8) + 3 * player.has_trait("CHEERFUL"))
        player.change_health(rng.randint(0, 2))
        player.change_stress(-rng.randint(1, 7))
        player.change_smarts(rng.randint(0, 1 + player.has_trait("NERD")))
        player.listened_to_music = True
        changes = _changes(before, _stat_snapshot(
            player, ("happiness", "health", "smarts")),
            ("happiness", "health", "smarts"))
        if player.stress != stress0:
            changes["stress"] = player.stress - stress0
        return _result(events=[_event("activity",
                                      "You listened to some "
                                      f"{rng.choice(['pop music', 'rock music', 'hip-hop', 'latin music'])}.",
                                      player, changes)])
    return _result(events=[_event("activity", "I listened to some music.",
                                  player)])


# ---- Row 28: Find a Partner ----------------------------------------------

def sample_find_partner(player, rng):
    """activities.py 'Find a Partner': the match and the compatibility
    outcome are sampled now; date_options is consumed at offer time."""
    _guard_romance(player)
    _check(player.age >= 18, "not_allowed", "you are too young")
    _check(player.marital_status == 0, "not_allowed",
           "you are already in a relationship")
    _check(player.date_options > 0, "not_allowed",
           "you are unable to find anyone to ask on a date")
    partner = player.generate_partner()
    compatible = partner.compatibility_check(player)
    player.date_options -= 1
    return _result(pending_decision={
        "kind": "date_offer",
        "title": "A date?",
        "text": f"You met a {'male' if partner.gender == Gender.Male else 'female'} named {partner.name}.",
        "options": [{"id": "ask_out", "label": "Ask them out"},
                    {"id": "decline", "label": "Not my type"}],
        "sampled": {"compatible": compatible},
        "sampled_partner_id": _pid(partner),
        "sampled_partner": partner,
    })


def resolve_date_offer(player, pending, option_id, rng):
    _guard_romance(player, pending.get("sampled_partner"))
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    partner = pending.get("sampled_partner")
    _check(partner is not None, "invalid_input", "missing sampled partner")
    if option_id == "decline":
        return _result(events=[_event("relationship", "I walked away.",
                                      player)])
    if pending["sampled"]["compatible"]:
        before = _stat_snapshot(player, ("happiness",))
        player.change_happiness(rng.randint(15, 20))
        player.partner = partner
        player.relations.append(partner)
        return _result(events=[_event(
            "relationship", f"You are now dating {partner.name}.", player,
            _changes(before, _stat_snapshot(player, ("happiness",)),
                     ("happiness",)))])
    before = _stat_snapshot(player, ("happiness",))
    player.change_happiness(-rng.randint(2, 4))
    return _result(events=[_event(
        "relationship", f"{partner.name} rejected you.", player,
        _changes(before, _stat_snapshot(player, ("happiness",)),
                 ("happiness",)))])


# ---- Row 29: Doctor ------------------------------------------------------

DOCTOR_CURE_TABLE = ("Depression", "High Blood Pressure", "Common Cold",
                     "Flu", "Cancer")


def sample_doctor_visit(player, rng):
    """activities.py 'Doctor': $100 fee at 18+; one stored choice lists the
    illnesses with each treatment outcome pre-sampled (legacy cure rolls)."""
    _check(player.age >= 4, "not_allowed", "you are too young")
    fee = 100 if player.age >= 18 else 0
    illnesses = [str(i) for i in player.illnesses]
    outcomes, gains = {}, {}
    for illness in illnesses:
        if illness == "Depression":
            outcomes[illness] = (_one_in(rng, 4)
                                 and player.happiness >= rng.randint(20, 35))
            gains[illness] = (rng.randint(4, 8), None) if outcomes[illness] \
                else (rng.randint(3, 5), rng.randint(3, 5))
        elif illness == "High Blood Pressure":
            outcomes[illness] = (player.stress < rng.randint(50, 80)
                                 and _one_in(rng, 3))
            gains[illness] = ((rng.randint(4, 8), rng.randint(3, 6))
                              if outcomes[illness]
                              else (rng.randint(3, 5), rng.randint(3, 5)))
        elif illness == "Common Cold":
            outcomes[illness] = True
            gains[illness] = (rng.randint(4, 6), rng.randint(4, 7))
        elif illness == "Flu":
            outcomes[illness] = True
            gains[illness] = (rng.randint(5, 6), rng.randint(4, 8))
        elif illness == "Cancer":
            outcomes[illness] = (_one_in(rng, 20)
                                 and rng.randint(1, 100) <= player.health)
            gains[illness] = ((rng.randint(30, 50), rng.randint(20, 40))
                              if outcomes[illness]
                              else (rng.randint(5, 9), rng.randint(3, 7)))
        else:
            outcomes[illness] = False
            gains[illness] = (rng.randint(3, 5), rng.randint(3, 5))
    options = [{"id": "back", "label": "Back"}] + [
        {"id": f"treat:{name}",
         "label": f"Treat {name}",
         "enabled": not fee or player.money >= fee,
         "reason": None if (not fee or player.money >= fee) else
                   f"the consultation costs ${fee} and you have "
                   f"${player.money:,}",
         "known_costs": ([{"label": "Consultation fee", "amount": fee}]
                         if fee else [])}
        for name in illnesses]
    return _result(pending_decision={
        "kind": "doctor_visit",
        "title": "Doctor",
        "text": ("The doctor lists your illnesses."
                 if illnesses else
                 "The doctor found no illnesses."),
        "options": options,
        "sampled": {"fee": fee, "outcomes": outcomes, "gains": gains},
    })


def resolve_doctor_visit(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "back":
        return _result(events=[_event("health", "I left the doctor.", player)])
    illness = option_id.split(":", 1)[1]
    fee = pending["sampled"]["fee"]
    if fee:
        _check(player.money >= fee, "not_allowed",
               f"the consultation costs ${fee} and you have ${player.money}")
    money = [_money("health", "doctor", "fee", player, "medical", -fee,
                    "Doctor consultation and treatment")] if fee else []
    cured = pending["sampled"]["outcomes"].get(illness, False)
    events = []
    before = _stat_snapshot(player, ("health", "happiness"))
    if cured:
        health_gain, happy_gain = pending["sampled"]["gains"].get(
            illness, (3, 3))
        player.change_health(health_gain)
        if happy_gain:
            player.change_happiness(happy_gain)
        if illness == "Depression":
            player.change_happiness((100 - player.happiness) // 2)
        player.remove_illness(illness)
        events.append(_event(
            "health",
            f"I'm no longer suffering from the {str(illness).lower()}.",
            player,
            _changes(before, _stat_snapshot(player, ("health", "happiness")),
                     ("health", "happiness"))))
    else:
        health_gain, happy_gain = pending["sampled"]["gains"].get(
            illness, (3, 3))
        player.change_health(health_gain)
        player.change_happiness(happy_gain)
        events.append(_event(
            "health",
            f"I'm still suffering from the {str(illness).lower()}.", player,
            _changes(before, _stat_snapshot(player, ("health", "happiness")),
                     ("health", "happiness"))))
    return _result(events=events, money_entries=money)


# ---- Row 30: Witch doctor -----------------------------------------------

def sample_witch_doctor(player, rng):
    """activities.py 'Witch Doctor': fee and outcome sampled now. A lethal
    outcome resolves as a terminal transition (A performs the death)."""
    _check(player.age >= 18, "not_allowed", "you are too young")
    fee = rng.randint(50, 100)
    # Gate on the $50 minimum (what action_specs advertises); the drawn fee
    # still gates the pending's "take" option and resolve re-checks.
    _check(player.money >= 50, "not_allowed",
           f"the treatment costs at least $50 and you have ${player.money:,}")
    health_mod = player.witch_doctor_health + rng.gauss(0, 2)
    if player.health >= 60:
        health_mod -= (player.health - 60) / 10
    if not player.illnesses:
        health_mod -= abs(rng.gauss(0, 5))
    health_mod = _round_stochastic(rng, health_mod)
    wd_delta = rng.gauss(-1.5, 1)
    cure = None
    if health_mod > 0 and player.illnesses \
            and _x_in_y(rng, health_mod, 200):
        cure = rng.choice(list(player.illnesses))
    new_health = clamp(player.health + health_mod, 0, 100)
    return _result(pending_decision={
        "kind": "witch_doctor",
        "title": "Witch Doctor",
        "text": f"The witch doctor offers a \"special\" treatment. Cost: {fee}",
        "options": [{"id": "take", "label": "Take it",
                     "enabled": player.money >= fee,
                     "reason": None if player.money >= fee else
                               f"the treatment costs ${fee} and you have "
                               f"${player.money:,}"},
                    {"id": "refuse", "label": "Refuse"}],
        "sampled": {"fee": fee, "health_mod": health_mod,
                    "witch_doctor_delta": wd_delta, "cure": cure,
                    "fatal": new_health <= 0},
    })


def resolve_witch_doctor(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "refuse":
        return _result(events=[_event("health", "I refused the treatment.",
                                      player)])
    sampled = pending["sampled"]
    _check(player.money >= sampled["fee"], "not_allowed",
           f"the treatment costs ${sampled['fee']} and you have ${player.money}")
    money = [_money("health", "witch_doctor", "fee", player, "medical",
                    -sampled["fee"], "Witch doctor treatment")]
    events = []
    player.witch_doctor_health += sampled["witch_doctor_delta"]
    before = _stat_snapshot(player, ("health", "happiness"))
    player.change_health(sampled["health_mod"])
    player.change_happiness(sampled["health_mod"])
    events.append(_event("health", "I took the witch doctor's treatments.",
                         player, _changes(
                             before, _stat_snapshot(
                                 player, ("health", "happiness")),
                             ("health", "happiness"))))
    if sampled["cure"]:
        player.remove_illness(sampled["cure"])
        events.append(_event(
            "health",
            f"I'm no longer suffering from the "
            f"{str(sampled['cure']).lower()}.",
            player))
    if sampled["fatal"]:
        events.append(_event(
            "death", "You died after taking a witch doctor's treatments.",
            player))
        return _result(events=events, money_entries=money,
                       finished_turn=True,
                       continuation={"phase_id": "year_close",
                                     "item_cursor": 0})
    return _result(events=events, money_entries=money)


# ---- Rows 31-33: Mind & Body ---------------------------------------------

def meditate(player, rng):
    """activities.py 'Meditate' (age 12+), once per year."""
    _check(player.age >= 12, "not_allowed", "you are too young")
    _check(not player.meditated, "not_allowed",
           "you already meditated this year")
    before = _stat_snapshot(player, ("happiness", "health", "karma"))
    stress0 = player.stress
    player.change_health(rng.randint(2, 4))
    player.change_happiness(rng.randint(3, 5))
    player.change_karma(rng.randint(1, 3))
    player.change_stress(-rng.randint(3, 8))
    if player.has_trait("MEDITATOR"):
        player.change_stress(-4)
        player.change_happiness(2)
        player.change_health(rng.randint(1, 2))
    deeper = player.times_meditated == 0 or _one_in(rng, 20)
    if deeper:
        player.change_happiness(2)
        player.change_stress(-3)
        learn = None
        if player.times_meditated > 0 and _x_in_y(rng, 2, 5) \
                and not _x_in_y(rng, player.stress, 100):
            learn = "MEDITATOR"
        if learn:
            player.learn_trait(learn)
    if player.has_trait("CHEERFUL"):
        player.change_happiness(4)
    player.meditated = True
    player.times_meditated += 1
    changes = _changes(before, _stat_snapshot(
        player, ("happiness", "health", "karma")),
        ("happiness", "health", "karma"))
    if player.stress != stress0:
        changes["stress"] = player.stress - stress0
    return _result(events=[_event("activity", "I practiced meditation.",
                                  player, changes)])


def gym(player, rng):
    """activities.py 'Gym' (age 12+, health 10+), once per year."""
    _check(player.age >= 12, "not_allowed", "you are too young")
    _check(player.health >= 10, "not_allowed",
           "your health is too weak to visit the gym")
    _check(not player.worked_out, "not_allowed",
           "you already worked out this year")
    enjoyment = rng.randint(35, 80)
    if rng.randint(1, 3) == 1:
        enjoyment += rng.randint(10, 20)
    before = _stat_snapshot(player, ("happiness", "health", "looks"))
    player.change_happiness(_round_stochastic(rng, enjoyment / 12))
    if player.has_trait("CHEERFUL"):
        player.change_happiness(3)
    player.change_health(rng.randint(3, 6))
    if player.looks < rng.randint(1, 100):
        player.change_looks(rng.randint(2, 4))
    player.worked_out = True
    return _result(events=[_event("activity", "I worked out at the gym.",
                                  player, _changes(
                                      before, _stat_snapshot(
                                          player, ("happiness", "health",
                                                   "looks")),
                                      ("happiness", "health", "looks")))])


def library(player, rng):
    """activities.py 'Library' (age 12+), once per year; NERD/BOOK_LOVER
    trait learning stays a live roll inside the action (legacy order)."""
    _check(player.age >= 12, "not_allowed", "you are too young")
    _check(not player.visited_library, "not_allowed",
           "you already went to the library this year")
    enjoyment = rng.randint(15, 65)
    if player.has_trait("CHEERFUL"):
        enjoyment = max(enjoyment, rng.randint(15, 65))
    elif player.has_trait("GRUMPY"):
        enjoyment = min(enjoyment, rng.randint(15, 65))
    if player.has_trait("GENIUS"):
        enjoyment += rng.randint(5, 20)
    elif player.has_trait("NERD"):
        enjoyment += rng.randint(0, 15)
    if player.has_trait("BOOK_LOVER"):
        enjoyment = max(enjoyment, rng.randint(35, 100))
    before = _stat_snapshot(player, ("happiness", "smarts"))
    happy_gain = enjoyment / 15
    if player.has_trait("BOOK_LOVER"):
        happy_gain *= 1.5
        happy_gain += 2
    player.change_happiness(_round_stochastic(rng, happy_gain))
    if player.has_trait("CHEERFUL"):
        player.change_happiness(3)
    player.change_smarts(rng.randint(2, 5))
    if player.has_trait("GENIUS"):
        player.change_smarts(5)
    elif player.has_trait("NERD"):
        player.change_smarts(3)
    player.times_visited_library += 1
    if _one_in(rng, 48) and rng.random() < 0.95 ** player.times_visited_library:
        if not player.has_trait("NERD") and _x_in_y(rng, player.smarts, 2000):
            player.learn_trait("NERD")
        else:
            player.learn_trait("BOOK_LOVER")
    player.visited_library = True
    return _result(events=[_event("activity", "I went to the library.",
                                  player, _changes(
                                      before, _stat_snapshot(
                                          player, ("happiness", "smarts")),
                                      ("happiness", "smarts")))])


# ---- Row 34: Read a Book (collapsed per Claude) --------------------------

def sample_read_book(player, rng):
    """activities.py 'Read a Book' collapsed to one action (Claude-approved):
    5 sampled age-eligible books; per-book outcome pre-sampled."""
    available = [b for b in BOOKS if player.age >= b[3]]
    _check(player.age >= 12, "not_allowed", "you are too young")
    _check(available, "not_allowed", "no books available for your age")
    selection = rng.sample(available, min(len(available), 5))
    per_book = {}
    for book in selection:
        title = book[0]
        happy_amount = book[4]
        if player.has_trait("BOOK_LOVER"):
            happy_amount = (math.ceil(min(happy_amount * 1.5,
                                          (100 + happy_amount) / 2))
                            if happy_amount > 0
                            else -(abs(happy_amount) // 2))
        enjoyment = rng.randint(25, 50) + rng.randint(0, 25)
        if happy_amount > 0:
            r = (happy_amount / 100) ** 0.7
            enjoyment = _round_stochastic(rng, enjoyment
                                          + (100 - enjoyment) * r)
        learn = []
        if book[2] > rng.randint(48, 150) \
                and _x_in_y(rng, math.sqrt(book[5]), 100):
            learn.append("NERD")
        if _x_in_y(rng, math.sqrt(max(0, happy_amount)), 100):
            learn.append("BOOK_LOVER")
        h_amount = math.ceil(rng.uniform(0.8, 1) * happy_amount
                             * (1 - player.happiness / 100))
        s_amount = math.ceil(rng.uniform(0.8, 1) * book[5]
                             * (1 - player.smarts / 100))
        if player.has_trait("CHEERFUL"):
            h_amount += max(4, h_amount // 3)
        stress_relief = 0
        if happy_amount > 0:
            stress_relief = -_round_stochastic(
                rng, rng.uniform(0, math.sqrt(happy_amount))
                * (min(player.stress / 70, 1)) ** 2)
        per_book[title] = {"enjoyment": enjoyment, "learn": learn,
                           "h_amount": h_amount, "s_amount": s_amount,
                           "stress_relief": stress_relief}
    return _result(pending_decision={
        "kind": "book_choice",
        "title": "Read a Book",
        "text": "Which book would you like to read?",
        "options": [{"id": f"book:{b[0]}", "label": b[0]} for b in selection]
                   + [{"id": "back", "label": "Back"}],
        "sampled": {"per_book": per_book},
    })


def resolve_book_choice(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "back":
        return _result(events=[_event("activity", "I put the book back.",
                                      player)])
    title = option_id.split(":", 1)[1]
    per_book = (pending.get("sampled") or {}).get("per_book") or {}
    fx = per_book.get(title)
    _check(fx is not None, "invalid_input", "unknown book outcome")
    before = _stat_snapshot(player, ("happiness", "smarts"))
    stress0 = player.stress
    player.change_happiness(fx.get("h_amount", 0))
    player.change_smarts(fx.get("s_amount", 0))
    if fx.get("stress_relief"):
        player.change_stress(fx["stress_relief"])
    for trait in fx.get("learn") or []:
        player.learn_trait(trait)
    changes = _changes(before, _stat_snapshot(player, ("happiness", "smarts")),
                       ("happiness", "smarts"))
    if player.stress != stress0:
        changes["stress"] = player.stress - stress0
    return _result(events=[_event(
        "activity", f'You read all of "{title}".', player, changes)])


# ---- Row 35: Plastic Surgery ---------------------------------------------

def sample_plastic_surgery(player, rng):
    """activities.py 'Plastic Surgery': $25000, 6-year spacing; per-surgeon
    outcome (botched vs results) sampled now."""
    _check(player.age >= 18, "not_allowed", "you are too young")
    _check(player.age - player.last_plastic_surgery >= 6, "not_allowed",
           "please wait a while before getting another plastic surgery")
    _check(player.money >= 25000, "not_allowed",
           f"plastic surgery costs $25,000 and you have ${player.money:,}")
    per_surgeon = []
    for name, reputation in player.plastic_surgeons:
        botched = _one_in(rng, 5) and not _x_in_y(rng, reputation, 100)
        if botched:
            d = 100 - reputation
            damage = rng.randint(6, 12) + rng.randint(d // 4, d * 3 // 4)
            fatal = (reputation < 40)  # health loss value; fatality at resolve
            per_surgeon.append({"botched": True,
                                "value": _round_stochastic(rng, damage / 2),
                                "fatal": fatal})
        else:
            rolls = [rng.randint(20, 100) for _ in range(rng.randint(2, 3))]
            results = min(rolls, key=lambda v: abs(v - reputation))
            rep_gain, rep_cap = 0, 0
            if results > reputation:
                if _x_in_y(rng, results - reputation, 80):
                    rep_cap = rng.randint(50, 100)
                    if reputation < rep_cap:
                        rep_gain = rng.randint(2, 6)
            per_surgeon.append({"botched": False,
                                "value": _round_stochastic(rng, results / 2),
                                "reputation": reputation,
                                "rep_gain": rep_gain, "rep_cap": rep_cap})
    return _result(pending_decision={
        "kind": "plastic_surgery",
        "title": "Plastic Surgery",
        "text": "Which plastic surgeon would you like to visit? (Cost: $25000)",
        "options": [{"id": f"surgeon:{i}",
                     "label": f"Dr. {player.plastic_surgeons[i][0]}",
                     "enabled": player.money >= 25000,
                     "reason": None if player.money >= 25000 else
                               f"plastic surgery costs $25,000 and you have "
                               f"${player.money:,}",
                     "known_costs": [{"label": "Plastic surgery",
                                      "amount": 25000}]}
                    for i in range(len(player.plastic_surgeons))]
                   + [{"id": "cancel", "label": "Cancel"}],
        "sampled": {"per_surgeon": per_surgeon},
    })


def resolve_plastic_surgery(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "cancel":
        return _result(events=[_event("health", "I decided against "
                                      "plastic surgery.", player)])
    raw_idx = option_id.split(":", 1)[1]
    _check(raw_idx.isdigit(), "invalid_input", "unknown surgeon")
    idx = int(raw_idx)
    per_surgeon = (pending.get("sampled") or {}).get("per_surgeon") or []
    _check(0 <= idx < len(per_surgeon), "invalid_input", "unknown surgeon")
    sampled = per_surgeon[idx]
    _check(player.money >= 25000, "not_allowed",
           f"plastic surgery costs $25,000 and you have ${player.money:,}")
    money = [_money("health", "plastic_surgery", "cost", player, "medical",
                    -25000, "Plastic surgery")]
    events = []
    if sampled["botched"]:
        before = _stat_snapshot(player, ("happiness", "health", "looks"))
        player.change_happiness(-sampled["value"])
        player.change_health(-sampled["value"])
        player.change_looks(-sampled["value"])
        events.append(_event(
            "health", "Your plastic surgery was botched!", player,
            _changes(before, _stat_snapshot(
                player, ("happiness", "health", "looks")),
                ("happiness", "health", "looks"))))
        if player.health <= 0:
            events.append(_event(
                "death", "You died after receiving a botched plastic "
                "surgery.", player))
            return _result(events=events, money_entries=money,
                           finished_turn=True,
                           continuation={"phase_id": "year_close",
                                         "item_cursor": 0})
    else:
        before = _stat_snapshot(player, ("happiness", "looks"))
        player.change_happiness(sampled["value"])
        player.change_looks(sampled["value"])
        if sampled["rep_gain"]:
            new_rep = player.plastic_surgeons[idx][1] + sampled["rep_gain"]
            player.plastic_surgeons[idx][1] = min(new_rep,
                                                  sampled["rep_cap"])
        events.append(_event(
            "health", "Your plastic surgery was successful.", player,
            _changes(before, _stat_snapshot(player, ("happiness", "looks")),
                     ("happiness", "looks"))))
    player.last_plastic_surgery = player.age
    return _result(events=events, money_entries=money)


# ---- Row 36: Adoption -----------------------------------------------------

def sample_adoption(player, rng):
    """activities.py 'Adoption': offer sampled now (child stats, age, cost);
    acceptance yields a rename decision (A's canonical Child hook)."""
    _check(21 <= player.age <= 70, "not_allowed",
           "adoption is available from age 21 to 70")
    _check(len(player.children) < 5, "not_allowed",
           "you already have enough children")
    gender = (Gender.Male if rng.uniform(0, 100) < 51.2 else Gender.Female)
    smarts = rng.randint(0, 50) + rng.randint(0, 50)
    looks = rng.randint(0, 50) + rng.randint(0, 50)
    age = rng.randint(1, 17)
    cost = rng.randint(10000, 30000)
    # Gate on the $10,000 minimum (what action_specs advertises); the drawn
    # cost still gates the pending's "adopt" option and resolve re-checks.
    _check(player.money >= 10000, "not_allowed",
           f"adoption costs at least $10,000 and you have ${player.money:,}")
    # legacy: the child starts with a random last name; the rename decision
    # offers to change it to match the player's.
    child = Child(random_name(gender), random_last_name(), gender, smarts,
                  looks, adopted=True)
    child.age = age
    return _result(pending_decision={
        "kind": "adoption_offer",
        "title": "Adoption",
        "text": f"You have an opportunity to adopt {child.name}, "
                f"a {age}-year-old {'boy' if gender == Gender.Male else 'girl'}.",
        "options": [{"id": "adopt", "label": "Adopt",
                     "enabled": player.money >= cost,
                     "reason": None if player.money >= cost else
                               f"adoption costs ${cost:,} and you have "
                               f"${player.money:,}",
                     "known_costs": [{"label": "Adoption cost",
                                      "amount": cost}]},
                    {"id": "decline", "label": "Decline"}],
        "sampled": {"cost": cost},
        "sampled_child_id": _pid(child),
        "sampled_child": child,
    })


def resolve_adoption_offer(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "decline":
        return _result(events=[_event("family", "I declined the "
                                      "adoption.", player)])
    child = pending.get("sampled_child")
    _check(child is not None, "invalid_input", "missing sampled child")
    sampled = pending.get("sampled") or {}
    cost = sampled.get("cost")
    _check(isinstance(cost, (int, float)) and not isinstance(cost, bool)
           and cost >= 0, "invalid_input", "missing adoption cost")
    cost = int(cost)
    _check(player.money >= cost, "not_allowed",
           f"adoption costs ${cost:,} and you have ${player.money:,}")
    money = [_money("family_events", "adoption", "cost", player, "other",
                    -cost, "Adoption fee")]
    child.happiness = rng.randint(40, 100)
    before = _stat_snapshot(player, ("happiness",))
    player.change_happiness(rng.randint(15, 30))
    child.set_parent(player)
    player.relations.append(child)
    player.children.append(child)
    return _result(events=[_event(
        "family", f"I adopted {child.name}.", player,
        _changes(before, _stat_snapshot(player, ("happiness",)),
                 ("happiness",)))],
        money_entries=money,
        pending_decision={
            "kind": "adoption_rename",
            "title": "Adoption",
            "text": f"Would you like to change {child.name}'s last name to "
                    f"match yours?",
            "options": [{"id": "rename", "label": "Yes"},
                        {"id": "keep_name", "label": "No"}],
            "sampled_child_id": _pid(child),
            "sampled_child": child,
        })


def resolve_adoption_rename(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    child = pending.get("sampled_child")
    _check(child is not None, "invalid_input", "missing sampled child")
    if option_id == "rename":
        child.lastname = player.lastname
        child.update_name()
    return _result(events=[_event(
        "family", f"{child.name} joined your family.", child)])


# Dispatch tables (defined after all rule functions).
_TARGET_ACTIONS = {
    "spend_time": spend_time,
    "conversation": sample_conversation,
    "compliment": compliment,
    "insult": insult,
}

_SELF_ACTIONS = {
    "try_for_baby": sample_try_for_baby,
    "break_up": lambda p, rng: break_up(p),
    "propose": propose,
    "call_off_engagement": lambda p, rng: call_off_engagement(p),
    "plan_wedding": sample_wedding_plan,
    "divorce": divorce_partner,
    "renew_vows": renew_vows,
    "play_toys": play_toys,
    "arts_and_crafts": arts_and_crafts,
    "listen_to_music": listen_to_music,
    "meditate": meditate,
    "gym": gym,
    "library": library,
    "read_book": sample_read_book,
}

_PARENT_ACTIONS = {
    "ask_for_money": ask_for_money,
}

_OFFER_ACTIONS = {
    "doctor": sample_doctor_visit,
    "witch_doctor": sample_witch_doctor,
    "plastic_surgery": sample_plastic_surgery,
    "adoption": sample_adoption,
}

# Partner lifecycle actions are unreachable from the legacy school menu
# (main.py shows the school menu instead of the partner menus), and the
# service never advertises them while enrolled — so dispatch refuses them
# too. Relationship upkeep (spend time etc.) stays available: the service
# projection offers parent spend_time at any age >= 5 (M1-frozen).
_SCHOOL_BLOCKED_ACTIONS = {
    "try_for_baby", "break_up", "propose", "call_off_engagement",
    "plan_wedding", "divorce", "renew_vows", "find_partner",
    "dating_app",
}


# ===================================================== MORTALITY (phase 4)
# A detects deaths (death_check / sudden); C's sample_relation_death turns
# one death into: reason, inheritance money entry, happiness loss, the
# thanatophobia reaction, and a stored funeral decision. A owns the graph
# mutation (relations removal, partner/child clearing, archive).

from src.lifesim_lib.const import RELATION_SUDDEN_DEATH_REASONS
from src.people.classes.parent import Parent
from src.people.classes.sibling import Sibling
from src.people.classes.partner import Partner


def sample_relation_death(player, relation, rng, sudden=False):
    """Legacy Player.process_relation_death effects (player.py:374-425),
    death half only. A performs removal/archive after calling this."""
    if sudden:
        reasons = list(RELATION_SUDDEN_DEATH_REASONS)
        if relation.age >= 16:
            reasons.append("died after driving off a cliff while texting "
                           "and driving")
        reason = rng.choice(reasons)
        if _one_in(rng, 1000):
            reason = ("starved to death after playing this game for too "
                      "many days without stopping to eat")
    else:
        reason = "died of old age"
    events = [_event(
        "death",
        f"Your {relation.name_accusative() if hasattr(relation, 'name_accusative') else relation.name} "
        f"died at the age of {relation.age}. {relation.he_she().capitalize()} {reason}.",
        relation)]
    happy_remove = rng.randint(40, 55)
    if isinstance(relation, Sibling):
        happy_remove = rng.randint(25, 40)
    elif isinstance(relation, Partner):
        happy_remove = rng.randint(65, 100)
    elif isinstance(relation, Child):
        happy_remove = rng.randint(75, 100)
    before = _stat_snapshot(player, ("happiness", "stress"))
    player.change_happiness(-happy_remove)
    if player.has_trait("THANATOPHOBIA"):
        player.change_happiness(-rng.randint(30, 45))
        player.change_stress(rng.randint(5, 10))
        events.append(_event(
            "death", "You feel intense panic as the death of your loved "
            "one reminds you of how afraid you are of death.", player))
    changes = _changes(before, _stat_snapshot(player, ("happiness", "stress")),
                       ("happiness", "stress"))
    events[0]["changes"] = changes
    money = []
    inheritance = 0
    if isinstance(relation, Parent) and _x_in_y(rng, 70, 100) \
            and _x_in_y(rng, relation.generosity, 100):
        avg = 100000 * (relation.money / 100) ** 2
        lo = max(avg * relation.generosity / 200, 1)
        # _randexpo divides by (avg - lo): a broke parent's avg can be 0
        # (<= lo), which crashed with ZeroDivisionError or drew a negative
        # "inheritance". Clamp the mean above lo so the draw stays positive.
        avg = max(avg, lo + 1)
        inheritance = max(1, _round_stochastic(rng, _randexpo(rng, lo, avg)))
        money.append(_money("mortality", "inheritance", "transfer", player,
                            "inheritance", inheritance,
                            f"Inherited from {relation.name}"))
        player.change_happiness(
            _round_stochastic(rng, 1.5 * math.log10(inheritance)))
    pending = {
        "kind": "funeral",
        "title": "Funeral",
        "text": f"Would you like to attend {relation.name}'s funeral?",
        "options": [{"id": "attend", "label": "Attend the funeral"},
                    {"id": "skip", "label": "Don't attend"}],
        "sampled": {
            "attend": {"happiness": rng.randint(3, 5),
                       "karma": rng.randint(3, 5)},
            "skip": {"happiness": 0, "karma": -rng.randint(2, 5)},
        },
    }
    return _result(events=events, money_entries=money,
                   pending_decision=pending)


def resolve_funeral_choice(player, pending, option_id, rng):
    """Legacy funeral branch: attend (+happiness/+karma) or skip (-karma)."""
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    fx = pending["sampled"][option_id]
    before = _stat_snapshot(player, ("happiness", "karma"))
    # .get: a stored outcome missing a key means a zero delta, never a
    # KeyError -> 500 (same crash class as resolve_argument).
    player.change_happiness(fx.get("happiness", 0))
    player.change_karma(fx.get("karma", 0))
    label = ("I attended the funeral." if option_id == "attend"
             else "I didn't attend the funeral.")
    return _result(events=[_event("death", label, player,
                                  _changes(before, _stat_snapshot(
                                      player, ("happiness", "karma")),
                                      ("happiness", "karma")))])



def _relation_phrase(target, player):
    """Journal phrase: 'My mother Lin and I' / 'My partner Sam and I'."""
    from src.people.classes.parent import Parent
    from src.people.classes.partner import Partner
    from src.people.classes.child import Child
    from src.people.classes.sibling import Sibling
    from src.lifesim_lib.lifesim_lib import Gender
    first = getattr(target, 'firstname', None) or getattr(target, 'name', 'them')
    if isinstance(target, Parent):
        role = 'mother' if target.gender == Gender.Female else 'father'
        return f'My {role} {first} and I'
    if isinstance(target, Partner):
        return f'My partner {first} and I'
    if isinstance(target, Child):
        return f'My child {first} and I'
    if isinstance(target, Sibling):
        return f'My sibling {first} and I'
    return f'{first} and I'

def _apply_structural(context, player, pending, structural, events, money,
                      deaths, rng):
    """The v2.1 structural effect set, shared by both pending dialects.
    Mutates events/money/deaths lists in place; returns the (possibly
    extended) structural dict."""
    if structural.get("lose_partner") and player.partner is not None:
        player.lose_partner()
    if structural.get("divorce"):
        partner = player.partner
        if partner is not None:
            name = partner.name_accusative()
            player.lose_partner()
            events.append(_event("relationship",
                                 f"You divorced {name}.", player))
    if structural.get("partner_death"):
        partner = player.partner
        _check(partner is not None, "not_found", "no partner in the graph")
        # #3: the stored outcome (announcement) is authoritative — never
        # resample a different cause after the event announced "presumed
        # dead". A's single graph transition consumes the typed death
        # instruction (canonical person_id) exactly once: archive/remove/
        # mark dead, preserving journal references.
        cause = structural.get("partner_death")
        deaths.append({"person_id": _pid(partner),
                       "cause": str(cause),
                       "announcement": (
                           f"My partner {partner.firstname} is presumed "
                           f"dead after disappearing")})
        result = sample_relation_death(
            player, partner, rng, sudden=False)
        events.extend(result["events"])
        money.extend(result["money_entries"])
        structural = dict(structural)
        structural["_funeral_pending"] = result["pending_decision"]
    if structural.get("gain_partner"):
        stored = pending.get("sampled_partner")
        _check(stored is not None, "not_found",
               "no stored partner for this offer")
        _guard_romance(player, stored)
        player.partner = stored
        if stored not in player.relations:
            player.relations.append(stored)
    return structural


def _resolve_event_pending(context, player, pending, option_id, rng):
    """Resolve a loader-generated event pending (flat _effects + optional
    _structural). Applies stats/relationships directly, money via ledger
    entries (A posts), structural effects via the matching rule helpers,
    terminal outcomes as finished_turn -> year_close. Never rerolls:
    everything was pre-sampled at offer creation."""
    _guard_pending_romance(context, player, pending)
    if pending.get("schema_dialect") == "v3":
        return _resolve_event_v3(context, player, pending, option_id, rng)
    option = next((o for o in pending.get("options", [])
                   if o["id"] == option_id), None)
    _check(option is not None, "invalid_input", "unknown option")
    _check(option.get("enabled", True), "not_allowed",
           option.get("reason") or "this option is not available")
    _check(isinstance(pending.get("event_id"), str) and pending["event_id"],
           "invalid_input", "missing event id")
    events = []
    money = []
    deaths = []
    person_ids = list(pending.get("person_ids", []))
    for pid in (pending.get("_pre_person_ids_extra") or []):
        if pid not in person_ids:
            person_ids.append(pid)
    if option.get("_person_ids_extra"):
        person_ids += [i for i in option["_person_ids_extra"]
                       if i not in person_ids]
    targets = []
    for pid in person_ids:
        try:
            targets.append(_find_person(context, pid))
        except RuleError:
            pass
    targets = [t for t in targets if hasattr(t, "change_relationship")]
    # #4: event pre-effects are sampled at offer creation and normally
    # applied by A's serve-time applier (which sets pre_applied). Apply
    # here only when they were never applied (legacy/saved pendings).
    if pending.get("pre_effects") and not pending.get("pre_applied"):
        pre_fx = dict(pending["pre_effects"])
        pre_structural = {}
        for key in ("lose_partner", "divorce", "gain_partner",
                    "partner_death", "terminal"):
            if key in pre_fx:
                pre_structural[key] = pre_fx.pop(key)
        for key, amount in pre_fx.items():
            if key == "money" and amount:
                money.append(_money(
                    "decision_offer", f"{pending['event_id']}:pre", "main",
                    player, "other", amount,
                    f"{pending.get('title', 'Event')} (before the choice)"))
            elif key == "relationship":
                for target in targets:
                    target.change_relationship(amount)
            elif key in STAT_NAMES:
                getattr(player, f"change_{key}")(amount)
        structural_pre = pre_structural
    else:
        structural_pre = {}
    fx = dict(option.get("_effects", {}))
    before = _stat_snapshot(player, tuple(STAT_NAMES))
    money_before = player.money

    structural = dict(structural_pre)
    structural.update(option.get("_structural") or {})
    for key, amount in fx.items():
        if key == "money":
            if amount:
                money.append(_money(
                    "decision_offer", f"{pending['event_id']}:{option_id}",
                    "main", player, "other", amount,
                    f"{pending.get('title', 'Event')}: {option['label']}"))
        elif key == "relationship":
            for target in targets:
                before_r = target.relationship
                target.change_relationship(amount)
                delta_r = target.relationship - before_r
                if abs(delta_r) >= 5:
                    closer = delta_r > 0
                    events.append(_event(
                        "decision",
                        f"{_relation_phrase(target, player)} "
                        f"{'grew closer' if closer else 'drifted apart'} "
                        f"this year.", target,
                        {"relationship": delta_r}))
        elif key in STAT_NAMES:
            getattr(player, f"change_{key}")(amount)

    structural = _apply_structural(context, player, pending, structural,
                                   events, money, deaths, rng)

    changes = _changes(before, _stat_snapshot(player, tuple(STAT_NAMES)),
                       STAT_NAMES)
    outcome_text = option.get("_outcome", "").format(
        name="I")
    events.append(_event("decision", outcome_text or
                         pending.get("title", "Event"), player, changes))
    finished = bool(structural.get("terminal"))
    if finished:
        events.append(_event(
            "death", outcome_text or "You died.", player))
    return _result(
        events=events, money_entries=money,
        pending_decision=structural.get("_funeral_pending"),
        continuation={"phase_id": "year_close", "item_cursor": 0}
        if finished else None,
        finished_turn=finished, deaths=deaths)


# ------------------------------------------------------- v3 (M2.5) resolve
# Domain-effect appliers for v3 typed effects (frozen event-schema-v3.md
# §4). A (graph transitions) and B (policy commands) register the
# authoritative appliers when their domains land; C's knowledge effects
# register with the knowledge layer (M2.5 step 3). Signature:
#   applier(context, player, bindings, value, rng)
#     -> (events, money_entries)
_DOMAIN_EFFECTS = {}


def register_domain_effect(name, applier):
    _DOMAIN_EFFECTS[name] = applier


def _render_v3_outcome(template, player, bindings, context):
    """Render a v3 outcome template: {name} -> player firstname,
    {person:role} -> the BOUND person's name (stored bindings, never a
    re-pick). Unresolvable refs keep their placeholder text instead of
    raising (A's parenting gap: .format(name=...) crashed on {person:})."""
    import re as _re
    out = template.replace("{name}", player.firstname)
    if "{person:" not in out:
        return out

    def sub(match):
        bound = bindings.get(match.group(1))
        if bound is None:
            return match.group(0)
        try:
            person = _find_person(context, bound)
        except RuleError:
            return match.group(0)
        name = getattr(person, "name", None)
        return name if name else match.group(0)

    prev = None
    while prev != out:
        prev = out
        out = _re.sub(r"\{person:(\w+)\}", sub, out)
    return out


def _resolve_event_v3(context, player, pending, option_id, rng):
    """Resolve a v3 pending (frozen event-schema-v3.md §6/§7).

    Stale bindings never crash and never silently re-pick: an unresolvable
    bound person resolves to a safe explicit cancellation ('The moment has
    passed.'), preserving attempt usage. Domain effects are validated
    against the registry BEFORE anything mutates (no partial application);
    an unregistered domain API is a typed error, not a 500."""
    _guard_pending_romance(context, player, pending)
    option = next((o for o in pending.get("options", [])
                   if o["id"] == option_id), None)
    _check(option is not None, "invalid_input", "unknown option")
    _check(option.get("enabled", True), "not_allowed",
           option.get("reason") or "this option is not available")
    _check(isinstance(pending.get("event_id"), str) and pending["event_id"],
           "invalid_input", "missing event id")

    # 1. Revalidate every bound person id (chronology/membership guard
    # for romantic offers lives in A's graph; here we only need the
    # person to still exist in the walkable graph).
    bindings = pending.get("bindings") or {}
    stale = False
    for pid in bindings.values():
        if pid is None:
            continue
        try:
            _find_person(context, pid)
        except RuleError:
            stale = True
    if stale:
        return _result(events=[_event(
            "decision", "The moment has passed.", player)])

    # 2. Domain effects must all be applicable before anything mutates.
    domain = dict(option.get("_domain") or {})
    for key in domain:
        _check(key in _DOMAIN_EFFECTS, "not_available",
               f"domain effect {key!r} is not available in this engine yet")

    events = []
    money = []
    deaths = []
    person_ids = [pid for pid in bindings.values() if pid is not None]
    for pid in (pending.get("_pre_person_ids_extra") or []):
        if pid not in person_ids:
            person_ids.append(pid)
    if option.get("_person_ids_extra"):
        person_ids += [i for i in option["_person_ids_extra"]
                       if i not in person_ids]
    targets = []
    for pid in person_ids:
        try:
            targets.append(_find_person(context, pid))
        except RuleError:
            pass
    targets = [t for t in targets if hasattr(t, "change_relationship")]

    if pending.get("pre_effects") and not pending.get("pre_applied"):
        for key, amount in dict(pending["pre_effects"]).items():
            if key == "money" and amount:
                money.append(_money(
                    "decision_offer", f"{pending['event_id']}:pre", "main",
                    player, "other", amount,
                    f"{pending.get('title', 'Event')} (before the choice)"))
            elif key == "relationship":
                for target in targets:
                    target.change_relationship(amount)
            elif key in STAT_NAMES:
                getattr(player, f"change_{key}")(amount)

    before = _stat_snapshot(player, tuple(STAT_NAMES))
    fx = dict(option.get("_effects", {}))
    for key, amount in fx.items():
        if key == "money":
            if amount:
                money.append(_money(
                    "decision_offer", f"{pending['event_id']}:{option_id}",
                    "main", player, "other", amount,
                    f"{pending.get('title', 'Event')}: {option['label']}"))
        elif key == "relationship":
            for target in targets:
                before_r = target.relationship
                target.change_relationship(amount)
                delta_r = target.relationship - before_r
                if abs(delta_r) >= 5:
                    closer = delta_r > 0
                    events.append(_event(
                        "decision",
                        f"{_relation_phrase(target, player)} "
                        f"{'grew closer' if closer else 'drifted apart'} "
                        f"this year.", target, {"relationship": delta_r}))
        elif key in STAT_NAMES:
            getattr(player, f"change_{key}")(amount)

    structural = dict(option.get("_structural") or {})
    structural = _apply_structural(context, player, pending, structural,
                                   events, money, deaths, rng)

    for key, value in domain.items():
        applier = _DOMAIN_EFFECTS[key]
        kwargs = {}
        if "occurrence_id" in inspect.signature(applier).parameters:
            kwargs["occurrence_id"] = f"{pending['id']}:{key}"
        evs, mon = applier(context, player, bindings, value, rng, **kwargs)
        events.extend(evs or [])
        money.extend(mon or [])

    changes = _changes(before, _stat_snapshot(player, tuple(STAT_NAMES)),
                       STAT_NAMES)
    outcome_text = _render_v3_outcome(option.get("_outcome", ""),
                                      player, bindings, context)
    events.append(_event("decision", outcome_text or
                         pending.get("title", "Event"), player, changes))
    finished = bool(structural.get("terminal"))
    if finished:
        events.append(_event("death", outcome_text or "You died.", player))
    # Tag every emitted event with the audience/fact so A's journal
    # filter (knowledge_journal_visible) can apply the frozen §2 rule at
    # serialization. A player_known occurrence the player experienced
    # firsthand needs no fact record — it is public to them.
    audience = (pending.get("audience") or {}).get("scope", "public")
    fact_id = pending.get("fact_id")
    if audience == "player_known" and not fact_id:
        audience = "public"
    for entry in events:
        entry["audience"] = audience
        if fact_id:
            entry["fact_id"] = fact_id
    return _result(
        events=events, money_entries=money,
        pending_decision=structural.get("_funeral_pending"),
        continuation={"phase_id": "year_close", "item_cursor": 0}
        if finished else None,
        finished_turn=finished, deaths=deaths)


STAT_NAMES = ("happiness", "health", "smarts", "looks", "karma", "stress")


# ============================================================ M2.5 DATING
# Dating-app offers (frozen event-schema-v3.md + policy-evaluator.md):
# the app picks an adult stranger (A's registered graph predicate), B's
# evaluator scores hard-validity + acceptance, and THIS path draws
# accept/refuse ONCE with the seeded rng, storing the draw in the
# pending. Resolve applies stored data only; a stored refusal replaces
# the accept outcome text (no conditional logic in the resolver). Graph
# edge creation on acceptance is A's "offer" domain applier.

DATING_APP_EVENT = {
    "id": "m25_dating_app_date",
    "title": "A match wants to meet",
    "text": "{person:target} from the app asks {name} out for coffee.",
    "interaction": {"kind": "date", "romantic": True},
    "roles": {
        "actor": {"kind": "person", "select": "player"},
        "target": {"kind": "person", "select": "predicate",
                   "predicate": "adult_non_related_stranger"},
    },
    "choices": [
        {"id": "accept", "label": "Go on the date", "cost": {},
         "effects": {"stats": {"happiness": {"randint": [5, 12]}},
                     "chemistry": {"between": ["actor", "target"],
                                   "delta": {"randint": [3, 8]}},
                     "offer": {"interaction": "propose_courtship",
                               "to": "target", "as": "npc"}},
         "outcome_text": "We had a lovely evening."},
        {"id": "decline", "label": "Not tonight", "cost": {},
         "effects": {}, "outcome_text": "I swiped away."},
    ],
    "audience": {"scope": "player_known", "participants": ["actor",
                                                           "target"]},
}


def sample_dating_app(context, player, payload, rng):
    """Create a dating-app date offer: bind target, score with B's
    evaluator, draw accept/refuse once, store everything in the pending."""
    from src.game.events import EventEngine, PREDICATES
    env = getattr(context, "_env", None)
    _check(env is not None, "invalid_input",
           "the dating app needs the game service context")
    target_id = PREDICATES["adult_non_related_stranger"](
        player.person_id, context)
    _check(target_id is not None, "not_found",
           "no eligible adults around right now")
    from src.game.features.family_graph.service_view import (
        ServiceGraphView, ServicePolicyView)
    from src.game.features.relationship_policy.evaluator import (
        evaluate_offer)
    graph = ServiceGraphView(env)
    policy = ServicePolicyView(env)
    scored = evaluate_offer("date", player.person_id, target_id, graph,
                            policy, env.get("policy_revision", 0))
    _check(scored.get("hard", {}).get("ok"), "not_allowed",
           "this match is not possible right now")
    p_refuse = float((scored.get("refusal") or {}).get("p_refuse", 0.0))
    p_refuse = min(max(p_refuse, 0.0), 1.0)
    accepted = rng.random() >= p_refuse
    pending = EventEngine.instantiate3(
        DATING_APP_EVENT, 0, rng, context, prebound={"target": target_id})
    sampled = dict(pending.get("sampled") or {})
    sampled["accepted"] = accepted
    sampled["score"] = scored.get("score")
    pending["sampled"] = sampled
    reasons = scored.get("reasons") or []
    if reasons:
        from src.game.features.knowledge.drama import filter_reasons
        pending["reasons"] = filter_reasons(env, reasons)
    if not accepted:
        # Stored refusal: the accept option's effects are replaced by the
        # refusal outcome, sampled once here and never rerolled.
        for option in pending["options"]:
            if option["id"].endswith("_accept"):
                option["_effects"] = {}
                option["_domain"] = None
                option["_outcome"] = (
                    "I got stood up — they never replied.")
    return _result(pending_decision=pending)


# Self actions that need the duck-typed rule CONTEXT (not just the
# player): offer creation binds and scores against the envelope.
_CONTEXT_SELF_ACTIONS = {
    "dating_app": sample_dating_app,
}


# register in resolve_choice dispatch
_RESOLVER_KINDS = {    "argument": resolve_argument,
    "pregnancy": resolve_pregnancy,
    "wedding_plan": resolve_wedding_plan,
    "date_offer": resolve_date_offer,
    "doctor_visit": resolve_doctor_visit,
    "witch_doctor": resolve_witch_doctor,
    "book_choice": resolve_book_choice,
    "plastic_surgery": resolve_plastic_surgery,
    "adoption_offer": resolve_adoption_offer,
    "adoption_rename": resolve_adoption_rename,
    "funeral": resolve_funeral_choice,
}


# ========================================================== ILLNESS (phase)
# Legacy Player.random_events illness block (rows 58-60, 50). Fatal outcomes
# return finished_turn -> year_close (A performs the death).

from src.lifesim_lib.lifesim_lib import TranslateMarker


def illness_phase(player, turn_id, rng):
    events = []
    for illness in list(player.illnesses):
        if illness == "Depression":
            if player.happiness >= rng.randint(20, 35):
                before = _stat_snapshot(player, ("happiness", "health"))
                player.change_happiness((100 - player.happiness) // 2)
                player.change_health(rng.randint(4, 8))
                player.remove_illness("Depression")
                events.append(_event(
                    "health", "I am no longer suffering from depression",
                    player, _changes(before, _stat_snapshot(
                        player, ("happiness", "health")),
                        ("happiness", "health"))))
            else:
                before = _stat_snapshot(player, ("happiness", "health"))
                player.change_happiness(-rng.randint(1, 2))
                player.change_health(-rng.randint(1, 4))
                events.append(_event(
                    "health", "I'm suffering from depression.", player,
                    _changes(before, _stat_snapshot(
                        player, ("happiness", "health")),
                        ("happiness", "health"))))
        elif illness == "High Blood Pressure":
            before = _stat_snapshot(player, ("health",))
            player.change_health(-rng.randint(1, 5))
            events.append(_event(
                "health", "Your high blood pressure troubles you.", player,
                _changes(before, _stat_snapshot(player, ("health",)),
                         ("health",))))
            if player.stress > rng.randint(80, 95) \
                    and player.health < rng.randint(1, 15) \
                    and rng.randint(1, 3) == 1:
                events.append(_event(
                    "death", "You died due to a massive heart attack.",
                    player))
                return _result(events=events, finished_turn=True,
                               continuation={"phase_id": "year_close",
                                             "item_cursor": 0})
            if player.stress < rng.randint(25, 60) and _one_in(rng, 2):
                before = _stat_snapshot(player, ("happiness", "health"))
                player.change_happiness(rng.randint(4, 8))
                player.change_health(rng.randint(4, 8))
                player.remove_illness("High Blood Pressure")
                events.append(_event(
                    "health",
                    "I am no longer suffering from high blood pressure",
                    player, _changes(before, _stat_snapshot(
                        player, ("happiness", "health")),
                        ("happiness", "health"))))
        elif illness == "Cancer":
            before = _stat_snapshot(player, ("happiness", "health"))
            player.change_happiness(-rng.randint(5, 9))
            player.change_health(-rng.randint(10, 18))
            events.append(_event(
                "health", "You are suffering from cancer.", player,
                _changes(before, _stat_snapshot(
                    player, ("happiness", "health")),
                    ("happiness", "health"))))
            if player.health <= 0:
                events.append(_event(
                    "death",
                    "You died due to complications associated with cancer.",
                    player))
                return _result(events=events, finished_turn=True,
                               continuation={"phase_id": "year_close",
                                             "item_cursor": 0})
        elif illness in ("Common Cold", "Flu"):
            player.remove_illness(illness)
            events.append(_event(
                "health",
                f"I recovered from the {str(illness).lower()}.", player))
    if player.age >= 4 and _one_in(rng, 2000):
        before = _stat_snapshot(player, ("happiness", "health"))
        player.add_illness(TranslateMarker("Cancer"))
        player.change_health(-rng.randint(16, 50))
        player.change_happiness(-rng.randint(30, 50))
        events.append(_event(
            "health", "I've been diagnosed with cancer.", player,
            _changes(before, _stat_snapshot(player, ("happiness", "health")),
                     ("happiness", "health"))))
    elif _one_in(rng, 20):
        if player.age >= 3 and _one_in(rng, 6):
            before = _stat_snapshot(player, ("happiness", "health"))
            player.add_illness(TranslateMarker("Flu"))
            player.change_health(-rng.randint(2, 5))
            player.change_happiness(-rng.randint(3, 5))
            events.append(_event("health", "I've come down with the flu.",
                                 player, _changes(
                                     before, _stat_snapshot(
                                         player, ("happiness", "health")),
                                     ("happiness", "health"))))
        else:
            before = _stat_snapshot(player, ("happiness", "health"))
            player.add_illness(TranslateMarker("Common Cold"))
            player.change_health(-rng.randint(1, 4))
            player.change_happiness(-rng.randint(2, 4))
            events.append(_event(
                "health", "I've come down with the common cold.", player,
                _changes(before, _stat_snapshot(
                    player, ("happiness", "health")),
                    ("happiness", "health"))))
    if player.happiness < rng.randint(1, 10) and not player.is_depressed():
        if not player.has_trait("NON_EMOTIONAL") or _one_in(rng, 2):
            before = _stat_snapshot(player, ("happiness", "health"))
            player.add_illness(TranslateMarker("Depression"))
            player.change_happiness(-50)
            player.change_health(-rng.randint(4, 8))
            events.append(_event(
                "health", "I'm suffering from depression.", player,
                _changes(before, _stat_snapshot(
                    player, ("happiness", "health")),
                    ("happiness", "health"))))
    return _result(events=events,
                   continuation={"phase_id": "annual_events",
                                 "item_cursor": 0})


_PHASES["illness"] = illness_phase


# ==================================================== FAMILY_EVENTS (phase)
# Rows 48/49 (pregnancy/birth + naming decision) and 76 (child yearly split
# into the three canonical subrules). Babies are created through A's hook
# (player.generate_child) at SAMPLE time; the stored naming decision only
# applies the chosen name. A walks the year snapshot so newborns get no
# further update this turn.

def child_stat_year(child, rng):
    """Child.age_up stats/school slice (child.py): happiness drift, child
    university progression and school milestones. Returns events."""
    events = []
    if rng.randint(1, 2) == 1:
        if child.happiness < rng.randint(46, 50):
            child.change_happiness(rng.randint(0, 4))
        elif child.happiness > rng.randint(50, 54):
            child.change_happiness(-rng.randint(0, 3))
    if child.is_in_uv > 0:
        child.is_in_uv -= 1
        if child.is_in_uv == 0:
            child.change_smarts(rng.randint(6, 12))
            child.change_happiness(rng.randint(8, 14))
            events.append(_event(
                "family", f"Your {child.name_accusative()} graduated from "
                "university.", child))
    if child.age == 6:
        child.change_smarts(rng.randint(0, 2))
    if child.age == 12:
        child.change_smarts(rng.randint(0, 3))
    if child.age == 14:
        child.change_smarts(rng.randint(0, 4))
    if child.age == 17:
        child.change_smarts(rng.randint(5, 10))
        child.change_happiness(rng.randint(5, 10))
        if child.smarts >= rng.randint(35, 45) and rng.randint(1, 4) < 4:
            child.is_in_uv = 4
    return events


def child_money_year(child, rng):
    """Child.age_up income slice: net income to the CHILD's account as a
    child_net money entry (M1-fixed net, not full salary)."""
    if child.salary <= 0:
        return [], 0
    child.years_worked += 1
    retention = sample_expense_retention(rng)
    quote = annual_income_quote(child.salary, True, 40, retention)
    entry = _money("family_events", f"child_net:{_pid(child)}", "net", child,
                   "child_net", quote["net"], "Child net income")
    return [entry], quote["net"]


def child_social_year(child, rng):
    """Child.age_up partner/job slice (child.py). Partner death and the
    divorce/breakup/marriage transitions; jobs for unemployed adults."""
    events = []
    if child.partner:
        child.partner.age_up()
        if child.partner.death_check():
            child.partner = None
        else:
            breakup_chance = 45 if child.partner.status == 2 else 15
            if (child.may_divorce or child.partner.status != 2) \
                    and rng.randint(1, breakup_chance) == 1:
                child.change_happiness(-rng.randint(5, 20))
                if child.partner.status == 2:
                    if child.partner_desire > (cap := rng.randint(5, 12)):
                        child.partner_desire -= rng.randint(3, 10)
                        if child.partner_desire < cap:
                            child.partner_desire = cap
                else:
                    if child.partner_desire > (cap := rng.randint(10, 15)):
                        child.partner_desire -= rng.randint(1, 6)
                        if child.partner_desire < cap:
                            child.partner_desire = cap
                child.partner = None
            elif (child.age >= 18 and child.partner.age >= 18
                    and child.partner.status < 2
                    and child.partner.years_together
                    >= rng.randint(3, 7) and _one_in(rng, 5)):
                child.partner.change_relationship(rng.randint(10, 45))
                _guard_romance(child, child.partner)
                child.partner.status = 2
                child.may_divorce = _one_in(rng, 2)
    elif child.age >= 18 and rng.randint(1, 700) <= child.partner_desire:
        attempts = min(rng.randint(9, 11),
                       _round_stochastic(rng, child.partner_desire / 8))
        for _ in range(max(0, attempts)):
            partner = child.generate_partner()
            _guard_romance(child, partner)
            if partner.compatibility_check(child):
                child.partner = partner
                child.change_happiness(rng.randint(5, 20))
                break
    can_get_job = child.salary == 0 and child.age >= 18 \
        and child.is_in_uv == 0
    if can_get_job and rng.randint(1, 3) == 1:
        for _ in range(rng.randint(2, 7)):
            salary = _round_stochastic(rng, _randexpo(rng, 30000, 65000))
            if child.salary < rng.randint(30000, 40000) \
                    and rng.randint(1, 3) < 3:
                continue
            m = 100 + _round_stochastic(rng, (salary - 40000) / 300)
            mod = 50 - child.smarts
            roll = rng.randint(1, m)
            if mod > 0:
                roll += rng.randint(0, mod)
            elif mod < 0:
                roll -= rng.randint(0, abs(mod))
            if roll <= 100:
                child.years_worked = 0
                child.salary = salary
                break
    return events


def family_events_phase(player, turn_id, rng, snapshot_ids=None):
    """family_events phase: births/naming first, then per-child yearly
    subrules. A iterates the frozen turn snapshot: pass snapshot_ids
    (person_id set) so only snapshot members are updated — newborns are
    excluded (age 0, no second yearly update) and non-members are skipped."""
    events = []
    money = []
    pending = None
    if player.gender == Gender.Female and player.is_pregnant:
        player.is_pregnant = False
        if not _x_in_y(rng, player.fertility, 150) and _one_in(rng, 5):
            before = _stat_snapshot(player, ("happiness",))
            player.change_happiness(-rng.randint(30, 50))
            events.append(_event(
                "family", "I suffered a miscarriage and lost the baby!",
                player, _changes(before, _stat_snapshot(
                    player, ("happiness",)), ("happiness",))))
        else:
            father = player._recent_child_father or player.partner
            player._recent_child_father = None
            _check(father is not None, "invalid_input",
                   "no father recorded for the pregnancy")
            c = player.generate_child(father)  # A's canonical creation hook
            events.extend(_birth_events(player, c, rng, mother=True))
            pending = {
                "kind": "baby_naming",
                "title": f"A baby {'boy' if c.gender == Gender.Male else 'girl'}!",
                "text": "What will you name them?",
                "person_ids": [_pid(c)],
                "options": [{"id": "name", "label": "Choose a name",
                             "inputs": [{"id": "name", "type": "text",
                                         "label": "First name",
                                         "required": True, "min": 1,
                                         "max": 40}]}],
                "sampled": {},
                "sampled_child_id": _pid(c),
                "sampled_child": c,
            }
    elif player.partner is not None and player.partner.is_pregnant:
        partner = player.partner
        partner.is_pregnant = False
        if not _x_in_y(rng, partner.fertility, 100) and _one_in(rng, 5):
            before = _stat_snapshot(player, ("happiness",))
            player.change_happiness(-rng.randint(30, 50))
            events.append(_event(
                "family",
                f"My {partner.name_accusative()} suffered a miscarriage "
                "and lost the baby!", player,
                _changes(before, _stat_snapshot(player, ("happiness",)),
                         ("happiness",))))
        else:
            c = player.generate_child(partner)
            events.extend(_birth_events(player, c, rng, mother=False))
            pending = {
                "kind": "baby_naming",
                "title": f"A baby {'boy' if c.gender == Gender.Male else 'girl'}!",
                "text": "What will you name them?",
                "person_ids": [_pid(c)],
                "options": [{"id": "name", "label": "Choose a name",
                             "inputs": [{"id": "name", "type": "text",
                                         "label": "First name",
                                         "required": True, "min": 1,
                                         "max": 40}]}],
                "sampled": {},
                "sampled_child_id": _pid(c),
                "sampled_child": c,
            }
    for child in list(player.children):
        if snapshot_ids is not None and _pid(child) not in snapshot_ids:
            continue  # newborn or non-snapshot member
        events.extend(child_stat_year(child, rng))
        entries, _ = child_money_year(child, rng)
        money.extend(entries)
        events.extend(child_social_year(child, rng))
    return _result(events=events, money_entries=money,
                   pending_decision=pending,
                   continuation={"phase_id": "illness", "item_cursor": 0})


def _birth_events(player, child, rng, mother):
    events = []
    before = _stat_snapshot(player, ("happiness", "stress"))
    player.change_happiness(rng.randint(35, 50))
    player.change_stress(rng.randint(0, 10))
    player.relations.append(child)
    player.children.append(child)
    if player.partner:
        player.partner.change_relationship(
            rng.randint(3, 10) if mother else rng.randint(8, 15))
    events.append(_event(
        "family", f"I'm the {'mother' if mother else 'father'} of a "
        f"baby named {child.name}.", player,
        _changes(before, _stat_snapshot(player, ("happiness", "stress")),
                 ("happiness", "stress"))))
    events[-1]["person_ids"].append(_pid(child))
    return events


def resolve_baby_naming(player, pending, option_id, inputs, rng):
    """apply_action/resolve naming: bounded text input per frozen schema."""
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    child = pending.get("sampled_child")
    _check(child is not None, "invalid_input", "missing sampled child")
    _check(isinstance(inputs, dict), "invalid_input", "inputs must be an object")
    name = inputs.get("name")
    _check(isinstance(name, str), "invalid_input", "name must be a string")
    name = name.strip()
    _check(1 <= len(name) <= 40, "invalid_input",
           "name must be 1-40 characters")
    child.firstname = name
    child.update_name()
    return _result(events=[_event(
        "family", f"The baby was named {child.name}.", child)])


_RESOLVER_KINDS["baby_naming"] = resolve_baby_naming
_PHASES["family_events"] = family_events_phase


# ---- Row 38: Surrender (deliberate death, double-confirmed) ---------------

def sample_surrender(player, rng):
    """activities.py 'Surrender' as a stored two-step confirmation.

    Step 1 warns; step 2 is the final confirm. The terminal transition runs
    at resolve of step 2: A performs the death (status/archive/journal) and
    the succession flow; nothing else executes this turn except close work.
    """
    return _result(pending_decision={
        "kind": "surrender_confirm",
        "title": "Surrender this life",
        "text": "Are you sure you want to surrender this life?",
        "options": [{"id": "continue", "label": "Keep living"},
                    {"id": "ask_final", "label": "Continue to final warning",
                     "known_effects": ["this cannot be undone"]}],
        "sampled": {},
    })


def resolve_surrender_confirm(player, pending, option_id, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "continue":
        return _result(events=[_event("life", "I keep living.", player)])
    return _result(pending_decision={
        "kind": "surrender_final",
        "title": "Final warning",
        "text": "This will kill your current character. Continue?",
        "options": [{"id": "cancel", "label": "Cancel"},
                    {"id": "surrender", "label": "End this life",
                     "known_effects": ["death", "life summary",
                                       "continue as a child if eligible"]}],
        "sampled": {},
    })


def resolve_surrender_final(player, pending, option_id, rng):
    """The final confirm: terminal transition. A performs the actual death
    (die() equivalent: status dead, archive, summary) and succession."""
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "cancel":
        return _result(events=[_event("life", "I keep living.", player)])
    return _result(
        events=[_event("death", "I surrendered.", player)],
        finished_turn=True,
        continuation={"phase_id": "year_close", "item_cursor": 0})


_RESOLVER_KINDS["surrender_confirm"] = resolve_surrender_confirm
_RESOLVER_KINDS["surrender_final"] = resolve_surrender_final
_SELF_ACTIONS["surrender"] = sample_surrender


# ============================================================ SUCCESSION
# Parity row 77. A owns the identity-preserving graph conversion
# (convert_child_to_player); C supplies the eligibility predicate, the
# balanced cash transfer instructions and the successor gameplay state,
# all derived from the legacy convert_child_to_player semantics.

def eligible_successors(player):
    """Children the player can continue as: living children of the player.
    (Legacy allowed any child; eligibility is membership + liveness.)"""
    return [c for c in player.children
            if getattr(c, "health", 1) > 0]


def succession_instructions(player, child, rng):
    """Balanced transfer + gameplay instructions for A's graph conversion.

    Cash (legacy split): the successor inherits max(1, round(money/num))
    via the ledger (category transfer, phase succession); the residual
    stays with the deceased's estate and is recorded as an estate_close
    posting — share + residual == player.money exactly: no manufactured
    or silently dropped money. Gameplay state mirrors convert_child_to_player:
    generation+1, carried-over job/university state, carried-over partner.
    A performs every graph/identity mutation from these instructions.
    """
    _check(child in player.children, "not_allowed",
           "the successor must be one of your children")
    _check(getattr(child, "health", 1) > 0, "not_allowed",
           "the successor must be alive")
    num = max(1, len(player.children))
    money_entries = []
    share = 0
    residual = player.money
    if player.money > 0:
        share = max(1, _round_stochastic(rng, player.money / num))
        money_entries.append(_money("succession", "inheritance", "cash",
                                    child, "transfer", share,
                                    f"Cash inheritance from {player.name}"))
        residual = player.money - share
        money_entries.append(_money("succession", "estate", "residual",
                                    player, "estate_close", -residual,
                                    "Residual estate closed with the life"))
    gameplay = {
        "generation": player.generation + 1,
        "age": child.age,
        "happiness": round_stochastic_pair(
            rng, getattr(child, "relationship", child.happiness),
            child.happiness),
        "health": child.health,
        "smarts": child.smarts,
        "looks": child.looks,
    }
    uv_years = getattr(child, "is_in_uv", 0)
    if uv_years:
        gameplay["university"] = {
            "chose_student_loan": True,
            "uv_years": uv_years,
        }
    if child.salary > 0:
        gameplay["job"] = {
            "salary": child.salary,
            "years_worked": child.years_worked,
            "salary_years": [child.salary] * child.years_worked,
        }
    instructions = {
        "successor_id": _pid(child),
        "deceased_id": _pid(player),
        "generation": player.generation + 1,
        "money_entries": money_entries,
        "cash_share": share,
        "estate_residual": residual,
        "gameplay": gameplay,
        "sibling_ids": [_pid(c) for c in player.children if c is not child],
        "partner_id": _pid(child.partner) if child.partner else None,
        "preserve": ["person_id", "appearance", "firstname", "lastname"],
    }
    return instructions


def round_stochastic_pair(rng, relationship, happiness):
    """Legacy convert: successor happiness = round((rel + happy) / 2)."""
    return round((relationship + happiness) / 2)


# ============================================== AGEING SUBRULES (map §1)
# Canonical headless entries for relations_age / player_stats. Composite
# Person.age_up/Player.age_up stay terminal-only.

def increment_age(person):
    """Age +1 only (no drift)."""
    person.age += 1
    return person.age


def stat_drift(player, rng):
    """Person.age_up stat drift with explicit rng (age must already be
    incremented by increment_age): +-3 on the four stats, teen looks
    progression, post-50 looks decay."""
    player.change_happiness(rng.randint(-3, 3))
    player.change_health(rng.randint(-3, 3))
    player.change_smarts(rng.randint(-3, 3))
    player.change_looks(rng.randint(-3, 3))
    if player.age == 13:
        if rng.randint(1, 4) == 1:
            player.teen_looks_inc = rng.randint(0, 1)
        else:
            player.teen_looks_inc = min(rng.randint(0, 12)
                                        for _ in range(4))
    if player.age >= 13 and player.age < rng.randint(24, 27):
        player.change_looks(player.teen_looks_inc)
    if player.age > 50 and player.looks > rng.randint(20, 25) \
            and rng.randint(1, 4) < 4:
        decay = min((player.age - 51) // 5 + 1, 4)
        player.change_looks(-rng.randint(0, decay))


def relation_year_drift(relation, player_age, rng):
    """Parent relationship drift: +1 while the player is under 18, else
    -1/-1/0. Other relation kinds have no automatic yearly drift."""
    from src.people.classes.parent import Parent
    if isinstance(relation, Parent):
        if player_age < 18:
            relation.change_relationship(1)
        else:
            relation.change_relationship(rng.choice((-1, -1, 0)))
    return relation.relationship


def relation_year(relation, player_age, rng):
    """The ONE C primitive for a relation's yearly update (map section 1;
    A's relations_age calls this for SNAPSHOT members only). Performs the
    full legacy relation year EXCEPT child schooling/income/social (those
    are family_events' child slices): age+1, Person stat drift, per-year
    interaction flag resets, Partner extras, relationship drift.
    Composite relationship.age_up/Child.age_up are never called."""
    increment_age(relation)
    stat_drift(relation, rng)
    for flag in ("spent_time", "had_conversation", "was_complimented"):
        if hasattr(relation, flag):
            setattr(relation, flag, False)
    if relation is not None and hasattr(relation, "years_together"):
        relation.years_together += 1
        if hasattr(relation, "was_proposed_to"):
            relation.was_proposed_to = False
        if hasattr(relation, "asked_renew_vows"):
            relation.asked_renew_vows = False
    relation_year_drift(relation, player_age, rng)
    return relation


def apply_yearly_trait_effects(player, rng):
    """Player.age_up trait block with explicit rng (existing-trait
    modifiers only; trait gains are annual_events stored choices)."""
    events = []
    if player.age >= 6:
        if player.has_trait("WELL_KEPT"):
            player.change_looks(rng.randint(0, 2))
        elif player.has_trait("BADLY_KEPT"):
            player.change_looks(-rng.randint(0, 3))
    if player.has_trait("SICKLY"):
        player.change_health(-rng.randint(0, 4))
    if player.has_trait("MOODY") and not player.is_depressed():
        player.change_happiness(rng.randint(-6, 6))
    if player.has_trait("GRUMPY") and player.happiness > 33:
        if rng.randint(1, 10) == 1:
            player.change_happiness(-rng.randint(5, 8))
        else:
            player.change_happiness(-rng.randint(1, 5))
    if player.has_trait("FAST_WORKER"):
        player.change_performance(rng.randint(0, 3))
    elif player.has_trait("SLOW_WORKER"):
        player.change_performance(-rng.randint(0, 3))
    if player.has_trait("LAZY"):
        player.change_performance(-rng.randint(0, 5))
        player.change_stress(-rng.randint(0, 4))
    if player.has_trait("GENIUS"):
        if player.age < rng.randint(18, 25):
            player.change_smarts(rng.randint(0, 9))
        else:
            player.change_smarts(rng.randint(0, 4))
    return events


# ---- Row 37: Lottery ------------------------------------------------------

def sample_lottery(player, rng):
    """activities.py 'Lottery': ticket count choice; the winning numbers
    and the next jackpot are sampled NOW (stored private); guesses arrive
    as bounded inputs at resolve."""
    _check(player.age >= 18, "not_allowed", "you are too young")
    # Affordability gates the paid OPTIONS in the pending (and resolve
    # re-checks); the action itself stays available so a broke player can
    # walk away — frozen M1 behavior (test_game_m2a lottery flow).
    winning = sorted(rng.sample(range(1, 21), 4))
    next_jackpot = _round_stochastic(rng, _randexpo(rng, 100000, 1000000))
    return _result(pending_decision={
        "kind": "lottery",
        "title": "Lottery",
        "text": f"Play the lottery today! Jackpot: "
                f"${player.lottery_jackpot:,}. Guess the 4 winning numbers "
                f"(1-20, unique).",
        "options": [
            {"id": "buy_one", "label": "Buy a ticket ($4)",
             "enabled": player.money >= 4,
             "reason": None if player.money >= 4 else
                       f"a ticket costs $4 and you have ${player.money:,}",
             "known_costs": [{"label": "Ticket", "amount": 4}],
             "inputs": [{"id": "numbers", "type": "numbers", "count": 4,
                         "min": 1, "max": 20, "unique": True,
                         "label": "Your numbers", "required": True}]},
            {"id": "buy_ten", "label": "Buy 10 tickets ($40)",
             "enabled": player.money >= 40,
             "reason": None if player.money >= 40 else
                       f"10 tickets cost $40 and you have ${player.money:,}",
             "known_costs": [{"label": "Tickets", "amount": 40}],
             "inputs": [{"id": "numbers", "type": "numbers", "count": 4,
                         "min": 1, "max": 20, "unique": True,
                         "lines": 10, "label": "Your numbers (one line per "
                         "ticket)", "required": True}]},
            {"id": "back", "label": "Back", "known_costs": []},
        ],
        "sampled": {"winning": winning, "next_jackpot": next_jackpot},
    })


def _validate_guess_line(line):
    _check(isinstance(line, (list, tuple)) and len(line) == 4,
           "invalid_input", "each ticket needs exactly 4 numbers")
    nums = []
    for v in line:
        _check(isinstance(v, int) and not isinstance(v, bool),
               "invalid_input", "numbers must be integers")
        _check(1 <= v <= 20, "invalid_input",
               "numbers must be between 1 and 20")
        nums.append(v)
    _check(len(set(nums)) == 4, "invalid_input",
           "numbers must be unique within a line")


def resolve_lottery(player, pending, option_id, inputs, rng):
    options = [o["id"] for o in pending.get("options", [])]
    _check(option_id in options, "invalid_input", "unknown option")
    if option_id == "back":
        return _result(events=[_event("finances", "I saved my money.",
                                      player)])
    _check(isinstance(inputs, dict), "invalid_input",
           "inputs must be an object")
    raw = inputs.get("numbers")
    lines = raw if option_id == "buy_ten" else [raw]
    _check(isinstance(lines, list) and lines, "invalid_input",
           "numbers are required")
    if option_id == "buy_ten":
        _check(len(lines) <= 10, "invalid_input", "at most 10 tickets")
    for line in lines:
        _validate_guess_line(line)
    cost = 4 * (10 if option_id == "buy_ten" else 1) \
        if option_id == "buy_ten" else 4
    _check(player.money >= cost, "not_allowed",
           f" tickets cost ${cost} and you have ${player.money}")
    sampled = pending.get("sampled") or {}
    _check(isinstance(sampled.get("winning"), list), "invalid_input",
           "missing stored winning numbers")
    money = [_money("annual_events", "lottery", "tickets", player,
                    "activity", -cost, "Lottery tickets")]
    events = []
    won = any(set(line) == set(sampled["winning"]) for line in lines)
    if won:
        money.append(_money("annual_events", "lottery", "jackpot", player,
                            "lottery", player.lottery_jackpot,
                            "Lottery jackpot"))
        player.change_happiness(100)
        player.lottery_jackpot = sampled["next_jackpot"]
        events.append(_event(
            "finances", f"I won the lottery jackpot!", player))
    else:
        events.append(_event(
            "finances",
            f"I didn't win. The winning numbers were "
            f"{', '.join(map(str, sampled['winning']))}.", player))
    return _result(events=events, money_entries=money)


_RESOLVER_KINDS["lottery"] = resolve_lottery
_SELF_ACTIONS["lottery"] = sample_lottery
