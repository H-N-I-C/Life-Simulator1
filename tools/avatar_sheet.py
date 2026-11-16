"""Generate labelled avatar contact sheets as PNG evidence.

Renders every catalog option through src/appearance/render.py, lays the
avatars out in labelled SVG grids, and rasterises the sheets with
rsvg-convert. Fully deterministic: no randomness, no wall-clock input.

Sheets (in docs/collab/evidence/avatars/):
  - skin-age matrix      12 skin tones x 5 age stages
  - hair x headwear      every hair style under every hat
  - one sheet per face feature category (face_shape, eye_shape, brows,
    nose, mouth, eye_color, hair_texture, hair_color, facial_hair, marks)
  - outfits              outfit_style x outfit_color
  - accessories          glasses x earrings
Every sheet is produced at 32 px and 128 px cell sizes.

Run: python3 tools/avatar_sheet.py
"""

import html
import os
import re
import subprocess
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.appearance import catalog  # noqa: E402
from src.appearance.render import render_avatar_svg  # noqa: E402

OUT_DIR = os.path.join(ROOT, "docs", "collab", "evidence", "avatars")

AGES = [1, 8, 16, 30, 70]  # baby, child, teen, adult, elder
FONT = "Noto Sans"
LABEL_H = 16
PAD = 6

# A neutral adult base so one varying option is clearly visible per cell.
BASE = {
    "skin_tone": "skin_04", "face_shape": "oval", "eye_shape": "almond",
    "eye_color": "brown_dark", "brows": "soft", "nose": "button",
    "mouth": "smile", "hair_texture": "straight",
    "hair_color": "brown_medium", "hair_style": "short_crop",
    "facial_hair": "none", "outfit_style": "tee", "outfit_color": "blue",
    "marks": [], "accessories": {"glasses": "none", "headwear": "none",
                                 "earrings": "none"},
}


def app_with(**over):
    app = dict(BASE)
    app["accessories"] = dict(BASE["accessories"])
    app.update(over)
    return app


def inner_svg(avatar_svg, size):
    """Extract the rendered <svg> and resize its viewport to size px."""
    m = re.match(r'<svg[^>]*>(.*)</svg>$', avatar_svg, re.S)
    body = m.group(1)
    return ('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 200 200" '
            'width="%d" height="%d">%s</svg>' % (size, size, body))


def cell_svg(app, age, size, label, width):
    off = max(0.0, (width - size) / 2)
    return ('<g transform="translate(%.1f,%d)">%s'
            '<text x="%d" y="%d" font-family="%s" font-size="11" '
            'text-anchor="middle" fill="#333">%s</text></g>'
            % (off, LABEL_H, inner_svg(render_avatar_svg(app, age, size), size),
               size / 2, LABEL_H - 4, FONT, html.escape(label)))


def sheet(name, columns, size):
    """columns: list of (column header, [ (label, app, age), ... ])."""
    n_rows = max(len(cells) for _, cells in columns)
    # Column width must fit the longest label or header, not just the avatar.
    def text_w(s):
        return int(6.2 * len(s)) + 8
    col_w = size + PAD
    col_ws = [max(col_w, text_w(header),
                  max((text_w(label) for label, _, _ in cells), default=0))
              for header, cells in columns]
    row_h = size + LABEL_H + PAD
    hdr_h = 18  # header text + padding; rows start right below it
    w = sum(col_ws) + PAD
    h = PAD + hdr_h + n_rows * row_h + PAD
    parts = ['<svg xmlns="http://www.w3.org/2000/svg" width="%d" height="%d" '
             'font-family="%s">' % (w, h, FONT),
             '<rect width="100%%" height="100%%" fill="#ffffff"/>'
             % ()]
    for ci, ((header, cells), cw) in enumerate(zip(columns, col_ws)):
        x = PAD + sum(col_ws[:ci])
        parts.append('<text x="%d" y="%d" font-family="%s" font-size="12" '
                     'font-weight="bold" text-anchor="middle" fill="#111">'
                     '%s</text>'
                     % (x + cw / 2, PAD + 11, FONT, html.escape(header)))
        for ri, (label, app, age) in enumerate(cells):
            y = PAD + hdr_h + ri * row_h
            parts.append('<g transform="translate(%d,%d)">%s</g>'
                         % (x, y, cell_svg(app, age, size, label, cw)))
    parts.append("</svg>")
    return "".join(parts), w, h


