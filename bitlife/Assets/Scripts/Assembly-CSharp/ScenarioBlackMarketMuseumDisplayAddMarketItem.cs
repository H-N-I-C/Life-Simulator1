public class ScenarioBlackMarketMuseumDisplayAddMarketItem : Scenario
{
	private readonly SimAuctionItem _simAuctionItem;

	private readonly SimMuseumWing _simMuseumWing;

	private readonly SimDisplayItem _simDisplayItem;

	private readonly int _slotIndex;

	public ScenarioBlackMarketMuseumDisplayAddMarketItem(SimMuseumWing simMuseumWing, SimAuctionItem simAuctionItem, SimDisplayItem simDisplayItem, int slotIndex)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private int GetItemTypeForMuseum(int marketType)
	{
		return 0;
	}

	public override string EmojiString()
	{
		return null;
	}
}
