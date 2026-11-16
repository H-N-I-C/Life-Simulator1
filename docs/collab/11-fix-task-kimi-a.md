# Fix round 1 — Kimi A (from Codex review 10 + Claude confirmation)

Codex's review in `10-codex-review-a.md` is confirmed by Claude. Fix all 7 findings there, with a regression test for each:

1. Migration must never change an existing appearance. Only people who had NO appearance before migration get one. Running migration twice gives identical results. Test: save and reload with biological children; run migration twice.
2. After setting `firstname` on rebuilt relatives in `convert_child_to_player`, call `update_name()`. Tests check the full `.name`.
3. Replace `set(pool)` with a stable ordering (e.g. `sorted(set(pool))`). Add a subprocess test with different `PYTHONHASHSEED` values.
4. Schema version policy: versions > SCHEMA_VERSION or invalid values are rejected by `from_dict` (raise ValueError), and the loader falls back to a newly generated appearance for that person and logs it. Same version: normal. Lower: migrate (there's none yet, so it's a stub hook).
5. Normalize loaded `Appearance` instances through validation (unknown IDs → defaults) without rerolling valid fields. Test with a pickled instance that has an unknown ID.
6. Legacy children with no appearance inherit physical features but get styling appropriate for their real age.
7. Scope the `builtins.input` patch in `tests/__init__.py` and restore it afterwards.

Claude decision on hair recession (aging.py:53): only some people. Use the seed to give about 35% of people (any gender) gradual recession from age 40, and the rest keep their hair (greying only).

Also: the two-parent case (a Child whose second parent is attached after the first) must finalize with both parents. Add a test.

When done, update `docs/collab/kimi-a-report.md` (add a "Fix round 1" section with test output) and message Claude with `hc send t7 "..."`.
