# D round-2 addendum (R11 + robot 5xx + skips), Kimi D, 2026-11-17

## R11 items

1. **Fixture chronology — FIXED** in `tools/run_eligibility_matrix.py`:
   helpers were all age 50, so a first-cousin fixture could have a
   "parent" the same age as their child. Now: helper default 55,
   first-cousin grandparents 80 / parents 55 (strictly ordered
   generations), grandparent_grandchild middle generation derived from
   the target's fixture age. All 21 matrix cases rerun: PASS, 0
   failures, 0 skips, under hash seeds 0/1/9.
2. **HTTP secrecy checks — DONE**: new `m25-secrecy` browser flow
   plants a private journal row + a player-unknown secret fact (the
   same planting as A's sentinel test) and asserts over REAL HTTP
   responses: get_state JSON must not contain the private sentinel or
   the hidden fact id; relationships-view omits the unknown fact; after
   diegetic learning the fact appears in the view while the private row
   stays hidden. 16/16 PASS. Evidence:
   docs/collab/evidence/browser/m25-flows/ (m25-secrecy.png + report).
3. **Real journey replacing the D2 shortcut — DONE (E7)**:
   `m25-divorce` and `m25-editor-law` no longer call the in-process
   `form_union` setup op. Each flow divorces the auto-migrated
   neutral-law union through the public wedding endpoint, then remarries
   the same co-parent through the authorised public action journey:
   `courtship_offer` → stored v3 response → `union_proposal` → stored
   v3 response. The helper asserts action-spec advertisement, target
   binding, ONE stored accepted/refused answer, the `_ask` response
   option, journal evidence of the stored answer, and finally exactly
   one active graph union with the target. Browser evidence rows: both
   E7 flows green; editor-law reprojection/replay/stale-revision checks
   green. Related current-tree exception: the older `m25-dating-app`
   flow now hits `no eligible adults` on `married_30` after A's
   partner→graph sync; routed as an A/C predicate-regression, not
   masked by D.

## Robot scenario-setup 5xx — REPRODUCED and routed

Not a flake: ~0.2% of random profiles (deterministic seeds 2021261880,
254938035, 1852648667) fail `university_pending_17` setup at the
university smarts gate, and `src/game/scenarios.py` raises
`GameError(internal_error, …, 500)` — a client-retryable precondition
mis-mapped as a crash. Minimal repro evidence:
docs/collab/evidence/robot/scenario-setup-5xx-repro-20261117.json.
Robot mitigation already in place (boundary lives pin seed 777), so D
runs no longer trigger it; the mapping fix is A-owned (Open item 4).

## The 9 full-suite skips — explained

docs/collab/evidence/full-suite-9-skips-explained.md: 6 are
`LIFESIM_SLOW_TESTS=1` performance gates (slow relationship flows,
lifespan trial, near-retirement drive), 2 are owned wiring gaps
(A: completed_lives on surrender death; A/C: doctor-fee posting), 1 is
a fixture-data limit (no +happiness option to observe capped values).
None is invariant coverage; none is silent.

## 500-life M2.5 robot run — RUNNING

Unblocked after A/B/C reported. Current run:
`python3 tools/robot_player.py --lives 500 --seed 1 --tolerate-codes not_allowed --max-failures 0`.
Earlier attempts: two cascading-500 runs root-caused to the user disk
quota on /tmp (EDQUOT, OSError 122, instrumented trace captured), fixed
by saving under game_saves/ + reclaiming 1.5G of stale tmp dirs; the
mechanics (boundary lives, minor-romantic probes, money conservation)
are validated — 16 boundary lives, 667+ probes, zero probe 5xx.

## SELF-CHECK

- [x] dev branch; only tools/**, tests/browser/**, docs/collab/evidence/**
- [x] forced real-path: matrix cases against the real evaluator+graph;
      secrecy asserted over real HTTP GETs; scenario 500 reproduced
      with deterministic seeds
- [x] 3 hash seeds on the matrix; focused suites OK
- [x] no new skips; PENDING=FAIL gate intact (0 PENDING in flows)
- [x] cross-owner fixes (scenario error map, R5 journey) routed via
      Open items, not edited
