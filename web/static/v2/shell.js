/* v2 shell — navigation chrome + module host (Kimi B, redesign-v2).
 * Implements the frozen §Shell contract (docs/collab/redesign-v2/
 * contracts.md): five primary destinations, header with secondary "More"
 * routes, back-stack/scroll restore, and the mount/update/unmount lifecycle
 * host for feature modules (Person today; Home/Choice in Stage 2).
 *
 * The shell is chrome only: game.js keeps owning state, rendering of the
 * legacy panels, the retry-stable command machinery, decision modal, pills
 * and focus trap. The shell never mutates game state and never fabricates
 * data — an unavailable person projection renders an honest error state.
 */

import { normalizeAction } from "../adapter.js";

export const V2_SCREENS = ["home", "job", "assets", "people", "activities"];

const TABS = [
  { id: "home",       ic: "◷", label: "Life" },
  { id: "job",        ic: "▣", label: "Job" },
  { id: "assets",     ic: "⌂", label: "Money" },
  { id: "people",     ic: "☺", label: "People" },
  { id: "activities", ic: "✦", label: "Health" },
];

const MORE_ROUTES = [
  { id: "allLives",   label: "All lives" },
  { id: "editor",     label: "Character editor" },
  { id: "familyTree", label: "Family tree" },
  { id: "m25",        label: "Dating & weddings" },
];

function el(tag, className, text) {
  const n = document.createElement(tag);
  if (className) n.className = className;
  if (text !== undefined && text !== null) n.textContent = String(text);
  return n;
}

/* Subtitle for the header: real status only — actual job/status and
 * country, with student/unemployed/retired fallbacks (codex §1 Home). */
function headerSubtitle(state) {
  const parts = [];
  const w = state.sections.work;
  const ed = state.sections.education;
  if (w && w.employed && w.job_label) parts.push(w.job_label);
  else if (w && w.retired) parts.push("Retired");
  else if (ed && ed.enrolled) parts.push("Student");
  else if (w) parts.push("Not employed");
  if (state.country) parts.push(state.country);
  return parts.join(" · ");
}

