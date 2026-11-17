import os,sys,json,random,copy
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixtures
f=fixtures.CodexFixes();e=f.new()
try:
 def keep(tag):
  e=f.svc._load(f.gid);e['player'].fertility=100;e['player'].partner.fertility=100
  e['rng_state']=random.Random(1).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();f.svc._save(e)
  st=f.command('try_for_baby',req=tag);p=st['pending_decision']
  f.command('resolve_decision',{'decision_id':p['id'],'option_id':'keep'},req=tag+'-keep');return p['id']
 first=keep('first');e=f.svc._load(f.gid);original=copy.deepcopy(e['graph']['conception_outcomes'])
 e['player'].fertility=0;e['rng_state']=random.Random(2).getstate()
 e['turn']={'turn_id':'t_miscarriage','cursor':{'phase_id':'family_events','item_cursor':0},'done':{}}
 f.svc._save(e);st=f.command('age_up',req='miscarry')
 for i in range(30):
  if not st['pending_decision']:break
  p=st['pending_decision'];o=next(o for o in reversed(p['options']) if o['enabled'])
  st=f.command('resolve_decision',{'decision_id':p['id'],'option_id':o['id']},req='drain'+str(i))
 e=f.svc._load(f.gid);ended={'pregnant':e['player'].is_pregnant,'children':len(e['player'].children),'journal':[j['text'] for j in e['journal'] if 'miscarriage' in j['text']],'outcomes':copy.deepcopy(e['graph']['conception_outcomes'])}
 second=keep('second');e=f.svc._load(f.gid)
 print(json.dumps({'first_pending_id':first,'second_pending_id':second,'after_loss':ended,'after_second_keep':e['graph']['conception_outcomes'],'same_record_reused':e['graph']['conception_outcomes']==original},indent=2))
finally:f.doCleanups()
