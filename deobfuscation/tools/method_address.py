#!/usr/bin/env python3
"""Resolve an IL2CPP method name to its native code address.

The mapping lives in the method pointer table inside the UnityFramework Mach-O.
Per HANDOFF.md the table starts at file offset 0x959c438 and each entry is an
8-byte chained pointer whose low 36 bits carry the address.
"""
import json
import struct
import sys

BINARY = "/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e/ipa-extract/Payload/BitLife.app/Frameworks/UnityFramework.framework/UnityFramework"
METADATA = "/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e/extracted/il2cpp-full.json"
TABLE_OFFSET = 0x959C438
MASK = (1 << 36) - 1


def load_metadata():
    data = json.load(open(METADATA))
    nr = data.get("normalized_result", data)
    return nr


def find_methods(nr, name):
    types = nr["types"]
    hits = []
    for index, method in enumerate(nr["methods"]):
        if method.get("name") != name:
            continue
        type_index = method.get("declaring_type")
        type_name = "?"
        if isinstance(type_index, int) and 0 <= type_index < len(types):
            entry = types[type_index]
            type_name = entry.get("full_name") or entry.get("name") or f"type#{type_index}"
        hits.append((index, type_name))
    return hits


def read_pointer(handle, index):
    handle.seek(TABLE_OFFSET + index * 8)
    raw = handle.read(8)
    if len(raw) != 8:
        return None
    return struct.unpack("<Q", raw)[0]


def main():
    name = sys.argv[1] if len(sys.argv) > 1 else "GetNPCWinningDriver"
    nr = load_metadata()
    hits = find_methods(nr, name)
    print(f"matches for {name}: {len(hits)}")
    with open(BINARY, "rb") as handle:
        for index, type_name in hits:
            raw = read_pointer(handle, index)
            if raw is None:
                print(f"  idx={index} {type_name}: no entry")
                continue
            low = raw & MASK
            high = raw >> 36
            print(f"  idx={index} {type_name}: raw=0x{raw:016x} low36=0x{low:x} high=0x{high:x}")


if __name__ == "__main__":
    main()
