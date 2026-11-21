# v2 redesign contracts (Kimi B, 2026-11-18) — §Shell + §Tokens + §Home +
# §Choice FROZEN; §Person FROZEN v1 (A)

Status: §Shell and §Tokens are frozen by B (sole shell integrator). §Person
is FROZEN v1 by A. §Home and §Choice are frozen by B as of Stage 2. Changes
require B's sign-off (§Shell/§Tokens/§Home/§Choice) or A's (§Person);
consumers must not code against unfrozen sections.

Everything here sits on the accepted M2.5 engine. All M2.5 behaviour — retry/
revision machinery, privacy filtering, quotes, 18+ gates, offline retry pill,
decision pill, modal focus trap — is retained unchanged; the v2 shell is a
presentation layer over it. No mock-only data (Agreement %, fake jobs/cities,
Gift/Holiday/Call) may be rendered as real anywhere in v2.

## §Shell — navigation, mounts, hooks

### Screens (frozen route names)

Five primary destinations, supplied by the v2 bottom tab bar, plus a person
overlay and a secondary "More" menu. Old panels are re-grouped, never dropped:

| v2 screen | Old panels mounted (all remain reachable) |
|---|---|
| `home` (default) | playerCard, turnCard, screen-summary (timeline/journal) |
| `job` | education card + education actions; work card + work actions. Education stays visible whenever its actions exist — never hidden because the player has a job |
| `assets` | finances card + finance actions. Honest "Money" subheading; no property-portfolio implication |
| `people` | family section card, family roster, relationship actions, family actions; hosts the person overlay |
| `activities` | activities actions, health card + health/treatment actions, General card (General actions + Character editor / Family tree / Dating & weddings toggles) |
| overlay `person` | Person detail module (C), mounted by B in a host inside `people` |

The turn-progress card (turnCard) shows player-facing text only — internal
turn ids (`t_…`) and phase names (`year_close` etc.) never render in the
player view (feel-pass F2; the shell never fabricates a dev panel).

Secondary routes live in the v2 header "More" menu (frozen): All lives,
Character editor, Family tree, Dating & weddings. The global website topbar
(Creator / Gallery links) belongs to the lives list / creator only — inside a
running life the game is full-screen and the topbar is hidden (feel-pass F1).
Decision pill, retry pill, offline banner and the
modal render above the v2 chrome (z-order contract: chrome never covers them).
Secondary-route entries are IDEMPOTENT and reveal their owning surface: More →
Character editor selects the Activities screen (the General card owns the
editor toggle) and opens the editor only if closed — never toggles it shut.

game.html groups the existing panels into `<section id="screen-{home,job,
assets,people,activities}" class="screen">`; the legacy `tabbar` is replaced
by the v2 bottom bar (the old screen *content* is fully preserved inside the
new sections — no panel is removed).

### Bottom bar + stats dock (feel-pass F3/F7, 2026-11-20)

The v2 bottom bar is **Occupation · Assets · AGE · Relationships ·
Activities**: four section tabs with ONE big round Age button centred between
Assets and Relationships (`.v2-agebtn`). The Age button is shell chrome, not
Home content; Home is reached via the header "Life" link. Age eligibility and
its gate reason come from the server's advertised `age_up` spec; the reason
renders as a one-line note in the stats dock (`.v2-agenote`) so the button
never dead-ends silently.

A compact stats dock (`.v2-dock`: Happy/Health/Smarts/Looks, colour + % label)
is pinned above the bottom bar, always visible while alive; it hides honestly
when the projection carries no stats. Bars animate to new values and flash
green/red on change (120–200 ms transitions; `prefers-reduced-motion`
collapses everything to instant state changes — feel-pass F8).

### Navigation API (shell-owned, frozen)

- `selectScreen(name)` — existing game.js mechanism; the v2 tab bar and
  Home link drive it through the history-aware wrapper (below).
- `navigate("person", { personId })` — selects `people`, opens the person
  overlay for that exact ID. Two people with identical names resolve by ID.
