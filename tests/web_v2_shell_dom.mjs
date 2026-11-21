/* v2 shell DOM tests (Kimi B). Exercises web/static/v2/shell.js
 * (mountShell → {sync, navigate, unmount}) against a minimal DOM stub with
 * no server, per the frozen §Shell contract. The person overlay's fetch is
 * stubbed: typed 404 proves the honest "not available" state, a 200 with a
 * detail payload proves the C-module mount via a person.js stub.
 * Run: node tests/web_v2_shell_dom.mjs */

import assert from "node:assert/strict";

/* ---------- minimal DOM/browser stubs (same harness as C's dom tests) --- */
class El {
  constructor(tag) {
    this.tagName = tag.toUpperCase();
    this.children = [];
    this.parent = null;
    this.attributes = {};
    this.listeners = {};
    this.id = "";
    this._text = "";
    this.hidden = false;
    this.disabled = false;
    this.type = "";
    this.tabIndex = 0;
    this.style = {};
    this.title = "";
    this.dataset = {};
    this.scrollTop = 0;
    this._classes = new Set();
  }
  get className() { return [...this._classes].join(" "); }
  set className(v) {
    this._classes = new Set(String(v).split(/\s+/).filter(Boolean));
  }
  get firstChild() { return this.children[0] || null; }
  get textContent() {
    if (this.children.length) {
      return this.children.map((c) => c.textContent).join("");
    }
    return this._text;
  }
  set textContent(v) {
    this._text = String(v);
    if (v === "") this.children = [];
  }
  get classList() {
    const s = this._classes;
    return {
      add: (...c) => c.forEach((x) => s.add(x)),
      remove: (...c) => c.forEach((x) => s.delete(x)),
      toggle: (c, force) => {
        const want = force === undefined ? !s.has(c) : !!force;
        if (want) s.add(c); else s.delete(c);
        return want;
      },
      contains: (c) => s.has(c),
    };
  }
  set innerHTML(v) { if (v === "") this.children = []; }
  get innerHTML() { return ""; }
  get isConnected() { return this.parent !== null; }
  appendChild(c) {
    if (c.parent) {
      const i = c.parent.children.indexOf(c);
      if (i >= 0) c.parent.children.splice(i, 1);
    }
    c.parent = this;
    this.children.push(c);
    return c;
  }
  append(...cs) { cs.forEach((c) => this.appendChild(c)); }
  prepend(c) { c.parent = this; this.children.unshift(c); return c; }
  remove() {
    if (this.parent) {
      const i = this.parent.children.indexOf(this);
      if (i >= 0) this.parent.children.splice(i, 1);
      this.parent = null;
    }
  }
  click() { this.dispatch("click"); }
  setAttribute(k, v) { this.attributes[k] = String(v); }
  getAttribute(k) { return this.attributes[k]; }
  removeAttribute(k) { delete this.attributes[k]; }
  addEventListener(type, fn) { (this.listeners[type] ||= []).push(fn); }
  dispatch(type, event = {}) {
    for (const fn of this.listeners[type] || []) fn(event);
  }
  walk(fn) { fn(this); for (const c of this.children) c.walk(fn); }
  findAll(fn, out = []) { this.walk((n) => { if (fn(n)) out.push(n); });
                          return out; }
  findText(pred) {
    return this.findAll((n) => n.children.length === 0 &&
                              typeof n._text === "string" && n._text &&
                              pred(n._text)).map((n) => n._text);
  }
}

const documentStub = {
  body: new El("body"),
  activeElement: null,
  createElement: (tag) => new El(tag),
  createTextNode: (t) => { const n = new El("#text"); n._text = String(t);
                           return n; },
  getElementById: () => null,
  addEventListener: () => {},
};

globalThis.window = { addEventListener: () => {} };
globalThis.document = documentStub;
Object.defineProperty(globalThis, "navigator",
                      { value: { onLine: true }, configurable: true });
globalThis.location = { pathname: "/game" };
globalThis.sessionStorage = { getItem: () => null };

