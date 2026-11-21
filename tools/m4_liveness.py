"""M4 social liveness sweep (Kimi D, QA).

Real play of 20 seeded lives with NO browser: each life is born (age 0),
plays school 0->18 resolving every pending with the FIRST FREE enabled
option (the user's play rule), then takes a job (M3 apply -> offer ->
accept, training the advertised gates with real commands) and plays to
25.

Per life it reports: friends by 18, closeness distribution at 25,
soc_ events served (by pending event_id), invites (by status, with
adults_only count), bullying arcs seen, popularity band changes,
coworkers at 25, and NPC surname collisions with the player's family.

FAIL thresholds (per life, any breach fails the life and the command):
  * >= 1 friend by age 18
  * >= 3 soc_ events served across the life
  * popularity NOT stuck (band or score must move at least once)
  * 0 surname collisions between non-relative social NPCs and the
    player's family

Evidence: docs/collab/evidence/M4/liveness.{md,json}
QA rules: TMPDIR=~/.lifesim-tmp, save dir deleted after the run.
"""

import json
import os
import re
import sys
import tempfile
import threading
import time
import urllib.error
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

TMP_HOME = os.path.expanduser("~/.lifesim-tmp")
os.makedirs(TMP_HOME, exist_ok=True)
os.environ["TMPDIR"] = TMP_HOME

import web.server as server  # noqa: E402

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "M4")
N_LIVES = 20
SEED_BASE = 41000
SCHOOL_END = 18
PLAY_END = 25
FREE_PREFERENCE = re.compile(
    r"decline|keep|stay|not now|no\b|skip|pass", re.I)


def _free_calm_option(options):
    opts = [o for o in options
            if o.get("enabled", True) and not o.get("disabled")]
    if not opts:
        return None
    free = [o for o in opts if not o.get("known_costs")]
    pool = free or opts
    calm = [o for o in pool if FREE_PREFERENCE.search(
        str(o.get("label", "")) + " " + str(o.get("id", "")))]
    pool = calm or pool
    return min(pool, key=lambda o: len(o.get("known_effects") or []))


