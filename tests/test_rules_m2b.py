"""M2b rule tests: relationship + activity actions (parity rows 3-14, 25-36).

Real constructed Players/Partners/Parents; rng passed explicitly; money
asserted via money_entries (never direct mutation); choice outcomes
pre-sampled and stable across resolver RNGs.
"""

import random
import unittest

import src.menus.rule_actions as ra
from src.lifesim_lib.lifesim_lib import Gender
from src.people.classes.parent import Parent
from src.people.classes.partner import Partner
from tests.test_rules_income import make_player


def partner_for(player, **kw):
    defaults = dict(age=35, gender=Gender.Female, smarts=50, looks=50,
                    relationship=80, status=0)
    defaults.update(kw)
    p = Partner(defaults["age"], defaults["gender"], defaults["smarts"],
                defaults["looks"], defaults["relationship"],
                defaults["status"])
    player.partner = p
    player.relations.append(p)
    return p


def adult(**kw):
    kw.setdefault("age", 30)
    p = make_player(**kw)
    p.traits = {}
    return p


class SpendTimeTest(unittest.TestCase):
    def test_refusal_below_15(self):
        p = adult()
        rel = partner_for(p, relationship=5)
        before = p.happiness
        r = ra.spend_time(p, rel, random.Random(1))
        self.assertEqual(p.happiness, before - 4)
        self.assertIn("refused", r["events"][0]["text"])

    def test_enjoyment_applies_once_per_year(self):
        p = adult()
        rel = partner_for(p, relationship=80)
        r = ra.spend_time(p, rel, random.Random(2))
        self.assertTrue(rel.spent_time)
        self.assertIn("happiness", r["events"][0]["changes"])
        with self.assertRaises(ra.RuleError):
            ra.spend_time(p, rel, random.Random(3))


class ConversationTest(unittest.TestCase):
    def test_refusal_below_25(self):
        p = adult()
        rel = partner_for(p, relationship=10)
        with self.assertRaises(ra.RuleError):
            ra.sample_conversation(p, rel, random.Random(1))

    def test_argument_decision_resolves_stored_effects(self):
        p = adult()
        rel = partner_for(p, relationship=90)
        r = None
        for seed in range(50):
            rel.had_conversation = False
            rel.relationship = 90
            p.traits = {"GRUMPY": None}  # force low agreement rolls
            p.traits = {}
            p.happiness = 50
            r = ra.sample_conversation(p, rel, random.Random(seed))
            if r["pending_decision"]:
                break
        self.assertIsNotNone(r["pending_decision"])
        pending = r["pending_decision"]
        karma0 = p.karma
        rel0 = rel.relationship
        ra.resolve_argument(p, pending, "apologize", random.Random(9))
        self.assertEqual(p.karma, karma0 + pending["sampled"]["apologize"]
                         ["karma"])
        self.assertEqual(rel.relationship,
                         rel0 + pending["sampled"]["apologize"]
                         ["relationship"])
        with self.assertRaises(ra.RuleError):
            ra.resolve_argument(p, pending, "forge", random.Random(9))


class AskMoneyTest(unittest.TestCase):
    def test_gift_posts_money_entry(self):
        p = adult(age=15)
        parent = Parent("Lastname", 40, Gender.Female)
        parent.generosity = 100
        parent.money = 100
        parent.asked_for_money = 0
        p.relations.append(parent)
        rng = random.Random(4)
        for _ in range(50):
            parent.asked_for_money = 0
            r = ra.ask_for_money(p, parent, rng)
            if r["money_entries"]:
                break
        self.assertEqual(len(r["money_entries"]), 1)
        entry = r["money_entries"][0]
        self.assertEqual(entry["category"], "gift_received")
        self.assertGreater(entry["amount"], 0)
        self.assertEqual(entry["account_owner_id"], p.person_id)

    def test_third_ask_refused(self):
        p = adult()
        parent = Parent("Lastname", 40, Gender.Female)
        parent.asked_for_money = 3
        p.relations.append(parent)
        r = ra.ask_for_money(p, parent, random.Random(1))
        self.assertEqual(r["money_entries"], [])
        self.assertIn("stop asking", r["events"][0]["text"])


