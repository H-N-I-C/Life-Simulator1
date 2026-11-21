# M3 robot run with undo mix (Kimi D QA) — seed 41, 200 lives, 1036s

Command: `python3 tools/robot_m3_run.py --lives 200 --seed 41 --undo-bias 0.15` (tools/robot_player.py subclassed, shared robot unmodified; TMPDIR=~/.lifesim-tmp)

## Checks

- **5xx**: PASS (0)
- **invariants_money_revision**: PASS (0 invariant failures)
- **soft_locks**: PASS (0)
- **undo_redo_identical**: PASS (591 cycles)
- **18plus_minor_probes**: see findings (unexpected status on a probe)
- **money_conservation**: checked every state (money == ledger sum) by the robot Invariant
- **journal_no_repeat_within_3y**: FAIL (1613 violations by kind {'relationship': 1477, 'health': 63, 'education': 4, 'decision': 9, 'activity': 24, 'life': 11, 'death': 18, 'finances': 3, 'work': 4})

## Counts

```json
{
 "hard_failures": 0,
 "findings": 162,
 "journal_repetition_violations": 1613,
 "undo_cycles": 591,
 "redo_mismatches": 0
}
```

## Robot stats

```json
{
 "commands": 5402,
 "decisions": 2156,
 "age_ups": 3032,
 "continuations": 1,
 "duplicate_submits": 231,
 "expected_4xx": 853,
 "reloads": 1036,
 "undo_cycles": 591,
 "redo_mismatches": 0,
 "minor_romantic_probes": 711
}
```

## Findings (typed 4xx mismatches; reported, not gated)

