import { loadCatalog, renderAvatar, rng, pick, showCrashCard,
         setErrorRetryHandler } from "./common.js";

const TABS = [
  ["look", ["base"]],
  ["skin", ["skin_tone"]],
  ["hair", ["hair"]],
  ["extras", ["facial_hair", "headwear"]],
  ["badges", ["badges"]],
  ["background", ["background"]],
];

let data;
try {
  data = await loadCatalog();
} catch (e) {
  // The whole creator depends on the catalog: show the friendly card with
  // a Retry that simply re-runs the page (all choices are rebuilt from
  // the catalog, so nothing of value is lost).
  setErrorRetryHandler(() => location.reload());
  showCrashCard("Couldn't load the character catalog. " +
                ((e && e.message) || ""));
  // Halt module evaluation without an unhandled rejection (the card
  // above is the UI): nothing below can run without the catalog.
  await new Promise(() => {});
}
const cat = data.catalog;

const DEFAULT_APP = JSON.parse(JSON.stringify(data.presets[0].appearance));

let app = JSON.parse(JSON.stringify(DEFAULT_APP));
app.seed = Math.floor(Math.random() * 2 ** 31);
let activeTab = "body";
let seedCounter = app.seed;

const $ = (id) => document.getElementById(id);

const THUMB_CATEGORIES = new Set(["base", "skin_tone", "hair",
                                  "facial_hair", "headwear", "badges"]);

const CATEGORY_LABELS = {
  base: "Look", skin_tone: "Skin tone", hair: "Hair",
  facial_hair: "Facial hair", headwear: "Headwear", badges: "Badges",
  background: "Background",
};

function avatarUrl(appearance, age) {
  const raw = btoa(unescape(encodeURIComponent(
    JSON.stringify(appearance))));
  return "/api/avatar.png?app=" + raw.replace(/\+/g, "-")
      .replace(/\//g, "_").replace(/=+$/, "") + "&age=" + age;
}

/* ---- catalog accessors (marks is a list; accessories are single ids) ---- */
function entriesFor(category) {
  return cat[category] || [];
}
function getVal(category) {
  return app[category];
}
function setVal(category, id) {
  app[category] = id;
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
    label.textContent = CATEGORY_LABELS[category] ||
      category.replace(/_/g, " ");
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
      } else if (THUMB_CATEGORIES.has(category)) {
        // Fluent thumbnail: the option applied to the current look.
        // Retries absorb transient load races so a swatch never shows a
        // broken icon.
        const thumb = document.createElement("img");
        thumb.className = "swatch-thumb";
        thumb.alt = "";
        const variant = { ...app };
        let src;
        if (category === "badges") {
          variant.badges = [e.id];
          src = "/static/avatars/badge_" + encodeURIComponent(e.id) + ".png";
        } else {
          variant[category] = e.id;
          src = avatarUrl(variant, 30);
        }
        let tries = 0;
        thumb.onerror = () => {
          if (++tries < 3) setTimeout(() => { thumb.src = src; }, 300);
          else thumb.style.visibility = "hidden";
        };
        thumb.src = src;
        sw.appendChild(thumb);
      }
      sw.appendChild(document.createTextNode(e.label));
      sw.setAttribute("aria-pressed", "false");
      sw.addEventListener("click", () => onSwatch(sw));
      row.appendChild(sw);
    }
    if (category === "badges") {
      const none = document.createElement("button");
      none.className = "swatch";
      none.dataset.category = "badges";
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
  if (category === "badges") {
    // list semantics: toggle membership; "None" clears; contract cap 2
    if (value === "") {
      app.badges = [];
    } else {
      app.badges = Array.isArray(app.badges) ? app.badges : [];
      const i = app.badges.indexOf(value);
      if (i >= 0) app.badges.splice(i, 1);
      else if (app.badges.length < 2) app.badges.push(value);
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
    if (category === "badges") {
      pressed = value === "" ? (app.badges || []).length === 0
                             : (app.badges || []).includes(value);
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
    if (category === "badges") {
      app.badges = rand() < 0.5 ? [] : [pick(rand, entriesFor("badges")).id];
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
    badges: Array.isArray(app.badges) ? [...app.badges] : [],
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

/* ---- dev-only tools: Export/Print JSON hidden in normal play so
 * Start life is the main action ---- */
function applyDevToolsVisibility() {
  const dev = !!window.__devMode;
  for (const id of ["saveBtn", "printBtn"]) {
    const b = $(id);
    if (b) b.hidden = !dev;
  }
}

/* ---- dev scenario panel (only when the server exposes it) ---- */
let scenarioId = null;
(async () => {
  try {
    // Only probe scenarios when the server reports dev mode — avoids a
    // pointless 404 (and keeps the page idle) in normal play.
    const info = await fetch("/api/service-info").then((r) =>
      r.ok ? r.json() : null);
    window.__devMode = !!(info && info.dev);
    applyDevToolsVisibility();
    if (!window.__devMode) return;
    const res = await fetch("/api/scenarios");
    if (!res.ok) return;
    const data = await res.json();
    if (!data.scenarios || !data.scenarios.length) return;
    const card = document.createElement("section");
    card.className = "card";
    card.innerHTML = `<h2>Dev: start as…</h2>
      <p class="muted">Development-only scenario starts — real transitions,
      ordinary saves. Pick one or leave blank for a normal birth.</p>`;
    const row = document.createElement("div");
    row.className = "row";
    for (const sc of data.scenarios) {
      const b = document.createElement("button");
      b.className = "small";
      b.textContent = sc.label;
      b.setAttribute("aria-pressed", "false");
      b.addEventListener("click", () => {
        scenarioId = (scenarioId === sc.id) ? null : sc.id;
        for (const x of row.querySelectorAll("button")) {
          x.setAttribute("aria-pressed", "false");
        }
        if (scenarioId) b.setAttribute("aria-pressed", "true");
      });
      row.appendChild(b);
    }
    card.appendChild(row);
    document.querySelector("main").insertBefore(
      card, document.getElementById("startBtn").closest("section"));
  } catch (e) { /* dev panel unavailable — normal play */ }
})();

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
                  badges: Array.isArray(app.badges) ? [...app.badges] : [] },
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
    // badges: omit when empty (server validation rejects explicit [])
    const ap = op.profile.appearance;
    if (ap && Array.isArray(ap.badges) && ap.badges.length === 0) {
      delete ap.badges;
    }
    if (scenarioId) op.profile.scenario = scenarioId;
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

applyDevToolsVisibility();
selectTab("look", true);
refresh();
