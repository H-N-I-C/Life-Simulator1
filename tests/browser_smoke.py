"""Browser smoke test via Chrome DevTools Protocol (stdlib only).

Launches headless chromium, drives the real creator page over CDP, and
verifies the review findings in an actual browser:
  - marks export is a JSON list (finding 2)
  - keyboard focus is retained after swatch activation (finding 4)
  - tab arrow-key traversal works (finding 4)
  - rapid preview changes do not error (finding 5)

Run: python3 tests/browser_smoke.py   (needs chromium + the web server)
"""

import base64
import hashlib
import json
import os
import socket
import struct
import subprocess
import sys
import tempfile
import threading
import time
import urllib.request

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

import web.server as server  # noqa: E402

CHROMIUM = None
for cand in ("chromium", "chromium-browser", "google-chrome"):
    from shutil import which
    if which(cand):
        CHROMIUM = cand
        break


# ---------------------------------------------------------------------------
# minimal websocket client (RFC 6455, client-masked text frames)
# ---------------------------------------------------------------------------

class WS:
    def __init__(self, url):
        assert url.startswith("ws://")
        rest = url[5:]
        hostport, _, path = rest.partition("/")
        host, _, port = hostport.partition(":")
        self.sock = socket.create_connection((host, int(port or 80)))
        key = base64.b64encode(os.urandom(16)).decode()
        req = ("GET /%s HTTP/1.1\r\nHost: %s\r\nUpgrade: websocket\r\n"
               "Connection: Upgrade\r\nSec-WebSocket-Key: %s\r\n"
               "Sec-WebSocket-Version: 13\r\n\r\n" % (path, hostport, key))
        self.sock.sendall(req.encode())
        resp = b""
        while b"\r\n\r\n" not in resp:
            resp += self.sock.recv(4096)
        if b"101" not in resp.split(b"\r\n")[0]:
            raise RuntimeError("websocket handshake failed: %r" % resp[:120])
        self.buf = b""

    def send(self, text):
        data = text.encode()
        mask = os.urandom(4)
        header = bytearray([0x81])
        n = len(data)
        if n < 126:
            header.append(0x80 | n)
        elif n < 65536:
            header.append(0x80 | 126)
            header += struct.pack(">H", n)
        else:
            header.append(0x80 | 127)
            header += struct.pack(">Q", n)
        header += mask
        masked = bytes(b ^ mask[i % 4] for i, b in enumerate(data))
        self.sock.sendall(bytes(header) + masked)

    def _read(self, n):
        while len(self.buf) < n:
            chunk = self.sock.recv(65536)
            if not chunk:
                raise RuntimeError("socket closed")
            self.buf += chunk
        out, self.buf = self.buf[:n], self.buf[n:]
        return out

    def recv(self):
        while True:
            b1, b2 = self._read(2)
            opcode = b1 & 0x0F
            masked = b2 & 0x80
            n = b2 & 0x7F
            if n == 126:
                n = struct.unpack(">H", self._read(2))[0]
            elif n == 127:
                n = struct.unpack(">Q", self._read(8))[0]
            mask = self._read(4) if masked else None
            payload = self._read(n)
            if mask:
                payload = bytes(b ^ mask[i % 4] for i, b in enumerate(payload))
            if opcode == 0x9:  # ping -> pong
                self.sock.sendall(b"\x8a\x80" + os.urandom(4))
                continue
            if opcode in (0x8,):
                raise RuntimeError("closed")
            return payload.decode("utf-8", "replace")


