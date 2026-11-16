public class ScenarioOutdoorVehicleSellOffer : Scenario
{
	private readonly SimRecreationalVehicle _recreationalVehicle;

	protected float _listPrice;

	protected bool _blackMarket;

	protected DataStreetPerson _customer;

	protected double _offer;

	public ScenarioOutdoorVehicleSellOffer(SimRecreationalVehicle vehicle, float price, bool isBlackMarket)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
