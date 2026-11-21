> FROZEN v1 by Claude 2026-11-21, with the rulings in 00-kickoff.md.
# §UI — B (Assets tab + money surfaces). PROPOSED v1, 2026-11-21.
# ONLY Kimi B writes this file; amendments via Claude. Shared conventions
# live in 02-contracts.md (Claude); engine in 02-contracts-A.md, content/
# rules in 02-contracts-C.md, QA in 02-contracts-D.md. This section pins
# what the v2 web client NEEDS from A and C ("TO A, exact" / "TO C,
# exact"); where A or C already froze a shape, this section adopts it.

The Assets tab becomes a game surface, not a ledger dump: a Home card
first (where you live, what it costs, arrears warnings), then cars,
possessions, and loans, each with BitLife-style action tiles. Money
surfaces (header cash + net worth, affordability previews) follow the
existing v2 shell conventions — feature-module mounts, server reasons,
known_costs, zero client-side eligibility math, zero stat spoilers.

## 1. Conventions this section rides (existing, unchanged)

- **Feature-module mount pattern** (M3/M4, `web/static/game.js`
  `FEATURE_MODULES` + `syncFeatureModules(s)`): a module is a
  `{hostId, mount(host), sync(state)}` object; the host is a
  `<div id="v2AssetsMount">` in `game.html`'s `#screen-assets`. The
  module renders ONLY from the normalized state; it never fetches.
- **Hidden until live** (M4 pattern): when `state.assets` (name TO A,
  exact, §2) is absent — old saves, pre-migration — the module leaves
  its host empty and the LEGACY surfaces stay exactly as today:
  `#financesCard` + `#actions-finances` on the Assets screen. When
  `state.assets` exists, the legacy finance card hides (one money view,
  same dedupe precedent as the M4 family roster) and the action lists
  stay for anything C does not re-home.
