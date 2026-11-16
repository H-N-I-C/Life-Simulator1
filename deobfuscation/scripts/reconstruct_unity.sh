#!/usr/bin/env bash
# Reconstruct the full Unity project from the decrypted BitLife IPA.
# Prerequisites: UnityPy (pip), AssetRipper 2.0.0 (AssetRipper_mac_arm64),
# Cpp2IL 2022.1.0 built from source, .NET 10+ (or .NET 11 with roll-forward).
#
# Usage: reconstruct_unity.sh /path/to/com.wtfapps.apollo16-3.26.1-Decrypted.ipa /path/to/workdir
set -euo pipefail

IPA="$1"
WORK="$2"
APP="$WORK/ipa-extract/Payload/BitLife.app"
CPP2IL="${CPP2IL:-Cpp2IL}"           # path to the Cpp2IL binary
ASSETRIPPER="${ASSETRIPPER:-AssetRipper.GUI.Free}"
export DOTNET_ROLL_FORWARD=LatestMajor

mkdir -p "$WORK/ipa-extract" "$WORK/recovered" "$WORK/reconstructed"
unzip -o -q "$IPA" -d "$WORK/ipa-extract"

BIN="$APP/Frameworks/UnityFramework.framework/UnityFramework"
META="$APP/Data/Managed/Metadata/global-metadata.dat"
UVERSION=6000.3.9f1

# 1. Dummy DLLs -> let AssetRipper populate real script structures
"$CPP2IL" --force-binary-path "$BIN" --force-metadata-path "$META" \
  --force-unity-version "$UVERSION" --output-as dummydll --output-to "$WORK/recovered/dummydll"
cp "$WORK"/recovered/dummydll/*.dll "$APP/Data/Managed/"

# 2. IL method bodies (100% of methods recovered for this build)
"$CPP2IL" --force-binary-path "$BIN" --force-metadata-path "$META" \
  --force-unity-version "$UVERSION" --output-as dll_il_recovery --output-to "$WORK/recovered/il-dll"

# 3. Reference C# (noisy bodies; structures exact)
ilspycmd "$WORK/recovered/il-dll/Assembly-CSharp.dll" -o "$WORK/recovered/csharp-full"

# 4. AssetRipper headless export
"$ASSETRIPPER" --headless --port 7477 &
AR_PID=$!
trap 'kill $AR_PID' EXIT
sleep 3
curl -sf -X POST localhost:7477/LoadFolder --data-urlencode "path=$APP" -o /dev/null
curl -sf -X POST localhost:7477/Export/UnityProject \
  --data-urlencode "path=$WORK/reconstructed/BitLife-Unity" -o /dev/null

# 5. Decrypted gameplay data (CSV/JSON) for reference / tooling
python3 "$(dirname "$0")/decrypt_assets.py" "$APP" "$WORK/data"

echo "Done. Open $WORK/reconstructed/BitLife-Unity in Unity $UVERSION (+ WebGL module)."
