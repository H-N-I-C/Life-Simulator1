"""Dev-only scenario starts (spec addendum): valid states reached through
REAL transitions — the game is created normally, then driven through the
public command flow with the game's own seeded RNG until the scenario's
conditions hold. No bare field edits. A scenario game is an ordinary game
afterwards (same save/replay/decision semantics).
"""

from src.game.service import GameError

SCENARIO_MAX_ATTEMPTS = 8


class Session:
    """In-memory scenario drive: many commands through the SAME code path
    as the public API (_execute), persisted ONCE at the end. Same real
    transitions; the per-command durability overhead is the only thing
    skipped (a failed setup rolls back entirely, matching command
    semantics)."""

    def __init__(self, svc, game_id):
        self.svc = svc
        self.game_id = game_id
        self.env = svc._load(game_id)
        self.st = svc._state(self.env)

    def state(self):
        self.st = self.svc._state(self.env)
        return self.st

    def command(self, request_id, action, payload=None):
        self.st = self.svc._execute(
            self.env, self.env["revision"], request_id, action,
            payload or {})
        return self.st

    def save(self):
        self.svc._save(self.env)

SCENARIOS = {
    "high_school_first_day": {
        "label": "First day of high school",
        "target_age": 14, "employ": False,
    },
    "graduating_18": {
        "label": "Age 18, just graduated",
        "target_age": 18, "employ": False, "decline_university": True,
    },
    "unemployed_18": {
        "label": "Age 18, graduated, unemployed",
        "target_age": 18, "employ": False, "decline_university": True,
    },
    "enrolled_child_10": {
        "label": "Child age 10, enrolled",
        "target_age": 10, "employ": False,
    },
    "employed_30": {
        "label": "30, employed",
        "target_age": 30, "employ": True,
    },
    "near_retirement_64": {
        "label": "64, near retirement",
        "target_age": 64, "employ": True,
    },
    "retirement_eligible_65": {
        "label": "65, retirement-eligible",
        "target_age": 65, "employ": True,
    },
    "university_pending_17": {
        "label": "17, university acceptance pending funding",
        "target_age": 17, "employ": False, "stop_at_pending": True,
    },
    "ill_with_cash": {
        "label": "Adult, two illnesses, cash on hand",
        "target_age": 30, "employ": True,
        "illnesses": ["Common Cold", "Flu"],
    },
    "married_30": {
        "label": "30, married",
        "target_age": 30, "employ": True, "married": True,
    },
    "married_with_child": {
        "label": "30, married, one child",
        "target_age": 30, "employ": True, "married": True,
        "children": 1, "baby_name": "Robin",
    },
    "pregnant_25": {
        "label": "25, married, expecting",
        "target_age": 25, "employ": True, "married": True,
        "pregnant": True,
    },
    "new_parent_25": {
        "label": "25, married, new baby",
        "target_age": 25, "employ": True, "married": True,
        "children": 1, "baby_name": "Robin",
    },
    "wealthy_married_40": {
        "label": "40, married, $1M+ (kidnapping test hook)",
        "target_age": 40, "employ": True, "married": True,
        "dev_adjustment": "wealth",
        "_dev_hook": "documented dev-only cash adjustment (row 69 has no "
                     "legitimate forcing path); never reachable in normal "
                     "play; kidnapping itself still rolls 1/250 naturally",
    },
    "low_performance_30": {
        "label": "30, employed, low performance (S6 test hook)",
        "target_age": 30, "employ": True,
        "dev_adjustment": "performance",
        "_dev_hook": "documented dev-only adjustment for D's M2a-S6; "
                     "never reachable in normal play",
    },
}


def age_up_safe(svc, game_id, st, i):
    if st["pending_decision"]:
        return _resolve_first(svc, game_id, st, i)
    return svc.command(game_id, st["revision"], f"drv-age-{i}",
                       "age_up", {})


def _resolve_first_sess(sess, i, prefer=None):
    d = sess.state()["pending_decision"]
    if d.get("kind") == "retirement_offer" and prefer is None:
        prefer = ("keep_working",)
    options = list(d["options"])
    if prefer:
        options.sort(key=lambda o: 0 if any(
            k in o["id"].lower() for k in prefer) else 1)
    last_err = None
    for opt in options:
        try:
            return sess.command(f"drv-dec-{i}-{opt['id']}",
                                "resolve_decision",
                                {"decision_id": d["id"],
                                 "option_id": opt["id"]})
        except GameError as e:
            if e.code != "not_allowed":
                raise
            last_err = e
    raise last_err


