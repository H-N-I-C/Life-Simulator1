public class ScenarioDealerHeroRanksUp : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private SimDealerCartelMember _currentKingpin;

	private DEALER_RANK _previousRank;

	private readonly DEALER_RANK _overrideRankToPromoteFrom;

	private DEALER_RANK _rankPromotedTo;

	public ScenarioDealerHeroRanksUp(SimDealerCartelMember currentKingpin = null, DEALER_RANK overrideRankToPromoteFrom = DEALER_RANK.NONE)
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
