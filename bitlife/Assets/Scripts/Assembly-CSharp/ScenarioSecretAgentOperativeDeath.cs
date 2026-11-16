public class ScenarioSecretAgentOperativeDeath : Scenario
{
	private string _narrative;

	private readonly SimSecretAgent _agent;

	private readonly SimSpyAgency _agency;

	private readonly string _deathString;

	private readonly bool _naturalDeath;

	public ScenarioSecretAgentOperativeDeath(SimSecretAgent agent, string deathString, bool naturalDeath = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
