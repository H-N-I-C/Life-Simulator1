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

/* ================= B1: global error boundary =================
 * Every uncaught error / unhandled rejection is reported to
 * POST /api/client-error (fire-and-forget; failures are swallowed) and
 * surfaces a friendly card instead of leaving a broken page.
 *
 * FROZEN report shape (A1, docs/collab/q1-a1-report.md):
 *   POST /api/client-error, JSON {kind, message, stack?, url?, game_id?}
 *   — all strings; message required. We map type -> kind, page -> url,
 *   and fold source/lineno/colno into the stack string. No extra fields
 *   without agreement with A. Not a mutation: no request_id. */

const CLIENT_ERROR_URL = "/api/client-error";
const MAX_REPORTS = 20;
let reportCount = 0;
const lastReportAt = new Map();
let errorRetryHandler = null;
let crashCard = null;

/* Pages register what "Retry" should do (e.g. reload game state).
 * Default: reload the page, which always returns to a good state. */
export function setErrorRetryHandler(fn) { errorRetryHandler = fn; }

export function reportClientError(detail) {
  if (reportCount >= MAX_REPORTS) return;
  const message = String((detail && detail.message) || "unknown error")
    .slice(0, 500);
  const now = Date.now();
  // Dedupe bursts of the identical error (e.g. per-frame loop).
  if (now - (lastReportAt.get(message) || 0) < 5000) return;
  lastReportAt.set(message, now);
  reportCount++;
  // Fold position info into the stack string (frozen shape has no
  // source/lineno/colno fields).
  const stackParts = [];
  if (detail && detail.source) {
    stackParts.push("at " + detail.source + ":" +
                    ((detail.lineno != null) ? detail.lineno : 0) + ":" +
                    ((detail.colno != null) ? detail.colno : 0));
  }
  if (detail && detail.stack) stackParts.push(String(detail.stack));
  const payload = { kind: (detail && detail.type) || "error",
                    message: message };
  if (stackParts.length) payload.stack = stackParts.join("\n");
  try {
    payload.url = location.pathname;
  } catch (e) { /* location unavailable */ }
  try {
    const gid = sessionStorage.getItem("game_id");
    if (gid) payload.game_id = String(gid);
  } catch (e) { /* storage unavailable */ }
  const body = JSON.stringify(payload);
  try {
    fetch(CLIENT_ERROR_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body,
      keepalive: true,
    }).catch(() => { /* endpoint absent or offline — never cascade */ });
  } catch (e) {
    try {
      navigator.sendBeacon && navigator.sendBeacon(CLIENT_ERROR_URL,
        new Blob([body], { type: "application/json" }));
    } catch (e2) { /* ignore */ }
  }
}

/* Friendly full-page card shown when something uncaught happens.
 * Reassures the player their save is intact and offers a way forward. */
export function showCrashCard(message) {
  if (!document.body) return;
  if (crashCard && crashCard.isConnected) {
    const msg = crashCard.querySelector(".crash-detail");
    if (msg && message) msg.textContent = String(message);
    return;
  }
  const overlay = document.createElement("div");
  overlay.className = "crash-overlay";
  overlay.setAttribute("role", "alertdialog");
  overlay.setAttribute("aria-modal", "true");
  overlay.setAttribute("aria-labelledby", "crashTitle");
  const card = document.createElement("div");
  card.className = "crash-card";
  const title = document.createElement("h2");
  title.id = "crashTitle";
  title.textContent = "Something went wrong";
  const body = document.createElement("p");
  body.textContent = "Something went wrong. Your save is safe.";
  const detail = document.createElement("p");
  detail.className = "muted crash-detail";
  detail.textContent = message ? String(message) : "";
  const row = document.createElement("div");
  row.className = "row";
  const retry = document.createElement("button");
  retry.className = "primary";
  retry.textContent = "Retry";
  retry.addEventListener("click", () => {
    hideCrashCard();
    if (errorRetryHandler) {
      try { errorRetryHandler(); return; } catch (e) { /* fall through */ }
    }
    location.reload();
  });
  const reload = document.createElement("button");
  reload.textContent = "Reload";
  reload.addEventListener("click", () => location.reload());
  row.appendChild(retry);
  row.appendChild(reload);
  card.appendChild(title);
  card.appendChild(body);
  card.appendChild(detail);
  card.appendChild(row);
  overlay.appendChild(card);
  document.body.appendChild(overlay);
  crashCard = overlay;
  retry.focus();
}

export function hideCrashCard() {
  if (crashCard && crashCard.isConnected) crashCard.remove();
  crashCard = null;
}

function installErrorBoundary() {
  window.addEventListener("error", (e) => {
    reportClientError({
      type: "error",
      message: e.message,
      stack: e.error && e.error.stack,
      source: e.filename,
      lineno: e.lineno,
      colno: e.colno,
    });
    showCrashCard(e.message || "Unexpected error");
  });
  window.addEventListener("unhandledrejection", (e) => {
    const r = e.reason;
    reportClientError({
      type: "unhandledrejection",
      message: (r && r.message) || String(r),
      stack: r && r.stack,
    });
    showCrashCard((r && r.message) || String(r));
  });
}
installErrorBoundary();

/* ---- offline banner: visible on every page whenever the connection
 * drops, so a failing action is never a mystery. ---- */
function updateOfflineBanner() {
  let b = document.getElementById("offlineBanner");
  if (navigator.onLine !== false) {
    if (b) b.hidden = true;
    return;
  }
  if (!b) {
    b = document.createElement("div");
    b.id = "offlineBanner";
    b.className = "offline-banner";
    b.setAttribute("role", "status");
    b.textContent = "You are offline. Your saves are stored on this " +
      "device — the game will respond again when the connection returns.";
    document.body.prepend(b);
  }
  b.hidden = false;
}
window.addEventListener("online", updateOfflineBanner);
window.addEventListener("offline", updateOfflineBanner);
updateOfflineBanner();

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
