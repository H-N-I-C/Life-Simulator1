public class ScenarioFightAdCampaignGood : ScenarioFightAdCampaignBase
{
	private readonly int _streetCredsGained;

	public ScenarioFightAdCampaignGood(FightClubAdCampaignActivity activity, int streetCredsGained)
		: base(null, null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
