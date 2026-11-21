import sys,shutil
sys.path.insert(0,'/home/user/Life-Simulator1')
from tests.test_game_m3_integration import make_service,new_graduate_game,grant_skills

svc=make_service()
try:
    st=new_graduate_game(svc,7,age=26)
    gid=st['game_id']; grant_skills(svc,gid,care=100,communication=100,practical=100)
    env=svc._load(gid)
    env['education_state']['qualifications']=[{'qualification_id':'care_diploma_cert','program_id':'care_diploma','program_version':1,'person_id':env['player'].person_id,'awarded_turn':'fixture'}]
    svc._save(env); st=svc.get_state(gid); counter=0
    def cmd(action,payload):
        global st,counter
        counter+=1
        st=svc.command(gid,st['revision'],'tenure-'+str(counter),action,payload)
        return st
    def employ(role):
        cmd('careers_apply',{'opening':role+'@bluegum'})
        d=st['pending_decision']; cmd('resolve_decision',{'decision_id':d['id'],'option_id':'accept'})
    employ('care_assistant'); cmd('careers_resign',{})
    employ('qualified_care_worker'); cmd('age_up',{})
    for _ in range(20):
        d=st.get('pending_decision')
        if not d: break
        o=next(o for o in d['options'] if o.get('enabled',True) and not o.get('known_costs'))
        cmd('resolve_decision',{'decision_id':d['id'],'option_id':o['id'],'inputs':{}})
    print('Qualified-care tenure before resignation:',st['careers']['assignment']['role_tenure'])
    nurse=lambda:next(o for o in st['careers']['openings'] if o['role_id']=='nurse')
    print('Nurse eligibility while still employed:',nurse()['eligible'],nurse()['reason'])
    cmd('careers_resign',{})
    print('Nurse eligibility immediately after resignation:',nurse()['eligible'],nurse()['reason'])
    cmd('careers_apply',{'opening':'nurse@bluegum'})
    print('Actual application:',st['pending_decision']['kind'])
    env=svc._load(gid)
    env['education_state']['qualifications'].append({'qualification_id':'business_degree','program_id':'business_bachelors','program_version':1,'person_id':env['player'].person_id,'awarded_turn':'fixture'})
    svc._save(env)
    opening=next(o for o in svc.get_state(gid)['careers']['openings'] if o['role_id']=='electrician')
    print('Business-degree route projection:',{'eligible':opening['eligible'],'reason':opening['reason'],'requirements':opening['requirements']})
finally:shutil.rmtree(svc.save_dir,ignore_errors=True)
