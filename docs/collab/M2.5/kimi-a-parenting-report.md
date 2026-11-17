# M2.5 A — Parenting report (Kimi A, 2026-11-17; ownership transferred from C step 5)

Design note: contracts/README "Open items" (Parenting design note). C to
confirm v3 fit.

## What landed

- **`src/game/features/parenting/core.py`** — envelope block
  `env["parenting"] = {styles, personalities, milestones, seen}` (A-owned
  envelope write, backfilled on load); styles `balanced | warm_firm |
  permissive | hands_off` (default balanced; soft rate modifiers only,
  never gates); deterministic per-child personalities (energy/compliance/
  hobby — dedicated rng, NEVER the gameplay stream); once-per-child
  milestone records; per-(event,child) `seen` cadence; frozen-calibration
  style modifier table.
- **`engine.py`** — the `parenting_events` YEARLY PHASE (wired into A's
  phase map between family_events and illness): picks ONE occurrence per
  year over the par_* v3 events via C's real loader, binds roles once
  through `EventEngine.instantiate3` (frozen projection, free exit,
  policy_revision stamped), applies pre-effects with the same
  once-at-offer policy as the coordinator, and returns the standard phase
  result (resume continues at illness). `GameContext` is wrapped for
  instantiate3's duck-typed surface (`_V3Context`).
- **`effects.py`** — registers A's v3 domain appliers into C's frozen
  registry: `milestone`, `sample_conception` (persisted per conception
  pair), `end_relationship`, `end_union`, `chemistry`; plus the minor-binding
  child predicates (`dependent/toddler/school/teen_child_of_actor`) into
  C's predicate registry. par_* events are `interaction.romantic: false`
  — children stay non-romantic dependents, the 18+ guard never applies to
  them, and the validator rejects any romantic event with min_age < 18
  (regression-tested).
- **Content** (C's frozen v3 format, validated by C's loader):
  `v3_parenting_milestones.json` (first steps, school recital),
  `v3_parenting_requests.json` (money request, teen trouble, family
  holiday). Every manual choice keeps a free exit; charges post once via
  `effects.money` (cost is a gate marker only — content initially used
  `cost` and nothing posted; fixed to the frozen single-posting rule).

## Bugs found and fixed along the way

- My phase offered every pending with the same `d_<seq>` id (the service
  only assigns an id when missing): `next_decision_num` is now
  incremented at offer time — caught by a replay-conflict test failure.
- v3 `cost` does not post money (gate only); charges must be
  `effects.money` negative — content fixed, single-posting asserted.

## C gap to route (not fixed — C's file, C in flight)

`rule_actions._resolve_event_v3` renders only `{name}` in
`option._outcome` (`.format(name="I")`); `{person:role}` refs raise
KeyError at resolve. My content avoids role refs in outcome_text for now;
C's contract says outcomes should render through the stored bindings.

## Tests — tests/test_parenting.py (5, OK x3 hash seeds)

Real Child objects + real GameService commands end-to-end: content
validates in C's loader and all par_* events are nonromantic; romantic
minor event fails validation; toddler milestone fires through age_up,
records once, style/personality backfilled; money request posts exactly
one -50 ledger row, replay of the same resolve does not repost,
milestone record {given: 50}; at $0 the paid options are disabled with a
reason and the free exit stays enabled. Full regression: parenting +
family_graph + service + events + web_m2 = 144 tests OK; web M1 26 OK.

## Self-check

- [x] Real objects (Child/Player/GameService); seeded rng only via the
      game rng inside `_GameRNG` — personalities/styles use dedicated
      deterministic rngs.
- [x] Children non-romantic: content all romantic:false; validator
      regression for romantic minors.
- [x] Money: single posting, cost-as-gate-only, free exit everywhere.
- [x] Identity: person_id only; once-per-child milestones.
- [x] Save/load: envelope backfilled; seen-cadence survives reload.
- [x] No soft-locks: free exit asserted at $0.
- [x] Focused suites green under 3 PYTHONHASHSEED values.
- [ ] UI: milestone badges/tree surfacing not built yet (tree already
      renders children); holiday/teen UI is plain pending modals.

---

# Content expansion addendum (2026-11-17)

**29 new par_* v3 events (34 total)** across the ages:
- toddler (4): glorious mess, sleepless nights, park afternoon, endless why
- school (6): bully, science fair, team tryouts, report card, lunch money,
  pet contract
- teen (7): first job, learner's permit, concert, gaming, phone, college
  talks, the slammed door
- adult kids (6): moving out, wedding budget, job loss, in-law troubles,
  your birthday, the broken car
- grandchildren (6): first sleepover, the school play, babysitting week,
  family recipe, holiday hosting, the abstract portrait

All `interaction.romantic: false`, original first-person text, every
event keeps a charge-free option (structurally asserted), charges post
once via `effects.money`, `{person:role}` used only in `text` (C's
outcome rendering still pending). New predicates: `adult_child_of_actor`
and `grandchild_of_actor` (graph-parentage based).

**Real bug found and fixed**: the state projection rebuilt pending
records with a fixed key set, silently DROPPING the frozen v3 additive
fields (bindings/schema_dialect/interaction/audience/sampled/
policy_revision — Claude decision 1 violation). The projection now
passes them through.

Tests: +3 (34-event validation + free-option structure; grandkid event
binds the grandchild role through the real service; adult-kid event +
personality/milestone assertions). Phase tests made deterministic via
the engine's event-cache hook. 8 parenting tests + full regression 136
tests OK x3 PYTHONHASHSEED; web M1/M2 OK.
