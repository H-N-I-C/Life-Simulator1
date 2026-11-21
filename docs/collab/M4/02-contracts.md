# M4 contracts — FROZEN v1 by Claude 2026-11-21 (with 02-contracts-A.md, -C.md, -D.md). Decisions: coworker job change → faded (keeps contact), estranged only via events; event prefix soc_; empty-default migration, no retroactive circles. Changes only via a Claude-approved amendment note.

Scope: docs/collab/M4/00-kickoff.md. Everything rides the canonical person
graph, the one ledger and the v3 event pipeline — no parallel mini-engine.
Fixed rules from the kickoff: romance stays 18+ for both parties (M2.5);
minors' social content stays age-appropriate; adult-only venues are 18+.

Owners write their own section; interfaces between features are the
cross-reference requests at the end of each section. No parallel
implementation before the shared shapes are marked FROZEN.

Shared conventions (all sections):
- Commands ride the existing validated action/replay/transaction boundary
  (`GameService.command`); no feature HTTP; no nested saves; one RNG
  authority (the mounted command stream); one ledger poster.
- Projections are additive, server-computed, pure reads. The UI never
  computes eligibility, never samples, never posts.
- Person addressing is always `person_id`; labels come from data, never
  raw ids.
- Money uses M2's frozen `{phase, occurrence_id, leg, account_owner_id,
  category, amount, description, group_id?}` request through the one
  poster; previews never sample or post.
- Privacy: only player-known facts leave the server (the M2/M3 knowledge
  filter applies to social memory facts exactly like journal entries).

---

# §UI (Kimi B) — Relationships tab redesign, invites inbox, popularity,
clubs on Activities

B owns the social UI end to end: the module, its shell mounts, the
adapter passthrough and the DOM tests. B does NOT own social rules,
content, sampling or state — those are A (engine) and C (content/rules).

## File manifest (B-owned)

- `web/static/features/social/social.js` — `mountSocial(root, hooks, data)`
  per the frozen §Shell mount API (`{update(data), unmount()}`), data =
  `state.social`; renders groups, invites inbox, popularity.
- `web/static/features/social/social.css` — `.v2so-*` namespace, consumes
  §Tokens with fallbacks (same pattern as careers/skills/education).
- `web/static/features/social/clubs.js` + `clubs.css` (same directory,
  `.v2cl-*` namespace) — `mountClubs(root, hooks, data)`, data =
  `state.social.clubs`; clubs card for the Activities screen. Two mounts
  from one projection slice so the Relationships tab never pays for the
  clubs markup and vice versa.
- `web/static/game.html` — hosts: `<div id="v2SocialMount"></div>` at the
  top of `screen-people`; `<div id="v2ClubsMount"></div>` above the
  Activities card in `screen-activities`; the two module stylesheets.
- `web/static/game.js` — `FEATURE_MODULES` registration (lazy import +
  `update(data)` per refresh, unmount when the projection key is absent;
  the existing M3 registry pattern, no per-module shell edits).
- `web/static/adapter.js` — additive passthrough `social:
  raw.social || null`.
- `tests/web_v2_social_dom.mjs` (+ `web_v2_clubs_dom.mjs`) — node DOM
  stubs, no server.

The existing `screen-people` family section (Family card, roster,
relationship/family actions) and the C-owned person overlay stay — the
social module sits above them; where the `family` group overlaps the
legacy family section, the legacy section remains authoritative for
family ACTIONS (no reparenting of C's controls).

## Data the UI consumes (`state["social"]`, A-owned projection)

Server-computed, pure read, additive. The UI renders exactly this and
nothing else.

```jsonc
"social": {
  "schema_version": 1,
  "groups": [                    // frozen bucket order
    {"group_id": "family",     "label": "Family",
     "people": [person_ref]},
    {"group_id": "friends",    "label": "Friends",
     "people": [person_ref]},   // may be empty -> hidden honestly
    {"group_id": "classmates", "label": "Classmates",
     "people": [person_ref]},
    {"group_id": "coworkers",  "label": "Coworkers",
     "people": [person_ref]}
  ],
  "invites": [                   // the inbox; stored offers (A samples)
    {"invite_id": "inv_3",
     "kind": "party" | "study_group" | "after_work" | "team_practice",
     "title": "Birthday party",
     "host": person_ref,             // the inviter (person_id inside)
     "venue_label": "Riverside Park",
     "adults_only": false,           // true -> "18+" badge, server-gated
     "status": "open" | "accepted" | "declined" | "expired",
     "decision_id": "d_12" | null}   // set while status == "open"; the
                                     // inbox resolves THROUGH the frozen
                                     // pending envelope, never by itself
  ],
  "popularity": {
    "score": 23,                    // 0-100, person-scoped
    "band": "known",                // frozen vocabulary (see below)
    "label": "Well known",          // human label for the band
    "delta_last_year": 2            // actual delta, may be negative
  },
  "clubs": [                       // C-owned data, A/C-shaped projection
    {"club_id": "chess_club",
     "label": "Chess club",
     "category": "school" | "sport" | "hobby" | "work",
     "member": true,
     "role": "member" | "captain" | null,
     "meets_label": "Tuesdays after class",
     "cost_per_year": 0,
     "actions": [                  // advertised club actions, frozen spec
       {"id": "club_leave", "label": "Leave the club",
        "category": "social", "inputs": [],
        "known_costs": [], "limits": [],
        "enabled": true, "reason": null}]}
  ]
}
```

`person_ref` extends the existing people entries (A extends the people
projection; the shape below is the M4 delta, all other person fields
unchanged):

```jsonc
{"id": "<person_id>", "name": "Sam Carter", "age": 14, "alive": true,
 "social": {
   "kind": "friend" | "best_friend" | "acquaintance" | "rival" | "bully"
         | null,                       // null = plain circle member
   "closeness": 62,                   // 0-100
   "circles": ["classmates"],         // why this person is grouped here
   "memory": ["We worked on the science fair together."]  // player_known
}}                                   // facts ONLY (privacy filter); the
                                     // UI never fabricates memories