class ComplimentInsultTest(unittest.TestCase):
    def test_compliment_once_per_year(self):
        p = adult()
        rel = partner_for(p, relationship=90)
        ra.compliment(p, rel, random.Random(5))
        self.assertTrue(rel.was_complimented)
        with self.assertRaises(ra.RuleError):
            ra.compliment(p, rel, random.Random(6))

    def test_insult_lowers_relationship(self):
        p = adult()
        rel = partner_for(p, relationship=80)
        before = rel.relationship
        ra.insult(p, rel, random.Random(7))
        self.assertLess(rel.relationship, before)


class BabyTest(unittest.TestCase):
    def test_pregnancy_decision_stored_and_resolved(self):
        p = adult()
        partner_for(p, relationship=90)
        p.partner.years_together = 5
        r = None
        for seed in range(200):
            p.partner.is_pregnant = False
            p.is_pregnant = False
            r = ra.sample_try_for_baby(p, random.Random(seed))
            if r["pending_decision"]:
                break
        self.assertIsNotNone(r["pending_decision"])
        pending = r["pending_decision"]
        r2 = ra.resolve_pregnancy(p, pending, "keep", random.Random(99))
        if any("continues" in e["text"] for e in r2["events"]):
            self.assertTrue(p.partner.is_pregnant)  # male player
        # pre-sampled: a second resolution with a wild rng cannot reroll
        p.partner.is_pregnant = False
        r3 = ra.resolve_pregnancy(p, pending, "keep", random.Random(12345))
        self.assertEqual(
            [e["text"] for e in r2["events"]],
            [e["text"] for e in r3["events"]])

    def test_refusal_when_relationship_low(self):
        p = adult()
        partner_for(p, relationship=10)
        with self.assertRaises(ra.RuleError):
            ra.sample_try_for_baby(p, random.Random(1))


class PartnerLifecycleTest(unittest.TestCase):
    def test_propose_success_sets_engaged(self):
        p = adult()
        rel = partner_for(p, relationship=100)
        rel.years_together = 5
        rel.craziness = 0
        ra.propose(p, random.Random(8))
        self.assertEqual(rel.status, 1)

    def test_call_off_and_divorce_gates(self):
        p = adult()
        rel = partner_for(p)
        with self.assertRaises(ra.RuleError):
            ra.call_off_engagement(p)
        rel.status = 1
        ra.call_off_engagement(p)
        self.assertEqual(rel.status, 0)
        with self.assertRaises(ra.RuleError):
            ra.divorce_partner(p, random.Random(1))  # not married
        rel.status = 2
        r = ra.divorce_partner(p, random.Random(2))
        self.assertIsNone(p.partner)
        self.assertTrue(any(e["kind"] == "relationship"
                            for e in r["events"]))

    def test_wedding_plan_posts_cost_and_marries(self):
        p = adult(money=20000)
        rel = partner_for(p)
        rel.status = 1
        r = ra.sample_wedding_plan(p, random.Random(9))
        pending = r["pending_decision"]
        venue = next(o["id"] for o in pending["options"]
                     if o["id"] != "cancel")
        r2 = ra.resolve_wedding_plan(p, pending, venue, random.Random(10))
        self.assertEqual(rel.status, 2)
        (entry,) = r2["money_entries"]
        self.assertEqual(entry["category"], "wedding")
        self.assertLess(entry["amount"], 0)

    def test_wedding_unaffordable_rejected(self):
        p = adult(money=0)
        rel = partner_for(p)
        rel.status = 1
        r = ra.sample_wedding_plan(p, random.Random(9))
        pending = r["pending_decision"]
        venue = next(o["id"] for o in pending["options"]
                     if o["id"] != "cancel")
        with self.assertRaises(ra.RuleError):
            ra.resolve_wedding_plan(p, pending, venue, random.Random(10))

    def test_renew_vows_eligibility(self):
        p = adult(age=40)
        rel = partner_for(p, relationship=100)
        rel.status = 2
        rel.last_renew_vows = 30
        rel.asked_renew_vows = False
        r = ra.renew_vows(p, random.Random(11))
        self.assertIn("renewed", r["events"][0]["text"])
        with self.assertRaises(ra.RuleError):
            ra.renew_vows(p, random.Random(11))  # too soon


