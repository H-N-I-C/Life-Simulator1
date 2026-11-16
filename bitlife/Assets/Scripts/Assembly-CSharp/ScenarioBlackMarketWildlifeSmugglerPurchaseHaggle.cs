using System.Collections;

public class ScenarioBlackMarketWildlifeSmugglerPurchaseHaggle : Scenario
{
	private readonly SimPet _pet;

	private readonly float _cost;

	private string _narrative;

	private readonly SimBlackMarketVendor _vendor;

	private readonly bool _fromDiscountScenario;

	private float _previousBidValue;

	private float _originalPassthroughCost;

	public ScenarioBlackMarketWildlifeSmugglerPurchaseHaggle(SimBlackMarketVendor vendor, SimPet pet, float cost, bool fromDiscountScenario, float previousBidValue = 0f, float originalPassthroughCost = 0f)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool CanVendorAcceptHaggleBid(float haggleValue)
	{
		return false;
	}
}
