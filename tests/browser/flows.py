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
    # ------------------------------------------------------------------
    # M2.5 flows (D2): dating app -> date; wedding planner/divorce ops;
    # editor law change with reprojection + replay; family tree + the
    # known-secrets projection. All at phone width (390x844).
    # ------------------------------------------------------------------
    {
        # dating_app action -> frozen v3 offer pending -> deterministic
        # stored acceptance -> date resolves through the real v3 resolver.
        # married_30 proves the existing spouse is excluded as a stranger;
        # D plants a genuinely unrelated adult fixture because the public
        # dating-app predicate requires an eligible person to exist.
        "name": "m25-dating-app",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "add_adult_stranger",
                        "name": "Riley Stranger"}},
            {"python": {"hook": "seed_next_draw", "seed": 31}},
            {"python": {"hook": "cmd", "action": "dating_app"}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.pending_decision && "
                "state.pending_decision.kind === 'm25_dating_app_date'",
             "desc": "dating-app offer is the frozen v3 pending"},
            {"check_ctx": "state.pending_decision.options.length === 2 && "
                "state.pending_decision.options.every(function(o){"
                "return o.enabled})",
             "desc": "accept/decline both enabled and free"},
            {"store_ctx": "state.pending_decision.bindings.target",
             "as": "dating_target"},
            {"check_ctx": "CTX.dating_target === CTX.stranger_id && "
                "CTX.dating_target !== state.family.partner",
             "desc": "fixture stranger chosen, not the existing spouse"},
            {"check_ctx": "state.pending_decision.sampled.accepted === true",
             "desc": "stored dating response pinned to acceptance boundary"},
            {"store_ctx": "state.pending_decision.id", "as": "dating_dec"},
            {"python": {"hook": "store_first_option", "match": "accept"}},
            {"python": {"hook": "cmd", "action": "resolve_decision",
                        "payload": {"decision_id": "CTX.dating_dec",
                                    "option_id": "CTX.opt_id",
                                    "inputs": {}}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.people.some(function(p){"
                "return p.id === CTX.dating_target})",
             "desc": "the date made the match a known person"},
            {"python": {"hook": "settle_turn"}},
            {"python": {"hook": "settle_turn"}},
            {"python": "load_game_in_page"},
            {"js": "var b=document.getElementById('m25Toggle');"
                   "if(b){b.click()}"},
            {"wait_js": "var c=document.getElementById('m25Card');"
                "c && !c.hidden && c.innerText.length > 20",
             "desc": "M2.5 dating & weddings screen mounts"},
            {"shot": "m25-dating-app"},
        ],
    },
    {
        # E7: start from the real married_with_child scenario, end its
        # auto-migrated neutral-law union through the public wedding route,
        # then REMARRY the same co-parent through C's authorised
        # courtship_offer -> stored response -> union_proposal -> stored
        # response journey. No in-process form_union setup shortcut is used.
        "name": "m25-divorce",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.player_id", "as": "player_id"},
            {"python": "partner_id"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview"}},
            {"check_ctx": "CTX.relview.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id}).length === 1",
             "desc": "one active union with the spouse"},
            {"check_ctx": "CTX.relview.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id})[0].shared_children.length "
                "=== 1",
             "desc": "the shared dependent child is bound to the union"},
            {"check_ctx": "Object.keys(CTX.relview.catalog.venues).some("
                "function(k){return CTX.relview.catalog.venues[k].base "
                "=== 0})",
             "desc": "wedding planner publishes a free venue tier"},
            {"store_js": "CTX.relview.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id})[0].union_id",
             "as": "old_union_id"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "wedding_op", "op": "divorce_union",
                        "args": {"union_id": "CTX.old_union_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.family.partner === CTX.partner_id && "
                "state.family.partner_status !== 2 && "
                "!state.available_actions.some(function(a){"
                "return a.id === 'divorce' && "
                "a.target_id === CTX.partner_id && a.enabled === true})",
             "desc": "N3 exact post-divorce state: ex-partner is not married "
                     "and legacy Divorce is disabled"},
            {"check_ctx": "state.family.children.length === 1",
             "desc": "shared child untouched by the divorce"},
            {"python": {"hook": "real_offer_journey",
                        "target_id": "CTX.partner_id"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview_real"}},
            {"check_ctx": "CTX.relview_real.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id}).length === 1",
             "desc": "real offer journey remarries the co-parent"},
            {"check_ctx": "CTX.relview_real.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id})[0].shared_children.length "
                "=== 1",
             "desc": "shared dependent child bound to the new union"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "wedding_op", "op": "divorce_union",
                        "args": {"union_id": "CTX.real_union_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.family.children.length === 1",
             "desc": "shared child untouched by the second divorce"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview2"}},
            {"check_ctx": "CTX.relview2.unions.filter(function(u){"
                "return u.union_id === CTX.real_union_id && "
                "u.status === 'divorced'}).length === 1",
             "desc": "real-journey union recorded divorced (not erased)"},
            {"check_ctx": "CTX.relview2.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id}).length === 0",
             "desc": "no active union remains with the spouse"},
            {"python": "load_game_in_page"},
            {"js": "var b=document.getElementById('m25Toggle');"
                   "if(b){b.click()}"},
            {"wait_js": "var c=document.getElementById('m25Card');"
                "c && !c.hidden && c.innerText.length > 20",
             "desc": "M2.5 screen mounts after the divorce"},
            {"shot": "m25-divorce"},
        ],
    },
    {
        # Deterministic stored-response boundaries for C's targeted offer
        # actions. Seed 153 forces refusal above the 0.98 p_accept clamp;
        # seed 31 forces acceptance below the 0.02 floor. No retry loop.
        "name": "m25-offer-boundaries",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "employed_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "add_adult_stranger",
                        "name": "Refused Stranger", "as_": "refuse_id"}},
            {"python": {"hook": "refusal_boundary",
                        "target_id": "CTX.refuse_id"}},
            {"python": {"hook": "add_adult_stranger",
                        "name": "Accepted Stranger", "as_": "accept_id"}},
            {"python": {"hook": "real_offer_journey",
                        "target_id": "CTX.accept_id",
                        "as_": "accept_union_id"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "boundary_view"}},
            {"check_ctx": "CTX.boundary_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.accept_id}) && "
                "!CTX.boundary_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.refuse_id})",
             "desc": "accepted boundary forms union; refused boundary does not"},
            {"shot": "m25-offer-boundaries"},
        ],
    },
    {
        # E7 completion evidence: start from a real spouse+child union,
        # add an unrelated adult through D's fixture, form a SECOND active
        # union through the same public offer journey, hold the free
        # courthouse ceremony for that second union, then divorce only the
        # second union. The original spouse union and shared child must
        # survive selected divorce.
        "name": "m25-two-union-selected-divorce",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.player_id", "as": "player_id"},
            {"python": "partner_id"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "baseline_view"}},
            {"check_ctx": "CTX.baseline_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id && "
                "u.shared_children.length === 1})",
             "desc": "baseline spouse union has the shared child"},
            {"python": {"hook": "add_adult_stranger",
                        "name": "Sam Second", "as_": "second_id"}},
            {"python": {"hook": "real_offer_journey",
                        "target_id": "CTX.second_id",
                        "as_": "second_union_id"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "two_union_view"}},
            {"check_ctx": "CTX.two_union_view.unions.filter(function(u){"
                "return u.status === 'active' && "
                "(u.partner.id === CTX.partner_id || "
                "u.partner.id === CTX.second_id)}).length === 2",
             "desc": "two simultaneous active unions are public"},
            {"check_ctx": "CTX.two_union_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id && "
                "u.shared_children.length === 1})",
             "desc": "child remains bound to the original spouse union"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "wedding_op", "op": "plan_wedding",
                        "args": {"union_id": "CTX.second_union_id",
                                 "venue_id": "courthouse", "guests": 0,
                                 "outfit_id": "hand_me_down",
                                 "honeymoon_id": "none"}}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "wedding_view"}},
            {"check_ctx": "CTX.wedding_view.unions.some(function(u){"
                "return u.union_id === CTX.second_union_id && "
                "u.status === 'active' && u.ceremony_held === true})",
             "desc": "second union receives the free courthouse ceremony"},
            {"check_ctx": "CTX.wedding_view.money === CTX.balance_before",
             "desc": "free wedding configuration moved no money"},
            {"python": {"hook": "wedding_op", "op": "divorce_union",
                        "args": {"union_id": "CTX.second_union_id"}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.family.children.length === 1",
             "desc": "family child count unchanged by selected divorce"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "after_selected_view"}},
            {"check_ctx": "CTX.after_selected_view.unions.some(function(u){"
                "return u.union_id === CTX.second_union_id && "
                "u.status === 'divorced'})",
             "desc": "selected second union recorded divorced"},
            {"check_ctx": "CTX.after_selected_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id && "
                "u.shared_children.length === 1})",
             "desc": "spouse union and shared child survive selected divorce"},
            {"check_ctx": "!CTX.after_selected_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.second_id})",
             "desc": "no active second union remains"},
            {"check_ctx": "CTX.after_selected_view.money === "
                "CTX.wedding_view.money",
             "desc": "selected divorce of childless second union moved no money"},
            {"python": "load_game_in_page"},
            {"js": "var b=document.getElementById('m25Toggle');"
                   "if(b){b.click()}"},
            {"wait_js": "var c=document.getElementById('m25Card');"
                "c && !c.hidden && c.innerText.length > 20",
             "desc": "M2.5 screen mounts after selected divorce"},
            {"shot": "m25-two-union-selected-divorce"},
        ],
    },
    {
        # E7/editor law: replace the D2 in-process union setup with the
        # public offer journey, then apply a save-local jurisdiction
        # override and see THAT union's recognition reprojected. Also
        # prove replay idempotence and optimistic concurrency.
        "name": "m25-editor-law",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"store_ctx": "state.player_id", "as": "player_id"},
            {"python": "partner_id"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview"}},
            {"store_js": "CTX.relview.unions.filter(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id})[0].union_id",
             "as": "old_union_id"},
            {"python": {"hook": "wedding_op", "op": "divorce_union",
                        "args": {"union_id": "CTX.old_union_id"}}},
            {"python": {"hook": "real_offer_journey",
                        "target_id": "CTX.partner_id"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview_real"}},
            {"check_ctx": "CTX.relview_real.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id && "
                "u.recognition === 'recognized'})",
             "desc": "real-journey union recognized under baseline law"},
            {"python": {"hook": "editor_apply", "commands": [
                {"name": "edit_jurisdiction", "scope": "jurisdiction",
                 "target_id": "jur_metropolis_charter",
                 "patch": {"recognition": {
                     "monogamous_civil": "unrecognized"}}}]}},

            {"python": {"hook": "api_get", "suffix": "policy-state",
                        "as_": "pol_after"}},
            {"check_ctx": "CTX.pol_after.overrides.jurisdictions && "
                "CTX.pol_after.overrides.jurisdictions.jur_metropolis_charter"
                ".recognition.monogamous_civil === 'unrecognized'",
             "desc": "save-local law override persisted"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview2"}},
            {"check_ctx": "CTX.relview2.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.partner_id && "
                "u.recognition === 'unrecognized'})",
             "desc": "existing union's recognition reprojected (A4)"},
            {"check_ctx": "CTX.relview2.money === CTX.relview_real.money",
             "desc": "editor law change moved no money"},
            # replay-exact retry: same request_id + the pre-apply
            # revision must return the recorded response, not a 409
            {"python": {"hook": "editor_apply", "commands": [
                {"name": "edit_jurisdiction", "scope": "jurisdiction",
                 "target_id": "jur_metropolis_charter",
                 "patch": {"recognition": {
                     "monogamous_civil": "unrecognized"}}}],
                        "request_id": "CTX.ed_result_request_id",
                        "revision": "CTX.ed_result_revision",
                        "as_": "ed_replay"}},
            {"check_ctx": "JSON.stringify(CTX.ed_replay) === "
                "JSON.stringify(CTX.ed_result)",
             "desc": "replay with the recorded request_id returns the "
                    "recorded response byte-identically"},
            {"python": {"hook": "editor_apply", "commands": [
                {"name": "edit_jurisdiction", "scope": "jurisdiction",
                 "target_id": "jur_metropolis_charter",
                 "patch": {"recognition": {
                     "monogamous_civil": "recognized"}}}],
                        "revision": "CTX.ed_result_revision",
                        "expect_status": 409}},
            {"python": "load_game_in_page"},
            {"js": "var b=document.getElementById('editorToggle');"
                   "if(b){b.click()}"},
            {"wait_js": "document.body.innerText.length > 40",
             "desc": "editor screen mounts"},
            {"shot": "m25-editor-law"},
        ],
    },
    {
        # L1: a configured forbidden-union ceremony fine is a real charge,
        # not just an offer-time forecast. D drives the public editor ->
        # courtship/proposal -> free ceremony path and asserts the $500
        # civil fine posts once through the legal ledger.
        "name": "m25-law-ceremony-fine",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "employed_30"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "add_adult_stranger",
                        "name": "Fine Stranger", "as_": "fine_id"}},
            {"python": {"hook": "editor_apply", "commands": [
                {"name": "edit_jurisdiction", "scope": "jurisdiction",
                 "target_id": "jur_metropolis_charter",
                 "patch": {
                     "recognition": {"monogamous_civil": "forbidden",
                                     "plural_civil": "forbidden"},
                     "consequences": [{
                         "trigger": "ceremony_of_unrecognized_union",
                         "effect": "civil_fine",
                         "ledger_category": "legal",
                         "amount": {"randint": [500, 500]},
                         "max_per_occurrence_key": "union_id"}]}}]}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "real_offer_journey",
                        "target_id": "CTX.fine_id",
                        "as_": "fine_union_id"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "fine_view"}},
            {"check_ctx": "CTX.fine_view.unions.some(function(u){"
                "return u.status === 'active' && "
                "u.partner.id === CTX.fine_id && "
                "u.recognition === 'forbidden'})",
             "desc": "public proposal persists forbidden recognition"},
            {"python": {"hook": "record_balance"}},
            {"python": {"hook": "wedding_op", "op": "plan_wedding",
                        "args": {"union_id": "CTX.fine_union_id",
                                 "venue_id": "courthouse", "guests": 0,
                                 "outfit_id": "hand_me_down",
                                 "honeymoon_id": "none"}}},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "state.money === CTX.balance_before - 500",
             "desc": "L1 civil fine reduced money by configured amount"},
            {"python": {"hook": "assert_last_payment",
                        "category": "legal", "amount_min": 500,
                        "amount_max": 500,
                        "desc": "L1 civil fine posted exactly once"}},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "fine_after_view"}},
            {"check_ctx": "CTX.fine_after_view.unions.some(function(u){"
                "return u.union_id === CTX.fine_union_id && "
                "u.status === 'active' && u.ceremony_held === true})",
             "desc": "forbidden-union ceremony held after fine"},
            {"python": "load_game_in_page"},
            {"js": "var b=document.getElementById('m25Toggle');"
                   "if(b){b.click()}"},
            {"wait_js": "var c=document.getElementById('m25Card');"
                "c && !c.hidden && c.innerText.length > 20",
             "desc": "M2.5 screen mounts after fined ceremony"},
            {"shot": "m25-law-ceremony-fine"},
        ],
    },
    {
        # Family tree renders the player, spouse and child at phone
        # width; the secrets surface is the player-known projection
        # (facts/reputation-as-known) with the pending's display reasons
        # exposed only through the same predicate as get_state.
        "name": "m25-family-tree",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"python": {"hook": "api_get", "suffix": "family-tree",
                        "as_": "tree"}},
            {"check_ctx": "CTX.tree.tree.nodes.length >= 3",
             "desc": "tree projects player, spouse and child"},
            {"check_ctx": "CTX.tree.tree.edges.some(function(e){"
                "return e.kind === 'parentage'})",
             "desc": "parentage edges present (real graph, no invention)"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview"}},
            {"check_ctx": "Array.isArray(CTX.relview.facts) && "
                "Array.isArray(CTX.relview.reputation)",
             "desc": "known-secrets projection arrays present"},
            {"check_ctx": "CTX.relview.facts.every(function(f){"
                "return !!f.fact_id && !!f.kind})",
             "desc": "every known fact carries id+kind (provenance)"},
            {"python": "load_game_in_page"},
            {"js": "var b=document.getElementById('familyTreeToggle');"
                   "if(b){b.click()}"},
            {"wait_js": "document.querySelector('.ftree') !== null",
             "desc": "family tree renders"},
            {"shot": "m25-family-tree"},
        ],
    },
    {
        # R11: secrecy exercised through HTTP. A private journal row and
        # a player-unknown secret fact are planted in the save (same
        # planting as A's sentinel test); every assertion below reads
        # REAL HTTP responses: get_state and relationships-view must not
        # contain the sentinel, the hidden fact id, or the secret fact;
        # after the player learns it, the fact appears in the view while
        # the private row stays hidden.
        "name": "m25-secrecy",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_with_child"}},
            {"python": {"hook": "refresh_state"}},
            {"python": "partner_id"},
            {"python": "plant_secret"},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "JSON.stringify(state).indexOf("
                "'PRIVATE_SENTINEL_M25') === -1",
             "desc": "HTTP get_state hides the private journal row"},
            {"check_ctx": "JSON.stringify(state).indexOf('f_hidden_m25') "
                "=== -1",
             "desc": "HTTP get_state hides the private fact id"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview"}},
            {"check_ctx": "CTX.relview.facts.every(function(f){"
                "return f.fact_id !== CTX.secret_fact_id})",
             "desc": "the secrets view omits the unknown fact"},
            {"python": "learn_secret"},
            {"python": {"hook": "refresh_state"}},
            {"check_ctx": "JSON.stringify(state).indexOf("
                "'PRIVATE_SENTINEL_M25') === -1",
             "desc": "private row still hidden after learning"},
            {"python": {"hook": "api_get", "suffix": "relationships-view",
                        "as_": "relview2"}},
            {"check_ctx": "CTX.relview2.facts.some(function(f){"
                "return f.fact_id === CTX.secret_fact_id})",
             "desc": "the learned fact now appears in the secrets view"},
            {"shot": "m25-secrecy"},
        ],
    },

    {
        # v2 shell route visibility (Kimi D, stage1-evaluation.md S1/S2):
        # actual-visibility (checkVisibility) regressions for the
        # More->Editor route, All lives -> resume, and death -> legacy.
        # Runs against the CURRENT tree's v2 shell, not the pre-v2
        # baseline. The S1 exact signature is WARN-masked only while B's
        # scoped-CSS fix is open; it self-removes once [hidden] is
        # honored, and any other deviation fails hard.
        "name": "v2-route-visibility",
        "start": "/game",
        "setup_js": "sessionStorage.removeItem('game_id')",
        "steps": [
            {"python": "phone_viewport"},
            {"python": {"hook": "scenario_start",
                        "scenario": "married_30"}},
            {"js": "window.location.href = '/game?game=' + "
                   "window.CTX.game_id; true"},
            {"wait_js": "!!document.getElementById('gameView') && "
                "!document.getElementById('gameView').hidden",
             "desc": "v2 shell game view loaded"},

            {"js": "(() => { const hb = document.querySelector("
                "'.v2-header-btn'); if (hb) hb.click(); const b = "
                "Array.from(document.querySelectorAll('.v2-more button'))"
                ".find(x => x.textContent.indexOf('Character editor') "
                ">= 0); if (b) { b.click(); return true; } "
                "return false; })()"},
            {"wait_js": "!!document.getElementById('editorPanel') && "
                "!document.getElementById('editorPanel').hidden",
             "desc": "editor panel opened from More menu"},
            {"check_js": "(() => { const e = document.getElementById("
                "'editorPanel'); return !!e && !e.hidden && "
                "e.checkVisibility() === true; })()",
             "desc": "More->Editor: editor actually visible from Home"},

            {"python": {"hook": "check_shell_visibility",
                        "visible": "resumeView", "hidden": "gameView",
                        "action_js": "document.getElementById("
                            "'backToLives').click(); true"}},
            {"python": "resume_first_life"},

            {"python": "refresh_state"},
            {"python": "surrender_to_death"},
            {"js": "sessionStorage.setItem('game_id', "
                "window.CTX.game_id); true"},
            {"js": "window.location.href = '/game?game=' + "
                   "window.CTX.game_id; true"},
            {"wait_js": "!!document.getElementById('legacyView') && "
                "!document.getElementById('legacyView').hidden",
             "desc": "legacy view shown after death"},
            {"python": {"hook": "check_shell_visibility",
                        "visible": "legacyView", "hidden": "gameView"}},
        ],
    },
]
