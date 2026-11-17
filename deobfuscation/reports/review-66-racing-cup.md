# Review #66 — racing-cup recovery (independent native re-read)

Reviewer: OpenCode (t1h continuation, task #66) · 2026-11-17
Scope reviewed: `SimRaceCarDriver ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` —
the only racing-cup repair currently in `tools/RecoveredMethodFixer/` (RacingCupMethodRepairs.cs).
`ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario`, `ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario`
and `ScenarioRacingCupSeriesDemolitionRaceResults::RefreshScenario` have no repair source yet (mid-recovery under #68);
nothing to review there until they land.

## Method

- VA re-resolved independently: `python3 tools/method_address.py GetNPCWinningDriver` → idx 26650, low36 `0x3967d78`
  (inside `__TEXT.il2cpp` 0x370a280–0x76af590). Matches the repair source's comment.
- Disassembled `0x3967d78`–`0x3967ecc` (160 instructions) with `ipsw macho disass --vaddr ... --cache <scratch>`
  (native evidence untouched; a2s cache kept out of the evidence tree).
- Compared against the fixer-emitted IL dumped from the patched assembly via `tools/IlMethodDump`.

## Result: APPROVE — no mismatches found

Verified equivalent on every axis the task listed:

1. **Control flow**: MoveNext-first loop (`bl 0x5f64a28` / `tbz w0,#0`) matches `br loopCheck` entry;
   `cmp w8,#1; b.ne` matches `ldc.i4.1; bne.un` with identical polarity; found-path `b 0x3967e58` matches
   `get_Key; stloc; leave done`; exhausted-path `mov x19,#0` matches the null-initialized V_3.
2. **Field offsets**: `ldr x0,[x19,#0x8]` = `competitorResults` at struct offset 0x8 of the by-value
   `RacingCupSeriesResults` parameter — the IL's `ldarga.s; ldfld` handles this correctly.
   `Placement` (int at value-struct +0x0, read at enumerator+0x18 via `ldr w8,[sp,#0x28]`) matches
   `get_Value; ldfld Placement`. Key at `[sp,#0x20]` (KVP+0x0) matches `get_Key`.
3. **Null check**: native `cbz x0 → 0x1eca75c` (NRE throw helper) before the enumerator exists; the IL's
   `callvirt GetEnumerator` on the loaded field raises NRE at the same point, so no spurious Dispose —
   consistent with the native landing pads which only dispose once the enumerator exists.
4. **Call targets**: 0x5de1bac/0x5f64a28/0x5f64b5c = GetEnumerator/MoveNext/Dispose all present, in the
   same order, Dispose in a `finally` covering normal and exceptional exit.
5. **Constants**: the only constant is 1 (`ldc.i4.1`).
6. **Generic identity**: the original dummy defect (`Dictionary<System.Object,...>::GetEnumerator()` on a
   `Dictionary<SimRaceCarDriver,...>` value) is gone — one consistent instantiation everywhere.
7. **Return contract**: null when no competitor has Placement 1, captured key otherwise.

Structural note (not a defect): native reads the current entry's key/value fields directly from the
enumerator's stack slot; the IL goes through `get_Current`/`get_Key`/`get_Value` — standard managed
foreach, semantically identical. Also the native explicit 48-byte zeroing of the enumerator is covered by
`InitLocals = true`.

Caveat: static equivalence only. Runtime parity (and IL2CPP acceptance of this body) is proven only by a
successful export; no build has run since the repair was integrated (fixer exits 0, `Reconstructed 134 methods`).
