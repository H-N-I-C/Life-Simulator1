public class ScenarioAircraftSellOffer : Scenario
{
	protected SimAircraft _aircraft;

	protected float _listPrice;

	protected bool _blackMarket;

	protected string _customer;

	protected int _customerGender;

	protected double _offer;

	public ScenarioAircraftSellOffer(SimAircraft theAircraft, float theListPrice, bool isBlackMarket)
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
