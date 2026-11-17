# M2.5 A — Step 3 report: family-tree backing + feature UI (Kimi A, 2026-11-17)

## What landed

- **Service**: `GameService.get_family_tree(game_id, focus_id=None,
  max_generations=3) -> {tree, appearances}` — the frozen §6 projection
  (secrets hidden until C's knowledge records) plus appearances for the
  M0b portrait renderer. Unknown focus → typed 404.
- **Legacy parentage backfill** (`_read_save`, once): graph parentage is
  seeded from legacy `mother`/`father` attrs AND `Parent` relations, so
  current-format games render a real tree immediately (verified: player +
  two parents, 2 biological edges, appearances for every node).
- **UI module** `web/static/features/family_graph/family_tree.js`
  (A-owned namespace): generational layout (BFS depths over the frozen
  edges), portraits via the shared `renderAvatar`, edge legend with frozen
  styles — biological/adoptive/step/guardian/social parentage labels,
  double line for unions, dashed + amber for secrets, grayscale+italic
  for deceased/ex, badges for flags. Frozen §6.1 label_keys ship with en
  defaults in `LABELS`. No numeric scores. Node syntax-checked.

## Hook request to B (exact)

1. Route: `GET /api/games/<id>/family-tree[?focus=<person_id>]` →
   `server.service.get_family_tree(game_id, focus)`; JSON passthrough.
2. Shell: a "Family tree" button in the General card lazy-importing
   `../../static/features/family_graph/family_tree.js` and calling
   `mountFamilyTree(container, gameId)` into a card/section, same pattern
   as the editor button.

## Tests

tests/test_family_graph_service.py +12 (tree endpoint incl. appearances
+ unknown-focus 404, plus the earlier adapter suites). Family-graph +
service suites 83 tests OK under PYTHONHASHSEED 0/42/12345; web M1/M2 60
OK.

## Self-check delta

- [x] Real objects end-to-end (GameService → envelope → projection).
- [x] Rare paths forced (empty tree, unknown focus, no parentage).
- [x] No shared-file edits outside A ownership (game.html/game.js and
      web/server.py untouched — B owns the shell + routes).
- [x] UI: reuses the shared palette classes, flex-wrap (phone-safe), no
      fixed widths; every node has a portrait via the M0b renderer.
- [ ] Full browser walk-through pending B's route/shell wiring (D's
      browser checks cover it after integration).
