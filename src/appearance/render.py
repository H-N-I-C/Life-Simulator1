"""SVG avatar renderer for the appearance system.

`render_avatar_svg(appearance_dict, age, size=128)` returns a layered,
deterministic SVG string (viewBox 0 0 200 200, no scripts, no external refs).

Layer order: background -> back hair -> outfit(shoulders) -> ears -> face ->
features -> marks -> wrinkles -> front hair -> facial hair -> glasses ->
earrings -> headwear.

Owned by Kimi B. Ageing comes from Kimi A's src/appearance/aging.py via the
wrapper below; render failures there raise (no silent fallback art).
"""

from src.appearance.aging import aged_view as _real_aged_view

VIEWBOX = "0 0 200 200"


def aged_view(appearance, age):
    """Contract wrapper: appearance dict in, view dict out.

    Delegates to src/appearance/aging.py. Returns stage, hair_color id,
    hair_hex, grey factor, wrinkles (0-3), recession (0-3).
    """
    from src.appearance.model import Appearance
    app_obj = appearance if isinstance(appearance, Appearance) \
        else Appearance.from_dict(appearance or {})
    v = _real_aged_view(app_obj, int(age))
    return {
        "stage": v["stage"],
        "hair_color": v["hair_color"],
        "hair_hex": v.get("hair_hex"),
        "grey": v.get("grey_factor", 0.0),
        "wrinkles": v.get("wrinkles", 0),
        "recession": v.get("recession", 0),
    }


# ---------------------------------------------------------------------------
# Catalog resolution
# ---------------------------------------------------------------------------

_FALLBACK_CATEGORIES = {
    # Only used if Kimi A's catalog module cannot be imported at all.
    "skin_tone": ["skin_%02d" % i for i in range(1, 13)],
    "face_shape": ["oval", "round", "square", "heart", "long", "diamond"],
    "eye_shape": ["almond", "round", "upturned", "downturned", "hooded",
                  "monolid", "deep_set"],
    "eye_color": ["brown_dark", "brown_light", "hazel", "amber", "green",
                  "blue", "gray", "blue_green"],
    "brows": ["soft", "thick", "thin", "arched", "straight", "bushy"],
    "nose": ["button", "straight", "wide", "pointed", "upturned", "hooked"],
    "mouth": ["smile", "neutral", "full", "thin", "wide", "pout"],
    "hair_texture": ["straight", "wavy", "curly", "coily"],
    "hair_color": ["black", "brown_dark", "brown_medium", "brown_light",
                   "blonde_dark", "blonde", "blonde_platinum", "red",
                   "auburn", "chestnut", "gray", "white", "blue_black"],
    "hair_style": ["short_crop", "buzz", "bald", "side_part", "undercut",
                   "curtains", "slick_back", "man_bun", "ponytail",
                   "high_ponytail", "braids", "locs", "afro_medium",
                   "afro_large", "long_straight", "long_wavy", "long_curly",
                   "wavy_bob", "bob", "bangs", "bun"],
    "facial_hair": ["none", "stubble", "mustache", "goatee", "beard_short",
                    "beard_full"],
    "marks": ["freckles", "mole", "beauty_mark", "scar_brow", "birthmark"],
    "glasses": ["none", "round", "square", "rectangle", "oval", "cat_eye"],
    "headwear": ["none", "cap", "beanie", "headscarf", "sun_hat",
                 "headband", "beret"],
    "earrings": ["none", "studs", "hoops", "dangles", "clip_ons"],
    "outfit_style": ["tee", "shirt", "hoodie", "sweater", "dress", "blouse",
                     "suit", "jacket"],
    "outfit_color": ["blue", "red", "green", "yellow", "purple", "pink",
                     "orange", "black", "white", "gray", "brown", "teal"],
}

_DEFAULTS = {
    "skin_tone": "skin_04", "face_shape": "oval", "eye_shape": "almond",
    "eye_color": "brown_dark", "brows": "soft", "nose": "button",
    "mouth": "smile", "hair_texture": "straight",
    "hair_color": "brown_medium", "hair_style": "short_crop",
    "facial_hair": "none", "outfit_style": "tee", "outfit_color": "blue",
    "glasses": "none", "headwear": "none", "earrings": "none",
}


def get_catalog():
    from src.appearance import catalog as _c
    return getattr(_c, "CATEGORIES", None) or _FALLBACK_CATEGORIES


def _resolve(cat, category, value):
    """Resolve a value to a catalog entry id, falling back to the default.

    Non-string/unhashable values (e.g. lists) are treated as unknown and
    fall back safely — never crash on malformed input.
    """
    entries = cat.get(category) or []
    known = None
    if isinstance(value, str):
        known = {e.get("id") for e in entries
                 if isinstance(e, dict)} or set(_FALLBACK_CATEGORIES.get(
                     category, []))
    if not (isinstance(value, str) and value in known):
        value = _DEFAULTS.get(category)
        if value not in {e.get("id") for e in entries if isinstance(e, dict)}:
            fallback_ids = _FALLBACK_CATEGORIES.get(category, [])
            value = fallback_ids[0] if fallback_ids else value
    # colour data from the real catalog entry when present
    for e in entries:
        if isinstance(e, dict) and e.get("id") == value:
            return e
    return {"id": value}


# ---------------------------------------------------------------------------
# Colour helpers
# ---------------------------------------------------------------------------

def _hex_to_rgb(c):
    c = (c or "#000000").lstrip("#")
    return tuple(int(c[i:i + 2], 16) for i in (0, 2, 4))


def _rgb_to_hex(rgb):
    return "#%02x%02x%02x" % tuple(max(0, min(255, round(v)))
                                   for v in rgb)


def _mix(c1, c2, t):
    a, b = _hex_to_rgb(c1), _hex_to_rgb(c2)
    return _rgb_to_hex(tuple(a[i] + (b[i] - a[i]) * t for i in range(3)))


def _shade(c, t):
    """t > 0 darkens toward black, t < 0 lightens toward white."""
    return _mix(c, "#ffffff" if t < 0 else "#000000", abs(t))


# ---------------------------------------------------------------------------
# Geometry per age stage — head-and-shoulders portrait, head 50–60% of frame
# ---------------------------------------------------------------------------

def _stage_geometry(stage):
    """Head ellipse + anchors. Babies/children: bigger, rounder heads."""
    if stage == "baby":
        return dict(cx=100, cy=98, rx=47, ry=46, neck=0, shoulder_y=156,
                    shoulder_w=48, eye_y=92, eye_dx=19, mouth_y=118,
                    brow_y=76, nose_y=106, ear_y=96, ear_rx=6, ear_ry=8,
                    scale=1.0)
    if stage == "child":
        return dict(cx=100, cy=90, rx=44, ry=44, neck=8, shoulder_y=150,
                    shoulder_w=58, eye_y=83, eye_dx=17, mouth_y=110,
                    brow_y=67, nose_y=97, ear_y=88, ear_rx=5.5, ear_ry=7.5,
                    scale=0.95)
    if stage == "teen":
        return dict(cx=100, cy=86, rx=40, ry=44, neck=13, shoulder_y=148,
                    shoulder_w=68, eye_y=78, eye_dx=15, mouth_y=104,
                    brow_y=62, nose_y=92, ear_y=84, ear_rx=5, ear_ry=7,
                    scale=0.9)
    # adult / elder — head occupies ~55% of the frame height
    return dict(cx=100, cy=84, rx=40, ry=52, neck=16, shoulder_y=150,
                shoulder_w=76, eye_y=74, eye_dx=14, mouth_y=102,
                brow_y=58, nose_y=90, ear_y=82, ear_rx=5, ear_ry=7,
                scale=0.86)


