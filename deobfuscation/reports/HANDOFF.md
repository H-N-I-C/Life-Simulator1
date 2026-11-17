# BitLife 3.26.1 WebGL recovery — Herdr handoff

Date: 2026-11-17 (Australia/Sydney)

## 1. Mission

Recover the genuine BitLife 3.26.1 IPA, preserve the original gameplay, neutralize ads/tracking, export through Unity WebGL, browser-test real gameplay, and push only verified changes to:

- Repository: `https://github.com/H-N-I-C/Life-Simulator1`
- Branch: `test` only
- Never touch or push `main`.
- Never substitute the old Life Simulator prototype.
- Do not begin extra features until the recovered original is playable and ad-free in the browser.

The user owns the local task environment and has explicitly authorized subagents. This is a private recovery/iteration exercise; do not claim that the result is ready for redistribution.

## 2. Workspace and authoritative project

Workspace root:

```text
/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e
```

Active Unity project:

```text
reconstructed/BitLife-Unity-dll/ExportedProject
```

Unity 6000.3.9f1 with WebGL support is installed and licensed at:

```text
unity/Unity.app
```

Intended WebGL output:

```text
webgl-dll-build
```

Repository checkout:

```text
repo
```

The repository is on `test`, HEAD `70d5198d`, matching `origin/test` at the last check. No recovery-phase changes have been pushed. It contains intentional uncommitted ad-resource deletions and a modification to `bitlife/Assets/Scenes/Main.unity`; preserve these. Do not adopt the repository's stubbed source as the recovered game.

## 3. Current verified state

- Original IPA assets and native IL2CPP evidence are preserved.
- REA metadata extraction and native method linking have been performed.
- Unity editor and WebGL module are installed and licensed.
- Advertising/tracking neutralization has been statically applied to 5,259 methods, including 394 in `Assembly-CSharp.dll`.
- This neutralization has **not** yet been verified by browser network testing.
- The current recovered-method fixer expects and applies exactly **124 distinct methods**.
- The most recent successful fixer log is `il-dumps/build-35-fixer.log` and ends with `Reconstructed 124 methods`.
- A whole-assembly scope audit found no remaining `System.Private.CoreLib` reference in `Assembly-CSharp.dll` after the latest correction.
- The whole-assembly audit still reports 18 non-CoreLib observations, primarily iOS-only Unity APIs and generic-interface resolution. These are not proof of a build failure; inspect the Unity linker result before changing them.
- No successful full WebGL export of the current recovered game has been verified.
- No genuine browser gameplay test has passed.
- No current recovery work has been committed or pushed.

### Latest build result and active repair queue

Build 35 (`bash-yki9fm3a`) failed in IL2CPP after passing the earlier UnityLinker error. Log: `unity-dll-webgl-build-35.log`. It exposed these 12 unique methods (the log repeats them in a second run):

1. `Game.DataOutdoorAnimalTreat DataHub::GetRandomOutdoorAnimalTreatForDangerScore(System.Int32)` — `Enumerator` stack type.
2. `System.Boolean ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush()` — `RuntimeTypeHandle` stack type.
3. `System.Void ScenarioScriptEventParentMarriesStepparent::RefreshScenario()` — `Enumerator` stack type.
4. `System.Void ScenarioSchoolActivityJoin::ChoiceSelectedAtIndex(System.Int32)` — `JoinSchoolActivityContext` stack type.
5. `System.Void ScenarioProfileGrandchild::RefreshScenario()` — `Decimal` stack type.
6. `System.Void ScenarioProfileNieceNephew::RefreshScenario()` — `Decimal` stack type.
7. `System.Void ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()` — `RacingCupSeriesResults` stack type.
8. `SimRaceCarDriver ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` — `RacingCupSeriesResults` stack type.
9. `System.Void ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()` — `RacingCupSeriesResults` stack type.
10. `System.Void ScenarioPrisonReceiveGift::.ctor()` — `Enumerator` stack type.
11. `System.Boolean ScenarioPetFind::ScenarioIsEligibleForRandomPush()` — `RuntimeTypeHandle` stack type.
12. `List LifeInterface::GetLuxuryUpscaleJewelersList()` — invalid global-variables count at a branch join.

The independent encounter audit (`agent-23`) completed and found two confirmed ghost-repair defects in `ParanormalMethodRepairs.cs`:

- Seven sites mix integer constants with the `System.Single` ghost friendliness field: fixer lines 581, 604, 611, 650, 696, 698, 715 in the audited revision. Native code uses floating-point comparisons/subtraction.
- `_didSummon` branches to `finalScenario`, incorrectly skipping chance, vanish, and attack processing. Native branches to chance processing at `0x38D02E0`; only health/heart-attack/disease processing is bypassed.

The audit found no numeric stack/type errors in the paranormal method and no unresolved branch targets, stack-height/merge failures, underflows, or nonempty returns in either encounter method. This remains static evidence, not runtime verification.

No Unity build is active after build 35 failed. The next build must wait until the above repairs are integrated and audited.

## 4. Native evidence — never delete

```text
ipa-extract/Payload/BitLife.app/Frameworks/UnityFramework.framework/UnityFramework
ipa-extract/Payload/BitLife.app/Data/Managed/Metadata/global-metadata.dat
extracted/il2cpp-full.json
```

Method pointer table file offset: `0x959c438`.

Method indices correspond to `normalized_result.methods`. Chained pointers use the low 36 bits.

Original assets, native binaries, metadata, and agent input evidence must remain intact. Only delete clearly generated build caches/output when no build is using them.

## 5. Fixer workflow

Current fixer source:

```text
tools/RecoveredMethodFixer/Program.cs
tools/RecoveredMethodFixer/*.cs
```

Run from the workspace root with exactly these dependency directories:

```bash
export PATH="/opt/homebrew/opt/dotnet/bin:$PATH" DOTNET_ROOT="/opt/homebrew/opt/dotnet/libexec"
dotnet run --project tools/RecoveredMethodFixer -- \
  "$PWD/reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins" \
  "$PWD/unity/Unity.app/Contents/PlaybackEngines/WebGLSupport/Managed" \
  "$PWD/unity/Unity.app/Contents/Resources/Scripting/MonoBleedingEdge/lib/mono/unityaot-linux"
```

Required success line:

```text
Reconstructed 124 methods
```

If new dispatched methods are added, increase the expected count by the actual number of new distinct method signatures and record the arithmetic in the handoff. Do not substitute `recovered/dummydll`, `recovered/il-dll`, or `recovered/il-dll-callanalyzer` for the dependency directories; those substitutions caused unrelated resolution failures.

### Latest corrections

- `NullableGameplayMethodRepairs.cs`: potion, hustle, friendship, and hookup repairs; damaged exception locals retyped; native `Math.Round(double)` behavior restored.
- Hustle's `System.Math.Round` must be pinned to the assembly's Unity `mscorlib` reference. Importing through host .NET reflection produced `System.Private.CoreLib, Version=11.0.0.0` and caused builds 33 and 34 to fail in UnityLinker.
- `DealerMethodRepairs.cs`: `UIBackButtonManager.Inst` is loaded before all three `RemoveLastBreadcrumb()` calls.
- `GameplayListMethodRepairs.cs`: jeweler constructor arguments are spilled to locals so branches join with an empty evaluation stack.
- `OutdoorMethodRepairs.cs`: cave button index and weighted outdoor animal selection restored. Cave uses the actual `ValueTuple<int, ScenarioChoice>.Item1` field.
- `MediaFashionMethodRepairs.cs`: OnlyFans cross-promotion and fashion-week outcomes restored.
- `ParanormalMethodRepairs.cs`: ghost and paranormal choice handling added and dispatched. The generic insult-list `Add` lookup intentionally matches by name/arity rather than requiring a literal `System.String` parameter on `List<T>`.

