"""Event data-file engine (loader for the M2c content pipeline; C-owned
per 00-TEAM.md 2026-11-16, schema v2.1).

Loads event definitions from src/game/events/*.json (bundled fit-proof)
and src/game/content/events/*.json (C content) per the frozen
event-schema.json (v2.1: auto events, sign, branches field_refs,
sampled_at_offer, chance_n), validates the structural invariants
(including the soft-lock rule for non-auto events), evaluates conditions
against the transaction-local context, and instantiates pending decisions
with the same shape the service resolver consumes. All randomness is
sampled once at offer creation; resolve never rerolls.
"""

import json
import os
import random

EVENTS_DIR = os.path.join(os.path.dirname(__file__), "events")
CONTENT_DIR = os.path.join(os.path.dirname(__file__), "content", "events")

# Declared legacy order for independent annual checks (finding-round #5):
# the order the gates appear in Player.age_up/random_events. chance_n
# events are evaluated in THIS order, separately from the flavor pool.
LEGACY_CHANCE_ORDER = [
    "lightning_strike",
    "partner_breakup_offer",
    "partner_divorce_offer",
    "partner_kidnapping",
    "dating_offer",
    "thanatophobia_worry",
    "vaccination_age2",
]

STAT_KEYS = ("happiness", "health", "smarts", "looks", "karma", "stress")
CONDITION_KINDS = {
    "alive", "has_illness", "employed", "in_school", "min_stat",
    "min_money", "flag", "no_partner", "partner_status",
    "min_age_sampled", "max_age_sampled", "partner_relationship_below",
    "karma_gate",
}


class EventValidationError(Exception):
    """An event file violates the schema invariants."""


def validate_event(ev):
    """Structural invariants per event-schema.json v2.1."""
    for key in ("id", "title", "text"):
        if key not in ev:
            raise EventValidationError(f"event missing {key!r}")
    if not isinstance(ev["id"], str) or not ev["id"]:
        raise EventValidationError("event id must be a non-empty string")
    if not isinstance(ev.get("weight", 1), int) or ev.get("weight", 1) < 1:
        raise EventValidationError(f"{ev['id']}: weight must be int >= 1")
    chance_n = ev.get("chance_n")
    if chance_n is not None             and (not isinstance(chance_n, int) or chance_n < 1):
        raise EventValidationError(f"{ev['id']}: chance_n must be int >= 1")
    if ev.get("auto"):
        # v2.1 automatic event: no choices; effects (possibly outcomes)
        # apply immediately at offer time and are journaled.
        if "effects" not in ev and "outcomes" not in ev:
            raise EventValidationError(
                f"{ev['id']}: auto event needs effects or outcomes")
        if ev.get("choices"):
            raise EventValidationError(
                f"{ev['id']}: auto event must not carry choices")
    else:
        choices = ev.get("choices")
        if not isinstance(choices, list) or len(choices) < 2:
            raise EventValidationError(
                f"{ev['id']}: at least 2 choices required")
        free = 0
        for choice in choices:
            for key in ("id", "label"):
                if key not in choice:
                    raise EventValidationError(
                        f"{ev['id']}:{choice.get('id')}: missing {key!r}")
            if "branches" not in choice and "outcomes" not in choice:
                for key in ("effects", "outcome_text"):
                    if key not in choice:
                        raise EventValidationError(
                            f"{ev['id']}:{choice.get('id')}: missing "
                            f"{key!r}")
            cost = choice.get("cost", {})
            if cost.get("money", 0) == 0 and "money_sampled" not in cost:
                free += 1
        if free < 1:
            # SOFT-LOCK RULE: every decision keeps an always-available option
            raise EventValidationError(
                f"{ev['id']}: no free option (soft-lock rule)")
    cond = ev.get("conditions", {})
    for clause in cond.get("requires", []):
        if clause.get("kind") not in CONDITION_KINDS:
            raise EventValidationError(
                f"{ev['id']}: unknown condition kind "
                f"{clause.get('kind')!r}")


