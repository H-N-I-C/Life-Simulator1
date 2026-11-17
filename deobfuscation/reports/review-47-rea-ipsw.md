# Review #47 — REA and ipsw-skill usage for the deobfuscation task

Reviewer: DeepSeek Harness CLI [t1h] · 2026-11-17 · Review only, no duplication of the main deobfuscation run.
Everything below was read from disk; the only writes were my own scratch files (`/tmp/fieldaudit`, this report).

## 1. What was reviewed

| Item | Path | Role |
| --- | --- | --- |
| Native evidence | `ipa-extract/Payload/BitLife.app/Frameworks/UnityFramework.framework/UnityFramework` | 169,308,640 B, mtime 4 Nov 11:02 — untouched |
| Metadata evidence | `extracted/il2cpp-full.json` | 27,772,992 B, mtime 16 Nov 18:21 — untouched |
| Resolver | `tools/method_address.py` | method index → native VA |
| Disassembler | `ipsw` 3.1.737 (Homebrew) via `tools/ipsw-skill` | `macho disass --vaddr … -c N` |
| CLI's reported outputs | HANDOFF §14, §16, §19, §20; `il-dumps/native/*` | cross-checked below |
| Peer cross-checks | `herdr-reports/native-crosscheck-51.md`, `herdr-reports/racing-cup-crosscheck.md` | compared against the same evidence |

## 2. Read-only safety

**Resolver — clean.** `method_address.py` opens `il2cpp-full.json` with default (read) mode and the Mach-O with `"rb"`;
it only `seek`s the pointer table. No write path exists in the file.

**Metadata — clean.** `extracted/il2cpp-full.json` still carries its 16 Nov 18:21 mtime; nothing in this work rewrote it.

**ipsw — one real side effect.** `ipsw macho disass` builds an address→symbol cache next to the binary:

```
ipa-extract/Payload/BitLife.app/Frameworks/UnityFramework.framework/UnityFramework.a2s
17,903,157 B, mtime 17 Nov 11:25
```

