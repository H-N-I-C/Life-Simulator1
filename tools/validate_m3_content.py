"""M3 content validation (Kimi D, tooling/QA owner).

One command for all M3 content families:

    python3 tools/validate_m3_content.py            # validate content
    python3 tools/validate_m3_content.py --selftest # fixture self-test

What it does:
  * Runs the PRODUCTION frozen structural checkers (validate_event /
    validate_event3 in src/game/events.py) over every event content file
    and COLLECTS all failures (the production loader stops at the first).
  * Adds D-owned checks production does not do:
      - EPYTHON: no Python/eval/exec/__import__ anywhere in authored
        content strings (frozen rule: no code in content).
      - ERESKIN: warning — two events with identical normalized
        (conditions, choices effects/costs) but different IDs.
      - EASSET: portrait/look keys must exist in the pinned M0b manifest
        (active once career/program data references looks).
  * Family validators (careers/employers=A, programs=B, skills=C) run in
    WARN-ONLY draft mode until the family's schema is FROZEN in
    docs/collab/M3/02-contracts.md; they never gate.
  * --selftest runs the intentionally-invalid fixtures under
    tests/content/fixtures/invalid/ (one broken rule per file, expected
    code in the filename prefix). A fixture that does NOT fail with its
    expected code is itself a FAILURE.

Exit non-zero if any frozen-input check fails or selftest fails.
Reports: docs/collab/M3/../evidence/M3/content-validation.{md,json}
"""

import json
import os
import re
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

EVIDENCE = os.path.join(ROOT, "docs", "collab", "evidence", "M3")
EVENT_CONTENT_DIR = os.path.join(ROOT, "src", "game", "content", "events")
INVALID_FIXTURES = os.path.join(ROOT, "tests", "content", "fixtures",
                                "invalid")

PYTHONY = re.compile(r"\b(eval|exec|__import__|compile|globals|locals)\s*\(")


class Finding:
    def __init__(self, code, where, message, fatal=True):
        self.code = code
        self.where = where
        self.message = message
        self.fatal = fatal

    def as_dict(self):
        return {"code": self.code, "where": self.where,
                "message": self.message, "fatal": self.fatal}


def find_python_strings(node, path="$"):
    """Yield (json-path, string) for every string in the doc."""
    if isinstance(node, str):
        yield path, node
    elif isinstance(node, dict):
        for k, v in node.items():
            yield from find_python_strings(v, "%s.%s" % (path, k))
    elif isinstance(node, list):
        for i, v in enumerate(node):
            yield from find_python_strings(v, "%s[%d]" % (path, i))


def normalize_event(ev):
    """Normalized identity for reskin detection."""
    def norm(node):
        if isinstance(node, dict):
            return {k: norm(v) for k, v in sorted(node.items())}
        if isinstance(node, list):
            return [norm(v) for v in node]
        return node
    identity = {k: norm(ev.get(k)) for k in
                ("conditions", "choices", "interaction", "roles")}
    return json.dumps(identity, sort_keys=True)


