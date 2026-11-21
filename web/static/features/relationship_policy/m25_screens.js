/* M2.5 dating & weddings screens (Kimi B).
 *
 * One toggleable card with three tabs:
 *   Dating     — find a match (the dating_app action), see the match
 *                portrait, and answer the pending offer with the
 *                reason words the policy evaluator produced.
 *   Weddings   — per-union ceremony planner (venue/outfit/honeymoon
 *                tiers, free configuration highlighted, unaffordable
 *                combinations disabled with a reason) plus vow renewal.
 *   Reputation — what your character knows: discovered secrets/facts
 *                about people and reputation as known to you.
 *
 * Commands go through the shell's retry-stable machinery (shellHooks);
 * the wedding route is POST /api/games/<id>/wedding with A's typed
 * envelope. Numeric scores are never shown (frozen decision 4); reason
 * words are bounded phrases.
 */
import { renderAvatar, miniPersonChip, formatMoney } from "../../common.js";

const $ = (id) => document.getElementById(id);
const esc = (s) => String(s).replace(/[&<>"']/g, (c) => (
  { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));

/* Bounded player-facing phrases for the evaluator's reason codes (words
 * only — contributions stay server-side, frozen decision 4). */
const REASON_WORDS = {
  "reason.attraction": "We seemed to click",
  "reason.chemistry": "There was chemistry between us",
  "reason.trust": "We already trust each other",
  "reason.personal_tenets": "Their personal beliefs matter here",
  "reason.faith_tenets": "Their faith's teaching matters here",
  "reason.culture": "Local custom has an opinion",
  "reason.law_risk": "The law frowns on this",
  "reason.reputation": "What they know about me matters",
  "reason.adult_age_gap": "The age gap gives pause",
  "reason.existing_commitments": "They are already committed",
  "reason.kinship": "We are family",
  "reason.convert_request": "Their stance on conversion",
};

const RECOGNITION_LABELS = {
  recognized: "recognized",
  civil_only: "civil union only",
  unrecognized: "not recognized",
  forbidden: "forbidden here",
};

const FACT_LABELS = {
  affair: "an affair",
  secret_union: "a secret union",
  discovery: "a discovery",
  disclosure: "something disclosed",
  forgiveness: "a forgiveness",
  breakup: "a breakup",
  conversion: "a change of faith",
};

let hooks = null;
let view = null;      // last /relationships-view payload
let activeTab = "dating";

export async function mountM25(card, shellHooks) {
  hooks = shellHooks;
  card.innerHTML = `
    <h2>Dating &amp; weddings</h2>
    <div class="row" role="tablist" aria-label="Dating and weddings">
      <button class="small" data-tab="dating" role="tab">Dating</button>
      <button class="small" data-tab="weddings" role="tab">Weddings</button>
      <button class="small" data-tab="reputation" role="tab">What I know</button>
    </div>
    <div id="m25Status" class="muted" role="status"></div>
    <div id="m25Body"></div>`;
  for (const b of card.querySelectorAll("[data-tab]")) {
    b.addEventListener("click", () => {
      activeTab = b.dataset.tab;
      render();
    });
  }
  await reload();
}

function setStatus(msg, isError) {
  const el = $("m25Status");
  if (!el) return;
  el.textContent = msg;
  el.style.color = isError ? "#a53b3b" : "";
}

async function reload() {
  const s = hooks.getState && hooks.getState();
  if (!s) return;
  try {
    const res = await fetch("/api/games/" +
                            encodeURIComponent(s.gameId) +
                            "/relationships-view");
    const data = await res.json().catch(() => null);
    if (!res.ok) {
      throw new Error((data && data.error && data.error.message) ||
                      "Request failed (" + res.status + ")");
    }
    view = data;
    setStatus("");
    render();
  } catch (err) {
    setStatus("Unavailable: " + err.message, true);
  }
}

function render() {
  const card = $("m25Body");
  if (!card) return;
  const root = card.parentElement;
  for (const b of root.querySelectorAll("[data-tab]")) {
    b.setAttribute("aria-selected", String(b.dataset.tab === activeTab));
  }
  card.innerHTML = "";
  if (activeTab === "dating") renderDating(card);
  else if (activeTab === "weddings") renderWeddings(card);
  else renderReputation(card);
}

/* ------------------------------- dating */

function renderDating(card) {
  const s = hooks.getState();
  const pending = s && s.pending;
  const isDatingPending = pending && /dating/i.test(
    String(pending.id) + " " + String(pending.title));

  const h = document.createElement("div");
  h.innerHTML = `<p class="muted">Swipe locally, meet a stranger, see if
    they answer. Both people must be naturally 18 or older. For adults,
    laws and customs affect consequences, and anyone can say no.</p>`;
  card.appendChild(h);

  const find = document.createElement("button");
  find.className = "primary";
  find.textContent = "Find a match";
  const datingAction = (s.actions || []).find((a) => a.id === "dating_app");
  find.disabled = !!pending || !datingAction || datingAction.enabled !== true;
  const unavailableReason = !datingAction ? "Dating is unavailable right now. Both people must be 18 or older."
    : (!datingAction.enabled ? datingAction.reason : null);
  if (unavailableReason) {
    find.title = unavailableReason;
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = unavailableReason;
    card.appendChild(note);
  }
  find.addEventListener("click", async () => {
    find.disabled = true;
    setStatus("Looking for someone…");
    hooks.sendAction("dating_app", {}).catch(() => {});
    // The shell resolves the command and re-renders; give it a beat,
    // then adopt the fresh state and show the match.
    setTimeout(async () => {
      await (hooks.refresh && hooks.refresh());
      setStatus("");
      render();
    }, 700);
  });
  card.appendChild(find);

  // R5: authorized offer journey. Buttons exist ONLY for explicitly
  // advertised, enabled actions (spec/dispatch parity); the responses
  // arrive as pending decisions driven by the stored offer, never via
  // an unrestricted accept endpoint.
  const actionRow = (actionId, verb) => {
    const rows = (s.actions || []).filter((a) => a.id === actionId);
    if (!rows.length) return null;
    const box = document.createElement("div");
    for (const a of rows) {
      const row = document.createElement("div");
      row.className = "row";
      const target = a.targetId && s.peopleById[a.targetId];
      if (target) row.appendChild(miniPersonChip(target, 40));
      const b = document.createElement("button");
      b.textContent = verb + (a.targetLabel ? " — " + a.targetLabel : "");
      b.disabled = !!pending || !a.enabled;
      if (!a.enabled && a.reason) {
        b.title = a.reason;
        const r = document.createElement("span");
        r.className = "cost";
        r.textContent = a.reason;
        row.appendChild(r);
      }
      b.addEventListener("click", () => {
        for (const x of box.querySelectorAll("button")) x.disabled = true;
        hooks.sendAction(actionId, {}, a.targetId).catch(() => {});
        setTimeout(async () => {
          await (hooks.refresh && hooks.refresh());
          await reload();
        }, 700);
      });
      row.appendChild(b);
      box.appendChild(row);
    }
    return box;
  };
  const courtship = actionRow("courtship_offer", "Ask out");
  if (courtship) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = "Ask someone out:";
    card.appendChild(note);
    card.appendChild(courtship);
  }
  const proposals = actionRow("union_proposal", "Propose a union");
  if (proposals) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = "Ready to commit:";
    card.appendChild(note);
    card.appendChild(proposals);
  }

  // Current courtship edges and pending offers (graph state, read-only).
  const rels = (view && view.relationships) || [];
  if (rels.length) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = "Courtships:";
    card.appendChild(note);
    for (const rel of rels) {
      const row = document.createElement("div");
      row.className = "row";
      row.appendChild(miniPersonChip(
        { name: rel.other.name, age: rel.other.age, alive: rel.other.alive,
          appearance: rel.other.appearance }, 40));
      const txt = document.createElement("span");
      const agreement = rel.exclusivity && typeof rel.exclusivity === "object"
        ? rel.exclusivity.agreement : rel.exclusivity;
      txt.textContent = rel.other.name +
        (typeof rel.chemistry === "number"
          ? " — chemistry " + rel.chemistry : "") +
        (agreement && agreement !== "exclusive"
          ? " (" + agreement + ")" : "");
      row.appendChild(txt);
      card.appendChild(row);
    }
  }
  const offers = (view && view.offers) || [];
  if (offers.length) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = "Offers in flight:";
    card.appendChild(note);
    for (const o of offers) {
      const row = document.createElement("div");
      row.className = "row";
      row.appendChild(miniPersonChip(
        { name: o.other.name, age: o.other.age, alive: o.other.alive,
          appearance: o.other.appearance }, 40));
      const txt = document.createElement("span");
      txt.textContent = o.direction === "incoming"
        ? o.other.name + " asked you — answer in the decision dialog."
        : "Waiting for " + o.other.name + "'s answer.";
      row.appendChild(txt);
      card.appendChild(row);
    }
  }

  if (isDatingPending) {
    const box = document.createElement("div");
    box.className = "card";
    box.innerHTML = `<h3>${esc(pending.title)}</h3>
      <p>${esc(pending.text)}</p>`;
    const peopleRow = document.createElement("div");
    peopleRow.className = "row";
    for (const pid of pending.personIds || []) {
      const p = s.peopleById[pid];
      if (p) peopleRow.appendChild(miniPersonChip(p, 56));
    }
    box.appendChild(peopleRow);

    const words = (view && view.pending_reasons) || [];
    if (words.length) {
      const ul = document.createElement("ul");
      for (const w of words) {
        const li = document.createElement("li");
        li.textContent = REASON_WORDS[w.label_key] || REASON_WORDS[w.code] ||
                         "Something unspoken";
        ul.appendChild(li);
      }
      const note = document.createElement("p");
      note.className = "muted";
      note.textContent = "What might shape their answer:";
      box.appendChild(note);
      box.appendChild(ul);
    }

    const optRow = document.createElement("div");
    optRow.className = "row";
    for (const opt of pending.options || []) {
      const b = document.createElement("button");
      b.className = opt.id.includes("accept") ? "primary" : "";
      b.textContent = opt.label;
      b.disabled = !opt.enabled;
      if (!opt.enabled && opt.reason) b.title = opt.reason;
      b.addEventListener("click", () => {
        for (const x of optRow.querySelectorAll("button")) x.disabled = true;
        hooks.resolveDecision(pending.id, opt.id, {}).catch(() => {});
      });
      optRow.appendChild(b);
    }
    box.appendChild(optRow);
    const hint = document.createElement("p");
    hint.className = "muted";
    hint.textContent = "Answer from here or from the decision dialog — " +
      "both drive the same single command.";
    box.appendChild(hint);
    card.appendChild(box);
  } else if (pending) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = "A different decision is waiting for you first.";
    card.appendChild(note);
  } else {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = (rels.length || offers.length)
      ? "No pending decision — existing courtships and offers are listed " +
        "above."
      : "No match yet — try Find a match.";
    card.appendChild(note);
  }
}

