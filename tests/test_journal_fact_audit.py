"""M3c flavour fact audit (Kimi C, 2026-11-21).

Claude's play finding: flavour lines asserted facts the game did not
record ("I got the promotion" with no promotion). This pins the audit of
every yearly flavour line in src/game/content/journal/yearly_lines.py:

- no line may contain a false-fact phrase (promotion, marriage,
  moving in, a newborn, a job lost THIS year, untracked money earned,
  grandchildren, a fixed anniversary length, retirement claims without
  the retired flag, ...);
- illness/medical claims must be gated on the ill flag or a health stat;
- no money amounts appear in flavour text;
- elder_retire_1 must be gated on the retired flag (the only remaining
  lifecycle claim, now backed by real state).

Run: python3 -m unittest tests.test_journal_fact_audit
"""

import re
import unittest

from src.game.content.journal import yearly_lines


# Phrases that assert a specific, game-tracked event the flavour layer
# cannot know happened. Case-insensitive; word-ish matched.
FORBIDDEN = [
    r"got the promotion",
    r"\bpromoted\b",
    r"i got married",
    r"\bwe got married\b",
    r"my wedding",
    r"\bmoved in together\b",
    r"brought the baby home",
    r"\bgave birth\b",
    r"\bi'm pregnant\b",
    r"\bpregnant\b",
    r"lost my job this year",
    r"\bi was fired\b",
    r"earned its first money",
    r"grandkid",
    r"fifty years",
    r"i graduate this year",
    r"i started middle school",
    r"last commute",
    r"new baby in the house",
    r"the old photograph",   # implies a specific lost partner
    r"set two cups",
    r"\bi asked for a raise\b",
    r"\bi put my hand up for a bigger role\b",
    r"\bi bought a lottery ticket\b",
]

MEDICAL = re.compile(
    r"\bdoctor\b|\bclinic\b|\bfever\b|\bcold\b|\bsick\b|\bill\b|"
    r"\billness\b|check-?up|\bdiagnos|\bhospital\b|\bnurse\b", re.I)


class FactAuditTests(unittest.TestCase):
    def texts(self):
        return {line["id"]: line["text"] for line in yearly_lines.LINES}

    def test_no_false_fact_phrases(self):
        texts = self.texts()
        self.assertGreaterEqual(len(texts), 150)
        for line_id, text in texts.items():
            low = text.lower()
            for pat in FORBIDDEN:
                self.assertIsNone(
                    re.search(pat, low),
                    "%s still claims an unrecorded fact (%r): %r"
                    % (line_id, pat, text))

    def test_medical_claims_are_gated(self):
        for line in yearly_lines.LINES:
            if not MEDICAL.search(line["text"]):
                continue
            when = line.get("when") or {}
            conds = (when.get("all") or []) + (when.get("any") or [])
            gated = any(
                ("flag" in c and c["flag"] == "ill")
                or ("stat" in c and c["stat"] == "health")
                for c in conds)
            self.assertTrue(
                gated,
                "%s makes a medical claim without an ill/health gate: %r"
                % (line["id"], line["text"]))

    def test_no_money_amounts_in_flavour(self):
        for line in yearly_lines.LINES:
            self.assertNotIn("$", line["text"],
                             "%s puts an amount in flavour text: %r"
                             % (line["id"], line["text"]))

    def test_lifecycle_claims_backed_by_flags(self):
        texts = self.texts()
        by_id = {l["id"]: l for l in yearly_lines.LINES}
        # retirement claim must require the retired work flag
        retire = by_id["elder_retire_1"]
        conds = (retire.get("when") or {}).get("all") or []
        self.assertIn({"flag": "retired", "is": True}, conds)
        self.assertIn("working days are behind me", texts["elder_retire_1"])
        # rewritten relationship-family lines keep their gates
        for line_id in ("adult_married_1", "adult_movedin_1",
                        "adult_baby_1", "adult_promotion_1"):
            self.assertTrue((by_id[line_id].get("when") or {}).get("all"),
                            "%s lost its state gate" % line_id)

    def test_lines_still_original_and_shaped(self):
        for line in yearly_lines.LINES:
            self.assertTrue(line["text"].strip())
            self.assertIn(line["band"], yearly_lines.BANDS)
            self.assertLess(len(line["text"]), 220)


if __name__ == "__main__":
    unittest.main()
