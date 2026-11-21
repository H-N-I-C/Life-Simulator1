/* M2.5 relationship_policy — in-play character editor UI (Kimi B).
 *
 * Speaks the frozen editor-protocol shapes:
 *   POST /api/games/<id>/editor/preview  {commands: [...]}
 *   POST /api/games/<id>/editor/apply    {commands: [...], request_id,
 *                                         expected_revision}
 * The server (A's registration of B's domain) owns validation
 * authority; this UI renders preview results and never second-guesses
 * them. Numeric scores are never shown (frozen decision 4). The editor
 * is a normal in-play screen, distinct from dev scenario starts.
 */
import { showCrashCard, setErrorRetryHandler } from "../../common.js";

const $ = (id) => document.getElementById(id);
const esc = (s) => String(s).replace(/[&<>"']/g, (c) => (
  { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));

let gameState = null;   // normalized adapter state (for the people list)
let policyState = null; // /policy-state payload
let policyLoadVersion = 0;
let selectedPerson = null;
let shellHooks = null;  // B1: {getState, refresh} published by the shell

export function initEditor(state, hooks) {
  gameState = state;
  shellHooks = hooks || shellHooks;
  let panel = $("editorPanel");
  if (!panel) {
    panel = document.createElement("section");
    panel.id = "editorPanel";
    panel.className = "card";
    panel.hidden = true;
    const anchor = $("actionList");
    anchor.parentElement.appendChild(panel);
  }
  renderShell(panel);
  return panel;
}

export function toggleEditor(state, hooks) {
  gameState = state || gameState;
  shellHooks = hooks || shellHooks;
  const panel = initEditor(gameState, shellHooks);
  panel.hidden = !panel.hidden;
  if (!panel.hidden) loadPolicyState();
}

/* Frozen §Shell secondary-route contract (redesign-v2): the shell can open
 * the editor idempotently from the More menu — never toggle it closed. */
export function isEditorOpen() {
  const panel = $("editorPanel");
  return !!panel && !panel.hidden;
}

/* §Editor navigation (A contract, FROZEN v1): openEditor(state, hooks,
 * personId?) — the third parameter is optional and additive. When given,
 * the editor opens with THAT exact person_id selected (roster row
 * aria-pressed="true", form loaded, law pseudo-entry not selected).
 * Unknown/deceased IDs open with NO selection and an honest status line —
 * never a fabricated selection, never a blocked editor. */
export async function openEditor(state, hooks, personId) {
  gameState = state || gameState;
  shellHooks = hooks || shellHooks;
  const panel = initEditor(gameState, shellHooks);
  panel.hidden = false;
  await loadPolicyState();
  if (!policyState) return panel;
  if (personId === undefined || personId === null) return panel;
  const people = (gameState.people || []).filter((p) => p.alive !== false);
  const person = people.find((p) => p.id === personId);
  if (!person) {
    selectedPerson = null;
    renderPeople();
    setStatus("That person is not available for editing — opened " +
              "without a selection.", true);
    return panel;
  }
  selectedPerson = person.id;
  renderPeople();
  renderForm(person);
  const box = $("editorPeople");
  if (box && box.querySelectorAll) {
    for (const b of box.querySelectorAll("button")) {
      if (b.getAttribute("aria-pressed") === "true" &&
          typeof b.scrollIntoView === "function") {
        b.scrollIntoView({ block: "nearest" });
      }
    }
  }
  return panel;
}

function renderShell(panel) {
  panel.innerHTML = `
    <h2>Character editor <span class="muted">(out-of-character; changes
      apply immediately and are audited)</span></h2>
    <div id="editorStatus" class="muted" role="status"></div>
    <div class="row" id="editorPeople"></div>
    <div id="editorForm"></div>
    <div id="editorPreview"></div>`;
  renderPeople();
}

function setStatus(msg, isError) {
  const el = $("editorStatus");
  el.textContent = msg;
  el.style.color = isError ? "#a53b3b" : "";
}

async function api(path, body) {
  const res = await fetch(path, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });
  const data = await res.json().catch(() => null);
  if (!res.ok) {
    const err = new Error((data && data.error && data.error.message) ||
                          "Request failed (" + res.status + ")");
    err.code = data && data.error && data.error.code;
    throw err;
  }
  return data;
}

