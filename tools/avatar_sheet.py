"""Generate labelled avatar contact sheets as PNG evidence (M0b Fluent).

Composes sheets from the downloaded Fluent 3D assets
(web/static/avatars via tools/fetch_fluent_avatars.py) using the
manifest's published combo key space. The legacy SVG-renderer sheets
were removed when the renderer was retired.

Deterministic: no randomness, no wall-clock input.
Sheets (docs/collab/evidence/avatars/): fluent_matrix (stage x skin),
fluent_hair, fluent_looks, fluent_extras.

Run: python3 tools/avatar_sheet.py
"""

import json
import os
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

OUT_DIR = os.path.join(ROOT, "docs", "collab", "evidence", "avatars")


def fluent_sheets():
    """Compose contact sheets from the Fluent 3D asset set (M0b)."""
    import PIL.Image
    import PIL.ImageDraw
    import PIL.ImageFont

    av_dir = os.path.join(ROOT, "web", "static", "avatars")
    with open(os.path.join(av_dir, "manifest.json")) as f:
        man = json.load(f)
    combos = man["combos"]

    def img_for(combo):
        chain = combos.get(combo)
        if not chain:
            return None
        return PIL.Image.open(os.path.join(av_dir,
                                           man["assets"][chain[0]]))

    font = PIL.ImageFont.truetype(
        "/usr/share/fonts/noto/NotoSans-Regular.ttf", 16)
    font_b = PIL.ImageFont.truetype(
        "/usr/share/fonts/noto/NotoSans-Regular.ttf", 18)
    CELL = 128
    LABEL = 22

    def sheet(name, columns):
        """columns: list of (column header, [ (label, combo), ... ])."""
        n_rows = max(len(cells) for _, cells in columns)
        col_ws = []
        for header, cells in columns:
            w = CELL + 8
            for label, _ in cells:
                w = max(w, int(font.getlength(label)) + 12)
            w = max(w, int(font_b.getlength(header)) + 12)
            col_ws.append(w)
        hdr_h = 28
        row_h = CELL + LABEL + 6
        W = sum(col_ws) + 8
        H = hdr_h + n_rows * row_h + 8
        canvas = PIL.Image.new("RGB", (W, H), "white")
        draw = PIL.ImageDraw.Draw(canvas)
        x = 4
        for (header, cells), cw in zip(columns, col_ws):
            draw.text((x + cw / 2, 4), header, font=font_b, fill="black",
                      anchor="ma")
            for ri, (label, combo) in enumerate(cells):
                y = hdr_h + ri * row_h
                im = img_for(combo)
                if im is not None:
                    canvas.paste(im.resize((CELL, CELL)), (x + (cw - CELL) // 2,
                                                           y), im.resize((CELL, CELL)))
                draw.text((x + cw / 2, y + CELL + 2), label, font=font,
                          fill="#333333", anchor="ma")
            x += cw
        out = os.path.join(OUT_DIR, name + ".png")
        canvas.save(out, optimize=True)
        print("wrote %s (%dx%d)" % (out, W, H))

    skins = man["skins"]
    stages = [("Baby", "person-baby-%s"), ("Child", "person-child-%s"),
              ("teen_adult", "person-teen_adult-%s"),
              ("Greying", "person-greying-%s"),
              ("Elder", "person-elder-%s")]
    sheet("fluent_matrix",
          [(s, [(sk, pat % sk) for sk in skins]) for s, pat in stages]
          + [("adult " + b,
              [(sk, "%s-teen_adult-%s" % (b, sk)) for sk in skins])
             for b in ("woman", "man")]
          + [("elder " + b,
              [(sk, "%s-elder-%s" % (b, sk)) for sk in skins])
             for b in ("woman", "man")])

    hairs = ["default", "curly", "red", "white", "bald"]
    sheet("fluent_hair",
          [("woman: " + h, [(sk, "woman-teen_adult-%s-hair-%s" % (sk, h))
                            for sk in skins]) for h in hairs]
          + [("man: " + h, [(sk, "man-teen_adult-%s-hair-%s" % (sk, h))
                            for sk in skins]) for h in hairs])

    sheet("fluent_looks",
          [(lk.replace("_", " "),
            [(sk, "person-teen_adult-%s-look-%s" % (sk, lk)) for sk in skins])
           for lk in man["looks"]])

    extras = [("beard (man)", "man-teen_adult-%s-hair-default-beard"),
              ("beard (person)", "person-teen_adult-%s-hair-default-beard"),
              ("headscarf", "woman-teen_adult-%s-hair-default-headscarf")]
    badge_cols = [(b, [(b, "badge-%s" % b)]) for b in man["badges"]]
    sheet("fluent_extras",
          [(h, [(sk, p % sk) for sk in skins]) for h, p in extras]
          + badge_cols)


def main():
    os.makedirs(OUT_DIR, exist_ok=True)
    if not os.path.exists(os.path.join(ROOT, "web", "static", "avatars",
                                       "manifest.json")):
        print("SKIP: web/static/avatars/manifest.json missing - run "
              "python3 tools/fetch_fluent_avatars.py first")
        return 1
    fluent_sheets()
    return 0


if __name__ == "__main__":
    sys.exit(main())
