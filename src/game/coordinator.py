"""Yearly turn coordinator (A-owned).

Runs the frozen phase order from docs/collab/M2/phase-map.md against the
transaction-local envelope. Phases implemented by C (src/menus/rule_actions.py)
dispatch there; A-owned phases run built-in minimal implementations that
preserve M1 behavior until C's extractions land. Money entries post through
the single ledger authority; events become journal entries; pending choices
and cursors persist at every boundary.
"""

import random as _random
from random import randint

from src.game.context import RuleContext
from src.game import rule_adapter

PHASE_ORDER = [
    "year_start", "relations_age", "player_stats", "mortality",
    "work_income", "education", "debt_service", "family_events",
    "illness", "annual_events", "decision_offer", "year_close",
]

RULE_PHASES = ("work_income", "education", "debt_service",
               "family_events", "illness")  # served by rule_actions._PHASES


def _year_start(env, ctx, service, rng):
    from src.menus import rule_actions
    player = env["player"]
    player.total_happiness += player.happiness
    rule_actions.increment_age(player)
    # legacy per-year reset (Player.age_up's reset_already_did): annual
    # action flags, date_options and witch_doctor_health — without this,
    # once-per-year actions and dating never become available again
    player.reset_already_did()
    env["year_actions"] = {"spend_time": []}
    env["turn"]["snapshot"] = [
        r.person_id for r in player.relations
        if r.person_id not in env.get("dead_ids", [])]
    env["turn"]["year_start_happiness"] = player.happiness
    return {"events": [], "money_entries": [], "pending_decision": None,
            "continuation": {"phase_id": "relations_age", "item_cursor": 0},
            "finished_turn": False}


def _relations_age(env, ctx, service, rng):
    from src.menus import rule_actions
    player = env["player"]
    # snapshot walk only: newborns and late additions receive NO yearly
    # update; one primitive call per relation (no composite age_up, so
    # child schooling/income/social cannot double-run in family_events)
    snapshot = env["turn"].get("snapshot") or []
    for relation in list(player.relations):
        if snapshot and relation.person_id not in snapshot:
            continue
        rule_actions.relation_year(relation, player.age, rng)
    return {"events": [], "money_entries": [], "pending_decision": None,
            "continuation": {"phase_id": "player_stats", "item_cursor": 0},
            "finished_turn": False}


def _player_stats(env, ctx, service, rng):
    from src.menus import rule_actions
    player = env["player"]
    # C's extracted subrules — the single source for player drift and
    # existing-trait modifiers (age itself is year_start's increment_age)
    rule_actions.stat_drift(player, rng)
    rule_actions.apply_yearly_trait_effects(player, rng)
    player.change_karma(randint(-2, 2))  # legacy karma drift
    return {"events": [], "money_entries": [], "pending_decision": None,
            "continuation": {"phase_id": "work_income", "item_cursor": 0},
            "finished_turn": False}


