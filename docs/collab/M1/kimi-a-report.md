# M1 Kimi A report — engine/application boundary

## Files changed/created (A scope)

- `src/game/__init__.py`, `src/game/service.py` — GameService (creation,
  list, get_state, command), persistence, replay, locking.
- `src/game/decisions.py` — six original contextual decision templates
  spanning child/teen/adult/elder with deterministic bounded effects.
- `src/lifesim_lib/translation.py` — import-time language prompt removed;
  explicit `select_language()` (TTY only, EOF-safe English default).
- `src/people/classes/partner.py` — import cycle fixed properly (bottom
  `from ... player import Player` removed; `compatibility_check` duck-types
  `karma`). Player can now be imported first; no monkeypatching anywhere.
- `lifesim.py` — calls `translation.select_language()` before other imports.
- `tests/__init__.py` — reduced to a docstring (no prompt/stub needed).
- `tests/test_game_service.py` — 21 service tests.
- `docs/collab/M1/api-examples.json` — contract examples (published first,
  updated once for converged error codes).

## Legacy rule mapping (extracted vs deferred)

Extracted/reused (I/O-free): Player/Person/Parent construction and M0
appearance inheritance; `Person.age_up` stat drift (happiness/health/smarts/
looks ±3, teen looks ramp, post-50 looks decay); parent relationship drift
(+1/year while the player is under 18, else -1/-1/0, from `Player.age_up`);
`Person.death_check` for player and relations; the "Spend time" enjoyment
formulas from `src/menus/main.py` (refusal below relationship 15 at -4
happiness; `max(randint(0,70),randint(0,70))+randint(0,30)` player enjoyment,
`triangular(0,100,relationship)` for the relative; gains enjoyment/12;
CHEERFUL/GRUMPY modifiers when the trait exists; once per parent per year).

Re-implemented (not called): yearly orchestration, journaling, relation
death removal, decision sampling, death transition — because
`Player.age_up`/`Player.random_events` print and prompt for terminal input.

Deferred to M2: school, university, jobs/income, romance/pregnancy/children
as gameplay, illnesses, traits UI, plastic surgery, legacy random_events
content, terminal-save import. Money changes only through defined decision
effects (no arbitrary minting).

## How B initializes/calls the service

```python
from src.game.service import GameService, GameError
service = GameService(save_dir)            # game_saves/web/
state = service.new_game(profile, request_id)   # state dict + "created" bool
listing = service.list_games()                  # {"games": [...]}
state = service.get_state(game_id)
state = service.command(game_id, expected_revision, request_id, action, payload)
```

Errors are `GameError(code, message, status)`; `as_dict()` for HTTP bodies.
State envelope and error shapes match `docs/collab/M1/api-examples.json`
(codes: `invalid_input` 400, `not_found` 404, `stale_revision`/`conflict`/
`pending_decision`/`not_allowed`/`game_over`/`unsupported_schema` 409,
`internal_error` 500).

## Contract convergence with Kimi B (server/tests)

Aligned on: replay returns the byte-identical stored response (no marker);
error codes `conflict`, `not_allowed`, `invalid_input` for a bad option;
creation revision is 1; person IDs are service-generated `p_01…` (read from
state, never hardcoded); roles are `mother`/`father`; `available_actions` is
`[]` while a decision is pending (use `pending_decision`). B's web tests pass
against the real service. `web/stub_service.py` should now be deleted by B.

## Verification

- `python3 -m unittest discover -s tests -t .` — **81 tests, OK**
  (37 M0 appearance + 21 service + 23 web HTTP).
- Closed-stdin subprocess import check: no prompts, no output.
- `./play-web.sh` smoke: `POST /api/games` created a real game (rev 1,
  people p_01/p_02/p_03) through `src.game.service`; M0 creator/gallery and
  `/api/render` untouched and passing.
- Determinism: same seed → identical stats/people across games and across
  interleavings; global `random` state unchanged by all commands.
- Exactly-once: replay (incl. creation) returns the original response after
  restart; conflicting request_id → 409; failed saves roll back (read-only
  dir test); corrupt saves → 500 without internals; unsupported schema → 409;
  symlink/path-escape IDs rejected.
- Lifespan: seeded full life reaches death; actions stop (409 `game_over`);
  journal/history readable after restart.

## Limitations

- M1 is a vertical slice: no income/careers/romance/children systems (M2);
  parent death uses legacy `death_check`; siblings/partners never occur in
  web games yet.
- Replay ledger retained per game (last 100 commands); expired ids fall
  through to the stale-revision check rather than reapplying.
- Saves are pickle envelopes behind the restricted unpickler (same
  protections as terminal saves); terminal save slots are untouched.

## Fix round 1 (M1 Codex review 8 findings + M0 re-review 2 + 2 flaky tests)

