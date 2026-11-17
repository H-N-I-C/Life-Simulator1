"""Policy catalog access: presets, save-local overrides, neutral defaults.

Resolution order (frozen policy-catalogs.json): save-local override (if
present) -> catalog preset (by id+version) -> built-in neutral default.
A content/catalog update must never silently erase a save-local edit.
"""

import json
import os

DATA_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)),
                         "data", "policy_catalogs.json")

STANCE_VALUES = ("favoured", "neutral", "disfavoured")
RECOGNITION_VALUES = ("recognized", "civil_only", "unrecognized",
                      "forbidden")


class PolicyError(Exception):
    """Typed domain error for the policy feature.

    `code` is stable and machine-checkable (mirrors the frozen
    hard-validity/reason vocabulary where applicable).
    """

    def __init__(self, code, message, status=400, field=None,
                 allowed=None):
        super().__init__(message)
        self.code = code
        self.message = message
        self.status = status
        self.field = field
        self.allowed = allowed

    def __repr__(self):
        return "PolicyError(%s, %s)" % (self.code, self.message)


def load_catalog():
    with open(DATA_PATH, encoding="utf-8") as f:
        return json.load(f)


def default_profile(catalog):
    return dict(catalog["defaults"]["neutral_profile"])


def resolve_faith(catalog, overrides, faith_id):
    """Public tenet stances for a faith: catalog preset merged with the
    save-local override (override tenets win per-tenet), else the catalog
    preset, else the neutral (unaffiliated) profile. Overrides are
    per-tenet patches — editing one tenet never erases the rest."""
    faith_id = faith_id or "faith_unaffiliated"
    base = None
    for faith in catalog.get("faiths", []):
        if faith["id"] == faith_id:
            base = faith
            break
    if base is None:
        base = _neutral_faith(catalog)
    ov = (overrides or {}).get("faiths", {}).get(faith_id)
    if not ov:
        return base
    merged = {"id": base["id"], "label": ov.get("label", base.get("label")),
              "version": ov.get("version", base.get("version")),
              "tenets": [dict(t) for t in base.get("tenets", [])]}
    by_id = {t["id"]: t for t in merged["tenets"]}
    for tenet in ov.get("tenets", []):
        if tenet["id"] in by_id:
            by_id[tenet["id"]].update(tenet)
        else:
            merged["tenets"].append(dict(tenet))
    return merged


def _neutral_faith(catalog):
    for faith in catalog.get("faiths", []):
        if faith["id"] == "faith_unaffiliated":
            return faith
    return {"id": "faith_unaffiliated", "tenets": []}


def resolve_culture(catalog, overrides, culture_id):
    culture_id = culture_id or "culture_meridian"
    base = None
    for culture in catalog.get("cultures", []):
        if culture["id"] == culture_id:
            base = culture
            break
    if base is None:
        base = {"id": culture_id, "modifiers": {}}
    ov = (overrides or {}).get("cultures", {}).get(culture_id)
    if not ov:
        return base
    merged = {"id": base["id"], "label": ov.get("label",
                                                base.get("label")),
              "version": ov.get("version", base.get("version")),
              "modifiers": dict(base.get("modifiers", {}))}
    merged["modifiers"].update(ov.get("modifiers", {}))
    return merged


def resolve_jurisdiction(catalog, overrides, jurisdiction_id):
    jurisdiction_id = jurisdiction_id or "jur_metropolis_charter"
    base = None
    for jur in catalog.get("jurisdiction_presets", []):
        if jur["id"] == jurisdiction_id:
            base = jur
            break
    if base is None:
        base = _neutral_jurisdiction()
        base["id"] = jurisdiction_id
    ov = (overrides or {}).get("jurisdictions", {}).get(jurisdiction_id)
    if not ov:
        return base
    merged = {"id": base["id"], "label": ov.get("label", base.get("label")),
              "version": ov.get("version", base.get("version")),
              "recognition": dict(base.get("recognition", {})),
              "consequences": [dict(c) for c in
                               base.get("consequences", [])]}
    merged["recognition"].update(ov.get("recognition", {}))
    if ov.get("consequences") is not None:
        merged["consequences"] = [dict(c) for c in ov["consequences"]]
    merged["law_version"] = ov.get("law_version",
                                   base.get("law_version", 0))
    return merged


def _neutral_jurisdiction():
    return {"id": "jur_neutral", "label": "Neutral jurisdiction",
            "recognition": {k: "recognized" for k in (
                "monogamous_civil", "plural_civil", "religious_only",
                "customary", "private")},
            "consequences": []}


def tenet_stance(catalog, overrides, faith_id, tenet_id,
                 personal_overrides=None):
    """Effective stance of a faith's tenet: personal override (this person
    only) beats save-local override beats catalog preset. Returns
    (stance, source) where source is personal|override|catalog."""
    if personal_overrides and tenet_id in personal_overrides:
        return personal_overrides[tenet_id], "personal"
    faith = resolve_faith(catalog, overrides, faith_id)
    for tenet in faith.get("tenets", []):
        if tenet["id"] == tenet_id:
            return tenet["stance"], (
                "override" if (overrides or {}).get("faiths", {})
                .get(faith_id) else "catalog")
    return "neutral", "catalog"


def jurisdiction_of_save(save_policy_state):
    """The save's effective jurisdiction id (baseline; a country pick maps
    to a preset via the service adapter in a later step)."""
    return (save_policy_state or {}).get("jurisdiction_id",
                                         "jur_metropolis_charter")
