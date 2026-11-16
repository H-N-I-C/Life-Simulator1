public class ScenarioSecretAgentMissionDossier : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentMission _mission;

	public ScenarioSecretAgentMissionDossier(SimSecretAgentMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private string DossierDescription()
	{
		return null;
	}

	private string Objective()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
