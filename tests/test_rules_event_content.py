"""Content validation: the M2c annual-event data files must conform to the
frozen event schema (docs/collab/M2/event-schema.json) AND match the parity
oracle (tests/fixtures/annual_events_oracle.json) exactly — probabilities,
gates, choice sets, effect ranges. Runs without the loader: pure content
checks, so it guards the files while A adapts the loader to v2."""

import glob
import json
import os
import unittest

CONTENT_DIR = os.path.join("src", "game", "content", "events")
ORACLE = json.load(open("tests/fixtures/annual_events_oracle.json"))["events"]

STAT_KEYS = {"happiness", "health", "smarts", "looks", "karma", "stress"}
CONDITION_KINDS = {
    "alive", "no_partner", "has_illness", "employed", "in_school",
    "min_stat", "min_money", "flag", "partner_status", "min_age_sampled",
    "max_age_sampled", "partner_relationship_below", "karma_gate",
}
ROLE_KEYS = {"mother", "father", "partner", "child", "all_parents",
             "sibling", "random_parent", "random_relation"}


def delta_range(value):
    """(min, max) of a DeltaValue."""
    if isinstance(value, int):
        return value, value
    if isinstance(value, dict) and "randint" in value:
        lo, hi = value["randint"]
        sign = value.get("sign", 1)
        return min(lo * sign, hi * sign), max(lo * sign, hi * sign)
    raise AssertionError(f"not a DeltaValue: {value!r}")


# oracle id -> content file event id
ORACLE_TO_CONTENT = {
    "lightning": "lightning_strike",
    "thanatophobia_worry": "thanatophobia_worry",
    "vaccination": "vaccination_age2",
    "partner_breakup_offer": "partner_breakup_offer",
    "partner_divorce_offer": "partner_divorce_offer",
    "partner_kidnapping": "partner_kidnapping",
    "dating_offer": "dating_offer",
}


def load_events():
    events = {}
    for path in sorted(glob.glob(os.path.join(CONTENT_DIR, "*.json"))):
        data = json.load(open(path))
        # v2 and v3 files coexist (M2.5); each validates against its own
        # schema (checked separately in test_rules_v3_loader).
        assert data.get("schema_version", 2) in (2, 3), path
        for event in data["events"]:
            assert event["id"] not in events, event["id"]
            events[event["id"]] = (event, path, data.get("schema_version", 2))
    return events


class SchemaConformanceTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.events = load_events()

    def test_all_seven_oracle_events_present(self):
        for event_id in ORACLE:
            self.assertIn(ORACLE_TO_CONTENT[event_id], self.events,
                          event_id)

    def test_structure_and_soft_lock(self):
        for event_id, (event, path, sv) in self.events.items():
            if sv == 3:
                continue  # v3 structure is validated by validate_event3
            for key in ("id", "title", "text", "category", "weight",
                        "conditions"):
                self.assertIn(key, event, (event_id, key))
            if event.get("auto"):
                self.assertNotIn("choices", event, event_id)
                self.assertIn("effects", event, event_id)
                continue
            choices = event["choices"]
            self.assertGreaterEqual(len(choices), 2, event_id)
            free = [c for c in choices
                    if c.get("cost", {}).get("money", 0) == 0
                    and "money_sampled" not in c.get("cost", {})]
            self.assertTrue(free, f"soft-lock violated: {event_id}")
            for clause in event["conditions"].get("requires", []):
                self.assertIn(clause["kind"], CONDITION_KINDS,
                              (event_id, clause))
            for choice in choices:
                self.assertIn("label", choice)
                self.assertIn("known_effects", choice)
                self.assertIn("cost", choice)
                if "branches" in choice:
                    sample = choice["branches"]["sample"]
                    self.assertEqual(sample["kind"], "x_in_y")
                    self.assertIn("then", choice["branches"])
                    self.assertIn("else", choice["branches"])
                if "outcomes" in choice:
                    self.assertGreaterEqual(
                        sum(o.get("weight", 1) for o in choice["outcomes"]),
                        1)

    def test_no_ambient_money_double_posting(self):
        # cost gates only; effects.money is the single posting (P6)
        for event_id, (event, path, _sv) in self.events.items():
            if event.get("auto"):
                continue
            for choice in event["choices"]:
                if choice.get("cost", {}).get("money", 0) > 0:
                    self.assertIn("money", choice.get("effects", {}),
                                  (event_id, choice["id"]))


