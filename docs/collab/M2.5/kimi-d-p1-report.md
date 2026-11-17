# M2.5 Phase 1 — Kimi D report (contracts drafts)

Date: 2026-11-17. Scope: D's Phase-1 publications only (Claude's go after
Q1 D1). Everything below is a PROPOSAL against the unfrozen contracts;
nothing implements production behavior.

## Deliverables

1. `docs/collab/M2.5/contracts/validation-protocol.md` — D's contract doc:
   the nine v3 validation rules (R1–R9), the eligibility-matrix fixture
   format, and the exact inputs D needs from A/B/C before freeze.
2. `tools/validate_v3_content.py` — executable v2.1 + draft-v3 content
   validator. Verified: all 13 real v2 content files in
   `src/game/content/events/` pass unchanged (v2 is preserved, not
   relabelled); unknown `schema_version` exits 2; checks that can't run
   against a frozen source report UNSUPPORTED instead of passing.
3. `tests/content/v3/valid_basic.json` — 3-event valid v3 fixture (first
   date, second-spouse proposal, nonromantic parenting request); passes
   the validator (exit 0).
4. `tests/content/v3/invalid_minors_and_shapes.json` — 9 events that must
   each fail: romantic `min_age: 16` (the spec's explicit must-fail),
   missing `adult_only`, auto-romance, unknown binding role, duplicate
   binding, unknown condition kind, raw consent setter, no free exit,
   unbound template role. All 11 expected violations reported (exit 1).
5. `tests/content/eligibility_matrix.json` — 18 real-state cases:
   ages 17/18/19 in both participant orderings (+17/17), same/opposite
   gender, unrelated/biological-sibling/step/adoptive/in-law, hostile vs
   permissive law/tenets, prior partners, attraction/openness at both
   extremes (refusal must stay possible), prohibited-law adults stay
   attemptable, self-target and unknown-id graph errors, and the fixed
   rule that a minor can never become a preference.
6. `tools/run_eligibility_matrix.py` — matrix runner. Validates fixture
   well-formedness INCLUDING the fixed-rule assertions (minor ⇒ blocked &
   not attemptable; refusal_possible for adults; `not_adult` only with a
   minor), then looks up B's frozen `evaluator_entry` hook in
   `contracts/policy-fixtures.json`. While contracts are drafts: prints
   `SKIP` per case, exit 0 — never fakes a pass. After the freeze the
   same fixtures run unmodified against the real evaluator.

## Decisions I made (flag if wrong)

- The 18+ rule is enforced in BOTH tools independently of anyone's
  evaluator: the validator rejects romantic `min_age < 18` content, and
  the runner hard-fails any fixture expecting a minor to pass.
- Combinator vocabulary lives at the conditions root and inside
  `requires` as `{all|any|not: [...]}` with depth ≤ 4 (validator checks).
- Edge bindings must use `rel_*`/`uni_*` id prefixes so dangling strings
  are caught statically.
- `reason_codes` in fixtures are a SUBSET assertion (evaluator may emit
  more), per B's "named bounded contributions" wording.

## Needed from owners before freeze

- C: v3 schema files in contracts/ (validator R3–R5 allowlists copied
  from prose today — must be re-synced from frozen text).
- B: `policy-fixtures.json` with `evaluator_entry` {module, callable} +
  reason-code vocabulary.
- A: the chronology-guard entry point B stamps into the same hook table.

## Update 2026-11-17 (B's contract landed; B's review feedback applied)

- Matrix remapped to B's published `reason_code_vocabulary` from
  `policy-fixtures.json`; the runner validates every fixture code against
  that vocabulary and, once `evaluator.py` imports, executes B's own
  fixtures cross-contract (deterministic parts only).
- Added B's two UI projection classes as matrix cases:
  `ui/cost-preview-unrecognized-union` (projected_consequences_present,
  amount_range_ordered) and `ui/reasons-include-law-risk`
  (reasons_include). Protocol §2 documents the `expect.ui` fields.
- `interaction_kind` values are marked PROPOSAL (not confirmed) in the
  protocol and the validator, per B's flag.
- GAP flagged to Claude/B: spec 02 §3 names kinship as a bounded
  contribution, but B's vocabulary has no kinship soft code; the 4
  kinship cases assert gate/attemptability only (`_notes` on each case).
  **RESOLVED by B (2026-11-17): `kinship` soft code added** (social-only
  0, first_cousin −6, sibling −10, bound [−10,0], acceptance-only).
  Matrix now: sibling + new first-cousin cases assert the `kinship`
  reason appears; step/adoptive/in-law assert no code (0 contribution,
  below the |contrib| ≥ 3 reasons threshold). 21 cases total.
- Matrix now 20 cases; runner: 20 shape OK + fixed-rule assertions OK,
  engine evaluation SKIPPED (evaluator/graph not implemented), exit 0.

## Update 2026-11-17 (pre-freeze fit-proof run, per Claude)

- Validator re-anchored to C's published `event-schema-v3.md` (the
  `interaction`/`roles` shape supersedes my earlier prose-draft
  `bindings`/`adult_only` design): template `{person:role}` placeholders
  handled, cost rule corrected to frozen v2.1 semantics (gates only, no
  required money effect — my earlier R7 was wrong and is removed).
