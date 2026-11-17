# Verification #63 — racing-cup recovery, independent re-check + evidence integrity

Verifier: DeepSeek Harness CLI [t1h] · 2026-11-17 · Read-only review. Disassembly used
`ipsw macho disass … --cache /tmp/t1h-63.a2s`, so nothing was written inside `ipa-extract/` during this check.

## 1. Independent disassembly

Re-resolved and re-disassembled from scratch (no reuse of earlier dumps):

| method | metadata idx | VA | window |
| --- | --- | --- | --- |
| `ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` | 26650 | `0x3967d78` | 200 instr |
| `ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()` | 26590 | `0x3960e7c` | 120 instr |
| `ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()` | 26682 | `0x396afec` | 120 instr |

All three VAs fall inside `__TEXT.il2cpp` (`0x00370a280–0x0076af590`).

## 2. `GetNPCWinningDriver` — branches, offsets, call targets

Fresh native loop (`0x3967d78`):

```
0x03967dfc  ldr  x0,[x19,#0x8]        ; competitorResults (struct: Placement 0x0, Score 0x4, dict 0x8)
0x03967e00  cbz  x0 -> 0x3967e7c      ; null -> 0x1eca75c (NullReferenceException helper)
0x03967e28  bl   0x5de1bac           ; Dictionary`2.GetEnumerator into sp+0x10
0x03967e38  bl   0x5f64a28           ; MoveNext
0x03967e3c  tbz  w0,#0 -> 0x3967e54  ; loop exhausted
0x03967e40  ldr  w8,[sp,#0x28]       ; pair.Value.Placement (pair at sp+0x20, Value at +8)
0x03967e44  cmp  w8,#0x1
0x03967e48  b.ne 0x3967e30           ; Placement != 1 -> continue
0x03967e4c  ldr  x19,[sp,#0x20]      ; result = pair.Key
0x03967e54  mov  x19,#0              ; result = null on exhaustion
0x03967e60  bl   0x5f64b5c           ; Dispose -- reached from both exits
0x03967e64  mov  x0,x19 ; ret
```

Emitted IL and its correspondence:

| native construct | IL | check |
| --- | --- | --- |
| `ldr x0,[x19,#0x8]` | `ldarga.s racingCupSeriesResults; ldfld competitorResults` | **offset matches** — 0x8 is the dictionary, consistent with declaration order Placement/Score/competitorResults/activeRace |
| `cbz x0 -> 0x1eca75c` | `callvirt Dictionary`2<SimRaceCarDriver,RacingCupSeriesResults>::GetEnumerator()` | **call target matches** — callvirt raises the same NRE |
| `bl 0x5de1bac` | same `GetEnumerator` call | target matches |
| `bl 0x5f64a28` + `tbz` | `call Enumerator::MoveNext(); brtrue loop` | **branch polarity matches** (continue while true) |
| `ldr w8,[sp,#0x28]; cmp #1; b.ne` | `get_Value; ldfld Placement; ldc.i4.1; bne.un` | **field offset and comparison match** |
| `ldr x19,[sp,#0x20]` | `get_Key; stloc result` | matches (Key is the pair's first slot) |
| `mov x19,#0` | `ldnull; stloc result` before the loop | matches |
| `bl 0x5f64b5c` at `0x3967e58` | `finally { Dispose }` covering both `leave`s | matches — Dispose runs on break **and** exhaustion |
| `mov x0,x19; ret` | `ldloc result; ret` | matches |

**No mismatches.** The one intentional difference is that the native reads the pair slots directly while the IL calls
`get_Key`/`get_Value`; those accessors return exactly `sp+0x20`/`sp+0x28`, so behaviour is identical.

## 3. The two `RefreshScenario` methods — unrecovered, nothing to compare

No dispatch entry exists for either, and both bodies still contain `NoteDecompilerIssue` dummies (9 and 11 occurrences).
So there is no hand-written IL to verify for them, and none was claimed as done. Their known defect sites
(`ldfld _results` followed by `ldc.i4 0/1; ceq`, and `ldfld _results; call GetPlacementString(int)`) are unchanged.

## 4. Evidence integrity

**Original evidence files are byte-identical to the IPA:**

| file | on disk | inside app.ipa |
| --- | --- | --- |
| `Frameworks/UnityFramework.framework/UnityFramework` | `6b4a4b4568a1c53940c216a3fd126ae21f4582a8` | `6b4a4b45…` (identical) |
| `Data/Managed/Metadata/global-metadata.dat` | `6a691594910cb775ce18f1c1dd7794b13600d2f9` | `6a691594…` (identical) |

UnityFramework mtime is still `11-04 11:02`, size 169,308,640 — untouched.

**`extracted/` is clean:** zero files modified on 17 Nov; `il2cpp-full.json` still `16 Nov 18:21`; `decrypted/`, `textassets/`,
`game-classes.txt` unchanged.

**`ipa-extract/` has additions but no overwritten originals** — 130 files are newer than `app.ipa`:

1. **129 DLLs** under `Payload/BitLife.app/Data/Managed/`, all mtime `16 Nov 19:34`. `app.ipa` contains **no** managed DLLs
   (only `Metadata/global-metadata.dat`, `Resources/*.dll-resources.dat`, `mono/4.0/machine.config`), so these are generated
   reconstruction output staged inside the extracted tree — nothing was overwritten.
2. **`UnityFramework.a2s`**, 17,903,157 B, mtime `17 Nov 11:43` — ipsw's address→symbol cache. It is created by
   `ipsw macho disass` whenever `--cache` is not given. **This one was written by the racing recovery run itself** (the
   120-instruction dumps), not by a foreign process. It is regenerable and not evidence, but it is a file *added inside*
   `ipa-extract/`.

So the strict claim "no files under ipa-extract/ or extracted/ were modified" holds for every **original** file (verified by
hash), but is **false as stated** for the tree: one cache file was added to `ipa-extract/` by the recovery work, plus the 129
generated DLLs. Recommended: run ipsw with `--cache /tmp/…` (as this review did) and move the generated DLLs to
`recovered/`.

## 5. Verdict

`GetNPCWinningDriver`: IL matches the ARM64 at every branch, field offset and call target — **no mismatches**.
The two `RefreshScenario` methods remain unrecovered (no false claim of completion).
Evidence: both native binaries and the metadata are byte-identical to the IPA; `extracted/` untouched; the only tree changes
are additions (129 generated DLLs + one ipsw cache file).
