"""Scenario precondition retries through the real service and HTTP."""
import hashlib
import json
from pathlib import Path
import tempfile
import threading
import unittest
from unittest.mock import patch
import urllib.error
import urllib.request

from src.game import scenarios
from src.game.service import GameError, GameService
from web import server

PROFILE = {'first_name': 'Dev', 'last_name': 'Tester', 'gender': 'female',
           'country': 'Australia', 'scenario': 'university_pending_17', 'appearance': None}


class RecordingService(GameService):
    """Observe creation attempts without replacing any game behavior/I/O."""
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.attempts = set()

    def _save(self, env):
        self.attempts.add(env['game_id'])
        return super()._save(env)


class ScenarioRetryTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(prefix='scenario-retry-')
        self.addCleanup(self.temp.cleanup)
        self.httpd = server.make_server(0, save_dir=self.temp.name)
        self.httpd.dev_mode = True
        self.service = RecordingService(self.temp.name)
        self.httpd.service = self.service
        threading.Thread(target=self.httpd.serve_forever, daemon=True).start()
        self.addCleanup(self.httpd.server_close)
        self.addCleanup(self.httpd.shutdown)

    def create(self, profile, request_id):
        request = urllib.request.Request(
            'http://127.0.0.1:%s/api/games' % self.httpd.server_port,
            data=json.dumps(dict(profile, request_id=request_id)).encode(),
            headers={'Content-Type': 'application/json'})
        try:
            with urllib.request.urlopen(request) as res:
                return res.status, json.load(res)
        except urllib.error.HTTPError as exc:
            with exc:
                return exc.code, json.load(exc)

    def test_reported_unlucky_seeds_retry_and_creation_replays(self):
        for seed in (2021261880, 254938035, 1852648667):
            with self.subTest(seed=seed):
                profile = dict(PROFILE, seed=seed)
                request = 'university-%s' % seed
                status, state = self.create(profile, request)
                self.assertEqual(status, 201, state)
                player = next(p for p in state['people'] if p['id'] == state['player_id'])
                self.assertEqual(player['age'], 17)
                self.assertIn(state['pending_decision']['kind'],
                              ('university_application', 'university_funding'))
                replay_status, replay = self.create(profile, request)
                self.assertEqual(replay_status, 201, replay)
                self.assertEqual(replay, state)
                fresh = GameService(self.temp.name)
                self.assertEqual(fresh.new_game(profile, request), state)
                env = fresh._load(state['game_id'])
                fingerprint = hashlib.sha256(json.dumps(
                    profile, sort_keys=True,
                    default=str).encode()).hexdigest()
                self.assertEqual(env['creation']['fingerprint'], fingerprint)
                with self.assertRaises(GameError) as error:
                    fresh.new_game(dict(profile, seed=seed + 1), request)
                self.assertEqual(error.exception.code, 'conflict')
        self.assertGreater(len(self.service.attempts), 3)
        self.assertEqual(len(list(Path(self.temp.name).glob('*.pickle'))), 3)

    def test_exhaustion_is_bounded_typed_and_does_not_publish_partial_game(self):
        # Force a real impossible precondition: no university decision at
        # birth. Only the scenario data is changed; all engine/save paths run.
        impossible = dict(scenarios.SCENARIOS['university_pending_17'], target_age=0)
        profile = dict(PROFILE, seed=2021261880)
        with patch.dict(scenarios.SCENARIOS, university_pending_17=impossible):
            status, body = self.create(profile, 'exhausted')
        self.assertEqual(status, 409, body)
        self.assertEqual(body['error']['code'], 'scenario_precondition')
        self.assertEqual(len(self.service.attempts), scenarios.SCENARIO_MAX_ATTEMPTS)
        self.assertEqual(list(Path(self.temp.name).glob('*.pickle')), [])
        self.assertEqual(self.service._load_creations(), {})
        # A failed setup does not poison request ownership or a later retry.
        status, state = self.create(profile, 'exhausted')
        self.assertEqual(status, 201, state)
        self.assertEqual(GameService(self.temp.name).new_game(profile, 'exhausted'), state)
