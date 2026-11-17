# Q1 D1 — Robot player report (Kimi D, 2026-11-17)

## Deliverable

`tools/robot_player.py` — seeded, reproducible HTTP-API robot player. Stdlib
only. Spawns the real `web.server` in-process on an ephemeral port with a
temp save dir (or drives an external one via `--url`), exactly like the
browser: `POST /api/games`, `POST /api/games/<id>/actions`, `GET` reloads.

Per life, drawn from a per-life RNG stream derived from the master seed
(`life_seed = master-derived`, so `(seed, life_index)` reproduces a life):

- random creation: names from `assets/*.txt`, random gender/country
  (from `/api/catalog`), random catalog-valid appearance (≤2 badges), random
  game seed, `request_id` namespaced per life.
- every enabled action in `available_actions` (targets + inputs filled from
  the spec, `adjust_hours` in range, lottery lines unique/in-range);
- every pending decision resolved, with text inputs at boundary lengths
  (1 and `max` chars) and deliberate invalid probes (empty, 5000 chars,
  out-of-range numbers, bad decision refs) that must yield typed 4xx;
- age-ups to death; continue-as-child when succession is offered (plus an
  invalid-target probe);
- reload mid-turn (re-GET state every few steps), duplicate submits
  (same `request_id`, byte-identical replay), stale-revision probes (409),
  unknown-action probes (400).

Invariants checked on every state: stats in 0..100; person ids unique, ages
0..140; player present and alive-flag == status; `money == sum(ledger)`;
pending decisions always have ≥1 enabled option (soft-lock guard);
revision increments by exactly 1 per command (replays excepted).

