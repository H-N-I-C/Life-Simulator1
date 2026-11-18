# v2 mockup implementation review — Codex, 2026-11-18

**Verdict: feasible as a presentation redesign on the accepted M2.5 engine.** Home and Choice mostly have the data they need. Person needs a small, privacy-aware projection and a new navigation component. These three mockups are not a complete replacement for the current game UI: retain explicit access to the existing screens until their replacements are designed and verified.

Scope: reviewed the three `.dc.html` sources, `web/static/game.{html,js}`, `adapter.js`, feature modules, service projections and relationship action specs. Planning only; no code changes or test execution. This is not a rendered pixel comparison or a new M2.5 acceptance review. Branch verified `dev`; accepted M2.5 remains the starting point.

## 1. Mockup-to-engine fit

| Mockup | Already supported | Gap / implementation decision |
|---|---|---|
| Home: portrait, name, cash, four stats | `GameService._state` supplies `player_id`, `people[].appearance`, `money`, `stats`; `normalizeState` already adapts them. Existing `renderAvatar` uses the Fluent assets. | Use server values and existing renderer. “BANK” currently represents the projected cash balance, not a separate bank account. Use “Money” unless a distinct bank system is introduced. |
| Home: job/location subtitle | `work.job_label`, country, education/retirement state exist. | `_work_view` currently calls employment “Generic job”; no Lagos/city or named Junior Architect projection. Show the actual job/status and country, with student/unemployed/retired fallbacks. Named careers/cities are future feature work, not UI fixtures. |
| Home: annual journal cards | `journal` has age, text, kind, person IDs and changes. `game.js:276` already groups age cards, handles quiet years and adds people chips. | Retain all history and server filtering. Mock “This year/Last year” can use age relative to the current player, but there is no authoritative calendar-year field. Decide newest-first display; do not mutate the underlying chronological journal. Reuse current text handling deliberately rather than rewriting events into fictional headlines. |
| Home: central Age button | Advertised `age_up` command, busy/pending state and retry ledger already exist. | Bind the server-advertised action through `sendAction`; retain dead/continuation and interrupted-year behavior. Never copy the mock's local age increment or `Math.random()`. Keep current age visible even with an empty journal. |
| Choice: title, narrative, involved people, options | `pending_decision` and `normalizeDecision`; option inputs, `known_effects`, enabled/reason. `game.js:726` already renders a complete decision dialog. | Replace presentation, preserve resolution machinery. Mock “AGE 27 · FAMILY” needs an explicit display-category mapping/fallback; not every pending has a category or target. Support multiple people and no portrait. |
| Choice: hint chips and coloured choices | Public known effects and disabled reasons are available. | “Agreement 22%” is not an approved public numeric acceptance score. Do not derive it from `sampled`/internal evaluator data. Relationship may be displayed where already public; omit unavailable agreement data. Hints such as “Mum likely softens” require a server-authored forecast, not an inference from option text. Neutral styling when no authoritative risk classification exists. |
| Person: name, age, role, portrait, relationship | `people[]` currently exposes these basic descriptors; actions already carry `target_id`. | Health, happiness, generosity, NPC job and city are **not** in the generic person projection. Faith needs an approved public/observer-known view. Do not populate the card from editor state or raw model attributes. Omit unavailable details or label them unknown. |
| Person: action grid | `rule_actions._relationship_specs` supplies spend_time, conversation, compliment, insult and parent-specific ask_for_money, plus eligible adult offers. | Gift, Holiday and Call tiles are not established targetable actions by these mockups. Show only actual target action specs; generic holidays/gifts must not be silently reinterpreted as person-specific actions. Prices and cooldowns come from specs, not “From $20/$900” or assumed annual limits. “More” reveals remaining supported actions. |
| Person: edit and back | Existing character editor; per-person identity and browser navigation primitives. | Add selected-person navigation and an editor entry that selects that exact `person_id`. Keep editor mode explicit and preserve preview/apply, pending supersession and revision refresh. A Back button must restore the previous list/tab/scroll, not reset the life. |

