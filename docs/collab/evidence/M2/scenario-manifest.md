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
| M2a-S1 | 15,17,62,64,65 | `high_school_first_day` (age 14, enrolled) | study_harder ×2 → age up | grades change per rule; 2nd study same year = typed 409 + no mutation (revision unchanged); age +1 exactly once after settle | `m2a-s1-*` in browser report | **covered** (`m2a-s1-school-limits`, green) |
| M2a-S2 | 16,66,56 | `university_pending_17` (seed 4242; application accepted, decision OPEN) | resolve application (Yes) → await funding → loan option → assert education.loan.has_loan | loan recorded at acceptance per frozen shape | m2a-s2-*.png | **pending-engine** — funding decision now appears and resolves ("Student Loan") but `education.loan.has_loan` stays false (A/C: funding resolution doesn't record the loan) |
| M2a-S3 | 18,19,21,22,54,71,74,75 | `graduating_18` → find_job | offer decision → apply → adjust_hours 55 → settle year | gross == round(salary×55/40), tax == bracket calc (signed legs), gross+tax+expenses==net, Σlegs == Δbalance, ledger shape per frozen rows | `m2a-s3-ledger` | **blocked: scenario regression** — `unemployed_18` now lands enrolled in university (3y remaining), so find_job 409s; violates its frozen spec (A to fix) |
| M2a-S4 | 20,55,73 | `near_retirement_64` → age to 65 (offer answered keep_working) → retire action → settle | pension = calc_pension (salary becomes pension, has_job False); pension-year income legs post once; WARN-masked: projection shows retired:false/pension:0 (finding, A) | `m2a-s4-pension` | **covered** (`m2a-s4-retirement-pension`, fully strict — retired projection fixed by A, pension gross == 54218) |
| M2a-S5 | 55 (accounting audit) | Fixed salary/hours scenario | age up; read `finances.ledger` | for the service's own sampled retention: gross−tax−expenses==net; sum(amounts)==Δbalance; keys unique; estimate ≠ posted rows | m2a-s5-ledger.json | **pending-factory (flow authored)** |
| M2a-S6 | 71 | Employee performance <15 at year boundary (factory) | age up | fired once; salary stops next turn; no duplicate final pay | m2a-s6-*.png | **pending-factory (flow authored)** |

## M2b — health, activities, family

| ID | Parity rows | Scenario setup | Browser actions | Key assertions | Status |
|----|-------------|----------------|-----------------|----------------|--------|
| M2b-S1 | 29 | Adult with 2 illnesses + cash (factory) | visit_doctor → treat cheapest → age up | $100 fee (18+) posted once before confirm; cure applied once; health delta matches journal | pending-factory (flow not yet authored — M2b) |
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

## M2b execution status (2026-11-16, browser flows live)

| ID | Flow | Status |
|----|------|--------|
| M2b-S1 | conversation/argument (`m2b-s1-conversation`) | **covered** — direct effect, repeat allowed (no annual limit in parity), partner identity preserved |
| M2b-S2 | wedding venues (`m2b-s2-wedding-venues`) | **pending-path** — divorce works; `find_partner` never becomes available within 12 commands (dating path not wired from divorced state) |
| M2b-S3 | baby naming (`m2b-s3-baby-naming`) | **pending-engine** — try_for_baby applies but no birth/naming pending within 6 years (pregnancy→birth decision path not firing; same gap seen on `pregnant_25`) |
| M2b-S4 | adoption + rename (`m2b-s4-adoption`) | **covered** — offer→adopt→rename chain, child added via canonical hook, cost within 10k–30k posted once (`:adoption:` source key, category `other`) |
| M2b-S5 | doctor fees/cures (`m2b-s5-doctor-cures`) | **covered** — treat Common Cold -$100 once, cured; Flu remains; second treat cures Flu |
| M2b-S6 | witch doctor (`m2b-s6-witch-doctor`) | **covered** — take it; fee within 50–100 posted once |
| M2b-S7 | funeral (`m2b-s7-funeral`) | **failing: engine gap** — parents age 62/55 at scenario start reach 92/85 with ZERO relation deaths in 30 seeded years; P3 relation mortality apparently not wired (finding routed to A/C) |
