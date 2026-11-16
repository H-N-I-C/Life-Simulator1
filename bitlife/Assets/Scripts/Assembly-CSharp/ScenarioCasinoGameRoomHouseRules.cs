using System.Collections;

public class ScenarioCasinoGameRoomHouseRules : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoGameRoom _gameRoom;

	private readonly bool _isNewGameRoom;

	public ScenarioCasinoGameRoomHouseRules(SimCasinoGameRoom gameRoom, bool isNewGameRoom = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void ShowFeedback(string title, string body)
	{
	}
}
