"""M4 social content validation (Kimi D, tooling/QA owner).

One command for all M4 content families:

    python3 tools/validate_m4_social.py            # validate content
    python3 tools/validate_m4_social.py --selftest # fixture self-test

Contract: docs/collab/M4/02-contracts.md (FROZEN v1) + owner sections
-A/-C/-D. All M4 families are FROZEN v1, so every check below is
ERROR-gating: a missing family file, a bad foreign key, an adult venue
reachable by minors, or a missing bullying response fails the command.
Warnings never gate.

Families:
  * C: src/game/content/social/clubs.json  (clubs/teams)
  * C: src/game/content/social/bullying.json (arc stage table)
  * C: src/game/content/events/m4_*.json   (v3 social events, ids soc_*)
  * A: src/game/features/social/popularity_bands.json (band data)

Checks production does not do:
  EPYTHON   no Python/eval/exec anywhere in authored strings
  EDUP/ERESKIN  duplicate ids / identical reskins (warning)
  ECLUB*    clubs schema: required keys, category vocab (C-owned
            school|sport|arts|social), adults_only consistency,
            cost_per_year bounds (Amendment 1: cost_per_year,
            not 'join'), member_cap
  ECLUBAGE  adults_only club/venue with min_age < 18 (fixed rule)
  EBULLY    bullying table: the four frozen responses exist, stage
            values resolve, tell_someone has no rng gate
  EBANDS    popularity bands: exactly the frozen 5-band vocabulary with
            contiguous monotonic bounds and labels
  EAGE      every soc_ event declares its age band (min_age/max_age)
  EINVITE   soc_inv_* records carry venue_label + adults_only (top level
            or in interaction) so A's 18+ gate has data
  EINVITEAGE adults_only invite not gated min_age >= 18
  ESOFTLOCK every event has at least one free (money) choice
  ETEXT     outcome lines >= 6 words (C content invariant)
  EREFCLUB  in_club conditions reference defined club ids
  ESPEC     >= 30 soc_ records overall (kickoff count)

Cross-owner note (WARNING, not gated): B's clubs-card comment lists the
category chip vocabulary school|sport|hobby|work while C's data schema
(02-contracts-C.md) freezes school|sport|arts|social. This validator
gates on C's data schema (C owns the file); the vocab alignment is an
owner amendment for Claude, reported in the evidence header.

--selftest runs the intentionally-invalid fixtures under
tests/content/fixtures/invalid/ (m4_* fixtures: one broken rule per
file, expected code in the first filename segment). A fixture that does
NOT fail with its expected code is itself a FAILURE.

Reports: docs/collab/evidence/M4/validate-m4-social.{md,json}
Exit non-zero if any ERROR fires or selftest fails.
"""

import json
import os
import re
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "M4")
EVENT_CONTENT_DIR = os.path.join(ROOT, "src", "game", "content", "events")
INVALID_FIXTURES = os.path.join(ROOT, "tests", "content", "fixtures",
                                "invalid")
CLUBS_PATH = os.path.join(ROOT, "src", "game", "content", "social",
                          "clubs.json")
BULLYING_PATH = os.path.join(ROOT, "src", "game", "content", "social",
                             "bullying.json")
BANDS_PATH = os.path.join(ROOT, "src", "game", "features", "social",
                          "popularity_bands.json")

PYTHONY = re.compile(r"\b(eval|exec|__import__|compile|globals|locals)\s*\(")

# C's frozen clubs.json category vocabulary (02-contracts-C.md).
CLUB_CATEGORIES = {"school", "sport", "arts", "social"}
# B's projection comment vocabulary (02-contracts.md §UI) — cross-owner
# note only, see header.
B_CATEGORY_NOTE = {"school", "sport", "hobby", "work"}

BULLY_RESPONSES = {"tell_someone", "stand_up", "avoid", "befriend"}
BULLY_STAGES = {"targeted", "escalated", "resolving", "ended"}

# A's frozen band vocabulary (02-contracts-A.md / §UI): band -> bounds.
FROZEN_BANDS = {"unknown": (0, 19), "known": (20, 39), "liked": (40, 59),
                "popular": (60, 79), "beloved": (80, 100)}

