/* M2 API adapter — the ONLY module that knows the wire shape.
 *
 * The backend contract is docs/collab/M2/02-contract.md. Every screen
 * consumes the normalized shapes from this module; if A's fixtures drift
 * from the frozen contract, only this file changes.
 *
 * Normalization is deliberately tolerant: M1-shaped states (no
 * education/work/... sections, plain action entries) still produce sane
 * empty structures so the UI degrades gracefully across A's rollout.
 */
"use strict";

export const ACTION_CATEGORIES = [
  "relationships", "family", "education", "work", "activities", "health",
  "finances",
];
// actions whose category renders in the General bucket (not a tab)
export const GENERAL_CATEGORIES = ["life", "other"];

export function normalizeState(raw) {
  const rawPeople = Array.isArray(raw.people) ? raw.people : [];
  // M4: the social edge (kind/closeness/circles/memory) travels on the
  // social group refs; attach it to the canonical people entries (copies,
  // people-array objects never mutated) so the Person page can render it.
  const socialEdges = {};
  if (raw.social && Array.isArray(raw.social.groups)) {
    for (const g of raw.social.groups) {
      for (const ref of g.people || []) {
        if (ref && typeof ref.id === "string" &&
            ref.social && !socialEdges[ref.id]) {
          socialEdges[ref.id] = ref.social;
        }
      }
    }
  }
  const people = rawPeople.map((p) =>
    p && !p.social && socialEdges[p.id]
      ? { ...p, social: socialEdges[p.id] } : p);
  const peopleById = {};
  for (const p of people) peopleById[p.id] = p;

  const sections = {
    education: raw.education || null,
    work: raw.work || null,
    health: raw.health || null,
    family: raw.family || null,
    finances: raw.finances || null,
  };

  const actions = (raw.available_actions || []).map(normalizeAction);
  const actionsByCategory = {};
  for (const cat of ACTION_CATEGORIES) actionsByCategory[cat] = [];
  actionsByCategory.other = [];
  for (const a of actions) {
    (actionsByCategory[a.category] || actionsByCategory.other).push(a);
  }

  return {
    raw,
    gameId: raw.game_id,
    revision: raw.revision,
    status: raw.status,
    playerId: raw.player_id,
    people,
    peopleById,
    player: peopleById[raw.player_id] || null,
    stats: raw.stats || {},
    money: typeof raw.money === "number" ? raw.money : 0,
    country: raw.country || "",
    journal: Array.isArray(raw.journal) ? raw.journal : [],
    sections,
    actions,
    actionsByCategory,
    pending: raw.pending_decision ? normalizeDecision(raw.pending_decision)
                                  : null,
    turn: raw.turn || null,
    // §Decision outcome (feel-pass F5): actual deltas of the most recent
    // resolve_decision, for C's outcome popup; null until the first resolve.
    lastDecisionOutcome: raw.last_decision_outcome || null,
    // M3 feature projections (additive): careers (A), skills (C) and the
    // education program view (B). The legacy M2 education section stays
    // under sections.education — the M3 programs view travels under
    // `education_programs` to avoid the key collision.
    careers: raw.careers || null,
    skills: raw.skills || null,
    educationFeature: raw.education_programs || null,
    // M4: group/invite refs are deliberately minimal on the wire; the
    // adapter fills appearance/relationship/role from the people entries
    // (same source person.js/event cards use) so feature modules render
    // real avatars and the family relationship bar.
    social: raw.social ? normalizeSocial(raw.social, peopleById) : null,
    generation: raw.generation ?? 1,
    completedLives: Array.isArray(raw.completed_lives) ? raw.completed_lives
                                                       : [],
    // frozen M2c shape: dead states carry succession.eligible +
    // generation_next (older 'continuation' block still tolerated)
    succession: raw.succession || null,
    continuation: raw.continuation || null,
  };
}

