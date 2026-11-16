# M3 careers/employers contract — DRAFT for freeze (A-owned; not frozen, no coding on it)

Proposed shapes for the A feature (named careers/employers) per
docs/collab/M3/01-spec.md and 02-contracts.md. Worked examples in
careers-examples.json; content data in careers-roles.json /
careers-employers.json. Owners at freeze: A (careers), B approves
shell/projection fit, C approves skills-query usage, Claude freezes.
Everything below is a proposal; no parallel implementation until marked
FROZEN.

## 1. Data files

- `src/game/content/careers/roles.json` — all 12 roles (schema §2).
- `src/game/content/careers/employers.json` — 6 fictional employers
  (schema §3).
- One file per family (consistent with C's proposed bundle style).
- Stable string IDs, never reused across catalog versions; IDs are
  independent of labels and file order.

## 2. Role definition (roles.json)

```jsonc
{
  "schema_version": 1,
  "roles": [
    {
      "id": "electrician",                 // stable string
      "label": "Electrician",
      "path": "electrical",
      "level": 2,                          // 1=entry ... 4=top
      "entry": false,
      "requirements": {
        "min_age": 18,
        "qualifications": ["electrical_apprenticeship"],
        "skills": [{"id": "practical", "min": 30}],
        "legacy_education": "secondary"    // accepted legacy generic records
      },
      "pay": {"min": 52000, "max": 68000, "hours": 38},
      "promotion_edges": [
        {"to": "senior_electrician",
         "criteria": {"tenure_years": 3, "performance_min": 55,
                      "skills": [{"id": "practical", "min": 55},
                                 {"id": "organization", "min": 25}]}}
      ],
      "look_override": "mechanic",          // optional; M0b manifest key
      "event_ids": ["electrical_safety_scare", "tool_theft_offer"]
    }
  ]
}
```

Rules:
- `requirements.qualifications` reference B's credential IDs; the
  authoritative check is B's frozen query (award records, not enrollment).
- `requirements.skills` use C's frozen query `skills.requirements_met`.
- `legacy_education` declares exactly which generic M2 academic records
  satisfy the gate ("secondary" | "any_degree" | null) — never invent a
  named major from generic university.
- `pay` bounds the sampled offer salary (integer, min<=max); offers are
  sampled once at offer creation and stored, never rerolled on reload.
- `look_override` must validate against the FROZEN M0b manifest
  vocabulary; unknown keys behave as absent (regular portrait fallback).
  Leaving a role clears the effective context look; base Appearance,
  person_id and badges are never edited by employment.

## 3. Employer definition (employers.json)

```jsonc
{
  "schema_version": 1,
  "employers": [
    {
      "id": "voltaic_services",
      "name": "Voltaic Services",
      "sector": "electrical",
      "role_ids": ["apprentice_electrician", "electrician",
                   "senior_electrician", "site_supervisor"],
      "hiring": {"open_roles": ["apprentice_electrician", "electrician"]}
      // open_roles rotates by seeded annual event data (M3c content);
      // applications to a closed role are rejected with a typed reason.
    }
  ]
}
```

Employers are organizations with stable IDs — never person_ids, never
fake coworker people. Portraits for employers are not a thing; only the
player's context look derives from their active assignment.

## 4. Feature state (validated envelope dictionaries)

```jsonc
// env["features"]["careers"] = {
  "schema_version": 1,
  "assignment": {               // null when unemployed
    "assignment_id": "as_0007",
    "person_id": "<player person_id>",
    "role_id": "electrician",
    "employer_id": "voltaic_services",
    "accepted_turn": "t_000031", "accepted_age": 24,
    "tenure_turns": 0,
    "salary": 61000, "hours": 38,
    "performance": 50, "stress": 30,
    "promotion_history": [{"role_id": "electrician", "turn": "t_000031"}],
    "legacy_generic": false
  },
  "offers": [                   // stored sampled offers, never rerolled
    {"offer_id": "of_0003", "role_id": "electrician",
     "employer_id": "voltaic_services", "salary": 61000,
     "sampled_turn": "t_000030", "status": "accepted",
     "expires_after_turn": "t_000032"}
  ],
  "application_attempts": {"t_000042": 2}
// }
```

- ONE salary authority: the adapter mirrors assignment salary/hours into
  the legacy M2 fields (`has_job`, `salary`, `job_hours`, `performance`)
  on transitions ONLY, so the frozen M2 payroll/pension/quote path keeps
  working untouched. Two independently editable salaries are forbidden;
  legacy fields are written by the declared adapter, never by feature
  code and M2 rules in the same turn.
- `tenure_turns` increments at the work_income hook; promotion criteria
  read tenure/performance at A's own hook (work_income), after C's skill
  contributions from the PREVIOUS turn's phases have landed (frozen
  ordering, see §7).
