using System.Collections;

public class ScenarioBlackMarketAuctionItemSell : Scenario
{
	private readonly SimAuctionItem _item;

	private string _narrative;

	private readonly DataStreetPerson _buyer;

	public ScenarioBlackMarketAuctionItemSell(SimAuctionItem item)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
