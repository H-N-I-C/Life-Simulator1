# M1 draft — real life loop in the browser

Status: draft for Claude approval. Process: ../00-PROCESS.md. M0 must pass both reviews before this work starts; plan against its final reviewed scaffold, not an in-progress snapshot. No implementation authorized by this document alone until Claude assigns tasks.

## Outcome and scope

`./play-web.sh` opens a real saved game: create a character with the M0 creator, be born into a generated family, view stats and parents with portraits, interact with a parent, advance years, resolve contextual decisions, and resume after a server restart. A real simulation replaces fixture data on game pages. Keep gallery and demo routes separately labeled.

This is a vertical slice, not full legacy parity. Reuse Person/Player and appearance models; extract a small, explicit subset of rules into an I/O-free application boundary. Do not implement a second unrelated game model or blindly call the existing interactive `Player.age_up`/`random_events`. M2 ports remaining education, career, romance, finance and succession behavior. M1 supports aging across the lifespan with basic health/death handling and a completed-life screen; disclose that later-life systems arrive in M2. No age cap that silently strands a save. Country remains background only; creator age slider is preview only and new games start at zero.

## Playable behavior

- Creator retains JSON export and adds Start life. Name/gender/background country and validated appearance reach the real Player. Manual appearance wins over constructor-generated appearance. Server owns stats, money and eligibility.
- `/game` shows name/portrait, age, four core stats, cash, chronological journal, parents and available actions. All person references use stable IDs plus the shared portrait component. M1 parent interaction: spend time together, with age-appropriate text, limited to once per parent/year and a small bounded relationship/happiness effect. Reuse existing relationship semantics where possible and document any subset adjustments.
- Age advances exactly once per successful command, ages living family members, resets annual action limits, updates basic health/stats and records a structured year event. Extract/reuse the relevant safe existing rules; document differences and the exact source rules ported. Basic death transitions end actions cleanly. Avoid calling legacy methods that can invoke terminal input indirectly.
- At least six original contextual decision templates spanning child/teen/adult/elder, with two or more meaningful choices; at least one available in the early childhood smoke journey. Visible choices have deterministic defined effects; no arbitrary cash minting. Every annual turn has a journal entry even when no decision triggers. Content depth expands in M2; 50 events is not an M1 gate.
- Pending decisions block unrelated gameplay actions. Age is already advanced when its decision appears; resolving it applies only its choice effects, never repeats the year's core effects. Persist pending decision/options and sampled outcomes so reload cannot reroll them. Completed life retains history and permits starting a new life without deleting older saves.

## Application/data contract (freeze before parallel implementation)

A owns Python application/rules/persistence; B owns HTTP/frontend. Imports must use canonical `src.appearance...` / `src.people...` paths from the repository root: the current server's `appearance` alias must not create duplicate model classes for pickle/type checks.

`src/game/service.py`: `GameService(save_dir)` exposes `new_game(profile, request_id)`, `list_games()`, `get_state(game_id)`, `command(game_id, expected_revision, request_id, action, payload)`. Methods return plain JSON-compatible dictionaries; errors are typed `GameError(code, message, status)`. Constructor accepts a temporary save directory for tests. No web dependency. Profile: first_name, last_name, gender (explicit adapter to current enum), country, appearance; optional deterministic seed is a test/service capability, not an unexplained normal-play control.

State envelope:
```
{schema_version: 1, game_id, revision, status: "alive"|"dead",
 player_id, people: [{id, name, role, age, alive, appearance, relationship?}],
 stats: {happiness, health, smarts, looks}, money, country,
 journal: [{id, age, kind, text, person_ids: [], changes: {}}],
 available_actions: [{id, label, target_id?}],
 pending_decision: null | {id, title, text, person_ids: [],
   options: [{id, label, known_effects: []}]}}
```
`action` is `age_up`, `spend_time` (`target_id`) or `resolve_decision` (`decision_id`, `option_id`). Server recalculates eligibility. No raw Python objects, file paths or random state go into public JSON. B can render fallback error messages but cannot invent client-side simulation results.

IDs for people/events/decisions are stable and unique within a game, persisted, and independent of names and appearance seeds. Store person identifiers on canonical people or an explicit persisted identity mapping; don't recreate them per response. Use structured event references, never regex matching names in prose.

