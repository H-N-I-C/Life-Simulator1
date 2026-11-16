using System.Collections.Generic;

public class ScenarioLuxuryIslandVacationResultFeedback : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly List<SimPerson> _guests;

	private readonly SimLuxuryIsland _island;

	private readonly string _relationshipString;

	public ScenarioLuxuryIslandVacationResultFeedback(List<SimPerson> guests, SimLuxuryIsland island, string relationshipString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
