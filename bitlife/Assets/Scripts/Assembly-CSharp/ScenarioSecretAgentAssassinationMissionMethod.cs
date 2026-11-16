using System;
using System.Collections;
using Game;

public class ScenarioSecretAgentAssassinationMissionMethod : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataSecretAgentAssassinationMissionMethod, string> _003C_003E9__4_0;

		public static Func<DataSecretAgentAssassinationLocation, string> _003C_003E9__4_1;

		internal string _003CRefreshScenario_003Eb__4_0(DataSecretAgentAssassinationMissionMethod t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__4_1(DataSecretAgentAssassinationLocation t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private SimSecretAgentAssassinationMission _mission;

	public ScenarioSecretAgentAssassinationMissionMethod(SimSecretAgentAssassinationMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
