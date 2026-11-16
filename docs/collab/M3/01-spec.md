# M3 planning pass — careers, qualifications and skills

**Proposal for Claude approval; no implementation/review performed.** One planning pass under the new cadence. Deliver on accepted M2 engine/ledger/turn contracts, not a second simulation. Each complete stage gets one consolidated review and at most one fixes re-review before user testing.

## Outcome

A player can choose study or a trade, build a skill, apply to a named role at a persistent employer, earn pay through the existing ledger, seek promotion or change employer, and recover after rejection/dismissal. Qualifications and skills affect real eligibility and outcomes. Work/education/skills pages explain requirements and progress. Job portraits may use M0b context looks without changing personal appearance.

Do not expose nonfunctional career catalogs. M3 excludes business ownership, property, friends/coworker social graphs, specialist fame careers, location-specific licensing and M6's full weekly schedule. Employers are persistent organizations with stable IDs; managers/coworkers are not invented text-only people. Reuse M2 school, debt, pension, job stress, mortality, journal and scenario facilities.

## Dependency and contract gates

1. Start implementation only on the accepted M2 baseline and approved stage branch. M2's sole phase-map owner remains A; feature owners register hooks at agreed existing phases, never create another phase order.
2. `docs/collab/M2/event-schema.json` was explicitly DRAFT when this plan was written. A owns that schema; A+C+Claude must freeze it before M3 writes event records against it. Proposed M3 condition/effect extensions are design requests, not permission to invent private interpreters.
3. M0b avatar contract/examples and manifest also need freezing. `docs/collab/M0b-examples.json` was absent at planning. Use the approved M0b resolver when available; regular personal portrait is the intentional fallback for missing job art. Do not infer downloaded assets or claim a job look exists without a manifest entry.
4. Before parallel coding, Claude freezes M3 feature API/state, career/program/skill data schemas, action/choice validation, salary synchronization, qualification prerequisites and event extensions. Proposed shapes/pitfalls are in 02-contracts.md. Record schema owners and accepted example payloads. No agent codes on draft shapes.

## Initial coherent content (original fictional data)

| Path | Complete progression | Education/skills that matter |
|---|---|---|
| Electrical trade | Apprentice → Electrician → Senior electrician → Site supervisor | secondary completion/apprenticeship, practical skill, organization |
| Office operations | Office assistant → Coordinator → Operations specialist → Operations manager | secondary entry route, business degree alternative, communication/organization |
| Healthcare | Care assistant → Qualified care worker → Nurse → Senior nurse | care diploma/bachelor program as defined by the fictional game, care/communication |

Baseline: **12 reachable roles, 6 fictional employers (2 per path), 4 programs, 4 trainable skills**. These are minimum complete loops, not permission to fill a catalog with unreachable choices. Program set: paid electrical apprenticeship, care diploma, business bachelor's, nursing bachelor's. Skills: practical, organization, communication, care. Freeze exact durations/costs/thresholds with Claude; this plan does not assert real-world accreditation rules.

Each path has entry, two meaningful advancement steps, top role, setbacks and an exit/recovery route. A nondegree route remains viable. Moving employer should be an alternative to promotion, with clear salary/tenure effects. An apprenticeship is a paid employment+training arrangement, not two full-time salaries.

Use a small set of well-tested data events (target 4 distinct contextual decisions per path and 4 education/skill events) with real choice tradeoffs, stored sampling and free alternatives. Larger event counts are not a release gate. D validates reachability and variation; feature owners remain responsible for the meaning of their own content.

## Playable stages and exit journeys

### M3a — skills and qualifications

C ships skill progress/training; B ships programs/enrollment/tuition/debt/completion and Education UI. A supplies only the frozen bridge needed for career eligibility; the whole careers feature can follow in M3b. Complete an electrical apprenticeship (using its named apprentice assignment) or care diploma, see actual qualification/progress and a newly unlocked job requirement. An ordinary school graduate can train skills without enrolling. If prerequisite modules aren't integrated, do not expose dummy career eligibility; keep the stage gated until its complete journey works.

Exit: enroll → pay/decline funding → advance through progress or a failed year → reload midcourse → finish/leave → qualification persists; train a skill with bounded annual usage → actual eligibility changes. $0 player can decline/leave/choose an available paid/free route and still age. Apprenticeship employment requires A's real bridge before that route counts complete.

