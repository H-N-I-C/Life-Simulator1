# Deobfuscation — BitLife 3.26.1 (`com.wtfapps.apollo16`)

Target: `com.wtfapps.apollo16-3.26.1-Decrypted.ipa` (already FairPlay-decrypted).
Everything below was recovered statically from that artifact.

## Toolchain

| Tool | Role |
| --- | --- |
| [REA](https://github.com/morluto/rea) (`npx rea-agents@latest`) | IL2CPP `global-metadata.dat` inventory: 22,642 types, 155,573 methods, 117,369 fields, 71,699 string literals (metadata version **39**) |
| [ipsw](https://github.com/blacktop/ipsw-skill) (`brew install blacktop/tap/ipsw`) | Mach-O inspection of the 161 MB `UnityFramework` dylib (arm64, `__TEXT.il2cpp` section) |
| [UnityPy](https://github.com/K0lb3/UnityPy) | Extraction of 51,658 objects from `Data/data.unity3d`: 1,086 TextAssets, 8,422 Texture2D, 8,288 MonoScripts |

## Binary layout

- `Payload/BitLife.app/BitLife` — 90 KB launcher stub.
- `Frameworks/UnityFramework.framework/UnityFramework` — all game + engine code (Unity **IL2CPP**, arm64).
- `Data/Managed/Metadata/global-metadata.dat` — magic `0xFAB11BAF`, version 39.
- Game code is obfuscated with **Beebyte Obfuscator** (see `Beebyte.Obfuscator.*`
  attributes in `game-classes.txt`); string literals are obfuscated at build time.
- 6,098 game classes (Assembly-CSharp) are listed in `game-classes.txt`.

## Game-data encryption (fully recovered)

967 TextAssets named `*-enc` hold virtually all gameplay data (scenarios,
careers, accessories, names, …). `EncryptionManager` (12 methods:
`DecodeAndDecryptString`, `DecryptString`, `DecryptOriginalString`,
`XORCipherString`, `DefaultCipherKey`, `ObfuscateString`, …) implements:

```
Base64 decode → repeating-key XOR (20-byte key) → UTF-8 text (CSV or JSON)
```

- **Key**: `yst.odkzffq.zfshhs16` (recovered by known-plaintext/frequency
  analysis over all 966 files; not present as a plaintext literal).
- **Two variants** (both live in this build):
  - *byte-wise* (legacy, `DecryptOriginalString`): XOR raw bytes — 910 files.
  - *char-wise* (`DecryptString`): ciphertext is UTF-8 of code units, XOR per
    character — 54 files.
- Result: **964 / 966 files fully decrypted**, checked into `../data/`
  (1082 files including the 118 unencrypted TextAssets).

Reproduce everything with:

```bash
python3 deobfuscation/scripts/decrypt_assets.py /path/to/BitLife.app data/
```

## Data model highlights (used by the web port)

| File | Contents |
| --- | --- |
| `a16_dumb-scenarios-universal` | 257 random life events: age range, student/job/gender conditions, up to 4 choices, each with narrative + karma/discipline/happiness/intelligence/appearance impacts |
| `a16_dumb-scenarios-career` | Per-career random events (matched on career `Type`), incl. job performance impact |
| `a16_dumb-scenarios-friend` | Friend interaction events with relationship impact |
| `a16_careers` | 167 careers: seniority tracks, salaries, education/age/stat requirements |
| `a16_birth-circumstances` | 186 birth flavour texts with parent/age/stat constraints |
| `a16_alcohol-scenarios` | Nightlife/drinking choice events |
| `a16_news-events` | World-news flavour with `<country>`, `<numX-Y>` templating |

Scenario text uses a templating DSL: `<a/b/c>` random pick, `<num5-100>`
random integer, `<friend>`, `<friend-possessive>`, `<he/she>`, `<his/her>`,
`<him/her>`, `<country>`, `<teacher>`, `<singular>`/`<plural>` (drinks), `<rb>`.

## Exact-recovery pipeline (Unity project reconstruction)

Engine version detected by AssetRipper: **Unity 6000.3.9f1**.

1. **Cpp2IL 2022.1.0** (built from source, `net10.0`; run with
   `DOTNET_ROLL_FORWARD=LatestMajor` on .NET 11) reads the Mach-O fine —
   code registration `0x8D02B98`, metadata registration `0x8E84D00`, all
   155,573 method pointers mapped:

   ```bash
   Cpp2IL --force-binary-path .../UnityFramework.framework/UnityFramework \
          --force-metadata-path .../Data/Managed/Metadata/global-metadata.dat \
          --force-unity-version 6000.3.9f1 \
          --output-as dummydll --output-to recovered/dummydll
   ```

   Output formats used: `dummydll` (128 DLLs, script structure for AssetRipper),
   `diffable-cs` (full class/method map), `dll_il_recovery` (method bodies).
   IL recovery result: **100% of methods decompiled (90,870 / 90,870)**.
   Bodies carry Beebyte/ARM64 recovery noise (`Cpp2ILHelpers.NoteDecompilerIssue`)
   and need per-method cleanup; structures, signatures and fields are exact.
   Reference C#: `ilspycmd recovered/il-dll/Assembly-CSharp.dll -o recovered/csharp-full`.

2. **AssetRipper 2.0.0** (`AssetRipper_mac_arm64`) — copy the 128 dummy DLLs into
   `Payload/BitLife.app/Data/Managed/` first so scripts are populated, then:

   ```bash
   ./AssetRipper.GUI.Free --headless --port 7477
   curl -X POST localhost:7477/LoadFolder --data-urlencode "path=.../BitLife.app"
   curl -X POST localhost:7477/Export/UnityProject --data-urlencode "path=.../BitLife-Unity"
   ```

   Result: complete Unity project — `Main.unity` scene, all sprites/audio/fonts/
   prefabs, and the full `Assembly-CSharp` tree (`BitLife`, `Game`, `Backend`,
   `CandyBusiness`, …) with real class structures.

3. Open in **Unity 6000.3.9f1** with the **WebGL Build Support** module to build
   the web version. Method bodies must be merged from the IL-recovery output
   (`ilspycmd` on `dll_il_recovery` DLLs) wherever AssetRipper bodies are empty.

### Ad / tracking SDKs to strip for the ad-free build

Native frameworks: `AppLovinSDK`, `HyprMX`, `InMobiSDK`, `MolocoSDK`,
`OMSDK_Appodeal`, `OMSDK_Pubnativenet`, `ATOM`, `Gadsme`, `Didomi` (ad consent).
Managed namespaces: `Appodeal`, `Gadsme`, `GameAnalyticsSDK`, `CleverTapSDK`,
`AppsFlyer`, `Embrace`, `Backend.Authentication`/`AppsFlyerDeferredEventCache`
(attribution). Keep `UnityEngine.Purchasing` stubs only if IAP is wanted;
remove `Data/Raw/genesis_paywall*` gating for full unlock.

## Open items / next deobfuscation passes

1. **2 undecoded files**: `a16_countries-enc`, `a16_local-notification-v1-text-enc`
   (the latter decodes char-wise except one segment — possibly a per-file key or
   the `ObfuscateChar` path; decompiling `EncryptionManager` will settle it).
2. ~~Method-address linking~~ **Solved by Cpp2IL** (handles the chained fixups;
   full code+metadata registration recovered). REA's IL2CPP inventory remains
   useful for fast type/string queries.
3. **MonoBehaviour typetrees are stripped** — ScriptableObject payloads (name
   lists, balancing config) need typetree generation (Il2CppInspector/Cpp2IL)
   before UnityPy can parse them.
4. **8,422 Texture2D / 437 AudioClip** not yet exported (art/audio for the web UI).
