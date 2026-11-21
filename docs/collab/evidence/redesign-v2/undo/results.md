# Undo browser tests (Kimi D, QA — 2026-11-20)

Harness: tools/feel_undo.py — real server + headless Chromium, one launch, fresh tab per scenario. TMPDIR=~/.lifesim-tmp; profile deleted after the run.

- **PASS** fresh-life disabled reason — spec={'enabled': False, 'reason': 'there is no year to undo'} btn={'text': '↩ Undo last year', 'disabled': True, 'reason': 'there is no year to undo', 'hidden': False}
- **PASS** scenario life starts undo-enabled [390] — scenario driver ages to the target age, so a snapshot exists: spec={'enabled': True, 'reason': None}
- **PASS** age_up accepted [390] — rev 94->95
- **PASS** enabled after age [390] — {'enabled': True, 'reason': None}
- **PASS** confirm arm [390] — {'text': 'Tap again to confirm', 'disabled': False, 'reason': 'Rewinds the last year — no re-roll.', 'hidden': False}
- **PASS** undo restores age, monotonic revision [390] — age 31->30 rev 95->96
- **PASS** consumed reason [390] — spec={'enabled': False, 'reason': 'only one year can be undone'} btn={'text': '↩ Undo last year', 'disabled': True, 'reason': 'only one year can be undone', 'hidden': False}
- **PASS** redo age_up accepted [390]
- **PASS** redone year replays exactly [390] — timeline identical=True
- **PASS** scenario life starts undo-enabled [320] — scenario driver ages to the target age, so a snapshot exists: spec={'enabled': True, 'reason': None}
- **PASS** age_up accepted [320] — rev 96->97
- **PASS** enabled after age [320] — {'enabled': True, 'reason': None}
- **PASS** confirm arm [320] — {'text': 'Tap again to confirm', 'disabled': False, 'reason': 'Rewinds the last year — no re-roll.', 'hidden': False}
- **PASS** undo restores age, monotonic revision [320] — age 31->30 rev 97->98
- **PASS** consumed reason [320] — spec={'enabled': False, 'reason': 'only one year can be undone'} btn={'text': '↩ Undo last year', 'disabled': True, 'reason': 'only one year can be undone', 'hidden': False}
- **PASS** redo age_up accepted [320]
- **PASS** redone year replays exactly [320] — timeline identical=True
- **PASS** stale setup: age from another tab
- **PASS** stale tab cannot mutate (age/revision unchanged) — console/exceptions=[]
- **PASS** rapid burst (4 taps) = exactly one undo — age 31->30 rev 112->113
- **PASS** reload resets armed confirm — {'text': '↩ Undo last year', 'disabled': False, 'reason': '', 'hidden': False}
- **PASS** undo persists across reload with consumed reason — age=30 spec={'enabled': False, 'reason': 'only one year can be undone'}
- **PASS** pending decision created for undo test — par_school_project
- **PASS** undo row hidden while a pending is open — None
- **PASS** undo after resolving the pending: age restored — pending=False age=32->31

Screenshots: this directory (names mirror the scenario steps).
