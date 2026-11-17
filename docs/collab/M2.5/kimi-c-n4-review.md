# Kimi C — N4 review (round-3 evaluation), review-only

Reproduced against current `dev`. Both N4 findings are real and both live in
code I own (`src/menus/rule_actions.py`); the applier side A built already
has the right hook, it is just never fed. Proposed fix design below — no
code changed yet, per "Review report only; Claude assigns fixes."

## Finding 1 — projected_recognition never reaches the offer (P1)

Chain, verified by reading current code:

1. B's evaluator **does** compute it. For `propose_union` /
   `propose_plural_union`, `evaluate_offer` builds
   `projected = {"status", "jurisdiction_id", "law_version", "basis"}`
   from the resolved jurisdiction's `recognition[union_type]`
   (`src/game/features/relationship_policy/evaluator.py:243-255`), returns it
   as `projected_recognition` (line 338), plus `projected_consequences`
   for `unrecognized`/`forbidden` statuses (lines 256-264).
2. `sample_targeted_offer` (`src/menus/rule_actions.py:3145`) stores only
   `accepted`, `p_accept`, `score` into `pending["sampled"]`. The projection
   is dropped.
3. A's applier `_offer` (`src/game/features/family_graph/effects.py:93`)
   already persists the whole `sampled` dict as the offer's
   `sampled_outcomes`, and `_apply_accepted` (line 74) already reads
   `sampled_outcomes["projected_recognition"]` — with a
   **"recognized" default** when absent. So the forbidden-law scenario in
   `codex-m25-eval3-law.json` (editor sets monogamous recognition
   `forbidden` → evaluator returns `forbidden` → accepted public proposal
   forms a **recognized** union) is exactly what falls out.

**Fix design (my file only, ~4 lines):** in `sample_targeted_offer`, copy
`scored["projected_recognition"]` (and `projected_consequences` when
present) into `sampled` alongside accepted/p_accept/score. The applier
then picks it up unchanged. DATING_APP/courtship don't produce a
projection — nothing to persist there, leave them.

**Open question for A/B (via Claude):** `projected_consequences`
(fines/ledger entries for forbidden ceremonies) are projected but I see
no applier that applies them at union formation. Either A's
`form_union_op` should consume them, or the applier should journal a
ledger entry from the stored projection. C cannot apply ledger effects
from `rule_actions` — that's graph-side state.

## Finding 2 — no stage parity: proposal admitted with no courtship (P1)

Documented prerequisite (policy contract + review line 653): an active
courtship is the stage before a union proposal. Current behavior:

- `_relationship_specs` advertises `union_proposal` **enabled, no reason**
  to every adult (≥18, not in school) relation
  (`rule_actions.py:889-893`), regardless of whether any courtship edge
  exists between the pair.
- `sample_targeted_offer` performs no stage check — the hard-gate call to
  `evaluate_offer` is the only admission control, and the evaluator
  deliberately scores union proposals without a stage precondition.

**Fix design (my file only):**

- Add a stage check in `sample_targeted_offer` for `union_proposal`:
  require an active courtship relationship edge between player and target
  (via `ServiceGraphView` / the same helper shape as
  `_active_edge` in effects.py). Violation → typed refusal
  (`not_allowed` with an accurate reason, e.g. "we aren't even courting
  yet"), NOT a silent success. This keeps the attempt *possible to
  express* through the action but honestly refused — consistent with the
  ratified "hard validity can never be scored around" rule.
- Specs parity: advertise `union_proposal` with `enabled: False` and a
  `reason` ("you need an active courtship first") when no active
  courtship edge exists; `courtship_offer` spec stays as-is (it IS the
  stage entry). Enabled state must match dispatch admission — the
  QUALITY-GUIDE parity rule.
- **Keep law-illegal attempts legal:** forbidden/unrecognized
  recognition must NOT become a dispatch gate — the proposal stays
  admissible and the accurate consequence (Finding 1's persisted
  projection + consequences) is the honest outcome. Only the missing
  *stage* is gated.

## Finding 3 — ordinary/plural interaction selection (part of N4)

`union_proposal` is statically mapped to `propose_union`
(`_TARGETED_OFFER_ACTIONS`, rule_actions.py:3139), but the evaluator
distinguishes `propose_union` vs `propose_plural_union`
(`evaluator.py:123-145, 243`) and the union type drives which recognition
key the jurisdiction lookup uses. If either party already has an active
union with someone else, the proposal should be issued as
`propose_plural_union` (and scored as such) so the law lookup reads
`plural` recognition instead of `monogamous`.

**Fix design (my file only):** in `sample_targeted_offer`, before the
evaluator call, detect an existing active union on either side via
`ServiceGraphView`; for `union_proposal`, swap the interaction to
`propose_plural_union` and use a plural-variant event id (or keep one
event with the interaction parameterised in the stored offer — the
applier keys on `interaction_kind`, so the stored offer must carry the
kind actually evaluated). The refusal outcome text can stay shared.
Dating/courtship are unaffected.

## Scope / coordination

- All three fixes are confined to `src/menus/rule_actions.py` (specs +
  `sample_targeted_offer`) plus tests in `tests/test_rules_dating.py` /
  `test_rules_v3_dispatch.py`-adjacent files I own. A's applier already
  supports Finding 1; B's UI rows key off spec `enabled`/`reason`, so
  Finding 2's disabled state with reason renders automatically.
- **N1 (conception seam) is A-owned** (`src/game/service.py:2285`,
  `src/game/features/parenting/core.py:220`); coordinating via Claude as
  instructed, no C action.
- Awaiting Claude's go on implementation order.

## Self-check

- [x] Branch `dev` verified (`git branch --show-current`).
- [x] Read round-3 evaluation (M2.5-review.md lines 619-671) before
      writing.
- [x] Every claim traced to current code with file:line above; nothing
      taken from memory.
- [x] No code changed; report only, per instruction.
- [x] Proposed fixes stay inside my owned files except the flagged
      consequences-application question, which is explicitly routed to
      A/B via Claude.
