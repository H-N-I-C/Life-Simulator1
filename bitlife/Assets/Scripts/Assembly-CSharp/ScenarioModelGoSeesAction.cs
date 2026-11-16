using System.Collections.Generic;
using Game;

public class ScenarioModelGoSeesAction : Scenario
{
	private string _narrative;

	private int _clientIndex;

	private readonly List<SimModelingAgencyClient> _clients;

	private readonly List<float> _clientRelationshipDiffs;

	private readonly SimModelingAgencyClient _theClient;

	private DataModelGoSeeActionPose _theAction;

	private BracketParser _parser;

	public ScenarioModelGoSeesAction(List<SimModelingAgencyClient> clients, List<float> clientRelationshipDiffs, int clientIndex)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
