# Kimi D — M3 content tooling + independent QA (incremental, 2026-11-20)

No commit. Codex out of limit; Claude reviews.

## This increment

1. **Contracts** — D section written in `docs/collab/M3/02-contracts.md`
   ("Content validation — D (PROPOSED, pending cross-reference sign-off)").
   NOT marked FROZEN: open cross-refs listed for A (careers/employers
   schema + examples), B (programs schema + tuition/funding fields),
   C (skills schema + training-action IDs), A/B/C (confirm the v3 event
   vocabulary in `src/game/events.py` is the complete supported M3 set).

2. **Stage-1 slice** — `tools/validate_m3_content.py`:
   - Runs the production frozen checkers (`validate_event` / `validate_event3`,
     `src/game/events.py`) over ALL event content files and collects every
     failure (production loader stops at the first).
   - D-owned checks: EPYTHON (no eval/exec/__import__/compile/globals/
     locals in authored strings), EDUP (cross-file duplicate event IDs),
     ERESKIN (warning: identical normalized conditions/choices/effects
     under different IDs), EASSET helper wired for the pinned M0b manifest
     (activates when career/program data references look keys).
   - Family validators for careers/programs/skills: DRAFT/WARN-only stubs
     by design — no gating on unfrozen schemas (kickoff trap).
   - Exit non-zero on any frozen-input error; reports to
     `docs/collab/evidence/M3/content-validation.{md,json}`.

3. **Intentionally-invalid fixtures** — `tests/content/fixtures/invalid/`
   (one broken rule per file, expected code = filename prefix):
   missing title, no free option (soft-lock rule), unknown condition
   kind, Python in text. `--selftest`: 4/4 fixtures fail with their
   expected codes.

## Checks run (exact commands)

- `python3 tools/validate_m3_content.py --selftest` → 4/4 PASS, exit 0
- `python3 tools/validate_m3_content.py` → 0 errors, 0 warnings across
  20 event content files (`src/game/content/events/*.json`), exit 0
- Import check: tool imports `src.game.events` (no service.py edits made
  by D in this increment)

## Next (per 06-task-kimi-d.md, as feature data lands)

- Reachability/cost report (12 roles / 4 programs / 4 skills) once A/B/C
  freeze schemas; browser runner/scenario manifest extension for M3a
  (skills+education) then M3b (careers); employed-year ledger audit with
  explicit expected fixtures; visual/context evidence via manifest
  fallback chain.

## Journey harness (increment 2, 2026-11-20)

- `tests/browser/m3_journeys.py` — journey manifest: J1 electrical trade
  (enroll apprenticeship → apprentice assignment → single-ledger-wage year
  → promotion → dismissal/recovery), J2 healthcare (care diploma →
  qualification-gated nurse), J3 skills, J4 $0 legal start, J5 old-save
  resume. Steps return PASS / FAIL / PENDING(owner, why) / SKIP; PENDING is
  emitted automatically when the contract surface is absent (`unknown
  action` 400, missing endpoint, missing ledger-leg projection) and never
  counts as a pass. FAIL only when a surface exists and behaves wrong.
- `tools/m3_journeys.py` — runner: one chromium launch, fresh tab per
  journey, 390x844 + 320x844, evidence `docs/collab/evidence/M3/
  journeys.{md,json}` + `journey-*.png`; TMPDIR=~/.lifesim-tmp with
  profile/save-dir cleanup (verified: 0 leftovers, 0 processes).
- First run: 4 PASS (finish-school baseline x2, work tab at both widths,
  $0 legal aging), 9 PENDING with owners (B: programs x2; A: apply_role,
  promotion, dismissal, ledger-legs projection, old-save migration; C:
  train_skill), 0 FAIL, exit 0. Steps flip PENDING→real automatically as
  A/B/C land their schemas — no harness changes needed.

## Schema freeze — validators switched WARN → ERROR (2026-11-20)

- contracts: D section in docs/collab/M3/02-contracts.md amended to
  FROZEN v1 (per Claude); WARN-only draft mode ended at freeze.
- tools/validate_m3_content.py now implements the three frozen family
  validators as ERROR-gating checks over the real data files:
  - skills.json: frozen skill set {practical, organization,
    communication, care}, min<=default<=max bounds, training option
    vocabulary (cost.money int>=0, annual_limit>=1, 0<=min<=max progress,
    min_age, choose_skill rule).
  - programs.json: exactly 4 programs, unique credential ids, int
    bounds (duration 1..10, tuition/fee/min_age >= 0, min_smarts
    null|0..100), funding vocabulary, acyclic admission-qualification
    graph, study_skill refs, events exist in loaded event content.
  - careers.json: exactly 12 roles / 6 employers / paths {electrical,
    office, healthcare}, unique ids, employer role_id refs, 0<=pay_min
    <=pay_max, base_hours/level/tenure bounds, prerequisite
    skill/qualification/role refs resolve, practices_skill refs,
    look_override in the pinned M0b manifest vocabulary, events exist,
    promotion graph acyclic + every role reachable from a root role and
    offered by at least one employer (EREACH).
- Checks run: `--selftest` 4/4 PASS; full validation now exits 1 with
  2 ERRORs — real content findings, NOT fixed by D (owner content):
  1. EREF careers.json:role:qualified_care_worker requires qualification
     'care_diploma' but the program awards credential 'care_diploma_cert'
     → role gated forever (owner: A).
  2. EREF careers.json:role:nurse requires 'nursing_bachelor' but the
     program awards 'nursing_degree' → role gated forever (owner: A).
  (A's bridge reads qualification_id verbatim per §Education; absent/
  mismatched reads as unmet, never fabricated.)

## Journey re-run after A's careers slice + feature-module check (2026-11-20)

- Journey manifest updated to the FROZEN command IDs: education_enroll
  (B), careers_apply/careers_promote/careers_resign (A), train_skill (C,
  option_id payload); ledger legs read the frozen `finances.ledger`
  projection; steps resolve stale pendings before clean probes and report
  PENDING (not PASS) when only an open pending blocks the probe.
- Re-run: 6 PASS / 7 PENDING / 0 FAIL. Live A signals: careers_apply gate
  enforced with precise server reasons ("practical skill 6/10"; nurse:
  "requires age 21 or older; you need the nursing_degree qualification").
  Still PENDING: B education_enroll + C train_skill dispatch (unknown
  action -> not yet whitelisted), apprentice wage legs, old-save
  migration contract.
- v2 browser harness (tools/feel_screens.py) now includes a
  feature-module check at 320: every web/static/features/* module
  dynamic-imports without errors; §Shell-API modules (careers, skills)
  mount + update + unmount in a detached container with the real
  projection slice — all PASS, zero console/network errors; findings
  land in after-feature-modules.json next to the screenshots.
  Note: modules currently render 0 chars with the injected slice (no
  console errors) — B's wiring + real hook state will confirm rendering;
  the check gates errors, not content.
