"""Appearance engine tests — schema v2 (Fluent avatars) per
docs/collab/M0b-examples.json: model round-trip, v1->v2 migration,
generation, inheritance, aging, integration/save invariants, identity
stability (ported from the v1 suite and rewritten for v2)."""

import glob
import io
import json
import os
import pickle
import random
import subprocess
import sys
import unittest

from src.appearance import catalog
from src.appearance.aging import (aged_view, age_stage, effective_hair,
                                  greying_onset_year)
from src.appearance.generate import (age_appropriate, inherit_appearance,
                                     random_appearance)
from src.appearance.integration import (copy_appearance, ensure_appearance,
                                        migrate_appearances)
from src.appearance.model import Appearance, SCHEMA_VERSION
from src.lifesim_lib.lifesim_lib import Gender

FIXTURE_DIR = os.path.join(os.path.dirname(__file__), "fixtures",
                           "appearances")

PHYSICAL = ("skin_tone", "hair")

V1_SAMPLE = {
    "schema_version": 1, "seed": 7, "skin_tone": "skin_03",
    "face_shape": "oval", "eye_shape": "almond", "eye_color": "brown_dark",
    "brows": "soft", "nose": "button", "mouth": "smile",
    "hair_texture": "curly", "hair_color": "black", "marks": ["freckles"],
    "hair_style": "short_crop", "facial_hair": "none",
    "accessories": {"glasses": "none", "headwear": "none",
                    "earrings": "none"},
    "outfit_style": "tee", "outfit_color": "blue",
}


def rng(seed=1):
    return random.Random(seed)


class ModelCatalogTests(unittest.TestCase):
    def test_catalog_meets_contract_minimums(self):
        self.assertGreaterEqual(len(catalog.all_ids("skin_tone")), 6)
        self.assertGreaterEqual(len(catalog.all_ids("hair")), 5)
        self.assertGreaterEqual(len(catalog.all_ids("badges")), 5)
        self.assertGreaterEqual(len(catalog.all_ids("background")), 10)
        self.assertGreaterEqual(len(catalog.all_ids("look_override")), 13)
        self.assertEqual(catalog.all_ids("base"),
                         ["person", "woman", "man"])
        self.assertEqual(catalog.default_id("base"), "person")
        self.assertEqual(catalog.default_id("facial_hair"), "none")

    def test_skin_tones_ordered_light_to_dark(self):
        tones = catalog.all_ids("skin_tone")
        self.assertEqual(tones[0], "default")
        self.assertEqual(tones[-1], "dark")
        self.assertIn("medium-light", tones)

    def test_from_dict_tolerant_unknown_ids_fall_back_to_defaults(self):
        app = Appearance.from_dict({"base": "alien", "hair": "mohawk",
                                    "badges": ["crown", "crown", "alien"]})
        self.assertEqual(app.base, "person")
        self.assertEqual(app.hair, "default")
        # unknown badge id normalizes to the first badge id; duplicates drop
        self.assertEqual(app.badges, ["crown", "glasses"])

    def test_from_dict_garbage_input(self):
        for garbage in (None, 42, "x", [], object()):
            self.assertIsInstance(Appearance.from_dict(garbage), Appearance)

    def test_round_trip(self):
        app = Appearance(seed=5, base="woman", skin_tone="dark",
                         hair="curly", badges=["glasses", "flower"],
                         background="bg_04", look_override="bride")
        self.assertEqual(Appearance.from_dict(app.to_dict()).to_dict(),
                         app.to_dict())

    def test_rejects_newer_version(self):
        with self.assertRaises(ValueError):
            Appearance.from_dict({"schema_version": 99})

    def test_refine_base_from_gender(self):
        app = Appearance.from_dict(dict(V1_SAMPLE))
        self.assertEqual(app.base, "person")
        app.refine_base_from_gender(Gender.Male)
        self.assertEqual(app.base, "man")


