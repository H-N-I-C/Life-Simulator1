using Game;

public class ScenarioBlackMarketAuctionHouseBidWar : Scenario
{
	private readonly SimAuctionHouse _auctionHouse;

	private readonly SimAuctionItem _auctionItem;

	private readonly float _bidAmount;

	private string _narrative;

	private BracketParser _parser;

	private SimPerson _randomPerson;

	private readonly bool _isOutbid;

	public ScenarioBlackMarketAuctionHouseBidWar(SimAuctionHouse auctionHouse, SimAuctionItem auctionItem, float reservePrice, bool isOutbid)
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
}
