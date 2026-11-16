using Game;

public class ScenarioSecretAgentOperativeAssassinationResultFeedback : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private readonly SimMurderTarget _target;

	private readonly DataSecretAgentAssassinationMethod _murderMethod;

	public ScenarioSecretAgentOperativeAssassinationResultFeedback(SimSecretAgent agent, SimMurderTarget target, DataSecretAgentAssassinationMethod murderMethod)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
