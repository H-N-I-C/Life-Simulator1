# Codex proposal: browser-playable life simulation roadmap

Planning proposal for Claude to merge. User approval of the avatar/web plan is recorded in the collaboration message; the “pending user approval” heading in 03-plan.md is stale. This roadmap is not approval to implement later milestones. Existing gameplay comes from CODEX-HANDOFF.md; catalog entries alone do not count as features.

## Delivery rules

Every milestone ships through `./play-web.sh` and includes a short browser play script, regression checks and a known-limitations note. No milestone is complete solely because models or catalogs exist. Keep unfinished actions out of normal navigation. Ship a coherent ordinary life before specialist careers. Preserve saves through versioned migrations; use fixed simulation seeds for reproducible tests and separate cosmetic randomness.

Use a persistent character summary, timeline, contextual decisions, and Relationships / Education & Work / Activities / Finances navigation. Name-bearing person components should use the shared portrait component. Prefer event-driven choices and clear consequences over menus of repetitive stat boosts.

## M1 — Engine/web separation and one playable life loop

**Build:** Introduce an application boundary that takes an explicit action and returns structured events, pending decisions and state changes. Move input/print out of reached rule paths; retain a thin terminal adapter where practical. Provide a read-only state projection for the browser, with stable person IDs and structured references in events. Keep Python authoritative: validate age, funds and prerequisites server-side. Advance a year as one operation; pause at explicit decisions without rerolling or repeating effects when resumed. Centralize save access and handle duplicate submissions. Bind to loopback, reject unexpected origins for mutations and avoid arbitrary file paths in requests.

**Playable slice:** Creator → actual new game → home with parents → one relationship/activity action → age up → resolve an event → save, close and reload. Preserve the chosen avatar. The current avatar milestone precedes this and is a creator/gallery deliverable, not a playable game.

**Exit checks:** Complete the slice in the browser without terminal prompts. Rule execution never blocks on stdin. A resumed decision applies exactly once; reload preserves age, money, relatives, appearance and pending decisions. Failed actions leave state consistent. Use one action/event pattern future features can extend rather than attempting to port every menu at once.

## M2 — Existing-game parity and trustworthy progression

**Build:** Port all reachable existing education, work, health, romance, parenting, activities and death/succession flows into that boundary. Resolve the documented pension/overtime accounting, child income, partner-divorce and university-expulsion bugs. Preserve successor and relative identities from the avatar milestone. Add clear annual income/tax/expense/debt summaries and a persistent journal of decisions and major events. Store structured events rather than rendered HTML.

**Play:** Live from childhood through school, first job, marriage/children, retirement and death; continue as a child. Include a short scenario start for adult and retirement checks so testing does not require dozens of clicks.

**Exit checks:** Every formerly reachable action has a browser route or an explicit deferred-item record. Verify a yearly finance ledger against balances, retirement pension, expulsion state, divorce, save/reload and succession. Document that pre-journal saves cannot reconstruct missing history. Make this the reliable baseline before expanding content.

## M3 — Named careers and education with consequences

**Build:** Named jobs/employers, applications, qualification requirements, performance, promotion ladders, dismissal and retirement. Add majors, vocational qualifications and professional study. Begin with three complete ladders (trade, office and healthcare), not hundreds of names. Introduce trainable skills and prerequisite descriptions; keep skills distinct from innate stats.

**Play:** Choose vocational versus university study, manage tuition/debt, qualify for a role, earn a promotion or recover after dismissal.

**Exit checks:** Qualifications gate applications, progress persists, salaries flow through the shared ledger, and each ladder has several reachable stages and failure/recovery events. Include a viable route without university.

## M4 — Friends, classmates and coworkers

**Build:** Persistent nonfamily NPCs with portraits, interests, memories and a bounded relationship history. Add school/work social circles, invitations, friendship/conflict/reconciliation, clubs, popularity and bullying responses. NPCs can initiate events. Support orientation, pronouns and relationship variety through explicit identity/eligibility rules; do not infer them from avatars.

**Play:** Meet a classmate through a club, become friends, resolve conflict, retain the friendship after leaving school; later meet coworkers through a job.

**Exit checks:** Relationships survive role changes, saves and aging. Invitations respect availability and age. Memories change later choices, NPC counts remain bounded, and romance respects age and consent constraints.

## M5 — Household economy, housing and possessions

**Build:** Rent/buy/sell homes, mortgages, maintenance, moving and household composition. Break recurring costs into understandable categories, including childcare. Add a small vehicle catalog with running costs. Establish ownership records for inheritance; avoid silently sharing all assets between people.

**Play:** Move out, choose between rent and saving for a home, raise a child, handle repairs or job loss, then sell/move.

**Exit checks:** Deposits, interest, repayments and sale proceeds reconcile; insufficient funds/debt have clear consequences and recovery options. Housing persists through reload and has meaningful effects beyond a stat bonus.

## M6 — Time commitments, projects and remembered consequences

**Build:** A weekly commitment budget summarized within annual turns: work, study, training, family and leisure. Add multiyear projects such as an instrument, a book or a certification. Traits influence available choices and outcomes. Introduce event chains with saved state, delays and resolutions, plus personal goals.

**Play:** Finish a project while balancing school/work and family; encounter a later consequence of an earlier decision.

**Exit checks:** Committed time cannot exceed the budget without an explicit tradeoff. Progress and delayed events survive reload; event chains cannot duplicate rewards. Basic actions remain quick, with no requirement to micromanage 52 weeks.

