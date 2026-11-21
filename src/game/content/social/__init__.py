"""M4 social content data (Kimi C): clubs.json + bullying.json loaders.

Data only — every load validates (content errors are loud, never
silent). Membership/arc/popularity state is A's `env["social_state"]`;
these loaders never touch an envelope.
"""

import json
import os

DATA_DIR = os.path.dirname(os.path.abspath(__file__))

_cache = {}


class SocialDataError(ValueError):
    pass


def _load(name, validate):
    if name in _cache:
        return _cache[name]
    with open(os.path.join(DATA_DIR, "%s.json" % name),
              encoding="utf-8") as f:
        raw = json.load(f)
    validate(raw)
    _cache[name] = raw
    return raw


def load_clubs(reload=False):
    if reload:
        _cache.pop("clubs", None)
    return _load("clubs", _validate_clubs)


def _validate_clubs(d):
    if not isinstance(d, dict) or d.get("schema_version") != 1:
        raise SocialDataError("clubs data: schema_version must be 1")
    clubs = d.get("clubs")
    if not isinstance(clubs, list) or not clubs:
        raise SocialDataError("clubs data: clubs must be a non-empty list")
    ids = set()
    categories = {"school", "sport", "arts", "social"}
    for c in clubs:
        cid = c.get("id")
        if not isinstance(cid, str) or not cid:
            raise SocialDataError("club missing id: %r" % (c,))
        if cid in ids:
            raise SocialDataError("duplicate club id %r" % cid)
        ids.add(cid)
        for key in ("label", "category", "meets_label"):
            if not isinstance(c.get(key), str) or not c[key]:
                raise SocialDataError("club %r: %s required" % (cid, key))
        if c["category"] not in categories:
            raise SocialDataError("club %r: bad category %r"
                                  % (cid, c["category"]))
        if not isinstance(c.get("min_age"), int) or not isinstance(
                c.get("max_age"), int) or c["min_age"] > c["max_age"]:
            raise SocialDataError("club %r: age band invalid" % cid)
        cost = c.get("cost_per_year")
        if not isinstance(cost, int) or cost < 0:
            raise SocialDataError("club %r: cost_per_year must be int >= 0"
                                  % cid)
        for key in ("popularity_delta", "member_cap"):
            if not isinstance(c.get(key), int) or c[key] < 0:
                raise SocialDataError("club %r: %s invalid" % (cid, key))
        if c.get("adults_only") and c["min_age"] < 18:
            raise SocialDataError("club %r: adults_only needs min_age 18"
                                  % cid)
        if c["min_age"] >= 18 and not c.get("adults_only"):
            raise SocialDataError(
                "club %r: min_age >= 18 requires adults_only (Amendment 1)"
                % cid)
        if c.get("school"):
            stages = c["school"].get("stages")
            if not isinstance(stages, list) or not stages or any(
                    s not in ("elementary", "middle", "high")
                    for s in stages):
                raise SocialDataError("club %r: bad school stages" % cid)


def club(club_id):
    for c in load_clubs()["clubs"]:
        if c["id"] == club_id:
            return c
    return None


def load_bullying(reload=False):
    if reload:
        _cache.pop("bullying", None)
    return _load("bullying", _validate_bullying)


def _validate_bullying(d):
    if not isinstance(d, dict) or d.get("schema_version") != 1:
        raise SocialDataError("bullying data: schema_version must be 1")
    stages = d.get("stages")
    if (not isinstance(stages, list) or stages != [
            "targeted", "escalated", "resolving", "ended"]):
        raise SocialDataError("bullying data: stages must be the frozen "
                              "four-stage list")
    responses = d.get("responses")
    want = {"tell_someone", "stand_up", "avoid", "befriend"}
    if not isinstance(responses, dict) or set(responses) != want:
        raise SocialDataError("bullying data: responses must be exactly "
                              "%s" % sorted(want))
    for name, r in responses.items():
        if r.get("to_stage") is not None and r["to_stage"] not in stages:
            raise SocialDataError("bullying response %r: bad to_stage"
                                  % name)
        if r.get("fail_to_stage") and r["fail_to_stage"] not in stages:
            raise SocialDataError("bullying response %r: bad "
                                  "fail_to_stage" % name)
