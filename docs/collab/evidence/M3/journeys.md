# M3 browser journeys (Kimi D QA, 2026-11-21)

Harness: tools/m3_journeys.py + tests/browser/m3_journeys.py — real service + headless Chromium at 390x844 and 320x844. PENDING = contract surface not frozen/implemented (owner named); FAIL = surface exists but behavior wrong.


## j1_electrical_trade
- **PASS** `finish_school` — already graduated (age 18)
- **PASS** `work_tab` — work tab at 390/320
- **PASS** `enroll_apprenticeship` — enroll flow started (kind=None)
- **PASS** `apprentice_assignment` — joint apprentice assignment active at voltworks (one commitment via the apprenticeship bridge)
- **PASS** `apprentice_wage` — one payroll income leg in t_000027, delta 10534
- **PASS** `promotion` — promotion gate enforced: you need 2 years in your current role first
- **PASS** `dismissal_recovery` — resigned; assignment cleared

## j2_healthcare
- **PASS** `finish_school` — already graduated (age 18)
- **PASS** `nurse_gate` — qualification gate enforced: this role requires age 21 or older; you need Bachelor of Nursing + experience as Qualified care worker or 8 years as a Q
- **PASS** `education_ui` — programs listed at 390/320; ineligibility reason visible (nursing_bachelors: you need the Care diploma)
- **PASS** `education_ui_enroll` — UI enroll started (pending=education_funding, enrollment=False)
- **PASS** `education_progress` — completed care_diploma: care_diploma_cert awarded once, history row completed (2/2 years)
- **PASS** `education_reload` — qualification 'Care diploma' persisted across reload

## j3_skills
- **PASS** `train_skill` — trained practical: uses 0->1 value 7->16
- **PASS** `skills_ui` — skills UI mounted; 4 values, 5 training options advertised
- **PASS** `skills_ui_train` — UI training applied: train_practical uses 1 -> 2

## j4_zero_money
- **PASS** `zero_money_legal` — aged legally at $0

## j5_old_save
- **PASS** `strip_and_load` — pre-M3 save resumed: age 30, legacy job 'Generic job' at $42000
- **PASS** `no_invented_records` — no invented assignments/employers/enrollments/degrees
- **PASS** `legacy_pay_preserved` — legacy wage paid once (t_000048, delta 24814)
- **PASS** `legacy_transition_gate` — legacy->named transition: app_electrician at voltworks, pay $26246 via the one adapter

## j6_m3b_careers
- **PASS** `named_employment` — named employment: app_electrician at voltworks, salary $27379
- **PASS** `employer_change` — employer change: voltworks (app_electrician) -> meridian (office_assistant), pay $32982, history [('voltworks', 'left')]
- **PASS** `dismissal_forced` — engine dismissal closed office_assistant (meridian) with a 'dismissed' history row; recovery actions legal
- **PASS** `retirement_direct` — retired from voltworks; pension $32384 paid once in t_000105 (money delta 22407)
- **PASS** `retirement_offered` — offered path: retired from voltworks; pension $32384 paid once in t_000113 (money delta 12198)
- **PASS** `salary_pension_reload` — pension $32384, history [('voltworks', 'retired')] and 51 ledger rows identical after fresh reload

Counts: {"PASS": 27}
