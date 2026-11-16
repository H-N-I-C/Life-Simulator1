"""Avatar rendering v2 — Fluent 3D emoji asset resolution (Kimi B).

The SVG art renderer is retired with the M0b style switch: avatars are
resolved to downloaded Fluent 3D emoji PNG assets via
web/static/avatars/manifest.json (fetched by D's tools/fetch_fluent_avatars.py).

`avatar_image_key(appearance, age, context=None)` returns the resolution:

    {asset, background_hex, badges, found}

- asset: manifest asset key whose PNG exists (never None when the manifest
  is complete — chains end in a guaranteed fallback)
- background_hex: chip background colour for appearance.background
- badges: badge asset keys for appearance.badges (babies/children: [])

Stage mapping (aging.py): baby 0-2, child 3-12, teen_adult 13-59,
greying 60-69 (hair derives white unless bald), elder 70+.
"""

import os
import threading

from src.appearance import catalog
from src.appearance.aging import aged_view as _aged_view

ROOT = os.path.dirname(os.path.dirname(os.path.dirname(
    os.path.abspath(__file__))))
AVATAR_DIR = os.path.join(ROOT, "web", "static", "avatars")
MANIFEST_PATH = os.path.join(AVATAR_DIR, "manifest.json")

_manifest = None
_manifest_lock = threading.Lock()


def _load_manifest():
    """Atomic, thread-safe, single-assignment manifest load."""
    global _manifest
    if _manifest is not None:
        return _manifest
    with _manifest_lock:
        if _manifest is None:
            with open(MANIFEST_PATH, encoding="utf-8") as f:
                data = json_load(f)
            data["_assets"] = data.get("assets", {})
            _manifest = data
    return _manifest


_resolution_cache = {}
_resolution_cache_lock = threading.Lock()
_RESOLUTION_CACHE_MAX = 1024


def json_load(f):
    import json
    return json.load(f)


def _background_hex(background_id):
    try:
        entries = catalog.CATEGORIES.get("background", [])
        for e in entries:
            if e.get("id") == background_id and e.get("hex"):
                return e["hex"]
    except Exception:
        pass
    m = _load_manifest()
    idx = 0
    try:
        idx = int(str(background_id).rsplit("_", 1)[1]) - 1
    except (ValueError, IndexError):
        idx = 0
    backs = m.get("backgrounds") or ["#eeeae4"]
    return backs[max(0, min(idx, len(backs) - 1))]


def avatar_image_key(appearance, age, context=None):
    """Resolve an appearance+age to a manifest asset key + chip styling.

    appearance: v2 dict (or v1 — model.from_dict migrates). context may
    carry job/wedding hints; look_override on the appearance wins.

    Key space (D's manifest): <base>-<stage>-<skin>[-hair-<hair>][-beard]
    [-headscarf][-look-<look>], with dash-separated badge-<id> entries.
    """
    from src.appearance.model import Appearance
    app_obj = appearance if isinstance(appearance, Appearance) \
        else Appearance.from_dict(appearance or {})
    import json as _json
    canonical = _json.dumps(app_obj.to_dict(), sort_keys=True)
    cache_key = (canonical, int(age))
    with _resolution_cache_lock:
        hit = _resolution_cache.get(cache_key)
    if hit is None:
        hit = _resolve(app_obj, int(age))
        with _resolution_cache_lock:
            if len(_resolution_cache) >= _RESOLUTION_CACHE_MAX:
                _resolution_cache.clear()
            _resolution_cache[cache_key] = hit
    return hit


def _resolve(app_obj, age):
    view = _aged_view(app_obj, int(age))
    m = _load_manifest()
    combos = m.get("combos", {})
    assets = m.get("_assets", {})

    stage = view["stage"]          # baby|child|teen_adult|greying|elder
    skin = view["skin_tone"]
    base = view["base"]
    young = stage in ("baby", "child")

    def build_key(with_hair=True, beard=False, headscarf=False):
        key = "%s-%s-%s" % (base, stage, skin)
        if with_hair and not young:
            key += "-hair-%s" % view["hair"]
        if beard:
            key += "-beard"
        if headscarf:
            key += "-headscarf"
        return key

    beard = (not young) and view.get("facial_hair") == "beard"
    scarf = (not young) and view.get("headwear") == "headscarf"

    chain = None
    look = getattr(app_obj, "look_override", "none")
    if look and look != "none":
        for candidate in ("%s-%s-%s-look-%s" % (base, stage, skin, look),
                          "%s-teen_adult-%s-look-%s" % (base, skin, look)):
            if candidate in combos:
                chain = combos[candidate]
                break
    if not chain:
        chain = combos.get(build_key(beard=beard, headscarf=scarf))
    if not chain and (beard or scarf):
        # manifest gaps for combined states: degrade gracefully
        chain = combos.get(build_key(beard=beard, headscarf=False)) or \
            combos.get(build_key()) or \
            combos.get("%s-%s-%s" % (base, stage, skin))
    if not chain:
        chain = combos.get("%s-%s-%s" % (base, stage, skin))
    if not chain and asset_stage in ("baby", "child") and base == "person":
        # the Fluent set has no gender-neutral baby/child asset: fall back
        # to the boy/girl assets deterministically by seed
        alt = "boy" if (getattr(app_obj, "seed", 0) or 0) % 2 else "girl"
        chain = combos.get("%s-%s-%s" % (alt, asset_stage, skin)) or \
            combos.get("man-%s-%s" % (asset_stage, skin))
    asset = None
    for candidate in (chain or []):
        if candidate in assets and os.path.isfile(os.path.join(
                AVATAR_DIR, assets[candidate])):
            asset = candidate
            break
    if asset is None and chain:
        asset = chain[0]  # manifest guarantees these exist
    badges = []
    if not young:
        for b in (view.get("badges") or []):
            for candidate in combos.get("badge-%s" % b, []):
                if candidate in assets:
                    badges.append(candidate)
                    break
    return {
        "asset": asset,
        "file": assets.get(asset),
        "background_hex": _background_hex(view.get("background")),
        "badges": badges,
        "stage": stage,
        "found": asset is not None and os.path.isfile(os.path.join(
            AVATAR_DIR, assets.get(asset) or "")),
    }


# ---------------------------------------------------------------------------
# Legacy compatibility during the transition: the web layer used to call
# render_avatar_svg. The M0b contract deletes it; this shim raises with a
# clear message so stale callers fail loudly instead of silently.
# ---------------------------------------------------------------------------

def render_avatar_svg(*args, **kwargs):
    raise NotImplementedError(
        "render_avatar_svg was retired by the M0b avatar switch — use "
        "avatar_image_key(appearance, age, context) + the PNG assets in "
        "web/static/avatars/")


def get_catalog():
    """Pass-through to the v2 catalog (kept for the web adapter)."""
    return catalog.CATEGORIES
