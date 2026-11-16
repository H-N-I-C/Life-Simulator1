using System.Collections;
using Game;

public class ScenarioBlackMarketAuctionHouseBidAmount : Scenario
{
	private readonly bool _isOpeningBid;

	private readonly SimAuctionHouse _auctionHouse;

	private readonly SimAuctionItem _auctionItem;

	private readonly float _bidAmount;

	private readonly int _bidSeconds;

	private string _narrative;

	public ScenarioBlackMarketAuctionHouseBidAmount(SimAuctionHouse auctionHouse, SimAuctionItem auctionItem, float bidAmount, bool isOpeningBid, int bidSeconds)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool ShowShouldPushOfferScenario()
	{
		return false;
	}
}
