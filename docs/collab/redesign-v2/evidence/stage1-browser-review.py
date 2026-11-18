import os,sys,json,tempfile,threading
sys.path.insert(0,os.getcwd())
from pathlib import Path
from web import server
from tools.cdp_client import launch_chrome
out=Path('docs/collab/redesign-v2/evidence')
h=server.make_server(0,save_dir=tempfile.mkdtemp(dir='.review-tmp/v2-stage1'));h.dev_mode=True
threading.Thread(target=h.serve_forever,daemon=True).start();c=None;result={}
try:
 st=h.service.new_game({'first_name':'Review','last_name':'StageOne','gender':'female','country':'Australia','seed':42,'scenario':'married_30'},'review-create');gid=st['game_id']
 c,conn=launch_chrome();s=conn.new_session('http://127.0.0.1:%d/game?game=%s'%(h.server_port,gid))
 s.call('Emulation.setDeviceMetricsOverride',{'width':390,'height':844,'deviceScaleFactor':1,'mobile':True})
 s.wait_js("!!document.querySelector('.v2-tabbar')")
 s.js("document.querySelector('.v2-header-btn').click();Array.from(document.querySelectorAll('.v2-more button')).find(b=>b.textContent==='Character editor').click()")
 s.wait_js("!!document.getElementById('editorPanel') && !document.getElementById('editorPanel').hidden")
 result['editor_from_home'] = s.js("(()=>{let e=document.getElementById('editorPanel');return {visible:e.checkVisibility(),activities_hidden:document.getElementById('screen-activities').hidden,panel_hidden:e.hidden}})()")
 s.js("document.querySelector('[data-screen=people]').click()")
 target=next(a['target_id'] for a in st['available_actions'] if a['id']=='spend_time' and a['enabled'])
 name=next(p['name'] for p in st['people'] if p['id']==target)
 s.js("Array.from(document.querySelectorAll('#familyList .chip')).find(e=>e.textContent.includes("+json.dumps(name)+")).click()")
 s.wait_js("!!document.querySelector('.v2-person-host .v2p-name')")
 result['person_initial']=s.js("document.querySelector('.v2-person-host').innerText")
 s.js("Array.from(document.querySelectorAll('.v2p-action')).find(b=>b.textContent.includes('Spend time')).click()")
 s.wait_js("document.querySelector('.v2-person-host').innerText.includes('already spent time')")
 result['person_after_action']=s.js("Array.from(document.querySelectorAll('.v2p-action')).map(b=>({text:b.textContent,disabled:b.disabled}))")
 result['revision_after_action']=h.service.get_state(gid)['revision']
 s.js("document.querySelector('.v2p-header-btn[aria-label=Edit]').click()")
 result['person_edit']=s.js("(()=>{let e=document.getElementById('editorPanel');return {visible:e.checkVisibility(),hidden:e.hidden,selected:Array.from(document.querySelectorAll('#editorPeople button[aria-pressed=true]')).map(b=>b.textContent)}})()")
 result['expected_edit_name']=name
 s.js("document.getElementById('backToLives').click()")
 s.wait_js("!document.getElementById('resumeView').hidden")
 result['all_lives']=s.js("({game_hidden:document.getElementById('gameView').hidden,game_visible:document.getElementById('gameView').checkVisibility(),resume_visible:document.getElementById('resumeView').checkVisibility(),game_display:getComputedStyle(document.getElementById('gameView')).display})")
 result['console_errors']=s.console_errors
 result['overflow']=s.js('document.documentElement.scrollWidth>innerWidth')
except Exception as e:
 result['error']={'type':type(e).__name__,'message':str(e)}
finally:
 if c:c.terminate()
 h.shutdown();h.server_close()
(out/'stage1-browser-review.json').write_text(json.dumps(result,indent=2));print(json.dumps(result,indent=2))