class ActivitiesTest(unittest.TestCase):
    def test_play_toys_age_gate_and_once(self):
        p = adult(age=3)
        p.played = False
        ra.play_toys(p, random.Random(1))
        self.assertTrue(p.played)
        with self.assertRaises(ra.RuleError):
            ra.play_toys(make_player(age=14), random.Random(1))

    def test_music_library_flags(self):
        p = adult(age=20)
        p.listened_to_music = False
        p.visited_library = False
        ra.listen_to_music(p, random.Random(2))
        self.assertTrue(p.listened_to_music)
        ra.library(p, random.Random(3))
        self.assertTrue(p.visited_library)

    def test_gym_requires_health(self):
        p = adult(age=20, health=5)
        with self.assertRaises(ra.RuleError):
            ra.gym(p, random.Random(1))

    def test_book_choice_resolves_sampled_effects(self):
        p = adult(age=30)
        r = ra.sample_read_book(p, random.Random(12))
        pending = r["pending_decision"]
        book = next(o["id"] for o in pending["options"]
                    if o["id"] != "back")
        s0, h0 = p.smarts, p.happiness
        ra.resolve_book_choice(p, pending, book, random.Random(13))
        self.assertGreater(p.smarts, s0)
        self.assertGreater(p.happiness, h0)


class HealthActivitiesTest(unittest.TestCase):
    def test_doctor_fee_entry_and_cure_cold(self):
        p = adult(money=500)
        from src.lifesim_lib.lifesim_lib import TranslateMarker
        p.illnesses = [TranslateMarker("Common Cold")]
        r = ra.sample_doctor_visit(p, random.Random(14))
        pending = r["pending_decision"]
        self.assertEqual(pending["sampled"]["outcomes"]["Common Cold"], True)
        r2 = ra.resolve_doctor_visit(p, pending, "treat:Common Cold",
                                     random.Random(15))
        (entry,) = r2["money_entries"]
        self.assertEqual(entry["amount"], -100)
        self.assertNotIn("Common Cold", p.illnesses)

    def test_doctor_unaffordable_rejected(self):
        p = adult(money=0)
        from src.lifesim_lib.lifesim_lib import TranslateMarker
        p.illnesses = [TranslateMarker("Flu")]
        r = ra.sample_doctor_visit(p, random.Random(14))
        with self.assertRaises(ra.RuleError):
            ra.resolve_doctor_visit(p, r["pending_decision"], "treat:Flu",
                                    random.Random(15))

    def test_witch_doctor_fatal_is_terminal_transition(self):
        p = adult(money=1000, health=1)
        r = ra.sample_witch_doctor(p, random.Random(16))
        pending = r["pending_decision"]
        # force the sampled fatal outcome
        pending["sampled"]["fatal"] = True
        pending["sampled"]["health_mod"] = -50
        r2 = ra.resolve_witch_doctor(p, pending, "take", random.Random(17))
        self.assertTrue(r2["finished_turn"])
        self.assertEqual(r2["continuation"]["phase_id"], "year_close")
        self.assertTrue(any(e["kind"] == "death" for e in r2["events"]))

    def test_plastic_surgery_gates(self):
        p = adult(money=100, age=30)
        p.last_plastic_surgery = p.age - 2  # too soon
        with self.assertRaises(ra.RuleError):
            ra.sample_plastic_surgery(p, random.Random(18))  # too soon
        p.last_plastic_surgery = 10
        r = ra.sample_plastic_surgery(p, random.Random(18))
        pending = r["pending_decision"]
        with self.assertRaises(ra.RuleError):
            ra.resolve_plastic_surgery(p, pending, "surgeon:0",
                                       random.Random(19))  # cannot afford

    def test_plastic_surgery_success(self):
        p = adult(money=30000, age=40)
        p.last_plastic_surgery = 10
        r = ra.sample_plastic_surgery(p, random.Random(18))
        pending = r["pending_decision"]
        # force success outcome
        for s in pending["sampled"]["per_surgeon"]:
            s["botched"] = False
            s["value"] = 20
        looks0 = p.looks
        r2 = ra.resolve_plastic_surgery(p, pending, "surgeon:0",
                                        random.Random(19))
        (entry,) = r2["money_entries"]
        self.assertEqual(entry["amount"], -25000)
        self.assertGreater(p.looks, looks0)
        self.assertEqual(p.last_plastic_surgery, p.age)


