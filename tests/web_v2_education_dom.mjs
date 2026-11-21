/* v2 Education DOM tests (Kimi B, M3). Exercises
 * web/static/features/education/education.js (mountEducation handle)
 * against a minimal DOM stub with no server. The module is pure
 * presentation on the frozen §Shell mount API and the §Education
 * projection (advertised == dispatched: eligible/enabled come from the
 * server and the dispatch payloads are the frozen action IDs).
 * Run: node tests/web_v2_education_dom.mjs */

import assert from "node:assert/strict";
import { mountEducation } from "../web/static/features/education/education.js";

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
  enrollment: {
    enrollment_id: "e_1", program_id: "care_diploma",
    program_label: "Care diploma", progress: 1, duration_years: 2,
    status: "active", funding: "student_loan",
    tuition_per_year: 3500, fee_per_year: 0, studied_this_year: false,
    attempts: 0,
  },
  programs: [
    { program_id: "care_diploma", label: "Care diploma",
      credential_id: "care_diploma_cert", credential_label: "Care diploma",
      duration_years: 2, tuition_per_year: 3500, fee_per_year: 0,
      total_cost: 7000, study_skill: "care", apprentice_role: null,
      requirements: { min_age: 17, secondary_met: true, min_smarts: null,
        qualifications: [] },
      funding_options: ["self", "student_loan", "parents", "scholarship"],
      eligible: false,
      reason: "you are already enrolled in a program" },
    { program_id: "app_electrical", label: "Electrical apprenticeship",
      credential_id: "electrical_trade_cert",
      credential_label: "Electrical trade certificate",
      duration_years: 4, tuition_per_year: 0, fee_per_year: 400,
      total_cost: 1600, study_skill: "practical",
      apprentice_role: { role_id: "app_electrician", employer_id: "voltworks" },
      requirements: { min_age: 17, secondary_met: true, min_smarts: null,
        qualifications: [] },
      funding_options: ["self"],
      eligible: false,
      reason: "you are already enrolled in a program" },
    { program_id: "nursing_bachelors", label: "Nursing bachelor's",
      credential_id: "nursing_degree",
      credential_label: "Bachelor of Nursing",
      duration_years: 4, tuition_per_year: 8500, fee_per_year: 0,
      total_cost: 34000, study_skill: "care", apprentice_role: null,
      requirements: { min_age: 18, secondary_met: true, min_smarts: null,
        qualifications: [{ id: "care_diploma_cert", label: "Care diploma",
                           met: true }] },
      funding_options: ["self", "student_loan"],
      eligible: false,
      reason: "you are already enrolled in a program" },
  ],
  qualifications: [],
  history: [],
};

const FREE_DATA = {
  enrollment: null,
  programs: [
    { program_id: "business_bachelors", label: "Business bachelor's",
      credential_id: "business_degree",
      credential_label: "Bachelor of Business",
      duration_years: 4, tuition_per_year: 9000, fee_per_year: 0,
      total_cost: 36000, study_skill: "organization",
      apprentice_role: null,
      requirements: { min_age: 17, secondary_met: true, min_smarts: 40,
        qualifications: [] },
      funding_options: ["self", "student_loan"],
      eligible: false,
      reason: "this program asks for smarts 40 or higher" },
    { program_id: "app_electrical", label: "Electrical apprenticeship",
      credential_id: "electrical_trade_cert",
      credential_label: "Electrical trade certificate",
      duration_years: 4, tuition_per_year: 0, fee_per_year: 400,
      total_cost: 1600, study_skill: "practical",
      apprentice_role: { role_id: "app_electrician", employer_id: "voltworks" },
      requirements: { min_age: 17, secondary_met: true, min_smarts: null,
        qualifications: [] },
      funding_options: ["self"],
      eligible: true, reason: null },
  ],
  qualifications: [{ qualification_id: "care_diploma_cert",
                     label: "Care diploma", program_id: "care_diploma",
                     awarded_turn: "t_000030" }],
  history: [{ enrollment_id: "e_0", program_id: "care_diploma",
              program_label: "Care diploma", status: "left", progress: 1,
              started_turn: "t_000028" }],
};

/* ---------- tests: enrolled view ---------- */
{
  const root = new El("div");
  const calls = [];
  const hooks = { sendAction: (...a) => calls.push(a) };
  const h = mountEducation(root, hooks, DATA);

  const text = root.textContent;
  assert.ok(text.includes("Care diploma"), "enrollment program shown");
  assert.ok(text.includes("Student loan"), "funding label shown");
  assert.ok(text.includes("Year 1 of 2"), "progress line shown");
  assert.ok(text.includes("$3,500"), "tuition formatted");
  assert.ok(text.includes("Programs"), "catalog shown");
  assert.ok(text.includes("Electrical apprenticeship"), "apprenticeship shown");
  assert.ok(text.includes("Paid apprenticeship"), "apprenticeship note");
  assert.ok(text.includes("you are already enrolled in a program"),
            "server reason shown");

  const study = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent.startsWith("Study hard"))[0];
  study.click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["education_study", {}], "study dispatch");

  const leave = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent.startsWith("Leave"))[0];
  leave.click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["education_leave", {}], "leave dispatch");

  const enrollBtns = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent === "Enroll");
  assert.equal(enrollBtns.length, 3);
  for (const b of enrollBtns) {
    assert.equal(b.disabled, true, "enroll disabled while enrolled");
    assert.equal(b.getAttribute("aria-disabled"), "true");
  }

  // update: no enrollment → eligible program becomes enrollable
  h.update(FREE_DATA);
  const t2 = root.textContent;
  assert.ok(t2.includes("Not enrolled in a program"), "empty state");
  assert.ok(t2.includes("Care diploma"), "qualification listed");
  assert.ok(t2.includes("Past enrollments"), "history listed");
  assert.ok(t2.includes("this program asks for smarts 40 or higher"),
            "ineligible reason shown");
  const enrollBtns2 = root.query((n) => n.tagName === "BUTTON" &&
    n.textContent === "Enroll");
  assert.equal(enrollBtns2[0].disabled, true, "smarts-gated disabled");
  assert.equal(enrollBtns2[1].disabled, false, "apprenticeship enrollable");
  enrollBtns2[1].click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["education_enroll", { program_id: "app_electrical" }],
                   "enroll dispatches frozen payload");

  // unmount clears
  h.unmount();
  assert.equal(root.children.length, 0);
  console.log("education DOM: all assertions passed");
}
