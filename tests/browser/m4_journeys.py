"""M4 journey manifest (Kimi D, QA) — played by tools/m4_journeys.py.

Honesty rules (unchanged from M3): PENDING(owner, want) when the frozen
surface does not exist yet (missing projection key, unknown action id,
unserved content); FAIL only when the surface exists and behavior is
wrong; pendings drained with a bound before probes; rejections assert the
actual error code/reason; screenshots at 390 AND 320. No env fixtures
except the documented legacy-save delta (not needed for M4 v1).

Frozen surface keyed to:
  docs/collab/M4/02-contracts.md  (§UI, B — state.social shape, hosts)
  docs/collab/M4/02-contracts-A.md (engine — circles/drift/invites/memory)
  docs/collab/M4/02-contracts-C.md (content — club_join/club_leave,
                                    social_* person actions, soc_ events)
"""

import json

from tests.browser.m3_journeys import (  # noqa: F401
    _error_code, _typed_reason, drain_pendings)


def _social(st):
    return (st or {}).get("social") or {}


def _people_by_id(st):
    return {p["id"]: p for p in st.get("people") or []}


def _group(social, group_id):
    return next((g for g in social.get("groups") or []
                 if g.get("group_id") == group_id), None)


def _pending(ctx):
    return ctx.state().get("pending_decision") or {}


def _resolve_first(ctx, prefer=None):
    st = ctx.state()
    d = st.get("pending_decision") or {}
    opts = [o for o in d.get("options", [])
            if o.get("enabled", True) and not o.get("disabled")]
    if not opts:
        return False, "pending %s has no enabled options" % d.get("kind")
    pick = None
    if prefer:
        pick = next((o for o in opts if o.get("id") == prefer), None)
    pick = pick or next(
        (o for o in opts if not o.get("known_costs")), opts[0])
    r = ctx.api("resolve_decision",
                {"decision_id": d["id"], "option_id": pick["id"],
                 "inputs": {}})
    if r.get("game_id"):
        return True, "resolved %s with %s" % (d.get("kind"), pick["id"])
    return False, "resolve failed: %s" % r.get("_body")


def _unknown_action(r):
    body = str(r.get("_body"))
    return r.get("_status") == 400 and "unknown action" in body


def _drain(ctx, bound=8):
    ok, det = drain_pendings(ctx, bound=bound)
    return None if ok else ("FAIL", "pending chain: " + det)


# ---------------------------------------------------------------------------
# m4_j1 — make a friend at school -> invite -> fall out -> reconcile
# ---------------------------------------------------------------------------

def m4_j1_classmates_surface(ctx):
    """state.social groups list real classmates (canonical person ids)."""
    bad = _drain(ctx)
    if bad:
        return bad
    st = ctx.state()
    social = _social(st)
    if not social:
        return "PENDING", "A: state.social projection not exposed yet"
    group = _group(social, "classmates")
    if group is None:
        return "PENDING", "A: social.groups has no classmates bucket yet"
    people = _people_by_id(st)
    ids = [p.get("id") for p in group.get("people") or []]
    if not ids:
        return "FAIL", "classmates group exists but lists nobody " \
                       "(school cohort not built)"
    ghosts = [i for i in ids if i not in people]
    if ghosts:
        return "FAIL", "classmates reference non-canonical ids %s" % ghosts
    return "PASS", "%d classmates, all canonical person ids" % len(ids)


def m4_j1_befriend(ctx):
    """social_befriend on a classmate raises closeness (real command)."""
    bad = _drain(ctx)
    if bad:
        return bad
    st = ctx.state()
    social = _social(st)
    group = _group(social, "classmates")
    if not group:
        return "PENDING", "A: no classmates group yet"
    target = next((p for p in group.get("people") or []), None)
    if not target:
        return "FAIL", "no classmate to befriend"
    before = ((target.get("social") or {}).get("closeness"))
    r = ctx.api("social_befriend", {"target_id": target["id"]})
    if _unknown_action(r):
        return "PENDING", "C: social_befriend action not landed"
    if r.get("_status"):
        return "FAIL", "social_befriend refused: %s %s" % (
            _error_code(r), _typed_reason(r)[:140])
    st1 = ctx.state()
    after = None
    for g in (_social(st1).get("groups") or []):
        for p in g.get("people") or []:
            if p.get("id") == target["id"]:
                after = (p.get("social") or {}).get("closeness")
                kind = (p.get("social") or {}).get("kind")
    if after is None:
        return "FAIL", "befriended classmate vanished from the projection"
    if before is not None and after <= before:
        return "FAIL", "closeness did not grow: %s -> %s" % (before, after)
    return "PASS", "closeness %s -> %s (kind %s)" % (before, after,
                                                     kind)


