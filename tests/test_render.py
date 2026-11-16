"""Render tests for the avatar SVG (Kimi B). Run: python3 -m unittest discover tests"""

import os
import sys
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.appearance import catalog  # noqa: E402
from src.appearance.render import render_avatar_svg  # noqa: E402

BASE = {
    "schema_version": 1, "seed": 123456789, "skin_tone": "skin_04",
    "face_shape": "oval", "eye_shape": "almond", "eye_color": "brown_dark",
    "brows": "soft", "nose": "button", "mouth": "smile",
    "hair_texture": "straight", "hair_color": "black", "marks": ["freckles"],
    "hair_style": "short_crop", "facial_hair": "none",
    "accessories": {"glasses": "none", "headwear": "none",
                    "earrings": "none"},
    "outfit_style": "tee", "outfit_color": "blue",
}

AGES = [0, 1, 2, 3, 8, 12, 13, 17, 18, 30, 49, 50, 65, 80]


def variant(category, value, age=30):
    app = dict(BASE)
    if category in ("glasses", "headwear", "earrings"):
        app["accessories"] = {"glasses": "none", "headwear": "none",
                              "earrings": "none"}
        app["accessories"][category] = value
    elif category == "marks":
        app["marks"] = [value]
    else:
        app[category] = value
    return render_avatar_svg(app, age)


class TestRender(unittest.TestCase):
    def assert_svg(self, app, age):
        svg = render_avatar_svg(app, age)
        self.assertTrue(svg.startswith("<svg"))
        self.assertIn('viewBox="0 0 200 200"', svg)
        self.assertNotIn("<script", svg)
        self.assertNotIn("http://", svg.replace("http://www.w3.org", ""))
        return svg

    def test_all_ages(self):
        for age in AGES:
            self.assert_svg(BASE, age)

    def test_every_catalog_value(self):
        for category, entries in catalog.CATEGORIES.items():
            for e in entries:
                for age in (1, 8, 15, 30, 70):
                    self.assert_svg(self._app(category, e["id"]), age)

    def _app(self, category, value):
        app = dict(BASE)
        app["accessories"] = {"glasses": "none", "headwear": "none",
                              "earrings": "none"}
        if category in ("glasses", "headwear", "earrings"):
            app["accessories"][category] = value
        elif category == "marks":
            app["marks"] = [value]
        else:
            app[category] = value
        return app

    def test_unknown_ids_fall_back(self):
        app = dict(BASE)
        app["hair_style"] = "nonexistent"
        app["skin_tone"] = "nope"
        app["accessories"] = {"glasses": "??", "headwear": None,
                              "earrings": "x"}
        self.assert_svg(app, 30)

    def test_tolerates_missing_and_bad_input(self):
        self.assert_svg({}, 30)
        self.assert_svg(None, 5)
        self.assert_svg({"marks": "freckles"}, 70)
        # unhashable / wrong types must not crash (review finding 5/8)
        self.assert_svg({"hair_style": ["a", "b"]}, 30)
        self.assert_svg({"skin_tone": {"tone": 1}}, 30)
        self.assert_svg({"age": "x"}, 30)

    def test_deterministic(self):
        self.assertEqual(render_avatar_svg(BASE, 30),
                         render_avatar_svg(dict(BASE), 30))

    def test_age_changes_output(self):
        self.assertNotEqual(render_avatar_svg(BASE, 1),
                            render_avatar_svg(BASE, 70))

    # -- visual distinction (review finding 3: no inert controls) ----------
    def _all_differ(self, category, ids, age=30):
        svgs = [variant(category, i, age) for i in ids]
        self.assertEqual(len(set(svgs)), len(ids),
                         "%s variants must differ: %s" % (category, ids))

    def test_hair_texture_differs(self):
        self._all_differ("hair_texture", ["straight", "wavy", "curly",
                                          "coily"])
        # texture must be visible on a compatible style
        self._all_differ("hair_texture", ["straight", "curly"], age=30)

    def test_eye_shapes_differ(self):
        self._all_differ("eye_shape", ["almond", "round", "upturned",
                                       "downturned", "hooded", "monolid",
                                       "deep_set"])

    def test_hair_styles_differ(self):
        self._all_differ("hair_style", ["short_crop", "slick_back",
                                        "curtains", "side_part", "bob",
                                        "wavy_bob", "buzz"])

    def test_other_features_differ(self):
        self._all_differ("face_shape", ["oval", "round", "square", "heart",
                                        "long", "diamond"])
        self._all_differ("brows", ["soft", "thick", "thin", "arched",
                                   "straight", "bushy"])
        self._all_differ("nose", ["button", "straight", "wide", "pointed",
                                  "upturned", "hooked"])
        self._all_differ("mouth", ["smile", "neutral", "full", "thin",
                                   "wide", "pout"])

    # -- review finding 1: afro must not hide the face ----------------------
    def test_afro_keeps_features_visible(self):
        for hat in ("none", "cap", "beanie", "headscarf"):
            svg = variant("hair_style", "afro_medium") if hat == "none" \
                else render_avatar_svg({**BASE, "hair_style": "afro_medium",
                                        "accessories": {"glasses": "none",
                                                        "headwear": hat,
                                                        "earrings": "none"}},
                                       30)
            iris = svg.find('fill="#3b2a20"')  # iris colour
            # iris is drawn after the back-hair afro circles; the front
            # afro band (also hair-coloured) must sit above the hairline,
            # never over the eyes
            first_hair = svg.find('fill="#191414"')
            self.assertGreater(iris, first_hair,
                               "features must be drawn above afro hair")
            self.assertNotEqual(iris, -1)

    # -- review finding (B3): babies get no accessories ---------------------
    def test_baby_has_no_accessories(self):
        svg = render_avatar_svg({**BASE, "accessories": {
            "glasses": "round", "headwear": "cap", "earrings": "hoops"}},
            1)
        self.assertNotIn("rgba(205,228,246", svg)  # no glasses lens
        self.assertNotIn("#2f6fed", svg)  # no cap blue
        self.assertNotIn('stroke="#d9b45b"', svg)  # no hoops

    def test_child_has_no_facial_hair(self):
        with_beard = render_avatar_svg({**BASE, "facial_hair": "beard_full",
                                        "hair_style": "bald",
                                        "hair_color": "brown_medium"}, 8)
        without = render_avatar_svg({**BASE, "facial_hair": "none",
                                     "hair_style": "bald",
                                     "hair_color": "brown_medium"}, 8)
        self.assertEqual(with_beard, without)

    def test_size_and_age_bounds(self):
        svg = render_avatar_svg(BASE, 10000, 99999)
        self.assertIn('width="512"', svg)


if __name__ == "__main__":
    unittest.main()