1. **Creation atomicity with durable replay.** The game envelope is now the
   single authoritative record: it carries `creation.request_id`,
   `fingerprint` and the original `response`. The JSON index is a rebuildable
   cache (with a scan-the-envelopes fallback after a crash). Ledger write
   failure rolls the game file back and raises a typed `internal_error`;
   corrupt ledger fails closed (typed 500, no duplicate creation). Tests:
   injected failure at the ledger boundary, restart + retry (exactly one
   life), corrupt ledger, tombstone expiry.
2. **Creation replay returns the original response** (revision 1 state even
   after later actions), not the latest state. Evicted creation entries
   become persisted tombstones; replaying an expired creation id fails 409
   `stale_revision` instead of silently creating a second life.
3. **Dead people stay in public history.** Relations that die are moved to a
   persisted `archive`; `_state` projects them with `alive: false` and their
   appearance. Full-life seeded probe: every journal `person_id` resolves in
   `people` before and after death and across restart.
4. **Paid decisions are gated and journals are honest.** Unaffordable
   options are rejected authoritatively (409 `not_allowed`, state unchanged).
   Decision journal `changes` are computed from actual before/after values
   (capped stats/relationships and clamped money included). `known_effects`
   of all six templates now match their real effects (e.g. "Say no" shows
   `-happiness`). Tests: zero-funds rejection, no false money delta,
   capped-stat delta accuracy.
5. **Cosmetic isolation and seed reproduction.** The cosmetic RNG is a
   process-global spare driven from each game's own persisted
   `cosmetic_rng_state` (initialized once at creation; never consumes the
   simulation stream afterwards). `player.py` no longer imports `_APP_RNG`
   by value — it reads `integration._APP_RNG` at call time, so the adapter's
   swap reaches every generator. The global-random compatibility lock is a
   module-level lock shared by all GameService instances. Same-seed games
   without a manual appearance now reproduce the player appearance exactly.
6. **Typed errors for malformed input.** Envelope structure is validated
   after loading (required keys/types); malformed supported saves raise
   `internal_error`, never a raw `KeyError`. Profile `appearance` with an
   unsupported `schema_version` raises 400 `invalid_input`. A
   `_migrate_envelope` hook exists for future older versions.
7. **Flakes fixed.** Inherited marks are deduplicated at generation time
   (`generate.py`), so valid generated data satisfies round-trip invariants
   (deterministic repro: parents with all five marks, 200 seeds). B's
   `test_spend_time_once_per_year` no longer asserts past the 100
   relationship cap — it checks the journal's actual recorded gain.
8. **Age/context eligibility.** "Spend time" narration is age-staged
   (baby/child/teen/adult/elder places). `family_reunion` is skipped when no
   parent is alive; `sample()` now actually uses `alive_parent_ids`.

**M0 re-review 2:** `_finalized_parent_ids` (process-local `id()`) replaced
by `_finalized_parent_keys` — stable persisted `(firstname, lastname,
appearance.seed)` tuples — so saved children never reroll after reload.
Regression tests: full-save pickle round-trip (no stripping) with parent
re-attachment, plus marks-dedupe.

**Test process note:** per the updated 00-PROCESS.md, only focused tests run
during the milestone; the two formerly-flaky tests were run under
PYTHONHASHSEED 0/3/7/42/123 (all pass).

Current state: focused suites green; full suite 104 tests OK (run once
before the updated process rule); terminal game and `./play-web.sh` both
launch against the real service.

## Fix round 2 (M1 re-review 1: four residual issues + M0 person identity)

**Immutable person ID (M0 + API).** Every `Person` now gets `Person.ID`, a
uuid4 assigned at creation (os.urandom-backed — never consumes gameplay RNG),
persisted with the object, and backfilled cycle-safely for legacy saves
(`integration.ensure_person_id`, called from migration, child finalization
and the web loader). It is the single canonical identity: child
finalization metadata stores parent IDs, and the web API's person `id`
(field `player_id`, `target_id`, journal/decision `person_ids`, `dead_ids`)
is now `Person.ID` itself (the old sequential `web_id`/`next_person_num`
scheme is gone). No names, cosmetic seeds, or process addresses identify
anyone. New M0 tests: parent rename before/after save, two distinct parents
sharing name AND appearance seed, unchanged reload, second-parent
construction.

**Creation expiry actually works.** The eviction loop had converted the
oldest entry to a tombstone and then immediately deleted that tombstone in
the same loop. Now ACTIVE entries are capped (500); tombstones persist as
durable request ownership, so an expired creation id fails 409
`stale_revision` after the real 501st insertion, across restart, for both
identical and conflicting payloads (test exercises the real eviction, not a
fabricated ledger). Documented nuance: if the index file itself is lost in a
crash before its first write, the envelope scan treats the creation as live
— exactly-once is still preserved (no second life is created).

