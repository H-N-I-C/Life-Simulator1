import glob
import io
import json
import os
import pickle
import random
import unittest

from src.appearance import catalog
from src.appearance.aging import aged_view, age_stage
from src.appearance.generate import (
    age_appropriate,
    inherit_appearance,
    random_appearance,
)
from src.appearance.integration import ensure_appearance
from src.appearance.model import Appearance, SCHEMA_VERSION


FIXTURE_DIR = os.path.join(os.path.dirname(__file__), "fixtures", "appearances")


class ModelCatalogTests(unittest.TestCase):
    def test_catalog_meets_contract_minimums(self):
        minimums = {
            "skin_tone": 10, "face_shape": 5, "eye_shape": 6, "eye_color": 8,
            "brows": 5, "nose": 6, "mouth": 6, "hair_texture": 4,
            "hair_color": 12, "hair_style": 18, "facial_hair": 6,
            "marks": 5, "glasses": 6, "headwear": 7, "earrings": 5,
            "outfit_style": 8, "outfit_color": 12,
        }
        for category, minimum in minimums.items():
            self.assertGreaterEqual(
                len(catalog.CATEGORIES[category]), minimum, category)

    def test_skin_tones_form_distinct_light_to_dark_gradient(self):
        def luminance(hex_value):
            r, g, b = (int(hex_value[i:i + 2], 16)
                       for i in (1, 3, 5))
            return 0.2126 * r + 0.7152 * g + 0.0722 * b

        tones = catalog.SKIN_TONES
        lums = [luminance(t["hex"]) for t in tones]
        # strictly light -> dark
        for a, b in zip(lums, lums[1:]):
            self.assertGreater(a - b, 4.0, (a, b))
        # every neighbour pair clearly distinct in RGB distance
        for t1, t2 in zip(tones, tones[1:]):
            c1 = [int(t1["hex"][i:i + 2], 16) for i in (1, 3, 5)]
            c2 = [int(t2["hex"][i:i + 2], 16) for i in (1, 3, 5)]
            dist = sum((x - y) ** 2 for x, y in zip(c1, c2)) ** 0.5
            self.assertGreater(dist, 18.0, (t1["id"], t2["id"]))

    def test_from_dict_tolerant_unknown_ids_fall_back_to_defaults(self):
        data = Appearance(seed=7).to_dict()
        data["skin_tone"] = "skin_999"
        data["hair_style"] = 42
        data["eye_color"] = None
        data["outfit_color"] = ["not", "a", "string"]
        data["accessories"] = {"glasses": "xray_specs", "headwear": None}
        data["marks"] = ["freckles", "bogus_mark", "freckles"]
        app = Appearance.from_dict(data)
        self.assertEqual(app.skin_tone, catalog.default_id("skin_tone"))
        self.assertEqual(app.hair_style, catalog.default_id("hair_style"))
        self.assertEqual(app.eye_color, catalog.default_id("eye_color"))
        self.assertEqual(app.outfit_color, catalog.default_id("outfit_color"))
        self.assertEqual(app.accessories["glasses"],
                         catalog.default_id("glasses"))
        self.assertEqual(app.accessories["headwear"],
                         catalog.default_id("headwear"))
        self.assertEqual(app.marks, ["freckles"])  # valid, deduped

    def test_from_dict_garbage_input(self):
        for garbage in (None, "string", 42, ["list"]):
            self.assertIsInstance(Appearance.from_dict(garbage), Appearance)
        self.assertEqual(Appearance.from_dict({}).schema_version,
                         SCHEMA_VERSION)

    def test_to_from_dict_round_trip(self):
        rng = random.Random(11)
        app = random_appearance(rng, 33)
        self.assertEqual(
            Appearance.from_dict(app.to_dict()).to_dict(), app.to_dict())


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
        rng = random.Random(0)
        for age in (0, 5, 13, 30, 70):
            random_appearance(rng, age)
        m = random_appearance(random.Random(3), 30)
        f = random_appearance(random.Random(4), 30)
        inherit_appearance(rng, m, f)

        class Fake:
            age = 20
            gender = None
            adopted = False

        ensure_appearance(Fake())
        self.assertEqual(random.getstate(), before)

    def test_babies_have_no_accessories(self):
        rng = random.Random(7)
        for _ in range(50):
            app = random_appearance(rng, rng.randint(0, 2))
            self.assertEqual(app.accessories, {
                "glasses": "none", "headwear": "none", "earrings": "none"})
            self.assertEqual(app.facial_hair, "none")
            self.assertIn(app.hair_style, ("bald", "buzz"))

    def test_children_have_no_facial_hair_or_earrings(self):
        rng = random.Random(8)
        for _ in range(50):
            app = random_appearance(rng, rng.randint(3, 12))
            self.assertEqual(app.facial_hair, "none")
            self.assertEqual(app.accessories["earrings"], "none")

    def test_inheritance_biological_vs_adoption(self):
        rng = random.Random(20)
        mother = random_appearance(random.Random(21), 35)
        father = random_appearance(random.Random(22), 37)
        physical = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                    "brows", "nose", "mouth", "hair_texture")
        for _ in range(30):
            child = inherit_appearance(rng, mother, father)
            # at most one feature mutates away from both parents
            diffs = sum(
                getattr(child, f) not in
                (getattr(mother, f), getattr(father, f))
                for f in physical)
            self.assertLessEqual(diffs, 1)
            self.assertIsInstance(child.hair_color, str)
            # styling never inherited
            self.assertEqual(child.accessories, {
                "glasses": "none", "headwear": "none", "earrings": "none"})
        # unknown parents -> independent random
        self.assertIsInstance(inherit_appearance(rng, None, None), Appearance)

    def test_inheritance_bounded_variation(self):
        rng = random.Random(31)
        mother = random_appearance(random.Random(32), 35)
        father = random_appearance(random.Random(33), 37)
        features = ("face_shape", "eye_shape", "brows", "nose", "mouth",
                    "skin_tone")
        for _ in range(60):
            child = inherit_appearance(rng, mother, father)
            diffs = sum(
                getattr(child, f) not in
                (getattr(mother, f), getattr(father, f)) for f in features)
            # bounded variation: at most one feature differs from both parents
            self.assertLessEqual(diffs, 1)


