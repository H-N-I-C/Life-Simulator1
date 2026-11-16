public class ScenarioFightAdCampaignResult : ScenarioFightAdCampaignBase
{
	private readonly float _reception;

	public ScenarioFightAdCampaignResult(FightClubAdCampaignActivity activity, float reception, DismissCallback onDismiss)
		: base(null, null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override void RefreshScenario()
	{
	}
}
