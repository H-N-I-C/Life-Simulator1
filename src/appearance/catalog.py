"""Appearance catalog: category -> ordered list of {id, label, ...}.

Every value used by an Appearance is a stable string ID from this catalog.
Each category includes a safe default as its first entry; unknown IDs must
fall back to the default (see model.Appearance.from_dict).
"""

CATALOG_VERSION = 1

# Each entry: {"id": str, "label": str}. Colour categories add "hex".
# The first entry of every category is the safe default.

SKIN_TONES = [
    {"id": "skin_01", "label": "Porcelain", "hex": "#f7ece5"},
    {"id": "skin_02", "label": "Ivory", "hex": "#f3e3d3"},
    {"id": "skin_03", "label": "Sand", "hex": "#eed7bf"},
    {"id": "skin_04", "label": "Honey", "hex": "#e6c5a2"},
    {"id": "skin_05", "label": "Warm Olive", "hex": "#d9b78f"},
    {"id": "skin_06", "label": "Tan", "hex": "#cfa276"},
    {"id": "skin_07", "label": "Caramel", "hex": "#bd8a5b"},
    {"id": "skin_08", "label": "Bronze", "hex": "#a57144"},
    {"id": "skin_09", "label": "Brown", "hex": "#8a5a33"},
    {"id": "skin_10", "label": "Espresso", "hex": "#6d4527"},
    {"id": "skin_11", "label": "Deep Umber", "hex": "#503119"},
    {"id": "skin_12", "label": "Ebony", "hex": "#38220f"},
]

FACE_SHAPES = [
    {"id": "oval", "label": "Oval"},
    {"id": "round", "label": "Round"},
    {"id": "square", "label": "Square"},
    {"id": "heart", "label": "Heart"},
    {"id": "long", "label": "Long"},
    {"id": "diamond", "label": "Diamond"},
]

EYE_SHAPES = [
    {"id": "almond", "label": "Almond"},
    {"id": "round", "label": "Round"},
    {"id": "upturned", "label": "Upturned"},
    {"id": "downturned", "label": "Downturned"},
    {"id": "hooded", "label": "Hooded"},
    {"id": "monolid", "label": "Monolid"},
    {"id": "deep_set", "label": "Deep-set"},
]

EYE_COLORS = [
    {"id": "brown_dark", "label": "Dark brown", "hex": "#3b2a20"},
    {"id": "brown_light", "label": "Light brown", "hex": "#7a5230"},
    {"id": "hazel", "label": "Hazel", "hex": "#8c6a3f"},
    {"id": "amber", "label": "Amber", "hex": "#a87b32"},
    {"id": "green", "label": "Green", "hex": "#4d7a4d"},
    {"id": "blue", "label": "Blue", "hex": "#4a6fa5"},
    {"id": "gray", "label": "Gray", "hex": "#7d8891"},
    {"id": "blue_green", "label": "Blue-green", "hex": "#3f7d7a"},
]

BROWS = [
    {"id": "soft", "label": "Soft"},
    {"id": "thick", "label": "Thick"},
    {"id": "thin", "label": "Thin"},
    {"id": "arched", "label": "Arched"},
    {"id": "straight", "label": "Straight"},
    {"id": "bushy", "label": "Bushy"},
]

NOSES = [
    {"id": "button", "label": "Button"},
    {"id": "straight", "label": "Straight"},
    {"id": "wide", "label": "Wide"},
    {"id": "pointed", "label": "Pointed"},
    {"id": "upturned", "label": "Upturned"},
    {"id": "hooked", "label": "Hooked"},
]

MOUTHS = [
    {"id": "smile", "label": "Smile"},
    {"id": "neutral", "label": "Neutral"},
    {"id": "full", "label": "Full lips"},
    {"id": "thin", "label": "Thin lips"},
    {"id": "wide", "label": "Wide"},
    {"id": "pout", "label": "Pout"},
]

HAIR_TEXTURES = [
    {"id": "straight", "label": "Straight"},
    {"id": "wavy", "label": "Wavy"},
    {"id": "curly", "label": "Curly"},
    {"id": "coily", "label": "Coily"},
]

HAIR_COLORS = [
    {"id": "black", "label": "Black", "hex": "#191414"},
    {"id": "brown_dark", "label": "Dark brown", "hex": "#3a2a1c"},
    {"id": "brown_medium", "label": "Brown", "hex": "#6b4a2c"},
    {"id": "brown_light", "label": "Light brown", "hex": "#9c7040"},
    {"id": "blonde_dark", "label": "Dark blonde", "hex": "#b08d4f"},
    {"id": "blonde", "label": "Blonde", "hex": "#d9bc72"},
    {"id": "blonde_platinum", "label": "Platinum blonde", "hex": "#e9dcb5"},
    {"id": "red", "label": "Red", "hex": "#9e3b22"},
    {"id": "auburn", "label": "Auburn", "hex": "#7a3b24"},
    {"id": "chestnut", "label": "Chestnut", "hex": "#5c3a24"},
    {"id": "gray", "label": "Gray", "hex": "#9a9a9a"},
    {"id": "white", "label": "White", "hex": "#e8e8e8"},
    {"id": "blue_black", "label": "Blue-black", "hex": "#141a24"},
]

