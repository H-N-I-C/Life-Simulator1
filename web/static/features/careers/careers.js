/* v2 Careers (Work) module (Kimi A, M3). Mounted by the shell under the
 * `job` screen (B applies the registration per contracts.md M3 §Careers
 * and §Shell). Follows the §Shell mount API:
 *
 *   const handle = mountCareers(root, hooks, data);  // data = state.careers
 *   handle.update(data); handle.unmount();
 *
 * Shows the active assignment (role, employer, pay, hours, tenure,
 * performance, advancement path), the openings list with server-computed
 * eligibility + precise refusal reasons, and the resignation control.
 * Applications/promotions dispatch through hooks.sendAction with the
 * frozen action IDs (careers_apply / careers_promote / careers_resign) —
 * the module never calls fetch for mutations and never invents data:
 * pay ranges, requirements, eligibility and reasons all come from the
 * A-owned projection. No stat-effect spoilers (F5): only real costs,
 * limits and server reasons.
 */

import { formatMoney } from "../../common.js";

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

function money(n) {
  return formatMoney(n);
}

function dispatch(hooks, btn, actionId, payload) {
  if (typeof hooks.sendAction !== "function") return;
  btn.disabled = true;
  btn.classList.add("v2cw-busy");
  Promise.resolve(hooks.sendAction(actionId, payload, null))
    .catch(() => {})
    .finally(() => btn.classList.remove("v2cw-busy"));
}

function actionButton(hooks, spec, payload, extraClass) {
  const enabled = spec.enabled !== false;
  const b = el("button", "v2cw-btn " + (extraClass || ""), spec.label);
  b.type = "button";
  b.disabled = !enabled;
  if (!enabled) {
    b.setAttribute("aria-disabled", "true");
    if (spec.reason) {
      b.title = spec.reason;
      b.appendChild(el("span", "v2cw-reason", spec.reason));
    }
    return b;
  }
  b.addEventListener("click", () => dispatch(hooks, b, spec.id, payload));
  return b;
}

function requirementLine(req) {
  const parts = [];
  for (const q of req.qualifications || []) {
    parts.push((q.met ? "✓ " : "• ") + (q.label ||
               q.id.replace(/_/g, " ")));
  }
  for (const s of req.skills || []) {
    const have = s.value === null || s.value === undefined
      ? "?" : s.value;
    parts.push((s.met ? "✓ " : "• ") + s.id + " " + have + "/" +
               s.required);
  }
  for (const r of req.roles || []) {
    parts.push((r.met ? "✓ " : "• ") + "Experience as " +
               (r.label || r.id.replace(/_/g, " ")));
  }
  if (req.min_age) parts.push("age " + req.min_age + "+");
  return parts;
}

/* C3 (codex m3c review): render the server's named routes as
 * ALTERNATIVES — "route A OR route B". Shared age/skill requirements
 * render once above the routes; each route shows the server's label
 * (never an id) with per-item met markers. The module computes NO
 * eligibility itself: Apply stays bound to the server's o.eligible. */
function renderRoutes(row, req) {
  const routes = req.routes;
  if (!routes || routes.length < 2) return false;

  const sharedBits = [];
  if (req.shared && req.shared.min_age) {
    sharedBits.push("age " + req.shared.min_age + "+");
  }
  for (const s of (req.shared && req.shared.skills) || []) {
    const have = s.value === null || s.value === undefined
      ? "?" : s.value;
    sharedBits.push((s.met ? "✓ " : "• ") + s.id + " " + have + "/" +
                    s.required);
  }
  if (sharedBits.length) {
    row.appendChild(el("p", "v2cw-shared",
                       "Needs " + sharedBits.join(" · ")));
  }

  const box = el("div", "v2cw-routes");
  routes.forEach((route, i) => {
    if (i) box.appendChild(el("div", "v2cw-route-or", "or"));
    const r = el("div", "v2cw-route " +
                      (route.met ? "v2cw-route-met" : "v2cw-route-unmet"));
    r.appendChild(el("p", "v2cw-route-label",
                     (route.met ? "✓ " : "• ") + route.label));
    const items = el("ul", "v2cw-route-items");
    const singleItem = (route.qualifications || []).length +
                       (route.roles || []).length === 1;
    for (const q of route.qualifications || []) {
      if (singleItem) continue; // the route label already names this credential
      items.appendChild(el("li", q.met ? "v2cw-ri-met" : "v2cw-ri-unmet",
                           (q.met ? "✓ " : "• ") +
                           (q.label || String(q.id).replace(/_/g, " "))));
    }
    for (const rr of route.roles || []) {
      if (singleItem && rr.min_tenure == null) continue;
      let text = (rr.met ? "✓ " : "• ") +
                 (singleItem ? "Recorded experience" :
                  (rr.label || String(rr.id).replace(/_/g, " ")));
      if (rr.min_tenure != null) {
        text += " — " + (rr.recorded_years || 0) + "/" +
                rr.min_tenure + " years";
      }
      items.appendChild(el("li", rr.met ? "v2cw-ri-met" : "v2cw-ri-unmet",
                           text));
    }
    if (items.children.length) r.appendChild(items);
    box.appendChild(r);
  });
  row.appendChild(box);
  return true;
}

