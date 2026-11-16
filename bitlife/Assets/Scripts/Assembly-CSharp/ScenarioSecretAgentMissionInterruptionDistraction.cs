using Game;

public class ScenarioSecretAgentMissionInterruptionDistraction : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentMission _mission;

	private string _randomPersonDescription;

	private DataSecretAgentDistractionRandomPeople _dataPerson;

	private bool MissionWillContinue__BackingField;

	public bool MissionWillContinue
	{
		get
		{
			return MissionWillContinue__BackingField;
		}
		set
		{
			MissionWillContinue__BackingField = value;
		}
	}

	public ScenarioSecretAgentMissionInterruptionDistraction(SimSecretAgentMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
