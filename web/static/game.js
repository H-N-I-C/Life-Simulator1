import { renderAvatar, miniPersonChip } from "./common.js";
import { normalizeState, financeRows, journalPeople,
         ACTION_CATEGORIES } from "./adapter.js";

const $ = (id) => document.getElementById(id);
const resumeView = $("resumeView");
const gameView = $("gameView");
const legacyView = $("legacyView");
const toast = $("toast");
const tabbar = $("tabbar");

let state = null;          // normalized adapter state
let busy = false;
let activeScreen = "summary";

/* current game id: per-tab so refresh/new tab resumes the intended game */
function currentGameId() { return sessionStorage.getItem("game_id"); }
function setCurrentGameId(id) {
  if (id) sessionStorage.setItem("game_id", id);
  else sessionStorage.removeItem("game_id");
}

function showToast(message, isError) {
  toast.textContent = message;
  toast.classList.toggle("error", !!isError);
  toast.hidden = false;
  clearTimeout(showToast._t);
  showToast._t = setTimeout(() => { toast.hidden = true; }, 5000);
}

async function api(path, options = {}) {
  const res = await fetch(path, options);
  let body = null;
  try { body = await res.json(); } catch (e) { /* non-JSON */ }
  if (!res.ok) {
    const err = (body && body.error) || { code: "http_" + res.status,
                                          message: "Request failed" };
    err.status = res.status;
    throw err;
  }
  return body;
}

