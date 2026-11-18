/* v2 Person detail DOM tests (Kimi C). Exercises web/static/v2/person.js
 * (mountPerson/updatePerson/unmountPerson) against a minimal DOM stub with
 * no server. The module is pure presentation on frozen shell hooks.
 * Run: node tests/web_v2_person_dom.mjs */

import assert from "node:assert/strict";

/* ---------- minimal DOM/browser stubs ---------- */
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
    this.style = {};
    this.title = "";
    this._classes = new Set();
  }
  get className() { return [...this._classes].join(" "); }
  set className(v) {
    this._classes = new Set(String(v).split(/\s+/).filter(Boolean));
  }
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
      contains: (c) => s.has(c),
    };
  }
  set innerHTML(v) { if (v === "") this.children = []; }
  get innerHTML() { return ""; }
  get isConnected() { return this.parent !== null; }
  appendChild(c) { c.parent = this; this.children.push(c); return c; }
  append(...cs) { cs.forEach((c) => this.appendChild(c)); }
  prepend(c) { c.parent = this; this.children.unshift(c); return c; }
  remove() {
    if (this.parent) {
      const i = this.parent.children.indexOf(this);
      if (i >= 0) this.parent.children.splice(i, 1);
      this.parent = null;
    }
  }
  setAttribute(k, v) { this.attributes[k] = String(v); }
  getAttribute(k) { return this.attributes[k]; }
  addEventListener(type, fn) { (this.listeners[type] ||= []).push(fn); }
  dispatch(type, event = {}) {
    for (const fn of this.listeners[type] || []) fn(event);
  }
  click() { this.dispatch("click"); }
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
  getElementById: () => null,
  addEventListener: () => {},
};

globalThis.window = { addEventListener: () => {} };
globalThis.document = documentStub;
Object.defineProperty(globalThis, "navigator",
                      { value: { onLine: true }, configurable: true });
globalThis.location = { pathname: "/game" };
globalThis.sessionStorage = { getItem: () => null };
// Avatar fetch fails softly (common.js keeps the previous/empty container).
globalThis.fetch = () => Promise.reject(new Error("no network in tests"));
globalThis.URL.createObjectURL = () => "blob:stub";
globalThis.console.warn = () => {};

const { mountPerson, updatePerson, unmountPerson, PersonNotFoundError } =
  await import("file:///home/user/Life-Simulator1/web/static/v2/person.js");

/* ---------- fixtures ---------- */
function detail(over = {}) {
  return {
    id: "p1", name: "Ada Okafor", age: 54, role: "Mother", alive: true,
    appearance: { schema_version: 1, seed: 3 },
    descriptors: { job: "Nurse", faith: "Christian", city: "Lagos" },
    bars: [
      { key: "relationship", label: "Relationship", value: 82 },
      { key: "generosity", label: "Generosity", value: null },
      { key: "health", label: "Health", value: 70 },
    ],
    actions: [
      { id: "spend_time", label: "Spend time", category: "relationships",
        targetId: "p1", targetLabel: null, inputs: [], knownCosts: [],
        limits: [], enabled: true, reason: "" },
      { id: "compliment", label: "Compliment", category: "relationships",
        targetId: "p1", targetLabel: null, inputs: [],
        knownCosts: [{ kind: "money", amount: 0 }], limits: [],
        enabled: true, reason: "" },
      { id: "ask_for_money", label: "Ask for money", category: "relationships",
        targetId: "p1", targetLabel: null, inputs: [],
        knownCosts: [], limits: [{ used: 1, max: 1 }],
        enabled: false, reason: "Used this year" },
    ],
    ...over,
  };
}

function hooks(calls) {
  return {
    getState: () => ({ actions: [] }),
    sendAction: (id, payload, targetId) =>
      calls.push({ hook: "send", id, payload, targetId }),
    refresh: () => Promise.resolve(null),
    onBack: () => calls.push({ hook: "back" }),
    onEdit: (pid) => calls.push({ hook: "edit", pid }),
  };
}

function tiles(root) {
  return root.findAll((n) => n.tagName === "BUTTON" &&
                             n._classes.has("v2p-action") &&
                             !n.textContent.includes("More"));
}

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

/* ---------- tests ---------- */

test("mount renders name, role title, descriptors and bar values", () => {
  const root = new El("div");
  mountPerson(root, hooks([]), detail());
  const texts = root.findText(() => true);
  assert.ok(texts.includes("Ada Okafor"));
  assert.ok(texts.includes("Mother"));            // header title = role
  assert.ok(texts.some((t) => t.includes("54") && t.includes("Nurse") &&
                             t.includes("Lagos")));
  assert.ok(texts.includes("82%"));
  assert.ok(texts.includes("unknown"));           // null bar value
});

test("no mock-only data: grid shows exactly the advertised specs", () => {
  const root = new El("div");
  mountPerson(root, hooks([]), detail());
  const labels = tiles(root).map((t) => t.textContent);
  assert.equal(tiles(root).length, 3);
  for (const banned of ["Gift", "Holiday", "Call", "Agreement", "%"]) {
    assert.ok(!labels.some((l) => l.includes(banned)),
              "must not render " + banned);
  }
});

