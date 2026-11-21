"""Transaction-local rule context for C's headless rule boundary.

Built fresh for each command by GameService from the in-memory envelope.
Duck-typed fields per the frozen phase map / rule-examples; rule code never
imports GameService (no cycles). All reads/writes are against the live
envelope objects — the service discards them on failed saves, which is the
rollback story.
"""

import uuid


class RuleContext:
    def __init__(self, env, service):
        self._env = env
        self._service = service
        turn = env.setdefault("turn", {})
        if not turn.get("turn_id"):
            turn["turn_id"] = f"t_{env['revision'] + 1:06d}"
        if "cursor" not in turn:
            turn["cursor"] = {"phase_id": "year_start", "item_cursor": 0}
        if "done" not in turn:
            turn["done"] = {}
        self.player = env["player"]
        self.turn_id = turn["turn_id"]
        self.cursor = turn["cursor"]
        self.done_markers = turn["done"]

    # -- people ---------------------------------------------------------

    @property
    def people_by_id(self):
        by_id = {}
        for person in self._service._walk_people(self.player):
            by_id[person.person_id] = person
        for person in self._env.get("archive", []):
            if person.person_id not in by_id:
                by_id[person.person_id] = person
        # M4 social NPCs are canonical people too (resolve/bindings/
        # person actions must see them exactly like family members)
        for person in self._env.get("social_people", []):
            if getattr(person, "person_id", None) \
                    and person.person_id not in by_id:
                by_id[person.person_id] = person
        return by_id

    # -- annual flags -----------------------------------------------------

    @property
    def annual_flags(self):
        return self._env["year_actions"]

    def mark_done(self, phase_id, marker):
        self.done_markers.setdefault(phase_id, [])
        if marker not in self.done_markers[phase_id]:
            self.done_markers[phase_id].append(marker)

    def is_done(self, phase_id, marker):
        return marker in self.done_markers.get(phase_id, [])

    # -- versioned data ---------------------------------------------------

    @property
    def data(self):
        return {"version": 2}

    @property
    def turn_snapshot(self):
        """person_ids that receive yearly updates this turn (frozen at
        year_start; newborns are excluded until next year)."""
        return list(self._env.get("turn", {}).get("snapshot", []))

    # -- person creation hook (canonical identity/appearance) --------------

    def new_person_id(self):
        return str(uuid.uuid4())
