# Browser check report

PASS: 118 | FAIL: 3 | PENDING: 0 | WARN: 1

| Result | Check | Detail |
|--------|-------|--------|
| PASS | [m25-dating-app] phone viewport 390x844 |  |
| PASS | [m25-dating-app] scenario married_30 accepted | game_id=g_8a2f06 |
| PASS | [m25-dating-app] refresh state | game_id=g_8a2f06 |
| PASS | [m25-dating-app] refresh state | game_id=g_8a2f06 |
| FAIL | [m25-dating-app] cmd dating_app | status=404 {"error": {"code": "not_found", "message": "no eligible adults around right now"}} |
| PASS | [m25-dating-app] refresh state | game_id=g_8a2f06 |
| FAIL | [m25-dating-app] dating-app offer is the frozen v3 pending | not boolean true: None |
| FAIL | [m25-dating-app] flow aborted | step 6 ({"check_ctx": "state.pending_decision.options.length === 2 && state.pending_decision.options.every(function(o){return o.enabled})", "desc": "accept/decline both): JS: {"exceptionId": 1, "text": "Uncaught", "lineNumber": 0, "columnNumber": 23, "scriptId": "11", "stackTrace": {"callFrames": [{"functionName": "", "scriptId": "11", "url": "", "lineNumber": 0, "columnNumber": 23}]}, "exception": {"type": "object", "subtype": "error", "className": "TypeError", "description": "TypeError: Cannot read properties of null (reading 'options')\n    at <anonymous>:1:24", "objectId": "-4676331749940433685.2.2"}} |
| PASS | [m25-divorce] phone viewport 390x844 |  |
| PASS | [m25-divorce] scenario married_with_child accepted | game_id=g_53485c |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] partner id resolved | 88f56b34 |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] one active union with the spouse |  |
| PASS | [m25-divorce] the shared dependent child is bound to the union |  |
| PASS | [m25-divorce] wedding planner publishes a free venue tier |  |
| PASS | [m25-divorce] balance recorded | balance=823577 |
| PASS | [m25-divorce] wedding op divorce_union | status=200 {"revision": 95, "result": {"ok": true, "ids": {"union_id": "uni-9182ff1d25f8"}, "journal": [{"kind": "wedding", "text": "You and Armond Kemp divorced.", "perso |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| WARN | [m25-divorce] WARN legacy partner layer untouched by the graph divorce (cross-layer gap, Open item) |  |
| PASS | [m25-divorce] shared child untouched by the divorce |  |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] courtship_offer advertised for real journey | target=88f56b34 |
| PASS | [m25-divorce] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_53485cef85", "revision": 96, "status": "alive", "player_id": "e3aafed1-25bc-4976-9760-4c15ccac5fff", "people": [{"id": "e3aa |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-divorce] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-divorce] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_53485cef85", "revision": 97, "status": "alive", "player_id": "e3aafed1-25bc-4976-9760-4c15ccac5fff", "people": [{"id": "e3aa |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] courtship_offer accepted through stored response | attempts=1 |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] union_proposal advertised for real journey | target=88f56b34 |
| PASS | [m25-divorce] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_53485cef85", "revision": 98, "status": "alive", "player_id": "e3aafed1-25bc-4976-9760-4c15ccac5fff", "people": [{"id": "e3aa |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] union_proposal stored pending bound to target | kind=propose_union accepted=True |
| PASS | [m25-divorce] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-divorce] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_53485cef85", "revision": 99, "status": "alive", "player_id": "e3aafed1-25bc-4976-9760-4c15ccac5fff", "people": [{"id": "e3aa |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] union_proposal accepted through stored response | attempts=1 |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] real offer journey remarries the co-parent |  |
| PASS | [m25-divorce] shared dependent child bound to the new union |  |
| PASS | [m25-divorce] balance recorded | balance=821577 |
| PASS | [m25-divorce] wedding op divorce_union | status=200 {"revision": 100, "result": {"ok": true, "ids": {"union_id": "uni-8ec255aa2a5d"}, "journal": [{"kind": "wedding", "text": "You and Armond Kemp divorced.", "pers |
| PASS | [m25-divorce] refresh state | game_id=g_53485c |
| PASS | [m25-divorce] shared child untouched by the second divorce |  |
| PASS | [m25-divorce] GET relationships-view |  |
| PASS | [m25-divorce] real-journey union recorded divorced (not erased) |  |
| PASS | [m25-divorce] no active union remains with the spouse |  |
| PASS | [m25-divorce] game loaded in page | game_id=g_53485c |
| PASS | [m25-divorce] M2.5 screen mounts after the divorce |  |
| PASS | [m25-editor-law] phone viewport 390x844 |  |
| PASS | [m25-editor-law] scenario married_with_child accepted | game_id=g_a0dc9b |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] partner id resolved | 63549d55 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] wedding op divorce_union | status=200 {"revision": 95, "result": {"ok": true, "ids": {"union_id": "uni-d14f21eece91"}, "journal": [{"kind": "wedding", "text": "You and Armond Kemp divorced.", "perso |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] courtship_offer advertised for real journey | target=63549d55 |
| PASS | [m25-editor-law] cmd courtship_offer | status=200 {"schema_version": 2, "game_id": "g_a0dc9b706c", "revision": 96, "status": "alive", "player_id": "0d68b069-141e-4f09-b721-b9f45b32ae2c", "people": [{"id": "0d68 |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] courtship_offer stored pending bound to target | kind=propose_courtship accepted=True |
| PASS | [m25-editor-law] courtship_offer response option offered | option=d_0_ask |
| PASS | [m25-editor-law] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_a0dc9b706c", "revision": 97, "status": "alive", "player_id": "0d68b069-141e-4f09-b721-b9f45b32ae2c", "people": [{"id": "0d68 |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] courtship_offer accepted through stored response | attempts=1 |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] union_proposal advertised for real journey | target=63549d55 |
| PASS | [m25-editor-law] cmd union_proposal | status=200 {"schema_version": 2, "game_id": "g_a0dc9b706c", "revision": 98, "status": "alive", "player_id": "0d68b069-141e-4f09-b721-b9f45b32ae2c", "people": [{"id": "0d68 |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] union_proposal stored pending bound to target | kind=propose_union accepted=True |
| PASS | [m25-editor-law] union_proposal response option offered | option=d_0_ask |
| PASS | [m25-editor-law] cmd resolve_decision | status=200 {"schema_version": 2, "game_id": "g_a0dc9b706c", "revision": 99, "status": "alive", "player_id": "0d68b069-141e-4f09-b721-b9f45b32ae2c", "people": [{"id": "0d68 |
| PASS | [m25-editor-law] refresh state | game_id=g_a0dc9b |
| PASS | [m25-editor-law] union_proposal accepted through stored response | attempts=1 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] real offer journey forms one active union | active_with_target=1 |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] real-journey union recognized under baseline law |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 100, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"m |
| PASS | [m25-editor-law] GET policy-state |  |
| PASS | [m25-editor-law] save-local law override persisted |  |
| PASS | [m25-editor-law] GET relationships-view |  |
| PASS | [m25-editor-law] existing union's recognition reprojected (A4) |  |
| PASS | [m25-editor-law] editor law change moved no money |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=200 {"revision": 100, "policy_revision": 1, "audit": [{"command": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metropolis_charter", "before": {"m |
| PASS | [m25-editor-law] replay with the recorded request_id returns the recorded response byte-identically |  |
| PASS | [m25-editor-law] editor/apply [{"name": "edit_jurisdiction", "scope": "jurisdiction", "target_id": "jur_metrop | status=409 {"error": {"code": "stale_revision", "message": "expected_revision 99 does not match current revision 100"}} |
| PASS | [m25-editor-law] game loaded in page | game_id=g_a0dc9b |
| PASS | [m25-editor-law] editor screen mounts |  |
| PASS | [m25-family-tree] phone viewport 390x844 |  |
| PASS | [m25-family-tree] scenario married_with_child accepted | game_id=g_86caa7 |
| PASS | [m25-family-tree] refresh state | game_id=g_86caa7 |
| PASS | [m25-family-tree] refresh state | game_id=g_86caa7 |
| PASS | [m25-family-tree] GET family-tree |  |
| PASS | [m25-family-tree] tree projects player, spouse and child |  |
| PASS | [m25-family-tree] parentage edges present (real graph, no invention) |  |
| PASS | [m25-family-tree] GET relationships-view |  |
| PASS | [m25-family-tree] known-secrets projection arrays present |  |
| PASS | [m25-family-tree] every known fact carries id+kind (provenance) |  |
| PASS | [m25-family-tree] game loaded in page | game_id=g_86caa7 |
| PASS | [m25-family-tree] family tree renders |  |
| PASS | [m25-secrecy] phone viewport 390x844 |  |
| PASS | [m25-secrecy] scenario married_with_child accepted | game_id=g_36cf0d |
| PASS | [m25-secrecy] refresh state | game_id=g_36cf0d |
| PASS | [m25-secrecy] refresh state | game_id=g_36cf0d |
| PASS | [m25-secrecy] partner id resolved | f050dd04 |
| PASS | [m25-secrecy] private row + unknown secret fact planted | fact_id=fact-493270893521 |
| PASS | [m25-secrecy] refresh state | game_id=g_36cf0d |
| PASS | [m25-secrecy] HTTP get_state hides the private journal row |  |
| PASS | [m25-secrecy] HTTP get_state hides the private fact id |  |
| PASS | [m25-secrecy] GET relationships-view |  |
| PASS | [m25-secrecy] the secrets view omits the unknown fact |  |
| PASS | [m25-secrecy] player learned the fact | fact-493270893521 |
| PASS | [m25-secrecy] refresh state | game_id=g_36cf0d |
| PASS | [m25-secrecy] private row still hidden after learning |  |
| PASS | [m25-secrecy] GET relationships-view |  |
| PASS | [m25-secrecy] the learned fact now appears in the secrets view |  |
