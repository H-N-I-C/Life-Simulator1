import os,sys,json,random,tempfile,threading,urllib.request,urllib.error
from pathlib import Path
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixtures
from src.people.classes.parent import Parent
from web import server
seed=int(sys.argv[1]) if len(sys.argv)>1 else 1
f=fixtures.CodexFixes();env=f.new();h=None
try:
 parents=[p for p in env['player'].relations if isinstance(p,Parent)]
 for i,p in enumerate(parents):p.age=130;p.health=100;p.happiness=100;p.money=60+10*i;p.generosity=100
 env['rng_state']=random.Random(seed).getstate();env['cosmetic_rng_state']=random.Random(0).getstate()
 env['turn']={'turn_id':'t_two_parent_deaths','cursor':{'phase_id':'mortality','item_cursor':0},'done':{}}
 f.svc._save(env);state=f.svc.get_state(f.gid);before=Path(f.svc._path(f.gid)).read_bytes()
 h=server.make_server(0,save_dir=f.tmp.name);h.service=f.svc;threading.Thread(target=h.serve_forever,daemon=True).start()
 req=urllib.request.Request('http://127.0.0.1:%s/api/games/%s/actions'%(h.server_port,f.gid),data=json.dumps({'expected_revision':state['revision'],'request_id':'double-inheritance','action':'age_up','payload':{}}).encode(),headers={'Content-Type':'application/json'})
 try:
  with urllib.request.urlopen(req) as response:status=response.status;body=json.load(response)
 except urllib.error.HTTPError as error:
  with error:status=error.code;body=json.load(error)
 result={'seed':seed,'http_status':status,'body':body if status>=400 else {'pending':(body.get('pending_decision') or {}).get('kind')},'parents':[p.person_id for p in parents],'unchanged_on_error':Path(f.svc._path(f.gid)).read_bytes()==before}
 print(json.dumps(result,indent=2))
finally:
 if h:h.shutdown();h.server_close()
 f.doCleanups()
