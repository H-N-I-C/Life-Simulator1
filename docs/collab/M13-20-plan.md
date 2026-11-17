# M13–M20 delivery plan

User-approved scope, 2026-11-17: include all eight additions in milestones and plans now. This is planning authorization; implementation starts after accepted M12 and the milestone's own dependencies. M2.5 remains the active priority. The earlier prohibition on post-M12 planning is superseded by this request.

## Sequence and dependencies

| Milestone | Builds on | User-testable result |
|---|---|---|
| [M13 World history](M13/01-spec.md) | M3 careers, M5 economy, M7 places, M10 markets, M12 content | The same type of life plays differently across world years and local conditions |
| [M14 Independent NPC lives](M14/01-spec.md) | M2.5 graph/knowledge, M4 social circles, M13 world clock | Follow a friend or sibling whose career, home and relationships change independently |
| [M15 Inner life](M15/01-spec.md) | M6 commitments/projects, M8 goals, M14 people | Change direction as priorities evolve and distinguish fulfillment from wealth/status |
| [M16 Caregiving and recovery](M16/01-spec.md) | M2.5 family, M5 costs, M6 time, M14 autonomy, M15 priorities | Negotiate care, adapt to changed circumstances and rebuild over several years |
| [M17 Creative works](M17/01-spec.md) | M6 projects, M8 legacy, M11 audiences, M14 collaborators | Publish a work, see its reception, and find it again in the next generation |
| [M18 Player content editor](M18/01-spec.md) | Stable M12+ schemas and M13–M17 feature registrations | Author a valid scenario/event pack, export it and play an imported copy |
| [M19 What-if timelines](M19/01-spec.md) | Stable persistence/replay plus M13–M18 versioned state/content | Fork one decision and compare independent lives without altering the original |
| [M20 Storytelling director](M20/01-spec.md) | M6 chains, M13 world, M14 NPCs, M15 motives, M18 content metadata, M19 comparisons | Choose pacing and play connected stories with coherent quiet periods and turning points |

Deliver each milestone in its own playable stages. Review and offer each completed stage to the user; do not wait until M20 to demonstrate M13. Progress is ordinary browser gameplay through the real service, supported by dev-only reproducible scenario starts.

## Shared guarantees

- **Contract first:** freeze data/commands/projections, migrations and examples when a milestone activates. These plans do not prematurely freeze implementation shapes. Claude appoints feature owners end to end; one owner integrates shared service/coordinator hooks. Avoid another permanent A-owned integration bottleneck.
- **World time:** M13 defines calendar progression independently of the current character's age. Succession preserves world chronology. Commands, reloads, pending resolutions and director decisions do not advance the world a second time.
- **One simulation:** NPCs, care, royalties, markets and authored events use canonical people, relationships, institutions, projects and ledger accounts. No parallel mini-engine or duplicate salary/household charge. Persist sampled outcomes and occurrence IDs.
- **Knowledge and agency:** maintain engine facts separately from observer knowledge. Both participants must naturally be 18+ for romance/sexual interactions/marriage, with consent; editors, packs, NPC logic, fork settings and the director cannot bypass that rule. Adult law/religion/kinship/orientation factors stay modifiers rather than categorical bans.
- **Reproducibility:** freeze a living-person snapshot per world turn, bound background work, use stable iteration and seeded streams, preserve pending samples, and prevent projections from consuming RNG. New settings do not reroll an existing offer.
- **Compatibility:** saves retain content/schema versions and referenced data. Migrations are explicit and idempotent; unavailable optional content cannot strand an existing pending choice. Forking, export/import and succession must preserve privacy and protected chronology.
- **Content and UI:** authored, original event libraries with reachable branches and a free decline/rest path. Phone width, keyboard access, portraits, reduced motion and actual cost/eligibility previews remain release requirements. Quantity does not substitute for tested journeys.

## Completion and tests

Each spec has concrete browser journeys and exit checks. Implementers run focused real-service regressions, force rare paths and verify three hash seeds. D adds independent browser coverage and honest PASS/FAIL/PENDING accounting. Codex performs the milestone/stage consolidated review and full suite, with at most one fixes re-review. Reports follow QUALITY-GUIDE.md and end with 00-SELF-CHECK.md.

## Planning self-check

Ticked entries describe requirements covered by these plans; they are not runtime certification.

Logic
- [ ] Real-object tests executed: planning only; required in every milestone's acceptance checks.
- [ ] Failing-before regressions: no implementation/fixes made in this planning task.
- [x] RNG, snapshots and once-only occurrence handling explicitly required.
- [x] Typed commands, validation and frozen examples required before implementation.
- [x] One ledger, conserved transfers and actual-delta checks required.
- [x] Canonical person/edge/project/work IDs required; branch namespace specified in M19.
- [x] Save/load, migration, pending preservation and versioned content covered.
- [x] Costs/time/eligibility previews and affordable recovery paths covered.
- [x] Protected adulthood/consent remain enforced across NPCs, authorship, branches and pacing.
- [x] Free decisions and missing-pack recovery required; optional features cannot strand a life.

Hygiene
- [x] Planning/status documents only; existing files read and dev verified before edits.
- [x] No implemented balance/probability changes; future fictional calibration requires a contract freeze.
- [ ] Runtime/hash-seed tests: not applicable to documentation; link/format checks performed.
- [ ] UI validation: future browser journeys defined; no UI built here.
- [x] Forced rare-path scenarios included in the milestone exit checks.