def _face_path(cx, cy, rx, ry, shape):
    """Head outline per face shape (visibly distinct silhouettes)."""
    if shape == "round":
        return _ellipse_path(cx, cy, rx * 1.02, ry * 0.96)
    if shape == "square":
        return ("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                "Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f "
                "L %.1f %.1f Q %.1f %.1f %.1f %.1f Z"
                % (cx - rx, cy - ry * 0.35,
                   cx - rx, cy - ry, cx - rx * 0.5, cy - ry,
                   cx - rx * 0.56, cy + ry * 0.8,
                   cx - rx * 0.5, cy + ry, cx, cy + ry,
                   cx + rx * 0.5, cy + ry, cx + rx * 0.56, cy + ry * 0.8,
                   cx + rx * 0.5, cy - ry,
                   cx + rx, cy - ry, cx + rx, cy - ry * 0.35))
    if shape == "heart":
        return ("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                "Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z"
                % (cx - rx * 0.9, cy - ry * 0.2,
                   cx - rx, cy - ry * 0.8, cx, cy - ry,
                   cx + rx, cy - ry * 0.8, cx + rx * 0.9, cy - ry * 0.2,
                   cx + rx * 0.45, cy + ry,
                   cx - rx * 0.45, cy + ry,
                   cx - rx * 0.9, cy - ry * 0.2))
    if shape == "long":
        return _ellipse_path(cx, cy - ry * 0.06, rx * 0.88, ry * 1.1)
    if shape == "diamond":
        return ("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f "
                "Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z"
                % (cx, cy - ry,
                   cx + rx * 0.85, cy - ry * 0.35, cx + rx * 0.95, cy,
                   cx + rx * 0.7, cy + ry * 0.6, cx, cy + ry,
                   cx - rx * 0.7, cy + ry * 0.6, cx - rx * 0.95, cy,
                   cx - rx * 0.85, cy - ry * 0.35, cx, cy - ry))
    # oval
    return _ellipse_path(cx, cy, rx * 0.95, ry)


def _ellipse_path(cx, cy, rx, ry):
    return ("M %.1f %.1f A %.1f %.1f 0 1 0 %.1f %.1f "
            "A %.1f %.1f 0 1 0 %.1f %.1f Z"
            % (cx - rx, cy, rx, ry, cx + rx, cy, rx, ry, cx - rx, cy))


# ---------------------------------------------------------------------------
# SVG builder
# ---------------------------------------------------------------------------

class _SVG:
    def __init__(self):
        self.parts = []

    def add(self, s):
        self.parts.append(s)

    def ellipse(self, cx, cy, rx, ry, fill, extra=""):
        self.add('<ellipse cx="%.1f" cy="%.1f" rx="%.1f" ry="%.1f" '
                 'fill="%s" %s/>' % (cx, cy, rx, ry, fill, extra))

    def circle(self, cx, cy, r, fill, extra=""):
        self.add('<circle cx="%.1f" cy="%.1f" r="%.1f" fill="%s" %s/>'
                 % (cx, cy, r, fill, extra))

    def path(self, d, fill, extra=""):
        self.add('<path d="%s" fill="%s" %s/>' % (d, fill, extra))

    def line(self, x1, y1, x2, y2, stroke, w, extra=""):
        self.add('<line x1="%.1f" y1="%.1f" x2="%.1f" y2="%.1f" '
                 'stroke="%s" stroke-width="%.1f" %s/>'
                 % (x1, y1, x2, y2, stroke, w, extra))

    def render(self):
        return "".join(self.parts)


# ---------------------------------------------------------------------------
# Background + outfit
# ---------------------------------------------------------------------------

def _draw_background(svg, g, outfit_color):
    bg = _mix(outfit_color, "#ffffff", 0.86)
    svg.circle(100, 100, 96, bg)
    # soft ground shadow
    svg.ellipse(g["cx"], 196, g["shoulder_w"] * 0.9, 7, "#000000",
                'opacity="0.07"')


def _draw_outfit(svg, g, app, cat, skin, view):
    style = _resolve(cat, "outfit_style", app.get("outfit_style"))["id"]
    color = _resolve(cat, "outfit_color", app.get("outfit_color")).get(
        "hex", "#3b6ea5")
    dark = _shade(color, 0.22)
    light = _shade(color, -0.25)
    cx = g["cx"]
    y = g["shoulder_y"]
    w = g["shoulder_w"]
    neck_y = y - g["neck"]
    if g["neck"]:
        svg.add('<rect x="%.1f" y="%.1f" width="14" height="%.1f" rx="4" '
                'fill="%s"/>' % (cx - 7, y - g["neck"] - 4,
                                 g["neck"] + 10, _shade(skin, 0.06)))
    # torso with shoulder slope
    svg.path("M %.1f 200 L %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
             "Q %.1f %.1f %.1f %.1f L %.1f 200 Z"
             % (cx - w, cx - w, y + 12, cx - w * 0.72, y - 5, cx - 15,
                neck_y + 4, cx + 15, neck_y + 4, cx + w * 0.72, y - 5,
                cx + w, y + 12, cx + w), color)

    if style == "tee":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 15, neck_y + 4, cx, neck_y + 15, cx + 15, neck_y + 4),
                 "none", 'stroke="%s" stroke-width="2.6" '
                 'stroke-linecap="round"' % dark)
    elif style == "shirt":
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx - 15, neck_y + 2, cx, neck_y + 12, cx + 15, neck_y + 2),
                 "#f4f4f6")
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx - 15, neck_y + 2, cx - 7, neck_y + 7, cx - 13,
                  neck_y + 10), dark)
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx + 15, neck_y + 2, cx + 7, neck_y + 7, cx + 13,
                  neck_y + 10), dark)
        svg.line(cx, neck_y + 12, cx, 200, dark, 1.8)
        for by in range(int(neck_y) + 28, 200, 18):
            svg.circle(cx, by, 1.6, dark)
    elif style == "hoodie":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 18, neck_y + 6, cx, neck_y - 6, cx + 18, neck_y + 6,
                  cx, neck_y + 18, cx - 18, neck_y + 6), dark)
        svg.line(cx - 5, neck_y + 14, cx - 5, neck_y + 27, "#f4f4f6", 1.8)
        svg.line(cx + 5, neck_y + 14, cx + 5, neck_y + 27, "#f4f4f6", 1.8)
        svg.circle(cx - 5, neck_y + 28, 1.6, "#f4f4f6")
        svg.circle(cx + 5, neck_y + 28, 1.6, "#f4f4f6")
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx - 22, y + 30, cx - 6, y + 30, cx - 6, y + 46, cx - 22,
                  y + 46), dark)
        svg.path("M %.1f %.1f L %.1f %.1f" % (cx - 22, y + 30, cx - 6, y + 30),
                 "none", 'stroke="%s" stroke-width="1.4"' % light)
    elif style == "sweater":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 16, neck_y + 4, cx, neck_y + 14, cx + 16, neck_y + 4),
                 "none", 'stroke="%s" stroke-width="5" stroke-linecap="round"'
                 % dark)
        for dy in (40, 50):
            svg.line(cx - w * 0.55, y + dy, cx + w * 0.55, y + dy, dark,
                     1.3, 'opacity="0.4"')
        # knitted cuffs
        svg.line(cx - w, 188, cx - w * 0.7, 188, dark, 4, 'opacity="0.5"')
        svg.line(cx + w * 0.7, 188, cx + w, 188, dark, 4, 'opacity="0.5"')
    elif style == "dress":
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f 200 L %.1f 200 Z" %
                 (cx - 15, neck_y + 4, cx + 15, neck_y + 4, cx + w * 0.92,
                  cx - w * 0.92), dark)
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 15, neck_y + 4, cx, neck_y + 13, cx + 15, neck_y + 4),
                 "none", 'stroke="%s" stroke-width="2.2"' % light)
        svg.line(cx - w * 0.7, y + 34, cx + w * 0.7, y + 34, light, 1.4,
                 'opacity="0.7"')
    elif style == "blouse":
        # peter-pan collar
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 15, neck_y + 2, cx - 4, neck_y + 12, cx, neck_y + 8,
                  cx - 2, neck_y + 2, cx - 15, neck_y + 2), "#f4f4f6")
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (cx + 15, neck_y + 2, cx + 4, neck_y + 12, cx, neck_y + 8,
                  cx + 2, neck_y + 2, cx + 15, neck_y + 2), "#f4f4f6")
        svg.circle(cx, neck_y + 6, 1.8, dark)
        # puff sleeves
        svg.circle(cx - w * 0.78, y + 6, 9, color)
        svg.circle(cx + w * 0.78, y + 6, 9, color)
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - w * 0.78, y + 13, cx, y + 20, cx + w * 0.78, y + 13),
                 "none", 'stroke="%s" stroke-width="1.3" opacity="0.6"'
                 % dark)
    elif style == "suit":
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx - 14, neck_y + 2, cx, neck_y + 11, cx + 14, neck_y + 2),
                 "#f4f4f6")
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx - 17, neck_y + 3, cx - 2, neck_y + 11, cx - 7, y + 34,
                  cx - 20, neck_y + 14), dark)
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx + 17, neck_y + 3, cx + 2, neck_y + 11, cx + 7, y + 34,
                  cx + 20, neck_y + 14), dark)
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                 (cx, neck_y + 11, cx - 3.5, neck_y + 16, cx + 3.5,
                  neck_y + 16), "#8a3b3b")
        svg.circle(cx, y + 40, 1.6, dark)
    elif style == "jacket":
        svg.line(cx, neck_y + 8, cx, 200, dark, 2.2)
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f" %
                 (cx - 15, neck_y + 2, cx - 8, neck_y + 10, cx - 15,
                  neck_y + 16), "none",
                 'stroke="%s" stroke-width="3" stroke-linecap="round"'
                 % dark)
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f" %
                 (cx + 15, neck_y + 2, cx + 8, neck_y + 10, cx + 15,
                  neck_y + 16), "none",
                 'stroke="%s" stroke-width="3" stroke-linecap="round"'
                 % dark)
        for side in (-1, 1):
            svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                     (cx + side * 14 - 8, y + 34, cx + side * 14 + 8, y + 34,
                      cx + side * 14 + 8, y + 44, cx + side * 14 - 8, y + 44),
                     dark, 'opacity="0.85"')


