# M3 contracts — FROZEN v1 by Claude 2026-11-20 (§Careers A, §Skills C, §Education B, §Content validation D). Changes only via an amendment note approved by Claude.

Owner for freeze: A (engine/career/ledger hooks), B (education/frontend shell), C (skills), D (content validation); Claude resolves/approves cross-feature decisions. No parallel implementation before one agreed example set/schema version is marked FROZEN. These are proposed semantics/field requirements, not a new wire format silently replacing accepted M2.

## Feature boundaries and authoritative state

- **Career:** assignment_id, person_id, role_id, employer_id, accepted_age/turn, role tenure, employment status, salary terms and promotion history. Assignment belongs to one person; employer IDs belong to organization data, never person_id. Stable role/employer IDs are strings independent of names/catalog order.
- **Education:** enrollment_id, person_id, program_id, started_turn, completed annual credits/years, progress/grades, status; separate immutable earned-qualification records with qualification_id/program version/award turn. One enrollment completion cannot award twice.
- **Skills:** per-person skill_id → value0–100, training/annual use records and progress history as needed. Derive labels from versioned data. Real deceased NPCs are read-only, and successors keep their own skills/credentials, not the deceased player's.
- **Compatibility:** choose the authoritative relationship between new assignment state and M2 has_job/salary/job_hours/performance fields. Preferred: named-assignment metadata plus one declared adapter updating legacy salary/job state on transitions; forbid two independently editable salaries. Existing M2 pension behavior remains the payroll authority. Course progress and old uv_years similarly need one bridge; old enrollment is not advanced by two systems.

Keep feature state in validated versioned envelope dictionaries or an agreed serializable model, not arbitrary new pickle globals. Additive projections under Work/Education/Skills preserve existing M2 fields until adapters migrate. Feature commands route through the existing validated action/resolve envelope and transaction/replay boundary; no feature-specific HTTP server.

## Data families (owners must publish valid examples)

**Careers/employers (A):** schema_version, roles with stable ID/label/path/level, allowed employer types, qualification and skill prerequisites, pay range/base hours, promotion edges/criteria, portrait-look key (optional), contextual event IDs. Employers have ID/name/sector and role availability. Openings/offers get persisted occurrence IDs and sampled salary/acceptance data; do not persist a transient array index.

**Programs (B):** stable ID/label, credential awarded, duration/progress units, prerequisites, cost/funding schedule, study commitment, optional linked paid apprentice role, progression/failure/leave rules, accepted skill contributions and event IDs. Degrees and trades are data, not extra hardcoded menus. Tuition/cash/debt liability timing must be explicit; funding advances principal only once at the agreed time.

**Skills (C):** stable ID/label, range/default, compatible training action IDs, costs, annual limits, bounded progress rule and role/program uses. Pick a small supported declarative rule vocabulary; adding a skill with existing behavior should not require another engine branch.

**Event records:** use the *frozen M2c event schema*, not a career-only fork. The draft lacks qualification/skill/role/employer predicates and domain commands to award credentials/promote/enroll; A must approve bounded typed extensions or keep such transitions in authoritative domain commands. Content must not set arbitrary model fields, call Python/eval, write a salary or directly grant a credential with unrestricted set_flag.

Event reference binding uses person_id for people and separate organization/role IDs. No fake person portraits for an employer name. Validate job/event foreign keys and eligibility; flags cannot bypass mandatory qualification, age, funding or payroll gates.

## Suggested extension interfaces to agree once

Freeze command IDs, argument schemas, event result/money-entry shapes, projection examples, annual hook order and migration registry for each feature. Owner modules provide validation/action specs/apply/resolve/annual hook and projection through the M2 context/result boundary; no nested self-saving or direct calls into interactive legacy functions.

A's integration registry names each feature hook once in existing phases. Education progresses once in education; skills finish their once-year effort contribution at the agreed hook; careers evaluate performance/promotion and payroll in existing work_income. Define whether newly awarded credentials qualify this turn or next under frozen M2 ordering (work before education), without moving phases to make tests convenient. A freeze manifest must state skill update ordering too.

B's shared shell loads feature modules with a declared mount/state/action adapter and CSS namespace. A owns Work component files, B Education files, C Skills files; each exports through agreed registration rather than editing game.js three ways. Per-feature data files avoid shared content arrays. Shared dispatcher/adapter registration requests are applied by their named owner in small integration commits.

## Finance invariants

All wages/tuition/training/fees/loan payments use M2's frozen `{phase, occurrence_id, leg, account_owner_id, category, amount, description, group_id?}` request and one poster. Signed integer amounts. Lifetime IDs and assignment/enrollment/offer occurrences distinguish same-turn fees and account owners. Reusing a posting key with different data is a conflict. Group borrowing, tuition and liability updates into the same atomic command. Previewing an offer/program never samples or posts money.

Paid apprenticeship has one wage quote in payroll and one education progress hook; it must not also receive generic job income or a second stipend. Promotion changes the *next scheduled* pay calculation. Existing pensions and job salary histories have documented interaction with a new assignment. Honor accepted M2 negative-balance/debt rules; don't introduce hidden affordability clamps.

## Look override — context, not cosmetic mutation

A career data may declare `look_override` only from the frozen M0b manifest vocabulary (examples in the brief include health worker/mechanic/scientist; existence still requires manifest validation). During projection, derive an effective context look from active qualifying assignment and age. Keep base/skin/hair/badges/background unchanged. Role leave/dismissal returns to normal look; no purchase needed to recover appearance.

Freeze priority with M0b owner: temporary life-event presentation (e.g. wedding/graduate) versus active job context versus personal look, with a defined end condition. Do not implement competing overrides in creator, server and client. Existing badge/headscarf/age compatibility follows the manifest's fallback chain; use regular portrait if no valid job combination. Save identity and base Appearance exactly; any stored override must have a validated source/lifetime and be cleared by the domain transition. Creator age preview never changes employment.

Provide examples for active nurse, apprentice without a special asset, unemployed former nurse, an elder employee, person with headscarf/badges and temporary graduation look. The preview must not select nonexistent files or make all employed people look identical through an overbroad override.