class AdoptionTest(unittest.TestCase):
    def test_adoption_flow_with_rename(self):
        p = adult(age=35, money=50000)
        p.children = []
        r = ra.sample_adoption(p, random.Random(20))
        pending = r["pending_decision"]
        r2 = ra.resolve_adoption_offer(p, pending, "adopt",
                                       random.Random(21))
        child = p.children[0]
        self.assertTrue(child.adopted)
        self.assertEqual(len(r2["money_entries"]), 1)
        rename = r2["pending_decision"]
        old_last = child.lastname
        ra.resolve_adoption_rename(p, rename, "rename", random.Random(22))
        self.assertEqual(child.lastname, p.lastname)
        self.assertNotEqual(child.lastname, old_last)

    def test_adoption_age_and_count_gates(self):
        p = adult(age=20)
        with self.assertRaises(ra.RuleError):
            ra.sample_adoption(p, random.Random(20))
        p = adult(age=35)
        p.children = [object()] * 5
        with self.assertRaises(ra.RuleError):
            ra.sample_adoption(p, random.Random(20))

    def test_find_partner_and_date_resolution(self):
        p = adult(age=25)
        r = ra.sample_find_partner(p, random.Random(23))
        pending = r["pending_decision"]
        before = p.date_options
        self.assertEqual(p.date_options, before)
        r2 = ra.resolve_date_offer(p, pending, "ask_out", random.Random(24))
        if pending["sampled"]["compatible"]:
            self.assertIsNotNone(p.partner)
        else:
            self.assertIsNone(p.partner)
            self.assertIn("rejected", r2["events"][0]["text"])


class DispatchTest(unittest.TestCase):
    def test_apply_action_routes_target_actions(self):
        p = adult()
        rel = partner_for(p, relationship=80)
        r = ra.apply_action(p, "compliment", {"target_id": rel.person_id},
                            random.Random(30))
        self.assertTrue(rel.was_complimented)
        with self.assertRaises(ra.RuleError):
            ra.apply_action(p, "compliment", {"target_id": "nope"},
                            random.Random(30))
        with self.assertRaises(ra.RuleError):
            ra.apply_action(p, "spend_time", {}, random.Random(30))

    def test_action_specs_include_m2b(self):
        p = adult(age=30)
        partner_for(p, relationship=80)
        ids = [s["id"] for s in ra.action_specs(p)]
        for expected in ("spend_time", "conversation", "compliment",
                         "insult", "doctor", "read_book", "meditate"):
            self.assertIn(expected, ids)
        self.assertIn("try_for_baby", [s["id"] for s in ra.action_specs(p)])


if __name__ == "__main__":
    unittest.main()


class MortalityHelpersTest(unittest.TestCase):
    def test_parent_death_inheritance_and_funeral(self):
        p = adult(age=30)
        parent = Parent("Lastname", 70, Gender.Female)
        parent.generosity = 100
        parent.money = 100
        p.relations.append(parent)
        r = None
        for seed in range(60):
            p.happiness = 50
            p.karma = 50
            parent.generosity = 100
            r = ra.sample_relation_death(p, parent, random.Random(seed))
            if r["money_entries"]:
                break
        self.assertTrue(r["money_entries"])
        entry = r["money_entries"][0]
        self.assertEqual(entry["category"], "inheritance")
        self.assertEqual(entry["phase"], "mortality")
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "funeral")
        karma0 = p.karma
        ra.resolve_funeral_choice(p, pending, "attend", random.Random(9))
        self.assertEqual(p.karma, karma0 + pending["sampled"]["attend"]
                         ["karma"])

    def test_funeral_skip_lowers_karma_and_unknown_option_rejected(self):
        p = adult()
        parent = Parent("Lastname", 90, Gender.Male)
        parent.generosity = 0
        p.relations.append(parent)
        r = ra.sample_relation_death(p, parent, random.Random(1))
        pending = r["pending_decision"]
        karma0 = p.karma
        ra.resolve_funeral_choice(p, pending, "skip", random.Random(2))
        self.assertLess(p.karma, karma0)
        with self.assertRaises(ra.RuleError):
            ra.resolve_funeral_choice(p, pending, "forge", random.Random(2))

    def test_sudden_death_reason_present(self):
        p = adult()
        parent = Parent("Lastname", 40, Gender.Female)
        p.relations.append(parent)
        r = ra.sample_relation_death(p, parent, random.Random(3), sudden=True)
        self.assertIn("died at the age of", r["events"][0]["text"])


