"""Forced real-service/browser regressions for Home's command settlement."""
import random
import shutil
import tempfile
import threading
import unittest

from tools.cdp_client import launch_chrome
from web import server


class HomeLifecycleBrowserTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.tmp = tempfile.mkdtemp(prefix='h1-')
        cls.httpd = server.make_server(0, save_dir=cls.tmp)
        cls.thread = threading.Thread(target=cls.httpd.serve_forever, daemon=True)
        cls.thread.start()
        cls.svc = cls.httpd.service
        cls.base = 'http://127.0.0.1:%d' % cls.httpd.server_port

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.tmp)

    def new_game(self, **extras):
        profile = dict(first_name='Home', last_name='Lifecycle', gender='female',
                       country='Australia', seed=42, **extras)
        return self.svc.new_game(profile, self.id())

    def open_game(self, gid):
        proc, conn = launch_chrome()
        def close():
            proc.terminate()
            proc.wait(timeout=5)
            if proc.stderr:
                proc.stderr.close()
        self.addCleanup(close)
        page = conn.new_session(self.base + '/game?game=' + gid)
        page.wait_js("!!document.querySelector('.v2h-age') && "
                     "!document.querySelector('.v2h-age').disabled")
        # Observe the real I/O boundary without replacing any server logic.
        page.js("window.h1Requests=[];window.h1Statuses=[];"
                "const realFetch=window.fetch;window.fetch=async(...args)=>{"
                "const command=String(args[0]).endsWith('/actions');"
                "if(command)window.h1Requests.push(JSON.parse(args[1].body));"
                "const response=await realFetch(...args);"
                "if(command)window.h1Statuses.push(response.status);return response;};")
        return page

    @staticmethod
    def age(state):
        return next(p['age'] for p in state['people'] if p['id'] == state['player_id'])

    def settled(self, page):
        page.wait_js("document.querySelector('.v2h-age-note').textContent !== 'Ageing…'",
                     desc='Home busy state settles')

    def assert_age_enabled(self, page, gid):
        state = self.svc.get_state(gid)
        allowed = next((a for a in state['available_actions'] if a['id'] == 'age_up'), None)
        self.assertTrue(allowed and allowed['enabled'], state['pending_decision'])
        page.wait_js("!document.querySelector('.v2h-age').disabled",
                     desc='central Age reflects the current allowed action')
        self.assertEqual(page.js("document.querySelector('.v2h-age').getAttribute('aria-disabled')"), 'false')
        self.assertEqual(page.console_errors, [])

    def test_two_successive_real_age_clicks(self):
        state = self.new_game()
        gid = state['game_id']
        page = self.open_game(gid)
        for age in (1, 2):
            page.js("document.querySelector('.v2h-age').click()")
            page.wait_js('window.h1Statuses.length === %d' % age)
            self.settled(page)
            current = self.svc.get_state(gid)
            self.assertEqual(self.age(current), age)
            self.assertIsNone(current['pending_decision'])
            self.assert_age_enabled(page, gid)
        self.assertEqual(page.js('window.h1Statuses'), [200, 200])
        self.assertEqual(len({r['request_id'] for r in page.js('window.h1Requests')}), 2)

    def test_successful_age_into_pending_stays_disabled(self):
        state = self.new_game()
        gid = state['game_id']
        # Resume a legitimate yearly cursor just before education's age-17
        # offer. The real rule samples/stores the pending, not a fake response.
        env = self.svc._load(gid)
        env['player'].age = 17
        env['player'].smarts = env['player'].grades = 100
        env['rng_state'] = random.Random(1).getstate()
        env['turn'] = {'turn_id': 't_h1_education',
                      'cursor': {'phase_id': 'education', 'item_cursor': 0}, 'done': {}}
        self.svc._save(env)
        page = self.open_game(gid)
        page.js("document.querySelector('.v2h-age').click()")
        page.wait_js('window.h1Statuses.length === 1')
        self.settled(page)
        current = self.svc.get_state(gid)
        self.assertEqual(current['pending_decision']['kind'], 'university_application')
        self.assertNotIn('age_up', [a['id'] for a in current['available_actions']])
        page.wait_js("!document.getElementById('modal').hidden")
        self.assertTrue(page.js("document.querySelector('.v2h-age').disabled"))
        self.assertEqual(page.console_errors, [])

    def test_real_stale_revision_rejection_reconciles_age(self):
        state = self.new_game(scenario='enrolled_child_10')
        gid = state['game_id']
        page = self.open_game(gid)
        action = next(a for a in state['available_actions']
                      if a['id'] == 'spend_time' and a['enabled'])
        # Another real client advances revision without ageing or a pending.
        self.svc.command(gid, state['revision'], 'h1-other-client',
                         'spend_time', {'target_id': action['target_id']})
        page.js("document.querySelector('.v2h-age').click()")
        page.wait_js('window.h1Statuses.length === 1')
        self.assertEqual(page.js('window.h1Statuses'), [409])
        page.wait_js("!!document.getElementById('pendingPill') && "
                     "!document.getElementById('pendingPill').hidden")
        self.settled(page)
        self.assertEqual(self.age(self.svc.get_state(gid)), self.age(state))
        self.assert_age_enabled(page, gid)

    def test_lost_success_response_keeps_safe_retry_and_does_not_age_twice(self):
        state = self.new_game()
        gid = state['game_id']
        page = self.open_game(gid)
        # Fault injection is only at I/O: commit the real request, discard
        # its first response, then let the existing retry ledger answer.
        page.js("const observedFetch=window.fetch;let drop=true;"
                "window.fetch=async(...args)=>{const r=await observedFetch(...args);"
                "if(drop && String(args[0]).endsWith('/actions')){drop=false;"
                "await r.clone().json();throw new TypeError('H1 dropped response');}return r;};"
                "document.querySelector('.v2h-age').click()")
        page.wait_js("!!document.getElementById('pendingPill') && "
                     "!document.getElementById('pendingPill').hidden")
        self.settled(page)
        before = self.svc.get_state(gid)
        self.assertEqual(self.age(before), 1)
        op = page.js("JSON.parse(sessionStorage.getItem('pending_game_op'))")
        self.assertEqual(op['request_id'], page.js('window.h1Requests[0].request_id'))
        page.js("Array.from(document.querySelectorAll('#pendingPill button'))"
                ".find(b=>b.textContent.includes('Check result')).click()")
        page.wait_js("sessionStorage.getItem('pending_game_op') === null")
        self.settled(page)
        after = self.svc.get_state(gid)
        self.assertEqual(self.age(after), 1)
        self.assertEqual(after['revision'], before['revision'])
        requests = page.js('window.h1Requests')
        self.assertEqual(len(requests), 2)
        self.assertEqual(requests[0], requests[1])
        self.assert_age_enabled(page, gid)
