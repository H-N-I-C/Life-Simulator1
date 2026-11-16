# Annual-events parity oracle (C-owned, schema-independent)

Source of truth for the M2c data files: every legacy annual event's
data-file entry is checked against `tests/fixtures/annual_events_oracle.json`
(exact probabilities, gates, choice sets, effect ranges, source refs).
Probabilities below are the exact legacy values from
`src/people/classes/player.py` (`random_events` / `age_up` blocks);
statistical verification lives in `tests/test_rules_oracle.py`.

| Event | Source (branch) | Trigger conditions | Occurrence | Choices | Key effects (legacy ranges) |
|-------|-----------------|--------------------|------------|---------|------------------------------|
| Lightning | random_events, age≥5 block | age ≥ 5 | 1/5000 | none | 1/2 good: all stats +100 (clamped); 1/2 bad: all stats −100, then 1/5 death |
| Thanatophobia worry | age_up, trait block | THANATOPHOBIA trait, age ≥ randint(12,17) | 1/10 | none | happiness −randint(4,6), stress +randint(3,8), health −1 |
| Vaccination | random_events, age==2 block | age == 2 | 1/2 | stay_calm / tantrum / bite | calm: none; tantrum: happy −25..35, mother rel −6..10; bite: happy −6..10, mother rel −25..35 |
| Partner breakup offer | random_events, marital 1–2 (M1-fixed) | dating/engaged, rel < randint(25,35) | 1/3 (gated) | wish_well / beg / insult | pre: happy −15..18; wish: lose partner; beg: two-draw willpower — insist prob 1−(1−w/100)² → lose partner + happy −10..20, else stays; insult: karma −4, lose partner |
| Partner divorce offer | random_events, marital 3 (M1-fixed) | married, rel ≤ randint(15,30) | 1/6 (gated) | honor / beg / insult | pre: happy −15..18; honor: divorce + settlement money×U(0.4,0.6) (if money>0, then happy −10..15); beg: two-draw willpower — insist → divorce, else stay + happy −15..22; insult: karma −4, divorce |
| Partner kidnapping | random_events, kidnapping block | partner, money ≥ 1,000,000, NOT x_in_y(karma,130) | 1/250 (gated) | pay / police / nothing | ransom = round(money×U(0.2,0.8)) sampled at offer; pre: happy −30..40; pay: −ransom, fail 1/6; police: fail 1/3; nothing: karma −8..16; success: happy +6..12 + partner rel +30..50; failure: partner presumed dead (relation death) |
| Dating offer | random_events, dating branch | age ≥ 18, age < randint(42,70), single | 1/16 (gated) | accept / reject | accept (compatibility gate): happy +12..20, gain partner; reject: none |

Yearly non-event rolls (not decisions): lottery jackpot re-roll
`round(randexpo(100000, 1000000))` (age_up `change_jackpot`); plastic-surgeon
list rotation (each of 2 slots on 1/35).

Notes for the data files:
- Two-draw willpower (breakup/divorce beg): two independent `x_in_y`
  draws, short-circuit order (Claude's M1 decision) — insist probability
  1−(1−p)², e.g. 0.75 at p=0.5.
- All gated occurrences compose: conditions → 1-in-N roll → offer creation
  samples private data (ransom, thresholds already sampled in gates) →
  resolve applies stored data only.
- Effects listed as ranges are randint ranges; the data files must sample
  once per offer and store private results (never reroll on resolve).
- Illness-phase events (flu/cold 1/20, cancer 1/2000, depression onset) are
  already extracted and tested in `test_rules_m2b.py` (illness_phase) and
  are intentionally NOT duplicated here.
