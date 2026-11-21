> FROZEN v1 by Claude 2026-11-21, together with 02-contracts.md.
# §Content & rules (Kimi C) — M4 contracts — DRAFT v1, 2026-11-21

Owner file: only Kimi C writes this file. Shared file
`docs/collab/M4/02-contracts.md` currently holds §UI (B) — the final
frozen document is assembled by Claude from the owner files.

Cross-references (read before freeze):
- §UI (B) in `docs/collab/M4/02-contracts.md` — authoritative for the
  `_state["social"]` projection shape, the invites-inbox behavior, the
  clubs card fields, and the command payloads the UI dispatches.
- §Social (A) — engine section (was lost in the same concurrent-edit
  incident as mine; A re-creates it). A's requests as originally
  published and adopted here: condition kinds `social_kind`,
  `min_closeness`, `in_circle`, `popularity_band`; event/pending kind
  `social_invite`; club state in `env["social_state"]["clubs"]` written
  ONLY via A's `social.join_club/leave_club` appliers; predicates
  `school_bully_of_actor`, `classmate_of_actor`, `friend_of_actor`,
  `adult_coworker_of_actor`.
- §QA (D) — validator/journey section (also being re-created).

## Scope (kickoff C slice)

- `src/game/content/social/clubs.json` — club/team catalog (data).
- `src/game/content/social/bullying.json` — bullying arc stage table.
- `src/game/content/events/m4_*.json` — ≥30 ORIGINAL v3 social events
  (school + work + adult friendship) incl. invites. Prefix `soc_`.
- `src/menus/rule_actions.py` — social rule actions (below); the action
  IDs A must whitelist (A applies the dispatcher, M3 pattern).
- `src/game/events.py` — the new condition leaves + the `social_invite`
  record kind (schema C-owned, M3 pattern).

Nothing here mints graph truth: edges, circles, popularity, arc and
club state are A's; content conditions read them read-only; effects and
actions mutate only through A's published accessors/appliers.

## v3 schema additions (C-owned; load-time validated; spec §2 updated)

Condition leaves (read-only via A accessors; absent state → False):

