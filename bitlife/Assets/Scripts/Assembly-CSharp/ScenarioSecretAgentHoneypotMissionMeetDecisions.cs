using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioSecretAgentHoneypotMissionMeetDecisions : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public List<string> aDisguise;

		internal bool _003CRefreshScenario_003Eb__0(string t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public DataSecretAgentHoneypotAgentDisguise disguiseData;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(string turnOff)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(string turnOn)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentHoneypotMission _mission;

	private DataSecretAgentHoneypotApproachColor _data;

	private BracketParser _parser;

	public ScenarioSecretAgentHoneypotMissionMeetDecisions(SimSecretAgentHoneypotMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
