# Merged roadmap (Claude + Codex) — for user prioritisation

Detailed milestone specs and exit checks: 07-roadmap-codex.md. Each milestone is playable in the browser via ./play-web.sh and passes Codex + Claude review before it reaches the user.

| # | Milestone | What you can play |
|---|-----------|------------------|
| M0 | Avatars (in progress) | Creator, gallery, avatar next to every name |
| M1 | Real game in the browser | Create → be born → age up → events → save/reload |
| M2 | Everything the old game had, fixed | School, jobs, romance, kids, health, death, continue as child; money ledger; life journal |
| M2.5 | Romance & Family pack (CK3-style, see user-wishlist.md) | Dating, drama, weddings, polygamy/relatives/same-sex as modifiers, religion & laws, parenting |
| M3 | Real careers & education | Named jobs, promotions, degrees, trades, skills |
| M4 | Friends, classmates, coworkers | Clubs, popularity, bullying, NPCs who remember you and invite you |
| M5 | Money, homes, cars | Rent/buy, mortgages, bills, possessions, inheritance |
| M6 | Long projects & consequences | Write a book, learn an instrument, train; choices that come back years later |
| M7 | Places & everyday life | Countries/cities, moving abroad, pets, holidays, nightlife, licences |
| M8 | Legacy & goals | Family tree, heirlooms, achievements, life goals, completed-life records |
| M9 | Crime & justice | Crimes, police, court, prison, rehabilitation |
| M10 | Business & investing | Start a company, stocks, property rentals |
| M11 | Fame | Social media, celebrity, first specialist career (acting or music) |
| M12 | Expansions | Politics, sports, royalty-style dynasties, special modes |

## Claude additions (apply across milestones)
- Content volume target: each milestone ships with a written event library (aim 50+ distinct, branching events per milestone), not just mechanics, so lives feel different.
- Story feed polish: events written in first person with people's avatars inline; choices show likely consequences after the fact.
- Game feel: animations on Age, stat-change pop-ups, sound toggle, dark mode, phone-friendly layout.
- Developer tools for testing: a "jump to age / scenario start" panel and fixed seeds so the user can test a feature in seconds.
- Personal touches: a place to list the user's own ideas (docs/collab/user-wishlist.md) that get folded into milestones.

## Codex notes

The sequence and additions work for me, with these scope/acceptance clarifications:

- **M0 scope:** “avatar next to every name” means the creator/gallery and shared-component relationships demo this round. Actual game-screen coverage arrives with M1–M2, as agreed in 03-plan.md. M0 is an interactive preview rather than a playable life.
- **Event volume:** use 50+ as an eventual content ambition for substantial gameplay milestones, not a release gate for every milestone. M0/M1 primarily establish infrastructure; later releases should prioritize reachable, varied event chains with tested resolutions over a fixed count. Define whether branches count separately before measuring volume.
- **Choice clarity:** show known costs and eligibility before confirmation; show actual outcomes afterward. Uncertain outcomes can remain uncertain. “Likely consequences after the fact” should not mean hiding predictable tuition, debt or time commitments until the player has committed.
- **Developer tools:** prefer valid scenario starts over simply assigning a new age, which skips education, relationships and financial progression. Keep debug controls separate from normal play; fixed seeds must cover simulation randomness independently of avatar randomness.
- **Ownership and inheritance:** M5 establishes asset ownership and integrates with existing cash succession; M8 delivers the deeper will/property/heirloom inheritance experience. This keeps the summary consistent with the detailed milestone dependencies.
- **Presentation:** animations should respect reduced-motion settings and sound should be optional. Feed entries should retain structured person references so inline portraits and accessible text do not depend on parsing prose. Treat politics/sports and other M12 examples as candidates for user prioritization rather than committed scope.

No other disagreement with the merged direction. Avatar implementation review remains pending the agents' reports and Claude's signal.

## After M12 (user direction, 2026-11-16)
Once M0–M12 are all built, tested by the user and approved, the next phase is expansions and flavour: more content, special modes, polish and personality. Plan that phase only after M12 is approved. Ideas can be collected in docs/collab/user-wishlist.md meanwhile.
