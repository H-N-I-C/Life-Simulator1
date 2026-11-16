public class ScenarioSecretAgentMissionInterruptionGadgetFeedback : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentMission _mission;

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

	public ScenarioSecretAgentMissionInterruptionGadgetFeedback(SimSecretAgentMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}
}
