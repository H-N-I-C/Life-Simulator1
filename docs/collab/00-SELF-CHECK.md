# Self-check before reporting "done" (every Kimi, every task)

Go through this list and paste it, ticked, at the end of your report. Unticked items need a one-line reason.

Logic
- [ ] Tests use REAL objects (Player/Child/GameService) for behaviour; mocks only at true boundaries (I/O, clock).
- [ ] Each fix has a test that FAILS without the fix (say how you checked).
- [ ] Randomness: every draw uses the passed-in or seeded RNG, never global `random`; results are reproducible from the seed; there's no set/dict iteration before RNG use.
- [ ] Inputs are validated: invalid IDs, options, ranges, types and missing fields give typed errors, never a raw KeyError/ValueError/500.
- [ ] Money: every movement is posted exactly once through the ledger, with a unique key; the journal shows the ACTUAL before/after deltas (after caps and clamps).
- [ ] Identity: people are referenced by person_id only, never by name, seed, index or id().
- [ ] Save/load: new fields survive a pickle round-trip and old saves load (migration or default).
- [ ] Shown costs and eligibility match what is actually applied; you can't pay for something you can't afford.
- [ ] Age/state gates: the action is impossible when ineligible, server-side, not just hidden in the UI.
- [ ] No soft-locks: every decision keeps at least one always-available option (decline/do-nothing/free); a $0 life can always make a legal move (validator check + $0 test over every template).

Hygiene
- [ ] Only files and functions I own (00-TEAM.md) were edited; shared files were re-read before editing.
- [ ] No probability or balance changes beyond the agreed bug fixes.
- [ ] Focused tests pass; flaky-prone tests were run under 3 PYTHONHASHSEED values.
- [ ] UI (B): keyboard works, focus is kept, phone width has no overflow, every person shown has an avatar, contrast is ≥ 4.5:1.
- [ ] Regression tests FORCE the rare path (seeded or injected trigger, e.g. force the auto event), never rely on a random roll happening to fire. After bulk or string-replacement edits, grep that earlier fixes are still in place.
