"""E1: legacy stored pregnancy/birth resolves recheck protected adulthood."""
from pathlib import Path
import random
import unittest

from tests import test_game_m25_codex_fixes as fixtures
from src.game.service import GameError, GameService
from src.people.classes.child import Child
from src.lifesim_lib.lifesim_lib import Gender


class LegacyPregnancyAdultTests(unittest.TestCase):
    def case(self, actor_age, target_age, male=False):
        f = fixtures.CodexFixes()
        env = f.new(actor_age, target_age)
        if male:
            env['player'].gender = Gender.Male
            env['player'].partner.gender = Gender.Female
        env['player'].fertility = 100
        env['player'].partner.fertility = 100
        env['player'].is_pregnant = False
        env['player'].partner.is_pregnant = False
        env['rng_state'] = random.Random(1).getstate()
        env['cosmetic_rng_state'] = random.Random(0).getstate()
        return f, env

    def save_and_reopen(self, f, env):
        f.svc._save(env)
        f.svc = GameService(f.tmp.name)
        f.svc.get_state(f.gid)  # complete any legitimate old-save normalization
        return Path(f.svc._path(f.gid)).read_bytes()

    def naming(self, f, env):
        child = Child('Original', 'Lee', Gender.Male, 50, 50, adopted=True)
        env['player'].children.append(child)
        env['player'].relations.append(child)
        f.svc._ensure_ids(env)
        env['pending'] = {
            'id': 'd_stored_name', 'kind': 'baby_naming',
            'title': 'Name', 'text': 'Name the child',
            'person_ids': [child.person_id],
            'options': [{'id': 'name', 'label': 'Choose a name',
                         'inputs': [{'id': 'name', 'type': 'text', 'required': True,
                                     'min': 1, 'max': 40}]}],
            'sampled': {}, 'sampled_child_id': child.person_id,
            'sampled_child': child}
        return child.person_id

    def resolve(self, f, decision, option, inputs=None):
        return f.command('resolve_decision', {
            'decision_id': decision, 'option_id': option, 'inputs': inputs or {}},
            req='resolve-stored')

    def test_stored_pregnancy_keep_and_overruled_abort_check_both_ages(self):
        for male in (False, True):
            for ages in ((17, 18), (18, 17), (18, 18)):
                for option, overrule_u in (('keep', 100), ('abort', 0)):
                    with self.subTest(male=male, ages=ages, option=option):
                        f, env = self.case(*ages, male)
                        try:
                            env['pending'] = {
                                'id': 'd_stored_pregnancy', 'kind': 'pregnancy',
                                'title': 'Baby', 'text': 'Pregnancy',
                                'options': [{'id': 'keep', 'label': 'Keep'},
                                            {'id': 'abort', 'label': 'Abort'}],
                                'sampled': {'overrule_u': overrule_u,
                                    'overrule_chance': {'keep': 0, 'abort': 10}}}
                            before = self.save_and_reopen(f, env)
                            if min(ages) < 18:
                                with self.assertRaises(GameError) as error:
                                    self.resolve(f, 'd_stored_pregnancy', option)
                                self.assertEqual(error.exception.code, 'not_adult')
                                self.assertEqual(error.exception.status, 409)
                                self.assertEqual(Path(f.svc._path(f.gid)).read_bytes(), before)
                            else:
                                self.resolve(f, 'd_stored_pregnancy', option)
                                after = f.svc._load(f.gid)
                                pregnant = after['player'].partner if male else after['player']
                                self.assertTrue(pregnant.is_pregnant)
                                self.assertIsNone(after['pending'])
                        finally:
                            f.doCleanups()

    def test_birth_continuation_after_stored_choice_has_typed_adult_guard(self):
        for male in (False, True):
            for ages in ((17, 18), (18, 17), (18, 18)):
                with self.subTest(male=male, ages=ages):
                    f, env = self.case(*ages, male)
                    try:
                        old_child_id = self.naming(f, env)
                        pregnant = env['player'].partner if male else env['player']
                        pregnant.is_pregnant = True
                        if not male:
                            env['player']._recent_child_father = env['player'].partner
                        # Resume the REAL birth phase after the stored naming
                        # decision; seed1 forces successful gestation, no roll retry.
                        env['turn'] = {'turn_id': 't_stored_birth',
                            'cursor': {'phase_id': 'family_events', 'item_cursor': 0},
                            'interrupted': True, 'done': {}}
                        before = self.save_and_reopen(f, env)
                        if min(ages) < 18:
                            with self.assertRaises(GameError) as error:
                                self.resolve(f, 'd_stored_name', 'name', {'name': 'Renamed'})
                            self.assertEqual(error.exception.code, 'not_adult')
                            self.assertEqual(error.exception.status, 409)
                            # Naming, pregnancy flags, RNG and all other state roll back.
                            self.assertEqual(Path(f.svc._path(f.gid)).read_bytes(), before)
                        else:
                            state = self.resolve(f, 'd_stored_name', 'name', {'name': 'Renamed'})
                            children = f.svc._load(f.gid)['player'].children
                            self.assertEqual(len(children), 2)
                            self.assertEqual(children[0].person_id, old_child_id)
                            self.assertEqual(children[0].firstname, 'Renamed')
                            self.assertEqual(state['pending_decision']['kind'], 'baby_naming')
                    finally:
                        f.doCleanups()

    def test_existing_child_naming_remains_nonromantic_for_minor_parent(self):
        for ages in ((17, 18), (18, 17)):
            with self.subTest(ages=ages):
                f, env = self.case(*ages)
                try:
                    child_id = self.naming(f, env)
                    self.save_and_reopen(f, env)
                    self.resolve(f, 'd_stored_name', 'name', {'name': 'Renamed'})
                    after = f.svc._load(f.gid)
                    self.assertEqual(len(after['player'].children), 1)
                    self.assertEqual(after['player'].children[0].person_id, child_id)
                    self.assertEqual(after['player'].children[0].firstname, 'Renamed')
                    self.assertFalse(after['player'].is_pregnant)
                    self.assertFalse(after['player'].partner.is_pregnant)
                finally:
                    f.doCleanups()

    def test_adult_abort_without_overrule_remains_available(self):
        for male in (False, True):
            with self.subTest(male=male):
                f, env = self.case(18, 18, male)
                try:
                    env['pending'] = {
                        'id': 'd_stored_pregnancy', 'kind': 'pregnancy', 'title': 'Baby',
                        'text': 'Pregnancy', 'options': [{'id': 'abort', 'label': 'Abort'}],
                        'sampled': {'overrule_u': 100, 'overrule_chance': {'abort': 0}}}
                    self.save_and_reopen(f, env)
                    self.resolve(f, 'd_stored_pregnancy', 'abort')
                    after = f.svc._load(f.gid)
                    self.assertFalse(after['player'].is_pregnant)
                    self.assertFalse(after['player'].partner.is_pregnant)
                finally:
                    f.doCleanups()
