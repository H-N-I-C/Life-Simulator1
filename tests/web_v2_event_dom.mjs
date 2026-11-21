/* v2 Event modal + outcome popup DOM tests (Kimi C, F5/F6). Exercises
 * web/static/v2/event.js against a minimal DOM stub: spoiler-free options
 * (knownEffects hidden, real costs/reasons kept), the OTHER person's
 * avatar, disabled semantics, busy → outcome popup flow with the actual
 * server deltas, and the null-outcome path.
 * Run: node tests/web_v2_event_dom.mjs */

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

const { mountEventModal } = await import("../web/static/v2/event.js");

function decision(over = {}) {
  return {
    id: "d1", kind: null, title: "A choice", text: "Something happened.",
    personIds: ["p_mom"],
    inputs: [],
    options: [
      { id: "a", label: "Do it", enabled: true, reason: "",
        knownEffects: ["-happiness (25-35)", "-mother relationship (6-10)"],
        knownCosts: [{ kind: "money", amount: 200, label: "Costs $200" }],
        inputs: [] },
      { id: "b", label: "Skip", enabled: true, reason: "",
        knownEffects: [], knownCosts: [], inputs: [] },
    ],
    ...over,
  };
}

const MOM = { id: "p_mom", name: "Ada Okafor", age: 54,
              appearance: { schema_version: 1, seed: 3 }, role: "mother" };

function mount(d, opts = {}) {
  const box = new El("div");
  const calls = [];
  const avatars = [];
  const handle = mountEventModal(box, {
    decision: d,
    people: [MOM],
    playerAge: 27,
    onResolve: opts.onResolve ||
      ((optId, inputs) => calls.push({ resolve: optId, inputs })),
    onCollapse: () => calls.push({ collapse: true }),
    onError: (m) => calls.push({ error: m }),
    onClose: () => calls.push({ close: true }),
    renderAvatar: (el_, appearance, age, size) =>
      avatars.push({ appearance, age, size }),
    ...opts,
  });
  return { box, handle, calls, avatars };
}

const options = (box) =>
  box.findAll((n) => n.tagName === "BUTTON" &&
                   n._classes.has("v2e-option"));

const tests = [];
let passed = 0;
function test(name, fn) { tests.push([name, fn]); }
const tick = () => Promise.resolve();

test("no fixed option count: 2-option and 5-option decisions render", () => {
  assert.equal(options(mount(decision()).box).length, 2);
  const five = decision({ options: Array.from({ length: 5 },
    (_, i) => ({ id: "o" + i, label: "Option " + i, enabled: true,
                 reason: "", inputs: [] })) });
  assert.equal(options(mount(five).box).length, 5);
});

test("F5: knownEffects spoilers are NOT rendered before choosing", () => {
  const { box } = mount(decision());
  const texts = box.findText(() => true).join("\n");
  assert.ok(!texts.includes("-happiness (25-35)"), "stat range hidden");
  assert.ok(!texts.includes("mother relationship"), "relation range hidden");
});

test("F5: real money costs and disabled reasons stay visible", () => {
  const d = decision({ options: [
    { id: "a", label: "Pay", enabled: false,
      reason: "You can't afford it",
      knownCosts: [{ kind: "money", amount: 200, label: "Costs $200" }],
      inputs: [] },
    { id: "b", label: "Walk away", enabled: true, reason: "", inputs: [] },
  ] });
  const { box, calls } = mount(d);
  const dis = options(box)[0];
  assert.equal(dis.disabled, true);
  assert.equal(dis.getAttribute("aria-disabled"), "true");
  assert.ok(dis.textContent.includes("Costs $200"));
  assert.ok(dis.textContent.includes("You can't afford it"));
  dis.click();
  assert.equal(calls.filter((c) => c.resolve).length, 0);
  // focus target is the first ENABLED option
  assert.equal(mount(d).handle.firstEnabled.textContent
                 .includes("Walk away"), true);
});

test("F6: the OTHER person's avatar renders, not the player's", () => {
  const { box, avatars } = mount(decision());
  assert.equal(avatars.length, 1);
  assert.deepEqual(avatars[0].appearance, MOM.appearance);
  assert.equal(avatars[0].age, 54);
  assert.ok(box.findAll((n) => n._classes.has("v2e-avatar")).length >= 1);
  // nobody else involved → no avatar block at all
  const solo = mount(decision(), { people: [] });
  assert.equal(solo.avatars.length, 0);
  assert.equal(solo.box.findAll((n) => n._classes.has("v2e-avatar"))
                 .length, 0);
});

