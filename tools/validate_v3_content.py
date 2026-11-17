#!/usr/bin/env python3
"""Validate life-event content files against v2.1 (frozen) and the
PROPOSED v3 schema (docs/collab/M2.5/contracts/event-schema-v3.md), and
run C's v3 fit-proof bundle (docs/collab/M2.5/contracts/v3-fit-proof.json).

Draft status: v3 rules mirror event-schema-v3.md (C's proposal); re-sync
at freeze. The validator NEVER guesses: unknown schema_version exits 2,
and any rule that cannot be checked against a frozen source is reported
as UNSUPPORTED/SKIP, not passed.

Usage:
  python tools/validate_v3_content.py FILE [FILE...]   # content files
  python tools/validate_v3_content.py --bundle BUNDLE.json
Exit: 0 all checks pass (SKIPs allowed), 1 validation failures,
      2 tool/shape error.
"""

import json
import os
import re
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

ADULT_AGE = 18

# B's PROPOSED interaction kinds (proposal until C confirms + freeze).
INTERACTION_KINDS = {
    "flirt", "date", "propose_courtship", "propose_union",
    "propose_plural_union", "convert_request", "repair_attempt",
    "parenting_request",  # nonromantic
}
ROMANTIC_KINDS = INTERACTION_KINDS - {"parenting_request"}

ROLE_KINDS = {"person", "relationship", "union", "household"}
SELECT_KINDS = {"player", "predicate"}

# v2.1 condition clauses + C's v3 leaves (event-schema-v3.md §2).
V2_CLAUSE_KINDS = {
    "alive", "no_partner", "has_illness", "employed", "in_school",
    "min_stat", "min_money", "flag", "partner_status", "min_age_sampled",
    "max_age_sampled", "partner_relationship_below", "karma_gate",
}
V3_CLAUSE_KINDS = V2_CLAUSE_KINDS | {
    "relationship_state", "union_state", "expectation", "recognition",
    "tenet_stance", "faith", "jurisdiction", "attraction_band",
    "known_fact", "reputation_at_least", "kinship", "milestone", "bound",
}

# v2 Effects keys + C's v3 typed domain effects (event-schema-v3.md §4).
V2_EFFECT_KEYS = {
    "stats", "money", "relationship", "add_illness", "remove_illness",
    "learn_trait", "lose_trait", "set_flag", "lose_partner", "divorce",
    "gain_partner", "partner_death", "terminal",
}
V3_EFFECT_KEYS = V2_EFFECT_KEYS | {
    "offer", "respond", "end_relationship", "end_union", "reveal_fact",
    "reputation", "chemistry", "convert_profile", "set_personal_tenet",
    "reproject_recognition", "milestone", "sample_conception",
}
RAW_SETTER_KEYS = {"set_partner", "set_consent", "set_age", "set_money",
                   "set_relationship"}
# role-bearing fields inside v3 typed effects (values must be declared
# roles, or lists of declared roles).
EFFECT_ROLE_FIELDS = {
    "offer": ("to",), "respond": ("offer_role",),
    "end_relationship": ("between",), "end_union": ("union_role",),
    "reveal_fact": ("fact_role", "to"), "reputation": ("person",),
    "chemistry": ("between",), "convert_profile": ("person",),
    "set_personal_tenet": ("person",),
    "reproject_recognition": (), "milestone": ("child",),
    "sample_conception": ("mother", "father"),
}

V2_CATEGORIES = {"childhood", "teen", "adult", "elder", "health",
                 "family", "work", "finance"}
AUDIENCE_SCOPES = {"public", "player_known", "private"}


def _fail(errors, path, rule, msg):
    errors.append("%s [%s] %s" % (path, rule, msg))


def _load_manifest_looks():
    try:
        from src.appearance import render as avatar_render
        with open(avatar_render.MANIFEST_PATH, encoding="utf-8") as f:
            combos = json.load(f).get("combos", {})
        return {k.split(":", 1)[1] for k in combos
                if k.startswith("look:")}
    except Exception:
        return None  # UNSUPPORTED


