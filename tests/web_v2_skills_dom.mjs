/* v2 Skills DOM tests (Kimi C, M3 R4). Exercises
 * web/static/features/skills/skills.js (mountSkills handle) against a
 * minimal DOM stub with no server. Pure presentation on frozen shell
 * hooks; the dispatch dialect is Codex's pinned one:
 * sendAction("train_skill", payload, targetId).
 * Run: node tests/web_v2_skills_dom.mjs */

import assert from "node:assert/strict";
import { mountSkills } from "../web/static/features/skills/skills.js";

/* ---------- minimal DOM stub ---------- */
class El {
  constructor(tag) {
    this.tagName = tag.toUpperCase();
    this.children = [];
    this.attributes = {};
    this.listeners = {};
    this._text = "";
    this.disabled = false;
    this.title = "";
    this._classes = new Set();
    this.style = {};
    this.parent = null;
  }
  get className() { return [...this._classes].join(" "); }
  set className(v) {
    this._classes = new Set(String(v).split(/\s+/).filter(Boolean));
  }
  get classList() {
    const s = this._classes;
    return {
      add: (c) => s.add(c),
      remove: (c) => s.delete(c),
      contains: (c) => s.has(c),
    };
  }
  get nextSibling() {
    if (!this.parent) return null;
    const i = this.parent.children.indexOf(this);
    return this.parent.children[i + 1] || null;
  }
  get textContent() {
    if (this.children.length) {
      return (this._text +
              this.children.map((c) => c.textContent).join(""));
    }
    return this._text;
  }
  set textContent(v) {
    this.children = [];
    this._text = String(v);
  }
  appendChild(n) {
    n.parent = this;
    this.children.push(n);
    return n;
  }
  insertBefore(node, ref) {
    node.parent = this;
    if (!ref) { this.children.push(node); return node; }
    const i = this.children.indexOf(ref);
    if (i < 0) this.children.push(node);
    else this.children.splice(i, 0, node);
    return node;
  }
  insertAdjacentElement(position, node) {
    if (position === "afterend") {
      const sib = this.nextSibling;
      this.parent.insertBefore(node, sib);
    }
    return node;
  }
  remove() {
    if (this.parent) {
      const i = this.parent.children.indexOf(this);
      if (i >= 0) this.parent.children.splice(i, 1);
      this.parent = null;
    }
  }
  addEventListener(type, fn) { (this.listeners[type] ||= []).push(fn); }
  click() { for (const fn of this.listeners.click || []) fn(); }
  setAttribute(k, v) { this.attributes[k] = String(v); }
  getAttribute(k) { return this.attributes[k]; }
  classListContains(c) { return this._classes.has(c); }
}

const documentStub = { createElement: (t) => new El(t) };
globalThis.document = documentStub;

/* ---------- fixture ---------- */
const META = {
  practical: { label: "Practical", description: "" },
  organization: { label: "Organization", description: "" },
  communication: { label: "Communication", description: "" },
  care: { label: "Care", description: "" },
};

function data() {
  return {
    schema_version: 1,
    values: { practical: 10, organization: 20, communication: 30, care: 40 },
    skills_meta: META,
    uses_this_year: { train_practical: 1 },
    training: [
      { id: "train_skill", target_id: "train_practical",
        label: "Practise hands-on skills", inputs: [],
        known_costs: [], limits: ["2 per year"], enabled: true,
        reason: null },
      { id: "train_skill", target_id: "train_communication",
        label: "Take a public speaking class", inputs: [],
        known_costs: [{ kind: "money", amount: 25 }],
        limits: ["2 per year"], enabled: false,
        reason: "this training costs $25 and you have $0" },
      { id: "train_skill", target_id: "train_evening_course",
        label: "Take an evening skills course",
        inputs: [{ id: "skill", type: "choice", label: "Skill",
                   required: true,
                   choices: ["practical", "organization", "communication",
                             "care"] }],
        known_costs: [{ kind: "money", "amount": 60 }],
        limits: ["1 per year"], enabled: true, reason: null },
    ],
  };
}

function trainingList() {
  return root.children[0].children
    .filter((n) => n.className === "v2sk-training")[0];
}
function chooserOf() {
  return trainingList().children.find((n) => n.className === "v2sk-chooser");
}

