import sys,random
from unittest.mock import patch
sys.path.insert(0,'/home/user/Life-Simulator1')
from tests.test_rules_m3_gates import GateTests
from src.game import events,coordinator
from src.game.context import RuleContext

case=GateTests('test_work_events_shut_without_assignment')
case.setUp()
try:
    env=case._take_office_job()
    loaded=[e for e in events.load_event_files()
            if e['id'].startswith(('wrk_','stu_'))]
    control,_=events.EventEngine(loaded).sample_v3(
        random.Random(0),20,[],events.GameContext(env,case.svc),
        prefixes=('wrk_',),actor_id=env['player'].person_id)
    env['turn'].setdefault('done',{})
    # Content loading is the I/O boundary. Exercise the real coordinator
    # with only the new event family available; its samplers exclude v3.
    with patch.object(events,'load_event_files',return_value=loaded):
        result=coordinator._decision_offer(
            env,RuleContext(env,case.svc),case.svc,random.Random(0))
    print({'new_events':len(loaded),'direct_v3_control':control['id'],
           'production_decision_offer':result.get('pending_decision')})
finally:
    case.tearDown()