class LifePlayer:
    def __init__(self, base, game_id, seed):
        self.base = base
        self.game_id = game_id
        self.seed = seed
        self.req = 0
        self.soc_events = []       # event ids served (accepted path only
                                   # counts when resolved; served = seen)
        self.invites_seen = 0
        self.invites_adults = 0
        self.bully_ids = set()
        self.band_history = []
        self.friends_at_18 = None

    def state(self):
        with urllib.request.urlopen(
                self.base + "/api/games/" + self.game_id) as resp:
            return json.loads(resp.read())

    def api(self, action, payload):
        st = self.state()
        self.req += 1
        req = urllib.request.Request(
            self.base + "/api/games/" + self.game_id + "/actions",
            data=json.dumps({
                "expected_revision": st["revision"],
                "request_id": "m4l-%s-%d" % (self.seed, self.req),
                "action": action, "payload": payload}).encode(),
            headers={"Content-Type": "application/json"})
        try:
            return json.loads(urllib.request.urlopen(req).read())
        except urllib.error.HTTPError as e:
            return {"_status": e.code, "_body": e.read()[:300]}

    def _observe(self, st):
        """Count what the pipeline actually served this read."""
        d = st.get("pending_decision") or {}
        eid = d.get("event_id") or ""
        if eid.startswith("soc_"):
            self.soc_events.append(eid)
        social = st.get("social") or {}
        for inv in social.get("invites") or []:
            pass  # invites counted from the final state + pending echo
        pop = social.get("popularity") or {}
        band = pop.get("band")
        if band is not None and (not self.band_history
                                 or self.band_history[-1] != band):
            self.band_history.append(band)
        for g in social.get("groups") or []:
            for p in g.get("people") or []:
                if (p.get("social") or {}).get("kind") in ("bully",
                                                           "rival"):
                    self.bully_ids.add(p.get("id"))

    def resolve_pending(self):
        st = self.state()
        d = st.get("pending_decision")
        if not d:
            return True
        if d.get("kind") == "social_invite":
            self.invites_seen += 1
            inv = next((i for i in (st.get("social") or {}).get("invites")
                        or [] if i.get("decision_id") == d.get("id")), None)
            if inv is None:
                inv = next((i for i in (st.get("social") or {})
                            .get("invites") or []
                            if i.get("status") == "open"), None)
            if inv and inv.get("adults_only"):
                self.invites_adults += 1
        pick = _free_calm_option(d.get("options") or [])
        if pick is None:
            return False
        r = self.api("resolve_decision",
                     {"decision_id": d["id"], "option_id": pick["id"],
                      "inputs": {}})
        return bool(r.get("game_id"))

    def drain(self, bound=10):
        for _ in range(bound):
            if not self.resolve_pending():
                return False
            if not (self.state().get("pending_decision")):
                return True
        return not self.state().get("pending_decision")

    def age_once(self):
        if not self.drain():
            return False, "pending chain stuck"
        r = self.api("age_up", {})
        if not r.get("game_id"):
            return False, "age_up refused: %s" % r.get("_body")
        self._observe(self.state())
        return True, ""

    def player_age(self, st):
        return next(p["age"] for p in st["people"]
                    if p["id"] == st["player_id"])

    def friends_count(self, st):
        n = 0
        for g in (st.get("social") or {}).get("groups") or []:
            for p in g.get("people") or []:
                if (p.get("social") or {}).get("kind") in ("friend",
                                                           "best_friend"):
                    n += 1
        return n

    def get_job(self, bound=7):
        """M3 route: apply -> stored offer -> accept. Fresh graduates
        have ~$0, so prefer openings whose skill gates are FREE-trainable
        (apprentice/care assistant); wages then fund anything later."""
        for _ in range(bound):
            if not self.drain():
                return False
            st = self.state()
            if ((st.get("careers") or {}).get("assignment") or {}).get(
                    "status") == "active":
                return True
            openings = (st.get("careers") or {}).get("openings") or []
            eligible = next((o for o in openings if o.get("eligible")),
                            None)
            if eligible:
                r = self.api("careers_apply",
                             {"role_id": eligible.get("role_id"),
                              "employer_id": eligible.get("employer_id")})
                if r.get("game_id"):
                    d = self.state().get("pending_decision") or {}
                    if d.get("kind") == "career_offer":
                        self.api("resolve_decision",
                                 {"decision_id": d["id"],
                                  "option_id": "accept", "inputs": {}})
                        continue
            # train one free-trainable unmet gate this year
            st = self.state()
            sk = st.get("skills") or {}
            training = {t.get("target_id") or t.get("id"): t
                        for t in sk.get("training") or []}
            uses = sk.get("uses_this_year") or {}
            trained = False
            for o in openings:
                unmet = [s for s in ((o.get("requirements") or {})
                                     .get("skills") or []) if not s.get("met")]
                if not unmet or any(
                        (q for q in ((o.get("requirements") or {})
                         .get("qualifications") or []) if not q.get("met"))):
                    continue
                if any(training.get("train_" + s["id"], {}).get("known_costs")
                       for s in unmet):
                    continue  # paid gate: not free-trainable
                for s in unmet:
                    opt = "train_" + s["id"]
                    spec = training.get(opt)
                    if spec and spec.get("enabled", True) and \
                            uses.get(opt, 0) < 2:
                        r = self.api("train_skill", {"target_id": opt})
                        if not r.get("_status"):
                            trained = True
                            break
                if trained:
                    break
            ok, _ = self.age_once()
            if not ok:
                return False
        return False

    def play(self):
        log = []
        # 0 -> 18 (school)
        while True:
            st = self.state()
            age = self.player_age(st)
            if age >= SCHOOL_END:
                break
            ok, err = self.age_once()
            if not ok:
                log.append("died/stuck at %d: %s" % (age, err))
                return log
        self.friends_at_18 = self.friends_count(self.state())
        # 18 -> 25 with a job
        self.get_job()
        while True:
            st = self.state()
            age = self.player_age(st)
            if age >= PLAY_END:
                break
            ok, err = self.age_once()
            if not ok:
                log.append("stuck at %d: %s" % (age, err))
                return log
        return log


def closeness_histogram(st):
    buckets = {"0-19": 0, "20-39": 0, "40-59": 0, "60-79": 0, "80-100": 0}
    for g in (st.get("social") or {}).get("groups") or []:
        for p in g.get("people") or []:
            c = (p.get("social") or {}).get("closeness")
            if c is None:
                continue
            if c < 20:
                buckets["0-19"] += 1
            elif c < 40:
                buckets["20-39"] += 1
            elif c < 60:
                buckets["40-59"] += 1
            elif c < 80:
                buckets["60-79"] += 1
            else:
                buckets["80-100"] += 1
    return buckets


