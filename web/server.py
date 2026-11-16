#!/usr/bin/env python3
"""Local web server: M0 avatar tools (creator/gallery/demo) + M1 game API.

Stdlib only. Binds 127.0.0.1. Run via ./play-web.sh.

Game rules live in GameService: Kimi A's src/game/service.py when available,
otherwise the TEMPORARY web/stub_service.py (remove before acceptance).
"""

import html
import json
import os
import sys
import threading
import uuid

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer  # noqa: E402
from urllib.parse import urlparse, parse_qs, unquote  # noqa: E402

from src.appearance import render as avatar_render  # noqa: E402
from src.game.service import GameError  # noqa: E402
from src.game.service import GameService  # noqa: E402

SERVICE_SOURCE = "src.game.service"

STATIC = os.path.join(ROOT, "web", "static")
FIXTURES = os.path.join(ROOT, "tests", "fixtures", "appearances")
SAVE_DIR = os.path.join(ROOT, "game_saves", "web")

BODY_MAX = 65536  # bounded request bodies

AGES = [1, 8, 15, 30, 70]
AGE_LABELS = {1: "Baby", 8: "Child", 15: "Teen", 30: "Adult", 70: "Elder"}

PRESETS = [
    {"name": "Classic Alex", "appearance": {
        "skin_tone": "skin_04", "face_shape": "oval", "eye_shape": "almond",
        "eye_color": "brown_dark", "brows": "soft", "nose": "button",
        "mouth": "smile", "hair_texture": "straight",
        "hair_color": "brown_medium", "marks": [], "hair_style": "short_crop",
        "facial_hair": "none",
        "accessories": {"glasses": "none", "headwear": "none",
                        "earrings": "none"},
        "outfit_style": "tee", "outfit_color": "blue"}},
    {"name": "Curly Priya", "appearance": {
        "skin_tone": "skin_06", "face_shape": "heart", "eye_shape": "round",
        "eye_color": "brown_light", "brows": "arched", "nose": "upturned",
        "mouth": "full", "hair_texture": "curly", "hair_color": "black",
        "marks": ["beauty_mark"], "hair_style": "long_curly",
        "facial_hair": "none",
        "accessories": {"glasses": "cat_eye", "headwear": "none",
                        "earrings": "hoops"},
        "outfit_style": "dress", "outfit_color": "red"}},
    {"name": "Elder Mr. Okafor", "appearance": {
        "skin_tone": "skin_09", "face_shape": "round",
        "eye_shape": "monolid", "eye_color": "brown_dark", "brows": "thick",
        "nose": "wide", "mouth": "neutral", "hair_texture": "coily",
        "hair_color": "gray", "marks": ["mole"], "hair_style": "buzz",
        "facial_hair": "beard_short",
        "accessories": {"glasses": "round", "headwear": "none",
                        "earrings": "none"},
        "outfit_style": "suit", "outfit_color": "blue"}},
    {"name": "Sporty Sam", "appearance": {
        "skin_tone": "skin_03", "face_shape": "square", "eye_shape": "round",
        "eye_color": "blue", "brows": "straight", "nose": "straight",
        "mouth": "smile", "hair_texture": "straight",
        "hair_color": "blonde", "marks": ["freckles"],
        "hair_style": "undercut", "facial_hair": "none",
        "accessories": {"glasses": "none", "headwear": "cap",
                        "earrings": "none"},
        "outfit_style": "hoodie", "outfit_color": "green"}},
    {"name": "Boho Ines", "appearance": {
        "skin_tone": "skin_02", "face_shape": "long",
        "eye_shape": "downturned", "eye_color": "green", "brows": "soft",
        "nose": "pointed", "mouth": "smile", "hair_texture": "wavy",
        "hair_color": "auburn", "marks": ["freckles"],
        "hair_style": "long_wavy", "facial_hair": "none",
        "accessories": {"glasses": "none", "headwear": "sun_hat",
                        "earrings": "dangles"},
        "outfit_style": "sweater", "outfit_color": "purple"}},
    {"name": "Punk Ren", "appearance": {
        "skin_tone": "skin_05", "face_shape": "oval", "eye_shape": "upturned",
        "eye_color": "hazel", "brows": "thin", "nose": "button",
        "mouth": "smile", "hair_texture": "straight", "hair_color": "red",
        "marks": ["scar_brow"], "hair_style": "undercut", "facial_hair": "none",
        "accessories": {"glasses": "cat_eye", "headwear": "none",
                        "earrings": "studs"},
        "outfit_style": "tee", "outfit_color": "black"}},
]

DEFAULT_APPEARANCE = PRESETS[0]["appearance"]


def catalog():
    return avatar_render.get_catalog()


