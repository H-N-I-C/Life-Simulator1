using Game;

public class ScenarioBlackMarketStorageUnitAuctionBid : Scenario
{
	private string _narrative;

	private readonly SimAuctionHouse _auctionHouse;

	private readonly bool _isOpeningBid;

	private float _bidAmount;

	private SimStreetPerson _person;

	public ScenarioBlackMarketStorageUnitAuctionBid(SimAuctionHouse auctionHouse, SimStreetPerson person, float bidAmount, bool isOpeningBid)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void CountdownTimerExpired()
	{
	}

	public override void CountdownTimerUpdated(int currentTimerValue)
	{
	}

	private float GenerateNewBidAmount()
	{
		return 0f;
	}
}
