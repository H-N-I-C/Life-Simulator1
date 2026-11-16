using System.Collections;

public class ScenarioRacingAgeUpFanEncouragement : Scenario
{
	private string _narrative;

	private readonly SimRacing _racing;

	private BracketParser _parser;

	private string _raceTrackTypeString;

	public ScenarioRacingAgeUpFanEncouragement(SimRacing racing)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
