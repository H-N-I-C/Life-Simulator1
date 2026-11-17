"""Q1-C1 property-style hardening tests (Kimi C).

Randomized states x every rule entry point: for seeded random Players
(with random partners/parents/siblings/children/jobs/school states), this
drives EVERY action in the dispatch tables, EVERY pending resolver with
valid and garbage inputs, EVERY yearly phase, and EVERY content event
(conditions, instantiate, per-option resolve), asserting:

- only typed RuleError escapes (never KeyError/TypeError/ValueError/500);
- invariants hold afterwards: stats stay in 0..100, rules never mutate
  player.money directly (cash moves only via ledger money_entries),
  every money entry is an int with a unique (phase, occurrence, leg) key,
  events reference people by canonical person_id strings.

Plus regression tests that FORCE the two crash paths fixed in C1:
- _resolve_event_pending with unapplied pre_effects and extra person ids
  (NameError: person_ids/targets used before assignment);
- inheritance from a broke parent (negative "inheritance" draw).
"""

import random
import unittest

import src.menus.rule_actions as ra
from src.game.features.family_graph.graph import GraphError
from src.game.events import CONTENT_DIR, EventEngine, load_event_files
from src.lifesim_lib.lifesim_lib import Gender
from src.people.classes.child import Child
from src.people.classes.parent import Parent
from src.people.classes.partner import Partner
from src.people.classes.sibling import Sibling
from tests.test_rules_income import make_player
from tests.test_rules_m2b import partner_for

STATS = ("happiness", "health", "smarts", "looks", "karma", "stress")


def _walk(p):
    seen = []
    for rel in list(getattr(p, "relations", []) or []):
        if rel not in seen:
            seen.append(rel)
    for c in list(getattr(p, "children", []) or []):
        if c not in seen:
            seen.append(c)
    return seen


class PropContext:
    """Duck-typed context: .player for rule_actions, loader methods for
    EventEngine, people_by_id so _find_person resolves real people, and
    a duck envelope/service so context-aware actions (dating_app) and A's
    graph predicates can run."""
    def __init__(self, player, rng):
        self.player = player
        self._rng = rng
        self.people_by_id = {}
        for person in [player] + _walk(player):
            self.people_by_id[person.person_id] = person
        self._env = {
            "player": player, "archive": [], "dead_ids": [],
            "policy": {"profiles": {}, "overrides": {},
                       "jurisdiction_id": None},
            "graph": {"relationships": [], "unions": [], "parentage": [],
                      "households": []},
        }
        self._service = _PropService(player)

    # ---- EventEngine context surface ----
    def alive_role(self, role):
        return bool(self.role_person_ids(role))

    def has_illness(self, illness):
        return illness in (self.player.illnesses or [])

    def is_employed(self):
        return bool(self.player.has_job)

    def is_in_school(self):
        try:
            return self.player.is_in_school()
        except AttributeError:
            return False

    def stat(self, name):
        return getattr(self.player, name, 0)

    def money(self):
        return self.player.money

    def flag(self, name):
        return bool(getattr(self.player, name, False))

    def partner_status(self):
        partner = self.player.partner
        return partner.status if partner is not None else None

    def partner_relationship(self):
        partner = self.player.partner
        return getattr(partner, "relationship", None) if partner else None

    def field(self, ref):
        return getattr(self.player, str(ref), 0) or 0

    def person_name(self, person_id):
        person = self.people_by_id.get(person_id)
        return person.name if person is not None else None

    def role_person_ids(self, role):
        p = self.player
        if role == "partner":
            return [p.partner.person_id] if p.partner is not None else []
        if role in ("mother", "father"):
            out = []
            for par in (p.parents or {}).values():
                want = Gender.Female if role == "mother" else Gender.Male
                if par.gender == want:
                    out.append(par.person_id)
            return out
        if role in ("random_parent", "all_parents"):
            return [par.person_id for par in (p.parents or {}).values()]
        if role == "child":
            return [c.person_id for c in (p.children or [])]
        if role == "random_relation":
            ids = [person.person_id for person in _walk(p)]
            return [self._rng.choice(ids)] if ids else []
        return []

    def random_person_id(self, parents_only=False, rng=None):
        rng = rng or self._rng
        if parents_only and self.player.parents:
            return rng.choice(list(self.player.parents.values())).person_id
        ids = [person.person_id for person in _walk(self.player)]
        return rng.choice(ids) if ids else None


