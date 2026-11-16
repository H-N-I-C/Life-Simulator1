public class ScenarioHouseSellOffer : Scenario
{
	protected SimHouse _house;

	protected float _listPrice;

	protected string _customer;

	protected int _customerGender;

	protected float _offer;

	public ScenarioHouseSellOffer(SimHouse theHouse, float theListPrice)
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

	private bool _003CChoiceSelectedAtIndex_003Eb__7_0(SimPropertyLeaseContract x)
	{
		return false;
	}
}