```

Popularity bands (frozen vocabulary, A owns the mapping):
`unknown` (0-19), `known` (20-39), `liked` (40-59), `popular` (60-79),
`beloved` (80-100). `label` is the display string; `band` is the stable id.

## Commands the UI dispatches (C-owned rule actions / A-owned pendings)

The UI dispatches ONLY ids the server advertised, through the frozen
shell hooks (`hooks.sendAction`, `hooks.resolveDecision`) — never fetch,
never free-typed targets.

1. **Invites resolve through the frozen pending envelope.** Request to A:
   pending kind `social_invite` (`display_category "social"`), options
   carrying `known_costs` (usually free) and no stat spoilers, resolve
   applied atomically inside the existing resolve boundary (the
   `career_offer`/`education_funding` pattern). The inbox row for an
   `open` invite renders Accept/Decline buttons that call
   `hooks.resolveDecision(invite.decision_id, <advertised option id>)`;
   the advertised option ids come from the pending projection (the UI
   never hardcodes option ids beyond reading them from the decision it
   fetches via `hooks.getState().pending` when ids match; when the
   pending is not loaded, the button navigates to the decision surface
   via the existing pill/modal instead of resolving blind).
2. **Clubs**: `club_join {club_id}` / `club_leave {club_id}` (C-owned,
   validated + replay-safe, advertised inside `clubs[].actions` or the
   action list with `known_costs`). Adults-only clubs (bars etc.) refuse
   under-18s server-side with the precise reason; the UI shows the
   server's `reason` on the disabled control and renders an "18+" badge
   from `adults_only`/advertised data only.
3. **Person social actions** (spend time with a friend, stand up to a
   bully, tell someone about bullying, befriend): C-owned, advertised on
   the existing person overlay action list (the §Person mechanism — no
   new surface). The Relationships-tab chips navigate to the person
   overlay (`shellHooks.navigate("person", {personId})`); actions stay
   in the overlay, not duplicated in the tab.

## UI behavior (frozen)

- **Groups** render in the frozen bucket order with honest empty-state
  hiding (a group with zero people and no pending invites is not
  rendered). A person in several circles appears in each group; their
  `social.kind` renders as a badge (`Best friend`, `Rival`, `Bully`…)
  using C/A-provided labels, never ids.
- **Closeness bars** per chip: width = `social.closeness`, color bands
  red/amber/green, no numeric spoiler beyond the projection.
- **Invites inbox**: newest first, status chips (Open / Accepted /
  Declined / Expired), host chip (clickable → person overlay), venue
  label, "18+" badge when `adults_only`. Open invites resolve only
  through the pending envelope (above). Expired/resolved invites are
  read-only history rows.
- **Popularity indicator** in the Relationships tab header: band label +
  score and last-year delta ("Well known · 23 (+2)"), all verbatim from
  the projection.
- **Clubs card** on Activities, above the Activities card (same position
  rule as skills): club name, category chip, meets label, real cost,
  Join/Leave with server `reason` on disabled controls.
- **No client-side eligibility anywhere**: every enabled/disabled state
  and every reason string comes from the server (advertised specs /
  projection). The UI never re-derives gates from age, kind or closeness.
- **Age-appropriateness is server authority**: minors never see adult
  venues as joinable/accept-able — the server refuses with its reason
  and the UI only displays it. The UI adds no extra gating of its own.
- **Privacy**: `social.memory` lines are already player-known-filtered by
  A; the UI renders them verbatim and never synthesizes social facts.

## Example (one filled shape)

```jsonc
"social": {
  "schema_version": 1,
  "groups": [
    {"group_id": "classmates", "label": "Classmates", "people": [
      {"id": "p_88", "name": "Sam Carter", "age": 14, "alive": true,
       "social": {"kind": "best_friend", "closeness": 78,
                  "circles": ["classmates"],
                  "memory": ["We got detention together."]}},
      {"id": "p_91", "name": "Dana Cole", "age": 14, "alive": true,
       "social": {"kind": "bully", "closeness": 12,
                  "circles": ["classmates"], "memory": []}}]},
    {"group_id": "coworkers", "label": "Coworkers", "people": []}
  ],
  "invites": [
    {"invite_id": "inv_3", "kind": "party", "title": "Sam's birthday",
     "host": {"id": "p_88", "name": "Sam Carter", "age": 14,
              "alive": true},
     "venue_label": "Riverside Park", "adults_only": false,
     "status": "open", "decision_id": "d_12"},
    {"invite_id": "inv_1", "kind": "after_work",
     "title": "After-work drinks", "host": {"id": "p_40", "name": "Jo",
       "age": 34, "alive": true},
     "venue_label": "The Harbour Bar", "adults_only": true,
     "status": "declined", "decision_id": null}
  ],
  "popularity": {"score": 23, "band": "known", "label": "Well known",
                 "delta_last_year": 2},
  "clubs": [
    {"club_id": "chess_club", "label": "Chess club",
     "category": "school", "member": true, "role": "member",
     "meets_label": "Tuesdays after class", "cost_per_year": 0,
     "actions": [{"id": "club_leave", "label": "Leave the club",
                  "category": "social", "inputs": [],
                  "known_costs": [], "limits": [],
                  "enabled": true, "reason": null}]},
    {"club_id": "harbour_run_club", "label": "Harbour Runners",
     "category": "sport", "member": false, "role": null,
     "meets_label": "Saturday mornings", "cost_per_year": 50,
     "actions": [{"id": "club_join", "label": "Join ($50/year)",
                  "category": "social", "inputs": [],
                  "known_costs": [{"kind": "money", "amount": 50}],
                  "limits": [],
                  "enabled": false,
                  "reason": "you must be 18 or older to join"}]}
  ]
}
```

## Cross-reference requests

**TO A (engine), exact:**
1. `_state["social"]` projection per the shape above (groups from
   circles, invites from stored offers, popularity, person_ref delta),
   guarded like careers; envelope key `env["social_state"]` with the
   frozen empty default for pre-M4 saves.
2. Pending kind `social_invite`, `display_category "social"`, resolved
   through the existing `_cmd_resolve` seam; `decision_id` echoed into
   the invite record while open.
3. People-projection delta: `social {kind, closeness, circles, memory}`
   with the player_known privacy filter on `memory` (missing person =
   no delta, never fabricated).
4. Popularity band mapping (frozen vocabulary above) as data, not code.
5. Migration: pre-M4 saves validate to empty groups/invites/popularity
   baseline; no retroactive circles.

**TO C (content/rules), exact:**
1. `club_join {club_id}` / `club_leave {club_id}` rule actions
   (validated, replay-safe, 18+ gate with precise reason, money through
   the one poster with per-year occurrence keys for fees).
2. Clubs data (`src/game/content/social/clubs.json`): stable ids,
   labels, category, meets_label, cost_per_year, adults_only.
3. Person social actions advertised on the person overlay (spend time,
   stand up to, tell someone, befriend) using the existing §Person
   action mechanism — ids frozen in C's section.
4. ≥30 v3 social events + bullying arcs + invite events per the kickoff
   counts; event IDs referenced from clubs/circles data must validate.

**TO D (QA):** M4 content validator + the kickoff's journey list;
privacy probes on `social.memory`; 18+ venue probes.

**FROM B to all:** the UI ships only after `state.social` and the
`social_invite` pending land; until then the mounts stay hidden (the
existing honest-absence rule — no fabricated empty cards).

> NOTE (Claude): sections A, C and D were lost to concurrent overwrites of this file. Each owner now keeps its section in its OWN file: 02-contracts-A.md, 02-contracts-C.md, 02-contracts-D.md (B's section stays here). Never rewrite another owner's file.

## Amendment 1 (Claude, 2026-11-21): clubs.json money field
Clubs use `cost_per_year` (B's UI shape, as implemented) instead of C's `join` object. Rule: a club with min_age ≥ 18 must have adults_only true. D's validator follows this.