- Back stack/history (frozen per Codex back-evaluation, 2026-11-18):
  - Every user navigation pushes a tagged entry: `{screen}` | `{person:id}`
    | `{panel:which}` | `{lives}`. Entries carry the game id, a persistent
    history index and the scroll position (captured via `replaceState` when
    leaving a destination).
  - **History position belongs to the entry**, not a process-local counter:
    the index is stored on the entry and restored from incoming history, so
    Forward + Back and in-app Back share one position; the in-app root guard
    prevents ejecting the player from the page.
  - **Reload restores the destination**: initial rendering reapplies the
    saved screen/person/panel and usable depth (reload on Money → Money;
    Back → Job). The base entry carries the game id so "All lives" can
    return to the original life; older payload entries without id/index are
    repaired on the way out.
  - **Open is idempotent**: identical destination requests never push
    duplicate entries; an already-open requested panel is preserved, not
    toggled or rebuilt. Editor entries retain an explicitly selected
    person ID for restoration.
  - **Openers reveal their owning surface**: feature panels (editor, family
    tree, dating & weddings) select Activities, close other panels, and
    preserve an already-open requested panel; legacy toggle buttons and
    More routes feed the same navigation intent. History restoration
    dispatches directly — it never pushes.
  - **Person Back consumes the person entry** through the history hook (no
    false intermediate pushes); error-state Back uses the same path. Person
    Edit closes its local overlay without a navigation entry, then opens
    the exact editor route.
  - Back restores the previous screen **with its scroll position**.
  - Escape closes menu/overlay/panels where safe; while a server decision
    is pending, Escape collapses the sheet to the pill and navigation never
    discards or resolves it (verified: stored decision id/revision and Age
    gating unchanged by Back/Forward).
- `hooks.onEdit(personId)` — Person module's Edit control requests the
  editor for the exact mounted person ID (the module always passes
  `detail.id`, never a name). The shell: reveals the editor's owning
  surface (Activities), opens the editor idempotently (never toggles an
  open panel shut) and pre-selects THAT person ID; identical names resolve
  by ID. Editor keeps preview/apply, pending supersession and revision
  refresh semantics.

### Module mount lifecycle (frozen — C already implements this shape)

```js
const handle = mountX(root, hooks, data);  // → { update(data), unmount() }
updateX(root, data);                        // re-render in place
unmountX(root);                             // detach + clear root
```

- `root`: an empty HTMLElement owned by the module; the shell only shows/
  hides/reparents the host, never mutates module content.
- After every successful shell `refresh()` the shell calls `update(data)` on
  the mounted module with the freshly projected data (same person ID →
  update; changed/missing ID → unmount).
- Mount failure (e.g. typed `person_not_found`) leaves the overlay closed and
  surfaces an honest toast — never fabricated content.

### shellHooks (frozen — extends M2.5 hooks, game.js)

```js
shellHooks = {
  getState(),                                  // normalized adapter state
  refresh(),                                   // retry/revision-safe re-read + render
  sendAction(actionId, payload, targetId),     // persisted op + replay ledger
  resolveDecision(decisionId, optionId, inputs),
  navigate(screen, params?),                   // NEW (v2)
  onBack?, onEdit?(personId),                  // consumed by feature modules
  renderAvatar?(el, appearance, age, size),    // optional override; default common.js
}
```

All mutations stay on the existing retry-stable operation machinery. Feature
modules never call `fetch` for mutations and never apply local state changes
that pretend server acceptance.

**Promise semantics (frozen, per Codex interim evaluation):** `sendAction`
and `resolveDecision` ALWAYS return a Promise, so components can drive
busy → settled UI:

- success → resolves with the fresh normalized state;
- definitive failure (typed 4xx incl. `pending_decision`, `stale_revision`)
  → rejects with the typed error (the shell also toasts);
- uncertain failure (transport/5xx) → resolves `null` after the shell shows
  the safe-retry pill — the pill owns the outcome, the dispatch itself did
  not definitively fail;
- another op already in flight → rejects with `{code: "busy"}`.

Components that fire-and-forget MUST `.catch(() => {})` so rejections never
reach the global error boundary.

### Action specs (frozen — existing adapter shape, no new fields)

`normalizeAction` output: `{ id, label, category, targetId, targetLabel,
inputs, knownCosts, limits, enabled, reason }`. UI rules:

- Disabled means `disabled` + `aria-disabled="true"` + the server `reason`
  visible (title + text). Opacity is never the disabled mechanism.
- Costs/limits come from `knownCosts`/`limits` only. No invented prices or
  annual caps. A `$0`/free cost renders as "Free".
- Only specs the server advertises render. Unknown actions never become
  clickable because there is room in a grid.
- Category mapping for v2 destinations is shell-owned: `relationships` +
  `family` → people; `education` + `work` → job; `finances` → assets;
  `health` + `activities` → activities; `life`/`other`/unmapped → General
  (activities screen catch-all).

### Person detail HTTP (B exposes; A fills in Stage 3)

