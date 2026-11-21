> FROZEN v1 by Claude 2026-11-21, with the rulings in 00-kickoff.md.
# §QA + content validation (Kimi D) — DRAFT v1, 2026-11-21

Owner file: only Kimi D writes this file; amendments via Claude.
Scope: docs/collab/M5/00-kickoff.md. A/B/C keep their own sections in
02-contracts-A.md / -B.md / -C.md; Claude freezes when cross-refs agree.

D owns the M5 content validator, the M5 browser/HTTP journeys, the
money-conservation/ledger probes, the M5 liveness sweep, and the
evidence under `docs/collab/evidence/M5/**`. D never edits production
data to make validation pass and never reimplements pricing/eligibility
— every journey assertion reads the frozen projections and dispatches
the frozen commands; expected money values in tests are explicit
fixtures or recomputed from the PUBLISHED terms, never a call back into
production calculators.

Fixed rules (kickoff) D pins as gates: one ledger — every payment is a
ledger leg; affordability previews before every choice (advertised ==
dispatched); free/low-cost paths so no life gets stuck.

## D1 — M5 content validator (`tools/validate_m5_assets.py`)

    python3 tools/validate_m5_assets.py
    # human + JSON report -> docs/collab/evidence/M5/validate-m5-assets.{md,json}
    # exit non-zero on any ERROR (families are WARN-only while DRAFT);
    # warnings never gate.

**Inputs (frozen only).** C's catalogues — `src/game/content/assets/**`
(propose: `homes.json`, `cars.json`, `possessions.json`; the exact
paths and the country-tier table are C's to freeze, cross-ref C-1) — and
C's money-related v3 events (propose prefix `fin_`, cross-ref C-4).
A's asset/loan data if A authors any (cross-ref A-6). Production
`validate_event3` stays the structural authority for events; D mirrors
it like the M3/M4 validators.

**Checks (fail with file+id+rule):**
1. Structural: unique stable string IDs; required keys per the frozen
   catalogue schema; `schema_version` present; `kind` vocabulary
   (home|car|possession — the frozen set, cross-ref C-1).
2. Economics: all prices/costs/values integers >= 0; every PAID choice
   (rent, buy, loan, insurance, repairs) has a free or <= low-cost
   alternative on the same surface (soft-lock rule); loan terms sane —
   interest >= 0, deposit 0-100%, term years > 0 and within a normal
   lifespan (data-bound bound, e.g. <= 50).
3. Tiers: every catalogue row resolves its country tier (frozen tier
   table from C); prices sit inside the tier's published bounds (C
   freezes bounds per tier; D reports out-of-tier rows as ERRORs).
4. Foreign keys: event ids referenced from catalogues resolve in
   loaded event content; condition kinds in money events are within the
   frozen v3 vocabulary (production checker) plus any C-published M5
   additions; effects may not write asset/loan fields directly — asset
   transitions stay in domain commands (the M3 event-records rule).
5. Depreciation/possession value: rows that declare value change rules
   (cars/possessions) declare bounded, monotone-declining or data-bound
   curves — no negative values, no unbounded growth.
6. EPYTHON: no Python/eval/exec anywhere in authored strings; EDUP/
   ERESKIN: duplicate ids / identical reskins (warning).
7. Reachability: from a normal new life, at least one executable path
   to (a) renting, (b) home ownership via mortgage, (c) car ownership,
   (d) at least one possession — each within a normal lifespan and
   without requiring a paid gate before any wage income.

**Fixtures.** Intentionally-invalid JSON under
`tests/content/fixtures/invalid/` with the `*.m5.*` segment (one broken
rule per file, expected code in the first filename segment):
`EPRICE.negative.json`, `ETIER.unknown-tier.json`, `ELOAN.bad-term.json`,
`EREF.unknown-event.json`, `EVALUE.unbounded.json`,
`ESCHEMA.missing-free-exit.json`, ... — a fixture that passes is itself
a test failure.

## D2 — M5 journeys (`tests/browser/m5_journeys.py` + `tools/m5_journeys.py`)

Runner reuses the M3/M4 harness (JourneyCtx, qa hooks,
TMPDIR=~/.lifesim-tmp, one headless Chromium, evidence to
`docs/collab/evidence/M5/journeys.{md,json}` + `m5-*.png`). Honesty
rules unchanged: **PENDING(owner, want)** until the frozen surface
lands; **FAIL** only when the surface exists and behavior is wrong;
pendings drained with a bound; rejections assert the exact error
code/reason; previews assert advertised == dispatched == posted; 390
AND 320 screenshots. UI probes key off B's frozen hosts/selectors
(cross-ref B-3).

- **m5_j1_rent_to_mortgage**: move out of the parents' home -> rent
  (rent command, first rent leg posts once, deposit/occurrence keys
  unique) -> save -> buy with a mortgage (affordability preview quote
  == accepted terms == ledger legs; deposit leg + mortgage principal
  posting + amortisation schedule in the projection) -> deliberately
  miss a payment (arrears state per A's freeze) -> recover through the
  frozen free/low-cost path (never a soft-lock) -> resume repayments.
  Assert exact before/after: rent vs mortgage legs, arrears flag
  transitions, repossession ONLY after the frozen threshold, recovery
  always legal.
- **m5_j2_used_car**: buy a used car (loan terms published ==
  charged) -> insure (insurance leg; a breakdown event with insurance
  vs without posts differently, at most once) -> breakdown/repair
  event served and resolved -> sell (sale leg, remaining loan settled
  from proceeds, net delta == ledger sum).
- **m5_j3_inheritance**: a life with assets + mortgage dies (surrender
  fixture) -> successor inherits assets AND liabilities per the frozen
  rule (cross-ref A-4) and M2's cash succession is unchanged; a
  mortgaged home inheritance carries the mortgage; nothing invented
  for old saves (pre-M5 save -> empty asset envelope, cross-ref A-5).
- **m5_j4_money_conservation**: per audited financed year: integer
  cash delta == signed sum of ALL that turn's ledger legs (rent,
  utilities, phone, mortgage repayment, insurance, upkeep — every
  payment is a leg); leg ids unique (one poster); undo across a
  financed year restores money + envelope byte-identically (reborn
  year replays identically).