## Migration and recovery policies to decide before freeze

- Legacy generic employed M2 life retains its pay/performance/tenure as a clearly labeled legacy generic assignment, or receives an explicitly agreed mapped entry. Never invent a named employer history/qualification from old salary alone. Offer transition into new jobs through normal actions.
- Legacy generic university/completed study must be preserved as generic/unspecified academic history. Specify exactly which prerequisites it satisfies; don't claim a nursing/business major that was never chosen.
- Missing skill data backfills once via an explicit documented baseline; don't retroactively reroll talent per load or max every elder's skill. Decide how old pending job/university choices resolve under their saved version.
- Data edits retain stable IDs, completed credentials and accepted offer terms. Validate unknown IDs with explicit migration/tombstones, never silently replace a nurse with electrician. Store enough version/terms snapshot to honor accepted fees/pay after catalog changes.
- $0, failed course, rejected job, fired employee, disabled paid training and maximum skill all retain legal free/decline/leave/age actions.

## §Careers (A, FROZEN v1, 2026-11-20 — pending B/C/D cross-reference agreement)

A owns careers/employers end to end. All commands ride the existing
validated action/replay/transaction boundary (`GameService.command`); no
feature HTTP, no nested saves, no second RNG authority. Draws consume the
mounted command stream only.

### Data (src/game/content/careers/careers.json, schema_version 1)

```jsonc
{
  "schema_version": 1,
  "employers": [
    {"id": "voltworks", "name": "Voltworks Electrical", "sector": "electrical",
     "role_ids": ["app_electrician", "electrician"]}
  ],
  "roles": [
    {"id": "electrician", "label": "Electrician", "path": "electrical",
     "level": 2, "base_hours": 38,
     "pay_min": 52000, "pay_max": 64000,
     "prerequisites": {"min_age": 18,
                       "qualifications": ["electrical_trade_cert"],
                       "skills": {"practical": 40},
                       "roles": ["app_electrician"]},
     "promotions_to": ["senior_electrician"],
     "promote_after_years": 2, "promote_performance_min": 55,
     "look_override": "mechanic",          // optional; M0b manifest vocab only
     "events": ["career_electrical_1"]     // contextual event ids (frozen schema)
    }
  ]
}
```

Freeze facts: 12 roles / 6 fictional employers / 3 paths (electrical,
office, healthcare) per 01-spec; role/employer IDs are stable strings,
never names or catalog indexes. `look_override` only from the M0b
manifest vocabulary; absent asset → regular portrait (fallback is the
intended behavior, §Look rules above apply unchanged). Openings are
derived from employer `role_ids` — no transient array indexes persisted.

### Envelope state (env["careers"], validated versioned dict)

```jsonc
{"schema_version": 1,
 "next_num": 3,
 "assignments": {"<person_id>": {
    "assignment_id": "ca_1", "role_id": "electrician",
    "employer_id": "voltworks", "salary": 58000, "hours": 38,
    "accepted_turn": "t_000042", "accepted_age": 24,
    "role_tenure": 3, "status": "active",   // active|left|dismissed|retired
    "history": [{"role_id": "app_electrician", "employer_id": "voltworks",
                 "from_turn": "t_000040", "to_turn": "t_000042",
                 "salary": 41000, "reason": "promotion"}]}},
 "offers": {"co_2": {"offer_id": "co_2", "person_id": "<pid>",
    "role_id": "electrician", "employer_id": "voltworks",
    "salary": 58000, "hours": 38, "sampled_turn": "t_000042",
    "status": "accepted",                    // open|accepted|declined|expired
    "decision_id": "d_12"}},                // the pending that carried it
 "attempts": {"<year_or_turn>": {"apply": 1, "promote": 0}},
 "past_assignments": {"<person_id>": [{"assignment_id":"ca_1",
    "role_id":"app_electrician","employer_id":"voltworks","salary":28000,
    "accepted_turn":"t_000040","to_turn":"t_000042","status":"promoted"}]}}
```

One assignment per person; assignment IDs (`ca_N`) and offer IDs (`co_N`)
are envelope-scoped. Closed assignments (resign/promotion/transition)
move to `past_assignments` — the single history store the projection
flattens; the active assignment never carries an embedded history. Accepted offer terms (salary/hours) are snapshotted
into the assignment and the offer — catalog edits never rewrite accepted
terms.

### Legacy adapter (one wage payer)

M2 payroll stays the ONLY wage authority: on every assignment transition
the adapter sets `player.has_job/salary/job_hours` from the active
assignment (leave/dismissal → `has_job=False, salary=0`), so
`work_income` legs, pension history, stress/hours and
`annual_income_quote` behave exactly as today. Named employment never
posts its own salary legs. Two independently editable salaries are
forbidden: feature code mutates legacy scalars ONLY through
`careers.apply_adapter(env, person)`.

### Commands (whitelist additions, validated + replay-safe)

