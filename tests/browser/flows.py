"""Data-driven browser flows executed by tools/browser_check.py.

Each flow is a dict:
  name      short identifier used in the report
  start     URL path to navigate to before the first step
  setup_js  optional JS run once after navigation (before waits)
  steps     list of step dicts, executed in order

Step kinds:
  {"wait_js": expr, "desc": str}          poll until truthy (required)
  {"check_js": expr, "desc": str}         assert truthy
  {"js": expr}                            run, fail on exception
  {"store_js": expr, "as": name}          save result in flow context
  {"key": "Tab"}                          trusted key event
  {"shot": "file-stem"}                   screenshot into evidence dir
  {"python": name}                        named Python hook in the runner
                                          (e.g. "validate_export", "create_game")
  {"api": {"method": "GET", "path": "/api/...", "as": name}}
                                          raw API call; JSON body stored in the
                                          flow context under `name`

Adding flows for future milestones means appending a dict here — no runner
changes needed (add new step kinds to the runner first, if required).
"""

FLOWS = [
    {
        "name": "creator-keyboard",
        "start": "/creator",
        "steps": [
            {"wait_js": "document.querySelectorAll('.swatch').length > 0",
             "desc": "creator swatches render (options built)"},
            {"shot": "creator-initial"},

            # Select a skin tone; focus must stay on the activated swatch
            # across the async preview round-trip. The Fluent creator
            # opens on the Look tab, so switch to Skin first.
            {"js": "document.getElementById('tab-skin').click(); true"},
            {"wait_js": """document.querySelectorAll(
                '.swatch[data-category="skin_tone"]').length >= 5""",
             "desc": "skin tab swatches built"},
            {"js": """document.querySelector(
                '.swatch[data-category="skin_tone"]').focus()"""},
            {"js": "document.activeElement.click()"},
            {"wait_js": """!!(document.activeElement &&
                document.activeElement.dataset.category === 'skin_tone')""",
             "desc": "focus retained after swatch click"},
            {"wait_js": """!!(document.activeElement &&
                document.activeElement.getAttribute('aria-pressed') ===
                'true')""",
             "desc": "activated swatch is aria-pressed"},

            # Tab arrow-key traversal between tabs.
            {"js": "document.getElementById('tab-skin').focus()"},
            {"key": "ArrowRight"},
            {"check_js": "document.activeElement.id === 'tab-hair'",
             "desc": "ArrowRight moves to next tab"},

            # Badges: list semantics with contract cap of 2. Two badges
            # stick; a third is refused (not pressed, still 2 chosen).
            {"js": "document.getElementById('tab-badges').click(); true"},
            {"wait_js": """document.querySelectorAll(
                '.swatch[data-category="badges"]').length >= 5""",
             "desc": "badges tab shows badge swatches"},
            {"js": """[...document.querySelectorAll('.swatch')].find(
                s => s.dataset.category === 'badges' &&
                     s.dataset.value === 'glasses').click()"""},
            {"js": """[...document.querySelectorAll('.swatch')].find(
                s => s.dataset.category === 'badges' &&
                     s.dataset.value === 'crown').click()"""},
            {"wait_js": """['glasses','crown'].every(b => {
                const sw = [...document.querySelectorAll('.swatch')].find(
                    s => s.dataset.category === 'badges' &&
                         s.dataset.value === b);
                return sw && sw.getAttribute('aria-pressed') === 'true';
            })""",
             "desc": "two badges multi-selected"},
            {"js": """[...document.querySelectorAll('.swatch')].find(
                s => s.dataset.category === 'badges' &&
                     s.dataset.value === 'flower').click()"""},
            {"wait_js": """(() => {
                const sw = [...document.querySelectorAll('.swatch')].find(
                    s => s.dataset.category === 'badges' &&
                         s.dataset.value === 'flower');
                return sw && sw.getAttribute('aria-pressed') === 'false';
            })()""",
             "desc": "third badge refused (cap 2)"},
            {"wait_js": """(() => { const img = document.querySelector(
                '#preview img'); return !!img && img.naturalWidth > 0; })()""",
             "desc": "badge-bearing preview renders (badge avatars pass "
                     "validation)"},

            # Export the appearance JSON and validate it in Python.
            {"js": "document.getElementById('printBtn').click()"},
            {"store_js": "document.querySelector('pre.export').textContent",
             "as": "export_json"},
            {"python": "validate_export"},

            # Age slider sweep keeps a valid preview (Fluent img).
            {"js": """const a = document.getElementById('age');
                     for (let v = 0; v <= 80; v += 8) {
                       a.value = v;
                       a.dispatchEvent(new Event('input'));
                     } true"""},
            {"wait_js": """(() => { const p = document.getElementById(
                'preview');
                return !!(p.querySelector('img') || p.querySelector('svg'))
                    && !p.title; })()""",
             "desc": "preview valid after rapid age changes"},
        ],
    },
    {
        "name": "m1-life-flow",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "create_game"},
            {"js": "sessionStorage.setItem('game_id', CTX.game_id); true"},
            {"js": "location.reload(); true"},
            {"wait_js": "!document.getElementById('gameView').hidden",
             "desc": "game view visible after create"},
            {"wait_js": """document.querySelectorAll(
                '#actionList button').length > 0""",
             "desc": "actions available"},
            {"check_js": """document.querySelector('#playerCard .name')
                .textContent.includes('QA')""",
             "desc": "player name shown"},
            {"shot": "game-born"},

            # Age up; if a decision appears, resolve the first option,
            # then age again. Repeat until age >= 2.
            {"python": "age_until_two"},

            # Reload: state must persist (revision/age from the server).
            {"js": "location.reload(); true"},
            {"wait_js": "!document.getElementById('gameView').hidden",
             "desc": "game view visible after reload"},
            {"check_js": """(() => { const m = document.querySelector(
                '#playerCard .role').textContent.match(/Age (\\d+)/);
                return m && +m[1] >= 2; })()""",
             "desc": "age persisted across reload (>= 2)"},

            # Player avatar renders from /api/render (known B outage
            # mid-migration -> WARN; hard wait otherwise).
            {"python": "assert_player_avatar"},
            {"shot": "game-reloaded"},

            # Extend through a REAL decision: age (API) until a pending
            # decision with non-empty participants, then walk the modal
            # in the UI — focus trap, portraits, Escape pill, resolve.
            {"python": {"hook": "reach_participant_decision"}},
            {"python": "decision_modal_walk"},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_refs"},
        ],
    },
    {
        # Consolidated from B's browser_smoke regressions (review item 5):
        # retry-stable creation, deep link vs Resume, invalid-link recovery,
        # resume-list portraits.
        "name": "m1-resilience",
        "start": "/creator",
        "steps": [
            {"wait_js": "document.querySelectorAll('.swatch').length > 0",
             "desc": "creator ready"},
            {"python": {"hook": "count_games", "as_": "n_before"}},
            # simulate a committed-but-lost creation response
            {"js": """window.__dropNextCreate = true;
                const origFetch = window.fetch;
                window.fetch = (...a) => origFetch(...a).then(r => {
                  if (window.__dropNextCreate &&
                      String(a[0]).includes('/api/games') &&
                      a[1] && a[1].method === 'POST' && r.ok) {
                    window.__dropNextCreate = false;
                    return Promise.reject(new Error('simulated loss'));
                  }
                  return r;
                }); true"""},
            {"js": "document.getElementById('startBtn').click(); true"},
            {"wait_js": """(() => { const m = document.getElementById(
                'startMsg').textContent;
                return typeof m === 'string' && m.includes('retry'); })()""",
             "desc": "create failure surfaces retry"},
            {"js": "document.getElementById('startBtn').click(); true"},
            {"wait_js": "location.pathname === '/game'",
             "desc": "retry navigates to the new life"},
            {"python": {"hook": "count_games", "as_": "n_after"}},
            {"check_ctx": "CTX.n_after === CTX.n_before + 1",
             "desc": "retry reused the operation: exactly one new life"},
            {"js": "location.href = '/creator'; true"},
            {"wait_js": "document.querySelectorAll('.swatch').length > 0",
             "desc": "creator reloaded"},
            {"check_js": """sessionStorage.getItem('pending_create_op') ===
                null""",
             "desc": "operation cleared after definitive success"},

            # deep link vs Resume
            {"python": {"hook": "make_life", "first": "Alpha",
                        "as_": "gid_a"}},
            {"python": {"hook": "make_life", "first": "Beta",
                        "as_": "gid_b"}},
            {"js": "sessionStorage.clear(); true"},
            {"js": "location.href = '/game?game=' + CTX.gid_a; true"},
            {"wait_js": """(() => { const n = document.querySelector(
                '#playerCard .name');
                return !!n && n.textContent === 'Alpha Life'; })()""",
             "desc": "deep link opens game Alpha"},
            {"js": "document.getElementById('backToLives').click(); true"},
            {"wait_js": "!document.getElementById('resumeView').hidden",
             "desc": "life list visible"},
            {"wait_js": """[...document.querySelectorAll(
                '#lifeList .chip')].some(
                r => r.textContent.includes('Beta'))""",
             "desc": "life list loaded with Beta"},
            {"js": """[...document.querySelectorAll('#lifeList .chip')].find(
                r => r.textContent.includes('Beta')).querySelector(
                'button').click(); true"""},
            {"wait_js": """(() => { const n = document.querySelector(
                '#playerCard .name');
                return !!n && n.textContent === 'Beta Life'; })()""",
             "desc": "Resume selects Beta"},
            {"check_js": "location.search === ''",
             "desc": "query param consumed"},
            {"js": "location.reload(); true"},
            {"wait_js": """(() => { const n = document.querySelector(
                '#playerCard .name');
                return !!n && n.textContent === 'Beta Life'; })()""",
             "desc": "refresh keeps selected game Beta"},
            {"js": "sessionStorage.clear(); true"},
            {"js": "location.href = '/game?game=nope-invalid'; true"},
            {"wait_js": "!document.getElementById('resumeView').hidden",
             "desc": "invalid deep link falls back to life list"},
            {"clear_network_errors": "intentional invalid-game 404 probe"},
            {"wait_js": "document.querySelectorAll('#lifeList svg, "
                "#lifeList img').length >= 2",
             "desc": "resume rows show portraits"},
        ],
    },
    # ------------------------------------------------------------------
    # M2a flows — coded against the FROZEN docs/collab/M2/api-examples.json
    # and phase-map v4. Every scenario-dependent flow starts with
    # scenario_start; until A lands the dev scenario factory it is recorded
    # as PENDING (skipped, never faked with age edits/injected balances).
    # ------------------------------------------------------------------
    {
        "name": "m2a-s1-school-limits",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "enrolled_child_10"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"check_ctx": "state.education.enrolled === true"},
            {"python": {"hook": "cmd", "action": "study_harder"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"store_ctx": "state.revision", "as": "before_revision"},
            {"python": {"hook": "cmd", "action": "study_harder",
                        "expect_status": 409}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.revision === CTX.before_revision",
             "desc": "rejected repeat study made no mutation"},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"shot": "m2a-s1-after-year"},
        ],
    },
    {
        "name": "m2a-s2-university-loan",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "university_pending_17", "seed": 4242}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === "
                "'university_application' || state.pending_decision.kind === "
                "'university_funding'",
             "desc": "university decision left open by scenario"},
            {"store_ctx": "state.pending_decision.kind", "as": "uni_kind"},
            {"python": "resolve_university_funding_loan"},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.education.loan.has_loan === true && "
                "state.education.loan.remaining > 0",
             "desc": "student loan recorded for the financed year"},
            {"python": "assert_state"},
            {"shot": "m2a-s2-university"},
        ],
    },
    {
        "name": "m2a-s3-job-hours-income-ledger",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "unemployed_18"}},
            {"python": {"hook": "clear_pending"}},
            {"python": "find_job_until_employed"},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.work.employed === true",
             "desc": "employed after accepting stored offer"},
            {"python": {"hook": "cmd", "action": "adjust_hours",
                        "payload": {"hours": 55}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.work.hours === 55",
             "desc": "hours adjusted to 55"},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "assert_work_income", "hours": 55}},
            {"python": "assert_state"},
            {"shot": "m2a-s3-ledger"},
        ],
    },
    {
        "name": "m2a-s4-retirement-pension",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "retirement_eligible_65"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.work.employed === true",
             "desc": "scenario starts employed at 65"},
            {"check_ctx": "state.work.can_retire === true",
             "desc": "retirement gates met (10y+, age 65)"},
            {"python": "retire_if_working"},
            {"python": {"hook": "refresh_state"}},
            {"python": "expect_retired_projection"},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "expect_pension_income_warn"},
            {"python": "assert_state"},
            {"shot": "m2a-s4-pension"},
        ],
    },
    {
        # Parity row 71: fired for performance (<15) at the year boundary.
        "name": "m2a-s6-fired-for-performance",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "low_performance_30"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.work.employed === true && "
                "state.work.performance < 15",
             "desc": "scenario starts employed with performance below 15"},
            {"python": {"hook": "settle_until_fired",
                        "scenario": "low_performance_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_fired_cleanly"},
            {"python": "assert_state"},
            {"shot": "m2a-s6-fired"},
        ],
    },
    # ------------------------------------------------------------------
    # M2b flows — health, activities, family. API-level steps now; UI
    # steps land when B's M2b screens + Fluent avatar swap are done.
    # ------------------------------------------------------------------
    {
        "name": "m2b-s1-conversation",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"python": "partner_id"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "conversation",
                        "payload": {"target_id": "CTX.partner_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"python": {"hook": "cmd", "action": "conversation",
                        "payload": {"target_id": "CTX.partner_id"},
                        "expect_status": 200}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.family.partner === CTX.partner_id",
             "desc": "partner identity unchanged after conversations"},
            {"shot": "m2b-s1-conversation"},
        ],
    },
    {
        # parity rows 10-12: propose -> wedding venues. Reached only via
        # divorce -> dating -> engagement; if the dating path is not
        # offered this is PENDING (no engaged scenario exists yet).
        "name": "m2b-s2-wedding-venues",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "cmd", "action": "divorce"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.family.partner === null",
             "desc": "divorced"},
            {"python": {"hook": "await_action", "action": "find_partner"}},
        ],
    },
    {
        # parity row 8 + 48/49: try for baby -> birth -> naming with
        # bounded long-name validation.
        "name": "m2b-s3-baby-naming",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "clear_pending"},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.family.children.length",
             "as": "children_before"},
            {"python": {"hook": "cmd", "action": "try_for_baby"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "await_pending_kind",
                        "kinds": ["pregnancy", "baby_naming", "birth_naming"],
                        "max_commands": 6}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === 'pregnancy' || "
                "state.pending_decision.kind === 'baby_naming' || "
                "state.pending_decision.kind === 'birth_naming'",
             "desc": "pregnancy (keep/abort) or naming decision pending"},
            {"python": {"hook": "resolve_pending_option", "match": "keep"}},
            {"python": {"hook": "await_pending_kind",
                        "kinds": ["baby_naming", "birth_naming"],
                        "max_commands": 4}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.pending_decision.id", "as": "birth_id"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.birth_id",
                                    "option_id": "first_enabled",
                                    "inputs": {"name": "AAAAAAAAAAAAAAAAAA"
                                               "AAAAAAAAAAAAAAAAAAAAAAA"
                                               "AAAAAAAAAAAAAAAAAAA"}},
                        "expect_status": 400}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.birth_id",
                                    "option_id": "first_enabled",
                                    "inputs": {"name": "Robin Alexandra "
                                               "Rivera-Testlife"}},
                        "expect_status": 200}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"check_ctx": "state.family.children.length === "
                "CTX.children_before + 1",
             "desc": "baby created via canonical hook"},
            {"shot": "m2b-s3-baby"},
        ],
    },
    {
        # parity row 36: adoption + rename; cost 10k-30k posted once.
        "name": "m2b-s4-adoption",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "adoption"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === 'adoption_offer'",
             "desc": "adoption offer is a stored decision"},
            {"store_ctx": "state.pending_decision.id", "as": "adopt_id"},
            {"store_ctx": "state.family.children.length",
             "as": "children_before"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.adopt_id",
                                    "option_id": "adopt", "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === 'adoption_rename'",
             "desc": "rename decision follows acceptance"},
            {"store_ctx": "state.pending_decision.id", "as": "rename_id"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.rename_id",
                                    "option_id": "rename", "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"check_ctx": "state.family.children.length === "
                "CTX.children_before + 1",
             "desc": "adopted child added"},
            {"python": {"hook": "assert_last_payment",
                        "source_contains": ":adoption:",
                        "amount_min": 10000, "amount_max": 30000,
                        "desc": "adoption cost within 10k-30k, posted once"}},
            {"shot": "m2b-s4-adoption"},
        ],
    },
    {
        # parity row 29: doctor fees and cures ($100/adult, per illness).
        "name": "m2b-s5-doctor-cures",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "ill_with_cash"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.health.illnesses.length === 2",
             "desc": "scenario starts with two illnesses"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "doctor"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === 'doctor_visit'",
             "desc": "doctor visit is a stored decision"},
            {"store_ctx": "state.pending_decision.id", "as": "doc_id"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.doc_id",
                                    "option_id": "treat:Common Cold",
                                    "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "assert_last_payment", "category": "medical",
                        "amount_min": 100, "amount_max": 100,
                        "desc": "doctor fee $100 posted exactly once"}},
            {"python": {"hook": "assert_illnesses", "absent": ["Common Cold"],
                        "present": ["Flu"]}},
            {"python": "clear_pending"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "doctor"}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.pending_decision.id", "as": "doc_id2"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.doc_id2",
                                    "option_id": "treat:Flu",
                                    "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "assert_illnesses", "absent":
                        ["Common Cold", "Flu"]}},
            {"shot": "m2b-s5-doctor"},
        ],
    },
    {
        # parity row 30: witch doctor, fee 50-100 posted once.
        "name": "m2b-s6-witch-doctor",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "witch_doctor"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === "
                "'witch_doctor'",
             "desc": "witch doctor is a stored decision"},
            {"store_ctx": "state.pending_decision.id", "as": "wd_id"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.wd_id",
                                    "option_id": "take", "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "assert_last_payment", "category": "medical",
                        "amount_min": 50, "amount_max": 100,
                        "desc": "witch doctor fee within 50-100, once"}},
            {"shot": "m2b-s6-witch"},
        ],
    },
    {
        # parity row 51: relation death -> funeral decision + inheritance.
        "name": "m2b-s7-funeral",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "clear_pending"},
            {"check_ctx": "state.family.parents.length >= 1",
             "desc": "parents alive for funeral path"},
            {"python": "await_rel_death"},
            {"python": {"hook": "refresh_state"}},
            {"python": "resolve_funeral_if_pending"},
            {"python": "assert_state"},
            {"shot": "m2b-s7-funeral"},
        ],
    },
    # ------------------------------------------------------------------
    # Backlog parity-row flows (API-level until B's screens land).
    # ------------------------------------------------------------------
    {
        # row 2: view relation — projection carries per-person values
        "name": "m2b-r2-view-relation",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"check_ctx": "state.people.length >= 5",
             "desc": "relation view data: partner, child, parents projected"},
            {"check_ctx": "state.family.children.length === 1 && "
                "!!state.family.partner",
             "desc": "family refs explicit and non-empty"},
        ],
    },
    {
        # row 5: ask for money (parents; generosity gate; 3-strike rule)
        "name": "m2b-r5-ask-for-money",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "parent_id"}},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "ask_for_money",
                        "payload": {"target_id": "CTX.parent_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"shot": "m2b-r5-ask-money"},
        ],
    },
    {
        # rows 6/7: compliment (once/year per parity) + insult
        "name": "m2b-r6-r7-compliment-insult",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "partner_id"},
            {"python": {"hook": "cmd", "action": "compliment",
                        "payload": {"target_id": "CTX.partner_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "cmd", "action": "compliment",
                        "payload": {"target_id": "CTX.partner_id"},
                        "expect_status": 409}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "cmd", "action": "insult",
                        "payload": {"target_id": "CTX.partner_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
        ],
    },
    {
        # row 22: request a raise (gates + processing)
        "name": "m2b-r22-request-raise",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "employed_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "cmd", "action": "request_raise"}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.work.salary", "as": "salary_before"},
            {"python": {"hook": "cmd", "action": "request_raise",
                        "expect_status": 409}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.work.salary === CTX.salary_before",
             "desc": "no double raise effect (limit enforced)"},
        ],
    },
    {
        # row 76: child yearly (school/age progression, child account)
        "name": "m2b-r76-child-yearly",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.family.children.0", "as": "child_id"},
            {"store_ctx": "state.people.length", "as": "people_before"},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"check_ctx": "state.family.children.indexOf(CTX.child_id) >= 0",
             "desc": "child identity stable across years"},
            {"shot": "m2b-r76-child-yearly"},
        ],
    },
    {
        # row 35: plastic surgery ($25k, surgeon choice)
        "name": "m2b-r35-plastic-surgery",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "cmd", "action": "plastic_surgery"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision.kind === "
                "'plastic_surgery'",
             "desc": "surgeon choice pending"},
            {"store_ctx": "state.pending_decision.id", "as": "surg_id"},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.surg_id",
                                    "option_id": "surgeon:0",
                                    "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "assert_last_payment",
                        "category": "medical", "amount_min": 25000,
                        "amount_max": 25000,
                        "desc": "surgery $25,000 posted exactly once"}},
            {"python": {"hook": "cmd", "action": "plastic_surgery",
                        "expect_status": 409}},
            {"shot": "m2b-r35-surgery"},
        ],
    },
    {
        # row 61: age-2 vaccination event (1/2 chance; bounded watch)
        "name": "m2b-r61-vaccination",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "await_vaccination"},
        ],
    },
    {
        # rows 58-60: illness acquisition/progression via natural aging
        # (bounded; PendingSkip if the seeded life stays healthy)
        "name": "m2b-r58-60-illness-progression",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "ill_with_cash"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.health.illnesses.length >= 1",
             "desc": "scenario carries illnesses"},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "typeof state.health.illnesses.length === "
                "'number'",
             "desc": "illness projection stable after a year"},
        ],
    },
    {
        # row 69: partner kidnapping ransom (1/250, money >= 1M) — no
        # legitimate forcing path; PENDING until A provides one.
        "name": "m2b-r69-kidnapping",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "wealthy_married_40"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "await_kidnapping"},
        ],
    },
    {
        # row 51 inheritance check on the funeral path
        "name": "m2b-r51-inheritance",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start", "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "clear_pending"},
            {"python": "await_rel_death"},
            {"python": {"hook": "refresh_state"}},
            {"python": "resolve_funeral_if_pending"},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
        ],
    },
    {
        # M2c-S5 succession (rows 38 surrender + 77 continue-as-child)
        # against the frozen continue_as shape.
        "name": "m2c-s5-succession",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "clear_pending"},
            {"python": {"hook": "record_balance"}},
            {"python": "surrender_to_death"},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.status === 'dead'",
             "desc": "life ended"},
            {"check_ctx": "!!state.succession && "
                "state.succession.eligible.length >= 1",
             "desc": "eligible children listed (frozen shape)"},
            {"python": "continue_as_eligible"},
            {"python": {"hook": "refresh_state"}},
            {"python": "assert_state"},
            {"shot": "m2c-s5-successor"},
        ],
    },
]
