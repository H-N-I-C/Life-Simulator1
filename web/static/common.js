/* Shared helpers: catalog load, sequenced avatar preview, person chip. */
"use strict";

let CATALOG = null;

export async function loadCatalog() {
  if (CATALOG) return CATALOG;
  const res = await fetch("/api/catalog");
  if (!res.ok) throw new Error("catalog load failed");
  CATALOG = await res.json();
  return CATALOG;
}

/* M0b avatar rendering: the server resolves a Fluent 3D emoji PNG;
   the client draws it in a circle on the appearance's background colour
   with badge overlays in the corner. Sequenced so an older response can
   never overwrite a newer one; failures keep the previous image. */
const backgroundCache = {};

async function backgroundHex(backgroundId) {
  if (backgroundId in backgroundCache) {
    return backgroundCache[backgroundId];
  }
  try {
    const data = await loadCatalog();
    const entries = (data.catalog.background) || [];
    const hit = entries.find((e) => e.id === backgroundId);
    backgroundCache[backgroundId] = (hit && hit.hex) || "#eeeae4";
  } catch (e) {
    backgroundCache[backgroundId] = "#eeeae4";
  }
  return backgroundCache[backgroundId];
}

export async function renderAvatar(container, appearance, age, size) {
  const seq = (container._renderSeq || 0) + 1;
  container._renderSeq = seq;
  appearance = appearance || {};
  try {
    const res = await fetch("/api/render", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ appearance, age, size }),
    });
    if (container._renderSeq !== seq) return true;  // superseded
    if (!res.ok) {
      let msg = "portrait request failed (" + res.status + ")";
      try {
        const body = await res.json();
        if (body && body.error && body.error.message) msg = body.error.message;
      } catch (e) { /* keep generic message */ }
      container.title = msg;
      console.warn(msg);
      return false;
    }
    const blob = await res.blob();
    if (container._renderSeq !== seq) return true;  // superseded
    const url = URL.createObjectURL(blob);
    const img = document.createElement("img");
    img.src = url;
    img.alt = "";
    img.width = size;
    img.height = size;
    container.innerHTML = "";
    container.appendChild(img);
    container.title = "";
    backgroundHex(appearance.background).then((hex) => {
      if (container._renderSeq === seq) container.style.background = hex;
    });
    // badge overlays (babies/children carry none per the guardrail)
    const badges = Array.isArray(appearance.badges)
      ? appearance.badges : [];
    if (badges.length && age > 12) { // guardrail: children render no badges
      const corner = document.createElement("span");
      corner.className = "badge-corner";
      for (const b of badges.slice(0, 2)) { // contract cap: up to 2
        const bi = document.createElement("img");
        bi.src = "/static/avatars/badge_" + encodeURIComponent(b) + ".png";
        bi.alt = "";
        bi.className = "badge-icon";
        corner.appendChild(bi);
      }
      container.appendChild(corner);
    }
    return true;
  } catch (err) {
    if (container._renderSeq === seq) {
      container.title = String(err);
      console.warn("portrait render failed", err);
    }
    return false;
  }
}

/* Build a "person chip": avatar + name + role + optional relationship bar. */
export function personChip(person, opts = {}) {
  const chip = document.createElement("div");
  chip.className = "chip";
  const avatar = document.createElement("div");
  avatar.className = "avatar";
  chip.appendChild(avatar);
  const who = document.createElement("div");
  who.className = "who";
  const name = document.createElement("div");
  name.className = "name";
  name.textContent = person.name || "???";
  const role = document.createElement("div");
  role.className = "role";
  role.textContent = person.role || "";
  who.appendChild(name);
  who.appendChild(role);
  chip.appendChild(who);
  if (opts.relationship != null) {
    const bar = document.createElement("div");
    bar.className = "relbar";
    const track = document.createElement("div");
    track.className = "track";
    const fill = document.createElement("div");
    fill.className = "fill";
    const pct = Math.max(0, Math.min(100, opts.relationship));
    fill.style.width = pct + "%";
    track.appendChild(fill);
    const val = document.createElement("span");
    val.className = "val";
    val.textContent = pct + "%";
    bar.appendChild(track);
    bar.appendChild(val);
    chip.appendChild(bar);
  }
  renderAvatar(avatar, person.appearance || person, person.age ?? 30, 96);
  return chip;
}

/* Mini avatar+name chip for inline person references (timeline, decisions,
   lists). Uses the shared portrait renderer. */
export function miniPersonChip(person, size = 28) {
  const chip = document.createElement("span");
  chip.className = "chip mini";
  const av = document.createElement("span");
  av.className = "avatar mini";
  av.style.width = size + "px";
  av.style.height = size + "px";
  const name = document.createElement("span");
  name.textContent = person.name + (person.alive ? "" : " \u2020");
  chip.appendChild(av);
  chip.appendChild(name);
  renderAvatar(av, person.appearance || {}, person.age ?? 30, size * 2);
  return chip;
}

/* Simple seeded RNG (mulberry32) for randomize. */
export function rng(seed) {
  let a = seed >>> 0;
  return function () {
    a |= 0; a = (a + 0x6d2b79f5) | 0;
    let t = Math.imul(a ^ (a >>> 15), 1 | a);
    t = (t + Math.imul(t ^ (t >>> 7), 61 | t)) ^ t;
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}

export function pick(rand, arr) { return arr[Math.floor(rand() * arr.length)]; }
