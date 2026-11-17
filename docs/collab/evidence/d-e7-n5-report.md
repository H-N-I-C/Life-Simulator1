# Kimi D — E7/N5 direct report (2026-11-17)

Scope: D-owned QA only (`tools/**`, `tests/browser/**`, `docs/collab/evidence/**`). No production fix attempted for N1/N2/N4; those remain owner-blockers per the E-round review.

## N5 fixes completed

1. **Dating-app fixture fixed without weakening spouse exclusion.**
   - `m25-dating-app` still starts from real `married_30`, but now D plants a genuinely unrelated adult fixture (`Riley Stranger`) before invoking the public `dating_app` action.
   - Asserted the chosen target is the planted stranger and **not** `state.family.partner`.
   - Next gameplay draw pinned to seed 31 (`u=0.012278`, below the ratified `p_accept` floor), so the stored dating response is deterministically accepted; resolve still goes through the normal decision endpoint.

2. **Divorce tautology replaced with an exact graph/family/action assertion.**
   - Removed `!!state.family.partner || true`.
   - Current tree assertion: after real graph divorce, the ex-partner may remain referenced, but `family.partner_status !== 2` and the legacy `divorce` action for that partner must not be enabled.
   - Note: the E-round text described N3 as still leaving status2/Divorce enabled; on the current tree the real scenario divorce returns `partner_status=0` and no enabled legacy Divorce action. D now hard-fails if that old married-state divergence returns.

3. **Deterministic offer response boundaries.**
   - Added `_hook_seed_next_draw` to pin only the test RNG stream for the next public action.
   - Seed 31 (`u=0.012278 < 0.02`) forces stored acceptance at the clamp floor; seed 153 (`u=0.993368 > 0.98`) forces stored refusal at the clamp ceiling.
   - `_hook_real_offer_journey` no longer retries up to eight random draws; it asserts the stored boundary, the `_ask` option, and the journal outcome for one real attempt.

4. **True plural journey + selected-divorce evidence added.**
   - New flow `m25-two-union-selected-divorce`: real spouse+child union, planted unrelated second adult, public courtship/proposal forms a second active union, the second union receives the free courthouse ceremony (`courthouse/0/hand_me_down/none`, $0), then `divorce_union` selects only the second union.
   - Asserted two active unions are public, the shared child remains bound to the original spouse union, the free wedding marks only the second union `ceremony_held` with no money movement, the selected second union is recorded divorced, no active second union remains, the spouse union+child survive, and the childless selected divorce moves no money.

5. **Deterministic refusal/acceptance browser flow added.**
   - New flow `m25-offer-boundaries`: one stranger deterministically refuses (`seed 153`) and records the declined offer only; a second stranger deterministically accepts (`seed 31`) and forms an active union.

## Selected M2.5 browser evidence

Command shape: real Chromium phone-width run of only the M2.5 flows (no legacy M1/M2 sweep, no built-in quick robot).

Result:

- **256 PASS / 0 FAIL / 0 WARN / 0 PENDING / 0 required-pending**
- Evidence report: `docs/collab/evidence/browser/m25-flows/report.md`
- Screenshots in the same directory.
- Browser-check server saves were moved to `game_saves/` for this run; the one transient `/tmp`-save failure class was infrastructure quota, not a game assertion.

Flows included:

- `m25-dating-app`
- `m25-divorce`
- `m25-offer-boundaries`
- `m25-two-union-selected-divorce`
- `m25-editor-law`
- `m25-family-tree`
- `m25-secrecy`

## 500-life robot status

Command:

```bash
python3 tools/robot_player.py --lives 500 --seed 1 --tolerate-codes not_allowed --max-failures 0
```

Status at this report: **still running and actively progressing** (creation ledger observed through life 159/500; process CPU high). Current failure snapshot through life 159: 23 recorded failures = 22 typed `refused` findings (predominantly real `try_for_baby` refusals) + 1 hard HTTP 5xx below.

Hard failure root-caused from `logs/server.log`:

- Evidence: `docs/collab/evidence/robot/robot-seed1-life58-http_5xx-20261117-223137.json`
- Crash id: `001e1ecd2c6d`
- Error: `LedgerConflict: posting key reused with different details: t_000154:mortality:inheritance:21a72c0f-bb40-4836-b5c4-8c2e4f7343e8:transfer`
- Path: `age_up` → mortality phase → inheritance money posting.
- Owner: A/C ledger-key interface; **not** counted as clean.

Typed 4xx findings so far are predominantly real eligibility/refusal outcomes (for example `try_for_baby` → HTTP 409 `refused`); they are reported as findings, not hard failures.

## 9 full-suite skips

Source: `docs/collab/evidence/full-suite-9-skips-explained.md` (independent full suite reported separately as 728 tests, 9 skips).

- 6 skips are explicit `LIFESIM_SLOW_TESTS=1` performance gates: lifespan trial, four slow relationship flows, and the near-retirement drive.
- 2 skips are owned wiring gaps: completed-lives record on surrender death (A) and doctor-fee posting (A/C). They become failures once wired.
- 1 skip is a fixture-data limit: the seeded decision has no +happiness option for the capped-value assertion.

