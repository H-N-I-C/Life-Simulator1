# M3c independent evidence (Kimi D QA, 2026-11-21)

Harness: tools/m3c_evidence.py + tests/browser/m3c_checks.py — real service + headless Chromium at 390x844 and 320x844. Ladders are climbed by real play (train_skill / education_enroll+study / careers_apply+accept / careers_promote); the only env-level hook is the documented pre-M3 save delta.

## j7_contrasting_starts
- **PASS** `contrasting_starts` — starts aged legally with free actions: graduating_18, unemployed_18, enrolled_child_10, employed_30

## j8_legacy_university
- **PASS** `legacy_university` — legacy uv_years course resumed, finished and gated enrollment; then care_diploma enrolled via normal action, no invented credential

## ladder_electrical
- **PASS** `climb` — role site_supervisor reached in 8 years, history [('app_electrician', 'left'), ('app_electrician', 'promoted'), ('electrician', 'promoted'), ('senior_electrician', 'left')], money 141731

## ladder_office
- **PASS** `climb` — role operations_manager reached in 6 years, history [('app_electrician', 'left'), ('office_assistant', 'promoted'), ('coordinator', 'promoted'), ('operations_specialist', 'left')], money 147722

## ladder_healthcare
- **PASS** `climb` — role senior_nurse reached in 10 years, history [('care_assistant', 'left'), ('qualified_care_worker', 'left'), ('app_electrician', 'left'), ('nurse', 'left')], money 67503

## j11_promotion_unavailable
- **PASS** `unavailable_reason` — unavailable promotion refused with the real reason: Meridian Office Group has no internal promotion to Operations manager — resign and apply there to change employer

## j12_alt_route_accumulates
- **PASS** `accumulates` — nurse reached via the 8-year alternative route (5+3 recorded years across two stints, no degree)

## j12_alt_route_below_threshold
- **PASS** `below_threshold` — 7 recorded years refused with the exact alternative-route reason: you need Bachelor of Nursing + experience as Qualified care worker or 8 years as a Qualified care worker

## j9_money_conservation
- **PASS** `conservation` — t_000051: delta 16904 == sum of 3 legs {'income': 40959, 'tax': -4759, 'expenses': -19296}

## j10_visual_pass
- **PASS** `visual_pass` — Home + 4 tabs x 390/320: no overflow, no console errors, money format clean, screenshots captured

Counts: {"PASS": 10}
