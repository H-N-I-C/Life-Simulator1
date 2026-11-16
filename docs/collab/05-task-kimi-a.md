# Task — Kimi A: appearance model, generation, game integration

Read first: docs/collab/03-plan.md, 04-appearance-contract.md (frozen contract), CODEX-HANDOFF.md.
Your files: `src/appearance/` (model, catalog, generate, aging — NOT render.py), `src/people/classes/*`, `src/lifesim_lib/safe_save.py`, save-load code, `tests/` (except render/web tests). Do NOT edit `web/` or `src/appearance/render.py` (Kimi B owns those).

## Order
1. FIRST (Kimi B is waiting on these): `catalog.py` + `model.py` + 12+ diverse fixtures in `tests/fixtures/appearances/`. Then send a one-line note to Kimi B (`hc send t12 "..."`) saying they exist.
2. `generate.py` (own RNG; age-appropriate accessories; biological inheritance with bounded variation; adopted/unknown parents → independent) and `aging.py`.
3. Integrate: every Person subclass gets `.appearance`, finalized once parents are known (Child parents can be set after construction). Adoption path → independent. Role changes copy appearance, never regenerate.
4. Successor bug in `Player.convert_child_to_player`: the chosen child must not appear as their own sibling; reconstructed relatives keep real names and appearances; the successor keeps their appearance.
5. Saves: register new class(es) in the restricted unpickler allowlist (import the module before loading), migrate old saves once with cycle-safe traversal, unknown IDs → defaults.
6. Tests (pytest or unittest, runnable with `python3 -m pytest` or `python3 -m unittest`): determinism, gameplay RNG unaffected (global `random` state identical with/without appearance generation), inheritance vs adoption, all Person subclasses, old-save migration, successor fix, unknown-ID fallback.

## Rules
- Don't commit. Don't change gameplay balance. Keep diffs focused.
- When done, write `docs/collab/kimi-a-report.md`: what you did, files changed, how to test, test output, anything unfinished. Then stop and wait for review.
