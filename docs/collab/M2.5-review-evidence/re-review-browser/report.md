# Browser check report

PASS: 86 | FAIL: 0 | PENDING: 0 | WARN: 1

| Result | Check | Detail |
|--------|-------|--------|
| PASS | [m25-dating-app] phone viewport 390x844 |  |
| PASS | [m25-dating-app] scenario married_30 accepted | game_id=g_3df219 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd dating_app | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 85, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] dating-app offer is the frozen v3 pending |  |
| PASS | [m25-dating-app] accept/decline both enabled and free |  |
| PASS | [m25-dating-app] target bound once at offer creation |  |
| PASS | [m25-dating-app] option 'd_0_accept' stored |  |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 86, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] the date made the match a known person |  |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 87, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 88, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 89, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] age advanced exactly once | before=30 after=31 |
| PASS | [m25-dating-app] turn settled (no pending) | revision=89 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd age_up | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 90, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 91, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_3df219f834", "revision": 92, "status": "alive", "player_id": "eb306332-0e7a-461d-a126-dcbfb19a1f5d", "people": [{"id": "eb30 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] refresh state | game_id=g_3df219 |
| PASS | [m25-dating-app] age advanced exactly once | before=31 after=32 |
| PASS | [m25-dating-app] turn settled (no pending) | revision=92 |
| PASS | [m25-dating-app] game loaded in page | game_id=g_3df219 |
| PASS | [m25-dating-app] M2.5 dating & weddings screen mounts |  |
| PASS | [m25-divorce] phone viewport 390x844 |  |
| PASS | [m25-divorce] scenario married_with_child accepted | game_id=g_f6a1ed |
| PASS | [m25-divorce] refresh state | game_id=g_f6a1ed |
| PASS | [m25-divorce] refresh state | game_id=g_f6a1ed |
| PASS | [m25-divorce] partner id resolved | 9f64a87b |
| PASS | [m25-divorce] graph op form_union | {"revision": 76, "result": {"ok": true, "ids": {"union_id": "uni-06ae101063ca"}, "journal": [{"kind": "union", "text": "Dev Tester and Damon Berry formed a marr |
| PASS | [m25-divorce] refresh state | game_id=g_f6a1ed |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] one active union with the spouse |  |
| PASS | [m25-divorce] the shared dependent child is bound to the union |  |
| PASS | [m25-divorce] wedding planner publishes a free venue tier |  |
| PASS | [m25-divorce] balance recorded | balance=752683 |
| PASS | [m25-divorce] wedding op divorce_union | status=200 {"revision": 77, "result": {"ok": true, "ids": {"union_id": "uni-06ae101063ca"}, "journal": [{"kind": "wedding", "text": "You and Damon Berry divorced.", "perso |
| PASS | [m25-divorce] refresh state | game_id=g_f6a1ed |
| WARN | [m25-divorce] WARN legacy partner layer untouched by the graph divorce (cross-layer gap, Open item) |  |
| PASS | [m25-divorce] shared child untouched by the divorce |  |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] union recorded divorced (not erased) |  |
| PASS | [m25-divorce] game loaded in page | game_id=g_f6a1ed |
| PASS | [m25-divorce] M2.5 screen mounts after the divorce |  |
| PASS | [m25-editor-law] phone viewport 390x844 |  |
| PASS | [m25-editor-law] scenario married_with_child accepted | game_id=g_e37d10 |
| PASS | [m25-editor-law] refresh state | game_id=g_e37d10 |
| PASS | [m25-editor-law] refresh state | game_id=g_e37d10 |
| PASS | [m25-editor-law] partner id resolved | 496b3262 |
| PASS | [m25-editor-law] graph op form_union | {"revision": 76, "result": {"ok": true, "ids": {"union_id": "uni-ec202aa1da65"}, "journal": [{"kind": "union", "text": "Dev Tester and Damon Berry formed a marr |
| PASS | [m25-editor-law] refresh state | game_id=g_e37d10 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] union recognized under the baseline law |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 77, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"mo |
| PASS | [m25-editor-law] GET policy-state |  |
| PASS | [m25-editor-law] save-local law override persisted |  |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] existing union's recognition reprojected (A4) |  |
| PASS | [m25-editor-law] editor law change moved no money |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 77, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"mo |
| PASS | [m25-editor-law] replay with the recorded request_id returns the recorded response byte-identically |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=409 {"error": {"code": "stale_revision", "message": "expected_revision 76 does not match current revision 77"}} |
| PASS | [m25-editor-law] game loaded in page | game_id=g_e37d10 |
| PASS | [m25-editor-law] editor screen mounts |  |
| PASS | [m25-family-tree] phone viewport 390x844 |  |
| PASS | [m25-family-tree] scenario married_with_child accepted | game_id=g_f1d07e |
| PASS | [m25-family-tree] refresh state | game_id=g_f1d07e |
| PASS | [m25-family-tree] refresh state | game_id=g_f1d07e |
| PASS | [m25-family-tree] GET family-tree |  |
| PASS | [m25-family-tree] tree projects player, spouse and child |  |
| PASS | [m25-family-tree] parentage edges present (real graph, no invention) |  |
| PASS | [m25-family-tree] GET relationships-view |  |
| PASS | [m25-family-tree] known-secrets projection arrays present |  |
| PASS | [m25-family-tree] every known fact carries id+kind (provenance) |  |
| PASS | [m25-family-tree] game loaded in page | game_id=g_f1d07e |
| PASS | [m25-family-tree] family tree renders |  |