test("disabled spec is really disabled with the server reason visible", () => {
  const root = new El("div");
  const calls = [];
  mountPerson(root, hooks(calls), detail());
  const used = tiles(root).find((t) => t.textContent.includes("Ask for money"));
  assert.equal(used.disabled, true);
  assert.equal(used.getAttribute("aria-disabled"), "true");
  assert.ok(used.textContent.includes("Used this year"));
  used.click();
  assert.equal(calls.length, 0, "disabled tile must not send");
});

test("enabled click sends the exact action id, payload and target id", async () => {
  const root = new El("div");
  const calls = [];
  mountPerson(root, hooks(calls), detail());
  const tile = tiles(root).find((t) => t.textContent.includes("Spend time"));
  tile.click();
  await Promise.resolve(); await Promise.resolve();   // busy-guard promise
  assert.deepEqual(calls, [{ hook: "send", id: "spend_time",
                             payload: {}, targetId: "p1" }]);
});

test("unknown or malformed detail throws the typed error", () => {
  const root = new El("div");
  assert.throws(() => mountPerson(root, hooks([]), null),
                PersonNotFoundError);
  assert.throws(() => mountPerson(root, hooks([]), { name: "Ghost" }),
                (e) => e.code === "person_not_found");
});

test("deceased person shows †, note, and disables all tiles", () => {
  const root = new El("div");
  mountPerson(root, hooks([]),
              detail({ alive: false, actions: [
                { id: "spend_time", label: "Spend time", targetId: "p1",
                  inputs: [], knownCosts: [], limits: [],
                  enabled: true, reason: "" },
              ] }));
  const texts = root.findText(() => true);
  assert.ok(texts.includes("Ada Okafor †"));
  assert.ok(texts.includes("This person has died."));
  assert.ok(tiles(root).every((t) => t.disabled));
});

test("back and edit route through the navigation hooks", () => {
  const root = new El("div");
  const calls = [];
  mountPerson(root, hooks(calls), detail());
  root.findAll((n) => n.getAttribute("aria-label") === "Back")
      .forEach((b) => b.click());
  root.findAll((n) => n.getAttribute("aria-label") === "Edit")
      .forEach((b) => b.click());
  assert.deepEqual(calls, [{ hook: "back" }, { hook: "edit", pid: "p1" }]);
});

test("More reveals the remaining specs only", () => {
  const root = new El("div");
  const many = detail({ actions: Array.from({ length: 11 }, (_, i) => ({
    id: "act" + i, label: "Action " + i, targetId: "p1", inputs: [],
    knownCosts: [], limits: [], enabled: true, reason: "",
  })) });
  mountPerson(root, hooks([]), many);
  assert.equal(tiles(root).length, 8, "collapsed to 8 tiles");
  const more = root.findAll((n) => n.tagName === "BUTTON" &&
                                   n.textContent.includes("More"))[0];
  more.click();
  assert.equal(tiles(root).length, 11);
});

test("updatePerson re-renders in place; unmountPerson clears the root", () => {
  const root = new El("div");
  mountPerson(root, hooks([]), detail());
  updatePerson(root, detail({ age: 55 }));
  assert.ok(root.findText((t) => t.includes("55")).length >= 1);
  unmountPerson(root);
  assert.equal(root.children.length, 0);
  assert.equal(root._classes.has("v2p-root"), false);
});

test("user-edited name is rendered as text, never HTML", () => {
  const root = new El("div");
  mountPerson(root, hooks([]),
              detail({ name: "<img src=x onerror=alert(1)>" }));
  const imgs = root.findAll((n) => n.tagName === "IMG");
  assert.equal(imgs.length, 0);
  assert.ok(root.findText((t) => t.includes("<img src=x")).length >= 1);
});

test("busy guard: a second click while in flight is ignored", async () => {
  const root = new El("div");
  const calls = [];
  mountPerson(root, hooks(calls), detail());
  const tile = tiles(root).find((t) => t.textContent.includes("Spend time"));
  tile.click();
  tile.click();   // still busy
  await Promise.resolve(); await Promise.resolve();
  assert.equal(calls.filter((c) => c.hook === "send").length, 1);
});

console.log(passed + " v2-person DOM tests passed" +
            (process.exitCode ? " (with failures)" : ""));

const fs = await import('node:fs');
const real = JSON.parse(fs.readFileSync('docs/collab/redesign-v2/evidence/real-disabled-person.json', 'utf8'));
const adapter = await import('file:///home/user/Life-Simulator1/web/static/adapter.js');
const root = new El('div');
const calls = [];
mountPerson(root, hooks(calls), {...real.person, actions: [adapter.normalizeAction(real.action)]});
const tile = tiles(root)[0];
console.log('REAL_DISABLED_ACTION_RESULT', JSON.stringify({server_enabled: real.action.enabled, tile_disabled: tile.disabled, text:tile.textContent}));
assert.equal(tile.disabled, true, 'real server-disabled spec must render disabled');
