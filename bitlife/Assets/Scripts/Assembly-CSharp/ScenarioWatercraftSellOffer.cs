public class ScenarioWatercraftSellOffer : Scenario
{
	protected SimWatercraft _watercraft;

	protected double _listPrice;

	protected bool _blackMarket;

	protected string _customer;

	protected int _customerGender;

	protected double _offer;

	public ScenarioWatercraftSellOffer(SimWatercraft theWatercraft, double theListPrice, bool isBlackMarket)
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
