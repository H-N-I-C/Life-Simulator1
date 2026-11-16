using System.Collections;
using Game;

public class ScenarioRacingAgeUpFanInteraction : Scenario
{
	private string _narrative;

	private SimStreetPerson _streetPerson;

	private BracketParser _parser;

	private DataRacingRacingFanInteraction _data;

	private readonly bool _isPositive;

	private readonly SimRacing _racing;

	public ScenarioRacingAgeUpFanInteraction(SimRacing racing, bool isPositive)
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
