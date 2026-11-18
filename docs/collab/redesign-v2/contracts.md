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

Secondary routes live in the v2 header "More" menu (frozen): All lives,
Character editor, Family tree, Dating & weddings. The global topbar keeps
Creator / Gallery links. Decision pill, retry pill, offline banner and the
modal render above the v2 chrome (z-order contract: chrome never covers them).
Secondary-route entries are IDEMPOTENT and reveal their owning surface: More →
Character editor selects the Activities screen (the General card owns the
editor toggle) and opens the editor only if closed — never toggles it shut.

game.html groups the existing panels into `<section id="screen-{home,job,
assets,people,activities}" class="screen">`; the legacy `tabbar` is replaced
by the v2 tab bar (the old screen *content* is fully preserved inside the new
sections — no panel is removed).

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

Sheet behaviour (frozen by B): capped scrollable body, controls above the
software keyboard, backdrop/Escape collapse to the "Decide later" pill —
never discarding a server decision.

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