/* ------------------------------- weddings */

function moneyFmt(n) { return formatMoney(n); }

function renderWeddings(card) {
  const s = hooks.getState();
  const unions = (view && view.unions) || [];
  const catalog = (view && view.catalog) ||
    { venues: {}, outfits: {}, honeymoons: {} };
  const cash = (view && typeof view.money === "number")
    ? view.money : (s ? s.money : 0);
  const active = unions.filter((u) => u.status === "active");

  if (!active.length) {
    const note = document.createElement("p");
    note.className = "muted";
    note.textContent = "No active unions — there is no one to marry or " +
      "part from yet.";
    card.appendChild(note);
  }

  for (const u of active) {
    const box = document.createElement("div");
    box.className = "card";
    const head = document.createElement("div");
    head.className = "row";
    if (u.partner) head.appendChild(miniPersonChip(
      { name: u.partner.name, age: u.partner.age, alive: u.partner.alive,
        appearance: u.partner.appearance }, 56));
    const title = document.createElement("div");
    const rec = RECOGNITION_LABELS[u.recognition] || u.recognition || "";
    title.innerHTML = `<b>${esc(u.partner ? u.partner.name : "Someone")}</b>
      <div class="muted">${esc(u.kind)} · ${esc(rec)}` +
      (u.ceremony_held ? " · married" : "") + `</div>`;
    head.appendChild(title);
    box.appendChild(head);

    if (!u.ceremony_held) box.appendChild(plannerForm(u, catalog, cash));
    else box.appendChild(renewalForm(u, cash));
    box.appendChild(divorceForm(u, cash, catalog));
    card.appendChild(box);
  }
}