async function loadPolicyState() {
  const version = ++policyLoadVersion;
  const gameId = gameState.gameId;
  policyState = null;
  setStatus("Loading…");
  renderPeople();
  try {
    const gid = encodeURIComponent(gameId);
    const loaded = await fetch("/api/games/" + gid + "/policy-state")
      .then(async (res) => {
        if (!res.ok) throw new Error("policy-state failed");
        return res.json();
      });
    if (version !== policyLoadVersion || gameState.gameId !== gameId) return;
    policyState = loaded;
    setStatus("Policy revision " + policyState.policy_revision + ".");
    renderPeople();
  } catch (err) {
    if (version === policyLoadVersion) {
      setStatus("Editor unavailable: " + err.message, true);
    }
  }
}

function renderPeople() {
  const box = $("editorPeople");
  if (!box) return;
  box.innerHTML = "";
  // Controls depend on the policy catalogs. Do not expose clickable rows
  // while those catalogs are still loading (or a request has failed).
  if (!policyState) {
    box.innerHTML = '<p class="muted">Waiting for editor data…</p>';
    return;
  }
  const people = (gameState.people || []).filter((p) => p.alive !== false);
  if (!people.length) {
    box.innerHTML = '<p class="muted">No one to edit.</p>';
    return;
  }
  for (const p of people) {
    const b = document.createElement("button");
    b.className = "small";
    b.textContent = p.name + (p.id === gameState.playerId ? " (you)" : "");
    b.setAttribute("aria-pressed", String(p.id === selectedPerson));
    b.addEventListener("click", () => {
      selectedPerson = p.id;
      renderPeople();
      renderForm(p);
    });
    box.appendChild(b);
  }
  const law = document.createElement("button");
  law.className = "small";
  law.textContent = "Local laws";
  law.setAttribute("aria-pressed", String(selectedPerson === "__law__"));
  law.addEventListener("click", () => {
    selectedPerson = "__law__";
    renderPeople();
    renderLawForm();
  });
  box.appendChild(law);
}

function renderForm(person) {
  const form = $("editorForm");
  const profile = (policyState && policyState.profiles[person.id]) || {};
  const disabledNote = '<p class="muted">Age, identity, consent, ' +
    'parentage, money and history are protected and cannot be edited.</p>';
  form.innerHTML = `
    <h3>${esc(person.name)}</h3>
    <div class="row">
      <label class="field" style="flex:1;min-width:140px;">Openness
        <input type="number" id="edOpenness" min="0" max="1" step="0.05"
               value="${profile.openness ?? 0.5}"></label>
      <label class="field" style="flex:1;min-width:140px;">Jealousy
        <input type="number" id="edJealousy" min="0" max="1" step="0.05"
               value="${profile.jealousy ?? 0.4}"></label>
      <label class="field" style="flex:1;min-width:140px;">Monogamy preference
        <input type="number" id="edMonogamy" min="0" max="1" step="0.05"
               value="${profile.monogamy_preference ?? 0.5}"></label>
    </div>
    <div class="row">
      <label class="field" style="flex:1;min-width:140px;">Faith
        <select id="edFaith"></select></label>
      <label class="field" style="flex:1;min-width:140px;">First name
        <input id="edFirstName" maxlength="40"
               value="${esc(person.name.split(" ")[0] || "")}"></label>
    </div>
    <p class="muted">Personal tenets (this person only):</p>
    <div class="row" id="edTenets"></div>
    ${disabledNote}
    <div class="row" style="margin-top:10px;">
      <button class="primary" id="edPreview">Preview changes</button>
    </div>`;
  const faithSel = $("edFaith");
  for (const f of (policyState.catalogs && policyState.catalogs.faiths) ||
                   []) {
    const o = document.createElement("option");
    o.value = f.id;
    o.textContent = f.label;
    if (f.id === profile.faith_id) o.selected = true;
    faithSel.appendChild(o);
  }
  const tenetsBox = $("edTenets");
  const faithId = profile.faith_id || "faith_unaffiliated";
  const faith = ((policyState.catalogs || {}).faiths || [])
    .find((f) => f.id === faithId);
  const personal = profile.personal_tenet_overrides || {};
  for (const tenet of (faith && faith.tenets) || []) {
    const wrap = document.createElement("label");
    wrap.className = "field";
    wrap.style.minWidth = "150px";
    const sel = document.createElement("select");
    sel.dataset.tenet = tenet.id;
    const current = personal[tenet.id] || tenet.stance;
    for (const stance of ["favoured", "neutral", "disfavoured"]) {
      const o = document.createElement("option");
      o.value = stance;
      o.textContent = stance;
      if (stance === current) o.selected = true;
      sel.appendChild(o);
    }
    wrap.appendChild(document.createTextNode(tenet.label || tenet.id));
    wrap.appendChild(sel);
    tenetsBox.appendChild(wrap);
  }
  $("edPreview").addEventListener("click", () => previewOrApply(person));
}

