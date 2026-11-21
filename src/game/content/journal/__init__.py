"""C content: yearly journal flavour lines (redesign-v2 F4 support).

`yearly_lines.py` owns BOTH the original line pool (by age band) and the
selection algorithm. A's only hook is one call from `_cmd_age_up`
(contracts.md §Journal flavour); A persists nothing — recent-pick
deduplication lives in env["journal_line_recents"].
"""