MIN_SOCIAL_EVENTS = 30
MIN_OUTCOME_WORDS = 6


class Finding:
    def __init__(self, code, where, message, fatal=True):
        self.code = code
        self.where = where
        self.message = message
        self.fatal = fatal

    def as_dict(self):
        return {"code": self.code, "where": self.where,
                "message": self.message, "fatal": self.fatal}


class _BadJson:
    def __init__(self, err):
        self.err = err


def _is_int(v):
    return isinstance(v, int) and not isinstance(v, bool)


def find_python_strings(node, path="$"):
    if isinstance(node, str):
        yield path, node
    elif isinstance(node, dict):
        for k, v in node.items():
            yield from find_python_strings(v, "%s.%s" % (path, k))
    elif isinstance(node, list):
        for i, v in enumerate(node):
            yield from find_python_strings(v, "%s[%d]" % (path, i))


def load_json(path, findings, family_code="EFAMILY"):
    if not os.path.isfile(path):
        findings.append(Finding(
            family_code, os.path.relpath(path, ROOT),
            "frozen family data file missing (M4 contracts FROZEN v1)"))
        return None
    try:
        with open(path, encoding="utf-8") as f:
            return json.load(f)
    except json.JSONDecodeError as exc:
        findings.append(Finding("EFILE", os.path.relpath(path, ROOT),
                                "invalid JSON: %s" % exc))
        return None


# ---------------------------------------------------------------------------
# Family checks
# ---------------------------------------------------------------------------

def check_clubs(doc, findings, where0="clubs.json"):
    """Returns the set of club ids (for foreign-key checks)."""
    ids = set()
    if doc is None:
        return ids
    if not _is_int(doc.get("schema_version")):
        findings.append(Finding("EFILE", where0,
                                "schema_version must be an int"))
    clubs = doc.get("clubs")
    if not isinstance(clubs, list) or not clubs:
        findings.append(Finding("EFILE", where0, "'clubs' must be a list"))
        return ids
    vocab = set()
    for club in clubs:
        cid = club.get("id") if isinstance(club, dict) else None
        w = "%s:club:%s" % (where0, cid or "?")
        if not cid or not isinstance(cid, str):
            findings.append(Finding("EID", w, "club needs a string id"))
            continue
        if cid in ids:
            findings.append(Finding("EID", w, "duplicate club id"))
        ids.add(cid)
        for key in ("label", "category", "meets_label", "cost_per_year",
                    "member_cap", "adults_only", "min_age", "max_age"):
            if key not in club:
                findings.append(Finding(
                    "ECLUB", w, "missing required key %r" % key))
        cat = club.get("category")
        vocab.add(cat)
        if cat is not None and cat not in CLUB_CATEGORIES:
            findings.append(Finding(
                "ECLUBCAT", w,
                "category %r not in the frozen vocab %s"
                % (cat, sorted(CLUB_CATEGORIES))))
        min_age, max_age = club.get("min_age"), club.get("max_age")
        if _is_int(min_age) and _is_int(max_age):
            if min_age < 0 or min_age > max_age:
                findings.append(Finding(
                    "ECLUBAGE", w, "need 0 <= min_age <= max_age, got "
                                   "%r/%r" % (min_age, max_age)))
        adults_only = club.get("adults_only")
        if adults_only is True and _is_int(min_age) and min_age < 18:
            findings.append(Finding(
                "ECLUBAGE", w, "adults_only club with min_age %d < 18 "
                               "(fixed rule: adult venues are 18+)"
                               % min_age))
        if adults_only is False and _is_int(min_age) and min_age >= 18:
            findings.append(Finding(
                "ECLUBAGE", w, "min_age %d >= 18 but adults_only is false "
                               "— an 18+ floor IS an adults-only venue; "
                               "B renders the 18+ badge from the flag"
                               % min_age))
        cost = club.get("cost_per_year")
        if not _is_int(cost) or cost < 0:
            findings.append(Finding(
                "EBOUND", w, "cost_per_year must be int >= 0 (Amendment 1: "
                             "costs ride cost_per_year, not 'join')"))
        cap = club.get("member_cap")
        if _is_int(cap) is False and cap is not None:
            findings.append(Finding("EBOUND", w,
                                    "member_cap must be an int"))
        delta = club.get("popularity_delta")
        if delta is not None and not _is_int(delta):
            findings.append(Finding("EBOUND", w,
                                    "popularity_delta must be an int"))
    if vocab and vocab.isdisjoint(B_CATEGORY_NOTE):
        findings.append(Finding(
            "WCATVOCAB", where0,
            "no category overlaps B's published chip vocab %s — B/C "
            "category vocabulary needs a Claude amendment (gating on "
            "C's data schema %s)" % (sorted(B_CATEGORY_NOTE),
                                     sorted(CLUB_CATEGORIES)),
            fatal=False))
    return ids