def check_event_docs(docs, manifest_looks=None):
    """Run production validation + D checks over parsed docs.

    docs: list of (filename, doc-dict). Returns list of Finding."""
    from src.game.events import (EventValidationError, validate_event,
                                 validate_event3)
    findings = []
    seen_ids = {}
    identities = {}

    for fname, doc in docs:
        where_doc = fname
        if not isinstance(doc, dict) \
                or not isinstance(doc.get("events"), list):
            findings.append(Finding("EFILE", where_doc,
                                    "bad event file shape (need "
                                    "{'events': [...]})"))
            continue
        version = doc.get("schema_version", 2)
        if not isinstance(version, int):
            findings.append(Finding("EFILE", where_doc,
                                    "schema_version must be an int"))
            continue
        # EPYTHON: no code in authored content, any schema version
        for path, s in find_python_strings(doc):
            if PYTHONY.search(s):
                findings.append(Finding(
                    "EPYTHON", "%s:%s" % (where_doc, path),
                    "Python call %r in authored content string"
                    % PYTHONY.search(s).group(0)))
        for ev in doc["events"]:
            eid = ev.get("id") if isinstance(ev, dict) else None
            where = "%s:%s" % (where_doc, eid or "?")
            try:
                if version == 3:
                    validate_event3(ev)
                else:
                    validate_event(ev)
            except EventValidationError as exc:
                findings.append(Finding("ESCHEMA", where, str(exc)))
                continue
            except Exception as exc:  # unexpected checker crash: report
                findings.append(Finding("ECHECKER", where,
                                        "validator crashed: %r" % exc))
                continue
            # cross-file unique IDs (EDUP)
            if eid in seen_ids:
                findings.append(Finding(
                    "EDUP", where,
                    "duplicate event id also in %s" % seen_ids[eid]))
            else:
                seen_ids[eid] = where
            # reskin detection (ERESKIN, warning)
            ident = normalize_event(ev)
            if ident in identities:
                findings.append(Finding(
                    "ERESKIN", where,
                    "identical conditions/choices/effects as %s "
                    "(reskin?)" % identities[ident], fatal=False))
            else:
                identities[ident] = where
    return findings


def check_manifest_looks(look_keys, manifest_path):
    """EASSET: look keys must exist in the pinned M0b manifest."""
    findings = []
    if not look_keys:
        return findings
    with open(manifest_path, encoding="utf-8") as f:
        manifest = json.load(f)
    looks = set(manifest.get("looks") or [])
    # the manifest 'looks' list holds the valid look-override vocabulary
    if not looks:
        looks = set(manifest.get("combos", {}).get("looks") or [])
    for where, key in look_keys:
        if key not in looks:
            findings.append(Finding(
                "EASSET", where,
                "look key %r not in pinned M0b manifest vocabulary" % key))
    return findings


def validate_files(paths, manifest_path=None):
    docs = []
    for path in paths:
        with open(path, encoding="utf-8") as f:
            try:
                docs.append((os.path.basename(path), json.load(f)))
            except json.JSONDecodeError as exc:
                docs.append((os.path.basename(path),
                             _BadJson(str(exc))))
    findings = []
    good_docs = [(n, d) for n, d in docs if not isinstance(d, _BadJson)]
    for n, d in docs:
        if isinstance(d, _BadJson):
            findings.append(Finding("EFILE", n, "invalid JSON: %s" % d.err))
    return findings + check_event_docs(good_docs)


class _BadJson:
    def __init__(self, err):
        self.err = err


# ---------------------------------------------------------------------------
# Family validators — FROZEN v1 schemas (§Careers A / §Education B /
# §Skills C). These are ERROR-gating: a missing family file, a bad
# foreign key, an unreachable role or an out-of-bounds wage fails the
# command. (WARN-only mode ended at freeze, 2026-11-20.)
# ---------------------------------------------------------------------------

FAMILY_FILES = {
    "careers": os.path.join(ROOT, "src", "game", "content", "careers",
                            "careers.json"),
    "programs": os.path.join(ROOT, "src", "game", "content", "education",
                             "programs.json"),
    "skills": os.path.join(ROOT, "src", "game", "content", "skills",
                           "skills.json"),
}

FUNDING_VOCAB = {"self", "student_loan", "parents", "scholarship"}
SPEC_PATHS = {"electrical", "office", "healthcare"}
SPEC_N = {"careers.roles": 12, "careers.employers": 6,
          "programs.programs": 4}
SKILL_IDS_SPEC = {"practical", "organization", "communication", "care"}


def _is_int(v):
    return isinstance(v, int) and not isinstance(v, bool)


def load_family(name, findings):
    path = FAMILY_FILES[name]
    if not os.path.isfile(path):
        findings.append(Finding(
            "EMISSING", os.path.relpath(path, ROOT),
            "frozen family data file missing (schema FROZEN v1)"))
        return None
    try:
        with open(path, encoding="utf-8") as f:
            return json.load(f)
    except json.JSONDecodeError as exc:
        findings.append(Finding("EFILE", os.path.relpath(path, ROOT),
                                "invalid JSON: %s" % exc))
        return None


