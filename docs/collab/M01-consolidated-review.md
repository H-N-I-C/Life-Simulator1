# Consolidated M0 + M1 review

**PASS for user testing now, with minor polish items. No release-blocking defect found in the reviewed M0+M1 scope.** The user can test the creator, portraits, real browser life loop, decisions and save/resume while B finishes the queued polish. This is not acceptance of M2 or full legacy-game parity.

## Independent verification

| Check | Result |
|---|---|
| Full `python3 -m unittest discover -s tests -t .` | **180 tests, OK**; 14.246s unittest / 15.095s measured wall time |
| `python3 tools/browser_check.py` | **49/49 PASS**, actual Chromium, temporary saves, phone/desktop |
| `python3 tests/browser_smoke.py` | **27/27 PASS**, actual Chromium, response-loss/navigation/modal regressions |
| Previously flaky save round-trip, adult styling and age-three toy tests | **3 tests × PYTHONHASHSEED 0/3/7, all PASS** |
| Direct visual inspection | Current matrix/lifecycle, hair×headwear, eye/nose/face surveys in evidence/avatars inspected as PNGs |

No implementation edits or user-save changes. Generated browser evidence refreshed through D's runner. Read A's M0/M1 fixes, B's M0 art/M1 fixes, C's rule report/addendum and D's tooling/report. Current test results supersede their older counts and obsolete issue notes.

## Blockers

**None for beginning M0+M1 user testing.** Existing M2 phase-map/rule/ledger review findings remain M2 acceptance gates; green tests containing some M2 scaffolding do not override those findings.

## Accepted integrated behavior

- **A — model, identity and persistence:** stable appearance data/catalog IDs, dedicated cosmetic randomness, biological/adopted generation, deterministic aging, and normalized legacy saves are covered. Canonical `person_id` is separate from `Player.ID` save slots; parent renames, legacy finalization metadata and successor/relative identity preservation have regressions. `src/game/service.py:172` now migrates live and response-snapshot pending references; `:327` requires the private option fields consumed by resolution. The two final A blockers from re-review3 are closed. M0-A/M1-A scope is accepted.
- **A+B — playable browser boundary:** real Player/family creation, creator appearance override, parental interaction, annual aging, contextual decisions, dead-relative history and completed-life record exist. Server validates eligibility/funds, revisions and pending state. Command/creation replay, atomic-save failure, migration/restart and mortality cases pass. M1 offers a small life loop; generic jobs/education/family gameplay from the legacy game are M2, not claimed complete here.
- **B — client corrections:** simulated committed-but-lost creation response retries produce one life; lost Age response replays once. Resume B overrides an earlier deep link to A, refresh retains B, invalid links recover. Structured timeline/resume participants use `miniPersonChip` (`web/static/common.js:92`); dialog focus/trapping, Escape-to-reopen pill and resolution pass actual browser tests. JSON mark export survives model validation. Origin/Host/body/static-containment tests pass; no temporary service fallback remains.
- **B — art:** braids/locs are continuous strands; afro/hats preserve the face; eye geometry and face/nose/mouth options are materially more distinct. Skin range now runs light to deep and outfits/accessories are visible. Small portraits remain simpler than 128px portraits, as expected. Inspected sheets support a user-testable result rather than a finished-art claim.
- **C — scoped legacy fixes:** pension/overtime use consistent gross/tax/net, working children receive net income, expulsion clears enrollment, partner-divorce branches work, custom life gets shared family setup, optional blank integer input works, and age-specific toy fixes are tested. Legacy two-draw willpower probability is restored (`src/people/classes/player.py:1066`), closing the unapproved probability change. Deterministic integer quote/explicit retention sampler and documented loan-on-expulsion policy are now tested; M2 must still integrate them through its single poster.
- **D — useful repeatable QA:** real browser startup/page/keyboard/export/age/reload checks and deterministic raster contact sheets are available. Runner uses temporary saves and returns failure status; full tests and visual/browser evidence complement one another rather than treating HTTP200 as proof of correctness.

## Minor items — can be fixed during user testing

