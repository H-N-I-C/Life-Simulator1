#!/usr/bin/env python3
"""Export the decrypted CSV data used by the web port to JSON.

Run from the repository root:
    python3 deobfuscation/scripts/export_web_data.py
Writes web/public/data/*.json
"""
import csv
import json
import os

FILES = {
    "a16_dumb-scenarios-universal": "universal-scenarios.json",
    "a16_dumb-scenarios-career": "career-scenarios.json",
    "a16_dumb-scenarios-friend": "friend-scenarios.json",
    "a16_careers": "careers.json",
    "a16_birth-circumstances": "birth-circumstances.json",
    "a16_alcohol-scenarios": "alcohol-scenarios.json",
    "a16_alcohol": "alcohol.json",
    "a16_news-events": "news-events.json",
}


def main():
    out = os.path.join("web", "public", "data")
    os.makedirs(out, exist_ok=True)
    for src, dst in FILES.items():
        with open(os.path.join("data", src), encoding="utf-8") as f:
            rows = list(csv.DictReader(f))
        with open(os.path.join(out, dst), "w", encoding="utf-8") as f:
            json.dump(rows, f, ensure_ascii=False)
        print(f"{src}: {len(rows)} rows -> {dst}")


if __name__ == "__main__":
    main()
