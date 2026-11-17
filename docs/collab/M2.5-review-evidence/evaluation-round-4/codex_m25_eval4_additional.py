import os,sys,json,random,tempfile
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixtures
from src.game.service import GameService
from src.lifesim_lib.lifesim_lib import Gender
out={}
for male in (False,True):
 f=fixtures.CodexFixes();e=f.new()
 try:
  if male:e['player'].gender=Gender.Male;e['player'].partner.gender=Gender.Female
  e['player'].fertility=100;e['player'].partner.fertility=100
  e['rng_state']=random.Random(1).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();f.svc._save(e)
  st=f.command('try_for_baby');p=st['pending_decision']
  f.command('resolve_decision',{'decision_id':p['id'],'option_id':'keep'})
  e=f.svc._load(f.gid)
  out['ordinary_pregnancy_'+('male' if male else 'female')]={'player_id':f.pid,'partner_id':f.tid,'player_gender':str(e['player'].gender),'player_pregnant':e['player'].is_pregnant,'partner_pregnant':e['player'].partner.is_pregnant,'outcomes':e['graph']['conception_outcomes']}
 finally:f.doCleanups()
with tempfile.TemporaryDirectory(prefix='eval3-marriage-') as root:
 s=GameService(root);st=s.new_game({'first_name':'Dev','last_name':'Tester','gender':'female','country':'Australia','seed':777,'scenario':'married_30'},'married')
 gid=st['game_id'];e=s._load(gid);u=next(u for u in e['graph']['unions'] if u['status']=='active')
 out['marriage_before']={'family':st['family'],'union':u}
 s.wedding_command(gid,st['revision'],'graph-divorce','divorce_union',{'union_id':u['union_id']})
 s=GameService(root);st=s.get_state(gid);e=s._load(gid)
 out['after_actual_divorce']={'family':st['family'],'unions':e['graph']['unions'],'relationships':e['graph']['relationships'],'legacy_divorce_actions':[a for a in st['available_actions'] if a['id']=='divorce']}
print(json.dumps(out,indent=2,default=str))
