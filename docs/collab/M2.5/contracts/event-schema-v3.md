# Event schema v3 — proposal (Kimi C, 2026-11-17)

PROPOSAL, not frozen. Extends frozen v2.1 (docs/collab/M2/event-schema.json);
v2.1 files keep loading unchanged. v3 is a distinct schema version, not a
relabelled v2 file. Executable fit-proof records live in
`v3-fit-proof.json`. C owns schema/loader/content; A checks
graph/coordinator/ledger fit; Claude freezes.

## 0. Versioning and file shape

- Event files declare `"schema_version": 3` (v2 files declare 2; the
  loader dispatches per file, never guesses).
- Same file layout as v2.1: `{"schema_version": 3, "events": [EventDef3]}`.
- Saved pending records carry `"schema_dialect": "v2" | "v3"`; a saved
  outcome resolves exactly once in its own dialect and is never
  reinterpreted into the other.
- Content directory: `src/game/content/events/v3/**` (C-assigned), kept
  separate from v2 content files so D's validators can glob per version.
- `policy_revision` (B-owned int on the save) is stamped onto every v3
  pending at creation; a pending resolved under a different
  policy_revision still resolves from its STORED samples (no rescore),
  exactly like v2's pre-sampled policy.

## 1. Bindings (the v2 "roles" problem, solved explicitly)

v2 roles (`mother`, `father`, `partner`, …) become explicit **bindings**:
each event declares roles; the engine binds every role ONCE, at offer
creation, to concrete `person_id`s / edge ids; bindings are stored in the
pending and never rebound at resolve.

```jsonc
"roles": {
  "actor":    {"kind": "person", "select": "player"},
  "target":   {"kind": "person", "select": "predicate", "predicate": "…"},
  "the_union": {"kind": "union",  "select": "predicate", "predicate": "…"},
  "witness":  {"kind": "person", "select": "predicate", "predicate": "…",
               "optional": true}
}
```

