# M2 phase map — FROZEN v4 (A-owned; legacy-parity ordering)

FROZEN by Claude's decision (00-TEAM.md): A owns this file and the
ledger/entry schema; only A edits it. Merges the A/C phase
designs and all docs/collab/M2/codex-phase-review.md fixes.
Changes require Claude approval.

One canonical yearly phase list. C's rule_actions implements work_income
(legacy tag P5) and education (P6); legacy P-numbers map to the canonical
names below. Composite legacy `age_up`/`random_events` stay FORBIDDEN in the
web coordinator; C exposes the headless subrules named in §1.

## 0. Ordering baseline

Legacy (`src/people/classes/player.py`) ages relations, then checks the
player's old-age death, then relation deaths, and income accrues LAST
inside `random_events`. The phase order below preserves exactly that
(FINAL, Claude 2026-11-16 — no exceptions): deaths run BEFORE
work_income, the deceased are not paid, and relation deaths plus
inheritance post in the mortality phase. Debt timing is also legacy
parity: education (graduation-year loan principal) runs BEFORE
debt_service, so a new graduate's first repayment lands in the SAME year
as in the legacy game.

## 1. Canonical phases (order frozen)

| # | phase_id | legacy tag | Owner | Rule entry / subrules | may mutate |
|---|----------|-----------|-------|------------------------|-----------|
| 1 | year_start | P1 | A | orchestration; snapshot living people by person_id | player.age, total_happiness, annual flags/limits, turn snapshot |
| 2 | relations_age | P2 | C | `increment_age(person)` for snapshot members; `relation_year_drift` | relation ages, relationship values (NO deaths) |
| 3 | player_stats | P3 | C | `stat_drift(player, rng)`, `apply_yearly_trait_effects(player, rng)` (existing-trait modifiers only) | player stat fields, teen_looks_inc, karma, surgeon list |
| 4 | mortality | — | A/C | player old-age check + relation deaths (C subrule `sample_relation_death`); inheritance money_entries post HERE | status, dead_ids, archive, inheritance |
| 5 | work_income | P5 | C | `yearly_work_phase`: stress/perf/years, retirement offer, firing; income via `annual_income_quote` legs (money_entries only) | employment fields; NO direct money mutation |
| 6 | education | P6 | C | `yearly_education_phase`: grade drift, school stages, university; graduation-year loan principal (before debt_service) | grades/enrollment/uv years; loan principal via money_entries |
| 7 | debt_service | — | C→A | student-loan repayment computation | loan balances via money_entries only |
| 8 | family_events | P7 | C (M2b) | `child_stat_year`, `child_money_year(child)->money_entry`, `child_social_year` (job/partner), pregnancy/births (A creation hook) | partner/child state, births, relationship values (NO deaths) |
| 9 | illness | — | C (M2b) | illness onset/progression; deaths here return a TERMINAL transition | illnesses, health, treatment money_entries |
| 10 | annual_events | — | C (M2b/M2c) | legacy random_events branches as stored choices; trait gains ONLY here | event-specific fields; money_entries |
| 11 | decision_offer | — | A | M1 contextual templates (RETAINED) | pending choice record only |
| 12 | year_close | — | A | journal year entry (ACTUAL deltas), ledger close, cursor reset | journal, cursor |

Deaths before income (legacy parity): the deceased are not paid; relation
deaths and inheritance post in the mortality phase (4). A fatal effect in
illness/annual_events returns `finished_turn: true` with continuation
{phase_id: year_close}: only close work runs afterward, exactly once.
Phases that may run after player death: year_close only. Player old-age
mortality (phase 4) and relation deaths are processed BEFORE
decision_offer (11): the dying never face a new contextual choice.

## 2. Ageing invariant

year_start snapshots living people; only snapshot members are aged;
newborns (created in family_events) are age 0 and receive NO further annual
update this turn. A walks the snapshot once with visited IDs; child yearly
updates are the three split subrules (stat/money/social), never composite
`Child.age_up`.

## 3. Cursor (frozen)

`{phase_id, item_cursor, event_id?, step?}`; named order above (no stored
numeric index). Multi-item phases persist per-entity `done` markers and a
continuation pointing AFTER the current item. `resolve_choice(context,
pending, option_id, inputs, rng)` applies only stored pre-sampled option
data and returns the continuation; it never reruns a phase and validates
inputs/membership before any mutation or RNG draw.

## 4. Money (frozen)

- Entry: `{phase, occurrence_id, leg, account_owner_id (= canonical
  person_id), category, amount, description, group_id?}`; single poster in
  `src/game/finance.py`; key `{turn_id}:{phase}:{occurrence_id}:{account_id}:{leg}`;
  same key + different details → LedgerConflict; identical retry → original
  row; transfer legs share `group_id`.
- ALL cash flows through the ledger, including action costs (doctor,
  wedding, lottery, tuition). Rule code never mutates `money`.
- Income quote: C's pure deterministic `annual_income_quote(salary,
  has_job, job_hours, expense_factor)` returns integer gross/tax/expenses/
  net with `net == gross - tax - expenses` exactly. Rounding policy
  (frozen, C): deterministic integers with residual allocated to expenses;
  NO posting-time RNG. The expense factor is sampled once per account/turn
  through the supplied rng and stored in the cursor; A persists the
  immutable quote per account/turn and posts its legs verbatim.
  Executable fixture: quote(30000, True, 40, 0.6) = 30000/3401/10640/15959
  (tests/test_rules_income.py AnnualIncomeQuoteTest).
  The legacy terminal helper keeps its internal sampling (parity).
- Employment state is read at work_income; mid-turn job/retirement changes
  (annual_events choices) take effect next turn (matches legacy, where
  income accrues from the pre-event state).
- Estimates are pure projections, never posted or RNG-sampled.

## 5. Forced/voluntary retirement

Forced retirement is an EVENT (never an options=[] pending). Voluntary
retirement pauses at a defined substep with recorded substeps; resolution
applies stored outcomes exactly once; the year's pay/pension follows the
phase-4/5 parity rule (state at income time).

## 6. M1 → M2 migration

idle → cursor {phase_id: year_start, item_cursor: 0}. Pending M1 choice →
phases up to annual_events marked complete (effects committed); the stored
pending carries over UNCHANGED and resolves from stored effects; cursor =
continuation after the choice. Application-type M1 decisions map to an
equivalent held decision_offer state. dead → terminal, history preserved.
web_id p_XX references are remapped to canonical person_id by the v1→v2
envelope migration before any M2 rule runs.

## 7. Shared-file ownership

Unchanged: A owns Person identity/appearance + `convert_child_to_player`
identity portions + succession graph conversion; C owns gameplay functions
and extracted subrules. Succession: C returns transfer instructions; A owns
identity-preserving conversion + archived-life snapshot.

## 8. Examples policy

All costs/age windows/eligibility in rule-examples.json and
api-examples.json come from docs/collab/M2/parity-matrix.md. There is NO
university application fee (removed); admission is age 17 (row 66).

## Open items (post-freeze work, not shape changes)

- C: restructure P5 to the quote API + extended keys; executable
  continuation examples (two choices/one phase, two people,
  debt-before-family, interruption after one account posting).
- A: context factory + ledger hook wiring; sync api-examples work_income
  wording (already consistent).
