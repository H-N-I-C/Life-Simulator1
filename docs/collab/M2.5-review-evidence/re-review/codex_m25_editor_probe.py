import os,sys,json
sys.path.insert(0,os.getcwd())
from tests.test_game_m25_codex_fixes import CodexFixes
x=CodexFixes();e=x.new()
try:
 e['graph']['relationships']=[];e['graph']['unions']=[];x.svc._save(e)
 st=x.command('dating_app');before=x.svc._load(x.gid)['pending'];tid=before['bindings']['target']
 res=x.svc.editor_apply(x.gid,st['revision'],'modifier-change',[{'name':'edit_person_attributes','scope':'person','target_id':tid,'patch':{'openness':0.99}}])
 after=x.svc._load(x.gid)['pending']
 print(json.dumps({'before':{'id':before['id'],'sampled':before.get('sampled')},'after':{'id':after['id'],'sampled':after.get('sampled')} if after else None,'policy_revision':res['policy_revision']},indent=2))
finally:x.doCleanups()
