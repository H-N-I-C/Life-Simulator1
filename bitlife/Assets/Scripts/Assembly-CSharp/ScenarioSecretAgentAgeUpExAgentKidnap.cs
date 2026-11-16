using System;

public class ScenarioSecretAgentAgeUpExAgentKidnap : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgent, bool> _003C_003E9__8_0;

		public static Func<SimSecretAgent, bool> _003C_003E9__8_1;

		public static Func<SimPerson, bool> _003C_003E9__9_0;

		public static Func<SimChild, bool> _003C_003E9__9_1;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__8_0(SimSecretAgent t)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__8_1(SimSecretAgent t)
		{
			return false;
		}

		internal bool _003CGetKidnappedPerson_003Eb__9_0(SimPerson t)
		{
			return false;
		}

		internal bool _003CGetKidnappedPerson_003Eb__9_1(SimChild t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private double _amount;

	private SimPerson _kidnappedPerson;

	public ScenarioSecretAgentAgeUpExAgentKidnap(SimSecretAgent agent)
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

	private static SimPerson GetKidnappedPerson()
	{
		return null;
	}
}