- Legacy M2 generic employment migrates to
  `{"legacy_generic": true, "role_id": null, "employer_id": null}` —
  pay/performance/tenure preserved, no invented employer history.

## 5. Commands (freeze IDs now)

- action `job_apply` — payload `{role_id, employer_id}`; validates
  opening, requirements (qualifications via B query, skills via C query,
  age, funds none), attempt caps; samples the offer (salary in role pay
  range, weighted by performance) and stores it; returns the offer as a
  stored pending decision (`kind: "job_offer"`).
- resolve `job_offer` — option `accept` / `decline`; accept performs the
  assignment transition through the adapter (atomic with the command);
  decline is free and the offer tombstones. Reopening the page never
  rerolls a stored offer.
- action `job_promotion_apply` — payload `{role_id}` (target of a
  promotion edge from the current role); validates edge criteria
  (tenure, performance_min, skills sampled at attempt); bounded attempts
  per year (`application_attempts`); success transitions the assignment
  at the NEXT turn boundary (pay quote changes next year per the frozen
  timing — never a second credit this year).
- action `job_transfer` — like apply, but preserves mapped tenure per
  the frozen tenure rule (default: tenure resets to 0 on transfer;
  salary band may improve ±10%).
- action `job_quit` — leaves employment; look_override clears; M2
  lose_job rules apply (stress reset etc.).
- Promotion is never automatic; dismissal uses the existing M2
  performance rules at the work_income hook.

## 6. Projection (additive under Work)

`work.career = null | {assignment fields + role label/level/path,
employer name, next_requirements: [{kind, id, label, have, need, met}],
openings: [{role_id, employer_id, label, pay_range, requirements_met,
missing}]}`. Job loss restores the personal portrait (effective context
look absent). Elders/retired show pension as today (M2 authority).

## 7. Annual hook order (freeze manifest)

work_income (A hook: tenure++, promotion evaluation, payroll quote via
M2 path) runs BEFORE education/skills contributions in the frozen M2
order. Consequences, frozen: a skill gained in education this turn
cannot trigger a promotion until A's hook NEXT turn; C applies skill
gains only; A reads values at A's hook. No phase moves to make tests
convenient. Skills annual effort resets at year_start (C's hook).

## 8. Finance

- All wages keep flowing through the frozen M2 payroll quote (adapter
  sets legacy salary; the existing gross/tax/expenses/net legs post
  once). No career-side wage poster.
- Fees (application fee if any, training via C) use the frozen
  money-entry shape: `{phase, occurrence_id, leg, account_owner_id,
  category, amount, description, group_id?}`; career phase is
  `work_income` for employment-linked fees, `careers` for application
  fees; occurrence ids embed offer/assignment ids
  (`{turn}:{offer_id}:fee`) so retries cannot double-post.
- Paid apprenticeship: ONE wage quote (the apprentice role's pay) + B's
  education progress hook; no second stipend, no generic-job double
  pay. Apprenticeship cancellation consequences frozen with B (default:
  assignment ends; accrued tuition debt stays).

## 9. Migration

- Legacy generic job -> legacy_generic assignment (pay/tenure intact).
- Legacy generic university -> satisfies `legacy_education` claims only
  ("secondary" for graduates; "any_degree" for uv completers); never a
  named major.
- Missing skills backfill once at default 0 (C's rule; no rerolls).
- Accepted offer terms snapshot in the offer record and are honored
  after catalog edits; unknown role/employer IDs in old saves become
  explicit tombstones (legacy_generic), never silent role swaps.

## 10. Answers to C's skills questions (skills-contract.md)

- **Q1 (env section name):** A's convention: namespaced feature
  envelopes — `env["features"]["skills"]` and `env["features"]["careers"]`,
  each a versioned dict (`schema_version` inside). Keeps the top-level
  envelope stable and the migration registry simple (one features
  subtree to validate/backfill).
- **Q2 (training action files):** one bundle `training_actions.json` is
  fine — consistent with careers' one-file-per-family.
- **Q3 (event extensions vs domain commands):** A approves the two
  BOUNDED typed extensions to the frozen M2c event schema:
  condition `{"kind": "min_skill", "skill": "<id>", "value": N}` and
  effect `{"add_skill": {"skill": "<id>", "gain": <DeltaValue>}}`,
  sampled once at offer creation per the pre-sampled policy.
  Credential awards, promotions and enrollments stay in authoritative
  domain commands (B/C feature actions) — events may adjust skills and
  stats but never grant credentials or jobs. Flags cannot bypass the
  qualification/payroll gates.
  (Q4-Q6 are C/Claude numbers; A has no objection to C's proposals.)
