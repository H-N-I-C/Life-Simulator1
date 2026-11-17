# D2 report: M2.5 browser flows on phone viewport (Kimi D, 2026-11-17)

Evidence: docs/collab/evidence/browser/m25-flows/ (report.md + 4 PNGs,
all 390x844). Result: **86 PASS, 0 FAIL, 0 PENDING, 1 WARN** across the
four flows, run against a real server (`web.server`) with real
scenario-started lives.

## Flows (tests/browser/flows.py; hooks in tools/browser_check.py)

1. **m25-dating-app** — married_30 → `dating_app` action → the frozen
   v3 pending (`kind: m25_dating_app_date`, accept/decline both enabled
   and free, target bound once in `bindings.target`) → resolve accept →
   the match becomes a known person → two settled years → the Dating &
   weddings screen mounts in-page at phone width.
   (Also verified: a childless single 18yo gets the typed 404 "no
   eligible adults around right now" — advertised eligibility equals
   dispatch.)
2. **m25-divorce** — the production union is formed with A's typed
   `form_union` transition (in-process `service.graph_command`, the same
   call B's own screen tests use; there is no HTTP route for it — Open
   item). Then through the real web endpoint `POST /wedding
   divorce_union`: union recorded `divorced` (not erased), the shared
   dependent child untouched, wedding catalog publishes the free
   courthouse tier. WARN: the legacy `player.partner` layer stays set
   after a graph divorce (cross-layer gap, Open item for Claude).
3. **m25-editor-law** — `editor/apply edit_jurisdiction` (save-local
   override monogamous_civil → unrecognized): override persisted in
   policy-state; the EXISTING union's recognition reprojected to
   unrecognized in relationships-view (A4 end to end); no money moved;
   replay-exact retry (same request_id + recorded revision) returns the
   recorded response byte-identically; a fresh request at the stale
   revision gets the typed 409 `stale_revision`. Editor screen mounts.
4. **m25-family-tree** — `GET family-tree` projects player+spouse+child
   with real parentage edges; relationships-view exposes the
   player-known secrets surface (facts with id/kind provenance,
   reputation-as-known); the family-tree card renders in-page at phone
   width.

## Harness changes (my files only)

- `tools/browser_check.py`: Runner takes an optional in-process
  `service` handle (main passes `httpd.service`); new hooks
  `phone_viewport`, `api_get`, `graph_op`, `wedding_op`, `editor_apply`,
  `store_first_option`, `load_game_in_page` (drives the real page
  session for in-page UI mounts; flows otherwise speak HTTP only).
- `tests/browser/flows.py`: the four flows above. No existing flow
  touched except file-tail additions.

## Verification

- M2.5-only sweep: 0 FAIL / 0 REQUIRED-PENDING (report.md).
- B's suites still green after the Runner signature change:
  tests.test_web_m25_screens, tests.test_web_m25_browser OK.
- Screenshots verified 390x844 PNGs.

## Known gaps → contracts README 'Open items (D)'

- propose→wedding in-page journey blocked: no HTTP route for
  graph_command and the A3/C1 offer-respond UI path is not wired yet.
- legacy partner layer vs graph divorce divergence (WARN, routed).
- secrets-hidden-until-discovered: the view-layer projection is
  asserted; the engine-side invariant (private facts never serialize)
  gets its 500-life statistical coverage in D3's robot run.

## SELF-CHECK

- [x] dev branch; edited only tools/**, tests/browser/**, my evidence
- [x] production routes exercised (HTTP actions/wedding/editor/
      family-tree/relationships-view); in-process service only where
      the HTTP route does not exist (documented, Open item)
- [x] typed 4xx asserted where expected (stale_revision 409; the
      single-adult dating 404 verified by hand)
- [x] no skipped matrix/eligibility checks; PENDING = FAIL gate intact
- [x] replay idempotence + revision concurrency asserted byte-exact
- [x] findings routed via README Open items, no agent DMs
