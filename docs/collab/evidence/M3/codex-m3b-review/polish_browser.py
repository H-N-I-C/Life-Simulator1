import sys,json
sys.path.insert(0,'/home/user/Life-Simulator1')
from tools import m3_journeys as runner
from src.game.finance import Ledger

def probe(ctx):
    svc=runner.RUNNER['httpd'].service
    env=svc._load(ctx.game_id); player=env['player']
    Ledger(env,resolver=svc._make_resolver(env)).post('review-cash',player.age,{
        'phase':'review','occurrence_id':'negative-cash','account_owner_id':player.person_id,
        'category':'other','amount':-8095-player.money,'description':'Forced display fixture'})
    svc._save(env)
    rows=[]
    for width in (390,320):
        s=ctx.open_page(width)
        s.wait_js("!!document.querySelector('.v2-header-namerow')",timeout=10)
        cash=s.js("document.querySelector('.v2-header-cash').textContent")
        assert '-$8,095' in cash,cash
        assert s.js("document.getElementById('educationCard').hidden")
        s.js("document.querySelector('.v2-tabbar button[data-screen=activities]').click()")
        ctx.settle(s,500)
        assert s.js("[...document.querySelectorAll('button')].some(b=>b.textContent.includes('Join a public speaking circle') && b.textContent.includes('Cost: $25'))")
        assert not s.js('document.documentElement.scrollWidth > innerWidth')
        ctx.shot(s,'polish-'+str(width))
        for key in ('Enter',' '):
            s.js("document.querySelector('.v2-tabbar button[data-screen=activities]').click()")
            s.js("document.querySelector('.v2-header-namerow').dispatchEvent(new KeyboardEvent('keydown',{key:%s,bubbles:true}))" % json.dumps(key))
            assert s.js("document.querySelector('.v2-header-namerow').getAttribute('aria-current')==='page'")
        rows.append({'width':width,'cash':cash,'home_enter_space':True,'empty_education_hidden':True,'cost_label':'Cost: $25','overflow':False})
    env=svc._load(ctx.game_id)
    env['player'].student_loan=12345; env['player'].chose_student_loan=True
    svc._save(env)
    s=ctx.open_page(320)
    assert not s.js("document.getElementById('educationCard').hidden")
    assert '$12,345' in s.js("document.getElementById('educationCard').textContent")
    print(json.dumps(rows),flush=True)
    return 'PASS','Signed cash/cost formatting, Home Enter/Space, empty-card hiding and live-loan retention verified at 390/320'

runner.EVIDENCE='/home/user/Life-Simulator1/docs/collab/evidence/M3/codex-m3b-review'
runner.JOURNEYS=[{'id':'polish','title':'Polish verification','scenario':'graduating_18','steps':[('ui','Header, money and legacy Education',probe)]}]
sys.exit(runner.main())