/* fetch stub: the shell's person-detail GET succeeds for p1, typed-404s
 * otherwise; every other URL (avatar render) fails softly. */
globalThis.fetch = (url) => {
  const u = String(url);
  if (u.includes("/people/")) {
    if (u.endsWith("/p1")) {
      return Promise.resolve({
        ok: true,
        json: () => Promise.resolve({
          id: "p1", name: "Ada Okafor", age: 54, role: "Mother", alive: true,
          appearance: {}, descriptors: { job: "Nurse" },
          bars: [{ key: "relationship", label: "Relationship", value: 82 }],
          // A's advertised snake_case shape — the shell must normalize.
          actions: [{ id: "spend_time", label: "Spend time",
                      category: "relationships", target_id: "p1",
                      inputs: [], known_costs: [{ kind: "money",
                                                  amount: 20 }],
                      limits: [], enabled: true, reason: null }],
        }),
      });
    }
    if (u.endsWith("/dead1")) {
      return Promise.resolve({
        ok: true,
        json: () => Promise.resolve({
          id: "dead1", name: "Grace Okafor", age: 88, role: "Grandmother",
          alive: false, appearance: {},
          descriptors: {},                 // no approved public descriptors
          bars: [{ key: "relationship", label: "Relationship", value: 71 }],
          actions: [],                     // deceased: no actions served
        }),
      });
    }
    if (u.endsWith("/sparse")) {
      return Promise.resolve({
        ok: true,
        json: () => Promise.resolve({
          id: "sparse", name: "Sam", alive: true, appearance: null,
          // role/descriptors/bars/actions absent — must render honestly
        }),
      });
    }
    return Promise.resolve({
      ok: false,
      json: () => Promise.resolve(
        { error: { code: "person_not_found", message: "not available" } }),
    });
  }
  return Promise.reject(new Error("no network in tests"));
};

const { mountShell, V2_SCREENS } =
  await import("../web/static/v2/shell.js");

/* ---------- fixtures ---------- */
function state(over = {}) {
  return {
    gameId: "g1",
    revision: 3,
    status: "alive",
    playerId: "me",
    player: { id: "me", name: "Maya Okafor", age: 27 },
    money: 48210,
    country: "Nigeria",
    sections: {
      work: { employed: true, job_label: "Junior Architect" },
      education: null,
    },
    ...over,
  };
}

function makeHost() {
  const host = new El("section");
  for (const s of V2_SCREENS) {
    const sec = new El("section");
    sec.id = "screen-" + s;
    sec.hidden = true;
    host.appendChild(sec);
  }
  return host;
}

function setup(opts = {}, overState = {}) {
  const host = makeHost();
  const calls = { screens: [] };
  const hooks = {
    getState: () => state(overState),
    sendAction: () => {},
    resolveDecision: () => {},
    refresh: () => Promise.resolve(null),
  };
  const shell = mountShell(host, hooks, {
    selectScreen: (n) => calls.screens.push(n),
    getActiveScreen: () => calls.screens[calls.screens.length - 1] || "home",
    onError: (m) => calls.error = m,
    ...opts,
  });
  return { host, shell, calls };
}

const tick = () => new Promise((r) => setTimeout(r, 0));

let passed = 0;
function test(name, fn) {
  try {
    fn();
    passed++;
    console.log("ok - " + name);
  } catch (e) {
    console.error("FAIL - " + name);
    console.error(e);
    process.exitCode = 1;
  }
}
async function testAsync(name, fn) {
  try {
    await fn();
    passed++;
    console.log("ok - " + name);
  } catch (e) {
    console.error("FAIL - " + name);
    console.error(e);
    process.exitCode = 1;
  }
}

/* ---------- tests ---------- */