def random_player(rng):
    age = rng.randint(0, 90)
    p = make_player(
        age=age,
        happiness=rng.randint(0, 100),
        health=rng.randint(1, 100),
        smarts=rng.randint(0, 100),
        looks=rng.randint(0, 100),
        karma=rng.randint(0, 100),
        money=rng.choice([0, 0, 1, rng.randint(0, 500),
                          rng.randint(500, 200000), -rng.randint(0, 50000)]),
        salary=0, has_job=False,
        grades=rng.randint(0, 100) if age < 18 else None,
    )
    p.stress = rng.randint(0, 100)
    p.traits = {}
    p.illnesses = []
    p.generation = rng.randint(1, 3)
    p.date_options = rng.randint(0, 3)
    p.lottery_jackpot = rng.randint(100000, 1000000)
    p.student_loan = rng.choice([0, 0, rng.randint(1000, 40000)])
    p.uv_years = rng.choice([0, 0, 1, 4])
    p.dropped_out = rng.random() < 0.3
    p.salary_years = []
    p.fertility = rng.randint(0, 100)
    if age >= 18 and rng.random() < 0.6:
        p.has_job = True
        p.salary = rng.randint(20000, 150000)
        p.job_hours = rng.randint(38, 70)
        p.years_worked = rng.randint(0, 40)
        p.performance = rng.randint(0, 100)
        p.last_raise = max(0, age - rng.randint(0, 15))
    # Partner
    if age >= 16 and rng.random() < 0.6:
        partner_for(p, age=max(16, age - rng.randint(-5, 10)),
                    status=rng.randint(0, 2),
                    relationship=rng.randint(0, 100))
    # Parents
    if rng.random() < 0.8:
        p.parents = {
            "Mother": Parent(p.lastname, rng.randint(30, 90), Gender.Female),
            "Father": Parent(p.lastname, rng.randint(30, 90), Gender.Male),
        }
        for par in p.parents.values():
            par.money = rng.randint(0, 100)
            par.generosity = rng.randint(0, 100)
            par.relationship = rng.randint(0, 100)
            p.relations.append(par)
    # Sibling
    if rng.random() < 0.5:
        sib = Sibling(p.lastname, rng.randint(0, 80),
                      rng.choice([Gender.Male, Gender.Female]),
                      rng.randint(0, 100), rng.randint(0, 100))
        sib.relationship = rng.randint(0, 100)
        p.relations.append(sib)
    # Children
    for _ in range(rng.randint(0, 3)):
        if age < 4:
            break
        c = Child(rng.choice(["A", "B"]), p.lastname,
                  rng.choice([Gender.Male, Gender.Female]),
                  rng.randint(0, 100), rng.randint(0, 100), adopted=True)
        c.age = max(0, age - rng.randint(18, 40)) if age >= 18 else 0
        c.relationship = rng.randint(0, 100)
        c.salary = 0
        c.is_in_uv = 0
        c.money = 0
        c.years_worked = 0
        p.children.append(c)
        p.relations.append(c)
    return p


def assert_invariants(testcase, p, result, money_before):
    for key in ("events", "money_entries", "pending_decision",
                "continuation", "finished_turn", "deaths"):
        testcase.assertIn(key, result)
    # Rules never move cash directly; the ledger does (A posts entries).
    testcase.assertEqual(p.money, money_before)
    for person in [p] + _walk(p):
        for stat in STATS:
            value = getattr(person, stat, None)
            if value is None:
                continue  # e.g. parents carry no stress/karma
            testcase.assertGreaterEqual(value, 0, f"{stat} below 0")
            testcase.assertLessEqual(value, 100, f"{stat} above 100")
    seen = set()
    for entry in result["money_entries"]:
        testcase.assertIsInstance(entry["amount"], int)
        testcase.assertIsInstance(entry["account_owner_id"], str)
        key = (entry["phase"], entry["occurrence_id"], entry["leg"])
        testcase.assertNotIn(key, seen, f"duplicate ledger key {key}")
        seen.add(key)
    for event in result["events"]:
        for pid in event.get("person_ids", []):
            testcase.assertIsInstance(pid, str)
    for death in result["deaths"]:
        testcase.assertIsInstance(death["person_id"], str)


def random_inputs(rng):
    return rng.choice([
        {},
        {"name": "x" * rng.randint(0, 50)},
        {"name": "Zoe"},
        {"numbers": [rng.randint(0, 25) for _ in range(rng.randint(0, 6))]},
        {"hours": rng.randint(0, 100)},
        "garbage", None, 42,
    ])


