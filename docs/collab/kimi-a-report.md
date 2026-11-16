# Kimi A report — appearance model, generation, game integration

## What was built

**New package `src/appearance/`** (render.py left for Kimi B):

- `catalog.py` — all 17 contract categories with stable string IDs, user-facing
  labels, hex values on colour categories, and every contract minimum met or
  exceeded (12 skin tones, 21 hair styles, 6+none glasses, 6+none headwear,
  4+none earrings, 8 outfit styles, 12 outfit colours, etc.). Helpers:
  `default_id`, `normalize_id`, `is_valid_id`, `entry`, `all_ids`.
- `model.py` — `Appearance` dataclass, `SCHEMA_VERSION = 1`, `to_dict()`,
  `from_dict()` (tolerant: unknown/mistyped IDs fall back to the category
  default, marks deduped, accessories defaulted; garbage input → defaults).
- `generate.py` — `random_appearance(rng, age, gender=None)`,
  `inherit_appearance(rng, mother, father)` (either parent may be None;
  physical traits inherited with at most one feature mutated; hair colour
  blended by colour group; marks probabilistically inherited; styling never
  inherited), `age_appropriate(appearance, age)` (babies: no accessories,
  bald/buzz, no facial hair; children: no facial hair/earrings).
  Uses only a caller-supplied `random.Random`.
- `aging.py` — `age_stage`, `aged_view(appearance, age) -> dict` with
  effective hair colour/hex, grey factor, wrinkle level 0–3, recession level
  0–3, stage name. All derived deterministically from `seed` + `age`;
  the Appearance itself is never mutated.
- `integration.py` — dedicated `_APP_RNG` (never touches global `random`),
  `ensure_appearance(person, mother=None, father=None)` (inherits when
  biological parents with appearances are provided, unless adopted; otherwise
  generates independent; idempotent — never replaces an existing appearance
  when no parents are given), `copy_appearance()` (deep copy for role
  changes), `migrate_appearances(player)` (cycle-safe traversal over the
  loaded save graph; missing appearances assigned once, biological children
  inherit from their now-ensured parents, adopted children independent).

## Game integration (modified files)

- `src/people/classes/person.py` — every `Person` gets `.appearance` in
  `__init__` via `ensure_appearance` (independent, age-appropriate).
- `src/people/classes/child.py` — `finalize_appearance()` called at the end of
  `__init__` and from `set_parent()`; inheritance settles once parents are
  known; adopted/unknown-parent children keep their independent appearance.
- `src/people/classes/player.py`
  - Player's appearance is inherited from the two parents after they're built.
  - `Player.load()` runs `migrate_appearances()` so old saves get avatars
    exactly once and new saves keep theirs.
  - **Successor bug fixed** in `convert_child_to_player`: the chosen child is
    skipped when building siblings (no more self-sibling); reconstructed
    parents and siblings keep the real first names and copies of their
    appearances; the successor keeps their own appearance (copied, never
    regenerated).
- `src/lifesim_lib/safe_save.py` — `src.appearance.model.Appearance` added to
  the restricted-unpickler allowlist; the module is imported before loading.
  Unknown globals and persistent references are still rejected (tested).

## Fixtures

12 diverse bare-appearance JSON fixtures in `tests/fixtures/appearances/`
(adult/teen/child/baby/elder, varied skin tones, textures, styles,
accessories). Kimi B has since added wrapped person-descriptor fixtures; both
shapes pass validation.

## Tests

`tests/test_appearance.py` (unittest — pytest is not installed on this
machine and pip/ensurepip are unavailable; run with
`python3 -m unittest discover -s tests -t .` from the repo root; the `-t .` is
needed so `tests/__init__.py` runs, which stubs the game's interactive
language prompt and works around a pre-existing partner/player import cycle).

27 tests, all passing: catalog minimums, tolerant from_dict, round-trips,
determinism, global-RNG isolation, baby/child accessory rules, biological
inheritance vs adoption, bounded variation, ageing progression/determinism,
fixture validity, all Person subclasses, idempotency, old-save migration,
new-save round-trip, unpickler blocking, successor identity.

Final run:

```
Ran 27 tests in 0.502s
OK
```

The game still launches normally (`printf '1\n1\n' | python3 lifesim.py`
reaches the start menu and generates a family).

## Notes / unfinished

- `inherit_appearance` and `aged_view` take/return the model objects named in
  the contract; rendering API untouched (Kimi B's scope).
- Pre-existing, out of scope: `Person.generate_partner()` infinite-loops for
  age-0 players (`randint(0, 0)`); only reachable by calling it directly, not
  in normal gameplay.
- The other handoff bugs (pension/overtime accounting, divorce `parter`
  typo, child income) were explicitly out of scope for this task.
- Not committed, per instructions.

## Fix round 1 (Codex review 10 + Claude confirmation, task 11)

All 7 findings fixed, each with a regression test, plus the two extra items:

1. **Migration never changes existing appearances.** `migrate_appearances`
   now only assigns to people whose appearance is genuinely missing/invalid;
   biological children with saved appearances keep them. Running migration
   twice is identical (tested).
2. **Rebuilt relatives get `update_name()`.** Mother, father, and sibling
   reconstructed in `convert_child_to_player` refresh `.name`; the successor
   test now asserts full `.name`.
3. **Stable mark-inheritance ordering.** `sorted(set(pool))` replaces
   `set(pool)`; new subprocess test runs the same seeded inheritance under
   `PYTHONHASHSEED=0/1/2/3` and asserts identical output.
4. **Schema-version policy.** `Appearance.from_dict` raises `ValueError` for
   versions above `SCHEMA_VERSION` or invalid values (non-int, bool, <1);
   missing version = current; lower versions route through a `_migrate_legacy`
   stub hook. The loader (`integration._as_appearance`) catches this,
   generates a fresh appearance for that person, and logs a warning.
5. **Saved instances are normalized.** Loaded `Appearance` instances pass
   through `from_dict(to_dict())`: unknown IDs become defaults, valid fields
   and seeds preserved (tested with a pickled instance carrying stale IDs).
6. **Legacy children get age-appropriate styling.** `inherit_appearance`
   accepts `age` (default 0); migration and `ensure_appearance` pass the
   person's real age, so a legacy adult child inherits physical features but
   is styled as an adult (tested).
7. **`builtins.input` patch scoped.** `tests/__init__.py` restores the real
   `input` in a `finally` block; a test asserts it is not left stubbed.

**Extra — recession:** `_recession_level` now gives only ~35% of people (by
`seed % 100 < 35`, any gender) gradual recession starting ages 40–54; everyone
else keeps their hair (greying only). Bald/buzz remain choices, not recession.

**Extra — two-parent sequential finalize:** `Child.finalize_appearance`
re-settles when `set_parent` attaches a second parent, so inheritance uses
both parents (tested).

Test output (full suite):

```
Ran 37 tests in 1.357s
OK
```

The game still launches (`printf '1\n1\n' | python3 lifesim.py` reaches the
start menu).

## Fix round 2 (re-review 1: finalization idempotence)

Codex re-review found one new regression: repeated `finalize_appearance()`
or `set_parent()` with the same parent re-rolled an established child's
appearance. Fixed: `Child.finalize_appearance` now records the identity set
of parents used at finalization (`_finalized_parent_ids`) and re-inherits
only when a genuinely new parent is attached during creation. Established or
loaded children (finalized without a recorded set) record current parents
and never reroll; migration records the set for deferred children too.
Added the three requested regression tests (repeated finalize, repeated
same-parent set_parent incl. new-parent re-settle, post-load parent
reattachment). Full suite: **84 tests, OK**.
