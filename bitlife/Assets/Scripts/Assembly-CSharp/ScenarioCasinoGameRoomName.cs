using System.Collections;

public class ScenarioCasinoGameRoomName : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoGameRoom _gameRoom;

	private readonly bool _isNewGameRoom;

	public ScenarioCasinoGameRoomName(SimCasinoGameRoom gameRoom, bool isNewGameRoom = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