def _goals_met_sess(sess, spec):
    return _goals_met(sess.svc, sess.game_id, sess.state(), spec)


def _drive_romance_step_sess(sess, spec, i, state):
    st = sess.state()
    d = st["pending_decision"]
    if d:
        kind = d.get("kind")
        if kind == "baby_naming":
            return sess.command(
                f"drv-name-{i}", "resolve_decision",
                {"decision_id": d["id"], "option_id": d["options"][0]["id"],
                 "inputs": {"name": spec.get("baby_name", "Robin")}})
        if kind == "date_offer":
            return _resolve_first_sess(sess, i, ("accept", "yes", "date"))
        if kind == "university_application" \
                and not spec.get("accept_university"):
            return _resolve_first_sess(sess, i,
                                       ("decline", "no", "not", "skip"))
        if kind == "wedding_plan":
            venues = [o for o in d["options"] if o["id"] != "cancel"]
            venues.sort(key=lambda o: (o.get("known_costs") or [{}])[0].get(
                "amount", 10 ** 9))
            ordered = venues + [o for o in d["options"]
                                if o["id"] == "cancel"]
            last_err = None
            venues_ok = False
            for opt in ordered:
                if opt["id"] == "cancel":
                    continue  # never auto-cancel: earn and re-plan instead
                try:
                    venues_ok = True
                    return sess.command(
                        f"drv-dec-{i}-{opt['id']}", "resolve_decision",
                        {"decision_id": d["id"], "option_id": opt["id"]})
                except GameError as e:
                    if e.code != "not_allowed":
                        raise
                    last_err = e
            # every venue unaffordable this year: pass the turn and earn
            return sess.command(f"drv-age-{i}", "age_up")
        return _resolve_first_sess(sess, i)
    fam = st["family"]
    if spec.get("married") and not fam["partner"]:
        return sess.command(f"drv-partner-{i}", "find_partner")
    if spec.get("married") or spec.get("children"):
        env_partner = fam["partner"]
        if env_partner:
            status = fam.get("partner_status")
            if status in (None, 0):
                age = next(p for p in st["people"]
                           if p["id"] == st["player_id"])["age"]
                if state.get("age") != age:
                    state["age"] = age
                    state["used"] = set()
                actions = {a["id"]: a for a in st["available_actions"]}
                for act in ("conversation", "compliment", "spend_time",
                            "propose"):
                    if act in state["used"]:
                        continue
                    state["used"].add(act)
                    try:
                        if act == "propose":
                            return sess.command(f"drv-propose-{i}", "propose")
                        return sess.command(
                            f"drv-{act}-{i}", act,
                            {"target_id": env_partner})
                    except GameError as e:
                        if e.code != "not_allowed":
                            raise
                        continue
            if status == 1:
                return sess.command(f"drv-wed-{i}", "plan_wedding")
    want = spec.get("children", 0)
    baby_goal = max(want, 1 if spec.get("pregnant") else 0)
    if baby_goal and len(fam["children"]) < baby_goal \
            and not _pregnancy_in_progress(sess.svc, sess.game_id):
        try:
            return sess.command(f"drv-baby-{i}", "try_for_baby")
        except GameError as e:
            if e.code not in ("not_allowed", "refused"):
                raise
    return sess.command(f"drv-age-{i}", "age_up")


def _apply_setup_hook_sess(sess, spec, i):
    illnesses = spec.get("illnesses")
    adjustment = spec.get("dev_adjustment")
    if illnesses or adjustment:
        env = sess.env
        player = env["player"]
        if illnesses:
            from src.lifesim_lib.lifesim_lib import TranslateMarker
            for name in illnesses:
                player.add_illness(TranslateMarker(name))
        if adjustment == "performance":
            player.performance = 10
        elif adjustment == "wealth":
            # DEV-ONLY test hook (row 69): kidnapping needs $1M+ and the
            # LEGACY karma gate (NOT x_in_y(karma, 130) — lower karma
            # fires more often); neither has a legitimate forcing path
            player.money = 1_000_000
            player.karma = 50
    return sess.state()


def _pregnancy_in_progress(svc, game_id):
    env = svc._load(game_id)
    player = env["player"]
    if getattr(player, "is_pregnant", False):
        return True
    partner = getattr(player, "partner", None)
    return bool(partner and getattr(partner, "is_pregnant", False))