HAIR_STYLES = [
    {"id": "short_crop", "label": "Short crop"},
    {"id": "buzz", "label": "Buzz cut"},
    {"id": "bald", "label": "Bald"},
    {"id": "side_part", "label": "Side part"},
    {"id": "undercut", "label": "Undercut"},
    {"id": "curtains", "label": "Curtains"},
    {"id": "slick_back", "label": "Slicked back"},
    {"id": "man_bun", "label": "Man bun"},
    {"id": "ponytail", "label": "Ponytail"},
    {"id": "high_ponytail", "label": "High ponytail"},
    {"id": "braids", "label": "Braids"},
    {"id": "locs", "label": "Locs"},
    {"id": "afro_medium", "label": "Afro"},
    {"id": "afro_large", "label": "Large afro"},
    {"id": "long_straight", "label": "Long straight"},
    {"id": "long_wavy", "label": "Long wavy"},
    {"id": "long_curly", "label": "Long curly"},
    {"id": "wavy_bob", "label": "Wavy bob"},
    {"id": "bob", "label": "Bob"},
    {"id": "bun", "label": "Bun"},
    {"id": "bangs", "label": "Bangs"},
]

FACIAL_HAIR = [
    {"id": "none", "label": "None"},
    {"id": "stubble", "label": "Stubble"},
    {"id": "mustache", "label": "Mustache"},
    {"id": "goatee", "label": "Goatee"},
    {"id": "beard_short", "label": "Short beard"},
    {"id": "beard_full", "label": "Full beard"},
]

MARKS = [
    {"id": "freckles", "label": "Freckles"},
    {"id": "mole", "label": "Mole"},
    {"id": "beauty_mark", "label": "Beauty mark"},
    {"id": "scar_brow", "label": "Brow scar"},
    {"id": "birthmark", "label": "Birthmark"},
]

GLASSES = [
    {"id": "none", "label": "None"},
    {"id": "round", "label": "Round"},
    {"id": "square", "label": "Square"},
    {"id": "rectangle", "label": "Rectangle"},
    {"id": "oval", "label": "Oval"},
    {"id": "cat_eye", "label": "Cat-eye"},
]

HEADWEAR = [
    {"id": "none", "label": "None"},
    {"id": "cap", "label": "Cap"},
    {"id": "beanie", "label": "Beanie"},
    {"id": "headscarf", "label": "Headscarf"},
    {"id": "sun_hat", "label": "Sun hat"},
    {"id": "headband", "label": "Headband"},
    {"id": "beret", "label": "Beret"},
]

EARRINGS = [
    {"id": "none", "label": "None"},
    {"id": "studs", "label": "Studs"},
    {"id": "hoops", "label": "Hoops"},
    {"id": "dangles", "label": "Dangles"},
    {"id": "clip_ons", "label": "Clip-ons"},
]

OUTFIT_STYLES = [
    {"id": "tee", "label": "T-shirt & jeans"},
    {"id": "shirt", "label": "Button-up"},
    {"id": "hoodie", "label": "Hoodie"},
    {"id": "sweater", "label": "Sweater"},
    {"id": "dress", "label": "Dress"},
    {"id": "blouse", "label": "Blouse & skirt"},
    {"id": "suit", "label": "Suit"},
    {"id": "jacket", "label": "Jacket"},
]

OUTFIT_COLORS = [
    {"id": "blue", "label": "Blue", "hex": "#3b6ea5"},
    {"id": "red", "label": "Red", "hex": "#a53b3b"},
    {"id": "green", "label": "Green", "hex": "#3ba55b"},
    {"id": "yellow", "label": "Yellow", "hex": "#d9b53b"},
    {"id": "purple", "label": "Purple", "hex": "#7a3ba5"},
    {"id": "pink", "label": "Pink", "hex": "#d97ab0"},
    {"id": "orange", "label": "Orange", "hex": "#d97a3b"},
    {"id": "black", "label": "Black", "hex": "#2b2b2b"},
    {"id": "white", "label": "White", "hex": "#f0f0f0"},
    {"id": "gray", "label": "Gray", "hex": "#888888"},
    {"id": "brown", "label": "Brown", "hex": "#7a5230"},
    {"id": "teal", "label": "Teal", "hex": "#3ba5a0"},
]

CATEGORIES = {
    "skin_tone": SKIN_TONES,
    "face_shape": FACE_SHAPES,
    "eye_shape": EYE_SHAPES,
    "eye_color": EYE_COLORS,
    "brows": BROWS,
    "nose": NOSES,
    "mouth": MOUTHS,
    "hair_texture": HAIR_TEXTURES,
    "hair_color": HAIR_COLORS,
    "hair_style": HAIR_STYLES,
    "facial_hair": FACIAL_HAIR,
    "marks": MARKS,
    "glasses": GLASSES,
    "headwear": HEADWEAR,
    "earrings": EARRINGS,
    "outfit_style": OUTFIT_STYLES,
    "outfit_color": OUTFIT_COLORS,
}

# marks is the only multi-select physical category; all others are single IDs.
MULTI_CATEGORIES = {"marks"}

ACCESSORY_CATEGORIES = ("glasses", "headwear", "earrings")

_DEFAULTS = None


def default_id(category):
    """Safe default ID for a category (first entry)."""
    entries = CATEGORIES[category]
    return entries[0]["id"]


def is_valid_id(category, value):
    return any(e["id"] == value for e in CATEGORIES[category])


def normalize_id(category, value):
    """Return value if valid, else the category default."""
    if value is None:
        return default_id(category)
    if isinstance(value, str) and is_valid_id(category, value):
        return value
    return default_id(category)


def entry(category, value):
    """Look up the catalog entry for a valid ID, else the default entry."""
    for e in CATEGORIES[category]:
        if e["id"] == value:
            return e
    return CATEGORIES[category][0]


def all_ids(category):
    return [e["id"] for e in CATEGORIES[category]]
