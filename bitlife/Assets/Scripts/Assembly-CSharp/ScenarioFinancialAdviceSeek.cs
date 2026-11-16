using System.Collections;
using System.Collections.Generic;

public class ScenarioFinancialAdviceSeek : Scenario
{
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public string advisorString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(KeyValuePair<SimPerson, string> x)
		{
			return false;
		}
	}

	protected SimInvestmentMarket _market;

	protected Dictionary<SimPerson, string> advisorsDictionary;

	public ScenarioFinancialAdviceSeek(SimInvestmentMarket theMarket)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
