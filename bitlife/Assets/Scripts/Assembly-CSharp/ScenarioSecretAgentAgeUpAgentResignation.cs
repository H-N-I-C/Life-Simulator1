public class ScenarioSecretAgentAgeUpAgentResignation : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	public ScenarioSecretAgentAgeUpAgentResignation(SimSecretAgent agent)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
