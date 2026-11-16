public class ScenarioSecretAgentStealthMissionObstruction : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentStealthMission _mission;

	private string _obstructionPerson;

	private GENDER _obstructionPersonGender;

	private int _obstructionPersonThreatScore;

	private int _eventsCompleted;

	public ScenarioSecretAgentStealthMissionObstruction(SimSecretAgentStealthMission mission, int eventsCompleted = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
