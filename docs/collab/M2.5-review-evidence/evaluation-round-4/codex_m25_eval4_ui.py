import os,sys,tempfile,threading,json,random,base64
sys.path.insert(0,os.getcwd())
from web import server
from tools.cdp_client import launch_chrome
from tools.browser_check import Runner
out='docs/collab/M2.5-review-evidence/evaluation-round-4';os.makedirs(out,exist_ok=True)
h=server.make_server(0,save_dir=tempfile.mkdtemp(prefix='m25-eval3-ui-'));h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start();c=None;result={}
try:
 c,conn=launch_chrome();s=conn.new_session();base='http://127.0.0.1:%s'%h.server_port
 r=Runner(base,s,out,service=h.service);r._hook_phone_viewport('own');r._hook_scenario_start('own',scenario='married_with_child')
 gid=r.ctx['game_id'];state=h.service.get_state(gid);pid=state['family']['partner'];target=next(p for p in state['people'] if p['id']==pid);name=target['name']
 result['initial_family']=state['family'];result['initial_unions']=h.service._load(gid)['graph']['unions']
 s.call('Page.navigate',{'url':base+'/game'});s.wait_js('location.origin === '+json.dumps(base));r._hook_load_game_in_page('own')
 s.js("document.getElementById('m25Toggle').click()");s.wait_js("!!document.getElementById('m25Body') && document.getElementById('m25Body').innerText.length>10")
 def tab(t):s.js("document.querySelector('[data-tab=%s]').click();document.getElementById('m25Card').scrollIntoView()"%t)
 tab('weddings');s.js("Array.from(document.querySelectorAll('#m25Body button')).find(b=>b.textContent.startsWith('Divorce ')).click()")
 s.wait_js("document.getElementById('m25Body').innerText.includes('No active unions')")
 result['after_divorce']=h.service.get_state(gid)['family']
 for verb,label in [('Ask out','Ask them out'),('Propose a union','Propose a union')]:
  tab('dating');e=h.service._load(gid);e['rng_state']=random.Random(139).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();h.service._save(e)
  expr="Array.from(document.querySelectorAll('#m25Body button')).find(b=>b.textContent.trim().startsWith("+json.dumps(verb)+")"+" && b.parentElement.innerText.includes("+json.dumps(name)+"))"
  s.wait_js('!!('+expr+')');s.js(expr+'.click()')
  s.wait_js("!!document.getElementById('modal') && !document.getElementById('modal').hidden")
  p=h.service._load(gid)['pending'];result[verb]={'sampled':p.get('sampled'),'target':p.get('bindings',{}).get('target')}
  s.js("Array.from(document.querySelectorAll('#modal button')).find(b=>b.textContent.trim()==="+json.dumps(label)+").click()")
  s.wait_js("document.getElementById('modal').hidden")
  # Re-open to fetch current graph data after the shell has adopted revision.
  s.js("if(!document.getElementById('m25Card').hidden)document.getElementById('m25Toggle').click();document.getElementById('m25Toggle').click()")
  s.wait_js("!!document.getElementById('m25Body') && document.getElementById('m25Body').innerText.length>10")
 tab('weddings');s.wait_js("!!document.querySelector('#m25Body [data-picker=Venue]')")
 s.js("document.querySelector('[data-picker=Venue]').value='courthouse';document.querySelector('[data-picker=Outfit]').value='hand_me_down';document.querySelector('[data-picker=Honeymoon]').value='none';document.querySelector('[data-picker=Venue]').dispatchEvent(new Event('change',{bubbles:true}))")
 s.js("Array.from(document.querySelectorAll('#m25Body button')).find(b=>b.textContent.startsWith('Book the wedding')).click()")
 s.wait_js("document.getElementById('m25Body').innerText.includes('Renew the vows')")
 e=h.service._load(gid);result['final']={'family':h.service.get_state(gid)['family'],'unions':e['graph']['unions'],'ledger':e['ledger'],'overflow':s.js('document.documentElement.scrollWidth>innerWidth'),'text':s.js("document.getElementById('m25Body').innerText")}
 s.js("document.getElementById('m25Card').scrollIntoView()")
 open(out+'/own-wedding-journey.png','wb').write(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
except Exception as error:
 result['error']={'type':type(error).__name__,'message':str(error)}
 if c:open(out+'/own-error.png','wb').write(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
finally:
 if c:c.terminate()
 h.shutdown();h.server_close()
open(out+'/own-ui-result.json','w').write(json.dumps(result,indent=2,default=str))
print('OWN_UI_RESULT',json.dumps(result,indent=2,default=str))
