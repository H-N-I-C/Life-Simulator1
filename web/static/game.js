import { renderAvatar, miniPersonChip, setErrorRetryHandler,
         hideCrashCard } from "./common.js";
import { normalizeState, financeRows, journalPeople,
         ACTION_CATEGORIES, GENERAL_CATEGORIES } from "./adapter.js";

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


const pageLoading = $("pageLoading");
function setPageLoading(on) { pageLoading.hidden = !on; }

function showToast(message, isError) {
  toast.textContent = message;
  toast.classList.toggle("error", !!isError);
  toast.hidden = false;
  clearTimeout(showToast._t);
  showToast._t = setTimeout(() => { toast.hidden = true; }, 5000);
}

async function api(path, options = {}) {
  if (navigator.onLine === false) {
    const err = new Error(
      "You are offline — reconnect, then retry. Your save is safe.");
    err.status = 0;
    err.code = "offline";
    throw err;
  }
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
  setPageLoading(true);
  resumeView.hidden = false;
  gameView.hidden = true;
  legacyView.hidden = true;
  tabbar.hidden = true;
  const list = $("lifeList");
  list.innerHTML = "<p>Loading…</p>";
  try {
    const data = await api("/api/games");
    setPageLoading(false);
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
    setPageLoading(false);
    list.innerHTML = "";
    const card = document.createElement("div");
    card.className = "card load-error";
    const msg = document.createElement("p");
    msg.innerHTML = "<b>Couldn't load your lives.</b>";
    const detail = document.createElement("p");
    detail.className = "muted";
    detail.textContent = (err && err.message) || "Unknown error";
    const retry = document.createElement("button");
    retry.textContent = "Retry";
    retry.addEventListener("click", showResume);
    card.appendChild(msg);
    card.appendChild(detail);
    card.appendChild(retry);
    list.appendChild(card);
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
  setPageLoading(false);
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

  // --- turn progress (frozen cursor shape) ---
  const tc = $("turnCard");
  if (s.turn && (s.turn.pending || s.pending)) {
    tc.hidden = false;
    const cursor = s.turn.cursor || {};
    tc.innerHTML = `<h2>This year <span class="muted">(${esc(
        s.turn.turn_id || "")})</span></h2>
      <p>${s.pending
        ? "A choice is needed before the year can continue."
        : "Resolving the year…"} Current phase:
        <b>${esc(cursor.phase_id || "year_start")}</b></p>`;
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

function firstPerson(text) {
  const player = state.player;
  if (!player) return text;
  const names = [player.name, player.name.split(" ")[0]].filter(Boolean)
    .sort((a, b) => b.length - a.length);
  for (const n of names) {
    if (text.startsWith(n + " ")) return "I " + text.slice(n.length + 1);
    if (text === n) return "I";
  }
  if (text.startsWith("You are ")) return "I am " + text.slice(8);
  if (text.startsWith("You have ")) return "I have " + text.slice(9);
  if (text.startsWith("Your ")) return "My " + text.slice(5);
  if (text.startsWith("You ")) return "I " + text.slice(4);
  return text;
}

const TURNED_RE = /^I turned \d+\.?$/;

// never expose schema tokens to the player (engine resolves person
// roles; this is a last-line strip in case one leaks)
function stripTokens(text) {
  return String(text).replace(/\{[a-z_]+:[^}]*\}/gi, "someone")
    .replace(/\s{2,}/g, " ").trim();
}

// illness names read naturally lower-case inside sentences ("I recovered
// from the common cold"); keep a fallback list plus the live illness set
const KNOWN_CONDITIONS = ["Common Cold", "Flu", "Cancer", "Cold", "Food " +
  "Poisoning", "Migraine", "Pneumonia", "Chicken Pox", "Measles"];

function sentenceCaseConditions(text) {
  const names = new Set(KNOWN_CONDITIONS);
  const health = state.sections.health;
  for (const i of (health && health.illnesses) || []) {
    names.add(typeof i === "string" ? i : (i && i.name) || "");
  }
  for (const name of names) {
    if (!name || name === name.toLowerCase()) continue;
    const lower = name.toLowerCase();
    let idx = text.indexOf(name);
    while (idx !== -1) {
      if (idx > 0) {  // not sentence-initial
        text = text.slice(0, idx) + lower + text.slice(idx + name.length);
        idx = text.indexOf(name, idx + lower.length);
      } else {
        idx = text.indexOf(name, idx + name.length);
      }
    }
  }
  return text;
}

function renderTimeline() {
  // group entries per age into cards; drop "I turned N" filler (the card
  // header already gives the age); collapse runs of quiet years; first
  // person; chips only for OTHER people involved
  const j = $("journalList");
  j.innerHTML = "";
  const groups = [];
  for (const e of state.journal) {
    const text = sentenceCaseConditions(firstPerson(e.text));
    if (TURNED_RE.test(text)) continue;  // age header covers this
    const last = groups[groups.length - 1];
    if (last && last.age === e.age) last.entries.push({ e, text });
    else groups.push({ age: e.age, entries: [{ e, text }] });
  }
  const frag = document.createDocumentFragment();
  let quietFrom = null, quietTo = null;
  const flushQuiet = () => {
    if (quietFrom === null) return;
    const li = document.createElement("li");
    li.className = "jquiet";
    li.textContent = quietFrom === quietTo
      ? "Age " + quietFrom + ": a quiet year"
      : "Ages " + quietFrom + "-" + quietTo + ": quiet years";
    frag.appendChild(li);
    quietFrom = quietTo = null;
  };
  let prevAge = null;
  for (const g of groups) {
    if (prevAge !== null && g.age > prevAge + 1) {
      quietFrom = prevAge + 1;
      quietTo = g.age - 1;
    }
    flushQuiet();
    const card = document.createElement("li");
    card.className = "jcard";
    const head = document.createElement("div");
    head.className = "jcard-head";
    head.textContent = "Age " + g.age;
    card.appendChild(head);
    for (const { e, text } of g.entries) {
      const line = document.createElement("div");
      line.className = "jline";
      line.textContent = stripTokens(text);
      const others = journalPeople(e, state.peopleById)
        .filter((p) => p.id !== state.playerId);
      if (others.length) {
        const chips = document.createElement("span");
        chips.className = "jchips";
        for (const person of others) {
          chips.appendChild(miniPersonChip(person, 22));
        }
        line.appendChild(chips);
      }
      card.appendChild(line);
    }
    frag.appendChild(card);
    prevAge = g.age;
  }
  flushQuiet();
  if (!groups.length && !state.journal.length) {
    const li = document.createElement("li");
    li.className = "jquiet";
    li.textContent = "Nothing has happened yet — age up to live your " +
      "first year.";
    frag.appendChild(li);
  } else if (!groups.length) {
    const li = document.createElement("li");
    li.className = "jquiet";
    li.textContent = "A quiet life so far.";
    frag.appendChild(li);
  }
  j.appendChild(frag);
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
      ["Stage", e.stage],
      ["Enrolled", e.enrolled ? "Yes" : "No"],
      ["Grades", e.grades != null ? e.grades : null],
      ["Dropped out", e.dropped_out ? "Yes" : null],
      ["High school done", e.graduated_high_school ? "Yes" : null],
      ["University years left", e.university_years_remaining],
      ...(e.loan && e.loan.has_loan
          ? [["Loan remaining", "$" + e.loan.remaining.toLocaleString()]]
          : []),
    ])}</div>`;
  } else {
    ed.innerHTML = `<h2>Education</h2><p class="muted">No education data yet.</p>`;
  }

  // Work
  const wk = $("workCard");
  if (sec.work) {
    const w = sec.work;
    wk.innerHTML = `<h2>Work</h2><div class="kvlist">${kv([
      ["Status", w.retired ? "Retired"
        : w.employed ? "Employed" : "Not employed"],
      ["Job", w.employed ? w.job_label : null],
      ["Hours", w.employed ? w.hours : null],
      ["Salary", w.employed && w.salary != null
        ? "$" + w.salary.toLocaleString() + "/yr" : null],
      ["Annual gross", w.effective_annual_gross != null
        ? "$" + w.effective_annual_gross.toLocaleString() : null],
      ["Pension", w.pension != null && w.pension > 0
        ? "$" + w.pension.toLocaleString() : null],
      ["Performance", w.employed ? w.performance : null],
      ["Stress", w.employed ? w.stress : null],
      ["Years worked", w.years_worked != null ? w.years_worked : null],
      ["Can retire", w.can_retire ? "Yes" : null],
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
    const treatments = (h.treatments_available || []).map((t) => esc(t));
    hc.innerHTML = `<h2>Health</h2>` +
      (illnesses ? `<ul>${illnesses}</ul>`
                 : `<p class="muted">No active illnesses.</p>`) +
      (treatments.length
        ? `<p class="muted">Treatments available: ${treatments.join(", ")}</p>`
        : "");
  } else {
    hc.innerHTML = `<p class="muted">No health data yet.</p>`;
  }


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
        [esc(d.label || d.kind || d.category || "Debt") + " owed",
         "$" + (d.remaining ?? d.amount ?? d.balance ?? 0).toLocaleString()]),
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

function renderFamilySection() {
  const box = $("familySectionCard");
  if (!box) return;
  const f = state.sections.family;
  if (!f) { box.innerHTML = ""; box.hidden = true; return; }
  box.hidden = false;
  const group = (title, ids) => {
    const people = (ids || []).map((id) => state.peopleById[id])
      .filter(Boolean);
    if (!people.length) return "";
    return `<h3>${esc(title)}</h3><div class="row">` +
      people.map((p) => `<span class="chip mini"
        data-person="${esc(p.id)}">${esc(p.name)}${p.alive ? "" : " †"}
        · ${p.age}</span>`).join("") + `</div>`;
  };
  box.innerHTML = group("Parents", f.parents) +
    group("Partner", f.partner ? [f.partner] : []) +
    group("Children", f.children) +
    group("Siblings", f.siblings);
  // portraits for the chips (avatars render asynchronously)
  box.querySelectorAll(".chip.mini[data-person]").forEach((chip) => {
    const person = state.peopleById[chip.dataset.person];
    if (!person) return;
    const av = document.createElement("span");
    av.className = "avatar mini";
    av.style.width = "26px";
    av.style.height = "26px";
    chip.insertBefore(av, chip.firstChild);
    renderAvatar(av, person.appearance || {}, person.age, 52);
  });
}

function renderFamily() {
  renderFamilySection();
  const fam = $("familyList");
  fam.innerHTML = "";
  const others = state.people.filter((p) => p.id !== state.playerId);
  if (!others.length) {
    fam.innerHTML = '<p class="muted">No family or relationships yet.</p>';
    return;
  }
  for (const p of others) {
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
  // general (life/orphan categories, e.g. age_up)
  const acts = $("actionList");
  acts.innerHTML = "";
  const general = state.actions.filter(
    (a) => GENERAL_CATEGORIES.includes(a.category) ||
          !ACTION_CATEGORIES.includes(a.category));
  for (const a of general) {
    const b = actionButton(a);
    if (a.id === "age_up") b.classList.add("primary");
    acts.appendChild(b);
  }
  if (!general.length) {
    acts.innerHTML = "<p class=\"muted\">No actions available.</p>";
  }
}

function actionButton(a) {
  const b = document.createElement("button");
  const cost = costLabel(a);
  const who = a.targetLabel ? ` — ${esc(a.targetLabel)}` : "";
  b.innerHTML = esc(a.label) + who +
    (cost ? `<span class="cost">${cost}</span>` : "");
  // Frozen shape: ineligible actions arrive enabled:false with a reason.
  b.disabled = busy || !!state.pending || !a.enabled;
  if (!a.enabled && a.reason) {
    b.title = a.reason;
    const r = document.createElement("span");
    r.className = "cost";
    r.textContent = a.reason;
    b.appendChild(r);
  }
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
    } else if (input.max != null) {
      control.maxLength = input.max;
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
      if (v && input.min != null && v.length < input.min) {
        throw new Error(input.label + " must be at least " + input.min +
                        " characters");
      }
      if (v && input.max != null && v.length > input.max) {
        throw new Error(input.label + " must be at most " + input.max +
                        " characters");
      }
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
      // Duplicate-submit guard: one in-flight command per modal. The modal
      // closes on success; on a validation error the button stays and is
      // re-enabled.
      submit.disabled = true;
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

const DANGER_KINDS = new Set(["surrender_final"]);

function openDecisionModal() {
  const d = state.pending;
  const pill = $("decisionPill");
  if (pill) pill.hidden = true;
  lastFocusedBeforeModal = document.activeElement;
  $("modalTitle").textContent = stripTokens(d.title);
  // fatal/terminal choices get distinct danger styling
  document.querySelector(".modal-box").classList.toggle(
    "danger", DANGER_KINDS.has(d.kind));
  $("modalText").textContent = stripTokens(d.text);
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
        // Duplicate-submit guard: freeze every option while the one
        // command is in flight (busy also gates it globally).
        for (const x of box.querySelectorAll("button")) x.disabled = true;
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
  if (!gid) { setPageLoading(false); showResume(); return; }
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
  setPageLoading(false);
  const s = state;
  gameView.hidden = true;
  resumeView.hidden = true;
  tabbar.hidden = true;
  legacyView.hidden = false;
  const eligible = (s.succession && s.succession.eligible) || [];
  const generationNext = s.succession && s.succession.generation_next;
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
      ${generationNext ? `<p class="muted">Generation ${esc(
          generationNext)}</p>` : ""}
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
      <div class="role">Age ${esc(String(c.age))}</div>`;
    row.appendChild(av);
    row.appendChild(who);
    const btn = document.createElement("button");
    btn.textContent = "Continue";
    const pid = c.person_id || c.id;
    btn.addEventListener("click", () => {
      // Duplicate-submit guard: freeze all Continue buttons; the retry-
      // stable operation machinery makes the single attempt safe.
      for (const x of list.querySelectorAll("button")) x.disabled = true;
      continueAs(pid);
    });
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
  // Same retry-stable path as every other command: persisted op, replay
  // ledger, safe retry. (The old ad-hoc helper was not defined and would
  // have crashed here.)
  startOperation("continue_as", { target_id: childId });
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
  setPageLoading(true);
  const gid = currentGameId();
  if (!gid) { showResume(); return; }
  try {
    const raw = await api("/api/games/" + encodeURIComponent(gid));
    state = normalizeState(raw);
    if (state.status === "dead" &&
        (state.succession || state.continuation)) {
      renderLegacy();
    } else {
      render();
      selectScreen(activeScreen);
    }
  } catch (err) {
    if (err.status === 404) {
      setCurrentGameId(null);
      showResume();
      return;
    }
    showFatalLoadError(err);
  }
}

/* Full-view error state when the current game cannot be loaded: the page
 * is never left half-broken, and both ways forward are explicit. */
function showFatalLoadError(err) {
  setPageLoading(false);
  state = null;
  gameView.hidden = true;
  legacyView.hidden = true;
  tabbar.hidden = true;
  resumeView.hidden = false;
  const list = $("lifeList");
  list.innerHTML = "";
  const card = document.createElement("div");
  card.className = "card load-error";
  const msg = document.createElement("p");
  msg.innerHTML = "<b>Couldn't load this life.</b> Your save is safe on " +
    "the server.";
  const detail = document.createElement("p");
  detail.className = "muted";
  detail.textContent = (err && err.message) || "Unknown error";
  const row = document.createElement("div");
  row.className = "row";
  const retry = document.createElement("button");
  retry.className = "primary";
  retry.textContent = "Retry";
  retry.addEventListener("click", loadGame);
  const all = document.createElement("button");
  all.textContent = "All lives";
  all.addEventListener("click", () => {
    setCurrentGameId(null);
    showResume();
  });
  row.appendChild(retry);
  row.appendChild(all);
  card.appendChild(msg);
  card.appendChild(detail);
  card.appendChild(row);
  list.appendChild(card);
}

$("backToLives").addEventListener("click", () => {
  setCurrentGameId(null);
  showResume();
});

/* M2.5 shell hooks shared by the editor and the feature screens (Kimi B).
 * After a successful out-of-band transaction (editor apply, wedding or
 * graph command) the shell adopts the server's new revision by re-reading
 * authoritative state and re-rendering — no page reload, focus stays
 * where the feature module put it. */
async function refreshShell() {
  const gid = currentGameId();
  if (!gid) return null;
  const raw = await api("/api/games/" + encodeURIComponent(gid));
  state = normalizeState(raw);
  render();
  selectScreen(activeScreen);
  return state;
}
const shellHooks = {
  getState: () => state,
  refresh: refreshShell,
  // Feature screens drive game commands through the shell's retry-stable
  // operation machinery (persisted op + replay ledger), never ad-hoc.
  sendAction: (actionId, payload, targetId) =>
    sendAction(actionId, payload || {}, targetId),
  resolveDecision: (decisionId, optionId, inputs) =>
    resolveDecision(decisionId, optionId, inputs || {}),
};

/* M2.5 in-play character editor (relationship_policy feature, B). Lazy
 * import so the game page pays nothing when the panel stays closed. */
let editorModule = null;
$("editorToggle").addEventListener("click", async () => {
  try {
    if (!editorModule) {
      editorModule = await import(
        "/static/features/relationship_policy/editor.js");
    }
    editorModule.toggleEditor(state, shellHooks);
  } catch (err) {
    showToast("Editor failed to load.", true);
  }
});

/* M2.5 family tree (family_graph feature, A's module; B owns the shell
 * hook). Lazy-import and mount into a toggleable card. */
let familyTreeModule = null;
let familyTreeCard = null;
$("familyTreeToggle").addEventListener("click", async () => {
  try {
    if (!familyTreeCard) {
      familyTreeCard = document.createElement("section");
      familyTreeCard.className = "card";
      familyTreeCard.id = "familyTreeCard";
      familyTreeCard.hidden = true;
      $("actionList").parentElement.appendChild(familyTreeCard);
    }
    if (!familyTreeModule) {
      familyTreeModule = await import(
        "/static/features/family_graph/family_tree.js");
    }
    familyTreeCard.hidden = !familyTreeCard.hidden;
    if (!familyTreeCard.hidden) {
      await familyTreeModule.mountFamilyTree(familyTreeCard,
                                             state.gameId);
    }
  } catch (err) {
    showToast("Family tree failed to load.", true);
  }
});

/* M2.5 dating & weddings screens (Kimi B's relationship_policy feature
 * module). Lazy-import and mount into a toggleable card, same pattern as
 * the editor and the family tree. */
let m25Module = null;
let m25Card = null;
$("m25Toggle").addEventListener("click", async () => {
  try {
    if (!m25Card) {
      m25Card = document.createElement("section");
      m25Card.className = "card";
      m25Card.id = "m25Card";
      m25Card.hidden = true;
      $("actionList").parentElement.appendChild(m25Card);
    }
    if (!m25Module) {
      m25Module = await import(
        "/static/features/relationship_policy/m25_screens.js");
    }
    m25Card.hidden = !m25Card.hidden;
    if (!m25Card.hidden) {
      await m25Module.mountM25(m25Card, shellHooks);
    }
  } catch (err) {
    showToast("Dating & weddings failed to load.", true);
  }
});

/* Global error boundary "Retry": re-render from the server's current
 * state — always safe, never a mutation. Falls back to the lives list. */
setErrorRetryHandler(() => {
  hideCrashCard();
  if (currentGameId()) loadGame();
  else showResume();
});

loadGame();