# ---------------------------------------------------------------------------
# Ears + face
# ---------------------------------------------------------------------------

def _draw_ears(svg, g, skin, skin_dark):
    y = g["ear_y"]
    for side in (-1, 1):
        x = g["cx"] + side * (g["rx"] * 0.97 - 1)
        svg.ellipse(x, y, g["ear_rx"], g["ear_ry"], skin,
                    'stroke="%s" stroke-width="1.6"' % skin_dark)
        svg.ellipse(x + side * 1.5, y + 1, g["ear_rx"] * 0.45,
                    g["ear_ry"] * 0.55, _shade(skin, 0.12))


def _draw_face_base(svg, g, skin, skin_dark, face_shape, view):
    svg.path(_face_path(g["cx"], g["cy"], g["rx"], g["ry"], face_shape),
             skin, 'stroke="%s" stroke-width="2"' % skin_dark)
    # soft cheek/jaw shading for depth
    svg.ellipse(g["cx"] - g["rx"] * 0.45, g["eye_y"] + 14, 10, 6,
                _shade(skin, 0.1), 'opacity="0.35"')
    svg.ellipse(g["cx"] + g["rx"] * 0.45, g["eye_y"] + 14, 10, 6,
                _shade(skin, 0.1), 'opacity="0.35"')
    if view["stage"] in ("baby", "child"):
        for side in (-1, 1):
            svg.ellipse(g["cx"] + side * g["rx"] * 0.5, g["eye_y"] + 13,
                        6.5, 4, _mix(skin, "#e88a8a", 0.4), 'opacity="0.55"')


# ---------------------------------------------------------------------------
# Features
# ---------------------------------------------------------------------------

def _draw_eyes(svg, g, app, cat, skin):
    """Eyes built from named inner/outer corner coordinates per shape.

    Every shape defines (iw, ih, inner_y, outer_y, top_y, bottom_y) so the
    aperture, width and tilt are genuinely different — not a shared outline
    with a tweaked lash line. Coordinates mirror about the face centre.
    """
    eye_shape = _resolve(cat, "eye_shape", app.get("eye_shape"))["id"]
    eye_hex = _resolve(cat, "eye_color", app.get("eye_color")).get(
        "hex", "#3b2a20")
    cx, cy, dx = g["cx"], g["eye_y"], g["eye_dx"]
    sc = g["scale"] + 0.25
    dark = _shade(eye_hex, 0.4)

    shapes = {
        #        iw    ih   in_y  out_y  top_y          spacing
        "almond":    (10.5, 6.5, 0.8, 0.8, -1.0, 0),
        "round":     (8.6, 8.8, 0.6, 0.6, -1.05, 0),
        "monolid":   (10.0, 5.2, 1.2, 1.2, -0.45, -1),
        "hooded":    (10.0, 5.8, 1.0, 1.0, -0.8, 0),
        "deep_set":  (9.2, 6.0, 0.8, 0.8, -0.9, -1),
        "upturned":  (10.0, 6.4, 1.8, -3.0, -1.0, 0),
        "downturned": (10.0, 6.4, -1.8, 3.0, -1.0, 0),
    }
    iw, ih, in_y, out_y, top_k, spacing = shapes.get(
        eye_shape, shapes["almond"])
    iw *= sc
    ih *= sc
    dx += spacing

    for side in (-1, 1):
        ex = cx + side * dx
        inner_x = ex - side * iw
        outer_x = ex + side * iw
        inner_yy = cy + in_y
        outer_yy = cy + out_y
        top_yy = cy + ih * top_k
        bot_yy = cy + ih * 0.85
        # sclera: inner corner -> top -> outer corner -> bottom -> close
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (inner_x, inner_yy, ex, top_yy, outer_x, outer_yy,
                  ex, bot_yy + 1.5, inner_x, inner_yy),
                 "#ffffff", 'stroke="%s" stroke-width="1.3"' % dark)
        if eye_shape == "monolid":
            # flat upper lid line, no visible crease
            svg.line(inner_x, top_yy + 1, outer_x, top_yy + 1, dark, 1.8,
                     'stroke-linecap="round"')
        elif eye_shape == "hooded":
            # heavy lid arc + skin overlapping the top of the iris
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (inner_x, top_yy - 2.5, ex, top_yy - ih * 0.8,
                      outer_x, top_yy - 2.5), "none",
                     'stroke="%s" stroke-width="2.2" stroke-linecap="round"'
                     % _shade(skin, 0.3))
            svg.ellipse(ex, cy - ih * 0.35, iw * 0.85, ih * 0.42, skin,
                        'opacity="0.9"')
        elif eye_shape == "deep_set":
            # pronounced socket shadow above a slightly recessed eye
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (inner_x - 1, top_yy - 2, ex, top_yy - ih * 1.05,
                      outer_x + 1, top_yy - 2), "none",
                     'stroke="%s" stroke-width="2.4" stroke-linecap="round" '
                     'opacity="0.75"' % _shade(skin, 0.35))
        # iris/pupil/highlight
        iris_r = ih * (0.78 if eye_shape == "round" else 0.7)
        svg.circle(ex, cy + 0.8, iris_r, eye_hex)
        svg.circle(ex, cy + 0.8, iris_r * 0.52, "#1c1c22")
        svg.circle(ex - iris_r * 0.3, cy + 0.2, iris_r * 0.2, "#ffffff")
        # lash line follows the actual named corners
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (inner_x, inner_yy + 0.4, ex, top_yy - 1.2, outer_x,
                  outer_yy + 0.4), "none",
                 'stroke="%s" stroke-width="1.9" stroke-linecap="round"'
                 % dark)
    _draw_brows(svg, g, app, cat, dx)


def _draw_brows(svg, g, app, cat, dx=None):
    style = _resolve(cat, "brows", app.get("brows"))["id"]
    cx = g["cx"]
    dx = g["eye_dx"] if dx is None else dx
    # (width, weight, height offset, length factor)
    styles = {
        "soft":     (2.2, 1.0, 0.0, 1.0),
        "thick":    (4.8, 1.0, -1.0, 1.05),
        "thin":     (1.3, 0.9, +1.0, 0.8),
        "arched":   (2.5, 1.0, -2.0, 0.95),
        "straight": (3.2, 1.0, +1.5, 1.1),
        "bushy":    (5.0, 1.0, -1.5, 1.1),
    }
    w, wt, dy, lf = styles.get(style, styles["soft"])
    cy = g["brow_y"] + dy
    bw = (10 * g["scale"] + 3.5) * lf
    color = svg._brow_color
    for side in (-1, 1):
        ex = cx + side * dx
        if style == "arched":
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (ex - bw, cy + 3, ex, cy - 6.5, ex + bw, cy + 1.5),
                     "none", 'stroke="%s" stroke-width="%.1f" '
                     'stroke-linecap="round"' % (color, w * wt))
        elif style == "straight":
            svg.line(ex - bw, cy + 0.5, ex + bw, cy, color, w * wt,
                     'stroke-linecap="round"')
        else:
            lift = 3.0 if style == "soft" else 1.4
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (ex - bw, cy + 1.8, ex, cy - lift, ex + bw, cy + 0.9),
                     "none", 'stroke="%s" stroke-width="%.1f" '
                     'stroke-linecap="round"' % (color, w * wt))
            if style == "bushy":
                for i in range(4):
                    fx = ex - bw + (i + 0.5) * (2 * bw / 4)
                    svg.line(fx, cy - 0.5, fx + 1.8, cy - 4, color, 1.2,
                             'stroke-linecap="round"')


