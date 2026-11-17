# M2.5 Phase 2 — Kimi B step 1 report: policy data + evaluator

2026-11-17. Task file: docs/collab/M2.5/04-task-kimi-b.md (step 1 publish
was Phase 1; this is the first IMPLEMENTATION step: data module +
evaluator, task item 2 engine half).

## What landed

- `src/game/features/relationship_policy/__init__.py` — feature package.
- `data/policy_catalogs.json` — runtime copy of the FROZEN contract
  `docs/collab/M2.5/contracts/policy-catalogs.json`; a test asserts the
  two stay byte-identical (minus `_status`).
- `catalog.py` — versioned preset access with save-local **merge**
  semantics (override tenets/modifiers/recognition keys win per-key over
  the catalog preset; editing one tenet never erases the rest);
  `PolicyError` typed domain errors (code/message/field/allowed);
  neutral defaults for unknown ids.
- `profiles.py` — personal attraction/preference profiles: orientation
  drawn from the PASSED-IN seeded rng (never global random, never
  inferred from name/gender); culture defaults applied per preset;
  typed `validate_profile_patch` / `validate_tenet_overrides` with
  frozen field allowlist and bounds.
- `evaluator.py` — `evaluate_offer(...)` implementing the frozen
  policy-evaluator.md exactly: hard validity first (`not_adult`, `dead`,
  `self_target`, `unknown_person` — never overridable), then the 12
  bounded contributions with the frozen exact mappings (personal tenet
  full weight + halved public tenet under a personal override, faith
  clamp [-10,10], law_risk by projected recognition, kinship
  acceptance-only, adult age-gap bands, existing-commitments by target
  monogamy preference), reasons at |contribution| >= 3 with label_keys,
  `p_refuse` formula, projected recognition/consequences for union
  proposals. Deterministic — NO rng inside (C owns the draw). Read-only
  `GraphView`/`PolicyView` interfaces injected (A's §5 queries plug in
  at integration; tests use dict-backed fakes — a true boundary). All 7
  frozen v3 predicate functions (`tenet_stance`, `faith_id`,
  `jurisdiction_id`, `recognition_status`, `attraction_band`,
  `openness_band`, `monogamy_band`).
- `tests/test_relationship_policy.py` — 14 tests, all OK under
  PYTHONHASHSEED 0/1/42:
  - runtime-catalog == frozen-contract parity;
  - every executable frozen fixture executed against the REAL evaluator
    (scores, breakdowns, reasons, p_refuse, projected recognition and
    consequences), including the kinship fixture and the name-neutrality
    variants;
  - hard-validity rejection cases (17-year-old target AND initiator,
    dead, self-target, unknown id) — no score returned;
  - profile creation reproducibility from the same seeded rng,
    culture defaults, all typed validation errors;
  - save-local override merge and neutral fallbacks; predicate bands.

## Contract correction (flagged, minimal)

Executing the frozen fixtures against the implementation exposed one
pre-freeze arithmetic error IN THE CONTRACT, not the code: fixture
`illegal_but_attemptable_plural_union` summed faith_tenets to −12
(plural −6 + interfaith −6), which violates the frozen per-contribution
bound [-10, +10]; the clamped value is −10 and the total is 12, not 10.
Corrected `policy-fixtures.json` in place with a `_detail` note
documenting the change and this report. No shape changed: bound,
mappings, inputs and expected classes are untouched. Please ratify at
review; the alternative (raising the faith bound) would be a shape
change through you.

## Decisions made within frozen bounds

- Save-local overrides MERGE over presets per key (the frozen
  resolution_order reads naturally as merge; replace-semantics would
  make single-tenet edits destructive). Documented in catalog.py; happy
  to take a ruling if you intended replace.
- `evaluate_offer` never calls A's guard; it mirrors the checks
  defensively and returns `not_adult` etc. as hard failures. A's
  `graph_assert_adult_romantic` remains authoritative at the transition
  layer (A stamps offers).

