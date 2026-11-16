public class ScenarioBlackMarketJewelryFencerPurchaseHaggleDiscount : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly SimBlackMarketVendor _vendor;

	private readonly float _cost;

	private readonly float _yourPrice;

	private float _discount;

	private string _narrative;

	public ScenarioBlackMarketJewelryFencerPurchaseHaggleDiscount(SimBlackMarketVendor vendor, SimAuctionItem item, float cost, float yourPrice, float discount = 0f)
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
