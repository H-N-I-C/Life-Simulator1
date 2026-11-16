#!/usr/bin/env python3
"""
BitLife (com.wtfapps.apollo16) 3.26.1 — game-data extractor & decryptor.

Recovered from the shipped IPA with REA (IL2CPP metadata inventory) and
ipsw (Mach-O analysis). The game's EncryptionManager implements:

    DecodeAndDecryptString = Base64Decode -> XORCipherString
    XORCipherString        = repeating-key XOR, 20-byte key
    DecryptString          = current variant  (char-wise, UTF-16 code units)
    DecryptOriginalString  = legacy variant   (byte-wise)

Two on-disk encodings of the same cipher exist in the wild:

  byte-wise : ciphertext is raw bytes; plaintext[i] = ct[i] ^ key[i % 20]
  char-wise : ciphertext is the UTF-8 encoding of Unicode code units;
              plaintext_char[i] = chr(ord(ct_char[i]) ^ ord(key[i % 20]))

Both use the same 20-byte key. The key was recovered by known-plaintext /
frequency cryptanalysis across 966 files (no literal in IL2CPP metadata —
literals are obfuscated by Beebyte Obfuscator).

Usage:
    pip install UnityPy
    python3 decrypt_assets.py /path/to/BitLife.app /path/to/output-dir
"""
import base64
import os
import re
import sys

import UnityPy

XOR_KEY = "yst.odkzffq.zfshhs16"  # 20 chars
KEY_BYTES = XOR_KEY.encode("ascii")


def _clean(s: str) -> bool:
    printable = sum(1 for ch in s if ch.isprintable() or ch in "\n\r\t")
    return printable / max(1, len(s)) > 0.995


def decrypt(data_b64: bytes):
    """Returns (plaintext, mode) or (None, None) if undecodable."""
    data = base64.b64decode(data_b64)
    try:
        s = bytes(b ^ KEY_BYTES[i % 20] for i, b in enumerate(data)).decode("utf-8")
        if _clean(s):
            return s, "byte"
    except (UnicodeDecodeError, ValueError):
        pass
    try:
        ct = data.decode("utf-8")
        s = "".join(chr(ord(ch) ^ ord(XOR_KEY[i % 20])) for i, ch in enumerate(ct))
        if _clean(s):
            return s, "char"
    except (UnicodeDecodeError, ValueError):
        pass
    return None, None


def main(app_dir: str, out_dir: str):
    os.makedirs(out_dir, exist_ok=True)
    env = UnityPy.load(os.path.join(app_dir, "Data", "data.unity3d"))
    stats = {"plain": 0, "byte": 0, "char": 0, "fail": 0}
    failures = []
    for o in env.objects:
        if o.type.name != "TextAsset":
            continue
        d = o.read()
        name = d.m_Name
        raw = d.m_Script.encode("utf-8", "surrogateescape") if isinstance(d.m_Script, str) else d.m_Script
        safe = re.sub(r"[^\w.\-]", "_", name)
        if name.endswith("-enc"):
            pt, mode = decrypt(raw)
            if pt is None:
                stats["fail"] += 1
                failures.append(name)
                continue
            stats[mode] += 1
            with open(os.path.join(out_dir, safe[:-4]), "w", encoding="utf-8") as f:
                f.write(pt)
        else:
            stats["plain"] += 1
            with open(os.path.join(out_dir, safe), "wb") as f:
                f.write(raw)
    print(stats)
    if failures:
        print("undecoded:", failures)


if __name__ == "__main__":
    if len(sys.argv) != 3:
        sys.exit(__doc__)
    main(sys.argv[1], sys.argv[2])
