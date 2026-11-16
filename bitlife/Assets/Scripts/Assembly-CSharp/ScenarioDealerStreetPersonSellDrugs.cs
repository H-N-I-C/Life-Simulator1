using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerStreetPersonSellDrugs : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public SimDealerDrug drug;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(KeyValuePair<string, float> t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerStreetPerson _person;

	private List<SimDealerDrug> _drugs;

	private ArrayList _drugDropDownContents;

	public ScenarioDealerStreetPersonSellDrugs(SimDealerStreetPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string GetRandomDrug()
	{
		return null;
	}
}