def _draw_nose(svg, g, app, cat, skin_dark):
    """Noses with strong width/bridge/tip differences (readable at 128px)."""
    style = _resolve(cat, "nose", app.get("nose"))["id"]
    cx, y = g["cx"], g["nose_y"]
    sc = (g["scale"] + 0.25) * 1.25  # overall larger than round 1
    w = 2.4  # base stroke weight
    if style == "button":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 4 * sc, y - 4, cx + 4 * sc, y - 4, cx, y + 3),
                 "none", 'stroke="%s" stroke-width="%.1f" '
                 'stroke-linecap="round"' % (skin_dark, w))
        svg.circle(cx - 4.2 * sc, y + 3.5, 1.6, skin_dark)
        svg.circle(cx + 4.2 * sc, y + 3.5, 1.6, skin_dark)
    elif style == "wide":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 9 * sc, y - 5, cx + 9 * sc, y - 5, cx, y + 3),
                 "none", 'stroke="%s" stroke-width="%.1f" '
                 'stroke-linecap="round"' % (skin_dark, w))
        svg.circle(cx - 8 * sc, y + 3.5, 2.0, skin_dark)
        svg.circle(cx + 8 * sc, y + 3.5, 2.0, skin_dark)
        svg.line(cx - 9 * sc, y - 5, cx - 7 * sc, y - 8, skin_dark, w)
        svg.line(cx + 9 * sc, y - 5, cx + 7 * sc, y - 8, skin_dark, w)
    elif style == "pointed":
        svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f" %
                 (cx - 4 * sc, y - 6, cx + 1.5, y + 4.5, cx + 5 * sc, y - 2.5),
                 "none", 'stroke="%s" stroke-width="%.1f" '
                 'stroke-linecap="round" stroke-linejoin="round"'
                 % (skin_dark, w))
    elif style == "upturned":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 4 * sc, y - 2, cx, y + 5.5, cx + 4 * sc, y - 2),
                 "none", 'stroke="%s" stroke-width="%.1f" '
                 'stroke-linecap="round"' % (skin_dark, w))
        svg.circle(cx - 3.6 * sc, y + 2.5, 1.4, skin_dark)
        svg.circle(cx + 3.6 * sc, y + 2.5, 1.4, skin_dark)
    elif style == "hooked":
        svg.path("M %.1f %.1f L %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 2 * sc, y - 9, cx - 3 * sc, y + 1, cx + 4 * sc,
                  y + 4, cx + 5 * sc, y - 1),
                 "none", 'stroke="%s" stroke-width="%.1f" '
                 'stroke-linecap="round"' % (skin_dark, w + 0.2))
    elif style == "straight":
        svg.line(cx, y - 8, cx, y + 3, skin_dark, w,
                 'stroke-linecap="round"')
        svg.circle(cx - 4.2 * sc, y + 3.5, 1.6, skin_dark)
        svg.circle(cx + 4.2 * sc, y + 3.5, 1.6, skin_dark)
    else:  # strong: broad bridge + wide base
        svg.path("M %.1f %.1f L %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 3.5 * sc, y - 9, cx - 2.5 * sc, y + 1, cx + 5.5 * sc,
                  y + 3, cx + 6.5 * sc, y - 1),
                 "none", 'stroke="%s" stroke-width="%.1f" '
                 'stroke-linecap="round"' % (skin_dark, w + 0.4))
        svg.circle(cx - 5.5 * sc, y + 3.5, 1.8, skin_dark)
        svg.circle(cx + 5.5 * sc, y + 3.5, 1.8, skin_dark)


def _draw_mouth(svg, g, app, cat):
    """Mouths with clearly different width and lip construction."""
    style = _resolve(cat, "mouth", app.get("mouth"))["id"]
    cx, y = g["cx"], g["mouth_y"]
    sc = g["scale"] + 0.3
    lip = "#b06060"
    dark = "#8c4048"
    if style == "smile":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 9 * sc, y, cx, y + 8 * sc, cx + 9 * sc, y,
                  cx, y + 2.8 * sc, cx - 9 * sc, y), lip)
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 8 * sc, y, cx, y + 6.5 * sc, cx + 8 * sc, y), "none",
                 'stroke="%s" stroke-width="1.5" stroke-linecap="round"'
                 % dark)
    elif style == "neutral":
        svg.line(cx - 7.5 * sc, y + 1, cx + 7.5 * sc, y + 1, lip, 2.8,
                 'stroke-linecap="round"')
        svg.line(cx - 6 * sc, y + 3.5, cx + 6 * sc, y + 3.5, lip, 1.2,
                 'stroke-linecap="round" opacity="0.6"')
    elif style == "full":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 8.5 * sc, y, cx - 3.8 * sc, y - 4.5 * sc, cx, y - 1,
                  cx + 3.8 * sc, y - 4.5 * sc, cx + 8.5 * sc, y,
                  cx + 3.8 * sc, y + 5 * sc, cx - 3.8 * sc, y + 5 * sc),
                 lip)
        svg.line(cx - 8.5 * sc, y, cx + 8.5 * sc, y, dark, 1.2)
        svg.ellipse(cx, y + 2.8 * sc, 4.5 * sc, 1.8 * sc,
                    _mix(lip, "#ffffff", 0.3))
    elif style == "thin":
        svg.line(cx - 6.5 * sc, y + 1, cx + 6.5 * sc, y + 1, lip, 1.7,
                 'stroke-linecap="round"')
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 4.5 * sc, y + 3.2, cx, y + 4.4 * sc, cx + 4.5 * sc,
                  y + 3.2), "none",
                 'stroke="%s" stroke-width="1.1" stroke-linecap="round"'
                 % lip)
    elif style == "wide":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 12 * sc, y - 1, cx, y + 10 * sc, cx + 12 * sc, y - 1,
                  cx, y + 2.2 * sc, cx - 12 * sc, y - 1), "#8c4048")
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 11 * sc, y - 1, cx, y - 4.5 * sc, cx + 11 * sc, y - 1,
                  cx + 7.5 * sc, y + 1.2, cx, y + 1.8 * sc, cx - 7.5 * sc,
                  y + 1.2), "#f4f4f6")
    else:  # pout: small puckered lips, pronounced lower lip
        svg.ellipse(cx, y + 0.5, 6 * sc, 3.8 * sc, lip)
        svg.ellipse(cx, y + 2 * sc, 4 * sc, 2 * sc,
                    _mix(lip, "#ffffff", 0.22))
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - 5 * sc, y - 0.2, cx, y + 2.2 * sc, cx + 5 * sc,
                  y - 0.2), "none",
                 'stroke="%s" stroke-width="1.3" stroke-linecap="round"'
                 % dark)


def _draw_marks(svg, g, app, cat, skin, view):
    marks = app.get("marks") or []
    if isinstance(marks, str):
        marks = [marks]
    if not isinstance(marks, list):
        return
    cx, cy = g["cx"], g["eye_y"] + 2
    sc = g["scale"] + 0.2
    if "freckles" in marks:
        dots = [(-15, -2), (-10, 3), (-5, -4), (5, -4), (10, 3), (15, -2),
                (-8, -8), (8, -8), (0, 6)]
        for dx, dy in dots:
            svg.circle(cx + dx * (sc + 0.2), cy + dy + 9, 1.1,
                       _shade(skin, 0.3))
    if "mole" in marks:
        svg.circle(cx + 11 * sc, g["mouth_y"] + 6, 1.5, _shade(skin, 0.55))
    if "beauty_mark" in marks:
        svg.circle(cx - 10 * sc, g["mouth_y"] - 1, 1.4, "#3a2a22")
    if "scar_brow" in marks:
        bx = cx + g["eye_dx"]
        svg.line(bx - 3.5, g["brow_y"] - 1, bx + 3.5, g["brow_y"] + 2.5,
                 _shade(skin, 0.4), 1.4, 'stroke-linecap="round"')
    if "birthmark" in marks:
        svg.ellipse(cx - 13 * sc, cy + 11, 3.8 * sc, 2.8 * sc,
                    _shade(skin, 0.2))


