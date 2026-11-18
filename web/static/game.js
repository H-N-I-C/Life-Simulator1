import { renderAvatar, miniPersonChip, setErrorRetryHandler,
         hideCrashCard } from "./common.js";
import { normalizeState, financeRows, journalPeople,
         ACTION_CATEGORIES, GENERAL_CATEGORIES } from "./adapter.js";
import { buildInputRow, collectInputs } from "./v2/inputs.js";
import { mountChoiceSheet } from "./v2/choice.js";

const $ = (id) => document.getElementById(id);
const resumeView = $("resumeView");
const gameView = $("gameView");
const legacyView = $("legacyView");
const toast = $("toast");

let state = null;          // normalized adapter state
let busy = false;
let activeScreen = "home";
let v2Shell = null;        // v2 navigation chrome (contracts.md §Shell)

/* ---------- v2 navigation history (USER BUG: visible + browser Back) -----
 * Every user navigation pushes a tagged history entry; browser Back/Forward
 * (popstate) applies it. In-app Back goes through history too, so both stay
 * in lock-step. Entries: {v2:1, screen} | {person:id} | {panel:which} |
 * {lives:1}. Scroll positions ride inside entries (captured on push via
 * replaceState) so Back restores the previous screen's scroll. */
let v2Depth = history.state?.v2 ? (history.state.v2Index || 1) : 0;
let v2Restoring = 0;
history.scrollRestoration = "manual";

function currentV2Scroll() {
  const sc = document.querySelector(".v2-scroll");
  return sc ? sc.scrollTop : 0;
}
function v2SetScroll(v) {
  const sc = document.querySelector(".v2-scroll");
  if (sc) sc.scrollTop = v || 0;
}
function v2Push(s) {
  try {
    // Existing tabs may still contain the earlier unindexed base entry.
    // Repair the entry we leave as well as the new destination.
    const current = history.state?.v2
      ? Object.assign({}, history.state, {
          game: history.state.game || (state ? state.gameId : currentGameId()),
          v2Index: history.state.v2Index || v2Depth || 1,
        })
      : { v2: 1, game: state ? state.gameId : currentGameId(),
          v2Index: 1, screen: activeScreen };
    const next = Object.assign({ v2: 1, game: state ? state.gameId : null }, s);
    const sameRoute = ["screen", "person", "panel", "lives", "game"]
      .every((key) => current[key] === next[key]);
    if (current.v2 && sameRoute &&
        (!("personId" in s) || current.personId === next.personId)) return;
    history.replaceState(Object.assign({}, current,
                                       { scroll: currentV2Scroll() }), "");
    next.v2Index = (current.v2Index || v2Depth || 1) + 1;
    history.pushState(next, "");
    v2Depth = next.v2Index;
  } catch (e) { /* storage-less context: in-app Back still works */ }
}
function v2Navigate(name) {
  v2Push({ screen: name });
  if (v2Shell) v2Shell.closeAux();
  closeV2Panels();
  selectScreen(name);
}
window.addEventListener("popstate", () => {
  const s = history.state;
  if (!s || !s.v2) return;
  // Forward moves toward a larger index; do not treat every pop as Back.
  v2Depth = s.v2Index || 1;
  v2Apply(s).catch(() => showToast("Couldn't restore this view.", true));
});
async function v2Apply(s) {
  if (s.game && (!state || state.gameId !== s.game) && !s.lives) {
    v2Restoring++;
    try {
      setCurrentGameId(s.game);
      await loadGame();
    } finally { v2Restoring--; }
    if (!state || state.gameId !== s.game ||
        history.state?.v2Index !== s.v2Index) return;
  }
  if (s.screen !== undefined) {
    if (state) { resumeView.hidden = true; gameView.hidden = false; }
    if (v2Shell) v2Shell.closeAux();
    closeV2Panels();
    selectScreen(s.screen);
    v2SetScroll(s.scroll);
  } else if (s.person) {
    if (state) { resumeView.hidden = true; gameView.hidden = false; }
    if (v2Shell) v2Shell.closeAux();
    closeV2Panels();
    selectScreen("people");
    if (v2Shell) await v2Shell.navigate("person", { personId: s.person });
  } else if (s.panel) {
    if (state) { resumeView.hidden = true; gameView.hidden = false; }
    if (v2Shell) v2Shell.closeAux();
    closeV2Panels();
    selectScreen("activities");
    await openV2PanelDispatch(s.panel, s.personId);
  } else if (s.lives) {
    setCurrentGameId(null);
    await showResume();
  }
  if (v2Shell) v2Shell.sync();
}

