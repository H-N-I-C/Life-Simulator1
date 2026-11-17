# BitLife Unity Project — Ad & Tracking Remnant Audit

**Target:** `/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e/repo/bitlife`
**Date:** 2026-11-17 · **Mode:** read-only audit (no project files modified)

**Scope scanned:** `Packages/` (manifest.json, packages-lock.json), `Assets/` (Scripts, Plugins, Resources, Scenes, Editor), `ProjectSettings/`, `UserSettings/`. Method: ripgrep over all 43,046 text files (`.cs`, `.asset`, `.unity`, `.prefab`, `.json`, `.shader`, `.meta`, …) for the nine removed SDK names (AppsFlyer, Didomi, Gadsme, Embrace, Firebase, GoogleMobileAds, LevelPlay, CleverTap, GameAnalytics) plus 20 other ad/analytics networks; manual context review of all 145 hits; DLL inventory of `Assets/Plugins`; dangling-GUID verification for every orphaned asset.

---

## Verdict (carefully worded)

**The Unity project source contains no functional third-party ad/tracking code.** No ad-SDK DLL exists under `Assets/Plugins`, no network-call with a body, and every SDK-named method in the decompiled C# has an empty body (`{ }`) or returns `null`/`false`/`0f` — consistent with a deliberate SDK-removal pass over this codebase.

**However, "all ads removed" cannot be claimed**, for three concrete reasons below: (1) real ad-network *artifacts* (Gadsme config with a live game ID, the complete Gadsme MRAID ad-rendering JS, 8 Gadsme shaders, Google Mobile Ads settings, Embrace SDK metadata) are still on disk as orphaned assets; (2) the SDK surface survives as dozens of empty-bodied stub classes; (3) the **shipped IPA** (`ipa-extract/Payload/BitLife.app/Frameworks/`) still *embeds* the native SDK binaries — the cleanup is not reflected in that built artifact.

---

## 1. Package manifest — clean

`Packages/manifest.json` contains only stock Unity modules (`com.unity.modules.*`). No third-party packages at all — no AppsFlyer, Didomi, Gadsme, Firebase, LevelPlay, GameAnalytics, Advertisement, etc. `packages-lock.json` matches.

Note: `com.unity.modules.unityanalytics` is a **stock Unity engine module** (built-in, present in every project), not an added analytics SDK. Harmless.

## 2. Plugin DLL inventory — clean of ad SDKs

`Assets/Plugins/` (170 files) holds only: **Unity.Purchasing** family (IAP — see §6), AWSSDK.Cognito*, Apple.Core/GameKit/Auth, VoxelBusters.CoreLibrary/EssentialKit (native sharing/features), DOTween, Unity.TextMeshPro, ZXing, Newtonsoft.Json, Castle.Core, NativeShare, DynamicExpresso, Unity.Services.Core.* (licensing), UnityOpenFeature, Google.MiniJson. **No ad-network DLLs.** No native `.a`/`.framework`/`.dylib` binaries anywhere under `Assets/` — the native side of the SDKs is gone from the project.

## 3. Real ad/tracking artifacts still on disk (orphaned but present)

These are the strongest findings — files whose content is genuine ad-SDK material:

