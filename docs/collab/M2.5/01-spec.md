# M2.5 proposal — Romance & Family pack

Planning only; for Claude approval. Starts immediately after accepted M2, alongside M3 through separate feature modules/branches. Read the ENTIRE user-wishlist.md, frozen event schema v2.1 and current team/process. No implementation or review performed.

## Fixed rule and interpretation

**Both characters must be naturally 18+ and consent for ALL romance, sexual interactions and marriage.** The final wishlist update supersedes the earlier teen-dating exception. No romantic dating, flirting, engagement or sexual events for minors; childhood/teen content remains ordinary parenting, school and social interaction.

Any adult may attempt to romance or marry any OTHER adult, including same/opposite gender, relatives, stepfamily/in-laws, different faith/class/culture, large adult age gaps and existing spouses. These factors affect acceptance/opinion/reputation/legal recognition/consequences, not categorical eligibility. Valid living targets, mutual consent and meaningful state transitions remain required. Self-targets/invalid IDs are data errors, not an extra relationship-policy ban.

Attemptable does not mean guaranteed acceptance. Attraction/orientation, openness and monogamy preference are modifiers, not editable consent tokens. Maximum scores cannot force acceptance, and repeated pressure is not a guaranteed-success mechanic. Text remains non-graphic. Social/legal consequences are neutral simulation outcomes, not moral judgments about identities.

## Systems and real play

| System | Required player-facing behavior |
|---|---|
| Religion/tenets | Individual belief profiles, configurable relationship tenets, conversion and family/community reactions; personal dissent may differ from public tenets. |
| Country laws/culture | Save-local jurisdiction profiles for marriage recognition/types, divorce and consequences. Disallowed/unrecognized unions remain possible with acceptance/scandal/legal effects. |
| Attraction/preferences | Orientation/attraction, openness, jealousy, monogamy preference, compatibility and developing chemistry; visible bounded reasons and independent NPC refusal. |
| Reputation/secrecy | Scoped social reputation, observer-specific knowledge, private/public relationships, disclosure/discovery, jealousy, forgiveness and separation. |
| Multiple partners/spouses | Several adult lovers, spouses or consensual consort/concubine-style relationships; expectations/recognition differ; divorce targets one union. |
| Family graph/health | Biological versus social/adoptive kinship, step/in-law links, grandchildren, inspectable family tree and abstract related-parent child-health risk. |
| Character editor | In-play attribute/modifier edits, belief/tenet/culture settings and local-law toggles; immediate saved effects with scope preview and audit; age/consent/identity protected. |
| Dating/drama | Apps, blind dates, first-date choices, compatibility, chemistry, flirting/rejection, affairs, exes/in-laws, money/parenting fights and repair. |
| Weddings/parenting | Guests/outfit/venue/honeymoon, cash prenups, anniversaries/vows, marital satisfaction; parenting styles/personalities/milestones/requests, teen troubles, grandchildren and holidays. |

Religion/culture/law catalogs are fictional configurable game presets, not assertions about current real laws or all members of any faith/country. Country selects a simulated policy profile. Characters may disagree with community norms. Neither name nor gender nor complexion determines a belief/orientation default.

## Dependency/freeze gates

- Reuse accepted M2 transactions, person_id, ledger, phase ordering, pending continuations and no-soft-lock guarantees. No second relationship simulator or legacy interactive method called from web.
- C owns event schema/loader/content under current 00-TEAM. Preserve frozen v2.1; propose a separate versioned v3 for new bindings/conditions/effects. C drafts, A checks domain/coordinator/ledger fit, Claude freezes. The earlier M3 plan's A-owned schema wording is superseded.
- Save-envelope, event-file, policy-config and API versions are distinct. Existing v2 content and pending choices continue with explicit adapters; never reroll/reinterpret an old partner effect onto a different spouse.
- M0b assets/resolver remain authoritative. Temporary wedding/graduate contexts use existing manifest fallback and never change personal base/gender/skin/hair.
- Freeze graph/policy/editor/offer/v3 examples before parallel implementation. Contracts here are proposals, not permission to code on unfrozen shapes.

## End-to-end ownership and M3 coexistence

| Owner | Pack feature, including rules + service adapter + UI + tests |
|---|---|
| A | relationships/unions graph, kinship/health calculation, family-tree UI and migrations |
| B | faith/culture/law/preferences policy, conversion and character editor |
| C | dating/drama/secrecy/reputation/weddings/parenting, content and event schema v3/loader |
| D | independent browser/graph/editor/ledger QA, content validators and evidence |

