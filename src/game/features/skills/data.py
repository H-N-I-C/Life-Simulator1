"""Versioned skills content: load + validate (src/game/content/skills)."""

import json
import os

DATA_PATH = os.path.join(os.path.dirname(__file__), "..", "..", "content",
                         "skills", "skills.json")

_cache = None


class SkillsDataError(ValueError):
    pass


def load(reload=False):
    global _cache
    if _cache is not None and not reload:
        return _cache
    with open(DATA_PATH, encoding="utf-8") as f:
        raw = json.load(f)
    _validate(raw)
    _cache = raw
    return raw


def _validate(d):
    if not isinstance(d, dict) or d.get("schema_version") != 1:
        raise SkillsDataError("skills data: schema_version must be 1")
    skills = d.get("skills")
    options = d.get("training_options")
    rules = d.get("rules")
    if not isinstance(skills, list) or not skills:
        raise SkillsDataError("skills data: skills must be a non-empty list")
    if not isinstance(options, list) or not options:
        raise SkillsDataError("skills data: training_options required")
    if not isinstance(rules, dict):
        raise SkillsDataError("skills data: rules required")
    ids = set()
    for s in skills:
        for key in ("id", "label"):
            if not isinstance(s.get(key), str) or not s[key]:
                raise SkillsDataError("skill %r: %s required" % (s, key))
        if s["id"] in ids:
            raise SkillsDataError("duplicate skill id %r" % s["id"])
        ids.add(s["id"])
        if s.get("min", 0) != 0 or s.get("max") != 100:
            raise SkillsDataError("skill %r: range must be 0-100" % s["id"])
        default = s.get("default")
        if not isinstance(default, int) or not 0 <= default <= 100:
            raise SkillsDataError("skill %r: default must be int 0-100"
                                  % s["id"])
    skill_ids = ids
    opt_ids = set()
    for o in options:
        oid = o.get("id")
        if not isinstance(oid, str) or not oid:
            raise SkillsDataError("training option missing id: %r" % (o,))
        if oid in opt_ids:
            raise SkillsDataError("duplicate training option id %r" % oid)
        opt_ids.add(oid)
        skill = o.get("skill")
        if skill is not None and skill not in skill_ids:
            raise SkillsDataError("option %r: unknown skill %r" % (oid, skill))
        if o.get("choose_skill") and skill is not None:
            raise SkillsDataError("option %r: choose_skill needs skill null"
                                  % oid)
        cost = o.get("cost") or {}
        if not isinstance(cost.get("money", 0), int) or cost.get("money", 0) < 0:
            raise SkillsDataError("option %r: cost.money must be int >= 0"
                                  % oid)
        limit = o.get("annual_limit")
        if not isinstance(limit, int) or limit < 1:
            raise SkillsDataError("option %r: annual_limit must be int >= 1"
                                  % oid)
        prog = o.get("progress") or {}
        lo, hi = prog.get("min"), prog.get("max")
        if not (isinstance(lo, int) and isinstance(hi, int)
                and 0 < lo <= hi <= 100):
            raise SkillsDataError("option %r: progress.min/max invalid"
                                  % oid)
        req = o.get("requires") or {}
        if "min_age" in req and (not isinstance(req["min_age"], int)
                                 or req["min_age"] < 0):
            raise SkillsDataError("option %r: requires.min_age invalid" % oid)
    bonus = (rules.get("smarts_learning_bonus") or {})
    if bonus.get("per_10_smarts", 0) < 0 or bonus.get("cap", 0) < 0:
        raise SkillsDataError("rules.smarts_learning_bonus invalid")
    for key in ("job_practice_per_year", "study_contribution_per_year"):
        r = rules.get(key) or {}
        if not (isinstance(r.get("min"), int) and isinstance(r.get("max"), int)
                and 0 < r["min"] <= r["max"] <= 100):
            raise SkillsDataError("rules.%s invalid" % key)


def skill_ids():
    return [s["id"] for s in load()["skills"]]


def training_options():
    return load()["training_options"]


def option(option_id):
    for o in load()["training_options"]:
        if o["id"] == option_id:
            return o
    return None


def skill_def(skill_id):
    for s in load()["skills"]:
        if s["id"] == skill_id:
            return s
    return None
