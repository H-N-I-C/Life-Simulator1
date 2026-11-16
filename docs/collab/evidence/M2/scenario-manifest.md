# M2 scenario manifest — Kimi D (QA authority for browser parity)

Every row: deterministic setup (a **valid domain state** — correct age,
education, relationships and debt; never a bare age change or injected
balance), actual browser actions, state/ledger assertions with
**independently computed** expectations (tools/expected_finance.py, not the
implementation helpers), and an evidence path. Status is honest: a row is
`covered` only if the browser flow actually ran and asserted; `blocked`
rows name exactly what is waited on.

Global invariants asserted in every scenario where applicable:
- age advances exactly once per committed year; choice effects apply once
- every person reference in state/journal resolves to a persisted person id
- annual net == sum of signed ledger legs; running `balance_after` correct
- pending turn survives reload and server restart without repeating phases
- successor identity/cash consistent; generation increments

Setup mechanism: service API only (no debug UI in normal play). A test-only
**scenario factory** is requested from Kimi A (via Claude) so scenarios can
start e.g. "18-year-old, high-school graduate, unemployed, $0 debt" through
a supported path rather than fabricated state.

## M2a — school/work slice + audited annual money

| ID | Parity rows | Scenario setup | Browser actions | Key assertions | Evidence | Status |
|----|-------------|----------------|-----------------|----------------|----------|--------|
| M2a-S1 | 15,17,62,64,65 | Child age 10, enrolled, grades set (factory) | study_harder ×2 → age up → skip_school → age up | grades change exactly per rule, once/year limit enforced on 2nd attempt (typed rejection, no mutation); journal entries present once | m2a-s1-*.png + report | **blocked: factory + api-examples.json (A)** |
| M2a-S2 | 16,66,56 | Age 17, accepted to university, funding decision pending | resolve funding (loan option) → age up through uv years → graduation | loan principal lands once at graduation; debt decreases by 1k–3k×min rule on later years; uv stage transitions exact | m2a-s2-*.png | **blocked: A** |
| M2a-S3 | 18,19,21,22,54,71,74,75 | Age 18+, unemployed, smarts/karma fixed (factory) | find_job → offer decision (stored, no reroll — resolve twice with same request_id → identical result) → apply → adjust_hours 55 → work_harder → age up | offer salary identical across reload; hours gross = salary×55/40 via expected_income_quote; performance/stress move per rules once | m2a-s3-*.png | **blocked: A** |
| M2a-S4 | 20,55,73 | Worker with 10+ years, age 65 (factory) | request_raise (reject path) → retire via decision → age up ×2 | pension = expected_pension(salary_years, years) independent of job_hours after retire; income legs post once with source_key `t_*:income:salary:*` | m2a-s4-*.png | **blocked: A** |
| M2a-S5 | 55 (accounting audit) | Fixed salary/hours scenario | age up; read `finances.ledger` | for the service's own sampled retention: gross−tax−expenses==net; sum(amounts)==Δbalance; keys unique; estimate ≠ posted rows | m2a-s5-ledger.json | **blocked: A** |
| M2a-S6 | 71 | Employee performance <15 at year boundary (factory) | age up | fired once; salary stops next turn; no duplicate final pay | m2a-s6-*.png | **blocked: A** |

## M2b — health, activities, family

| ID | Parity rows | Scenario setup | Browser actions | Key assertions | Status |
|----|-------------|----------------|-----------------|----------------|--------|
| M2b-S1 | 29 | Adult with 2 illnesses + cash (factory) | visit_doctor → treat cheapest → age up | $100 fee (18+) posted once before confirm; cure applied once; health delta matches journal | blocked: A (health projection) |
| M2b-S2 | 30,35 | Age 18+ with cash | witch doctor (fixed fee bounds 50–100) → plastic surgeon decline | cost within bounds, posted once; no silent death path | blocked: A |
| M2b-S3 | 25,26,27,31,32,33,34 | Ages 3/5/12/18 via factory | each activity once → repeat same year | once/year limit; second attempt rejected with typed error, no effect | blocked: A |
| M2b-S4 | 28,70 | Single 18+, date_options available | find_partner → ask-out decision; separately seed dating-offer event | stored offer not rerolled; compatibility effect once | blocked: A |
| M2b-S5 | 10,12,13,14,67,68 | Dating → engaged (factory, rel/years gates met) | propose → plan_wedding (venue 4-choice, edit + cancel subflow) → age up → divorce; separately partner-initiated breakup/divorce events | venue costs within 51–15300; divorce split 40–60% per rule; both event decisions offer exactly 3 responses | blocked: A |
| M2b-S6 | 8,48,49 | Partner, gates met (factory) | try_for_baby → pregnancy → birth naming modal (text input, long name) → age up | baby person created via canonical hook; name persisted (long name, max length enforced); child age 0, no second annual update this turn | blocked: A |
| M2b-S7 | 36 | Age 25, married, <5 children, $30k (factory) | adoption → accept decision → rename decision | child created with identity hook; cost within 10k–30k, posted once; age gate disabled-reason shown at 18–20 | blocked: A |

## M2c — full yearly orchestration, debt, succession

| ID | Parity rows | Scenario setup | Browser actions | Key assertions | Status |
|----|-------------|----------------|-----------------|----------------|--------|
| M2c-S1 | 1,39–47,50–53,57–61,63 | Seeded natural life (one continuous run) | age up repeatedly through childhood/school/work with reloads at every pending decision and one full server restart mid-turn | phases resume at cursor, never repeat; exactly-once postings across restart; journal chronological | blocked: A (phase coordinator) |
| M2c-S2 | 57 | University graduate with loan (factory) | age up ×3 | repayment each year within 1k–3k×min rule, capped at balance; borrow/repay legs each once | blocked: A |
| M2c-S3 | 37 | Age 18+, $4 (factory) | buy_lottery with guesses 1,2,3,4 → age up | $4 leg once; jackpot re-roll does not duplicate | blocked: A |
| M2c-S4 | 38 | Any adult | surrender → double confirm | explicit two-step confirm; normal death + summary flow; no accidental path | blocked: A/B |
| M2c-S5 | 45,77 | Elder with 2+ eligible children (factory) | age up until death → continue-as-child selection | successor keeps person id/appearance/name; generation+1; cash inheritance matches transfer instruction exactly (no manufactured/lost money); archived life journal viewable, refs resolve | blocked: A |
| M2c-S6 | 23 | Two saved lives | game list → load other life → delete with confirmation | list reflects load/delete; delete is the confirmed destructive action only | blocked: B |

## Cross-cutting (run with every stage)

- Phone 390×844 + desktop 1280×800; keyboard/focus walk of each new screen;
  long names (60 chars) and $1,000,000+ labels don't overflow horizontally
  (recorded as real overflow, not screenshot eyeballing).
- Console/network errors recorded per scenario; temporary save dirs and
  free ports only.
- Visual regression: `tools/avatar_sheet.py` regenerated per stage
  (lifecycle sheet covers newborns→centenarians; matrix covers dark/light
  tones; hair-headwear sheet covers braids/locs + all headwear).

## Uncovered inventory (explicit, not silently skipped)

- Row 61 age-2 vaccination: covered only inside M2c-S1 natural run; no
  dedicated factory scenario until A exposes event seeding.
- Row 69 partner kidnapping (1/250, money≥1M): needs A-supported rare-event
  scenario; will not be claimed from random aging.
- Row 53 lightning (1/5000), row 59 cancer (1/2000): same — factory or
  honest "not performed".
