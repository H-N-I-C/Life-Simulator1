# M2 consolidated review — 2026-11-16

**FAIL — ready for user acceptance testing: NO. M2a independently ready: NO.**

Reviewed M2a/b/c, the parity proof register, all four agent reports, frozen phase map v4/API examples, data events and M0b Fluent integration. No implementation changes made. This is the consolidated review; reserve the one permitted re-review for the integrated fixes. Agents were still updating this shared tree during the review, so counts below identify my actual runs, not earlier report totals.

M2a is blocked by finding 1: university, firing, retirement and funeral interruptions cannot safely finish their current year. These are ordinary progression paths, not cosmetic omissions. The user can explore the current build informally, but neither M2 nor its school/work slice should be presented as accepted yet.

## Verification

- `python3 -m unittest discover -s tests -t .`: **397 tests, OK, 10 skipped**, 111.518 seconds reported by unittest; **112.250 seconds wall time**. Default discovery was run; optional skipped coverage is not claimed as passed.
- `python3 tools/browser_check.py`: **565/567**, exit 1. Evidence: `docs/collab/evidence/browser/report.md`. Failures: obsolete `#gameView` expectation in m1-life-flow and a navigation/context failure in m1-resilience. This does not establish that either corresponding product flow is broken; the tests need repair and a meaningful rerun.
- The browser total includes PENDING and WARN rows marked PASS: creator keyboard testing parked, baby naming not reached within six commands, kidnapping not reached within three, and a missing-turn-cursor warning. These are unproved paths, not successful parity evidence.
- Independently exercised real GameService/Child/Partner/Ledger objects in temporary saves, with seeded RNG and no fabricated implementation results. Reproductions below ran successfully despite the green unit suite.
- Visually inspected `evidence/avatars/fluent_matrix.png`, `fluent_extras.png` and `evidence/m0b/gallery-390.png`. The matrix shows distinct age silhouettes, readable faces across all six skin choices, white hair and elder variants; beard/headscarf art is coherent. The gallery screenshot has an empty Child tile and oversized single-column cards. It may be older evidence; current UI needs a fresh verified screenshot before treating that defect as current.

## Blockers and concrete guidance

### 1. FAIL — interrupted yearly turns are discarded; resolving does not resume them (A)

`src/game/service.py:1151–1169` calls the coordinator and then resets the live turn even when a decision is pending. The current reset retains only a new `year_start` cursor, dropping turn ID, done markers, snapshot and funeral queue. `src/game/service.py:1492–1536` later sets `resume_after_resolve`; there is no consumer of that field and no coordinator call to complete the interrupted year.

**Real-service reproduction:** create Ada Lee, female, Australia, seed 4242; age normally and resolve intervening choices with the first enabled option. At age 17 the university application is pending, but the persisted turn is already `{"cursor":{"phase_id":"year_start","item_cursor":0}}`. Decline university through `command`. The resulting turn has a newly generated ID, empty done markers and `resume_after_resolve: year_start`; no pending choice remains and later phases of age 17 have not resumed. A named cursor merely hides the original null-cursor symptom.

**Fix design:** retain the entire interrupted turn unchanged across saves/reloads. Apply each stored choice once; if it produces another pending choice, retain it; otherwise resume from the persisted continuation immediately until another decision or real year_close. Clear/reset only after successful closure. Process queued funerals before continuing. Route fatal outcomes to closure without running later phases. Tests must force interruptions at education, work and mortality, reload between options, and assert same turn ID, same age, all remaining phases and exact ledger/journal counts. Include multi-funeral years. Do not fix this by relaxing D's cursor assertion.

### 2. FAIL — composite child aging still runs alongside extracted subrules (A + C)

`src/game/coordinator.py:46–53` calls `relation.age_up()` instead of the frozen snapshot's primitive age increment. For a Child this invokes `src/people/classes/child.py:109` onward: schooling, income, job and partner logic. Then `src/menus/rule_actions.py:2883–2887` runs child stat/money/social slices again. The captured snapshot is not used for traversal; newly born children are also included by that family loop.

**Real-object reproduction:** a 24-year-old Child with salary 50,000 and money zero, attached to a real player, goes through `_relations_age`. Result: age 25, **money 20,772, years_worked 1, zero ledger rows**. Calling the extracted child money slice next produces a second **28,363** posting request and years_worked 2. Exact first amount depends on the legacy ambient RNG; the unledgered credit and double work-year advancement are unconditional for this salaried fixture. Existing child partners can also be aged in both composites.

