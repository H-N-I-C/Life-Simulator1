public class ScenarioBlackMarketAntiquePurchaseHaggleDiscount : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly SimBlackMarketVendor _vendor;

	private readonly double _cost;

	private readonly double _yourPrice;

	private double _discount;

	private string _narrative;

	public ScenarioBlackMarketAntiquePurchaseHaggleDiscount(SimBlackMarketVendor vendor, SimAuctionItem item, double cost, double yourPrice, float discount = 0f)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
