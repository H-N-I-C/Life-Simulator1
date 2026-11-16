public class ScenarioSecretAgentInfiltrateMissionDossier : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private SimRivalSpyAgency _rivalAgency;

	private SimSecretAgentMission _mission;

	public ScenarioSecretAgentInfiltrateMissionDossier(SimRivalSpyAgency rivalAgency, SimSecretAgentMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