/* ---------- feature panels (editor / family tree / dating & weddings) ----- */
function isV2PanelOpen() {
  const ep = $("editorPanel");
  return Boolean((ep && !ep.hidden) ||
                 (familyTreeCard && !familyTreeCard.hidden) ||
                 (m25Card && !m25Card.hidden));
}
function closeV2Panels(except) {
  const ep = $("editorPanel");
  if (ep && except !== "editor") ep.hidden = true;
  if (familyTreeCard && except !== "familyTree") familyTreeCard.hidden = true;
  if (m25Card && except !== "m25") m25Card.hidden = true;
}
function openV2Panel(which) {
  v2Push({ panel: which });
  return Promise.resolve(openV2PanelDispatch(which)).then(() => {
    if (v2Shell) v2Shell.sync();
  }).catch(() => showToast("Couldn't open this panel.", true));
}

/* In-app Back for panels: prefer history (keeps browser Back in sync);
 * fall back to a plain close when there is no entry to pop. */
function closeTopPanel() {
  if (v2Depth > 1) { history.back(); return; }
  closeV2Panels();
  if (v2Shell) v2Shell.sync();
}

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
  // USER BUG FIX: a visible Back out of "All lives" when we came from a
  // game (the lives history entry knows which one).
  const rb = $("resumeBackBtn");
  if (rb) {
    rb.hidden = v2Depth <= 1;
    rb.onclick = () => { if (v2Depth > 1) history.back(); };
  }
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
        v2Push({ screen: "home", game: g.game_id, scroll: 0 });
        activeScreen = "home";
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

/* v2 destinations (frozen contracts.md §Shell). Every legacy panel lives
 * inside exactly one of these sections in game.html. */
const SCREENS = ["home", "job", "assets", "people", "activities"];

function selectScreen(name) {
  activeScreen = name;
  for (const s of SCREENS) $("screen-" + s).hidden = s !== name;
  if (v2Shell) v2Shell.sync();
}

/* ================= rendering ================= */

function render() {
  setPageLoading(false);
  const s = state;
  resumeView.hidden = true;
  legacyView.hidden = true;
  gameView.hidden = false;

  // --- Home module (v2): central Age control + real stats sheet ---
  if (v2Home) v2Home.update(s);

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
    // right after reload) reopen it so the choice is never stranded. If it
    // IS open but shows a DIFFERENT decision id (a resolve just advanced
    // the chain, e.g. surrender_confirm → surrender_final), re-render the
    // sheet — a stale sheet must never sit over a new server decision.
    const box = document.querySelector(".modal-box");
    const openId = box && box._v2SheetDecisionId;
    if (modal.hidden) {
      const pill = $("decisionPill");
      if (!pill || pill.hidden) openDecisionModal();
    } else if (openId && openId !== s.pending.id) {
      openDecisionModal();
    }
  } else {
    closeModal();
    const pill = $("decisionPill");
    if (pill) pill.hidden = true;
  }
  const op = loadPendingOp();
  if (op) showPendingPill(op);
  if (v2Shell) v2Shell.sync();
  else {
    // A page can load straight into the legacy view (dead life) where
    // renderGame never ran — mount the chrome lazily so a continuation
    // (or archive view) still lands in the v2 shell.
    ensureV2Shell().then(async () => {
      await ensureV2Home();
      if (v2Shell) v2Shell.sync();
    }).catch(() => {});
  }
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

/* Stage 3 (frozen §Shell): an inline person chip that opens the Person
 * overlay for that exact ID — used by the timeline and the choice sheet.
 * Keyboard reachable (Enter/Space), textContent-only. */
