/* M2.5 family tree feature (Kimi A). Renders the frozen graph_tree
 * projection (docs/collab/M2.5/contracts/graph-contract.md §6) with
 * portraits via the shared M0b renderer. The shell route
 * GET /api/games/<id>/family-tree (B) returns {tree, appearances}.
 * Label keys follow the frozen §6.1 namespace; en defaults ship here.
 */
import { renderAvatar } from "../../common.js";

export const LABELS = {
  "graph.union_kind.marriage": "Married",
  "graph.union_kind.civil_partnership": "Civil partnership",
  "graph.union_kind.handfasting": "Handfasted",
  "graph.union_kind.consort_union": "Consort union",
  "graph.union_status.active": "Together",
  "graph.union_status.divorced": "Divorced",
  "graph.union_status.dissolved": "Separated",
  "graph.union_status.widowed": "Widowed",
  "graph.recognition.recognized": "Recognized",
  "graph.recognition.civil_only": "Civil only",
  "graph.recognition.unrecognized": "Not recognized",
  "graph.recognition.forbidden": "Forbidden",
  "graph.parent_role.biological": "Biological",
  "graph.parent_role.adoptive": "Adoptive",
  "graph.parent_role.step": "Step",
  "graph.parent_role.guardian": "Guardian",
  "graph.parent_role.social": "Social",
  "graph.flag.deceased": "Deceased",
  "graph.flag.ex": "Ex",
  "graph.flag.secret": "Secret",
  "graph.flag.unknown_parent": "Parentage unknown",
  "graph.flag.adoptive_parent_edge": "Adoptive parent",
};

const t = (key) => LABELS[key] || key;

let stylesInjected = false;
function injectStyles() {
  if (stylesInjected) return;
  stylesInjected = true;
  const css = `
  .ftree { display: flex; flex-direction: column; gap: 18px; }
  .ftree-gen { display: flex; flex-wrap: wrap; gap: 14px; justify-content: center; }
  .ftree-node { display: flex; flex-direction: column; align-items: center;
    gap: 6px; min-width: 96px; }
  .ftree-node .avatar { width: 64px; height: 64px; border-radius: 50%;
    overflow: hidden; background: var(--card, #23283a); }
  .ftree-node.deceased .avatar { filter: grayscale(1); opacity: .65; }
  .ftree-node .nm { font-weight: 600; }
  .ftree-node.deceased .nm, .ftree-node.ex .nm { opacity: .65;
    font-style: italic; }
  .ftree-badges { display: flex; flex-wrap: wrap; gap: 4px;
    justify-content: center; }
  .ftree-badge { font-size: 11px; padding: 1px 8px; border-radius: 999px;
    border: 1px solid currentColor; }
  .ftree-badge.kind { opacity: .8; }
  .ftree-badge.flag { color: #c9a24b; }
  .ftree-links { display: flex; flex-wrap: wrap; gap: 8px;
    justify-content: center; opacity: .9; }
  .ftree-link { font-size: 12px; display: inline-flex; gap: 6px;
    align-items: center; }
  .ftree-link .dash { width: 26px; border-top: 2px solid #7a819e; }
  .ftree-link.secret .dash { border-top-style: dashed; color: #c9a24b; }
  .ftree-link.union .dash { border-top-style: double; }
  .ftree-empty { opacity: .7; text-align: center; padding: 24px 0; }
  `;
  const el = document.createElement("style");
  el.textContent = css;
  document.head.appendChild(el);
}

function depthMap(tree) {
  const byId = Object.fromEntries(tree.nodes.map((n) => [n.person_id, n]));
  const depth = { [tree.focus_id]: 0 };
  // edges are few: relax until stable (bounded)
  for (let i = 0; i < tree.nodes.length; i++) {
    for (const e of tree.edges) {
      if (e.kind === "parentage") {
        if (e.from_id in depth && !(e.to_id in depth))
          depth[e.to_id] = depth[e.from_id] + 1;
        else if (e.to_id in depth && !(e.from_id in depth))
          depth[e.from_id] = depth[e.to_id] - 1;
      } else if (e.from_id in depth && !(e.to_id in depth)) {
        depth[e.to_id] = depth[e.from_id];
      } else if (e.to_id in depth && !(e.from_id in depth)) {
        depth[e.from_id] = depth[e.to_id];
      }
    }
  }
  for (const id of Object.keys(byId)) if (!(id in depth)) depth[id] = 0;
  return { byId, depth };
}

function nodeBadges(node, tree) {
  const badges = [];
  for (const f of node.flags || []) {
    badges.push(`<span class="ftree-badge flag">${t("graph.flag." + f)}</span>`);
  }
  return badges.join("");
}

function linkRow(e, byId) {
  const a = byId[e.from_id], b = byId[e.to_id];
  if (!a || !b) return "";
  const cls = ["ftree-link", e.kind,
               (e.flags || []).includes("secret") ? "secret" : ""].join(" ");
  const label = e.kind === "parentage"
    ? t("graph.parent_role." + (e.flags || [])[0])
    : (e.flags || []).map((f) => t("graph.union_status." + f)).join(" · ");
  return `<span class="${cls}"><span class="dash"></span>` +
         `${a.name} — ${b.name} <em>${label}</em></span>`;
}

export async function mountFamilyTree(container, gameId, { focus } = {}) {
  injectStyles();
  container.innerHTML = `<p class="muted">Loading family tree…</p>`;
  const qs = focus ? `?focus=${encodeURIComponent(focus)}` : "";
  const res = await fetch(`/api/games/${encodeURIComponent(gameId)}/family-tree${qs}`, { cache: "no-store" });
  if (!res.ok) {
    container.innerHTML = `<p class="ftree-empty">The family tree could not be loaded (${res.status}).</p>`;
    return;
  }
  const { tree, appearances } = await res.json();
  if (!tree.nodes.length) {
    container.innerHTML = `<p class="ftree-empty">No family recorded yet.</p>`;
    return;
  }
  const { byId, depth } = depthMap(tree);
  const generations = new Map();
  for (const n of tree.nodes) {
    const d = depth[n.person_id];
    if (!generations.has(d)) generations.set(d, []);
    generations.get(d).push(n);
  }
  const ordered = [...generations.keys()].sort((a, b) => a - b);
  container.innerHTML = "";
  const root = document.createElement("div");
  root.className = "ftree";
  for (const d of ordered) {
    const row = document.createElement("div");
    row.className = "ftree-gen";
    row.dataset.generation = d;
    for (const node of generations.get(d)) {
      const el = document.createElement("div");
      el.className = "ftree-node" +
        ((node.flags || []).includes("deceased") ? " deceased" : "") +
        ((node.flags || []).includes("ex") ? " ex" : "");
      const av = document.createElement("div");
      av.className = "avatar";
      el.appendChild(av);
      const app = appearances[node.person_id];
      renderAvatar(av, app || {}, node.age ?? 30, 128);
      el.insertAdjacentHTML("beforeend",
        `<div class="nm">${node.name}</div>` +
        `<div class="muted">${node.living ? "age " + node.age : ""}</div>` +
        `<div class="ftree-badges">${nodeBadges(node, tree)}</div>`);
      row.appendChild(el);
    }
    root.appendChild(row);
  }
  const links = document.createElement("div");
  links.className = "ftree-links";
  for (const e of tree.edges) {
    links.insertAdjacentHTML("beforeend", linkRow(e, byId));
  }
  root.appendChild(links);
  container.innerHTML = "";
  container.appendChild(root);
}

export default { mountFamilyTree, LABELS };