| # | Path | What it is | Evidence |
|---|------|-----------|----------|
| 1 | `Assets/Resources/GadsmePreferences.asset` | **Gadsme ad-network config** with a real production `gameId: cmo2s0m38009jx80j0z8cbh2q`, `maxConcurrentRequests: 3`, `maxActiveAdContents: 10` | Its script GUID `dbce21dc56d1adc93e3c79d5a61381d6` matches **no** `.meta` in the project → orphaned (Missing Script) |
| 2 | `Assets/Resources/gadsmebridge.js.txt` | **Complete Gadsme MRAID bridge** (~16 KB minified JS): `mraid` command executor, `GadsmeNativeBridgeAndroid` / `webkit.messageHandlers.GadsmeNativeBridge` postMessage plumbing, ad click/resize/storePicture logic | No source file references `gadsmebridge` → dead resource |
| 3 | `Assets/Resources/shaders/gadsme{placement,placement2d,placementunlit,placementroundedprogress,visibility,unregistered,unregistered2d}shader.shader` (7 files) + `Assets/Resources/computeshaders/GadsmeColorVisibilityComputeShader.asset` | Gadsme in-3D-world ad rendering shaders | No script/consumer references remain |
| 4 | `Assets/Resources/GoogleMobileAdsSettings.asset` | Google Mobile Ads settings asset | Script GUID `80ff55188f2e4651127747f99e6b46bf` dangling → orphaned |
| 5 | `Assets/Resources/EmbraceFrameMeasurer.prefab`, `Assets/Resources/EmbraceMemoryMonitor.prefab` | Embrace (telemetry) instrumentation prefabs | Script GUID `6f0177f915ee75847eb652abc008ca2f` dangling in both |
| 6 | `Assets/Resources/info/EmbraceSdkInfo.json` | Embrace SDK metadata: `version 2.7.0`, endpoint `repo.embrace.io/repository/unity` | Static file |
| 7 | `Assets/Resources/package.json` | Plugin manifest for `com.didomi.unityplugin` **2.18.1** ("Didomi Unity SDK", iosNativeVersion 2.36.2) — the consent-SDK package descriptor left behind | Static file |
| 8 | `ProjectSettings/UnityConnectSettings.asset` | `UnityAdsSettings:` block (m_Enabled: **0** — disabled, but the setting block remains) | Stock Unity settings key |

These cannot execute (their code side was deleted), but they are real ad-infrastructure residue, not game content.

## 4. Scene remnants — `Assets/Scenes/Main.unity`

Two SDK placeholder GameObjects survive in the main scene:

- **`AppsFlyerObject`** (`&532`, line ~10055): carries a MonoBehaviour whose fields are exactly the AppsFlyer SDK component shape — `devKey`, `appID`, `isDebug`, `getConversionData`, `Purchase` (all empty) — script GUID `ff31607cab28adacefe1bbd285a9a3a9` **dangling** (script deleted).
- **`GameAnalytics`** (`&45`, line ~955): two MonoBehaviours with script GUIDs `e6366f0ce53ff0d6236ab2f8331664b5` and `7ab0bc58a58d8acc66765d7102fef460`, both **dangling**.

Both render as "Missing Script" placeholders in the Editor. No functional risk; they reference deleted code.

## 5. SDK surface survives as empty-bodied stubs (decompiled C#)

Every one of these has an empty body or `null`/`false`/`0f` return — inert by inspection:

**Tracking layer (`Assets/Scripts/Assembly-CSharp/Tracking/` + `UIAnalyticsManager.cs`)**
- `CommonProperties.cs` — `AppsFlyerID => null`
- `UIAnalyticsManager.cs` — `LogAppsFlyerEvent(...) { }`
- `CrashReport.cs` — `FirebaseTracking(...) { }`, `FirebaseLog(...) { }` (+ closure classes `_003CFirebaseTracking_003E…`)
- `IFirebaseGameTelemetryEvent.cs`, `TempBitLife1asMigrationEvent.cs`, `TempBitLife1asMigrationWelcome.cs` (takes `appsFlyerId` params), `TempBitLife1asPreferredLanguageConfirmed.cs`, `BitLifeAdInterstitialStart.cs` — event classes whose `GetDataCleverTap()` / Firebase paths return `null`/empty

**CleverTap** — `DeepLinkManager.cs` (`IsCleverTapDeepLink` → `false`; `HandleCleverTapActionUrl { }`), `Game/GameConstants.cs` (`CleverTapApiUrl`, `GetCleverTapApiKey()`), `IAP_ORIGIN.cs` (enum labels `CLEVERTAP = 112`, `POP_UP_CT_WEBSHOP = 114`)

