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
try:
    exec(compile('import os,sys,json,tempfile,threading\nsys.path.insert(0,os.getcwd())\nfrom pathlib import Path\nfrom web import server\nfrom tools.cdp_client import launch_chrome\nout=Path(\'docs/collab/redesign-v2/evidence/rereview\')\nh=server.make_server(0,save_dir=tempfile.mkdtemp(dir=os.environ[\'TMPDIR\']));h.dev_mode=True\nthreading.Thread(target=h.serve_forever,daemon=True).start();c=None;result={}\ntry:\n st=h.service.new_game({\'first_name\':\'Review\',\'last_name\':\'StageOne\',\'gender\':\'female\',\'country\':\'Australia\',\'seed\':42,\'scenario\':\'married_30\'},\'review-create\');gid=st[\'game_id\']\n c,conn=launch_chrome();s=conn.new_session(\'http://127.0.0.1:%d/game?game=%s\'%(h.server_port,gid))\n s.call(\'Emulation.setDeviceMetricsOverride\',{\'width\':390,\'height\':844,\'deviceScaleFactor\':1,\'mobile\':True})\n s.wait_js("!!document.querySelector(\'.v2-tabbar\')")\n s.js("document.querySelector(\'.v2-header-btn\').click();Array.from(document.querySelectorAll(\'.v2-more button\')).find(b=>b.textContent===\'Character editor\').click()")\n s.wait_js("!!document.getElementById(\'editorPanel\') && !document.getElementById(\'editorPanel\').hidden")\n result[\'editor_from_home\'] = s.js("(()=>{let e=document.getElementById(\'editorPanel\');return {visible:e.checkVisibility(),activities_hidden:document.getElementById(\'screen-activities\').hidden,panel_hidden:e.hidden}})()")\n s.js("document.querySelector(\'[data-screen=people]\').click()")\n target=next(a[\'target_id\'] for a in st[\'available_actions\'] if a[\'id\']==\'spend_time\' and a[\'enabled\'])\n name=next(p[\'name\'] for p in st[\'people\'] if p[\'id\']==target)\n s.js("Array.from(document.querySelectorAll(\'#familyList .chip\')).find(e=>e.textContent.includes("+json.dumps(name)+")).click()")\n s.wait_js("!!document.querySelector(\'.v2-person-host .v2p-name\')")\n result[\'person_initial\']=s.js("document.querySelector(\'.v2-person-host\').innerText")\n s.js("Array.from(document.querySelectorAll(\'.v2p-action\')).find(b=>b.textContent.includes(\'Spend time\')).click()")\n s.wait_js("document.querySelector(\'.v2-person-host\').innerText.includes(\'already spent time\')")\n result[\'person_after_action\']=s.js("Array.from(document.querySelectorAll(\'.v2p-action\')).map(b=>({text:b.textContent,disabled:b.disabled}))")\n result[\'revision_after_action\']=h.service.get_state(gid)[\'revision\']\n s.js("document.querySelector(\'.v2p-header-btn[aria-label=Edit]\').click()")\n result[\'person_edit\']=s.js("(()=>{let e=document.getElementById(\'editorPanel\');return {visible:e.checkVisibility(),hidden:e.hidden,selected:Array.from(document.querySelectorAll(\'#editorPeople button[aria-pressed=true]\')).map(b=>b.textContent)}})()")\n result[\'expected_edit_name\']=name\n s.js("document.getElementById(\'backToLives\').click()")\n s.wait_js("!document.getElementById(\'resumeView\').hidden")\n result[\'all_lives\']=s.js("({game_hidden:document.getElementById(\'gameView\').hidden,game_visible:document.getElementById(\'gameView\').checkVisibility(),resume_visible:document.getElementById(\'resumeView\').checkVisibility(),game_display:getComputedStyle(document.getElementById(\'gameView\')).display})")\n result[\'console_errors\']=s.console_errors\n result[\'overflow\']=s.js(\'document.documentElement.scrollWidth>innerWidth\')\nexcept Exception as e:\n result[\'error\']={\'type\':type(e).__name__,\'message\':str(e)}\nfinally:\n if c:c.terminate()\n h.shutdown();h.server_close()\n(out/\'stage1-browser-review.json\').write_text(json.dumps(result,indent=2));print(json.dumps(result,indent=2))\n', "targeted-stage1-review", "exec"))
    ok=True
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
