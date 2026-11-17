# D validation protocol — PROPOSAL (Kimi D, 2026-11-17)

Draft until C's v3 schema files and B's evaluator land in this directory;
Claude freezes everything together. Two deliverables: the v3 content
validator rules (`tools/validate_v3_content.py`, executable) and the
real-state eligibility-matrix fixtures (`tests/content/eligibility_matrix.json`
+ `tools/run_eligibility_matrix.py`). Nothing here implements production
behavior; both tools FAIL LOUDLY (not silently pass) on shapes that are
still drafts.

## 1. v3 content validation (task doc item 1)

Validator input: one event file (top-level `schema_version`, `events[]`).
It must validate v3 files and KEEP validating v2.1 files (v2 is preserved,
not relabelled). Rules, in check order:

R1 **Version gate.** `schema_version` ∈ {2, 3}. Version 2 files are checked
against the frozen v2.1 constraint set (id/title/text/category/weight/
conditions/choices/Effects — the validator embeds a copy of the v2.1
constraint summaries from docs/collab/M2/event-schema.json). Version 3
files get the additional rules below.

R2 **Adult guard is a schema-level invariant, not a condition.**
Every event with `interaction: {"kind": K, "romantic": true}` where K ∈
{`flirt`, `date`, `propose_courtship`, `propose_union`,
`propose_plural_union`, `convert_request`, `repair_attempt`} (B's set —
PROPOSED until C confirms and Claude freezes, per B 2026-11-17) MUST have
`conditions.min_age` ≥ 18 when `min_age` is present. A romantic event with
`min_age: 16` (or any < 18) FAILS VALIDATION even though the runtime guard
also checks actual participants. `auto: true` romantic events are a hard
fail: automatic events cannot create consent or forced unions (nor carry
offer/respond/end_union/sample_conception effects).

R3 **Roles (bindings).** v3 declares a `roles` object: each role name maps
to `{kind: person|relationship|union|household, select: player|predicate,
predicate?, optional?}` (event-schema-v3.md §1). Role kinds and select
values outside the allowlist fail; `select: predicate` requires a
predicate name; `audience.participants`, effect role fields
(`to`/`between`/`union_role`/`fact_role`/`person`/`child`/`mother`/
`father`) and `{person:role}` template placeholders must reference
DECLARED roles (v2 placeholder names `mother|father|partner|child|sibling|
all_parents|random_parent|random_relation` stay legal in templates).

R4 **Conditions vocabulary.** Only the allowlisted clause kinds: v2's set
plus C's v3 leaves `relationship_state`, `union_state`, `expectation`,
`recognition`, `tenet_stance`, `faith`, `jurisdiction`,
`attraction_band`, `known_fact`, `reputation_at_least`, `kinship`,
`milestone`, `bound`. Combinators: `all`/`any`/`not`, bounded depth.
Anything else (eval, python paths, free-form strings) fails.

R5 **Effects vocabulary.** Only the typed effect set: v2 Effects plus C's
v3 domain commands `offer`, `respond`, `end_relationship`, `end_union`,
`reveal_fact`, `reputation`, `chemistry`, `convert_profile`,
`set_personal_tenet`, `reproject_recognition`, `milestone`,
`sample_conception`. Role-bearing fields must name declared roles. Raw
salary/graph/consent setters (`set_partner`, `set_consent`, `set_age`,
`set_money`, `set_relationship`) fail. `effects.money` stays a single
posting per occurrence/choice (v2 parity).

R6 **Free exit.** Every manual (non-auto) event with choices has ≥1 option
with zero cost and no gating condition that can disable it (the legal
decline/rest route). An event whose only exit costs money fails — a $0
life must always have a legal move.

R7 **Costs are gates only (frozen v2.1 semantics).** `cost.money` must be
a non-negative int; it produces the affordability gate (enabled:false +
reason) and NEVER requires a matching money effect — declared costs and
money effects are independent, exactly as in v2.1.

R8 **Asset references.** Any `look_override`/asset field must resolve
against the M0b manifest (read-only); unresolved asset ids fail. Assets
must never change base/gender/skin/hair (wedding/graduate contexts use
manifest fallback).

R9 **Legacy binds (v2 compatibility in v3 files).** `gain_partner`,
`lose_partner`, `divorce`, `partner_death` effects in a v3 file must name
the intended bound person/edge at offer creation; a resumed v2 choice may
not re-target (stale context ⇒ explicit `cancelled`, never silent
re-roll). Validator checks the binding is present and that the event id
is not reused across schema versions.

Exit codes: 0 = all files valid; 1 = validation failures (each printed as
`file: event: rule: message`); 2 = tool/shape error (e.g. unknown
schema_version).