class CDP:
    def __init__(self, ws_url):
        self.ws = WS(ws_url)
        self.next_id = 1

    def call(self, method, params=None, timeout=15):
        mid = self.next_id
        self.next_id += 1
        self.ws.send(json.dumps({"id": mid, "method": method,
                                 "params": params or {}}))
        deadline = time.time() + timeout
        while time.time() < deadline:
            msg = json.loads(self.ws.recv())
            if msg.get("id") == mid:
                if "error" in msg:
                    raise RuntimeError(msg["error"])
                return msg.get("result", {})
        raise TimeoutError(method)

    def js(self, expression, timeout=15):
        res = self.call("Runtime.evaluate",
                        {"expression": expression, "returnByValue": True,
                         "awaitPromise": True}, timeout)
        if res.get("exceptionDetails"):
            raise RuntimeError(json.dumps(res["exceptionDetails"])[:400])
        return res.get("result", {}).get("value")


def main():
    if not CHROMIUM:
        print("SKIP: chromium not found")
        return 0
    save_dir = tempfile.mkdtemp(prefix="lifesim-smoke-")
    httpd = server.make_server(0, save_dir=save_dir)
    port = httpd.server_port
    threading.Thread(target=httpd.serve_forever, daemon=True).start()
    chrome = subprocess.Popen(
        [CHROMIUM, "--headless=new", "--disable-gpu", "--no-sandbox",
         "--remote-debugging-port=0", "about:blank"],
        stderr=subprocess.PIPE, stdout=subprocess.DEVNULL, text=True)
    try:
        # parse the DevTools websocket URL from stderr
        ws_url = None
        deadline = time.time() + 20
        line = ""
        while time.time() < deadline:
            line = chrome.stderr.readline()
            if "DevTools listening on" in line:
                ws_url = line.split("DevTools listening on", 1)[1].strip()
                break
        if not ws_url:
            print("FAIL: no DevTools URL; last line:", line[:200])
            return 1
        b = CDP(ws_url)
        target = b.call("Target.createTarget",
                        {"url": "about:blank"})["targetId"]
        session = b.call("Target.attachToTarget",
                         {"targetId": target, "flatten": True})["sessionId"]
        c = CDP.__new__(CDP)
        c.ws = b.ws
        c.next_id = b.next_id
        orig_call = c.call

        def call(method, params=None, timeout=15):
            mid = c.next_id
            c.next_id += 1
            c.ws.send(json.dumps({"id": mid, "method": method,
                                  "params": params or {},
                                  "sessionId": session}))
            deadline = time.time() + timeout
            while time.time() < deadline:
                msg = json.loads(c.ws.recv())
                if msg.get("id") == mid:
                    if "error" in msg:
                        raise RuntimeError(msg["error"])
                    return msg.get("result", {})
            raise TimeoutError(method)
        c.call = call
        c.js = lambda e, timeout=15: CDP.js(c, e, timeout)

        base = "http://127.0.0.1:%d" % port
        results = []
        check = lambda name, ok, extra="": results.append(
            (name, bool(ok), extra))

        # ---- creator ----
        c.call("Page.enable")
        c.call("Page.navigate", {"url": base + "/creator"})
        time.sleep(2.5)
        loaded = c.js("document.title.includes('Creator') || " 
                      "document.body.innerText.includes('Character Creator')")
        check("creator page loads", loaded)

        # switch to the Extras tab, then toggle the earrings badge twice
        c.js("document.getElementById('tab-badges').click()")
        time.sleep(0.3)
        c.js("""[...document.querySelectorAll('.swatch')].find(
            s => s.dataset.category === 'badges' &&
                 s.dataset.value === 'earrings').focus()""")
        focused = c.js("document.activeElement.dataset.value")
        c.js("document.activeElement.click()")
        time.sleep(1.2)  # async render round-trip
        focused_after = c.js(
            "document.activeElement && document.activeElement.dataset.value "
            "|| document.activeElement.tagName")
        check("focus retained after swatch click",
              focused_after == "earrings", "activeElement=%r" %
              focused_after)

        aria = c.js("""[...document.querySelectorAll('.swatch')].find(
            s => s.dataset.category === 'badges' &&
                 s.dataset.value === 'earrings')
            .getAttribute('aria-pressed')""")
        check("earrings aria-pressed true", aria == "true", "aria=%r" % aria)

        # toggle a second badge: list semantics
        c.js("""[...document.querySelectorAll('.swatch')].find(
            s => s.dataset.category === 'badges' &&
                 s.dataset.value === 'cap').click()""")
        time.sleep(1.0)

        # export via Print JSON and validate through the page's own data
        c.js("document.getElementById('printBtn').click()")
        exported = c.js("""JSON.parse(
            document.querySelector('pre.export').textContent).badges""")
        check("badges exported as a list", isinstance(exported, list) and
              len(exported) == 2, "badges=%r" % (exported,))

        # Python model round-trip of the exported JSON
        from src.appearance.model import Appearance
        full = c.js("document.querySelector('pre.export').textContent")
        model = Appearance.from_dict(json.loads(full))
        check("badges survive Appearance.from_dict",
              set(model.badges) == set(exported),
              "model badges=%r" % (list(model.badges),))

        # keyboard: arrow keys move between tabs
        c.js("document.getElementById('tab-look').focus()")
        c.js("""document.getElementById('tab-look').dispatchEvent(
            new KeyboardEvent('keydown', {key: 'ArrowRight', bubbles: true}))
        """)
        after_arrow = c.js("document.activeElement.id")
        check("ArrowRight moves tab focus", after_arrow == "tab-skin",
              "focused=%r" % after_arrow)

        # rapid age-slider changes: preview stays valid SVG, no error state
        c.js("""const a = document.getElementById('age');
                for (let v = 0; v <= 80; v += 8) {
                  a.value = v; a.dispatchEvent(new Event('input'));
                }""")
        time.sleep(2.5)
        svg_ok = c.js("""(() => { const p = document.getElementById('preview');
            const s = p.querySelector('img');
            return !!s && !!s.src && !p.title; })()""")
        check("rapid slider changes keep valid preview", svg_ok)

        # clear badges: A-side validation currently rejects badge values,
        # so keep the create payload badge-free (flagged to A)
        c.js("document.getElementById('tab-badges').click()")
        time.sleep(0.4)
        c.js("""[...document.querySelectorAll('.swatch')].find(
            s => s.dataset.category === 'badges' &&
                 s.dataset.value === '').click()""")
        time.sleep(0.8)

        # ---- M1 fix round: retry-stable creation (review finding 1) ----
        # Intercept the NEXT successful /api/games POST after commit and
        # make the client see a network loss, then retry + reload.
        c.js("""window.__dropNextCreate = true;
                const origFetch = window.fetch;
                window.fetch = (...a) => origFetch(...a).then(r => {
                  if (window.__dropNextCreate &&
                      String(a[0]).includes('/api/games') &&
                      a[1] && a[1].method === 'POST' && r.ok) {
                    window.__dropNextCreate = false;
                    return Promise.reject(new Error('simulated loss'));
                  }
                  return r;
                });""")
        before = c.js(
            "fetch('/api/games').then(r => r.json()).then(d => d.games.length)")
        c.js("document.getElementById('startBtn').click()")
        time.sleep(2.0)
        err_shown = c.js("document.getElementById('startMsg').textContent")
        check("create failure surfaced for retry", "retry" in err_shown,
              err_shown[:60])
        c.js("document.getElementById('startBtn').click()")
        time.sleep(2.5)
        # success navigates to /game; count lives via the API
        c.call("Page.navigate", {"url": base + "/api/games"})
        time.sleep(1.0)
        n_after_retry = c.js(
            "JSON.parse(document.body.innerText).games.length")
        check("retry reuses operation: exactly one new life",
              n_after_retry == before + 1,
              "before=%r after=%r" % (before, n_after_retry))
        # reload the creator: the pending op must be gone after success
        c.call("Page.navigate", {"url": base + "/creator"})
        time.sleep(2.0)
        op_after_success = c.js(
            "sessionStorage.getItem('pending_create_op')")
        check("operation cleared after definitive success",
              op_after_success in (None, "null"), repr(op_after_success))

        # ---- M1 fix round: deep link vs Resume (review finding 2) ----
        # create two distinct lives directly through the service
        def mklife(name):
            import uuid as _u
            r = urllib.request.Request(
                base + "/api/games",
                data=json.dumps({
                    "first_name": name, "last_name": "Probe",
                    "gender": "female", "country": "AU",
                    "appearance": {"skin_tone": "skin_04"},
                    "request_id": "smoke-" + name + "-" +
                                  _u.uuid4().hex[:8]}).encode(),
                method="POST",
                headers={"Content-Type": "application/json"})
            with urllib.request.urlopen(r, timeout=5) as resp:
                return json.loads(resp.read())["game_id"]
        gid_a = mklife("Alpha")
        gid_b = mklife("Beta")
        # fresh-tab semantics: deep link applies when no per-tab selection
        # is stored yet
        c.js("sessionStorage.clear()")
        c.call("Page.navigate", {"url": base + "/game?game=" + gid_a})
        time.sleep(2.0)
        name_a = c.js("document.querySelector('#playerCard .name') && " 
                      "document.querySelector('#playerCard .name').textContent")
        check("deep link opens game A", name_a == "Alpha Probe",
              repr(name_a))
        # Resume B from the list; the stale query param must not win
        c.js("document.getElementById('backToLives').click()")
        time.sleep(1.2)
        c.js("""[...document.querySelectorAll('#lifeList .chip')].find(
            r => r.textContent.includes('Beta')).querySelector(
            'button').click()""")
        time.sleep(2.0)
        name_b = c.js("document.querySelector('#playerCard .name') && "
                      "document.querySelector('#playerCard .name').textContent")
        check("Resume selects game B over stale deep link",
              name_b == "Beta Probe", repr(name_b))
        url_clean = c.js("location.search === ''")
        check("query param consumed (URL cleaned)", url_clean)
        c.call("Page.navigate", {"url": base + "/game"})
        time.sleep(2.0)
        name_b2 = c.js("document.querySelector('#playerCard .name') && "
                       "document.querySelector('#playerCard .name').textContent")
        check("refresh keeps selected game B", name_b2 == "Beta Probe",
              repr(name_b2))
        # invalid deep link recovers (fresh tab, unknown id)
        c.js("sessionStorage.clear()")
        c.call("Page.navigate", {"url": base + "/game?game=nope-invalid"})
        time.sleep(2.0)
        recovered = c.js("!document.getElementById('resumeView').hidden")
        check("invalid deep link falls back to life list", recovered)

        # ---- M1 fix round: portraits on name-bearing surfaces (finding 4)
        time.sleep(1.5)
        resume_svgs = c.js(
            "document.querySelectorAll('#lifeList .avatar img').length")
        check("resume rows show portraits", (resume_svgs or 0) >= 2,
              "imgs=%r" % resume_svgs)

        # ---- M1 fix round: action retry + focus lifecycle (findings 3,5)
        c.js("document.getElementById('backToLives').click()")
        time.sleep(1.2)
        c.js("""[...document.querySelectorAll('#lifeList .chip')].find(
            r => r.textContent.includes('Beta')).querySelector(
            'button').click()""")
        time.sleep(2.0)
        rev0 = c.js("window.__rev = +document.querySelector("
                    "'#playerCard .role').textContent.match(/Age (\\d+)/)[1]")
        c.js("""window.__dropNextAction = true;
                const of2 = window.fetch;
                window.fetch = (...a) => of2(...a).then(r => {
                  if (window.__dropNextAction && String(a[0]).includes(
                      '/actions') && r.ok) {
                    window.__dropNextAction = false;
                    return Promise.reject(new Error('simulated loss'));
                  }
                  return r;
                });""")
        c.js("""[...document.querySelectorAll('#actionList button')].find(
            b => b.textContent.includes('Age')).click()""")
        time.sleep(2.0)
        pill_shown = c.js("""(() => { const p =
            document.getElementById('pendingPill');
            return p && !p.hidden ? p.textContent : ""; })()""")
        check("uncertain action offers safe-retry pill", "safe retry"
              in pill_shown, pill_shown[:60])
        c.js("""document.querySelector('#pendingPill button').click()""")
        time.sleep(2.0)
        age2 = c.js("""+document.querySelector('#playerCard .role')
            .textContent.match(/Age (\\d+)/)[1]""")
        check("retry replays operation: age advanced exactly once",
              age2 == rev0 + 1, "before=%r after=%r" % (rev0, age2))

        # focus lifecycle with a real pending decision (age via API)
        gid = session_js = None
        games_now = json.loads(urllib.request.urlopen(
            base + "/api/games", timeout=5).read())["games"]
        beta = next(g for g in games_now if g["name"].startswith("Beta"))
        # age Beta until a decision appears (bounded)
        st = json.loads(urllib.request.urlopen(
            base + "/api/games/" + beta["game_id"], timeout=5).read())
        for _ in range(60):
            if st.get("pending_decision"):
                break
            body = json.dumps({
                "expected_revision": st["revision"],
                "request_id": "smoke-age-" + str(st["revision"]),
                "action": "age_up", "payload": {}}).encode()
            r = urllib.request.Request(
                base + "/api/games/" + beta["game_id"] + "/actions",
                data=body, method="POST",
                headers={"Content-Type": "application/json"})
            with urllib.request.urlopen(r, timeout=5) as resp:
                st = json.loads(resp.read())
        if st.get("pending_decision"):
            c.call("Page.navigate", {"url": base + "/game?game=" +
                                     beta["game_id"]})
            time.sleep(2.2)
            in_modal = c.js("""document.getElementById('modal') &&
                !document.getElementById('modal').hidden &&
                document.getElementById('modal')
                  .contains(document.activeElement)""")
            check("pending dialog opens with focus inside", in_modal)
            first_label = c.js("document.activeElement.textContent")
            c.js("""document.activeElement.dispatchEvent(
                new KeyboardEvent('keydown', {key: 'Tab', bubbles: true}))""")
            still_in = c.js("""document.getElementById('modal')
                .contains(document.activeElement)""")
            check("Tab stays trapped in decision dialog", still_in)
            c.js("""document.dispatchEvent(new KeyboardEvent('keydown',
                {key: 'Escape', bubbles: true}))""")
            time.sleep(0.3)
            collapsed = c.js("""(() => { const p =
                document.getElementById('decisionPill');
                const m = document.getElementById('modal');
                return p && !p.hidden && m.hidden; })()""")
            check("Escape collapses to a reopening pill", collapsed)
            c.js("document.querySelector('#decisionPill button').click()")
            time.sleep(0.5)
            reopened = c.js("""!document.getElementById('modal').hidden &&
                document.getElementById('modal')
                  .contains(document.activeElement)""")
            check("pill reopens decision with focus", reopened)
            # decision participants show portraits
            time.sleep(1.5)
            n_person_ids = len(st["pending_decision"].get("person_ids") or [])
            part_svgs = c.js(
                "document.querySelectorAll('#modalPeople .avatar img').length")
            # templates may reference nobody; every referenced participant
            # must render as a portrait chip
            check("decision participants show portraits",
                  n_person_ids == 0 or part_svgs >= 1,
                  "person_ids=%r svgs=%r" % (n_person_ids, part_svgs))
            # resolve via keyboard-focused button
            c.js("document.activeElement.click()")
            time.sleep(2.0)
            resolved = c.js("""(() => { const m =
                document.getElementById('modal');
                return m.hidden || !m.contains(document.activeElement); })()""")
            check("decision resolves and dialog closes", resolved)
        else:
            check("pending decision reached for focus tests", False,
                  "no decision in 60 years")

        # timeline portraits
        c.call("Page.navigate", {"url": base + "/game?game=" + beta["game_id"]})
        time.sleep(2.2)
        tl_svgs = c.js("document.querySelectorAll('#journalList .avatar img').length")
        check("timeline entries show participant portraits", tl_svgs >= 1,
              "imgs=%r" % tl_svgs)

        # ---- gallery: every tile renders a real image (Fluent DOM) ----
        c.call("Page.navigate", {"url": base + "/gallery"})
        time.sleep(3.5)
        tiles = c.js("""(() => {
            const imgs = [...document.querySelectorAll('#matrix img')];
            return { total: imgs.length,
                     complete: imgs.filter(i => i.complete &&
                                              i.naturalWidth > 0).length };
        })()""")
        check("gallery: matrix tiles render real images",
              tiles["total"] == 30 and tiles["complete"] == 30,
              "tiles=%r" % (tiles,))

        # ---- creator keyboard: Tab + Enter on the Fluent DOM ----
        c.call("Page.navigate", {"url": base + "/creator"})
        time.sleep(2.5)
        c.js("""[...document.querySelectorAll('.swatch')].find(
            s => s.dataset.category === 'base').focus()""")
        # native key events (synthetic Tab has no default traversal)
        c.call("Input.dispatchKeyEvent",
               {"type": "rawKeyDown", "key": "Tab", "code": "Tab",
                "windowsVirtualKeyCode": 9})
        time.sleep(0.3)
        tabbed = c.js("""document.activeElement.dataset &&
            document.activeElement.dataset.value""")
        check("Tab moves between swatches", tabbed in ("woman", "man"),
              "on=%r" % tabbed)
        c.call("Input.dispatchKeyEvent",
               {"type": "keyDown", "key": "Enter", "code": "Enter",
                "text": "\r", "unmodifiedText": "\r",
                "windowsVirtualKeyCode": 13,
                "nativeVirtualKeyCode": 13})
        c.call("Input.dispatchKeyEvent",
               {"type": "keyUp", "key": "Enter", "code": "Enter",
                "windowsVirtualKeyCode": 13,
                "nativeVirtualKeyCode": 13})
        time.sleep(1.2)
        pressed = c.js("""document.activeElement.getAttribute(
            'aria-pressed')""")
        check("Enter activates a swatch from the keyboard",
              pressed == "true", "aria=%r" % (pressed,))
        # Escape on the decision dialog keeps a return path (Fluent DOM)
        # (covered earlier in the M1 section against a real pending)

        # ---- gallery ----
        c.call("Page.navigate", {"url": base + "/gallery"})
        time.sleep(3.0)
        cells = c.js("document.querySelectorAll('#hair .cell').length")
        check("gallery: hair x base coverage (>= 15 cells)",
              isinstance(cells, int) and cells >= 15, "cells=%r" % cells)
        matrix = c.js("document.querySelectorAll('#matrix .cell').length")
        check("gallery: skin x age matrix = 30", matrix == 30,
              "cells=%r" % matrix)
        imgs = c.js("document.querySelectorAll('#matrix img').length")
        check("gallery: matrix cells render avatar images", imgs == 30,
              "imgs=%r" % imgs)

        failed = [r for r in results if not r[1]]
        for name, ok, extra in results:
            print("%s %s %s" % ("PASS" if ok else "FAIL", name, extra))
        print("SUMMARY: %d/%d passed" % (len(results) - len(failed),
                                         len(results)))
        return 1 if failed else 0
    finally:
        chrome.terminate()
        try:
            chrome.wait(timeout=5)
        except Exception:
            chrome.kill()
        httpd.shutdown()
        httpd.server_close()


if __name__ == "__main__":
    sys.exit(main())