def check_skills(doc, findings):
    where0 = "skills.json"
    if not _is_int(doc.get("schema_version")):
        findings.append(Finding("EFILE", where0, "schema_version must be int"))
    skills = doc.get("skills")
    if not isinstance(skills, list) or not skills:
        findings.append(Finding("EFILE", where0, "'skills' must be a list"))
        return set()
    ids = set()
    for sk in skills:
        sid = sk.get("id")
        w = "%s:skill:%s" % (where0, sid or "?")
        if not sid or not isinstance(sid, str):
            findings.append(Finding("EID", w, "skill needs a string id"))
            continue
        if sid in ids:
            findings.append(Finding("EID", w, "duplicate skill id"))
        ids.add(sid)
        lo, hi, default = sk.get("min"), sk.get("max"), sk.get("default")
        if not all(_is_int(v) for v in (lo, hi, default)) or \
                not 0 <= lo <= default <= hi <= 100:
            findings.append(Finding(
                "EBOUND", w, "need ints 0 <= min <= default <= max <= 100, "
                "got %r/%r/%r" % (lo, default, hi)))
    if ids != SKILL_IDS_SPEC:
        findings.append(Finding(
            "ESPEC", where0, "frozen skill set is %s, got %s"
            % (sorted(SKILL_IDS_SPEC), sorted(ids))))
    opts = doc.get("training_options") or []
    seen = set()
    for opt in opts:
        oid = opt.get("id")
        w = "%s:training:%s" % (where0, oid or "?")
        if not oid:
            findings.append(Finding("EID", w, "training option needs id"))
            continue
        if oid in seen:
            findings.append(Finding("EID", w, "duplicate training option id"))
        seen.add(oid)
        skill = opt.get("skill")
        if skill is not None and skill not in ids:
            findings.append(Finding(
                "EREF", w, "skill %r not defined" % skill))
        if skill is None and not opt.get("choose_skill"):
            findings.append(Finding(
                "EFILE", w, "skill-null option must set choose_skill"))
        cost = (opt.get("cost") or {}).get("money", 0)
        if not _is_int(cost) or cost < 0:
            findings.append(Finding("EBOUND", w,
                                    "cost.money must be int >= 0"))
        limit = opt.get("annual_limit")
        if not _is_int(limit) or limit < 1:
            findings.append(Finding("EBOUND", w,
                                    "annual_limit must be int >= 1"))
        prog = opt.get("progress") or {}
        if not (_is_int(prog.get("min")) and _is_int(prog.get("max"))) \
                or prog["min"] > prog["max"] or prog["min"] < 0:
            findings.append(Finding(
                "EBOUND", w, "progress.min/max must be ints, 0<=min<=max"))
        req = opt.get("requires") or {}
        if req.get("min_age") is not None and (
                not _is_int(req["min_age"]) or not 0 <= req["min_age"]):
            findings.append(Finding("EBOUND", w,
                                    "requires.min_age must be int >= 0"))
    return ids


