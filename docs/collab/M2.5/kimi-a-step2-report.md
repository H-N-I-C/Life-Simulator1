# M2.5 A — Step 2 report: family_graph core (Kimi A, 2026-11-17)

Scope: `src/game/features/family_graph/**` (new, A-owned namespace), minimal
envelope integration in `src/game/service.py` (A-owned), new
`tests/test_family_graph_core.py` (18 tests). Implements the frozen
contracts (graph-contract.md + graph-examples.json).

## What landed

- **`graph.py`** — the only writer/authoritative reader of `env["graph"]`:
  - `new_graph()` (frozen shape), `validate_graph`, `normalize_envelope`
    (backfills `graph` + `editor_audit` onto any save; `_read_save` persists
    once — old saves verified loading unchanged).
  - Fixed guard `assert_adult_romantic` (naturally 18+, re-run at offer
    creation AND resolution; not overridable), typed `GraphError`
    (not_adult 409 / graph_error 400/409 / not_found 404 / invalid_input).
  - Pairwise relationship + union edges with plural memberships, frozen
    recognition sub-shape (forbidden unions still form), prenup (cash),
    `policy_revision` stamped at union creation (additive key, promised to
    B — flagged below), selected-edge `end_union` (children/other unions
    untouched).
  - Parentage edges with true DAG cycle detection (child-is-ancestor-of-
    parent walk; multi-path redundancy stays legal) and duplicate edges.
  - Offer envelope (7 romantic kinds + `parenting_request` nonromantic,
    romantic flag governs the guard), expiry, attempt preservation,
    `supersede_offers` (B's `on_policy_revision_bump` semantics).
  - Queries: `graph_person`, `graph_relatedness` (biological-only, frozen
    bands, explicit `lineage:"unknown"` marker → unknown — see flag),
    `graph_edges`, `graph_relationship`, `graph_union_memberships`,
    `graph_household`, `graph_tree` (bounded, cycle-safe; matches the
    frozen fixture exactly: ancestors+descendants within N generations,
    other-parents-of-included-children closure, union partners, secret
    edges only when viewer-known, ex as node flag).
  - `sample_conception` (bounded fictional bands, seeded rng, rolled once
    per conception, persisted by caller; no draw for adoption).
  - `migrate_v2`: exactly one union from the legacy single partner,
    biological parentage from RECORDED child mother/father only, archive
    ex-partners as ended edges, `primary_relationship_id` projection;
    any odd legacy data (e.g. the fragile `marital_status` property)
    degrades to a neutral empty graph — loading never breaks.
- **service.py**: `new_game` writes `graph` + `editor_audit`;
  `_read_save` normalizes old saves (verified: stripped env reloads and
  backfills).
- **graph-examples.json**: one clarification — morgan's record gains
  `lineage: "unknown"` (the trigger for the frozen 'unknown' relatedness
  row; category enum and all other rows unchanged). graph-contract.md §5
  documents the trigger.

## Tests — tests/test_family_graph_core.py (18, all OK under 3 hash seeds)

Real Player/Parent/Partner objects; the blended family fixture built
edge-by-edge through the real transition functions. Guard (creation +
resolve-time, scores don't override), self-edge/duplicate/active-pair
conflicts, parentage DAG + duplicates, forbidden union forms, selected
divorce, policy_revision stamping, all six frozen relatedness rows,
social-parentage zero risk, frozen tree projection incl. secret-edge
hide/show, offer envelope + supersession + expiry + nonromantic skip,
conception persistence, envelope normalize, v2 migration (happy +
odd-data).

## Verification

test_family_graph_core 18 OK × PYTHONHASHSEED 0/42/12345;
test_game_service + test_game_crash_safety 53 OK; old-save backfill
script verified. Adjacent suites ran earlier this session after the
latest service.py changes (ask_for_money fix): 78 OK.

## Flags for Claude

1. **Additive shape change**: unions now carry `policy_revision` (int) —
   promised to B in the README cross-shapes ("you stamp it on offers and
   unions") but absent from the frozen union fixture rows. Additive key;
   approve or strike.
2. `lineage: "unknown"` fixture clarification (above) — approve.
3. Yearly-phase money postings (coordinator path) were NOT given
   per-command keys — they stay once-per-turn as frozen. Only
   command-driven postings (_cmd_rule_action/_resolve_rule_pending) use
   request_id-suffixed occurrences (the ask_for_money fix).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real objects throughout (Player/Parent/Partner/GameService); no
      mocks — RNG is a real seeded `random.Random`.
- [x] Rare paths forced: cycle/duplicate/forbidden/supersede/expiry all
      direct calls, no random rolls.
- [x] Inputs validated: kinds, statuses, bounds, ids → typed errors.
- [x] Identity: person_id only; graph edge ids minted here only.
- [x] Save/load: new fields backfilled; old saves load (script-verified).
- [x] Adult gate centralized, non-overridable, checked at creation AND
      resolution; editor/content cannot lower it (no editor surface yet).
Hygiene
- [x] Only A-owned files (new namespace + service.py envelope lines +
      my test file + my own contract files).
- [x] No probability/balance changes to existing gameplay.
- [x] Focused tests green under 3 PYTHONHASHSEED values.
- [x] n/a UI this step (tree UI is step 3).
