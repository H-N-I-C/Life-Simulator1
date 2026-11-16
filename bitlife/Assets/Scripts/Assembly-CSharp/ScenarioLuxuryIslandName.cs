using System.Collections;

public class ScenarioLuxuryIslandName : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimLuxuryIsland _island;

	private readonly bool _newIsland;

	public ScenarioLuxuryIslandName(SimLuxuryIsland island, bool newIsland = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
