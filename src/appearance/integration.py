"""Integration of appearances with game Person objects.

Uses a dedicated random.Random instance so cosmetic generation never
consumes from (or perturbs) the global gameplay RNG.
"""

import logging
import random
import uuid

from src.appearance.model import Appearance, SCHEMA_VERSION
from src.appearance.generate import random_appearance, inherit_appearance

_APP_RNG = random.Random()
_log = logging.getLogger(__name__)


def ensure_person_id(person):
    """Immutable persisted PERSON identity; legacy backfill. Distinct from
    Player.ID (legacy save-slot identifier)."""
    pid = getattr(person, "person_id", None)
    if not pid:
        pid = str(uuid.uuid4())
        person.person_id = pid
    return pid


def _is_v1_data(value):
    """True for raw v1 appearance dicts (need base refinement from gender)."""
    return isinstance(value, dict) and \
        value.get("schema_version", SCHEMA_VERSION) < SCHEMA_VERSION


def _as_appearance(value):
    """Tolerant coercion: Appearance, dict, or None.

    Existing instances are normalized through the validated model path
    (unknown IDs fall back to defaults; valid fields are preserved).
    Unsupported schema versions or unparseable data are treated as missing
    so the caller can generate a fresh appearance.
    """
    if isinstance(value, Appearance):
        return _normalize(value)
    if isinstance(value, dict):
        try:
            return Appearance.from_dict(value)
        except ValueError as e:
            _log.warning("Discarding unsupported appearance data: %s", e)
            return None
    return None


def _normalize(appearance):
    """Re-validate a loaded instance without rerolling valid fields."""
    version = appearance.schema_version
    if (not isinstance(version, int) or isinstance(version, bool)
            or version < 1 or version > SCHEMA_VERSION):
        _log.warning(
            "Discarding appearance with unsupported schema version %r",
            version)
        return None
    return Appearance.from_dict(appearance.to_dict())


def ensure_appearance(person, mother=None, father=None):
    """Make sure a person has an Appearance, assigning one if missing.

    If biological parents with appearances are provided (and the person is
    not flagged adopted), the appearance is inherited; otherwise it is
    generated independently and made age-appropriate.
    """
    raw = getattr(person, "appearance", None)
    existing = _as_appearance(raw)
    if existing is not None and _is_v1_data(raw):
        # base refinement applies ONLY to freshly migrated v1 data; an
        # established v2 'person' base (e.g. mixed-parent child) is data
        # and must survive load unchanged.
        existing.refine_base_from_gender(getattr(person, "gender", None))
    adopted = getattr(person, "adopted", False)
    if not adopted and (mother is not None or father is not None):
        mapp = _as_appearance(getattr(mother, "appearance", None)) if mother else None
        fapp = _as_appearance(getattr(father, "appearance", None)) if father else None
        if mapp is not None or fapp is not None:
            person.appearance = inherit_appearance(
                _APP_RNG, mapp, fapp, age=getattr(person, "age", 0))
            return person.appearance
    if existing is None:
        existing = random_appearance(
            _APP_RNG, getattr(person, "age", 18), getattr(person, "gender", None)
        )
    person.appearance = existing
    return existing


def copy_appearance(appearance):
    """Deep-copy an appearance (role changes copy, never regenerate)."""
    app = _as_appearance(appearance)
    if app is None:
        return random_appearance(_APP_RNG, 18)
    return Appearance.from_dict(app.to_dict())


def migrate_appearances(player):
    """Assign appearances once to every Person in a loaded save.

    Cycle-safe graph traversal. Persons missing an appearance get one:
    biological children inherit from parents (when known), everyone else
    gets an independent random appearance. Existing appearances (including
    dict forms) are kept and normalized.
    """
    from src.people.classes.person import Person
    from src.people.classes.child import Child

    seen = set()
    persons = []

    def walk(obj):
        if id(obj) in seen:
            return
        seen.add(id(obj))
        if isinstance(obj, Person):
            persons.append(obj)
            for value in vars(obj).values():
                walk(value)
        elif isinstance(obj, dict):
            for value in obj.values():
                walk(value)
        elif isinstance(obj, (list, tuple, set, frozenset)):
            for value in obj:
                walk(value)

    walk(player)

    # Validate/normalize what each person already has (unknown IDs fall back
    # to defaults, valid fields preserved). Only people whose appearance is
    # missing or unusable get a new one; running migration twice is identical.
    missing = []
    for person in persons:
        ensure_person_id(person)  # immutable identity backfill
        app = _as_appearance(getattr(person, "appearance", None))
        if app is None:
            missing.append(person)
        else:
            raw = getattr(person, "appearance", None)
            if _is_v1_data(raw):
                app.refine_base_from_gender(getattr(person, "gender", None))
            person.appearance = app

    deferred_children = []
    for person in missing:
        mother = getattr(person, "mother", None)
        father = getattr(person, "father", None)
        if (isinstance(person, Child) and not getattr(person, "adopted", False)
                and (mother is not None or father is not None)):
            deferred_children.append(person)  # inherit once parents are ensured
        else:
            ensure_appearance(person)

    # Biological children of old saves inherit physical features from their
    # (now ensured) parents, with styling appropriate for their real age.
    for child in deferred_children:
        ensure_appearance(child, child.mother, child.father)
        child._appearance_finalized = True
        child._finalized_parent_ids = [
            ensure_person_id(p)
            for p in (child.mother, child.father) if p is not None]

    # Normalize finalization metadata for EVERY established child: earlier
    # formats stored process-local id() integers or (name, seed) tuples in
    # the same field. Rebuild from canonical current parent IDs without
    # re-inheriting; established appearances are never rerolled.
    for person in persons:
        if not isinstance(person, Child):
            continue
        current = sorted(
            ensure_person_id(p)
            for p in (getattr(person, "mother", None),
                      getattr(person, "father", None)) if p is not None)
        keys = getattr(person, "_finalized_parent_ids", None)
        legacy = keys is not None and any(
            not isinstance(k, str) for k in keys)
        if getattr(person, "_appearance_finalized", False):
            if keys is None or legacy or set(keys) != set(current):
                person._finalized_parent_ids = current
        elif legacy:
            del person._finalized_parent_ids

    return player