- life 0 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life0-unexpected_4xx-20261120-203803.json
- life 1 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life1-unexpected_4xx-20261120-203829.json
- life 3 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life3-unexpected_4xx-20261120-203839.json
- life 4 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life4-unexpected_4xx-20261120-203846.json
- life 6 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life6-unexpected_4xx-20261120-203848.json
- life 7 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life7-unexpected_4xx-20261120-203851.json
- life 8 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life8-unexpected_4xx-20261120-203851.json
- life 9 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life9-unexpected_4xx-20261120-203859.json
- life 10 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life10-unexpected_4xx-20261120-203900.json
- life 12 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life12-unexpected_4xx-20261120-203913.json
- life 13 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life13-unexpected_4xx-20261120-203914.json
- life 14 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life14-unexpected_4xx-20261120-203914.json
- life 15 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life15-unexpected_4xx-20261120-203915.json
- life 16 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life16-unexpected_4xx-20261120-203916.json
- life 17 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life17-unexpected_4xx-20261120-203917.json
- life 18 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life18-unexpected_4xx-20261120-203918.json
- life 19 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life19-unexpected_4xx-20261120-203922.json
- life 20 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life20-unexpected_4xx-20261120-203923.json
- life 21 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life21-unexpected_4xx-20261120-203924.json
- life 22 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life22-unexpected_4xx-20261120-203925.json
- life 23 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life23-unexpected_4xx-20261120-203925.json
- life 25 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life25-unexpected_4xx-20261120-203953.json
- life 27 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life27-unexpected_4xx-20261120-204000.json
- life 29 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life29-unexpected_4xx-20261120-204021.json
- life 30 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life30-unexpected_4xx-20261120-204022.json
- life 31 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life31-unexpected_4xx-20261120-204023.json
- life 32 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life32-unexpected_4xx-20261120-204024.json
- life 33 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life33-unexpected_4xx-20261120-204025.json
- life 35 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life35-unexpected_4xx-20261120-204029.json
- life 37 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life37-unexpected_4xx-20261120-204047.json
- life 38 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life38-unexpected_4xx-20261120-204048.json
- life 40 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life40-unexpected_4xx-20261120-204051.json
- life 41 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life41-unexpected_4xx-20261120-204053.json
- life 42 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life42-unexpected_4xx-20261120-204054.json
- life 43 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life43-unexpected_4xx-20261120-204055.json
- life 45 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life45-unexpected_4xx-20261120-204100.json
- life 46 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life46-unexpected_4xx-20261120-204104.json
- life 47 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life47-unexpected_4xx-20261120-204105.json
- life 48 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life48-unexpected_4xx-20261120-204107.json
- life 50 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life50-unexpected_4xx-20261120-204210.json
- life 52 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life52-unexpected_4xx-20261120-204215.json
- life 55 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life55-unexpected_4xx-20261120-204231.json
- life 56 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life56-unexpected_4xx-20261120-204232.json
- life 57 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life57-unexpected_4xx-20261120-204234.json
- life 60 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life60-unexpected_4xx-20261120-204239.json
- life 61 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life61-unexpected_4xx-20261120-204305.json
- life 62 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life62-unexpected_4xx-20261120-204309.json
- life 63 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life63-unexpected_4xx-20261120-204313.json
- life 64 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life64-unexpected_4xx-20261120-204314.json
- life 65 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life65-unexpected_4xx-20261120-204316.json
- life 66 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life66-unexpected_4xx-20261120-204318.json
- life 67 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life67-unexpected_4xx-20261120-204320.json
- life 69 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life69-unexpected_4xx-20261120-204323.json
- life 70 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life70-unexpected_4xx-20261120-204325.json
- life 72 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life72-unexpected_4xx-20261120-204354.json
- life 73 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life73-unexpected_4xx-20261120-204357.json
- life 75 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life75-unexpected_4xx-20261120-204401.json
- life 76 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life76-unexpected_4xx-20261120-204403.json
- life 77 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life77-unexpected_4xx-20261120-204406.json
- life 78 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life78-unexpected_4xx-20261120-204408.json
- life 80 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life80-unexpected_4xx-20261120-204416.json
- life 82 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life82-unexpected_4xx-20261120-204439.json
- life 83 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life83-unexpected_4xx-20261120-204441.json
- life 84 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life84-unexpected_4xx-20261120-204443.json
- life 85 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life85-unexpected_4xx-20261120-204446.json
- life 86 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life86-unexpected_4xx-20261120-204449.json
- life 87 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life87-unexpected_4xx-20261120-204451.json
- life 88 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life88-unexpected_4xx-20261120-204453.json
- life 89 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life89-unexpected_4xx-20261120-204456.json
- life 90 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life90-unexpected_4xx-20261120-204615.json
- life 91 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life91-unexpected_4xx-20261120-204626.json
- life 94 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life94-unexpected_4xx-20261120-204634.json
- life 95 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life95-unexpected_4xx-20261120-204636.json
- life 96 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life96-unexpected_4xx-20261120-204639.json
- life 97 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life97-unexpected_4xx-20261120-204641.json
- life 98 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life98-unexpected_4xx-20261120-204648.json
- life 99 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life99-unexpected_4xx-20261120-204652.json
- life 101 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life101-unexpected_4xx-20261120-204658.json
- life 102 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life102-unexpected_4xx-20261120-204700.json
- life 103 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life103-unexpected_4xx-20261120-204703.json
- life 104 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life104-unexpected_4xx-20261120-204707.json
- life 105 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life105-unexpected_4xx-20261120-204710.json
- life 106 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life106-unexpected_4xx-20261120-204712.json
- life 107 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life107-unexpected_4xx-20261120-204715.json
- life 109 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life109-unexpected_4xx-20261120-204819.json
- life 110 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life110-unexpected_4xx-20261120-204822.json
- life 111 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life111-unexpected_4xx-20261120-204824.json
- life 112 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life112-unexpected_4xx-20261120-204827.json
- life 113 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life113-unexpected_4xx-20261120-204830.json
- life 114 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life114-unexpected_4xx-20261120-204832.json
- life 116 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life116-unexpected_4xx-20261120-204838.json
- life 119 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life119-unexpected_4xx-20261120-204847.json
- life 120 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life120-unexpected_4xx-20261120-204849.json
- life 121 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life121-unexpected_4xx-20261120-204853.json
- life 123 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life123-unexpected_4xx-20261120-204858.json
- life 124 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life124-unexpected_4xx-20261120-204900.json
- life 125 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life125-unexpected_4xx-20261120-204908.json
- life 126 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life126-unexpected_4xx-20261120-204913.json
- life 127 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life127-unexpected_4xx-20261120-204917.json
- life 130 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life130-unexpected_4xx-20261120-204925.json
- life 131 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life131-unexpected_4xx-20261120-204928.json
- life 132 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life132-unexpected_4xx-20261120-204931.json
- life 133 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life133-unexpected_4xx-20261120-204934.json
- life 134 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life134-unexpected_4xx-20261120-204937.json
- life 135 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life135-unexpected_4xx-20261120-204940.json
- life 136 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life136-unexpected_4xx-20261120-204958.json
- life 137 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life137-unexpected_4xx-20261120-205001.json
- life 138 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life138-unexpected_4xx-20261120-205004.json
- life 139 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life139-unexpected_4xx-20261120-205008.json
- life 140 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life140-unexpected_4xx-20261120-205011.json
- life 141 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life141-unexpected_4xx-20261120-205014.json
- life 142 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life142-unexpected_4xx-20261120-205017.json
- life 144 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life144-unexpected_4xx-20261120-205024.json
- life 145 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life145-unexpected_4xx-20261120-205027.json
- life 146 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life146-unexpected_4xx-20261120-205030.json
- life 147 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life147-unexpected_4xx-20261120-205033.json
- life 148 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life148-unexpected_4xx-20261120-205036.json
- life 149 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life149-unexpected_4xx-20261120-205039.json
- life 150 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life150-unexpected_4xx-20261120-205042.json
- life 151 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life151-unexpected_4xx-20261120-205044.json
- life 153 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life153-unexpected_4xx-20261120-205056.json
- life 154 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life154-unexpected_4xx-20261120-205101.json
- life 155 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life155-unexpected_4xx-20261120-205104.json
- life 156 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life156-unexpected_4xx-20261120-205107.json
- life 157 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life157-unexpected_4xx-20261120-205110.json
- life 158 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life158-unexpected_4xx-20261120-205120.json
- life 159 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life159-unexpected_4xx-20261120-205125.json
- life 160 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life160-unexpected_4xx-20261120-205128.json
- life 163 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life163-unexpected_4xx-20261120-205143.json
- life 164 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life164-unexpected_4xx-20261120-205146.json
- life 165 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life165-unexpected_4xx-20261120-205149.json
- life 166 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life166-unexpected_4xx-20261120-205152.json
- life 167 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life167-unexpected_4xx-20261120-205155.json
- life 168 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life168-unexpected_4xx-20261120-205158.json
- life 169 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life169-unexpected_4xx-20261120-205201.json
- life 170 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life170-unexpected_4xx-20261120-205204.json
- life 171 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life171-unexpected_4xx-20261120-205210.json
- life 173 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life173-unexpected_4xx-20261120-205220.json
- life 174 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life174-unexpected_4xx-20261120-205223.json
- life 176 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life176-unexpected_4xx-20261120-205237.json
- life 177 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life177-unexpected_4xx-20261120-205241.json
- life 178 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life178-unexpected_4xx-20261120-205245.json
- life 179 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life179-unexpected_4xx-20261120-205251.json
- life 180 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life180-unexpected_4xx-20261120-205255.json
- life 181 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life181-unexpected_4xx-20261120-205300.json
- life 182 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life182-unexpected_4xx-20261120-205303.json
- life 183 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life183-unexpected_4xx-20261120-205307.json
- life 184 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life184-unexpected_4xx-20261120-205310.json
- life 185 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life185-unexpected_4xx-20261120-205314.json
- life 186 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life186-unexpected_4xx-20261120-205317.json
- life 187 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life187-unexpected_4xx-20261120-205327.json
- life 188 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life188-unexpected_4xx-20261120-205331.json
- life 189 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life189-unexpected_4xx-20261120-205339.json
- life 191 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life191-unexpected_4xx-20261120-205349.json
- life 192 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life192-unexpected_4xx-20261120-205352.json
- life 193 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life193-unexpected_4xx-20261120-205356.json
- life 194 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life194-unexpected_4xx-20261120-205359.json
- life 195 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life195-unexpected_4xx-20261120-205403.json
- life 196 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life196-unexpected_4xx-20261120-205407.json
- life 197 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life197-unexpected_4xx-20261120-205447.json
- life 198 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life198-unexpected_4xx-20261120-205457.json
- life 199 `unexpected_4xx` — owner depends on the action id in the detail (careers A / education B / skills C / engine A)
  evidence: robot-seed41-life199-unexpected_4xx-20261120-205518.json

