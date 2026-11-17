#!/usr/bin/env python3
"""Reverse of tools/method_address.py: native VA -> method (index, name, declaring type).

Reads the method pointer table at file offset 0x959C438 (low 36 bits carry the
code address) and the method/type names from extracted/il2cpp-full.json.
Usage: va_to_method.py 0x396afec 0x395301c ...
"""
import json
import struct
import sys

BINARY = "/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e/ipa-extract/Payload/BitLife.app/Frameworks/UnityFramework.framework/UnityFramework"
METADATA = "/Users/sahyaboutorabi/Documents/kimi/tasks/2026-11-16/18-08-11-77022b3e/extracted/il2cpp-full.json"
TABLE_OFFSET = 0x959C438
MASK = (1 << 36) - 1


def main():
    wanted = [int(a, 16) for a in sys.argv[1:]]
    nr = json.load(open(METADATA))
    nr = nr.get("normalized_result", nr)
    types = nr["types"]
    methods = nr["methods"]
    hits = {va: [] for va in wanted}
    with open(BINARY, "rb") as handle:
        for index, method in enumerate(methods):
            handle.seek(TABLE_OFFSET + index * 8)
            raw = handle.read(8)
            if len(raw) != 8:
                continue
            low = struct.unpack("<Q", raw)[0] & MASK
            if low in hits:
                t = method.get("declaring_type")
                type_name = "?"
                if isinstance(t, int) and 0 <= t < len(types):
                    entry = types[t]
                    type_name = entry.get("full_name") or entry.get("name") or f"type#{t}"
                hits[low].append((index, method.get("name"), type_name))
    for va in wanted:
        for index, name, type_name in hits[va]:
            print(f"0x{va:x}  idx={index}  {type_name}::{name}")
        if not hits[va]:
            print(f"0x{va:x}  <no method pointer match>")


if __name__ == "__main__":
    main()
