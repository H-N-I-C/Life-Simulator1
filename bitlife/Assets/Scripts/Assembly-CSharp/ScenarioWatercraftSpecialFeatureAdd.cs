using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioWatercraftSpecialFeatureAdd : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataLuxuryLuxuryWatercraftSpecialFeature t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimWatercraft _watercraft;

	private readonly bool _newlyPurchasedWatercraft;

	private List<DataLuxuryLuxuryWatercraftSpecialFeature> _listFeatures;

	public ScenarioWatercraftSpecialFeatureAdd(SimWatercraft watercraft, bool newlyPurchasedWatercraft = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
