"""Relationship policy feature (Kimi B, M2.5).

Fictional, save-local, versioned policy catalogs (faiths/tenets/cultures/
jurisdictions), personal attraction/preference profiles, the bounded
acceptance evaluator, and (later steps) the character-editor domain
commands. Frozen contract shapes live in
docs/collab/M2.5/contracts/; this package is their implementation.

Fixed rule: both participants in any romantic interaction are naturally
18+ (protected chronology, A's centralized graph guard). Nothing here can
lower that — `not_adult` is a hard validity failure and no score, tenet,
law or edit overrides it.
"""

from . import catalog, profiles, evaluator, editor  # noqa: F401
