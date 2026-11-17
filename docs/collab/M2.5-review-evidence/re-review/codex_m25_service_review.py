import os,sys,json,random
sys.path.insert(0,os.getcwd())
from tests.test_game_m25_codex_fixes import CodexFixes
from src.game.features.family_graph.service_view import ServiceGraphView,ServicePolicyView
from src.game.features.relationship_policy.evaluator import evaluate_offer
from src.game.features.family_graph.predicates import _non_stranger
from src.game.events import GameContext,EventEngine
from src.menus import rule_actions
out={}
t=CodexFixes();e=t.new()
try:
 try:t.svc.wedding_command(t.gid,t.state['revision'],'bad-input','plan_wedding',{})
 except Exception as x:out['missing_wedding_args']={'type':type(x).__name__,'message':str(x)}
 e=t.svc._load(t.gid);e['graph']['relationships']=[];e['graph']['unions']=[];t.svc._save(e)
 ev=t.event({'chemistry':{'between':['actor','target'],'delta':5}},interaction={'kind':'date','romantic':True})
 p=t.install(ev,'d_unsampled');out['chemistry_sampled']=p.get('sampled');t.resolve(p)
 out['chemistry_without_consent_relationships']=t.svc._load(t.gid)['graph']['relationships']
 e=t.svc._load(t.gid)
 e['pending']={'id':'d_policy','kind':'m25_dating_app_date','bindings':{'actor':t.pid,'target':t.tid},'sampled':{'accepted':False},'options':[]}
 t.svc._save(e);st=t.svc.get_state(t.gid)
 try:
  res=t.svc.editor_apply(t.gid,st['revision'],'editor-modifier',[{'name':'edit_person_attributes','scope':'person','target_id':t.tid,'patch':{'openness':0.99}}])
  out['modifier_edit']={'result':res,'pending':t.svc._load(t.gid)['pending']}
 except Exception as x:out['modifier_edit_error']=str(x)
 e=t.svc._load(t.gid);e['pending']=None
 a=t.svc.record_conception(t.gid,'future-child',t.pid,t.tid,'conception-1')
 b=t.svc.record_conception(t.gid,'future-child',t.tid,t.pid,'conception-2')
 out['same_child_changed_request']={'first':a,'second':b,'keys':list(t.svc._load(t.gid)['graph']['conception_outcomes'])}
finally:t.doCleanups()
print(json.dumps(out,indent=2,default=str))
