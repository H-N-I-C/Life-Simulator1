using Game;

public class ScenarioBlackMarketStorageUnitAuctionBidPass : Scenario
{
	private readonly SimAuctionHouse _auctionHouse;

	private readonly float _currentBid;

	private string _narrative;

	private readonly bool _showFeedback;

	public ScenarioBlackMarketStorageUnitAuctionBidPass(SimAuctionHouse auctionHouse, float currentBid, bool showFeedback)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
