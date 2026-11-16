using System.Collections;
using System.Collections.Generic;

public class ScenarioLuxuryIslandSell : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimLuxuryIsland _island;

	private List<double> _listPrices;

	public ScenarioLuxuryIslandSell(SimLuxuryIsland island)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