def surname_collisions(st):
    """NPC surnames (social circle people) matching a family surname,
    excluding actual relatives."""
    family_ids = set()
    fam = st.get("family") or {}
    for key in ("parents", "siblings", "children", "partners",
                "grandparents"):
        for entry in fam.get(key) or []:
            if isinstance(entry, dict) and entry.get("id"):
                family_ids.add(entry["id"])
            elif isinstance(entry, str):
                family_ids.add(entry)
    people = {p["id"]: p for p in st.get("people") or []}
    player = next(p for p in st["people"] if p["id"] == st["player_id"])
    family_ids.add(player["id"])

    def surname(name):
        parts = (name or "").split()
        return parts[-1].lower() if len(parts) > 1 else None

    family_surnames = {surname(p["name"])
                       for pid, p in people.items()
                       if pid in family_ids} - {None}
    collisions = []
    seen_pids = set()
    for g in (st.get("social") or {}).get("groups") or []:
        for p in g.get("people") or []:
            pid = p.get("id")
            if not pid or pid in family_ids or pid in seen_pids:
                continue
            sn = surname(p.get("name"))
            if sn and sn in family_surnames:
                seen_pids.add(pid)
                collisions.append((p.get("name"), sorted(family_surnames)))
    return collisions


def assess(life, final_state):
    social = final_state.get("social") or {}
    invites = social.get("invites") or []
    coworkers = next(
        (len(g.get("people") or []) for g in social.get("groups") or []
         if g.get("group_id") == "coworkers"), 0)
    pop = social.get("popularity") or {}
    collisions = surname_collisions(final_state)
    employed = bool((final_state.get("careers") or {}).get("assignment"))
    row = {
        "seed": life.seed,
        "age": life.player_age(final_state),
        "friends_by_18": life.friends_at_18,
        "closeness_at_25": closeness_histogram(final_state),
        "soc_events_served": len(set(life.soc_events)),
        "invites": {"total": len(invites),
                    "accepted": sum(1 for i in invites
                                    if i.get("status") == "accepted"),
                    "adults_only_seen": life.invites_adults},
        "bullies_ever": len(life.bully_ids),
        "popularity_band_changes": max(0, len(life.band_history) - 1),
        "popularity_final": {"score": pop.get("score"),
                             "band": pop.get("band")},
        "coworkers_at_25": coworkers,
        "employed_at_25": employed,
        "surname_collisions": len(collisions),
        "collision_detail": collisions[:5],
        "job": employed,
    }
    fails = []
    if (row["friends_by_18"] or 0) < 1:
        fails.append("friends_by_18=%s (<1)" % row["friends_by_18"])
    if row["soc_events_served"] < 3:
        fails.append("soc_events=%s (<3)" % row["soc_events_served"])
    if row["popularity_band_changes"] < 1 and \
            not life.band_history:
        fails.append("popularity stuck: no band ever reported")
    elif row["popularity_band_changes"] < 1:
        fails.append("popularity stuck: band never changed (%s)"
                     % life.band_history)
    if row["surname_collisions"]:
        fails.append("surname collisions=%d" % row["surname_collisions"])
    # the sweep takes a real job (apply -> stored offer -> accept); an
    # adult still unemployed at 25 failed that step, and an employed
    # adult must share a workplace with >= 1 real coworker
    if not employed:
        fails.append("unemployed at 25 despite the real job step")
    elif coworkers < 1:
        fails.append("employed but coworkers=%s (<1)" % coworkers)
    row["status"] = "FAIL" if fails else "PASS"
    row["failures"] = fails
    return row


def _median(values):
    vals = sorted(values)
    n = len(vals)
    if not n:
        return None
    mid = n // 2
    return vals[mid] if n % 2 else (vals[mid - 1] + vals[mid]) / 2


def aggregate_checks(rows):
    """Cross-life imbalance thresholds (Claude, 2026-11-21). Returns
    list of (name, ok, detail)."""
    out = []
    n = len(rows)
    bully_lives = sum(1 for r in rows if r["bullies_ever"] >= 1)
    rate = bully_lives / n if n else 0.0
    ok = 0.15 <= rate <= 0.50
    out.append(("bullying arc rate in [15%%, 50%%]", ok,
                "%d/%d lives = %.0f%%" % (bully_lives, n, rate * 100)))
    median_friends = _median([r["friends_by_18"] or 0 for r in rows])
    ok = median_friends is not None and 2 <= median_friends <= 5
    out.append(("median friends at 18 in [2, 5]", ok,
                "median=%s" % median_friends))
    return out


