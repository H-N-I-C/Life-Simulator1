import json,sys,os,tempfile,traceback
sys.path.insert(0,os.getcwd())
from src.game.service import GameService
p='docs/collab/evidence/robot/robot-seed1-life340-http_5xx-20261117-105002.json'
d=json.load(open(p))
with tempfile.TemporaryDirectory() as tmp:
 s=GameService(tmp);body=d['command_log'][0]['body'].copy();req=body.pop('request_id');st=s.new_game(body,req);gid=st['game_id']
 for c in d['command_log'][1:]:
  if c['kind']!='command':continue
  print(c['action'],c['payload'],flush=True)
  st=s.command(gid,c['expected_revision'],c['request_id'],c['action'],c['payload'])
 print('PASS current-code exact command replay')
