# Browser check report

PASS: 739 | FAIL: 0 | PENDING: 1 | WARN: 7

## Pending rows (unperformed paths)

- [m2b-r69-kidnapping] kidnapping (legacy ~1/500/yr with karma<=100) did not fire across 6 seeds x 8 years; deferred by Claude's written approval until A's force_event hook lands -- deferred (Claude-approved 2026-11-16: legacy 1/500/yr rarity; awaiting A's dev-only force_event hook, then must become a forced real check); 

| Result | Check | Detail |
|--------|-------|--------|
| PASS | [creator_390x844] no horizontal overflow | scrollWidth=390 innerWidth=390 |
| PASS | [creator_390x844] no console errors | [] |
| PASS | [creator_390x844] no uncaught exceptions | [] |
| PASS | [creator_390x844] no failed requests | [] |
| PASS | [creator_1280x800] no horizontal overflow | scrollWidth=1265 innerWidth=1280 |
| PASS | [creator_1280x800] no console errors | [] |
| PASS | [creator_1280x800] no uncaught exceptions | [] |
| PASS | [creator_1280x800] no failed requests | [] |
| PASS | [gallery_390x844] no horizontal overflow | scrollWidth=390 innerWidth=390 |
| PASS | [gallery_390x844] no console errors | [] |
| PASS | [gallery_390x844] no uncaught exceptions | [] |
| PASS | [gallery_390x844] no failed requests | [] |
| PASS | [gallery_1280x800] no horizontal overflow | scrollWidth=1265 innerWidth=1280 |
| PASS | [gallery_1280x800] no console errors | [] |
| PASS | [gallery_1280x800] no uncaught exceptions | [] |
| PASS | [gallery_1280x800] no failed requests | [] |
| PASS | [game_390x844] no horizontal overflow | scrollWidth=390 innerWidth=390 |
| PASS | [game_390x844] no console errors | [] |
| PASS | [game_390x844] no uncaught exceptions | [] |
| PASS | [game_390x844] no failed requests | [] |
| PASS | [game_1280x800] no horizontal overflow | scrollWidth=1280 innerWidth=1280 |
| PASS | [game_1280x800] no console errors | [] |
| PASS | [game_1280x800] no uncaught exceptions | [] |
| PASS | [game_1280x800] no failed requests | [] |
| PASS | [relationships_390x844] no horizontal overflow | scrollWidth=390 innerWidth=390 |
| PASS | [relationships_390x844] no console errors | [] |
| PASS | [relationships_390x844] no uncaught exceptions | [] |
| PASS | [relationships_390x844] no failed requests | [] |
| PASS | [relationships_1280x800] no horizontal overflow | scrollWidth=1265 innerWidth=1280 |
| PASS | [relationships_1280x800] no console errors | [] |
| PASS | [relationships_1280x800] no uncaught exceptions | [] |
| PASS | [relationships_1280x800] no failed requests | [] |
| PASS | [creator-keyboard] creator swatches render (options built) |  |
| PASS | [creator-keyboard] skin tab swatches built |  |
| PASS | [creator-keyboard] focus retained after swatch click |  |
| PASS | [creator-keyboard] activated swatch is aria-pressed |  |
| PASS | [creator-keyboard] ArrowRight moves to next tab |  |
| PASS | [creator-keyboard] badges tab shows badge swatches |  |
| PASS | [creator-keyboard] two badges multi-selected |  |
| PASS | [creator-keyboard] third badge refused (cap 2) |  |
| PASS | [creator-keyboard] badge-bearing preview renders (badge avatars pass validation) |  |
| PASS | [creator-keyboard] export validates via Appearance.from_dict | base=person skin=default |
| PASS | [creator-keyboard] preview present at export time |  |
| PASS | [creator-keyboard] preview valid after rapid age changes |  |
| PASS | [m1-life-flow] life created via API | game_id=g_544aaa |
| PASS | [m1-life-flow] game view visible after create |  |
| PASS | [m1-life-flow] actions available |  |
| PASS | [m1-life-flow] player name shown |  |
| PASS | [m1-life-flow] reached age >= 2 | age=2 |
| PASS | [m1-life-flow] game view visible after reload |  |
| PASS | [m1-life-flow] age persisted across reload (>= 2) |  |
| PASS | [m1-life-flow] player avatar rendered |  |
| PASS | [m1-life-flow] pending decision with participants | kind=vaccination_age2 n=1 |
| PASS | [m1-life-flow] participant fixture is non-empty | person_ids=1 |
| PASS | [m1-life-flow] decision participants show portraits | person_ids=1 svgs=1 |
| PASS | [m1-life-flow] post-decision reload keeps portraits |  |
| PASS | [m1-life-flow] refresh state | game_id=g_544aaa |
| PASS | [m1-life-flow] person references resolve | [] |
| PASS | [m1-resilience] creator ready |  |
| PASS | [m1-resilience] create failure surfaces retry |  |
| PASS | [m1-resilience] retry navigates to the new life |  |
| PASS | [m1-resilience] retry reused the operation: exactly one new life |  |
| PASS | [m1-resilience] creator reloaded |  |
| PASS | [m1-resilience] operation cleared after definitive success |  |
| PASS | [m1-resilience] created life Alpha |  |
| PASS | [m1-resilience] created life Beta |  |
| PASS | [m1-resilience] deep link opens game Alpha |  |
| PASS | [m1-resilience] life list visible |  |
| PASS | [m1-resilience] life list loaded with Beta |  |
| PASS | [m1-resilience] Resume selects Beta |  |
| PASS | [m1-resilience] query param consumed |  |
| PASS | [m1-resilience] refresh keeps selected game Beta |  |
| PASS | [m1-resilience] invalid deep link falls back to life list |  |
| PASS | [m1-resilience] resume rows show portraits |  |
| PASS | [m2a-s1-school-limits] scenario enrolled_child_10 accepted | game_id=g_6870c6 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| WARN | [m2a-s1-school-limits] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s1-school-limits] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] M2 person references match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] state.education.enrolled === true |  |
| PASS | [m2a-s1-school-limits] cmd study_harder | status=200 {"schema_version": 2, "game_id": "g_6870c66bd2", "revision": 18, "status": "alive", "player_id": "4c12b4b9-8c3f-47c7-9a43-df9f50835a89", "people": [{"id": "4c12 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| WARN | [m2a-s1-school-limits] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s1-school-limits] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] M2 person references match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] cmd study_harder | status=409 {"error": {"code": "not_allowed", "message": "you already studied this year"}} |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| PASS | [m2a-s1-school-limits] rejected repeat study made no mutation |  |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| PASS | [m2a-s1-school-limits] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6870c66bd2", "revision": 19, "status": "alive", "player_id": "4c12b4b9-8c3f-47c7-9a43-df9f50835a89", "people": [{"id": "4c12 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| PASS | [m2a-s1-school-limits] age advanced exactly once | before=10 after=11 |
| PASS | [m2a-s1-school-limits] turn settled (no pending) | revision=19 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_6870c6 |
| WARN | [m2a-s1-school-limits] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s1-school-limits] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] M2 person references match frozen shapes | [] |
| PASS | [m2a-s2-university-loan] scenario university_pending_17 accepted | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] university decision left open by scenario |  |
| PASS | [m2a-s2-university-loan] open university pending | kind=university_application |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_eaaa1949e3", "revision": 26, "status": "alive", "player_id": "906ab612-5004-49d0-9158-9b9bbb5a7d64", "people": [{"id": "906a |
| PASS | [m2a-s2-university-loan] resolved pending via 'Yes' | kind=university_application |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] awaited pending ('university_funding',) | kind=university_funding |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_eaaa1949e3", "revision": 27, "status": "alive", "player_id": "906ab612-5004-49d0-9158-9b9bbb5a7d64", "people": [{"id": "906a |
| PASS | [m2a-s2-university-loan] resolved pending via 'Student Loan' | kind=university_funding |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_eaaa1949e3", "revision": 28, "status": "alive", "player_id": "906ab612-5004-49d0-9158-9b9bbb5a7d64", "people": [{"id": "906a |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_eaaa1949e3", "revision": 29, "status": "alive", "player_id": "906ab612-5004-49d0-9158-9b9bbb5a7d64", "people": [{"id": "906a |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_eaaa1949e3", "revision": 30, "status": "alive", "player_id": "906ab612-5004-49d0-9158-9b9bbb5a7d64", "people": [{"id": "906a |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] age advanced exactly once | before=17 after=18 |
| PASS | [m2a-s2-university-loan] turn settled (no pending) | revision=30 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_eaaa19 |
| PASS | [m2a-s2-university-loan] student loan recorded for the financed year |  |
| WARN | [m2a-s2-university-loan] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s2-university-loan] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s2-university-loan] M2 person references match frozen shapes | [] |
| PASS | [m2a-s3-job-hours-income-ledger] scenario unemployed_18 accepted | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] no open pending decisions |  |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd find_job | status=200 {"schema_version": 2, "game_id": "g_bf1b25c972", "revision": 31, "status": "alive", "player_id": "6c9113ef-2178-4a8f-945a-ecfcea2a4680", "people": [{"id": "6c91 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_bf1b25c972", "revision": 32, "status": "alive", "player_id": "6c9113ef-2178-4a8f-945a-ecfcea2a4680", "people": [{"id": "6c91 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] employed after interview | salary=129965 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] employed after accepting stored offer |  |
| PASS | [m2a-s3-job-hours-income-ledger] cmd adjust_hours | status=200 {"schema_version": 2, "game_id": "g_bf1b25c972", "revision": 33, "status": "alive", "player_id": "6c9113ef-2178-4a8f-945a-ecfcea2a4680", "people": [{"id": "6c91 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] hours adjusted to 55 |  |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_bf1b25c972", "revision": 34, "status": "alive", "player_id": "6c9113ef-2178-4a8f-945a-ecfcea2a4680", "people": [{"id": "6c91 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] age advanced exactly once | before=18 after=19 |
| PASS | [m2a-s3-job-hours-income-ledger] turn settled (no pending) | revision=34 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_bf1b25 |
| PASS | [m2a-s3-job-hours-income-ledger] work_income legs match independent expectation | [] |
| PASS | [m2a-s3-job-hours-income-ledger] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s3-job-hours-income-ledger] M2 person references match frozen shapes | [] |
| PASS | [m2a-s4-retirement-pension] scenario retirement_eligible_65 accepted | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] scenario starts employed at 65 |  |
| PASS | [m2a-s4-retirement-pension] retirement gates met (10y+, age 65) |  |
| PASS | [m2a-s4-retirement-pension] cmd retire | status=200 {"schema_version": 2, "game_id": "g_61bdffbfde", "revision": 99, "status": "alive", "player_id": "b4498754-a27a-40a3-a984-d818aac1a5b2", "people": [{"id": "b449 |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] retire command accepted |  |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] retired flag set |  |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_61bdffbfde", "revision": 100, "status": "alive", "player_id": "b4498754-a27a-40a3-a984-d818aac1a5b2", "people": [{"id": "b44 |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] age advanced exactly once | before=65 after=66 |
| PASS | [m2a-s4-retirement-pension] turn settled (no pending) | revision=100 |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_61bdff |
| PASS | [m2a-s4-retirement-pension] pension gross equals projected pension | pension=90976 |
| WARN | [m2a-s4-retirement-pension] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s4-retirement-pension] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s4-retirement-pension] M2 person references match frozen shapes | [] |
| PASS | [m2a-s6-fired-for-performance] scenario low_performance_30 accepted | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] scenario starts employed with performance below 15 |  |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 51, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 52, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] resolved pending via 'Give the toast' | kind=flavor_wedding_toast |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 53, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 54, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] resolved pending via 'Start dating' | kind=dating_offer |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 55, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 56, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 57, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] resolved pending via 'Become its personal physician' | kind=flavor_office_plant |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 58, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 59, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 60, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] resolved pending via 'Accept the sacred duty' | kind=flavor_office_plantsitter |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 61, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 62, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 63, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 64, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] resolved pending via 'Eat the cake, compliment the cake' | kind=flavor_office_cake |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_acfe5d6476", "revision": 65, "status": "alive", "player_id": "ca320deb-7a10-4836-9f5a-1c2af54f791e", "people": [{"id": "ca32 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_acfe5d |
| PASS | [m2a-s6-fired-for-performance] seed exhausted without firing; retrying | next seed=4242 |
| PASS | [m2a-s6-fired-for-performance] scenario low_performance_30 accepted | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6628b81d6a", "revision": 46, "status": "alive", "player_id": "942d4524-dec6-4067-b101-66b010fece04", "people": [{"id": "942d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6628b81d6a", "revision": 47, "status": "alive", "player_id": "942d4524-dec6-4067-b101-66b010fece04", "people": [{"id": "942d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6628b81d6a", "revision": 48, "status": "alive", "player_id": "942d4524-dec6-4067-b101-66b010fece04", "people": [{"id": "942d |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] fired after 3 year(s) |  |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_6628b8 |
| PASS | [m2a-s6-fired-for-performance] fired: no longer employed | {"employed": false, "retired": false, "job_label": null, "hours": 0, "salary": 0, "effective_annual_gross": 0, "pension": 0, "performance": 0, "stress": 0, "yea |
| PASS | [m2a-s6-fired-for-performance] ledger integrity after firing | [] |
| PASS | [m2a-s6-fired-for-performance] boundary-year pay posted at most once | gross legs this turn: 0 |
| WARN | [m2a-s6-fired-for-performance] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s6-fired-for-performance] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s6-fired-for-performance] M2 person references match frozen shapes | [] |
| PASS | [m2b-s1-conversation] scenario married_30 accepted | game_id=g_0ae4bd |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_0ae4bd |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_0ae4bd |
| PASS | [m2b-s1-conversation] M2 state sections match frozen shapes | [] |
| PASS | [m2b-s1-conversation] M2 person references match frozen shapes | [] |
| PASS | [m2b-s1-conversation] partner id resolved | 109e7a4b |
| PASS | [m2b-s1-conversation] balance recorded | balance=708058 |
| PASS | [m2b-s1-conversation] cmd conversation | status=200 {"schema_version": 2, "game_id": "g_0ae4bddc92", "revision": 78, "status": "alive", "player_id": "56a71261-8d72-483b-8b4a-d1a196876c26", "people": [{"id": "56a7 |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_0ae4bd |
| PASS | [m2b-s1-conversation] M2 state sections match frozen shapes | [] |
| PASS | [m2b-s1-conversation] M2 person references match frozen shapes | [] |
| PASS | [m2b-s1-conversation] cmd conversation | status=200 {"schema_version": 2, "game_id": "g_0ae4bddc92", "revision": 79, "status": "alive", "player_id": "56a71261-8d72-483b-8b4a-d1a196876c26", "people": [{"id": "56a7 |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_0ae4bd |
| PASS | [m2b-s1-conversation] partner identity unchanged after conversations |  |
| PASS | [m2b-s2-wedding-venues] scenario married_30 accepted | game_id=g_f6659e |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_f6659e |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_f6659e |
| PASS | [m2b-s2-wedding-venues] cmd divorce | status=200 {"schema_version": 2, "game_id": "g_f6659e90d2", "revision": 78, "status": "alive", "player_id": "9fe456bb-c07e-4985-a3ce-685b7c46c9da", "people": [{"id": "9fe4 |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_f6659e |
| PASS | [m2b-s2-wedding-venues] divorced |  |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_f6659e |
| PASS | [m2b-s2-wedding-venues] action find_partner available |  |
| PASS | [m2b-s3-baby-naming] scenario married_30 accepted | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] no open pending decisions |  |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] cmd try_for_baby | status=200 {"schema_version": 2, "game_id": "g_57b6aeda56", "revision": 78, "status": "alive", "player_id": "e188e1e3-854b-4093-a143-bc3eedd2d6de", "people": [{"id": "e188 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] awaited pending ('pregnancy', 'baby_naming', 'birth_naming') | kind=pregnancy |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] pregnancy (keep/abort) or naming decision pending |  |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_57b6aeda56", "revision": 79, "status": "alive", "player_id": "e188e1e3-854b-4093-a143-bc3eedd2d6de", "people": [{"id": "e188 |
| PASS | [m2b-s3-baby-naming] resolved pending via 'Keep the baby' | kind=pregnancy |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_57b6aeda56", "revision": 80, "status": "alive", "player_id": "e188e1e3-854b-4093-a143-bc3eedd2d6de", "people": [{"id": "e188 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] awaited pending ('baby_naming', 'birth_naming') | kind=baby_naming |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] cmd resolve_decision | status=400 {"error": {"code": "invalid_input", "message": "name must be 1-40 characters"}} |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_57b6aeda56", "revision": 81, "status": "alive", "player_id": "e188e1e3-854b-4093-a143-bc3eedd2d6de", "people": [{"id": "e188 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_57b6ae |
| PASS | [m2b-s3-baby-naming] M2 state sections match frozen shapes | [] |
| PASS | [m2b-s3-baby-naming] M2 person references match frozen shapes | [] |
| PASS | [m2b-s3-baby-naming] baby created via canonical hook |  |
| PASS | [m2b-s4-adoption] scenario married_30 accepted | game_id=g_6966ac |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_6966ac |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_6966ac |
| PASS | [m2b-s4-adoption] balance recorded | balance=708058 |
| PASS | [m2b-s4-adoption] cmd adoption | status=200 {"schema_version": 2, "game_id": "g_6966ac31a0", "revision": 78, "status": "alive", "player_id": "dc0d5774-7533-474a-9cd1-4bae1bdc9332", "people": [{"id": "dc0d |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_6966ac |
| PASS | [m2b-s4-adoption] adoption offer is a stored decision |  |
| PASS | [m2b-s4-adoption] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6966ac31a0", "revision": 79, "status": "alive", "player_id": "dc0d5774-7533-474a-9cd1-4bae1bdc9332", "people": [{"id": "dc0d |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_6966ac |
| PASS | [m2b-s4-adoption] rename decision follows acceptance |  |
| PASS | [m2b-s4-adoption] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6966ac31a0", "revision": 80, "status": "alive", "player_id": "dc0d5774-7533-474a-9cd1-4bae1bdc9332", "people": [{"id": "dc0d |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_6966ac |
| PASS | [m2b-s4-adoption] M2 state sections match frozen shapes | [] |
| PASS | [m2b-s4-adoption] M2 person references match frozen shapes | [] |
| PASS | [m2b-s4-adoption] adopted child added |  |
| PASS | [m2b-s4-adoption] adoption cost within 10k-30k, posted once | new_rows=1 payment_rows=1 amount=-27686 [] |
| PASS | [m2b-s5-doctor-cures] scenario ill_with_cash accepted | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] scenario starts with two illnesses |  |
| PASS | [m2b-s5-doctor-cures] balance recorded | balance=781133 |
| PASS | [m2b-s5-doctor-cures] cmd doctor | status=200 {"schema_version": 2, "game_id": "g_82f29d8592", "revision": 51, "status": "alive", "player_id": "ed89ff44-3645-4bf5-9d7b-adbc9682c8f4", "people": [{"id": "ed89 |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] doctor visit is a stored decision |  |
| PASS | [m2b-s5-doctor-cures] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_82f29d8592", "revision": 52, "status": "alive", "player_id": "ed89ff44-3645-4bf5-9d7b-adbc9682c8f4", "people": [{"id": "ed89 |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] doctor fee $100 posted exactly once | new_rows=1 payment_rows=1 amount=-100 [] |
| PASS | [m2b-s5-doctor-cures] illnesses updated | illnesses=['Flu'] |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] no open pending decisions |  |
| PASS | [m2b-s5-doctor-cures] balance recorded | balance=781033 |
| PASS | [m2b-s5-doctor-cures] cmd doctor | status=200 {"schema_version": 2, "game_id": "g_82f29d8592", "revision": 53, "status": "alive", "player_id": "ed89ff44-3645-4bf5-9d7b-adbc9682c8f4", "people": [{"id": "ed89 |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_82f29d8592", "revision": 54, "status": "alive", "player_id": "ed89ff44-3645-4bf5-9d7b-adbc9682c8f4", "people": [{"id": "ed89 |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_82f29d |
| PASS | [m2b-s5-doctor-cures] illnesses updated | illnesses=[] |
| PASS | [m2b-s6-witch-doctor] scenario married_30 accepted | game_id=g_7974ed |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_7974ed |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_7974ed |
| PASS | [m2b-s6-witch-doctor] balance recorded | balance=708058 |
| PASS | [m2b-s6-witch-doctor] cmd witch_doctor | status=200 {"schema_version": 2, "game_id": "g_7974ed85bc", "revision": 78, "status": "alive", "player_id": "f9a3355c-d706-476b-a4b6-20ce50944a56", "people": [{"id": "f9a3 |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_7974ed |
| PASS | [m2b-s6-witch-doctor] witch doctor is a stored decision |  |
| PASS | [m2b-s6-witch-doctor] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7974ed85bc", "revision": 79, "status": "alive", "player_id": "f9a3355c-d706-476b-a4b6-20ce50944a56", "people": [{"id": "f9a3 |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_7974ed |
| PASS | [m2b-s6-witch-doctor] witch doctor fee within 50-100, once | new_rows=1 payment_rows=1 amount=-74 [] |
| PASS | [m2b-s7-funeral] scenario married_30 accepted | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] no open pending decisions |  |
| PASS | [m2b-s7-funeral] parents alive for funeral path |  |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 78, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 79, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 80, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 81, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 82, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 83, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] resolved pending via 'Doorbell-tomato everyone' | kind=flavor_garden_tomatoes |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 84, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 85, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] resolved pending via 'Go, nerves and all' | kind=flavor_first_date_nerves |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 86, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 87, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 88, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 89, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] resolved pending via 'Attend and take notes' | kind=flavor_meeting_that_email |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 90, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 91, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 92, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 93, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 94, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 95, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 96, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] resolved pending via 'Reach out and help them network' | kind=flavor_fired_peer |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 97, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 98, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 99, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5eda |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 100, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5ed |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] funeral decision after 23 year(s) | kind=funeral |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] balance recorded | balance=1818731 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7863f37d65", "revision": 101, "status": "alive", "player_id": "5eda256e-1a64-477b-aef1-7789dc7fbf24", "people": [{"id": "5ed |
| PASS | [m2b-s7-funeral] resolved pending via 'Attend the funeral' | kind=funeral |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_7863f3 |
| PASS | [m2b-s7-funeral] deceased archived and refs resolve |  |
| PASS | [m2b-s7-funeral] person references resolve | [] |
| PASS | [m2b-s7-funeral] M2 state sections match frozen shapes | [] |
| PASS | [m2b-s7-funeral] M2 person references match frozen shapes | [] |
| PASS | [m2b-r2-view-relation] scenario married_with_child accepted | game_id=g_e32814 |
| PASS | [m2b-r2-view-relation] refresh state | game_id=g_e32814 |
| PASS | [m2b-r2-view-relation] refresh state | game_id=g_e32814 |
| PASS | [m2b-r2-view-relation] M2 state sections match frozen shapes | [] |
| PASS | [m2b-r2-view-relation] M2 person references match frozen shapes | [] |
| PASS | [m2b-r2-view-relation] relation view data: partner, child, parents projected |  |
| PASS | [m2b-r2-view-relation] family refs explicit and non-empty |  |
| PASS | [m2b-r5-ask-for-money] scenario married_30 accepted | game_id=g_7f664f |
| PASS | [m2b-r5-ask-for-money] refresh state | game_id=g_7f664f |
| PASS | [m2b-r5-ask-for-money] refresh state | game_id=g_7f664f |
| PASS | [m2b-r5-ask-for-money] parent id resolved | 1533bc63 |
| PASS | [m2b-r5-ask-for-money] balance recorded | balance=708058 |
| PASS | [m2b-r5-ask-for-money] cmd ask_for_money | status=200 {"schema_version": 2, "game_id": "g_7f664f2ed4", "revision": 78, "status": "alive", "player_id": "133be8ec-8467-4efb-a45b-dc244dada87d", "people": [{"id": "133b |
| PASS | [m2b-r5-ask-for-money] refresh state | game_id=g_7f664f |
| PASS | [m2b-r5-ask-for-money] M2 state sections match frozen shapes | [] |
| PASS | [m2b-r5-ask-for-money] M2 person references match frozen shapes | [] |
| PASS | [m2b-r6-r7-compliment-insult] scenario married_30 accepted | game_id=g_9e0751 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_9e0751 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_9e0751 |
| PASS | [m2b-r6-r7-compliment-insult] partner id resolved | 99cb2426 |
| PASS | [m2b-r6-r7-compliment-insult] cmd compliment | status=200 {"schema_version": 2, "game_id": "g_9e0751edbb", "revision": 78, "status": "alive", "player_id": "7a874018-2bc5-4cce-9727-027edaa3a234", "people": [{"id": "7a87 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_9e0751 |
| PASS | [m2b-r6-r7-compliment-insult] cmd compliment | status=409 {"error": {"code": "not_allowed", "message": "you already complimented them this year"}} |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_9e0751 |
| PASS | [m2b-r6-r7-compliment-insult] cmd insult | status=200 {"schema_version": 2, "game_id": "g_9e0751edbb", "revision": 79, "status": "alive", "player_id": "7a874018-2bc5-4cce-9727-027edaa3a234", "people": [{"id": "7a87 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_9e0751 |
| PASS | [m2b-r6-r7-compliment-insult] M2 state sections match frozen shapes | [] |
| PASS | [m2b-r6-r7-compliment-insult] M2 person references match frozen shapes | [] |
| PASS | [m2b-r22-request-raise] scenario employed_30 accepted | game_id=g_75fd91 |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_75fd91 |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_75fd91 |
| PASS | [m2b-r22-request-raise] cmd request_raise | status=200 {"schema_version": 2, "game_id": "g_75fd91c021", "revision": 51, "status": "alive", "player_id": "bc05d86b-b9ac-4ae0-8f09-bbed5b638e8e", "people": [{"id": "bc05 |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_75fd91 |
| PASS | [m2b-r22-request-raise] cmd request_raise | status=409 {"error": {"code": "not_allowed", "message": "you already asked for a raise this year"}} |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_75fd91 |
| PASS | [m2b-r22-request-raise] no double raise effect (limit enforced) |  |
| PASS | [m2b-r76-child-yearly] scenario married_with_child accepted | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ecbdbadaaa", "revision": 78, "status": "alive", "player_id": "e35be5c7-bba7-4110-b7cf-eedaa74bfc74", "people": [{"id": "e35b |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] age advanced exactly once | before=30 after=31 |
| PASS | [m2b-r76-child-yearly] turn settled (no pending) | revision=78 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ecbdbadaaa", "revision": 79, "status": "alive", "player_id": "e35be5c7-bba7-4110-b7cf-eedaa74bfc74", "people": [{"id": "e35b |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ecbdbadaaa", "revision": 80, "status": "alive", "player_id": "e35be5c7-bba7-4110-b7cf-eedaa74bfc74", "people": [{"id": "e35b |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] age advanced exactly once | before=31 after=32 |
| PASS | [m2b-r76-child-yearly] turn settled (no pending) | revision=80 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_ecbdba |
| PASS | [m2b-r76-child-yearly] M2 state sections match frozen shapes | [] |
| PASS | [m2b-r76-child-yearly] M2 person references match frozen shapes | [] |
| PASS | [m2b-r76-child-yearly] child identity stable across years |  |
| PASS | [m2b-r35-plastic-surgery] scenario married_30 accepted | game_id=g_483c2c |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_483c2c |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_483c2c |
| PASS | [m2b-r35-plastic-surgery] balance recorded | balance=708058 |
| PASS | [m2b-r35-plastic-surgery] cmd plastic_surgery | status=200 {"schema_version": 2, "game_id": "g_483c2c82a6", "revision": 78, "status": "alive", "player_id": "e01e105d-3289-4c0f-83e8-66ecf5f7f34e", "people": [{"id": "e01e |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_483c2c |
| PASS | [m2b-r35-plastic-surgery] surgeon choice pending |  |
| PASS | [m2b-r35-plastic-surgery] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_483c2c82a6", "revision": 79, "status": "alive", "player_id": "e01e105d-3289-4c0f-83e8-66ecf5f7f34e", "people": [{"id": "e01e |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_483c2c |
| PASS | [m2b-r35-plastic-surgery] surgery $25,000 posted exactly once | new_rows=1 payment_rows=1 amount=-25000 [] |
| PASS | [m2b-r35-plastic-surgery] cmd plastic_surgery | status=409 {"error": {"code": "not_allowed", "message": "please wait a while before getting another plastic surgery"}} |
| PASS | [m2b-r61-vaccination] life created via API | game_id=g_8d71b3 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_8d71b3 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_8d71b3 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_8d71b3bc5e", "revision": 2, "status": "alive", "player_id": "037498c8-1dad-4f95-8c9f-0f7b8a68f5de", "people": [{"id": "03749 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_8d71b3 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_8d71b3bc5e", "revision": 3, "status": "alive", "player_id": "037498c8-1dad-4f95-8c9f-0f7b8a68f5de", "people": [{"id": "03749 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_8d71b3 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_8d71b3bc5e", "revision": 4, "status": "alive", "player_id": "037498c8-1dad-4f95-8c9f-0f7b8a68f5de", "people": [{"id": "03749 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_8d71b3 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_8d71b3bc5e", "revision": 5, "status": "alive", "player_id": "037498c8-1dad-4f95-8c9f-0f7b8a68f5de", "people": [{"id": "03749 |
| PASS | [m2b-r61-vaccination] seed missed the 1/2 roll; retrying | next seed=2 |
| PASS | [m2b-r61-vaccination] life created via API | game_id=g_806526 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_806526 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_806526 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_806526699e", "revision": 2, "status": "alive", "player_id": "813850b6-59bb-49f0-bd96-a5cdc2fba61f", "people": [{"id": "81385 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_806526 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_806526699e", "revision": 3, "status": "alive", "player_id": "813850b6-59bb-49f0-bd96-a5cdc2fba61f", "people": [{"id": "81385 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_806526 |
| PASS | [m2b-r61-vaccination] awaited pending ('vaccination_age2',) | kind=vaccination_age2 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_806526 |
| PASS | [m2b-r61-vaccination] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_806526699e", "revision": 4, "status": "alive", "player_id": "813850b6-59bb-49f0-bd96-a5cdc2fba61f", "people": [{"id": "81385 |
| PASS | [m2b-r61-vaccination] resolved pending via 'Try to stay calm' | kind=vaccination_age2 |
| PASS | [m2b-r61-vaccination] vaccination event exercised | seed=2 |
| PASS | [m2b-r58-60-illness-progression] scenario ill_with_cash accepted | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] scenario carries illnesses |  |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_791fab7c7f", "revision": 51, "status": "alive", "player_id": "570725e7-16c7-4f77-91d3-c9f3b8d2eff6", "people": [{"id": "5707 |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] age advanced exactly once | before=30 after=31 |
| PASS | [m2b-r58-60-illness-progression] turn settled (no pending) | revision=51 |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_791fab |
| PASS | [m2b-r58-60-illness-progression] illness projection stable after a year |  |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 91, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 92, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 93, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Become its personal physician' | kind=flavor_office_plant |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 94, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 95, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 96, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 97, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7ee60e |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_7ee60e852a", "revision": 98, "status": "alive", "player_id": "9bd7c44c-31ec-4420-9ea8-897452ed539b", "people": [{"id": "9bd7 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Reach out and help them network' | kind=flavor_fired_peer |
| PASS | [m2b-r69-kidnapping] seed 777: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 97, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa16 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 98, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa16 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Reach out and help them network' | kind=flavor_fired_peer |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 99, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa16 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 100, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa1 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 101, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa1 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 102, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa1 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Buy supplies and go for it ($20)' | kind=flavor_side_hustle |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 103, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa1 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_d97a46 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_d97a4604f2", "revision": 104, "status": "alive", "player_id": "fa162339-0fb8-43f5-97bf-194eb67430f2", "people": [{"id": "fa1 |
| PASS | [m2b-r69-kidnapping] seed 4242: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 77, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 78, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 79, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 80, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 81, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 82, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 83, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_7524c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_7524c651fa", "revision": 84, "status": "alive", "player_id": "ba94b02f-2cba-41ca-a0ca-b78ee21b0ba6", "people": [{"id": "ba94 |
| PASS | [m2b-r69-kidnapping] seed 1: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 90, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 91, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 92, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 93, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Invest $500' | kind=flavor_surprise_raise_pitch |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 94, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 95, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 96, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_fb4155 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_fb41554771", "revision": 97, "status": "alive", "player_id": "60edc037-367b-48ea-9619-1cbfc5e371e6", "people": [{"id": "60ed |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Give the toast' | kind=flavor_wedding_toast |
| PASS | [m2b-r69-kidnapping] seed 2: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 83, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 84, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 85, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Give the full speech, stapler story included' | kind=flavor_retirement_speech |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 86, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 87, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Attempt a five-course masterpiece' | kind=flavor_in_law_dinner |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 88, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 89, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f61013 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f610137a62", "revision": 90, "status": "alive", "player_id": "570cd67d-1587-4f44-9ec8-762694969c52", "people": [{"id": "570c |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Go ($25)' | kind=flavor_coworker_lunch |
| PASS | [m2b-r69-kidnapping] seed 3: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 73, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 74, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Settle in and listen' | kind=flavor_parent_advice |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 75, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 76, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Give up the weekend and go' | kind=flavor_skill_workshop |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 77, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 78, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 79, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_2406d7 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_2406d7a7d3", "revision": 80, "status": "alive", "player_id": "422bde56-10de-46ec-b2d8-1ed5abd1027e", "people": [{"id": "422b |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Attend and take notes' | kind=flavor_meeting_that_email |
| PASS | [m2b-r69-kidnapping] seed 42: no kidnapping in 8 years | 1/250 roll missed |
| PENDING | [m2b-r69-kidnapping] kidnapping (legacy ~1/500/yr with karma<=100) did not fire across 6 seeds x 8 years; deferred by Claude's written approval until A's force_event hook lands |  |
| PASS | [m2b-r51-inheritance] scenario married_30 accepted | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] no open pending decisions |  |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 78, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 79, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 80, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 81, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 82, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 83, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Doorbell-tomato everyone' | kind=flavor_garden_tomatoes |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 84, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 85, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Go, nerves and all' | kind=flavor_first_date_nerves |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 86, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 87, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 88, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 89, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Attend and take notes' | kind=flavor_meeting_that_email |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 90, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 91, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 92, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 93, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 94, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 95, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 96, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Reach out and help them network' | kind=flavor_fired_peer |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 97, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 98, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 99, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef7 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 100, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] funeral decision after 23 year(s) | kind=funeral |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] balance recorded | balance=1818731 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ae2634146a", "revision": 101, "status": "alive", "player_id": "eef7faa1-4cc7-4794-badd-91324aaabb52", "people": [{"id": "eef |
| PASS | [m2b-r51-inheritance] resolved pending via 'Attend the funeral' | kind=funeral |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] deceased archived and refs resolve |  |
| PASS | [m2b-r51-inheritance] person references resolve | [] |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ae2634 |
| PASS | [m2b-r51-inheritance] M2 state sections match frozen shapes | [] |
| PASS | [m2b-r51-inheritance] M2 person references match frozen shapes | [] |
| PASS | [m2c-s5-succession] scenario married_with_child accepted | game_id=g_2d481a |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] no open pending decisions |  |
| PASS | [m2c-s5-succession] balance recorded | balance=748674 |
| PASS | [m2c-s5-succession] cmd surrender | status=200 {"schema_version": 2, "game_id": "g_2d481a041e", "revision": 78, "status": "alive", "player_id": "f4fca2aa-b76c-44e2-9b34-447b6681db86", "people": [{"id": "f4fc |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_2d481a041e", "revision": 79, "status": "alive", "player_id": "f4fca2aa-b76c-44e2-9b34-447b6681db86", "people": [{"id": "f4fc |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_2d481a041e", "revision": 80, "status": "dead", "player_id": "f4fca2aa-b76c-44e2-9b34-447b6681db86", "people": [{"id": "f4fca |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] surrender leads to death state | status=dead |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] life ended |  |
| PASS | [m2c-s5-succession] eligible children listed (frozen shape) |  |
| PASS | [m2c-s5-succession] cmd continue_as | status=200 {"schema_version": 2, "game_id": "g_2d481a041e", "revision": 81, "status": "alive", "player_id": "048e8054-41cc-4112-8da5-1b0832df50e7", "people": [{"id": "048e |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| PASS | [m2c-s5-succession] continue_as: identity/generation preserved | player=True gen 1->2 age=9 |
| PASS | [m2c-s5-succession] same game_id after succession |  |
| PASS | [m2c-s5-succession] cash continuity recorded | balance 748674 -> 748674; completed_lives=1 |
| PASS | [m2c-s5-succession] person references resolve | [] |
| PASS | [m2c-s5-succession] refresh state | game_id=g_2d481a |
| WARN | [m2c-s5-succession] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2c-s5-succession] M2 state sections match frozen shapes | [] |
| PASS | [m2c-s5-succession] M2 person references match frozen shapes | [] |
