using System.Collections;
using System.Collections.Generic;

public class ScenarioFinancialAssetSeekAdvice : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string advisorString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(KeyValuePair<SimPerson, string> x)
		{
			return false;
		}
	}

	protected SimCrypto _crypto;

	protected SimStock _stock;

	protected bool _giveBuyAdvice;

	protected Dictionary<SimPerson, string> advisorsDictionary;

	public ScenarioFinancialAssetSeekAdvice(SimStock stock, bool giveBuyAdvice)
	{
	}

	public ScenarioFinancialAssetSeekAdvice(SimCrypto crypto, bool giveBuyAdvice)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
