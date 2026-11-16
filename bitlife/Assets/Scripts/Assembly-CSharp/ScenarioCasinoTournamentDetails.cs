using System.Collections;

public class ScenarioCasinoTournamentDetails : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly CASINO_GAME_TYPE _gameType;

	private float _prizeMax;

	public ScenarioCasinoTournamentDetails(CASINO_GAME_TYPE gameType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
