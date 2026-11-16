using Game;

public class ScenarioSecretAgentAssassinationMissionOpportunity : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentAssassinationMission _mission;

	private int _goodOpportunitiesIgnored;

	private DataSecretAgentAssassinationTargetOpportunitySnippet _opportunityScenarioData;

	private readonly DataSecretAgentAssassinationMissionMethod _assassinationMethodData;

	private BracketParser _parser;

	public ScenarioSecretAgentAssassinationMissionOpportunity(SimSecretAgentAssassinationMission mission, int goodOpportunitiesIgnored = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
