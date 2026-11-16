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
| 17 | Skip school | in school | main.py School menu → `skip_school` | a | `skip_school` (rule_actions) | explicit policy: core losses apply EVERY use (legacy), positive fun reward only first use per year; 1/5 caught (not in uv) |
| 18 | Find a Job | age≥18, unemployed | main.py:637-665 | a | `find_job` (sample offer) + decision (apply) | interview roll (smarts/karma mod) |
| 19 | Work Harder | has job, once/year | main.py:681-688 | a | `work_harder` | LAZY extra stress |
| 20 | Retire / Quit | retire: years≥10 & age≥65 | main.py:689-707 | a | `retire` / `quit_job` | pension = calc_pension() |
| 21 | Adjust Hours | has job, 38–70 | main.py:708-710 | a | `adjust_hours` | stress delta 0.75/h |
| 22 | Request a Raise | ≥10y since last, once/year, perf gate | main.py:711-726 | a | `request_raise` | rejection; 1/9 fired after 2+ asks |
| 23 | View Saved Games / load / delete | terminal | main.py `View Saved Games` branch | b | web game list covers load and delete (Kimi B), delete behind confirmation | INCLUDED per Claude (2026-11-16); destructive control needs explicit confirm |
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
| 34 | Read a Book (grouped subflows | Mind & Body menu, age 6+) | activities.py Mind & Body → Read a Book | b | `read_book` + book choice (5 sampled of age-eligible BOOKS) | APPROVED collapse to one action keeping book choice and equivalent effects (smarts/happiness, limits); abandon = exit with no gain |
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
| 56 | University year progression / graduation / expulsion | uv_years>0 | player.py uv block / rule_actions.yearly_education_phase | a | education | loan debt accrues per financed year (Claude policy 2026-11-16: tuition financed stays as debt); leaving keeps accrued debt, never zero |
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
| Loan: explicit student-loan choice vs automatic debt service | 66 (funding choice), 56 (per-year accrual), 57 (automatic repayment); no general loan menu exists |
6. Row 38 Surrender: include it in the web version, with an explicit double confirmation ("End this life?" then a final confirm), then the normal death and summary flow. Row 23 deletion: answered in 1 (included, with confirmation).

## Codex M2a review metadata fixes (2026-11-16)

- Source column now uses function/branch names (line numbers drift).
- Row 23: INCLUDED (Claude answer), assigned to B with confirmation.
- Row 34: parent gate Mind & Body age 6+ recorded.
- Row 17: explicit skip-school once-per-year policy (core losses repeat).

## Approved rule policies (Claude, 2026-11-16)

1. **Retirement income (row 73/55)**: legacy order — income is quoted from
   the employment state at the work_income phase. A voluntary retiree is
   paid this year's salary and the pension starts NEXT turn; a FORCED
   retirement (applied inside the phase, before the salary line, as legacy)
   quotes the pension for the current year.
2. **Critical stress (row 72)**: BUG FIX approved — legacy *added* health
   (`change_health(+...)` under stress > 85); the extracted rule applies
   damage (`-round_stochastic((stress-80)/4)`). Terminal path also fixed.
3. **Student loans (rows 56/66)**: financed tuition stays as debt — each
   loan-financed university year accrues a year's tuition (rng 5000–10000)
   when financed; dropout/expulsion keep accrued debt, never zero.
4. **Skip school (row 17)**: core losses apply on every use; the positive
   fun reward only the first use per year.

## Status updates (2026-11-16, M2b)

- Extracted + tested (rule_actions.py, tests/test_rules_m2b.py, 29 tests):
  rows 3-9, 10-14 (spend_time, conversation+argument decision, ask_for_money,
  compliment, insult, try_for_baby keep/abort with partner-overrule,
  break_up, propose, call_off_engagement, plan_wedding with venue
  do/cancel, divorce with settlement entry, renew_vows) and rows 25-36
  (play_toys, arts_and_crafts, listen_to_music, find_partner+date decision,
  doctor with per-illness pre-sampled cure outcomes and $100 fee entry,
  witch_doctor with terminal-transition death, meditate, gym, library,
  read_book collapsed with per-book pre-sampled outcomes, plastic_surgery
  with per-surgeon pre-sampled botched/success, adoption with accept +
  rename decisions).
- Legacy parity fix found during extraction: adopted children start with a
  random last name (rename decision offers matching) — matches activities.py.
- Deaths inside activities (witch doctor, botched surgery) return
  finished_turn -> year_close; A performs the actual death.

# Row status & proof register (2026-11-16, M2d audit source)

Every row has a status and its exact proofs: unit test (file.class) and D
browser flow (ID + coverage state from docs/collab/evidence/M2/scenario-manifest.md).
Status: DONE / DONE-PENDING-D (code+unit proven, D flow still blocked) /
DEFERRED-APPROVED / NO-PROOF (flagged).

