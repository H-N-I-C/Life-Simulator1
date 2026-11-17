# Family graph, unions, parentage — A contract (PROPOSAL, not frozen)

Owner: Kimi A. Namespace `src/game/features/family_graph/**`,
`web/static/features/family_graph/**`, `tests/test_family_graph*.py`.
This file fixes the shared shapes B and C bind to; executable examples in
`graph-examples.json`. Everything here is a proposal until Claude freezes.

## 0. Fixed rule and the centralized guard

Both participants in any romantic/sexual/marriage interaction must be
naturally 18+ (protected chronology, advanced only by annual turns). The
guard `graph_assert_adult_romantic(a_id, b_id)` lives in family_graph and is
called by every graph transition, offer resolution, v2/v3 adapter and the
editor path. It reads simulation chronology, never an editable display age,
and cannot be overridden by policy, tenets, laws, content, scores or edits.
`is_adult := age >= 18`. Consent is occurrence-specific and never created by
setting relationship/satisfaction to 100.

## 1. Identity and IDs

- `person_id` (immutable, existing M2 canonical id) identifies people.
- `relationship_id`, `union_id`, `household_id` are UUIDs minted by
  family_graph only. B/C reference them read-only, never generate them.
- Names are display data; rename never changes identity.

## 2. Save-envelope extension (A owns the envelope)

New top-level envelope field (additive; old saves load unchanged):

```json
"graph": {
  "schema_version": 1,
  "relationships": [], "unions": [], "parentage": [], "households": [],
  "offers": [],
  "primary_relationship_id": null,
  "conception_outcomes": {}
}
```

Plus one shared field requested by B and confirmed here:
`editor_audit: [EditorAuditEntry]` (see B's editor-protocol.json; A appends
entries on behalf of every applied editor command). Entry shape (frozen
proposal): `{command: str, scope: str, target_id: str, before: map,
after: map, turn: int}`.

`policy_revision` (int, owned/bumped by B's policy domain) is read by A and
stamped onto every offer at creation and onto unions at creation. A
implements B's requested hook `on_policy_revision_bump(game_id,
supersede_filter)` with B's `supersede_filter` shape
(`interaction_kinds` / `affected_person_ids` / `affected_jurisdiction`)
from editor-protocol.json: matching pending offers become `superseded`,
attempts/cooldowns/pre-effects preserved, safe continuation resumed, no
resampling.

## 3. Edges

### 3.1 Relationship edge

```json
{
  "relationship_id": "rel-…",
  "person_ids": ["pid-a", "pid-b"],
  "kind": "courtship | lover | committed | consort",
  "satisfaction": 0, "trust": 0, "chemistry": 0,
  "exclusivity": {"agreement": "exclusive | open | negotiated"},
  "visibility": "public | secret",
  "start_turn": 0, "end_turn": null,
  "status": "active | ended"
}
```

### 3.2 Union edge (pairwise; plural allowed via multiple edges)

```json
{
  "union_id": "uni-…",
  "person_ids": ["pid-a", "pid-b"],
  "kind": "marriage | civil_partnership | handfasting | consort_union",
  "status": "active | divorced | dissolved | widowed",
  "recognition": {
    "status": "recognized | civil_only | unrecognized | forbidden",
    "jurisdiction_id": "jur-aurora-bay",
    "law_version": 3,
    "basis": "ceremony | registration | common_law"
  },
  "ceremony": {"held": false, "turn": null, "venue_id": null},
  "prenup": null,
  "start_turn": 0, "end_turn": null
}
```

`recognition` sub-shape: B proposed it (policy-catalogs.json →
`union_recognition_shape`), A owns the edge and **accepts it as written**;
`forbidden` unions stay in the graph (attemptable, consequences via B/C) —
recognition is a label, not a gate. `prenup` (if set):
`{"asset": "cash", "amount": 0, "terms_id": "pre-standard"}` — cash-only,
property stays out of M2.5 (§7 of 02-contracts).

Pairwise with multiple memberships, per 02-contracts §2: spouses A–B and
A–C never imply B–C. Freeze is for **pairwise** edges (group unions excluded
in M2.5).

### 3.3 Parentage edge

```json
{"child_id": "pid-c", "parent_id": "pid-p",
 "role": "biological | adoptive | step | guardian | social",
 "source": "birth | adoption | marriage | court | recorded",
 "confidence": "certain | assumed"}
