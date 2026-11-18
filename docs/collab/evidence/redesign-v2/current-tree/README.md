# Current-tree route-visibility checks (Kimi D, 2026-11-18)

Companion to the pre-v2 baseline in `../SURFACES.md`. Those inventory the
accepted M2.5 UI; the checks here run against the CURRENT v2 shell tree and
assert ACTUAL visibility (`Element.checkVisibility()`, not the `hidden`
attribute), per `../../redesign-v2/stage1-evaluation.md` (S1/S2, S5).

Suite: flow `v2-route-visibility` in `tests/browser/flows.py` (runs in the
standard `tools/browser_check.py` gate), hooks `check_shell_visibility` /
`resume_first_life` in `tools/browser_check.py`. Latest run: `report.md` —
**18 PASS / 0 FAIL / 0 PENDING / 0 WARN**.

Coverage:
- More → Character editor from Home: panel actually visible
  (S2 — now PASS; B navigates to the owning surface first).
- All lives: `resumeView` visible AND `gameView` actually hidden
  (S1 — now PASS; scoped CSS honors `[hidden]`, display `none`).
- All lives → Resume: game view actually visible again (round-trip).
- Death → legacy/archive: `legacyView` visible AND `gameView` actually
  hidden (S1's shared hide path, verified rather than assumed).

The `check_shell_visibility` hook is three-state: PASS when healthy, WARN
only for the exact confirmed S1 signature (`hidden=true` but
`checkVisibility=true` with computed `display:flex` — B's open fix per
stage1-evaluation.md), FAIL for anything else. The WARN mask
self-removes once the CSS honors `[hidden]`.

Still open (not asserted here, by design): S4 exact-person editor
selection is scheduled Stage 3; the evaluation explicitly says not to
treat the current Person Edit control as complete.
