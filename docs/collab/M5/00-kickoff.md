# M5 — Money, homes, cars (Claude, 2026-11-21). M4 is APPROVED (Claude review: full suite 1123 OK, liveness 20/20, real browser play, 0 kin-bound romance events in 15 lives).
Goal (user-testable): renting vs buying a home (deposit, mortgage, repayments, missed payments → arrears/repossession), moving out of the parents' home, bills (rent, utilities, phone), cars (buy new/used, loans, insurance, upkeep, selling, breakdowns), possessions with value that changes, and inheritance of assets on death (with M2's cash succession). One ledger; every payment is a ledger leg; affordability previews before every choice; free/low-cost paths so no life gets stuck.
Ownership (each owner writes its contract section in ITS OWN FILE, never a shared file):
- A (engine): assets/liabilities model (owned assets, loans/mortgages with amortisation, arrears), the yearly finance phase, the asset part of inheritance, migration (no invented assets for old saves): 02-contracts-A.md
- B (UI): the Assets tab (homes, cars, possessions, loans; buy/sell/rent flows with affordability previews), Home header money/net worth, 390/320: 02-contracts-B.md
- C (content/rules): homes/cars/possession catalogues (original names/prices by country tier), rule actions (rent, buy, sell, take/repay a loan, insure), money-related v3 events (breakdowns, repairs, windfalls, scams, landlord disputes): 02-contracts-C.md
- D (QA): an M5 validator, journeys (move out → rent → save → buy with a mortgage → miss a payment → recover; buy a used car → breakdown → sell), money-conservation/ledger probes, liveness (money doesn't explode or trap lives): 02-contracts-D.md
Stage 1 = contracts only (one task). Claude freezes. Same rules as before.

## FREEZE v1 (Claude, 2026-11-21): 02-contracts-A/B/C/D.md are FROZEN, with these rulings
1. arrears_months is derived per liability on every projection (A). net_worth = cash + current value of owned assets (homes, cars, possessions) − outstanding loan/mortgage principal; future interest is not subtracted.
2. Purchases/rentals/loans/sales are DIRECT rule actions (C) with server-side previews in known_costs + a confirm step in the UI (B). Only event-driven deals (a friend selling a car, an auction, a landlord offer) are stored-offer pendings. No stored affordability-quote pending: the command recomputes and refuses with a typed 409 'price changed' if the preview is stale.
3. take_loan payload = {product_id, amount} (C's and A's lists align to this).
4. Inheritance: the successor inherits assets WITH their liabilities (a mortgaged home carries its mortgage); negative-equity estates follow A's section; M2 cash succession unchanged.
5. Repossession/arrears thresholds as written in A's section (A's numbers are the source of truth; D asserts them verbatim).
Stage 2: implement your slice against the frozen files. One task each. Same rules.