function makeHooks() {
  const calls = [];
  return {
    calls,
    sendAction(actionId, payload, targetId) {
      calls.push({ actionId, payload, targetId });
      return Promise.resolve({});
    },
  };
}

/* ---------- tests ---------- */
const root = new El("div");
const hooks = makeHooks();
const handle = mountSkills(root, hooks, data());
let failures = 0;
const queue = [];
function check(name, fn) { queue.push([name, fn]); }

check("renders the four skill rows with values", () => {
  const names = root.children[0].children
    .filter((n) => n.className === "v2sk-grid")[0].children
    .map((r) => r.children[0].children.map((s) => s.textContent).join(" "));
  assert.equal(names.length, 4);
  assert.ok(names.some((n) => n.includes("Practical") && n.includes("10/100")));
});

check("dispatchable dialect: every spec is train_skill + target_id", () => {
  const buttons = root.children[0].children
    .filter((n) => n.className === "v2sk-training")[0].children;
  assert.equal(buttons.length, 3);
});

check("plain option dispatches train_skill with its target_id", async () => {
  const buttons = root.children[0].children
    .filter((n) => n.className === "v2sk-training")[0].children;
  const plain = buttons.find((b) => b.textContent.includes("Practise"));
  assert.ok(plain);
  plain.click();
  await Promise.resolve();
  assert.deepEqual(hooks.calls.at(-1), {
    actionId: "train_skill", payload: {}, targetId: "train_practical" });
});

check("disabled option shows the server reason and does not dispatch", () => {
  const buttons = root.children[0].children
    .filter((n) => n.className === "v2sk-training")[0].children;
  const dis = buttons.find((b) => b.textContent.includes("public speaking"));
  assert.equal(dis.disabled, true);
  assert.equal(dis.getAttribute("aria-disabled"), "true");
  dis.click();
  assert.equal(hooks.calls.length, 1, "no dispatch from disabled button");
});

check("choose-skill opens a selector with all four skills", () => {
  const buttons = root.children[0].children
    .filter((n) => n.className === "v2sk-training")[0].children;
  const course = buttons.find((b) => b.textContent.includes("evening"));
  course.click();
  const chooser = chooserOf();
  assert.ok(chooser, "chooser must appear");
  const options = chooser.children.filter((n) =>
    n.className === "v2sk-choice");
  assert.equal(options.length, 4);
  assert.deepEqual(options.map((o) => o.getAttribute("data-skill")),
                   ["practical", "organization", "communication", "care"]);
  assert.ok(options.some((o) => o.textContent === "Care"));
});

check("choosing a skill dispatches {skill} with the course target", async () => {
  const chooser = chooserOf();
  const care = chooser.children.find(
    (o) => o.className === "v2sk-choice" &&
           o.getAttribute("data-skill") === "care");
  care.click();
  await Promise.resolve();
  assert.deepEqual(hooks.calls.at(-1), {
    actionId: "train_skill",
    payload: { skill: "care" },
    targetId: "train_evening_course" });
  assert.ok(!chooserOf(), "chooser closes after the choice");
});

check("clicking the course again toggles the chooser closed", () => {
  const buttons = root.children[0].children
    .filter((n) => n.className === "v2sk-training")[0].children;
  const course = buttons.find((b) => b.textContent.includes("evening"));
  course.click();  // open
  assert.ok(chooserOf());
  course.click();  // close
  assert.ok(!chooserOf());
});

check("update() re-renders and unmount() clears", () => {
  const d = data();
  d.values.practical = 55;
  handle.update(d);
  assert.ok(root.textContent.includes("55/100"));
  handle.unmount();
  assert.equal(root.children.length, 0);
  assert.ok(!root.classListContains("v2sk-scope"));
});

(async () => {
  for (const [name, fn] of queue) {
    try { await fn(); console.log("ok - " + name); }
    catch (e) { failures++; console.error("FAIL - " + name + "\n  " + e.message); }
  }
  if (failures) { console.error(failures + " failure(s)"); process.exit(1); }
  console.log("all skills DOM tests passed");
})();