- **Pending pause** (D's M4 finding, already fixed tree-wide): the
  assets module's buttons are inside the hosts `applyPendingPause()`
  covers the moment `state.pending` exists; new buttons from a re-render
  are caught on the same sync pass. No module-side exemption.
- **Commands**: `shellHooks.sendAction(action, payload)` →
  `/api/games/:id/actions`; a rejected command returns
  `{ok: false, reason}` and the client renders the server `reason`
  VERBATIM (never a client-authored message). Affordability failures,
  age gates, and "you already have a home" are all server reasons.
- **Known costs**: every purchasable option carries `known_costs`
  (§Choice costs v2, frozen M3); the client renders them with the
  shared `formatMoney` ('-$8,095', commas — M3a). Money figures are
  NEVER spoilers; stat effects stay hidden.
- **Decision sheet**: v3 events with choices render through the
  existing `.v2e-card` sheet (frozen since M2); B adds NOTHING there.
  Direct rule actions render as action tiles with immediate server
  reasons. Which purchases are pendings (C, §4) is C's call — the UI
  handles both shapes identically to M3/M4.
- **No client-side math**: amortisation, net worth, condition decay,
  affordability — ALL server-computed and projected. The client only
  formats and lays out.

## 2. TO A, exact — the `assets` projection

One new top-level state key, normalized by `adapter.js` (fill-in from
`peopleById` precedent: pure derivation, no mutation of raw entries):

```jsonc
state.assets = {
  "cash": 5210,                       // == state.money (mirror for layout)
  "net_worth": 48210,                 // cash + asset values − loan balances
  "income_last_year": 30600,
  "bills_yearly": 17400,              // rent/mortgage + utilities + phone
  "home": null | {                    // null = living with parents / homeless
    "home_id": "h_3",                 // C catalogue id (label source)
    "kind": "rented|owned",
    "label": "2-bed apartment",       // server-resolved, country-tier aware
    "location_label": "Sydney",
    "monthly": 1450,                  // rent OR mortgage repayment
    "yearly": 17400,
    "value": null | 320000,           // owned only
    "equity": null | 42100,           // owned only
    "mortgage_loan_id": null | "l_2",
    "arrears_months": 0,              // >0 → amber/red card state + reason
    "insurance": null | {"kind": "building|contents",
                          "premium_year": 240, "insured_value": 8000}
  },
  "cars": [{"asset_id": "c_1", "car_id": "used_hatch_2012",
            "label": "Used Hatchback (2012)",
            "value": 6800, "condition": 62,        // 0-100, drives upkeep
            "yearly_upkeep": 640, "insured": true,
            "premium_year": 410,
            "loan_id": null | "l_1", "arrears_months": 0}],
  "possessions": [{"asset_id": "p_9", "possession_id": "acoustic_guitar",
                   "label": "Guitar", "value": 350,
                   "trend": "up|down|flat"}],
  "loans": [{"loan_id": "l_1", "label": "Car loan",
             "balance": 5200, "rate": 0.079, "monthly": 210,
             "term_years_left": 2, "secured_by": "c_1"|null,
             "arrears_months": 0}]
}
```

Hard requirements on the projection:
- `null`-safe everywhere a life simply has no car/home/loan (minors
  start with `home: null` = parents' place; the UI labels that state
  "Living with parents", NOT a nullish blank).
- `arrears_months` is per-liability (home/car/loan) so the card can flag
  exactly what's in danger; repossession/repossession-warning events
  arrive through C's normal pipeline — the UI never invents them.
- Every monetary figure is an integer in the game's currency units.
- **Migration (A-owned)**: old saves get the envelope but NO invented
  assets; `state.assets` appears with `home: null`, empty lists — the
  UI's "nothing yet" states (§3) cover this.
- Header support: A adds `net_worth` to the existing header payload
  (alongside `money`); no other header change.

## 3. UI behaviour (B-owned, pinned)

**Assets tab layout (top → bottom), 390/320 single column, cards in the
page gutter:**
1. **Home card** — always first. States: "Living with parents · move
   out when you can afford it" (with C's `move_out` tile if offered);
   rented (monthly, arrears badge); owned (value, equity, monthly
   mortgage, arrears). Arrears > 0 renders an amber card border + a
   warning line fed by a server reason/action (`pay_arrears` when C
   offers it); the client never computes consequences.
2. **Cars card** — one row per car: portrait-free (no avatar asset for
   cars; a generic icon), label, value, condition bar (green/amber/red
   by `condition`, same `barColor` convention as relationship bars),
   insured badge or "Uninsured" tag.
3. **Possessions card** — compact rows: label, value, trend arrow
   (▲/▼/▬ from `trend`).
4. **Loans card** — one row per loan: label, balance (formatted as
   debt, red), monthly, arrears badge; `secured_by` renders a "secured"
   tag so the repossession stakes are legible.
5. **Finance actions** — the existing `#actions-finances` list stays
   at the bottom for anything C does not re-home as tiles.

**Action tiles** (same tile convention as the M4 person-social page):
each advertised action renders as a tile with its server `reason` when
rejected and `known_costs` when present. Examples of pinned tiles:
"Move out", "Rent this place", "Buy (mortgage)", "Sell home", "Buy car
(new/used)", "Sell car", "Insure", "Repay loan early".

**Affordability previews**: for buy/rent flows the PREVIEW figures
(deposit, monthly, term total) come from the server — either in the
action's advertised payload (TO C: include a `preview` block in the
advertised action) or in the pending's option `known_costs`/text when C
models the purchase as a v3 offer event. The client renders these
figures verbatim above the confirm/option buttons. Rule: **no figure
the server did not send**.

**Buy/sell flows**: if C serves a purchase as a pending (offer event),
the existing `.v2e-card` sheet handles the whole flow (options with
known_costs, outcome popup with server deltas) — zero new UI. If C
serves it as a direct rule action, the tile fires it and the server
reason confirms. B does not choose which; C pins that per action (§4).

**Header money surfaces**: the existing `.v2-header-cash` chip stays;
A adds net worth beside it ("$5,210 · NW $48,210" — short label, 320px
safe: cash first, net worth abbreviated `NW`, both `formatMoney`).
Truncation rule at 320: net worth chip may wrap under cash; never
shrinks the name/age cluster.

**Home feed**: money legs already journal through the existing feed
(kinds `finances` → 💰 icon per the M4 feel pass); windfalls,
breakdowns, repossessions arrive as normal journal lines/events. B adds
NO new feed fields.

**Inheritance**: renders through existing surfaces only — journal
line(s), ledger legs, and the possessions/cars/home cards reflecting
new ownership on the next render. No dedicated inheritance UI.

## 4. TO C, exact — actions and content the UI lists

Rule-action ids B renders as tiles (ids are C's to freeze; the UI reads
them from `available_actions`, so renames only need adapter sync):
- `move_out`, `rent_apply` (payload `{home_id}`), `rent_leave`
- `buy_home` (`{home_id, deposit, term_years}`), `sell_home`
- `buy_car` (`{car_id, finance: "cash|loan"}`), `sell_car` `{asset_id}`
- `insure_car` / `insure_home`, `possession_sell` `{asset_id}`
- `loan_repay_extra` `{loan_id, amount}`, `pay_arrears` `{liability_id}`

