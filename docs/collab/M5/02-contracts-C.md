> FROZEN v1 by Claude 2026-11-21, with the rulings in 00-kickoff.md.
# M5 contracts — §Content & rules (C) DRAFT v1

Owner: Kimi C (pane t15). I am the only writer of this file. Frozen
sections live in the sibling owner files: `02-contracts-A.md` (A:
assets/liabilities model, loans/mortgages + amortisation, arrears,
yearly finance phase, asset inheritance, migration), `02-contracts-B.md`
(B: Assets tab, buy/sell/rent flows with affordability previews, Home
header money/net worth, 390/320), `02-contracts-D.md` (D: validator,
journeys, money-conservation probes, liveness). Cross-references below
name the seams I need FROM each owner; my open items at the end list
exactly what I must hand over.

Scope (kickoff): homes (rent/buy/mortgage/arrears), cars
(buy/loan/insurance/upkeep/sell/breakdowns), possessions with drifting
value, money v3 events. One ledger; every payment a ledger leg;
affordability preview before every choice; free/low-cost paths so no
life gets stuck.

---

## 1. Data (C-owned catalogues)

New package `src/game/content/assets/` with a loud-validating loader
(same pattern as `src/game/content/social/`). All catalogue ids are
stable slugs; all money amounts are NON-NEGATIVE ints in the game's
currency unit.

### 1.1 `economy_tiers.json` — country → tier

The game has no country economy model today; M5 prices must vary by
country tier without inventing per-country economics. C owns a 3-tier
table over the frozen country list (`src/countries/countries.py`
COUNTRIES):

```json
{
  "schema_version": 1,
  "tiers": {
    "high":   {"countries": ["Australia", "Canada", "Norway", "Switzerland", "United States"], "multiplier": 1.0},
    "medium": {"countries": ["Brazil", "China", "Poland", "South Africa", "Turkey"], "multiplier": 0.55},
    "low":    {"countries": ["Afghanistan", "Bangladesh", "Ethiopia", "Nepal", "Vietnam"], "multiplier": 0.22}
  },
  "default_tier": "medium"
}
```

Every country in COUNTRIES appears in exactly one tier (loader enforces
coverage; unknown country at runtime → `default_tier`). Accessors:
`assets.tier_for_country(country) -> str`,
`assets.tier_multiplier(tier) -> float`. A applies the multiplier when
quoting any catalogue price (A's finance phase and affordability
previews); C applies the SAME accessor in rule-action gates so specs
and dispatch never disagree. This is the single source of price
variation — catalogue entries store ONE base price, never per-country
price lists.

### 1.2 `homes.json` — rental listings + purchasable homes

```json
{
  "schema_version": 1,
  "listings": [
    {
      "home_id": "studio_flat",
      "label": "Studio flat",
      "style": "studio | flat | terrace | detached | apartment",
      "bedrooms": 1,
      "tenure": ["rent", "buy"],
      "base_price": 42000,
      "base_rent_per_year": 3600,
      "deposit_months": 1,
      "bills_estimate_per_year": 900,
      "condition": "fair | good | excellent",
      "min_age": 18
    }
  ]
}
```

Loader rules: unique `home_id`; `base_price > base_rent_per_year * 8`
(cheaper to rent short-term) and `< base_rent_per_year * 40` (buying
pays off); `tenure` non-empty; `deposit_months` 0–3; at least 8 rent
listings and 8 buy listings across styles/bedrooms; every rent listing
with `base_rent_per_year * tier_multiplier(low) > 0` affordable on a
minimum-wage adult income (checked against A's income floor accessor,
see open items). Original names only (no real estate brands).

### 1.3 `cars.json` — cars new/used

```json
{
  "schema_version": 1,
  "cars": [
    {
      "car_id": "compact_hatch",
      "label": "Compact hatch",
      "class": "city | compact | family | estate | van",
      "base_price_new": 15000,
      "used_discount": 0.55,
      "upkeep_per_year": 650,
      "insurance_group": 2,
      "reliability": "high | medium | low",
      "breakdown_weight": 1,
      "min_age": 18
    }
  ]
}
```

