# Annual-events data drafts (C-owned, pre-freeze)

Seven data files, one per legacy annual event, drafted under A's
event-schema.json shape plus C's seven review proposals. Every file carries
an `_oracle` pointer into tests/fixtures/annual_events_oracle.json (exact
probabilities/ranges the frozen content is checked against).

Adapting after freeze should be mechanical: rename `_proposal`-marked fields
to whatever A's frozen names are; everything else already matches the
current draft schema shape.

Proposal legend (see hc review 2026-11-16):
- P1: random-range deltas (randint) for stats/relationship effects
- P2: proportional/sampled money (money_pct, sampled cost/value references)
- P3: conditional outcomes (one-of / two_draw_willpower / one_in branches,
      compatibility gates), one sampled at offer creation
- P4: per-event occurrence chance (chance_n) for 1-in-N parity
- P5: no_partner condition kind
- P7: extra roles/outcomes (lose_partner, divorce, gain_partner,
      partner_death "presumed dead")
- P8: sampled gates (age/relationship thresholds drawn at offer time)

Known open shapes also marked inline: pre-effects shared by all choices
(breakup/divorce happy -15..18) are currently attached per choice; if A
adds an event-level "effects" block, move them there.