def check_programs(doc, findings, skill_ids, role_ids, employer_ids,
                   event_ids):
    where0 = "programs.json"
    if not _is_int(doc.get("schema_version")):
        findings.append(Finding("EFILE", where0, "schema_version must be int"))
    programs = doc.get("programs")
    if not isinstance(programs, list) or not programs:
        findings.append(Finding("EFILE", where0, "'programs' must be a list"))
        return set()
    if len(programs) != SPEC_N["programs.programs"]:
        findings.append(Finding(
            "ESPEC", where0, "frozen set is exactly %d programs, got %d"
            % (SPEC_N["programs.programs"], len(programs))))
    ids, creds = set(), set()
    for p in programs:
        pid = p.get("id")
        w = "%s:program:%s" % (where0, pid or "?")
        if not pid:
            findings.append(Finding("EID", w, "program needs id"))
            continue
        if pid in ids:
            findings.append(Finding("EID", w, "duplicate program id"))
        ids.add(pid)
        cred = p.get("credential_id")
        if not cred or not isinstance(cred, str):
            findings.append(Finding("EID", w, "program needs credential_id"))
        elif cred in creds:
            findings.append(Finding(
                "EID", w, "duplicate credential_id %r" % cred))
        else:
            creds.add(cred)
        for key in ("duration_years", "tuition_per_year", "fee_per_year",
                    "min_age"):
            v = p.get(key)
            if not _is_int(v) or v < 0 or (key == "duration_years"
                                           and not 1 <= v <= 10):
                findings.append(Finding(
                    "EBOUND", w, "%s must be an int in range, got %r"
                    % (key, v)))
        if not isinstance(p.get("requires_secondary"), bool):
            findings.append(Finding("EFILE", w,
                                    "requires_secondary must be a bool"))
        adm = p.get("admission") or {}
        ms = adm.get("min_smarts")
        if ms is not None and (not _is_int(ms) or not 0 <= ms <= 100):
            findings.append(Finding("EBOUND", w,
                                    "admission.min_smarts null or 0..100"))
        bad_funding = [f for f in (p.get("funding") or [])
                       if f not in FUNDING_VOCAB]
        if bad_funding:
            findings.append(Finding(
                "EVOCAB", w, "funding outside frozen vocabulary: %r"
                % bad_funding))
        sk = p.get("study_skill")
        if sk is not None and sk not in skill_ids:
            findings.append(Finding("EREF", w,
                                    "study_skill %r not defined" % sk))
        ar = p.get("apprentice_role")
        if ar is not None and role_ids:
            # role/employer ids are validated in the second pass once the
            # careers family has been loaded
            if ar.get("role_id") not in role_ids:
                findings.append(Finding(
                    "EREF", w, "apprentice_role.role_id %r not a known role"
                    % ar.get("role_id")))
            if ar.get("employer_id") not in employer_ids:
                findings.append(Finding(
                    "EREF", w,
                    "apprentice_role.employer_id %r not a known employer"
                    % ar.get("employer_id")))
        for eid in p.get("events") or []:
            if eid not in event_ids:
                findings.append(Finding(
                    "EEVENT", w, "event %r not in loaded event content"
                    % eid))
    # admission.qualifications must be credentials of the SAME catalog and
    # the credential dependency graph must be acyclic (no circular prereqs)
    graph = {}
    for p in programs:
        w = "%s:program:%s" % (where0, p.get("id") or "?")
        deps = []
        for q in (p.get("admission") or {}).get("qualifications") or []:
            if q not in creds:
                findings.append(Finding(
                    "EREF", w, "admission.qualification %r is not awarded by "
                    "any program in the catalog" % q))
            else:
                deps.append(q)
        graph[p.get("credential_id")] = deps
    cred_owner = {p.get("credential_id"): p.get("id") for p in programs}
    # map credential deps to owner programs for cycle reporting
    prog_deps = {p.get("id"): [cred_owner.get(q) for q in deps]
                 for p in programs
                 for deps in [((p.get("admission") or {})
                               .get("qualifications") or [])]}
    _acyclic(prog_deps, findings, where0, "EGRAPH",
             "circular program prerequisite")
    return creds


def _acyclic(edges, findings, where0, code, label):
    """edges: node -> [node]. Report one finding per detected cycle."""
    WHITE, GRAY, BLACK = 0, 1, 2
    color = {n: WHITE for n in edges}
    for start in edges:
        if color.get(start) != WHITE:
            continue
        stack = [(start, iter(edges.get(start) or []))]
        color[start] = GRAY
        path = [start]
        while stack:
            node, it = stack[-1]
            nxt = next(it, None)
            if nxt is None:
                color[node] = BLACK
                stack.pop()
                path.pop()
                continue
            if color.get(nxt) == GRAY:
                findings.append(Finding(
                    code, where0, "%s: %s" % (label, " -> ".join(
                        path + [nxt]))))
            elif color.get(nxt) == WHITE:
                color[nxt] = GRAY
                path.append(nxt)
                stack.append((nxt, iter(edges.get(nxt) or [])))


