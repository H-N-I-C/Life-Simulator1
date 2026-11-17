# M13 — A world with its own history

User-approved addition, 2026-11-17. Read [delivery rules/dependencies](../M13-20-plan.md). Starts after accepted M12; this plan does not activate implementation. Extends M3 careers, M5 household costs, M7 places and M10 fictional markets.

## Outcome

Birth year and hometown change the opportunities a character encounters. An inspectable world history records economic cycles, technology shifts and local changes, with visible effects on jobs, housing, education and community life.

## Playable stages

- **M13a — Calendar and local history:** choose a fictional setting and starting period, see a calendar alongside age, inspect local history, and advance through a community change such as a school closure or new transport connection. Succession continues in the same world year.
- **M13b — Economy and technology:** play through a downturn/recovery and a technology transition. Hiring, wage opportunities, housing costs and business demand use the same world conditions. Show which changes affect existing contracts and which affect new offers.
- **M13c — Distinct cohorts:** play controlled starts with matched personal attributes in different periods/places. Compare opportunities and outcomes through a history panel linking public changes to their actual effects.

## Content and model

Start with two fictional places and three complete change chains: economic downturn/recovery, technology adoption, and a local institution opening/closing. Use original data-driven events and versioned fictional calibration. Keep essential education, housing and recovery routes available during difficult conditions.

At activation, freeze world calendar, location/institution IDs, condition snapshots, event occurrence keys, price/eligibility projections and migration fixtures. Calendar year is distinct from player age and command revision; derive protected birth chronology consistently. Existing contract/asset IDs stay stable as circumstances change. Public history does not disclose private household/NPC facts.

## Browser acceptance and exit checks

1. Start in one place, inspect the public outlook, take a job/housing decision, advance through a forced downturn and recover through a legal affordable route.
2. Reload at a world-event decision: the year, quotes and sampled outcomes remain unchanged; resolving never posts a second annual cost or advances the calendar twice.
3. Continue as a child after a parent's death: world year/history persist while the player's age changes to the successor's actual age.
4. Reconcile salary, prices and business effects against one saved condition snapshot. Test conflicting changes and compare matched starts with only period/place varied.
5. Same seed/state/actions reproduce the history; viewing a forecast consumes no RNG. Legacy saves receive an explicit calendar migration without invented personal history.

## Traps and ownership

Do not reset the world clock on succession, reprice fixed agreements retroactively without a defined rule, or apply the same economic effect independently in jobs, finances and business. Bound the simulated world to tracked locations/institutions. One appointed feature owner delivers rules, history UI and tests; shared clock/ledger hooks go through the integration owner. D supplies independent cohort and succession checks.