Loader rules: unique `car_id`; `used_discount` in (0.2, 0.8);
`insurance_group` 1–5 (feeds A's insurance premium formula);
`reliability` weights breakdown-event eligibility; at least 8 cars
spanning classes and price bands, including one sub-2000 used city car
so a poor life can still own a car. Original model-flavoured labels
only (no real marques).

### 1.4 `possessions.json` — possessions with drifting value

```json
{
  "schema_version": 1,
  "possessions": [
    {
      "possession_id": "secondhand_guitar",
      "label": "Secondhand guitar",
      "category": "instrument | electronics | sports | collection | appliance",
      "base_price": 220,
      "value_depreciation_per_year": 0.08,
      "condition": "fair | good | excellent",
      "min_age": 0
    }
  ]
}
```

Loader rules: unique id; `value_depreciation_per_year` in [0, 0.5]
(0 = appreciates, used for collections); at least 12 possessions across
categories and price bands, at least 3 under 150 base. A owns the
stored value drift (`state["assets"]`); C owns only the catalogue
constants.

---

## 2. Rule actions (C-owned, `src/menus/rule_actions.py`)

All commands follow the frozen M3 dispatch dialect: `{action, ...}`
payloads via the shared dispatcher, admitted through `ACTION_IDS`,
typed `RuleError` (400 invalid_input / 404 not_found / 409 not_allowed)
BEFORE any mutation, and a single-source gate function shared by
`action_specs` and the command so the projection can never disagree
with dispatch. Money moves ONLY through A's ledger poster (one posting
request per leg, per-year occurrence keys); C never mutates balances.

