using System.Collections;
using System.Collections.Generic;

public class ScenarioFightClubDopingEntry : ScenarioLazyCsvBase
{
	private FightClubDopingActivity _activity;

	private Dictionary<string, SimFightClubSteroid> _steroids;

	public ScenarioFightClubDopingEntry(FightClubDopingActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