## Not in this step (next steps)

- Step 2: editor domain commands (preview/apply/audit, policy_revision,
  supersession hook request), conversion/profile-change service adapter.
- Step 3: editor UI under web/static/features/relationship_policy.
- Step 4: integration requests to A (service/coordinator registration,
  on_policy_revision_bump; ledger `legal` category confirmation).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real objects: real catalog data + real evaluator; graph/policy
  reads injected through boundary interfaces (fakes only at that
  boundary).
- [x] Failing-without-fix: fixture tests fail if any mapping/clamp is
  altered (verified by mutating contributions during development);
  catalog parity test fails if runtime data drifts from the contract.
- [x] Randomness: the only draw (orientation) uses the passed-in seeded
  rng; evaluator is pure; no set/dict-order dependence before any draw
  (orientation draw indexes a tuple by int(r * 4)).
- [x] Inputs validated: unknown interaction kinds, bad profile fields,
  out-of-bounds floats, bad stances and unknown person ids give typed
  PolicyError / hard codes, never raw exceptions.
- [x] Money: no money handled in this step; projected consequences are
  forecast labels with amount ranges only (single-posting rule lives in
  C's resolution path).
- [x] Identity: person_id only; no names used anywhere in scoring.
- [x] Save/load: not touched in this step (step 2).
- [x] Costs/eligibility: law/tenets/kinship/age-gap are soft only;
  illegal-but-attemptable fixture passes with hard.ok true.
- [x] Age gates: not_adult is a hard failure for either participant at
  offer scoring; refusal stays possible at every score.
- [x] No soft-locks: evaluator never gates the free-exit rule; refusal
  possible at max score.

Hygiene
- [x] Only my namespaces: src/game/features/relationship_policy/**,
  tests/test_relationship_policy.py, and the flagged fixture correction
  in contracts/ (awaiting ratification).
- [x] No probability/balance changes beyond the frozen mappings.
- [x] Focused tests pass under 3 PYTHONHASHSEED values.
- [x] UI: no UI in this step (step 3).
- [x] Rare paths forced: hard-rejection fixtures invoke not_adult for
  BOTH orderings, dead, self-target and unknown-id directly; no
  random-roll reliance.

---

# Step 2 addendum: editor domain + conversion (2026-11-17)

Task item 2 (conversion/profile changes) engine half + task item 3/4
editor domain (UI is step 3).

## What landed

- `editor.py` — frozen editor-protocol.json implemented:
  - `Editor.preview(commands)` — validates ALL commands (typed errors
    with command_index/code/field/allowed), reports affected people
    (incl. believers affected by faith-profile edits, skipping those
    with personal overrides on the edited tenets), affected unions with
    current-vs-projected recognition via the injected graph view,
    policy_revision_delta, journal preview. Never mutates.
  - `Editor.apply(commands, turn)` — atomic (revalidates everything
    before mutating), appends frozen EditorAuditEntry records
    {command, scope, target_id, before, after, turn}, bumps
    policy_revision exactly when faith/jurisdiction policy data
    changed, fires the injected supersession hook once (A's
    on_policy_revision_bump replacement) with affected persons and
    jurisdictions.
  - Commands: edit_person_attributes, edit_personal_tenets,
    edit_faith_profile, edit_jurisdiction (recognition map + typed
    consequences with max_per_occurrence_key and bounded fine ranges),
    rename_person (name-only; the service adapter forwards it to A's
    person record — names live in A's domain).
  - Protected fields (age/DOB/chronology/person_id/consent/ancestry/
    RNG/pending/ledger) rejected by pattern with code
    `protected_field` before anything else runs. Money edits do not
    exist (no frozen ledger-adjustment command).
  - `convert_profile(person_id, target_faith_id)` — typed conversion
    with audit entry; community reactions stay C's content.
  - Save-local overrides stay per-key MERGE entries (step-1 decision);
  jurisdiction edits bump law_version per override.
- 12 new tests (26 total, all OK under PYTHONHASHSEED 0/1/42):
  preview error paths with no mutation; atomic apply (invalid second
  command leaves state and audit untouched); audit before/after values;
  personal-tenet isolation between two believers; faith-scope listing
  (override-holders excluded); revision bump + hook firing + tenet
  merge preservation; evaluator observing editor changes end-to-end
  (override halving shows up in faith_tenets); jurisdiction
  reprojection + law_version bump + read-time merge; typed errors for
  bad scopes/ids/recognition values/empty names; conversion typed
  errors and audit.

## Self-check delta for step 2

- [x] Real objects: Editor runs against a real policy_state dict; only
  the graph view is injected (boundary).
- [x] Failing-without-fix: atomicity/preview tests fail if apply
  mutates before validating; merge tests fail under replace semantics.
- [x] No RNG in the editor (deterministic validation/apply).
- [x] Inputs validated: every command/scope/target/patch field is
  typed; protected patterns rejected with typed codes.
- [x] Money: none handled; jurisdiction consequences are validated
  data, posted (once per occurrence key) by C's resolution path.
- [x] Identity: person_id only; rename never touches ids.
- [x] Save/load: policy_state is a plain dict (pickle-safe); revision/
  audit fields additive — service-envelope persistence lands with A's
  registration (step 4 request).
- [x] No soft-locks: editor cannot gate actions; supersession resumes
  safe continuations via A's hook.

---

# Step 3 addendum: editor UI (2026-11-17)

- `web/static/features/relationship_policy/editor.js` — the in-play
  editor screen: person picker (alive people + "Local laws"), per-person
  form (openness/jealousy/monogamy, faith conversion via typed
  convert_profile command, rename, personal-tenet selects showing
  effective stance), law form (per-union-type recognition selects),
  preview panel (affected people/unions, revision delta, typed error
  list) and apply with request_id/expected_revision (frozen M2 replay
  semantics) + duplicate-submit guard. Numeric scores never shown
  (frozen decision 4). Speaks the frozen preview/apply shapes; the
  server endpoints are A's registration (request below). Lazy-loaded
  from game.js via a "Character editor" button in the General card;
  distinct from dev scenario starts.
- `game.html`/`game.js` — editor button + dynamic import wiring.
- `editor.py` — added the frozen convert_profile as a first-class
  editor command (validation + audit + no policy_revision bump: a
  personal conversion is not shared policy data).
- Tests: 2 HTTP static/wiring tests (module served with frozen routes;
  page wiring), 1 node import/export smoke test, 1 new convert_profile
  command test (27 policy tests, 61 incl. web_m2 — all OK x3 hash seeds).

## Step 4 — integration requests to A (routed by Claude)

1. Persist `policy` (policy_state dict from
   `editor.new_policy_state()`) in the save envelope; migrate old saves
   to a default state on load; stamp new persons' profiles via
   `profiles.new_profile(catalog, game_rng, culture_id)`.
2. Endpoints on the shared service (frozen editor-protocol routes):
   `GET /api/games/<id>/policy-state` (policy_state + catalogs subset +
   people roster), `POST .../editor/preview`, `POST .../editor/apply`
   (with request_id/expected_revision replay + revision semantics);
   apply drives `relationship_policy.editor.Editor` and appends
   `editor_audit` (A owns the envelope write per graph-contract §2).
3. Rename forwarding: apply's rename_person audit entry updates the
   person's display name via A's person record (names live in A's
   domain); person_id untouched.
4. Supersession: A implements the hook my Editor fires after a
   policy-changing apply (frozen filter: interaction_kinds
   propose_*/convert_request + affected_person_ids/jurisdiction) —
   supersede matching pending offers, keep attempts/cooldowns/
   pre-effects, resume continuations, never resample.
