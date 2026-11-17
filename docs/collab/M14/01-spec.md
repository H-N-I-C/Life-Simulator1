# M14 — NPCs who live independently

User-approved addition, 2026-11-17. Read [delivery rules/dependencies](../M13-20-plan.md). Builds on M2.5 canonical family/knowledge, M4 social circles and M13 world chronology. Extends NPC invitations/memories into independent lives.

## Outcome

Friends, siblings, coworkers and exes pursue careers, move, form relationships and support their own families. NPC-to-NPC changes happen even without the player's involvement; the player learns about them through appropriate contact, public news or disclosure.

## Playable stages

- **M14a — Independent work and home:** follow a sibling/friend through a job change and move. Catch up later and see persistent changes rather than a fresh generated biography.
- **M14b — Relationships between NPCs:** two known adults meet, accept/refuse, form or end a relationship, and may become parents through existing consent/chronology rules. The player can support, react or stay uninvolved.
- **M14c — Connected lives:** a coworker's departure changes the workplace, a relative's financial difficulty creates a request, and an ex's move affects future contact. Follow these changes through several years and a succession.

## Content and model

Starter chains: career change, relocation, friendship conflict/repair, adult relationship/parenthood, and household setback/recovery. NPC ambitions/resources constrain actions; personalities influence decisions without making outcomes certain. Ordinary uneventful years remain possible.

Freeze autonomous intent/action/result records, tracked-cast bounds, household/account ownership, knowledge notifications and world-turn scheduling before coding. Use the existing people and relationship graph, not text-only replacement NPCs. Snapshot living tracked IDs, process each once with stable ordering and persisted seeded outcomes; newborns do not receive another yearly update. Use bounded background summaries outside the tracked cast.

## Browser acceptance and exit checks

1. Advance a valid scenario without contacting a friend for several years; reconnect and inspect their independently changed job/home/history.
2. Force an NPC-to-NPC adult offer and refusal, then a different accepted relationship; reload between decisions and assert no forced consent or duplicate edges.
3. An NPC secret remains absent from client payloads and uninformed reactions until legitimate discovery. Editor inspection does not create knowledge.
4. Check salaries, moves, support and parenthood costs against canonical accounts; shared households/children are not paid or charged once per relationship.
5. Verify 17/18 boundaries for both NPC participants, world-year/cast snapshot behavior, death and succession references, deterministic hash-seed behavior and a measured turn-time budget.

## Traps and ownership

Avoid copying the legacy partner simulator, using the player's cash for all NPCs, simulating a person twice through different circles, or revealing private facts as global news. Preserve recognizable people when the active cast changes; archives are history, not a pool of ex-partners. Assign one end-to-end autonomy owner; domain owners provide typed career/move/relationship hooks. D verifies real effects outside player-initiated paths.
