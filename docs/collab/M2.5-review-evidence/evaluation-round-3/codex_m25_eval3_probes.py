import os,sys,json,random,copy
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixture
from src.game.service import GameError
from src.game.events import EventEngine,GameContext
from src.game.features.knowledge import engine as drama_engine
from src.game.features.parenting import engine as parenting_engine
from src.game import events as event_mod
out={}
for ages in [(17,18),(18,17),(18,18)]:
 t=fixture.CodexFixes();env=t.new(*ages)
 try:
  env['pending']={'id':'d_old_pregnancy','kind':'pregnancy','title':'Baby','text':'Pregnancy',
   'options':[{'id':'keep','label':'Keep'},{'id':'abort','label':'Abort'}],
   'sampled':{'overrule_u':100,'overrule_chance':{'keep':0,'abort':0}}}
  t.svc._save(env)
  try:
   t.command('resolve_decision',{'decision_id':'d_old_pregnancy','option_id':'keep'})
   e=t.svc._load(t.gid);out['pregnancy_resolve_%s_%s'%ages]={'accepted':True,'pregnant':e['player'].is_pregnant}
  except GameError as x:out['pregnancy_resolve_%s_%s'%ages]={'accepted':False,'error':x.code}
 finally:t.doCleanups()
t=fixture.CodexFixes();env=t.new()
try:
 t.command('try_for_baby');p=t.svc._load(t.gid)['pending']
 t.command('resolve_decision',{'decision_id':p['id'],'option_id':'keep'})
 env=t.svc._load(t.gid)
 out['ordinary_pregnancy']={'pregnant':env['player'].is_pregnant,'conception_outcomes':env['graph']['conception_outcomes']}
 t.svc.record_conception(t.gid,'future-A',t.pid,t.tid,'record-A')
 t.svc.record_conception(t.gid,'future-B',t.pid,t.tid,'record-B')
 consumed=t.svc.consume_conception_outcome(t.gid,'future-B')
 env=t.svc._load(t.gid)
 out['child_specific_consumption']={'requested_child':'future-B','returned':consumed,'stored':env['graph']['conception_outcomes']}
 second=t.svc.consume_conception_outcome(t.gid,'future-B')
 out['same_child_second_consumption']=second
 # Only the injected dispatch input differs: the production phase and
 # real persisted yearly cursor still run. A false condition must exclude it.
 event=t.event(interaction={'kind':'parenting_request','romantic':False})
 event.update({'id':'dra_review_impossible','schema_version':3,'conditions':{'min_age':99}})
 env['pending']=None;env['turn']={'turn_id':'review-turn','cursor':{'phase_id':'drama_events','item_cursor':0},'done':{}}
 t.svc._save(env)
 old=drama_engine._EVENT_CACHE;drama_engine._EVENT_CACHE=[event]
 try:
  st=t.command('age_up');out['drama_false_condition']={'age':next(p['age'] for p in st['people'] if p['id']==t.pid),'pending_event':(st.get('pending_decision') or {}).get('event_id')}
 except Exception as x:out['drama_false_condition']={'error':type(x).__name__,'message':str(x)}
 finally:drama_engine._EVENT_CACHE=old
 env=t.svc._load(t.gid);env['pending']=None
 env['turn']={'turn_id':'review-parenting','cursor':{'phase_id':'parenting_events','item_cursor':0},'done':{}}
 t.svc._save(env)
 event=dict(event,id='par_review_impossible')
 old=parenting_engine._EVENT_CACHE;parenting_engine._EVENT_CACHE=[event]
 try:
  st=t.command('age_up');out['parenting_false_condition']={'age':next(p['age'] for p in st['people'] if p['id']==t.pid),'pending_event':(st.get('pending_decision') or {}).get('event_id')}
 finally:parenting_engine._EVENT_CACHE=old
finally:t.doCleanups()
print(json.dumps(out,indent=2,default=str))
