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
  const people = Array.isArray(raw.people) ? raw.people : [];
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