def _draw_wrinkles(svg, g, view, skin_dark):
    level = view.get("wrinkles", 0)
    if level <= 0:
        return
    cx, cy = g["cx"], g["brow_y"]
    rx = g["rx"]
    hairline = g["cy"] - g["ry"] + view.get("recession", 0) * 3.5
    for i in range(level + 1):
        y = cy - 2 - i * 4.5
        if y < hairline + 2:
            continue
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx - rx * 0.5, y, cx, y - 2, cx + rx * 0.5, y),
                 "none", 'stroke="%s" stroke-width="1.1" '
                 'stroke-linecap="round" opacity="0.5"' % skin_dark)
    if level >= 2:
        my = g["mouth_y"]
        for side in (-1, 1):
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (cx + side * 7, g["nose_y"] + 5, cx + side * 12,
                      my - 7, cx + side * 11, my + 1), "none",
                     'stroke="%s" stroke-width="1.2" '
                     'stroke-linecap="round" opacity="0.45"' % skin_dark)
            ex = cx + side * g["eye_dx"]
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (ex - 7.5, g["eye_y"] + 8, ex, g["eye_y"] + 11,
                      ex + 7.5, g["eye_y"] + 8), "none",
                     'stroke="%s" stroke-width="1" stroke-linecap="round" '
                     'opacity="0.4"' % skin_dark)
    if level >= 3:
        for side in (-1, 1):
            ex = cx + side * (g["eye_dx"] + 10)
            for dy in (-2, 1.5, 5):
                svg.line(ex, g["eye_y"] + dy * 0.5, ex + side * 4.5,
                         g["eye_y"] + dy, skin_dark, 1,
                         'stroke-linecap="round" opacity="0.45"')


# ---------------------------------------------------------------------------
# Hair — volume, shade, highlight and texture per hair_texture
# ---------------------------------------------------------------------------

def _scalp_path(g, inflate=5, recession=0, fringe="round", hairline=None):
    """Cap covering the skull. fringe: round|straight|side|none."""
    cx = g["cx"]
    top = (hairline if hairline is not None
           else g["cy"] - g["ry"] + recession * 3.5) - inflate
    # hug the skull: a fixed small overhang so no background wedge shows
    # between the cap edge and the head outline at the temples
    rx = g["rx"] + 1.5
    left, right = cx - rx, cx + rx
    bottom = g["eye_y"] - 7
    if fringe == "straight":
        edge = "L %.1f %.1f L %.1f %.1f" % (right, bottom + 1,
                                            left, bottom + 1)
    elif fringe == "side":
        edge = "L %.1f %.1f L %.1f %.1f L %.1f %.1f" % (
            cx + 9, bottom + 1, cx - 5, bottom - 6, left, bottom - 2)
    elif fringe == "none":
        # hairline sits high and open (slicked back)
        edge = "L %.1f %.1f L %.1f %.1f" % (right, g["brow_y"] - 6,
                                            left, g["brow_y"] - 6)
    else:
        # Rounded fringe running just above the brow line from temple to
        # temple (cubic keeps the edge low at the sides too, so no
        # background wedge opens beside the hair near the ears).
        be = g["brow_y"] - 1
        edge = "C %.1f %.1f %.1f %.1f %.1f %.1f" % (
            right - 4, be, left + 4, be, left, be)
    return ("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f %s Z"
            % (left, bottom - 2, rx, max(10, bottom - top), right,
               bottom - 2, edge))


def _hair_shine(svg, g, inflate=6):
    """Subtle highlight arc on the upper-left of the hair."""
    cx, cy, rx, ry = g["cx"], g["cy"], g["rx"], g["ry"]
    svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f" %
             (cx - rx * 0.75, cy - ry - inflate * 0.4,
              rx + inflate * 0.5, ry * 0.95 + inflate,
              cx + rx * 0.15, cy - ry - inflate * 0.9), "none",
             'stroke="#ffffff" stroke-width="4" stroke-linecap="round" '
             'opacity="0.22"')


def _hair_texture(svg, g, texture, color, inflate=5, length=18):
    """Strand lines showing straight/wavy/curly/coily."""
    cx, cy, rx, ry = g["cx"], g["cy"], g["rx"], g["ry"]
    stroke = _shade(color, 0.3)
    top = cy - ry - inflate * 0.5
    import math
    if texture == "straight":
        for t in (-0.6, -0.3, 0, 0.3, 0.6):
            x = cx + t * (rx + inflate * 0.4)
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (x, top + 6, x + 2, top + 6 + length * 0.6, x,
                      top + 6 + length), "none",
                     'stroke="%s" stroke-width="1.1" opacity="0.55" '
                     'stroke-linecap="round"' % stroke)
    elif texture == "wavy":
        for t in (-0.55, -0.25, 0.05, 0.35, 0.65):
            x = cx + t * (rx + inflate * 0.4)
            pts = []
            for j in range(7):
                yy = top + 4 + j * length / 6
                xx = x + math.sin(j * math.pi * 0.9) * 2.2
                pts.append((xx, yy))
            svg.add('<polyline points="%s" fill="none" stroke="%s" '
                    'stroke-width="1.1" opacity="0.55" '
                    'stroke-linecap="round"/>' %
                    (" ".join("%.1f,%.1f" % p for p in pts), stroke))
    elif texture == "curly":
        for t in (-0.6, -0.3, 0, 0.3, 0.6):
            x = cx + t * (rx + inflate * 0.4)
            for j in range(3):
                yy = top + 5 + j * length / 3
                svg.path("M %.1f %.1f A 2.4 2.4 0 0 1 %.1f %.1f" %
                         (x - 2.2, yy, x + 2.2, yy), "none",
                         'stroke="%s" stroke-width="1.1" opacity="0.55" '
                         'stroke-linecap="round"' % stroke)
    else:  # coily: tight scallops along the hairline
        for j in range(8):
            a = math.pi + j * math.pi / 7
            x = cx + (rx + 2) * math.cos(a)
            y = (cy - 2) + (ry + 2) * math.sin(a) * 0.9
            svg.circle(x, y, 2.6, "none",
                       'stroke="%s" stroke-width="1.1" opacity="0.6"'
                       % stroke)


def _afro_radius(g, style):
    return g["rx"] + (30 if style == "afro_large" else 19)