function post(path, data) {
  return api(path, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
}

function esc(s) {
  return String(s).replace(/[&<>"']/g, (c) => (
    { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));
}

/* ================= resume view ================= */

async function showResume() {
  state = null;
  resumeView.hidden = false;
  gameView.hidden = true;
  legacyView.hidden = true;
  tabbar.hidden = true;
  const list = $("lifeList");
  list.innerHTML = "<p>Loading…</p>";
  try {
    const data = await api("/api/games");
    if (!data.games.length) {
      list.innerHTML = "<p>No lives yet. Start one!</p>";
      return;
    }
    list.innerHTML = "";
    for (const g of data.games) {
      const row = document.createElement("div");
      row.className = "chip";
      const who = document.createElement("div");
      who.className = "who";
      who.innerHTML = `<div class="name">${esc(g.name)} — age ${g.age}` +
        (g.status === "dead" ? " (deceased)" : "") + `</div>
        <div class="role">${g.status === "dead" ? "Completed life"
                                                : "Living"}</div>`;
      row.appendChild(who);
      const btn = document.createElement("button");
      btn.textContent = "Resume";
      btn.addEventListener("click", () => {
        setCurrentGameId(g.game_id);
        loadGame();
      });
      row.appendChild(btn);
      if (g.player && g.player.appearance) {
        row.insertBefore(miniPersonChip(
          { name: "", age: g.player.age, alive: g.player.alive !== false,
            appearance: g.player.appearance }, 40), row.firstChild);
        row.querySelector(".chip.mini span:last-child").remove();
      }
      list.appendChild(row);
    }
  } catch (err) {
    list.innerHTML = "";
    showToast(err.message, true);
  }
}

/* ================= screens ================= */

const SCREENS = ["summary", "education", "work", "relationships",
                 "activities", "finances"];

function selectScreen(name) {
  activeScreen = name;
  for (const s of SCREENS) $("screen-" + s).hidden = s !== name;
  for (const b of tabbar.querySelectorAll("button")) {
    if (b.dataset.screen === name) b.setAttribute("aria-current", "page");
    else b.removeAttribute("aria-current");
  }
}

for (const b of tabbar.querySelectorAll("button")) {
  b.addEventListener("click", () => selectScreen(b.dataset.screen));
}

/* ================= rendering ================= */

function render() {
  const s = state;
  const player = s.player;
  resumeView.hidden = true;
  legacyView.hidden = true;
  gameView.hidden = false;
  tabbar.hidden = false;

  // --- summary card ---
  const pc = $("playerCard");
  const dead = s.status === "dead";
  pc.innerHTML = `
    <div class="player-head">
      <div class="avatar big" id="playerAvatar"></div>
      <div>
        <div class="name">${esc(player.name)}</div>
        <div class="role">Age ${player.age} · ${esc(s.country)}` +
        (s.generation > 1 ? ` · Generation ${s.generation}` : "") + `</div>
        <div class="role cash">$${s.money.toLocaleString()}</div>
      </div>
    </div>
    <div class="stats">
      ${Object.entries(s.stats).map(([k, v]) => `
        <div class="stat">
          <span>${esc(k)}</span>
          <div class="track"><div class="fill ${v >= 60 ? "good"
            : v >= 35 ? "mid" : "low"}"
            style="width:${Math.max(0, Math.min(100, v))}%"></div></div>
          <b>${v}</b>
        </div>`).join("")}
    </div>
    ${dead ? `<p class="dead-note">This life has ended at age ${player.age}.
      The record remains.</p>` : ""}`;
  renderAvatar($("playerAvatar"), player.appearance || {}, player.age, 168);

  // --- turn progress (visible multi-phase annual flow) ---
  const tc = $("turnCard");
  if (s.turn && !s.pending) {
    tc.hidden = false;
    const phases = s.turn.phases || [];
    const done = phases.filter((p) => p.done).length;
    tc.innerHTML = `<h2>This year</h2>
      <p>${esc(s.turn.label || "Resolving the year…")} — phase ${done} of
         ${phases.length || "?"}</p>
      <div class="row">${phases.map((p) =>
        `<span class="phase ${p.done ? "done" : ""}">${esc(p.label || p.id)}</span>`
      ).join("")}</div>`;
  } else {
    tc.hidden = true;
  }

  renderTimeline();
  renderSectionCards();
  renderFamily();
  renderActions();
  if (s.pending) {
    // Pending decisions block everything; if the dialog is not open (e.g.
    // right after reload) reopen it so the choice is never stranded.
    if (modal.hidden) {
      const pill = $("decisionPill");
      if (!pill || pill.hidden) openDecisionModal();
    }
  } else {
    closeModal();
    const pill = $("decisionPill");
    if (pill) pill.hidden = true;
  }
  const op = loadPendingOp();
  if (op) showPendingPill(op);
}

function renderTimeline() {
  const j = $("journalList");
  j.innerHTML = "";
  for (const e of state.journal) {
    const li = document.createElement("li");
    li.innerHTML = `<span class="jage">${e.age}</span> <span class="jtext">` +
      `${esc(e.text)}</span>`;
    const who = journalPeople(e, state.peopleById);
    if (who.length) {
      const chips = document.createElement("span");
      chips.className = "jchips";
      for (const person of who) chips.appendChild(miniPersonChip(person, 22));
      li.appendChild(chips);
    }
    j.appendChild(li);
  }
}

function kv(rows) {
  return rows.filter((r) => r[1] !== null && r[1] !== undefined &&
                           r[1] !== "")
    .map(([k, v]) => `<div class="kv"><span>${esc(k)}</span><b>${esc(v)}</b></div>`)
    .join("");
}

function renderSectionCards() {
  const sec = state.sections;

  // Education
  const ed = $("educationCard");
  if (sec.education) {
    const e = sec.education;
    ed.innerHTML = `<h2>Education</h2><div class="kvlist">${kv([
      ["Stage", e.stage], ["Enrolled", e.enrolled ? "Yes" : "No"],
      ["Grades", e.grades != null ? e.grades : null],
      ["University years", e.university_years],
      ["Student loan", e.loan_status],
    ])}</div>`;
  } else {
    ed.innerHTML = `<h2>Education</h2><p class="muted">No education data yet.</p>`;
  }

  // Work
  const wk = $("workCard");
  if (sec.work) {
    const w = sec.work;
    wk.innerHTML = `<h2>Work</h2><div class="kvlist">${kv([
      ["Status", w.state || (w.employed ? "Employed" : "Not employed")],
      ["Job", w.job_label], ["Hours", w.hours],
      ["Salary", w.salary != null ? "$" + w.salary.toLocaleString() : null],
      ["Annual gross", w.effective_annual_gross != null
        ? "$" + w.effective_annual_gross.toLocaleString() : null],
      ["Pension", w.pension != null ? "$" + w.pension.toLocaleString()
                                   : null],
      ["Performance", w.performance], ["Stress", w.stress],
    ])}</div>`;
  } else {
    wk.innerHTML = `<h2>Work</h2><p class="muted">No work data yet.</p>`;
  }

  // Health
  const hc = $("healthCard");
  if (sec.health) {
    const h = sec.health;
    const illnesses = (h.illnesses || []).map((i) =>
      `<li>${esc(i.name || i)} — ${esc(i.status || "")}</li>`).join("");
    hc.innerHTML = `<h2>Health</h2>` +
      (illnesses ? `<ul>${illnesses}</ul>`
                 : `<p class="muted">No active illnesses.</p>`);
  } else {
    hc.innerHTML = `<p class="muted">No health data yet.</p>`;
  }

  // Activities placeholder copy
  const ac = $("activitiesCard");
  ac.innerHTML = `<h2>Activities</h2>` +
    (sec.activities ? "" : `<p class="muted">Activities appear as they
     become available.</p>`);

  // Finances
  const fin = financeRows(state);
  const fc = $("financesCard");
  const ledgerRows = fin.ledger.slice(-12).reverse().map((r) =>
    `<tr><td>${esc(r.age ?? "")}</td><td>${esc(r.category || "")}</td>
     <td>${esc(r.description || "")}</td>
     <td class="${r.amount < 0 ? "neg" : "pos"}">` +
     `${r.amount < 0 ? "−" : "+"}$${Math.abs(r.amount).toLocaleString()}</td>
     <td>$${(r.balance_after ?? "").toLocaleString ?
          (r.balance_after ?? "").toLocaleString() : ""}</td></tr>`).join("");
  fc.innerHTML = `<h2>Finances</h2>
    <div class="kvlist">${kv([
      ["Balance", "$" + (fin.balance ?? state.money).toLocaleString()],
      ...(fin.debts || []).map((d) =>
        [esc(d.label || d.category || "Debt") + " owed",
         "$" + (d.amount ?? d.balance).toLocaleString()]),
    ])}</div>
    ${fin.estimate ? `<h2>Annual estimate <span class="muted">(forecast, not
      posted)</span></h2><div class="kvlist">${kv([
        ["Gross", fin.estimate.gross != null
          ? "$" + fin.estimate.gross.toLocaleString() : null],
        ["Tax", fin.estimate.tax != null
          ? "$" + fin.estimate.tax.toLocaleString() : null],
        ["Living costs", fin.estimate.expenses != null
          ? "$" + fin.estimate.expenses.toLocaleString() : null],
        ["Debt service", fin.estimate.debt_service != null
          ? "$" + fin.estimate.debt_service.toLocaleString() : null],
        ["Pension", fin.estimate.pension != null
          ? "$" + fin.estimate.pension.toLocaleString() : null],
        ["Net", fin.estimate.net != null
          ? "$" + fin.estimate.net.toLocaleString() : null],
      ])}</div>` : ""}
    ${ledgerRows ? `<h2>Ledger</h2><table class="ledger"><thead>
      <tr><th>Age</th><th>Category</th><th>Description</th><th>Amount</th>
      <th>Balance</th></tr></thead><tbody>${ledgerRows}</tbody></table>` : ""}`;
}

function renderFamily() {
  const fam = $("familyList");
  fam.innerHTML = "";
  for (const p of state.people.filter((p) => p.id !== state.playerId)) {
    const chip = document.createElement("div");
    chip.className = "chip";
    const av = document.createElement("div");
    av.className = "avatar";
    const who = document.createElement("div");
    who.className = "who";
    who.innerHTML = `<div class="name">${esc(p.name)}` +
      (p.alive ? "" : " †") + `</div>
      <div class="role">${esc(p.role)} · ${p.age}` +
      (p.relationship != null ? ` · relationship ${p.relationship}%` : "") +
      `</div>`;
    chip.appendChild(av);
    chip.appendChild(who);
    fam.appendChild(chip);
    renderAvatar(av, p.appearance, p.age, 96);
  }
}

/* ---------- actions ---------- */

function costLabel(a) {
  const bits = [];
  for (const c of a.knownCosts || []) {
    if (c.amount != null) {
      bits.push((c.amount < 0 ? "Cost: $" + (-c.amount).toLocaleString()
                              : "+$" + c.amount.toLocaleString()));
    } else if (c.label) {
      bits.push(esc(c.label));
    }
  }
  return bits.join(" · ");
}

function renderActions() {
  // grouped category lists
  for (const cat of ACTION_CATEGORIES) {
    const box = $("actions-" + cat);
    box.innerHTML = "";
    for (const a of state.actionsByCategory[cat] || []) {
      box.appendChild(actionButton(a));
    }
    if (!(state.actionsByCategory[cat] || []).length) {
      box.innerHTML = `<p class="muted">Nothing available right now.</p>`;
    }
  }
  // general (age_up etc.)
  const acts = $("actionList");
  acts.innerHTML = "";
  for (const a of state.actionsByCategory.other) {
    const b = actionButton(a);
    if (a.id === "age_up") b.classList.add("primary");
    acts.appendChild(b);
  }
  if (!state.actionsByCategory.other.length) {
    acts.innerHTML = "<p class=\"muted\">No actions available.</p>";
  }
}

function actionButton(a) {
  const b = document.createElement("button");
  const cost = costLabel(a);
  b.innerHTML = esc(a.label) + (cost ? `<span class="cost">${cost}</span>` : "");
  b.disabled = busy || !!state.pending;
  b.addEventListener("click", () => onAction(a));
  return b;
}

function onAction(a) {
  if (a.inputs && a.inputs.length) openActionModal(a);
  else sendAction(a.id, {}, a.targetId);
}

/* ---------- modals ---------- */

const modal = $("modal");

function closeModal() {
  modal.hidden = true;
  $("modalForm").innerHTML = "";
  $("modalOptions").innerHTML = "";
}

$("modalCancel").addEventListener("click", closeModal);

function buildInputRow(input, doc) {
  const wrap = document.createElement("label");
  wrap.className = "field";
  const id = "in-" + input.id;
  let control;
  if (input.type === "choice" && input.choices) {
    control = document.createElement("select");
    for (const c of input.choices) {
      const o = document.createElement("option");
      o.value = typeof c === "object" ? c.id : c;
      o.textContent = typeof c === "object" ? c.label : c;
      control.appendChild(o);
    }
  } else if (input.type === "boolean") {
    control = document.createElement("input");
    control.type = "checkbox";
  } else {
    control = document.createElement("input");
    control.type = input.type === "number" ? "number" : "text";
    if (input.type === "number") {
      if (input.min != null) control.min = input.min;
      if (input.max != null) control.max = input.max;
    } else {
      control.maxLength = 60;
    }
  }
  control.id = id;
  control.required = !!input.required;
  wrap.appendChild(document.createTextNode(input.label +
    (input.required ? " *" : "")));
  wrap.appendChild(control);
  doc[input.id] = { control, input };
  return wrap;
}

function collectInputs(doc) {
  const payload = {};
  for (const [key, { control, input }] of Object.entries(doc)) {
    let v;
    if (input.type === "boolean") v = control.checked;
    else if (input.type === "number") {
      if (control.value === "") {
        if (input.required) throw new Error(input.label + " is required");
        continue;
      }
      v = Number(control.value);
      if (!Number.isFinite(v)) throw new Error(input.label + " must be a number");
      if (input.min != null && v < input.min) {
        throw new Error(input.label + " must be at least " + input.min);
      }
      if (input.max != null && v > input.max) {
        throw new Error(input.label + " must be at most " + input.max);
      }
    } else {
      v = control.value.trim();
      if (input.required && !v) throw new Error(input.label + " is required");
    }
    payload[key] = v;
  }
  return payload;
}

function openActionModal(a) {
  $("modalTitle").textContent = a.label;
  $("modalText").textContent = "";
  $("modalPeople").innerHTML = "";
  $("modalOptions").innerHTML = "";
  const form = $("modalForm");
  form.innerHTML = "";
  const doc = {};
  for (const input of a.inputs) form.appendChild(buildInputRow(input, doc));
  const submit = document.createElement("button");
  submit.className = "primary";
  const cost = costLabel(a);
  submit.textContent = "Confirm" + (cost ? ` (${cost})` : "");
  submit.addEventListener("click", () => {
    try {
      const payload = collectInputs(doc);
      closeModal();
      sendAction(a.id, payload, a.targetId);
    } catch (err) {
      showToast(err.message, true);
    }
  });
  form.appendChild(submit);
  modal.hidden = false;
  const first = form.querySelector("input, select");
  if (first) first.focus();
}

let lastFocusedBeforeModal = null;

function trapModalFocus(e) {
  if (modal.hidden || e.key !== "Tab") return;
  const focusables = modal.querySelectorAll(
    "button:not([disabled]), input, select, [tabindex]:not([tabindex='-1'])");
  if (!focusables.length) return;
  const first = focusables[0];
  const last = focusables[focusables.length - 1];
  if (e.shiftKey && document.activeElement === first) {
    e.preventDefault();
    last.focus();
  } else if (!e.shiftKey && document.activeElement === last) {
    e.preventDefault();
    first.focus();
  }
}

document.addEventListener("keydown", trapModalFocus);
document.addEventListener("keydown", (e) => {
  if (e.key === "Escape" && !modal.hidden && state && state.pending) {
    collapseDecisionModal();
  }
});

function openDecisionModal() {
  const d = state.pending;
  const pill = $("decisionPill");
  if (pill) pill.hidden = true;
  lastFocusedBeforeModal = document.activeElement;
  $("modalTitle").textContent = d.title;
  $("modalText").textContent = d.text;
  const pp = $("modalPeople");
  pp.innerHTML = "";
  for (const pid of d.personIds) {
    const p = state.peopleById[pid];
    if (!p) continue;
    pp.appendChild(miniPersonChip(p, 26));
  }
  const form = $("modalForm");
  form.innerHTML = "";
  const doc = {};
  for (const input of d.inputs) form.appendChild(buildInputRow(input, doc));
  const box = $("modalOptions");
  box.innerHTML = "";
  for (const opt of d.options) {
    const b = document.createElement("button");
    b.innerHTML = esc(opt.label) +
      (opt.knownEffects && opt.knownEffects.length
        ? `<span class="cost">${opt.knownEffects.map(esc).join(" · ")}</span>`
        : "");
    b.disabled = busy || !opt.enabled;
    if (!opt.enabled && opt.reason) b.title = opt.reason;
    b.addEventListener("click", () => {
      try {
        const inputs = collectInputs({ ...doc,
                                       ...(opt._doc || {}) });
        closeModal();
        resolveDecision(d.id, opt.id, inputs);
      } catch (err) {
        showToast(err.message, true);
      }
    });
    box.appendChild(b);
  }
  // disabled options show their reason as a note
  const reasons = d.options.filter((o) => !o.enabled && o.reason)
    .map((o) => `${o.label}: ${o.reason}`);
  if (reasons.length) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = reasons.join("  ·  ");
    box.appendChild(note);
  }
  // The pending decision blocks all other gameplay, so the dialog gets a
  // complete modal lifecycle: initial focus on the first enabled choice,
  // Tab/Shift-Tab trapped inside, Escape/Cancel collapse it to a visible
  // reopening pill instead of losing it.
  const later = document.createElement("button");
  later.textContent = "Decide later";
  later.addEventListener("click", collapseDecisionModal);
  box.appendChild(later);
  modal.hidden = false;
  $("modalCancel").hidden = true;
  const firstEnabled = box.querySelector("button:not([disabled])");
  if (firstEnabled) firstEnabled.focus();
}

