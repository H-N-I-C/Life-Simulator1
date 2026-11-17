"""Real service courtship -> forbidden union -> ceremony, fixed legal fine."""
import os,sys,json,random
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixtures
f=fixtures.CodexFixes();e=f.new()
try:
 e['graph']['relationships']=[];e['graph']['unions']=[];e['player'].partner=None;f.svc._save(e)
 jur=e['policy']['jurisdiction_id'];st=f.svc.get_state(f.gid)
 f.svc.editor_apply(f.gid,st['revision'],'law-edit',[{'name':'edit_jurisdiction','scope':'jurisdiction','target_id':jur,'patch':{'recognition':{'monogamous_civil':'forbidden','plural_civil':'forbidden'},'consequences':[{'trigger':'ceremony_of_unrecognized_union','effect':'civil_fine','ledger_category':'legal','amount':{'randint':[500,2000]},'max_per_occurrence_key':'union_id'},{'trigger':'ceremony_of_unrecognized_union','effect':'civil_fine','ledger_category':'legal','amount':{'randint':[700,700]},'max_per_occurrence_key':'union_id'}]}}])
 samples=[]
 for action in ['courtship_offer','union_proposal']:
  e=f.svc._load(f.gid);e['rng_state']=random.Random(139).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();f.svc._save(e)
  st=f.command(action,{'target_id':f.tid});p=st['pending_decision'];samples.append(p['sampled'])
  ask=next(o for o in p['options'] if o['id'].endswith('_ask'))
  f.command('resolve_decision',{'decision_id':p['id'],'option_id':ask['id']})
 e=f.svc._load(f.gid);u=e['graph']['unions'][-1];before=e['player'].money;ledger_before=json.dumps(e['ledger'],sort_keys=True)
 from web import server
 from tools.cdp_client import launch_chrome
 from tools.browser_check import Runner
 import tempfile,threading,base64
 from pathlib import Path
 out=Path('docs/collab/M2.5-review-evidence/evaluation-round-7')
 h=server.make_server(0,save_dir=f.tmp.name);h.service=f.svc
 threading.Thread(target=h.serve_forever,daemon=True).start();c=None
 try:
  c,conn=launch_chrome();s=conn.new_session();base='http://127.0.0.1:%d'%h.server_port
  r=Runner(base,s,str(out),service=f.svc);r.ctx['state']=f.svc.get_state(f.gid);r._hook_phone_viewport('quote')
  s.call('Page.navigate',{'url':base+'/game'});s.wait_js('location.origin === '+json.dumps(base));r._hook_load_game_in_page('quote')
  s.js("document.getElementById('m25Toggle').click()")
  s.wait_js("!!document.querySelector('#m25Body [data-tab]') || !!document.querySelector('[data-tab=weddings]')")
  s.js("document.querySelector('[data-tab=weddings]').click()")
  s.wait_js("!!document.querySelector('#m25Body [data-picker=Venue]')")
  s.js("document.querySelector('[data-picker=Venue]').value='courthouse';document.querySelector('[data-picker=Outfit]').value='hand_me_down';document.querySelector('[data-picker=Honeymoon]').value='none';document.querySelector('#m25Body input[type=number]').value='0';document.querySelector('[data-picker=Venue]').dispatchEvent(new Event('change',{bubbles:true}))")
  button="Array.from(document.querySelectorAll('#m25Body button')).find(b=>b.textContent.startsWith('Book the wedding'))"
  s.wait_js('!!('+button+') && !('+button+').disabled')
  shown=s.js("document.getElementById('m25Body').innerText")
  quote=f.svc._load(f.gid)['weddings']['quotes'][u['union_id']]
  assert len(quote['lines'])==2,quote
  expected='$'+format(quote['total'],',')
  assert expected in shown,(expected,shown)
  assert all('$'+format(line['amount'],',') in shown for line in quote['lines']),shown
  s.js(button+'.click()');s.wait_js("document.getElementById('m25Body').innerText.includes('Renew the vows')")
  e=f.svc._load(f.gid);after=e['player'].money
  assert before-after==quote['total'],(before,after,quote)
  result={'quote':quote,'shown_text':shown,'actual_charge':before-after,'overflow':s.js('document.documentElement.scrollWidth>innerWidth')}
  assert result['overflow'] is False
  (out/'variable-multiple-ui.json').write_text(json.dumps(result,indent=2))
  (out/'variable-multiple-ui.png').write_bytes(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
  print('PASS',json.dumps(result,indent=2))
 finally:
  if c:c.terminate()
  h.shutdown();h.server_close()
finally:f.doCleanups()