**Interaction journals record actual deltas.** `_cmd_spend_time` measures
before/after for both the refusal branch (previously always logged -4 even
when clamped to 0) and the enjoyment branch, including the CHEERFUL bonus;
zeros are omitted, matching decision logging. Decision relationship effects
apply to every target and report the summed actual delta. B's
`test_spend_time_once_per_year` now compares the journal to the actual state
delta instead of asserting a positive gain.

**Complete safe validation.** `_validate_envelope` covers every field the
service reads (country, pending shape, year_actions, dead_ids, archive,
used_decisions, journal/replay entry shapes, creation, player fields) and
validates both RNG states with a local `random.Random` before any global
swap or lock — a bad state can no longer strand the compatibility lock.
Missing/empty profile `country` is a typed 400. Load-time backfill assigns
person IDs and appearances before projection. Tests parameterized over
missing/wrongly-typed fields, each followed by a successful valid request.

Focused tests: `tests.test_appearance` 37 OK, `tests.test_game_service` +
`tests.test_web_m1` 59 OK. Terminal game and `./play-web.sh` launch.

## Fix round 2 addendum (reviewer-requested adjustments)

- **Creation retention limit is injectable:** `GameService(save_dir,
  creation_replay_limit=500)`; the eviction test now uses a limit of 5 and
  exercises the real 6th-insertion eviction in milliseconds (full replay
  class: 5 tests in 0.13s).
- **Skin tone gradient:** the 12 skin tones are re-graded into a strictly
  light-to-dark sequence with clearly distinct neighbours (Warm Olive moved
  to the middle of the scale, Ebony now darkest). IDs unchanged. New focused
  test asserts strictly decreasing luminance (>4.0 steps) and minimum RGB
  neighbour distance (>18) so this cannot regress.

## Fix round 3 (M1 re-review 2: validation, v1 identity migration, legacy parent metadata, successor IDs, flaky test)

1. **Complete nested validation.** `_validate_envelope` now enumerates
   required fields for every consumed record: pending (id/title/text/
   person_ids/options with id/label/known_effects + private shapes), journal
   (id/age/kind/text/person_ids/changes), replay (request_id/action/payload/
   response), creation, year_actions, player attributes, plus documented
   optional-field defaults (ledger/balances/generation) normalized in place.
   No `.get`-then-index gaps; AttributeError is caught; RNG states are
   probed before the global swap/lock. Table-driven missing/wrong-type tests
   verify a subsequent valid command still works.
2. **v1 → v2 identity migration.** Envelope schema is now 2. `_migrate_v1_web_ids`
   builds an old web_id → canonical person_id mapping from the real
   graph/archive, remaps journal/dead_ids/spent-time/pending AND the
   creation/replay response snapshots, rejects dangling references with a
   typed 500, and persists the migrated envelope atomically once — repeated
   read-only loads are ID-stable. New-format API ids remain canonical UUIDs.
3. **Legacy parent metadata.** Migration normalizes `_finalized_parent_ids`
   for EVERY established child: numeric id() sets and (name, seed) tuples
   are rebuilt from canonical current parent IDs without re-inheriting —
   loaded children never reroll (real pickle round-trip tests for both old
   formats).
4. **Successor conversion keeps canonical identity.** Person identity moved
   to a dedicated `person_id` attribute (distinct from `Player.ID`, the
   legacy save-slot id — no conflation, save-path behavior unchanged).
   `convert_child_to_player` copies the successor's person_id and the
   reconstructed relatives' person_ids; assertions added alongside the
   name/appearance checks.
5. **Flaky adult-styling test fixed.** The bald rejection (a valid adult
   catalog choice) was replaced with a spy asserting migration passes the
   child's real age to inheritance; verified alone under PYTHONHASHSEED
   0/3/7/42/123.

Also: `GameService(creation_replay_limit=...)` validates a non-negative
integer (negative limits could loop forever).

Focused suites: test_appearance 39 OK; service+finance+web+render 82 OK.

## Fix round 4 (M1 re-review 3 closers)

1. Validator/resolver agreement: pending options now REQUIRE `_effects`
   (dict) and `_outcome` (str) — the fields the resolver consumes — so a
   restricted-loadable pending without them fails validation with a typed
   500 instead of a raw KeyError at resolve time. Mutation-table test added.
2. v1→v2 migration now remaps `pending_decision.person_ids` inside
   creation/replay response snapshots (top-level pending was already
   covered). Fixture test: replay snapshot with p_XX pending references →
   remapped to canonical person_ids resolving in the current graph.

Focused suites: test_game_service 37 OK; appearance+finance+web also green.