def _mortality_phase(env, ctx, service, rng):
    """Phase 4 (legacy parity): relation deaths BEFORE income — the
    deceased are not paid. C's sample_relation_death turns each death into
    reason/inheritance/funeral; A performs the graph mutation (removal,
    partner/child clearing, archive). Multiple deaths in one year queue
    their funerals; the turn continues only when the queue is empty."""
    from src.menus import rule_actions
    player = env["player"]
    queue = env["turn"].setdefault("funeral_queue", [])
    pending = None
    for relation in list(player.relations):
        sudden = relation.age >= 6 and _random.randint(1, 1400) == 1
        if not (sudden or relation.death_check()):
            continue
        result = rule_actions.sample_relation_death(
            player, relation, rng, sudden=sudden)
        # A owns the graph mutation (legacy process_relation_death identity
        # semantics), preserving canonical person_id + history
        player.relations.remove(relation)
        env["dead_ids"].append(relation.person_id)
        if relation not in env["archive"]:
            env["archive"].append(relation)
        if type(relation).__name__ == "Partner":
            for c in player.children:
                if c.father is relation:
                    c.father = None
                elif c.mother is relation:
                    c.mother = None
            if player.partner is relation:
                player.partner = None
        elif type(relation).__name__ == "Child":
            if relation in player.children:
                player.children.remove(relation)
        rule_adapter.post_money_entries(
            env, service, env["turn"]["turn_id"], player.age,
            result.get("money_entries"))
        rule_adapter.journal_events(env, result.get("events"))
        funeral = result.get("pending_decision")
        if funeral is not None:
            funeral["person_ids"] = [relation.person_id]
            queue.append(funeral)
    if queue:
        pending = queue.pop(0)
    result = {"events": [], "money_entries": [],
              "pending_decision": pending,
              "continuation": {"phase_id": "work_income", "item_cursor": 0},
              "finished_turn": False}
    if not queue:
        death = _player_death_check(env, ctx, service, rng)
        if death["finished_turn"]:
            return death
    return result


def _player_death_check(env, ctx, service, rng):
    player = env["player"]
    if player.death_check():
        env["status"] = "dead"
        env["dead_ids"].append(player.person_id)
        rule_adapter.journal_events(env, [{
            "kind": "death",
            "text": f"{player.name} passed away at the age of {player.age}.",
            "person_ids": [player.person_id],
            "changes": {},
        }])
        env["pending"] = None
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "year_close", "item_cursor": 0},
                "finished_turn": True}
    return {"events": [], "money_entries": [], "pending_decision": None,
            "continuation": {"phase_id": "decision_offer", "item_cursor": 0},
            "finished_turn": False}


