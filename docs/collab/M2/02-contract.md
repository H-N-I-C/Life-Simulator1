# M2 draft integration contract

Claude must freeze this after A/C confirm source inventory; changes go through Claude. Preserve M1 endpoint names and request revision/request_id semantics.

## Rule boundary: C implements, A consumes

`src/menus/rule_actions.py` provides:

- `action_specs(context) -> list[dict]`: allowed action IDs, target kinds, bounded input schemas and known costs/prerequisites. Pure read; no RNG consumption or mutation.
- `apply_action(context, action_id, payload, rng) -> RuleResult`: validates and applies one action to the transaction-local canonical model; never saves, prints or prompts.
- `advance_phase(context, phase_id, rng) -> RuleResult`: applies one explicit yearly phase and returns the next phase/cursor or a pending choice. No annual effects duplicated in Person/Player/Child wrappers.
- `resolve_choice(context, pending, option_id, inputs, rng) -> RuleResult`: resolves only the stored decision, then identifies its continuation. Does not reroll the initial event or repeat earlier effects.

Context has the actual Player graph, stable ID lookup, annual flags, phase/cursor and turn_id plus versioned rule data. A provides the context factory in src/game; C uses duck-typed fields rather than importing GameService (avoid cycles). Before coding, A/C publish minimal JSON-compatible result examples in M2/rule-examples.json and an exact phase list in M2/phase-map.md.

RuleResult is a plain dict with `events`, `money_entries`, `pending_decision` (null or serializable data), `continuation` (phase/cursor or null), and `finished_turn` bool. Events carry kind, text/template, person_ids and actual state changes; relationship deltas are per person ID. Monetary entries request posting through the one shared ledger authority; do not both mutate money and request the same posting. Agree which legacy helpers mutate directly and adapt them once at extraction, with before/after assertions preventing double credits. C's annual_income_breakdown helper supplies gross/tax/expenses/net once per eligible person/year; A owns posting, storage and projection.

A owns immutable IDs; C never derives identity from names, seeds or memory addresses. Generated babies/partners use A's canonical creation hooks. Appearance/persistence changes go to A, not C. C's successor financial/gameplay preparation returns transfer instructions; A owns actual identity-preserving graph conversion and archived-life snapshot, while B owns the selection UI. A/C must agree this function boundary before changing convert_child_to_player.

## Public API additions

Retain M1 `/api/games`, game GET and actions POST with replay-before-revision semantics. Extend available_actions entries with:

`{id, label, category, target_id?, inputs: [{id,type,label,required,min?,max?,choices?}], known_costs: [], limits: []}`.

Categories: relationships, education, work, activities, health, finances. UI consumes declared schemas only; backend independently validates types/ranges, age, funds, target, annual limit and life status. Option availability is explicit: pending options have `enabled` and `reason` plus precise known costs/effects; never show guaranteed effects as uncertainty or reveal hidden random outcomes.

Pending decisions support `inputs` for naming/selecting a successor and serializable continuation data kept private. Browser sends decision_id, option_id and bounded `inputs` in resolve_decision payload; the backend checks them. A logical annual turn can span several committed decision commands, each revisioned and durable; persist phase completion and sampled results at every boundary. A failure rolls back only the current command, without losing previously committed phases or repeating them. No re-entry into a legacy interactive method as a continuation strategy.

State adds:

- `education`: stage, enrolled, grades, university years, loan status, eligible actions.
- `work`: employed/retired state, generic job label, hours, salary, effective annual gross, pension, performance, stress. Do not invent a named M3 career.
- `health`: illnesses/treatments and relevant action availability.
- `family`: explicit relationship/partner/children IDs and status (all resolve in people).
- `finances`: balance, debt breakdown, current annual estimate, last closed year and ledger entries.
- `generation`, completed_lives summaries, pending turn status and stable identity references.

Each projection is a read-only view, not a second model. Existing M1 fields remain supported or undergo a documented versioned API transition coordinated with B/D. Do not return full private pickle state.

## Finance invariants

Ledger row: `{id, source_key, turn_id, age, person_id, category, amount, balance_after, description}` with signed amounts; original currency model remains one fictional-dollar unit. Debt principal/liabilities are explicit separate balances; record borrow/repay cash legs once. Gross pay minus tax minus living expenses minus debt service plus other cash flows must equal cash delta for the period. Annual estimates are previews, never posted transactions. Retired pension is independent of job_hours; overtime gross is the gross taxed and credited. Working-child net is not full salary. Define insolvency/negative-balance behavior explicitly from corrected legacy rules; no silent flooring that fabricates affordability.

Annual phases must clearly distinguish player/household and child-owned accounts so childcare/child net cash are not counted twice. Every row has an idempotent source_key. Model mutations, postings, pending decisions, replay response and revision commit in one save transaction. Ledger references remain valid after succession; inherited cash transfer cannot manufacture or silently drop money. Actual before/after stat and per-person relationship changes must match journal values.

## Saves and errors

A supplies migration from accepted M1 web-save envelopes, preserving IDs/avatars/history/pending M1 decisions; unsupported versions fail clearly. Explain how M1 pending choices continue under M2 rules without rerolling them. New required fields get real documented defaults/backfill, validated before rule/RNG contexts. Invalid actions/inputs return typed errors with no mutation; errors never expose filesystem paths or tracebacks. Existing origin/host/body/path guards remain in B's adapter.

## Required cross-agent artifacts before bulk implementation

C: parity-matrix.md (source file:function/branch → action/event → preconditions → stage → test ID → status) and rule-examples.json skeleton.
A+C: phase-map.md and final rule-examples.json; list precise shared-function ownership, including school/work/child yearly updates, financial posting and succession.
A: api-examples.json covering each state section, paid action rejection, multiphase pending turn, birth/naming and succession.
B: validate fixture-driven layouts against the API before inventing ad hoc endpoints.
D: scenario manifest naming deterministic setup, actions, assertions, expected final state and evidence paths; use actual service commands for flows.