class AgingTests(unittest.TestCase):
    def test_age_stages(self):
        self.assertEqual(age_stage(0), "baby")
        self.assertEqual(age_stage(2), "baby")
        self.assertEqual(age_stage(3), "child")
        self.assertEqual(age_stage(12), "child")
        self.assertEqual(age_stage(13), "teen")
        self.assertEqual(age_stage(17), "teen")
        self.assertEqual(age_stage(18), "adult")
        self.assertEqual(age_stage(49), "adult")
        self.assertEqual(age_stage(50), "elder")
        self.assertEqual(age_stage(120), "elder")

    def test_aged_view_progression_is_deterministic(self):
        app = random_appearance(random.Random(60), 20)
        young = aged_view(app, 25)
        old = aged_view(app, 80)
        self.assertEqual(young["wrinkles"], 0)
        self.assertEqual(young["grey_factor"], 0.0)
        self.assertIn(old["wrinkles"], (1, 2, 3))
        self.assertGreater(old["grey_factor"], young["grey_factor"])
        self.assertEqual(aged_view(app, 80), old)
        self.assertGreaterEqual(old["wrinkles"], 0)
        self.assertLessEqual(old["wrinkles"], 3)
        self.assertGreaterEqual(old["recession"], 0)
        self.assertLessEqual(old["recession"], 3)
        # original appearance is unchanged by ageing
        self.assertNotEqual(app.hair_color, "white")

    def test_age_appropriate_baby_rules_in_place(self):
        app = random_appearance(random.Random(61), 30)
        app.accessories = {"glasses": "round", "headwear": "cap",
                           "earrings": "hoops"}
        app.facial_hair = "beard_full"
        app.hair_style = "long_straight"
        age_appropriate(app, 1)
        self.assertEqual(app.accessories["glasses"], "none")
        self.assertEqual(app.accessories["headwear"], "none")
        self.assertEqual(app.accessories["earrings"], "none")
        self.assertEqual(app.facial_hair, "none")
        self.assertIn(app.hair_style, ("bald", "buzz"))


