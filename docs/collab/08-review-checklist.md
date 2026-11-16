# Avatar milestone review checklist

Prepared before implementation reports. This is a review procedure, not a finding that checks passed. Scope: approved 03-plan.md, frozen 04-appearance-contract.md and tasks 05/06. Read both Kimi reports before beginning. Codex review → Claude review → user report. Do not expand scope into full-game web integration.

## 1. Inventory and contract

- [ ] Read reports, inspect focused diffs and preserve pre-existing local changes. Note unfinished claims and actual test commands/results.
- [ ] Verify the frozen API signatures, module locations, JSON fields and schema version; both agents use the same catalogs and aging implementation, with no remaining stub.
- [ ] IDs are stable strings; labels and color values are catalog metadata. Confirm the category counts and required styles in 04-appearance-contract.md, plus 12+ diverse fixtures.
- [ ] `Appearance.to_dict/from_dict` round-trip valid input. Missing/unknown IDs and malformed supported fields yield documented defaults or clear validation errors without renderer crashes. Do not silently interpret a future incompatible schema as current.
- [ ] No unapproved dependencies, gameplay balance changes or unrelated fixes.

## 2. Generation, inheritance and identity

- [ ] Every actual Person subclass receives a valid appearance, including all reachable creation paths, partners and adopted children.
- [ ] Same cosmetic seed/input produces the same result; generation/aging/rendering leave global gameplay RNG state unchanged. Check indirect helper calls as well as direct imports.
- [ ] Appearance is generated once, not whenever a portrait is requested or a person ages. No gameplay stat, gender or income restrictions are introduced by cosmetic choices.
- [ ] Biological children inherit only physical fields, with documented bounded variation; verify two parents, one parent and no parents. Adopted children are generated independently.
- [ ] Child parent links finalized after construction are handled deliberately, without rerolling already-established/saved appearance. Styling is not copied as genetic inheritance.
- [ ] NPC accessories vary naturally; babies have no generated hats/jewelry. Styling remains selectable regardless of gender. Confirm how creator selections interact with age preview without destroying stored selections.
- [ ] Succession preserves the chosen child's appearance and name, excludes that child from siblings, and preserves reconstructed relatives' names/appearance. Check zero/one/multiple siblings and surviving parent/partner cases; flag broader pre-existing graph issues separately.

## 3. Persistence and load safety

- [ ] New save → reload retains all appearance fields, including seed, marks and accessories.
- [ ] Load a genuinely old-format save lacking appearance; inspect every reachable person, save again, reload and confirm identities remain unchanged.
- [ ] Migration handles cycles and shared references without recursion failure, duplicate generation or replacing valid existing appearance.
- [ ] Exact new class is allowed and its module imported before loading. Existing restricted-unpickler rejection of unexpected globals/persistent references remains effective; no permissive fallback to ordinary pickle loading.
- [ ] Legacy untrusted-save protections are preserved. Test fixtures must not require executing unsafe serialized content.

## 4. Renderer and visual inspection

- [ ] SVG uses the required fixed viewBox, deterministic output, controlled catalog values and no scripts, event handlers, external references or embedded untrusted markup. Validate/escape size and user text wherever applicable.
- [ ] `aged_view` supplies the real stage/gray/wrinkle/recession values; render consumes them. Verify boundaries 2/3, 12/13, 17/18, 49/50 and later progression; repeated rendering does not flicker or mutate appearance.
- [ ] Inspect all catalog styles in the gallery, including all skin tones and age stages at 32 and 128 px; spot-check 24 and 40 px readability.
- [ ] Inspect hair/headwear and glasses/earring overlaps, facial hair, marks and outfits. No clipped features, hidden faces, unintended transparency or duplicated SVG IDs when several avatars appear together.
- [ ] Face shapes, features and age proportions are visibly distinct; complexion/eye/hair colors are recognizable. Dark features remain legible across skin tones.
- [ ] Portraits remain recognizably the same person across ages; aging does not overwrite saved hair color/style. Record screenshot evidence or clear visual findings rather than claiming appearance quality from unit tests alone.

## 5. Creator, gallery and shared person component

- [ ] `./play-web.sh` works from the documented location, prints a usable URL and binds only to 127.0.0.1. Verify routes `/creator`, `/gallery` and the relationships demo; inspect startup and browser-console failures.
- [ ] Creator has live preview; Body/Face/Hair/Style/Accessories tabs; labeled swatches; 4+ presets; all/category randomization; reset and accessory removal; name/gender/country fields; age preview.
- [ ] Category randomization changes only the intended category. Presets/reset have clear predictable behavior; preview age changes do not silently alter saved physical data.
- [ ] Exported appearance JSON validates through the Python model and renders identically on reload. Make clear that download/export does not yet create a playable game. Do not imply country affects simulation in this milestone.
- [ ] Shared person chip shows avatar, name and role (optional relationship bar), works for long names and multiple instances, and is used on the fixture relationships demo. No initial-letter substitute for a supported person.
- [ ] Gallery actually covers the required combinations, with useful labels and both sizes; it must not merely repeat a few attractive presets.
- [ ] Keyboard access: tabs, swatches, sliders and buttons have visible focus, meaningful labels and selected states; no pointer-only controls. Check contrast, zoom and a phone-width viewport for overflow.
- [ ] User names are rendered as text; any local endpoints validate inputs and do not expose arbitrary filesystem paths. If requests mutate server state, verify origin handling.

## 6. Verification and review outcome

- [ ] Run reported focused tests and a browser smoke check; add/ask for targeted regression coverage where a substantive defect is found. Check assertions verify behavior rather than merely field existence.
- [ ] Minimum evidence: deterministic generation/rendering; RNG isolation; inheritance/adoption; all subclasses; old/new save loading; unknown-ID fallback; succession identity; renderer/creator smoke.
- [ ] Clearly separate blocking defects, follow-up improvements and pre-existing issues. Cite file locations and reproducible steps for blockers.
- [ ] After fixes, rerun affected checks and the short browser journey. Report exactly what was inspected and any visual/browser checks unavailable in this environment.
- [ ] Handoff to Claude with completion status against this milestone only. Full engine separation and portraits throughout actual game screens belong to the next milestone; do not mark those complete from the demo.
