# Eligibility-handle method family — #55

Agent: OpenCode [t2m] · 2026-11-17 · Workspace: this task root.
Scope: the two `ScenarioIsEligibleForRandomPush` methods that block the WebGL build (build-42/43 failure list). The other 21 eligibility methods in `il-dumps/option2-methods-v2.txt` remain in the option-2 queue (t1h's pipeline) — noted, not touched.

## 1. Methods, addresses, resolver validation

| Method | Metadata idx | Native VA (low36) | Validation |
|---|---|---|---|
| `ScenarioPetFind::ScenarioIsEligibleForRandomPush()` | 26010 | `0x38e34a4` | `raw=0x00100000038e34a4` from table @ file offset `0x959c438`; inside `__TEXT.il2cpp` 0x370a280–0x76af590; disassembles to a valid ARM64 prologue (`sub sp, sp, #0x70` + callee-saved stores) |
| `ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush()` | 27912 | `0x3a7d0e0` | same table; inside `__TEXT.il2cpp`; 438 instructions disassembled |

Resolver: `python3 tools/method_address.py ScenarioIsEligibleForRandomPush` (deterministic; cross-checked earlier via idx 12629 → 0x3d2e73c and `__TEXT.il2cpp` containment). ipsw runs used `--cache /tmp/t2m.a2s` (evidence tree untouched; per #47 review).

## 2. Native semantics (decoded this task)

**Static chain (both methods):** usage slot `0x96ce080` holds token `0x2000c3d3` = kind TypeInfo (1), index 50131 → the GameManager class; `klass+0xB8` = static-fields pointer; static field `+0x0` = `public static GameManager Inst` (stub offset 0x0). Singleton instance fields `+0x20/+0x28/+0x30` hold the Life / hero / SimPast references. Named equivalents used in the repairs: `GameManager.Inst.Life`, then `Life.<Hero>k__BackingField` (0x88), `Life.<Past>k__BackingField` (0x98), `Life.<Finances>k__BackingField` (0xE8 — the exact native offset used for the `EffectiveBankBalance` receiver).

**PetFind (0x38e34a4–0x38e382c):** `Life::get_PetArray` (0x47def50) → ArrayList foreach (interface dispatch; runtime helper `il2cpp_vm_object_is_inst` = 0x1eca624) → `isinst` against class slot `0x96d9430` → sum `item+0x28` = `SimPet.<Alive>` → gates: hero `Age > 17` (`cmp w9, #0x11`), `Life::PresentedCountForScenario` (0x47f42a0) `<= 2` (`cmp w0, #2; b.gt fail`), `alivePets < SimHero::MaxNumberOfPetsPermissable` (0x3f0cb2c), `SimFinances::EffectiveBankBalance` (0x3ef6e54) `> 0.0` (`fcmp d0, #0`), return `!SimHero::CurrentlyInPrison` (0x3ef9c7c, `eor w0, w0, #1`). The string argument is produced natively as `typeof(T).Name` (Il2CppType usage slot `0x96bb008` → type object → virtual `get_Name` via vtable slot 0x208). The calls to `SimPet::get_PetType`/`DataPetType::AnimalString` at 0x38e3938/0x38e394c belong to the adjacent `ScenarioPetFind(bool)` ctor (stores `_happenedOnWalk`+0x178 / `_petCandidate`+0x180), not the eligibility method.

**SexualityDeclare (0x3a7d0e0–0x3a7d4dc):** `SimPast::get_ExLoverArray` (0x3f103b8, receiver `[Inst+0x30]`) → foreach; per item: cast to class slot `0x96d91f0` then `matched |= (item.<Gender@0x1C> == hero.<Gender@0x1C>)`; loop sets `w23=5` on normal exit (the `w23 ∈ {0,5}` gate at 0x3a7d398 therefore always continues on the normal path); gates: hero `Age > 11` (`cmp #0xB`), `Sexuality(0x24) == 0`, an unnamed float `hero+0xA0 >= 15.0` ORed with `matched`, `LgbtqGenderIdentity(0x44) == 0`, `PresentedCountForScenario(typeof(T).Name) <= 2`; final `return Life::YearsSinceScenario(0x47f48c4)(typeof(T).ToString()) > 2` — the second string uses a different vtable slot (0x168), matching the dummy's `MemberInfo::get_Name()` vs `Type::ToString()` split. The `SimPodcastActivity::LogToConsole` hit (0x3a7d6fc) is the adjacent next method, like PetFind's ctor neighbour.

## 3. Repairs (`tools/RecoveredMethodFixer/EligibilityHandleMethodRepair.cs`)

Fresh bodies from named metadata refs (style of `CultMethodRepairs.FixRelationshipEligibility`), helpers copied per the established per-file pattern. Dispatch cases added in `Program.cs`; count assertion 131 → **133** (baseline had moved 125 → 131 via t1h's four post-build-43 additions; build-43 log says 127, Program.cs 131 pre-dates build 44).

**Documented inferences / omissions (review points):**
1. `typeof(T)` for both string productions is taken as the scenario's own type — the only self-consistent reading of a `PresentedCountForScenario(name)` gate; the Il2CppType usage slots (`0x96bb008`, `0x96bb200`) cannot be mapped to a typedef without the binary's metadata-registration tables.
2. PetFind's `isinst` target = `SimPet` (class slot `0x96d9430` unnamed; only type-consistent reading).
3. Sexuality's loop cast target = `SimPerson` (slot `0x96d91f0` unnamed; compared field is `SimPerson.<Gender>`).
4. Sexuality's unnamed-float gate (`hero+0xA0 >= 15.0`, ORed with `matched`) and the initial `[Life+0xD8]` gender comparison are **omitted** (fields unnamed in the DiffableCs stubs; SimClassmate fields start at 0x1F0 so 0xA0 is inside SimPerson's unnamed region). Omitted rather than guessed.
5. Enumerator Dispose-in-finally (present in native) is not rendered — the Cpp2IL dummy renders no Dispose either; the loop uses `IEnumerator::MoveNext/get_Current` interface calls.

## 4. Compile-check

- `dotnet build tools/RecoveredMethodFixer` → 0 errors (5 pre-existing warnings).
- Full fixer run → **`Reconstructed 133 methods`**, exit 0.
- Repaired IL re-dumped with `tools/IlMethodDump`: both methods show the intended bodies (locals `Life/SimHero/SimFinances/...`; chain `ldsfld GameManager::Inst → ldfld Life → ...`; `ldtoken ScenarioPetFind → Type::GetTypeFromHandle → MemberInfo::get_Name → Life::PresentedCountForScenario`; final `cgt; ret` for SexualityDeclare).

## 5. Merge order (for desktop)

The fixer now writes 133 methods into `reconstructed/BitLife-Unity-dll/ExportedProject/Assets/Plugins/Assembly-CSharp.dll` (already applied by the compile-check run). Desktop should build IL2CPP from this state (expect the two methods gone from the failure list, per the 12-error-cap behaviour); t1h's four staged post-131 repairs ride along in the same assembly. If desktop wants the pre-#55 state instead, the fixer dispatch entries are two isolated `case` blocks + the count constant — trivially revertible.

Related: `herdr-reports/deobfuscation-toolchain-47.md` (toolchain + §6 addendum), `herdr-reports/racing-cup-crosscheck.md` (#68), `herdr-reports/HANDOFF.md` §19–21.