The DC files are design source, not drop-in app components: they use `DCLogic`, `sc-for`, bindings and `../support.js`, which are not part of the game runtime. Translate into the existing ES modules; no framework migration is required. Replace artifact `/_blob/...` images with the existing avatar renderer. Nunito needs a deliberate font delivery/fallback decision; do not make loading an external font a prerequisite for play.

## 2. Navigation and uncovered screens

The mock Home has Job, Assets, People and Activities around Age. It drops the current explicit Life/Study/Work/Family/Health/Money tab map. Freeze the destinations before implementation:

- Home is the default life timeline; the player header provides a labelled route back to it, with an obvious active-screen indication.
- Job/Occupation includes current education actions while in school, work and retirement afterwards; keep education accessible whenever its actions exist. Do not hide study simply because a character has a job.
- Assets should initially open current finances/money actions; retain an honest “Money” subheading and avoid implying an implemented property portfolio.
- People opens the roster and Person detail, with explicit routes to dating, all unions/weddings, family tree and What I know.
- Activities retains health/treatments and the complete remaining advertised categories. Keep a catch-all for future/unmapped actions; do not lose General actions.
- All lives, creator, gallery, character editor, completed-life archive/succession and current decision/retry indicators remain reachable. Freeze whether a compact header menu supplies these secondary routes.

**M2.5 surfaces absent from the three mockups:**

1. Dating app, pending match, refusal/acceptance, current courtships, target proposals and stage/18+ disabled reasons.
2. All active unions and each selected union's wedding planner: every legal-charge quote line, total, affordability, venue/guests/outfit/honeymoon; anniversary and renewal UI.
3. Selected-union divorce/dissolution, prenup and support preview; plural unions must never collapse into the single legacy `family.partner` slot.
4. What I know: player-known secrets, disclosures, scoped reputation and provenance. Person details cannot reveal hidden facts merely because the UI looks more personal.
5. Family tree: biological/adoptive/social distinctions, unknown lineage and known secret edges.
6. Character editor: attributes/modifiers, personal/faith tenets, orientation/attraction, jurisdiction laws, validation, preview/apply and stale-state/pending supersession.
7. Parenting actions and choices, naming, adoption and pregnancy decisions. The generic sheet must support these existing inputs; a separate parenting dashboard is not supplied by these mockups and should not be invented as part of the reskin.

Initially mount the existing feature modules behind the new navigation, using scoped visual tokens. They can be restyled later; dropping a link is a functional regression even if Home matches perfectly.

## 3. Contracts to freeze first

A short fixture/interface document is sufficient; no engine rewrite.

- **Navigation + mounts:** screen names, Home destination, selected person/union IDs, back-stack/scroll behavior, existing feature entry points, and B's module mount/refresh/unmount lifecycle.
- **Person projection (A):** propose an additive detail view keyed by person ID. Public basics, observer-safe optional descriptors/stats and authoritative action specs or references to the existing target-filtered specs. Unknown values stay null/absent. Reject unknown IDs with typed errors. Specify deceased, unrelated adult, child and archived-person states. Decide visibility per field; the editor's privileged catalog is not the source for a gameplay card. B owns HTTP exposure; A owns projection/filtering.
- **Shell hooks (B):** build on `shellHooks.getState/sendAction/resolveDecision/refresh`; add selected-person/editor navigation hooks as needed. Components use the adapter, and actions retain the exact action ID, target ID and payload. All mutations stay on the existing retry/revision machinery.
- **Choice display:** canonical inputs/options/disabled reasons; optional public category/hint metadata only if required. A/C must approve any new semantic field. Do not send hidden conditions/scores to make the card decorative.
- **Visual tokens:** colours/type/spacing/portrait sizes, semantic stat colours, button variants, focus styles, safe-area/scroll behavior and loading/error states. Share CSS variables, not copied inline styles.

## 4. Proposed non-overlapping Kimi split

This needs Claude's explicit temporary ownership grant: C normally owns rules, not web files. Do not send all four agents into `game.js` or `style.css`.

