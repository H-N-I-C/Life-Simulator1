#!/usr/bin/env python3
"""Fix IL2CPP round-trip compile artifacts in the reconstructed C# sources.

Usage: python3 deobfuscation/scripts/fix_compile_artifacts.py /path/to/bitlife

Fixes:
- CS0051: private closure structs used by internal local functions -> internal
- CS1908: [DefaultParameterValue(0)] on enum parameter -> drop attribute
- Didomi leftovers in GameManager (using + ErrorEvent handler signature)
- CS0535/CS0470: get_Item/set_Item methods -> real C# indexers
- NativeAdsProvider shim (enum only; native ads SDKs were removed)
"""
import os
import re
import sys

ROOT = sys.argv[1]
BASE = os.path.join(ROOT, "Assets/Scripts/Assembly-CSharp")

CS0051 = {
    "Backend/MyStuffController.cs": ["26_0"],
    "DataHub.cs": ["2451_0"],
    "Game/Translation/TranslationController.cs": ["25_0"],
    "Genesis/Entities/GenesisEntityMemoryHelper.cs": ["3_0"],
    "IAPManager.cs": ["136_0"],
    "LifeInterface.cs": ["214_0", "303_0", "318_0", "319_0"],
}


def edit(path, fn):
    with open(path, encoding="utf-8") as f:
        text = f.read()
    new = fn(text)
    if new != text:
        with open(path, "w", encoding="utf-8") as f:
            f.write(new)
        print("patched", os.path.relpath(path, BASE))


for rel, structs in CS0051.items():
    def fix(text, structs=structs):
        for sid in structs:
            name = f"_003C_003Ec__DisplayClass{sid}"
            text = text.replace(f"private struct {name}", f"internal struct {name}")
        return text
    edit(os.path.join(BASE, rel), fix)


def fix_uistats(text):
    return text.replace("[Optional][DefaultParameterValue(0)] BOOST_ATTRIBUTE", "[Optional] BOOST_ATTRIBUTE")
edit(os.path.join(BASE, "BitLife/UI/SupportObjects/UIStatsDataObject.cs"), fix_uistats)


def fix_gamemanager(text):
    text = text.replace("using IO.Didomi.SDK.Events;\n", "")
    return text.replace("DidomiError(object sender, ErrorEvent e)", "DidomiError(object sender, object e)")
edit(os.path.join(BASE, "GameManager.cs"), fix_gamemanager)


def fix_unitydictionary(text):
    old = """	// C# has no syntax for parameterized property 'Item'.
	public virtual V get_Item(K key)
	{
		return default;
	}

	public virtual void set_Item(K key, V value)
	{
	}"""
    new = """	public virtual V this[K key]
	{
		get { return default; }
		set { }
	}"""
    return text.replace(old, new)
edit(os.path.join(BASE, "UnityDictionary.cs"), fix_unitydictionary)


def fix_weightedlist(text):
    old = """	// C# has no syntax for parameterized property 'Item'.
	public WeightedEntry<T> get_Item(int index)
	{
		return null;
	}

	public void set_Item(int index, WeightedEntry<T> value)
	{
	}"""
    new = """	public WeightedEntry<T> this[int index]
	{
		get { return null; }
		set { }
	}"""
    return text.replace(old, new)
edit(os.path.join(BASE, "WeightedList.cs"), fix_weightedlist)

shim_dir = os.path.join(BASE, "Shims")
os.makedirs(shim_dir, exist_ok=True)
with open(os.path.join(shim_dir, "NativeAdsShim.cs"), "w") as f:
    f.write("""namespace CandyBusiness.NativeAds
{
	// Shim for the removed native-ads SDKs (Gadsme / Google Immersive).
	// Type identity only — no ad functionality.
	public enum NativeAdsProvider
	{
		None,
		Gadsme,
		Google,
	}
}
""")
print("shim written: Shims/NativeAdsShim.cs")
