using System.Collections.Generic;

public class ScenarioSecretAgentStealthMissionCover : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentStealthMission _mission;

	private string _threatString;

	private int _threatScore;

	private List<int> _effectivenessScores;

	private int _eventsCompleted;

	public ScenarioSecretAgentStealthMissionCover(SimSecretAgentStealthMission mission, int eventsCompleted = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