That file sits **inside the evidence tree**. The skill documents this (`tools/ipsw-skill/ipsw/SKILL.md:55`: "First
`dyld disass`/`xref` … builds `<DSC>.a2s`"), so it is expected behaviour rather than misuse — but HANDOFF §4 requires the
native evidence tree to remain intact, and a 17.9 MB cache appearing beside the pristine binary is a mutation.
`ipsw macho disass --help` offers the fix: `--cache <path>` (and `--dec-nocache`). Recommendation: point `--cache` at
`il-dumps/native/` or `/tmp`, and keep `ipa-extract/` byte-identical. The Mach-O itself was not modified (mtime and size
unchanged), and the cache is regenerable, so no evidence was lost.

**ipa-extract/Data/Managed — generated output, not originals.** 129 files there carry mtime **16 Nov 19:34**, i.e. after the
extraction. Cross-checked against the archive: `app.ipa` contains only

```
Payload/BitLife.app/Data/Managed/Metadata/global-metadata.dat
Payload/BitLife.app/Data/Managed/Resources/*.dll-resources.dat   (7 files)
Payload/BitLife.app/Data/Managed/mono/4.0/machine.config
```

— **no managed DLLs at all**. So those 129 DLLs are reconstruction/deobfuscation output staged into the extracted app tree.
No original was overwritten (the IPA still holds everything), but the evidence tree and generated output are now mixed in one
directory. Recommendation: move them to `recovered/` so a future re-extract cannot silently consume stale output.

**Other note:** `extracted/xor-key.txt` (23 B) stores the text-asset XOR key inside the evidence directory — correct for
reproducibility, but treat that directory as sensitive.

## 3. Resolver validation (independent of the CLI's claim)

- Deterministic: repeated runs give `GetRandomOutdoorAnimalTreatForDangerScore` → idx 12633, raw `0x0010000003d2f06c`,
  low36 `0x3d2f06c`; `GetRandomOutdoorAnimal` → idx 12629, raw `0x0010000003d2e73c`, low36 `0x3d2e73c`.
- The high bits (`0x10000`) are the chained-pointer tag the low-36-bit rule discards; the values are consistent across
  both methods, which is what a valid tag looks like.
- **Segment check (new evidence, not in the handoff):** `ipsw macho info` shows
  `__TEXT.il2cpp` at addr `0x00370a280–0x0076af590`. Every VA the resolver produced falls inside it:
  `0x38e34a4`, `0x3a7d0e0`, `0x3922b40`, `0x3920c4c`, `0x3a1c23c`, `0x3967d78`, `0x3960e7c`, `0x396afec`, `0x3d2f06c`,
  `0x3d2e73c`, `0x3a81478`. That is an independent confirmation the table yields *code* addresses, not data.
- Also confirmed: for these segments file offset == VM address, so `adrp/add`-computed data addresses can be read straight
  from the file — the assumption the racing-cup cross-check relies on.

## 4. Cross-checks of the CLI's reported outputs

| Claim | Independent check | Result |
| --- | --- | --- |
| Racing trio VAs (§14) | re-derived in `racing-cup-crosscheck.md` from metadata + resolver | **match** |
| `GetNPCWinningDriver` generic-identity defect | dumped the dummy IL myself | **reproduced**: `ldfld Dictionary`2<SimRaceCarDriver,RacingCupSeriesResults>` then `call Dictionary`2<System.Object,RacingCupSeriesResults>::GetEnumerator()`, plus `ceq;ceq` double negation and `NoteDecompilerIssue` calls — exactly as the peer report states |
| Treat filter offsets `+0x18`/`+0x1c` | metadata field order for `DataOutdoorAnimalTreat` is `<String>`, `<AnimalDangerScore>`, `<CommonnessScore>`; with IL2CPP's 0x10 object header that is 0x10/0x18/0x1c | **consistent** with the native loads and both reports |
| `ScenarioPrisonReceiveGift::.ctor` disambiguation (§14) | disassembled both | `0x3922b40` uses only `x0` (**Parameters=0**, the failing one); `0x3920c4c` consumes `x1` | **§14 is correct** |
| Treat fix in build 42 (§19) | build-42 log has no treat error; IL dump shows `ldarg.0` in a `Static=True, Parameters=1` method | **confirmed** |
| "12-error cap is IL2CPP's own" (§20) | `/tmp/il2cpp-probe.log` from the direct converter run | **confirmed**: exit 255, exactly 12 `^Error: IL2CPP error for method` lines |
| Scanner tracks IL2CPP (§20) | re-ran `tools/IlInvalidScan` | **confirmed**: 179 → 178 after the treat fix |

## 5. Discrepancies (in severity order)

### 5.1 The recovered getters read a field nothing ever writes — build success ≠ working behaviour

`LoadOutdoorAnimalTreatSet` and `LoadOutdoorAnimalSet` are **Cpp2IL stubs** (their bodies are confessions like
`ldc.i4 163442688`, `ldstr "a16_dealer-hero-threatens-rival-cartel-member-enc"`, `"Unmanaged memory load: [9BDFD5C]"`), and
they are not among the 125 repaired methods. A Cecil audit of every instruction in `Assembly-CSharp.dll` finds **no
`stsfld` to either field** — the only references are the two repaired getters' `ldsfld`:

```
Ldsfld   _outdoorAnimalSet        in Game.DataOutdoorAnimal DataHub::GetRandomOutdoorAnimal(System.String)
Ldsfld   _outdoorAnimalTreatSet   in Game.DataOutdoorAnimalTreat DataHub::GetRandomOutdoorAnimalTreatForDangerScore(System.Int32)
```

So both repaired methods will call `GetEnumerator` on **null** and throw `NullReferenceException` at runtime. Their IL is
faithful to the native control flow, but their data source is never populated in this reconstruction. HANDOFF §5 says
runtime behaviour "remains unverified until browser testing" — this is stronger than unverified: it is provably broken
until the loaders are recovered or the field is populated. Recommend recovering `LoadOutdoorAnimalSet` /
`LoadOutdoorAnimalTreatSet` before claiming these two methods work.

### 5.2 The treat body dropped the native null guard

The emitted IL calls `LoadOutdoorAnimalTreatSet()` unconditionally, while native does `if (list == null) call loader`
(cross-check step 1: "loads a static list … if null calls the static field initializer at `0x3d2eccc`"). Build 41 removed
the guard to mirror the passing sibling, which made IL2CPP happy but is a fidelity deviation. It is currently harmless
**only because** the loader is a stub (5.1); once the loader is real, an unconditional call could rebuild the list on every
invocation. Restore the `ldsfld; brtrue` guard when the loader is recovered.

### 5.3 HANDOFF §5 is stale

§5 still says the required success line is `Reconstructed 124 methods`; §16 raised the assertion to 125 and §19 confirms
125. Anyone following §5 verbatim would treat a correct run as a failure. Update §5 to 125.

### 5.4 Documentation gap for the ipsw cache

Nothing in the handoff warns that `ipsw macho disass` writes `.a2s` into the evidence directory. Add the `--cache`
convention to §4/§6 so the next agent does not mutate `ipa-extract/` by accident.

## 6. Verdict

The REA/ipsw approach itself is appropriate and the commands are correct: metadata-driven index → chained pointer low-36
→ `__TEXT.il2cpp` disassembly is sound, deterministic, and independently reproduced. Read-only safety holds for the
resolver and the metadata; the two exceptions are the ipsw `.a2s` cache inside the evidence tree (fixable with
`--cache`) and 129 generated DLLs staged under `ipa-extract/Data/Managed` (fixable by moving them to `recovered/`).
All of the CLI's reported addresses, counts and defect readings check out against independent derivation; the one
substantive gap is 5.1 — two "recovered" methods whose backing collection is never populated.

## 7. Stub coverage in the data layer (found while verifying 5.1)

Following 5.1 I measured how much of `DataHub` has a real body, by dumping every `DataHub::` method from
`Assembly-CSharp.dll` and counting bodies containing `Cpp2ILHelpers.NoteDecompilerIssue`:

| measure | count |
| --- | --- |
| `DataHub` methods | 2,366 |
| stubs (Cpp2IL dummy bodies) | 2,321 |
| real bodies | 45 |

**98% of the game's data hub is a decompiler placeholder.** The 45 real bodies are essentially the methods the fixer has
reconstructed by hand. This is not an export problem — it is a runtime-data problem: no loader populates any data table, so
even if IL2CPP succeeds the game has nothing to play with.

Consequence for planning: the 178-method queue blocks *compilation*; the 2,321-method `DataHub` stub set blocks *gameplay*.
Both would have to be solved for the mission's "browser-verify gameplay" gate, and the second is far larger than the first.

