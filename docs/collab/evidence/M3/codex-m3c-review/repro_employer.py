import sys,json
sys.path.insert(0,'/home/user/Life-Simulator1')
from tools import m3c_evidence as runner
from tests.browser.m3c_checks import _ladder_step

def check(ctx):
    status,detail=_ladder_step('electrical')(ctx)
    if status!='PASS':return status,detail
    st=ctx.state(); assignment=st['careers']['assignment']
    pair=(assignment['role_id'],assignment['employer_id'])
    valid={(o['role_id'],o['employer_id']) for o in st['careers']['openings']}
    print(json.dumps({'final_pair':pair,'advertised_valid_pair':pair in valid,'progress':detail}),flush=True)
    return ('PASS' if pair in valid else 'FAIL'), 'Top-role assignment must use a valid employer-role pair'

runner.EVIDENCE='/home/user/Life-Simulator1/docs/collab/evidence/M3/codex-m3c-review'
runner.CHECKS=[{'id':'employer_pair','title':'Real-play employer pair validation','scenario':'graduating_18','steps':[('top_pair','Check final assignment',check)]}]
sys.exit(runner.main())
