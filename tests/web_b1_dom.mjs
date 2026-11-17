/* B1 front-end resilience tests (Kimi B): the global error boundary in
   web/static/common.js, exercised with stubbed browser globals.
   Run: node tests/web_b1_dom.mjs
   Also invoked by tests.test_web_b1 (unittest wrapper). */

import assert from "node:assert/strict";

/* ---------- minimal DOM/browser stubs ---------- */
class El {
  constructor(tag) {
    this.tagName = tag.toUpperCase();
    this.children = [];
    this.parent = null;
    this.attributes = {};
    this.listeners = {};
    this.className = "";
    this.id = "";
    this.textContent = "";
    this.innerHTML = "";
    this.hidden = false;
    this.disabled = false;
    this.style = {};
  }
  get isConnected() { return this.parent !== null; }
  appendChild(c) { c.parent = this; this.children.push(c); return c; }
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
  focus() {}
  _walk(out = []) {
    out.push(this);
    for (const c of this.children) c._walk(out);
    return out;
  }
  querySelector(sel) {
    const all = this._walk().slice(1);
    if (sel.startsWith(".")) {
      const cls = sel.slice(1);
      return all.find((e) => e.className.split(" ").includes(cls)) || null;
    }
    return null;
  }
  querySelectorAll() { return []; }
}

const documentStub = {
  body: new El("body"),
  createElement: (tag) => new El(tag),
  getElementById(id) {
    return documentStub.body._walk().find((e) => e.id === id) || null;
  },
  addEventListener() {},
};

const windowListeners = {};
const windowStub = {
  addEventListener(type, fn) { (windowListeners[type] ||= []).push(fn); },
};
function fireWindow(type, event) {
  for (const fn of windowListeners[type] || []) fn(event);
}

const fetchCalls = [];
let fetchImpl = () => Promise.resolve({ ok: true, status: 200 });
const fetchStub = (...args) => { fetchCalls.push(args); return fetchImpl(); };

let online = true;
const navigatorStub = { onLine: true, userAgent: "b1-test" };

let storedGameId = null;
const sessionStorageStub = {
  getItem: (k) => (k === "game_id" ? storedGameId : null),
  setItem: (k, v) => { if (k === "game_id") storedGameId = v; },
  removeItem: (k) => { if (k === "game_id") storedGameId = null; },
};

const locationStub = { pathname: "/game", reloadCount: 0,
                       reload() { this.reloadCount++; } };

globalThis.window = windowStub;
globalThis.document = documentStub;
Object.defineProperty(globalThis, "navigator",
                      { value: navigatorStub, configurable: true });
globalThis.sessionStorage = sessionStorageStub;
globalThis.location = locationStub;
globalThis.fetch = fetchStub;

Object.defineProperty(navigatorStub, "onLine", { get: () => online });

/* ---------- import the module under test ---------- */
const common = await import("../web/static/common.js");
assert.equal(typeof common.reportClientError, "function");
assert.equal(typeof common.showCrashCard, "function");
assert.equal(typeof common.setErrorRetryHandler, "function");

let passed = 0;
function test(name, fn) {
  try {
    fn();
    passed++;
    console.log("ok - " + name);
  } catch (e) {
    console.error("FAIL - " + name + "\n  " + e.message);
    process.exitCode = 1;
  }
}

function lastPayload() {
  const [url, opts] = fetchCalls[fetchCalls.length - 1];
  assert.equal(url, "/api/client-error");
  assert.equal(opts.method, "POST");
  return JSON.parse(opts.body);
}

/* ---------- tests ---------- */

