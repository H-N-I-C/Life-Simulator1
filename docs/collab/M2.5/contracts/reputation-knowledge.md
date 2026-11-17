# Reputation, secrets and observer knowledge — proposal (Kimi C, 2026-11-17)

PROPOSAL, not frozen. C owns the knowledge/reputation domain for M2.5
(dating drama, secrecy, discovery, forgiveness) plus v3 audience
filtering. Executable fixtures are embedded in `v3-fit-proof.json`
(`knowledge_fixtures` block). A owns the graph/save envelope (C stores
these records through A's envelope extension, requested below); B emits
reason codes whose VISIBILITY C decides.

## 1. Core separation: facts vs knowledge

- **Facts** are engine-true occurrences (an affair, a secret marriage, a
  discovery). They exist regardless of who knows them.
- **Knowledge records** say WHO learned WHAT, WHEN, from WHERE, and how
  confidently. Global facts are never NPC knowledge by themselves.

```jsonc
// Fact (created by occurrences; immutable once created)
{"fact_id": "fact-…",            // minted by C's domain, referenced read-only
 "kind": "affair | secret_union | discovery | disclosure | forgiveness |
          breakup | conversion",
 "subject_ids": ["pid-a", "pid-b"],
 "union_id": null, "relationship_id": "rel-…",
 "occurrence_id": "occ-…",       // the v3 occurrence that created it
 "created_turn": 12,
 "ended_turn": null}             // set by forgiveness/end transitions

// Knowledge record
{"observer_id": "pid-w",         // person_id; the PLAYER observer is the
                                 // reserved id "player"
 "fact_id": "fact-…",
 "learned_turn": 14,
 "source": "witnessed | told | discovered | rumor",
 "confidence": 1.0}              // 0..1; rumor may be < 1 (optional depth)
```

Save-envelope field (C requests, A owns the envelope — additive,
old saves load as empty):

```jsonc
"knowledge": {"schema_version": 1, "facts": [], "records": []}
```

## 2. Authorized visibility and secrets

- A relationship/union edge with `visibility: "secret"` (A's shape) has
  an authorized-knowledge set: the participants plus anyone a
  `disclosure` occurrence added. C maintains the set; A stores the edge.
- Events project per audience (v3 `audience.scope`): `public`,
  `player_known` (the player knows iff a knowledge record with
  observer_id "player" exists — editor inspection does NOT create one),
  `private`.
- **Server-side guarantee:** the game-state API and journal/notification
  projections apply this filter where entries are SERIALIZED (C's
  knowledge layer owns the predicate; A owns the endpoint code) — a
  `private` occurrence simply never appears in payloads sent to the
  client, so the UI cannot leak it even by bug. Editor inspection uses a
  separate out-of-character view with the same filtering rule for NPC
  knowledge.
- NPC reaction content conditions use `known_fact` /
  `reputation_at_least` (v3 §2) so uninformed NPCs never react to a
  stored secret.

## 3. Reason-code visibility (consumed by B's evaluator)

B returns `reasons: [{code, contribution, label_key}]`. Before display,
C's filter removes codes whose justification the player cannot
legitimately know:

- `reputation` reasons are displayable only if the underlying scoped
  fact is known to the player (observer "player").
- `tenet_stance` / `faith` reasons are displayable when the stance is
  public (faith public profile or a known disclosure); a private
  personal override the player hasn't learned is filtered out.
- Filtered codes are dropped from the UI text, NOT from scoring.
- **Display rule (Claude decision 4, 2026-11-17):** reasons surface ONCE
  at OFFER level as bounded phrases (`label_key` text only) — numeric
  scores and raw contributions are NEVER sent to player-facing UI; the
  API may carry contributions for tests/tools, but the client display
  path is words-only.

Filter API (proposal): `knowledge_filter_reasons(player_id, reasons) ->
reasons`. Frozen with the rest; B calls it at offer-creation text time.

## 4. Discovery / reveal occurrences

A discovery is a persisted v3 occurrence: bound witnesses/evidence
(`witness` role, `evidence: "witnessed"`), sampled outcomes stored once
(standard v3 policy). On creation it:

1. appends knowledge records for each bound witness (source `witnessed`),
2. may apply `reputation` deltas scoped per jurisdiction,
3. **dedupes**: reputation/legal effects post once per
   (fact_id, observer_id, jurisdiction_id, occurrence_id) — re-witnessing
   the same fact never re-fines or re-punishes.

Typed transitions (C domain API, called by v3 effects and editor-safe):
`reveal_fact(...)`, `disclose_to(...)`, `forgive(fact_id, by_id)`
(repairs trust via A's relationship patch; sets `ended_turn`; never
erases history), `end_fact(...)`. Secrets of state never mutate past
turns.

## 5. Scoped reputation

- Stores: `(person_id, scope_id, value)` where scope_id is a
  jurisdiction id (B's) or `"global"`. Value ∈ [−100, 100].
- Reputation the player "has" is always mediated by knowledge: an NPC's
  reaction reads reputation-as-known-to-that-NPC; the UI shows
  reputation-as-known-to-player. Editor view shows true values
  out-of-character and writes no knowledge records.
- Deltas from occurrences post once per occurrence key (§4) through the
  ledger where they carry money (fines) — one posting, labeled category.

## 6. Fixed-rule note

Nothing here creates consent, lowers 18+, or manufactures knowledge for
NPCs. Forgiveness repairs trust; it does not rewrite a fact. Discovery
of a consensual plural union the participants agreed to keep private is
possible content; the "affair" fact kind is reserved for expectation
violations (`exclusive` agreement breached), per 02-contracts §4.