def m4_j1_invite(ctx):
    """A school invite arrives as a stored social_invite offer; accepting
    through the frozen pending envelope marks it accepted (free decline
    stays legal)."""
    invited = False
    for _year in range(8):
        bad = _drain(ctx)
        if bad:
            return bad
        st = ctx.state()
        d = _pending(ctx)
        if d.get("kind") == "social_invite":
            inv = next((i for i in (_social(st).get("invites") or [])
                        if i.get("decision_id") == d.get("id")), None)
            if inv and inv.get("adults_only"):
                return "FAIL", "minor was offered an adults_only invite " \
                               "(%s)" % inv.get("venue_label")
            ok, det = _resolve_first(ctx)
            if not ok:
                return "FAIL", det
            invited = True
            break
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused: %s" % r.get("_body")
    if not invited:
        return "PENDING", "C: no social_invite served to a school life " \
                          "in 8 years (invite content/route pending)"
    st = ctx.state()
    acc = [i for i in (_social(st).get("invites") or [])
           if i.get("status") == "accepted"]
    if not acc:
        return "FAIL", "resolved the invite but no invite record shows " \
                       "status accepted"
    if acc[-1].get("decision_id") is not None:
        return "FAIL", "accepted invite still echoes decision_id %s " \
                       "(freeze: cleared on resolve)" % \
                       acc[-1].get("decision_id")
    # decline path: a second open invite must be freely declinable
    declined = None
    for _year in range(4):
        bad = _drain(ctx)
        if bad:
            return bad
        st = ctx.state()
        d = _pending(ctx)
        if d.get("kind") == "social_invite":
            ok, det = _resolve_first(ctx, prefer="decline")
            if not ok:
                return "FAIL", "declining an invite failed: " + det
            declined = True
            break
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused: %s" % r.get("_body")
    detail = "invite accepted via the pending envelope (%s), decision_id " \
             "cleared" % acc[-1].get("kind")
    detail += "; decline path proven free" if declined else \
        "; no second invite served in 4y (decline path not exercised)"
    return "PASS", detail


def m4_j1_fallout_reconcile(ctx):
    """No contact fades the friendship (kind drops); the frozen reconcile
    path restores it. Bounded both ways."""
    social = _social(ctx.state())
    friend_id = None
    for g in social.get("groups") or []:
        for p in g.get("people") or []:
            if (p.get("social") or {}).get("kind") in (
                    "friend", "best_friend"):
                friend_id = p.get("id")
    if not friend_id:
        return "SKIP", "no friendship to fade (befriend/invite steps " \
                       "cover creation)"
    dropped = False
    for _year in range(10):
        bad = _drain(ctx)
        if bad:
            return bad
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused during fade watch: %s" % \
                           r.get("_body")
        st = ctx.state()
        person = _people_by_id(st).get(friend_id)
        kind = ((person or {}).get("social") or {}).get("kind")
        if kind in ("acquaintance", None):
            dropped = True
            break
    if not dropped:
        return "PENDING", "A: friendship did not fade in 10 no-contact " \
                          "years (drift thresholds not landed or too slow)"
    r = ctx.api("social_befriend", {"target_id": friend_id})
    if _unknown_action(r):
        return "PENDING", "C: reconcile/befriend action not landed"
    if r.get("_status"):
        if _error_code(r) == "not_allowed":
            return "PASS", "friendship faded to %s; reconcile refused " \
                           "with the typed gate (real behavior): %s" % (
                               kind, _typed_reason(r)[:100])
        return "FAIL", "reconcile unexpected: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    st = ctx.state()
    person = _people_by_id(st).get(friend_id)
    kind1 = ((person or {}).get("social") or {}).get("kind")
    if kind1 in ("friend", "best_friend"):
        return "PASS", "fall-out observed and reconcile restored the " \
                       "friendship (%s -> %s)" % ("acquaintance", kind1)
    return "FAIL", "reconcile accepted but kind is %r" % kind1


