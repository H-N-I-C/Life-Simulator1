public class ScenarioPoliticalCampaignResults : Scenario
{
	protected SimPoliticalCampaign _campaign;

	protected bool _didWin;

	protected string _narrative;

	public ScenarioPoliticalCampaignResults(SimPoliticalCampaign theCampaign)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
