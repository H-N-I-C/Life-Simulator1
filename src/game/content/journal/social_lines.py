"""Social journal flavour pools for A's M4 drift (Kimi C).

Original first-person lines for the social situations A's yearly drift
applier journals: friend made, friendship grew, best friend, drifting
apart, falling out, reconciled, rival, ex-classmate reunion and
coworker friend. Same entry shape as the other journal pools
(id/band/text) so A's drift draws through C's frozen pick_year_line via
its `pool=` argument:

    pool = social_lines.lines_for("friendship_grew")
    picked = pick_year_line(player, None, recents, cosmetic_rng,
                            pool=pool)

Texts are original, age-banded (6 bands, replicated), carry a {name}
placeholder for the social person (formatted by the caller), and never
assert unrecorded facts — the SITUATION (chosen by the drift code from
real state) supplies the truth, the line only colours it.
"""

BANDS = ("baby", "toddler", "child", "teen", "adult", "elder")

_TEXTS = {
    "friend_made": [
        "I clicked with {name} this year the way you click with "
        "people who make ordinary days slightly better.",
        "A shared enemy, deadlines, made friends of {name} and me "
        "faster than any party could have.",

        "I made a new friend in {name}, starting with a small brave hello.",
        "{name} and I got talking and discovered we laugh at the same "
        "things, which is the whole foundation, really.",
        "A new friendship with {name} began this year out of nothing "
        "fancy, just shared time and shared snacks.",
        "I count {name} as a friend now, and I think they count me too, "
        "which is the important bit.",
        "Somewhere between the jokes and the waiting around, {name} "
        "became my friend without either of us announcing it.",
        "{name} shared their umbrella with me once and loyalty was "
        "instantly sealed.",
        "I decided this year that {name} is my kind of person, and said "
        "so out loud, which sped things along nicely.",
        "New friends are rare currency as you get older, so I treasure "
        "the one I made in {name}.",
    ],
    "friendship_grew": [
        "{name} remembered the small thing I told them once, and that "
        "quietly moved the friendship up a level.",

        "{name} and I got closer this year, the easy way, by simply "
        "showing up for each other.",
        "My friendship with {name} deepened over long chats and small "
        "favours, which is how the real ones grow.",
        "I understand {name} better than I did before, and I like what "
        "I understand.",
        "{name} and I built one of those friendships where silence is "
        "just as comfortable as conversation.",
        "This year {name} became one of the people I tell things to "
        "first, which says everything.",
        "We are not dramatic about it, but {name} and I are properly "
        "good friends now, and it shows.",
        "I made more time for {name} this year and the friendship "
        "repaid every minute of it.",
    ],
    "best_friend": [
        "{name} knows my coffee order, my worst habits and my best "
        "stories, which is what best friends are for.",
        "This year sealed it: {name} is my best friend, my first call "
        "and my favourite witness.",

        "{name} is my best friend now, and I would defend that title "
        "in any court.",
        "If I had to name one person who knows the real me, it would "
        "be {name}, my best friend.",
        "{name} and I passed some unofficial test this year and "
        "became best friends, no paperwork required.",
        "My best friend {name} has seen me at my worst and stayed, "
        "which is the entire job description.",
        "I realised {name} is my best friend when telling them good "
        "news felt better than the news itself.",
    ],
    "drifting_apart": [
        "I saw {name} across a crowded room this year and we both "
        "pretended not to, which told me plenty.",
        "The calendar beat my friendship with {name}; we simply never "
        "found the week to meet.",

        "{name} and I have been drifting apart, and neither of us has "
        "quite said it out loud.",
        "Life got busy and my friendship with {name} got quiet, and I "
        "feel the distance.",
        "I miss how close {name} and I used to be, even though neither "
        "of us did anything wrong.",
        "The messages with {name} got shorter this year, and so, "
        "quietly, did the friendship.",
        "{name} and I are still friendly, but the easy closeness has "
        "gone somewhere I cannot find.",
    ],
    "falling_out": [
        "{name} and I drew a line under our friendship with a "
        "conversation neither of us enjoyed.",

        "{name} and I had a proper falling out this year, loud enough "
        "for the neighbours to learn our business.",
        "My friendship with {name} broke on a stupid argument, the "
        "kind that never feels stupid at the time.",
        "{name} and I stopped speaking after what happened, and the "
        "silence is its own conversation.",
        "I lost {name} as a friend this year over a disagreement that "
        "grew teeth we never meant it to have.",
        "Things with {name} ended badly, and I am still carrying the "
        "shape of it around.",
    ],
    "reconciled": [
        "Time did most of the healing with {name}; I just showed up "
        "and said the rest out loud.",
        "My friendship with {name} survived its worst year, which "
        "makes it worth more, not less.",

        "{name} and I made peace this year, and the apology felt "
        "lighter than the grudge ever did.",
        "After far too long, {name} and I picked up our friendship "
        "like it had only been paused.",
        "I reconciled with {name} over a long talk and an even longer "
        "silence, and both were necessary.",
        "Forgiving {name}, and being forgiven, turned out to be the "
        "easiest hard thing I did all year.",
        "{name} and I agreed to let the old argument die, and "
        "remarkably, it did.",
        "We shook hands on it, {name} and I, and the friendship "
        "walked away from the wreckage intact.",
    ],
    "rival": [
        "{name} edged ahead of me again this year, and I grudgingly "
        "admitted it made my own effort better.",

        "{name} remains my rival, and honestly the competition keeps "
        "both of us sharper.",
        "My rivalry with {name} flared up again this year, all "
        "friendly fire and scorekeeping.",
        "{name} and I are rivals in the ancient way: we pretend it is "
        "about the prize and know it is about each other.",
        "Nothing motivates me quite like {name} being slightly ahead, "
        "and this year they were.",
        "I scored one over my rival {name} this year and celebrated "
        "with the maturity of a game-show winner.",
    ],
    "reunion": [
        "An old classmate, {name}, turned up at a gathering and the "
        "years fell off us both within minutes.",

        "I ran into {name}, an old classmate, and we picked up "
        "mid-sentence like the years were a commercial break.",
        "An old school friend, {name}, came back into my life this "
        "year, and the reunion was pure delight.",
        "I attended a school reunion and spent the whole evening "
        "laughing with {name} about people we no longer are.",
        "{name} from my school days found me again, and we traded "
        "stories until the café threw us out.",
        "Reuniting with {name} reminded me that some friendships "
        "merely sleep, they never die.",
    ],
    "coworker_friend": [
        "{name} covered for me on a bad day without being asked, and "
        "that is the day coworkers became friends.",

        "{name} from work became a real friend this year, which "
        "started with shared complaints and ended with shared lunches.",
        "My coworker {name} is the reason some shifts felt shorter "
        "than they were.",
        "I never expected to find a friend at work, but {name} "
        "happened to me anyway.",
        "{name} and I bonded over the shared chaos of our workplace, "
        "the strongest glue there is.",
        "A colleague became a confidant this year: {name}, who knows "
        "exactly what my days are like.",
        "Work gave me a wage and, unexpectedly, a friend in {name}; "
        "the second was the better benefit.",
    ],
}


# Social drift serves child..elder only: there is no social graph at
# 0-4 (A's circles start at school age), so baby/toddler carry no lines.
_SOCIAL_BANDS = ("child", "teen", "adult", "elder")


def _build(texts, prefix):
    out = []
    for i, text in enumerate(texts):
        for band in _SOCIAL_BANDS:
            out.append({"id": f"socl_{prefix}_{band}_{i}",
                        "band": band, "text": text})
    return out


POOLS = {name: _build(texts, name) for name, texts in _TEXTS.items()}


def lines_for(situation):
    """Direction/situation-specific pool for A's drift journal entry.

    situation one of: friend_made, friendship_grew, best_friend,
    drifting_apart, falling_out, reconciled, rival, reunion
    (ex-classmate), coworker_friend. Returns the (id/band/text) pool
    for pick_year_line's `pool=` argument; the caller formats {name}."""
    pool = POOLS.get(situation)
    if pool is None:
        raise KeyError("unknown social journal situation: %r"
                       % (situation,))
    return pool


def situations():
    return tuple(_TEXTS)