def _check_v2_constraints(ev, path, errors):
    """The six frozen v2.1 constraints (embedded summaries)."""
    auto = bool(ev.get("auto"))
    choices = ev.get("choices") or []
    if auto:
        if choices:
            _fail(errors, path, "v2.auto",
                  "auto events must not carry choices")
        return
    free = [c for c in choices
            if (c.get("cost") or {}).get("money", 0) == 0]
    if not free:
        _fail(errors, path, "v2.softlock",
              "no zero-cost choice (free-exit rule)")
    if len(choices) < 2:
        _fail(errors, path, "v2.choices",
              "non-auto events need at least 2 choices")


def _check_clause(clause, cpath, errors, kinds):
    if not isinstance(clause, dict):
        _fail(errors, cpath, "cond.clause", "clause must be an object")
        return
    if clause.get("kind") not in kinds:
        _fail(errors, cpath, "cond.kind",
              "unknown condition kind %r" % clause.get("kind"))


def _check_conditions(cond, path, errors, schema_v3):
    if not isinstance(cond, dict):
        _fail(errors, path, "cond.shape", "conditions must be an object")
        return
    if "min_age" in cond and not isinstance(cond["min_age"], int):
        _fail(errors, path, "cond.min_age", "min_age must be an int")
    kinds = V3_CLAUSE_KINDS if schema_v3 else V2_CLAUSE_KINDS
    for i, clause in enumerate(cond.get("requires") or []):
        _check_clause(clause, "%s.requires[%d]" % (path, i), errors, kinds)
    if schema_v3:
        _check_v3_expr(cond, path, errors, depth=0)


def _check_v3_expr(node, path, errors, depth):
    """v3 combinator vocabulary: all/any/not, bounded depth."""
    if depth > 4:
        _fail(errors, path, "v3.cond.depth",
              "condition combinator depth exceeds 4")
        return
    if not isinstance(node, dict):
        return
    for comb in ("all", "any"):
        if comb in node:
            if not isinstance(node[comb], list):
                _fail(errors, path, "v3.cond.%s" % comb,
                      "%s must be a list" % comb)
            else:
                for i, sub in enumerate(node[comb]):
                    if isinstance(sub, dict) and "kind" in sub:
                        _check_clause(sub, "%s.%s[%d]"
                                      % (path, comb, i), errors,
                                      V3_CLAUSE_KINDS)
                    else:
                        _check_v3_expr(sub, "%s.%s[%d]" % (path, comb, i),
                                       errors, depth + 1)
    if "not" in node:
        sub = node["not"]
        if isinstance(sub, dict) and "kind" in sub:
            _check_clause(sub, path + ".not", errors, V3_CLAUSE_KINDS)
        else:
            _check_v3_expr(sub, path + ".not", errors, depth + 1)


def _check_effects(effects, roles, path, errors, schema_v3):
    if not isinstance(effects, dict):
        _fail(errors, path, "fx.shape", "effects must be an object")
        return
    for key in effects:
        if key in RAW_SETTER_KEYS:
            _fail(errors, path, "fx.raw",
                  "raw setter %r is forbidden (use typed domain effects)"
                  % key)
        elif key not in (V3_EFFECT_KEYS if schema_v3 else V2_EFFECT_KEYS):
            _fail(errors, path, "fx.key", "unknown effect key %r" % key)
    if not schema_v3:
        return
    for key, fields in EFFECT_ROLE_FIELDS.items():
        fx = effects.get(key)
        if fx is None:
            continue
        if not isinstance(fx, dict):
            _fail(errors, path, "v3.fx.shape",
                  "%s must be an object" % key)
            continue
        for field in fields:
            value = fx.get(field)
            values = value if isinstance(value, list) else [value]
            for v in values:
                if v is not None and v not in roles:
                    _fail(errors, path, "v3.fx.role",
                          "%s.%s references undeclared role %r"
                          % (key, field, v))


def _check_choice(choice, roles, path, errors, schema_v3):
    if not isinstance(choice.get("id"), str) or not choice.get("id"):
        _fail(errors, path, "choice.id", "choice needs a stable id")
    cost_block = choice.get("cost") or {}
    if "money" in cost_block and (not isinstance(cost_block["money"], int)
                                  or isinstance(cost_block["money"], bool)
                                  or cost_block["money"] < 0):
        _fail(errors, path, "v2.cost",
              "cost.money must be a non-negative int (gate only)")
    fx = choice.get("effects") or {}
    outcomes = choice.get("outcomes") or []
    chances = [o.get("chance") for o in outcomes
               if isinstance(o.get("chance"), (int, float))]
    if outcomes and len(chances) == len(outcomes) \
            and abs(sum(chances) - 1.0) > 1e-6:
        _fail(errors, path, "v2.outcomes",
              "outcome chances must sum to 1.0, got %s" % sum(chances))
    _check_effects(fx, roles, path + ".effects", errors, schema_v3)
    for i, o in enumerate(outcomes):
        if isinstance(o, dict) and "effects" in o:
            _check_effects(o["effects"], roles,
                           "%s.outcomes[%d].effects" % (path, i),
                           errors, schema_v3)