## 2. Eligibility-matrix fixtures (task doc item 2)

`tests/content/eligibility_matrix.json` is a list of executable cases.
Each case names the actor and target profiles inline (no engine state),
the policy context, and the EXPECTED outcome classes:

```json
{
  "id": "matrix/adult-minor-actor18-target17",
  "kind": "hard_validity",
  "actor":  {"age": 18, "gender": "female"},
  "target": {"age": 17, "gender": "male", "relation": "unrelated"},
  "policy": {"law": "neutral", "tenets": "neutral"},
  "expect": {"adult_gate": "blocked", "attemptable": false,
             "refusal_possible": false,
             "reason_codes": ["not_adult"]}
}
```

Field semantics:
- `interaction_kind`: B's PROPOSED set (`flirt`, `date`,
  `propose_courtship`, `propose_union`, `propose_plural_union`,
  `convert_request`, `repair_attempt`), treated as proposal until C
  confirms and Claude freezes (B, 2026-11-17). Absent ⇒ the runner only
  self-asserts the fixed rules.
- `kind`: `hard_validity` (the fixed 18+ gate and graph errors) or
  `soft_score` (acceptance scoring: refusal must remain possible even at
  max attraction/openness; penalties must not become hidden eligibility).
- `relation`: `unrelated | biological_sibling | half_sibling | step |
  adoptive | in_law | parent_child | grandparent_grandchild` (social-only
  links carry no genetic component; the matrix only asserts attemptability
  classes, not the health coefficient — that's task item 5).
- `policy.law`: `neutral | permissive | prohibited_unrecognized` —
  prohibited/unrecognized unions stay ATTEMPTABLE for adults (consequence
  classes, not hard bans). Minors always fail regardless.
- `expect`: `adult_gate` ∈ {`both_pass`, `blocked`}; `attemptable` bool;
  `refusal_possible` bool (true for every adult case — max scores cannot
  manufacture consent); `reason_codes` ⊆ B's published
  `reason_code_vocabulary` in `policy-fixtures.json` (soft codes are a
  subset assertion on the result's `reasons[]`; hard codes assert
  `hard.code`); `ui`: UI-visible projection classes per B's request
  (2026-11-17) — `projected_consequences_present` (the offer screen's
  cost preview must list bound, deduped `projected_consequences`),
  `amount_range_ordered` (every `amount_range` is `[int, int]` with
  `0 <= lo <= hi`), and `reasons_include` (codes the player-facing
  disabled-reason list must contain, after C's observer filtering).

The runner (`tools/run_eligibility_matrix.py`) loads B's evaluator and A's
chronology guard via the hook table B publishes in
`policy-evaluator.md`/`policy-fixtures.json`; while those are drafts it
validates fixture WELL-FORMEDNESS and prints `SKIP: contracts not frozen`
per case with exit 0 — it never fakes a pass. After the freeze the same
fixture file runs unmodified against the real evaluator.

Coverage (task doc item 2): ages 17/18/19 × both participant orderings
(actor-minor and target-minor); same/opposite-gender adults;
biological/step/adoptive/in-law relations; hostile vs permissive
law/tenets; actor/target with prior partners vs without;
attraction/openness at both extremes (refusal must stay possible);
self-target and unknown-id graph errors; a `min_age: 16` romantic CONTENT
case that must fail R2 in the validator (cross-check between the two
tools).

## 4. Fit-proof bundle mode (2026-11-17)

`python tools/validate_v3_content.py --bundle
docs/collab/M2.5/contracts/v3-fit-proof.json` runs C's bundle: every
`valid_events` record must pass this validator; every `invalid_events`
record with an `event` must FAIL, with the stated `expected.error`
covered by at least one violation; records whose expectation is
resolve-domain (safe cancellation) or knowledge-layer semantics SKIP —
they belong to C's loader/knowledge modules, and a SKIP is never a pass.
`v2_compatibility_fixtures` pending records get structural checks
(dialect, options, ids) here; their resolve semantics are loader-owned.

Current bundle result: 11 passed / 6 failed / 6 skipped, all 6 failures
inside C's `valid_events/wedding_occurrence` (no free exit; `reveal_fact`
references undeclared roles `the_union_fact`/`witness_public`; template
references undeclared role `venue`) — reported to C (t15) 2026-11-17.

## 3. What D needs from A/B/C before freeze

- C: v3 schema file(s) in this directory (or a pointer) so R3–R5
  allowlists are copied from the frozen text, not from 02-contracts prose.
- B: `policy-fixtures.json` evaluator entry point + reason-code vocabulary.
- A: the chronology-guard entry point used by the eligibility runner.
