# M2.5 A — Drama / secrets / reputation report (Kimi A, 2026-11-17;
# C's step 3 transferred per 00-TEAM rebalance #3). Implements C's
# FROZEN contracts/reputation-knowledge.md; C reviews against contract.

## What landed — `src/game/features/knowledge/**`

- **`core.py`** — the facts-vs-knowledge separation (frozen §1/§2):
  immutable facts (`kind` ∈ affair/secret_union/discovery/disclosure/
  forgiveness/breakup/conversion; `ended_turn` only via typed
  transitions), knowledge records (who learned what, when, from where,
  confidence; deduped per (observer, fact), max confidence, earliest
  turn; reserved observer id `"player"`), and the scoped reputation
  store (person × scope → value, bounded [−100, 100], deltas deduped
  per (fact, observer, jurisdiction, occurrence) — re-witnessing never
  re-fines). `knows()`/`known_facts()` are the ONLY way an NPC can
  react to a secret. `journal_entry_visible(env, entry)` is the
  serialization predicate for §2's server-side guarantee.
- **`drama.py`** — expectations & affairs (frozen §6): `detect_affair`
  mints an `affair` fact ONLY when an `exclusive` agreement of either
  participant is breached with someone else; open/negotiated
  agreements and the unpartnered are never affairs — consensual plural
  unions are not cheating. Typed transitions: `reveal_fact` (mint +
  witness records + deduped reputation), `disclose_to` (told-records =
  the secret edge's authorized-knowledge set), `forgive` (sets
  ended_turn, bounded trust repair +15 through A's relationship patch,
  knowledge records retained, mints a forgiveness history fact, never
  erases), `end_fact`. `filter_reasons` — display-only reason filter:
  reputation reasons require a player-known `fact_id`; private
  tenet/faith reasons require public/disclosed-known visibility;
  scoring untouched (Claude decision 4: words-only in player UI).
- **`effects.py`** — v3 domain appliers registered into C's resolve
  registry: `reveal_fact {kind, about, to, reputation}` and
  `reputation {person, scope, delta}`.
- **Envelope** (A-owned): `env["knowledge"]` backfilled in
  `_read_save`; old saves load empty.
- **Server-side filter** (A owns the endpoint code, frozen §2/Claude
  decision 3): `service._state` now filters journal entries through
  `journal_entry_visible` — `private` occurrences never serialize, and
  `player_known` entries serialize only once a player knowledge record
  exists (editor inspection never creates one). Verified over the real
  get_state API: a private entry is absent, a player_known entry
  appears only after the player learns the fact.

## Tests — tests/test_knowledge.py (10, OK ×3 hash seeds)

All four `knowledge_fixtures` from v3-fit-proof.json executable:
uninformed NPC cannot react; witness record created exactly once (max
confidence kept); reason visibility filtering (reputation dropped,
public faith kept, scoring unchanged; reputation shown once the player
knows the fact); forgiveness repairs not erases (ended_turn set, trust
20 → 35 via the bounded patch, knowledge retained, second forgive
no-op). Plus: exclusive breach IS an affair; open/no-agreement never
is; reputation dedupe per occurrence key + [−100,100] clamp;
disclose adds `told` knowledge; server-side serialization filter over
real `get_state`. Full regression: 156 tests OK ×3 seeds; web M1/M2
OK.

## Notes / gaps for C's review

- Journal entries carry `audience`/`fact_id` tags when the producer
  supplies them; C's resolver doesn't tag outcome events yet (same gap
  family as outcome_text role refs) — the filter machinery and the
  convention are in place.
- M2.5a keeps one current reputation value per scope; reactions are
  gated on `known_fact` so uninformed NPCs never act on secrets
  (frozen §5 note in core.docstring).
- v3 `known_fact` / `reputation_at_least` condition evaluation reads
  `knows()`/`reputation_as_known` — exposed here for C's loader.
