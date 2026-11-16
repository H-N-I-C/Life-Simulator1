using System.Collections.Generic;
using Game;

public class ScenarioSecretAgentHoneypotMissionIntimateMoment : Scenario
{
	private string _narrative;

	private readonly SimSecretAgentHoneypotMission _mission;

	private List<DataSecretAgentHoneypotStingAction> _dataButtons;

	private string _locationString;

	private BracketParser _parser;

	private List<string> _listButtonStrings;

	public ScenarioSecretAgentHoneypotMissionIntimateMoment(SimSecretAgentHoneypotMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