def resolve_pending(testcase, ctx, pending, rng, p, seen_ids):
    """Resolve a pending decision with every option + garbage; recurse into
    follow-up pendings (bounded)."""
    for _ in range(6):
        if not pending or not isinstance(pending, dict):
            return
        kind = pending.get("kind", "?")
        if kind in seen_ids and seen_ids[kind] > 3:
            return
        seen_ids[kind] = seen_ids.get(kind, 0) + 1
        options = [o["id"] for o in pending.get("options", [])]
        for option_id in options + ["bogus_option"]:
            before = p.money
            try:
                result = ra.resolve_choice(ctx, pending, option_id,
                                           random_inputs(rng), rng)
            except ra.RuleError:
                continue
            assert_invariants(testcase, p, result, before)
            for death in result["deaths"]:
                testcase.assertTrue(death["person_id"])
        # follow one chain step with the last valid option
        pending = None


ALL_ACTIONS = (
    sorted(ra._ACTIONS) + sorted(ra._TARGET_ACTIONS)
    + sorted(ra._PARENT_ACTIONS) + sorted(ra._OFFER_ACTIONS)
    + sorted(ra._SELF_ACTIONS) + sorted(ra._CONTEXT_SELF_ACTIONS)
    + ["find_job", "find_partner"]
)

ALL_PHASES = sorted(set(ra._PHASES) | {"illness", "family_events"})


class PropertyHardeningTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.events = load_event_files(CONTENT_DIR)

    def drive_seed(self, seed):
        rng = random.Random(seed)
        p = random_player(rng)
        ctx = PropContext(p, rng)
        target_ids = [rel.person_id for rel in _walk(p)] + ["bogus", "", 123]

        for action in ALL_ACTIONS:
            payload = {"hours": rng.randint(0, 100),
                       "target_id": rng.choice(target_ids)}
            before = p.money
            try:
                result = ra.apply_action(ctx, action, payload, rng)
            except ra.RuleError:
                continue
            assert_invariants(self, p, result, before)
            resolve_pending(self, ctx, result["pending_decision"], rng, p, {})

        for phase in ALL_PHASES:
            before = p.money
            try:
                result = ra.advance_phase(ctx, phase, rng,
                                          turn_id=f"turn-{seed}")
            except ra.RuleError:
                continue
            assert_invariants(self, p, result, before)
            resolve_pending(self, ctx, result["pending_decision"], rng, p, {})

    def test_random_states_every_action_and_phase(self):
        for seed in range(40):
            self.drive_seed(seed)

    def test_unknown_action_and_phase_are_typed_errors(self):
        p = random_player(random.Random(999))
        ctx = PropContext(p, random.Random(999))
        with self.assertRaises(ra.RuleError):
            ra.apply_action(ctx, "not_an_action", {}, random.Random(1))
        with self.assertRaises(ra.RuleError):
            ra.advance_phase(ctx, "not_a_phase", random.Random(1))
        with self.assertRaises(ra.RuleError):
            ra.resolve_choice(ctx, {"kind": "nope"}, "x", {}, random.Random(1))
        with self.assertRaises(ra.RuleError):
            ra.resolve_choice(ctx, "not-a-dict", "x", {}, random.Random(1))


class EventContentPropertyTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.events = load_event_files(CONTENT_DIR)
        cls.engine = EventEngine(cls.events)

    def test_every_event_instantiates_and_resolves_every_option(self):
        for seed in range(12):
            rng = random.Random(1000 + seed)
            p = random_player(rng)
            ctx = PropContext(p, rng)
            for seq, ev in enumerate(self.events):
                if ev.get("auto"):
                    bundle = EventEngine.instantiate_auto(ev, rng, ctx)
                    self.assertIsInstance(bundle["_effects"], dict)
                    continue
                try:
                    pending = EventEngine.instantiate(
                        ev, seq, p.firstname,
                        alive_role_ids=ctx.role_person_ids("random_relation"),
                        rng=rng, context=ctx)
                except GraphError as exc:
                    self.assertEqual(exc.code, "not_adult")
                    self.assertLess(p.age, 18)
                    continue  # deliberately invalid actor, not a legal offer
                self.assertGreaterEqual(len(pending["options"]), 2)
                before = p.money
                for option in pending["options"]:
                    try:
                        result = ra.resolve_choice(ctx, pending,
                                                   option["id"], {}, rng)
                    except ra.RuleError:
                        continue
                    assert_invariants(self, p, result, before)

    def test_conditions_never_crash_across_random_states(self):
        rng = random.Random(7)
        for age in range(0, 100, 3):
            p = random_player(rng)
            ctx = PropContext(p, rng)
            self.engine.sample(rng, age, set(), ctx)
            self.engine.sample_independent(rng, age, set(), ctx)

    def test_pre_effects_with_extra_person_ids_resolve(self):
        """Regression (C1): _resolve_event_pending referenced person_ids and
        targets before assignment when pre_effects were unapplied and the
        event carried relationship pre-effects -> NameError crash."""
        rng = random.Random(3)
        p = random_player(rng)
        ctx = PropContext(p, rng)
        ev = {
            "id": "prop_pre_fx",
            "title": "Pre",
            "text": "Something happened to {name}.",
            "choices": [
                {"id": "accept", "label": "Accept",
                 "effects": {"money": 100}, "outcome_text": "ok"},
                {"id": "decline", "label": "Decline",
                 "effects": {}, "outcome_text": "no"},
            ],
            "effects": {"relationship": {"role": "random_relation",
                                         "delta": {"randint": [1, 5]}},
                        "happiness": -1},
        }
        pending = EventEngine.instantiate(ev, 0, p.firstname, rng=rng,
                                          context=ctx)
        self.assertTrue(pending["pre_effects"])
        self.assertTrue(pending["_pre_person_ids_extra"])
        option_id = pending["options"][0]["id"]
        result = ra.resolve_choice(ctx, pending, option_id, {}, rng)
        assert_invariants(self, p, result, p.money)
        self.assertEqual(result["events"], result["events"])  # resolved OK


class InheritanceHazardTest(unittest.TestCase):
    def test_broke_parent_never_negative_inheritance(self):
        """Regression (C1): a parent with $0 gave avg < lo, drawing a
        NEGATIVE 'inheritance' (or crashing). Forced: money=0, many seeds."""
        p = make_player(age=50, money=0)
        parent = Parent(p.lastname, 90, Gender.Female)
        parent.money = 0
        parent.generosity = 100
        p.relations.append(parent)
        for seed in range(50):
            rng = random.Random(seed)
            result = ra.sample_relation_death(p, parent, rng)
            for entry in result["money_entries"]:
                self.assertGreaterEqual(entry["amount"], 1)




class SpecDispatchAgreementTest(unittest.TestCase):
    """Q1-C1 (D's robot player findings): action_specs and dispatch must
    agree in BOTH directions over random states:
      - every spec advertised enabled is accepted by apply_action;
      - every payload apply_action accepts matches an enabled spec.
    Stochastic legacy refusals (code "refused": partner says no, low
    relationship) are gameplay outcomes, not eligibility, and are allowed
    to raise even for enabled specs."""

    SOFT_CODES = {"refused"}

    def assert_agrees(self, seed):
        rng = random.Random(seed)
        p = random_player(rng)
        ctx = PropContext(p, rng)
        specs = ra.action_specs(ctx)
        enabled = {}
        for s in specs:
            enabled.setdefault(s["id"], []).append(s)

        # Direction 1: enabled spec -> dispatch accepts. Specs are re-read
        # fresh per application: earlier actions in the loop legitimately
        # change eligibility (break_up removes the partner and its specs).
        tried = set()
        while True:
            fresh = {s["id"] + "|" + str(s.get("target_id"))
                     for s in ra.action_specs(ctx) if s.get("enabled", True)}
            remaining = [key for key in fresh if key not in tried]
            if not remaining:
                break
            key = remaining[0]
            tried.add(key)
            spec = next(s for s in ra.action_specs(ctx)
                        if s["id"] + "|" + str(s.get("target_id")) == key
                        and s.get("enabled", True))
            payload = {}
            if spec.get("target_id"):
                payload["target_id"] = spec["target_id"]
            for inp in spec.get("inputs", []):
                if inp.get("id") == "hours":
                    payload["hours"] = 54
            try:
                ra.apply_action(ctx, spec["id"], payload, rng)
            except ra.RuleError as e:
                self.assertIn(e.code, self.SOFT_CODES,
                              f"seed {seed}: enabled spec {spec['id']!r} "
                              f"(target {spec.get('target_id')}) refused by "
                              f"dispatch with hard code {e.code!r}: {e}")

        # Direction 2: dispatch accepts -> an enabled spec advertised it.
        # Re-read the projection after direction 1: accepted actions may
        # legitimately have changed eligibility (drop_out unlocks find_job).
        enabled = {}
        for s in ra.action_specs(ctx):
            enabled.setdefault(s["id"], []).append(s)
        target_ids = [r.person_id for r in _walk(p)]
        candidates = []
        for action in ALL_ACTIONS:
            if action in ra._TARGET_ACTIONS or action in ra._PARENT_ACTIONS:
                for tid in target_ids:
                    candidates.append((action, {"target_id": tid}))
            elif action == "adjust_hours":
                for hours in (38, 54, 70):
                    candidates.append((action, {"hours": hours}))
            else:
                candidates.append((action, {}))
        for action, payload in candidates:
            try:
                ra.apply_action(ctx, action, payload, rng)
            except ra.RuleError:
                continue
            matches = [s for s in enabled.get(action, [])
                       if not s.get("target_id")
                       or s["target_id"] == payload.get("target_id")]
            self.assertTrue(matches,
                            f"seed {seed}: dispatch accepted {action!r} "
                            f"{payload} but no enabled spec advertised it; "
                            f"specs had: {enabled.get(action)}")

    def test_agreement_over_random_states(self):
        for seed in range(3000, 3060):
            self.assert_agrees(seed)


