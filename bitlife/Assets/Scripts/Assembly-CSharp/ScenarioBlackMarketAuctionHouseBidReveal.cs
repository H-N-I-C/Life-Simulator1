using Game;

public class ScenarioBlackMarketAuctionHouseBidReveal : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly SimAuctionHouse _auctionHouse;

	private readonly float _cost;

	public ScenarioBlackMarketAuctionHouseBidReveal(SimAuctionHouse auctionHouse, SimAuctionItem simItem, float cost)
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