class FixtureTests(unittest.TestCase):
    def test_fixtures_are_valid_and_round_trip(self):
        files = sorted(glob.glob(os.path.join(FIXTURE_DIR, "*.json")))
        self.assertGreaterEqual(len(files), 12)
        fields = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                  "brows", "nose", "mouth", "hair_texture",
                  "hair_color", "hair_style", "facial_hair",
                  "outfit_style", "outfit_color")
        for path in files:
            with open(path) as f:
                data = json.load(f)
            # Kimi A fixtures are bare appearances; Kimi B may wrap the
            # appearance in a person descriptor under an "appearance" key.
            if "appearance" in data:
                data = data["appearance"]
            app = Appearance.from_dict(data)
            self.assertEqual(
                Appearance.from_dict(app.to_dict()).to_dict(), app.to_dict(),
                path)
            # every ID in the file must come from the catalog
            for field in fields:
                self.assertTrue(
                    catalog.is_valid_id(field, data[field]), (path, field))


class IntegrationTests(unittest.TestCase):
    def test_all_person_subclasses_get_appearances(self):
        from src.people.classes.player import Player
        from src.people.classes.sibling import Sibling
        from src.people.classes.child import Child
        from src.lifesim_lib.lifesim_lib import Gender

        player = Player()
        self.assertIsInstance(player.appearance, Appearance)
        for parent in player.parents.values():
            self.assertIsInstance(parent.appearance, Appearance)
        player.age = 30
        partner = player.generate_partner()
        self.assertIsInstance(partner.appearance, Appearance)
        sibling = Sibling("Smith", 7, Gender.Male, 50, 50)
        self.assertIsInstance(sibling.appearance, Appearance)

        # biological child inherits from parents known at construction
        # (at most one feature may mutate away from both parents)
        bio = Child("Bio", "Smith", Gender.Female, 50, 50,
                    player.parents["Mother"], player.parents["Father"])
        self.assertIsInstance(bio.appearance, Appearance)
        physical = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                    "brows", "nose", "mouth", "hair_texture")
        bio_diffs = sum(
            getattr(bio.appearance, f) not in (
                getattr(player.parents["Mother"].appearance, f),
                getattr(player.parents["Father"].appearance, f))
            for f in physical)
        self.assertLessEqual(bio_diffs, 1)

        # adopted child: independent
        adopted = Child("Ado", "Lee", Gender.Male, 50, 50, adopted=True)
        self.assertIsInstance(adopted.appearance, Appearance)

        # parents set after construction (set_parent) finalize inheritance
        late = Child("Late", "Smith", Gender.Male, 50, 50)
        self.assertFalse(late._appearance_finalized)
        late.set_parent(player.parents["Mother"])
        self.assertTrue(late._appearance_finalized)
        late_diffs = sum(
            getattr(late.appearance, f) not in (
                getattr(player.parents["Mother"].appearance, f),
                getattr(player.parents["Father"].appearance, f))
            for f in physical)
        self.assertLessEqual(late_diffs, 1)

    def test_ensure_appearance_idempotent(self):
        from src.people.classes.sibling import Sibling
        from src.lifesim_lib.lifesim_lib import Gender
        s = Sibling("Smith", 9, Gender.Female, 50, 50)
        first = s.appearance.to_dict()
        ensure_appearance(s)
        self.assertEqual(s.appearance.to_dict(), first)  # kept, not replaced


class Evil:
    """Module-level class not in the save allowlist."""