def load_event_files(directory=None):
    """Load and validate every event file. Returns [EventDef].

    Default loads BOTH the bundled fit-proof dir (src/game/events) and the
    content dir (src/game/content/events). Pass an explicit directory to
    load only that one (tests).
    """
    if directory is not None:
        directories = [directory]
    else:
        directories = [EVENTS_DIR, CONTENT_DIR]
    events = []
    for directory in directories:
        if not os.path.isdir(directory):
            continue
        for name in sorted(os.listdir(directory)):
            if not name.endswith(".json"):
                continue
            with open(os.path.join(directory, name),
                      encoding="utf-8") as f:
                doc = json.load(f)
            if not isinstance(doc, dict) \
                    or not isinstance(doc.get("events"), list):
                raise EventValidationError(f"{name}: bad event file shape")
            for ev in doc["events"]:
                validate_event(ev)
                events.append(ev)
    ids = [ev["id"] for ev in events]
    if len(ids) != len(set(ids)):
        raise EventValidationError("duplicate event ids")
    return events


class EventEngine:
    """Condition evaluation + sampling over loaded event defs."""

    def __init__(self, events):
        self.events = list(events)

    @staticmethod
    def _conditions_met(ev, age, used_ids, context, rng):
        cond = ev.get("conditions", {})
        if not (cond.get("min_age", 0) <= age <= cond.get("max_age", 200)):
            return False
        if cond.get("once_per_lifetime", True) and ev["id"] in used_ids:
            return False
        for clause in cond.get("requires", []):
            kind = clause.get("kind")
            if kind == "alive":
                if not context.alive_role(clause.get("role")):
                    return False
            elif kind == "has_illness":
                if not context.has_illness(clause.get("illness")):
                    return False
            elif kind == "employed":
                if not context.is_employed():
                    return False
            elif kind == "in_school":
                if not context.is_in_school():
                    return False
            elif kind == "min_stat":
                if context.stat(clause.get("stat")) < clause.get("value", 0):
                    return False
            elif kind == "min_money":
                if context.money() < clause.get("value", 0):
                    return False
            elif kind == "flag":
                if not context.flag(clause.get("name")):
                    return False
            elif kind == "no_partner":
                if context.alive_role("partner"):
                    return False
            elif kind == "partner_status":
                status = context.partner_status()
                if status is None:
                    return False
                if "value" in clause and status != clause["value"]:
                    return False
                if "max" in clause and status > clause["max"]:
                    return False
            elif kind == "min_age_sampled":
                if age < rng.randint(*clause["randint"]):
                    return False
            elif kind == "max_age_sampled":
                if age >= rng.randint(*clause["randint"]):
                    return False
            elif kind == "partner_relationship_below":
                rel = context.partner_relationship()
                if rel is None or rel >= rng.randint(*clause["randint"]):
                    return False
            elif kind == "karma_gate":
                # legacy: passes when NOT x_in_y(karma, value)
                if rng.uniform(0, clause.get("value", 1)) < context.stat(
                        "karma"):
                    return False
        return True

    def roll_independent(self, rng, age, used_ids, context):
        """A's coordinator entry point for finding-round #5 (independent
        chance_n events, handled separately from the flavor pool)."""
        return self.sample_independent(rng, age, used_ids, context)

    def sample_independent(self, rng, age, used_ids, context):
        """Findings-round #5: events WITH chance_n carry their own
        independent per-year 1-in-N gate (legacy parity) and are handled
        SEPARATELY from the flavor pool, in declared (file) order, so two
        successful independent events both happen and flavor content can
        never dilute their rates. Returns the list of passed events
        (possibly empty); each occurrence must be handled exactly once by
        the coordinator with a per-occurrence done marker."""
        out = []
        order = {eid: i for i, eid in enumerate(LEGACY_CHANCE_ORDER)}
        eligible = [ev for ev in self.events if ev.get("chance_n")]
        eligible.sort(key=lambda ev: order.get(ev["id"], len(order)))
        for ev in eligible:
            if not self._conditions_met(ev, age, used_ids, context, rng):
                continue
            if rng.randrange(ev["chance_n"]) == 0:
                out.append(ev)
        return out

    def sample_flavor(self, rng, age, used_ids, context):
        """A's coordinator entry point for the weighted flavor pool."""
        return self.sample(rng, age, used_ids, context)

    def sample(self, rng, age, used_ids, context):
        """FLAVOR selection only: weighted pool (with the ~1-in-3
        eligible-years gate, M1 template parity) over events WITHOUT
        chance_n. Independent legacy chances run via sample_independent."""
        pool = [ev for ev in self.events
                if not ev.get("chance_n") and self._conditions_met(
                    ev, age, used_ids, context, rng)]
        if not pool:
            return None
        if rng.randrange(3) != 0:
            return None
        total = sum(ev.get("weight", 1) for ev in pool)
        roll = rng.randrange(total)
        for ev in pool:
            roll -= ev.get("weight", 1)
            if roll < 0:
                return ev
        return pool[-1]

    @staticmethod
    def _sign(value, default=1):
        """v2.1 accepts 1/-1 ints and 'positive'/'negative' strings."""
        if value in ("negative", -1):
            return -1
        if value in ("positive", 1):
            return 1
        return default

    @classmethod
    def _money_value(cls, money, rng, context, sampled=None):
        sampled = sampled or {}
        if isinstance(money, int):
            return money
        if isinstance(money, dict) and "money_pct" in money:
            lo, hi = money["money_pct"]
            sign = cls._sign(money.get("sign", 1))
            return sign * round(context.money() * rng.uniform(lo, hi))
        if isinstance(money, dict) and "sampled" in money:
            sign = cls._sign(money.get("sign", 1))
            return sign * sampled[money["sampled"]]
        return 0

    @classmethod
    def _resolve_effects(cls, effects, rng, context, sampled=None):
        """Flatten the schema Effects form to the service's flat _effects
        and sample ALL randomness ONCE (pre-sampled policy): randint ranges
        (and ordered {"sum": [...]} combinations), money_pct (with sign),
        sampled_at_offer references, and relationship role targets.
        Structural keys pass through for the resolver's _structural split.
        """
        import copy
        sampled = sampled or {}
        flat = {}
        extra_ids = []

        def sample_delta(value):
            if isinstance(value, dict) and "randint" in value:
                lo, hi = value["randint"]
                return cls._sign(value.get("sign", 1)) \
                    * rng.randint(lo, hi)
            if isinstance(value, dict) and "sum" in value:
                return sum(sample_delta(v) for v in value["sum"])
            return value

        stats = effects.get("stats", {})
        for key, value in list(stats.items()):
            flat[key] = sample_delta(value)
        # Tolerant lift: stat keys placed at the top level of an effects
        # map (e.g. "karma": -1 beside "stats") are real effects and must
        # not be dropped — fold them in unless already present.
        for key in ("happiness", "health", "smarts", "looks", "karma",
                    "stress"):
            if key in effects and key not in flat:
                flat[key] = sample_delta(effects[key])
        money = effects.get("money")
        if isinstance(money, dict) and "sum" in money:
            flat["money"] = sum(
                cls._money_value(m, rng, context, sampled)
                for m in money["sum"])
        elif money is not None:
            flat["money"] = cls._money_value(money, rng, context, sampled)
        rel = effects.get("relationship")
        if isinstance(rel, dict):
            delta = rel.get("delta")
            delta = sample_delta(delta)
            role = rel.get("role")
            if role in ("random_parent", "random_relation"):
                pid = context.random_person_id(
                    parents_only=(role == "random_parent"), rng=rng)
                if pid:
                    extra_ids.append(pid)
                    flat["relationship"] = delta
            elif role in ("mother", "father", "partner", "child",
                          "all_parents"):
                pids = context.role_person_ids(role)
                extra_ids.extend(pids)
                flat["relationship"] = delta
            else:
                flat["relationship"] = delta
        return flat, extra_ids

    _STRUCTURAL_KEYS = ("lose_partner", "divorce", "gain_partner",
                        "partner_death", "terminal", "add_illness",
                        "remove_illness", "learn_trait", "lose_trait",
                        "set_flag")

    @classmethod
    def _split_structural(cls, effects):
        """Structural effects are NOT applied by the legacy money/stats
        resolver; they ride in a separate _structural key so the current
        resolver ignores them safely (A applies them via rule actions)."""
        return {k: v for k, v in effects.items() if k in cls._STRUCTURAL_KEYS}

    @staticmethod
    def _sample_offer_values(ev, rng, context):
        """sampled_at_offer block: drawn once, kept private in the pending."""
        sampled = {}
        for name, spec in (ev.get("sampled_at_offer") or {}).items():
            if isinstance(spec, dict) and "money_pct" in spec:
                lo, hi = spec["money_pct"]
                sign = EventEngine._sign(spec.get("sign", 1))
                sampled[name] = sign * round(
                    context.money() * rng.uniform(lo, hi))
            elif isinstance(spec, dict) and "randint" in spec:
                sampled[name] = rng.randint(*spec["randint"])
            else:
                sampled[name] = spec
        return sampled

    @staticmethod
    def _pick_outcome(outcomes, rng):
        total = sum(o.get("weight", 1) for o in outcomes)
        roll = rng.randrange(total)
        for o in outcomes:
            roll -= o.get("weight", 1)
            if roll < 0:
                return o
        return outcomes[-1]

    @classmethod
    def instantiate(cls, ev, seq, player_name, alive_role_ids=None, rng=None,
                    context=None):
        """Build the service pending record (same shape as M1 templates;
        resolver consumes _effects/_outcome). All randomness is sampled
        HERE, once, per the pre-sampled policy. Event-level pre-effects are
        merged into every option (legacy applies them at offer time)."""
        rng = rng or random.Random()
        context = context or _NullContext()
        if ev.get("auto"):
            raise EventValidationError(
                f"{ev['id']}: auto events use instantiate_auto()")
        person_ids = list(alive_role_ids or [])
        text = cls._render_text(ev["text"], player_name, context)
        sampled = cls._sample_offer_values(ev, rng, context)
        pre = ev.get("effects", {})
        pre_flat, pre_extra = ([], [])
        if pre:
            pre_flat, pre_extra = cls._resolve_effects(pre, rng, context,
                                                       sampled)
        options = []
        for c in ev["choices"]:
            # choice-level effects always apply; outcomes/branches overlay
            base = dict(c.get("effects", {}))
            bundle = {"effects": base,
                      "outcome_text": c.get("outcome_text", "")}
            if c.get("outcomes"):
                picked = cls._pick_outcome(c["outcomes"], rng)
                base.update(picked.get("effects", {}))
                bundle = {"effects": base,
                          "outcome_text": picked.get("outcome_text",
                                                      bundle["outcome_text"])}
            if c.get("branches"):
                br = c["branches"]
                sample = br.get("sample", {})
                x = context.field(sample.get("x")) \
                    if isinstance(sample.get("x"), str) \
                    else sample.get("x", 1)
                y = context.field(sample.get("y")) \
                    if isinstance(sample.get("y"), str) \
                    else sample.get("y", 1)
                draws = sample.get("draws", 1)
                hit = False
                for _ in range(draws):
                    if rng.uniform(0, y) < x:
                        hit = True
                        break
                branch = br["then"] if hit else br["else"]
                base.update(branch.get("effects", {}))
                bundle = {"effects": base,
                          "outcome_text": branch.get(
                              "outcome_text", bundle["outcome_text"])}
            # #4: options carry CHOICE effects only; event pre-effects are
            # sampled once at offer creation and served via the pending's
            # pre_effects (applied at offer time by the coordinator).
            fx, extra = cls._resolve_effects(
                bundle.get("effects", {}), rng, context, sampled)
            structural = cls._split_structural(bundle.get("effects", {}))
            # affordability projection: cost gates only (P6); the resolver
            # still enforces it authoritatively.
            enabled, reason = True, None
            if fx.get("money", 0) < 0 and context is not None \
                    and -fx["money"] > context.money():
                enabled = False
                reason = (f"this choice costs ${-fx['money']} and you have "
                          f"${context.money()}")
            options.append({
                "id": f"d_{seq}_{c['id']}",
                "label": c["label"],
                "known_effects": list(c.get("known_effects", [])),
                "enabled": enabled,
                "reason": reason,
                "inputs": c.get("inputs", []),
                "_outcome": bundle["outcome_text"],
                "_effects": fx,
                "_structural": structural or None,
                "_person_ids_extra": extra or None,
            })
        return {
            "id": f"d_{seq}",
            "kind": ev["id"],
            "event_id": ev["id"],
            "title": ev["title"],
            "text": text,
            "person_ids": person_ids,
            "sampled": sampled or None,
            "pre_effects": pre_flat or None,
            "_pre_person_ids_extra": pre_extra or None,
            "options": options,
        }

    @staticmethod
    def _merge_effects(pre, choice):
        """Deep-merge choice effects over event pre-effects. Per-stat and
        money entries present in BOTH are combined as an ordered {"sum":
        [...]} so every consequence applies (a choice stats map must not
        replace the whole pre-effect map)."""
        merged = dict(pre)
        for key, value in choice.items():
            if key == "stats":
                stats = dict(merged.get("stats", {}))
                for sk, sv in value.items():
                    if sk in stats:
                        stats[sk] = {"sum": [stats[sk], sv]}
                    else:
                        stats[sk] = sv
                merged["stats"] = stats
            elif key == "money" and key in merged:
                merged["money"] = {"sum": [merged["money"], value]}
            elif key == "relationship" and key in merged \
                    and isinstance(merged[key], dict) \
                    and isinstance(value, dict):
                rel = dict(merged[key])
                if "delta" in rel and "delta" in value:
                    rel["delta"] = {"sum": [rel["delta"], value["delta"]]}
                    for k2, v2 in value.items():
                        rel.setdefault(k2, v2)
                    merged["relationship"] = rel
                else:
                    merged[key] = value
            else:
                merged[key] = value
        return merged

    @staticmethod
    def _render_text(template, player_name, context=None):
        """Resolve {name} and {person:role} refs (engine-resolved to
        canonical names via the context, including archived people).
        Never leak schema syntax to the player."""
        out = template.replace("{name}", player_name)
        if context is None:
            return out

        def sub(match):
            role = match.group(1)
            pids = context.role_person_ids(role)
            if pids:
                name = context.person_name(pids[0])
                if name:
                    return name
            return match.group(0)
        import re as _re
        prev = None
        while prev != out:
            prev = out
            out = _re.sub(r"\{person:(\w+)\}", sub, out)
        return out

    @classmethod
    def instantiate_auto(cls, ev, rng, context):
        """v2.1 auto event: no pending record. Samples the event-level
        effects/outcomes once and returns the resolved bundle for the
        coordinator to apply immediately and journal:
        {"event_id", "_effects" (flat), "_outcome", "terminal"}."""
        rng = rng or random.Random()
        context = context or _NullContext()
        if not ev.get("auto"):
            raise EventValidationError(
                f"{ev['id']}: not an auto event")
        bundle = {"effects": dict(ev.get("effects", {})),
                  "outcome_text": ev.get("outcome_text", "")
                  or ev.get("text", "")}
        if ev.get("outcomes"):
            picked = cls._pick_outcome(ev["outcomes"], rng)
            bundle = {"effects": cls._merge_effects(
                          ev.get("effects", {}),
                          picked.get("effects", {})),
                      "outcome_text": picked.get("outcome_text")
                      or ev.get("text", ""),
                      "terminal": picked.get("terminal", False)}
        merged = bundle.get("effects", {})
        fx, extra = cls._resolve_effects(merged, rng, context)
        return {
            "event_id": ev["id"],
            "_effects": fx,
            "_structural": cls._split_structural(merged) or None,
            "_outcome": bundle.get("outcome_text", ""),
            "terminal": bool(bundle.get("terminal")),
            "_person_ids_extra": extra or None,
        }