**Fix design:** walk the frozen living-ID snapshot once with visited IDs; use primitive age/reset/drift operations only. Run each child slice once in its declared phase, only for eligible snapshot members. Return every cash movement through the ledger. Newborns remain age zero and receive no second yearly update. Test salaried children and shared child partners through the production coordinator, checking object balance equals ledger balance, one work-year increment and one partner age increment. Do not mock Child.age_up in that regression.

### 3. FAIL — event-driven partner death creates a funeral without killing the partner (C + A)

`src/menus/rule_actions.py:2524–2532` samples death effects/funeral but returns no graph-mutation instruction. `src/game/service.py:1492–1536` posts those effects without removing/archiving the person or marking them dead. The corresponding graph mutation exists only in the coordinator's ordinary mortality path.

**Real-service reproduction:** attach a real Partner to a real player, instantiate the shipped `partner_kidnapping` event using EventEngine, persist that pending offer, then resolve its “Do nothing” option through GameService. Result: funeral pending, **partner still attached, still in live relations, dead_ids empty**. Subsequent years can age/pay/interact with someone whose death was already announced.

**Fix design:** return a typed death instruction containing the canonical person_id and stored outcome; let A's single graph-transition operation archive/remove/mark dead exactly once for both mortality and event outcomes. Preserve journal references. Save/replay tests must prove the deceased cannot reappear and that funeral resolution resumes the same turn. The extraction helper must not resample a different cause after the event already established “presumed dead.”

### 4. FAIL — content pre-effects and automatic effects lose required consequences (A + C)

`src/game/events.py:364–366` shallowly overlays choice effects onto event pre-effects. A choice containing `stats` replaces the whole pre-effect stats map; pre-effects are also delayed until resolution and sampled separately per option. **Observed shipped example:** the kidnapping “Do nothing” offer contains only `karma: -15`, losing its event-level happiness penalty entirely.

`src/game/coordinator.py:173–190` applies automatic effects only to four core stats and journals an empty delta map. The shipped thanatophobia event's real instantiated bundle was `happiness: -4, stress: 7, health: -1`, with an empty outcome string. Stress is silently omitted and the journal text can be blank. `src/game/events.py:406–433` defaults automatic outcome text to an empty string despite available event text.

**Fix design:** sample and apply event pre-effects once at offer time, persist their completion and real deltas, then independently apply the selected stored choice. Preserve repeated stat effects in order, including clamps, rather than a shallow merge. Use a shared complete effect applier for automatic and selected outcomes, including stress/karma and ledger instructions; fall back to meaningful event text. Add forced production tests for thanatophobia and every kidnapping branch, including reload before resolution. Validate the journal against actual before/after values.

### 5. FAIL — independent legacy event chances compete in one weighted lottery (A + C)

`src/game/events.py:184–212` rolls each `chance_n` gate, appends successful events to the flavor pool, then chooses only one weighted winner. Thus passing a 1/N gate does not mean the legacy event happens; unrelated flavor content dilutes lightning, kidnapping and trait-event rates, and two successful independent events cannot both happen. `src/game/coordinator.py:263–270` leaves annual_events as a no-op; decision_offer serves this combined pool.

**Fix design:** execute the frozen annual-event checks in their declared legacy order, with resumable per-occurrence markers/queued decisions, separately from the flavor selection pool. Keep stored outcomes stable across interruptions. Test with multiple eligible independent events forced to pass and a competing flavor event present; assert every required occurrence is handled once. Isolated one-event sampling/oracle tests cannot prove this invariant. Any intended balance change needs explicit approval, not an incidental consequence of adding content.

### 6. NEEDS-FIX — parity acceptance tooling counts unperformed checks as passes (D + B)

`tools/browser_check.py:198` records parked flow checks as success. Current report rows 39, 222 and 459 show that creator keyboard, birth/naming and kidnapping were not exercised; the proof register still has DONE-PENDING-D and historical blocked claims (`docs/collab/M2/parity-matrix.md:205` onward). A later green total therefore cannot establish complete parity. The two actual browser failures also need repair rather than masking.

