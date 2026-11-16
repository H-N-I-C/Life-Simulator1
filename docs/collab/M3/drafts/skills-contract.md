# M3 skills contract — DRAFT for freeze (C-owned; not frozen, no coding on it)

Proposed shapes for the C feature (skills/training) per docs/collab/M3/01-spec.md
and 02-contracts.md. Owners at freeze: C (skills), A approves engine/ledger/hook
fit + event extensions, B approves shell/projection fit, Claude freezes.
Everything below is a proposal with worked examples in skills-examples.json.

## 1. Skill definitions (src/game/content/skills/skills.json)

```jsonc
{
  "schema_version": 1,
  "skills": [
    {
      "id": "practical",               // stable string, never reused
      "label": "Practical skill",
      "range": [0, 100],
      "default": 0,                     // deterministic initial value
      "description": "Hands-on trade competence."
    }
  ]
}
```

Baseline set per the M3 plan: practical, organization, communication, care.
Skills are independent of smarts/looks/appearance. Adding a skill with existing
behavior = adding a data row, never an engine branch.

## 2. Per-person state (validated envelope dictionary, versioned)

```jsonc
// env["skills"] = {
  "schema_version": 1,
  "people": {
    "<person_id>": {
      "values": {"practical": 12, "care": 40},     // only non-default entries required
      "used_this_turn": {"evening_class:practical": 1},  // annual-limit ledger, reset by year_start hook
      "history": [{"turn_id": "t_000042", "action_id": "evening_class",
                   "skill_id": "practical", "gain": 6, "value_after": 12}]
    }
  }
// }
```

- One gain per persisted occurrence (history rows are append-only).
- Successors keep their own skills; the deceased player's entry is read-only.
- Legacy M2 saves backfill ONCE: `default` for missing entries at load, no RNG
  consumed on reads, no age-derived expertise.

## 3. Training actions (src/game/content/skills/training_actions.json)

Declarative, small rule vocabulary (no per-skill engine branches):

```jsonc
{
  "schema_version": 1,
  "actions": [
    {
      "id": "evening_class",
      "label": "Take an evening class",
      "category": "skills",
      "input": {"id": "skill_id", "type": "choice",
                "choices_from": "skills"},        // which skill to train
      "cost": {"money": 150},
      "limits": ["once_per_year_per_skill"],
      "gain": {"randint": [5, 9]},                 // DeltaValue (M2c schema form)
      "learning_rate": {"stat": "smarts", "per": 25, "cap": 2}
        // +1 bonus gain per full 25 smarts, capped at +2 (bounded, declarative)
    },
    {
      "id": "self_study",
      "label": "Study on your own",
      "category": "skills",
      "input": {"id": "skill_id", "type": "choice", "choices_from": "skills"},
      "cost": {"money": 0},
      "limits": ["once_per_year_per_skill"],
      "gain": {"randint": [1, 3]}
    }
  ]
}
```

Rules:
- Annual limit is enforced against `used_this_turn` server-side; saturated
  skills (value == range max) return a typed refusal, never a fake +progress
  journal row.
- Costs post through the frozen M2 money-entry shape, phase `skills`,
  occurrence `{turn_id}:{action_id}:{skill_id}`, leg `cost`; failed
  eligibility spends nothing.
- Free actions exist for every skill (soft-lock rule), so a $0 player can
  still progress slowly.

## 4. Progression curve

- Value 0-100, integer. Gains are bounded ranges (see actions), plus the
  declared bounded learning-rate modifier. No compounding, no per-click
  training: at most one training action per skill per year (plus declared
  job/study contributions, §6).
- Diminishing returns are NOT baked in at M3a (four skills, small ranges);
  if needed later it is one declarative field (`"gain_scaling": {...}`)
  rather than a code branch. Open question Q4.

## 5. How skills gate jobs and programs (frozen queries)

Skills expose read-only requirement evaluation; A (careers) and B (programs)
own their gates and call the frozen query — the skills module never grants a
promotion or credential:

```jsonc
// role/program data (A/B files) reference skills declaratively:
"requirements": {"skills": [{"id": "care", "min": 40}], "qualifications": [...]}
// frozen query: skills.requirements_met(person_state, requirements) -> {met: bool, missing: [{id, min, have}]}
```

Projection shows values plus the concrete linked requirements that are
unmet (id + min + current value), satisfying "inspect while ineligible".

## 6. Annual contributions (job practice / study)

- A's careers hook and B's program hook contribute at their OWN once-year
  hooks via the frozen context/result boundary:
  `{"skill_id": "practical", "gain": {"randint": [2, 4]}, "source": "apprenticeship:<assignment_id>"}`
- Contribution occurrence ids include the assignment/enrollment id, so a
  contribution posts exactly once and never double-counts with manual
  training (separate occurrence namespace).
- Ordering trap (from 05-task): work_income runs before education in the
  frozen M2 order; a threshold crossed in education must NOT trigger a
  promotion in the already-finished work phase — C's hook applies gains only;
  A's promotion evaluation reads values at A's own hook next turn. Stated in
  the freeze manifest as the skill update ordering.

## 7. Event records (frozen M2c event schema + proposed typed extensions)

Skill/training events use the frozen schema; the two gaps to agree with A:
- condition extension: `{"kind": "min_skill", "skill": "care", "value": 30}`
- effect extension: `{"add_skill": {"skill": "care", "gain": {"randint": [2, 5]}}}`
  (sampled once at offer creation per the pre-sampled policy)
Alternative per 02-contracts: keep such transitions in authoritative domain
commands and out of events. C has no preference; A decides. Every authored
event keeps a zero-cost decline/rest choice (soft-lock).

## 8. Commands (freeze IDs now)

- action `train` — payload `{action_id, skill_id}`; validates skill id,
  annual limit, saturation, funds; applies gain; returns RuleResult with
  journal-honest actual delta.
- projection section `skills` — per-person values, remaining annual uses,
  available training with known costs, and unmet linked requirements.
- annual hook `skills_year_contribution` — applies A/B declared
  contributions once per turn (called from the agreed existing phase).

## Open questions for freeze

- Q1: env section name/version key (`skills` vs `features.skills`) — A's
  envelope convention decides.
- Q2: do training actions live in one content file per action or one
  skills-bundle file? (C proposes one bundle: training_actions.json.)
- Q3: event condition/effect extensions (§7) vs domain commands — A decides.
- Q4: diminishing-returns curve needed at M3a? (C proposes no.)
- Q5: exact smarts/trait learning-rate table — freeze numbers with Claude
  (proposal: smarts +1 per 25, cap +2; NERD trait +1, cap +2 total bonus).
- Q6: backfill default for legacy saves — 0 for all four skills unless a
  documented baseline says otherwise (C proposes 0; no age-derived values).