class MigrationTest(unittest.TestCase):
    def test_contract_example(self):
        app = Appearance.from_dict(dict(V1_SAMPLE))
        self.assertEqual(app.schema_version, 2)
        self.assertEqual(app.seed, 7)
        self.assertEqual(app.skin_tone, "light")  # skin_03 -> 2nd bucket
        self.assertEqual(app.hair, "curly")
        self.assertEqual(app.facial_hair, "none")
        self.assertEqual(app.badges, [])
        self.assertEqual(app.background, "bg_08")  # 7 % 10 + 1

    def test_skin_pairs_all_twelve(self):
        expected = ["default", "default", "light", "light",
                    "medium-light", "medium-light", "medium", "medium",
                    "medium-dark", "medium-dark", "dark", "dark"]
        for i, want in enumerate(expected, start=1):
            data = dict(V1_SAMPLE)
            data["skin_tone"] = f"skin_{i:02d}"
            self.assertEqual(Appearance.from_dict(data).skin_tone, want,
                             f"skin_{i:02d}")

    def test_hair_derivation(self):
        base = {"hair_texture": "straight", "hair_color": "black",
                "hair_style": "short_crop"}
        cases = [
            ({"hair_texture": "curly"}, "curly"),
            ({"hair_texture": "coily"}, "curly"),
            ({"hair_color": "red"}, "red"),
            ({"hair_color": "auburn"}, "red"),
            ({"hair_style": "bald"}, "bald"),
            ({"hair_color": "gray"}, "white"),
            ({"hair_color": "white"}, "white"),
            (dict(base), "default"),
        ]
        for patch, want in cases:
            data = dict(V1_SAMPLE)
            data.update(base)
            data.update(patch)
            self.assertEqual(Appearance.from_dict(data).hair, want, patch)

    def test_badges_and_headwear(self):
        data = dict(V1_SAMPLE)
        data["accessories"] = {"glasses": "round", "earrings": "studs",
                               "headwear": "cap"}
        app = Appearance.from_dict(data)
        self.assertEqual(app.badges, ["glasses", "earrings"])  # cap over cap
        data["accessories"]["headwear"] = "headscarf"
        app = Appearance.from_dict(data)
        self.assertEqual(app.headwear, "headscarf")

    def test_beard_migration(self):
        data = dict(V1_SAMPLE)
        data["facial_hair"] = "beard_full"
        self.assertEqual(Appearance.from_dict(data).facial_hair, "beard")


class GenerationTests(unittest.TestCase):
    def test_generation_is_deterministic_for_same_seed(self):
        a = random_appearance(random.Random(42), 25)
        b = random_appearance(random.Random(42), 25)
        self.assertEqual(a.to_dict(), b.to_dict())
        m = random_appearance(random.Random(1), 30)
        f = random_appearance(random.Random(2), 30)
        c1 = inherit_appearance(random.Random(99), m, f)
        c2 = inherit_appearance(random.Random(99), m, f)
        self.assertEqual(c1.to_dict(), c2.to_dict())

    def test_generation_never_touches_global_random_state(self):
        random.seed(1234)
        before = random.getstate()
        r = random.Random(0)
        for age in (0, 5, 13, 30, 70):
            random_appearance(r, age)
        m = random_appearance(random.Random(3), 30)
        f = random_appearance(random.Random(4), 30)
        inherit_appearance(r, m, f)

        class Fake:
            age = 20
            gender = None
            adopted = False

        ensure_appearance(Fake())
        self.assertEqual(random.getstate(), before)

    def test_babies_have_no_accessories(self):
        r = random.Random(7)
        for _ in range(50):
            app = random_appearance(r, r.randint(0, 2))
            self.assertEqual(app.badges, [])
            self.assertEqual(app.facial_hair, "none")
            self.assertEqual(app.headwear, "none")

    def test_children_have_no_facial_hair_or_badges(self):
        r = random.Random(8)
        for _ in range(50):
            app = random_appearance(r, r.randint(3, 12))
            self.assertEqual(app.facial_hair, "none")
            self.assertEqual(app.badges, [])

    def test_badge_cap_two(self):
        for seed in range(50):
            app = random_appearance(rng(seed), age=30)
            self.assertLessEqual(len(app.badges), 2)

    def test_inheritance_biological_vs_adoption(self):
        r = random.Random(20)
        mother = random_appearance(random.Random(21), 35)
        father = random_appearance(random.Random(22), 37)
        order = catalog.all_ids("skin_tone")
        for _ in range(30):
            child = inherit_appearance(r, mother, father)
            # skin: one parent's tone, or an in-between tone on the scale
            a = order.index(mother.skin_tone)
            b = order.index(father.skin_tone)
            self.assertTrue(min(a, b) <= order.index(child.skin_tone)
                            <= max(a, b))
            # hair: a parent's hair, or the documented 10% mutation pool
            self.assertIn(child.hair,
                          {mother.hair, father.hair,
                           "default", "curly", "red"})
            # styling never inherited
            self.assertEqual(child.badges, [])
            self.assertEqual(child.headwear, "none")
            self.assertEqual(child.facial_hair, "none")
        self.assertIsInstance(inherit_appearance(r, None, None), Appearance)

    def test_inheritance_bounded_variation(self):
        r = random.Random(31)
        mother = random_appearance(random.Random(32), 35)
        father = random_appearance(random.Random(33), 37)
        for _ in range(60):
            child = inherit_appearance(r, mother, father)
            diffs = sum(
                getattr(child, f) not in
                (getattr(mother, f), getattr(father, f)) for f in PHYSICAL)
            self.assertLessEqual(diffs, 1)


