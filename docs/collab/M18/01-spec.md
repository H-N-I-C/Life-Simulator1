# M18 — Player scenario and content editor

User-approved addition, 2026-11-17. Read [delivery rules/dependencies](../M13-20-plan.md). Builds on the established versioned event pipeline, character editor and M13–M17 feature registrations. This is player-facing authoring, extending earlier developer content tools.

## Outcome

Create starting families, fictional countries/jurisdictions and event packs; validate them, export files and play imported copies. Authors can build unusual adult social settings and contrasting starts while the engine preserves protected age/consent/identity rules.

## Playable stages

- **M18a — Scenario designer:** build a valid starting household, choose a world period/place/resources and preview how family, education and finances are initialized. Launch a new life from it without altering an existing save.
- **M18b — Event/setting authoring:** edit declared roles, conditions, choices, costs/effects and audience using forms; customize fictional policy/world presets. Preview eligibility and a $0 legal exit in a test life.
- **M18c — Export/import:** package scenarios/settings/events with a manifest, export, import into a fresh local profile and run the same playable journey. Show dependencies, validation errors and version/update choices clearly.

## Content and model

Ship editable examples: blended family, difficult financial start, adult plural-union setting and a short original event chain. Formats are declarative/versioned with namespaced IDs, required feature/schema versions and an explicit pack version. Export/import does not execute pack-provided Python/JavaScript or silently overwrite built-in content.

Freeze manifest/dependency rules, authoring projections, safe effect/condition allowlists, scenario constructors and compatibility tests. Existing saves pin referenced definitions and stored pending outcomes; updating/removing a pack must not reinterpret a spouse binding, erase history or strand a pending decision. Scenario authoring creates a new chronology; the in-play editor cannot age an existing minor into eligibility.

## Browser acceptance and exit checks

1. Author a starting family and short chain, preview valid state, launch it, resolve choices and reload.
2. Export/import the pack and reproduce that journey. Reject duplicate IDs, unknown effects, dangling roles, cycles, unsupported versions and missing free options with specific typed diagnostics.
3. Attempt underage romance, a lowered adulthood setting, false romance flags and editable consent/RNG/ledger fields: reject before installation or runtime effects. All NPC paths retain the same guard.
4. Test pack update/uninstall during an active decision, missing optional dependencies and an old-save reload; preserve pinned outcomes or provide an explicit safe recovery.
5. Test bounded pack size/condition depth and private-data export previews. Published sharing/catalog hosting is a separate deployment decision; manual file sharing is playable first.

## Traps and ownership

Avoid a second permissive validator, scripts disguised as expressions, globally mutable catalogs or importing a pack directly into a running life. Author previews must call the real engine. One owner delivers forms, validation and import/export end to end; domain owners register safe capabilities. D independently checks malicious/malformed packages and successful round trips.