def check_careers(doc, findings, skill_ids, cred_ids, event_ids):
    where0 = "careers.json"
    if not _is_int(doc.get("schema_version")):
        findings.append(Finding("EFILE", where0, "schema_version must be int"))
    employers = doc.get("employers") or []
    roles = doc.get("roles") or []
    if len(roles) != SPEC_N["careers.roles"]:
        findings.append(Finding(
            "ESPEC", where0, "frozen set is exactly %d roles, got %d"
            % (SPEC_N["careers.roles"], len(roles))))
    if len(employers) != SPEC_N["careers.employers"]:
        findings.append(Finding(
            "ESPEC", where0, "frozen set is exactly %d employers, got %d"
            % (SPEC_N["careers.employers"], len(employers))))
    role_ids, employer_ids = set(), set()
    for e in employers:
        eid = e.get("id")
        w = "%s:employer:%s" % (where0, eid or "?")
        if not eid:
            findings.append(Finding("EID", w, "employer needs id"))
            continue
        if eid in employer_ids:
            findings.append(Finding("EID", w, "duplicate employer id"))
        employer_ids.add(eid)
        if not isinstance(e.get("name"), str) or not e["name"]:
            findings.append(Finding("EFILE", w, "employer needs a name"))
    for r in roles:
        rid = r.get("id")
        w = "%s:role:%s" % (where0, rid or "?")
        if not rid:
            findings.append(Finding("EID", w, "role needs id"))
            continue
        if rid in role_ids:
            findings.append(Finding("EID", w, "duplicate role id"))
        role_ids.add(rid)
        if not isinstance(r.get("label"), str) or not r["label"]:
            findings.append(Finding("EFILE", w, "role needs a label"))
        if r.get("path") not in SPEC_PATHS:
            findings.append(Finding(
                "EVOCAB", w, "path %r outside frozen paths %s"
                % (r.get("path"), sorted(SPEC_PATHS))))
        for key, lo, hi in (("level", 1, 10), ("base_hours", 1, 80),
                            ("promote_after_years", 0, 70)):
            v = r.get(key)
            if v is not None and (not _is_int(v) or not lo <= v <= hi):
                findings.append(Finding(
                    "EBOUND", w, "%s must be int in [%d, %d], got %r"
                    % (key, lo, hi, v)))
        lo, hi = r.get("pay_min"), r.get("pay_max")
        if not _is_int(lo) or not _is_int(hi) or lo < 0 or hi < lo:
            findings.append(Finding(
                "EBOUND", w, "need ints 0 <= pay_min <= pay_max, got "
                "%r/%r" % (lo, hi)))
        pm = r.get("promote_performance_min")
        if pm is not None and (not _is_int(pm) or not 0 <= pm <= 100):
            findings.append(Finding(
                "EBOUND", w, "promote_performance_min must be int 0..100"))
        pre = r.get("prerequisites") or {}
        ma = pre.get("min_age")
        if ma is not None and (not _is_int(ma) or not 0 <= ma <= 99):
            findings.append(Finding("EBOUND", w,
                                    "prerequisites.min_age int 0..99"))
        for cred in pre.get("qualifications") or []:
            if cred not in cred_ids:
                findings.append(Finding(
                    "EREF", w, "prerequisite qualification %r not awarded by "
                    "any frozen program" % cred))
        for sid, need in (pre.get("skills") or {}).items():
            if sid not in skill_ids:
                findings.append(Finding(
                    "EREF", w, "prerequisite skill %r not defined" % sid))
            if not _is_int(need) or not 0 <= need <= 100:
                findings.append(Finding(
                    "EBOUND", w, "skill requirement %r must be int 0..100"
                    % sid))
        for need_role in pre.get("roles") or []:
            if need_role not in role_ids:
                findings.append(Finding(
                    "EREF", w, "prerequisite role %r not defined" % need_role))
        ps = r.get("practices_skill")
        if ps is not None and ps not in skill_ids:
            findings.append(Finding(
                "EREF", w, "practices_skill %r not defined" % ps))
        look = r.get("look_override")
        if look is not None and look not in MANIFEST_LOOKS:
            findings.append(Finding(
                "EASSET", w, "look_override %r outside pinned M0b manifest "
                "vocabulary" % look))
        for eid in r.get("events") or []:
            if eid not in event_ids:
                findings.append(Finding(
                    "EEVENT", w, "event %r not in loaded event content"
                    % eid))
    # employer role_ids must exist; every role offered by >=1 employer is
    # implied by openings — roles with no employer are unreachable offers
    for e in employers:
        for rid in e.get("role_ids") or []:
            if rid not in role_ids:
                findings.append(Finding(
                    "EREF", "%s:employer:%s" % (where0, e.get("id") or "?"),
                    "role_ids references unknown role %r" % rid))
    offered = {rid for e in employers for rid in e.get("role_ids") or []}
    for rid in role_ids - offered:
        findings.append(Finding(
            "EREACH", "%s:role:%s" % (where0, rid),
            "role is not in any employer's role_ids (no openings)"))
    # promotion graph: edges must resolve, be acyclic, and every role must
    # be reachable from a root role (no prerequisite roles)
    edges = {r["id"]: list(r.get("promotions_to") or []) for r in roles
             if r.get("id")}
    for rid, targets in edges.items():
        for t in targets:
            if t not in role_ids:
                findings.append(Finding(
                    "EREF", "%s:role:%s" % (where0, rid),
                    "promotions_to references unknown role %r" % t))
    _acyclic(edges, findings, where0, "EGRAPH", "promotion cycle")
    roots = {r["id"] for r in roles
             if r.get("id") and not (r.get("prerequisites") or {})
             .get("roles")}
    reached = set(roots)
    frontier = list(roots)
    while frontier:
        cur = frontier.pop()
        for nxt in edges.get(cur) or []:
            if nxt not in reached:
                reached.add(nxt)
                frontier.append(nxt)
    for rid in role_ids - reached:
        findings.append(Finding(
            "EREACH", "%s:role:%s" % (where0, rid),
            "role unreachable from any root role via promotions_to"))
    return role_ids, employer_ids


