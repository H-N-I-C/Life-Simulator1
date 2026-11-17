import os,sys,tempfile,threading,json,random,base64,urllib.request,urllib.error
sys.path.insert(0,os.getcwd())
from web import server
from tools.cdp_client import launch_chrome
from tools.browser_check import Runner
out='docs/collab/M2.5-review-evidence/evaluation-round-2';os.makedirs(out,exist_ok=True)
h=server.make_server(0,save_dir=tempfile.mkdtemp(prefix='m25-eval-ui-'));h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start();c=None
result={}
try:
 c,conn=launch_chrome();s=conn.new_session();base='http://127.0.0.1:%s'%h.server_port
 r=Runner(base,s,out,service=h.service);r._hook_phone_viewport('own');r._hook_scenario_start('own',scenario='married_30')
 gid=r.ctx['game_id'];env=h.service._load(gid)
 result['scenario_marriage']={'family':h.service.get_state(gid)['family'], 'graph_unions':env['graph']['unions']}
 env['rng_state']=random.Random(139).getstate();env['cosmetic_rng_state']=random.Random(0).getstate();h.service._save(env)
 s.call('Page.navigate',{'url':base+'/game'});s.wait_js('location.origin === '+json.dumps(base))
 r._hook_load_game_in_page('own')
 s.js("document.getElementById('m25Toggle').click()")
 s.wait_js("!!document.querySelector('#m25Body button.primary')")
 s.js("document.getElementById('m25Card').scrollIntoView()")
 s.js("document.querySelector('#m25Body button.primary').click()")
 s.wait_js("!!document.getElementById('modal') && !document.getElementById('modal').hidden")
 p=h.service._load(gid)['pending'];result['date_response']=p.get('sampled');result['date_target']=p.get('bindings',{}).get('target')
 result['modal_buttons']=s.js("Array.from(document.querySelectorAll('#modal button')).map(b=>b.textContent)")
 s.js("Array.from(document.querySelectorAll('#modal button')).find(b=>b.textContent.trim()==='Go on the date').click()")
 s.wait_js("document.getElementById('modal').hidden")
 env=h.service._load(gid);state=h.service.get_state(gid)
 result['after_date']={'relationships':env['graph']['relationships'],'unions':env['graph']['unions'],
  'proposal_actions':[a for a in state['available_actions'] if a['id'] in ('courtship_offer','union_proposal')]}
 proposal=urllib.request.Request(base+'/api/games/'+gid+'/actions',data=json.dumps({'expected_revision':state['revision'],'request_id':'review-proposal','action':'union_proposal','payload':{'target_id':result['date_target']}}).encode(),headers={'Content-Type':'application/json'})
 try:
  with urllib.request.urlopen(proposal) as response:result['proposal_http']={'status':response.status,'body':json.load(response)}
 except urllib.error.HTTPError as error:
  with error:result['proposal_http']={'status':error.code,'body':json.load(error)}
 s.js("if(!document.getElementById('m25Card').hidden)document.getElementById('m25Toggle').click();document.getElementById('m25Toggle').click()")
 s.wait_js("!!document.getElementById('m25Body') && document.getElementById('m25Body').innerText.toLowerCase().includes('courtship')")
 for tab in ('dating','weddings','reputation'):
  s.js("document.querySelector('[data-tab=%s]').click();document.getElementById('m25Card').scrollIntoView()"%tab)
  result[tab]={'text':s.js("document.getElementById('m25Body').innerText"),'buttons':s.js("Array.from(document.querySelectorAll('#m25Body button')).map(b=>({text:b.innerText,disabled:b.disabled}))"),'overflow':s.js('document.documentElement.scrollWidth>innerWidth')}
  open(out+'/own-'+tab+'.png','wb').write(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
except Exception as e:
 result['error']={'type':type(e).__name__,'message':str(e)}
 if c:open(out+'/own-error.png','wb').write(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
finally:
 if c:c.terminate()
 h.shutdown();h.server_close()
print('OWN_UI_RESULT',json.dumps(result,indent=2,default=str))
open(out+'/own-ui-result.json','w').write(json.dumps(result,indent=2,default=str))