function collapseDecisionModal() {
  modal.hidden = true;
  $("modalCancel").hidden = false;
  if (lastFocusedBeforeModal && lastFocusedBeforeModal.focus) {
    lastFocusedBeforeModal.focus();
  }
  // Always leave a return path.
  let pill = $("decisionPill");
  if (!pill) {
    pill = document.createElement("div");
    pill.id = "decisionPill";
    pill.className = "pending-pill decision";
    document.querySelector("main").prepend(pill);
  }
  pill.hidden = false;
  pill.innerHTML = "";
  const label = document.createElement("span");
  label.textContent = "A decision is waiting for you.";
  const show = document.createElement("button");
  show.className = "small primary";
  show.textContent = "Show";
  show.addEventListener("click", () => {
    pill.hidden = true;
    openDecisionModal();
  });
  pill.appendChild(label);
  pill.appendChild(show);
}

/* ---------- commands: retry-stable pending operations ----------
 * Each attempted command persists {game_id, expected_revision, action,
 * payload, request_id} per tab BEFORE sending. On uncertain failure the
 * same operation is offered for retry; the server checks its replay ledger
 * before the revision check, so a committed-but-lost command returns its
 * original response instead of reapplying. */
const PENDING_OP_KEY = "pending_game_op";

function loadPendingOp() {
  try { return JSON.parse(sessionStorage.getItem(PENDING_OP_KEY) || "null"); }
  catch (e) { return null; }
}