test("bottom bar: four tabs + one big central Age button (F3)", () => {
  const { host } = setup();
  const tabs = host.findAll((n) => n._classes.has("v2-tab"));
  assert.equal(tabs.length, 4);
  const labels = tabs.map((t) => t.textContent);
  for (const l of ["Occupation", "Assets", "Relationships",
                   "Activities"]) {
    assert.ok(labels.some((x) => x.includes(l)), "bar must include " + l);
  }
  assert.equal(labels.some((x) => x.includes("AGE")), false,
               "AGE is the centre button, not a tab");
  const age = host.findAll((n) => n._classes.has("v2-agebtn"));
  assert.equal(age.length, 1);
  assert.ok(age[0].textContent.includes("AGE"));
  // AGE sits between Assets and Relationships in the bar.
  const bar = host.findAll((n) => n._classes.has("v2-tabbar"))[0];
  const order = bar.children.map((c) => c.textContent);
  const idx = (pred) => order.findIndex((t) => pred(t));
  assert.ok(idx((t) => t.includes("Assets")) > -1 &&
            idx((t) => t.includes("Assets")) < idx((t) => t.includes("AGE")) &&
            idx((t) => t.includes("AGE")) <
              idx((t) => t.includes("Relationships")),
            "bar order: Occupation · Assets · AGE · Relationships · Activities");
  const more = host.findAll((n) => n._classes.has("v2-more") &&
                                   n.tagName === "DIV")[0];
  const moreLabels = more.findText(() => true);
  for (const l of ["All lives", "Character editor", "Family tree",
                   "Dating & weddings"]) {
    assert.ok(moreLabels.includes(l), "More must include " + l);
  }
});

test("More routes dispatch to the frozen secondary callbacks", () => {
  const got = [];
  const { host } = setup({
    onAllLives: () => got.push("allLives"),
    onEditor: () => got.push("editor"),
    onFamilyTree: () => got.push("familyTree"),
    onM25: () => got.push("m25"),
  });
  const more = host.findAll((n) => n._classes.has("v2-more"))[0];
  const byText = (t) => more.findAll((n) => n.tagName === "BUTTON" &&
                                           n.textContent === t)[0];
  for (const [t, id] of [["All lives", "allLives"],
                         ["Character editor", "editor"],
                         ["Family tree", "familyTree"],
                         ["Dating & weddings", "m25"]]) {
    byText(t).click();
    assert.ok(got.includes(id), id + " callback fired");
  }
});

test("sync renders real name, real job status and money — no mock data", () => {
  const { host, shell } = setup();
  shell.sync();
  const texts = host.findText(() => true);
  assert.ok(texts.includes("Maya Okafor"));
  assert.ok(texts.some((t) => t.includes("Junior Architect") &&
                             t.includes("Nigeria")));
  assert.ok(texts.includes("$48,210"));
  // student/unemployed/retired fallbacks, never invented jobs
  const host2 = makeHost();
  const shell2 = mountShell(host2, {
    getState: () => state({ sections: {
      work: { employed: false, retired: true }, education: null } }),
  }, {});
  shell2.sync();
  assert.ok(host2.findText((t) => t.includes("Retired")).length >= 1);
});

test("header subtitle shows the named career title when assigned", () => {
  const { host, shell } = setup();
  shell.sync();
  const base = host.findText((t) => t.includes("Junior Architect")).length;
  assert.ok(base >= 1, "legacy job label shown without an assignment");
  // with a M3 named assignment the subtitle leads with role · employer
  const host3 = makeHost();
  const shell3 = mountShell(host3, {
    getState: () => state({
      careers: { assignment: {
        role_id: "electrician", role_label: "Electrician",
        employer_id: "voltworks", employer_name: "Voltworks Electrical" } },
      sections: { work: { employed: true, job_label: "Legacy label" } } }),
  }, {});
  shell3.sync();
  const texts = host3.findText((t) => t.includes("Electrician"));
  assert.ok(texts.length >= 1, "named role shown");
  assert.ok(texts.some((t) => t.includes("Voltworks Electrical")),
            "named employer shown");
  assert.equal(host3.findText((t) => t.includes("Legacy label")).length, 0,
               "legacy label replaced by the named title");
});

