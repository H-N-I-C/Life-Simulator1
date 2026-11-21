# Kimi A — feel-pass round 2 report (Codex Review #2 follow-up)

Requested path/filename: docs/collab/redesign-v2/kimi-a-feel-round2-report.md.

Date: 2026-11-20. No commit. Scope: outcome privacy hardening at the
projection (Codex feel-eval follow-up), on top of the round-1 items
(F4/F2 server side, decision outcome, F9 avatar defaults, pending
known_costs — see kimi-a-feel-report.md and contracts.md §Journal v2 /
§Decision outcome / §Avatar defaults / §Choice costs v2).

## Finding addressed

Codex Review #2 (provisional accept) flagged that `last_decision_outcome`
was serialized raw from the envelope while journal/feed pass
`knowledge_journal_visible`, and that the rule-path capture copied the
first resolver event without checking `audience`/`fact_id`.

## Fix (defense in depth, both layers)

`src/game/service.py`:

1. **Capture (`_resolve_rule_pending`)** — the leading outcome event is
   captured only when it passes the SAME `knowledge_journal_visible` gate
   as journal serialization; otherwise the field is set to `null`. The
   event's visibility metadata (`audience`, `fact_id`) is persisted ON the
   stored outcome.
2. **Projection (`_state` → new `_visible_outcome`)** — the field is
   re-filtered at serialization with the same gate from the stored
   metadata (no metadata = public); internal `audience`/`fact_id` keys are
   stripped and never leave the server. A legacy/stored outcome with
   private metadata is re-filtered even at read time.
3. M1 path additionally emits per-target `relationship_changes`
   `{person_id: actual delta}` (rule path already forwarded its map).

Contract: §Decision outcome updated (capture gate + projection re-filter +
metadata stripping + per-person relationship map).

## Tests (forced private sentinel, real command + GET paths)

`tests/test_game_v2_journal.py` `OutcomePrivacyTests` (7) — synthetic rule
pending persisted and resolved through the REAL `resolve_decision` command
with a stubbed resolver; assertions cover BOTH the command response state
and the `get_state` (GET /api/games/{id}) projection:

- `audience: "private"` outcome → `last_decision_outcome` null in command
  AND GET; secret text absent from both journals.
- `player_known` without `fact_id` → null everywhere (never visible).
- stored private-metadata outcome re-filtered to null at read time.
- public outcome → captured with actual changes; metadata keys absent.
- public with `relationship_changes` → per-person map preserved.
- no outcome events → null.

## Checks run

- `pytest tests/test_game_v2_journal.py tests/test_game_v2_avatar_defaults.py`
  → 21 passed.
- `pytest tests/test_game_m2a.py tests/test_game_scenario_retry.py`
  → 37 passed, 3 subtests.
- (TMPDIR under the workspace; no broad suites per instruction.)

## Notes for Codex

- Adapter cost plumbing / event.js cost+relationship rendering / outcome
  lifecycle gating remain B/C areas; the server shapes they consume are
  now: preserved authored `known_costs` (label+amount) + derived
  `{kind:"money",amount}` for event/M1 options; `last_decision_outcome`
  with per-person `relationship_changes`, privacy-gated.
