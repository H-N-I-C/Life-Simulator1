import { loadCatalog, renderAvatar, rng, pick } from "./common.js";

const TABS = [
  ["body", ["skin_tone", "face_shape"]],
  ["face", ["eye_shape", "eye_color", "brows", "nose", "mouth", "marks"]],
  ["hair", ["hair_texture", "hair_color", "hair_style", "facial_hair"]],
  ["style", ["outfit_style", "outfit_color"]],
  ["accessories", ["glasses", "headwear", "earrings"]],
];

const data = await loadCatalog();
const cat = data.catalog;

const DEFAULT_APP = JSON.parse(JSON.stringify(data.presets[0].appearance));

let app = JSON.parse(JSON.stringify(DEFAULT_APP));
app.seed = Math.floor(Math.random() * 2 ** 31);
let activeTab = "body";
let seedCounter = app.seed;

const $ = (id) => document.getElementById(id);

/* ---- catalog accessors (marks is a list; accessories are single ids) ---- */
function entriesFor(category) {
  return cat[category] || [];
}
function getVal(category) {
  if (category === "glasses" || category === "headwear" ||
      category === "earrings") {
    return app.accessories[category];
  }
  return app[category];
}
function setVal(category, id) {
  if (category === "glasses" || category === "headwear" ||
      category === "earrings") {
    app.accessories[category] = id;
  } else {
    app[category] = id;
  }
}

const preview = $("preview");
let lastFocusedSwatch = null;

async function refresh() {
  const age = +$("age").value;
  const stage =
    age <= 2 ? "Baby" : age <= 12 ? "Child" : age <= 17 ? "Teen"
    : age <= 49 ? "Adult" : "Elder";
  $("ageLabel").textContent = `${stage} · ${age}`;
  const ok = await renderAvatar(preview, app, age, 220);
  if (ok) updatePressedStates();
  else $("startMsg").textContent = "Preview failed to load; check the server.";
}

/* ---- tabs: accessible pattern (roving tabindex + arrow keys) ---- */
const tabsEl = $("tabs");
const tabButtons = [];
for (const [name] of TABS) {
  const b = document.createElement("button");
  b.textContent = name[0].toUpperCase() + name.slice(1);
  b.setAttribute("role", "tab");
  b.id = "tab-" + name;
  b.dataset.tab = name;
  b.addEventListener("click", () => selectTab(name, true));
  b.addEventListener("keydown", (e) => {
    const idx = tabButtons.indexOf(b);
    let next = null;
    if (e.key === "ArrowRight") next = tabButtons[(idx + 1) % tabButtons.length];
    else if (e.key === "ArrowLeft") next = tabButtons[(idx - 1 + tabButtons.length) % tabButtons.length];
    else if (e.key === "Home") next = tabButtons[0];
    else if (e.key === "End") next = tabButtons[tabButtons.length - 1];
    if (next) { e.preventDefault(); next.focus(); selectTab(next.dataset.tab, true); }
  });
  tabsEl.appendChild(b);
  tabButtons.push(b);
}

function selectTab(name, rebuild) {
  activeTab = name;
  for (const b of tabButtons) {
    const selected = b.dataset.tab === name;
    b.setAttribute("aria-selected", String(selected));
    b.tabIndex = selected ? 0 : -1;
  }
  if (rebuild) buildOptions();
}

/* ---- options: built once per tab; selection updates state in place ---- */
const optionsBox = $("options");

function buildOptions() {
  optionsBox.innerHTML = "";
  const cats = TABS.find(([n]) => n === activeTab)[1];
  for (const category of cats) {
    const label = document.createElement("h2");
    label.textContent = category.replace(/_/g, " ");
    optionsBox.appendChild(label);
    const row = document.createElement("div");
    row.className = "swatches";
    row.dataset.category = category;
    row.style.marginBottom = "10px";
    for (const e of entriesFor(category)) {
      const sw = document.createElement("button");
      sw.className = "swatch";
      sw.dataset.category = category;
      sw.dataset.value = e.id;
      if (e.hex) {
        const dot = document.createElement("span");
        dot.className = "dot";
        dot.style.background = e.hex;
        sw.appendChild(dot);
      }
      sw.appendChild(document.createTextNode(e.label));
      sw.setAttribute("aria-pressed", "false");
      sw.addEventListener("click", () => onSwatch(sw));
      row.appendChild(sw);
    }
    if (category === "marks") {
      const none = document.createElement("button");
      none.className = "swatch";
      none.dataset.category = "marks";
      none.dataset.value = "";
      none.textContent = "None";
      none.setAttribute("aria-pressed", "false");
      none.addEventListener("click", () => onSwatch(none));
      row.appendChild(none);
    }
    optionsBox.appendChild(row);
  }
  updatePressedStates();
}

function onSwatch(sw) {
  const category = sw.dataset.category;
  const value = sw.dataset.value;
  lastFocusedSwatch = sw;
  if (category === "marks") {
    // list semantics: toggle membership; "None" clears
    if (value === "") {
      app.marks = [];
    } else {
      app.marks = Array.isArray(app.marks) ? app.marks : [];
      const i = app.marks.indexOf(value);
      if (i >= 0) app.marks.splice(i, 1);
      else app.marks.push(value);
    }
  } else {
    setVal(category, value);
  }
  refresh();  // updates pressed states in place after a successful render
}

