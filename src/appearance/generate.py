"""Appearance generation: random, inherited, and age-appropriate rules.

Uses only the rng passed in (a random.Random owned by the caller) — never
touches the global `random` module, so cosmetics never affect gameplay RNG.
"""

import random

from . import catalog
from .model import Appearance

NON_GRAY_HAIR_COLORS = [
    e["id"] for e in catalog.HAIR_COLORS
    if e["id"] not in ("gray", "white")
]

# Rough colour groups for inheritance blending (index = colour ID).
_HAIR_GROUP = {
    "black": "dark", "blue_black": "dark", "brown_dark": "brown",
    "brown_medium": "brown", "chestnut": "brown", "brown_light": "brown",
    "blonde_dark": "blonde", "blonde": "blonde", "blonde_platinum": "blonde",
    "red": "red", "auburn": "red",
}
_GROUP_MEMBERS = {
    "dark": ["black", "blue_black", "black", "brown_dark"],
    "brown": ["brown_dark", "brown_medium", "chestnut", "brown_light"],
    "blonde": ["blonde_dark", "blonde", "blonde_platinum", "brown_light"],
    "red": ["red", "auburn", "chestnut"],
}


def _pick(rng, category):
    return rng.choice(catalog.all_ids(category))


def _pick_weighted(rng, pairs):
    ids = [p[0] for p in pairs]
    weights = [p[1] for p in pairs]
    return rng.choices(ids, weights=weights, k=1)[0]


def random_appearance(rng, age, gender=None):
    """Fully independent random appearance, age-appropriate."""
    if not isinstance(rng, random.Random):
        raise TypeError("random_appearance requires a random.Random instance")
    app = Appearance(
        seed=rng.randrange(1, 2**31),
        skin_tone=_pick(rng, "skin_tone"),
        face_shape=_pick(rng, "face_shape"),
        eye_shape=_pick(rng, "eye_shape"),
        eye_color=_pick(rng, "eye_color"),
        brows=_pick(rng, "brows"),
        nose=_pick(rng, "nose"),
        mouth=_pick(rng, "mouth"),
        hair_texture=_pick(rng, "hair_texture"),
        hair_color=_pick(rng, "hair_color"),
        marks=_random_marks(rng),
        hair_style=_pick(rng, "hair_style"),
        facial_hair=_pick(rng, "facial_hair"),
        accessories={
            "glasses": _pick(rng, "glasses"),
            "headwear": _pick(rng, "headwear"),
            "earrings": _pick(rng, "earrings"),
        },
        outfit_style=_pick(rng, "outfit_style"),
        outfit_color=_pick(rng, "outfit_color"),
    )
    age_appropriate(app, age, gender)
    return app


def _random_marks(rng):
    marks = []
    for mark in catalog.all_ids("marks"):
        if rng.random() < 0.12:
            marks.append(mark)
    return marks


def inherit_appearance(rng, mother, father, age=0):
    """Inherit physical features from biological parents with small variation.

    Either parent may be None (unknown/independent side). Styling is never
    inherited — it is randomized fresh, then made appropriate for `age`
    (newborns by default; pass the real age when migrating older people).
    """
    if not isinstance(rng, random.Random):
        raise TypeError("inherit_appearance requires a random.Random instance")
    parents = [p for p in (mother, father) if p is not None]
    seed = rng.randrange(1, 2**31)

    def inherit_field(field):
        if not parents:
            return _pick(rng, field)
        return getattr(rng.choice(parents), field)

    app = Appearance(
        seed=seed,
        skin_tone=inherit_field("skin_tone"),
        face_shape=inherit_field("face_shape"),
        eye_shape=inherit_field("eye_shape"),
        eye_color=inherit_field("eye_color"),
        brows=inherit_field("brows"),
        nose=inherit_field("nose"),
        mouth=inherit_field("mouth"),
        hair_texture=inherit_field("hair_texture"),
        hair_color=_inherit_hair_color(rng, parents),
        marks=_inherit_marks(rng, parents),
        hair_style=_pick(rng, "hair_style"),
        facial_hair="none",
        accessories={"glasses": "none", "headwear": "none", "earrings": "none"},
        outfit_style=_pick(rng, "outfit_style"),
        outfit_color=_pick(rng, "outfit_color"),
    )
    # small variation: chance to mutate one facial feature
    if parents and rng.random() < 0.35:
        field = rng.choice(
            ["face_shape", "eye_shape", "brows", "nose", "mouth"])
        setattr(app, field, _pick(rng, field))
    age_appropriate(app, age)
    return app


def _inherit_hair_color(rng, parents):
    colors = [
        p.hair_color for p in parents
        if p.hair_color in NON_GRAY_HAIR_COLORS
    ]
    if not colors:
        return _pick(rng, "hair_color")
    if len(colors) == 1:
        base = colors[0]
    else:
        base = colors[0] if rng.random() < 0.5 else colors[1]
    if rng.random() < 0.15:
        # mutation: jump to a neighbouring colour group
        group = _HAIR_GROUP.get(base, "brown")
        pool = [c for g in _GROUP_MEMBERS.values() for c in g]
        return rng.choice(pool)
    return base


def _inherit_marks(rng, parents):
    pool = []
    for p in parents:
        pool.extend(m for m in p.marks if m in catalog.all_ids("marks"))
    # sorted(set(...)) keeps consumption order stable across processes
    # (PYTHONHASHSEED would otherwise change which mark gets which draw)
    inherited = [m for m in sorted(set(pool)) if rng.random() < 0.25]
    if rng.random() < 0.08:
        mark = rng.choice(catalog.all_ids("marks"))
        if mark not in inherited:
            inherited.append(mark)
    return inherited


def age_appropriate(appearance, age, gender=None):
    """Enforce accessory/style rules for the age stage, in place.

    Babies: no accessories, bald/buzz or minimal hair, no facial hair.
    Children: no facial hair, no earrings.
    Teens/adults/elder: only glasses may persist for elders etc. — no
    extra restriction, accessories spread naturally.
    """
    from .aging import age_stage  # local import to avoid cycle
    stage = age_stage(age)
    if stage == "baby":
        appearance.facial_hair = "none"
        appearance.accessories = {
            "glasses": "none", "headwear": "none", "earrings": "none"}
        if appearance.hair_style not in ("bald", "buzz"):
            appearance.hair_style = "buzz"
        appearance.marks = [m for m in appearance.marks
                            if m in catalog.all_ids("marks")]
    elif stage == "child":
        appearance.facial_hair = "none"
        appearance.accessories["earrings"] = "none"
        if appearance.hair_style == "bald":
            appearance.hair_style = "buzz"
    return appearance
