"""Backlog polish pin (Kimi C): event-outcome journal lines must carry the
event's prompt context (BACKLOG.md 2026-11-20). Guards the seven rewritten
outcome_texts against regressing to the terse context-free originals.

Run: python3 -m unittest tests.test_event_outcome_context
"""

import json
import os
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

OLD_TERSE = {
    ("vaccination_age2", "stay_calm"): "I remained calm.",
    ("vaccination_age2", "tantrum"): "I threw a tantrum.",
    ("vaccination_age2", "bite"): "I bit your mother.",
    ("partner_breakup_offer", "wish_well"): "I wished them the best.",
    ("partner_breakup_offer", "insult"):
        "I insulted them as they walked out.",
    ("partner_divorce_offer", "honor"):
        "I honored the request for a divorce.",
    ("partner_divorce_offer", "insult"): "They divorced you.",
    ("partner_kidnapping", "pay"): "I paid, but they never returned.",
    ("partner_kidnapping", "nothing"): "I did absolutely nothing.",
    ("dating_offer", "reject"): "I rejected them.",
    ("flavor_summer_reading_list", "read"):
        "I read all four and returned to school insufferably prepared.",
    ("dra_gossip_work", "laugh"): "I gave them nothing and kept my smile.",
}


def _outcome(event_id, choice_id):
    path = None
    for name in os.listdir(os.path.join(ROOT, "src", "game", "content",
                                        "events")):
        if not name.endswith(".json"):
            continue
        with open(os.path.join(ROOT, "src", "game", "content", "events",
                               name), encoding="utf-8") as f:
            d = json.load(f)
        evs = d.get("events", d if isinstance(d, list) else [])
        for e in evs:
            if e.get("id") == event_id:
                path = e
                break
        if path is not None:
            break
    assert path is not None, event_id
    for c in path.get("choices", []):
        if c.get("id") == choice_id:
            return c.get("outcome_text")
    raise AssertionError("choice %s not in %s" % (choice_id, event_id))


class OutcomeContextTests(unittest.TestCase):
    def test_no_regressions_to_terse_lines(self):
        for (event_id, choice_id), old in OLD_TERSE.items():
            text = _outcome(event_id, choice_id)
            self.assertIsNotNone(text, (event_id, choice_id))
            self.assertNotEqual(text, old, (event_id, choice_id))

    def test_rewritten_lines_carry_context(self):
        # each rewritten line references its prompt situation and is long
        # enough to stand alone in the journal
        minimum = {
            "vaccination_age2": ("stay_calm", "doctor"),
            "partner_breakup_offer": ("wish_well", "over"),
            "partner_divorce_offer": ("honor", "divorce"),
            "partner_kidnapping": ("nothing", "ransom"),
            "dating_offer": ("reject", "asked me out"),
            "flavor_summer_reading_list": ("read", "reading list"),
            "dra_gossip_work": ("laugh", "gossip"),
        }
        for event_id, (choice_id, fragment) in minimum.items():
            text = _outcome(event_id, choice_id)
            self.assertIn(fragment, text.lower(), (event_id, text))
            self.assertGreaterEqual(len(text), 40, (event_id, text))


class OutcomeContextSweepTests(unittest.TestCase):
    """Second context sweep (Claude's review, 2026-11-21): rom_/dra_/
    flavor_ outcome lines that referenced their situation only as
    'it/the moment/the rumour'. The old context-free texts must never
    come back; the rewrites must name their subject."""

    GONE = [
        "I politely pulled away. Something in me unclenched the same "
        "evening.",
        "I confirmed the rumour myself and took its teeth.",
        "I tipped. The drummer nodded in perfect rhythm.",
        "I agreed to the date, but they got cold feet and rejected me.",
        "I let the moment pass and wondered about it all the way home.",
        "I studied my glass very hard for the rest of the night.",
        "The joke landed softly and died quickly.",
        "Some doors stay closed for a reason.",
        "I passed. The family car quietly began avoiding the driveway.",
        "The talk waited. It did not disappear.",
        "It was smaller than the fear of keeping it.",
    ]

    def _all_texts(self):
        root = os.path.join(ROOT, "src", "game", "content", "events")
        texts = set()
        for name in sorted(os.listdir(root)):
            if not name.endswith(".json"):
                continue
            with open(os.path.join(root, name), encoding="utf-8") as f:
                data = json.load(f)
            def walk(node):
                if isinstance(node, dict):
                    if "outcome_text" in node and node["outcome_text"]:
                        texts.add(node["outcome_text"])
                    for v in node.values():
                        walk(v)
                elif isinstance(node, list):
                    for v in node:
                        walk(v)
            walk(data)
        return texts

    def test_old_context_free_lines_are_gone(self):
        texts = self._all_texts()
        for old in self.GONE:
            self.assertNotIn(old, texts)

    def test_rewrites_name_their_subject(self):
        texts = self._all_texts()
        joined = "\n".join(texts)
        for fragment in ("almost-kiss",
                         "the rumour about my marriage",
                         "the street drummer",
                         "lost their nerve",
                         "the chance to ask them out",
                         "my driving test",
                         "to my ex",
                         "the almost-affair",
                         "the in-laws' jab",
                         "theory test"):
            self.assertIn(fragment, joined, fragment)


class OutcomeValidatorTests(unittest.TestCase):
    """Validator-style sweep (BACKLOG 2026-11-21): every outcome_text in
    the event content must be self-explanatory in the journal — at least
    MIN_WORDS words, and a line opening with a bare pronoun (I/They/...)
    must clear PRONOUN_MIN_WORDS so it never reads as dangling context
    (e.g. 'I gave them nothing and kept my smile.')."""

    MIN_WORDS = 6
    PRONOUN_MIN_WORDS = 8
    BARE = ("i", "they", "he", "she", "we", "it", "them", "you")

    def _all_outcome_texts(self):
        root = os.path.join(ROOT, "src", "game", "content", "events")
        texts = []
        for name in sorted(os.listdir(root)):
            if not name.endswith(".json"):
                continue
            with open(os.path.join(root, name), encoding="utf-8") as f:
                data = json.load(f)
            def walk(node):
                if isinstance(node, dict):
                    if "outcome_text" in node and node["outcome_text"]:
                        texts.append((name, node["outcome_text"]))
                    for v in node.values():
                        walk(v)
                elif isinstance(node, list):
                    for v in node:
                        walk(v)
            walk(data)
        return texts

    def test_all_outcomes_meet_minimum_length(self):
        texts = self._all_outcome_texts()
        self.assertGreater(len(texts), 100)
        for name, text in texts:
            wc = len(text.split())
            self.assertGreaterEqual(wc, self.MIN_WORDS,
                                    "%s: %r too short (%d words)"
                                    % (name, text, wc))

    def test_no_bare_pronoun_openers_without_context(self):
        for name, text in self._all_outcome_texts():
            first = text.split(None, 1)[0].strip(",.?!'").lower()
            if first in self.BARE:
                wc = len(text.split())
                self.assertGreaterEqual(wc, self.PRONOUN_MIN_WORDS,
                                        "%s: %r opens with a bare pronoun "
                                        "but is only %d words"
                                        % (name, text, wc))


if __name__ == "__main__":
    unittest.main()
