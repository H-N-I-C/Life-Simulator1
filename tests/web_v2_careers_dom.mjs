/* v2 Careers (Work) DOM tests (Kimi A, M3). Exercises
 * web/static/features/careers/careers.js (mountCareers handle) against a
 * minimal DOM stub with no server. The module is pure presentation on
 * frozen shell hooks.
 * Run: node tests/web_v2_careers_dom.mjs */

import assert from "node:assert/strict";
import { mountCareers } from "../web/static/features/careers/careers.js";

/* ---------- minimal DOM stub ---------- */
class El {
  constructor(tag) {
    this.tagName = tag.toUpperCase();
    this.children = [];
    this.attributes = {};
    this.listeners = {};
    this._text = "";
    this.disabled = false;
    this.hidden = false;
    this.title = "";
    this._classes = new Set();
    this.style = {};
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
    this.children = [];
    this._text = String(v);
  }
  appendChild(n) {
    n.parent = this;
    if (n.tagName === "FRAGMENT") {
      for (const c of [...n.children]) {
        c.parent = this;
        this.children.push(c);
      }
      n.children = [];
      return n;
    }
    this.children.push(n);
    return n;
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
  classList = {
    _s: this._classes,
    add: (...cs) => cs.forEach((c) => this._classes.add(c)),
    remove: (...cs) => cs.forEach((c) => this._classes.delete(c)),
  };
  query(pred, out = []) {
    for (const c of this.children) {
      if (pred(c)) out.push(c);
      c.query(pred, out);
    }
    return out;
  }
}

globalThis.document = {
  createElement: (tag) => new El(tag),
  createDocumentFragment: () => new El("fragment"),
};

/* ---------- fixtures ---------- */
const DATA = {
  assignment: {
    role_id: "office_assistant", role_label: "Office assistant",
    employer_id: "meridian", employer_name: "Meridian Office Group",
    salary: 32573, hours: 38, role_tenure: 1, performance: 62,
    status: "active", promotions_to: ["coordinator"],
  },
  openings: [
    { role_id: "office_assistant", role_label: "Office assistant",
      path: "office", level: 1, employer_id: "meridian",
      employer_name: "Meridian Office Group",
      pay_min: 30000, pay_max: 36000, base_hours: 38,
      promotions_to: ["coordinator"],
      requirements: { min_age: 18,
        qualifications: [],
        skills: [{ id: "communication", value: 50, required: 10,
                   met: true }],
        roles: [] },
      eligible: true, reason: null },
    { role_id: "nurse", role_label: "Nurse", path: "healthcare",
      level: 3, employer_id: "staria", employer_name: "St Aria Hospital",
      pay_min: 58000, pay_max: 72000, base_hours: 38,
      promotions_to: ["senior_nurse"],
      requirements: { min_age: 21,
        qualifications: [{ id: "nursing_bachelor", met: false }],
        skills: [{ id: "care", value: 5, required: 45, met: false }],
        roles: [] },
      eligible: false,
      reason: "you need the nursing_bachelor qualification" },
  ],
  history: [{ role_id: "care_assistant", role_label: "Care assistant",
              employer_id: "bluegum", employer_name: "Bluegum Care Homes",
              salary: 30000, from_turn: "t_1", to_turn: "t_2",
              reason: "left" }],
};

function collectText(root) {
  return root.textContent;
}

/* ---------- tests ---------- */
{
  const root = new El("div");
  const calls = [];
  const hooks = { sendAction: (...a) => calls.push(a) };
  const h = mountCareers(root, hooks, DATA);

  const text = collectText(root);
  assert.ok(text.includes("Office assistant"), "assignment role shown");
  assert.ok(text.includes("Meridian Office Group"), "employer shown");
  assert.ok(text.includes("$32,573"), "salary formatted");
  assert.ok(text.includes("$32,573 / year"), "hero salary line");
  assert.ok(text.includes("Job openings"), "openings section");
  assert.ok(text.includes("you need the nursing_bachelor qualification"),
            "ineligible reason shown");
  // M3c hero card: performance bar reflects the projection value
  const perfFill = root.query((n) =>
    n._classes && n._classes.has("v2cw-perf-fill"))[0];
  assert.ok(perfFill, "performance bar rendered");
  assert.equal(perfFill.style.width, "62%", "bar width = performance");

  const applyBtns = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent.startsWith("Apply"));
  assert.equal(applyBtns.length, 2);
  assert.equal(applyBtns[0].disabled, false, "eligible apply enabled");
  assert.equal(applyBtns[1].disabled, true, "ineligible apply disabled");
  assert.equal(applyBtns[1].getAttribute("aria-disabled"), "true");

  applyBtns[0].click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["careers_apply", { role_id: "office_assistant",
                                      employer_id: "meridian" }],
                   "apply dispatches frozen payload");

  const promo = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent.includes("Seek promotion"))[0];
  promo.click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["careers_promote", { role_id: "coordinator" }]);

  const resign = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent.startsWith("Resign"))[0];
  resign.click();
  assert.deepEqual(calls.at(-1).slice(0, 2), ["careers_resign", {}]);

  h.update({ ...DATA, assignment: {
    ...DATA.assignment, promotions_to: [],
    promotion_reason: "No internal promotion here — change employer",
  } });
  assert.equal(root.query((n) => n.tagName === "BUTTON" &&
    n.textContent.includes("Seek promotion")).length, 0,
    "unavailable internal promotion is not an enabled hero control");
  assert.ok(collectText(root).includes("change employer"),
            "hero retains the server's advancement reason");

  // update: resign clears the assignment
  h.update({ ...DATA, assignment: null, history: DATA.history });
  assert.ok(collectText(root).includes("Not employed right now"));

  // unmount clears
  h.unmount();
  assert.equal(root.children.length, 0);
  console.log("careers DOM: all assertions passed");
}