test("frozen chip: AGE + display_category; age only when category null", () => {
  assert.ok(mount(decision({ displayCategory: "family" })).box
    .findText((t) => t === "AGE 27 · FAMILY").length >= 1);
  assert.ok(mount(decision({ displayCategory: null })).box
    .findText((t) => t === "AGE 27").length >= 1);
  const bare = mount(decision(), { playerAge: null });
  assert.equal(bare.box.findAll((n) => n._classes.has("v2e-kind")).length, 0);
});

test("choosing shows the ACTUAL deltas from the resolve outcome", async () => {
  const outcome = {
    text: "You told your mother the truth. She went quiet.",
    changes: { happiness: -12, money: -200, relationship: 8 },
    person_ids: ["p_mom"],
  };
  const { box, calls } = mount(decision(), {
    onResolve: (optId, inputs) => {
      calls.push({ resolve: optId, inputs });
      return Promise.resolve(outcome);
    },
  });
  options(box)[0].click();
  // busy: every option frozen while the resolve is in flight
  assert.ok(options(box).every((o) => o.disabled));
  await tick(); await tick();
  const texts = box.findText(() => true).join("\n");
  assert.ok(texts.includes("You told your mother the truth. She went quiet."));
  assert.ok(texts.includes("Happiness"));
  assert.ok(texts.includes("-12"));
  assert.ok(texts.includes("-$200"));
  assert.ok(texts.includes("+8"));
  assert.ok(texts.includes("Relationship"));
  // the spoiler range never leaks even here
  assert.ok(!texts.includes("25-35"));
  const up = box.findAll((n) => n._classes.has("v2e-delta-up"));
  const down = box.findAll((n) => n._classes.has("v2e-delta-down"));
  assert.equal(up.length, 1);
  assert.equal(down.length, 2);
  // Continue closes via onClose
  const cont = box.findAll((n) => n.tagName === "BUTTON" &&
                                   n._classes.has("v2e-continue"))[0];
  cont.click();
  assert.deepEqual(calls.at(-1), { close: true });
});

test("null/absent outcome → no popup, onClose() immediately", async () => {
  const { box, calls } = mount(decision(), {
    onResolve: () => Promise.resolve(null),
  });
  options(box)[0].click();
  await tick(); await tick();
  assert.equal(box.findAll((n) => n._classes.has("v2e-outcome")).length, 0);
  assert.deepEqual(calls.at(-1), { close: true });
  // empty text AND empty changes → also no popup
  const e2 = mount(decision(), {
    onResolve: () => Promise.resolve({ text: "", changes: {} }),
  });
  options(e2.box)[0].click();
  await tick(); await tick();
  assert.deepEqual(e2.calls.at(-1), { close: true });
});

test("sync onResolve return value is tolerated", async () => {
  const { box, calls } = mount(decision(), {
    onResolve: () => ({ text: "Done.", changes: { smarts: 3 },
                         person_ids: [] }),
  });
  options(box)[0].click();
  await tick(); await tick();
  assert.ok(box.findText((t) => t === "+3").length >= 1);
  const cont = box.findAll((n) => n.tagName === "BUTTON" &&
                                   n._classes.has("v2e-continue"))[0];
  cont.click();
  assert.deepEqual(calls.at(-1), { close: true });
});

test("rejected resolve closes without a popup (shell owns the error)", async () => {
  const { box, calls } = mount(decision(), {
    onResolve: () => Promise.reject(Object.assign(
      new Error("stale"), { code: "stale_revision" })),
  });
  options(box)[0].click();
  await tick(); await tick();
  assert.equal(box.findAll((n) => n._classes.has("v2e-outcome")).length, 0);
  assert.deepEqual(calls.at(-1), { close: true });
});

test('"Decide later" collapses — it never resolves', () => {
  const { box, calls } = mount(decision());
  const later = box.findAll((n) => n.tagName === "BUTTON" &&
                                  n._classes.has("v2e-later"))[0];
  later.click();
  assert.deepEqual(calls, [{ collapse: true }]);
});

test("danger kind styles the card; unmount clears the box", () => {
  const box = new El("div");
  const h = mountEventModal(box, {
    decision: decision({ kind: "surrender_final" }),
    people: [], onResolve: () => {}, onCollapse: () => {}, onClose: () => {},
  });
  assert.equal(box._classes.has("danger"), true);
  h.unmount();
  assert.equal(box.children.length, 0);
  assert.equal(box._classes.has("danger"), false);
});

