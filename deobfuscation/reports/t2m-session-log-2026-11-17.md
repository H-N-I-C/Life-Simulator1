# t2m (OpenCode) — Work Log — BitLife 3.26.1 WebGL IL2CPP Recovery

- Agent: OpenCode [t2m] in the Herdr Office
- Workspace: `/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e`
- Log written: 2026-11-17 (covers 16–17 Nov)

## Completed tasks

### #47 — Deobfuscation toolchain record
- Wrote `herdr-reports/deobfuscation-toolchain-47.md`: the Cpp2IL → Mono.Cecil repair pipeline, evidence conventions, and tool inventory.
- §6 addendum records t1h's review feedback.

### #65 — Metadata-usage verification
- Verified the reconstructed `Assembly-CSharp` metadata tokens against the native binary's usage slots: **PASS, no discrepancies**.

### #68 — Racing-cup native cross-check
- Fully decoded `GetNPCWinningDriver` native ARM64 (VA `0x3967d78`, inside `__TEXT.il2cpp`).
- Report: `herdr-reports/racing-cup-crosscheck.md`.

### #55 — Eligibility-handle repairs (2 methods)
- Hand-wrote Cecil IL for the two blocked eligibility methods: `FixPetFindEligibility` + `FixSexualityEligibility` in `tools/RecoveredMethodFixer/EligibilityHandleMethodRepair.cs`; dispatch cases added to `Program.cs`.
- Fixer method count 131 → 133; run printed `Reconstructed 133 methods`, exit 0; repaired IL re-verified with `tools/IlMethodDump`.
- Native decode: shared `GameManager.Inst` static chain (slot `0x96ce080`, token `0x2000c3d3`, kind=TypeInfo idx 50131); PetFind VAs recorded in `HANDOFF.md` §22; report: `herdr-reports/eligibility-handle-55.md`.
- Documented decisions: `typeof(ScenarioX).Name` / `.ToString()` used as the `PresentedCountForScenario`/`YearsSinceScenario` string source (the Il2CppType usage slots `0x96bb008`/`0x96bb200` can't be mapped to a typedef without the binary's metadata-registration tables — recorded as inference). Unnamed `SimPerson+0xA0` float gate and `[Life+0xD8]` initial-gender comparison **omitted** (unnamed in DiffableCs stubs; SimClassmate fields start at 0x1F0) — omitted rather than guessed.

### #67 — Verification of t1h's `GetNPCWinningDriver` repair (COMPLETE as of 17 Nov)
- Dummy defect confirmed at `il-dumps/build-35/ScenarioRacingCupSeriesOutcomeFeedback.GetNPCWinningDriver.il.txt` line 47: `Dictionary`2<System.Object,…>::GetEnumerator()` (wrong generic identity).
- Fixer build: 0 errors; run: `Reconstructed 134 methods`, exit 0 (count assertion 133 → 134).
- Emitted IL dumped via `tools/IlMethodDump`; generic identity verified correct (`Dictionary`2<SimRaceCarDriver,RacingCupSeriesResults>`; metadata: type idx 3914, fields 18706–18709, offsets 0x0/0x4/0x8/0x10; `SimRaceCarDriver` = type idx 5357).
- Native logic cross-checked against my #68 decode: struct param `RacingCupSeriesResults` → `ldarga_S + ldfld` idiom.
- **Final open question RESOLVED 17 Nov**: Cecil 0.11.6 `CodeWriter.GetParameterIndex` (Mono.Cecil.Cil/CodeWriter.cs, lines 300–310) returns `parameter.Index + 1` when `body.method.HasThis`, so the emitted `ldarga.s` byte for the first declared parameter of an instance method is **1** → loads `&results`, not `&this`. The §19 bug-class concern does not apply; t1h's repair is encoded correctly.
- Verdict: **PASS** (compile-check only — no WebGL build run, per task scope).

## In progress / pending
- **#54** — profile-decimal + school-activity methods: acked, not yet started (interrupted by #67 wrap-up and #73).
- **#73** — stuck-dispatch investigation: started 17 Nov in this session.

## Environment / infrastructure notes
- Fixer run command: `dotnet run --project tools/RecoveredMethodFixer -- "$PWD/reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins" "$PWD/unity/Unity.app/Contents/PlaybackEngines/WebGLSupport/Managed" "$PWD/unity/Unity.app/Contents/Resources/Scripting/MonoBleedingEdge/lib/mono/unityaot-linux"` with `PATH=/opt/homebrew/opt/dotnet/bin`, `DOTNET_ROOT=/opt/homebrew/opt/dotnet/libexec`. Count assertion currently `repaired.Count == 134`.
- Cecil 0.11.6 (NuGet cache also has 0.11.5), TargetFramework net11.0.
- `tools/method_address.py`: `TABLE_OFFSET=0x959C438`, low-36-bit VA mask. Known VAs: PetFind `0x38e34a4` (idx 26010), SexualityDeclare `0x3a7d0e0` (idx 27912), GetNPCWinningDriver `0x3967d78`.
- IL2CPP facts: `__TEXT.il2cpp` = `0x370a280–0x76af590`; metadata v39 has **no `types` section** (Il2CppType tables live in the binary's registration structures — unresolved rabbit hole, deliberately cut off); runtime helpers (`0x1eca4f0` metadata-init, `0x1eca75c` null-ref throw, `0x1eca620` class-init, `0x1eca624` isinst, `0x1ecac9c` InvalidCastException) are NOT in the method-pointer table; usage tokens encode `(kind << 29) | index` (kinds 1=TypeInfo, 2=Il2CppType, 3=MethodDef, 4=FieldInfo, 5=StringLiteral, 6=MethodRef).
- IL2CPP aborts after exactly 12 method-conversion failures per build (§20) — fixing methods surfaces previously masked ones. Some Cpp2IL stub helpers have no `stsfld` population (§21) — repaired getters may still throw at runtime.
- Protected evidence: never modify the native Mach-O (`ipa-extract/Payload/BitLife.app/Frameworks/UnityFramework.framework/UnityFramework`) or `extracted/il2cpp-full.json`.
- `herdr-office` bridge to t1h restored and tested 17 Nov ~12:40.

## Files produced / modified (workspace root)
- `herdr-reports/`: `deobfuscation-toolchain-47.md`, `eligibility-handle-55.md`, `racing-cup-crosscheck.md`, `t2m-session-log-2026-11-17.md` (this file)
- `tools/RecoveredMethodFixer/`: `EligibilityHandleMethodRepair.cs` (new, #55); `Program.cs` dispatch cases + count assertion (now `== 134`)
- `herdr-reports/HANDOFF.md` §22 (native decode record, #55)
- Scratch: `/tmp/opencode/elig/` (petfind/sexuality disassemblies + dumps)
