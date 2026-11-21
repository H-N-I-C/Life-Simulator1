> FROZEN v1 by Claude 2026-11-21, with the rulings in 00-kickoff.md.
# §Engine (Kimi A) — M5 contracts: assets/liabilities, loans &
# amortisation, arrears, the yearly finance phase, asset inheritance,
# migration. PROPOSED v1, 2026-11-21. ONLY Kimi A writes this file;
# amendments via Claude. Shared conventions live in the kickoff; B/C/D
# keep 02-contracts-B.md / -C.md / -D.md.

Everything rides the canonical machinery: the ONE ledger poster with its
frozen CATEGORIES vocab (no new categories), the existing
command/replay/undo boundary, the v3 event pipeline for C's money
events, and the phase map (exactly one new phase, added — never a
reorder). No parallel money engine, no feature HTTP, no nested saves.

## Authoritative state

Envelope `env["assets"]` (validated versioned dict, additive,
pickle/undo/replay-safe):

```jsonc
{"schema_version": 1, "next_num": 7,
 "homes": {"h_1": {"asset_id": "h_1", "label": "Small terrace",
    "tier": "mid",                 // C's country-tier data, verbatim
    "status": "owned",             // owned|rented|repossessed|sold
    "current_value": 168000, "condition": 74,
    "mortgage_id": "l_1"|null,
    "tenancy": null | {"landlord": "market", "rent_per_year": 12000,
                        "since_turn": "t_000120"}}},
 "cars": {"c_2": {"asset_id": "c_2", "label": "Used hatchback",
    "current_value": 6100, "condition": 58, "age_years": 6,
    "loan_id": null, "policy_id": null}},
 "possessions": {"p_3": {"asset_id": "p_3", "label": "Games console",
    "category": "electronics", "current_value": 240, "condition": 80}},
 "loans": {"l_1": {"loan_id": "l_1", "kind": "mortgage",
    // car_loan|personal|mortgage|default_deficiency
    "principal_original": 150000, "principal_remaining": 142000,
    "annual_rate_bp": 430,        // basis points, C's product table
    "term_years": 25, "payment_per_year": 9800,
    "secured_by": "h_1"|null,      // repossession target
    "start_turn": "t_000090", "status": "active",
    // active|paid_off|defaulted
    "missed_years": 0}},
 "policies": {"po_1": {"policy_id": "po_1", "kind": "car_insurance",
    "asset_id": "c_2", "premium_per_year": 480,
    "since_turn": "t_000100"}}
}
```

IDs are envelope-scoped (`h_N`, `c_N`, `p_N`, `l_N`, `po_N`). Money
amounts are integers; rates are basis points. Asset/loan state is never
edited outside A's appliers.

## A appliers (the ONLY asset/loan mutators)

- `assets.acquire_home(env, person, *, home_id|build data, funding:
  "cash"|{"mortgage": {...}}, rng-less)` — validates affordability for
  the deposit+fees BEFORE any leg; cash purchase posts one purchase leg;
  mortgage funding also creates the loan record (principal legs via
  `loan_borrow`, deposit via the purchase leg's group_id).
- `assets.take_loan(env, person, *, amount, kind, term_years,
  rate_bp, secured_by=None)` — C's loan-product data bounds
  amount/term/rate; one `loan_borrow` leg.
- `assets.repay_loan(env, person, loan_id, amount)` — partial or full;
  full sets `paid_off`; occurrence key per year.
- `assets.sell_asset(env, person, asset_id)` — sale at engine-computed
  value: outstanding secured loan paid from proceeds (repay legs), net
  posted as a positive `income` leg ("Sale: <label>"); asset → `sold`.
- `assets.rent_home(env, person, *, home_id, rent_per_year)` /
  `end_tenancy(env, person)` — tenancy records only; the recurring rent
  leg posts in the finance phase.
- `assets.insure(env, person, asset_id, policy)` / `cancel_policy` —
  policy records; premiums post in the finance phase.
- `assets.repossess(env, service, loan_id)` — internal: secured asset →
  `repossessed` (leaves the household at outstanding-debt value),
  deficiency becomes a `default_deficiency` loan (unsecured), journal +
  typed event for C's content hooks.
- `assets.inherit_assets(env, deceased_assets_view, successor)` —
  called from A's continue_as seam: owned homes/cars/possessions move to
  the successor at current value WITH attached loans/policies; rented
  tenancies end; M2's cash succession is untouched.

All typed errors (ValueError→400, PermissionError→409) exactly like
M3/M4; eligibility == advertised; previews never sample or post.

## Frozen ledger-category mapping (no new vocab)

| leg | category | occurrence key |
|---|---|---|
| rent | `expenses` | `fin:rent:{tenancy}:{year}` |
| utilities/upkeep/premiums | `expenses` | `fin:upkeep:{asset_id}:{year}` |
| loan principal+interest payment | `loan_repay` | `fin:loan:{loan_id}:{year}` |
| loan drawdown | `loan_borrow` | `fin:borrow:{loan_id}:0` |
| asset purchase (price+fees) | `expenses` | `fin:buy:{asset_id}` |
| asset sale proceeds (net) | `income` | `fin:sell:{asset_id}` |
| missed-payment fee | `other` (negative) | `fin:late:{loan_id}:{year}` |
| repossession deficiency | `loan_borrow` | `fin:deficiency:{loan_id}` |

Reusing a key with different data is a LedgerConflict (frozen rule).

## Yearly finance phase (A-owned; the ONE new phase)

Phase id `finance`, added to PHASE_ORDER directly AFTER
`debt_service`, BEFORE `family_events` (frozen order: ... education <
workplace/social < debt_service < **finance** < family_events ...; no
existing phase moves). Served by
`src/game/features/assets/phase.py` through the existing phase/result
boundary; its money returns through the same one poster.

1. **Liabilities first**: each `active` loan accrues one year's simple
   interest (`remaining += remaining * rate_bp/10000`, rounded), then
   posts its scheduled payment leg. Unaffordable → missed payment:
   `missed_years += 1`, late-fee leg, happiness hit, journal; at 3
   consecutive misses → `repossess` for secured loans, else the
   deficiency handling above.
2. **Ownership costs**: owned homes post utilities+maintenance
   (C's per-tier data); cars post upkeep (condition-scaled by C's
   table) plus each policy's premium; rented homes post rent.
   Living with parents (no tenancy, no owned home) posts NOTHING — the
   free path that keeps no-life-stuck.
3. **Value drift (deterministic, zero rng)**: car condition -1..-4 and
   matching value drift by C's depreciation table, via hash(turn +
   asset_id); home values drift by C's per-tier market table;
   possessions drift by category table. Never on reads — only here.
