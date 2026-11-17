import os,tempfile,threading,sys,json
sys.path.insert(0,os.getcwd())
from web import server
from tools.cdp_client import launch_chrome
from tools.browser_check import Runner,write_report,DEFERRED_PENDING
from tests.browser.flows import FLOWS
out='docs/collab/M2.5-review-evidence/re-review-browser'
os.makedirs(out,exist_ok=True)
h=server.make_server(0,save_dir=tempfile.mkdtemp(prefix='review-web-'));h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start()
c=None
try:
 c,conn=launch_chrome();s=conn.new_session();r=Runner('http://127.0.0.1:%d'%h.server_port,s,out,service=h.service)
 for f in FLOWS:
  if f['name']=='m25-family-tree':r.run_flow(f)
 s.js("document.getElementById('m25Toggle').click()")
 s.wait_js("document.getElementById('m25Body') && document.getElementById('m25Body').innerText.length > 10")
 for tab in ['dating','weddings','reputation']:
  s.js("document.querySelector('[data-tab=%s]').click();document.getElementById('m25Card').scrollIntoView()"%tab)
  print('UI',tab,s.js("JSON.stringify({text:document.getElementById('m25Body').innerText,overflow:document.documentElement.scrollWidth>innerWidth,buttons:Array.from(document.getElementById('m25Body').querySelectorAll('button')).map(b=>({text:b.innerText,disabled:b.disabled}))})"))
  import base64
  open(out+'/own-'+tab+'.png','wb').write(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
 os.makedirs(out+'/own',exist_ok=True);write_report(out+'/own',r.results,DEFERRED_PENDING)
 print(json.dumps(r.results,indent=2))
finally:
 if c:c.terminate()
 h.shutdown()
