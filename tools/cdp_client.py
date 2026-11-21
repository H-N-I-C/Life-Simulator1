"""Minimal Chrome DevTools Protocol client (stdlib only, no deps).

Wraps a raw websocket (RFC 6455 client-masked frames) and exposes:
  Connection       browser-level connection (create/attach targets)
  Session          per-target session with call()/js(), robust condition
                   polling, and automatic collection of console errors,
                   uncaught exceptions and failed network requests.

Designed for headless Chromium launched with --remote-debugging-port=0.
"""

import base64
import json
import os
import socket
import struct
import subprocess
import time

CHROMIUM = None
for _cand in ("chromium", "chromium-browser", "google-chrome"):
    from shutil import which
    if which(_cand):
        CHROMIUM = _cand
        break


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
        head, _, leftover = resp.partition(b"\r\n\r\n")
        if b"101" not in head.split(b"\r\n")[0]:
            raise RuntimeError("websocket handshake failed: %r" % resp[:120])
        self.buf = leftover

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
        self.sock.sendall(bytes(header) +
                          bytes(b ^ mask[i % 4] for i, b in enumerate(data)))

    def _read(self, n):
        while len(self.buf) < n:
            chunk = self.sock.recv(65536)
            if not chunk:
                raise RuntimeError("socket closed")
            self.buf += chunk
        out, self.buf = self.buf[:n], self.buf[n:]
        return out

    def recv_nowait(self):
        """Read one message with a short socket timeout; None on timeout."""
        self.sock.settimeout(0.1)
        try:
            return self.recv()
        except (TimeoutError, socket.timeout):
            return None
        finally:
            self.sock.settimeout(None)

    def recv(self):
        while True:
            b1, b2 = self._read(2)
            opcode = b1 & 0x0F
            n = b2 & 0x7F
            if n == 126:
                n = struct.unpack(">H", self._read(2))[0]
            elif n == 127:
                n = struct.unpack(">Q", self._read(8))[0]
            mask = self._read(4) if b2 & 0x80 else None
            payload = self._read(n)
            if mask:
                payload = bytes(b ^ mask[i % 4] for i, b in enumerate(payload))
            if opcode == 0x9:  # ping -> pong
                self.sock.sendall(b"\x8a\x80" + os.urandom(4))
                continue
            if opcode == 0x8:
                raise RuntimeError("websocket closed")
            if opcode in (0xA,):  # pong
                continue
            text = payload.decode("utf-8", "replace")
            return text if text else self.recv()


