using System;
using System.Collections;

public class ScenarioSecretAgentHoneypotMissionBriefing : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgent, SECRET_AGENT_RANK> _003C_003E9__4_0;

		public static Func<SimSecretAgent, string> _003C_003E9__4_1;

		public static Func<SimSecretAgentGadget, string> _003C_003E9__4_2;

		internal SECRET_AGENT_RANK _003CRefreshScenario_003Eb__4_0(SimSecretAgent t)
		{
			return SECRET_AGENT_RANK.NONE;
		}

		internal string _003CRefreshScenario_003Eb__4_1(SimSecretAgent t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__4_2(SimSecretAgentGadget t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string selectedAgentName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimSecretAgent t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentHoneypotMission _mission;

	public ScenarioSecretAgentHoneypotMissionBriefing(SimSecretAgentMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
