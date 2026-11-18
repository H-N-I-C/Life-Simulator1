"""Review runner: real tests; isolated Chrome profile/process cleanup only."""
import os,sys,tempfile,subprocess,signal,shutil,unittest,json
from pathlib import Path
sys.path.insert(0,os.getcwd())
root=tempfile.mkdtemp(prefix='v2-rereview-',dir=Path('.review-tmp').resolve())
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
    from tools.browser_check import Runner,write_report,DEFERRED_PENDING
    from tests.browser.flows import FLOWS
    import threading
    out='docs/collab/redesign-v2/evidence/rereview/browser-context-aligned'
    os.makedirs(out,exist_ok=True)
    h=server.make_server(0,save_dir=tempfile.mkdtemp(dir=root));h.dev_mode=True
    threading.Thread(target=h.serve_forever,daemon=True).start()
    proc,conn=launch_chrome();page=conn.new_session()
    r=Runner('http://127.0.0.1:%d'%h.server_port,page,out,service=h.service)
    for f in FLOWS:
        if f['name'].startswith('m25-') or f['name']=='v2-route-visibility':
            if f['name']=='v2-route-visibility':
                import copy
                f=copy.deepcopy(f)
                # Match the actual page's game selection to the API life
                # surrendered by this flow; retain all original assertions.
                step=f['steps'][-3]
                assert 'window.location.href' in step['js']
                step['js']="sessionStorage.setItem('game_id', window.CTX.game_id); "+step['js']
                print('QA_SETUP_CORRECTION: align browser session to surrendered CTX.game_id')
            r.run_flow(f)
    write_report(out,r.results,DEFERRED_PENDING)
    print(json.dumps(r.results,indent=2))
    ok=all(row[2] for row in r.results)
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