Ghost native entry: `0x38CF034`.

Paranormal native entry: `0x38D3600`.

Runtime behavior of these repairs remains unverified until browser testing.

## 6. IL inspection and audit

Dump one method:

```bash
export PATH="/opt/homebrew/opt/dotnet/bin:$PATH" DOTNET_ROOT="/opt/homebrew/opt/dotnet/libexec"
dotnet run --project tools/IlMethodDump -- \
  reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins/Assembly-CSharp.dll \
  "System.Void ScenarioHustleEarnings::RefreshScenario()"
```

Current dumps:

```text
il-dumps/build-34/
```

Run focused reference audit:

```bash
dotnet run --project tools/IlReferenceAudit -- \
  "$PWD/reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins/Assembly-CSharp.dll" \
  "$PWD/unity/Unity.app/Contents/PlaybackEngines/WebGLSupport/Managed" \
  "$PWD/unity/Unity.app/Contents/Resources/Scripting/MonoBleedingEdge/lib/mono/unityaot-linux"
```

Run whole-game assembly audit:

```bash
dotnet run --project tools/IlReferenceAudit -- \
  "$PWD/reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins/Assembly-CSharp.dll" \
  "$PWD/unity/Unity.app/Contents/PlaybackEngines/WebGLSupport/Managed" \
  "$PWD/unity/Unity.app/Contents/Resources/Scripting/MonoBleedingEdge/lib/mono/unityaot-linux" \
  --all
```

Latest whole-assembly audit:

```text
il-dumps/build-35-all-reference-audit.log
```

Generic placeholders are intentionally excluded from ordinary resolve failures. `CORELIB REFERENCE` is a critical finding for this Unity project and must be corrected before rebuilding.

## 7. Unity build workflow

Check no other Unity build is active. Remove only generated data and check free space:

```bash
rm -rf reconstructed/BitLife-Unity-dll/ExportedProject/Library/Bee webgl-dll-build
df -h .
```

Build from the workspace root:

```bash
./unity/Unity.app/Contents/MacOS/Unity \
  -batchmode -nographics \
  -projectPath "$PWD/reconstructed/BitLife-Unity-dll/ExportedProject" \
  -executeMethod BuildWebGL.Build \
  -buildOutput "$PWD/webgl-dll-build" \
  -quit \
  -logFile "$PWD/unity-dll-webgl-build-35.log"
```

Use a new build number/log for each attempt. Do not overwrite earlier evidence logs.

When a build fails:

1. Extract each unique IL2CPP/UnityLinker failure from the new log.
2. Dump every failing method with `tools/IlMethodDump`.
3. Recover native behavior from the original binary rather than inserting defaults, empty bodies, or stack shortcuts.
4. Assign separate methods to separate agents to avoid file collisions.
5. Agents should not write the active game DLL directly or launch competing Unity builds.
6. Integrate completed repairs, require the updated exact fixer count, dump emitted IL, and run the reference audit.
7. Remove generated build data and confirm disk space before the next export.

Recent builds have run with roughly 1.6–3.2 GiB free. Disk pressure is a real risk; do not delete original evidence to make room.

## 8. Browser acceptance gate

Only after a successful export:

1. Temporarily serve `webgl-dll-build` on port 7100.
2. Invoke the `in-app-browser` skill before browser interaction.
3. Verify the game loads without a blank screen or indefinite loader.
4. Start a new life and exercise genuine gameplay: age up, make choices, open activities, and confirm state/UI updates.
5. Inspect browser console output for exceptions.
6. Inspect network traffic and confirm ads/tracking do not execute.
7. Test any specifically repaired gameplay path that can be reached safely; otherwise mark it unverified rather than claiming parity.
8. Stop the temporary server before final delivery.

A successful Unity export alone is not proof that the game is exact, playable, or ad-free.

## 9. Repository integration and push gate

Only after genuine ad-free browser gameplay works:

1. Adopt the verified direct-DLL project into `repo/bitlife`.
2. Remove duplicate stub source and old Life Simulator material without deleting genuine recovered assets.
3. Repeat the final Unity build and browser tests from the adopted project.
4. Review `git status` and the complete diff.
5. Confirm the branch is `test` and that `main` is untouched.
6. Commit only verified changes and push only to `origin/test`.
7. Extra features come afterward as a separate pass.

Do not push merely because the fixer or Unity build succeeds.

## 10. Suggested Herdr assignments

### Coordinator / build triage

- Monitor the active build; never duplicate it.
- Extract the next unique failure and assign method ownership.
- Integrate completed repairs serially.
- Keep this handoff updated with build numbers, counts, and verified results.

### Native recovery agents

- Own one failing method or one small related method family each.
- Use native evidence and emitted IL.
- Write a focused repair source file and report native addresses, assumptions, and verification performed.
- Do not modify the active Unity project, active game DLL, repository, or build process.

### IL verification agent

- Continue the independent ghost/paranormal audit already assigned to `agent-23`.
- Check stack behavior, branch targets, signatures, and native dispatch.
- Do not claim runtime correctness from static analysis.

### Browser QA agent

- Wait for a successful export.
- Serve temporarily, test real gameplay, capture console/network evidence, and stop the server.
- Report concrete failures and reproduction steps.

### Repository integrator

- No adoption, commit, or push until browser acceptance passes.
- Preserve existing intentional ad-resource deletions.
- Push only to `test` after final verification.

## 11. Herdr operational note

At handoff time, `hc server status` reported three live terminals, but `hc ls --all` failed with `registry writer stopped`. Do not restart Herdr blindly: restarting can interrupt existing terminals. The current independent audit is running through the built-in subagent system while Herdr delegation is unavailable.

## 12. Definition of done

The recovery phase is done only when all of these are true:

- Full Unity WebGL export succeeds.
- The genuine recovered game starts and is playable in the browser.
- Real interactions and state changes are verified.
- No browser runtime exceptions block gameplay.
- Browser network evidence confirms ads/tracking do not execute.
- The verified project is adopted without reviving stub/old-prototype code.
- Final diff is reviewed and only `test` is pushed.

Until then, describe the current state as recovery/build verification in progress, not as a finished game.
## 13. Build 36 / 37 status (DeepSeek Harness CLI, 2026-11-16T23:04:47.392Z)

