"""Application boundary for the web slice.

GameService owns the simulation for the browser client: real creation from
the M0 creator profile, parental interaction, yearly effects, contextual
decisions, death, journaling, revisions, durable replay and atomic saves.

Rules slice extracted from the legacy engine (src/people, src/menus):
- Player/Person construction and appearance inheritance (M0).
- Person.age_up stat drift, teen looks and post-50 looks decay.
- Parent relationship drift per year (+1 under 18, else -1/-1/0).
- Person.death_check for the player and relations.
- The "Spend time" enjoyment formulas from src/menus/main.py.
Deferred to M2: school, careers, romance, pregnancy/children, money income,
traits UI, illnesses, random_events content. Player.random_events and
Player.age_up's interactive parts are NOT called; yearly effects are
re-implemented here on top of Person.age_up.

Legacy helpers consume the global `random`; the service uses a strictly
locked, save/restore-in-finally adapter so each game has its own persisted
RNG state and games never interleave.
"""

import hashlib
import json
import os
import pickle
import random
import re
import tempfile
import threading
from random import randint

from src.appearance.model import Appearance
from src.appearance import catalog
from src.lifesim_lib.lifesim_lib import Gender, clamp, round_stochastic
from src.lifesim_lib.safe_save import load_save
from src.people.classes.child import Child
from src.people.classes.parent import Parent
from src.people.classes.partner import Partner
from src.people.classes.person import Person
from src.people.classes.player import Player
from src.people.classes.relationship import Relationship
from src.people.classes.sibling import Sibling
from src.game import decisions

ENVELOPE_SCHEMA_VERSION = 2
REPLAY_LIMIT = 100
_CREATION_LEDGER = "_creations.json"
_ID_RE = re.compile(r"^[A-Za-z0-9_-]{1,64}$")

# Process-global compatibility resources: the legacy engine consumes the
# global `random` module and the appearance integration module has its own
# cosmetic RNG object. Both are swapped per command, so the locks and the
# spare cosmetic RNG instance are shared across ALL GameService instances.
_COMPAT_LOCK = threading.Lock()
_COSMETIC_RNG = random.Random()

# Age-appropriate "spend time" narration (M1 subset of SPEND_TIME_PLACES).
SPEND_PLACES = {
    "baby": ["at home", "in the nursery"],
    "child": ["at the park", "at the playground", "building a blanket fort"],
    "teen": ["at the mall", "at a café", "at the cinema"],
    "adult": ["over a quiet dinner", "on a long walk"],
    "elder": ["over tea and old stories", "in the garden"],
}

ROLE_NAMES = {
    "player": "player",
    "mother": "mother",
    "father": "father",
    "sibling": "sibling",
    "partner": "partner",
    "child": "child",
}


class GameError(Exception):
    def __init__(self, code, message, status):
        super().__init__(message)
        self.code = code
        self.message = message
        self.status = status

    def as_dict(self):
        return {"error": {"code": self.code, "message": self.message}}


def _invalid(message):
    return GameError("invalid_input", message, 400)


def _not_found(message):
    return GameError("not_found", message, 404)


def _conflict(code, message):
    return GameError(code, message, 409)


class _GameRNG:
    """Per-game RNG adapter for the legacy global-random engine.

    Acquires the process-global compatibility lock, restores the caller's
    global state in finally, persists the game's simulation stream, and
    drives the cosmetic RNG from the game's own persisted cosmetic stream
    (never consuming simulation randomness after creation).
    """

    def __init__(self, service, env):
        self._service = service
        self._env = env

    def __enter__(self):
        _COMPAT_LOCK.acquire()
        self._saved = random.getstate()
        random.setstate(self._env["rng_state"])
        import src.appearance.integration as integration
        self._integration = integration
        self._saved_app_rng = integration._APP_RNG
        self._saved_cosmetic = _COSMETIC_RNG.getstate()
        state = self._env.get("cosmetic_rng_state")
        if state is None:
            # one-time initialization for games created before this stream
            # existed; afterwards the stream is persisted per game
            _COSMETIC_RNG.seed(random.getrandbits(64))
            self._env["cosmetic_rng_state"] = _COSMETIC_RNG.getstate()
        else:
            _COSMETIC_RNG.setstate(state)
        integration._APP_RNG = _COSMETIC_RNG
        return self

    def __exit__(self, exc_type, exc, tb):
        if exc_type is None:
            self._env["rng_state"] = random.getstate()
            self._env["cosmetic_rng_state"] = _COSMETIC_RNG.getstate()
        _COSMETIC_RNG.setstate(self._saved_cosmetic)
        self._integration._APP_RNG = self._saved_app_rng
        random.setstate(self._saved)
        _COMPAT_LOCK.release()
        return False