function linkedPersonChip(person, size = 22) {
  const chip = miniPersonChip(person, size);
  chip.classList.add("v2-person-link");
  chip.setAttribute("role", "button");
  chip.tabIndex = 0;
  chip.setAttribute("aria-label", person.name + " — view person");
  const open = () =>
    shellHooks.navigate("person", { personId: person.id });
  chip.addEventListener("click", open);
  chip.addEventListener("keydown", (e) => {
    if (e.key === "Enter" || e.key === " ") {
      e.preventDefault();
      open();
    }
  });
  return chip;
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
  // Frozen §Home: the card list renders NEWEST FIRST; the underlying
  // chronological journal is never mutated.
  const frag = document.createDocumentFragment();
  const items = [];
  let quietFrom = null, quietTo = null;
  const flushQuiet = () => {
    if (quietFrom === null) return;
    const li = document.createElement("li");
    li.className = "jquiet";
    li.textContent = quietFrom === quietTo
      ? "Age " + quietFrom + ": a quiet year"
      : "Ages " + quietFrom + "-" + quietTo + ": quiet years";
    items.push(li);
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
          chips.appendChild(linkedPersonChip(person));
        }
        line.appendChild(chips);
      }
      card.appendChild(line);
    }
    items.push(card);
    prevAge = g.age;
  }
  flushQuiet();
  if (!groups.length && !state.journal.length) {
    const li = document.createElement("li");
    li.className = "jquiet";
    li.textContent = "Nothing has happened yet — age up to live your " +
      "first year.";
    items.push(li);
  } else if (!groups.length) {
    const li = document.createElement("li");
    li.className = "jquiet";
    li.textContent = "A quiet life so far.";
    items.push(li);
  }
  for (let i = items.length - 1; i >= 0; i--) frag.appendChild(items[i]);
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
    // v2: the roster opens the Person detail overlay for this exact ID.
    chip.classList.add("v2-person-link");
    chip.setAttribute("role", "button");
    chip.tabIndex = 0;
    chip.setAttribute("aria-label", p.name + " — view person");
    const open = () =>
      shellHooks.navigate("person", { personId: p.id });
    chip.addEventListener("click", open);
    chip.addEventListener("keydown", (e) => {
      if (e.key === "Enter" || e.key === " ") {
        e.preventDefault();
        open();
      }
    });
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
  else sendAction(a.id, {}, a.targetId).catch(() => {});
}

/* ---------- modals ---------- */

const modal = $("modal");

function closeModal() {
  modal.hidden = true;
  const box = document.querySelector(".modal-box");
  if (box && box._v2Sheet) {
    box._v2Sheet.unmount();
    delete box._v2Sheet;
    delete box._v2SheetDecisionId;
  } else if (box) {
    for (const n of [...box.children]) {
      if (n.id !== "modalCancel") n.remove();
    }
  }
  const cancel = $("modalCancel");
  if (cancel) cancel.hidden = false;
}

$("modalCancel").addEventListener("click", closeModal);

function openActionModal(a) {
  const box = document.querySelector(".modal-box");
  for (const n of [...box.children]) {
    if (n.id !== "modalCancel") n.remove();
  }
  const title = document.createElement("h2");
  title.id = "v2SheetTitle";
  title.textContent = a.label;
  box.appendChild(title);
  const form = document.createElement("form");
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
      sendAction(a.id, payload, a.targetId).catch(() => {});
    } catch (err) {
      showToast(err.message, true);
    }
  });
  form.appendChild(submit);
  box.appendChild(form);
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
/* Escape on v2 surfaces where it is SAFE: it never discards a pending
 * server decision (the sheet branch above wins while a decision is open). */
document.addEventListener("keydown", (e) => {
  if (e.key !== "Escape" || !modal.hidden) return;
  if (v2Shell) v2Shell.escape();
});

function openDecisionModal() {
  const d = state.pending;
  const pill = $("decisionPill");
  if (pill) pill.hidden = true;
  lastFocusedBeforeModal = document.activeElement;
  const box = document.querySelector(".modal-box");
  const people = [];
  for (const pid of d.personIds) {
    const p = state.peopleById[pid];
    if (p) people.push(p);
  }
  // v2 Choice sheet (contracts.md §Choice): presentation only — resolution
  // stays on the retry-stable op machinery below. The sheet never invents
  // options, reasons, or effects; collapse never discards the decision.
  const sheet = mountChoiceSheet(box, {
    decision: { ...d, title: stripTokens(d.title || ""),
                text: stripTokens(d.text || "") },
    people,
    playerAge: state.player ? state.player.age : null,
    onResolve: (optId, inputs) => {
      closeModal();
      resolveDecision(d.id, optId, inputs).catch(() => {});
    },
    onCollapse: collapseDecisionModal,
    onError: (m) => showToast(m, true),
    onPersonClick: (pid) =>
      shellHooks.navigate("person", { personId: pid }),
  });
  box._v2Sheet = sheet;
  box._v2SheetDecisionId = d.id;
  modal.hidden = false;
  $("modalCancel").hidden = true;
  if (sheet.firstEnabled) sheet.firstEnabled.focus();
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
  if (busy) {
    return Promise.reject(Object.assign(
      new Error("Another action is still in flight"), { code: "busy" }));
  }
  busy = true;
  const op = { game_id: state.gameId, expected_revision: state.revision,
               action: actionId, payload: payload || {},
               request_id: crypto.randomUUID() };
  savePendingOp(op);
  return sendOperation(op);
}