Proposed exclusive namespaces: src/game/features/family_graph/** and web/static/features/family_graph/** (A); relationship_policy/** (B); romance_family/** (C); matching tests/test_family_graph*.py, test_relationship_policy*.py, test_romance_family*.py. C assigns per-domain content directories under their existing content ownership. D retains tools/**, tests/browser/**, approved tests/content/** and evidence/M2.5/**.

A alone edits shared service/coordinator/phase/ledger/migration registration; B alone edits shared frontend shell/adapter; C alone edits schema/loader. Other owners send exact hook requests. Claude must record this feature manifest before coding; old layer ownership does not authorize shared-file collisions.

The same agents own M3 careers/education/skills. “Alongside” means modular branches/integration, not simultaneous tasks by one person. Claude sequences pack foundations first and interleaves independent feature commits. Freeze shared hooks together; basic dating/class modifiers must work with M2 finances without depending on unfinished M3 careers. Per-agent branches/small commits and Claude merges follow current process.

## User-testable stages

**M2.5a — foundations/editor:** migrate a real M2 family, inspect the tree/existing union, inspect/edit policy and individual modifiers, convert, reload/rename without breaking kinship. A minimal real adult offer demonstrates changed acceptance/consequences. Editor is normal in-play UI, distinct from dev scenario starts; controls must have actual effects.

**M2.5b — adult dating/plural unions/drama/weddings:** dating apps/blind dates/chemistry, refusal, multiple partners/spouses, affairs/discovery/reputation, planning/prenups/anniversaries/divorce. Demonstrate an illegal/unrecognized adult union remains attemptable but carries consequences. Recognition never overrides fixed18+.

**M2.5c — parenting/generational continuity:** styles/milestones/requests/teen troubles/holidays/grandchildren, consanguinity health model, blended/adoptive tree, archives and succession. Shared child effects/costs occur once per person, not once per spouse/union.

One consolidated review per complete stage and at most one fixes re-review; stage handoff happens when ready. Do not label foundations as the completed pack. Claude may combine stages only if actual complete journeys and evidence exist.

## Initial content coverage

Original data-driven decisions: target6 dating/chemistry,6 drama/secrecy,4 wedding/anniversary,8 parenting/milestone and4 belief/law/community decisions. Prioritize meaningful distinct conditions/outcomes over quotas. Every manual decision has a legal free decline/rest option; automatic events use the frozen auto rules. D verifies reachability, actual effects and no dangling branches.

## Editor guarantees

Attributes/modifiers use a typed allowlist. person_id, birth/age chronology, adulthood threshold, biological parentage, consent responses, RNG, pending samples and ledger rows are not editable. Names can change without changing identity. Simplest initial editor omits money edits; if added, a specific audited ledger-adjustment command is required.

Personal tenet changes affect that person's profile; changing a shared faith requires explicit save-local profile/scope semantics. Country-law edits preview all affected residents/unions, then update recognition/consequences without deleting relationships or retroactively issuing repeated penalties. Editor omniscience is out-of-character and does not disclose secrets to NPCs.

Changes apply immediately. Relevant pending relationship offers are atomically superseded/cancelled with safe continuation and preserved attempt/cooldown usage; unrelated choices retain stored outcomes. Never reroll a rejected offer through editor refresh or rewrite past consent/birth/history. Age presentation, “maturity”, laws, culture, traits and content files cannot make a minor eligible.

## Planning self-check (00-SELF-CHECK; planning scope only)

Ticked entries below mean the requirement is explicitly addressed in this plan, not that implementation passed a runtime check. Unticked entries are unperformed implementation checks.

Logic
- [ ] Tests use REAL objects: no implementation/tests executed in this planning-only pass; real-object scenarios are required in the tasks.
- [ ] Each fix has a failing-before test: no fix was implemented or reverted; future regression coverage is specified.
- [x] Randomness: plan requires passed-in seeded draws, stable binding/order and persisted outcomes, with no editor-triggered rerolls.
- [x] Inputs: plan specifies typed field/role/option validation and immutable invariant enforcement outside content/editor controls.
- [x] Money: one ledger authority, unique account/occurrence/leg keys, conserved transfers, no multi-spouse duplicate charges and actual deltas are required.
- [x] Identity: person_id only for people; separate union/relationship/fact IDs; names/seeds/indexes are not identity.
- [x] Save/load: v2.1/v3 compatibility, legacy single-partner migration, archived family and pending-choice preservation are specified.
- [x] Costs/eligibility: explicit previews, authoritatively applied costs, legal adult attempts despite soft modifiers and independent NPC consent are required.
- [x] Age/state gates: natural18+ for both participants is fixed, not editable via age/DOB/law/tenets/content; final wishlist update supersedes teen dating.
- [x] No soft-locks: every manual decision needs a legal free decline/rest route; relevant editor cancellation resumes a safe continuation.

Hygiene
- [x] Only authorized planning/status documents edited; team/process/shared contracts read before drafting.
- [x] No probability/balance changes implemented; proposed modifier/risk calibration awaits Claude's freeze.
- [ ] Focused tests/three PYTHONHASHSEED runs: not applicable to planning; required from implementers and consolidated review.
- [ ] UI keyboard/focus/phone/avatars/contrast checks: no UI built or reviewed; explicit feature/D acceptance requirements cover these.

## Claude approval (2026-11-16)
Approved. Start gate: after M2 is accepted, together with M3, following the collision sequencing in this spec. Content-only drafts (data, not engine) may start earlier by their owners.

## Start (Claude, 2026-11-17, user chose M2.5 next; M3 waits)
Phase 1 (now): contracts only. A publishes the graph/union/parentage fixtures; B publishes the faith/tenet/law/attraction data plus the evaluator and editor fixtures; C proposes event schema v3 plus fit-proof; D builds the v3 validator and eligibility-matrix fixtures (after finishing Q1 D1). Everything goes in docs/collab/M2.5/contracts/. Claude freezes it all together, and nobody implements on unfrozen shapes.
Phase 2: implementation per the task files, after the freeze. The fixed rule throughout: 18+ for both participants, with no bypass by age, DOB, law, tenet, editor or content.

## Completion gate (user, 2026-11-17)
When M2.5 implementation is finished, Codex does a THOROUGH consolidated evaluation of all of M2.5 before anything goes to the user: correctness, the fixed 18+ invariant under every path (editor, content, NPC, legacy, auto), modifiers never acting as bans, secrecy/knowledge, plural unions and money conservation, family tree and biological risk, migration from M2 saves, UI/UX quality at phone width, and a real browser play-through. That's one review plus one re-review, as usual.
