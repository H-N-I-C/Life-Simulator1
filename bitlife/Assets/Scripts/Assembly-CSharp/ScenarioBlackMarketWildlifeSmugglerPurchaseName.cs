using System.Collections;

public class ScenarioBlackMarketWildlifeSmugglerPurchaseName : Scenario
{
	private readonly double _cost;

	private readonly SimPet _pet;

	private readonly SimBlackMarketVendor _vendor;

	public ScenarioBlackMarketWildlifeSmugglerPurchaseName(SimBlackMarketVendor vendor, SimPet pet, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