5. Evaluator graph views: A's read-only queries backing
   `evaluator.GraphView` (graph_person incl. gender if available —
   the same-gender tenet needs it — otherwise profiles carry it).

## Self-check delta for step 3

- [x] UI: native controls (keyboard works), apply button takes initial
  focus after preview, no fixed widths (flex-wrap, phone-safe), muted/
  error colours reuse the >=4.5:1 palette; every person in the picker
  is rendered by name (portrait rendering via the existing chip path
  is A's tree/roster work — the editor deliberately uses compact
  name buttons).
- [x] No numeric scores anywhere (frozen decision 4).
- [x] Secret safety: the editor never displays observer knowledge or
  secrets; it edits out-of-character policy data only.
- [x] Duplicate-submit guard on Preview→Apply (buttons freeze for the
  attempt and re-enable on failure).

---

# Step 4 addendum: editor HTTP routes + end-to-end (2026-11-17)

- `web/server.py` — three routes per A's published GameService
  signatures (README Open items):
  - `GET /api/games/<id>/policy-state` — `_policy_state_payload` merges
    A's `get_policy_state` with the catalogs subset (faiths with labels
    and tenets; jurisdictions with labels and recognition maps) and the
    people roster (id/name/alive). Placed BEFORE the generic game-state
    route so the suffix is not swallowed by the 404 path.
  - `POST .../editor/preview` — delegates to `service.editor_preview`
    (dry-run on a copy; typed `invalid_input` for non-list commands).
  - `POST .../editor/apply` — validates request_id/expected_revision at
    the route, delegates to `service.editor_apply` (replay-exact
    retries, stale_revision 409, rename forwarding, audit mirror,
    supersession hook, envelope persisted once by A).
  Handlers convert GameError exactly like the other API errors; any
  unexpected exception goes through the crash reporter.
