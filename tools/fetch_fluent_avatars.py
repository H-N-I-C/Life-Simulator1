"""Fetch the Fluent 3D avatar assets (asset pipeline for M0b).

Downloads ONLY the needed 3D PNGs from microsoft/fluentui-emoji at a
PINNED commit, resizes them to 256 px (alpha preserved), writes them to
web/static/avatars/<key>.png and builds manifest.json with the full
combo-resolution table (every logical combo maps to an ordered fallback
chain of existing asset keys). Also copies the MIT LICENSE.

Deterministic and idempotent: existing files with the right size are
skipped, so re-runs only fetch what is missing.

Run: python3 tools/fetch_fluent_avatars.py
"""

import io
import json
import os
import sys
import urllib.request

import PIL.Image

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
OUT_DIR = os.path.join(ROOT, "web", "static", "avatars")
MANIFEST_PATH = os.path.join(OUT_DIR, "manifest.json")
SIZE = 256

PINNED_COMMIT = "1ffb34c752ecf5d402f04cfb4b392c77f57c54bc"
RAW = ("https://raw.githubusercontent.com/microsoft/fluentui-emoji/"
       + PINNED_COMMIT + "/")

SKINS = ["default", "light", "medium-light", "medium", "medium-dark", "dark"]
SKIN_DIRS = {"default": "Default", "light": "Light",
             "medium-light": "Medium-Light", "medium": "Medium",
             "medium-dark": "Medium-Dark", "dark": "Dark"}

BASES = ["person", "woman", "man"]
STAGES = ["baby", "child", "adult", "greying", "elder"]
HAIRS = ["default", "curly", "red", "white", "bald"]
BEARDS = ["none", "beard"]
HEADSCARVES = ["none", "headscarf"]

# logical key -> (emoji asset dir, filename slug); {skin} is substituted.
# Only the assets the M0b contract can reference are listed.
EMOJI = {
    # stages
    "baby": ("Baby", "baby"),
    "child": ("Child", "child"),
    "girl": ("Girl", "girl"),
    "boy": ("Boy", "boy"),
    "adult_person": ("Person", "person"),
    "adult_woman": ("Woman", "woman"),
    "adult_man": ("Man", "man"),
    "elder_person": ("Older person", "older_person"),
    "elder_woman": ("Old woman", "old_woman"),
    "elder_man": ("Old man", "old_man"),
    # hair variants (adult bases only; elders/babies fall back)
    "adult_person_curly": ("Person curly hair", "person_curly_hair"),
    "adult_person_red": ("Person red hair", "person_red_hair"),
    "adult_person_white": ("Person white hair", "person_white_hair"),
    "adult_person_bald": ("Person bald", "person_bald"),
    "adult_woman_curly": ("Woman curly hair", "woman_curly_hair"),
    "adult_woman_red": ("Woman red hair", "woman_red_hair"),
    "adult_woman_white": ("Woman white hair", "woman_white_hair"),
    "adult_woman_bald": ("Woman bald", "woman_bald"),
    "adult_man_curly": ("Man curly hair", "man_curly_hair"),
    "adult_man_red": ("Man red hair", "man_red_hair"),
    "adult_man_white": ("Man white hair", "man_white_hair"),
    "adult_man_bald": ("Man bald", "man_bald"),
    # facial hair (adults only)
    "beard_man": ("Man beard", "man_beard"),
    "beard_person": ("Person beard", "person_beard"),
    # headscarf
    "headscarf": ("Woman with headscarf", "woman_with_headscarf"),
    # look overrides
    "look_student": ("Student", "student"),
    "look_health_worker": ("Health worker", "health_worker"),
    "look_police": ("Police officer", "police_officer"),
    "look_chef": ("Cook", "cook"),
    "look_farmer": ("Farmer", "farmer"),
    "look_mechanic": ("Mechanic", "mechanic"),
    "look_scientist": ("Scientist", "scientist"),
    "look_artist": ("Artist", "artist"),
    "look_judge": ("Judge", "judge"),
    "look_pilot": ("Pilot", "pilot"),
    "look_groom": ("Person in tuxedo", "person_in_tuxedo"),
    "look_bride": ("Woman with veil", "woman_with_veil"),
}

# badges are flat object emoji (no skin variants)
BADGES = {
    "glasses": ("Glasses", "glasses"),
    "earrings": ("Ring", "ring"),  # no earrings emoji; ring is the fallback
    "cap": ("Billed cap", "billed_cap"),
    "crown": ("Crown", "crown"),
    "flower": ("Rose", "rose"),
}

