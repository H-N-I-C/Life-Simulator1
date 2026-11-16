"""Contextual decision templates for the M1 web slice.

Each template defines an age window, a sampling weight, and options with
deterministic, bounded effects. Effects keys: happiness, health, smarts,
looks, money (player), relationship (applied to person_ids).
"""

TEMPLATES = [
    {
        "id": "new_friend",
        "min_age": 4,
        "max_age": 9,
        "weight": 3,
        "title": "A new friend",
        "text": "A kid from the neighbourhood invites {name} to play in the park.",
        "person_ids": [],
        "options": [
            {"id": "o1", "label": "Go play",
             "known_effects": ["+happiness", "+health"],
             "outcome": "{name} went to the park with a new friend.",
             "effects": {"happiness": 8, "health": 3}},
            {"id": "o2", "label": "Stay home",
             "known_effects": ["+smarts", "-happiness"],
             "outcome": "{name} stayed home and read a picture book.",
             "effects": {"smarts": 4, "happiness": -2}},
        ],
    },
    {
        "id": "science_fair",
        "min_age": 7,
        "max_age": 12,
        "weight": 2,
        "title": "The school science fair",
        "text": "{name}'s school is holding a science fair and a teacher encourages {name} to enter.",
        "person_ids": [],
        "options": [
            {"id": "o1", "label": "Build an entry",
             "known_effects": ["+smarts", "-happiness"],
             "outcome": "{name} built a baking-soda volcano and took third place.",
             "effects": {"smarts": 7, "happiness": -3}},
            {"id": "o2", "label": "Skip it",
             "known_effects": ["+happiness", "-smarts"],
             "outcome": "{name} skipped the fair and played instead.",
             "effects": {"happiness": 5, "smarts": -2}},
        ],
    },
    {
        "id": "party_or_study",
        "min_age": 13,
        "max_age": 17,
        "weight": 2,
        "title": "Party night",
        "text": "A big party is on the same night {name} should be studying for an exam.",
        "person_ids": [],
        "options": [
            {"id": "o1", "label": "Stay in and study",
             "known_effects": ["+smarts", "-happiness"],
             "outcome": "{name} studied all night and aced the exam.",
             "effects": {"smarts": 8, "happiness": -4}},
            {"id": "o2", "label": "Go to the party",
             "known_effects": ["+happiness", "-health", "-smarts"],
             "outcome": "{name} went to the party and had a great night.",
             "effects": {"happiness": 9, "health": -3, "smarts": -3}},
        ],
    },
    {
        "id": "old_friend_loan",
        "min_age": 18,
        "max_age": 60,
        "weight": 2,
        "title": "An old friend in need",
        "text": "An old friend asks {name} to lend them $500 to get through a rough patch.",
        "person_ids": [],
        "options": [
            {"id": "o1", "label": "Lend the money",
             "known_effects": ["-money", "+happiness"],
             "outcome": "{name} lent the money and felt good about helping.",
             "effects": {"money": -500, "happiness": 6}},
            {"id": "o2", "label": "Say no",
             "known_effects": ["-happiness"],
             "outcome": "{name} politely declined to lend the money.",
             "effects": {"happiness": -2}},
        ],
    },
    {
        "id": "healthy_lifestyle",
        "min_age": 25,
        "max_age": 65,
        "weight": 2,
        "title": "A health scare",
        "text": "{name}'s doctor advises a healthier lifestyle before things get worse.",
        "person_ids": [],
        "options": [
            {"id": "o1", "label": "Join a gym",
             "known_effects": ["+health", "-money"],
             "outcome": "{name} joined a gym and started feeling stronger.",
             "effects": {"health": 9, "money": -300, "happiness": 3}},
            {"id": "o2", "label": "Ignore the advice",
             "known_effects": ["-health"],
             "outcome": "{name} ignored the doctor's advice.",
             "effects": {"health": -6}},
        ],
    },
    {
        "id": "family_reunion",
        "min_age": 50,
        "max_age": 120,
        "weight": 2,
        "title": "The family reunion",
        "text": "{name}'s family is organising a reunion and hopes {name} will come.",
        "person_ids": "parents",
        "options": [
            {"id": "o1", "label": "Attend the reunion",
             "known_effects": ["+happiness", "+relationship"],
             "outcome": "{name} attended the reunion and reconnected with family.",
             "effects": {"happiness": 10, "relationship": 8}},
            {"id": "o2", "label": "Skip it",
             "known_effects": ["-happiness"],
             "outcome": "{name} skipped the reunion and stayed home.",
             "effects": {"happiness": -3}},
        ],
    },
]


def eligible(template, age, used_ids):
    return (template["min_age"] <= age <= template["max_age"]
            and template["id"] not in used_ids)


def sample(rng, age, used_ids, alive_parent_ids):
    """Pick a decision template for this year, or None.

    Weighted sampling over eligible templates; each template fires at most
    once per lifetime. Templates that reference living parents are skipped
    when no parent is alive.
    """
    candidates = []
    for t in TEMPLATES:
        if not eligible(t, age, used_ids):
            continue
        if t["person_ids"] == "parents" and not alive_parent_ids:
            continue
        candidates.append(t)
    if not candidates:
        return None
    if rng.randrange(3) != 0:  # ~1 in 3 years with an eligible template
        return None
    total = sum(t["weight"] for t in candidates)
    roll = rng.randrange(total)
    for t in candidates:
        roll -= t["weight"]
        if roll < 0:
            return t
    return candidates[-1]


def instantiate(template, seq, name, parent_ids):
    """Build a pending-decision record with concrete IDs."""
    person_ids = list(parent_ids) if template["person_ids"] == "parents" else []
    return {
        "id": f"d_{seq}",
        "template_id": template["id"],
        "title": template["title"],
        "text": template["text"].format(name=name),
        "person_ids": person_ids,
        "options": [
            {
                "id": f"d_{seq}_{opt['id']}",
                "label": opt["label"],
                "known_effects": list(opt["known_effects"]),
                "_outcome": opt["outcome"],
                "_effects": dict(opt["effects"]),
            }
            for opt in template["options"]
        ],
    }


def option_cost(option):
    """Positive cost of an option, 0 when free. Accepts both the template
    shape (effects) and the instantiated pending shape (_effects)."""
    effects = option.get("_effects", option.get("effects", {}))
    return max(0, -effects.get("money", 0))


def has_free_option(template):
    """Soft-lock rule: every decision must keep at least one option that is
    always available (no cost), e.g. a decline/do-nothing path."""
    return any(option_cost(o) == 0 for o in template["options"])


def validate_templates():
    """Structural soft-lock check over all templates."""
    bad = [t["id"] for t in TEMPLATES if not has_free_option(t)]
    if bad:
        raise ValueError(
            f"decision templates without a free option: {bad}")