- Ghost-repair corrections are VERIFIED, not just edited: all seven integer/float sites now emit `ldc.r4` against the `Single` field `SimGhost::<Att_friendliness>k__BackingField` (dump shows `ldc.r4 20/10/40/50/40/100/0.5/70`, `sub` and `bge` on floats), and `ScenarioGhostEncounter::ChoiceSelectedAtIndex` now branches `ldfld _didSummon → brtrue IL_092b` which lands on chance processing (`ldc.r4 0 ; stloc chance`), bypassing only health/heart-attack/disease.
- `LifeInterface::GetLuxuryUpscaleJewelersList` was failing with `Invalid global variables count`: the watch loop emitted `brtrue`/`brfalse` directly on `ldc.i4.6/7` constants, so the jewel type stayed on the evaluation stack and joins mismatched. Fixed in `GameplayListMethodRepairs.cs` to `beq included` / `bne.un loopCheck` / `bne.un useCaratString`. Build 37 confirms the method is gone from the failure list.
- Fixer still reports exactly `Reconstructed 124 methods` after both corrections (logs `il-dumps/build-36-fixer.log`, `build-37-fixer.log`).
- IMPORTANT: the IL2CPP error list is capped/deterministic — it reports 12 methods per run. Fixing the jeweler method did not reduce the list, it surfaced a *previously masked* 13th method: `System.ValueTuple\`2<System.Int32,System.Int32> SimPodcastActivity::CalculateContentListenerCount()` (`Cannot get stack type for ValueTuple\`2`, from a binary operation). Expect further masked methods to appear after each batch; the true queue length is unknown.
- Cache cleared before build 37 (generated only, no source, no repo data): `reconstructed/BitLife-Unity-dll/ExportedProject/Library/PlayerDataCache`, `.../Library/BuildPlayerData`, `.../Library/Bee`, `webgl-dll-build`. `Library/Artifacts` was deliberately KEPT (1.9 GB) so iterations reach IL2CPP in ~15 s instead of a full reimport. Free space ~2-3 GiB.
- New helper committed to the workspace: `tools/method_address.py <MethodName>` resolves a method name to its native VA via the method pointer table at file offset 0x959c438 (low 36 bits), e.g. `GetNPCWinningDriver` → `0x3967d78`. Disassemble with `ipsw macho disass <binary> --vaddr 0x... -c N`.
## 14. #51 native addresses (resolved `tools/method_address.py`, resolver VALIDATED)

Resolver validated against repairs already in the tree: `DataHub::GetRandomOutdoorAnimal` resolves to `0x3d2e73c`, exactly the address cited in `OutdoorMethodRepairs.cs` ("Native 0x3d2e73c"). The method pointer table offset `0x959c438` + low-36-bits rule is therefore sound.

| Method | native VA |
| --- | --- |
| `ScenarioPetFind::ScenarioIsEligibleForRandomPush()` | `0x38e34a4` |
| `ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush()` | `0x3a7d0e0` |
| `ScenarioPrisonReceiveGift::.ctor()` | `0x3922b40` or `0x3920c4c` (two ctors; the failing one takes no parameters — disassemble both and keep the one that matches `Parameters=0`) |
| `ScenarioScriptEventParentMarriesStepparent::RefreshScenario()` | `0x3a1c23c` |
| `DataHub::GetRandomOutdoorAnimalTreatForDangerScore(System.Int32)` | `0x3d2f06c` |
| `SimPodcastActivity::CalculateContentListenerCount()` (build 37 addition) | `0x3a81478` |
| `ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` | `0x3967d78` |

Note on `.ctor()` disambiguation: the resolver matches by name, so overloads share a name. Resolve first, then confirm the parameter count from the dummy IL dump header (`FLAGS ... Parameters=N`).

One-command iteration loop added: `tools/iterate_webgl_build.sh [buildNumber]` — applies the fixer, checks the exact `Reconstructed N methods` count (override with `EXPECTED_METHODS`), clears only generated cache, runs the export, then writes `il-dumps/build-N/failures.txt` and dumps IL for every failing method. It refuses to run while another Unity build is active and never overwrites an existing build log.
## 15. Queue size measured — the "12 per build" list is NOT the whole queue

IL2CPP reports a capped/deterministic 12 methods per run, so builds discover the queue 12 at a time. A static scan now measures it directly.

Tool: `tools/IlInvalidScan` (Mono.Cecil). It flags a method when a load of a **non-primitive value type** (`ldloc` of such a local, or `ldfld` of such a field) is fed within 3 instructions into an arithmetic/comparison opcode — the exact shape that makes `Unity.IL2CPP.StackTypeConverter.StackTypeFor` throw `Cannot get stack type for <T>` (see the build-37 stack trace: `StackAnalysisUtils.CorrectLargestTypeFor` <- `WriteBinaryOperationUsingLargestOperandTypeAsResultType`). Primitives, enums and `Nullable<T>` are excluded because they have primitive stack behaviour.

Run:

```bash
export PATH="/opt/homebrew/opt/dotnet/bin:$PATH" DOTNET_ROOT="/opt/homebrew/opt/dotnet/libexec"
cd tools/IlInvalidScan && dotnet run -- "$PWD/../../reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins/Assembly-CSharp.dll"
```

Result on the current patched assembly (saved to `il-dumps/il-invalid-scan.txt`):

| measure | count |
| --- | --- |
| methods with a body | 67,700 |
| still containing `Cpp2ILHelpers.NoteDecompilerIssue` (raw Cpp2IL dummies) | 27,482 |
| matches the IL2CPP stack-type failure shape | **179** |
| of those, gameplay `Scenario*` methods | 47 |

Calibration: the scan reproduces **12 of 12** methods in the build-37 failure list, including `ScenarioProfileGrandchild`, `ScenarioProfileNieceNephew`, both `ScenarioIsEligibleForRandomPush`, `ScenarioSchoolActivityJoin::ChoiceSelectedAtIndex`, `DataHub::GetRandomOutdoorAnimalTreatForDangerScore`, `SimPodcastActivity::CalculateContentListenerCount` and the three racing-cup methods. Earlier looser versions of the heuristic over-counted (1,686 / 24,467) until primitives, enums and `Nullable<T>` were excluded; 179 is the calibrated figure.

**Consequence:** the remaining distance to a successful WebGL export is on the order of ~179 methods, not 12. At 12 discovered per build, merely *seeing* the whole queue costs ~15 builds, and hand-recovering each from ARM64 is far beyond that. This is a scope decision, not a coding one:

1. Keep hand-recovering native-faithful IL — highest fidelity, but realistically only a handful of the 179 per session.
2. Mechanically rewrite the structurally invalid methods into *valid but behaviourally default* IL (the shape a Cpp2IL dummy has anyway), so the export succeeds and the game is playable, while documenting the fidelity loss and keeping hand-recovery for gameplay-critical paths (the 47 `Scenario*` methods and the `DataHub` pickers are the visible ones).
3. Stop pursuing a full WebGL export and ship the already-verified parts (data recovery, ad/tracking strip, browser prototype).

No method was stubbed and no native evidence was touched while producing this measurement.

### §15 addendum — second failure class measured

The scanner now also runs an evaluation-stack-height worklist over every method body (Cecil
`StackBehaviourPop`/`StackBehaviourPush`, following branch and exception-flow edges) and flags any
instruction reachable at two different heights — the `Invalid global variables count` class that
the jeweler method used to hit. On the current patched assembly it reports **0**, which is expected:
the jeweler `brtrue`/`brfalse`-on-constant defect is already fixed in this DLL. So the outstanding
queue remains ~179 methods, all of the `Cannot get stack type` class. Caveat to keep in mind: the
balance detector has only been checked against an already-fixed method, so treat its 0 as "nothing
obvious remains", not as proof.
## 16. Fixer count now 125 (arithmetic recorded)

