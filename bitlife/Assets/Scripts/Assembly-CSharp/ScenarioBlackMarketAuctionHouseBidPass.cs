using Game;

public class ScenarioBlackMarketAuctionHouseBidPass : Scenario
{
	private readonly SimAuctionItem _auctionItem;

	private readonly SimAuctionHouse _simAuctionHouse;

	private readonly float _currentBid;

	private BracketParser _parser;

	private string _narrative;

	private readonly bool _showFeedback;

	public ScenarioBlackMarketAuctionHouseBidPass(SimAuctionHouse simAuctionHouse, SimAuctionItem auctionItem, float bidAmount, BracketParser randomPersonBracketParser, bool showFeedback = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
