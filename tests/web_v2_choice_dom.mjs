/* v2 Choice sheet DOM tests (Kimi B). Exercises web/static/v2/choice.js
 * against a minimal DOM stub: no fixed option count, disabled reasons,
 * knownEffects ($0 = "Free" as authored), naming input collection,
 * per-option inputs, collapse semantics, danger styling, frozen chip.
 * Run: node tests/web_v2_choice_dom.mjs */

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
    this.value = "";
    this.checked = false;
    this.required = false;
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
      toggle: (c, on) => { (on ?? !s.has(c)) ? s.add(c) : s.delete(c); },
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
  querySelectorAll(sel) {
    const pred = typeof sel === "function" ? sel
      : (n) => n.tagName === String(sel).toUpperCase();
    return this.findAll(pred);
  }
}

globalThis.document = {
  createElement: (tag) => new El(tag),
  createTextNode: (t) => { const n = new El("#text"); n._text = String(t);
                           return n; },
  getElementById: () => null,
  addEventListener: () => {},
  body: new El("body"),
};
globalThis.window = { addEventListener: () => {} };
Object.defineProperty(globalThis, "navigator",
                      { value: { onLine: true }, configurable: true });
// Avatar fetch fails softly in common.js.
globalThis.fetch = () => Promise.reject(new Error("no network in tests"));
globalThis.URL.createObjectURL = () => "blob:stub";
globalThis.console.warn = () => {};

const { mountChoiceSheet } = await import("../web/static/v2/choice.js");

function decision(over = {}) {
  return {
    id: "d1", kind: null, title: "A choice", text: "Something happened.",
    personIds: [],
    inputs: [],
    options: [
      { id: "a", label: "Do it", enabled: true, reason: "",
        knownEffects: ["Cost: $20"], inputs: [] },
      { id: "b", label: "Skip", enabled: true, reason: "",
        knownEffects: ["Free"], inputs: [] },
    ],
    ...over,
  };
}

function mount(d, calls, opts = {}) {
  const box = new El("div");
  const handle = mountChoiceSheet(box, {
    decision: d,
    people: [],
    playerAge: 27,
    onResolve: (optId, inputs) => calls.push({ resolve: optId, inputs }),
    onCollapse: () => calls.push({ collapse: true }),
    onError: (m) => calls.push({ error: m }),
    ...opts,
  });
  return { box, handle, calls };
}

const options = (box) =>
  box.findAll((n) => n.tagName === "BUTTON" &&
                   n._classes.has("v2c-option"));

let passed = 0;
function test(name, fn) {
  try { fn(); passed++; console.log("ok - " + name); }
  catch (e) {
    console.error("FAIL - " + name); console.error(e); process.exitCode = 1;
  }
}

test("no fixed option count: 2-option and 5-option decisions render", () => {
  const two = mount(decision(), []);
  assert.equal(options(two.box).length, 2);
  const five = mount(decision({ options: Array.from({ length: 5 },
    (_, i) => ({ id: "o" + i, label: "Option " + i, enabled: true,
                 reason: "", inputs: [] })) }), []);
  assert.equal(options(five.box).length, 5);
});

test("paid and $0 knownEffects render verbatim; $0 shows as authored Free",
     () => {
  const { box } = mount(decision(), []);
  const texts = box.findText(() => true);
  assert.ok(texts.includes("Cost: $20"));
  assert.ok(texts.includes("Free"));
});

test("disabled option is really disabled with the server reason visible",
     () => {
  const d = decision({ options: [
    { id: "a", label: "Adopt", enabled: false,
      reason: "Spouse disagrees", inputs: [] },
    { id: "b", label: "Not now", enabled: true, reason: "", inputs: [] },
  ] });
  const { box, calls } = mount(d, []);
  const dis = options(box)[0];
  assert.equal(dis.disabled, true);
  assert.equal(dis.getAttribute("aria-disabled"), "true");
  assert.ok(dis.textContent.includes("Spouse disagrees"));
  dis.click();
  assert.equal(calls.filter((c) => c.resolve).length, 0);
  // initial focus target is the first ENABLED option
  const h2 = mount(d, []);
  assert.equal(h2.handle.firstEnabled.textContent.includes("Not now"), true);
});

