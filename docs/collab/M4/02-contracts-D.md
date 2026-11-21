> FROZEN v1 by Claude 2026-11-21, together with 02-contracts.md.
# §QA + content validation (Kimi D) — DRAFT v2, 2026-11-21

Owner file: only Kimi D writes this file. The shared file
`02-contracts.md` holds B's §UI section; A and C keep
`02-contracts-A.md` / `02-contracts-C.md`. Claude freezes when the
cross-references agree. D accepts B's proposed shapes (pending kind
`social_invite`, `state.social`, `club_join`/`club_leave`,
person-overlay social actions) as the working surface and will adjust
only via amendments approved by Claude.

D owns the M4 content validator, the M4 browser/HTTP journeys, the
privacy and 18+ venue probes, and the evidence under
`docs/collab/evidence/M4/**`. D never edits production data to make
validation pass and never reimplements route/eligibility logic — every
journey assertion reads the frozen projections and dispatches the frozen
commands.

## D1 — M4 content validator (`tools/validate_m4_social.py`)

Standalone command (M3 pattern: `tools/validate_m3_content.py`):

    python3 tools/validate_m4_social.py
    # human report + JSON -> docs/collab/evidence/M4/validate-m4-social.{md,json}
    # exit non-zero when any frozen-input check fails; warnings never gate.

