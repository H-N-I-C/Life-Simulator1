using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioBlackMarketStorageUnitAuctionBidAmount : Scenario
{
	private string _narrative;

	private readonly SimAuctionHouse _auctionHouse;

	private readonly bool _isOpeningBid;

	private readonly float _currentBid;

	private readonly SimStreetPerson _person;

	public ScenarioBlackMarketStorageUnitAuctionBidAmount(SimAuctionHouse auctionHouse, SimStreetPerson person, bool isOpeningBid, float bidAmount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool HasItemsToSell()
	{
		return false;
	}

	private SimAuctionItem GenerateHiddenItem()
	{
		return null;
	}

	private bool FoundHiddenItem()
	{
		return false;
	}

	private List<SimBurgleItem> GenerateItemsToSell()
	{
		return null;
	}

	private bool CanLoopBid()
	{
		return false;
	}
}
