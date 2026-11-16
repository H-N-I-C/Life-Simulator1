public class ScenarioBlackMarketAntiquePurchase : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly SimBlackMarketVendor _vendor;

	private string _narrative;

	private double _cost;

	private readonly bool _isHaggle;

	public ScenarioBlackMarketAntiquePurchase(SimBlackMarketVendor vendor, SimAuctionItem item)
	{
	}

	public ScenarioBlackMarketAntiquePurchase(SimBlackMarketVendor vendor, SimAuctionItem item, double cost, bool isHaggle)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool CanKeepLooking()
	{
		return false;
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