- **m5_j5_no_trap_starts**: $0/unemployed/student starts: bills never
  make aging illegal; at least one free/low-cost action exists at
  every arrears stage; decline/sell/leave always legal.
- **m5_j6_preview_parity**: for buy/rent/loan/insure, the advertised
  preview (known_costs + A's published quote fields) equals the
  dispatched charge to the dollar across every journey above —
  mismatch is FAIL(owner of the surface that lied).

## D3 — M5 liveness (`tools/m5_liveness.py`)

Browserless real play of 20 seeded lives to at least age 40 (school,
then work, then real housing/car choices through the frozen commands,
first free option on pendings). Per life: housing state trajectory
(parents -> rent -> own), car ownership spells, net-worth curve,
arrears events + recoveries, possessions held, inheritance witnessed.
Cross-life thresholds (calibrated with Claude after first run, same
pattern as M4):
- **no money explosion**: median net worth at 40 inside a published
  band (first run proposes the band from data; Claude freezes);
- **no traps**: 0 lives with zero legal free/low-cost actions at any
  stage; 0 aging refusals;
- **arrears recoverable**: among lives that hit arrears, the recovery
  rate inside [a frozen band] (repossession exists but is not the
  norm);
- **value changes observed**: >= 1 possession/car value change per
  sweep;
- **inheritance observed**: >= 1 asset inheritance across the sweep.
Evidence: `docs/collab/evidence/M5/liveness.{md,json}`; exit 1 on any
breach (this sweep is EXPECTED to fail until A/C land).

## D4 — Money-conservation/ledger probes (standing)

The M4 conservation probe family extends to M5 legs: every new
category (rent, utilities, phone, mortgage, insurance, upkeep,
sale proceeds) must (a) appear as legs, (b) keep the per-turn
delta == sum identity, (c) post once per occurrence key, (d) survive
reload byte-identically. D asserts these inside journeys and liveness;
the M3 validator's frozen-input list gains C's catalogues.

## D5 — File manifest (D-owned only)

- `tools/validate_m5_assets.py` + `tests/content/fixtures/invalid/*m5*.json`
- `tests/browser/m5_journeys.py`, `tools/m5_journeys.py`
- `tools/m5_liveness.py`
- `docs/collab/evidence/M5/**`

Not D-owned (no edits): anything in `src/**`, `web/**`, production
content. Changes elsewhere go to the owner with the exact request via
Claude.

## Cross-reference requests

**TO A (engine), exact:**
1. Envelope + projection freeze: `env["assets_state"]` (owned assets,
   loans/mortgages with amortisation state, arrears) and the additive
   `state["assets"]` shape (per-asset id/kind/label/value/lien, loan
   schedules, net worth, the affordability quote fields) — D consumes
   read-only.
2. Yearly finance phase freeze: where rent/utilities/phone/mortgage/
   insurance/upkeep post relative to `work_income` and the M3 hook
   registry; missed-payment evaluation order; repossession threshold.
3. Command IDs are C's (rule actions) — A freezes the dispatcher
   allowlist pattern and the resolve seam for any M5 pendings
   (affordability quote as a stored pending? decide and freeze).
4. Inheritance freeze: assets + liabilities pass to the successor by
   the exact rule (mortgaged home carries the mortgage; negative
   equity handling; co-owned/insured cases); M2 cash succession
   unchanged.
5. Migration freeze: pre-M5 saves validate to the empty envelope; no
   invented assets (D re-checks the M3 no-invented-records journey
   pattern against assets).
6. Any A-authored asset data files for the validator.

**TO C (content/rules), exact:**
1. Catalogue freeze: file paths (`src/game/content/assets/**`), the
   homes/cars/possessions schemas, the country-tier table with
   per-tier price bounds, and the kind vocabulary.
2. Rule action freeze: IDs + payload dialects for rent/move-out, buy
   (with mortgage), sell, loan take/repay, insure — typed 400/409,
   eligibility BEFORE any spend, advertised specs with known_costs
   and A's quote fields.
3. Free/low-cost alternatives per paid surface (validator check 2).
4. Event prefix + venue/condition additions for money events
   (breakdowns, repairs, windfalls, scams, landlord disputes); every
   money event carries its cost data for the validator.
5. Repair/breakdown odds as data (D liveness calibrates arrears bands
   from real rates).

**TO B (UI), exact:**
1. Assets tab freeze: host ids for homes/cars/possessions/loans
   sections, the buy/sell/rent flow mounts, the affordability preview
   element (quote fields rendered verbatim), money/net-worth header
   element — so D's DOM probes assert the real mounted UI.
2. Preview rendering rule: the UI shows the server's advertised quote
   and never recomputes affordability (B's M3 rule, unchanged).

**FROM D to all:** journeys/liveness ship against the frozen surface
only; until `state.assets` and the C commands land, every M5 step
reports honest PENDING with the owner named — never a fabricated pass.

## Planning self-check

- [x] Read M5 kickoff + M4 conventions; owner file only; no
  implementation, no unfrozen schema coded.
- [x] Validator/journey/liveness/probe scope names exact owner
  cross-refs; honest PENDING/FAIL semantics preserved; 3-seed + TMPDIR
  rules apply at implementation.
- [ ] Runtime evidence: not applicable to a planning-only pass; required
  at implementation against the frozen contracts.
