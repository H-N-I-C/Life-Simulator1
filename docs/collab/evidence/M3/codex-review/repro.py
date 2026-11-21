import sys, tempfile, random, json, shutil
sys.path.insert(0, '/home/user/Life-Simulator1')
from src.game.service import GameService, GameError
from src.game.features.education import core as edu
from src.game.features.careers import core as career
from src.game.features.skills import training, state

directory = tempfile.mkdtemp()
svc = GameService(directory)
st = svc.new_game({'first_name':'Ada','last_name':'Lee','gender':'female','country':'Australia','seed':7}, 'create')
gid = st['game_id']
env = svc._load(gid)
p = env['player']; p.age = 22; p.grades = None; p.dropped_out = False
svc._save(env)
def command(action, payload):
    st = svc.get_state(gid)
    try:
        out = svc.command(gid, st['revision'], 'probe-'+str(st['revision'])+action, action, payload)
        return {'ok':True, 'pending':(out.get('pending_decision') or {}).get('kind')}
    except Exception as e:
        return {'error':type(e).__name__, 'code':getattr(e,'code',None), 'message':str(e)}

print('UI skill action:', command('train_practical', {'target_id':'train_practical'}))
print('canonical skill action:', command('train_skill', {'target_id':'train_practical'}))
print('bad education type:', command('education_enroll', {'program_id':[]}))
print('bad career type:', command('careers_apply', {'role_id':[], 'employer_id':'voltworks'}))
env = svc._load(gid); p = env['player']
career.create_apprenticeship_assignment(env,p,'app_electrician','voltworks')
print('active job declares:', training._declared_contributions(env))
before = dict(state.values(env,p.person_id))
training.annual_hook(env,random.Random(7),svc)
print('annual job gain:', {k:state.values(env,p.person_id)[k]-v for k,v in before.items()})
# Exercise annual applier with the expected declaration, without changing code.
original = training._declared_contributions
training._declared_contributions = lambda env: {'job':'practical'}
try:
    training.annual_hook(env,random.Random(7),svc)
except Exception as e:
    print('annual applier with declaration:', type(e).__name__, str(e))
finally:
    training._declared_contributions = original
career.close_apprenticeship(env,p,'test')
edu._enroll(env,p,edu.data()['care_diploma'],'self',random.Random(7))
state.values(env,p.person_id)['communication']=50
svc._save(env)
print('job while enrolled:', command('careers_apply', {'role_id':'office_assistant','employer_id':'meridian'}))
env = svc._load(gid); p = env['player']
env['pending'] = None
p.has_job=True; p.salary=50000
career.ensure(env)['assignments'].clear()
edu.active_enrollment(env,p.person_id)['status']='left'
svc._save(env)
print('apprentice replaces legacy job:', command('education_enroll', {'program_id':'app_electrical'}))
env = svc._load(gid)
print('legacy salary after:',env['player'].salary)
env['pending']=None
p=env['player']
p.has_job=False; p.salary=0
career.annual_hook(env,svc,random.Random(7))
print('assignment after legacy dismissal:',career.active_assignment(env,p.person_id))
# A saved qualification has no owner; a new current person sees it too.
edu.ensure(env)['qualifications'].append({'qualification_id':'nursing_degree','program_id':'nursing_bachelors','program_version':1,'awarded_turn':'test'})
original_pid=p.person_id
p.person_id='different-successor'
print('successor qualifications:',edu.education_view(env,p)['qualifications'])
old_enrollment=next(iter(edu.ensure(env)['enrollments'].values()))
old_enrollment['status']='active'; old_enrollment['progress']=0; old_enrollment['studied_this_year']=True
env['turn']['turn_id']='successor-turn'
print('old enrollment owner:',old_enrollment['person_id'],'current player:',p.person_id)
print('successor charged for old enrollment:',edu.annual_hook(env,svc,random.Random(7))['money_entries'])
shutil.rmtree(directory)
