using System.Collections;

public class ScenarioBlackMarketJewelryFencerPurchaseHaggle : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly float _cost;

	private string _narrative;

	private readonly SimBlackMarketVendor _vendor;

	private readonly bool _fromDiscountScenario;

	private float _previousBidValue;

	public ScenarioBlackMarketJewelryFencerPurchaseHaggle(SimBlackMarketVendor vendor, SimAuctionItem item, float cost, bool fromDiscountScenario, float previousBidValue = 0f)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
