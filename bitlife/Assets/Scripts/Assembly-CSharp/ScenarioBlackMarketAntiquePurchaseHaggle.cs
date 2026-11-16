using System.Collections;

public class ScenarioBlackMarketAntiquePurchaseHaggle : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly double _cost;

	private string _narrative;

	private readonly SimBlackMarketVendor _vendor;

	private bool _fromDiscountScenario;

	private float _previousBidValue;

	public ScenarioBlackMarketAntiquePurchaseHaggle(SimBlackMarketVendor vendor, SimAuctionItem item, double cost, bool fromDiscountScenario, float previousBidValue = 0f)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
