> FROZEN v1 by Claude 2026-11-21, together with 02-contracts.md.
# §Social — A (engine): social circles/relationship kinds/memory/invites/
# popularity. PROPOSED v1, 2026-11-21. ONLY Kimi A writes this file;
# amendments via Claude. Shared conventions + B's UI section live in
# 02-contracts.md; C and D keep 02-contracts-C.md / 02-contracts-D.md.
# This section is written AGAINST B's published requests ("TO A, exact",
# 02-contracts.md) — where B pins a shape, this section adopts it.

Everything rides the canonical machinery: Person identity and the
family graph (A-owned, extended here with social nodes), the one ledger
poster, the v3 event pipeline (C-owned schema/content, A-owned resolve
appliers), the existing command/replay/undo boundary. No parallel
mini-engine, no feature HTTP, no phase reorder.

## Persons and circles (authoritative state)

Social people are REAL canonical `Person` objects (stable `person_id`,
M0b appearance via the existing integration seam), registered as
`social`-kind graph nodes and reached by `service._walk_people` — they
appear in the existing `people` projection like any other person, never
as projection fabrications.

Envelope: `env["social_state"]` (B request 1; validated versioned
dict, additive, pickle/undo/replay-safe):

```jsonc
{"schema_version": 1, "next_num": 5,
 "circles": {
   "school":  {"cohort_key": "hs:42"|null, "member_ids": ["<pid>"]},
   "work":    {"<assignment_id>": {"member_ids": ["<pid>"]}},
   "clubs":   {"<club_id>": {"member_ids": ["<pid>"], "since_turn": "t_N"}}
 },
 "people": {"<pid>": {
    "kind": "acquaintance|friend|best_friend|rival|bully",
    "closeness": 0,                  // 0-100
    "status": "active|faded|estranged",
    "since_turn": "t_000042",
    "source": {"type": "school|work|club|event", "ref": "<cohort/assignment/club/event id>"},
    "met_turn": "t_000042"}},
 "best_friend": "<pid>|null",       // at most one at a time
 "popularity": {"score": 0, "phase": "child|teen|adult",
                "history": [{"turn": "t_N", "score": 12}]},  // capped 20
 "invites": {"<invite_id>": {
    "invite_id": "inv_1",            // B shape: inv_N
    "kind": "party|study_group|after_work|team_practice",
    "host_id": "<pid>", "circle": "school|work|club",
    "venue_label": "Riverside Park", "adults_only": false,
    "offered_turn": "t_000042",
    "decision_id": "d_12"|null,      // echoed while status == "open"
    "status": "open|accepted|declined|expired"}}
}
```

Circle membership is written by A's own transition hooks (no other
feature edits social state directly): classmates join/leave on school
phase/program cohort changes (A's education hooks call
`social.on_cohort_change`), coworkers join on assignment acceptance and
go `faded` when the assignment closes (A's careers adapter/bridge calls
`social.on_assignment_change`). Club membership is written ONLY by A's
`join_club`/`leave_club` appliers, called from C's `club_join` /
`club_leave` rule actions (C's section). A faded ex-coworker keeps a
`people` record (status `faded`) — the "job change keeps contact"
behavior; losing contact entirely is an event-driven `estranged`
transition (C content via applier), never automatic.

Members are generated NPCs (names/appearance through the existing
generators), capped per circle (school 12, work 8, club 15, friends
implicit via kind records). Caps are data, not engine branches.

## Relationship kinds, closeness, drift, appliers

Kinds: `acquaintance -> friend -> best_friend` (closeness growth;
`best_friend` exclusive — setting a new one demotes the old),
`rival` and `bully` set by v3 events (C) through A's single appliers.
Kind/closeness/popularity NEVER mutate in content directly:

- `social.apply_kind(env, person, target_id, kind)` — typed
  (unknown person/kind -> ValueError), journaled, reversal-safe.
- `social.apply_closeness(env, person, target_id, delta, why)` —
  clamped 0-100, actual-delta first-person journal line, feeds
  popularity at the yearly recompute.
- `social.apply_popularity_delta(env, person, delta, why)` — bounded
  +-20 per call, clamped, journaled, occurrence-keyed.
- `social.join_club(env, person, club_id)` / `leave_club` — the ONLY
  writers of `circles.clubs`; enforce C's data gates (age/adults_only,
  capacity) with the precise reason BEFORE any mutation, money (fees)
  through the one poster with per-year occurrence keys.