| Owner | Exclusive proposed files / responsibility | Boundary |
|---|---|---|
| A | `src/game/service.py` person projection; any new `src/game/features/person_view/**`; `tests/test_game_v2_person*.py` | Safe data only. No routing, HTML, CSS or rule/balance changes. Contract fixtures published first. |
| B | `web/static/game.js`, `game.html`, `adapter.js`, `style.css`, `common.js` if needed; new `web/static/v2/{home,choice,shell}.js`, `v2/theme.css`; `web/server.py`; existing web feature-module integration; `tests/test_web_v2_shell*.py` | Sole shell integrator and route owner. Home, choice lifecycle and nav. Owns imports and stylesheet links for C. Existing M2.5 modules retained. Avoid unneeded creator/gallery CSS changes. |
| C (temporary grant) | New `web/static/v2/person.js`, `v2/person.css`; `tests/test_web_v2_person*.py` | Entire Person feature on frozen hooks: roster-to-detail rendering, target action grid, edit/back requests, loading/deceased/unknown states. No edits to shell/adapter/common/server or shared stylesheet. Proposed module exports mount/update/unmount; B integrates it. No new Gift/Holiday/Call rules in this milestone. |
| D | `tests/browser/**`, `tools/**` where necessary, `docs/collab/evidence/redesign-v2/**` | Real browser navigation, keyboard/phone/visual coverage and existing feature reachability. No production UI edits. One owner for shared browser-flow definitions. |

B has the integration dependency, so C can build against frozen data fixtures and a test host, then deliver the module for B to wire. Do not compensate for slow integration by letting C patch B's files. A need not change existing core state fields if an additive detail view suffices.

## 5. Playable delivery order and traps

1. **Contract + shell skeleton:** freeze the above interfaces, B adds the v2 mount/navigation preserving all old panels, D records the reachability baseline. Existing accepted scenarios still run.
2. **Home + Choice:** actual timeline/stats, central Age and responsive sheet through current command transport. Test a yearly pending, naming input, paid/$0 choices, collapse/reopen, offline retry and death/continuation. No fixed three-option assumption.
3. **People + Person:** A projection and C module land; B integrates person chips from roster/journal/decision. Two people with identical names must still open and act on different IDs. Test minor/adult, deceased, missing field and once-used action cases.
4. **Integrated parity + visual pass:** retain and visit every uncovered M2.5 surface listed above. Replay a plural union journey, variable/multiple-fine quote booking and selected divorce; verify secret visibility before/after discovery. Claude/Codex consolidated review before user handoff.

Specific traps:

- Mock roots use `390×844` and `overflow:hidden`. Production must scroll at 320px, landscape, short screens and text zoom; use dynamic viewport/safe areas without covering the last journal entry under Age/stats. A choice sheet needs a capped, scrollable body and accessible controls above the software keyboard.
- Keep the existing modal focus trap, initial focus, focus restoration and pending “Decide later” pill. Back/Escape/backdrop must not discard a server decision or enable ageing during it. Avoid two feature dialogs owning focus simultaneously.
- Mock opacity is not disabled behavior. Use disabled/aria-disabled consistently, visible server reason and explicit loading/busy states. Unknown actions never become clickable just because there is room in the grid.
- Stat colour cannot be the only signal. Keep numeric labels and audit actual foreground contrast, including green values on white and white text over the green Age gradient; do not assume the mock palette passes.
- Preserve request IDs, stale-revision recovery, session selection, offline retry, editor refresh and per-union quote persistence. No local money/health/relationship updates that pretend the server accepted an action.
- Render names and journal text safely with text nodes/escaping. Do not interpolate user-edited names into unescaped HTML or infer private narrative from internal fields.

## Self-check

- [x] Reviewed real source and projections; no code or gameplay changes made.
- [x] Missing backend fields/actions distinguished from existing features; no invented data, prices or probabilities.
- [x] RNG, ledger, identity, save/retry, visibility and 18+ boundaries explicitly retained in the plan.
- [x] Contracts precede shared-shape work; proposed ownership is explicit and non-overlapping.
- [x] Planned tests force real service/browser paths, including rare pending, plural, secrecy and variable-price cases.
- [x] No balance changes or new feature implementations slipped into the reskin.
- [x] Planning-only verification: source review completed; no suite run or runtime/visual certification claimed.
- [x] Report/status recorded and lead notified; implementation awaits Claude's assignment and interface freeze.
