"""Knowledge / drama / reputation feature (M2.5, Kimi A implements C's
frozen reputation-knowledge.md). Facts vs observer knowledge, scoped
reputation, affairs (expectation violations only), discovery,
disclosure, forgiveness, and the server-side player_known filter."""

from .core import (
    KNOWLEDGE_SCHEMA_VERSION, FACT_KINDS, KNOWLEDGE_SOURCES,
    PLAYER_OBSERVER,
    new_knowledge_state, normalize_knowledge,
    mint_fact, get_fact, learn, knows, known_facts,
    reputation_get, reputation_delta, reputation_as_known,
    journal_entry_visible,
)
from .drama import (
    TRUST_REPAIR_ON_FORGIVENESS,
    detect_affair, reveal_fact, disclose_to, forgive, end_fact,
    filter_reasons,
)

__all__ = [name for name in dir() if not name.startswith("_")]
