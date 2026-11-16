using Game;

public class ScenarioBlackMarketStorageAuctionInsufficientFunds : Scenario
{
	private readonly SimAuctionHouse _auctionHouse;

	private readonly double _cost;

	private string _narrative;

	public ScenarioBlackMarketStorageAuctionInsufficientFunds(SimAuctionHouse auctionHouse, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
