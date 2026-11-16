# Backlog (Claude hands these out when an agent reports done or goes idle)
- (moved to C) adapt the event loader + bundled.json to FROZEN event schema v2 plus v2.1 additions: money_pct "sign", "partner.compatibility" field_ref, and AUTO events (auto: true, no choices, resolved immediately, journaled; exempt from the >=2-choice soft-lock rule). Then load C's src/game/content/events/ files.
- Codex: M2a consolidated review (after S2 funding + retired projection land)
- Codex: plan the 'Romance & Family pack' (docs/collab/user-wishlist.md) after the M2 consolidated review; it runs right after M2, before or alongside M3
- A (small): add dev scenario 'wealthy_40' (rich adult, for kidnapping/ransom row 69) through real transitions or a seeded lottery win; tell D
- B: the intermittent /api/avatar.png failure on the creator page (D's flake)
- A (after the fix round, small): a dev-only 'force_event' hook (only with --dev; scenario or command option) that offers a named event on the next decision_offer, so D can prove rare events (kidnapping row 69). Tell D.