test("tab click selects its screen; header home link selects home", () => {
  const { host, shell, calls } = setup();
  shell.sync();
  const people = host.findAll((n) => n._classes.has("v2-tab"))
                     .find((b) => b.textContent.includes("Relationships"));
  people.click();
  assert.deepEqual(calls.screens, ["people"]);
  const home = host.findAll((n) => n._classes.has("v2-header-namerow"))[0];
  home.click();
  assert.deepEqual(calls.screens, ["people", "home"]);
});

testAsync("navigate('person') mounts C's module; back restores the screen",
          async () => {
  const { host, shell, calls } = setup();
  shell.navigate("people");           // come from the People roster
  shell.navigate("person", { personId: "p1" });
  await tick(); await tick(); await tick();
  const hostEl = host.findAll((n) => n._classes.has("v2-person-host"))[0];
  assert.equal(hostEl.hidden, false);
  // C's module rendered the real detail (name, bar value) inside the host.
  assert.ok(hostEl.findText((t) => t.includes("Ada Okafor")).length >= 1);
  assert.ok(hostEl.findText((t) => t.includes("82%")).length >= 1);
  // Normalization: known_costs → knownCosts reached the module ("$20" tile).
  assert.ok(hostEl.findText((t) => t.includes("$20")).length >= 1,
            "shell normalized A's snake_case spec through normalizeAction");
  // Back via the module's own back button closes the overlay and returns.
  const back = hostEl.findAll((n) =>
    n.getAttribute && n.getAttribute("aria-label") === "Back")[0];
  back.click();
  assert.equal(hostEl.hidden, true);
  assert.equal(hostEl.children.length, 0, "host cleared after back");
  assert.deepEqual(calls.screens[calls.screens.length - 1], "people");
});

testAsync("typed person_not_found closes honestly, never fabricates",
          async () => {
  const { host, shell, calls } = setup();
  shell.navigate("person", { personId: "ghost" });
  await tick(); await tick();
  assert.equal(calls.error, "Person not available.");
  const hostEl = host.findAll((n) =>
    n._classes.has("v2-person-host"))[0];
  assert.equal(hostEl.hidden, true);
  assert.equal(hostEl.findText((t) => t.includes("ghost")).length, 0);
});

testAsync("deceased detail: † marker, died note, empty grid, no Edit",
          async () => {
  const { host, shell } = setup();
  shell.navigate("person", { personId: "dead1" });
  await tick(); await tick();
  const hostEl = host.findAll((n) => n._classes.has("v2-person-host"))[0];
  const texts = hostEl.findText(() => true);
  assert.ok(texts.includes("Grace Okafor †"));
  assert.ok(texts.includes("This person has died."));
  // no action tiles, and the Edit control is hidden for the deceased
  assert.equal(hostEl.findAll((n) => n._classes.has("v2p-action")).length, 0);
  const edit = hostEl.findAll((n) =>
    n.getAttribute && n.getAttribute("aria-label") === "Edit")[0];
  assert.equal(edit.hidden, true);
});

testAsync("missing fields render honestly — no subtitle, no bars, no crash",
          async () => {
  const { host, shell } = setup();
  shell.navigate("person", { personId: "sparse" });
  await tick(); await tick();
  const hostEl = host.findAll((n) => n._classes.has("v2-person-host"))[0];
  assert.ok(hostEl.findText((t) => t.includes("Sam")).length >= 1);
  assert.equal(hostEl.findAll((n) => n._classes.has("v2p-subtitle")).length,
               0, "no fabricated subtitle");
  assert.equal(hostEl.findAll((n) => n._classes.has("v2p-bar")).length, 0,
               "no fabricated bars");
  assert.equal(hostEl.findAll((n) => n._classes.has("v2p-action")).length, 0);
});

test("unmount unwraps the scroll region and removes chrome", () => {
  const { host, shell } = setup();
  shell.sync();
  shell.unmount();
  assert.equal(host.findAll((n) => n._classes.has("v2-header")).length, 0);
  assert.equal(host.findAll((n) => n._classes.has("v2-tabbar")).length, 0);
  assert.equal(host.findAll((n) => n._classes.has("v2-dock")).length, 0);
  for (const s of V2_SCREENS) {
    assert.ok(host.findAll((n) => n.id === "screen-" + s).length === 1,
              "screen-" + s + " still under host");
  }
});