class SaveTests(unittest.TestCase):
    @staticmethod
    def _strip_appearances(obj, seen=None):
        """Simulate an old save: remove every appearance attribute."""
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
        kid = player.generate_child(player.parents["Mother"])
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        self._strip_appearances(player)

        buf = io.BytesIO(pickle.dumps(player.__dict__))
        data = load_save(buf)
        loaded = Player.load(data)

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
        # successor keeps their own appearance AND canonical person id
        self.assertEqual(player.appearance.to_dict(),
                         successor.appearance.to_dict())
        self.assertEqual(player.person_id, successor.person_id)
        # reconstructed parents keep real names and appearances
        new_mother = player.parents["Mother"]
        self.assertEqual(new_mother.firstname, mother.firstname)
        self.assertEqual(new_mother.name, mother.firstname + " " + mother.lastname)
        self.assertEqual(new_mother.appearance.to_dict(),
                         mother.appearance.to_dict())
        new_father = player.parents["Father"]
        self.assertEqual(new_father.firstname, father.firstname)
        self.assertEqual(new_father.name, father.firstname + " " + father.lastname)
        self.assertEqual(new_father.appearance.to_dict(),
                         father.appearance.to_dict())
        # canonical identity survives role conversion
        self.assertEqual(new_mother.person_id, mother.person_id)
        self.assertEqual(new_father.person_id, father.person_id)
        self.assertEqual(sibling_objs[0].person_id, other.person_id)
        self.assertEqual(sibling_objs[0].name,
                         other.firstname + " " + other.lastname)


