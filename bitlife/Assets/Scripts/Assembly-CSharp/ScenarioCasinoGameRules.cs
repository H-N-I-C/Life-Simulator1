public class ScenarioCasinoGameRules : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private SimCasinoSession _session;

	private SimCasinoGameRoom _gameRoom;

	public ScenarioCasinoGameRules(SimCasinoSession session, SimCasinoGameRoom gameRoom = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
