public class ScenarioSecretAgentMissionInterruptionFight : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentMission _mission;

	private string _rivalAgencyName;

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

	public ScenarioSecretAgentMissionInterruptionFight(SimSecretAgentMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
