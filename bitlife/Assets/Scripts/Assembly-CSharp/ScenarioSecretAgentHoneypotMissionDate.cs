using System;
using System.Collections.Generic;
using Game;

public class ScenarioSecretAgentHoneypotMissionDate : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataSecretAgentHoneypotDateLocation, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(DataSecretAgentHoneypotDateLocation t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string selectedChoice;

		public ScenarioSecretAgentHoneypotMissionDate _003C_003E4__this;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataSecretAgentHoneypotDateLocation t)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(DataSecretAgentHoneypotDateLocation t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentHoneypotMission _mission;

	private readonly string _firstDateLocation;

	private List<DataSecretAgentHoneypotDateLocation> _choiceDataMatch;

	public ScenarioSecretAgentHoneypotMissionDate(SimSecretAgentHoneypotMission mission, string firstDateLocation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