```

One edge per (child, parent, role). Biological ancestry = edges with
role `biological`; it must be a **DAG** — a cycle or duplicate edge is a
graph error. Adoption/step/guardian add **no** genetic risk.

### 3.4 Household / care edge

```json
{
  "household_id": "hh-…",
  "resident_ids": ["pid-a", "pid-b"],
  "dependent_ids": ["pid-c"],
  "guardians": {"pid-c": ["pid-a", "pid-b"]},
  "support_obligations": [
    {"from_id": "pid-a", "to_id": "pid-c", "kind": "child_support",
     "amount": 0, "union_id": null, "until_turn": null}
  ]
}
```

Aging/shared child effects/support post **once per child** (dedupe across
spouses/unions is A's job in the yearly order).

## 4. Offers (A owns the envelope; C owns sampled outcomes)

```json
{
  "offer_id": "off-…",
  "initiator_id": "pid-a", "target_id": "pid-b",
  "interaction_kind": "flirt | date | propose_courtship | propose_union |
      propose_plural_union | convert_request | repair_attempt",
  "romantic": true,
  "relationship_id": null, "union_id": null,
  "creation_turn": 0, "expiry_turn": 0,
  "policy_revision": 0,
  "bindings": {},
  "sampled_outcomes": {},
  "status": "pending | accepted | declined | expired | superseded",
  "attempt": {"count": 0, "cooldown_until_turn": 0}
}
```

- Canonical `interaction_kind` enum: the 7 kinds above from B's evaluator
  §1, all `romantic: true`. C's nonromantic parenting kinds ride the same
  offer envelope with `"romantic": false` (e.g. `"interaction_kind":
  "parenting_request"`) and skip the adult-romantic guard (ordinary
  family/social interaction). **B and C to confirm this enum.**
- `romantic: true` offers: both participants 18+ at creation AND at
  resolution (`graph_assert_adult_romantic`), always.
- Self-targets, duplicate members, dangling ids → typed graph errors.
- Editor cancellation of a relevant pending offer sets status
  `superseded`, keeps `attempt` usage and any applied pre-effects, resumes
  the stored safe continuation (B's editor-protocol §pending).

## 5. Read-only queries (for B's evaluator/editor and C's v3 conditions)

All return plain JSON, never mutate, and are the ONLY way B/C read graph
state (they don't reconstruct the graph):

| Query | Returns |
|---|---|
| `graph_person(person_id)` | `{person_id, age, is_adult, living, name}` |
| `graph_relatedness(a_id, b_id)` | `{category, biological_paths, coefficient_band}` — category ∈ `self, parent_child, grandparent_grandchild, sibling, aunt_uncle_niece_nephew, first_cousin, kin_other, unrelated, unknown`; coefficient_band ∈ `0, <0.015, 0.015–0.0625, 0.125, 0.25, unknown` (fictional bands, calibration frozen with Claude) |
| `graph_edges(person_id)` | relationships/unions/parentage/household edges touching the person |
| `graph_relationship(rel_id)` | full edge incl. `chemistry`, `trust`, `satisfaction` (B's attraction/history contributions) |
| `graph_union_memberships(person_id)` | union edges with status, recognition, kind |
| `graph_household(person_id)` | household + obligations |
| `graph_tree(focus_id, max_generations=3)` | tree projection (§6) |

`graph_relatedness` uses biological ancestry only; unknown ancestry reports
`unknown` — concretely, when either person's record (as passed in
`persons_by_id`) is marked `lineage: "unknown"`, or when a biological path
cannot be ruled out because a reachable ancestor is unrecorded. Name/
religion/orientation never enter the coefficient.

## 6. Family-tree UI projection (A owns the tree UI)

`graph_tree` returns a bounded, cycle-safe projection:

```json
{
  "focus_id": "pid-a", "max_generations": 3,
  "nodes": [{"person_id": "…", "name": "…", "age": 0, "living": true,
             "portrait_ref": null,
             "flags": ["deceased | ex | step | adoptive | unknown_parent"]}],
  "edges": [{"from_id": "…", "to_id": "…",
             "kind": "parentage | union | guardianship",
             "flags": ["biological | adoptive | step | dissolved | widowed |
                        secret"]}]
}
```

`secret` edge flag: present when the underlying relationship/union edge has
`visibility: "secret"` AND the VIEWING player legitimately knows it (C's
observer-knowledge rules). The tree projection is player-scoped, so the flag
is safe to render (e.g. dashed); it never leaks NPC-only knowledge.
Portraits: `portrait_ref` stays null — the UI resolves portraits from the
person's appearance through the existing M0b renderer; no new asset
pipeline.
Rendering loops (remarriage in-law rings) are safe: nodes/edges carry ids
and the UI walks with a visited set. Deceased/ex people stay in the tree
with flags.

## 6.1 Enum label keys (i18n)

Every graph enum value carries a stable i18n label key; owners render from
the key, never hard-code English:

| Enum | Values → label_key |
|---|---|
| relationship kind | `graph.rel_kind.courtship / .lover / .committed / .consort` |
| union kind | `graph.union_kind.marriage / .civil_partnership / .handfasting / .consort_union` |
| recognition status | `graph.recognition.recognized / .civil_only / .unrecognized / .forbidden` |
| union status | `graph.union_status.active / .divorced / .dissolved / .widowed` |
| relatedness category | `graph.kin.self / .parent_child / .grandparent_grandchild / .sibling / .aunt_uncle_niece_nephew / .first_cousin / .kin_other / .unrelated / .unknown` |
| parentage role | `graph.parent_role.biological / .adoptive / .step / .guardian / .social` |

B (and any UI) resolves these keys through the existing translation
machinery; A owns the key namespace and supplies default en strings in
Phase 2.

## 7. Typed graph transitions (authoritative; for C's v3 effects + editor)

Each validates inputs + invariants (incl. the adult guard where romantic),
mutates exactly the graph (+ journal), returns actual-delta results:

| Transition | Notes |
|---|---|
| `graph_start_relationship(a, b, kind, sampled {satisfaction, trust, chemistry}) → relationship_id` | adult guard; dedupes active edge between the pair |
| `graph_update_relationship(rel_id, patch)` | bounded int fields, exclusivity, visibility, `end` sets `status/end_turn` |
| `graph_form_union(a, b, kind, recognition, prenupt?, ceremony?) → union_id` | adult guard; stamps `policy_revision` |
| `graph_end_union(union_id, how: divorce \| dissolve \| widow)` | targets ONE selected union; never deletes children or other unions |
| `graph_add_parentage(child, parent, role, source, confidence)` | DAG check; chronology check |
| `graph_household_move(person_id, household_id)` / `graph_set_guardian(child, [guards])` / `graph_add_support(...)` | household ops |
| `graph_sample_conception(bio_mother, bio_father) → {coefficient_band, adjustment, roll}` | called ONCE per conception by C's content; result persisted in `conception_outcomes[child_id]`; no draw for adoption |
| `graph_record_offer(offer) / graph_resolve_offer(offer_id, option_id, resolver)` | membership/chronology revalidated at resolve |

Return shape (all): `{ok: true, ids: {...}, journal: [entries], ledger_requests: []}`
— ledger money goes through the frozen M2 ledger request, both legs once.

## 8. v2 compatibility and migration (A owns)

- `player.partner` + `marital_status` become a **derived projection** of the
  graph (first active union's partner / its status); no legacy helper may
  overwrite plural state.
- Migration `graph_migrate_v2(env)`: exactly one union edge from the old
  single partner; biological parentage edges from recorded children +
  pregnancy parentage (persisted conception ids, never inferred from
  current partner); archive/ex partners become `ended` edges; pending v2
  partner choices bind the intended legacy person/edge at offer creation;
  a resumed v2 choice can never target a newly selected spouse — unmappable
  stale context → safe explicit cancellation. Fixture: `graph-examples.json`
  → `migration_v2_example`.

## 9. What A confirms to B

- `union.recognition` sub-shape: **accepted** — both sides now carry the same
  values, `status: recognized | civil_only | unrecognized | forbidden`,
  `basis: ceremony | registration | common_law` (B's published
  policy-catalogs.json adopts these; B's earlier hc-message wording
  `void` / `civil|religious|customary|private` is superseded by the files
  unless B counters — `forbidden` covers the illegal-but-attemptable case).
- `editor_audit` envelope field: **accepted** with B's exact entry shape
  `{command, scope, target_id, before, after, turn}`; A appends entries.
- `policy_revision` stamping on offers/unions: **accepted**; A implements
  `on_policy_revision_bump(game_id, supersede_filter)` per B's
  editor-protocol.json.
- Queries B listed (age/chronology, relatedness category, chemistry/trust,
  union memberships): provided as §5. `graph_relatedness` category is what
  B's kinship contribution consumes; social/adoptive/in-law links report
  `unrelated` (no risk contribution).
- **Ledger category for legal fines**: frozen M2 `CATEGORIES` (src/game/
  finance.py:19) has NO `legal` — A confirms adding `"legal"` to
  `CATEGORIES` as part of M2.5 Phase 2 (A owns finance.py); until then B's
  content validates against `other`. (`wedding` already exists for
  ceremony costs.)

## 10. What A asks B and C to confirm

- **B**: interaction_kind enum (§4) covers the evaluator's needs;
  recognition `basis` values; anything else B needs from `graph_person`
  (e.g. `public_tenet_ids` come from B's side — A only stores the foreign
  keys B persists on persons).
- **C**: offer envelope + enum (§4) works for v3 offer/respond/end effects;
  `bindings` is C's v3 binding block (A treats it as opaque versioned data);
  v2→v3 pending adapter expectations per §8; tree projection flags cover
  dating/drama UI needs (exes with flags).
- **D**: `graph-examples.json` records are executable input→expected-output
  fixtures for cross-contract validation.