def _draw_back_hair(svg, g, style, texture, hair, view):
    cx, cy, rx, ry = g["cx"], g["cy"], g["rx"], g["ry"]
    rec = view.get("recession", 0)
    stage = view["stage"]
    if stage == "baby":
        return  # wispy baby hair drawn in the front layer only
    if style.startswith("afro"):
        r = _afro_radius(g, style)
        svg.circle(cx, cy - 2, r, hair)
        # scalloped edge volume
        import math
        for i in range(11):
            a = math.pi + i * math.pi / 10
            x = cx + r * math.cos(a)
            y = (cy - 2) + r * math.sin(a) * 0.96
            svg.circle(x, y, r * 0.24 + 2, hair)
        _hair_shine(svg, g, inflate=r - rx + 4)
        return
    if style in ("long_straight", "long_wavy", "long_curly"):
        length = 52
        if style == "long_straight":
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                     "L %.1f %.1f C %.1f %.1f %.1f %.1f %.1f %.1f Z" %
                     (cx - rx - 5, cy - ry * 0.5, cx - rx - 12, cy,
                      cx - rx - 8, cy + ry * 0.4 + length, cx - rx - 8,
                      cy + ry * 0.4 + length, cx + rx + 8,
                      cy + ry * 0.4 + length, cx + rx + 8, cy + ry * 0.4,
                      cx + rx + 12, cy, cx + rx + 5, cy - ry * 0.5), hair)
        else:
            wav = 5 if style == "long_wavy" else 8
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f C %.1f %.1f %.1f %.1f "
                     "%.1f %.1f L %.1f %.1f C %.1f %.1f %.1f %.1f %.1f %.1f "
                     "Q %.1f %.1f %.1f %.1f Z" %
                     (cx - rx - 5, cy - ry * 0.5, cx - rx - 13, cy,
                      cx - rx - 9, cy + ry * 0.4,
                      cx - rx - 13 - wav, cy + ry + 18, cx - rx - 3,
                      cy + ry + 34, cx - rx - 8, cy + ry * 0.4 + length,
                      cx + rx + 8, cy + ry * 0.4 + length,
                      cx + rx + 3, cy + ry + 34, cx + rx + 13 + wav,
                      cy + ry + 18, cx + rx + 9, cy + ry * 0.4,
                      cx + rx + 13, cy, cx + rx + 5, cy - ry * 0.5), hair)
        return
    if style in ("wavy_bob", "bob"):
        length = 14 if style == "bob" else 18
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - rx - 6, cy - ry * 0.4, cx - rx - 12, cy + ry * 0.5,
                  cx - rx - 5, cy + ry + length, cx + rx + 5,
                  cy + ry + length, cx + rx + 12, cy + ry * 0.5,
                  cx + rx + 6, cy - ry * 0.4), hair)
        return
    if style in ("braids", "locs"):
        # Continuous tapered strands anchored to the scalp, drawn as thick
        # round-capped strokes; never disconnected decorative circles.
        dark = _shade(hair, 0.25)
        light = _shade(hair, -0.3)
        n_side = 3
        for side in (-1, 1):
            for k in range(n_side):
                spread = (k - (n_side - 1) / 2) * 7
                x0 = cx + side * (rx * 0.55 + 6) + spread
                y0 = cy - ry * 0.55 + abs(spread) * 0.3
                x1 = cx + side * (rx + 12 + abs(spread) * 0.8)
                y1 = cy + ry + 38 + (k % 2) * 5
                xm = (x0 + x1) / 2 + side * 5
                ym = (y0 + y1) / 2
                d = "M %.1f %.1f Q %.1f %.1f %.1f %.1f" % (x0, y0, xm, ym,
                                                            x1, y1)
                if style == "locs":
                    # thick continuous rope with subtle irregular texture
                    svg.path(d, "none",
                             'stroke="%s" stroke-width="9" '
                             'stroke-linecap="round"' % hair)
                    svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                             (x0 + side * 1.5, y0 + 10, xm + side * 2,
                              ym, x1 + side * 1, y1 - 8), "none",
                             'stroke="%s" stroke-width="1.6" '
                             'stroke-linecap="round" opacity="0.55"' % dark)
                    svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                             (x0 - side * 1.5, y0 + 6, xm - side * 1.5,
                              ym - 6, x1 - side * 2, y1 - 14), "none",
                             'stroke="%s" stroke-width="1.4" '
                             'stroke-linecap="round" opacity="0.5"' % light)
                else:
                    # braid: continuous strand + interwoven chevrons
                    svg.path(d, "none",
                             'stroke="%s" stroke-width="7.5" '
                             'stroke-linecap="round"' % hair)
                    steps = 6
                    for j in range(steps):
                        t0 = (j + 0.5) / steps
                        # point along the quadratic curve
                        bx = ((1 - t0) ** 2) * x0 + 2 * (1 - t0) * t0 * xm                             + t0 ** 2 * x1
                        by = ((1 - t0) ** 2) * y0 + 2 * (1 - t0) * t0 * ym                             + t0 ** 2 * y1
                        dxdt = 2 * (1 - t0) * (xm - x0) + 2 * t0 * (x1 - xm)
                        dydt = 2 * (1 - t0) * (ym - y0) + 2 * t0 * (y1 - ym)
                        ll = (dxdt ** 2 + dydt ** 2) ** 0.5 or 1
                        px, py = -dydt / ll, dxdt / ll  # unit perpendicular
                        hh = 4.2
                        svg.line(bx - px * hh, by - py * hh, bx + px * hh,
                                 by + py * hh, dark, 2.2,
                                 'stroke-linecap="round" opacity="0.8"')
                    # hair tie at the end
                    svg.circle(x1, y1, 2.4, "#d9b45b")
        return
    if style in ("ponytail", "high_ponytail"):
        base_y = cy - ry * 0.55 if style == "ponytail" else cy - ry - 4
        tx = cx + rx * 0.5
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (tx, base_y, cx + rx + 22, base_y + 8, cx + rx + 14,
                  cy + 20, cx + rx * 0.6, base_y + 6, cx + 4,
                  base_y - 6), hair)
        svg.circle(tx, base_y, 4.5, _shade(hair, 0.25))
        return


