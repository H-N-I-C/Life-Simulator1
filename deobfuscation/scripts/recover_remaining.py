#!/usr/bin/env python3
"""Recover the two remaining BitLife TextAssets.

- notification: base64 -> XOR over UTF-16 code units (key repeats every 20)
- countries: .NET BinaryFormatter/NRBF List<DataCountry>, decoded by the
  companion nrbf-dump project (System.Formats.Nrbf, no type loading).
"""
from __future__ import annotations

import base64
import subprocess
import sys
from pathlib import Path

KEY = "yst.odkzffq.zfshhs16"
HERE = Path(__file__).resolve().parent
REPO = HERE.parents[1]
TASK = REPO.parent
TEXTASSETS = TASK / "extracted" / "textassets"
DATA = REPO / "data"


def notification(src: Path, dst: Path) -> None:
    raw = src.read_bytes()
    data = base64.b64decode(b"".join(raw.split()))
    text = data.decode("utf-8")
    units = text.encode("utf-16-le")
    out = bytearray()
    for i in range(0, len(units), 2):
        unit = units[i] | (units[i + 1] << 8)
        unit ^= ord(KEY[(i // 2) % 20])
        out += unit.to_bytes(2, "little")
    dst.write_text(out.decode("utf-16-le"), encoding="utf-8")


def countries(src: Path, dst: Path) -> None:
    subprocess.run(
        ["dotnet", "run", "--project", str(HERE / "nrbf-dump"), "--", str(src), str(dst)],
        check=True,
    )


def main() -> None:
    DATA.mkdir(parents=True, exist_ok=True)
    notification(TEXTASSETS / "a16_local-notification-v1-text-enc", DATA / "a16_local-notification-v1-text")
    countries(TEXTASSETS / "a16_countries-enc", DATA / "a16_countries")


if __name__ == "__main__":
    main()
