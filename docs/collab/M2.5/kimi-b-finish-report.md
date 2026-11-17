# Kimi B — M2.5 finish report (2026-11-17)

Scope: M2.5-finish.md tasks B1, B2, B3. Branch `dev` (verified). Files
edited (per ownership): `web/server.py`, `web/static/game.js`,
`web/static/game.html`, `web/static/features/relationship_policy/editor.js`,
`web/static/features/relationship_policy/m25_screens.js` (new),
`src/game/features/relationship_policy/evaluator.py`,
`tests/test_relationship_policy.py`, `tests/test_web_m25_screens.py` (new),
`tests/test_web_m25_browser.py`, plus the contracts README 'Open items'
(coordination, as instructed).

## B3 — dating score is USED (contract published first)

- `evaluator.py`: `evaluate_offer` now returns an additive
  `"acceptance": {"possible": true, "p_accept": round(score/100, 2)}`
  block; hard failures return `{"possible": false}`. `score`,
  `score_breakdown`, `refusal.p_refuse` unchanged (frozen §5 shape is
  additive-only). Refusal stays independent and possible at any score.
- Field contract published to C in the contracts README 'Open items'
  (B → C, 2026-11-17): C draws ONCE per offer
  `accepted = rng.random() < p_accept`, then the independent refusal roll
  with `refusal.p_refuse`; overall acceptance probability
  `p_accept * (1 - p_refuse)`.
- Test: `test_acceptance_probability_tracks_score` (+ hard-fail
  `acceptance.possible` assertions) in `tests/test_relationship_policy.py`.
  PASS (28 tests).
- **Consumed in C's file** (`sample_dating_app` is C's; C1 swaps the
  p_refuse-only draw for the contract above).

## B1 — second-edit 409 fixed; shell refresh/revision callback

- Root cause (Codex review confirmed): `editor.js` sent
  `expected_revision` from the page-load state snapshot and never adopted
  the post-apply revision or refreshed canonical state.
- `game.js`: published `shellHooks = {getState, refresh, sendAction,
  resolveDecision}`. `refresh` re-reads authoritative state and re-renders
  (no page reload; focus preserved); command hooks route feature screens
  through the shell's retry-stable operation machinery.
- `editor.js`: the apply handler now reads the CURRENT revision from
  `shellHooks.getState()` at send time, adopts `out.revision`, calls
  `shellHooks.refresh()` after apply (so displayed names, tree and
  ordinary actions use the new state), re-renders its people list from
  the fresh state, and shows each affected union's **reprojected
  recognition as the new status** (e.g. "Union uni-… is now unrecognized
  (was recognized)") — the display side of A4's service reprojection.
  On `stale_revision` it refreshes the shell so the retry sends the fresh
  revision.
- **Failing-without-fix verified**: restored the exact pre-fix apply
  handler (stale snapshot revision, no adoption/refresh) in a scratch
  edit → `test_second_consecutive_edit_and_m25_screens` FAILS (second
  apply 409s, wait times out). With the fix: PASSES — three consecutive
  applies in one browser session, audits all commit, final value lands.
- Tests: `test_two_consecutive_applies_using_returned_revision_succeed`
  (HTTP: revision chain + replay-exact retry + genuine stale → 409 + no
  mutation on rejection); browser test above (real Chromium, 390×844).

## B2 — M2.5 screens

Server (`web/server.py`, my files only — mutation authority stays in
GameService):

- `GET /api/games/<id>/relationships-view`: read-only projection under
  `service._lock` — the player's unions (union_id, partner descriptor with
  name/age/alive/appearance, kind, status, recognition, ceremony_held,
  prenup_amount, shared dependent children, renewals), the PLAYER
  observer's known facts (kind, subject descriptors, learned turn,
  source), reputation-as-known-to-player, cash, the fictional weddings
  price catalog, and `pending_reasons` — the latter ONLY when
  `knowledge.pending_visible` passes (same predicate as `get_state`, so a
  private offer never leaks through this route either).
- `POST /api/games/<id>/wedding`: typed envelope around A's replay-safe
  `wedding_command` (`plan_wedding` / `renew_vows` / `divorce_union`);
  unknown op / missing revision / non-object args → 400 `invalid_input`;
  service errors pass through typed (e.g. unaffordable → 409
  `not_allowed`, stale → 409 `stale_revision`).

Client (`m25_screens.js`, mounted behind the new "Dating & weddings"
toggle in game.html/game.js; phone-width, keyboard-focusable controls,
avatars on every person via `miniPersonChip`):

