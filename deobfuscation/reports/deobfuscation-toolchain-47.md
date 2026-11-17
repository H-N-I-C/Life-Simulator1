# Deobfuscation toolchain record — REA + ipsw (#47)

Agent: OpenCode [t2m] · 2026-11-17 · Target: BitLife 3.26.1 (iOS IPA, Unity IL2CPP) in this task workspace.
Metadata/binary contents untouched — **but see the addendum: `ipsw macho disass` writes a symbol cache beside the
binary, so the ipsw steps were not byte-level read-only on the bundle directory.** Future runs must pass
`--cache /tmp/t2m.a2s` (or `--dec-nocache`).

## 1. Toolchain inventory

| Tool | Version / location | Role in this recovery |
|---|---|---|
| **REA** (`github.com/morluto/rea`) | `rea@6.7.0` at `~/.nvm/versions/node/v22.22.0/bin/rea` | Managed/il2cpp metadata inventory → `extracted/il2cpp-full.json` |
| **ipsw** (`github.com/blacktop/ipsw`) | `3.1.737` (Homebrew) at `/opt/homebrew/bin/ipsw` + skill bundle `tools/ipsw-skill/` | Mach-O inspection, segment mapping, disassembly of UnityFramework |
| Companions (already in workspace) | UnityPy (TextAssets), `repo/deobfuscation/scripts/` (XOR/NRBF decoders), `tools/method_address.py`, Mono.Cecil fixer | Asset decryption, VA resolution, IL reconstruction |

