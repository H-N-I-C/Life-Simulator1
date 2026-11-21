# Backlog (Claude hands these out when an agent reports done or goes idle)
- (moved to C) adapt the event loader + bundled.json to FROZEN event schema v2 plus v2.1 additions: money_pct "sign", "partner.compatibility" field_ref, and AUTO events (auto: true, no choices, resolved immediately, journaled; exempt from the >=2-choice soft-lock rule). Then load C's src/game/content/events/ files.
- Codex: M2a consolidated review (after S2 funding + retired projection land)
- Codex: plan the 'Romance & Family pack' (docs/collab/user-wishlist.md) after the M2 consolidated review; it runs right after M2, before or alongside M3
- A (small): add dev scenario 'wealthy_40' (rich adult, for kidnapping/ransom row 69) through real transitions or a seeded lottery win; tell D
- B: the intermittent /api/avatar.png failure on the creator page (D's flake)
- A (after the fix round, small): a dev-only 'force_event' hook (only with --dev; scenario or command option) that offers a named event on the next decision_offer, so D can prove rare events (kidnapping row 69). Tell D.
- Codex: THOROUGH M2.5 evaluation when M2.5 is finished (gate in docs/collab/M2.5/01-spec.md)

## NEXT after v2 redesign fixes are verified (user-approved 2026-11-18): "Undo last year"
- A: snapshot the full game state before each Age; restore = revert to that snapshot (ledger, graph, knowledge, pendings, RNG state). One level or N levels TBD in the contract; it must be replay-safe (request_id/revision); never lets an 18+ rule or a death be bypassed in a way the forward path wouldn't allow.
- B: an "Undo year" button on Home (disabled with a reason when there is no snapshot), with confirmation.
- D: browser test: age → undo → age gives a consistent state; money is conserved.

## Polish found in Claude's play check (2026-11-20, after the undo/journal round)
- Event-outcome journal lines lack context ("I read all four and returned…", "I gave them nothing…"): prefix with or merge in the event's prompt (C).
- The father's chip avatar renders as a dark-red blob at small size (appearance/web).
- "I am starting middle school" has no period / tone mismatch with the new lines (legacy text).

## M4, deferred from Kimi A's overloaded task (2026-11-21): A's NEXT task, one at a time
- the person view for social-circle NPCs (Person page 404s for classmates/coworkers)
- NPC surnames shouldn't reuse the family surname unless related
- the legacy new_friend event → a real Friends-group friend
- classmates fade to 'former classmates' after graduation
- (C, next task) relationship drift lines are age-inappropriate for babies/toddlers: at age 1, 'I helped Mum out more around the house'. Baby/toddler bands need age-true lines, or none.
- (A, later) A's relationship_lines.py also gives babies/toddlers grown-up lines ('My relationship with {name} grew stronger this year' at age 1); same age-fit treatment as C did.
- (A, next) the person view labels a classmate as 'relative' in the Person page header; use the social kind/circle ('Classmate · Acquaintance').
- (A, next) the social-NPC surname generator must exclude family surnames (D liveness: Wilfred Stafford collision, 1/20).