4. Arrears/tenancy ageing and expiry bookkeeping.

## Inheritance (asset part)

On death, assets stay on the envelope (deceased read-only). A's
`continue_as` seam calls `inherit_assets`: the successor receives
owned assets at current value WITH their loans/policies (a mortgaged
home can sink an heir — intended drama); the deceased's tenancies end
(free). Cash succession stays M2's. D journeys: "inherits the mortgaged
terrace → keeps up payments or gets repossessed".

## Commands

No new player-facing IDs from A: C's rule actions (rent, buy, sell,
take/repay a loan, insure — C lists them in §Content) ride the existing
rule-action boundary and mutate ONLY via the appliers above. A adds
their IDs to the dispatcher allowlist on C's request (M3/M4 pattern).

## Projections (additive `_state["assets"]`, B renders exactly this)

```jsonc
"assets": {
  "net_worth": 23400,        // cash + Σ values − Σ principal_remaining
  "homes": [{"asset_id","label","tier","status","current_value",
             "condition","mortgage_id"|null,
             "tenancy": {...}|null,
             "payments": {"per_year": 9800} | {"rent_per_year": 12000}}],
  "cars": [...], "possessions": [...],
  "loans": [{"loan_id","kind","principal_remaining","payment_per_year",
             "missed_years","status","secured_by"|null}],
  "policies": [...],
  "arrears": [{"loan_id","missed_years","amount_due"}]
}
```

Every buy/sell/rent/loan/insure command is advertised by C with
`known_costs` (the affordability-gate figure) and server `enabled` +
precise `reason`; B never recomputes eligibility or net worth.

## Migration / invariants

Pre-M5 saves validate to the empty envelope: no homes/cars/possessions/
loans, zero net-worth delta; M2 lives (living with parents, no assets)
behave exactly as today and enter M5 through C's normal commands —
never invented starter assets. Invariants pinned by forced tests:
integer amounts; person_id addressing; one poster (every M5 money
movement is a findable leg with the frozen keys above); eligibility ==
advertised; previews never sample/post; every paid path has a free or
leave/decline exit; replay byte-identical; undo-safe via the snapshot;
repossession never traps (selling, leaving, earning, ageing stay
legal).

## File manifest (A-owned)

- `src/game/features/assets/{__init__,state,loans,phase,inheritance,
  projection}.py` — envelope+appliers, amortisation/arrears, the yearly
  phase, asset inheritance, the projection.
- `src/game/coordinator.py` — PHASE_ORDER + `_DEFAULT_IMPLS` +
  `_NOOP_NEXT` entries (the only phase-map addition).
- `src/game/service.py` — envelope default in validation,
  `_state["assets"]` (guarded), C's action allowlist, continue_as
  inheritance seam.
- `tests/test_game_m5_assets.py` — forced real-service tests, 3 seeds:
  amortisation schedule, missed-payment ladder, repossession +
  deficiency, rent/buy/sell flows with single posting, inheritance with
  a mortgage, migration to empty.

## Cross-reference requests

**TO B (02-contracts-B.md):** render the projection above verbatim;
the Assets tab groups (Homes / Cars / Possessions / Loans) map to the
projection keys; affordability previews come from advertised
`known_costs` + the gated `enabled`/`reason` (never client math);
net-worth header reads `assets.net_worth`.

**TO C (02-contracts-C.md):** (1) catalogues in your files with the
engine-consumed shapes: homes per country tier (label, price range,
upkeep/yr, utility/yr), cars (label, price range by age, depreciation
table, upkeep by condition), possession categories (value bands,
drift), loan products (kind, min/max amount, terms, rate_bp by credit
band), insurance products (premium by asset value); (2) your rule-action
IDs + payload dialect for A's allowlist (rent {home_id}, buy_home,
sell_asset {asset_id}, take_loan {amount?, product_id}, repay_loan
{loan_id, amount?}, insure {asset_id, product_id}, end_tenancy, all
typed 400/409, eligibility before spend); (3) v3 money events
(breakdowns, repairs, windfalls, scams, landlord disputes) ride the
existing pipeline — money effects post through the event boundary
exactly like M3/M4 (A's phase never double-posts an event leg).

**TO D (02-contracts-D.md):** validator inputs are C's catalogues
plus A's phase config; money-conservation probes sum ledger legs per
life (every M5 movement is a leg — nothing leaves through a side
door); journeys consume only the frozen commands + projection;
migration journey: pre-M5 save → empty assets → enter via C's commands.
