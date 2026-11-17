/* M2.5 step-3 smoke test: the editor UI module imports cleanly in a
   stubbed browser environment and exports its surface. Run:
   node tests/web_m25_editor_dom.mjs */

import assert from "node:assert/strict";

class El {
  constructor(tag) {
    this.tagName = tag.toUpperCase();
    this.children = []; this.parent = null; this.attributes = {};
    this.className = ""; this.id = ""; this.textContent = "";
    this.innerHTML = ""; this.hidden = false; this.style = {};
  }
  appendChild(c) { c.parent = this; this.children.push(c); return c; }
  remove() {}
  setAttribute(k, v) { this.attributes[k] = String(v); }
  addEventListener() {}
  focus() {}
}
const documentStub = {
  body: new El("body"),
  createElement: (t) => new El(t),
  getElementById: () => null,
  addEventListener() {},
};
globalThis.window = { addEventListener() {} };
globalThis.document = documentStub;
Object.defineProperty(globalThis, "navigator",
                      { value: { onLine: true, userAgent: "m25-test" },
                        configurable: true });
globalThis.sessionStorage = { getItem: () => null, setItem() {},
                              removeItem() {} };
globalThis.location = { pathname: "/game", reload() {} };
globalThis.fetch = () => Promise.resolve({ ok: true, status: 200,
                                             json: async () => ({}) });

const editor = await import(
  "../web/static/features/relationship_policy/editor.js");
assert.equal(typeof editor.initEditor, "function");
assert.equal(typeof editor.toggleEditor, "function");
console.log("ok - editor module imports and exports initEditor/toggleEditor");
console.log("1 M2.5 editor UI smoke test done");
