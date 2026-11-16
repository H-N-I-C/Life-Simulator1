using System.Collections;

public class ScenarioBlackMarketMuseumItemDisplayChange : Scenario
{
	private readonly SimMuseumWing _simMuseumWing;

	private readonly SimAuctionItem _simAuctionItem;

	private readonly SimDisplayItem _simDisplayItem;

	private readonly int _slotIndex;

	public ScenarioBlackMarketMuseumItemDisplayChange(SimMuseumWing simMuseumWing, SimAuctionItem simAuctionItem, SimDisplayItem simDisplayItem, int slotIndex)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private int GetItemTypeForMuseum(int marketType)
	{
		return 0;
	}
}
