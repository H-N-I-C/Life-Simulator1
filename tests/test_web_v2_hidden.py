"""S1: semantic hidden must beat the v2 shell's flex layout in Chromium."""
import json
import shutil
import tempfile
import threading
import unittest
from pathlib import Path

from tools.cdp_client import launch_chrome
from web import server


class V2HiddenBrowserTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.save_dir = tempfile.mkdtemp(
            prefix='v2-hidden-', dir=Path(__file__).resolve().parents[1] / 'game_saves')
        cls.httpd = server.make_server(0, save_dir=cls.save_dir)
        cls.thread = threading.Thread(target=cls.httpd.serve_forever, daemon=True)
        cls.thread.start()
        cls.base = 'http://127.0.0.1:%d' % cls.httpd.server_port
        cls.game = cls.httpd.service.new_game({
            'first_name': 'Visibility', 'last_name': 'Review',
            'gender': 'female', 'country': 'Australia', 'seed': 42,
        }, 'hidden-create')

    @classmethod
    def tearDownClass(cls):
        cls.httpd.shutdown()
        cls.httpd.server_close()
        shutil.rmtree(cls.save_dir)

    def test_all_lives_hides_shell_and_resume_reveals_it(self):
        proc, conn = launch_chrome()
        try:
            page = conn.new_session(self.base + '/game?game=' + self.game['game_id'])
            page.wait_js("!!document.querySelector('.v2-tabbar') && "
                         "document.getElementById('gameView').checkVisibility()")
            self.assertTrue(page.js("document.getElementById('gameView').checkVisibility()"))
            page.js("document.querySelector('[aria-label=\"More routes\"]').click()")
            page.wait_js("!document.querySelector('.v2-more').hidden")
            page.js("Array.from(document.querySelectorAll('.v2-more button'))"
                    ".find(b=>b.textContent==='All lives').click()")
            page.wait_js("!document.getElementById('resumeView').hidden && "
                         "!!document.querySelector('#lifeList button')")
            visible = page.js("(()=>{const g=document.getElementById('gameView');"
                              "return {hidden:g.hidden,visible:g.checkVisibility(),"
                              "display:getComputedStyle(g).display,"
                              "resume:document.getElementById('resumeView').checkVisibility()};})()")
            self.assertTrue(visible['hidden'])
            self.assertFalse(visible['visible'], json.dumps(visible))
            self.assertEqual(visible['display'], 'none')
            self.assertTrue(visible['resume'])
            page.js("document.querySelector('#lifeList button').click()")
            page.wait_js("!document.getElementById('gameView').hidden")
            self.assertTrue(page.js("document.getElementById('gameView').checkVisibility()"))
            self.assertEqual(page.js("getComputedStyle(document.getElementById('gameView')).display"), 'flex')
            self.assertFalse(page.js("document.getElementById('resumeView').checkVisibility()"))
            self.assertEqual(page.console_errors, [])
        finally:
            proc.terminate()