class GameService:
    """Owns web games. All methods return plain JSON-compatible dicts."""

    def __init__(self, save_dir, creation_replay_limit=500):
        if (not isinstance(creation_replay_limit, int)
                or isinstance(creation_replay_limit, bool)
                or creation_replay_limit < 0):
            raise ValueError("creation_replay_limit must be a non-negative integer")
        self.save_dir = os.path.abspath(save_dir)
        os.makedirs(self.save_dir, exist_ok=True)
        self._creation_replay_limit = creation_replay_limit
        self._lock = threading.RLock()

    # ------------------------------------------------------------- paths

    def _path(self, game_id):
        if not isinstance(game_id, str) or not _ID_RE.match(game_id):
            raise _invalid("invalid game id")
        path = os.path.abspath(os.path.join(self.save_dir, game_id + ".pickle"))
        if os.path.dirname(path) != self.save_dir:
            raise _invalid("invalid game id")
        return path

    def _migrate_envelope(self, env, version):
        """Versioned envelope migrations."""
        if version == 1:
            env = self._migrate_v1_web_ids(env)
        return env

    def _migrate_v1_web_ids(self, env):
        """v1 -> v2: public identity moved from sequential web_id (p_XX) to
        immutable canonical Person.person_id (UUID). Remaps every
        reference-bearing field, including replay/creation response
        snapshots, then rejects dangling references explicitly."""
        from src.appearance.integration import ensure_person_id
        try:
            people = list(self._walk_people(env["player"]))
            for person in env.get("archive", []):
                if isinstance(person, Person) and person not in people:
                    people.append(person)
            mapping = {}
            for person in people:
                old = getattr(person, "web_id", None)
                ensure_person_id(person)
                if old:
                    mapping[old] = person.person_id

            def remap(value):
                return mapping.get(value, value)

            for entry in env["journal"]:
                entry["person_ids"] = [remap(i)
                                       for i in entry["person_ids"]]
            env["dead_ids"] = [remap(i) for i in env.get("dead_ids", [])]
            env["year_actions"]["spend_time"] = [
                remap(i) for i in env["year_actions"].get("spend_time", [])]
            pending = env.get("pending")
            if pending is not None:
                pending["person_ids"] = [remap(i)
                                         for i in pending["person_ids"]]
            for state_like in [env.get("creation", {}).get("response")] + [
                    e.get("response") for e in env.get("replay", [])]:
                if not isinstance(state_like, dict):
                    continue
                state_like["player_id"] = remap(state_like["player_id"])
                for person in state_like.get("people", []):
                    person["id"] = remap(person["id"])
                for entry in state_like.get("journal", []):
                    entry["person_ids"] = [remap(i)
                                           for i in entry["person_ids"]]
                for action in state_like.get("available_actions", []):
                    if "target_id" in action:
                        action["target_id"] = remap(action["target_id"])
                pending_like = state_like.get("pending_decision")
                if isinstance(pending_like, dict) \
                        and isinstance(pending_like.get("person_ids"), list):
                    pending_like["person_ids"] = [
                        remap(i) for i in pending_like["person_ids"]]
            # dangling references are corrupt data, not silent remaps
            known = {person.person_id for person in people}
            for entry in env["journal"]:
                for pid in entry["person_ids"]:
                    if pid not in known:
                        raise GameError(
                            "internal_error", "save data is invalid", 500)
            env["schema_version"] = 2
            return env
        except (KeyError, TypeError, AttributeError) as e:
            raise GameError(
                "internal_error", "save data is invalid", 500) from e

    def _load(self, game_id):
        path = self._path(game_id)
        if not os.path.isfile(path) or os.path.islink(path):
            raise _not_found("unknown game")
        try:
            with open(path, "rb") as f:
                env = load_save(f)
        except GameError:
            raise
        except Exception as e:
            raise GameError("internal_error", "save data is corrupt", 500) from e
        if not isinstance(env, dict):
            raise GameError("internal_error", "save data is corrupt", 500)
        version = env.get("schema_version")
        if not isinstance(version, int) or isinstance(version, bool):
            raise GameError("internal_error", "save data is corrupt", 500)
        if version > ENVELOPE_SCHEMA_VERSION:
            raise _conflict(
                "unsupported_schema",
                f"unsupported save schema version: {version!r}")
        dirty = version < ENVELOPE_SCHEMA_VERSION
        if dirty:
            env = self._migrate_envelope(env, version)
        self._validate_envelope(env)
        # Backfill person identity/appearances before any projection;
        # persist once so repeated read-only loads are stable.
        from src.appearance.integration import ensure_appearance, ensure_person_id
        people = self._walk_people(env["player"])
        for person in env.get("archive", []):
            if isinstance(person, Person) and person not in people:
                people.append(person)
        for person in people:
            if not getattr(person, "person_id", None):
                dirty = True
            ensure_person_id(person)
            if not isinstance(getattr(person, "appearance", None),
                              Appearance):
                ensure_appearance(person)
                dirty = True
        if dirty:
            self._save(env)
        return env

    @staticmethod
    def _validate_envelope(env):
        """Validate every persisted record the service consumes. Required
        fields are indexed directly (no .get-with-default then mandatory
        indexing later); documented optional fields are normalized to real
        defaults here."""
        required = {
            "game_id": str, "revision": int, "status": str,
            "player": Player, "journal": list, "rng_state": tuple,
            "next_journal_num": int, "next_decision_num": int,
            "country": str, "creation": dict, "pending": object,
            "year_actions": dict, "used_decisions": list,
            "dead_ids": list, "archive": list, "replay": list,
        }
        try:
            for key, typ in required.items():
                if key not in env or not isinstance(env[key], typ) \
                        or (typ is not object and isinstance(env[key], bool)):
                    raise TypeError(key)
            if env["revision"] < 0 or env["status"] not in ("alive", "dead"):
                raise TypeError("revision/status")
            creation = env["creation"]
            for key in ("request_id", "fingerprint", "response"):
                if not isinstance(creation.get(key), (str, dict)):
                    raise TypeError("creation." + key)

            player = env["player"]
            for attr, typ in (("firstname", str), ("lastname", str),
                              ("age", int), ("happiness", int),
                              ("health", int), ("smarts", int),
                              ("looks", int), ("money", int),
                              ("relations", list), ("children", list)):
                if not isinstance(getattr(player, attr), typ) \
                        or isinstance(getattr(player, attr), bool):
                    raise TypeError(f"player.{attr}")
            if player.age < 0:
                raise TypeError("player.age")

            if not isinstance(env["year_actions"].get("spend_time"), list):
                raise TypeError("year_actions.spend_time")

            pending = env["pending"]
            if pending is not None:
                if not isinstance(pending, dict):
                    raise TypeError("pending")
                for key, typ in (("id", str), ("title", str),
                                 ("text", str), ("person_ids", list),
                                 ("options", list)):
                    if not isinstance(pending.get(key), typ):
                        raise TypeError("pending." + key)
                for option in pending["options"]:
                    if not isinstance(option, dict):
                        raise TypeError("pending option")
                    for key, typ in (("id", str), ("label", str),
                                     ("known_effects", list),
                                     ("_effects", dict), ("_outcome", str)):
                        if not isinstance(option.get(key), typ):
                            raise TypeError("pending option." + key)

            for entry in env["journal"]:
                if not isinstance(entry, dict):
                    raise TypeError("journal entry")
                for key, typ in (("id", str), ("age", int), ("kind", str),
                                 ("text", str), ("person_ids", list),
                                 ("changes", dict)):
                    if not isinstance(entry.get(key), typ):
                        raise TypeError("journal entry." + key)

            for entry in env["replay"]:
                if not isinstance(entry, dict):
                    raise TypeError("replay entry")
                for key, typ in (("request_id", str), ("action", str),
                                 ("payload", dict), ("response", dict)):
                    if not isinstance(entry.get(key), typ):
                        raise TypeError("replay entry." + key)

            # documented optional fields introduced after v1: normalize
            env.setdefault("ledger", [])
            env.setdefault("balances", {})
            env.setdefault("ledger_next_id", 1)
            env.setdefault("generation", 1)
            env.setdefault("completed_lives", [])
            if not isinstance(env["ledger"], list) \
                    or not isinstance(env["balances"], dict):
                raise TypeError("ledger fields")

            # validate RNG states locally BEFORE any global swap happens
            probe = random.Random()
            probe.setstate(env["rng_state"])
            cosmetic = env.get("cosmetic_rng_state")
            if cosmetic is not None:
                probe.setstate(cosmetic)
        except (KeyError, TypeError, ValueError, AttributeError) as e:
            raise GameError(
                "internal_error", "save data is invalid", 500) from e

    def _save(self, env):
        """Atomic replace; raises on failure so callers roll back."""
        path = self._path(env["game_id"])
        try:
            fd, tmp = tempfile.mkstemp(dir=self.save_dir, suffix=".tmp")
            with os.fdopen(fd, "wb") as f:
                pickle.dump(env, f)
            os.replace(tmp, path)
        except Exception as e:
            try:
                os.unlink(tmp)
            except (OSError, UnboundLocalError):
                pass
            raise GameError(
                "internal_error",
                "the operation failed; no changes were applied", 500) from e

    def _load_creations(self):
        """Creation replay index. Corrupt metadata fails closed."""
        path = os.path.join(self.save_dir, _CREATION_LEDGER)
        if not os.path.isfile(path) or os.path.islink(path):
            return {}
        try:
            with open(path, encoding="utf-8") as f:
                data = json.load(f)
            if not isinstance(data, dict) or any(
                    not isinstance(v, dict)
                    or not isinstance(v.get("fingerprint"), str)
                    for v in data.values()):
                raise ValueError("bad ledger shape")
            return data
        except (ValueError, OSError) as e:
            raise GameError(
                "internal_error",
                "creation replay metadata is corrupt", 500) from e

    def _save_creations(self, ledger):
        path = os.path.join(self.save_dir, _CREATION_LEDGER)
        try:
            fd, tmp = tempfile.mkstemp(dir=self.save_dir, suffix=".tmp")
            with os.fdopen(fd, "w", encoding="utf-8") as f:
                json.dump(ledger, f)
            os.replace(tmp, path)
        except Exception as e:
            try:
                os.unlink(tmp)
            except (OSError, UnboundLocalError):
                pass
            raise GameError(
                "internal_error",
                "the operation failed; no changes were applied", 500) from e

    def _scan_creation(self, request_id):
        """Crash-durable fallback: find a creation request id recorded in a
        game envelope (used when the index write did not survive)."""
        for name in sorted(os.listdir(self.save_dir)):
            if not name.endswith(".pickle") or name.startswith("_"):
                continue
            path = os.path.join(self.save_dir, name)
            if not os.path.isfile(path) or os.path.islink(path):
                continue
            try:
                with open(path, "rb") as f:
                    env = load_save(f)
            except Exception:
                continue
            if not isinstance(env, dict):
                continue
            creation = env.get("creation")
            if isinstance(creation, dict) \
                    and creation.get("request_id") == request_id:
                return creation
        return None

    # ------------------------------------------------------------- people

    @staticmethod
    def _walk_people(player):
        """All reachable living Person objects (cycle-safe)."""
        seen = set()
        people = []

        def visit(person):
            if id(person) in seen or not isinstance(person, Person):
                return
            seen.add(id(person))
            people.append(person)
            relations = getattr(person, "relations", None)
            if relations:
                for r in relations:
                    visit(r)
            for attr in ("mother", "father", "partner"):
                visit(getattr(person, attr, None))

        visit(player)
        return people

    def _ensure_ids(self, env):
        """Canonical identity mapping: the immutable persisted Person.person_id
        is the public API id (backfilled for legacy people)."""
        from src.appearance.integration import ensure_person_id
        by_id = {}
        for person in self._walk_people(env["player"]):
            by_id[ensure_person_id(person)] = person
        return by_id

    def _make_resolver(self, env):
        def resolve(owner_id):
            for person in self._walk_people(env["player"]):
                if person.person_id == owner_id:
                    return person
            for person in env.get("archive", []):
                if getattr(person, "person_id", None) == owner_id:
                    return person
            return None
        return resolve

    def _find_person(self, env, person_id):
        by_id = self._ensure_ids(env)
        person = by_id.get(person_id)
        if person is None or person_id in env.get("dead_ids", []):
            raise _not_found("unknown target")
        return person

    @staticmethod
    def _role(person):
        if isinstance(person, Player):
            return "player"
        if isinstance(person, Parent):
            return "mother" if person.gender == Gender.Female else "father"
        for cls, name in ((Sibling, "sibling"), (Partner, "partner"),
                          (Child, "child")):
            if isinstance(person, cls):
                return name
        return "relative"

    # ------------------------------------------------------------- state

    def _journal(self, env, kind, text, person_ids=(), changes=None):
        seq = env["next_journal_num"]
        env["next_journal_num"] += 1
        entry = {
            "id": f"j_{seq}",
            "age": env["player"].age,
            "kind": kind,
            "text": text,
            "person_ids": list(person_ids),
            "changes": changes or {},
        }
        env["journal"].append(entry)
        return entry

    def _state(self, env):
        player = env["player"]
        people = []
        seen_ids = set()

        def add_person(person):
            if person.person_id in seen_ids:
                return
            seen_ids.add(person.person_id)
            item = {
                "id": person.person_id,
                "name": person.name,
                "role": self._role(person),
                "age": person.age,
                "alive": person.person_id not in env.get("dead_ids", []),
            }
            if isinstance(person, Relationship):
                item["relationship"] = person.relationship
            appearance = getattr(person, "appearance", None)
            item["appearance"] = (
                appearance.to_dict() if isinstance(appearance, Appearance)
                else None)
            people.append(item)

        for person in self._walk_people(player):
            add_person(person)
        for person in env.get("archive", []):
            if isinstance(person, Person):
                add_person(person)

        actions = []
        pending = env.get("pending")
        if env["status"] == "alive" and not pending:
            actions.append({"id": "age_up", "label": "Age up"})
            spent = env["year_actions"].get("spend_time", [])
            for person in self._walk_people(player):
                if isinstance(person, Parent) and person.person_id not in spent:
                    actions.append({
                        "id": "spend_time",
                        "label": f"Spend time with {person.firstname}",
                        "target_id": person.person_id,
                    })

        pending_out = None
        if pending:
            pending_out = {
                "id": pending["id"],
                "title": pending["title"],
                "text": pending["text"],
                "person_ids": list(pending["person_ids"]),
                "options": [
                    {"id": o["id"], "label": o["label"],
                     "known_effects": list(o["known_effects"])}
                    for o in pending["options"]
                ],
            }

        return {
            "schema_version": ENVELOPE_SCHEMA_VERSION,
            "game_id": env["game_id"],
            "revision": env["revision"],
            "status": env["status"],
            "player_id": player.person_id,
            "people": people,
            "stats": {
                "happiness": player.happiness,
                "health": player.health,
                "smarts": player.smarts,
                "looks": player.looks,
            },
            "money": player.money,
            "country": env["country"],
            "journal": [dict(e, person_ids=list(e["person_ids"]),
                             changes=dict(e["changes"]))
                        for e in env["journal"]],
            "available_actions": actions,
            "pending_decision": pending_out,
        }

    # ------------------------------------------------------------- games

    def list_games(self):
        with self._lock:
            games = []
            for name in sorted(os.listdir(self.save_dir)):
                if not name.endswith(".pickle") or name.startswith("_"):
                    continue
                path = os.path.join(self.save_dir, name)
                if not os.path.isfile(path) or os.path.islink(path):
                    continue
                try:
                    env = self._load(name[:-7])
                except GameError:
                    continue
                games.append({
                    "game_id": env["game_id"],
                    "name": env["player"].name,
                    "age": env["player"].age,
                    "status": env["status"],
                    "revision": env["revision"],
                })
            return {"games": games}

    def get_state(self, game_id):
        with self._lock:
            env = self._load(game_id)
            self._ensure_ids(env)
            return self._state(env)

    @staticmethod
    def _validate_profile(profile):
        if not isinstance(profile, dict):
            raise _invalid("profile must be an object")
        first = profile.get("first_name")
        last = profile.get("last_name")
        for label, value in (("first_name", first), ("last_name", last)):
            if (not isinstance(value, str) or not value.strip()
                    or len(value) > 40):
                raise _invalid(f"{label} must be a non-empty string")
        gender = profile.get("gender")
        if not isinstance(gender, str) or gender.lower() not in ("male", "female"):
            raise _invalid("gender must be 'male' or 'female'")
        country = profile.get("country")
        if not isinstance(country, str) or not country.strip() \
                or len(country) > 60:
            raise _invalid("country must be a non-empty string")
        seed = profile.get("seed")
        if seed is not None and (not isinstance(seed, int)
                                 or isinstance(seed, bool)):
            raise _invalid("seed must be an integer")
        appearance = profile.get("appearance")
        if appearance is not None:
            if not isinstance(appearance, dict):
                raise _invalid("appearance must be an object")
            GameService._validate_appearance_ids(appearance)
        return gender

    @staticmethod
    def _validate_appearance_ids(data):
        singles = ("skin_tone", "face_shape", "eye_shape", "eye_color",
                   "brows", "nose", "mouth", "hair_texture", "hair_color",
                   "hair_style", "facial_hair", "outfit_style",
                   "outfit_color")
        for field in singles:
            if field in data and not catalog.is_valid_id(field, data[field]):
                raise _invalid(f"appearance.{field} is not a valid catalog ID")
        if "marks" in data:
            marks = data["marks"]
            if not isinstance(marks, list) or any(
                    not catalog.is_valid_id("marks", m) for m in marks):
                raise _invalid("appearance.marks contains an invalid ID")
        if "accessories" in data:
            acc = data["accessories"]
            if not isinstance(acc, dict):
                raise _invalid("appearance.accessories must be an object")
            for cat in catalog.ACCESSORY_CATEGORIES:
                if cat in acc and not catalog.is_valid_id(cat, acc[cat]):
                    raise _invalid(
                        f"appearance.accessories.{cat} is not a valid catalog ID")

    def new_game(self, profile, request_id):
        with self._lock:
            if not isinstance(request_id, str) or not request_id or len(request_id) > 128:
                raise _invalid("request_id must be a non-empty string")
            self._validate_profile(profile)
            fingerprint = hashlib.sha256(
                json.dumps(profile, sort_keys=True, default=str).encode()
            ).hexdigest()
            ledger = self._load_creations()
            entry = ledger.get(request_id)
            if entry is None:
                # crash-durable fallback: the game file may exist even if
                # the index write did not survive
                creation = self._scan_creation(request_id)
                if creation is not None:
                    entry = {"fingerprint": creation.get("fingerprint", ""),
                             "game_id": creation.get("game_id", ""),
                             "response": creation.get("response")}
                    ledger[request_id] = entry
                    self._save_creations(ledger)
            if entry is not None:
                if entry.get("tombstone"):
                    raise _conflict(
                        "stale_revision",
                        "creation request is older than the replay "
                        "retention window")
                if entry.get("fingerprint") != fingerprint:
                    raise _conflict(
                        "conflict",
                        "request_id was already used with a different payload")
                env = self._load(entry["game_id"])
                creation = env.get("creation") or {}
                response = creation.get("response")
                if not isinstance(response, dict):
                    raise GameError(
                        "internal_error",
                        "creation replay data is corrupt", 500)
                # the ORIGINAL creation response, not the latest state
                return json.loads(json.dumps(response))

            appearance = profile.get("appearance")
            if appearance is not None:
                try:
                    Appearance.from_dict(appearance)
                except ValueError:
                    raise _invalid(
                        "appearance has an unsupported schema_version")

            seed = profile.get("seed")
            rng = random.Random(seed) if seed is not None else random.Random()
            game_id = "g_" + hashlib.sha256(
                os.urandom(16)).hexdigest()[:10]
            env = {
                "schema_version": ENVELOPE_SCHEMA_VERSION,
                "game_id": game_id,
                "revision": 0,
                "status": "alive",
                "country": profile.get("country", ""),
                "player": None,
                "journal": [],
                "rng_state": rng.getstate(),
                "cosmetic_rng_state": None,
                "pending": None,
                "year_actions": {"spend_time": []},
                "dead_ids": [],
                "archive": [],
                "used_decisions": [],
                "next_journal_num": 1,
                "next_decision_num": 1,
                "replay": [],
                "creation": {
                    "request_id": request_id,
                    "fingerprint": fingerprint,
                    "game_id": game_id,
                    "response": None,  # filled in below
                },
            }
            with _GameRNG(self, env):
                player = Player(
                    profile["first_name"].strip(),
                    profile["last_name"].strip(),
                    Gender.Male if profile["gender"].lower() == "male"
                    else Gender.Female,
                )
                player.money = 0
            if appearance is not None:
                # Manual creator appearance wins over the generated one
                player.appearance = Appearance.from_dict(appearance)
            env["player"] = player
            self._ensure_ids(env)
            self._journal(
                env, "birth",
                f"{player.name} was born"
                + (f" in {env['country']}." if env["country"] else "."),
                [p.person_id for p in self._walk_people(player)])
            env["revision"] = 1
            state = self._state(env)
            state["created"] = True
            env["creation"]["response"] = state
            # Publish the game and its creation replay as one logical unit:
            # the envelope is authoritative; the index is a rebuildable cache.
            self._save(env)
            ledger[request_id] = {
                "fingerprint": fingerprint,
                "game_id": game_id,
            }
            def _active_count():
                return sum(1 for v in ledger.values()
                           if not v.get("tombstone"))

            while _active_count() > self._creation_replay_limit:
                # cap ACTIVE entries only; tombstones persist as durable
                # request ownership so an expired id can never revive
                for oldest, evicted in list(ledger.items()):
                    if not evicted.get("tombstone"):
                        ledger[oldest] = {
                            "tombstone": True,
                            "fingerprint": evicted.get("fingerprint", "")}
                        break
            try:
                self._save_creations(ledger)
            except GameError:
                # roll the game publication back so a retry stays exactly-once
                try:
                    os.unlink(self._path(game_id))
                except OSError:
                    pass
                raise
            return json.loads(json.dumps(state))

    # ------------------------------------------------------------- replay

    def _record_replay(self, env, request_id, action, payload, response):
        env["replay"].append({
            "request_id": request_id,
            "action": action,
            "payload": payload,
            "response": response,
        })
        while len(env["replay"]) > REPLAY_LIMIT:
            env["replay"].pop(0)

    @staticmethod
    def _same_payload(a, b):
        return a == b

    # ------------------------------------------------------------- commands

    def command(self, game_id, expected_revision, request_id, action, payload):
        with self._lock:
            if not isinstance(request_id, str) or not request_id or len(request_id) > 128:
                raise _invalid("request_id must be a non-empty string")
            if not isinstance(action, str) or action not in (
                    "age_up", "spend_time", "resolve_decision"):
                raise _invalid("unknown action")
            if payload is None:
                payload = {}
            if not isinstance(payload, dict):
                raise _invalid("payload must be an object")
            if (not isinstance(expected_revision, int)
                    or isinstance(expected_revision, bool)
                    or expected_revision < 0):
                raise _invalid("expected_revision must be a non-negative integer")

            env = self._load(game_id)

            # Known replay is checked before the revision check; the stored
            # response is returned unchanged (byte-identical) so retries,
            # including after a restart, never reapply effects.
            for entry in env["replay"]:
                if entry["request_id"] == request_id:
                    if (entry["action"] != action
                            or not self._same_payload(entry["payload"], payload)):
                        raise _conflict(
                            "conflict",
                            "request_id was already used with a different payload")
                    return dict(entry["response"])

            if expected_revision != env["revision"]:
                raise _conflict(
                    "stale_revision",
                    f"expected_revision {expected_revision} does not match "
                    f"current revision {env['revision']}")

            if env["pending"] and action != "resolve_decision":
                raise _conflict(
                    "pending_decision",
                    "resolve the pending decision before other actions")
            if env["status"] != "alive" and action != "resolve_decision":
                raise _conflict("game_over", "this life has ended")

            with _GameRNG(self, env):
                if action == "age_up":
                    self._cmd_age_up(env)
                elif action == "spend_time":
                    self._cmd_spend_time(env, payload)
                else:
                    self._cmd_resolve(env, payload)

            env["revision"] += 1
            state = self._state(env)
            self._record_replay(env, request_id, action, payload, state)
            self._save(env)  # rollback = discard env; nothing else mutated
            return state

    # ------------------------------------------------------------- actions

    def _cmd_age_up(self, env):
        player = env["player"]
        env["year_actions"] = {"spend_time": []}
        before = (player.happiness, player.health, player.smarts, player.looks)

        player.total_happiness += player.happiness
        # Core drift only: Player.age_up drives the terminal UI and may
        # prompt for input, so the web slice uses Person.age_up plus the
        # documented legacy extras below (karma drift, relation aging).
        Person.age_up(player)
        player.change_karma(randint(-2, 2))
        for relation in list(player.relations):
            relation.age_up()
            if isinstance(relation, Parent):
                relation.change_relationship(
                    1 if player.age < 18 else random.choice((-1, -1, 0)))
            if relation.death_check():
                player.relations.remove(relation)
                env["dead_ids"].append(relation.person_id)
                # keep the person reachable for history/portraits
                if relation not in env["archive"]:
                    env["archive"].append(relation)
                self._journal(
                    env, "relation_death",
                    f"{relation.name} passed away at the age of {relation.age}.",
                    [relation.person_id])

        after = (player.happiness, player.health, player.smarts, player.looks)
        labels = ("happiness", "health", "smarts", "looks")
        changes = {label: new - old for label, old, new in
                   zip(labels, before, after) if new != old}
        self._journal(env, "year", f"{player.firstname} turned {player.age}.",
                      [player.person_id], changes)

        if player.death_check():
            env["status"] = "dead"
            env["dead_ids"].append(player.person_id)
            self._journal(
                env, "death",
                f"{player.name} passed away at the age of {player.age}.",
                [player.person_id])
            env["pending"] = None
            return

        template = decisions.sample(
            random, player.age, env["used_decisions"],
            [p.person_id for p in self._walk_people(player)
             if isinstance(p, Parent)])
        if template is not None:
            parent_ids = ([p.person_id for p in self._walk_people(player)
                           if isinstance(p, Parent)]
                          if template["person_ids"] == "parents" else [])
            seq = env["next_decision_num"]
            env["next_decision_num"] += 1
            env["pending"] = decisions.instantiate(
                template, seq, player.firstname, parent_ids)
            env["used_decisions"].append(template["id"])

    def _cmd_spend_time(self, env, payload):
        player = env["player"]
        target_id = payload.get("target_id")
        if not isinstance(target_id, str):
            raise _invalid("target_id is required")
        person = self._find_person(env, target_id)
        if not isinstance(person, Parent):
            raise _not_found("spend time is only available with parents")
        if target_id in env["year_actions"]["spend_time"]:
            raise _conflict("not_allowed",
                            "you already spent time with this person this year")
        env["year_actions"]["spend_time"].append(target_id)

        # Legacy rule from src/menus/main.py "Spend time" (M1 subset). The
        # journal records ACTUAL applied deltas (stats/relationship clamp at
        # 0/100), measured before/after — never the sampled intent.
        happy_before = player.happiness
        rel_before = person.relationship
        if person.relationship < 15:
            player.change_happiness(-4)
            changes = {}
            delta = player.happiness - happy_before
            if delta:
                changes["happiness"] = delta
            self._journal(
                env, "spend_time",
                f"{person.name} refused to see {player.firstname}.",
                [player.person_id, target_id], changes)
            return
        enjoyment1 = max(randint(0, 70), randint(0, 70)) + randint(0, 30)
        if player.has_trait("CHEERFUL"):
            enjoyment1 = max(enjoyment1, randint(0, 100))
        elif player.has_trait("GRUMPY"):
            enjoyment1 = min(enjoyment1, randint(0, 100))
        enjoyment2 = round(random.triangular(0, 100, person.relationship))
        happy_gain = round_stochastic(enjoyment1 / 12)
        rel_gain = round_stochastic(enjoyment2 / 12)
        player.change_happiness(happy_gain)
        person.change_relationship(rel_gain)
        if player.has_trait("CHEERFUL"):
            player.change_happiness(3)
        if player.age <= 2:
            stage = "baby"
        elif player.age <= 12:
            stage = "child"
        elif player.age <= 17:
            stage = "teen"
        elif player.age <= 49:
            stage = "adult"
        else:
            stage = "elder"
        place = random.choice(SPEND_PLACES[stage])
        changes = {}
        happy_delta = player.happiness - happy_before
        rel_delta = person.relationship - rel_before
        if happy_delta:
            changes["happiness"] = happy_delta
        if rel_delta:
            changes["relationship"] = rel_delta
        self._journal(
            env, "spend_time",
            f"{player.firstname} spent time with {person.name} {place}.",
            [player.person_id, target_id], changes)

    def _cmd_resolve(self, env, payload):
        pending = env.get("pending")
        if pending is None:
            raise _conflict("no_pending_decision", "nothing to resolve")
        decision_id = payload.get("decision_id")
        option_id = payload.get("option_id")
        if decision_id != pending["id"]:
            raise _not_found("unknown decision")
        option = next((o for o in pending["options"] if o["id"] == option_id), None)
        if option is None:
            raise _invalid("unknown option")
        player = env["player"]
        person_ids = list(pending["person_ids"])
        effects = option["_effects"]
        # authoritative affordability gate
        cost = effects.get("money", 0)
        if cost < 0 and player.money < -cost:
            raise _conflict(
                "not_allowed",
                f"this choice costs ${-cost} and you have ${player.money}")

        before_money = player.money
        before_stats = {
            k: getattr(player, k) for k in
            ("happiness", "health", "smarts", "looks")}
        rel_before = {}
        if "relationship" in effects and person_ids:
            for pid in person_ids:
                target = self._find_person(env, pid)
                if isinstance(target, Relationship):
                    rel_before[target] = target.relationship

        for key, amount in effects.items():
            if key == "money":
                player.money = max(0, player.money + amount)
            elif key == "relationship":
                for pid in person_ids:
                    target = self._find_person(env, pid)
                    if isinstance(target, Relationship):
                        target.change_relationship(amount)
            else:
                getattr(player, f"change_{key}")(amount)

        # journal records the ACTUAL applied deltas (capped values included)
        changes = {}
        money_delta = player.money - before_money
        if money_delta:
            changes["money"] = money_delta
        for key, before in before_stats.items():
            delta = getattr(player, key) - before
            if delta:
                changes[key] = delta
        if rel_before:
            rel_delta = sum(target.relationship - before
                            for target, before in rel_before.items())
            if rel_delta:
                changes["relationship"] = rel_delta
        self._journal(env, "decision",
                      option["_outcome"].format(name=player.firstname),
                      [player.person_id] + person_ids, changes)
        env["pending"] = None
