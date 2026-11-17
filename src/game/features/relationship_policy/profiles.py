"""Personal attraction/preference profiles (frozen attraction_profile).

Soft modifiers only: they shift acceptance scores and NPC choice, they
never create eligibility or consent. Orientation is drawn at person
creation from the passed-in seeded RNG — never inferred from name,
gender or appearance.
"""

from .catalog import PolicyError, default_profile

ORIENTATIONS = ("heterosexual", "homosexual", "bisexual", "asexual")

PROFILE_FIELDS = {
    "orientation": {"type": "enum", "values": ORIENTATIONS},
    "openness": {"type": "float", "min": 0.0, "max": 1.0},
    "jealousy": {"type": "float", "min": 0.0, "max": 1.0},
    "monogamy_preference": {"type": "float", "min": 0.0, "max": 1.0},
    "attraction_to_initiator": {"type": "float", "min": 0.0, "max": 1.0},
}

STANCE_VALUES = ("favoured", "neutral", "disfavoured")


def new_profile(catalog, rng, culture_id=None):
    """Create a fresh profile for a generated person. `rng` is the
    caller's seeded generator (never the global random module)."""
    base = default_profile(catalog)
    culture = None
    if culture_id:
        from .catalog import resolve_culture
        culture = resolve_culture(catalog, None, culture_id)
    mods = (culture or {}).get("modifiers", {})

    def draw(default, key):
        return float(mods.get(key, default))

    orientation = base["orientation"]
    if orientation is None:
        orientation_draw = rng.random()
        orientation = ORIENTATIONS[min(len(ORIENTATIONS) - 1,
                                       int(orientation_draw *
                                           len(ORIENTATIONS)))]
    return {
        "faith_id": base["faith_id"],
        "culture_id": culture_id or base["culture_id"],
        "orientation": orientation,
        "openness": draw(base["openness"], "openness_default"),
        "jealousy": draw(base["jealousy"], "jealousy_default"),
        "monogamy_preference": draw(base["monogamy_preference"],
                                    "monogamy_preference_default"),
        # develops through interaction; neutral at creation
        "attraction_to_initiator": 0.5,
        "personal_tenet_overrides": {},
    }


def validate_profile_patch(patch):
    """Typed validation for editor edits. Returns the cleaned patch or
    raises PolicyError with a stable code (frozen editor-protocol)."""
    cleaned = {}
    for field, value in patch.items():
        spec = PROFILE_FIELDS.get(field)
        if spec is None:
            raise PolicyError("invalid_value",
                              "field %r is not editable" % field)
        if spec["type"] == "enum":
            if value not in spec["values"]:
                raise PolicyError("invalid_value",
                                  "%s must be one of %s"
                                  % (field, list(spec["values"])),
                                  field=field, allowed=list(spec["values"]))
        else:
            if not isinstance(value, (int, float)) \
                    or isinstance(value, bool):
                raise PolicyError("type_error",
                                  "%s must be a number" % field,
                                  field=field)
            if not (spec["min"] <= value <= spec["max"]):
                raise PolicyError(
                    "invalid_value",
                    "%s must be between %s and %s" % (field, spec["min"],
                                                      spec["max"]),
                    field=field,
                    allowed=[spec["min"], spec["max"]])
        cleaned[field] = value
    return cleaned


def validate_tenet_overrides(patch):
    cleaned = {}
    for tenet_id, stance in patch.items():
        if not isinstance(tenet_id, str) or not tenet_id:
            raise PolicyError("invalid_value", "tenet_id must be a string")
        if stance not in STANCE_VALUES:
            raise PolicyError("invalid_value",
                              "stance must be one of %s" % (STANCE_VALUES,),
                              field=tenet_id, allowed=list(STANCE_VALUES))
        cleaned[tenet_id] = stance
    return cleaned
