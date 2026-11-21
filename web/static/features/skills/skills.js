/* v2 Skills module (Kimi C, M3). Mounted by the shell under the
 * `activities` screen (B applies the registration per contracts.md
 * M3 §Skills). Follows the §Shell mount API:
 *
 *   const handle = mountSkills(root, hooks, data);  // data = state.skills
 *   handle.update(data); handle.unmount();
 *
 * Shows the four skill values with progress, this year's training
 * uses/limits, real known costs and server disabled reasons. No spoiler
 * effect ranges: only costs/limits/reasons the server advertised.
 * Training dispatches through hooks.sendAction (retry-stable ops); the
 * module never calls fetch for mutations.
 */

import { formatMoney } from "../../common.js";

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

function bar(value) {
  const track = el("div", "v2sk-bar");
  const fill = el("div", "v2sk-bar-fill");
  fill.style.width = Math.max(0, Math.min(100, value)) + "%";
  track.appendChild(fill);
  return track;
}

function costText(costs) {
  if (!costs || !costs.length) return "Free";
  return costs.map((c) => {
    if (c && c.kind === "money" && Number.isFinite(c.amount)) {
      return c.amount > 0 ? formatMoney(c.amount) : "Free";
    }
    return c && c.label ? c.label : String(c);
  }).join(" · ");
}

export function mountSkills(root, hooks, data) {
  if (!root) throw new TypeError("mountSkills(root, hooks, data)");
  root.classList.add("v2sk-scope");

  function render(skills) {
    for (const n of [...root.children]) n.remove();
    if (!skills) {
      root.appendChild(el("p", "v2sk-empty", "No skill data yet."));
      return;
    }
    const card = el("section", "v2sk-card");
    card.appendChild(el("h2", "v2sk-title", "Skills"));

    const meta = skills.skills_meta || {};
    const values = skills.values || {};
    const grid = el("div", "v2sk-grid");
    for (const [skillId, value] of Object.entries(values)) {
      const row = el("div", "v2sk-skill");
      const label = (meta[skillId] && meta[skillId].label) || skillId;
      const head = el("div", "v2sk-skill-head");
      head.appendChild(el("span", "v2sk-skill-name", label));
      head.appendChild(el("span", "v2sk-skill-value", value + "/100"));
      row.appendChild(head);
      row.appendChild(bar(value));
      const desc = meta[skillId] && meta[skillId].description;
      if (desc) row.appendChild(el("p", "v2sk-skill-desc", desc));
      grid.appendChild(row);
    }
    card.appendChild(grid);

    const list = el("div", "v2sk-training");
    for (const spec of skills.training || []) {
      const enabled = spec.enabled !== false;
      const targetId = spec.target_id || spec.id;
      const b = el("button", "v2sk-train", spec.label || targetId);
      b.type = "button";
      const cost = el("span", "v2sk-cost", costText(spec.knownCosts ||
                                                    spec.known_costs));
      b.appendChild(cost);
      const used = (skills.uses_this_year || {})[targetId] || 0;
      const limits = (spec.limits || []).join(" · ");
      const usesLine = limits + (used ? " — used " + used : "");
      if (usesLine.trim()) b.appendChild(el("span", "v2sk-limits", usesLine));
      b.disabled = !enabled;
      if (!enabled) {
        b.setAttribute("aria-disabled", "true");
        if (spec.reason) {
          b.title = spec.reason;
          b.appendChild(el("span", "v2sk-reason", spec.reason));
        }
      } else if (typeof hooks.sendAction === "function") {
        const needsChoice = !!(spec.inputs && spec.inputs.length &&
                               spec.inputs[0].type === "choice");
        let chooser = null;
        const closeChooser = () => {
          if (chooser) { chooser.remove(); chooser = null; }
        };
        const dispatch = (payload) => {
          b.disabled = true;
          b.classList.add("v2sk-busy");
          return Promise.resolve(
            hooks.sendAction("train_skill", payload, targetId))
            .catch(() => {})
            .finally(() => { b.classList.remove("v2sk-busy"); });
        };
        b.addEventListener("click", () => {
          if (!needsChoice) {
            closeChooser();
            dispatch({}).finally(() => {});
            return;
          }
          // choose-skill option: offer the actual skill choice instead
          // of silently picking the first advertised one
          if (chooser) { closeChooser(); return; }
          chooser = el("div", "v2sk-chooser");
          chooser.setAttribute("role", "group");
          chooser.appendChild(el("span", "v2sk-chooser-prompt",
                                 "Train which skill?"));
          const choiceInput = spec.inputs[0];
          for (const skillId of choiceInput.choices || []) {
            const label = (meta[skillId] && meta[skillId].label) || skillId;
            const opt = el("button", "v2sk-choice", label);
            opt.type = "button";
            opt.setAttribute("data-skill", skillId);
            opt.addEventListener("click", () => {
              closeChooser();
              dispatch({ skill: skillId }).finally(() => {});
            });
            chooser.appendChild(opt);
          }
          b.insertAdjacentElement
            ? b.insertAdjacentElement("afterend", chooser)
            : b.parentNode && b.parentNode.insertBefore(chooser,
                                                        b.nextSibling);
        });
      }
      list.appendChild(b);
    }
    if (list.children.length) card.appendChild(list);
    root.appendChild(card);
  }

  render(data);

  return {
    update(data) { render(data); },
    unmount() {
      for (const n of [...root.children]) n.remove();
      root.classList.remove("v2sk-scope");
    },
  };
}
