import os,sys,json,threading,urllib.request
sys.path.insert(0,os.getcwd())
from tests.test_game_m25_codex_fixes import CodexFixes
from src.game.features.knowledge import core as k
from web import server
x=CodexFixes();e=x.new()
h=None
try:
 f=k.mint_fact(e,'secret_union',[x.tid],e['revision'],occurrence_id='private-sentinel')
 k.get_fact(e,f)['reputation_changes']=[{'person_id':x.tid,'scope':'global','delta':-40}]
 k.learn(e,x.tid,f,e['revision'],'witnessed')
 k.reputation_delta(e,x.tid,'global',-40,'private-reputation')
 x.svc._save(e)
 h=server.make_server(0,save_dir=x.tmp.name);threading.Thread(target=h.serve_forever,daemon=True).start()
 with urllib.request.urlopen('http://127.0.0.1:%d/api/games/%s/relationships-view'%(h.server_port,x.gid)) as r:view=json.load(r)
 print(json.dumps({'player_knows_fact':k.knows(e,'player',f),'visible_facts':view['facts'],'visible_reputation':view['reputation']},indent=2))
finally:
 if h:h.shutdown();h.server_close()
 x.doCleanups()