Not used, by design: Cpp2IL / AssetRipper decompilation as *evidence* (their output is what the fixer repairs — the corrupted dummy IL documented in HANDOFF §3 and my #68 report).

## 2. Deobfuscation steps (as executed on this target)

1. **IPA unpack** → `ipa-extract/Payload/BitLife.app/` (evidence, untouched).
2. **REA — IL2CPP metadata inventory**: operation `inspect_il2cpp_metadata` (provider `il2cpp-metadata`, "REA IL2CPP global-metadata reader" v1) on
   `ipa-extract/Payload/BitLife.app/Data/Managed/Metadata/global-metadata.dat`
   → **`extracted/il2cpp-full.json`** (Evidence JSON, `evidence_id ev_a820ffb47c06…`, subject sha256 `6a691594…`).
   Decodes metadata **version 39**: 22,642 typeDefinitions, 155,573 methods, 117,369 fields, 71,699 string literals,
   plus the full global-metadata section map (stringLiterals @0x17c, methods @0x63399c, typeDefinitions @0xf8e71c, …).
3. **ipsw — native side**: `ipsw macho info` (64-bit Mach-O DYLIB, iOS 15+, SDK 26.2; `__TEXT` 0x0–0x8bac000,
   `__DATA_CONST` 0x8bac000–0x90a8000, `__DATA` 0x90a8000–0x9ea4000 — VM addr == file offset) and
   `ipsw macho disass --vaddr …` for native method bodies.
4. **Bridge** between the two: the IL2CPP method-pointer table at Mach-O file offset `0x959c438`
   (8-byte chained pointers, low 36 bits = VA), driven by `tools/method_address.py` using the REA metadata indices.
5. **Asset layer**: UnityPy TextAsset extraction (`extracted/textassets/`, 1,087 entries) + the workspace's
   XOR-over-UTF-16 and NRBF decoders for the obfuscated game data files (independently re-verified in #49).

## 3. Tool outputs captured this session (re-runs, read-only)

- `rea --version` → `rea@6.7.0`; `rea doctor` → client registrations missing (cosmetic; CLI works).
- `rea inspect …/global-metadata.dat` → **`target_unavailable` / "unsupported binary format"**: the current REA
  build no longer exposes the `inspect_il2cpp_metadata` operation through `rea inspect` (tool drift since the
  Nov 16 run). **Consequence: `extracted/il2cpp-full.json` is the only REA-generated IL2CPP inventory — treat it
  as irreplaceable evidence.**
- `rea inspect-evidence-bundle extracted/il2cpp-full.json` → schema mismatch: the inspector expects a multi-artifact
  *bundle* envelope (`rea evidence-export` output), not a single-analysis Evidence JSON. Not an integrity failure.
- **Evidence-chain digest check**: `shasum -a 256 global-metadata.dat` →
  `6a691594910cb775ce18f1c1dd7794b13600d2f98b4c340276e1655b8be31cb3` — **matches** the subject digest recorded
  inside `il2cpp-full.json`. The metadata inventory is provably derived from the file still on disk.
- `ipsw macho info` → segment map above (re-confirmed).
- **Method-pointer table re-verification**: `DataHub::GetRandomOutdoorAnimal` = metadata method idx **12629**;
  table entry at `0x959c438 + 12629*8` = raw `0x0010000003d2e73c` → low36 `0x3d2e73c` — **matches** the
  HANDOFF §14 validated address. First table entries are null (methods without native bodies).
- `ipsw macho disass --vaddr 0x3d2e73c` → valid ARM64 prologue (`stp x26,x25…`, frame setup) — address→code binding confirmed.

## 4. Findings

1. **The deobfuscation chain is: REA (metadata) + ipsw (native disassembly) joined by the low-36-bit method-pointer
   table.** Everything the recovery needs crosses that bridge: metadata index → table slot → native VA → ipsw
   disassembly → hand-written IL. It has now been independently re-validated end-to-end.
2. **The obfuscation layers actually present** are: (a) IL2CPP ahead-of-time compilation (no managed IL in the app —
   bodies exist only as ARM64), (b) encrypted/obfuscated TextAssets (base64+XOR, NRBF), (c) metadata-usage tagging
   (0xc00341e5-style tokens resolved at runtime; string literals live in `global-metadata.dat`, **not** the Mach-O),
   (d) the UnityMono asset layer (`.dll`-based project) whose recovered methods needed the fixer.
3. **Tool-drift risk is real**: REA 6.7.0 can no longer regenerate the IL2CPP metadata inventory. Preserving
   `extracted/il2cpp-full.json` and the digest-pinned `global-metadata.dat` is mandatory; do not "clean up" either.
4. REA's own limitations block (quoted from the Evidence JSON) remain accurate: field/parameter/return types are not
   recovered without the native binary, and native code addresses are not recovered without it — which is exactly why
   the ipsw half of the chain is required.

## 5. Related reports

- `herdr-reports/racing-cup-crosscheck.md` (#68) — native cross-check of the racing-cup family using this chain.
- `herdr-reports/HANDOFF.md` — recovery state, §14 resolver validation, §16–18 build status.
- `herdr-reports/data-recovery.md`, `herdr-reports/ad-audit.md`, `herdr-reports/native-crosscheck-51.md`.

## 6. Addendum (2026-11-17, after t1h's #47 review `herdr-reports/review-47-rea-ipsw.md`)

Corrections and verified evidence from the review, adopted here:

1. **ipsw cache write inside the evidence tree (corrects §3 above):** `ipsw macho disass` against UnityFramework
   creates `ipa-extract/.../UnityFramework.framework/UnityFramework.a2s` (17.9 MB, documented in
   `tools/ipsw-skill/ipsw/SKILL.md:55`). The mtime (17 Nov 11:43) falls inside this session's re-runs, so the
   "read-only" characterization of the ipsw steps was wrong. Remediation adopted: all future ipsw invocations from
   this agent pass `--cache /tmp/t2m.a2s` (or `--dec-nocache`). The `.a2s` file is a derived cache — deleting it is
   a hygiene decision for the evidence owner (t1h), not this agent.
2. **Managed DLLs in the bundle are generated output:** `ipa-extract/.../Data/Managed/` holds 129 DLLs
   (mtime 16 Nov 19:34) that do not exist in `app.ipa` (which carries only `Metadata/global-metadata.dat`,
   `Resources/*.dll-resources.dat`, `mono/4.0/machine.config`). They are staged reconstruction output inside the
   extracted bundle — a hygiene issue, nothing destroyed.
3. **Resolver validation (cite, don't re-derive):** repeated `method_address.py` runs are deterministic
   (treat idx 12633 → raw `0x0010000003d2f06c` → `0x3d2f06c`; sibling idx 12629 → `0x3d2e73c`), and every resolved
   VA falls inside `__TEXT.il2cpp` (`0x00370a280–0x0076af590` per `ipsw macho info`) — the strongest single
   validation that the pointer table yields code addresses.
4. **Substantive discrepancy carried into the record (t1h-verified):** `LoadOutdoorAnimalSet` and
   `LoadOutdoorAnimalTreatSet` are Cpp2IL stubs, and a Cecil scan of `Assembly-CSharp.dll` finds **no `stsfld` to
   `_outdoorAnimalSet` / `_outdoorAnimalTreatSet` anywhere** — only the two repaired getters' `ldsfld`. Both
   getters will therefore enumerate a null collection and throw at runtime: the repaired IL is faithful, but its
   data source is never populated. This qualifies the treat-method repair (HANDOFF §16, the 125th method) and any
   build verdict that depends on it.
5. **HANDOFF §5 is stale** ("Reconstructed 124 methods" vs §16/§19's 125) — flagged to the document owner.
