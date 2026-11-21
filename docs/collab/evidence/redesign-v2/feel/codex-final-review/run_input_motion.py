import os,sys,tempfile,threading,json,base64,shutil
from pathlib import Path
sys.path.insert(0,os.getcwd())
Path('.review-tmp/codex-feel-tmp').mkdir(parents=True, exist_ok=True)
from web import server
from tools.cdp_client import launch_chrome
from src.people.classes.child import Child
from src.lifesim_lib.lifesim_lib import Gender
out=Path('docs/collab/evidence/redesign-v2/feel/codex-final-review');out.mkdir(parents=True,exist_ok=True)
tmp=tempfile.mkdtemp(dir='.review-tmp/codex-feel-tmp');h=server.make_server(0,save_dir=tmp);threading.Thread(target=h.serve_forever,daemon=True).start();c=None;r={}
def snap(s,n):
 (out/(n+'.png')).write_bytes(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
try:
 st=h.service.new_game(dict(first_name='Review',last_name='Naming',gender='female',country='Australia',seed=42),'input');gid=st['game_id'];env=h.service._load(gid);p=env['player'];p.age=30
 child=Child('Original','Naming',Gender.Male,50,50,adopted=True);p.children.append(child);p.relations.append(child);h.service._ensure_ids(env)
 env['pending']={'id':'d_stored_name','kind':'baby_naming','title':'A baby boy!','text':'What will you name them?','person_ids':[child.person_id],'options':[{'id':'name','label':'Choose a name','inputs':[{'id':'name','type':'text','label':'First name','required':True,'min':1,'max':40}]}],'sampled':{},'sampled_child_id':child.person_id,'sampled_child':child};h.service._save(env)
 c,conn=launch_chrome();s=conn.new_session();s.call('Emulation.setDeviceMetricsOverride',{'width':320,'height':420,'deviceScaleFactor':1,'mobile':True});s.call('Emulation.setEmulatedMedia',{'features':[{'name':'prefers-reduced-motion','value':'reduce'}]});s.call('Page.navigate',{'url':'http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid)});s.wait_js("!!document.querySelector('.v2e-form input')");s.wait_idle();s.js("document.querySelector('.v2e-form input').focus()")
 for ch in 'River':
  s.call('Input.dispatchKeyEvent',{'type':'keyDown','key':ch,'text':ch,'code':'Key'+ch.upper(),'windowsVirtualKeyCode':ord(ch.upper())});s.call('Input.dispatchKeyEvent',{'type':'keyUp','key':ch,'code':'Key'+ch.upper(),'windowsVirtualKeyCode':ord(ch.upper())})
 assert s.js("document.querySelector('.v2e-form input').value")=='River';r['trustedInput']='River';snap(s,'naming-input-320x420')
 s.key('Enter',key_code=13);assert s.js("document.querySelector('.v2e-form input').value")=='River';r['enterDoesNotReload']=True
 s.js("document.querySelector('.v2e-option').click()");s.wait_js("!!document.querySelector('.v2e-outcome')");s.wait_idle();snap(s,'naming-outcome-320x420');actual=h.service.get_state(gid);assert next(x['name'] for x in actual['people'] if x['id']==child.person_id)=='River Naming';r['serverPersistedName']='River Naming';s.js("document.querySelector('.v2e-continue').click()");s.js("document.querySelector('.v2-agebtn').click()");s.wait_idle()
 r['reducedMotion']=s.js("({transition:getComputedStyle(document.querySelector('.v2-stat-fill')).transitionDuration,animations:[...document.querySelectorAll('.v2-stat')].reduce((n,e)=>n+e.getAnimations().length,0),age:document.querySelector('.v2-header-age').textContent,overflow:document.documentElement.scrollWidth>innerWidth})")
 r['errors']={'console':s.console_errors,'exceptions':s.exceptions,'requests':s.failed_requests}
except Exception as e:r['error']=repr(e)
finally:
 if c:c.terminate()
 h.shutdown();h.server_close();shutil.rmtree(tmp,ignore_errors=True)
(out/'input-motion-results.json').write_text(json.dumps(r,indent=2));print(json.dumps(r,indent=2))