| action | payload | behavior |
|---|---|---|
| `careers_apply` | `{role_id, employer_id}` | validates eligibility (age/qualifications/skills/prior role via B/C bridges below); samples salary (rng) and persists an `open` offer; opens pending kind `career_offer` (accept/decline). No money moves. Capped attempts (see below). |
| `careers_resign` | `{}` | closes the active assignment (`left`), clears adapter, restores personal look. |
| `careers_promote` | `{role_id}` | validates tenure/performance/prereqs of a `promotions_to` target; sampled promotion offer via the same pending flow; on accept the adapter moves payroll to the new terms NEXT pay period (this turn's salary legs are never re-posted). |
| `careers_dismiss` | — internal/event-driven only | `dismissed` status + adapter clear; free recovery actions remain. |

Attempt caps (frozen): max 2 applications + 2 promotion attempts per
person per aged year (counter keyed by turn id, reset by year_start);
further attempts are refused with the precise server reason. Inspecting
openings (projection) never samples and never costs.

Pending kind `career_offer` is A-owned (display_category `"work"`); its
options carry `known_costs: []` (free) and no stat spoilers; the resolve
applies/declines atomically inside the existing resolve boundary.

### Projection (additive `_state["careers"]`, M2 work fields preserved)

```jsonc
{"assignment": {"role_id","role_label","employer_id","employer_name",
                "salary","hours","role_tenure","performance","status",
                "look_override"} | null,
 "openings": [{"role_id","role_label","path","level","employer_id",
               "employer_name","pay_min","pay_max","base_hours",
               "promotions_to":["senior_electrician"],
               "requirements": {"min_age":18,
                 "qualifications":[{"id":"electrical_trade_cert",
                                    "met":false,"label":"Electrical trade certificate"}],
                 "skills":[{"id":"practical","value":null,"required":40,
                              "met":false}],
                 "roles":[{"id":"app_electrician","met":true}]},
               "eligible": false,
               "reason": "you need the Electrical trade certificate"}],
 "history": [{"role_id","role_label","employer_id","employer_name",
              "salary","from_turn","to_turn","reason"}]}
```

`eligible`/`reason` are computed server-side with the same gates dispatch
enforces (advertised == dispatched). Requirement `met` reads B
(env["education"] qualifications) and C (env["skills"]) when present; an
absent bridge reads as unmet-with-label, never fabricated.

### Annual hook + ordering

`careers.annual_hook(env, service, rng)` runs inside `run_turn`
immediately AFTER the `work_income` phase result is journaled (A-owned
one-line integration in coordinator): active assignments gain +1
`role_tenure`, dismissal status transitions (e.g. prolonged low
performance) are evaluated here, and attempts counters roll with the
year. Payroll legs are posted by M2 `work_income` from the adapter
scalars — never by this hook. Ordering consequence (frozen): a
qualification awarded THIS turn by B's education phase qualifies for
commands NEXT turn (work before education; phases are not moved).

### Migration

Old saves without `env["careers"]` validate to the empty dict (no
assignments, no offers; `openings` still project from data). A legacy
generic employed life (`has_job=True`, no assignment) keeps M2 pay as
today and is offered transition into named careers via normal
`careers_apply` — never an invented employer history.

### Cross-references A depends on (to agree before global freeze)

- B: qualifications read from env["education"]["qualifications"]
  `[{qualification_id, program_id, awarded_turn}]` (bridge key names).
- C: skills read from env["skills"]["values"] `{skill_id: 0..100}`.
- Events: M3 career events use the frozen M2c schema with the bounded
  extensions already listed in the shared section above (A approves).
- D: validates careers.json schema/reachability (12 roles, 3 ladders,
  6 employers, all promotions reachable).

## Content validation — D (FROZEN v1, 2026-11-20 — cross-references
agreed per Claude: §Careers A, §Skills C, §Education B are FROZEN v1)

D owns the standalone content-validation command (`tools/validate_m3_content.py`),
its intentionally-invalid fixtures, and the reachability/cost report. It reads
data families; it never edits production data to make validation pass.

**Inputs (frozen only).** Careers/employers (A), programs (B), skills (C)
data files — all three are FROZEN v1 (§Careers A, §Education B, §Skills C)
as of 2026-11-20, so their validators are ERROR-gating (a missing family
file is itself an ERROR); the frozen M2c event schema — v3 events under
`src/game/content/events/*.json` structurally validated by the production
frozen checker `src/game/events.py:validate_event3`, which D mirrors rather
than redefines. (Pre-freeze, families were WARN-only; that mode ended at
freeze.)

**Checks (mirror existing frozen vocabularies, fail with file+id+rule):**
1. Structural: unique, stable string IDs per family and across files;
   required keys per the family's frozen schema; version fields present.
2. Foreign keys: role/program/skill/employer/event references resolve to
   defined IDs; `look_override`-style portrait keys exist in the pinned M0b
   manifest (`web/static/avatars/manifest.json` combos/looks vocabulary)
   or are absent (fallback to regular portrait — never a fake/employer
   portrait); event IDs referenced by career/program data exist in loaded
   event content.
3. Graph: promotion/prerequisite edges form no unreachable roles and no
   cycles unless the family schema declares an explicit valid loop rule;
   every role/program/skill has at least one executable path from a normal
   new life; no prerequisite requires a credential that itself requires
   the gated item (circular prerequisites); promotion thresholds are
   attainable within a normal lifespan (data-bound sanity bounds, e.g.
   tenure in years < 70).
4. Economics: wage/cost/funding amounts are integers within published
   bounds (wages >= 0, tuition >= 0, funding principal >= 0); every paid
   choice or program has a free/decline/leave/exit option (soft-lock
   rule); paid apprenticeship declares exactly one wage source.
5. Vocabulary: conditions/predicates/effects use only the frozen
   vocabularies (`PREDICATES`, `V3_CONDITION_KINDS`, v3 effect keys in
   `src/game/events.py`); **no Python/eval/exec or arbitrary model-field
   writes in authored content** — effects may not set salary, grant
   credentials, or `set_flag`-bypass mandatory gates; such transitions
   stay in domain commands (per the event-records section above).
6. Duplicate detection: identical reskins (same effects/costs/prereqs
   under different IDs/names) are reported as warnings with a diff
   summary, not silently counted as content.

**Fixtures.** Tiny intentionally-invalid JSON fixtures under
`tests/content/fixtures/invalid/` (one rule broken per file, named by
rule); the tool must report each fixture's expected failure code — a
fixture that passes is itself a test failure. Valid fixtures come from
the owners' frozen examples, not from D.

**Output / gating.** Human-readable + JSON report under
`docs/collab/evidence/M3/`; exit non-zero when any frozen-input check
fails; warnings never gate. Reachability/cost report (deliverable 2)
reuses the same loader and adds per-path human-readable summaries for
Claude; expected values in D's year-audit tests are explicit fixtures,
never a call back into production's income calculator.

**Cross-references (RESOLVED at freeze, 2026-11-20):** §Careers A /
§Education B / §Skills C all FROZEN v1 per Claude; the v3 event vocabulary
in `src/game/events.py` remains the supported set until an approved
amendment adds bounded typed extensions. Amendments to this section
need Claude's approval per the header rule.

