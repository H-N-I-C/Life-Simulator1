public class ScenarioModelGiftSellOffer : Scenario
{
	protected SimModelGift _gift;

	protected float _listPrice;

	protected string _customer;

	protected int _customerGender;

	protected float _offer;

	public ScenarioModelGiftSellOffer(SimModelGift theGift, float theListPrice)
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