function retryPendingOp() {
  const op = loadPendingOp();
  if (!op || busy) return Promise.resolve(null);
  busy = true;
  // sendOperation surfaces errors via the pill/toast; swallow the rejection
  // here so the click handler never trips the global error boundary.
  return sendOperation(op).catch(() => {});
}

function sendOperation(op) {
  render();
  return post(`/api/games/${encodeURIComponent(op.game_id)}/actions`, {
    expected_revision: op.expected_revision,
    request_id: op.request_id,
    action: op.action,
    payload: op.payload,
  }).then((raw) => {
    busy = false;
    clearPendingOp();          // definitive success
    state = normalizeState(raw);
    render();
    return state;              // frozen §Shell: resolve with fresh state
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
      throw err;               // definitive: rejected per frozen §Shell
    }
    if (err.status === 409 && err.code === "pending_decision") {
      showToast("A decision is waiting for you first.", true);
      clearPendingOp();
      loadGame();
      throw err;               // definitive: rejected per frozen §Shell
    }
    if (err.status && err.status < 500 && err.code !== "conflict" &&
        err.code !== "replay_conflict") {
      // Definitive validation rejection: retrying identical data cannot
      // succeed; reconcile deliberately.
      clearPendingOp();
      showToast(err.message, true);
      render();
      throw err;               // definitive: rejected per frozen §Shell
    }
    // Transport failure or unexpected error: outcome uncertain — keep the
    // operation, offer the safe retry pill, and settle as null (the pill
    // owns the outcome; this dispatch itself did not fail definitively).
    showPendingPill(loadPendingOp() || op);
    showToast(err.message || "Network error — result uncertain.", true);
    render();
    return null;
  });
}

let v2Home = null;
async function ensureV2Home() {
  if (v2Home) return v2Home;
  const mod = await import("./v2/home.js");
  v2Home = mod.mountHome($("v2HomeMount"), shellHooks);
  return v2Home;
}

async function renderGame() {
  await ensureV2Shell();
  await ensureV2Home();
  render();
  selectScreen(activeScreen);
  // Seed the history stack once so Back/Forward (and the header Back) have
  // a base entry and in-app Back can never eject the player from the page.
  if (!history.state || !history.state.v2) {
    try {
      history.replaceState({ v2: 1, v2Index: 1, game: state.gameId,
                             screen: activeScreen, scroll: 0 }, "");
      v2Depth = 1;
    } catch (e) { /* history unavailable (test hosts) */ }
  }
  if (history.state?.v2) {
    v2Depth = history.state.v2Index || 1;
    if (!v2Restoring) await v2Apply(history.state);
  }
}

async function loadGameKeepOp() {
  const gid = currentGameId();
  if (!gid) { setPageLoading(false); showResume(); return; }
  try {
    const raw = await api("/api/games/" + encodeURIComponent(gid));
    state = normalizeState(raw);
    if (state.status === "dead" && state.continuation) renderLegacy();
    else { await renderGame(); }
  } catch (err) {
    showToast(err.message, true);
  }
}

function sendAction(actionId, payload, targetId) {
  const body = {};
  if (targetId) body.target_id = targetId;
  Object.assign(body, payload);
  return startOperation(actionId, body);
}

function resolveDecision(decisionId, optionId, inputs) {
  return startOperation("resolve_decision",
                        { decision_id: decisionId, option_id: optionId,
                          inputs });
}

/* ---------- legacy / continuation ---------- */

function renderLegacy() {
  setPageLoading(false);
  const s = state;
  gameView.hidden = true;
  resumeView.hidden = true;
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
    selectScreen("home");
    render();
  });
}

function continueAs(childId) {
  // Same retry-stable path as every other command: persisted op, replay
  // ledger, safe retry. (The old ad-hoc helper was not defined and would
  // have crashed here.)
  startOperation("continue_as", { target_id: childId }).catch(() => {});
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
      await renderGame();
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
  // USER BUG FIX: "All lives" is a place, not an exit — Back returns to the
  // game (the history entry remembers which one).
  v2Push({ lives: 1, game: currentGameId() });
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
  // v2 navigation (frozen contracts.md §Shell).
  navigate: (screen, params) => {
    if (screen === "person" && params && params.personId) {
      v2Push({ person: params.personId });
    }
    v2Shell && v2Shell.navigate(screen, params);
  },
  // USER BUG FIX: header Back for plain screens; the shell handles menu /
  // overlay / panels itself. History-driven so browser Back matches.
  onNavBack: () => {
    if (v2Depth > 1) { history.back(); return; }
    if (activeScreen !== "home") selectScreen("home");
  },
  onOverlayBack: () => {
    if (v2Depth > 1) { history.back(); return; }
    if (v2Shell) v2Shell.backFromPerson();
  },
};

