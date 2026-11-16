public class ScenarioSecretAgentAgeUpMissingAgentStatusRevealRecovered : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public SimSecretAgent agent;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__0(SimSecretAgentMission x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private readonly SimSecretAgentMission _mission;

	public ScenarioSecretAgentAgeUpMissingAgentStatusRevealRecovered(SimSecretAgent agent, SimSecretAgentMission mission)
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