class _NullContext:
    """Minimal context for standalone loader use (tests)."""
    def alive_role(self, role):
        return False

    def has_illness(self, illness):
        return False

    def is_employed(self):
        return False

    def is_in_school(self):
        return False

    def stat(self, name):
        return 0

    def money(self):
        return 0

    def flag(self, name):
        return False

    def partner_status(self):
        return None

    def partner_relationship(self):
        return None

    def field(self, ref):
        return 0

    def person_name(self, person_id):
        dead = self._env.get("dead_ids", [])
        for p in self._service._walk_people(self._player):
            if p.person_id == person_id:
                return p.name
        for p in self._env.get("archive", []):
            if getattr(p, "person_id", None) == person_id:
                return p.name
        return None

    def role_person_ids(self, role):
        return []

    def random_person_id(self, parents_only=False, rng=None):
        return None


class GameContext:
    """Condition context over the service envelope (duck-typed for C)."""

    def __init__(self, env, service):
        self._env = env
        self._service = service
        self._player = env["player"]

    def alive_role(self, role):
        from src.lifesim_lib.lifesim_lib import Gender
        dead = self._env.get("dead_ids", [])
        if role in ("mother", "father", "parents"):
            parents = [p for p in self._service._walk_people(self._player)
                       if type(p).__name__ == "Parent"
                       and p.person_id not in dead]
            if role == "parents":
                return bool(parents)
            want = Gender.Female if role == "mother" else Gender.Male
            return any(p.gender == want for p in parents)
        if role == "partner":
            p = self._player.partner
            return p is not None and p.person_id not in dead
        if role == "child":
            return any(c.person_id not in dead
                       for c in self._player.children)
        return False

    def has_illness(self, illness):
        return any(str(i) == illness for i in self._player.illnesses)

    def is_employed(self):
        return self._player.has_job

    def is_in_school(self):
        return self._player.is_in_school() or self._player.uv_years > 0

    def stat(self, name):
        return getattr(self._player, name, 0)

    def money(self):
        return self._player.money

    def flag(self, name):
        return bool(self._env.setdefault("flags", {}).get(name))

    def partner_status(self):
        p = self._player.partner
        return p.status if p is not None else None

    def partner_relationship(self):
        p = self._player.partner
        return p.relationship if p is not None else None

    def person_name(self, person_id):
        dead = self._env.get("dead_ids", [])
        for p in self._service._walk_people(self._player):
            if p.person_id == person_id:
                return p.name
        for p in self._env.get("archive", []):
            if getattr(p, "person_id", None) == person_id:
                return p.name
        return None

    def role_person_ids(self, role):
        dead = self._env.get("dead_ids", [])
        people = self._service._walk_people(self._player)
        if role in ("mother", "father"):
            from src.lifesim_lib.lifesim_lib import Gender
            want = Gender.Female if role == "mother" else Gender.Male
            return [p.person_id for p in people
                    if type(p).__name__ == "Parent"
                    and p.gender == want and p.person_id not in dead]
        if role == "partner":
            p = self._player.partner
            return [p.person_id] if p is not None \
                and p.person_id not in dead else []
        if role == "child":
            return [c.person_id for c in self._player.children
                    if c.person_id not in dead]
        if role == "all_parents":
            return [p.person_id for p in people
                    if type(p).__name__ == "Parent"
                    and p.person_id not in dead]
        return []

    def field(self, ref):
        """field_ref: player.<attr> | partner.<attr> (+ partner.compatibility
        = the one-draw compatibility check, 100 or 0); unknown -> 0."""
        if not isinstance(ref, str) or "." not in ref:
            return 0
        owner, attr = ref.split(".", 1)
        if owner == "partner" and attr == "compatibility":
            partner = self._player.partner
            if partner is None:
                return 0
            return 100 if partner.compatibility_check(self._player) else 0
        obj = self._player if owner == "player" else \
            getattr(self._player, "partner", None) if owner == "partner" \
            else None
        value = getattr(obj, attr, 0) if obj is not None else 0
        try:
            return int(value)
        except (TypeError, ValueError):
            return 0

    def random_person_id(self, parents_only=False, rng=None):
        dead = self._env.get("dead_ids", [])
        people = [p for p in self._service._walk_people(self._player)
                  if p is not self._player and p.person_id not in dead]
        if parents_only:
            people = [p for p in people
                      if type(p).__name__ == "Parent"]
        if not people:
            return None
        if rng is not None:
            return people[rng.randrange(len(people))].person_id
        return people[0].person_id
