public class ScenarioSecretAgentCandidate : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _candidate;

	private readonly bool _includeVerification;

	private int _potentialScore;

	public ScenarioSecretAgentCandidate(SimSecretAgent candidate, bool includeVerification = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