| | |
| --- | --- |
| previous expected count | 124 |
| new method | `Game.DataOutdoorAnimalTreat DataHub::GetRandomOutdoorAnimalTreatForDangerScore(System.Int32)` |
| new expected count | **125** |

The assertion in `tools/RecoveredMethodFixer/Program.cs` (`return repaired.Count == N ? 0 : 1;`) was raised from 124 to 125 in the same change. Source: `tools/RecoveredMethodFixer/OutdoorAnimalTreatMethodRepair.cs`, dispatched from `DataHub.GetRandomOutdoorAnimalTreatForDangerScore`.

Recovered behaviour (native `0x3d2f06c`, read independently twice — by me and by a recovery agent, identical result):

```csharp
if (DataHub._outdoorAnimalTreatSet == null) DataHub.LoadOutdoorAnimalTreatSet();
var candidates = new List<DataOutdoorAnimalTreat>();
foreach (var treat in DataHub._outdoorAnimalTreatSet)
{
    if (treat.CommonnessScore < 1) continue;        // ldr w8,[x21,#0x1c]; cmp w8,#1; b.lt
    if (treat.AnimalDangerScore > score) continue;  // ldr w9,[x21,#0x18]; cmp w9,w20; b.gt
    for (var i = 0; i < treat.CommonnessScore; i++) candidates.Add(treat);  // weight loop 0x3d2f230
}
return candidates[GameReference.GetRandomNumber(0, candidates.Count)];
```

Verification performed: `IlMethodDump` on the patched assembly shows `ldsfld DataHub::_outdoorAnimalTreatSet` (static field, **not** `ldarg.0`/`ldfld` — an earlier draft of mine had that bug and the dump caught it before a build was spent), the guard order `CommonnessScore < 1` then `AnimalDangerScore > score`, the weight loop, the enumerator used only via `ldloca`, and `finally` disposal around it. Fixer exits 0 and prints `Reconstructed 125 methods`.
## 17. Build 39 result — treat body takes effect, hits a *different* IL2CPP failure

`Reconstructed 125 methods` (exit 0). The export still fails, with the same 12-method cap, and two facts matter:

1. The treat method's error **changed**: it used to be `Cannot get stack type for Enumerator` and is now
   `System.ArgumentOutOfRangeException: Index was out of range ... at System.SZArrayHelper.get_Item[T](Int32 index)`
   thrown inside `Unity.IL2CPP.MethodWriting.ResolvedMethodContext.CreateInstruction`.
   So the replaced body is being converted, and the original stack-type defect is gone for this method.
2. `Enumerator` failures dropped 6 -> 4 (i.e. one of the three Enumerator methods left that class), confirming the new body took effect.

The emitted IL was verified by dump and is structurally the same shape as the already-passing sibling
`DataHub::GetRandomOutdoorAnimal` (same try/finally enumerator, same weight loop, same tail:
`GetRandomNumber(0, candidates.Count)` then `get_Item`). Both loaders are `public static void`, the set field is
`private static`, so `ldsfld` + `call` are the correct opcodes. Max stack of the body is 2, far under the default 8.

Not yet explained: why IL2CPP's resolver indexes out of range on this body. Candidate next experiments, each costing
one ~12 minute export:

- emit an unconditional `call LoadOutdoorAnimalTreatSet()` first, mirroring the sibling exactly and dropping the
  `ldsfld; brtrue` prefix (the only structural difference from the passing sibling);
- set `body.MaxStackSize` explicitly (e.g. 8) instead of relying on the `MethodBody` default;
- check whether the UnityLinker-managed copy differs from the plugin copy for this one method.

The faithful body remains in `tools/RecoveredMethodFixer/OutdoorAnimalTreatMethodRepair.cs` and is dispatched; it is
kept rather than reverted because it is correct by reading and its only open question is an IL2CPP-side resolver crash.
## 18. Build 40 did not produce a verdict — it hung, and my own script caused it

Build 40 (sibling-mirroring treat body, fixer 125) never reached IL2CPP. Its log froze at exactly
`4,895,491` bytes for over 30 minutes while the Unity process sat at ~80% CPU, in shader serialisation
(`Serialized binary data for shader Hidden/Physics2D/SDF_Point ...`). `IL2CPP error for method` count: 0.
I killed it; Unity is stopped; free space 4.6 GiB. `unity-dll-webgl-build-40.log` is preserved as evidence
of the hang and must not be overwritten.

**So the guard-branch hypothesis is UNTESTED, not disproven.** Build 39's `ArgumentOutOfRangeException` in
`ResolvedMethodContext.CreateInstruction` is still the open question for the treat method.

Root cause of the hang, honestly: my own `tools/iterate_webgl_build.sh` deleted
`Library/PlayerDataCache` before every build. That directory *is* the shader/player cache, so each run
recompiled every shader from cold — builds went from ~12 minutes to unbounded, and the 40th run wedged.
The script now removes only `Library/BuildPlayerData`, `Library/Bee` and `webgl-dll-build`, leaving the
shader cache intact. Earlier hand-run builds (35-39) never cleared it and completed normally, which is
consistent with this being the cause.

Next attempt, in order:
1. Re-run the export with the corrected script (shader cache preserved) so a build actually reaches IL2CPP in ~12 minutes.
2. If the treat method still throws the resolver exception with the no-guard body, try an explicit `body.MaxStackSize = 8`.
3. If it still fails, stop spending exports on it and hand the method over with this diagnosis.

## 19. Build 42 result — the treat method is FIXED; root cause was a bad `ldarg` index (2026-11-17T00:27Z)

Build 41 (sibling-mirroring treat body) still failed with `ArgumentOutOfRangeException ... SZArrayHelper.get_Item`
inside `ResolvedMethodContext.CreateInstruction`. Dumping the emitted IL against the passing sibling exposed the cause:

```
FLAGS Static=True HasThis=False Parameters=1
IL_0048: ldarg.1        <-- reads argument 1 of a method that only has argument 0
```

`DataHub::GetRandomOutdoorAnimalTreatForDangerScore` is **static**, so `score` is `ldarg.0`. The body loaded `ldarg.1`
(an instance-method assumption). IL2CPP indexed its parameter list with 1 and crashed — the resolver crash was never about
the try/finally, the guard, or MaxStackSize. Fixed to `ldarg.0` in `OutdoorAnimalTreatMethodRepair.cs`.

**Build 42 verdict:** `Reconstructed 125 methods`; the treat method is **gone** from the IL2CPP failure list, and a
previously masked method surfaced in its slot (`DataHub::GetRandomOutdoorBikeRideLocationData()`), exactly the 12-cap
behaviour. Remaining failures: 12 (racing trio, two eligibility checks, podcast ValueTuple, prison-gift ctor, profile pair,
school-activity choice, parent-marries refresh, outdoor bike-ride location).

### New guard in the fixer — `ValidateBody`

`tools/RecoveredMethodFixer/Program.cs` now runs `ValidateBody` on every repaired method before the assembly is written
and exits **3** with `INVALID BODY <method>: <instruction>` if the body would be rejected by IL2CPP. It checks:

