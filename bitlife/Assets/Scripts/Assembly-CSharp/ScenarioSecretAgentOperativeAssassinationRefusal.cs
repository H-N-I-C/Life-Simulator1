using Game;

public class ScenarioSecretAgentOperativeAssassinationRefusal : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private readonly SimMurderTarget _target;

	private readonly DataSecretAgentAssassinationMethod _murderMethod;

	private readonly float _amount;

	public ScenarioSecretAgentOperativeAssassinationRefusal(SimSecretAgent agent, SimMurderTarget target, DataSecretAgentAssassinationMethod murderMethod, float amount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
