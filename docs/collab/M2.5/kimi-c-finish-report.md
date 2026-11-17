# Kimi C — M2.5 finish report (2026-11-17)

Branch `dev` (verified). Tasks C1, C2, C3 per `docs/collab/M2.5-finish.md`.
QUALITY-GUIDE.md landed mid-task and was applied (production-route tests,
failing-before checks, 3 hash seeds, typed errors, no soft-locks).

## C1 — dating_app end to end: DONE

Fixes (my files only):

1. `src/menus/rule_actions.py` — `ACTION_IDS` now admits `dating_app`, so
   `GameService.command` no longer rejects it (`unknown action`). The
   service allowlist reads `ACTION_IDS` + four tables; dispatch already
   routes `_CONTEXT_SELF_ACTIONS` correctly, so this one admission wire
   is the whole fix. Fails-before verified: with the id removed,
   `GameService.command("dating_app")` raises `GameError invalid_input`.
2. `src/menus/rule_actions.py` (`sample_dating_app`) — the offer is now
   instantiated with the proper `GameContext(env, service)` adapter
   (Codex break #2: `RuleContext` has no `person_name`/`money`, so
   production 500'd/duck-typed). Scoring/draw unchanged: B's
   `evaluate_offer` hard-validity gate, then ONE seeded accept/refuse
   draw from `refusal.p_refuse`, stored in `pending["sampled"]`; a stored
   refusal swaps the accept option's effects/outcome once, never rerolled.
3. `src/game/events.py` — `GameContext` gained a `people_by_id` property
   (same semantics as `RuleContext`'s), so `guard_event_adults` inside
   `instantiate3` sees EVERY bound person — the dating-app target is a
   stranger outside the relation walk, so the guard previously skipped it.

Tests (`tests/test_rules_dating.py`, new `DatingAppServiceTest`, 3 tests):
real persisted `GameService` — command dispatch + pending shape
(incl. `sampled.accepted`/`score`, `interaction.kind`), stored refusal
resolved via `resolve_decision` (refusal text, no graph edge, no reroll),
and accept resolving through the `offer` domain applier at the registry
seam (fake applier, production applier is A3's; `chemistry` faked too
because A's current applier requires an active edge — see PENDING).

- Fails-before: registration break reproduced live (`invalid_input`);
  the context break is Codex's documented repro #2 (RuleContext.person_name).
- Suite: 10/10 dating tests; rules suites + 3 hash seeds OK.

PENDING (not mine): A3's `offer`/`respond` domain appliers (accept path
is typed `not_available` until A registers them — deliberate, not a 500);
ORDER NOTE for A3: the accept option's `_domain` dict runs `chemistry`
before `offer`, so A's appliers must tolerate stranger→edge creation in
that order (A3's "stranger → partner chemistry" scope covers this).

## C2 — validator violations in my files: NONE

`tools/validate_v3_content.py` reports 95 violations, ALL in A's
`v3_parenting_*.json` (top-level stat-effect spelling). Every file I own
(`dating_offer.json`, `drama_relationships.json`, `flavor_*.json`) is
valid; the new `v3_romance.json` validates clean. Routed to A via the
existing review queue (D1) — no action available in my files.

## C3 — 30 general romance/relationship v3 events: DONE (dispatch routed)

New file `src/game/content/events/v3_romance.json`, ids `rom_*`:
5 first dates, 3 chemistry, 5 flirting & rejection, 4 exes, 4 money
fights, 3 in-laws, 3 make-ups, 3 dating anniversaries = 30. All:
- `interaction` metadata with `romantic: true` (fixed-rule guard applies
  at offer AND resolve; `conditions.min_age` 18);
- roles bound via A's registered adult predicates only
  (`adult_non_related_stranger`, `adult_acquaintance`,
  `adult_existing_lover`, `adult_ex_lover_of_actor`,
  `adult_partner_or_lover_of_actor`);
- first-person outcome text (frozen journal voice; found and fixed one
  `{name}` leak and one second-person line in self-review);
- every event keeps at least one free exit (verified over all 30);
- `audience: player_known` with only declared person participants;
- money uses single-posting effects; DeltaValue randints where variable.

Namespace decision (IMPORTANT): I first shipped them as `dra_rom_*` so
A's yearly drama pipeline would select them. That broke A's curated-pool
tests (`test_knowledge` count 20→50, `dra_caught_late` forced-flow
dilution, `test_game_m2a` year interruptions, A's free-option check not
parsing DeltaValue money). I reverted to `rom_*`: full suite green again
(648 OK / 9 skips — the same 9 baseline skips Codex reported). The pack
is validated, loader-clean and pipeline-proven
(`RomancePackV3Test.test_reachable_through_v3_pipeline_on_real_service`
instantiates every bindable event through the real `instantiate3` on a
real service env, ≥25 of 30 bind with partner+stranger present), but NO
engine selects `rom_*` yet. Routed as contracts-README Open item #5 for
Claude: widen A's drama pool (A updates its count tests) or give C a
C-owned phase hook.

## Verification

- Full discovery: 642→648 tests, OK, 9 skips (baseline), ~117s.
- `tests.test_rules_dating` + `tests.test_rules_event_content` +
  `test_rules_v3_loader`/`event_loader`/`hashseed`: OK under
  PYTHONHASHSEED 0/1/42.
- `git diff --check` clean; only my owned files touched
  (`events.py`, `rule_actions.py`, `test_rules_dating.py`,
  `test_rules_event_content.py`, new `v3_romance.json`, docs rows).
- Content validator: my files clean (A's 95 violations reported above).

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Real GameService/Player/Partner objects in the new service tests; the
      only mocks are at B's `evaluate_offer` boundary (true domain boundary,
      same as the pre-existing dating tests) — scoring itself is B's.
- [x] Failing-before checked live for the registration break; the context
      break is Codex's documented repro (#2 of the deferred dating item).
- [x] The new draw path adds no draws; existing single seeded draw kept;
      3 hash seeds reproduce.
- [x] Typed errors throughout (`unknown action`→invalid_input,
      no-eligible-adults→not_found, hard-fail→not_allowed,
      missing applier→not_available); no raw 500s in the new paths.
- [x] Money: no new postings in C1; the pack uses single-posting effects
      and every event has a free exit (asserted in tests).
- [x] Identity: bindings/predicates use person_id only.
- [x] Save/load: pending with stored draw survives pickle (service tests
      reload via `_load`); no new envelope fields.
- [x] Costs/eligibility: affordability gate runs in instantiate3 via the
      new GameContext.money(); refusal path applies stored data only.
- [x] Age gates: all pack events romantic:true + min_age 18; guard runs
      at offer (instantiate3) and resolve (_guard_pending_romance).
- [x] No soft-locks: free-exit test over all 30 events + validator.

Hygiene
- [x] Only owned files edited; shared files re-read before editing.
- [x] No probability/balance changes (draw formula untouched).
- [x] Focused suites pass under 3 hash seeds.
- [n/a] UI items are B's.
- [x] Rare paths forced via the real registry seam, not random rolls.
