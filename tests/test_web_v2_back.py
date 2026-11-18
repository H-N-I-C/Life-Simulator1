"""Real-browser navigation history regressions: Back, Forward and reload."""
import shutil
import tempfile
import threading
import unittest

from tools.cdp_client import launch_chrome
from web import server


class BackNavigationBrowserTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.tmp = tempfile.mkdtemp(prefix='back-')
        cls.httpd = server.make_server(0, save_dir=cls.tmp)
        threading.Thread(target=cls.httpd.serve_forever, daemon=True).start()
        cls.base = 'http://127.0.0.1:%d' % cls.httpd.server_port

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.tmp)

    def open_game(self):
        st = self.httpd.service.new_game(dict(first_name='Back', last_name='Review',
            gender='female', country='Australia', seed=42), self.id())
        proc, conn = launch_chrome()
        def close():
            proc.terminate(); proc.wait(timeout=5)
            if proc.stderr: proc.stderr.close()
        self.addCleanup(close)
        page = conn.new_session(self.base + '/game?game=' + st['game_id'])
        page.wait_js("!!document.querySelector('.v2h-age')")
        return page, st

    def screen(self, page, name):
        page.wait_js("!document.getElementById('screen-%s').hidden && "
                     "document.getElementById('gameView').checkVisibility()" % name,
                     timeout=3, desc=name + ' screen visible')

    def tab(self, page, name):
        page.js("document.querySelector('.v2-tab[data-screen=%s]').click()" % name)
        self.screen(page, name)

    def back(self, page):
        page.js("document.querySelector('.v2-back').click()")

    def test_forward_then_visible_back_preserves_history_depth(self):
        page, _ = self.open_game()
        self.tab(page, 'job'); self.tab(page, 'assets')
        page.js('history.back()'); self.screen(page, 'job')
        page.js('history.forward()'); self.screen(page, 'assets')
        self.back(page); self.screen(page, 'job')
        self.back(page); self.screen(page, 'home')
        self.assertEqual(page.console_errors, [])

    def test_reload_keeps_destination_and_previous_screen(self):
        page, _ = self.open_game()
        self.tab(page, 'job'); self.tab(page, 'assets')
        page.call('Page.reload', {})
        page.wait_js("!!document.querySelector('.v2h-age')")
        self.screen(page, 'assets')
        self.back(page); self.screen(page, 'job')

    def test_all_lives_back_restores_plain_home(self):
        page, _ = self.open_game()
        # Existing tabs can retain B's earlier unindexed history payload.
        page.js("history.replaceState({v2:1,screen:'home',scroll:0},'')")
        page.call('Page.reload', {})
        page.wait_js("!!document.querySelector('.v2h-age')")
        page.js("document.getElementById('backToLives').click()")
        page.wait_js("document.getElementById('resumeView').checkVisibility()")
        page.call('Page.reload', {})
        page.wait_js("document.getElementById('resumeBackBtn') && "
                     "!document.getElementById('resumeBackBtn').hidden")
        page.js("document.getElementById('resumeBackBtn').click()")
        self.screen(page, 'home')

    def test_all_lives_back_restores_nonhome_destination(self):
        page, _ = self.open_game()
        self.tab(page, 'job')
        page.js("document.getElementById('backToLives').click()")
        page.wait_js("document.getElementById('resumeView').checkVisibility()")
        page.js("document.getElementById('resumeBackBtn').click()")
        self.screen(page, 'job')

    def test_reopening_editor_does_not_add_an_extra_back_step(self):
        page, _ = self.open_game()
        for _ in range(2):
            page.js("document.querySelector('[aria-label=\"More routes\"]').click();"
                    "Array.from(document.querySelectorAll('.v2-more button'))"
                    ".find(b=>b.textContent==='Character editor').click()")
            page.wait_js("document.getElementById('editorPanel') && "
                         "document.getElementById('editorPanel').checkVisibility()")
        self.back(page)
        self.screen(page, 'home')
        self.assertFalse(page.js("document.getElementById('editorPanel').checkVisibility()"))

    def test_more_family_tree_and_dating_are_visible_from_home(self):
        page, _ = self.open_game()
        for label, panel in [('Family tree', 'familyTreeCard'),
                             ('Dating & weddings', 'm25Card')]:
            page.js("document.querySelector('[aria-label=\"More routes\"]').click();"
                    "Array.from(document.querySelectorAll('.v2-more button'))"
                    ".find(b=>b.textContent===%r).click()" % label)
            page.wait_js("document.getElementById(%r) && "
                         "document.getElementById(%r).checkVisibility()" % (panel, panel),
                         timeout=3)
            self.back(page); self.screen(page, 'home')

    def test_person_back_uses_same_history_and_person_survives_reload(self):
        page, _ = self.open_game()
        self.tab(page, 'people')
        page.js("document.querySelector('#familyList .chip').click()")
        page.wait_js("!!document.querySelector('.v2p-name')")
        name = page.js("document.querySelector('.v2p-name').textContent")
        page.call('Page.reload', {})
        page.wait_js("!!document.querySelector('.v2p-name')")
        self.assertEqual(page.js("document.querySelector('.v2p-name').textContent"), name)
        page.js("document.querySelector('.v2p-header-btn[aria-label=Back]').click()")
        page.wait_js("document.querySelector('.v2-person-host').hidden")
        # One Back should consume Person, not push a second People entry.
        self.back(page); self.screen(page, 'home')
        page.js('history.forward()'); self.screen(page, 'people')
        page.js('history.forward()')
        page.wait_js("!!document.querySelector('.v2p-name')")

    def test_switch_lives_then_back_restores_original_identity(self):
        page, state = self.open_game()
        self.tab(page, 'job')
        other = self.httpd.service.new_game(dict(first_name='Other', last_name='Life',
            gender='male', country='Australia', seed=43), self.id()+'-other')
        page.js("document.getElementById('backToLives').click()")
        page.wait_js("Array.from(document.querySelectorAll('#lifeList .chip'))"
                     ".some(e=>e.textContent.includes('Other Life'))")
        page.js("Array.from(document.querySelectorAll('#lifeList .chip'))"
                ".find(e=>e.textContent.includes('Other Life')).querySelector('button').click()")
        self.screen(page, 'home')
        self.assertEqual(page.js("sessionStorage.getItem('game_id')"), other['game_id'])
        page.js('history.back()')
        page.wait_js("document.getElementById('resumeView').checkVisibility()")
        page.js('history.back()'); self.screen(page, 'job')
        self.assertEqual(page.js("sessionStorage.getItem('game_id')"), state['game_id'])
        self.assertIn('Back Review', page.js("document.querySelector('.v2-header-name').textContent"))

    def test_browser_back_preserves_pending_decision_and_revision(self):
        page, state = self.open_game()
        self.tab(page, 'job'); self.tab(page, 'activities')
        page.js("Array.from(document.querySelectorAll('#actionList button'))"
                ".find(e=>e.textContent.includes('Surrender')).click()")
        page.wait_js("!document.getElementById('modal').hidden && "
                     "!!document.querySelector('.v2c-options')")
        before = self.httpd.service.get_state(state['game_id'])
        self.assertIsNotNone(before['pending_decision'])
        page.js('history.back()'); self.screen(page, 'job')
        after = self.httpd.service.get_state(state['game_id'])
        self.assertEqual(after['pending_decision']['id'], before['pending_decision']['id'])
        self.assertEqual(after['revision'], before['revision'])
        self.assertFalse(page.js("document.getElementById('modal').hidden"))
        self.assertTrue(page.js("document.querySelector('.v2h-age').disabled"))