**Fix design:** separate pass/fail/pending/known-deviation counts. Unperformed required parity paths must fail the acceptance gate or be explicitly deferred by Claude. Use valid deterministic scenario starts to reach rare paths, then assert resulting graph, cursor and money effects through real browser actions and reloads. Refresh the proof register with exact current evidence. Rebuild creator keyboard and M1 lifecycle/resilience checks for the actual Fluent DOM. Keep the existing strict state-shape checks.

## Minor items and bounded follow-ups

- **NEEDS-FIX, A:** `src/game/finance.py:132–148`, rollback removes replayed rows that existed before the batch. Real Ledger reproduction: post +100; call post_many with the identical entry followed by an invalid amount. On failure, row count falls 1→0 and balance 100→0. Command-level envelope discard limits persisted damage today, but the helper violates its atomicity promise. Snapshot batch state or track only newly inserted rows; test existing-row replay followed by failure, including repeated keys.
- **NEEDS-FIX, C/B:** `src/game/events.py:400–404` leaves `{person:partner}` unresolved. The actual kidnapping pending text contained that literal token. Resolve roles to canonical IDs and render names consistently, including archived people; don't expose schema syntax to the player.
- **PASS with evidence limitation, M0b:** inspected Fluent contact sheets meet the basic art direction, age and skin requirements; `web/static/avatars/LICENSE-fluentui-emoji.txt` includes Microsoft's MIT notice and `web/static/gallery.html:33` credits the source. Existing phone/desktop smoke checks found no horizontal overflow or console failures. Creator keyboard and the blank gallery tile in older evidence remain unproved until refreshed. This art switch is not the main release blocker.
- **NEEDS-FIX, reports:** reconcile stale phase/report claims with frozen v4 and current code; a helper-level test or a reached HTTP endpoint is not evidence that the integrated yearly phase ran correctly.

## Fix-round acceptance

One integrated fix batch, then the single consolidated re-review: run full discovery, repaired browser acceptance with honest pending totals, and regressions for findings 1–5. Preserve M2 scope; no M3/M2.5 work. After M2 acceptance all agents halt for user testing and commit, per the process addendum.

## Self-check

This is a review checklist, not certification that the implementation satisfies the failed invariants. Unticked entries identify limits or defects above.

Logic
- [x] Tests/probes use REAL Player/Child/Partner/GameService/Ledger objects; no behavior mocked in the independent reproductions.
- [ ] Each fix has a failing-before regression — no fixes authored; required regressions specified above.
- [ ] Randomness reproducible throughout implementation — composite legacy calls and event selection still need correction (2, 5).
- [ ] All invalid inputs produce typed errors — not exhaustively established by this review.
- [ ] Every money movement posted once; journal actual deltas — fails 2 and 4.
- [x] Review reproductions identify people by person_id; identity-preserving succession tests passed in discovery/browser coverage.
- [ ] Save/load invariant — interrupted-turn persistence fails 1; general migration tests passed discovery.
- [ ] Displayed costs/eligibility match application — not certified across the unresolved event paths.
- [ ] All age/state gates server-side — not exhaustively established; newborn processing violates the phase gate in 2.
- [ ] No soft-locks — required pending flows are not all exercised; cursor/resumption fails 1.

Hygiene
- [x] Only review/status documents edited; implementation unchanged by reviewer. Shared status re-read before editing.
- [x] No reviewer probability or balance changes.
- [ ] Focused tests under three PYTHONHASHSEED values — not run; full discovery and independent reproductions run instead. Re-review must cover determinism where fixed.
- [ ] UI keyboard/focus/phone/avatar/contrast complete — phone smoke passed; creator keyboard parked, complete contrast/focus audit not established.
- [x] Rare failures independently forced using real domain fixtures and stored event offers; not dependent on a rare random event occurring naturally.

## Re-review

**FAIL — M2 is not final; user acceptance readiness remains NO.** This is the single integrated re-review requested after all four agents' fix reports. Findings 1–3 and the automatic-effect portion of 4 are substantially corrected, and browser reporting is now honest. Two original blockers remain in production integration: queued independent events are discarded, and pre-effects are not applied when an offer is served. The approved row-69 browser deferral is respected; it does not resolve these separately reproduced engine defects. M2a's original university-cursor defect is fixed, but its shared yearly engine is not fully accepted while finding 5 remains open.

### Independent verification