def _draw_front_hair(svg, g, app, cat, hair, view):
    style = _resolve(cat, "hair_style", app.get("hair_style"))["id"]
    texture = _resolve(cat, "hair_texture",
                       app.get("hair_texture"))["id"]
    rec = view.get("recession", 0)
    stage = view["stage"]
    cx, cy, rx, ry = g["cx"], g["cy"], g["rx"], g["ry"]
    dark = _shade(hair, 0.2)

    # Babies (0-2): only short wispy hair of their colour, whatever is stored.
    if stage == "baby":
        for t in (-0.5, -0.2, 0.15, 0.45):
            x = cx + t * rx
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (x - 4, cy - ry + 3, x, cy - ry - 7, x + 5,
                      cy - ry + 2), "none",
                     'stroke="%s" stroke-width="2.6" stroke-linecap="round" '
                     'opacity="0.9"' % hair)
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f" %
                 (cx - rx * 0.8, cy - ry * 0.55, rx * 0.95, ry * 0.95,
                  cx + rx * 0.8, cy - ry * 0.55), "none",
                 'stroke="%s" stroke-width="3" stroke-linecap="round" '
                 'opacity="0.55"' % hair)
        return

    # recession (0-3) raises the hairline only; bald stays a style choice
    if stage in ("adult", "elder") and rec >= 2 and style == "buzz":
        style = "short_crop"  # a buzz cannot show a receding line
    if stage in ("baby", "child") and style in (
            "man_bun", "slick_back", "undercut"):
        style = "short_crop"

    if style == "bald":
        return
    if style == "buzz":
        svg.path(_scalp_path(g, inflate=1.5, recession=rec), hair)
        return
    if style == "short_crop":
        svg.path(_scalp_path(g, inflate=5, recession=rec), hair)
        _hair_texture(svg, g, texture, hair, inflate=5, length=13)
        _hair_shine(svg, g)
        for side in (-1, 1):
            svg.add('<rect x="%.1f" y="%.1f" width="4.5" height="11" rx="2" '
                    'fill="%s"/>' % (cx + side * (rx + 1) - 2.2,
                                     cy - ry * 0.3, hair))
        return
    if style == "side_part":
        svg.path(_scalp_path(g, inflate=5, recession=rec, fringe="side"),
                 hair)
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                 (cx + 10, cy - ry - 3, cx - 2, cy - ry * 0.3, cx - 4,
                  g["eye_y"] - 8), "none",
                 'stroke="%s" stroke-width="1.5" opacity="0.7"' % dark)
        _hair_shine(svg, g)
        return
    if style == "bangs":
        svg.path(_scalp_path(g, inflate=5, recession=rec, fringe="straight"),
                 hair)
        _hair_shine(svg, g)
        return
    if style == "curtains":
        # middle part, two strands framing the face
        svg.path(_scalp_path(g, inflate=5, recession=rec, fringe="none",
                             hairline=g["brow_y"] - 10), hair)
        for side in (-1, 1):
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                     "Q %.1f %.1f %.1f %.1f Z" %
                     (cx + side * 2, cy - ry - 2,
                      cx + side * (rx + 7), cy - ry * 0.4,
                      cx + side * (rx + 3), cy + 8,
                      cx + side * (rx - 3), cy + 2,
                      cx + side * (rx - 2), cy - ry * 0.5,
                      cx + side * 2, cy - ry - 2), hair)
        svg.line(cx, cy - ry - 4, cx, cy - ry * 0.3, dark, 1.4,
                 'opacity="0.7"')
        return
    if style == "slick_back":
        # visible hairline, volume swept upward and back
        svg.path(_scalp_path(g, inflate=6, recession=rec, fringe="none",
                             hairline=g["brow_y"] - 8), hair)
        for t in (-0.5, -0.15, 0.2, 0.55):
            x = cx + t * rx
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
                     (x, g["brow_y"] - 4, x + 3, cy - ry - 6, x + 8,
                      cy - ry - 2), "none",
                     'stroke="%s" stroke-width="1.2" opacity="0.6" '
                     'stroke-linecap="round"' % dark)
        _hair_shine(svg, g, inflate=8)
        return
    if style == "undercut":
        # shaved sides + longer top
        svg.path(_scalp_path(g, inflate=1.5, recession=rec), hair)
        svg.path(_scalp_path(g, inflate=6, recession=rec, fringe="round",
                             hairline=g["brow_y"] - 12), hair)
        for side in (-1, 1):
            for k in range(3):
                yy = cy - ry * 0.35 + k * 5
                svg.line(cx + side * (rx + 1), yy, cx + side * (rx - 2),
                         yy + 2, _shade(svg._skin, 0.25), 1)
        return
    if style.startswith("afro"):
        # front: curly band across the hairline only — never the face
        r = _afro_radius(g, style)
        band_y = g["eye_y"] - 9
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f L %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - r * 0.82, band_y + 3, r * 0.82,
                  r * 0.82, cx + r * 0.82, band_y + 3, cx + rx * 0.7,
                  band_y - 6, cx, band_y - 12, cx - rx * 0.7, band_y - 6),
                 hair)
        return
    if style in ("long_straight", "long_wavy", "long_curly", "wavy_bob",
                 "bob"):
        # front strands framing the face + crown
        length_end = cy + 26 if style in ("wavy_bob", "bob") else cy + 34
        for side in (-1, 1):
            x = cx + side * (rx - 1)
            svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                     "Q %.1f %.1f %.1f %.1f Z" %
                     (x, cy - ry * 0.55, cx + side * (rx + 8),
                      cy - ry * 0.05, cx + side * (rx + 6), length_end - 18,
                      cx + side * (rx - 3), length_end,
                      cx + side * (rx - 5), cy + 4,
                      x, cy - ry * 0.55), hair)
        svg.path(_scalp_path(g, inflate=4, recession=rec), hair)
        _hair_shine(svg, g, inflate=4)
        if style in ("long_straight", "bob"):
            _hair_texture(svg, g, "straight", hair, inflate=4, length=24)
        elif style == "long_curly":
            _hair_texture(svg, g, "curly", hair, inflate=4, length=22)
        return
    if style in ("braids", "locs"):
        svg.path(_scalp_path(g, inflate=4, recession=rec), hair)
        svg.line(cx, cy - ry - 2, cx, cy - ry * 0.4, dark, 1.4,
                 'opacity="0.6"')
        _hair_shine(svg, g, inflate=4)
        return
    if style in ("bun", "man_bun"):
        svg.path(_scalp_path(g, inflate=4, recession=rec), hair)
        bun_y = cy - ry - (8 if style == "bun" else 10)
        svg.circle(cx, bun_y, 11 if style == "bun" else 9, hair)
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f" %
                 (cx - 7, bun_y - 3, 8.5, 8.5, cx + 7, bun_y - 3), "none",
                 'stroke="%s" stroke-width="1.5" opacity="0.6"' % dark)
        return
    if style in ("ponytail", "high_ponytail"):
        svg.path(_scalp_path(g, inflate=4, recession=rec), hair)
        _hair_shine(svg, g, inflate=4)
        return
    # fallback
    svg.path(_scalp_path(g, inflate=5, recession=rec), hair)


def _hair_top_y(g, style):
    """Top of the visible hair volume — hats sit on this."""
    if style.startswith("afro"):
        return g["cy"] - 2 - _afro_radius(g, style)
    if style in ("bun", "man_bun"):
        return g["cy"] - g["ry"] - (19 if style == "bun" else 17)
    if style in ("ponytail", "high_ponytail"):
        return g["cy"] - g["ry"] - 12
    return g["cy"] - g["ry"] - 6


# ---------------------------------------------------------------------------
# Facial hair
# ---------------------------------------------------------------------------

def _draw_facial_hair(svg, g, app, cat, hair, view):
    style = _resolve(cat, "facial_hair", app.get("facial_hair"))["id"]
    if style == "none":
        return
    if view.get("stage") in ("baby", "child"):
        return  # children get no facial hair
    cx, my = g["cx"], g["mouth_y"]
    sc = g["scale"] + 0.3
    dark = _shade(hair, 0.08)
    jaw_y = my + 13 * sc
    if style == "stubble":
        svg.ellipse(cx, my + 10 * sc, 16 * sc, 11 * sc, dark,
                    'opacity="0.3"')
        return
    if style == "mustache":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 10 * sc, my - 2.5, cx - 3.5 * sc, my - 6.5 * sc, cx,
                  my - 3, cx + 3.5 * sc, my - 6.5 * sc, cx + 10 * sc,
                  my - 2.5, cx + 3.5 * sc, my + 0.5, cx - 3.5 * sc,
                  my + 0.5), dark)
        return
    if style == "goatee":
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 6.5 * sc, jaw_y - 4, cx, jaw_y + 7 * sc, cx + 6.5 * sc,
                  jaw_y - 4, cx, jaw_y, cx - 6.5 * sc, jaw_y - 4), dark)
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - 6.5 * sc, my - 2.5, cx - 2.5 * sc, my - 5 * sc, cx,
                  my - 2.8, cx + 2.5 * sc, my - 5 * sc, cx + 6.5 * sc,
                  my - 2.5, cx + 2.5 * sc, my - 0.3, cx - 2.5 * sc,
                  my - 0.3), dark)
        return
    depth = 9 * sc if style == "beard_short" else 15 * sc
    svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f "
             "Q %.1f %.1f %.1f %.1f Q %.1f %.1f %.1f %.1f Z" %
             (cx - 14 * sc, my - 3, cx - 16.5 * sc, my + 9 * sc,
              cx - 9.5 * sc, my + depth, cx, my + depth + 4.5 * sc,
              cx + 9.5 * sc, my + depth, cx + 16.5 * sc, my + 9 * sc,
              cx + 14 * sc, my - 3, cx, my + 9 * sc, cx - 14 * sc, my - 3),
             dark)
    svg.ellipse(cx, my + 1, 7.5 * sc, 3.6 * sc, svg._skin)


# ---------------------------------------------------------------------------
# Accessories (gated to age 3+; babies never wear accessories)
# ---------------------------------------------------------------------------

def _draw_glasses(svg, g, app, cat, view):
    if view.get("stage") == "baby":
        return
    style = (app.get("accessories") or {}).get("glasses", "none")
    style = _resolve(cat, "glasses", style)["id"]
    if style == "none":
        return
    cx, cy, dx = g["cx"], g["eye_y"], g["eye_dx"]
    ew = 11 * g["scale"] + 4
    eh = 9 * g["scale"] + 3
    frame = "#2e3138"
    lens = "rgba(205,228,246,0.4)"
    if style in ("round", "square", "rectangle", "oval"):
        rw = ew * (1.3 if style == "rectangle" else 1.0)
        rh = eh * (0.8 if style == "rectangle" else 1.0)
        for side in (-1, 1):
            ex = cx + side * dx
            if style == "round":
                svg.circle(ex, cy, ew, lens,
                           'stroke="%s" stroke-width="2.4"' % frame)
            elif style in ("square", "rectangle"):
                svg.add('<rect x="%.1f" y="%.1f" width="%.1f" '
                        'height="%.1f" rx="3.5" fill="%s" '
                        'stroke="%s" stroke-width="2.4"/>' %
                        (ex - rw, cy - rh * 0.85, rw * 2, rh * 1.7,
                         lens, frame))
            else:
                svg.ellipse(ex, cy, ew * 1.1, eh * 0.9, lens,
                            'stroke="%s" stroke-width="2.4"' % frame)
            svg.line(ex + side * (rw if style in ("square", "rectangle")
                                  else ew), cy, cx + side * (g["rx"] + 3),
                     g["ear_y"] - 2, frame, 2)
    else:  # cat_eye
        for side in (-1, 1):
            ex = cx + side * dx
            svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                     (ex - ew, cy + eh * 0.5, ex - ew * 0.75, cy - eh * 1.05,
                      ex + ew * 0.95, cy - eh * 0.8, ex + ew, cy + eh * 0.5),
                     lens, 'stroke="%s" stroke-width="2.4" '
                     'stroke-linejoin="round"' % frame)
            svg.line(ex + side * ew, cy, cx + side * (g["rx"] + 3),
                     g["ear_y"] - 2, frame, 2)
    svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f" %
             (cx - dx + ew * 0.6, cy - eh * 0.65, cx, cy - eh * 0.95,
              cx + dx - ew * 0.6, cy - eh * 0.65), "none",
             'stroke="%s" stroke-width="2.4" stroke-linecap="round"' % frame)