Each game has independent persisted simulation RNG state. Appearance uses its own RNG. Existing legacy helpers use global `random`; refactor reached helpers to accept a dedicated RNG, or use a strictly locked, restore-in-finally compatibility adapter for a limited documented slice. Never let different games interleave a shared RNG or leak changes to unrelated gameplay. Restore RNG and state on rejected/failed commands.

## HTTP contract (B)

- GET `/api/games` → `{games: [{game_id, name, age, status, revision}]}`.
- POST `/api/games` → profile fields plus `request_id`; returns state, HTTP 201 (replay may return 200).
- GET `/api/games/<id>` → state.
- POST `/api/games/<id>/actions` → `{expected_revision, request_id, action, payload}`; returns updated state.
- Error: `{error: {code, message}}`, with 400 invalid input, 404 unknown game/target, 409 stale revision/pending decision/conflicting replay, 500 unexpected failure without traceback/path exposure.

Duplicate request IDs with identical payload return the original successful response without applying effects twice, including after restart. Reusing an ID for a different payload is a conflict. Check known replay before stale revision. Persist a bounded replay ledger with a documented retention policy; an expired old request must fail stale rather than reapply. Creation also needs replay protection across double-click/restart. Serialize mutations with a service lock compatible with ThreadingHTTPServer. New command publication, RNG state, replay entry and save must be one atomic success; a failed write must not leave memory ahead of disk.

## Persistence and initialization

Use a versioned web-save envelope around the existing model, journal, RNG and pending turn data; keep exact restricted-unpickler protections if using pickle. Prefer plain dict/list envelope fields and existing allowlisted model classes to unnecessary new pickle globals. Atomic replace in a dedicated `game_saves/web/` directory; opaque server-generated IDs map to filenames, never browser-provided paths. Prevent symlink/path escapes. M1 need not import terminal save slots; leave them intact and document that boundary. Include a testable schema migration hook and reject unsupported future versions clearly.

Server imports must never prompt for language. A removes language selection side effects from translation import, keeping English default and an explicit selection function invoked by the terminal entry point. Fix import ordering/cycles properly in reached paths, without permanently patching builtins.input or hiding prompts with captured stdout. Backend rules return events instead of scraping printed text.

Mutating HTTP routes require same-origin application/json requests, validated Host/Origin (exact loopback host and actual port), bounded bodies and clear validation errors. No permissive CORS. Existing `/api/render` remains compatible with the same-origin client. Handle absent Origin explicitly for nonbrowser/test clients under a documented policy; do not accept hostile origins. No state changes on GET.

## Ownership

A: `src/game/**`, reached engine files under `src/people/`, `src/lifesim_lib/translation.py`, `lifesim.py`, narrowly necessary safe-save changes, `tests/test_game_*.py`. Do not edit M0 appearance modules while their fixes are active; coordinate any necessary contract change with Claude.

B: `web/**`, `play-web.sh`, `tests/test_web_*.py`, browser smoke artifacts. Uses reviewed M0 renderer/shared person chip. No engine/save edits.

Shared contract is this document; A provides fixtures/examples FIRST in `docs/collab/M1/api-examples.json`. A and B each own their report. Coordinate changed API through Claude before implementation; no unilateral signature drift. Do not rewrite M0 tests or bootstrap to make new tests pass; report a required shared-test change to Claude.

## Acceptance/play script

1. Start with closed stdin; open creator, customize face/name, Start life, verify age zero and chosen appearance.
2. View real parents, spend time, verify journal/stat/relationship change and annual repeat rejection.
3. Age into an early childhood decision, verify age increased once; reload while pending, resolve, retry same request, verify effects once.
4. Close/restart server, choose saved life, verify exact appearance/IDs/history/limits/decision state.
5. Open same game in two tabs, submit conflicting revisions, verify clean conflict and refreshed state; submit two independent games without RNG cross-talk.
6. Use a valid test scenario near old age, reach death, verify actions stop and record stays readable. No normal-play “jump age” shortcut.
7. Run A/B tests and M0 regressions; verify keyboard access, narrow viewport, readable errors and unchanged gallery/export functionality.

Review gates: no terminal prompts in startup/import/reached actions; deterministic seeded transitions; eligibility and range bounds; decision/replay durability; atomic save failure; unsupported/corrupt save handling; path/origin rejection; lifespan/death; portraits and stable person references. Both reports include actual commands/output, limitations and manual browser evidence where available. Codex then Claude review; Claude notifies the user only when ready to test.