- New `--bundle` mode runs C's `v3-fit-proof.json`: valid fixtures must
  pass, invalid fixtures must fail for their stated reason, resolve-domain
  and knowledge records SKIP honestly. Result: 11/6/6, all 6 failures in
  C's `wedding_occurrence` fixture (reported to C, t15).
- My own v3 fixtures rewritten to the new schema: `valid_basic.json`
  passes; 10 invalid events each trip exactly one rule.
- All 13 real v2 content files still pass the v2 path (14 files incl. my
  valid fixture).

## Robot player status (Q1 D1, COMPLETE)

Quick mode finished: 2,000 lives, 0 invariant breaks, 0 soft-locks, 1
server crash (root-caused: `resolve_argument` KeyError on the insult
option — routed to Claude), 1,744 eligibility-mismatch findings (1,522
spend_time — A's age-gate fix landed; 165 doctor-in-school, 46
affordability refusals, 8 misc — routed to C). Hammer mode partial
(seed 2, 300 lives, stopped for time): same classes, no new 5xx.
Final numbers in docs/collab/q1-kimi-d-report.md.

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real objects: validator/runner operate on real content files and
      (post-freeze) the real evaluator; no mocks.
- [~] Failing-before test: no production fix made (contracts phase); the
      invalid fixture is the failing-before corpus for C's loader later.
- [x] Randomness: fixtures are static data; runner has no draws.
- [x] Inputs validated: shape errors exit 2 with typed messages; the
      fixed 18+ rule is asserted independently of the evaluator.
- [x] Money: cost-honesty rule (R7) requires declared costs to match
      money effects; ledger specifics stay with A's frozen shapes.
- [x] Identity: fixtures use person_id/edge_id shapes only; no names as
      identity.
- [~] Save/load: not in scope for Phase-1 drafts (frozen shapes pending).
- [x] Costs/eligibility: matrix asserts attemptable≠accepted and refusal
      at max scores.
- [x] Age gates: minors blocked in both tools, independent of evaluators.
- [x] No soft-locks: R6 free-exit rule; $0 life always has a legal move.
Hygiene
- [x] Only D-owned paths touched (tools/**, tests/content/**,
      docs/collab/M2.5/contracts/validation-protocol.md, my status row).
- [x] No probability/balance changes.
- [x] Validator and runner re-run; robot player re-verified under 3
      PYTHONHASHSEEDs earlier.
- [~] UI: N/A this phase.
- [x] Rare paths forced: every invalid fixture is a direct forced case,
      not a random roll.