def main():
    os.makedirs(EVIDENCE, exist_ok=True)
    save_dir = tempfile.mkdtemp(prefix="lifesim-m4l-", dir=TMP_HOME)
    httpd = server.make_server(0, save_dir=save_dir)
    httpd.dev_mode = True
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    base = "http://127.0.0.1:%d" % httpd.server_port
    rows = []
    try:
        for i in range(N_LIVES):
            seed = SEED_BASE + i
            req = urllib.request.Request(
                base + "/api/games",
                data=json.dumps({
                    "first_name": "Liveness", "last_name": "Probe",
                    "gender": "female" if i % 2 else "male",
                    "country": "Australia", "seed": seed,
                    "request_id": "m4l-create-%d" % seed}).encode(),
                headers={"Content-Type": "application/json"})
            gid = json.loads(urllib.request.urlopen(req).read())["game_id"]
            life = LifePlayer(base, gid, seed)
            log = life.play()
            row = assess(life, life.state())
            row["log"] = log
            rows.append(row)
            print("%s seed %d: friends@18=%s soc=%d invites=%d " 
                  "bullies=%d band_changes=%s coworkers=%s collisions=%s%s"
                  % (row["status"], seed, row["friends_by_18"],
                     row["soc_events_served"], row["invites"]["total"],
                     row["bullies_ever"], row["popularity_band_changes"],
                     row["coworkers_at_25"], row["surname_collisions"],
                     ("  <- " + "; ".join(row["failures"]))
                     if row["failures"] else ""))
    finally:
        httpd.shutdown()
        import shutil
        shutil.rmtree(save_dir, ignore_errors=True)
    counts = {}
    for r in rows:
        counts[r["status"]] = counts.get(r["status"], 0) + 1
    aggregates = aggregate_checks(rows)
    counts["aggregate_failures"] = sum(1 for _, ok, _ in aggregates
                                       if not ok)
    with open(os.path.join(EVIDENCE, "liveness.json"), "w") as f:
        json.dump({"counts": counts,
                   "aggregates": [{"check": n, "ok": ok, "detail": d}
                                  for n, ok, d in aggregates],
                   "lives": rows}, f, indent=1)
    with open(os.path.join(EVIDENCE, "liveness.md"), "w") as f:
        f.write("# M4 social liveness sweep (Kimi D QA, %s)\n\n"
                % time.strftime("%Y-%m-%d"))
        f.write("20 seeded lives, real play only: school 0->18 (first free "
                "option), a real job (apply -> stored offer -> accept), "
                "then to 25. Per-life thresholds: >=1 friend by 18, >=3 "
                "soc_ events, popularity not stuck, 0 surname collisions, "
                "employed at 25 with >= 1 coworker. Aggregate imbalance "
                "thresholds: bullying arc rate across lives in [15%, 50%], "
                "median friends at 18 in [2, 5].\n\n")
        f.write("Counts: %s\n\n" % json.dumps(counts))
        for n, ok, d in aggregates:
            f.write("- **%s** %s — %s\n" % ("PASS" if ok else "FAIL",
                                            n, d))
        f.write("\n")
        for r in rows:
            f.write("- **%s** seed %d — friends@18=%s soc=%d invites=%d "
                    "(adult %d) bullies=%d band_changes=%d employed=%s "
                    "coworkers=%s collisions=%s %s\n"
                    % (r["status"], r["seed"], r["friends_by_18"],
                       r["soc_events_served"], r["invites"]["total"],
                       r["invites"]["adults_only_seen"],
                       r["bullies_ever"], r["popularity_band_changes"],
                       r["employed_at_25"], r["coworkers_at_25"],
                       r["surname_collisions"],
                       ("FAIL: " + "; ".join(r["failures"]))
                       if r["failures"] else ""))
    print("counts: %s" % json.dumps(counts))
    for n, ok, d in aggregates:
        print("AGGREGATE %s %s — %s" % ("PASS" if ok else "FAIL", n, d))
    print("evidence: %s" % EVIDENCE)
    return 1 if any(r["status"] == "FAIL" for r in rows) or \
        counts["aggregate_failures"] else 0


if __name__ == "__main__":
    sys.exit(main())
