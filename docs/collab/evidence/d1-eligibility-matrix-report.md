# D1 report: eligibility matrix RUNS + validator false positives (Kimi D, 2026-11-17)

## Result: PASS — `python3 tools/run_eligibility_matrix.py` exits 0, zero SKIP

32/32 checks green: 7 b-fixtures (+5 hard-validity variants) and all 21
matrix cases (18+/kinship/law/ui projection).

## Fixes (files I own)

1. `tools/run_eligibility_matrix.py`
   - Hook import: the frozen `evaluator_entry.module` is a repo-relative
     path (`src/game/features/relationship_policy/evaluator.py`); the
     runner now resolves a published `.py` path to a dotted module name
     (Codex finding, M2.5-review.md:147 — was the cause of all 21 SKIPs).
   - Matrix engine evaluation, previously "SKIP until A publishes a
     builder": each case now builds a REAL A graph
     (`family_graph/graph.py`: new_graph + add_parentage/form_union per
     relation — biological/half sibling, step, adoptive, in-law unions,
     first cousin, parent/grandparent) so `relatedness_category` comes
     from `graph_relatedness` — the actual A↔B integration, no stubbed
     category. Social-only relations genuinely resolve to 'unrelated'
     (contribution 0), siblings/cousins emit the soft 'kinship' reason.
   - Policy mapping per case: law `prohibited_unrecognized` → real
     jurisdiction override (recognition 'unrecognized' for all union
     types + `ceremony_of_unrecognized_union` fine [500,2000]);
     tenets permissive/hostile → faith override with favoured/disfavoured
     stances on the four relevant tenets; neutral → unaffiliated faith +
     `jur_metropolis_charter` (all recognized, no consequences).
   - adult_gate re-read as the AGE gate only (frozen protocol): the
     self-target case is both_pass on age with hard.code 'self_target'
     and attemptable=false (previous logic derived the gate from
     hard.ok and could never pass that fixture).
   - b-fixtures: views built from each fixture's context (mirrors B's
     tests/test_relationship_policy.py, incl. the `hard_validity_rejections`
     multi-case variant fixture: not_adult x2, dead, self_target,
     unknown_person — score must be None and refusal impossible on hard
     failure).

2. `tools/validate_v3_content.py` — validator false positives resolved.
   C's loader lifts bare stat keys at the top level of an effects map
   (src/game/events.py "tolerant lift": happiness/health/smarts/looks/
   karma/stress are in its `allowed` set and applied). The validator's
   V2/V3 effect-key sets omitted them, flagging 95 violations across
   A's v3_parenting_* content. Added the six stat keys to
   V2_EFFECT_KEYS (v3 inherits). `python3 tools/validate_v3_content.py
   $(find src/game/content -name '*.json')` → "OK: 21 file(s) valid".
   REAL VIOLATIONS: none. Codex's validator findings were all this one
   false positive; no content changes needed (A's/C's files untouched).

## Verification

- Runner: exit 0, 0 failures, 0 skips (output above).
- Focused regressions x3 hash seeds (0/1/9): tests.test_relationship_policy,
  tests.test_rules_dating — OK.
- Fit-proof bundle still green: 12 passed, 0 failed, 6 loader-owned SKIPs.

## Remaining gaps (WARN)

- Matrix cases execute the evaluator + real graph directly (per the
  frozen protocol's "no engine state" design); they are not routed
  through GameService. GameService-route adult-guard coverage on
  v3/legacy paths lives in the game suites + my D2/D3 browser/robot work.
- `prior_partners` and `openness` have no evaluator-visible graph
  effect (documented mapping in the runner); prior partners are history,
  not active unions.

## SELF-CHECK (docs/collab/00-SELF-CHECK.md)

- [x] Branch dev; edited only tools/** (mine)
- [x] Real evaluator + real A graph, no faked passes; SKIP→PASS only by
      running the engine
- [x] Fixed 18+ rule asserted independently of the evaluator (shape
      phase), plus evaluator not_adult hard fails for every minor case
- [x] Reason codes ⊆ B's frozen vocabulary
- [x] 3 hash seeds, focused suites
- [x] Real violations reported: NONE (all validator findings were the
      stat-key false positive)
