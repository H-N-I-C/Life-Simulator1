import sys, json
sys.path.insert(0,'/home/user/Life-Simulator1')
from tools import m3_journeys as runner

def probe(ctx):
    st=ctx.state()
    before=st['skills']['values']['care']; cash=st['money']
    s=ctx.open_page(320)
    s.js("[...document.querySelectorAll('.v2-tabbar button')].find(b=>/activities/i.test(b.textContent)).click()")
    s.wait_js("!!document.querySelector('.v2sk-train')", timeout=10)
    st=ctx.state()
    before=st['skills']['values']['care']; cash=st['money']
    opened=s.js("(() => { const b=[...document.querySelectorAll('.v2sk-train')].find(b=>/evening skills course/i.test(b.textContent)); if (!b || b.disabled) return false; b.click(); return true; })()")
    assert opened, 'paid course must be enabled'
    assert s.js("!!document.querySelector('.v2sk-choice[data-skill=care]')")
    ctx.shot(s,'paid-choice-320')
    s.js("document.querySelector('.v2sk-choice[data-skill=care]').click()")
    for _ in range(20):
        ctx.settle(s,250)
        after=ctx.state()
        if after['skills']['uses_this_year'].get('train_evening_course')==1:
            break
    assert after['skills']['uses_this_year'].get('train_evening_course')==1
    fees=[e for e in after['finances']['ledger']
          if 'train_evening_course' in e.get('source_key','')]
    assert len(fees)==1 and fees[0]['amount']==-60, fees
    assert after['skills']['values']['care']>before
    assert not s.js('document.documentElement.scrollWidth > innerWidth')
    ctx.shot(s,'paid-result-320')
    print(json.dumps({'money_before':cash,'money_after':after['money'],'course_ledger':fees,'care_before':before,'care_after':after['skills']['values']['care'],'uses':after['skills']['uses_this_year']}),flush=True)
    return 'PASS','Actual care choice, single $60 charge, persisted gain, no overflow at 320'

runner.EVIDENCE='/home/user/Life-Simulator1/docs/collab/evidence/M3/codex-rereview'
runner.JOURNEYS=[{'id':'paid_course','title':'Paid choose-skill browser journey','scenario':'wealthy_married_40','steps':[('choose_and_pay','Choose care and pay once',probe)]}]
sys.exit(runner.main())