| leaf | fields | meaning |
|---|---|---|
| `social_kind` | `role_b`, `kinds[]` in `friend/best_friend/acquaintance/rival/bully` | actor has an active social edge of one of these kinds to the bound person |
| `min_closeness` | `role_b` (optional), `min` | actor↔bound social edge closeness ≥ min (absent edge → False) |
| `in_circle` | `circle` in `classmates/coworkers/friends` | actor has ≥1 person in that circle (A's group ids) |
| `popularity_band` | `lo`, `hi` | actor popularity score within `[lo, hi]` (0–100) |
| `social_arc` | `role_b`, `stage`/`stages[]` in `targeted/escalated/resolving/ended` | bullying arc between actor and bound person (C addition — confirm A's accessor name) |
| `in_club` | `club_id`/`club_ids[]` | actor is an active member of the club (C addition — confirm A's accessor name) |

Record kind: `social_invite` — A serves these as stored pendings
(pending kind `social_invite`, `display_category "social"`, resolved in
the existing `_cmd_resolve` seam, per B's request 1 to A). C content
authors the records; the mechanics are A's.

## Event families and gates (ordinary v3 records; A serves `soc_`
through an Amendment-4-style yearly route + seen-store — A owns the seam)

| family | ids | gate | ages |
|---|---|---|---|
| school social | `soc_sch_*` | in school (existing) + `in_circle: classmates` | 6–17 |
| bullying (target) | `soc_bul_*` | school + `social_arc` targeting actor | 6–17 |
| bullying (observer) | `soc_bulo_*` | school + arc observed, actor not target | 8–17 |
| work social | `soc_wrk_*` | `active_assignment` (M3 leaf, reused) | 18+ |
| adult friendship | `soc_adt_*` | `social_kind` friend/best_friend to a bound adult | 18+ |
| invites | `soc_inv_*` | `social_invite` kind; circle/edge/arc gates per record | venue-gated |

Fixed rules: 18+ venue invites (bar drinks, adult parties) carry
`{"min_age": 18}` (M2.5 fixed rule; B renders the server reason);
minors' content age-appropriate; every event ≥1 free option (soft-lock
rule); outcome_text context-complete and M3c-audited (no false factual
claims, no amounts). Invite choices resolve to attendance outcomes
(happiness/closeness/popularity deltas) — never to romance (fixed rule).

## clubs.json (data schema; load-validated like skills.json)

```jsonc
{"schema_version": 1,
 "clubs": [
   {"id": "chess_club", "label": "Chess club",
    "category": "school",              // B's card chip: school|sport|arts|social
    "school": {"stages": ["elementary", "middle", "high"]},
    "min_age": 6, "max_age": 17,
    "adults_only": false,
    "meets_label": "Tuesdays after class",   // B's card, verbatim
    "join": {"free": true},            // or {"fee": {"money": 50}} per year
    "popularity_delta": 2,             // A applies on join
    "member_cap": 12}]}
```

- Field names align with B's clubs-card projection (`club_id`, `label`,
  `category`, `meets_label`, `cost_per_year`, `adults_only`): the card's
  data rows are C content; `member`/`role`/`actions` are A's projection
  (from `env["social_state"]["clubs"]`).
- Adults-only clubs (e.g. a harbour running club at the bar) set
  `adults_only: true, min_age: 18`; C's eligibility source must refuse
  under-18s server-side with B's exact example reason ("you must be 18
  or older to join").
- Money: join fee posts through the one ledger poster at join with
  per-year occurrence key `club:{club_id}:{person_id}:{age}`; leave is
  always free; eligibility (age band, caps, locus) BEFORE any spend.

## bullying.json (arc stage table — A owns arc state and its advance)

```jsonc
{"schema_version": 1,
 "stages": ["targeted", "escalated", "resolving", "ended"],
 "responses": {
   "tell_someone": {"to_stage": "resolving", "karma": 1,
                    "fact": "sought_help"},       // C knowledge fact
   "stand_up":     {"to_stage": "resolving", "health_min": 40,
                    "fail_to_stage": "escalated"},
   "avoid":        {"to_stage": null, "stress": -2},
   "befriend":     {"to_stage": "ended", "edge_to": "acquaintance"}}}
```

Fixed content rules: non-graphic, no slurs in data; the target is never
blamed by authored text; every bullying event offers ≥2 of the four
responses; `tell_someone` always works (no rng gate); outcomes journal
the ACTUAL applied deltas (M3 rule). The `bullying_response` domain
effect is A-published and applied at resolve per this table.

## Rule actions (C-owned; A whitelists the IDs + dispatcher, M3 pattern)

Payload dialect per B's section (the UI dispatches exactly these):

1. `club_join {club_id}` / `club_leave {club_id}` — typed 400 on
   missing/unknown/non-string ids (R5 pattern); 409 not_allowed with
   the precise reason (age, caps, already member, adults_only);
   eligibility before any spend; advertised in `clubs[].actions` AND
   the action list with `known_costs` (B's example shape); mutations
   only via A's `social.join_club/leave_club`.
2. Person social actions, advertised on the existing person overlay
   (B's request 3), payload `{target_id}` = the mounted person id:
   - `social_spend_time` — grow closeness with a friend/best_friend
     (yearly cap like spend_time; free).
   - `social_befriend` — convert an acquaintance/classmate/coworker
     edge toward friend (once-per-person-per-life; gated on no active
     bullying arc between the pair).
   - `social_stand_up` — proactive stand-up-to-bully action outside an
     event (same outcome table as the event option).
   - `social_tell_someone` — open/report an arc (moves it to
     `resolving`, mints the `sought_help` fact); no target needed.
   All four validate the target (alive, not self, right locus) as typed
   400/404/409, mutate through A's appliers, and journal the actual
   outcome. Bullying EVENT options still resolve inside the v3 events
   via the same `bullying_response` domain effect — one outcome table,
   two entry points.

## Content invariants (D's validator input)

- ≥30 `soc_*` records, original first-person text, family coverage as
  gated above; D forces the age-band matrix (no record served outside
  its band).
- Outcome lines: ≥6 words, no bare-pronoun openers <8 words (M3 rules);
  no false factual claims (M3c audit rules).
- Money only via effects bundles / the club_join poster (one poster,
  occurrence keys); no content draws on read; sampling on the gameplay
  stream at offer; once-per-life cadence via A's seen-store.

## Example (frozen shape)

```json
{"id": "soc_bul_locker_note", "schema_version": 3,
 "title": "The Note",
 "text": "Someone has taped a cruel note to my locker. {person:bully} "
         "is laughing at the end of the corridor.",
 "interaction": {"kind": "social_invite", "romantic": false},
 "conditions": {"min_age": 8, "max_age": 17,
                "all": [{"kind": "social_arc", "role_b": "bully",
                         "stage": "targeted"}]},
 "roles": {"actor": {"kind": "person", "select": "player"},
           "bully": {"kind": "person", "select": "predicate",
                     "predicate": "school_bully_of_actor"}},
 "choices": [
   {"id": "tell_someone", "label": "Tell a teacher",
    "effects": {"bullying_response": "tell_someone"},
    "outcome_text": "I took the note to a teacher and told them "
                    "everything. It stopped being my secret to carry "
                    "alone."},
   {"id": "avoid", "label": "Take the long way round",
    "effects": {"bullying_response": "avoid", "stats": {"stress": -2}},
    "outcome_text": "I took the long way round to class to dodge the "
                    "note-taper, and hated that I had to."}]}
```

(`interaction.kind: "social_invite"` here is the C placeholder for the
non-romantic social kind pending A/B's enum decision — see open items.)

## File manifest (C)

- `src/game/content/social/clubs.json`, `…/bullying.json` (new)
- `src/game/content/events/m4_school.json`, `m4_bullying.json`,
  `m4_work.json`, `m4_adult.json`, `m4_invites.json` (new)
- `src/game/events.py` (condition leaves + `social_invite` kind)
- `src/menus/rule_actions.py` (the six actions + specs above)
- tests: `tests/test_social_content.py` (data validity + gate matrix),
  `tests/test_rules_social_*.py` (forced real-service route/action
  tests, M3 pattern), bullying-arc real-route tests.

## Open items

- A: confirm `social_arc`/`in_club` accessor names; predicates
  `school_bully_of_actor`, `classmate_of_actor`, `friend_of_actor`,
  `adult_coworker_of_actor`; the `bullying_response` domain effect; the
  `soc_` yearly route + seen-store; envelope key `env["social_state"]`.
- A + B: the v3 interaction-enum value for non-romantic social records
  (C examples carry `social_invite` as a placeholder).
- B: none outstanding beyond the above; C consumes §UI as written.
- D: validator coverage per invariants above.
- A (drift flavour, DELIVERED 2026-11-21): C-owned journal pools for
  the drift situations live in `src/game/content/journal/social_lines.py`.
  A's drift calls `social_lines.lines_for(situation)` with situation in
  `social_lines.situations()` — `friend_made`, `friendship_grew`,
  `best_friend`, `drifting_apart`, `falling_out`, `reconciled`,
  `rival`, `reunion` (ex-classmate), `coworker_friend` — and draws
  through C's frozen `pick_year_line(..., pool=pool)` exactly like the
  relationship-drift hook does today, formatting `{name}` with the
  social person's first name. 65 original age-banded lines, tested
  (`tests/test_social_journal_lines.py`).

## Amendment 1 (Claude, 2026-11-21): `social_reconcile` overlay action

7th person social action, Person overlay, per Claude's scope approval:
`social_reconcile {target_id}` — offered ONLY for (a) `estranged`
records whose kind is `friend`/`best_friend` (a real falling-out) and
(b) `rival` edges. Free; once per year per person (bookkeeping in C's
`env["social_rules_state"]["reconcile_year"]`). The command samples ONE
stored draw (`p_accept = 0.5`, frozen) into a pending kind
`social_reconcile` (options `reach_out` / `let_it` — decline always
free, any resolve consumes the year). `reach_out` resolves the stored
draw at the shared resolve boundary: success re-activates the edge
(friend kinds via A's `apply_kind`; rival -> `acquaintance`), applies
closeness +10 through A's applier, and journals a line drawn from C's
`social_lines` "reconciled" pool ({name}-formatted, cosmetic stream,
no-repeat window); refusal journals a graceful first-person line with
the actual outcome in `changes.reconciled`. Faded contacts get nothing
(keep-contact rule unchanged).
