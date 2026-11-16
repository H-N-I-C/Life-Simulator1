using System.Collections;
using System.Collections.Generic;

public class ScenarioBlackMarketStreetChemistRequestDose : Scenario
{
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public DataBlackMarketStreetChemistProduct item;

		internal bool _003CRefreshScenario_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private readonly SimBlackMarketVendor _vendor;

	private List<SimAuctionItem> _items;

	private string _narrative;

	public ScenarioBlackMarketStreetChemistRequestDose(SimBlackMarketVendor vendor)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
