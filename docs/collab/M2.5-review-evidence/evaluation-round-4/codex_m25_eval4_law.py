import os,sys,json,random
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixtures
from src.game.features.family_graph.service_view import ServiceGraphView,ServicePolicyView
from src.game.features.relationship_policy.evaluator import evaluate_offer
f=fixtures.CodexFixes();e=f.new()
try:
 e['graph']['relationships']=[];e['graph']['unions']=[];e['player'].partner=None;f.svc._save(e)
 jur=e['policy']['jurisdiction_id'];st=f.svc.get_state(f.gid)
 res=f.svc.editor_apply(f.gid,st['revision'],'law-edit',[{'name':'edit_jurisdiction','scope':'jurisdiction','target_id':jur,'patch':{'recognition':{'monogamous_civil':'forbidden','plural_civil':'forbidden'}}}])
 e=f.svc._load(f.gid);expected=evaluate_offer('propose_union',f.pid,f.tid,ServiceGraphView(e),ServicePolicyView(e))
 e['rng_state']=random.Random(139).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();f.svc._save(e)
 st=f.command('union_proposal',{'target_id':f.tid});e=f.svc._load(f.gid);p=e['pending'];stored=p.get('sampled')
 opt=next(o for o in p['options'] if o['id'].endswith('_ask'));f.command('resolve_decision',{'decision_id':p['id'],'option_id':opt['id']})
 e=f.svc._load(f.gid)
 print(json.dumps({'prior_courtship_count':0,'evaluator_recognition':expected['projected_recognition'],'evaluator_consequences':expected['projected_consequences'],'stored_sample':stored,'result_unions':e['graph']['unions']},indent=2))
finally:f.doCleanups()
