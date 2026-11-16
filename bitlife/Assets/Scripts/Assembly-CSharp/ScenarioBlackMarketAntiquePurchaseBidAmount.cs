using System.Collections;

public class ScenarioBlackMarketAntiquePurchaseBidAmount : Scenario
{
	private double _currentOffer;

	private readonly SimAuctionItem _item;

	private string _narrative;

	private readonly SimBlackMarketVendor _vendor;

	private readonly SimStreetPerson _person;

	public ScenarioBlackMarketAntiquePurchaseBidAmount(SimStreetPerson person, SimBlackMarketVendor vendor, SimAuctionItem item, double currentOffer)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool VendorWillAcceptBid(double selectedBidValue)
	{
		return false;
	}
}
