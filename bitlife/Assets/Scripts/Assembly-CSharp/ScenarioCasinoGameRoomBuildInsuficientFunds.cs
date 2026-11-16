public class ScenarioCasinoGameRoomBuildInsuficientFunds : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private double _cost;

	private CASINO_GAME_TYPE _gameType;

	private int _unitsToInstall;

	public ScenarioCasinoGameRoomBuildInsuficientFunds(CASINO_GAME_TYPE gameType, int unitsToInstall, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
