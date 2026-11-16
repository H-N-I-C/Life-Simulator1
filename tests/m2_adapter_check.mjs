// Validates web/static/adapter.js normalization against the FROZEN
// docs/collab/M2/api-examples.json. Run: node tests/m2_adapter_check.mjs
import { readFileSync } from "node:fs";
import { fileURLToPath } from "node:url";
import { dirname, join } from "node:path";

const here = dirname(fileURLToPath(import.meta.url));
const root = join(here, "..");
const ex = JSON.parse(readFileSync(
  join(root, "docs/collab/M2/api-examples.json"), "utf8"));
const adapter = await import(join(root, "web/static/adapter.js"));

let failures = 0;
const ok = (cond, msg) => {
  console.log((cond ? "PASS " : "FAIL ") + msg);
  if (!cond) failures++;
};

// --- actions ---
const acts = ex.available_actions_example.map(adapter.normalizeAction);
const byId = Object.fromEntries(acts.map((a) => [a.id, a]));
ok(byId.age_up.category === "life", "age_up category life");
ok(byId.study.category === "education" && byId.study.enabled === true &&
   byId.study.limits.includes("once_per_year"), "study normalized");
ok(byId.adjust_hours.inputs[0].min === 20 &&
   byId.adjust_hours.inputs[0].max === 80 &&
   byId.adjust_hours.inputs[0].required === true,
   "adjust_hours bounded number input");
ok(byId.apply_university.inputs[0].type === "choice" &&
   byId.apply_university.inputs[0].choices.length === 3,
   "apply_university choice input");
const dis = adapter.normalizeAction(ex.disabled_action_example);
ok(dis.enabled === false && dis.reason === "already studied this year",
   "disabled action carries reason");

// --- decisions ---
const pd = adapter.normalizeDecision(
  ex.pending_multiphase_example.pending_decision);
ok(pd.kind === "partner_divorce" && pd.options.length === 2,
   "multiphase decision normalized");
ok(pd.options.every((o) => o.enabled && o.reason === ""),
   "options default enabled");
ok(pd.continuation &&
   pd.continuation.phase_id === "family_events" &&
   pd.continuation.item_cursor === 3,
   "continuation_after_resolve mapped");
const baby = adapter.normalizeDecision(
  ex.pending_with_inputs_example.pending_decision);
ok(baby.inputs.length === 0 && baby.options[0].inputs[0].type === "text" &&
   baby.options[0].inputs[0].min === 1 &&
   baby.options[0].inputs[0].max === 40,
   "baby naming bounded text input on option");

// --- state sections (assembled envelope) ---
const state = adapter.normalizeState({
  game_id: "g_x", revision: 5, status: "alive", player_id: "p1",
  people: [{ id: "p1", name: "A", role: "player", age: 30, alive: true,
             appearance: {} }],
  stats: {}, money: 0, country: "AU",
  available_actions: ex.available_actions_example,
  journal: [], pending_decision: null,
  education: ex.new_state_sections.education,
  work: ex.new_state_sections.work,
  health: ex.new_state_sections.health,
  family: ex.new_state_sections.family,
  finances: ex.new_state_sections.finances,
  generation: ex.new_state_sections.generation,
  turn: ex.pending_multiphase_example.turn,
});
ok(state.sections.education.university_years_remaining === 0 &&
   state.sections.education.loan.has_loan === false,
   "education section passthrough");
ok(state.sections.work.effective_annual_gross === 40000 &&
   state.sections.work.can_retire === false, "work section passthrough");
const fin = adapter.financeRows(state);
ok(fin.estimate.gross === 40000 && fin.estimate.net === 22200,
   "annual estimate fields");
ok(fin.ledger.length === 1 && fin.ledger[0].source_key.includes(":gross"),
   "ledger fields");
ok(fin.debts[0].kind === "student_loan" && fin.debts[0].remaining === 15000,
   "debts fields");
ok(fin.lastClosedTurn === "t_000042", "last_closed_turn field");
ok(state.turn.cursor.phase_id === "family_events" &&
   state.turn.pending === true, "turn cursor passthrough");

// ledger invariant from the frozen example:
// gross - tax - expenses == net  <=>  gross + (-tax) + (-expenses) + (-net) == 0
const legs = ex.work_income_turn_example.ledger_legs;
const sum = legs.filter((l) => l.source_key.includes(":work_income"))
  .reduce((a, l) => a + (l.source_key.endsWith(":net") ? -l.amount
                                                       : l.amount), 0);
ok(sum === 0, "frozen example: gross - tax - expenses == net exactly");

console.log(failures ? "FAILURES: " + failures : "ALL OK");
process.exit(failures ? 1 : 0);
