# M4 browser journeys (Kimi D QA, 2026-11-21)

Harness: tools/m4_journeys.py + tests/browser/m4_journeys.py — real service + headless Chromium at 390x844 and 320x844. PENDING = frozen surface not landed (owner named); FAIL = surface exists but behavior is wrong.

## m4_j1_school_friend
- **PASS** `classmates_surface` — 6 classmates, all canonical person ids
- **PASS** `befriend` — closeness 0 -> 10 (kind friend)
- **PENDING** `invite` — C: no social_invite served to a school life in 8 years (invite content/route pending)
- **SKIP** `fallout_reconcile` — no friendship to fade (befriend/invite steps cover creation)

## m4_j2_bully
- **PENDING** `bully_surface` — C: no bullying arc served to a school life in 10 years (bully content/route pending)
- **SKIP** `respond` — no bully in the circle (bully_surface covers it)
- **FAIL** `minor_content` — minor can join adults_only clubs ['harbour_run_club']

## m4_j3_coworker
- **PASS** `coworkers_surface` — 3 coworkers in the circle (all canonical)
- **PASS** `befriend` — coworker aaa50196-9868-4ad1-8499-1fbcdbbe8616 is now kind=friend
- **PASS** `job_change` — after resign, ex-coworker friends remain real people in the projection (faded keep-contact)

## m4_j4_privacy_venues
- **PASS** `adult_club_refusal` — adults_only join refused: you must be 18 or older to join
- **PASS** `minor_invite_watch` — 8 minor years: no adults_only invite ever served
- **PASS** `romance_18_gate` — 17-year-old find_partner refused: both participants must be 18 or older
- **PENDING** `memory_privacy` — A/C: no memory fact recorded after befriend + 8 years of contact (memory pipeline not landed yet) — privacy probe has no subject

## m4_j5_contrasting_starts
- **PASS** `starts` — social projection + free actions + legal aging: Child, Graduate, Employed adult

Counts: {"PASS": 9, "PENDING": 3, "SKIP": 2, "FAIL": 1}
