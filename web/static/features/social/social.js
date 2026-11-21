/* v2 Social module (Kimi B, M4 §UI). Mounted by the shell at the top of
 * the Relationships screen (B applies the registration per M4
 * 02-contracts.md). Follows the §Shell mount API:
 *
 *   const handle = mountSocial(root, hooks, data);  // data = state.social
 *   handle.update(data); handle.unmount();
 *
 * Renders the server projection verbatim: popularity, the social groups
 * (Family / Friends / Classmates / Coworkers) with person chips
 * (data-person-id, avatar, relation · kind label, closeness bar with %)
 * and the invites inbox (status, host, venue, 18+ badge). Open invites
 * respond ONLY through the frozen pending envelope (hooks.resolveDecision
 * with the advertised option ids) — the module never invents options,
 * never computes eligibility. Memory lines render exactly as the server
 * published them (player_known-filtered).
 */

import { renderAvatar } from "../../common.js";

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

/* Avatar: initial-letter fallback immediately; the real portrait renders
 * async into the same box when the server avatar endpoint answers (in a
 * bare test host the fetch fails and the letter stays). */
function avatarBox(p) {
  const box = el("span", "v2so-avatar",
                 (p.name || "?").trim().charAt(0).toUpperCase() || "?");
  if (p.appearance && p.alive !== false) {
    try {
      renderAvatar(box, p.appearance, p.age, 40);
    } catch (e) { /* letter fallback stays */ }
  }
  return box;
}

function bar(value) {
  const v = Math.max(0, Math.min(100, value));
  const track = el("div", "v2so-bar");
  const fill = el("div", "v2so-bar-fill");
  fill.style.width = v + "%";
  if (v < 35) fill.classList.add("v2so-bar-low");
  else if (v < 70) fill.classList.add("v2so-bar-mid");
  track.appendChild(fill);
  return track;
}

const KIND_BADGES = {
  best_friend: "Best friend",
  friend: "Friend",
  acquaintance: "Acquaintance",
  rival: "Rival",
  bully: "Bully",
};

const GROUP_RELATION_WORDS = {
  family: "Family",
  friends: "Friend",
  classmates: "Classmate",
  coworkers: "Coworker",
};

/* "Classmate · Friend" — the relation word comes from the (frozen)
 * group vocabulary or the person's role (family), the kind label from
 * the projection; both are display labels, never ids. */
function relationLabel(groupId, p) {
  let rel = null;
  if (groupId === "family" && p.role) rel = p.role;
  if (!rel) rel = GROUP_RELATION_WORDS[groupId] || null;
  const kind = p.social && p.social.kind;
  const kindLabel = kind && KIND_BADGES[kind];
  if (rel && kindLabel && rel !== kindLabel) return rel + " · " + kindLabel;
  return rel || kindLabel || "";
}

function personChip(hooks, p, groupId) {
  const chip = el("button", "v2so-chip", "");
  chip.type = "button";
  chip.setAttribute("data-person-id", p.id);
  const dead = p.alive === false;
  chip.appendChild(avatarBox(p));
  const kind = p.social && p.social.kind;
  const body = el("span", "v2so-chip-body");
  const head = el("span", "v2so-chip-head");
  head.appendChild(el("span", "v2so-chip-name",
                      p.name + (dead ? " †" : "")));
  const rel = relationLabel(groupId, p);
  if (rel) {
    const relEl = el("span", "v2so-rel", rel);
    if (kind === "rival" || kind === "bully") {
      relEl.classList.add("v2so-rel-hostile");
    }
    head.appendChild(relEl);
  }
  body.appendChild(head);
  const soc = p.social || {};
  if (typeof soc.closeness === "number" && !dead) {
    const row = el("span", "v2so-chip-bar");
    row.appendChild(bar(soc.closeness));
    row.appendChild(el("span", "v2so-chip-pct", soc.closeness + "%"));
    body.appendChild(row);
  } else if (typeof p.relationship === "number" && !dead) {
    // family rows: the existing relationship value is the bar source
    const row = el("span", "v2so-chip-bar");
    row.appendChild(bar(p.relationship));
    row.appendChild(el("span", "v2so-chip-pct", p.relationship + "%"));
    body.appendChild(row);
  }
  for (const m of soc.memory || []) {
    body.appendChild(el("span", "v2so-memory", m));
  }
  chip.appendChild(body);
  if (!dead && typeof hooks.navigate === "function") {
    chip.addEventListener("click", () =>
      hooks.navigate("person", { personId: p.id }));
  } else {
    chip.disabled = dead;
  }
  return chip;
}

