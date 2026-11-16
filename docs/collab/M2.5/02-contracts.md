# M2.5 proposed contracts — NOT FROZEN

C owns event schema/loader; A graph/coordinator/ledger/save integration; B policy/editor; Claude freezes. Publish executable examples before coding. Frozen M2 v2.1 remains supported; v3 is a distinct schema, not a relabelled old file.

## 1. Identity, protected chronology and consent

Every person has immutable person_id. Adult eligibility derives from protected simulation chronology advanced by normal annual turns, not an editable display age. Backfill trusted chronology once from accepted saves. Both living participants must be naturally18+ at offer AND resolution for romantic/sexual/marriage actions. Actions, NPC offers, automatic events, editor, content effects and legacy adapters call the same non-overridable guard.

Consent is occurrence/action/participant-specific and revocable, not a permanent yes or editable personality score. Refusal/cancellation remains possible at maximum attraction/openness. Editor changes cannot create consent or establish a union by setting relationship100.

Offer: offer_id, initiator_id, target_id, interaction_kind, relationship/union references, creation_turn, policy_revision, stored bindings/sampled outcomes, status, expiry and attempt/cooldown. Resolve validates membership/options/chronology before mutation. Minors' ordinary family/social interactions do not use romantic offers. Self-targets, duplicate members and dangling IDs reject as graph errors.

## 2. Relationships, unions and family graph (A)

- Relationship edge: relationship_id, adult participants, kind (courtship/lover/committed/consort), satisfaction/trust/chemistry, expectations/exclusivity, start/end and visibility/fact references.
- Union edge: union_id, consenting adult participants, commitment/ceremony status, recognition per jurisdiction/version, agreement/prenup and dates.
- Parentage edge: child_id, parent_id, biological/social/adoptive/guardian role, chronology/source.
- Household/care edge: residents/dependents/guardians, custody/support obligations.

Initial proposal is pairwise unions with multiple memberships: sharing a spouse never implies all spouses are romantically involved with one another. Freeze pairwise/group support explicitly. primary_relationship_id is a UI/legacy selection, not authority to erase other partners. Single legacy player.partner/marital_status becomes a bounded derived compatibility projection.

Archive deceased/ex people and edges. Divorce targets one union and never deletes children. Validate ancestry cycles/duplicates/impossible chronology; safely render marriage/in-law graph loops with visited IDs. Unknown parents, adoption, remarriage, step links and grandchildren are first-class tree cases.

Biological relatedness uses only biological ancestry and a documented pedigree/kinship algorithm. Adoption/step/in-law links alone add no genetic risk. Unknown ancestry carries uncertainty. Compute a bounded abstract game-health adjustment once for actual conception parents; persist sampled child outcome. No deterministic illness/disability for every related-parent child and no risk draw for adoption. This is fictional balancing, not a clinical estimate; name/religion/orientation do not enter the coefficient. Do not blame/stigmatize children. Freeze risk calibration with Claude.

## 3. Policy and acceptance (B)

Personal profiles: belief/tenet/culture IDs plus personal overrides, attraction/orientation, openness, jealousy and monogamy preference. Save-local catalogs are versioned and scoped; public faith tenets do not automatically equal private preferences.

Evaluator separates hard validity from soft score. Named bounded contributions: attraction, mutual history/chemistry, personal tenets/beliefs, culture, law risk, reputation, class, adult age gap, kinship and existing commitments. Freeze bounds/aggregation and refusal policy so penalties do not become hidden eligibility gates and score100 does not manufacture consent. Explain reasons according to what the player legitimately knows.

Law profile: simulated jurisdiction, union recognition/type policies and social/legal consequence/enforcement rules. Illegal/unrecognized marriage stays attemptable; label civil recognition separately from personal ceremony/commitment. Presets cannot lower18 or override consent. Editing rules updates current recognition, not historical facts or repeated automatic fines. Profile conversion/change uses typed commands, not arbitrary event set_flag.

Country policy is a save-local baseline+override/version; content updates cannot silently erase edits. Real country labels do not assert legal accuracy. Neutral defaults do not infer orientation/religion/culture from personal names or gender.

## 4. Reputation, secrets and observers (C)

Actual facts are separate from knowledge records: observer_id, fact_id, learned_turn/source and, optionally, confidence. Secret relationships specify authorized knowledge/visibility. Affairs mean violations of agreed expectations, not all plural relationships by default.

Discovery/reveal is a persisted event occurrence with bound witnesses/evidence and sampled outcomes. Deduplicate reputation/legal effects per fact/observer/jurisdiction/occurrence. Uninformed NPCs cannot react to a global secret merely because the engine stores it. Editor inspection is explicitly out-of-character and never propagates knowledge. Private data stays out of normal API/journal notifications unless the player knows; the editor's own view is separate.