### M3b — named careers and employers

A ships the entire careers feature including Work UI/data/tests. Apply for an eligible opening, accept a stored offer, earn auditable salary, earn promotion or change employer, handle rejection/dismissal, and retire through M2. Integrate all three ladders with B/C systems; jobs use optional derived avatar context.

Exit: play trade and office/healthcare examples, including a degree-gated role and a nondegree route. Verify salary/qualification/skills/performance gates in UI and server; employer/assignment identity, annual salary history and pension survive reload. Job loss restores personal avatar; debts remain.

### M3c — content, balance and continuity

All feature owners close their data/content gaps; D delivers independent browser/accounting/save/visual evidence. Run contrasting starts, complete every ladder/qualification, and migrate M2 lives without inventing historical degrees/employers. Resolve content dead ends and misleading requirements. Claude can merge this gate with M3b if scope/evidence are already complete; no artificial extra review cycle.

## Shared gameplay policies to freeze

- One primary full-time work/study commitment. Apprenticeship is one joint commitment; paid wages post once. Part-time/coexisting paths only if the accepted M2 rules support them and Claude explicitly chooses them.
- Player can inspect a program/job while ineligible. Actions clearly show missing prerequisites, annual effort limits and exact known costs before confirmation; backend enforces the same gates.
- Earned qualifications are durable records; enrollment is separate. Dropping out/expulsion does not create a credential. Switching programs needs an explicit credit/refund rule (default no transfer/refund unless defined), with prior completed credentials retained.
- Skills are independent of smarts/looks and cosmetic appearance. Smarts/traits can modify bounded learning rates, never silently replace a required qualification. Training has an annual limit and cannot be spammed to maximum in one turn.
- Promotion is not automatic age progression: meet published role requirements, tenure/performance and skill thresholds, with bounded uncertainty if defined. Failed applications do not reroll merely by reopening the page. Define attempt limits and eligible recovery actions.
- Keep M2's pay-period timing. Job acceptance/promotion between turns changes the next applicable quote, not a second credit for the already-paid year. Mortgage/financial systems are not added here.

## Ownership model (supersedes M2 layer ownership only after Claude approval)

A owns **careers/employers** end to end; B owns **education/qualifications** end to end; C owns **skills/training** end to end; D owns QA and content tooling. Each feature owner edits its rules, projection/commands, data, frontend module and tests. Old “B edits all UI/A all rules” restrictions must be replaced by a Claude-approved M3 ownership manifest in 00-TEAM before coding.

A remains the single integration owner for service dispatcher, canonical phase map, ledger, migration registry and save orchestration. B owns shared frontend shell registration, CSS base and adapter. These owners make small pre-agreed extension points once; each feature then registers through its own files. Other agents send exact integration requests instead of touching shared code. No shared Player scalar edits without a declared adapter contract.

Claude uses per-agent branches/small commits per updated Git policy, routes integration changes and merges after focused checks. Do not assume branch creation/merging is part of this planning pass. Each stage is reviewed as one integrated result; no individual-fix review loop.

## Definition of done

Every included role/program/skill is reachable and browser-playable with meaningful progression and rejection/exit. Deterministic seeds, paused decisions, revisions/replay, one ledger poster, immutable IDs, M2 migration, accessible phone UI and no-soft-lock rules remain intact. New role looks resolve through real manifest fallback. Feature reports include focused results and the ticked 00-SELF-CHECK with honest exceptions; Codex runs full suite plus consolidated evidence when Claude requests review.

## Planning self-check

- [x] Read process/team/M2 phase map and event draft/M0b requirements; status updated. No code or unfrozen schema implemented.
- [x] Proposed end-to-end feature ownership, shared-file owners, prerequisites, stage journeys and concrete traps.
- [x] Money/identity/RNG/save/eligibility/soft-lock/UI acceptance requirements specified, not claimed verified.
- [ ] Runtime/failing-before tests, three-seed checks and browser/contrast validation: not applicable to a planning-only pass; required from implementation and consolidated review.
- [x] No implementation/balance/schema changes made; future reports must paste the full 00-SELF-CHECK list.

## Claude approval (2026-11-16)
Approved, including the feature-ownership model, which takes effect when M3 starts. Start gate: M3 tasks begin only after M2 is accepted (Codex consolidated review), the event schema is frozen, and M0b-examples.json is published. Until then, agents finish M2 and M0b.
