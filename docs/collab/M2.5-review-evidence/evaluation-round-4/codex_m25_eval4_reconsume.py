import os,sys,json,random
sys.path.insert(0,os.getcwd())
from tests.test_game_m25_conception import ConceptionBirthTests
from src.game.service import GameService
c=ConceptionBirthTests();c.setUp();s=c.svc;gid=c.gid

def keep(tag):
 e=s._load(gid);e['rng_state']=random.Random(1).getstate();e['cosmetic_rng_state']=random.Random(0).getstate();s._save(e)
 st=s.get_state(gid);st=s.command(gid,st['revision'],tag,'try_for_baby',{});p=st['pending_decision']
 return s.command(gid,st['revision'],tag+'-keep','resolve_decision',{'decision_id':p['id'],'option_id':'keep'})
keep('first');e=s._load(gid)
e['turn']={'turn_id':'forced-birth','cursor':{'phase_id':'family_events','item_cursor':0},'done':{}}
e['rng_state']=random.Random(1).getstate();s._save(e)
c._give_birth();e=s._load(gid);baby=e['player'].children[-1];first_baby_id=baby.person_id
st=s.get_state(gid)
for i in range(30):
 if not st['pending_decision']:break
 p=st['pending_decision'];o=next(o for o in reversed(p['options']) if o['enabled'])
 st=s.command(gid,st['revision'],'drain'+str(i),'resolve_decision',{'decision_id':p['id'],'option_id':o['id']})
keep('second');before=s._load(gid)['graph']['conception_outcomes']
result=s.consume_conception_outcome(gid,first_baby_id)
after=s._load(gid)['graph']['conception_outcomes']
print(json.dumps({'first_child':first_baby_id,'before':before,'retried_old_child_result':result,'after':after},indent=2))