/* ---------- USER BUG FIX: visible Back (priority + visibility) ---------- */

function backButton(host) {
  return host.findAll((n) => n.tagName === "BUTTON" &&
                            n._classes.has("v2-back"))[0];
}

test("Back button: hidden on plain Home, visible on every other view", () => {
  const { host, shell, calls } = setup();
  shell.sync();
  assert.equal(backButton(host).hidden, true, "plain Home hides Back");
  calls.screens.push("job");
  shell.sync();
  assert.equal(backButton(host).hidden, false, "non-Home screen shows Back");
});

test("Back priority: More menu closes first (no navigation)", () => {
  const { host, shell, calls } = setup();
  shell.sync();
  const moreBtn = host.findAll((n) =>
    n.getAttribute && n.getAttribute("aria-label") === "More routes")[0];
  moreBtn.click();
  assert.equal(backButton(host).hidden, false);
  backButton(host).click();
  const more = host.findAll((n) => n._classes.has("v2-more"))[0];
  assert.equal(more.hidden, true, "menu closed");
  assert.deepEqual(calls.screens, [], "no screen navigation happened");
});

testAsync("Back with an open overlay routes through onOverlayBack",
          async () => {
  const overlayCalls = { n: 0 };
  const { host, shell } = setup({
    onOverlayBack: () => overlayCalls.n++,
    isPanelOpen: () => false,
  });
  shell.navigate("person", { personId: "p1" });
  await tick(); await tick(); await tick();
  assert.equal(backButton(host).hidden, false, "overlay makes Back visible");
  backButton(host).click();
  assert.equal(overlayCalls.n, 1, "overlay back delegated");
});

test("Back with an open panel routes through closeTopPanel", () => {
  const closed = { n: 0 };
  const { host, shell } = setup({
    isPanelOpen: () => true,
    closeTopPanel: () => closed.n++,
  });
  shell.sync();
  assert.equal(backButton(host).hidden, false, "panel makes Back visible");
  backButton(host).click();
  assert.equal(closed.n, 1, "panel close delegated");
});

test("Escape closes the More menu and reports other surfaces", () => {
  const { host, shell } = setup({ isPanelOpen: () => false });
  shell.sync();
  const moreBtn = host.findAll((n) =>
    n.getAttribute && n.getAttribute("aria-label") === "More routes")[0];
  moreBtn.click();
  assert.equal(shell.escape(), true, "escape consumed by the menu");
  const more = host.findAll((n) => n._classes.has("v2-more"))[0];
  assert.equal(more.hidden, true);
  assert.equal(shell.escape(), false, "nothing to close on plain Home");
});

test("stats dock: real stats pinned above the bar with % labels (F7)", () => {
  const { host, shell } = setup({}, {
    stats: { happiness: 78, health: 64, smarts: 83, looks: 41 },
  });
  shell.sync();
  const dock = host.findAll((n) => n._classes.has("v2-dock"))[0];
  assert.equal(dock.hidden, false);
  const texts = host.findText(() => true);
  for (const v of [78, 64, 83, 41]) {
    assert.ok(texts.includes(v + "%"), "numeric label " + v + "%");
  }
  // F7: short display label — "Happiness" would clip at 320px ("Happin…")
  assert.ok(texts.includes("Happy"), "happiness displays as Happy");
  assert.equal(texts.includes("Happiness"), false);
  const fills = host.findAll((n) => n._classes.has("v2-stat-fill"));
  assert.equal(fills.length, 4);
  assert.equal(fills[0].style.background, "var(--v2-bar-good)");  // 78
  assert.equal(fills[3].style.background, "var(--v2-bar-mid)");   // 41
  // no stats → dock hides honestly
  const host2 = makeHost();
  const shell2 = mountShell(host2, { getState: () => state() }, {});
  shell2.sync();
  assert.equal(host2.findAll((n) => n._classes.has("v2-dock"))[0].hidden,
               true);
});