class IllnessPhaseTest(unittest.TestCase):
    def test_cold_recovers_same_year(self):
        from src.lifesim_lib.lifesim_lib import TranslateMarker
        p = adult()
        p.illnesses = [TranslateMarker("Common Cold")]
        r = ra.illness_phase(p, "t1", random.Random(40))
        self.assertNotIn("Common Cold", p.illnesses)
        self.assertEqual(r["continuation"]["phase_id"], "annual_events")

    def test_cancer_death_is_terminal(self):
        from src.lifesim_lib.lifesim_lib import TranslateMarker
        p = adult(health=3)
        p.illnesses = [TranslateMarker("Cancer")]
        r = ra.illness_phase(p, "t1", random.Random(41))
        self.assertTrue(r["finished_turn"])
        self.assertEqual(r["continuation"]["phase_id"], "year_close")
        self.assertTrue(any(e["kind"] == "death" for e in r["events"]))

    def test_depression_recovery_branch(self):
        from src.lifesim_lib.lifesim_lib import TranslateMarker
        p = adult(happiness=90)
        p.illnesses = [TranslateMarker("Depression")]
        ra.illness_phase(p, "t1", random.Random(42))
        self.assertNotIn("Depression", p.illnesses)


class FamilyEventsPhaseTest(unittest.TestCase):
    def test_birth_creates_child_and_naming_decision(self):
        p = make_player(age=30, gender=Gender.Female)
        p.traits = {}
        father = partner_for(p)
        p.is_pregnant = True
        p.fertility = 100
        p._recent_child_father = father
        p.children = []
        r = None
        for seed in range(30):
            p.children = []
            p.relations = [father]
            p.is_pregnant = True
            p._recent_child_father = father
            r = ra.family_events_phase(p, "t1", random.Random(seed))
            if r["pending_decision"]:
                break
        self.assertIsNotNone(r["pending_decision"])
        pending = r["pending_decision"]
        self.assertEqual(pending["kind"], "baby_naming")
        child = pending["sampled_child"]
        self.assertIn(child, p.children)
        with self.assertRaises(ra.RuleError):
            ra.resolve_baby_naming(p, pending, "name", {"name": ""},
                                   random.Random(1))
        with self.assertRaises(ra.RuleError):
            ra.resolve_baby_naming(p, pending, "name", {"name": "x" * 41},
                                   random.Random(1))
        ra.resolve_baby_naming(p, pending, "name", {"name": "Jamie"},
                               random.Random(1))
        self.assertEqual(child.firstname, "Jamie")

    def test_miscarriage_branch_no_child(self):
        p = make_player(age=30, gender=Gender.Female)
        p.traits = {}
        p.fertility = 1  # x_in_y(1,150) fails -> miscarriage path possible
        father = partner_for(p)
        p._recent_child_father = father
        r = None
        for seed in range(80):
            p.children = []
            p.relations = [father]
            p.is_pregnant = True
            r = ra.family_events_phase(p, "t1", random.Random(seed))
            if not r["pending_decision"]:
                break
        self.assertIsNone(r["pending_decision"])
        self.assertEqual(p.children, [])
        self.assertTrue(any("miscarriage" in e["text"] for e in r["events"]))

    def test_child_money_year_posts_child_net(self):
        p = adult()
        child = make_player(age=20, salary=40000, has_job=False)
        child.person_id = child.person_id  # real object already
        p.children = [child]
        with mock.patch.object(ra, "sample_expense_retention",
                               return_value=0.6):
            entries, net = ra.child_money_year(child, random.Random(50))
        self.assertEqual(child.years_worked, 1)
        self.assertEqual(len(entries), 1)
        self.assertEqual(entries[0]["category"], "child_net")
        self.assertEqual(entries[0]["account_owner_id"], child.person_id)
        self.assertNotEqual(entries[0]["account_owner_id"], p.person_id)