class OracleMatchTest(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.events = load_events()

    def test_occurrence_probabilities(self):
        for event_id, spec in ORACLE.items():
            event, _, _ = self.events[ORACLE_TO_CONTENT[event_id]]
            want = spec["occurrence"]["n"]
            self.assertEqual(event.get("chance_n"), want,
                             f"{event_id} occurrence {event.get('chance_n')}"
                             f" != {want}")

    def test_lightning_branch_weights(self):
        event, _, _ = self.events["lightning_strike"]
        self.assertTrue(event.get("auto"))
        weights = [o.get("weight", 1) for o in event["outcomes"]]
        self.assertEqual(weights, [5, 4, 1])  # 1/2, 2/5, death 1/10
        terminal = [o for o in event["outcomes"] if o.get("terminal")]
        self.assertEqual(len(terminal), 1)

    def test_breakup_and_divorce_ranges(self):
        for event_id, pre_key in (("partner_breakup_offer", None),
                                  ("partner_divorce_offer", None)):
            event, _, _ = self.events[event_id]
            lo, hi = delta_range(event["effects"]["stats"]["happiness"])
            self.assertEqual((lo, hi), (-18, -15), event_id)
            ids = [c["id"] for c in event["choices"]]
            self.assertEqual(ids, ["honor" if event_id.endswith("divorce_offer")
                                   else "wish_well", "beg", "insult"])
            beg = event["choices"][1]
            self.assertEqual(beg["branches"]["sample"]["draws"], 2)
            if event_id == "partner_divorce_offer":
                honor = event["choices"][0]
                self.assertTrue(honor["effects"]["divorce"])
                money = honor["effects"]["money"]
                self.assertEqual(money["money_pct"], [0.4, 0.6])
                self.assertEqual(money.get("sign"), -1)

    def test_kidnapping_ransom_and_rates(self):
        event, _, _ = self.events["partner_kidnapping"]
        self.assertEqual(event["sampled_at_offer"]["ransom"]["money_pct"],
                         [0.2, 0.8])
        pay = event["choices"][0]
        self.assertEqual(pay["cost"], {"money_sampled": "ransom"})
        weights = [o.get("weight", 1) for o in pay["outcomes"]]
        self.assertEqual(weights, [5, 1])  # 1/6 failure
        police = event["choices"][1]
        self.assertEqual([o.get("weight", 1) for o in police["outcomes"]],
                         [2, 1])  # 1/3 failure
        nothing = event["choices"][2]
        lo, hi = delta_range(nothing["effects"]["stats"]["karma"])
        self.assertEqual((lo, hi), (-16, -8))
        self.assertEqual(nothing["effects"]["partner_death"], "presumed dead")

    def test_dating_and_vaccination(self):
        dating, _, _ = self.events["dating_offer"]
        self.assertIn({"kind": "no_partner"},
                      dating["conditions"]["requires"])
        accept = dating["choices"][0]
        self.assertIn("gain_partner",
                      accept["branches"]["then"]["effects"])
        lo, hi = delta_range(
            accept["branches"]["then"]["effects"]["stats"]["happiness"])
        self.assertEqual((lo, hi), (12, 20))

        vacc, _, _ = self.events["vaccination_age2"]
        self.assertEqual(vacc["conditions"]["min_age"], 2)
        self.assertEqual(vacc["conditions"]["max_age"], 2)
        bite = vacc["choices"][2]
        lo, hi = delta_range(bite["effects"]["stats"]["happiness"])
        self.assertEqual((lo, hi), (-10, -6))
        lo, hi = delta_range(bite["effects"]["relationship"]["delta"])
        self.assertEqual((lo, hi), (-35, -25))
        self.assertEqual(bite["effects"]["relationship"]["role"], "mother")

    def test_thanatophobia_ranges(self):
        event, _, _ = self.events["thanatophobia_worry"]
        self.assertTrue(event.get("auto"))
        stats = event["effects"]["stats"]
        self.assertEqual(delta_range(stats["happiness"]), (-6, -4))
        self.assertEqual(delta_range(stats["stress"]), (3, 8))
        self.assertEqual(stats["health"], -1)
        kinds = [c["kind"] for c in event["conditions"]["requires"]]
        self.assertIn("min_age_sampled", kinds)


class RomancePackV3Test(unittest.TestCase):
    """M2.5 finish C3: the general romance/relationship v3 pack
    (v3_romance.json) — 30 adult-only events with the fixed-rule
    interaction metadata, first-person outcomes, a free exit on every
    event and valid role references, loadable through the real v3
    loader and reachable via the yearly drama pipeline pool."""

    PACK = os.path.join(CONTENT_DIR, "v3_romance.json")
    V3_STAT_KEYS = STAT_KEYS

    def setUp(self):
        self.doc = json.load(open(self.PACK))
        self.events = self.doc["events"]

    def test_pack_shape_and_count(self):
        self.assertEqual(self.doc["schema_version"], 3)
        self.assertEqual(len(self.events), 30)
        ids = [e["id"] for e in self.events]
        self.assertEqual(len(set(ids)), len(ids))
        self.assertTrue(all(i.startswith("rom_") for i in ids))

    def test_adult_only_romantic_interactions(self):
        for e in self.events:
            inter = e.get("interaction")
            self.assertIsNotNone(inter, e["id"])
            self.assertIn(inter["kind"],
                          {"flirt", "date", "propose_courtship",
                           "propose_union", "propose_plural_union",
                           "convert_request", "repair_attempt"}, e["id"])
            self.assertIs(inter["romantic"], True, e["id"])
            self.assertGreaterEqual(e.get("conditions", {}).get(
                "min_age", 18), 18, e["id"])

    def test_free_exit_and_first_person_outcomes(self):
        for e in self.events:
            free = False
            for c in e["choices"]:
                cost = (c.get("cost") or {}).get("money", 0)
                money = c.get("effects", {}).get("money", 0)
                negative = (isinstance(money, int) and money < 0) or (
                    isinstance(money, dict)
                    and money.get("sign") in (-1, "negative"))
                if cost <= 0 and not negative:
                    free = True
                self.assertFalse(
                    c.get("outcome_text", "").startswith("You "), e["id"])
            self.assertTrue(free, f"{e['id']}: no free exit")

    def test_text_roles_are_declared_and_audience_tagged(self):
        import re
        for e in self.events:
            person_roles = {n for n, cfg in e["roles"].items()
                            if cfg.get("kind", "person") == "person"}
            for token in re.findall(r"\{person:(\w+)\}", e["text"]):
                self.assertIn(token, person_roles, e["id"])
            self.assertEqual(e["audience"]["scope"], "player_known", e["id"])
            for p in e["audience"]["participants"]:
                self.assertIn(p, person_roles, e["id"])

    def test_loads_and_validates_through_real_loader(self):
        from src.game import events as event_mod
        import src.game.features.family_graph.predicates  # noqa: F401
        loaded = event_mod.load_event_files()
        mine = {e["id"]: e for e in loaded if e["id"].startswith("rom_")}
        self.assertEqual(len(mine), 30)
        for e in mine.values():
            event_mod.validate_event3(e)


if __name__ == "__main__":
    unittest.main()

    def test_reachable_through_v3_pipeline_on_real_service(self):
        # production route: real GameService env, graph backfilled from
        # legacy state; every pack event must either bind and instantiate
        # through instantiate3 (the yearly drama pipeline's exact call)
        # or be cleanly ineligible — never crash.
        import random
        import tempfile
        from src.game.service import GameService
        from src.game.events import EventEngine, GameContext
        from src.people.classes.partner import Partner
        from src.lifesim_lib.lifesim_lib import Gender
        from src.game.features.family_graph import core as graph_core
        tmp = tempfile.TemporaryDirectory(prefix="m25-rom-")
        self.addCleanup(tmp.cleanup)
        svc = GameService(tmp.name)
        state = svc.new_game({"first_name": "Ada", "last_name": "Lee",
                              "gender": "female", "country": "Australia",
                              "seed": 4242}, "create")
        gid = state["game_id"]
        env = svc._load(gid)
        player = env["player"]
        player.age = 30
        player.money = 5000
        partner = Partner(29, Gender.Male, 60, 60, 100, 0)
        player.partner = partner
        player.relations.append(partner)
        stranger = Partner(31, Gender.Female, 60, 60, 100, 0)
        player.relations.append(stranger)
        svc._ensure_ids(env)
        graph_core.backfill_all(env, svc)
        context = GameContext(env, svc)
        rng = random.Random(3)
        bound = 0
        for ev in self.events:
            roles = ev.get("roles") or {}
            ok = True
            for name, cfg in roles.items():
                if cfg.get("select", "player" if name == "actor" else None) \
                        == "player":
                    continue
                from src.game import events as event_mod
                if event_mod.PREDICATES[cfg["predicate"]](
                        player.person_id, context) is None \
                        and not cfg.get("optional"):
                    ok = False
            if not ok:
                continue
            pending = EventEngine.instantiate3(ev, 0, rng, context)
            self.assertEqual(pending["schema_dialect"], "v3", ev["id"])
            self.assertTrue(pending["options"], ev["id"])
            bound += 1
        # partner + stranger are present, so the partner/lover/stranger
        # events bind; only ex-events (no ended relationship) stay out
        self.assertGreaterEqual(bound, 25)
