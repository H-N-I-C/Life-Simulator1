#!/usr/bin/env python3
"""Remove ad/attribution/analytics SDKs from the reconstructed Unity project.

Usage:
    python3 deobfuscation/scripts/strip_ads.py /path/to/BitLife-Unity/ExportedProject [--apply]

Default is a dry-run report. --apply deletes the SDK folders/files listed below.
Game-side callers (AdManager, paywall gates) are patched separately in the
feature pass; this script only removes the third-party SDK payloads.
"""
import os
import shutil
import sys

# Third-party ad / tracking SDK directories and script roots (relative to ExportedProject)
SDK_PATHS = [
    "Assets/Scripts/Assembly-CSharp/Gadsme",
    "Assets/Scripts/Assembly-CSharp/GameAnalyticsSDK",
    "Assets/Scripts/Assembly-CSharp/CleverTapSDK",
    "Assets/Scripts/Assembly-CSharp/UnityEngine/Purchasing",
    "Assets/Scripts/Assembly-CSharp/ISAdQualityJSON",
]
SDK_FILES = [
    "Assets/Scripts/Assembly-CSharp/AppsFlyerManager.cs",
    "Assets/Scripts/Assembly-CSharp/AppsFlyerDeferredEvent.cs",
    "Assets/Scripts/Assembly-CSharp/AppsFlyerDeferredEventCache.cs",
    "Assets/Scripts/Assembly-CSharp/AppsFlyerEventName.cs",
    "Assets/Scripts/Assembly-CSharp/AppsFlyerGoogleReceipt.cs",
    "Assets/Scripts/Assembly-CSharp/AppsFlyerMilestoneLogger.cs",
    "Assets/Scripts/Assembly-CSharp/ClevertapTriggerManager.cs",
    "Assets/Scripts/Assembly-CSharp/EmbraceManager.cs",
    "Assets/Scripts/Assembly-CSharp/CLEVERTAP_TRIGGER.cs",
]
NATIVE_FRAMEWORKS = [
    "AppLovinSDK.framework", "HyprMX.framework", "InMobiSDK.framework",
    "MolocoSDK.framework", "OMSDK_Appodeal.framework", "OMSDK_Pubnativenet.framework",
    "ATOM.framework", "Didomi.framework",
]


def main():
    root = sys.argv[1]
    apply = "--apply" in sys.argv
    removed, missing = [], []
    for rel in SDK_PATHS + SDK_FILES:
        p = os.path.join(root, rel)
        for target in (p, p + ".meta"):
            if os.path.exists(target):
                removed.append(target)
                if apply:
                    shutil.rmtree(target) if os.path.isdir(target) else os.remove(target)
            else:
                missing.append(rel)
    print(("APPLIED" if apply else "DRY-RUN"), f"— {len(removed)} paths")
    for p in removed:
        print("  ", os.path.relpath(p, root))
    if missing:
        print("not present:", sorted(set(missing)))
    print("\nNative frameworks to exclude from any iOS re-build (not part of WebGL):")
    for f in NATIVE_FRAMEWORKS:
        print("  ", f)


if __name__ == "__main__":
    main()
