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
import { formatMoney } from "../common.js";

export const V2_SCREENS = ["home", "job", "assets", "people", "activities"];

/* Bottom bar (feel-pass F3): four section tabs with ONE big round Age
 * button centred between them — Occupation · Assets · AGE · Relationships ·
 * Activities — always reachable one-handed, never floating mid-page. */
const TABS = [
  { id: "job",        ic: "▣", label: "Occupation" },
  { id: "assets",     ic: "⌂", label: "Assets" },
  { id: "people",     ic: "☺", label: "Relationships" },
  { id: "activities", ic: "✦", label: "Activities" },
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

/* Subtitle for the header: real status only — the NAMED career title
 * first when a M3 assignment is active, then actual job/status and
 * country, with student/unemployed/retired fallbacks (codex §1 Home). */
function headerSubtitle(state) {
  const parts = [];
  const c = state.careers && state.careers.assignment;
  const w = state.sections.work;
  const ed = state.sections.education;
  if (c) {
    parts.push((c.role_label || c.role_id) +
               (c.employer_name ? " · " + c.employer_name : ""));
  }
  else if (w && w.employed && w.job_label) parts.push(w.job_label);
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
  // M3a polish: the name row IS the Home control — the old standalone
  // "Life" label crowded the 320px header and truncated the name. Tapping
  // the name (or focusing it and pressing Enter/Space) goes Home.
  const id = el("div", "v2-header-id");
  const nameRow = el("div", "v2-header-namerow");
  nameRow.setAttribute("role", "button");
  nameRow.tabIndex = 0;
  nameRow.setAttribute("aria-label", "Home — life timeline");
  const goHome = () => {
    closeMore();
    if (typeof opts.selectScreen === "function") opts.selectScreen("home");
  };
  nameRow.addEventListener("click", goHome);
  nameRow.addEventListener("keydown", (e) => {
    if (e.key === "Enter" || e.key === " ") {
      e.preventDefault();
      goHome();
    }
  });
  const nameEl = el("div", "v2-header-name");
  const ageEl = el("span", "v2-header-age");
  ageEl.setAttribute("aria-label", "Current age");
  nameRow.append(nameEl, ageEl);
  const subEl = el("div", "v2-header-sub");
  id.append(nameRow, subEl);
  const cashEl = el("div", "v2-header-cash");
  const moreBtn = el("button", "v2-header-btn", "⋯");
  moreBtn.type = "button";
  moreBtn.setAttribute("aria-label", "More routes");
  moreBtn.setAttribute("aria-expanded", "false");
  header.append(backBtn, id, cashEl, moreBtn);

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
  for (const t of TABS.slice(0, 2)) tabbar.appendChild(makeTab(t));
  // The one big Age button, centred in the bar (feel-pass F3).
  const ageBtn = el("button", "v2-agebtn");
  ageBtn.type = "button";
  ageBtn.setAttribute("aria-label", "Age up");
  ageBtn.append(el("span", "v2-agebtn-plus", "+"),
                el("span", "v2-agebtn-word", "AGE"));
  ageBtn.addEventListener("click", () => ageUp());
  tabbar.appendChild(ageBtn);
  for (const t of TABS.slice(2)) tabbar.appendChild(makeTab(t));
  function makeTab(t) {
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
    tabButtons[t.id] = b;
    return b;
  }

  /* Stats dock (feel-pass F7): Happy/Health/Smarts/Looks pinned above the
   * bottom bar, always visible; bars animate to new values and flash
   * green/red on change (F8). Also hosts the one-line Age gate reason.
   *
   * F8 (codex-feel-evaluation §5): cells and fills PERSIST across syncs —
   * existing fills have their width updated after paint so the CSS
   * transition actually runs; rebuilding the row every sync made every bar
   * appear at its final width with no animation. */
  const dock = el("div", "v2-dock");
  const statsRow = el("div", "v2-statsbar");
  /* §Undo (contracts.md): "Undo last year" — spec-driven from the server's
   * advertised undo_year action (same enabled/reason source as can_undo).
   * Two-tap confirm; the command rides the shell's retry-stable machinery
   * and the normal refresh re-renders feed + stats. */
  const undoRow = el("div", "v2-undorow");
  const undoBtn = el("button", "v2-undo", "↩ Undo last year");
  undoBtn.type = "button";
  const undoReason = el("span", "v2-undo-reason");
  undoRow.append(undoBtn, undoReason);
  let undoConfirming = false;
  let undoConfirmation = null;
  let undoBusy = false;
  undoBtn.addEventListener("click", () => {
    const current = hooks.getState();
    renderUndo(current);
    if (undoBtn.disabled) return;
    if (!undoConfirming) {
      // Confirm step: the next tap on this button executes the undo.
      undoConfirming = true;
      undoConfirmation = { gameId: current.gameId, revision: current.revision };
      undoBtn.classList.add("confirm");
      undoBtn.textContent = "Tap again to confirm";
      undoReason.textContent = "Rewinds the last year — no re-roll.";
      return;
    }
    undoConfirming = false;
    undoBtn.classList.remove("confirm");
    undoBtn.textContent = "↩ Undo last year";
    undoReason.textContent = "";
    if (typeof hooks.sendAction !== "function") return;
    undoBusy = true;
    renderUndo(hooks.getState());
    Promise.resolve(hooks.sendAction("undo_year", {}, null))
      .catch(() => {})
      .finally(() => {
        undoBusy = false;
        const current = hooks.getState();
        if (current) { renderUndo(current); renderStats(current); }
      });
  });

  function renderUndo(state) {
    const spec = ((state && state.actions) || [])
      .find((a) => a.id === "undo_year");
    // A confirmation belongs to exactly one authoritative revision.
    // A new year, another action, or a different life needs a fresh tap.
    if (undoConfirming && (!spec || spec.enabled === false ||
        undoConfirmation.gameId !== state.gameId ||
        undoConfirmation.revision !== state.revision)) {
      undoConfirming = false;
      undoConfirmation = null;
    }
    undoRow.hidden = !spec;
    if (!spec) return;
    undoBtn.classList.toggle("confirm", undoConfirming);
    undoBtn.textContent = undoBusy ? "Undoing…" : undoConfirming
      ? "Tap again to confirm" : "↩ Undo last year";
    const blocked = undoBusy || spec.enabled === false;
    undoBtn.disabled = blocked;
    undoBtn.setAttribute("aria-disabled", String(blocked));
    undoReason.textContent = undoBusy ? "" : spec.enabled === false
      ? spec.reason || "" : undoConfirming
      ? "Rewinds the last year — no re-roll." : "";
  }
  const ageNote = el("div", "v2-agenote");
  dock.append(statsRow, undoRow, ageNote);
  const statCells = {};        // key → { cell, fill, val, label }
  let statPainted = false;     // first paint snaps, later changes animate

  /* Short display labels (F7): the 4-up dock truncates "Happiness" at
   * 320px ("Happin…") while the feel-pass names are Happy/Health/Smarts/
   * Looks. The projection key stays `happiness`; only the label shortens. */
  const STAT_LABELS = { happiness: "Happy" };

  function cap(s) { return s ? s[0].toUpperCase() + s.slice(1) : s; }
  function statLabel(k) { return STAT_LABELS[k] || cap(k); }

  function renderStats(state) {
    const entries = Object.entries((state && state.stats) || {});
    const seen = new Set();
    for (const [k, v] of entries) {
      seen.add(k);
      const pct = Math.max(0, Math.min(100, typeof v === "number" ? v : 0));
      const color = pct >= 60 ? "var(--v2-bar-good)"
                  : pct >= 35 ? "var(--v2-bar-mid)" : "var(--v2-bar-bad)";
      let slot = statCells[k];
      if (!slot) {
        // New stat: build at its final width (no animation on first paint
        // of this cell).
        const cell = el("div", "v2-stat");
        const top = el("div", "v2-stat-top");
        const val = el("span", "v2-stat-value", pct + "%");
        top.append(el("span", "v2-stat-label", statLabel(k)), val);
        const track = el("div", "v2-stat-track");
        const fill = el("div", "v2-stat-fill");
        track.appendChild(fill);
        cell.append(top, track);
        statsRow.appendChild(cell);
        slot = statCells[k] = { cell, fill, val, had: false};
        fill.style.width = pct + "%";
        fill.style.background = color;
        val.style.color = color;
      }
      if (typeof v === "number") {
        slot.val.textContent = pct + "%";
        slot.val.style.color = color;
        slot.fill.style.background = color;
        if (slot.had && statPainted) {
          // Persisted element: changing the width AFTER it is in the DOM
          // lets the .animate transition run from the old value.
          slot.fill.classList.add("animate");
          slot.fill.style.width = pct + "%";
          if (slot.last !== v) {
            // Brief green/red flash on change (numeric label is primary).
            slot.cell.classList.remove("up", "down");
            if (slot.cell.offsetWidth !== undefined) {
              // restart the CSS animation even on repeat changes
              void slot.cell.offsetWidth;
            }
            slot.cell.classList.add(v > slot.last ? "up" : "down");
          }
        }
        slot.last = v;
        slot.had = true;
      }
    }
    // Stats removed from the projection disappear; the rest stay put.
    for (const k of Object.keys(statCells)) {
      if (!seen.has(k)) {
        statCells[k].cell.remove();
        delete statCells[k];
      }
    }
    statPainted = true;
    dock.hidden = entries.length === 0;
  }

  /* Age gate + busy state, driven by the server's advertised age_up spec
   * (never a local increment). Reason shows as a one-line note above the
   * bar so the big button never silently dead-ends. */
  let ageBusy = false;
  function renderAge(state) {
    const spec = ((state && state.actions) || [])
      .find((a) => a.id === "age_up");
    const blocked = ageBusy || !spec || spec.enabled === false;
    ageBtn.disabled = blocked;
    ageBtn.setAttribute("aria-disabled", String(blocked));
    ageBtn.classList.toggle("busy", ageBusy);
    ageNote.textContent = ageBusy ? "Ageing…"
      : (spec && spec.enabled === false && spec.reason) ? spec.reason : "";
    ageNote.hidden = !ageNote.textContent;
  }

  function ageUp() {
    if (ageBtn.disabled || typeof hooks.sendAction !== "function") return;
    ageBusy = true;
    renderAge(hooks.getState());
    Promise.resolve(hooks.sendAction("age_up", {}, null))
      .catch(() => {})
      .finally(() => {
        ageBusy = false;
        const current = hooks.getState();
        if (current) { renderAge(current); renderStats(current); }
      });
  }

  // Person overlay host (inside scroll flow container but fixed overlay).
  const personHost = el("div", "v2-person-host");
  personHost.hidden = true;

  host.append(header, scroll, dock, tabbar, personHost);
  // The Undo row and reasons change the fixed footer's real height.
  // Reserve that height instead of the former stats-only constant.
  function reserveDockSpace() {
    if (typeof dock.getBoundingClientRect !== "function" ||
        typeof host.style.setProperty !== "function") return;
    const height = Math.ceil(dock.getBoundingClientRect().height);
    if (!height) return;
    const follow = activeScreen() === "home" &&
      scroll.scrollHeight - scroll.scrollTop - scroll.clientHeight < 48;
    host.style.setProperty("--v2-dock-h", height + "px");
    if (follow) scroll.scrollTop = scroll.scrollHeight;
  }
  const dockObserver = typeof ResizeObserver === "function"
    ? new ResizeObserver(reserveDockSpace) : null;
  if (dockObserver) dockObserver.observe(dock);


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

  /* F8 age tick: the header age counts up (or down) from the previously
   * rendered age over ~450ms instead of snapping; reduced motion (or no
   * rAF in test hosts) sets it instantly. */
  let ageTickTimer = null;
  let ageShown = null;
  function renderAgeTick(age) {
    if (typeof age !== "number" || age === ageShown) return;
    const from = typeof ageShown === "number" ? ageShown : age;
    ageShown = age;
    if (ageTickTimer && typeof clearInterval === "function") {
      clearInterval(ageTickTimer);
      ageTickTimer = null;
    }
    const reduced = typeof window !== "undefined" &&
      typeof window.matchMedia === "function" &&
      window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    const raf = typeof requestAnimationFrame === "function";
    if (reduced || !raf || from === age) {
      ageEl.textContent = "Age " + age;
      return;
    }
    // F8 requires a fast, punchy tick: the whole transition lands inside
    // the 120–200 ms window regardless of the jump size.
    const steps = Math.abs(age - from);
    const per = Math.max(30, Math.floor(180 / steps));
    let cur = from;
    ageEl.textContent = "Age " + cur;
    ageTickTimer = setInterval(() => {
      cur += age > from ? 1 : -1;
      ageEl.textContent = "Age " + cur;
      if (cur === age && ageTickTimer) {
        clearInterval(ageTickTimer);
        ageTickTimer = null;
      }
    }, per);
  }

  function sync() {
    const state = hooks.getState();
    if (!state || !state.player) return;
    nameEl.textContent = state.player.name || "";
    renderAgeTick(state.player.age);
    subEl.textContent = headerSubtitle(state);
    cashEl.innerHTML = "";
    cashEl.append(el("small", "", "MONEY"),
                  document.createTextNode(formatMoney(state.money || 0)));
    renderStats(state);
    renderAge(state);
    renderUndo(state);
    const cur = activeScreen();
    for (const [id, b] of Object.entries(tabButtons)) {
      if (id === cur) b.setAttribute("aria-current", "page");
      else b.removeAttribute("aria-current");
    }
    nameRow.setAttribute("aria-current",
                         cur === "home" ? "page" : "false");
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
      if (dockObserver) dockObserver.disconnect();
      host.style.removeProperty?.("--v2-dock-h");
      if (ageTickTimer) { clearInterval(ageTickTimer); ageTickTimer = null; }
      header.remove();
      dock.remove();
      tabbar.remove();
      personHost.remove();
      // Unwrap scroll region.
      while (scroll.firstChild) host.appendChild(scroll.firstChild);
      scroll.remove();
      host.classList.remove("v2-scope");
    },
  };
}
