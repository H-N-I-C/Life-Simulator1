/* v2 Clubs DOM tests (Kimi B, M4 §UI). Exercises
 * web/static/features/social/clubs.js (mountClubs) against a minimal
 * DOM stub with no server, using the FROZEN example shapes from
 * docs/collab/M4/02-contracts.md. Enabled/disabled states and reasons
 * come from the advertised specs verbatim; the module computes nothing.
 * Run: node tests/web_v2_clubs_dom.mjs */

import assert from "node:assert/strict";
import { mountClubs } from "../web/static/features/social/clubs.js";

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

const DATA = {
  schema_version: 1,
  clubs: [
    { club_id: "chess_club", label: "Chess club", category: "school",
      member: true, role: "member", meets_label: "Tuesdays after class",
      cost_per_year: 0,
      actions: [{ id: "club_leave", label: "Leave the club",
                  category: "social", inputs: [],
                  known_costs: [], limits: [],
                  enabled: true, reason: null }] },
    { club_id: "harbour_run_club", label: "Harbour Runners",
      category: "sport", member: false, role: null,
      meets_label: "Saturday mornings", cost_per_year: 50,
      adults_only: true,
      actions: [{ id: "club_join", label: "Join ($50/year)",
                  category: "social", inputs: [],
                  known_costs: [{ kind: "money", "amount": 50 }],
                  limits: [],
                  enabled: false,
                  reason: "you must be 18 or older to join" }] },
    { club_id: "photo_club", label: "Photography club",
      category: "hobby", member: false, role: null,
      meets_label: "Thursday evenings", cost_per_year: 20,
      actions: [{ id: "club_join", label: "Join ($20/year)",
                  category: "social", inputs: [],
                  known_costs: [{ kind: "money", amount: 20 }],
                  limits: [],
                  enabled: true, reason: null }] },
  ],
};

{
  const root = new El("div");
  const calls = [];
  const hooks = { sendAction: (...a) => calls.push(a) };
  const h = mountClubs(root, hooks, DATA);
  const text = root.textContent;

  assert.ok(text.includes("Clubs & teams"), "card title");
  const rows = root.query((n) => n._classes &&
                                  n._classes.has("v2cl-club"));
  assert.equal(rows.length, 3);
  assert.equal(rows[0].getAttribute("data-club-id"), "chess_club");

  // member club: badge + meets + Free + leave dispatch
  assert.ok(rows[0].textContent.includes("Member"), "member badge");
  assert.ok(rows[0].textContent.includes("Tuesdays after class"),
            "meets label");
  assert.ok(rows[0].textContent.includes("Free"), "free club");
  const leave = rows[0].query((n) => n.tagName === "BUTTON")[0];
  leave.click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["club_leave", { club_id: "chess_club" }],
                   "leave dispatches frozen payload");

  // adults-only club: 18+ badge, disabled join with the server reason
  assert.ok(rows[1].textContent.includes("18+"), "adults-only badge");
  const joinDisabled = rows[1].query((n) => n.tagName === "BUTTON")[0];
  assert.equal(joinDisabled.disabled, true);
  assert.equal(joinDisabled.getAttribute("aria-disabled"), "true");
  assert.ok(joinDisabled.textContent.includes("you must be 18 or older"),
            "server refusal reason shown");

  // joinable club: real cost + enabled join dispatch
  assert.ok(rows[2].textContent.includes("$20 / year"), "annual cost");
  const join = rows[2].query((n) => n.tagName === "BUTTON")[0];
  assert.equal(join.disabled, false);
  join.click();
  assert.deepEqual(calls.at(-1).slice(0, 2),
                   ["club_join", { club_id: "photo_club" }],
                   "join dispatches frozen payload");

  // honest absence: no clubs -> no fabricated card
  const root2 = new El("div");
  mountClubs(root2, hooks, { clubs: [] });
  assert.equal(root2.children.length, 0, "empty clubs render nothing");
  const root3 = new El("div");
  mountClubs(root3, hooks, null);
  assert.equal(root3.children.length, 0, "null social renders nothing");

  // update + unmount
  h.update({ clubs: [DATA.clubs[0]] });
  assert.equal(root.query((n) => n._classes &&
                            n._classes.has("v2cl-club")).length, 1);
  h.unmount();
  assert.equal(root.children.length, 0);
  console.log("clubs DOM: all assertions passed");
}
