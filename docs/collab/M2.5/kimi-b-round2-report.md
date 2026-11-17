# Kimi B — M2.5 fix round 2 report (2026-11-17)

Scope from `M2.5-fix-round-2.md`: R4 (personal-editor supersession, B
half), R5 (UI journey half), R3 (B's side of the ratified probability
contract: evaluator field + docs). Branch `dev` verified. QUALITY-GUIDE
followed; Codex took R1/R2/R6/R10 + the smaller items (I verified their
wording/gating fixes are in place and left them untouched).

## R3 — evaluator side of the ratified probability contract

Ratified contract (Claude, fix-round-2): one sampled-once draw per offer;
`accept = (u < p_accept)`; refusal traits live INSIDE p_accept;
`p_refuse = 1 - p_accept` display-only; hard validity is not a
probability.

- `evaluator.py`: `p_accept = clamp(round(score/100 -
  (0.4*(1-openness) + 0.2*jealousy)/2, 4), 0.02, 0.98)` — the frozen §3
  score table is untouched; the trait drag uses the previously ratified
  trait weights at half strength inside the probability. The [0.02, 0.98]
  clamp keeps refusal always possible and acceptance never impossible
  (soft modifiers are never bans). `refusal.p_refuse = 1 - p_accept`.
  Hard failures: `acceptance.possible: False`.
- Docs: `policy-evaluator.md` gains §3a (ratified contract; old §3
  refusal paragraph kept labeled as history); §5 result shape shows the
  `acceptance` block; §7 C question updated to point at §3a. Contracts
  README B→C item rewritten to the ratified single-draw contract
  (supersedes my earlier two-draw proposal).
- Fixtures: `policy-fixtures.json` refusal values recomputed with the
  real evaluator from each fixture's own inputs (6 values changed;
  scores/breakdowns identical), `_status` updated to say so — the old
  values pinned the pre-R3 formula.
- Consumption (`rule_actions.sample_dating_app`) is C's R3 item.
- Tests: rewritten `test_acceptance_probability_tracks_score` (monotone
  in score, clamp bounds at both trait extremes, refusal = 1 − p_accept);
  hard-fail fixtures assert `acceptance.possible` is False. 29 tests OK.

## R4 — personal-editor supersession (B half DONE, A half filed)

- `editor.py`: `apply` now bumps `policy_revision` and fires the
  supersession hook for personal edits (`edit_person_attributes`,
  `edit_personal_tenets`, `convert_profile`) with exactly the affected
  person ids (shared-scope edits keep the all-profiles list);
  `rename_person` alone stays revision-neutral. `preview` advertises the
  same `policy_revision_delta` so the UI shows what apply will do.
- Tests: new `test_r4_personal_edits_bump_revision_and_fire_hook`; old
  tests that pinned the buggy behavior updated
  (`test_apply_person_attributes_audits_before_after`,
  `test_convert_profile_command_via_apply`,
  `test_preview_valid_does_not_touch_save`) — the review explicitly
  declares "policy revision stays 0" a defect.
- **A half filed** (contracts README Open items): `service._editor_supersede`
  must also cancel actual `env.pending` dating/offer pendings whose bound
  target is affected (it only forwards graph offers today; the dating
  pending is not a graph offer), preserving attempts/cooldowns and
  keeping the year continuable. Until A lands it, the hook fires
  correctly but a live dating pending survives a target-attribute edit.

## R5 — UI journey half

- `web/server.py` `relationships-view`: additive `relationships` (active
  edges: kind, other person, visibility, exclusivity, chemistry/trust/
  satisfaction) and `offers` (pending graph offers involving the player:
  direction incoming/outgoing, interaction kind, other person,
  created/expiry turns). Read-only; descriptors reuse the existing
  privacy-safe person projection.
- `m25_screens.js` dating tab: per-target **Ask out** (`courtship_offer`)
  and **Propose a union** (`union_proposal`) rows rendered ONLY from
  explicitly enabled `available_actions` entries (spec/dispatch parity —
  disabled actions show their reason); courtship edge list with chemistry;
  "offers in flight" list (incoming = answer via the decision dialog,
  which drives the stored `respond` domain effect — no unrestricted
  accept endpoint exists or was added); weddings/divorce tabs then operate
  on the unions those authorized offers form.
- **C item filed** (Open items): implement the two context self actions
  (per-target `dating_app` variants: score via `evaluate_offer`, one
  seeded R3 draw, persist in `pending.sampled`, A's `offer` applier
  records/resolves; `union_proposal` stores `projected_recognition` in
  `sampled_outcomes` and requires an active courtship edge).
- **A items filed/owned elsewhere**: graph/legacy `player.partner` sync
  on graph divorce (A's half of R5) stays with A.
- Test: `test_r5_view_exposes_relationships_and_offers` — real
  `start_relationship` + `record_offer` graph commands through the
  service, asserted through the HTTP view.

## Smaller B items (Codex)

Verified in place, untouched: adult-rule wording on the dating tab and
explicitly-enabled Find a match with reason
(`m25_screens.js:131-149`).

## Verification

- Focused: `test_web_m25_screens` (7), `test_relationship_policy` (29),
  `test_web_m25`, `test_web_b1` → all OK; browser suite
  (`test_web_m25_browser`, real Chromium 390×844) OK.
- PYTHONHASHSEED 0/1/42 on the changed suites: OK ×3.
- `py_compile` on changed Python, `node --check` on the changed JS,
  `git diff --check` clean.
- Full discovery suite: run after these changes (see repo state at
  Codex's re-evaluation; concurrent agents were editing).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real objects: fixtures recomputed through the real evaluator;
  view tests through real HTTP + GameService graph commands; no mocked
  service in my tests.
- [x] Failing-without-fix: old fixture refusal values fail against the
  ratified formula (6 diffs, then recomputed); R4 tests assert the
  revision bump the old code did not perform; R5 view test fails if the
  new fields are absent.
- [x] Randomness: evaluator remains deterministic; no new draws; the
  single draw stays C's seeded offer sampling.
- [x] Inputs validated: no new mutation routes (view is read-only);
  existing typed validation untouched.
- [x] Money: no new money paths; wedding/divorce previews unchanged.
- [x] Identity: person_ids only.
- [x] Save/load: view reads the persisted envelope under the service
  lock; editor revision bump persists via the existing envelope save.
- [x] Costs/eligibility parity: R5 buttons render only from explicitly
  enabled action specs.
- [x] Age gates: 18+ wording explicit in UI; guards stay server-side (A).
- [x] No soft-locks: R4 hook preserves attempts/cooldowns (A implements
  the pending cancellation with that requirement); no new pendings.

Hygiene
- [x] Only owned files edited (evaluator/editor in relationship_policy,
  web/**, my tests, contracts docs per instruction); shared status
  reread.
- [x] Probability change ONLY the ratified R3 contract (fixture values
  recomputed, not hand-tuned).
- [x] Focused suites green under 3 hash seeds.
- [x] UI: existing patterns reused (native controls, chips with avatars,
  phone-width rows); full keyboard/contrast certification is D's scope.
- [x] Rare paths forced via real graph commands and real scenarios, no
  random rolls.