- Full discovery: **415 tests, OK, 10 skipped, 59.327 seconds** (`python3 -m unittest discover -s tests -t .`). The initial sandboxed attempt could not open sockets for HTTP tests; the complete rerun with local socket access passed. This was an environment limitation, not a product failure. Log: `/tmp/codex-m2-rereview-tests-allowed.log`.
- Browser: **737 PASS / 0 FAIL / 1 PENDING / 7 WARN**, exit 0. `docs/collab/evidence/browser/report.md` was regenerated by this review. The only PENDING is the explicitly approved kidnapping row 69, awaiting the dev force_event hook; all seven WARNs concern the unemployed job_label shape question. PENDING/WARN are no longer counted as passes. Creator keyboard, gallery images and the previously broken M1 flows are now exercised successfully.
- `tests.test_rules_fix_round`: **9 tests passed under each PYTHONHASHSEED 1, 7 and 42**. These helper tests pass while the production integration probes below fail, which identifies the remaining coverage gap.
- Repeated real-object probes with temporary saves; no implementation edits or behavior mocks. Seeded normal GameService progression, persisted offers, production coordinator/service commands and real Child/Partner/Ledger objects were used.

### Findings rechecked

**1 — PASS for the reproduced university interruption/resumption defect.** `src/game/service.py:1159` now resets a turn only after coordinator completion; the resolve path resumes interrupted turns. In a normal seed-4242 life the age-17 university offer persisted `t_000025`, debt_service continuation and interrupted=true. After reopening GameService and declining, the remaining year completed at age 17 with no pending choice and a fresh year_start cursor. The suite/browser also pass the funding, firing, pension and funeral flows. This does not certify the separate event-queue completion defect below.

**2 — PASS for duplicate child yearly slices and the newborn filter.** `src/game/coordinator.py:46` uses `relation_year`; `src/menus/rule_actions.py:3184` increments age and applies relation primitives without Child.age_up. The family phase filters by snapshot IDs at `rule_actions.py:2950`. Repeating the original salaried-child fixture now yields age 25, money **0**, years_worked **0** after relations_age, rather than unledgered pay and a premature work-year increment. Child income remains in its extracted ledger phase. Full suite and snapshot-filter regressions pass.

**3 — PASS for event-driven partner death.** `_resolve_rule_pending` consumes typed deaths through `_apply_event_death` (`src/game/service.py:1493`). Repeating the original real kidnapping “Do nothing” resolution now detaches the partner, records one dead ID, leaves a funeral pending and journals the stored “presumed dead” cause. The person remains referencable through the archive. This probe does not require the deferred browser force_event hook.

**4 — PARTIAL / BLOCKER REMAINS: pre-effects still are not served at offer time.** Automatic events now use the shared applier and meaningful text. A real employed-player probe of thanatophobia changed stress **10→17**, with journal deltas happiness -4, health -1, stress +7 and nonempty text: that part passes.

However, `src/game/coordinator.py:226–234` instantiates and returns a pending event without applying `pending.pre_effects` or setting `pre_applied`. Those names have no serving implementation in coordinator/service. In the production coordinator with a queued shipped kidnapping event, happiness remained **76→76**, although the persisted pending contained **pre_effects={happiness:-31}**, pre_applied absent. Resolution later applies the penalty in C's fallback (`src/menus/rule_actions.py:2500–2528`), after which the journal snapshot is taken; the pre-effect's actual delta is omitted from that decision's measured deltas.

**Required design:** apply the sampled pre-effects through the audited effect applier when the offer is installed, with an occurrence-scoped ledger key and actual-delta journal; persist pre_applied with the pending in the same transaction. Retain the fallback for older pending saves, measuring its deltas too. Initialize role IDs/targets before using the fallback: current lines 2505–2507 and 2519–2521 can reference locals before assignment for pre-effects with relationship targets. Test offer → reload → resolve through GameService, asserting the penalty happens immediately and exactly once, with clamp-aware journal deltas. An instantiate-only test proves storage, not application.

**5 — FAIL / BLOCKER REMAINS: the coordinator consumes only the first queued independent event.** The sampler now returns independent gates separately, which fixes flavor dilution inside the sampler. But `src/game/coordinator.py:170–182` pops only one occurrence; both automatic and interactive return paths continue to year_close (`:218–233`). `run_turn` marks decision_offer done and advances (`:335–348`). It never revisits the remaining queue. Resetting the completed year drops those successful events.

