"""Relation-type-specific relationship-drift journal lines (Kimi C).

Varied first-person lines for the yearly relationship-drift journal entry,
split by relation role (mother, father, sibling, partner, child) and by
direction (improved / worsened). Same entry shape as Kimi A's
relationship_lines.py (id/band/text), so the pools plug straight into the
frozen pick_year_line selector via its `pool` argument:

    pool = relationship_flavour.lines_for(delta, role)
    picked = pick_year_line(player, None, recents, cosmetic_rng, pool=pool)

Role resolution is the caller's job (A's coordinator hook): lines_for
expects a role string from ROLES. Texts use a {name} placeholder formatted
with the relation's first name after selection.
"""

BANDS = ("baby", "toddler", "child", "teen", "adult", "elder")

ROLES = ("mother", "father", "sibling", "partner", "child")

_IMPROVED = {
    "mother": [
        "Mum and I talked for hours this year; I felt really heard.",
        "I helped Mum out more around the house and we grew closer.",
        "Mum's advice this year steered me right, and I told her so.",
        "I called Mum just to chat more often, and it made us closer.",
        "Mum and I cooked my favourite meal together and laughed the whole time.",
        "I finally opened up to Mum about my life, and she understood.",
    ],
    "father": [
        "Dad and I spent a whole afternoon just talking, and it meant a lot.",
        "I asked Dad to teach me something he knows, and we bonded over it.",
        "Dad backed me up when it mattered this year; I felt his pride.",
        "Dad and I went out together and it felt like old times.",
        "I made more effort with Dad this year, and he noticed.",
        "Dad's quiet support carried me through a hard stretch this year.",
    ],
    "sibling": [
        "{name} and I teamed up this year instead of bickering, and it was great.",
        "I actually confided in {name} for once, and they really came through.",
        "{name} and I rebuilt the easy friendship we had as kids.",
        "I stood up for {name} when it counted, and we got closer.",
        "{name} and I started a little tradition of our own this year.",
        "We had our differences, but {name} and I ended the year as real friends.",
    ],
    "partner": [
        "{name} and I made time for each other this year, and it showed.",
        "I fell for {name} all over again this year.",
        "{name} supported me through a rough patch, and I felt so loved.",
        "{name} and I planned something special together and it brought us closer.",
        "We talked honestly about our future, and {name} and I feel stronger for it.",
        "Little everyday kindnesses from {name} reminded me why I chose them.",
        "{name} and I laughed more this year than we had in ages.",
    ],
    "child": [
        "I watched {name} grow this year and felt so proud to be their parent.",
        "{name} and I found a new activity we both love.",
        "I made sure to be at {name}'s big moments this year, and it mattered.",
        "{name} came to me with something important, and I was there.",
        "Bedtime stories with {name} became the best part of my day.",
        "I taught {name} something new this year and loved every minute.",
    ],
}

_WORSENED = {
    "mother": [
        "Mum and I kept butting heads this year, and the distance grew.",
        "I brushed off Mum's calls more than I should have, and I felt the gap.",
        "Mum disapproved of my choices this year, and it stung.",
        "An old argument with Mum flared up again; we never quite settled it.",
        "I felt like Mum didn't really see me anymore.",
    ],
    "father": [
        "Dad and I barely spoke this year, and I missed how we used to be.",
        "Dad and I argued about my future, and it left a mark.",
        "I kept things from Dad this year because I expected judgment, not help.",
        "Dad was distant this year, and I stopped reaching out.",
        "A harsh word from Dad stuck with me longer than it should have.",
    ],
    "sibling": [
        "{name} and I fell out this year and never properly made up.",
        "Old rivalries with {name} resurfaced, and we drifted apart.",
        "I felt invisible next to {name} at every family gathering this year.",
        "{name} let me down when I needed them, and I've kept my distance.",
        "We barely talk anymore, {name} and I; life just pulled us apart.",
    ],
    "partner": [
        "{name} and I have been walking on eggshells all year.",
        "Small resentments piled up between {name} and me this year.",
        "We stopped making time for each other, and {name} and I felt it.",
        "{name} and I had the same fight again, and nothing changed.",
        "I went to bed angry next to {name} more times than I can count.",
        "The silence between {name} and me grew louder this year.",
    ],
    "child": [
        "{name} and I clashed more this year as they stretched their wings.",
        "I worried I was too hard on {name} this year.",
        "{name} pulled away from me, and I didn't know how to close the gap.",
        "I missed {name}'s important moments, and I could see the hurt.",
        "{name} stopped telling me about their day, and I felt shut out.",
    ],
}