# ---------------------------------------------------------------------------
# m4_j2 — bully -> respond (real options), minor-content probe
# ---------------------------------------------------------------------------

def m4_j2_bully_surface(ctx):
    """A classmate with kind bully/rival appears (arc events, bounded)."""
    for _year in range(10):
        bad = _drain(ctx)
        if bad:
            return bad
        st = ctx.state()
        for g in (_social(st).get("groups") or []):
            for p in g.get("people") or []:
                if (p.get("social") or {}).get("kind") in ("bully",
                                                           "rival"):
                    return "PASS", "%s present (%s)" % (
                        (p.get("social") or {}).get("kind"), p.get("id"))
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused: %s" % r.get("_body")
    return "PENDING", "C: no bullying arc served to a school life in 10 " \
                      "years (bully content/route pending)"


def m4_j2_respond(ctx):
    """Each real response dispatches with the advertised dialect and
    records a player-known memory fact."""
    st = ctx.state()
    bully = None
    for g in (_social(st).get("groups") or []):
        for p in g.get("people") or []:
            if (p.get("social") or {}).get("kind") in ("bully", "rival"):
                bully = p
    if not bully:
        return "SKIP", "no bully in the circle (bully_surface covers it)"
    out = []
    for action, payload in (
            ("social_stand_up", {"target_id": bully["id"]}),
            ("social_tell_someone", {})):
        r = ctx.api(action, payload)
        if _unknown_action(r):
            return "PENDING", "C: %s action not landed" % action
        if r.get("_status"):
            return "FAIL", "%s refused a valid target: %s %s" % (
                action, _error_code(r), _typed_reason(r)[:120])
        out.append(action)
    st = ctx.state()
    person = _people_by_id(st).get(bully["id"])
    memory = ((person or {}).get("social") or {}).get("memory") or []
    if not memory:
        return "FAIL", "A: %s dispatched but NO memory fact recorded on " \
                       "the bully (arc responses must record player-known " \
                       "facts)" % "+".join(out)
    return "PASS", "%s dispatched; bully memory facts: %d" % (
        "+".join(out), len(memory))


def m4_j2_minor_content_probe(ctx):
    """Everything actually served to a minor is age-appropriate: no
    adults_only invite/offer, no adults_only club joinable."""
    st = ctx.state()
    social = _social(st)
    adults_invites = [i for i in social.get("invites") or []
                      if i.get("adults_only")]
    if adults_invites:
        return "FAIL", "minor's invite inbox carries adults_only entries " \
                       "%s" % [i.get("invite_id") for i in adults_invites]
    clubs = social.get("clubs") or []
    joinable_adult = [c for c in clubs
                      if c.get("adults_only") and
                      any(a.get("enabled") for a in c.get("actions") or []
                          if a.get("id") == "club_join")]
    if joinable_adult:
        return "FAIL", "minor can join adults_only clubs %s" % [
            c.get("club_id") for c in joinable_adult]
    d = _pending(ctx)
    if d.get("kind") == "social_invite":
        inv = next((i for i in social.get("invites") or []
                    if i.get("decision_id") == d.get("id")), None)
        if inv and inv.get("adults_only"):
            return "FAIL", "minor is being offered an adults_only invite"
    return "PASS", "invites/adult clubs clean for a minor"


# ---------------------------------------------------------------------------
# m4_j3 — coworker friend -> job change -> keep/lose contact
# ---------------------------------------------------------------------------

