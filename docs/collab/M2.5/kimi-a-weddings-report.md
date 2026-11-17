# M2.5 A — Weddings & marriage report (Kimi A, 2026-11-17; ownership
transferred from C step 4). Design note: contracts/README Open items.

## What landed

- **`src/game/features/weddings/core.py`** — fictional versioned price
  catalog (`VENUES` courthouse/garden/hall/grand, `OUTFITS`,
  `HONEYMOONS`; free tier in each = the legal no-cost exit),
  `plan_wedding` (per-part single ledger postings, occurrence
  `wed:<union_id>`, leg per part, affordability gate BEFORE any posting —
  no partial charges, ceremony recorded on the SELECTED union edge with
  the player's AGE as the calendar), `renew_vows` (free backyard tier +
  paid tiers, counted per union), `divorce_union` (selected edge only;
  settlement = cash prenup amount once, or $0 — no invented default
  splits; shared dependent children (A's `dependent_children` both sides)
  get a one-time `child_support` posting + obligation/household record;
  parentage and other unions untouched; forbidden recognition never
  blocks it), and `anniversary_check` (year_start hook: milestones at
  1/5/10/… using player AGE, journaled once per union per milestone,
  +2 happiness).
- **`GameService.wedding_command`** — replay-safe wrapper (byte-identical
  retries replay from the log; the stored payload is the caller's args —
  the injected `turn` is kept out of the replay key after a real retry-
  conflict bug). Typed `GraphError` → `GameError` mapping.
- **Envelope**: `env["weddings"]` backfilled in new_game/_read_save
  (A-owned write); anniversary/renewal state persists across reloads.

## Bugs found and fixed during testing

- **Anniversary calendar used `revision`**, which counts resolves and
  commands within a year — a 4-year-old marriage showed "5 years" after
  four age-ups. Ceremonies now stamp player `age` and anniversaries diff
  ages.
- **Replay payload pollution**: injecting `turn` into the stored replay
  args made legitimate retries a conflict; the wrapper now stores the
  caller's args only.
- Ledger rows carry `leg` only inside `source_key` — tests filter by
  description instead.

## Tests — tests/test_weddings.py (9, OK x3 hash seeds)

Real GameService + graph_command-formed plural unions: free courthouse
wedding at $0 (no charge, ceremony held, recognition preserved); grand
wedding charges each part exactly once ($18,000 total, one row per part,
replay does not double-charge); unaffordable wedding refused with $0
spent and ceremony NOT held; second ceremony for the same union refused;
vow renewal free tier costs nothing, paid tier posts once, counted;
anniversaries: exactly one year-1 journal across 4 years (full-journal
test bug fixed — counting from the final state); selected-union divorce
among two spouses (divorced A, B stays active, prenup settlement exactly
one -$5,000 row, replay-safe); no-prenup divorce = settlement $0 but
child support posts once + obligation/household created + parentage
preserved; forbidden-recognition union divorces fine. Full regression
145 tests OK; web M1/M2 OK.

## Self-check

- [x] Money: every charge/settlement exactly one ledger row; gate before
      posting (no partial); free legal exit in every choice (courthouse /
      no-frills / backyard / decline).
- [x] Selected-edge semantics: plural unions; parentage untouched.
- [x] Identity: person_id/union_id only; replay byte-identical.
- [x] Save/load: envelope backfill; anniversary state persists.
- [x] Adult guard: unions only form via graph transitions (18+ enforced).
- [x] 3-seed runs; no files outside A ownership (coordinator year_start
      call is A's).
- [ ] UI: venue/outfit pickers not built (commands are the M2.5a surface;
      C's offer UI can call wedding_command).
