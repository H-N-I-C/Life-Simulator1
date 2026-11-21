"""Appearance generation v2: random, inherited, and age-appropriate rules.

Uses only the rng passed in (a random.Random owned by the caller) — never
touches the global `random` module, so cosmetics never affect gameplay RNG.
"""

import random

from . import catalog
from .model import Appearance

_SKIN_ORDER = catalog.all_ids("skin_tone")


def _pick(rng, category):
    return rng.choice(catalog.all_ids(category))


def random_appearance(rng, age, gender=None):
    """Fully independent random appearance, age-appropriate.

    `base` defaults from gameplay gender ('person' when unknown).
    Accessories are gender/age-fitting (§Avatar defaults, contracts.md):
    beards only on man-based adults (25%), headscarf only on woman-based
    adults (15%) — never a bearded woman or a hooded man.
    """
    if not isinstance(rng, random.Random):
        raise TypeError("random_appearance requires a random.Random instance")
    base = _default_base(gender)
    facial_hair, headwear = _gendered_accessories(rng, base, age)
    app = Appearance(
        seed=rng.randrange(1, 2**31),
        base=base,
        skin_tone=_pick(rng, "skin_tone"),
        hair=_pick(rng, "hair"),
        facial_hair=facial_hair,
        headwear=headwear,
        badges=_random_badges(rng),
        background=_pick(rng, "background"),
        look_override="none",
    )
    age_appropriate(app, age, gender)
    return app


def _gendered_accessories(rng, base, age):
    """§Avatar defaults: beard = man-based adult only (25%); headscarf =
    woman-based adult only (15%); everyone else none."""
    from .aging import age_stage
    adult = age_stage(age) not in ("baby", "child")
    if not adult:
        return "none", "none"
    facial_hair = "beard" if base == "man" and rng.random() < 0.25 \
        else "none"
    headwear = "headscarf" if base == "woman" and rng.random() < 0.15 \
        else "none"
    return facial_hair, headwear


def _default_base(gender):
    from src.lifesim_lib.lifesim_lib import Gender
    if gender == Gender.Male:
        return "man"
    if gender == Gender.Female:
        return "woman"
    return "person"


def _random_badges(rng):
    badges = []
    for badge in catalog.all_ids("badges"):
        if rng.random() < 0.12:
            badges.append(badge)
    return badges[:catalog.BADGE_LIMIT]


def inherit_appearance(rng, mother, father, age=0):
    """Inherit v2 traits from biological parents with small variation.

    Either parent may be None. skin_tone: one parent's tone (50/50), or the
    in-between tone when both are known. hair: either parent's hair. Badges
    and headwear are NEVER inherited (fresh random). Facial hair starts as
    none; beards only appear via adulthood styling.
    """
    if not isinstance(rng, random.Random):
        raise TypeError("inherit_appearance requires a random.Random instance")

    def _coerce(p):
        # tolerate raw dicts from old saves
        if isinstance(p, Appearance) or p is None:
            return p
        if isinstance(p, dict):
            try:
                return Appearance.from_dict(p)
            except ValueError:
                return None
        return None

    parents = [_coerce(p) for p in (mother, father)]
    parents = [p for p in parents if p is not None]
    skin_tone = _inherit_skin(rng, parents)
    app = Appearance(
        seed=rng.randrange(1, 2**31),
        base=_inherit_base(rng, parents),
        skin_tone=skin_tone,
        hair=_inherit_hair(rng, parents),
        facial_hair="none",
        headwear="none",
        badges=[],
        background=_pick(rng, "background"),
        look_override="none",
    )
    # mutation: small chance the hair trait drifts (curly/red/default).
    # Design invariant (asserted by test_appearance): a biological child
    # differs from BOTH parents in at most ONE physical trait — so the
    # hair mutation is skipped when the skin tone already landed
    # in-between the parents' tones.
    novel_skin = parents and skin_tone not in tuple(
        p.skin_tone for p in parents)
    if parents and not novel_skin and rng.random() < 0.1:
        app.hair = rng.choice(["default", "curly", "red"])
    age_appropriate(app, age)
    return app


def _inherit_base(rng, parents):
    tones = [p for p in parents if p is not None]
    if not tones:
        return "person"
    if len(tones) == 2 and tones[0].base != tones[1].base:
        return "person" if rng.random() < 0.5 else rng.choice(tones).base
    return rng.choice(tones).base


def _inherit_skin(rng, parents):
    tones = [p.skin_tone for p in parents if p is not None]
    if not tones:
        return _pick(rng, "skin_tone")
    if len(tones) == 2:
        if rng.random() < 0.5:
            return rng.choice(tones)  # one parent's tone
        # in-between tone on the 6-step scale
        a, b = _SKIN_ORDER.index(tones[0]), _SKIN_ORDER.index(tones[1])
        return _SKIN_ORDER[(a + b + 1) // 2] if a != b else tones[0]
    return tones[0]


def _inherit_hair(rng, parents):
    hairs = [p.hair for p in parents if p is not None]
    if not hairs:
        return _pick(rng, "hair")
    hair = rng.choice(hairs)
    # white in a stored parent trait means genuinely white hair; children
    # inherit the trait as-is (bald parents can have haired children).
    return hair


def age_appropriate(appearance, age, gender=None):
    """Enforce the M0b guardrails, in place.

    Babies/children: no badges, no facial hair, no headwear. Beards only
    from the teen_adult stage. Badges capped at 2 always.
    """
    from .aging import age_stage
    stage = age_stage(age)
    if stage in ("baby", "child"):
        appearance.facial_hair = "none"
        appearance.headwear = "none"
        appearance.badges = []
    appearance.badges = catalog.normalize_multi("badges", appearance.badges)
    return appearance