export function mountCareers(root, hooks, data) {
  if (!root) throw new TypeError("mountCareers(root, hooks, data)");
  root.classList.add("v2cw-scope");

  function render(careers) {
    for (const n of [...root.children]) n.remove();
    if (!careers) {
      root.appendChild(el("p", "v2cw-empty", "No career data yet."));
      return;
    }
    const frag = document.createDocumentFragment();

    // ---- current assignment: the hero card (M3c game feel) — title,
    // employer, prominent salary, performance bar, years in role
    const card = el("section", "v2cw-card v2cw-current");
    card.appendChild(el("h2", "v2cw-title", "Work"));
    const a = careers.assignment;
    if (a) {
      const head = el("div", "v2cw-job-head");
      head.appendChild(el("span", "v2cw-role", a.role_label || a.role_id));
      head.appendChild(el("span", "v2cw-employer",
                          a.employer_name || a.employer_id));
      card.appendChild(head);
      card.appendChild(el("p", "v2cw-salary-line",
                          money(a.salary) + " / year"));

      const perfWrap = el("div", "v2cw-perf");
      const perfLabel = el("span", "v2cw-perf-label", "Performance");
      const perfTrack = el("div", "v2cw-perf-track");
      const perfFill = el("div", "v2cw-perf-fill");
      const perfVal = Math.max(0, Math.min(100, a.performance));
      perfFill.style.width = perfVal + "%";
      if (perfVal < 35) perfFill.classList.add("v2cw-perf-low");
      else if (perfVal < 70) perfFill.classList.add("v2cw-perf-mid");
      perfTrack.appendChild(perfFill);
      perfWrap.appendChild(perfLabel);
      perfWrap.appendChild(perfTrack);
      perfWrap.appendChild(el("span", "v2cw-perf-value",
                              a.performance + "/100"));
      card.appendChild(perfWrap);

      const stats = el("dl", "v2cw-stats");
      const rows = [
        ["Hours", a.hours + " / week"],
        ["Years in role", String(a.role_tenure)],
      ];
      for (const [k, v] of rows) {
        stats.appendChild(el("dt", "v2cw-stat-k", k));
        stats.appendChild(el("dd", "v2cw-stat-v", v));
      }
      card.appendChild(stats);
      if (a.promotions_to && a.promotions_to.length) {
        const promo = el("div", "v2cw-promo");
        promo.appendChild(el("h3", "v2cw-sub",
                             "Advancement: " +
                             a.promotions_to.map((r) =>
                               r.replace(/_/g, " ")).join(", ")));
        for (const roleId of a.promotions_to) {
          const spec = el("button", "v2cw-btn v2cw-promote",
                          "Seek promotion — " + roleId.replace(/_/g, " "));
          spec.type = "button";
          spec.addEventListener("click", () =>
            dispatch(hooks, spec, "careers_promote", { role_id: roleId }));
          promo.appendChild(spec);
        }
        card.appendChild(promo);
      } else if (a.promotion_reason) {
        card.appendChild(el("p", "v2cw-path", a.promotion_reason));
      }
      const resign = el("button", "v2cw-btn v2cw-resign", "Resign");
      resign.type = "button";
      resign.addEventListener("click", () =>
        dispatch(hooks, resign, "careers_resign", {}));
      card.appendChild(resign);
    } else {
      card.appendChild(el("p", "v2cw-none",
                          "Not employed right now. Browse openings below."));
    }
    frag.appendChild(card);

    // ---- openings
    const openings = careers.openings || [];
    if (openings.length) {
      const list = el("section", "v2cw-openings");
      list.appendChild(el("h2", "v2cw-title", "Job openings"));
      let lastPath = null;
      for (const o of openings) {
        if (o.path !== lastPath) {
          lastPath = o.path;
          list.appendChild(el("h3", "v2cw-path",
                              o.path.charAt(0).toUpperCase() +
                              o.path.slice(1)));
        }
        const row = el("article", "v2cw-opening" +
                       (o.eligible ? "" : " v2cw-ineligible"));
        const head = el("div", "v2cw-opening-head");
        head.appendChild(el("span", "v2cw-role", o.role_label));
        head.appendChild(el("span", "v2cw-employer", o.employer_name));
        head.appendChild(el("span", "v2cw-pay",
                            money(o.pay_min) + " – " + money(o.pay_max)));
        row.appendChild(head);
        const shownAsRoutes = renderRoutes(row, o.requirements || {});
        if (!shownAsRoutes) {
          const reqs = requirementLine(o.requirements || {});
          if (reqs.length) {
            const ul = el("ul", "v2cw-reqs");
            for (const r of reqs) ul.appendChild(el("li", "v2cw-req", r));
            row.appendChild(ul);
          }
        }
        if (!o.eligible && o.reason && o.reason_kind !== "requirements") {
          row.appendChild(el("p", "v2cw-reason", o.reason));
        }
        const apply = el("button", "v2cw-btn v2cw-apply", "Apply");
        apply.type = "button";
        apply.disabled = !o.eligible;
        if (!o.eligible) {
          apply.setAttribute("aria-disabled", "true");
          if (o.reason) apply.title = o.reason;
        } else {
          apply.addEventListener("click", () =>
            dispatch(hooks, apply, "careers_apply",
                     { role_id: o.role_id, employer_id: o.employer_id }));
        }
        row.appendChild(apply);
        list.appendChild(row);
      }
      frag.appendChild(list);
    }

    // ---- history
    const hist = careers.history || [];
    if (hist.length) {
      const h = el("section", "v2cw-history");
      h.appendChild(el("h2", "v2cw-title", "Career history"));
      const ul = el("ul", "v2cw-history-list");
      for (const row of hist) {
        ul.appendChild(el("li", "v2cw-history-row",
                          (row.role_label || row.role_id) + " · " +
                          (row.employer_name || row.employer_id) + " · " +
                          money(row.salary)));
      }
      h.appendChild(ul);
      frag.appendChild(h);
    }

    root.appendChild(frag);
  }

  render(data);

  return {
    update(next) { render(next); },
    unmount() { for (const n of [...root.children]) n.remove(); },
  };
}