test("window error reports to /api/client-error with the frozen A1 shape", () => {
  fireWindow("error", { message: "boom", filename: "/static/game.js",
                        lineno: 3, colno: 7,
                        error: new Error("boom") });
  const p = lastPayload();
  // Frozen shape (q1-a1-report.md): {kind, message, stack?, url?, game_id?}
  assert.deepEqual(Object.keys(p).sort(),
                   ["kind", "message", "stack", "url"]);
  assert.equal(p.kind, "error");
  assert.equal(p.message, "boom");
  assert.equal(p.url, "/game");
  assert.ok(p.stack.includes("/static/game.js:3:7"),
            "source position folded into stack: " + p.stack);
  assert.ok(p.stack.includes("boom"), "original stack kept");
});

test("unhandledrejection reports and shows friendly card", () => {
  fireWindow("unhandledrejection",
             { reason: new Error("async kaput") });
  const p = lastPayload();
  assert.equal(p.kind, "unhandledrejection");
  assert.equal(p.message, "async kaput");
  const card = documentStub.body.querySelector(".crash-card");
  assert.ok(card, "crash card rendered");
  const texts = card._walk().map((e) => e.textContent).join(" ");
  assert.ok(texts.includes("Something went wrong"), "title");
  assert.ok(texts.includes("Your save is safe"), "reassurance");
  const buttons = card._walk().filter((e) => e.tagName === "BUTTON");
  assert.deepEqual(buttons.map((b) => b.textContent), ["Retry", "Reload"]);
});

test("duplicate identical errors are deduped (one report)", () => {
  const before = fetchCalls.length;
  fireWindow("error", { message: "same", error: new Error("same") });
  fireWindow("error", { message: "same", error: new Error("same") });
  fireWindow("error", { message: "same", error: new Error("same") });
  assert.equal(fetchCalls.length - before, 1);
});

test("endpoint failure never cascades (404 tolerated)", async () => {
  fetchImpl = () => Promise.resolve({ ok: false, status: 404 });
  common.reportClientError({ type: "error", message: "tolerated" });
  await new Promise((r) => setTimeout(r, 0));
  fetchImpl = () => Promise.resolve({ ok: true, status: 200 });
});

test("game_id is attached when a game is active", () => {
  storedGameId = "g-123";
  common.reportClientError({ type: "error", message: "with-game" });
  assert.equal(lastPayload().game_id, "g-123");
  storedGameId = null;
});

test("offline banner appears offline, hides online", () => {
  online = false;
  fireWindow("offline", {});
  const banner = documentStub.getElementById("offlineBanner");
  assert.ok(banner, "banner created");
  assert.equal(banner.hidden, false);
  assert.ok(banner.textContent.includes("offline"));
  online = true;
  fireWindow("online", {});
  assert.equal(banner.hidden, true);
});

test("Retry on crash card calls registered handler, not reload", () => {
  common.showCrashCard("x");
  let retried = 0;
  common.setErrorRetryHandler(() => { retried++; });
  const card = documentStub.body.querySelector(".crash-card");
  const retryBtn = card._walk().find((e) => e.tagName === "BUTTON" &&
                                           e.textContent === "Retry");
  retryBtn.click();
  assert.equal(retried, 1);
  assert.equal(locationStub.reloadCount, 0);
  // Reload button still reloads.
  common.showCrashCard("y");
  const card2 = documentStub.body.querySelector(".crash-card");
  const reloadBtn = card2._walk().find((e) => e.tagName === "BUTTON" &&
                                             e.textContent === "Reload");
  reloadBtn.click();
  assert.equal(locationStub.reloadCount, 1);
});

test("no crash card before any error", () => {
  // Previous tests created and removed cards; ensure hideCrashCard works.
  common.showCrashCard("z");
  common.hideCrashCard();
  assert.equal(documentStub.body.querySelector(".crash-card"), null);
});

test("reporting caps at 20 per session", () => {
  for (let i = 0; i < 30; i++) {
    common.reportClientError({ type: "error", message: "m" + i });
  }
  const reports = fetchCalls.filter((c) => c[0] === "/api/client-error");
  assert.ok(reports.length <= 20,
            "capped, got " + reports.length);
});


console.log(passed + " DOM behaviour tests done");