## Journal repetition samples (first 20)

- relationship ages [1, 2]: 'My relationship with Kelsey improved.'
- relationship ages [1, 2]: 'My relationship with Socrates improved.'
- relationship ages [2, 3]: 'My relationship with Kelsey improved.'
- relationship ages [2, 3]: 'My relationship with Socrates improved.'
- relationship ages [3, 4]: 'My relationship with Kelsey improved.'
- relationship ages [3, 4]: 'My relationship with Socrates improved.'
- relationship ages [4, 5]: 'My relationship with Kelsey improved.'
- relationship ages [4, 5]: 'My relationship with Socrates improved.'
- relationship ages [5, 6]: 'My relationship with Kelsey improved.'
- relationship ages [5, 6]: 'My relationship with Socrates improved.'
- relationship ages [6, 7]: 'My relationship with Socrates improved.'
- relationship ages [7, 8]: 'My relationship with Socrates improved.'
- relationship ages [8, 9]: 'My relationship with Socrates improved.'
- relationship ages [9, 10]: 'My relationship with Socrates improved.'
- health ages [9, 12]: "I've come down with the common cold."
- health ages [10, 13]: 'I recovered from the common cold.'
- relationship ages [18, 19]: 'My relationship with Kelsey worsened.'
- relationship ages [18, 20]: 'My relationship with Socrates worsened.'
- relationship ages [19, 19]: 'I had a conversation.'
- relationship ages [19, 20]: 'My relationship with Kelsey worsened.'

