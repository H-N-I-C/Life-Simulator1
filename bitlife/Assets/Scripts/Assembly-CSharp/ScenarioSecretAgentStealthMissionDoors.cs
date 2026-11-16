using Game;

public class ScenarioSecretAgentStealthMissionDoors : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentStealthMission _mission;

	private string _noiseLeft;

	private string _noiseRight;

	private int _eventsCompleted;

	private DataSecretAgentStealthDoorNoise _dataDoorLeft;

	private DataSecretAgentStealthDoorNoise _dataDoorRight;

	public ScenarioSecretAgentStealthMissionDoors(SimSecretAgentStealthMission mission, int eventsCompleted = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
