# Kimi C — Round 4 report (W1, W3, W2-C-half), 2026-11-17

Branch `dev`. All changes in owned files (`src/menus/rule_actions.py`,
`tests/test_rules_dating.py`, `tests/test_rules_m2b.py`) plus coordination
docs. One task at a time, in W1 → W3 → W2 order.

## W1 — inheritance occurrence collision (P1) — DONE

`sample_relation_death` posted every parent bequest as occurrence
`"inheritance"` with the player as owner; two parents dying in one turn
produced the same posting key (`turn:mortality:inheritance:<player>:transfer`)
with different amounts → `LedgerConflict` → HTTP 500 on public `age_up`.

Fix (`rule_actions.py`): occurrence is now the **deceased's immutable
id** — `"inheritance:{_pid(relation)}"`. Category stays `inheritance`
(the ledger already carries the recipient); `LedgerConflict` is untouched.

Test: `W1InheritanceCollisionTest` (tests/test_rules_m2b.py) — two
parents, different estates, same turn, on a real persisted GameService:
distinct occurrences keyed by each deceased, both post through
`rule_adapter.post_money_entries` into the real ledger, balance equals
the sum, and an exact replay is idempotent (no conflict, no double
credit).

## W3 — N4 implementation (P1) — DONE

All three reviewed items, in `sample_targeted_offer` /
`_relationship_specs`:

1. **Projection persisted at offer creation.** `projected_recognition`
   and `projected_consequences` from B's evaluator are stored in
   `pending["sampled"]`; the existing applier hook
   (`effects.py:74`, reads `sampled_outcomes["projected_recognition"]`)
   now receives them, so an accepted public proposal forms the union
   with the recognition the law actually projects — never the
   recognized default.
2. **Stage parity.** `union_proposal` requires an active
   courtship-stage edge (`_active_stage_edge`, kinds courtship /
   committed / lover / consort). Dispatch refuses typed `not_allowed`
   ("you need an active courtship before proposing a union"); the spec
   is advertised `enabled: False` with that exact reason when the stage
   is missing, `enabled: True` once courting. Courtship_offer is the
   stage entry and stays as-is.
3. **Ordinary/plural selection.** When either side already has an
   active union (`ServiceGraphView.active_union_count`), the proposal is
   scored and stored as `propose_plural_union` (derived event def via
   `_plural_union_event`), so the evaluator reads the jurisdiction's
   `plural_civil` recognition and the stored offer/applier carry the
   kind actually evaluated.

Law-illegal attempts stay admissible: forbidden/unrecognized
recognition is stored and applied as a consequence, never gated.

Tests (`W3LawProjectionServiceTest`, tests/test_rules_dating.py), all on
the real persisted GameService with the real evaluator and the real law
editor (`editor_apply` → `edit_jurisdiction`):
- monogamous recognition `forbidden` → evaluator projection `forbidden`
  → stored offer carries it → resolved union's
  `recognition.status == "forbidden"`.
- existing second union + `plural_civil: "unrecognized"` → evaluator
  spied as `propose_plural_union` → stored offer `interaction_kind ==
  "propose_plural_union"` → new union's recognition `unrecognized`
  (never the monogamous default).
- no-courtship: spec disabled with reason + dispatch `not_allowed`
  before the stage; enabled and admitted after a real accepted
  courtship offer (existing service test extended, not weakened).

## W2 — miscarriage stale conception (P1) — C HALF DONE, A HALF OPEN

C's miscarriage writer (both carrier branches of `family_events_phase`)
now retires the lost pregnancy's draw at the loss via the new
`terminate_active_conception(env, player, turn)`: marks the active
unconsumed conception outcome `terminated` (+ turn, immutable history
never erased) and clears `player._active_conception_key`. `env` is
plumbed through `advance_phase` (keyword-only, default `None` — the
phase signature stays backward-compatible).

**A's half (Open item #1 in contracts/README.md):**
`record_legacy_conception` must not reuse a terminated record and
`find_conception_outcome` must never match one — otherwise the lost
`d_1` draw is still reused by the later `d_5` pregnancy and W2 stays
open.

Tests (`W2MiscarriageTerminationTest`, tests/test_rules_m2b.py):
both carrier branches terminate + clear the binding (pair fallback when
the branch doesn't know the key); the continuing-pregnancy branch does
NOT terminate (draw survives for the birth).

## N4 consequences — Open item to A/B

Projected consequences are now persisted in `sampled_outcomes`, but
nothing applies configured fines at union formation (Open item #2 in
contracts/README.md): should `form_union_op` journal them or the
applier? C cannot journal graph-side ledger entries from rule_actions.

## Verification

- My 8 suites (dating, m2b, v3_dispatch, v3_loader, event_loader,
  event_content, property, hashseed): **OK × PYTHONHASHSEED 0/1/42**.
- All `test_rules_*`: 278 tests OK.
- Full-suite run: see status row (background at report time).
- `git diff --check` clean.

## Self-check

- [x] Branch `dev` verified.
- [x] Round-4 evaluation (M2.5-review.md W1–W3) read before editing;
      repro scripts consulted for the law/editor and inheritance paths.
- [x] Edits confined to owned files (+ coordination docs); A's
      `effects.py` / `parenting/core.py` / `finance.py` untouched.
- [x] LedgerConflict not weakened; stage gate keeps attempts expressible
      (typed refusal, accurate reason) and law-illegal proposals
      admissible with accurate consequences.
- [x] Real-path tests on the production service for W1 and W3; no
      weakened assertions (existing union test now goes through the real
      courtship stage — stronger, not relaxed).
- [x] Open items filed for both cross-owner dependencies.

## Addendum — W3 verification on Codex's own fixture (Claude's "still unfixed")

Reran Codex's exact round-4 law probe
(`codex_m25_eval4_law.py`, fixture `test_game_m25_codex_fixes.CodexFixes`)
against the current tree, stage-compliant:

- `union_proposal` with **no courtship edge** → public action refuses
  typed `not_allowed: "you need an active courtship before proposing a
  union"`.
- Real stage entry (public `courtship_offer` → accepted → active
  `courtship` edge) under editor-forbidden monogamous recognition:
  evaluator `projected_recognition.status = "forbidden"` → stored offer
  `sampled.projected_recognition.status = "forbidden"` → resolved union
  `recognition.status = "forbidden"` (never `recognized`).

Both previously-reported failure modes now behave per contract on the
public action path; forced service tests in `tests/test_rules_dating.py`
(`W3LawProjectionServiceTest`, 2 tests) pin both.

## Addendum 2 — W2 closed end-to-end (contract aligned with A)

A's `test_w2_miscarriage_retires_draw_next_pregnancy_is_new`
(`tests/test_game_m25_conception.py`) encoded the consumer contract:
`terminated == "miscarriage"` (cause string, history) **and**
`consumed == True` (reusing A's existing never-reuse semantics so no
consumer change was needed). C's `terminate_active_conception` was
adapted to exactly that contract. The test now passes on the real
service: real keep (`d1`) → forced miscarriage (public `age_up`) → lost
draw terminated+consumed → second real keep records a NEW outcome with
its own `pregnancy_id`. Full suite: 749 tests, only that test errored
pre-alignment; rerun green (see status row). Open item #1 marked
RESOLVED in contracts/README.md.
