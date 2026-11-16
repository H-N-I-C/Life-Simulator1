public class ScenarioInstrumentSellOffer : Scenario
{
	protected SimMusicInstrumentPhysical _theInstrument;

	protected float _listPrice;

	protected string _customer;

	protected int _customerGender;

	protected float _offer;

	public ScenarioInstrumentSellOffer(SimMusicInstrumentPhysical theInstrument, float theListPrice)
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
