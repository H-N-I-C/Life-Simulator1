# Kimi A — 'Undo last year' engine report (2026-11-20)

User-approved BACKLOG item, engine side. Contract published FIRST:
**contracts.md §Undo (FROZEN v1)**. No commit.

## Shape (§Undo v1, frozen)

- Projection: `"can_undo": {"enabled": bool, "reason": null | str}` —
  reasons: "there is no year to undo" (fresh / consumed / continue_as),
  "only one year can be undone" (just used).
- `undo_year` is ALSO advertised in `available_actions` (category `life`,
  label "Undo last year", same enabled/reason) — single source
  `GameService._can_undo`. Advertised even when dead (the only action
  then); disabled+reason renders per the shell's disabled rules.
- Command: `command(gid, expected_revision, request_id, "undo_year", {})`.
  Allowed while a pending decision is open (the pending belongs to the
  undone year and vanishes with it) and while dead (restores the living
  pre-year state). Whitelisted in `command()`; `pending_decision` /
  `game_over` guards bypassed for it by name.

## Implementation (src/game/service.py)

- `_snapshot_for_undo(env)`: at the start of every `age_up` execution, a
  full pickle of the pre-age envelope (minus the prior snapshot) is stored
  at `env["_undo_snapshot"]`; each age_up REPLACES it (one level, always
  "the last year"); clears `_undo_consumed`.
- `_cmd_undo_year(env)`: validates (`_validate_envelope`) and restores the
  whole snapshot — ledger/money, people graph (relations/partner/children/
  dead/archive), journal/feed, pendings, knowledge/facts, flags, replay
  ledger as of then, and BOTH RNG streams. Runs OUTSIDE `_GameRNG` (the
  wrapper would re-commit the post-year stream over the restored one);
  rehydrates `random`, `_COSMETIC_RNG` and `integration._APP_RNG` from the
  restored envelope under `_COMPAT_LOCK`. Sets `_undo_consumed`.
- `_cmd_continue_as` clears the snapshot (no cross-generation undo).
- Anti-bypass: because the RNG streams are restored, the next `age_up`
  consumes the identical stream — the redone year replays EXACTLY (same
  events, same eligibility/18+ outcomes). Undo is never a re-roll. (The
  test helper learned this the hard way: undo+re-age deterministically
  reproduces the same interruption forever.)
- Replay-safety: undo rides the normal command machinery. The undone
  year's request_ids leave the replay ledger with the rollback, so a
  retried `age_up` re-executes the identical year (desired redo path); a
  retried `undo_year` returns the byte-identical stored response.

## Tests (forced, 3 seeds: 7 / 4242 / 90210)

New `tests/test_game_v2_undo.py` (8 tests):
1. can_undo lifecycle + advertised spec + typed 409 on undo with nothing.
2. Full restore across 3 seeds: stats/money/people/journal/feed/pending/
   education/work/health/family/finances byte-identical to the pre-year
   capture; actions identical except the undo_year availability itself;
   revision lands at pre-year + 1.
3. Determinism (3 seeds): the redone year's journal lines (text + changes)
   are byte-identical — no re-roll.
4. Replay: undo retry returns the byte-identical stored response; a
   retried undone-year age_up request re-executes the identical year.
5. stale_revision typed on undo.
6. Mid-year rule action (spend_time) rolled back with the boundary;
   availability matches the pre-year advertisement.
7. Undo with a pending decision open: pending cleared, surfaces match
   pre-year.
8. (in m2a) death advertises exactly undo_year only.

Test-infra fixes worth noting: unique request ids per settle call (replay
dedupe returned stale responses when ids collided — the snapshot was then
one year stale); the m2a surrender test's `available_actions == []` on
death updated to the new contract (undo_year only).

## Checks run (TMPDIR=~/.lifesim-tmp per new rule; no browsers used)

- `unittest tests.test_game_v2_undo` — 8/8 OK.
- `pytest test_game_m2a + test_game_v2_undo + test_game_scenario_retry +
  test_game_v2_journal + test_game_v2_avatar_defaults + test_game_v2_person
  + test_rules_event_loader + test_appearance` — 136 passed, 3 subtests.

## B handoff

Wire the shell's Undo control to the advertised `undo_year` spec +
`shellHooks.sendAction` (busy/retry semantics already on the shell);
`can_undo` is for display gating. Death screen may offer Undo (restores
the living pre-year state); Continue-as clears it server-side.

## Codex undo-engine review corrections (2026-11-20, same no-commit batch)

Review verdict CHANGES REQUESTED → all four corrections implemented in
`src/game/service.py` (`_cmd_undo_year`), contract §Undo updated, import
check (`python -c 'import src.game.service'`) run after every edit:

1. **Monotonic revision (High):** undo restores gameplay state, NOT the
   concurrency token — response revision is always `pre-undo + 1`
   (1 → Age 2 → Undo **3**). Stale clients holding a pre-undo revision get
   `stale_revision`; verified across a multi-command year (age + resolves)
   where the old behavior rewound by more than one.
2. **Restricted loader (High):** the snapshot is deserialized via
   `safe_save.load_save(io.BytesIO(snap))` (SaveUnpickler) — never bare
   `pickle.loads` — and validated before application. Forced test: a
   tampered snapshot carrying a disallowed global is rejected WITHOUT the
   global being invoked; the game stays playable.
3. **Ambient RNG (Medium):** undo no longer touches `random`,
   `_COSMETIC_RNG`, or the `_APP_RNG` binding at all — the restored
   streams are the single source of truth and the next `_GameRNG` entry
   rehydrates from them. Test asserts `random.getstate()`,
   `_COSMETIC_RNG.getstate()` and `_APP_RNG` identity are unchanged.
4. **Retry vs redo (contract frozen):** old request_id + original
   `expected_revision` = delayed retry → `stale_revision` (never a
   byte-identical success, never mutates the restored timeline); same
   request_id + CURRENT revision = explicit redo, re-executes the
   identical year deterministically; `undo_year`'s own retry stays
   idempotent via the replay ledger.

**Verification:** Codex's own reproducer
(`docs/collab/evidence/redesign-v2/undo-engine-review/reproduce.py`) now
records revisions 1/2/3, `globalRandomChangedByUndo: false`,
post-age stale client and original-age retry both rejected with
`stale_revision`. Suite: `tests.test_game_v2_undo` 13/13 (incl. 3-seed
forced determinism/restore); combined pytest (undo, m2a, scenario_retry,
v2_journal, v2_avatar_defaults, rules_event_loader) 82 passed + 3
subtests; TMPDIR=~/.lifesim-tmp; no browsers used.
