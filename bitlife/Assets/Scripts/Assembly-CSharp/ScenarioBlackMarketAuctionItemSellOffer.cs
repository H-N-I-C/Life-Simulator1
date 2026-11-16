public class ScenarioBlackMarketAuctionItemSellOffer : Scenario
{
	private readonly SimAuctionItem _item;

	private readonly string _customer;

	private readonly float _listPrice;

	private readonly DataStreetPerson _buyer;

	private string _narrative;

	private float _offer;

	public ScenarioBlackMarketAuctionItemSellOffer(SimAuctionItem item, float listPrice, DataStreetPerson buyer)
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
