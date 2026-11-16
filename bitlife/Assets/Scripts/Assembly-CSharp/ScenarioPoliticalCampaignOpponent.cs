public class ScenarioPoliticalCampaignOpponent : Scenario
{
	protected SimPoliticalCampaign _campaign;

	public ScenarioPoliticalCampaignOpponent(SimPoliticalCampaign theCampaign)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public DataPoliticalParty GetPoliticalPartyForOpponent()
	{
		return null;
	}

	public int GetElectionPlatformIssueForOpponent()
	{
		return 0;
	}
}