def _check_roles(ev, path, errors):
    """v3 §1: roles object; each role declared once; valid kind/select."""
    roles = ev.get("roles") or {}
    declared = set()
    if not isinstance(roles, dict):
        _fail(errors, path, "v3.roles.shape", "roles must be an object")
        return set()
    for name, spec in roles.items():
        rpath = "%s.roles.%s" % (path, name)
        declared.add(name)
        if not isinstance(spec, dict):
            _fail(errors, rpath, "v3.roles.spec", "role spec must object")
            continue
        if spec.get("kind") not in ROLE_KINDS:
            _fail(errors, rpath, "v3.roles.kind",
                  "kind must be person|relationship|union|household")
        if spec.get("select") not in SELECT_KINDS:
            _fail(errors, rpath, "v3.roles.select",
                  "select must be player|predicate")
        if spec.get("select") == "predicate" and \
                not isinstance(spec.get("predicate"), str):
            _fail(errors, rpath, "v3.roles.predicate",
                  "predicate selection needs a predicate name")
    return declared


def _check_v3_event(ev, path, errors, manifest_looks):
    interaction = ev.get("interaction")
    romantic = False
    if interaction is not None:
        if not isinstance(interaction, dict):
            _fail(errors, path, "v3.interaction.shape",
                  "interaction must be an object")
        else:
            kind = interaction.get("kind")
            if kind not in INTERACTION_KINDS:
                _fail(errors, path, "v3.interaction.kind",
                      "unknown interaction kind %r" % kind)
            romantic = bool(interaction.get("romantic")) and \
                kind in ROMANTIC_KINDS

    min_age = (ev.get("conditions") or {}).get("min_age")
    if romantic and isinstance(min_age, int) and min_age < ADULT_AGE:
        _fail(errors, path, "v3.adult_gate",
              "romantic event proposes min_age %s (< %s): must fail "
              "schema validation" % (min_age, ADULT_AGE))
    if ev.get("auto") and romantic:
        _fail(errors, path, "v3.auto_romance",
              "auto events cannot create consent or force unions")

    roles = _check_roles(ev, path, errors)

    audience = ev.get("audience")
    if audience is not None:
        if not isinstance(audience, dict) or \
                audience.get("scope") not in AUDIENCE_SCOPES:
            _fail(errors, path, "v3.audience.scope",
                  "audience.scope must be public|player_known|private")
        else:
            for p in audience.get("participants") or []:
                if p not in roles:
                    _fail(errors, path, "v3.audience.participant",
                          "audience names undeclared role %r" % p)

    text_fields = [ev.get("title", ""), ev.get("text", "")]
    for c in ev.get("choices") or []:
        text_fields += [c.get("label", ""), c.get("outcome_text") or ""]
        for o in c.get("outcomes") or []:
            if isinstance(o, dict):
                text_fields.append(o.get("outcome_text") or "")
    v2_placeholder_roles = {"mother", "father", "partner", "child",
                            "sibling", "all_parents", "random_parent",
                            "random_relation"}
    for tf in text_fields:
        for ph in re.findall(r"\{([^{}]+)\}", str(tf)):
            name = ph.split(":", 1)[0]
            if name == "name":
                continue
            if name == "person":
                role = ph.split(":", 1)[1] if ":" in ph else ""
                if role.split("|")[0] not in roles \
                        and role not in v2_placeholder_roles:
                    _fail(errors, path, "v3.template",
                          "template references undeclared role %r" % role)
                continue
            if name not in roles:
                _fail(errors, path, "v3.template",
                      "template references undeclared role %r" % name)

    look = ev.get("look_override")
    if look and manifest_looks is not None and look != "none" \
            and look not in manifest_looks:
        _fail(errors, path, "v3.asset",
              "look_override %r not in M0b manifest" % look)
    elif look and manifest_looks is None:
        _fail(errors, path + ".UNSUPPORTED", "v3.asset",
              "manifest unavailable; asset check not run")

    _check_effects(ev.get("effects") or {}, roles, path + ".effects",
                   errors, True)
    if ev.get("auto"):
        auto_fx = ev.get("effects") or {}
        for key in ("offer", "respond", "end_union", "sample_conception"):
            if key in auto_fx:
                _fail(errors, path, "v3.auto_fx",
                      "auto events cannot carry %r (consent/union effects)"
                      % key)


