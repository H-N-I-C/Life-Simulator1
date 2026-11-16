public class ScenarioCarSellOffer : Scenario
{
	protected SimCar _car;

	protected float _listPrice;

	protected bool _blackMarket;

	protected DataStreetPerson _customer;

	protected double _offer;

	public ScenarioCarSellOffer(SimCar theCar, float theListPrice, bool isBlackMarket)
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
