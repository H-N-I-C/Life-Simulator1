# M2 parity matrix — Kimi C (reachable legacy gameplay)

Scope authority for M2. Source = what is actually reachable in the legacy game
(`src/menus/**` menus + `Player.age_up`/`Player.random_events`). Catalog data in
`const.py` that no menu or event reaches is NOT listed as a feature.

Stage column: a=M2a, b=M2b, c=M2c. Status: pending/approved-defer.

## Main menu (src/menus/main.py)

| # | Branch (legacy label) | Preconditions | Source | Stage | Proposed rule action / event | Notes |
|---|----------------------|---------------|--------|-------|------------------------------|-------|
| 1 | Age +1 | always | main.py:41 → Player.age_up | c | `advance_phase` (phase map) | see phase-map.md |
| 2 | Relationships → view relation | relation exists | main.py:44-97 | b | projection only (A/B) | bars: relationship/generosity/money/smarts/looks/petulance/craziness |
| 3 | Spend time | rel ≥ age gates, rel.age≥5, once/year | main.py:99-128 | b | `spend_time` | already in service for parents; extend to all relation kinds |
| 4 | Have a conversation | rel≥25 to proceed; argument sub-choice | main.py:129-212 | b | `conversation` + pending decision (apologize/disagree/insult) | refusal branch rel<25 |
| 5 | Ask for money (parents) | parent, asked<3 | main.py:213-232 | b | `ask_for_money` | generosity gate, money formula, 3-strike refusal |
| 6 | Compliment | once/year | main.py:233-299 | b | `compliment` | return-compliment event |
| 7 | Insult | confirm | main.py:300-335 | b | `insult` | counter-insult / attack (sibling petulance, partner craziness) |
| 8 | Have a baby | partner, not pregnant, rel/years gate | main.py:336-401 | b | `try_for_baby` + pending decision (keep/abort, overrule branches) | fertility check; partner overrule |
| 9 | Break up | partner status<2 | main.py:402-412 | b | `break_up` | confirm |
| 10 | Propose | partner, years/rel gate | main.py:413-433 | b | `propose` | one-time happiness hit on rejection |
| 11 | Call off engagement | status=1 | main.py:434-445 | b | `call_off_engagement` | |
| 12 | Plan the wedding (incl. edit/cancel subflow) | status=1, afford venue | main.py:446-480 | b | `plan_wedding` + venue choice (4 sampled, costs 51–15300); per-venue sub-choice Do it / Edit the plan (re-pick) / Actually never mind (exit loop) | affordability check; edit and cancel sub-steps are inventoried, not just the heading |
| 13 | Divorce (player-initiated) | status=2 | main.py:481-487 | b | `divorce` | 40–60% money split (C-owned rule) |
| 14 | Renew vows | status=2, ≥10y since last, rel gate, once | main.py:488-498 | b | `renew_vows` | |
| 15 | Study harder | in school, once/year | main.py:508-515 | a | `study_harder` | NERD trait learn roll |
| 16 | Drop out | age≥18, or uv, or 15-16 + smarts gate | main.py:516-531 | a | `drop_out` | parents refusal branch; clears uv |
| 17 | Skip school | in school | main.py:532-545 | a | `skip_school` | 1/5 caught (not in uv); once/year |
| 18 | Find a Job | age≥18, unemployed | main.py:637-665 | a | `find_job` (sample offer) + decision (apply) | interview roll (smarts/karma mod) |
| 19 | Work Harder | has job, once/year | main.py:681-688 | a | `work_harder` | LAZY extra stress |
| 20 | Retire / Quit | retire: years≥10 & age≥65 | main.py:689-707 | a | `retire` / `quit_job` | pension = calc_pension() |
| 21 | Adjust Hours | has job, 38–70 | main.py:708-710 | a | `adjust_hours` | stress delta 0.75/h |
| 22 | Request a Raise | ≥10y since last, once/year, perf gate | main.py:711-726 | a | `request_raise` | rejection; 1/9 fired after 2+ asks |
| 23 | View Saved Games / load / delete | terminal | main.py:727-749 | b | web game list covers load and delete (Kimi B), delete behind confirmation | INCLUDED per Claude; destructive control needs explicit confirm |
| 24 | Debug Menu (stats/identity edit) | DEBUG only | main.py:547-636 | excluded | replaced later by dev/scenario panel (roadmap) | EXCLUDED per Claude |

