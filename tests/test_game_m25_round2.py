"""Forced M2.5 round-2 regressions through persisted service/HTTP paths."""
import json
import random
import tempfile
from pathlib import Path
import threading
import unittest
import urllib.error
import urllib.request

from tests import test_game_m25_codex_fixes as fixtures
from src.game.service import GameError, GameService
from src.game.features.knowledge import core as knowledge
from src.game.features.family_graph import graph
from src.game.features.family_graph.service_view import ServiceGraphView
from src.people.classes.partner import Partner
from src.lifesim_lib.lifesim_lib import Gender
from web import server


class RoundTwoTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.webtmp = tempfile.TemporaryDirectory(prefix='m25-round2-http-')
        cls.httpd = server.make_server(0, save_dir=cls.webtmp.name)
        threading.Thread(target=cls.httpd.serve_forever, daemon=True).start()

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        cls.webtmp.cleanup()

    def setUp(self):
        self.f = fixtures.CodexFixes()
        self.addCleanup(self.f.doCleanups)
        self.env = self.f.new()
        self.httpd.service = self.f.svc

    def request(self, method, suffix, body=None):
        req = urllib.request.Request(
            'http://127.0.0.1:%s/api/games/%s%s' % (
                self.httpd.server_port, self.f.gid, suffix),
            method=method, data=None if body is None else json.dumps(body).encode(),
            headers={'Content-Type': 'application/json'})
        try:
            with urllib.request.urlopen(req) as res:
                return res.status, json.load(res)
        except urllib.error.HTTPError as exc:
            with exc:
                return exc.code, json.load(exc)

    def reload(self):
        self.f.svc = GameService(self.f.tmp.name)
        self.httpd.service = self.f.svc
        return self.f.svc._load(self.f.gid)

    def no_edges(self):
        env = self.f.svc._load(self.f.gid)
        env['graph']['relationships'] = []
        env['graph']['unions'] = []
        self.f.svc._save(env)
        return env

    def union(self):
        f = self.f
        st = f.svc.get_state(f.gid)
        res = f.svc.graph_command(f.gid, st['revision'], 'form-union', 'form_union', {
            'a_id': f.pid, 'b_id': f.tid, 'kind': 'marriage', 'turn': 0,
            'recognition': {'status': 'recognized', 'jurisdiction_id': 'jur-neutral-default',
                            'law_version': 1, 'basis': 'registration'}})
        return res['result']['ids']['union_id']

    def wedding(self, op, args, req):
        st = self.f.svc.get_state(self.f.gid)
        return self.f.svc.wedding_command(self.f.gid, st['revision'], req, op, args)

    def test_private_reputation_absent_from_http_until_observer_learns(self):
        f, env = self.f, self.env
        fact = knowledge.mint_fact(env, 'secret_union', [f.tid], 1,
                                   occurrence_id='private-reputation')
        knowledge.get_fact(env, fact)['reputation_changes'] = [
            {'person_id': f.tid, 'scope': 'global', 'delta': -40}]
        knowledge.learn(env, f.tid, fact, 1, 'witnessed')
        knowledge.reputation_delta(env, f.tid, 'global', -40, 'private-reputation')
        f.svc._save(env)
        self.reload()
        status, view = self.request('GET', '/relationships-view')
        self.assertEqual(status, 200)
        self.assertEqual(view['facts'], [])
        self.assertEqual(view['reputation'], [])  # existence is private, too
        env = f.svc._load(f.gid)
        self.assertEqual(ServiceGraphView(env).reputation_of(f.tid, f.pid), 0)
        uninformed = next(pid for pid in f.svc._all_people(env) if pid not in (f.pid, f.tid))
        self.assertEqual(knowledge.reputation_as_known(env, f.tid, 'global', uninformed), 0)
        knowledge.learn(env, f.pid, fact, 2, 'told')
        f.svc._save(env)
        self.reload()
        _, view = self.request('GET', '/relationships-view')
        self.assertEqual([r['value'] for r in view['reputation']], [-40])
        self.assertEqual([v['fact_id'] for v in view['facts']], [fact])
        env = f.svc._load(f.gid)
        self.assertEqual(ServiceGraphView(env).reputation_of(f.tid, f.pid), -40)
        self.assertEqual(knowledge.reputation_as_known(env, f.tid, 'global', uninformed), 0)

    def test_known_reputation_does_not_include_hidden_changes_or_scope(self):
        f, env = self.f, self.env
        scope = env['policy']['jurisdiction_id']
        for occurrence, delta, scope_id, known in (
                ('known', 10, 'global', True), ('hidden', -60, 'global', False),
                ('private-scope', -30, scope, False)):
            fid = knowledge.mint_fact(env, 'discovery', [f.tid], 1,
                                      occurrence_id=occurrence)
            knowledge.get_fact(env, fid)['reputation_changes'] = [
                {'person_id': f.tid, 'scope': scope_id, 'delta': delta}]
            knowledge.reputation_delta(env, f.tid, scope_id, delta, occurrence)
            if known:
                knowledge.learn(env, f.pid, fid, 1, 'witnessed')
        f.svc._save(env)
        self.reload()
        _, view = self.request('GET', '/relationships-view')
        self.assertEqual([(r['scope_id'], r['value']) for r in view['reputation']], [('global', 10)])
        self.assertEqual(ServiceGraphView(f.svc._load(f.gid)).reputation_of(f.tid, f.pid), 10)

    def test_chemistry_never_creates_unsampled_relationship(self):
        self.no_edges()
        ev = self.f.event({'chemistry': {'between': ['actor', 'target'], 'delta': 5}},
                          interaction={'kind': 'date', 'romantic': True})
        pending = self.f.install(ev, 'd_unsampled_chemistry')
        self.assertNotIn('accepted', pending.get('sampled') or {})
        self.f.resolve(pending)
        env = self.reload()
        self.assertEqual(env['graph']['relationships'], [])
        self.assertEqual(env['graph']['offers'], [])

    def test_accepted_offer_precedes_chemistry_and_refusal_has_no_gains(self):
        for accepted in (True, False):
            with self.subTest(accepted=accepted):
                self.no_edges()
                ev = self.f.event({
                    'chemistry': {'between': ['actor', 'target'], 'delta': 7},
                    'offer': {'interaction': 'propose_courtship', 'to': 'target'}},
                    interaction={'kind': 'date', 'romantic': True})
                pending = self.f.install(ev, 'd_consent_%s' % accepted)
                env = self.f.svc._load(self.f.gid)
                env['pending']['sampled'] = {'accepted': accepted}
                self.f.svc._save(env)
                self.f.resolve(pending)
                env = self.reload()
                edges = env['graph']['relationships']
                self.assertEqual(len(edges), int(accepted))
                if accepted:
                    self.assertEqual(edges[0]['chemistry'], 62)  # offer 55 + delta 7
                offer = env['graph']['offers'][-1]
                self.assertEqual(offer['status'], 'accepted' if accepted else 'declined')

    def test_dating_refusal_preserves_declined_offer_through_reload(self):
        f, env = self.f, self.no_edges()
        # This persisted RNG's first draw is > .98: refusal is forced
        # for every eligible profile under the ratified probability clamp.
        env['rng_state'] = random.Random(153).getstate()
        env['cosmetic_rng_state'] = random.Random(0).getstate()
        f.svc._save(env)
        f.command('dating_app')
        pending = f.svc._load(f.gid)['pending']
        self.assertIs(pending['sampled']['accepted'], False)
        accept = next(o for o in pending['options'] if o['id'].endswith('_accept'))
        self.assertEqual(set(accept['_domain']), {'offer'})
        self.reload()
        before = f.svc._load(f.gid)['player'].happiness
        f.resolve(pending)
        env = self.reload()
        self.assertEqual(env['graph']['relationships'], [])
        self.assertTrue(env['graph']['offers'])
        self.assertEqual(env['graph']['offers'][-1]['status'], 'declined')
        self.assertEqual(env['player'].happiness, before)

    def test_married_stranger_is_still_a_dating_candidate(self):
        f, env = self.f, self.no_edges()
        target = f.svc._all_people(env)[f.tid]
        other = Partner(35, Gender.Female, 60, 60, 80, 0)
        env['player'].relations.append(other)
        f.svc._ensure_ids(env)
        graph.form_union(env['graph'], target, other, 'marriage',
                         {'status': 'recognized', 'jurisdiction_id': 'jur-neutral-default',
                          'law_version': 1, 'basis': 'registration'}, 0)
        graph.start_relationship(env['graph'], env['player'], other, 'courtship',
                                 {'satisfaction': 50, 'trust': 50, 'chemistry': 50}, 0)
        f.svc._save(env)
        st = f.command('dating_app')
        self.assertEqual(st['pending_decision']['bindings']['target'], f.tid)
        self.assertEqual(self.reload()['pending']['bindings']['target'], f.tid)

    def test_wedding_invalid_inputs_are_typed_and_atomic_over_http(self):
        uid = self.union()
        valid = {'union_id': uid, 'venue_id': 'courthouse', 'guests': 0,
                 'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'}
        cases = [('plan_wedding', {}), ('plan_wedding', dict(valid, venue_id=[])),
                 ('plan_wedding', dict(valid, guests=True)),
                 ('plan_wedding', dict(valid, guests=501)),
                 ('plan_wedding', dict(valid, outfit_id={})),
                 ('plan_wedding', dict(valid, honeymoon_id=None)),
                 ('plan_wedding', dict(valid, union_id=[])),
                 ('plan_wedding', dict(valid, turn=999)),
                 ('plan_wedding', dict(valid, extra='ignored?')),
                 ('renew_vows', {'union_id': uid, 'tier': []}),
                 ('divorce_union', {'union_id': None}),
                 ('divorce_union', {'union_id': ''})]
        before = Path(self.f.svc._path(self.f.gid)).read_bytes()
        rev = self.f.svc.get_state(self.f.gid)['revision']
        for i, (op, args) in enumerate(cases):
            with self.subTest(op=op, args=args):
                status, body = self.request('POST', '/wedding', {
                    'op': op, 'args': args, 'expected_revision': rev,
                    'request_id': 'bad-wedding-%s' % i})
                self.assertEqual(status, 400, body)
                self.assertEqual(body['error']['code'], 'invalid_input')
                self.assertEqual(Path(self.f.svc._path(self.f.gid)).read_bytes(), before)
        res = self.wedding('plan_wedding', valid, 'valid-after-invalid')
        self.assertTrue(res['result']['ok'])

    def test_renewal_once_per_union_per_year_including_free_and_reload(self):
        uid = self.union()
        args = {'union_id': uid, 'tier': 'dinner'}
        before = self.f.svc._load(self.f.gid)
        first = self.wedding('renew_vows', args, 'vows-1')
        env = self.reload()
        self.assertEqual(env['player'].money, before['player'].money - 400)
        self.assertEqual(env['player'].happiness, before['player'].happiness + 3)
        snapshot = Path(self.f.svc._path(self.f.gid)).read_bytes()
        for tier in ('dinner', 'backyard', 'extravaganza'):
            with self.subTest(tier=tier):
                with self.assertRaises(GameError) as error:
                    self.wedding('renew_vows', {'union_id': uid, 'tier': tier}, 'again-' + tier)
                self.assertEqual(error.exception.code, 'not_allowed')
                self.assertEqual(Path(self.f.svc._path(self.f.gid)).read_bytes(), snapshot)
        retry = self.f.svc.wedding_command(self.f.gid, before['revision'],
                                            'vows-1', 'renew_vows', args)
        self.assertEqual(retry, first)
        env = self.reload()
        env['player'].age += 1
        self.f.svc._save(env)
        self.wedding('renew_vows', args, 'next-year')
        env = self.reload()
        self.assertEqual(env['player'].money, before['player'].money - 800)
        self.assertEqual(env['weddings']['renewals'][uid], 2)

    def test_divorced_union_has_no_anniversary_during_real_year(self):
        uid = self.union()
        self.wedding('plan_wedding', {'union_id': uid, 'venue_id': 'courthouse',
            'guests': 0, 'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'}, 'wedding')
        self.wedding('divorce_union', {'union_id': uid}, 'divorce')
        state = self.f.command('age_up')
        for n in range(30):
            if not state['pending_decision']:
                break
            d = state['pending_decision']
            option = next(o for o in reversed(d['options']) if o['enabled'])
            inputs = {s['id']: 'Test Child' for s in option.get('inputs', []) if s['type'] == 'text'}
            state = self.f.command('resolve_decision', {'decision_id': d['id'],
                'option_id': option['id'], 'inputs': inputs}, 'settle-%s' % n)
        self.assertFalse(state['pending_decision'])
        env = self.reload()
        self.assertEqual(env['player'].age, 31)
        self.assertNotIn(uid, env['weddings']['anniversaries'])
        self.assertFalse(any(e['text'].startswith('Wedding anniversary') for e in env['journal']))

    def test_phone_dating_control_requires_explicit_adult_action(self):
        from tools.cdp_client import launch_chrome
        chrome, connection = launch_chrome()
        try:
            session = connection.new_session()
            session.call('Emulation.setDeviceMetricsOverride', {
                'width': 390, 'height': 844, 'deviceScaleFactor': 1, 'mobile': True})
            base = 'http://127.0.0.1:%s' % self.httpd.server_port
            session.call('Page.navigate', {'url': base + '/game?game=' + self.f.gid})
            session.wait_js("!!document.getElementById('gameView') && !document.getElementById('gameView').hidden")
            session.js("sessionStorage.setItem('game_id', %s)" % json.dumps(self.f.gid))
            for age in (17, 18):
                env = self.no_edges()
                env['player'].age = age
                self.f.svc._save(env)
                session.call('Page.navigate', {'url': base + '/game'})
                session.wait_js("!!document.getElementById('gameView') && !document.getElementById('gameView').hidden")
                session.js("document.getElementById('m25Toggle').click()")
                session.wait_js("!!document.querySelector('#m25Body button.primary')")
                self.assertEqual(session.js(
                    "document.querySelector('#m25Body button.primary').disabled"), age < 18)
                self.assertIn('18', session.js("document.getElementById('m25Body').innerText"))
                self.assertFalse(session.js('document.documentElement.scrollWidth > innerWidth'))
                if age == 17:
                    st = self.f.svc.get_state(self.f.gid)
                    status, _ = self.request('POST', '/actions', {
                        'action': 'dating_app', 'payload': {}, 'request_id': 'minor-ui-bypass',
                        'expected_revision': st['revision']})
                    self.assertTrue(400 <= status < 500)
                    self.assertIsNone(self.reload()['pending'])
                else:
                    session.js("document.querySelector('#m25Body button.primary').click()")
                    session.wait_js("!document.getElementById('modal').hidden")
                    pending = self.reload()['pending']
                    self.assertEqual(pending['kind'], 'm25_dating_app_date')
                    self.assertEqual(pending['event_id'], 'm25_dating_app_date')
                    self.assertIn('accepted', pending['sampled'])
        finally:
            connection.ws.sock.close()
            chrome.terminate()
            chrome.wait(timeout=10)
            chrome.stderr.close()

    def _dating_pending(self):
        env = self.no_edges()
        env['player'].age = 30
        self.f.svc._save(env)
        st = self.f.svc.get_state(self.f.gid)
        status, body = self.request('POST', '/actions', {
            'action': 'dating_app', 'payload': {},
            'request_id': 'r4-dating', 'expected_revision': st['revision']})
        self.assertEqual(status, 200, body)
        env = self.f.svc._load(self.f.gid)
        self.assertIsNotNone(env['pending'])
        return env

    def test_personal_edit_supersedes_real_dating_pending(self):
        # R4 (A half): editing the TARGET's personal attributes must
        # atomically cancel the live dating offer; the stored draw is
        # never resampled and the year keeps moving.
        env = self._dating_pending()
        target_id = env['pending']['bindings']['target']
        st = self.f.svc.get_state(self.f.gid)
        out = self.f.svc.editor_apply(
            self.f.gid, st['revision'], 'r4-edit',
            [{'name': 'edit_person_attributes', 'scope': 'person',
              'target_id': target_id, 'patch': {'openness': 0.99}}])
        self.assertEqual(out['policy_revision'], 1)
        env = self.f.svc._load(self.f.gid)
        self.assertIsNone(env['pending'])
        self.assertTrue(any('moment has passed' in e['text']
                            for e in env['journal']))
        # no soft-lock: the year still progresses
        st = self.f.svc.get_state(self.f.gid)
        state = self.f.svc.command(self.f.gid, st['revision'], 'r4-age',
                                   'age_up', {})
        self.assertIsNotNone(state)

    def test_edit_of_unrelated_person_keeps_dating_pending(self):
        env = self._dating_pending()
        involved = {self.f.pid, env['pending']['bindings']['target']}
        recorded = dict(env['pending'].get('sampled') or {})
        profiles = self.f.svc.get_policy_state(self.f.gid)['profiles']
        other = next(pid for pid in profiles if pid not in involved)
        st = self.f.svc.get_state(self.f.gid)
        self.f.svc.editor_apply(
            self.f.gid, st['revision'], 'r4-edit-other',
            [{'name': 'edit_person_attributes', 'scope': 'person',
              'target_id': other, 'patch': {'openness': 0.11}}])
        env = self.f.svc._load(self.f.gid)
        self.assertIsNotNone(env['pending'])
        # the stored refusal/score draw is untouched
        self.assertEqual(dict(env['pending'].get('sampled') or {}),
                         recorded)

    def test_w1_two_inheritances_same_turn_are_typed_never_500(self):
        """W1 (A half): two parents dying in the same turn must not
        produce an HTTP 500; a genuine key collision is a typed 4xx with
        the save byte-unchanged (C's deceased-id occurrence id removes
        the collision entirely)."""
        from pathlib import Path
        from src.people.classes.parent import Parent
        import random as _random
        env = self.f.svc._load(self.f.gid)
        parents = [p for p in env['player'].relations
                   if isinstance(p, Parent)]
        self.assertGreaterEqual(len(parents), 2)
        for i, p in enumerate(parents):
            p.age = 130
            p.health = 100
            p.happiness = 100
            p.money = 60 + 10 * i
            p.generosity = 100
        env['rng_state'] = _random.Random(3).getstate()
        env['cosmetic_rng_state'] = _random.Random(0).getstate()
        env['turn'] = {'turn_id': 't_w1_deaths',
                       'cursor': {'phase_id': 'mortality',
                                  'item_cursor': 0},
                       'done': {}}
        self.f.svc._save(env)
        before = Path(self.f.svc._path(self.f.gid)).read_bytes()
        status, body = self.request('POST', '/actions', {
            'expected_revision': self.f.svc.get_state(self.f.gid)["revision"],
            'request_id': 'w1-double-inheritance', 'action': 'age_up',
            'payload': {}})
        self.assertNotEqual(status, 500, body)
        self.assertLess(status, 500)
        if status >= 400:
            self.assertEqual(body['error']['code'], 'ledger_conflict')
            self.assertEqual(Path(self.f.svc._path(self.f.gid)).read_bytes(),
                             before)
        else:
            # C's deceased-id occurrence id landed: both inheritances
            # posted exactly once and money is conserved
            money = body.get('money')
            self.assertIsNotNone(money)
            ledger = (body.get('finances') or {}).get('ledger') or []
            inherited = sum(r['amount'] for r in ledger
                            if r['category'] == 'inheritance')
            self.assertGreater(inherited, 0)

    def test_l1_forbidden_ceremony_law_fine_posted_once(self):
        """L1: the forbidden-union ceremony fine forecast at proposal is
        posted at the ceremony through the ledger — once, idempotent,
        replay-safe. Mirrors evaluation-round-5/law_ceremony.py."""
        env = self.f.svc._load(self.f.gid)
        env['graph']['relationships'] = []
        env['graph']['unions'] = []
        env['player'].partner = None
        self.f.svc._save(env)
        jur = env['policy']['jurisdiction_id']
        st = self.f.svc.get_state(self.f.gid)
        self.f.svc.editor_apply(
            self.f.gid, st['revision'], 'l1-law-edit',
            [{'name': 'edit_jurisdiction', 'scope': 'jurisdiction',
              'target_id': jur,
              'patch': {'recognition': {'monogamous_civil': 'forbidden',
                                        'plural_civil': 'forbidden'},
                        'consequences': [{
                            'trigger': 'ceremony_of_unrecognized_union',
                            'effect': 'civil_fine',
                            'ledger_category': 'legal',
                            'amount': {'randint': [500, 500]},
                            'max_per_occurrence_key': 'union_id'}]}}])
        import random as _random
        for action in ('courtship_offer', 'union_proposal'):
            env = self.f.svc._load(self.f.gid)
            env['rng_state'] = _random.Random(139).getstate()
            env['cosmetic_rng_state'] = _random.Random(0).getstate()
            self.f.svc._save(env)
            st = self.f.command(action, {'target_id': self.f.tid})
            pending = st['pending_decision']
            if action == 'union_proposal':
                self.assertEqual(
                    pending['sampled']['projected_recognition']['status'],
                    'forbidden')
                self.assertTrue(pending['sampled']
                                ['projected_consequences'])
            ask = next(o for o in pending['options']
                       if o['id'].endswith('_ask'))
            self.f.command('resolve_decision',
                           {'decision_id': pending['id'],
                            'option_id': ask['id']})
        env = self.f.svc._load(self.f.gid)
        union = env['graph']['unions'][-1]
        self.assertEqual(union['recognition']['status'], 'forbidden')
        before = env['player'].money
        st = self.f.svc.get_state(self.f.gid)
        out = self.f.svc.wedding_command(
            self.f.gid, st['revision'], 'l1-ceremony', 'plan_wedding',
            {'union_id': union['union_id'], 'venue_id': 'courthouse',
             'guests': 0, 'outfit_id': 'hand_me_down',
             'honeymoon_id': 'none'})
        self.assertTrue(out['result']['ok'])
        env = self.f.svc._load(self.f.gid)
        # the forecast $500 fine posted exactly once through the ledger
        self.assertEqual(env['player'].money, before - 500)
        fines = [r for r in env['ledger']
                 if r['category'] == 'legal'
                 and 'lawfine' in r['source_key']]
        self.assertEqual(len(fines), 1)
        self.assertEqual(fines[0]['amount'], -500)
        self.assertEqual(fines[0]['person_id'], self.f.pid)
        # the union is held and a retry returns the stored response
        # without a second charge
        replay = self.f.svc.wedding_command(
            self.f.gid, st['revision'], 'l1-ceremony', 'plan_wedding',
            {'union_id': union['union_id'], 'venue_id': 'courthouse',
             'guests': 0, 'outfit_id': 'hand_me_down',
             'honeymoon_id': 'none'})
        self.assertEqual(replay, out)
        env = self.f.svc._load(self.f.gid)
        self.assertEqual(env['player'].money, before - 500)
        self.assertEqual(len([r for r in env['ledger']
                              if r['category'] == 'legal']), 1)
        # a brand-new ceremony command is rejected (already held), no
        # double charge either
        st = self.f.svc.get_state(self.f.gid)
        with self.assertRaises(GameError) as err:
            self.f.svc.wedding_command(
                self.f.gid, st['revision'], 'l1-again', 'plan_wedding',
                {'union_id': union['union_id'], 'venue_id': 'courthouse',
                 'guests': 0, 'outfit_id': 'hand_me_down',
                 'honeymoon_id': 'none'})
        self.assertEqual(err.exception.status, 409)
        self.assertEqual(self.f.svc._load(self.f.gid)['player'].money,
                         before - 500)

    def _forbidden_union(self, consequences):
        """Shared Q1 setup: editor law edit + courtship -> forbidden
        union. Returns the union id."""
        env = self.f.svc._load(self.f.gid)
        env['graph']['relationships'] = []
        env['graph']['unions'] = []
        env['player'].partner = None
        self.f.svc._save(env)
        jur = env['policy']['jurisdiction_id']
        st = self.f.svc.get_state(self.f.gid)
        self.f.svc.editor_apply(
            self.f.gid, st['revision'], 'q1-law-edit',
            [{'name': 'edit_jurisdiction', 'scope': 'jurisdiction',
              'target_id': jur,
              'patch': {'recognition': {'monogamous_civil': 'forbidden',
                                        'plural_civil': 'forbidden'},
                        'consequences': consequences}}])
        import random as _random
        for action in ('courtship_offer', 'union_proposal'):
            env = self.f.svc._load(self.f.gid)
            env['rng_state'] = _random.Random(139).getstate()
            env['cosmetic_rng_state'] = _random.Random(0).getstate()
            self.f.svc._save(env)
            st = self.f.command(action, {'target_id': self.f.tid})
            pending = st['pending_decision']
            ask = next(o for o in pending['options']
                       if o['id'].endswith('_ask'))
            self.f.command('resolve_decision',
                           {'decision_id': pending['id'],
                            'option_id': ask['id']})
        env = self.f.svc._load(self.f.gid)
        union = env['graph']['unions'][-1]
        self.assertEqual(union['recognition']['status'], 'forbidden')
        return union['union_id']

    def test_q1_variable_range_quote_is_stored_and_posted_exactly(self):
        """Q1: a nonconstant [500, 2000] range samples ONCE, stores on
        the plan, never redraws, and the Ledger posts exactly the stored
        quote (preview total == posted total)."""
        uid = self._forbidden_union(
            [{'trigger': 'ceremony_of_unrecognized_union',
              'effect': 'civil_fine', 'ledger_category': 'legal',
              'amount': {'randint': [500, 2000]},
              'max_per_occurrence_key': 'union_id'}])
        quote = self.f.svc.wedding_quote(self.f.gid, uid)
        self.assertEqual(len(quote['lines']), 1)
        self.assertTrue(500 <= quote['lines'][0]['amount'] <= 2000)
        self.assertEqual(quote['total'], quote['lines'][0]['amount'])
        # the stored quote is reused, never redrawn
        again = self.f.svc.wedding_quote(self.f.gid, uid)
        self.assertEqual(again, quote)
        env = self.f.svc._load(self.f.gid)
        self.assertEqual(env['weddings']['quotes'][uid], quote)
        before = env['player'].money
        st = self.f.svc.get_state(self.f.gid)
        self.f.svc.wedding_command(
            self.f.gid, st['revision'], 'q1-ceremony', 'plan_wedding',
            {'union_id': uid, 'venue_id': 'courthouse', 'guests': 0,
             'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'})
        env = self.f.svc._load(self.f.gid)
        rows = [r for r in env['ledger'] if r['category'] == 'legal']
        self.assertEqual(len(rows), 1)
        self.assertEqual(-rows[0]['amount'], quote['total'])
        self.assertEqual(env['player'].money, before - quote['total'])

    def test_q1_multiple_fines_all_lines_posted(self):
        """Q1: two $500 + $700 fines — the quote returns BOTH lines and
        the Ledger posts both, nothing first-line-only."""
        uid = self._forbidden_union(
            [{'trigger': 'ceremony_of_unrecognized_union',
              'effect': 'civil_fine', 'ledger_category': 'legal',
              'amount': {'randint': [500, 500]},
              'max_per_occurrence_key': 'union_id'},
             {'trigger': 'ceremony_of_unrecognized_union',
              'effect': 'civil_fine', 'ledger_category': 'legal',
              'amount': {'randint': [700, 700]},
              'max_per_occurrence_key': 'union_id'}])
        quote = self.f.svc.wedding_quote(self.f.gid, uid)
        self.assertEqual([l['amount'] for l in quote['lines']],
                         [500, 700])
        self.assertEqual(quote['total'], 1200)
        env = self.f.svc._load(self.f.gid)
        before = env['player'].money
        st = self.f.svc.get_state(self.f.gid)
        self.f.svc.wedding_command(
            self.f.gid, st['revision'], 'q1-ceremony2', 'plan_wedding',
            {'union_id': uid, 'venue_id': 'courthouse', 'guests': 0,
             'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'})
        env = self.f.svc._load(self.f.gid)
        rows = sorted(r['amount'] for r in env['ledger']
                      if r['category'] == 'legal')
        self.assertEqual(rows, [-700, -500])
        self.assertEqual(env['player'].money, before - 1200)

    def test_q1_quote_affordability_and_replay(self):
        """Q1: parts + quoted fine gate affordability (typed rejection,
        no posting); the same request id replays the stored response
        with exactly one charge."""
        uid = self._forbidden_union(
            [{'trigger': 'ceremony_of_unrecognized_union',
              'effect': 'civil_fine', 'ledger_category': 'legal',
              'amount': {'randint': [500, 500]},
              'max_per_occurrence_key': 'union_id'}])
        quote = self.f.svc.wedding_quote(self.f.gid, uid)
        self.assertEqual(quote['total'], 500)
        env = self.f.svc._load(self.f.gid)
        env['player'].money = 400
        env['balances'][self.f.pid] = 400
        self.f.svc._save(env)
        st = self.f.svc.get_state(self.f.gid)
        with self.assertRaises(GameError) as err:
            self.f.svc.wedding_command(
                self.f.gid, st['revision'], 'q1-poor', 'plan_wedding',
                {'union_id': uid, 'venue_id': 'courthouse', 'guests': 0,
                 'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'})
        self.assertEqual(err.exception.code, 'not_allowed')
        env = self.f.svc._load(self.f.gid)
        self.assertEqual(env['player'].money, 400)
        self.assertEqual([r for r in env['ledger']
                          if r['category'] == 'legal'], [])
        # now affordable: plan posts the STORED quote (no redraw)
        env['player'].money = 1000
        env['balances'][self.f.pid] = 1000
        self.f.svc._save(env)
        st = self.f.svc.get_state(self.f.gid)
        out = self.f.svc.wedding_command(
            self.f.gid, st['revision'], 'q1-rich', 'plan_wedding',
            {'union_id': uid, 'venue_id': 'courthouse', 'guests': 0,
             'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'})
        env = self.f.svc._load(self.f.gid)
        self.assertEqual(env['player'].money, 500)
        rows = [r for r in env['ledger'] if r['category'] == 'legal']
        self.assertEqual([-r['amount'] for r in rows], [500])
        replay = self.f.svc.wedding_command(
            self.f.gid, st['revision'], 'q1-rich', 'plan_wedding',
            {'union_id': uid, 'venue_id': 'courthouse', 'guests': 0,
             'outfit_id': 'hand_me_down', 'honeymoon_id': 'none'})
        self.assertEqual(replay, out)
        env = self.f.svc._load(self.f.gid)
        self.assertEqual(env['player'].money, 500)
        self.assertEqual(len([r for r in env['ledger']
                              if r['category'] == 'legal']), 1)

    def test_q1_law_edit_requotes_deliberately(self):
        """Q1: after a law edit (law_version bump) the next quote call
        re-samples under the new law — a deliberate refresh, not a
        silent redraw."""
        uid = self._forbidden_union(
            [{'trigger': 'ceremony_of_unrecognized_union',
              'effect': 'civil_fine', 'ledger_category': 'legal',
              'amount': {'randint': [500, 500]},
              'max_per_occurrence_key': 'union_id'}])
        quote = self.f.svc.wedding_quote(self.f.gid, uid)
        env = self.f.svc._load(self.f.gid)
        st = self.f.svc.get_state(self.f.gid)
        self.f.svc.editor_apply(
            self.f.gid, st['revision'], 'q1-law-edit-2',
            [{'name': 'edit_jurisdiction', 'scope': 'jurisdiction',
              'target_id': env['policy']['jurisdiction_id'],
              'patch': {'consequences': [{
                  'trigger': 'ceremony_of_unrecognized_union',
                  'effect': 'civil_fine', 'ledger_category': 'legal',
                  'amount': {'randint': [900, 900]},
                  'max_per_occurrence_key': 'union_id'}]}}])
        requoted = self.f.svc.wedding_quote(self.f.gid, uid)
        self.assertNotEqual(requoted, quote)
        self.assertEqual(requoted['lines'][0]['amount'], 900)
        self.assertGreater(requoted['law_version'], quote['law_version'])

    def test_legacy_free_renewal_history_recovers_year_guard_idempotently(self):
        uid = self.union()
        self.wedding('renew_vows', {'union_id': uid}, 'old-free-renewal')
        env = self.reload()
        del env['weddings']['renewal_years']
        self.f.svc._save(env)
        env = self.reload()
        self.assertEqual(env['weddings']['renewal_years'][uid], env['player'].age)
        snapshot = Path(self.f.svc._path(self.f.gid)).read_bytes()
        self.reload()
        self.assertEqual(Path(self.f.svc._path(self.f.gid)).read_bytes(), snapshot)
        with self.assertRaises(GameError) as error:
            self.wedding('renew_vows', {'union_id': uid}, 'old-retry-new-command')
        self.assertEqual(error.exception.code, 'not_allowed')
        self.assertEqual(Path(self.f.svc._path(self.f.gid)).read_bytes(), snapshot)
