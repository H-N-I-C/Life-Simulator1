# M2 draft — existing gameplay, fixed and playable in the browser

Draft for Claude approval; no task begins until assigned. Read ../00-PROCESS.md, ../00-TEAM.md, approved M1 spec and latest reviews. M0/M1 blockers remain gates, not work to bypass. M2 is several small integrated stages under one milestone: do not wait for a giant terminal-to-web rewrite before testing a real life.

## Outcome and boundaries

Deliver browser parity for **reachable** legacy gameplay, trustworthy annual finances, a persistent life journal, death and continue-as-child. Preserve M1 character appearance, immutable person IDs, game saves, durable decisions/replay and completed histories. Browser backend remains authoritative; no menus driven through fake input or scraped print output. Country is still background; named career ladders, majors, friends, housing and other roadmap expansions belong to M3+. Do not present const.py catalogs as working systems.

Reachable action inventory is the scope authority: C inventories actual menu and annual-event branches, Claude confirms ambiguous cases, and A/B/D use the same checklist. Any deferred legacy action must be explicitly identified and approved by Claude; no silent omissions. M2 need not import terminal saves unless separately authorized; M1 web saves must migrate. Do not change legacy probability/balance merely to simplify the UI, except documented bug fixes and agreed affordability/consistency corrections.

## Traps to resolve before coding

1. Calling legacy age_up/random_events can prompt midway through a mutation; replaying the method to reach the next choice repeats effects. Use explicit serializable phase/cursor state and deterministic pending outcomes, not live generators, thread-held prompts or rerunning random choices.
2. Annual pay, child net income, loan principal and debt interest can be counted twice when wiring C's helper. One ledger authority posts each money movement once, with a unique source/turn key.
3. Several menu branches directly mutate Player rather than calling reusable methods. Extract the rule once; terminal and web adapters call it. Do not preserve a second independently maintained formula in the service.
4. Identity, death, surname/relationship changes and succession are graph transformations. Preserve canonical Person IDs and history; never reconstruct relationships by name or appearance seed.
5. Broad file ownership is not permission to touch another agent's functions. A/C must share a function-level manifest before editing player.py or child.py.
6. Tests must exercise production transitions with small configured fixtures, not fabricate success states or generate hundreds of lives to test a three-entry eviction. Implementers run focused tests; Codex runs full discovery on every review.

## Stages and browser acceptance

### M2a — contracts, audited annual money, school/work slice

C publishes the inventory/source map and headless rule interface first; A publishes API fixtures before B wires screens. School enrolls by age, study/skip/dropout alter grades/state, university applications/scholarship/student loans and expulsion resolve through decisions. Generic job application, hours, performance/stress, raises, dismissal and retirement use the corrected legacy accounting. Show current commitments, eligibility, fees and annual cash summary. Test a valid school/university scenario and an employed/retired scenario via actual service/browser actions. Scenario setup must keep the correct age, education, relationships and debt state; no bare age assignment.

### M2b — health, activities and family

Port existing doctors/illness treatment, witch doctor, plastic surgery, gym, meditation, library/reading and age-specific music/toys/crafts as inventoried. Port existing family interactions (conversation, time, compliments, insults, parent money requests), dating and eligible romance, proposal/wedding/breakup/divorce/vow renewal, fertility/pregnancy/miscarriage/baby naming and adoption. Reuse existing traits/trait conflict behavior and expose random/custom creation where reachable legacy flow supports it. Reflect costs, annual limits, relationship effects and failures in journal/UI. No accessory economy, synthetic friends system or new gender/orientation rule expansion hidden inside this parity milestone; existing creator identity options must have explicit supported semantics, without silent fallback.

### M2c — full yearly orchestration, debt and succession

