# Racing-cup recovery — independent native cross-check (#68)

Agent: OpenCode [t2m] · 2026-11-17 · Read-only on native evidence (UnityFramework Mach-O, `il2cpp-full.json`, `global-metadata.dat`).

## 1. Scope and method

Sample: the three racing-cup methods from the build-35/39 IL2CPP failure list.

| Method | Metadata idx | Declaring type (idx) | low36 VA | Disassembly |
|---|---|---|---|---|
| `SimRaceCarDriver ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` | 26650 | ScenarioRacingCupSeriesOutcomeFeedback (3923-adj.) | `0x3967d78` | 518 instrs |
| `System.Void ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()` | 26682 | ScenarioRacingCupSeriesStreetRaceResults | `0x396afec` | 949 instrs |
| `System.Void ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()` | 26590 | ScenarioRacingCupSeriesCircuitRaceResults | `0x3960e7c` | 943 instrs |

Indices, declaring types and low36 addresses were re-derived independently from `extracted/il2cpp-full.json`
(`normalized_result.methods[]` → `declaring_type` → `types[]`), including the method-range invariant
(`method_start <= idx < method_start + method_count`). **All three check out; no discrepancies.**

VAs resolved with `python3 tools/method_address.py <Name>` (table at file offset `0x959c438`, low-36-bit mask),
disassembled with `ipsw macho disass … --vaddr 0xADDR -c N`. Segments have VM addr == file offset, so the
`adrp/add`-computed data addresses can be read directly from the file.

### Native helper legend (recurs throughout the family)

