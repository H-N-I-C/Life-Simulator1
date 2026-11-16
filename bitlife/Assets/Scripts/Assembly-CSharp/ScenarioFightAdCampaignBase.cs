public abstract class ScenarioFightAdCampaignBase : ScenarioLazyCsvBase
{
	protected readonly FightClubAdCampaignActivity _activity;

	protected ScenarioFightAdCampaignBase(FightClubAdCampaignActivity activity, string scenarioId, ChoiceCallback choiceCallback = null, DismissCallback onDismiss = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