from unittest import mock  # noqa: E402  (kept with other M2b imports)


class SurrenderTest(unittest.TestCase):
    def test_double_confirm_then_terminal(self):
        p = adult()
        r = ra.sample_surrender(p, random.Random(1))
        step1 = r["pending_decision"]
        self.assertEqual(step1["kind"], "surrender_confirm")
        r2 = ra.resolve_surrender_confirm(p, step1, "continue",
                                          random.Random(2))
        self.assertIsNone(r2["pending_decision"])
        # full path to the end
        r = ra.sample_surrender(p, random.Random(1))
        step2 = ra.resolve_surrender_confirm(
            p, r["pending_decision"], "ask_final", random.Random(2))[
            "pending_decision"]
        self.assertEqual(step2["kind"], "surrender_final")
        final = ra.resolve_surrender_final(p, step2, "surrender",
                                           random.Random(3))
        self.assertTrue(final["finished_turn"])
        self.assertEqual(final["continuation"]["phase_id"], "year_close")
        self.assertTrue(any(e["kind"] == "death" for e in final["events"]))
        # cancel at the final step
        cancel = ra.resolve_surrender_final(p, step2, "cancel",
                                            random.Random(3))
        self.assertFalse(cancel["finished_turn"])
        with self.assertRaises(ra.RuleError):
            ra.resolve_surrender_final(p, step2, "forge", random.Random(3))

    def test_dispatch(self):
        p = adult()
        r = ra.apply_action(p, "surrender", {}, random.Random(1))
        self.assertEqual(r["pending_decision"]["kind"], "surrender_confirm")


class SuccessionTest(unittest.TestCase):
    def _family(self, money=100003):
        p = make_player(age=70, money=money, generation=2)
        c1 = make_player(age=20, health=80)
        c2 = make_player(age=15, health=90)
        p.children = [c1, c2]
        return p, c1, c2

    def test_eligibility(self):
        p, c1, c2 = self._family()
        self.assertEqual(ra.eligible_successors(p), [c1, c2])
        c2.health = 0
        self.assertEqual(ra.eligible_successors(p), [c1])
        with self.assertRaises(ra.RuleError):
            ra.succession_instructions(p, make_player(age=5),
                                       random.Random(1))

    def test_balanced_cash_transfer(self):
        p, c1, c2 = self._family(money=100003)
        inst = ra.succession_instructions(p, c1, random.Random(7))
        self.assertEqual(inst["cash_share"] + inst["estate_residual"],
                         p.money)  # nothing manufactured or dropped
        amounts = [abs(e["amount"]) for e in inst["money_entries"]]
        self.assertEqual(sum(amounts), p.money)  # balanced legs: no gap
        share_entry, residual_entry = inst["money_entries"]
        self.assertEqual(share_entry["account_owner_id"], c1.person_id)
        self.assertEqual(share_entry["category"], "transfer")
        self.assertEqual(share_entry["amount"], inst["cash_share"])
        self.assertEqual(residual_entry["amount"], -inst["estate_residual"])
        self.assertEqual(inst["generation"], 3)

    def test_zero_money_no_entries(self):
        p, c1, _ = self._family(money=0)
        inst = ra.succession_instructions(p, c1, random.Random(7))
        self.assertEqual(inst["money_entries"], [])
        self.assertEqual(inst["cash_share"], 0)
        self.assertEqual(inst["estate_residual"], 0)

    def test_gameplay_carryover(self):
        p, c1, c2 = self._family()
        c1.salary = 45000
        c1.years_worked = 6
        c1.is_in_uv = 0
        inst = ra.succession_instructions(p, c1, random.Random(3))
        self.assertEqual(inst["gameplay"]["job"]["salary"], 45000)
        self.assertEqual(inst["gameplay"]["job"]["years_worked"], 6)
        self.assertEqual(inst["gameplay"]["age"], c1.age)
        self.assertNotIn("university", inst["gameplay"])
        # university path
        c1.salary = 0
        c1.is_in_uv = 3
        inst = ra.succession_instructions(p, c1, random.Random(3))
        self.assertEqual(inst["gameplay"]["university"]["uv_years"], 3)
        self.assertNotIn("job", inst["gameplay"])
        # sibling list excludes the successor; identity preserved fields
        self.assertEqual(inst["sibling_ids"], [c2.person_id])
        self.assertEqual(inst["successor_id"], c1.person_id)
        self.assertIn("appearance", inst["preserve"])


