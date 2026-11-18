"""Review runner: real tests; isolated Chrome profile/process cleanup only."""
import os,sys,tempfile,subprocess,signal,shutil,unittest,json
from pathlib import Path
sys.path.insert(0,os.getcwd())
root=tempfile.mkdtemp(prefix='r-',dir=Path('.review-tmp').resolve())
os.environ['TMPDIR']=root;tempfile.tempdir=None
original=subprocess.Popen
chrome=[]
def tracked_popen(args,*a,**kw):
    if isinstance(args,list) and '--headless=new' in args and '--remote-debugging-port=0' in args:
        profile=tempfile.mkdtemp(prefix='chrome-profile-',dir=root)
        args=list(args)+['--user-data-dir='+profile]
        kw['start_new_session']=True
        p=original(args,*a,**kw);chrome.append(p);return p
    return original(args,*a,**kw)
subprocess.Popen=tracked_popen
ok=False
h=None
try:
    from web import server
    from tools.cdp_client import launch_chrome
    import threading
    out=Path('docs/collab/redesign-v2/evidence/stage2')
    h=server.make_server(0,save_dir=tempfile.mkdtemp(dir=root))
    threading.Thread(target=h.serve_forever,daemon=True).start()
    st=h.service.new_game({'first_name':'Age','last_name':'Review','gender':'female','country':'Australia','seed':42},'age-review')
    gid=st['game_id'];proc,conn=launch_chrome();p=conn.new_session('http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid))
    p.wait_js("!!document.querySelector('.v2h-age') && !document.querySelector('.v2h-age').disabled")
    p.js("window.reviewActionDone=false;const old=window.fetch;window.fetch=(...args)=>old(...args).then(r=>{if(String(args[0]).includes('/actions'))window.reviewActionDone=true;return r});document.querySelector('.v2h-age').click()")
    p.wait_js('window.reviewActionDone===true')
    p.js('new Promise(r=>setTimeout(r,100))')
    state=h.service.get_state(gid)
    result={'age':next(x['age'] for x in state['people'] if x['id']==state['player_id']),'pending':state['pending_decision'],'age_advertised':any(x['id']=='age_up' and x['enabled'] for x in state['available_actions']),'button':p.js("({disabled:document.querySelector('.v2h-age').disabled,note:document.querySelector('.v2h-age-note').textContent})"),'console_errors':p.console_errors}
    (out/'age-repeat.json').write_text(json.dumps(result,indent=2));print(json.dumps(result,indent=2))
    ok=True
finally:
    if h:h.shutdown();h.server_close()
    subprocess.Popen=original
    for p in chrome:
        try:os.killpg(p.pid,signal.SIGTERM)
        except ProcessLookupError:pass
    for p in chrome:
        try:p.wait(timeout=3)
        except subprocess.TimeoutExpired:
            try:os.killpg(p.pid,signal.SIGKILL)
            except ProcessLookupError:pass
            p.wait()
        if p.stderr:p.stderr.close()
    shutil.rmtree(root)
    print('REVIEW_CLEANUP',json.dumps({'chrome_launches':len(chrome),'temp_root_removed':not os.path.exists(root)}))
sys.exit(0 if ok else 1)