def check_bullying(doc, findings, where0="bullying.json"):
    if doc is None:
        return
    if not _is_int(doc.get("schema_version")):
        findings.append(Finding("EFILE", where0,
                                "schema_version must be an int"))
    stages = doc.get("stages")
    if not isinstance(stages, list) or not stages:
        findings.append(Finding("EFILE", where0, "'stages' must be a list"))
        stages = []
    for st in stages:
        if st not in BULLY_STAGES:
            findings.append(Finding("EBULLY", "%s:stages" % where0,
                                    "unknown stage %r" % st))
    responses = doc.get("responses")
    if not isinstance(responses, dict):
        findings.append(Finding("EFILE", where0,
                                "'responses' must be an object"))
        return
    for key in sorted(BULLY_RESPONSES - set(responses)):
        findings.append(Finding(
            "EBULLY", "%s:responses" % where0,
            "missing the frozen real response %r" % key))
    for rid, resp in responses.items():
        w = "%s:responses:%s" % (where0, rid)
        if not isinstance(resp, dict):
            findings.append(Finding("EBULLY", w, "response must be object"))
            continue
        for tk in ("to_stage", "fail_to_stage"):
            if tk in resp and resp[tk] is not None \
                    and resp[tk] not in BULLY_STAGES:
                findings.append(Finding(
                    "EBULLY", w, "%s %r is not a frozen stage"
                    % (tk, resp[tk])))
        if rid == "tell_someone" and any(
                "chance" in k or "prob" in k for k in resp):
            findings.append(Finding(
                "EBULLY", w, "tell_someone must always work — no "
                             "rng/probability field allowed"))


def check_bands(doc, findings):
    where0 = "popularity_bands.json"
    if doc is None:
        return
    bands = doc.get("bands") or doc
    if not isinstance(bands, (list, dict)):
        findings.append(Finding("EFILE", where0,
                                "'bands' must be a list or an object"))
        return

    def entries():
        if isinstance(bands, list):
            for b in bands:
                yield b.get("band") or b.get("id"), b
        else:
            for k, b in bands.items():
                yield k, b

    seen = {}
    for band_id, b in entries():
        w = "%s:%s" % (where0, band_id or "?")
        if band_id not in FROZEN_BANDS:
            findings.append(Finding("EBANDS", w,
                                    "band %r not in the frozen vocabulary"
                                    % band_id))
            continue
        lo, hi = b.get("lo"), b.get("hi")
        if not (_is_int(lo) and _is_int(hi)):
            for key in ("min", "max"):
                if key in b:
                    lo, hi = b.get("min"), b.get("max")
        if not (_is_int(lo) and _is_int(hi)):
            findings.append(Finding("EBANDS", w, "needs int lo/hi bounds"))
            continue
        flo, fhi = FROZEN_BANDS[band_id]
        if (lo, hi) != (flo, fhi):
            findings.append(Finding(
                "EBANDS", w, "frozen bounds for %s are %s, got %s"
                % (band_id, (flo, fhi), (lo, hi))))
        if not b.get("label"):
            findings.append(Finding("EBANDS", w,
                                    "band needs a display label"))
        seen[band_id] = (lo, hi)
    missing = set(FROZEN_BANDS) - set(seen)
    if missing:
        findings.append(Finding("EBANDS", where0,
                                "missing frozen bands %s" % sorted(missing)))
    bounds = [seen[b] for b in ("unknown", "known", "liked", "popular",
                                 "beloved") if b in seen]
    for (lo1, hi1), (lo2, hi2) in zip(bounds, bounds[1:]):
        if lo2 != hi1 + 1:
            findings.append(Finding(
                "EBANDS", where0,
                "bands must be contiguous and monotonic: %s then %s"
                % ((lo1, hi1), (lo2, hi2))))


