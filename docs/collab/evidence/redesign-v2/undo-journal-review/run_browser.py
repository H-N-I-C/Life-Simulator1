import sys,os,json,base64,tempfile,threading,shutil,signal,collections
from pathlib import Path
ROOT=Path.cwd();sys.path.insert(0,str(ROOT))
from web import server
from tools.cdp_client import launch_chrome
out=ROOT/'docs/collab/evidence/redesign-v2/undo-journal-review';out.mkdir(parents=True,exist_ok=True)
base_tmp=ROOT/'.review-tmp/codex-feel-tmp';base_tmp.mkdir(parents=True,exist_ok=True)
run=Path(tempfile.mkdtemp(prefix='undo-review-',dir=base_tmp));profile=run/'chrome';profile.mkdir();h=server.make_server(0,save_dir=str(run/'saves'));h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start();proc=None;results={}
def state(gid):return h.service.get_state(gid)
def age(st):return next(p['age'] for p in st['people'] if p['id']==st['player_id'])
def cmd(gid,action,payload=None):
 st=state(gid);return h.service.command(gid,st['revision'],'review-%s-%s'%(action,st['revision']),action,payload or {})
def snap(s,name):
 (out/(name+'.png')).write_bytes(base64.b64decode(s.call('Page.captureScreenshot',{'format':'png'})['data']))
def geometry(s):
 return s.js("""(()=>{let sc=document.querySelector('.v2-scroll'),dock=document.querySelector('.v2-dock'),last=document.querySelector('#journalList li:last-child');return {overflow:document.documentElement.scrollWidth>innerWidth,scroller:sc&&{top:sc.scrollTop,height:sc.clientHeight,full:sc.scrollHeight},dock:dock&&dock.getBoundingClientRect().toJSON(),last:last&&last.getBoundingClientRect().toJSON(),undoText:document.querySelector('.v2-undo')?.textContent}})()""")
def openlife(conn,w,gid):
 s=conn.new_session();s.call('Emulation.setDeviceMetricsOverride',{'width':w,'height':844,'deviceScaleFactor':1,'mobile':True});s.call('Page.navigate',{'url':'http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid)});s.wait_js("!!document.querySelector('.v2-undo') || !document.getElementById('legacyView').hidden");s.wait_idle();return s
try:
 proc,conn=launch_chrome(str(profile));threading.Thread(target=lambda:collections.deque(proc.stderr,maxlen=100),daemon=True).start()
 for w in [390,320]:
  st=h.service.new_game(dict(first_name='Undo',last_name='Review',gender='female',country='Australia',seed=7),'browser-'+str(w));gid=st['game_id'];s=openlife(conn,w,gid);r={};snap(s,f'fresh-{w}');r['fresh']=geometry(s)
  s.js("document.querySelector('.v2-agebtn').click()");s.wait_idle();first=state(gid);snap(s,f'aged-{w}');r['firstYearLines']=[e['text'] for e in first['journal'] if e['age']==1]
  s.js("document.querySelector('.v2-undo').click()");r['firstTapMutated']=state(gid)['revision']!=first['revision'];snap(s,f'confirm-{w}');r['confirm']=geometry(s)
  s.js("document.querySelector('.v2-undo').click()");s.wait_idle();r['undoAge']=age(state(gid));r['undoRevision']=state(gid)['revision'];snap(s,f'undone-{w}')
  s.js("document.querySelector('.v2-agebtn').click()");s.wait_idle();r['deterministicRedo']=[e['text'] for e in state(gid)['journal'] if e['age']==1]==r['firstYearLines']
  s.js("document.querySelector('.v2-undo').click()");s.js("document.querySelector('.v2-agebtn').click()");s.wait_idle();r['confirmAfterNewYear']=geometry(s);snap(s,f'stale-confirm-{w}');before=state(gid)['revision'];s.js("document.querySelector('.v2-undo').click()");s.wait_idle();r['singleTapAfterStateChangeMutated']=state(gid)['revision']!=before
  # Reopen fresh to clear a confirmation; reach the first yearly choice.
  s.call('Page.reload');s.wait_idle()
  for _ in range(12):
   if state(gid)['pending_decision']:break
   s.js("document.querySelector('.v2-agebtn').click()");s.wait_idle()
  pending=state(gid)['pending_decision'];r['pendingOffered']=bool(pending)
  if pending:
   snap(s,f'pending-{w}');s.js("document.querySelector('.v2e-later').click()");snap(s,f'collapsed-pending-{w}');r['pendingLayout']=geometry(s);pa=age(state(gid));s.js("document.querySelector('.v2-undo').click();document.querySelector('.v2-undo').click()");s.wait_idle();r['pendingUndo']={'ageBefore':pa,'ageAfter':age(state(gid)),'pendingAfter':state(gid)['pending_decision'],'modalHidden':s.js("document.getElementById('modal').hidden")};snap(s,f'pending-undone-{w}')
  # Full-page death view, following a real age snapshot + surrender path.
  dead=h.service.new_game(dict(first_name='Death',last_name='Review',gender='female',country='Australia',seed=42),'death-'+str(w));dg=dead['game_id'];cmd(dg,'age_up');cmd(dg,'surrender');d=state(dg)['pending_decision'];cmd(dg,'resolve_decision',{'decision_id':d['id'],'option_id':'ask_final'});d=state(dg)['pending_decision'];dead=cmd(dg,'resolve_decision',{'decision_id':d['id'],'option_id':'surrender'});ds=openlife(conn,w,dg);snap(ds,f'death-{w}');r['death']={'status':dead['status'],'canUndo':dead['can_undo'],'visibleUndo':ds.js("[...document.querySelectorAll('button')].filter(b=>b.checkVisibility()&&/Undo last year/i.test(b.textContent)).length"),'body':ds.js('document.body.innerText')}
  r['errors']={'console':s.console_errors+ds.console_errors,'exceptions':s.exceptions+ds.exceptions,'requests':s.failed_requests+ds.failed_requests};results[str(w)]=r
except Exception as e:results['error']=repr(e)
finally:
 if proc:
  try:os.killpg(proc.pid,signal.SIGTERM);proc.wait(timeout=5)
  except Exception:
   try:os.killpg(proc.pid,signal.SIGKILL);proc.wait(timeout=5)
   except Exception:pass
 h.shutdown();h.server_close();shutil.rmtree(run,ignore_errors=True)
(out/'browser-results.json').write_text(json.dumps(results,indent=2)+'\n');print(json.dumps(results,indent=2))
