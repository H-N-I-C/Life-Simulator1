# M3 task proposal — C owns skills and training end to end

Await Claude assignment/FROZEN contracts. Own skills rules, feature service integration, Skills UI, skill data/events and tests. Feature scope replaces the old restriction to legacy functions once Claude records the M3 manifest.

## Exclusive files (proposed manifest)

`src/game/features/skills/**`, `src/game/content/skills/**`, `web/static/features/skills/**`, `tests/test_skills*.py`. No edits to A/B feature internals, shared service/coordinator/finance or common frontend files. Send exact integration requests to A(engine) or B(shell). Existing Player/Child fields should not be patched independently; choose feature-state bridge at freeze.

## Traps to flag first

A named skill cannot just be a relabeled smarts stat. Infinite training clicks break progression; repeated annual hooks can apply gains twice. Set/dict iteration before random draws breaks seed reproduction. Double-counting job/study experience plus manual training can overshoot limits. A threshold reached during education (after work_income) must not trigger a promotion in an already-finished phase. Costs/skill caps/limits must produce honest actual deltas and legal free fallback actions.

## Build and demonstrate

1. Define practical/organization/communication/care in versioned data with range0–100, deterministic initial defaults, bounded training options and annual limits. Freeze shared skill requirements/contribution queries with A/B. No hidden looks/gender/income restrictions on access.
2. Implement training actions and annual progress contributions through the accepted M2 context/result/ledger boundary. Publish exact costs and expected effects; spend no cash on failed eligibility. Saturated skills stop generating misleading +progress journal values or duplicate qualification rewards.
3. Build Skills UI with values/progress, available training, limits, known costs and concrete linked job/program requirements. Register your own frontend module through B's shell contract; don't ask B to implement the feature for you.
4. Connect job practice and study contributions with their owners through frozen APIs. A/B retain their domain authority; skills module cannot grant a promotion or credential through an arbitrary set_flag.
5. Author skill/training events in the frozen event format, retaining a zero-cost decline/rest choice. D checks content/data validity; C owns meaningful outcomes.
6. Backfill skill state once for M2 saves without consuming cosmetic/gameplay RNG on reads, preserve person_id, and retain the successor's own skills. Do not derive lifelong expertise from current age alone.

## Tests/report

Real players/service: reject invalid IDs/types/targets; annual usage and cap edges; exact training costs/deltas; seeded reproducibility/global RNG isolation; one gain per persisted occurrence; job/study contribution order; role/program requirement changes; $0 fallback and legacy save/restart. Provide an actual browser train→eligibility-change journey for M3a with B. Focused tests only. Update status on work/block/done; report exact files/results/limits, ticked full 00-SELF-CHECK and hc send t7. Wait for consolidated stage review; do not launch a per-feature Codex review.
