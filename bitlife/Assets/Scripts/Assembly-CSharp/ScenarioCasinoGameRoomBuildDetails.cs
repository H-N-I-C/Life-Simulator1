using System.Collections;

public class ScenarioCasinoGameRoomBuildDetails : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly CASINO_GAME_TYPE _gameType;

	private string _unitDescription;

	public ScenarioCasinoGameRoomBuildDetails(CASINO_GAME_TYPE gameType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