## Activities menu (src/menus/activities.py)

| # | Branch | Preconditions | Source | Stage | Proposed action | Notes |
|---|--------|---------------|--------|-------|-----------------|-------|
| 25 | Play with your toys | age 3–12 | activities.py:39-249 | b | `play_toys` | age-banded sayings; depression branch; once/year |
| 26 | Arts and Crafts | age≥5 | activities.py:250-340 | b | `arts_and_crafts` | ideas-failure branch; once/year |
| 27 | Listen to music | age≥12 | activities.py:341-360 | b | `listen_to_music` | stress relief; once/year |
| 28 | Find a Partner | age≥18 single, date_options>0 | activities.py:361-389 | b | `find_partner` + decision (ask out) | date_options 9–11/year |
| 29 | Doctor | age≥4; $100 fee at 18+ | activities.py:390-481 | b | `visit_doctor` + per-illness treat choice | cure tables per illness; affordability |
| 30 | Witch Doctor | age≥18 | activities.py:482-509 | b | `visit_witch_doctor` | fee 50–100; can kill (health ≤0) |
| 31 | Meditate | age≥12 | activities.py:526-548 | b | `meditate` | MEDITATOR trait learn; once/year |
| 32 | Gym | age≥12, health≥10 | activities.py:549-566 | b | `gym` | once/year |
| 33 | Library | age≥12 | activities.py:567-601 | b | `library` | NERD/BOOK_LOVER learn; once/year |
| 34 | Read a Book (grouped subflows) | always | activities.py:602-668 | b | `read_book` + book choice (5 sampled of age-eligible BOOKS) | APPROVED collapse to one action keeping book choice and equivalent effects (smarts/happiness, limits); abandon = exit with no gain |
| 35 | Plastic Surgery | age≥18, ≥6y since last, $25000 | activities.py:669-714 | b | `plastic_surgeon` + surgeon choice | botched → damage, possible death |
| 36 | Adoption | menu 21+, body: ≤70, <5 children | activities.py:715-755 | b | `adoption` + accept decision + rename decision | show from 21+ and enforce body checks with clear disabled reason (Claude); cost 10k–30k; A's child-creation hook |
| 37 | Lottery | age≥18 | activities.py:756-837 | c | `buy_lottery` with numeric guesses (4 unique 1–20) | $4/ticket; jackpot from change_jackpot |
| 38 | Surrender | always | activities.py:838-842 | c | `surrender` with double confirm | INCLUDED per Claude: explicit double confirmation, then normal death/summary flow |

## School menu (main.py:502-545)

Covered by rows 15–17. Menu visibility: in school → School; elif age≥18 →
Job menu / Find a Job.

## Yearly phases (Player.age_up + Player.random_events)