const STATUS_LABELS = {
  open: "Open", accepted: "Accepted",
  declined: "Declined", expired: "Expired",
};

export function mountSocial(root, hooks, data) {
  if (!root) throw new TypeError("mountSocial(root, hooks, data)");
  root.classList.add("v2so-scope");

  function render(social) {
    for (const n of [...root.children]) n.remove();
    if (!social) {
      root.appendChild(el("p", "v2so-empty", "No social data yet."));
      return;
    }
    const frag = document.createDocumentFragment();

    // ---- popularity indicator: game-y ("Popular · 72%"), delta only
    // when it moved; the meaningless floor band ("unknown") hides the
    // indicator entirely rather than reading "Unknown · 0 (0)"
    const pop = social.popularity;
    if (pop && pop.band && pop.band !== "unknown") {
      const p = el("div", "v2so-pop");
      const head = el("span", "v2so-pop-label", "Popularity");
      const val = el("span", "v2so-pop-value",
                     (pop.label || pop.band) + " · " + pop.score + "%");
      const d = pop.delta_last_year;
      if (typeof d === "number" && d !== 0) {
        val.appendChild(el("span", "v2so-pop-delta" +
                                (d < 0 ? " v2so-pop-down" : ""),
                           (d > 0 ? " (+" : " (") + d + ")"));
      }
      p.appendChild(head);
      p.appendChild(val);
      frag.appendChild(p);
    }

    // ---- groups (frozen bucket order; empty groups hidden honestly)
    for (const g of social.groups || []) {
      if (!g.people || !g.people.length) continue;
      const card = el("section", "v2so-group");
      card.setAttribute("data-group-id", g.group_id);
      card.appendChild(el("h2", "v2so-group-title", g.label));
      const list = el("div", "v2so-chips");
      for (const p of g.people) list.appendChild(personChip(hooks, p, g.group_id));
      card.appendChild(list);
      frag.appendChild(card);
    }

    // ---- invites inbox (newest first)
    const invites = social.invites || [];
    if (invites.length) {
      const box = el("section", "v2so-invites");
      box.appendChild(el("h2", "v2so-group-title", "Invites"));
      for (const inv of invites) {
        const row = el("article", "v2so-invite v2so-invite-" + inv.status);
        row.setAttribute("data-invite-id", inv.invite_id);
        const head = el("div", "v2so-invite-head");
        head.appendChild(el("span", "v2so-invite-title", inv.title));
        head.appendChild(el("span", "v2so-invite-status",
                            STATUS_LABELS[inv.status] || inv.status));
        if (inv.adults_only) {
          head.appendChild(el("span", "v2so-18", "18+"));
        }
        row.appendChild(head);
        const meta = el("div", "v2so-invite-meta");
        if (inv.host) {
          const host = personChip(hooks, inv.host);
          host.classList.add("v2so-host");
          meta.appendChild(host);
        }
        if (inv.venue_label) {
          meta.appendChild(el("span", "v2so-venue", inv.venue_label));
        }
        row.appendChild(meta);
        // open invites respond through the frozen pending envelope only,
        // using the ADVERTISED option ids from the matching decision
        if (inv.status === "open" && inv.decision_id &&
            typeof hooks.getState === "function" &&
            typeof hooks.resolveDecision === "function") {
          const state = hooks.getState();
          const pending = state && state.pending;
          if (pending && pending.id === inv.decision_id) {
            const acts = el("div", "v2so-invite-actions");
            for (const opt of pending.options || []) {
              const b = el("button", "v2so-invite-btn", opt.label);
              b.type = "button";
              b.disabled = opt.enabled === false;
              if (opt.enabled === false) {
                b.setAttribute("aria-disabled", "true");
                if (opt.reason) {
                  b.title = opt.reason;
                  b.appendChild(el("span", "v2so-invite-reason",
                                   opt.reason));
                }
              } else {
                b.addEventListener("click", () => {
                  b.disabled = true;
                  b.classList.add("v2so-busy");
                  Promise.resolve(hooks.resolveDecision(
                    pending.id, opt.id, {}))
                    .catch(() => {})
                    .finally(() => b.classList.remove("v2so-busy"));
                });
              }
              acts.appendChild(b);
            }
            row.appendChild(acts);
          }
        }
        box.appendChild(row);
      }
      frag.appendChild(box);
    }

    root.appendChild(frag);
  }

  render(data);

  return {
    update(next) { render(next); },
    unmount() { for (const n of [...root.children]) n.remove(); },
  };
}
