"""Ageing: derive the effective view of an appearance at a given age.

Pure and deterministic: grey/wrinkle/recession progression is derived from
the appearance seed + age only. Age is not stored in Appearance.
"""

from . import catalog

BABY_MAX = 2
CHILD_MAX = 12
TEEN_MAX = 17
ADULT_MAX = 49


def age_stage(age):
    if age <= BABY_MAX:
        return "baby"
    if age <= CHILD_MAX:
        return "child"
    if age <= TEEN_MAX:
        return "teen"
    if age <= ADULT_MAX:
        return "adult"
    return "elder"


def _grey_factor(appearance, age):
    """0.0 = no grey, 1.0 = fully grey/white. Deterministic per seed+age."""
    if age < 35:
        return 0.0
    seed = appearance.seed or 0
    # individual greying speed in [0.6, 1.4]
    speed = 0.6 + (seed % 1000) / 1000.0 * 0.8
    t = (age - 35) / 40.0 * speed  # full grey around age 75 * speed
    return max(0.0, min(1.0, t))


def _wrinkle_level(appearance, age):
    if age < 35:
        return 0
    seed = appearance.seed or 0
    offset = (seed // 1000) % 10  # individual offset 0-9 years
    eff = age + offset
    if eff < 40:
        return 0
    if eff < 52:
        return 1
    if eff < 65:
        return 2
    return 3


def _recession_level(appearance, age):
    """0 = none, 1 = slight, 2 = receding, 3 = high/bald front.

    Only some people recede: the seed gives ~35% of people (any gender)
    gradual recession from their 40s; the rest keep their hair (greying
    only). Bald/buzz styles are a choice, not recession.
    """
    style = appearance.hair_style
    if style in ("bald", "buzz"):
        return 0
    seed = appearance.seed or 0
    if seed % 100 >= 35:
        return 0
    starts = 40 + (seed // 100) % 15  # recession starts between 40 and 54
    if age < starts:
        return 0
    years = age - starts
    if years < 10:
        return 1
    if years < 22:
        return 2
    return 3


def _mix_hex(hex_a, hex_b, t):
    a = [int(hex_a[i:i + 2], 16) for i in (1, 3, 5)]
    b = [int(hex_b[i:i + 2], 16) for i in (1, 3, 5)]
    m = [round(x + (y - x) * t) for x, y in zip(a, b)]
    return "#{:02x}{:02x}{:02x}".format(*m)


def aged_view(appearance, age):
    """Return a dict describing how the appearance looks at the given age."""
    age = max(0, int(age))
    stage = age_stage(age)
    grey = _grey_factor(appearance, age)
    base_hex = catalog.entry("hair_color", appearance.hair_color)["hex"]
    if grey >= 0.999:
        effective_hair_color = "white"
        effective_hair_hex = catalog.entry("hair_color", "white")["hex"]
    elif grey > 0.0:
        grey_hex = catalog.entry("hair_color", "gray")["hex"]
        effective_hair_color = appearance.hair_color
        effective_hair_hex = _mix_hex(base_hex, grey_hex, grey)
    else:
        effective_hair_color = appearance.hair_color
        effective_hair_hex = base_hex
    return {
        "stage": stage,
        "hair_color": effective_hair_color,
        "hair_hex": effective_hair_hex,
        "grey_factor": grey,
        "wrinkles": _wrinkle_level(appearance, age),
        "recession": _recession_level(appearance, age),
    }
