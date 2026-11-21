import os,sys,threading,tempfile,shutil,json,base64
from pathlib import Path
sys.path.insert(0,os.getcwd())
Path('.review-tmp/codex-feel-tmp').mkdir(parents=True, exist_ok=True)
from web import server
from tools.cdp_client import launch_chrome
from src.game.finance import Ledger
out=Path('docs/collab/evidence/redesign-v2/feel/codex-final-review');out.mkdir(parents=True,exist_ok=True)
tmp=tempfile.mkdtemp(dir='.review-tmp/codex-feel-tmp');h=server.make_server(0,save_dir=tmp);threading.Thread(target=h.serve_forever,daemon=True).start();results={};proc=None
def snap(s,n):
 (out/(n+'.png')).write_bytes(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
def fixture(w,credit=1000,long=False):
 st=h.service.new_game(dict(first_name='Review',last_name='Cost',gender='female',country='Australia',seed=7),'cost-'+str(w)+'-'+str(credit));gid=st['game_id'];env=h.service._load(gid);p=env['player'];mom=next(iter(p.parents.values()));mom.relationship=50
 if credit:
  Ledger(env,resolver=h.service._make_resolver(env)).post('t_fixture',p.age,{'phase':'review','occurrence_id':'fixture-credit','account_owner_id':p.person_id,'category':'other','amount':credit,'description':'Review fixture funding'})
 if long:
  p.firstname='Alexandria'*8;p.lastname='LongName'*6;mom.firstname='VeryLongMotherName'*5;mom.lastname='Family'*6
 env['pending']={'id':'d_fixture','title':'A real fee' if not long else 'AnUnbrokenEventTitle'*9,'text':'Talk to your mother and pay the fee.' if not long else ('LongUnbrokenNarrative'*8+' ')*7,'person_ids':[p.person_id,mom.person_id],'options':[
 {'id':'pay','label':'Pay the fee','known_effects':['-happiness (25-35)'],'_effects':{'money':-120,'smarts':2,'relationship':-4},'_outcome':'I paid the fee and talked to my mother.'},
 {'id':'free','label':'Walk away','known_effects':[],'_effects':{},'_outcome':'I walked away.'}]}
 if long:
  env['pending']['options'] += [{'id':'free'+str(i),'label':'AnotherLongChoiceWithoutSpaces'*4,'known_effects':[],'_effects':{},'_outcome':'I walked away.'} for i in range(8)]
 h.service._save(env);return gid
try:
 proc,conn=launch_chrome()
 import collections
 threading.Thread(target=lambda: collections.deque(proc.stderr,maxlen=100),daemon=True).start()
 for w in [390,320]:
  gid=fixture(w);s=conn.new_session();s.call('Emulation.setDeviceMetricsOverride',{'width':w,'height':844,'deviceScaleFactor':1,'mobile':True});s.call('Page.navigate',{'url':'http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid)});s.wait_js("!!document.querySelector('.v2e-option')");s.wait_idle(timeout=30)
  text=s.js("document.querySelector('.v2e-card').innerText");assert 'Costs $120' in text,text;assert '25-35' not in text
  assert s.js("!!document.querySelector('.v2e-avatar')")
  snap(s,f'paid-choice-{w}');before=h.service.get_state(gid)
  s.js("document.querySelector('.v2e-option').click()");s.wait_js("!!document.querySelector('.v2e-outcome')");s.wait_idle(timeout=30);snap(s,f'paid-outcome-{w}');after=h.service.get_state(gid);o=after['last_decision_outcome'];assert before['money']-after['money']==120
  text=s.js("document.querySelector('.v2e-outcome').innerText");assert '-$120' in text and '+2' in text and '-4' in text,text
  assert s.js("document.querySelector('.v2-agebtn').disabled")
  s.js("document.querySelector('.v2e-continue').click()");s.call('Page.reload');s.wait_idle(timeout=30);assert s.js("document.getElementById('modal').hidden")
  results[str(w)]={'paidCostShown':True,'outcome':o,'actualMoneyDelta':after['money']-before['money'],'reloadDoesNotReplay':True,'errors':s.console_errors+s.exceptions}
 gid=fixture('zero',0);s=conn.new_session('http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid));s.call('Emulation.setDeviceMetricsOverride',{'width':320,'height':568,'deviceScaleFactor':1,'mobile':True});s.wait_js("!!document.querySelector('.v2e-option')");s.wait_idle(timeout=30);assert s.js("document.querySelector('.v2e-option').disabled");assert s.js("!document.querySelectorAll('.v2e-option')[1].disabled");snap(s,'unaffordable-choice-320');results['unaffordable']='paid disabled; free enabled'
 gid=fixture('long',1234567890123456,True);s=conn.new_session('http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid));s.call('Emulation.setDeviceMetricsOverride',{'width':320,'height':420,'deviceScaleFactor':1,'mobile':True});s.wait_js("!!document.querySelector('.v2e-option')");s.wait_idle(timeout=30);snap(s,'long-short-event-320');results['longShort']=s.js("({overflow:document.documentElement.scrollWidth>innerWidth,cardWidth:document.querySelector('.v2e-card').getBoundingClientRect().width,cardHeight:document.querySelector('.v2e-card').getBoundingClientRect().height,cardScrollHeight:document.querySelector('.v2e-card').scrollHeight})");s.js("document.querySelector('.v2e-later').scrollIntoView({block:'nearest'})");snap(s,'long-short-event-bottom-320');s.js("document.querySelector('.v2e-later').click()");assert s.js("document.getElementById('modal').hidden && getComputedStyle(document.getElementById('modal')).display==='none'");snap(s,'huge-money-header-320');results['longHeader']=s.js("({overflow:document.documentElement.scrollWidth>innerWidth,width:document.documentElement.scrollWidth})")
except Exception as e:results['error']=repr(e)
finally:
 if proc:proc.terminate()
 h.shutdown();h.server_close();shutil.rmtree(tmp,ignore_errors=True)
(out/'edge-results.json').write_text(json.dumps(results,indent=2));print(json.dumps(results,indent=2))