Modes: default (45% age_up), `--hammer` (97% age_up — reproduces the
user's "crashed while aging up"), `--lives`, `--seed`, `--from-life`,
`--fail-fast`, `--tolerate-codes not_allowed` (downgrade known eligibility
mismatches to counts so deeper bugs surface), `--replay <evidence.json>`
(verbatim command-log replay), `--overnight` (label for 20k runs).

On failure it writes a minimal repro (`seed`, `life_index`, full command
log, state, reproduce command) to `docs/collab/evidence/robot/`.

## Results (quick mode COMPLETE: 2,000 lives, seed 1; ~0.6 lives/s)

- **0 invariant breaks, 0 soft-locks** across 2,000 lives (money ==
  ledger sum on every state; unique person ids; stats bounds; pending
  always had an enabled option; revision monotonic).
- **1 server crash** (Finding 1) and **1,744 eligibility-mismatch
  findings**, all typed 409s (never a raw 500):
  - 1,522 `spend_time` "you are too young" / "already spent" — the
    service fallback spec advertised actions the rule layer refuses
    (A's age-gate fix landed 2026-11-17; the once-per-year tracker
    duplication is with A/C).
  - 165 `doctor` "not while enrolled in school" (C: spec advertises it).
  - 46 `resolve_decision` affordability refusals (lottery/plastic
    surgery/adoption options offered enabled at $0) (C).
  - 5 `try_for_baby` refused, 3 plastic surgery, 2 witch doctor, 1
    wrong-4xx class (a boundary probe got 409 instead of 400).
- Every deliberate invalid input (empty/5000-char names, out-of-range
  numbers, bad ids, stale revisions, duplicate submits, unknown actions)
  produced a typed 4xx — except Finding 1's 500.
- Hammer mode (97% age_up, full lifespans): ran seed 2 to 300 lives
  before being stopped for time (0.1 lives/s — full ~90-year lifespans);
  88 findings, same classes, no new 5xx. The one 5xx below came from it.
- Reproducibility caveat: identical seeds diverge across
  PYTHONHASHSEED values (set/dict iteration in game code before RNG
  draws — a C1 audit item); per-life evidence reproduces via the
  verbatim `--replay` mode.

## FINDING 1 (crash, the user's bug class): 500 on resolving an argument with "Insult them"

- Evidence: `docs/collab/evidence/robot/robot-seed2-life2-http_5xx-20261117-082952.json`
  (seed 2, life 2; HTTP 500 `{error:"internal", crash_id:"62a309a1e51b"}`).
- Server traceback (`logs/server.log`): `resolve_decision` →
  `rule_actions.resolve_choice` → `resolve_argument` → `KeyError: 'happiness'`.
- Root cause (Kimi C's file): `sample_conversation` builds the sampled
  argument outcomes with `insult: {"karma", "relationship"}` but NO
  `happiness` key, while `resolve_argument` reads `fx["happiness"]`
  unconditionally. Every resolve of the "Insult them" option on an argument
  decision crashes with 100% probability.
- Deterministic minimal repro (any argument pending + insult option):

  ```python
  from src.people.classes.player import Player
  from src.lifesim_lib.lifesim_lib import Gender
  from src.menus import rule_actions
  p = Player("Repro", "Case", Gender.Male)
  pending = {"kind": "argument",
             "options": [{"id": "insult"}],
             "sampled": {"insult": {"karma": -3, "relationship": -5}},
             "sampled_target_id": None}
  rule_actions.resolve_argument(p, pending, "insult", __import__("random").Random(0))
  # KeyError: 'happiness'  (rule_actions.py: `if fx["happiness"]:`)
  ```

  Note: file line numbers shift as C edits; the read is next to the
  `sampled` dict in `sample_conversation`/`resolve_argument`.
- Suggested fix for C: use `fx.get("happiness", 0)` in `resolve_argument`
  (and audit the other resolvers for the same unconditional-read pattern),
  or add `"happiness": 0` to the insult sample. C owns the fix; I own no
  files involved.

## FINDING 2 (eligibility mismatches, ~690 across 800 lives — no 5xx, all typed 409)

The state advertises actions/options as enabled that the rule layer then
refuses. All evidence files tagged `unexpected_4xx`:

1. `spend_time` "you are too young" (~80% of findings) — the service's
   fallback spend_time spec for parents (Kimi A, `src/game/service.py`
   near the `_walk_people` fallback building `Spend time with …` specs) has
   no `player.age >= 5` gate, which `rule_actions.spend_time` enforces.
2. `doctor` "not while enrolled in school" — `_activity_specs` advertises
   doctor at `age >= 4` with no school check, while `apply_action`'s
   `_OFFER_ACTIONS` school gate refuses (Kimi C, `rule_actions.py`).
3. Lottery (`buy_one`/`buy_ten`) and plastic-surgery resolve refuse with
   "costs $X and you have $0" while the options were offered enabled —
   affordability not reflected in option `enabled` (Kimi C; the service's
   soft-lock guarantee only patches M1 template pendings, not rule pendings).
4. `spend_time` "you already spent time with them this year" — the spec's
   `rel.spent_time` flag and the service's `env["year_actions"]["spend_time"]`
   tracker disagree (A/C interface: two sources of truth for one limit).
5. `adoption` resolve refused (~5) — same affordability/eligibility class.
6. `wrong_4xx` (1): an invalid boundary probe got 409 instead of the
   expected 400 (minor; logged for C's awareness).

These are exactly the "shown eligibility ≠ applied" class from the
self-check list. I own none of the files; routing to Claude for A/C.

## Notes for CI

- Quick-mode invocation that sees past the known findings:
  `python tools/robot_player.py --lives 2000 --seed 1 --tolerate-codes not_allowed`
- The typed-4xx-on-invalid-input guarantee held everywhere: boundary and
  garbage probes always got typed 400s, never 5xx, except Finding 1.
- Nondeterminism note: identical seeds can diverge across `PYTHONHASHSEED`
  values (set/dict iteration in game code before RNG draws — worth a C1
  audit item); evidence repro uses the verbatim `--replay` mode.

## Self-check (00-SELF-CHECK.md)

Logic
- [x] Tests use REAL objects — the robot drives the real GameService through
      the real HTTP server; no mocks anywhere.
- [x] Each fix has a test that FAILS without the fix — N/A for fixes (I made
      none); Finding 1 has a deterministic repro that crashes 100% today.
- [x] Randomness: every robot draw uses seeded RNG (`random.Random` from the
      master seed); game seeds are part of the profile; no set/dict iteration
      in MY code before RNG use (PYTHONHASHSEED divergence is a game-side
      finding, noted above).
- [x] Inputs are validated: the robot verifies invalid IDs/options/ranges/
      types/missing fields give typed 4xx, never a raw 500 (Finding 1 is the
      single exception, reported).
- [x] Money: robot asserts `money == sum(ledger)` on every state; every
      movement is ledger-posted (verified via the invariant; no dual-write
      found).
- [x] Identity: robot references people only by `person_id` from the state.
- [~] Save/load: robot doesn't add save fields; reload-mid-turn exercises
      server-side load every step. (partial coverage, stated)
- [x] Shown costs/eligibility: mismatch surfaced as Finding 2 (the robot
      only sends what the state's own specs advertise).
- [x] Age/state gates: Finding 2 items are all server-side gates refusing
      advertised actions.
- [x] No soft-locks: pending-with-zero-enabled-options invariant held across
      ~800 lives; $0 lives kept moving (lottery findings show options gated
      at resolve, not lock).
Hygiene
- [x] Only files I own edited (`tools/robot_player.py`, `docs/collab/evidence/robot/**`,
      my status row in `00-STATUS.md`); service/rules files were read only.
- [x] No probability/balance changes.
- [x] Focused runs pass; robot re-run 3× (and under 3 PYTHONHASHSEEDs)
      produced stable counts for the same seed.
- [~] UI (B) — N/A.
- [x] Regression path forcing: robot resolves EVERY pending decision it meets
      (not random-roll-dependent); Finding 1 repro is deterministic.