function updatePressedStates() {
  for (const sw of optionsBox.querySelectorAll(".swatch")) {
    const category = sw.dataset.category;
    const value = sw.dataset.value;
    let pressed;
    if (category === "marks") {
      pressed = value === "" ? (app.marks || []).length === 0
                             : (app.marks || []).includes(value);
    } else {
      pressed = getVal(category) === value;
    }
    sw.setAttribute("aria-pressed", String(pressed));
  }
}

/* ---- identity fields ---- */
const countrySel = $("country");
for (const c of data.countries) {
  const o = document.createElement("option");
  o.textContent = c;
  countrySel.appendChild(o);
}
const presetSel = $("preset");
for (let i = 0; i < data.presets.length; i++) {
  const o = document.createElement("option");
  o.textContent = data.presets[i].name;
  o.value = i;
  presetSel.appendChild(o);
}
presetSel.addEventListener("change", () => {
  app = JSON.parse(JSON.stringify(data.presets[+presetSel.value].appearance));
  app.seed = seedCounter;
  refresh();
});

/* ---- randomize / reset ---- */
function randomize(categories) {
  const rand = rng(seedCounter = (seedCounter + 1) | 0);
  for (const category of categories) {
    const list = entriesFor(category);
    if (!list.length) continue;
    if (category === "marks") {
      app.marks = rand() < 0.5 ? [] : [pick(rand, entriesFor("marks")).id];
    } else {
      setVal(category, pick(rand, list).id);
    }
  }
  refresh();
}
$("randomAll").addEventListener("click", () =>
  randomize(TABS.flatMap(([, cs]) => cs)));
$("randomCat").addEventListener("click", () =>
  randomize(TABS.find(([n]) => n === activeTab)[1]));
$("resetBtn").addEventListener("click", () => {
  app = JSON.parse(JSON.stringify(DEFAULT_APP));
  app.seed = seedCounter;
  refresh();
});
$("age").addEventListener("input", refresh);

/* ---- save ---- */
function buildSave() {
  return {
    schema_version: 1,
    seed: app.seed,
    ...app,
    marks: Array.isArray(app.marks) ? [...app.marks] : [],
    identity: {
      name: $("name").value,
      gender: $("gender").value,
      country: countrySel.value,
    },
  };
}
$("saveBtn").addEventListener("click", () => {
  const blob = new Blob([JSON.stringify(buildSave(), null, 2)],
    { type: "application/json" });
  const a = document.createElement("a");
  a.href = URL.createObjectURL(blob);
  a.download = "appearance.json";
  a.click();
  URL.revokeObjectURL(a.href);
});
$("printBtn").addEventListener("click", () => {
  document.querySelectorAll("pre.export").forEach((n) => n.remove());
  const pre = document.createElement("pre");
  pre.className = "card export";
  pre.textContent = JSON.stringify(buildSave(), null, 2);
  document.querySelector("main").appendChild(pre);
});

/* ---- start life: retry-stable creation operation ----
 * The operation (immutable profile snapshot + request_id) is persisted per
 * tab BEFORE sending and reused on every uncertain retry, including after
 * reload, until a definitive response arrives. Server-side replay then
 * guarantees exactly one life per operation. */
const CREATE_OP_KEY = "pending_create_op";

function buildProfile() {
  const parts = ($("name").value || "Alex Rivers").trim().split(/\s+/);
  return {
    first_name: parts[0] || "Alex",
    last_name: parts.slice(1).join(" ") || "Rivers",
    gender: $("gender").value,
    country: countrySel.value,
    appearance: { ...app, seed: app.seed,
                  marks: Array.isArray(app.marks) ? [...app.marks] : [] },
  };
}

function loadCreateOp() {
  try { return JSON.parse(sessionStorage.getItem(CREATE_OP_KEY) || "null"); }
  catch (e) { return null; }
}

const startBtn = $("startBtn");
if (loadCreateOp()) {
  $("startMsg").textContent =
    "An unfinished 'start life' attempt is being retried with its original " +
    "request so no duplicate life is created.";
}
startBtn.addEventListener("click", async () => {
  startBtn.disabled = true;
  $("startMsg").textContent = "";
  // Reuse the pending operation (same request_id + profile snapshot) until
  // it definitively succeeds or is rejected. Only deliberate validation
  // failures clear it; transport/parse failures keep it for retry.
  let op = loadCreateOp();
  if (!op) {
    op = { profile: buildProfile(), request_id: crypto.randomUUID() };
    sessionStorage.setItem(CREATE_OP_KEY, JSON.stringify(op));
  }
  try {
    const res = await fetch("/api/games", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...op.profile, request_id: op.request_id }),
    });
    const body = await res.json();
    if (!res.ok) {
      // Definitive rejection: validation errors cannot be fixed by retrying
      // the identical operation.
      sessionStorage.removeItem(CREATE_OP_KEY);
      throw new Error((body.error && body.error.message) ||
                      "Could not start life");
    }
    // Definitive success — clear the operation, then navigate.
    sessionStorage.removeItem(CREATE_OP_KEY);
    sessionStorage.setItem("game_id", body.game_id);
    location.href = "/game";
  } catch (err) {
    // Uncertain outcome (network loss, parse failure): keep the operation
    // so the next click (or a reload) resends the identical request and
    // the server replays the original response instead of creating a
    // second life.
    $("startMsg").textContent =
      err.message + " — press Start life again to retry safely.";
    startBtn.disabled = false;
  }
});

selectTab("body", true);
refresh();
