/* v2 Education module (Kimi B, M3 §Education). Mounted by the shell under
 * the `job` screen (B applies the registration per contracts.md §Education
 * and §Shell). Follows the §Shell mount API:
 *
 *   const handle = mountEducation(root, hooks, data);  // data = education view
 *   handle.update(data); handle.unmount();
 *
 * Shows the active enrollment (program, year progress, funding, real
 * per-year costs), the program catalog with server-computed eligibility +
 * precise refusal reasons, earned qualifications and enrollment history.
 * Enroll/study/leave dispatch through hooks.sendAction with the frozen
 * action IDs (education_enroll / education_study / education_leave) — the
 * module never calls fetch for mutations and never invents data: costs,
 * requirements, eligibility and reasons all come from the B-owned
 * projection. No stat-effect spoilers (F5): only real costs and server
 * reasons; funding option outcomes (parents/scholarship acceptance) are
 * sampled server-side and never previewed here.
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

function fundingLabel(funding) {
  return {
    self: "Paying each year",
    student_loan: "Student loan",
    parents: "Parents pay",
    scholarship: "Scholarship",
  }[funding] || funding;
}

function dispatch(hooks, btn, actionId, payload) {
  if (typeof hooks.sendAction !== "function") return;
  btn.disabled = true;
  btn.classList.add("v2ed-busy");
  Promise.resolve(hooks.sendAction(actionId, payload, null))
    .catch(() => {})
    .finally(() => btn.classList.remove("v2ed-busy"));
}

function requirementLine(req) {
  const parts = [];
  if (req.min_age) parts.push("age " + req.min_age + "+");
  if (req.secondary_met === false) parts.push("finish secondary school");
  if (req.min_smarts) parts.push("smarts " + req.min_smarts + "+");
  for (const q of req.qualifications || []) {
    parts.push((q.met ? "✓ " : "• ") +
               (q.label || String(q.id).replace(/_/g, " ")));
  }
  return parts;
}

export function mountEducation(root, hooks, data) {
  if (!root) throw new TypeError("mountEducation(root, hooks, data)");
  root.classList.add("v2ed-scope");

  function render(view) {
    for (const n of [...root.children]) n.remove();
    if (!view) {
      root.appendChild(el("p", "v2ed-empty", "No education data yet."));
      return;
    }
    const frag = document.createDocumentFragment();

    // ---- active enrollment
    const card = el("section", "v2ed-card");
    card.appendChild(el("h2", "v2ed-title", "Education"));
    const e = view.enrollment;
    if (e) {
      const head = el("div", "v2ed-enroll-head");
      head.appendChild(el("span", "v2ed-program", e.program_label));
      head.appendChild(el("span", "v2ed-funding",
                          fundingLabel(e.funding)));
      card.appendChild(head);

      const track = el("div", "v2ed-progress");
      const fill = el("div", "v2ed-progress-fill");
      fill.style.width = Math.round(100 * e.progress /
                                    Math.max(1, e.duration_years)) + "%";
      track.appendChild(fill);
      card.appendChild(track);
      card.appendChild(el("p", "v2ed-yearline",
                          "Year " + e.progress + " of " +
                          e.duration_years +
                          (e.attempts ? " · repeated " + e.attempts +
                           (e.attempts === 1 ? " year" : " years") : "")));

      const costs = [];
      if (e.tuition_per_year) {
        costs.push("Tuition " + money(e.tuition_per_year) + " / year");
      }
      if (e.fee_per_year) {
        costs.push("Materials fee " + money(e.fee_per_year) + " / year");
      }
      if (costs.length) {
        card.appendChild(el("p", "v2ed-costs", costs.join(" · ")));
      }
      if (e.studied_this_year) {
        card.appendChild(el("p", "v2ed-studied",
                            "You studied hard this year."));
      }

      const row = el("div", "v2ed-actions");
      if (!e.studied_this_year) {
        const study = el("button", "v2ed-btn v2ed-study",
                         "Study hard this year");
        study.type = "button";
        study.addEventListener("click", () =>
          dispatch(hooks, study, "education_study", {}));
        row.appendChild(study);
      }
      const leave = el("button", "v2ed-btn v2ed-leave", "Leave the program");
      leave.type = "button";
      leave.addEventListener("click", () =>
        dispatch(hooks, leave, "education_leave", {}));
      row.appendChild(leave);
      card.appendChild(row);
    } else {
      card.appendChild(el("p", "v2ed-none",
                          "Not enrolled in a program. Browse programs " +
                          "below."));
    }
    frag.appendChild(card);

    // ---- program catalog
    const programs = view.programs || [];
    if (programs.length) {
      const list = el("section", "v2ed-programs");
      list.appendChild(el("h2", "v2ed-title", "Programs"));
      for (const p of programs) {
        const row = el("article", "v2ed-program-row" +
                         (p.eligible ? "" : " v2ed-ineligible"));
        const head = el("div", "v2ed-program-head");
        head.appendChild(el("span", "v2ed-program", p.label));
        head.appendChild(el("span", "v2ed-credential",
                            p.credential_label));
        row.appendChild(head);
        row.appendChild(el("p", "v2ed-program-meta",
                           p.duration_years + (p.duration_years === 1
                             ? " year" : " years") +
                           " · " +
                           (p.tuition_per_year
                             ? money(p.tuition_per_year) + " / year"
                             : "no tuition") +
                           (p.fee_per_year
                             ? " · fee " + money(p.fee_per_year) + " / year"
                             : "") +
                           " · total " + money(p.total_cost || 0)));
        if (p.apprentice_role) {
          row.appendChild(el("p", "v2ed-apprentice",
                             "Paid apprenticeship — you work while you " +
                             "study."));
        }
        const reqs = requirementLine(p.requirements || {});
        if (reqs.length) {
          const ul = el("ul", "v2ed-reqs");
          for (const r of reqs) ul.appendChild(el("li", "v2ed-req", r));
          row.appendChild(ul);
        }
        const funding = (p.funding_options || []).map(fundingLabel);
        if (funding.length && p.tuition_per_year) {
          row.appendChild(el("p", "v2ed-fundingopts",
                             "Funding: " + funding.join(" · ")));
        }
        if (!p.eligible && p.reason) {
          row.appendChild(el("p", "v2ed-reason", p.reason));
        }
        const enroll = el("button", "v2ed-btn v2ed-enroll", "Enroll");
        enroll.type = "button";
        enroll.disabled = !p.eligible;
        if (!p.eligible) {
          enroll.setAttribute("aria-disabled", "true");
          if (p.reason) enroll.title = p.reason;
        } else {
          enroll.addEventListener("click", () =>
            dispatch(hooks, enroll, "education_enroll",
                     { program_id: p.program_id }));
        }
        row.appendChild(enroll);
        list.appendChild(row);
      }
      frag.appendChild(list);
    }

    // ---- qualifications
    const quals = view.qualifications || [];
    if (quals.length) {
      const q = el("section", "v2ed-quals");
      q.appendChild(el("h2", "v2ed-title", "Qualifications"));
      const ul = el("ul", "v2ed-qual-list");
      for (const qual of quals) {
        ul.appendChild(el("li", "v2ed-qual",
                          qual.label ||
                          String(qual.qualification_id).replace(/_/g, " ")));
      }
      q.appendChild(ul);
      frag.appendChild(q);
    }

    // ---- history
    const hist = view.history || [];
    if (hist.length) {
      const h = el("section", "v2ed-history");
      h.appendChild(el("h2", "v2ed-title", "Past enrollments"));
      const ul = el("ul", "v2ed-history-list");
      for (const row of hist) {
        ul.appendChild(el("li", "v2ed-history-row",
                          row.program_label + " · " +
                          (row.status === "left" ? "left" : row.status) +
                          " · year " + row.progress));
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