export function mountShell(host, hooks, opts = {}) {
  if (!host || !hooks || typeof hooks.getState !== "function") {
    throw new TypeError("mountShell(host, hooks) requires a host element " +
                        "and shell hooks");
  }
  host.classList.add("v2-scope");

  // --- chrome -----------------------------------------------------------
  const header = el("header", "v2-header");
  // USER BUG FIX: one consistent visible Back control, top-left, on every
  // non-Home view (and Home while an overlay/panel/menu is open). Priority:
  // More menu → person overlay → feature panel → previous screen.
  const backBtn = el("button", "v2-header-btn v2-back", "‹");
  backBtn.type = "button";
  backBtn.setAttribute("aria-label", "Back");
  backBtn.addEventListener("click", () => backOut());
  const homeLink = el("button", "v2-home-link", "Life");
  homeLink.type = "button";
  homeLink.setAttribute("aria-label", "Home — life timeline");
  homeLink.addEventListener("click", () => {
    closeMore();
    if (typeof opts.selectScreen === "function") opts.selectScreen("home");
  });
  const id = el("div", "v2-header-id");
  const nameEl = el("div", "v2-header-name");
  const subEl = el("div", "v2-header-sub");
  id.append(nameEl, subEl);
  const cashEl = el("div", "v2-header-cash");
  const moreBtn = el("button", "v2-header-btn", "⋯");
  moreBtn.type = "button";
  moreBtn.setAttribute("aria-label", "More routes");
  moreBtn.setAttribute("aria-expanded", "false");
  header.append(backBtn, homeLink, id, cashEl, moreBtn);

  const moreMenu = el("div", "v2-more");
  moreMenu.hidden = true;
  for (const route of MORE_ROUTES) {
    const b = el("button", "", route.label);
    b.type = "button";
    b.addEventListener("click", () => {
      closeMore();
      const fn = opts["on" + route.id[0].toUpperCase() +
                         route.id.slice(1)];
      if (typeof fn === "function") fn();
    });
    moreMenu.appendChild(b);
  }
  header.appendChild(moreMenu);
  function closeMore() {
    moreMenu.hidden = true;
    moreBtn.setAttribute("aria-expanded", "false");
  }
  moreBtn.addEventListener("click", () => {
    moreMenu.hidden = !moreMenu.hidden;
    moreBtn.setAttribute("aria-expanded", String(!moreMenu.hidden));
    sync();
  });

  /* USER BUG FIX: one Back behavior with a strict priority — More menu →
   * person overlay → feature panel → previous screen (shell hook). Never
   * touches the decision sheet; a server decision can't be discarded here. */
  function backOut() {
    if (!moreMenu.hidden) { closeMore(); sync(); return; }
    if (!personHost.hidden) {
      const overlayBack = opts.onOverlayBack || hooks.onOverlayBack;
      if (typeof overlayBack === "function") overlayBack();
      else backFromPerson();
      return;
    }
    if (typeof opts.isPanelOpen === "function" && opts.isPanelOpen()) {
      if (typeof opts.closeTopPanel === "function") opts.closeTopPanel();
      return;
    }
    if (typeof hooks.onNavBack === "function") hooks.onNavBack();
  }

  /* Escape (only called when the decision sheet is NOT open — the shell
   * never discards a pending decision). Same priority as the Back button. */
  function escape() {
    if (!moreMenu.hidden) { closeMore(); sync(); return true; }
    if (!personHost.hidden) { backOut(); return true; }
    if (typeof opts.isPanelOpen === "function" && opts.isPanelOpen()) {
      if (typeof opts.closeTopPanel === "function") opts.closeTopPanel();
      return true;
    }
    return false;
  }

  /* popstate application: close every auxiliary surface WITHOUT touching
   * history (the browser already moved) and without toasts. */
  function closeAux() {
    closeMore();
    closePersonQuiet();
    if (typeof opts.closePanels === "function") opts.closePanels();
  }

  // Wrap the existing gameView children (screens + cards) in the scroll
  // region so the fixed tab bar can never cover the last entry.
  const scroll = el("div", "v2-scroll");
  while (host.firstChild) scroll.appendChild(host.firstChild);

  const tabbar = el("nav", "v2-tabbar");
  tabbar.setAttribute("aria-label", "Game sections");
  const tabButtons = {};
  for (const t of TABS) {
    const b = el("button", "v2-tab");
    b.type = "button";
    b.dataset.screen = t.id;
    b.append(el("span", "v2-tab-ic", t.ic),
             el("span", "", t.label));
    b.addEventListener("click", () => {
      closeMore();
      if (typeof opts.selectScreen === "function") {
        opts.selectScreen(t.id);
      }
    });
    tabbar.appendChild(b);
    tabButtons[t.id] = b;
  }

  // Person overlay host (inside scroll flow container but fixed overlay).
  const personHost = el("div", "v2-person-host");
  personHost.hidden = true;

  host.append(header, scroll, tabbar, personHost);

  // --- state ------------------------------------------------------------
  const stack = [];               // { screen, scrollTop }
  let personHandle = null;        // C's module handle
  let personId = null;
  let personLoading = false;

  function activeScreen() {
    return (typeof opts.getActiveScreen === "function")
      ? opts.getActiveScreen() : "home";
  }

  function showError(message) {
    personHost.hidden = false;
    personHost.textContent = "";
    const box = el("div", "v2-error");
    box.append(el("p", "", message));
    const back = el("button", "", "Back");
    back.type = "button";
    back.addEventListener("click", () => backOut());
    box.appendChild(back);
    personHost.appendChild(box);
  }

  async function loadPersonDetail(pid) {
    const gid = hooks.getState() && hooks.getState().gameId;
    if (!gid) throw Object.assign(new Error("no game"), { code: "no_game" });
    const res = await fetch("/api/games/" + encodeURIComponent(gid) +
                            "/people/" + encodeURIComponent(pid));
    const body = await res.json().catch(() => null);
    if (!res.ok) {
      const err = (body && body.error) || { code: "http_" + res.status };
      throw Object.assign(new Error(err.message || "person load failed"),
                          { code: err.code });
    }
    // A's projection passes advertised snake_case specs through; the frozen
    // adapter normalizer is the single client-side dialect, so known_costs
    // becomes knownCosts (etc.) before C's module ever sees them.
    if (body && Array.isArray(body.actions)) {
      body.actions = body.actions.map(normalizeAction);
    }
    return body;
  }

  async function openPerson(pid) {
    if (personLoading) return;
    personLoading = true;
    personId = pid;
    personHost.hidden = false;
    personHost.textContent = "";
    personHost.appendChild(el("div", "v2-loading", "Loading…"));
    // Freeze the return point before we leave the list.
    if (!stack.length) {
      stack.push({ screen: activeScreen(),
                   scrollTop: scroll.scrollTop });
    }
    try {
      const detail = await loadPersonDetail(pid);
      if (personId !== pid) return;         // superseded
      const mod = await import("./person.js");
      personHost.textContent = "";
      const root = el("div");
      personHost.appendChild(root);
      personHandle = mod.mountPerson(root, {
        ...hooks,
        // The module's Back goes through the SAME priority path as the
        // header Back (history-aware where available), so browser Back and
        // the in-module control never diverge.
        onBack: () => backOut(),
        onEdit: (id) => {
          closePersonQuiet();
          if (typeof opts.onEdit === "function") opts.onEdit(id);
        },
      }, detail);
    } catch (err) {
      if (personId !== pid) return;
      if (err && err.code === "person_not_found") {
        closePerson("Person not available.");
      } else {
        showError("Couldn't load this person. Your save is safe.");
      }
    } finally {
      personLoading = false;
    }
  }

  function backFromPerson() {
    const prev = stack.pop() || { screen: "people", scrollTop: 0 };
    closePerson();
    if (typeof opts.selectScreen === "function") {
      opts.selectScreen(prev.screen);
    }
    scroll.scrollTop = prev.scrollTop;
  }

  function closePerson(toastMsg) {
    if (personHandle) {
      try { personHandle.unmount(); } catch (e) { /* detach anyway */ }
      personHandle = null;
    }
    personId = null;
    personHost.hidden = true;
    personHost.textContent = "";
    if (toastMsg && typeof opts.onError === "function") opts.onError(toastMsg);
  }

  function closePersonQuiet() { closePerson(); }

  /* Re-project the open overlay after a shell refresh (retry/revision
   * machinery adopted a new revision). Missing/unavailable → honest close. */
  async function resyncPerson() {
    if (!personId || !personHandle) return;
    const pid = personId;
    try {
      const detail = await loadPersonDetail(pid);
      if (personId === pid) personHandle.update(detail);
    } catch (err) {
      if (personId === pid) {
        if (err && err.code === "person_not_found") {
          closePerson("Person not available.");
        } else {
          showError("Couldn't refresh this person.");
        }
      }
    }
  }

  function sync() {
    const state = hooks.getState();
    if (!state || !state.player) return;
    nameEl.textContent = state.player.name || "";
    subEl.textContent = headerSubtitle(state);
    cashEl.innerHTML = "";
    cashEl.append(el("small", "", "MONEY"),
                  document.createTextNode(
                    "$" + (state.money || 0).toLocaleString()));
    const cur = activeScreen();
    for (const [id, b] of Object.entries(tabButtons)) {
      if (id === cur) b.setAttribute("aria-current", "page");
      else b.removeAttribute("aria-current");
    }
    homeLink.setAttribute("aria-current", cur === "home" ? "page" : "false");
    // Back is visible on every non-Home view (and on Home while an
    // auxiliary surface is open); hidden on plain Home — nothing to undo.
    const panelOpen = typeof opts.isPanelOpen === "function" &&
                      opts.isPanelOpen();
    backBtn.hidden = !(!moreMenu.hidden || !personHost.hidden || panelOpen ||
                       cur !== "home");
    if (personHandle) resyncPerson();
  }

  return {
    sync,
    escape,
    closeAux,
    backFromPerson,          // exposed for history-less fallback hosts
    navigate(screen, params = {}) {
      if (screen === "person") {
        if (params.personId) return openPerson(params.personId);
        return;
      }
      closeMore();
      if (typeof opts.selectScreen === "function") opts.selectScreen(screen);
    },
    unmount() {
      closePerson();
      header.remove();
      tabbar.remove();
      personHost.remove();
      // Unwrap scroll region.
      while (scroll.firstChild) host.appendChild(scroll.firstChild);
      scroll.remove();
      host.classList.remove("v2-scope");
    },
  };
}