def validate_file(path, errors):
    with open(path, encoding="utf-8") as f:
        data = json.load(f)
    if not isinstance(data, dict) or not isinstance(data.get("events"),
                                                    list):
        _fail(errors, path, "shape", "file must be {schema_version, events[]}")
        return
    version = data.get("schema_version")
    if version not in (2, 3):
        _fail(errors, path, "version",
              "unsupported schema_version %r (expected 2 or 3)" % version)
        return
    manifest_looks = _load_manifest_looks()
    seen_ids = set()
    for i, ev in enumerate(data["events"]):
        epath = "%s:events[%d]" % (path, i)
        if not isinstance(ev, dict):
            _fail(errors, epath, "shape", "event must be an object")
            continue
        if not isinstance(ev.get("id"), str):
            _fail(errors, epath, "id", "event needs a stable string id")
            continue
        if ev["id"] in seen_ids:
            _fail(errors, epath, "id", "duplicate event id %r" % ev["id"])
            continue
        seen_ids.add(ev["id"])
        if not ev.get("title") or not ev.get("text"):
            _fail(errors, epath, "shape", "event needs title and text")
        if version == 2 and ev.get("category") not in V2_CATEGORIES:
            _fail(errors, epath, "v2.category",
                  "category %r not in frozen v2 set" % ev.get("category"))
        if not isinstance(ev.get("weight", 1), int) or \
                ev.get("weight", 1) < 1:
            _fail(errors, epath, "v2.weight", "weight must be int >= 1")
        _check_conditions(ev.get("conditions") or {}, epath, errors,
                          version == 3)
        _check_v2_constraints(ev, epath, errors)
        for j, choice in enumerate(ev.get("choices") or []):
            _check_choice(choice, set((ev.get("roles") or {}).keys()),
                          "%s.choices[%d]" % (epath, j), errors,
                          version == 3)
        if version == 3:
            _check_v3_event(ev, epath, errors, manifest_looks)


# ------------------------------------------------------------------ bundle

def _validate_event_obj(data, label):
    """Validate an {schema_version, events[]} object; return error list."""
    errors = []
    tmp = label
    if not isinstance(data, dict) or not isinstance(data.get("events"),
                                                    list):
        return ["%s [shape] bundle record must be an event file object"
                % label]
    if data.get("schema_version") not in (2, 3):
        return ["%s [version] unsupported schema_version %r"
                % (label, data.get("schema_version"))]
    import tempfile
    with tempfile.NamedTemporaryFile(
            "w", suffix=".json", delete=False, encoding="utf-8") as f:
        json.dump(data, f)
        tmp = f.name
    try:
        validate_file(tmp, errors)
    finally:
        os.unlink(tmp)
    # re-label temp paths with the fixture name
    return [e.replace(tmp, label) for e in errors]


def _keyword_match(expected_error, violations):
    """A fixture's expected.error prose is considered covered when at
    least one violation shares a salient token with it."""
    tokens = {t.lower() for t in re.findall(r"[a-z_]{4,}", expected_error)}
    stop = {"with", "which", "that", "must", "rule", "fixed",
            "content", "make", "minor", "eligib", "player", "schema",
            "valid", "have"}
    tokens -= stop
    for v in violations:
        vlow = v.lower()
        if any(t in vlow for t in tokens):
            return True
    return False