| Address | Role (from repeated context) |
|---|---|
| `0x1eca4f0` | one-time metadata-usage initializer (takes tagged slot address; ~50 calls per method — **IL2CPP machinery, not game logic; must NOT be reproduced in IL**) |
| `0x1eca75c` | null-reference throw helper (every `cbz` null-guard funnels here) |
| `0x1eca768` | second throw helper (reached from bounds/`b.ls` checks — IndexOutOfRange-class) |
| `0x1eca580` | runtime alloc/box helper (called with a class slot + small constant) |
| `0x1eca748` / `0x1eca620` | static-field / class-init helpers |
| `0x5de1bac` | `Dictionary`2::GetEnumerator` native impl |
| `0x5f64a28` | enumerator `MoveNext` |
| `0x5f64b5c` | enumerator `Dispose` |

## 2. GetNPCWinningDriver @ 0x3967d78 — full plain-language reading

```
SimRaceCarDriver GetNPCWinningDriver(RacingCupSeriesResults results)   // x0=this, x1=results
{
    var result = (SimRaceCarDriver) null;                 // x19; [sp+0x40] zeroed too
    // one-time metadata init guarded by flag byte [0x9bdf000+0x32] (6 usage slots)  ← not IL

    var dict = results.<field at offset 0x8>;             // ldr x0, [x19, #0x8]
    if (dict == null) → throw NullReferenceException      // cbz → 0x1eca75c; landing pads dispose + unwind

    var e = dict.GetEnumerator();                         // bl 0x5de1bac; 48-byte enumerator zero-inited at sp+0x10
    while (e.MoveNext()) {                                // bl 0x5f64a28; tbz w0,#0 exits loop
        if (<int32 field at enumerator offset 0x18> == 1) // ldr w8,[sp,#0x28]; cmp w8,#1; b.ne loop
        {
            result = <ref field at enumerator offset 0x10>;   // ldr x19,[sp,#0x20]
            break;                                        // → loc_3967e58
        }
    }
    e.Dispose();                                          // bl 0x5f64b5c — runs on normal AND exceptional exits
    return result;                                        // null if never matched
}
```

What the hand-written IL must reproduce (and only this):

1. **Null check** on the dictionary field (`ldfld` at offset 0x8 of the parameter) → `newobj NullReferenceException; throw`.
2. **foreach** over that dictionary: `GetEnumerator` → `MoveNext` loop via `ldloca` on the enumerator local → `Dispose` in a **finally** (the native code disposes on the exceptional path too — landing pads at `0x3967e7c`–`0x3967ec8`).
3. The in-loop test compares an **int32** field against `1` and, on match, captures the **reference** field (the other half of the KeyValuePair) as the return value; loop-exit value is `null`.
4. Return the captured reference.

**Divergences found in the build-35 dummy IL** (what any repair must fix):

- **Generic-identity mismatch (the actual "RacingCupSeriesResults stack type" defect):** the dummy loads the field as `Dictionary<SimRaceCarDriver, RacingCupSeriesResults>` but then calls `Dictionary<System.Object, RacingCupSeriesResults>::GetEnumerator()` on it. Two different instantiations on one stack value is exactly the stack-type error IL2CPP reports. One consistent instantiation must be chosen.
- **The loop test compares an unset variable:** `ldloc V_30; ldc.i4 1; ceq` — V_30 is never assigned on any path. The native test is an int32 `== 1` on a field reached through the enumerator/current item.
- **`ceq; ldc.i4.0; ceq` double negation** everywhere — produces int32-not-bool stack noise; use a single `ceq`/`brtrue` polarity instead.
- **Spurious `NoteDecompilerIssue`** calls and dead `newobj OutOfMemoryException` — decompiler confessions, not game code; drop them.
- **Null-check path is wrong:** the dummy builds a `NullReferenceException` but then branches into unrelated code; natively the null dictionary throws immediately (with the enumerator-finally semantics not yet entered).
- The dummy's `MoveNext`/`Dispose` via `ldloca V_35` **is** the correct shape — keep that part.

## 3. StreetRaceResults::RefreshScenario @ 0x396afec — structural reading

~950 instructions, 152 calls (53 of them the metadata initializer), 98 branches. Shape:

1. **Init + field resets:** one-time init (flag `[0x9bdf000+0x43]`, 22 usage slots); then `this.<bool@0x28> = false` (`strb wzr,[x19,#0x28]`) and `this.<field@0x30> = [static 0x974d1f8]`.
2. **Null-guard funnel:** `this.<field@0x190>` is loaded **9+ times**, each load followed by `cbz → 0x396b478` (throw helper `0x1eca75c`). A second guard chain dereferences `[x8+0x28]` and `[x22+0x10]`. Every dereference is guarded — the IL must carry the same null checks.
3. **Helper sequence:** `0x395301c(field@0x190, 0)` → `csel` between two static slots → stored to `this.<field@0x38>`; later `0x3fdd064(x, 0)`, and a bool byte `[x22+0x338]` `csel` picks between statics `0x97ab04d0` / `0x97a0390`.
4. **State machine** on `w8 = [x19+0x178]` (int field at offset 0x178):
   - `== 1` → `0x1eca580(class=[0x96c7130], 7)` — construct/box with constant **7**; `[x+0x18] == 0` → throw (`0x1eca768`); store static `[0x978ee58]` at `[x+0x20]`.
   - `!= 0 && != 1` → same construct but stores `[0x978d2a8]` instead — **different constant source, same shape** (easy to get wrong: two nearly identical branches).
   - `== 0` → no construction: `0x3952ed4(field@0x190, 0)`, `x1=[x0+0x40]`, then 4-arg call `0x6834748([0x9790428], x1, [0x97367b8], 0)` and jump past the populate tail.
5. **Populate tail** (shared by the state 1 / state other paths): a **bounds-checked sequence** over `w8 = [x22+0x18]` — `cmp #1; b.ls throw` → store `[x0+0x40]`→`[x22+0x28]`; `cmp #2; b.eq throw` → store; `cmp #3; b.ls throw` … filling slots at offsets `0x20,0x28,0x30,0x38,0x40,0x48,0x50` (7 slots — matches the constructor constant 7). These are per-element bounds checks; each is a distinct branch the IL must mirror.
6. **Post-build:** `0x68350e0(obj, 0)`; re-read `field@0x190`, `0x395301c` again → `tbnz w0,#0` (branch polarity: **true skips** the `[x19+0x198]`/`[x8+0x48]`/`0x6834748` fallback that produces `x20`); `0x6823894(x21, x20, 0)` → stored to **`this.<field@0x48>`**.
7. **Tail:** `0x1eca748`+`0x693c9ec` (static setup); `0x395301c` once more → `cbz w22` selects static `0x974c2f8` vs `0x97446d8`; `0x3e9c520(x0, x1, 0, 1, 0)` — note the **hard-coded constant 1 in w3**; a `blr x9` virtual call through vtable slots `[x8+0x308]/[x8+0x310]`; `0x3ea252c(this, x20, 0)`; then two this-call helpers `0x396b918(this)` → `this.<field@0xa0>` and `0x396b484(this)` → `this.<field@0xa8>`; epilogue.

**Dummy-IL divergences to flag:** the build-35 dummy (741 lines) contains `NoteDecompilerIssue("Method not found @1ECA580")` / `@1ECA748` — the decompiler itself recorded that it could not resolve the very helpers I identify above (alloc + static-init). It also emits explicit `NullReferenceException`/`IndexOutOfRangeException` constructors at `IL_07a6`/`IL_07b4`, which matches the native throw helpers — the *placement*, not the object model, is what needs repair. Any hand-written IL must not carry over `NoteDecompilerIssue` strings, and must reproduce the **state-1 vs state-other asymmetry** (different stored statics) and the **true-skips-fallback polarity** of the `tbnz` at `0x396b364`.

## 4. CircuitRaceResults::RefreshScenario @ 0x3960e7c — structural reading

~945 instructions, 147 calls, 86 branches. Same family shape as street, plus two **dictionary foreach loops** of its own:

- State field `[x19+0x178]` read at `0x3960fdc`, written at `0x3961610/0x396170c` (state transitions *inside* the method — the IL must store back to this field).
- `this.<field@0x190>` is the recurring guarded datum (12+ loads).
- Same helper family: `0x395301c` ×5, `0x3952ed4` ×5, `0x6834748` ×2, `0x68350e0`, `0x6823894`, `0x3e9c520`, `0x3ea252c`, `0x3ea23f4`, plus `0x3fdd064` ×3.
- **Enumerator loop #1** at `0x39619d8`: `0x5de1bac` (GetEnumerator) → `0x5f64a28` (MoveNext) → `0x3fdd064` per element → `0x5f64b5c` (Dispose, called twice = normal + exceptional paths).
- **Enumerator loop #2** at `0x3961c4c`: same GetEnumerator/MoveNext pattern with `0x3fdd064` per element.
- Five `blr x9` virtual dispatches — the IL will need `callvirt` on the corresponding interfaces.

The dummy IL for this method suffers the same defects as the street one (mixed instantiations, `NoteDecompilerIssue` placeholders, int/bool stack noise).

## 5. String literals — where they actually live (feeds #66)

The racing-cup literals are **not in the Mach-O**. Byte probes for `"You won the"`, `"RacingCup"`, `"street race"`, `"trophy"` find nothing (only unrelated hits like `OccupiedWord`, sound-file names in other sections). The literals live in **`global-metadata.dat`** (`ipa-extract/Payload/BitLife.app/Data/Managed/Metadata/global-metadata.dat`):

- `stringLiterals` section: offset `0x17c`, size `0x46050`, 71,700 entries; `stringLiteralData` at `0x461cc`, 1,896,615 bytes, 71,699 unique strings.
- REA decoded these into `extracted/il2cpp-full.json` (`normalized_result.string_literals`, 71,699 entries). Probes there succeed: `"RacingCup"` → `a16_sndRacingCupBronze.aiff` / `a16_sndRacingCupGold.aiff` (idx 53916–53918), `"street race"` ×16, `"trophy"` ×16, `"School Activity"` (idx 38943).
- The native code reaches literals via the **tagged metadata-usage slots** (`0xc00341e5`-style tokens in `__DATA`), initialized by `0x1eca4f0` — the token encodes a metadata index, and the runtime resolves it against the metadata file at load. **So: verify literal text/length/encoding against `global-metadata.dat` (or the JSON's `string_literals`), never against Mach-O bytes.** No discrepancies were found between the JSON literal table and the metadata section layout it documents.

## 6. Verification status and caveats

- Metadata side (indices, declaring types, method ranges, parameter rows, `System.Decimal` availability at type idx 9461, `JoinSchoolActivityContext`/`SimSchoolActivity`/`DataSchoolActivity` registrations): **all verified, no discrepancies** (see #65 findings).
- Native readings above are from direct disassembly; control flow, field offsets, call targets and constants are quoted from the disassembler output. Generic-argument identities of helper calls (e.g. which dictionary instantiation `0x5de1bac` operates on) are inferred from the call context + dummy-IL field declarations, and should be confirmed against the metadata usage slots during repair.
- Build 39 was the last build to reach IL2CPP (12 failures). Builds 40/41 never reached the IL2CPP stage (log froze in shader serialisation — see HANDOFF §18), so "0 IL2CPP errors" in those logs is **not** a pass. The fixer count is 125 (the treat-method repair); **no racing-cup repair exists in `tools/RecoveredMethodFixer/` yet**, and the project DLL still carries the build-35 dummy bodies (verified by re-dump — byte-identical to the build-35 dumps).
- Native evidence untouched throughout; scratch disassemblies live in `/tmp/opencode/crosscheck/`.