---

# §Skills (Kimi C) — PROPOSED v1, 2026-11-20. FROZEN when A/B cross-refs agree.

Skills are per-person values 0-100 in four versioned data skills:
practical, organization, communication, care. Skills are independent of
smarts/looks; smarts may modify bounded learning rates, never replace a
required qualification.

## Data (`src/game/content/skills/skills.json`, owner C, versioned)

```jsonc
{
  "schema_version": 1,
  "skills": [
    {"id": "practical",     "label": "Practical",     "min": 0, "max": 100,
     "default": 5, "description": "Hands-on trade and repair ability."},
    {"id": "organization",  "label": "Organization",  "min": 0, "max": 100,
     "default": 5, "description": "Planning, order and reliability."},
    {"id": "communication", "label": "Communication", "min": 0, "max": 100,
     "default": 5, "description": "Working with and through people."},
    {"id": "care",          "label": "Care",          "min": 0, "max": 100,
     "default": 5, "description": "Patience and skill in helping others."}
  ],
  "training_options": [
    {"id": "train_practical",     "skill": "practical",
     "label": "Tinker with old machines",
     "cost": {"money": 0}, "annual_limit": 2, "progress": {"min": 3, "max": 6},
     "requires": {"min_age": 10}},
    {"id": "train_organization",  "skill": "organization",
     "label": "Volunteer to plan a neighbourhood event",
     "cost": {"money": 0}, "annual_limit": 2, "progress": {"min": 3, "max": 6},
     "requires": {"min_age": 12}},
    {"id": "train_communication", "skill": "communication",
     "label": "Join a public speaking circle",
     "cost": {"money": 25}, "annual_limit": 2, "progress": {"min": 3, "max": 6},
     "requires": {"min_age": 12}},
    {"id": "train_care",          "skill": "care",
     "label": "Help out at a community kitchen",
     "cost": {"money": 0}, "annual_limit": 2, "progress": {"min": 3, "max": 6},
     "requires": {"min_age": 10}},
    {"id": "train_study_skills",  "skill": null,  /* one per year, any skill */
     "label": "Take an evening skills course",
     "cost": {"money": 60}, "annual_limit": 1,
     "progress": {"min": 5, "max": 9}, "requires": {"min_age": 16},
     "choose_skill": true}
  ],
  "rules": {
    "smarts_learning_bonus": {"per_10_smarts": 0.5, "cap": 4},
    "job_practice_per_year": {"min": 1, "max": 2},
    "study_contribution_per_year": {"min": 1, "max": 2}
  }
}
```

Declarative rule vocabulary (fixed; new skills need no engine branch):
`cost.money` (int >= 0), `annual_limit` (int >= 1), `progress.min/max`
(int, min<=max), `requires.min_age`, `choose_skill`. Smarts bonus adds
`min(per_10_smarts * 0.5, cap)` to the rolled gain, rounded stochastically.
Saturation: rolled gain is clamped to the remaining headroom; at 100 no
gain occurs, no cost may be charged for a zero-gain attempt (the action is
disabled with reason "already at the maximum"), and no +progress journal
line is written for zero gain.

## Feature state (env["skills"], additive envelope key, owner C)

```jsonc
{
  "schema_version": 1,
  "people": {                       // keyed by person_id
    "<pid>": {
      "values": {"practical": 12, "organization": 7,
                 "communication": 9, "care": 5},
      "trained_this_year": {"train_practical": 1},   // option_id -> uses
      "history": [{"year": 12, "option_id": "train_practical",
                   "skill": "practical", "gain": 4}] // capped at last 20
    }
  }
}
```

Defaults are deterministic per person: `default + (stable_id_hash(pid,
skill_id) % 6)`, computed on first read and persisted — NO rng on reads,
no retroactive reroll per load. Deceased are read-only; successors keep
their own entry (keyed by their own person_id). Backfill happens once via
A's migration registry on load of pre-M3 saves.

## Commands (validated action envelope; A registers IDs in the dispatcher)

- `train_skill {target_id: "<option_id>", skill?: "<skill_id>"}` —
  `choose_skill` options require `skill`. Eligibility: min_age, annual
  limit, not saturated, can afford (money cost checked before ANY spend).
  One occurrence per dispatch: `occurrence_id =
  "skills:<person_id>:<option_id>:<year>:<n>"` (n = per-year running
  count), so replays cannot double-apply. Posts at most one money request
  `{phase, occurrence_id, leg:"training", account_owner_id, category:
  "education", amount, description}` through the frozen ledger boundary;
  free options post nothing. Journal (first person, "skill" kind):
  "I spent weekends pulling apart an old radio. I'm getting the hang of
  this." with actual `changes: {<skill_id>: <applied_gain>}` only.
- Skill state is NEVER mutated by events via set_flag; event options that
  train route through the same `apply_training` applier.

## Annual hook (order frozen with A)

One hook per year per person, named by A's registry (request: run AFTER
`work_income` and BEFORE `education`): job practice adds a bounded gain to
the skill declared by the active role/program data (A/B publish which
skill_id via their requirement data); study adds a bounded gain to one
declared study skill. Both go through `apply_training` with persisted
occurrence keys, so reload/replay cannot apply twice and a threshold
crossed in education never re-triggers a promotion in a finished phase.
Job/study contributions do NOT consume the manual annual_limit.

## Projection (additive under `state["skills"]`, owner C, A exposes)

```jsonc
"skills": {
  "values": {"practical": 12, ...},
  "training": [                 // the frozen action-spec dialect
    {"id": "train_practical", "label": "Tinker with old machines",
     "category": "activities", "target_id": "train_practical",
     "inputs": [],
     "known_costs": [], "limits": ["2 per year"],
     "enabled": true, "reason": null}
  ],
  "uses_this_year": {"train_practical": 1}
}
```

