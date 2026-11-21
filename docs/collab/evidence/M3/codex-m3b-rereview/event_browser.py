import sys,json
sys.path.insert(0,'/home/user/Life-Simulator1')
from tools import m3_journeys as runner
from src.game.events import load_event_files

def probe(ctx):
    result=ctx.api('education_enroll',{'program_id':'app_electrical'})
    assert not result.get('_status'),result
    keep={'wrk_skill_refresh','stu_group_project'}
    def force(env):
        env['wrk_stu_seen']=[e['id'] for e in load_event_files()
                             if e['id'].startswith(('wrk_','stu_'))
                             and e['id'] not in keep]
    ctx.qa_env(force)
    result=ctx.api('age_up',{})
    assert result['pending_decision']['kind']=='wrk_skill_refresh'
    s=ctx.open_page(320)
    s.wait_js("!!document.querySelector('.v2e-option')",timeout=10)
    assert 'Back to Class' in s.js("document.querySelector('.v2e-card').textContent")
    assert not s.js('document.documentElement.scrollWidth > innerWidth')
    ctx.shot(s,'work-event-320')
    s.js("[...document.querySelectorAll('.v2e-option')].find(b=>b.textContent.includes('Attend the workshop')).click()")
    s.wait_js("!!document.querySelector('.v2e-outcome')",timeout=10)
    ctx.shot(s,'work-outcome-320')
    assert ctx.state()['pending_decision']['kind']=='stu_group_project'
    s.js("document.querySelector('.v2e-continue').click()")
    s.wait_js("!!document.querySelector('.v2e-option')",timeout=10)
    assert 'The Vanishing Group' in s.js("document.querySelector('.v2e-card').textContent")
    ctx.shot(s,'study-event-320')
    s.js("document.querySelector('.v2e-option').click()")
    s.wait_js("!!document.querySelector('.v2e-outcome')",timeout=10)
    ctx.shot(s,'study-outcome-320')
    return 'PASS','Actual work choice/outcome/Continue/study choice/outcome at 320; both families persisted in one year'

runner.EVIDENCE='/home/user/Life-Simulator1/docs/collab/evidence/M3/codex-m3b-rereview'
runner.JOURNEYS=[{'id':'event_ui','title':'Work and study event UI','scenario':'graduating_18','steps':[('both_families','Actual modal sequence',probe)]}]
sys.exit(runner.main())