Advertising contract (existing v2 rules apply): each action carries
`reason` when unavailable ("You need a deposit of $32,000 first") and
`known_costs` on every option that costs money. C pins per action
whether it resolves directly or opens a pending (offer event); B
renders both identically. C's money v3 events (breakdowns, repairs,
windfalls, scams, landlord disputes) flow through the frozen `.v2e`
sheet and journal — no UI additions.

Catalogues (C): homes/cars/possessions with original names, country-tier
pricing, and STABLE ids — the projection's `home_id`/`car_id`/
`possession_id` are the join keys the UI displays labels from.

## 5. Example (projection → screen)

```jsonc
// state.assets (age 24, rented, one used car on a loan)
{"cash": 5210, "net_worth": -1980, "income_last_year": 30600,
 "bills_yearly": 23880,
 "home": {"home_id": "apt_2bed", "kind": "rented",
          "label": "2-bed apartment", "location_label": "Sydney",
          "monthly": 1450, "yearly": 17400, "arrears_months": 1,
          "value": null, "equity": null, "mortgage_loan_id": null,
          "insurance": null},
 "cars": [{"asset_id": "c_1", "car_id": "used_hatch_2012",
           "label": "Used Hatchback (2012)", "value": 6800,
           "condition": 62, "yearly_upkeep": 640, "insured": false,
           "premium_year": null, "loan_id": "l_1", "arrears_months": 0}],
 "possessions": [], "loans": [{"loan_id": "l_1", "label": "Car loan",
   "balance": 11000, "rate": 0.079, "monthly": 460,
   "term_years_left": 2, "secured_by": "c_1", "arrears_months": 0}]}
```
Screen: amber-bordered Home card "2-bed apartment · Sydney — Rent
-$1,450/mo · 1 month behind", tiles "Pay arrears" (reason-priced) and
"Move out"; Cars row with a 62% amber condition bar and "Uninsured"
tag; Loans row "Car loan -$11,000 · $460/mo · secured". Header: "$5,210
· NW -$1,980".

## 6. File manifest (B-owned)

- NEW `web/static/features/assets/assets.js` + `assets.css` — the
  module (Home/Cars/Possessions/Loans cards + tiles).
- `web/static/game.html` — `#v2AssetsMount` host (replaces nothing;
  legacy `#financesCard` stays in the markup for the hidden-until-live
  fallback).
- `web/static/game.js` — mount registration in `FEATURE_MODULES`,
  legacy `#financesCard` hide when `state.assets` exists, assets host
  in the `applyPendingPause()` host list.
- `web/static/adapter.js` — `normalizeAssets` (null-safety defaults,
  `cash` mirror); pure derivation only.
- `web/static/v2/shell.js` — header net-worth chip (A's `net_worth`).
- `web/static/common.js` — no change (`formatMoney` already shared).
- TESTS: NEW `tests/web_v2_assets_dom.mjs` (DOM suite vs the §5
  example, both `home: null` and populated states); browser probes at
  390/320 with `scrollWidth@320` checks; evidence screenshots in
  `docs/collab/M5/`.

## 7. Cross-refs and open questions

- `02-contracts-A.md`: §2 above is B's request on A; A's envelope,
  amortisation, yearly finance phase, arrears transitions, asset
  inheritance and migration freeze over it. OPEN to A: (a) confirm
  `arrears_months` is derivable per-liability each projection; (b)
  confirm `net_worth` includes cars/possessions at current value and
  subtracts loan balances only (not future interest).
- `02-contracts-C.md`: §4 is B's request on C; catalogue ids and the
  direct-vs-pending decision per action freeze there. OPEN to C: pin
  which purchases are offer events vs direct actions.
- `02-contracts-D.md`: journeys in §3 (rent → buy → arrears → recover;
  used car → breakdown → sell) map 1:1 onto the cards/tiles here;
  money-conservation probes assert the same ledger the UI formats.
- `02-contracts.md` (shared, Claude): conventions in §1 ride the frozen
  M3/M4 shared sections (feature modules, known_costs, decision sheet,
  pending pause); this file adds nothing to the shared layer.
- Non-goals (pinned): no client-side eligibility, amortisation, or
  affordability math; no stat-effect spoilers; no new server fields
  beyond §2 + `net_worth`; no parallel asset HTTP.