def _countries():
    try:
        from src.countries.countries import COUNTRIES  # type: ignore
        return sorted(COUNTRIES)
    except Exception:
        return ["Australia", "Brazil", "Canada", "China", "France", "Germany",
                "India", "Indonesia", "Japan", "Nigeria", "Spain", "UK", "USA"]


def render_svg(app_dict, age, size):
    return avatar_render.render_avatar_svg(app_dict, age, size)


def _bounded_int(value, lo, hi, name):
    """Coerce to int within [lo, hi]; deliberate 400 otherwise."""
    if isinstance(value, bool):
        raise GameError("invalid_input", "%s must be an integer" % name, 400)
    try:
        ivalue = int(value)
    except (TypeError, ValueError):
        raise GameError("invalid_input", "%s must be an integer" % name, 400)
    if ivalue != value and not (isinstance(value, str) and
                                 value.strip().lstrip("-").isdigit()):
        raise GameError("invalid_input", "%s must be an integer" % name, 400)
    if not lo <= ivalue <= hi:
        raise GameError("invalid_input",
                        "%s must be between %d and %d" % (name, lo, hi),
                        400)
    return ivalue


def _cell(svg, label=""):
    label_html = ('<figcaption>%s</figcaption>' % html.escape(label)
                  if label else "")
    return '<figure class="cell">%s%s</figure>' % (svg, label_html)


def grid_skins(size):
    return "".join(
        _cell(render_svg(dict(DEFAULT_APPEARANCE, skin_tone=e["id"]),
                         30, size), e["label"])
        for e in catalog().get("skin_tone", []))


def grid_ages(size):
    return "".join(_cell(render_svg(DEFAULT_APPEARANCE, age, size),
                         AGE_LABELS[age]) for age in AGES)


def grid_hair_hats(size):
    """Every catalog hair style x every headwear, from the real catalog."""
    out = []
    hats = ["none"] + [e["id"] for e in catalog().get("headwear", [])
                       if e["id"] != "none"]
    for h in [e["id"] for e in catalog().get("hair_style", [])]:
        for hat in hats:
            app = dict(DEFAULT_APPEARANCE, hair_style=h)
            app["accessories"] = {"glasses": "none", "headwear": hat,
                                  "earrings": "none"}
            out.append(_cell(render_svg(app, 30, size),
                             "%s + %s" % (h, hat)))
    return "".join(out)


def grid_feature(category, size):
    """Survey of one feature category (eye_shape, brows, nose, mouth,
    face_shape, hair_texture, facial_hair, outfit_style)."""
    out = []
    for e in catalog().get(category, []):
        app = dict(DEFAULT_APPEARANCE)
        if category == "facial_hair":
            app[category] = e["id"]
            out.append(_cell(render_svg(app, 40, size), e["label"]))
        elif category == "outfit_style":
            app[category] = e["id"]
            out.append(_cell(render_svg(app, 30, size), e["label"]))
        else:
            app[category] = e["id"]
            out.append(_cell(render_svg(app, 30, size), e["label"]))
    return "".join(out)


def grid_matrix(size):
    """Skin tone x age stage matrix (all 12 tones x 5 stages)."""
    out = []
    for e in catalog().get("skin_tone", []):
        for age in AGES:
            out.append(_cell(render_svg(dict(DEFAULT_APPEARANCE,
                                             skin_tone=e["id"]), age, size),
                             "%s · %s" % (e["label"], AGE_LABELS[age])))
    return "".join(out)


def grid_glasses_earrings(size):
    acc = catalog()
    out = []
    for gl in acc.get("glasses", []):
        for ea in acc.get("earrings", []):
            app = dict(DEFAULT_APPEARANCE)
            app["accessories"] = {"glasses": gl["id"], "headwear": "none",
                                  "earrings": ea["id"]}
            out.append(_cell(render_svg(app, 30, size),
                             "%s + %s" % (gl["label"], ea["label"])))
    return "".join(out)


def load_people():
    people = []
    if os.path.isdir(FIXTURES):
        for fn in sorted(os.listdir(FIXTURES)):
            if not fn.endswith(".json"):
                continue
            try:
                with open(os.path.join(FIXTURES, fn), encoding="utf-8") as f:
                    data = json.load(f)
            except Exception:
                continue
            data.setdefault("name", fn[:-5].replace("_", " ").title())
            people.append(data)
    return people


PAGES = {
    "/creator": "creator.html",
    "/gallery": "gallery.html",
    "/relationships": "relationships.html",
    "/game": "game.html",
}


