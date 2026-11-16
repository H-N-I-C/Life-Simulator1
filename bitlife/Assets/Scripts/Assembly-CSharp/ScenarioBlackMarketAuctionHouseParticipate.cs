using Game;

public class ScenarioBlackMarketAuctionHouseParticipate : Scenario
{
	private string _narrative;

	private readonly SimAuctionHouse _auctionHouse;

	private readonly SimAuctionItem _auctionItem;

	private float _reservePrice;

	private readonly int _passCount;

	public ScenarioBlackMarketAuctionHouseParticipate(SimAuctionHouse auctionHouse, SimAuctionItem auctionItem, int passCount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