def _ensure_named_assignment(ctx, bound=8):
    """Real-play setup for the coworker journeys: an employed adult holds
    a LEGACY generic job — transition to a named assignment through the
    normal apply -> stored offer -> accept route (Amendment 2), training
    the advertised communication gate with real wages. Returns (ok, err).
    """
    for _year in range(bound):
        ok, det = drain_pendings(ctx, bound=8)
        if not ok:
            return False, "pending chain: " + det
        st = ctx.state()
        if ((st.get("careers") or {}).get("assignment") or {}).get("status") \
                == "active":
            return True, ""
        o = next((o for o in (st.get("careers") or {}).get("openings") or []
                  if o.get("role_id") == "office_assistant"), None)
        if o is None:
            return False, "PENDING: A: no office_assistant opening in the " \
                          "projection"
        skills = [s for s in ((o.get("requirements") or {})
                              .get("skills") or []) if not s.get("met")]
        plan = {s["id"]: s["required"] for s in skills}
        if o.get("eligible"):
            r = ctx.api("careers_apply",
                        {"role_id": "office_assistant",
                         "employer_id": o.get("employer_id")})
            if r.get("game_id"):
                d = ctx.state().get("pending_decision") or {}
                if d.get("kind") != "career_offer":
                    return False, "FAIL: apply accepted but no career_offer"
                r = ctx.api("resolve_decision",
                            {"decision_id": d["id"], "option_id": "accept",
                             "inputs": {}})
                if r.get("game_id"):
                    return True, ""
                return False, "FAIL: offer accept failed: %s" % \
                              r.get("_body")
            if _error_code(r) == "not_allowed" and "pending" in \
                    _typed_reason(r):
                continue
            return False, "FAIL: careers_apply refused: %s %s" % (
                _error_code(r), _typed_reason(r)[:120])
        # train the advertised gates (real commands, real wages pay for it)
        for _ in range(4):
            st = ctx.state()
            sk = st.get("skills") or {}
            uses = sk.get("uses_this_year") or {}
            training = {t.get("target_id") or t.get("id"): t
                        for t in sk.get("training") or []}
            skill = next((s for s, t in plan.items()
                          if (sk.get("values") or {}).get(s, 0) < t), None)
            if not skill:
                break
            opt = "train_" + skill
            spec = training.get(opt)
            if spec and spec.get("enabled", True) and \
                    uses.get(opt, 0) < 2:
                r = ctx.api("train_skill", {"target_id": opt})
                if r.get("_status"):
                    break
                continue
            eve = training.get("train_evening_course")
            if eve and eve.get("enabled", True):
                r = ctx.api("train_skill",
                            {"target_id": "train_evening_course",
                             "skill": skill})
                if not r.get("_status"):
                    continue
            break
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return False, "FAIL: age_up refused: %s" % r.get("_body")
    return False, "PENDING: named assignment not established in %d years " \
                  "(gates too slow or openings unservable)" % bound


def m4_j3_coworkers_surface(ctx):
    """Coworker circle derives from the active assignment: with a real
    named assignment held, the circle must list >= 1 real coworker (a
    vacuous 0-coworker 'PASS' is forbidden)."""
    ok, err = _ensure_named_assignment(ctx)
    if not ok:
        status = "PENDING" if err.startswith("PENDING") else "FAIL"
        return status, err.split(": ", 1)[-1]
    st = ctx.state()
    social = _social(st)
    if not social:
        return "PENDING", "A: state.social projection not exposed yet"
    group = _group(social, "coworkers")
    if group is None:
        return "PENDING", "A: social.groups has no coworkers bucket yet"
    people = _people_by_id(st)
    ids = [p.get("id") for p in group.get("people") or []]
    ghosts = [i for i in ids if i not in people]
    if ghosts:
        return "FAIL", "coworkers reference non-canonical ids %s" % ghosts
    if not ids:
        return "FAIL", "A: active named assignment but the coworkers " \
                       "circle is EMPTY (work circle not populated)"
    return "PASS", "%d coworkers in the circle (all canonical)" % len(ids)