- `tests/test_web_m25.py` — 9 HTTP tests over the real server+service:
  payload shape, unknown-game typed 404, preview valid (save untouched),
  protected-field preview errors, non-list commands 400, apply with
  byte-identical replay, stale revision 409, rename forwarding to the
  person record (identity preserved), jurisdiction apply bumping
  policy_revision and persisting the override. All green x3 hash seeds.
- `tests/test_web_m25_browser.py` — real browser smoke (headless
  Chromium over CDP, phone viewport): opens /game, launches the editor,
  picks the player, edits openness, previews, applies, then verifies via
  the API that the value persisted and the audit recorded, and that the
  global error boundary never fired. Green.
- Regression: all B suites (120 tests: policy, web m1/m2/m25/b1,
  render) + both node suites pass.

The editor is now playable end to end. Remaining B-side M2.5a evidence:
a minimal real adult offer showing changed acceptance via C's offers
(evaluator is integrated on my side; C's offer creation consumes it).

---

# Step 4b addendum: family-tree hooks for A (2026-11-17)

Per A's step-3 report hook request (exact):
- `GET /api/games/<id>/family-tree[?focus=<person_id>]` in web/server.py —
  JSON passthrough to `service.get_family_tree(game_id, focus_id=focus)`,
  placed with the other per-game GET routes; typed GameError passthrough
  (unknown focus → 404) and crash reporting like its siblings.
- "Family tree" button in the game page's General card: lazy-imports
  `/static/features/family_graph/family_tree.js` and calls
  `mountFamilyTree(card, state.gameId)` into a toggleable card
  (`#familyTreeCard`), same pattern as the editor button. Bug caught by
  the browser smoke: a freshly created element starts un-hidden, so the
  first toggle hid the card — fixed by initialising `hidden = true`.
- Tests: 3 new HTTP tests (passthrough payload with appearances, focus +
  unknown-focus 404, page wiring) and the browser smoke now also opens
  the tree card and asserts content renders with the error boundary
  silent. Full B suites: 122 tests OK (3 hash seeds on the new suites).
