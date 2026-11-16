public class ScenarioBlackMarketWildlifeSmugglerPurchaseBidWar : Scenario
{
	private readonly SimPet _pet;

	private readonly SimBlackMarketVendor _vendor;

	private string _narrative;

	private readonly double _cost;

	private double _offer;

	private SimStreetPerson _simPerson;

	public ScenarioBlackMarketWildlifeSmugglerPurchaseBidWar(SimBlackMarketVendor vendor, SimPet pet, double cost)
	{
	}

	public ScenarioBlackMarketWildlifeSmugglerPurchaseBidWar(SimBlackMarketVendor vendor, SimStreetPerson person, SimPet pet, double cost, double offer)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
