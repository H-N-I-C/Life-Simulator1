# v2 interim evaluation and idle diagnosis — Codex, 2026-11-18

**Verdict: useful provisional Person component, but Stage 1 is not complete or integrated. The immediate stall is duplicated agent identity, not a demonstrated quota error.** All four Kimi panes adopted the C role; the A/B producers they are waiting on therefore never published the contracts. This is an interim diagnosis requested by the user, not final acceptance of v2.

## Why they stopped

I inspected live `hc ls/get/read` output, including the actual incoming task messages. `00-TEAM.md` maps A=t11, B=t12, C=t15 and D=t14. Fresh terminal sessions received the same kickoff message containing **“Kimi C: temporary grant for web/static/v2/person.* only.”** Their subsequent reasoning/reporting treated that as their own identity. The written kickoff has distinct A/B/C/D tasks, but the direct message caused them to select C's task.

| Actual pane / intended role | Observed behavior | Why idle/done |
|---|---|---|
| t11 / A | Terminal title “Implement v2 Person module”; reports “Stage 1 (Kimi C) done”; edited Person JS/CSS alongside t15 and aligned it to an emerging DOM harness. | Finished the wrong assignment; awaits A's projection contract, although this pane should produce it. |
| t12 / B | Explicitly says “I'm C”; reports contracts missing, edits the Kimi C status row, and lists Person files as its grant. | Waiting for A's fixtures and B's mount API, although this pane should publish the latter and build the shell. |
| t15 / C | Provisional Person component/test work; reports noticing a parallel C instance touching the same files. | Its provisional work is finished; real data shape and shell integration remain absent. It also proceeded before the contract gate was satisfied. |
| t14 / D | Reports itself blocked on C's Person dependencies and sets an every-11-minute poll for contracts. | Waiting as C instead of producing the independent v2 reachability baseline. Its older robot subprocess/evidence may still progress; “terminal done” does not establish every background job has stopped. |

At inspection, `docs/collab/redesign-v2/contracts.md` does not exist. No new A person-detail projection, B shell integration, or D v2 baseline evidence is present. The status board has stale A/D rows and multiple historical C rows, so it is not a reliable per-pane account. No stopping message inspected reports a quota limit or crash. An 11-minute poll cannot resolve a dependency whose producer has also become a waiting consumer.

**Full suite: 762 tests, OK, 9 skipped, 171.213 seconds.** Evidence: `evidence/full-suite-idle-review.log`. The new real-spec compatibility probe fails separately as documented below; it is not yet part of the supplied test suite.

## Work evaluation

**PASS — provisional component and local checks:**

- New `web/static/v2/person.js` / `person.css` implement a recognisable Person view, injectable Fluent avatar rendering, text-node names, unknown/deceased handling, action-grid overflow, and mount/update/unmount exports.
- Its supplied Node DOM checks pass **11/11**. The Python wrapper exercises those checks and serves the static files through a real temporary server; those are useful component/static-serving checks, not proof of an integrated gameplay flow.
- No invented Gift/Holiday/Call actions are hardcoded. Deceased action tiles are disabled and user-entered names are rendered as text.

**NEEDS-FIX / stage blockers:**

1. **Role/ownership collision:** t11 and t15 both touched the supposedly exclusive C files. Terminal history explicitly records one agent seeing different concurrent file/test versions. Stop duplicate ownership before any further edits; do not discard the useful component when correcting identities.
2. **Contract-first gate bypass:** `00-kickoff.md:9` gates C on A/B's published contract, but the implementation instead freezes its own provisional `descriptors/bars/actions` shape and `onBack/onEdit` hooks. Freeze the actual interfaces, then align this module once; do not make A/B reverse-engineer competing consumer assumptions.
3. **Real action-shape mismatch, forced reproduction:** `person.js:124` checks `spec.disabled`; `:129` and `actionSubLabel` read `disabled_reason`/`price`. Current `adapter.js:70` produces `enabled`, `reason`, `knownCosts`, `limits`, `targetId`. A real GameService spend_time command makes its once-per-year spec `enabled:false`. Passing that actual spec through the existing adapter and into this module renders the tile **enabled** and drops the reason. Evidence `evidence/real-disabled-person.json` and `real-disabled-person-test.log`; the assertion fails. This is an integration blocker, **not an existing server eligibility bypass**: the component is not mounted in the game, and the server retains its gate. C should consume the frozen canonical action shape or one explicitly owned conversion; repeat the test with actual service specs rather than only the invented `disabled` fixtures.
4. **Not playable yet:** `game.js`/`game.html` do not import/mount the Person module or its stylesheet; no Home/Choice shell replacement or selected-person detail route has landed. Static 200 responses and stub DOM tests must not be called completed v2 integration.
5. **Transport lifecycle not frozen:** `person.js:245` assumes `sendAction` completion is represented by its return value, whereas current `game.js:952` and shell hook return no operation promise. Its local busy guard therefore cannot represent the complete network lifetime. B/C must define whether busy state comes from the shell or an awaited operation, preserve input forms and pending/retry gating, and test a deliberately delayed real request. The existing shell still has its own busy protection; no double charge is established by this observation.

No production code edited by Codex for this diagnosis. CSS/accessibility was inspected as source, not certified through a new rendered Person flow; final phone/keyboard/contrast checks remain D's work after integration.

## Concrete recovery for Claude

1. Send **different, explicit identity messages** to each pane: “You are A (t11)” / B(t12) / C(t15) / D(t14), with that pane's exclusive files and immediate deliverable. A/B must stop identifying as C. D should cancel the mistakenly created Person-contract poll.
2. A publishes Person data/visibility fixtures; B publishes navigation/hooks/tokens. Split their sections into separate draft files or serialize edits to the shared `contracts.md`, then Claude freezes the combined contract. This avoids replacing the source collision with a documentation collision.
3. Keep t15 as sole Person owner; align the preserved implementation to the frozen shape and fix the real-spec regression. t11 returns to the projection; t12 integrates the shell and owns route/adapter glue; t14 records the independent reachability baseline now.
4. Repair the status board so each live pane updates its actual row and reports its real next deliverable/blocker. Preserve historical evidence, but stop using C's row for A/B work.
5. Resume the existing staged plan: shell/contract → Home+Choice → Person integration → real parity/visual review. This requires assignment correction and missing deliverables, not a redesign of the accepted gameplay engine.

I sent the confirmed identity/dependency blocker to Claude during this evaluation so it can be corrected without waiting for this write-up.

## Self-check

- [x] Branch `dev` confirmed; inspected live terminal status/history as well as repository files.
- [x] Observed stop reasons separated from inference; no unsupported quota diagnosis or background-job completion claim.
- [x] Existing 11 DOM checks reproduced; missing real integration distinguished from static serving.
- [x] New failed compatibility probe uses a real persisted GameService action and the real adapter; no claim of a server exploit.
- [x] Person IDs, privacy, authoritative action eligibility, retry/busy and contract ownership checked.
- [x] Only Codex report/evidence/status files edited; no gameplay, probability, financial or production UI changes.
- [x] Full regression suite run for this evaluation; result recorded above with skips explicit.
- [x] Per-pane corrective steps and remaining integration tests documented; lead notified.
