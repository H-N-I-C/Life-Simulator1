/* v2 Social DOM tests (Kimi B, M4 §UI). Exercises
 * web/static/features/social/social.js (mountSocial) against a minimal
 * DOM stub with no server, using the FROZEN example shapes from
 * docs/collab/M4/02-contracts.md. The module is pure presentation:
 * projection-sourced text, server-option dispatch, no eligibility math.
 * Run: node tests/web_v2_social_dom.mjs */

import assert from "node:assert/strict";
import { mountSocial } from "../web/static/features/social/social.js";
import { normalizeState } from "../web/static/adapter.js";

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
    const parts = [];
    if (this._text) parts.push(this._text);
    for (const c of this.children) parts.push(c.textContent);
    return parts.join("");
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

/* ---------- frozen-example fixture ---------- */
const P = {
  mum: { id: "p_10", name: "Beitris Jenkins", age: 36, alive: true,
         role: "Mother", relationship: 100 },
  sam: { id: "p_88", name: "Sam Carter", age: 14, alive: true,
         social: { kind: "best_friend", closeness: 78,
                   circles: ["classmates"],
                   memory: ["We got detention together."] } },
  dana: { id: "p_91", name: "Dana Cole", age: 14, alive: true,
          social: { kind: "bully", closeness: 12,
                    circles: ["classmates"], memory: [] } },
  jo: { id: "p_40", name: "Jo", age: 34, alive: true },
};

const PENDING = {
  id: "d_12", kind: "social_invite",
  options: [
    { id: "accept", label: "Go", enabled: true, reason: null },
    { id: "decline", label: "Skip it", enabled: true, reason: null },
  ],
};

const DATA = {
  schema_version: 1,
  popularity: { score: 23, band: "known", label: "Well known",
                delta_last_year: 2 },
  groups: [
    { group_id: "family", label: "Family", people: [P.mum] },
    { group_id: "classmates", label: "Classmates",
      people: [P.sam, P.dana] },
    { group_id: "friends", label: "Friends", people: [] },
    { group_id: "coworkers", label: "Coworkers", people: [] },
  ],
  invites: [
    { invite_id: "inv_3", kind: "party", title: "Sam's birthday",
      host: P.sam, venue_label: "Riverside Park", adults_only: false,
      status: "open", decision_id: "d_12" },
    { invite_id: "inv_1", kind: "after_work", title: "After-work drinks",
      host: P.jo, venue_label: "The Harbour Bar", adults_only: true,
      status: "declined", decision_id: null },
  ],
  clubs: [],
};

