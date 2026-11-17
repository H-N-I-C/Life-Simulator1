# M2.5 policy evaluator — proposal (Kimi B, 2026-11-17)

Awaiting Claude freeze together with the other M2.5 contracts.

The evaluator scores **acceptance** for a romantic/social offer. It has
exactly two jobs: (1) hard validity, (2) a bounded, explainable soft
score. It never bans an otherwise-legal adult consensual attempt and it
never manufactures consent.

## 1. Inputs

```jsonc
{
  "interaction_kind": "flirt | date | propose_courtship | propose_union | propose_plural_union | convert_request | repair_attempt",   // set to confirm with C
  "initiator_id": "person_id",          // usually the player
  "target_id": "person_id",
  "initiator": { "profile": "attraction_profile (policy-catalogs.json)", "person_ref": "person_id" },
  "target":    { "profile": "attraction_profile", "person_ref": "person_id" },
  "context": {
    "relationship": "relationship edge or null (A, read-only: chemistry, trust, kind, expectations)",
    "existing_unions_initiator": ["union_id"],
    "existing_unions_target": ["union_id"],
    "kinship_category": "none | biological_close | biological_distant | social_only | unknown (A's read-only query; social/adoptive/in-law links alone are ALWAYS 'none' for risk)",
    "age_gap_years": "int (protected chronology, A)",
    "jurisdiction": "resolved jurisdiction preset/override for the SAVE's country",
    "reputation": "scoped reputation of initiator as known to target (C, read-only)"
  }
}
```

## 2. Hard validity (never overridable)

Checked BEFORE scoring; any failure returns `{hard: {ok: false, code}}`
with NO score. These mirror A's centralized guard — the evaluator
rejects defensively but A's guard is authoritative.

| code | condition |
|------|-----------|
| `not_adult` | either participant naturally < 18 at offer AND resolution time (protected chronology; never editable) |
| `dead` | either participant not alive |
| `self_target` | initiator_id == target_id |
| `unknown_person` | dangling/unknown person_id |
| `duplicate_member` | same person already bound to this offer |
| `graph_error` | dangling relationship/union references |

`not_adult` cannot be waived by score, tenets, laws, editor, content or
legacy adapters (spec fixed rule).

## 3. Contributions (bounded, named)

Score = base 50 + sum of contributions, clamped to [0, 100]. Nothing
outside this table may move the score. Value mappings are exact so the
fixtures in `policy-fixtures.json` are checkable by hand.

