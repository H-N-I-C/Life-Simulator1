import sys,json,random,tempfile,shutil
from pathlib import Path
sys.path.insert(0,str(Path.cwd()))
from src.game.service import GameService
from src.game.content.journal import yearly_lines as yl
from src.people.classes.child import Child
from src.lifesim_lib.lifesim_lib import Gender
scratch=Path('.review-tmp/codex-feel-tmp');scratch.mkdir(parents=True,exist_ok=True);tmp=tempfile.mkdtemp(dir=scratch);svc=GameService(tmp);result={}
try:
 st=svc.new_game(dict(first_name='Journal',last_name='Review',gender='female',country='Australia',seed=7),'journal');env=svc._load(st['game_id']);p=env['player'];context={'work':svc._work_view(p),'education':svc._education_view(p)}
 rng=random.Random(7);recent=[];chosen=[];fallback=[]
 for age in range(18,65):
  p.age=age;p.has_job=False;context['work']=svc._work_view(p)
  item=yl.pick_year_line(p,context,recent,rng)
  if item:recent.append(item[0]);del recent[:-yl.RECENT_WINDOW];chosen.append({'age':age,'id':item[0]})
  else:fallback.append(age)
 result['adultPool']={'picked':len(chosen),'fallbackAges':fallback,'lastIds':chosen[-3:]}
 def only(target):
  return yl.pick_year_line(p,context,[l['id'] for l in yl.LINES if l['id']!=target],random.Random(1))
 p.age=30;p.happiness=30;p.has_job=False;context['work']=svc._work_view(p);result['neverEmployedCanClaimJobLoss']=only('adult_firedd_1')
 p.age=65;c=Child('Small','Review',Gender.Male,50,50,adopted=True);p.children.append(c);context['work']=svc._work_view(p);result['noGrandchildrenCanClaimGrandchildren']={'childAge':c.age,'childHasChildren':bool(getattr(c,'children',None)),'pick':only('elder_grandkid_1')}
 # The real production year entry still uses the old default.
 st=svc.command(st['game_id'],st['revision'],'real-age','age_up',{});result['actualYearLines']=[e['text'] for e in st['journal'] if e['kind']=='year'];result['productionRecents']=svc._load(st['game_id']).get('journal_line_recents')
finally:shutil.rmtree(tmp,ignore_errors=True)
out=Path('docs/collab/evidence/redesign-v2/undo-journal-review/journal-results.json');out.write_text(json.dumps(result,indent=2)+'\n');print(json.dumps(result,indent=2))