1. **Hair-side white patches (B):** visible around some child/adult hairlines. Inspect back-hair/ear/front-hair junctions (`src/appearance/render.py:379`, `:799`, `:915`) using dark hair on dark/light skin; close unintended gaps without painting over ears or features. Regenerate controlled 8/16/30 age comparisons.
2. **Elder hair evidence (B+D):** retain the queued check for overly bald age70 examples. The sheets I inspected now show gray hair at70; I cannot confirm universal baldness in the current images. Source recession remains seed-limited to35% (`src/appearance/aging.py:64`) and renderer comments now raise the hairline rather than forcing all receding styles bald (`render.py:940`). Use matched seed10/40 examples with identical nonbald styles to demonstrate receding/nonreceding elders; a single repeated seed across a matrix is not a population sample.
3. **Stat-bar colors (B):** finalize the queued green/amber/red mapping and keep numeric labels/contrast, so meaning does not rely on color. Duplicate CSS fill rules around `web/static/style.css:143`/`:233` deserve consolidation when applying this polish.
4. **Favicon (B):** serve the queued icon or an intentional empty response. No favicon request failure occurred in my current browser run, so this may already be fixed or suppressed by browser behavior; it is not a blocker.
5. **QA assertion/coverage clarity (D):** `tools/browser_check.py:154` treats numeric0/empty string as passing because it checks object identity against False/None rather than JS truthiness. Require a boolean result or evaluate Boolean(expression). Its M1 flow stops at age2 (`:272`) before the first decision template's normal age window, so **49/49 alone does not prove decision resolution**. B's separate27-check run does reach/resolve a real decision. Consolidate these regressions into D's runner when convenient. The participant-portrait assertion used zero participants in this run; source wiring exists, but add a nonempty-participant fixture to make that particular browser assertion meaningful.
6. **Report/test hygiene (all):** remove stale contradictory contract observations and add the new self-check to subsequent reports. Existing ResourceWarnings/unclosed legacy test files and terminal ANSI output are noise, not correctness blockers. No revert-and-rerun of every historical fix was performed in this consolidated review.
7. **Communicate palette/scope honestly (Claude):** new hex values can recolor existing saved appearances, especially former skin_12. Mention the deliberate palette refresh. Do not describe visible M2 section scaffolding as working career/education/finance parity before M2a passes.

## Suggested user test

Run `./play-web.sh`, open the printed `/creator` URL, customize face/hair/accessories and start a life. Spend time with parents, age until a choice appears, resolve it, reload and resume that life. Try the gallery at32/128px and compare faces/hairstyles. Report visual issues or confusing actions; M2 content is still in development. Testing can start without waiting for the minor items above.

## Self-check (review scope; exceptions stated)

Logic
- [x] Tests use REAL objects (Player/Child/GameService) for behaviour; integrated service and real-browser tests were run. Legacy formula tests still use deterministic RNG mocks; this is not a claim that every historical test meets the stricter new mocking rule.
- [ ] Each fix has a test that FAILS without the fix — regression cases were inspected/run, but this review did not revert every fix to independently prove failing-before.
- [x] Randomness: reviewed seed/cosmetic isolation and ordered inheritance regressions pass; legacy global-random rules remain behind the documented locked service adapter, not an assertion that all legacy code has been refactored.
- [x] Inputs are validated: reviewed malformed IDs/options/ranges/types/missing-field regressions and safe structured error paths pass; corrupt saves intentionally use typed500 responses.
- [ ] Money: every movement through the ledger — the single-ledger authority is an M2 requirement, not implemented/accepted M1 parity. M1 actual deltas, affordability and replay checks pass.
- [x] Identity: reviewed public references/finalization/succession use person_id; names, cosmetic seeds, memory addresses and save-slot IDs are not identity keys.
- [x] Save/load: current round-trip, legacy migration, pending/replay snapshot and restart regressions pass.
- [x] Shown costs/eligibility: M1 paid-choice rejection and server eligibility tests pass; broader M2 action costs are outside this verdict.
- [x] Age/state gates: M1 rules enforce eligibility/pending/death server-side; full legacy gates remain M2 work.

Hygiene
- [x] Only review/status documents edited; implementation files were inspected, not changed.
- [x] No probability/balance changes introduced by this review; C restored the agreed legacy divorce probability.
- [x] Full suite and focused regressions pass; flaky-prone cases run under3 PYTHONHASHSEED values.
- [x] UI reviewed with actual Chromium at phone/desktop widths; creator focus/export and dialog navigation pass, structured portrait components exist and contrast fixes are present. Known visual polish and the nonempty-participant coverage limitation are listed above; no exhaustive contrast claim for every pixel/color combination.
