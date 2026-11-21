import sys,tempfile,json,random,shutil
sys.path.insert(0,'.')
from src.game.service import GameService,GameError
from pathlib import Path
Path('.review-tmp/codex-feel-tmp').mkdir(parents=True,exist_ok=True)
scratch=tempfile.mkdtemp(dir='.review-tmp/codex-feel-tmp');svc=GameService(scratch);out={}
try:
 st=svc.new_game({'first_name':'Undo','last_name':'Review','gender':'female','country':'Australia','seed':7},'new');gid=st['game_id'];initial=st
 age=svc.command(gid,st['revision'],'age','age_up',{});old_random=random.getstate();undo=svc.command(gid,age['revision'],'undo','undo_year',{})
 out['revisions']={'initial':initial['revision'],'aged':age['revision'],'undone':undo['revision']};out['globalRandomChangedByUndo']=old_random!=random.getstate()
 try:
  stale=svc.command(gid,age['revision'],'stale-tab-action','age_up',{});out['postAgeStaleClient']={'accepted':True,'newRevision':stale['revision'],'currentAge':next(p['age'] for p in stale['people'] if p['id']==stale['player_id'])}
 except GameError as e:out['postAgeStaleClient']={'accepted':False,'error':e.code}
 try:
  replay=svc.command(gid,initial['revision'],'age','age_up',{});out['originalAgeRetry']={'returned':True,'revision':replay['revision']}
 except GameError as e:out['originalAgeRetry']={'returned':False,'error':e.code}
finally:shutil.rmtree(scratch,ignore_errors=True)
print(json.dumps(out,indent=2))