function plannerForm(u, catalog, cash) {
  const wrap = document.createElement("div");
  wrap.innerHTML = "<h4>Plan the wedding</h4>";
  const sel = (name, items, isFree) => {
    const row = document.createElement("label");
    row.className = "field";
    row.style.minWidth = "160px";
    row.appendChild(document.createTextNode(name));
    const selEl = document.createElement("select");
    selEl.dataset.picker = name;
    for (const [id, item] of Object.entries(items)) {
      const o = document.createElement("option");
      o.value = id;
      const price = item.per_guest != null
        ? item.base + "+/guest" : (item.price != null ? item.price : item.base);
      o.textContent = item.label + (price ? " — " +
        (typeof price === "number" ? moneyFmt(price) : price) : " — free");
      selEl.appendChild(o);
    }
    row.appendChild(selEl);
    return row;
  };
  const grid = document.createElement("div");
  grid.className = "row";
  grid.appendChild(sel("Venue", catalog.venues || {}));
  grid.appendChild(sel("Outfit", catalog.outfits || {}));
  grid.appendChild(sel("Honeymoon", catalog.honeymoons || {}));
  const guestsWrap = document.createElement("label");
  guestsWrap.className = "field";
  guestsWrap.style.minWidth = "120px";
  guestsWrap.appendChild(document.createTextNode("Guests"));
  const guests = document.createElement("input");
  guests.type = "number";
  guests.min = "0";
  guests.max = "500";
  guests.value = "20";
  guestsWrap.appendChild(guests);
  grid.appendChild(guestsWrap);
  wrap.appendChild(grid);

  const total = document.createElement("p");
  total.className = "muted";
  wrap.appendChild(total);
  const finesBox = document.createElement("div");
  wrap.appendChild(finesBox);

  const go = document.createElement("button");
  go.className = "primary";
  go.textContent = "Book the wedding";

  // Q2: A's stored quote is the ONLY source of law-fine charges — no
  // web-side forecasting. Sampled once and persisted, so this is a POST;
  // the ceremony posts exactly these lines.
  let quote = null;
  go.disabled = true;
  (async () => {
    const s = hooks.getState();
    try {
      const res = await fetch("/api/games/" +
                              encodeURIComponent(s.gameId) +
                              "/wedding-quote",
                              { method: "POST",
                                headers: { "Content-Type": "application/json" },
                                body: JSON.stringify({ union_id: u.union_id }) });
      const data = await res.json().catch(() => null);
      if (!res.ok) {
        throw new Error((data && data.error && data.error.message) ||
                        "Request failed (" + res.status + ")");
      }
      quote = data;
    } catch (err) {
      setStatus("Fine quote unavailable: " + err.message, true);
    }
    finesBox.textContent = "";
    if (quote && quote.lines && quote.lines.length) {
      const note = document.createElement("p");
      note.className = "muted";
      note.textContent = "Law charges for this ceremony:";
      finesBox.appendChild(note);
      const ul = document.createElement("ul");
      for (const line of quote.lines) {
        const li = document.createElement("li");
        li.textContent = line.description + ": " + moneyFmt(line.amount);
        ul.appendChild(li);
      }
      finesBox.appendChild(ul);
    }
    compute();
  })();

  const compute = () => {
    const v = (catalog.venues || {})[grid.querySelector(
      '[data-picker="Venue"]').value] || { base: 0, per_guest: 0 };
    const o = (catalog.outfits || {})[grid.querySelector(
      '[data-picker="Outfit"]').value] || { price: 0 };
    const h = (catalog.honeymoons || {})[grid.querySelector(
      '[data-picker="Honeymoon"]').value] || { price: 0 };
    const g = Math.max(0, Math.min(500, Number(guests.value) || 0));
    const ceremony = v.base + v.per_guest * g + o.price + h.price;
    const fineAmt = quote ? quote.total : null;
    const cost = ceremony + (fineAmt || 0);
    total.textContent = "Total: " + (cost ? moneyFmt(cost) : "free") +
      (fineAmt ? " (ceremony " + moneyFmt(ceremony) + " + " +
        moneyFmt(fineAmt) + " law charges)" : "") +
      " · you have " + moneyFmt(cash);
    // the free configuration (courthouse, no frills) is always legal,
    // but a hostile law's ceremony fines still apply on top of it; while
    // the quote loads the booking button stays disabled
    go.disabled = fineAmt === null || cost > cash;
    go.textContent = cost > cash
      ? "Book the wedding — costs " + moneyFmt(cost) + " and you have " +
        moneyFmt(cash)
      : "Book the wedding" + (cost ? " (" + moneyFmt(cost) + ")" : " (free)");
    return go.disabled ? null : {
      venue_id: grid.querySelector('[data-picker="Venue"]').value,
      outfit_id: grid.querySelector('[data-picker="Outfit"]').value,
      honeymoon_id: grid.querySelector('[data-picker="Honeymoon"]').value,
      guests: g,
    };
  };
  grid.addEventListener("change", compute);
  guests.addEventListener("input", compute);
  go.addEventListener("click", async () => {
    const args = compute();
    if (!args) return;
    go.disabled = true;
    await sendWedding("plan_wedding", { union_id: u.union_id, ...args }, go);
  });
  wrap.appendChild(go);
  compute();
  // Highlight the always-available free configuration.
  const freeNote = document.createElement("p");
  freeNote.className = "muted";
  freeNote.textContent = "A courthouse ceremony with hand-me-down finery " +
    "and no honeymoon is always free of ceremony costs — any law charges " +
    "above still apply.";
  wrap.appendChild(freeNote);
  return wrap;
}

