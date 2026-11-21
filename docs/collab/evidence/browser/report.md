# Browser check report

PASS: 1343 | FAIL: 19 | PENDING: 1 | WARN: 8

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
| PASS | [m1-life-flow] life created via API | game_id=g_c588b8 |
| PASS | [m1-life-flow] game view visible after create |  |
| PASS | [m1-life-flow] actions available |  |
| PASS | [m1-life-flow] player name shown |  |
| PASS | [m1-life-flow] refresh state | game_id=g_c588b8 |
| PASS | [m1-life-flow] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_c588b82c18", "revision": 2, "status": "alive", "player_id": "7def6f5d-7a0f-4df3-823f-079ba1caef7b", "people": [{"id": "7def6 |
| PASS | [m1-life-flow] refresh state | game_id=g_c588b8 |
| PASS | [m1-life-flow] refresh state | game_id=g_c588b8 |
| PASS | [m1-life-flow] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_c588b82c18", "revision": 3, "status": "alive", "player_id": "7def6f5d-7a0f-4df3-823f-079ba1caef7b", "people": [{"id": "7def6 |
| PASS | [m1-life-flow] refresh state | game_id=g_c588b8 |
| PASS | [m1-life-flow] reached age >= 2 | age=2 (server state) |
| PASS | [m1-life-flow] game view visible after reload |  |
| PASS | [m1-life-flow] refresh state | game_id=g_c588b8 |
| PASS | [m1-life-flow] age persisted across reload (>= 2) | age=2 (server state) |
| FAIL | [m1-life-flow] player avatar rendered | render endpoint healthy but avatar absent |
| PASS | [m1-life-flow] pending decision with participants | kind=vaccination_age2 n=1 |
| PASS | [m1-life-flow] participant fixture is non-empty | person_ids=1 |
| FAIL | [m1-life-flow] decision participants show portraits | person_ids=1 svgs=0 (never rendered) |
| FAIL | [m1-life-flow] flow aborted | step 14 ({"python": "decision_modal_walk"}): wait_js: decision resolves and modal closes (last=False) |
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
| PASS | [m2a-s1-school-limits] scenario enrolled_child_10 accepted | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| WARN | [m2a-s1-school-limits] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s1-school-limits] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] M2 person references match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] state.education.enrolled === true |  |
| PASS | [m2a-s1-school-limits] cmd study_harder | status=200 {"schema_version": 2, "game_id": "g_f9de897380", "revision": 22, "status": "alive", "player_id": "6df7dc33-dd98-44ce-8081-05a3a73227ad", "people": [{"id": "6df7 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| WARN | [m2a-s1-school-limits] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s1-school-limits] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] M2 person references match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] cmd study_harder | status=409 {"error": {"code": "not_allowed", "message": "you already studied this year"}} |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] rejected repeat study made no mutation |  |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f9de897380", "revision": 23, "status": "alive", "player_id": "6df7dc33-dd98-44ce-8081-05a3a73227ad", "people": [{"id": "6df7 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f9de897380", "revision": 24, "status": "alive", "player_id": "6df7dc33-dd98-44ce-8081-05a3a73227ad", "people": [{"id": "6df7 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f9de897380", "revision": 25, "status": "alive", "player_id": "6df7dc33-dd98-44ce-8081-05a3a73227ad", "people": [{"id": "6df7 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| PASS | [m2a-s1-school-limits] age advanced exactly once | before=10 after=11 |
| PASS | [m2a-s1-school-limits] turn settled (no pending) | revision=25 |
| PASS | [m2a-s1-school-limits] refresh state | game_id=g_f9de89 |
| WARN | [m2a-s1-school-limits] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s1-school-limits] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s1-school-limits] M2 person references match frozen shapes | [] |
| PASS | [m2a-s2-university-loan] scenario university_pending_17 accepted | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] university decision left open by scenario |  |
| PASS | [m2a-s2-university-loan] open university pending | kind=university_application |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 36, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] resolved pending via 'Yes' | kind=university_application |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] awaited pending ('university_funding',) | kind=university_funding |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 37, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] resolved pending via 'Student Loan' | kind=university_funding |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 38, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 39, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 40, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 41, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8be9783ba", "revision": 42, "status": "alive", "player_id": "5ac6ae34-80c8-4bb4-b32d-84854c428dac", "people": [{"id": "5ac6 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] age advanced exactly once | before=17 after=18 |
| PASS | [m2a-s2-university-loan] turn settled (no pending) | revision=42 |
| PASS | [m2a-s2-university-loan] refresh state | game_id=g_b8be97 |
| PASS | [m2a-s2-university-loan] student loan recorded for the financed year |  |
| WARN | [m2a-s2-university-loan] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s2-university-loan] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s2-university-loan] M2 person references match frozen shapes | [] |
| PASS | [m2a-s3-job-hours-income-ledger] scenario unemployed_18 accepted | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] no open pending decisions |  |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd find_job | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 39, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 40, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] employed after interview | salary=63454 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] employed after accepting stored offer |  |
| PASS | [m2a-s3-job-hours-income-ledger] cmd adjust_hours | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 41, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] hours adjusted to 55 |  |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 42, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 43, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 44, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b8e252c9c9", "revision": 45, "status": "alive", "player_id": "b59e31c3-6b5f-4fa2-9b68-b2e135536324", "people": [{"id": "b59e |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] age advanced exactly once | before=18 after=19 |
| PASS | [m2a-s3-job-hours-income-ledger] turn settled (no pending) | revision=45 |
| PASS | [m2a-s3-job-hours-income-ledger] refresh state | game_id=g_b8e252 |
| PASS | [m2a-s3-job-hours-income-ledger] work_income legs match independent expectation | [] |
| PASS | [m2a-s3-job-hours-income-ledger] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s3-job-hours-income-ledger] M2 person references match frozen shapes | [] |
| PASS | [m2a-s4-retirement-pension] scenario retirement_eligible_65 accepted | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] scenario starts employed at 65 |  |
| PASS | [m2a-s4-retirement-pension] retirement gates met (10y+, age 65) |  |
| PASS | [m2a-s4-retirement-pension] cmd retire | status=200 {"schema_version": 2, "game_id": "g_c3246f58f2", "revision": 120, "status": "alive", "player_id": "fa4cc8ad-8485-443e-ac43-149cf25c5b2a", "people": [{"id": "fa4 |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] retire command accepted |  |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] retired flag set |  |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_c3246f58f2", "revision": 121, "status": "alive", "player_id": "fa4cc8ad-8485-443e-ac43-149cf25c5b2a", "people": [{"id": "fa4 |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] age advanced exactly once | before=65 after=66 |
| PASS | [m2a-s4-retirement-pension] turn settled (no pending) | revision=121 |
| PASS | [m2a-s4-retirement-pension] refresh state | game_id=g_c3246f |
| PASS | [m2a-s4-retirement-pension] pension gross equals projected pension | pension=44418 |
| WARN | [m2a-s4-retirement-pension] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s4-retirement-pension] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s4-retirement-pension] M2 person references match frozen shapes | [] |
| PASS | [m2a-s6-fired-for-performance] scenario low_performance_30 accepted | game_id=g_74fe4a |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_74fe4a |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_74fe4a |
| PASS | [m2a-s6-fired-for-performance] scenario starts employed with performance below 15 |  |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_74fe4a |
| PASS | [m2a-s6-fired-for-performance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_74fe4a44ca", "revision": 66, "status": "alive", "player_id": "1017e790-4f8e-4761-8083-dc11da339c32", "people": [{"id": "1017 |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_74fe4a |
| PASS | [m2a-s6-fired-for-performance] fired after 1 year(s) |  |
| PASS | [m2a-s6-fired-for-performance] refresh state | game_id=g_74fe4a |
| PASS | [m2a-s6-fired-for-performance] fired: no longer employed | {"employed": false, "retired": false, "job_label": null, "hours": 0, "salary": 0, "effective_annual_gross": 0, "pension": 0, "performance": 0, "stress": 0, "yea |
| PASS | [m2a-s6-fired-for-performance] ledger integrity after firing | [] |
| PASS | [m2a-s6-fired-for-performance] boundary-year pay posted at most once | gross legs this turn: 0 |
| WARN | [m2a-s6-fired-for-performance] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2a-s6-fired-for-performance] M2 state sections match frozen shapes | [] |
| PASS | [m2a-s6-fired-for-performance] M2 person references match frozen shapes | [] |
| PASS | [m2b-s1-conversation] scenario married_30 accepted | game_id=g_d5ee41 |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_d5ee41 |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_d5ee41 |
| FAIL | [m2b-s1-conversation] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:1cdee99f-fe4c-4f4f-948c-3c8713ce51fd:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-s1-conversation] M2 person references match frozen shapes | [] |
| PASS | [m2b-s1-conversation] partner id resolved | cfe5c5a2 |
| PASS | [m2b-s1-conversation] balance recorded | balance=401480 |
| PASS | [m2b-s1-conversation] cmd conversation | status=200 {"schema_version": 2, "game_id": "g_d5ee41060f", "revision": 92, "status": "alive", "player_id": "1cdee99f-fe4c-4f4f-948c-3c8713ce51fd", "people": [{"id": "1cde |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_d5ee41 |
| FAIL | [m2b-s1-conversation] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:1cdee99f-fe4c-4f4f-948c-3c8713ce51fd:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-s1-conversation] M2 person references match frozen shapes | [] |
| PASS | [m2b-s1-conversation] cmd conversation | status=200 {"schema_version": 2, "game_id": "g_d5ee41060f", "revision": 93, "status": "alive", "player_id": "1cdee99f-fe4c-4f4f-948c-3c8713ce51fd", "people": [{"id": "1cde |
| PASS | [m2b-s1-conversation] refresh state | game_id=g_d5ee41 |
| PASS | [m2b-s1-conversation] partner identity unchanged after conversations |  |
| PASS | [m2b-s2-wedding-venues] scenario married_30 accepted | game_id=g_570c9c |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_570c9c |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_570c9c |
| PASS | [m2b-s2-wedding-venues] cmd divorce | status=200 {"schema_version": 2, "game_id": "g_570c9c1710", "revision": 92, "status": "alive", "player_id": "5825d07a-3597-4838-8f14-4e9ef35e7b3c", "people": [{"id": "5825 |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_570c9c |
| PASS | [m2b-s2-wedding-venues] divorced |  |
| PASS | [m2b-s2-wedding-venues] refresh state | game_id=g_570c9c |
| PASS | [m2b-s2-wedding-venues] action find_partner available |  |
| PASS | [m2b-s3-baby-naming] scenario married_30 accepted | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] no open pending decisions |  |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] cmd try_for_baby | status=200 {"schema_version": 2, "game_id": "g_0499b7cb35", "revision": 92, "status": "alive", "player_id": "6546009a-05e8-4a8e-916e-5f28117bc6bf", "people": [{"id": "6546 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] awaited pending ('pregnancy', 'baby_naming', 'birth_naming') | kind=pregnancy |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] pregnancy (keep/abort) or naming decision pending |  |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0499b7cb35", "revision": 93, "status": "alive", "player_id": "6546009a-05e8-4a8e-916e-5f28117bc6bf", "people": [{"id": "6546 |
| PASS | [m2b-s3-baby-naming] resolved pending via 'Keep the baby' | kind=pregnancy |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_0499b7cb35", "revision": 94, "status": "alive", "player_id": "6546009a-05e8-4a8e-916e-5f28117bc6bf", "people": [{"id": "6546 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] awaited pending ('baby_naming', 'birth_naming') | kind=baby_naming |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] cmd resolve_decision | status=400 {"error": {"code": "invalid_input", "message": "name must be 1-40 characters"}} |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| PASS | [m2b-s3-baby-naming] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0499b7cb35", "revision": 95, "status": "alive", "player_id": "6546009a-05e8-4a8e-916e-5f28117bc6bf", "people": [{"id": "6546 |
| PASS | [m2b-s3-baby-naming] refresh state | game_id=g_0499b7 |
| FAIL | [m2b-s3-baby-naming] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:6546009a-05e8-4a8e-916e-5f28117bc6bf:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-s3-baby-naming] M2 person references match frozen shapes | [] |
| PASS | [m2b-s3-baby-naming] baby created via canonical hook |  |
| PASS | [m2b-s4-adoption] scenario married_30 accepted | game_id=g_068ca8 |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_068ca8 |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_068ca8 |
| PASS | [m2b-s4-adoption] balance recorded | balance=401480 |
| PASS | [m2b-s4-adoption] cmd adoption | status=200 {"schema_version": 2, "game_id": "g_068ca823b8", "revision": 92, "status": "alive", "player_id": "85862ecb-7fb7-4aa6-b380-cb78d6d0beca", "people": [{"id": "8586 |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_068ca8 |
| PASS | [m2b-s4-adoption] adoption offer is a stored decision |  |
| PASS | [m2b-s4-adoption] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_068ca823b8", "revision": 93, "status": "alive", "player_id": "85862ecb-7fb7-4aa6-b380-cb78d6d0beca", "people": [{"id": "8586 |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_068ca8 |
| PASS | [m2b-s4-adoption] rename decision follows acceptance |  |
| PASS | [m2b-s4-adoption] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_068ca823b8", "revision": 94, "status": "alive", "player_id": "85862ecb-7fb7-4aa6-b380-cb78d6d0beca", "people": [{"id": "8586 |
| PASS | [m2b-s4-adoption] refresh state | game_id=g_068ca8 |
| FAIL | [m2b-s4-adoption] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:85862ecb-7fb7-4aa6-b380-cb78d6d0beca:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape", "finances.ledger: row 37: source_key 't_000 |
| PASS | [m2b-s4-adoption] M2 person references match frozen shapes | [] |
| PASS | [m2b-s4-adoption] adopted child added |  |
| FAIL | [m2b-s4-adoption] adoption cost within 10k-30k, posted once | new_rows=1 payment_rows=0 ["row 0: source_key 't_000092:family_events:adoption#46fb76c6-113e-406f-929d-888900e0fc5f:85862ecb-7fb7-4aa6-b380-cb78d6 |
| PASS | [m2b-s5-doctor-cures] scenario ill_with_cash accepted | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] scenario starts with two illnesses |  |
| PASS | [m2b-s5-doctor-cures] balance recorded | balance=361048 |
| PASS | [m2b-s5-doctor-cures] cmd doctor | status=200 {"schema_version": 2, "game_id": "g_0e7914dfc3", "revision": 68, "status": "alive", "player_id": "400efb74-9ee8-410f-b925-7451ad987fe6", "people": [{"id": "400e |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] doctor visit is a stored decision |  |
| PASS | [m2b-s5-doctor-cures] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0e7914dfc3", "revision": 69, "status": "alive", "player_id": "400efb74-9ee8-410f-b925-7451ad987fe6", "people": [{"id": "400e |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| FAIL | [m2b-s5-doctor-cures] doctor fee $100 posted exactly once | new_rows=1 payment_rows=1 amount=-100 ["row 0: source_key 't_000068:health:doctor#2127fdd4-afbd-4412-81f9-14016d9fa7e1:400efb74-9ee8-410f-b925-7451ad987fe6:fe |
| PASS | [m2b-s5-doctor-cures] illnesses updated | illnesses=['Flu'] |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] no open pending decisions |  |
| PASS | [m2b-s5-doctor-cures] balance recorded | balance=360948 |
| PASS | [m2b-s5-doctor-cures] cmd doctor | status=200 {"schema_version": 2, "game_id": "g_0e7914dfc3", "revision": 70, "status": "alive", "player_id": "400efb74-9ee8-410f-b925-7451ad987fe6", "people": [{"id": "400e |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0e7914dfc3", "revision": 71, "status": "alive", "player_id": "400efb74-9ee8-410f-b925-7451ad987fe6", "people": [{"id": "400e |
| PASS | [m2b-s5-doctor-cures] refresh state | game_id=g_0e7914 |
| PASS | [m2b-s5-doctor-cures] illnesses updated | illnesses=[] |
| PASS | [m2b-s6-witch-doctor] scenario married_30 accepted | game_id=g_0a1fb4 |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_0a1fb4 |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_0a1fb4 |
| PASS | [m2b-s6-witch-doctor] balance recorded | balance=401480 |
| PASS | [m2b-s6-witch-doctor] cmd witch_doctor | status=200 {"schema_version": 2, "game_id": "g_0a1fb4f09c", "revision": 92, "status": "alive", "player_id": "1c1626c7-23d1-4bee-a95e-79a3379ba2dc", "people": [{"id": "1c16 |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_0a1fb4 |
| PASS | [m2b-s6-witch-doctor] witch doctor is a stored decision |  |
| PASS | [m2b-s6-witch-doctor] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0a1fb4f09c", "revision": 93, "status": "alive", "player_id": "1c1626c7-23d1-4bee-a95e-79a3379ba2dc", "people": [{"id": "1c16 |
| PASS | [m2b-s6-witch-doctor] refresh state | game_id=g_0a1fb4 |
| FAIL | [m2b-s6-witch-doctor] witch doctor fee within 50-100, once | new_rows=1 payment_rows=1 amount=-56 ["row 0: source_key 't_000092:health:witch_doctor#4ae8b14f-222d-4e75-a242-50a4cf49485c:1c1626c7-23d1-4bee-a95e-79a3379ba |
| PASS | [m2b-s7-funeral] scenario married_30 accepted | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] no open pending decisions |  |
| PASS | [m2b-s7-funeral] parents alive for funeral path |  |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 92, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 93, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] resolved pending via 'Agree — open books' | kind=dra_phones_open |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 94, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] resolved pending via 'A day trip somewhere new' | kind=rom_anniversary_celebration |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 95, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] resolved pending via 'Go, nerves and all' | kind=flavor_first_date_nerves |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 96, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 97, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] resolved pending via 'Take their hand and reassure them' | kind=dra_jealous_glance |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 98, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] resolved pending via 'Say the first sorry' | kind=rom_makeup_apology |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 99, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c5 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 100, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Suggest you both walk it off' | kind=dra_public_argument |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 101, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Ask for the whole truth' | kind=rom_money_fight_secret_debt |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 102, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Buy supplies and go for it ($20)' | kind=flavor_side_hustle |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 103, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 104, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Be completely honest, even the awkward parts' | kind=dra_open_checkin |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 105, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Cherish every second' | kind=rom_anniversary_remembered |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 106, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Give the full speech, stapler story included' | kind=flavor_retirement_speech |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 107, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 108, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Offer a half-true version' | kind=dra_nearly_caught |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 109, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Let it slide off' | kind=rom_inlaws_comment |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 110, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Join a gym' | kind=healthy_lifestyle |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 111, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 112, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Charm offensive: wine and compliments' | kind=dra_meddling_inlaw |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 113, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Bring home flowers' | kind=rom_makeup_gesture |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 114, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 115, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Throw the budget overboard' | kind=dra_honeymoon_spat |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 116, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Receive it warmly' | kind=rom_makeup_slow_thaw |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 117, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 118, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Forgive, honestly' | kind=dra_partner_confesses |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 119, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Game on' | kind=rom_money_team_up |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 120, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 121, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Book the quiet cabin' | kind=dra_rebuild_weekend |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 122, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Find my partner immediately' | kind=rom_flirt_while_partnered |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 123, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Pivot to an emergency pizza feast' | kind=flavor_burnt_holiday |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 124, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 125, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Confess the almost' | kind=dra_confess_temptation |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 126, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Dance badly, happily' | kind=rom_chemistry_dance_floor |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 127, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Doorbell-tomato everyone' | kind=flavor_garden_tomatoes |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 128, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 129, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Grand gesture, immediate' | kind=dra_forgot_anniversary |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 130, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Propose splitting the day' | kind=rom_inlaws_holiday |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 131, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Attempt a five-course masterpiece' | kind=flavor_in_law_dinner |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 132, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 133, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Ask about it in the morning, calmly' | kind=dra_suspicious_text |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 134, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Lean into the moment' | kind=rom_chemistry_spark |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 135, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Drop everything and dance' | kind=flavor_kitchen_dancing |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 136, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 137, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Introduce them properly' | kind=dra_partner_jealous_friend |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 138, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Savour the quiet' | kind=rom_chemistry_slow_burn |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 139, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Reach out and help them network' | kind=flavor_fired_peer |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 140, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 141, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Sit down and plan together' | kind=rom_money_fight_bill |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 142, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 143, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Turn on the charm' | kind=rom_inlaws_dinner |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 144, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 145, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Make real amends' | kind=rom_anniversary_forgot |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 146, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 147, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Ask about it calmly' | kind=rom_money_fight_splurge |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 148, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 149, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 150, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Show up with gloves and a truck' | kind=flavor_sibling_moving_help |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 151, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| FAIL | [m2b-s7-funeral] funeral decision reached | no family death in 60 seeded years |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] balance recorded | balance=1026836 |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3cd8fe0f2e", "revision": 152, "status": "alive", "player_id": "d9c54974-e553-410f-abe0-163a67deead1", "people": [{"id": "d9c |
| PASS | [m2b-s7-funeral] resolved pending via 'Attend the funeral' | kind=funeral |
| PASS | [m2b-s7-funeral] refresh state | game_id=g_3cd8fe |
| PASS | [m2b-s7-funeral] deceased archived and refs resolve |  |
| PASS | [m2b-s7-funeral] person references resolve | [] |
| FAIL | [m2b-s7-funeral] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:d9c54974-e553-410f-abe0-163a67deead1:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-s7-funeral] M2 person references match frozen shapes | [] |
| PASS | [m2b-r2-view-relation] scenario married_with_child accepted | game_id=g_efb093 |
| PASS | [m2b-r2-view-relation] refresh state | game_id=g_efb093 |
| PASS | [m2b-r2-view-relation] refresh state | game_id=g_efb093 |
| FAIL | [m2b-r2-view-relation] M2 state sections match frozen shapes | ["finances.ledger: row 13: source_key 't_000075:relationships:wedding#drv-dec-78-venue:courthouse:39fee436-54d4-485f-949c-567f78804e49:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-r2-view-relation] M2 person references match frozen shapes | [] |
| PASS | [m2b-r2-view-relation] relation view data: partner, child, parents projected |  |
| PASS | [m2b-r2-view-relation] family refs explicit and non-empty |  |
| PASS | [m2b-r5-ask-for-money] scenario married_30 accepted | game_id=g_81c973 |
| PASS | [m2b-r5-ask-for-money] refresh state | game_id=g_81c973 |
| PASS | [m2b-r5-ask-for-money] refresh state | game_id=g_81c973 |
| PASS | [m2b-r5-ask-for-money] parent id resolved | d4d96344 |
| PASS | [m2b-r5-ask-for-money] balance recorded | balance=401480 |
| PASS | [m2b-r5-ask-for-money] cmd ask_for_money | status=200 {"schema_version": 2, "game_id": "g_81c9733506", "revision": 92, "status": "alive", "player_id": "6f5d22e0-c263-46fb-aa00-78647c56a027", "people": [{"id": "6f5d |
| PASS | [m2b-r5-ask-for-money] refresh state | game_id=g_81c973 |
| FAIL | [m2b-r5-ask-for-money] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:6f5d22e0-c263-46fb-aa00-78647c56a027:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape", "finances.ledger: row 37: source_key 't_000 |
| PASS | [m2b-r5-ask-for-money] M2 person references match frozen shapes | [] |
| PASS | [m2b-r6-r7-compliment-insult] scenario married_30 accepted | game_id=g_ad9e89 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_ad9e89 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_ad9e89 |
| PASS | [m2b-r6-r7-compliment-insult] partner id resolved | 30dd6f5a |
| PASS | [m2b-r6-r7-compliment-insult] cmd compliment | status=200 {"schema_version": 2, "game_id": "g_ad9e89f68a", "revision": 92, "status": "alive", "player_id": "d776e76c-cc75-4d14-a630-9d9f0133ce6c", "people": [{"id": "d776 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_ad9e89 |
| PASS | [m2b-r6-r7-compliment-insult] cmd compliment | status=409 {"error": {"code": "not_allowed", "message": "you already complimented them this year"}} |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_ad9e89 |
| PASS | [m2b-r6-r7-compliment-insult] cmd insult | status=200 {"schema_version": 2, "game_id": "g_ad9e89f68a", "revision": 93, "status": "alive", "player_id": "d776e76c-cc75-4d14-a630-9d9f0133ce6c", "people": [{"id": "d776 |
| PASS | [m2b-r6-r7-compliment-insult] refresh state | game_id=g_ad9e89 |
| FAIL | [m2b-r6-r7-compliment-insult] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:d776e76c-cc75-4d14-a630-9d9f0133ce6c:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-r6-r7-compliment-insult] M2 person references match frozen shapes | [] |
| PASS | [m2b-r22-request-raise] scenario employed_30 accepted | game_id=g_1bf828 |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_1bf828 |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_1bf828 |
| PASS | [m2b-r22-request-raise] cmd request_raise | status=200 {"schema_version": 2, "game_id": "g_1bf8286c39", "revision": 68, "status": "alive", "player_id": "f5b5907f-f477-4bb6-ba0d-088d4283aee9", "people": [{"id": "f5b5 |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_1bf828 |
| PASS | [m2b-r22-request-raise] cmd request_raise | status=409 {"error": {"code": "not_allowed", "message": "you already asked for a raise this year"}} |
| PASS | [m2b-r22-request-raise] refresh state | game_id=g_1bf828 |
| PASS | [m2b-r22-request-raise] no double raise effect (limit enforced) |  |
| PASS | [m2b-r76-child-yearly] scenario married_with_child accepted | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 99, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acfe |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 100, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 101, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 102, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] age advanced exactly once | before=30 after=31 |
| PASS | [m2b-r76-child-yearly] turn settled (no pending) | revision=102 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 103, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 104, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 105, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_685627b80d", "revision": 106, "status": "alive", "player_id": "acfe44bc-32a4-451f-a1b0-1a27c86d4648", "people": [{"id": "acf |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| PASS | [m2b-r76-child-yearly] age advanced exactly once | before=31 after=32 |
| PASS | [m2b-r76-child-yearly] turn settled (no pending) | revision=106 |
| PASS | [m2b-r76-child-yearly] refresh state | game_id=g_685627 |
| FAIL | [m2b-r76-child-yearly] M2 state sections match frozen shapes | ["finances.ledger: row 13: source_key 't_000075:relationships:wedding#drv-dec-78-venue:courthouse:acfe44bc-32a4-451f-a1b0-1a27c86d4648:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-r76-child-yearly] M2 person references match frozen shapes | [] |
| PASS | [m2b-r76-child-yearly] child identity stable across years |  |
| PASS | [m2b-r35-plastic-surgery] scenario married_30 accepted | game_id=g_b5620c |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_b5620c |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_b5620c |
| PASS | [m2b-r35-plastic-surgery] balance recorded | balance=401480 |
| PASS | [m2b-r35-plastic-surgery] cmd plastic_surgery | status=200 {"schema_version": 2, "game_id": "g_b5620c058c", "revision": 92, "status": "alive", "player_id": "bbb0af38-474a-402e-b9eb-62f988049870", "people": [{"id": "bbb0 |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_b5620c |
| PASS | [m2b-r35-plastic-surgery] surgeon choice pending |  |
| PASS | [m2b-r35-plastic-surgery] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_b5620c058c", "revision": 93, "status": "alive", "player_id": "bbb0af38-474a-402e-b9eb-62f988049870", "people": [{"id": "bbb0 |
| PASS | [m2b-r35-plastic-surgery] refresh state | game_id=g_b5620c |
| FAIL | [m2b-r35-plastic-surgery] surgery $25,000 posted exactly once | new_rows=1 payment_rows=1 amount=-25000 ["row 0: source_key 't_000092:health:plastic_surgery#dd45c960-1225-40f9-b3c1-7638846a09bd:bbb0af38-474a-402e-b9eb-62f988 |
| PASS | [m2b-r35-plastic-surgery] cmd plastic_surgery | status=409 {"error": {"code": "not_allowed", "message": "please wait a while before getting another plastic surgery"}} |
| PASS | [m2b-r61-vaccination] life created via API | game_id=g_4bde6f |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_4bde6f |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_4bde6f |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_4bde6fa123", "revision": 2, "status": "alive", "player_id": "2bc7a1ef-59e0-44a3-a972-21af7fcf5512", "people": [{"id": "2bc7a |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_4bde6f |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_4bde6fa123", "revision": 3, "status": "alive", "player_id": "2bc7a1ef-59e0-44a3-a972-21af7fcf5512", "people": [{"id": "2bc7a |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_4bde6f |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_4bde6fa123", "revision": 4, "status": "alive", "player_id": "2bc7a1ef-59e0-44a3-a972-21af7fcf5512", "people": [{"id": "2bc7a |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_4bde6f |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_4bde6fa123", "revision": 5, "status": "alive", "player_id": "2bc7a1ef-59e0-44a3-a972-21af7fcf5512", "people": [{"id": "2bc7a |
| PASS | [m2b-r61-vaccination] seed missed the 1/2 roll; retrying | next seed=2 |
| PASS | [m2b-r61-vaccination] life created via API | game_id=g_bce504 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_bce504 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_bce504 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_bce504d5d6", "revision": 2, "status": "alive", "player_id": "99565888-bdb2-434c-98f2-bdc285bb9a08", "people": [{"id": "99565 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_bce504 |
| PASS | [m2b-r61-vaccination] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_bce504d5d6", "revision": 3, "status": "alive", "player_id": "99565888-bdb2-434c-98f2-bdc285bb9a08", "people": [{"id": "99565 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_bce504 |
| PASS | [m2b-r61-vaccination] awaited pending ('vaccination_age2',) | kind=vaccination_age2 |
| PASS | [m2b-r61-vaccination] refresh state | game_id=g_bce504 |
| PASS | [m2b-r61-vaccination] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_bce504d5d6", "revision": 4, "status": "alive", "player_id": "99565888-bdb2-434c-98f2-bdc285bb9a08", "people": [{"id": "99565 |
| PASS | [m2b-r61-vaccination] resolved pending via 'Try to stay calm' | kind=vaccination_age2 |
| PASS | [m2b-r61-vaccination] vaccination event exercised | seed=2 |
| PASS | [m2b-r58-60-illness-progression] scenario ill_with_cash accepted | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] scenario carries illnesses |  |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_a52cb68978", "revision": 68, "status": "alive", "player_id": "d55c8499-3dbf-48b3-9dfb-fd2df5c471dc", "people": [{"id": "d55c |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] age advanced exactly once | before=30 after=31 |
| PASS | [m2b-r58-60-illness-progression] turn settled (no pending) | revision=68 |
| PASS | [m2b-r58-60-illness-progression] refresh state | game_id=g_a52cb6 |
| PASS | [m2b-r58-60-illness-progression] illness projection stable after a year |  |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 113, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 114, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Offer a half-true version' | kind=dra_nearly_caught |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 115, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Ask for the whole truth' | kind=rom_money_fight_secret_debt |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 116, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 117, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Ask about it in the morning, calmly' | kind=dra_suspicious_text |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 118, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Lean into the moment' | kind=rom_chemistry_spark |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 119, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Update everything and network' | kind=flavor_layoff_scare |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_f70a32 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_f70a3270b9", "revision": 120, "status": "alive", "player_id": "4ab8b156-7382-4690-a058-aa555aa10d8e", "people": [{"id": "4ab |
| PASS | [m2b-r69-kidnapping] seed 777: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 113, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 114, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Confess the almost' | kind=dra_confess_temptation |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 115, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Bring home flowers' | kind=rom_makeup_gesture |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 116, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Drop a few dollars in the hat' | kind=flavor_street_performer |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 117, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 118, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Throw the budget overboard' | kind=dra_honeymoon_spat |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 119, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Find my partner immediately' | kind=rom_flirt_while_partnered |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_001403 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_0014034d92", "revision": 120, "status": "alive", "player_id": "463ff43f-e97f-4e8a-a1d0-4672ade62079", "people": [{"id": "463 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Lend the money' | kind=old_friend_loan |
| PASS | [m2b-r69-kidnapping] seed 4242: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 107, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 108, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Forgive, honestly' | kind=dra_partner_confesses |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 109, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Cherish every second' | kind=rom_anniversary_remembered |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 110, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 111, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Ask about it in the morning, calmly' | kind=dra_suspicious_text |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 112, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Dance badly, happily' | kind=rom_chemistry_dance_floor |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 113, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_ccf725 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ccf725d9d7", "revision": 114, "status": "alive", "player_id": "ef656e0d-d38f-49e3-a846-4f5bcb937d6d", "people": [{"id": "ef6 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Suggest you both walk it off' | kind=dra_public_argument |
| PASS | [m2b-r69-kidnapping] seed 1: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 111, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 112, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Grand gesture, immediate' | kind=dra_forgot_anniversary |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 113, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Savour the quiet' | kind=rom_chemistry_slow_burn |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 114, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 115, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Agree — open books' | kind=dra_phones_open |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 116, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Make real amends' | kind=rom_anniversary_forgot |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 117, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_89835f |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_89835f759b", "revision": 118, "status": "alive", "player_id": "8c947864-7a8b-4d72-81b4-a30009773c09", "people": [{"id": "8c9 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Ask about it in the morning, calmly' | kind=dra_suspicious_text |
| PASS | [m2b-r69-kidnapping] seed 2: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 117, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 118, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Throw the budget overboard' | kind=dra_honeymoon_spat |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 119, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Sit down and plan together' | kind=rom_money_fight_bill |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 120, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 121, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Charm offensive: wine and compliments' | kind=dra_meddling_inlaw |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 122, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Receive it warmly' | kind=rom_makeup_slow_thaw |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 123, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Drop a few dollars in the hat' | kind=flavor_street_performer |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_9da8c6 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_9da8c6fa5b", "revision": 124, "status": "alive", "player_id": "dfff7fbe-8105-4449-8cbb-2beec2295cd4", "people": [{"id": "dff |
| PASS | [m2b-r69-kidnapping] seed 3: no kidnapping in 8 years | 1/250 roll missed |
| PASS | [m2b-r69-kidnapping] scenario wealthy_married_40 accepted | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 116, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 117, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Suggest you both walk it off' | kind=dra_public_argument |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 118, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Sit down and plan together' | kind=rom_money_fight_bill |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 119, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Show up with gloves and a truck' | kind=flavor_sibling_moving_help |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 120, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 121, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Offer a half-true version' | kind=dra_nearly_caught |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 122, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] resolved pending via 'Lean into the moment' | kind=rom_chemistry_spark |
| PASS | [m2b-r69-kidnapping] refresh state | game_id=g_6818b9 |
| PASS | [m2b-r69-kidnapping] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_6818b94328", "revision": 123, "status": "alive", "player_id": "e17220d5-a06d-4363-b37b-59e2ddef07e0", "people": [{"id": "e17 |
| PASS | [m2b-r69-kidnapping] seed 42: no kidnapping in 8 years | 1/250 roll missed |
| PENDING | [m2b-r69-kidnapping] kidnapping (legacy ~1/500/yr with karma<=100) did not fire across 6 seeds x 8 years; deferred by Claude's written approval until A's force_event hook lands |  |
| PASS | [m2b-r51-inheritance] scenario married_30 accepted | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] no open pending decisions |  |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 92, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 93, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] resolved pending via 'Agree — open books' | kind=dra_phones_open |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 94, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] resolved pending via 'A day trip somewhere new' | kind=rom_anniversary_celebration |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 95, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] resolved pending via 'Go, nerves and all' | kind=flavor_first_date_nerves |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 96, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 97, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] resolved pending via 'Take their hand and reassure them' | kind=dra_jealous_glance |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 98, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] resolved pending via 'Say the first sorry' | kind=rom_makeup_apology |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 99, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9e |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 100, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Suggest you both walk it off' | kind=dra_public_argument |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 101, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Ask for the whole truth' | kind=rom_money_fight_secret_debt |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 102, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Buy supplies and go for it ($20)' | kind=flavor_side_hustle |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 103, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 104, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Be completely honest, even the awkward parts' | kind=dra_open_checkin |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 105, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Cherish every second' | kind=rom_anniversary_remembered |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 106, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Give the full speech, stapler story included' | kind=flavor_retirement_speech |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 107, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 108, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Offer a half-true version' | kind=dra_nearly_caught |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 109, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Let it slide off' | kind=rom_inlaws_comment |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 110, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Join a gym' | kind=healthy_lifestyle |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 111, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 112, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Charm offensive: wine and compliments' | kind=dra_meddling_inlaw |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 113, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Bring home flowers' | kind=rom_makeup_gesture |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 114, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 115, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Throw the budget overboard' | kind=dra_honeymoon_spat |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 116, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Receive it warmly' | kind=rom_makeup_slow_thaw |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 117, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 118, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Forgive, honestly' | kind=dra_partner_confesses |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 119, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Game on' | kind=rom_money_team_up |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 120, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 121, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Book the quiet cabin' | kind=dra_rebuild_weekend |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 122, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Find my partner immediately' | kind=rom_flirt_while_partnered |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 123, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Pivot to an emergency pizza feast' | kind=flavor_burnt_holiday |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 124, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 125, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Confess the almost' | kind=dra_confess_temptation |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 126, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Dance badly, happily' | kind=rom_chemistry_dance_floor |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 127, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Doorbell-tomato everyone' | kind=flavor_garden_tomatoes |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 128, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 129, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Grand gesture, immediate' | kind=dra_forgot_anniversary |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 130, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Propose splitting the day' | kind=rom_inlaws_holiday |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 131, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Attempt a five-course masterpiece' | kind=flavor_in_law_dinner |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 132, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 133, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Ask about it in the morning, calmly' | kind=dra_suspicious_text |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 134, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Lean into the moment' | kind=rom_chemistry_spark |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 135, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Drop everything and dance' | kind=flavor_kitchen_dancing |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 136, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 137, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Introduce them properly' | kind=dra_partner_jealous_friend |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 138, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Savour the quiet' | kind=rom_chemistry_slow_burn |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 139, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Reach out and help them network' | kind=flavor_fired_peer |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 140, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 141, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Sit down and plan together' | kind=rom_money_fight_bill |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 142, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 143, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Turn on the charm' | kind=rom_inlaws_dinner |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 144, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 145, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Make real amends' | kind=rom_anniversary_forgot |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 146, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 147, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Ask about it calmly' | kind=rom_money_fight_splurge |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 148, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 149, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 150, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Show up with gloves and a truck' | kind=flavor_sibling_moving_help |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 151, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| FAIL | [m2b-r51-inheritance] funeral decision reached | no family death in 60 seeded years |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] balance recorded | balance=1026836 |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_ac27e5cdd3", "revision": 152, "status": "alive", "player_id": "ac9e4c07-5e57-40ab-8e62-0a9b8497368a", "people": [{"id": "ac9 |
| PASS | [m2b-r51-inheritance] resolved pending via 'Attend the funeral' | kind=funeral |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| PASS | [m2b-r51-inheritance] deceased archived and refs resolve |  |
| PASS | [m2b-r51-inheritance] person references resolve | [] |
| PASS | [m2b-r51-inheritance] refresh state | game_id=g_ac27e5 |
| FAIL | [m2b-r51-inheritance] M2 state sections match frozen shapes | ["finances.ledger: row 12: source_key 't_000072:relationships:wedding#drv-dec-75-venue:courthouse:ac9e4c07-5e57-40ab-8e62-0a9b8497368a:cost' off the frozen {turn}:{phase}:{occurrence}:{person}:{leg} shape"] |
| PASS | [m2b-r51-inheritance] M2 person references match frozen shapes | [] |
| PASS | [m2c-s5-succession] scenario married_with_child accepted | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] no open pending decisions |  |
| PASS | [m2c-s5-succession] balance recorded | balance=339628 |
| PASS | [m2c-s5-succession] cmd surrender | status=200 {"schema_version": 2, "game_id": "g_f1c5c41ee9", "revision": 99, "status": "alive", "player_id": "d35d4674-f58a-44d8-b36b-694ae1551d9b", "people": [{"id": "d35d |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f1c5c41ee9", "revision": 100, "status": "alive", "player_id": "d35d4674-f58a-44d8-b36b-694ae1551d9b", "people": [{"id": "d35 |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f1c5c41ee9", "revision": 101, "status": "dead", "player_id": "d35d4674-f58a-44d8-b36b-694ae1551d9b", "people": [{"id": "d35d |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] surrender leads to death state | status=dead |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] life ended |  |
| PASS | [m2c-s5-succession] eligible children listed (frozen shape) |  |
| PASS | [m2c-s5-succession] cmd continue_as | status=200 {"schema_version": 2, "game_id": "g_f1c5c41ee9", "revision": 102, "status": "alive", "player_id": "5aa0307b-1a5c-4864-bdf4-f8b7db360ab3", "people": [{"id": "5aa |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| PASS | [m2c-s5-succession] continue_as: identity/generation preserved | player=True gen 1->2 age=10 |
| PASS | [m2c-s5-succession] same game_id after succession |  |
| PASS | [m2c-s5-succession] cash continuity recorded | balance 339628 -> 339628; completed_lives=1 |
| PASS | [m2c-s5-succession] person references resolve | [] |
| PASS | [m2c-s5-succession] refresh state | game_id=g_f1c5c4 |
| WARN | [m2c-s5-succession] WARN open question: job_label null while unemployed (frozen shape silent; A to confirm) | job_label=None |
| PASS | [m2c-s5-succession] M2 state sections match frozen shapes | [] |
| PASS | [m2c-s5-succession] M2 person references match frozen shapes | [] |
| PASS | [m25-dating-app] phone viewport 390x844 |  |
| PASS | [m25-dating-app] scenario married_30 accepted | game_id=g_991260 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] unrelated adult stranger fixture added | Riley Stranger 3a913207 |
| PASS | [m25-dating-app] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-dating-app] cmd dating_app | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 92, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] dating-app offer is the frozen v3 pending |  |
| PASS | [m25-dating-app] accept/decline both enabled and free |  |
| PASS | [m25-dating-app] fixture stranger chosen, not the existing spouse |  |
| PASS | [m25-dating-app] stored dating response pinned to acceptance boundary |  |
| PASS | [m25-dating-app] option 'd_0_accept' stored |  |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 93, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] the date made the match a known person |  |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 94, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 95, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 96, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] age advanced exactly once | before=30 after=31 |
| PASS | [m25-dating-app] turn settled (no pending) | revision=96 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 97, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 98, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_9912600895", "revision": 99, "status": "alive", "player_id": "db216f0b-709c-48b2-ac6d-b76ccf5e68ce", "people": [{"id": "db21 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] refresh state | game_id=g_991260 |
| PASS | [m25-dating-app] age advanced exactly once | before=31 after=32 |
| PASS | [m25-dating-app] turn settled (no pending) | revision=99 |
| PASS | [m25-dating-app] game loaded in page | game_id=g_991260 |
| PASS | [m25-dating-app] M2.5 dating & weddings screen mounts |  |
| PASS | [m25-divorce] phone viewport 390x844 |  |
| PASS | [m25-divorce] scenario married_with_child accepted | game_id=g_f0e7fa |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] partner id resolved | 9de39222 |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] one active union with the spouse |  |
| PASS | [m25-divorce] the shared dependent child is bound to the union |  |
| PASS | [m25-divorce] wedding planner publishes a free venue tier |  |
| PASS | [m25-divorce] balance recorded | balance=339628 |
| PASS | [m25-divorce] wedding op divorce_union | status=200 {"revision": 99, "result": {"ok": true, "ids": {"union_id": "uni-74e7abc44eb8"}, "journal": [{"kind": "wedding", "text": "You and Nathanial Dickson divorced.",  |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] N3 exact post-divorce state: ex-partner is not married and legacy Divorce is disabled |  |
| PASS | [m25-divorce] shared child untouched by the divorce |  |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] courtship_offer advertised for real journey | target=9de39222 |
| PASS | [m25-divorce] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-divorce] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_f0e7fa6ecd", "revision": 100, "status": "alive", "player_id": "c89b96be-7695-4717-b22a-148674792170", "people": [{"id": "c89 |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-divorce] courtship_offer acceptance boundary stored | p_accept=0.46 |
| PASS | [m25-divorce] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-divorce] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f0e7fa6ecd", "revision": 101, "status": "alive", "player_id": "c89b96be-7695-4717-b22a-148674792170", "people": [{"id": "c89 |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] courtship_offer accepted through stored response | deterministic_seed=31 |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] union_proposal advertised for real journey | target=9de39222 |
| PASS | [m25-divorce] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-divorce] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_f0e7fa6ecd", "revision": 102, "status": "alive", "player_id": "c89b96be-7695-4717-b22a-148674792170", "people": [{"id": "c89 |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] union_proposal stored pending bound to target | kind=propose_union accepted=True |
| PASS | [m25-divorce] union_proposal acceptance boundary stored | p_accept=0.46 |
| PASS | [m25-divorce] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-divorce] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_f0e7fa6ecd", "revision": 103, "status": "alive", "player_id": "c89b96be-7695-4717-b22a-148674792170", "people": [{"id": "c89 |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] union_proposal accepted through stored response | deterministic_seed=31 |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] real offer journey remarries the co-parent |  |
| PASS | [m25-divorce] shared dependent child bound to the new union |  |
| PASS | [m25-divorce] balance recorded | balance=337628 |
| PASS | [m25-divorce] wedding op divorce_union | status=200 {"revision": 104, "result": {"ok": true, "ids": {"union_id": "uni-4350a5fc6b28"}, "journal": [{"kind": "wedding", "text": "You and Nathanial Dickson divorced.", |
| PASS | [m25-divorce] refresh state | game_id=g_f0e7fa |
| PASS | [m25-divorce] shared child untouched by the second divorce |  |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] real-journey union recorded divorced (not erased) |  |
| PASS | [m25-divorce] no active union remains with the spouse |  |
| PASS | [m25-divorce] game loaded in page | game_id=g_f0e7fa |
| PASS | [m25-divorce] M2.5 screen mounts after the divorce |  |
| PASS | [m25-offer-boundaries] phone viewport 390x844 |  |
| PASS | [m25-offer-boundaries] scenario employed_30 accepted | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] unrelated adult stranger fixture added | Refused Stranger 43c4ad62 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] courtship_offer advertised for refusal boundary | target=43c4ad62 |
| PASS | [m25-offer-boundaries] next gameplay draw pinned | seed=153 u=0.993368 |
| PASS | [m25-offer-boundaries] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_d46a06b69e", "revision": 68, "status": "alive", "player_id": "c9f01396-de20-4f6e-bae9-f389016a2d4e", "people": [{"id": "c9f0 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] refusal boundary stored | accepted=False p_accept=0.16 |
| PASS | [m25-offer-boundaries] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_d46a06b69e", "revision": 69, "status": "alive", "player_id": "c9f01396-de20-4f6e-bae9-f389016a2d4e", "people": [{"id": "c9f0 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] stored refusal records declined attempt |  |
| PASS | [m25-offer-boundaries] GET relationships-view |  |
| PASS | [m25-offer-boundaries] refusal creates no courtship edge or union |  |
| PASS | [m25-offer-boundaries] unrelated adult stranger fixture added | Accepted Stranger c18213c6 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] courtship_offer advertised for real journey | target=c18213c6 |
| PASS | [m25-offer-boundaries] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-offer-boundaries] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_d46a06b69e", "revision": 70, "status": "alive", "player_id": "c9f01396-de20-4f6e-bae9-f389016a2d4e", "people": [{"id": "c9f0 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-offer-boundaries] courtship_offer acceptance boundary stored | p_accept=0.16 |
| PASS | [m25-offer-boundaries] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-offer-boundaries] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_d46a06b69e", "revision": 71, "status": "alive", "player_id": "c9f01396-de20-4f6e-bae9-f389016a2d4e", "people": [{"id": "c9f0 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] courtship_offer accepted through stored response | deterministic_seed=31 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] union_proposal advertised for real journey | target=c18213c6 |
| PASS | [m25-offer-boundaries] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-offer-boundaries] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_d46a06b69e", "revision": 72, "status": "alive", "player_id": "c9f01396-de20-4f6e-bae9-f389016a2d4e", "people": [{"id": "c9f0 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] union_proposal stored pending bound to target | kind=propose_union accepted=True |
| PASS | [m25-offer-boundaries] union_proposal acceptance boundary stored | p_accept=0.36 |
| PASS | [m25-offer-boundaries] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-offer-boundaries] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_d46a06b69e", "revision": 73, "status": "alive", "player_id": "c9f01396-de20-4f6e-bae9-f389016a2d4e", "people": [{"id": "c9f0 |
| PASS | [m25-offer-boundaries] refresh state | game_id=g_d46a06 |
| PASS | [m25-offer-boundaries] union_proposal accepted through stored response | deterministic_seed=31 |
| PASS | [m25-offer-boundaries] GET relationships-view |  |
| PASS | [m25-offer-boundaries] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-offer-boundaries] GET relationships-view |  |
| PASS | [m25-offer-boundaries] accepted boundary forms union; refused boundary does not |  |
| PASS | [m25-two-union-selected-divorce] phone viewport 390x844 |  |
| PASS | [m25-two-union-selected-divorce] scenario married_with_child accepted | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] partner id resolved | 67f4bd80 |
| PASS | [m25-two-union-selected-divorce] GET relationships-view |  |
| PASS | [m25-two-union-selected-divorce] baseline spouse union has the shared child |  |
| PASS | [m25-two-union-selected-divorce] unrelated adult stranger fixture added | Sam Second e8746499 |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] courtship_offer advertised for real journey | target=e8746499 |
| PASS | [m25-two-union-selected-divorce] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-two-union-selected-divorce] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_cec8ad75a1", "revision": 99, "status": "alive", "player_id": "b9262cba-a702-49b8-a050-e29410fc5653", "people": [{"id": "b926 |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-two-union-selected-divorce] courtship_offer acceptance boundary stored | p_accept=0.16 |
| PASS | [m25-two-union-selected-divorce] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-two-union-selected-divorce] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_cec8ad75a1", "revision": 100, "status": "alive", "player_id": "b9262cba-a702-49b8-a050-e29410fc5653", "people": [{"id": "b92 |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] courtship_offer accepted through stored response | deterministic_seed=31 |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] union_proposal advertised for real journey | target=e8746499 |
| PASS | [m25-two-union-selected-divorce] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-two-union-selected-divorce] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_cec8ad75a1", "revision": 101, "status": "alive", "player_id": "b9262cba-a702-49b8-a050-e29410fc5653", "people": [{"id": "b92 |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] union_proposal stored pending bound to target | kind=propose_plural_union accepted=True |
| PASS | [m25-two-union-selected-divorce] union_proposal acceptance boundary stored | p_accept=0.36 |
| PASS | [m25-two-union-selected-divorce] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-two-union-selected-divorce] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_cec8ad75a1", "revision": 102, "status": "alive", "player_id": "b9262cba-a702-49b8-a050-e29410fc5653", "people": [{"id": "b92 |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] union_proposal accepted through stored response | deterministic_seed=31 |
| PASS | [m25-two-union-selected-divorce] GET relationships-view |  |
| PASS | [m25-two-union-selected-divorce] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-two-union-selected-divorce] GET relationships-view |  |
| PASS | [m25-two-union-selected-divorce] two simultaneous active unions are public |  |
| PASS | [m25-two-union-selected-divorce] child remains bound to the original spouse union |  |
| PASS | [m25-two-union-selected-divorce] balance recorded | balance=339628 |
| PASS | [m25-two-union-selected-divorce] wedding op plan_wedding | status=200 {"revision": 103, "result": {"ok": true, "ids": {"union_id": "uni-b8852499871f"}, "journal": [{"kind": "wedding", "text": "Dev Tester and Sam Second celebrated  |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] GET relationships-view |  |
| PASS | [m25-two-union-selected-divorce] second union receives the free courthouse ceremony |  |
| PASS | [m25-two-union-selected-divorce] free wedding configuration moved no money |  |
| PASS | [m25-two-union-selected-divorce] wedding op divorce_union | status=200 {"revision": 104, "result": {"ok": true, "ids": {"union_id": "uni-b8852499871f"}, "journal": [{"kind": "wedding", "text": "You and Sam Second divorced.", "perso |
| PASS | [m25-two-union-selected-divorce] refresh state | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] family child count unchanged by selected divorce |  |
| PASS | [m25-two-union-selected-divorce] GET relationships-view |  |
| PASS | [m25-two-union-selected-divorce] selected second union recorded divorced |  |
| PASS | [m25-two-union-selected-divorce] spouse union and shared child survive selected divorce |  |
| PASS | [m25-two-union-selected-divorce] no active second union remains |  |
| PASS | [m25-two-union-selected-divorce] selected divorce of childless second union moved no money |  |
| PASS | [m25-two-union-selected-divorce] game loaded in page | game_id=g_cec8ad |
| PASS | [m25-two-union-selected-divorce] M2.5 screen mounts after selected divorce |  |
| PASS | [m25-editor-law] phone viewport 390x844 |  |
| PASS | [m25-editor-law] scenario married_with_child accepted | game_id=g_79f671 |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] partner id resolved | 5cdbaa29 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] wedding op divorce_union | status=200 {"revision": 99, "result": {"ok": true, "ids": {"union_id": "uni-959e395145e0"}, "journal": [{"kind": "wedding", "text": "You and Nathanial Dickson divorced.",  |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] courtship_offer advertised for real journey | target=5cdbaa29 |
| PASS | [m25-editor-law] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-editor-law] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_79f6712384", "revision": 100, "status": "alive", "player_id": "7bd00e92-26c5-4a32-8444-1453256350a7", "people": [{"id": "7bd |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-editor-law] courtship_offer acceptance boundary stored | p_accept=0.46 |
| PASS | [m25-editor-law] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-editor-law] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_79f6712384", "revision": 101, "status": "alive", "player_id": "7bd00e92-26c5-4a32-8444-1453256350a7", "people": [{"id": "7bd |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] courtship_offer accepted through stored response | deterministic_seed=31 |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] union_proposal advertised for real journey | target=5cdbaa29 |
| PASS | [m25-editor-law] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-editor-law] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_79f6712384", "revision": 102, "status": "alive", "player_id": "7bd00e92-26c5-4a32-8444-1453256350a7", "people": [{"id": "7bd |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] union_proposal stored pending bound to target | kind=propose_union accepted=True |
| PASS | [m25-editor-law] union_proposal acceptance boundary stored | p_accept=0.46 |
| PASS | [m25-editor-law] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-editor-law] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_79f6712384", "revision": 103, "status": "alive", "player_id": "7bd00e92-26c5-4a32-8444-1453256350a7", "people": [{"id": "7bd |
| PASS | [m25-editor-law] refresh state | game_id=g_79f671 |
| PASS | [m25-editor-law] union_proposal accepted through stored response | deterministic_seed=31 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] real-journey union recognized under baseline law |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 104, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"m |
| PASS | [m25-editor-law] GET policy-state |  |
| PASS | [m25-editor-law] save-local law override persisted |  |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] existing union's recognition reprojected (A4) |  |
| PASS | [m25-editor-law] editor law change moved no money |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 104, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"m |
| PASS | [m25-editor-law] replay with the recorded request_id returns the recorded response byte-identically |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=409 {"error": {"code": "stale_revision", "message": "expected_revision 103 does not match current revision 104"}} |
| PASS | [m25-editor-law] game loaded in page | game_id=g_79f671 |
| PASS | [m25-editor-law] editor screen mounts |  |
| PASS | [m25-law-ceremony-fine] phone viewport 390x844 |  |
| PASS | [m25-law-ceremony-fine] scenario employed_30 accepted | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] unrelated adult stranger fixture added | Fine Stranger 34e60af2 |
| PASS | [m25-law-ceremony-fine] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 68, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"mo |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] courtship_offer advertised for real journey | target=34e60af2 |
| PASS | [m25-law-ceremony-fine] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-law-ceremony-fine] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_6f48b0ed19", "revision": 69, "status": "alive", "player_id": "b30fb000-cb27-4588-ad54-a52e2495adad", "people": [{"id": "b30f |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-law-ceremony-fine] courtship_offer acceptance boundary stored | p_accept=0.16 |
| PASS | [m25-law-ceremony-fine] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-law-ceremony-fine] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6f48b0ed19", "revision": 70, "status": "alive", "player_id": "b30fb000-cb27-4588-ad54-a52e2495adad", "people": [{"id": "b30f |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] courtship_offer accepted through stored response | deterministic_seed=31 |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] union_proposal advertised for real journey | target=34e60af2 |
| PASS | [m25-law-ceremony-fine] next gameplay draw pinned | seed=31 u=0.012278 |
| PASS | [m25-law-ceremony-fine] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_6f48b0ed19", "revision": 71, "status": "alive", "player_id": "b30fb000-cb27-4588-ad54-a52e2495adad", "people": [{"id": "b30f |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] union_proposal stored pending bound to target | kind=propose_union accepted=True |
| PASS | [m25-law-ceremony-fine] union_proposal acceptance boundary stored | p_accept=0.21 |
| PASS | [m25-law-ceremony-fine] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-law-ceremony-fine] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_6f48b0ed19", "revision": 72, "status": "alive", "player_id": "b30fb000-cb27-4588-ad54-a52e2495adad", "people": [{"id": "b30f |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] union_proposal accepted through stored response | deterministic_seed=31 |
| PASS | [m25-law-ceremony-fine] GET relationships-view |  |
| PASS | [m25-law-ceremony-fine] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-law-ceremony-fine] GET relationships-view |  |
| PASS | [m25-law-ceremony-fine] public proposal persists forbidden recognition |  |
| PASS | [m25-law-ceremony-fine] balance recorded | balance=361048 |
| PASS | [m25-law-ceremony-fine] wedding op plan_wedding | status=200 {"revision": 73, "result": {"ok": true, "ids": {"union_id": "uni-035eac48c1b1"}, "journal": [{"kind": "wedding", "text": "Dev Tester and Fine Stranger celebrate |
| PASS | [m25-law-ceremony-fine] refresh state | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] L1 civil fine reduced money by configured amount |  |
| PASS | [m25-law-ceremony-fine] L1 civil fine posted exactly once | new_rows=1 payment_rows=1 amount=-500 [] |
| PASS | [m25-law-ceremony-fine] GET relationships-view |  |
| PASS | [m25-law-ceremony-fine] forbidden-union ceremony held after fine |  |
| PASS | [m25-law-ceremony-fine] game loaded in page | game_id=g_6f48b0 |
| PASS | [m25-law-ceremony-fine] M2.5 screen mounts after fined ceremony |  |
| PASS | [m25-family-tree] phone viewport 390x844 |  |
| PASS | [m25-family-tree] scenario married_with_child accepted | game_id=g_f6a77c |
| PASS | [m25-family-tree] refresh state | game_id=g_f6a77c |
| PASS | [m25-family-tree] refresh state | game_id=g_f6a77c |
| PASS | [m25-family-tree] GET family-tree |  |
| PASS | [m25-family-tree] tree projects player, spouse and child |  |
| PASS | [m25-family-tree] parentage edges present (real graph, no invention) |  |
| PASS | [m25-family-tree] GET relationships-view |  |
| PASS | [m25-family-tree] known-secrets projection arrays present |  |
| PASS | [m25-family-tree] every known fact carries id+kind (provenance) |  |
| PASS | [m25-family-tree] game loaded in page | game_id=g_f6a77c |
| PASS | [m25-family-tree] family tree renders |  |
| PASS | [m25-secrecy] phone viewport 390x844 |  |
| PASS | [m25-secrecy] scenario married_with_child accepted | game_id=g_68fca3 |
| PASS | [m25-secrecy] refresh state | game_id=g_68fca3 |
| PASS | [m25-secrecy] refresh state | game_id=g_68fca3 |
| PASS | [m25-secrecy] partner id resolved | 4da41673 |
| PASS | [m25-secrecy] private row + unknown secret fact planted | fact_id=fact-054aa71dc0ed |
| PASS | [m25-secrecy] refresh state | game_id=g_68fca3 |
| PASS | [m25-secrecy] HTTP get_state hides the private journal row |  |
| PASS | [m25-secrecy] HTTP get_state hides the private fact id |  |
| PASS | [m25-secrecy] GET relationships-view |  |
| PASS | [m25-secrecy] the secrets view omits the unknown fact |  |
| PASS | [m25-secrecy] player learned the fact | fact-054aa71dc0ed |
| PASS | [m25-secrecy] refresh state | game_id=g_68fca3 |
| PASS | [m25-secrecy] private row still hidden after learning |  |
| PASS | [m25-secrecy] GET relationships-view |  |
| PASS | [m25-secrecy] the learned fact now appears in the secrets view |  |
| PASS | [v2-route-visibility] phone viewport 390x844 |  |
| PASS | [v2-route-visibility] scenario married_30 accepted | game_id=g_16edcb |
| PASS | [v2-route-visibility] refresh state | game_id=g_16edcb |
| PASS | [v2-route-visibility] v2 shell game view loaded |  |
| PASS | [v2-route-visibility] editor panel opened from More menu |  |
| PASS | [v2-route-visibility] More->Editor: editor actually visible from Home |  |
| PASS | [v2-route-visibility] resumeView visible + gameView actually hidden | {"visible_shown": true, "visible_check": true, "hidden_attr": true, "hidden_check": false, "hidden_display": "none"} |
| PASS | [v2-route-visibility] resume life: game view actually visible |  |
| PASS | [v2-route-visibility] refresh state | game_id=g_16edcb |
| PASS | [v2-route-visibility] cmd surrender | status=200 {"schema_version": 2, "game_id": "g_16edcbe715", "revision": 92, "status": "alive", "player_id": "61009db9-5b45-4c67-80f6-656e161853cc", "people": [{"id": "6100 |
| PASS | [v2-route-visibility] refresh state | game_id=g_16edcb |
| PASS | [v2-route-visibility] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_16edcbe715", "revision": 93, "status": "alive", "player_id": "61009db9-5b45-4c67-80f6-656e161853cc", "people": [{"id": "6100 |
| PASS | [v2-route-visibility] refresh state | game_id=g_16edcb |
| PASS | [v2-route-visibility] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_16edcbe715", "revision": 94, "status": "dead", "player_id": "61009db9-5b45-4c67-80f6-656e161853cc", "people": [{"id": "61009 |
| PASS | [v2-route-visibility] refresh state | game_id=g_16edcb |
| PASS | [v2-route-visibility] surrender leads to death state | status=dead |
| PASS | [v2-route-visibility] legacy view shown after death |  |
| PASS | [v2-route-visibility] legacyView visible + gameView actually hidden | {"visible_shown": true, "visible_check": true, "hidden_attr": true, "hidden_check": false, "hidden_display": "none"} |
| WARN | [robot] robot-quick (60 lives) | 46 eligibility finding(s), 0 hard; 909 commands/602 decisions; first: /home/user/Life-Simulator1/docs/collab/evidence/browser/robot/robot-seed7-life0-unexpected_4xx-20261121-222042.json |