def _decision_offer(env, ctx, service, rng):
    """M1-contextual decision_offer phase, served from event DATA FILES via
    the EventEngine (M2c content pipeline). Falls back to the legacy
    in-code templates only when no valid event files are installed."""
    from src.game import events as event_mod
    player = env["player"]
    parent_ids = [p.person_id for p in service._walk_people(player)
                  if isinstance(p, Parent)]
    seq = env["next_decision_num"]
    try:
        engine_events = event_mod.load_event_files()
    except (event_mod.EventValidationError, OSError, ValueError):
        engine_events = []
    if engine_events:
        engine = event_mod.EventEngine(engine_events)
        context = event_mod.GameContext(env, service)
        # independent legacy chances (chance_n) execute separately from
        # the flavor pool: roll once per year, serve one queued
        # occurrence per decision_offer invocation so unrelated flavor
        # content can never dilute 1/N rates (Codex #5)
        if not env["turn"]["done"].get("independent_rolled"):
            env["turn"]["independent_rolled"] = True
            passed = engine.sample_independent(
                rng, player.age, env["used_decisions"], context)
            if passed:
                env["turn"]["event_queue"] = [e["id"] for e in passed]
        queue = env["turn"].get("event_queue", [])
        while queue:
            # serve queued AUTO events immediately, in legacy order;
            # only a choice event interrupts the phase (and the phase
            # stays open while occurrences remain — Codex #5)
            queued = next(e for e in engine_events
                          if e["id"] == queue[0])
            if not queued.get("auto"):
                break
            queue.pop(0)
            if not queue:
                env["turn"].pop("event_queue", None)
            bundle = event_mod.EventEngine.instantiate_auto(
                queued, rng, context)
            text = bundle.get("_outcome") or queued.get("text") \
                or queued.get("title", "Something happened.")
            rule_adapter.apply_effects_bundle(
                env, service, player, bundle.get("_effects"),
                occurrence=f"{queued['id']}:auto",
                person_ids=bundle.get("_person_ids_extra") or [],
                journal_kind="death" if bundle["terminal"] else "event",
                outcome_text=text.format(name=player.firstname))
            env["used_decisions"].append(queued["id"])
            if bundle["terminal"]:
                env["status"] = "dead"
                env["dead_ids"].append(player.person_id)
                env["pending"] = None
                return {"events": [], "money_entries": [],
                        "pending_decision": None,
                        "continuation": {"phase_id": "year_close",
                                         "item_cursor": 0},
                        "finished_turn": True}
        if queue:
            ev = next(e for e in engine_events
                      if e["id"] == queue[0])
            queue.pop(0)
            if not queue:
                env["turn"].pop("event_queue", None)
        else:
            ev = engine.sample_flavor(rng, player.age,
                                      env["used_decisions"], context)
        if ev is None:
            return {"events": [], "money_entries": [],
                    "pending_decision": None,
                    "continuation": {"phase_id": "year_close",
                                     "item_cursor": 0},
                    "finished_turn": False}
        env["next_decision_num"] += 1
        env["used_decisions"].append(ev["id"])
        if ev.get("auto"):
            # v2.1 automatic event: apply the sampled outcome immediately,
            # journal it, no pending (a 1/5000 lightning strike must never
            # 500 the age_up)
            bundle = event_mod.EventEngine.instantiate_auto(ev, rng,
                                                            context)
            text = bundle.get("_outcome") or ev.get("text") \
                or ev.get("title", "Something happened.")
            rule_adapter.apply_effects_bundle(
                env, service, player, bundle.get("_effects"),
                occurrence=f"{ev['id']}:auto",
                person_ids=bundle.get("_person_ids_extra") or [],
                journal_kind="death" if bundle["terminal"] else "event",
                outcome_text=text.format(name=player.firstname))
            if bundle["terminal"]:
                # A performs the actual death
                env["status"] = "dead"
                env["dead_ids"].append(player.person_id)
                env["pending"] = None
                return {"events": [], "money_entries": [],
                        "pending_decision": None,
                        "continuation": {"phase_id": "year_close",
                                         "item_cursor": 0},
                        "finished_turn": True}
            return {"events": [], "money_entries": [],
                    "pending_decision": None,
                    "continuation": {"phase_id": "year_close",
                                     "item_cursor": 0},
                    "finished_turn": False}
        needs_parents = any(
            c.get("kind") == "alive" and c.get("role") == "parents"
            for c in ev.get("conditions", {}).get("requires", []))
        pending = event_mod.EventEngine.instantiate(
            ev, seq, player.firstname,
            parent_ids if needs_parents else [],
            rng=rng, context=context)
        # event pre-effects apply ONCE at offer time (Codex #4): real
        # deltas journaled, marker persisted; C's resolver skips them
        # when the marker is present and re-applies only legacy pendings
        if pending.get("pre_effects"):
            rule_adapter.apply_effects_bundle(
                env, service, player, pending["pre_effects"],
                occurrence=f"{ev['id']}:pre",
                person_ids=pending.get("_pre_person_ids_extra") or [],
                journal_kind="event",
                outcome_text=pending["text"])
            pending["pre_applied"] = True
        return {"events": [], "money_entries": [],
                "pending_decision": pending,
                "continuation": {"phase_id": "year_close",
                                 "item_cursor": 0},
                "finished_turn": False,
                # the phase is NOT complete while independent occurrences
                # remain queued: resume re-enters decision_offer instead
                # of skipping it as done
                "_keep_phase": bool(env["turn"].get("event_queue"))}
    # legacy fallback: in-code templates
    from src.game import decisions
    decisions.validate_templates()  # soft-lock invariant over all templates
    template = decisions.sample(
        _random, player.age, env["used_decisions"], parent_ids)
    if template is not None and not decisions.has_free_option(template):
        # never offer a decision with no always-available option
        template = None
    if template is None:
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": "year_close", "item_cursor": 0},
                "finished_turn": False}
    env["next_decision_num"] += 1
    env["used_decisions"].append(template["id"])
    pending = decisions.instantiate(template, seq, player.firstname,
                                    parent_ids if template["person_ids"] == "parents" else [])
    return {"events": [], "money_entries": [], "pending_decision": pending,
            "continuation": {"phase_id": "year_close", "item_cursor": 0},
            "finished_turn": False}


