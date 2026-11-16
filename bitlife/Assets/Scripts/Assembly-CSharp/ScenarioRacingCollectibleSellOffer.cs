public class ScenarioRacingCollectibleSellOffer : Scenario
{
	private readonly SimRacingCollectible _collectible;

	private readonly float _listPrice;

	private string _customer;

	private int _customerGender;

	private float _offer;

	public ScenarioRacingCollectibleSellOffer(SimRacingCollectible theCollectible, float theListPrice)
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