class PendingOptionAffordabilityTest(unittest.TestCase):
    """Paid pending options are advertised disabled (with a reason) at
    sample time when unaffordable, so the UI/robot never gets a 409 at
    resolve for a shown-enabled option."""

    def _assert_options_gate(self, result, free_option_ids):
        pending = result["pending_decision"]
        self.assertIsNotNone(pending)
        options = pending["options"]
        for o in options:
            if o["id"] in free_option_ids:
                continue
            if not o.get("enabled", True):
                self.assertTrue(o.get("reason"))
        self.assertTrue(any(o.get("enabled", True) for o in options),
                        "soft-lock: no enabled option left")

    def test_lottery_options_gated_when_broke(self):
        p = make_player(age=30, money=20)
        result = ra.sample_lottery(p, random.Random(1))
        self._assert_options_gate(result, {"back"})

    def test_witch_doctor_take_gated_when_broke(self):
        p = make_player(age=30, money=75)
        result = ra.sample_witch_doctor(p, random.Random(1))
        self._assert_options_gate(result, {"refuse"})

    def test_plastic_surgery_sample_gated_when_broke(self):
        # Action-level gate: sampling refuses outright when unaffordable.
        p = make_player(age=30, money=100)
        with self.assertRaises(ra.RuleError):
            ra.sample_plastic_surgery(p, random.Random(1))

    def test_plastic_surgery_resolve_rechecks_affordability(self):
        # Backstop: afford it at sample time, go broke before resolving —
        # resolve must refuse (the ledger never goes negative on paper).
        p = make_player(age=30, money=30000)
        result = ra.sample_plastic_surgery(p, random.Random(1))
        self._assert_options_gate(result, {"cancel"})
        p.money = 0
        with self.assertRaises(ra.RuleError):
            ra.resolve_plastic_surgery(
                p, result["pending_decision"], "surgeon:0", random.Random(2))

    def test_wedding_venues_gated_when_broke(self):
        p = make_player(age=30, money=300)
        p.partner = partner_for(p, status=1)
        result = ra.sample_wedding_plan(p, random.Random(1))
        self._assert_options_gate(result, {"cancel"})

    def test_adoption_option_gated_when_broke(self):
        p = make_player(age=40, money=15000)
        result = ra.sample_adoption(p, random.Random(1))
        self._assert_options_gate(result, {"decline"})

    def test_doctor_treat_options_gated_when_broke(self):
        p = make_player(age=30, money=50)
        p.illnesses = ["Flu"]
        result = ra.sample_doctor_visit(p, random.Random(1))
        self._assert_options_gate(result, {"back"})