None is silent; none is counted as a pass in D’s gates.

## Remaining owner blockers

- **N1/N2:** conception recording/consumption remains with Kimi A; D has not edited production or claimed it fixed.
- **N4:** law/stage-contract public proposal issues remain with Kimi C + A/B; D has not edited production or claimed it fixed.
- Robot hard failure above is routed as A/C ledger-key work, not a D pass.


## Round 4 integrated evaluation addendum

Independent Round 4 confirmed D’s corrected selected browser paths:

- Selected M2.5 Chromium flows: **256 PASS / 0 FAIL / 0 PENDING / 0 WARN**.
- D’s dating/plural/exact-divorce improvements are accepted.
- Matrix: 21/21 PASS, zero skips.
- Content: 22/22 valid.
- Full discovery: 735 tests, **1 failure**, 9 skips; the failure is B’s unseeded `$0` wedding-affordability fixture (`test_unaffordable_wedding_is_typed_not_allowed`), not a D browser/matrix/content failure.

Round 4 owner findings now preserved in D’s evidence:

- **W1 inheritance collision (A/C ledger/coordinator):** independently reduced by Codex to two real old parents, generosity100, mortality cursor, saved RNG seed3; same HTTP 500 and unchanged save under hash seeds 0/1/42. This is the same class as D robot life 58 and remains a hard failure.
- **W2 miscarriage/new-keep identity (A/C):** real miscarriage after `d_1`, then new successful keep `d_5`, reuses the old `d_1` conception outcome byte-for-byte. D does not count conception identity complete.
- **W3/N4 law/stage contract (C with A/B):** still unchanged/review-only; forbidden evaluator recognition can still result in a recognized public-proposal union and stage prerequisite remains unenforced.
- **N1/N2 original cases:** closed by owner/Codex round; W2 is the remaining lifecycle blocker found by the new miscarriage probe.
- **N3:** closed on current tree; D’s exact post-divorce assertion guards it.

Robot exact snapshot at this addendum (job still running, high CPU):

- Command: `python3 tools/robot_player.py --lives 500 --seed 1 --tolerate-codes not_allowed --max-failures 0`
- Creation ledger observed through **life index 191** (192 lives created).
- Evidence files so far: **24 failures**
  - 23 typed 4xx findings, all code `refused` (predominantly real `try_for_baby` refusal outcomes);
  - 1 hard HTTP 5xx: life 58, W1 inheritance `LedgerConflict`, crash id `001e1ecd2c6d`.
- This is **not** a clean/final 500-life run. D preserves the hard failure and will publish a final exact summary after owner fixes and a rerun.


## Round 6 L3 update — current-tree robot and L1 check

D stopped the stale pre-W1/W2/W3 robot process because it was started before owner fixes and was running old imports. It is preserved as historical evidence only, not final disposition.

Fresh current-tree run launched:

```bash
python3 tools/robot_player.py --lives 500 --seed 1 --tolerate-codes not_allowed --max-failures 0
```

L1 browser assertion added and now passes on the current tree:

- New flow: `m25-law-ceremony-fine`
- Real editor configures forbidden recognition plus fixed `$500` `ceremony_of_unrecognized_union` civil fine.
- Public courtship/proposal persists forbidden recognition.
- Free courthouse ceremony is held.
- Asserted money drops by exactly `500` and exactly one new ledger row posts with category `legal`, amount `-500`.
- Selected M2.5 Chromium evidence including this L1 flow: **298 PASS / 0 FAIL / 0 WARN / 0 PENDING**.


## Historical 5xx disposition on current tree

- **W1 inheritance reduced repro:** `codex_m25_eval4_inheritance.py 3` returns HTTP 200 with pending `funeral` under `PYTHONHASHSEED=0`, `1`, and `42`. The save changes only because the command now succeeds; no `LedgerConflict` occurs.
- **Old robot W1 life 58 replay:** `tools/robot_player.py --replay docs/collab/evidence/robot/robot-seed1-life58-http_5xx-20261117-223137.json` completes 197 commands with **0 new 5xx**.
- **Old robot life 340 replay:** `tools/robot_player.py --replay docs/collab/evidence/robot/robot-seed1-life340-http_5xx-20261117-105002.json` completes 8 commands with **0 new 5xx**. The archived Round 5 note still stands for cause attribution: this proves the old HTTP 500 does not recur, not that the original crash cause is newly proven; the exact old pending sequence can end in typed `no_pending_decision` instead.


## Fresh current-tree 500-life robot — interim snapshot

Command:

```bash
python3 tools/robot_player.py --lives 500 --seed 1 --tolerate-codes not_allowed --max-failures 0
```

Status: **running**.

Interim observed progress: creation ledger through life index **86** / 500. Evidence failures so far: **6**, all typed 4xx with code `refused` (real NPC/refusal outcomes). Current-run 5xx so far: **0**. No invariant/soft-lock evidence files so far.

This is not the final acceptance summary; final counts will be written only after the process exits.
