# Task — Kimi C: fix legacy gameplay-rule bugs (groundwork for M2)

Read: docs/collab/00-PROCESS.md, 00-TEAM.md (ownership!), CODEX-HANDOFF.md ("Concrete code findings").
Kimi A is concurrently changing identity code in person.py/child.py/player.py: do NOT touch `__init__` identity fields, appearance, set_parent, finalize_appearance or convert_child_to_player. Re-read files fresh before each edit.

Fix these, each with a test in tests/test_rules_<topic>.py that fails before your fix (use fixed seeds; patch input/print if legacy code prompts):
1. Retirement/pension: lose_job zeroes job_hours, but yearly income uses job_hours, so pension income is wrong. Pensions must be paid correctly every year after retirement.
2. Overtime: work hours change the taxable amount, but yearly income starts from the unadjusted salary. Make the gross pay, tax and net pay consistent.
3. Child.age_up computes after-tax/expense income, then credits the full salary. Credit the correct net.
4. Partner-initiated divorce in Player.random_events: `choices`/`choice` mix-up and the `self.parter` typo. Fix it so both branches work.
5. University expulsion state (see handoff; find it in src/menus/ or player.py) must be consistent.
6. From Claude's review: the Custom Life path in src/menus/start.py returns before parents are announced and before a sibling can be generated. Custom lives should get the same family setup as random lives. `int_input_range_optional` can never return None, so blank input should return None. In activities.py, the toddler toy lines are unreachable (age < 3 inside a 3–12 menu) and two lines aren't wrapped in _().
Write the money logic as one small, clearly named helper (e.g. annual_income_breakdown) that returns gross, tax, expenses and net, so M2's finance ledger can reuse it. Don't change game balance beyond fixing the bugs.

Report: docs/collab/M1/kimi-c-report.md (per bug: cause, fix, test), then `hc send t7`. Focused tests only; Codex runs the full suite.