function renewalForm(u, cash) {
  const wrap = document.createElement("div");
  wrap.innerHTML = "<h4>Renew the vows</h4>";
  const row = document.createElement("div");
  row.className = "row";
  const tiers = { backyard: 0, dinner: 400, extravaganza: 4000 };
  for (const [tier, cost] of Object.entries(tiers)) {
    const b = document.createElement("button");
    b.textContent = tier[0].toUpperCase() + tier.slice(1) +
      (cost ? " (" + moneyFmt(cost) + ")" : " (free)");
    b.disabled = cost > cash;
    if (cost > cash) b.title = "Costs " + moneyFmt(cost) + " and you have " +
      moneyFmt(cash);
    b.addEventListener("click", async () => {
      for (const x of row.querySelectorAll("button")) x.disabled = true;
      await sendWedding("renew_vows",
                        { union_id: u.union_id, tier: tier }, b);
    });
    row.appendChild(b);
  }
  wrap.appendChild(row);
  return wrap;
}

function divorceForm(u, cash, catalog) {
  const wrap = document.createElement("div");
  wrap.innerHTML = "<h4>Part ways</h4>";
  const settlement = u.prenup_amount || 0;
  const perChild = (catalog && catalog.child_support_per_child) || 2000;
  const kids = u.shared_children || [];
  const support = perChild * kids.length;
  const payableSupport = Math.max(
    0, Math.min(support, cash - Math.min(settlement, cash)));
  const outstanding = support - payableSupport;
  const lines = [];
  lines.push(settlement
    ? "Prenup settlement to " + (u.partner ? u.partner.name : "them") +
      ": " + moneyFmt(settlement)
    : "No prenup — no settlement.");
  if (kids.length) {
    lines.push("Child support for " + kids.length + " shared child" +
      (kids.length > 1 ? "ren" : "") + ": " + moneyFmt(support) +
      " (pays " + moneyFmt(payableSupport) + " now" +
      (outstanding > 0
        ? ", " + moneyFmt(outstanding) + " recorded as owed" : "") + ")");
  }
  lines.push("Recognition never blocks a parting. Other unions and the " +
    "children's parentage are untouched.");
  for (const t of lines) {
    const p = document.createElement("p");
    p.className = "muted";
    p.textContent = t;
    wrap.appendChild(p);
  }
  const row = document.createElement("div");
  row.className = "row";
  const kidsRow = document.createElement("div");
  kidsRow.className = "row";
  for (const c of kids) {
    kidsRow.appendChild(miniPersonChip(
      { name: c.name, age: c.age, alive: c.alive,
        appearance: c.appearance }, 40));
  }
  if (kids.length) row.appendChild(kidsRow);
  const go = document.createElement("button");
  go.textContent = "Divorce " + (u.partner ? u.partner.name : "this union");
  // divorce itself is always legal at $0 (support is capped to cash);
  // only the settlement needs money up front.
  go.disabled = settlement > cash;
  if (settlement > cash) {
    go.title = "The settlement is " + moneyFmt(settlement) + " and you " +
      "have " + moneyFmt(cash);
  }
  go.addEventListener("click", async () => {
    go.disabled = true;
    await sendWedding("divorce_union", { union_id: u.union_id }, go);
  });
  row.appendChild(go);
  wrap.appendChild(row);
  return wrap;
}