def _year_close(env, ctx, service, rng):
    return {"events": [], "money_entries": [], "pending_decision": None,
            "continuation": None, "finished_turn": True}


_DEFAULT_IMPLS = {
    "year_start": _year_start,
    "relations_age": _relations_age,
    "player_stats": _player_stats,
    "mortality": _mortality_phase,
    "decision_offer": _decision_offer,
    "year_close": _year_close,
}

from src.people.classes.parent import Parent  # noqa: E402


def _noop_phase(next_phase):
    def run(env, ctx, service, rng):
        return {"events": [], "money_entries": [],
                "pending_decision": None,
                "continuation": {"phase_id": next_phase, "item_cursor": 0},
                "finished_turn": False}
    return run


_NOOP_NEXT = {
    "debt_service": "family_events",
    "family_events": "illness",
    "illness": "annual_events",
    "annual_events": "decision_offer",
}


def run_turn(env, service, rng):
    """Run yearly phases from the persisted cursor until a pending choice,
    a finished turn, or death.

    Returns True only when the year RAN TO COMPLETION (every phase done,
    no pending choice): callers reset the turn in that case. Returns
    False when interrupted by a stored decision or a terminal outcome —
    the interrupted turn (id, done markers, snapshot, queues) stays
    untouched on the envelope so a later resolve can resume it. The
    funeral queue is served BEFORE continuing (resolve path). Loop-guarded.
    """
    from src.game.service import GameError
    ctx = RuleContext(env, service)
    turn = env["turn"]
    turn.setdefault("done", {})
    done = turn["done"]
    start = turn["cursor"]["phase_id"]
    idx = PHASE_ORDER.index(start) if start in PHASE_ORDER else 0
    year_start_happiness = turn.get("year_start_happiness")

    for _ in range(len(PHASE_ORDER) + 5):  # loop guard
        phase_id = PHASE_ORDER[idx]
        if phase_id in done:
            idx += 1
            if idx >= len(PHASE_ORDER):
                break
            continue
        if phase_id in RULE_PHASES:
            from src.menus import rule_actions
            result = rule_actions.advance_phase(
                ctx, phase_id, rng, turn["turn_id"],
                year_start_happiness=year_start_happiness)
        else:
            impl = _DEFAULT_IMPLS.get(phase_id) or _noop_phase(
                _NOOP_NEXT.get(phase_id, "year_close"))
            result = impl(env, ctx, service, rng)
        rule_adapter.post_money_entries(
            env, service, turn["turn_id"], env["player"].age,
            result.get("money_entries"))
        rule_adapter.journal_events(env, result.get("events"))
        finished = rule_adapter.apply_cursor_update(env, result)
        if env.get("pending") and "id" not in env["pending"]:
            env["pending"]["id"] = f"d_{env['next_decision_num']}"
            env["next_decision_num"] += 1
        if env.get("pending"):
            # phase interrupted by a choice: keep the ENTIRE turn (id,
            # done markers, snapshot, queues) and remember where to
            # resume; the resolve path continues from here
            if result.get("_keep_phase"):
                # phase incomplete (queued occurrences remain): resume
                # re-enters THIS phase, not the continuation
                env["turn"]["cursor"] = {"phase_id": phase_id,
                                         "item_cursor": 0}
            else:
                env["turn"]["cursor"] = result.get("continuation") or {
                    "phase_id": PHASE_ORDER[idx + 1], "item_cursor": 0}
            env["turn"]["interrupted"] = True
            if not result.get("_keep_phase"):
                done[phase_id] = True
            return False
        done[phase_id] = True
        idx += 1
        if finished or idx >= len(PHASE_ORDER):
            break
    return not env.get("pending") and env["status"] != "dead"