test("Age click sends the exact age_up action; spec gates with reason", async () => {
  const calls = [];
  const host = makeHost();
  const shell = mountShell(host, {
    getState: () => state({ actions: [{ id: "age_up", label: "Age up",
                                         enabled: true, reason: "" }] }),
    sendAction: (id, payload, targetId) => {
      calls.push({ id, payload, targetId });
      return Promise.resolve({});
    },
  }, {});
  shell.sync();
  const age = host.findAll((n) => n._classes.has("v2-agebtn"))[0];
  assert.equal(age.disabled, false);
  age.click();
  assert.deepEqual(calls, [{ id: "age_up", payload: {}, targetId: null }]);
  shell.unmount();
});

test("disabled spec disables Age with the server reason visible (F3)", () => {
  const { host, shell } = setup({}, { actions: [{ id: "age_up", label: "Age up",
      enabled: false, reason: "A decision is waiting for you." }] });
  shell.sync();
  const age = host.findAll((n) => n._classes.has("v2-agebtn"))[0];
  assert.equal(age.disabled, true);
  assert.equal(age.getAttribute("aria-disabled"), "true");
  assert.ok(host.findText((t) => t.includes("decision is waiting")).length >= 1);
});

testAsync("Age busy settles from authoritative state; note clears", async () => {
  const host2 = makeHost();
  const current = state({
    actions: [{ id: "age_up", label: "Age up", enabled: true, reason: "" }],
  });
  let resolve;
  const shell2 = mountShell(host2, {
    getState: () => current,
    sendAction: () => new Promise((r) => { resolve = r; }),
  }, {});
  shell2.sync();
  const age2 = host2.findAll((n) => n._classes.has("v2-agebtn"))[0];
  age2.click();
  assert.equal(age2.disabled, true);
  assert.ok(host2.findText((t) => t.includes("Ageing")).length >= 1);
  resolve(current);
  await tick(); await tick();
  assert.equal(age2.disabled, false);
  assert.equal(host2.findText((t) => t.includes("Ageing")).length, 0);
  shell2.unmount();
});

test("F8: stat fills persist across syncs — width updated, flash class, same element", () => {
  const host = makeHost();
  let current = state({
    stats: { happiness: 78, health: 64, smarts: 83, looks: 41 },
  });
  const shell = mountShell(host, { getState: () => current }, {});
  shell.sync();
  const fill = host.findAll((n) => n._classes.has("v2-stat-fill"))[0];
  assert.equal(fill.style.width, "78%");
  assert.equal(fill.classList.contains("animate"), false,
               "first paint snaps, no animation class");
  // second sync with a new value: SAME fill element, width updated
  current = state({
    stats: { happiness: 90, health: 64, smarts: 83, looks: 41 },
  });
  shell.sync();
  const fill2 = host.findAll((n) => n._classes.has("v2-stat-fill"))[0];
  assert.equal(fill2, fill, "fill element persisted (F8)");
  assert.equal(fill2.style.width, "90%");
  assert.equal(fill2.classList.contains("animate"), true,
               "persisted fill animates to the new width");
  const cell = host.findAll((n) => n._classes.has("v2-stat"))[0];
  assert.equal(cell.classList.contains("up"), true, "green flash on gain");
  const val = host.findAll((n) => n._classes.has("v2-stat-value"))[0];
  assert.equal(val.textContent, "90%");
  shell.unmount();
});

test("F8: header age chip renders the player's age", () => {
  const { host, shell } = setup();
  shell.sync();
  const texts = host.findText((t) => t.startsWith("Age "));
  assert.ok(texts.some((t) => t === "Age 27"), "age chip shows Age 27");
});

