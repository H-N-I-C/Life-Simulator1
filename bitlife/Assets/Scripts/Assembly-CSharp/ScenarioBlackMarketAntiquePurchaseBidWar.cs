public class ScenarioBlackMarketAntiquePurchaseBidWar : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly SimBlackMarketVendor _vendor;

	private string _narrative;

	private readonly double _cost;

	private double _offer;

	private SimStreetPerson _simPerson;

	public ScenarioBlackMarketAntiquePurchaseBidWar(SimBlackMarketVendor vendor, SimAuctionItem item, double cost)
	{
	}

	public ScenarioBlackMarketAntiquePurchaseBidWar(SimBlackMarketVendor vendor, SimStreetPerson person, SimAuctionItem item, double cost, double offer)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