def iter_event_docs(paths):
    for path in paths:
        name = os.path.basename(path)
        with open(path, encoding="utf-8") as f:
            try:
                doc = json.load(f)
            except json.JSONDecodeError as exc:
                findings_bad(name, str(exc))
                continue
        if isinstance(doc, dict) and isinstance(doc.get("events"), list):
            for ev in doc["events"]:
                yield name, ev
        elif isinstance(doc, dict) and "id" in doc:
            yield name, doc
        else:
            yield name, None


_BAD = []


def findings_bad(name, err):
    _BAD.append(Finding("EFILE", name, "invalid JSON: %s" % err))


def check_social_events(paths, club_ids, findings):
    """v3 production validation + D-owned M4 checks. Returns soc_ count."""
    from src.game.events import (EventValidationError, validate_event3)
    count = 0
    seen_ids = {}
    identities = {}
    any_m4_file = False
    seen_paths = set(paths)
    for name in sorted(os.listdir(EVENT_CONTENT_DIR)):
        if name.startswith("m4_") and name.endswith(".json"):
            any_m4_file = True
            full = os.path.join(EVENT_CONTENT_DIR, name)
            if full not in seen_paths:
                paths.append(full)
                seen_paths.add(full)
    if not any_m4_file and paths is not None and \
            all("fixtures" in p for p in paths):
        any_m4_file = True  # selftest mode: fixture files only
    if not any_m4_file:
        findings.append(Finding(
            "EM4EVENTS", os.path.relpath(EVENT_CONTENT_DIR, ROOT),
            "no m4_*.json social event files landed (C family, FROZEN v1)"))
    for path in paths:
        if not path.endswith(".json"):
            continue
        name = os.path.basename(path)
        with open(path, encoding="utf-8") as f:
            try:
                doc = json.load(f)
            except json.JSONDecodeError as exc:
                findings.append(Finding("EFILE", name,
                                        "invalid JSON: %s" % exc))
                continue
        events = doc.get("events") if isinstance(doc, dict) else None
        if events is None and isinstance(doc, dict) and "id" in doc:
            events = [doc]
        if not isinstance(events, list):
            findings.append(Finding("EFILE", name,
                                    "bad event file shape"))
            continue
        for path_s, s in find_python_strings(doc):
            if PYTHONY.search(s):
                findings.append(Finding(
                    "EPYTHON", "%s:%s" % (name, path_s),
                    "Python call %r in authored content string"
                    % PYTHONY.search(s).group(0)))
        for ev in events:
            if not isinstance(ev, dict):
                findings.append(Finding("EFILE", name,
                                        "event entry not an object"))
                continue
            eid = ev.get("id")
            w = "%s:%s" % (name, eid or "?")
            if not eid or not isinstance(eid, str):
                findings.append(Finding("EID", w, "event needs a string id"))
                continue
            is_soc = eid.startswith("soc_")
            try:
                validate_event3(ev)
            except EventValidationError as exc:
                findings.append(Finding("ESCHEMA", w, str(exc)))
                continue
            except Exception as exc:
                findings.append(Finding("ECHECKER", w,
                                        "validator crashed: %r" % exc))
                continue
            if not is_soc:
                continue
            count += 1
            if eid in seen_ids:
                findings.append(Finding(
                    "EDUP", w, "duplicate event id also in %s" % seen_ids[eid]))
            else:
                seen_ids[eid] = w
            ident = json.dumps({k: ev.get(k) for k in
                                ("conditions", "choices", "interaction",
                                 "roles")}, sort_keys=True)
            if ident in identities:
                findings.append(Finding(
                    "ERESKIN", w, "identical conditions/choices/effects as "
                                  "%s (reskin?)" % identities[ident],
                    fatal=False))
            else:
                identities[ident] = w
            _check_one_social_event(ev, w, club_ids, findings)
    return count