`GET /api/games/{game_id}/people/{person_id}` → §Person detail JSON (live:
backed by A's `GameService.get_person_view`; B owns this exposure).
Errors: typed 404 `person_not_found` for unknown IDs (per A's frozen §Person
v1, deceased/archived/non-relatives remain viewable). The overlay renders an
honest error state on failure — never fabricated content.
The shell runs `detail.actions` through the frozen adapter `normalizeAction`
before mounting/updating a module: A's projection passes advertised
snake_case specs through, and the adapter is the single client-side dialect
(so `known_costs` reaches modules as `knownCosts`, etc.).

## §Tokens — visual tokens (frozen)

Single source: `web/static/v2/theme.css`, loaded **after** all module
stylesheets. Theme may restate any token at equal-or-greater specificity
(later order wins); modules must not use `!important` on tokens. Modules may
carry fallback defaults on their own root (C's person.css does) so they also
work in a bare test host without the theme.

### Core palette (names frozen — C's person.css already consumes these)

| Token | Default | Use |
|---|---|---|
| `--v2-accent` | `#2a63e6` | primary actions, links, header gradient end |
| `--v2-accent-2` | `#3a7bfd` | header gradient start |
| `--v2-ink` | `#16203a` | primary text |
| `--v2-ink-soft` | `#2b3550` | secondary text |
| `--v2-muted` | `#6b7690` | captions, subtitles |
| `--v2-faint` | `#69718a` | sub-labels, disabled text (AA-compliant) |
| `--v2-page` | `#eef2f9` | page background |
| `--v2-card` | `#ffffff` | cards/sheets |
| `--v2-track` | `#e7ebf3` | bar tracks |
| `--v2-bar-good/mid/bad` | `#15803d` / `#b45309` / `#dc2626` | semantic stat colours (AA-compliant, see rule 2) |
| `--v2-bar-1..4` | `#2a63e6` `#15803d` `#a16207` `#be185d` | ordinal bar colours (unknown values) |
| `--v2-radius` | `16px` | card corners (20px hero cards) |
| `--v2-shadow` | `0 4px 16px rgba(22,32,58,.10)` | card shadow |
| `--v2-gap` | `10px` | standard gap |

Shell additions (frozen): `--v2-font` (font stack, see below), `--v2-header-h`,
`--v2-tabbar-h`, `--v2-age-good` (`#1fb35a`) / `--v2-age-good-dark` (`#10783a`)
/ `--v2-age-grad` for the Age button, `--v2-danger` (`#b91c1c`) for fatal
choices, `--v2-focus` (`#2f6fed`).

### Rules (binding)

1. **Type:** `--v2-font: "Nunito", "Segoe UI", system-ui, sans-serif`. Nunito
   is progressive enhancement — loaded locally if available, never a blocking
   external prerequisite for play.
2. **Contrast (audited, WCAG 2.1 AA, computed ratios on `--v2-card`
   `#ffffff`):** stat colour is never the only signal — numeric labels always
   accompany bars. `--v2-bar-good` `#15803d` = 5.0:1, `--v2-bar-mid`
   `#b45309` = 5.1:1, `--v2-bar-bad` `#dc2626` = 4.8:1 (the mock's
   `#16a34a`/`#d97706` were 3.3:1/3.2:1 and FAIL small bold text — replaced).
   White on the accent header gradient (worst stop `#2a63e6`) = 5.2:1.
   `--v2-ink`/`--v2-ink-soft` ≥ 10:1, `--v2-muted` `#6b7690` = 4.6:1,
   `--v2-faint` `#69718a` = 4.9:1 (the mock's `#8a93a8` was 3.1:1 — replaced).
   Anything new must be computed, not assumed.
3. **Focus:** `:focus-visible` outline `3px solid var(--v2-focus)` +
   `outline-offset 2px` everywhere in v2 scope.
4. **Layout:** dynamic viewport (`100dvh` with `100vh` fallback), safe-area
   insets via `env()`, no fixed `390×844` or `overflow:hidden` page roots.
   All v2 screens scroll at 320px width, landscape and text-zoom up to 200%;
   the last content is never covered by the tab bar (`scroll-padding-bottom`
   ≥ tab bar height + safe area).
5. **States:** explicit loading (skeleton or labelled spinner), error
   (message + retry where a retry exists — never a blank screen), busy
   (disabled controls + label). Disabled reasons come from the server.
6. Components share these variables; no copied inline colours. Feature-module
   class prefixes stay scoped (`v2p-*` person, `v2-*` shell).

## §Person — additive person detail projection (A, FROZEN v1, 2026-11-18)

Source of truth: `src/game/features/person_view/projection.py`
(`person_detail`). Service entry: `GameService.get_person_view(game_id,
person_id)` (implemented; tests `tests/test_game_v2_person.py`). B's route
`GET /api/games/{game_id}/people/{person_id}` serves this dict verbatim.

Design rules (codex-review.md §3 Person):

- **Observer-safe**: fields come only from data the generic state already
  exposes (`people[]`, advertised action specs) or an approved observer-known
  view. The editor's privileged policy catalog is NEVER a source.
- v1 has **no approved public view** for NPC job, city, faith, generosity, or
  NPC health/happiness: they appear nowhere in the payload (not even as
  nulls). `descriptors` is `{}` today; `job`/`faith`/`city`/`country` are
  reserved keys, added only with an approved observer-known projection and a
  fixture here. The client renders `value: null` bars as "unknown" and omits
  absent descriptors — never fabricated.
- Actions are **filtered, never re-derived**: entries are the exact specs
  from `_advertised_actions` (the same list `_state` serves as
  `available_actions`, pending/death gating included) with
  `target_id == person_id`, in advertised snake_case shape — the frozen
  adapter `normalizeAction` (§Shell) is the client-side normalizer. No new
  dialect, so advertised eligibility equals dispatch by construction
  (QUALITY-GUIDE #3).
- Addressability: anyone in `_all_people` (living walk + archive) is
  viewable — including deceased and non-relatives. **Only truly unknown IDs
  error.** NOTE to B: this supersedes the interim "incl. non-relatives" note
  in §Shell — non-relatives return a normal detail (fixture 1 below).
- Typed error: unknown ID → `GameError(code="person_not_found",
  status=404)` (B's route maps it to the frozen `404 person_not_found`
  body).

### Shape

```jsonc
{
  "id": "b7f2…",                    // person_id (same id as people[])
  "name": "Ada Okafor",
  "age": 54,
  "role": "mother",                 // player|mother|father|sibling|partner|
                                    //   child|relative (same _role() as state)
  "alive": true,
  "appearance": { "schema_version": 1, "seed": 3, ... } | null,

  "descriptors": {},                // observer-public descriptors only; v1 {}

  "bars": [                         // public observer-safe stats
    { "key": "relationship", "label": "Relationship", "value": 82 }
  ],                                // [] when not a Relation; value int 0-100
                                    //   or null ("unknown") for future stats

  "actions": [                      // exact advertised specs, target-filtered;
                                    //   [] when deceased / pending open /
                                    //   player dead
    { "id": "spend_time", "label": "Spend time",
      "category": "relationships",
      "target_id": "b7f2…", "target_label": "Ada Okafor",
      "inputs": [],
      "known_costs": [],            // money costs {kind:"money",amount:N};
                                    //   $0 stays 0 (free is real)
      "limits": ["once per year per person"],
      "enabled": true,              // false = ineligible/used this year
      "reason": null }              // precise server-authored refusal reason
  ]
}
```

- Player's own detail (`role: "player"`): `bars: []`, `actions: []` — the
  Person card is for NPCs; the player has Home.
- Deceased: `alive: false`, `actions: []`, `bars` retained as at death. No
  server-side † in `name` — the marker is a client rendering choice.

### Fixtures (pinned by tests/test_game_v2_person.py)

1. **Unrelated adult** — living adult not on the player's relations list:
   viewable, `role: "relative"`, `bars: []`, `actions: []` (the shell
   advertises no target specs for non-relations).
2. **Child** — own child, age < 18: `role: "child"`, relationship bar,
   `actions` limited to child-eligible advertised specs (age gates in
   `_relationship_specs` keep adult offers out of the advertised list, so
   they can never appear here).
3. **Deceased** — archived dead relative: `alive: false`, `actions: []`,
   relationship bar retained, plain `name`.
4. **Unknown ID** — `get_person_view(game_id, "nope")` → `person_not_found`,
   status 404.

## §Home (A, FROZEN v1, 2026-11-18) — no new server fields

Home renders entirely from the existing frozen `_state` projection; A adds
**nothing** for it and consumers must not invent parallel fields:

- Header portrait/name: `people[]` player entry (`appearance` via the
  existing avatar renderer), `money` for cash ("Money" label — no separate
  bank system exists).
- Subtitle: `work.job_label` + `country`, with the client choosing the
  honest fallback (student → `education` state, retired → retirement state,
  unemployed → status string). Named careers/cities are future feature
  work, not UI fixtures.
- Stats: the four `stats` values; semantic colours are §Tokens' concern.
- Timeline: `journal` (age/text/kind/person_ids/changes, already
  player-known filtered server-side). "This year / Last year" grouping is
  client-side age math against the player's current age; the journal's
  chronological order is never mutated. No authoritative calendar-year
  field exists and none is added.
- Age button: the advertised `age_up` spec + shell `sendAction` (busy,
  retry-ledger, dead/continuation semantics all stay on the shell).

## §Choice (A display metadata, FROZEN v1, 2026-11-18; sheet behaviour stays B's)

Canonical rendering stays `normalizeDecision`
(title/text/personIds/inputs/options with enabled/reason/knownEffects).
The header chip ("AGE 27 · FAMILY") uses ONE additive, observer-safe server
field on `pending_decision`:

- `display_category`: `null` | one of `"family"`, `"dating"`, `"weddings"`,
  `"education"`, `"work"`. It is a pure function of the already-public
  `kind` — no new semantics leave the engine:

| pending `kind` | `display_category` |
|---|---|
| `baby_naming` | `"family"` |
| `date_offer`, `dating_offer` | `"dating"` |
| `wedding_plan` | `"weddings"` |
| `university_application`, `university_funding` | `"education"` |
| `retirement_offer` | `"work"` |
| any other kind, or kind absent (M1 generic) | `null` → UI shows the age only, no chip |

Rules: unknown kinds are a silent `null`, never an error and never a
guess; no other display metadata is added without A/C sign-off (codex
§3 Choice); no Agreement-% or inferred hint fields. The mapping table
lives server-side (`GameService._display_category`) so every client
shares one source.

```jsonc
{
  "id": "d_4",
  "kind": "vaccination_age2",
  "event_id": "vaccination_age2",
  "display_category": null,
  "title": "…", "text": "…",
  "person_ids": ["…"],
  "options": [
    { "id": "d_4_yes", "label": "Yes",
      "known_effects": [],              // stat spoilers (F5: hidden pre-choice)
      "known_costs": [                  // §Choice costs v2 (2026-11-20): REAL,
                                        //   already-public money costs only
        {"kind": "money", "amount": 120}
      ],                                // [] when the option is free — free is
                                        //   real; never null, never omitted
      "enabled": true, "reason": null,
      "inputs": [] }
  ]
}
```

- **`known_costs` (FROZEN v2, A, 2026-11-20 — Codex feel-eval #2):** every
  projected pending option carries `known_costs` — a list (never null,
  never omitted; `[]` when free — free is real). Two dialects, both real
  money charges: rule-owned options keep their **authored**
  `known_costs` (`{"label", "amount"}`), e.g. wedding venues and lottery
  tickets; event-file/M1 options get it **derived** from the option's
  public money charge — the same figure the affordability gate enforces —
  as `{"kind": "money", "amount": N}`. Stat effects stay in
  `known_effects` and remain hidden pre-choice (F5); no invented prices,
  no time/eligibility pseudo-costs (those stay in `reason`). Consumers
  must tolerate both dialects and an empty list (never index `[0]`
  blindly). The adapter passes the field through normalization verbatim
  (B/C plumbing); clients format structured money costs, never
  `String(object)`.

Sheet behaviour (frozen by B): capped scrollable body, controls above the
software keyboard, backdrop/Escape collapse to the "Decide later" pill —
never discarding a server decision.

## §Journal v2 (A, FROZEN v1, 2026-11-20 — feel-pass F4 + F2 server side)

Two additive, server-owned pieces for the BitLife-style scrolling feed.

### `journal_feed` (new field on `_state`, additive)

The flat `journal` field is UNCHANGED (frozen §Home rendering keeps working).
`journal_feed` is a **derived** regrouping of the same player-known-filtered
entries — no new persistence, same chronological order, never mutated:

```jsonc
"journal_feed": [
  {
    "age": 2,                       // in-game age for the whole block
    "header": "Age 2",              // server-authored header text (age only,
                                    //   no year/turn/calendar — none exists)
    "entries": [                    // every journal line of that age, in order
      {"id": "j_12", "kind": "year"|"event"|"education"|"relationship"|
             "decision"|"death"|...,
       "text": "…", "person_ids": ["…"], "changes": {"happiness": -3}}
    ]
  }
]
```

Guarantees (pinned by tests):

- Every year the player lives through produces at least one block with the
  aging line (`kind: "year"`, e.g. "Maya turned 6.") — including years
  interrupted by a pending decision.
- Blocks contain lines from all engine sources: aging, events, education
  (school transitions/graduations), relationships (spend-time, and yearly
  parent-relationship drift when it actually moved), decisions, deaths.
- Blocks are keyed by `age` and ordered oldest → newest (feed scrolls up).
- Client renders `header` per block and auto-scrolls to the newest block;
  grouping never requires client-side age math over the flat journal.

### F2 server side (player-facing text purity)

Server-authored player-facing strings — journal/feed `text`, pending
`title`/`text`, option `label`s and `reason`s — NEVER contain internal ids
(`t_000003`), phase names (`year_close`), or cursor state. Machine fields
(`turn`, `finances.last_closed_turn`) remain for dev/ledger use but are not
player text and B's dev-only panel is their only renderer. Pinned by a test
scanning the projected player-facing strings against
`/\bt_\d{6}\b/|year_start|year_close|phase_id|item_cursor`.

## §Decision outcome (A, FROZEN v1, 2026-11-20 — feel-pass F5, consumed by C)

One additive field on `_state`, set by every successful `resolve_decision`
(both the M1 template path and C's rule path), carrying ONLY what actually
happened (capped values included):

```jsonc
"last_decision_outcome": null | {
  "decision_id": "d_4",
  "option_id": "take_it",
  "text": "I took the offer.",            // first-person outcome line, same
                                          //   text journaled for the choice
  "person_ids": ["…player…", "…target…"],
  "changes": {"happiness": -5, "money": -120},   // actual applied deltas
  "relationship_changes": {"<person_id>": 4}     // optional, only when a
                                                  //   relation moved
}
```

Rules: `null` when nothing has been resolved (new game / legacy saves use
`env.get`, never a KeyError) and also when the resolved outcome event is not
player-visible — the captured event must pass the SAME server-side
`player_known` gate as journal serialization (`audience`/`fact_id`), so this
raw-projected field can never bypass privacy. Defense in depth: the capture
persists the event's visibility metadata on the stored outcome, and the
`_state` projection re-applies the same gate at serialization
(`GameService._visible_outcome`), stripping the internal metadata keys —
stored private metadata is re-filtered even at read time. Each resolve REPLACES the
previous value; other commands (age_up, rule actions) never invent or clear
it — C shows the popup from the resolve response itself, so no client-side
dedupe tracking is needed. Relationship deltas are mapped per target person
in `relationship_changes` `{person_id: delta}` (both M1 and rule paths); the
summed scalar `changes.relationship` is journal legacy and ambiguous with
multiple targets — consumers should prefer `relationship_changes`. The
event-file auto outcomes (no player choice) are NOT included — this is
decision-resolve only. Costs/eligibility `known_effects` on pending
options stay as-is (F5 hides them pre-choice client-side); this field is the
post-choice source of truth for the popup.

## §Avatar defaults (A, FROZEN v1, 2026-11-20 — feel-pass F9 / Codex feel-eval #3)

Server-side generation rules for **default** (non-user-edited) appearances,
pinned by tests/test_game_v2_avatar_defaults.py:

- **Gender-fitting accessories:** `facial_hair` is `"beard"` only for
  man-based adults (25% roll), otherwise `"none"` — women never get a
  beard. `headwear` is `"headscarf"` only for woman-based adults (15%
  roll), otherwise `"none"`. The baby/child guardrail (no badges, no
  facial hair, no headwear) is unchanged (`aging.aged_view` + generation).
- **Household variety:** each person rolls independently from the
  dedicated cosmetic `_APP_RNG` stream; two adults in one household never
  collapse to the same base+accessory look by construction (man/woman
  bases differ; accessory rates are low and gendered).
- **User edits preserved:** `ensure_appearance`/`migrate_appearances`
  NEVER reroll a valid stored appearance (any schema_version the model
  accepts); generation runs only for missing/unparseable data, and
  biological children still inherit via `inherit_appearance`
  (facial hair starts "none" there — unchanged).
- Cosmetic generation consumes only the dedicated cosmetic RNG stream —
  it never perturbs gameplay RNG.

## §Undo (A, FROZEN v1, 2026-11-20 — BACKLOG 'Undo last year', engine side)

One level of undo for the last aged year. Snapshot + restore of the FULL
envelope; no re-roll lever.

### Projection (additive)

```jsonc
"can_undo": { "enabled": true, "reason": null }
// enabled=false, reason="there is no year to undo"      (fresh game /
//   snapshot consumed / cleared by continue_as)
// enabled=false, reason="only one year can be undone"   (undo just used)
```

`undo_year` is ALSO advertised in `available_actions` (category `life`,
label "Undo last year") with the same enabled/reason — one source
(`GameService._can_undo`).

### Command

`command(game_id, expected_revision, request_id, "undo_year", {})` — in the
whitelist alongside age_up/resolve_decision; allowed while a pending
decision is open (the pending belongs to the undone year and vanishes with
it) and while dead (restores the pre-year, living state).

Semantics (pinned by tests):

- **Snapshot:** at the start of every `age_up` execution the server stores
  a full serialized copy of the pre-age envelope (ledger, people graph,
  journal/feed, pendings, knowledge/facts, flags, replay ledger as of then,
  `rng_state` + `cosmetic_rng_state`). One snapshot — each successful
  `age_up` REPLACES it, so undo is always exactly "the last year".
- **Restore:** `undo_year` validates and restores the entire snapshot:
  stats/money via the ledger, relations/partner/children/dead/archive
  graph, journal (the undone year's entries disappear), pendings,
  knowledge records, and BOTH RNG streams. The snapshot is deserialized
  through the **restricted save loader** (`safe_save.load_save` over a
  `BytesIO` of the snapshot bytes — `SaveUnpickler` blocks unapproved
  globals), never bare `pickle.loads`; the restored envelope is validated
  before it is applied.
- **Monotonic revision (Codex undo review, 2026-11-20):** undo restores
  gameplay state, NOT the concurrency token. The response revision is
  always `pre-undo revision + 1` (fresh 1 → Age 2 → Undo **3**, never
  back to 2). A client holding a pre-undo revision goes
  `stale_revision` — stale tabs can never mutate the restored timeline.
  This holds even when the undone year spanned multiple commands (age +
  several resolves): the revision never rewinds.
- **Ambient RNG:** undo never touches process-global streams or the
  cosmetic RNG binding — the restored `rng_state` /
  `cosmetic_rng_state` are the single source of truth; the next
  `_GameRNG` entry rehydrates the globals from them. `random.getstate()`,
  `_COSMETIC_RNG.getstate()` and `integration._APP_RNG` identity are
  identical before and after an undo.
- **Anti-bypass / 18+ parity:** restoring the RNG streams means the next
  `age_up` consumes the identical stream — the redone year replays
  EXACTLY (same events, same eligibility/18+ gating outcomes). Undo is
  never a re-roll. One level only: the snapshot is consumed by undo;
  `continue_as` clears it (no cross-generation undo).
- **Retry vs redo (frozen distinction):** the undone year's request_ids
  leave the replay ledger with the rollback. A delayed **retry** of the
  original operation — same request_id AND its original
  `expected_revision` — is rejected with `stale_revision` (never a
  byte-identical success, never a mutation of the restored timeline). An
  **explicit redo** — same request_id resubmitted with the CURRENT
  revision — is a new operation and re-executes the identical year
  (deterministic via the restored streams). `undo_year`'s own retry
  stays idempotent: same request_id + payload returns the stored
  response unchanged (replay ledger is authoritative, checked before
  the revision check).
- Definitive errors are typed as everywhere else (`stale_revision`,
  `busy` via the shell).

## §Editor navigation (A contract, FROZEN v1, 2026-11-18 — B implements in editor.js/game.js)

Exact-person Edit by ID (Stage 3):

- `shellHooks.onEdit(personId)` (§Shell) opens the in-play character
  editor idempotently (`editor.openEditor`, never toggle-closed) with
  **that exact `person_id` selected**: the roster row shows
  `aria-pressed="true"`, the panel scrolls to/loads that person, and the
  law pseudo-entry is not selected.
- Signature amendment (B's file): `openEditor(state, hooks, personId?)` —
  the third parameter is optional and additive; omitting it keeps today's
  behaviour (no selection).
- IDs are the same `person_id` space as `people[]` and §Person; the
  editor roster already keys off it, so no server change is needed.
- Unknown/deceased `personId`: open the editor with NO selection and an
  honest status line — never fabricate a selection, never block the
  editor. Deceased people are not editable (`renderPeople` already
  filters `alive !== false`), so a dead id falls back to no-selection.
- Editor semantics (preview/apply, pending supersession, stale-revision
  refresh) are unchanged.

## §Event — event modal card + outcome popup (integrated v2, 2026-11-20)

Scope: feel-pass F5 + F6. `web/static/v2/event.js` + `v2/event.css` (C) is a
drop-in replacement for the §Choice sheet renderer: B swaps the
`mountChoiceSheet` import in game.js for `mountEventModal`; the sheet's frozen
behaviour (no invented options/reasons, collapse never discards, one in-flight
resolve, initial focus, naming/per-option inputs) is retained unchanged.

### Mount API (frozen for B)

```js
const sheet = mountEventModal(box, {
  decision,          // normalized decision (§Choice shape, unchanged)
  people,            // public resolved person objects for decision.personIds
  playerId,          // excludes the player from main portrait and person chips
  playerAge,
  outcome,           // optional: show a safely retried resolution directly
  onResolve(optId, inputs),  // MUST return a Promise resolving to the
                             //   outcome object (§Outcome below) or null
  onCollapse(),              // "Decide later" — NEVER discards the decision
  onError(message),
  onPersonClick?(id),
  onClose(),                 // after the outcome popup's Continue (or when
                             //   the outcome is absent)
  renderAvatar?(el, appearance, age, size),  // default: common.js renderAvatar
});
// → { firstEnabled, unmount() }   — same return shape as mountChoiceSheet
```

- The adapter exposes `lastDecisionOutcome: raw.last_decision_outcome || null`.
  game.js returns that outcome only when its decision_id and option_id match
  the resolve being displayed. The shell preserves an outcome until Continue,
  then opens any next authoritative pending decision. Safe retries can mount
  the matching outcome directly; ordinary rerenders do not replay it. Frozen §Shell
  promise semantics otherwise unchanged (rejections still typed; uncertain
  failures still settle null with the pill owning the outcome).
- The module renders into `box` (the existing `.modal-box`); it preserves any
  child with `id === "modalCancel"` exactly like the old sheet.
- **F5 pre-choice rule:** option buttons render `label` + real costs/reasons
  only: `knownCosts` (money costs; `$0` renders "Free") on any option that
  carries them, and the server `reason` on disabled options (title + visible
  text). `knownEffects` (stat-effect ranges like "-happiness (25-35)") are
  NEVER rendered before choosing. Nothing else is synthesized.
- **F6:** the module renders a centered modal card (`v2e-card`), not a bottom
  sheet: header = the OTHER person's avatar (first non-player in `people`,
  via `renderAvatar`; omitted when no other participant is available) + title + the frozen §Choice age/category chip. B centers
  `.modal-box` in `#modal` via theme.css (flex overlay, dimmed backdrop);
  event.css carries a `#modal:has(.v2e-card)` fallback so a bare test host
  also centers.

### §Outcome (frozen for A — the resolve result C's popup shows)

The authoritative shape is **§Decision outcome (FROZEN v1)** above.
`last_decision_outcome` is retained on subsequent states; clients display it
only for the matching successful resolution or safe retry:

```jsonc
"last_decision_outcome": {
  "decision_id": "d_1", "option_id": "truth",
  "text": "You told your mother the truth. She went quiet.",  // first-person
                                                              // outcome line
  "changes": { "happiness": -12, "money": -200, "relationship": -8 },
  "person_ids": ["p_mom"],         // public people the event involved
  "relationship_changes": {"p_mom": -8} // optional actual per-person deltas
}
```

- `changes` keys ⊆ `money|happiness|health|smarts|looks|relationship`, int
  deltas actually applied (post-cap), nonzero only, `{}` when nothing moved.
  These are the same values `_cmd_resolve_decision` already computes for the
  journal `changes`; the outcome just surfaces them (plus the formatted
  outcome text) on the response instead of only in the journal.
- `text` is the formatted `option["_outcome"]` line (player-known filtered).
- The popup is skipped (module calls `onClose()`) when the field is absent
  (older server, replay-ledger replay, or uncertain failure — the pill owns
  that outcome). The module never fabricates deltas or text.
- Client rendering: each change = label + signed value, green/red per sign
  (`--v2-bar-good`/`--v2-bar-bad`, AA-compliant), numeric labels always
  shown; `money` uses `$` formatting. relationship = "Relationship".

### Codex integration invariants (F5/F10/F11)

- Normalized option `knownCosts` preserves server `known_costs`; structured
  `{kind:"money", amount:N}` renders a money amount, never an object string.
  Stat-effect forecasts stay hidden.
- `relationship_changes` renders signed rows per public participant name;
  unknown IDs never appear as player-facing labels. It supersedes a generic
  aggregate relationship row when both are present.
- Main portrait and participant chips exclude `playerId`; other participants
  retain their existing chip navigation. Modal/outcome controls own focus while the background is inert.
- A resolution and its outcome block Age; Continue opens the next pending
  choice without advancing time. Escape can collapse a pending decision but
  cannot dismiss an in-flight resolution or its outcome.

## §Journal flavour — yearly lines (C content + selector, FROZEN v1, 2026-11-20; A hook WIRED)

C owns `src/game/content/journal/yearly_lines.py`: an original pool of 176
first-person lines across six age bands (baby 0-1, toddler 2-4, child 5-12,
teen 13-17, adult 18-64, elder 65+) plus the selection algorithm.
Condition dialect (observer-safe, player-known only): stat min/max,
flags (employed/partner/children/mother/father/ill/dropped_out/retired),
school stage, role_relation min/max.

### A hook (one call in `_cmd_age_up`, replacing the dull default)

```python
from src.game.content.journal.yearly_lines import year_journal_line
text = year_journal_line(env, {
    "education": self._education_view(player),
    "work": self._work_view(player),
})
self._journal(env, "year", text or f"{player.firstname} turned {player.age}.",
              [player.person_id], changes)
```

Guarantees the selector provides (pinned by tests/test_journal_yearly_lines.py):
- Draws ONLY from the cosmetic stream (`integration._APP_RNG`, mounted per
  command) — never consumes simulation randomness, so gameplay streams are
  unchanged and picks are deterministic per save.
- No repeats: `env["journal_line_recents"]` (additive env key, pickled with
  the save) excludes the last 60 picks; a line id never repeats within the
  window while its band has any other eligible line.
- Age fit: only the line's own band is eligible for the player's age.
- `None` when nothing fits → A keeps the existing default text (behaviour
  unchanged for legacy saves; recents initialize empty).