**ironSource Ad Quality** — `IronSourceAdQuality.cs` (P/Invoke `ironSourceAdQuality_initialize` declarations; `Initialize(string appKey) { }`), `IronSourceAdQualityManifestTools.cs` (empty class), `ISAdQualityiOSInitHandler.cs`, `ISAdQualityCustomMediationRevenue.cs`, `ISAdQualityMediationNetwork.cs` (enum `ADMOB/DT_FAIR_BID/HELIUM/LEVEL_PLAY/MAX/UNITY`), `GAIronSourceIntegration.cs` (`ListenForImpressions { }`)

**Didomi** — `GameManager.cs` (`DidomiReady`, `InitializeDidomi() { }`, `DidomiError { }`, `DelayedAppTrackingPrompt` → `null`)

**Flurry** — `CandyBusiness/Managers/FlurryManager.cs` (`using FlurrySDK;` with all methods empty — the referenced `FlurrySDK` DLL no longer exists)

**CandyBusiness SDK enum** — `CandyBusiness/SDKInteraction.cs`: `enum SDKInteraction { Firebase, Flurry, AppsFlyer, Ironsource, GameAnalytics }` (labels only)

**CandyBusiness "Confidence" (remote-config) ad attributes** — `ConfidenceAttributes.cs` / `ConfidenceAttributeNames.cs` / `ConfidenceSnapshot.cs` (`AppsFlyerID` attribute + `AppsFlyerIdPlayerPrefsKey = "AppsFlyerID"`), `ConfidenceFlags.cs` (`AdInterstitialMasterControl`, `AdInterstitialConfig`, `AdImmersiveMasterControl`, `AdImmersiveConfig`, `AdImmersiveListData`, `BitizenshipAdWording`), `Variants/InterstitialAdVariant.cs`, `Variants/ImmersiveAdVariant.cs`, `Variants/ImmersiveAdListDataVariant.cs` (AdMob ID properties, values null)

**Game's own ad UI (inert, not third-party)** — `UIAdManager.cs`, `UIManager.cs` (`ShowAdBannerBacker { }`, `GetAdBannerHeight → 0f`), `GameReference.cs` (`ExperiencedEnoughForInterstitialAndVideoAds`, `InterstitialOriginString`), `Game/GameVars.cs` (`DateOfLastInterstitial`), `CandyBusiness/Managers/OfflineAdManager.cs` (plays *local VideoClips* — the in-game "watch an ad" feature of the CandyBusiness sim, no SDK), `Shims/NativeAdsShim.cs` — explicit shim: *"Shim for the removed native-ads SDKs (Gadsme / Google Immersive). Type identity only — no ad functionality."*

**False positives excluded:** ~90 of the 145 grep hits are gameplay vocabulary — `Adjust*` = slider/salary adjustments (`MiniGame*Slider.cs`, `Scenario*Adjust*.cs`), `Singular` = grammar article property (`Data*.cs`), `Embrace` = story text (`genesis/*.txt`), `Firebase*` methods that are empty tracking stubs already listed above.

## 6. Intentionally retained: Unity.Purchasing (IAP — not ads)

`Assets/Plugins/` keeps the full IAP stack: `Unity.Purchasing.dll`, `.Stores`, `.Apple`, `.AppleMacosStub`, `.Security`, `.SecurityCore`, `.Utilities`, `.PaymentProviderService`, `.LiveContentAdapterService`, `.WebshopService` (+ `Unity.Services.Core.*`). 15 decompiled scripts reference `UnityEngine.Purchasing`. These are **receipt-validation / store infrastructure — the "intentionally retained inert Unity.Purchasing receipt types"** — and are correctly kept: they implement purchases, not advertising. They should **not** be removed by any cleanup. Same for AWSSDK.Cognito*, Apple.* and VoxelBusters.* (real game features).

## 7. Cross-reference: the shipped IPA still embeds the native SDKs