# Age-true baby/toddler bands (0-4): attachment and sensory truth only.
# The grown-up lists below serve child..elder; a one-year-old cannot
# have helped around the house or shortened their phone calls.
_GROWN_BANDS = ("child", "teen", "adult", "elder")
_BABY_BANDS = ("baby", "toddler")

_IMPROVED_BABY = {
    "mother": [
        "Mum's face is my favourite view in any room.",
        "I reached up for Mum and she scooped me up; the whole day was "
        "safe again.",
        "I learned that Mum comes back when she goes away, and I clap "
        "every single time she does.",
    ],
    "father": [
        "Dad's shoulders are the best seat in the house.",
        "I grabbed Dad's finger and held on like it was the whole "
        "world, which to me it nearly is.",
        "Dad makes a noise like an engine and I laugh so hard I fall "
        "over.",
    ],
    "sibling": [
        "{name} smiled at me today, and I have decided the small loud "
        "one can stay.",
        "{name} shared a biscuit with me by chewing it first, which I "
        "choose to count as sharing.",
    ],
    "partner": [
        "{name} picked me up and the whole world smelled right "
        "again.",
        "{name} sang the wobbly song and I stopped crying mid-scream, "
        "out of respect.",
    ],
    "child": [
        "{name} carried me past the scary dark hallway and "
        "everything was fine.",
        "{name} tickled my toes and I kicked like a happy frog.",
    ],
}

_WORSENED_BABY = {
    "mother": [
        "Mum left the room and I cried like the sun had gone out.",
        "I wanted Mum and got the babysitter, so I filed a complaint "
        "in screams.",
    ],
    "father": [
        "Dad went to work and I watched the door for a very long "
        "time, unimpressed.",
        "I reached for Dad and he was holding the groceries instead, "
        "and I let him know my feelings.",
    ],
    "sibling": [
        "{name} knocked down my tower and relations remain frosty.",
        "{name} grabbed my toy and I responded with the full force of "
        "my tiny lungs.",
    ],
    "partner": [
        "{name} put me down for a nap and I objected in the "
        "strongest possible terms.",
        "{name} said goodnight and left, and I sang about my sadness "
        "until sleep won.",
    ],
    "child": [
        "{name} told me no and I tested the no thoroughly, at "
        "volume.",
        "{name} took away the thing I was not supposed to chew, and I "
        "mourned it loudly.",
    ],
}


def _build(texts_by_role, prefix, bands=BANDS):
    out = []
    for role, texts in texts_by_role.items():
        for band in bands:
            for i, text in enumerate(texts):
                out.append({"id": f"relf_{prefix}_{role}_{band}_{i}",
                            "band": band, "text": text})
    return out


IMPROVED_LINES = _build(_IMPROVED, "imp", _GROWN_BANDS) +     _build(_IMPROVED_BABY, "impbaby", _BABY_BANDS)
WORSENED_LINES = _build(_WORSENED, "wor", _GROWN_BANDS) +     _build(_WORSENED_BABY, "worbaby", _BABY_BANDS)


def lines_for(delta, role):
    """Direction- and role-specific pool for a relationship change.

    delta > 0 -> improved pool, else worsened. Unknown role falls back to
    the generic sibling-set lines (never None, so the caller always has a
    pool to offer the selector)."""
    if role not in ROLES:
        role = "sibling"
    pool = IMPROVED_LINES if delta > 0 else WORSENED_LINES
    return [l for l in pool if f"_{role}_" in l["id"]]
