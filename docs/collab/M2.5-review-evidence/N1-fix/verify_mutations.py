from pathlib import Path
import json,os,shutil,subprocess,tempfile
root=Path.cwd();clone=Path(tempfile.mkdtemp(prefix='n1-proof-',dir=root/'.review-tmp'))
for name in ('src','web','tests','tools','assets'):
 shutil.copytree(root/name,clone/name,ignore=shutil.ignore_patterns('__pycache__','*.pyc'))
for relative in ('docs/collab/M2.5/contracts','docs/collab/M2'):
 shutil.copytree(root/relative,clone/relative,ignore=shutil.ignore_patterns('*.png','*.log','evidence'))
env=dict(os.environ,TMPDIR=str(root/'.review-tmp/m25'))
r=subprocess.run(['python3','-m','unittest','tests.test_game_m25_n1'],cwd=clone,env=env,capture_output=True,text=True,timeout=60)
print(json.dumps({'baseline_exit':r.returncode,'output':r.stderr[-1300:]}),flush=True)
assert r.returncode==0,'snapshot must pass before mutation'
cases=[('male-carrier-not-recorded','src/game/service.py',
'''            _pregnancy_continues = (
                getattr(env["player"], "is_pregnant", False)
                or getattr(getattr(env["player"], "partner", None),
                           "is_pregnant", False))''',
'''            _pregnancy_continues = getattr(env["player"], "is_pregnant", False)'''),
('female-roles-reversed','src/game/features/parenting/core.py',
'''    if getattr(player, "gender", None) == Gender.Female:''',
'''    if getattr(player, "gender", None) == "female":''')]
fail=[]
for label,path,old,new in cases:
 p=clone/path;s=p.read_text();assert s.count(old)==1,(label,s.count(old));p.write_text(s.replace(old,new))
 try:
  r=subprocess.run(['python3','-m','unittest','tests.test_game_m25_n1.LegacyConceptionRolesTests.test_real_keep_replay_reload_and_birth_preserve_ordered_roles'],cwd=clone,env=env,capture_output=True,text=True,timeout=60)
  expected=r.returncode!=0 and 'FAIL:' in r.stderr and 'Ran 1 test' in r.stderr
  print(json.dumps({'case':label,'expected_failure':expected,'output':r.stderr[-2200:]}),flush=True)
  if not expected:fail.append(label)
 finally:p.write_text(s)
print(json.dumps({'cases':len(cases),'unexpected_results':fail}),flush=True)
shutil.rmtree(clone)
raise SystemExit(bool(fail))
