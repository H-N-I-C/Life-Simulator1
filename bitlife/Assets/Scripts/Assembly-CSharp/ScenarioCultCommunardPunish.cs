using System.Collections;
using Game;

public class ScenarioCultCommunardPunish : Scenario
{
	private string _narrative;

	private readonly SimCommunard _communard;

	private SimCult _cult;

	private readonly BracketParser _parser;

	private DataCultCommunardPunishment _chosenPunishment;

	public ScenarioCultCommunardPunish(SimCommunard communard)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