def _draw_earrings(svg, g, app, cat, view):
    if view.get("stage") == "baby":
        return
    style = (app.get("accessories") or {}).get("earrings", "none")
    style = _resolve(cat, "earrings", style)["id"]
    if style == "none":
        return
    y = g["ear_y"] + 5
    metal = "#d9b45b"
    for side in (-1, 1):
        x = g["cx"] + side * (g["rx"] * 0.97 + 2)
        if style == "studs":
            svg.circle(x, y, 2.4, metal)
        elif style == "clip_ons":
            svg.circle(x, y, 2.4, metal)
            svg.circle(x, y - 2.6, 1.4, metal)
        elif style == "hoops":
            svg.circle(x, y + 3.5, 5, "none",
                       'stroke="%s" stroke-width="2"' % metal)
        else:  # dangles
            svg.line(x, y - 1, x, y + 5.5, metal, 1.5)
            svg.path("M %.1f %.1f L %.1f %.1f L %.1f %.1f Z" %
                     (x - 2.4, y + 5.5, x + 2.4, y + 5.5, x, y + 12), metal)


def _draw_headwear(svg, g, app, cat, hair_style, view):
    if view.get("stage") == "baby":
        return  # no hats on babies
    style = (app.get("accessories") or {}).get("headwear", "none")
    style = _resolve(cat, "headwear", style)["id"]
    if style == "none":
        return
    cx = g["cx"]
    top = _hair_top_y(g, hair_style)  # hats sit on the hair volume
    rx = g["rx"]
    brow = g["brow_y"]
    if style == "cap":
        dome_top = top - 4
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f Z" %
                 (cx - rx - 5, brow - 2, rx + 5,
                  max(12, brow - 2 - dome_top), cx + rx + 5, brow - 2),
                 "#2f6fed")
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f Z" %
                 (cx + rx * 0.05, dome_top + 6, cx + rx + 24, brow - 6,
                  cx + rx + 24, brow + 1, cx + 2, brow - 3), "#2f6fed")
        svg.circle(cx, dome_top + 2, 3.2, _shade("#2f6fed", 0.2))
    elif style == "beanie":
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f L %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - rx - 6, brow + 1, rx + 6,
                  max(12, brow + 1 - (top - 6)), cx + rx + 6, brow + 1,
                  cx + rx + 6, brow + 7, cx, brow + 11, cx - rx - 6,
                  brow + 7), "#c94f6d")
        svg.add('<rect x="%.1f" y="%.1f" width="%.1f" height="9" rx="4" '
                'fill="%s"/>' % (cx - rx - 6, brow - 3, 2 * rx + 12,
                                 _shade("#c94f6d", 0.15)))
        svg.circle(cx, top - 3, 5.5, "#f2f2f4")
    elif style == "headscarf":
        edge_y = brow + 4  # always above the eyes
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx - rx - 7, brow - 4, rx + 7,
                  max(12, brow - 4 - (top - 4)), cx + rx + 7, brow - 4,
                  cx + rx + 2, edge_y, cx - rx - 2, edge_y), "#e8945a")
        svg.path("M %.1f %.1f Q %.1f %.1f %.1f %.1f L %.1f %.1f "
                 "Q %.1f %.1f %.1f %.1f Z" %
                 (cx + rx - 2, top + 2, cx + rx + 17, top + 8,
                  cx + rx + 13, edge_y + 10, cx + rx - 2, edge_y,
                  cx + rx - 8, top + 8, cx + rx - 2, top + 2),
                 _shade("#e8945a", 0.12))
    elif style == "sun_hat":
        svg.ellipse(cx, brow + 2, rx + 24, 9, "#e8c95a")
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f Z" %
                 (cx - rx * 0.75, brow + 2, rx * 0.75,
                  max(12, brow + 2 - (top - 2)), cx + rx * 0.75, brow + 2),
                 "#e8c95a")
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f" %
                 (cx - rx * 0.77, brow - 2, rx * 0.77,
                  max(12, brow - 2 - (top - 2)), cx + rx * 0.77, brow - 2),
                 "none", 'stroke="#c94f6d" stroke-width="5"')
    elif style == "headband":
        svg.path("M %.1f %.1f A %.1f %.1f 0 0 1 %.1f %.1f" %
                 (cx - rx - 4, brow + 3, rx + 4,
                  max(12, brow + 3 - (top + 2)), cx + rx + 4, brow + 3),
                 "none", 'stroke="#c94f6d" stroke-width="7" '
                 'stroke-linecap="round"')
    elif style == "beret":
        svg.ellipse(cx - 5, top + 2, rx * 0.95, 13, "#8a63b8")
        svg.circle(cx - 5, top - 9, 2.6, _shade("#8a63b8", 0.25))


# ---------------------------------------------------------------------------
# Main entry point
# ---------------------------------------------------------------------------

def render_avatar_svg(appearance, age, size=128):
    """Render an avatar as a deterministic layered SVG string.

    appearance: dict in the contract shape (04-appearance-contract.md).
    age: integer years. size: output width/height in px (8..512).
    """
    if not isinstance(appearance, dict):
        appearance = {}
    if isinstance(age, bool):
        age = 0
    age = max(0, min(120, int(age)))
    size = max(8, min(512, int(size)))
    cat = get_catalog()
    view = aged_view(appearance, age)
    g = _stage_geometry(view["stage"])

    skin = _resolve(cat, "skin_tone", appearance.get("skin_tone")).get(
        "hex", "#e4bd93")
    skin_dark = _shade(skin, 0.2)
    hair = view.get("hair_hex") or _resolve(
        cat, "hair_color", appearance.get("hair_color")).get(
        "hex", "#6b4a2c")
    outfit_color = _resolve(cat, "outfit_color",
                            appearance.get("outfit_color")).get(
        "hex", "#3b6ea5")
    svg = _SVG()
    svg._skin = skin
    svg._brow_color = _mix("#4a3a2c", hair, 0.7)

    face_shape = _resolve(cat, "face_shape",
                          appearance.get("face_shape"))["id"]
    hair_style = _resolve(cat, "hair_style",
                          appearance.get("hair_style"))["id"]
    hair_texture = _resolve(cat, "hair_texture",
                            appearance.get("hair_texture"))["id"]

    _draw_background(svg, g, outfit_color)
    _draw_back_hair(svg, g, hair_style, hair_texture, hair, view)
    _draw_outfit(svg, g, appearance, cat, skin, view)
    _draw_ears(svg, g, skin, skin_dark)
    _draw_face_base(svg, g, skin, skin_dark, face_shape, view)
    _draw_eyes(svg, g, appearance, cat, skin)
    _draw_nose(svg, g, appearance, cat, skin_dark)
    _draw_mouth(svg, g, appearance, cat)
    _draw_marks(svg, g, appearance, cat, skin, view)
    _draw_wrinkles(svg, g, view, skin_dark)
    _draw_front_hair(svg, g, appearance, cat, hair, view)
    _draw_facial_hair(svg, g, appearance, cat, hair, view)
    _draw_glasses(svg, g, appearance, cat, view)
    _draw_earrings(svg, g, appearance, cat, view)
    _draw_headwear(svg, g, appearance, cat, hair_style, view)

    return ('<svg xmlns="http://www.w3.org/2000/svg" viewBox="%s" '
            'width="%d" height="%d" role="img" aria-label="avatar">%s'
            '</svg>' % (VIEWBOX, size, size, svg.render()))