def _check_one_social_event(ev, w, club_ids, findings):
    cond = ev.get("conditions") or {}
    eid = ev.get("id", "")
    # EAGE: every soc_ record declares its age band (C content
    # invariant). min_age-only is a valid open-ended adult band (the
    # production v3 convention); both present must be ordered.
    min_age, max_age = cond.get("min_age"), cond.get("max_age")
    if not (_is_int(min_age) or _is_int(max_age)):
        findings.append(Finding(
            "EAGE", w, "soc_ event must declare its age band "
                       "(conditions.min_age and/or max_age)"))
    elif _is_int(min_age) and _is_int(max_age) and min_age > max_age:
        findings.append(Finding(
            "EAGE", w, "conditions.min_age %r > max_age %r"
            % (min_age, max_age)))
    # ESOFTLOCK: >= 1 free choice.
    choices = ev.get("choices") or []
    free = False
    for ch in choices:
        cost = (ch or {}).get("cost") or {}
        if not _is_int(cost.get("money")) or cost.get("money") <= 0:
            free = True
        # ETEXT: outcome lines >= MIN_OUTCOME_WORDS.
        text = (ch or {}).get("outcome_text") or ""
        if len(text.split()) < MIN_OUTCOME_WORDS:
            findings.append(Finding(
                "ETEXT", "%s:choice:%s" % (w, (ch or {}).get("id") or "?"),
                "outcome_text has %d words, needs >= %d"
                % (len(text.split()), MIN_OUTCOME_WORDS)))
    if choices and not free:
        findings.append(Finding(
            "ESOFTLOCK", w, "no free choice (soft-lock rule)"))
    # EINVITE / EINVITEAGE: invite records carry venue data, and adult
    # venues are 18+.
    if eid.startswith("soc_inv_") or \
            (ev.get("interaction") or {}).get("kind") == "social_invite":
        inter = ev.get("interaction") or {}
        venue = ev.get("venue_label", inter.get("venue_label"))
        adults = ev.get("adults_only", inter.get("adults_only"))
        if venue is None or adults is None:
            findings.append(Finding(
                "EINVITE", w, "invite record must carry venue_label and "
                              "adults_only (top level or interaction) so "
                              "A's 18+ gate has data"))
        elif adults is True and (_is_int(cond.get("min_age"))
                                 and cond["min_age"] < 18
                                 or not _is_int(cond.get("min_age"))):
            findings.append(Finding(
                "EINVITEAGE", w, "adults_only invite must be gated "
                                 "conditions.min_age >= 18, got %r"
                                 % cond.get("min_age")))
    # EREFCLUB: in_club conditions reference defined clubs.
    leaves = []
    if isinstance(cond.get("all"), list):
        leaves += cond["all"]
    if isinstance(cond.get("any"), list):
        leaves += cond["any"]
    if isinstance(cond.get("requires"), list):
        leaves += cond["requires"]
    for leaf in leaves:
        if isinstance(leaf, dict) and leaf.get("kind") == "in_club":
            refs = []
            if leaf.get("club_id"):
                refs.append(leaf["club_id"])
            refs += leaf.get("club_ids") or []
            for ref in refs:
                if club_ids and ref not in club_ids:
                    findings.append(Finding(
                        "EREFCLUB", w, "in_club references unknown club "
                                       "%r" % ref))


# ---------------------------------------------------------------------------
# Modes
# ---------------------------------------------------------------------------

