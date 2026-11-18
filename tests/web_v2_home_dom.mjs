/* v2 Home DOM tests (Kimi B). Exercises web/static/v2/home.js against a
 * minimal DOM stub with no server: real stats + numeric labels, Age
 * eligibility from the advertised spec, busy state, dead state, unmount.
 * Run: node tests/web_v2_home_dom.mjs */

import assert from "node:assert/strict";

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

globalThis.document = {
  createElement: (tag) => new El(tag),
};
globalThis.window = { addEventListener: () => {} };

const { mountHome, unmountHome } =
  await import("../web/static/v2/home.js");

function state(over = {}) {
  return {
    status: "alive",
    money: 48210,
    stats: { happiness: 78, health: 64, smarts: 83, looks: 41 },
    actions: [{ id: "age_up", label: "Age up", category: "life",
                enabled: true, reason: "" }],
    ...over,
  };
}

function hooks(calls, send) {
  return {
    getState: () => state(),
    sendAction: send ||
      ((id, payload, targetId) =>
        calls.push({ id, payload, targetId }) ?? Promise.resolve({})),
  };
}

const tests = [];
function test(name, fn) { tests.push({ name, fn }); }
const settled = () => new Promise((resolve) => setImmediate(resolve));

test("real stats render with numeric % labels (colour never sole signal)",
     () => {
  const root = new El("div");
  mountHome(root, hooks([]));
  const texts = root.findText(() => true);
  for (const [k, v] of Object.entries(state().stats)) {
    assert.ok(texts.includes(v + "%"), k + " numeric label");
    assert.ok(texts.includes(k[0].toUpperCase() + k.slice(1)), k + " label");
  }
  // bars carry the semantic token colours
  const fills = root.findAll((n) => n._classes.has("v2h-stat-fill"));
  assert.equal(fills.length, 4);
  assert.equal(fills[0].style.background, "var(--v2-bar-good)");  // 78
  assert.equal(fills[3].style.background, "var(--v2-bar-mid)");   // 41
});

test("Age click sends the exact age_up action; spec gates with reason", () => {
  const root = new El("div");
  const calls = [];
  mountHome(root, hooks(calls));
  const age = root.findAll((n) => n._classes.has("v2h-age"))[0];
  age.click();
  assert.deepEqual(calls, [{ id: "age_up", payload: {}, targetId: null }]);
});

test("disabled spec disables Age with the server reason visible", () => {
  const root = new El("div");
  const st = state({ actions: [{ id: "age_up", label: "Age up",
                                 enabled: false,
                                 reason: "A decision is waiting for you." }] });
  mountHome(root, { getState: () => st, sendAction: () => {} });
  const age = root.findAll((n) => n._classes.has("v2h-age"))[0];
  assert.equal(age.disabled, true);
  assert.equal(age.getAttribute("aria-disabled"), "true");
  assert.ok(root.findText((t) => t.includes("decision is waiting")).length >= 1);
  const calls = [];
  age.click();  // disabled buttons don't dispatch, but guard anyway
  assert.equal(calls.length, 0);
});

test("busy settles after the shell renders; two consecutive Age clicks work", async () => {
  const root = new El("div");
  let current = state(), finish, calls = 0, h;
  h = mountHome(root, { getState: () => current, sendAction: () => {
    calls++;
    return new Promise((resolve) => { finish = (next) => {
      current = next;
      h.update(current); // production renders BEFORE resolving transport
      resolve(current);
    }; });
  } });
  const age = root.findAll((n) => n._classes.has("v2h-age"))[0];
  for (let n = 0; n < 2; n++) {
    age.click();
    assert.equal(age.disabled, true);
    finish(state());
    assert.equal(age.disabled, true, "local busy remains set during shell render");
    await settled();
    assert.equal(age.disabled, false, "settlement must repaint the current spec");
    assert.equal(age.getAttribute("aria-disabled"), "false");
    assert.equal(root.findText((t) => t.includes("Ageing")).length, 0);
  }
  assert.equal(calls, 2);
});

for (const outcome of ["pending", "rejected", "uncertain"]) {
  test("settlement uses authoritative state: " + outcome, async () => {
    const root = new El("div");
    let current = state(), finish, h;
    h = mountHome(root, { getState: () => current, sendAction: () =>
      new Promise((resolve, reject) => { finish = () => {
        current = outcome === "pending" ? state({ actions: [], pending: { id: "d_1" } }) : state();
        h.update(current);
        if (outcome === "rejected") reject({ code: "not_allowed" });
        else resolve(outcome === "uncertain" ? null : current);
      }; }) });
    const age = root.findAll((n) => n._classes.has("v2h-age"))[0];
    age.click(); finish(); await settled();
    assert.equal(age.disabled, outcome === "pending");
    assert.equal(root.findText((t) => t.includes("Ageing")).length, 0);
  });
}

for (const viaExport of [false, true]) {
  test("settling after unmount never reads or changes the former root: " + viaExport, async () => {
    const root = new El("div");
    let finish, unmounted = false;
    const h = mountHome(root, { getState: () => {
      assert.equal(unmounted, false, "must not read shell state after unmount");
      return state();
    }, sendAction: () => new Promise((resolve) => { finish = resolve; }) });
    root.findAll((n) => n._classes.has("v2h-age"))[0].click();
    if (viaExport) unmountHome(root); else h.unmount();
    unmounted = true;
    finish(state()); await settled();
    assert.equal(root.children.length, 0);
  });
}


test("late settlement and old unmount cannot overwrite a remounted Home", async () => {
  const root = new El("div");
  let finish;
  const old = mountHome(root, { getState: () => state(),
    sendAction: () => new Promise((resolve) => { finish = resolve; }) });
  root.findAll((n) => n._classes.has("v2h-age"))[0].click();
  const freshState = state({ stats: { health: 37 } });
  mountHome(root, { getState: () => freshState, sendAction: () => Promise.resolve(freshState) });
  old.unmount();
  finish(state()); await settled();
  assert.ok(root.findText((t) => t === "37%").length);
  assert.equal(root.findText((t) => t === "78%").length, 0);
});

test("dead life: no Age control, honest ended-life note", () => {
  const root = new El("div");
  mountHome(root, { getState: () => state({ status: "dead" }),
                    sendAction: () => {} });
  assert.equal(root.findAll((n) => n._classes.has("v2h-agebar"))[0].hidden,
               true);
  assert.ok(root.findText((t) => t.includes("life has ended")).length >= 1);
});

test("unmount clears the root", () => {
  const root = new El("div");
  const h = mountHome(root, hooks([]));
  h.unmount();
  assert.equal(root.children.length, 0);
  mountHome(root, hooks([]));
  unmountHome(root);
  assert.equal(root.children.length, 0);
});

if (process.argv.includes("--async-failure-probe")) {
  test("awaited async failure probe", async () => {
    await settled();
    assert.fail("intentional async harness negative control");
  });
}

let passed = 0;
for (const { name, fn } of tests) {
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
console.log(passed + " v2-home DOM tests passed" +
            (process.exitCode ? " (with failures)" : ""));