class AgingTests(unittest.TestCase):
    def test_age_stages(self):
        self.assertEqual(age_stage(0), "baby")
        self.assertEqual(age_stage(2), "baby")
        self.assertEqual(age_stage(3), "child")
        self.assertEqual(age_stage(12), "child")
        self.assertEqual(age_stage(13), "teen_adult")
        self.assertEqual(age_stage(59), "teen_adult")
        self.assertEqual(age_stage(60), "greying")
        self.assertEqual(age_stage(69), "greying")
        self.assertEqual(age_stage(70), "elder")

    def test_aged_view_progression_is_deterministic(self):
        app = random_appearance(random.Random(60), 20)
        young = aged_view(app, 25)
        old = aged_view(app, 80)
        self.assertEqual(young["stage"], "teen_adult")
        self.assertEqual(old["stage"], "elder")
        self.assertEqual(aged_view(app, 80), old)
        # original appearance is unchanged by ageing
        self.assertNotEqual(app.hair, "white")

    def test_effective_hair_white_after_onset(self):
        app = Appearance(seed=61, hair="default")
        onset = greying_onset_year(app)
        self.assertTrue(60 <= onset <= 64)
        self.assertEqual(effective_hair(app, onset - 1), "default")
        self.assertEqual(effective_hair(app, onset), "white")
        self.assertEqual(effective_hair(app, 80), "white")

    def test_bald_stays_bald(self):
        app = Appearance(seed=60, hair="bald")
        self.assertEqual(effective_hair(app, 80), "bald")

    def test_stored_hair_unchanged_by_aging(self):
        app = Appearance(seed=60, hair="default")
        aged_view(app, 80)
        self.assertEqual(app.hair, "default")

    def test_age_appropriate_baby_rules_in_place(self):
        app = random_appearance(random.Random(61), 30)
        app.badges = ["glasses", "cap"]
        app.facial_hair = "beard"
        app.headwear = "headscarf"
        age_appropriate(app, 1)
        self.assertEqual(app.badges, [])
        self.assertEqual(app.facial_hair, "none")
        self.assertEqual(app.headwear, "none")

    def test_aged_view_masks_young_styling(self):
        app = Appearance(seed=1, badges=["glasses"], facial_hair="beard",
                         headwear="headscarf")
        self.assertEqual(aged_view(app, 1)["facial_hair"], "none")
        self.assertEqual(aged_view(app, 1)["badges"], [])
        self.assertEqual(aged_view(app, 1)["headwear"], "none")
        self.assertEqual(aged_view(app, 45)["facial_hair"], "beard")


class FixtureTests(unittest.TestCase):
    def test_fixtures_are_valid_and_round_trip(self):
        files = sorted(glob.glob(os.path.join(FIXTURE_DIR, "*.json")))
        self.assertGreaterEqual(len(files), 12)
        for path in files:
            with open(path) as f:
                data = json.load(f)
            if "appearance" in data:
                data = data["appearance"]
            app = Appearance.from_dict(data)  # v1 fixtures migrate
            self.assertEqual(
                Appearance.from_dict(app.to_dict()).to_dict(), app.to_dict(),
                path)