# 10 soft background colours (no assets needed; rendered by the UI)
BACKGROUNDS = ["#fde7e9", "#fdeccf", "#fff7cc", "#e6f4d7", "#d7f0e6",
               "#d9ecf7", "#e4dcf7", "#f7dcf0", "#eeeae4", "#e0e0e0"]

CHILD_MAP = {"person": "child", "woman": "girl", "man": "boy"}
ELDER_MAP = {"person": "elder_person", "woman": "elder_woman",
             "man": "elder_man"}
ADULT_MAP = {"person": "adult_person", "woman": "adult_woman",
             "man": "adult_man"}


def asset_keys():
    """Every (asset_key, repo relpath) to download."""
    out = []
    for key, (emoji_dir, slug) in EMOJI.items():
        for skin in SKINS:
            rel = ("assets/%s/%s/3D/%s_3d_%s.png"
                   % (emoji_dir, SKIN_DIRS[skin], slug, skin))
            out.append(("%s_%s" % (key, skin), rel))
    for key, (emoji_dir, slug) in BADGES.items():
        out.append(("badge_%s" % key,
                    "assets/%s/3D/%s_3d.png" % (emoji_dir, slug)))
    return out


def fetch(key, rel):
    """Download one asset, resize to SIZE px, save as <key>.png."""
    dest = os.path.join(OUT_DIR, key + ".png")
    if os.path.exists(dest) and os.path.getsize(dest) > 0:
        return os.path.getsize(dest), False
    with urllib.request.urlopen(RAW + urllib.parse.quote(rel),
                                timeout=60) as resp:
        raw = resp.read()
    img = PIL.Image.open(io.BytesIO(raw)).convert("RGBA")
    img = img.resize((SIZE, SIZE), PIL.Image.LANCZOS)
    img.save(dest, optimize=True)
    return os.path.getsize(dest), True


def skin_chain(make_key):
    """Fallback chain for a skin-keyed asset: requested skin -> default."""
    return lambda skin: [make_key(skin), make_key("default")]


