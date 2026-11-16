"""Render tests for the M0b Fluent 3D avatar resolution (Kimi B).

The SVG renderer was retired; these tests verify avatar_image_key against
the manifest: stage mapping, key selection, fallback chains, the
baby/child badge guardrail, v1 migration and determinism.

Run: python3 -m unittest tests.test_render
"""

import os
import sys
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.appearance import catalog  # noqa: E402
from src.appearance.render import avatar_image_key, AVATAR_DIR  # noqa: E402

V2 = {"schema_version": 2, "seed": 7, "base": "person", "skin_tone":
      "default", "hair": "default", "facial_hair": "none", "headwear":
      "none", "badges": [], "background": "bg_01", "look_override": "none"}


def app(**over):
    a = dict(V2)
    a.update(over)
    return a


class TestAvatarKeyResolution(unittest.TestCase):
    def assert_found(self, a, age):
        key = avatar_image_key(a, age)
        self.assertTrue(key["found"], "no asset for %r at %d" % (a, age))
        self.assertTrue(key["file"].endswith(".png"))
        self.assertTrue(os.path.isfile(os.path.join(AVATAR_DIR, key["file"])))
        return key

    def test_stage_mapping(self):
        self.assertEqual(avatar_image_key(V2, 0)["stage"], "baby")
        self.assertEqual(avatar_image_key(V2, 2)["stage"], "baby")
        self.assertEqual(avatar_image_key(V2, 3)["stage"], "child")
        self.assertEqual(avatar_image_key(V2, 12)["stage"], "child")
        self.assertEqual(avatar_image_key(V2, 13)["stage"], "teen_adult")
        self.assertEqual(avatar_image_key(V2, 59)["stage"], "teen_adult")
        self.assertEqual(avatar_image_key(V2, 60)["stage"], "greying")
        self.assertEqual(avatar_image_key(V2, 69)["stage"], "greying")
        self.assertEqual(avatar_image_key(V2, 70)["stage"], "elder")

    def test_all_skins_and_ages_resolve(self):
        for e in catalog.CATEGORIES["skin_tone"]:
            for age in (0, 5, 25, 45, 65, 75):
                key = self.assert_found(app(skin_tone=e["id"]), age)
                self.assertTrue(key["background_hex"].startswith("#"))

    def test_bases_resolve(self):
        for base in ("person", "woman", "man"):
            for age in (0, 8, 30, 72):
                self.assert_found(app(base=base), age)

    def test_hair_variants(self):
        for hair in ("default", "curly", "red", "white", "bald"):
            k1 = self.assert_found(app(hair=hair), 30)
            k2 = self.assert_found(app(base="man", hair=hair), 45)
            self.assertNotEqual(k1["asset"], k2["asset"])

    def test_greying_resolves_via_manifest_chain(self):
        # the manifest owns greying fallbacks: chains point at the
        # white-hair adult assets (no greying+bald combo exists)
        k = self.assert_found(app(hair="red", seed=7), 65)
        self.assertTrue(k["found"])
        k = self.assert_found(app(hair="bald", seed=7), 65)
        self.assertTrue(k["found"])

    def test_beard_and_headscarf(self):
        k = self.assert_found(app(base="man", facial_hair="beard"), 40)
        self.assertIn("beard", k["asset"])
        k = self.assert_found(app(base="woman", headwear="headscarf"), 40)
        self.assertIn("headscarf", k["asset"])

    def test_baby_child_badge_guardrail(self):
        k = avatar_image_key(app(badges=["glasses", "crown"]), 1)
        self.assertEqual(k["badges"], [])
        k = avatar_image_key(app(badges=["glasses", "crown"]), 8)
        self.assertEqual(k["badges"], [])
        k = avatar_image_key(app(badges=["glasses", "crown"]), 30)
        self.assertEqual(len(k["badges"]), 2)
        for b in k["badges"]:
            self.assertTrue(os.path.isfile(os.path.join(AVATAR_DIR,
                                                        b + ".png")))

    def test_look_override(self):
        k = self.assert_found(app(look_override="artist"), 30)
        self.assertIn("look_artist", k["asset"])
        # unknown look falls back to the regular look, still resolved
        k = self.assert_found(app(look_override="nope"), 30)
        self.assertTrue(k["found"])

    def test_v1_dict_migrates_and_resolves(self):
        v1 = {"schema_version": 1, "seed": 7, "skin_tone": "skin_04",
              "hair_style": "short_crop", "hair_color": "red",
              "accessories": {"glasses": "none", "headwear": "none",
                              "earrings": "studs"}}
        key = self.assert_found(v1, 30)
        self.assertEqual(key["stage"], "teen_adult")

    def test_unknown_ids_fall_back(self):
        key = self.assert_found(app(skin_tone="nope", hair="???",
                                    base="alien"), 30)
        self.assertTrue(key["found"])

    def test_garbage_input_still_resolves(self):
        for bad in ({}, None, {"badges": "glasses"}, 42):
            key = avatar_image_key(bad, 30)
            self.assertTrue(key["found"])

    def test_deterministic(self):
        a = app(seed=3, hair="red")
        self.assertEqual(avatar_image_key(a, 62), avatar_image_key(a, 62))

    def test_context_argument_accepted(self):
        key = avatar_image_key(V2, 30, context={"job": "artist"})
        self.assertTrue(key["found"])


if __name__ == "__main__":
    unittest.main()


class TestAvatarEndpointConcurrency(unittest.TestCase):
    """Burst-safety: concurrent thumbnail requests never 400 and agree."""

    def test_concurrent_avatar_requests(self):
        import base64
        import concurrent.futures as cf
        import json as _json
        import threading
        import urllib.error
        import urllib.request

        import web.server as server

        httpd = server.make_server(0, save_dir=self._tmpdir())
        threading.Thread(target=httpd.serve_forever, daemon=True).start()
        try:
            port = httpd.server_port
            apps = []
            for skin in ("default", "light", "medium", "dark"):
                for age in (1, 8, 30, 65, 75):
                    apps.append(({"schema_version": 2, "seed": 5,
                                  "base": "person", "skin_tone": skin,
                                  "hair": "default"}, age))

            def fetch(app_age):
                app, age = app_age
                raw = base64.urlsafe_b64encode(
                    _json.dumps(app, separators=(",", ":")).encode()
                    ).decode().rstrip("=")
                url = ("http://127.0.0.1:%d/api/avatar.png?app=%s&age=%d"
                       % (port, raw, age))
                try:
                    with urllib.request.urlopen(url, timeout=15) as r:
                        return r.status, r.read()
                except urllib.error.HTTPError as e:
                    return e.code, e.read()

            # 32 threads x full matrix twice: a creator-scale burst
            with cf.ThreadPoolExecutor(max_workers=32) as pool:
                results = list(pool.map(fetch, apps * 2))
            codes = [s for s, _ in results]
            self.assertEqual(set(codes), {200},
                             "non-200 under burst: %r" %
                             sorted(set(codes)))
            bodies = [b for _, b in results]
            for b in bodies:
                self.assertTrue(b[:8] == b"\x89PNG\r\n\x1a\n")
            # same key -> identical bytes across threads (cache coherence)
            n_apps = len(apps)
            for i in range(n_apps):
                self.assertEqual(bodies[i], bodies[i + n_apps])
        finally:
            httpd.shutdown()
            httpd.server_close()

    def _tmpdir(self):
        import tempfile
        return tempfile.mkdtemp(prefix="avatars-burst-")


if __name__ == "__main__":
    unittest.main()