test("naming input: text field renders and its value reaches the resolve",
     () => {
  const d = decision({ kind: "baby_naming", inputs: [],
    options: [{ id: "name_child", label: "Name the baby", enabled: true,
                reason: "", inputs: [
                  { id: "name", type: "text", label: "Name",
                    required: true, min: 1, max: 24 }] }] });
  const { box, calls } = mount(d, []);
  const input = box.findAll((n) => n.tagName === "INPUT")[0];
  assert.ok(input, "text input rendered");
  input.value = "Robin";
  options(box)[0].click();
  assert.deepEqual(calls, [{ resolve: "name_child",
                             inputs: { name: "Robin" } }]);
});

test("required naming input blocks resolve with an error, no dispatch",
     () => {
  const d = decision({ options: [{ id: "name_child",
    label: "Name the baby", enabled: true, reason: "", inputs: [
      { id: "name", type: "text", label: "Name", required: true,
        min: 1, max: 24 }] }] });
  const { box, calls } = mount(d, []);
  options(box)[0].click();   // empty required name
  assert.equal(calls.filter((c) => c.resolve).length, 0);
  assert.ok(calls.some((c) => c.error && c.error.includes("required")));
});

test("per-option inputs collect only for the chosen option", () => {
  const d = decision({ options: [
    { id: "a", label: "Pay", enabled: true, reason: "", inputs: [
      { id: "amount", type: "number", label: "Amount", required: true,
        min: 1, max: 100 }] },
    { id: "b", label: "Refuse", enabled: true, reason: "", inputs: [] },
  ] });
  const { box, calls } = mount(d, []);
  const amt = box.findAll((n) => n.tagName === "INPUT")[0];
  amt.value = "50";
  options(box)[0].click();
  assert.deepEqual(calls, [{ resolve: "a", inputs: { amount: 50 } }]);
});

test("one resolve in flight: clicking freezes every option", () => {
  const { box, calls } = mount(decision(), []);
  const opts = options(box);
  opts[0].click();
  assert.ok(opts.every((o) => o.disabled), "all options frozen after click");
  assert.equal(calls.filter((c) => c.resolve).length, 1);
});

test('"Decide later" collapses — it never resolves', () => {
  const { box, calls } = mount(decision(), []);
  const later = box.findAll((n) => n.tagName === "BUTTON" &&
                                  n._classes.has("v2c-later"))[0];
  later.click();
  assert.deepEqual(calls, [{ collapse: true }]);
});

test("danger kind styles the sheet", () => {
  const box = new El("div");
  mountChoiceSheet(box, {
    decision: decision({ kind: "surrender_final" }),
    people: [], onResolve: () => {}, onCollapse: () => {},
  });
  assert.equal(box._classes.has("danger"), true);
});

test("frozen chip: AGE + display_category; age only when category null; "
     + "no synthesis without either", () => {
  const withCat = mount(decision({ displayCategory: "family" }), []);
  assert.ok(withCat.box.findText((t) => t === "AGE 27 · FAMILY").length >= 1);
  const noCat = mount(decision({ displayCategory: null }), []);
  assert.ok(noCat.box.findText((t) => t === "AGE 27").length >= 1);
  const bare = mount(decision({ displayCategory: null }), [], { playerAge: null });
  assert.equal(bare.box.findAll((n) => n._classes.has("v2c-kind")).length, 0);
});

test("unmount clears the box and the danger class", () => {
  const box = new El("div");
  const h = mountChoiceSheet(box, {
    decision: decision({ kind: "surrender_final" }),
    people: [], onResolve: () => {}, onCollapse: () => {},
  });
  h.unmount();
  assert.equal(box.children.length, 0);
  assert.equal(box._classes.has("danger"), false);
});

console.log(passed + " v2-choice DOM tests passed" +
            (process.exitCode ? " (with failures)" : ""));