def run_bundle(path):
    with open(path, encoding="utf-8") as f:
        bundle = json.load(f)
    failures = []
    skips = []
    passed = 0

    for rec in bundle.get("valid_events") or []:
        errors = _validate_event_obj(rec.get("event"), rec.get("_name",
                                                                "?"))
        if errors:
            failures += errors
            for e in errors:
                print("FAIL valid/%s: %s" % (rec.get("_name"), e))
        else:
            passed += 1
            print("PASS valid/%s" % rec.get("_name"))

    for rec in bundle.get("invalid_events") or []:
        name = rec.get("_name")
        event = rec.get("event")
        expected = rec.get("expected") or {}
        if event is None:
            # resolve-level record (e.g. missing-edge safe cancellation):
            # domain behavior, not content shape — C's loader owns it.
            skips.append("invalid/%s: resolve-domain expectation (%s); "
                         "needs C's loader" % (name, expected.get("resolve")))
            print("SKIP invalid/%s: resolve-domain expectation, loader-owned"
                  % name)
            continue
        errors = _validate_event_obj(event, name)
        if not errors:
            failures.append("%s [bundle] invalid fixture PASSED validation "
                            "(expected fail: %s)" % (name, expected))
            print("FAIL invalid/%s: passed, expected fail (%s)"
                  % (name, expected.get("error")))
            continue
        if expected.get("error") and \
                not _keyword_match(expected["error"], errors):
            failures.append("%s [bundle] failed but not for the stated "
                            "reason: %r; got: %s"
                            % (name, expected["error"], errors[0]))
            print("FAIL invalid/%s: wrong reason. expected %r; got %s"
                  % (name, expected["error"], "; ".join(errors)))
            continue
        passed += 1
        print("PASS invalid/%s (rejected: %s)" % (name, errors[0].split(
            " [", 1)[-1][:80]))

    for rec in bundle.get("v2_compatibility_fixtures") or []:
        name = rec.get("_name")
        record = rec.get("record") or {}
        pending = record.get("pending")
        if pending is not None:
            problems = []
            if pending.get("schema_dialect") != "v2":
                problems.append("schema_dialect != v2")
            if not isinstance(pending.get("options"), list) \
                    or not pending["options"]:
                problems.append("pending needs non-empty options")
            for key in ("kind",):
                if not isinstance(pending.get(key), str):
                    problems.append("pending.%s must be a string" % key)
            if problems:
                failures += ["%s: %s" % (name, p) for p in problems]
                for p in problems:
                    print("FAIL v2compat/%s: %s" % (name, p))
            else:
                passed += 1
                print("PASS v2compat/%s (record shape; resolve semantics "
                      "are loader-owned: %s)" % (name, (rec.get("expected")
                                                        or {}).get("resolve")))
        elif record.get("files"):
            missing = [p for p in record["files"]
                       if not os.path.isfile(os.path.join(ROOT, p))]
            if missing:
                skips.append("v2compat/%s: files not present yet: %s"
                             % (name, missing))
                print("SKIP v2compat/%s: %d referenced file(s) not present "
                      "yet" % (name, len(missing)))
            else:
                passed += 1
                print("PASS v2compat/%s" % name)
        else:
            skips.append("v2compat/%s: unrecognised record shape" % name)
            print("SKIP v2compat/%s: unrecognised record shape" % name)

    for rec in bundle.get("knowledge_fixtures") or []:
        skips.append("knowledge/%s: knowledge-layer semantics owned by C "
                     "(reputation-knowledge.md)" % rec.get("_name"))
        print("SKIP knowledge/%s: knowledge-layer semantics are C's domain"
              % rec.get("_name"))

    print("---\nbundle: %d passed, %d failed, %d skipped" % (passed, len(failures), len(skips)))
    return 1 if failures else 0


def main(argv):
    args = [a for a in argv[1:]]
    if not args:
        print(__doc__)
        return 2
    if args[0] == "--bundle":
        if len(args) != 2:
            print("usage: validate_v3_content.py --bundle BUNDLE.json")
            return 2
        try:
            return run_bundle(args[1])
        except (OSError, ValueError) as e:
            print("FAIL bundle unreadable: %s" % e)
            return 2
    errors = []
    for path in args:
        try:
            validate_file(path, errors)
        except (OSError, ValueError) as e:
            _fail(errors, path, "tool", "cannot read/parse: %s" % e)
    unsupported = [e for e in errors if ".UNSUPPORTED" in e.split(" [")[0]]
    for e in errors:
        print("FAIL %s" % e)
    if unsupported:
        print("NOTE: %d check(s) UNSUPPORTED (draft source missing); they "
              "did not pass, they were skipped." % len(unsupported))
    if errors:
        print("%d violation(s)" % len(errors))
        return 1
    print("OK: %d file(s) valid" % len(args))
    return 0


if __name__ == "__main__":
    sys.exit(main(sys.argv))
