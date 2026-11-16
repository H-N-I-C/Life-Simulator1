using System.Collections;

public class ScenarioBlackMarketWildlifeSmugglerPurchaseBidAmount : Scenario
{
	private double _currentOffer;

	private double _originalCost;

	private readonly SimPet _pet;

	private string _narrative;

	private readonly SimBlackMarketVendor _vendor;

	private readonly SimStreetPerson _person;

	public ScenarioBlackMarketWildlifeSmugglerPurchaseBidAmount(SimStreetPerson person, SimBlackMarketVendor vendor, SimPet pet, double currentOffer, double originalCost)
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