- `ldarg`/`ldarga`/`starg` (both implicit `Ldarg_0..3` and operand forms) against the real argument count
  (`HasThis ? Parameters.Count + 1 : Parameters.Count`) — this is the check that catches the defect above;
- `ldloc`/`ldloca`/`stloc` against the declared local count;
- branch and `switch` targets, and exception-handler boundaries, are instructions inside the body.

Proven both ways: reintroducing `ldarg.1` makes the fixer exit 3 with
`INVALID BODY ...: IL_0000: ldarg.1 reads argument 1 but the method only has 1` and **no** assembly is written; with the
fix, all 125 repairs pass and it prints `Reconstructed 125 methods`. All 125 previously written bodies therefore also
pass this check — no other repair has a bad argument/local index or an out-of-body branch.


## 20. The 12-error cap is IL2CPP's own abort — proven; and the option-2 tool is ready (2026-11-17T00:40Z)

### The cap is internal to IL2CPP, not Unity's build system

Ran the converter directly, with the exact command line Unity used (extracted from `unity-dll-webgl-build-42.log`,
9,445 chars) against the same `ManagedStripped` inputs and the same `--generatedcppdir`:

```
PROBE EXIT=255
12        # ^Error: IL2CPP error for method  (exactly 12)
```

So IL2CPP itself stops after 12 method-conversion failures and exits 255. Nothing in Unity's build graph or the log
capture is trimming the list, and there is no CLI option to raise the limit (`il2cpp --help` has no error/max/skip
switch). **The queue can therefore only be discovered 12 at a time, ~15 builds for 179.**

### The scanner tracks IL2CPP exactly

Re-ran `tools/IlInvalidScan` on the assembly as build 42 left it:

| measure | before treat fix | after |
| --- | --- | --- |
| methods with a body | 67,700 | 67,700 |
| still Cpp2IL dummies | 27,482 | 27,481 |
| stack-type-invalid | 179 | **178** |
| stack-height-imbalanced | 0 | 0 |

The treat method dropped out of the flagged set in the same step that IL2CPP stopped reporting it, so the scanner is
calibrated on both directions (12/12 on the visible failures, and it removes a method when it is fixed).
List saved to `il-dumps/il-invalid-scan-42.txt`; the 178 method names to `il-dumps/option2-methods.txt`.

### Option-2 tool built, dry-run only, NOT applied

`tools/DefaultBodyRewriter` (Mono.Cecil) replaces each listed method's body with a valid default-return body
(`ldloca` + `initobj` + `ldloc` + `ret`, `InitLocals`, `MaxStackSize = 8`; plain `ret` for `void`) — the same shape a
Cpp2IL dummy already has. Dry run on the current assembly:

```
requested 178, rewritten 178, missing 0
dry run: the assembly was not written (pass --apply to write it)
```

Assembly hash unchanged by the dry run (`8d14656a4ec78e511029...`). **No method has been stubbed.** Applying it needs the
user's decision, because HANDOFF forbids stubbing gameplay methods without authorisation; if approved, the sequence is
`--apply` -> fixer (still 125) -> one export -> re-scan must report 0.


## 21. #47 review outcome + the data-layer reality (2026-11-17T00:46Z)

Full review: `herdr-reports/review-47-rea-ipsw.md`.

Verified good: the resolver is read-only and deterministic, and every VA it produces lands in `__TEXT.il2cpp`
(`0x00370a280-0x0076af590` per `ipsw macho info`) — independent proof the pointer table yields code addresses. All §14
addresses, the §19 treat fix, the §20 12-error cap and the scanner's 179->178 movement were re-checked and hold. Peer
cross-checks (`native-crosscheck-51.md`, `racing-cup-crosscheck.md`) match; I reproduced t2m's
`Dictionary<SimRaceCarDriver,...>` vs `Dictionary<System.Object,...>::GetEnumerator()` defect in the dummy IL myself.

Hygiene findings:

- `ipsw macho disass` writes `UnityFramework.a2s` (17.9 MB) **into the evidence directory** (`ipa-extract/.../UnityFramework.framework/`).
  Documented behaviour (`tools/ipsw-skill/ipsw/SKILL.md:55`), but use `--cache <scratch>` from now on. The Mach-O itself is unmodified.
- `ipa-extract/.../Data/Managed/` holds 129 generated DLLs (16 Nov 19:34); `app.ipa` contains **no** managed DLLs, so these are
  output staged inside the evidence tree. No original was destroyed.
- HANDOFF §5 still demands `Reconstructed 124 methods`; the current required line is **125** (§16, §19).

### The blocker nobody had measured: the data layer is stubs

`LoadOutdoorAnimalSet` and `LoadOutdoorAnimalTreatSet` are dummies, and a Cecil scan finds **no `stsfld`** to
`_outdoorAnimalSet`/`_outdoorAnimalTreatSet` anywhere in the assembly — only the two repaired getters' `ldsfld`. Those two
getters therefore enumerate `null` and throw at runtime despite compiling.

Census of the whole type:

| measure | count |
| --- | --- |
| `DataHub` methods | 2,366 |
| Cpp2IL stub bodies | 2,321 |
| real bodies | 45 |

So the export queue (178 methods) blocks *compilation*, while 2,321 `DataHub` stubs block *gameplay*. Making IL2CPP succeed
will produce a build that runs but has no game data. This must be stated in any report claiming "playable".


## 22. Measured on a scratch copy: option 2 clears IL2CPP's visible 12, but the queue is far larger than 178 (2026-11-17T01:05Z)

All of this was measured on **scratch copies**; the project DLL
(`reconstructed/.../Assets/Plugins/Assembly-CSharp.dll`, `8d14656a…`) was never written, and the scratch copy of
`Library/Bee/artifacts/WebGL/ManagedStripped/Assembly-CSharp.dll` was restored afterwards (hash `22dce996…` verified
before and after every run). This is a measurement of what option 2 would do, not an application of it.

### Experiment A — rewrite only the 178 scanner-flagged methods

```
requested 178, rewritten 178, missing 0
PROBE EXIT=255   errors: 12
```

Every method in the previous failure list is gone. **The rewrite works.** But IL2CPP immediately reported 12 *different*
methods, and **none of the 12 were in the 178 list**:

```
ObjectDictionary::ConvertUkvp(UnityKeyValuePair`2<System.String,System.String>)
SimSpecialJobAstronaut::GetAstronautByName(System.String)
CanadaEmigrationCategory::ValidateIsCanada(EmigrateContext)
ExoticPetPurchaseCategory::ValidateIsExoticPet(PetPurchaseContext)
ScenarioFinancialAdvisorInvestmentAllocation/<>c::<InvestAvailableFunds>b__16_1(KeyValuePair`2<SimBond,System.Double>)
SimZoo/<>c::<GetMostPopularAnimal>b__228_0(KeyValuePair`2<SimZooAnimal,SimZooHabitat>)
DumbScenario::ChoiceSelectedAtIndex(System.Int32)
ScenarioFinancialAdvisorInvestmentAllocation::InvestAvailableFunds(System.Boolean)
ScenarioSportsTryOut::RefreshScenario()
ScenarioVacationFeedback::RefreshScenario()
SimHero::StartDatingLover(SimLover)
TranslationApi/<>c::<UnityInit>b__83_0(Backend.Authentication.GetConfigResponse)
```

So §15's "179 is the queue" is **wrong** — the scanner misses methods. Error subjects on that run:
`Nullable`1` ×5, `KeyValuePair`2` ×2, `Enumerator` ×1, `EmigrateContext` ×1, `PetPurchaseContext` ×1.

### Experiment B — rewrite the 1,195 methods flagged by the improved scanner

Scanner fixes made here (source: `tools/IlInvalidScan/Program.cs`):

1. `Nullable<T>` is **no longer excluded** — IL2CPP really does throw `Cannot get stack type for Nullable`1`, which is
   exactly the opposite of the old comment's assumption.