class Handler(BaseHTTPRequestHandler):
    server_version = "LifeSimWeb/1.0"

    def log_message(self, *args):
        pass

    # -- helpers -----------------------------------------------------------
    def _origin_ok(self):
        """Mutating routes require same-origin loopback requests.

        Policy: Host must be exactly 127.0.0.1:<actual port>. If an Origin
        header is present it must be http://127.0.0.1:<actual port>. An
        absent Origin is accepted for non-browser/test clients (curl,
        unittest) since no browser sends none on same-origin POSTs; hostile
        or foreign origins are always rejected.
        """
        host = self.headers.get("Host", "")
        expected_host = "127.0.0.1:%d" % self.server.server_port
        if host != expected_host:
            return False
        origin = self.headers.get("Origin")
        if origin is None:
            return True
        return origin == "http://" + expected_host

    def _send(self, body, ctype="text/html; charset=utf-8", code=200):
        data = body.encode("utf-8") if isinstance(body, str) else body
        self.send_response(code)
        self.send_header("Content-Type", ctype)
        self.send_header("Content-Length", str(len(data)))
        self.send_header("Cache-Control", "no-store")
        self.end_headers()
        self.wfile.write(data)

    def _json(self, obj, code=200):
        self._send(json.dumps(obj), "application/json; charset=utf-8", code)

    def _error(self, code, message, status):
        self._json({"error": {"code": code, "message": message}}, status)

    def _read_json_body(self):
        raw_length = self.headers.get("Content-Length")
        if raw_length is None:
            raise GameError("invalid_input",
                            "Content-Length header is required", 400)
        try:
            length = int(raw_length)
        except ValueError:
            raise GameError("invalid_input",
                            "Content-Length must be a non-negative integer",
                            400)
        if not 0 <= length <= BODY_MAX:
            raise GameError("invalid_input",
                            "Content-Length must be between 0 and %d"
                            % BODY_MAX, 400)
        ctype = self.headers.get("Content-Type", "")
        media_type = ctype.split(";", 1)[0].strip().lower()
        if media_type != "application/json":
            raise GameError("invalid_input",
                            "Content-Type must be application/json", 400)
        raw = self.rfile.read(length)
        try:
            data = json.loads(raw or b"{}")
        except ValueError:
            raise GameError("invalid_input", "malformed JSON body", 400)
        if not isinstance(data, dict):
            raise GameError("invalid_input", "body must be a JSON object",
                            400)
        return data

    # -- routing -----------------------------------------------------------
    def do_GET(self):
        parsed = urlparse(self.path)
        path = parsed.path
        qs = parse_qs(parsed.query)
        if path == "/":
            self.send_response(302)
            self.send_header("Location", "/game")
            self.end_headers()
            return
        if path in PAGES:
            with open(os.path.join(STATIC, PAGES[path]), "rb") as f:
                self._send(f.read())
            return
        if path.startswith("/static/"):
            fn = os.path.normpath(unquote(path[len("/static/"):]))
            full = os.path.join(STATIC, fn)
            real = os.path.realpath(full)
            if (os.path.commonpath([real, os.path.realpath(STATIC)])
                    != os.path.realpath(STATIC)
                    or os.path.islink(full) or not os.path.isfile(real)):
                self._send("not found", code=404)
                return
            full = real
            ctype = "text/css" if full.endswith(".css") else \
                "application/javascript" if full.endswith(".js") else \
                "image/svg+xml" if full.endswith(".svg") else \
                "application/octet-stream"
            with open(full, "rb") as f:
                self._send(f.read(), ctype)
            return
        if path == "/api/service-info":
            self._json({"service": SERVICE_SOURCE})
            return
        if path == "/api/catalog":
            self._json({"catalog": catalog(), "presets": PRESETS,
                        "countries": _countries(),
                        "age_stages": AGE_LABELS})
            return
        if path == "/api/people":
            self._json(load_people())
            return
        if path.startswith("/api/grid/"):
            try:
                size = _bounded_int(qs.get("size", ["128"])[0], 8, 512,
                                    "size")
            except GameError as exc:
                self._error(exc.code, exc.message, exc.status)
                return
            grids = {"skins": grid_skins, "ages": grid_ages,
                     "hair-hats": grid_hair_hats,
                     "glasses-earrings": grid_glasses_earrings,
                     "matrix": grid_matrix,
                     "eye-shapes": lambda n: grid_feature("eye_shape", n),
                     "brows": lambda n: grid_feature("brows", n),
                     "noses": lambda n: grid_feature("nose", n),
                     "mouths": lambda n: grid_feature("mouth", n),
                     "face-shapes": lambda n: grid_feature("face_shape", n),
                     "textures": lambda n: grid_feature("hair_texture", n),
                     "outfits": lambda n: grid_feature("outfit_style", n),
                     "facial-hair": lambda n: grid_feature("facial_hair", n)}
            fn = grids.get(path[len("/api/grid/"):])
            if not fn:
                self._send("unknown grid", code=404)
                return
            self._send(fn(size))
            return
        if path == "/api/games":
            try:
                self._json(_enriched_list_payload(self.server.service))
            except GameError as exc:
                self._error(exc.code, exc.message, exc.status)
            except Exception:
                self._error("internal", "unexpected server error", 500)
            return
        if path.startswith("/api/games/"):
            game_id = path[len("/api/games/"):]
            if "/" in game_id or not game_id:
                self._error("not_found", "unknown route", 404)
                return
            try:
                self._json(self.server.service.get_state(game_id))
            except GameError as exc:
                self._error(exc.code, exc.message, exc.status)
            except Exception:
                self._error("internal", "unexpected server error", 500)
            return
        self._send("not found", code=404)

    def do_POST(self):
        parsed = urlparse(self.path)
        path = parsed.path
        if not self._origin_ok():
            self._error("forbidden", "origin or host not allowed", 403)
            return
        try:
            if path == "/api/render":
                payload = self._read_json_body()
                app_dict = payload.get("appearance")
                if app_dict is None:
                    app_dict = {}
                if not isinstance(app_dict, dict):
                    raise GameError("invalid_input",
                                    "appearance must be an object", 400)
                age = _bounded_int(payload.get("age", 30), 0, 120, "age")
                size = _bounded_int(payload.get("size", 128), 8, 512, "size")
                self._send(render_svg(app_dict, age, size), "image/svg+xml")
                return
            if path == "/api/games":
                payload = self._read_json_body()
                profile = {k: payload.get(k) for k in (
                    "first_name", "last_name", "gender", "country",
                    "appearance", "seed")}
                request_id = self._require_request_id(payload)
                state = self.server.service.new_game(profile, request_id)
                self._json(state, 201)
                return
            if path.startswith("/api/games/") and path.endswith("/actions"):
                game_id = path[len("/api/games/"):-len("/actions")]
                payload = self._read_json_body()
                request_id = self._require_request_id(payload)
                expected = payload.get("expected_revision")
                if not isinstance(expected, int):
                    raise GameError("invalid_input",
                                    "expected_revision (int) is required",
                                    400)
                action = payload.get("action")
                if not isinstance(action, str) or not action:
                    raise GameError("invalid_input", "action is required",
                                    400)
                body = payload.get("payload")
                if body is None:
                    body = {}
                if not isinstance(body, dict):
                    raise GameError("invalid_input",
                                    "payload must be an object", 400)
                state = self.server.service.command(
                    game_id, expected, request_id, action, body)
                self._json(state)
                return
            self._send("not found", code=404)
        except GameError as exc:
            self._error(exc.code, exc.message, exc.status)
        except (ValueError, TypeError):
            self._error("invalid_input", "invalid field types", 400)
        except Exception:
            # no traceback/path exposure
            self._error("internal", "unexpected server error", 500)

    def _require_request_id(self, payload):
        request_id = payload.get("request_id")
        if not isinstance(request_id, str) or not request_id.strip():
            raise GameError("invalid_input", "request_id is required", 400)
        if len(request_id) > 128:
            raise GameError("invalid_input", "request_id too long", 400)
        return request_id



