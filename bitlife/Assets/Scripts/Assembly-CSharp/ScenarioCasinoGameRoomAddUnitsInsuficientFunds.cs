public class ScenarioCasinoGameRoomAddUnitsInsuficientFunds : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private double _cost;

	private SimCasinoGameRoom _gameRoom;

	private int _unitsToInstall;

	private string _unitDescription;

	public ScenarioCasinoGameRoomAddUnitsInsuficientFunds(SimCasinoGameRoom gameRoom, int unitsToInstall, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
