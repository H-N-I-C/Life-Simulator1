from pathlib import Path
import json,os,shutil,subprocess,tempfile
root=Path.cwd();clone=Path(tempfile.mkdtemp(prefix='e1-proof-',dir=root/'.review-tmp'))
for name in ('src','web','tests','tools','assets'):
 shutil.copytree(root/name,clone/name,ignore=shutil.ignore_patterns('__pycache__','*.pyc'))
for relative in ('docs/collab/M2.5/contracts','docs/collab/M2'):
 shutil.copytree(root/relative,clone/relative,ignore=shutil.ignore_patterns('*.png','*.log','evidence'))
base=['python3','-m','unittest','tests.test_game_m25_e1']
env=dict(os.environ,TMPDIR=str(root/'.review-tmp/m25'))
r=subprocess.run(base,cwd=clone,env=env,capture_output=True,text=True,timeout=60)
print(json.dumps({'baseline_exit':r.returncode,'output':r.stderr[-1000:]}),flush=True)
assert r.returncode==0,'snapshot baseline must pass before any mutation'
cases=[]
p=clone/'src/menus/rule_actions.py';s=p.read_text();a=s.index('def resolve_pregnancy(');b=s.index('\n\ndef ',a+1);part=s[a:b];assert part.count('    _guard_romance(player, partner)\n')==1
cases.append(('missing-pregnancy-resolve-guard',p,s,s[:a]+part.replace('    _guard_romance(player, partner)\n','')+s[b:],'test_stored_pregnancy_keep_and_overruled_abort_check_both_ages'))
p=clone/'src/game/service.py';s=p.read_text();old='''            # A birth can resume here after a stored choice. Preserve the
            # same typed guard errors as direct age_up, with full rollback.
            try:
                completed = coordinator.run_turn(env, self, random)
            except (rule_actions.RuleError, GraphError) as e:
                status = 409 if e.code in ("not_allowed", "refused", "not_adult") \\
                    else 404 if e.code == "not_found" else 400
                raise GameError(e.code, e.message, status) from e''';assert s.count(old)==1
cases.append(('untyped-birth-continuation',p,s,s.replace(old,'            completed = coordinator.run_turn(env, self, random)'),'test_birth_continuation_after_stored_choice_has_typed_adult_guard'))
failed=[]
for label,p,original,mutated,test in cases:
 p.write_text(mutated)
 try:
  r=subprocess.run(['python3','-m','unittest','tests.test_game_m25_e1.LegacyPregnancyAdultTests.'+test],cwd=clone,env=env,capture_output=True,text=True,timeout=60)
  expected=r.returncode!=0 and ('FAIL:' in r.stderr or 'ERROR:' in r.stderr) and 'Ran 1 test' in r.stderr
  print(json.dumps({'case':label,'expected_failure':expected,'output':r.stderr[-2500:]}),flush=True)
  if not expected:failed.append(label)
 finally:p.write_text(original)
print(json.dumps({'cases':len(cases),'unexpected_results':failed}),flush=True)
shutil.rmtree(clone)
raise SystemExit(bool(failed))