class LotteryTest(unittest.TestCase):
    def test_buy_one_win_and_jackpot_reroll(self):
        p = adult(money=100)
        p.lottery_jackpot = 250000
        r = ra.sample_lottery(p, random.Random(2))
        pending = r["pending_decision"]
        winning = pending["sampled"]["winning"]
        r2 = ra.resolve_lottery(p, pending, "buy_one",
                                {"numbers": list(winning)}, random.Random(3))
        legs = {m["leg"]: m for m in r2["money_entries"]}
        self.assertEqual(legs["tickets"]["amount"], -4)
        self.assertEqual(legs["jackpot"]["amount"], 250000)
        self.assertEqual(p.lottery_jackpot, pending["sampled"]["next_jackpot"])
        self.assertEqual(p.happiness, 100)

    def test_loss_and_back(self):
        p = adult(money=100)
        p.lottery_jackpot = 250000
        r = ra.sample_lottery(p, random.Random(2))
        pending = r["pending_decision"]
        guess = [n % 20 + 1 for n in pending["sampled"]["winning"]]
        if sorted(guess) == pending["sampled"]["winning"]:
            guess = list(reversed(guess))
        r2 = ra.resolve_lottery(p, pending, "buy_one", {"numbers": guess},
                                random.Random(3))
        self.assertEqual(len(r2["money_entries"]), 1)
        back = ra.resolve_lottery(p, pending, "back", {}, random.Random(3))
        self.assertEqual(back["money_entries"], [])

    def test_input_validation_and_affordability(self):
        p = adult(money=100)
        r = ra.sample_lottery(p, random.Random(2))
        pending = r["pending_decision"]
        for bad in ([1, 2, 3], [1, 2, 3, 3], [1, 2, 3, 21], [1, 2, 3, "x"]):
            with self.assertRaises(ra.RuleError):
                ra.resolve_lottery(p, pending, "buy_one", {"numbers": bad},
                                   random.Random(3))
        poor = adult(money=2)
        r = ra.sample_lottery(poor, random.Random(2))
        with self.assertRaises(ra.RuleError):
            ra.resolve_lottery(poor, r["pending_decision"], "buy_one",
                               {"numbers": [1, 2, 3, 4]}, random.Random(3))
        with self.assertRaises(ra.RuleError):
            ra.resolve_lottery(p, pending, "forge", {"numbers": [1, 2, 3, 4]},
                               random.Random(3))

    def test_buy_ten(self):
        p = adult(money=100)
        r = ra.sample_lottery(p, random.Random(2))
        pending = r["pending_decision"]
        lines = [[1, 2, 3, 4]] * 10
        r2 = ra.resolve_lottery(p, pending, "buy_ten", {"numbers": lines},
                                random.Random(3))
        (ticket,) = [m for m in r2["money_entries"] if m["leg"] == "tickets"]
        self.assertEqual(ticket["amount"], -40)


class FindPartnerAfterDivorceTest(unittest.TestCase):
    """D gap M2b-S2 root cause: find_partner was dispatchable but never
    projected in action_specs, so the browser never offered it."""

    def test_find_partner_spec_after_divorce(self):
        p = adult(age=35)
        partner_for(p)
        p.partner.status = 2
        self.assertNotIn("find_partner", [s["id"]
                                          for s in ra.action_specs(p)])
        ra.divorce_partner(p, random.Random(1))
        self.assertIsNone(p.partner)
        ids = [s["id"] for s in ra.action_specs(p)]
        self.assertIn("find_partner", ids)

    def test_find_partner_spec_gates(self):
        p = adult(age=35)  # single
        self.assertIn("find_partner", [s["id"] for s in ra.action_specs(p)])
        p.date_options = 0
        self.assertNotIn("find_partner", [s["id"]
                                          for s in ra.action_specs(p)])
        p.date_options = 10
        partner_for(p)  # partnered
        self.assertNotIn("find_partner", [s["id"]
                                          for s in ra.action_specs(p)])