export function normalizeAction(a) {
  return {
    id: a.id,
    label: a.label,
    // M1 entries have no category; the UI buckets them sensibly.
    category: a.category ||
      (a.id === "spend_time" ? "relationships" :
       a.id === "age_up" ? "life" : "other"),
    targetId: a.target_id ?? null,
    targetLabel: a.target_label || null,
    inputs: (a.inputs || []).map(normalizeInput),
    knownCosts: a.known_costs || a.knownCosts || [],
    limits: a.limits || [],
    // Frozen M2 shape: ineligible/used actions are projected with
    // enabled:false and a precise reason.
    enabled: a.enabled !== false,
    reason: a.reason || "",
  };
}

export function normalizeInput(i) {
  return {
    id: i.id,
    type: i.type,               // "number" | "text" | "choice"
    label: i.label,
    required: !!i.required,
    min: i.min ?? null,
    max: i.max ?? null,
    choices: i.choices || null,
  };
}

export function normalizeDecision(d) {
  return {
    id: d.id,
    kind: d.kind || null,
    title: d.title,
    text: d.text,
    personIds: d.person_ids || [],
    inputs: (d.inputs || []).map(normalizeInput),
    options: (d.options || []).map(normalizeOption),
    continuation: d.continuation_after_resolve || null,
    // v2 §Choice (A, frozen v1): pure display metadata, null when unmapped.
    displayCategory: d.display_category || null,
  };
}

export function normalizeOption(o) {
  return {
    id: o.id,
    label: o.label,
    // Contract: pending options carry enabled + reason. Absent means enabled
    // (M1 semantics).
    enabled: o.enabled !== false,
    reason: o.reason || "",
    knownEffects: o.known_effects || o.knownEffects || [],
    knownCosts: o.known_costs || o.knownCosts || [],
    inputs: (o.inputs || []).map(normalizeInput),
  };
}

/* ---- journal entries referencing people (incl. deceased) -------------- */

export function journalPeople(entry, peopleById) {
  const out = [];
  for (const id of entry.person_ids || []) {
    if (peopleById[id]) out.push(peopleById[id]);
  }
  return out;
}

/* ---- M4 social --------------------------------------------------------- */

/* Group/invite person refs arrive minimal (id/name/age[/alive][/social]).
 * Fill appearance/relationship/role from the canonical people entries —
 * the same source person.js and the event cards use — so the social
 * module renders real portraits and the family relationship bar. Ref
 * fields always win; enrichment only fills what the wire omitted. */
export function normalizeSocial(rawSocial, peopleById) {
  const fill = (ref) => {
    if (!ref || typeof ref.id !== "string") return ref;
    const full = peopleById[ref.id];
    if (!full) return ref;
    return {
      ...full,
      ...ref,
      appearance: ref.appearance !== undefined ? ref.appearance
                                               : full.appearance,
      relationship: ref.relationship !== undefined ? ref.relationship
                                                   : full.relationship,
      role: ref.role !== undefined ? ref.role : full.role,
    };
  };
  const social = { ...rawSocial };
  if (Array.isArray(social.groups)) {
    social.groups = social.groups.map((g) => ({
      ...g,
      people: Array.isArray(g.people) ? g.people.map(fill) : g.people,
    }));
  }
  if (Array.isArray(social.invites)) {
    social.invites = social.invites.map((inv) =>
      inv && inv.host ? { ...inv, host: fill(inv.host) } : inv);
  }
  return social;
}

/* ---- finances projection ---------------------------------------------- */

/* Returns rows suitable for a ledger table; empty when the service has not
   published finances yet (M1). */
export function financeRows(state) {
  const f = state.sections.finances;
  if (!f) return { estimate: null, ledger: [], debts: [] };
  return {
    estimate: f.annual_estimate || null,
    ledger: f.ledger || [],
    debts: f.debts || [],
    lastClosedTurn: f.last_closed_turn ?? null,
    balance: f.balance ?? state.money,
  };
}
