"""N1: both pregnancy locations record ordered biological parent roles."""
import copy
import random
import unittest

from tests import test_game_m25_codex_fixes as fixtures
from src.game.service import GameService
from src.lifesim_lib.lifesim_lib import Gender


class LegacyConceptionRolesTests(unittest.TestCase):
    def case(self, male=False):
        f = fixtures.CodexFixes()
        env = f.new(30, 30)
        player, partner = env['player'], env['player'].partner
        player.gender = Gender.Male if male else Gender.Female
        partner.gender = Gender.Female if male else Gender.Male
        player.fertility = partner.fertility = 100
        partner.craziness = 0
        player.is_pregnant = partner.is_pregnant = False
        env['rng_state'] = random.Random(1).getstate()
        env['cosmetic_rng_state'] = random.Random(0).getstate()
        f.svc._save(env)
        return f

    def assert_roles(self, outcome, f, male):
        self.assertEqual(outcome['bio_mother_id'], f.tid if male else f.pid)
        self.assertEqual(outcome['bio_father_id'], f.pid if male else f.tid)

    def test_real_keep_replay_reload_and_birth_preserve_ordered_roles(self):
        for male in (False, True):
            with self.subTest(male=male):
                f = self.case(male)
                try:
                    state = f.command('try_for_baby', req='try')
                    pending = state['pending_decision']
                    self.assertEqual(pending['kind'], 'pregnancy')
                    payload = {'decision_id': pending['id'], 'option_id': 'keep'}
                    response = f.command('resolve_decision', payload, req='keep')
                    env = f.svc._load(f.gid)
                    self.assertEqual(env['player'].is_pregnant, not male)
                    self.assertEqual(env['player'].partner.is_pregnant, male)
                    outcomes = env['graph']['conception_outcomes']
                    self.assertEqual(len(outcomes), 1)
                    key, outcome = next(iter(outcomes.items()))
                    self.assert_roles(outcome, f, male)
                    self.assertFalse(outcome.get('consumed'))
                    saved_outcomes = copy.deepcopy(outcomes)
                    saved_rng = env['rng_state']
                    f.svc = GameService(f.tmp.name)
                    # Exact replay must not draw/record another conception.
                    replay = f.svc.command(f.gid, state['revision'], 'keep',
                                            'resolve_decision', payload)
                    self.assertEqual(replay, response)
                    env = f.svc._load(f.gid)
                    self.assertEqual(env['rng_state'], saved_rng)
                    self.assertEqual(env['graph']['conception_outcomes'], saved_outcomes)
                    # Force successful gestation in the actual birth phase;
                    # no Child/result/applier is mocked or manually inserted.
                    env['turn'] = {'turn_id': 't_n1_birth',
                        'cursor': {'phase_id': 'family_events', 'item_cursor': 0},
                        'done': {}}
                    env['rng_state'] = random.Random(1).getstate()
                    f.svc._save(env)
                    born = f.command('age_up', req='birth')
                    self.assertEqual(born['pending_decision']['kind'], 'baby_naming')
                    env = f.svc._load(f.gid)
                    self.assertEqual(len(env['player'].children), 1)
                    child = env['player'].children[0]
                    recorded = env['graph']['conception_outcomes'][key]
                    self.assert_roles(recorded, f, male)
                    self.assertEqual(child.mother.person_id, recorded['bio_mother_id'])
                    self.assertEqual(child.father.person_id, recorded['bio_father_id'])
                    self.assertTrue(recorded['consumed'])
                    self.assertEqual(recorded['consumed_child_id'], child.person_id)
                finally:
                    f.doCleanups()

    def test_stored_overruled_abort_records_both_pregnancy_locations(self):
        for male in (False, True):
            with self.subTest(male=male):
                f = self.case(male)
                try:
                    env = f.svc._load(f.gid)
                    env['pending'] = {
                        'id': 'd_overruled', 'kind': 'pregnancy', 'title': 'Baby',
                        'text': 'Pregnancy', 'options': [{'id': 'abort', 'label': 'Abort'}],
                        'sampled': {'overrule_u': 0, 'overrule_chance': {'abort': 10}}}
                    f.svc._save(env)
                    f.svc = GameService(f.tmp.name)
                    f.command('resolve_decision', {'decision_id': 'd_overruled',
                        'option_id': 'abort'}, req='overruled')
                    env = f.svc._load(f.gid)
                    outcomes = env['graph']['conception_outcomes']
                    self.assertEqual(len(outcomes), 1)
                    self.assert_roles(next(iter(outcomes.values())), f, male)
                finally:
                    f.doCleanups()

    def test_termination_records_neither_pregnancy_nor_conception(self):
        for male in (False, True):
            with self.subTest(male=male):
                f = self.case(male)
                try:
                    env = f.svc._load(f.gid)
                    env['pending'] = {
                        'id': 'd_abort', 'kind': 'pregnancy', 'title': 'Baby',
                        'text': 'Pregnancy', 'options': [{'id': 'abort', 'label': 'Abort'}],
                        'sampled': {'overrule_u': 100, 'overrule_chance': {'abort': 0}}}
                    f.svc._save(env)
                    rng_before = f.svc._load(f.gid)['rng_state']
                    f.command('resolve_decision', {'decision_id': 'd_abort',
                        'option_id': 'abort'}, req='abort')
                    env = f.svc._load(f.gid)
                    self.assertFalse(env['player'].is_pregnant)
                    self.assertFalse(env['player'].partner.is_pregnant)
                    self.assertEqual(env['graph']['conception_outcomes'], {})
                    self.assertEqual(env['rng_state'], rng_before)
                finally:
                    f.doCleanups()