## Root-cause + owner (Kimi D analysis, post-run)

1. **journal_no_repeat_within_3y — FAIL (1613)** — owner **A (journal
   engine)**. Root cause: `src/game/coordinator.py:67-77` writes the fixed
   template "My relationship with {firstname} improved/worsened." every
   year the parent drift is nonzero, so the identical line recurs within
   3 years by construction (1477/1613 are this kind; the rest are
   health/activity/decision templates with the same fixed-text pattern).
   Fix direction: vary the template (A), or Claude scopes the QA rule to
   flavor/"year" lines only. Check as specified fails.
2. **unexpected_4xx x~90, all `careers_apply` 400 "unknown opening"** —
   owner **A (careers, in-flight M3)**. Root cause: the advertised
   `careers_apply` action spec presents FREE-TEXT inputs (robot boundary
   fills like "N at NNNN…"), while dispatch validates against known
   opening ids — the frozen §Careers payload is `{role_id, employer_id}`.
   Server gating is correct (typed 400, never 5xx); the advertised input
   shape must be structured (selectable opening), not free text.
3. Everything else **PASS**: 0 5xx / 0 invariant breaks (money ==
   ledger sum on every state, revision monotonic) / 0 soft-locks;
   **591 undo cycles** (age→undo→age) all replayed byte-identically with
   monotonic revision (§Undo never a re-roll, confirmed at scale);
   minor 18+ romantic probes all answered with typed 4xx (no silent
   success, no 5xx).

Harness notes: tools/robot_m3_run.py subclasses the shared robot
(unmodified); fresh save dir per run (a persistent dir makes the replay
ledger resurrect old responses — phantom 409/duplicates); TMPDIR
~/.lifesim-tmp; no browser involved.