def _enriched_list_payload(service):
    """list_games() plus a minimal player descriptor per game so the UI can
    render portraits on resume rows without name parsing."""
    listing = service.list_games()
    for g in listing.get("games", []):
        try:
            st = service.get_state(g["game_id"])
        except Exception:
            continue
        player = next((p for p in st.get("people", [])
                       if p.get("id") == st.get("player_id")), None)
        if player:
            g["player"] = {"name": player.get("name"),
                           "age": player.get("age"),
                           "appearance": player.get("appearance"),
                           "alive": player.get("alive")}
    return listing


def make_server(port=8765, save_dir=SAVE_DIR):
    server = ThreadingHTTPServer(("127.0.0.1", port), Handler)
    server.service = GameService(save_dir)
    return server


def main(port=8765):
    server = make_server(port)
    print("Life Simulator web UI (service: %s)" % SERVICE_SOURCE)
    print("  Game:         http://127.0.0.1:%d/game" % port)
    print("  Creator:      http://127.0.0.1:%d/creator" % port)
    print("  Gallery:      http://127.0.0.1:%d/gallery" % port)
    print("  Demo people:  http://127.0.0.1:%d/relationships" % port)
    print("Press Ctrl+C to stop.")
    try:
        server.serve_forever()
    except KeyboardInterrupt:
        pass
    finally:
        server.server_close()


if __name__ == "__main__":
    main(int(sys.argv[1]) if len(sys.argv) > 1 else 8765)