function collectCommands(person) {
  const commands = [];
  const num = (id) => {
    const el = $(id);
    return el && el.value !== "" ? Number(el.value) : null;
  };
  const profile = (policyState.profiles || {})[person.id] || {};
  const patch = {};
  for (const [id, key] of [["edOpenness", "openness"],
                           ["edJealousy", "jealousy"],
                           ["edMonogamy", "monogamy_preference"]]) {
    const value = num(id);
    if (value !== null && value !== profile[key]) patch[key] = value;
  }
  if (Object.keys(patch).length) {
    commands.push({ name: "edit_person_attributes", scope: "person",
                    target_id: person.id, patch });
  }
  const faithSel = $("edFaith");
  if (faithSel && faithSel.value !== profile.faith_id) {
    commands.push({ name: "convert_profile", scope: "person",
                    target_id: person.id,
                    patch: { target_faith_id: faithSel.value } });
  }
  const first = $("edFirstName");
  const currentFirst = (person.name || "").split(" ")[0];
  if (first && first.value.trim() && first.value.trim() !== currentFirst) {
    commands.push({ name: "rename_person", scope: "person",
                    target_id: person.id,
                    patch: { first_name: first.value.trim() } });
  }
  const tenetOverrides = {};
  document.querySelectorAll("#edTenets select").forEach((sel) => {
    tenetOverrides[sel.dataset.tenet] = sel.value;
  });
  const personal = profile.personal_tenet_overrides || {};
  const faith = ((policyState.catalogs.faiths || []).find(
    (f) => f.id === (profile.faith_id || "faith_unaffiliated")) || {});
  const publicStance = (tenetId) => {
    const tenet = ((faith.tenets) || []).find((x) => x.id === tenetId);
    return (tenet && tenet.stance) || "neutral";
  };
  const changed = Object.keys(tenetOverrides).filter(
    (t) => tenetOverrides[t] !== (personal[t] || publicStance(t)));
  if (changed.length) {
    commands.push({ name: "edit_personal_tenets",
                    scope: "personal_tenets", target_id: person.id,
                    patch: { tenet_overrides:
                             Object.fromEntries(changed.map(
                               (t) => [t, tenetOverrides[t]])) } });
  }
  return commands;
}

function renderLawForm() {
  const form = $("editorForm");
  const jur = ((policyState.catalogs || {}).jurisdictions || []).find(
    (j) => j.id === policyState.jurisdiction_id) ||
    { id: policyState.jurisdiction_id, recognition: {} };
  form.innerHTML = `
    <h3>Local laws <span class="muted">(${esc(jur.label ||
      jur.id)}, save-local override)</span></h3>
    <p class="muted">Changing recognition reprojects current unions only —
      it never rewrites history or repeats old penalties.</p>
    <div class="row" id="edLaw"></div>
    <div class="row" style="margin-top:10px;">
      <button class="primary" id="edPreview">Preview changes</button>
    </div>`;
  const box = $("edLaw");
  const overrideRec = ((policyState.overrides || {}).jurisdictions || {})
    [jur.id] || { recognition: {} };
  const labels = { monogamous_civil: "Monogamous civil unions",
                   plural_civil: "Plural civil unions",
                   religious_only: "Religious-only unions",
                   customary: "Customary unions",
                   private: "Private unions" };
  for (const [unionType, label] of Object.entries(labels)) {
    const wrap = document.createElement("label");
    wrap.className = "field";
    wrap.style.minWidth = "170px";
    const sel = document.createElement("select");
    sel.dataset.unionType = unionType;
    const current = overrideRec.recognition[unionType] ||
      jur.recognition[unionType] || "recognized";
    for (const status of ["recognized", "civil_only", "unrecognized",
                          "forbidden"]) {
      const o = document.createElement("option");
      o.value = status;
      o.textContent = status;
      if (status === current) o.selected = true;
      sel.appendChild(o);
    }
    wrap.appendChild(document.createTextNode(label));
    wrap.appendChild(sel);
    box.appendChild(wrap);
  }
  $("edPreview").addEventListener("click", () => previewOrApply(null));
}

function collectLawCommands() {
  const jurId = policyState.jurisdiction_id;
  const jur = ((policyState.catalogs || {}).jurisdictions || []).find(
    (j) => j.id === jurId) || { recognition: {} };
  const overrideRec = ((policyState.overrides || {}).jurisdictions || {})
    [jurId] || { recognition: {} };
  const recognition = {};
  document.querySelectorAll("#edLaw select").forEach((sel) => {
    const base = overrideRec.recognition[sel.dataset.unionType] ||
      jur.recognition[sel.dataset.unionType] || "recognized";
    if (sel.value !== base) recognition[sel.dataset.unionType] = sel.value;
  });
  if (!Object.keys(recognition).length) return [];
  return [{ name: "edit_jurisdiction", scope: "jurisdiction",
            target_id: jurId, patch: { recognition } }];
}

