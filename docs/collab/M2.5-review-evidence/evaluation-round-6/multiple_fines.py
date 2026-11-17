"""Real service courtship -> forbidden union -> ceremony, fixed legal fine."""
import os,sys,json,random
sys.path.insert(0,os.getcwd())
from tests import test_game_m25_codex_fixes as fixtures
f=fixtures.CodexFixes();e=f.new()
try:
 e['graph']['relationships']=[];e['graph']['unions']=[];e['player'].partner=None;f.svc._save(e)
 jur=e['policy']['jurisdiction_id'];st=f.svc.get_state(f.gid)
 f.svc.editor_apply(f.gid,st['revision'],'law-edit',[{'name':'edit_jurisdiction','scope':'jurisdiction','target_id':jur,'patch':{'recognition':{'monogamous_civil':'forbidden','plural_civil':'forbidden'},'consequences':[{'trigger':'ceremony_of_unrecognized_union','effect':'civil_fine','ledger_category':'legal','amount':{'randint':[500,500]},'max_per_occurrence_key':'union_id'},{'trigger':'ceremony_of_unrecognized_union','effect':'civil_fine','ledger_category':'legal','amount':{'randint':[700,700]},'max_per_occurrence_key':'union_id'}]}}])
 samples=[]
 for action in ['courtship_offer','union_proposal']:
  e=f.svc._load(f.gid);e['rng_state']=random.Random(139).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();f.svc._save(e)
  st=f.command(action,{'target_id':f.tid});p=st['pending_decision'];samples.append(p['sampled'])
  ask=next(o for o in p['options'] if o['id'].endswith('_ask'))
  f.command('resolve_decision',{'decision_id':p['id'],'option_id':ask['id']})
 e=f.svc._load(f.gid);u=e['graph']['unions'][-1];before=e['player'].money;ledger_before=json.dumps(e['ledger'],sort_keys=True)
 from web.server import _relationships_view
 view=_relationships_view(f.svc,f.gid)
 preview=next(v for v in view['unions'] if v['union_id']==u['union_id']).get('ceremony_fine')
 st=f.svc.get_state(f.gid)
 f.svc.wedding_command(f.gid,st['revision'],'ceremony','plan_wedding',{'union_id':u['union_id'],'venue_id':'courthouse','guests':0,'outfit_id':'hand_me_down','honeymoon_id':'none'})
 e=f.svc._load(f.gid)
 print(json.dumps({'preview':preview,'sampled':samples[-1],'union':e['graph']['unions'][-1],'before_money':before,'preview_total':preview['amount'],'after_money':e['player'].money,'legal_rows':[r for r in e['ledger'] if r['category']=='legal'],'ledger_unchanged':json.dumps(e['ledger'],sort_keys=True)==ledger_before},indent=2))
finally:f.doCleanups()