| # | Event | Preconditions | Source | Stage | Phase | Notes |
|---|-------|---------------|--------|-------|-------|-------|
| 39 | Stat drift (age, looks/smarts/health drift) | always | Person.age_up (person.py) | c | P2 | A re-implements drift in service today |
| 40 | Trait yearly effects (WELL_KEPT, SICKLY, MOODY, GRUMPY, worker traits, LAZY) | has trait | player.py:431-451 | c | P2 | C extracts `apply_yearly_trait_effects` |
| 41 | Annual flags reset (already_did set, witch doctor mod, date_options) | always | player.py:354-372 | c | P1 | C rule; A stores flags |
| 42 | Karma drift ±2 | always | player.py:453 | c | P1 | |
| 43 | Plastic surgeon list rotation | 1/35 ×2 | player.py:454-456 | c | P1 | cosmetic offer data |
| 44 | Relations age + parent rel drift (+1 <18, else −1/−1/0) | always | player.py:457-463 | c | P3 | A does drift today |
| 45 | Death check (old age) | always | player.py:466 | c | P9 | A owns death/journal |
| 46 | Lottery jackpot re-roll | always | player.py:469 | c | P1 | feeds row 37 |
| 47 | Job stress model (target stress by happiness/hours/kids, MEDITATOR) | has_job | player.py:470-486 | a/c | P5 | C extracts |
| 48 | Player pregnancy → miscarriage or birth + naming | is_pregnant | player.py:488-523 | b/c | P4 | naming = pending decision w/ text input |
| 49 | Partner pregnancy → birth + naming + stat reveal | partner.is_pregnant | player.py:524-564 | b/c | P4 | |
| 50 | Depression onset | happiness < rand 1–10 | player.py:565-570 | c | P7 | |
| 51 | Relation deaths (natural + sudden w/ reasons) + funeral decision + inheritance | death_check/sudden | player.py:571-582, 374-425 | c | P3 | funeral = decision; inheritance sampled; A archives |
| 52 | Thanatophobia worry | trait, age 12–17+ | player.py:584-599 | c | P8 | |
| 53 | Lightning strike | age≥5, 1/5000 | player.py:785-801 | c | P8 | can kill |
| 54 | years_worked increment | has_job | player.py:802-803 | a | P5 | |
| 55 | Annual income (salary/pension) | salary>0 | player.py:821-823 (M1 fixed) | a | P5 | annual_income_breakdown; A posts ledger |
| 56 | University year progression / graduation / expulsion | uv_years>0 | player.py:824-839 (M1 fixed) | a | P6 | loan principal on graduation |
| 57 | Student loan repayment | loan>0 | player.py:840-846 | c | P5 | 1k–3k×min, capped |
| 58 | Illness progression (depression recovery, HBP, cancer, cold/flu) | per illness | player.py:847-877 | c | P7 | cancer can kill |
| 59 | Cancer diagnosis | age≥4, 1/2000 | player.py:862-866 | c | P7 | |
| 60 | Flu / common cold | 1/20 | player.py:867-877 | c | P7 | |
| 61 | Vaccination event (age 2) | age==2, 1/2 | player.py:878-899 | c | annual_events | KEPT per Claude; 3-way behavior choice |
| 62 | Grade drift (smarts anchor, happiness effect) | in school | player.py:900-910 | a | P6 | C extracts (school yearly) |
| 63 | GENIUS smarts drift | trait | player.py:911-915 | c | P2 | |
| 64 | School start: elementary 6 / middle 12 / high 14 | age==6/12/14 | player.py:916-927 | a | P6 | sets grades |
| 65 | High school graduation | age==17, not dropped out | player.py:1087-1094 | a | P6 | |
| 66 | University application + funding decision (scholarship / loan / parents) | age 17, accepted | player.py:1095-1160 | a | P6 | multi-option decision; parents option removable on refusal |
| 67 | Partner-initiated breakup (3 responses) | dating/engaged, rel low, 1/3 | player.py:928-976 | b/c | P8 | decision |
| 68 | Partner-initiated divorce (3 responses) | married, rel low, 1/6 | player.py:993-1042 (M1 fixed) | b/c | P8 | decision; willpower check |
| 69 | Partner kidnapping ransom (3 responses) | partner, money≥1M, 1/250 | player.py:1028-1062 | c | P8 | decision; can "kill" partner |
| 70 | Dating offer (accept/reject) | 18+, single, 1/16 | player.py:1063-1086 | b/c | annual_events | KEPT per Claude; both dating paths stay; decision; compatibility |
| 71 | Fired for performance (<15) | has_job | player.py:634-643 | a | P5 | |
| 72 | Stress damage / high blood pressure | stress>65 | player.py:644-662 | a/c | P5 | can add HBP illness |
| 73 | Retirement offer (eligibility + accept/decline) | can_retire, 1/3 | player.py:619-633 | a | P5 | decision; forced at 70 w/ 1/3 |
| 74 | salary_years append | has_job | player.py:604-605 | a | P5 | feeds calc_pension |
| 75 | Performance yearly change + stress from happiness swing | has_job | player.py:606-618 | a | P5 | |
| 76 | Child yearly (income net, school, uv, jobs, partners, divorce) | per child | child.py age_up | c | P3/P5 | child income = child-owned account |
| 77 | Death & continue-as-child | player dies w/ children | convert_child_to_player | c | P9 | A owns identity conversion; C prepares transfer |

