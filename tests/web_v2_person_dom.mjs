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
  await import("../web/static/v2/person.js");

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

test("M4: header title prefers the social kind over the wrong role word",
     () => {
  const root = new El("div");
  const h = hooks([]);
  h.getState = () => ({ peopleById: { p1: { id: "p1", name: "Ada",
    social: { kind: "acquaintance", closeness: 10, circles: [],
              memory: [] } } } });
  mountPerson(root, h, detail());
  const texts = root.findText(() => true);
  assert.ok(texts.includes("Acquaintance"), "kind text in header");
  assert.ok(!texts.includes("Mother"), "wrong role word replaced");
});

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

test("deceased person shows †, note, disables tiles and hides Edit", () => {
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
  const edit = root.findAll((n) => n.getAttribute("aria-label") === "Edit")[0];
  assert.equal(edit.hidden, true);
});

test("Edit is hidden when the shell offers no onEdit hook", () => {
  const root = new El("div");
  const hk = hooks([]);
  delete hk.onEdit;
  mountPerson(root, hk, detail());
  const edit = root.findAll((n) => n.getAttribute("aria-label") === "Edit")[0];
  assert.equal(edit.hidden, true);
});

test("Edit passes the exact mounted person id (name-twin safe)", () => {
  const root = new El("div");
  const calls = [];
  mountPerson(root, hooks(calls), detail({ id: "p2", name: "Ada Okafor" }));
  root.findAll((n) => n.getAttribute("aria-label") === "Edit")
      .forEach((b) => b.click());
  assert.deepEqual(calls, [{ hook: "edit", pid: "p2" }]);
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

test("wire dialect: snake_case known_costs/target_id and string limits render", async () => {
  const root = new El("div");
  const calls = [];
  mountPerson(root, hooks(calls), detail({ actions: [
    { id: "spend_time", label: "Spend time", category: "relationships",
      target_id: "p1", target_label: "Ada Okafor", inputs: [],
      known_costs: [{ kind: "money", amount: 0 }],
      limits: ["once per year per person"],
      enabled: true, reason: null },
  ] }));
  const tile = tiles(root).find((t) => t.textContent.includes("Spend time"));
  assert.ok(tile.textContent.includes("Free"));
  assert.ok(tile.textContent.includes("once per year per person"),
            "string limit must be shown");
  tile.click();
  await Promise.resolve(); await Promise.resolve();
  assert.deepEqual(calls, [{ hook: "send", id: "spend_time",
                             payload: {}, targetId: "p1" }]);
});

test("normalized dialect: object limits render as used/max", () => {
  const root = new El("div");
  mountPerson(root, hooks([]), detail({ actions: [
    { id: "spend_time", label: "Spend time", targetId: "p1", inputs: [],
      knownCosts: [], limits: [{ used: 1, max: 1 }],
      enabled: true, reason: "" },
  ] }));
  const tile = tiles(root)[0];
  assert.ok(tile.textContent.includes("1/1"));
});

/* ---------- M4 social (B round): edge block + social action tiles ------ */

test("M4: social kind + closeness + memory render from the people entry",
     () => {
  const root = new El("div");
  const h = hooks([]);
  h.getState = () => ({ peopleById: { p1: { id: "p1", name: "Ada",
    social: { kind: "best_friend", closeness: 62,
              circles: ["classmates"],
              memory: ["We got detention together."] } } } });
  mountPerson(root, h, detail());
  const texts = root.findText(() => true);
  assert.ok(texts.includes("Best friend"), "social kind label");
  assert.ok(texts.some((t) => t.includes("Relationship") &&
                             t.includes("62%")), "labelled Relationship bar");
  assert.ok(texts.some((t) => t.indexOf("Age 54") === 0), "age labelled");
  assert.ok(texts.includes("We got detention together."),
            "memory line verbatim");
  const block = root.findAll((n) => n._classes.has("v2p-social"))[0];
  const fill = block.findAll((n) => n._classes.has("v2p-bar-fill"))[0];
  assert.equal(fill.style.width, "62%", "closeness bar width");
});

test("M4: no social edge -> no social block (never faked)", () => {
  const root = new El("div");
  mountPerson(root, hooks([]), detail());
  assert.equal(root.findAll((n) => n._classes.has("v2p-social")).length, 0);
});

test("M4: social action tiles render with server reasons and dispatch",
     async () => {
  const root = new El("div");
  const calls = [];
  const specs = [
    { id: "social_befriend", label: "Befriend Ada", targetId: "p1",
      inputs: [], knownCosts: [], limits: ["once ever"],
      enabled: true, reason: "" },
    { id: "social_stand_up", label: "Stand up to Ada", targetId: "p1",
      inputs: [], knownCosts: [], limits: [],
      enabled: false, reason: "the bullying has not started" },
  ];
  mountPerson(root, hooks(calls), detail({ actions: specs }));
  const socialTiles = tiles(root);
  assert.equal(socialTiles.length, 2);
  assert.ok(socialTiles[0].textContent.includes("Befriend Ada"));
  assert.ok(socialTiles[0].textContent.includes("once ever"),
            "server limit shown");
  assert.equal(socialTiles[1].disabled, true, "server-disabled tile");
  assert.equal(socialTiles[1].getAttribute("aria-disabled"), "true");
  assert.ok(socialTiles[1].textContent.includes(
    "the bullying has not started"), "server reason visible");
  socialTiles[0].click();
  await Promise.resolve(); await Promise.resolve();
  assert.deepEqual(calls, [{ hook: "send", id: "social_befriend",
                             payload: {}, targetId: "p1" }]);
});

console.log(passed + " v2-person DOM tests passed" +
            (process.exitCode ? " (with failures)" : ""));