- **Dating**: Find-a-match button (disabled with its advertised reason
  when ineligible; hidden while any pending blocks play), and when the
  pending is the dating offer: match portraits, the evaluator's reason
  WORDS (bounded phrases, never numbers — frozen decision 4) and
  Accept/Decline buttons driving `resolveDecision` through the shell.
- **Weddings**: per-union planner with venue/outfit/honeymoon/guest
  controls, live total vs cash, unaffordable combinations disabled with
  the cost reason in the button label, the free configuration
  (courthouse/hand-me-down/no honeymoon) called out as always available;
  vow-renewal tiers with the same affordability gating.
- **Divorce**: per-union, shows the prenup settlement, shared-children
  support preview ($2,000/child, pays min(cash−settlement, support), the
  remainder recorded as owed per A's conservation fix), children's
  portraits; divorce stays legal at $0 (only the settlement needs cash).
- **Reputation/known secrets**: player-known facts about people (label +
  subjects + how learned) and a reputation-as-known-to-player list.

Tests: `tests/test_web_m25_screens.py` (6 tests, real HTTP + real
GameService): view shape + typed 404; wedding route validation; free
wedding costs $0; divorce transfers exactly the prenup once with
replay-exact retry; $0 game rejects a paid wedding typed (no money moved,
no ceremony) while the free configuration succeeds; forbidden-but-
attemptable union shows `forbidden` recognition. Plus the browser test
(tabs render, no phone-width overflow, no crash overlay).

## Coordination / Open items filed (contracts README)

- B → C: `acceptance.p_accept` field contract (above).
- B → A: add `"reasons"` to the v3 pending pass-through list in
  `service._state` (~line 805); until then the dating screen reads
  privacy-gated reasons via `relationships-view`.

## Verification

- Focused: `tests.test_web_m25 tests.test_web_m25_screens tests.test_web_b1
  tests.test_relationship_policy tests.test_web_m1 tests.test_web_m2`
  → 115 tests OK (8 pre-existing skips). Node DOM checks pass.
- Hash seeds 0/1/42 on the new/changed suites: OK ×3.
- `python3 -m py_compile` on changed Python, `git diff --check` clean.
- Full discovery suite: see `00-STATUS.md` / Codex's re-review run.

## Remaining gaps (not mine to close here)

- The complete date→respond journey depends on C1 (dispatch/context/
  offer applier) and A3 (offer/respond domain applier); my UI and routes
  are ready against those contracts.
- Generic decision modal does not yet show reason words (needs A's
  pass-through; filed).
- Editor still exposes internal tenet keys/labels in places and the full
  contrast audit is D's browser-scope work.

## Self-check (docs/collab/00-SELF-CHECK.md)

Logic
- [x] Real GameService/HTTP/browser throughout; no mocks of the service,
  ledger or graph (routes call the real service; browser test is real
  Chromium).
- [x] Failing-without-fix checked: B1 old apply handler restored in a
  scratch edit → browser regression fails (second edit 409); reverted.
  B3 tested directly. B2 money/validation tests fail against the
  pre-fix service behavior by construction (typed 409/404/absence).
- [x] No new random draws; evaluator stays deterministic (p_accept is a
  pure function of score). No set/dict iteration before RNG use added.
- [x] Inputs validated: wedding route (op/args/expected_revision),
  typed 400/404/409; no raw 500s (route tests assert non-500).
- [x] Money: transfers posted once inside A's wedding ops (grouped legs);
  tests assert actual account totals before/after and replay-exact
  retries; free configuration genuinely $0; $0 divorce stays legal.
- [x] Identity: person_ids everywhere; descriptors resolved server-side.
- [x] Save/load: routes read through the service's locked load; replay
  ledger covers wedding/editor commands across reloads.
- [x] Shown costs match application: planner total == ledger posting
  (single occurrence per part, A's code); unaffordable disabled in UI
  AND rejected server-side.
- [x] Age/state gates server-side (A's guards inside wedding/graph ops;
  the $0 game test still had both adults).
- [x] No soft-locks introduced: free wedding config and free divorce
  path always available; decline option untouched.

Hygiene
- [x] Only owned files edited; contracts README 'Open items' per the
  finish-plan instruction; shared status reread.
- [x] No probability/balance changes (p_accept derives from the frozen
  score as published; refusal formula untouched).
- [x] Focused suites green; new/changed suites under PYTHONHASHSEED
  0/1/42.
- [x] UI: real-browser check at 390×844 — no horizontal overflow, no
  crash overlay; buttons are native & focusable; avatars on every person
  chip; contrast reuses the existing styled components (full audit is
  D's scope).
- [x] Rare paths forced: union formed through the real graph command
  with a real scenario adult; $0 affordability forced via the
  unemployed_18 scenario; no reliance on random rolls.