/* ---------- C3: alternative routes (codex m3c review) ---------- */
function routeReq(routeMet, qualsMet, roleMet) {
  return {
    min_age: 18,
    qualifications: [], skills: [], roles: [],
    shared: { min_age: 18,
              skills: [{ id: "practical", value: 45, required: 40,
                         met: true }] },
    routes: [
      { label: "Electrical trade certificate + experience as " +
               "Apprentice electrician",
        met: routeMet === "default",
        qualifications: [{ id: "electrical_trade_cert",
                           label: "Electrical trade certificate",
                           met: qualsMet }],
        roles: [{ id: "app_electrician", label: "Apprentice electrician",
                  min_tenure: null, recorded_years: null, met: roleMet }] },
      { label: "Bachelor of Business",
        met: routeMet === "alt",
        qualifications: [{ id: "business_degree",
                           label: "Bachelor of Business",
                           met: qualsMet }],
        roles: [] },
    ],
  };
}

function routeOpening(routeMet, eligible, reason, qualsMet, roleMet) {
  return { role_id: "electrician", role_label: "Electrician",
           path: "electrical", level: 2, employer_id: "voltworks",
           employer_name: "Voltworks Electrical",
           pay_min: 52000, pay_max: 64000, base_hours: 38,
           promotions_to: ["senior_electrician"],
           requirements: routeReq(routeMet, qualsMet, roleMet),
           eligible, reason, reason_kind: reason ? "requirements" : null };
}

{
  const root = new El("div");
  const calls = [];
  const hooks = { sendAction: (...a) => calls.push(a) };
  const data = { assignment: null, history: [], openings: [
    // default route passes -> enabled Apply
    routeOpening("default", true, null, true, true),
    // alternative route passes -> enabled Apply, default shown unmet
    routeOpening("alt", true, null, false, false),
    // neither route passes -> disabled Apply with the server reason
    routeOpening("neither", false,
                 "you need Electrical trade certificate + experience as " +
                 "Apprentice electrician or Bachelor of Business",
                 false, false),
  ] };
  mountCareers(root, hooks, data);
  const text = root.textContent;

  // routes render as labelled ALTERNATIVES, ids never shown
  assert.ok(text.includes("Bachelor of Business"), "alt label shown");
  assert.ok(text.includes("Electrical trade certificate"),
            "default route label shown");
  assert.ok(text.includes("Apprentice electrician"), "role label shown");
  assert.ok(!text.includes("business_degree"), "no raw qual id");
  assert.ok(!text.includes("electrical_trade_cert"), "no raw qual id (2)");
  assert.ok(!text.includes("app_electrician"), "no raw role id");

  // the OR divider sits between the two routes (per opening)
  const ors = root.query((n) => n._classes &&
                           n._classes.has("v2cw-route-or"));
  assert.equal(ors.length, 3, "one 'or' divider per opening");

  // shared requirements render ONCE (not repeated inside each route)
  const sharedLines = root.query((n) => n._classes &&
                                 n._classes.has("v2cw-shared"));
  assert.equal(sharedLines.length, 3,
               "one shared-requirements line per opening");
  assert.ok(text.includes("Needs age 18+ · ✓ practical 45/40"),
            "shared age + skill with values, once");

  // route met/unmet styling comes from the server's route.met
  const metRoutes = root.query((n) => n._classes &&
                                 n._classes.has("v2cw-route-met"));
  const unmetRoutes = root.query((n) => n._classes &&
                                   n._classes.has("v2cw-route-unmet"));
  assert.equal(metRoutes.length, 2, "default-pass + alt-pass routes met");
  assert.equal(unmetRoutes.length, 4, "the rest unmet");

  // eligibility is the SERVER's: apply buttons follow o.eligible only —
  // the module never re-derives it from the routes
  const applyBtns = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent === "Apply");
  assert.equal(applyBtns.length, 3);
  assert.equal(applyBtns[0].disabled, false, "default-pass apply enabled");
  assert.equal(applyBtns[1].disabled, false, "alt-pass apply enabled");
  assert.equal(applyBtns[2].disabled, true, "neither-route apply disabled");
  assert.equal(applyBtns[2].getAttribute("aria-disabled"), "true");
  assert.ok(!root.textContent.includes("you need Electrical trade certificate"),
            "requirement-only reason is not repeated beside the complete list");
  assert.equal(applyBtns[2].title, data.openings[2].reason,
               "the authoritative reason remains on the disabled control");
  const alt = root.query((n) => n._classes &&
                               n._classes.has("v2cw-route"))[1];
  assert.equal((alt.textContent.match(/Bachelor of Business/g) || []).length, 1,
               "a single-credential route is named only once");

  // no-client-eligibility pin: server eligible wins even if a client
  // looking at routes alone would disagree
  const tricky = { assignment: null, history: [], openings: [
    routeOpening("neither", true, null, false, false)] };
  const root2 = new El("div");
  mountCareers(root2, hooks, tricky);
  const apply2 = root2.query((n) => n.tagName === "BUTTON" &&
                             n.textContent === "Apply")[0];
  assert.equal(apply2.disabled, false,
               "module never overrides the server's eligibility");

  console.log("careers routes DOM: all assertions passed");
}