def build_combos(assets):
    """Full logical combo -> ordered fallback chain of asset keys, in the
    PUBLISHED key shape (docs/collab/M0b-examples.json asset_pipeline):
        <base>-<stage>-<skin>[-hair-<hair>][-beard][-headscarf][-look-<look>]
    Stages: baby|child|teen_adult|greying|elder. Fallbacks per spec:
    missing hair variant -> stage base; curly+elder -> elder; missing skin
    -> default; missing look asset -> regular look; graduate has no asset
    and always falls back.
    """
    combos = {}

    def put(combo, chain):
        seen = set()
        chain = [k for k in chain if k in assets and not (k in seen or
                                                           seen.add(k))]
        if chain:
            combos[combo] = chain

    def skin_chain(make_key):
        return lambda skin: [make_key(skin), make_key("default")]

    ADULT = {"person": "adult_person", "woman": "adult_woman",
             "man": "adult_man"}
    ELDER = {"person": "elder_person", "woman": "elder_woman",
             "man": "elder_man"}
    CHILD = {"person": "child", "woman": "girl", "man": "boy"}
    looks = ["student", "health_worker", "police", "chef", "farmer",
             "mechanic", "scientist", "artist", "judge", "pilot",
             "graduate", "bride", "groom"]
    LOOKS_WITH_ASSETS = [l for l in looks
                         if "look_%s_default" % l in assets]

    for base in BASES:
        for skin in SKINS:
            # --- baby / child: hair/beard/headscarf fall back to the
            # plain stage asset (engine clears them anyway) ---
            for stage, make in (("baby", lambda s: "baby_%s" % s),
                                ("child", lambda s: "%s_%s"
                                 % (CHILD[base], s))):
                base_chain = skin_chain(make)(skin)
                put("%s-%s-%s" % (base, stage, skin), base_chain)
                for hair in HAIRS:
                    if hair == "default":
                        continue
                    put("%s-%s-%s-hair-%s" % (base, stage, skin, hair),
                        base_chain)
                put("%s-%s-%s-beard" % (base, stage, skin), base_chain)
                put("%s-%s-%s-headscarf" % (base, stage, skin), base_chain)
            # --- teen_adult / greying / elder ---
            for stage in ("teen_adult", "greying", "elder"):
                if stage == "elder":
                    # elders have no hair-variant assets: any hair falls
                    # back to the elder base (spec: curly+elder -> elder)
                    def elder_key(s, b=base):
                        return "%s_%s" % (ELDER[b], s)
                    stage_chain = skin_chain(elder_key)(skin)
                    hairs = HAIRS
                elif stage == "greying":
                    # greying renders with derived white hair (spec)
                    def grey_key(s, b=base):
                        return "%s_white_%s" % (ADULT[b], s)
                    def plain_key(s, b=base):
                        return "%s_%s" % (ADULT[b], s)
                    stage_chain = (skin_chain(grey_key)(skin)
                                   + skin_chain(plain_key)(skin))
                    hairs = HAIRS
                else:
                    def plain_key(s, b=base):
                        return "%s_%s" % (ADULT[b], s)
                    stage_chain = skin_chain(plain_key)(skin)
                    hairs = HAIRS
                put("%s-%s-%s" % (base, stage, skin), stage_chain)
                for hair in hairs:
                    if stage == "elder":
                        chain = stage_chain
                    elif hair == "default":
                        chain = stage_chain
                    else:
                        def hair_key(s, b=base, h=hair):
                            return "%s_%s_%s" % (ADULT[b], h, s)
                        chain = (skin_chain(hair_key)(skin)
                                 + stage_chain)
                    put("%s-%s-%s-hair-%s" % (base, stage, skin, hair),
                        chain)
                    for beard in ("", "-beard"):
                        for scarf in ("", "-headscarf"):
                            suffix = beard + scarf
                            bchain = chain
                            if beard:
                                bkey = ("beard_man" if base == "man"
                                        else "beard_person")
                                bchain = (skin_chain(
                                    lambda s, k=bkey: "%s_%s" % (k, s))(
                                    skin) + bchain)
                            if scarf:
                                bchain = (skin_chain(
                                    lambda s: "headscarf_%s" % s)(skin)
                                    + bchain)
                            put("%s-%s-%s-hair-%s%s"
                                % (base, stage, skin, hair, suffix),
                                bchain)
                # look overrides apply to adult-ish stages
                for look in looks:
                    lchain = stage_chain
                    if look in LOOKS_WITH_ASSETS:
                        lchain = (skin_chain(
                            lambda s, l=look: "look_%s_%s" % (l, s))(skin)
                            + lchain)
                    put("%s-%s-%s-look-%s" % (base, stage, skin, look),
                        lchain)
    for badge in BADGES:
        put("badge-%s" % badge, ["badge_%s" % badge])
    return combos


def main():
    os.makedirs(OUT_DIR, exist_ok=True)
    total = 0
    fetched = 0
    assets = {}
    for key, rel in asset_keys():
        try:
            size, is_new = fetch(key, rel)
        except Exception as exc:
            print("FAIL %s (%s): %s" % (key, rel, exc))
            return 1
        total += size
        fetched += 1 if is_new else 0
        assets[key] = key + ".png"
    # license
    lic_dest = os.path.join(OUT_DIR, "LICENSE-fluentui-emoji.txt")
    if not os.path.exists(lic_dest):
        with urllib.request.urlopen(RAW + "LICENSE", timeout=60) as resp:
            with open(lic_dest, "wb") as f:
                f.write(resp.read())
    combos = build_combos(assets)
    manifest = {
        "source": "microsoft/fluentui-emoji",
        "license": "MIT (see LICENSE-fluentui-emoji.txt)",
        "commit": PINNED_COMMIT,
        "size_px": SIZE,
        "skins": SKINS, "bases": BASES, "stages": STAGES,
        "hairs": HAIRS, "badges": sorted(BADGES),
        "looks": ["student", "health_worker", "police", "chef", "farmer",
                  "mechanic", "scientist", "artist", "judge", "pilot",
                  "graduate", "bride", "groom"],
        "backgrounds": BACKGROUNDS,
        "assets": assets,
        "combos": combos,
    }
    with open(MANIFEST_PATH, "w") as f:
        json.dump(manifest, f, indent=1, sort_keys=True)
    n_png = len(assets)
    print("%d assets (%d fetched now), %.2f MB total, %d combos"
          % (n_png, fetched, total / 1e6, len(combos)))
    print("wrote %s" % OUT_DIR)
    print("manifest: %s (%.1f KB)" % (MANIFEST_PATH,
                                      os.path.getsize(MANIFEST_PATH) / 1e3))
    return 0


if __name__ == "__main__":
    import urllib.parse  # noqa: E402
    sys.exit(main())
