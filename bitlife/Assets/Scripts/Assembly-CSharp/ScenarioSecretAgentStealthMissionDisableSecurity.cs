using System.Collections.Generic;

public class ScenarioSecretAgentStealthMissionDisableSecurity : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentStealthMission _mission;

	private string _correctWireColor;

	private List<string> _wireColors;

	private int _eventsCompleted;

	public ScenarioSecretAgentStealthMissionDisableSecurity(SimSecretAgentStealthMission mission, int eventsCompleted = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