MANIFEST_LOOKS = set()


def _load_manifest_looks():
    path = os.path.join(ROOT, "web", "static", "avatars", "manifest.json")
    try:
        with open(path, encoding="utf-8") as f:
            MANIFEST_LOOKS.update(set(json.load(f).get("looks") or []))
    except Exception:
        MANIFEST_LOOKS.clear()


def event_ids_from_docs(docs):
    ids = set()
    for fname, doc in docs:
        if isinstance(doc, dict) and isinstance(doc.get("events"), list):
            for ev in doc["events"]:
                if isinstance(ev, dict) and isinstance(ev.get("id"), str):
                    ids.add(ev["id"])
    return ids


def validate_families(findings, docs):
    """Frozen v1 family validation (ERROR-gating). Order matters:
    skills -> programs (needs skill ids) -> careers (needs both)."""
    _load_manifest_looks()
    event_ids = event_ids_from_docs(docs)
    skills_doc = load_family("skills", findings)
    skill_ids = check_skills(skills_doc, findings) if skills_doc else set()
    programs_doc = load_family("programs", findings)
    cred_ids = set()
    if programs_doc:
        cred_ids = check_programs(programs_doc, findings, skill_ids,
                                  set(), set(), event_ids)
    careers_doc = load_family("careers", findings)
    if careers_doc:
        # pass known role/employer ids as empty first? programs needs
        # them for apprentice_role; careers defines them. Two-pass:
        role_ids = {r.get("id") for r in careers_doc.get("roles") or []
                    if isinstance(r.get("id"), str)}
        employer_ids = {e.get("id") for e in careers_doc.get("employers")
                        or [] if isinstance(e.get("id"), str)}
        if programs_doc:
            # re-check apprentice_role refs now that careers ids known
            for p in programs_doc.get("programs") or []:
                ar = p.get("apprentice_role")
                if ar and ar.get("role_id") not in role_ids:
                    findings.append(Finding(
                        "EREF", "programs.json:program:%s" % p.get("id"),
                        "apprentice_role.role_id %r not a known role"
                        % ar.get("role_id")))
                if ar and ar.get("employer_id") not in employer_ids:
                    findings.append(Finding(
                        "EREF", "programs.json:program:%s" % p.get("id"),
                        "apprentice_role.employer_id %r not a known "
                        "employer" % ar.get("employer_id")))
        check_careers(careers_doc, findings, skill_ids, cred_ids,
                      event_ids)


