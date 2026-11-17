# M19 — What-if timelines

User-approved addition, 2026-11-17. Read [delivery rules/dependencies](../M13-20-plan.md). Requires stable versioned saves/replay and M13–M18 world/content state. Extends ordinary save/resume with deliberate, independent branching.

## Outcome

Fork a life at a major choice, keep both timelines and compare how they develop. Accept a job abroad in one branch and stay in another; revisit both without overwriting the original or moving money/knowledge across worlds.

## Playable stages

- **M19a — Fork and resume:** name a branch from a committed save or stored pending decision; choose different options in the two copies. Display which timeline is active and retain the original.
- **M19b — Compare:** view side-by-side decision history, relationships, world circumstances, finances, fulfillment and creative works at matched dates, with accessible explanations of major differences.
- **M19c — Timeline library:** manage several branches, fork a branch again, rename/export/delete a timeline and continue through death/succession independently in each.

## Model and contracts

Freeze branch IDs, origin/snapshot metadata, save/list/replay namespaces, comparison projections and retention behavior. Each branch has its own durable snapshot, ledger, NPC/world state, knowledge and future outcomes. Person IDs stay stable within the copied world and are qualified by branch when comparing; the origin is provenance, not a live shared mutable parent.

Fork creation does not consume simulation RNG or reroll an existing offer. Copies of a pending contain the same bound IDs/samples and pinned content versions. Repeating the same actions from the same starting snapshot remains reproducible; different decisions can change subsequent random-draw paths. No branch merging or cross-branch asset transfer is part of this milestone.

## Browser acceptance and exit checks

1. Fork a stored job/move decision, take different choices, reload both timelines and verify the original pending/choice is independent.
2. Duplicate a command across a restart: only the intended branch responds/replays; no posting, record or creation-index collision touches its sibling.
3. Advance one branch for several years while the other remains paused. NPCs, care, royalties, world calendar and private facts change only in the active branch.
4. Continue as different successors and compare at calendar dates without confusing age, person identity or inherited accounts.
5. Delete the origin and keep descendants usable; simulate a failed clone save and leave no half-created branch. Test pinned authored packs and private-data filtering in comparisons/exports.

## Traps and ownership

Do not alias mutable objects/files, reuse a save-slot/request namespace across branches, compare only by age, or expose another branch's secrets through the comparison panel. Copying raw bytes is insufficient if external indexes/content references remain shared. One owner delivers persistence/library/comparison UI together with the integration owner for save hooks. D verifies isolation and failure recovery through the actual browser/API.