Requirements query for A/B (read-only, no mutation):
`skills.requirements_met(person_state, [{"skill": "practical", "min": 40}])
-> {"ok": true, "missing": []} | {"ok": false, "missing": [{skill, need,
 have}]}`.

## UI (`web/static/features/skills/`, owner C; B applies shell registration)

`skills.js` exports `mountSkills(root, hooks, data)` per the §Shell mount
API; shows the four values with progress, this year's uses/limits, known
costs, missing-requirement notes from A/B data, and disabled reasons —
never spoiler stat-effect ranges. Registration request to B: mount under
the `activities` screen alongside the existing Activities card, above it.

## RNG

Training rolls draw ONLY from the command's simulation rng at dispatch
(persisted via the envelope like other actions). Reads/projections/queries
never draw. `stable_id_hash` is sha256-based — no rng at all.

---

# §Education (Kimi B) — PROPOSED v1, 2026-11-20. FROZEN when A/C cross-refs agree.

B owns programs/qualifications end to end: program data, feature engine,
enrollment/tuition/debt/completion, Education UI module, feature tests.
Commands ride the existing validated action/replay/transaction boundary
(`GameService.command`) — A applies the exact dispatcher/projection/hook
integration requests listed at the end; feature code never self-saves,
never posts money outside the one ledger poster, never touches legacy
school/university progression.

## Data (`src/game/content/education/programs.json`, owner B, schema_version 1)

```jsonc
{
  "schema_version": 1,
  "programs": [
    {"id": "app_electrical", "label": "Electrical apprenticeship",
     "credential_id": "electrical_trade_cert",
     "credential_label": "Electrical trade certificate",
     "duration_years": 4, "tuition_per_year": 0, "fee_per_year": 400,
     "min_age": 17, "requires_secondary": true,
     "admission": {"min_smarts": null, "qualifications": []},
     "study_skill": "practical",
     "funding": ["self"],
     "apprentice_role": {"role_id": "app_electrician",
                         "employer_id": "voltworks"},
     "events": []},
    {"id": "care_diploma", "label": "Care diploma",
     "credential_id": "care_diploma_cert",
     "credential_label": "Care diploma",
     "duration_years": 2, "tuition_per_year": 3500, "fee_per_year": 0,
     "min_age": 17, "requires_secondary": true,
     "admission": {"min_smarts": null, "qualifications": []},
     "study_skill": "care",
     "funding": ["self", "student_loan", "parents", "scholarship"],
     "apprentice_role": null, "events": []},
    {"id": "business_bachelors", "label": "Business bachelor's",
     "credential_id": "business_degree",
     "credential_label": "Bachelor of Business",
     "duration_years": 4, "tuition_per_year": 9000, "fee_per_year": 0,
     "min_age": 17, "requires_secondary": true,
     "admission": {"min_smarts": 40, "qualifications": []},
     "study_skill": "organization",
     "funding": ["self", "student_loan", "parents", "scholarship"],
     "apprentice_role": null, "events": []},
    {"id": "nursing_bachelors", "label": "Nursing bachelor's",
     "credential_id": "nursing_degree",
     "credential_label": "Bachelor of Nursing",
     "duration_years": 4, "tuition_per_year": 8500, "fee_per_year": 0,
     "min_age": 18, "requires_secondary": true,
     "admission": {"min_smarts": null,
                   "qualifications": ["care_diploma_cert"]},
     "study_skill": "care",
     "funding": ["self", "student_loan", "parents", "scholarship"],
     "apprentice_role": null, "events": []}
  ]
}
```

