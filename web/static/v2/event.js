/* v2 Event modal + outcome popup (Kimi C, redesign-v2 F5/F6).
 * Per docs/collab/redesign-v2/contracts.md §Event: a drop-in replacement
 * for the §Choice sheet renderer — B mounts it in game.js's decision modal
 * in place of mountChoiceSheet. Same opts contract plus the outcome flow:
 *
 *   const sheet = mountEventModal(box, {
 *     decision, people, playerAge,
 *     onResolve(optId, inputs),  // → Promise resolving to the §Outcome
 *                                //   object ({text, changes, person_ids})
 *                                //   or null
 *     onCollapse(), onError(message), onPersonClick?(id), onClose(),
 *     renderAvatar?(el, appearance, age, size),
 *   });
 *   // → { firstEnabled, unmount() }
 *
 * F5: no stat-effect spoilers before choosing — option buttons render
 * label + real knownCosts + disabled reasons only; knownEffects never
 * render pre-choice. After choosing, the ACTUAL deltas from the server's
 * resolve outcome render in a popup; a tap on Continue calls onClose().
 * F6: a centered modal card showing the OTHER person's avatar (never the
 * player's); when the event involves nobody else, no avatar renders.
 *
 * Hard rules retained from §Choice: no fixed option count; disabled means
 * disabled + aria-disabled + the server reason visible; one in-flight
 * resolve; initial focus on the first enabled option; Escape/Cancel
 * collapse to the pill via onCollapse — a server decision is never
 * discarded by the UI; no fabricated options, reasons, effects or deltas.
 */

import { miniPersonChip, renderAvatar as defaultRenderAvatar,
         formatMoney } from "../common.js";
import { buildInputRow, collectInputs } from "./inputs.js";

const DANGER_KINDS = new Set(["surrender_final"]);

const DELTA_LABELS = {
  happiness: "Happiness",
  health: "Health",
  smarts: "Smarts",
  looks: "Looks",
  money: "Money",
  relationship: "Relationship",
};

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

function clearBox(box) {
  for (const n of [...box.children]) if (n.id !== "modalCancel") n.remove();
}

function fmtDelta(key, value) {
  const v = Math.round(value);
  if (key === "money") {
    return (v > 0 ? "+" : "") + formatMoney(v);
  }
  return (v > 0 ? "+" : "") + v;
}

function costText(cost) {
  if (typeof cost === "string") return cost;
  if (!cost || typeof cost !== "object") return "";
  const label = typeof cost.label === "string" ? cost.label : "";
  if (Number.isFinite(cost.amount) && (!cost.kind || cost.kind === "money")) {
    // Legacy rule options use {label, amount}; labels name the service,
    // so they must not hide its price. Already-formatted labels stay intact.
    if (label.includes("$")) return label;
    const price = cost.amount === 0 ? "Free" : formatMoney(cost.amount);
    return label ? label + " · " + price : cost.amount === 0 ? price : "Costs " + price;
  }
  if (label) return label;
  if (cost.kind === "time" && Number.isFinite(cost.amount) && cost.unit) {
    return cost.amount + " " + cost.unit;
  }
  return "";
}

/* ---- pre-choice card -------------------------------------------------- */