| Rows | Status | Unit proof | D flow proof |
|------|--------|-----------|--------------|
| 1 | DONE-PENDING-D | test_rules_continuations (phase chains) | M2c-S1 blocked-A (coordinator resume) |
| 2 | DONE | B projection | m2b-r2-view-relation (green) |
| 3 | DONE | test_rules_m2b.SpendTimeTest; A service spend_time (M1) | via M2b flows (spend_time in scenarios) |
| 4 | DONE | test_rules_m2b.ConversationTest | M2b-S1 covered (m2b-s1-conversation) |
| 5 | DONE | test_rules_m2b.AskMoneyTest | m2b-r5-ask-for-money (green) |
| 6, 7 | DONE | test_rules_m2b.ComplimentInsultTest | m2b-r6-r7-compliment-insult (green; row 6 second-compliment now a typed 409, see policy note) |
| 8 | DONE-PENDING-D | test_rules_m2b.BabyTest | M2b-S3 pending-engine (birth/naming not firing in browser) |
| 9–14 | DONE-PENDING-D | test_rules_m2b.PartnerLifecycleTest | M2b-S2 pending-path (dating not wired from divorced state) |
| 15, 17 | DONE | test_rules_m2a.SchoolActionTest | M2a-S1 covered (green) |
| 16 | DONE | test_rules_m2a dropout + loan-policy tests | M2a-S2 (partial) |
| 18 | DONE | test_rules_m2a.FindJobFlowTest | M2a-S3 blocked: scenario regression (unemployed_18 enrolled — A to fix) |
| 19, 21 | DONE | test_rules_m2a.JobActionTest; WorkPhaseTest | M2a-S3 (as above) |
| 20 | DONE | test_rules_m2a retirement tests; test_rules_income.RetirementPensionFlowTest | M2a-S4 covered (fully strict) |
| 22 | DONE | test_rules_m2a raise approval/rejection | m2b-r22-request-raise (green) |
| 23 | DEFERRED-APPROVED (B) | none | M2c-S6 blocked-B |
| 24 | EXCLUDED (Claude: dev panel later) | — | — |
| 25–27, 31–34 | DONE-PENDING-D | test_rules_m2b.ActivitiesTest | M2b-S3 flow (activities subset; manifest S1/S5/S6 covered) |
| 28 | DONE-PENDING-D | test_rules_m2b find_partner tests | M2b-S2 pending-path |
| 29 | DONE | test_rules_m2b.HealthActivitiesTest (doctor) | M2b-S5 covered |
| 30, 35 | DONE | test_rules_m2b (witch terminal, surgery) | m2b-r35-plastic-surgery + m2b-s6-witch-doctor (green) |
| 36 | DONE | test_rules_m2b.AdoptionTest | M2b-S4 covered |
| 37 | DONE | test_rules_m2b.LotteryTest (win+jackpot reroll, loss, input validation, affordability, buy-10) | M2c-S3 blocked-A |
| 38 | DONE | test_rules_m2b.SurrenderTest (double-confirm) | m2c-s5-succession (surrender double-confirm green) |
| 39–44 | DONE (A-owned engine) | A service suites | M2c-S1 |
| 45 | DONE-PENDING-D | A mortality phase + test_rules_m2b.MortalityHelpersTest | M2c-S5 blocked-A |
| 46 | DONE (A-owned) | A suites | M2c-S3 |
| 47 | DONE | test_rules_m2a.WorkPhaseTest | M2a-S3/S5 |
| 48, 49 | DONE-PENDING-D | test_rules_m2b.FamilyEventsPhaseTest (birth+naming, miscarriage) | M2b-S3 pending-engine |
| 50 | DONE | test_rules_m2b.IllnessPhaseTest (depression) | M2c-S1 |
| 51 | DONE | test_rules_m2b.MortalityHelpersTest | m2b-s7-funeral + m2b-r51-inheritance (green; funeral pending fires ~year 28 in married_30; archive/person refs green) |
| 52, 53 | DONE | test_rules_oracle (statistical+exact); test_rules_event_content; test_rules_event_loader (auto rates) | none dedicated — flag |
| 54, 74, 75 | DONE | test_rules_m2a work-phase tests | M2a-S3 |
| 55 | DONE | test_rules_income quote tests; m2a legs | M2a-S3/S5 (S5 pending-factory) |
| 56 | DONE | test_rules_m2a.EducationPhaseTest | M2a-S2 pending (education.loan.has_loan projection false — A/C) |
| 57 | DONE-PENDING-D | test_rules_m2a.DebtServiceTest | M2c-S2 blocked-A |
| 58–60 | DONE | test_rules_m2b.IllnessPhaseTest | m2b-r58-60-illness-progression + ill_with_cash (green) |
| 61 | DONE | test_rules_oracle vaccination tests; content+loader | await_vaccination seed-sweep (green) |
| 62, 64, 65 | DONE | test_rules_m2a education tests | M2a-S1 covered |
| 63 | DONE | test_rules_m2a.TraitDriftTest (increment_age, stat_drift determinism, relation_year_drift, trait effects); subrules: increment_age/stat_drift/relation_year_drift/apply_yearly_trait_effects | M2c-S1 |
| 66 | DONE | test_rules_m2a university chain tests | M2a-S2 pending (as 56) |
| 67, 68 | DONE | test_rules_oracle; test_rules_divorce (M1); event content+loader | M2b-S5 (divorce works; S2 pending-path) |
| 69 | DEFERRED-APPROVED (Claude's single deferral) | test_rules_oracle exact (ransom/rates); event content+loader | m2b-r69-kidnapping via wealthy_married_40: records PENDING (excluded from the gate, never a pass) until A's dev-only force_event hook lands, then a forced real check |
| 70 | DONE-PENDING-D | test_rules_oracle + test_rules_m2b find_partner | M2b-S2 pending-path |
| 71 | DONE | test_rules_m2a.FiringAndStressDamageTest (fired on roll, kept on miss) | M2a-S6 pending-factory |
| 72 | DONE | test_rules_m2a.FiringAndStressDamageTest (critical stress damages health, HBP added; non-critical none) | none |
| 73 | DONE | test_rules_m2a retirement-offer tests | M2a-S4 covered |
| 76 | DONE | test_rules_m2b child stat/money/social tests | m2b-r76-child-yearly (green) |
| 77 | DONE | test_rules_m2b.SuccessionTest | m2c-s5-succession (identity/generation+1/same game_id green) |

## Unproven rows (updated 2026-11-16 after C items landed)

Resolved by C: 37 (lottery), 63 (trait-drift subrules), 71 (firing test),
72 (stress-damage test).

- 2 — no unit test or D flow (B projection; needs a D view-relation check).
- 37 — lottery not extracted (C; M2c remainder).
- 63 — apply_yearly_trait_effects / increment_age / stat_drift /
  relation_year_drift extraction unstarted (C).
- 71 — performance-firing branch: no isolated unit test (C); D factory pending.
- 72 — stress-damage/HBP branch: no isolated unit test (C).
- Remaining tracked items: D's WARN finding with exact signature
  'turn.cursor.phase_id = null after a mid-year performance firing'
  (WARN-masked in D's runner; self-removes when A's projection fix lands).
  A's factory fix has LANDED; D's latest runner: PASS=737 FAIL=0
  PENDING=1 (this approved deferral) WARN=7.
- Pending-D engines (known, in-flight): M2a-S3 scenario regression (A),
  M2a-S2/S2 has_loan projection (A/C), M2b-S2 dating-after-divorce (A),
  M2b-S3 birth/naming not firing in browser (A/C), M2c-S1..S6 (A/B).

## D evidence + findings (2026-11-16)

Green flows cited above (docs/collab/evidence/browser/report.md). Findings:
- Row 6: second compliment returned 200 with no effect — FIXED by C (now a
  typed 409 not_allowed; legacy granted nothing on repeat, the extracted
  rule refuses explicitly so the spec's enabled/reason matches dispatch;
  policy note for Claude/Codex). RE-RUN GREEN (m2b-r6-r7 asserts the 409).
  SWEEP: the same fix was applied to every
  other once-per-year action (spend_time, play_toys, arts_and_crafts,
  listen_to_music, meditate, gym, library — all now typed 409s on second
  use, with specs' enabled/reason matching; tests in
  test_rules_m2b.OncePerYearLimitSweepTest).
- Row 51: RESOLVED — A's mortality wiring fix landed; D re-ran green
  (m2b-s7-funeral, m2b-r51-inheritance; funeral pending fires ~year 28 in
  seeded married_30; archive + person-ref assertions green).
- Row 61: vaccination is a single-year (age 2) 50% gate per legacy.
  RESOLVED: D's await_vaccination sweeps a fixed seed list (1-6,
  deterministic per run), resolves the 3-way choice when it fires; green.
- Row 69 (2026-11-16, per Claude's ruling): the single APPROVED deferral.
  The flow records PENDING (excluded from the acceptance gate, never a
  pass) until A's dev-only force_event hook lands, then becomes a forced
  real check. D's root cause stands as context: partner_kidnapping carries
  the LEGACY karma gate NOT x_in_y(karma,130) (correct parity, documented
  in the data file) and ~1/500/yr even when passable — unreachable by any
  seed sweep. Claude's ruling: the unblock is A's dev-only force_event
  hook (approved deferral stands). NOTE: A has since pinned
  wealthy_married_40 karma=50 so the legacy gate IS passable there
  (documented in the hook); D re-runs m2b-r69 against it while the
  force_event hook is pending.