Port reached legacy annual events and lifecycle transitions onto the serializable turn coordinator. Illness, parent/partner/child events, schooling, employment and death must coexist. Implement existing loans, lottery, taxes/expenses, cash inheritance and pensions from the inventory. Continue as an eligible child preserves person identity/appearance/name, generation count and the specified cash inheritance without a self-sibling or duplicate NPCs. Save finished-life records separately from the successor's current-person journal view; keep old journal references resolvable through archives. M8 later deepens property wills/family tree; M2 does not invent those systems.

### M2d — parity audit and handoff

D checks every inventory row against actual browser behavior; A/C compare annual ledger/results to corrected rules, B resolves missing navigation/accessibility, Codex and Claude review. All agreed reachable legacy actions must be accounted for before calling M2 parity complete. User receives a playable coherent release, known limits and short test paths; intermediate ready slices may be offered by Claude once reviewed.

## Ownership and scheduling

- **A (t11):** src/game/** (service, turn coordinator, projection, ledger/save orchestration); person.py immutable identity; appearance model integration and safe_save; tests/test_game_*.py and tests/test_appearance.py. In player.py/child.py A owns ONLY identity/appearance/role-conversion identity portions specified by 00-TEAM, coordinated through Claude. No gameplay-rule edits in C's functions.
- **B (t12):** web/**, play-web.sh, renderer, tests/test_web_*.py and tests/test_render.py. Own browser/API adapter, all real game screens and UI contract validation. No engine/rule fixes.
- **C (t15):** assigned gameplay functions in player.py/child.py/partner.py/parent.py/sibling.py, src/menus/** and assigned helper functions in lifesim_lib.py; tests/test_rules_*.py. Extract headless entry points into **src/menus/rule_actions.py** (within C's existing owned namespace). This module contains no terminal I/O despite its legacy directory; A imports it as the compatibility boundary. Do not introduce a competing service or write to src/game/**. Preserve terminal wrappers.
- **D (t14):** tools/**, tests/browser/**, docs/collab/evidence/**. Own independent browser scenarios/assertions and evidence tooling. No edits to engine/UI/unit tests.

A and C first agree the yearly phase/function map and notify Claude before either touches shared files. Existing M1 bug fixes by C are prerequisites, not formulas to reimplement. B starts on approved response fixtures while A/C integrate; D authors scenarios against that same interface. New reports: M2/kimi-a-report.md through kimi-d-report.md. Plan artifacts below may be authored by their assigned agent as explicit report/contract deliverables; otherwise docs/collab plans remain Claude/Codex-owned.

## Completion evidence

- Browser: normal creation → childhood → study → first job → partner/child → doctor → retirement → death → continue as child, with meaningful branches and reload at pending decisions. Use valid scenario starts for rare transitions as well as one naturally progressed seeded life.
- Reconcile representative salaried, overtime, unemployed, pension and working-child years; verify debt and cash before/after actions. Correct bugs stay fixed in both terminal and browser paths.
- Every visible person reference resolves to a persisted ID and portrait; archived deceased people remain viewable; name changes never alter identity or appearance.
- Decisions/replay survive restart and duplicate submissions; save failures leave no partial turn/ledger/reward. No stdin prompts or print scraping on reached rule paths.
- D provides phone/desktop screenshots, actual assertions, console/network failures and parity checklist evidence. Mark unperformed checks honestly. Codex runs full suite for each review; fix reports use focused failing-before/passing-after tests.

## Claude approval (2026-11-16)
Approved. Edit: each stage (M2a, M2b, M2c) is handed to the user to test as soon as it passes review; don't wait for M2d. Each agent starts its M2 task only after finishing and reporting its current M0/M1 task.

## Addendum (Claude, speed-up)
- M2a gains dev-only scenario starts (e.g. "first day of high school", "age 18 graduating", "30, employed, married with a child", "64, near retirement"), built through real transitions/seeded setup, not bare field edits. A: service function + tests; B: hidden dev panel (enabled via a flag in play-web.sh, e.g. --dev); D: browser flows use them.
- M2c defines the data-file schema for events (conditions, choices, effects, text with person references) and moves the legacy random events into it. This becomes the content pipeline for M3+.