async function previewOrApply(person) {
  const previewBox = $("editorPreview");
  previewBox.innerHTML = "<p class=\"muted\">Checking…</p>";
  const commands = person ? collectCommands(person) : collectLawCommands();
  if (!commands.length) {
    previewBox.innerHTML = '<p class="muted">No changes to preview.</p>';
    return;
  }
  const gid = encodeURIComponent(gameState.gameId);
  let preview;
  try {
    preview = await api("/api/games/" + gid + "/editor/preview",
                        { commands });
  } catch (err) {
    previewBox.innerHTML = '<p class="load-error"><b>Preview failed:</b> ' +
      esc(err.message) + "</p>";
    return;
  }
  if (!preview.valid) {
    previewBox.innerHTML = "<p><b>These changes are not allowed:</b></p><ul>" +
      preview.errors.map((e) => "<li>" + esc(e.code) +
        (e.field ? " (" + esc(e.field) + ")" : "") + "</li>").join("") +
      "</ul>";
    return;
  }
  const rows = [];
  if (preview.affected_people.length) {
    rows.push("Affected people: " + preview.affected_people.length);
  }
  for (const u of preview.affected_unions) {
    rows.push("Union " + esc(u.union_id) + ": " +
              esc(u.current_recognition) + " → " +
              esc(u.projected_recognition));
  }
  if (preview.policy_revision_delta) {
    rows.push("Policy revision +" + preview.policy_revision_delta);
  }
  const applyBtn = document.createElement("button");
  applyBtn.className = "primary";
  applyBtn.textContent = "Apply now";
  const cancelBtn = document.createElement("button");
  cancelBtn.textContent = "Cancel";
  applyBtn.addEventListener("click", async () => {
    // Duplicate-submit guard: freeze both buttons for this attempt.
    applyBtn.disabled = true;
    cancelBtn.disabled = true;
    try {
      // B1: read the CURRENT revision from the shell's live state at
      // send time, not from the state snapshot captured when the panel
      // opened — that staleness is what produced the second-edit 409.
      const fresh = shellHooks && shellHooks.getState &&
                    shellHooks.getState();
      const revision = (fresh && fresh.revision != null)
        ? fresh.revision : gameState.revision;
      const out = await api("/api/games/" + gid + "/editor/apply", {
        commands,
        request_id: crypto.randomUUID(),
        expected_revision: revision,
      });
      gameState.revision = out.revision;
      setStatus("Applied — policy revision " + out.policy_revision + ".");
      const items = out.audit.map((a) => esc(a.command) + " on " +
        esc(String(a.target_id)));
      // A's service-side reprojection (A4) makes the preview's projected
      // recognition the unions' actual status; show it as the result.
      for (const u of preview.affected_unions || []) {
        items.push("Union " + esc(u.union_id) + " is now " +
                   esc(u.projected_recognition) +
                   (u.current_recognition !== u.projected_recognition
                     ? " (was " + esc(u.current_recognition) + ")" : ""));
      }
      previewBox.innerHTML = "<p class=\"muted\">Applied:</p><ul>" +
        items.map((t) => "<li>" + t + "</li>").join("") + "</ul>";
      // Adopt the shell's authoritative state (new revision, renamed
      // people, reprojected recognition) without a page reload; then
      // re-render this panel's people list from the fresh state.
      if (shellHooks && shellHooks.refresh) {
        const s = await shellHooks.refresh(out);
        if (s) {
          gameState = s;
          renderPeople();
        }
      }
      await loadPolicyState();
    } catch (err) {
      applyBtn.disabled = false;
      cancelBtn.disabled = false;
      setStatus("Apply failed: " + err.message, true);
      if (err.code === "stale_revision" && shellHooks &&
          shellHooks.refresh) {
        // State moved elsewhere; adopt it so the next attempt sends the
        // fresh revision.
        const s = await shellHooks.refresh().catch(() => null);
        if (s) {
          gameState = s;
          renderPeople();
        }
      }
    }
  });
  cancelBtn.addEventListener("click", () => {
    previewBox.innerHTML = "";
  });
  previewBox.innerHTML = rows.map((r) => "<p>" + r + "</p>").join("");
  previewBox.appendChild(applyBtn);
  previewBox.appendChild(cancelBtn);
  applyBtn.focus();
}