export function mountEventModal(box, opts) {
  if (!box || !opts || !opts.decision) {
    throw new TypeError("mountEventModal(box, opts) requires a decision");
  }
  const d = opts.decision;
  const drawAvatar = typeof opts.renderAvatar === "function"
    ? opts.renderAvatar : defaultRenderAvatar;
  let alive = true;       // false after unmount: no post-resolve UI
  let resolving = false;
  let continued = false;  // Continue pressed at most once

  function renderChoice() {
    clearBox(box);
    box.classList.toggle("danger", DANGER_KINDS.has(d.kind));

    const card = el("div", "v2e-card");
    card.setAttribute("role", "dialog");
    card.setAttribute("aria-modal", "true");
    card.setAttribute("aria-labelledby", "v2SheetTitle");

    const head = el("div", "v2e-head");
    // F6: the avatar of the OTHER person involved — the first resolved
    // non-player person. Never the player's; omitted when nobody else is
    // part of the event.
    const other = (opts.people || []).find((p) => p.id !== opts.playerId);
    if (other) {
      const av = el("div", "v2e-avatar");
      drawAvatar(av, other.appearance || {}, other.age, 56);
      head.appendChild(av);
    }
    const headText = el("div", "v2e-head-text");
    const h2 = el("h2", "v2e-title", d.title || "A decision");
    h2.id = "v2SheetTitle";
    headText.appendChild(h2);
    const age = typeof opts.playerAge === "number" ? opts.playerAge : null;
    if (age !== null || d.displayCategory) {
      const chip = age !== null ? "AGE " + age : "";
      const cat = d.displayCategory
        ? String(d.displayCategory).toUpperCase() : "";
      headText.appendChild(el("span", "v2e-kind",
                              [chip, cat].filter(Boolean).join(" · ")));
    }
    head.appendChild(headText);
    card.appendChild(head);

    if (d.text) card.appendChild(el("p", "v2e-text", d.text));

    const pp = el("div", "v2e-people");
    for (const p of opts.people || []) {
      if (p.id === opts.playerId) continue;
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
    if (pp.children.length) card.appendChild(pp);

    const form = el("form", "v2e-form");
    form.addEventListener("submit", (e) => e.preventDefault());
    const doc = {};
    for (const input of d.inputs || []) {
      form.appendChild(buildInputRow(input, doc));
    }
    // Per-option inputs (frozen option shape): rows grouped under their
    // option and collected only when that option is chosen.
    const optDocs = {};
    for (const opt of d.options || []) {
      if (!opt.inputs || !opt.inputs.length) continue;
      const odoc = {};
      const group = el("div", "v2e-opt-inputs");
      group.appendChild(el("span", "v2e-opt-inputs-for", opt.label || opt.id));
      for (const input of opt.inputs) {
        group.appendChild(buildInputRow(input, odoc));
      }
      form.appendChild(group);
      optDocs[opt.id] = odoc;
    }
    if (form.children.length) card.appendChild(form);

    const list = el("div", "v2e-options");
    let firstEnabled = null;
    for (const opt of d.options || []) {
      const b = el("button", "v2e-option", opt.label || opt.id);
      b.type = "button";
      // F5: real costs stay visible; stat-effect spoilers (knownEffects)
      // are never rendered before choosing.
      const costs = opt.knownCosts || opt.known_costs;
      if (costs && costs.length) {
        b.appendChild(el("span", "v2e-costs",
                         costs.map(costText).filter(Boolean).join(" · ")));
      }
      const enabled = opt.enabled !== false;
      b.disabled = !enabled;
      if (!enabled) {
        b.setAttribute("aria-disabled", "true");
        if (opt.reason) {
          b.title = opt.reason;
          b.appendChild(el("span", "v2e-reason", opt.reason));
        }
      } else if (!firstEnabled) {
        firstEnabled = b;
      }
      if (enabled) {
        b.addEventListener("click", () => choose(b, opt, doc, optDocs, list));
      }
      list.appendChild(b);
    }
    card.appendChild(list);

    const later = el("button", "v2e-later", "Decide later");
    later.type = "button";
    later.addEventListener("click", () => {
      if (!resolving) opts.onCollapse();
    });
    card.appendChild(later);

    box.appendChild(card);
    return firstEnabled;
  }

  function choose(btn, opt, doc, optDocs, list) {
    if (!alive || resolving || continued) return;
    let inputs;
    try {
      inputs = collectInputs({ ...doc, ...(optDocs[opt.id] || {}) });
    } catch (err) {
      if (opts.onError) opts.onError(err.message);
      return;
    }
    resolving = true;
    // Duplicate-submit guard: freeze every option while one resolve is in
    // flight; the busy label shows on the chosen option.
    for (const x of list.querySelectorAll("button")) x.disabled = true;
    btn.classList.add("v2e-busy");
    const label = opt.label || opt.id;
    btn.textContent = label + "…";
    let result;
    try { result = opts.onResolve(opt.id, inputs); }
    catch (err) { result = Promise.reject(err); }
    Promise.resolve(result).then((outcome) => {
      if (!alive) return;
      resolving = false;
      if (outcome && ((outcome.text && String(outcome.text).trim()) ||
                      (outcome.changes && Object.keys(outcome.changes).length) ||
                      (outcome.relationship_changes &&
                       Object.keys(outcome.relationship_changes).length))) {
        renderOutcome(outcome);
        const cont = [...box.querySelectorAll("button")].find(
          (b) => b.classList && b.classList.contains("v2e-continue"));
        if (cont && cont.focus) cont.focus();
      } else {
        opts.onClose();          // nothing honest to show — shell decides
      }
    }, () => {
      if (!alive) return;
      resolving = false;
      // The shell reconciles the pending choice or offers safe retry.
      opts.onClose();
    });
  }

  /* ---- post-choice outcome popup (F5) ---------------------------------- */

  function renderOutcome(outcome) {
    clearBox(box);
    box.classList.remove("danger");
    const drawAvatar = typeof opts.renderAvatar === "function"
      ? opts.renderAvatar : defaultRenderAvatar;

    const card = el("div", "v2e-card v2e-outcome");
    card.setAttribute("role", "alertdialog");
    card.setAttribute("aria-modal", "true");
    card.setAttribute("aria-labelledby", "v2OutcomeTitle");

    const head = el("div", "v2e-head");
    const other = (opts.people || []).find((p) => p.id !== opts.playerId);
    if (other) {
      const av = el("div", "v2e-avatar v2e-avatar-outcome");
      drawAvatar(av, other.appearance || {}, other.age, 56);
      head.appendChild(av);
    }
    const headText = el("div", "v2e-head-text");
    headText.appendChild(el("h2", "v2e-title", "What happened"));
    headText.children[0].id = "v2OutcomeTitle";
    head.appendChild(headText);
    card.appendChild(head);

    if (outcome.text) card.appendChild(el("p", "v2e-text", outcome.text));

    const changes = outcome.changes || {};
    const relationships = Object.entries(outcome.relationship_changes || {})
      .filter(([, value]) => Number.isFinite(value) && value !== 0);
    const keys = Object.keys(changes).filter(
      (k) => Number.isFinite(changes[k]) && changes[k] !== 0 && DELTA_LABELS[k]
        && !(k === "relationship" && relationships.length));
    if (keys.length || relationships.length) {
      const rows = el("div", "v2e-deltas");
      for (const k of keys) {
        const row = el("div", "v2e-delta");
        const v = changes[k];
        row.appendChild(el("span", "v2e-delta-label", DELTA_LABELS[k]));
        row.appendChild(el("span",
                           "v2e-delta-value " +
                             (v > 0 ? "v2e-delta-up" : "v2e-delta-down"),
                           fmtDelta(k, v)));
        rows.appendChild(row);
      }
      for (const [id, value] of relationships) {
        const person = (opts.people || []).find((p) => p.id === id);
        const row = el("div", "v2e-delta");
        row.appendChild(el("span", "v2e-delta-label",
          person ? person.name + " · Relationship" : "Relationship"));
        row.appendChild(el("span", "v2e-delta-value " +
          (value > 0 ? "v2e-delta-up" : "v2e-delta-down"),
          fmtDelta("relationship", value)));
        rows.appendChild(row);
      }
      card.appendChild(rows);
    }

    const cont = el("button", "v2e-continue", "Continue");
    cont.type = "button";
    cont.addEventListener("click", () => {
      if (!alive || continued) return;
      continued = true;
      opts.onClose();
    });
    card.appendChild(cont);
    box.appendChild(card);
  }

  let firstEnabled;
  if (opts.outcome) {
    renderOutcome(opts.outcome);
    firstEnabled = [...box.querySelectorAll("button")].find(
      (b) => b.classList.contains("v2e-continue"));
  } else firstEnabled = renderChoice();

  return {
    firstEnabled,
    unmount() {
      alive = false;
      clearBox(box);
      box.classList.remove("danger");
    },
  };
}