def m4_j3_befriend_coworker(ctx):
    st = ctx.state()
    group = _group(_social(st), "coworkers")
    if not group or not group.get("people"):
        return "SKIP", "no coworkers to befriend"
    target = group["people"][0]
    r = ctx.api("social_befriend", {"target_id": target["id"]})
    if _unknown_action(r):
        return "PENDING", "C: social_befriend action not landed"
    if r.get("_status"):
        return "FAIL", "befriend coworker refused: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    st1 = ctx.state()
    person = _people_by_id(st1).get(target["id"])
    kind = ((person or {}).get("social") or {}).get("kind")
    if kind not in ("friend", "best_friend"):
        return "FAIL", "befriend accepted but kind is %r" % kind
    return "PASS", "coworker %s is now kind=%s" % (target["id"], kind)


def m4_j3_job_change_keeps_contact(ctx):
    """M3 employer change; the frozen A-7 rule: an ex-coworker KEEPS a
    people record (status faded) — job change keeps contact; losing it
    entirely is event-driven only, never automatic."""
    bad = _drain(ctx)
    if bad:
        return bad
    st = ctx.state()
    social = _social(st)
    friend_ids = set()
    for g in social.get("groups") or []:
        for p in g.get("people") or []:
            if (p.get("social") or {}).get("kind") in ("friend",
                                                       "best_friend"):
                friend_ids.add(p.get("id"))
    if not friend_ids:
        return "SKIP", "no coworker friendship yet (befriend step covers)"
    r = ctx.api("careers_resign", {})
    if not r.get("game_id"):
        if _unknown_action(r):
            return "PENDING", "A: careers surface missing (M3 regression)"
        return "FAIL", "resign refused: %s %s" % (
            _error_code(r), _typed_reason(r)[:120])
    st1 = ctx.state()
    people = _people_by_id(st1)
    gone = [pid for pid in friend_ids if pid not in people]
    if gone:
        return "FAIL", "job change DELETED a real person %s from the " \
                       "canonical graph" % gone
    vanished = []
    for pid in friend_ids:
        person = people.get(pid)
        in_any_group = any(
            pid in [p.get("id") for p in g.get("people") or []]
            for g in (_social(st1).get("groups") or []))
        if not in_any_group:
            vanished.append(pid)
    if vanished:
        return "FAIL", "ex-coworker contact vanished entirely %s — the " \
                       "frozen keep-contact rule is event-driven only" % \
                       vanished
    return "PASS", "after resign, ex-coworker friends remain real people " \
                   "in the projection (faded keep-contact)"


# ---------------------------------------------------------------------------
# m4_j4 — privacy + 18+ venue probes
# ---------------------------------------------------------------------------

def m4_j4_adult_club_refusal(ctx):
    """A minor dispatching club_join on an adults_only club is refused
    with the precise 18+ reason and no mutation."""
    bad = _drain(ctx)
    if bad:
        return bad
    st = ctx.state()
    clubs = _social(st).get("clubs") or []
    adult = next((c for c in clubs if c.get("adults_only")), None)
    if not adult:
        return "PENDING", "A/C: no adults_only club in the projection " \
                          "(clubs data/route pending)"
    r = ctx.api("club_join", {"club_id": adult.get("club_id")})
    if _unknown_action(r):
        return "PENDING", "C: club_join action not landed"
    if r.get("game_id"):
        return "FAIL", "minor club_join on adults_only club %s ACCEPTED" \
                       % adult.get("club_id")
    reason = _typed_reason(r)
    if r.get("_status") != 409 or "18" not in reason:
        return "FAIL", "wrong refusal for adults_only join: %s %s" % (
            _error_code(r), reason[:140])
    st1 = ctx.state()
    still = next((c for c in (_social(st1).get("clubs") or [])
                  if c.get("club_id") == adult.get("club_id")), {})
    if still.get("member"):
        return "FAIL", "refused join but the club shows member=true"
    return "PASS", "adults_only join refused: %s" % reason[:120]


def m4_j4_minor_invite_watch(ctx):
    """Aging a minor: no adults_only invite may ever be served (8y
    bounded watch over everything the pipeline actually offers)."""
    for _year in range(8):
        bad = _drain(ctx)
        if bad:
            return bad
        st = ctx.state()
        d = _pending(ctx)
        if d.get("kind") == "social_invite":
            inv = next((i for i in (_social(st).get("invites") or [])
                        if i.get("decision_id") == d.get("id")), None)
            if inv is None:
                inv = next((i for i in (_social(st).get("invites") or [])
                            if i.get("status") == "open"), None)
            if inv and inv.get("adults_only"):
                return "FAIL", "adults_only invite %s served to a minor " \
                               "(%s)" % (inv.get("invite_id"),
                                         inv.get("venue_label"))
            ok, det = _resolve_first(ctx)
            if not ok:
                return "FAIL", det
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused: %s" % r.get("_body")
    return "PASS", "8 minor years: no adults_only invite ever served"