def _goals_met(svc, game_id, st, spec):
    fam = st["family"]
    if spec.get("married"):
        if not fam["partner"]:
            return False
        if fam.get("partner_status") is not None \
                and fam["partner_status"] < 2:
            return False
    if spec.get("pregnant"):
        # satisfied while expecting OR once the baby has arrived (the
        # birth may land between drive iterations)
        if _pregnancy_in_progress(svc, game_id):
            return True
        return len(fam["children"]) > 0
    n = spec.get("children", 0)
    if len(fam["children"]) < n:
        return False
    return True


def _drive_romance_step(svc, game_id, st, spec, i, state):
    """One action towards marriage/children through real commands."""
    d = st["pending_decision"]
    if d:
        kind = d.get("kind")
        if kind == "baby_naming":
            return svc.command(
                game_id, st["revision"], f"drv-name-{i}",
                "resolve_decision",
                {"decision_id": d["id"], "option_id": d["options"][0]["id"],
                 "inputs": {"name": spec.get("baby_name", "Robin")}})
        prefer = None
        if kind == "date_offer":
            prefer = ("accept", "yes", "date")
        elif kind == "wedding_plan":
            # cheapest venue first (all tried before ever touching cancel);
            # resolve_wedding_plan rejects unaffordable venues with 409 and
            # the loop continues to the next cheapest
            venues = [o for o in d["options"] if o["id"] != "cancel"]
            venues.sort(key=lambda o: (o.get("known_costs") or [{}])[0].get(
                "amount", 10 ** 9))
            ordered = venues + [o for o in d["options"]
                                if o["id"] == "cancel"]
            return _resolve_ordered(svc, game_id, st, i, ordered)
        return _resolve_preferred(svc, game_id, st, i, prefer)
    fam = st["family"]
    if spec.get("married") and not fam["partner"]:
        return svc.command(game_id, st["revision"], f"drv-partner-{i}",
                           "find_partner", {})
    if spec.get("married") or spec.get("children"):
        env_partner = fam["partner"]
        if env_partner:
            status = fam.get("partner_status")
            if status in (None, 0):
                # court once per year with each relationship action, then
                # propose once; rejection/limits pass the turn naturally
                age = next(p for p in st["people"]
                           if p["id"] == st["player_id"])["age"]
                if state.get("age") != age:
                    state["age"] = age
                    state["used"] = set()
                actions = {a["id"]: a for a in st["available_actions"]}
                for act in ("conversation", "compliment", "spend_time",
                            "propose"):
                    if act in state["used"]:
                        continue
                    state["used"].add(act)
                    try:
                        if act == "propose":
                            return svc.command(
                                game_id, st["revision"],
                                f"drv-propose-{i}", "propose", {})
                        return svc.command(
                            game_id, st["revision"], f"drv-{act}-{i}",
                            act, {"target_id": env_partner})
                    except GameError as e:
                        if e.code != "not_allowed":
                            raise
                        continue
            if status == 1:
                return svc.command(game_id, st["revision"],
                                   f"drv-wed-{i}", "plan_wedding", {})
    want = spec.get("children", 0)
    baby_goal = max(want, 1 if spec.get("pregnant") else 0)
    if baby_goal and len(fam["children"]) < baby_goal \
            and not _pregnancy_in_progress(svc, game_id):
        try:
            return svc.command(game_id, st["revision"],
                               f"drv-baby-{i}", "try_for_baby", {})
        except GameError as e:
            if e.code not in ("not_allowed", "refused"):
                raise
    return svc.command(game_id, st["revision"], f"drv-age-{i}",
                       "age_up", {})


def _resolve_ordered(svc, game_id, st, i, ordered_options):
    d = st["pending_decision"]
    last_err = None
    for opt in ordered_options:
        try:
            return svc.command(game_id, st["revision"],
                               f"drv-dec-{i}", "resolve_decision",
                               {"decision_id": d["id"],
                                "option_id": opt["id"]})
        except GameError as e:
            if e.code != "not_allowed":
                raise
            last_err = e
    raise last_err


def _resolve_preferred(svc, game_id, st, i, prefer_substrs=None):
    d = st["pending_decision"]
    options = list(d["options"])
    if prefer_substrs:
        options.sort(key=lambda o: 0 if any(
            k in o["id"].lower() for k in prefer_substrs) else 1)
    last_err = None
    for opt in options:
        try:
            return svc.command(game_id, st["revision"],
                               f"drv-dec-{i}", "resolve_decision",
                               {"decision_id": d["id"],
                                "option_id": opt["id"]})
        except GameError as e:
            if e.code != "not_allowed":
                raise
            last_err = e
    raise last_err