## M7 — Places, everyday life and companions

**Build:** A small set of distinct locations with jobs, living costs and move eligibility; migration with transparent requirements. Add holidays, driving/licences, volunteering and age-appropriate nightlife. Add pets with identity, care costs, relationships and lifespans. Country selection from the creator becomes meaningful here; until then label it as background information.

**Play:** Qualify to move, compare costs, relocate with family, adopt a pet and plan a holiday around commitments.

**Exit checks:** Moving updates costs/opportunities without deleting people/assets. Recurring costs use the ledger. Pets and travel have events and tradeoffs rather than acting as repeatable happiness buttons. Start with one documented currency model; postpone exchange-rate complexity.

## M8 — Family legacy, complete lives and replay goals

**Build:** Family tree, completed-life records, achievements, scenario starts, wills and inheritance of property, businesses where available, and selected heirlooms. Carry a limited set of family memories/reputation effects across generations. Expose kinship through stable identities rather than reconstructing people from names.

**Play:** Complete a modest-life or difficult-start goal, choose an heir, inspect the previous generation and continue with inherited obligations/assets.

**Exit checks:** No self-siblings, duplicate relatives or unexplained asset creation. Inheritance allocation and debt rules are explicit and tested. Family trees handle adoption and separation; completed-life records remain accessible after succession.

## M9 — Crime, justice and recovery

**Build:** A bounded crime/justice system with choices, investigation, legal costs, trials, sentences, prison events and reintegration. Link consequences to employment and relationships. Provide lawful recovery routes; avoid making crime the only viable path out of debt.

**Play:** Take a risky choice, respond to legal proceedings, serve or avoid a sentence through defined rules, rebuild a life afterward.

**Exit checks:** Arrest/custody changes available actions consistently. Pending cases and sentences survive reload; legal costs reconcile; random outcomes are reproducible under test seeds.

## M10 — Business and investments

**Build:** One complete small-business loop (capital, product/service, staffing, revenue, costs, failure/sale), then savings/investment portfolios and rental property. Use transparent fictional market rules rather than live financial feeds. Keep household and business accounts distinct.

**Play:** Save startup funds, operate a business through a difficult year, hire someone and sell or pass ownership onward; compare it with salaried work.

**Exit checks:** Revenue is not confused with profit, financing and insolvency balance, staff have persistent identities, and ownership survives succession. Split investments/rentals into a follow-up release if the business loop is not yet coherent.

## M11 — Fame and the first specialist career

**Build:** Social accounts, audience/reputation and sponsorship opportunities, followed by one complete specialist path selected with the user: musician, actor, athlete or politician. Use shared skills, contacts, projects, schedules and finance systems. Each path needs entry, progression, setbacks, peak and exit; release paths separately.

**Play:** Build a skill, secure an opportunity, complete a project/season, manage public reaction and transition out of fame.

**Exit checks:** Fame has costs and consequences, career-specific choices affect progression, and the route offers depth beyond reskinned salary promotions. Further specialist careers require the same browser-playable acceptance standard.

## M12 — Content depth and optional expansions

**Build:** Broaden ordinary-life event pools across ages, traits, jobs, families and places. Add scenarios, adjustable challenge settings and content coverage tooling to catch unreachable or overly repeated events. Consider organized crime, directing, professional sport, elaborate politics or novelty/fantasy systems only after user prioritization.

**Play:** Repeat several contrasting lives and encounter distinct choices, histories and endings without depending on specialist packs.

**Exit checks:** Each added event has reachable prerequisites and a resolution; translations and accessibility remain workable. Measure repetition and broken chains, not just catalog size.

## Cross-cutting completion bar

For every milestone: browser smoke journey; focused rule/state tests; save/load and migration coverage for new fields; deterministic reproduction of failures; accessible keyboard controls; readable phone-width layouts; and portraits on supported person references. Test data should include poor/wealthy starts, different identities, children/adults/elders and multigeneration saves. Keep test scenarios developer-accessible without leaking internal implementation controls into normal play.

Prioritize M1–M4 as the next core-game releases. Reassess later milestone order after actual play feedback; deliver each as its own small playable release rather than waiting for the entire roadmap.

## M13–M20 — user-approved roadmap extension (2026-11-17)

The user requested that all eight additions be incorporated into milestones and plans now. The [delivery plan](M13-20-plan.md) and individual specs extend this roadmap:

- [M13 — World history](M13/01-spec.md): an advancing world, changing local circumstances and meaningful birth cohorts.
- [M14 — Independent NPC lives](M14/01-spec.md): autonomous careers, moves and relationships within a persistent social world.
- [M15 — Inner life](M15/01-spec.md): evolving priorities, ambitions and fulfillment.
- [M16 — Caregiving and recovery](M16/01-spec.md): care responsibilities, adaptation, grief and rebuilding.
- [M17 — Creative works](M17/01-spec.md): novels, albums and films become persistent works with audiences and legacy.
- [M18 — Player content editor](M18/01-spec.md): author, validate, export and import scenarios, fictional settings and event packs.
- [M19 — What-if timelines](M19/01-spec.md): fork committed lives and compare independent branches.
- [M20 — Storytelling director](M20/01-spec.md): optional adaptive pacing using the established world and eligible content.

These deepen the existing projects/social/legacy systems through explicit later deliverables. M2.5 remains active; no future implementation is started by this planning update.
