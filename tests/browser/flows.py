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
             "desc": "creator swatches render"},
            {"shot": "creator-initial"},

            # Select a skin tone; focus must stay on the activated swatch
            # across the async preview round-trip.
            {"js": """document.querySelector(
                '.swatch[data-category="skin_tone"]').focus()"""},
            {"js": "document.activeElement.click()"},
            {"wait_js": """document.activeElement &&
                document.activeElement.dataset.category === 'skin_tone'""",
             "desc": "focus retained after swatch click"},
            {"wait_js": """document.activeElement &&
                document.activeElement.getAttribute('aria-pressed') ===
                'true'""",
             "desc": "activated swatch is aria-pressed"},

            # Tab arrow-key traversal between tabs.
            {"js": "document.getElementById('tab-face').focus()"},
            {"key": "ArrowRight"},
            {"check_js": "document.activeElement.id === 'tab-hair'",
             "desc": "ArrowRight moves to next tab"},

            # Multi-select marks: freckles + mole (activate the Face tab
            # first — the tab bar rebuilt the options above).
            {"js": "document.getElementById('tab-face').click(); true"},
            {"wait_js": """document.querySelectorAll(
                '.swatch[data-category="marks"]').length >= 5""",
             "desc": "face tab shows mark swatches"},
            {"js": """[...document.querySelectorAll('.swatch')].find(
                s => s.dataset.category === 'marks' &&
                     s.dataset.value === 'freckles').click()"""},
            {"js": """[...document.querySelectorAll('.swatch')].find(
                s => s.dataset.category === 'marks' &&
                     s.dataset.value === 'mole').click()"""},
            {"wait_js": """[...document.querySelectorAll(
                '.swatch[data-category="marks"]')].filter(
                s => ['freckles','mole'].includes(s.dataset.value))
                .every(s => s.getAttribute('aria-pressed') === 'true')""",
             "desc": "marks are multi-selectable"},

            # Export the appearance JSON and validate it in Python.
            {"js": "document.getElementById('printBtn').click()"},
            {"store_js": "document.querySelector('pre.export').textContent",
             "as": "export_json"},
            {"python": "validate_export"},

            # Age slider sweep keeps a valid preview.
            {"js": """const a = document.getElementById('age');
                     for (let v = 0; v <= 80; v += 8) {
                       a.value = v;
                       a.dispatchEvent(new Event('input'));
                     } true"""},
            {"wait_js": """(() => { const s = document.querySelector(
                '#preview svg'); return s && !document.getElementById('preview')
                .title; })()""",
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

            # Player avatar renders as inline SVG from /api/render.
            {"wait_js": "document.querySelector('#playerCard .avatar svg')",
             "desc": "player avatar rendered"},
            {"shot": "game-reloaded"},
        ],
    },
]
