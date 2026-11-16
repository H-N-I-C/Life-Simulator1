using Game;

public class ScenarioBlackMarketAuctionHouseBid : Scenario
{
	private readonly SimAuctionHouse _auctionHouse;

	private readonly SimAuctionItem _item;

	private readonly float _bidAmount;

	private string _narrative;

	public ScenarioBlackMarketAuctionHouseBid(SimAuctionHouse auctionHouse, SimAuctionItem item, float reservePrice)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void CountdownTimerExpired()
	{
	}

	public override void CountdownTimerUpdated(int currentTimerValue)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