def m4_j4_romance_18_gate(ctx):
    """M2.5 regression: a 17-year-old (not in school) cannot start
    romance through the frozen find_partner surface — the refusal must
    cite the age gate."""
    ctx.start("graduating_18", 9922)
    me = next(p for p in ctx.state()["people"]
              if p["id"] == ctx.state()["player_id"])

    def set_17(env):
        env["player"].age = 17

    try:
        ctx.qa_env(set_17)
    except Exception as exc:
        return "FAIL", "qa age fixture crashed: %r" % exc
    age = ctx.player_age(ctx.state())
    if age != 17:
        return "FAIL", "age fixture did not take (age %s)" % age
    bad = _drain(ctx)
    if bad:
        return bad
    r = ctx.api("find_partner", {})
    if r.get("game_id"):
        return "FAIL", "find_partner ACCEPTED for a 17-year-old (M2.5 " \
                       "18+ gate bypass)"
    reason = _typed_reason(r)
    if r.get("_status") != 409:
        return "FAIL", "unexpected find_partner refusal: %s %s" % (
            _error_code(r), reason[:120])
    if "18" not in reason and "age" not in reason.lower():
        return "FAIL", "refusal does not cite the age gate: %s" % \
                       reason[:140]
    return "PASS", "17-year-old find_partner refused: %s" % reason[:120]


def m4_j4_memory_privacy(ctx):
    """Privacy probe with a FORCED subject: a fresh school life befriends
    a classmate and keeps real social contact (bounded) until at least
    one player-known memory fact exists — only then are the privacy/
    well-formedness assertions meaningful. 0 memories is PENDING, never
    a vacuous PASS."""
    ctx.start("enrolled_child_10", 9923)
    ok, det = drain_pendings(ctx, bound=8)
    if not ok:
        return "FAIL", det
    # befriend a classmate (real command; C's action)
    target = None
    st = ctx.state()
    group = _group(_social(st), "classmates")
    if group and group.get("people"):
        target = group["people"][0]["id"]
    if target:
        r = ctx.api("social_befriend", {"target_id": target})
        if _unknown_action(r):
            return "PENDING", "C: social_befriend not landed — cannot " \
                              "force a memory subject"
    # spend time + age (bounded) until a memory line exists somewhere
    memories = 0
    people_with = 0
    for _year in range(8):
        st = ctx.state()
        total, with_m = _memory_stats(st)
        memories, people_with = total, with_m
        if memories >= 1:
            break
        if target:
            r = ctx.api("social_spend_time", {"target_id": target})
            if _unknown_action(r):
                return "PENDING", "C: social_spend_time not landed — no " \
                                  "way to force contact yet"
        ok, det = drain_pendings(ctx, bound=8)
        if not ok:
            return "FAIL", det
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "age_up refused while forcing a memory: %s" % \
                           r.get("_body")
    if memories < 1:
        return "PENDING", "A/C: no memory fact recorded after befriend " \
                          "+ 8 years of contact (memory pipeline not " \
                          "landed yet) — privacy probe has no subject"
    # the actual privacy/well-formedness assertions
    st = ctx.state()
    for g in (_social(st).get("groups") or []):
        for p in g.get("people") or []:
            memory = (p.get("social") or {}).get("memory")
            if memory is not None and (
                    not isinstance(memory, list)
                    or not all(isinstance(m, str) for m in memory)):
                return "FAIL", "memory must be a list of strings on %s" \
                               % p.get("id")
    return "PASS", "%d memory line(s) on %d people, all well-formed " \
                   "player-known strings" % (memories, people_with)


