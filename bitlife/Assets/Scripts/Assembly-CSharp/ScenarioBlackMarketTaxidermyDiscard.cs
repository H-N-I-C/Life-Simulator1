using System.Collections;
using System.Collections.Generic;

public class ScenarioBlackMarketTaxidermyDiscard : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string selectedItem;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataBlackMarketTaxidermyDiscardLocation t)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(DataBlackMarketTaxidermyDiscardLocation t)
		{
			return false;
		}
	}

	private readonly SimAuctionItem _item;

	private string _narrative;

	private List<DataBlackMarketTaxidermyDiscardLocation> _locations;

	private int _riskScore;

	public ScenarioBlackMarketTaxidermyDiscard(SimAuctionItem item)
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
