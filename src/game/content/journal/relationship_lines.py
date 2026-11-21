"""Relationship-drift journal lines (Kimi A, M3 amendment 5).

Original first-person lines for the yearly parent/partner relationship
drift entry (coordinator._relations_age). Selected through C's frozen
pick_year_line machinery (passed as the `pool` argument — see the M3
contracts amendment), so the no-repeat window and cosmetic-only RNG
guarantees are shared, not re-implemented.

Texts use a {name} placeholder formatted with the relation's first name
after selection. Direction (improved/worsened) is a per-call pool split.
"""

BANDS = ("baby", "toddler", "child", "teen", "adult", "elder")

_IMPROVED = [
    "My relationship with {name} grew stronger this year.",
    "{name} and I understand each other better these days.",
    "I feel closer to {name} than I did before.",
    "I made more time for {name} this year, and it showed.",
    "{name} has really been there for me lately.",
    "{name} and I laughed a lot together this year.",
]

_WORSENED = [
    "{name} and I have been drifting apart.",
    "Things have been strained between me and {name}.",
    "I argued with {name} more than usual this year.",
    "I did not see much of {name} this year, and I felt it.",
    "{name} and I kept misunderstanding each other.",
]

# Age-fitting (review backlog): baby/toddler bands get sensory,
# pre-verbal bonding lines — never grown-up relationship language.
_YOUNG_IMPROVED = [
    "{name} held me close when I cried this year.",
    "{name}'s cuddles are my favourite place in the world.",
    "I giggle every time {name} makes funny faces at me.",
    "{name} sang me to sleep more nights than I can count.",
    "When {name} comes home, my whole day gets better.",
]

_YOUNG_WORSENED = [
    "{name} seemed too busy for me this year.",
    "I cried more when {name} was away.",
    "{name} did not hold me as much as I wanted this year.",
    "I felt shy and unsure around {name} for a while.",
    "Too many long days without {name} this year.",
]


def _build(texts, young, prefix):
    out = []
    for band in BANDS:
        band_texts = young if band in ("baby", "toddler") else texts
        for i, text in enumerate(band_texts):
            out.append({"id": f"rel_{prefix}_{band}_{i}", "band": band,
                        "text": text})
    return out


IMPROVED_LINES = _build(_IMPROVED, _YOUNG_IMPROVED, "imp")
WORSENED_LINES = _build(_WORSENED, _YOUNG_WORSENED, "wor")


def lines_for(delta):
    """Direction-specific pool for a relationship change."""
    return IMPROVED_LINES if delta > 0 else WORSENED_LINES
