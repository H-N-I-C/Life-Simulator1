# Codex Undo engine review — corrections verified, UI pending (2026-11-20)

## Correction verification

A's consolidated corrections have now been independently checked. The
original three engine blockers below are **resolved**: revisions advance
1 → 2 → 3, stale clients cannot mutate after Undo, snapshot loading uses
the restricted loader before validation/application, and Undo leaves the
caller's random streams and appearance RNG binding untouched. The revised
retry/redo contract explicitly rejects the old persisted Age operation as
stale while retaining idempotent Undo retries and deterministic explicit redo.

- Own reproducer: `corrected-results.json` records 1/2/3,
  `globalRandomChangedByUndo: false`, both stale requests rejected.
- Independent focused run: `tests/test_game_v2_undo.py` plus
  `tests/test_game_scenario_retry.py`: **15 passed, 3 subtests passed**.
  Includes the disallowed nested-global regression and the corrected
  monotonic-revision/RNG checks; no skips in this run.
- `git diff --check` passed.

**Engine correction review passes. Integrated browser acceptance remains
pending B's delivered UI, C's richer journal, and D's evidence.** This is
not approval of the entire new round. The findings below describe the
original implementation and are retained as the rationale for the fixes.


A's delivery was reviewed against §Undo and the existing revision, retry,
restricted-save and process-global RNG guarantees. This is an engine review;
B's integration report and D's browser evidence have not arrived, so the new
round remains **pending integrated review** and integrated UI review is still pending.

## Required corrections — one consolidated engine task

1. **High: revision rollback permits stale-client writes.** `_cmd_undo_year`
   restores the snapshot's old revision, then `_execute` increments it.
   Reproduced with the real service: fresh revision 1 → Age revision 2 →
   Undo revision 2. A client holding the post-age revision 2 successfully
   submits another Age after Undo instead of receiving `stale_revision`.
   Keep the transport revision monotonically increasing across restoration;
   restore gameplay state, not the concurrency token. The test asserting
   `before['revision'] + 1` after Undo currently pins the wrong behavior.
   Add a real two-client regression using the pre-Undo revision, including a
   year with multiple resolved choices/actions (where the counter can go
   backwards by more than one).

2. **High: nested snapshot bypasses the restricted save loader.**
   `_cmd_undo_year` calls `pickle.loads(snap)` before validation. The outer
   save loader permits bytes, so a nested pickle is opaque to
   `SaveUnpickler`; Undo then interprets its globals unrestricted. Existing
   `src/lifesim_lib/safe_save.py:load_save` blocks unapproved globals, and
   must also protect the snapshot (for example using a BytesIO stream), or
   the snapshot must use a representation that requires no unrestricted
   deserialization. Validate the restored envelope before applying it, and
   test that a disallowed nested global is rejected without being invoked.
   This is source-confirmed; no malicious payload was executed in review.

3. **Medium: Undo changes process-global RNG state and cosmetic RNG binding.**
   `_cmd_undo_year` permanently calls `random.setstate`, changes
   `_COSMETIC_RNG`, and reassigns `integration._APP_RNG`. A lock protects
   those writes only while the lock is held; it does not restore the
   caller's state afterward. The service's `_GameRNG` contract explicitly
   saves/restores the caller's globals. Reproduced:
   `random.getstate()` differs before/after an Undo. Restoring the two
   persisted game streams is sufficient for the next `_GameRNG` entry;
   avoid changing ambient globals, or restore all caller state/bindings in
   finally. Test the RNG objects/states as well as interleaved games and
   identical redo results.

## Retry contract needs alignment with the correction

A intentionally removes the undone year's request IDs, and its redo test
resubmits the old request ID **with a changed expected revision**. That is
not a lost-response retry of the original persisted operation. Preserve a
clear distinction between a new explicit redo and a delayed retry, and
freeze the behavior in the contract/tests. In the reproduction the original
Age request retried with its original expected revision receives
`stale_revision`; do not describe that path as a successful byte-identical
retry. Keep Undo's own retry idempotent and prevent late requests from an
undone timeline from mutating the restored one.

## Evidence

`docs/collab/evidence/redesign-v2/undo-engine-review/reproduce.py` runs against
real GameService objects and isolated saves. `results.json` records:

- revisions 1 → 2 → 2;
- stale post-age client action accepted at the restored age;
- process-global random state changed by Undo;
- original request/expected-revision retry rejected as stale.

Run from the repo root with TMPDIR under `.review-tmp/codex-feel-tmp`.
No production edits, source rollbacks, or stash operations were performed
for this review. Submit the corrections together, then Codex will verify
engine invariants and the real browser flow after B reports.
