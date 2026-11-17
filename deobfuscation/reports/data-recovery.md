# BitLife data recovery — the two remaining files

Task root: `/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e`
Date: 2026-11-17 (Australia/Sydney)
Scope: recover `a16_countries-enc` and `a16_local-notification-v1-text-enc`; no edits to `repo/bitlife`; no Cpp2IL/AssetRipper; originals preserved.

## Result

Both files are recovered and verified.

| Output | Path | Size | SHA-256 |
| --- | --- | ---: | --- |
| Notification CSV | `repo/data/a16_local-notification-v1-text` | 6,646 chars | `fb1c26ea02d2f3e0547f0d2ff4f9dd19fe8c43308205444e310f31eacdcdff97` |
| Countries JSON | `repo/data/a16_countries` | 5,184,700 bytes | `a60186dcc0bb6f6f711552099a021a9586b779e2027dab123f9582e94394a3c8` |

Inputs (unchanged):

| Input | Size | SHA-256 |
| --- | ---: | --- |
| `extracted/textassets/a16_countries-enc` | 981,594 bytes | `40139bbd76156bff1c92497c7d6e4905cd984b72ba6b7e77fe4902c3643e45b0` |
| `extracted/textassets/a16_local-notification-v1-text-enc` | 9,028 bytes | `face5a74f3fc2ec0434723612dab18703b37258c03bf6930dfb41940b5633baf` |

## 1. `a16_local-notification-v1-text-enc` — base64 + XOR over UTF-16 code units

The file is ASCII base64. After base64 decoding, the existing pipeline's char-wise variant XORs **Unicode code points**. That is wrong for non-BMP characters: a character such as `🧑‍🎓` is one Python code point but **two UTF-16 code units**, so the repeating key advances by two in the game's `XORCipherString` but by one in the pipeline. The key phase shifts at the first emoji, and the rest of the file becomes binary-looking text.

Correct decode: base64 → decode UTF-8 to a string → re-encode as UTF-16-LE → XOR each 16-bit unit with the 20-character key `yst.odkzffq.zfshhs16` → decode UTF-16-LE.

```python
import base64
KEY = "yst.odkzffq.zfshhs16"
raw = open("extracted/textassets/a16_local-notification-v1-text-enc", "rb").read()
data = base64.b64decode(b"".join(raw.split()))
ct = data.decode("utf-8")
units = ct.encode("utf-16-le")
out = bytearray()
for i in range(0, len(units), 2):
    u = units[i] | (units[i + 1] << 8)
    u ^= ord(KEY[(i // 2) % 20])
    out += u.to_bytes(2, "little")
text = out.decode("utf-16-le")
open("repo/data/a16_local-notification-v1-text", "w", encoding="utf-8").write(text)
```

Verification:

- 6,646 characters, 0 U+FFFD.
- CSV parse: **29 rows** (1 header + 28 notifications), every row **17 columns**.
- Header: `Subcategory,Notification ID,Emoji,Title,Notification Body,Is Bit Pass Active,Is Hero Dead,Hero Min Age,Hero Max Age,Student,Job,Hero Has Lover,Hero Does Not Have Lover,Both Parents Alive,Hero Has Pet,Hero Gender,Lover Gender`.
- Emoji and quoted multi-line bodies survive intact (e.g. the `general_callback_to_play` row and `BitPass,bitpass_claim_rewards_callback,👑,...`).

## 2. `a16_countries-enc` — BinaryFormatter/NRBF `List<DataCountry>`, not XOR

This file is **not base64 and not XOR-encrypted**. It starts with the .NET BinaryFormatter stream header:

```
00 01 00 00 00 ff ff ff ff 01 00 00 00 00 00 00 00
0c 02 00 00 00 46 "Assembly-CSharp, Version=0.0.0.0, ..."
04 01 00 00 00 78 "System.Collections.Generic.List`1[[DataCountry, Assembly-CSharp, ...]]"
03 00 00 00 " _items _size _version "
```

It serializes a `List<DataCountry>` with **128 slots**: 102 `ClassWithMembersAndTypes` records (object ids 4..105) and 26 nulls. Each `DataCountry` has **62 members** (backing fields plus `Cities`, `Disasters`, `DeathPenaltyMethods`, `ImmigrantCountries`, `MaleNames`, `FemaleNames`, `LastNames`).

The payload is decoded with .NET 11's safe NRBF reader `System.Formats.Nrbf` (no type loading, no BinaryFormatter deserialization):

```csharp
using System.Formats.Nrbf;
using var fs = File.OpenRead(path);
var root = NrbfDecoder.Decode(fs);                       // SystemClassWithMembersAndTypesRecord
var items = (SZArrayRecord<SerializationRecord>)((ClassRecord)root)
            .GetSerializationRecord("_items")!;          // SZArrayOfRecords, 128 elements
var records = items.GetArray(true);                      // 102 DataCountry + 26 null
```

Project reference used (SDK-local, no NuGet install):

```xml
<Reference Include="System.Formats.Nrbf">
  <HintPath>$(MSBuildSDKsPath)/../System.Formats.Nrbf.dll</HintPath>
</Reference>
```

The dumper reads each `ClassRecord`'s `MemberNames`, uses `GetRawValue` for strings/primitives and recurses into `ClassRecord`/`SZArrayRecord<string>` for the list fields, then writes JSON. The exact program used is the `nrbf-dump` helper in this report's companion script (see below); it produced 5,184,700 bytes of JSON.

Verification:

- JSON parses; array length 128; **102 non-null** entries; every non-null entry has **the same 62 keys**.
- Sample records:
  - Afghanistan / `af` / Asia / `Dari` / `$` / Cities `["Kabul","Kandahar","Jalalabad"]` / Disasters `["Earthquakes","Floods","Droughts"]`.
  - United States / `us` / 58 cities / 512 male names.
  - Japan / `jp` / 8 cities / 128 male names; Brazil / `br` / 12 cities / 128 male names.
- No replacement characters; 39 empty strings total across the 102 records (legitimate empty fields such as optional article/school names).

## How REA and ipsw were used

- REA inventory `extracted/il2cpp-full.json` (metadata version 39, 22,642 types, 117,369 fields) confirms the type `DataCountry` with 62 fields (matching the serialized member names) and the presence of `System.Runtime.Serialization.Formatters.Binary.BinaryFormatter`.
- `ipsw macho info` on `ipa-extract/.../UnityFramework.framework/UnityFramework` confirms the arm64 dylib and the `__TEXT.il2cpp` section (0x0370a280–0x076af590), matching the metadata source. Both commands are read-only.
- The existing 964/966 pipeline in `repo/deobfuscation/scripts/decrypt_assets.py` was not modified.

## Reproduce

Notification: run the Python block above against `extracted/textassets/a16_local-notification-v1-text-enc`.

Countries:

```bash
cd repo/deobfuscation/scripts/nrbf-dump
dotnet run -- <task>/extracted/textassets/a16_countries-enc <task>/repo/data/a16_countries
```

## Files created

- `repo/data/a16_local-notification-v1-text` (verified CSV)
- `repo/data/a16_countries` (verified JSON; 128 slots, 102 records)
- `repo/deobfuscation/scripts/recover_remaining.py` (notification decoder + countries wrapper)
- `repo/deobfuscation/scripts/nrbf-dump/` (`Program.cs` + `nrbf-dump.csproj`)

No files under `repo/bitlife` were edited; no git commit or push was made.