function savePendingOp(op) {
  sessionStorage.setItem(PENDING_OP_KEY, JSON.stringify(op));
}

function clearPendingOp() {
  sessionStorage.removeItem(PENDING_OP_KEY);
  const pill = $("pendingPill");
  if (pill) pill.hidden = true;
}

function showPendingPill(op) {
  let pill = $("pendingPill");
  if (!pill) {
    pill = document.createElement("div");
    pill.id = "pendingPill";
    pill.className = "pending-pill";
    document.querySelector("main").prepend(pill);
  }
  pill.hidden = false;
  pill.innerHTML = "";
  const label = document.createElement("span");
  label.textContent = `Uncertain result for "${op.action}" — it may have ` +
    `succeeded. `;
  const retry = document.createElement("button");
  retry.className = "small";
  retry.textContent = "Check result (safe retry)";
  retry.addEventListener("click", () => retryPendingOp());
  const dismiss = document.createElement("button");
  dismiss.className = "small";
  dismiss.textContent = "Dismiss";
  dismiss.addEventListener("click", clearPendingOp);
  pill.appendChild(label);
  pill.appendChild(retry);
  pill.appendChild(dismiss);
}

function startOperation(actionId, payload) {
  if (busy) return;
  busy = true;
  const op = { game_id: state.gameId, expected_revision: state.revision,
               action: actionId, payload: payload || {},
               request_id: crypto.randomUUID() };
  savePendingOp(op);
  sendOperation(op);
}

