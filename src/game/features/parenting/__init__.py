"""Parenting feature (M2.5, Kimi A): styles, personalities, milestones,
requests, teen troubles, holidays. Children are non-romantic dependents;
all par_* v3 content declares interaction.romantic false."""

from .core import (
    STYLES, DEFAULT_STYLE, new_parenting_state, normalize_parenting,
    ensure_child_record, backfill_all, set_style, style_of,
    record_milestone, milestones_of, has_milestone,
    mark_seen, is_seen, child_style_modifier, person_children,
)

__all__ = [name for name in dir() if not name.startswith("_")]
