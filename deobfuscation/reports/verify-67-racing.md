# Verification #67 — racing-cup IL recovery, independent check

Verifier: DeepSeek Harness CLI [t1h] reviewing its own #66 output · 2026-11-17
Read-only: native binary, `il2cpp-full.json` and the IL under review were not modified. ipsw was run with
`--cache /tmp/t1h-verify67.a2s` so no file was added inside `ipa-extract/`.

## 1. Address resolution

`python3 tools/method_address.py <MethodName>` matches on **name only**, so it is ambiguous for shared names. Called as the
task suggests for `RefreshScenario`, it returns the *wrong* method:

```
RefreshScenario -> idx=64053 ScenarioFightingLeagueSponsorship raw=0x001000000485d724 low36=0x485d724
```

Resolving by declaring type instead (the rule already recorded in HANDOFF §14) gives the correct racing VAs, and both sit
inside `__TEXT.il2cpp` (`0x00370a280–0x0076af590`):

| method | metadata idx | VA |
| --- | --- | --- |
| `ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` | 26650 | `0x3967d78` |
| `ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()` | 26590 | `0x3960e7c` |
| `ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()` | 26682 | `0x396afec` |

**Finding (process):** anyone following the literal instruction "resolve with `method_address.py <MethodName>`" for
`RefreshScenario` would disassemble the wrong function. Name-only matching needs a declaring-type filter for overloaded or
repeated names.

## 2. `GetNPCWinningDriver` — native vs emitted IL

Disassembled `0x3967d78` (593 lines, 560 instructions). The relevant flow, read independently:

```
loc_3967dec:
  str  xzr,[sp,#0x40]              ; result slot zeroed
  zero sp+0x10..0x30               ; enumerator storage
  ldr  x0,[x19,#0x8]               ; x19 = &results -> competitorResults (Placement 0x0, Score 0x4, dict 0x8)
  cbz  x0 -> 0x1eca75c             ; null dictionary -> NullReferenceException
  ...
  bl   0x5de1bac                   ; Dictionary<K,V>.GetEnumerator into sp+0x10
loc_3967e30:                       ; loop head
  bl   0x5f64a28                   ; MoveNext
  tbz  w0,#0 -> loc_3967e54        ; loop exhausted
  ldr  w8,[sp,#0x28]               ; pair.Value.Placement (Value starts at pair+8)
  cmp  w8,#1
  b.ne loc_3967e30                 ; Placement != 1 -> continue
  ldr  x19,[sp,#0x20]              ; result = pair.Key
  b    loc_3967e58
loc_3967e54:  mov x19,#0           ; result = null
loc_3967e58:  bl 0x5f64b5c         ; Dispose(enumerator) -- reached on both paths
loc_3967e64:  mov x0,x19 ; ret     ; return result
```

Emitted IL (dumped from the patched assembly):

```
ldarga.s racingCupSeriesResults
ldfld    Dictionary`2<SimRaceCarDriver,RacingCupSeriesResults> RacingCupSeriesResults::competitorResults
stloc V_0
ldloc V_0 ; callvirt Dictionary`2<SimRaceCarDriver,RacingCupSeriesResults>::GetEnumerator() ; stloc V_1
ldnull ; stloc V_3
[tryStart] br loopCheck
loop:  ldloca.s V_1 ; call Enumerator::get_Current() ; stloc V_2
       ldloca.s V_2 ; call KeyValuePair::get_Value() ; ldfld RacingCupSeriesResults::Placement
       ldc.i4.1 ; bne.un loopCheck
       ldloca.s V_2 ; call KeyValuePair::get_Key() ; stloc V_3 ; leave done
loopCheck: ldloca.s V_1 ; call Enumerator::MoveNext() ; brtrue loop ; leave done
finally: ldloca.s V_1 ; call Enumerator::Dispose() ; endfinally
done: ldloc V_3 ; ret
```

Point-by-point comparison — **no mismatches**:

| native | IL | verdict |
| --- | --- | --- |
| `ldr x0,[x19,#0x8]` | `ldarga.s results; ldfld competitorResults` | match (0x8 is the dict; struct order Placement/Score/dict/activeRace) |
| `cbz x0 -> throw` | `callvirt GetEnumerator` (NRE on null) | match |
| GetEnumerator happens **before** the try | `stloc V_1` precedes `tryStart` | match — Dispose is not called if GetEnumerator throws, as in C# |
| `bl 0x5f64a28` / `tbz w0,#0` | `MoveNext` + `brtrue loop` | match |
| `ldr w8,[sp,#0x28]; cmp #1; b.ne` | `get_Value; ldfld Placement; ldc.i4.1; bne.un` | match (signedness irrelevant for equality) |
| `ldr x19,[sp,#0x20]` | `get_Key; stloc V_3` | match |
| `mov x19,#0` on exhaustion | `ldnull; stloc V_3` before loop | match |
| Dispose on break **and** on exhaustion | `finally` covering both `leave`s | match |
| `mov x0,x19; ret` | `ldloc V_3; ret` | match |

One deliberate difference: the native passes the enumerator's pair storage directly (`sp+0x20`/`sp+0x28`), while the IL
calls `get_Value()`/`get_Key()`. Those accessors return exactly those slots, so behaviour is identical.

**Not verified:** IL2CPP acceptance. #66 was compile-check only, so the body is verified structurally and by IL dump, not by
a converter run.

## 3. The two `RefreshScenario` methods — nothing to verify

Neither is recovered: there is no dispatch entry for
`ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario` or `...StreetRaceResults::RefreshScenario` in
`Program.cs`, and the assembly still holds dummy bodies (9 and 11 `NoteDecompilerIssue` calls respectively). So there is
no hand-written IL to compare — the earlier report stands: 1 of 3 methods recovered.

Their defect sites are unchanged and confirmed still present in the dummies:

| method | site | dummy IL |
| --- | --- | --- |
| Circuit | IL_008d | `ldfld _results; ldc.i4 0; ceq` |
| Circuit | IL_021e | `ldfld _results; call SimRacing::GetPlacementString(System.Int32)` |
| Circuit | IL_074a | `ldfld _results; ldc.i4 1; ceq` |
| Street | IL_01d7 | `ldfld _results; ldc.i4 0; ceq` |
| Street | IL_0823 | `ldfld _results; ldc.i4 1; ceq` |

Native confirms the placement argument is `this.<int at 0x178>` (`ldr w0,[x19,#0x178]; bl 0x3f9fd38`), not `_results`, so the
decompiler used the wrong receiver — guessing it would produce wrong placement text.

## 4. Verdict

`GetNPCWinningDriver` matches the native semantics at every decision point, including the subtle ones (GetEnumerator outside
the try, Dispose on both loop exits, null result on exhaustion). No mismatches found. The two `RefreshScenario` methods are
unrecovered and were not falsely presented as done. The only defect found is procedural: name-only VA resolution picks the
wrong `RefreshScenario`.
