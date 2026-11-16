using System;

public class ScenarioSecretAgentAgeUpExAgentBlackmail : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgent, bool> _003C_003E9__7_0;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__7_0(SimSecretAgent t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private double _amount;

	public ScenarioSecretAgentAgeUpExAgentBlackmail(SimSecretAgent agent)
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