Outside the audited project (in `ipa-extract/Payload/BitLife.app/Frameworks/`) the **built app** still contains the native binaries: `AppLovinSDK.framework`, `InMobiSDK.framework`, `HyprMX.framework`, `MolocoSDK.framework`, `OMSDK_Appodeal.framework`, `OMSDK_Pubnativenet.framework`, `Didomi.framework`, `ATOM.framework` (plus `UnityFramework.framework` with a ~67 MB `__TEXT.il2cpp` section). Since the Unity project contains no corresponding native plugins, **that IPA predates (or diverges from) the SDK removal in this source tree**. Anyone verifying "ads removed" against the *binary* will find them; the verification target must be this project source, and the IPA should be rebuilt from the cleaned source.

## 8. What was not verifiable

- **No compiled output exists in the project** (`Library/` is empty — no build cache), so I could not confirm the SDK code is absent from a fresh compile; the assessment is source-level.
- The decompiled `.cs` files have empty bodies *by decompilation of a stubbed build*; if the original (pre-removal) sources are ever re-imported, this audit's conclusions would need re-checking.
- `Logs/` contains only a shader-compiler log; nothing relevant.

## 9. Safe-removal recommendations (in order of confidence)

1. **Delete the orphaned Gadsme/GoogleMobileAds/Embrace assets** (§3 items 1–7): `GadsmePreferences.asset`, `gadsmebridge.js.txt`, the 8 Gadsme shaders/compute shader, `GoogleMobileAdsSettings.asset`, both `Embrace*.prefab`, `info/EmbraceSdkInfo.json`, `Resources/package.json` (Didomi descriptor). All reference deleted code (dangling GUIDs) — zero functional impact, verified by GUID search.
2. **Delete the two scene placeholders** `AppsFlyerObject` and `GameAnalytics` from `Assets/Scenes/Main.unity` (missing-script GameObjects).
3. **Delete the empty-bodied tracking stubs** (§5) *if* the goal is a fully cleaned codebase: `Tracking/*` Firebase/AppsFlyer classes, `UIAnalyticsManager.LogAppsFlyerEvent`, `FlurryManager`, `IronSourceAdQuality*` + `ISAdQuality*` + `GAIronSourceIntegration`, the `SDKInteraction` enum, the CleverTap members in `DeepLinkManager`/`GameConstants`. **Caveat:** these are decompiled sources; other code may call them. If the tree must stay compilable, prefer the `NativeAdsShim.cs` pattern already used in the project (keep the type, keep the empty body, add the comment) rather than deleting.
4. **Keep** everything in §6 (Unity.Purchasing IAP stack, AWS/Apple/VoxelBusters) — intentional, functional-adjacent, not advertising.
5. **Review `privacyPolicy.txt` / `privacyPolicy-ios.txt`**: they still advertise AppLovin, AppsFlyer, Chartboost, GameAnalytics, HyprMX, InMobi, IronSource and Vungle as data partners. If the SDK removal is final, the legal copy is now inaccurate and should be updated (or confirmed accurate for the *shipped* IPA, which still contains some of these).
6. **Rebuild the IPA** from this cleaned project if a verified ad-free binary is the goal; the current `ipa-extract` artifact fails that test (§7).
7. **Optional hygiene:** the disabled `UnityAdsSettings` block in `ProjectSettings/UnityConnectSettings.asset` can stay (stock Unity settings) or be zeroed via Project Settings → Services.

---

### Summary table

| Category | Status |
|---|---|
| Ad-SDK packages in manifest | **None** ✅ |
| Ad-SDK DLLs / native plugins under `Assets/` | **None** ✅ |
| Functional ad/tracking code in decompiled C# | **None found** — all SDK-named methods empty-bodied ✅ |
| Orphaned ad-SDK artifacts (Gadsme/Google/Embrace/Didomi) | **8 clusters remain** ⚠️ |
| SDK-named stub classes & scene placeholders | **Dozens remain** (inert) ⚠️ |
| Unity.Purchasing (IAP) | Intentionally retained, not ads ✅ |
| Shipped IPA binary | **Still embeds native ad SDKs** ❌ |
| "All ads removed" claim | **Not verified for the binary; source tree is clean of functional ad code with the residues listed above** |