/* v2 shell chrome (Kimi B, redesign-v2): header + tab bar + More routes +
 * person overlay host, per the frozen §Shell contract. Chrome only — game.js
 * keeps owning state, panel rendering and the command machinery. Secondary
 * routes reuse the existing toggles so every M2.5 surface stays reachable. */
async function openEditorFromShell(personId, fromHistory) {
  // Frozen §Shell/§Editor navigation: the editor's owning surface is the
  // Activities screen (General card). More → Editor must reveal it and open
  // idempotently; an exact personId pre-selects that person (never toggles
  // an open panel shut, never fabricates a selection).
  if (!fromHistory) v2Push({ panel: "editor", ...(personId ? { personId } : {}) });
  closeV2Panels("editor");
  selectScreen("activities");
  try {
    if (!editorModule) {
      editorModule = await import(
        "/static/features/relationship_policy/editor.js");
    }
    if (typeof editorModule.openEditor === "function") {
      await editorModule.openEditor(state, shellHooks, personId);
    } else {
      if (!editorModule.isEditorOpen()) {
        editorModule.toggleEditor(state, shellHooks);
      }
    }
    if (v2Shell) v2Shell.sync();
  } catch (err) {
    showToast("Editor failed to load.", true);
  }
}

function openV2PanelDispatch(which, personId) {
  // Restoring any panel must not push another navigation entry.
  if (which === "editor") return openEditorFromShell(personId, true);
  if (which === "familyTree") return openFamilyTreePanel();
  if (which === "m25") return openM25Panel();
  return Promise.resolve();
}

async function ensureV2Shell() {
  if (v2Shell) return v2Shell;
  const mod = await import("./v2/shell.js");
  v2Shell = mod.mountShell(gameView, shellHooks, {
    selectScreen: v2Navigate,
    getActiveScreen: () => activeScreen,
    onAllLives: () => $("backToLives").click(),
    onEditor: () => openEditorFromShell(undefined, false),
    onFamilyTree: () => openV2Panel("familyTree"),
    onM25: () => openV2Panel("m25"),
    // §Editor navigation: exact person ID from the Person module.
    onEdit: (pid) => openEditorFromShell(pid, false),
    onError: (m) => showToast(m, true),
    isPanelOpen: () => isV2PanelOpen(),
    closeTopPanel: () => closeTopPanel(),
    closePanels: () => closeV2Panels(),
  });
  return v2Shell;
}

/* M2.5 in-play character editor (relationship_policy feature, B). Lazy
 * import so the game page pays nothing when the panel stays closed. */
let editorModule = null;
$("editorToggle").addEventListener("click", () => {
  if (editorModule && editorModule.isEditorOpen()) closeTopPanel();
  else openEditorFromShell();
});

/* M2.5 family tree (family_graph feature, A's module; B owns the shell
 * hook). Lazy-import and mount into a toggleable card. */
let familyTreeModule = null;
let familyTreeCard = null;
async function openFamilyTreePanel() {
  selectScreen("activities");
  closeV2Panels("familyTree");
  if (familyTreeCard && !familyTreeCard.hidden) return;
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
  familyTreeCard.hidden = false;
  await familyTreeModule.mountFamilyTree(familyTreeCard, state.gameId);
}
$("familyTreeToggle").addEventListener("click", async () => {
  try {
    if (familyTreeCard && !familyTreeCard.hidden) {
      closeTopPanel();
      return;
    }
    v2Push({ panel: "familyTree" });
    await openFamilyTreePanel();
    if (v2Shell) v2Shell.sync();
  } catch (err) {
    showToast("Family tree failed to load.", true);
  }
});

/* M2.5 dating & weddings screens (Kimi B's relationship_policy feature
 * module). Lazy-import and mount into a toggleable card, same pattern. */
let m25Module = null;
let m25Card = null;
async function openM25Panel() {
  selectScreen("activities");
  closeV2Panels("m25");
  if (m25Card && !m25Card.hidden) return;
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
  m25Card.hidden = false;
  await m25Module.mountM25(m25Card, shellHooks);
}
$("m25Toggle").addEventListener("click", async () => {
  try {
    if (m25Card && !m25Card.hidden) {
      closeTopPanel();
      return;
    }
    v2Push({ panel: "m25" });
    await openM25Panel();
    if (v2Shell) v2Shell.sync();
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