function retryPendingOp() {
  const op = loadPendingOp();
  if (!op || busy) return;
  busy = true;
  sendOperation(op);
}

function sendOperation(op) {
  render();
  post(`/api/games/${encodeURIComponent(op.game_id)}/actions`, {
    expected_revision: op.expected_revision,
    request_id: op.request_id,
    action: op.action,
    payload: op.payload,
  }).then((raw) => {
    busy = false;
    clearPendingOp();          // definitive success
    state = normalizeState(raw);
    render();
  }).catch((err) => {
    busy = false;
    if (err.status === 409 && err.code === "stale_revision") {
      // Could be a genuinely stale op or a lost-response retry racing a
      // committed state. Reload authoritative state; if our op was already
      // applied the server replay ledger answered before the revision
      // check, so a stale_revision here means it was NOT applied — keep the
      // operation and tell the user, never auto-reissue a new mutation.
      showPendingPill(loadPendingOp() || op);
      showToast("State changed elsewhere — refreshed. Retry from the pill.",
                true);
      loadGameKeepOp();
      return;
    }
    if (err.status === 409 && err.code === "pending_decision") {
      showToast("A decision is waiting for you first.", true);
      clearPendingOp();
      loadGame();
      return;
    }
    if (err.status && err.status < 500 && err.code !== "conflict" &&
        err.code !== "replay_conflict") {
      // Definitive validation rejection: retrying identical data cannot
      // succeed; reconcile deliberately.
      clearPendingOp();
      showToast(err.message, true);
      render();
      return;
    }
    // Transport failure or unexpected error: outcome uncertain — keep the
    // operation and offer the safe retry pill.
    showPendingPill(loadPendingOp() || op);
    showToast(err.message || "Network error — result uncertain.", true);
    render();
  });
}

