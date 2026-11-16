# BitLife — reconstructed from `com.wtfapps.apollo16` 3.26.1

This repository contains the BitLife 3.26.1 (iOS) game recovered from the
decrypted IPA, for private modification and a web (WebGL) build. Not for
redistribution or sale.

## Layout

- `bitlife/` — the reconstructed Unity project (**Unity 6000.3.9f1**).
  Open `bitlife/` in Unity Hub; install the **WebGL Build Support** module
  for browser builds.
- `data/` — 1,082 gameplay data files (scenarios, careers, names, …)
  decrypted from the app's TextAssets.
- `deobfuscation/` — methodology, recovered encryption details, and the
  reproducible scripts (`decrypt_assets.py`, `export_web_data.py`,
  `strip_ads.py`, `reconstruct_unity.sh`).

## Status

- [x] IPA unpacked; Unity IL2CPP build identified (Unity 6000.3.9f1)
- [x] Game-data encryption cracked (20-byte XOR key, two variants), 964/966 files decrypted
- [x] Full IL2CPP decompilation with Cpp2IL — 100% of 90,870 methods recovered
- [x] Unity project reconstructed with AssetRipper (scene, assets, full C# class tree)
- [x] Unity Editor 6000.3.9f1 + WebGL Build Support installed locally
  (`unity/Unity.app` in the task workspace; Unity Hub installed for licensing)
- [ ] Activate free Unity Personal license (Unity Hub sign-in)
- [ ] Clean up obfuscated method bodies so `Assembly-CSharp` compiles
- [ ] Strip ad/tracking SDKs (`deobfuscation/scripts/strip_ads.py`)
- [ ] WebGL build

See `deobfuscation/README.md` for the complete technical write-up.
