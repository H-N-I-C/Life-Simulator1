from pathlib import Path
import json,os,shutil,subprocess,tempfile
root=Path.cwd();clone=Path(tempfile.mkdtemp(prefix='scenario-retry-proof-',dir=root/'.review-tmp'))
for name in ('src','web','tests','tools','assets'):
 shutil.copytree(root/name,clone/name,ignore=shutil.ignore_patterns('__pycache__','*.pyc'))
for relative in ('docs/collab/M2.5/contracts','docs/collab/M2'):
 shutil.copytree(root/relative,clone/relative,ignore=shutil.ignore_patterns('*.png','*.log','evidence'))
p=clone/'src/game/service.py';source=p.read_text()
cases=[('missing-original-fingerprint','fingerprint = creation_fingerprint or hashlib.sha256(', 'fingerprint = hashlib.sha256(', 'test_reported_unlucky_seeds_retry_and_creation_replays'),
 ('missing-university-retry','if e.code == "scenario_precondition" \\\n                            or "life ended early" in e.message:', 'if "life ended early" in e.message:', 'test_reported_unlucky_seeds_retry_and_creation_replays'),
 ('wrong-attempt-bound','if scenario_attempt + 1 < SCENARIO_MAX_ATTEMPTS:', 'if scenario_attempt + 1 < SCENARIO_MAX_ATTEMPTS - 1:', 'test_exhaustion_is_bounded_typed_and_does_not_publish_partial_game'),
 ('exhaustion-is-500','''                            "scenario_precondition",
                            "scenario setup preconditions could not be met "
                            f"after {SCENARIO_MAX_ATTEMPTS} attempts; try another seed",
                            409) from e''','''                            "internal_error",
                            "scenario setup failed", 500) from e''','test_exhaustion_is_bounded_typed_and_does_not_publish_partial_game')]
fail=[]
for label,old,new,test in cases:
 assert source.count(old)==1,(label,source.count(old));p.write_text(source.replace(old,new))
 r=subprocess.run(['python3','-m','unittest','tests.test_game_scenario_retry.ScenarioRetryTests.'+test],cwd=clone,env=dict(os.environ,TMPDIR=str(root/'.review-tmp/m25')),capture_output=True,text=True,timeout=60)
 okay=r.returncode!=0 and 'FAIL:' in r.stderr and 'Ran 1 test' in r.stderr
 print(json.dumps({'case':label,'expected_failure':okay,'output':r.stderr[-1700:]}),flush=True)
 if not okay:fail.append(label)
 p.write_text(source)
print(json.dumps({'cases':len(cases),'unexpected_results':fail}),flush=True)
shutil.rmtree(clone)
raise SystemExit(bool(fail))
