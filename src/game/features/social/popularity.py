"""Popularity: deterministic yearly recompute from stored state only
(no rng on reads), frozen formula + band mapping (data, not code —
see popularity_bands.json and the drift-threshold freeze in
02-contracts-A.md)."""

import json
import os

_BANDS = None


def bands():
    global _BANDS
    if _BANDS is None:
        path = os.path.join(os.path.dirname(__file__),
                            "popularity_bands.json")
        with open(path, encoding="utf-8") as f:
            _BANDS = json.load(f)["bands"]
    return _BANDS


def band_for(score):
    for band in bands():
        if band["lo"] <= score <= band["hi"]:
            return band
    return bands()[0]


def _phase(age):
    if age < 13:
        return "child"
    if age < 18:
        return "teen"
    return "adult"


def recompute(env, player):
    """Frozen formula: base 15 (a life with no contacts at all stays
    'unknown' below the known band); +3 per active friend; +5
    best_friend; +1 per 2 current classmates (cap 6); +1 per 2 current
    coworkers (cap 4); + each joined club's popularity_delta (C data);
    -4 per active rival; -8 per active bullying arc. Clamped 0-100."""
    from src.game.features.social import circles as circles_mod
    from src.game.features.social import state as social
    st = social.ensure(env)
    score = 15
    friends = best = rivals = 0
    for rec in st["people"].values():
        if rec["status"] != "active":
            continue
        if rec["kind"] == "friend":
            friends += 1
        elif rec["kind"] == "best_friend":
            best += 1
        elif rec["kind"] == "rival":
            rivals += 1
    score += 3 * friends + 5 * best - 4 * rivals
    score -= 8 * len([a for a in st["arcs"].values()
                      if a.get("stage") in ("targeted", "escalated")])
    score += min(6, len(circles_mod.group_member_ids(env,
                                                     "classmates")) // 2)
    score += min(4, len(circles_mod.group_member_ids(env,
                                                     "coworkers")) // 2)
    clubs = social.load_clubs()
    for club_id in st.get("circles", {}).get("clubs", {}):
        club = clubs.get(club_id)
        if club:
            score += int(club.get("popularity_delta", 0))
    pop = st["popularity"]
    score += pop.get("bonus", 0)
    pop["last_score"] = pop["score"]
    pop["score"] = max(0, min(100, score))
    pop["phase"] = _phase(player.age)
    pop["history"].append({"turn": env.get("turn", {}).get(
        "turn_id") or f"rev{env['revision']}", "score": pop["score"]})
    del pop["history"][:-social.POPULARITY_HISTORY_CAP]
    return pop