## Proposed deferrals (need Claude confirmation)

- Row 23 (in-game save management) — web game list covers it.
- Row 24 (debug menu) — not gameplay.
- Row 34 page-loop — collapse to single read action.
- Terminal-only: `play.sh`/language selection (B adapter owns web i18n).

## Ambiguities for Claude

1. Rows 23/24: exclude from web parity?
2. Row 34: collapse page-loop to one action?
3. Row 36 adoption: menu gate is age≥21 in activities (`age >= 21`), body checks >70 and <5 children — confirm web uses menu gate 21+.
4. Row 61 (age-2 vaccination): keep? It is reachable only at age 2 via age-up.
5. Dating offer (row 70) duplicates Find a Partner action (row 28) — keep both (legacy has both)?

## Status updates

- 2026-11-16 (M2a extracted): rows 15–22, 54–56, 62, 64–66, 71, 73–75 have
  headless implementations in `src/menus/rule_actions.py`
  (study_harder/skip_school/drop_out, find_job offer+apply, work_harder,
  adjust_hours, request_raise, retire/quit_job, yearly_work_phase,
  yearly_education_phase + resolve_* continuations), covered by
  tests/test_rules_m2a.py. Pending A confirmation of phase-map.md before the
  service wires P5/P6.

## Claude answers (2026-11-16)
1. Row 23: NOT excluded. Web game list covers load and delete (with confirmation) — Kimi B. Row 24: Debug Menu excluded from normal play; replaced later by the dev/scenario panel (roadmap cross-cutting item).
2. Row 34: Yes, collapse to one action, but keep the book choice and equivalent effects (smarts/happiness gains, time/limit rules).
3. Row 36: Show adoption from 21+, and also enforce the body checks (age ≤ 70, fewer than 5 children) with a clear disabled reason.
4. Row 61: Keep the age-2 vaccination event.
5. Row 70: Keep both — "Find a Partner" is the player's action, the dating offer is a random yearly event.

## Cross-check against codex-inventory-check.md (2026-11-16)

| Codex item | Matrix rows |
|---|---|
| Main nav: School, job application/menu, relationships, activities, saved games | 1, 2–22, 25–38, 23 |
| Save loading/deletion scope decision; debug not parity | 23, 24 |
| Conversation, time, compliment, insult, ask money | 3, 4, 5, 6, 7 |
| Partner: baby, propose, call off engagement, wedding edit/cancel, breakup/divorce, renew vows | 8, 10, 11, 12, 9, 13, 14 |
| School study/dropout/skip + annual university funding/progression | 15–17, 66, 56 |
| Job menu: work harder, hours, raise, quit, retirement; applications generic | 19–22, 18 |
| Activities incl. Surrender (explicit confirm/death flow) | 25–38 |
| Mind & Body: meditate, gym, library, book subflows | 31, 32, 33, 34 |
| Age gates: doctor 4+, 18+ extras, partner search single, adoption 21+ | 29, 30/35/37, 28, 36 |
| Annual pass: funding, pensions, pregnancy/naming, relation-initiated decisions, death/succession | 66, 73/55, 48/49, 67/68/69, 45/77 |
| Loan: explicit student-loan choice vs automatic debt service | 66 (principal), 57 (automatic repayment); no general loan menu exists |
6. Row 38 Surrender: include it in the web version, with an explicit double confirmation ("End this life?" then a final confirm), then the normal death and summary flow. Row 23 deletion: answered in 1 (included, with confirmation).
