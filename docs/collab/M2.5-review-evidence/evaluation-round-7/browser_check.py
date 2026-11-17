import os,tempfile,threading,sys,json
sys.path.insert(0,os.getcwd())
from web import server
from tools.cdp_client import launch_chrome
from tools.browser_check import Runner,write_report,DEFERRED_PENDING
from tests.browser.flows import FLOWS
out='docs/collab/M2.5-review-evidence/evaluation-round-7/browser'
os.makedirs(out,exist_ok=True)
h=server.make_server(0,save_dir=tempfile.mkdtemp(prefix='review-web-'));h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start()
c=None
try:
 c,conn=launch_chrome();s=conn.new_session();r=Runner('http://127.0.0.1:%d'%h.server_port,s,out,service=h.service)
 for f in FLOWS:
  if f['name'].startswith('m25-'):r.run_flow(f)
 write_report(out,r.results,DEFERRED_PENDING)
 print(json.dumps(r.results,indent=2))
finally:
 if c:c.terminate()
 h.shutdown()