/* ---------- tests ---------- */
{
  const root = new El("div");
  const calls = [];
  const hooks = {
    navigate: (...a) => calls.push(["navigate", ...a]),
    getState: () => ({ pending: PENDING }),
    resolveDecision: (...a) => calls.push(["resolve", ...a]),
  };
  const h = mountSocial(root, hooks, DATA);
  const text = root.textContent;

  // popularity indicator, game-y ("Label · NN%"), delta when it moved
  assert.ok(text.includes("Well known · 23%"), "popularity label + score%");
  assert.ok(text.includes("(+2)"), "positive delta shown");

  // groups: frozen order among non-empty; empty groups hidden honestly
  const groups = root.query((n) => n._classes &&
                                  n._classes.has("v2so-group"));
  assert.equal(groups.length, 2, "empty groups hidden");
  assert.equal(groups[0].getAttribute("data-group-id"), "family");
  assert.equal(groups[1].getAttribute("data-group-id"), "classmates");

  // family row: relationship bar from the existing relationship value
  const famChip = groups[0].query((n) => n._classes &&
                                        n._classes.has("v2so-chip"))[0];
  assert.ok(famChip.textContent.includes("Mother"),
            "family role label");
  assert.ok(famChip.textContent.includes("100%"),
            "family relationship bar %");
  assert.ok(text.includes("Classmates"), "group label");

  // chips: data-person-id, names, relation · kind labels, closeness bar
  const groupCard = groups[1];
  const chips = groupCard.query((n) => n._classes &&
                                      n._classes.has("v2so-chip"));
  assert.equal(chips.length, 2, "two classmates chips");
  const byId = {};
  for (const c of chips) byId[c.getAttribute("data-person-id")] = c;
  assert.ok(byId["p_88"], "chip for Sam");
  assert.ok(byId["p_91"], "chip for Dana");
  assert.ok(byId["p_88"].textContent.includes("Classmate · Best friend"),
            "relation + kind label");
  assert.ok(byId["p_88"].textContent.includes("S"), "avatar initial");
  assert.ok(byId["p_91"].textContent.includes("Classmate · Bully"),
            "bully relation label");
  assert.ok(byId["p_91"].textContent.includes("12%"), "closeness % shown");
  const fills = byId["p_88"].query((n) => n._classes &&
                                     n._classes.has("v2so-bar-fill"));
  assert.equal(fills[0].style.width, "78%", "closeness bar width");
  assert.ok(byId["p_88"].textContent.includes("We got detention together."),
            "memory line verbatim");

  // chip click navigates by person id
  byId["p_88"].click();
  assert.deepEqual(calls.at(-1), ["navigate", "person",
                                  { personId: "p_88" }]);

  // invites inbox
  const rows = root.query((n) => n._classes &&
                                  n._classes.has("v2so-invite"));
  assert.equal(rows.length, 2, "two inbox rows");
  const openRow = rows.find((r) => r.getAttribute("data-invite-id") ===
                                  "inv_3");
  const declinedRow = rows.find((r) =>
    r.getAttribute("data-invite-id") === "inv_1");
  assert.ok(openRow.textContent.includes("Open"), "open status chip");
  assert.ok(openRow.textContent.includes("Riverside Park"), "venue");
  assert.ok(!openRow.textContent.includes("18+"), "no 18+ on kid party");
  assert.ok(declinedRow.textContent.includes("Declined"), "status chip");
  assert.ok(declinedRow.textContent.includes("18+"), "adults-only badge");
  assert.equal(declinedRow.query((n) => n._classes &&
                                   n._classes.has("v2so-invite-btn"))
               .length, 0, "resolved invite has no action buttons");

  // open invite responds ONLY through the matching pending, advertised ids
  const btns = openRow.query((n) => n._classes &&
                               n._classes.has("v2so-invite-btn"));
  assert.equal(btns.length, 2, "accept + decline");
  btns[0].click();
  assert.deepEqual(calls.at(-1), ["resolve", "d_12", "accept", {}],
                   "resolveDecision with advertised option id");
  btns[1].click();
  assert.deepEqual(calls.at(-1), ["resolve", "d_12", "decline", {}]);

  // pending id mismatch -> no action buttons (never resolve blind)
  const root2 = new El("div");
  mountSocial(root2, {
    navigate: () => {}, getState: () => ({ pending: { id: "d_other" } }),
    resolveDecision: () => {},
  }, DATA);
  const rows2 = root2.query((n) => n._classes &&
                                  n._classes.has("v2so-invite"));
  const open2 = rows2.find((r) => r.getAttribute("data-invite-id") ===
                                   "inv_3");
  assert.equal(open2.query((n) => n._classes &&
                             n._classes.has("v2so-invite-btn")).length, 0,
               "no buttons without the matching pending");

  // option disabled state renders from the advertised option
  const root3 = new El("div");
  mountSocial(root3, {
    navigate: () => {},
    getState: () => ({ pending: { id: "d_12", options: [
      { id: "accept", label: "Go", enabled: false,
        reason: "you must be 18 or older to attend" }] } }),
    resolveDecision: () => {},
  }, DATA);
  const rows3 = root3.query((n) => n._classes &&
                                  n._classes.has("v2so-invite"));
  const open3 = rows3.find((r) => r.getAttribute("data-invite-id") ===
                                   "inv_3");
  const b3 = open3.query((n) => n._classes &&
                             n._classes.has("v2so-invite-btn"));
  assert.equal(b3.length, 1);
  assert.equal(b3[0].disabled, true, "server-disabled option");
  assert.equal(b3[0].getAttribute("aria-disabled"), "true");
  assert.ok(b3[0].textContent.includes("you must be 18 or older"),
            "server reason shown");

  // the floor band hides the indicator instead of reading "Unknown · 0"
  const root4 = new El("div");
  mountSocial(root4, { navigate: () => {} }, {
    ...DATA,
    popularity: { score: 5, band: "unknown", label: "Unknown",
                  delta_last_year: 0 },
  });
  assert.equal(root4.query((n) => n._classes &&
                             n._classes.has("v2so-pop")).length, 0,
               "unknown band hides the popularity indicator");

  // update + unmount
  h.update({ ...DATA, popularity: { score: 55, band: "liked",
                                    label: "Liked", delta_last_year: -3 } });
  assert.ok(root.textContent.includes("Liked · 55% (-3)"),
            "update re-renders");
  h.unmount();
  assert.equal(root.children.length, 0);
  // relation word == kind label renders once ("Friend", not "Friend · Friend")
  const root5 = new El("div");
  mountSocial(root5, { navigate: () => {} }, {
    ...DATA,
    groups: [{ group_id: "friends", label: "Friends",
               people: [{ id: "p_77", name: "Pat Pyne", age: 15,
                          alive: true,
                          social: { kind: "friend", closeness: 40,
                                    circles: ["friends"], memory: [] } }] }],
    invites: [],
  });
  const chip5 = root5.query((n) => n._classes &&
                                  n._classes.has("v2so-chip"))[0];
  assert.ok(chip5.textContent.includes("Friend"));
  assert.ok(!chip5.textContent.includes("Friend · Friend"),
            "relation/kind dedup");

  console.log("social DOM: all assertions passed");
}

/* ---------- adapter enrichment: refs gain appearance/relationship ------ */
{
  const raw = {
    game_id: "g1", revision: 1, status: "alive", player_id: "me",
    people: [
      { id: "me", name: "Me", age: 16, alive: true, appearance: {},
        role: "Player", relationship: null },
      { id: "p_10", name: "Beitris Jenkins", age: 36, alive: true,
        appearance: { seed: 5 }, role: "Mother", relationship: 100 },
    ],
    social: {
      groups: [{ group_id: "family", label: "Family",
                 people: [{ id: "p_10", name: "Beitris Jenkins", age: 36,
                            alive: true }] }],
      invites: [{ invite_id: "inv_9", kind: "party", title: "T",
                  host: { id: "p_10", name: "Beitris Jenkins", age: 36,
                          alive: true },
                  venue_label: "V", adults_only: false,
                  status: "open", decision_id: "d_1" }],
    },
  };
  const st = normalizeState(raw);
  const ref = st.social.groups[0].people[0];
  assert.equal(ref.appearance.seed, 5, "appearance filled from people");
  assert.equal(ref.relationship, 100, "relationship filled from people");
  assert.equal(ref.role, "Mother", "role filled from people");
  assert.equal(st.social.invites[0].host.relationship, 100,
               "invite host enriched too");
  console.log("adapter social enrichment: all assertions passed");
}
