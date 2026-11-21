/* v2 Person detail module (Kimi C, redesign-v2).
 *
 * Presentation-only feature module for the Person mockup
 * (docs/collab/redesign-v2/Person.dc.html). It renders the additive person
 * detail projection (A owns projection/filtering; B owns HTTP exposure) and
 * dispatches through the frozen shell hooks — it never invents data.
 *
 * Contract: docs/collab/redesign-v2/contracts.md — §Shell/§Tokens/§Person
 * are FROZEN (this file consumes them; it does not invent fields).
 *   mountPerson(root, hooks, detail)
 *     root   – empty HTMLElement owned by this module (class "v2p-root").
 *     hooks  – shell hooks (§Shell):
 *       getState()            – normalized adapter state
 *       sendAction(id, payload, targetId)
 *       refresh()             – shell retry/revision machinery
 *       onBack?()             – shell back-stack pop (restores screen+scroll)
 *       onEdit?(personId)     – §Shell (frozen): Edit requests the editor
 *                               for this exact detail.id (never a name);
 *                               shell reveals Activities, opens idempotently
 *                               and pre-selects the ID (B wires Stage 3)
 *       renderAvatar?(el, appearance, age, size) – optional override
 *     detail – §Person detail projection for one person ID
 *       { id, name, age, role|null, alive, appearance,
 *         descriptors: {}   // v1: observer-public only; absent keys omitted
 *         bars: [{ key, label, value (0-100|null) }]  // null = "unknown"
 *         actions: [] }   // exact advertised specs (target-filtered) in
 *                          // WIRE shape (snake_case known_costs/target_id/
 *                          // limits incl. plain strings); §Shell says the
 *                          // shell normalizes once via adapter
 *                          // normalizeAction — until that boundary fix
 *                          // lands we tolerate both dialects read-only
 *   updatePerson(root, detail) – re-render in place (same person ID).
 *   unmountPerson(root)        – detach content, clear the root.
 *
 * Unknown/malformed detail (no id or no name) throws PersonNotFoundError
 * (typed, code "person_not_found").
 *
 * Hard rules honoured here (codex-review.md §5 traps + §1 Person):
 *   - No mock-only data: no Agreement %, no Gift/Holiday/Call tiles unless a
 *     real action spec supplies them, no "From $20/$900" invented prices.
 *   - Prices/reasons come from the action specs only.
 *   - Unknown/missing projection fields render as "unknown" or are omitted,
 *     never faked from editor state or raw model attributes.
 *   - Actions dispatch through hooks.sendAction with the exact spec action ID
 *     and target ID; no local state mutation pretending server acceptance.
 *   - Disabled specs stay disabled (disabled + aria-disabled) with the
 *     server reason visible; opacity is never the disabled mechanism.
 *   - All strings render via textContent; no name interpolation into HTML.
 */

import { renderAvatar as defaultRenderAvatar }
  from "../common.js";

export class PersonNotFoundError extends Error {
  constructor(personId) {
    super("unknown person id: " + String(personId));
    this.name = "PersonNotFoundError";
    this.code = "person_not_found";
    this.personId = personId;
  }
}

const GRID_LIMIT = 8; // tiles before the "More" overflow tile

/* Cosmetic label hints only: glyphs + tile tints keyed off well-known action
 * ids. Eligibility never derives here — it arrives on the spec. */
const ACTION_PRESENTATION = {
  spend_time:    { glyph: "☕", tint: "blue" },
  conversation:  { glyph: "✉", tint: "green" },
  compliment:    { glyph: "★", tint: "amber" },
  insult:        { glyph: "!", tint: "red" },
  ask_for_money: { glyph: "$", tint: "green" },
  // M4 social actions (C-owned ids; presentation hints only — eligibility
  // and reasons always arrive on the spec)
  social_spend_time: { glyph: "☕", tint: "blue" },
  social_befriend:   { glyph: "🤝", tint: "green" },
  social_stand_up:   { glyph: "🛡", tint: "red" },
  social_tell_someone: { glyph: "🗣", tint: "amber" },
};

const SOCIAL_KIND_LABELS = {
  best_friend: "Best friend",
  friend: "Friend",
  acquaintance: "Acquaintance",
  rival: "Rival",
  bully: "Bully",
};

/* M4 social block: the person's social edge (kind + closeness + visible
 * memory lines) lives on the adapter-enriched people entry
 * (state.peopleById[id].social, populated from the M4 social projection)
 * — never faked here. Styled like the Relationships-tab chips: a
 * labelled "Relationship" bar coloured green/amber/red by value, with
 * the kind text beside it. */
function personSocialEdge(hooks, detail) {
  if (typeof hooks.getState !== "function") return null;
  const state = hooks.getState();
  const person = state && state.peopleById
    ? state.peopleById[detail.id] : null;
  return person && person.social ? person.social : null;
}

