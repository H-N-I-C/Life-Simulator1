"""Legacy life-milestone journal lines (Kimi C, M BACKLOG polish).

The legacy milestone strings in src/menus/rule_actions.py ("I am
starting middle school", "I am now enrolled in university", ...) were
flat, inconsistent about periods, and second-person in one place. These
are the replacements: 2-4 ORIGINAL first-person variants per milestone,
same fact, flavour-voice. Same id/band/text pool shape as the other
journal pools — the rule_actions call sites draw through C's frozen
pick_year_line via pool= (cosmetic stream, no-repeat window persisted
on the player object, undo-safe via the envelope).

Facts are frozen per situation: a variant may colour the moment, never
change what happened.
"""

BANDS = ("baby", "toddler", "child", "teen", "adult", "elder")

_TEXTS = {
    "start_elementary": [
        "I started elementary school this year, brand-new shoes and a "
        "backpack bigger than me.",
        "Elementary school began for me with a solemn first day and a "
        "packed lunch I had supervised heavily.",
        "I began elementary school, and immediately decided the "
        "sandpit was the best room in the building.",
    ],
    "start_middle": [
        "I started middle school this year, and the hallways are a "
        "maze I intend to conquer.",
        "Middle school began, bringing lockers, multiple teachers, and "
        "a timetable I have already lost twice.",
        "I moved up to middle school, where the big kids look enormous "
        "and the homework looks worse.",
        "This year I started middle school, trading one classroom for "
        "seven and my confidence for a map.",
    ],
    "start_high": [
        "I started high school this year, which everyone assures me "
        "counts for something.",
        "High school began for me this year, and with it came "
        "older students, harder subjects, and a strange new freedom.",
        "I began high school quietly determined to be a different "
        "person by the end of it.",
        "This year I walked into high school for the first time, "
        "pretending the nerves were excitement.",
    ],
    "graduate_high": [
        "I graduated from high school, and the whole family behaved as "
        "if I had won something, which perhaps I had.",
        "High school is behind me now; I graduated with a certificate, "
        "a sunburn from the ceremony, and real relief.",
        "I finished high school this year, and the last bell felt "
        "different from every bell before it.",
    ],
    "enroll_university": [
        "I am now enrolled at university, a place with far too many "
        "buildings and exactly one café I have memorised.",
        "University enrollment happened this year, and I celebrated "
        "by buying a notebook I may never open.",
        "I enrolled at university, where the lectures are bigger, the "
        "deadlines are real, and nobody checks my homework.",
        "This year I became a university student, which mostly means "
        "carrying more books and opinions than before.",
    ],
    "graduate_university": [
        "I graduated from university, degree in hand and absolutely no "
        "idea what Tuesday looks like now.",
        "University is done: I graduated this year and immediately "
        "missed the library I complained about for four years.",
        "I finished my degree at university and graduated, which felt "
        "less like an ending and more like being gently pushed off a "
        "dock.",
    ],
    "expelled_university": [
        "I was expelled from university after my grades collapsed, and "
        "I am still deciding what the lesson was.",
        "University expelled me this year over grades I would also "
        "have expelled me over. It stings honestly.",
        "My university expelled me for bad results, and I collected "
        "my things from the library desk in silence.",
    ],
    "loan_repayment": [
        "My student loan repayments have now begun for me, a monthly "
        "reminder that education was never free.",
        "I now have to start paying back my student loan, which makes "
        "the degree feel heavier somehow.",
        "The student loan bills have arrived, and my salary and I are "
        "about to get much better acquainted.",
    ],
    "scholarship_rejected": [
        "My scholarship application was rejected, and the polite "
        "letter did not make the no any softer.",
        "The scholarship people rejected me this year, so the "
        "university dream got a price tag instead of a discount.",
    ],
    "parents_refused": [
        "My parents refused to pay my university tuition, and the "
        "conversation about it was the loudest quiet dinner we have "
        "had in years.",
        "My parents refused the university money, with love and "
        "absolutely no cheque attached.",
    ],
    "funding_refused": [
        "My request for university funding was refused, and the "
        "envelope went back in the drawer while I reconsidered.",
        "The funding I asked for was refused, so university would "
        "have to wait for a better plan.",
    ],
    "university_rejected": [
        "My application to university was rejected, and I pretended "
        "the letter did not bother me for almost a whole day.",
        "The university rejected me this year, in very polite "
        "language that did not soften it at all.",
        "I was rejected by the university, which turned out to be a "
        "door closing loudly enough to hear the other one open.",
    ],
}


def _build(texts, prefix):
    out = []
    for i, text in enumerate(texts):
        for band in BANDS:
            out.append({"id": f"msl_{prefix}_{band}_{i}",
                        "band": band, "text": text})
    return out


POOLS = {name: _build(texts, name) for name, texts in _TEXTS.items()}


def lines_for(situation):
    """Pool (id/band/text) for pick_year_line's pool= argument."""
    pool = POOLS.get(situation)
    if pool is None:
        raise KeyError("unknown milestone situation: %r" % (situation,))
    return pool


def situations():
    return tuple(_TEXTS)