- `social.record_memory(env, person, target_id, fact_text)` — see below.

Yearly drift hook: ONE line in the coordinator's existing hook
registry, frozen slot AFTER `relations_age`, BEFORE `player_stats` (no
phase reorder, no new phase id). Drift: `faded` contacts decay,
friendship slow-burn (+/- small), rival grudge cooldown. Drift draws
from the mounted SIMULATION stream (it mutates state); flavour text,
if any, from the cosmetic stream only. The same hook recomputes
popularity from stored state (no rng on reads) and expires `open`
invites older than one aged year (status `expired`, silently — never a
soft-lock).

## Memory facts ("remember what you did")

Storage = A's knowledge facts (M2.5 knowledge feature; audience =
the friend `person_id`; `fact_id` stable). "Remembers what you did" =
facts the target person knows about the player. The envelope keeps a
bounded index (cap 20/person) of `{fact_id, turn, sentiment}`;
serialization applies the existing player_known filter — a fact the
target cannot know never leaves the server (the M2/M3 private-sentinel
rule unchanged). The people-projection delta exposes the surviving
lines as plain strings (B shape: `social.memory: [text]`); the UI
renders them verbatim and never synthesizes memories (B's rule).

## Invites (stored offers through the v3 pipeline)

NPC-hosted invites are ordinary v3 events (C-owned content, event kind
`social_invite`), offered through the existing pipeline and resolved
through the shared `resolve_decision` boundary. A owns (B request 2):

- pending kind `social_invite`, `display_category "social"`,
  `decision_id` echoed into the invite record while `status == "open"`
  (cleared on resolve/expiry). Options carry `known_costs` and no stat
  spoilers; resolve applies atomically inside the existing seam (the
  `career_offer` / `education_funding` pattern).
- the `social_invite` resolve applier: accept applies the event's
  effects through the existing bundle seam PLUS the envelope update
  (accepted, closeness to host +, memory line); decline is always free;
  occurrence id `soc:<invite_id>` per posting. DEFENSE-IN-DEPTH 18+
  GATE: an `adults_only` invite refuses accept (typed `not_allowed`,
  "you must be 18 or older to attend") when the player is under 18 —
  even if content forgot to gate the offer.
- money posts ONLY through the one ledger poster with unique occurrence
  keys; free invites post nothing. Ledger category reuses an existing
  frozen value (no new category vocab).

Player-hosted invites: out of scope for v1.

## Popularity

One 0-100 score per life, phase-tagged (`child|teen|adult`, phase from
age at recompute). Yearly recompute from stored state only: circle
sizes, active-closeness sums, kind histogram (rival/bully negative,
best_friend bonus), bounded deltas applied via the applier. Bands are
DATA (A-owned `src/game/features/social/popularity_bands.json`), not
code — frozen vocabulary per B: `unknown` 0-19, `known` 20-39,
`liked` 40-59, `popular` 60-79, `beloved` 80-100; each band maps to its
display `label`. Projection exposes `score`, `band`, `label`,
`delta_last_year` (actual, may be negative).

## Commands

No new player-facing command IDs in v1: invites resolve through the
shared `resolve_decision`; club join/leave are C-owned rule actions
(`club_join {club_id}` / `club_leave {club_id}`) riding the existing
rule-action boundary — C lists them in §C and A adds the IDs to the
dispatcher allowlist exactly like M3's `train_skill`. Person social
actions (spend time, stand up to, tell someone, befriend) are likewise
C-owned rule actions on the existing §Person mechanism; their state
effects go through the appliers above.

## Projections (A-owned; B renders exactly this)

`_state["social"]` per B's published shape (02-contracts.md): `groups`
in the frozen bucket order family/friends/classmates/coworkers —
`family` from the existing family walk (social delta may be absent),
`friends` from `people` records with a kind, `classmates`/`coworkers`
from the circles (a person in several circles appears in each, with
their `circles` list saying why); `invites` (newest first, host as
person_ref, `decision_id` while open); `popularity`; `clubs` (C's club
data merged with A's membership and C's advertised action specs).
Additive `person_ref` delta on the existing people projection per B
request 3: `social {kind, closeness, circles, memory}` — `kind: null`
for plain circle members (never fabricated); `memory` player_known-
filtered. Guarded like careers (projection never breaks state reads);
deceased contacts serialize read-only via the existing alive flag.

## Migration / invariants (B request 5)

Pre-M4 saves validate to the frozen empty envelope: no circles, no
people, `best_friend` null, popularity score 0 with the age-appropriate
phase, no invites; NO retroactive circles. Invariants pinned by forced
tests: person_id addressing everywhere; at most one best_friend;
closeness clamped; invite occurrence keys unique; adult-venue 18+ gate
unbypassable; replay byte-identical; undo-safe via the existing
snapshot; no money outside the one poster; projections pure reads.

## File manifest (A-owned)

- `src/game/features/social/__init__.py`, `state.py` (envelope,
  appliers, migration default), `circles.py` (cohort/work circle
  build/refresh, NPC generation via existing generators), `drift.py`
  (yearly hook body), `invites.py` (pending/applier/expiry),
  `popularity.py` (recompute + band mapping), `projection.py`.
- `src/game/features/social/popularity_bands.json` (band data).
- `src/game/service.py` — `_state["social"]`, people-projection delta,
  envelope default in validation, `_cmd_resolve` route for
  `social_invite`, allowlist additions for C's club/social action IDs.
- `src/game/coordinator.py` — ONE hook-registry line for the yearly
  social drift slot.
- `src/game/features/careers/core.py`, `src/game/features/education/core.py`
  — the existing transition hooks call `social.on_assignment_change` /
  `social.on_cohort_change` (A-owned files, additive one-liners).
- `tests/test_social*.py`, `tests/test_game_m4_social.py` (forced real-
  service tests, 3 hash seeds).

## Cross-reference requests

**TO B (02-contracts.md §UI):** none beyond what your section already
pins — the projection/pending shapes above implement your "TO A" list
verbatim. One note: `groups[].people` entries reuse the existing people
fields (id/name/age/alive/appearance) plus the `social` delta.

**TO C (02-contracts-C.md):** (1) v3 schema additions you own: event
kind `social_invite` (fields used by A: `venue_label`, `adults_only`,
host role binding); condition kinds `social_kind`, `min_closeness`,
`in_circle`, `popularity_band`; role bindings `host`/`target` from the
social envelope. (2) `src/game/content/social/clubs.json` per B's
shape (stable ids, label, category, meets_label, cost_per_year,
adults_only) + `club_specs(env, person)` advertised actions. (3)
`club_join`/`club_leave` rule actions calling A's appliers (never
writing the envelope directly). (4) Person social actions on the
§Person overlay, effects through A's appliers. (5) ≥30 v3 social
events per the kickoff; invite events set `venue_label`/`adults_only`
so A's gate has data.

