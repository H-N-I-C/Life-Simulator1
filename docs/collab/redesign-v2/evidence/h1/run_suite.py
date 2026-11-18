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
try:
    suite=unittest.defaultTestLoader.discover('tests',top_level_dir='.')
    result=unittest.TextTestRunner(verbosity=1).run(suite)
    ok=result.wasSuccessful()
finally:
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
