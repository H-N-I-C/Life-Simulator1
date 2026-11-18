# v2 redesign: pre-redesign UI reachability baseline (Kimi D, 2026-11-18)

**What this is:** the Stage-1 parity input for the v2 redesign. Every UI
surface the current (accepted M2.5) web UI exposes, recorded with a real
browser pass and a screenshot, so Stage 4 can prove the v2 shell keeps each
one reachable. Source list: codex-review.md §2 (M2.5 surfaces the three
mockups omit) plus the core shell.

**Baseline commit:** `595fd9ab` (M2.5 accepted), run in a detached
`git worktree` because the main tree is mid-migration (B is editing
game.html/game.js). Re-run: `python3 tools/v2_reachability_baseline.py`
from a clean checkout of that commit.

**Harness:** `tools/v2_reachability_baseline.py` — real `web.server`, real
scenario-started lives (`married_with_child` + one fixture stranger for
dating eligibility), headless Chromium at 390×844 with a 320×568 floor pass.
Full check list: `report.md`. Result: **67 PASS / 0 FAIL / 0 PENDING / 0
WARN** on 2026-11-18.

## Surface checklist (Codex §2 → evidence)

| # | Surface | How reached today | Evidence | Status |
|---|---------|-------------------|----------|--------|
| 1 | Life timeline / journal (incl. quiet years, people chips) | tab "Life" (`screen-summary`, `#journalList`) | baseline-tab-summary.png | PASS |
| 2 | Study (education actions, all school ages) | tab "Study" (`screen-education`, `#actions-education`) | baseline-tab-education.png | PASS |
| 3 | Work (job card + work actions; retirement state) | tab "Work" (`screen-work`, `#actions-work`) | baseline-tab-work.png | PASS |
| 4 | Family (roster, relationship + family/parenting actions) | tab "Family" (`screen-relationships`, `#familyList`, `#actions-relationships`, `#actions-family`) | baseline-tab-relationships.png | PASS |
| 5 | Health / activities | tab "Health" (`screen-activities`, `#healthCard`, `#actions-health`) | baseline-tab-activities.png | PASS |
| 6 | Money (finances card + finance actions) | tab "Money" (`screen-finances`, `#financesCard`, `#actions-finances`) | baseline-tab-finances.png | PASS |
| 7 | Dating app (swipe UI, match pending, refusal/acceptance) | `Dating & weddings` card → Dating tab; pending via `dating_app` action | baseline-m25-dating.png, baseline-decision-modal.png | PASS |
| 8 | Decision dialog + "Decide later" pill (focus trap, Escape→pill, reopen, resolve) | pending decision auto-opens modal | baseline-decision-modal.png, baseline-decision-pill.png | PASS |
| 9 | Unions list + wedding planner (quote lines, venue/guests/outfit/honeymoon pickers, total) | `Dating & weddings` → Weddings tab | baseline-m25-wedding-planner.png | PASS |
| 10 | Divorce / dissolution (+ prenup/support preview in planner) | Weddings tab → "Part ways" section on the active union | baseline-m25-wedding-planner.png | PASS |
| 11 | Anniversary / vow renewal | Weddings tab → "Renew the vows" section | baseline-m25-wedding-planner.png | PASS |
| 12 | What I know (player-known secrets, disclosures, scoped reputation, provenance) | `Dating & weddings` → "What I know" tab | baseline-m25-what-i-know.png | PASS |
| 13 | Family tree (biological/adoptive/social edges, unknown lineage, secret edges) | `Family tree` toggle card | baseline-family-tree.png | PASS |
| 14 | Character editor (attributes, tenets, faith, orientation, jurisdiction laws, preview/apply, audit note) | `Character editor` toggle → person form + "Local laws" | baseline-editor.png | PASS |
| 15 | Death → completed-life archive + continuation (succession) | surrender chain → `#legacyView` + `#continueCard` | baseline-legacy-continuation.png | PASS |
| 16 | All-lives list / resume | `#backToLives` → `#resumeView` / `#lifeList` | baseline-all-lives.png | PASS |
| 17 | Creator | `/creator` | creator_390x844.png | PASS |
| 18 | Gallery | `/gallery` | gallery_390x844.png | PASS |
| 19 | Relationships standalone view | `/relationships` | relationships_390x844.png | PASS |
| 20 | 320px floor (no horizontal overflow, page usable) | `/game` at 320×568 + live game at 320×568 | game_320x568.png, baseline-game-320.png | PASS |
| 21 | Global chrome (toast, offline banner, crash card, retry) | `common.js` boundary on every page; offline banner asserted in Q1 suites | page checks (no console errors/exceptions) | PASS |

## Deliberately not re-asserted here (covered by standing suites)

- **Retry/revision, replay idempotence, stale-revision 409:** D2 flows +
  editor-law flow (docs/collab/evidence/browser/m25-flows/).
- **Eligibility/disabled reasons incl. 18+ gating:**
  `tools/run_eligibility_matrix.py` + robot statistical runs
  (docs/collab/evidence/robot/, still running).
- **Plural-union journeys, variable/multiple-fine quote booking, secrecy
  before/after discovery:** D2/D3 flows; Stage 4 must replay them against
  the v2 shell, not just this baseline.

## Notes for Stage 4

- The six legacy tab destinations map to the v2 nav as: Life→Home,
  Work→Job, Money→Assets, Family→People, Health→Activities, Study→
  (must stay reachable whenever education actions exist — Codex §2).
- Screenshots are 390×844 (and one 320×568 set); the mock roots' fixed
  390×844 / overflow:hidden must not become production constraints.
- During this baseline the main tree's game.js/game.html were mid-edit by
  B (a transient `tabbar is not defined` appeared there); that is why the
  baseline is pinned to the accepted commit rather than the live tree.