**Production reproduction:** initialize a real temporary-save envelope at the decision_offer boundary with independent_rolled=true and the already-selected queue `[thanatophobia_worry, partner_kidnapping]` (the state after two successful gates). Call the real `GameService.command(..., 'age_up', {})`. Result: **no pending**, used_decisions contains only thanatophobia_worry, stress correctly changes 10→17, and the turn resets to year_start. The kidnapping occurrence is lost. A direct coordinator run additionally exposes **completed=true with event_queue still containing partner_kidnapping**. The second queued event is never instantiated, so its own eligibility/effects are irrelevant to demonstrating the queue-draining failure. This reproduces the original multiple-successful-events defect without waiting for rare rolls.

**Required design:** treat decision_offer as a resumable sequence of occurrences. Drain automatic events in order, pause for each interactive event, resume the same queue after resolve/funeral, then sample flavor at most once and close only when no work remains. Do not mark the whole phase done until this sequence completes. Store per-occurrence progress and sampled outcomes; bound the loop without dropping the tail. Add production-service tests with auto→choice and choice→choice queues, save/reload between decisions, and assert every queued occurrence executes exactly once in the same year. The current test only asserting that sample_independent returns two entries cannot establish execution.

**6 — PASS with the approved deferral.** D's separate status counts and acceptance gate now work; the fresh browser run has no unexpected failures or required pending paths. The original creator/gallery/M1 coverage gaps are closed by real assertions. Row 69 remains visibly deferred rather than falsely green.

**Minor findings:** ledger replay rollback **PASS** (`src/game/finance.py:132`): repeating +100 replay followed by an invalid entry now preserves both the original row count and balance. Person-role text **PASS**: the real kidnapping offer contains the partner's actual name, without schema tokens. M0b concurrency/gallery/keyboard regressions passed the full suite and browser run; no new art blocker found.

### Handoff

Send the remaining finding-4 and finding-5 integration gaps to A/C with the above concrete regressions. This completes the permitted single re-review; no further review cycle or next-milestone drafting is started here. Claude can coordinate resolution under the user's review-cadence rule. M2 has not met the condition for final acceptance/HALT yet.

### Re-review self-check

Logic
- [x] Independent behavior probes used real Player/Child/Partner/GameService/Ledger objects; no behavior mocks.
- [ ] Each fix has a failing-before production regression — two remaining integration paths lack it; specific tests prescribed above.
- [x] Seeded probes reproducible; fix-round tests passed three hash seeds. Complete implementation RNG correctness is not certified.
- [ ] All invalid-input paths typed — not exhaustively re-audited; pre-effect fallback has uninitialized-local paths noted above.
- [ ] Money/deltas exact throughout — ledger rollback passes; pre-effect journal accounting still fails finding 4.
- [x] Reviewed death/relationship transitions use person_id; archived partner references remain available.
- [x] Reopened real service at university interruption; persistence/resumption passed. Queue persistence/completion exception is explicitly recorded in finding 5.
- [ ] All displayed costs/eligibility verified — deferred kidnapping browser path remains unperformed.
- [x] Snapshot newborn exclusion rechecked; server-side gating regressions passed discovery/browser acceptance.
- [ ] No decision/progression loss — queued successful occurrences are still lost in finding 5.

Hygiene
- [x] Only review/status documents edited by reviewer; browser tooling regenerated its owned evidence. Status reread before editing.
- [x] No reviewer probability or balance changes.
- [x] Full suite passed; focused fix-round tests passed PYTHONHASHSEED 1, 7 and 42.
- [ ] Complete UI contrast/focus audit — current keyboard/gallery/phone assertions passed, but exhaustive contrast testing was not repeated.
- [x] Rare-path probes use stored selected events and real production transitions rather than waiting for a lucky random gate.

## Final verification (Claude, 2026-11-16)
Codex's re-review blockers #4 and #5 verified by Claude: the code is on disk (coordinator.py:271 pre_applied, :280/:385 _keep_phase); the forced service-level tests pass (FixRoundServiceLevelTests); the full suite has 420 tests OK; browser_check PASS=739 FAIL=0 PENDING=1 (the approved kidnapping deferral). **M2 is FINAL.**