2. `ldarg`/`ldarg_S`/`Ldarg_0..3` and `ldsfld` are now inspected, not just `ldloc`/`ldfld` — struct **parameters** were
   invisible before.
3. Bug fixed: a `ParameterDefinition` operand is already in *parameter* space (this excluded), while a raw int / implicit
   opcode is in *argument-slot* space (this included). The old code subtracted 1 from both and therefore never saw an
   instance method's first parameter (`CanadaEmigrationCategory::ValidateIsCanada(EmigrateContext)` was missed by exactly
   this).
4. Look-back window widened from 3 to 4 instructions.

Result: **1,195** flagged (was 178). Rewriting all of them on the scratch copy:

```
requested 1195, rewritten 1195, missing 0
PROBE EXIT=255   errors: 12
```

Still 12, and the scanner catches only **4 of those 12** (it misses `ConvertUkvp`, `GridIndexForPosition(Vector3)`,
`DumbScenario::ChoiceSelectedAtIndex`, `AddFullWallAtCoordinate(Vector2Int)`, `SetAngle(Vector2,Vector2,Single)`,
`MiniGameCraps::PostRollFlow`, `MiniGameEarthDefense::Update`, `ScenarioSportsTryOut::RefreshScenario`).

### What this means

- The rewrite **works** — it clears whatever IL2CPP currently reports.
- The static scanner is **not a complete oracle**. True queue is at least ~1,200 and probably larger; the residual 12 show
  the heuristic still cannot see struct loads that happen more than 4 instructions before the consuming op, or that arrive
  through a call/field chain.
- **Option 2 is therefore not "one build".** It is an iterative loop with IL2CPP as the oracle:
  converter run → take the 12 reported names → rewrite exactly those → repeat until 0 errors → then one Unity export.
  A converter-only round costs ~2-3 minutes (no Unity build, no shader work), so ~100 rounds ≈ 4-5 hours if the queue is
  ~1,200, and it can run unattended.

That loop is mechanical and needs no human input per round, but it is still stubbing ~1,200+ gameplay methods, so it stays
behind the user's authorisation (HANDOFF forbids stubbing without it).


## 23. Profile-decimal pair repaired surgically (#54/#56) — fixer count 125 -> 127 (2026-11-17T01:20Z)

Unlike the treat method, these two did not need a full body recovery: the defect is **one instruction pair**, and the
original semantics are recoverable exactly.

```
ScenarioProfileGrandchild::RefreshScenario()   and   ScenarioProfileNieceNephew::RefreshScenario()
IL_21bc: ldloc V_209                       (string)
IL_21c2: call Decimal String::System.IConvertible.ToDecimal(IFormatProvider)
IL_21c7: stloc V_321                       (Decimal)
IL_21d7: ldloc V_321                       <-- Decimal pushed BY VALUE
IL_21db: ldc.i4 1
IL_21e0: clt                               <-- int32 < Decimal -> "Cannot get stack type for Decimal"
```

C# cannot emit `decimal < int` like that; the compiler emits the operator overload. So the faithful repair is
`Decimal.op_LessThan(d, Decimal.op_Implicit(1))`, which is what the fixer now emits:

```
IL_21d7: ldloc V_321
IL_21db: ldc.i4 1
IL_21e0: call System.Decimal System.Decimal::op_Implicit(System.Int32)
IL_21e5: call System.Boolean System.Decimal::op_LessThan(System.Decimal,System.Decimal)
IL_21ea: stloc V_322
```

Implementation: `tools/RecoveredMethodFixer/ProfileDecimalMethodRepairs.cs`. It walks the body for
`ldloc <Decimal local>; ldc.i4 <const>; clt|cgt|ceq` and rewrites the pair in place — nothing else in the 5,419- and
3,981-instruction bodies is touched, so this is a surgical fidelity fix, not a stub. It is idempotent (after the rewrite
the `clt` no longer exists). Dispatch added for both signatures; assertion raised 125 -> **127** (+2 distinct signatures).

Pitfall worth recording: the family helpers' `ImportMethod(module, type, name, arity, instance, …)` parameter is
**"is static"**: passing `true` selects *instance* methods. Passing `true` for `op_Implicit`/\`op_LessThan` threw
`Sequence contains no matching element` and aborted the fixer with exit 134 before writing anything (safe failure).

Verification before spending a build: fixer exits 0 with `Reconstructed 127 methods`, and `IlMethodDump` shows the
`op_Implicit` + `op_LessThan` pair at both sites. Build 43 is running with `EXPECTED_METHODS=127`.


## 24. Build 43 — profile-decimal pair cleared; two masked methods surfaced (2026-11-17T01:08Z)

`Reconstructed 127 methods` (exit 0). The failure list no longer contains
`ScenarioProfileGrandchild::RefreshScenario` or `ScenarioProfileNieceNephew::RefreshScenario` (0 mentions each in the log),
so the surgical `Decimal.op_LessThan(d, Decimal.op_Implicit(1))` rewrite was accepted by IL2CPP.

Two previously masked methods took their slots, as the 12-cap predicts:
`ScenarioFinancialAdvisorInvestmentAllocation::InvestAvailableFunds(System.Boolean)` and
`ScenarioSportsTryOut::RefreshScenario()`.