def validate(paths=None):
    findings = []
    if paths is None:
        paths = []
        for name in sorted(os.listdir(EVENT_CONTENT_DIR)):
            if name.startswith("m4_") and name.endswith(".json"):
                paths.append(os.path.join(EVENT_CONTENT_DIR, name))
        clubs = load_json(CLUBS_PATH, findings)
        club_ids = check_clubs(clubs, findings)
        check_bullying(load_json(BULLYING_PATH, findings), findings)
        check_bands(load_json(BANDS_PATH, findings), findings)
        count = check_social_events(list(paths), club_ids, findings)
        if count < MIN_SOCIAL_EVENTS:
            findings.append(Finding(
                "ESPEC", "m4_*.json",
                "kickoff/§C pin >= %d soc_ social events, found %d"
                % (MIN_SOCIAL_EVENTS, count)))
        return _BAD + findings
    # selftest / explicit-paths mode: dispatch each file by its shape.
    event_paths = []
    for path in paths:
        name = os.path.basename(path)
        with open(path, encoding="utf-8") as f:
            try:
                doc = json.load(f)
            except json.JSONDecodeError as exc:
                findings.append(Finding("EFILE", name,
                                        "invalid JSON: %s" % exc))
                continue
        if isinstance(doc, dict) and "clubs" in doc:
            check_clubs(doc, findings, where0=name)
        elif isinstance(doc, dict) and ("responses" in doc or
                                        "stages" in doc):
            check_bullying(doc, findings, where0=name)
        elif isinstance(doc, dict) and "bands" in doc:
            check_bands(doc, findings)
        else:
            event_paths.append(path)
    count = check_social_events(event_paths, set(), findings)
    return _BAD + findings


def selftest():
    """Invalid m4_* fixtures must fail with the filename's expected code.
    M4 fixtures carry an '.m4.' segment to partition the shared fixture
    directory from the M3 selftest."""
    m4_fixtures = sorted(
        os.path.join(INVALID_FIXTURES, n)
        for n in os.listdir(INVALID_FIXTURES)
        if ".m4." in n and n.endswith(".json"))
    findings = validate(m4_fixtures)
    by_file = {}
    for f in findings:
        by_file.setdefault(f.where.split(":")[0], []).append(f)
    ok_all = True
    for name in sorted(os.listdir(INVALID_FIXTURES)):
        if not (".m4." in name and name.endswith(".json")):
            continue
        expected = name.split(".")[0]
        hits = by_file.get(name, [])
        if not hits:
            print("SELFTEST FAIL %s: fixture passed validation "
                  "(expected %s)" % (name, expected))
            ok_all = False
            continue
        codes = {h.code for h in hits}
        if expected in codes:
            print("SELFTEST PASS %s (%s)" % (name, expected))
        else:
            print("SELFTEST FAIL %s: expected %s, got %s (%s)"
                  % (name, expected, sorted(codes),
                     "; ".join(h.message for h in hits)[:160]))
            ok_all = False
    return 0 if ok_all else 1


def main():
    if "--selftest" in sys.argv:
        return selftest()
    os.makedirs(EVIDENCE, exist_ok=True)
    findings = validate()
    fatals = [f for f in findings if f.fatal]
    warnings = [f for f in findings if not f.fatal]
    for f in findings:
        print("%s %s %s: %s" % ("ERROR" if f.fatal else "WARN ",
                                f.code, f.where, f.message))
    with open(os.path.join(EVIDENCE, "validate-m4-social.json"), "w") as f:
        json.dump({"errors": [x.as_dict() for x in fatals],
                   "warnings": [x.as_dict() for x in warnings]},
                  f, indent=1)
    with open(os.path.join(EVIDENCE, "validate-m4-social.md"), "w") as f:
        f.write("# M4 social content validation (Kimi D tooling, "
                "%s)\n\n" % __import__("time").strftime("%Y-%m-%d"))
        f.write("Command: `python3 tools/validate_m4_social.py` "
                "(contracts: docs/collab/M4/02-contracts*.md, FROZEN v1)\n\n")
        f.write("- errors: %d, warnings: %d\n" % (len(fatals),
                                                  len(warnings)))
        for x in findings:
            f.write("- %s `%s` **%s**: %s\n"
                    % ("ERROR" if x.fatal else "WARN", x.code, x.where,
                       x.message))
        if not fatals:
            f.write("\nAll frozen M4 content passes.\n")
    print("errors: %d, warnings: %d" % (len(fatals), len(warnings)))
    print("evidence: %s" % EVIDENCE)
    return 1 if fatals else 0


if __name__ == "__main__":
    sys.exit(main())
