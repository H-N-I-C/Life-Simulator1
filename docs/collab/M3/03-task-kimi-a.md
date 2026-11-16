# M3 task proposal — A owns careers and employers end to end

Await Claude assignment and FROZEN M3 contracts. Read process/team, M3 spec/contracts and accepted M2/M0b documents. New feature ownership is deliberate: own careers rules + service hooks + Work UI + data + tests, not just engine plumbing.

## Exclusive feature files (proposed manifest)

`src/game/features/careers/**`, `src/game/content/careers/**`, `web/static/features/careers/**`, `tests/test_careers*.py`. A alone also owns integration changes in existing service/coordinator/finance/migration registry. Existing names/paths may be adjusted once at freeze; record them before coding. Do not edit B/C feature modules. Ask B to apply shared shell registration/CSS changes.

## First: settle traps/bridges

- Freeze M3 API/data/result/annual ordering with B/C; publish role/employer/opening/assignment/promotion fixtures. Event schema remains A-owned, with Claude approval. Don't code optional draft fields and hope others match.
- Declare one salary/job authority and legacy migration path; named employment must not double-trigger generic M2 wages, promotions, raises or pension history.
- Freeze role looks against M0b resolver/context/manifest, not a private PNG map. Coordinate immutable base Appearance with that owner.

## Build and demonstrate

1. Implement all12 roles/6 fictional employers with entry, requirements, promotion edges and variable openings/offers defined in data. Work UI shows employer, role, pay/hours, tenure/performance, missing requirements and advancement path. Application/promotion/transfer commands validate server-side and persist sampled offers before display.
2. Implement meaningful promotion, changing employer, rejection, dismissal and return-to-work. Reuse existing stress/hours/retirement/pension rules; define capped application/promotion attempts. A bad choice cannot create endless same-turn salary rerolls or extra pay.
3. Complete paid apprenticeship employment bridge for M3a alongside B's study feature. One wage payer, one study-progress owner; cancellation of either side has explicit agreed consequences and recovery.
4. Derive optional job look_override from active assignment using M0b resolver; absent asset falls back. Job change/loss clears effective job context without editing skin/hair/badges or person_id.
5. Career work events use frozen event schema; feature data owns career semantics, D validates schema/reachability. No fake coworkers/person IDs just to narrate an employer.
6. Migrate real M2 generic job records and pending offers with accepted terms intact. Service integration stays atomic under existing request/revision/ledger semantics; no second save/RNG authority.

## Tests/report

Use real GameService/persons and deterministic rules: prerequisite rejection, reachable 3 ladders, promotion attempts/replay, offer reload, two employers, one apprenticeship salary, no pay on opening inspection, pay-period transition, dismissal/avatar fallback, pension history and legacy saves. Provide browser Work journey and D scenarios; focused implementer tests only. Update status on start/block/done, report exact changed files/results/limits and full ticked 00-SELF-CHECK with exceptions, then hc send t7. No per-task Codex review; wait for consolidated stage review. Branch/commit/merge policy is Claude's approved M3 manifest.