async function loadGameKeepOp() {
  const gid = currentGameId();
  if (!gid) { showResume(); return; }
  try {
    const raw = await api("/api/games/" + encodeURIComponent(gid));
    state = normalizeState(raw);
    if (state.status === "dead" && state.continuation) renderLegacy();
    else { render(); selectScreen(activeScreen); }
  } catch (err) {
    showToast(err.message, true);
  }
}

function sendAction(actionId, payload, targetId) {
  const body = {};
  if (targetId) body.target_id = targetId;
  Object.assign(body, payload);
  startOperation(actionId, body);
}

function resolveDecision(decisionId, optionId, inputs) {
  startOperation("resolve_decision",
                 { decision_id: decisionId, option_id: optionId, inputs });
}

/* ---------- legacy / continuation ---------- */

function renderLegacy() {
  const s = state;
  gameView.hidden = true;
  resumeView.hidden = true;
  tabbar.hidden = true;
  legacyView.hidden = false;
  const cont = s.continuation;
  const eligible = (cont && cont.eligible_children) || [];
  legacyView.innerHTML = `
    <div class="card">
      <h2>A completed life</h2>
      <p>${esc(s.player.name)} lived to ${s.player.age}. The journal and
      ledger stay available in the archive.</p>
      <p class="muted">Inheritance at this stage is cash only; property and
      family-tree systems arrive in a later milestone.</p>
      <div class="row">
        <button id="viewArchive">View archived journal</button>
        <button id="legacyBack">All lives</button>
      </div>
    </div>
    <div class="card" id="continueCard">
      <h2>Continue as a child</h2>
      ${eligible.length ? "" : `<p class="muted">No eligible children.</p>`}
      <div id="childList"></div>
    </div>`;
  const list = $("childList");
  for (const c of eligible) {
    const row = document.createElement("div");
    row.className = "chip";
    const av = document.createElement("div");
    av.className = "avatar";
    const who = document.createElement("div");
    who.className = "who";
    who.innerHTML = `<div class="name">${esc(c.name)}</div>
      <div class="role">Age ${c.age} · inherits ` +
      `$${(c.inheritance ?? 0).toLocaleString()}</div>`;
    row.appendChild(av);
    row.appendChild(who);
    const btn = document.createElement("button");
    btn.textContent = "Continue";
    btn.addEventListener("click", () => continueAs(c.id));
    row.appendChild(btn);
    list.appendChild(row);
    renderAvatar(av, c.appearance, c.age, 96);
  }
  $("legacyBack").addEventListener("click", () => {
    setCurrentGameId(null);
    showResume();
  });
  $("viewArchive").addEventListener("click", () => {
    gameView.hidden = false;
    legacyView.hidden = true;
    tabbar.hidden = false;
    selectScreen("summary");
    render();
  });
}

