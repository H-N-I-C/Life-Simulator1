using System;

public class ScenarioSecretAgentAgeUpRivalAgencyHack : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSpyAgencySecurityFeature, int> _003C_003E9__9_0;

		internal int _003CScenarioIsEligibleForRandomPush_003Eb__9_0(SimSpyAgencySecurityFeature t)
		{
			return 0;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private SimSecretAgent _stolenAgent;

	private SimSecretAgentMission _stolenMission;

	private readonly SimRivalSpyAgency _rivalAgency;

	private double _stolenAmount;

	public ScenarioSecretAgentAgeUpRivalAgencyHack(SimRivalSpyAgency rivalAgency)
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