def drive_scenario(svc, game_id, scenario_id):
    """Drive a freshly created game to the scenario state via real
    commands. Runs in memory for speed and persists once at the end."""
    spec = SCENARIOS[scenario_id]
    target = spec["target_age"]
    romance_state = {}
    sess = Session(svc, game_id)
    for i in range(300):  # loop guard
        st = sess.state()
        if st["status"] == "dead":
            raise GameError("internal_error",
                            "scenario setup failed: the life ended early",
                            500)
        age = next(p for p in st["people"]
                   if p["id"] == st["player_id"])["age"]
        employed = st["work"]["employed"]
        goals_pending = (spec.get("married") or spec.get("children")
                         or spec.get("pregnant"))
        if goals_pending and age >= 18:
            if spec["employ"] and not employed:
                if st["pending_decision"]:
                    sess.st = _resolve_first_sess(sess, i)
                    continue
                sess.command(f"drv-job-{i}", "find_job")
                continue
            if not _goals_met_sess(sess, spec):
                _drive_romance_step_sess(sess, spec, i, romance_state)
                continue
            elif age < target:
                sess.st = _resolve_first_sess(sess, i) \
                    if st["pending_decision"] else \
                    sess.command(f"drv-age-{i}", "age_up")
                continue
        if age >= target and (not spec["employ"] or employed):
            if spec.get("stop_at_pending"):
                if st["pending_decision"]:
                    _check_university_pending(st)
                    sess.save()
                    return sess.state()
                raise GameError(
                    "scenario_precondition",
                    "scenario setup could not reach university acceptance "
                    "with this seed (smarts gate)", 409)
            while st["pending_decision"]:
                sess.st = _resolve_first_sess(sess, i)
                st = sess.state()
            if spec["employ"] and not st["work"]["employed"]:
                continue  # a final pending may have ended the job
            st = _apply_setup_hook_sess(sess, spec, i)
            sess.save()
            return st
        if st["pending_decision"]:
            prefer = None
            d0 = st["pending_decision"]
            if d0.get("kind") == "university_application" \
                    and not spec.get("accept_university"):
                # scenarios target working/family life: decline enrollment
                prefer = ("decline", "no", "not", "skip")
            sess.st = _resolve_first_sess(sess, i, prefer)
            continue
        if spec["employ"] and age >= 18 and not employed:
            sess.command(f"drv-job-{i}", "find_job")
            continue
        sess.command(f"drv-age-{i}", "age_up")
    raise GameError("internal_error",
                    "scenario setup failed: progress guard exhausted", 500)


def _check_university_pending(st):
    kind = st["pending_decision"].get("kind")
    if kind not in ("university_application", "university_funding"):
        raise GameError(
            "scenario_precondition",
            f"scenario setup expected a university pending decision "
            f"at 17, got {kind!r}", 409)


def _apply_setup_hook(svc, game_id, st, spec, i):
    """Legitimate model-API setup that has no headless transition yet
    (documented per scenario)."""
    illnesses = spec.get("illnesses")
    adjustment = spec.get("dev_adjustment")
    if illnesses or adjustment:
        env = svc._load(game_id)
        player = env["player"]
        if illnesses:
            from src.lifesim_lib.lifesim_lib import TranslateMarker
            for name in illnesses:
                player.add_illness(TranslateMarker(name))
        if adjustment == "performance":
            # DEV-ONLY test hook (approved by Claude): no legitimate
            # transition forces sub-15 performance at a year boundary
            player.performance = 10
        svc._save(env)
        st = svc.get_state(game_id)
    return st


# options that keep the player employable/employed come first when a
# scenario requires employment at the target age
_KEEP_PREFERENCE = ("keep", "walk", "stay", "no", "refuse", "decline",
                    "not", "skip")


def _resolve_first(svc, game_id, st, i, prefer_keep=False,
                   prefer_substrs=None):
    d = st["pending_decision"]
    options = list(d["options"])
    if prefer_substrs:
        options.sort(key=lambda o: 0 if any(
            k in o["id"].lower() for k in prefer_substrs) else 1)
    elif prefer_keep:
        options.sort(key=lambda o: 0 if any(
            k in o["id"].lower() for k in _KEEP_PREFERENCE) else 1)
    last_err = None
    for opt in options:
        try:
            return svc.command(game_id, st["revision"],
                               f"drv-dec-{i}", "resolve_decision",
                               {"decision_id": d["id"],
                                "option_id": opt["id"]})
        except GameError as e:
            if e.code != "not_allowed":
                raise
            last_err = e
    raise last_err
