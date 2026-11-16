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
from src.appearance.render import avatar_image_key  # noqa: E402,F401
from src.game.service import GameError  # noqa: E402
from src.game.service import GameService  # noqa: E402

SERVICE_SOURCE = "src.game.service"

STATIC = os.path.join(ROOT, "web", "static")
AVATAR_STATIC = os.path.join(STATIC, "avatars")
FIXTURES = os.path.join(ROOT, "tests", "fixtures", "appearances")
SAVE_DIR = os.path.join(ROOT, "game_saves", "web")

BODY_MAX = 65536  # bounded request bodies

AGES = [1, 8, 15, 30, 70]
AGE_LABELS = {1: "Baby", 8: "Child", 15: "Teen", 30: "Adult", 70: "Elder"}

PRESETS = [
    {"name": "Classic Alex", "appearance": {
        "schema_version": 2, "seed": 101, "base": "person",
        "skin_tone": "medium-light", "hair": "default",
        "facial_hair": "none", "headwear": "none", "badges": [],
        "background": "bg_05", "look_override": "none"}},
    {"name": "Curly Priya", "appearance": {
        "schema_version": 2, "seed": 102, "base": "woman",
        "skin_tone": "medium", "hair": "curly",
        "facial_hair": "none", "headwear": "none",
        "badges": ["earrings"], "background": "bg_04",
        "look_override": "none"}},
    {"name": "Elder Mr. Okafor", "appearance": {
        "schema_version": 2, "seed": 103, "base": "man",
        "skin_tone": "dark", "hair": "white",
        "facial_hair": "beard", "headwear": "none", "badges": ["glasses"],
        "background": "bg_09", "look_override": "none"}},
    {"name": "Sporty Sam", "appearance": {
        "schema_version": 2, "seed": 104, "base": "man",
        "skin_tone": "light", "hair": "red",
        "facial_hair": "none", "headwear": "none", "badges": ["cap"],
        "background": "bg_03", "look_override": "none"}},
    {"name": "Boho Ines", "appearance": {
        "schema_version": 2, "seed": 105, "base": "woman",
        "skin_tone": "light", "hair": "default",
        "facial_hair": "none", "headwear": "headscarf",
        "badges": ["flower"], "background": "bg_08",
        "look_override": "none"}},
    {"name": "Grad Robin", "appearance": {
        "schema_version": 2, "seed": 106, "base": "person",
        "skin_tone": "medium-dark", "hair": "default",
        "facial_hair": "none", "headwear": "none", "badges": ["crown"],
        "background": "bg_02", "look_override": "student"}},
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


def _img_cell(app, age, label=""):
    import base64
    raw = json.dumps(app, separators=(",", ":")).encode()
    b64 = base64.urlsafe_b64encode(raw).decode()
    label_html = ("<figcaption>%s</figcaption>" % html.escape(label)
                  if label else "")
    return ('<figure class="cell"><img loading="lazy" '
            'src="/api/avatar.png?app=%s&amp;age=%d" alt="">%s</figure>'
            % (b64, age, label_html))


V2_BASE = {"schema_version": 2, "seed": 7, "base": "person",
           "skin_tone": "default", "hair": "default",
           "facial_hair": "none", "headwear": "none", "badges": [],
           "background": "bg_01", "look_override": "none"}


def _v2(**over):
    app = dict(V2_BASE)
    app.update(over)
    return app


def grid_skins(size):
    return "".join(_img_cell(_v2(skin_tone=e["id"]), 30, e["label"])
                   for e in catalog().get("skin_tone", []))


def grid_ages(size):
    return "".join(_img_cell(V2_BASE, age, AGE_LABELS[age]) for age in AGES)


def grid_matrix(size):
    out = []
    for e in catalog().get("skin_tone", []):
        for age in AGES:
            out.append(_img_cell(_v2(skin_tone=e["id"]), age,
                                 "%s · %s" % (e["label"], AGE_LABELS[age])))
    return "".join(out)


def grid_hair(size):
    out = []
    for e in catalog().get("hair", []):
        out.append(_img_cell(_v2(hair=e["id"]), 30, e["label"]))
        out.append(_img_cell(_v2(base="woman", hair=e["id"]), 30,
                             "woman · %s" % e["label"]))
        out.append(_img_cell(_v2(base="man", hair=e["id"]), 30,
                             "man · %s" % e["label"]))
    for e in catalog().get("facial_hair", []):
        if e["id"] != "none":
            out.append(_img_cell(_v2(base="man", facial_hair=e["id"]), 45,
                                 e["label"]))
    for e in catalog().get("headwear", []):
        if e["id"] != "none":
            out.append(_img_cell(_v2(base="woman", headwear=e["id"]), 40,
                                 e["label"]))
    return "".join(out)


def grid_extras(size):
    out = []
    for e in catalog().get("badges", []):
        out.append(_img_cell(_v2(badges=[e["id"]]), 35, e["label"]))
    for e in catalog().get("background", []):
        out.append(_img_cell(_v2(background=e["id"]), 30, e["label"]))
    return "".join(out)


def grid_looks(size):
    looks = sorted({k.split(":")[1] for k in _avatar_combos()
                    if k.startswith("look:")})
    out = []
    for look in looks:
        out.append(_img_cell(_v2(look_override=look), 40, look))
    return "".join(out)


def _avatar_combos():
    try:
        import json as _json
        with open(avatar_render.MANIFEST_PATH, encoding="utf-8") as f:
            return _json.load(f).get("combos", {})
    except Exception:
        return {}


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

    def _send(self, body, ctype="text/html; charset=utf-8", code=200,
              cache="no-store"):
        data = body.encode("utf-8") if isinstance(body, str) else body
        self.send_response(code)
        self.send_header("Content-Type", ctype)
        self.send_header("Content-Length", str(len(data)))
        self.send_header("Cache-Control", cache)
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
        if path.startswith("/static/avatars/"):
            fn = os.path.normpath(unquote(path[len("/static/avatars/"):]))
            full = os.path.join(AVATAR_STATIC, fn)
            real = os.path.realpath(full)
            if (os.path.commonpath([real, os.path.realpath(AVATAR_STATIC)])
                    != os.path.realpath(AVATAR_STATIC)
                    or os.path.islink(full) or not os.path.isfile(real)
                    or not real.endswith(".png")):
                self._send("not found", code=404)
                return
            with open(real, "rb") as f:
                self._send(f.read(), "image/png")
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
        if path.startswith("/api/avatar.png"):
            # stateless GET variant for <img> cells: ?app=<base64url json>
            # &age=<int>; read-only, no game state touched.
            import base64
            raw = qs.get("app", [""])[0]
            try:
                # tolerate stripped padding (client omits it)
                pad = "=" * (-len(raw) % 4)
                app_dict = json.loads(base64.urlsafe_b64decode(
                    (raw + pad).encode()).decode())
            except Exception:
                self._error("invalid_input", "malformed app parameter", 400)
                return
            if not isinstance(app_dict, dict):
                self._error("invalid_input", "malformed app parameter", 400)
                return
            try:
                age = _bounded_int(qs.get("age", ["30"])[0], 0, 120, "age")
                key = avatar_render.avatar_image_key(app_dict, age)
            except GameError as exc:
                self._error(exc.code, exc.message, exc.status)
                return
            if not key["found"]:
                self._error("internal_error",
                            "avatar asset missing for this look", 500)
                return
            with open(os.path.join(avatar_render.AVATAR_DIR, key["file"]),
                      "rb") as f:
                # immutable per (appearance, age): cache to avoid the
                # intermittent refetch flake on the creator
                self._send(f.read(), "image/png",
                           cache="public, max-age=3600")
            return
        if path == "/api/service-info":
            self._json({"service": SERVICE_SOURCE,
                        "dev": bool(getattr(self.server, "dev_mode", False))})
            return
        if path == "/api/scenarios":
            # dev-only scenario starts; hidden in normal play
            if not getattr(self.server, "dev_mode", False):
                self._error("not_found", "unknown route", 404)
                return
            self._json(_scenarios())
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
                     "matrix": grid_matrix, "hair": grid_hair,
                     "extras": grid_extras, "looks": grid_looks}
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
                self._error("internal_error", "unexpected server error", 500)
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
                self._error("internal_error", "unexpected server error", 500)
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
                # M0b: resolves the Fluent 3D avatar asset and streams the
                # PNG. `size` is accepted for client compatibility; assets
                # are served at their fetched resolution.
                payload = self._read_json_body()
                app_dict = payload.get("appearance")
                if app_dict is None:
                    app_dict = {}
                if not isinstance(app_dict, dict):
                    raise GameError("invalid_input",
                                    "appearance must be an object", 400)
                age = _bounded_int(payload.get("age", 30), 0, 120, "age")
                _bounded_int(payload.get("size", 128), 8, 512, "size")
                key = avatar_render.avatar_image_key(app_dict, age)
                if not key["found"]:
                    raise GameError("internal_error",
                                    "avatar asset missing for this look",
                                    500)
                with open(os.path.join(avatar_render.AVATAR_DIR,
                                       key["file"]), "rb") as f:
                    self._send(f.read(), "image/png")
                return
            if path == "/api/games":
                payload = self._read_json_body()
                profile = {k: payload.get(k) for k in (
                    "first_name", "last_name", "gender", "country",
                    "appearance", "seed", "scenario")}
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
        except Exception:
            # All deliberate validation raises GameError; anything else is
            # a server fault — no traceback/path exposure.
            self._error("internal_error", "unexpected server error", 500)

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


# The frozen scenario set lives in docs/collab/M2/api-examples.json
# (scenario_start_example) — the contract is the single source of truth for
# what the dev panel may offer. Server-side extras are not advertised.
_EXAMPLES_PATH = os.path.join(ROOT, "docs", "collab", "M2",
                              "api-examples.json")


def _frozen_scenarios():
    try:
        with open(_EXAMPLES_PATH, encoding="utf-8") as f:
            ex = json.load(f)
        return [(s["id"], s.get("label", s["id"]))
                for s in ex["scenario_start_example"]["scenarios"]]
    except Exception:
        return []


def _scenarios():
    try:
        from src.game.scenarios import SCENARIOS
        return {"scenarios": [{"id": k, "label": label}
                              for k, label in _frozen_scenarios()
                              if k in SCENARIOS]}
    except Exception:
        return {"scenarios": []}


def main(port=8765, dev=False):
    server = make_server(port)
    server.dev_mode = dev
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
    args = [a for a in sys.argv[1:] if a != "--dev"]
    main(*(int(args[0]),) if args else (),
         **{"dev": "--dev" in sys.argv})