Use typed reveal/disclose/forgive/end transitions. “Forgiveness” can repair trust without erasing history. Rumor/misidentification is optional later depth, not a prerequisite.

## 5. Event schema v3 proposal (C; A checks fit; Claude freezes)

Extend frozen v2.1 with:
- Explicit bindings: actor, target, relationship, union, spouse, lover, ex, coparent, guardian, child, grandchild, in_law and witness. Bind each role once to concrete person/edge IDs in the occurrence. Define biological/social qualifiers and selection predicates; never implicitly pick first spouse.
- Conditions: relationship/union state and expectations, recognition/policy/tenets, known facts/discovery, scoped reputation, attraction/modifier predicates, kinship category and dependent milestones. Bounded allowlisted all/any/not/query vocabulary; no eval or arbitrary Python paths.
- Typed domain effects: offer/respond/end a relationship/selected union, reveal fact, change scoped reputation/trust/chemistry, convert/profile-change, record parenting milestone. Effects call authoritative domain APIs; no raw salary/graph/consent setters.
- Audience/visibility: public/player-known/private event projection; structured participants with person_id, not prose parsing.
- Compatibility: v2 gain_partner/lose_partner/divorce/partner_death binds the intended legacy person/edge at offer creation. A resumed v2 choice cannot divorce a newly selected spouse. Unmappable stale context produces safe explicit cancellation, not random target selection.

Retain v2.1 manual/auto distinction, cost-as-gate-only, effects.money-as-single-posting, occurrence rates and once-per-offer sampling. Free exit for every manual choice. Adult requirement is an immutable engine tag/guard for romantic interaction kinds, not only conditions.min_age. A romantic event proposing min_age16 must fail schema validation, while runtime still checks actual participants. Auto events cannot create consent/forced unions.

Separate schema-file version, saved-pending dialect and policy revision. Keep loader support for v2 while adding v3; old saved outcomes stay stored and resolve only once. No second romance interpreter.

Before freeze show real v3 records for first date/refusal, second-spouse proposal, selected divorce, one-witness discovery, conversion reaction, wedding and nonromantic parenting request; invalid minor-target/unknown-role/missing-edge/$0-no-exit examples too.

## 6. Editor protocol and pending effects (B/A/C)

Revisioned allowlisted patch commands have explicit scope: person, personal tenets, save-local belief profile or jurisdiction. Preview affected people/relationships/costs; apply atomically saves and journals actual before/after. Never expose raw __dict__/arbitrary JSON paths. Protected age/DOB/chronology/person_id/consent/parentage/RNG/pending/ledger fields return typed errors. Money edits omitted initially unless a specific adjustment action is frozen.

Support editing while pending through a declared handler. A relevant edit atomically cancels/supersedes the affected relationship offer and resumes its stored safe continuation; keep consumed attempts/cooldowns and already-applied pre-effects. Unrelated edits leave samples intact. Freeze the dependency set with A/C. Never use modifier edits to resample offers repeatedly in one window.

Do not rewrite prior marriages, consent, discovery or birth-risk outcomes. Current modifiers/recognition change immediately; a new reconsideration event can be scheduled as a new occurrence without rewriting old history.

## 7. Money, parentage and lifecycle

Dates/weddings/honeymoons/holidays/prenups/support/fines use the frozen M2 phase/occurrence/account/leg/group ledger request. Transfer both modeled legs once; label external sources/sinks where counterpart finances are not modeled. No duplicated household wallet per spouse. Cash-only prenups/inheritance are explicit; property remains M5/M8.

Persist actual conception-parent/donor/gestational/social IDs as supported by domain model; don't infer parentage from current partner at birth after a breakup. Both participants in romantic/conception actions are protected adults. Recognition doesn't define parentage; adoption/co-parenting are viable independent routes.

Shared dependent aging/effects/support post once per child, not per spouse/household edge. A divorce cannot silently cancel custody/support. Participant death cancels stale offers and preserves other partners' completed phases; A's sole yearly order still applies.

## 8. Freeze/migration fixtures

A: two spouses, former spouse, adopted child, unknown parent, biological ancestry, public tree and old single-partner migration.
B: scoped editor preview/apply, illegal-but-attemptable proposal, rename, protected-field rejection, pending-policy edit.
C: v3 events and real v2 pending compatibility.
D: cross-contract validation and executable journeys.

Migrate M2 single partner into exactly one correct relationship/union; preserve person IDs, cash, pregnancy parentage, archives and pending references. Do not invent affairs/additional spouses/history. Save-once neutral profile defaults preserve later edits; no per-load random belief assignment. Existing completed events and stored pending dialects remain versioned. An old broad partner helper must not overwrite a new plural graph.
