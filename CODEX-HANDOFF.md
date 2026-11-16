# Codex handoff — 2026-11-16

The user is having Claude read this game's files, identify issues, and design an appropriate UI. Codex was asked to independently learn the game's features, compare them with current BitLife, and recommend missing and worthwhile features. The user intends to reopen Codex in a pane alongside Claude. This document preserves that review and collaboration context. No implementation changes were requested or made during the review.

## Main conclusion

The game has a playable basic life simulation. Its largest gaps are meaningful careers, a wider social world, assets/housing, and choices with lasting consequences. Improve those before chasing BitLife's newest specialised packs. UI improvements should be supported by separating game rules from terminal input/output.

## Verified existing gameplay

- Random/custom names, binary gender, random/custom traits and trait conflicts.
- Annual aging; happiness, health, smarts, looks, karma; illness, events and death.
- Parents, siblings, partners and biological/adopted children; conversations, time together, compliments, insults and asking parents for money.
- Dating, proposals, weddings, breakups, divorce, vow renewal, fertility, pregnancy, miscarriage, baby naming and adoption.
- School stages, grades, studying, skipping, dropout, generic university, scholarships, student loans.
- Generic job applications, salary, work hours, performance, stress, raises, dismissal, retirement and pensions.
- Doctors, witch doctors, plastic surgery, meditation, gym, library, reading, music, toys and crafts.
- Simplified taxation/expenses, loans, lottery, inheritance.
- Continue as a child, generation count, cash inheritance, multiple saves and automatic saving after menu actions.

Important: src/lifesim_lib/const.py has extensive lists for careers, acting, business, organised crime, modelling, music, politics, sports, pets and social content. These are mostly data, not playable implementations. Count features by reachable gameplay. Job application in src/menus/main.py:637 generates a salary for an unnamed job and has a TODO for job types. Country data is not an active location system. SocialMedia is an unused small model; Player.social_media starts as None.

## Missing systems and priorities

Very high: named careers/employers/promotions/qualifications; friends and social circles; housing and assets; persistent life journal/history.

High: majors/degrees/vocational and professional education; trainable skills/talents; childhood clubs/teams/popularity/bullying/part-time work; locations/migration/local costs; sexual orientation and gender identity/relationship variety; goals/achievements/completed-life records.

Medium: crime/justice/prison; social media/fame; pets; business ownership; investments/rentals; holidays/nightlife/licences/volunteering.

Later: specialised acting/music/sport/politics/modelling/crime/space/hustling careers; fantasy modes and elaborate minigames.

## Recommended distinctive additions

1. Persistent journal with remembered choices and delayed consequences.
2. NPC memories, ambitions, interests and autonomous invitations/actions.
3. Explicit household economy: income/tax/rent/food/childcare/debt/savings.
4. Weekly commitment budget inside annual turns, balancing work/study/training/family.
5. Multiyear projects: books, instruments, training, qualifications, businesses.
6. Trait-driven options and consequences.
7. Family tree and multigenerational property/business/reputation/heirlooms/history.
8. Scenario starts and personal goals, including ordinary-life challenges.

## Concrete code findings (source review, not exhaustive runtime verification)

- Retirement sets pension in salary, but lose_job resets job_hours to zero and annual income uses job_hours for its taxable-income calculation: pension accounting is incorrect.
- Work hours adjust the taxable amount, but annual income starts from unadjusted salary: overtime accounting inconsistent.
- Partner-initiated divorce in Player.random_events assigns response to `choices` then checks `choice`; also references `self.parter`: incorrect branch handling/possible crash.
- convert_child_to_player creates sibling objects for every child including the selected successor: possible self-duplicate sibling.
- Child.age_up calculates after-tax/expense income then credits full salary.
- University expulsion announces expulsion without clearing university state in that branch.
- Rules, prompts, print/input and UI are coupled. Prefer actions returning structured events, decisions and stat changes before a new GUI.

Main files: src/people/classes/player.py, src/people/classes/child.py, src/people/classes/person.py, src/menus/main.py, src/menus/activities.py, src/menus/start.py, src/lifesim_lib/lifesim_lib.py, src/lifesim_lib/const.py, lifesim.py.

## Suggested implementation order and UI implications

1. Fix accounting/progression bugs; separate simulation and terminal UI; persist event history.
2. Named careers, qualifications, friends/classmates, skills and clear finances.
3. Housing, vehicles, household costs, holidays, pets.
4. Memories, event chains, goals, deeper inheritance.
5. One substantial specialised career at a time.
6. Optional fantasy/novelty systems later.

UI recommendation: persistent life timeline, always-visible character summary, contextual decisions, and clear relationships, education/work, activities and finances areas. Make room for future systems without presenting data-only placeholders as implemented features. Claude owns the requested UI design unless the user directs otherwise.

## BitLife research snapshot (checked 2026-11-16)

Comparison included paid content, not only the free game. Public pages can change; recheck if current details matter.

- Official support lists specialised Mafia, Pro Athlete, Musician, Actor job packs: https://bitlife.zendesk.com/hc/en-us/articles/5165069804948-What-is-Boss-Mode
- Movie Director premiered September 10, 2026; casting, filmmaking, Hollywood progression, awards/festivals: https://bitlifeapp.com/moviedirector
- Ultimate Fighter (June 2026): training, coaches, street fights, championship progression, interactive MMA: https://bitlifeapp.com/whats-new/ultimate-fighter-mode
- Vampire (October 30, 2025): feeding, powers, hunters, vampire bloodlines: https://bitlifeapp.com/whats-new/vampire-mode/
- Seasons/daily tasks and adventures, SoundCloud, Music Producer in recent App Store history: https://apps.apple.com/st/app/bitlife-life-simulator/id1374403536
- US listing version 3.26.1 dated November 5 teased Magic Mode as coming soon. Treat as announced, not confirmed released from evidence reviewed: https://apps.apple.com/us/app/bitlife-life-simulator/id1374403536
- Broader expansion catalogue: landlord, investor, black market, cult, secret agent, zoo, casino, racing, outdoor. Secondary reference: https://bitlife-life-simulator.fandom.com/wiki/Category%3AExpansion_Packs

## Workspace caution

Pre-existing changes observed: modified src/lifesim_lib/lifesim_lib.py; untracked LOCAL-REVIEW.md, play.sh, src/lifesim_lib/safe_save.py. Preserve them. LOCAL-REVIEW.md documents earlier local setup/save-loading security work. No tests or gameplay fixes were performed for this comparison.
