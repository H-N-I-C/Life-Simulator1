public class ScenarioBlackMarketWildlifeSmugglerPurchaseHaggleDiscount : Scenario
{
	private readonly SimPet _pet;

	private readonly SimBlackMarketVendor _vendor;

	private readonly float _cost;

	private readonly float _yourPrice;

	private float _discount;

	private string _narrative;

	public ScenarioBlackMarketWildlifeSmugglerPurchaseHaggleDiscount(SimBlackMarketVendor vendor, SimPet pet, float cost, float yourPrice, float discount = 0f)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
