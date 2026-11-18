/* v2 Choice sheet (Kimi B, redesign-v2): presentation for a pending
 * decision, per the Choice mockup and the frozen §Choice contract.
 * Resolution machinery stays in game.js (retry-stable ops); this module
 * only renders and reports intents:
 *
 *   const handle = mountChoiceSheet(box, {
 *     decision,            // normalized decision (adapter normalizeDecision)
 *     people,              // resolved person objects for personIds
 *     onResolve(optId, inputs),
 *     onCollapse(),        // "Decide later" — NEVER discards the decision
 *     onError(message),    // input validation errors
 *     onPersonClick?(id),  // person chips open the Person overlay (Stage 3)
 *   });
 *   // → { firstEnabled, unmount() }
 *
 * Hard rules (codex-review.md §5): no fixed option count; disabled options
 * carry the server reason visibly; knownEffects render as authored ($0 is
 * "Free", never hidden); one in-flight resolve at a time; initial focus on
 * the first enabled option; Escape/Cancel collapse to the pill via
 * onCollapse — a server decision is never discarded by the UI.
 */

import { miniPersonChip } from "../common.js";
import { buildInputRow, collectInputs } from "./inputs.js";

const DANGER_KINDS = new Set(["surrender_final"]);

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

export function mountChoiceSheet(box, opts) {
  if (!box || !opts || !opts.decision) {
    throw new TypeError("mountChoiceSheet(box, opts) requires a decision");
  }
  const d = opts.decision;
  for (const n of [...box.children]) if (n.id !== "modalCancel") n.remove();
  box.classList.toggle("danger", DANGER_KINDS.has(d.kind));

  // Header: title + the frozen §Choice chip — player age (always real) plus
  // the observer-safe display_category when the server mapped one; nothing
  // is synthesized for unknown/absent kinds.
  const head = el("div", "v2c-head");
  const h2 = el("h2", "v2c-title", d.title || "A decision");
  h2.id = "v2SheetTitle";
  head.appendChild(h2);
  const age = typeof opts.playerAge === "number" ? opts.playerAge : null;
  if (age !== null || d.displayCategory) {
    const chip = age !== null ? "AGE " + age : "";
    const cat = d.displayCategory ? String(d.displayCategory).toUpperCase()
                                  : "";
    head.appendChild(el("span", "v2c-kind",
                        [chip, cat].filter(Boolean).join(" · ")));
  }
  box.appendChild(head);

  if (d.text) box.appendChild(el("p", "v2c-text", d.text));

  const pp = el("div", "v2c-people");
  for (const p of opts.people || []) {
    const chip = miniPersonChip(p, 30);
    if (typeof opts.onPersonClick === "function" && p.id) {
      chip.classList.add("v2-person-link");
      chip.setAttribute("role", "button");
      chip.tabIndex = 0;
      chip.setAttribute("aria-label", p.name + " — view person");
      const open = () => opts.onPersonClick(p.id);
      chip.addEventListener("click", open);
      chip.addEventListener("keydown", (e) => {
        if (e.key === "Enter" || e.key === " ") {
          e.preventDefault();
          open();
        }
      });
    }
    pp.appendChild(chip);
  }
  if (pp.children.length) box.appendChild(pp);

  const form = el("form", "v2c-form");
  const doc = {};
  for (const input of d.inputs || []) {
    form.appendChild(buildInputRow(input, doc));
  }
  // Per-option inputs (frozen option shape): rows grouped under their option
  // and collected only when that option is chosen.
  const optDocs = {};
  for (const opt of d.options || []) {
    if (!opt.inputs || !opt.inputs.length) continue;
    const odoc = {};
    const group = el("div", "v2c-opt-inputs");
    group.appendChild(el("span", "v2c-opt-inputs-for", opt.label || opt.id));
    for (const input of opt.inputs) {
      group.appendChild(buildInputRow(input, odoc));
    }
    form.appendChild(group);
    optDocs[opt.id] = odoc;
  }
  box.appendChild(form);

  const list = el("div", "v2c-options");
  let firstEnabled = null;
  for (const opt of d.options || []) {
    const b = el("button", "v2c-option", opt.label || opt.id);
    b.type = "button";
    if (opt.knownEffects && opt.knownEffects.length) {
      b.appendChild(el("span", "v2c-effects",
                       opt.knownEffects.map(String).join(" · ")));
    }
    const enabled = opt.enabled !== false;
    b.disabled = !enabled;
    if (!enabled) {
      b.setAttribute("aria-disabled", "true");
      if (opt.reason) {
        b.title = opt.reason;
        b.appendChild(el("span", "v2c-reason", opt.reason));
      }
    } else if (!firstEnabled) {
      firstEnabled = b;
    }
    if (enabled) {
      if (!firstEnabled) firstEnabled = b;
      b.addEventListener("click", () => {
        try {
          const inputs = collectInputs({ ...doc, ...(optDocs[opt.id] || {}) });
          // Duplicate-submit guard: freeze every option while one resolve is
          // in flight; the modal closes on success.
          for (const x of list.querySelectorAll("button")) x.disabled = true;
          opts.onResolve(opt.id, inputs);
        } catch (err) {
          for (const x of list.querySelectorAll("button")) {
            x.disabled = x.getAttribute("aria-disabled") === "true";
          }
          if (opts.onError) opts.onError(err.message);
        }
      });
    }
    list.appendChild(b);
  }
  box.appendChild(list);

  const later = el("button", "v2c-later", "Decide later");
  later.type = "button";
  later.addEventListener("click", () => opts.onCollapse());
  box.appendChild(later);

  return {
    firstEnabled,
    unmount() {
      for (const n of [...box.children]) if (n.id !== "modalCancel") n.remove();
      box.classList.remove("danger");
    },
  };
}