| contribution | exact mapping | bound |
|--------------|---------------|-------|
| `attraction` | `clamp(round((attraction_to_initiator − 0.5) × 30), −15, +15)` | [-15, +15] |
| `chemistry` / `trust` | `clamp(round((value − 50) / 5), −10, +10)` from A's relationship edge | [-10, +10] |
| `personal_tenets` | per relevant tenet (see below): favoured +8, neutral 0, disfavoured −8; sum clamped | [-10, +10] |
| `faith_tenets` | per relevant tenet from the target faith's PUBLIC profile: favoured +6 / disfavoured −6; if the target has a personal override on that tenet, halve (±3); sum clamped | [-10, +10] |
| `culture` | per relevant social stance: favoured +4, neutral 0, disfavoured −4; sum clamped | [-8, +8] |
| `law_risk` | projected recognition (A's enum): unrecognized −12, forbidden −15, civil_only −4, recognized 0 | [-15, 0] |
| `reputation` | `clamp(round(scoped_reputation / 10), −10, +10)`, value ∈ [−100, +100] as known to the target (C) | [-10, +10] |
| `class` | 0 placeholder until M3 careers freeze | [-5, +5] |
| `adult_age_gap` | gap 0–9: 0, 10–19: −5, 20–29: −8, 30+: −10 | [-10, 0] |
| `existing_commitments` | −round(8 × target monogamy_preference) if the target already has ≥1 union, else 0 | [-8, 0] |
| `kinship` | social-acceptance modifier from `graph_relatedness(a, b).category` (A's read-only query): unrelated 0, social-only (step/adoptive/in-law) 0, kin_other −4, unknown −2, first_cousin −6, aunt_uncle_niece_nephew −8, parent_child / grandparent_grandchild / sibling −10 | [-10, 0] |
| `convert_request` | (convert_request only) target faith's public `conversion_out` stance: disfavoured −6, halved (−3) with a personal override | [-10, 0] |

Relevant tenets per interaction (faith/personal/culture contributions
consult ONLY these):

- `propose_union`: `interfaith_unions` (if faiths differ),
  `same_gender_unions` (if the participants share a gender),
  `large_age_gap` (if gap ≥ 10)
- `propose_plural_union`: the above plus `plural_unions`
- `flirt`, `date`, `propose_courtship`, `repair_attempt`: none (0)
- `convert_request`: handled by `convert_request` above

`kinship` is an acceptance modifier ONLY: relatives remain fully
attemptable per the spec fixed rule (modifiers, not bans). The biological
child-health coefficient is A's separate `graph_sample_conception` model
and never enters this score. Name/religion/orientation do not enter
either side.

Relevant culture stances mirror the same interaction key
(`plural_unions_social_stance`, `interfaith_social_stance`,
`divorce_social_stance`).

Refusal stays possible at ANY score (spec): after acceptance sampling,
the target independently rolls refusal with
`p_refuse = clamp(0.05 + 0.4 * (1 - openness) + 0.2 * jealousy, 0.05, 0.6)`
drawn from the passed-in seeded RNG, stored once per offer (C owns offer
sampling; this formula is the default B provides, freeze may adjust).

## 4. Reason disclosure

Every score returns `reasons: [{code, contribution}]` for the
contributions with |contribution| >= 3. **C filters reason codes by what
the observer legitimately knows** (a secret reputation fact the player
cannot know produces no reason text). Reasons are bounded and
user-facing ("She values her faith's teaching on plural unions"), never
raw internals.

## 5. Result shape (consumed by C's offers and A's transitions)

```jsonc
{
  "hard": {"ok": true, "code": null},
  "score": 0,                    // int 0..100, present only if hard.ok
  "reasons": [{"code": "law_risk", "contribution": -15, "label_key": "reason.law_risk"}],
  "refusal": {"possible": true, "p_refuse": 0.23},
  "projected_recognition": {     // for union proposals only (A's shape)
    "status": "recognized | civil_only | unrecognized | forbidden",
    "jurisdiction_id": "jur_riverlands",
    "law_version": 0,            // int, per-jurisdiction save-local revision
    "basis": "ceremony | registration | common_law"
  },
  "projected_consequences": [    // bound, deduped per occurrence key
    {"trigger": "ceremony_of_unrecognized_union", "effect": "civil_fine",
     "ledger_category": "legal", "amount_range": [500, 2000]}
  ],
  "policy_revision": 0           // stamp: which policy state scored this
}
```

`projected_*` are previews shown before the player commits (costs as
gate only, frozen M2 rule); the actual application happens once, through
the ledger, at resolution.

## 6. Read-only predicates B exposes to C's v3 conditions

Names = the freeze set agreed with C (event-schema-v3.md §2/§8):
`tenet_stance(person_id, tenet_id) -> favoured|neutral|disfavoured`,
`faith_id(person_id) -> faith_id`, `jurisdiction_id(save) -> jurisdiction_id`,
`recognition_status(union_id) -> recognition.status` (A's edge; projected
status for proposals comes from the evaluator result),
`attraction_band(person_id) -> low|mid|high`,
`openness_band(person_id)`, `monogamy_band(person_id)`.
All read-only, all return typed errors on unknown ids.

## 7. Coordination questions

- **A**: confirm union recognition sub-shape and `editor_audit` save
  field; confirm exact frozen M2 ledger category name for legal fines.
- **C**: confirm interaction_kind set and that C's offer sampling uses
  `p_refuse` from this result (or proposes a different split).
- Freeze: aggregation bounds and refusal formula above are the proposal.
