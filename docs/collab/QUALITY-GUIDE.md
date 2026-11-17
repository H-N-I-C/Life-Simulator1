# Code quality: build a working journey, prove its invariants

Use TokenEconomizer. Check `git branch --show-current` (`dev`), read your current task/ownership, and finish one task at a time. Agree shared fields/signatures in the contracts README before implementing against them.

1. **Test the production route.** Exercise `GameService.command` → rules → adapter → save → `get_state`, then the actual browser action. The dating handler's unit tests passed while the service rejected `dating_app`, its context lacked `person_name`, and its `offer` applier was unregistered. A helper passing does not prove the feature is reachable.

2. **Force the failure, then verify the fix.** Use real Player/Partner/Child objects and a saved fixture, a known seed, or a temporary event file with a probability-one gate. Seed 9 exercised NPC marriage with an underage target. Do not wait for kidnapping/lightning to happen naturally. Restore the old bug in an isolated copy to check that the regression actually fails.

3. **Keep advertised eligibility equal to dispatch.** Every enabled `action_specs` action must reach the real handler with the same age, school, target and affordability rules. Invalid attempts return typed errors before effects. Test both 17/18 orderings and allowed 18/18. Call the central adult guard at offer and resolve; `romantic:false` cannot downgrade a date. Laws/preferences/kinship modify adult outcomes, not eligibility.

4. **One money authority; conserved transfers.** Rules return entries; the ledger posts them. Never credit salary in `Child.age_up` and again in `child_money_year`. A settlement needs payer −$2,000 and recipient +$2,000 in one transaction/group. Check account totals, replay, actual clamped deltas and insufficient funds—not merely “one debit exists.” A $0 option stays legal even when cash is negative.

5. **Bind once, sample once, identify the occurrence.** Persist concrete person/union IDs and outcomes at offer creation. Resolve those IDs, never “current partner” or the first spouse. Use the stored occurrence/effect ID for dedupe: two equal −5 events must total −10; retrying either must add zero. Names, revisions and effect values are not occurrence identities.

6. **Finish the whole turn before closing it.** Preserve cursor, done markers, snapshot and queues across pending choices/reloads. A sampler returning two events proves little if the coordinator executes one and drops the tail. Test auto→choice, choice→choice and multiple funerals in one year; assert age/pay happen once. Recheck continuations whenever inserting a phase.

7. **Enforce privacy at serialization.** Facts are engine truth; knowledge belongs to an observer. Preserve `audience`/`fact_id` through `journal_events` and filter actual command/GET JSON, including pendings and pre-effects. Use a private sentinel to prove absence. Hiding a modal in JS does not protect its payload; private choices must not strand the save.

8. **Migrate facts, not guesses.** Use a version/marker; loading twice changes nothing. An archived parent is not an ex-lover, dating is not marriage, and adoption is not biological parentage. Preserve IDs, cash, pregnancy and stored pending targets. Do not swallow an error and replace the whole graph with an empty one.

9. **Keep integration real.** Mock only boundaries such as file I/O or clocks; a seeded RNG is preferable for outcomes. Do not stub the graph, service, ledger or domain applier that your test is supposed to verify. Reload with a fresh GameService and retry the request. Update old tests to drain legitimate new decisions while retaining their original invariant assertions.

10. **Make small changes and verify their surroundings.** Read shared functions before editing; avoid broad replacements/reformatting. After bulk edits, `rg` every affected hook/guard and earlier fix; inspect the diff, compile changed Python and run `git diff --check`. Run focused regressions under three hash seeds. Codex runs the full suite at the reserved review.

Report exact paths/tests and remaining gaps; distinguish PASS, FAIL, PENDING and WARN. A skipped eligibility matrix is not a passed matrix. End reports with `00-SELF-CHECK.md`, update your status row, and provide one reproducible browser journey before calling a feature ready.
