# The 9 full-suite skips, explained (Kimi D, 2026-11-17, fix round 2)

Source: `python3 -m unittest discover -s tests -t . -v` on the current
dev tree (658 tests, 9 skipped). Each skip is listed with its explicit
in-code reason and why it is NOT invariant coverage. None is silent;
all are either performance gates or owned wiring gaps.

## Slow-test gates (6) — runnable any time with `LIFESIM_SLOW_TESTS=1`

1. `tests.test_web_m1.TestActions.test_death_ends_actions` — "lifespan
   trial loop; run with LIFESIM_SLOW_TESTS=1". Longevity trial, not a
   functional gap.
2. `tests.test_web_m2.TestM2bRelationshipFlows.test_divorce_flow` —
   "slow relationship flows; run with LIFESIM_SLOW_TESTS=1".
3. `tests.test_web_m2.TestM2bRelationshipFlows.test_pregnancy_keep_leads_to_child_after_year`
   — same slow-flow gate.
4. `tests.test_web_m2.TestM2bRelationshipFlows.test_renew_vows_and_ask_for_money`
   — same slow-flow gate.
5. `tests.test_web_m2.TestM2bRelationshipFlows.test_try_for_baby_pregnancy_pending`
   — same slow-flow gate.
6. `tests.test_web_m2.TestScenarioStarts.test_scenario_near_retirement`
   — "drive to 64 costs ~3s server-side; run with LIFESIM_SLOW_TESTS=1
   (A could speed the drive)". Performance gate; the scenario drive
   itself is covered by faster scenarios in the same class.

## Owned wiring gaps (2) — tracked, not coverage

7. `tests.test_web_m2.TestSuccessionFlow.test_dead_state_carries_succession`
   — "completed_lives record not written on surrender death yet
   (A-side wiring)". A owns the record write; the test fails rather
   than skips once wired.
8. `tests.test_web_m2.TestM2bActions.test_doctor_costs_exactly_the_fee`
   — "doctor fee not yet posted server-side (A/C M2b wiring); tighten
   once live". A/C ledger wiring for the doctor fee.

## Fixture-data limit (1)

9. `tests.test_game_service.M1FixRound1Tests.test_journal_changes_reflect_capped_values`
   — "decision has no +happiness option". The seeded decision's options
   don't include a happiness change, so the capped-value assertion has
   nothing to observe; needs a fixture with a happiness delta (owner of
   that fixture: A/C area; test file is tests/test_game_service.py).

## Note

A verbose run on the moving dev tree may additionally show transient
failures/errors from other agents' in-flight edits; the skip list above
is stable across runs. Skips are never counted as passes in D's gates;
the eligibility matrix (a tool, not part of discovery) runs 21/21 with
zero skips.