function socialBlock(hooks, detail) {
  const soc = personSocialEdge(hooks, detail);
  if (!soc) return null;
  const block = el("div", "v2p-social");
  const head = el("div", "v2p-social-head");
  const kindLabel = soc.kind && SOCIAL_KIND_LABELS[soc.kind];
  if (kindLabel) head.append(el("span", "v2p-social-kind", kindLabel));
  if (typeof soc.closeness === "number") {
    head.append(el("span", "v2p-social-rel",
                   "Relationship " + soc.closeness + "%"));
  }
  block.append(head);
  if (typeof soc.closeness === "number") {
    const track = el("div", "v2p-bar-track");
    const fill = el("div", "v2p-bar-fill");
    fill.style.width = Math.max(0, Math.min(100, soc.closeness)) + "%";
    fill.style.background = barColor(soc.closeness, 0);
    track.append(fill);
    block.append(track);
  }
  for (const m of soc.memory || []) {
    block.append(el("div", "v2p-social-memory", m));
  }
  return block;
}

const BAR_COLOR_ORDER = ["var(--v2-bar-1)", "var(--v2-bar-2)",
                         "var(--v2-bar-3)", "var(--v2-bar-4)"];

function el(tag, className, text) {
  const node = document.createElement(tag);
  if (className) node.className = className;
  if (text !== undefined && text !== null) node.textContent = String(text);
  return node;
}

function barColor(value, index) {
  if (typeof value === "number") {
    if (value >= 60) return "var(--v2-bar-good)";
    if (value >= 35) return "var(--v2-bar-mid)";
    return "var(--v2-bar-bad)";
  }
  return BAR_COLOR_ORDER[index % BAR_COLOR_ORDER.length];
}

function validateDetail(detail) {
  if (!detail || typeof detail !== "object" ||
      detail.id === undefined || detail.id === null ||
      typeof detail.name !== "string") {
    throw new PersonNotFoundError(detail && detail.id);
  }
}

function subtitleFrom(detail) {
  const parts = [];
  if (typeof detail.age === "number") parts.push("Age " + detail.age);
  const d = detail.descriptors || {};
  // Only observer-public descriptors; unknown fields are omitted, not faked.
  for (const key of ["job", "faith", "city"]) {
    if (typeof d[key] === "string" && d[key]) parts.push(d[key]);
  }
  return parts.join(" · ");
}

function actionSubLabel(spec) {
  // Frozen adapter shape (adapter.js normalizeAction): eligibility is
  // enabled + reason; costs come from knownCosts, limits from limits.
  if (spec.enabled === false) return spec.reason || "Unavailable";
  // Tolerate the wire dialect (known_costs) until the shell normalizes at
  // its boundary (§Shell S3); read-only, no re-derivation. Cost and limit
  // are independent facets — both must survive into the sub-label.
  const parts = [];
  const costs = spec.knownCosts || spec.known_costs || [];
  const c = costs[0];
  if (c && typeof c.amount === "number") {
    parts.push(c.amount === 0 ? "Free" : "$" + c.amount +
               (costs.length > 1 ? " +" + (costs.length - 1) : ""));
  } else if (c) {
    parts.push(String(c.label || c.kind || ""));
  }
  // §Person wire limits are plain strings, e.g. "once per year per person".
  const lim = (spec.limits || [])[0];
  if (typeof lim === "string") parts.push(lim);
  else if (lim) {
    if (lim.used != null && lim.max != null) parts.push(lim.used + "/" + lim.max);
    else if (lim.label) parts.push(String(lim.label));
  }
  return parts.join(" \u00B7 ");
}

function buildTile(spec, ctx) {
  const pres = ACTION_PRESENTATION[spec.id] || {};
  const tile = el("button",
                  "v2p-action v2p-tint-" + (pres.tint || "neutral"));
  tile.type = "button";
  const ineligible = spec.enabled === false;
  const blocked = ineligible || ctx.deceased || ctx.busy;
  if (blocked) {
    tile.disabled = true;
    tile.setAttribute("aria-disabled", "true");
  }
  if (ineligible && spec.reason) tile.title = spec.reason;

  tile.append(
    el("span", "v2p-action-icon", pres.glyph || "•"),
    el("span", "v2p-action-label", spec.label || spec.id),
    el("span", "v2p-action-sub", actionSubLabel(spec)),
  );

  if (!blocked) {
    tile.addEventListener("click", () => ctx.dispatch(spec));
  }
  return tile;
}

