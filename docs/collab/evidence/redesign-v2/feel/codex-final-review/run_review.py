import os,sys,tempfile,threading,json,base64,shutil
from pathlib import Path
sys.path.insert(0,os.getcwd())
Path('.review-tmp/codex-feel-tmp').mkdir(parents=True, exist_ok=True)
from web import server
from tools.cdp_client import launch_chrome
out=Path('docs/collab/evidence/redesign-v2/feel/codex-final-review');out.mkdir(parents=True,exist_ok=True)
tmp=tempfile.mkdtemp(dir='.review-tmp/codex-feel-tmp');h=server.make_server(0,save_dir=tmp);h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start();c=None;results={}
def snap(s,name):
 (out/(name+'.png')).write_bytes(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
def metrics(s):
 return s.js("""(()=>{let r={};for(let sel of ['.v2-scroll','.v2-dock','.v2-tabbar','.v2-agebtn','.v2e-card']){let e=document.querySelector(sel);if(e){let b=e.getBoundingClientRect();r[sel]={x:b.x,y:b.y,w:b.width,h:b.height,scroll:e.scrollTop,scrollHeight:e.scrollHeight,clientHeight:e.clientHeight}}}return {boxes:r,overflow:document.documentElement.scrollWidth>innerWidth,body:document.body.innerText,windowScroll:scrollY}})()""")
try:
 c,conn=launch_chrome()
 for w in [390,320]:
  st=h.service.new_game(dict(first_name='Alexandria',last_name='Review',gender='female',country='Australia',seed=7),'codex-feel-'+str(w));gid=st['game_id']
  s=conn.new_session();s.call('Emulation.setDeviceMetricsOverride',{'width':w,'height':844,'deviceScaleFactor':1,'mobile':True});s.call('Page.navigate',{'url':'http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid)})
  s.wait_js("!!document.querySelector('.v2-agebtn')");s.wait_idle();snap(s,f'home-{w}');r={'home':metrics(s)}
  s.js("window.feelTransitions=[];document.addEventListener('transitionrun',e=>{if(e.target.classList.contains('v2-stat-fill'))window.feelTransitions.push(e.propertyName)});")
  s.js("document.querySelector('.v2-agebtn').click();document.querySelector('.v2-agebtn').click()")
  s.wait_idle();r['doubleTapAge']=next(p['age'] for p in h.service.get_state(gid)['people'] if p['id']==st['player_id'])
  for i in range(12):
   if s.js("!document.getElementById('modal').hidden && !!document.querySelector('.v2e-option')"):break
   if s.js("!!document.querySelector('.v2e-continue') && !document.getElementById('modal').hidden"):s.js("document.querySelector('.v2e-continue').click()")
   s.js("document.querySelector('.v2-agebtn').click()");s.wait_idle()
  snap(s,f'aged-or-event-{w}');r['aged']=metrics(s)
  if s.js("!document.getElementById('modal').hidden && !!document.querySelector('.v2e-option')"):
   r['decision']=h.service.get_state(gid)['pending_decision'];snap(s,f'event-{w}')
   s.js("document.querySelector('.v2e-option:not(:disabled)').click()");s.wait_idle();snap(s,f'outcome-{w}');r['outcome']=metrics(s);r['serverOutcome']=h.service.get_state(gid).get('last_decision_outcome');r['ageEnabledDuringOutcome']=s.js("!document.querySelector('.v2-agebtn').disabled")
   if s.js("!!document.querySelector('.v2e-continue')"):s.js("document.querySelector('.v2e-continue').click()")
  r['journalAfterOutcome']=metrics(s);snap(s,f'journal-aged-{w}')
  r['transitionEvents']=s.js('window.feelTransitions');s.call('Page.reload');s.wait_idle();r['reloadFeed']=metrics(s);snap(s,f'journal-reloaded-{w}')
  r['headers']=s.js("[...document.querySelectorAll('.jcard-head')].map(e=>e.textContent)")
  s.js("document.querySelector('.v2-scroll').scrollTop=0")
  s.js("document.querySelector('.v2-agebtn').click()")
  s.wait_idle();r['olderReadingAfterAge']=metrics(s)
  if s.js("!document.getElementById('modal').hidden && !!document.querySelector('.v2e-later')"):
   s.js("document.querySelector('.v2e-later').click()")
   r['collapseHidden']=s.js("document.getElementById('modal').hidden && getComputedStyle(document.getElementById('modal')).display==='none'")
   s.js("document.querySelector('#decisionPill button').click()");s.wait_idle()
   s.js("document.querySelector('.v2e-option:not(:disabled)').click()");s.wait_idle()
   if s.js("!!document.querySelector('.v2e-continue')"):s.js("document.querySelector('.v2e-continue').click()")
  s.js("document.querySelector('.v2-tab[data-screen=people]').click()");s.wait_idle();snap(s,f'people-{w}')
  r['people']=metrics(s)
  if s.js("!!document.querySelector('#familyList .chip')"):
   s.js("document.querySelector('#familyList .chip').click()");s.wait_idle();snap(s,f'person-{w}');r['person']=metrics(s)
  r['routes']={}
  for route in ['Character editor','Family tree','Dating & weddings','All lives']:
   s.call('Page.navigate',{'url':'http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid)})
   s.wait_js("!!document.querySelector('.v2-agebtn')");s.wait_idle()
   s.js("document.querySelector('button[aria-label=\"More routes\"]').click()")
   s.js("[...document.querySelectorAll('.v2-more button')].find(b=>b.textContent.includes("+json.dumps(route)+")).click()")
   s.wait_idle();snap(s,'route-'+route.lower().replace(' ','-')+'-'+str(w));r['routes'][route]=metrics(s)
  r['errors']={'console':s.console_errors,'exceptions':s.exceptions,'requests':s.failed_requests};results[str(w)]=r
except Exception as e:
 results['error']=repr(e)
finally:
 if c:c.terminate()
 h.shutdown();h.server_close();shutil.rmtree(tmp,ignore_errors=True)
(out/'results.json').write_text(json.dumps(results,indent=2));print(json.dumps(results,indent=2))