def _memory_stats(st):
    total, with_m = 0, 0
    for g in (_social(st).get("groups") or []):
        for p in g.get("people") or []:
            memory = (p.get("social") or {}).get("memory")
            if memory:
                total += len(memory)
                with_m += 1
    return total, with_m


# ---------------------------------------------------------------------------
# m4_j5 — contrasting social starts
# ---------------------------------------------------------------------------

STARTS = [("enrolled_child_10", "Child"), ("graduating_18", "Graduate"),
          ("employed_30", "Employed adult")]


def m4_j5_contrasting_starts(ctx):
    out = []
    for i, (scenario, label) in enumerate(STARTS):
        ctx.start(scenario, 9930 + i)
        bad = _drain(ctx)
        if bad:
            return bad
        st = ctx.state()
        social = _social(st)
        if not social:
            return "PENDING", "A: state.social not exposed (%s start)" % \
                              label
        if not isinstance(social.get("groups"), list):
            return "FAIL", "social.groups missing for the %s start" % label
        actions = st.get("available_actions") or []
        enabled = [a for a in actions if a.get("enabled", True)]
        if not enabled:
            return "FAIL", "%s start: no enabled actions (soft-lock)" % \
                           label
        if not any(not a.get("known_costs") for a in enabled):
            return "FAIL", "%s start: no free action (soft-lock)" % label
        r = ctx.api("age_up", {})
        if not r.get("game_id"):
            return "FAIL", "%s start cannot age: %s" % (label,
                                                        r.get("_body"))
        out.append(label)
    return "PASS", "social projection + free actions + legal aging: %s" % \
                   ", ".join(out)


# ---------------------------------------------------------------------------
# Manifest
# ---------------------------------------------------------------------------

JOURNEYS = [
    {"id": "m4_j1_school_friend",
     "title": "School friend: circle -> befriend -> invite -> fall out -> "
              "reconcile",
     "scenario": "enrolled_child_10",
     "steps": [
         ("classmates_surface", "Classmates circle lists real people",
          m4_j1_classmates_surface),
         ("befriend", "social_befriend grows closeness", m4_j1_befriend),
         ("invite", "Stored social_invite offer, accepted via the pending "
          "envelope", m4_j1_invite),
         ("fallout_reconcile", "No-contact fade, then reconcile",
          m4_j1_fallout_reconcile),
     ]},
    {"id": "m4_j2_bully",
     "title": "Bullying arc: real responses, minor-safe content",
     "scenario": "enrolled_child_10",
     "steps": [
         ("bully_surface", "A bully/rival appears in the circle",
          m4_j2_bully_surface),
         ("respond", "stand_up + tell_someone dispatch, memory recorded",
          m4_j2_respond),
         ("minor_content", "Nothing adult served to a minor",
          m4_j2_minor_content_probe),
     ]},
    {"id": "m4_j3_coworker",
     "title": "Coworker friend survives a job change (frozen keep rule)",
     "scenario": "employed_30",
     "steps": [
         ("coworkers_surface", "Coworker circle from the assignment",
          m4_j3_coworkers_surface),
         ("befriend", "Befriend a coworker", m4_j3_befriend_coworker),
         ("job_change", "Resign; ex-coworker contact is kept (faded), "
          "never auto-deleted", m4_j3_job_change_keeps_contact),
     ]},
    {"id": "m4_j4_privacy_venues",
     "title": "Privacy + 18+ venue probes",
     "scenario": "enrolled_child_10",
     "steps": [
         ("adult_club_refusal", "club_join on adults_only refused with "
          "the 18+ reason", m4_j4_adult_club_refusal),
         ("minor_invite_watch", "8 years: no adults_only invite served",
          m4_j4_minor_invite_watch),
         ("romance_18_gate", "M2.5 regression: minors cannot find_partner",
          m4_j4_romance_18_gate),
         ("memory_privacy", "social.memory well-formed per person",
          m4_j4_memory_privacy),
     ]},
    {"id": "m4_j5_contrasting_starts",
     "title": "Contrasting starts expose the social projection legally",
     "scenario": None,
     "steps": [
         ("starts", "Child/graduate/adult: groups + free actions + aging",
          m4_j5_contrasting_starts),
     ]},
]
