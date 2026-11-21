# M3 browser journeys (Kimi D QA, 2026-11-21)

Harness: tools/m3_journeys.py + tests/browser/m3_journeys.py — real service + headless Chromium at 390x844 and 320x844. PENDING = contract surface not frozen/implemented (owner named); FAIL = surface exists but behavior wrong.


## paid_course
- **PASS** `choose_and_pay` — Actual care choice, single $60 charge, persisted gain, no overflow at 320

Counts: {"PASS": 1}
