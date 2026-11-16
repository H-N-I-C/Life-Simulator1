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

/* Sequenced avatar rendering: concurrent calls cannot let an older
   response overwrite a newer one, and failures keep the previous image.
   Returns true on success, false on failure. */
export async function renderAvatar(container, appearance, age, size) {
  const seq = (container._renderSeq || 0) + 1;
  container._renderSeq = seq;
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
    const text = await res.text();
    if (container._renderSeq !== seq) return true;  // superseded
    container.innerHTML = text;
    container.title = "";
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
