public class ScenarioBlackMarketAntiquePurchaseCollected : Scenario
{
	private readonly double _cost;

	private readonly SimAuctionItem _item;

	private readonly SimBlackMarketVendor _vendor;

	private readonly bool _isRewardedAd;

	public ScenarioBlackMarketAntiquePurchaseCollected(SimBlackMarketVendor vendor, SimAuctionItem item, double cost, bool isRewardedAd = false)
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