class IntegrationTests(unittest.TestCase):
    def test_ensure_appearance_refines_base(self):
        class P:
            pass
        p = P()
        p.appearance = dict(V1_SAMPLE)
        p.age = 40
        p.gender = Gender.Female
        p.adopted = False
        app = ensure_appearance(p)
        self.assertEqual(app.schema_version, 2)
        self.assertEqual(app.base, "woman")

    def test_all_person_subclasses_get_appearances(self):
        from src.people.classes.player import Player
        from src.people.classes.sibling import Sibling
        from src.people.classes.child import Child

        player = Player()
        self.assertIsInstance(player.appearance, Appearance)
        for parent in player.parents.values():
            self.assertIsInstance(parent.appearance, Appearance)
        player.age = 30
        partner = player.generate_partner()
        self.assertIsInstance(partner.appearance, Appearance)
        sibling = Sibling("Smith", 7, Gender.Male, 50, 50)
        self.assertIsInstance(sibling.appearance, Appearance)

        mother = player.parents["Mother"]
        father = player.parents["Father"]
        bio = Child("Bio", "Smith", Gender.Female, 50, 50, mother, father)
        self.assertIsInstance(bio.appearance, Appearance)
        bio_diffs = sum(
            getattr(bio.appearance, f) not in
            (getattr(mother.appearance, f), getattr(father.appearance, f))
            for f in PHYSICAL)
        self.assertLessEqual(bio_diffs, 1)

        adopted = Child("Ado", "Lee", Gender.Male, 50, 50, adopted=True)
        self.assertIsInstance(adopted.appearance, Appearance)

        late = Child("Late", "Smith", Gender.Male, 50, 50)
        self.assertFalse(late._appearance_finalized)
        late.set_parent(mother)
        self.assertTrue(late._appearance_finalized)
        late_diffs = sum(
            getattr(late.appearance, f) not in
            (getattr(mother.appearance, f), getattr(father.appearance, f))
            for f in PHYSICAL)
        self.assertLessEqual(late_diffs, 1)

    def test_ensure_appearance_idempotent(self):
        from src.people.classes.sibling import Sibling
        s = Sibling("Smith", 9, Gender.Female, 50, 50)
        first = s.appearance.to_dict()
        ensure_appearance(s)
        self.assertEqual(s.appearance.to_dict(), first)  # kept, not replaced

    def test_copy_appearance(self):
        app = Appearance(seed=9, look_override="graduate")
        clone = copy_appearance(app)
        self.assertEqual(clone.to_dict(), app.to_dict())
        self.assertIsNot(clone, app)


class Evil:
    """Module-level class not in the save allowlist."""


class SaveTests(unittest.TestCase):
    @staticmethod
    def _strip_appearances(obj, seen=None):
        from src.people.classes.person import Person
        if seen is None:
            seen = set()
        if id(obj) in seen:
            return
        seen.add(id(obj))
        if isinstance(obj, Person):
            obj.__dict__.pop("appearance", None)
            for v in vars(obj).values():
                SaveTests._strip_appearances(v, seen)
        elif isinstance(obj, dict):
            for v in obj.values():
                SaveTests._strip_appearances(v, seen)
        elif isinstance(obj, (list, tuple, set)):
            for v in obj:
                SaveTests._strip_appearances(v, seen)

    def test_old_save_migration_assigns_appearances(self):
        from src.people.classes.player import Player
        from src.people.classes.person import Person
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        player.age = 18  # biological creation uses the protected adult guard
        kid = player.generate_child(player.parents["Mother"])
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        self._strip_appearances(player)

        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))

        seen = set()

        def check(obj):
            if id(obj) in seen:
                return
            seen.add(id(obj))
            if isinstance(obj, Person):
                self.assertIsInstance(obj.appearance, Appearance, type(obj))
                for v in vars(obj).values():
                    check(v)
            elif isinstance(obj, dict):
                for v in obj.values():
                    check(v)
            elif isinstance(obj, (list, tuple, set)):
                for v in obj:
                    check(v)

        check(loaded)

    def test_new_save_round_trip_preserves_appearances(self):
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        data = load_save(buf)
        loaded = Player.load(data)
        self.assertEqual(loaded.appearance.to_dict(),
                         player.appearance.to_dict())
        for orig, new in zip(player.relations, loaded.relations):
            self.assertEqual(new.appearance.to_dict(),
                             orig.appearance.to_dict())

    def test_migrate_dict_v1_save_shape(self):
        from src.people.classes.person import Person
        p = Person("Test", "Person", 30, Gender.Male, 50, 50, 50, 50)
        p.appearance = dict(V1_SAMPLE)
        migrate_appearances(p)
        self.assertEqual(p.appearance.schema_version, 2)
        self.assertEqual(p.appearance.base, "man")

    def test_unpickler_still_blocks_unknown_globals(self):
        buf = io.BytesIO(pickle.dumps(Evil()))
        from src.lifesim_lib.safe_save import load_save
        with self.assertRaises(pickle.UnpicklingError):
            load_save(buf)


