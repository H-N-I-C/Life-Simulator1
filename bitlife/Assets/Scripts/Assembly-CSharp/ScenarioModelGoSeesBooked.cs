using System.Collections.Generic;

public class ScenarioModelGoSeesBooked : Scenario
{
	private string _narrative;

	private List<SimModelingAgencyClient> _clients;

	private List<float> _clientRelationshipDiffs;

	private SimSpecialJobModel _modelJob;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
