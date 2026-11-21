/* v2 Home DOM tests (Kimi B). Exercises web/static/v2/home.js against a
 * minimal DOM stub with no server. The Age button and stats dock moved into
 * the shell chrome (feel-pass F3/F7) — those behaviors are tested in
 * web_v2_shell_dom.mjs. This module only owns the ended-life note.
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

const tests = [];
function test(name, fn) { tests.push({ name, fn }); }

test("alive life: no dead note", () => {
  const root = new El("div");
  mountHome(root, { getState: () => state() });
  assert.equal(root.findText((t) => t.includes("life has ended")).length, 0);
});

test("dead life: honest ended-life note", () => {
  const root = new El("div");
  mountHome(root, { getState: () => state({ status: "dead" }) });
  assert.equal(root.findAll((n) => n._classes.has("v2h-dead"))[0].hidden,
               false);
  assert.ok(root.findText((t) => t.includes("life has ended")).length >= 1);
});

test("alive → dead update reveals the note", () => {
  const root = new El("div");
  const h = mountHome(root, { getState: () => state() });
  h.update(state({ status: "dead" }));
  assert.equal(root.findAll((n) => n._classes.has("v2h-dead"))[0].hidden,
               false);
});

test("unmount clears the root", () => {
  const root = new El("div");
  const h = mountHome(root, { getState: () => state() });
  h.unmount();
  assert.equal(root.children.length, 0);
  mountHome(root, { getState: () => state() });
  unmountHome(root);
  assert.equal(root.children.length, 0);
});

if (process.argv.includes("--async-failure-probe")) {
  test("awaited async failure probe", async () => {
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
