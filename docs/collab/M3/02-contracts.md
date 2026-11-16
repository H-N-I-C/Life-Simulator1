# M3 proposed contract checklist — NOT FROZEN

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