async function sendWedding(op, args, btn) {
  const s = hooks.getState();
  try {
    const res = await fetch("/api/games/" + encodeURIComponent(s.gameId) +
                            "/wedding", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        op, args,
        request_id: crypto.randomUUID(),
        expected_revision: s.revision,
      }),
    });
    const data = await res.json().catch(() => null);
    if (!res.ok) {
      throw new Error((data && data.error && data.error.message) ||
                      "Request failed (" + res.status + ")");
    }
    setStatus("Done.");
    await (hooks.refresh && hooks.refresh());
    await reload();
  } catch (err) {
    if (btn) btn.disabled = false;
    if (err.message && err.message.indexOf("stale_revision") !== -1) {
      await (hooks.refresh && hooks.refresh()).catch(() => null);
    }
    setStatus("Failed: " + err.message, true);
  }
}

/* ------------------------------- reputation / known secrets */

function renderReputation(card) {
  const facts = (view && view.facts) || [];
  const reputation = (view && view.reputation) || [];

  const h = document.createElement("h4");
  h.textContent = "What I know";
  card.appendChild(h);
  if (!facts.length) {
    const p = document.createElement("p");
    p.className = "muted";
    p.textContent = "Nothing yet — secrets surface as life unfolds and " +
      "word gets around.";
    card.appendChild(p);
  }
  for (const f of facts) {
    const row = document.createElement("div");
    row.className = "row";
    for (const sub of f.subjects || []) {
      row.appendChild(miniPersonChip(
        { name: sub.name, age: sub.age, alive: sub.alive,
          appearance: sub.appearance }, 40));
    }
    const txt = document.createElement("span");
    const names = (f.subjects || []).map((x) => x.name).join(" and ");
    txt.textContent = (FACT_LABELS[f.kind] || "something") +
      (names ? " — " + names : "") +
      (f.source ? " (learned by " + f.source + ")" : "");
    row.appendChild(txt);
    card.appendChild(row);
  }

  const h2 = document.createElement("h4");
  h2.textContent = "Standing (as I know it)";
  card.appendChild(h2);
  if (!reputation.length) {
    const p = document.createElement("p");
    p.className = "muted";
    p.textContent = "No word about anyone yet.";
    card.appendChild(p);
  }
  for (const r of reputation) {
    const row = document.createElement("div");
    row.className = "row";
    row.appendChild(miniPersonChip(
      { name: r.person.name, age: r.person.age, alive: r.person.alive,
        appearance: r.person.appearance }, 40));
    const bar = document.createElement("div");
    bar.style.flex = "1";
    const label = document.createElement("span");
    label.textContent = r.person.name + " — word is " +
      (r.value >= 0 ? "good" : "poor") + " (" + r.value + ")";
    bar.appendChild(label);
    const track = document.createElement("div");
    track.className = "track";
    const fill = document.createElement("div");
    fill.className = "fill " + (r.value >= 0 ? "good" : "low");
    fill.style.width = Math.min(100, Math.abs(r.value)) + "%";
    track.appendChild(fill);
    bar.appendChild(track);
    row.appendChild(bar);
    card.appendChild(row);
  }
}