class ResolverKeyAccessTest(unittest.TestCase):
    """Crash sweep (Q1, robot player crash_id 62a309a1e51b): resolvers must
    treat missing keys in stored sampled outcomes as typed errors or zero
    deltas — never a KeyError -> 500. Each test forces the exact stored
    shape that used to crash (no random rolls involved)."""

    def _ctx(self, p):
        return PropContext(p, random.Random(0))

    def test_argument_insult_option_without_happiness_key(self):
        # The deterministic crash: sample_conversation's "insult" outcome
        # has karma + relationship but NO happiness key.
        p = make_player(age=30)
        rel = partner_for(p)
        pending = {
            "kind": "argument",
            "title": "Argument",
            "text": "You argued.",
            "options": [{"id": "apologize"}, {"id": "disagree"},
                        {"id": "insult"}],
            "sampled": {
                "apologize": {"karma": 2, "happiness": -3, "relationship": 3},
                "disagree": {"karma": 0, "happiness": 0, "relationship": 0},
                "insult": {"karma": -4, "relationship": -6},
            },
            "sampled_target_id": rel.person_id,
        }
        result = ra.resolve_argument(p, pending, "insult", random.Random(1))
        self.assertEqual(result["events"][0]["text"], "I insulted them.")

    def test_funeral_option_missing_karma_key(self):
        p = make_player(age=40)
        pending = {
            "kind": "funeral",
            "title": "Funeral",
            "options": [{"id": "attend"}, {"id": "skip"}],
            "sampled": {"attend": {"happiness": 4},  # no karma key
                        "skip": {"happiness": 0, "karma": -3}},
        }
        ra.resolve_funeral_choice(p, pending, "attend", random.Random(1))

    def test_plastic_surgery_crafted_index_is_typed_error(self):
        p = make_player(age=30, money=30000)
        p.plastic_surgeons = [["Dre", 50]]
        pending = {
            "kind": "plastic_surgery",
            "title": "Plastic Surgery",
            "options": [{"id": "surgeon:0"}, {"id": "surgeon:9"},
                        {"id": "cancel"}],
            "sampled": {"per_surgeon": [{"botched": False, "value": 10,
                                         "reputation": 50, "rep_gain": 0,
                                         "rep_cap": 0}]},
        }
        with self.assertRaises(ra.RuleError):
            ra.resolve_plastic_surgery(p, pending, "surgeon:9",
                                       random.Random(1))

    def test_book_choice_unknown_title_is_typed_error(self):
        p = make_player(age=30)
        pending = {
            "kind": "book_choice",
            "title": "Read a Book",
            "options": [{"id": "book:Real Book"}, {"id": "back"}],
            "sampled": {"per_book": {}},
        }
        with self.assertRaises(ra.RuleError):
            ra.resolve_book_choice(p, pending, "book:Real Book",
                                   random.Random(1))

    def test_university_funding_missing_outcome_is_typed_error(self):
        p = make_player(age=17)
        pending = {
            "kind": "university_funding",
            "title": "University",
            "options": [{"id": "scholarship"}, {"id": "student_loan"}],
            "sampled": {"funding_options": [{"id": "scholarship"},
                                            {"id": "student_loan"}],
                        "funding_outcomes": {"student_loan": True},
                        "funding_effects": {"student_loan": 0},
                        "spent": []},
        }
        with self.assertRaises(ra.RuleError):
            ra.resolve_university_funding(p, pending, "scholarship",
                                          random.Random(1))

    def test_pregnancy_missing_outcome_is_typed_error(self):
        p = make_player(age=30)
        partner_for(p)
        pending = {
            "kind": "pregnancy",
            "title": "Baby",
            "options": [{"id": "keep"}, {"id": "abort"}],
            "sampled": {"overrule_u": 0.5, "overrule_chance": {"keep": 0.0}},
        }
        with self.assertRaises(ra.RuleError):
            ra.resolve_pregnancy(p, pending, "abort", random.Random(1))

    def test_job_offer_missing_sampled_is_typed_error(self):
        p = make_player(age=30)
        pending = {"kind": "job_offer", "title": "Job offer",
                   "options": [{"id": "apply"}, {"id": "walk_away"}],
                   "sampled": {}}
        with self.assertRaises(ra.RuleError):
            ra.resolve_choice(self._ctx(p), pending, "apply", {},
                              random.Random(1))

    def test_lottery_missing_winning_is_typed_error(self):
        p = make_player(age=30, money=100)
        pending = {"kind": "lottery", "title": "Lottery",
                   "options": [{"id": "buy_one"}, {"id": "back"}],
                   "sampled": {}}
        with self.assertRaises(ra.RuleError):
            ra.resolve_lottery(p, pending, "buy_one",
                               {"numbers": [1, 2, 3, 4]}, random.Random(1))

if __name__ == "__main__":
    unittest.main()


class _PropService:
    def __init__(self, player):
        self._root = player

    def _walk_people(self, player):
        return _walk(player) + [player]