class SuccessorTests(unittest.TestCase):
    def test_successor_is_not_own_sibling_and_relatives_keep_identity(self):
        from src.people.classes.player import Player
        from src.people.classes.sibling import Sibling

        player = Player()
        player.age = 18
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        successor = player.generate_child(mother)
        successor.age = 10
        successor.money = 0
        successor.salary = 0
        successor.partner = None
        successor.mother = mother
        successor.father = father
        other = player.generate_child(mother)
        other.age = 12
        other.mother = mother
        other.father = father
        player.children = [successor, other]

        player.convert_child_to_player(successor)

        sibling_objs = [r for r in player.relations
                        if isinstance(r, Sibling)]
        self.assertEqual(len(sibling_objs), 1)  # only the other child
        self.assertEqual(sibling_objs[0].firstname, other.firstname)
        self.assertEqual(sibling_objs[0].appearance.to_dict(),
                         other.appearance.to_dict())
        self.assertEqual(player.appearance.to_dict(),
                         successor.appearance.to_dict())
        self.assertEqual(player.person_id, successor.person_id)
        new_mother = player.parents["Mother"]
        self.assertEqual(new_mother.firstname, mother.firstname)
        self.assertEqual(new_mother.appearance.to_dict(),
                         mother.appearance.to_dict())
        new_father = player.parents["Father"]
        self.assertEqual(new_father.firstname, father.firstname)
        self.assertEqual(new_father.appearance.to_dict(),
                         father.appearance.to_dict())
        self.assertEqual(new_mother.person_id, mother.person_id)
        self.assertEqual(new_father.person_id, father.person_id)
        self.assertEqual(sibling_objs[0].person_id, other.person_id)