Freeze facts: exactly these 4 programs (M3a minimum complete set);
program/credential IDs are stable strings. `tuition_per_year` is billed
per advanced year; `fee_per_year` (materials etc.) bills the same way but
is never loan/sponsor-funded — always cash. `apprentice_role` declares
the joint paid apprenticeship (A's role/employer IDs) — exactly one wage
source (A's payroll adapter); education never posts wage legs.

## Envelope state (env["education_state"], validated versioned dict)

```jsonc
{"schema_version": 1, "next_num": 3,
 "enrollments": {"e_1": {
    "enrollment_id": "e_1", "person_id": "<pid>",
    "program_id": "app_electrical", "program_version": 1,
    "started_turn": "t_000042", "progress": 2,       // completed years
    "status": "active",        // active|completed|left
    "funding": "self",         // self|student_loan|parents|scholarship
    "tuition_per_year": 0, "fee_per_year": 400,     // accepted-terms snapshot
    "studied_this_year": false,
    "attempts": 0}},           // repeat attempts at the current year
 "qualifications": [{"qualification_id": "electrical_trade_cert",
    "program_id": "app_electrical", "program_version": 1,
    "awarded_turn": "t_000046"}]}
```

The **bridge A's careers code already reads**:
`env["education_state"]["qualifications"]` is a flat list of immutable
records `{qualification_id, program_id, program_version, awarded_turn}`.
(A's §Careers prose says env["education"] — the implemented key is
`education_state`; this section fixes the prose to the code.)
Qualifications are durable: survive dismissal/retirement, save/reload,
succession of the same person; leaving a program never removes one.
One enrollment awards at most once (award only on the active→completed
transition). Accepted terms (tuition/fee snapshot) are honored after
catalog edits.

## Legacy bridge (one progress authority)

Legacy school/university progression (`yearly_education_phase`,
`uv_years`, graduation/expulsion) stays C/A-owned and untouched. B's
system NEVER reads or writes `uv_years`/`graduated_uv`. Enrollment is
refused while `player.uv_years > 0` or `player.is_in_school()`
(legacy enrollment still keeps grades set — the two systems can never
double-advance one course). Feature code mutates the legacy
`student_loan`/`chose_student_loan` scalars ONLY through
`education.apply_funding_adapter(env, person)` (loan funding).

- Loan funding: `student_loan += total_tuition` ONCE at enrollment;
  per-year tuition draws NO cash leg; M2 `debt_service` repays as today.
- Self funding: each advanced year posts one cash leg
  `-tuition_per_year` (category "education") at progress time.
- Parents/scholarship: no player cash leg (legacy parity); parents
  option exists only while a parent is alive and may be refused
  (pre-sampled at enroll time; refused option removed and re-offered,
  legacy re-offer pattern).
- `fee_per_year` always bills cash (self) regardless of funding.
- Previews/inspection never sample and never post money.

## Commands (whitelist additions; A registers IDs, B owns feature module)

| action | payload | behavior |
|---|---|---|
| `education_enroll` | `{program_id}` | validates min_age, secondary completion, admission smarts, prerequisite qualifications (B bridge), no active enrollment, no legacy school/university, no active career assignment (apprenticeship creates its own joint one). Samples funding availability (rng, persisted in pending `sampled`): parents alive + pre-sampled accept, scholarship pre-sampled (smarts>=70). If tuition+funding choices exist → pending kind `education_funding` (accept/decline); tuition 0 → enrolls directly. No money moves at sample time. |
| `education_funding` resolve | option id | via `_cmd_resolve` route: applies funding, posts loan principal ONCE (adapter), apprenticeship enrollment also creates A's joint apprentice assignment (see cross-ref). Decline → re-offer minus spent options; all declined → nothing enrolled, free. |
| `education_study` | `{}` | "Study hard this year": once per enrolled year, free, sets `studied_this_year`. |
| `education_leave` | `{}` | leaves the active enrollment (status left). No refund/credit transfer (frozen default); earned credentials retained; apprenticeship leave also closes the joint assignment via A's bridge. |

Pending kind `education_funding` is B-owned (display_category
`"education"`); options carry `known_costs` and no stat spoilers.

## Annual progress (B's hook, ordering frozen)

`education.annual_hook(env, service, rng)` runs in `run_turn`
immediately AFTER the legacy `education` phase result is journaled
(same seam as careers; A-owned one-line integration). Per active
enrollment, once per turn id (done-marker guarded):
1. Bill the year: self → cash leg `edu:<enrollment_id>:fee:<progress>`
   occurrence; loan/parents/scholarship → no cash leg; fee_per_year cash
   leg when set.
2. Pass/fail: studied_this_year → pass; else fail on `rng < 0.15`.
   Fail → repeat: no progress, happiness -rng(5,10), journal line;
   attempts counter increments (uncapped; leave/age always legal).
3. Pass → progress += 1, smarts +rng(1,3); when progress ==
   duration_years → status completed, award immutable qualification
   record (once), smarts +rng(8,12), happiness +rng(10,16), first-person
   journal; apprenticeship completion is also the promotion gate A reads
   via role prerequisites (next-turn rule below).
4. Reset studied_this_year.

Ordering consequence (frozen, same as §Careers): a qualification awarded
THIS turn qualifies for career commands NEXT turn (work before
education; phases are not moved). C's skills hook stays
work_income < education < **B hook**.

## Projection (additive `state["education"]`, M2 education view preserved)

```jsonc
{"enrollment": {"enrollment_id", "program_id", "program_label",
                "progress", "duration_years", "status", "funding",
                "tuition_per_year", "fee_per_year",
                "studied_this_year", "attempts"} | null,
 "programs": [{"program_id", "label", "credential_id",
               "credential_label", "duration_years", "tuition_per_year",
               "fee_per_year", "total_cost", "study_skill",
               "apprentice_role": {"role_id", "employer_id"} | null,
               "requirements": {"min_age", "secondary_met",
                 "min_smarts", "qualifications":[{"id","label","met"}]},
               "funding_options": ["self", "student_loan", ...],
               "eligible": false,
               "reason": "you need the Care diploma"}],
 "qualifications": [{"qualification_id", "label", "program_id",
                     "awarded_turn"}],
 "history": [{"enrollment_id", "program_id", "program_label", "status",
              "progress", "started_turn"}]}
```

`eligible`/`reason` are computed server-side with the same gates dispatch
enforces (advertised == dispatched); unmet prerequisite reads
unmet-with-label, never fabricated. Degree-gated jobs read earned
credentials only through A's read-only bridge (`env["education_state"]`
records) — UI never infers credentials from age/text.

## Migration

Old saves without `env["education_state"]` validate to
`{"schema_version":1, "next_num":1, "enrollments":{}, "qualifications":[]}`
(A's envelope default, one line). Legacy generic university
(`uv_years>0`/`graduated_uv`) keeps M2 behavior and earns NO named
credential — no retroactive major is invented; such a player enrolls in
programs through normal `education_enroll`. Partial generic university
never converts mid-course (uv_years must reach 0 or expulsion first —
the coexistence gate above applies at every enroll attempt).

## Cross-references / exact integration requests to A

1. `service.py` command whitelist: add `education_enroll`,
   `education_study`, `education_leave` to the rule_actions set (same
   pattern as `careers_core.ACTION_IDS`; B exports
   `education.core.ACTION_IDS`).
2. `_execute` dispatch branch: `elif action in education_core.ACTION_IDS:
   self._cmd_education_action(env, action, payload)` — a clone of
   `_cmd_careers_action` (typed PermissionError→409 not_allowed,
   ValueError→400, `post_money_entries` + `journal_events` +
   pending id assignment).
3. `_cmd_resolve`: route pending kind `education_funding` to
   `education.resolve_funding(env, player, pending, option_id, random)`
   (same seam as C's `resolve_choice` kinds; result events/money/pending
   handled by the existing boundary).
4. `_advertised_actions`: `actions.extend(education_core.action_specs(
   env, player))` inside the existing `alive and not pending` block.
5. `_state`: additive `"education": education_core.education_view(env,
   player)`.
6. Envelope default in save validation:
   `env.setdefault("education_state", {...empty dict per Migration...})`.
7. `_DISPLAY_CATEGORIES["education_funding"] = "education"`.
8. `coordinator.py`: after the legacy `education` phase result is
   journaled (same one-line seam as careers' hook), call
   `education.annual_hook(env, service, rng)`.
9. **A bridge for the apprenticeship** (needed before that route counts
   complete, per M3a): expose
   `careers.create_apprenticeship_assignment(env, person, role_id,
   employer_id) -> assignment` that creates the apprentice assignment +
   `apply_adapter` (one wage via M2 payroll), and
   `careers.close_apprenticeship(env, person, reason)` for leave. B
   calls these from `resolve_funding`/`education_leave`; if absent,
   apprenticeship enrollment is refused with reason "apprenticeship
   placements are not open yet" (stage gate, never a dummy wage).

## Events / portraits

M3 education events (when authored) use the frozen M2c schema only;
none are required for M3a. Graduate/student portrait context follows the
frozen M0b precedence/lifetime policy through the shared resolver —
education stores no cosmetic override.

# Amendments (Claude-approved via t7 task, 2026-11-20)

1. **§Careers + §Skills — trains_skill declarations:** each role in
   careers.json declares `"practices_skill"` (electrical→practical,
   office→organization, healthcare→care). Accessor:
   `careers.core.practiced_skill(role_id) -> skill_id | None`. C's
   job-practice hook reads ONLY this declaration for the active
   assignment.
2. **Annual-hook registry (frozen order, A-owned):** after `work_income`
   is journaled, BEFORE education — (a) `careers.annual_hook` (tenure),
   (b) `skills.training.annual_hook` (job practice, reads
   `practiced_skill`). No hook posts money; M2 work_income legs stay the
   only wage payer.
3. **§Skills bridge wired:** careers hiring gates call C's frozen
   `skills.projection.requirements_met(values, reqs)` with values from
   `skills.state.values(env, person_id)`; A exposes C's projection at
   `_state["skills"]` (guarded). The §Skills env shape is
   `env["skills"]["people"][person_id]["values"]` — the earlier §Careers
   cross-reference line naming `env["skills"]["values"]` is superseded.
4. **Ledger categories (agreed):** M2 payroll legs = `income` (unchanged
   authority); skill training = `education` (C); tuition/fee categories
   owned by B. Careers stage-1 posts no feature legs of its own.
5. **Relationship-drift journal lines (D robot finding, Claude-approved):**
   the yearly parent-relationship drift entry no longer repeats one fixed
   sentence. It is drawn through C's frozen `pick_year_line` machinery via
   the additive `pool` parameter (coordinated change to C's selector:
   `pool=None` keeps the default LINES pool; C's own tests unchanged), from
   A's pool `src/game/content/journal/relationship_lines.py`
   (direction-split improved/worsened, `{name}` placeholder). Recents live
   in `env["relationship_line_recents"]` with the same 60-pick window;
   draws are cosmetic-stream only; undo restores the window with the
   snapshot. Fallback when the pool is exhausted: the plain
   "My relationship with X improved/worsened." sentence. **Careers input
   dialect:** `careers_apply`/`careers_promote` advertise `type:"choice"`
   selects (role_id/employer_id; promotion choices from the active
   assignment's `promotions_to`) — clients and the robot pick advertised
   values, never free-typed ids. **Credential alignment:** careers role
   gates reference B's actual credential ids (`care_diploma_cert`,
   `nursing_degree`, `business_degree`, `electrical_trade_cert`).

## Amendment 1 (Claude, 2026-11-21): the projection key for the M3 education view is `state['education_programs']` (approved). `state['education']` stays the legacy M2 view, unchanged. A exposes the new view under `education_programs`; B's client reads it.

## Amendment 2 (Claude, 2026-11-21): ONE shared primary-commitment decision (Codex review R3, approved)

A owns `src/game/features/commitments.py` — the single source both
features consult before sampling/mutating anything. B's education
`_blocked_reason` consumes it (B's side of R3); careers dispatch and
advertised reasons consume it on A's side. No feature invents its own
job/study conflict rules after this amendment.

```python
refusal(env, person, want) -> str | None
```

`want` is one of three frozen verbs:

- `"job"` — new named employment (`careers_apply`).
- `"study"` — a degree/diploma program (`education_enroll`).
- `"apprenticeship"` — the joint paid apprenticeship (it is BOTH job
  and study, so it conflicts with either).

Decision table (frozen; the first matching row wins, evaluated in this
order). Sources: A's named assignment (`careers.active_assignment`),
B's active enrollment (`education.active_enrollment`), legacy M2
scalars/flags (`person.has_job`, `person.uv_years`,
`person.is_in_school()`). An absent feature envelope reads as "no
commitment", never as a refusal.

| existing commitment | want=`job` | want=`study` | want=`apprenticeship` |
|---|---|---|---|
| active feature enrollment | "finish or leave your program before taking a job" | "you are already enrolled in a program" | "you are already enrolled in a program" |
| named career assignment | "you already have a job — resign or seek a promotion first" | "resign from your job before enrolling" | "you already have a job — an apprenticeship is a joint commitment, resign first" |
| legacy job (`has_job`, no named assignment) | **allowed** (normal job transition; the accepted offer overwrites legacy pay through the one adapter) | "resign from your job before enrolling" | "you already have a job — an apprenticeship is a joint commitment, resign first" |
| legacy university (`uv_years > 0`) | allowed (M2 parity) | "finish or leave your current university course first" | "finish or leave your current university course first" |
| in secondary school | allowed (M2 parity) | "finish secondary school first" | "finish secondary school first" |

Freeze facts: the refusal is checked BEFORE any salary/funding sampling,
attempt counting, or mutation, on both the dispatch and the advertised
-spec paths (advertised == dispatched). The apprenticeship's own joint
assignment (created by enrollment) is legal and never self-conflicts.
Leave/resign/decline/age stay legal from every state. Legacy generic
jobs remain fully supported — a legacy-employed life applies for named
roles through the normal `careers_apply` and transitions on accept.

## Amendment 3 (Claude, 2026-11-21): fresh-employment baseline in the
one adapter (Codex defect, approved). A not-employed -> employed
transition through `careers.apply_adapter` (ordinary offer accept AND
the apprenticeship bridge start) initializes the M2 job-start baseline
with `Player.get_job` parity: performance 50, stress 45, years_worked
0, last_raise = age, times_asked_since_last_raise 0, fresh
salary_years. Ongoing employment (promotion; accept while still
legacy-employed) keeps performance/stress/years and never resets
salary_years mid-job (pension basis). Without this a new hire kept the
cleared 0/0 state and M2's performance firing (<15) fired on the first
age_up.

## Amendment 4 (Claude, 2026-11-21): work/study yearly v3 route (M3b B1
seam, approved so C can test against it)

- New yearly phase `workplace_events`, inserted into coordinator
  PHASE_ORDER directly AFTER `education` (before `debt_service`). No
  existing phase moves relative to another — this is an addition, not a
  reorder. Frozen order: ... work_income < careers/skills hooks <
  education < B education hook < **workplace_events** < debt_service ...
- Served by A-owned `src/game/features/workplace/engine.py` through the
  EXISTING v3 pipeline only: `v3_bindable` + `evaluate_v3_conditions` +
  `EventEngine.instantiate3` + the shared resolve boundary. No new
  interpreter, no feature-specific HTTP.
- Prefixes: `wrk_` requires an ACTIVE NAMED CAREER ASSIGNMENT;
  `stu_` requires an ACTIVE FEATURE ENROLLMENT (apprenticeship joint
  state satisfies both — it holds one of each). The route checks only
  the PRESENCE of the active source; role/program gates stay in the
  content condition trees (C-owned). No active source -> that family is
  silently skipped for the year (gates stay closed).
- Cadence: at most ONE occurrence per family per aged year; each record
  is offered at most once per life. Seen-cadence lives in
  env["wrk_stu_seen"] (plain list, pickle/undo/replay-safe); the
  per-year served marker lives in env["turn"]["workplace_served"] and
  dies with the turn. Within one year the phase serves wrk_ first, then
  re-enters via the existing `_keep_phase` mechanism after the pending
  resolves, so a working student can see BOTH families in one year.
- Occurrences are ordinary v3 pendings: pre_effects apply once at offer
  through the existing effects-bundle seam; money (when a choice
  carries it) posts ONCE through the one ledger poster at resolve;
  undo/redo ride the existing snapshot/replay boundary.
- Open items: D's tools/m2_assertions.py carries its own PHASE_ORDER
  copy — D adds "workplace_events" there when updating fixtures.

## Amendment 5 (Claude, 2026-11-21): M3c careers balance + paired
openings + parent-drift cap (approved)

- **Dual route on every ladder (data-only, no engine change):** the
  existing `alternatives` vocabulary is used so each ladder has a
  degree-gated AND a non-degree route to its top:
  - trade: `electrician` gains `alternatives: [{"qualifications":
    ["business_degree"]}]` — a Bachelor of Business plus the published
    practical-skill requirement enters the trade ladder without the
    apprenticeship certificate.
  - healthcare: `nurse` gains `alternatives: [{"roles":
    ["qualified_care_worker"], "min_role_tenure": 8, "label":
    "8 years as a Qualified care worker"}]` — the diploma + long
    experience route reaches nursing without the Bachelor of Nursing.
  - office already has both (`operations_specialist`). Skills/role
    prerequisites of the target still apply on every route
    (alternatives only waive the listed qualification/role gate).
- **Paired openings (advertised == dispatchable):** `careers_apply`
  advertises ONE `opening` choice input whose values are the valid
  `"role_id@employer_id"` pairs derived from employer `role_ids`
  (18 pairs; invalid combinations are never published). Dispatch
  accepts the advertised `opening` string OR the legacy
  `{role_id, employer_id}` pair (both validated server-side; malformed
  opening -> typed 400). No other advertised-input change.
- **Parent-drift journal cap (coordinator):** the yearly relations_age
  hook journals at most ONE parent-drift flavour line per year — the
  relation with the strongest |delta| (strict greater-than, so ties
  keep the earliest snapshot relation; deterministic) — and only when
  |delta| >= 1 (any actual change is notable; drift deltas are +-1).
  Per-relation rolling three-pick recents and the cosmetic-stream draw
  are unchanged; the plain-sentence exhaustion fallback is unchanged.

## Amendment 6 (Claude, 2026-11-21): tenure policy, promotion employer
availability, published routes (C1-C3, approved)

- **C1 tenure policy (frozen):** role experience = the TOTAL recorded
  years across ALL stints in that role (current assignment `role_tenure`
  + every past assignment's recorded `role_tenure` for that role), from
  stored tenure only. Unknown/missing tenure on a record counts as 0
  (legacy saves can never silently satisfy a minimum). Resignation,
  dismissal, employer change or taking another role never adds years.
  `min_role_tenure` gates (alternative routes) compare against this
  total; plain role-presence gates are unchanged (current or past).
- **C2 promotion employer availability:** `careers_promote` only opens
  an internal offer when the CURRENT employer's `role_ids` include the
  target. Otherwise dispatch refuses with the real reason naming the
  employer-change path ("... has no internal promotion to X — resign
  and apply there to change employer"), and the advertised promote spec
  lists only internally available targets (with the same reason when
  none are). Employer change stays the approved resign -> apply ->
  accept path; employer `role_ids` remain the single pairing authority.
  The active-assignment view mirrors the spec's filtered `promotions_to`
  choices and exposes `promotion_reason` for the hero card's explanation.
- **C3 published routes (server-owned projection; B renders):** every
  opening's `requirements` gains:
  - `routes`: one entry per route — `{"label", "met",
    "qualifications": [{id, label, met}],
    "roles": [{id, label, min_tenure, recorded_years, met}]}` —
    where `recorded_years` is the C1 total (null for presence-only
    roles). A nonempty default (main quals+roles) route comes first;
    an empty main route is omitted when alternatives exist, per Amendment 5.
  - `shared`: `{"min_age", "skills"}` — the requirements that apply on
    EVERY route (rendered once by B).
  - An opening's `reason_kind: "requirements"` identifies an explanation
    already represented by those lists. The UI may omit that duplicate
    paragraph while retaining the reason on its disabled Apply control;
    other reason kinds remain visible. Flat role items also publish labels.
  Backend stays the single eligibility authority; the UI must not
  reimplement route logic.