def iter_content_files():
    for name in sorted(os.listdir(EVENT_CONTENT_DIR)):
        if name.endswith(".json"):
            yield os.path.join(EVENT_CONTENT_DIR, name)


def selftest():
    """Invalid fixtures must fail with their filename's expected code.
    Fixtures with an '.m4.' segment belong to the M4 validator
    (tools/validate_m4_social.py) and are skipped here."""
    findings = validate_files(sorted(
        os.path.join(INVALID_FIXTURES, n)
        for n in os.listdir(INVALID_FIXTURES)
        if n.endswith(".json") and ".m4." not in n))
    by_file = {}
    for f in findings:
        fname = f.where.split(":")[0]
        by_file.setdefault(fname, []).append(f)
    ok_all = True
    for name in sorted(os.listdir(INVALID_FIXTURES)):
        if not name.endswith(".json") or ".m4." in name:
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
    manifest_path = os.path.join(ROOT, "web", "static", "avatars",
                                 "manifest.json")
    paths = list(iter_content_files())
    docs = []
    for path in paths:
        with open(path, encoding="utf-8") as f:
            try:
                docs.append((os.path.basename(path), json.load(f)))
            except json.JSONDecodeError as exc:
                docs.append((os.path.basename(path), _BadJson(str(exc))))
    findings = []
    good_docs = [(n, d) for n, d in docs if not isinstance(d, _BadJson)]
    for n, d in docs:
        if isinstance(d, _BadJson):
            findings.append(Finding("EFILE", n, "invalid JSON: %s" % d.err))
    findings += check_event_docs(good_docs)
    # frozen v1 families (ERROR-gating since the 2026-11-20 freeze)
    validate_families(findings, good_docs)
    fatals = [f for f in findings if f.fatal]
    warnings = [f for f in findings if not f.fatal]
    for f in findings:
        print("%s %s %s: %s" % ("ERROR" if f.fatal else "WARN ",
                                f.code, f.where, f.message))
    paths = sorted({f.where.split(":")[0] for f in findings})
    with open(os.path.join(EVIDENCE, "content-validation.json"), "w") as f:
        json.dump({"files_checked": len(list(iter_content_files())),
                   "errors": [x.as_dict() for x in fatals],
                   "warnings": [x.as_dict() for x in warnings]},
                  f, indent=1)
    with open(os.path.join(EVIDENCE, "content-validation.md"), "w") as f:
        f.write("# M3 content validation (Kimi D tooling)\n\n")
        f.write("Command: `python3 tools/validate_m3_content.py`\n\n")
        f.write("- files checked: %d\n" % len(list(iter_content_files())))
        f.write("- errors: %d, warnings: %d\n" % (len(fatals),
                                                  len(warnings)))
        for x in findings:
            f.write("- %s `%s` **%s** %s: %s\n"
                    % ("ERROR" if x.fatal else "WARN", x.code, x.where,
                       "", x.message))
        if not findings:
            f.write("\nAll frozen event content passes production schema "
                    "checks + D checks (EPYTHON/EDUP/ERESKIN).\n")
        f.write("\nFamily validators (careers/programs/skills): DRAFT "
                "mode, WARN-only, pending schema freeze in "
                "docs/collab/M3/02-contracts.md.\n")
    print("validation: %d errors, %d warnings; evidence in %s"
          % (len(fatals), len(warnings), EVIDENCE))
    return 1 if fatals else 0


if __name__ == "__main__":
    sys.exit(main())