class FixRound1Tests(unittest.TestCase):
    """Identity/finalization invariants (ported from the v1 suite)."""

    def _player_with_bio_child(self):
        from src.people.classes.player import Player
        player = Player()
        player.age = 18  # biological creation uses the protected adult guard
        kid = player.generate_child(player.parents["Mother"])
        kid.mother = player.parents["Mother"]
        kid.father = player.parents["Father"]
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        return player, kid

    def test_migration_preserves_saved_child_and_is_idempotent(self):
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player, kid = self._player_with_bio_child()
        kid_app = kid.appearance.to_dict()
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        loaded_kid = loaded.children[0]
        self.assertEqual(loaded_kid.appearance.to_dict(), kid_app)
        migrate_appearances(loaded)
        self.assertEqual(loaded_kid.appearance.to_dict(), kid_app)
        self.assertEqual(loaded.appearance.to_dict(),
                         player.appearance.to_dict())

    def test_inheritance_stable_across_pythonhashseeds(self):
        code = (
            "import sys; sys.path.insert(0, '.')\n"
            "import random\n"
            "from src.appearance.model import Appearance\n"
            "from src.appearance.generate import inherit_appearance\n"
            "m = Appearance(seed=1, skin_tone='light', hair='curly')\n"
            "f = Appearance(seed=2, skin_tone='dark', hair='red')\n"
            "c = inherit_appearance(random.Random(7), m, f)\n"
            "print(c.to_dict())\n"
        )
        outputs = set()
        for seed in ("0", "1", "2", "3"):
            import os as _os
            env = dict(_os.environ, PYTHONHASHSEED=seed)
            out = subprocess.run(
                [sys.executable, "-c", code], capture_output=True, text=True,
                env=env, timeout=60)
            self.assertEqual(out.returncode, 0, out.stderr)
            outputs.add(out.stdout.strip())
        self.assertEqual(len(outputs), 1, outputs)

    def test_schema_version_policy(self):
        good = Appearance(seed=3).to_dict()
        self.assertEqual(Appearance.from_dict(good).schema_version,
                         SCHEMA_VERSION)
        for bad_version in (999, 0, -1, "1", 1.5, True):
            data = dict(good, schema_version=bad_version)
            with self.assertRaises(ValueError, msg=repr(bad_version)):
                Appearance.from_dict(data)
        missing = dict(good)
        missing.pop("schema_version")  # missing = current, must load
        self.assertIsInstance(Appearance.from_dict(missing), Appearance)

    def test_loader_replaces_unsupported_version_appearance(self):
        from src.people.classes.sibling import Sibling
        from src.appearance.integration import ensure_appearance
        s = Sibling("Smith", 20, Gender.Male, 50, 50)
        s.appearance.schema_version = 999
        ensure_appearance(s)
        self.assertIsInstance(s.appearance, Appearance)
        self.assertEqual(s.appearance.schema_version, SCHEMA_VERSION)

    def test_pickled_instance_with_unknown_id_is_normalized(self):
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player, _kid = self._player_with_bio_child()
        player.appearance.skin_tone = "skin_999"  # stale ID (either generation)
        player.appearance.badges = ["xray_specs"]
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        self.assertEqual(loaded.appearance.skin_tone,
                         catalog.default_id("skin_tone"))
        # valid fields were not rerolled
        self.assertEqual(loaded.appearance.hair, player.appearance.hair)
        self.assertEqual(loaded.appearance.seed, player.appearance.seed)

    def test_legacy_adult_child_gets_age_appropriate_styling(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        adult = Child("Old", "Smith", player.gender, 50, 50)
        adult.mother = mother
        adult.father = father
        adult.age = 30
        player.children.append(adult)
        player.relations.append(adult)
        SaveTests._strip_appearances(player)

        import unittest.mock as mock
        import src.appearance.integration as integration_mod
        recorded = {}
        real_inherit = integration_mod.inherit_appearance

        def spy(rng, mother, father, age=0):
            recorded["age"] = age
            return real_inherit(rng, mother, father, age=age)

        buf = io.BytesIO(pickle.dumps(player.__dict__))
        with mock.patch.object(integration_mod, "inherit_appearance", spy):
            loaded = Player.load(load_save(buf))
        self.assertEqual(recorded.get("age"), 30)

        loaded_adult = loaded.children[0]
        app = loaded_adult.appearance
        loaded_mother_app = loaded.parents["Mother"].appearance
        loaded_father_app = loaded.parents["Father"].appearance
        order = catalog.all_ids("skin_tone")
        a = order.index(loaded_mother_app.skin_tone)
        b = order.index(loaded_father_app.skin_tone)
        self.assertTrue(min(a, b) <= order.index(app.skin_tone) <= max(a, b))
        self.assertIn(app.hair, {loaded_mother_app.hair,
                                 loaded_father_app.hair,
                                 "default", "curly", "red"})

    def test_input_patch_is_restored(self):
        import builtins
        self.assertNotEqual(getattr(builtins.input, "__name__", ""),
                            "<lambda>")

    def test_two_parents_attached_sequentially_settle_with_both(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        child = Child("Two", "Smith", player.gender, 50, 50)
        child.set_parent(mother)
        child.set_parent(father)
        self.assertTrue(child._appearance_finalized)
        diffs = sum(
            getattr(child.appearance, f) not in
            (getattr(mother.appearance, f), getattr(father.appearance, f))
            for f in PHYSICAL)
        self.assertLessEqual(diffs, 1)
        self.assertIsInstance(child.appearance, Appearance)
        self.assertEqual(child.appearance.schema_version, SCHEMA_VERSION)

    def test_repeated_finalization_preserves_appearance(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        child = Child("Re", "Smith", player.gender, 50, 50, mother, father)
        before = child.appearance.to_dict()
        child.finalize_appearance()
        child.finalize_appearance()
        self.assertEqual(child.appearance.to_dict(), before)

    def test_repeated_same_parent_set_parent_preserves_appearance(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        child = Child("Same", "Smith", player.gender, 50, 50, mother, father)
        before = child.appearance.to_dict()
        child.set_parent(mother)
        child.set_parent(father)
        child.set_parent(mother)
        self.assertEqual(child.appearance.to_dict(), before)
        other = Child("New", "Smith", player.gender, 50, 50)
        other.set_parent(mother)
        first = other.appearance.to_dict()
        other.set_parent(mother)
        self.assertEqual(other.appearance.to_dict(), first)
        other.set_parent(father)
        diffs = sum(
            getattr(other.appearance, f) not in
            (getattr(mother.appearance, f), getattr(father.appearance, f))
            for f in PHYSICAL)
        self.assertLessEqual(diffs, 1)

    def test_post_load_parent_reattachment_preserves_appearance(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        kid = Child("Load", "Smith", player.gender, 50, 50, mother, father)
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        SaveTests._strip_appearances(player)
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        loaded_kid = loaded.children[0]
        established = loaded_kid.appearance.to_dict()
        loaded_kid.mother = None
        loaded_kid.set_parent(loaded.parents["Mother"])
        self.assertEqual(loaded_kid.appearance.to_dict(), established)

    def test_renamed_parent_does_not_reroll_child(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        child = Child("Ren", "Smith", player.gender, 50, 50, mother, father)
        child.age = 5
        player.children.append(child)
        player.relations.append(child)
        before = child.appearance.to_dict()
        mother.lastname = "Changed"
        mother.update_name()
        child.set_parent(mother)
        self.assertEqual(child.appearance.to_dict(), before)
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        loaded_mother = loaded.parents["Mother"]
        loaded_mother.firstname = "Renamed"
        loaded_mother.update_name()
        loaded_children = [r for r in loaded.relations
                           if isinstance(r, Child)]
        kid = loaded_children[0]
        kid.set_parent(loaded_mother)
        self.assertEqual(kid.appearance.to_dict(), before)

    def test_distinct_parents_with_same_name_and_seed_stay_distinct(self):
        from src.appearance.model import Appearance
        from src.people.classes.child import Child
        from src.people.classes.parent import Parent
        from src.people.classes.player import Player

        player = Player()
        shared = Appearance(seed=99)
        mother_a = Parent("Smith", 30, Gender.Female)
        mother_a.firstname = "Dana"
        mother_a.appearance = Appearance.from_dict(shared.to_dict())
        mother_b = Parent("Smith", 30, Gender.Female)
        mother_b.firstname = "Dana"
        mother_b.appearance = Appearance.from_dict(shared.to_dict())
        self.assertNotEqual(mother_a.person_id, mother_b.person_id)
        child = Child("Key", "Smith", player.gender, 50, 50, mother_a)
        child.set_parent(mother_b)  # genuinely new parent -> re-settles
        self.assertTrue(child._appearance_finalized)
        diffs = sum(
            getattr(child.appearance, f) not in
            (getattr(mother_a.appearance, f),
             getattr(mother_b.appearance, f)) for f in PHYSICAL)
        self.assertLessEqual(diffs, 1)

    def test_legacy_numeric_finalization_metadata_does_not_reroll(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        kid = Child("Num", "Smith", player.gender, 50, 50, mother, father)
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        established = kid.appearance.to_dict()
        kid._finalized_parent_ids = {id(mother), id(father)}
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        loaded_kid = loaded.children[0]
        self.assertEqual(loaded_kid.appearance.to_dict(), established)
        keys = loaded_kid._finalized_parent_ids
        self.assertTrue(all(isinstance(k, str) for k in keys))
        self.assertEqual(
            set(keys),
            {loaded.parents["Mother"].person_id,
             loaded.parents["Father"].person_id})
        loaded_kid.set_parent(loaded.parents["Mother"])
        self.assertEqual(loaded_kid.appearance.to_dict(), established)

        kid._finalized_parent_ids = [
            (mother.firstname, mother.lastname, mother.appearance.seed),
            (father.firstname, father.lastname, father.appearance.seed)]
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        self.assertEqual(loaded.children[0].appearance.to_dict(),
                         established)

    def test_full_save_round_trip_reattach_parents_preserves_appearance(self):
        from src.people.classes.child import Child
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        kid = Child("Full", "Smith", player.gender, 50, 50, mother, father)
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        established = kid.appearance.to_dict()
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        loaded_kid = loaded.children[0]
        self.assertEqual(loaded_kid.appearance.to_dict(), established)
        loaded_kid.set_parent(loaded.parents["Mother"])
        loaded_kid.set_parent(loaded.parents["Father"])
        self.assertEqual(loaded_kid.appearance.to_dict(), established)
        loaded_kid.mother = None
        loaded_kid.set_parent(loaded.parents["Mother"])
        self.assertEqual(loaded_kid.appearance.to_dict(), established)


if __name__ == "__main__":
    unittest.main()
