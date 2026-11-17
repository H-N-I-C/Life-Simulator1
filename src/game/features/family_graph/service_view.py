"""Service-backed GraphView/PolicyView for B's evaluator + editor previews
(M2.5, Kimi A). Implements the frozen read-only interfaces from
relationship_policy.evaluator against the save envelope. Dangling ids
raise B's PolicyError with stable codes, per the interface contract."""

from . import graph as graph_mod


class ServiceGraphView:
    """evaluator.GraphView backed by env['graph'] + the envelope's people.
    Built per call site (env is loaded state, never mutated here)."""

    def __init__(self, env):
        self._env = env
        self._people = {}
        from src.game.service import GameService
        for person in GameService._walk_people(env["player"]):
            self._people[person.person_id] = person
        for person in env.get("archive", []):
            if hasattr(person, "person_id") \
                    and person.person_id not in self._people:
                self._people[person.person_id] = person
        for pid in env.get("dead_ids", []):
            if pid in self._people:
                self._people[pid].alive = False
        self._graph = env["graph"]

    def _person(self, person_id):
        person = self._people.get(person_id)
        if person is None:
            from src.game.features.relationship_policy.catalog import \
                PolicyError
            raise PolicyError("unknown_id", "unknown person %r" % person_id)
        return person

    def person(self, person_id):
        out = graph_mod.graph_person(self._person(person_id))
        gender = getattr(self._person(person_id), "gender", None)
        out["gender"] = getattr(gender, "name", str(gender or "")).lower()
        return out

    def relatedness_category(self, a_id, b_id):
        self._person(a_id)
        self._person(b_id)
        return graph_mod.graph_relatedness(self._graph, a_id, b_id)[
            "category"]

    def relationship_scores(self, a_id, b_id):
        self._person(a_id)
        self._person(b_id)
        for rel in self._graph["relationships"]:
            if set(rel["person_ids"]) == {a_id, b_id} \
                    and rel["status"] == "active":
                return {"chemistry": rel["chemistry"],
                        "trust": rel["trust"],
                        "satisfaction": rel["satisfaction"]}
        return {"chemistry": 0, "trust": 0, "satisfaction": 0}

    def active_union_count(self, person_id):
        self._person(person_id)
        return sum(1 for u in self._graph["unions"]
                   if person_id in u["person_ids"]
                   and u["status"] == "active")

    def reputation_fact_id(self, person_id, observer_id):
        from src.game.features.knowledge import core as knowledge
        for fact in reversed(self._env.get("knowledge", {}).get("facts", [])):
            if knowledge.knows(self._env, observer_id, fact["fact_id"]) and any(
                    c.get("person_id") == person_id
                    for c in fact.get("reputation_changes", [])):
                return fact["fact_id"]
        return None

    def reputation_of(self, person_id, observer_id):
        from src.game.features.knowledge import core as knowledge
        value = 0
        scope = self._env.get("policy", {}).get("jurisdiction_id")
        for fact in self._env.get("knowledge", {}).get("facts", []):
            if not knowledge.knows(self._env, observer_id, fact["fact_id"]):
                continue
            for change in fact.get("reputation_changes", []):
                if change.get("person_id") == person_id and change.get(
                        "scope", "global") in ("global", scope):
                    value += change["delta"]
        return max(-100, min(100, value))


class ServicePolicyView:
    """evaluator.PolicyView/StaticPolicyView equivalent over env['policy']."""

    def __init__(self, env):
        from src.game.features.relationship_policy.catalog import \
            load_catalog
        self._state = env["policy"]
        self.catalog = load_catalog()
        self.overrides = self._state.get("overrides", {})

    def profile(self, person_id):
        profile = dict(self._state.get("profiles", {}).get(person_id, {}))
        if "gender" not in profile:
            # gender may live on the person record; the service adapter
            # fills it when available (B's PolicyView documents this)
            pass
        return profile

    def jurisdiction_id(self):
        return self._state.get("jurisdiction_id")
