# Kimi A — feel-pass report (engine: F4 server side, F2 server side, decision outcome)

Date: 2026-11-20. Scope per feel-pass.md: F4 server side (journal feed), F2
server side (no internal strings in player text), and the actual-deltas
outcome on decision resolve for C's popup. Shapes were published FIRST in
`docs/collab/redesign-v2/contracts.md` as **§Journal v2 (FROZEN v1)** and
**§Decision outcome (FROZEN v1)**.

## Contract shapes (now in contracts.md)

- `_state` gains `journal_feed`: `[{"age": N, "header": "Age N",
  "entries": [<same entries as flat journal>]}]` — a pure, derived regrouping
  of the same player-known-filtered entries; flat `journal` UNCHANGED
  (frozen §Home rendering unaffected). Blocks oldest → newest, one per age.
- `_state` gains `last_decision_outcome`: `null | {decision_id, option_id,
  text, person_ids, changes, relationship_changes?}` — actual applied deltas
  only, set by every successful `resolve_decision` (M1 template path AND
  C's rule path), replaced per resolve, never invented/cleared by other
  commands. C shows the popup straight from the resolve response.
- F2 server side: journal/feed texts, pending title/text, option
  labels/reasons never contain `t_\d{6}` / phase names / cursor keys;
  machine fields (`turn`, `finances.last_closed_turn`) stay dev-only.

## Engine changes

- `src/game/service.py` — `_journal_feed()` helper (derived blocks);
  `_state` exposes `journal_feed` + `last_decision_outcome` (env.get, so
  legacy saves return `null`, never KeyError); `_cmd_resolve` records the
  M1 outcome (actual before/after deltas incl. money/relationship);
  `_resolve_rule_pending` captures the resolver's leading outcome event
  (changes + relationship_changes passthrough).
- `src/game/coordinator.py` — `_relations_age` now journals first-person
  relationship lines when yearly parent drift actually moved the value
  ("My relationship with X improved/worsened.", kind `relationship`,
  person-tagged, actual delta in `changes`). Events/aging/school lines
  already journaled via existing paths (events pipeline, `"year"` aging
  line per lived year incl. interrupted years, education transitions).

## Tests (forced, 3 seeds: 7, 4242, 90210)

New `tests/test_game_v2_journal.py`, 4 tests:
1. Feed blocks per age with aging line — per seed: ascending unique ages,
   `header == "Age N"`, every lived year has a `kind:"year"` line, feed
   entries byte-equal the flat journal.
2. Relationship lines from yearly drift (under-18 parent +1 drift).
3. Outcome-on-resolve: null before resolve and after age_up, populated
   after resolve with option/decision ids, first-person text equal to the
   journaled decision line, and every `changes` delta verified against the
   real before/after of that command.
4. F2 purity scan to age 30 per seed over all player-facing strings.

Results:
- `tests.test_game_v2_journal` (unittest): 4/4 OK.
- pytest `test_rules_event_loader.py + test_game_v2_journal.py`: 15 passed.
- pytest `test_game_m2a.py + test_game_v2_person.py +
  test_game_v2_journal.py`: 47 passed.
- `test_game_m25_conception.py`: 4 failures remain — PRE-EXISTING
  (baseline without my changes fails 13 in that file; the 4 are a subset).
  Not caused by this work; left untouched.

Note for the team: /tmp on this box intermittently hits per-user quota
(concurrent browser-burst dirs); run engine tests with
`TMPDIR=<workspace>/.test-tmp` to avoid flaky EDQUOT save failures.

## Handoff

- B: render `journal_feed` as the BitLife scroll feed (header per block,
  auto-scroll to newest); F2 web side (the "This year (t_000003)" /
  "Current phase" chrome) is yours — server player text is now pinned
  clean by test 4.
- C: `last_decision_outcome` on the resolve response is the popup's
  post-choice payload (text + actual deltas); contract in §Decision
  outcome.