testAsync("F8: age chip ticks through intermediate ages", async () => {
  const host = makeHost();
  let current = state({ player: { id: "me", name: "Maya Okafor",
                                   age: 27 } });
  const shell = mountShell(host, { getState: () => current }, {});
  shell.sync();
  // pretend a browser environment so the animated tick path runs
  globalThis.requestAnimationFrame = () => 1;
  try {
    current = state({ player: { id: "me", name: "Maya Okafor", age: 30 } });
    shell.sync();                  // starts the tick 27 → 30
    const read = () => {
      const t = host.findText((x) => /^Age \d+$/.test(x));
      return t.length ? Number(t[0].replace("Age ", "")) : null;
    };
    assert.equal(read(), 27, "tick starts from the previous age");
    for (let i = 0; i < 12 && read() !== 30; i++) {
      await new Promise((r) => setTimeout(r, 100));
    }
    assert.equal(read(), 30, "tick lands on the new age");
  } finally {
    delete globalThis.requestAnimationFrame;
    shell.unmount();
  }
});

testAsync("§Undo: spec-driven enable/disable, reason, two-tap confirm sends undo_year", async () => {
  const host = makeHost();
  const calls = [];
  let current = state({ actions: [
    { id: "age_up", label: "Age up", enabled: true, reason: null },
    { id: "undo_year", label: "Undo last year", category: "life",
      enabled: false, reason: "there is no year to undo" },
  ] });
  const shell = mountShell(host, {
    getState: () => current,
    sendAction: (id, payload, targetId) => {
      calls.push({ id, payload, targetId });
      return Promise.resolve({});
    },
  }, {});
  shell.sync();
  const undo = () => host.findAll((n) => n._classes.has("v2-undo"))[0];
  const reason = host.findAll((n) => n._classes.has("v2-undo-reason"))[0];
  assert.equal(undo().disabled, true, "disabled when can_undo is false");
  assert.ok(reason.textContent.includes("no year to undo"),
            "server reason shown");
  undo().click();
  assert.equal(calls.length, 0, "disabled button never sends");

  // enabled: first tap arms the confirm, second tap sends
  current = state({ actions: [
    { id: "age_up", label: "Age up", enabled: true, reason: null },
    { id: "undo_year", label: "Undo last year", category: "life",
      enabled: true, reason: null },
  ] });
  shell.sync();
  assert.equal(undo().disabled, false);
  assert.equal(reason.textContent, "");
  undo().click();
  assert.equal(calls.length, 0, "first tap is the confirm step");
  assert.ok(undo().textContent.includes("confirm"), "confirm label");
  undo().click();
  assert.deepEqual(calls, [{ id: "undo_year", payload: {}, targetId: null }]);
  await tick(); await tick();           // busy settles
  assert.equal(undo().disabled, false, "settlement re-renders the spec");

  // action absent from the projection → row hides honestly
  current = state({ actions: [
    { id: "age_up", label: "Age up", enabled: true, reason: null }] });
  shell.sync();
  assert.equal(host.findAll((n) => n._classes.has("v2-undorow"))[0].hidden,
               true);
  shell.unmount();
});

testAsync("§Undo: a changed revision or life requires a fresh confirmation", async () => {
  const host = makeHost(), calls = [];
  const actions = [{id:"undo_year",label:"Undo last year",enabled:true}];
  let current = state({actions, revision:2});
  const shell = mountShell(host, {getState:()=>current,
    sendAction:(id)=>{calls.push(id);return Promise.resolve();}}, {});
  const undo = host.findAll((n)=>n._classes.has("v2-undo"))[0];
  undo.click();
  current = state({actions, revision:3});
  shell.sync();
  assert.ok(!undo.textContent.includes("confirm"));
  undo.click();
  assert.equal(calls.length,0,"new revision requires the first tap again");
  current = state({actions,revision:3,gameId:"another-life"});
  undo.click();
  assert.equal(calls.length,0,"click handler rechecks identity without sync");
  undo.click();
  assert.deepEqual(calls,["undo_year"]);
  await tick();await tick();shell.unmount();
});

console.log(passed + " v2-shell DOM tests passed" +
            (process.exitCode ? " (with failures)" : ""));
