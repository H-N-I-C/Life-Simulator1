/* v2 Home module (Kimi B, redesign-v2): the Life timeline screen's own
 * content. The Age button and the stats dock moved into the shell chrome
 * (feel-pass F3/F7): one big Age button centred in the bottom bar, stats
 * pinned above it — always reachable one-handed. This module keeps only
 * what is unique to the timeline screen: the honest ended-life note.
 * The timeline itself is rendered by game.js: a viewport-filling
 * chronological feed (oldest → newest) per §Journal v2. */

const homeContexts = new WeakMap();

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

export function mountHome(root, hooks) {
  if (!root || !hooks || typeof hooks.getState !== "function") {
    throw new TypeError("mountHome(root, hooks) requires a root and hooks");
  }
  if (homeContexts.has(root)) unmountHome(root);
  root.classList.add("v2h-root");

  const deadNote = el("div", "v2h-dead v2-error");
  deadNote.hidden = true;
  root.appendChild(deadNote);

  const ctx = { mounted: true };
  homeContexts.set(root, ctx);

  function render(state) {
    const dead = state && state.status === "dead";
    deadNote.hidden = !dead;
    if (dead) {
      deadNote.textContent = "This life has ended. The record remains — " +
        "open the More menu to continue as a child or view the archive.";
    }
  }

  render(hooks.getState());
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
