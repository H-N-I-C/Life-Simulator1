/* v2 Clubs module (Kimi B, M4 §UI). Mounted by the shell above the
 * Activities card (B applies the registration per M4 02-contracts.md).
 * Follows the §Shell mount API:
 *
 *   const handle = mountClubs(root, hooks, data);  // data = state.social
 *   handle.update(data); handle.unmount();
 *
 * Renders C's club catalog merged with A's membership and C's advertised
 * action specs — nothing else. Join/Leave dispatch the advertised action
 * ids through hooks.sendAction with the frozen {club_id} payload; every
 * enabled/disabled state and reason string comes from the server. The
 * module computes no eligibility (18+ gating is server authority; the
 * "18+" badge renders from the adults_only data flag only).
 */

import { formatMoney } from "../../common.js";

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

function actionButton(hooks, spec, clubId) {
  const enabled = spec.enabled !== false;
  const b = el("button", "v2cl-btn", spec.label);
  b.type = "button";
  b.disabled = !enabled;
  if (!enabled) {
    b.setAttribute("aria-disabled", "true");
    if (spec.reason) {
      b.title = spec.reason;
      b.appendChild(el("span", "v2cl-reason", spec.reason));
    }
    return b;
  }
  if (typeof hooks.sendAction === "function") {
    b.addEventListener("click", () => {
      b.disabled = true;
      b.classList.add("v2cl-busy");
      Promise.resolve(hooks.sendAction(spec.id, { club_id: clubId }, null))
        .catch(() => {})
        .finally(() => b.classList.remove("v2cl-busy"));
    });
  }
  return b;
}

export function mountClubs(root, hooks, data) {
  if (!root) throw new TypeError("mountClubs(root, hooks, data)");
  root.classList.add("v2cl-scope");

  function render(social) {
    for (const n of [...root.children]) n.remove();
    const clubs = social && social.clubs;
    if (!clubs || !clubs.length) {
      // honest absence: no fabricated catalog
      return;
    }
    const card = el("section", "v2cl-card");
    card.appendChild(el("h2", "v2cl-title", "Clubs & teams"));
    for (const club of clubs) {
      const row = el("article", "v2cl-club");
      row.setAttribute("data-club-id", club.club_id);
      const head = el("div", "v2cl-head");
      head.appendChild(el("span", "v2cl-name", club.label));
      if (club.category) {
        head.appendChild(el("span", "v2cl-cat",
                            club.category.charAt(0).toUpperCase() +
                            club.category.slice(1)));
      }
      if (club.adults_only) head.appendChild(el("span", "v2cl-18", "18+"));
      if (club.member) {
        head.appendChild(el("span", "v2cl-member",
                            club.role && club.role !== "member"
                              ? club.role.charAt(0).toUpperCase() +
                                club.role.slice(1)
                              : "Member"));
      }
      row.appendChild(head);
      const meta = [];
      if (club.meets_label) meta.push(club.meets_label);
      meta.push(club.cost_per_year ? formatMoney(club.cost_per_year) + " / year"
                                   : "Free");
      row.appendChild(el("p", "v2cl-meta", meta.join(" · ")));
      for (const spec of club.actions || []) {
        row.appendChild(actionButton(hooks, spec, club.club_id));
      }
      card.appendChild(row);
    }
    root.appendChild(card);
  }

  render(data);

  return {
    update(next) { render(next); },
    unmount() { for (const n of [...root.children]) n.remove(); },
  };
}