class OncePerYearLimitSweepTest(unittest.TestCase):
    """Every once-per-year action: second use in the same year is a typed
    refusal (displayed enabled/reason always matches dispatch)."""

    def test_activity_limits(self):
        cases = [
            ("play_toys", lambda p: ra.play_toys(p, random.Random(1)),
             dict(age=5), "played"),
            ("arts_and_crafts",
             lambda p: ra.arts_and_crafts(p, random.Random(1)),
             dict(age=20), "did_arts_and_crafts"),
            ("listen_to_music",
             lambda p: ra.listen_to_music(p, random.Random(1)),
             dict(age=20), "listened_to_music"),
            ("meditate", lambda p: ra.meditate(p, random.Random(1)),
             dict(age=20), "meditated"),
            ("gym", lambda p: ra.gym(p, random.Random(1)),
             dict(age=20, health=50), "worked_out"),
            ("library", lambda p: ra.library(p, random.Random(1)),
             dict(age=20), "visited_library"),
        ]
        for name, fn, overrides, flag in cases:
            p = adult(**overrides)
            fn(p)
            self.assertTrue(getattr(p, flag), name)
            with self.assertRaises(ra.RuleError, msg=name):
                fn(p)

    def test_specs_match_dispatch(self):
        p = adult(age=20)
        p.meditated = True
        p.played = True
        specs = {s["id"]: s for s in ra.action_specs(p)}
        for used in ("meditate", "gym", "library", "listen_to_music"):
            p2 = adult(age=20)
            getattr(ra, {"meditate": "meditate", "gym": "gym",
                         "library": "library",
                         "listen_to_music": "listen_to_music"}[used])(
                p2, random.Random(1))
            specs2 = {s["id"]: s for s in ra.action_specs(p2)}
            self.assertFalse(specs2[used]["enabled"], used)
            with self.assertRaises(ra.RuleError):
                getattr(ra, used)(p2, random.Random(1))


import json as _json


class JournalTextQualityTest(unittest.TestCase):
    def test_small_relationship_delta_journals_nothing(self):
        """Silent yearly drift must not appear as filler journal text;
        a real sentence appears only for significant swings (>=5)."""
        p = adult()
        rel = partner_for(p)
        pending = {
            "id": "d_1", "event_id": "flavor_test", "title": "T",
            "text": "x", "person_ids": [p.person_id],
            "options": [{
                "id": "d_1_a", "label": "A", "known_effects": [],
                "enabled": True, "reason": None, "inputs": [],
                "_outcome": "It happened.",
                "_effects": {"relationship": -2},
                "_person_ids_extra": [rel.person_id],
            }],
        }
        r = ra.resolve_choice(p, pending, "d_1_a", {}, random.Random(1))
        drift_texts = [e for e in r["events"]
                       if "relationship changed" in e["text"]]
        self.assertEqual(drift_texts, [])
        # significant swing -> real sentence
        pending2 = _json.loads(_json.dumps(pending))
        pending2["options"][0]["_effects"] = {"relationship": -9}
        rel2 = partner_for(p)
        pending2["options"][0]["_person_ids_extra"] = [rel2.person_id]
        r2 = ra.resolve_choice(p, pending2, "d_1_a", {}, random.Random(1))
        sentences = [e for e in r2["events"] if "drifted apart" in e["text"]]
        self.assertEqual(len(sentences), 1)

    def test_illness_names_lowercase_in_sentences(self):
        from src.lifesim_lib.lifesim_lib import TranslateMarker
        # illness-phase recovery text
        p = adult()
        p.illnesses = [TranslateMarker("Common Cold")]
        r = ra.illness_phase(p, "t1", random.Random(40))
        texts = [e["text"] for e in r["events"]]
        # doctor cure text
        p2 = adult(money=500)
        p2.illnesses = [TranslateMarker("Common Cold")]
        r2 = ra.sample_doctor_visit(p2, random.Random(14))
        out = ra.resolve_doctor_visit(p2, r2["pending_decision"],
                                      "treat:Common Cold", random.Random(15))
        texts += [e["text"] for e in out["events"]]
        self.assertTrue(any("the common cold" in t for t in texts), texts)
