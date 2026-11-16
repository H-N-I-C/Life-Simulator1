using System;
using System.Collections;

public class ScenarioSecretAgentAgeUpExAgentKidnapRescue : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgent, SECRET_AGENT_RANK> _003C_003E9__5_0;

		public static Func<SimSecretAgent, string> _003C_003E9__5_1;

		internal SECRET_AGENT_RANK _003CRefreshScenario_003Eb__5_0(SimSecretAgent t)
		{
			return SECRET_AGENT_RANK.NONE;
		}

		internal string _003CRefreshScenario_003Eb__5_1(SimSecretAgent t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string selectedAgentName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimSecretAgent t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _kidnapperAgent;

	private readonly SimPerson _kidnappedPerson;

	public ScenarioSecretAgentAgeUpExAgentKidnapRescue(SimSecretAgent kidnapperAgent, SimPerson kidnappedPerson)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