**Inputs (frozen only).** C's data families: `src/game/content/social/**`
(clubs/teams per B-TO-C request 2: `clubs.json` with stable ids, labels,
category, meets_label, cost_per_year, adults_only; social venue list if
authored separately) and the ≥30 v3 social events (bullying arcs, invite
events, club events) under `src/game/content/events/` with C's frozen
prefix (to be declared in C's section; D assumes `soc_` until then — one
amendment renames if C chooses otherwise). A's social feature data, if A
authors any (A's section pending). The frozen v3 event schema is
structurally validated by the production checker
`src/game/events.py:validate_event3`; D mirrors it rather than redefines
it. A missing family file is itself an ERROR (post-freeze; WARN-only
while any section is DRAFT).

**Checks (fail with file+id+rule):**
1. Structural: unique stable string IDs per family and across files;
   required keys per the family's frozen schema; `schema_version`
   present. For clubs: category in the frozen vocabulary
   (`school|sport|hobby|work`, per B's projection), cost_per_year integer
   >= 0.
2. Foreign keys: club IDs referenced by events/actions resolve; venue
   labels/IDs referenced by invite events resolve to C's venue data;
   role/employer/program IDs used for coworker/classmate binding resolve
   in the frozen M3 careers/education data; event person references use
   the frozen v3 person_id binding vocabulary — never club/employer names
   as person stand-ins (kickoff: managers/coworkers are real graph
   people, not invented text-only ones — D flags any event that names a
   non-existent person).
3. Age gating (M4-specific): every club and venue carries an explicit
   `adults_only` boolean; anything `adults_only` refuses under-18s
   (server-side reason is C/A's job — D validates the DATA declares it
   and that adult-venue/-club events and invites carry the flag);
   romance-adjacent social events keep the M2.5 18+ for BOTH parties;
   events tagged adult are not reachable from minor lives through the
   shared condition evaluator (D mirrors `evaluate_v3_conditions` with
   fixed minor fixtures — it does not invent a second interpreter).
4. Vocabulary: conditions/predicates/effects use only the frozen
   vocabularies (`PREDICATES`, `V3_CONDITION_KINDS`, v3 effect keys in
   `src/game/events.py`); no Python/eval/exec; effects may not write
   relationship/closeness/popularity fields directly — social
   transitions stay in domain commands (A/C), per the M3 event-records
   rule.
5. Economics: costs integers >= 0; every paid choice (club fees, paid
   invites, events) has a free/decline/leave exit (soft-lock rule);
   per-year fee occurrence keys are unique (M3 ledger pattern).
6. Graph/reachability: every club has at least one executable join path
   from a normal new life in its age band (minors can join school clubs;
   adults can join work/hobby clubs); bullying arcs expose at least the
   four real responses (tell someone, stand up, avoid, befriend) as
   data-bound options, not flavor-only text; invite events exist for
   each frozen invite kind (`party|study_group|after_work|team_practice`,
   B's inbox vocabulary) with `after_work` adult-only.
7. Duplicates: identical reskins (same effects/costs/prereqs under
   different IDs) reported as warnings with a diff summary.

**Fixtures.** Tiny intentionally-invalid JSON under
`tests/content/fixtures/invalid/` named by rule (`m4_club_no_adults_flag`,
`m4_minor_adult_venue`, `m4_bully_no_response`, `m4_invite_unknown_venue`,
`m4_club_unknown_id`, ...): the tool must report each fixture's expected
failure code — a fixture that passes is itself a test failure. Valid
fixtures come from the owners' frozen examples, not from D.

## D2 — M4 journeys (`tests/browser/m4_journeys.py` + `tools/m4_journeys.py`)

Runner reuses the M3 harness (`tools/m3_journeys.py:JourneyCtx`, qa
hooks, TMPDIR=~/.lifesim-tmp, one headless Chromium, profiles/saves
cleaned, evidence to `docs/collab/evidence/M4/journeys.{md,json}` +
`m4-*.png`). Step honesty rules unchanged: **PENDING(owner, want)**
when the frozen surface does not exist yet; **FAIL** only when the
surface exists and behavior is wrong; pendings drained with a bound
before probes; rejections assert the exact error code/reason;
screenshots at 390 AND 320. D drives the UI through B's frozen hosts
(`#v2SocialMount` on screen-people, `#v2ClubsMount` on Activities) and
the `.v2so-*` / `.v2cl-*` namespaces for DOM probes.

Journeys (kickoff list, frozen-surface keyed):

- **m4_j1_school_friend** (school-age scenario): classmates group in
  `state.social.groups` lists real `state.people` ids; a C person-overlay
  social action (B-TO-C request 3: spend time / befriend) raises
  `social.closeness` on the person_ref; an invite arrives as stored
  offer — pending kind `social_invite`, `decision_id` echoed in
  `invites[]`; accept via the frozen pending envelope posts at most the
  published cost once (ledger legs); years of no contact fade the
  friendship per A's frozen drift thresholds until `social.kind` drops
  (friend -> acquaintance); the frozen reconcile action restores
  closeness; reload keeps groups/invites/popularity.
- **m4_j2_bully** (school-age scenario): a classmate with
  `social.kind == "bully"` exists (forced within a bound through C's
  bullying arc events — never env injection); each real response — tell
  someone, stand up, avoid, befriend — is dispatchable with the
  advertised dialect and records a player-known memory fact
  (`social.memory`, privacy-filtered); **minor-content probe**: every
  pending actually offered to the minor carries `adults_only == false`
  and no romance kinds (asserted on served decisions, not pool scanning
  alone).
- **m4_j3_coworker** (employed_30 + M3 apply/accept): coworkers group
  derives from employer/role (A); befriend a coworker; M3 employer
  change (resign -> apply -> accept); assert exactly the frozen keep/
  lose behavior for the coworker circle (A's section must freeze ONE
  rule — kept as acquaintance with slowed/off drift, or dropped) and
  that the drift math matches; re-establishing contact post-move works
  through the frozen path.
- **m4_j4_privacy_and_venues**: (a) `social.memory` lines and private
  invite facts from person X never appear in other people's projections
  (server-side audience gating, the M2/M3 player_known filter family);
  (b) 18+ venue probe: a 17-year-old life never receives an
  `adults_only` invite or an adult-club join option enabled (forced
  within a bounded fixture if the served pool is rare); an under-18
  `club_join` on an adults_only club is refused with the precise
  server reason and no mutation; (c) M2.5 regression: romance actions
  stay refused while any party is under 18, exact reason asserted.
- **m4_j5_contrasting_social_starts**: $0 child, graduate, employed
  adult each expose at least one enabled free social/club action and
  legal aging (M3 j7 pattern).

## D3 — File manifest (D-owned only)

- `tools/validate_m4_social.py` — the validator (section D1).
- `tests/content/fixtures/invalid/m4_*.json` — intentionally-invalid
  fixtures (one broken rule per file, named by rule).
- `tests/browser/m4_journeys.py` — journey steps/manifest (frozen
  surface probes only).
- `tools/m4_journeys.py` — runner (reuses the M3 harness).
- `docs/collab/evidence/M4/**` — validator report, journeys md/json,
  screenshots, fix-round evidence.

Not D-owned (no edits): anything in `src/**`, `web/**`, production
content. If a journey needs a change elsewhere, D files a request with
the exact change and owner via Claude.

## Cross-reference requests

**TO A (engine), exact (aligned with B's TO-A list; A's file pending):**
1. Circle derivation freeze: school cohort / program classmates,
   employer+role coworkers; who enters `groups` and when.
2. `env["social_state"]` envelope + `_state["social"]` projection per
   B's shape (D consumes it read-only).
3. Drift thresholds: grow/fade/fall-out per kind, no-contact decay,
   and the **job-change keep/lose rule for coworkers — freeze exactly
   one behavior** (m4_j3 asserts it verbatim).
4. Memory-fact schema + the player_known privacy filter applied to
   `social.memory` (m4_j4 asserts it).
5. Migration: pre-M4 saves validate to the frozen empty default; no
   retroactive circles (D's legacy-save journey re-checks the M3 rule).

**TO C (content/rules), exact (aligned with B's TO-C list):**
1. `club_join {club_id}` / `club_leave {club_id}`: validated,
   replay-safe, adults_only gate with precise reason, per-year fee
   occurrence keys through the one poster.
2. `clubs.json` schema per B-TO-C-2 plus `adults_only` on every row
   (validator check 1/3).
3. Person-overlay social actions: frozen IDs + payload dialect
   (spend time, befriend, stand up to, tell someone about, reconcile)
   advertised through the §Person mechanism.
4. Event prefix + venue data freeze for the validator (D assumes
   `soc_` + a venue/adults_only declaration until C's section lands).
5. Bullying arcs expose the four responses as data-bound options
   (validator check 6).

**TO B (UI), exact:** none new — B's section already freezes the hosts,
namespaces and inbox behavior D probes. One request: keep
`data-person-id` (or an equivalent stable attribute) on person chips —
already implied by the person_ref shape; D's DOM probes navigate by the
projected id.

**FROM D to all:** journeys ship against the frozen surface only; until
`state.social` and `social_invite` land, every M4 step reports honest
PENDING with the owner named — never a fabricated pass.

## Planning self-check

- [x] Read M4 kickoff, B's §UI section and the M3 conventions; section
  recreated in this owner file after the shared-file loss; no
  implementation, no unfrozen schema coded.
- [x] Validator/journey/probe scope names exact owner cross-refs; honest
  PENDING/FAIL semantics preserved; 3-seed + TMPDIR rules apply at
  implementation.
- [ ] Runtime evidence: not applicable to a planning-only pass; required
  at implementation against the frozen contracts.
