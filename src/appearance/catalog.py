"""Appearance catalog v2 (Fluent emoji avatars): category -> allowed IDs.

Every value used by an Appearance is a stable string ID from this catalog.
Each category includes a safe default; unknown IDs fall back to the default
(see model.Appearance.from_dict). The v1 catalog is retained under V1_* for
the v1->v2 migration only.
"""

CATALOG_VERSION = 2

BASES = [
    {"id": "person", "label": "Person"},
    {"id": "woman", "label": "Woman"},
    {"id": "man", "label": "Man"},
]

SKIN_TONES = [
    {"id": "default", "label": "Default"},
    {"id": "light", "label": "Light"},
    {"id": "medium-light", "label": "Medium-light"},
    {"id": "medium", "label": "Medium"},
    {"id": "medium-dark", "label": "Medium-dark"},
    {"id": "dark", "label": "Dark"},
]

HAIRS = [
    {"id": "default", "label": "Default"},
    {"id": "curly", "label": "Curly"},
    {"id": "red", "label": "Red"},
    {"id": "white", "label": "White"},
    {"id": "bald", "label": "Bald"},
]

FACIAL_HAIR = [
    {"id": "none", "label": "None"},
    {"id": "beard", "label": "Beard"},
]

HEADWEAR = [
    {"id": "none", "label": "None"},
    {"id": "headscarf", "label": "Headscarf"},
]

BADGES = [
    {"id": "glasses", "label": "Glasses"},
    {"id": "earrings", "label": "Earrings"},
    {"id": "cap", "label": "Cap"},
    {"id": "crown", "label": "Crown"},
    {"id": "flower", "label": "Flower"},
]

BACKGROUNDS = [
    {"id": "bg_01", "label": "Rose", "hex": "#f2d8d8"},
    {"id": "bg_02", "label": "Sand", "hex": "#f2e6d0"},
    {"id": "bg_03", "label": "Butter", "hex": "#f5f0c8"},
    {"id": "bg_04", "label": "Mint", "hex": "#d8ecd8"},
    {"id": "bg_05", "label": "Sky", "hex": "#d0e8f0"},
    {"id": "bg_06", "label": "Lavender", "hex": "#d8d8f0"},
    {"id": "bg_07", "label": "Lilac", "hex": "#ecd8f0"},
    {"id": "bg_08", "label": "Blush", "hex": "#f0d8e0"},
    {"id": "bg_09", "label": "Fog", "hex": "#e8e8e8"},
    {"id": "bg_10", "label": "Stone", "hex": "#e0d8c8"},
]

LOOK_OVERRIDES = [
    {"id": "none", "label": "None"},
    {"id": "student", "label": "Student"},
    {"id": "health_worker", "label": "Health worker"},
    {"id": "police", "label": "Police"},
    {"id": "chef", "label": "Chef"},
    {"id": "farmer", "label": "Farmer"},
    {"id": "mechanic", "label": "Mechanic"},
    {"id": "scientist", "label": "Scientist"},
    {"id": "artist", "label": "Artist"},
    {"id": "judge", "label": "Judge"},
    {"id": "pilot", "label": "Pilot"},
    {"id": "graduate", "label": "Graduate"},
    {"id": "bride", "label": "Bride"},
    {"id": "groom", "label": "Groom"},
]

CATEGORIES = {
    "base": BASES,
    "skin_tone": SKIN_TONES,
    "hair": HAIRS,
    "facial_hair": FACIAL_HAIR,
    "headwear": HEADWEAR,
    "badges": BADGES,
    "background": BACKGROUNDS,
    "look_override": LOOK_OVERRIDES,
}

# badges is the only multi-select category (max 2, enforced in the model).
MULTI_CATEGORIES = {"badges"}
BADGE_LIMIT = 2

# Service-side profile validation iterates these names inside the
# accessories object. v1 names keep validating against the v1 tables; v2
# profiles use "badges" (validated per-item elsewhere by the model).
ACCESSORY_CATEGORIES = ("badges", "glasses", "headwear", "earrings")


def default_id(category):
    entries = CATEGORIES[category]
    return entries[0]["id"]


def is_valid_id(category, value):
    """Accept v2 IDs; for v1-era category names also accept the v1 IDs
    (service validation and old fixtures must keep passing — the model
    migrates v1 data to v2 on load)."""
    entries = CATEGORIES.get(category)
    if entries is None:
        entries = V1_CATEGORIES.get(category)
        if entries is None:
            return False
    elif category in V1_CATEGORIES:
        # shared name (skin_tone): accept both generations
        return any(e["id"] == value for e in entries) or any(
            e["id"] == value for e in V1_CATEGORIES[category])
    return any(e["id"] == value for e in entries)


