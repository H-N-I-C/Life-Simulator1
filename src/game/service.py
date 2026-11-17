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
from src.game.features.family_graph.graph import GraphError
from src.game.features.family_graph import new_graph
from src.game.features.relationship_policy.editor import new_policy_state
import src.game.features.parenting.effects  # noqa: F401,E402  (registers v3 domain appliers + child predicates)
import src.game.features.family_graph.effects  # noqa: F401,E402  (graph offer/respond v3 domain appliers)
import src.game.features.family_graph.predicates  # noqa: F401,E402  (graph-backed v3 role predicates)
import src.game.features.knowledge.effects  # noqa: F401,E402  (knowledge v3 domain appliers)
from src.game.finance import LedgerConflict  # noqa: E402
from src.game.features.parenting import new_parenting_state
from src.game.features.weddings import new_weddings_state
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


def knowledge_journal_visible(env, entry):
    """player_known/private journal filtering at serialization (frozen
    reputation-knowledge.md §2)."""
    from src.game.features.knowledge import journal_entry_visible
    return journal_entry_visible(env, entry)



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
        # game_id -> human-readable notice for the NEXT state/command
        # response (e.g. "recovered from backup"); consumed on read.
        self._notices = {}

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

    def _read_save(self, path):
        """Load + validate + migrate one envelope file."""
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
        # M2.5: backfill the frozen family-graph + editor-audit fields onto
        # any save (old saves load unchanged, persist once)
        from src.game.features.family_graph import normalize_envelope
        if normalize_envelope(env):
            dirty = True
        if not isinstance(env.get("policy"), dict):
            env["policy"] = new_policy_state()
            dirty = True
        from src.game.features.parenting import (
            new_parenting_state, normalize_parenting, backfill_all)
        if not isinstance(env.get("parenting"), dict):
            env["parenting"] = new_parenting_state()
            dirty = True
        elif normalize_parenting(env):
            dirty = True
        from src.game.features.weddings import (
            new_weddings_state, normalize_weddings)
        if not isinstance(env.get("weddings"), dict):
            env["weddings"] = new_weddings_state()
            dirty = True
        elif normalize_weddings(env):
            dirty = True
        from src.game.features.knowledge import (
            new_knowledge_state, normalize_knowledge)
        if not isinstance(env.get("knowledge"), dict):
            env["knowledge"] = new_knowledge_state()
            dirty = True
        elif normalize_knowledge(env):
            dirty = True
        if backfill_all(env, self):
            dirty = True
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
        # stamp missing M2.5 policy profiles once (deterministic per
        # person; a no-op after the first backfill)
        profiles_map = env["policy"].setdefault("profiles", {})
        for person in people:
            if person.person_id not in profiles_map:
                self.stamp_person_profile(env, person)
                dirty = True
        # M2 -> M2.5 family migration: runs exactly once per save, guarded
        # by the graph's migration_version marker (NOT by "no union edges",
        # which is also true after a divorce). Migration is idempotent and
        # merges into the existing graph, so a retry can never duplicate or
        # discard history.
        if env["graph"].get("migration_version") is None:
            from src.game.features.family_graph import migrate_v2
            env["graph"] = migrate_v2(env)
            dirty = True
        else:
            # E3: games whose FIRST load predated the family (scenario
            # starts walk the player up from childhood across loads)
            # stamped the marker while the partner did not exist yet —
            # the married partner never migrated. Rerun the idempotent
            # merge when a married adult partner has no graph edge; the
            # graph stays authoritative, so a post-divorce game
            # (partner None via the legacy sync) never re-triggers.
            from src.game.features.family_graph import graph as _fg_graph
            _player = env.get("player")
            _partner = getattr(_player, "partner", None)
            if _player is not None and _partner is not None \
                    and getattr(_player, "age", 0) >= _fg_graph.ADULT_AGE \
                    and getattr(_partner, "age", 0) >= _fg_graph.ADULT_AGE \
                    and _fg_graph._partner_married(_partner):
                _pid = getattr(_player, "person_id", None)
                _ppid = getattr(_partner, "person_id", None)
                if _pid and _ppid and not any(
                        _pid in u["person_ids"]
                        and _ppid in u["person_ids"]
                        for u in env["graph"]["unions"]):
                    from src.game.features.family_graph import migrate_v2
                    env["graph"] = migrate_v2(env)
                    dirty = True
        # Backfill graph parentage from legacy mother/father records,
        # per-record (never all-or-nothing): a newborn after the initial
        # parents still gets edges on the next load. Adopted children get
        # adoptive edges, never biological.
        everyone = list(people) + [
            c for c in (getattr(env["player"], "children", None) or [])
            if isinstance(c, Person)]
        for person in everyone:
            adopted = bool(getattr(person, "adopted", False))
            role = "adoptive" if adopted else "biological"
            source = "adoption" if adopted else "birth"
            for attr in ("mother", "father"):
                par = getattr(person, attr, None)
                if par is None or not getattr(par, "person_id", None):
                    continue
                if person.person_id == par.person_id:
                    continue
                if not any(
                        e["child_id"] == person.person_id
                        and e["parent_id"] == par.person_id
                        for e in env["graph"]["parentage"]):
                    env["graph"]["parentage"].append({
                        "child_id": person.person_id,
                        "parent_id": par.person_id,
                        "role": role,
                        "source": source,
                        "confidence": "assumed"})
                    dirty = True
            # legacy web games keep the player's own parents as Parent
            # relations; only a relation that IS a recorded parent (the
            # person's mother/father attribute or parents-dict entry) may
            # become a parentage edge — a Parent-typed acquaintance is not
            # a parent
            recorded = set()
            for attr in ("mother", "father"):
                par = getattr(person, attr, None)
                if par is not None:
                    recorded.add(getattr(par, "person_id", None))
            for par in (getattr(person, "parents", None) or {}).values():
                recorded.add(getattr(par, "person_id", None))
            for rel in getattr(person, "relations", None) or []:
                if isinstance(rel, Parent) \
                        and rel.person_id in recorded \
                        and person.person_id != rel.person_id and not any(
                            e["child_id"] == person.person_id
                            and e["parent_id"] == rel.person_id
                            for e in env["graph"]["parentage"]):
                    env["graph"]["parentage"].append({
                        "child_id": person.person_id,
                        "parent_id": rel.person_id,
                        "role": role,
                        "source": source,
                        "confidence": "assumed"})
                    dirty = True
        if dirty:
            self._save(env)
        # R5: the legacy single-partner view derives from the graph
        # authority at load; pure-legacy partners (no graph edges) are
        # untouched. Persist the resynced presentation once.
        from src.game.features.family_graph import transitions as \
            _fg_transitions
        if _fg_transitions.sync_legacy_partner(env):
            self._save(env)
        return env

    def _load(self, game_id):
        """Load a game; fall back to the last-good backup when the primary
        save is missing or corrupt, surfacing a one-shot notice."""
        path = self._path(game_id)
        if not os.path.isfile(path) or os.path.islink(path):
            primary_err = _not_found("unknown game")
        else:
            primary_err = None
        if primary_err is None:
            try:
                return self._read_save(path)
            except GameError as e:
                primary_err = e
        bak = path + ".bak"
        if os.path.isfile(bak) and not os.path.islink(bak):
            try:
                env = self._read_save(bak)
            except GameError:
                raise primary_err
            self._notices[game_id] = (
                "recovered your game from the last-good backup; the most "
                "recent changes may have been lost")
            try:
                self._save(env)  # restore the primary from the backup
            except GameError:
                pass  # serve from memory; the primary stays broken
            return env
        raise primary_err

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
                                 ("text", str), ("options", list)):
                    if not isinstance(pending.get(key), typ):
                        raise TypeError("pending." + key)
                if "person_ids" in pending                         and not isinstance(pending["person_ids"], list):
                    raise TypeError("pending.person_ids")
                if "kind" in pending:
                    pending.setdefault("person_ids", [])
                for option in pending["options"]:
                    if not isinstance(option, dict) \
                            or not isinstance(option.get("id"), str) \
                            or not isinstance(option.get("label"), str):
                        raise TypeError("pending option")
                if "kind" not in pending:
                    # M1 template pendings resolve through the service path
                    for option in pending["options"]:
                        for key, typ in (("known_effects", list),
                                         ("_effects", dict),
                                         ("_outcome", str)):
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
            env.setdefault("turn", {})
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
        """Atomic replace; raises on failure so callers roll back. Keeps the
        previous good save as <id>.pickle.bak so a corrupt primary can be
        recovered."""
        path = self._path(env["game_id"])
        try:
            fd, tmp = tempfile.mkstemp(dir=self.save_dir, suffix=".tmp")
            with os.fdopen(fd, "wb") as f:
                pickle.dump(env, f)
            if os.path.isfile(path) and not os.path.islink(path):
                os.replace(path, path + ".bak")
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

    def _all_people(self, env):
        """Everyone addressable by the graph: the living walk plus the
        archived (deceased/ex) people history keeps referencing."""
        people = {p.person_id: p for p in
                  self._walk_people(env["player"])}
        for person in env.get("archive", []):
            if isinstance(person, Person) \
                    and person.person_id not in people:
                people[person.person_id] = person
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
        from src.game.features.knowledge.core import pending_visible
        if pending and not pending_visible(env, pending):
            pending = None
        if env["status"] == "alive" and not pending:
            actions.append({"id": "age_up", "label": "Age up",
                            "category": "life", "inputs": [],
                            "known_costs": [], "limits": [],
                            "enabled": True, "reason": None})
            from src.menus import rule_actions
            from src.game.context import RuleContext
            covered_targets = set()
            try:
                for spec in rule_actions.action_specs(
                        RuleContext(env, self)):
                    spec.setdefault("inputs", [])
                    spec.setdefault("known_costs", [])
                    spec.setdefault("enabled", True)
                    spec.setdefault("reason", None)
                    actions.append(spec)
                    if spec.get("id") == "spend_time" and spec.get(
                            "target_id"):
                        covered_targets.add(spec["target_id"])
            except Exception:
                pass  # rule projection must never break state reads
            # Once-per-year state lives ONLY on the relation object
            # (relation.spent_time, set by rule_actions.spend_time and reset
            # by the yearly relation phase). env["year_actions"] is a legacy
            # envelope field kept for old saves, not a source of truth —
            # the advertised spec and dispatch read the same flag. The
            # fallback fires only for a parent the rule projection failed
            # to cover AND who is a real relation (dispatch refuses anyone
            # outside player.relations, so advertising them would be a
            # spec/dispatch disagreement).
            relation_ids = {p.person_id for p in
                            getattr(player, "relations", [])}
            for person in self._walk_people(player):
                # Same eligibility the rule enforces in
                # rule_actions.spend_time / _relationship_specs: never
                # advertise what dispatch will refuse.
                if player.age < 5 or getattr(person, "age", 0) < 5:
                    continue
                if isinstance(person, Parent) and not getattr(
                        person, "spent_time", False) \
                        and person.person_id not in covered_targets \
                        and person.person_id in relation_ids:
                    actions.append({
                        "id": "spend_time",
                        "label": f"Spend time with {person.firstname}",
                        "category": "relationships",
                        "target_id": person.person_id,
                        "inputs": [], "known_costs": [],
                        "limits": ["once_per_year"],
                        "enabled": True, "reason": None,
                    })

        pending_out = None
        if pending:
            options_out = []
            for o in pending["options"]:
                enabled = o.get("enabled", True)
                reason = o.get("reason")
                if "kind" not in pending and enabled:
                    # M1 template: gate paid options on actual affordability
                    eff = o.get("_effects") or {}
                    cost = max(0, -eff.get("money", 0))
                    if cost and player.money < cost:
                        enabled = False
                        reason = (f"this choice costs ${cost} and you have "
                                  f"${player.money}")
                options_out.append({
                    "id": o["id"], "label": o["label"],
                    "known_effects": list(o.get("known_effects", [])),
                    "enabled": enabled, "reason": reason,
                    "inputs": o.get("inputs", [])})
            # SOFT-LOCK GUARANTEE: at least one option stays available
            if options_out and not any(o["enabled"] for o in options_out):
                by_id = {x["id"]: (x.get("_effects") or {})
                         for x in pending["options"]}
                free = next(o for o in options_out
                            if max(0, -by_id.get(o["id"], {})
                                   .get("money", 0)) == 0)
                free["enabled"] = True
                free["reason"] = None
            pending_out = {
                "id": pending["id"],
                "kind": pending.get("kind"),
                "event_id": pending.get("event_id"),
                "title": pending["title"],
                "text": pending["text"],
                "person_ids": list(pending.get("person_ids", [])) or
                [player.person_id],
                "options": options_out,
            }
            # v3 additive fields (frozen event-schema-v3.md §6 / Claude
            # decision 1): pass through untouched; resolve uses the
            # envelope copy, the client only needs them read-only
            for key in ("schema_dialect", "bindings", "interaction",
                        "audience", "sampled", "policy_revision"):
                if key in pending:
                    pending_out[key] = pending[key]

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
                        for e in env["journal"]
                        # server-side player_known filter (frozen
                        # reputation-knowledge.md §2, Claude decision 3):
                        # private / not-player-known occurrences never
                        # leave the server
                        if knowledge_journal_visible(env, e)],
            "available_actions": actions,
            "pending_decision": pending_out,
            "education": self._education_view(player),
            "work": self._work_view(player),
            "health": {"illnesses": [str(i) for i in player.illnesses],
                       "treatments_available": []},
            "family": {
                "parents": [p.person_id for p in
                            env["player"].parents.values()
                            if p.person_id not in env.get("dead_ids", [])],
                "partner": env["player"].partner.person_id
                if env["player"].partner else None,
                "partner_status": env["player"].partner.status
                if env["player"].partner else None,
                "children": [c.person_id for c in env["player"].children],
                "siblings": [],
            },
            "finances": self._finances_view(env),
            "turn": {"turn_id": env.get("turn", {}).get("turn_id"),
                     "cursor": env.get("turn", {}).get("cursor"),
                     "pending": bool(pending)},
            "generation": env.get("generation", 1),
            "completed_lives": list(env.get("completed_lives", [])),
            "succession": self._succession_view(env),
        }

    def _succession_view(self, env):
        if env.get("status") != "dead":
            return None
        from src.menus import rule_actions
        eligible = rule_actions.eligible_successors(env["player"])
        return {
            "eligible": [{
                "person_id": c.person_id,
                "name": c.name,
                "age": c.age,
            } for c in eligible],
            "generation_next": env.get("generation", 1) + 1,
        }

    def _finances_view(self, env):
        from src.game.finance import Ledger
        from src.people.classes.player import annual_income_quote
        ledger = Ledger(env, resolver=self._make_resolver(env))
        player = env["player"]
        # Deterministic preview: the quote at the retention mean (U(0.4,0.8)
        # -> 0.6). The actual retention is sampled once per account/turn at
        # year close and posted; previews never consume gameplay RNG.
        estimate = annual_income_quote(
            player.salary, player.has_job, player.job_hours, 0.6)
        return {
            "balance": player.money,
            "debts": ledger.debt_projection(),
            "annual_estimate": estimate,
            "last_closed_turn": env.get("turn", {}).get("turn_id"),
            "ledger": ledger.entries_for(
                owner_id=player.person_id),
        }

    @staticmethod
    def _education_view(player):
        if player.uv_years > 0 or getattr(player, "graduated_uv", False):
            stage = "university"
        elif player.is_in_school():
            if player.age <= 11:
                stage = "elementary"
            elif player.age <= 13:
                stage = "middle"
            else:
                stage = "high"
        else:
            stage = "none"
        return {
            "stage": stage,
            "enrolled": player.is_in_school() or player.uv_years > 0,
            "grades": player.grades if isinstance(player.grades, int)
            else 0,
            "dropped_out": player.dropped_out,
            "university_years_remaining": player.uv_years,
            "graduated_high_school": player.age > 17 and not player.dropped_out,
            "loan": {
                "principal": getattr(player, "student_loan", 0),
                "remaining": player.student_loan,
                "has_loan": player.student_loan > 0,
                # funding source chosen at enrollment; the PRINCIPAL lands
                # at graduation (legacy parity), so has_loan stays false
                # while enrolled — funded_by shows the source meanwhile
                "funded_by": "loan" if getattr(
                    player, "chose_student_loan", False) else None,
            },
        }

    @staticmethod
    def _work_view(player):
        # C's retire path: lose_job() then salary = pension. A fired
        # worker also has has_job False but salary reset to 0, so
        # salary > 0 at retirement age distinguishes the pensioner.
        retired = (not player.has_job) and player.salary > 0 \
            and player.age >= 65
        return {
            "employed": player.has_job,
            "retired": retired,
            "job_label": "Generic job" if player.has_job else None,
            "hours": player.job_hours,
            "salary": player.salary,
            "effective_annual_gross": player.salary,
            "pension": player.salary if retired else 0,
            "performance": player.performance,
            "stress": player.stress,
            "years_worked": player.years_worked,
            "can_retire": (not retired) and player.years_worked >= 10
            and player.age >= 65,
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

    def delete_game(self, game_id):
        """Confirmed destructive delete (B's UI owns the double-confirm).
        Removes the save file; listing and replay state reflect it."""
        with self._lock:
            path = self._path(game_id)
            if not os.path.isfile(path) or os.path.islink(path):
                raise _not_found("unknown game")
            try:
                os.unlink(path)
                bak = path + ".bak"
                if os.path.isfile(bak) and not os.path.islink(bak):
                    os.unlink(bak)
            except OSError as e:
                raise GameError(
                    "internal_error",
                    "the operation failed; no changes were applied",
                    500) from e
            return {"deleted": True, "game_id": game_id}

    # ------------------------------------------- M2.5 policy/editor adapter
    # A owns: the envelope (policy/audit), person records (rename
    # forwarding), the graph supersession hook. B owns the Editor engine
    # (relationship_policy.editor) and the HTTP routes in web/server.py.

    @staticmethod
    def _profile_rng(env, person_id):
        """Deterministic per (game, person). Profile generation is cosmetic
        and deliberately NOT drawn from the gameplay RNG stream, so M2.5
        stamping can never perturb life simulation draws (replay parity)."""
        digest = hashlib.sha256(
            ("profile:%s:%s" % (env["game_id"], person_id)).encode()
        ).hexdigest()
        return random.Random(int(digest[:16], 16))

    def stamp_person_profile(self, env, person):
        """Frozen attraction/policy profile for one person; called at
        creation and whenever C's content adds a person (births, new
        relations). Idempotent per person."""
        from src.game.features.relationship_policy import profiles
        from src.game.features.relationship_policy.catalog import \
            load_catalog
        profiles_map = env["policy"].setdefault("profiles", {})
        pid = person.person_id
        if pid not in profiles_map:
            profiles_map[pid] = profiles.new_profile(
                load_catalog(), self._profile_rng(env, pid))
        return profiles_map[pid]

    def _stamp_all_profiles(self, env):
        for person in self._walk_people(env["player"]):
            self.stamp_person_profile(env, person)

    def get_policy_state(self, game_id):
        """Envelope policy state (deep copy). B's route merges catalogs +
        the people roster around this."""
        with self._lock:
            env = self._load(game_id)
            return json.loads(json.dumps(env["policy"]))

    def get_family_tree(self, game_id, focus_id=None, max_generations=3):
        """Frozen family-tree projection + appearances for portrait
        rendering (M0b renderer resolves them client-side). Secret edges
        are hidden until C's observer-knowledge records decide what the
        viewer knows (M2.5a ships no secrets)."""
        from src.game.features import family_graph as fg
        with self._lock:
            env = self._load(game_id)
            people = self._all_people(env)
            for pid in env.get("dead_ids", []):
                if pid in people:
                    people[pid].alive = False
            focus = focus_id or env["player"].person_id
            if focus not in people:
                raise _not_found("unknown person")
            from src.game.features.knowledge.core import (
                known_secret_relationship_ids)
            known_ids = known_secret_relationship_ids(env)
            tree = fg.graph_tree(env["graph"], focus, people,
                                 max_generations=max_generations,
                                 known_relationship_ids=known_ids)
            appearances = {}
            for node in tree["nodes"]:
                person = people.get(node["person_id"])
                app = getattr(person, "appearance", None)
                if app is not None:
                    appearances[node["person_id"]] = \
                        app.to_dict() if hasattr(app, "to_dict") else None
            return {"tree": tree, "appearances": appearances}

    # ----------------------------------------- step 4: kinship + child health

    def get_relatedness(self, game_id, a_id, b_id):
        """Documented fictional biological relatedness (frozen bands).
        Name/religion/orientation never enter the coefficient."""
        from src.game.features import family_graph as fg
        with self._lock:
            env = self._load(game_id)
            people = self._all_people(env)
            for pid in (a_id, b_id):
                if pid not in people:
                    raise _not_found("unknown person")
            return fg.graph_relatedness(env["graph"], a_id, b_id,
                                        persons_by_id=people)

    def record_conception(self, game_id, child_id, bio_mother_id,
                          bio_father_id, request_id):
        """Roll the bounded fictional child-health adjustment ONCE per
        conception, with the game RNG, and persist it. Idempotent per
        (child_id, request_id): a retried conception command returns the
        stored outcome; adoption never routes here. The conception binds
        IMMUTABLE biological parent ids: biological parentage edges are
        recorded at conception (deduped), and the stored outcome carries
        the bound parent ids for exactly-once consumption at birth."""
        from src.game.features import family_graph as fg
        with self._lock:
            if not isinstance(request_id, str) or not request_id:
                raise _invalid("request_id must be a non-empty string")
            if bio_mother_id == bio_father_id:
                raise _invalid("biological parents must be two people")
            env = self._load(game_id)
            outcomes = env["graph"].setdefault("conception_outcomes", {})
            key = "%s:%s" % (child_id, request_id)
            if key in outcomes:
                return json.loads(json.dumps(outcomes[key]))
            # ONE conception per child: an existing bound outcome for this
            # child wins BEFORE any RNG use — a retried or new-request
            # conception of the same parents returns the stored draw; a
            # different parent pair is a typed conflict, never a reroll.
            for stored_key, stored in outcomes.items():
                if not stored_key.startswith("%s:" % child_id):
                    continue
                bound = {stored.get("bio_mother_id"),
                         stored.get("bio_father_id")}
                if bound == {bio_mother_id, bio_father_id}:
                    return json.loads(json.dumps(stored))
                raise _conflict(
                    "graph_error",
                    "this child already has a conception bound to other "
                    "biological parents")
            people = self._all_people(env)
            for pid in (bio_mother_id, bio_father_id):
                person = people.get(pid)
                if person is None:
                    raise _not_found("unknown person")
                if person.age < 18:
                    raise _conflict("not_adult",
                                    "both biological parents must be 18+")
            with _GameRNG(self, env):
                outcome = fg.sample_conception(env["graph"], bio_mother_id,
                                               bio_father_id, random,
                                               persons_by_id=people)
            outcome["bio_mother_id"] = bio_mother_id
            outcome["bio_father_id"] = bio_father_id
            # bind the conception to immutable biological parentage now,
            # so a later birth cannot reroll or reassign the parents
            existing = {(e["child_id"], e["parent_id"])
                        for e in env["graph"]["parentage"]}
            for parent_id in (bio_mother_id, bio_father_id):
                if parent_id == child_id \
                        or (child_id, parent_id) in existing:
                    continue
                env["graph"]["parentage"].append({
                    "child_id": child_id, "parent_id": parent_id,
                    "role": "biological", "source": "birth",
                    "confidence": "certain"})
            outcomes[key] = outcome
            self._save(env)
            return json.loads(json.dumps(outcome))

    def consume_conception_outcome(self, game_id, child_id):
        """Exactly-once consumption of the recorded fictional conception
        outcome at birth, however it was stored (service child:request
        keys or v3 occurrence/pair keys). Returns the stored outcome
        (incl. its adjustment) or None when no conception was recorded
        (e.g. legacy births/adoption — adoption never rolls). Marks the
        outcome consumed so a replayed/retried birth cannot apply it
        twice."""
        from src.game.features.parenting import core as parenting_core
        with self._lock:
            env = self._load(game_id)
            child = self._all_people(env).get(child_id)
            # the child record may not exist yet (a pre-birth id bound at
            # conception): resolve the binding from graph parentage edges
            probe = child if child is not None else child_id
            key, outcome = parenting_core.find_conception_outcome(
                env, probe)
            if outcome is None:
                return None
            outcome["consumed"] = True
            outcome["consumed_child_id"] = child_id
            self._save(env)
            return json.loads(json.dumps(outcome))

    def wedding_quote(self, game_id, union_id):
        """Q1: the shared engine quote for ceremony legal consequences
        (B's planner route consumes this). Samples once, stores the
        quote on the wedding plan and returns ALL fine lines; the
        Ledger later posts exactly this quote. Sampling rides the
        gameplay stream and the plan state persists, so this is a
        mutation — B should expose it as POST, not GET."""
        from src.game.features.weddings import core as weddings
        from src.game.features.family_graph.graph import GraphError
        with self._lock:
            if not isinstance(union_id, str) or not union_id:
                raise _invalid("union_id must be a non-empty string")
            env = self._load(game_id)
            try:
                with _GameRNG(self, env):
                    quote = weddings.quote_ceremony_fines(env, union_id)
            except GraphError as e:
                raise GameError(e.code, e.message, e.status) from e
            self._save(env)
            return json.loads(json.dumps(quote))

    # ------------------------------------------- M2.5 weddings adapter

    def wedding_command(self, game_id, expected_revision, request_id, op,
                        args):
        """Replay-safe wrapper for weddings ops (plan_wedding, renew_vows,
        divorce_union). Same replay/revision envelope as graph_command;
        every charge posts once through the ledger inside the op."""
        from src.game.features.weddings import core as weddings
        from src.game.features.family_graph.graph import GraphError
        OPS = {"plan_wedding": weddings.plan_wedding,
               "renew_vows": weddings.renew_vows,
               "divorce_union": weddings.divorce_union}
        with self._lock:
            if not isinstance(request_id, str) or not request_id \
                    or len(request_id) > 128:
                raise _invalid("request_id must be a non-empty string")
            if not isinstance(op, str) or op not in OPS:
                raise _invalid("unknown wedding op")
            if not isinstance(expected_revision, int) or isinstance(expected_revision, bool) \
                    or expected_revision < 0:
                raise _invalid("expected_revision must be a non-negative integer")
            if not isinstance(args, dict):
                raise _invalid("args must be an object")
            required = {"union_id"}
            allowed = {"union_id"}
            if op == "plan_wedding":
                required |= {"venue_id", "guests", "outfit_id", "honeymoon_id"}
                allowed = required
            elif op == "renew_vows":
                allowed |= {"tier"}
            if required - args.keys() or args.keys() - allowed:
                raise _invalid("missing or unknown wedding fields")
            if not isinstance(args["union_id"], str) or not args["union_id"] \
                    or len(args["union_id"]) > 128:
                raise _invalid("union_id must be a non-empty id")
            if op == "plan_wedding":
                for key, catalog in (("venue_id", weddings.VENUES),
                                     ("outfit_id", weddings.OUTFITS),
                                     ("honeymoon_id", weddings.HONEYMOONS)):
                    if not isinstance(args[key], str) or args[key] not in catalog:
                        raise _invalid("unknown wedding %s" % key)
                guests = args["guests"]
                if not isinstance(guests, int) or isinstance(guests, bool) \
                        or not 0 <= guests <= 500:
                    raise _invalid("guests must be an integer in [0, 500]")
            elif op == "renew_vows":
                tier = args.get("tier", "backyard")
                if not isinstance(tier, str) or tier not in weddings.RENEWAL_TIERS:
                    raise _invalid("unknown renewal tier")
            env = self._load(game_id)
            for entry in env["replay"]:
                if entry["request_id"] == request_id:
                    if (entry["action"] != "wedding:" + op
                            or not self._same_payload(
                                entry["payload"], args)):
                        raise _conflict(
                            "conflict",
                            "request_id was already used with a different "
                            "payload")
                    return dict(entry["response"])
            if expected_revision != env["revision"]:
                raise _conflict(
                    "stale_revision",
                    f"expected_revision {expected_revision} does not match "
                    f"current revision {env['revision']}")
            op_args = dict(args)
            op_args["turn"] = env["revision"]
            try:
                # L1: ceremony law fines sample from the gameplay stream,
                # so wedding commands run under the per-game RNG exactly
                # like every other mutation.
                with _GameRNG(self, env):
                    result = OPS[op](env, self, **op_args)
            except GraphError as e:
                raise GameError(e.code, e.message, e.status) from e
            from src.game.features.family_graph import transitions as \
                _fg_transitions
            _fg_transitions.sync_legacy_partner(env)
            for entry in result.get("journal") or []:
                self._journal(env, entry["kind"], entry["text"],
                              entry.get("person_ids", []))
            env["revision"] += 1
            state = {"revision": env["revision"], "result": result}
            # replay stores the CALLER's args (no injected turn) so a
            # retry compares byte-identically
            self._record_replay(env, request_id, "wedding:" + op, args,
                                state)
            self._save(env)
            return json.loads(json.dumps(state))

    # -------------------------------------- step 6: typed graph transitions

    def graph_command(self, game_id, expected_revision, request_id, op,
                      args):
        """Replay-safe wrapper around family_graph.transitions.OPS for
        direct UI/feature use. B/C effect targets should call the OPS
        inside their own command context instead (frozen §7); this wrapper
        journals actual deltas, bumps the revision and persists."""
        from src.game.features.family_graph import transitions
        from src.game.features.family_graph.graph import GraphError
        with self._lock:
            if not isinstance(request_id, str) or not request_id \
                    or len(request_id) > 128:
                raise _invalid("request_id must be a non-empty string")
            if op not in transitions.OPS:
                raise _invalid("unknown graph op")
            if not isinstance(args, dict):
                raise _invalid("args must be an object")
            env = self._load(game_id)
            for entry in env["replay"]:
                if entry["request_id"] == request_id:
                    if (entry["action"] != "graph:" + op
                            or not self._same_payload(
                                entry["payload"], args)):
                        raise _conflict(
                            "conflict",
                            "request_id was already used with a different "
                            "payload")
                    return dict(entry["response"])
            if expected_revision != env["revision"]:
                raise _conflict(
                    "stale_revision",
                    f"expected_revision {expected_revision} does not match "
                    f"current revision {env['revision']}")
            try:
                result = transitions.OPS[op](env, **args)
            except GraphError as e:
                raise GameError(e.code, e.message, e.status) from e
            transitions.sync_legacy_partner(env)
            for entry in result.get("journal") or []:
                self._journal(env, entry["kind"], entry["text"],
                              entry.get("person_ids", []))
            env["revision"] += 1
            state = {"revision": env["revision"], "result": result}
            self._record_replay(env, request_id, "graph:" + op, args,
                                state)
            self._save(env)
            return json.loads(json.dumps(state))



    def _editor(self, env, hook=True):
        from src.game.features.relationship_policy.editor import Editor
        from src.game.features.family_graph.service_view import \
            ServiceGraphView
        supersession = (lambda flt: self._editor_supersede(env, flt)) \
            if hook else None
        return Editor(env["policy"], graph=ServiceGraphView(env),
                      supersession_hook=supersession)

    def _editor_supersede(self, env, flt):
        """A's on_policy_revision_bump (frozen filter): supersede matching
        pending offers AND the actual stored decision when it is one of
        those offers (dating pendings are not graph offers). Attempts/
        cooldowns/pre-effects are preserved; supersession never resamples
        and never invents consent; the cancelled offer resolves to a safe
        explicit 'the moment has passed' outcome."""
        from src.game.features import family_graph
        from src.game.features.family_graph.graph import ROMANTIC_KINDS
        jurisdictions = flt.get("affected_jurisdictions") or []
        superseded = family_graph.supersede_offers(
            env["graph"],
            interaction_kinds=list(ROMANTIC_KINDS),
            affected_person_ids=flt.get("affected_person_ids"),
            affected_jurisdiction=jurisdictions[0]
            if len(jurisdictions) == 1 else None)
        pending = env.get("pending")
        if pending is not None and self._pending_superseded(
                pending, flt, set(ROMANTIC_KINDS)):
            env["pending"] = None
            self._journal(
                env, "event",
                "The moment has passed — the offer no longer stands.",
                [])
            superseded.append("pending:%s" % pending.get("id"))
        return superseded

    @staticmethod
    def _pending_superseded(pending, flt, romantic_kinds):
        """A stored decision is superseded when its interaction is a
        romantic kind the evaluator would now score differently AND it
        touches an affected person. Legacy stored date offers count."""
        kind = (pending.get("interaction") or {}).get("kind")
        if kind is None and pending.get("kind") == "date_offer":
            kind = "date"
        if kind not in romantic_kinds:
            return False
        affected = flt.get("affected_person_ids")
        if not affected:
            return True
        involved = set()
        for value in (pending.get("bindings") or {}).values():
            if isinstance(value, str):
                involved.add(value)
        for key in ("sampled_partner", "target_id"):
            value = pending.get(key)
            if isinstance(value, str):
                involved.add(value)
            elif hasattr(value, "person_id"):
                involved.add(value.person_id)
        return bool(involved & set(affected))

    def _editor_reproject(self, env, jurisdiction_ids):
        """A4 recognition REPROJECTION: a jurisdiction law edit updates
        the living world's active unions in that jurisdiction — new
        status from the ratified recognition patch, law_version stamped
        with the new policy_revision, basis/union untouched. Unions the
        edited law does not govern keep their recognition; no union is
        deleted and no fine is retroactively repeated."""
        from src.game.features.relationship_policy.editor import (
            _union_type_of)
        policy = env["policy"]
        overrides = (policy.get("overrides") or {}).get("jurisdictions", {})
        law_version = int(policy.get("policy_revision", 0))
        changed = []
        for union in env["graph"]["unions"]:
            if union["status"] != "active":
                continue
            rec = union.get("recognition") or {}
            if rec.get("jurisdiction_id") not in jurisdiction_ids:
                continue
            union_type = _union_type_of(union)
            patch = (overrides.get(rec["jurisdiction_id"]) or {}) \
                .get("recognition", {})
            if union_type not in patch:
                continue
            if rec.get("status") == patch[union_type] \
                    and int(rec.get("law_version", 0)) == law_version:
                continue
            new_rec = dict(rec)
            new_rec["status"] = patch[union_type]
            new_rec["law_version"] = law_version
            union["recognition"] = new_rec
            changed.append(union["union_id"])
        return changed

    def editor_preview(self, game_id, commands):
        """Dry-run on a COPY of the policy state; the save is untouched."""
        with self._lock:
            if not isinstance(commands, list) or not commands:
                raise _invalid("commands must be a non-empty list")
            env = self._load(game_id)
            editor = self._editor(env, hook=False)
            editor.state = json.loads(json.dumps(env["policy"]))
            return editor.preview(commands)

    def editor_apply(self, game_id, expected_revision, request_id,
                     commands):
        """Frozen editor-protocol apply: replay-exact retries, revision
        semantics, A-side rename forwarding + audit mirror + supersession
        hook, envelope persisted once."""
        with self._lock:
            if not isinstance(request_id, str) or not request_id \
                    or len(request_id) > 128:
                raise _invalid("request_id must be a non-empty string")
            if not isinstance(commands, list) or not commands:
                raise _invalid("commands must be a non-empty list")
            env = self._load(game_id)
            # known replay answers before the revision check: retries,
            # including across restarts, return the stored response
            for entry in env["replay"]:
                if entry["request_id"] == request_id:
                    if (entry["action"] != "editor_apply"
                            or not self._same_payload(
                                entry["payload"], {"commands": commands})):
                        raise _conflict(
                            "conflict",
                            "request_id was already used with a different "
                            "payload")
                    return dict(entry["response"])
            if expected_revision != env["revision"]:
                raise _conflict(
                    "stale_revision",
                    f"expected_revision {expected_revision} does not match "
                    f"current revision {env['revision']}")
            result = self._editor(env).apply(commands,
                                             turn=env["revision"])
            self._apply_editor_side_effects(env, result["audit"])
            env["revision"] += 1
            state = {"revision": env["revision"],
                     "policy_revision": result["policy_revision"],
                     "audit": result["audit"]}
            self._record_replay(env, request_id, "editor_apply",
                                {"commands": commands}, state)
            self._save(env)
            return json.loads(json.dumps(state))

    def _apply_editor_side_effects(self, env, audit):
        """A-owned effects of an applied editor batch: the envelope audit
        mirror, rename forwarding to the person record (identity
        untouched), and a diegetic journal entry for renames only
        (scoring edits stay journal-quiet per the frozen protocol)."""
        people = {p.person_id: p for p in self._walk_people(env["player"])}
        for entry in audit:
            env["editor_audit"].append(entry)
            if entry.get("command") != "rename_person":
                continue
            person = people.get(entry.get("target_id"))
            if person is None:
                raise GameError("not_found", "unknown person", 404)
            after = entry.get("after") or {}
            if "first_name" in after:
                person.firstname = after["first_name"]
            if "last_name" in after:
                person.lastname = after["last_name"]
            if hasattr(person, "update_name"):
                person.update_name()
            self._journal(env, "editor",
                          f"{person.name} is now called {person.name}.",
                          [person.person_id])
        # A4: jurisdiction law edits reproject recognition onto the
        # living world's active unions immediately
        jur_ids = sorted({entry.get("target_id") for entry in audit
                          if entry.get("scope") == "jurisdiction"})
        if jur_ids:
            for union_id in self._editor_reproject(env, jur_ids):
                self._journal(
                    env, "editor",
                    "The law changed, and a union's recognition "
                    "changed with it.", [])


    def get_state(self, game_id):
        with self._lock:
            env = self._load(game_id)
            self._ensure_ids(env)
            state = self._state(env)
            notice = self._notices.pop(game_id, None)
            if notice:
                state["notice"] = notice
            return state

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
        scenario = profile.get("scenario")
        if scenario is not None:
            from src.game.scenarios import SCENARIOS
            if not isinstance(scenario, str) or scenario not in SCENARIOS:
                raise _invalid(f"unknown scenario: {scenario!r}")
        return gender

    @staticmethod
    def _validate_appearance_ids(data):
        """Validate only the fields the CURRENT catalog knows: every
        present field belonging to a live category must hold a valid id
        (v1 ids are accepted — the model migrates them at load); fields
        from older/newer schema versions that the catalog no longer has
        are ignored (tolerant contract)."""
        for field, value in data.items():
            if field in ("schema_version", "seed"):
                continue
            if field == "marks":
                if value is None:
                    continue
                if not isinstance(value, list) or (
                        "marks" in catalog.CATEGORIES and any(
                            not catalog.is_valid_id("marks", m)
                            for m in value)):
                    raise _invalid(
                        "appearance.marks contains an invalid ID")
                continue
            if field in getattr(catalog, "MULTI_CATEGORIES", set()):
                # list-valued category (v2 badges: up to 2 of
                # glasses/earrings/cap/crown/flower)
                if not isinstance(value, list) or any(
                        not catalog.is_valid_id(field, v) for v in value):
                    raise _invalid(
                        f"appearance.{field} contains an invalid ID")
                if field == "badges" and len(value) > 2:
                    raise _invalid(
                        "appearance.badges allows at most 2 badges")
                continue
            if field == "accessories":
                if value is None:
                    continue
                if not isinstance(value, dict):
                    raise _invalid("appearance.accessories must be an object")
                for cat in catalog.ACCESSORY_CATEGORIES:
                    if cat in value and cat in catalog.CATEGORIES \
                            and not catalog.is_valid_id(cat, value[cat]):
                        raise _invalid(
                            f"appearance.accessories.{cat} is not a "
                            f"valid catalog ID")
                continue
            if field in catalog.CATEGORIES and not catalog.is_valid_id(
                    field, value):
                raise _invalid(f"appearance.{field} is not a valid "
                               f"catalog ID")

    def new_game(self, profile, request_id):
        return self._new_game(profile, request_id)

    def _new_game(self, profile, request_id, scenario_attempt=0,
                  creation_fingerprint=None):
        """Bound dev-scenario retries while retaining the caller's replay key."""
        with self._lock:
            if not isinstance(request_id, str) or not request_id or len(request_id) > 128:
                raise _invalid("request_id must be a non-empty string")
            self._validate_profile(profile)
            fingerprint = creation_fingerprint or hashlib.sha256(
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
                "graph": new_graph(),
                "editor_audit": [],
                "policy": new_policy_state(),
                "parenting": new_parenting_state(),
                "weddings": new_weddings_state(),
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
            # M2.5: stamp the frozen attraction/policy profile for every
            # initial person. Profile draws use a DEDICATED deterministic
            # rng (seeded per game+person) so cosmetic policy generation
            # never perturbs the gameplay rng stream (M2 replay parity).
            self._stamp_all_profiles(env)
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
            scenario = profile.get("scenario")
            if scenario is not None:
                from src.game.scenarios import drive_scenario, SCENARIO_MAX_ATTEMPTS
                try:
                    state = drive_scenario(self, game_id, scenario)
                except GameError as e:
                    # dev-only path: roll the unfinished scenario back so
                    # a retry starts clean
                    try:
                        os.unlink(self._path(game_id))
                    except OSError:
                        pass
                    ledger.pop(request_id, None)
                    self._save_creations(ledger)
                    # Retry stochastic setup misses, not engine/I/O faults.
                    # Keep the original fingerprint: the shifted seed is an
                    # internal setup detail, not a new creation request.
                    if e.code == "scenario_precondition" \
                            or "life ended early" in e.message:
                        if scenario_attempt + 1 < SCENARIO_MAX_ATTEMPTS:
                            seed = profile.get("seed")
                            next_seed = (seed if seed is not None else 0) + 1
                            return self._new_game(
                                dict(profile, seed=next_seed), request_id,
                                scenario_attempt=scenario_attempt + 1,
                                creation_fingerprint=fingerprint)
                        raise GameError(
                            "scenario_precondition",
                            "scenario setup preconditions could not be met "
                            f"after {SCENARIO_MAX_ATTEMPTS} attempts; try another seed",
                            409) from e
                    raise
                # replays of the creation request return the SCENARIO state
                env = self._load(game_id)
                env["creation"]["response"] = json.loads(
                    json.dumps(state))
                self._save(env)
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
            from src.menus import rule_actions
            rule_actions_set = set(getattr(
                rule_actions, "ACTION_IDS", ()))
            for table in ("_TARGET_ACTIONS", "_SELF_ACTIONS",
                          "_PARENT_ACTIONS", "_OFFER_ACTIONS"):
                rule_actions_set |= set(getattr(
                    rule_actions, table, {}))
            rule_actions_set.add("find_partner")
            rule_actions_set.add("continue_as")
            rule_actions_set.add("lottery")
            if not isinstance(action, str) or (
                    action not in ("age_up", "spend_time",
                                   "resolve_decision")
                    and action not in rule_actions_set):
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
            state = self._execute(
                env, expected_revision, request_id, action, payload)
            self._save(env)  # rollback = discard env; nothing else mutated
            notice = self._notices.pop(game_id, None)
            if notice:
                state["notice"] = notice
            return state

    def _execute(self, env, expected_revision, request_id, action, payload):
        """Validate and apply one command to a loaded envelope. Callers own
        persistence: GameService.command saves after success (discarding
        the envelope on failure); the dev scenario driver runs many
        commands in memory and persists once at the end."""
        if True:
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

            from src.game.features.knowledge.core import pending_visible
            if env["pending"] and not pending_visible(env, env["pending"]):
                # An old/private NPC offer is not a player decision. Cancel
                # it without choosing an outcome or inventing consent; the
                # saved cursor lets age_up continue the interrupted turn.
                env["pending"] = None
            if env["pending"] and action not in (
                    "resolve_decision", "continue_as"):
                raise _conflict(
                    "pending_decision",
                    "resolve the pending decision before other actions")
            if env["status"] != "alive" and action not in (
                    "resolve_decision", "continue_as"):
                raise _conflict("game_over", "this life has ended")

            try:
                with _GameRNG(self, env):
                    if action == "age_up":
                        self._cmd_age_up(env)
                    elif action == "resolve_decision":
                        self._cmd_resolve(env, payload, request_id)
                    elif action == "continue_as":
                        self._cmd_continue_as(env, payload)
                    else:
                        self._cmd_rule_action(env, action, payload,
                                              request_id)
            except LedgerConflict as exc:
                # W1 (A half): a genuine posting-key collision is a
                # typed conflict, never an unhandled 500; the save is
                # unchanged because command failures discard the
                # envelope.
                raise GameError(
                    "ledger_conflict",
                    "a money posting key was reused with different "
                    f"details: {exc}", 409) from exc

            env["revision"] += 1
            state = self._state(env)
            self._record_replay(env, request_id, action, payload, state)
            return state

    # ------------------------------------------------------------- actions

    def _cmd_age_up(self, env):
        from src.game import coordinator
        player = env["player"]
        before = (player.happiness, player.health, player.smarts,
                  player.looks)
        try:
            completed = coordinator.run_turn(env, self, random)
        except GraphError as exc:
            raise GameError(exc.code, exc.message, exc.status) from exc
        if env["status"] != "dead":
            after = (player.happiness, player.health, player.smarts,
                     player.looks)
            labels = ("happiness", "health", "smarts", "looks")
            changes = {label: new - old for label, old, new in
                       zip(labels, before, after) if new != old}
            self._journal(env, "year",
                          f"{player.firstname} turned {player.age}.",
                          [player.person_id], changes)
            if completed:
                # the year ran to completion: a fresh turn begins on the
                # next age_up; the projected cursor always names a valid
                # phase (never null)
                env["turn"] = {"cursor": {"phase_id": "year_start",
                                          "item_cursor": 0}}

    def _cmd_resolve(self, env, payload, request_id):
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
        if "kind" in pending:
            # rule-owned pending (M2): resolve through C's boundary with the
            # stored pre-sampled outcomes; never the M1 template path
            return self._resolve_rule_pending(env, pending, option_id,
                                              payload.get("inputs"),
                                              request_id)
        player = env["player"]
        person_ids = list(pending["person_ids"])
        if option.get("_person_ids_extra"):
            person_ids += [i for i in option["_person_ids_extra"]
                           if i not in person_ids]
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
                # the ledger is the single authority for cash: post the
                # decision's money effect once (idempotent per
                # decision/option), never mutate directly
                from src.game.finance import Ledger
                ledger = Ledger(env, resolver=self._make_resolver(env))
                ledger.post(env["turn"].get(
                    "turn_id", f"t_{env['revision']}"), player.age, {
                    "phase": "decision_offer",
                    "occurrence_id": f"{pending['id']}:{option['id']}",
                    "account_owner_id": player.person_id,
                    "category": "other",
                    "amount": amount,
                    "description": f"Decision: {option['label']}",
                })
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
        # M1 template decisions can also interrupt a year (sampled in
        # decision_offer): resume the persisted continuation until the
        # next decision or real year_close
        if (env["status"] == "alive"
                and env.get("turn", {}).get("interrupted")):
            from src.game import coordinator
            completed = coordinator.run_turn(env, self, random)
            if completed:
                env["turn"] = {"cursor": {"phase_id": "year_start",
                                          "item_cursor": 0}}

    def _cmd_continue_as(self, env, payload):
        """M2c succession: continue as one of the deceased's children.
        C supplies the transfer/gameplay instructions; A performs the
        identity-preserving graph conversion and ledger postings."""
        from src.menus import rule_actions
        from src.game.context import RuleContext
        from src.game.finance import Ledger
        from src.people.classes.parent import Parent
        from src.people.classes.sibling import Sibling
        from src.people.classes.person import Person
        from random import randint
        from src.lifesim_lib.lifesim_lib import (
            Gender, round_stochastic)

        if env["status"] != "dead":
            raise _conflict("game_over", "this life has not ended")
        player = env["player"]
        target_id = payload.get("target_id")
        if not isinstance(target_id, str):
            raise _invalid("target_id is required")
        eligible = rule_actions.eligible_successors(player)
        child = next((c for c in eligible if c.person_id == target_id),
                     None)
        if child is None:
            raise _not_found("unknown or ineligible successor")
        instructions = rule_actions.succession_instructions(
            player, child, random)
        turn_id = f"t_succ_g{instructions['generation']}"
        ledger = Ledger(env, resolver=self._make_resolver(env))
        # C's instructions carry share + residual where share uses legacy
        # stochastic rounding (money/num): share + residual == money, but
        # posting +share/-residual nets to 2*share-money (manufactures or
        # drops $1 on odd balances). A owns posting: the estate debit is
        # the share (residual stays on the deceased's archived account —
        # never manufactured, never dropped); books balance exactly.
        entries = []
        estate_residual = instructions.get("estate_residual", 0)
        for entry in instructions["money_entries"]:
            if entry["category"] == "estate_close":
                entry = dict(entry)
                entry["amount"] = -(
                    player.money - estate_residual)
            entries.append(entry)
        ledger.post_many(turn_id, player.age, entries)
        env["replay"] = []  # a new life: fresh replay ledger

        # completed-life record + archive the deceased (journal refs stay
        # resolvable through the archive)
        env.setdefault("completed_lives", []).append({
            "generation": env.get("generation", 1),
            "person_id": player.person_id,
            "name": player.name,
            "age_at_death": player.age,
            "net_worth_at_death": player.money,
        })
        if player not in env["archive"]:
            env["archive"].append(player)

        # identity-preserving successor graph conversion (mirrors the
        # legacy convert semantics; person_id/appearance/name preserved)
        successor = Player(child.firstname, child.lastname, child.gender)
        successor.person_id = child.person_id
        from src.appearance.integration import copy_appearance
        successor.appearance = copy_appearance(child.appearance)
        gp = instructions["gameplay"]
        successor.age = gp["age"]
        successor.gender = child.gender
        successor.happiness = gp["happiness"]
        successor.health = gp["health"]
        successor.smarts = gp["smarts"]
        successor.looks = gp["looks"]
        successor.total_happiness = child.total_happiness
        successor.money = child.money  # share already posted to the child
        successor.generation = instructions["generation"]
        if "university" in gp:
            successor.chose_student_loan = True
            successor.uv_years = gp["university"]["uv_years"]
            successor.calc_grades(randint(-5, 10))
        if "job" in gp:
            successor.has_job = True
            successor.salary = gp["job"]["salary"]
            successor.years_worked = gp["job"]["years_worked"]
            successor.salary_years = list(gp["job"]["salary_years"])
            successor.stress = randint(20, 50)
            successor.performance = randint(40, 80)
        successor.change_happiness(-randint(0, 50))

        successor.relations = []
        successor.parents = {}
        successor.children = []
        if child.mother and type(child.mother).__name__ != "Player":
            mother = Parent(child.mother.lastname, child.mother.age,
                            Gender.Female)
            mother.firstname = child.mother.firstname
            mother.appearance = copy_appearance(child.mother.appearance)
            mother.person_id = child.mother.person_id
            mother.update_name()
            mother.health = child.mother.health
            mother.relationship = (randint(90, 100)
                                   - randint(0, min(child.age, 50)))
            successor.relations.append(mother)
            successor.parents["Mother"] = mother
        if child.father and type(child.father).__name__ != "Player":
            father = Parent(child.father.lastname, child.father.age,
                            Gender.Male)
            father.firstname = child.father.firstname
            father.appearance = copy_appearance(child.father.appearance)
            father.person_id = child.father.person_id
            father.update_name()
            father.health = child.father.health
            father.relationship = (randint(90, 100)
                                   - randint(0, min(child.age, 50)))
            successor.relations.append(father)
            successor.parents["Father"] = father
        for other in player.children:
            if other is child:
                continue  # the successor is not their own sibling
            sib = Sibling(other.lastname, other.age, other.gender,
                          other.smarts, other.looks)
            sib.firstname = other.firstname
            sib.appearance = copy_appearance(other.appearance)
            sib.person_id = other.person_id
            sib.update_name()
            import random as _r
            sib.change_relationship(round_stochastic(_r.gauss(0, 8)))
            successor.relations.append(sib)
        if child.partner:
            successor.partner = child.partner
            successor.partner.change_relationship(randint(-10, 10))
            successor.relations.append(child.partner)

        # archive the old life's relations that are not carried into the
        # successor's graph (e.g. the deceased's partner) so every old
        # journal reference stays resolvable
        carried = {p.person_id for p in successor.relations}
        carried.add(successor.person_id)
        for relation in list(player.relations):
            if relation.person_id not in carried \
                    and relation not in env["archive"]:
                env["archive"].append(relation)
        env["player"] = successor
        env["status"] = "alive"
        env["pending"] = None
        env["turn"] = {"cursor": {"phase_id": "year_start",
                                  "item_cursor": 0}}
        env["year_actions"] = {"spend_time": []}
        env["dead_ids"] = [pid for pid in env.get("dead_ids", [])
                           if pid != successor.person_id]
        env["generation"] = instructions["generation"]
        # M2.5: the primary-relationship projection follows the successor
        primary = None
        for rel in env["graph"]["relationships"]:
            if rel["status"] == "active" \
                    and successor.person_id in rel["person_ids"]:
                primary = rel["relationship_id"]
                break
        env["graph"]["primary_relationship_id"] = primary
        self._journal(
            env, "succession",
            f"{successor.name} continues the family legacy "
            f"(generation {successor.generation}).",
            [successor.person_id])

    def _apply_event_death(self, env, death):
        """Single graph transition for event-driven deaths (#3):
        archive/remove/mark-dead exactly once, preserving journal refs.
        `cause` is the stored outcome — never resampled."""
        pid = death.get("person_id")
        person = None
        for candidate in env["player"].relations:
            if candidate.person_id == pid:
                person = candidate
                break
        if person is None or pid in env.get("dead_ids", []):
            return  # already transitioned (replay/idempotence)
        env["player"].relations.remove(person)
        env["dead_ids"].append(pid)
        if person not in env["archive"]:
            env["archive"].append(person)
        if env["player"].partner is person:
            for c in env["player"].children:
                if c.father is person:
                    c.father = None
                elif c.mother is person:
                    c.mother = None
            env["player"].partner = None
        if type(person).__name__ == "Child" and person in env["player"].children:
            env["player"].children.remove(person)
        # M2.5 graph: end active edges, cancel the deceased's pending
        # offers; history edges stay for the tree
        from src.game.features.family_graph import apply_death
        apply_death(env["graph"], pid, env.get("revision", 0))
        self._journal(env, "death",
                      death.get("announcement")
                      or f"{person.name} {death.get('cause', 'passed away')}.",
                      [pid])

    @staticmethod
    def _per_command_entries(entries, request_id):
        """Command-driven money postings are one occurrence per COMMAND,
        not per turn: some actions can succeed several times within a
        single year (ask_for_money grants), and a constant occurrence id
        would reuse the posting key with different amounts (LedgerConflict
        -> the typed 500 the robot found, crash_id e1b1a7e5326b). The
        request_id suffix keeps each command unique while a retried command
        never reposts: the replay log answers it before re-execution."""
        if not entries:
            return entries
        return [dict(entry,
                     occurrence_id="%s#%s" % (entry["occurrence_id"],
                                              request_id))
                for entry in entries]

    def _cmd_rule_action(self, env, action, payload, request_id):
        from src.game.context import RuleContext
        from src.game import rule_adapter
        from src.menus import rule_actions
        ctx = RuleContext(env, self)
        try:
            result = rule_actions.apply_action(ctx, action, payload, random)
        except (rule_actions.RuleError, GraphError) as e:
            status = 409 if e.code in ("not_allowed", "refused", "not_adult") \
                else 404 if e.code == "not_found" else 400
            raise GameError(e.code, e.message, status)
        turn_id = env["turn"].get("turn_id", f"t_{env['revision'] + 1}")
        rule_adapter.post_money_entries(
            env, self, turn_id, env["player"].age,
            self._per_command_entries(result.get("money_entries"),
                                      request_id))
        rule_adapter.journal_events(env, result.get("events"))
        if "pending_decision" in result:
            env["pending"] = result["pending_decision"]
            if env["pending"] is not None:
                env["pending"]["id"] = f"d_{env['next_decision_num']}"
                env["next_decision_num"] += 1

    def _resolve_rule_pending(self, env, pending, option_id, inputs,
                              request_id):
        from src.game import coordinator
        from src.game import rule_adapter
        from src.game.context import RuleContext
        from src.menus import rule_actions
        ctx = RuleContext(env, self)
        try:
            result = rule_actions.resolve_choice(
                ctx, pending, option_id, inputs, random)
            _pregnancy_continues = (
                getattr(env["player"], "is_pregnant", False)
                or getattr(getattr(env["player"], "partner", None),
                           "is_pregnant", False))
            if pending.get("kind") == "pregnancy" and _pregnancy_continues:
                # E4/N1 (A-owned seam): a pregnancy that continues records
                # its conception ONCE here on the service side (never in
                # C's resolver), so the birth transaction consumes
                # exactly one immutable draw. Both carrier branches count
                # (the player herself OR her pregnant partner). Runs
                # inside _GameRNG, so the draw rides the seeded gameplay
                # stream.
                from src.game.features.parenting import core as \
                    _parenting_core
                _parenting_core.record_legacy_conception(
                    env, env["player"], random,
                    pregnancy_id=pending.get("id"))
        except (rule_actions.RuleError, GraphError) as e:
            status = 409 if e.code in ("not_allowed", "refused", "not_adult") \
                else 404 if e.code == "not_found" else 400
            raise GameError(e.code, e.message, status)
        turn_id = env["turn"].get("turn_id", f"t_{env['revision'] + 1}")
        rule_adapter.post_money_entries(
            env, self, turn_id, env["player"].age,
            self._per_command_entries(result.get("money_entries"),
                                      request_id))
        rule_adapter.journal_events(env, result.get("events"))
        for death in result.get("deaths") or []:
            self._apply_event_death(env, death)
        if result.get("finished_turn"):
            # terminal rule resolve (surrender): A performs the death;
            # no later phases run (only year_close/estate work remains)
            env["status"] = "dead"
            env["dead_ids"].append(env["player"].person_id)
            env["turn"]["cursor"] = {"phase_id": "year_close",
                                     "item_cursor": 0}
        # a resolve may carry the NEXT stored decision (e.g. university
        # application resolve returns the funding offer) — persist it
        env["pending"] = result.get("pending_decision")
        if env["pending"] is not None and "id" not in env["pending"]:
            env["pending"]["id"] = f"d_{env['next_decision_num']}"
            env["next_decision_num"] += 1
        # multi-death years: serve the next queued funeral immediately
        # (unless the resolve itself carried the next decision)
        if pending.get("kind") == "funeral" and env["pending"] is None:
            queue = env["turn"].get("funeral_queue", [])
            if queue:
                env["pending"] = queue.pop(0)
                env["pending"]["id"] = f"d_{env['next_decision_num']}"
                env["next_decision_num"] += 1
        # resume the interrupted year from its persisted continuation
        # (same turn id/done markers/snapshot) until the next decision or
        # real year_close
        if (env["pending"] is None and env["status"] == "alive"
                and env.get("turn", {}).get("interrupted")):
            from src.game import coordinator
            # A birth can resume here after a stored choice. Preserve the
            # same typed guard errors as direct age_up, with full rollback.
            try:
                completed = coordinator.run_turn(env, self, random)
            except (rule_actions.RuleError, GraphError) as e:
                status = 409 if e.code in ("not_allowed", "refused", "not_adult") \
                    else 404 if e.code == "not_found" else 400
                raise GameError(e.code, e.message, status) from e
            if completed:
                env["turn"] = {"cursor": {"phase_id": "year_start",
                                          "item_cursor": 0}}