function continueAs(childId) {
  if (busy) return;
  busy = true;
  const requestId = crypto.randomUUID();
  attempt("continue_as_child", requestId, { target_id: childId }, 0);
}

/* ---------- loading ---------- */

function consumeDeepLink() {
  // Consume ?game= exactly once at initial navigation, then clean the URL
  // so sessionStorage is the single resume authority (Resume/All lives
  // update storage, never fight a stale query param).
  const q = new URLSearchParams(location.search).get("game");
  if (q) {
    if (!sessionStorage.getItem("game_id")) sessionStorage.setItem("game_id", q);
    history.replaceState(null, "", "/game");
  }
}

async function loadGame() {
  consumeDeepLink();
  const gid = currentGameId();
  if (!gid) { showResume(); return; }
  try {
    const raw = await api("/api/games/" + encodeURIComponent(gid));
    state = normalizeState(raw);
    if (state.status === "dead" && state.continuation) {
      renderLegacy();
    } else {
      render();
      selectScreen(activeScreen);
    }
  } catch (err) {
    if (err.status === 404) {
      setCurrentGameId(null);
      showResume();
    }
    showToast(err.message, true);
  }
}

$("backToLives").addEventListener("click", () => {
  setCurrentGameId(null);
  showResume();
});

loadGame();