| action | payload | gate (409 reason) | legs (occurrence key) |
|---|---|---|---|
| `rent_home` | `{home_id}` | already renting; under 18; cannot afford deposit+first year rent | `deposit:{home_id}` once, then rent via A's yearly finance phase |
| `end_tenancy` | — | not renting | none (A closes tenancy) |
| `buy_home` | `{home_id}` | already own; cannot afford deposit (rest is A's mortgage) | `deposit:{home_id}` once |
| `sell_home` | — | do not own | proceeds via A (sale quote) |
| `take_loan` | `{loan_id, amount}` | existing loan of same id; amount outside A's band | `loan_borrow:{loan_id}` once |
| `repay_loan` | `{loan_id, amount}` | no such loan; arrears rules per A | `loan_repay:{loan_id}` per call |
| `buy_car` | `{car_id, condition: "new"|"used"}` | under 18; cannot afford (cash or loan-approved) | `purchase:{car_id}:{condition}` once |
| `sell_car` | `{asset_id}` | not owned | proceeds via A |
| `insure_car` | `{asset_id}` | not owned; already insured | `insurance:{asset_id}` per year via A |
| `buy_possession` | `{possession_id}` | cannot afford | `purchase:{possession_id}` once |
| `sell_possession` | `{asset_id}` | not owned | proceeds via A |

Mutations of asset/loan/tenancy STATE go through A's published appliers
only (A's §2 names them; C calls, A owns). C commands also journal a
first-person, fact-true line (existing journal pools; new `asset_lines`
pool for rent/buy/sell/breakdown beats, picked via the frozen
`pick_year_line(..., pool=)` cosmetic stream).

**Affordability rule (frozen)**: every paid command checks cash
affordability (after A's tier multiplier) BEFORE mutation and refuses
with 409 + a spec `reason` string; `action_specs` advertises the same
costs in `known_costs` (B dialect, amount = tier-adjusted int) so B's
flow can preview. No command may leave a life unable to pay a
compulsory bill — A's finance phase owns compulsory bills and arrears;
C never creates a mandatory cost without a free alternative nearby
(events always carry a free option, per M3 soft-lock rules).

**School gate**: all home/car/loan commands are blocked while
`p.is_in_school()` (added to `_SCHOOL_BLOCKED_ACTIONS`);
`buy_possession` stays legal for any age with `min_age` respected and
pocket-money prices.

## 3. Projections / specs

- `asset_specs(context)` (C-owned, additive to `action_specs`): one
  spec per action above with `{id, category: "assets", label, enabled,
  reason, limits, known_costs}` and `target_id/target_label` where a
  specific asset is implied. B renders flows from these.
- `state["assets"]` (A-owned): holdings `{asset_id, catalogue_id,
  condition, current_value, tenure}`, loans `{loan_id, principal,
  outstanding, apr, arrears}`, tenancy `{home_id, rent_due}`. C READS
  it for gates and v3 condition leaves; never writes it.
- Catalogue labels for B's Assets tab: `assets.catalogue()` accessor
  (id → label, category, base stats) so B never parses content JSON.

## 4. Money v3 events (C-owned content)

New files `src/game/content/events/m5_{home,car,money}.json`, all
schema v3, riding the single dispatch path (prefixes `m5_home_`,
`m5_car_`, `m5_money_`), served by C's own yearly phase registered in
`_PHASES` (pattern: `romance_events_phase`; A wires `PHASE_ORDER` after
the finance phase — A open item). Families:

- **home** (≥10): rent rise letter, landlord inspection, plumbing
  emergency, deposit dispute, nightmare neighbour, moving scam,
  mortgage rate letter (owner), repair-or-live-with-it (owner),
  sublet offer, eviction warning (in-arrears gate → free legal-aid
  exit).
- **car** (≥10): breakdown (reliability-weighted eligibility),
  flat battery, speeding fine, insurance renewal shock (insured gate),
  uninsured crash scare (uninsured gate, free exit), buyer lowball
  (selling), pothole damage, petrol vs electric bill, road-trip spend,
  parking ticket, catalytic converter theft (city gate).
- **money** (≥10): tax refund windfall, found wallet, payroll mistake
  (overpaid: keep-or-report), phishing scam, too-good car deal scam,
  mate's loan request, forgotten subscription bleed, charity ask,
  prize-draw "win", elderly-relative gift.

**New v3 condition leaves** (`src/game/events.py`, C-owned, added to
`V3_CONDITION_KINDS` so load-time validation covers them; read-only
probes of A's `state["assets"]`, absent → False):

```json
{"kind": "asset_state", "asset_type": "home|car|loan|possession",
 "state": "renting|owned|insured|in_arrears|has_any"}
```

Event invariants (D validates): actor-only; every paid choice carries a
free alternative; costs use the existing v3 `cost.money` affordability
gate (loader-enforced, events.py:526); outcome lines name their subject
(M3c context rule, pinned by `tests/test_event_outcome_context.py`);
min_age 18 for car/home money events except scam/windfall events
(min_age 0 with age-true wording); no real brands; original text.

## 5. Frozen example (normative)

```json
{
  "id": "m5_home_plumbing_emergency",
  "schema_version": 3,
  "title": "The Ceiling Gave In",
  "text": "A brown bloom spread across the ceiling overnight, and a drip started over my bed.",
  "interaction": {"kind": "home_repair"},
  "conditions": {"min_age": 18,
                 "all": [{"kind": "asset_state", "asset_type": "home",
                          "state": "renting"}]},
  "roles": {"actor": {"kind": "person", "select": "player"}},
  "audience": {"scope": "private"},
  "choices": [
    {"id": "call_landlord", "label": "Call the landlord",
     "effects": {"happiness": 2},
     "outcome_text": "The landlord sent a plumber by evening. I slept in the living room, but the drip stopped."},
    {"id": "fix_myself", "label": "Patch it myself",
     "cost": {"money": 120},
     "effects": {"stats": {"smarts": 1}}, "health": -1,
     "outcome_text": "I bought sealant and a ladder and patched the ceiling myself for $120. It held, mostly."},
    {"id": "bucket_it", "label": "Put a bucket under it",
     "effects": {"happiness": -3, "health": -2},
     "outcome_text": "I set a bucket under the drip and lived with the stain. The smell got worse all year."}
  ],
  "weight": 1
}
```

Note the free option first; the paid option's cost is previewed and
affordability-gated by the existing loader rule.

## 6. File manifest (C)

- `src/game/content/assets/{economy_tiers,homes,cars,possessions}.json`
- `src/game/content/assets/__init__.py` (loader + accessors, loud
  validation, tier coverage over COUNTRIES)
- `src/game/content/events/m5_{home,car,money}.json`
- `src/game/events.py` — `asset_state` condition leaf (additive)
- `src/menus/rule_actions.py` — commands, gates, `asset_specs`,
  `_SCHOOL_BLOCKED_ACTIONS` addition, `m5_events_phase`
- `src/game/content/journal/asset_lines.py` (asset beat flavour pool)
- Tests: `tests/test_assets_content.py` (catalogue validity, tier
  coverage, price sanity), `tests/test_rules_asset_actions.py` (per-action
  gates through real `svc.command`, typed errors, ledger exactness ×3
  hash seeds), `tests/test_rules_m5_events.py` (gates per family,
  affordability, free exits)

A-owned (NOT mine): `src/game/features/assets/**`,
finance-phase ordering, mortgage/loan amortisation, arrears/repossession,
asset inheritance, migration, ledger `CATEGORIES` extension.

## 7. Open items

**From A** (blocking my commands/events):
1. Published appliers `features/assets/core.py`: `rent_home`, 
   `end_tenancy`, `buy_home` (deposit + mortgage creation), `sell_home`,
   `take_loan`, `repay_loan`, `register_car`, `sell_asset`,
   `set_insured` — signatures per your §2; C calls, A owns state.
2. Affordability accessor: `assets_core.affordable(env, amount) -> bool`
   and `assets_core.quote(env, catalogue_id, tier) -> int` (applies my
   tier multiplier) so specs and dispatch share one quote.
3. Ledger categories: `housing`, `vehicle`, `possessions`, `insurance`,
   `maintenance` added to `finance.CATEGORIES` (or a mapping in your
   §2 if you prefer existing ones).
4. Yearly finance phase order: compulsory bills (rent, utilities, phone,
   insurance, upkeep) AFTER income, BEFORE my `m5_events_phase`; expose
   `state["assets"]` projection.
5. Migration: old saves start assetless (no invented assets) — my gates
   then simply advertise nothing; confirm in your §4.

**From B**: Assets tab consumes `state["assets"]` + `assets.catalogue()`
+ my `asset_specs` `known_costs`; affordability preview component shows
the tier-adjusted int I advertise; confirm the spec dialect in your §3.

**From D**: validator matrix additions (catalogue loader rules from
§1, event invariants from §4, money-conservation probes across my
commands); journeys (move out → rent → save → buy → arrears → recover;
used car → breakdown → sell) driven through real routes; liveness
(free exits always reachable, no life trapped by bills).

**From Claude**: freeze pass; one naming check — I reuse the word
"assets" for the content package while A owns `features/assets`;
happy to rename mine to `content/property/` if A prefers.

## 8. Cross-refs

- `02-contracts-A.md` §assets/liabilities model → my §2 command table
  (appliers), §3 `state["assets"]` shape, §7 items 1–5.
- `02-contracts-B.md` §Assets tab → my §3 projections and §7 B item.
- `02-contracts-D.md` §validator/journeys → my §1 loader rules and §4
  invariants.
- Frozen schemas I extend (C-owned files): v3 event schema
  `docs/collab/M2.5/contracts/event-schema-v3.md` §2 (one new leaf,
  additive); M2 ledger schema `docs/collab/M2/` (A extends CATEGORIES).
- Prior art: M4 social contract `docs/collab/M4/02-contracts-C.md`
  (same command/spec/gate discipline).
