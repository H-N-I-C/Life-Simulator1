# Native cross-check note — #51 methods

Independent disassembly by the #51 lead, written to compare against the owner's
hand-written IL. Native binary and metadata were read only.

Resolver: `python3 tools/method_address.py <MethodName>` (validated — see HANDOFF §14).
Disassembler: `ipsw macho disass <UnityFramework> --vaddr 0x... -c N`.

## Resolved addresses

| Method | VA |
| --- | --- |
| `ScenarioPetFind::ScenarioIsEligibleForRandomPush()` | `0x38e34a4` |
| `ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush()` | `0x3a7d0e0` |
| `ScenarioPrisonReceiveGift::.ctor()` | `0x3922b40` / `0x3920c4c` (pick the `Parameters=0` one) |
| `ScenarioScriptEventParentMarriesStepparent::RefreshScenario()` | `0x3a1c23c` |
| `DataHub::GetRandomOutdoorAnimalTreatForDangerScore(System.Int32)` | `0x3d2f06c` |

## `DataHub::GetRandomOutdoorAnimalTreatForDangerScore(int score)` — observed shape

Read from `0x3d2f06c`. Prologue and a standard IL2CPP type-init block, then:

1. `0x3d2f150`: loads a **static list** via `[class+0xb8] + 0x1958`; if null calls the
   static field initializer at `0x3d2eccc`. This is the same lazily-initialised data
   collection pattern as `DataHub::GetRandomOutdoorAnimal` (`0x3d2e73c`).
2. `0x3d2f16c`: `ldr x0,[x19]; bl 0x1eca748` allocates the result list, then
   `bl 0x6335088` with a second static argument — result list construction/registration.
3. `0x3d2f19c`: reloads the static list, bails to `0x3d2f34c` when null.
4. `0x3d2f1b4`–`0x3d2f1d4`: `bl 0x63369f0` materialises a **List<T>.Enumerator**
   (16–24 byte struct) into `sp+0x20`/`sp+0x30`, then `stp xzr, x8, [sp, #0x8]` primes the
   MoveNext state. Enumerator is used strictly by address (`add x0, sp, #0x20`) — which is
   exactly why the placeholder IL's by-value `Enumerator` push fails IL2CPP.
5. Loop head `0x3d2f1e8`: `bl 0x5f0089c` = `MoveNext()`; `tbz w0,#0` exits to `0x3d2f2d0`.
6. Per element (`x21 = [sp+0x30]`, null-checked → `0x3d2f344`):
   - `ldr w8,[x21,#0x1c]`; `cmp w8,#1`; `b.lt` → skip element (a positive-weight guard).
   - `ldr w9,[x21,#0x18]`; `cmp w9,w20` (w20 = `score` argument); `b.gt` → skip element.
   So the filter is **`field+0x1c >= 1` and `field+0x18 <= score`**.
7. `0x3d2f218`–`0x3d2f244`: inner accumulation into the new list — `str x21,[x9,#0x20]` with
   `str w9,[x19,#0x18]` updating the list's size field, i.e. `list.Add(element)` inlined.
8. Weighted pick happens after `0x3d2f2d0` (not yet transcribed here).

**Cross-check expectations for the owner's IL:** an `Enumerator` local used with
`ldloca`+`call` only; a `try/finally` around the enumeration; filter order
(weight guard before the `score` comparison — both are `continue`, order is observable only
through side effects, but keep native order); offsets `+0x18` and `+0x1c` on
`DataOutdoorAnimalTreat` must map to the real field names from
`extracted/il2cpp-full.json`, not to guessed ones.

## Remaining work in this note

Steps 8 onward for the outdoor method, and the full control flow for the other four
methods, are still to be transcribed here once the owner's files land.