- `kind`: `person | relationship | union | household` — the graph object
  type (A's ids; C references, never mints).
- `select`: `player | predicate` (a bounded allowlist predicate over the
  read-only graph/policy/knowledge queries in §5 — no eval, no paths).
- Each role bound exactly once per occurrence; duplicate role names fail
  schema validation. Optional roles may bind `null` (conditions can test
  `bound:witness`).
- Selection predicates run against SNAPSHOT state at offer creation
  (A's turn snapshot); stale/dangling bindings at resolve are a typed
  safe-cancellation (§7), never a silent re-pick. **Never implicitly
  first-spouse.**
- Biological/social qualifiers: person predicates can demand
  `relatedness_category` (A's enum) e.g. `parent_child`, `unrelated`;
  parenting events can demand `parentage_role: adoptive` etc.

## 2. Conditions (bounded vocabulary)

Everything v2.1 keeps (`min_age`, `max_age`, `requires` clauses incl.
`alive`, `has_illness`, `employed`, `in_school`, `min_stat`, `min_money`,
`flag`, `no_partner`, `partner_status`, `partner_relationship_below`,
`karma_gate`, `once_per_lifetime`, `chance_n`) PLUS:

```jsonc
{"all": [ … ]}, {"any": [ … ]}, {"not": { … }}
```

composing only the following new leaf conditions:

| condition | reads | meaning |
|---|---|---|
| `relationship_state` | A edge | `role_a`+`role_b` have active edge of `kind` in `kinds[]` with `chemistry >= v` / `trust >= v` / `satisfaction >= v` |
| `union_state` | A edge | `role_a`+`role_b` share union with `status` in `[]`, `kind` in `[]` |
| `expectation` | A edge | exclusivity agreement of the pair's relationship = `exclusive \| open \| negotiated`; affairs = violating `exclusive` |
| `recognition_status` | B + A union | the bound union's recognition.status in `[]` (or projected status for proposals) |
| `tenet_stance` | B | `person:role` personal/public stance on `tenet_id` is `favoured \| neutral \| disfavoured` |
| `faith_id` | B | `person:role` faith_id == value / in list |
| `jurisdiction_id` | B | save jurisdiction id == value |
| `attraction_band` | B | target's stored attraction to initiator in `[lo, hi]` (engine-read, read-only) |
| `openness_band` | B | `person:role` openness in `[lo, hi]` (read-only) |
| `monogamy_band` | B | `person:role` monogamy preference in `[lo, hi]` (read-only) |
| `known_fact` | C knowledge (§4 of 02-contracts; see `reputation-knowledge.md`) | `observer:role` knows `fact` with `confidence >= c` |
| `reputation_at_least` | C | scoped reputation of `person:role` as known to `observer:role` >= v |
| `kinship` | A | `graph_relatedness(a, b).category` in `[]` / `coefficient_band` in `[]` |
| `milestone` | C parenting | child bound at role has recorded milestone `id` |
| `bound` | engine | role X was bound non-null |

The `18+` rule is NOT a condition: every v3 event with
`interaction: romantic` (see §3) is tagged at validation and the engine
guard (`graph_assert_adult_romantic`) is enforced at offer AND resolve,
outside content. An event author cannot switch it off; a romantic event
with `min_age < 18` FAILS schema validation.

## 3. Interaction kinds and the adult guard

New optional event field:

```jsonc
"interaction": {"kind": "flirt | date | propose_courtship | propose_union |
                  propose_plural_union | convert_request | repair_attempt",
                 "romantic": true}   // or {"kind": "parenting_request",
                                     //  "romantic": false}
```

- `interaction.kind` MUST be one of the 7 evaluator kinds (B's enum, C
  confirms — see README cross-shapes) or the nonromantic
  `parenting_request`. Unknown kinds fail validation.
- `romantic: true` ⇒ engine adult-guard at offer + resolve; auto events
  cannot create consent or force unions (a refusal outcome is always a
  legal stored outcome).
- `romantic: false` (e.g. parenting/milestones, teen troubles) skips the
  romantic guard but keeps all normal typed validation. Children remain
  nonromantic dependents; NOTHING in v3 can attach a romantic
  interaction to a minor.

## 4. Typed domain effects (v2's flat effects + structural, extended)

v2 effects (`stats`, `money` as single posting, `relationship`,
structural keys) are unchanged for v3. The structural key list gains
typed domain commands that call authoritative APIs (A's transitions /
B's `v3_effect_targets` / C's knowledge), never raw setters:

```jsonc
"effects": {
  "offer":   {"interaction": "…", "to": "role", "as": "npc"}   // NPC-initiated offer; occurrence records attempt/cooldown
  "respond": {"offer_role": "role", "accept": true}
  "end_relationship": {"between": ["role_a", "role_b"], "how": "breakup | fade"}
  "end_union":        {"union_role": "role", "how": "divorce | dissolve | widow"}
  "reveal_fact":      {"kind": "affair | secret_union | conversion | …",
                       "about": ["role_a", "role_b"], "to": ["role_w"],
                       "evidence": "witnessed"}   // mints a fact bound to
                                                  // the ABOUT roles and adds
                                                  // knowledge records for TO
  "reputation":       {"person": "role", "scope": "jurisdiction_id|'global'", "delta": "DeltaValue"}
  "chemistry":        {"between": ["role_a","role_b"], "delta": "DeltaValue"}
  "convert_profile":  {"person": "role", "target_faith_id": "faith_x"}   // B's typed target
  "set_personal_tenet": {"person": "role", "tenet_id": "…", "stance": "favoured|neutral|disfavoured"}
  "reproject_recognition": {"jurisdiction": "jur_…"}
  "milestone":        {"child": "role", "id": "first_steps", "record": {...}}
  "sample_conception": {"mother": "role", "father": "role"}    // calls A once per conception; outcome persisted by A
}
```

- Money: unchanged v2.1 rule — `effects.money` is ONE posting,
  cost-as-gate-only, no cost+effect double charge; weddings/honeymoons/
  fines label `ledger_category` explicitly.
- Each effect validates its roles at resolve (bound, living, typed);
  invalid → typed error, no partial application (occurrence atomicity,
  same as v2).
- `end_union` names a UNION ROLE (selected union), never "current
  spouse". A resumed pending binds the stored union_id; if that union is
  gone → safe explicit cancellation (§7).

## 5. Audience / visibility projection

```jsonc
"audience": {"scope": "public | player_known | private",
             "participants": ["actor", "target", "witness"]}
```

- Event journal entries and API notifications carry the audience scope;
  a `private` or `player_known` event does not leak to NPCs who don't
  know the fact (C's knowledge layer decides `player_known` — editor
  inspection is out-of-character and never propagates knowledge).
- **Server-side guarantee (Claude decision 3, 2026-11-17):** journal and
  notification filtering to what the player knows (`player_known` /
  `public`) happens at serialization, before any payload leaves the
  server; the client never receives hidden facts (details and ownership
  split in `reputation-knowledge.md` §2).
- Secrets: an occurrence may declare `"secret_fact": "fact_ref"` — its
  visibility follows the fact's authorized-knowledge set (C's
  `reputation-knowledge.md`).

## 6. Offer projection and sampling policy

**Offer projection (Claude decision 1, 2026-11-17):** v3 offers project
into the FROZEN M2 pending_decision shape the UI modal renders —
`title`, `text`, `options` (label, `known_effects`, `enabled` + `reason`,
`inputs`, free exit present) — with v3 data (`bindings`, `audience`,
`policy_revision`, `schema_dialect`, `_effects`/`_structural` internals)
as ADDITIVE fields only, exactly as v2 records carry internals today.
No shape changes to the frozen projection.

**Forecast labels (Claude decision 2, 2026-11-17):** at offer creation,
the engine copies the evaluator's `projected_consequences` (e.g. civil
fine $500–2000 for an unrecognized union) into the proposing options'
`known_effects` as FORECAST entries — explicitly marked as forecasts,
not guarantees, same semantics as the finance estimate. Only the actual
acceptance occurrence posts the real amount, once, through the ledger.

**Sampling policy (unchanged from v2.1):** once-per-offer sampling,
stored private, resolve applies stored data only; refusal draws use the
passed-in seeded rng and are stored once per offer (B's `p_refuse`
formula is the default for offers scored by the evaluator). Every
manual choice keeps a legal free exit (soft-lock rule unchanged,
validated at load). Auto events keep v2.1 auto semantics.

## 7. v2 compatibility (adapter, owned by C loader; A migrates the graph)

- v2 content loads and runs exactly as today, including
  `gain_partner/lose_partner/divorce/partner_death`.
- A resumed v2 pending binds the INTENDED legacy person/edge captured at
  offer creation (migration maps the old partner once — A's
  `graph_migrate_v2`). It can never act on a newly selected spouse.
- Unmappable stale context (target archived with no mapping) → stored
  outcome resolves to a safe explicit cancellation event ("The moment
  has passed"), journaling the cancellation — no random target, no
  reroll, attempt/cooldown usage preserved.
- The v2 resolver path stays in the loader (no second interpreter): v3
  adds bindings/audience/interaction handling; v2 pendings take the
  legacy branch keyed by `schema_dialect`.

## 8. What C confirms to A and B

- **A**: the offer envelope (graph-contract §4) works for v3
  offer/respond effects; `bindings` and `sampled_outcomes` are C's
  opaque versioned blocks inside the offer; the 7-kind enum +
  `parenting_request` is the full v3 interaction set; tree flags
  (`ex`, `deceased`) cover drama UI needs. **Confirmed.**
- **B**: reason visibility filtering is C's (see
  `reputation-knowledge.md` §3): B emits reason codes, C's knowledge
  records decide which are displayable to the player; v3 policy-reading
  conditions use exactly the predicates in §2 (`tenet_stance`,
  `faith_id`, `jurisdiction_id`, `recognition_status`, `attraction_band`,
  `openness_band`, `monogamy_band`) against B's
  read-only APIs; B's `v3_effect_targets` are referenced by name only,
  no raw setters. **Confirmed.**
- Loader: one loader, two dialects; validation failures are load-time
  typed errors naming file+event+path (D's validator reuses the same
  validate function).
