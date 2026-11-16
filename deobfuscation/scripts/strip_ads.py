#!/usr/bin/env python3
"""Remove ad/attribution/analytics/IAP SDKs from the reconstructed BitLife project.

Usage:
    python3 deobfuscation/scripts/strip_ads.py /path/to/bitlife [--apply]

Default is a dry-run report. --apply deletes the SDK payloads (DLLs and C#
source trees). Game-side callers are fixed in the compile pass afterwards.
"""
import os
import shutil
import sys

# C# source trees inside Assets/Scripts/Assembly-CSharp
CS_DIRS = [
    "Gadsme",
    "GameAnalyticsSDK",
    "CleverTapSDK",
    "ISAdQualityJSON",
    "UnityEngine/Purchasing",
]
# Game-side ad/tracking manager scripts (inert stubs, removed with the SDKs)
CS_FILES = [
    "AppsFlyerManager.cs",
    "AppsFlyerDeferredEvent.cs",
    "AppsFlyerDeferredEventCache.cs",
    "AppsFlyerEventName.cs",
    "AppsFlyerGoogleReceipt.cs",
    "AppsFlyerMilestoneLogger.cs",
    "ClevertapTriggerManager.cs",
    "CLEVERTAP_TRIGGER.cs",
    "EmbraceManager.cs",
]
# Precompiled SDK payloads in Assets/Plugins
PLUGIN_DLLS = [
    "AppsFlyer.dll",
    "DidomiAssembly.dll",
    "Gadsme.dll",
    "Embrace.SDK.dll",
    "Firebase.Analytics.dll",
    "Firebase.App.dll",
    "Firebase.Crashlytics.dll",
    "Firebase.Messaging.dll",
    "Firebase.Platform.dll",
    "Firebase.TaskExtension.dll",
    "GoogleMobileAds.Common.dll",
    "GoogleMobileAds.Core.dll",
    "GoogleMobileAds.Ump.dll",
    "GoogleMobileAds.Ump.iOS.dll",
    "GoogleMobileAds.dll",
    "GoogleMobileAds.iOS.dll",
    "GoogleMobileAdsNative.Common.dll",
    "GoogleMobileAdsNative.dll",
    "GoogleMobileAdsNative.iOS.dll",
    "Unity.LevelPlay.dll",
    "Unity.Purchasing.Apple.dll",
    "Unity.Purchasing.AppleMacosStub.dll",
    "Unity.Purchasing.LiveContentAdapterService.dll",
    "Unity.Purchasing.PaymentProviderService.dll",
    "Unity.Purchasing.Security.dll",
    "Unity.Purchasing.SecurityCore.dll",
    "Unity.Purchasing.Stores.dll",
    "Unity.Purchasing.Utilities.dll",
    "Unity.Purchasing.WebshopService.dll",
    "Unity.Purchasing.dll",
    "Unity.Services.Core.Telemetry.dll",
    "UnityEngine.UnityConsentModule.dll",
    "VoxelBusters.EssentialKit.Demo.dll",
]


def main():
    root = sys.argv[1]
    apply = "--apply" in sys.argv
    targets = [f"Assets/Scripts/Assembly-CSharp/{p}" for p in CS_DIRS + CS_FILES]
    targets += [f"Assets/Plugins/{p}" for p in PLUGIN_DLLS]
    removed, missing = [], []
    for rel in targets:
        p = os.path.join(root, rel)
        for target in (p, p + ".meta"):
            if os.path.exists(target):
                removed.append(rel)
                if apply:
                    shutil.rmtree(target) if os.path.isdir(target) else os.remove(target)
            elif not target.endswith(".meta"):
                missing.append(rel)
    print(("APPLIED" if apply else "DRY-RUN"), f"— {len(removed)} paths removed")
    for rel in sorted(set(removed)):
        print("  ", rel)
    if missing:
        print("not present:", missing)


if __name__ == "__main__":
    main()
