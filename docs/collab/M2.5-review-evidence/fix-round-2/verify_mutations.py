from pathlib import Path
import json,os,shutil,subprocess,tempfile
root=Path.cwd();clone=Path(tempfile.mkdtemp(prefix='m25-round2-mutations-',dir=root/'.review-tmp'))
for name in ('src','web','tests','tools','assets'):
 shutil.copytree(root/name,clone/name,ignore=shutil.ignore_patterns('__pycache__','*.pyc'))
for relative in ('docs/collab/M2.5/contracts','docs/collab/M2'):
 shutil.copytree(root/relative,clone/relative,ignore=shutil.ignore_patterns('*.png','*.log','evidence'))
cases=[]
def case(label,path,old,new,test):cases.append((label,path,old,new,test))
case('R1-known-value','src/game/features/knowledge/core.py','''    value = sum(change["delta"] for change in reputation_changes_known(
        env, observer_id) if change.get("person_id") == person_id
        and change.get("scope", "global") == scope_id)
    return max(-100, min(100, value))''','''    return reputation_get(env, person_id, scope_id)''','test_known_reputation_does_not_include_hidden_changes_or_scope')
case('R1-hidden-row','web/server.py','''        known_scopes = {(change["person_id"], change.get("scope", "global"))
                        for change in knowledge.reputation_changes_known(
                            env, knowledge.PLAYER_OBSERVER)}''','''        known_scopes = {(pid, scope) for pid, scopes in env["knowledge"]["reputation"].items() for scope in scopes}''','test_private_reputation_absent_from_http_until_observer_learns')
case('R2-unsampled-creation','src/game/features/parenting/effects.py','''        # A chemistry delta is not consent. Only an accepted offer may
        # create the edge; established relationships may change normally.
        return [], []''','''        transitions.start_relationship_op(env, a_id, b_id, "courtship", {"satisfaction":55,"trust":45,"chemistry":50}, _turn(env))
        rel = _find_active_relationship(env["graph"], a_id, b_id)''','test_chemistry_never_creates_unsampled_relationship')
case('R6-married-ban','src/game/features/family_graph/predicates.py','''        or any(set(u["person_ids"]) == {a_id, b_id}
               and u["status"] == "active" for u in graph["unions"])''','''        or any(b_id in u["person_ids"] and u["status"] == "active" for u in graph["unions"])''','test_married_stranger_is_still_a_dating_candidate')
case('R6-refusal-history','src/menus/rule_actions.py','''                option["_domain"] = {"offer": domain["offer"]} if "offer" in domain else None''','''                option["_domain"] = None''','test_dating_refusal_preserves_declined_offer_through_reload')
s=(clone/'src/game/service.py').read_text();a=s.index('            required = {"union_id"}',s.index('    def wedding_command'));b=s.index('            env = self._load(game_id)',a)
case('R10-input-validation','src/game/service.py',s[a:b],'','test_wedding_invalid_inputs_are_typed_and_atomic_over_http')
case('renewal-cadence','src/game/features/weddings/core.py','''    if last_year is not None and last_year >= player.age:
        raise GraphError("not_allowed", "vows already renewed this year", 409)''','''    # old behavior: each command grants another renewal''','test_renewal_once_per_union_per_year_including_free_and_reload')
case('ended-anniversary','src/game/features/weddings/core.py','''        if union["status"] != "active" \\
                or env["player"].person_id not in union["person_ids"]:
            continue''','''        # old behavior: include ended unions''','test_divorced_union_has_no_anniversary_during_real_year')
case('UI-explicit-enabled','web/static/features/relationship_policy/m25_screens.js','''  find.disabled = !!pending || !datingAction || datingAction.enabled !== true;''','''  find.disabled = !!pending || (datingAction && !datingAction.enabled);''','test_phone_dating_control_requires_explicit_adult_action')
case('UI-adult-wording','web/static/features/relationship_policy/m25_screens.js','''    they answer. Both people must be naturally 18 or older. For adults,
    laws and customs affect consequences, and anyone can say no.</p>''','''    they answer. Nobody is off-limits by law or custom.</p>''','test_phone_dating_control_requires_explicit_adult_action')
s=(clone/'src/game/features/weddings/core.py').read_text();a=s.index('    # Older saves have counts');b=s.index('    return dirty',a)
case('renewal-migration','src/game/features/weddings/core.py',s[a:b],'','test_legacy_free_renewal_history_recovers_year_guard_idempotently')
failures=[]
for label,path,old,new,test in cases:
 p=clone/path;s=p.read_text();assert s.count(old)==1,(label,s.count(old));p.write_text(s.replace(old,new))
 try:
  res=subprocess.run(['python3','-m','unittest','tests.test_game_m25_round2.RoundTwoTests.'+test],cwd=clone,env=dict(os.environ,TMPDIR=str(root/'.review-tmp/m25')),capture_output=True,text=True,timeout=60)
  expected=res.returncode!=0 and ('FAIL:' in res.stderr or 'ERROR:' in res.stderr) and 'Ran 1 test' in res.stderr
  row={'case':label,'expected_failure':expected,'exit_code':res.returncode,'output':res.stderr[-2300:]}
  print(json.dumps(row),flush=True)
  if not expected:failures.append(label)
 finally:p.write_text(s)
print(json.dumps({'mutations':len(cases),'unexpected_results':failures,'clone':str(clone)}),flush=True)
raise SystemExit(bool(failures))