function render(root, detail, ctx) {
  validateDetail(detail);
  root.textContent = "";
  const deceased = detail.alive === false;

  /* Header: back / title / edit */
  const header = el("div", "v2p-header");
  const back = el("button", "v2p-header-btn", "‹");
  back.type = "button";
  back.setAttribute("aria-label", "Back");
  back.addEventListener("click", () => {
    if (typeof ctx.hooks.onBack === "function") ctx.hooks.onBack();
  });
  const title = el("div", "v2p-header-title",
                   // M4: the social kind (Acquaintance/Friend/…) is the
                   // honest label when the projection carries one (the
                   // server role word "relative" is wrong for classmates
                   // — A's role fix is backlogged); fall back to role.
                   (() => {
                     const soc = personSocialEdge(ctx.hooks, detail);
                     const kindLabel = soc && soc.kind &&
                       SOCIAL_KIND_LABELS[soc.kind];
                     return kindLabel || detail.role ||
                            detail.relationship || "Person";
                   })());
  const edit = el("button", "v2p-header-btn", "✎");
  edit.type = "button";
  edit.setAttribute("aria-label", "Edit");
  if (deceased || typeof ctx.hooks.onEdit !== "function") {
    // No editor for the dead, and no dead control when the shell does not
    // offer the route (§Shell: onEdit is the module's only edit path).
    edit.hidden = true;
  } else {
    // §Shell (frozen): pass the exact mounted ID — never a name — so two
    // people with identical names still open the right editor target.
    edit.addEventListener("click", () => ctx.hooks.onEdit(detail.id));
  }
  header.append(back, title, edit);

  /* Card: portrait, name, subtitle, public stat bars */
  const card = el("div", "v2p-card");
  const avatar = el("div", "v2p-avatar");
  card.append(avatar,
              el("div", "v2p-name", deceased ? detail.name + " †"
                                             : detail.name));
  const subtitle = subtitleFrom(detail);
  if (subtitle) card.append(el("div", "v2p-subtitle", subtitle));

  (detail.bars || []).forEach((b, i) => {
    const row = el("div", "v2p-bar");
    const top = el("div", "v2p-bar-top");
    top.append(el("span", "v2p-bar-label", b.label || b.key));
    const known = typeof b.value === "number";
    top.append(el("span", "v2p-bar-value", known ? b.value + "%" : "unknown"));
    const track = el("div", "v2p-bar-track");
    if (known) {
      const fill = el("div", "v2p-bar-fill");
      fill.style.width = Math.max(0, Math.min(100, b.value)) + "%";
      fill.style.background = barColor(b.value, i);
      track.append(fill);
    }
    row.append(top, track);
    card.append(row);
  });

  /* Action grid: only real target-filtered specs, plus overflow "More". */
  const grid = el("div", "v2p-grid");
  const specs = Array.isArray(detail.actions) ? detail.actions : [];
  for (const spec of specs.slice(0, GRID_LIMIT)) {
    grid.append(buildTile(spec, ctx));
  }
  if (specs.length > GRID_LIMIT) {
    const more = el("button", "v2p-action v2p-tint-neutral");
    more.type = "button";
    more.setAttribute("aria-expanded", "false");
    more.append(
      el("span", "v2p-action-icon", "⋯"),
      el("span", "v2p-action-label", "More"),
      el("span", "v2p-action-sub", "+" + (specs.length - GRID_LIMIT)),
    );
    more.addEventListener("click", () => {
      grid.textContent = "";
      for (const spec of specs) grid.append(buildTile(spec, ctx));
    });
    grid.append(more);
  }

  if (deceased) {
    grid.append(el("div", "v2p-deceased-note", "This person has died."));
  }

  // M4: social edge (kind + closeness bar + memory) when the projection
  // carries one for this person; absent edge -> no block, never faked
  const social = socialBlock(ctx.hooks, detail);

  root.append(header, card);
  if (social) root.append(social);
  root.append(grid);

  const paint = ctx.hooks.renderAvatar || defaultRenderAvatar;
  if (paint) {
    Promise.resolve(paint(avatar, detail.appearance || {},
                          detail.age ?? 30, 80))
      .catch(() => { avatar.classList.add("v2p-avatar-fallback"); });
  }
}

export function mountPerson(root, hooks, detail) {
  if (!root || !hooks || typeof hooks.sendAction !== "function") {
    throw new TypeError(
      "mountPerson(root, hooks, detail) requires a root element and " +
      "hooks with sendAction");
  }
  const ctx = {
    hooks,
    busy: false,
    get deceased() {
      return Boolean(ctx._detail && ctx._detail.alive === false);
    },
    _detail: null,
    dispatch(spec) {
      if (ctx.busy) return;
      ctx.busy = true;
      Promise.resolve(hooks.sendAction(
          spec.id, spec.payload || {},
          spec.target_id ?? spec.targetId ?? ctx._detail.id))
        .catch(() => { /* shell retry machinery owns error surfacing */ })
        .finally(() => { ctx.busy = false; });
    },
  };
  ctx._detail = detail || null;
  root.classList.add("v2p-root");
  render(root, detail, ctx);
  root._v2pCtx = ctx;
  return {
    update(next) {
      ctx._detail = next || null;
      render(root, next, ctx);
    },
    unmount() { unmountPerson(root); },
  };
}

export function updatePerson(root, detail) {
  if (!root || !root._v2pCtx) {
    throw new TypeError("updatePerson: root was not mounted");
  }
  root._v2pCtx._detail = detail || null;
  render(root, detail, root._v2pCtx);
}

export function unmountPerson(root) {
  if (!root) return;
  root.textContent = "";
  root.classList.remove("v2p-root");
  delete root._v2pCtx;
}