def normalize_id(category, value):
    """Return value if valid, else the category default."""
    if value is None:
        return default_id(category)
    entries = CATEGORIES.get(category, V1_CATEGORIES.get(category))
    if entries is None:
        return value
    if isinstance(value, str) and any(e["id"] == value for e in entries):
        return value
    return entries[0]["id"]


def normalize_multi(category, values, limit=None):
    """Validate a multi-select list: drop unknowns/duplicates, cap at limit."""
    if not isinstance(values, (list, tuple)):
        return []
    seen = []
    for v in values:
        v = normalize_id(category, v)
        if v not in seen:
            seen.append(v)
    cap = limit or BADGE_LIMIT
    return seen[:cap]


def entry(category, value):
    entries = CATEGORIES.get(category, V1_CATEGORIES.get(category))
    if entries is None:
        raise KeyError(category)
    for e in entries:
        if e["id"] == value:
            return e
    return entries[0]


def all_ids(category):
    entries = CATEGORIES.get(category, V1_CATEGORIES.get(category))
    if entries is None:
        raise KeyError(category)
    return [e["id"] for e in entries]


# --------------------------------------------------------------------- v1
# Retained for the v1 -> v2 migration AND backward compatibility: the
# service validates creator profiles and old saves against these IDs, and
# Appearance.from_dict migrates them to v2. Do not use for new content.

V1_SKIN_TONES = [
    {"id": "skin_01"}, {"id": "skin_02"}, {"id": "skin_03"}, {"id": "skin_04"},
    {"id": "skin_05"}, {"id": "skin_06"}, {"id": "skin_07"}, {"id": "skin_08"},
    {"id": "skin_09"}, {"id": "skin_10"}, {"id": "skin_11"}, {"id": "skin_12"},
]

# v1-only categories, keyed exactly as the v1 catalog named them. Kept so
# is_valid_id()/normalize_id() keep accepting everything that was valid
# before the v2 switch (service validation, old saves, fixtures).
V1_CATEGORIES = {
    "skin_tone": V1_SKIN_TONES,
    "face_shape": [{"id": v} for v in (
        "oval", "round", "square", "heart", "long", "diamond")],
    "eye_shape": [{"id": v} for v in (
        "almond", "round", "upturned", "downturned", "hooded", "monolid",
        "deep_set")],
    "eye_color": [{"id": v} for v in (
        "brown_dark", "brown_light", "hazel", "amber", "green", "blue",
        "gray", "blue_green")],
    "brows": [{"id": v} for v in (
        "soft", "thick", "thin", "arched", "straight", "bushy")],
    "nose": [{"id": v} for v in (
        "button", "straight", "wide", "pointed", "upturned", "hooked")],
    "mouth": [{"id": v} for v in (
        "smile", "neutral", "full", "thin", "wide", "pout")],
    "hair_texture": [{"id": v} for v in ("straight", "wavy", "curly", "coily")],
    "hair_color": [{"id": v} for v in (
        "black", "brown_dark", "brown_medium", "brown_light", "blonde_dark",
        "blonde", "blonde_platinum", "red", "auburn", "chestnut", "gray",
        "white", "blue_black")],
    "hair_style": [{"id": v} for v in (
        "short_crop", "buzz", "bald", "side_part", "undercut", "curtains",
        "slick_back", "man_bun", "ponytail", "high_ponytail", "braids",
        "locs", "afro_medium", "afro_large", "long_straight", "long_wavy",
        "long_curly", "wavy_bob", "bob", "bun", "bangs")],
    "facial_hair": [{"id": v} for v in (
        "none", "stubble", "mustache", "goatee", "beard_short", "beard_full")],
    "marks": [{"id": v} for v in (
        "freckles", "mole", "beauty_mark", "scar_brow", "birthmark")],
    "glasses": [{"id": v} for v in (
        "none", "round", "square", "rectangle", "oval", "cat_eye")],
    "headwear": [{"id": v} for v in (
        "none", "cap", "beanie", "headscarf", "sun_hat", "headband",
        "beret")],
    "earrings": [{"id": v} for v in (
        "none", "studs", "hoops", "dangles", "clip_ons")],
    "outfit_style": [{"id": v} for v in (
        "tee", "shirt", "hoodie", "sweater", "dress", "blouse", "suit",
        "jacket")],
    "outfit_color": [{"id": v} for v in (
        "blue", "red", "green", "yellow", "purple", "pink", "orange",
        "black", "white", "gray", "brown", "teal")],
}

# v1 tones are ordered light -> dark; pairwise buckets map into the 6 v2 tones.
V1_SKIN_MAP = {}
for _i, _e in enumerate(V1_SKIN_TONES):
    V1_SKIN_MAP[_e["id"]] = SKIN_TONES[_i // 2]["id"]

V1_CURLY_TEXTURES = {"curly", "coily"}
V1_RED_HAIR = {"red", "auburn"}
V1_WHITE_HAIR = {"white", "gray"}
V1_BADGE_CAP_HAIR = {"cap"}