Remaining 12: bike-ride location, racing outcome feedback, the two eligibility checks, podcast `ValueTuple`2`, invest-funds,
prison-gift ctor, circuit/street racing refresh, school-activity choice, parent-marries refresh, sports try-out.

### School-activity is NOT surgical (deferred)

`ScenarioSchoolActivityJoin::ChoiceSelectedAtIndex` has a 7,972-instruction body and two decompiler artifacts of the shape
`ldc.i4.0; conv.i; ldloc <JoinSchoolActivityContext>; ceq` (IL_012e) and `ldloc <JoinOutdoor...>; ldc.i4 0; ceq` (IL_6c87).
There is no operator overload to restore intent from — the compared value is a decompiler confession, not a real C# comparison —
so this one needs native recovery (0x39e29d0), not a two-instruction rewrite.


### Surgical repairs do not scale — measured

The scanner now also classifies a defect as surgically repairable when the decompiler lost a comparison operator, i.e.
`load <struct>; ldc.i4 <const>; clt|cgt|ceq` and the struct defines the matching operator plus `op_Implicit(int32)` — the
exact shape the profile-decimal pair had. On the current assembly only **4 of 1,193** flagged methods match:

```
SURGICAL DataActorReal DataHub::GetRandomRealActor(System.String,System.Int32)
(+ the three others listed in il-dumps/il-invalid-scan-v4.txt)
```

So the two profile methods were the lucky case, not the pattern. There is **no bulk fidelity-preserving rewrite** available:
the remaining ~1,190 defects are decompiler confessions (`ldc.i4.0; conv.i` compared against a struct, unmanaged-memory
placeholders) whose original intent is not present in the IL at all. Recovering them means reading the native code per
method, or accepting default bodies.


## 25. Struct-comparison repair generalised — fixer count 127 -> 131 (2026-11-17T01:15Z)

The profile-decimal fix generalised cleanly. `tools/RecoveredMethodFixer/StructComparisonMethodRepairs.cs` replaces the
Decimal-only file and rewrites any `<load a value type>; ldc.i4 <const>; clt|cgt|ceq` whose type defines the matching
operator plus `op_Implicit(int32)`. It handles `ldloc`/`ldloc_S`/`ldloc_0..3`/`ldfld`/`ldsfld` as the producer, and is
idempotent.

Dispatched for the six methods the scanner proved have that shape (the scanner's v4 run reported exactly 4 still-unfixed
ones, the other two being the profile pair already repaired in build 43):

| method | native | repair |
| --- | --- | --- |
| `ScenarioProfileGrandchild::RefreshScenario` | — | `Decimal.op_LessThan(d, 1m)` (build 43 verified) |
| `ScenarioProfileNieceNephew::RefreshScenario` | — | same (build 43 verified) |
| `ScenarioProfileLover::RefreshScenario` | — | same |
| `ScenarioProfileSibling::RefreshScenario` | — | same |
| `DataHub::GetRandomRealActor(System.String,System.Int32)` | — | same |
| `VoltronManager::GetGenderSwapDataForData(System.String)` | — | same |

Arithmetic: 127 + 4 = **131**. Assertion raised to 131; fixer exits 0 with `Reconstructed 131 methods`.

All four new sites verified by `IlMethodDump` before any build was spent — each shows
`ldc.i4 1; call Decimal::op_Implicit(int32); call Decimal::op_LessThan(Decimal,Decimal)`. The four are currently *masked*
(not in IL2CPP's visible 12), so the next build cannot show them passing; they will simply not appear when the queue
advances to them.


## 22. #55 — the two blocked eligibility-handle methods are repaired (2026-11-17)

`ScenarioPetFind::ScenarioIsEligibleForRandomPush` (idx 26010, native VA **0x38E34A4**) and
`ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush` (idx 27912, native VA **0x3A7D0E0**) — both resolved via the
low-36 method-pointer table at file offset 0x959C438 and validated inside `__TEXT.il2cpp` (0x370A280–0x76AF590) — were
decoded natively and rebuilt as fresh Mono.Cecil bodies in `tools/RecoveredMethodFixer/EligibilityHandleMethodRepair.cs`.
Fixer now asserts **133** methods (`Reconstructed 133 methods`, exit 0); compile-check passed and the repaired IL was
re-dumped and verified. Full native decode, inference list (typeof(T).Name argument, unnamed SimPerson float @0xA0 gate
omitted), and merge-order note for desktop: `herdr-reports/eligibility-handle-55.md`. The remaining 21
`ScenarioIsEligibleForRandomPush` methods in `il-dumps/option2-methods-v2.txt` stay in the option-2 queue.

## 26. Racing-cup family (#66): GetNPCWinningDriver recovered; two RefreshScenario bodies deferred (2026-11-17T01:45Z)

### Done — `ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)`, native `0x3967d78`

Recovered in `tools/RecoveredMethodFixer/RacingCupMethodRepairs.cs`, dispatched, fixer count raised to **134**.

Native reading (matches `racing-cup-crosscheck.md` and the struct layout: `Placement` 0x0, `Score` 0x4,
`competitorResults` 0x8, `activeRace` 0x10):

```csharp
SimRaceCarDriver result = null;
var dict = results.competitorResults;          // ldr x0,[x19,#0x8]
if (dict == null) throw NullReferenceException; // callvirt GetEnumerator gives the same NRE
foreach (var pair in dict)                      // GetEnumerator/MoveNext/Dispose(0x5f64b5c)
{
    if (pair.Value.Placement == 1)              // ldr w8,[sp,#0x28]; cmp w8,#1  (sp+0x28 = first int of Value)
    {
        result = pair.Key;                      // ldr x19,[sp,#0x20]
        break;
    }
}
return result;
```

Emitted IL verified by dump: `ldarga.s results; ldfld competitorResults`, then
`callvirt Dictionary<SimRaceCarDriver,RacingCupSeriesResults>::GetEnumerator()` — **one consistent generic
instantiation**, which is exactly the defect the decompiler had (it called `Dictionary<System.Object,...>::GetEnumerator()`).
Enumerator used only via `ldloca`, `finally` Dispose, `leave` on break. Fixer exits 0 with `Reconstructed 134 methods`.

### Deferred — `ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()` (0x3960e7c) and
`...StreetRaceResults::RefreshScenario()` (0x396afec)

Both are ~950-instruction UI-text builders, not surgical cases. The invalid sites are:

| method | site | dummy IL | what it must be |
| --- | --- | --- | --- |
| Circuit | IL_008d | `ldfld _results; ldc.i4 0; ceq` | missing field load |
| Circuit | IL_021e | `ldfld _results; call SimRacing::GetPlacementString(System.Int32)` | receiver is wrong |
| Circuit | IL_074a | `ldfld _results; ldc.i4 1; ceq` | missing field load |
| Street | IL_01d7 | `ldfld _results; ldc.i4 0; ceq` | missing field load |
| Street | IL_0823 | `ldfld _results; ldc.i4 1; ceq` | missing field load |

Native evidence gathered for the placement call (circuit, `0x03961098`):

```
ldr w0, [x19, #0x178]      ; argument is this.<int field at 0x178> -- NOT _results.Placement
mov x1, #0
bl  0x3f9fd38              ; SimRacing::GetPlacementString(int)
```

So the decompiler did not merely drop a field on `_results`: it substituted the wrong receiver entirely. `0x178` is not a
field of `ScenarioRacingCupSeriesCircuitRaceResults` (its fields are `_activeRace`, `_cup`, `_results`), so it belongs to a
base class. Neither the metadata (`offset: None` on every field) nor the managed DLL exposes field offsets, so mapping
`0x178` to a name needs more work than this session allowed. The rest of each body also needs its string literals decoded
from the Mach-O to reproduce exact UI text.

Conclusion for the family: **1 of 3 recovered and verified at the IL level; the two RefreshScenario methods need a
dedicated pass** — they are not stub-shaped defects, and guessing the receiver would silently produce wrong placement text.

### Concurrency note

While this ran, OpenCode added `EligibilityHandleMethodRepair.cs` (12:36) with dispatch for
`ScenarioPetFind::ScenarioIsEligibleForRandomPush` and `ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush` — those were
in my #51 scope; they are now theirs and I will cross-check rather than duplicate. The count went 131 -> 133 (their two) ->
**134** (my racing method). Anyone editing `Program.cs`'s assertion must re-read it first; a stale `131` anchor already
cost one failed edit.


## 27. Racing-cup method family recovered — fixer count 134 → 137 (task #68, 2026-11-17)

Arithmetic: 134 (post-eligibility baseline this session) + street + circuit + demolition `RefreshScenario`
= **137**. Dispatch cases and the assertion were raised in step with each landing; the fixer exits 0 with
`Reconstructed 137 methods`. All four bodies were re-dumped with `IlMethodDump` and verified instruction by
instruction against ARM64; `IlInvalidScan` reports **0 stack-height-imbalanced joins** assembly-wide and the
`ScenarioRacingCupSeries*` methods have left the stack-type-invalid queue.

| method | native | repair |
| --- | --- | --- |
| `SimRaceCarDriver ScenarioRacingCupSeriesOutcomeFeedback::GetNPCWinningDriver(RacingCupSeriesResults)` | `0x3967d78` | `RacingCupMethodRepairs.FixGetNpcWinningDriver` (earlier; independently re-verified in review #66 — no mismatch) |
| `System.Void ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()` | `0x396afec` | `RacingCupScenarioMethodRepairs.FixStreetRefreshScenario` |
| `System.Void ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()` | `0x3960e7c` | `RacingCupScenarioMethodRepairs.FixCircuitRefreshScenario` |
| `System.Void ScenarioRacingCupSeriesDemolitionRaceResults::RefreshScenario()` | `0x3964494` | `RacingCupScenarioMethodRepairs.FixDemolitionRefreshScenario` (bonus: was in the option-2 scanner queue, not the visible 12) |

Circuit and demolition are the same native program with two literals changed ("race" → "derby") and are
recovered by one parameterized builder. Both notes recorded in the source: circuit ends at the ret at
`0x39612d0` (demolition `0x39648d4`); the enumerator loops the earlier cross-check attributed to
`RefreshScenario` actually live in the class helpers `CreateTableOrder`/`CreatePointTotalDictionary`.

### Literal decoding — reusable discovery

String literals for IL2CPP v39 are NOT reachable by reading Mach-O bytes. The reliable chain, now proven
on 20+ literals (all racing-cup texts decoded exactly: "Cup Series", "Race Results"/"Cup results",
"You won the race on/derby at", "You lost the race on ", " against your "/" to your ", "rival"/"fellow",
" racer, ", "Continue to the next race", "Attend the trophy ceremony", "Vehicle Condition", …):

1. Each `adrp/add` pair in `__DATA` addresses a **usage slot** holding an encoded token.
2. Token layout (read from the lazy initializer at `0x1f3b730`): kind = token bits 29-31
   (1=TypeInfo, 2=Il2CppType, 3=MethodDef, 4=FieldInfo, 5=StringLiteral, 6=MethodRef), **index = bits 1-28**
   (bit 0 is a runtime flag). The naive `index = low 29 bits` reading is wrong by ~2x and yields plausible
   but INCORRECT strings — the earlier OpenCode session was misled by exactly this.
3. For kind 5, index into `global-metadata.dat`'s `stringLiterals` section (offset 380, 71,700 × uint32
   offsets into `stringLiteralData`; strings NUL-terminated). Parse `extracted/il2cpp-full.json`'s
   `sections[]` for the offsets rather than assuming the header layout.

Helper: `tools/va_to_method.py <VA>…` (reverse of `tools/method_address.py`, same low-36 table).
Field offsets: `tools/FieldOffsetDump` (Cecil, il2cpp layout; note its alignment bug — it aligns to field
size, so verify against native `ldfld` offsets before trusting; every field used in the racing-cup repairs
was cross-checked against the native loads).

### Layout facts established (calibrated against native offsets)

- `RacingCupSeriesResults` (struct, 24 B): Placement@0x0, Score@0x4, competitorResults@0x8, activeRace@0x10.
- `RacingCupSeriesActiveRace`: raceCupRegistration (struct, 32 B)@0x10, _raceResults@0x30,
  _trackIndex@0x38, _trackCompleted@0x3c, _pivotalMomentScore@0x40.
- `RacingCupSeriesRegistration` (struct): simRacing@0x0, simRacingCup@0x8, simRaceCar@0x10, simOpponents@0x18.
- Scenario-derived classes: `_results`@0x178, `_activeRace`@0x190, `_cup`@0x198.
- `RacingCupSeriesActiveRace::get_ActiveTrack` (0x3952ed4) = null-safe `reg.simRacingCup.Tracks[_trackIndex]`;
  `HasAdditionalTracks` (0x395301c) = `cup != null && Tracks != null && 0 <= _trackIndex < Tracks.Count - 1`.
- Generic `List<T>::get_Item` shared thunk: `0x633572c` (takes list, index, methodinfo).
- `0x6834748` = `String.Concat(s,s,s)`, `0x68350e0` = `String.Concat(string[])`, `0x6823894` = `String.Concat(s,s)`.

### Remaining racing-cup family (scanner-flagged, not yet recovered)

`DataHub::GetRacingCupSeriesWinnings(String)`, `RacingCupSeriesActiveRace::GetPlayerTotalTime/GetRacerTotalPoints/
GetRacerTotalTime/GetCurrentPlacement/ConvertTrackResults`, `RacingCupSeriesRaceResultsFactory::CreateScenario`,
`SimRacingCup::.ctor(String,RACING_LICENSE,RACING_TRACK_CATEGORY,List<SimRacingTrack>)` — the class helpers
(`CreateTableOrder`, `CreatePointTotalDictionary`) also still carry dummy bodies but are not in the IL2CPP
failure queue. No racing-cup native semantics were undetermined.

### Related task status

- #62 compliance review: `herdr-reports/review-62-compliance.md` — PASS (one new family file; Program.cs
  touched only for the mandated dispatch + count; compile-only build clean).
- #66 racing-cup review: `herdr-reports/review-66-racing-cup.md` — APPROVE, no mismatch.
- #50 eligibility-handle: complete (re-verified this session).
- #56: profile-decimal pair done (build 43); **school-activity `ChoiceSelectedAtIndex` (0x39e29d0,
  7,972-instruction dummy body) remains** — owned, not yet recovered.

## 28. Task parked by the user — resume from `herdr-reports/UNRESOLVED-TASKS.md` (2026-11-17 ~14:00)

Five `RacingCupSeriesActiveRace` methods were additionally recovered (source `RacingCupActiveRaceMethodRepairs.cs`,
dispatched, fixer count raised 137 → **142**): `GetPlayerTotalPoints` (0x3953eb8), `GetPlayerTotalTime`
(0x3953ffc, `?? TimeSpan.Zero` accumulation), `GetRacerTotalPoints` (0x3954214, TryGetValue sum),
`GetRacerTotalTime` (0x39543bc), `GetCurrentPlacement` (0x3964624? — correction: 0x3954624, points-then-time
ranking over `reg.simOpponents`). Copy-verified: `Reconstructed 142 methods`, exit 0, `IlInvalidScan`
0 imbalanced joins, value-type queue 1185 → 1180. The shared DLL was NOT rewritten (desktop bisecting);
t1h's 13:18 generic fix and these five are in source only. All remaining work, resume order, layouts,
thunk identities and the copy-verification protocol are recorded in `herdr-reports/UNRESOLVED-TASKS.md`.
