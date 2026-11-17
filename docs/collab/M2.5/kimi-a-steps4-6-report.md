# M2.5 A — Steps 4–6 report (Kimi A, 2026-11-17)

## Step 4 — biological relatedness + child health (service surface)

- `GameService.get_relatedness(game_id, a_id, b_id)` → frozen
  `{category, biological_paths, coefficient_band}`; typed 404 on unknown
  ids. Biological ancestry only; name/religion/orientation never enter
  the coefficient.
- `GameService.record_conception(game_id, child_id, bio_mother_id,
  bio_father_id, request_id)` — rolls the bounded fictional adjustment
  ONCE per conception with the GAME rng (inside `_GameRNG`), persists it
  in `graph.conception_outcomes["<child>:<request>"]`, and is idempotent:
  a retry returns the stored outcome and does NOT re-consume the rng
  (test asserts rng_state equality across the retry). Both biological
  parents must be 18+ (fixed rule); adoption never routes here.
- Fictional risk bands calibrated in `sample_conception`
  (`0:0.02, <0.015:0.03, 0.015-0.0625:0.08, 0.125:0.15, 0.25:0.30,
  unknown:0.05`) — PLACEHOLDER calibration, frozen with Claude per the
  contract; no deterministic illness, no stigmatising text.

## Step 5 — M2 migration of real families (+ pending preservation)

- `_read_save` now migrates exactly once: legacy `player.partner` with an
  untouched graph → `migrate_v2(env)` (one union, recorded parentage
  only, archive ex-partners as ended edges, primary projection). Odd data
  degrades to a neutral graph; loading never breaks (incl. the fragile
  legacy `marital_status` property, which is no longer read).
- Cash, pending decisions and archives are untouched by migration —
  regression test drives a real GameService game, wipes the graph,
  reloads, and asserts the union renders, is idempotent, and that
  $4242 + a stored pending survive.

## Step 6 — typed graph transitions for B/C

- `src/game/features/family_graph/transitions.py` — frozen §7 effect
  targets: `start_relationship_op`, `update_relationship_op`,
  `form_union_op`, `end_union_op`, `add_parentage_op`, `record_offer_op`,
  `resolve_offer_op`. Each resolves people from the envelope, validates
  through `family_graph.graph`, mutates `env["graph"]`, and returns
  ACTUAL-delta results `{ok, ids, journal, ledger_requests}` (journal
  entries carry real names + person_ids; no direct money — costs stay in
  C's content via the ledger). `GraphError` re-exported for code mapping.
- `GameService.graph_command(game_id, expected_revision, request_id, op,
  args)` — replay-safe wrapper (byte-identical retries, stale_revision,
  conflict-on-payload-change) that journals, bumps revision, records
  replay and saves — for direct UI/feature use; B/C should call the OPS
  inside their own command context.

## Tests

tests/test_family_graph_service.py now 21 tests (+ relatedness rows,
conception once/idempotency/adult-gate, real-family migration +
idempotency, pending/cash preservation, graph_command replay/guard/unknown
op). Suites: family_graph + service + game_service + crash_safety 92
tests OK under PYTHONHASHSEED 0/42/12345; web M1/M2 60 OK.

## Self-check delta

- [x] Real objects (GameService/Player/Partner) end-to-end; seeded RNGs.
- [x] Rare paths forced (cycle, odd migration data, retry, expiry).
- [x] Money: graph transitions post NO direct money; ledger stays the
      single authority (empty ledger_requests by design).
- [x] Identity: person_id only; rename/identity untouched.
- [x] Save/load: new fields backfill; migration idempotent.
- [x] Adult gate enforced on conception parents and every romantic op.
- [x] Focused suites under 3 hash seeds; no files outside A ownership.

---

# Step 7 addendum (Kimi A, 2026-11-17)

Risk bands frozen as proposed — recorded in contracts/README "Addenda
frozen during Phase 2"; UI never presents them as real-world odds.

1. **Rename/same-name handling** — regression test only (graph is
   person_id-based by design): father renamed through the real
   editor_apply path to the player's EXACT name; kinship, tree nodes,
   parentage edges all stay correct; a second rename touches only the
   father record.
2. **Death/succession graph ops** — new `family_graph.apply_death`:
   active relationships end, active unions become `widowed`, the
   deceased's pending offers are cancelled (attempts preserved);
   parentage/history edges stay so the tree keeps rendering the deceased
   (flagged, grayscale in the UI). Wired into `_apply_event_death`
   (idempotent); succession now re-points `primary_relationship_id` at
   the successor's active edge. Fixed a real gap found by the test:
   archived (dead/ex) people were missing from the tree/relatedness
   person maps — `_all_people(env)` now merges the archive and marks
   `dead_ids` not-alive for projections (also applied to
   ServiceGraphView).
3. **Child-cost dedupe** — new `family_graph.dependent_children(graph,
   person_id)`: unique child ids across ALL parentage roles and
   households; test proves two households sharing child-2 yield
   ["child-1","child-2","child-3"] — once per child, for C's yearly
   phases to consume.

Tests: +3 (rename/same-name, death lifecycle incl. idempotency + tree
rendering of the deceased, dedupe). Domain suites 96 tests OK under
PYTHONHASHSEED 0/42/12345; web M1/M2 60 OK.
