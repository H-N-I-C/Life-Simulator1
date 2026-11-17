"""Birth-applier replay cannot steal a newer, explicitly bound conception."""
import copy
import random
import shutil
import unittest

from tests import test_game_m25_conception as fixtures
from src.game.features.parenting import core as parenting
from src.game.service import GameService


class BirthApplierReplayTests(unittest.TestCase):
    def test_old_child_replay_ignores_new_pregnancy_active_and_explicit_keys(self):
        case = fixtures.ConceptionBirthTests()
        case.setUp()
        self.addCleanup(shutil.rmtree, case.tmp, True)
        svc, gid = case.svc, case.gid

        def keep(tag):
            env = svc._load(gid)
            env['player'].fertility = env['player'].partner.fertility = 100
            env['player'].partner.craziness = 0
            env['rng_state'] = random.Random(1).getstate()
            env['cosmetic_rng_state'] = random.Random(0).getstate()
            svc._save(env)
            st = svc.get_state(gid)
            st = svc.command(gid, st['revision'], tag, 'try_for_baby', {})
            pending = st['pending_decision']
            return svc.command(gid, st['revision'], tag + '-keep', 'resolve_decision',
                               {'decision_id': pending['id'], 'option_id': 'keep'})

        keep('first')
        env = svc._load(gid)
        env['turn'] = {'turn_id': 't_replay_birth',
            'cursor': {'phase_id': 'family_events', 'item_cursor': 0}, 'done': {}}
        env['rng_state'] = random.Random(1).getstate()
        svc._save(env)
        case._give_birth()  # real yearly birth and stored naming resolution
        state = svc.get_state(gid)
        for n in range(30):
            if not state['pending_decision']:
                break
            pending = state['pending_decision']
            option = next(o for o in reversed(pending['options']) if o['enabled'])
            state = svc.command(gid, state['revision'], 'drain-%s' % n,
                'resolve_decision', {'decision_id': pending['id'], 'option_id': option['id']})
        self.assertIsNone(state['pending_decision'])
        keep('second')
        svc = GameService(case.tmp)
        env = svc._load(gid)
        old_child = env['player'].children[0]
        active_key = env['player']._active_conception_key
        self.assertFalse(env['graph']['conception_outcomes'][active_key].get('consumed'))
        baseline = copy.deepcopy(env['graph']['conception_outcomes'])
        health, rng = old_child.health, env['rng_state']
        for explicit_key in (None, active_key):
            with self.subTest(explicit_key=explicit_key):
                # Replay the production domain applier against the saved
                # real child, as a recovery caller may do; not an HTTP route.
                self.assertIsNone(parenting.apply_birth_conception(env, old_child,
                                                                   key=explicit_key))
                self.assertEqual(env['graph']['conception_outcomes'], baseline)
                self.assertEqual(env['player']._active_conception_key, active_key)
                self.assertEqual(old_child.health, health)
                self.assertEqual(env['rng_state'], rng)
        svc._save(env)
        loaded = GameService(case.tmp)._load(gid)
        self.assertEqual(loaded['graph']['conception_outcomes'], baseline)
        self.assertEqual(loaded['player']._active_conception_key, active_key)
