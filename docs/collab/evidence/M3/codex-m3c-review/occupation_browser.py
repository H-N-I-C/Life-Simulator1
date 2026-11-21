import sys,json
sys.path.insert(0,'/home/user/Life-Simulator1')
from tools import m3_journeys as runner
from tests.browser.m3_journeys import drain_pendings

def probe(ctx):
    ok,detail=drain_pendings(ctx); assert ok,detail
    result=ctx.api('train_skill',{'target_id':'train_communication'})
    assert not result.get('_status'),result
    result=ctx.api('careers_apply',{'opening':'office_assistant@meridian'})
    assert not result.get('_status'),result
    d=result['pending_decision']
    result=ctx.api('resolve_decision',{'decision_id':d['id'],'option_id':'accept'})
    assert not result.get('_status'),result
    for _ in range(2):
        result=ctx.api('age_up',{}); assert not result.get('_status'),result
        ok,detail=drain_pendings(ctx); assert ok,detail
    assignment=ctx.state()['careers']['assignment']
    rows=[]
    for width,height in ((390,844),(320,568)):
        s=ctx.open_page(width)
        s.call('Emulation.setDeviceMetricsOverride',{'width':width,'height':height,'deviceScaleFactor':1,'mobile':True})
        s.js("document.querySelector('.v2-tabbar button[data-screen=job]').click()")
        s.wait_js("!!document.querySelector('.v2cw-perf-fill')",timeout=10)
        text=s.js("document.querySelector('.v2cw-current').textContent")
        assert assignment['role_label'] in text and assignment['employer_name'] in text,text
        assert s.js("document.querySelector('.v2cw-perf-fill').style.width")==str(assignment['performance'])+'%'
        assert s.js("document.querySelector('#screen-job').firstElementChild.id")== 'v2CareersMount'
        subtitle=s.js("document.querySelector('.v2-header-sub').textContent")
        assert assignment['role_label'] in subtitle and assignment['employer_name'] in subtitle
        assert not s.js('document.documentElement.scrollWidth > innerWidth')
        ctx.shot(s,'occupation-%dx%d'%(width,height))
        rows.append({'width':width,'height':height,'performance':assignment['performance'],'subtitle':subtitle,'overflow':False})
    print(json.dumps(rows),flush=True)
    return 'PASS','Real training/hire/two years; hero first, correct performance and named header at390x844/320x568'

runner.EVIDENCE='/home/user/Life-Simulator1/docs/collab/evidence/M3/codex-m3c-review'
runner.JOURNEYS=[{'id':'occupation_ui','title':'Occupation UI verification','scenario':'employed_30','steps':[('hero','Actual employment presentation',probe)]}]
sys.exit(runner.main())
