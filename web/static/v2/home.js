/* v2 Home module (Kimi B, redesign-v2): the central Age control + the real
 * stats sheet, per the Home mockup and contracts.md §Home. Real data only:
 * stats come from the normalized state, Age eligibility from the server's
 * advertised age_up spec (enabled/reason) — never a local age increment or
 * Math.random (codex-review.md §5.2). The timeline above is rendered by
 * game.js (newest-first, frozen §Home). */

const homeContexts = new WeakMap();

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

function statColor(v) {
  return v >= 60 ? "var(--v2-bar-good)"
                 : v >= 35 ? "var(--v2-bar-mid)" : "var(--v2-bar-bad)";
}

function cap(s) { return s ? s[0].toUpperCase() + s.slice(1) : s; }

export function mountHome(root, hooks) {
  if (!root || !hooks || typeof hooks.sendAction !== "function") {
    throw new TypeError("mountHome(root, hooks) requires a root and hooks");
  }
  if (homeContexts.has(root)) unmountHome(root);
  root.classList.add("v2h-root");

  const agebar = el("div", "v2h-agebar");
  const ageBtn = el("button", "v2h-age", "+");
  ageBtn.type = "button";
  ageBtn.setAttribute("aria-label", "Age up");
  ageBtn.append(el("span", "v2h-age-plus", "+"),
                el("span", "v2h-age-word", "AGE"));
  const ageNote = el("div", "v2h-age-note");
  agebar.append(ageBtn, ageNote);

  const statgrid = el("div", "v2h-stats");

  const deadNote = el("div", "v2h-dead v2-error");
  deadNote.hidden = true;

  root.append(agebar, statgrid, deadNote);

  const ctx = { busy: false, mounted: true };
  homeContexts.set(root, ctx);

  function render(state) {
    const dead = state.status === "dead";
    deadNote.hidden = !dead;
    if (dead) {
      deadNote.textContent = "This life has ended. The record remains — " +
        "open the More menu to continue as a child or view the archive.";
      agebar.hidden = true;
      statgrid.hidden = true;
      return;
    }
    agebar.hidden = false;
    statgrid.hidden = false;

    // Age eligibility is the server's advertised spec — including the
    // pending-decision gate and its precise reason.
    const spec = (state.actions || []).find((a) => a.id === "age_up");
    const blocked = ctx.busy || !spec || spec.enabled === false;
    ageBtn.disabled = blocked;
    ageBtn.setAttribute("aria-disabled", String(blocked));
    ageNote.textContent = (spec && spec.enabled === false && spec.reason)
      ? spec.reason
      : (ctx.busy ? "Ageing…" : "");

    statgrid.textContent = "";
    const entries = Object.entries(state.stats || {});
    if (!entries.length) {
      statgrid.append(el("p", "v2-loading", "No stats yet."));
    }
    for (const [k, v] of entries) {
      const pct = Math.max(0, Math.min(100, typeof v === "number" ? v : 0));
      const row = el("div", "v2h-stat");
      const top = el("div", "v2h-stat-top");
      const val = el("span", "v2h-stat-value", pct + "%");
      top.append(el("span", "v2h-stat-label", cap(k)), val);
      // Colour is never the only signal: the numeric label always renders.
      if (typeof v === "number") val.style.color = statColor(pct);
      const track = el("div", "v2h-stat-track");
      if (typeof v === "number") {
        const fill = el("div", "v2h-stat-fill");
        fill.style.width = pct + "%";
        fill.style.background = statColor(pct);
        track.appendChild(fill);
      }
      row.append(top, track);
      statgrid.appendChild(row);
    }
  }

  ageBtn.addEventListener("click", () => {
    if (ageBtn.disabled) return;
    ctx.busy = true;
    ageBtn.disabled = true;
    ageNote.textContent = "Ageing…";
    // The shell renders before its promise settles. Repaint after clearing
    // local busy, using the latest spec (including pending/death gates).
    Promise.resolve(hooks.sendAction("age_up", {}, null))
      .catch(() => {})
      .finally(() => {
        ctx.busy = false;
        if (!ctx.mounted) return;
        const current = hooks.getState();
        if (current) render(current);
      });
  });

  render(hooks.getState && hooks.getState());
  return {
    update(state) { if (ctx.mounted) render(state); },
    unmount() {
      if (homeContexts.get(root) === ctx) unmountHome(root);
    },
  };
}

export function unmountHome(root) {
  if (!root) return;
  const ctx = homeContexts.get(root);
  if (ctx) ctx.mounted = false;
  homeContexts.delete(root);
  root.textContent = "";
  root.classList.remove("v2h-root");
}