**TO D (02-contracts-D.md):** validator inputs include A's
`popularity_bands.json` (band bounds monotonic, labels present);
journeys use real commands only; privacy probes on `social.memory`
reuse the knowledge-filter sentinels; 18+ probes hit the resolve
applier gate (decline stays legal for minors, accept is the gated one).

**FROM A to all:** engine ships after C's `clubs.json` + event kinds
land (clubs projection and invite content are C-shaped); until then
the projection renders honest empties (B's hidden-mount rule).

## Implementation clarifications (frozen with the implementation, 2026-11-21)

- **NPC derivation draws nothing.** Classmate/coworker traits derive
  from sha256(creation fingerprint + envelope counter) — deterministic
  per life, identical for same-seed twins, zero draws from any rng
  stream (legacy M2 seeded parity is never perturbed; person_id stays
  the os.urandom-backed identity exception). Group counts: 4-6
  classmates, 3-5 coworkers.
- **Social NPC storage.** Living social NPCs persist in
  `env["social_people"]` (list of canonical Person objects); A-owned
  service seams (`_ensure_ids`, `_make_resolver`, the people walk in
  `_state`) include them so addressing/resolution/projection treat
  them like every other person.
- **Popularity bonus accumulator.** `apply_popularity_delta` moves the
  live score AND a bounded accumulator (`bonus`, +-30) that the yearly
  recompute adds to the structural formula — event-applied deltas
  survive recomputes. delta_last_year = score - last_score, updated at
  each yearly recompute.
- **Knowledge kind.** "social_memory" was added to knowledge
  FACT_KINDS (knowledge core is A-owned from M2.5); memory lines mint
  a fact with subjects [player, friend] and teach it to the friend
  (audience = friend).
- **Clubs are LIVE (C landed).** `clubs[]` renders C's catalog merged
  with membership and C's `club_specs`; `join_club` re-validates
  C's gates before mutating, writes membership (the ONLY writer),
  posts the per-year fee itself through the one ledger poster with
  occurrence `club:{club_id}:{person_id}:{age}` (category "other"),
  and applies the club's popularity_delta. `leave_club` removes
  membership (free).
- **Invite expiry mechanics.** An `open` invite whose stored
  `offered_turn` differs from the current year's turn id expires at
  the yearly hook (covers cancelled/lost pendings; a live pending is
  resolved before aging by the existing gate, so expiry is the
  safety net, not the normal path).
- **Service seams.** A-owned reconcile calls after careers/education
  commands and both offer resolves (inside the mounted boundary);
  `_cmd_resolve` routes kind `social_invite` to `_resolve_social_invite`
  (money via the one poster, stats via the effects bundle, journal,
  interrupted-year resume).
- **C's landed actions are already wired:** `club_join`/`club_leave`/
  `social_*` were whitelisted by C through ACTION_IDS (rule-action
  boundary); A's appliers are consumed via C's `_social_appliers`
  bridge. A's accessors for C's v3 leaves: `social.state.edge`,
  `social.state.in_circle_group` (plus `arc_stage`, `in_club`,
  `popularity_score` in state.py).

## Amendment 2 (Claude-approved via t7, 2026-11-21): soc_ yearly route,
# invites routing, bullying arc wiring (real-play review)

1. **Yearly route (Amendment-4 pattern):** new phase `social_events`
   after `workplace_events` (no reorder), served by
   `src/game/features/social/events_route.py` through the existing v3
   pipeline. Cadence: one `soc_` occurrence per aged year, once per life
   (`env["soc_seen"]`; per-year marker `env["turn"]["soc_served"]`).
   Gates stay in C's condition trees.
2. **soc_inv_* routing:** records with `interaction.kind ==
   "social_invite"` are instantiated through the normal v3 path, then
   converted: pending kind becomes `social_invite`, an invite envelope
   record is created (`invites.register_for_v3`), and the projection's
   `decision_id` is derived from the open pending. Resolve rides the
   SHARED v3 resolver (C's `resolve_choice` — no second interpreter):
   A's `_resolve_social_invite` enforces the adults_only 18+ gate
   before delegating, then marks the invite accepted/declined. C's
   accept effects (stats/money/relationship deltas) post exactly as
   for any v3 event.
3. **Bullying arcs:** `bullying.start_bullying_arc(env, service,
   person)` assigns a bully from the current school circle (seed-stable
   hash pick, zero rng draws), sets kind/arc targeted, journals.
   `bullying_response` domain effect: A registers the applier (from
   `social.predicates.register_domain_effects`, idempotent) which
   delegates to A's frozen `apply_response` table application — C's
   proactive actions and v3 event options share ONE outcome table.
4. **Seed-stable clicker:** drift's per-year "clicked" pick hashes the
   TURN only, indexing into the deterministic member ORDER — never
   uuid4 person_ids (same-seed twin lives stay byte-identical).