def option_sheet(category, age=30):
    """One column per catalog entry of a single category."""
    col = []
    for e in catalog.CATEGORIES[category]:
        kw = {category: e["id"]}
        if category in catalog.ACCESSORY_CATEGORIES:
            acc = dict(BASE["accessories"])
            acc[category] = e["id"]
            kw = {"accessories": acc}
        if category == "marks":
            kw = {"marks": [e["id"]]}
        col.append((e["label"], app_with(**kw), age))
    return [(catalog_key_title(category), col)]


def catalog_key_title(category):
    return category.replace("_", " ").title()


def hair_hats_columns():
    cols = []
    for h in catalog.CATEGORIES["headwear"]:
        acc = dict(BASE["accessories"])
        acc["headwear"] = h["id"]
        cells = [(s["label"], app_with(hair_style=s["id"], accessories=acc),
                  30) for s in catalog.CATEGORIES["hair_style"]]
        cols.append((h["label"], cells))
    return cols


def outfit_columns():
    cols = []
    for c in catalog.CATEGORIES["outfit_color"]:
        cells = [(s["label"],
                  app_with(outfit_style=s["id"], outfit_color=c["id"]), 30)
                 for s in catalog.CATEGORIES["outfit_style"]]
        cols.append((c["label"], cells))
    return cols


def accessory_columns():
    cols = []
    for g in catalog.CATEGORIES["glasses"]:
        acc = dict(BASE["accessories"])
        acc["glasses"] = g["id"]
        cells = []
        for e in catalog.CATEGORIES["earrings"]:
            a2 = dict(acc)
            a2["earrings"] = e["id"]
            cells.append((e["label"], app_with(accessories=a2), 30))
        cols.append((g["label"], cells))
    return cols


def marks_columns():
    col = [("All marks", app_with(marks=catalog.all_ids("marks")), 30)]
    for e in catalog.CATEGORIES["marks"]:
        col.append((e["label"], app_with(marks=[e["id"]]), 30))
    return [("Marks", col)]


def matrix_columns():
    cols = []
    for skin in catalog.CATEGORIES["skin_tone"]:
        cells = [(str(a), app_with(skin_tone=skin["id"]), a) for a in AGES]
        cols.append((skin["label"], cells))
    return cols


def lifecycle_columns():
    ages = [0, 1, 2, 5, 10, 13, 16, 21, 30, 45, 60, 70, 85, 100]
    return [("Age", [(str(a), app_with(), a) for a in ages])]


def build_sheets():
    sheets = {
        "matrix": matrix_columns,
        "lifecycle": lifecycle_columns,
        "hair-headwear": hair_hats_columns,
        "face-shape": lambda: option_sheet("face_shape"),
        "eye-shape": lambda: option_sheet("eye_shape"),
        "eye-color": lambda: option_sheet("eye_color"),
        "brows": lambda: option_sheet("brows"),
        "nose": lambda: option_sheet("nose"),
        "mouth": lambda: option_sheet("mouth"),
        "hair-texture": lambda: option_sheet("hair_texture", age=8),
        "hair-color": lambda: option_sheet("hair_color"),
        "facial-hair": lambda: option_sheet("facial_hair"),
        "marks": marks_columns,
        "outfits": outfit_columns,
        "accessories": accessory_columns,
    }
    return sheets


def main():
    os.makedirs(OUT_DIR, exist_ok=True)
    sheets = build_sheets()
    written = []
    for size in (32, 128):
        for name, builder in sheets.items():
            svg, w, h = sheet(name, builder(), size)
            svg_path = os.path.join(OUT_DIR, "_tmp_%s_%d.svg" % (name, size))
            png_path = os.path.join(OUT_DIR, "%s_%dpx.png" % (name, size))
            with open(svg_path, "w") as f:
                f.write(svg)
            subprocess.run(["rsvg-convert", "-w", str(w), "-h", str(h),
                            "-o", png_path, svg_path], check=True)
            os.remove(svg_path)
            written.append(os.path.basename(png_path))
            print("wrote %s (%dx%d)" % (png_path, w, h))
    print("%d sheets written to %s" % (len(written), OUT_DIR))
    return 0


if __name__ == "__main__":
    sys.exit(main())
