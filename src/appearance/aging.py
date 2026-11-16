"""Ageing: derive the effective view of an appearance at a given age.

Pure and deterministic. Stage boundaries per docs/collab/M0b-examples.json:
baby 0-2, child 3-12, teen_adult 13-59, greying 60-69 (hair derives to
white when hair != bald), elder 70+. Age is not stored in Appearance.
"""

BABY_MAX = 2
CHILD_MAX = 12
GREYING_MAX = 69

# A light determinism wrinkle: greying onset within the stage gives a
# per-person onset year from the seed (60-64), so two 62-year-olds can
# differ, matching the v1 engine's seed-driven aging spirit.
def age_stage(age):
    age = max(0, int(age))
    if age <= BABY_MAX:
        return "baby"
    if age <= CHILD_MAX:
        return "child"
    if age <= GREYING_MAX:
        return "greying" if age >= 60 else "teen_adult"
    return "elder"


def greying_onset_year(appearance):
    """First year the white-hair derivation applies (60-64, seed-driven)."""
    return 60 + (appearance.seed or 0) % 5


def effective_hair(appearance, age):
    """Hair as rendered: white from the greying onset through elder when
    the stored hair is not bald; otherwise the stored hair."""
    if appearance.hair == "bald":
        return "bald"
    if age_stage(age) in ("greying", "elder") \
            and int(age) >= greying_onset_year(appearance):
        return "white"
    return appearance.hair


def aged_view(appearance, age):
    """Everything the renderer needs for a portrait at the given age.
    Babies/children never render badges, beards or headwear (guardrail)."""
    age = max(0, int(age))
    stage = age_stage(age)
    young = stage in ("baby", "child")
    return {
        "stage": stage,
        "base": appearance.base,
        "skin_tone": appearance.skin_tone,
        "hair": effective_hair(appearance, age),
        "stored_hair": appearance.hair,
        "facial_hair": "none" if young else appearance.facial_hair,
        "headwear": "none" if young else appearance.headwear,
        "badges": [] if young else list(appearance.badges),
        "background": appearance.background,
        "look_override": appearance.look_override,
    }
