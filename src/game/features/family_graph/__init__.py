"""Family graph — relationships, unions, parentage, households (M2.5, A).

Implements the frozen contracts in docs/collab/M2.5/contracts/graph-contract.md
and fixtures in graph-examples.json. Plain-dict edges inside the save
envelope field env["graph"]; this module is the ONLY writer and the
authoritative reader. person objects are real src Person instances.
"""

from .graph import (
    GRAPH_SCHEMA_VERSION,
    GraphError,
    new_graph,
    validate_graph,
    normalize_envelope,
    # guard
    assert_adult_romantic,
    is_adult,
    # relationship edges
    start_relationship,
    update_relationship,
    end_relationship,
    # union edges
    form_union,
    end_union,
    # parentage / household
    add_parentage,
    household_move,
    set_guardian,
    add_support,
    # offers
    record_offer,
    resolve_offer,
    # queries
    graph_person,
    graph_relatedness,
    graph_edges,
    graph_relationship,
    graph_union_memberships,
    graph_household,
    graph_tree,
    # child health
    sample_conception,
    # migration
    migrate_v2,
    # lifecycle
    apply_death,
    dependent_children,
    # editor support
    supersede_offers,
)

__all__ = [name for name in dir() if not name.startswith("_")]