test("modalCancel child is preserved", () => {
  const box = new El("div");
  const cancel = new El("button");
  cancel.id = "modalCancel";
  box.appendChild(cancel);
  const h = mountEventModal(box, {
    decision: decision(),
    people: [], onResolve: () => {}, onCollapse: () => {}, onClose: () => {},
  });
  assert.equal(box.children.includes(cancel), true);
  h.unmount();
  assert.equal(box.children.includes(cancel), true);
});

test("outcome popup shows the other person's avatar too", async () => {
  const { box, avatars } = mount(decision(), {
    onResolve: () => Promise.resolve(
      { text: "It went fine.", changes: { looks: 5 }, person_ids: ["p_mom"] }),
  });
  const before = avatars.length;
  options(box)[0].click();
  await tick(); await tick();
  assert.equal(avatars.length, before + 1);
  assert.deepEqual(avatars.at(-1).appearance, MOM.appearance);
});

test("unmount during a pending resolve is a no-op", async () => {
  let resolveIt;
  const { box, calls, handle } = mount(decision(), {
    onResolve: () => new Promise((r) => { resolveIt = r; }),
  });
  options(box)[0].click();
  handle.unmount();
  resolveIt({ text: "Late.", changes: { health: 2 }, person_ids: [] });
  await tick(); await tick();
  assert.equal(calls.some((c) => c.close), false);
});

test("unlabelled structured costs survive adapter normalization", async () => {
  const { normalizeOption } = await import("../web/static/adapter.js");
  const normalized = normalizeOption({ id: "pay", label: "Pay", enabled: true,
    known_costs: [{ kind: "money", amount: 1234 }], known_effects: ["-health (2-4)"] });
  const { box } = mount(decision({ options: [normalized] }));
  assert.ok(options(box)[0].textContent.includes("Costs $1,234"));
  assert.ok(!options(box)[0].textContent.includes("[object Object]"));
  assert.ok(!options(box)[0].textContent.includes("health"));
});

test("legacy labelled costs show the amount and explicit zero is free", () => {
  const { box } = mount(decision({ options: [{ id: "pay", label: "Buy",
    knownCosts: [{ label: "Ticket", amount: 40 }, {kind:"money", amount:0}],
    inputs: [] }] }));
  assert.ok(options(box)[0].textContent.includes("Ticket · $40"));
  assert.ok(options(box)[0].textContent.includes("Free"));
});

test("relationship-only outcome shows actual named deltas without private IDs", async () => {
  const { box } = mount(decision(), { onResolve: () => ({ text: "", changes: {},
    relationship_changes: { p_mom: -4, secret_internal_id: 3 } }) });
  options(box)[0].click();
  await tick(); await tick();
  assert.ok(box.textContent.includes("Ada Okafor · Relationship"));
  assert.ok(box.textContent.includes("-4"));
  assert.ok(box.textContent.includes("+3"));
  assert.ok(!box.textContent.includes("secret_internal_id"));
});

test("player-first participants omit the own chip and retain the NPC", () => {
  const player = { ...MOM, id: "player", name: "Player", age: 10 };
  const { box, avatars } = mount(decision(), { playerId: "player", people: [player, MOM] });
  assert.equal(avatars[0].age, MOM.age);
  assert.ok(!box.textContent.includes("Player"));
  assert.ok(box.textContent.includes(MOM.name));
  const solo = mount(decision(), { playerId: "player", people: [player] });
  assert.equal(solo.avatars.length, 0);
  assert.ok(!solo.box.textContent.includes("Player"));
  assert.equal(solo.box.findAll((n) => n._classes.has("v2e-people")).length, 0);
});

test("rapid duplicate choice taps resolve only once and cannot collapse in flight", async () => {
  let finish;
  const { box, calls } = mount(decision(), { onResolve: () => {
    calls.push({ resolve: true }); return new Promise((r) => { finish = r; });
  } });
  const button = options(box)[0];
  button.click(); button.click();
  box.findAll((n) => n._classes.has("v2e-later"))[0].click();
  assert.equal(calls.filter((c) => c.resolve).length, 1);
  assert.equal(calls.filter((c) => c.collapse).length, 0);
  finish({ text: "Done", changes: {} });
  await tick(); await tick();
});

for (const [name, fn] of tests) {
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

console.log(passed + " v2-event DOM tests passed" +
            (process.exitCode ? " (with failures)" : ""));
