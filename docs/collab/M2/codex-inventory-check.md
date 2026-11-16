# Preliminary inventory sanity check

C's parity-matrix.md has not landed at this check. This is a source baseline for comparing that deliverable, not approval of an unseen inventory or implementation. No code changes.

- Main navigation includes School, generic job application/job menu, relationships, activities and saved games (`src/menus/main.py:28`). Save-slot viewing/loading is user functionality; legacy save deletion needs an explicit scope decision before exposing destructive browser controls. Debug Menu is developer-only and should not count toward normal gameplay parity.
- Relationships includes conversation, time, compliment, insult, ask parents for money; partner actions also include have baby, propose, call off engagement, plan wedding, breakup/divorce and renew vows (`src/menus/main.py:74`). Do not lose engagement cancellation or the wedding-plan edit/cancel subflow when inventorying only major headings.
- School has Study harder, Drop out and Skip school (`src/menus/main.py:506`). University admission/funding/progression also occurs in annual events; a menu-only inventory misses it.
- Job Menu has Work Harder, Adjust Hours, Request a Raise, Quit Job and retirement (`src/menus/main.py:674`); job applications remain generic (`:637`). Named employers/career ladders are M3, not hidden implemented content.
- Activities include toys, doctor/witch doctor, arts/crafts, Mind & Body, music, adoption, lottery, plastic surgery, Find a Partner and Surrender (`src/menus/activities.py:14`). Surrender is reachable ordinary gameplay and needs an explicit safe confirmation/death flow or an approved deferral.
- Mind & Body includes meditation, gym, library and book selection/reading/abandon/change-book (`src/menus/activities.py:511`). Book subchoices need inventory rows or documented grouped coverage.
- Age gates matter: doctor starts at age 4, adult-only extras start at 18, partner search requires marital_status == 0, and adoption starts at 21 (`src/menus/activities.py:17`). Reflect exact rules and annual limits, not just button names.
- The second inventory pass must inspect Player.age_up/random_events and Child/Partner annual rules for university funding, pensions, pregnancy/naming, relationship-initiated decisions, death and successor choice. Automatic events and prompts are as important as menu actions. Distinguish explicit loan actions from automatic student-loan debt service; don't invent a general loan menu from a data/model field.

Next: compare each of these against C's actual inventory and inspect its eligibility/source/test mapping. C and D implementation reviews remain pending Claude's signal.