class FixRound1Tests(unittest.TestCase):
    """Regressions for docs/collab/10-codex-review-a.md findings 1-7."""

    def _player_with_bio_child(self):
        from src.people.classes.player import Player
        player = Player()
        kid = player.generate_child(player.parents["Mother"])
        kid.mother = player.parents["Mother"]
        kid.father = player.parents["Father"]
        kid.age = 5
        player.children.append(kid)
        player.relations.append(kid)
        return player, kid

    def test_migration_preserves_saved_child_and_is_idempotent(self):
        # Finding 1: existing appearances must survive load and re-migration
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save
        from src.appearance.integration import migrate_appearances

        player, kid = self._player_with_bio_child()
        kid_app = kid.appearance.to_dict()
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        loaded_kid = loaded.children[0]
        self.assertEqual(loaded_kid.appearance.to_dict(), kid_app)
        # second migration pass changes nothing
        migrate_appearances(loaded)
        self.assertEqual(loaded_kid.appearance.to_dict(), kid_app)
        self.assertEqual(loaded.appearance.to_dict(), player.appearance.to_dict())

    def test_rebuilt_relative_names_are_refreshed(self):
        # Finding 2 covered via full .name in SuccessorTests; check the
        # standalone name-refresh invariant directly
        from src.people.classes.parent import Parent
        from src.lifesim_lib.lifesim_lib import Gender
        p = Parent("Bullard", 55, Gender.Female)
        p.firstname = "Alexina"
        p.update_name()
        self.assertEqual(p.name, "Alexina Bullard")

    def test_inheritance_stable_across_pythonhashseeds(self):
        # Finding 3: seeded mark inheritance must not depend on hash seed
        import subprocess
        import sys
        code = (
            "import builtins; builtins.input = lambda *a: '1'\n"
            "import sys; sys.path.insert(0, '.')\n"
            "import random\n"
            "import src.people.classes.partner\n"
            "from src.appearance.model import Appearance\n"
            "from src.appearance.generate import inherit_appearance\n"
            "m = Appearance(marks=['freckles','mole','beauty_mark','scar_brow','birthmark'])\n"
            "f = Appearance(marks=['freckles','mole'])\n"
            "print(','.join(inherit_appearance(random.Random(7), m, f).marks))\n"
        )
        outputs = set()
        for seed in ("0", "1", "2", "3"):
            env = dict(os.environ, PYTHONHASHSEED=seed)
            out = subprocess.run(
                [sys.executable, "-c", code], capture_output=True, text=True,
                env=env, timeout=60)
            self.assertEqual(out.returncode, 0, out.stderr)
            outputs.add(out.stdout.strip())
        self.assertEqual(len(outputs), 1, outputs)

    def test_schema_version_policy(self):
        # Finding 4: unsupported versions rejected; same version normal
        good = Appearance(seed=3).to_dict()
        self.assertEqual(Appearance.from_dict(good).schema_version,
                         SCHEMA_VERSION)
        for bad_version in (999, 0, -1, "1", 1.5, True, None):
            data = dict(good, schema_version=bad_version)
            if bad_version is None:
                data.pop("schema_version")  # missing = current, must load
                self.assertIsInstance(Appearance.from_dict(data), Appearance)
                continue
            with self.assertRaises(ValueError, msg=repr(bad_version)):
                Appearance.from_dict(data)

    def test_loader_replaces_unsupported_version_appearance(self):
        # Finding 4: loader falls back to a fresh appearance, keeps person valid
        from src.people.classes.sibling import Sibling
        from src.lifesim_lib.lifesim_lib import Gender
        from src.appearance.integration import ensure_appearance
        s = Sibling("Smith", 20, Gender.Male, 50, 50)
        s.appearance.schema_version = 999
        ensure_appearance(s)
        self.assertIsInstance(s.appearance, Appearance)
        self.assertEqual(s.appearance.schema_version, SCHEMA_VERSION)

    def test_pickled_instance_with_unknown_id_is_normalized(self):
        # Finding 5: saved instances pass through validation on load
        from src.people.classes.player import Player
        from src.lifesim_lib.safe_save import load_save

        player, _kid = self._player_with_bio_child()
        player.appearance.skin_tone = "skin_999"  # stale catalog ID
        player.appearance.accessories["glasses"] = "xray_specs"
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        self.assertEqual(loaded.appearance.skin_tone,
                         catalog.default_id("skin_tone"))
        self.assertEqual(loaded.appearance.accessories["glasses"], "none")
        # valid fields were not rerolled
        self.assertEqual(loaded.appearance.face_shape,
                         player.appearance.face_shape)
        self.assertEqual(loaded.appearance.seed, player.appearance.seed)

    def test_legacy_adult_child_gets_age_appropriate_styling(self):
        # Finding 6: inherit physical features, style for the real age
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

        # spy on the inheritance age argument: migration must pass the
        # child's REAL age (bald is a valid adult style, so we assert the
        # invariant directly instead of rejecting valid random output)
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
        mother_app = loaded.parents["Mother"].appearance
        father_app = loaded.parents["Father"].appearance
        physical = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                    "brows", "nose", "mouth", "hair_texture")
        diffs = sum(
            getattr(app, f) not in (
                getattr(mother_app, f), getattr(father_app, f))
            for f in physical)
        self.assertLessEqual(diffs, 1)
        # styling is valid for the catalog at any age (bald included);
        # facial hair rules come from the catalog, not baby forcing
        self.assertIn(app.hair_style, catalog.all_ids("hair_style"))
        self.assertIn(app.facial_hair, catalog.all_ids("facial_hair"))

    def test_input_patch_is_restored(self):
        # Finding 7: tests/__init__ must not leave a stubbed builtins.input
        import builtins
        self.assertNotEqual(getattr(builtins.input, "__name__", ""), "<lambda>")

    def test_two_parents_attached_sequentially_settle_with_both(self):
        # A child whose second parent is attached after the first must
        # finalize with both parents
        from src.people.classes.child import Child
        from src.people.classes.player import Player

        player = Player()
        mother = player.parents["Mother"]
        father = player.parents["Father"]
        child = Child("Two", "Smith", player.gender, 50, 50)
        child.set_parent(mother)
        first_app = child.appearance.to_dict()
        child.set_parent(father)
        self.assertTrue(child._appearance_finalized)
        physical = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                    "brows", "nose", "mouth", "hair_texture")
        diffs = sum(
            getattr(child.appearance, f) not in (
                getattr(mother.appearance, f), getattr(father.appearance, f))
            for f in physical)
        self.assertLessEqual(diffs, 1)
        # second parent contributed: appearance re-settled (not necessarily
        # different, but must be a valid inherit result from both parents)
        self.assertIsInstance(child.appearance, Appearance)
        self.assertEqual(child.appearance.schema_version, SCHEMA_VERSION)

    def test_repeated_finalization_preserves_appearance(self):
        # Re-review 1: finalize_appearance() with the same known parents
        # must never reroll an established child
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
        # set_parent() with the exact same parent must be idempotent
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
        # while a genuinely new parent still re-settles within creation
        other = Child("New", "Smith", player.gender, 50, 50)
        other.set_parent(mother)
        first = other.appearance.to_dict()
        other.set_parent(mother)
        self.assertEqual(other.appearance.to_dict(), first)
        other.set_parent(father)
        physical = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                    "brows", "nose", "mouth", "hair_texture")
        diffs = sum(
            getattr(other.appearance, f) not in (
                getattr(mother.appearance, f),
                getattr(father.appearance, f))
            for f in physical)
        self.assertLessEqual(diffs, 1)

    def test_post_load_parent_reattachment_preserves_appearance(self):
        # A loaded child whose appearance was established by migration must
        # never reroll when a parent reference is (re)attached
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
        # Person.ID is immutable identity: renaming a parent (before or
        # after saving) must not change the finalization keys
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
        # rename before saving
        mother.lastname = "Changed"
        mother.update_name()
        child.set_parent(mother)
        self.assertEqual(child.appearance.to_dict(), before)
        # rename after save/load
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
        # two DIFFERENT people sharing name and cosmetic seed must not
        # collapse into one parent identity
        from src.appearance.model import Appearance
        from src.people.classes.child import Child
        from src.people.classes.parent import Parent
        from src.people.classes.player import Player
        from src.lifesim_lib.lifesim_lib import Gender

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
        first = child.appearance.to_dict()
        child.set_parent(mother_b)  # genuinely new parent -> re-settles
        self.assertTrue(child._appearance_finalized)
        physical = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                    "brows", "nose", "mouth", "hair_texture")
        diffs = sum(
            getattr(child.appearance, f) not in (
                getattr(mother_a.appearance, f),
                getattr(mother_b.appearance, f))
            for f in physical)
        self.assertLessEqual(diffs, 1)

    def test_legacy_numeric_finalization_metadata_does_not_reroll(self):
        # Re-review 2: old saves stored process-local id() integers (or
        # name/seed tuples) in _finalized_parent_ids; loaded children must
        # not reroll when parent links are re-attached
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
        # simulate the two older formats
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

        # tuple (name, lastname, seed) format
        kid._finalized_parent_ids = [
            (mother.firstname, mother.lastname, mother.appearance.seed),
            (father.firstname, father.lastname, father.appearance.seed)]
        buf = io.BytesIO(pickle.dumps(player.__dict__))
        loaded = Player.load(load_save(buf))
        self.assertEqual(loaded.children[0].appearance.to_dict(),
                         established)

    def test_full_save_round_trip_reattach_parents_preserves_appearance(self):        # Re-review 2: an ordinary current save (no stripping/migration)
        # must not reroll when parents are re-attached after reload; the
        # identity keys must survive pickling (no process-local id())
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
        # re-attach the identical loaded parents: no reroll
        loaded_kid.set_parent(loaded.parents["Mother"])
        loaded_kid.set_parent(loaded.parents["Father"])
        self.assertEqual(loaded_kid.appearance.to_dict(), established)
        loaded_kid.mother = None
        loaded_kid.set_parent(loaded.parents["Mother"])
        self.assertEqual(loaded_kid.appearance.to_dict(), established)

    def test_inherited_marks_have_no_duplicates(self):
        # deterministic repro from review: parents with all five marks can
        # produce duplicate inherited marks, breaking round-trip invariants
        from src.appearance.model import Appearance
        from src.appearance.generate import inherit_appearance
        all_marks = ["freckles", "mole", "beauty_mark", "scar_brow",
                     "birthmark"]
        mother = Appearance(marks=list(all_marks))
        father = Appearance(marks=["freckles", "mole"])
        for seed in range(200):
            child = inherit_appearance(random.Random(seed), mother, father)
            self.assertEqual(len(child.marks), len(set(child.marks)), seed)

    def test_recession_only_for_some_people(self):
        # ~35% of people (by seed) recede from their 40s; the rest keep hair
        app = Appearance(seed=10)  # 10 % 100 < 35 -> recedes
        self.assertGreater(aged_view(app, 80)["recession"], 0)
        app_keep = Appearance(seed=40)  # 40 % 100 >= 35 -> keeps hair
        self.assertEqual(aged_view(app_keep, 100)["recession"], 0)
        self.assertEqual(aged_view(app, 20)["recession"], 0)


if __name__ == "__main__":
    unittest.main()