class _Caller:
    """Shared id/dispatch loop. Subclasses set .ws and .session_id."""

    ws = None
    session_id = None

    def __init__(self):
        self.next_id = 1
        self.console_errors = []      # [{text, url}]
        self.exceptions = []          # [description]
        self.failed_requests = []     # [{url, error or status}]
        self._inflight = set()
        self._req_urls = {}
        self._load_fired = False

    def _record_event(self, msg):
        if self.session_id and msg.get("sessionId") != self.session_id:
            return  # a shared websocket also carries the other tabs' events
        m = msg.get("method")
        p = msg.get("params", {})
        if m == "Runtime.consoleAPICalled" and p.get("type") == "error":
            text = " ".join(str(a.get("value", a.get("description", "")))
                            for a in p.get("args", []))
            self.console_errors.append(
                {"text": text[:400],
                 "url": (p.get("stackTrace", {}).get("callFrames") or [{}])[0]
                 .get("url", "")})
        elif m == "Runtime.exceptionThrown":
            d = p.get("exceptionDetails", {})
            ex = d.get("exception", {})
            self.exceptions.append(str(ex.get("description") or
                                       d.get("text", "exception"))[:400])
        elif m == "Network.requestWillBeSent":
            # A navigation invalidates outstanding work from the previous
            # document. Chromium need not emit completion for each aborted
            # portrait fetch on reload; retaining those IDs makes wait_idle
            # time out even though the new page has fully rendered.
            if p.get("type") == "Document":
                self._inflight.clear()
                self._load_fired = False
            # The load event marks document completion; loadingFinished for
            # the top document is not always emitted.
            # Locally-created blob portraits are not network work and
            # Chromium can omit their terminal event when a render replaces
            # them. Track only actual HTTP requests for network-idle.
            request_url = p.get("request", {}).get("url", "")
            if p.get("type") != "Document" and request_url.startswith(
                    ("http://", "https://")):
                self._inflight.add(p["requestId"])
            self._req_urls[p["requestId"]] = p.get("request", {}).get("url", "")
        elif m == "Network.loadingFinished":
            self._inflight.discard(p.get("requestId"))
        elif m == "Network.loadingFailed":
            self._inflight.discard(p.get("requestId"))
            # cancelled loads during navigation are not failures
            if not p.get("canceled"):
                self.failed_requests.append(
                    {"url": self._req_urls.get(p.get("requestId"), ""),
                     "error": p.get("errorText", "failed")})
        elif m == "Network.responseReceived":
            status = p.get("response", {}).get("status", 0)
            if status >= 400:
                self.failed_requests.append(
                    {"url": p["response"].get("url", ""),
                     "error": "HTTP %d" % status})
        elif m == "Page.loadEventFired":
            self._load_fired = True

    def call(self, method, params=None, timeout=20):
        if method in ("Page.navigate", "Page.reload"):
            self._inflight.clear()
            self._load_fired = False
        mid = self.next_id
        self.next_id += 1
        msg = {"id": mid, "method": method, "params": params or {}}
        if self.session_id:
            msg["sessionId"] = self.session_id
        self.ws.send(json.dumps(msg))
        deadline = time.time() + timeout
        while time.time() < deadline:
            resp = json.loads(self.ws.recv())
            if resp.get("id") == mid:
                if "error" in resp:
                    raise RuntimeError("%s: %s" % (method, resp["error"]))
                return resp.get("result", {})
            sid = resp.get("sessionId")
            child = getattr(self, "children", {}).get(sid)
            (child._record_event(resp) if child
             else self._record_event(resp))
        raise TimeoutError(method)

    def js(self, expression, timeout=20):
        res = self.call("Runtime.evaluate",
                        {"expression": expression, "returnByValue": True,
                         "awaitPromise": True}, timeout)
        if res.get("exceptionDetails"):
            d = res["exceptionDetails"]
            raise RuntimeError("JS: " + json.dumps(d)[:900])
        return res.get("result", {}).get("value")

    def wait_js(self, expression, timeout=10, desc=None):
        """Poll a JS condition until truthy. No fixed sleeps."""
        deadline = time.time() + timeout
        last = None
        while time.time() < deadline:
            last = self.js(expression)
            # Strict booleans: a numeric 0, "" or {} must NOT pass. Flow
            # expressions must return real booleans (wrap DOM lookups in !!).
            if isinstance(last, bool):
                if last:
                    return last
            elif last is not None:
                raise TypeError(
                    "wait_js expression returned non-boolean %r "
                    "(%s); return an explicit boolean" %
                    (last, desc or expression[:60]))
            time.sleep(0.1)
        raise TimeoutError("wait_js: %s (last=%r)" %
                           (desc or expression[:80], last))

    def wait_idle(self, timeout=10):
        """Wait until page loaded and no network request in flight."""
        deadline = time.time() + timeout
        quiet_since = None
        # Target.createTarget(url) may finish loading before Page.enable.
        # In that case no load event can arrive for this attached session.
        if self.js("document.readyState === 'complete'"):
            self._load_fired = True
        while time.time() < deadline:
            # Pump events: idle state only updates as messages are read.
            msg = self.ws.recv_nowait()
            while msg is not None:
                self._record_event(json.loads(msg))
                msg = self.ws.recv_nowait()
            if self._load_fired and not self._inflight:
                if quiet_since and time.time() - quiet_since > 0.3:
                    return
                quiet_since = quiet_since or time.time()
            else:
                quiet_since = None
            time.sleep(0.05)
        raise TimeoutError("network never went idle; in flight: %r"
                           % sorted(self._req_urls.get(r, r)
                                    for r in self._inflight))

    def key(self, key, code=None, key_code=None):
        """Send a trusted key event (e.g. Tab, Enter, ArrowRight)."""
        spec = {"key": key, "code": code or key,
                "windowsVirtualKeyCode": key_code or 0}
        base = {"type": "keyDown", **spec}
        self.call("Input.dispatchKeyEvent", base)
        self.call("Input.dispatchKeyEvent", {"type": "keyUp", **spec})


class Connection(_Caller):
    def __init__(self, ws_url):
        super().__init__()
        self.ws = WS(ws_url)
        self.children = {}

    def new_session(self, url="about:blank"):
        target = self.call("Target.createTarget", {"url": url})["targetId"]
        session_id = self.call("Target.attachToTarget",
                               {"targetId": target,
                                "flatten": True})["sessionId"]
        s = Session()
        s.ws = self.ws
        s.session_id = session_id
        s.next_id = self.next_id
        self.next_id += 100000  # keep ids unique across sessions
        self.children[session_id] = s
        s.call("Runtime.enable")
        s.call("Page.enable")
        s.call("Network.enable")
        return s


class Session(_Caller):
    pass


def launch_chrome(user_data_dir=None):
    """Launch headless chromium; returns (proc, browser Connection).

    user_data_dir: optional isolated profile dir (pass a per-run temp dir
    and delete it after the run — QA rule, TMPDIR-based profiles must not
    leak between runs)."""
    argv = [CHROMIUM, "--headless=new", "--disable-gpu", "--no-sandbox",
            "--remote-debugging-port=0", "about:blank"]
    if user_data_dir:
        argv.insert(1, "--user-data-dir=%s" % user_data_dir)
    proc = subprocess.Popen(
        argv,
        stderr=subprocess.PIPE, stdout=subprocess.DEVNULL, text=True,
        # Own process group so callers can kill the whole chromium tree
        # (headless spawns children that outlive a plain terminate()).
        start_new_session=True)
    ws_url = None
    deadline = time.time() + 20
    line = ""
    while time.time() < deadline:
        line = proc.stderr.readline()
        if "DevTools listening on" in line:
            ws_url = line.split("DevTools listening on", 1)[1].strip()
            break
    if not ws_url:
        proc.kill()
        raise RuntimeError("no DevTools URL; last line: %r" % line[:200])
    return proc, Connection(ws_url)
